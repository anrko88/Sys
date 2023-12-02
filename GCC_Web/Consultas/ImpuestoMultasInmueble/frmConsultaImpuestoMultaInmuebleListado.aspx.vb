Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS
Imports System.IO

Partial Class Consultas_ImpuestoMultasInmueble_frmConsultaImpuestoMultaInmuebleListado
    Inherits GCCBase

    Dim objLog As New GCCLog("frmConsultaImpuestoMultaInmuebleListado.aspx.vb")
    Dim strNroContrato As String

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

                GCCUtilitario.CargarComboValorGenerico(Me.cmdTipoDoc, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
                GCCUtilitario.CargarDepartamento(Me.cmbDepartamento)
                GCCUtilitario.CargarComboValorGenerico(Me.cmdEstadoPago, GCCConstante.C_TABLAGENERICA_Estado_Pago)
                GCCUtilitario.CargarComboValorGenerico(Me.cmdEstadoCobro, GCCConstante.C_TABLAGENERICA_Estado_Cobro)

            End If

        Catch ex As ApplicationException
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "')", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub

    ''' <summary>
    ''' Evento para Generar Excel
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - SCA
    ''' Fecha de Creación  : 28/01/2013
    ''' </remarks>
    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click

        'Variables
        Dim objLImpuestoMunicipalNTx As New LImpuestoMunicipalNTx

        Try

            'Inicializa Objeto
            Dim objEImpuestomunicipal As New EImpuestomunicipal
            Dim strEImpuestomunicipal As String
            With objEImpuestomunicipal
                .Departamento = GCCUtilitario.NullableStringCombo(cmbDepartamento.Value)
                .Provincia = GCCUtilitario.NullableStringCombo(cmbProvincia.Value)
                .Distrito = GCCUtilitario.NullableStringCombo(cmbDistrito.Value)
                .NroContrato = GCCUtilitario.NullableString(txtNroContrato.Value)
                .RazonSocial = GCCUtilitario.NullableString(txtRazonSocial.Value)
                .TipoDocumento = GCCUtilitario.NullableStringCombo(cmdTipoDoc.Value)
                .NroDocumento = GCCUtilitario.NullableString(txtNroDocumento.Value)
                .Periodo = GCCUtilitario.NullableString(txtPeriodo.Value)
                .Lote = GCCUtilitario.NullableString(txtLote.Value)
                .EstadoPago = GCCUtilitario.NullableStringCombo(cmdEstadoPago.Value)
                .EstadoCobro = GCCUtilitario.NullableStringCombo(cmdEstadoCobro.Value)
            End With
            strEImpuestomunicipal = GCCUtilitario.SerializeObject(Of EImpuestomunicipal)(objEImpuestomunicipal)

            'Ejecuta Consulta ListadoReporteSiniestro
            Dim dtImpMunicipal As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoMunicipalNTx.ListadoReporteImpuestoMunicipal(strEImpuestomunicipal))
            Me.pDescargarArchivo(dtImpMunicipal)


        Catch ex As Exception
            'Throw ex
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGenerar_Click")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        Finally
        End Try
    End Sub

#End Region

#Region "WebMethods"

    ''' <summary>
    ''' ListaImpuestoMunicipal
    ''' </summary>
    ''' <returns>Listado de Impuestos y Multas Municipales</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaImpuestoMunicipal(ByVal pPageSize As Integer, _
                                                     ByVal pCurrentPage As Integer, _
                                                     ByVal pSortColumn As String, _
                                                     ByVal pSortOrder As String, _
                                                     ByVal pstrDepartamento As String, _
                                                     ByVal pstrProvincia As String, _
                                                     ByVal pstrDistrito As String, _
                                                     ByVal pstrNroContrato As String, _
                                                     ByVal pstrRazonSocial As String, _
                                                     ByVal pstrTipoDoc As String, _
                                                     ByVal pstrDocumento As String, _
                                                     ByVal pstrPeriodo As String, _
                                                     ByVal txtLote As String, _
                                                     ByVal pstrEstadoPago As String, _
                                                     ByVal pstrEstadoCobro As String) As JQGridJsonResponse

        'Variables
        Dim objLImpuestoMunicipalNTx As New LImpuestoMunicipalNTx

        Try

            'Inicializa Objeto
            Dim objEImpuestomunicipal As New EImpuestomunicipal
            Dim strEImpuestomunicipal As String
            With objEImpuestomunicipal
                .Departamento = GCCUtilitario.NullableStringCombo(pstrDepartamento)
                .Provincia = GCCUtilitario.NullableStringCombo(pstrProvincia)
                .Distrito = GCCUtilitario.NullableStringCombo(pstrDistrito)
                .NroContrato = GCCUtilitario.NullableString(pstrNroContrato)
                .RazonSocial = GCCUtilitario.NullableString(pstrRazonSocial)
                .TipoDocumento = GCCUtilitario.NullableStringCombo(pstrTipoDoc)
                .NroDocumento = GCCUtilitario.NullableString(pstrDocumento)
                .Periodo = GCCUtilitario.NullableString(pstrPeriodo)
                .Lote = GCCUtilitario.NullableString(txtLote)
                .EstadoPago = GCCUtilitario.NullableStringCombo(pstrEstadoPago)
                .EstadoCobro = GCCUtilitario.NullableStringCombo(pstrEstadoCobro)
            End With
            strEImpuestomunicipal = GCCUtilitario.SerializeObject(Of EImpuestomunicipal)(objEImpuestomunicipal)

            'Ejecuta Consulta
            Dim dtImpMunicipal As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLImpuestoMunicipalNTx.ListadoImpuestoMunicipal(pPageSize, _
                                                                                                                                           pCurrentPage, _
                                                                                                                                           pSortColumn, _
                                                                                                                                           pSortOrder, _
                                                                                                                                           strEImpuestomunicipal))


            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtImpMunicipal.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtImpMunicipal.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtImpMunicipal.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtImpMunicipal)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

