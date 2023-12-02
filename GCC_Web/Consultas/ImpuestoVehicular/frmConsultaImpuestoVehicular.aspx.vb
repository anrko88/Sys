
Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports System.IO
Partial Class Consultas_ImpuestoVehicular_frmConsultaImpuestoVehicular
    Inherits GCCBase
    Dim objLog As New GCCLog("frmConsultaImpuestoVehicular.aspx.vb")
#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then

                GCCUtilitario.CargarComboValorGenerico(ddlTipoDocumento, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoPago, GCCConstante.C_TABLAGENERICA_Estado_Pago)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoCobro, GCCConstante.C_TABLAGENERICA_Estado_Cobro)
            End If

        Catch ex As ApplicationException
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                If Not IsPostBack Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "scriptSalir", "parent.fMensajeSalida('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaSDA") & "')", True)
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fMensajeSalida('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaSDA") & "')", True)
                End If
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If

        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim strFechaIni As String = String.Empty
        Dim strFechaFin As String = String.Empty
        Dim table As DataTable = Nothing

        Dim objImpuestoVehicularNTx As LImpuestoVehicularNTX = Nothing


        Try
            objImpuestoVehicularNTx = New LImpuestoVehicularNTX()

            Dim oEGccImpuestovehicular As New EImpuestovehicular
            Dim strEImpuestoVehicular As String
            With oEGccImpuestovehicular

                .Codsolcredito = txtNroContrato.Value
                .Codunico = txtcuCliente.Value
                .RazonSocialNombre = txtRazonSocialNombre.Value
                .CodigoTipoDocumento = ddlTipoDocumento.Value
                .NumeroDocumento = txtNroDocumento.Value
                .Placa = txtPlaca.Value
                If txtFechaInscripcionDesde.Value <> "" Then
                    .FechaInscripcionIni = GCCUtilitario.StringToDateTime(txtFechaInscripcionDesde.Value)
                End If
                If txtFechaInscripcionHasta.Value <> "" Then
                    .FechaInscripcionFin = GCCUtilitario.StringToDateTime(txtFechaInscripcionHasta.Value)
                End If

                .EstadoCobro = ddlEstadoCobro.Value
                .EstadoPago = ddlEstadoPago.Value
                .AnioFabricacion = txtAnioFabricacion.Value
                .Periodo = txtPeriodo.Value
                .CodNroLote = txtNroLote.Value

            End With

            strEImpuestoVehicular = GCCUtilitario.SerializeObject(oEGccImpuestovehicular)

            table = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularNTx.ListarImpuestoVehicularReporte(strEImpuestoVehicular))
            Me.pDescargarArchivo(table)
        Catch ex As Exception
            'Throw ex
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGenerar_Click")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        Finally
            If objImpuestoVehicularNTx IsNot Nothing Then objImpuestoVehicularNTx = Nothing
            If table IsNot Nothing Then
                table.Dispose()
                table = Nothing
            End If
            strFechaIni = String.Empty
            strFechaFin = String.Empty
        End Try
    End Sub
#End Region

