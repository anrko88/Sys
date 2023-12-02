Imports GCC.UI
Imports GCC.LogicWS
Imports System.Data
Partial Class Reportes_frmRepConsultaImpuestoMultaInmueble
    Inherits GCCBase

    Private Sub pLlenarTablaRegCompras(ByVal pstrFecha As String)
        Dim dtReporte As Data.DataTable
        Dim StrCodigo() As String
        StrCodigo = Request.QueryString("strcodigo").Split("|"c)
        Dim objRow As TableRow
        Try
            lblTitle.Text = "REPORTE IMPUESTOS Y MULTAS INMUEBLES"
            'lblSubTitle.Text = String.Format("Fecha y Hora : {0}", pstrFecha)

            Dim objCells(22) As TableCell

            'Cabeceras Grilla - Fila 1
            objRow = New TableRow
            objRow.CssClass = "trCabecera"
            objRow.HorizontalAlign = HorizontalAlign.Center

            objCells(0) = New TableCell
            objCells(0).CssClass = "trCabecera"
            objCells(0).Text = "Nº Contrato"

            objCells(1) = New TableCell
            objCells(1).CssClass = "trCabecera"
            objCells(1).Text = "CU Cliente"

            objCells(2) = New TableCell
            objCells(2).CssClass = "trCabecera"
            objCells(2).Text = "Razón Social o Nombre"

            objCells(3) = New TableCell
            objCells(3).CssClass = "trCabecera"
            objCells(3).Text = "Tipo de Documento"

            objCells(4) = New TableCell
            objCells(4).CssClass = "trCabecera"
            objCells(4).Text = "Nº de Documento"

            objCells(5) = New TableCell
            objCells(5).CssClass = "trCabecera"
            objCells(5).Text = "Departamento"

            objCells(6) = New TableCell
            objCells(6).CssClass = "trCabecera"
            objCells(6).Text = "Provincia"

            objCells(7) = New TableCell
            objCells(7).CssClass = "trCabecera"
            objCells(7).Text = "Distrito"

            objCells(8) = New TableCell
            objCells(8).CssClass = "trCabecera"
            objCells(8).Text = "Ubicación"

            objCells(9) = New TableCell
            objCells(9).CssClass = "trCabecera"
            objCells(9).Text = "Lote"

            objCells(10) = New TableCell
            objCells(10).CssClass = "trCabecera"
            objCells(10).Text = "Periodo"

            objCells(11) = New TableCell
            objCells(11).CssClass = "trCabecera"
            objCells(11).Text = "Total Autovaluo"

            objCells(12) = New TableCell
            objCells(12).CssClass = "trCabecera"
            objCells(12).Text = "Total Predial"

            objCells(13) = New TableCell
            objCells(13).CssClass = "trCabecera"
            objCells(13).Text = "Código del Predio"

            objCells(14) = New TableCell
            objCells(14).CssClass = "trCabecera"
            objCells(14).Text = "Autovaluo"

            objCells(15) = New TableCell
            objCells(15).CssClass = "trCabecera"
            objCells(15).Text = "Impuesto Predial"

            objCells(16) = New TableCell
            objCells(16).CssClass = "trCabecera"
            objCells(16).Text = "Arbitrio"

            objCells(17) = New TableCell
            objCells(17).CssClass = "trCabecera"
            objCells(17).Text = "Multa"

            objCells(18) = New TableCell
            objCells(18).CssClass = "trCabecera"
            objCells(18).Text = "Fiscalización"

            objCells(19) = New TableCell
            objCells(19).CssClass = "trCabecera"
            objCells(19).Text = "Importe Total"

            objCells(20) = New TableCell
            objCells(20).CssClass = "trCabecera"
            objCells(20).Text = "Pago Cliente"

            objCells(21) = New TableCell
            objCells(21).CssClass = "trCabecera"
            objCells(21).Text = "F. Pago"

            objCells(22) = New TableCell
            objCells(22).CssClass = "trCabecera"
            objCells(22).Text = "Estado Pago"

            objCells(23) = New TableCell
            objCells(23).CssClass = "trCabecera"
            objCells(23).Text = "F. Cobro"

            objCells(24) = New TableCell
            objCells(24).CssClass = "trCabecera"
            objCells(24).Text = "Estado Cobro"

            objCells(25) = New TableCell
            objCells(25).CssClass = "trCabecera"
            objCells(25).Text = "Observación"

            objRow.Cells.AddRange(objCells)
            Table1.Rows.Add(objRow)



            'Consulta

            Dim objUtilNTx As New LUtilNTX
            Dim dtRegImpuestoMultaInmueble As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTx.ListarPipeline())
            'Dim FechaAprox As String
            For Each dr As Data.DataRow In dtRegImpuestoMultaInmueble.Rows
                Dim objCellsCuerpo(25) As TableCell

                objRow = New TableRow
                objRow.CssClass = "tdFilas"
                objRow.HorizontalAlign = HorizontalAlign.Center


                objCellsCuerpo(0) = New TableCell
                objCellsCuerpo(0).Text = GCCUtilitario.CheckStr(dr("NroContrato").ToString)

                objCellsCuerpo(1) = New TableCell
                objCellsCuerpo(1).Text = GCCUtilitario.CheckStr(dr("CUCliente").ToString)

                objCellsCuerpo(2) = New TableCell
                objCellsCuerpo(2).Text = GCCUtilitario.CheckStr(dr("RazonSocialNombre").ToString)

                objCellsCuerpo(3) = New TableCell
                objCellsCuerpo(3).Text = GCCUtilitario.CheckStr(dr("TipoDocumento").ToString)

                objCellsCuerpo(4) = New TableCell
                objCellsCuerpo(4).Text = GCCUtilitario.CheckStr(dr("NroDocumento").ToString)

                objCellsCuerpo(5) = New TableCell
                objCellsCuerpo(5).Text = GCCUtilitario.CheckStr(dr("Departamento").ToString)

                objCellsCuerpo(6) = New TableCell
                objCellsCuerpo(6).Text = GCCUtilitario.CheckStr(dr("Provincia").ToString)

                objCellsCuerpo(7) = New TableCell
                objCellsCuerpo(7).Text = GCCUtilitario.CheckStr(dr("Distrito").ToString)

                objCellsCuerpo(8) = New TableCell
                objCellsCuerpo(8).Text = GCCUtilitario.CheckStr(dr("Ubicacion").ToString)

                objCellsCuerpo(9) = New TableCell
                objCellsCuerpo(9).Text = GCCUtilitario.CheckStr(dr("Lote").ToString)

                objCellsCuerpo(10) = New TableCell
                objCellsCuerpo(10).Text = GCCUtilitario.CheckStr(dr("Periodo").ToString)

                objCellsCuerpo(11) = New TableCell
                objCellsCuerpo(11).Text = GCCUtilitario.CheckStr(dr("TotalAutovaluo").ToString)

                objCellsCuerpo(12) = New TableCell
                objCellsCuerpo(12).Text = GCCUtilitario.CheckStr(dr("TotalPredial").ToString)

                objCellsCuerpo(13) = New TableCell
                objCellsCuerpo(13).Text = GCCUtilitario.CheckStr(dr("CodigoPredio").ToString)

                objCellsCuerpo(14) = New TableCell
                objCellsCuerpo(14).Text = GCCUtilitario.CheckStr(dr("Autovaluo").ToString)

                objCellsCuerpo(15) = New TableCell
                objCellsCuerpo(15).Text = GCCUtilitario.CheckStr(dr("ImpuestoPredial").ToString)

                objCellsCuerpo(16) = New TableCell
                objCellsCuerpo(16).Text = GCCUtilitario.CheckStr(dr("Arbitrio").ToString)

                objCellsCuerpo(17) = New TableCell
                objCellsCuerpo(17).Text = GCCUtilitario.CheckStr(dr("Multa").ToString)

                objCellsCuerpo(18) = New TableCell
                objCellsCuerpo(18).Text = GCCUtilitario.CheckStr(dr("Fiscalizacion").ToString)

                objCellsCuerpo(19) = New TableCell
                objCellsCuerpo(19).Text = GCCUtilitario.CheckStr(dr("ImporteTotal").ToString)

                objCellsCuerpo(20) = New TableCell
                objCellsCuerpo(20).Text = GCCUtilitario.CheckStr(dr("PagoCliente").ToString)

                objCellsCuerpo(21) = New TableCell
                objCellsCuerpo(21).Text = GCCUtilitario.CheckStr(dr("FecPago").ToString)

                objCellsCuerpo(22) = New TableCell
                objCellsCuerpo(22).Text = GCCUtilitario.CheckStr(dr("EstadoPago").ToString)

                objCellsCuerpo(23) = New TableCell
                objCellsCuerpo(23).Text = GCCUtilitario.CheckStr(dr("FecCobro").ToString)

                objCellsCuerpo(24) = New TableCell
                objCellsCuerpo(24).Text = GCCUtilitario.CheckStr(dr("EstadoCobro").ToString)

                objCellsCuerpo(25) = New TableCell
                objCellsCuerpo(25).Text = GCCUtilitario.CheckStr(dr("Observacion").ToString)


                objRow.Cells.AddRange(objCellsCuerpo)
                Table1.Rows.Add(objRow)
            Next

            Try
                Dim mNomReporte As String = "rptImpuestoMultaInmueble.xls"
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

        Catch ex As Exception
            Throw ex
        Finally
            dtReporte = Nothing
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pLlenarTablaRegCompras(Now.Date.ToString)
    End Sub
End Class
