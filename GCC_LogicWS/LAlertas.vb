#Region "Clase Transaccional"
Public Class LAlertasTX
    ''' <summary>
    '''Grabar Pipeline
    ''' </summary>
    ''' <param name="pEPipeline">Entidad Serializada</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 28/08/2012
    ''' </remarks>
    Public Function fInsertarAlertas(ByVal pEAlertas As String) As Boolean
        Dim objLAlertasNTX As Object = CreateObject("GCC.Logic.LAlertasTX")

        Try
            Return objLAlertasNTX.fInsertarAlertas(pEAlertas)
        Catch ex As Exception
            Throw ex
        Finally
            objLAlertasNTX.Dispose()
            objLAlertasNTX = Nothing
        End Try

    End Function
    
End Class
#End Region
#Region "Clase No Transaccional"
Public Class LAlertasNTX
    
End Class
#End Region


