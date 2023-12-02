Imports System.Runtime.InteropServices

#Region "Interface No Transaccional"


''' <summary>
''' Interfaz de métodos para la clase IValorGenericaNTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("536B14BC-61E9-4800-AEED-5A4ED5F0FE02")> _
Public Interface IValorGenericaNTx

    Function ListadoValorGenerica(ByVal pstrTablaGenerica As String) As String
    Function ListadoValorGenericaAnidada(ByVal pstrTablaGenerica As String, ByVal pstrCodigo As String) As String

    '10/01/2013 IBK - RPR
    Function ListadoValorGenericaEspecial(ByVal pEValorGenerica As String, ByVal type As Integer) As String
    'FIN RPR

End Interface

#End Region