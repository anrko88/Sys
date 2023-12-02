Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports Microsoft.VisualBasic

Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data


#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LUtilTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/05/2012
''' </remarks>
<Guid("F7E5FC30-C31A-4635-856B-6C31F08B0395") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LUtilTx")> _
Public Class LUtilTx
    Inherits ServicedComponent
    Implements IUtilTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LUtilTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <param name="pstrCodigoContrato">Código del Contrato</param>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 05/09/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnGestionFlujo(ByVal pstrCodigoContrato As String, _
                                 ByVal pstrCodigoModulo As String, _
                                 ByVal pstrUsuarioRegistro As String) As Boolean Implements IUtilTx.fblnGestionFlujo

        Dim objDUtilTx As DUtilTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDUtilTx = New DUtilTx
            blnResultado = objDUtilTx.fblnGestionFlujo(pstrCodigoContrato, pstrCodigoModulo, pstrUsuarioRegistro)
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilTx.Dispose()
            objDUtilTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 21/03/2013
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarContactoSuprestatario(ByVal pstrCodigoSuprestatario As String, _
                                 ByVal pstrNombre As String, _
                                 ByVal pstrCorreo As String, _
                                 ByVal pstrTelefono As String) As Boolean Implements IUtilTx.InsertarContactoSuprestatario

        Dim objDUtilTx As DUtilTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDUtilTx = New DUtilTx
            blnResultado = objDUtilTx.InsertarContactoSuprestatario(pstrCodigoSuprestatario, pstrNombre, pstrCorreo, pstrTelefono)
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilTx.Dispose()
            objDUtilTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 21/03/2013
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarContactoSuprestatario(ByVal pstrCodSuprestatarioContacto As Integer, ByVal pstrCodigoSuprestatario As String, _
                                 ByVal pstrNombre As String, _
                                 ByVal pstrCorreo As String, _
                                 ByVal pstrTelefono As String, _
                                 ByVal pstrEstado As String) As Boolean Implements IUtilTx.ModificarContactoSuprestatario

        Dim objDUtilTx As DUtilTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDUtilTx = New DUtilTx
            blnResultado = objDUtilTx.ModificarContactoSuprestatario(pstrCodSuprestatarioContacto, pstrCodigoSuprestatario, pstrNombre, pstrCorreo, pstrTelefono, pstrEstado)
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilTx.Dispose()
            objDUtilTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 22/03/2013
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarContactoPreferente(ByVal pstrCodSuprestatarioContacto As Integer, ByVal pstrCodigoSuprestatario As String, _
                                               ByVal pstrNombre As String, ByVal pstrCorreo As String, ByVal pstrTelefono As String) As Boolean Implements IUtilTx.InsertarContactoPreferente

        Dim objDUtilTx As DUtilTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDUtilTx = New DUtilTx
            blnResultado = objDUtilTx.InsertarContactoPreferente(pstrCodSuprestatarioContacto, pstrCodigoSuprestatario, pstrNombre, pstrCorreo, pstrTelefono)
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilTx.Dispose()
            objDUtilTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 22/03/2013
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ActualizarSubprestatario(ByVal pstrCodigoSuprestatario As String, _
                                               ByVal pstrDireccion As String) As Boolean Implements IUtilTx.ActualizarSubprestatario

        Dim objDUtilTx As DUtilTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDUtilTx = New DUtilTx
            blnResultado = objDUtilTx.ActualizarSubprestatario(pstrCodigoSuprestatario, pstrDireccion)
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilTx.Dispose()
            objDUtilTx = Nothing
        End Try

        Return blnResultado
    End Function
#End Region

End Class

#End Region

#Region "Clase No Transaccional"

''' <summary>
''' Implementación de la clase LTemporalNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("D8C13879-BE81-4221-89CA-5F1304C90B37") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LUtilNTx")> _
Public Class LUtilNTx
    Inherits ServicedComponent
    Implements IUtilNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LUtilNTx"
#End Region

#Region "Metodos"

    'Inicio IBK RPR
    Public Function ObtenerFechaCierre(ByVal pCodModulo As String) As String Implements IUtilNTx.ObtenerFechaCierre
        Try
            Using oDUltilNtx As New DUtilNtx
                Return oDUltilNtx.ObtenerFechaCierre(pCodModulo)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Fin IBK

    Public Function LeerMoneda() As String Implements IUtilNTx.ListadoMoneda
        Dim objDUtilNTx As DUtilNtx = Nothing
        Dim strResultado As String

        Try
            objDUtilNTx = New DUtilNtx
            strResultado = objDUtilNTx.ListadoMoneda()
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilNTx.Dispose()
            objDUtilNTx = Nothing

        End Try
        Return strResultado

    End Function

    Public Function LeerPais() As String Implements IUtilNTx.ListadoPais
        Dim objDUtilNTx As DUtilNtx = Nothing
        Dim strResultado As String

        Try
            objDUtilNTx = New DUtilNtx
            strResultado = objDUtilNTx.ListadoPais()
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilNTx.Dispose()
            objDUtilNTx = Nothing

        End Try
        Return strResultado

    End Function

    ''' <summary>
    ''' Obtener un la descripcion de un valor especifico en tabla valor generico
    ''' </summary>
    ''' <param name="pstrDominio">Id de la tabla a consultar</param>
    ''' <param name="pstrParametro">valor de la tabla a identificar la descripcion</param>
    ''' <returns>Datatable serializado</returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 04/05/2012
    ''' </remarks>
    Public Function ObtenerValorGenerico(ByVal pstrDominio As String, ByVal pstrParametro As String) As String Implements IUtilNTx.ObtenerValorGenerico
        Try
            Using oDUltilNtx As New DUtilNtx
                Return oDUltilNtx.ObtenerValorGenerico(pstrDominio, pstrParametro)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Listado de los contratos coincidentse con los criterios de búsqueda
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 09/05/2012
    ''' </remarks>
    Public Function ListarEstadosBusquedaContrato() As String Implements IUtilNTx.ListarEstadosBusquedaContrato
        Dim oDUltilNtx As DUtilNtx = Nothing
        Dim resultado As String

        Try
            oDUltilNtx = New DUtilNtx
            resultado = oDUltilNtx.ListarEstadosBusquedaContrato()
        Catch ex As Exception
            Throw ex
        Finally
            oDUltilNtx.Dispose()
        End Try

        Return resultado
    End Function
    'IBK - RPH
    Public Function ListarNotarias(ByVal pCodDepartamento As String, _
                                   ByVal pCodProvincia As String) As String Implements IUtilNTx.ListarNotarias

        Dim oDValorGenericaNTx As DValorGenericaNTx = Nothing
        Dim resultado As String

        Try
            oDValorGenericaNTx = New DValorGenericaNTx
            resultado = oDValorGenericaNTx.ListarNotarias(pCodDepartamento, pCodProvincia)
        Catch ex As Exception
            Throw ex
        Finally
            oDValorGenericaNTx.Dispose()
        End Try

        Return resultado
    End Function
    'IBK - RPH
    Public Function ObtenerContactoNotarias(ByVal pCodNotaria As String) As String Implements IUtilNTx.ObtenerContactoNotarias
        Dim oDValorGenericaNTx As DValorGenericaNTx = Nothing
        Dim resultado As String
        Try
            oDValorGenericaNTx = New DValorGenericaNTx
            resultado = oDValorGenericaNTx.ObtenerContactoNotarias(pCodNotaria)
        Catch ex As Exception
            Throw ex
        Finally
            oDValorGenericaNTx.Dispose()
        End Try

        Return resultado
    End Function






    Public Function ObtenerTipoCambio(ByVal pCodMoneda As String, ByVal pTipoModalidadCambio As String, ByVal pFechaInicioVigencia As String) As String Implements IUtilNTx.ObtenerTipoCambio
        Dim oDUtilNTx As DUtilNtx = Nothing
        Dim resultado As String

        Try
            oDUtilNTx = New DUtilNtx
            resultado = oDUtilNTx.ObtenerTipoCambio(pCodMoneda, pTipoModalidadCambio, pFechaInicioVigencia)
        Catch ex As Exception
            Throw ex
        Finally
            oDUtilNTx.Dispose()
        End Try

        Return resultado
    End Function

    ''' <summary>
    ''' ConsultaConceptosTarifas
    ''' </summary>
    ''' <param name="pdblimporte"></param>
    ''' <param name="pstrCodtarifa"></param>
    ''' <param name="pstrCodmoneda"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConsultaConceptosTarifas(ByVal pdblimporte As Decimal, _
                                        ByVal pstrTipoConcepto As String, _
                                        ByVal pstrCodtarifa As String, _
                                        ByVal pstrCodmoneda As String) As String Implements IUtilNTx.ConsultaConceptosTarifas
        Try
            Using oDLSGConceptosTarifasNTx As New DLSGConceptosTarifasNTx
                Return oDLSGConceptosTarifasNTx.ConsultaConceptosTarifas(pdblimporte, pstrTipoConcepto, pstrCodtarifa, pstrCodmoneda)

            End Using
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
    Public Function ListarRegistroCompra(ByVal strFechaIni As String, ByVal strFechaFin As String) As String Implements IUtilNTx.ListarRegistroCompra
        Dim objDUtilNTx As DUtilNtx = Nothing
        Dim strResultado As String

        Try
            objDUtilNTx = New DUtilNtx
            strResultado = objDUtilNTx.ListarRegistroCompra(strFechaIni, strFechaFin)
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilNTx.Dispose()
            objDUtilNTx = Nothing

        End Try
        Return strResultado

    End Function

#End Region

    Public Function ListarPipeline() As String Implements IUtilNTx.ListarPipeline
        Dim objDUtilNTx As DUtilNtx = Nothing
        Dim strResultado As String

        Try
            objDUtilNTx = New DUtilNtx
            strResultado = objDUtilNTx.ListarPipeline()
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilNTx.Dispose()
            objDUtilNTx = Nothing

        End Try
        Return strResultado
    End Function

    Public Function ListarRetenciones(ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String Implements IUtilNTx.ListarRetenciones
        Dim objDUtilNTx As DUtilNtx = Nothing
        Dim strResultado As String

        Try
            objDUtilNTx = New DUtilNtx
            strResultado = objDUtilNTx.ListarRetenciones(pFechaInicial, pFechaFinal)
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilNTx.Dispose()
            objDUtilNTx = Nothing

        End Try
        Return strResultado
    End Function
    'IBK JJM
    Public Function ListarRetencionesSunat(ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String Implements IUtilNTx.ListarRetencionesSunat
        Dim objDUtilNTx As DUtilNtx = Nothing
        Dim strResultado As String

        Try
            objDUtilNTx = New DUtilNtx
            strResultado = objDUtilNTx.ListarRetencionesSunat(pFechaInicial, pFechaFinal)
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilNTx.Dispose()
            objDUtilNTx = Nothing

        End Try
        Return strResultado
    End Function

    Public Function ListarRegistrosVentas(ByVal strFlag As String, ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String Implements IUtilNTx.ListarRegistrosVentas
        Dim objDUtilNTx As DUtilNtx = Nothing
        Dim strResultado As String

        Try
            objDUtilNTx = New DUtilNtx
            strResultado = objDUtilNTx.ListarRegistrosVentas(strFlag, pFechaInicial, pFechaFinal)
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilNTx.Dispose()
            objDUtilNTx = Nothing

        End Try
        Return strResultado
    End Function

    Function ListarPipeline2(ByVal strCotiza As String, _
                                    ByVal pCUCliente As String, _
                                    ByVal pRazonSocialCli As String, _
                                    ByVal pCodEjecutivo As String, _
                                    ByVal pCodBanca As String, _
                                    ByVal pCodEstado As String) As String Implements IUtilNTx.ListarPipeline2
        Dim objDUtilNTx As DUtilNtx = Nothing
        Dim strResultado As String

        Try
            objDUtilNTx = New DUtilNtx
            strResultado = objDUtilNTx.ListarPipeline2(strCotiza, pCUCliente, pRazonSocialCli, pCodEjecutivo, pCodBanca, pCodEstado)
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilNTx.Dispose()
            objDUtilNTx = Nothing

        End Try
        Return strResultado
    End Function

    Public Function ListarRegistrosCompras(ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String Implements IUtilNTx.ListarRegistrosCompras
        Dim objDUtilNTx As DUtilNtx = Nothing
        Dim strResultado As String

        Try
            objDUtilNTx = New DUtilNtx
            strResultado = objDUtilNTx.ListarRegistrosCompras(pFechaInicial, pFechaFinal)
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilNTx.Dispose()
            objDUtilNTx = Nothing

        End Try
        Return strResultado
    End Function

    'Inicio IBK - AAE
    Public Function ListarNotasAbono(ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String Implements IUtilNTx.ListarNotasAbono
        Dim objDUtilNTx As DUtilNtx = Nothing
        Dim strResultado As String

        Try
            objDUtilNTx = New DUtilNtx
            strResultado = objDUtilNTx.ListarNotasAbono(pFechaInicial, pFechaFinal)
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilNTx.Dispose()
            objDUtilNTx = Nothing

        End Try
        Return strResultado
    End Function
    ' Fin IBK
    'TS_AEP
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
                                     ByVal pTelefono As String) As String Implements IUtilNTx.ListadoClienteSuprestatario
        Dim objDUtilNTx As DUtilNtx = Nothing
        Dim strResultado As String

        Try
            objDUtilNTx = New DUtilNtx
            strResultado = objDUtilNTx.ListadoClienteSuprestatario(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pCodigo, pCuCliente, pTipoDocumento, pNumeroDocumento, _
                                                                  pNombreCliente, pDireccion, pNombre, pCorreo, pTelefono)
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilNTx.Dispose()
            objDUtilNTx = Nothing

        End Try
        Return strResultado
    End Function

    'TS_AEP
    Public Function ListarContactoSuprestatario(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pCodSuprestario As String, _
                                     ByVal pNombre As String, _
                                     ByVal pCorreo As String, _
                                     ByVal pTelefono As String) As String Implements IUtilNTx.ListarContactoSuprestatario
        Dim objDUtilNTx As DUtilNtx = Nothing
        Dim strResultado As String

        Try
            objDUtilNTx = New DUtilNtx
            strResultado = objDUtilNTx.ListarContactoSuprestatario(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pCodSuprestario, pNombre, pCorreo, pTelefono)
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilNTx.Dispose()
            objDUtilNTx = Nothing

        End Try
        Return strResultado
    End Function

    Public Function ObteberContactoPreferente(ByVal pCodSuprestario As String) As String Implements IUtilNTx.ObteberContactoPreferente
        Dim objDUtilNTx As DUtilNtx = Nothing
        Dim strResultado As String

        Try
            objDUtilNTx = New DUtilNtx
            strResultado = objDUtilNTx.ObteberContactoPreferente(pCodSuprestario)
        Catch ex As Exception
            Throw ex
        Finally
            objDUtilNTx.Dispose()
            objDUtilNTx = Nothing

        End Try
        Return strResultado
    End Function
End Class

#End Region