#Region "Metodos"

    Private Shared Function TotalPaginas(ByVal total As Integer, ByVal pPageSize As Integer) As Integer
        If (total Mod pPageSize > 0) Then
            Return total \ pPageSize + 1
        Else
            Return total \ pPageSize
        End If
    End Function

    Private Sub pDescargarArchivo(ByVal ptabla As DataTable)
        Dim sb As New StringBuilder
        Dim sw As StringWriter

        'Dim pMe As System.Web.UI.Page
        Dim pTitulo As String = "IMPUESTO MUNICIPAL"

        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList

        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("Nº Contrato")
        OrderDataTableColumnsTitles.Add("CU Cliente")
        OrderDataTableColumnsTitles.Add("Razón Social o Nombre")
        OrderDataTableColumnsTitles.Add("Tipo de Documento")
        OrderDataTableColumnsTitles.Add("Nº de Documento")
        OrderDataTableColumnsTitles.Add("Departamento")
        OrderDataTableColumnsTitles.Add("Provincia")
        OrderDataTableColumnsTitles.Add("Distrito")
        OrderDataTableColumnsTitles.Add("Ubicación")
        OrderDataTableColumnsTitles.Add("Lote")
        OrderDataTableColumnsTitles.Add("Periodo")
        OrderDataTableColumnsTitles.Add("Total Autovaluo")
        OrderDataTableColumnsTitles.Add("Total Predial")
        OrderDataTableColumnsTitles.Add("Código del Predio")
        OrderDataTableColumnsTitles.Add("Autovaluo")
        OrderDataTableColumnsTitles.Add("Impuesto Predial")
        OrderDataTableColumnsTitles.Add("Arbitrio")
        OrderDataTableColumnsTitles.Add("Multa")
        OrderDataTableColumnsTitles.Add("Fiscalización")
        OrderDataTableColumnsTitles.Add("Importe Total")
        OrderDataTableColumnsTitles.Add("Pago Cliente")
        OrderDataTableColumnsTitles.Add("F. Pago")
        OrderDataTableColumnsTitles.Add("Estado Pago")
        OrderDataTableColumnsTitles.Add("F. Cobro")
        OrderDataTableColumnsTitles.Add("Estado Cobro")
        OrderDataTableColumnsTitles.Add("Observación")

        '**** Nombres de las Columnas del DataTable ****

        OrderDataTableColumnsName.Add("NroContrato")
        OrderDataTableColumnsName.Add("CUCliente|1")
        OrderDataTableColumnsName.Add("RazonSocialNombre|3")
        OrderDataTableColumnsName.Add("TipoDocumento|3")
        OrderDataTableColumnsName.Add("NroDocumento|1")
        OrderDataTableColumnsName.Add("Departamento|3")
        OrderDataTableColumnsName.Add("Provincia|3")
        OrderDataTableColumnsName.Add("Distrito|3")
        OrderDataTableColumnsName.Add("Ubicacion|3")
        OrderDataTableColumnsName.Add("Lote|1")
        OrderDataTableColumnsName.Add("Periodo")
        OrderDataTableColumnsName.Add("TotalAutovaluo|2")
        OrderDataTableColumnsName.Add("TotalPredial|2")
        OrderDataTableColumnsName.Add("CodigoPredio")
        OrderDataTableColumnsName.Add("Autovaluo|2")
        OrderDataTableColumnsName.Add("ImpuestoPredial|2")
        OrderDataTableColumnsName.Add("Arbitrio|2")
        OrderDataTableColumnsName.Add("Multa|2")
        OrderDataTableColumnsName.Add("Fiscalizacion|2")
        OrderDataTableColumnsName.Add("ImporteTotal|2")
        OrderDataTableColumnsName.Add("PagoCliente")
        OrderDataTableColumnsName.Add("FecPago")
        OrderDataTableColumnsName.Add("EstadoPago")
        OrderDataTableColumnsName.Add("FecCobro")
        OrderDataTableColumnsName.Add("EstadoCobro")
        OrderDataTableColumnsName.Add("Observacion|3")

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

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "ImpuestoMunicipal")
    End Sub

#End Region

End Class


