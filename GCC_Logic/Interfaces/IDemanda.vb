Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IDemandaTx
''' </summary>
''' <remarks>TSF-JRC | 12/11/2012</remarks>
<Guid("F1A592D2-B814-497e-BD0C-0828A88F6B3F")> _
Public Interface IDemandaTx

    Function InsertarDemanda(ByVal pEDemanda As String, ByVal pESiniestro As String) As String
    Function ModificarDemanda(ByVal pEDemanda As String, ByVal pESiniestro As String) As Boolean
    Function EliminarDemanda(ByVal pEDemanda As String) As Boolean

    Function InsertarDemandaImplicado(ByVal pEImplicado As String) As Boolean
    Function ModificarDemandaImplicado(ByVal pEImplicado As String) As Boolean
    Function EliminarDemandaImplicado(ByVal pEImplicado As String) As Boolean

End Interface

#End Region


#Region "Interface NO Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IDemandaNTx
''' </summary>
''' <remarks>TSF-JRC | 12/11/2012</remarks>
<Guid("5C911EDB-5EDD-441e-B3A9-928A59F8BABD")> _
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
