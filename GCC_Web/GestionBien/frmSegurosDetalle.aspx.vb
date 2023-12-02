Imports System.Data
Imports System.Web.Services

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Partial Class GestionBien_frmSegurosDetalle
    Inherits GCCBase
    ReadOnly objLog As New GCCLog("frmSegurosDetalle.aspx.vb")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                If Not IsNothing(Request.QueryString("CodigoContrato")) Then
                    'hdnCodigoContrato.Value = Request.QueryString("CodigoContrato")
                    lblNroContrato.Text = Request.QueryString("CodigoContrato")
                End If
                If Not IsNothing(Request.QueryString("NroPoliza")) Then
                    lblNroPoliza.Text = Request.QueryString("NroPoliza")
                End If
                If Not IsNothing(Request.QueryString("TipoSeguro")) Then
                    lblTipoSeguro.Text = Request.QueryString("TipoSeguro")
                End If
                If Not IsNothing(Request.QueryString("CiaSeguro")) Then
                    lblCiaSeguro.Text = Request.QueryString("CiaSeguro")
                End If
                If Not IsNothing(Request.QueryString("Cliente")) Then
                    lblCliente.Text = Request.QueryString("Cliente")
                End If
                If Not IsNothing(Request.QueryString("NroPrenda")) Then
                    hdnCodigoContrato.Value = Request.QueryString("NroPrenda")
                    lblNroPrenda.Text = Request.QueryString("NroPrenda")
                End If
                If Not IsNothing(Request.QueryString("fini")) Then
                    lblFini.Text = Request.QueryString("fini")
                End If
                If Not IsNothing(Request.QueryString("ffin")) Then
                    lblFfin.Text = Request.QueryString("ffin")
                End If

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

#Region "WebMetodos"

    <WebMethod()> _
    Public Shared Function ListarDetalleSeguros(ByVal pPageSize As String, _
                                                ByVal pCurrentPage As String, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pCodigoContrato As String) As JQGridJsonResponse
        Dim objTemporalNTx As New LTemporalNTx

        Try
            Dim dtSeguros As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objTemporalNTx.ListarSegurosDetalle(pPageSize, _
                                                                                                                           pCurrentPage, _
                                                                                                                           pSortColumn, _
                                                                                                                           pSortOrder, _
                                                                                                                           pCodigoContrato))

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtSeguros.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtSeguros.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtSeguros.Rows(0)("TOTAL_PAGINA"))
            End If

            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtSeguros)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Shared Function TotalPaginas(ByVal total As Integer, ByVal pPageSize As Integer) As Integer
        If (total Mod pPageSize > 0) Then
            Return total \ pPageSize + 1
        Else
            Return total \ pPageSize
        End If
    End Function

#End Region
End Class
