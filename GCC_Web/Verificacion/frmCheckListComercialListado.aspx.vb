﻿Imports System.Web.Services
Imports System.Data
Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Verificacion_frmCheckListComercialListado
    Inherits System.Web.UI.Page

    Dim objLog As New GCCLog("frmCheckListComercialListado.aspx.vb")

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
                GCCUtilitario.CargarComboValorGenerico(cmbEjecutivo, GCCConstante.C_TABLAGENERICA_EJECUTIVO)
                GCCUtilitario.CargarComboValorGenerico(cmbClasificacionbien, GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)
                GCCUtilitario.CargarComboValorGenerico(cmbZonalCombo, GCCConstante.C_TABLAGENERICA_BANCA_ATENCION)
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

    <WebMethod()> _
    Public Shared Function ListadoContratoCotizacion(ByVal pPageSize As Integer, _
                                                     ByVal pCurrentPage As Integer, _
                                                     ByVal pSortColumn As String, _
                                                     ByVal pSortOrder As String, _
                                                     ByVal pNroCotizacion As String, _
                                                     ByVal pNroContrato As String, _
                                                     ByVal pCUCliente As String, _
                                                     ByVal pRazonSocialCli As String, _
                                                     ByVal pCodEjecutivo As String, _
                                                     ByVal pClasifBien As String, _
                                                     ByVal pZonal As String, _
                                                     ByVal pFechaInicio As String, _
                                                     ByVal pFechaFin As String) As JQGridJsonResponse


        Dim oLwsDocClienteNTx As New LDocClienteNTx
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
                .CodUnico = pCUCliente
                .NombreCliente = pRazonSocialCli
                .Codigoejecutivoleasing = GCCUtilitario.NullableStringCombo(pCodEjecutivo)
                .Codigoclasificacionbien = GCCUtilitario.NullableStringCombo(pClasifBien)
                .CodigoZonal = GCCUtilitario.NullableStringCombo(pZonal)
                .FechaInicio = GCCUtilitario.StringToDateTime(pFechaInicio)
                .FechaFin = GCCUtilitario.StringToDateTime(pFechaFin)
                .CodigoEstadoContrato = GCCConstante.C_CODIGO_ESTADO_CONTRATO_INGRESADO

            End With
            odtbListado = GCCUtilitario.DeserializeObject(Of DataTable)( _
                               oLwsDocClienteNTx.ListadoContratoCotizacion(pPageSize, _
                                                                           pCurrentPage, _
                                                                           pSortColumn, _
                                                                           pSortOrder, _
                                                                           GCCUtilitario.SerializeObject(Of EGcc_cotizacion)(oECotizacion) _
                                                                           ))

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If odtbListado.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(odtbListado.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(odtbListado.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim JQGridJsonResponse As New JQGridJsonResponse
            Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
            Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, odtbListado)
        Catch ex As Exception
            Throw ex
        Finally
            odtbListado.Dispose()
            oLwsDocClienteNTx = Nothing
        End Try
    End Function
End Class
