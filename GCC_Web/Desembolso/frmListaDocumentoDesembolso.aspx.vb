Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS
Imports System.Data
Imports System.Web.Services

Partial Class Desembolso_frmListaDocumentoDesembolso
    Inherits GCCBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objLog As New GCCLog("frmListaDocumentoDesembolso.aspx.vb")

        Try
            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not IsPostBack Then
                hidCodigoContrato.Value = Request.QueryString("cc")
                hidTipoDocumento.Value = Request.QueryString("td")
                hidNroDocumento.Value = Request.QueryString("nd")
                hidFechaEmision.Value = Request.QueryString("fe")
                hidCodProveedor.Value = Request.QueryString("cp")
                hidCodunico.Value = Request.QueryString("cu")




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
    Public Shared Function ListarDocumento(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pstrCodigoContrato As String, _
                                            ByVal pstrTipoDocumento As String, _
                                            ByVal pstrNroDocumento As String, _
                                            ByVal pstrFechaEmision As String, _
                                            ByVal pstrCodProveedor As String, _
                                            ByVal pstrCodunico As String) As JQGridJsonResponse

        Dim oLwsDesembolsoNTx As New LDesembolsoNTx
        Dim oEContratoEstructDoc As New ESolicitudcreditoestructuradoc
        Dim odtbLista As New DataTable
        Try
            With oEContratoEstructDoc
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrCodigoContrato)
                .Tipodocumento = GCCUtilitario.NullableString(pstrTipoDocumento)
                .Nrodocumento = GCCUtilitario.NullableString(pstrNroDocumento)
                If pstrFechaEmision = String.Empty Then pstrFechaEmision = "19000101"
                .Fechaemision = GCCUtilitario.StringToDateTime(pstrFechaEmision)
                .CodProveedor = GCCUtilitario.NullableString(pstrCodProveedor)
                .CodUnico = GCCUtilitario.NullableString(pstrCodunico)
            End With
            odtbLista = GCCUtilitario.DeserializeObject(Of DataTable)( _
                               oLwsDesembolsoNTx.ListarContratoEstructDocAsociar(pPageSize, _
                                                                           pCurrentPage, _
                                                                           pSortColumn, _
                                                                           pSortOrder, _
                                                                           GCCUtilitario.SerializeObject(Of ESolicitudcreditoestructuradoc)(oEContratoEstructDoc) _
                                                                           ))

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage

            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If odtbLista.Rows.Count = 0 Then
                totalRecords = 0
            Else
                totalRecords = Convert.ToInt32(odtbLista.Rows(0)("RecordCount"))
            End If

            ' Número total de páginas
            Dim JQGridJsonResponse As New JQGridJsonResponse
            Dim totalPages As Integer = JQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
            Return JQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, pCurrentPage, totalRecords, odtbLista)

        Catch ex As Exception
            Throw ex
        Finally
            odtbLista.Dispose()
            oLwsDesembolsoNTx = Nothing
        End Try
    End Function


End Class
