Imports GCC.UI
Imports GCC.LogicWS
Imports System.Data
Partial Class Reportes_frmRepLiquidarImpuestoVehicular
    Inherits GCCBase
    Private Sub pLlenarTablaRegCompras(ByVal pstrFecha As String, ByVal pstrCodigoImpuesto As String)
        Dim dtReporte As Data.DataTable
        Dim StrCodigo As String
        StrCodigo = Request.QueryString("strcodigo") '.Split(Convert.ToChar("|"))
        Dim objRow As TableRow
        Try
            lblTitle.Text = "REPORTE LIQUIDAR IMPUESTO VEHICULAR"
            'lblSubTitle.Text = String.Format("Fecha y Hora : {0}", pstrFecha)
            txtLote.Text = Request.QueryString("nrolote")
            txtTotal.Text = Request.QueryString("total")

            Dim objCells(12) As TableCell

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
            objCells(3).Text = "Tipo de Bien"

            objCells(4) = New TableCell
            objCells(4).CssClass = "trCabecera"
            objCells(4).Text = "Periodo"

            objCells(5) = New TableCell
            objCells(5).CssClass = "trCabecera"
            objCells(5).Text = "Nº Cuota"

            objCells(6) = New TableCell
            objCells(6).CssClass = "trCabecera"
            objCells(6).Text = "Distrito/Municipalidad"

            objCells(7) = New TableCell
            objCells(7).CssClass = "trCabecera"
            objCells(7).Text = "Placa"

            objCells(8) = New TableCell
            objCells(8).CssClass = "trCabecera"
            objCells(8).Text = "Marca"

            objCells(9) = New TableCell
            objCells(9).CssClass = "trCabecera"
            objCells(9).Text = "Modelo"

            objCells(10) = New TableCell
            objCells(10).CssClass = "trCabecera"
            objCells(10).Text = "Importe"

            objCells(11) = New TableCell
            objCells(11).CssClass = "trCabecera"
            objCells(11).Text = "Estado de Pago"

            objCells(12) = New TableCell
            objCells(12).CssClass = "trCabecera"
            objCells(12).Text = "Estado de Cobro"

            objRow.Cells.AddRange(objCells)
            Table1.Rows.Add(objRow)



            'Consulta

            Dim objImpuestoVehicularNTx As New LImpuestoVehicularNTX
            Dim dtRegCompra As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularNTx.ListarImpuestoVehicularReporteLiquidar(pstrCodigoImpuesto.ToString()))
            Dim FechaAprox As String
            Dim strCodImpuesto As String() = StrCodigo.Split(","c)
            For Each dr As Data.DataRow In dtRegCompra.Rows
                For i As Integer = 0 To strCodImpuesto.Length - 1
                    If dr("SecImpuesto").ToString = strCodImpuesto(i) Then
                        Dim objCellsCuerpo(12) As TableCell

                        objRow = New TableRow
                        objRow.CssClass = "tdFilas"
                        objRow.HorizontalAlign = HorizontalAlign.Center

                        objCellsCuerpo(0) = New TableCell
                        objCellsCuerpo(0).Text = GCCUtilitario.CheckStr(dr("CodSolicitudCredito").ToString)

                        objCellsCuerpo(1) = New TableCell
                        objCellsCuerpo(1).Text = GCCUtilitario.CheckStr(dr("CodUnico").ToString)

                        objCellsCuerpo(2) = New TableCell
                        objCellsCuerpo(2).Text = GCCUtilitario.CheckStr(dr("RazonSocial").ToString)

                        objCellsCuerpo(3) = New TableCell
                        objCellsCuerpo(3).Text = GCCUtilitario.CheckStr(dr("TipoBien").ToString)

                        objCellsCuerpo(4) = New TableCell
                        objCellsCuerpo(4).Text = GCCUtilitario.CheckStr(dr("Periodo").ToString)

                        objCellsCuerpo(5) = New TableCell
                        objCellsCuerpo(5).Text = GCCUtilitario.CheckStr(dr("NroCuota").ToString)

                        objCellsCuerpo(6) = New TableCell
                        objCellsCuerpo(6).Text = GCCUtilitario.CheckStr(dr("DistritoNombre").ToString)

                        objCellsCuerpo(7) = New TableCell
                        objCellsCuerpo(7).Text = GCCUtilitario.CheckStr(dr("Placa").ToString)

                        objCellsCuerpo(8) = New TableCell
                        objCellsCuerpo(8).Text = GCCUtilitario.CheckStr(dr("Marca").ToString)

                        objCellsCuerpo(9) = New TableCell
                        objCellsCuerpo(9).Text = GCCUtilitario.CheckStr(dr("Modelo").ToString).ToString()
                        'objCellsCuerpo(9).CssClass = "cost"

                        objCellsCuerpo(10) = New TableCell
                        objCellsCuerpo(10).Text = GCCUtilitario.CheckStr(dr("importe").ToString)
                        objCellsCuerpo(10).CssClass = "cost"

                        objCellsCuerpo(11) = New TableCell
                        objCellsCuerpo(11).Text = GCCUtilitario.CheckStr(dr("EstPago").ToString())

                        objCellsCuerpo(12) = New TableCell
                        objCellsCuerpo(12).Text = GCCUtilitario.CheckStr(dr("EstCobro").ToString).ToString()
                        'objCellsCuerpo(12).CssClass = "cost"

                        objRow.Cells.AddRange(objCellsCuerpo)
                        Table1.Rows.Add(objRow)
                    End If
                Next
            Next

            Try
                Dim mNomReporte As String = "rptLiquidarImpuestoVehicular.xls"
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
        Dim strCodigo As String = Request.QueryString("strcodigo")
        txtLote.Text = Request.QueryString("nrolote")
        txtTotal.Text = Request.QueryString("total")
        pLlenarTablaRegCompras(Now.Date.ToString, strCodigo)

    End Sub
End Class
