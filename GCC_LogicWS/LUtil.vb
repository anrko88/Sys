
#Region "Clase Transaccional"

Public Class LUtilTX

    ''' <summary>
    ''' Insertar Bloqueo
    ''' </summary>
    ''' <returns>Bloqueo</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Public Function InsertarBloqueo(ByVal pEBloqueo As String) As String

        Dim objLBloqueoNTx As Object = CreateObject("GCC.Logic.LBloqueoTx")
        Try
            Return objLBloqueoNTx.InsertarBloqueo(pEBloqueo)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Insertar Bloqueo
    ''' </summary>
    ''' <returns>Bloqueo</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Public Function ModificarBloqueo(ByVal pEBloqueo As String) As String

        Dim objLBloqueoNTx As Object = CreateObject("GCC.Logic.LBloqueoTx")
        Try
            Return objLBloqueoNTx.ModificarBloqueo(pEBloqueo)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <param name="pstrCodigoContrato">Código del Contrato</param>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 05/09/2012
    ''' </remarks>
    Public Function fblnGestionFlujo(ByVal pstrCodigoContrato As String, _
                                 ByVal pstrCodigoModulo As String, _
                                 ByVal pstrUsuarioRegistro As String) As Boolean

        Dim objLUtilTx As Object = CreateObject("GCC.Logic.LUtilTx")
        Try
            Return objLUtilTx.fblnGestionFlujo(pstrCodigoContrato, pstrCodigoModulo, pstrUsuarioRegistro)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 21/03/2013
    ''' </remarks>
    Public Function InsertarContactoSuprestatario(ByVal pstrCodigoSuprestatario As String, _
                                 ByVal pstrNombre As String, _
                                 ByVal pstrCorreo As String, _
                                 ByVal pstrTelefono As String) As Boolean

        Dim objLUtilTx As Object = CreateObject("GCC.Logic.LUtilTx")
        Try
            Return objLUtilTx.InsertarContactoSuprestatario(pstrCodigoSuprestatario, pstrNombre, pstrCorreo, pstrTelefono)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 21/03/2013
    ''' </remarks>
    Public Function ModificarContactoSuprestatario(ByVal pstrCodSuprestatarioContacto As Integer, ByVal pstrCodigoSuprestatario As String, _
                                 ByVal pstrNombre As String, _
                                 ByVal pstrCorreo As String, _
                                 ByVal pstrTelefono As String, _
                                 ByVal pstrEstado As String) As Boolean

        Dim objLUtilTx As Object = CreateObject("GCC.Logic.LUtilTx")
        Try
            Return objLUtilTx.ModificarContactoSuprestatario(pstrCodSuprestatarioContacto, pstrCodigoSuprestatario, pstrNombre, pstrCorreo, pstrTelefono, pstrEstado)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 22/03/2013
    ''' </remarks>
    Public Function InsertarContactoPreferente(ByVal pstrCodSuprestatarioContacto As Integer, ByVal pstrCodigoSuprestatario As String, _
                                               ByVal pstrNombre As String, ByVal pstrCorreo As String, ByVal pstrTelefono As String) As Boolean

        Dim objLUtilTx As Object = CreateObject("GCC.Logic.LUtilTx")
        Try
            Return objLUtilTx.InsertarContactoPreferente(pstrCodSuprestatarioContacto, pstrCodigoSuprestatario, pstrNombre, pstrCorreo, pstrTelefono)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 22/03/2013
    ''' </remarks>
    Public Function ActualizarSubprestatario(ByVal pstrCodigoSuprestatario As String, _
                                               ByVal pstrDireccion As String) As Boolean

        Dim objLUtilTx As Object = CreateObject("GCC.Logic.LUtilTx")
        Try
            Return objLUtilTx.ActualizarSubprestatario(pstrCodigoSuprestatario, pstrDireccion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

#End Region

#Region "Clase No Transaccional"

Public Class LUtilNTX

    'Inicio IBK RPR
    Public Function ObtenerFechaCierre(ByVal pCodModulo As String) As String
        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            Return objUtilNTx.ObtenerFechaCierre(pCodModulo)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Fin IBK

    Public Function ListarMoneda() As String
        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            'Return objUtilNTx.ListadoTemporal()
            Return objUtilNTx.LeerMoneda()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarPais() As String
        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            'Return objUtilNTx.ListadoTemporal()
            Return objUtilNTx.LeerPais()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarDepartamento() As String
        Dim objUbigeoNTx As Object = CreateObject("GCC.Logic.LUbigeoNTx")
        Try
            'Return objUtilNTx.ListadoTemporal()
            Return objUbigeoNTx.LeerDepartamento()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarProvincia(ByVal pstrDepartamento As String) As String
        Dim objUbigeoNTx As Object = CreateObject("GCC.Logic.LUbigeoNTx")
        Try
            'Return objUtilNTx.ListadoTemporal()
            Return objUbigeoNTx.LeerProvincia(pstrDepartamento)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ListarDistrito(ByVal pstrDepartamento As String, ByVal pstrProvincia As String) As String
        Dim objUbigeoNTx As Object = CreateObject("GCC.Logic.LUbigeoNTx")
        Try
            'Return objUtilNTx.ListadoTemporal()
            Return objUbigeoNTx.LeerDistrito(pstrDepartamento, pstrProvincia)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerValorGenerico(ByVal pstrDominio As String, ByVal pstrParametro As String) As String
        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            Return objUtilNTx.ObtenerValorGenerico(pstrDominio, pstrParametro)
        Catch ex As Exception
            Throw ex
        Finally
            objUtilNTx = Nothing
        End Try
    End Function
    ''' <summary>
    ''' Lista los estados del contrato a partir del estado "En Elaboracion".
    ''' </summary>
    ''' <returns>Lista de estados, ordenados por el orden que siguen en el flujo (valor4).</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListarEstadosBusquedaContrato() As String
        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            Return objUtilNTx.ListarEstadosBusquedaContrato()
        Catch ex As Exception
            Throw ex
        Finally
            objUtilNTx = Nothing
        End Try
    End Function
    Public Function ListarNotarias(ByVal pstrDepartamento As String, _
                                   ByVal pstrProvincia As String) As String

        Dim objUbigeoNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            Return objUbigeoNTx.ListarNotarias(pstrDepartamento, pstrProvincia)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'IBK - RPH
    Public Function ObtenerContatoNotarias(ByVal pstrCodNotaria) As String
        Dim objUbigeoNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            Return objUbigeoNTx.ObtenerContactoNotarias(pstrCodNotaria)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerTipoCambio(ByVal pCodMoneda As String, ByVal pTipoModalidadCambio As String, ByVal pFechaInicioVigencia As String) As String

        Dim objUbigeoNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            'Return objUtilNTx.ListadoTemporal()
            Return objUbigeoNTx.ObtenerTipoCambio(pCodMoneda, pTipoModalidadCambio, pFechaInicioVigencia)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ConsultaConceptosTarifas(ByVal pdblimporte As Decimal, _
                                             ByVal pstrTipoConcepto As String, _
                                             ByVal pstrCodtarifa As String, _
                                             ByVal pstrCodmoneda As String) As String

        Dim objLUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            'Return objUtilNTx.ListadoTemporal()
            Return objLUtilNTx.ConsultaConceptosTarifas(pdblimporte, pstrTipoConcepto, pstrCodtarifa, pstrCodmoneda)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Obtener Bloqueo
    ''' </summary>
    ''' <returns>Devuelve Bloqueo</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Public Function ObtenerBloqueo(ByVal pEBloqueo As String) As String

        Dim objLBloqueoNTx As Object = CreateObject("GCC.Logic.LBloqueoNTx")
        Try
            Return objLBloqueoNTx.ObtenerBloqueo(pEBloqueo)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' ListarRegistroCompra
    ''' </summary>
    ''' <param name="strFechaIni"></param>
    ''' <param name="strFechaFin"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListarRegistroCompra(ByVal strFechaIni As String, ByVal strFechaFin As String) As String

        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            'Return objUtilNTx.ListadoTemporal()
            Return objUtilNTx.ListarRegistroCompra(strFechaIni, strFechaFin)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' ListarPipeline
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListarPipeline() As String

        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            'Return objUtilNTx.ListadoTemporal()
            Return objUtilNTx.ListarPipeline()
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Inicio IBK - AAE 
    ''' <summary>
    ''' ListarPipeline
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListarPipeline2(ByVal strListaCotiz As String, _
                                    ByVal pCUCliente As String, _
                                    ByVal pRazonSocialCli As String, _
                                    ByVal pCodEjecutivo As String, _
                                    ByVal pCodBanca As String, _
                                    ByVal pCodEstado As String) As String

        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            'Return objUtilNTx.ListadoTemporal()
            Return objUtilNTx.ListarPipeline2(strListaCotiz, pCUCliente, pRazonSocialCli, pCodEjecutivo, pCodBanca, pCodEstado)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'Fin IBK - AAE
    ''' <summary>
    ''' ListarRetenciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListarRetenciones(ByVal pstrFechaInicial As String, ByVal pstrFechaFinal As String) As String

        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            Return objUtilNTx.ListarRetenciones(pstrFechaInicial, pstrFechaFinal)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'JJM IBK
    ''' <summary>
    ''' ListarRetenciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListarRetencionesSunat(ByVal pstrFechaInicial As String, ByVal pstrFechaFinal As String) As String

        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            Return objUtilNTx.ListarRetencionesSunat(pstrFechaInicial, pstrFechaFinal)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'Inicio IBK - AAE - Agrego funciones para registro de ventas
    Public Function ListaRegistroVentas(ByVal strFlag As String, ByVal pstrFechaInicial As String, ByVal pstrFechaFinal As String) As String

        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            Return objUtilNTx.ListarRegistrosVentas(strFlag, pstrFechaInicial, pstrFechaFinal)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'Inicio IBK - JJM - Agrego funciones para registro de compras
    Public Function ListarRegistrosCompras(ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String
        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            Return objUtilNTx.ListarRegistrosCompras(pFechaInicial, pFechaFinal)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ListarMunicipalidad() As String
        Dim objUbigeoNTx As Object = CreateObject("GCC.Logic.LUbigeoNTx")
        Try
            Return objUbigeoNTx.LeerMunicipalidad()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListadoMunicipalidadPaginado(ByVal pPageSize As Integer, _
                                          ByVal pCurrentPage As Integer, _
                                          ByVal pSortColumn As String, _
                                          ByVal pSortOrder As String, _
                                          ByVal pCodMunicipalidad As String, _
                                          ByVal pMunicipalidad As String) As String
        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUbigeoNTx")
        Try
            Return objUtilNTx.ListadoMunicipalidadPaginado(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pCodMunicipalidad, pMunicipalidad)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Fin JJM IBK

    'Inicio IBK - AAE - Agrego funciones para notas de abono
    Public Function ListarNotasAbono(ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String
        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            Return objUtilNTx.ListarNotasAbono(pFechaInicial, pFechaFinal)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'TS - AEP
    Public Function ListadoClienteSuprestatario(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pCodigo As String, _
                                     ByVal pCuCliente As String, _
                                     ByVal pTipoDocumento As String, _
                                     ByVal pNumeroDocumento As String, _
                                     ByVal pNombreCliente As String, _
                                     ByVal pDireccion As String, _
                                     ByVal pNombre As String, _
                                     ByVal pCorreo As String, _
                                     ByVal pTelefono As String) As String
        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            Return objUtilNTx.ListadoClienteSuprestatario(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pCodigo, pCuCliente, pTipoDocumento, pNumeroDocumento, _
                                                                  pNombreCliente, pDireccion, pNombre, pCorreo, pTelefono)
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    'TS - AEP
    Public Function ListarContactoSuprestatario(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pCodSuprestario As String, _
                                     ByVal pNombre As String, _
                                     ByVal pCorreo As String, _
                                     ByVal pTelefono As String) As String
        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            Return objUtilNTx.ListarContactoSuprestatario(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pCodSuprestario, pNombre, pCorreo, pTelefono)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function ObteberContactoPreferente(ByVal pCodSuprestario As String) As String
        Dim objUtilNTx As Object = CreateObject("GCC.Logic.LUtilNTx")
        Try
            Return objUtilNTx.ObteberContactoPreferente(pCodSuprestario)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
End Class

#End Region
