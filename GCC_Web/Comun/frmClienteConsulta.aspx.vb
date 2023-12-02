Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class Comun_frmClienteConsulta
    Inherits GCCBase

    Dim objLog As New GCCLog("frmClienteConsulta.aspx.vb")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
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

#Region "Métodos"

    <WebMethod()> _
    Public Shared Function BuscarCliente(ByVal pPageSize As Integer, _
                                       ByVal pCurrentPage As Integer, _
                                       ByVal pSortColumn As String, _
                                       ByVal pSortOrder As String, _
                                       ByVal pRazonSocial As String) As JQGridJsonResponse

        Dim objClienteNTx As New LCotizacionNTx

        Dim dtCliente As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objClienteNTx.ListadoCliente(pPageSize, _
                                                                                                                pCurrentPage, _
                                                                                                                pSortColumn, _
                                                                                                                pSortOrder, _
                                                                                                                pRazonSocial))

        ' Número de página actual.
        Dim currentPage As Integer = pCurrentPage
        Dim intTotalCurrent As Int32
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtCliente.Rows.Count = 0 Then
            totalRecords = 0
            intTotalCurrent = 1
        Else
            totalRecords = Convert.ToInt32(dtCliente.Rows(0)("RecordCount"))
            intTotalCurrent = Convert.ToInt32(dtCliente.Rows(0)("TOTAL_PAGINA"))
        End If

        If currentPage > intTotalCurrent Then
            currentPage = intTotalCurrent
        End If

        ' Número total de páginas
        Dim objJQGridJsonResponse As New JQGridJsonResponse
        Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
        Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtCliente)

    End Function
#End Region

End Class
