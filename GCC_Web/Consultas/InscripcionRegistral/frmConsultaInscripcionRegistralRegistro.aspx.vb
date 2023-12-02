Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Consultas_InscripcionRegistral_frmConsultaInscripcionRegistralRegistro
    Inherits GCCBase
    Dim objLog As New GCCLog("frmConsultaInscripcionRegistralRegistro.aspx.vb")
#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 20/09/2012
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida(Sesión)
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                hidCodigoContrato.Value = Request.QueryString("codContrato")
                hidCodigoBien.Value = Request.QueryString("codbien")
                pInicializarControles()

            End If

        Catch ex As ApplicationException
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                If Not IsPostBack Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "scriptSalir", "parent.fMensajeSalida('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaSDA") & "')", True)
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fMensajeSalida('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaSDA") & "')", True)
                End If
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If

        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub
    Private Sub pInicializarControles()
        Dim oLwsDocBienNtx As New LBienNTx
        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura

        Try
            oESolicitudCreditoEstructura = GCCUtilitario.DeserializeObject(Of ESolicitudcreditoestructura)(oLwsDocBienNtx.ObtenerDatosBienContratoConsulta(hidCodigoContrato.Value))
            If oESolicitudCreditoEstructura IsNot Nothing Then
                With oESolicitudCreditoEstructura
                    txtNumeroContrato.InnerText = .Codsolicitudcredito
                    txtEstadoContrato.InnerText = .EstadoContrato
                    txtclasificacion.InnerText = .ClasificacionBien
                    txtTipoBien.InnerText = .TipoBien
                    txtMoneda.InnerText = .Moneda
                    txtEstadoContrato.InnerText = .EstadoContrato
                    txtEjecutivo.InnerText = .EjecutivoBanca
                    txtBanca.InnerText = .Banca
                    txtKardex.InnerText = .Kardex
                    txtObservacionContrato.Value = .ObservacionContrato
                    txtFechaFinObra.InnerText = GCCUtilitario.CheckDateString2(.FechaProbableFinObra.ToString.Trim, "C")
                    txtFechaRealFinObra.InnerText = GCCUtilitario.CheckDateString2(.FechaRealFinObra.ToString.Trim, "C")
                    txtFechaInscripcionMunicipal.InnerText = GCCUtilitario.CheckDateString2(.FechaInscripcionMunicipal.ToString.Trim, "C")
                    ddlEstadoInscripcionMunicipal.InnerText = .CodEstadoMunicipal.Trim()
                    txtFechaEnvioNotaria.InnerText = GCCUtilitario.CheckDateString2(.FechaEnvioNotaria.ToString.Trim, "C")
                    txtFechaPropiedad.InnerText = GCCUtilitario.CheckDateString2(.FechaPropiedad.ToString.Trim, "C")
                    txtFechaInscripcionRegistral.InnerText = GCCUtilitario.CheckDateString2(.FechaInscripcionRegistral.ToString.Trim, "C")
                    ddlEstadoInscripcionRRPP.InnerText = .CodEstadoInscripcionRrPp.Trim()
                    txtOficinaRegistral.InnerText = .OficinaRegistral
                    ddlNotaria.InnerText = .CodigoNotaria.Trim()
                    'ddlPropiedad.Value = .CodEstadoTransferencia.Trim()
                    txtCUCliente.InnerText = .CodUnico
                    txtRazonSocial.InnerText = .RazonSocial
                    txtTipoDocumento.InnerText = .TipoDocumento
                    txtNumeroDocumento.InnerText = .NumeroDocumento
                End With
            End If

        Catch ex As Exception
            Throw ex
        Finally
            oLwsDocBienNtx = Nothing
        End Try
    End Sub
    <WebMethod()> _
        Public Shared Function ListaDocumentosInscripcion(ByVal pPageSize As Integer, _
                                                          ByVal pCurrentPage As Integer, _
                                                          ByVal pSortColumn As String, _
                                                          ByVal pSortOrder As String, _
                                                          ByVal pCodigoContrato As String, _
                                                          ByVal pCodigoBien As Integer) As JQGridJsonResponse
        Dim objBienNTx As New LBienNTx

        Dim dtDocumentos As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListaBienInscripcionMunicipal(pPageSize, _
                                                                                                                   pCurrentPage, _
                                                                                                                   pSortColumn, _
                                                                                                                   pSortOrder, _
                                                                                                                   pCodigoContrato, _
                                                                                                                   pCodigoBien.ToString()))
        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        'Dim total2 As Decimal
        If dtDocumentos.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtDocumentos.Rows(0)("RecordCount"))
            'total2 = Convert.ToInt32(dtCondicionAdicional.Rows(0)("total2"))
            intTotalCurrent = Convert.ToInt32(dtDocumentos.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim JQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtDocumentos)

    End Function
#End Region
End Class
