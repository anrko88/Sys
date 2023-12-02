Imports GCC.UI
Imports System.Data
Imports GCC.LogicWS
Imports System.IO

Partial Class Formalizacion_frmRepContratosActivados
    Inherits GCCBase
    Dim objLog As New GCCLog("frmRepContratosActivados.aspx.vb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                GCCUtilitario.CargarComboValorGenerico(Me.cmbClasificacionBien, GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)
                'GCCUtilitario.CargarComboValorGenericoAnidado(cmbTipoBien, GCCConstante.C_TABLAGENERICA_TIPO_BIEN, dtCotizacion.Rows(0).Item("Codigoclasificacionbien").ToString.Trim)
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
        Dim strTipo As String = String.Empty
        Dim strFecha As String = String.Empty
        Dim strClasificacionBien As String = String.Empty
        Dim strTipoBien As String = String.Empty
        Dim table As DataTable = Nothing

        Dim objContratoNTx As LContratoNTx = Nothing

        Try
            objContratoNTx = New LContratoNTx()

            If Me.rdMensual.Checked Then strTipo = "1"
            If Me.rdAnnio.Checked Then strTipo = "2"
            If Me.rdFecha.Checked Then strTipo = "3"

            strFecha = Me.txtFecha.Value
            strClasificacionBien = Me.cmbClasificacionBien.Value
            strTipoBien = Me.hdnTipoBeneficio.Value 'Me.cmbTipoBien.Value

            table = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.fobjListadoContratosActivadosReporte(strTipo, strFecha, strClasificacionBien, strTipoBien))
            Me.pDescargarArchivo(table)
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
            strTipo = String.Empty
            strFecha = String.Empty
            strClasificacionBien = String.Empty
            strTipoBien = String.Empty
        End Try
    End Sub

    Private Sub pDescargarArchivo(ByVal ptabla As DataTable)
        Dim sb As New StringBuilder
        Dim sw As StringWriter

        Dim pTitulo As String = "CONTRATOS ACTIVADOS"

        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList
        Dim pAlineacionColumna As New ArrayList

        '**** Etiquetas de la cabecera ****
        OrderHeaderTableColumnsLabels.Add("Fecha Contratos Activados")
        If Me.cmbClasificacionBien.Value <> String.Empty Then OrderHeaderTableColumnsLabels.Add("Clasificación del Bien")
        If Me.cmbTipoBien.Value <> String.Empty Then OrderHeaderTableColumnsLabels.Add("Tipo de Bien")

        '**** Texto de cada etiqueta ****
        Dim datFecha As DateTime = Convert.ToDateTime(Me.txtFecha.Value)

        If Me.rdMensual.Checked Then OrderHeaderTableColumnsText.Add(datFecha.ToString("MMMM-yyyy"))
        If Me.rdAnnio.Checked Then OrderHeaderTableColumnsText.Add(datFecha.ToString("yyyy"))
        If Me.rdFecha.Checked Then OrderHeaderTableColumnsText.Add(Me.txtFecha.Value)

        If Me.cmbClasificacionBien.Value <> "0" Then OrderHeaderTableColumnsText.Add(Me.cmbClasificacionBien.Items(Me.cmbClasificacionBien.SelectedIndex).Text)
        If Me.hdnTipoBeneficio.Value <> "0" Then OrderHeaderTableColumnsText.Add(Me.hdnTipoBeneficioDesc.Value)

        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("N°")
        OrderDataTableColumnsTitles.Add("Código Unico")
        OrderDataTableColumnsTitles.Add("N° Cred.")
        OrderDataTableColumnsTitles.Add("Fecha Activación")
        OrderDataTableColumnsTitles.Add("Cliente")
        OrderDataTableColumnsTitles.Add("Segmento")
        OrderDataTableColumnsTitles.Add("Tipo de Bien")
        OrderDataTableColumnsTitles.Add("Detalle de Bien")
        OrderDataTableColumnsTitles.Add("N° Bienes")
        OrderDataTableColumnsTitles.Add("Descripción Bien")
        OrderDataTableColumnsTitles.Add("Moneda")
        OrderDataTableColumnsTitles.Add("Colocación Neta ")
        OrderDataTableColumnsTitles.Add("Cuota Inicial")
        OrderDataTableColumnsTitles.Add("Valor de Compra")
        OrderDataTableColumnsTitles.Add("I.G.V.")
        OrderDataTableColumnsTitles.Add("Precio de Compra")
        OrderDataTableColumnsTitles.Add("Plazo")
        OrderDataTableColumnsTitles.Add("Costo ")
        OrderDataTableColumnsTitles.Add("TASA")
        OrderDataTableColumnsTitles.Add("TIR")
        OrderDataTableColumnsTitles.Add("TIR Recalculado")
        OrderDataTableColumnsTitles.Add("Spread")
        OrderDataTableColumnsTitles.Add("Seguro")
        OrderDataTableColumnsTitles.Add("Comisión Activación Neto IGV")
        OrderDataTableColumnsTitles.Add("Opción de Compra Neto IGV")
        OrderDataTableColumnsTitles.Add("Ejecutivo")
        OrderDataTableColumnsTitles.Add("Ejecutivo Leasing")
        OrderDataTableColumnsTitles.Add("Banca")
        OrderDataTableColumnsTitles.Add("Producto")
        OrderDataTableColumnsTitles.Add("Subproducto")

        '**** Nombres de las Columnas del DataTable ****
        OrderDataTableColumnsName.Add("Id")
        OrderDataTableColumnsName.Add("CodUnico")
        OrderDataTableColumnsName.Add("CodSolicitudCredito")
        OrderDataTableColumnsName.Add("FechaActivacion")
        OrderDataTableColumnsName.Add("NombreCliente")
        OrderDataTableColumnsName.Add("Segmento")
        OrderDataTableColumnsName.Add("NombreClasificacionbien")
        OrderDataTableColumnsName.Add("NombreTipoBien")
        OrderDataTableColumnsName.Add("CantidadBien")
        OrderDataTableColumnsName.Add("DescripcionBien")
        OrderDataTableColumnsName.Add("NombreMoneda")
        OrderDataTableColumnsName.Add("ColocacionNeta")
        OrderDataTableColumnsName.Add("Importecuotainicial")
        OrderDataTableColumnsName.Add("Valorventa")
        OrderDataTableColumnsName.Add("Valorventaigv")
        OrderDataTableColumnsName.Add("Precioventa")
        OrderDataTableColumnsName.Add("Numerocuotas")
        OrderDataTableColumnsName.Add("Costofondoporc")
        OrderDataTableColumnsName.Add("Teaporc")
        OrderDataTableColumnsName.Add("TIR")
        OrderDataTableColumnsName.Add("TIRRecalculado")
        OrderDataTableColumnsName.Add("Spreadporc")
        OrderDataTableColumnsName.Add("Bienimporteprima")
        OrderDataTableColumnsName.Add("Importecomisionactivacion")
        OrderDataTableColumnsName.Add("Importeopcioncompra")
        OrderDataTableColumnsName.Add("NombreEjecutivobanca")
        OrderDataTableColumnsName.Add("NombreEjecutivoleasing")
        OrderDataTableColumnsName.Add("NombreBanca")
        OrderDataTableColumnsName.Add("Producto")
        OrderDataTableColumnsName.Add("SubProducto")

        '**** Ancho de Columnas ****
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("right")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")

        Dim TotalColumnas As Integer = OrderDataTableColumnsTitles.Count

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
        sb.Append(" <br>")
        sb.Append(vbCrLf)
        sb.Append(GCCUtilitario.fHTMLGeneraTabla(ptabla, OrderDataTableColumnsTitles, OrderDataTableColumnsName, pAlineacionColumna))
        sb.Append("</body>")
        sb.Append(vbCrLf)
        sb.Append("</html>")
        sb.Append(vbCrLf)

        sw = New StringWriter(sb)

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "Contratos Activados")
    End Sub
End Class
