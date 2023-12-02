#Region "Clase Transaccional"

Public Class LImpuestoMunicipalTx
    Inherits LUtilitario

    ''' <summary>
    ''' Ingresa nuevo ImpuestoMunicipal
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Listado de Objeto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>    
    Public Function InsertarImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String
        Dim objLImpuestoMunicipalTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalTx")
        Try
            Return objLImpuestoMunicipalTx.InsertarImpuestoMunicipal(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoMunicipalTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Actualiza ImpuestoMunicipal
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Listado de Objeto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>    
    Public Function ModificarImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String
        Dim objLImpuestoMunicipalTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalTx")
        Try
            Return objLImpuestoMunicipalTx.ModificarImpuestoMunicipal(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoMunicipalTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina ImpuestoMunicipal
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Listado de Objeto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>    
    Public Function EliminarImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String
        Dim objLImpuestoMunicipalTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalTx")
        Try
            Return objLImpuestoMunicipalTx.EliminarImpuestoMunicipal(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoMunicipalTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Asignar Lote ImpuestoMunicipal
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Listado de Objeto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/11/2012
    ''' </remarks>    
    Public Function AsignarLoteImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String
        Dim objLImpuestoMunicipalTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalTx")
        Try
            Return objLImpuestoMunicipalTx.AsignarLoteImpuestoMunicipal(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoMunicipalTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Asignar Cheque ImpuestoMunicipal
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Listado de Objeto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/11/2012
    ''' </remarks>    
    Public Function AsignarChequeImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As Boolean
        Dim objLImpuestoMunicipalTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalTx")
        Try
            Return objLImpuestoMunicipalTx.AsignarChequeImpuestoMunicipal(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoMunicipalTx = Nothing
        End Try
    End Function
    ''' <summary>
    ''' Elimina Lote ImpuestoMunicipal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : JJM IBK
    ''' Fecha de Creación  : 13/02/2013
    ''' </remarks>    
    Public Function EliminarLoteImpuestoMunicipal(ByVal pNroLote As String) As String
        Dim objLImpuestoMunicipalTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalTx")
        Try
            Return objLImpuestoMunicipalTx.EliminarLoteImpuestoMunicipal(pNroLote)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoMunicipalTx = Nothing
        End Try
    End Function
   
    
End Class

#End Region


#Region "Clase NO Transaccional"

Public Class LImpuestoMunicipalNTx
    Inherits LUtilitario

    ''' <summary>
    ''' Listado de ImpuestoMunicipal
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function ListadoImpuestoMunicipal(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEImpuestoMunicipal As String) As String

        Dim objLImpuestoMunicipalNTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalNTx")
        Try
            Return objLImpuestoMunicipalNTx.ListadoImpuestoMunicipal(pPageSize, _
                                                                       pCurrentPage, _
                                                                       pSortColumn, _
                                                                       pSortOrder, _
                                                                       pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoMunicipalNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener ImpuestoMunicipal
    ''' </summary>
    ''' <returns>Devuelve un registro de ImpuestoMunicipalCotizacion</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function GetImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String
        Dim objLImpuestoMunicipalNTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalNTx")
        Try
            Return objLImpuestoMunicipalNTx.GetImpuestoMunicipal(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoMunicipalNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Listado de ImpuestoMunicipal Bienes
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function ListadoImpuestoMunicipalBienes(ByVal pPageSize As Integer, _
                                                    ByVal pCurrentPage As Integer, _
                                                    ByVal pSortColumn As String, _
                                                    ByVal pSortOrder As String, _
                                                    ByVal pEImpuestoMunicipal As String) As String

        Dim objLImpuestoMunicipalNTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalNTx")
        Try
            Return objLImpuestoMunicipalNTx.ListadoImpuestoMunicipalBienes(pPageSize, _
                                                                           pCurrentPage, _
                                                                           pSortColumn, _
                                                                           pSortOrder, _
                                                                           pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoMunicipalNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener ImpuestoMunicipal Bienes
    ''' </summary>
    ''' <returns>Devuelve un registro de ImpuestoMunicipalCotizacion</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function GetImpuestoMunicipalBienes(ByVal pEImpuestoMunicipal As String) As String
        Dim objLImpuestoMunicipalNTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalNTx")
        Try
            Return objLImpuestoMunicipalNTx.GetImpuestoMunicipalBienes(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoMunicipalNTx = Nothing
        End Try
    End Function


    Public Function ListarImpuestoMunicipalLiquidar(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pCodLote As String) As String
        Dim objLImpuestoMunicipalNTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalNTX")

        Try
            Return objLImpuestoMunicipalNTx.ListarImpuestoMunicipalLiquidar(pPageSize, _
                                       pCurrentPage, _
                                       pSortColumn, _
                                       pSortOrder, _
                                       pCodLote)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ListarImpuestoMunicipalLiquidarTodo(ByVal pCodLote As String) As String
        Dim objLImpuestoMunicipalNTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalNTX")

        Try
            Return objLImpuestoMunicipalNTx.ListarImpuestoMunicipalLiquidarTodo(pCodLote)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ListarImpuestoMunicipalReporteLiquidar(ByVal pCodigoImpuesto As String) As String
        Dim objLImpuestoMunicipalNTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalNTX")

        Try
            Return objLImpuestoMunicipalNTx.ListarImpuestoMunicipalReporteLiquidar(pCodigoImpuesto)
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    ''' <summary>
    ''' Exportar Listado de ImpuestoMunicipal
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - SCA
    ''' Fecha de Creación  : 28/01/2013
    ''' </remarks>
    Public Function ListadoReporteImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String

        Dim objLImpuestoMunicipalNTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalNTx")
        Try
            Return objLImpuestoMunicipalNTx.ListadoReporteImpuestoMunicipal(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoMunicipalNTx = Nothing
        End Try
    End Function

    'Inicio IBK
    Public Function GetImpuestoMultasInmueble(ByVal pNroLote As String) As String
        Dim objLImpuestoMunicipalNTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalNTx")
        Try
            Return objLImpuestoMunicipalNTx.GetImpuestoMultasInmueble(pNroLote)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoMunicipalNTx = Nothing
        End Try
    End Function
    Public Function GetCodigoPredioBien(ByVal pstrCodContrato As String, _
                                 ByVal strCodBien As String) As String
        Dim objLImpuestoMunicipalNTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalNTx")
        Try
            Return objLImpuestoMunicipalNTx.GetCodigoPredioBien(pstrCodContrato, strCodBien)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoMunicipalNTx = Nothing
        End Try
    End Function
    Public Function ListadoImpuestoMunicipalxLote(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEImpuestoMunicipal As String) As String

        Dim objLImpuestoMunicipalNTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalNTx")
        Try
            Return objLImpuestoMunicipalNTx.ListadoImpuestoMunicipalxLote(pPageSize, _
                                                                       pCurrentPage, _
                                                                       pSortColumn, _
                                                                       pSortOrder, _
                                                                       pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoMunicipalNTx = Nothing
        End Try
    End Function
    Public Function GetImpuestoTotalesInmueble(ByVal pEImpuestoMunicipal As String) As String
        Dim objLImpuestoMunicipalNTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalNTx")
        Try
            Return objLImpuestoMunicipalNTx.GetImpuestoTotalesInmueble(pEImpuestoMunicipal)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoMunicipalNTx = Nothing
        End Try
    End Function
    'Fin IBK
    ''' <summary>
    ''' descuento ImpuestoMunicipal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : JJM IBK
    ''' Fecha de Creación  : 19/02/2013
    ''' </remarks>    
    Public Function DescuentoLoteImpuestoMunicipal(ByVal pNroLote As String, ByVal pUsuarioModificacion As String, ByVal pDescuento As Decimal) As String
        Dim objLImpuestoMunicipalNTx As Object = CreateObject("GCC.Logic.LImpuestoMunicipalNTx")
        Try
            Return objLImpuestoMunicipalNTx.DescuentoLoteImpuestoMunicipal(pNroLote, pUsuarioModificacion, pDescuento)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoMunicipalNTx = Nothing
        End Try
    End Function


End Class

#End Region
