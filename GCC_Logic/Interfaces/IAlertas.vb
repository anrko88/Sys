Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IBloqueoTx
''' </summary>
''' <remarks>TSF-SCA | 29/01/2013</remarks>
<Guid("A8435201-B8C0-4606-B4FF-DF5A50348F85")> _
Public Interface IAlertasTx
    Function fInsertarAlertas(ByVal pEBloqueo As String) As Boolean
End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IBloqueoNTx
''' </summary>
''' <remarks>TSF-SCA | 29/01/2013</remarks>
<Guid("82616990-DAA5-4f41-B09D-B1BD558D3105")> _
Public Interface IAlertasNTx
    'Function ObtenerBloqueo(ByVal pstrCodBloqueo As String) As String
End Interface

#End Region