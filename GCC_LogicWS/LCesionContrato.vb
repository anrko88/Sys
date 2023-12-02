#Region "Clase Transaccional"

Public Class LCesionContratoTx
    Inherits LUtilitario

    ''' <summary>
    ''' Actualiza CesionContrato
    ''' </summary>
    ''' <param name="pECesionContrato">Listado de Objeto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 11/01/2013
    ''' </remarks>    
    Public Function ModificarCesionContrato(ByVal pECesionContrato As String) As String
        Dim objLCesionContratoTx As Object = CreateObject("GCC.Logic.LCesionContratoTx")
        Try
            Return objLCesionContratoTx.ModificarCesionContrato(pECesionContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objLCesionContratoTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Realiza CesionContrato
    ''' </summary>
    ''' <param name="pECesionContrato">Listado de Objeto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 18/01/2013
    ''' </remarks>    
    Public Function RealizarCesionContrato(ByVal pECesionContrato As String) As String
        Dim objLCesionContratoTx As Object = CreateObject("GCC.Logic.LCesionContratoTx")
        Try
            Return objLCesionContratoTx.RealizarCesionContrato(pECesionContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objLCesionContratoTx = Nothing
        End Try
    End Function

End Class

#End Region


#Region "Clase NO Transaccional"

Public Class LCesionContratoNTx
    Inherits LUtilitario

    ''' <summary>
    ''' Listado de CesionContrato
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 11/01/2013
    ''' </remarks>
    Public Function ListadoCesionContrato(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pECesionContrato As String) As String

        Dim objLCesionContratoNTx As Object = CreateObject("GCC.Logic.LCesionContratoNTx")
        Try
            Return objLCesionContratoNTx.ListadoCesionContrato(pPageSize, _
                                                       pCurrentPage, _
                                                       pSortColumn, _
                                                       pSortOrder, _
                                                       pECesionContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objLCesionContratoNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener CesionContrato
    ''' </summary>
    ''' <returns>Devuelve un registro de CesionContrato</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 11/01/2013
    ''' </remarks>
    Public Function GetCesionContrato(ByVal pECesionContrato As String) As String
        Dim objLCesionContratoNTx As Object = CreateObject("GCC.Logic.LCesionContratoNTx")
        Try
            Return objLCesionContratoNTx.GetCesionContrato(pECesionContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objLCesionContratoNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <param name="pFechaTermino">Fecha de término de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 17/01/2013 11:32:54 p.m. 
    ''' </remarks>
    Public Function ListadoCesionContratoReporte(ByVal pFechaInicio As String, _
                                    ByVal pFechaTermino As String) As String

        Dim objLCesionContratoNTx As Object = CreateObject("GCC.Logic.LCesionContratoNTx")
        Try
            Return objLCesionContratoNTx.ListadoCesionContratoReporte(pFechaInicio, _
                                                       pFechaTermino)
        Catch ex As Exception
            Throw ex
        Finally
            objLCesionContratoNTx = Nothing
        End Try
    End Function
End Class

#End Region
