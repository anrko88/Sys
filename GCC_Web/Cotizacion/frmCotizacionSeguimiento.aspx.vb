﻿Imports System.Web.Services
Imports System.Data
Imports GCC.UI
Imports GCC.LogicWS
Imports GCC.Entity

Partial Class Cotizacion_frmCotizacionSeguimiento
    Inherits System.Web.UI.Page

    Dim objLog As New GCCLog("frmCotizacionSeguimiento.aspx.vb")
    Shared mstrCodCot As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not IsPostBack Then
                mstrCodCot = Request.QueryString("hddCodigoCotizacion")
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
    ''' Listado de Seguimiento Cotizacion
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 23/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListarSeguimientoCotizacion(ByVal pPageSize As Integer, _
                                                       ByVal pCurrentPage As Integer, _
                                                       ByVal pSortColumn As String, _
                                                       ByVal pSortOrder As String) As JQGridJsonResponse
        Dim objCotizacionNTx As New LCotizacionNTx
        Try
            'Inicializa Objeto
            Dim oECotizacion As New EGcc_cotizacion
            With oECotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(mstrCodCot)
            End With

            'Ejecuta Consulta
            Dim dtSeguimiento As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)( _
                                                        objCotizacionNTx.ListadoSeguimientoCotizacion(pPageSize, _
                                                                                               pCurrentPage, _
                                                                                               pSortColumn, _
                                                                                               pSortOrder, _
                                                                                               GCCUtilitario.SerializeObject(Of EGcc_cotizacion)(oECotizacion) _
                                                                                               ) _
                                            )

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtSeguimiento.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtSeguimiento.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtSeguimiento.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtSeguimiento)

        Catch ex As Exception
            Throw ex
        Finally
            objCotizacionNTx = Nothing
        End Try
    End Function

    Private Shared Function TotalPaginas(ByVal total As Integer, ByVal pPageSize As Integer) As Integer
        If (total Mod pPageSize > 0) Then
            Return total \ pPageSize + 1
        Else
            Return total \ pPageSize
        End If
    End Function
End Class
