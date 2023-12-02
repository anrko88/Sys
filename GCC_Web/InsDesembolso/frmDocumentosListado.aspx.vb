Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Partial Class InsDesembolso_frmDocumentosListado
    Inherits GCCBase

    Dim objLog As New GCCLog("frmInsDesembolsoListado.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")
        Try

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then
                Dim strCodigoContrato As String = Request.QueryString("cc")
                Dim strCodInsDesembolso As String = Request.QueryString("cid")
                hddCodContrato.Value = strCodigoContrato
                hddCodInsDesembolso.Value = strCodInsDesembolso
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

#Region "WebEventos"

    ''' <summary>
    ''' ListaDocumentos
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pCodContrato"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function ListaDocumentos(ByVal pPageSize As Integer, _
                                                      ByVal pCurrentPage As Integer, _
                                                      ByVal pSortColumn As String, _
                                                      ByVal pSortOrder As String, _
                                                      ByVal pCodContrato As String) As JQGridJsonResponse


        Dim oLwsDesembolsoNTx As New LDesembolsoNTx
        Dim oEContratoEstructDoc As New ESolicitudcreditoestructuradoc
        Dim odtbLista As New DataTable
        Try
            With oEContratoEstructDoc
                .Codsolicitudcredito = GCCUtilitario.NullableString(pCodContrato)
            End With
            odtbLista = GCCUtilitario.DeserializeObject(Of DataTable)( _
                               oLwsDesembolsoNTx.ListarContratoEstructDoc(pPageSize, _
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

    ''' <summary>
    ''' Actualizar la Instrucción de Desembolso
    ''' </summary>
    ''' <param name="pstrCodContrato">Numero de Contrato</param>
    ''' <param name="pstrChekeados">Todos los Chekeados</param>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 19/06/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ActualizarID(ByVal pstrCodContrato As String, _
                                        ByVal pstrCodInsDesembolso As String, _
                                        ByVal pstrChekeados As String) As String

        Dim oEGCC_InsDesembolso As New EGCC_InsDesembolso
        Dim oLInstruccionDesembolsoTx As New LInstruccionDesembolsoTx
        Dim strResp As String = ""
        Try

            With oEGCC_InsDesembolso
                .Codsolicitudcredito = GCCUtilitario.NullableString(pstrCodContrato)
                .Codinstrucciondesembolso = GCCUtilitario.NullableString(pstrCodInsDesembolso)
                .Documentos = pstrChekeados + "''"
                .UsuarioRegistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With
            Dim strEGCC_InsDesembolso As String = GCCUtilitario.SerializeObject(oEGCC_InsDesembolso)
            Dim strNumeroID As String = oLInstruccionDesembolsoTx.GestionInsDesembolso(strEGCC_InsDesembolso)

            strResp = String.Concat("0|", strNumeroID)

        Catch ex As Exception
            strResp = String.Concat("1|", ex.Message)
        End Try

        Return strResp
    End Function

#End Region

End Class
