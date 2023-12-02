#Region "Clase Transaccional"
Public Class LPipelineTX
    ''' <summary>
    '''Grabar Pipeline
    ''' </summary>
    ''' <param name="pEPipeline">Entidad Serializada</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 28/08/2012
    ''' </remarks>
    Public Function GrabarPipeline(ByVal pEPipeline As String) As Boolean
        Dim objLPipelineNTX As Object = CreateObject("GCC.Logic.LPipelineTX")

        Try
            Return objLPipelineNTX.GrabarPipelineIns(pEPipeline)
        Catch ex As Exception
            Throw ex
        Finally
            objLPipelineNTX.Dispose()
            objLPipelineNTX = Nothing
        End Try

    End Function
    ''' <summary>
    '''Eliminar Pipeline
    ''' </summary>
    ''' <param name="pEPipeline">Entidad Serializada</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 29/08/2012
    ''' </remarks>
    Public Function EliminarPipeline(ByVal pCodCotizacion As String) As Boolean
        Dim objLPipelineNTX As Object = CreateObject("GCC.Logic.LPipelineTX")

        Try
            Return objLPipelineNTX.EliminarPipelineDel(pCodCotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLPipelineNTX.Dispose()
            objLPipelineNTX = Nothing
        End Try

    End Function
End Class
#End Region
#Region "Clase No Transaccional"
Public Class LPipelineNTX
    ''' <summary>
    ''' Verifica si el representante seleccionado se encuentra asociado con algún contrato diferente al enviado por parámetro.
    ''' </summary>
    ''' <param name="pESolicitudCredito"></param>
    ''' <param name="pEGccRepresentante"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ListarPipeline(ByVal pPageSize As Integer, _
                                       ByVal pCurrentPage As Integer, _
                                       ByVal pSortColumn As String, _
                                       ByVal pSortOrder As String, _
                                       ByVal pEPipeline As String) As String
        Dim objLPipelineNTX As Object = CreateObject("GCC.Logic.LPipelineNTX")

        Try
            Return objLPipelineNTX.ListarPipeline(pPageSize, _
                                       pCurrentPage, _
                                       pSortColumn, _
                                       pSortOrder, _
                                       pEPipeline)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
End Class
#End Region

