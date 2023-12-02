

Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS


Partial Class GestionBien_Tasacion_frmTasacionAsignacionListado
    Inherits GCCBase
    Dim objLog As New GCCLog("frmTasacionAsignacionListado.aspx.vb")
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
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoDocumento, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
                GCCUtilitario.CargarComboValorGenerico(Me.cbmEstadoTasacionContrato, GCCConstante.C_TABLAGENERICA_EstadoTasaciónContrato)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbClasificacionBien, GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbbanca, GCCConstante.C_TABLAGENERICA_BANCA_ATENCION)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbEstadocontrato, GCCConstante.C_TABLAGENERICA_ESTADO_CONTRATO)

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

#End Region

#Region "WebMethos"
    ''' <summary>
    ''' ListaCondicionesAdicionales
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pCodSolicitudcredito"></param>
    ''' <param name="pCuCliente"></param>
    ''' <param name="pRazonsolcial"></param>
    ''' <param name="pTipoDocumento"></param>
    ''' <param name="pNumerodocumento"></param>
    ''' <param name="pEstadoTasacion"></param>
    ''' <param name="pClasificacionBien"></param>
    ''' <param name="pBanca"></param>
    ''' <param name="pEjecutivoBanca"></param>
    ''' <param name="pPeriodo"></param>
    ''' <param name="pFechadesde"></param>
    ''' <param name="pFechaHasta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaCondicionesAdicionales(ByVal pPageSize As Integer, _
                                                       ByVal pCurrentPage As Integer, _
                                                       ByVal pSortColumn As String, _
                                                       ByVal pSortOrder As String, _
                                                       ByVal pCodSolicitudcredito As String, _
                                                       ByVal pCuCliente As String, _
                                                       ByVal pRazonsolcial As String, _
                                                       ByVal pTipoDocumento As String, _
                                                       ByVal pNumerodocumento As String, _
                                                       ByVal pEstadoTasacion As String, _
                                                       ByVal pClasificacionBien As String, _
                                                       ByVal pBanca As String, _
                                                       ByVal pEjecutivoBanca As String, _
                                                       ByVal pPeriodo As String, _
                                                       ByVal pFechadesde As String, _
                                                       ByVal pFechaHasta As String, _
                                                       ByVal pEstadoTasacionContrato As String) As JQGridJsonResponse
        Dim objTasadorNTx As New LTasadorNTx
        'Dim oEGccSolicitudcreditoestructuratasacion As New ESolicitudcreditoestructuratasacion
        'Dim strEGccSolicitudcreditoestructuratasacion As String
        'With oEGccSolicitudcreditoestructuratasacion
        '.Codsolicitudcredito = pCodSolicitudcredito
        '.
        '.Numerocontrato = GCCUtilitario.NullableString(pCodigo)
        '.Flagfiltro = GCCUtilitario.CheckInt(pFlagFiltro)
        '.Flagcartaenvio = GCCUtilitario.CheckInt(pFlagEnvioCarta)
        'End With
        'strEGccSolicitudcreditoestructuratasacion = GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructuratasacion)(oEGccSolicitudcreditoestructuratasacion)
        Try
            Dim dtCondicionAdicional As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objTasadorNTx.ContratoTasadorSel(pPageSize, _
                                                                                                                                   pCurrentPage, _
                                                                                                                                   pSortColumn, _
                                                                                                                                   pSortOrder, _
                                                                                                                                   pCodSolicitudcredito, _
                                                                                                                                   pCuCliente, _
                                                                                                                                   pRazonsolcial, _
                                                                                                                                   pTipoDocumento, _
                                                                                                                                   pNumerodocumento, _
                                                                                                                                   pEstadoTasacion, _
                                                                                                                                   pClasificacionBien, _
                                                                                                                                   pBanca, _
                                                                                                                                   pEjecutivoBanca, _
                                                                                                                                   pPeriodo, _
                                                                                                                                   pFechadesde, _
                                                                                                                                   pFechaHasta, _
                                                                                                                                   pEstadoTasacionContrato))
            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            'Dim total2 As Decimal
            If dtCondicionAdicional.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtCondicionAdicional.Rows(0)("RecordCount"))
                'total2 = Convert.ToInt32(dtCondicionAdicional.Rows(0)("total2"))
                intTotalCurrent = Convert.ToInt32(dtCondicionAdicional.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim JQGridJsonResponse As New JQGridJsonResponse
            Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
            Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtCondicionAdicional)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
