
#Region "Clase Transaccional"

Public Class LTemporalTx

    ''' <summary>
    ''' Ingresa nueva Temporal
    ''' </summary>
    ''' <param name="pETemporal">Listado de Objeto Temporal(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>    
    Public Function fintInsertarTemporal(ByVal pETemporal As String) As Integer
        Dim objLTemporalTx As Object = CreateObject("GCC.Logic.LTemporalTx")

        Try
            Return objLTemporalTx.InsertarTemporal(pETemporal)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modifica Temporal
    ''' </summary>
    ''' <param name="pETemporal">Listado de Objeto Temporal(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function fblnModificarTemporal(ByVal pETemporal As String) As Boolean
        Dim objLTemporalTx As Object = CreateObject("GCC.Logic.LTemporalTx")

        Try
            Return objLTemporalTx.ModificarTemporal(pETemporal)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Elimina un objeto Temporal, identificandolo por su clave primaria.
    ''' </summary>
    ''' <param name="pETemporal">Objeto Temporal(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>    
    Public Function fblnEliminarTemporal(ByVal pETemporal As String) As Boolean
        Dim objLTemporalTx As Object = CreateObject("GCC.Logic.LTemporalTx")

        Try
            Return objLTemporalTx.fblnEliminarTemporal(pETemporal)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

#End Region

#Region "Clase No Transaccional"

Public Class LTemporalNTx

    ''' <summary>
    ''' Obtiene un registro de Temporal
    ''' </summary>
    ''' <returns>String con la entidad ETemporal serializada.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function LeerTemporal(ByVal pETemporal As String) As String
        Dim objLTemporalNTx As Object = CreateObject("GCC.Logic.LTemporalNTx")
        Try
            Return objLTemporalNTx.LeerTemporal(pETemporal)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Listado de Temporal
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoTemporal(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pCodigo As String, _
                                    ByVal pFecha As String, _
                                    ByVal pNumero As String, _
                                    ByVal pDecimales As String, _
                                    ByVal pComentario As String, _
                                    ByVal pTexto As String, _
                                    ByVal pFlag As String) As String
        Dim objLTemporalNTx As Object = CreateObject("GCC.Logic.LTemporalNTx")

        Try
            Return objLTemporalNTx.ListadoTemporal(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pCodigo, _
                                                   pFecha, _
                                                   pNumero, _
                                                   pDecimales, _
                                                   pComentario, _
                                                   pTexto, _
                                                   pFlag)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'IBK - RPH Solo Momentaneo
    Public Function ListarSeguros(ByVal pPageSize As Integer, _
                                  ByVal pCurrentPage As Integer, _
                                  ByVal pSortColumn As String, _
                                  ByVal pSortOrder As String, _
                                  ByVal pESeguros As String) As String

        Dim objLTemporalNTx As Object = CreateObject("GCC.Logic.LTemporalNTx")
        Try

            Return objLTemporalNTx.ListarSeguros(pPageSize, _
                                                 pCurrentPage, _
                                                 pSortColumn, _
                                                 pSortOrder, _
                                                 pESeguros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ListarSegurosDetalle(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pCodigoContrato As String) As String

        Dim objLTemporalNTx As Object = CreateObject("GCC.Logic.LTemporalNTx")
        Try
            Return objLTemporalNTx.ListarSegurosDetalle(pPageSize, _
                                                        pCurrentPage, _
                                                        pSortColumn, _
                                                        pSortOrder, _
                                                        pCodigoContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Fin
End Class

#End Region

