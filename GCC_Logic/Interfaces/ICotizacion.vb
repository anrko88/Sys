Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ICotizacionTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("5D517E6E-809F-4931-9736-DA5FA8C740D8")> _
Public Interface ICotizacionTx

    ''' <summary>
    ''' Ingresa nueva Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    Function InsertarCotizacion(ByVal pECotizacion As String, ByVal pECronograma As String) As String
    'IBK - RPH
    Function RegistrarRutaCronograma(ByVal pECotizacion As String) As Boolean
    'Fin IBK

    ''' <summary>
    ''' Modifica Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    Function ModificarCotizacion(ByVal pECotizacion As String, ByVal pEListCronograma As String) As Boolean

    ''' <summary>
    ''' Modifica el estado de la Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Function ModificarCotizacionEstado(ByVal pECotizacion As String) As Boolean

    ''' <summary>
    ''' Modifica el estado de carta de la Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Function ModificarCotizacionCarta(ByVal pECotizacion As String) As Boolean

    ''' <summary>
    ''' Modifica el estado de la Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Function CotizacionAprobar(ByVal pECotizacion As String) As Boolean

    ''' <summary>
    ''' Rechazar la Cotizacion
    ''' </summary>
    ''' <param name="pECotizacionSeguimiento">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Function CotizacionRechazar(ByVal pECotizacionSeguimiento As String) As Boolean

    ''' <summary>
    ''' ModificarEstadoCotizacionWS
    ''' </summary>
    ''' <param name="pstrNumeroCotizacion"></param>
    ''' <param name="pstrCodigoEstado"></param>
    ''' <returns></returns>
    ''' 
    ''' <remarks></remarks>
    Function ModificarEstadoCotizacionWS(ByVal pstrNumeroCotizacion As String, ByVal pstrCodUnico As String, ByVal pstrCodigoEstado As String) As Boolean
    Function ModificarCotizacionWS(ByVal pECotizacion As String) As Boolean




#Region "SubPrestatario"

    ''' <summary>
    ''' Insert SubPrestatario
    ''' </summary>
    ''' <param name="pESubPrestatario">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Function InsertarSubPrestatario(ByVal pESubPrestatario As String) As Boolean

    ''' <summary>
    ''' Modifica SubPrestatario
    ''' </summary>
    ''' <param name="pESubPrestatario">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Function ModificarSubPrestatario(ByVal pESubPrestatario As String) As Boolean

#End Region

#Region "DocumentoComentario"

    ''' <summary>
    ''' Ingresar CotizacionDocumento
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Function InsertarCotizacionDocumento(ByVal pECotizacionDocumento As String) As Boolean

    ''' <summary>
    ''' Modificar CotizacionDocumento
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Function ModificarCotizacionDocumento(ByVal pECotizacionDocumento As String) As Boolean

    ''' <summary>
    ''' Eliminar CotizacionDocumento
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Function EliminarCotizacionDocumento(ByVal pECotizacionDocumento As String) As Boolean

#End Region

#Region "Cronograma"

    ''' <summary>
    ''' Ingresar CotizacionCronograma
    ''' </summary>
    ''' <param name="pECotizacionCronograma">Entidad serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Function InsertarCotizacionCronograma(ByVal pECotizacionCronograma As String) As Boolean

    ''' <summary>
    ''' Modificar CotizacionCronograma
    ''' </summary>
    ''' <param name="pECotizacionCronograma">Entidad serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Function ModificarCotizacionCronograma(ByVal pECotizacionCronograma As String) As Boolean

    ''' <summary>
    ''' Eliminar CotizacionCronograma
    ''' </summary>
    ''' <param name="pECotizacionCronograma">Entidad serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Function EliminarCotizacionCronograma(ByVal pECotizacionCronograma As String) As Boolean

#End Region

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ICotizacionNTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("F19B36A6-1DD4-43ab-94E9-3704727AE03D")> _
Public Interface ICotizacionNTx

    ''' <summary>
    ''' Listado de Cotizacion
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function ListadoCotizacion(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pECotizacion As String) As String

    ''' <summary>
    ''' Obtiene una cotizacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function GetCotizacion(ByVal pECotizacion As String) As String

    Function ConsultaCotizacion(ByVal pstrNumeroCotizacion As String, _
                                    ByVal pstrCodigoUnico As String) As String

    'inicio IBK
#Region "Busqueda de Clientes"
    Function ListadoCliente(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pRazonSocial As String) As String
#End Region
    'fin IBK
#Region "SubPrestatario"

    ''' <summary>
    ''' Obtiene el SubPrestatario de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <param name="pstrCodSuprestatario">Codigo Suprestatario</param>
    ''' <param name="pstrCodUnico">Codigo Unico</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 21/05/2012 
    ''' </remarks>
    Function ObtenerSubPrestatario(ByVal pstrCodSuprestatario As String, ByVal pstrCodUnico As String) As String

#End Region

#Region "DocumentoComentario"

    ''' <summary>
    ''' Listado de Cotizacion
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function ListadoCotizacionDocumento(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pECotizacionDocumento As String) As String

    ''' <summary>
    ''' Get de CotizacionDocumento
    ''' </summary>
    ''' <returns>Devuelve un registro de Cotizacion</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Function ObtenerCotizacionDocumento(ByVal pECotizacionDocumento As String) As String

#End Region

#Region "Cotizacion Version"
    Function ListadoCotizacionVersion(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pECotizacion As String) As String
    Function ObtenerCotizacionVersion(ByVal pECotizacion As String) As String
#End Region

#Region "Seguimiento Cotizacion"
    Function ListadoSeguimientoCotizacion(ByVal pPageSize As Integer, _
                                                 ByVal pCurrentPage As Integer, _
                                                 ByVal pSortColumn As String, _
                                                 ByVal pSortOrder As String, _
                                                 ByVal pECotizacion As String) As String
#End Region

#Region "Cotizacion Cronograma"
    Function ObtenerCotizacionCronograma(ByVal pECotizacion As String) As String
    Function ObtenerCronogramaActual(ByVal pstrNumeroCotizacion As String) As String
    Function CotizacionCronogramaGet(ByVal pECotizacion As String) As String
#End Region

    ' Inicio AAE 
#Region "Auxiliares"
    Function GetCodUsuarioEjecutivo(ByVal pstrNroCotizacion As String, ByVal nbrEsCotizacion As Integer) As String
    Function GetCodUsuarioAdministradoresLeasing() As String
    Function GetCodUsuarioAdministradoresComercial() As String
#End Region
    ' FIN AAE

#Region "Web Services"
    Function ConsultarCotizacionWS(ByVal pECodigoCotizacion As String, ByVal pECodigoUnico As String) As Boolean

#End Region

End Interface

#End Region
