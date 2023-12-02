
#Region "Clase Transaccional"
Public Class LImpuestoVehicularTX
    'Inicio IBK - AAE - Cambio que retorne un string
    Public Function GrabarImpuestoVehicular(ByVal pEImpuestoVehicular As String) As String
        Dim objLImpuestoVehicularTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularTX")
        Try
            Return objLImpuestoVehicularTX.GrabarImpuestoVehicular(pEImpuestoVehicular)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AsignarLoteImpuestoVehicular(ByVal pEImpuestoVehicular As String) As String
        Dim objLImpuestoVehicularTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularTX")
        Try
            Return objLImpuestoVehicularTX.AsignarLoteImpuestoVehicular(pEImpuestoVehicular)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Inicio IBK - AAE - Cambio que retorne un string
    Public Function ModificarImpuestoVehicular(ByVal pEImpuestoVehicular As String) As String
        Dim objLImpuestoVehicularTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularTX")
        Try
            Return objLImpuestoVehicularTX.ModificarImpuestoVehicular(pEImpuestoVehicular)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Inicio IBK - AAE - Agrego parametro
    Public Function EliminarImpuestoVehicular(ByVal pIntCodigoImpuesto As String, ByVal pStrCodigoUsuario As String, ByVal pstrNroLote As String) As Boolean
        Dim objLImpuestoVehicularTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularTX")
        Try
            Return objLImpuestoVehicularTX.EliminarImpuestoVehicular(pIntCodigoImpuesto, pStrCodigoUsuario, pstrNroLote)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AsignarChequeImpuestoVehicular(ByVal pEImpuestoVehicular As String) As Boolean
        Dim objLImpuestoVehicularTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularTX")
        Try
            Return objLImpuestoVehicularTX.AsignarChequeImpuestoVehicular(pEImpuestoVehicular)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Inicio IBK - AAE - Cambio que retorne un string
    Public Function GrabarMultaVehicular(ByVal pEMultaVehicular As String) As String
        Dim objLImpuestoVehicularTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularTX")
        Try
            Return objLImpuestoVehicularTX.GrabarMultaVehicular(pEMultaVehicular)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Inicio IBK - AAE - Cambio que retorne un string
    Public Function ModificarMultaVehicular(ByVal pEMultaVehicular As String) As String
        Dim objLImpuestoVehicularTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularTX")
        Try
            Return objLImpuestoVehicularTX.ModificarMultaVehicular(pEMultaVehicular)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function EliminarMultaVehicular(ByVal pIntCodigoMulta As String, ByVal pStrCodigoUsuario As String, ByVal pstrNroLote As String) As Boolean
        Dim objLImpuestoVehicularTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularTX")
        Try
            Return objLImpuestoVehicularTX.EliminarMultaVehicular(pIntCodigoMulta, pStrCodigoUsuario, pstrNroLote)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function EliminarLote(ByVal pLote As String) As Boolean
        Dim objLImpuestoVehicularTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularTX")
        Try
            Return objLImpuestoVehicularTX.EliminarLote(pLote)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AsignarLoteMultaVehicular(ByVal pEImpuestoVehicular As String) As String
        Dim objLImpuestoVehicularTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularTX")
        Try
            Return objLImpuestoVehicularTX.AsignarLoteMultaVehicular(pEImpuestoVehicular)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AsignarChequeMultaVehicular(ByVal pEImpuestoVehicular As String) As Boolean
        Dim objLImpuestoVehicularTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularTX")
        Try
            Return objLImpuestoVehicularTX.AsignarChequeMultaVehicular(pEImpuestoVehicular)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Inicio IBK - AAE - Agrego nueva operación
    Public Function AsignarLoteImpuestoVehicular2(ByVal pEImpuestoVehicular As String) As String
        Dim objLImpuestoVehicularTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularTX")
        Try
            Return objLImpuestoVehicularTX.AsignarLoteImpuestoVehicular2(pEImpuestoVehicular)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ReGenerarLote(ByVal strLote As String) As String
        Dim objLImpuestoVehicularTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularTX")
        Try
            Return objLImpuestoVehicularTX.ReGenerarLote(strLote)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AnularLote(ByVal pLote As String) As String
        Dim objLImpuestoVehicularTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularTX")
        Try
            Return objLImpuestoVehicularTX.AnularLote(pLote)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Fin IBK - AAE
    Public Function LiquidarLote(ByVal pUsuarioModificacion As String, ByVal pNroLote As String, ByVal pCodigoConcepto As String) As String
        Dim objLImpuestoVehicularTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")
        Try
            Return objLImpuestoVehicularTX.LiquidarLote(pUsuarioModificacion, pNroLote, pCodigoConcepto)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
