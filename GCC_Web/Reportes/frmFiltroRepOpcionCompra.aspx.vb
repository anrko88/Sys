Imports System.Diagnostics
Imports System.IO
Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Text
Partial Class Reportes_frmFiltroRepOpcionCompra
    Inherits GCCBase

    Dim objLog As New GCCLog("frmFiltroRepOpcionCompra.aspx.vb")
#Region "   Eventos     "


    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim strFechaIni As String = String.Empty
        Dim strFechaFin As String = String.Empty
        Dim table As DataTable = Nothing

        Dim objOpcionCompra As LOpcionCompraNTx = Nothing

        Try
            objOpcionCompra = New LOpcionCompraNTx()

            strFechaIni = txtFechaActIni.Value
            strFechaFin = txtFechaActFin.Value

            table = GCCUtilitario.DeserializeObject(Of DataTable)(objOpcionCompra.ListadoReporteOpcionCompra(strFechaIni, strFechaFin))
            Me.pDescargarArchivo(table)
        Catch ex As Exception
            'Throw ex
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGenerar_Click")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        Finally
            If objOpcionCompra IsNot Nothing Then objOpcionCompra = Nothing
            If table IsNot Nothing Then
                table.Dispose()
                table = Nothing
            End If
            strFechaIni = String.Empty
            strFechaFin = String.Empty
        End Try
    End Sub

#End Region

#Region "   Metodos     "


    Private Sub pDescargarArchivo(ByVal ptabla As DataTable)
        Dim sb As New StringBuilder
        Dim sw As StringWriter

        Dim pTitulo As String = "OPCION DE COMPRA"

        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList
        Dim ColumnModo As New ArrayList

        '**** Etiquetas de la cabecera ****
        OrderHeaderTableColumnsLabels.Add("Fecha Opción de Compra Desde")
        OrderHeaderTableColumnsLabels.Add("Hasta")

        '**** Texto de cada etiqueta ****
        OrderHeaderTableColumnsText.Add(txtFechaActIni.Value)
        OrderHeaderTableColumnsText.Add(txtFechaActFin.Value)

        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("Nº")
        OrderDataTableColumnsTitles.Add("N° Crédito")
        OrderDataTableColumnsTitles.Add("Cliente")
        OrderDataTableColumnsTitles.Add("Tipo del Bien")
        OrderDataTableColumnsTitles.Add("N° Placa")
        OrderDataTableColumnsTitles.Add("Vencimiento")
        OrderDataTableColumnsTitles.Add("Pago Opción de Compra")
        OrderDataTableColumnsTitles.Add("Descargo Municipal")
        OrderDataTableColumnsTitles.Add("Transferencia")
        OrderDataTableColumnsTitles.Add("Tipo de Opc.Compra")
        OrderDataTableColumnsTitles.Add("Estado del Crédito")
        OrderDataTableColumnsTitles.Add("Plaza")
        OrderDataTableColumnsTitles.Add("Observaciones")
        OrderDataTableColumnsTitles.Add("Ejecutivo Leasing")
        OrderDataTableColumnsTitles.Add("Ejecutivo")
        OrderDataTableColumnsTitles.Add("Estado")

        '**** Nombres de las Columnas del DataTable ****
        OrderDataTableColumnsName.Add("Contador")
        OrderDataTableColumnsName.Add("CodSolicitudCredito")
        OrderDataTableColumnsName.Add("RazonSocial|3")
        OrderDataTableColumnsName.Add("ClasificacionBien")
        OrderDataTableColumnsName.Add("Placa")
        OrderDataTableColumnsName.Add("FechaVencimiento")
        OrderDataTableColumnsName.Add("FechaOC")
        OrderDataTableColumnsName.Add("FechaDescargoMunicipal")
        OrderDataTableColumnsName.Add("FechaTranferencia")
        OrderDataTableColumnsName.Add("TipoOC")
        OrderDataTableColumnsName.Add("EstadoContrato")
        OrderDataTableColumnsName.Add("Plaza")
        OrderDataTableColumnsName.Add("Observacion|3")
        OrderDataTableColumnsName.Add("NombreEjecutivoleasing|3")
        OrderDataTableColumnsName.Add("DesEjecutivoBanca|3")
        OrderDataTableColumnsName.Add("EstadoBienOC||ColorEstadoBienOC")        

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
        sb.Append(GCCUtilitario.fHTMLGeneraTablaTitulo(pTitulo, Me, ptabla.Columns.Count))
        sb.Append(" <br>")
        sb.Append(vbCrLf)
        sb.Append(GCCUtilitario.fHTMLGeneraTablaCabecera(OrderHeaderTableColumnsLabels, OrderHeaderTableColumnsText, ptabla.Columns.Count))
        sb.Append(" <br>")
        sb.Append(vbCrLf)
        sb.Append(GCCUtilitario.fHTMLGeneraTablaEstilo(ptabla, OrderDataTableColumnsTitles, OrderDataTableColumnsName))
        sb.Append("</body>")
        sb.Append(vbCrLf)
        sb.Append("</html>")
        sb.Append(vbCrLf)

        sw = New StringWriter(sb)

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "OpcionCompra")
    End Sub

#End Region
End Class
