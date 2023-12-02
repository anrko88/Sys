Imports System.Runtime.InteropServices

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase IValorGenericaNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
''' 
<Guid("524A8E46-0EDE-4239-9022-2A1813B387ED")> _
Public Interface IValorGenericaNTx

    Function ListadoValorGenerica(ByVal pstrTablaGenerica As String) As String
    Function ListadoValorGenericaAnidada(ByVal pstrTablaGenerica As String, ByVal pstrCodigo As String) As String
    Function ListarNotarias(ByVal pCodDepartamento As String, ByVal pCodProvincia As String) As String
    Function ObtenerContactoNotarias(ByVal pCodNotaria As String) As String

    '10/01/2013 IBK - RPR
    Function ListadoValorGenericaEspecial(ByVal pEValorGenerica As String, ByVal type As Integer) As String
    'FIN

End Interface

#End Region