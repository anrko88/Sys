Imports System.Data
Imports System.Web.Services

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports System.IO


Partial Class SituacionCredito_frmSituacionCreditoListado
    Inherits GCCBase

    Private ReadOnly _objLog As New GCCLog("frmSituacionCreditoListado.aspx.vb")

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
            _objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                _objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                Call InicializarListas()

                Call InicializarParametros()
            End If

        Catch ex As ApplicationException
            _objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "')", True)
            Else
                GCCUtilitario.Show(ex.Message, Me)
            End If
        Catch ex As Exception
            _objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try

    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Lee las contsntes y valores iniciales, y las asifgna a controeles ocultos para ser accesibles vía javascript.
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Sub InicializarParametros()
        hddMensajeCorreo.Value = GCCConstante.C_MENSAJE_CORREO_CONTRATO
    End Sub

    ''' <summary>
    ''' Carga las listas desplegables con los datos de las tablas genericas correspondientes.
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Private Sub InicializarListas()
        GCCUtilitario.CargarComboValorGenerico(Me.cmbEjecutivo, GCCConstante.C_TABLAGENERICA_EJECUTIVO)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbClasificacion, GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbClasificacionContrato, GCCConstante.C_TABLAGENERICA_CLASIFICACION_CONTRATO)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbBanca, GCCConstante.C_TABLAGENERICA_BANCA_ATENCION)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoPersona, GCCConstante.C_TABLAGENERICA_TIPO_PERSONA)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbNotaria, GCCConstante.C_TABLAGENERICA_NOTARIA_PUBLICA)


        GCCUtilitario.CargarComboEstadosdelContrato(Me.cmbEstado)
    End Sub

    ''' <summary>
    ''' Consulta a la tabla de contratos.  
    ''' Devuelve un conjunto de resultados hasta un máximo especificado (pPageSize), agrupados en páginas  
    ''' (pCurrentPage). Requiere especificar el nombre de la columna (pSortColumn), puede especificar el orden (pSortOrder).  
    ''' Permite especificar los criterios de búsqueda (parámetros 5to en adelante).
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pContrato">Código (número) de contrato. Dato o parte de él (opcional).</param>
    ''' <param name="pCuCliente">Código del cliente. Dato o parte de él (opcional).</param>
    ''' <param name="pRazonSocial">Nombre o razón social del cliente. Dato o parte de él (opcional). </param>
    ''' <param name="pCotizacion">Código de la cotización</param>
    ''' <param name="pFechaIni">Fecha inicial de registro del contrato. Dato (opcional).</param>
    ''' <param name="pFechaFin">Fecha final de registro del contrato. Dato (opcional).</param>
    ''' <param name="pEjecutivo">Código del ejecutivo de leasing</param>
    ''' <param name="pEstado">Código del estado en el cual se encuentra el contrato. Dato (opcional).</param>
    ''' <param name="pZonal">Código zonal</param>
    ''' <param name="pClasificacion"></param>
    ''' <param name="pClasificacionContrato">Código de clasificación del contrato</param>
    ''' <param name="pCodigoBanca">Código de Banca</param>
    ''' <param name="pTipoPersona"></param>
    ''' <param name="pNotaria">Código notarial</param>
    ''' <param name="pKardex">Número de kardex</param>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function BuscarContratos(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pContrato As String, _
                                           ByVal pCuCliente As String, _
                                           ByVal pRazonSocial As String, _
                                           ByVal pCotizacion As String, _
                                           ByVal pFechaIni As String, _
                                           ByVal pFechaFin As String, _
                                           ByVal pEjecutivo As String, _
                                           ByVal pEstado As String, _
                                           ByVal pZonal As String, _
                                           ByVal pClasificacion As String, _
                                           ByVal pClasificacionContrato As String, _
                                           ByVal pCodigoBanca As String, _
                                           ByVal pTipoPersona As String, _
                                           ByVal pNotaria As String, _
                                           ByVal pKardex As String, _
                                           ByVal pFields As String) As JQGridJsonResponse
        Dim objContratoNTx As New LContratoNTx

        Dim dtContrato As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoContratos(pPageSize, _
                                                                                                                    pCurrentPage, _
                                                                                                                    pSortColumn, _
                                                                                                                    pSortOrder, _
                                                                                                                    pContrato, _
                                                                                                                    pCuCliente, _
                                                                                                                    pRazonSocial, _
                                                                                                                    pCotizacion, _
                                                                                                                    GCCUtilitario.ToStringyyyyMMdd(pFechaIni, ""), _
                                                                                                                    GCCUtilitario.ToStringyyyyMMdd(pFechaFin, ""), _
                                                                                                                    pEjecutivo, _
                                                                                                                    pEstado, _
                                                                                                                    pZonal, _
                                                                                                                    pClasificacion, _
                                                                                                                    pClasificacionContrato, _
                                                                                                                    pCodigoBanca, _
                                                                                                                    pTipoPersona, _
                                                                                                                    pNotaria, _
                                                                                                                    pKardex))

        Dim oJQGridJsonResponse As New JQGridJsonResponse
        'Total de registros a mostrar.
        Dim totalRecords As Integer
        Dim totalCurrent As Integer

        If dtContrato.Rows.Count = 0 Then
            totalRecords = 0
            totalCurrent = 0
        Else
            totalRecords = Convert.ToInt32(dtContrato.Rows(0)("RecordCount"))
            totalCurrent = Convert.ToInt32(dtContrato.Rows(0)("TOTAL_PAGINA"))
        End If
        If pCurrentPage > totalCurrent Then
            pCurrentPage = totalCurrent
        End If

        'Número total de páginas
        Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, _
                                                                     pPageSize)
        Return oJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, _
                                                               pCurrentPage, _
                                                               totalRecords, _
                                                               dtContrato, _
                                                               pFields)

    End Function

    ''' <summary>
    ''' Envía la carta y actualiza los parámetros y el estado del contrato.
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function EnviarCarta(ByVal pCodigoContrato As String) As String
        Dim objContratoTx As New LContratoTx
        Dim objESolicitudCredito As New ESolicitudcredito
        Dim objEgccCarta As New EGcc_carta
        Dim pSolicitudCredito As String
        Dim pEgccCarta As String

        objESolicitudCredito.Codsolicitudcredito = pCodigoContrato
        objESolicitudCredito.Codigoestadocontrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_ENVIADOCLIENTE
        objESolicitudCredito.Codusuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

        objEgccCarta.Codsolicitudcredito = pCodigoContrato
        objEgccCarta.Usuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
        objEgccCarta.Numerocarta = Nothing

        pSolicitudCredito = GCCUtilitario.SerializeObject(objESolicitudCredito)
        pEgccCarta = GCCUtilitario.SerializeObject(objEgccCarta)

        Dim blnResult As Boolean = objContratoTx.EnviarCarta(pSolicitudCredito, _
                                                             pEgccCarta)

        If blnResult Then
            Return "0"
        Else
            Return "1"
        End If
    End Function

    ''' <summary>
    ''' Devuelve el contenido de un mail, reemplazando las etiquetas con los datos del contrato correspondiente.
    ''' Códifica el contenido en su respectivo código utf8.
    ''' </summary>
    ''' <param name="pContrato">Número de contrato</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ContenidoMail(ByVal pContrato As String) As String
        Dim objContratoNTx As New LContratoNTx
        Dim nombreSubprestatario As String = ""
        Dim bienesDescripcion As String

        Dim pstrTitulo As String
        Dim pstrBody As String

        Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.RetornarContratoDatosCliente(pContrato))

        For Each oRow As DataRow In dtCliente.Rows
            If Not oRow.Item("NombreSubprestatario") Is DBNull.Value Then
                nombreSubprestatario = oRow.Item("NombreSubprestatario").ToString().Trim
            End If
        Next oRow
        bienesDescripcion = ListarBienesDescripcion(pContrato)

        pstrTitulo = GCCUtilitario.UnicodeToUtf8("Envío del contrato al cliente " + nombreSubprestatario)
        pstrBody = GCCConstante.C_MENSAJE_CORREO_CONTRATO
        pstrBody = pstrBody.Replace("SGL_NombreContacto", nombreSubprestatario)
        pstrBody = pstrBody.Replace("- SGL_DescripcionBien, SGL_Marca.", bienesDescripcion)
        pstrBody = GCCUtilitario.UnicodeToUtf8(pstrBody)

        Return "1|" + pstrTitulo + "|" + pstrBody
    End Function

    ''' <summary>
    ''' Devuelve un listado con la descripción de todos los bienes incluidos en el contrato
    ''' </summary>
    ''' <param name="pContrato">Número de contrato</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListarBienesDescripcion(ByVal pContrato As String) As String
        Dim objContratoNTx As New LContratoNTx
        Dim sListaBienes As New StringBuilder

        Dim dtBienes As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListarBienes(10000, _
                                                                                                              1, _
                                                                                                              "SecFinanciamiento", _
                                                                                                              "desc", _
                                                                                                              pContrato, _
                                                                                                              Nothing))

        For Each oRow As DataRow In dtBienes.Rows
            If Not oRow.Item("Comentario") Is DBNull.Value Then
                sListaBienes.Append(oRow.Item("Comentario").ToString.Trim)
            End If
        Next oRow

        Return sListaBienes.ToString
    End Function

