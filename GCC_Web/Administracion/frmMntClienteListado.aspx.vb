Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Mantenimiento_frmMntClienteListado
    Inherits GCCBase

    Dim objLog As New GCCLog("frmProveedorListado.aspx.vb")

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
                ' GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoDocumento, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
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

#Region "Métodos"
    <WebMethod()> _
    Public Shared Function BuscarSuprestatarios(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pCodigo As String, _
                                           ByVal pCuCliente As String, _
                                           ByVal pNombre As String, _
                                           ByVal pDireccion As String, _
                                           ByVal pTipoDocumento As String, _
                                           ByVal pDocumento As String, _
                                           ByVal pNombreC As String, _
                                           ByVal pCorreoC As String, _
                                           ByVal pTelefonoC As String) As JQGridJsonResponse
        Dim objUtilNTx As New LUtilNTX

        Dim dtSuprestatario As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objUtilNTx.ListadoClienteSuprestatario(pPageSize, _
                                                                                                                    pCurrentPage, _
                                                                                                                    pSortColumn, _
                                                                                                                    pSortOrder, _
                                                                                                                    pCodigo, _
                                                                                                                    pCuCliente, _
                                                                                                                    pNombre, _
                                                                                                                    pDireccion, _
                                                                                                                    pTipoDocumento, _
                                                                                                                    pDocumento, pNombreC, pCorreoC, pTelefonoC))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtSuprestatario.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtSuprestatario.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtSuprestatario.Rows(0)("TOTAL_PAGINA"))
        End If
        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtSuprestatario)

    End Function
#End Region

End Class
