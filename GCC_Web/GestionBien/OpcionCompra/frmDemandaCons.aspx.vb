Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class GestionBien_OpcionBien_frmDemandaCons
    Inherits GCCBase

    Dim objLog As New GCCLog("frmDemandaCons.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 04/01/2013
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then

                'Setea Modo Transaccion
                Me.hddTipoTx.Value = GCCConstante.C_TX_NUEVO
                Me.hddTipoTxImplicado.Value = GCCConstante.C_TX_NUEVO

                'Verifica Edicion
                Dim strCodContrato As String = Request.QueryString("hddCodContrato")
                Dim strCodBien As String = Request.QueryString("hddCodBien")
                Dim strCodDemanda As String = Request.QueryString("hddCodDemanda")
                Dim strCodSiniestro As String = Request.QueryString("hddCodSiniestro")
                Dim strCodUnico As String = Request.QueryString("hddCodUnico")
                Dim strVer As String = Request.QueryString("hddVer")


                'Pone Valores
                Me.hddCodContrato.Value = strCodContrato
                Me.hddCodBien.Value = strCodBien
                Me.hddCodSiniestro.Value = strCodSiniestro
                Me.hddCodUnico.Value = strCodUnico
                Me.hddVer.Value = strVer

                'Carga Combos Siniestro
                GCCUtilitario.CargarComboMoneda(cmbMonedaIndem)
                GCCUtilitario.CargarComboMoneda(cmbMonedaCuenta)

                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoCuenta, GCCConstante.C_TABLAGENERICA_TIPO_CTA_CTE)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipo, GCCConstante.C_TABLAGENERICA_Siniestro_Tipo)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbSituacion, GCCConstante.C_TABLAGENERICA_Siniestro_Situacion)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbAplicacion, GCCConstante.C_TABLAGENERICA_Siniestro_Aplicacion)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTransferencia, GCCConstante.C_TABLAGENERICA_Siniestro_Transferencia)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbContrato, GCCConstante.C_TABLAGENERICA_Siniestro_Contrato)

                GCCUtilitario.CargarComboValorGenerico(Me.cmbSeguro, GCCConstante.C_TABLAGENERICA_Siniestro_Seguro)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbOrigenCono, GCCConstante.C_TABLAGENERICA_Siniestro_Origen)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbEstadoBien, GCCConstante.C_TABLAGENERICA_Siniestro_EstadoBien)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoPoliza, GCCConstante.C_TABLAGENERICA_Siniestro_TipoPoliza)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbBancoEmite, GCCConstante.C_TABLAGENERICA_Siniestro_BancoEmite)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbAplicaFondo, GCCConstante.C_TABLAGENERICA_Siniestro_AplicaFondo)

                'Carga Combos Demanda
                GCCUtilitario.CargarComboMoneda(cmbMonedaDemanda)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbEstadoDemanda, GCCConstante.C_TABLAGENERICA_Demanda_Estado)

                'Fecha Hoy
                Dim dtFecha As Date = Now
                Me.txtFechaSituacion.Value = dtFecha.ToString("dd/MM/yyyy")

                'Carga Datos Editar
                If Not strCodDemanda Is Nothing Then
                    Me.hddTipoTx.Value = GCCConstante.C_TX_EDITAR
                    CargaEditarDemanda(strCodContrato, strCodBien, strCodDemanda, strCodSiniestro)
                End If

            End If

        Catch ex As ApplicationException
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "')", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub

#End Region

#Region "WebMethods"

    ''' <summary>
    ''' Lista Implicados
    ''' </summary>
    ''' <returns>Listado de Implicados</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaImplicados(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pstrCodContrato As String, _
                                         ByVal pstrCodBien As String, _
                                         ByVal pstrCodDemanda As String) As JQGridJsonResponse

        'Variables
        Dim objDemandaNTx As New LDemandaNTx

        Try

            'Inicializa Objeto
            Dim objEImplicado As New EGCC_Implicado
            Dim strEImplicado As String
            With objEImplicado
                .CodSolCredito = GCCUtilitario.NullableString(pstrCodContrato)
                .SecFinanciamiento = GCCUtilitario.StringToInteger(pstrCodBien)
                .CodDemanda = GCCUtilitario.NullableString(pstrCodDemanda)
            End With
            strEImplicado = GCCUtilitario.SerializeObject(Of EGCC_Implicado)(objEImplicado)

            'Ejecuta Consulta
            Dim dtImplicado As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objDemandaNTx.ListadoDemandaImplicado(pPageSize, _
                                                                                                                               pCurrentPage, _
                                                                                                                               pSortColumn, _
                                                                                                                               pSortOrder, _
                                                                                                                               strEImplicado))
            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtImplicado.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtImplicado.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtImplicado.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtImplicado)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function


#End Region

