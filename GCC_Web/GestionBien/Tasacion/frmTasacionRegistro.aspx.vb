


Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS


Partial Class GestionBien_Tasacion_frmTasacionRegistro
    Inherits GCCBase
    Dim objLog As New GCCLog("frmTasacionRegistro.aspx.vb")
    Dim strNroContrato As String

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                GCCUtilitario.CargarComboValorGenerico(Me.cbmEstadoContrato, GCCConstante.C_TABLAGENERICA_ESTADO_CONTRATO)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbClasificacionBien, GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbBanca, GCCConstante.C_TABLAGENERICA_BANCA_ATENCION)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbEjecutivoLeasing, GCCConstante.C_TABLAGENERICA_EJECUTIVO)
                GCCUtilitario.CargarComboMoneda(Me.cmbMoneda)
                CargarComboTasador(GCCConstante.C_TABLAGENERICA_Tasador)
                hddCodigoContratos.Value = Request.QueryString("cc")
                txtcontrato.Value = Request.QueryString("cc")
                LeerContrato(hddCodigoContratos.Value)
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

    Private Sub LeerContrato(ByVal codSolicitudCredito As String)
        Dim oLwsTasadorNtx As New LTasadorNTx
        Dim oLwsMantenimiento As New LMantenimientoNTX
        Try
            Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsTasadorNtx.ObtenerContratoCotizacionSaldoFinanciado(codSolicitudCredito))
            If odtbDatos IsNot Nothing Then
                If odtbDatos.Rows.Count > 0 Then
                    For Each dr As DataRow In odtbDatos.Rows
                        txtContrato.Value = codSolicitudCredito
                        txtcucliente.Value = dr("CodUnico").ToString
                        txtrazonsocial.Value = dr("NOMBRECLIENTE").ToString
                        cbmEstadoContrato.Value = dr("CODIGOESTADOCONTRATO").ToString.Trim
                        cmbClasificacionBien.Value = dr("CODIGOCLASIFICACIONBIEN").ToString.Trim
                        'txtFechaActivacion.Value = dr("FECHAACTIVACION").ToString   ''GCCUtilitario.ToStringyyyyMMdd(Today.ToString)
                        If (dr("FECHAACTIVACION").ToString.Trim <> "") Then
                            txtFechaActivacion.Value = Convert.ToDateTime(dr("FECHAACTIVACION").ToString.Trim).ToString("dd/MM/yyyy")
                        End If
                        cmbbanca.Value = dr("CODBANCA").ToString.Trim
                        cmbMoneda.Value = dr("CODMONEDA").ToString.Trim
                        txtdesejecutivobanca.Value = dr("DesEjecutivoBanca").ToString.Trim
                        cmbEjecutivoLeasing.Value = dr("Ejecutivoleasing").ToString.Trim

                        If (dr("CODMONEDA").ToString.Trim = "001") Then
                            txtcapitalfinanciadosoles.Value = Format(CDbl(dr("Capitalfinanciamiento").ToString.Trim), "#,##0.00")
                        Else
                            txtcapitalfinanciadodolar.Value = Format(CDbl(dr("Capitalfinanciamiento").ToString.Trim), "#,##0.00")
                        End If

                        If (dr("CODMONEDA").ToString.Trim = "001") Then
                            txtsaldocapitalsoles.Value = Format(CDbl(dr("Capitalfinanciamiento").ToString), "#,##0.00")
                        Else
                            '' txtsaldocapitaldolares.Value = dr("Capitalfinanciamiento").ToString.Trim
                            txtsaldocapitaldolares.Value = Format(CDbl(dr("Capitalfinanciamiento").ToString.Trim), "#,##0.00")
                        End If
                        ' txtMontoFinanciado.Value = Format(CDbl(oRow.Item("MontoFinanciamiento")), "#,##0.00")
                        Dim dtFecha As Date = Now
                        Me.hddFechaActual.Value = dtFecha.ToString("dd/MM/yyyy")

                        hddtiopcambiosunat.Value = ObtenerTipoCambio(IIf(cmbMoneda.Value = GCCConstante.C_COD_MONEDA_SOLES, GCCConstante.C_COD_MONEDA_DOLARES, cmbMoneda.Value), _
                           GCCUtilitario.ToStringyyyyMMdd(Today.ToString), "SBS")

                        'Dim strFechaCarta As String = GCCUtilitario.CheckDateString(dtCotizacion.Rows(0).Item("FechaCarta").ToString, "C")
                        'Me.hddGeneraCarta.Value = intGenerarcarta
                        'If Not strFechaCarta Is Nothing Then
                        'Me.lblFechacarta.InnerHtml = strFechaCarta
                        'End If
                        'ToString StringToDateTime
                        'Datos del contrato
                        'txtNroContrato.Value = codSolicitudCredito
                        'hddFlagRetorno.Value = oRow.Item("FlagRetorno").ToString().Trim()
                        'dr = Nothing
                        Exit For
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            'oLwsDocClienteNtx = Nothing
            oLwsMantenimiento = Nothing
        End Try

    End Sub

    Private Shared Function ObtenerTipoCambio(ByVal strMonedaBusq As String, _
                                           ByVal strFecha As String, _
                                           ByVal strTipoModalidaCambio As String) As String
        Dim oLwsTipoCambioNtx As New LUtilNTX
        Dim odtbDatos As New DataTable
        Dim strResult As String = ""
        Try
            odtbDatos = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsTipoCambioNtx.ObtenerTipoCambio(strMonedaBusq, strTipoModalidaCambio, strFecha))
            If odtbDatos.Rows.Count = 0 Then
                strResult = String.Concat("0$0")
            Else
                strResult = String.Concat(odtbDatos.Rows(0).Item("MontoValorVenta").ToString, "$", odtbDatos.Rows(0).Item("MontoValorCompra").ToString)
            End If
            odtbDatos = Nothing

            Return strResult
        Catch ex As Exception
            Return "0"
        Finally
            oLwsTipoCambioNtx = Nothing
        End Try
    End Function

    Public Sub CargarComboTasador(ByVal pstrTablaGenerica As String)
        Dim objLMantenimientolNTx As New LMantenimientoNTX
        Dim sbResultado As String = "0:[-Seleccione-];"


        Dim dtDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLMantenimientolNTx.ListarTablaGenerica(pstrTablaGenerica))

        For Each oRow As DataRow In dtDatos.Rows
            Dim value As String = ""
            Dim text As String = ""
            If Not oRow.Item("CODIGO") Is DBNull.Value Then
                value = Trim(oRow.Item("CODIGO").ToString())
            End If
            If Not oRow.Item("DESCRIPCION") Is DBNull.Value Then
                text = Trim(oRow.Item("DESCRIPCION").ToString())
            End If

            sbResultado = sbResultado + value + ":" + text + ";"
            hddComboTasador.Value = "" + Mid(sbResultado, 1, sbResultado.Length - 1) + ""
        Next oRow

    End Sub

