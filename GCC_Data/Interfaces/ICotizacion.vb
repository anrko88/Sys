Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ICotizacionTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("A783B751-460A-49d4-97DA-445E61B2F732")> _
Public Interface ICotizacionTx

    ''' <summary>
    ''' Inserta un nuevo registro en la tabla Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Cotizacion serializada</param>
    ''' <returns>String con el número de Cotizacion</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function InsertarCotizacion(ByVal pECotizacion As String) As String

    ''' <summary>
    ''' Modifica un registro existente de la tabla Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function ModificarCotizacion(ByVal pECotizacion As String) As Boolean

    'IBK - RPH
    Function RegistrarRutaCronograma(ByVal pECotizacion As String) As Boolean
    'Fin

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
    ''' Gestion Versionamiento de la  Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Function CotizacionVersionamiento(ByVal pECotizacion As String) As Boolean
    ''' <summary>
    ''' Rechazar la Cotizacion
    ''' </summary>
    ''' <param name="pECotizacionSeguimiento">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Function CotizacionRechazar(ByVal pECotizacionSeguimiento As String) As Boolean

    Function ModificarEstadoCotizacionWS(ByVal pstrNumeroCotizacion As String, ByVal pstrCodUnico As String, ByVal pstrCodigoEstado As String) As Boolean
    Function ModificarCotizacionWS(ByVal pECotizacion As String) As Boolean



End Interface

#End Region

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase CotizacionNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("55A95418-16F5-41c9-AF74-3234BDAEF4BD")> _
Public Interface ICotizacionNTx

    ''' <summary>
    ''' Lista Cotizacion
    ''' </summary>
    ''' <returns></returns>
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

    Function ConsultarCotizacionWS(ByVal pstrNumeroCotizacion As String, _
                                   ByVal pstrCodigoUnico As String) As String

    ' Inicio AAE 
    Function GetCodUsuarioEjecutivo(ByVal pstrNroCotizacion As String, ByVal nbrEsCotizacion As Integer) As String
    Function GetCodUsuarioAdministradoresLeasing() As String
    Function GetCodUsuarioAdministradoresComercial() As String
    ' FIN AAE

    'IBK - RPH
    Function ListadoCliente(ByVal pPageSize As Integer, _
                            ByVal pCurrentPage As Integer, _
                            ByVal pSortColumn As String, _
                            ByVal pSortOrder As String, _
                            ByVal pRazonSocial As String) As String
    'Fin
End Interface

#End Region

