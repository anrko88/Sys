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
Partial Class Reportes_frmFiltroRepSunatContrato
    Inherits GCCBase

    Dim objLog As New GCCLog("frmFiltroRepSunatContrato.aspx.vb")
#Region "   Eventos     "


    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim dtmFCCI As DateTime
        Dim dtmFCCD As DateTime
        Dim dtmFACI As DateTime
        Dim dtmFACD As DateTime

        Dim table As DataTable = Nothing

        Dim objContratoNTx As LContratoNTx = Nothing

        Try
            objContratoNTx = New LContratoNTx()
            If txtFechaCelConIni.Value = String.Empty Then
                dtmFCCI = GCCUtilitario.CheckDate("01/01/1900")
            Else
                dtmFCCI = GCCUtilitario.CheckDate(txtFechaCelConIni.Value)
            End If
            If txtFechaCelConFin.Value = String.Empty Then
                dtmFCCD = GCCUtilitario.CheckDate("01/01/1900")
            Else
                dtmFCCD = GCCUtilitario.CheckDate(txtFechaCelConFin.Value)
            End If
            If txtFechaActIni.Value = String.Empty Then
                dtmFACI = GCCUtilitario.CheckDate("01/01/1900")
            Else
                dtmFACI = GCCUtilitario.CheckDate(txtFechaActIni.Value)
            End If
            If txtFechaActFin.Value = String.Empty Then
                dtmFACD = GCCUtilitario.CheckDate("01/01/1900")
            Else
                dtmFACD = GCCUtilitario.CheckDate(txtFechaActFin.Value)
            End If

            table = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoReporteSunatContratos(dtmFCCI, dtmFCCD, dtmFACI, dtmFACD))
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
        End Try
    End Sub

#End Region

#Region "   Metodos     "
    Private Sub pDescargarArchivo(ByVal ptabla As DataTable)
        Dim sb As New StringBuilder
        Dim sw As StringWriter

        Dim pTitulo As String = "SUNAT - Contrato"

        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList        
        Dim ColumnStyle As New ArrayList

        '**** Etiquetas de la cabecera ****
        If txtFechaCelConIni.Value <> String.Empty And txtFechaCelConFin.Value <> String.Empty Then
            OrderHeaderTableColumnsLabels.Add("Fecha Celebración Contrato Desde")
            OrderHeaderTableColumnsLabels.Add("Hasta")
            OrderHeaderTableColumnsText.Add(txtFechaCelConIni.Value)
            OrderHeaderTableColumnsText.Add(txtFechaCelConFin.Value)
        End If
        If txtFechaActIni.Value <> String.Empty And txtFechaActIni.Value <> String.Empty Then
            OrderHeaderTableColumnsLabels.Add("Fecha Activación Contrato Desde")
            OrderHeaderTableColumnsLabels.Add("Hasta")
            OrderHeaderTableColumnsText.Add(txtFechaActIni.Value)
            OrderHeaderTableColumnsText.Add(txtFechaActFin.Value)
        End If

        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("Nº")
        OrderDataTableColumnsTitles.Add("Tipo de Contrato Original")
        OrderDataTableColumnsTitles.Add("Número o Código Asignado")
        OrderDataTableColumnsTitles.Add("Fecha Celebración del Contrato")
        OrderDataTableColumnsTitles.Add("RUC Informante")
        OrderDataTableColumnsTitles.Add("RUC Arrendatario")
        OrderDataTableColumnsTitles.Add("Razón Social del Arrendatario")
        OrderDataTableColumnsTitles.Add("Apellido Materno")
        OrderDataTableColumnsTitles.Add("Apellido Paterno")
        OrderDataTableColumnsTitles.Add("Nombres")
        OrderDataTableColumnsTitles.Add("Moneda Contrato")
        OrderDataTableColumnsTitles.Add("Capital Financiado")
        OrderDataTableColumnsTitles.Add("Interés a Devengar durante Plazo Contrato")
        OrderDataTableColumnsTitles.Add("I.G.V. Capital")
        OrderDataTableColumnsTitles.Add("I.G.V. Interés a devengar")
        OrderDataTableColumnsTitles.Add("Monto Total Contrato")
        OrderDataTableColumnsTitles.Add("Cantidad de Meses")
        OrderDataTableColumnsTitles.Add("Opción de Compra Neto IGV")
        OrderDataTableColumnsTitles.Add("Monto Cuota Fija neto IGV")
        OrderDataTableColumnsTitles.Add("Monto Cuota Varible neto IGV")
        OrderDataTableColumnsTitles.Add("Forma de Cálculo Cuota Varible")
        OrderDataTableColumnsTitles.Add("Fecha Vencimiento 1era Cuota")

        '**** Nombres de las Columnas del DataTable ****
        'OrderDataTableColumnsName.Add("<Campo>|<Estilo>")
        OrderDataTableColumnsName.Add("Contador")
        OrderDataTableColumnsName.Add("TipoContratoOriginal")
        OrderDataTableColumnsName.Add("NumeroCodigoAsignatario")
        OrderDataTableColumnsName.Add("FechaCelebracionContrato")
        OrderDataTableColumnsName.Add("RucInformante")
        OrderDataTableColumnsName.Add("RucArrendatario")
        OrderDataTableColumnsName.Add("RazonSocialArrendatario|3")
        OrderDataTableColumnsName.Add("ApPaternoArrendatario|3")
        OrderDataTableColumnsName.Add("ApMaternoArrendatario")
        OrderDataTableColumnsName.Add("NombreArrendatario|3")
        OrderDataTableColumnsName.Add("MonedaContrato")
        OrderDataTableColumnsName.Add("MontoCapitalFinanciadoInicial|2")
        OrderDataTableColumnsName.Add("InteresDevengarKapitalInicial|2")
        OrderDataTableColumnsName.Add("IGVTotalInicial|2")
        OrderDataTableColumnsName.Add("IGVInteresKapitalInicial|2")
        OrderDataTableColumnsName.Add("MontoTotalContrato|2")
        OrderDataTableColumnsName.Add("CantMesesCredito")
        OrderDataTableColumnsName.Add("OpcionCompraNetoIGV|2")
        OrderDataTableColumnsName.Add("MontoCuotaFijaNetoIGV|2")
        OrderDataTableColumnsName.Add("MontoCuotaVariableNetoIGV|2")
        OrderDataTableColumnsName.Add("ComentarioCuotaVariable")
        OrderDataTableColumnsName.Add("FechaVencimientoPrimeraCuota")


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

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "SunatContrato")
    End Sub
#End Region
End Class
