#Region "Clase Transaccional"

Public Class LSiniestroTx
    Inherits LUtilitario

    ''' <summary>
    ''' Ingresa nuevo Siniestro
    ''' </summary>
    ''' <param name="pESiniestro">Listado de Objeto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>    
    Public Function InsertarSiniestro(ByVal pESiniestro As String) As String
        Dim objLSiniestroTx As Object = CreateObject("GCC.Logic.LSiniestroTx")
        Try
            Return objLSiniestroTx.InsertarSiniestro(pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objLSiniestroTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Actualiza Siniestro
    ''' </summary>
    ''' <param name="pESiniestro">Listado de Objeto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>    
    Public Function ModificarSiniestro(ByVal pESiniestro As String) As String
        Dim objLSiniestroTx As Object = CreateObject("GCC.Logic.LSiniestroTx")
        Try
            Return objLSiniestroTx.ModificarSiniestro(pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objLSiniestroTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina Siniestro
    ''' </summary>
    ''' <param name="pESiniestro">Listado de Objeto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>    
    Public Function EliminarSiniestro(ByVal pESiniestro As String) As String
        Dim objLSiniestroTx As Object = CreateObject("GCC.Logic.LSiniestroTx")
        Try
            Return objLSiniestroTx.EliminarSiniestro(pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objLSiniestroTx = Nothing
        End Try
    End Function

End Class

#End Region


#Region "Clase NO Transaccional"

Public Class LSiniestroNTx
    Inherits LUtilitario

    ''' <summary>
    ''' Listado de SiniestroContrato
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function ListadoSiniestroContrato(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pESiniestro As String) As String

        Dim objLSiniestroNTx As Object = CreateObject("GCC.Logic.LSiniestroNTx")
        Try
            Return objLSiniestroNTx.ListadoSiniestroContrato(pPageSize, _
                                                               pCurrentPage, _
                                                               pSortColumn, _
                                                               pSortOrder, _
                                                               pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objLSiniestroNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener SiniestroContrato
    ''' </summary>
    ''' <returns>Devuelve un registro de SiniestroCotizacion</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function GetSiniestroContrato(ByVal pESiniestro As String) As String
        Dim objLSiniestroNTx As Object = CreateObject("GCC.Logic.LSiniestroNTx")
        Try
            Return objLSiniestroNTx.GetSiniestroContrato(pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objLSiniestroNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Listado de Siniestro
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function ListadoSiniestro(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pESiniestro As String) As String

        Dim objLSiniestroNTx As Object = CreateObject("GCC.Logic.LSiniestroNTx")
        Try
            Return objLSiniestroNTx.ListadoSiniestro(pPageSize, _
                                                       pCurrentPage, _
                                                       pSortColumn, _
                                                       pSortOrder, _
                                                       pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objLSiniestroNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener Siniestro
    ''' </summary>
    ''' <returns>Devuelve un registro de Siniestro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function GetSiniestro(ByVal pESiniestro As String) As String
        Dim objLSiniestroNTx As Object = CreateObject("GCC.Logic.LSiniestroNTx")
        Try
            Return objLSiniestroNTx.GetSiniestro(pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objLSiniestroNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Listado de Siniestro para exportar
    ''' </summary>
    ''' <param name="pESiniestro">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 21/01/2013
    ''' </remarks>
    Function ListadoReporteSiniestro(ByVal pESiniestro As String) As String
        Dim objLSiniestroNTx As Object = CreateObject("GCC.Logic.LSiniestroNTx")
        Try
            Return objLSiniestroNTx.ListadoReporteSiniestro(pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objLSiniestroNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el Siniestro
    ''' </summary>
    ''' <param name="pESiniestro">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 18/01/2013
    ''' </remarks>
    Function GetSiniestroConsulta(ByVal pESiniestro As String) As String
        Dim objLSiniestroNTx As Object = CreateObject("GCC.Logic.LSiniestroNTx")
        Try
            Return objLSiniestroNTx.GetSiniestroConsulta(pESiniestro)
        Catch ex As Exception
            Throw ex
        Finally
            objLSiniestroNTx = Nothing
        End Try
    End Function

End Class

#End Region
