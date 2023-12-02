
Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IContratoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("1F56E29E-3687-4d59-B99B-65EFF341A22C")> _
Public Interface IContratoTx

#Region "SolicitudCredito"

    ''' <summary>
    ''' Establece si algún elemento del anexo a sido modificado o no.
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function fblnModificado(ByVal pESolicitudCredito As String) As Boolean

    Function fblnModificarContrato(ByVal pESolicitudCredito As String) As Boolean

    ''' <summary>
    ''' Actualiza el estado del contrato
    ''' </summary>
    ''' <param name="pSolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function ActualizaEstado(ByVal pSolicitudCredito As String) As Boolean

    ''' <summary>
    ''' Actualiza el nombre del documento de separación del conyugue en la tabla Contrato (SolicitudCredito)
    ''' </summary>
    ''' <param name="pSolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function ActualizaDocumentoSeparacion(ByVal pSolicitudCredito As String) As Boolean

    ''' <summary>
    ''' Actualiza el nombre del documento de contrato en la tabla Contrato (SolicitudCredito)
    ''' </summary>
    ''' <param name="pSolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function ActualizaArchivoContratoAdjunto(ByVal pSolicitudCredito As String) As Boolean

#End Region

    ''' <summary>
    ''' Modifiar el Contrato(SolicitudCredito) para el Checklist
    ''' </summary>
    ''' <param name="pEGcc_checklisComercialBien">Entidad Contrato serializada</param>
    ''' <returns>String con el número de Contrato</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function ContratoChecklistUpd(ByVal pEGcc_checklisComercialBien As String) As Boolean

    ''' <summary>
    ''' Modifiar el Contrato(SolicitudCredito) para el Documentos Clientes
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad serializada</param>
    ''' <returns>Resultado Booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    ''' 

    Function ContratoDocClienteUpd(ByVal pESolicitudCredito As String) As Boolean

    ''' <summary>
    ''' GestionComercialEnviarUpd
    ''' </summary>
    ''' <param name="pEGcc_Contrato"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GestionComercialEnviarUpd(ByVal pEGcc_Contrato As String) As Boolean
    Function InsertaSeguimientoContrato(ByVal pEGcc_SeguimientoContrato As String) As Boolean
    'Inicio IBK - AAE - 12/02/2013 - Agrego nueva operación
    Function ContratoChecklistUpd2(ByVal pEGcc_checklisComercialBien As String, ByVal pEgcc_cotizacion As String) As Boolean
    'Fin IBK

End Interface

#End Region

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase ContratoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("2988BFC1-AC83-4bd8-918B-0B1689C5DCCF")> _
Public Interface IContratoNTx

    Function Retornar(ByVal codigoContrato As String) As String

    Function ListadoContratos(ByVal pPageSize As Integer, _
                              ByVal pCurrentPage As Integer, _
                              ByVal pSortColumn As String, _
                              ByVal pSortOrder As String, _
                              ByVal pContrato As String, _
                              ByVal pCuCliente As String, _
                              ByVal pRazonSocial As String, _
                              ByVal pCotizacion As String, _
                              ByVal pFechaIni As String, _
                              ByVal pFechaFin As String, _
                              ByVal pEjecutivo As String, _
                              ByVal pEstado As String, _
                              ByVal pZonal As String, _
                              ByVal pClasificacion As String, _
                              ByVal pClasificacionContrato As String, _
                              ByVal pCodigoBanca As String, _
                              ByVal pTipoPersona As String, _
                              ByVal pNotaria As String, _
                              ByVal pKardex As String) As String

    Function ListadoContratos2(ByVal pPageSize As Integer, _
                              ByVal pCurrentPage As Integer, _
                              ByVal pSortColumn As String, _
                              ByVal pSortOrder As String, _
                              ByVal pContrato As String, _
                              ByVal pCuCliente As String, _
                              ByVal pRazonSocial As String, _
                              ByVal pCotizacion As String, _
                              ByVal pFechaIni As String, _
                              ByVal pFechaFin As String, _
                              ByVal pEjecutivo As String, _
                              ByVal pEstado As String, _
                              ByVal pZonal As String, _
                              ByVal pClasificacion As String, _
                              ByVal pClasificacionContrato As String, _
                              ByVal pCodigoBanca As String, _
                              ByVal pTipoPersona As String, _
                              ByVal pNotaria As String, _
                              ByVal pKardex As String, _
                              ByVal pEstadoOperacionActiva As String) As String

    Function ListadoContratosDesembolso(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pContrato As String, _
                                         ByVal pCuCliente As String, _
                                         ByVal pRazonSocial As String, _
                                         ByVal pEjecutivo As String, _
                                         ByVal pEstado As String, _
                                         ByVal pClasificacion As String, _
                                         ByVal pCodigoSubTipoContrato As String, _
                                         ByVal pCodigoBanca As String, _
                                         ByVal pCodMoneda As String) As String

    Function DatosCliente(ByVal codigoContrato As String) As String

    Function ListadoContratoCotizacion(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal pECotizacion As String) As String

    Function ContratoCuentas(ByVal codigoContrato As String) As String
    'Inicio IBK RPR
    Function ObtenerContrato(ByVal codigoContrato As String) As String
    Function ObtenerCuentasContrato(ByVal codigoContrato As String) As String
    'Fin IBK
    Function ObtenerContratoCotizacion(ByVal pstrNroContrato As String) As String
    Function RetornarObservacionContratoDocumento(ByVal PEGccDocumentoObservacion As String) As String
    'INICIO_JJM
    Function RetornarObservacionContratoDocumentoInafectacion(ByVal PEGccDocumentoObservacion As String) As String
    'FIN_JJM

    Function ListadoSeguimientoGlobal(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal pCodContrato As String, _
                                              ByVal pCodCotizacion As String) As String

    Function ListadoSeguimientoContrato(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal pSeguimientoContrato As String) As String

    ''' <summary>
    ''' Listado General de Contrato y Cotizacion para la generación de reportes.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la pagina</param>
    ''' <param name="pCurrentPage">Pagina Actual</param>
    ''' <param name="pSortColumn">Columna a Ordenar</param>
    ''' <param name="pSortOrder">Tipo de Ordenamiento</param>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - EBL
    ''' Fecha de Creacion : 16/05/2012
    ''' </remarks>
    Function ListadoContratoCotizacionRep(ByVal pPageSize As Integer, _
                                          ByVal pCurrentPage As Integer, _
                                          ByVal pSortColumn As String, _
                                          ByVal pSortOrder As String, _
                                          ByVal pECotizacion As String) As String

    ' Inicio IBK - AAE - 03/10/2012 Se agrega método para listar contratocotización en sol Docs
    Function ListadoContratoCotizacionSolDoc(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal pECotizacion As String) As String
    ' fin IBK

    Function ListadoSituacionCreditoContrato(ByVal pContrato As String, _
                                     ByVal pCuCliente As String, _
                                     ByVal pRazonSocial As String, _
                                     ByVal pCotizacion As String, _
                                     ByVal pFechaIni As String, _
                                     ByVal pFechaFin As String, _
                                     ByVal pEjecutivo As String, _
                                     ByVal pEstado As String, _
                                     ByVal pZonal As String, _
                                     ByVal pClasificacion As String, _
                                     ByVal pClasificacionContrato As String, _
                                     ByVal pCodigoBanca As String, _
                                     ByVal pTipoPersona As String, _
                                     ByVal pNotaria As String, _
                                     ByVal pKardex As String) As String

    Function RetornarDatosContratoSituacionCredito(ByVal codigoContrato As String) As String
    Function RetornarDatosCronogramaSituacionCredito(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal codigoContrato As String) As String
    'Inicio IBK JJM
    Function RetornarDatosCronogramaSituacionCreditoExcel(ByVal codigoContrato As String, ByVal fechavalor As String) As String
    Function RetornarDatosCronogramaPostSituacionCreditoExcel(ByVal codigoContrato As String, ByVal Usuario As String) As String
    Function RetornarDatosCronogramaSituacionCreditoExcelHeader(ByVal codigoContrato As String) As String
    Function RetornarDatosCronogramaSituacionCreditoExcelResumen(ByVal codigoContrato As String, ByVal Usuario As String) As String
    Function RetornarDatosCronogramaSituacionCreditoExcelDetalle(ByVal codigoContrato As String, ByVal Usuario As String) As String
    'Fin IBK 
    Function RetornarDatosGastosSituacionCredito(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal codigoContrato As String) As String
    ''' <summary>
    ''' Listado de Sunat - Contrato.
    ''' </summary>
    ''' <param name="pdFechaCelebracionIni">Fecha Celebración de Contrato Inicial</param>
    ''' <param name="pdFechaCelebracionFin">Fecha Celebración de Contrato Final</param>
    ''' <param name="pdFechaActivacionIni">Fecha de Activación Inicial</param>
    ''' <param name="pdFechaActivacionFin">Fecha de Activación Final</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 22/01/2013
    ''' </remarks>
    Function ListadoReporteSunatContratos(ByVal pdFechaCelebracionIni As DateTime, _
                                          ByVal pdFechaCelebracionFin As DateTime, _
                                          ByVal pdFechaActivacionIni As DateTime, _
                                          ByVal pdFechaActivacionFin As DateTime) As String

    ''' <summary>
    ''' Listado de Detalle del Bien Reporte
    ''' </summary>
    ''' <param name="pdFechaActivacionIni">Fecha de Activación Inicial</param>
    ''' <param name="pdFechaActivacionFin">Fecha de Activación Final</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 24/01/2013
    ''' </remarks>
    Function ListadoReporteDetalleBien(ByVal pdFechaActivacionIni As DateTime, _
                                       ByVal pdFechaActivacionFin As DateTime) As String

    ''' <summary>
    ''' Lista todos los Saldos de Crédito
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 29/01/2013 09:15:54 a.m. 
    ''' </remarks>
    Function fobjListadoSaldosCreditoReporte(ByVal pFechaInicio As String) As String


    ''' <summary>
    ''' Lista todos los Saldos de Crédito
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 05/03/2013   
    ''' </remarks>
    Function fobjListadoSaldosCreditoReporteDolares(ByVal pFechaInicio As String) As String

    ''' <summary>
    ''' Lista todos los Créditos Activos
    ''' </summary>
    ''' <param name="pTipo">Tipo de Periodo</param>
    ''' <param name="pFecha">Fecha de Periodo</param>
    ''' <param name="pCodigoClasificacionBien">Codigo Clasificación del Bien</param>
    ''' <param name="pCodigoTipoBien">Codigo del Tipo de Bien</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 29/01/2013 04:30:54 p.m. 
    ''' </remarks>
    Function fobjListadoContratosActivadosReporte(ByVal pTipo As String, _
                                             ByVal pFecha As String, _
                                             ByVal pCodigoClasificacionBien As String, _
                                             ByVal pCodigoTipoBien As String) As String

#Region "Anexos"

    ''' <summary>
    ''' Devuelve el contrato especificado identificado por el número del crédito para la generación de los anexos.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function RetornarAnexoContrato(ByVal codigoContrato As String) As String

    ''' <summary>
    ''' Retorna los datos de la tarifas a aplicar en el contrato
    ''' </summary>
    ''' <param name="codigoContrato"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function RetornarTarifario(ByVal codigoContrato As String) As String

#End Region

End Interface

#End Region
