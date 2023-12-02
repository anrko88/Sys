Imports System.Data
Imports System.Web.Services

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class GestionBien_OtrosConceptos_frmCobroListado
    Inherits GCCBase
    Dim objLog As New GCCLog("frmCobroListado.aspx.vb")

#Region "   Eventos     "

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 27/11/2012
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
                InicializaCombos()
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

#Region "   Web Métodos "
    '''' <summary>
    '''' Devuelve el conjunto de cobros asociados a contratos hasta un máximo especificado (PageSize), agrupados en páginas
    '''' (CurrentPage). Permite especificar el orden (SortOrder).
    '''' </summary>
    '''' <param name="pPageSize">Tamaño de página</param>
    '''' <param name="pCurrentPage">Página actual</param>
    '''' <param name="pSortColumn">Ordenar por columna</param>
    '''' <param name="pSortOrder">Dirección de ordenamiento</param>
    '''' <param name="pstrCodigoTipoDocumento">Código tipo de documento</param>
    '''' <param name="pstrNroDocumento">Número de documento</param>
    '''' <param name="pstrRazonSocial">Razón social</param>
    '''' <param name="pstrNroContrato">Número de contrato</param>
    '''' <param name="pstrNroLote">Número de lote</param>
    '''' <param name="pstrCodigoConcepto">Código de concepto</param>
    '''' <returns>Listado de Cobro(Serializado)</returns>
    '''' <remarks>
    '''' Creado Por         : TSF - WCR
    '''' Fecha de Creación  : 27/11/2012
    '''' </remarks>
    '<WebMethod()> _
    'Public Shared Function BuscarCobros(ByVal pPageSize As Integer, _
    '                                       ByVal pCurrentPage As Integer, _
    '                                       ByVal pSortColumn As String, _
    '                                       ByVal pSortOrder As String, _
    '                                       ByVal pstrCodigoTipoDocumento As String, _
    '                                       ByVal pstrNroDocumento As String, _
    '                                       ByVal pstrRazonSocial As String, _
    '                                       ByVal pstrNroContrato As String, _
    '                                       ByVal pstrNroLote As String, _
    '                                       ByVal pstrCodigoConcepto As String) As JQGridJsonResponse
    '    Dim objCobroNTx As New LCobroNTx
    '    Dim strECreditoRecuperacionComision As String = String.Empty
    '    Dim objCreditoRecuperacionComision As New ECreditoRecuperacionComision

    '    With objCreditoRecuperacionComision
    '        .CodOperacionActiva = pstrNroContrato
    '        .CodigoTipoDocumento = pstrCodigoTipoDocumento
    '        .NumeroDocumento = pstrNroDocumento
    '        .RazonSocial = pstrRazonSocial
    '        .NumeroLote = pstrNroLote
    '        .CodComisionTipo = pstrCodigoConcepto
    '    End With

    '    strECreditoRecuperacionComision = GCCUtilitario.SerializeObject(Of ECreditoRecuperacionComision)(objCreditoRecuperacionComision)

    '    Dim dtbCobro As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCobroNTx.ListadoCobro(pPageSize, _
    '                                                                                                        pCurrentPage, _
    '                                                                                                        pSortColumn, _
    '                                                                                                        pSortOrder, _
    '                                                                                                        strECreditoRecuperacionComision))

    '    ' Número de página actual.
    '    Dim currentPage As Integer = pCurrentPage
    '    Dim intTotalCurrent As Int32
    '    ' Total de registros a mostrar.
    '    Dim totalRecords As Integer
    '    If dtbCobro.Rows.Count = 0 Then
    '        totalRecords = 0
    '        intTotalCurrent = 1
    '    Else
    '        totalRecords = Convert.ToInt32(dtbCobro.Rows(0)("RecordCount"))
    '        intTotalCurrent = Convert.ToInt32(dtbCobro.Rows(0)("TOTAL_PAGINA"))
    '    End If

    '    If currentPage > intTotalCurrent Then
    '        currentPage = intTotalCurrent
    '    End If

    '    ' Número total de páginas
    '    Dim objJQGridJsonResponse As New JQGridJsonResponse
    '    Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
    '    Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtbCobro)

    'End Function

    ''' <summary>
    ''' Devuelve el conjunto de cobros asociados a contratos hasta un máximo especificado (PageSize), agrupados en páginas
    ''' (CurrentPage). Permite especificar el orden (SortOrder).
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de página</param>
    ''' <param name="pCurrentPage">Página actual</param>
    ''' <param name="pSortColumn">Ordenar por columna</param>
    ''' <param name="pSortOrder">Dirección de ordenamiento</param>
    ''' <param name="pstrCUCliente">Código único cliente</param>
    ''' <param name="pstrRazonSocial">Razón social</param>
    ''' <param name="pstrNroContrato">Número de contrato</param>
    ''' <param name="pstrNroLote">Número de lote</param>
    ''' <param name="pstrCodigoConcepto">Código de concepto</param>
    ''' <param name="pstrEstadoCobro">Código tipo de documento</param>
    ''' <param name="pstrFlagIndividual">Flag individual</param>
    ''' <param name="pstrFlagRegistro">Flag registro</param>
    ''' <returns>Listado de Cobro(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 27/11/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function BuscarCobros(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pstrCUCliente As String, _
                                           ByVal pstrRazonSocial As String, _
                                           ByVal pstrNroContrato As String, _
                                           ByVal pstrNroLote As String, _
                                           ByVal pstrCodigoConcepto As String, _
                                           ByVal pstrEstadoCobro As String, _
                                           ByVal pstrFlagIndividual As String, _
                                           ByVal pstrFlagRegistro As String) As JQGridJsonResponse
        Dim objCobroNTx As New LCobroNTx
        Dim strECreditoRecuperacionComision As String = String.Empty
        Dim objCreditoRecuperacionComision As New ECreditoRecuperacionComision

        With objCreditoRecuperacionComision
            .CodOperacionActiva = pstrNroContrato
            .RazonSocial = pstrRazonSocial
            .NumeroLote = pstrNroLote
            .CodComisionTipo = pstrCodigoConcepto
            .CodigoUnico = pstrCUCliente
            .EstadoCobro = pstrEstadoCobro
            .FlagIndividual = pstrFlagIndividual
            .FlagRegistro = pstrFlagRegistro
        End With

        strECreditoRecuperacionComision = GCCUtilitario.SerializeObject(Of ECreditoRecuperacionComision)(objCreditoRecuperacionComision)

        Dim dtbCobro As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCobroNTx.ListadoCobro(pPageSize, _
                                                                                                            pCurrentPage, _
                                                                                                            pSortColumn, _
                                                                                                            pSortOrder, _
                                                                                                            strECreditoRecuperacionComision))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtbCobro.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtbCobro.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtbCobro.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtbCobro)

    End Function

