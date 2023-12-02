Imports System.Runtime.InteropServices


#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IUtilTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("646A71E4-6BE3-4998-B689-75BDE458B30B")> _
Public Interface IUtilTx

    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <param name="pstrCodigoContrato">Código del Contrato</param>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 05/09/2012
    ''' </remarks>
    Function fblnGestionFlujo(ByVal pstrCodigoContrato As String, _
                                 ByVal pstrCodigoModulo As String, _
                                 ByVal pstrUsuarioRegistro As String) As Boolean
    Function InsertarContactoSuprestatario(ByVal pstrCodigoSuprestatario As String, _
                             ByVal pstrNombre As String, _
                             ByVal pstrCorreo As String, _
                             ByVal pstrTelefono As String) As Boolean

    Function ModificarContactoSuprestatario(ByVal pstrCodSuprestatarioContacto As Integer, ByVal pstrCodigoSuprestatario As String, _
                                 ByVal pstrNombre As String, _
                                 ByVal pstrCorreo As String, _
                                 ByVal pstrTelefono As String, _
                                 ByVal pstrEstado As String) As Boolean

    Function InsertarContactoPreferente(ByVal pstrCodSuprestatarioContacto As Integer, ByVal pstrCodigoSuprestatario As String, _
                                               ByVal pstrNombre As String, ByVal pstrCorreo As String, ByVal pstrTelefono As String) As Boolean

    Function ActualizarSubprestatario(ByVal pstrCodigoSuprestatario As String, _
                                               ByVal pstrDireccion As String) As Boolean


End Interface

#End Region


#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IUtilNTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("C74F407A-A100-44bc-AEA8-B18704D46A79")> _
    Public Interface IUtilNTx

    'Inicio IBK RPR
    Function ObtenerFechaCierre(ByVal pCodModulo As String) As String
    'Fin IBK

    'Function ListadoValorGenerica(ByVal pstrTablaGenerica As String) As String
    'Function ListadoValorGenericaAnidada(ByVal pstrTablaGenerica As String, ByVal pstrCodigo As String) As String
    Function ListadoMoneda() As String
    Function ListadoPais() As String
    Function ObtenerValorGenerico(ByVal pstrDominio As String, ByVal pstrParametro As String) As String

    ''' <summary>
    ''' Listado de los contratos coincidentse con los criterios de búsqueda
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 09/05/2012
    ''' </remarks>
    Function ListarEstadosBusquedaContrato() As String

    'IBK - RPH
    Function ListarNotarias(ByVal pCodDepartamento As String, _
                            ByVal pCodProvincia As String) As String

    'Inicio IBK - RPH
    Function ObtenerContactoNotarias(ByVal pCodNotaria As String) As String
    'Fin IBK - RPH

    Function ObtenerTipoCambio(ByVal pCodMoneda As String, ByVal pTipoModalidadCambio As String, ByVal pFechaInicioVigencia As String) As String

    ''' <summary>
    ''' ConsultaConceptosTarifas
    ''' </summary>
    ''' <param name="pdblimporte"></param>
    ''' <param name="pstrCodtarifa"></param>
    ''' <param name="pstrCodmoneda"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConsultaConceptosTarifas(ByVal pdblimporte As Decimal, _
                                     ByVal pstrTipoConcepto As String, _
                                     ByVal pstrCodtarifa As String, _
                                     ByVal pstrCodmoneda As String) As String
    ''' <summary>
    ''' ListarRegistroCompra
    ''' </summary>
    ''' <param name="strFechaIni"></param>
    ''' <param name="strFechaFin"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListarRegistroCompra(ByVal strFechaIni As String, ByVal strFechaFin As String) As String

    ''' <summary>
    ''' ListarRegistroCompra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListarPipeline() As String

    ''' <summary>
    ''' ListarRetenciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListarRetenciones(ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String
    Function ListarRetencionesSunat(ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String 'JJM IBK
    'Inicio IBK - AAE
    Function ListarRegistrosVentas(ByVal strFlag As String, ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String
    Function ListarPipeline2(ByVal strCotiza As String, _
                            ByVal pCUCliente As String, _
                            ByVal pRazonSocialCli As String, _
                            ByVal pCodEjecutivo As String, _
                            ByVal pCodBanca As String, _
                            ByVal pCodEstado As String) As String
    'Fin IBK
    'Inicio IBK JJM
    Function ListarRegistrosCompras(ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String
    'Inicio IBK - AAE
    Function ListarNotasAbono(ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String
    'Fin IBK

    'TS_AEP
    Function ListadoClienteSuprestatario(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pCodigo As String, _
                                     ByVal pCuCliente As String, _
                                     ByVal pTipoDocumento As String, _
                                     ByVal pNumeroDocumento As String, _
                                     ByVal pNombreCliente As String, _
                                     ByVal pDireccion As String, _
                                     ByVal pNombre As String, _
                                     ByVal pCorreo As String, _
                                     ByVal pTelefono As String) As String
    'TS_AEP

    Function ListarContactoSuprestatario(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pCodSuprestario As String, _
                                     ByVal pNombre As String, _
                                     ByVal pCorreo As String, _
                                     ByVal pTelefono As String) As String

    Function ObteberContactoPreferente(ByVal pCodSuprestario As String) As String

End Interface

#End Region
