Imports System.Runtime.InteropServices

#Region "Interface Transaccional"
''' <summary>
''' Interfaz de métodos para la clase IContratoRepresentanteTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("DAE3D318-196D-4c26-94D2-319726D51A11")> _
Public Interface IContratoRepresentanteTx

    Function ContratoRepresentanteIns(ByVal pEGcc_contratoRepresentante As String) As String
    Function RepresentanteContratoItemDel(ByVal pEGcc_contratoRepresentante As String) As String
    Function RepresentanteContratoDel(ByVal pEGcc_contratoRepresentante As String) As String

End Interface

#End Region

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase ContratoRepresentanteNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("17165892-3CA1-43f0-9FCE-3AD167BB004B")> _
Public Interface IContratoRepresentanteNTx

    Function ContratoRepresentanteSel(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEGcc_contratoRepresentante As String, _
                                                ByVal pFirma As String) As String


End Interface

#End Region

