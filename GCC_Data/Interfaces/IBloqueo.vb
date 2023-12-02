Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IBloqueoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("9AC3D758-4BA4-4156-8801-7CF29BF0E2BC")> _
Public Interface IBloqueoTx
    Function InsertarBloqueo(ByVal pEBloqueo As String) As Boolean
    Function ModificarBloqueo(ByVal pEBloqueo As String) As Boolean
End Interface

#End Region

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase BloqueoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("9C6A9DA8-2FC2-48eb-B7F9-CEF5B53DE22F")> _
Public Interface IBloqueoNTx
    Function ObtenerBloqueo(ByVal pEBloqueo As String) As String
End Interface

#End Region