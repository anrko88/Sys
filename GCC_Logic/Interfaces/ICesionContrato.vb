Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ICesionContratoTx
''' </summary>
''' <remarks>TSF-JRC | 11/01/2012</remarks>
<Guid("B50B0509-972B-44e3-83D8-CDA77A8A0CD7")> _
Public Interface ICesionContratoTx

    Function ModificarCesionContrato(ByVal pECesionContrato As String) As Boolean
    Function RealizarCesionContrato(ByVal pECesionContrato As String) As Boolean

End Interface

#End Region


#Region "Interface NO Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ICesionContratoNTx
''' </summary>
''' <remarks>TSF-JRC | 11/01/2012</remarks>
<Guid("150B8601-57BB-4774-8567-498BCB169D52")> _
Public Interface ICesionContratoNTx

    Function ListadoCesionContrato(ByVal pPageSize As Integer, _
                                ByVal pCurrentPage As Integer, _
                                ByVal pSortColumn As String, _
                                ByVal pSortOrder As String, _
                                ByVal pECesionContrato As String) As String
    Function GetCesionContrato(ByVal pECesionContrato As String) As String

    ''' <summary>
    ''' Lista todas las cesiones por contrato
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <param name="pFechaTermino">Fecha de término de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 17/01/2013 10:31:54 a.m. 
    ''' </remarks>
    Function ListadoCesionContratoReporte(ByVal pFechaInicio As String, _
                                          ByVal pFechaTermino As String) As String
End Interface

#End Region
