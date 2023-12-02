Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports System.IO
Partial Class GestionBien_MultaVehicular_frmMultaVehicularLiquidar
    Inherits GCCBase
    Dim objLog As New GCCLog("frmImpuestoVehicularListado.aspx.vb")
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

#End Region
#Region "Métodos"
    <WebMethod()> _
    Public Shared Function ListaMultaVehicularLiquidar(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pNroLote As String) As JQGridJsonResponse
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        'Valida Campos            

        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarMultaVehicularLiquidar(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            pNroLote))

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
    <WebMethod()> _
    Public Shared Function ListaMultaVehicularLiquidarTodo(ByVal pNroLote As String) As JQGridJsonResponse
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        'Valida Campos            

        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarMultaVehicularLiquidarTodo(pNroLote))

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 1000, dtImpuestoVehicular)

    End Function
    'Inicio IBK - AAE - 14/02/2013 - Obtengo info del lote
    <WebMethod()> _
    Public Shared Function ObtenerHeaderLote(ByVal pNroLote As String) As JQGridJsonResponse
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        Dim oEImpuestoVehicular As New EImpuestovehicular
        'Valida Campos            
        Dim flag As String = GCCConstante.C_LOTE_MULT_VEHICULAR
        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ObtenerHeaderLote(pNroLote, flag))


        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 1000, dtImpuestoVehicular)

    End Function

    'Inicion IBK - JJM - 15/02/2013 - Liquido el lote
    <WebMethod()> _
  Public Shared Function LiquidarLote(ByVal strNroLote As String) As String
        Dim blnResult As Boolean = False
        Dim Resultado As String = String.Empty
        Dim objLImpuestoVehicular As New LImpuestoVehicularTX
        Try
            Dim pstrNroLote As String = strNroLote.Trim.PadLeft(8, "0"c)
            Dim pstrUsuarioModificacion As String = GCCSession.CodigoUsuario
            Dim pstrCodigoConcepto As String = GCCConstante.C_CONCEPTO_INFRACCION_TRANSITO
            'Ejecuta Consulta
            Resultado = objLImpuestoVehicular.LiquidarLote(pstrUsuarioModificacion, strNroLote, pstrCodigoConcepto)
            Return Resultado
        Catch ex As Exception
            Return ex.ToString() + "|-1" 'Error
        End Try
    End Function
    'Fin IBK
    'Inicio IBK - AAE - 13/02/2013
    <WebMethod()> _
   Public Shared Function CheckLote(ByVal pNroLote As String) As String
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        Dim flag As String = GCCConstante.C_LOTE_MULT_VEHICULAR ' Para impuesto inmueble el flag es 2
        Dim res As String = objImpuestoVehicularTx.CheckLote(pNroLote, flag)
        Return res
    End Function
    'Fin IBK
#End Region

    Private Sub pDescargarArchivo()
        Dim sb As New StringBuilder
        Dim sw As StringWriter

        Dim objImpuestoVehicularNTx As New LImpuestoVehicularNTX
        Dim dtRegCompra As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularNTx.ListarMultaVehicularReporteLiquidar(hddCodigos.Value.ToString()))

        Dim pTitulo As String = "LIQUIDAR - Multa Vehicular"

        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList
        Dim ColumnStyle As New ArrayList

        '**** Etiquetas de la cabecera ****


        OrderHeaderTableColumnsLabels.Add("Nº Lote:")
        OrderHeaderTableColumnsLabels.Add("Total:")
        OrderHeaderTableColumnsText.Add(hidNroLote.Value)
        OrderHeaderTableColumnsText.Add(dtRegCompra.Compute("sum(Importe)", ""))


        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("Nº Contrato")
        OrderDataTableColumnsTitles.Add("CU Cliente")
        OrderDataTableColumnsTitles.Add("Razón Social o Nombre")
        OrderDataTableColumnsTitles.Add("Nº Infracción")
        OrderDataTableColumnsTitles.Add("Tipo de Bien")
        OrderDataTableColumnsTitles.Add("Distrito/Municipalidad")
        OrderDataTableColumnsTitles.Add("Placa")
        OrderDataTableColumnsTitles.Add("Marca")
        OrderDataTableColumnsTitles.Add("Modelo")
        OrderDataTableColumnsTitles.Add("Importe")
        OrderDataTableColumnsTitles.Add("Estado de Pago")
        OrderDataTableColumnsTitles.Add("Estado de Cobro")


        '**** Nombres de las Columnas del DataTable ****
        'OrderDataTableColumnsName.Add("<Campo>|<Estilo>")
        OrderDataTableColumnsName.Add("CodSolicitudCredito")
        OrderDataTableColumnsName.Add("CodUnico|1")
        OrderDataTableColumnsName.Add("RazonSocial|3")
        OrderDataTableColumnsName.Add("NroInfraccion")
        OrderDataTableColumnsName.Add("TipoBien")
        OrderDataTableColumnsName.Add("DistritoNombre")
        OrderDataTableColumnsName.Add("Placa")
        OrderDataTableColumnsName.Add("Marca")
        OrderDataTableColumnsName.Add("Modelo")
        OrderDataTableColumnsName.Add("importe|2")
        OrderDataTableColumnsName.Add("EstPago")
        OrderDataTableColumnsName.Add("EstCobro")


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
        sb.Append(GCCUtilitario.fHTMLGeneraTablaTitulo(pTitulo, Me, dtRegCompra.Columns.Count))
        sb.Append(" <br>")
        sb.Append(vbCrLf)
        sb.Append(GCCUtilitario.fHTMLGeneraTablaCabecera(OrderHeaderTableColumnsLabels, OrderHeaderTableColumnsText, dtRegCompra.Columns.Count))
        sb.Append(" <br>")
        sb.Append(vbCrLf)
        sb.Append(GCCUtilitario.fHTMLGeneraTablaEstilo(dtRegCompra, OrderDataTableColumnsTitles, OrderDataTableColumnsName))
        sb.Append("</body>")
        sb.Append(vbCrLf)
        sb.Append("</html>")
        sb.Append(vbCrLf)

        sw = New StringWriter(sb)

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "MultaVehicular")
    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        pDescargarArchivo()
    End Sub
End Class
