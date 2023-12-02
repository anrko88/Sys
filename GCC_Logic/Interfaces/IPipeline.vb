
Imports System.Runtime.InteropServices
#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IPipelineTX
''' </summary>
''' <remarks>TSF-AEP | 27/08/2012</remarks>
<Guid("90EC38AC-D15C-43e3-9C32-DBFA73A67244")> _
Public Interface IPipelineTX
    Function GrabarPipelineIns(ByVal pEPipeline As String) As Boolean
    Function EliminarPipelineDel(ByVal pCodCotizacion As String) As Boolean
End Interface
#End Region
#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IPipelineTX
''' </summary>
''' <remarks>TSF-AEP | 27/08/2012</remarks>
<Guid("6E397F52-1B1F-4d66-8126-F56CBFBFC493")> _
Public Interface IPipelineNTX
    Function ListarPipeline(ByVal pPageSize As Integer, _
                                          ByVal pCurrentPage As Integer, _
                                          ByVal pSortColumn As String, _
                                          ByVal pSortOrder As String, _
                                          ByVal pEPipeline As String) As String
End Interface
#End Region