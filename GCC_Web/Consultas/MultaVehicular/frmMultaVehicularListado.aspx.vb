Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports System.IO

Partial Class Consultas_MultaVehicular_frmMultaVehicularListado
    Inherits GCCBase
    Dim objLog As New GCCLog("frmMultaVehicularListado.aspx.vb")

#Region "   Eventos     "

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 21/01/2013
    '''  </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            'If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
            '    objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
            '    Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            'End If

            If Not Page.IsPostBack Then

                GCCUtilitario.CargarComboValorGenerico(ddlTipoDocumento, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoPago, GCCConstante.C_TABLAGENERICA_Estado_Pago)
                GCCUtilitario.CargarComboValorGenerico(ddlEstadoCobro, GCCConstante.C_TABLAGENERICA_Estado_Cobro)
                GCCUtilitario.CargarComboValorGenerico(ddlConcepto, GCCConstante.C_TABLAGENERICA_MultaVehicular_Concepto)
                GCCUtilitario.CargarComboValorGenericoInfraccion(ddlCodInfraccion, GCCConstante.C_TABLAGENERICA_MultaVehiculAR_Infraccion)


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

        Dim objMultaVehicular As LImpuestoVehicularNTX = Nothing

        Try
            objMultaVehicular = New LImpuestoVehicularNTX()

            'Inicializa Objeto
            Dim strEMultaVehicular As String = String.Empty
            Dim oEMultaVehicular As New EGCC_MultaVehicular

            With oEMultaVehicular
                .Codsolcredito = txtNroContrato.Value
                .Codunico = txtcuCliente.Value
                .RazonSocialNombre = txtRazonSocialNombre.Value
                .CodigoTipoDocumento = IIf(ddlTipoDocumento.Value = "0", "", ddlTipoDocumento.Value)
                .NumeroDocumento = txtNroDocumento.Value
                .CodTipoBien = IIf(hidTipoBien.Value = "0", "", hidTipoBien.Value)
                .CodNroLote = txtNroLote.Value
                .CodConcepto = IIf(ddlConcepto.Value = "0", "", ddlConcepto.Value)
                .Placa = txtPlaca.Value
                .CodInfraccion = IIf(ddlCodInfraccion.Value = "0", "", ddlCodInfraccion.Value)
                .Infraccion = txtCodigoInfraccion.Value
                .CodMunicipalidad = IIf(hidMunicipalidad.Value = "0", "", hidMunicipalidad.Value)
                .EstadoCobro = IIf(ddlEstadoCobro.Value = "0", "", ddlEstadoCobro.Value)
                .EstadoPago = IIf(ddlEstadoPago.Value = "0", "", ddlEstadoPago.Value)
                .Secimpuesto = 0
            End With
            strEMultaVehicular = GCCUtilitario.SerializeObject(Of EGCC_MultaVehicular)(oEMultaVehicular)

            table = GCCUtilitario.DeserializeObject(Of DataTable)(objMultaVehicular.ObtenerDatosMultaConsulta(strEMultaVehicular))
            Me.pDescargarArchivo(table)
        Catch ex As Exception
            'Throw ex
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGenerar_Click")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        Finally
            If objMultaVehicular IsNot Nothing Then objMultaVehicular = Nothing
            If table IsNot Nothing Then
                table.Dispose()
                table = Nothing
            End If
            strFechaIni = String.Empty
            strFechaFin = String.Empty
        End Try
    End Sub

#End Region

#Region "   Web Métodos "
    <WebMethod()> _
    Public Shared Function ListaMultaVehicular(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pNumeroContraro As String, _
                                           ByVal pCUCliente As String, _
                                           ByVal pRazonSocial As String, _
                                           ByVal pTipoDocumento As String, _
                                           ByVal pNumeroDocumento As String, _
                                           ByVal pTipoBien As String, _
                                           ByVal pNroLote As String, _
                                           ByVal pConcepto As String, _
                                           ByVal pPlaca As String, _
                                           ByVal pCodInfraccion As String, _
                                           ByVal pInfraccion As String, _
                                           ByVal pCodMunicipalidad As String, _
                                           ByVal pEstadoCobro As String, _
                                           ByVal pEstadoPago As String) As JQGridJsonResponse
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        Dim oEMultaVehicular As New EGCC_MultaVehicular

        With oEMultaVehicular
            .Codsolcredito = pNumeroContraro
            .Codunico = pCUCliente
            .RazonSocialNombre = pRazonSocial
            .CodigoTipoDocumento = pTipoDocumento
            .NumeroDocumento = pNumeroDocumento
            .CodTipoBien = pTipoBien
            .CodNroLote = pNroLote
            .CodConcepto = pConcepto
            .Placa = pPlaca
            .CodInfraccion = pCodInfraccion
            .Infraccion = pInfraccion
            .CodMunicipalidad = pCodMunicipalidad
            .EstadoCobro = pEstadoCobro
            .EstadoPago = pEstadoPago

        End With


        Dim dtMultaVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarMultaVehicular(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            GCCUtilitario.SerializeObject(Of EGCC_MultaVehicular)(oEMultaVehicular)))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtMultaVehicular.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtMultaVehicular.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtMultaVehicular.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtMultaVehicular)

    End Function
#End Region

#Region "   Metodos     "
    Private Sub pDescargarArchivo(ByVal ptabla As DataTable)
        Dim sb As New StringBuilder
        Dim sw As StringWriter

        Dim pTitulo As String = "MULTA VEHICULAR"

        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList

        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("Nº Contrato")
        OrderDataTableColumnsTitles.Add("CU Cliente")
        OrderDataTableColumnsTitles.Add("Razón Social o Nombre")
        OrderDataTableColumnsTitles.Add("Placa Actual")
        OrderDataTableColumnsTitles.Add("Municipalidad")
        OrderDataTableColumnsTitles.Add("Marca")
        OrderDataTableColumnsTitles.Add("Modelo")
        OrderDataTableColumnsTitles.Add("Nº Motor")
        OrderDataTableColumnsTitles.Add("Nº Lote")
        OrderDataTableColumnsTitles.Add("Nº Infracción")
        OrderDataTableColumnsTitles.Add("Fecha Infracción")
        OrderDataTableColumnsTitles.Add("Concepto")
        OrderDataTableColumnsTitles.Add("Código Infracción")
        OrderDataTableColumnsTitles.Add("Fecha Registro")
        OrderDataTableColumnsTitles.Add("Fecha Recepción Banco")
        OrderDataTableColumnsTitles.Add("Importe")
        OrderDataTableColumnsTitles.Add("Importe Con Descuento")
        OrderDataTableColumnsTitles.Add("Municipalidad")
        OrderDataTableColumnsTitles.Add("Pago Cliente")
        OrderDataTableColumnsTitles.Add("Fecha Pago")
        OrderDataTableColumnsTitles.Add("Estado Pago")
        OrderDataTableColumnsTitles.Add("Fecha Cobro")
        OrderDataTableColumnsTitles.Add("Estado Cobro")
        OrderDataTableColumnsTitles.Add("Observaciones")

        '**** Nombres de las Columnas del DataTable ****
        OrderDataTableColumnsName.Add("CodSolicitudCredito")
        OrderDataTableColumnsName.Add("CodUnico|1")
        OrderDataTableColumnsName.Add("RazonSocial|3")
        OrderDataTableColumnsName.Add("Placa")        
        OrderDataTableColumnsName.Add("Municipalidad|3")
        OrderDataTableColumnsName.Add("Marca")
        OrderDataTableColumnsName.Add("Modelo")
        OrderDataTableColumnsName.Add("NroMotor")
        OrderDataTableColumnsName.Add("CodNroLote|1")
        OrderDataTableColumnsName.Add("NroInfraccion")
        OrderDataTableColumnsName.Add("FecInfraccion")
        OrderDataTableColumnsName.Add("Concepto")
        OrderDataTableColumnsName.Add("InfraccionTexto")
        OrderDataTableColumnsName.Add("FecIngreso")
        OrderDataTableColumnsName.Add("FecRecepcionBanco")
        OrderDataTableColumnsName.Add("Importe|2")
        OrderDataTableColumnsName.Add("ImporteDescuento|2")
        OrderDataTableColumnsName.Add("MunicipalidadMulta|3")
        OrderDataTableColumnsName.Add("DesPagoCliente")
        OrderDataTableColumnsName.Add("FecPago")
        OrderDataTableColumnsName.Add("DesEstadoPago")
        OrderDataTableColumnsName.Add("FecCobro")
        OrderDataTableColumnsName.Add("DesEstadoCobro")
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

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "MultaVehicular")
    End Sub
#End Region

End Class
