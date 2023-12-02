
Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

<Guid("DDEBC340-6796-4c04-8B83-A74900288500")> _
Public Interface IHostTx

    Function TransaccionGINA(ByVal strTramaIn As String, ByRef strTramaOut As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IPagosNTx
''' </summary>
''' <remarks>IBK - RPR | 25/12/2012</remarks>
<Guid("2719FBB3-6C7D-4d6a-97BA-84F894519C0E")> _
Public Interface IHostNTx



End Interface

#End Region

