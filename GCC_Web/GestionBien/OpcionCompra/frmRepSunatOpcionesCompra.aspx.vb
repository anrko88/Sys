Imports GCC.UI
Imports System.Data
Imports GCC.LogicWS
Imports System.IO

Partial Class Reportes_frmRepSunatOpcionesCompra
    Inherits GCCBase
    Dim objLog As New GCCLog("frmRepSunatOpcionesCompra.aspx.vb")

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

        Dim objOpcionCompraNTx As LOpcionCompraNTx = Nothing

        Try
            objOpcionCompraNTx = New LOpcionCompraNTx()

            strFechaIni = txtFechaInicial.Value
            strFechaFin = txtFechaFinal.Value

            table = GCCUtilitario.DeserializeObject(Of DataTable)(objOpcionCompraNTx.fobjListadoBienOpcionCompraReporte(strFechaIni, strFechaFin))
            Me.pDescargarArchivo(table)
        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGenerar_Click")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "')", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        Finally
            If objOpcionCompraNTx IsNot Nothing Then objOpcionCompraNTx = Nothing
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

        Dim pTitulo As String = "SUNAT OPCIONES DE COMPRA"

        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList
        Dim pAlineacionColumna As New ArrayList

        '**** Etiquetas de la cabecera ****
        OrderHeaderTableColumnsLabels.Add("Fecha Opción de Compra  Desde")
        OrderHeaderTableColumnsLabels.Add("Hasta")

        '**** Texto de cada etiqueta ****
        OrderHeaderTableColumnsText.Add(txtFechaInicial.Value)
        OrderHeaderTableColumnsText.Add(txtFechaFinal.Value)

        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("N°")
        OrderDataTableColumnsTitles.Add("Ruc Informante")
        OrderDataTableColumnsTitles.Add("Crédito")
        OrderDataTableColumnsTitles.Add("Fecha Pago Opción Compra")
        OrderDataTableColumnsTitles.Add("Tipo Documento Cliente")
        OrderDataTableColumnsTitles.Add("N° Documento Cliente")
        OrderDataTableColumnsTitles.Add("Ap Paterno")
        OrderDataTableColumnsTitles.Add("Ap Materno")
        OrderDataTableColumnsTitles.Add("Nombre")
        OrderDataTableColumnsTitles.Add("Razón Social Adquiriente")
        OrderDataTableColumnsTitles.Add("Descripción Bien")
        OrderDataTableColumnsTitles.Add("Cant Bienes")
        OrderDataTableColumnsTitles.Add("Tipo de Bien")
        OrderDataTableColumnsTitles.Add("Unidad de Medida")
        OrderDataTableColumnsTitles.Add("Moneda Transacción")
        OrderDataTableColumnsTitles.Add("Monto Adquisición Inc. IGV")

        '**** Nombres de las Columnas del DataTable ****
        OrderDataTableColumnsName.Add("Id")
        OrderDataTableColumnsName.Add("Ruc_Informante")
        OrderDataTableColumnsName.Add("Credito")
        OrderDataTableColumnsName.Add("Fecha_Pago_Opcion_Compra")
        OrderDataTableColumnsName.Add("Tipo_Documento_Cliente")
        OrderDataTableColumnsName.Add("N_Documento_Cliente")
        OrderDataTableColumnsName.Add("Ap_Paterno")
        OrderDataTableColumnsName.Add("Ap_Materno")
        OrderDataTableColumnsName.Add("Nombre")
        OrderDataTableColumnsName.Add("Razon_Social_Adquiriente")
        OrderDataTableColumnsName.Add("Descripcion_Bien")
        OrderDataTableColumnsName.Add("Cant_Bienes")
        OrderDataTableColumnsName.Add("Tipo_de_Bien")
        OrderDataTableColumnsName.Add("Unidad_de_Medida")
        OrderDataTableColumnsName.Add("Moneda_Transaccion")
        OrderDataTableColumnsName.Add("Monto_Adquisicion_Inc_IGV")

        '**** Tipo de columna ****
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
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
        sb.Append(GCCUtilitario.fHTMLGeneraTabla(ptabla, OrderDataTableColumnsTitles, OrderDataTableColumnsName, pAlineacionColumna, True))
        sb.Append("</body>")
        sb.Append(vbCrLf)
        sb.Append("</html>")
        sb.Append(vbCrLf)

        sw = New StringWriter(sb)

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "Opcion de Compra")
    End Sub
End Class
