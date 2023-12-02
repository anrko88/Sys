Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.UI
Imports GCC.Entity
Imports GCC.LogicWS

Partial Class Temporal_frmTemporalListado
    Inherits GCCBase

    ReadOnly objLog As New GCCLog("frmTemporalListado.aspx.vb")

#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

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

#Region "Web Methods"

    <WebMethod()> _
    Public Shared Function ListaTemporal(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pCodigo As String, _
                                         ByVal pFecha As String, _
                                         ByVal pNumero As String, _
                                         ByVal pDecimales As String, _
                                         ByVal pComentario As String, _
                                         ByVal pTexto As String, _
                                         ByVal pFlag As String) As JQGridJsonResponse

        Dim objTemporalNTx As New LTemporalNTx
        Dim temporals As New List(Of ETemporal)()

        If pFecha <> "" Then
            pFecha = CDate(pFecha).ToString("yyyyMMdd")
        End If

        Dim dtProducto As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objTemporalNTx.ListadoTemporal(pPageSize, _
                                                                                                                   pCurrentPage, _
                                                                                                                   pSortColumn, _
                                                                                                                   pSortOrder, _
                                                                                                                   pCodigo, _
                                                                                                                   pFecha, _
                                                                                                                   pNumero, _
                                                                                                                   pDecimales, _
                                                                                                                   pComentario, _
                                                                                                                   pTexto, _
                                                                                                                   pFlag))

        For Each oRow As DataRow In dtProducto.Rows
            Dim objETemporal As New ETemporal()

            With objETemporal
                If Not oRow.Item("codigo") Is DBNull.Value Then
                    .Codigo = Integer.Parse(oRow.Item("codigo").ToString())
                End If
                .Texto = oRow.Item("texto").ToString
                .Comentario = oRow.Item("comentario").ToString
                If Not oRow.Item("decimales") Is DBNull.Value Then
                    .Decimales = Decimal.Parse(oRow.Item("decimales").ToString())
                End If
                If Not oRow.Item("fecha") Is DBNull.Value Then
                    .Fecha = Date.Parse(oRow.Item("fecha").ToString())
                End If
                .Flag = oRow.Item("flag").ToString
                If Not oRow.Item("numero") Is DBNull.Value Then
                    .Numero = Integer.Parse(oRow.Item("numero").ToString())
                End If

                temporals.Add(objETemporal)
            End With
        Next oRow

        Dim oJQGridJsonResponse As New JQGridJsonResponse
        ' Total de registros a mostrar.
        Dim totalRecords As Integer
        If dtProducto.Rows.Count = 0 Then
            totalRecords = 0
        Else
            totalRecords = Convert.ToInt32(dtProducto.Rows(0)("RecordCount"))
        End If
        ' Número total de páginas
        Dim totalPages As Integer = oJQGridJsonResponse.TotalPaginas(totalRecords, pPageSize)

        Return oJQGridJsonResponse.JQGridJsonResponseClass(totalPages, pcurrentPage, totalRecords, temporals)
        'Return JQGridJsonResponse.JQGridJsonResponseDataTable(pageCount, currentPage, recordCount, dtProducto)

    End Function


    <WebMethod()> _
    Public Shared Function TemporalEliminar(ByVal pCodigo As String) As String
        Dim oETemporal As New ETemporal
        Dim objTemporalTx As New LTemporalTx
        Dim pETemporal As String

        oETemporal.Codigo = Integer.Parse(pCodigo)
        pETemporal = GCCUtilitario.SerializeObject(oETemporal)
        Dim blnResult As Boolean = objTemporalTx.fblnEliminarTemporal(pETemporal)

        If blnResult Then
            Return "0"
        Else
            Return "1"
        End If
    End Function

#End Region

#Region "Métodos"


#End Region

End Class
