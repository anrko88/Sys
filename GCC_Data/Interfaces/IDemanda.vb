Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IDemandaTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 12/11/2012
''' </remarks>
<Guid("0F7658A3-F28D-4867-A630-BB6FC8BF8116")> _
Public Interface IDemandaTx

    Function InsertarDemanda(ByVal pEDemanda As String) As String
    Function ModificarDemanda(ByVal pEDemanda As String) As Boolean
    Function EliminarDemanda(ByVal pEDemanda As String) As Boolean

    Function InsertarDemandaImplicado(ByVal pEImplicado As String) As Boolean
    Function ModificarDemandaImplicado(ByVal pEImplicado As String) As Boolean
    Function EliminarDemandaImplicado(ByVal pEImplicado As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IDemandaNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 12/11/2012
''' </remarks>
<Guid("7DE10BDE-CA38-42c8-A4E7-D0128D600E7B")> _
Public Interface IDemandaNTx

    Function ListadoDemandaContrato(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEDemanda As String) As String
    Function GetDemandaContrato(ByVal pEDemanda As String) As String
    Function ListadoDemanda(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pEDemanda As String) As String
    Function GetDemanda(ByVal pEDemanda As String) As String

    Function ListadoDemandaImplicado(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pEImplicado As String) As String
    Function GetDemandaImplicado(ByVal pEImplicado As String) As String

End Interface

#End Region
