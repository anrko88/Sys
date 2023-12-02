Imports System.Data
Imports System.Web.Services

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Reportes_frmBandejaListado
    Inherits System.Web.UI.Page

    ReadOnly _objLog As New GCCLog("frmBandejaListado.aspx.vb")

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
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
                GCCUtilitario.CargarComboValorGenerico(cmbEjecutivo, GCCConstante.C_TABLAGENERICA_EJECUTIVO)
                GCCUtilitario.CargarComboValorGenerico(cmbClasificacionbien, GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)
                GCCUtilitario.CargarComboValorGenerico(cmbZonalCombo, GCCConstante.C_TABLAGENERICA_BANCA_ATENCION)
            End If
        Catch ex As ApplicationException
            _objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
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
            _objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowError("ERROR => " + ex.Message, Me)
        End Try
    End Sub

    <WebMethod()> _
    Public Shared Function ListadoContratoCotizacionRep(ByVal pPageSize As Integer, _
                                                        ByVal pCurrentPage As Integer, _
                                                        ByVal pSortColumn As String, _
                                                        ByVal pSortOrder As String, _
                                                        ByVal pNroCotizacion As String, _
                                                        ByVal pNroContrato As String, _
                                                        ByVal pCuCliente As String, _
                                                        ByVal pRazonSocialCli As String, _
                                                        ByVal pCodEjecutivo As String, _
                                                        ByVal pClasifBien As String, _
                                                        ByVal pZonal As String, _
                                                        ByVal pFechaInicio As String, _
                                                        ByVal pFechaFin As String) As JQGridJsonResponse
        Dim oLContratoNTx As New LContratoNTx
        Dim oECotizacion As New EGcc_cotizacion
        Dim odtbListado As New DataTable

        If pFechaInicio <> "" Then
            pFechaInicio = CDate(pFechaInicio).ToString("yyyyMMdd")
        End If

        If pFechaFin <> "" Then
            pFechaFin = CDate(pFechaFin).ToString("yyyyMMdd")
        End If

        Try
            With oECotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pNroCotizacion)
                .CodigoContrato = GCCUtilitario.NullableString(pNroContrato)
                .CodUnico = pCuCliente
                .NombreCliente = pRazonSocialCli
                .Codigoejecutivoleasing = GCCUtilitario.NullableStringCombo(pCodEjecutivo)
                .Codigoclasificacionbien = GCCUtilitario.NullableStringCombo(pClasifBien)
                .CodigoZonal = GCCUtilitario.NullableStringCombo(pZonal)
                .FechaInicio = GCCUtilitario.StringToDateTime(pFechaInicio)
                .FechaFin = GCCUtilitario.StringToDateTime(pFechaFin)
            End With
            odtbListado = GCCUtilitario.DeserializeObject(Of DataTable)( _
                                   oLContratoNTx.ListadoContratoCotizacionRep(pPageSize, _
                                                                              pCurrentPage, _
                                                                              pSortColumn, _
                                                                              pSortOrder, _
                                                                              GCCUtilitario.SerializeObject(Of EGcc_cotizacion)(oECotizacion)))

            Dim oJQGridJsonResponse As New JQGridJsonResponse

            'Total de registros a mostrar.
            Dim totalRecords As Integer
            Dim totalCurrent As Integer

            If odtbListado.Rows.Count = 0 Then
                totalRecords = 0
                totalCurrent = 0
            Else
                totalRecords = Convert.ToInt32(odtbListado.Rows(0)("RecordCount"))
                totalCurrent = Convert.ToInt32(odtbListado.Rows(0)("TOTAL_PAGINA"))
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
                                                                   odtbListado)
        Catch ex As Exception
            Throw ex
        Finally
            odtbListado.Dispose()
        End Try
    End Function

End Class
