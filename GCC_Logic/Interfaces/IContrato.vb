
Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IContratoTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("CB4CA2CE-9953-4736-8772-73CC12524D96")> _
Public Interface IContratoTx

#Region "ChecklistActualiza"

    Function CheckLisComercialUpd(ByVal pEGcc_checklisComercialBien As String, _
                                  ByVal pEGcc_checklisComercialCuentas As String) As Boolean
    'Inicio IBK - AAE - 12/02/2013 - Agrego nueva función
    Function CheckLisComercialUpd2(ByVal pEGcc_checklisComercialBien As String, _
                                  ByVal pEGcc_checklisComercialCuentas As String, _
                                  ByVal pEgcc_cotizacion As String) As Boolean
    'Fin IBK
#End Region

#Region "Contrato"

    Function ActualizaEstado(ByVal pSolicitudCredito As String) As Boolean

    Function fblnModificarContrato(ByVal pESolicitudCredito As String, _
                                   ByVal pEGcc_contratootroconcepto As String) As Boolean

    Function EnviarCarta(ByVal pSolicitudCredito As String, _
                         ByVal pEGCC_Carta As String) As Boolean

    Function ContratoGuardarYEnviar(ByVal pSolicitudCredito As String, _
                                    ByVal pEGcc_contratootroconcepto As String) As Boolean

    'Inicio IBK - AAE
    Function fblnModificarContrato2(ByVal pESolicitudCredito As String, _
                                   ByVal pEGcc_contratootroconcepto As String) As Boolean

    Function ContratoGuardarYEnviar2(ByVal pSolicitudCredito As String, _
                                    ByVal pEGcc_contratootroconcepto As String) As Boolean
    'Fin IBK
    ''' <summary>
    ''' Actualiza el nombre del documento de separación del conyugue en la tabla Contrato (SolicitudCredito)
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function ActualizaDocumentoSeparacion(ByVal pESolicitudCredito As String) As Boolean

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

    Function ModificaActivacionContrato(ByVal pESolicitudCredito As String) As Boolean

#End Region

#Region "ContratoNotarial"

    Function EliminarContratoNotarial(ByVal pESolicitudCredito As String, _
                                      ByVal pEGCC_ContratoNotarial As String) As Boolean

    Function ModificarContratoNotarial(ByVal pESolicitudCredito As String, _
                                      ByVal pEGCC_ContratoNotarial As String) As Boolean

    Function InsertarContratoNotarial(ByVal pESolicitudCredito As String, _
                                      ByVal pEGCC_ContratoNotarial As String) As Integer

    ''' <summary>
    ''' Actualiza el nombre del archivo adjunto en la tabla GCC_ContratoNotarial.
    ''' a una adenda.
    ''' </summary>
    ''' <param name="pEGCC_ContratoNotarial">Objeto EGCC_ContratoNotarial (Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function ContratoNotarialActualizarNombreArchivo(ByVal pESolicitudCredito As String, _
                                                     ByVal pEGCC_ContratoNotarial As String) As Boolean

#End Region

#Region "Adenda"

    Function InsertarAdenda(ByVal pEGcc_contratonotarial As String) As Integer

    Function ModificarAdenda(ByVal pEGCC_ContratoNotarial As String) As Boolean

    Function EliminarAdenda(ByVal pEGCC_ContratoNotarial As String) As Boolean

#End Region

#Region "Bien"

    Function InsertarBien(ByVal pESolicitudCredito As String, _
                          ByVal pESolicitudCreditoEstructura As String, _
                          ByVal pESolicitudCreditoEstructuraCarac As String) As Integer

    Function EliminarBien(ByVal pESolicitudCredito As String, _
                          ByVal pESolicitudCreditoEstructura As String) As Boolean

    Function ModificarBien(ByVal pESolicitudCredito As String, _
                           ByVal pESolicitudCreditoEstructura As String, _
                           ByVal pESolicitudCreditoEstructuraCarac As String) As Boolean
    Function ContratoDocumentoAdjuntoUpd(ByVal pEGcc_contratodocumento As String) As Boolean

#End Region

#Region "ContratoDocumento"

    Function ModificarTextoPredefinido(ByVal pESolicitudCredito As String, _
                                       ByVal pEGCC_ContratoDocumento As String) As Boolean

    Function EstadoAprobarLegal(ByVal pEGCC_ContratoDocumento As String) As Boolean

#End Region

#Region "ContratoOtroConcepto"

    Function fblnModificarContratoOtroConcepto(ByVal pEGCC_ContratoOtroConcepto As String) As Boolean
    Function ContratoOtroConceptoAdjuntoUpd(ByVal pESolicitudCredito As String, _
                                            ByVal pEGCC_ContratoOtroConcepto As String) As Boolean

#End Region

#Region "ObservacionesDocumento"
    Function InsertarContratoDocumentoObservacion(ByVal pEGcc_contratodocumentoObservacion As String) As Boolean
    Function InsertarContratoDocumentoObservacionInafectacion(ByVal pEGcc_contratodocumentoObservacion As String) As Boolean
#End Region

#Region "Elimina Documento Contrato"
    Function EliminaContratoDocumento(ByVal pEGcc_contratodocumento As String) As Boolean
#End Region

#Region "Inserta Seguimiento Contrato"
    Function InsertaSeguimientoContrato(ByVal pEGcc_SeguimientoContrato As String) As Boolean
#End Region

#Region "ContratoRepresentante"

    ''' <summary>
    ''' Ingresa un nuevo representante en la tabla representantes y para los representantes del contrato.
    ''' </summary>
    ''' <param name="pEGcc_representante">Entidad pEGcc_representante serializada</param>
    ''' <param name="pEGcc_contratorepresentante">Entidad EGcc_contratorepresentante serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function RepresentanteContratoIns(ByVal pESolicitudCredito As String, _
                                      ByVal pEGcc_representante As String, _
                                      ByVal pEGcc_contratorepresentante As String) As Boolean