#End Region
#Region "Clase No Transaccional"

Public Class LImpuestoVehicularNTX

    Public Function ListarImpuestoVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pEImpuestoVehicular As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ListarImpuestoVehicular(pPageSize, _
                                       pCurrentPage, _
                                       pSortColumn, _
                                       pSortOrder, _
                                       pEImpuestoVehicular)
        Catch ex As Exception
            Throw ex
        End Try

    End Function



    Public Function ListarBienImpuestoVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pPlaca As String, _
                                   ByVal pTipo As String, _
                                           ByVal pNroMotor As String, _
                                           ByVal pCUCliente As String, _
                                           ByVal pCodContrato As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ListarBienImpuestoVehicular(pPageSize, _
                                                                           pCurrentPage, _
                                                                           pSortColumn, _
                                                                           pSortOrder, _
                                                                           pPlaca, pTipo, _
                                                                           pNroMotor, pCUCliente, pCodContrato)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ListarLoteImpuestoVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pPlaca As String, _
                                   ByVal pTipo As Integer, _
                                   ByVal pNroLote As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ListarLoteImpuestoVehicular(pPageSize, _
                                       pCurrentPage, _
                                       pSortColumn, _
                                       pSortOrder, _
                                       pPlaca, pTipo, pNroLote)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'GCCTS_AEP_20130212 - Se creo un metodo nuevo 
    Public Function ListarLoteImpuestoVehicularConsulta(ByVal pPageSize As Integer, _
                                  ByVal pCurrentPage As Integer, _
                                  ByVal pSortColumn As String, _
                                  ByVal pSortOrder As String, _
                                  ByVal pPlaca As String, _
                                  ByVal pTipo As Integer) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ListarLoteImpuestoVehicularConsulta(pPageSize, _
                                       pCurrentPage, _
                                       pSortColumn, _
                                       pSortOrder, _
                                       pPlaca, pTipo)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ObtenerDatosImpuesto(ByVal pstrPlaca As String, ByVal pCodImpuesto As Integer) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ObtenerDatosImpuesto(pstrPlaca, _
                                       pCodImpuesto)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ListarCuotasPeriodo(ByVal piiCodigoBien As Integer, ByVal piiPeriodo As Integer, ByVal picCodigoContrato As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ListarCuotasPeriodo(piiCodigoBien, _
                                       piiPeriodo, picCodigoContrato)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ObtenerPeriodosValidacion(ByVal pstrCadigosImpuesto As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ObtenerPeriodosValidacion(pstrCadigosImpuesto)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ObtenerTotalCuotas(ByVal pstrCadigosBien As String, ByVal pstrCodigosPeriodo As String, ByVal pstrContratos As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ObtenerTotalCuotas(pstrCadigosBien, pstrCodigosPeriodo, pstrContratos)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ListarImpuestoVehicularLiquidar(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pCodLote As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ListarImpuestoVehicularLiquidar(pPageSize, _
                                       pCurrentPage, _
                                       pSortColumn, _
                                       pSortOrder, _
                                       pCodLote)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ListarImpuestoVehicularLiquidarTodo(ByVal pCodLote As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ListarImpuestoVehicularLiquidarTodo(pCodLote)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ListarImpuestoVehicularReporteLiquidar(ByVal pCodigoImpuesto As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ListarImpuestoVehicularReporteLiquidar(pCodigoImpuesto)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ListarImpuestoVehicularReporte(ByVal pEImpuestoVehicular As String) As String
        Dim objLImpuestoVehicularReporteNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")
        Try
            Return objLImpuestoVehicularReporteNTX.ListarImpuestoVehicularReporte(pEImpuestoVehicular)
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Function ListarMultaVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pEMultaVehicular As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ListarMultaVehicular(pPageSize, _
                                       pCurrentPage, _
                                       pSortColumn, _
                                       pSortOrder, _
                                       pEMultaVehicular)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ListarBienMultaVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pCodMunicipalidad As String, _
                                   ByVal pPlaca As String, _
                                   ByVal pTipo As String, _
                                           ByVal pNroMotor As String, _
                                           ByVal pCUCliente As String, _
                                           ByVal pCodContrato As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ListarBienMultaVehicular(pPageSize, _
                                       pCurrentPage, _
                                       pSortColumn, _
                                       pSortOrder, _
                                       pCodMunicipalidad, _
                                       pPlaca, _
                                       pTipo, _
                                                                           pNroMotor, pCUCliente, pCodContrato)
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Public Function ObtenerDatosMulta(ByVal pstrPlaca As String, ByVal pintSecMulta As Integer) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ObtenerDatosMulta(pstrPlaca, pintSecMulta)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Obtiene un registro de la multa vehicular
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 21/01/2013
    ''' </remarks>
    Public Function ObtenerDatosMultaConsulta(ByVal pEMultaVehicular As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ObtenerDatosMultaConsulta(pEMultaVehicular)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ListarLoteMultaVehicular(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pPlaca As String, _
                                    ByVal pCodMunicipal As String, _
                                    ByVal pTipo As Integer, _
                                    ByVal pNroLote As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ListarLoteMultaVehicular(pPageSize, _
                                       pCurrentPage, _
                                       pSortColumn, _
                                       pSortOrder, _
                                       pPlaca, pCodMunicipal, pTipo, pNroLote)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ListarEscalaInfraccionesMulta() As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ListarEscalaInfraccionesMulta()
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ListarMultaVehicularLiquidar(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pCodLote As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ListarMultaVehicularLiquidar(pPageSize, _
                                       pCurrentPage, _
                                       pSortColumn, _
                                       pSortOrder, _
                                       pCodLote)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ListarMultaVehicularLiquidarTodo(ByVal pCodLote As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ListarMultaVehicularLiquidarTodo(pCodLote)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ListarMultaVehicularReporteLiquidar(ByVal pCodigoImpuesto As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ListarMultaVehicularReporteLiquidar(pCodigoImpuesto)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ListarAltertaImpuestoVehicular(ByVal pCodLote As String, _
                                                   ByVal pNroCheque As String) As String

        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ListarAltertaImpuestoVehicular(pCodLote, _
                                                                           pNroCheque)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Inicio IBK
    Public Function GetImpuestoMultas(ByVal pNroLote As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")
        Try
            Return objLImpuestoVehicularNTX.GetImpuestoMultas(pNroLote)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoVehicularNTX = Nothing
        End Try
    End Function
    Public Function GetImpuestoVehicular(ByVal pNroLote As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")
        Try
            Return objLImpuestoVehicularNTX.GetImpuestoVehicular(pNroLote)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoVehicularNTX = Nothing
        End Try
    End Function
    'Inicio IBK - AAE - Agrego función que devuelve el lote
    Public Function CheckLote(ByVal pNroLote As String, ByVal flag As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.CheckLote(pNroLote, flag)
        Catch ex As Exception
            Throw ex
        Finally
            objLImpuestoVehicularNTX = Nothing
        End Try
    End Function
    'IBK - AAE - Agrego funcion  para obtnener info del lote
    Public Function ObtenerHeaderLote(ByVal pCodLote As String, ByVal flag As String) As String
        Dim objLImpuestoVehicularNTX As Object = CreateObject("GCC.Logic.LImpuestoVehicularNTX")

        Try
            Return objLImpuestoVehicularNTX.ObtenerHeaderLote(pCodLote, flag)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'Fin IBK
End Class
#End Region

