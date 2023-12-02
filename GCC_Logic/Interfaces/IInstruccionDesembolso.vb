
Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IInstruccionDesembolsoTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("63F803C0-7D6F-4588-B3B0-4EFF941BAFEB")> _
Public Interface IInstruccionDesembolsoTx

    Function InsertarInsDesembolso(ByVal pEInsDesembolso As String, ByVal pESolicitudcredito As String) As String
    Function GestionInsDesembolso(ByVal pEInsDesembolso As String) As String
    Function InsertarInsDesembolsoAgrupacion(ByVal pEInsDesembolsoAgrupacion As String) As Boolean
    Function InsertarInsDesembolsoMedioPago(ByVal pEInsDesembolsoPago As String) As Boolean

    Function EliminarInsDesembolsoMedioPago(ByVal pEInsDesembolsoPago As String) As Boolean
    Function EliminarInsDesembolsoAgrupacion(ByVal pEDesembolsoAgrupacion As String) As Boolean
    Function recalcula(ByVal pEInsDesembolso As String) As Boolean

    Function ActualizarInsDesembolsoEstado(ByVal pEInsDesembolso As String) As Boolean

    Function InsertarInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As Boolean
    Function ModificarInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As Boolean
    Function EliminarInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As Boolean

    Function EjecutarInsDesembolsoEstado(ByVal pEInsDesembolso As String) As Boolean

    'Inicio IBK - AAE
    Function ActualizarEstadoejecucionInstruccionDesembolsoPago(ByVal pEInsDesembolso As String) As Boolean
    Function InsertarEjecucionDesembolsoPagoLog(ByVal pEInsDesembolsoPagoLog As String) As Boolean
    Function ActualizarEjecucionDesembolsoPagoLog(ByVal pEInsDesembolsoPagoLog As String) As Boolean
    Function EliminarEjecucionDesembolsoPagoLog(ByVal pEInsDesembolsoPagoLog As String) As Boolean
    Function EjecutarDesembolsoLPC(ByVal pFlag As String, ByVal pCodInstDesembolso As String, ByVal pRegUsuario As String) As Integer
    Function AnularInstDesembolso(ByVal pEInstruccionDesembolsoDoc As String) As String
    Function CheckRelacionesDocBienes(ByVal strCodContrato As String) As String
    Function LiberarInstDesembolso(ByVal pCodSolicitudCredito As String, ByVal pCodInstDesembolso As String) As Integer
    'Fin IBK
    'Inicio IBK - AAE - Activación de leasing parcial
    Function ActualizaCronogramaActivacion(ByVal strEGCC_InsDesembolsoActivacion As String, ByVal strCronograma As String) As Boolean
    'Fin IBK

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IInstruccionDesembolsoNTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("9BE792C0-98A9-44d3-A821-2079F81AC338")> _
Public Interface IInstruccionDesembolsoNTx

    Function ListadoInsDesembolso(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pEInsDesembolso As String) As String
    Function ListadoInsDesembolsoAgrupacion(ByVal pEInsDesembolsoAgrupacion As String) As String
    Function ListadoInsDesembolsoDocAgrupacion(ByVal pEInsDesembolsoAgrupacion As String) As String
    Function ListadoInsDesembolsoCargoAbono(ByVal pEInsDesembolsoAgrupacion As String) As String
    Function ListadoInsDesembolsoMedioPago(ByVal pEInsDesembolsoPago As String) As String
    Function ListadoInsDesembolsoMedioPagoGet(ByVal pEInsDesembolsoPago As String) As String
    Function ObtenerInsDesembolso(ByVal pEInsDesembolso As String) As String

    Function ObtenerInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As String
    Function ListadoInstruccionDesembolsoDoc(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEInstruccionDesembolsoDoc As String _
                                                ) As String

    Function ValidaEjecucionInstruccion(ByVal strEGInstruccionDesembolso As String) As String

    'Inicio IBK - AAE
    Function getCargosCuentaInsDes(ByVal pESolicitudcredito As String, ByVal pEInsDesembolso As String) As String
    Function ListadoInsDesembolsoMedioPago2(ByVal pEInsDesembolsoPago As String) As String
    Function obtenerInfoProveedor(ByVal pCodProv As String) As String
    Function callProgramaHost(ByVal argsTrama As String, _
                                     ByVal argsUsuarioTld As String, _
                                     ByVal argsAgenciaTld As String, _
                                     ByVal argsPrograma As String, _
                                     ByVal argsFuncion As String, _
                                     ByRef argsMensaje As String, _
                                     ByRef argsTldDatosTran As String) As String
    Function ListadoInsDesembolsoTotales(ByVal pCodContrato As String, ByVal pInsDesembolsoPago As String) As String
    Function obtenerWIO(ByVal pCodContrato As String, ByVal pInsDesembolsoPago As String) As String
    Function obtenerContablesMedioPago(ByVal strCodMedioPago As String) As String
    Function obtenerContablesSUNAT(ByVal strCodTipoAgrupacion As String) As String
    Function TieneNotasCredito(ByVal pstrCodSolicitudCredito As String, ByVal pstrCodInstruccionDesembolso As String, ByVal pstrCodAgrupacion As String, ByVal pstrCodCorrelativo As String) As Boolean
    ' Inicio - IBK - AAE - Activación Leasing Parcial
    Function ListadoInsDesembolsoActParcial(ByVal pEInsDesembolso As String) As String
    Function ListaDesembolsos(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pstrNroContrato As String, _
                                       ByVal pstrCodInstDesembolso As String) As String
    Function CronogramaActivacionGet(ByVal pstrNroContrato As String, _
                                                      ByVal pstrInstruccionDesembolso As String _
                                                      ) As String
    Function ValidaEjecucionInstruccionActParcial(ByVal strEGInstruccionDesembolso As String, ByVal strFlagCheckPrecuota As String) As String
    Function ReporteLeasingEnProceso(ByVal FecDesIni As String, ByVal FecDesFin As String, ByVal Moneda As String, ByVal CodSolCredito As String, ByVal Flag As String) As String

    ' Fin IBK 
    'AAE - Replico funciones de WIO que no funcionan de WEB
    Function fObtenerLineaOP(ByVal pstrCodigoUnico As String, ByVal pintTipo As Integer, ByVal pintCodigoProducto As Integer) As String
    Function fObtenerDatosLineaOP(ByVal pstrCodigoLinea As String) As String
    Function fObtenerTasasLineas(ByVal pstrCodUnico As String, ByVal pstrCodigoLinea As String) As String
    Function fObtenerParamDomWio(ByVal pstrCodDominio As String, ByVal pstrCodParam As String, ByVal pstrTipo As String) As String
    Function fintObtenerSecuenciaLs(ByVal pstrNumeroIO As String) As Integer

    ' Fin IBK

    ''' <summary>
    ''' Lista todos los desembolsos mensuales
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <param name="pFechaTermino">Fecha de término de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 22/01/2013 05:29:54 p.m. 
    ''' </remarks>
    Function fobjListadoDesembolsoMensualReporte(ByVal pFechaInicio As String, _
                                                ByVal pFechaTermino As String) As String
    Function ListaAgrupacionVoucher(ByVal pstrNroContrato As String, ByVal pstrNroInstruccion As String, ByVal pstrCorrelativo As String) As String 'JJM IBK 
End Interface

#End Region

