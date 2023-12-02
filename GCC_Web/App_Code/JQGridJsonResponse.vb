Imports System
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Data


''' <summary>
''' Respuesta JSON para JQGrid.
''' </summary>
Public Class JQGridJsonResponse

#Region "Fields"

    Private _pageCount As Integer
    Private _currentPage As Integer
    Private _recordCount As Integer
    Private _items As List(Of Hashtable)
    Private _flagError As Integer
    Private _msgError As String

#End Region

#Region "Properties"

    ''' <summary>
    ''' Cantidad de páginas del JQGrid.
    ''' </summary>
    Public Property PageCount() As Integer
        Get
            Return _pageCount
        End Get
        Set(ByVal value As Integer)
            _pageCount = value
        End Set
    End Property

    ''' <summary>
    ''' Página actual del JQGrid.
    ''' </summary>
    Public Property CurrentPage() As Integer
        Get
            Return _currentPage
        End Get
        Set(ByVal value As Integer)
            _currentPage = value
        End Set
    End Property

    ''' <summary>
    ''' Cantidad total de elementos de la lista.
    ''' </summary>
    Public Property RecordCount() As Integer
        Get
            Return _recordCount
        End Get
        Set(ByVal value As Integer)
            _recordCount = value
        End Set
    End Property

    ''' <summary>
    ''' Lista de elementos del JQGrid.
    ''' </summary>
    Public Property Items() As List(Of Hashtable)
        Get
            Return _items
        End Get
        Set(ByVal value As List(Of Hashtable))
            _items = value
        End Set
    End Property

    ''' <summary>
    ''' Si encuentra ERROR
    ''' </summary>
    Public Property FlagError() As Integer
        Get
            Return _flagError
        End Get
        Set(ByVal value As Integer)
            _flagError = value
        End Set
    End Property

    ''' <summary>
    ''' MENSAJE ERROR
    ''' </summary>
    Public Property MsgError() As String
        Get
            Return _msgError
        End Get
        Set(ByVal value As String)
            _msgError = value
        End Set
    End Property

#End Region

    Public Sub New()
        _items = New List(Of Hashtable)()
    End Sub

    Public Function JQGridJsonResponseClass(Of T)(ByVal pPageCount As Integer, _
                                                  ByVal pCurrentPage As Integer, _
                                                  ByVal pRecordCount As Integer, _
                                                  ByVal oList As IList(Of T)) As JQGridJsonResponse
        Dim oJqGridJsonResponse As New JQGridJsonResponse()

        Try
            Dim elementType As Type = GetType(T)

            oJqGridJsonResponse._pageCount = pPageCount
            oJqGridJsonResponse._currentPage = pCurrentPage
            oJqGridJsonResponse._recordCount = pRecordCount

            For Each item As T In oList
                Dim ohashTable As New Hashtable

                For Each propInfo As PropertyInfo In elementType.GetProperties()
                    If propInfo.GetValue(item, Nothing) = Nothing Then
                        ohashTable.Add(propInfo.Name, "")
                    Else
                        ohashTable.Add(propInfo.Name, propInfo.GetValue(item, Nothing).ToString())
                    End If
                Next
                oJqGridJsonResponse._items.Add(ohashTable)
            Next
        Catch ex As Exception
            'Throw
        End Try

        Return oJqGridJsonResponse
    End Function

    Public Function JQGridJsonResponseDataTable(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal oDataTable As DataTable) As JQGridJsonResponse
        Dim oJqGridJsonResponse As New JQGridJsonResponse()
        Dim totalRegistros As Integer = oDataTable.Rows.Count

        If totalRegistros > 0 Then
            If (totalRegistros Mod pPageSize > 0) Then
                PageCount = totalRegistros \ pPageSize + 1
            Else
                PageCount = totalRegistros \ pPageSize
            End If
        Else
            PageCount = 1
        End If

        oJqGridJsonResponse._pageCount = PageCount
        oJqGridJsonResponse._currentPage = pCurrentPage
        oJqGridJsonResponse._recordCount = oDataTable.Rows.Count

        For Each row As DataRow In oDataTable.Rows

            Dim oHashTable As New Hashtable

            For Each col As DataColumn In oDataTable.Columns
                oHashTable.Add(col.ColumnName, row.Item(col.ColumnName).ToString)
            Next

            oJqGridJsonResponse._items.Add(oHashTable)

        Next

        Return oJqGridJsonResponse
    End Function

    Public Function JQGridJsonResponseDataTable(ByVal pPageCount As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pRecordCount As Integer, _
                                                ByVal oDataTable As DataTable) As JQGridJsonResponse
        Dim oJqGridJsonResponse As New JQGridJsonResponse()

        oJqGridJsonResponse._pageCount = pPageCount
        oJqGridJsonResponse._currentPage = pCurrentPage
        oJqGridJsonResponse._recordCount = pRecordCount

        For Each row As DataRow In oDataTable.Rows

            Dim oHashTable As New Hashtable

            For Each col As DataColumn In oDataTable.Columns
                oHashTable.Add(col.ColumnName, row.Item(col.ColumnName).ToString)
            Next

            oJqGridJsonResponse._items.Add(oHashTable)

        Next

        Return oJqGridJsonResponse
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pPageCount"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pRecordCount"></param>
    ''' <param name="oDataTable"></param>
    ''' <param name="Fields"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function JQGridJsonResponseDataTable(ByVal pPageCount As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pRecordCount As Integer, _
                                                ByVal oDataTable As DataTable, _
                                                ByVal Fields As String) As JQGridJsonResponse
        Dim oJqGridJsonResponse As New JQGridJsonResponse()
        Dim vFields As String()
        Dim oDataView As DataView = oDataTable.DefaultView

        vFields = Fields.Split(New Char() {"|"c})
        Dim oNewDataTable As DataTable = oDataView.ToTable("oDataTable", True, vFields)

        oJqGridJsonResponse._pageCount = pPageCount
        oJqGridJsonResponse._currentPage = pCurrentPage
        oJqGridJsonResponse._recordCount = pRecordCount

        For Each row As DataRow In oNewDataTable.Rows

            Dim oHashTable As New Hashtable

            For Each vField As String In vFields
                'For Each col As DataColumn In oNewDataTable.Columns
                '    oHashTable.Add(col.ColumnName, row.Item(col.ColumnName).ToString)
                oHashTable.Add(vField, row.Item(vField).ToString)
            Next vField

            oJqGridJsonResponse._items.Add(oHashTable)

        Next row

        Return oJqGridJsonResponse
    End Function

    ''' <summary>
    ''' Devuelve el número de páginas a utilizar para el total de registros utilizados
    ''' y el tamaño indicado de la página.
    ''' </summary>
    ''' <param name="totalRegistros">Total de registros</param>
    ''' <param name="pPageSize">Tamaño de cada página</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Public Function TotalPaginas(ByVal totalRegistros As Integer, _
                                 ByVal pPageSize As Integer) As Integer
        If totalRegistros > 0 Then
            If (totalRegistros Mod pPageSize > 0) Then
                Return totalRegistros \ pPageSize + 1
            Else
                Return totalRegistros \ pPageSize
            End If
        Else
            Return 1
        End If
    End Function

    Public Function JQGridJsonResponseError(ByVal pstrError As String) As JQGridJsonResponse
        Dim oJqGridJsonResponse As New JQGridJsonResponse()

        oJqGridJsonResponse._pageCount = 0
        oJqGridJsonResponse._currentPage = 0
        oJqGridJsonResponse._recordCount = 0

        oJqGridJsonResponse._flagError = 1
        oJqGridJsonResponse._msgError = pstrError

        Return oJqGridJsonResponse
    End Function

End Class
