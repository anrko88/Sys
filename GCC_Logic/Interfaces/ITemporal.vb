Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ITemporalTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("6C43BC5C-9FC9-45a0-BA93-C4E2B143576D")> _
Public Interface ITemporalTx

    ''' <summary>
    ''' Ingresa nueva Temporal
    ''' </summary>
    ''' <param name="pETemporal">Listado de Objeto Temporal(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function InsertarTemporal(ByVal pETemporal As String) As Integer

    ''' <summary>
    ''' Actualiza Temporal
    ''' </summary>
    ''' <param name="pETemporal">Listado de Objeto Temporal(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function ModificarTemporal(ByVal pETemporal As String) As Boolean

    ''' <summary>
    ''' Elimina Temporal
    ''' </summary>
    ''' <param name="pETemporal">Listado de Objeto Temporal(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function EliminarTemporal(ByVal pETemporal As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ITemporalNTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("89AD34BE-9244-4822-BF88-5B298F34AA98")> _
Public Interface ITemporalNTx

    ''' <summary>
    ''' Permite obtener un registro de Temporal
    ''' </summary>
    ''' <param name="pETemporal">Listado de Objeto Temporal(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function LeerTemporal(ByVal pETemporal As String) As String

    ''' <summary>
    ''' Permite obtener el listado de Temporal
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
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

