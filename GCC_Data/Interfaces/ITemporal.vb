Imports System.Runtime.InteropServices

#Region "Interface Transaccional"
''' <summary>
''' Interfaz de métodos para la clase ITemporalTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("1B329043-1F94-426e-9923-D0B395C7E81C")> _
Public Interface ITemporalTx

    Function InsertarTemporal(ByVal pETemporal As String) As Integer
    Function ModificarTemporal(ByVal pETemporal As String) As Boolean
    Function EliminarTemporal(ByVal pETemporal As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase TemporalNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("680D082A-2D00-4a24-B383-66ABAB954F50")> _
Public Interface ITemporalNTx

    Function fobjLeerTemporal(ByVal pETemporal As String) As String

    Function ListadoTemporal(ByVal pPageSize As Integer, _
                             ByVal pCurrentPage As Integer, _
                             ByVal pSortColumn As String, _
                             ByVal pSortOrder As String, _
                             ByVal pCodigo As String, _
                             ByVal pFecha As String, _
                             ByVal pNumero As String, _
                             ByVal pDecimales As String, _
                             ByVal pComentario As String, _
                             ByVal pTexto As String, _
                             ByVal pFlag As String) As String

    'IBK - RPH
    Function ListarSeguros(ByVal pPageSize As Integer, _
                           ByVal pCurrentPage As Integer, _
                           ByVal pSortColumn As String, _
                           ByVal pSortOrder As String, _
                           ByVal pESeguros As String) As String

    Function ListarSegurosDetalle(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pCodigoContrato As String) As String
    'Fin
End Interface

#End Region