#End Region

#Region "   Métodos     "
    ''' <summary>
    ''' Inicializa Combos
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 27/11/2012
    ''' </remarks>
    Protected Sub InicializaCombos()
        'GCCUtilitario.CargarComboValorGenerico(cmbTipoDocumento, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
        pCargarConceptos()
        pCargarEstado()
    End Sub

    ''' <summary>
    ''' Carga combo de conceptos
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 27/11/2012
    ''' </remarks>
    Protected Sub pCargarConceptos()
        Dim objLCobro As New LCobroNTx
        Dim odtbConcepto As DataTable
        odtbConcepto = GCCUtilitario.DeserializeObject(Of DataTable)(objLCobro.ListadoConcepto(GCCConstante.C_TABLAGENERICA_Concepto))
        GCCUtilitario.pCargarHtmlSelect(cmbConcepto, odtbConcepto, "DESCRIPCION", "CODIGO", "[-Seleccione-]", "0")
    End Sub

    ''' <summary>
    ''' Carga estado cobro
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    Public Sub pCargarEstado()
        Dim oLwsMantenimiento As New LMantenimientoNTX
        Dim odtbParam As DataTable

        Try
            odtbParam = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsMantenimiento.ListarTablaGenerica(GCCConstante.C_TABLAGENERICA_Estado_Cobro_LPC))
            Dim dwPara As New DataView(odtbParam)
            dwPara.RowFilter = String.Format(" VALOR3 = '1'")
            GCCUtilitario.pCargarHtmlSelect(cmbEstadoCobro, dwPara.ToTable(), "VALOR2", "CODIGO", "[-Seleccione-]", "0")
        Catch ex As Exception
            Throw ex
        Finally
            odtbParam = Nothing
            oLwsMantenimiento = Nothing
        End Try
    End Sub
#End Region

End Class
