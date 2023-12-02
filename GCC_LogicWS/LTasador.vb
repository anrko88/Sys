#Region "Clase Transaccional"

Public Class LTasadorTx

    ''' <summary>
    ''' InsertarTasador
    ''' </summary>
    ''' <param name="pEGCC_ContratoTasador"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertarTasador(ByVal pEGCC_ContratoTasador As String) As String
        Dim objLTasadorTx As Object = CreateObject("GCC.Logic.LTasadorTx")
        Try
            Return objLTasadorTx.InsertarTasador(pEGCC_ContratoTasador)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ActualizaTasador(ByVal pEGCC_ContratoTasador As String) As String
        Dim objLTasadorTx As Object = CreateObject("GCC.Logic.LTasadorTx")
        Try
            Return objLTasadorTx.ActualizaTasador(pEGCC_ContratoTasador)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function EnviarCarta(ByVal pEGCC_ContratoTasador As String) As String
        Dim objLTasadorTx As Object = CreateObject("GCC.Logic.LTasadorTx")
        Try
            Return objLTasadorTx.EnviarCarta(pEGCC_ContratoTasador)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ActualizarTasacion(ByVal pEGCC_ContratoTasador As String) As String
        Dim objLTasadorTx As Object = CreateObject("GCC.Logic.LTasadorTx")
        Try
            Return objLTasadorTx.ActualizarTasacion(pEGCC_ContratoTasador)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

#End Region

#Region "Clase No Transaccional"

Public Class LTasadorNTx

    ''' <summary>
    ''' Obtiene Datos de Contrato para Tasacion
    ''' </summary>
    ''' <param name="pstrNroContrato">Numero de Contrato para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 28/01/2013
    ''' </remarks>
    Public Function ObtenerContratoTasacion(ByVal pstrNroContrato As String) As String
        Dim objLTasadorNTx As Object = CreateObject("GCC.Logic.LTasadorNTx")
        Try
            Return objLTasadorNTx.ObtenerContratoTasacion(pstrNroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objLTasadorNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pCodSolicitudcredito"></param>
    ''' <param name="pCuCliente"></param>
    ''' <param name="pRazonsolcial"></param>
    ''' <param name="pTipoDocumento"></param>
    ''' <param name="pNumerodocumento"></param>
    ''' <param name="pEstadoTasacion"></param>
    ''' <param name="pClasificacionBien"></param>
    ''' <param name="pBanca"></param>
    ''' <param name="pEjecutivoBanca"></param>
    ''' <param name="pPeriodo"></param>
    ''' <param name="pFechadesde"></param>
    ''' <param name="pFechaHasta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ContratoTasadorSel(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pCodSolicitudcredito As String, _
                                        ByVal pCuCliente As String, _
                                        ByVal pRazonsolcial As String, _
                                        ByVal pTipoDocumento As String, _
                                        ByVal pNumerodocumento As String, _
                                        ByVal pEstadoTasacion As String, _
                                        ByVal pClasificacionBien As String, _
                                        ByVal pBanca As String, _
                                        ByVal pEjecutivoBanca As String, _
                                        ByVal pPeriodo As String, _
                                        ByVal pFechadesde As String, _
                                        ByVal pFechaHasta As String, _
                                        ByVal pEstadoTasacionContrato As String) As String
        Dim objLTasadorNTx As Object = CreateObject("GCC.Logic.LTasadorNTx")
        Try
            Return objLTasadorNTx.ListadoContratoTasador(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pCodSolicitudcredito, pCuCliente, pRazonsolcial, pTipoDocumento, pNumerodocumento, pEstadoTasacion, pClasificacionBien, pBanca, pEjecutivoBanca, pPeriodo, pFechadesde, pFechaHasta, pEstadoTasacionContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objLTasadorNTx.Dispose()
            objLTasadorNTx = Nothing
        End Try




    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pstrNroContrato"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ObtenerContratoCotizacionSaldoFinanciado(ByVal pstrNroContrato As String) As String
        Dim oLDocClienteNTx As Object = CreateObject("GCC.Logic.LTasadorNTx")
        Try
            Return oLDocClienteNTx.ObtenerContratoCotizacionSaldoFinanciado(pstrNroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            oLDocClienteNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de la vista GCC_ContratoTasacion
    ''' </summary>
    ''' <param name="pCodSolicitudcredito">Código de Contrato</param>    
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function ListaContratoBienTasacion(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal pCodSolicitudcredito As String) As String

        Dim objLTasadorNTx As Object = CreateObject("GCC.Logic.LTasadorNTx")
        Try
            Return objLTasadorNTx.ListaContratoBienTasacion(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pCodSolicitudcredito)
        Catch ex As Exception
            Throw ex
        Finally
            objLTasadorNTx.Dispose()
            objLTasadorNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de la vista UV_GCC_TASACIONBIENCONTRATO
    ''' </summary>
    ''' <param name="pCodSolicitudcredito">Código de Contrato</param>    
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 18/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function ListaBienTasacion(ByVal pPageSize As Integer, _
                                      ByVal pCurrentPage As Integer, _
                                      ByVal pSortColumn As String, _
                                      ByVal pSortOrder As String, _
                                      ByVal pCodSolicitudcredito As String) As String

        Dim objLTasadorNTx As Object = CreateObject("GCC.Logic.LTasadorNTx")
        Try
            Return objLTasadorNTx.ListaBienTasacion(pPageSize, _
                                                    pCurrentPage, _
                                                    pSortColumn, _
                                                    pSortOrder, _
                                                    pCodSolicitudcredito)
        Catch ex As Exception
            Throw ex
        Finally
            objLTasadorNTx.Dispose()
            objLTasadorNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de la vista UV_GCC_HISTORICOTASACIONBIENCONTRATO
    ''' </summary>
    ''' <param name="pCodSolicitudcredito">Código de Contrato</param>
    ''' <param name="pCodContratoTasacion">Código de Contrato Tasación</param>
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function ListaHistoricoContratoBienTasacion(ByVal pPageSize As Integer, _
                                                      ByVal pCurrentPage As Integer, _
                                                      ByVal pSortColumn As String, _
                                                      ByVal pSortOrder As String, _
                                                      ByVal pCodSolicitudcredito As String, _
                                                      ByVal pCodContratoTasacion As Short) As String

        Dim objLTasadorNTx As Object = CreateObject("GCC.Logic.LTasadorNTx")
        Try
            Return objLTasadorNTx.ListaHistoricoContratoBienTasacion(pPageSize, _
                                                                    pCurrentPage, _
                                                                    pSortColumn, _
                                                                    pSortOrder, _
                                                                    pCodSolicitudcredito, _
                                                                    pCodContratoTasacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLTasadorNTx.Dispose()
            objLTasadorNTx = Nothing
        End Try
    End Function

    Public Function calculatotales(ByVal pCodSolicitudcredito As String) As String

        Dim objLTasadorNTx As Object = CreateObject("GCC.Logic.LTasadorNTx")
        Try
            Return objLTasadorNTx.calculatotales(pCodSolicitudcredito)
        Catch ex As Exception
            Throw ex
        Finally
            objLTasadorNTx.Dispose()
            objLTasadorNTx = Nothing
        End Try
    End Function

End Class

#End Region