#End Region

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IContratoNTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("D21C3BA9-4837-48d5-BAED-90BD15CB7C21")> _
Public Interface IContratoNTx

#Region "Contrato"

    Function RetornarContratoDatosCliente(ByVal pCodigoContrato As String) As String

    Function RetornarContrato(ByVal pCodigoContrato As String) As String

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

    Function ContratoCuentas(ByVal codigoContrato As String) As String
    'Inicio IBK RPR
    Function ObtenerContrato(ByVal codigoContrato As String) As String
    Function ObtenerCuentasContrato(ByVal codigoContrato As String) As String
    'Fin IBK
    Function RetTarifarioPredefContrato(ByVal CodProductoFinancieroActivo As String, _
                                       ByVal CodMoneda As String) As String

#End Region

#Region "ContratoNotarial"

    Function ListadoContratoNotarial(ByVal pNumeroContrato As String, _
                                     ByVal pCodigoOrigenAdenda As String) As String

    Function ListadoContratoNotarialPaginado(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pNumeroContrato As String, _
                                             ByVal pCodigoOrigenAdenda As String) As String

#End Region

#Region "Bien"

    Function ListadoBien(ByVal pPageSize As Integer, _
                         ByVal pCurrentPage As Integer, _
                         ByVal pSortColumn As String, _
                         ByVal pSortOrder As String, _
                         ByVal pCodsolicitudcredito As String, _
                         ByVal pCodProveedor As String) As String

#End Region

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
    ''' <param name="pCodigoContrato"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function RetornarTarifarioContrato(ByVal pCodigoContrato As String) As String

#End Region

#Region "ContratoDocumento"

    Function RetornarTextoPredefefinido(ByVal pCodigoContratoDocumento As Integer) As String

#End Region

#Region "ContratoDocumentoObservacion"

    Function RetornarObservacionContratoDocumento(ByVal PEGccDocumentoObservacion As String) As String
    'INICIO_JJM
    Function RetornarObservacionContratoDocumentoInafectacion(ByVal PEGccDocumentoObservacion As String) As String
    'FIN_JJM
#End Region

#Region "ContratoProveedor"

    Function ListadoContratoProveedor(ByVal pPageSize As Integer, _
                                      ByVal pCurrentPage As Integer, _
                                      ByVal pSortColumn As String, _
                                      ByVal pSortOrder As String, _
                                      ByVal pNumeroContrato As String) As String

    Function ListarDistinctContratoProveedores(ByVal pPageSize As Integer, _
                                               ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, _
                                               ByVal pSortOrder As String, _
                                               ByVal pNumeroContrato As String) As String

#End Region

#Region "Seguimiento Contrato"

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
                                       ByVal pNumeroContrato As String) As String
#End Region

#Region "Representantes"

    ''' <summary>
    ''' Listado de Representantes del cliente para todos los contratos
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function ListarDelCliente(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pCodigoContrato As String, _
                                   ByVal pCodUnico As String) As String

#End Region

#Region "Validar"
    ''' <summary>
    ''' Valida la duplicidad de las Condiciones del Documento
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : 
    ''' Fecha de Creación  : 
    ''' </remarks>
    Function SelCondicionesDocumentoCli(ByVal pEGcc_contratodocumento As String) As String

#End Region

#Region "Reporte Cotización-Contrato"

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

#End Region

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
    Function RetornarDatosCronogramaSituacionCreditoExcelHeader(ByVal codigoContrato As String) As String
    Function RetornarDatosCronogramaSituacionCreditoExcelResumen(ByVal codigoContrato As String, ByVal Usuario As String) As String
    Function RetornarDatosCronogramaSituacionCreditoExcelDetalle(ByVal codigoContrato As String, ByVal Usuario As String) As String
    Function RetornarDatosCronogramaPostSituacionCreditoExcel(ByVal codigoContrato As String, ByVal Usuario As String) As String
    'Fin IBK JJM
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
    ''' Fecha de Creación  : 29/01/2013 09:20:54 a.m. 
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
    ''' Fecha de Creación  : 29/01/2013 04:33:54 p.m. 
    ''' </remarks>
    Function fobjListadoContratosActivadosReporte(ByVal pTipo As String, _
                                             ByVal pFecha As String, _
                                             ByVal pCodigoClasificacionBien As String, _
                                             ByVal pCodigoTipoBien As String) As String
End Interface

#End Region
