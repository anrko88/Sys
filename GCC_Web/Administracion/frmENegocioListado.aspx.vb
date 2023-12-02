Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Data

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Administracion_frmENegocioListado
    Inherits GCCBase

    Dim objLog As New GCCLog("frmENegocioListado.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 19/03/2013
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
    Public Shared Function BuscarEjecutivo(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pTipo As String, _
                                           ByVal pCodigo As String, _
                                           ByVal pNombre As String) As JQGridJsonResponse


        Try

            Dim oLEjecutivoNTx As New LEjecutivoNTx
            Dim oEjecutivo As New EGCC_Ejecutivo
            Dim strEGcc_ejecutivo As String
            With oEjecutivo
                .ID_Tabla = "TBL410"
                .Tipo = GCCUtilitario.NullableString(pTipo)
                .CodigoEjecutivo = GCCUtilitario.NullableString(pCodigo)
                .NombreEjecutivo = GCCUtilitario.NullableString(pNombre)
            End With
            strEGcc_ejecutivo = GCCUtilitario.SerializeObject(oEjecutivo)

            Dim dtEjecutivo As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(oLEjecutivoNTx.ListadoEjecutivo(pPageSize, _
                                                                                                                        pCurrentPage, _
                                                                                                                        pSortColumn, _
                                                                                                                        pSortOrder, _
                                                                                                                        strEGcc_ejecutivo))

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            Dim intTotalCurrent As Int32
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtEjecutivo.Rows.Count = 0 Then
                totalRecords = 0
                intTotalCurrent = 1
            Else
                totalRecords = Convert.ToInt32(dtEjecutivo.Rows(0)("RecordCount"))
                intTotalCurrent = Convert.ToInt32(dtEjecutivo.Rows(0)("TOTAL_PAGINA"))
            End If
            If currentPage > intTotalCurrent Then
                currentPage = intTotalCurrent
            End If

            ' Número total de páginas
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Dim totalPages As Integer = objJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtEjecutivo)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function


    ''' <summary>
    ''' Eliminar Ejecutivo
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 20/03/2013
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function EliminarEjecutivo(ByVal pstrCodEjecutivo As String) As String

        Try
            'Instancia Clases
            Dim objLEjecutivoTx As New LEjecutivoTx
            Dim strEjecutivo As String

            'Graba Documentos
            Dim objEGCC_Ejecutivo As New EGCC_Ejecutivo
            With objEGCC_Ejecutivo
                .ID_Tabla = "TBL410"
                .Codigo = pstrCodEjecutivo
            End With

            strEjecutivo = GCCUtilitario.SerializeObject(Of EGCC_Ejecutivo)(objEGCC_Ejecutivo)
            objLEjecutivoTx.EliminarEjecutivo(strEjecutivo)

            Return ""

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

End Class
