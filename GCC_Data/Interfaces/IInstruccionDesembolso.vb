
Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IInstruccionDesembolsoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 25/09/2012
''' </remarks>
<Guid("888F2B14-637E-484d-9353-66BC5752645C")> _
Public Interface IInstruccionDesembolsoTx

    Function InsertarInsDesembolso(ByVal pEInsDesembolso As String) As String
    Function GestionInsDesembolso(ByVal pEInsDesembolso As String) As String
    Function InsertarInsDesembolsoAgrupacion(ByVal pEInsDesembolsoAgrupacion As String) As Boolean
    Function InsertarInsDesembolsoMedioPago(ByVal pEInsDesembolsoPago As String) As Boolean
    Function EliminarInsDesembolsoAgrupacion(ByVal pEInsDesembolsoAgrupacion As String) As Boolean
    Function EliminarInsDesembolsoMedioPago(ByVal pEInsDesembolsoPago As String) As Boolean
    Function recalcula(ByVal pEInsDesembolso As String) As Boolean
    Function ActualizarInsDesembolsoEstado(ByVal pEInsDesembolso As String) As Boolean

    Function EjecutarInsDesembolsoEstado(ByVal pEInsDesembolso As String) As Boolean

    'Inicio IBK - AAE
    Function ActualizarEstadoejecucionInstruccionDesembolsoPago(ByVal pEInsDesembolsoPago As String) As Boolean
    Function InsertarEjecucionDesembolsoPagoLog(ByVal pEInsDesembolsoPagoLog As String) As Boolean
    Function ActualizarEjecucionDesembolsoPagoLog(ByVal pEInsDesembolsoPagoLog As String) As Boolean
    Function EliminarEjecucionDesembolsoPagoLog(ByVal pEInsDesembolsoPagoLog As String) As Boolean
    Function EjecutarDesembolsoLPC(ByVal pFlag As String, ByVal pCodInstDesembolso As String, ByVal pRegUsuario As String) As Integer
    Function obtenerCheckeados(ByVal pCodContrato As String, ByVal pInsDesembolso As String) As String
    Function actualizaTC(ByVal pCodContrato As String) As Integer
    Function actualizaTCID(ByVal pEInsDesembolso As String) As Boolean
    Function regeneraID(ByVal pCodContrato As String, ByVal pCodInstDesembolso As String, ByVal pCheckeados As String, ByVal pUsuario As String) As String
    Function AnularInstDesembolso(ByVal pEInstruccionDesembolsoDoc As String) As String
    Function CheckRelacionesDocBienes(ByVal strCodContrato As String) As String
    Function LiberarInstDesembolso(ByVal pCodSolicitudCredito As String, ByVal pCodInstDesembolso As String) As Integer
    'Fin IBK
    'Inicio IBK - AAE - Activación de leasing parcial
    Function ActualizarInformacionActivacion(ByVal strEGCC_InsDesembolsoActivacion As String) As Boolean
    Function EliminarCronogramaActivacion(ByVal pCodNroContrato As String, ByVal pCodInstruccionDesembolso As String) As Boolean
    Function InsertarCronogramaActivacion(ByVal pCodNroContrato As String, ByVal pCodInstruccionDesembolso As String, ByVal pECotizacioncronograma As String) As Boolean
    Function ActualizarNroWIOActivacionParcial(ByVal pCodNroContrato As String, ByVal pNroInstruccionWIO As String) As Boolean
    'Fin IBK    

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IInstruccionDesembolsoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 25/0/2012
''' </remarks>
<Guid("AF9F4551-9A7E-4568-A626-A8B3B4384A9D")> _
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
    Function ValidaEjecucionInstruccion(ByVal strEGInstruccionDesembolso As String) As String
    'Inicio IBK - AAE
    Function getCargosCuentaInsDes(ByVal pESolicitudcredito As String, ByVal pEInsDesembolso As String) As String
    Function ListadoInsDesembolsoMedioPago2(ByVal pEInsDesembolsoPago As String) As String
    Function obtenerInfoProveedor(ByVal pCodProv As String) As String
    Function ListadoInsDesembolsoTotales(ByVal pCodContrato As String, ByVal pInsDesembolsoPago As String) As String
    Function obtenerWIO(ByVal pCodContrato As String, ByVal pInsDesembolsoPago As String) As String
    Function obtenerContablesMedioPago(ByVal strCodMedioPago As String) As String
    Function obtenerContablesSUNAT(ByVal strCodTipoAgrupacion As String) As String
    Function TieneNotasCredito(ByVal pstrCodSolicitudCredito As String, ByVal pstrCodInstruccionDesembolso As String, ByVal pstrCodAgrupacion As String, ByVal pstrCodCorrelativo As String) As Boolean

    ' Fin IBK
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
