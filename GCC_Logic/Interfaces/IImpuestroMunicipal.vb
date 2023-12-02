Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IImpuestoMunicipalTx
''' </summary>
''' <remarks>TSF-JRC | 12/11/2012</remarks>
<Guid("94613747-E8C0-4b59-B7A0-1F81E62CE1DF")> _
Public Interface IImpuestoMunicipalTx
    'Inicio IBK - AAE - Retorno un string
    Function InsertarImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String
    Function ModificarImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String
    Function EliminarImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As Boolean
    Function AsignarLoteImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String
    Function AsignarChequeImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As Boolean
    Function EliminarLoteImpuestoMunicipal(ByVal pNroLote As String) As Boolean ' JJM IBK

End Interface

#End Region


#Region "Interface NO Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IImpuestoMunicipalNTx
''' </summary>
''' <remarks>TSF-JRC | 12/11/2012</remarks>
<Guid("FDADA0C0-A6AB-4F36-AD90-AC22DDB411B5")> _
Public Interface IImpuestoMunicipalNTx

    Function ListadoImpuestoMunicipal(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEImpuestoMunicipal As String) As String

    Function GetImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String

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

    Function ListadoReporteImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String

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
