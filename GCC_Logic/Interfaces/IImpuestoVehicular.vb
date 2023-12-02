

Imports System.Runtime.InteropServices
#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IImpuestoVehicularNTX
''' </summary>
''' <remarks>TSF-AEP | 14/11/2012</remarks>
<Guid("B3309684-35DF-41ea-9C7D-72411F1B97EB")> _
Public Interface IImpuestoVehicularTX
    'Inicio IBK - AAE - Retorno un string
    Function GrabarImpuestoVehicular(ByVal pEImpuestoVehicular As String) As String
    Function ModificarImpuestoVehicular(ByVal pEImpuestoVehicular As String) As String
    'Fin IBK
    Function AsignarLoteImpuestoVehicular(ByVal pEImpuestoVehicular As String) As String
    ' Inicio IBK - Agrego Parametro
    Function EliminarImpuestoVehicular(ByVal pIntCodigoImpuesto As String, ByVal pStrCodigoUsuario As String, ByVal pstrLote As String) As Boolean
    Function AsignarChequeImpuestoVehicular(ByVal pEImpuestoVehicular As String) As Boolean


    '============MULTAS===============
    Function EliminarLote(ByVal pLote As String) As Boolean 'JJM IBK
    'Inicio IBK - AAE - Retorno un string
    Function GrabarMultaVehicular(ByVal pEMultaVehicular As String) As String
    Function ModificarMultaVehicular(ByVal pEMultaVehicular As String) As String
    ' Inicio IBK - Agrego Parametro
    Function EliminarMultaVehicular(ByVal pIntCodigoMulta As String, ByVal pStrCodigoUsuario As String, ByVal pstrLote As String) As Boolean
    Function AsignarLoteMultaVehicular(ByVal pEImpuestoVehicular As String) As String
    Function AsignarChequeMultaVehicular(ByVal pEMultaVehicular As String) As Boolean
    'Inicio IBK - AAE
    Function AsignarLoteImpuestoVehicular2(ByVal pEImpuestoVehicular As String) As String
    Function ReGenerarLote(ByVal strLote As String) As String
    Function AnularLote(ByVal pLote As String) As String
    'Fin IBK
End Interface
#End Region
#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IImpuestoVehicularNTX
''' </summary>
''' <remarks>TSF-AEP | 12/11/2012</remarks>
<Guid("554F2A03-6ADD-42ba-A365-6EC04B11EA1D")> _
Public Interface IImpuestoVehicularNTX

    Function LiquidarLote(ByVal pUsuarioModificacion As String, ByVal pNroLote As String, ByVal pCodigoConcepto As String) As String 'JJM IBK

    Function ListarImpuestoVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pEImpuestoVehicular As String) As String

    Function ListarBienImpuestoVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pPlaca As String, _
                                        ByVal pSecfinanciamiento As String, _
                                           ByVal pNroMotor As String, _
                                           ByVal pCUCliente As String, _
                                           ByVal pCodContrato As String) As String

    Function ListarImpuestoVehicularReporte(ByVal pEImpuestoVehicular As String) As String

    Function ListarLoteImpuestoVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pPlaca As String, _
                                   ByVal pTipo As Integer, _
                                   ByVal pNroLote As String) As String

    Function ListarLoteImpuestoVehicularConsulta(ByVal pPageSize As Integer, _
                                ByVal pCurrentPage As Integer, _
                                ByVal pSortColumn As String, _
                                ByVal pSortOrder As String, _
                                ByVal pPlaca As String, _
                                ByVal pTipo As Integer) As String

    Function ObtenerDatosImpuesto(ByVal pstrPlaca As String, ByVal pCodImpuesto As Integer) As String

    Function ListarCuotasPeriodo(ByVal piiCodigoBien As Integer, ByVal piiPeriodo As Integer, ByVal picCodigoContrato As String) As String

    Function ObtenerPeriodosValidacion(ByVal pstrCadigosImpuesto As String) As String

    Function ObtenerTotalCuotas(ByVal pstrCadigosBien As String, ByVal pstrCodigosPeriodo As String, ByVal pstrContratos As String) As String

    Function ListarImpuestoVehicularLiquidar(ByVal pPageSize As Integer, _
                                ByVal pCurrentPage As Integer, _
                                ByVal pSortColumn As String, _
                                ByVal pSortOrder As String, _
                                ByVal pCodLote As String) As String
    Function ListarImpuestoVehicularReporteLiquidar(ByVal pCodigoImpuesto As String) As String

    Function ListarImpuestoVehicularLiquidarTodo(ByVal pCodLote As String) As String
    Function GetImpuestoVehicular(ByVal pNroLote As String) As String 'JJM IBK
    '===============================================================================
    'MULTA VEHICULAR

    Function ListarMultaVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pEMultaVehicular As String) As String

    Function ListarBienMultaVehicular(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pCodMunicipalidad As String, _
                                    ByVal pPlaca As String, _
                                        ByVal pSecfinanciamiento As String, _
                                           ByVal pNroMotor As String, _
                                           ByVal pCUCliente As String, _
                                           ByVal pCodContrato As String) As String


    Function ObtenerDatosMulta(ByVal pstrPlaca As String, ByVal pintSecMulta As Integer) As String

    ''' <summary>
    ''' Obtiene un registro de la multa vehicular
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 21/01/2013
    ''' </remarks>
    Function ObtenerDatosMultaConsulta(ByVal pEMultaVehicular As String) As String


    Function ListarLoteMultaVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pPlaca As String, _
                                   ByVal pCodMunicipal As String, _
                                    ByVal pTipo As Integer, _
                                   ByVal pNroLote As String) As String

    Function ListarEscalaInfraccionesMulta() As String

    Function ListarMultaVehicularLiquidar(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pCodLote As String) As String

    Function ListarMultaVehicularLiquidarTodo(ByVal pCodLote As String) As String

    Function ListarMultaVehicularReporteLiquidar(ByVal pCodigoImpuesto As String) As String
    Function GetImpuestoMultas(ByVal pNroLote As String) As String 'JJM IBK
    '===============================================================================
    'ALERTAS
    Function ListarAltertaImpuestoVehicular(ByVal pNroLote, _
                                            ByVal pNroCheque) As String

    'Inicio IBK - AAE - 13/02/2013 - Agrego funcion
    Function CheckLote(ByVal pNroLote As String, ByVal pflag As String) As String
    Function ObtenerHeaderLote(ByVal pNroLote As String, ByVal pflag As String) As String
    'Fin IBK    
End Interface
#End Region