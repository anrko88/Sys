Imports System.Runtime.InteropServices


#Region "Interface Transaccional"


''' <summary>
''' Interfaz de métodos para la clase ILSGConceptosTarifasTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("7225B490-6405-4cf7-B514-2A3419626FB7")> _
Public Interface ILSGConceptosTarifasTx


End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ILSGConceptosTarifasNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("779D1C63-4F7A-44a8-9532-6AEB53581365")> _
Public Interface ILSGConceptosTarifasNTx


    Function RetornarTarifarioPredefContrato(ByVal CodProductoFinancieroActivo As String, _
                                             ByVal CodMoneda As String) As String


    Function ConsultaConceptosTarifas(ByVal pdblimporte As Decimal, _
                                      ByVal pstrTipoConcepto As String, _
                                      ByVal pstrCodtarifa As String, _
                                      ByVal pstrCodmoneda As String) As String

End Interface

#End Region