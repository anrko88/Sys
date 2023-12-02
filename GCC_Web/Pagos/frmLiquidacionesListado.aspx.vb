Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Pagos_frmLiquidacionesListado
    Inherits GCCBase

    Dim objLog As New GCCLog("frmPagoCuotasListado.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 18/12/2012
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página frmPagoCuotasListado", "Page_Load")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoLiquidacion, GCCConstante.C_TABLAGENERICA_TIPO_LIQUIDACION)
                GCCUtilitario.CargarComboValorGenerico(Me.cmbEstado, GCCConstante.C_TABLAGENERICA_ESTADO_LIQUIDACION)
                GCCUtilitario.CargarComboMoneda(Me.cmbMoneda)
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

#Region "WebMethods"

    <WebMethod()> _
    Public Shared Function ListaLiquidaciones(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pstrNroContrato As String, _
                                             ByVal pstrCodigoLiquidacion As String, _
                                             ByVal pstrCuCliente As String, _
                                             ByVal pstrRazonSocial As String, _
                                             ByVal pstrFechaValorInicio As String, _
                                             ByVal pstrFechaValorFin As String, _
                                             ByVal pstrTipoLiquidacion As String, _
                                             ByVal pstrEstado As String, _
                                             ByVal pstrMoneda As String, _
                                             ByVal pstrFlagAdenda As String) As JQGridJsonResponse


        'Variables
        Dim objLPagosNTx As New LPagosNTx

        Try

            'Valida Campos
            If pstrFechaValorInicio <> "" Then
                pstrFechaValorInicio = CDate(pstrFechaValorInicio).ToString("yyyyMMdd")
            End If
            If pstrFechaValorFin <> "" Then
                pstrFechaValorFin = CDate(pstrFechaValorFin).ToString("yyyyMMdd")
            End If

            'Inicializa Objeto
            Dim objEGCC_PagoCuotas As New EGCC_Liquidacion
            Dim strEGCC_PagoCuotas As String
            With objEGCC_PagoCuotas
                .CodOperacionActiva = GCCUtilitario.NullableString(pstrNroContrato)
                .CodigoLiquidacion = GCCUtilitario.NullableString(pstrCodigoLiquidacion)
                .CUCliente = GCCUtilitario.NullableString(pstrCuCliente)
                .RazonSocial = GCCUtilitario.NullableString(pstrRazonSocial)
                .FechaValorInicio = GCCUtilitario.NullableString(pstrFechaValorInicio)
                .FechaValorFin = GCCUtilitario.NullableString(pstrFechaValorFin)
                .TipoLiquidacion = GCCUtilitario.NullableStringCombo(pstrTipoLiquidacion)
                .EstadoLiquidacion = GCCUtilitario.NullableStringCombo(pstrEstado)
                .CodMoneda = GCCUtilitario.NullableStringCombo(pstrMoneda)
                .FlagAdenda = GCCUtilitario.NullableStringCombo(pstrFlagAdenda)
            End With
            strEGCC_PagoCuotas = GCCUtilitario.SerializeObject(objEGCC_PagoCuotas)

            'Ejecuta Consulta
            Dim dtLiquidaciones As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLPagosNTx.ListadoLiquidaciones(pPageSize, _
                                                                                                                       pCurrentPage, _
                                                                                                                       pSortColumn, _
                                                                                                                       pSortOrder, _
                                                                                                                       strEGCC_PagoCuotas))

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtLiquidaciones.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtLiquidaciones.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtLiquidaciones.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtLiquidaciones)

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

#End Region

End Class
