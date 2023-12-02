Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IImpuestoMunicipalTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 12/11/2012
''' </remarks>
<Guid("B711176A-760B-4ec1-A191-9A59ED6D3E3F")> _
Public Interface IImpuestoMunicipalTx
    'Inicio IBK - AAE - Retorno un string
    Function InsertarImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String
    Function ModificarImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String
    Function EliminarImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As Boolean
    Function AsignarLoteImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String
    Function AsignarChequeImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As Boolean
    Function EliminarLoteImpuestoMunicipal(ByVal pNroLote As String) As Boolean 'JJM IBK  


End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IImpuestoMunicipalNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 12/11/2012
''' </remarks>
<Guid("F40D9A02-DB8E-4C27-90FD-07C3EAD715E3")> _
Public Interface IImpuestoMunicipalNTx

    Function ListadoImpuestoMunicipal(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEImpuestoMunicipal As String) As String
    Function GetImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String
    Function ListadoReporteImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String
    Function ListadoImpuestoMunicipalBienes(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEImpuestoMunicipal As String) As String
    Function GetImpuestoMunicipalBienes(ByVal pEImpuestoMunicipal As String) As String

    Function ListarImpuestoMunicipalLiquidar(ByVal pPageSize As Integer, _
                                  ByVal pCurrentPage As Integer, _
                                  ByVal pSortColumn As String, _
                                  ByVal pSortOrder As String, _
                                  ByVal pCodLote As String) As String

    Function ListarImpuestoMunicipalLiquidarTodo(ByVal pCodLote As String) As String

    Function ListarImpuestoMunicipalReporteLiquidar(ByVal pCodigoImpuesto As String) As String
    'Inicio IBK
    Function GetImpuestoMultasInmueble(ByVal pNroLote As String) As String
    Function GetCodigoPredioBien(ByVal pCodSolicitud As String, ByVal pSecFinanciamiento As String) As String
    Function ListadoImpuestoMunicipalxLote(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pEImpuestoMunicipal As String) As String
    Function GetImpuestoTotalesInmueble(ByVal pEImpuestoMunicipal As String) As String
    Function DescuentoLoteImpuestoMunicipal(ByVal pNroLote As String, ByVal pUsuarioModificacion As String, ByVal pDescuento As Decimal) As String 'JJM IBK  
    'Fin IBK

End Interface

#End Region


