Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ISubPrestatarioTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 21/05/2012
''' </remarks>
<Guid("807002DE-3C4F-4395-AFB6-644CC19C77DB")> _
Public Interface ISubPrestatarioTx
    Function InsertarSubPrestatario(ByVal pESubPrestatario As String) As String
    Function ModificarSubPrestatario(ByVal pESubPrestatario As String) As Boolean
End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase SubPrestatarioNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 21/05/2012
''' </remarks>
<Guid("BA9791F7-0F00-47ae-BFC5-4CE98F898380")> _
Public Interface ISubPrestatarioNTx
    Function ObtenerSubPrestatario(ByVal pstrCodCotizacion As String, ByVal pstrCodContrato As String) As String
End Interface

#End Region