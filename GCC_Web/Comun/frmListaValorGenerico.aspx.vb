Imports GCC.UI
Imports GCC.LogicWS
Imports System.Data
Imports System.Web.Services

Partial Class Comun_frmListaValorGenerico
    Inherits GCCBase

    Dim objLog As New GCCLog("frmListaValorGenerico.aspx.vb")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            hidDominio.value = Request.QueryString("ncd")
            If Not IsPostBack Then
                lblTitulo.Text = Request.QueryString("nt")
                hidProvino.Value = Request.QueryString("np")
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
    ''' 
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pstrCodigoDominio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListarParametros(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pstrCodigoDominio As String) As JQGridJsonResponse

        Dim oLwsMantenimiento As New LMantenimientoNTX
        Dim odtbParam As New DataTable
        Dim dv As New DataView

        Try
            odtbParam = GCCUtilitario.DeserializeObject(Of DataTable)(oLwsMantenimiento.ListarTablaGenerica(pstrCodigoDominio))
            'If pstrCodigoDominio.ToString.Trim().Equals(GCCConstante.C_TABLAGENERICA_CONCEPTO_DETRACCION) Then
            'End If

            For Each oRow As DataRow In odtbParam.Rows
                oRow.Item("DESCRIPCION") = oRow.Item("VALOR4") + " - " + oRow.Item("DESCRIPCION")
            Next
            odtbParam.AcceptChanges()

            Dim dwParam As DataView = odtbParam.DefaultView
            dwParam.Sort = "VALOR4 ASC"
            odtbParam = New DataTable
            odtbParam = dwParam.ToTable


            Dim currentPage As Integer = pCurrentPage

            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If odtbParam.Rows.Count = 0 Then
                totalRecords = 0
            Else
                totalRecords = Convert.ToInt32(odtbParam.Rows.Count)
            End If
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim JQGridJsonResponse As New JQGridJsonResponse

            Return JQGridJsonResponse.JQGridJsonResponseDataTable(1, currentPage, totalRecords, odtbParam)

        Catch ex As Exception
            Throw ex
        Finally
            odtbParam = Nothing
            oLwsMantenimiento = Nothing
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
