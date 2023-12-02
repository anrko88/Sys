Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports System.Data
Partial Class Reportes_frmRepMultaVehicular
    Inherits GCCBase

    Private Sub pLlenarReporte(ByVal pstrFiltro As String)
        Dim dtReporte As Data.DataTable
        Dim objRow As TableRow
        Try

            Dim objCells(23) As TableCell

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
            objCells(3).Text = "Placa Actual"

            objCells(4) = New TableCell
            objCells(4).CssClass = "trCabecera"
            objCells(4).Text = "Municipalidad"

            objCells(5) = New TableCell
            objCells(5).CssClass = "trCabecera"
            objCells(5).Text = "Marca"

            objCells(6) = New TableCell
            objCells(6).CssClass = "trCabecera"
            objCells(6).Text = "Modelo"

            objCells(7) = New TableCell
            objCells(7).CssClass = "trCabecera"
            objCells(7).Text = "Nº Motor"

            objCells(8) = New TableCell
            objCells(8).CssClass = "trCabecera"
            objCells(8).Text = "Nº Lote"

            objCells(9) = New TableCell
            objCells(9).CssClass = "trCabecera"
            objCells(9).Text = "Nº Infracción"

            objCells(10) = New TableCell
            objCells(10).CssClass = "trCabecera"
            objCells(10).Text = "Fecha Infracción"

            objCells(11) = New TableCell
            objCells(11).CssClass = "trCabecera"
            objCells(11).Text = "Concepto"

            objCells(12) = New TableCell
            objCells(12).CssClass = "trCabecera"
            objCells(12).Text = "Código Infracción"

            objCells(13) = New TableCell
            objCells(13).CssClass = "trCabecera"
            objCells(13).Text = "Fecha Registro"

            objCells(14) = New TableCell
            objCells(14).CssClass = "trCabecera"
            objCells(14).Text = "Fecha Recepción Banco"

            objCells(15) = New TableCell
            objCells(15).CssClass = "trCabecera"
            objCells(15).Text = "Importe"

            objCells(16) = New TableCell
            objCells(16).CssClass = "trCabecera"
            objCells(16).Text = "Importe Con Descuento"

            objCells(17) = New TableCell
            objCells(17).CssClass = "trCabecera"
            objCells(17).Text = "Municipalidad"

            objCells(18) = New TableCell
            objCells(18).CssClass = "trCabecera"
            objCells(18).Text = "Pago Cliente"

            objCells(19) = New TableCell
            objCells(19).CssClass = "trCabecera"
            objCells(19).Text = "Fecha Pago"

            objCells(20) = New TableCell
            objCells(20).CssClass = "trCabecera"
            objCells(20).Text = "Estado Pago"

            objCells(21) = New TableCell
            objCells(21).CssClass = "trCabecera"
            objCells(21).Text = "Fecha Cobro"

            objCells(22) = New TableCell
            objCells(22).CssClass = "trCabecera"
            objCells(22).Text = "Estado Cobro"

            objCells(23) = New TableCell
            objCells(23).CssClass = "trCabecera"
            objCells(23).Text = "Observaciones"

            objRow.Cells.AddRange(objCells)
            tbReporte.Rows.Add(objRow)

            'Consulta
            Dim arrFiltro() As String = pstrFiltro.Split("|")
            Dim strEMultaVehicular As String = String.Empty
            If pstrFiltro <> String.Empty Then
                Dim oEMultaVehicular As New EGCC_MultaVehicular

                With oEMultaVehicular
                    .Codsolcredito = arrFiltro(0)
                    .Codunico = arrFiltro(1)
                    .RazonSocialNombre = arrFiltro(2)
                    .CodigoTipoDocumento = arrFiltro(3)
                    .NumeroDocumento = arrFiltro(4)
                    .CodTipoBien = arrFiltro(5)
                    .CodNroLote = arrFiltro(6)
                    .CodConcepto = arrFiltro(7)
                    .Placa = arrFiltro(8)
                    .CodInfraccion = arrFiltro(9)
                    .Infraccion = arrFiltro(10)
                    .CodMunicipalidad = arrFiltro(11)
                    .EstadoCobro = arrFiltro(12)
                    .EstadoPago = arrFiltro(13)
                    .Secimpuesto = 0
                End With
                strEMultaVehicular = GCCUtilitario.SerializeObject(Of EGCC_MultaVehicular)(oEMultaVehicular)
            End If


            Dim objMultas As New LImpuestoVehicularNTX
            Dim dtListado As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objMultas.ObtenerDatosMultaConsulta(strEMultaVehicular))
            'Dim strCodImpuesto As String() = StrCodigo.Split(","c)
            For Each dr As Data.DataRow In dtListado.Rows
                '    For i As Integer = 0 To strCodImpuesto.Length - 1
                'If dr("SecImpuesto").ToString = strCodImpuesto(i) Then
                Dim objCellsCuerpo(23) As TableCell

                objRow = New TableRow
                objRow.CssClass = "tdFilas"
                objRow.HorizontalAlign = HorizontalAlign.Center

                objCellsCuerpo(0) = New TableCell
                objCellsCuerpo(0).Text = GCCUtilitario.CheckStr(dr("CodSolicitudCredito").ToString)

                objCellsCuerpo(1) = New TableCell
                objCellsCuerpo(1).Text = GCCUtilitario.CheckStr(dr("CodUnico").ToString).PadLeft(10, "0")
                objCellsCuerpo(1).CssClass = "CssText"

                objCellsCuerpo(2) = New TableCell
                objCellsCuerpo(2).Text = GCCUtilitario.CheckStr(dr("RazonSocial").ToString).ToUpper()
                objCellsCuerpo(2).CssClass = "CssLeft"

                objCellsCuerpo(3) = New TableCell
                objCellsCuerpo(3).Text = GCCUtilitario.CheckStr(dr("Placa").ToString)

                objCellsCuerpo(4) = New TableCell
                objCellsCuerpo(4).Text = GCCUtilitario.CheckStr(dr("Municipalidad").ToString).ToUpper()
                objCellsCuerpo(4).CssClass = "CssLeft"

                objCellsCuerpo(5) = New TableCell
                objCellsCuerpo(5).Text = GCCUtilitario.CheckStr(dr("Marca").ToString)

                objCellsCuerpo(6) = New TableCell
                objCellsCuerpo(6).Text = GCCUtilitario.CheckStr(dr("Modelo").ToString)

                objCellsCuerpo(7) = New TableCell
                objCellsCuerpo(7).Text = GCCUtilitario.CheckStr(dr("NroMotor").ToString)

                objCellsCuerpo(8) = New TableCell
                objCellsCuerpo(8).Text = GCCUtilitario.CheckStr(dr("CodNroLote").ToString)
                objCellsCuerpo(8).CssClass = "CssText"

                objCellsCuerpo(9) = New TableCell
                objCellsCuerpo(9).Text = GCCUtilitario.CheckStr(dr("NroInfraccion").ToString)
                objCellsCuerpo(9).CssClass = "CssText"

                objCellsCuerpo(10) = New TableCell
                objCellsCuerpo(10).Text = GCCUtilitario.CheckStr(dr("FecInfraccion").ToString)

                objCellsCuerpo(11) = New TableCell
                objCellsCuerpo(11).Text = GCCUtilitario.CheckStr(dr("Concepto").ToString)

                objCellsCuerpo(12) = New TableCell
                objCellsCuerpo(12).Text = GCCUtilitario.CheckStr(dr("DesInfraccion").ToString()) & " - " & GCCUtilitario.CheckStr(dr("Infraccion").ToString())

                objCellsCuerpo(13) = New TableCell
                objCellsCuerpo(13).Text = GCCUtilitario.CheckStr(dr("FecIngreso").ToString).ToString()

                objCellsCuerpo(14) = New TableCell
                objCellsCuerpo(14).Text = GCCUtilitario.CheckStr(dr("FecRecepcionBanco").ToString).ToString()

                objCellsCuerpo(15) = New TableCell
                objCellsCuerpo(15).Text = GCCUtilitario.CheckStr(dr("Importe").ToString).ToString()
                objCellsCuerpo(15).CssClass = "cost"

                objCellsCuerpo(16) = New TableCell
                objCellsCuerpo(16).Text = GCCUtilitario.CheckStr(dr("ImporteDescuento").ToString).ToString()
                objCellsCuerpo(16).CssClass = "cost"

                objCellsCuerpo(17) = New TableCell
                objCellsCuerpo(17).Text = GCCUtilitario.CheckStr(dr("MunicipalidadMulta").ToString).ToString()

                objCellsCuerpo(18) = New TableCell
                objCellsCuerpo(18).Text = IIf(GCCUtilitario.CheckStr(dr("PagoCliente").ToString).ToString() = "1", "Si", "No")

                objCellsCuerpo(19) = New TableCell
                objCellsCuerpo(19).Text = GCCUtilitario.CheckStr(dr("FecPago").ToString).ToString()

                objCellsCuerpo(20) = New TableCell
                objCellsCuerpo(20).Text = GCCUtilitario.CheckStr(dr("DesEstadoPago").ToString).ToString()

                objCellsCuerpo(21) = New TableCell
                objCellsCuerpo(21).Text = GCCUtilitario.CheckStr(dr("FecCobro").ToString).ToString()

                objCellsCuerpo(22) = New TableCell
                objCellsCuerpo(22).Text = GCCUtilitario.CheckStr(dr("DesEstadoCobro").ToString).ToString()

                objCellsCuerpo(23) = New TableCell
                objCellsCuerpo(23).Text = GCCUtilitario.CheckStr(dr("Observaciones").ToString).ToString()
                objCellsCuerpo(23).CssClass = "CssLeft"

                objRow.Cells.AddRange(objCellsCuerpo)
                tbReporte.Rows.Add(objRow)
                '   End If
                'Next
            Next

            Try
                Dim mNomReporte As String = "rptMultaVehicular.xls"
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
                                    ".cost{mso-number-format:'#,##0.00';text-align:rgiht; }" & _
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
