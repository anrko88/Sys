Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IGestionBienDocTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("E752777D-5A7C-41b8-A374-E8C5C5520FE6")> _
Public Interface IGestionBienDocTx

    Function InsertarGestionBienDoc(ByVal pEGestionBienDoc As String) As Boolean
    Function ModificarGestionBienDoc(ByVal pEGestionBienDoc As String) As Boolean
    Function EliminarGestionBienDoc(ByVal pEGestionBienDoc As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IGestionBienDocNTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("9FD8DF52-381C-4ac6-AA64-06E7391B2AAE")> _
Public Interface IGestionBienDocNTx

    Function ObtenerGestionBienDoc(ByVal pEGestionBienDoc As String) As String
    Function ListadoGestionBienDoc(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEGestionBienDoc As String _
                                                ) As String

End Interface

#End Region

