


Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS


Partial Class GestionBien_Tasacion_FrmTasacionIndividual
    Inherits GCCBase
    Dim objLog As New GCCLog("FrmTasacionIndividual.aspx.vb")
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
                GCCUtilitario.CargarComboValorGenerico(Me.cmbEstadoContrato, GCCConstante.C_TABLAGENERICA_ESTADO_CONTRATO)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbClasificacionBien, GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbBanca, GCCConstante.C_TABLAGENERICA_BANCA_ATENCION)
                'GCCUtilitario.CargarComboValorGenerico(Me.cmbEjecutivoBanca, GCCConstante.C_TABLAGENERICA_EJECUTIVO)
                GCCUtilitario.CargarComboMoneda(Me.cmbMoneda)
                hddCodigoContratos.Value = Request.QueryString("sid")
                'stasador
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
        Dim oLTasadorNTx As New LTasadorNTx
        Dim oLwsMantenimiento As New LMantenimientoNTX
        Try
            Dim odtbDatos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLTasadorNTx.ObtenerContratoTasacion(codSolicitudCredito))
            If odtbDatos IsNot Nothing Then
                If odtbDatos.Rows.Count > 0 Then
                    For Each dr As DataRow In odtbDatos.Rows
                        txtContrato.Value = codSolicitudCredito
                        txtcucliente.Value = dr("CODUNICO").ToString
                        txtrasonSocial.Value = dr("NOMBRECLIENTE").ToString
                        'hddUbigeoUbicacion.Value = RTrim(oRow("UBIGEO").ToString)

                        cmbEstadoContrato.Value = dr("CODIGOESTADOCONTRATO").ToString.Trim


                        cmbClasificacionBien.Value = dr("CODIGOCLASIFICACIONBIEN").ToString.Trim
                        'GCCUtilitario.ToStringyyyyMMdd(Today.ToString)

                        If dr("FECHAACTIVACION").ToString.Trim <> "" Then
                            txtFechaActivacion.Value = Convert.ToDateTime(dr("FECHAACTIVACION").ToString.Trim).ToString("dd/MM/yyyy")
                            'GCCUtilitario.CheckDateString(dr("FECHAACTIVACION").ToString, "C")
                        End If
                        ' Day(dr("FECHAACTIVACION").ToString) + "/" + Month(dr("FECHAACTIVACION").ToString) + "/" + Year(dr("FECHAACTIVACION").ToString)



                        'Dim strFechaCarta As String = GCCUtilitario.CheckDateString(dtCotizacion.Rows(0).Item("FechaCarta").ToString, "C")
                        'Me.hddGeneraCarta.Value = intGenerarcarta
                        'If Not strFechaCarta Is Nothing Then
                        ' Me.lblFechacarta.InnerHtml = strFechaCarta
                        ' End If

                        '.ToString StringToDateTime
                        cmbBanca.Value = dr("CODBANCA").ToString.Trim

                        cmbMoneda.Value = dr("CODMONEDA").ToString.Trim
                        txtdesejecutivobanca.Value = dr("DesEjecutivoBanca").ToString.Trim

                        ' Datos del contrato
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

        Dim dtCondicionAdicional As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objTasadorNTx.ListaContratoBienTasacion(pPageSize, _
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
    ''' 
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pCodSolicitudcredito"></param>
    ''' <param name="pCodContratoTasacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
       Public Shared Function ListaHistoricoContratoBienTasacion(ByVal pPageSize As Integer, _
                                                                  ByVal pCurrentPage As Integer, _
                                                                  ByVal pSortColumn As String, _
                                                                  ByVal pSortOrder As String, _
                                                                  ByVal pCodSolicitudcredito As String, _
                                                                  ByVal pCodContratoTasacion As String) As JQGridJsonResponse
        Dim objTasadorNTx As New LTasadorNTx

        Dim dtCondicionAdicional As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objTasadorNTx.ListaHistoricoContratoBienTasacion(pPageSize, _
                                                                                                                                              pCurrentPage, _
                                                                                                                                              pSortColumn, _
                                                                                                                                              pSortOrder, _
                                                                                                                                              pCodSolicitudcredito, _
                                                                                                                                              GCCUtilitario.CheckInt(pCodContratoTasacion)))
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
    ''' enviarcarta
    ''' </summary>
    ''' <param name="pCodSolicitudcredito"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
        Public Shared Function enviarcarta(ByVal pCodSolicitudcredito As String) As String

        Try
            Dim objObservacionDocumentoInsTx As New LTasadorTx
            Dim oEGccSolicitudcreditoestructuratasacion As New ESolicitudcreditoestructuratasacion
            Dim strESolicitudcreditoestructuratasacion As String
            With oEGccSolicitudcreditoestructuratasacion

                .Codsolicitudcredito = pCodSolicitudcredito
                .Usuarioregistro = GCCSession.CodigoUsuario
                .CodEstadoTasacion = "2"  'Estado Tasacion bien enviado

            End With

            strESolicitudcreditoestructuratasacion = GCCUtilitario.SerializeObject(oEGccSolicitudcreditoestructuratasacion)
            Dim intResult As String = objObservacionDocumentoInsTx.EnviarCarta(strESolicitudcreditoestructuratasacion)
            If intResult = True Then
                'Enviar Alerta
                Dim blnRpta As Boolean = psEnviarAlerta(oEGccSolicitudcreditoestructuratasacion)
                Return "0"
            Else
                Return "1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Shared Function psEnviarAlerta(ByVal obj As ESolicitudcreditoestructuratasacion) As Boolean
        Dim objEGCC_Alertas As New EGCC_Alertas
        Dim objLAlertaImpVehicularNTx As New LImpuestoVehicularNTX
        Dim strNroLote As String = obj.Codsolicitudcredito.ToString()
        Dim blnRpta As Boolean = False
        Try
            Dim dtAlertaImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLAlertaImpVehicularNTx.ListarAltertaImpuestoVehicular(strNroLote, "AsignarTasador"))

            Dim objGCCBase As GCCBase = Nothing
            Dim bolResultado As Boolean = False

            For Each dr As DataRow In dtAlertaImpuestoVehicular.Rows
                With objEGCC_Alertas
                    .AudUsuarioRegistro = GCCSession.CodigoUsuario.ToString()
                    .CodTasador = dr.Item("CodTasador").ToString()

                    .FechaActual = Now.Date.Day.ToString() + " de " + MonthName(Now.Date.Month).ToString() + " del " + Now.Date.Year.ToString()
                    .RazonSocial = dr.Item("ClienteRazonSocial").ToString()
                    .Direccion = dr.Item("ClienteDomicilioLegal").ToString()
                    .Distrito = dr.Item("Distrito").ToString()
                    .NumContrato = dr.Item("CodSolicitudCredito").ToString()
                    .FechaActivacion = dr.Item("fecharegistro").ToString()
                    '------ Por Confirmar ---------
                    .EmpresaTasador = dr.Item("EmpresaTasador").ToString()
                    .Tasador = dr.Item("Tasador").ToString()
                    .Telefono = dr.Item("Telefono").ToString()
                    .Celular = dr.Item("CelularTasador").ToString()
                    .Correo = dr.Item("CorreoTasador").ToString()
                    .RutaWeb = HttpContext.Current.Server.MapPath("../..").ToString()
                End With
                objGCCBase = New GCCBase()
                bolResultado = objGCCBase.EnviarMailAlertas("MailAsignacionTasador", objEGCC_Alertas)
            Next
            blnRpta = True
        Catch ex As Exception

        End Try
        Return blnRpta
    End Function

#End Region

End Class
