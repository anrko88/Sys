Imports System.Runtime.InteropServices

#Region "Interface Transaccional"
''' <summary>
''' Interfaz de métodos para la clase IPagosTx
''' </summary>
''' <remarks>
''' Creado Por         : IBK - RPR
''' Fecha de Creación  : 06/01/2013
''' </remarks>
<Guid("40983309-2335-4cf5-BF9B-7AD3444C1C28")> _
Public Interface IPagosTx

    Function IngresarPagoCuotas(ByVal pECreditoRecuperacion As String) As String
    Function ProcesarLiquidacion(ByVal pEGCC_Liquidacion As String) As String
    Function ActualizarEstadoLiquidacion(ByVal pECreditoRecuperacion As String) As Boolean
    Function ActualizarEstadoPagoCuotas(ByVal pECreditoRecuperacion As String) As Boolean

    Function InsertaConceptoDetalle(ByVal pECodSolCredito As String) As Boolean
    Function ActualizaConceptoDetalle(ByVal pECodSolCredito As String) As Boolean
    Function EliminaConceptoDetalle(ByVal pECodSolCredito As String) As Boolean
    Function IngresarPagoConceptos(ByVal pECreditoRecuperacion As String) As String

    Function TMPInsertarCronograma(ByVal pEGCC_Liquidacion As String) As Boolean

    Function ActualizarTMPLiquidacion(ByVal pEGCC_Liquidacion As String) As String 'JJM IBK 02/04/2013
    Function ActualizarTMPLiquidacionAplicacion(ByVal pEGCC_Liquidacion As String) As String 'JJM IBK 10/04/2013
End Interface

#End Region

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase IPagosNTx
''' </summary>
''' <remarks>
''' Creado Por         : IBK - RPR
''' Fecha de Creación  : 26/12/2012
''' </remarks>
<Guid("D7922DFF-53E9-4822-ADED-06E41FD8E1C5")> _
Public Interface IPagosNTx

    Function ListadoLiquidaciones(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pEGCC_Liquidacion As String) As String
    Function ObtenerLiquidacion(ByVal pEGCC_Liquidacion As String) As String
    Function ObtenerCuotaAtrasadaComision(ByVal pEGCC_Liquidacion As String) As String

    Function SeleccionarAplicacionComision(ByVal pECreditoRecuperacionComision As String) As Boolean
    Function ListadoPagoCuotas(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pEPagoCuotas As String) As String
    Function ObtenerPagoCuotas(ByVal pECreditoRecuperacion As String) As String
    Function ObtenerPagoCuotasTotales(ByVal pECreditoRecuperacion As String) As String
    Function ObtenerDetalleCuotas(ByVal pECreditoRecuperacion As String) As String
    Function ObtenerProximasCuotas(ByVal pECreditoRecuperacion As String, ByVal pNroCuotas As Integer) As String
    Function ObtenerDetalleComisiones(ByVal pECreditoRecuperacion As String) As String
    Function ObtenerProximasComisiones(ByVal pECreditoRecuperacion As String) As String
    Function ObtenerTramaAutorizacionPagosVentanilla(ByVal pECreditoRecuperacion As String, ByVal STATUS As String) As String

    'IBK - JJM

    Function ListadoPagoConcepto(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEPagoConcepto As String) As String
    Function ListadoPagoConceptoDetalle(ByVal pEPagoConcepto As String) As String
    Function ListadoPagoConceptoxNumeroSecuencia(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEPagoConcepto As String) As String
    Function ObtenerConceptoEspecifico(ByVal pEPagoConcepto As String) As String
    Function ListadoPagoConceptoxNumeroSecuenciaTemporal(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEPagoConcepto As String) As String
    Function ObtenerCreditoConsulta(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pENroDocumento As String, ByVal pERazonSocial As String, _
                                                ByVal pETipoDocumento As String) As String
    Function GetDatosCarta(ByVal pNroLote As String) As String
    Function GetDatosCartaExcel(ByVal pNroLote As String) As String
End Interface

#End Region

