Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS
Imports System.IO

Partial Class Consultas_Siniestro_frmSiniestroListadoConsulta
    Inherits GCCBase

    Dim objLog As New GCCLog("frmSiniestroListadoConsulta.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then

                GCCUtilitario.CargarComboValorGenerico(Me.cmdTipoDoc, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
                'GCCUtilitario.CargarComboValorGenerico(Me.cmdTipoBien, GCCConstante.C_TABLAGENERICA_TIPO_BIEN)
                GCCUtilitario.CargarComboValorGenerico(Me.cmdEstadoContrato, GCCConstante.C_TABLAGENERICA_ESTADO_CONTRATO)
                GCCUtilitario.CargarComboValorGenerico(Me.cmdClasificacion, GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)

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

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim strFechaIni As String = String.Empty
        Dim strFechaFin As String = String.Empty
        Dim table As DataTable = Nothing

        Dim objSiniestro As LSiniestroNTx = Nothing

        Try
            objSiniestro = New LSiniestroNTx()
            Dim strESiniestro As String = String.Empty

            'Inicializa Objeto
            Dim objESiniestro As New ESiniestro

            With objESiniestro
                .NroContrato = txtNroContrato.Value
                .EstadoContrato = IIf(cmdEstadoContrato.Value = "0", "", cmdEstadoContrato.Value)
                .CUCliente = IIf(txtCUCliente.Value = String.Empty, String.Empty, txtCUCliente.Value.Trim.PadLeft(10, "0"c))
                .TipoDocumento = IIf(cmdTipoDoc.Value = "0", "", cmdTipoDoc.Value)
                .NroDocumento = txtNroDocumento.Value
                .RazonSocial = txtRazonSocial.Value
                .ClasificacionBien = IIf(cmdClasificacion.Value = "0", "", cmdClasificacion.Value)
                .Placa = txtPlaca.Value
                .NroMotor = txtMotor.Value
                .TipoBien = IIf(hidTipoBien.Value = "0", "", hidTipoBien.Value)
                .Ubicacion = txtUbicacion.Value
            End With
            strESiniestro = GCCUtilitario.SerializeObject(Of ESiniestro)(objESiniestro)


            table = GCCUtilitario.DeserializeObject(Of DataTable)(objSiniestro.ListadoReporteSiniestro(strESiniestro))
            Me.pDescargarArchivo(table)
        Catch ex As Exception
            'Throw ex
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "btnGenerar_Click")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        Finally
            If objSiniestro IsNot Nothing Then objSiniestro = Nothing
            If table IsNot Nothing Then
                table.Dispose()
                table = Nothing
            End If
            strFechaIni = String.Empty
            strFechaFin = String.Empty
        End Try
    End Sub

#End Region

#Region "WebMethods"

    ''' <summary>
    ''' ListaSiniestro
    ''' </summary>
    ''' <returns>Listado de Siniestro</returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaSiniestro(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pstrNroContrato As String, _
                                         ByVal pstrEstadoContrato As String, _
                                         ByVal pstrCUCliente As String, _
                                         ByVal pstrTipoDoc As String, _
                                         ByVal pstrNroDocumento As String, _
                                         ByVal pstrRazonSocial As String, _
                                         ByVal pstrClasificacion As String, _
                                         ByVal pstrPlaca As String, _
                                         ByVal pstrMotor As String, _
                                         ByVal pstrTipoBien As String, _
                                         ByVal pstrUbicacion As String) As JQGridJsonResponse

        'Variables
        Dim objSiniestroNTx As New LSiniestroNTx

        Try

            'Valida Campos            
            Dim strCodUnico As String = GCCUtilitario.NullableString(pstrCUCliente)
            If Not strCodUnico Is Nothing Then
                strCodUnico = strCodUnico.Trim.PadLeft(10, "0"c)
            End If


            'Inicializa Objeto
            Dim objESiniestro As New ESiniestro
            Dim strESiniestro As String
            With objESiniestro
                .NroContrato = GCCUtilitario.NullableString(pstrNroContrato)
                .EstadoContrato = GCCUtilitario.NullableStringCombo(pstrEstadoContrato)
                .CUCliente = GCCUtilitario.NullableString(pstrCUCliente)
                .TipoDocumento = GCCUtilitario.NullableStringCombo(pstrTipoDoc)
                .NroDocumento = GCCUtilitario.NullableString(pstrNroDocumento)
                .RazonSocial = GCCUtilitario.NullableString(pstrRazonSocial)
                .ClasificacionBien = GCCUtilitario.NullableStringCombo(pstrClasificacion)
                .Placa = GCCUtilitario.NullableString(pstrPlaca)
                .NroMotor = GCCUtilitario.NullableString(pstrMotor)
                .TipoBien = GCCUtilitario.NullableStringCombo(pstrTipoBien)
                .Ubicacion = GCCUtilitario.NullableString(pstrUbicacion)
            End With
            strESiniestro = GCCUtilitario.SerializeObject(Of ESiniestro)(objESiniestro)

            'Ejecuta Consulta
            Dim dtSiniestro As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objSiniestroNTx.ListadoSiniestroContrato(pPageSize, _
                                                                                                                                   pCurrentPage, _
                                                                                                                                   pSortColumn, _
                                                                                                                                   pSortOrder, _
                                                                                                                                   strESiniestro))
            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtSiniestro.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtSiniestro.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtSiniestro.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtSiniestro)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

