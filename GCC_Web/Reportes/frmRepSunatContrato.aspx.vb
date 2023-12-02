Imports GCC.UI
Imports GCC.LogicWS
Imports System.Data
Partial Class Reportes_frmRepSunatContrato
    Inherits GCCBase
    Private Sub pLlenarReporte(ByVal pdtmFCI As DateTime, _
                               ByVal pdtmFCF As DateTime, _
                               ByVal pdtmFAI As DateTime, _
                               ByVal pdtmFAF As DateTime)
        Dim dtReporte As Data.DataTable              
        Dim objRow As TableRow
        Try


            Dim objCells(22) As TableCell

            'Cabeceras Grilla - Fila 1
            objRow = New TableRow
            objRow.CssClass = "td-grid-header"
            objRow.HorizontalAlign = HorizontalAlign.Center

            objCells(0) = New TableCell
            objCells(0).CssClass = "trCabecera"
            objCells(0).Text = "Nº"

            objCells(1) = New TableCell
            objCells(1).CssClass = "trCabecera"
            objCells(1).Text = "RUC Arrendador"

            objCells(2) = New TableCell
            objCells(2).CssClass = "trCabecera"
            objCells(2).Text = "Tipo de Financiamiento"

            objCells(3) = New TableCell
            objCells(3).CssClass = "trCabecera"
            objCells(3).Text = "Fecha Celebración Contrato"

            objCells(4) = New TableCell
            objCells(4).CssClass = "trCabecera"
            objCells(4).Text = "Fecha de Activación"

            objCells(5) = New TableCell
            objCells(5).CssClass = "trCabecera"
            objCells(5).Text = "Código Contrato"

            objCells(6) = New TableCell
            objCells(6).CssClass = "trCabecera"
            objCells(6).Text = "Cant Meses Crédito"

            objCells(7) = New TableCell
            objCells(7).CssClass = "trCabecera"
            objCells(7).Text = "Cant Días Crédito"

            objCells(8) = New TableCell
            objCells(8).CssClass = "trCabecera"
            objCells(8).Text = "Tipo Documento Arrendatario"

            objCells(9) = New TableCell
            objCells(9).CssClass = "trCabecera"
            objCells(9).Text = "N° Documento  Arrendatario"

            objCells(10) = New TableCell
            objCells(10).CssClass = "trCabecera"
            objCells(10).Text = "Ap Paterno Arrendatario"

            objCells(11) = New TableCell
            objCells(11).CssClass = "trCabecera"
            objCells(11).Text = "Ap Materno Arrendatario"

            objCells(12) = New TableCell
            objCells(12).CssClass = "trCabecera"
            objCells(12).Text = "Nombre Arrendatario"

            objCells(13) = New TableCell
            objCells(13).CssClass = "trCabecera"
            objCells(13).Text = "Razón Social Arrendatario"

            objCells(14) = New TableCell
            objCells(14).CssClass = "trCabecera"
            objCells(14).Text = "Moneda Contrato"

            objCells(15) = New TableCell
            objCells(15).CssClass = "trCabecera"
            objCells(15).Text = "Monto Capital Inicial"

            objCells(16) = New TableCell
            objCells(16).CssClass = "trCabecera"
            objCells(16).Text = "Interes Total Inicial"

            objCells(17) = New TableCell
            objCells(17).CssClass = "trCabecera"
            objCells(17).Text = "IGV Total Inicial"

            objCells(18) = New TableCell
            objCells(18).CssClass = "trCabecera"
            objCells(18).Text = "Monto Total Contrato"

            objCells(19) = New TableCell
            objCells(19).CssClass = "trCabecera"
            objCells(19).Text = "Opción Compra"

            objCells(20) = New TableCell
            objCells(20).CssClass = "trCabecera"
            objCells(20).Text = "Monto Cuota"

            objCells(21) = New TableCell
            objCells(21).CssClass = "trCabecera"
            objCells(21).Text = "Comentario Cuota Variable"

            objCells(22) = New TableCell
            objCells(22).CssClass = "trCabecera"
            objCells(22).Text = "Fecha Vencimiento Cuota"

            objRow.Cells.AddRange(objCells)
            tbReporte.Rows.Add(objRow)

            'Consulta

            Dim objContrato As New LContratoNTx
            Dim dtListado As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContrato.ListadoReporteSunatContratos(pdtmFCI, pdtmFCF, pdtmFAI, pdtmFAF))
            Dim intContador As Integer = 0
            Dim strEstilo As String = String.Empty
            'Dim strCodImpuesto As String() = StrCodigo.Split(","c)
            For Each dr As Data.DataRow In dtListado.Rows
                '    For i As Integer = 0 To strCodImpuesto.Length - 1
                'If dr("SecImpuesto").ToString = strCodImpuesto(i) Then
                intContador = intContador + 1
                Dim objCellsCuerpo(22) As TableCell

                objRow = New TableRow
                If intContador Mod 2 = 0 Then
                    strEstilo = "td-grid-blanco"
                Else
                    strEstilo = "td-grid-gris"
                End If


                objRow.CssClass = strEstilo
                objRow.HorizontalAlign = HorizontalAlign.Center

                objCellsCuerpo(0) = New TableCell
                objCellsCuerpo(0).Text = intContador.ToString()

                objCellsCuerpo(1) = New TableCell
                objCellsCuerpo(1).Text = GCCUtilitario.CheckStr(dr("DocumentoArrendador").ToString)
                objCellsCuerpo(1).CssClass = "CssText"

                objCellsCuerpo(2) = New TableCell
                objCellsCuerpo(2).Text = GCCUtilitario.CheckStr(dr("TipoContrato").ToString).ToUpper()

                objCellsCuerpo(3) = New TableCell
                objCellsCuerpo(3).Text = GCCUtilitario.CheckStr(dr("FechaCelebracion").ToString)

                objCellsCuerpo(4) = New TableCell
                objCellsCuerpo(4).Text = GCCUtilitario.CheckStr(dr("FechaActivacionContrato").ToString)

                objCellsCuerpo(5) = New TableCell
                objCellsCuerpo(5).Text = GCCUtilitario.CheckStr(dr("CodSolicitudCredito").ToString)

                objCellsCuerpo(6) = New TableCell
                objCellsCuerpo(6).Text = GCCUtilitario.CheckStr(dr("CantidadMeses").ToString)

                objCellsCuerpo(7) = New TableCell
                objCellsCuerpo(7).Text = GCCUtilitario.CheckStr(dr("CantidadDias").ToString)

                objCellsCuerpo(8) = New TableCell
                objCellsCuerpo(8).Text = GCCUtilitario.CheckStr(dr("NombreTipoDocumento").ToString).ToUpper()

                objCellsCuerpo(9) = New TableCell
                objCellsCuerpo(9).Text = GCCUtilitario.CheckStr(dr("NumeroDocumento").ToString)


                objCellsCuerpo(10) = New TableCell
                objCellsCuerpo(10).Text = String.Empty

                objCellsCuerpo(11) = New TableCell
                objCellsCuerpo(11).Text = String.Empty

                objCellsCuerpo(12) = New TableCell
                objCellsCuerpo(12).Text = String.Empty

                objCellsCuerpo(13) = New TableCell
                objCellsCuerpo(13).Text = GCCUtilitario.CheckStr(dr("ClienteRazonSocial").ToString).ToString()
                objCellsCuerpo(13).CssClass = "CssLeft"

                objCellsCuerpo(14) = New TableCell
                objCellsCuerpo(14).Text = GCCUtilitario.CheckStr(dr("NombreMoneda").ToString).ToString().ToUpper()

                objCellsCuerpo(15) = New TableCell
                objCellsCuerpo(15).Text = GCCUtilitario.CheckStr(dr("MontoFinanciamiento").ToString).ToString()
                objCellsCuerpo(15).CssClass = "cost"

                objCellsCuerpo(16) = New TableCell
                objCellsCuerpo(16).Text = GCCUtilitario.CheckStr(dr("TotalInteres").ToString).ToString()
                objCellsCuerpo(16).CssClass = "cost"

                objCellsCuerpo(17) = New TableCell
                objCellsCuerpo(17).Text = GCCUtilitario.CheckStr(dr("ValorVentaIGV").ToString).ToString()
                objCellsCuerpo(17).CssClass = "cost"

                objCellsCuerpo(18) = New TableCell
                objCellsCuerpo(18).Text = GCCUtilitario.CheckStr(dr("TotalContrato").ToString).ToString()
                objCellsCuerpo(18).CssClass = "cost"

                objCellsCuerpo(19) = New TableCell
                objCellsCuerpo(19).Text = GCCUtilitario.CheckStr(dr("OpcionCompra").ToString).ToString()
                objCellsCuerpo(19).CssClass = "cost"

                objCellsCuerpo(20) = New TableCell
                objCellsCuerpo(20).Text = GCCUtilitario.CheckStr(dr("TotalPagar").ToString).ToString()
                objCellsCuerpo(20).CssClass = "cost"

                objCellsCuerpo(21) = New TableCell
                objCellsCuerpo(21).Text = GCCUtilitario.CheckStr(dr("ComentarioCuotaVariable").ToString).ToString()

                objCellsCuerpo(22) = New TableCell
                objCellsCuerpo(22).Text = GCCUtilitario.CheckStr(dr("FechaVencimiento").ToString).ToString()

                objRow.Cells.AddRange(objCellsCuerpo)
                tbReporte.Rows.Add(objRow)
                '   End If
                'Next
            Next

            Try
                Dim mNomReporte As String = "rptSunatContrato.xls"
                Dim style As String = "<style type='text/css'> " & _
                                        ".td-grid-header { " & _
                                        "background-color: #6ab952;" & _
                                        "font-family: Arial;" & _
                                        "font-size: 8pt;" & _
                                        "color: #ffffff;" & _
                                        "text-align:center;" & _
                                        "z-index: 10;" & _
                                        "font(-weight) : bold()" & _
                                        "}" & _
                                        ".td-grid-blanco{" & _
                                        "background-color: #ffffff;" & _
                                        "height: 15px;" & _
                                        "padding: 3px;" & _
                                        "font-family: Arial;" & _
                                        "font-size: 8pt;" & _
                                        "color: #404a55;" & _
                                        "text-align:center;" & _
                                        "}" & _
                                        ".td-grid-gris {" & _
                                        "background-color: #f0f0f0;" & _
                                        "height: 15px;" & _
                                        "padding: 3px;" & _
                                        "font-family: Arial;" & _
                                        "font-size: 8pt;" & _
                                        "color: #404a55;" & _
                                        "text-align:center;" & _
                                        "}" & _
                                        ".CssText { mso-number-format:\@; }" & _
                                        ".CssLeft { text-align:left; }" & _
                                        ".cost{mso-number-format:'#,##0.00'; text-align:right;}" & _
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
        Dim strFCI As String = IIf(Request.QueryString("pfcci") Is Nothing, "1900-01-01", Request.QueryString("pfcci"))
        Dim strFCF As String = IIf(Request.QueryString("pfccf") Is Nothing, "1900-01-01", Request.QueryString("pfccf"))
        Dim strFAI As String = IIf(Request.QueryString("pfai") Is Nothing, "1900-01-01", Request.QueryString("pfai"))
        Dim strFAF As String = IIf(Request.QueryString("pfaf") Is Nothing, "1900-01-01", Request.QueryString("pfaf"))

        Dim dtmFCI As DateTime = GCCUtilitario.CheckDate(strFCI)
        Dim dtmFCF As DateTime = GCCUtilitario.CheckDate(strFCF)
        Dim dtmFAI As DateTime = GCCUtilitario.CheckDate(strFAI)
        Dim dtmFAF As DateTime = GCCUtilitario.CheckDate(strFAF)

        tdFCCD.InnerHtml = GCCUtilitario.fSetearFecha(dtmFCI)
        tdFCCD.InnerHtml = GCCUtilitario.fSetearFecha(dtmFCF)
        tdFACD.InnerHtml = GCCUtilitario.fSetearFecha(dtmFAI)
        tdFACH.InnerHtml = GCCUtilitario.fSetearFecha(dtmFAF)

        Dim strRuta As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer")
        Dim strLogo As String = "logo.png"
        imgLogo.Src = IIf(strRuta.EndsWith("\"), strRuta, strRuta & "\") & strLogo

        pLlenarReporte(dtmFCI, dtmFCF, dtmFAI, dtmFAF)

    End Sub
End Class
