Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ITipoCambioTx
''' </summary>
''' <remarks>
''' Creado Por         :  JJM - IBK
''' Fecha de Creación  :  22/01/2013
''' </remarks>
<Guid("4df07616-4314-4c54-8e6a-c58a6d7c65a8")> _
Public Interface ITipoCambioTx
    Function InsertaTipoCambio(ByVal pETipoCambio As String) As Boolean
    Function ActualizaTipoCambio(ByVal pETipoCambio As String) As Boolean
    Function EliminaTipoCambio(ByVal pETipoCambio As String) As Boolean
End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ITipoCambioNTx
''' </summary>
''' <remarks>
''' Creado Por         : JJM - IBK
''' Fecha de Creación  : 22/01/2013
''' </remarks>
<Guid("61a0b552-186c-4af0-a86f-644d649e082d")> _
Public Interface ITipoCambioNTx

    Function ListaTipoCambio(ByVal pPageSize As Integer, _
               ByVal pCurrentPage As Integer, _
               ByVal pSortColumn As String, _
               ByVal pSortOrder As String, _
               ByVal pETipoCambio As String) As String
    Function ValidaTipoCambio(ByVal pETipoCambio As String) As String

End Interface
#End Region