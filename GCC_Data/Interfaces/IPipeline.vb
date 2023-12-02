Imports System.Runtime.InteropServices
#Region "Interface Transaccional"
''' <summary>
''' Interfaz de métodos para la clase IPipelineTX
''' </summary>
''' <remarks>
''' Creado Por         : TSF - AEP
''' Fecha de Creación  : 27/08/2012
''' </remarks>
<Guid("AF29EE7B-22BD-4b6f-A07A-5D03C85C1019")> _
Public Interface IPipelineTX
    Function GrabarPipelineIns(ByVal pEPipelien As String) As Boolean
    Function EliminarPipelineDel(ByVal pCodCotizacion As String) As Boolean
End Interface
#End Region

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase IPipelineNTX
''' </summary>
''' <remarks>
''' Creado Por         : TSF - AEP
''' Fecha de Creación  : 27/08/2012
''' </remarks>
<Guid("EB709143-B76B-41be-A611-EC9E89EBDAB3")> _
Public Interface IPipelineNTX
    Function ListarPipeline(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEPipeline As String) As String



End Interface
#End Region

