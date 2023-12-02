Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data
Imports System.IO
Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Consultas_InscripcionRegistral_frmConsultaInscripcionRegistral
    Inherits GCCBase
    Dim objLog As New GCCLog("frmConsultaInscripcionRegistral.aspx.vb")

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
                'hidSecFinanciamiento.Value = Request.QueryString("csf")
                'hidNumeroContrato.Value = Request.QueryString("csc")
                GCCUtilitario.CargarComboValorGenerico(cmbEstadoInscripcionMunicipal, GCCConstante.C_TABLAGENERICA_Estado_Municipal)
                GCCUtilitario.CargarComboValorGenerico(ddlTipoDocumento, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
                GCCUtilitario.CargarComboValorGenerico(cmbClasificacionBien, GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)
                'GCCUtilitario.CargarComboValorGenerico(cmbTipoBien, GCCConstante.C_TABLAGENERICA_TIPO_BIEN)
                GCCUtilitario.CargarComboValorGenerico(cmbEstadoContrato, GCCConstante.C_TABLAGENERICA_ESTADO_CONTRATO)
                GCCUtilitario.CargarComboValorGenerico(cmbNotaria, GCCConstante.C_TABLAGENERICA_NOTARIA_PUBLICA)
                GCCUtilitario.CargarComboValorGenerico(cmbEstadoInscripcionRegistral, GCCConstante.C_TABLAGENERICA_Estado_Inscripción_RRPP)
                'GCCUtilitario.CargarComboValorGenerico(ddlEstadoInscripcionMunicipal, GCCConstante.C_TABLAGENERICA_Estado_Municipal)
                ''GCCUtilitario.CargarComboValorGenerico(ddlPropiedad, GCCConstante.C_TABLAGENERICA_Estado_Transferencia)
                'pInicializarControles()

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

#End Region

#Region "Métodos"
    <WebMethod()> _
    Public Shared Function ListaBienContratoInscripcionResgistral(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pCodContrato As String, _
                                             ByVal pCuCliente As String, _
                                             ByVal pRazonSocial As String, _
                                             ByVal pCodTipoDocumento As String, _
                                             ByVal pCodNroDocumento As String, _
                                             ByVal pCodClasificacionBien As String, _
                                             ByVal pCodTipoBien As String, _
                                             ByVal pCodEstaContrato As String, _
                                             ByVal pKardex As String, _
                                             ByVal pFechaProbableFinObra As String, _
                                             ByVal pFechaRealFinObra As String, _
                                             ByVal pCodEstadoInscMunicipal As String, _
                                             ByVal pFechaInsMunicipal As String, _
                                             ByVal pCodNoptaria As String, _
                                             ByVal pFechaEnvioNotaria As String, _
                                             ByVal pCodEstadoInscRegistral As String, _
                                             ByVal pFechaInscrRegistral As String, _
                                             ByVal pOficinaRegistral As String, _
                                             ByVal pFechaPropiedad As String) As JQGridJsonResponse

        Dim objBienNTx As New LBienNTx
        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura

        With oESolicitudCreditoEstructura
            .NumeroContrato = pCodContrato
            .CodUnico = pCuCliente
            .RazonSocial = pRazonSocial
            .TipoDocumento = pCodTipoDocumento
            .NumeroDocumento = pCodNroDocumento
            .Tiporubrofinanciamiento = pCodClasificacionBien
            .Codigotipobien = pCodTipoBien
            .EstadoContrato = pCodEstaContrato
            .Kardex = pKardex
            .FechaProbableFinObra = GCCUtilitario.StringToDateTime(pFechaProbableFinObra)
            .FechaRealFinObra = GCCUtilitario.StringToDateTime(pFechaRealFinObra)
            .CodEstadoMunicipal = pCodEstadoInscMunicipal
            .FechaInscripcionMunicipal = GCCUtilitario.StringToDateTime(pFechaInsMunicipal)
            .CodigoNotaria = pCodNoptaria
            .FechaEnvioNotaria = GCCUtilitario.StringToDateTime(pFechaEnvioNotaria)
            .CodEstadoInscripcionRrPp = pCodEstadoInscRegistral
            .FechaInscripcionRegistral = GCCUtilitario.StringToDateTime(pFechaInscrRegistral)
            .OficinaRegistral = pOficinaRegistral
            .FechaPropiedad = GCCUtilitario.StringToDateTime(pFechaPropiedad)
        End With


        Dim dtBienesContratoInmuebles As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListaBienContratoInscripcionResgistral(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtBienesContratoInmuebles.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtBienesContratoInmuebles.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtBienesContratoInmuebles.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtBienesContratoInmuebles)

    End Function
#End Region
    Private Sub pDescargarArchivo()
        Dim sb As New StringBuilder
        Dim sw As StringWriter
        Dim objBienNTx As New LBienNTx
        Dim pTitulo As String = "INSCRIPCIÓN REGISTRAL"
        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura

        With oESolicitudCreditoEstructura
            .NumeroContrato = txtNroContrato.Value
            .CodUnico = txtcuCliente.Value
            .RazonSocial = txtRazonSocialNombre.Value
            .TipoDocumento = ddlTipoDocumento.Value
            .NumeroDocumento = txtNroDocumento.Value
            .Tiporubrofinanciamiento = cmbClasificacionBien.Value
            .Codigotipobien = cmbTipoBien.Value
            .EstadoContrato = cmbEstadoContrato.Value
            .Kardex = txtKardex.Value
            .FechaProbableFinObra = GCCUtilitario.StringToDateTime(txtFechaprobablefinObra.Value)
            .FechaRealFinObra = GCCUtilitario.StringToDateTime(txtFecharealfinObra.Value)
            .CodEstadoMunicipal = cmbEstadoInscripcionMunicipal.Value
            .FechaInscripcionMunicipal = GCCUtilitario.StringToDateTime(txtFechaIbscripcionMunicipal.Value)
            .CodigoNotaria = cmbNotaria.Value
            .FechaEnvioNotaria = GCCUtilitario.StringToDateTime(txtFechaEnvioNotaria.Value)
            .CodEstadoInscripcionRrPp = cmbEstadoInscripcionRegistral.Value
            .FechaInscripcionRegistral = GCCUtilitario.StringToDateTime(txtFechaInscripcionRegistral.Value)
            .OficinaRegistral = txtOficinaRegistral.Value
            .FechaPropiedad = GCCUtilitario.StringToDateTime(txtFechaPropiedad.Value)
        End With


        Dim dtReporte As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListaBienContratoInscripcionResgistralReporte(GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)))

        'Dim dtReporte As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListarDetalleBienConsulta())
        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList

        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("Nº Contrato")
        OrderDataTableColumnsTitles.Add("Clasificación del Bien")
        OrderDataTableColumnsTitles.Add("Tipo del Bien")
        OrderDataTableColumnsTitles.Add("Moneda")
        OrderDataTableColumnsTitles.Add("Estado del Contrato")
        OrderDataTableColumnsTitles.Add("CU Cliente")
        OrderDataTableColumnsTitles.Add("Razón Social o Nombre")
        OrderDataTableColumnsTitles.Add("Tipo Documento")
        'OrderDataTableColumnsTitles.Add("Opción de Compra")
        OrderDataTableColumnsTitles.Add("Nº Documento")
        OrderDataTableColumnsTitles.Add("Fecha Probable de Fin de Obra")
        OrderDataTableColumnsTitles.Add("Fecha Real de Fin de Obra")
        OrderDataTableColumnsTitles.Add("Fecha Inscripción Municipal")
        OrderDataTableColumnsTitles.Add("Estado Inscripción Municipal")
        OrderDataTableColumnsTitles.Add("Notaría")
        OrderDataTableColumnsTitles.Add("Fecha de Envío a Notaría")
        OrderDataTableColumnsTitles.Add("Fecha Inscripción Registral")
        OrderDataTableColumnsTitles.Add("Estado Inscripción Registral")
        OrderDataTableColumnsTitles.Add("Oficina Registral")
        OrderDataTableColumnsTitles.Add("Fecha de Propiedad")
        

        '**** Nombres de las Columnas del DataTable ****
        OrderDataTableColumnsName.Add("CodSolicitudCredito")
        OrderDataTableColumnsName.Add("ClasificacionBien")
        OrderDataTableColumnsName.Add("TipoBien")
        OrderDataTableColumnsName.Add("Moneda")
        OrderDataTableColumnsName.Add("EstadoContrato")
        OrderDataTableColumnsName.Add("CodUnico|1")
        OrderDataTableColumnsName.Add("RazonSocial|3")
        OrderDataTableColumnsName.Add("NombreTipoDocumento")
        'OrderDataTableColumnsName.Add("")
        OrderDataTableColumnsName.Add("NumeroDocumento")
        OrderDataTableColumnsName.Add("FechaProbableFinObra2")
        OrderDataTableColumnsName.Add("FechaRealFinObra2")
        OrderDataTableColumnsName.Add("FechaInscripcionMunicipal2")
        OrderDataTableColumnsName.Add("EstadoMunicipal")
        OrderDataTableColumnsName.Add("Notaria")
        OrderDataTableColumnsName.Add("FechaEnvioNotaria2")
        OrderDataTableColumnsName.Add("FechaInscripcionRegistral2")
        OrderDataTableColumnsName.Add("EstadoInscripcionRRPP")
        OrderDataTableColumnsName.Add("OficinaRegistral")
        OrderDataTableColumnsName.Add("FechaTransferencia2")
        

        'sb.Append("<!DOCTYPE html PUBLIC " & "-//W3C//DTD XHTML 1.0 Transitional//EN")
        sb.Append("<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'>")
        sb.Append(vbCrLf)
        sb.Append("<html>")
        sb.Append(vbCrLf)
        sb.Append("<head>")
        sb.Append(vbCrLf)
        sb.Append("<title>")
        sb.Append(pTitulo)
        sb.Append("</title>")
        sb.Append(vbCrLf)
        sb.Append("<meta http-equiv='Content-Type' content='text/html' charset='utf-8'>")
        sb.Append(vbCrLf)
        'Dim strStyle As String = Request.PhysicalApplicationPath + "Util/css/css_excel.css" '"http://" & Request.Url.Host & ResolveUrl("~/Estilos/estilos.css")
        'sb.Append("<link rel='stylesheet' type='text/css' href='" & strStyle & "'>")
        sb.Append(GCCUtilitario.fHTMLEstilo())
        sb.Append(vbCrLf)
        sb.Append("</head>")
        sb.Append(vbCrLf)
        sb.Append(" <body>")
        sb.Append(vbCrLf)
        sb.Append(GCCUtilitario.fHTMLGeneraTablaTitulo(pTitulo, Me, OrderDataTableColumnsName.Count))
        sb.Append(" <br>")
        sb.Append(vbCrLf)
        sb.Append(GCCUtilitario.fHTMLGeneraTablaCabecera(OrderHeaderTableColumnsLabels, OrderHeaderTableColumnsText, OrderDataTableColumnsName.Count))
        sb.Append(" <br>")
        sb.Append(vbCrLf)
        sb.Append(GCCUtilitario.fHTMLGeneraTablaEstilo(dtReporte, OrderDataTableColumnsTitles, OrderDataTableColumnsName))
        sb.Append("</body>")
        sb.Append(vbCrLf)
        sb.Append("</html>")
        sb.Append(vbCrLf)

        sw = New StringWriter(sb)

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "Inscripcion Registral")
    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        pDescargarArchivo()
    End Sub
End Class