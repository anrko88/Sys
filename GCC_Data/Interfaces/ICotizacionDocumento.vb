Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ICotizacionDocumentoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("1E276CB4-DB82-4f0a-AE8D-78E57FF89CE4")> _
Public Interface ICotizacionDocumentoTx

    ''' <summary>
    ''' Inserta el cotizacioNDocumento para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad Serializado de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012
    ''' </remarks>
    Function InsertarCotizacionDocumento(ByVal pECotizacionDocumento As String) As Boolean

    ''' <summary>
    ''' Modificar el cotizacioNDocumento de una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad Serializada de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012
    ''' </remarks>
    Function ModificarCotizacionDocumento(ByVal pECotizacionDocumento As String) As Boolean

    ''' <summary>
    ''' Eliminar el cotizacioNDocumento de una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad Serializada de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012
    ''' </remarks>
    Function EliminarCotizacionDocumento(ByVal pECotizacionDocumento As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase CotizacionDocumentoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("DCD2F949-65A2-4379-9F27-AC1A2BA0D246")> _
Public Interface ICotizacionDocumentoNTx

    ''' <summary>
    ''' Obtiene el CotizacionDocumento de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 21/05/2012 
    ''' </remarks>
    Function ObtenerCotizacionDocumento(ByVal pECotizacionDocumento As String) As String

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Function ListadoCotizacionDocumento(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pECotizacionDocumento As String _
                                                ) As String

End Interface

#End Region

