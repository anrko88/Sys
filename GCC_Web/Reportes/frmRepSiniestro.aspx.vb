Imports GCC.UI
Imports GCC.LogicWS
Imports GCC.Entity
Imports System.Data
Partial Class Reportes_frmRepSiniestro
    Inherits GCCBase

    Private Sub pLlenarReporte(ByVal pstrFiltro As String)
        Dim dtReporte As Data.DataTable
        Dim objRow As TableRow
        Try


            Dim objCells(24) As TableCell

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
            objCells(3).Text = "Estado del Contrato"

            objCells(4) = New TableCell
            objCells(4).CssClass = "trCabecera"
            objCells(4).Text = "Clasificación del Bien"

            objCells(5) = New TableCell
            objCells(5).CssClass = "trCabecera"
            objCells(5).Text = "Tipo de Bien"

            objCells(6) = New TableCell
            objCells(6).CssClass = "trCabecera"
            objCells(6).Text = "Nº Siniestro"

            objCells(7) = New TableCell
            objCells(7).CssClass = "trCabecera"
            objCells(7).Text = "F. Conocimiento Banco"

            objCells(8) = New TableCell
            objCells(8).CssClass = "trCabecera"
            objCells(8).Text = "F. Conocimiento Leasing"

            objCells(9) = New TableCell
            objCells(9).CssClass = "trCabecera"
            objCells(9).Text = "Fecha Siniestro"

            objCells(10) = New TableCell
            objCells(10).CssClass = "trCabecera"
            objCells(10).Text = "Tipo"

            objCells(11) = New TableCell
            objCells(11).CssClass = "trCabecera"
            objCells(11).Text = "F. Última Actualización"

            objCells(12) = New TableCell
            objCells(12).CssClass = "trCabecera"
            objCells(12).Text = "Situación"

            objCells(13) = New TableCell
            objCells(13).CssClass = "trCabecera"
            objCells(13).Text = "Modificación de Contrato"

            objCells(14) = New TableCell
            objCells(14).CssClass = "trCabecera"
            objCells(14).Text = "F. Aplicación"

            objCells(15) = New TableCell
            objCells(15).CssClass = "trCabecera"
            objCells(15).Text = "Aplicación"

            objCells(16) = New TableCell
            objCells(16).CssClass = "trCabecera"
            objCells(16).Text = "F. Descargo Municipal"

            objCells(17) = New TableCell
            objCells(17).CssClass = "trCabecera"
            objCells(17).Text = "Seguro"

            objCells(18) = New TableCell
            objCells(18).CssClass = "trCabecera"
            objCells(18).Text = "Estado del Bien"

            objCells(19) = New TableCell
            objCells(19).CssClass = "trCabecera"
            objCells(19).Text = "Nº Póliza"

            objCells(20) = New TableCell
            objCells(20).CssClass = "trCabecera"
            objCells(20).Text = "Tipo de Póliza"

            objCells(21) = New TableCell
            objCells(21).CssClass = "trCabecera"
            objCells(21).Text = "F. Rec. Indemnización"

            objCells(22) = New TableCell
            objCells(22).CssClass = "trCabecera"
            objCells(22).Text = "Moneda Indemnización"

            objCells(23) = New TableCell
            objCells(23).CssClass = "trCabecera"
            objCells(23).Text = "Monto Indemnización"

            objCells(24) = New TableCell
            objCells(24).CssClass = "trCabecera"
            objCells(24).Text = "Banco que Emite el Cheque"

            objRow.Cells.AddRange(objCells)
            tbReporte.Rows.Add(objRow)

            'Consulta
            Dim arrFiltro() As String = pstrFiltro.Split("|")
            Dim strESiniestro As String = String.Empty
            If pstrFiltro <> String.Empty Then
                'Inicializa Objeto
                Dim objESiniestro As New ESiniestro

                With objESiniestro
                    .NroContrato = arrFiltro(0)
                    .EstadoContrato = arrFiltro(1)
                    .CUCliente = IIf(arrFiltro(2) = String.Empty, String.Empty, arrFiltro(2).Trim.PadLeft(10, "0"c))
                    .TipoDocumento = arrFiltro(3)
                    .NroDocumento = arrFiltro(4)
                    .RazonSocial = arrFiltro(5)
                    .ClasificacionBien = arrFiltro(6)
                    .Placa = arrFiltro(7)
                    .NroMotor = arrFiltro(8)
                    .TipoBien = arrFiltro(9)
                    .Ubicacion = arrFiltro(10)
                End With
                strESiniestro = GCCUtilitario.SerializeObject(Of ESiniestro)(objESiniestro)
            End If

            Dim objSiniestroNTx As New LSiniestroNTx
            Dim dtListado As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objSiniestroNTx.ListadoReporteSiniestro(strESiniestro))
            'Dim strCodImpuesto As String() = StrCodigo.Split(","c)
            For Each dr As Data.DataRow In dtListado.Rows
                '    For i As Integer = 0 To strCodImpuesto.Length - 1
                'If dr("SecImpuesto").ToString = strCodImpuesto(i) Then
                Dim objCellsCuerpo(24) As TableCell

                objRow = New TableRow
                objRow.CssClass = "tdFilas"
                objRow.HorizontalAlign = HorizontalAlign.Center

                objCellsCuerpo(0) = New TableCell
                objCellsCuerpo(0).Text = GCCUtilitario.CheckStr(dr("CodSolicitudCredito").ToString)

                objCellsCuerpo(1) = New TableCell
                objCellsCuerpo(1).Text = GCCUtilitario.CheckStr(dr("CodUnico").ToString).PadLeft(10, "0")
                objCellsCuerpo(1).CssClass = "CssText"

                objCellsCuerpo(2) = New TableCell
                objCellsCuerpo(2).Text = GCCUtilitario.CheckStr(dr("ClienteRazonSocial").ToString)
                objCellsCuerpo(2).CssClass = "CssLeft"

                objCellsCuerpo(3) = New TableCell
                objCellsCuerpo(3).Text = GCCUtilitario.CheckStr(dr("EstadoContrato").ToString)

                objCellsCuerpo(4) = New TableCell
                objCellsCuerpo(4).Text = GCCUtilitario.CheckStr(dr("ClasificacionBien").ToString)
                objCellsCuerpo(4).CssClass = "CssLeft"

                objCellsCuerpo(5) = New TableCell
                objCellsCuerpo(5).Text = GCCUtilitario.CheckStr(dr("TipoBien").ToString)

                objCellsCuerpo(6) = New TableCell
                objCellsCuerpo(6).Text = GCCUtilitario.CheckStr(dr("NroSiniestro").ToString)
                objCellsCuerpo(6).CssClass = "CssText"

                objCellsCuerpo(7) = New TableCell
                objCellsCuerpo(7).Text = GCCUtilitario.CheckStr(dr("FecConocimientoBanco").ToString)

                objCellsCuerpo(8) = New TableCell
                objCellsCuerpo(8).Text = GCCUtilitario.CheckStr(dr("FecConocimiento").ToString)


                objCellsCuerpo(9) = New TableCell
                objCellsCuerpo(9).Text = GCCUtilitario.CheckStr(dr("FecSiniestro").ToString)


                objCellsCuerpo(10) = New TableCell
                objCellsCuerpo(10).Text = GCCUtilitario.CheckStr(dr("DesTipo").ToString)

                objCellsCuerpo(11) = New TableCell
                objCellsCuerpo(11).Text = GCCUtilitario.CheckStr(dr("FecSituacion").ToString)

                objCellsCuerpo(12) = New TableCell
                objCellsCuerpo(12).Text = GCCUtilitario.CheckStr(dr("DesSituacion").ToString())

                objCellsCuerpo(13) = New TableCell
                objCellsCuerpo(13).Text = GCCUtilitario.CheckStr(dr("DesContrato").ToString).ToString()

                objCellsCuerpo(14) = New TableCell
                objCellsCuerpo(14).Text = GCCUtilitario.CheckStr(dr("FecAplicacion").ToString).ToString()

                objCellsCuerpo(15) = New TableCell
                objCellsCuerpo(15).Text = GCCUtilitario.CheckStr(dr("DesAplicacion").ToString).ToString()

                objCellsCuerpo(16) = New TableCell
                objCellsCuerpo(16).Text = GCCUtilitario.CheckStr(dr("FecDescargoMunicipal").ToString).ToString()

                objCellsCuerpo(17) = New TableCell
                objCellsCuerpo(17).Text = GCCUtilitario.CheckStr(dr("DesSeguro").ToString).ToString()

                objCellsCuerpo(18) = New TableCell
                objCellsCuerpo(18).Text = GCCUtilitario.CheckStr(dr("DesEstadoBien").ToString).ToString()

                objCellsCuerpo(19) = New TableCell
                objCellsCuerpo(19).Text = GCCUtilitario.CheckStr(dr("NroPoliza").ToString).ToString()

                objCellsCuerpo(20) = New TableCell
                objCellsCuerpo(20).Text = GCCUtilitario.CheckStr(dr("DesTipoPoliza").ToString).ToString()

                objCellsCuerpo(21) = New TableCell
                objCellsCuerpo(21).Text = GCCUtilitario.CheckStr(dr("FecRecIndemnizacion").ToString).ToString()

                objCellsCuerpo(22) = New TableCell
                objCellsCuerpo(22).Text = GCCUtilitario.CheckStr(dr("NombreMoneda").ToString).ToString()

                objCellsCuerpo(23) = New TableCell
                objCellsCuerpo(23).Text = GCCUtilitario.CheckStr(dr("MontoIndemnizacion").ToString).ToString()
                objCellsCuerpo(23).CssClass = "cost"

                objCellsCuerpo(24) = New TableCell
                objCellsCuerpo(24).Text = GCCUtilitario.CheckStr(dr("DesBancoEmiteCheque").ToString).ToString()

                objRow.Cells.AddRange(objCellsCuerpo)
                tbReporte.Rows.Add(objRow)
                '   End If
                'Next
            Next

            Try
                Dim mNomReporte As String = "rptSiniestro.xls"
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
        Dim strFiltro As String = IIf(Request.QueryString("filtro") Is Nothing, "", Request.QueryString("filtro"))
        Dim strRuta As String = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer")
        Dim strLogo As String = "logo.png"
        imgLogo.Src = IIf(strRuta.EndsWith("\"), strRuta, strRuta & "\") & strLogo
        pLlenarReporte(strFiltro)
    End Sub
End Class
