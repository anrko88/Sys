﻿Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports System.IO

Partial Class GestionBien_ImpuestoVehicular_frmImpuestoVehicularLiquidar
    Inherits GCCBase
    Dim objLog As New GCCLog("frmImpuestoVehicularListado.aspx.vb")

#Region "   Eventos     "

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

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim table As DataTable = Nothing
        Dim objImpuestoVehicular As LImpuestoVehicularNTX = Nothing
        Try
            objImpuestoVehicular = New LImpuestoVehicularNTX
            table = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicular.ListarImpuestoVehicularReporteLiquidar(hidCodigos.Value))
            Me.pDescargarArchivo(table)
        Catch ex As Exception
            'Throw ex
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGenerar_Click")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        Finally
            If objImpuestoVehicular IsNot Nothing Then objImpuestoVehicular = Nothing
            If table IsNot Nothing Then
                table.Dispose()
                table = Nothing
            End If
        End Try
    End Sub
#End Region

#Region "   Web Métodos "
    <WebMethod()> _
    Public Shared Function ListaImpuestoVehicularLiquidar(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pNroLote As String) As JQGridJsonResponse
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        Dim oEImpuestoVehicular As New EImpuestovehicular
        'Valida Campos            

        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarImpuestoVehicularLiquidar(pPageSize, _
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
    Public Shared Function ListaImpuestoVehicularLiquidarTodo(ByVal pNroLote As String) As JQGridJsonResponse
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        Dim oEImpuestoVehicular As New EImpuestovehicular
        'Valida Campos            

        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ListarImpuestoVehicularLiquidarTodo(pNroLote))


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
        Dim flag As String = GCCConstante.C_LOTE_IMP_VEHICULAR
        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ObtenerHeaderLote(pNroLote, flag))


        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(1, 1, 1000, dtImpuestoVehicular)

    End Function
    <WebMethod()> _
   Public Shared Function ObtenerHeaderLote2(ByVal pNroLote As String) As EImpuestovehicular
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        Dim oEImpuestoVehicular As New EImpuestovehicular
        'Valida Campos            
        Dim flag As String = GCCConstante.C_LOTE_IMP_VEHICULAR
        Dim dtImpuestoVehicular As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objImpuestoVehicularTx.ObtenerHeaderLote(pNroLote, flag))
        If (dtImpuestoVehicular.Rows.Count > 0) Then
            oEImpuestoVehicular.CodNroLote = IIf(String.IsNullOrEmpty(dtImpuestoVehicular.Rows(0).Item("CodNroLote").ToString()), "", dtImpuestoVehicular.Rows(0).Item("CodNroLote").ToString())
            oEImpuestoVehicular.DescCodEstadoLote = IIf(String.IsNullOrEmpty(dtImpuestoVehicular.Rows(0).Item("DescCodEstadoLote").ToString()), "", dtImpuestoVehicular.Rows(0).Item("DescCodEstadoLote").ToString())
            oEImpuestoVehicular.CodEstadoLote = IIf(String.IsNullOrEmpty(dtImpuestoVehicular.Rows(0).Item("CodEstadoLote").ToString()), "", dtImpuestoVehicular.Rows(0).Item("CodEstadoLote").ToString())
            oEImpuestoVehicular.TotalLote = GCCUtilitario.ConvierteValorBien(IIf(String.IsNullOrEmpty(dtImpuestoVehicular.Rows(0).Item("TotalLote").ToString()), "0", dtImpuestoVehicular.Rows(0).Item("TotalLote").ToString()))
            oEImpuestoVehicular.FechaPagoLote = IIf(String.IsNullOrEmpty(dtImpuestoVehicular.Rows(0).Item("FechaPago").ToString()), "01/01/1999", dtImpuestoVehicular.Rows(0).Item("FechaPago").ToString())
            oEImpuestoVehicular.DescConcepto = IIf(String.IsNullOrEmpty(dtImpuestoVehicular.Rows(0).Item("DescConcepto").ToString()), "", dtImpuestoVehicular.Rows(0).Item("DescConcepto").ToString())
            oEImpuestoVehicular.CodigoConcepto = IIf(String.IsNullOrEmpty(dtImpuestoVehicular.Rows(0).Item("CodigoConcepto").ToString()), "", dtImpuestoVehicular.Rows(0).Item("CodigoConcepto").ToString())

            oEImpuestoVehicular.MontoDevuelto = IIf(String.IsNullOrEmpty(dtImpuestoVehicular.Rows(0).Item("DevueltoLote").ToString()), "0.00", dtImpuestoVehicular.Rows(0).Item("DevueltoLote").ToString())
            oEImpuestoVehicular.MontoReembolso = IIf(String.IsNullOrEmpty(dtImpuestoVehicular.Rows(0).Item("ReembolsoLote").ToString()), "0.00", dtImpuestoVehicular.Rows(0).Item("ReembolsoLote").ToString())
            oEImpuestoVehicular.MontoCheque = IIf(String.IsNullOrEmpty(dtImpuestoVehicular.Rows(0).Item("MontoCheque").ToString()), "0.00", dtImpuestoVehicular.Rows(0).Item("MontoCheque").ToString())
            oEImpuestoVehicular.FechaCobro = IIf(String.IsNullOrEmpty(dtImpuestoVehicular.Rows(0).Item("FechaCobro").ToString()), "", dtImpuestoVehicular.Rows(0).Item("FechaCobro").ToString())
            oEImpuestoVehicular.Nrocheque = IIf(String.IsNullOrEmpty(dtImpuestoVehicular.Rows(0).Item("NroCheque").ToString()), "", dtImpuestoVehicular.Rows(0).Item("NroCheque").ToString())
        End If
        Return oEImpuestoVehicular

    End Function
    'Fin IBK

    'Inicion IBK - JJM - 15/02/2013 - Liquido el lote
    <WebMethod()> _
  Public Shared Function LiquidarLote(ByVal strNroLote As String) As String
        Dim blnResult As Boolean = False
        Dim Resultado As String = String.Empty
        Dim objLImpuestoVehicular As New LImpuestoVehicularTX
        Try
            Dim pstrNroLote As String = strNroLote.Trim.PadLeft(8, "0"c)
            Dim pstrUsuarioModificacion As String = GCCSession.CodigoUsuario
            Dim pstrCodigoConcepto As String = GCCConstante.C_CONCEPTO_IMPUESTO_VEHICULAR
            'Ejecuta Consulta
            Resultado = objLImpuestoVehicular.LiquidarLote(pstrUsuarioModificacion, strNroLote, pstrCodigoConcepto)
            Return Resultado
        Catch ex As Exception
            Return ex.ToString() + "|-1"
        End Try
    End Function
    'Fin IBK
    'Inicio IBK - AAE - 13/02/2013
    <WebMethod()> _
   Public Shared Function CheckLote(ByVal pNroLote As String) As String
        Dim objImpuestoVehicularTx As New LImpuestoVehicularNTX
        Dim flag As String = GCCConstante.C_LOTE_IMP_VEHICULAR ' Para impuesto inmueble el flag es 2
        Dim res As String = objImpuestoVehicularTx.CheckLote(pNroLote, flag)
        Return res
    End Function
    'Fin IBK
