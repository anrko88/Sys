Imports System.Runtime.InteropServices


#Region "Interface Transaccional"


''' <summary>
''' Interfaz de métodos para la clase ICartaTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("5110A33C-BA3F-41a2-84B9-B2F4BD85B715")> _
Public Interface ICartaTx

    Function EnviarCarta(ByVal pEGCC_Carta As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"


''' <summary>
''' Interfaz de métodos para la clase CartaNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("474804D5-D6EA-4d76-94F6-C2933BF4CCB9")> _
Public Interface ICartaNTx


End Interface

#End Region
