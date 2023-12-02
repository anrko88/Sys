Imports GCC.UI
Imports System.Data
Imports GCC.LogicWS
Imports System.IO

Partial Class Formalizacion_frmRepSaldosCredito
    Inherits GCCBase
    Dim objLog As New GCCLog("frmRepSaldosCredito.aspx.vb")

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
        Dim table As DataTable = Nothing
        Dim tableD As DataTable = Nothing

        Dim objContratoNTx As LContratoNTx = Nothing

        Try
            objContratoNTx = New LContratoNTx()

            strFechaIni = txtFechaInicial.Value

            table = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.fobjListadoSaldosCreditoReporte(strFechaIni))
            tableD = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.fobjListadoSaldosCreditoReporteDolares(strFechaIni))

            Me.pDescargarArchivo(table, tableD)


        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGenerar_Click")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "')", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        Finally
            If objContratoNTx IsNot Nothing Then objContratoNTx = Nothing
            If table IsNot Nothing Then
                table.Dispose()
                table = Nothing
            End If
            strFechaIni = String.Empty
        End Try
    End Sub

    Private Sub pDescargarArchivo(ByVal ptabla As DataTable, ByVal ptablaD As DataTable)
        Dim sb As New StringBuilder
        Dim sw As StringWriter

        Dim pTitulo As String = "SALDOS DE CRÉDITOS"

        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList
        Dim pAlineacionColumna As New ArrayList

        Dim OrderDataTableColumnsTitles_t2 As New ArrayList
        Dim OrderDataTableColumnsName_t2 As New ArrayList
        Dim pAlineacionColumna_t2 As New ArrayList

        Dim OrderDataTableColumnsTitles_t3 As New ArrayList
        Dim OrderDataTableColumnsName_t3 As New ArrayList
        Dim pAlineacionColumna_t3 As New ArrayList

        Dim OrderDataTableColumnsTitles_t4 As New ArrayList
        Dim OrderDataTableColumnsName_t4 As New ArrayList
        Dim pAlineacionColumna_t4 As New ArrayList

        '**** Etiquetas de la cabecera ****
        OrderHeaderTableColumnsLabels.Add("Fecha Saldos de Crédito  Desde")

        '**** Texto de cada etiqueta ****
        OrderHeaderTableColumnsText.Add(txtFechaInicial.Value)

        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("Código Único")
        OrderDataTableColumnsTitles.Add("N° Crédito")
        OrderDataTableColumnsTitles.Add("Moneda Crédito")
        OrderDataTableColumnsTitles.Add("Cliente")
        OrderDataTableColumnsTitles.Add("Tipo de Bien")
        OrderDataTableColumnsTitles.Add("Capital Financiado")
        OrderDataTableColumnsTitles.Add("Interés")
        OrderDataTableColumnsTitles.Add("IGV")
        OrderDataTableColumnsTitles.Add("Monto Financiado")
        OrderDataTableColumnsTitles.Add("N° Cuotas Contrato")
        OrderDataTableColumnsTitles.Add("Plazo en Días")
        OrderDataTableColumnsTitles.Add("Saldo  Seguro Financiado")
        OrderDataTableColumnsTitles.Add("Interés del Seguro Financiado")

        OrderDataTableColumnsTitles_t2.Add("Capital Financiado Pagado")
        OrderDataTableColumnsTitles_t2.Add("Interés Capital Pagado")
        OrderDataTableColumnsTitles_t2.Add("Capital Seguro pagado")
        OrderDataTableColumnsTitles_t2.Add("Interés Seguro Pagado")
        OrderDataTableColumnsTitles_t2.Add("IGV Cancelado")
        OrderDataTableColumnsTitles_t2.Add("N° Cuotas Contrato Pagadas")
        OrderDataTableColumnsTitles_t2.Add("Plazo en Días Transcurridos")

        OrderDataTableColumnsTitles_t3.Add("Tasa")
        OrderDataTableColumnsTitles_t3.Add("Fecha Desembolso")
        OrderDataTableColumnsTitles_t3.Add("Fecha Vencimiento Crédito")

        OrderDataTableColumnsTitles_t4.Add("Ejecutivo Leasing")
        OrderDataTableColumnsTitles_t4.Add("Ejecutivo Banca")
        OrderDataTableColumnsTitles_t4.Add("Banca")
        OrderDataTableColumnsTitles_t4.Add("Última Calificación SBS Cliente")
        OrderDataTableColumnsTitles_t4.Add("Última Calificación Interbank Cliente")
        OrderDataTableColumnsTitles_t4.Add("Tipo Documento")
        OrderDataTableColumnsTitles_t4.Add("Documento")

        '**** Nombres de las Columnas del DataTable ****
        OrderDataTableColumnsName.Add("CodUnico")
        OrderDataTableColumnsName.Add("CodSolicitudCredito")
        OrderDataTableColumnsName.Add("NombreMoneda")
        OrderDataTableColumnsName.Add("NombreCliente")
        OrderDataTableColumnsName.Add("ClasificacionBien")
        OrderDataTableColumnsName.Add("Valorventa")
        OrderDataTableColumnsName.Add("MONTOINTERESBIEN")
        OrderDataTableColumnsName.Add("Valorventaigv")
        OrderDataTableColumnsName.Add("Precioventa")
        OrderDataTableColumnsName.Add("Numerocuotas")
        OrderDataTableColumnsName.Add("cantdiascuota")
        OrderDataTableColumnsName.Add("Bienimporteprima")
        OrderDataTableColumnsName.Add("INTERESSEGUROBIEN")

        OrderDataTableColumnsName_t2.Add("Capital_Financiado_Pagado")
        OrderDataTableColumnsName_t2.Add("Interes_Capital_Pagado")
        OrderDataTableColumnsName_t2.Add("Capital_Seguro_Pagado")
        OrderDataTableColumnsName_t2.Add("Interes_Seguro_Pagado")
        OrderDataTableColumnsName_t2.Add("IGV_Cancelado")
        OrderDataTableColumnsName_t2.Add("N_Cuotas_Contrato_Pagadas")
        OrderDataTableColumnsName_t2.Add("Plazo_en_Dias_Transcurridos")

        OrderDataTableColumnsName_t3.Add("Teaporc")
        OrderDataTableColumnsName_t3.Add("Fecha_Desembolso")
        OrderDataTableColumnsName_t3.Add("Fecha_Vencimiento_Crédito")

        OrderDataTableColumnsName_t4.Add("EjecutivoLeasing")
        OrderDataTableColumnsName_t4.Add("NombreEjecutivobanca")
        OrderDataTableColumnsName_t4.Add("NombreBanca")
        OrderDataTableColumnsName_t4.Add("Ultima_Calificacion_SBS_Cliente")
        OrderDataTableColumnsName_t4.Add("Ultima_Calificacion_Interbank_Cliente")
        OrderDataTableColumnsName_t4.Add("NombreTipoDocumento")
        OrderDataTableColumnsName_t4.Add("NumeroDocumento")

        '**** Ancho de Columnas ****
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")

        pAlineacionColumna_t2.Add("right")
        pAlineacionColumna_t2.Add("right")
        pAlineacionColumna_t2.Add("right")
        pAlineacionColumna_t2.Add("right")
        pAlineacionColumna_t2.Add("right")
        pAlineacionColumna_t2.Add("center")
        pAlineacionColumna_t2.Add("center")

        pAlineacionColumna_t3.Add("right")
        pAlineacionColumna_t3.Add("center")
        pAlineacionColumna_t3.Add("center")

        pAlineacionColumna_t4.Add("left")
        pAlineacionColumna_t4.Add("left")
        pAlineacionColumna_t4.Add("left")
        pAlineacionColumna_t4.Add("left")
        pAlineacionColumna_t4.Add("left")
        pAlineacionColumna_t4.Add("center")
        pAlineacionColumna_t4.Add("center")

        Dim TotalColumnas As Integer = OrderDataTableColumnsTitles.Count + _
                                        OrderDataTableColumnsTitles_t2.Count + _
                                        OrderDataTableColumnsTitles_t3.Count + _
                                        OrderDataTableColumnsTitles_t4.Count + 3

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
        sb.Append(GCCUtilitario.fHTMLGeneraTablaTitulo(pTitulo, Me, TotalColumnas))
        sb.Append(" <br>")
        sb.Append(vbCrLf)
        sb.Append(GCCUtilitario.fHTMLGeneraTablaCabecera(OrderHeaderTableColumnsLabels, OrderHeaderTableColumnsText, TotalColumnas))
        'sb.Append(" <br>")
        'sb.Append(GCCUtilitario.fHTMLGeneraTablaCabecera(OrderHeaderTableColumnsLabels, OrderHeaderTableColumnsText, TotalColumnas))
        sb.Append(vbCrLf)

        sb.Append("<table>")
        sb.Append("<tr>")
        sb.Append("<td>")
        sb.Append(GCCUtilitario.fHTMLGeneraTabla(ptabla, OrderDataTableColumnsTitles, OrderDataTableColumnsName, pAlineacionColumna))
        sb.Append("</td>")
        sb.Append("<td>")
        sb.Append(GCCUtilitario.fHTMLGeneraTabla(ptabla, OrderDataTableColumnsTitles_t2, OrderDataTableColumnsName_t2, pAlineacionColumna_t2))
        sb.Append("</td>")
        sb.Append("<td>")
        sb.Append(GCCUtilitario.fHTMLGeneraTabla(ptabla, OrderDataTableColumnsTitles_t3, OrderDataTableColumnsName_t3, pAlineacionColumna_t3))
        sb.Append("</td>")
        sb.Append("<td>")
        sb.Append(GCCUtilitario.fHTMLGeneraTabla(ptabla, OrderDataTableColumnsTitles_t4, OrderDataTableColumnsName_t4, pAlineacionColumna_t4))
        sb.Append("</td>")
        sb.Append("</tr>")
        sb.Append("</table>")


        sb.Append(" <br>")

        sb.Append("<table>")
        sb.Append("<tr>")
        sb.Append("<td>")
        sb.Append(GCCUtilitario.fHTMLGeneraTabla(ptablaD, OrderDataTableColumnsTitles, OrderDataTableColumnsName, pAlineacionColumna))
        sb.Append("</td>")
        sb.Append("<td>")
        sb.Append(GCCUtilitario.fHTMLGeneraTabla(ptablaD, OrderDataTableColumnsTitles_t2, OrderDataTableColumnsName_t2, pAlineacionColumna_t2))
        sb.Append("</td>")
        sb.Append("<td>")
        sb.Append(GCCUtilitario.fHTMLGeneraTabla(ptablaD, OrderDataTableColumnsTitles_t3, OrderDataTableColumnsName_t3, pAlineacionColumna_t3))
        sb.Append("</td>")
        sb.Append("<td>")
        sb.Append(GCCUtilitario.fHTMLGeneraTabla(ptablaD, OrderDataTableColumnsTitles_t4, OrderDataTableColumnsName_t4, pAlineacionColumna_t4))
        sb.Append("</td>")
        sb.Append("</tr>")
        sb.Append("</table>")

        sb.Append("</body>")
        sb.Append(vbCrLf)
        sb.Append("</html>")
        sb.Append(vbCrLf)

        sw = New StringWriter(sb)

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "Saldos de Credito")
    End Sub
End Class
