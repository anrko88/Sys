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
Partial Class Reportes_frmFiltroRepDetalleBien
    Inherits GCCBase

    Dim objLog As New GCCLog("frmFiltroRepDetalleBien.aspx.vb")
#Region "   Eventos     "


    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim strFechaIni As String = String.Empty
        Dim strFechaFin As String = String.Empty
        Dim table As DataTable = Nothing

        Dim objContratoNTx As LContratoNTx = Nothing

        Try
            objContratoNTx = New LContratoNTx()

            strFechaIni = txtFechaActIni.Value
            strFechaFin = txtFechaActFin.Value

            table = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoReporteDetalleBien(strFechaIni, strFechaFin))
            Me.pDescargarArchivo(table)
        Catch ex As Exception
            'Throw ex
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGenerar_Click")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        Finally
            If objContratoNTx IsNot Nothing Then objContratoNTx = Nothing
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

        Dim pTitulo As String = "DETALLE DEL BIEN"

        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList
        Dim ColumnModo As New ArrayList

        '**** Etiquetas de la cabecera ****
        OrderHeaderTableColumnsLabels.Add("Fecha de Activación Contrato Desde")
        OrderHeaderTableColumnsLabels.Add("Hasta")

        '**** Texto de cada etiqueta ****
        OrderHeaderTableColumnsText.Add(txtFechaActIni.Value)
        OrderHeaderTableColumnsText.Add(txtFechaActFin.Value)

        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("Nº")
        OrderDataTableColumnsTitles.Add("N° Crédito")
        OrderDataTableColumnsTitles.Add("Descripción Bien")
        OrderDataTableColumnsTitles.Add("RUC Proveedor")
        OrderDataTableColumnsTitles.Add("Moneda Crédito")
        OrderDataTableColumnsTitles.Add("Apellido Paterno")
        OrderDataTableColumnsTitles.Add("Apellido Materno")
        OrderDataTableColumnsTitles.Add("Nombres")
        OrderDataTableColumnsTitles.Add("Razón Social")
        OrderDataTableColumnsTitles.Add("N°")
        OrderDataTableColumnsTitles.Add("Unidad de Medida")
        OrderDataTableColumnsTitles.Add("Descripción del Bien")
        OrderDataTableColumnsTitles.Add("Moneda Factura")
        OrderDataTableColumnsTitles.Add("Clasificación del Bien")
        OrderDataTableColumnsTitles.Add("Suma de Capital")
        OrderDataTableColumnsTitles.Add("TC")
        OrderDataTableColumnsTitles.Add("Facturas Importe Moneda Crédito")
        OrderDataTableColumnsTitles.Add("Suma de Total De Bienes")
        OrderDataTableColumnsTitles.Add("Suma de Ruc Del Informante")


        '**** Nombres de las Columnas del DataTable ****
        OrderDataTableColumnsName.Add("")
        OrderDataTableColumnsName.Add("CodSolicitudCredito")
        OrderDataTableColumnsName.Add("Comentario|3")
        OrderDataTableColumnsName.Add("RUC|1")
        OrderDataTableColumnsName.Add("NombreMoneda")
        OrderDataTableColumnsName.Add("ApellidoPaterno|3")
        OrderDataTableColumnsName.Add("ApellidoMaterno|3")
        OrderDataTableColumnsName.Add("Nombres|3")
        OrderDataTableColumnsName.Add("NombreInstitucion|3")
        OrderDataTableColumnsName.Add("CantidadBienes")
        OrderDataTableColumnsName.Add("UnidadMedida")
        OrderDataTableColumnsName.Add("Comentario|3")
        OrderDataTableColumnsName.Add("NombreMonedaDoc")
        OrderDataTableColumnsName.Add("ClasificacionBien")
        OrderDataTableColumnsName.Add("Total|2")
        OrderDataTableColumnsName.Add("TC|2")
        OrderDataTableColumnsName.Add("TotalConvertido|2")
        OrderDataTableColumnsName.Add("TotalBienes|2")
        OrderDataTableColumnsName.Add("RUCBanco")


        ColumnModo.Add("CodSolicitudCredito")

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
        sb.Append(GCCUtilitario.fHTMLGeneraTablaEstilo(ptabla, OrderDataTableColumnsTitles, OrderDataTableColumnsName, Nothing, ColumnModo))
        sb.Append("</body>")
        sb.Append(vbCrLf)
        sb.Append("</html>")
        sb.Append(vbCrLf)

        sw = New StringWriter(sb)

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "DetalleBien")
    End Sub

#End Region
End Class
