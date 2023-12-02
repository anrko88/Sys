

Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data
#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase IImpuestoVehicularTX
''' </summary>
''' <remarks>
''' Creado Por         : TSF - AEP
''' Fecha de Creación  : 12/11/2012
''' </remarks>
<Guid("FB8225A1-A715-4187-8DDF-B018724CCF29") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase IImpuestoVehicularTX")> _
Public Class LImpuestoVehicularTX
    Inherits ServicedComponent
    Implements IImpuestoVehicularTX
    'Inicio IBK - AAE - Retorno un string
    <AutoComplete(True)> _
    Public Function GrabarImpuestoVehicular(ByVal pEImpuestoVehicular As String) As String Implements IImpuestoVehicularTX.GrabarImpuestoVehicular

        Dim objDImpuestoVehicularTx As DImpuestoVehicularTX = Nothing
        'Inicio IBK - AAE - Retorno un string
        'Dim blnResultado As Boolean
        Dim blnResultado As String
        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularTX
            blnResultado = objDImpuestoVehicularTx.GrabarImpuestoVehicular(pEImpuestoVehicular)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return blnResultado

    End Function
    'Inicio IBK - AAE - Retorno un string
    <AutoComplete(True)> _
 Public Function ModificarImpuestoVehicular(ByVal pEImpuestoVehicular As String) As String Implements IImpuestoVehicularTX.ModificarImpuestoVehicular

        Dim objDImpuestoVehicularTx As DImpuestoVehicularTX = Nothing
        'Inicio IBK - AAE - Retorno un string
        'Dim blnResultado As Boolean
        Dim blnResultado As String

        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularTX
            blnResultado = objDImpuestoVehicularTx.ModificarImpuestoVehicular(pEImpuestoVehicular)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return blnResultado

    End Function

    <AutoComplete(True)> _
Public Function AsignarLoteImpuestoVehicular(ByVal pEImpuestoVehicular As String) As String Implements IImpuestoVehicularTX.AsignarLoteImpuestoVehicular

        Dim objDImpuestoVehicularTx As DImpuestoVehicularTX = Nothing
        Dim strCodLote As String

        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularTX
            strCodLote = objDImpuestoVehicularTx.AsignarLoteImpuestoVehicular(pEImpuestoVehicular)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return strCodLote

    End Function

    'Inicio IBK - AAE Agrego paráemtro
    <AutoComplete(True)> _
 Public Function EliminarImpuestoVehicular(ByVal pIntCodigoImpuesto As String, ByVal pStrCodigoUsuario As String, ByVal pstrLote As String) As Boolean Implements IImpuestoVehicularTX.EliminarImpuestoVehicular

        Dim objDImpuestoVehicularTx As DImpuestoVehicularTX = Nothing
        Dim blnResultado As Boolean

        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularTX
            blnResultado = objDImpuestoVehicularTx.EliminarImpuestoVehicular(pIntCodigoImpuesto, pStrCodigoUsuario, pstrLote)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return blnResultado

    End Function
    <AutoComplete(True)> _
Public Function AsignarChequeImpuestoVehicular(ByVal pEImpuestoVehicular As String) As Boolean Implements IImpuestoVehicularTX.AsignarChequeImpuestoVehicular

        Dim objDImpuestoVehicularTx As DImpuestoVehicularTX = Nothing
        Dim strCodLote As String

        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularTX
            strCodLote = objDImpuestoVehicularTx.AsignarChequeImpuestoVehicular(pEImpuestoVehicular)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return strCodLote

    End Function
    'Inicio IBK - AAE - Retorno un string
    <AutoComplete(True)> _
  Public Function GrabarMultaVehicular(ByVal pEMultaVehicular As String) As String Implements IImpuestoVehicularTX.GrabarMultaVehicular

        Dim objDImpuestoVehicularTx As DImpuestoVehicularTX = Nothing
        'Inicio IBK - AAE - Retorno un string
        'Dim blnResultado As Boolean
        Dim blnResultado As String

        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularTX
            blnResultado = objDImpuestoVehicularTx.GrabarMultaVehicular(pEMultaVehicular)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return blnResultado

    End Function
    'Inicio IBK - AAE - Retorno un string
    <AutoComplete(True)> _
 Public Function ModificarMultaVehicular(ByVal pEMultaVehicular As String) As String Implements IImpuestoVehicularTX.ModificarMultaVehicular

        Dim objDImpuestoVehicularTx As DImpuestoVehicularTX = Nothing
        'Inicio IBK - AAE - Retorno un string
        'Dim blnResultado As Boolean
        Dim blnResultado As String

        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularTX
            blnResultado = objDImpuestoVehicularTx.ModificarMultaVehicular(pEMultaVehicular)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return blnResultado

    End Function
    ' Inicio IBK - Agrego Parametro
    <AutoComplete(True)> _
 Public Function EliminarMultaVehicular(ByVal pIntCodigoMulta As String, ByVal pStrCodigoUsuario As String, ByVal pstrLote As String) As Boolean Implements IImpuestoVehicularTX.EliminarMultaVehicular

        Dim objDImpuestoVehicularTx As DImpuestoVehicularTX = Nothing
        Dim blnResultado As Boolean

        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularTX
            blnResultado = objDImpuestoVehicularTx.EliminarMultaVehicular(pIntCodigoMulta, pStrCodigoUsuario, pstrLote)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return blnResultado

    End Function

    <AutoComplete(True)> _
Public Function AsignarLoteMultaVehicular(ByVal pEImpuestoVehicular As String) As String Implements IImpuestoVehicularTX.AsignarLoteMultaVehicular

        Dim objDImpuestoVehicularTx As DImpuestoVehicularTX = Nothing
        Dim strCodLote As String

        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularTX
            strCodLote = objDImpuestoVehicularTx.AsignarLoteMultaVehicular(pEImpuestoVehicular)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return strCodLote

    End Function
    <AutoComplete(True)> _
Public Function AsignarChequeMultaVehicular(ByVal pEImpuestoVehicular As String) As Boolean Implements IImpuestoVehicularTX.AsignarChequeMultaVehicular

        Dim objDImpuestoVehicularTx As DImpuestoVehicularTX = Nothing
        Dim strCodLote As String

        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularTX
            strCodLote = objDImpuestoVehicularTx.AsignarChequeMultaVehicular(pEImpuestoVehicular)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return strCodLote

    End Function
    <AutoComplete(True)> _
 Public Function EliminarLote(ByVal pLote As String) As Boolean Implements IImpuestoVehicularTX.EliminarLote

        Dim objDImpuestoVehicularTx As DImpuestoVehicularTX = Nothing
        Dim blnResultado As Boolean

        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularTX
            blnResultado = objDImpuestoVehicularTx.EliminarLote(pLote)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return blnResultado

    End Function
    'Inicio IBK - AAE - Obtengo el nro de lote tentativo
    <AutoComplete(True)> _
Public Function AsignarLoteImpuestoVehicular2(ByVal pEImpuestoVehicular As String) As String Implements IImpuestoVehicularTX.AsignarLoteImpuestoVehicular2

        Dim objDImpuestoVehicularTx As DImpuestoVehicularTX = Nothing
        Dim strCodLote As String

        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularTX
            strCodLote = objDImpuestoVehicularTx.AsignarLoteImpuestoVehicular2(pEImpuestoVehicular)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return strCodLote

    End Function
    <AutoComplete(True)> _
    Public Function ReGenerarLote(ByVal strLote As String) As String Implements IImpuestoVehicularTX.ReGenerarLote

        Dim objDImpuestoVehicularTx As DImpuestoVehicularTX = Nothing
        Dim strCodLote As String

        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularTX
            strCodLote = objDImpuestoVehicularTx.ReGenerarLote(strLote)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return strCodLote

    End Function
    <AutoComplete(True)> _
    Public Function AnularLote(ByVal pLote As String) As String Implements IImpuestoVehicularTX.AnularLote

        Dim objDImpuestoVehicularTx As DImpuestoVehicularTX = Nothing
        Dim strResultado As String

        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularTX
            strResultado = objDImpuestoVehicularTx.AnularLote(pLote)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return strResultado

    End Function
    'Fin IBK - AAE




   
End Class
#End Region

#Region "Clase No Transaccional"
''' <summary>
''' Implementación de la clase LDocProveedorNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - AEP
''' Fecha de Creación  : 12/11/2012
''' </remarks>
<Guid("9FC1BE84-18D3-4b96-AED3-D0B2EADE04DA") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LDocBienNTx")> _
Public Class LImpuestoVehicularNTX
    Inherits ServicedComponent
    Implements IImpuestoVehicularNTX

    'Inicio JJM IBK

    Public Function LiquidarLote(ByVal pUsuarioModificacion As String, ByVal pNroLote As String, ByVal pCodigoConcepto As String) As String Implements IImpuestoVehicularNTX.LiquidarLote

        Dim objDImpuestoVehicularTx As DImpuestoVehicularNTX = Nothing
        Dim strRespuesta As String

        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularNTX
            strRespuesta = objDImpuestoVehicularTx.LiquidarLote(pUsuarioModificacion, pNroLote, pCodigoConcepto)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return strRespuesta

    End Function
    'Fin JJM IBK

    Public Function ListarImpuestoVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pEImpuestoVehicular As String) As String Implements IImpuestoVehicularNTX.ListarImpuestoVehicular
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ListarImpuestoVehicular(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pEImpuestoVehicular)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    Public Function ListarBienImpuestoVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pPlaca As String, _
                                    ByVal pSecfinanciamiento As String, _
                                           ByVal pNroMotor As String, _
                                           ByVal pCUCliente As String, _
                                           ByVal pCodContrato As String) As String Implements IImpuestoVehicularNTX.ListarBienImpuestoVehicular

        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            'strResultado = objDImpuestoVehicularNTX.ListarBienImpuestoVehicular(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pPlaca, pSecfinanciamiento)
            strResultado = objDImpuestoVehicularNTX.ListarBienImpuestoVehicular(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pPlaca, pSecfinanciamiento, _
                                                                           pNroMotor, pCUCliente, pCodContrato)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    Public Function ListarImpuestoVehicularReporte(ByVal pEImpuestoVehicular As String) As String Implements IImpuestoVehicularNTX.ListarImpuestoVehicularReporte
        Dim objDImpuestoVehicularReporteNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularReporteNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularReporteNTX.ListarImpuestoVehicularReporte(pEImpuestoVehicular)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularReporteNTX.Dispose()
        End Try
        Return strResultado
    End Function

    Public Function ListarLoteImpuestoVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pPlaca As String, _
                                   ByVal pTipo As Integer, _
                                   ByVal pNroLote As String) As String Implements IImpuestoVehicularNTX.ListarLoteImpuestoVehicular

        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            'strResultado = objDImpuestoVehicularNTX.ListarLoteImpuestoVehicular(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pPlaca, pTipo)
            strResultado = objDImpuestoVehicularNTX.ListarLoteImpuestoVehicular(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pPlaca, pTipo, pNroLote)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    Public Function ListarLoteImpuestoVehicularConsulta(ByVal pPageSize As Integer, _
                                 ByVal pCurrentPage As Integer, _
                                 ByVal pSortColumn As String, _
                                 ByVal pSortOrder As String, _
                                 ByVal pPlaca As String, _
                                 ByVal pTipo As Integer) As String Implements IImpuestoVehicularNTX.ListarLoteImpuestoVehicularConsulta

        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ListarLoteImpuestoVehicularConsulta(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pPlaca, pTipo)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    Public Function ListarImpuestoVehicularLiquidarTodo(ByVal pCodLote As String) As String Implements IImpuestoVehicularNTX.ListarImpuestoVehicularLiquidarTodo
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ListarImpuestoVehicularLiquidarTodo(pCodLote)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    Public Function ListarImpuestoVehicularReporteLiquidar(ByVal pCodigoImpuesto As String) As String Implements IImpuestoVehicularNTX.ListarImpuestoVehicularReporteLiquidar
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ListarImpuestoVehicularReporteLiquidar(pCodigoImpuesto)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    Public Function ObtenerDatosImpuesto(ByVal pstrPlaca As String, ByVal pCodImpuesto As Integer) As String Implements IImpuestoVehicularNTX.ObtenerDatosImpuesto
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ObtenerDatosImpuesto(pstrPlaca, pCodImpuesto)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    Public Function ListarCuotasPeriodo(ByVal piiCodigoBien As Integer, ByVal piiPeriodo As Integer, ByVal picCodigoContrato As String) As String Implements IImpuestoVehicularNTX.ListarCuotasPeriodo
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ListarCuotasPeriodo(piiCodigoBien, piiPeriodo, picCodigoContrato)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    Public Function ObtenerPeriodosValidacion(ByVal pstrCadigosImpuesto As String) As String Implements IImpuestoVehicularNTX.ObtenerPeriodosValidacion
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ObtenerPeriodosValidacion(pstrCadigosImpuesto)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function

    Public Function ObtenerTotalCuotas(ByVal pstrCadigosBien As String, ByVal pstrCodigosPeriodo As String, ByVal pstrContratos As String) As String Implements IImpuestoVehicularNTX.ObtenerTotalCuotas
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ObtenerTotalCuotas(pstrCadigosBien, pstrCodigosPeriodo, pstrContratos)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    Public Function ListarMultaVehicular(ByVal pPageSize As Integer, _
                                ByVal pCurrentPage As Integer, _
                                ByVal pSortColumn As String, _
                                ByVal pSortOrder As String, _
                                ByVal pEMultaVehicular As String) As String Implements IImpuestoVehicularNTX.ListarMultaVehicular
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ListarMultaVehicular(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pEMultaVehicular)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    Public Function ListarBienMultaVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pCodMunicipalidad As String, _
                                   ByVal pPlaca As String, _
                                   ByVal pSecfinanciamiento As String, _
                                           ByVal pNroMotor As String, _
                                           ByVal pCUCliente As String, _
                                           ByVal pCodContrato As String) As String Implements IImpuestoVehicularNTX.ListarBienMultaVehicular
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            'strResultado = objDImpuestoVehicularNTX.ListarBienMultaVehicular(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pCodMunicipalidad, pPlaca, pSecfinanciamiento)
            strResultado = objDImpuestoVehicularNTX.ListarBienMultaVehicular(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pCodMunicipalidad, pPlaca, pSecfinanciamiento, _
                                                                           pNroMotor, pCUCliente, pCodContrato)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    Public Function ObtenerDatosMulta(ByVal pstrPlaca As String, ByVal pintSecMulta As Integer) As String Implements IImpuestoVehicularNTX.ObtenerDatosMulta
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ObtenerDatosMulta(pstrPlaca, pintSecMulta)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene un registro de la multa vehicular
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 21/01/2013
    ''' </remarks>
    Public Function ObtenerDatosMultaConsulta(ByVal pEMultaVehicular As String) As String Implements IImpuestoVehicularNTX.ObtenerDatosMultaConsulta
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ObtenerDatosMultaConsulta(pEMultaVehicular)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function

    Public Function ListarLoteMultaVehicular(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pPlaca As String, _
                                    ByVal pCodMunicipal As String, _
                                    ByVal pTipo As Integer, _
                                    ByVal pNroLote As String) As String Implements IImpuestoVehicularNTX.ListarLoteMultaVehicular
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            'strResultado = objDImpuestoVehicularNTX.ListarLoteMultaVehicular(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pPlaca, pCodMunicipal, pTipo)
            strResultado = objDImpuestoVehicularNTX.ListarLoteMultaVehicular(pPageSize, pCurrentPage, pSortColumn, pSortOrder, _
                                                                             pPlaca, pCodMunicipal, pTipo, pNroLote)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    Public Function ListarEscalaInfraccionesMulta() As String Implements IImpuestoVehicularNTX.ListarEscalaInfraccionesMulta
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ListarEscalaInfraccionesMulta()

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    Public Function ListarImpuestoVehicularLiquidar(ByVal pPageSize As Integer, _
                               ByVal pCurrentPage As Integer, _
                               ByVal pSortColumn As String, _
                               ByVal pSortOrder As String, _
                               ByVal pCodLote As String) As String Implements IImpuestoVehicularNTX.ListarImpuestoVehicularLiquidar
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ListarImpuestoVehicularLiquidar(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pCodLote)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    Public Function ListarMultaVehicularLiquidar(ByVal pPageSize As Integer, _
                               ByVal pCurrentPage As Integer, _
                               ByVal pSortColumn As String, _
                               ByVal pSortOrder As String, _
                               ByVal pCodLote As String) As String Implements IImpuestoVehicularNTX.ListarMultaVehicularLiquidar
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ListarMultaVehicularLiquidar(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pCodLote)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    Public Function ListarMultaVehicularLiquidarTodo(ByVal pCodLote As String) As String Implements IImpuestoVehicularNTX.ListarMultaVehicularLiquidarTodo
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ListarMultaVehicularLiquidarTodo(pCodLote)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    Public Function ListarMultaVehicularReporteLiquidar(ByVal pCodigoImpuesto As String) As String Implements IImpuestoVehicularNTX.ListarMultaVehicularReporteLiquidar
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ListarMultaVehicularReporteLiquidar(pCodigoImpuesto)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function
    'Inicio JJM IBK
    Function GetImpuestoMultas(ByVal pNroLote As String) As String Implements IImpuestoVehicularNTX.GetImpuestoMultas
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String

        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.GetImpuestoMultas(pNroLote)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try

        Return strResultado
    End Function
    Function GetImpuestoVehicular(ByVal pNroLote As String) As String Implements IImpuestoVehicularNTX.GetImpuestoVehicular
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String

        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.GetImpuestoVehicular(pNroLote)
        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try

        Return strResultado
    End Function
    'Fin JJM IBK

    '===============================================================================
    'ALERTAS
    Public Function ListarAltertaImpuestoVehicular(ByVal pNroLote, _
                                                   ByVal pNroCheque) As String Implements IImpuestoVehicularNTX.ListarAltertaImpuestoVehicular
        Dim objDImpuestoVehicularNTX As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String
        Try
            objDImpuestoVehicularNTX = New DImpuestoVehicularNTX
            strResultado = objDImpuestoVehicularNTX.ListarAltertaImpuestoVehicular(pNroLote, pNroCheque)

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularNTX.Dispose()
        End Try
        Return strResultado
    End Function

    'Inicio IBK - AAE - 13/02/2013 - Se agrega función para obtener el lote
    Public Function CheckLote(ByVal pNroLote As String, ByVal pflag As String) As String Implements IImpuestoVehicularNTX.CheckLote

        Dim objDImpuestoVehicularTx As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String

        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularNTX
            If IsNumeric(pNroLote) Then
                strResultado = objDImpuestoVehicularTx.CheckLote(pNroLote, pflag)
            Else
                strResultado = "-1"
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return strResultado

    End Function

    Public Function ObtenerHeaderLote(ByVal pNroLote As String, ByVal pflag As String) As String Implements IImpuestoVehicularNTX.ObtenerHeaderLote

        Dim objDImpuestoVehicularTx As DImpuestoVehicularNTX = Nothing
        Dim strResultado As String

        Try
            objDImpuestoVehicularTx = New DImpuestoVehicularNTX
            If IsNumeric(pNroLote) Then
                strResultado = objDImpuestoVehicularTx.ObtenerHeaderLote(pNroLote, pflag)
            Else
                strResultado = objDImpuestoVehicularTx.ObtenerHeaderLote("0", pflag)
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objDImpuestoVehicularTx.Dispose()
            objDImpuestoVehicularTx = Nothing
        End Try

        Return strResultado

    End Function
    'Fin IBK
End Class
#End Region

