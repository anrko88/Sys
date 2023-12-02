Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports System.IO
Partial Class Consultas_frmConsultaListaContrato
    Inherits GCCBase
    Dim objLog As New GCCLog("frmConsultaListaContrato.aspx.vb")

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
                GCCUtilitario.CargarComboValorGenerico(ddlClasificacionbien, GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoContrato, GCCConstante.C_TABLAGENERICA_ESTADO_CONTRATO)
                GCCUtilitario.CargarComboValorGenerico(ddlTipoDocumento, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
                hidEstadoVerificacion.Value = Request.QueryString("codest")
                hidMensajeVerificacion.Value = Request.QueryString("mensaje")
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
    Public Shared Function BuscarContrato(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pNumeroContraro As String, _
                                           ByVal pCUCliente As String, _
                                           ByVal pRazonSocial As String, _
                                           ByVal pCodClasificacionBien As String, _
                                           ByVal pCodTipoBien As String, _
                                           ByVal pEstadoContrato As String, _
                                           ByVal pTipoDocumento As String, _
                                           ByVal pNumeroDocumento As String, _
                                           ByVal pKardex As String) As JQGridJsonResponse

        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura
        Dim objBienNTx As New LBienNTx
        With oESolicitudCreditoEstructura
            .NumeroContrato = pNumeroContraro
            .CodUnico = pCUCliente
            .RazonSocial = pRazonSocial
            .Tiporubrofinanciamiento = pCodClasificacionBien
            .Codigotipobien = pCodTipoBien
            .EstadoContrato = pEstadoContrato
            .TipoDocumento = pTipoDocumento
            .NumeroDocumento = pNumeroDocumento
            .Kardex = pKardex
        End With


        Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListadoContratoBien(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtContrato.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtContrato.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtContrato.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtContrato)

    End Function

#End Region

    Private Sub pDescargarArchivo()
        Dim sb As New StringBuilder
        Dim sw As StringWriter
        Dim objBienNTx As New LBienNTx
        Dim pTitulo As String = "DETALLE DEL BIEN"
        Dim oESolicitudCreditoEstructura As New ESolicitudcreditoestructura

        With oESolicitudCreditoEstructura
            .NumeroContrato = txtNumeroDocumento.Value.Trim
            .CodUnico = txtCUCliente.Value.Trim
            .RazonSocial = txtRazonSocial.Value.Trim
            .Tiporubrofinanciamiento = ddlClasificacionbien.Value.Trim
            .Codigotipobien = ddlTipobien.Value.Trim
            .EstadoContrato = ddlEstadoContrato.Value.Trim
            .TipoDocumento = ddlTipoDocumento.Value.Trim
            .NumeroDocumento = txtNumeroDocumento.Value.Trim
            .Kardex = txtKardex.Value.Trim
        End With


        Dim dtReporte As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListarDetalleBienConsulta(GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructura)(oESolicitudCreditoEstructura)))

        'Dim dtReporte As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objBienNTx.ListarDetalleBienConsulta())
        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList

        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("Nº Contrato")
        OrderDataTableColumnsTitles.Add("CU Cliente")
        OrderDataTableColumnsTitles.Add("Razón Social o Nombre")
        OrderDataTableColumnsTitles.Add("Tipo de Documento")
        OrderDataTableColumnsTitles.Add("Nº de Documento")
        OrderDataTableColumnsTitles.Add("Clasificación del Bien")
        OrderDataTableColumnsTitles.Add("Tipo de Bien")
        OrderDataTableColumnsTitles.Add("Estado del Contrato")
        'OrderDataTableColumnsTitles.Add("Opción de Compra")
        OrderDataTableColumnsTitles.Add("Departamento")
        OrderDataTableColumnsTitles.Add("Provincia")
        OrderDataTableColumnsTitles.Add("Distrito")
        OrderDataTableColumnsTitles.Add("Ubicación")
        OrderDataTableColumnsTitles.Add("Nº de Serie")
        OrderDataTableColumnsTitles.Add("Motor")
        OrderDataTableColumnsTitles.Add("Marca")
        OrderDataTableColumnsTitles.Add("Modelo")
        OrderDataTableColumnsTitles.Add("Placa Actual")
        OrderDataTableColumnsTitles.Add("Año")
        OrderDataTableColumnsTitles.Add("Descripción")
        OrderDataTableColumnsTitles.Add("Código del Predio")
        OrderDataTableColumnsTitles.Add("Fecha Inscripción Municipal")
        OrderDataTableColumnsTitles.Add("Estado Inscripción Municipal")
        OrderDataTableColumnsTitles.Add("Fecha Inscripción Registral")
        OrderDataTableColumnsTitles.Add("Estado Inscripción Registral")
        OrderDataTableColumnsTitles.Add("Propiedad")
        OrderDataTableColumnsTitles.Add("Estado del Registro del Bien")
        OrderDataTableColumnsTitles.Add("Fecha de Transferencia")
        OrderDataTableColumnsTitles.Add("Fecha de Baja")

        '**** Nombres de las Columnas del DataTable ****
        OrderDataTableColumnsName.Add("CodSolicitudCredito")
        OrderDataTableColumnsName.Add("CodUnico|1")
        OrderDataTableColumnsName.Add("NombreCliente|3")
        OrderDataTableColumnsName.Add("TipoDocumento")
        OrderDataTableColumnsName.Add("NumeroDocumento")
        OrderDataTableColumnsName.Add("ClasificacionBien")
        OrderDataTableColumnsName.Add("TipoBien|1")
        OrderDataTableColumnsName.Add("EstadoContrato")
        'OrderDataTableColumnsName.Add("")
        OrderDataTableColumnsName.Add("DepartamentoNombre")
        OrderDataTableColumnsName.Add("ProvinciaNombre")
        OrderDataTableColumnsName.Add("DistritoNombre")
        OrderDataTableColumnsName.Add("Ubicacion")
        OrderDataTableColumnsName.Add("NroSerie")
        OrderDataTableColumnsName.Add("NroMotor")
        OrderDataTableColumnsName.Add("Marca")
        OrderDataTableColumnsName.Add("Modelo")
        OrderDataTableColumnsName.Add("Placa")
        OrderDataTableColumnsName.Add("Anio")
        OrderDataTableColumnsName.Add("Comentario")
        OrderDataTableColumnsName.Add("CodigoPredio")
        OrderDataTableColumnsName.Add("FechaInscripcionMunicipal")
        OrderDataTableColumnsName.Add("CodEstadoMunicipal")
        OrderDataTableColumnsName.Add("FechaInscripcionRegistral")
        OrderDataTableColumnsName.Add("CodEstadoInscripcionRRPP")
        OrderDataTableColumnsName.Add("Propiedad")
        OrderDataTableColumnsName.Add("CodEstadoBien|2")
        OrderDataTableColumnsName.Add("FechaTransferencia")
        OrderDataTableColumnsName.Add("FechaBaja")

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

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "DetalleBien")
    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        pDescargarArchivo()
    End Sub
End Class
