Imports GCC.UI
Imports System.Data
Imports GCC.LogicWS
Imports System.IO

Partial Class InsDesembolso_frmRepDesembolsoMensual
    Inherits GCCBase
    Dim objLog As New GCCLog("frmRepDesembolsoMensual.aspx.vb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then

            End If
        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "')", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        End Try
    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim strFechaIni As String = String.Empty
        Dim strFechaFin As String = String.Empty
        Dim table As DataTable = Nothing

        Dim objInstruccionDesembolsoNTx As LInstruccionDesembolsoNTx = Nothing

        Try
            objInstruccionDesembolsoNTx = New LInstruccionDesembolsoNTx()

            strFechaIni = Me.txtFechaInicial.Value
            strFechaFin = Me.txtFechaFinal.Value

            table = GCCUtilitario.DeserializeObject(Of DataTable)(objInstruccionDesembolsoNTx.fobjListadoDesembolsoMensualReporte(strFechaIni, strFechaFin))
            Me.pDescargarArchivo(table)
        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGenerar_Click")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "')", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        Finally
            If objInstruccionDesembolsoNTx IsNot Nothing Then objInstruccionDesembolsoNTx = Nothing
            If table IsNot Nothing Then
                table.Dispose()
                table = Nothing
            End If
            strFechaIni = String.Empty
            strFechaFin = String.Empty
        End Try
    End Sub

    Private Sub pDescargarArchivo(ByVal ptabla As DataTable)
        Dim sb As New StringBuilder
        Dim sw As StringWriter

        Dim pTitulo As String = "DESEMBOLSOS MENSUALES"

        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList
        Dim pAlineacionColumna As New ArrayList

        '**** Etiquetas de la cabecera ****
        OrderHeaderTableColumnsLabels.Add("Fecha Desembolso Mensual  Desde")
        OrderHeaderTableColumnsLabels.Add("Hasta")

        '**** Texto de cada etiqueta ****
        OrderHeaderTableColumnsText.Add(Me.txtFechaInicial.Value)
        OrderHeaderTableColumnsText.Add(Me.txtFechaFinal.Value)

        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("C.U")
        OrderDataTableColumnsTitles.Add("N° de contrato")
        OrderDataTableColumnsTitles.Add("Cliente")
        OrderDataTableColumnsTitles.Add("Moneda")
        OrderDataTableColumnsTitles.Add("Total Desembolsado Neto de IGV")
        OrderDataTableColumnsTitles.Add("Fecha de Activación")
        OrderDataTableColumnsTitles.Add("Cuota Inicial")
        OrderDataTableColumnsTitles.Add("Saldo Capital Seguro Neto")
        OrderDataTableColumnsTitles.Add("Banca")
        OrderDataTableColumnsTitles.Add("Spread")
        OrderDataTableColumnsTitles.Add("Ejecutivo Leasing")
        OrderDataTableColumnsTitles.Add("Frecuencia")
        OrderDataTableColumnsTitles.Add("Plazo")
        OrderDataTableColumnsTitles.Add("Plazo días")
        OrderDataTableColumnsTitles.Add("Pre-cuota")
        OrderDataTableColumnsTitles.Add("C. Activación")
        OrderDataTableColumnsTitles.Add("C. Estructuración")
        OrderDataTableColumnsTitles.Add("Pre-cuota")
        OrderDataTableColumnsTitles.Add("C. Activación")
        OrderDataTableColumnsTitles.Add("C. Estructuración")

        '**** Nombres de las Columnas del DataTable ****
        OrderDataTableColumnsName.Add("CodUnico")
        OrderDataTableColumnsName.Add("CodSolicitudCredito")
        OrderDataTableColumnsName.Add("NombreCliente")
        OrderDataTableColumnsName.Add("NombreMoneda")
        OrderDataTableColumnsName.Add("Total_Desembolsado")
        OrderDataTableColumnsName.Add("FechaActivacion")
        OrderDataTableColumnsName.Add("Importecuotainicial")
        OrderDataTableColumnsName.Add("Saldo_Capital_Seguro_Neto")
        OrderDataTableColumnsName.Add("NombreBanca")
        OrderDataTableColumnsName.Add("Spread")
        OrderDataTableColumnsName.Add("NombreEjecutivoleasing")
        OrderDataTableColumnsName.Add("NombreFrecuenciapago")
        OrderDataTableColumnsName.Add("Plazo")
        OrderDataTableColumnsName.Add("Plazo_Dias")
        OrderDataTableColumnsName.Add("PreCuota_Cobrada")
        OrderDataTableColumnsName.Add("Activacion_Cobrada")
        OrderDataTableColumnsName.Add("Estructuracion_Cobrada")
        OrderDataTableColumnsName.Add("PreCuota_PorCobrar")
        OrderDataTableColumnsName.Add("Activacion_PorCobrar")
        OrderDataTableColumnsName.Add("Estructuracion_PorCobrar")

        '**** Ancho de Columnas ****
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")

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
        sb.Append(Me.fstrCrearCabecera())
        sb.Append(GCCUtilitario.fHTMLGeneraTabla(ptabla, OrderDataTableColumnsTitles, OrderDataTableColumnsName, pAlineacionColumna))
        sb.Append("</body>")
        sb.Append(vbCrLf)
        sb.Append("</html>")
        sb.Append(vbCrLf)

        sw = New StringWriter(sb)

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "Desembolsos Mensuales")
    End Sub

    Private Function fstrCrearCabecera() As String
        Dim sb As New StringBuilder
        Dim sw As StringWriter

        sb.Append("<table><tr><td>&nbsp;</td><td>")

        sb.Append("<table border='1'>")
        sb.Append("<tr class='td-grid-header'>")

        sb.Append("<td>")
        sb.Append("&nbsp;</td>")

        sb.Append("<td colspan='4'>")
        sb.Append("Desembolsos</td>")

        sb.Append("<td colspan='3'>")
        sb.Append("Activados</td>")

        sb.Append("<td>")
        sb.Append("&nbsp;</td>")

        sb.Append("<td>")
        sb.Append("&nbsp;</td>")

        sb.Append("<td>")
        sb.Append("&nbsp;</td>")

        sb.Append("<td>")
        sb.Append("&nbsp;</td>")

        sb.Append("<td>")
        sb.Append("&nbsp;</td>")

        sb.Append("<td>")
        sb.Append("&nbsp;</td>")

        sb.Append("<td colspan='3'>")
        sb.Append("Comisiones cobradas</td>")

        sb.Append("<td colspan='3'>")
        sb.Append("Comisiones x cobrar</td>")

        sb.Append("</tr>")
        sb.Append("</table>")

        sb.Append("</td></tr></table>")

        sw = New StringWriter(sb)
        Return sw.ToString()
    End Function
End Class
