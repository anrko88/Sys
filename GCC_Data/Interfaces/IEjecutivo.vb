Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IEjecutivoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 19/03/2013
''' </remarks>
<Guid("2114EB0A-1987-4bf4-A25E-0C4641C74F72")> _
Public Interface IEjecutivoTx
    Function InsertarEjecutivo(ByVal pEEjecutivo As String) As Boolean
    Function ModificarEjecutivo(ByVal pEEjecutivo As String) As Boolean
    Function EliminarEjecutivo(ByVal pEEjecutivo As String) As Boolean
End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase EjecutivoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 19/03/2013
''' </remarks>
<Guid("16277FCC-F6F0-4ecc-9992-77CBA92EA7FE")> _
Public Interface IEjecutivoNTx
    Function ObtenerEjecutivo(ByVal pstrCodTabla As String, ByVal pstrCodigo As String) As String
    Function ListadoEjecutivo(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pEEjecutivo As String) As String
End Interface

#End Region