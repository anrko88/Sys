Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ITipoCambioTx
''' </summary>
''' <remarks>
''' Creado Por         :  JJM - IBK
''' Fecha de Creación  :  22/01/2013
''' </remarks>
<Guid("a5fb5c0d-f903-4fd5-97a7-0f5ad0e0e97f")> _
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
<Guid("3f9350c5-92d9-4fbe-b5e8-4ce006f9f190")> _
Public Interface ITipoCambioNTx

    Function ListaTipoCambio(ByVal pPageSize As Integer, _
               ByVal pCurrentPage As Integer, _
               ByVal pSortColumn As String, _
               ByVal pSortOrder As String, _
               ByVal pETipoCambio As String) As String
    Function ValidaTipoCambio(ByVal pETipoCambio As String) As String
End Interface
#End Region