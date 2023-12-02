Imports GCC.Entity
Imports GCC.UI
Imports GCC.LogicWS
Imports System.Data
Partial Class Reportes_frmConsultaImpuestoVehicular
    Inherits GCCBase
    Private Sub pLlenarTablaImpuestoVehicular(ByVal pNumeroContraro As String, _
                                         ByVal pCUCliente As String, _
                                         ByVal pRazonSocial As String, _
                                         ByVal pTipoDocumento As String, _
                                         ByVal pNumeroDocumento As String, _
                                         ByVal pPlaca As String, _
                                         ByVal pFechaInscripcionIni As String, _
                                         ByVal pFechaInscripcionFin As String, _
                                         ByVal pEstadoCobro As String, _
                                         ByVal pEstadoPago As String, _
                                         ByVal pAnioFabricacion As String, _
                                         ByVal pPeriodo As String, _
                                         ByVal pNroLote As String)
        Dim dtReporte As Data.DataTable
        Dim StrCodigo As String


        Dim oEGccImpuestovehicular As New EImpuestovehicular
        Dim strEImpuestoVehicular As String
        With oEGccImpuestovehicular

            .Codsolcredito = pNumeroContraro
            .Codunico = pCUCliente
            .RazonSocialNombre = pRazonSocial
            .CodigoTipoDocumento = pTipoDocumento
            .NumeroDocumento = pNumeroDocumento
            .Placa = pPlaca
            If pFechaInscripcionIni <> "" Then
                '.FechaInscripcionIni = GCCUtilitario.ToStringyyyyMMdd(pFechaInscripcionIni)
                .FechaInscripcionIni = GCCUtilitario.StringToDateTime(pFechaInscripcionIni)
            End If
            If pFechaInscripcionFin <> "" Then
                '.FechaInscripcionFin = GCCUtilitario.ToStringyyyyMMdd(pFechaInscripcionFin)
                .FechaInscripcionFin = GCCUtilitario.StringToDateTime(pFechaInscripcionFin)
            End If

            .EstadoCobro = pEstadoCobro
            .EstadoPago = pEstadoPago
            .AnioFabricacion = pAnioFabricacion
            .Periodo = pPeriodo
            .CodNroLote = pNroLote
            
        End With

        strEImpuestoVehicular = GCCUtilitario.SerializeObject(oEGccImpuestovehicular)

        'StrCodigo = Request.QueryString("strcodigo") '.Split(Convert.ToChar("|"))
        Dim objRow As TableRow
        Try
            lblTitle.Text = "REPORTE IMPUESTO VEHICULAR"
            'lblSubTitle.Text = String.Format("Fecha y Hora : {0}", pstrFecha)
            'txtLote.Text = Request.QueryString("nrolote")
            'txtTotal.Text = Request.QueryString("total")

            Dim objCells(21) As TableCell

            'Cabeceras Grilla - Fila 1
            objRow = New TableRow
            objRow.CssClass = "trCabecera"
            objRow.HorizontalAlign = HorizontalAlign.Center

            objCells(0) = New TableCell
            objCells(0).CssClass = "trCabecera"
            objCells(0).Text = "Nº Contrato"

            objCells(1) = New TableCell
            objCells(1).CssClass = "trCabecera"
            objCells(1).Text = "Razón Social"

            objCells(2) = New TableCell
            objCells(2).CssClass = "trCabecera"
            objCells(2).Text = "Lote"

            objCells(3) = New TableCell
            objCells(3).CssClass = "trCabecera"
            objCells(3).Text = "Placa Actual"

            objCells(4) = New TableCell
            objCells(4).CssClass = "trCabecera"
            objCells(4).Text = "Descripciòn del Bien"

            objCells(5) = New TableCell
            objCells(5).CssClass = "trCabecera"
            objCells(5).Text = "Nº Motor"

            objCells(6) = New TableCell
            objCells(6).CssClass = "trCabecera"
            objCells(5).Text = "Marca"

            objCells(7) = New TableCell
            objCells(7).CssClass = "trCabecera"
            objCells(7).Text = "Modelo"

            ''''''
            objCells(8) = New TableCell
            objCells(8).CssClass = "trCabecera"
            objCells(8).Text = "Año Fabricación"

            objCells(9) = New TableCell
            objCells(9).CssClass = "trCabecera"
            objCells(9).Text = "F. Inscripción Registral"

            objCells(10) = New TableCell
            objCells(10).CssClass = "trCabecera"
            objCells(10).Text = "F. Declaración"

            'objCells(11) = New TableCell
            'objCells(11).CssClass = "trCabecera"
            'objCells(11).Text = "Lote"

            objCells(11) = New TableCell
            objCells(11).CssClass = "trCabecera"
            objCells(11).Text = "Periodo"

            objCells(12) = New TableCell
            objCells(12).CssClass = "trCabecera"
            objCells(12).Text = "NºCuota"

            objCells(13) = New TableCell
            objCells(13).CssClass = "trCabecera"
            objCells(13).Text = "Moneda"

            objCells(14) = New TableCell
            objCells(14).CssClass = "trCabecera"
            objCells(14).Text = "Importe"

            objCells(15) = New TableCell
            objCells(15).CssClass = "trCabecera"
            objCells(15).Text = "Pago Cliente"

            objCells(16) = New TableCell
            objCells(16).CssClass = "trCabecera"
            objCells(16).Text = "F. Pago"

            objCells(17) = New TableCell
            objCells(17).CssClass = "trCabecera"
            objCells(17).Text = "Estado Pago"

            objCells(18) = New TableCell
            objCells(18).CssClass = "trCabecera"
            objCells(18).Text = "F. Cobro"

            objCells(19) = New TableCell
            objCells(19).CssClass = "trCabecera"
            objCells(19).Text = "Estado Cobro"

            objCells(20) = New TableCell
            objCells(20).CssClass = "trCabecera"
            objCells(20).Text = "Nº Cheque"

            objCells(21) = New TableCell
            objCells(21).CssClass = "trCabecera"
            objCells(21).Text = "Observaciones"

            objRow.Cells.AddRange(objCells)
            Table1.Rows.Add(objRow)



            'Consulta

            Dim objImpuestoVehicularNTx As New LImpuestoVehicularNTX
            Dim dtRegCompra As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularNTx.ListarImpuestoVehicularReporte(strEImpuestoVehicular))
            'Dim FechaAprox As String
            'Dim strCodImpuesto As String() = StrCodigo.Split(","c)
            For Each dr As Data.DataRow In dtRegCompra.Rows

                Dim objCellsCuerpo(21) As TableCell

                objRow = New TableRow
                objRow.CssClass = "tdFilas"
                objRow.HorizontalAlign = HorizontalAlign.Center

                objCellsCuerpo(0) = New TableCell
                objCellsCuerpo(0).Text = GCCUtilitario.CheckStr(dr("CodSolicitudCredito").ToString)

                objCellsCuerpo(1) = New TableCell
                objCellsCuerpo(1).Text = GCCUtilitario.CheckStr(dr("RazonSocial").ToString)

                objCellsCuerpo(2) = New TableCell
                objCellsCuerpo(2).Text = GCCUtilitario.CheckStr(dr("NroLote").ToString)

                objCellsCuerpo(3) = New TableCell
                objCellsCuerpo(3).Text = GCCUtilitario.CheckStr(dr("placa").ToString)

                objCellsCuerpo(4) = New TableCell
                objCellsCuerpo(4).Text = ""

                objCellsCuerpo(5) = New TableCell
                objCellsCuerpo(5).Text = GCCUtilitario.CheckStr(dr("NroMotor").ToString)
                'objCellsCuerpo(5).CssClass = "CssText"

                objCellsCuerpo(6) = New TableCell
                objCellsCuerpo(6).Text = GCCUtilitario.CheckStr(dr("Marca").ToString)

                objCellsCuerpo(7) = New TableCell
                objCellsCuerpo(7).Text = GCCUtilitario.CheckStr(dr("modelo").ToString)


                objCellsCuerpo(8) = New TableCell
                objCellsCuerpo(8).Text = GCCUtilitario.CheckStr(dr("anioFabricacion").ToString)

                objCellsCuerpo(9) = New TableCell
                objCellsCuerpo(9).Text = GCCUtilitario.CheckStr(dr("FechaInscripcion").ToString)

                objCellsCuerpo(10) = New TableCell
                objCellsCuerpo(10).Text = GCCUtilitario.CheckStr(dr("FecDeclaracion").ToString)

                'objCellsCuerpo(11) = New TableCell
                'objCellsCuerpo(11).Text = GCCUtilitario.CheckStr(dr("NroLote").ToString)

                objCellsCuerpo(11) = New TableCell
                objCellsCuerpo(11).Text = GCCUtilitario.CheckStr(dr("Periodo").ToString)

                objCellsCuerpo(12) = New TableCell
                objCellsCuerpo(12).Text = GCCUtilitario.CheckStr(dr("NroCuota").ToString)

                objCellsCuerpo(13) = New TableCell
                objCellsCuerpo(13).Text = GCCUtilitario.CheckStr(dr("Moneda").ToString)

                objCellsCuerpo(14) = New TableCell
                objCellsCuerpo(14).Text = GCCUtilitario.CheckStr(dr("Importe").ToString)

                objCellsCuerpo(15) = New TableCell
                objCellsCuerpo(15).Text = GCCUtilitario.CheckStr(dr("PagoCliente").ToString)

                objCellsCuerpo(16) = New TableCell
                objCellsCuerpo(16).Text = GCCUtilitario.CheckStr(dr("FechaPago").ToString)
                'objCellsCuerpo(15).CssClass = "cost" EstPago

                objCellsCuerpo(17) = New TableCell
                objCellsCuerpo(17).Text = GCCUtilitario.CheckStr(dr("EstPago").ToString)
                'objCellsCuerpo(16).CssClass = "cost"

                objCellsCuerpo(18) = New TableCell
                objCellsCuerpo(18).Text = GCCUtilitario.CheckStr(dr("FechaCobro").ToString)
                'objCellsCuerpo(18).CssClass = "cost"

                objCellsCuerpo(19) = New TableCell
                objCellsCuerpo(19).Text = GCCUtilitario.CheckStr(dr("EstCobro").ToString)
                ' objCellsCuerpo(19).CssClass = "cost"

                objCellsCuerpo(20) = New TableCell
                objCellsCuerpo(20).Text = GCCUtilitario.CheckStr(dr("NroCheque").ToString)
                'objCellsCuerpo(19).CssClass = "cost"

                objCellsCuerpo(21) = New TableCell
                objCellsCuerpo(21).Text = GCCUtilitario.CheckStr(dr("Observaciones").ToString)
   

                objRow.Cells.AddRange(objCellsCuerpo)
                Table1.Rows.Add(objRow)

            Next

            Try
                Dim mNomReporte As String = "rptImpuestoVehicular.xls"
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
        'Dim strCodigo As String = Request.QueryString("strcodigo")

        Dim pNumeroContraro As String = Request.QueryString("Codcontrato")
        Dim pCUCliente As String = Request.QueryString("CodUnico")
        Dim pRazonSocial As String = Request.QueryString("RazonSocial")
        Dim pTipoDocumento As String = Request.QueryString("TipoDocumento")
        Dim pNumeroDocumento As String = Request.QueryString("NumeroDocumento")
        Dim pPlaca As String = Request.QueryString("Placa")
        Dim pNroLote As String = Request.QueryString("NLote")
        Dim pAnioFabricacion As String = Request.QueryString("AnioFabricacion")
        Dim pPeriodo As String = Request.QueryString("Periodo")
        Dim pFechaInscripcionIni As String = Request.QueryString("FInscripcionDesde")
        Dim pFechaInscripcionFin As String = Request.QueryString("FInscripcionHasta")
        Dim pEstadoCobro As String = Request.QueryString("EstadoPago")
        Dim pEstadoPago As String = Request.QueryString("EstadoCobro")

        pLlenarTablaImpuestoVehicular(pNumeroContraro, pCUCliente, pRazonSocial, pTipoDocumento, pNumeroDocumento, pPlaca, pFechaInscripcionIni, pFechaInscripcionFin, pEstadoCobro, pEstadoPago, pAnioFabricacion, pPeriodo, pNroLote)

    End Sub
End Class
