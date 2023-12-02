
#Region "Clase Transaccional"

Public Class LOpcionCompraTx

    ''' <summary>
    ''' Inserta un nuevo registro en la tabla GCC_OpcionCompra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m. 
    ''' </remarks>	
    Public Function fblnInsertarOpcionCompra(ByVal pEGCC_OpcionCompra As String) As Boolean
        Dim objLOpcionCompraTx As Object = CreateObject("GCC.Logic.LOpcionCompraTx")
        Dim boolResultado As Boolean = False
        Try
            boolResultado = objLOpcionCompraTx.fblnInsertarOpcionCompra(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraTx = Nothing
        End Try
        Return boolResultado
    End Function

    ''' <summary>
    ''' Inserta un nuevo registro en la tabla GCC_OpcionCompraEnvio
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 10/01/2013 06:41:54 p.m. 
    ''' </remarks>	
    Public Function fblnInsertarOpcionCompraEnvio(ByVal pEGCC_OpcionCompra As String) As Boolean
        Dim objLOpcionCompraTx As Object = CreateObject("GCC.Logic.LOpcionCompraTx")
        Dim boolResultado As Boolean = False
        Try
            boolResultado = objLOpcionCompraTx.fblnInsertarOpcionCompraEnvio(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraTx = Nothing
        End Try
        Return boolResultado
    End Function

    ''' <summary>
    ''' Actualiza los registros de la tabla SolicitudCreditoEstructuraCarax
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 16/01/2013 06:41:54 p.m. 
    ''' </remarks>	
    Public Function fblnAprobacionBien(ByVal pEGCC_OpcionCompra As String) As Boolean
        Dim objLOpcionCompraTx As Object = CreateObject("GCC.Logic.LOpcionCompraTx")
        Dim boolResultado As Boolean = False
        Try
            boolResultado = objLOpcionCompraTx.fblnAprobacionBien(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraTx = Nothing
        End Try
        Return boolResultado
    End Function


    ''' <summary>
    ''' Modifica un registro existente de la tabla GCC_OpcionCompra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m.
    ''' </remarks>
    Public Function fblnModificarOpcionCompra(ByVal pEGCC_OpcionCompra As String) As Boolean
        Dim objLOpcionCompraTx As Object = CreateObject("GCC.Logic.LOpcionCompraTx")
        Dim boolResultado As Boolean = False
        Try
            boolResultado = objLOpcionCompraTx.fblnModificarOpcionCompra(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraTx = Nothing
        End Try
        Return boolResultado
    End Function

    ''' <summary>
    ''' Modifica un registro existente de la tabla SolicitudCreditoEstructuraCarac
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 09/01/2013 06:41:54 p.m.
    ''' </remarks>
    Public Function fblnModificarBienOpcionCompra(ByVal pEGCC_OpcionCompra As String) As Boolean
        Dim objLOpcionCompraTx As Object = CreateObject("GCC.Logic.LOpcionCompraTx")
        Dim boolResultado As Boolean = False
        Try
            boolResultado = objLOpcionCompraTx.fblnModificarBienOpcionCompra(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraTx = Nothing
        End Try
        Return boolResultado
    End Function

    ''' <summary>
    ''' Eliminar un registro existente de la tabla GCC_OpcionCompra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m.
    ''' </remarks>
    Public Function fblnEliminarOpcionCompra(ByVal pEGCC_OpcionCompra As String) As Boolean
        Dim objLOpcionCompraTx As Object = CreateObject("GCC.Logic.LOpcionCompraTx")
        Dim boolResultado As Boolean = False
        Try
            boolResultado = objLOpcionCompraTx.fblnEliminarOpcionCompra(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraTx = Nothing
        End Try
        Return boolResultado
    End Function

    ''' <summary>
    ''' Inserta un registro en la tabla GCC_OpcionCompraDocumento
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se registro correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 29/01/2013 06:41:54 p.m.
    ''' </remarks>
    Public Function fblnInsertarOpcionCompraDocumento(ByVal pEGCC_OpcionCompra As String) As Boolean
        Dim objLOpcionCompraTx As Object = CreateObject("GCC.Logic.LOpcionCompraTx")
        Dim boolResultado As Boolean = False
        Try
            boolResultado = objLOpcionCompraTx.fblnInsertarOpcionCompraDocumento(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraTx = Nothing
        End Try
        Return boolResultado
    End Function

    ''' <summary>
    ''' Inserta un registro en la tabla GCC_OpcionCompraDocumentoObservacion
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se registro correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 29/01/2013 06:41:54 p.m.
    ''' </remarks>
    Public Function fblnInsertarOpcionCompraDocumentoObservacion(ByVal pEGCC_OpcionCompra As String) As Boolean
        Dim objLOpcionCompraTx As Object = CreateObject("GCC.Logic.LOpcionCompraTx")
        Dim boolResultado As Boolean = False
        Try
            boolResultado = objLOpcionCompraTx.fblnInsertarOpcionCompraDocumentoObservacion(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraTx = Nothing
        End Try
        Return boolResultado
    End Function

End Class


#End Region

#Region "Clase No Transaccional"

Public Class LOpcionCompraNTx

    ''' <summary>
    ''' Obtiene los valores de un registro de la tabla GCC_OpcionCompra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjLeerOpcionCompra(ByVal pEGCC_OpcionCompra As String) As String
        Dim objLOpcionCompraNTx As Object = CreateObject("GCC.Logic.LOpcionCompraNTx")
        Dim strResultado As String = String.Empty

        Try
            strResultado = objLOpcionCompraNTx.fobjLeerOpcionCompra(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraNTx = Nothing
        End Try
        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene los valores de un registro de la vista UV_GCC_BIENOPCIOMCOMPRA_SEL
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 08/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjLeerBienOpcionCompra(ByVal pEGCC_OpcionCompra As String) As String
        Dim objLOpcionCompraNTx As Object = CreateObject("GCC.Logic.LOpcionCompraNTx")
        Dim strResultado As String = String.Empty

        Try
            strResultado = objLOpcionCompraNTx.fobjLeerBienOpcionCompra(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraNTx = Nothing
        End Try
        Return strResultado
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de página</param>
    ''' <param name="pCurrentPage">Página actual</param>
    ''' <param name="pSortColumn">Ordenar por columna</param>
    ''' <param name="pSortOrder">Dirección de ordenamiento</param>    
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoOpcionCompra(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEGCC_OpcionCompra As String) As String
        Dim objLOpcionCompraNTx As Object = CreateObject("GCC.Logic.LOpcionCompraNTx")
        Dim strResultado As String = String.Empty

        Try

            strResultado = objLOpcionCompraNTx.fobjListadoOpcionCompra(pPageSize, _
                                                                       pCurrentPage, _
                                                                       pSortColumn, _
                                                                       pSortOrder, _
                                                                       pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraNTx = Nothing
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoOpcionCompraTodo(ByVal pEGCC_OpcionCompra As String) As String
        Dim objLOpcionCompraNTx As Object = CreateObject("GCC.Logic.LOpcionCompraNTx")
        Dim strResultado As String = String.Empty

        Try

            strResultado = objLOpcionCompraNTx.fobjListadoOpcionCompraTodo(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraNTx = Nothing
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoOpcionCompraEnvio(ByVal pEGCC_OpcionCompra As String) As String
        Dim objLOpcionCompraNTx As Object = CreateObject("GCC.Logic.LOpcionCompraNTx")
        Dim strResultado As String = String.Empty

        Try

            strResultado = objLOpcionCompraNTx.fobjListadoOpcionCompraEnvio(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraNTx = Nothing
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de página</param>
    ''' <param name="pCurrentPage">Página actual</param>
    ''' <param name="pSortColumn">Ordenar por columna</param>
    ''' <param name="pSortOrder">Dirección de ordenamiento</param>    
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 08/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoBienOpcionCompra(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEGCC_OpcionCompra As String) As String
        Dim objLOpcionCompraNTx As Object = CreateObject("GCC.Logic.LOpcionCompraNTx")
        Dim strResultado As String = String.Empty

        Try

            strResultado = objLOpcionCompraNTx.fobjListadoBienOpcionCompra(pPageSize, _
                                                                           pCurrentPage, _
                                                                           pSortColumn, _
                                                                           pSortOrder, _
                                                                           pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraNTx = Nothing
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <param name="pFechaTermino">Fecha de término de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 16/01/2013 03:50:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoBienOpcionCompraReporte(ByVal pFechaInicio As String, _
                                                        ByVal pFechaTermino As String) As String
        Dim objLOpcionCompraNTx As Object = CreateObject("GCC.Logic.LOpcionCompraNTx")
        Dim strResultado As String = String.Empty

        Try

            strResultado = objLOpcionCompraNTx.fobjListadoBienOpcionCompraReporte(pFechaInicio, _
                                                                           pFechaTermino)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraNTx = Nothing
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Listado de Opción de Compra Reporte
    ''' </summary>
    ''' <param name="pdFechaActivacionIni">Fecha de Activación Inicial</param>
    ''' <param name="pdFechaActivacionFin">Fecha de Activación Final</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 28/01/2013
    ''' </remarks>
    Public Function ListadoReporteOpcionCompra(ByVal pdFechaActivacionIni As DateTime, _
                                               ByVal pdFechaActivacionFin As DateTime) As String
        Dim objLOpcionCompraNTx As Object = CreateObject("GCC.Logic.LOpcionCompraNTx")
        Dim strResultado As String = String.Empty
        Try
            strResultado = objLOpcionCompraNTx.ListadoReporteOpcionCompra(pdFechaActivacionIni, _
                                                              pdFechaActivacionFin)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraNTx = Nothing
        End Try

        Return strResultado
    End Function


    ''' <summary>
    ''' Listado de Documento de Opción de Compra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>  
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 29/01/2013
    ''' </remarks>
    Public Function ListadoOpcionCompraDocumento(ByVal pEGCC_OpcionCompra As String) As String
        Dim objLOpcionCompraNTx As Object = CreateObject("GCC.Logic.LOpcionCompraNTx")
        Dim strResultado As String = String.Empty
        Try
            strResultado = objLOpcionCompraNTx.ListadoOpcionCompraDocumento(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraNTx = Nothing
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Listado de Observaciones de Documento de Opción de Compra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>  
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 29/01/2013
    ''' </remarks>
    Public Function ListadoOpcionCompraDocumentoObservacion(ByVal pEGCC_OpcionCompra As String) As String
        Dim objLOpcionCompraNTx As Object = CreateObject("GCC.Logic.LOpcionCompraNTx")
        Dim strResultado As String = String.Empty
        Try
            strResultado = objLOpcionCompraNTx.ListadoOpcionCompraDocumentoObservacion(pEGCC_OpcionCompra)
        Catch ex As Exception
            Throw ex
        Finally
            objLOpcionCompraNTx = Nothing
        End Try

        Return strResultado
    End Function
End Class

#End Region

