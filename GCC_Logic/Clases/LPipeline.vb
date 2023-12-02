Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data
#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LPipelineTX
''' </summary>
''' <remarks>
''' Creado Por         : TSF - AEP
''' Fecha de Creación  : 27/08/2012
''' </remarks>
<Guid("7D63A4EE-3613-45a3-8560-DB475402457C") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LPipelineTX")> _
Public Class LPipelineTX
    Inherits ServicedComponent
    Implements IPipelineTX

    Public Function GrabarPipelineIns(ByVal pEPipeline As String) As Boolean Implements IPipelineTX.GrabarPipelineIns

        Dim objDPipelineTx As DPipelineTX = Nothing
        Dim blnResultado As Boolean

        Try
            objDPipelineTx = New DPipelineTX
            blnResultado = objDPipelineTx.GrabarPipelineIns(pEPipeline)
        Catch ex As Exception
            Throw ex
        Finally
            objDPipelineTx.Dispose()
            objDPipelineTx = Nothing
        End Try

        Return blnResultado

    End Function

    Public Function EliminarPipelineDel(ByVal pCodCotizacion As String) As Boolean Implements IPipelineTX.EliminarPipelineDel
        Dim objDPipelineTx As DPipelineTX = Nothing
        Dim blnResultado As Boolean

        Try
            objDPipelineTx = New DPipelineTX
            blnResultado = objDPipelineTx.EliminarPipelineDel(pCodCotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objDPipelineTx.Dispose()
            objDPipelineTx = Nothing
        End Try

        Return blnResultado

    End Function
End Class
#End Region
#Region "Clase No Transaccional"
''' <summary>
''' Implementación de la clase LDocProveedorNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - AEP
''' Fecha de Creación  : 27/08/2012
''' </remarks>
<Guid("F262DDA5-50AD-4cd1-9B12-675191EFBEC2") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LDocBienNTx")> _
Public Class LPipelineNTX
    Inherits ServicedComponent
    Implements IPipelineNTX
    Public Function ListarPipeline(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pEPipeline As String) As String Implements IPipelineNTX.ListarPipeline
        Dim objDPipelineNTX As DPipelineNTX = Nothing
        Dim strResultado As String
        Try
            objDPipelineNTX = New DPipelineNTX
            strResultado = objDPipelineNTX.ListarPipeline(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pEPipeline)

        Catch ex As Exception
            Throw ex
        Finally
            objDPipelineNTX.Dispose()
        End Try
        Return strResultado
    End Function
End Class
#End Region
