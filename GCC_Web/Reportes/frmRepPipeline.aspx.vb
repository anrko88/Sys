Imports GCC.UI
Imports GCC.LogicWS
Imports System.Data
Partial Class Reportes_frmRepPipeline
    Inherits GCCBase

    Private Sub pLlenarTablaRegCompras(ByVal pstrFecha As String)
        Dim dtReporte As Data.DataTable
        Dim StrCodigo() As String
        StrCodigo = Request.QueryString("strcodigo").Split(Convert.ToChar("|"))
        Dim objRow As TableRow
        Try
            lblTitle.Text = "REPORTE PIPELINE"
            'lblSubTitle.Text = String.Format("Fecha y Hora : {0}", pstrFecha)

            Dim objCells(22) As TableCell

            'Cabeceras Grilla - Fila 1
            objRow = New TableRow
            objRow.CssClass = "trCabecera"
            objRow.HorizontalAlign = HorizontalAlign.Center

            objCells(0) = New TableCell
            objCells(0).CssClass = "trCabecera"
            objCells(0).Text = "Tipo Persona"

            objCells(1) = New TableCell
            objCells(1).CssClass = "trCabecera"
            objCells(1).Text = "Tipo Documento"

            objCells(2) = New TableCell
            objCells(2).CssClass = "trCabecera"
            objCells(2).Text = "Numero de Documento"

            objCells(3) = New TableCell
            objCells(3).CssClass = "trCabecera"
            objCells(3).Text = "Nombre del cliente"

            objCells(4) = New TableCell
            objCells(4).CssClass = "trCabecera"
            objCells(4).Text = "Banca"

            objCells(5) = New TableCell
            objCells(5).CssClass = "trCabecera"
            objCells(5).Text = "Ejecutivo Leasing"

            objCells(6) = New TableCell
            objCells(6).CssClass = "trCabecera"
            objCells(6).Text = "Tipo de Leasing"

            objCells(7) = New TableCell
            objCells(7).CssClass = "trCabecera"
            objCells(7).Text = "Activo"

            objCells(8) = New TableCell
            objCells(8).CssClass = "trCabecera"
            objCells(8).Text = "Seguro"

            objCells(9) = New TableCell
            objCells(9).CssClass = "trCabecera"
            objCells(9).Text = "IMPORTE EN RIESGOS (neto de IGV y cuota inicial)"

            objCells(10) = New TableCell
            objCells(10).CssClass = "trCabecera"
            objCells(10).Text = "Moneda"

            objCells(11) = New TableCell
            objCells(11).CssClass = "trCabecera"
            objCells(11).Text = "Fecha de Cotización"

            objCells(12) = New TableCell
            objCells(12).CssClass = "trCabecera"
            objCells(12).Text = "Spread%"

            objCells(13) = New TableCell
            objCells(13).CssClass = "trCabecera"
            objCells(13).Text = "PROVEEDOR"

            objCells(14) = New TableCell
            objCells(14).CssClass = "trCabecera"
            objCells(14).Text = "OPCIÓN DE COMPRA"

            objCells(15) = New TableCell
            objCells(15).CssClass = "trCabecera"
            objCells(15).Text = "CUOTA INICIAL"

            objCells(16) = New TableCell
            objCells(16).CssClass = "trCabecera"
            objCells(16).Text = "COM DE ACTIVACIÓN"

            objCells(17) = New TableCell
            objCells(17).CssClass = "trCabecera"
            objCells(17).Text = "FECHA DE APROBACIÓN DE COTIZACIÓN - CLIENTE"

            objCells(18) = New TableCell
            objCells(18).CssClass = "trCabecera"
            objCells(18).Text = "CU CLIENTE"

            objCells(19) = New TableCell
            objCells(19).CssClass = "trCabecera"
            objCells(19).Text = "ESTADO"

            objCells(20) = New TableCell
            objCells(20).CssClass = "trCabecera"
            objCells(20).Text = "FECHA APROX DESEMBOLSO"

            objCells(21) = New TableCell
            objCells(21).CssClass = "trCabecera"
            objCells(21).Text = "MOTIVO DE DEMORA"

            objCells(22) = New TableCell
            objCells(22).CssClass = "trCabecera"
            objCells(22).Text = "NO. LEASING"

            

            objRow.Cells.AddRange(objCells)
            Table1.Rows.Add(objRow)



            'Consulta
          
            Dim objUtilNTx As New LUtilNTX
            Dim dtRegCompra As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTx.ListarPipeline())
            Dim FechaAprox As String
            For Each dr As Data.DataRow In dtRegCompra.Rows
                For i As Integer = 0 To StrCodigo.Length - 1
                    If dr("CodigoCotizacion").ToString = StrCodigo(i) Then
                        Dim objCellsCuerpo(22) As TableCell

                        objRow = New TableRow
                        objRow.CssClass = "tdFilas"
                        objRow.HorizontalAlign = HorizontalAlign.Center

                        objCellsCuerpo(0) = New TableCell
                        objCellsCuerpo(0).Text = GCCUtilitario.CheckStr(dr("TipoPersona").ToString)

                        objCellsCuerpo(1) = New TableCell
                        objCellsCuerpo(1).Text = GCCUtilitario.CheckStr(dr("TipoDocumento").ToString)

                        objCellsCuerpo(2) = New TableCell
                        objCellsCuerpo(2).Text = GCCUtilitario.CheckStr(dr("NumeroDocumento").ToString)

                        objCellsCuerpo(3) = New TableCell
                        objCellsCuerpo(3).Text = GCCUtilitario.CheckStr(dr("NombreCliente").ToString)

                        objCellsCuerpo(4) = New TableCell
                        objCellsCuerpo(4).Text = GCCUtilitario.CheckStr(dr("Banca").ToString)

                        objCellsCuerpo(5) = New TableCell
                        objCellsCuerpo(5).Text = GCCUtilitario.CheckStr(dr("EjecutivoLeasing").ToString)

                        objCellsCuerpo(6) = New TableCell
                        objCellsCuerpo(6).Text = GCCUtilitario.CheckStr(dr("TipoLeasing").ToString)

                        objCellsCuerpo(7) = New TableCell
                        objCellsCuerpo(7).Text = GCCUtilitario.CheckStr(dr("ClasificacionBien").ToString)

                        objCellsCuerpo(8) = New TableCell
                        objCellsCuerpo(8).Text = GCCUtilitario.CheckStr(dr("TipoSeguro").ToString)

                        objCellsCuerpo(9) = New TableCell
                        objCellsCuerpo(9).Text = GCCUtilitario.CheckDecimal(dr("RiesgoNeto").ToString).ToString(GCCConstante.C_FormatMiles)
                        objCellsCuerpo(9).CssClass = "cost"

                        objCellsCuerpo(10) = New TableCell
                        objCellsCuerpo(10).Text = GCCUtilitario.CheckStr(dr("Moneda").ToString)

                        objCellsCuerpo(11) = New TableCell
                        objCellsCuerpo(11).Text = GCCUtilitario.CheckStr(dr("FechaCarta").ToString())

                        objCellsCuerpo(12) = New TableCell
                        objCellsCuerpo(12).Text = GCCUtilitario.CheckDecimal(dr("Spread").ToString).ToString(GCCConstante.C_FormatMiles)
                        objCellsCuerpo(12).CssClass = "cost"

                        objCellsCuerpo(13) = New TableCell
                        objCellsCuerpo(13).Text = GCCUtilitario.CheckStr(dr("DesProveedor").ToString)

                        objCellsCuerpo(14) = New TableCell
                        objCellsCuerpo(14).Text = GCCUtilitario.CheckDecimal(dr("OpcionCompra").ToString).ToString(GCCConstante.C_FormatMiles)
                        objCellsCuerpo(14).CssClass = "cost"

                        objCellsCuerpo(15) = New TableCell
                        objCellsCuerpo(15).Text = GCCUtilitario.CheckDecimal(dr("CuotaInicial").ToString).ToString(GCCConstante.C_FormatMiles)
                        objCellsCuerpo(15).CssClass = "cost"

                        objCellsCuerpo(16) = New TableCell
                        objCellsCuerpo(16).Text = GCCUtilitario.CheckDecimal(dr("ComisionActivacion").ToString).ToString(GCCConstante.C_FormatMiles)
                        objCellsCuerpo(16).CssClass = "cost"

                        objCellsCuerpo(17) = New TableCell
                        objCellsCuerpo(17).Text = GCCUtilitario.CheckStr(dr("FechaAprobacionCotizacion").ToString)

                        objCellsCuerpo(18) = New TableCell
                        objCellsCuerpo(18).Text = GCCUtilitario.CheckStr(dr("CodigoUnico").ToString)
                        objCellsCuerpo(18).CssClass = "CssText"

                        objCellsCuerpo(19) = New TableCell
                        objCellsCuerpo(19).Text = GCCUtilitario.CheckStr(dr("Estado").ToString)

                        objCellsCuerpo(20) = New TableCell

                        Select Case GCCUtilitario.CheckStr(dr("FechaAproximada").ToString)
                            Case 1 : FechaAprox = MonthName(Now.Date.Month, False)
                            Case 2 : FechaAprox = MonthName(Now.Date.Month + 1, False)
                            Case 3 : FechaAprox = (Now.Year + 1).ToString()
                            Case "" : FechaAprox = ""
                        End Select
                        objCellsCuerpo(20).Text = FechaAprox.ToString()

                        objCellsCuerpo(21) = New TableCell
                        objCellsCuerpo(21).Text = GCCUtilitario.CheckStr(dr("Motivo").ToString)

                        objCellsCuerpo(22) = New TableCell
                        objCellsCuerpo(22).Text = GCCUtilitario.CheckStr(dr("CodigoContrato").ToString)


                        objRow.Cells.AddRange(objCellsCuerpo)
                        Table1.Rows.Add(objRow)
                    End If
                Next
            Next

            Try
                Dim mNomReporte As String = "rptPipeline.xls"
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