#End Region

#Region "   Metodos     "
    Private Sub pDescargarArchivo(ByVal ptabla As DataTable)
        Dim sb As New StringBuilder
        Dim sw As StringWriter

        Dim pTitulo As String = "REPORTE LIQUIDAR IMPUESTO VEHICULAR"

        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList
        Dim ColumnModo As New ArrayList

        '**** Etiquetas de la cabecera ****
        OrderHeaderTableColumnsLabels.Add("N° Lote")
        OrderHeaderTableColumnsLabels.Add("Total")

        '**** Texto de cada etiqueta ****
        OrderHeaderTableColumnsText.Add(hidNroLote.Value)
        OrderHeaderTableColumnsText.Add(hidTotal.Value)

        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("Nº Contrato")
        OrderDataTableColumnsTitles.Add("CU Cliente")
        OrderDataTableColumnsTitles.Add("Razón Social o Nombre")
        OrderDataTableColumnsTitles.Add("Tipo del Bien")
        OrderDataTableColumnsTitles.Add("Periodo")
        OrderDataTableColumnsTitles.Add("Nº Cuota")
        OrderDataTableColumnsTitles.Add("Distrito / Municipalidad")
        OrderDataTableColumnsTitles.Add("Placa")
        OrderDataTableColumnsTitles.Add("Marca")
        OrderDataTableColumnsTitles.Add("Modelo")
        OrderDataTableColumnsTitles.Add("Importe")
        OrderDataTableColumnsTitles.Add("Estado de Pago")
        OrderDataTableColumnsTitles.Add("Estado de Cobro")

        '**** Nombres de las Columnas del DataTable ****
        OrderDataTableColumnsName.Add("CodSolicitudCredito")
        OrderDataTableColumnsName.Add("CodUnico|1")
        OrderDataTableColumnsName.Add("RazonSocial|3")
        OrderDataTableColumnsName.Add("TipoBien")
        OrderDataTableColumnsName.Add("Periodo")
        OrderDataTableColumnsName.Add("NroCuota")
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
        sb.Append(GCCUtilitario.fHTMLGeneraTablaTitulo(pTitulo, Me, OrderDataTableColumnsName.Count))
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

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "ImpuestoVehicularLiquidar")
    End Sub
#End Region
End Class