#End Region
    Private Sub pDescargarArchivo()
        Dim sb As New StringBuilder
        Dim sw As StringWriter
        Dim objContratoNTx As New LContratoNTx
        Dim pTitulo As String = "SITUACIÓN DEL CREDITO"

        Dim dtReporte As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objContratoNTx.ListadoSituacionCreditoContrato(txtContrato.Value, txtCuCliente.Value, _
                                     txtRazonSocial.Value, txtCotizacion.Value, txtFechaIni.Value, txtFechaFin.Value, cmbEjecutivo.Value, cmbEstado.Value, _
                                     txtZonal.Value, cmbClasificacion.Value, cmbClasificacionContrato.Value, cmbBanca.Value, cmbTipoPersona.Value, cmbNotaria.Value, txtKardex.Value))


        Dim OrderHeaderTableColumnsLabels As New ArrayList
        Dim OrderHeaderTableColumnsText As New ArrayList
        Dim OrderDataTableColumnsTitles As New ArrayList
        Dim OrderDataTableColumnsName As New ArrayList

        '**** Titulos de las Columnas de la tabla HTML ****
        OrderDataTableColumnsTitles.Add("Nº Contrato")
        OrderDataTableColumnsTitles.Add("Nº Cotización")
        OrderDataTableColumnsTitles.Add("CU Cliente")
        OrderDataTableColumnsTitles.Add("Razón Social o Nombre")
        OrderDataTableColumnsTitles.Add("Tipo de Persona")
        OrderDataTableColumnsTitles.Add("Tipo de Documento")
        OrderDataTableColumnsTitles.Add("Nº de Documento")
        OrderDataTableColumnsTitles.Add("Clasificación del Bien")
        OrderDataTableColumnsTitles.Add("Tipo de Bien")
        OrderDataTableColumnsTitles.Add("Estado del Contrato")
        OrderDataTableColumnsTitles.Add("Fecha del Contrato")
        OrderDataTableColumnsTitles.Add("Moneda")
        OrderDataTableColumnsTitles.Add("Precio de Venta")
        OrderDataTableColumnsTitles.Add("Ejecutivo Leasing")
        OrderDataTableColumnsTitles.Add("Clasificación de Contrato")
        'OrderDataTableColumnsTitles.Add("Nº de Serie")
        'OrderDataTableColumnsTitles.Add("Motor")
        'OrderDataTableColumnsTitles.Add("Marca")
        'OrderDataTableColumnsTitles.Add("Modelo")
        'OrderDataTableColumnsTitles.Add("Placa Actual")
        OrderDataTableColumnsTitles.Add("T.E.A. %")
        OrderDataTableColumnsTitles.Add("Precuota %")
        'OrderDataTableColumnsTitles.Add("Código del Predio")
        'OrderDataTableColumnsTitles.Add("Estado Inscripción Municipal")
        'OrderDataTableColumnsTitles.Add("Estado Inscripción Registral")
        'OrderDataTableColumnsTitles.Add("Propiedad")
        

        '**** Nombres de las Columnas del DataTable ****
        OrderDataTableColumnsName.Add("CodSolicitudCredito")
        OrderDataTableColumnsName.Add("CodigoCotizacion|1")
        OrderDataTableColumnsName.Add("CodUnico|1")
        OrderDataTableColumnsName.Add("ClienteRazonSocial|3")
        OrderDataTableColumnsName.Add("TipoPersona")
        OrderDataTableColumnsName.Add("CodigoTipoDocumento")
        OrderDataTableColumnsName.Add("NumeroDocumento")
        OrderDataTableColumnsName.Add("ClasificacionBien")
        OrderDataTableColumnsName.Add("TipoBien|1")
        OrderDataTableColumnsName.Add("CodigoEstadoContrato")
        'OrderDataTableColumnsName.Add("")
        OrderDataTableColumnsName.Add("FechaContrato")
        OrderDataTableColumnsName.Add("NombreMoneda")
        OrderDataTableColumnsName.Add("PrecioVenta")
        OrderDataTableColumnsName.Add("CodigoEjecutivoLeasing")
        OrderDataTableColumnsName.Add("CodigoClasificacionContrato")
        'OrderDataTableColumnsName.Add("FechaRegistroPublico")
        'OrderDataTableColumnsName.Add("FechaFirmaNotaria")
        OrderDataTableColumnsName.Add("TEAPorc")
        OrderDataTableColumnsName.Add("PrecuotaPorc")
        'OrderDataTableColumnsName.Add("Anio")
        'OrderDataTableColumnsName.Add("Comentario")
        'OrderDataTableColumnsName.Add("CodigoPredio")
        'OrderDataTableColumnsName.Add("CodEstadoMunicipal")
        'OrderDataTableColumnsName.Add("CodEstadoInscripcionRRPP")
        'OrderDataTableColumnsName.Add("Propiedad")
        'OrderDataTableColumnsName.Add("CodEstadoBien|2")
        'OrderDataTableColumnsName.Add("FechaBaja")

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
        sb.Append(GCCUtilitario.fHTMLGeneraTablaCabecera(OrderHeaderTableColumnsLabels, OrderHeaderTableColumnsText, OrderDataTableColumnsName.Count))
        sb.Append(" <br>")
        sb.Append(vbCrLf)
        sb.Append(GCCUtilitario.fHTMLGeneraTablaEstilo(dtReporte, OrderDataTableColumnsTitles, OrderDataTableColumnsName))
        sb.Append("</body>")
        sb.Append(vbCrLf)
        sb.Append("</html>")
        sb.Append(vbCrLf)

        sw = New StringWriter(sb)

        GCCUtilitario.pDescargaArchivoPagina(Me, sw.ToString(), "Situación del Credito")
    End Sub

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        pDescargarArchivo()
    End Sub
End Class
