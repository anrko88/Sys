Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

<Guid("1CB63967-3F48-4644-A02A-A62462CFBDAD")> _
Public Interface ITasadorTx

    Function InsertarTasador(ByVal pEGCC_ContratoTasador As String) As String
    Function ActualizaTasador(ByVal pEGCC_ContratoTasador As String) As String
    Function EnviarCarta(ByVal pEGCC_ContratoTasador As String) As String
    Function ActualizarTasacion(ByVal pEGCC_ContratoTasador As String) As String
End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
<Guid("B7227F95-A158-4ccd-9BD3-87D7F8646B38")> _
Public Interface ITasadorNTx

    ''' <summary>
    ''' Obtiene Datos de Contrato para Tasacion
    ''' </summary>
    ''' <param name="pNumeroContrato">Numero de Contrato para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 28/01/2013
    ''' </remarks>
    Function ObtenerContratoTasacion(ByVal pNumeroContrato As String) As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pCodSolicitudcredito"></param>
    ''' <param name="pCuCliente"></param>
    ''' <param name="pRazonsolcial"></param>
    ''' <param name="pTipoDocumento"></param>
    ''' <param name="pNumerodocumento"></param>
    ''' <param name="pEstadoTasacion"></param>
    ''' <param name="pClasificacionBien"></param>
    ''' <param name="pBanca"></param>
    ''' <param name="pEjecutivoBanca"></param>
    ''' <param name="pPeriodo"></param>
    ''' <param name="pFechadesde"></param>
    ''' <param name="pFechaHasta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListadoContratoTasador(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pCodSolicitudcredito As String, _
                                            ByVal pCuCliente As String, _
                                            ByVal pRazonsolcial As String, _
                                            ByVal pTipoDocumento As String, _
                                            ByVal pNumerodocumento As String, _
                                            ByVal pEstadoTasacion As String, _
                                            ByVal pClasificacionBien As String, _
                                            ByVal pBanca As String, _
                                            ByVal pEjecutivoBanca As String, _
                                            ByVal pPeriodo As String, _
                                            ByVal pFechadesde As String, _
                                            ByVal pFechaHasta As String, _
                                            ByVal pEstadoTasacionContrato As String) As String

    Function ObtenerContratoCotizacionSaldoFinanciado(ByVal pNumeroContrato As String) As String

    ''' <summary>
    ''' Obtiene el listado de la vista GCC_ContratoTasacion
    ''' </summary>
    ''' <param name="pCodSolicitudcredito">Código de Contrato</param>    
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Function ListaContratoBienTasacion(ByVal pPageSize As Integer, _
                                       ByVal pCurrentPage As Integer, _
                                       ByVal pSortColumn As String, _
                                       ByVal pSortOrder As String, _
                                       ByVal pCodSolicitudcredito As String) As String

    ''' <summary>
    ''' Obtiene el listado de la vista UV_GCC_TASACIONBIENCONTRATO
    ''' </summary>
    ''' <param name="pCodSolicitudcredito">Código de Contrato</param>
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 18/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Function ListaBienTasacion(ByVal pPageSize As Integer, _
                               ByVal pCurrentPage As Integer, _
                               ByVal pSortColumn As String, _
                               ByVal pSortOrder As String, _
                               ByVal pCodSolicitudcredito As String) As String

    ''' <summary>
    ''' Obtiene el listado de la vista UV_GCC_HISTORICOTASACIONBIENCONTRATO
    ''' </summary>
    ''' <param name="pCodSolicitudcredito">Código de Contrato</param>
    ''' <param name="pCodContratoTasacion">Código de Contrato Tasación</param>
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 08/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Function ListaHistoricoContratoBienTasacion(ByVal pPageSize As Integer, _
                                               ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, _
                                               ByVal pSortOrder As String, _
                                               ByVal pCodSolicitudcredito As String, _
                                               ByVal pCodContratoTasacion As Short) As String

    Function calculatotales(ByVal pCodSolicitudcredito As String) As String


End Interface

#End Region