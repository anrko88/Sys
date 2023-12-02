Imports GCC.UI
Imports GCC.LogicWS
Imports GCC.Entity
Imports System.Data
Partial Class Reportes_frmRepDetalleBien
    Inherits GCCBase

    Private Sub pLlenarReporte(ByVal pdtmFAI As DateTime, _
                               ByVal pdtmFAF As DateTime)
        Dim dtReporte As Data.DataTable
        Dim objRow As TableRow
        Try


            Dim objCells(18) As TableCell

            'Cabeceras Grilla - Fila 1
            objRow = New TableRow
            objRow.CssClass = "trCabecera"
            objRow.HorizontalAlign = HorizontalAlign.Center

            objCells(0) = New TableCell
            objCells(0).CssClass = "trCabecera"
            objCells(0).Text = "Nº"

            objCells(1) = New TableCell
            objCells(1).CssClass = "trCabecera"
            objCells(1).Text = "N° Crédito"

            objCells(2) = New TableCell
            objCells(2).CssClass = "trCabecera"
            objCells(2).Text = "Descripción Bien"

            objCells(3) = New TableCell
            objCells(3).CssClass = "trCabecera"
            objCells(3).Text = "RUC Proveedor"

            objCells(4) = New TableCell
            objCells(4).CssClass = "trCabecera"
            objCells(4).Text = "Moneda Crédito"

            objCells(5) = New TableCell
            objCells(5).CssClass = "trCabecera"
            objCells(5).Text = "Apellido Paterno"

            objCells(6) = New TableCell
            objCells(6).CssClass = "trCabecera"
            objCells(6).Text = "Apellido Materno"

            objCells(7) = New TableCell
            objCells(7).CssClass = "trCabecera"
            objCells(7).Text = "Nombres"

            objCells(8) = New TableCell
            objCells(8).CssClass = "trCabecera"
            objCells(8).Text = "Razón Social"

            objCells(9) = New TableCell
            objCells(9).CssClass = "trCabecera"
            objCells(9).Text = "N°"

            objCells(10) = New TableCell
            objCells(10).CssClass = "trCabecera"
            objCells(10).Text = "Unidad de Medida"

            objCells(11) = New TableCell
            objCells(11).CssClass = "trCabecera"
            objCells(11).Text = "Descripción del Bien"

            objCells(12) = New TableCell
            objCells(12).CssClass = "trCabecera"
            objCells(12).Text = "Moneda Factura"

            objCells(13) = New TableCell
            objCells(13).CssClass = "trCabecera"
            objCells(13).Text = "Clasificación del Bien"

            objCells(14) = New TableCell
            objCells(14).CssClass = "trCabecera"
            objCells(14).Text = "Suma de Capital"

            objCells(15) = New TableCell
            objCells(15).CssClass = "trCabecera"
            objCells(15).Text = "TC"

            objCells(16) = New TableCell
            objCells(16).CssClass = "trCabecera"
            objCells(16).Text = "Facturas Importe Moneda Crédito"

            objCells(17) = New TableCell
            objCells(17).CssClass = "trCabecera"
            objCells(17).Text = "Suma de Total De Bienes"

            objCells(18) = New TableCell
            objCells(18).CssClass = "trCabecera"
            objCells(18).Text = "Suma de Ruc Del Informante"

            objRow.Cells.AddRange(objCells)
            tbReporte.Rows.Add(objRow)

            'Consulta            
            Dim objContrato As New LContratoNTx
            Dim dtListado As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContrato.ListadoReporteDetalleBien(pdtmFAI, pdtmFAF))
            Dim intContador As Integer = 1
            Dim strContador As String = String.Empty
            Dim strNroContrato As String = String.Empty
            Dim strNroContratoRep As String = String.Empty
            For Each dr As Data.DataRow In dtListado.Rows

                ' intContador = intContador + 1
                strNroContratoRep = String.Empty
                strContador = String.Empty
                If strNroContrato = String.Empty Then
                    strContador = intContador
                    strNroContrato = dr("CodSolicitudCredito").ToString
                    strNroContratoRep = dr("CodSolicitudCredito").ToString
                    strContador = intContador.ToString()
                End If


                If strNroContrato <> dr("CodSolicitudCredito").ToString Then
                    strNroContrato = dr("CodSolicitudCredito").ToString
                    strNroContratoRep = dr("CodSolicitudCredito").ToString
                    intContador = intContador + 1
                    strContador = intContador.ToString()
                End If

                Dim objCellsCuerpo(18) As TableCell

                objRow = New TableRow
                objRow.CssClass = "tdFilas"
                objRow.HorizontalAlign = HorizontalAlign.Center

                objCellsCuerpo(0) = New TableCell
                objCellsCuerpo(0).Text = strContador

                objCellsCuerpo(1) = New TableCell
                objCellsCuerpo(1).Text = strNroContratoRep

                objCellsCuerpo(2) = New TableCell
                objCellsCuerpo(2).Text = GCCUtilitario.CheckStr(dr("Comentario").ToString).ToUpper()
                objCellsCuerpo(2).CssClass = "CssLeft"

                objCellsCuerpo(3) = New TableCell
                objCellsCuerpo(3).Text = GCCUtilitario.CheckStr(dr("RUC").ToString)

                objCellsCuerpo(4) = New TableCell
                objCellsCuerpo(4).Text = GCCUtilitario.CheckStr(dr("NombreMoneda").ToString)

                objCellsCuerpo(5) = New TableCell
                objCellsCuerpo(5).Text = String.Empty
                objCellsCuerpo(5).CssClass = "CssLeft"

                objCellsCuerpo(6) = New TableCell
                objCellsCuerpo(6).Text = String.Empty
                objCellsCuerpo(6).CssClass = "CssLeft"

                objCellsCuerpo(7) = New TableCell
                objCellsCuerpo(7).Text = String.Empty
                objCellsCuerpo(7).CssClass = "CssLeft"

                objCellsCuerpo(8) = New TableCell
                objCellsCuerpo(8).Text = GCCUtilitario.CheckStr(dr("NombreInstitucion").ToString)
                objCellsCuerpo(8).CssClass = "CssLeft"

                objCellsCuerpo(9) = New TableCell
                objCellsCuerpo(9).Text = IIf(GCCUtilitario.CheckStr(dr("CantidadBienes").ToString) = "0", "", GCCUtilitario.CheckStr(dr("CantidadBienes").ToString))

                objCellsCuerpo(10) = New TableCell
                objCellsCuerpo(10).Text = GCCUtilitario.CheckStr(dr("UnidadMedida").ToString)

                objCellsCuerpo(11) = New TableCell
                objCellsCuerpo(11).Text = GCCUtilitario.CheckStr(dr("Comentario").ToString).ToUpper()
                objCellsCuerpo(11).CssClass = "CssLeft"

                objCellsCuerpo(12) = New TableCell
                objCellsCuerpo(12).Text = GCCUtilitario.CheckStr(dr("NombreMonedaDoc").ToString)

                objCellsCuerpo(13) = New TableCell
                objCellsCuerpo(13).Text = GCCUtilitario.CheckStr(dr("ClasificacionBien").ToString)

                objCellsCuerpo(14) = New TableCell
                objCellsCuerpo(14).Text = GCCUtilitario.CheckStr(dr("Total").ToString)
                objCellsCuerpo(14).CssClass = "cost"

                objCellsCuerpo(15) = New TableCell
                objCellsCuerpo(15).Text = IIf(GCCUtilitario.CheckDecimal(dr("TC").ToString) = 0, "", GCCUtilitario.CheckStr(dr("TC").ToString))
                objCellsCuerpo(15).CssClass = "cost"

                objCellsCuerpo(16) = New TableCell
                objCellsCuerpo(16).Text = GCCUtilitario.CheckStr(dr("TotalConvertido").ToString)
                objCellsCuerpo(16).CssClass = "cost"

                objCellsCuerpo(17) = New TableCell
                objCellsCuerpo(17).Text = GCCUtilitario.CheckStr(dr("TotalBienes").ToString)
                objCellsCuerpo(17).CssClass = "cost"

                objCellsCuerpo(18) = New TableCell
                objCellsCuerpo(18).Text = GCCUtilitario.CheckStr(dr("RUCBanco").ToString).ToString()

                objRow.Cells.AddRange(objCellsCuerpo)
                tbReporte.Rows.Add(objRow)
                '   End If
                'Next
            Next

            Try
                Dim mNomReporte As String = "rptDetalleBien.xls"
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
                                    ".CssLeft { text-align:left; }" & _
                                    ".cost{mso-number-format:'#,##0.00';text-align:right; }" & _
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
        Dim strFAI As String = IIf(Request.QueryString("pfai") Is Nothing, "1900-01-01", Request.QueryString("pfai"))
        Dim strFAF As String = IIf(Request.QueryString("pfaf") Is Nothing, "1900-01-01", Request.QueryString("pfaf"))

        Dim strRuta As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer")
        Dim strLogo As String = "logo.png"
        imgLogo.Src = IIf(strRuta.EndsWith("\"), strRuta, strRuta & "\") & strLogo

        pLlenarReporte(strFAI, strFAF)
    End Sub
End Class