#Region "   Metodos     "

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

        Dim pTitulo As String = "SINIESTRO"

        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList

        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("Nº Contrato")
        OrderDataTableColumnsTitles.Add("CU Cliente")
        OrderDataTableColumnsTitles.Add("Razón Social o Nombre")
        OrderDataTableColumnsTitles.Add("Estado del Contrato")
        OrderDataTableColumnsTitles.Add("Clasificación del Bien")
        OrderDataTableColumnsTitles.Add("Tipo del Bien")
        OrderDataTableColumnsTitles.Add("Nº Siniestro")
        OrderDataTableColumnsTitles.Add("F. Conocimiento Banco")
        OrderDataTableColumnsTitles.Add("F. Conocimiento Leasing")
        OrderDataTableColumnsTitles.Add("Fecha Siniestro")
        OrderDataTableColumnsTitles.Add("Tipo")
        OrderDataTableColumnsTitles.Add("F. Última Actualización")
        OrderDataTableColumnsTitles.Add("Situación")
        OrderDataTableColumnsTitles.Add("Modificación de Contrato")
        OrderDataTableColumnsTitles.Add("F. Aplicación")
        OrderDataTableColumnsTitles.Add("Aplicación")
        OrderDataTableColumnsTitles.Add("F. Descargo Municipal")
        OrderDataTableColumnsTitles.Add("Seguro")
        OrderDataTableColumnsTitles.Add("Estado del Bien")
        OrderDataTableColumnsTitles.Add("Nº Póliza")
        OrderDataTableColumnsTitles.Add("Tipo de Póliza")
        OrderDataTableColumnsTitles.Add("F. Rec. Indemnización")
        OrderDataTableColumnsTitles.Add("Moneda Indemnización")
        OrderDataTableColumnsTitles.Add("Monto Indemnización")
        OrderDataTableColumnsTitles.Add("Banco que Emite el Cheque")

        '**** Nombres de las Columnas del DataTable ****
        OrderDataTableColumnsName.Add("CodSolicitudCredito")
        OrderDataTableColumnsName.Add("CodUnico|1")
        OrderDataTableColumnsName.Add("ClienteRazonSocial|3")
        OrderDataTableColumnsName.Add("EstadoContrato")
        OrderDataTableColumnsName.Add("ClasificacionBien")
        OrderDataTableColumnsName.Add("TipoBien")
        OrderDataTableColumnsName.Add("NroSiniestro|1")
        OrderDataTableColumnsName.Add("FecConocimientoBanco")
        OrderDataTableColumnsName.Add("FecConocimiento")
        OrderDataTableColumnsName.Add("FecSiniestro")
        OrderDataTableColumnsName.Add("DesTipo")
        OrderDataTableColumnsName.Add("FecSituacion")
        OrderDataTableColumnsName.Add("DesSituacion")
        OrderDataTableColumnsName.Add("DesContrato")
        OrderDataTableColumnsName.Add("FecAplicacion")
        OrderDataTableColumnsName.Add("DesAplicacion")
        OrderDataTableColumnsName.Add("FecDescargoMunicipal")
        OrderDataTableColumnsName.Add("DesSeguro")
        OrderDataTableColumnsName.Add("DesEstadoBien")
        OrderDataTableColumnsName.Add("NroPoliza|1")
        OrderDataTableColumnsName.Add("DesTipoPoliza")
        OrderDataTableColumnsName.Add("FecRecIndemnizacion")
        OrderDataTableColumnsName.Add("NombreMoneda")
        OrderDataTableColumnsName.Add("MontoIndemnizacion|2")
        OrderDataTableColumnsName.Add("DesBancoEmiteCheque")

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

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "Siniestro")
    End Sub

#End Region


End Class
