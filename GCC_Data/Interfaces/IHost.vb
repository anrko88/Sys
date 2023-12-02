
Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

<Guid("FA1BE174-BE51-470a-A1CF-C314908DCCF7")> _
Public Interface IHostTx

    Function TransaccionGINA(ByVal strTramaIn As String, ByRef strTramaOut As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IPagosNTx
''' </summary>
''' <remarks>IBK - RPR | 25/12/2012</remarks>
<Guid("1C81C0E2-3698-4f63-8BD3-4A037513F129")> _
Public Interface IHostNTx



End Interface

#End Region

