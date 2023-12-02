Imports GCC.UI
Imports System.Data
Imports GCC.LogicWS
Imports System.IO

Partial Class Reportes_frmRepSunatCesionesContrato
    Inherits GCCBase
    Dim objLog As New GCCLog("frmRepSunatCesionesContrato.aspx.vb")

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

        Dim objCesionContratoNTx As LCesionContratoNTx = Nothing

        Try
            objCesionContratoNTx = New LCesionContratoNTx()

            strFechaIni = txtFechaInicial.Value
            strFechaFin = txtFechaFinal.Value

            table = GCCUtilitario.DeserializeObject(Of DataTable)(objCesionContratoNTx.ListadoCesionContratoReporte(strFechaIni, strFechaFin))
            Me.pDescargarArchivo(table)
        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGenerar_Click")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "')", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        Finally
            If objCesionContratoNTx IsNot Nothing Then objCesionContratoNTx = Nothing
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

        Dim pTitulo As String = "SUNAT CESIONES DE CONTRATO"

        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList
        Dim pAlineacionColumna As New ArrayList

        '**** Etiquetas de la cabecera ****
        OrderHeaderTableColumnsLabels.Add("Fecha Cesión de Contrato  Desde")
        OrderHeaderTableColumnsLabels.Add("Hasta")

        '**** Texto de cada etiqueta ****
        OrderHeaderTableColumnsText.Add(txtFechaInicial.Value)
        OrderHeaderTableColumnsText.Add(txtFechaFinal.Value)

        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("Leasing Antiguo")
        OrderDataTableColumnsTitles.Add("Cliente Original")
        OrderDataTableColumnsTitles.Add("Bienes que quedan en Contrato Original")
        OrderDataTableColumnsTitles.Add("Nuevo Número de Leasing")
        OrderDataTableColumnsTitles.Add("Nuevo Cliente")
        OrderDataTableColumnsTitles.Add("Bienes Transferidos a Nuevo Contrato")
        OrderDataTableColumnsTitles.Add("Ruc del Informante")
        OrderDataTableColumnsTitles.Add("Fecha de celebración del nuevo contrato en que se transfiere la posición contractual")
        OrderDataTableColumnsTitles.Add("Número o código asignado a cada contrato")
        OrderDataTableColumnsTitles.Add("Tipo de Contrato Original Leasing o Leaseback")
        OrderDataTableColumnsTitles.Add("Plazo expresado en meses del nuevo contrato o lo que resta para su culminación")
        OrderDataTableColumnsTitles.Add("Tipo de Documento del Arrendatario Original")
        OrderDataTableColumnsTitles.Add("N° de Documento del Arrendatario Original")
        OrderDataTableColumnsTitles.Add("Apellido Paterno del proveedor del bien si se trata de una persona natural")
        OrderDataTableColumnsTitles.Add("Apellido Materno del proveedor del bien si se trata de una persona natural")
        OrderDataTableColumnsTitles.Add("Nombres del proveedor del bien si se trata de una persona natural")
        OrderDataTableColumnsTitles.Add("Razon Social del proveedor del bien si se trata de una persona no natural")
        OrderDataTableColumnsTitles.Add("Tipo de Documento del Nuevo Arrendatario")
        OrderDataTableColumnsTitles.Add("N° de Documento del Nuevo Arrendatario")
        OrderDataTableColumnsTitles.Add("Apellido Paterno del que adquiere la posición contractual(nuevo arrendatario) si se trata de una persona natural")
        OrderDataTableColumnsTitles.Add("Apellido Materno del que adquiere la posición contractual(nuevo arrendatario) si se trata de una persona natural")
        OrderDataTableColumnsTitles.Add("Nombres del que adquiere la posición contractual(nuevo arrendatario) si se trata de una persona natural")
        OrderDataTableColumnsTitles.Add("Razón Social del que adquiere la posición contractual(nuevo arrendatario) si se trata de una no persona natural")
        OrderDataTableColumnsTitles.Add("Moneda en la cual se efectuó el contrato")
        OrderDataTableColumnsTitles.Add("Monto de venta por el cual el arrendatario original transfiere su posición contractual al nuevo arrendatario")

        '**** Nombres de las Columnas del DataTable ****
        OrderDataTableColumnsName.Add("Leasing_Antiguo")
        OrderDataTableColumnsName.Add("Cliente_Original")
        OrderDataTableColumnsName.Add("Bienes_Contrato_Original")
        OrderDataTableColumnsName.Add("Leasing_Nuevo")
        OrderDataTableColumnsName.Add("Nuevo_Cliente")
        OrderDataTableColumnsName.Add("Bienes_Nuevo_Contrato")
        OrderDataTableColumnsName.Add("Ruc_Informante")
        OrderDataTableColumnsName.Add("Fecha_Nuevo_Contrato")
        OrderDataTableColumnsName.Add("Numero_Asignado_Contrato")
        OrderDataTableColumnsName.Add("Tipo_Contrato_Original")
        OrderDataTableColumnsName.Add("Cuotas_Faltantes")
        OrderDataTableColumnsName.Add("Tipo_Documento_Arrendatario_Original")
        OrderDataTableColumnsName.Add("Nro_Documento_Arrendatario_Original")
        OrderDataTableColumnsName.Add("Ap_Paterno_Proveedor")
        OrderDataTableColumnsName.Add("Ap_Materno_Proveedor")
        OrderDataTableColumnsName.Add("Nombre_Proveedor")
        OrderDataTableColumnsName.Add("RazonSocial_Proveedor")
        OrderDataTableColumnsName.Add("Tipo_Documento_Arrendatario_Nuevo")
        OrderDataTableColumnsName.Add("Nro_Documento_Arrendatario_Nuevo")
        OrderDataTableColumnsName.Add("Ap_Paterno_Arrendatario_Nuevo")
        OrderDataTableColumnsName.Add("Ap_Materno_Arrendatario_Nuevo")
        OrderDataTableColumnsName.Add("Nombre_Arrendatario_Nuevo")
        OrderDataTableColumnsName.Add("RazonSocial_Arrendatario_Nuevo")
        OrderDataTableColumnsName.Add("Moneda_Contrato")
        OrderDataTableColumnsName.Add("Deuda_Actual")

        '**** Ancho de Columnas ****
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
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
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("left")
        pAlineacionColumna.Add("center")
        pAlineacionColumna.Add("center")

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
        sb.Append("<br>")
        sb.Append(vbCrLf)
        sb.Append(GCCUtilitario.fHTMLGeneraTablaCabecera(OrderHeaderTableColumnsLabels, OrderHeaderTableColumnsText, ptabla.Columns.Count))
        sb.Append("<br>")
        sb.Append(vbCrLf)
        sb.Append(GCCUtilitario.fHTMLGeneraTabla(ptabla, OrderDataTableColumnsTitles, OrderDataTableColumnsName, pAlineacionColumna))
        sb.Append("</body>")
        sb.Append(vbCrLf)
        sb.Append("</html>")
        sb.Append(vbCrLf)

        sw = New StringWriter(sb)

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "Cesiones de Contrato")
    End Sub
End Class
