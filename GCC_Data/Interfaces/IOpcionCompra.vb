
Imports System.Runtime.InteropServices

#Region "Interface Transaccional"
''' <summary>
''' Interfaz de métodos para la clase IOpcionCompraTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 07/01/2013 06:40:33 p.m.
''' </remarks>
<Guid("BFA2C36A-0F5F-4EC3-890B-394EDD6DDBA5")> _
Public Interface IOpcionCompraTx

    ''' <summary>
    ''' Inserta un nuevo registro en la tabla GCC_OpcionCompra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m. 
    ''' </remarks>	
    Function fblnInsertarOpcionCompra(ByVal pEGCC_OpcionCompra As String) As Boolean

    ''' <summary>
    ''' Inserta un nuevo registro en la tabla GCC_OpcionCompraEnvio
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 10/01/2013 06:41:54 p.m. 
    ''' </remarks>	
    Function fblnInsertarOpcionCompraEnvio(ByVal pEGCC_OpcionCompra As String) As Boolean

    ''' <summary>
    ''' Actualiza los registros de la tabla SolicitudCreditoEstructuraCarax
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 16/01/2013 06:41:54 p.m. 
    ''' </remarks>	
    Function fblnAprobacionBien(ByVal pEGCC_OpcionCompra As String) As Boolean

    ''' <summary>
    ''' Modifica un registro existente de la tabla GCC_OpcionCompra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 09/01/2013 06:41:54 p.m.
    ''' </remarks>
    Function fblnModificarOpcionCompra(ByVal pEGCC_OpcionCompra As String) As Boolean

    ''' <summary>
    ''' Modifica un registro existente de la tabla SolicitudCreditoEstructuraCarac
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m.
    ''' </remarks>
    Function fblnModificarBienOpcionCompra(ByVal pEGCC_OpcionCompra As String) As Boolean

    ''' <summary>
    ''' Eliminar un registro existente de la tabla GCC_OpcionCompra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m.
    ''' </remarks>
    Function fblnEliminarOpcionCompra(ByVal pEGCC_OpcionCompra As String) As Boolean

    ''' <summary>
    ''' Inserta un registro en la tabla GCC_OpcionCompraDocumento
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se registro correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 29/01/2013 06:41:54 p.m.
    ''' </remarks>
    Function fblnInsertarOpcionCompraDocumento(ByVal pEGCC_OpcionCompra As String) As Boolean

    ''' <summary>
    ''' Inserta un registro en la tabla GCC_OpcionCompraDocumentoObservacion
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se registro correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 29/01/2013 06:41:54 p.m.
    ''' </remarks>
    Function fblnInsertarOpcionCompraDocumentoObservacion(ByVal pEGCC_OpcionCompra As String) As Boolean

End Interface
#End Region

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase IOpcionCompraNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 07/01/2013 06:40:33 p.m.
''' </remarks>
<Guid("AFB505AC-53AB-4B48-977D-D215D64C4579")> _
Public Interface IOpcionCompraNTx

    ''' <summary>
    ''' Obtiene los valores de un registro de la tabla GCC_OpcionCompra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Function fobjLeerOpcionCompra(ByVal pEGCC_OpcionCompra As String) As String

    ''' <summary>
    ''' Obtiene los valores de un registro de la vista UV_GCC_BIENOPCIOMCOMPRA_SEL
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 08/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Function fobjLeerBienOpcionCompra(ByVal pEGCC_OpcionCompra As String) As String

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de página</param>
    ''' <param name="pCurrentPage">Página actual</param>
    ''' <param name="pSortColumn">Ordenar por columna</param>
    ''' <param name="pSortOrder">Dirección de ordenamiento</param>    
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Function fobjListadoOpcionCompra(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pEGCC_OpcionCompra As String) As String

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 11/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Function fobjListadoOpcionCompraTodo(ByVal pEGCC_OpcionCompra As String) As String

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 11/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Function fobjListadoOpcionCompraEnvio(ByVal pEGCC_OpcionCompra As String) As String

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de página</param>
    ''' <param name="pCurrentPage">Página actual</param>
    ''' <param name="pSortColumn">Ordenar por columna</param>
    ''' <param name="pSortOrder">Dirección de ordenamiento</param>    
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 08/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Function fobjListadoBienOpcionCompra(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pEGCC_OpcionCompra As String) As String

    ''' <summary>
    ''' Lista todos los bienes por contrato
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <param name="pFechaTermino">Fecha de término de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 16/01/2013 03:04:54 p.m. 
    ''' </remarks>
    Function fobjListadoBienOpcionCompraReporte(ByVal pFechaInicio As String, _
                                                ByVal pFechaTermino As String) As String

    ''' <summary>
    ''' Listado de Opción de Compra Reporte
    ''' </summary>
    ''' <param name="pdFechaActivacionIni">Fecha de Activación Inicial</param>
    ''' <param name="pdFechaActivacionFin">Fecha de Activación Final</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 28/01/2013
    ''' </remarks>
    Function ListadoReporteOpcionCompra(ByVal pdFechaActivacionIni As DateTime, _
                                        ByVal pdFechaActivacionFin As DateTime) As String

    ''' <summary>
    ''' Listado de Documento de Opción de Compra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>  
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 29/01/2013
    ''' </remarks>
    Function ListadoOpcionCompraDocumento(ByVal pEGCC_OpcionCompra As String) As String

    ''' <summary>
    ''' Listado de Observaciones de Documento de Opción de Compra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>  
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 29/01/2013
    ''' </remarks>
    Function ListadoOpcionCompraDocumentoObservacion(ByVal pEGCC_OpcionCompra As String) As String
End Interface

#End Region

