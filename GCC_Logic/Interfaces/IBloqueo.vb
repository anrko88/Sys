Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IBloqueoTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("AB94E5AB-5CE0-47b5-BDF9-11BF55383817")> _
Public Interface IBloqueoTx
    Function InsertarBloqueo(ByVal pEBloqueo As String) As Boolean
    Function ModificarBloqueo(ByVal pEBloqueo As String) As Boolean
End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IBloqueoNTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("84A9EEDB-DF18-484f-854A-E78A445437CE")> _
Public Interface IBloqueoNTx
    Function ObtenerBloqueo(ByVal pstrCodBloqueo As String) As String
End Interface

#End Region