#Region "Métodos"
    <WebMethod()> _
    Public Shared Function ListaImpuestoVehicular(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pNumeroContraro As String, _
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
                                           ByVal pNroLote As String) As JQGridJsonResponse
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        Dim oEImpuestoVehicular As New EImpuestovehicular
        'Valida Campos            
        Dim strCodUnico As String = GCCUtilitario.NullableString(pCUCliente)
        If Not strCodUnico Is Nothing Then
            strCodUnico = strCodUnico.Trim.PadLeft(10, "0"c)
        End If

        With oEImpuestoVehicular
            .Codsolcredito = pNumeroContraro
            .Codunico = strCodUnico
            .RazonSocialNombre = pRazonSocial
            .CodigoTipoDocumento = pTipoDocumento
            .NumeroDocumento = pNumeroDocumento
            .Placa = pPlaca
            .FechaInscripcionIni = GCCUtilitario.StringToDateTime(pFechaInscripcionIni)
            .FechaInscripcionFin = GCCUtilitario.StringToDateTime(pFechaInscripcionFin)
            .EstadoCobro = pEstadoCobro
            .EstadoPago = pEstadoPago
            .AnioFabricacion = pAnioFabricacion
            .Periodo = pPeriodo
            .CodNroLote = pNroLote
        End With


        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarImpuestoVehicular(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            GCCUtilitario.SerializeObject(Of EImpuestovehicular)(oEImpuestoVehicular)))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtImpuestoVehicular.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtImpuestoVehicular.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtImpuestoVehicular.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtImpuestoVehicular)

    End Function

    Private Sub pDescargarArchivo(ByVal ptabla As DataTable)
        Dim sb As New StringBuilder
        Dim sw As StringWriter

        Dim pTitulo As String = "IMPUESTO VEHICULAR"

        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList

        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("Nº Contrato")
        OrderDataTableColumnsTitles.Add("Razón Social o Nombre")
        OrderDataTableColumnsTitles.Add("Nº Lote")
        OrderDataTableColumnsTitles.Add("Placa Actual")
        OrderDataTableColumnsTitles.Add("Nº Motor")
        OrderDataTableColumnsTitles.Add("Marca")
        OrderDataTableColumnsTitles.Add("Modelo")
        OrderDataTableColumnsTitles.Add("Año Fabricación")
        OrderDataTableColumnsTitles.Add("F. Inscripción Registral")
        OrderDataTableColumnsTitles.Add("F. Declaración")
        OrderDataTableColumnsTitles.Add("Periodo")
        OrderDataTableColumnsTitles.Add("NºCuota")
        OrderDataTableColumnsTitles.Add("Moneda")
        OrderDataTableColumnsTitles.Add("Importe")
        OrderDataTableColumnsTitles.Add("Pago Cliente")
        OrderDataTableColumnsTitles.Add("F. Pago")
        OrderDataTableColumnsTitles.Add("Estado Pago")
        OrderDataTableColumnsTitles.Add("F. Cobro")
        OrderDataTableColumnsTitles.Add("Estado Cobro")
        OrderDataTableColumnsTitles.Add("Nº Cheque")
        OrderDataTableColumnsTitles.Add("Observaciones")

        '**** Nombres de las Columnas del DataTable ****
        OrderDataTableColumnsName.Add("CodSolicitudCredito")
        OrderDataTableColumnsName.Add("RazonSocial|3")
        OrderDataTableColumnsName.Add("NroLote")
        OrderDataTableColumnsName.Add("placa")
        OrderDataTableColumnsName.Add("NroMotor")
        OrderDataTableColumnsName.Add("Marca")
        OrderDataTableColumnsName.Add("modelo")
        OrderDataTableColumnsName.Add("anioFabricacion")
        OrderDataTableColumnsName.Add("FechaInscripcion")
        OrderDataTableColumnsName.Add("FecDeclaracion")
        OrderDataTableColumnsName.Add("Periodo")
        OrderDataTableColumnsName.Add("NroCuota")
        OrderDataTableColumnsName.Add("Moneda")
        OrderDataTableColumnsName.Add("Importe|2")
        OrderDataTableColumnsName.Add("PagoCliente")
        OrderDataTableColumnsName.Add("FechaPago")
        OrderDataTableColumnsName.Add("EstPago")
        OrderDataTableColumnsName.Add("FechaCobro")
        OrderDataTableColumnsName.Add("EstCobro")
        OrderDataTableColumnsName.Add("NroCheque")
        OrderDataTableColumnsName.Add("Observaciones|3")

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
        'Dim strStyle As String = Request.PhysicalApplicationPath + "Util/css/css_excel.css" '"http://" & Request.Url.Host & ResolveUrl("~/Estilos/estilos.css")
        'sb.Append("<link rel='stylesheet' type='text/css' href='" & strStyle & "'>")
        sb.Append(GCCUtilitario.fHTMLEstilo())
        sb.Append(vbCrLf)
        sb.Append("</head>")
        sb.Append(vbCrLf)
        sb.Append(" <body>")
        sb.Append(vbCrLf)
        sb.Append(GCCUtilitario.fHTMLGeneraTablaTitulo(pTitulo, Me, ptabla.Columns.Count))
        sb.Append(" <br>")
        sb.Append(vbCrLf)
        sb.Append(GCCUtilitario.fHTMLGeneraTablaCabecera(OrderHeaderTableColumnsLabels, OrderHeaderTableColumnsText, ptabla.Columns.Count))
        sb.Append(" <br>")
        sb.Append(vbCrLf)
        sb.Append(GCCUtilitario.fHTMLGeneraTablaEstilo(ptabla, OrderDataTableColumnsTitles, OrderDataTableColumnsName))
        sb.Append("</body>")
        sb.Append(vbCrLf)
        sb.Append("</html>")
        sb.Append(vbCrLf)

        sw = New StringWriter(sb)

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "ImpuestoVehicular")
    End Sub
#End Region

End Class