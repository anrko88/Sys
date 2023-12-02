Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IEjecutivoTx
''' </summary>
''' <remarks>TSF-SCA | 19/03/2013</remarks>
<Guid("45B2694A-6446-4186-8510-B6475DD06156")> _
Public Interface IEjecutivoTx

    Function InsertarEjecutivo(ByVal pEEjecutivo As String) As Boolean
    Function ModificarEjecutivo(ByVal pEEjecutivo As String) As Boolean
    Function EliminarEjecutivo(ByVal pEEjecutivo As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IEjecutivoNTx
''' </summary>
''' <remarks>TSF-SCA | 19/03/2013</remarks>
<Guid("48380BFD-C48D-43c5-9A09-8DE2DDD5D02B")> _
Public Interface IEjecutivoNTx

    Function ObtenerEjecutivo(ByVal pstrCodTabla As String, ByVal pstrCodigo As String) As String
    Function ListadoEjecutivo(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pEEjecutivo As String) As String

End Interface

#End Region