#Region "Metodos"

    ''' <summary>
    ''' Get Siniestro
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 04/01/2013
    ''' </remarks>
    Protected Sub CargaEditarDemanda(ByVal pstrCodContrato As String, _
                                     ByVal strCodBien As String, _
                                     ByVal strCodDemanda As String, _
                                     ByVal strCodSiniestro As String)

        Try

            Dim objDemandaNTx As New LDemandaNTx
            Dim objSiniestroNTx As New LSiniestroNTx
            Dim msgError As String = ""

            'Inicializa Objeto Demanda
            Dim objEDemanda As New EGcc_Demanda
            Dim strEDemanda As String
            With objEDemanda
                .CodSolCredito = GCCUtilitario.NullableString(pstrCodContrato)
                .SecFinanciamiento = GCCUtilitario.CheckInt(strCodBien)
                .CodDemanda = GCCUtilitario.CheckInt(strCodDemanda)
            End With
            strEDemanda = GCCUtilitario.SerializeObject(objEDemanda)

            'Ejecuta Consulta
            Dim dtDemanda As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objDemandaNTx.GetDemanda(strEDemanda))

            'Valida si existe
            If dtDemanda.Rows.Count > 0 Then

                Me.hddCodDemanda.Value = dtDemanda.Rows(0).Item("CodDemanda").ToString
                Me.txtNroDemanda.Value = dtDemanda.Rows(0).Item("NroDemanda").ToString
                Me.txtFechaDemanda.Value = dtDemanda.Rows(0).Item("FechaDemanda").ToString
                Me.txtJuzgado.Value = dtDemanda.Rows(0).Item("Juzgado").ToString
                Me.txtMontoDemanda.Value = GCCUtilitario.CheckDecimal(dtDemanda.Rows(0).Item("MontoDemanda").ToString()).ToString(GCCConstante.C_FormatMiles)

                GCCUtilitario.SeleccionaCombo(cmbEstadoDemanda, dtDemanda.Rows(0).Item("EstadoDemanda").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbMonedaDemanda, dtDemanda.Rows(0).Item("CodMoneda").ToString.Trim)

            End If


            'Inicializa Objeto Siniestro
            Dim objESiniestro As New ESiniestro
            Dim strESiniestro As String
            With objESiniestro
                .Codsolcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .Secfinanciamiento = GCCUtilitario.CheckInt(strCodBien)
                .Secsiniestro = GCCUtilitario.CheckInt(strCodSiniestro)
            End With
            strESiniestro = GCCUtilitario.SerializeObject(objESiniestro)

            'Ejecuta Consulta
            Dim dtSiniestro As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objSiniestroNTx.GetSiniestro(strESiniestro))

            'Valida si existe
            If dtSiniestro.Rows.Count > 0 Then

                Me.hddCodSiniestro.Value = dtSiniestro.Rows(0).Item("SecSiniestro").ToString

                Me.hddNroSiniestro.Value = dtSiniestro.Rows(0).Item("NroSiniestro").ToString
                Me.txtNroSiniestro.Value = dtSiniestro.Rows(0).Item("NroSiniestro").ToString
                Me.txtFechaConoBanco.Value = dtSiniestro.Rows(0).Item("FecConocimientoBanco").ToString
                Me.txtFechaConoLeasing.Value = dtSiniestro.Rows(0).Item("FecConocimiento").ToString
                Me.txtFechaSiniestro.Value = dtSiniestro.Rows(0).Item("FecSiniestro").ToString
                Me.txtFechaSituacion.Value = dtSiniestro.Rows(0).Item("FecSituacion").ToString
                Me.txtFechaAplicacion.Value = dtSiniestro.Rows(0).Item("FecAplicacion").ToString

                Me.txtFechaDescargoMunicipal.Value = dtSiniestro.Rows(0).Item("FecDescargoMunicipal").ToString
                Me.txtFechaTransparencia.Value = dtSiniestro.Rows(0).Item("FecTransferencia").ToString
                Me.txtChequeAseg.Value = dtSiniestro.Rows(0).Item("NroChequeAseguradora").ToString
                Me.txtNroPoliza.Value = dtSiniestro.Rows(0).Item("NroPoliza").ToString
                Me.txtFechaIndem.Value = dtSiniestro.Rows(0).Item("FecRecIndemnizacion").ToString
                Me.txtMontoIndem.Value = GCCUtilitario.CheckDecimal(dtSiniestro.Rows(0).Item("MontoIndemnizacion").ToString()).ToString(GCCConstante.C_FormatMiles)
                Me.txtNroCuenta.Value = dtSiniestro.Rows(0).Item("NroCuenta").ToString

                Me.hddTipoSiniestro.Value = dtSiniestro.Rows(0).Item("CodTipoSiniestro").ToString

                GCCUtilitario.SeleccionaCombo(cmbTipo, dtSiniestro.Rows(0).Item("Tipo").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbSituacion, dtSiniestro.Rows(0).Item("Situacion").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbContrato, dtSiniestro.Rows(0).Item("Contrato").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbAplicacion, dtSiniestro.Rows(0).Item("Aplicacion").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbTransferencia, dtSiniestro.Rows(0).Item("Transferencia").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbOrigenCono, dtSiniestro.Rows(0).Item("Origen").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbSeguro, dtSiniestro.Rows(0).Item("Seguro").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbEstadoBien, dtSiniestro.Rows(0).Item("CodEstadoBien").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbTipoPoliza, dtSiniestro.Rows(0).Item("CodTipoPoliza").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbMonedaIndem, dtSiniestro.Rows(0).Item("Moneda").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbBancoEmite, dtSiniestro.Rows(0).Item("CodBancoEmiteCheque").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbAplicaFondo, dtSiniestro.Rows(0).Item("CodAplicaFondo").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbMonedaCuenta, dtSiniestro.Rows(0).Item("CodMonedaCuenta").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbTipoCuenta, dtSiniestro.Rows(0).Item("CodTipoCuenta").ToString.Trim)

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Total Paginas
    ''' </summary>
    ''' <param name="total"></param>
    ''' <param name="pPageSize"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function TotalPaginas(ByVal total As Integer, ByVal pPageSize As Integer) As Integer
        If (total Mod pPageSize > 0) Then
            Return total \ pPageSize + 1
        Else
            Return total \ pPageSize
        End If
    End Function

#End Region

End Class
