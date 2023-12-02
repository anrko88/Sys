Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IBloqueoTx
''' </summary>
''' <remarks>TSF-SCA | 29/01/2013</remarks>
<Guid("BB86D0D6-E920-4f9b-BAC0-0808337AB3B5")> _
Public Interface IAlertasTx
    Function fInsertarAlertas(ByVal pEBloqueo As String) As Boolean
End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IBloqueoNTx
''' </summary>
''' <remarks>TSF-SCA | 29/01/2013</remarks>
<Guid("3C1DED00-6D4B-4ad8-998E-54A97BC3898F")> _
Public Interface IAlertasNTx
    'Function ObtenerBloqueo(ByVal pstrCodBloqueo As String) As String
End Interface

#End Region
