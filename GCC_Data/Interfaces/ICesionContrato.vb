Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ICesionContratoTx
''' </summary>
''' <remarks>Function ModificarCesionContrato(ByVal pECesionContrato As String) As Boolean
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 11/01/2013
''' </remarks>
<Guid("AFA4BECD-B725-4a1a-B67A-9B513DAC0C91")> _
Public Interface ICesionContratoTx
    Function ModificarCesionContrato(ByVal pECesionContrato As String) As Boolean
    Function RealizarCesionContrato(ByVal pECesionContrato As String) As Boolean
End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ICesionContratoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 11/01/2013
''' </remarks>
<Guid("2825C204-EB1D-4188-AB9E-50B618997B3B")> _
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
    ''' Fecha de Creación  : 17/01/2013 10:26:54 a.m. 
    ''' </remarks>
    Function ListadoCesionContratoReporte(ByVal pFechaInicio As String, _
                                          ByVal pFechaTermino As String) As String
End Interface

#End Region
