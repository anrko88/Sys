Imports GCC.UI
Imports GCC.LogicWS
Imports System.Data

Partial Class Reportes_frmRepRegistroCompra
    Inherits GCCBase

    Private Sub pLlenarTablaRegCompras(ByVal pstrFecha As String)
        Dim dtReporte As Data.DataTable
        Dim objRow As TableRow
        Try
            lblTitle.Text = "REGISTRO DE COMPRAS"
            lblSubTitle.Text = String.Format("Fecha y Hora : {0}", pstrFecha)

            Dim objCells(28) As TableCell

            'Cabeceras Grilla - Fila 1
            objRow = New TableRow
            objRow.CssClass = "trCabecera"
            objRow.HorizontalAlign = HorizontalAlign.Center

            objCells(0) = New TableCell
            objCells(0).RowSpan = 3
            objCells(0).CssClass = "trCabecera"
            objCells(0).Text = "Fec.Registro Contable"

            objCells(1) = New TableCell
            objCells(1).RowSpan = 3
            objCells(1).CssClass = "trCabecera"
            objCells(1).Text = "Tipo de Compra"

            objCells(2) = New TableCell
            objCells(2).RowSpan = 3
            objCells(2).CssClass = "trCabecera"
            objCells(2).Text = "Nro.Correlativo Registro"

            objCells(3) = New TableCell
            objCells(3).RowSpan = 3
            objCells(3).CssClass = "trCabecera"
            objCells(3).Text = "Fec.Emisión Comprobante"

            objCells(4) = New TableCell
            objCells(4).RowSpan = 3
            objCells(4).CssClass = "trCabecera"
            objCells(4).Text = "Fec.Vencimiento"

            objCells(5) = New TableCell
            objCells(5).ColumnSpan = 3
            objCells(5).CssClass = "trCabecera"
            objCells(5).Text = "Comprobante Pago o Documento"

            objCells(6) = New TableCell
            objCells(6).RowSpan = 3
            objCells(6).CssClass = "trCabecera"
            objCells(6).Text = "Nº del comprobante de pago, documento, Nº de orden del formulario fisico o virtual, Nº de DUA, DSI o liquidación de cobranza u otros documentos emitidos por SUNAT para acreditar el crédito fiscal en la importación"

            objCells(7) = New TableCell
            objCells(7).ColumnSpan = 3
            objCells(7).CssClass = "trCabecera"
            objCells(7).Text = "Información del Proveedor"

            objCells(8) = New TableCell
            objCells(8).RowSpan = 3
            objCells(8).CssClass = "trCabecera"
            objCells(8).Text = "Código Único Cliente"

            objCells(9) = New TableCell
            objCells(9).RowSpan = 3
            objCells(9).CssClass = "trCabecera"
            objCells(9).Text = "Moneda de Crédito"

            objCells(10) = New TableCell
            objCells(10).RowSpan = 3
            objCells(10).CssClass = "trCabecera"
            objCells(10).Text = "Moneda Origen Comprobante"

            objCells(11) = New TableCell
            objCells(11).RowSpan = 3
            objCells(11).CssClass = "trCabecera"
            objCells(11).Text = "Tipo Cambio Interbank"

            objCells(12) = New TableCell
            objCells(12).RowSpan = 3
            objCells(12).CssClass = "trCabecera"
            objCells(12).Text = "Tasa IGV %"

            objCells(13) = New TableCell
            objCells(13).ColumnSpan = 4
            objCells(13).CssClass = "trCabecera"
            objCells(13).Text = "Adquisición gravadas destinadas a operaciones gravadas y/o de exportación en moneda de origen"

            objCells(14) = New TableCell
            objCells(14).ColumnSpan = 2
            objCells(14).CssClass = "trCabecera"
            objCells(14).Text = "Adquisición gravadas destinadas a operciones gravadas y/o de exportación"

            objCells(15) = New TableCell
            objCells(15).ColumnSpan = 2
            objCells(15).CssClass = "trCabecera"
            objCells(15).Text = "Adquisición gravadas destinadas a operciones gravadas y/o de exportación y a operciones no gravadas"

            objCells(16) = New TableCell
            objCells(16).ColumnSpan = 2
            objCells(16).CssClass = "trCabecera"
            objCells(16).Text = "Adquisiciones gravadas destinadas a operciones no gravadas"

            objCells(17) = New TableCell
            objCells(17).RowSpan = 3
            objCells(17).CssClass = "trCabecera"
            objCells(17).Text = "Valor de las adquisiciones no gravadas"

            objCells(18) = New TableCell
            objCells(18).RowSpan = 3
            objCells(18).CssClass = "trCabecera"
            objCells(18).Text = "ISC"

            objCells(19) = New TableCell
            objCells(19).RowSpan = 3
            objCells(19).CssClass = "trCabecera"
            objCells(19).Text = "Otros tributos y cargos"

            objCells(20) = New TableCell
            objCells(20).RowSpan = 3
            objCells(20).CssClass = "trCabecera"
            objCells(20).Text = "Importe Total"

            objCells(21) = New TableCell
            objCells(21).RowSpan = 3
            objCells(21).CssClass = "trCabecera"
            objCells(21).Text = "IGV contabilizado"

            objCells(22) = New TableCell
            objCells(22).RowSpan = 3
            objCells(22).CssClass = "trCabecera"
            objCells(22).Text = "Diferencia IGV por ajustar"

            objCells(23) = New TableCell
            objCells(23).RowSpan = 3
            objCells(23).CssClass = "trCabecera"
            objCells(23).Text = "Nº de comprobante de pago emitido por sujeto no domiciliado"

            objCells(24) = New TableCell
            objCells(24).ColumnSpan = 2
            objCells(24).CssClass = "trCabecera"
            objCells(24).Text = "Constancia de deposito de detracción"

            objCells(25) = New TableCell
            objCells(25).RowSpan = 3
            objCells(25).CssClass = "trCabecera"
            objCells(25).Text = "Tipo de cambio SUNAT"

            objCells(26) = New TableCell
            objCells(26).ColumnSpan = 4
            objCells(26).CssClass = "trCabecera"
            objCells(26).Text = "Referencia del comprobante de pago o documento original que se modifica"

            objCells(27) = New TableCell
            objCells(27).RowSpan = 3
            objCells(27).CssClass = "trCabecera"
            objCells(27).Text = "Afecto a retención IGV 6%"

            objCells(28) = New TableCell
            objCells(28).RowSpan = 3
            objCells(28).CssClass = "trCabecera"
            objCells(28).Text = "Referencia"

            objRow.Cells.AddRange(objCells)
            Table1.Rows.Add(objRow)


            'Fila Dos
            Dim objCells1(20) As TableCell

            objRow = New TableRow
            objCells1(0) = New TableCell
            objCells1(0).RowSpan = 2
            objCells1(0).CssClass = "trCabecera"
            objCells1(0).Text = "Tipo (Tabla 10)"

            objCells1(1) = New TableCell
            objCells1(1).RowSpan = 2
            objCells1(1).CssClass = "trCabecera"
            objCells1(1).Text = "Serie o codigo de la dependencia aduanera (Tabla 11)"

            objCells1(2) = New TableCell
            objCells1(2).RowSpan = 2
            objCells1(2).CssClass = "trCabecera"
            objCells1(2).Text = "Año de emisió de la dua o dsi"

            objCells1(3) = New TableCell
            objCells1(3).ColumnSpan = 2
            objCells1(3).CssClass = "trCabecera"
            objCells1(3).Text = "Documento de Identidad"

            objCells1(4) = New TableCell
            objCells1(4).RowSpan = 2
            objCells1(4).CssClass = "trCabecera"
            objCells1(4).Text = "Apellidos y nombres, denomición o razon social"

            objCells1(5) = New TableCell
            objCells1(5).RowSpan = 2
            objCells1(5).CssClass = "trCabecera"
            objCells1(5).Text = "Base Imponible"

            objCells1(6) = New TableCell
            objCells1(6).RowSpan = 2
            objCells1(6).CssClass = "trCabecera"
            objCells1(6).Text = "IGV"

            objCells1(7) = New TableCell
            objCells1(7).RowSpan = 2
            objCells1(7).CssClass = "trCabecera"
            objCells1(7).Text = "No Gravado"

            objCells1(8) = New TableCell
            objCells1(8).RowSpan = 2
            objCells1(8).CssClass = "trCabecera"
            objCells1(8).Text = "Importe Total"

            objCells1(9) = New TableCell
            objCells1(9).RowSpan = 2
            objCells1(9).CssClass = "trCabecera"
            objCells1(9).Text = "Base Imponible Soles"

            objCells1(10) = New TableCell
            objCells1(10).RowSpan = 2
            objCells1(10).CssClass = "trCabecera"
            objCells1(10).Text = "IGV Soles"

            objCells1(11) = New TableCell
            objCells1(11).RowSpan = 2
            objCells1(11).CssClass = "trCabecera"
            objCells1(11).Text = "Base Imponible Soles"

            objCells1(12) = New TableCell
            objCells1(12).RowSpan = 2
            objCells1(12).CssClass = "trCabecera"
            objCells1(12).Text = "IGV Soles"

            objCells1(13) = New TableCell
            objCells1(13).RowSpan = 2
            objCells1(13).CssClass = "trCabecera"
            objCells1(13).Text = "Base Imponible Soles"

            objCells1(14) = New TableCell
            objCells1(14).RowSpan = 2
            objCells1(14).CssClass = "trCabecera"
            objCells1(14).Text = "IGV Soles"

            objCells1(15) = New TableCell
            objCells1(15).RowSpan = 2
            objCells1(15).CssClass = "trCabecera"
            objCells1(15).Text = "Número"

            objCells1(16) = New TableCell
            objCells1(16).RowSpan = 2
            objCells1(16).CssClass = "trCabecera"
            objCells1(16).Text = "Fecha Emisión"

            objCells1(17) = New TableCell
            objCells1(17).RowSpan = 2
            objCells1(17).CssClass = "trCabecera"
            objCells1(17).Text = "Fecha"

            objCells1(18) = New TableCell
            objCells1(18).RowSpan = 2
            objCells1(18).CssClass = "trCabecera"
            objCells1(18).Text = "Tipo (Tabla 10)"

            objCells1(19) = New TableCell
            objCells1(19).RowSpan = 2
            objCells1(19).CssClass = "trCabecera"
            objCells1(19).Text = "Serie"

            objCells1(20) = New TableCell
            objCells1(20).RowSpan = 2
            objCells1(20).CssClass = "trCabecera"
            objCells1(20).Text = "Nº del comprobante de pago o documento"

            objRow.Cells.AddRange(objCells1)
            Table1.Rows.Add(objRow)

            'Fila Tres
            Dim objCells2(1) As TableCell

            objRow = New TableRow
            objCells2(0) = New TableCell
            objCells2(0).CssClass = "trCabecera"
            objCells2(0).Text = "Tipo (Tabla 2)"

            objCells2(1) = New TableCell
            objCells2(1).CssClass = "trCabecera"
            objCells2(1).Text = "Numero"

            objRow.Cells.AddRange(objCells2)
            Table1.Rows.Add(objRow)


            'Consulta
            Dim strFechaIni As String = Request.QueryString("p1") '"07"
            Dim strFechaFin As String = Request.QueryString("p2") '"2012"
            Dim objUtilNTx As New LUtilNTX
            Dim dtRegCompra As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTx.ListarRegistroCompra(strFechaIni, strFechaFin))


            For Each dr As Data.DataRow In dtRegCompra.Rows

                Dim objCellsCuerpo(42) As TableCell

                objRow = New TableRow
                objRow.CssClass = "tdFilas"
                objRow.HorizontalAlign = HorizontalAlign.Center

                objCellsCuerpo(0) = New TableCell
                objCellsCuerpo(0).Text = GCCUtilitario.CheckStr(dr("FechaDesembolso").ToString)

                objCellsCuerpo(1) = New TableCell
                objCellsCuerpo(1).Text = GCCUtilitario.CheckStr(dr("FlagRegistro").ToString)

                objCellsCuerpo(2) = New TableCell
                objCellsCuerpo(2).Text = GCCUtilitario.CheckStr(dr("Correlativo").ToString)

                objCellsCuerpo(3) = New TableCell
                objCellsCuerpo(3).Text = GCCUtilitario.CheckStr(dr("FechaEmision").ToString)

                objCellsCuerpo(4) = New TableCell
                objCellsCuerpo(4).Text = GCCUtilitario.CheckStr(dr("FechaVencimiento").ToString)

                objCellsCuerpo(5) = New TableCell
                objCellsCuerpo(5).Text = GCCUtilitario.CheckStr(dr("TipoDocumento").ToString)
                objCellsCuerpo(5).CssClass = "CssText"

                objCellsCuerpo(6) = New TableCell
                objCellsCuerpo(6).Text = GCCUtilitario.CheckStr(dr("SerieDocumento").ToString)

                objCellsCuerpo(7) = New TableCell
                objCellsCuerpo(7).Text = GCCUtilitario.CheckStr(dr("AnnoDUA").ToString)

                objCellsCuerpo(8) = New TableCell
                objCellsCuerpo(8).Text = GCCUtilitario.CheckStr(dr("NroDocumento").ToString)

                objCellsCuerpo(9) = New TableCell
                objCellsCuerpo(9).Text = GCCUtilitario.CheckStr(dr("TipoDocumentoProv").ToString)

                objCellsCuerpo(10) = New TableCell
                objCellsCuerpo(10).Text = GCCUtilitario.CheckStr(dr("NroDocumentoProv").ToString)

                objCellsCuerpo(11) = New TableCell
                objCellsCuerpo(11).Text = GCCUtilitario.CheckStr(dr("NombreProveedor").ToString)

                objCellsCuerpo(12) = New TableCell
                objCellsCuerpo(12).Text = GCCUtilitario.CheckStr(dr("CodUnico").ToString)

                objCellsCuerpo(13) = New TableCell
                objCellsCuerpo(13).Text = GCCUtilitario.CheckStr(dr("MonedaContrato").ToString)

                objCellsCuerpo(14) = New TableCell
                objCellsCuerpo(14).Text = GCCUtilitario.CheckStr(dr("MonedaDocumento").ToString)

                objCellsCuerpo(15) = New TableCell
                objCellsCuerpo(15).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("TCUtilizado").ToString).ToString(GCCConstante.C_FormatMiles6))
                objCellsCuerpo(15).CssClass = "cost"

                objCellsCuerpo(16) = New TableCell
                objCellsCuerpo(16).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("TasaIGV").ToString).ToString(GCCConstante.C_FormatMiles6))
                objCellsCuerpo(16).CssClass = "cost"

                objCellsCuerpo(17) = New TableCell
                objCellsCuerpo(17).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("MontoGravado").ToString).ToString(GCCConstante.C_FormatMiles))
                objCellsCuerpo(17).CssClass = "cost"

                objCellsCuerpo(18) = New TableCell
                objCellsCuerpo(18).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("MontoIGV").ToString).ToString(GCCConstante.C_FormatMiles6))
                objCellsCuerpo(18).CssClass = "cost"

                objCellsCuerpo(19) = New TableCell
                objCellsCuerpo(19).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("MontoNoGravado").ToString).ToString(GCCConstante.C_FormatMiles))
                objCellsCuerpo(19).CssClass = "cost"

                objCellsCuerpo(20) = New TableCell
                objCellsCuerpo(20).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("ImporteTotal").ToString).ToString(GCCConstante.C_FormatMiles))
                objCellsCuerpo(20).CssClass = "cost"

                objCellsCuerpo(21) = New TableCell
                objCellsCuerpo(21).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("BaseImportacion").ToString).ToString(GCCConstante.C_FormatMiles))
                objCellsCuerpo(21).CssClass = "cost"

                objCellsCuerpo(22) = New TableCell
                objCellsCuerpo(22).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("BaseIGVImportacion").ToString).ToString(GCCConstante.C_FormatMiles6))
                objCellsCuerpo(22).CssClass = "cost"

                objCellsCuerpo(23) = New TableCell
                objCellsCuerpo(23).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("BaseImportacion1").ToString).ToString(GCCConstante.C_FormatMiles))
                objCellsCuerpo(23).CssClass = "cost"

                objCellsCuerpo(24) = New TableCell
                objCellsCuerpo(24).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("BaseIGVImportacion1").ToString).ToString(GCCConstante.C_FormatMiles6))
                objCellsCuerpo(24).CssClass = "cost"

                objCellsCuerpo(25) = New TableCell
                objCellsCuerpo(25).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("BaseImportacion2").ToString).ToString(GCCConstante.C_FormatMiles))
                objCellsCuerpo(25).CssClass = "cost"

                objCellsCuerpo(26) = New TableCell
                objCellsCuerpo(26).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("BaseIGVImportacion2").ToString).ToString(GCCConstante.C_FormatMiles6))
                objCellsCuerpo(26).CssClass = "cost"

                objCellsCuerpo(27) = New TableCell
                objCellsCuerpo(27).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("ValorAdquisision").ToString).ToString(GCCConstante.C_FormatMiles))
                objCellsCuerpo(27).CssClass = "cost"

                objCellsCuerpo(28) = New TableCell
                objCellsCuerpo(28).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("ISC").ToString).ToString(GCCConstante.C_FormatMiles))
                objCellsCuerpo(28).CssClass = "cost"

                objCellsCuerpo(29) = New TableCell
                objCellsCuerpo(29).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("OtrosTributos").ToString).ToString(GCCConstante.C_FormatMiles))
                objCellsCuerpo(29).CssClass = "cost"

                objCellsCuerpo(30) = New TableCell
                objCellsCuerpo(30).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("Total").ToString).ToString(GCCConstante.C_FormatMiles))
                objCellsCuerpo(30).CssClass = "cost"

                objCellsCuerpo(31) = New TableCell
                objCellsCuerpo(31).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("IGVContabilizado").ToString).ToString(GCCConstante.C_FormatMiles6))
                objCellsCuerpo(31).CssClass = "cost"

                objCellsCuerpo(32) = New TableCell
                objCellsCuerpo(32).Text = GCCUtilitario.CheckMontoCeroVacio(GCCUtilitario.CheckDecimal(dr("DiferenciaIGV").ToString).ToString(GCCConstante.C_FormatMiles6))
                objCellsCuerpo(32).CssClass = "cost"

                objCellsCuerpo(33) = New TableCell
                objCellsCuerpo(33).Text = GCCUtilitario.CheckStr(dr("NroComprobante").ToString)

                objCellsCuerpo(34) = New TableCell
                objCellsCuerpo(34).Text = GCCUtilitario.CheckStr(dr("NroConstanciaDetraccion").ToString)

                objCellsCuerpo(35) = New TableCell
                objCellsCuerpo(35).Text = GCCUtilitario.CheckStr(dr("FechaEmisionDetraccion").ToString)

                objCellsCuerpo(36) = New TableCell
                objCellsCuerpo(36).Text = GCCUtilitario.CheckStr(dr("TipoDocumentoAdd").ToString)

                objCellsCuerpo(37) = New TableCell
                objCellsCuerpo(37).Text = GCCUtilitario.CheckStr(dr("FechaEmisionRef").ToString)

                objCellsCuerpo(38) = New TableCell
                objCellsCuerpo(38).Text = GCCUtilitario.CheckStr(dr("TipoDocumentoRef").ToString)

                objCellsCuerpo(39) = New TableCell
                objCellsCuerpo(39).Text = GCCUtilitario.CheckStr(dr("SerieRef").ToString)

                objCellsCuerpo(40) = New TableCell
                objCellsCuerpo(40).Text = GCCUtilitario.CheckStr(dr("NroComprobanteRef").ToString)

                objCellsCuerpo(41) = New TableCell
                objCellsCuerpo(41).Text = GCCUtilitario.CheckStr(dr("Retencion").ToString)

                objCellsCuerpo(42) = New TableCell
                objCellsCuerpo(42).Text = GCCUtilitario.CheckStr(dr("Referencia").ToString)

                objRow.Cells.AddRange(objCellsCuerpo)
                Table1.Rows.Add(objRow)

            Next

        Catch ex As Exception
            Throw ex
        Finally
            dtReporte = Nothing
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pLlenarTablaRegCompras(Now.Date.ToString)
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Try
            Dim mNomReporte As String = "rptRegistroCompra.xls"
            Dim style As String = "<style type='text/css'> " & _
                                ".title{ " & _
                                "    border-right: black 1px solid; " & _
                                "    border-top: black 1px solid;" & _
                                "    font-weight: bold; " & _
                                "    font-size: 11px; " & _
                                "    border-left: black 1px solid; " & _
                                "    color: #ffffff;" & _
                                "    border-bottom: black 1px solid; " & _
                                "    font-family: arial; " & _
                                "    height: 15px; " & _
                                "    background-color: black;" & _
                                "    text-align:Center;" & _
                                "}" & _
                                ".trCabecera{" & _
                                "    font-family:arial;" & _
                                "    font-size: 9px;" & _
                                "    text-align:center;" & _
                                "    background-color: silver;" & _
                                "    border-right: black 1px solid;" & _
                                "    border-top: black 1px solid;" & _
                                "    border-bottom: black 1px solid;" & _
                                "    border-left: black 1px solid; " & _
                                "}" & _
                                ".tdFilas{" & _
                                "    font-family:arial;" & _
                                "    font-size: 9px;" & _
                                "    text-align:center;" & _
                                "    background-color:#ffffff;" & _
                                "    border-right: black 1px solid;" & _
                                "    border-top: black 1px solid;" & _
                                "    border-bottom: black 1px solid;" & _
                                "    border-left: black 1px solid;" & _
                                "}" & _
                                ".tableblackgrilla {" & _
                                "    border:solid;" & _
                                "	border:black;" & _
                                "	border-collapse:collapse;" & _
                                "	border-bottom-color:black;" & _
                                "	border-bottom-style:solid;" & _
                                "	border-top:black:1px;" & _
                                "	border-top-color:black;" & _
                                "	border-top-style:solid;" & _
                                "	border-left:black:1px;" & _
                                "	border-left-color:black;" & _
                                "	border-left-style:solid;" & _
                                "	border-right:black:1px;" & _
                                "	border-right-color:black;" & _
                                "	border-right-style:solid;" & _
                                "	border-width:1px;" & _
                                "}" & _
                                ".CssText { mso-number-format:\@; }" & _
                                ".cost{mso-number-format:'#,##0.00';}" & _
                                "</style>"

            Dim sb As New StringBuilder()
            Dim sw As New IO.StringWriter(sb)
            Dim htw As New HtmlTextWriter(sw)
            tblReporte.EnableViewState = False
            tblReporte.RenderControl(htw)
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", "attachment;filename=" & mNomReporte)
            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.[Default]
            Response.Write(style)
            Response.Write(sb.ToString())
            Response.[End]()
        Catch ex As Exception
            Dim strMensaje As String = ex.Message.ToString
            strMensaje = Replace(strMensaje, "'", " ")
            ClientScript.RegisterStartupScript(Me.GetType(), "script", "<script>alert(' " + strMensaje + " ');</script>")
        End Try
    End Sub
End Class