#End Region

#Region "WebMethos"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pCodSolicitudcredito"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
       Public Shared Function ListaContratoBienTasacion(ByVal pPageSize As Integer, _
                                                          ByVal pCurrentPage As Integer, _
                                                          ByVal pSortColumn As String, _
                                                          ByVal pSortOrder As String, _
                                                          ByVal pCodSolicitudcredito As String) As JQGridJsonResponse
        Dim objTasadorNTx As New LTasadorNTx

        Dim dtCondicionAdicional As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objTasadorNTx.ListaBienTasacion(pPageSize, _
                                                                                                                              pCurrentPage, _
                                                                                                                              pSortColumn, _
                                                                                                                              pSortOrder, _
                                                                                                                              pCodSolicitudcredito))
        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer

        Try
            If dtCondicionAdicional.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtCondicionAdicional.Rows(0)("RecordCount"))
                'total2 = Convert.ToInt32(dtCondicionAdicional.Rows(0)("total2"))
                intTotalCurrent = Convert.ToInt32(dtCondicionAdicional.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim JQGridJsonResponse As New JQGridJsonResponse
            Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
            Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtCondicionAdicional)


        Catch ex As Exception
            Throw ex
        Finally
            'oLwsDocClienteNtx = Nothing
            'dtCondicionAdicional = Nothing
        End Try
        'Dim total2 As Decimal

    End Function

    ''' <summary>
    ''' calculatotales
    ''' </summary>
    ''' <param name="pCodSolicitudcredito"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
   Public Shared Function calculatotales(ByVal pCodSolicitudcredito As String) As String
        Dim objTasadorNTx As New LTasadorNTx
        Try
            Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objTasadorNTx.calculatotales(pCodSolicitudcredito))
            Dim strcadtot As String
            If odtbDatos IsNot Nothing Then
                If odtbDatos.Rows.Count > 0 Then
                    For Each dr As DataRow In odtbDatos.Rows
                        strcadtot = dr("valorEjecucion").ToString() + "|" + dr("valorComercial").ToString
                    Next
                End If
            End If
            ' Número de página actual.
            'Dim currentPage As Integer = pCurrentPage
            'Dim intTotalCurrent As Int32
            '' Total de registros a mostrar.
            'Dim totalRecords As Integer

            Return strcadtot




            '    If dtCondicionAdicional.Rows.Count = 0 Then
            '        totalRecords = 0
            '        intTotalCurrent = 1
            '    Else
            '        totalRecords = Convert.ToInt32(dtCondicionAdicional.Rows(0)("RecordCount"))
            '        'total2 = Convert.ToInt32(dtCondicionAdicional.Rows(0)("total2"))
            '        intTotalCurrent = Convert.ToInt32(dtCondicionAdicional.Rows(0)("TOTAL_PAGINA"))
            '    End If

            '    If currentPage > intTotalCurrent Then
            '        currentPage = intTotalCurrent
            '    End If

            ' Número total de páginas
            'Dim JQGridJsonResponse As New JQGridJsonResponse
            'Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
            'Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtCondicionAdicional)


        Catch ex As Exception
            Throw ex
        Finally
            'oLwsDocClienteNtx = Nothing
            'dtCondicionAdicional = Nothing
        End Try
        'Dim total2 As Decimal

    End Function
    ''' <summary>
    ''' enviarcarta
    ''' </summary>
    ''' <param name="pCodSolicitudcredito"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
        Public Shared Function EnviarCartaII(ByVal pstrContrato As String, ByVal pstrFechaActivacion As String) As String
        Dim objEGCC_Alertas As New EGCC_Alertas
        Dim objGCCBase As GCCBase = Nothing
        Dim bolResultado As Boolean = False

        'Try
        '    Dim objObservacionDocumentoInsTx As New LTasadorTx
        '    Dim oEGccSolicitudcreditoestructuratasacion As New ESolicitudcreditoestructuratasacion
        '    Dim strESolicitudcreditoestructuratasacion As String
        '    With oEGccSolicitudcreditoestructuratasacion

        '        .Codsolicitudcredito = pCodSolicitudcredito
        '        .Usuarioregistro = GCCSession.CodigoUsuario
        '        .CodEstadoTasacion = "2"  'Estado Tasacion bien enviado

        '    End With

        '    strESolicitudcreditoestructuratasacion = GCCUtilitario.SerializeObject(oEGccSolicitudcreditoestructuratasacion)
        '    Dim intResult As String = objObservacionDocumentoInsTx.EnviarCarta(strESolicitudcreditoestructuratasacion)
        '    If intResult = True Then
        '        Return "0"
        '    Else
        '        Return "1"
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try

        Try
            objEGCC_Alertas.NumContrato = pstrContrato
            objEGCC_Alertas.FechaActivacion = pstrFechaActivacion

            objGCCBase = New GCCBase()
            bolResultado = objGCCBase.EnviarMailAlertas("MailRegistroTasador", objEGCC_Alertas)
        Catch ex As Exception
            bolResultado = "0"
            Throw ex
        End Try
        Return bolResultado
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pCodSolicitudcredito"></param>
    ''' <param name="pCodTasacion"></param>
    ''' <param name="pvalorejecucion"></param>
    ''' <param name="pvalorComercial"></param>
    ''' <param name="pfechaencargo"></param>
    ''' <param name="pfechatasacion"></param>
    ''' <param name="pCodTasador"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ActualizaTasacion(ByVal pCodSolicitudcredito As String, _
                                             ByVal pCodTasacion As String, _
                                             ByVal pvalorejecucion As String, _
                                             ByVal pvalorComercial As String, _
                                             ByVal pfechaencargo As String, _
                                             ByVal pfechatasacion As String, _
                                             ByVal pCodTasador As String, _
                                             ByVal pSecfinanciamiento As Integer) As String

        Try
            Dim objObservacionDocumentoInsTx As New LTasadorTx
            Dim oEGccSolicitudcreditoestructuratasacion As New ESolicitudcreditoestructuratasacion
            Dim strESolicitudcreditoestructuratasacion As String
            With oEGccSolicitudcreditoestructuratasacion

                .Codsolicitudcredito = pCodSolicitudcredito
                .Codtasacion = pCodTasacion
                .Codtasador = pCodTasador
                .Usuarioregistro = GCCSession.CodigoUsuario
                .Valorcomercial = GCCUtilitario.StringToDecimal(pvalorComercial)
                .Valorejecucion = GCCUtilitario.StringToDecimal(pvalorejecucion)
                .VFechaencargo = GCCUtilitario.ToStringyyyyMMdd(pfechaencargo)
                .VFechatasacion = GCCUtilitario.ToStringyyyyMMdd(pfechatasacion)
                .Secfinanciamiento = pSecfinanciamiento
            End With

            strESolicitudcreditoestructuratasacion = GCCUtilitario.SerializeObject(oEGccSolicitudcreditoestructuratasacion)
            Dim strResult As String = objObservacionDocumentoInsTx.ActualizarTasacion(strESolicitudcreditoestructuratasacion)
            If GCCUtilitario.CheckInt(strResult) = 0 Then
                Return "0"
            Else
                Return strResult
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class

