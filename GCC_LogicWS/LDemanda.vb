#Region "Clase Transaccional"

Public Class LDemandaTx
    Inherits LUtilitario

    ''' <summary>
    ''' Ingresa nuevo Demanda
    ''' </summary>
    ''' <param name="pEDemanda">Listado de Objeto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>    
    Public Function InsertarDemanda(ByVal pEDemanda As String, ByVal pESiniestro As String) As String
        Dim objLDemandaTx As Object = CreateObject("GCC.Logic.LDemandaTx")
        Try
            Return objLDemandaTx.InsertarDemanda(pEDemanda, pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objLDemandaTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Actualiza Demanda
    ''' </summary>
    ''' <param name="pEDemanda">Listado de Objeto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>    
    Public Function ModificarDemanda(ByVal pEDemanda As String, ByVal pESiniestro As String) As String
        Dim objLDemandaTx As Object = CreateObject("GCC.Logic.LDemandaTx")
        Try
            Return objLDemandaTx.ModificarDemanda(pEDemanda, pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objLDemandaTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina Demanda
    ''' </summary>
    ''' <param name="pEDemanda">Listado de Objeto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>    
    Public Function EliminarDemanda(ByVal pEDemanda As String) As String
        Dim objLDemandaTx As Object = CreateObject("GCC.Logic.LDemandaTx")
        Try
            Return objLDemandaTx.EliminarDemanda(pEDemanda)
        Catch ex As Exception
            Throw ex
        Finally
            objLDemandaTx = Nothing
        End Try
    End Function

#Region "Implicado"

    ''' <summary>
    ''' Ingresa nuevo Implicado
    ''' </summary>
    ''' <param name="pEImplicado">Listado de Objeto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/11/2012
    ''' </remarks>    
    Public Function InsertarDemandaImplicado(ByVal pEImplicado As String) As String
        Dim objLDemandaTx As Object = CreateObject("GCC.Logic.LDemandaTx")
        Try
            Return objLDemandaTx.InsertarDemandaImplicado(pEImplicado)
        Catch ex As Exception
            Throw ex
        Finally
            objLDemandaTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Actualiza Implicado
    ''' </summary>
    ''' <param name="pEImplicado">Listado de Objeto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/11/2012
    ''' </remarks>    
    Public Function ModificarDemandaImplicado(ByVal pEImplicado As String) As String
        Dim objLDemandaTx As Object = CreateObject("GCC.Logic.LDemandaTx")
        Try
            Return objLDemandaTx.ModificarDemandaImplicado(pEImplicado)
        Catch ex As Exception
            Throw ex
        Finally
            objLDemandaTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina Implicado
    ''' </summary>
    ''' <param name="pEImplicado">Listado de Objeto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/11/2012
    ''' </remarks>    
    Public Function EliminarDemandaImplicado(ByVal pEImplicado As String) As String
        Dim objLDemandaTx As Object = CreateObject("GCC.Logic.LDemandaTx")
        Try
            Return objLDemandaTx.EliminarDemandaImplicado(pEImplicado)
        Catch ex As Exception
            Throw ex
        Finally
            objLDemandaTx = Nothing
        End Try
    End Function

#End Region

End Class

#End Region


#Region "Clase NO Transaccional"

Public Class LDemandaNTx
    Inherits LUtilitario

    ''' <summary>
    ''' Listado de DemandaContrato
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function ListadoDemandaContrato(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEDemanda As String) As String

        Dim objLDemandaNTx As Object = CreateObject("GCC.Logic.LDemandaNTx")
        Try
            Return objLDemandaNTx.ListadoDemandaContrato(pPageSize, _
                                                               pCurrentPage, _
                                                               pSortColumn, _
                                                               pSortOrder, _
                                                               pEDemanda)
        Catch ex As Exception
            Throw ex
        Finally
            objLDemandaNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener DemandaContrato
    ''' </summary>
    ''' <returns>Devuelve un registro de DemandaCotizacion</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function GetDemandaContrato(ByVal pEDemanda As String) As String
        Dim objLDemandaNTx As Object = CreateObject("GCC.Logic.LDemandaNTx")
        Try
            Return objLDemandaNTx.GetDemandaContrato(pEDemanda)
        Catch ex As Exception
            Throw ex
        Finally
            objLDemandaNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Listado de Demanda
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function ListadoDemanda(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pEDemanda As String) As String

        Dim objLDemandaNTx As Object = CreateObject("GCC.Logic.LDemandaNTx")
        Try
            Return objLDemandaNTx.ListadoDemanda(pPageSize, _
                                                       pCurrentPage, _
                                                       pSortColumn, _
                                                       pSortOrder, _
                                                       pEDemanda)
        Catch ex As Exception
            Throw ex
        Finally
            objLDemandaNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener Demanda
    ''' </summary>
    ''' <returns>Devuelve un registro de Demanda</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function GetDemanda(ByVal pEDemanda As String) As String
        Dim objLDemandaNTx As Object = CreateObject("GCC.Logic.LDemandaNTx")
        Try
            Return objLDemandaNTx.GetDemanda(pEDemanda)
        Catch ex As Exception
            Throw ex
        Finally
            objLDemandaNTx = Nothing
        End Try
    End Function

#Region "Implicado"

    ''' <summary>
    ''' Listado de Implicados
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/11/2012
    ''' </remarks>
    Public Function ListadoDemandaImplicado(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEImplicado As String) As String

        Dim objLDemandaNTx As Object = CreateObject("GCC.Logic.LDemandaNTx")
        Try
            Return objLDemandaNTx.ListadoDemandaImplicado(pPageSize, _
                                                       pCurrentPage, _
                                                       pSortColumn, _
                                                       pSortOrder, _
                                                       pEImplicado)
        Catch ex As Exception
            Throw ex
        Finally
            objLDemandaNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener Implicado
    ''' </summary>
    ''' <returns>Devuelve un registro de Demanda</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/11/2012
    ''' </remarks>
    Public Function GetDemandaImplicado(ByVal pEImplicado As String) As String
        Dim objLDemandaNTx As Object = CreateObject("GCC.Logic.LDemandaNTx")
        Try
            Return objLDemandaNTx.GetDemandaImplicado(pEImplicado)
        Catch ex As Exception
            Throw ex
        Finally
            objLDemandaNTx = Nothing
        End Try
    End Function

#End Region

End Class

#End Region
