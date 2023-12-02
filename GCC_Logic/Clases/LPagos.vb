
Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LPagosTx
''' </summary>
''' <remarks>
''' Creado Por         : IBK - RPR
''' Fecha de Creación  : 19/12/2012
''' </remarks>
<Guid("9B13155E-1CF6-4647-9842-D6B3BC145D28") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LPagosTx")> _
Public Class LPagosTx
    Inherits ServicedComponent
    Implements IPagosTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LPagosTx"
#End Region

#Region "Metodos"


    ''' <summary>
    ''' Ingresa un Pago de Cuotas (CreditoRecuperacion, CreditoRecuperacionDetalle, CreditoRecuperacionComision)
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad serializada</param>
    ''' <returns>Dataset con valores de retorno</returns>
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    <AutoComplete(True)> _
     Public Function IngresarPagoCuotas(ByVal pECreditoRecuperacion As String) As String Implements IPagosTx.IngresarPagoCuotas
        Try
            Using oDPagosTx As New DPagosTx
                Return oDPagosTx.IngresarPagoCuotas(pECreditoRecuperacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Actualizar estado de Pago de Cuotas
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad serializada</param>
    ''' <returns>Booleano</returns>
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    <AutoComplete(True)> _
     Public Function ActualizarEstadoPagoCuotas(ByVal pECreditoRecuperacion As String) As Boolean Implements IPagosTx.ActualizarEstadoPagoCuotas
        Try
            Using oDPagosTx As New DPagosTx
                Return oDPagosTx.ActualizarEstadoPagoCuotas(pECreditoRecuperacion)
            End Using
        Catch ex As Exception
            Throw ex
            Return False
        End Try

    End Function

    <AutoComplete(True)> _
    Public Function ActualizarEstadoLiquidacion(ByVal pECreditoRecuperacion As String) As Boolean Implements IPagosTx.ActualizarEstadoLiquidacion
        Dim oDPagosTx As DPagosTx = Nothing
        Dim nbrRes As Boolean = False

        Try
            oDPagosTx = New DPagosTx
            nbrRes = oDPagosTx.ActualizarEstadoLiquidacion(pECreditoRecuperacion)
        Catch ex As Exception
            Throw ex
        Finally
            oDPagosTx.Dispose()
            oDPagosTx = Nothing
        End Try

        Return nbrRes

    End Function

    <AutoComplete(True)> _
    Public Function ProcesarLiquidacion(ByVal pECreditoRecuperacion As String) As String Implements IPagosTx.ProcesarLiquidacion
        Dim objDPagosTx As DPagosTx = Nothing
        Dim strRes As String = ""
        Try
            objDPagosTx = New DPagosTx
            strRes = objDPagosTx.ProcesarLiquidacion(pECreditoRecuperacion)
        Catch ex As Exception
            Throw ex
        Finally
            objDPagosTx.Dispose()
            objDPagosTx = Nothing
        End Try
        Return strRes
    End Function

    'Inicio JJM IBK
    <AutoComplete(True)> _
    Public Function InsertaConceptoDetalle(ByVal pEPagoConcepto As String) As Boolean Implements IPagosTx.InsertaConceptoDetalle
        Dim oDPagosTx As DPagosTx = Nothing
        Dim nbrRes As Boolean = False
        Try
            oDPagosTx = New DPagosTx
            nbrRes = oDPagosTx.InsertaConceptoDetalle(pEPagoConcepto)
        Catch ex As Exception
            Throw ex
        Finally
            oDPagosTx.Dispose()
            oDPagosTx = Nothing
        End Try

        Return nbrRes

    End Function

    <AutoComplete(True)> _
   Public Function ActualizaConceptoDetalle(ByVal pEPagoConcepto As String) As Boolean Implements IPagosTx.ActualizaConceptoDetalle
        Dim oDPagosTx As DPagosTx = Nothing
        Dim nbrRes As Boolean = False
        Try
            oDPagosTx = New DPagosTx
            nbrRes = oDPagosTx.ActualizaConceptoDetalle(pEPagoConcepto)
        Catch ex As Exception
            Throw ex
        Finally
            oDPagosTx.Dispose()
            oDPagosTx = Nothing
        End Try

        Return nbrRes

    End Function
    <AutoComplete(True)> _
    Public Function EliminaConceptoDetalle(ByVal pEPagoConcepto As String) As Boolean Implements IPagosTx.EliminaConceptoDetalle
        Dim oDPagosTx As DPagosTx = Nothing
        Dim nbrRes As Boolean = False
        Try
            oDPagosTx = New DPagosTx
            nbrRes = oDPagosTx.EliminaConceptoDetalle(pEPagoConcepto)
        Catch ex As Exception
            Throw ex
        Finally
            oDPagosTx.Dispose()
            oDPagosTx = Nothing
        End Try

        Return nbrRes

    End Function
    <AutoComplete(True)> _
    Public Function IngresarPagoConceptos(ByVal pECreditoRecuperacion As String) As String Implements IPagosTx.IngresarPagoConceptos
        Try
            Using oDPagosTx As New DPagosTx
                Return oDPagosTx.IngresarPagoConceptos(pECreditoRecuperacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    <AutoComplete(True)> _
  Public Function ActualizarTMPLiquidacion(ByVal pEGCC_Liquidacion As String) As String Implements IPagosTx.ActualizarTMPLiquidacion
        Try
            Using oDPagosTx As New DPagosTx
                Return oDPagosTx.ActualizarTMPLiquidacion(pEGCC_Liquidacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    <AutoComplete(True)> _
 Public Function ActualizarTMPLiquidacionAplicacion(ByVal pEGCC_Liquidacion As String) As String Implements IPagosTx.ActualizarTMPLiquidacionAplicacion
        Try
            Using oDPagosTx As New DPagosTx
                Return oDPagosTx.ActualizarTMPLiquidacionAplicacion(pEGCC_Liquidacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    <AutoComplete(True)> _
    Public Function TMPInsertarCronograma(ByVal pEGCC_Liquidacion As String) As Boolean Implements IPagosTx.TMPInsertarCronograma
        Try
            Using oDPagosTx As New DPagosTx
                Return oDPagosTx.TMPInsertarCronograma(pEGCC_Liquidacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'Fin IBK

#End Region

End Class

#End Region

#Region "Clase No Transaccional"

''' <summary>
''' Implementación de la clase LPagosNTx
''' </summary>
''' <remarks>
''' Creado Por         : IBK - RPR
''' Fecha de Creación  : 26/12/2012
''' </remarks>
<Guid("8485B0F5-6F04-4476-B94A-9B55DA60A26C") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LPagosNTx")> _
Public Class LPagosNTx
    Inherits ServicedComponent
    Implements IPagosNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LPagosNTx"
#End Region

#Region "Metodos"

    Public Function SeleccionarAplicacionComision(ByVal pECreditoRecuperacionComision As String) As Boolean Implements IPagosNTx.SeleccionarAplicacionComision

        Try
            Using objDPagosNTx As New DPagosNTx
                Return objDPagosNTx.SeleccionarAplicacionComision(pECreditoRecuperacionComision)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lista los Pagos de Cuotas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 26/12/2012
    ''' </remarks>
    Public Function ListadoPagoCuotas(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pEPagoCuotas As String) As String Implements IPagosNTx.ListadoPagoCuotas
        Try
            Using oDPagosNTx As New DPagosNTx
                Return oDPagosNTx.ListadoPagoCuotas(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pEPagoCuotas)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListadoLiquidaciones(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pEGCC_Liquidacion As String) As String Implements IPagosNTx.ListadoLiquidaciones
        Dim oDPagosNTx As New DPagosNTx
        Try
            Return oDPagosNTx.ListadoLiquidaciones(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pEGCC_Liquidacion)
        Catch ex As Exception
            Throw ex
        Finally
            oDPagosNTx.Dispose()
            oDPagosNTx = Nothing
        End Try
    End Function


    ''' <summary>
    ''' Obtiene un Pago de Cuotas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 26/12/2012
    ''' </remarks>
    Public Function ObtenerPagoCuotas(ByVal pECreditoRecuperacion As String) As String Implements IPagosNTx.ObtenerPagoCuotas
        Try
            Using oDPagosNTx As New DPagosNTx
                Return oDPagosNTx.ObtenerPagoCuotas(pECreditoRecuperacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerLiquidacion(ByVal pEGCC_Liquidacion As String) As String Implements IPagosNTx.ObtenerLiquidacion
        Dim oDPagosNTx As New DPagosNTx
        Try
            Return oDPagosNTx.ObtenerLiquidacion(pEGCC_Liquidacion)
        Catch ex As Exception
            Throw ex
        Finally
            oDPagosNTx.Dispose()
            oDPagosNTx = Nothing
        End Try
    End Function

    Public Function ObtenerCuotaAtrasadaComision(ByVal pEGCC_Liquidacion As String) As String Implements IPagosNTx.ObtenerCuotaAtrasadaComision
        Dim oDPagosNTx As New DPagosNTx
        Try
            Return oDPagosNTx.ObtenerCuotaAtrasadaComision(pEGCC_Liquidacion)
        Catch ex As Exception
            Throw ex
        Finally
            oDPagosNTx.Dispose()
            oDPagosNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un Pago de Cuotas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 26/12/2012
    ''' </remarks>
    Public Function ObtenerPagoCuotasTotales(ByVal pECreditoRecuperacion As String) As String Implements IPagosNTx.ObtenerPagoCuotasTotales
        Try
            Using oDPagosNTx As New DPagosNTx
                Return oDPagosNTx.ObtenerPagoCuotasTotales(pECreditoRecuperacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtener el detalle de cuotas pagadas en un CreditoRecuperacion
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 27/12/2012
    ''' </remarks>
    Public Function ObtenerDetalleCuotas(ByVal pECreditoRecuperacion As String) As String Implements IPagosNTx.ObtenerDetalleCuotas

        Try
            Using objDPagosNTx As New DPagosNTx
                Return objDPagosNTx.ObtenerDetalleCuotas(pECreditoRecuperacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtener las siguientes n cuotas a pagar en un credito
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado para la busqueda</param>
    ''' <param name="pNroCuotas">Numero de Cuotas a obtener</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 31/12/2012
    ''' </remarks>
    Public Function ObtenerProximasCuotas(ByVal pECreditoRecuperacion As String, ByVal pNroCuotas As Integer) As String Implements IPagosNTx.ObtenerProximasCuotas

        Try
            Using objDPagosNTx As New DPagosNTx
                Return objDPagosNTx.ObtenerProximasCuotas(pECreditoRecuperacion, pNroCuotas)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtener el detalle de comisiones pagadas en un CreditoRecuperacion
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 27/12/2012
    ''' </remarks>
    Public Function ObtenerDetalleComisiones(ByVal pECreditoRecuperacion As String) As String Implements IPagosNTx.ObtenerDetalleComisiones

        Try
            Using objDPagosNTx As New DPagosNTx
                Return objDPagosNTx.ObtenerDetalleComisiones(pECreditoRecuperacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Calcular las comisiones a cobrar por las proximas n cuotas seleccionadas
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 03/01/2012
    ''' </remarks>
    Public Function ObtenerProximasComisiones(ByVal pECreditoRecuperacion As String) As String Implements IPagosNTx.ObtenerProximasComisiones

        Try
            Using objDPagosNTx As New DPagosNTx
                Return objDPagosNTx.ObtenerProximasComisiones(pECreditoRecuperacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' Obtiene trama a enviar a Transactor para autorizar el pago de cuotas o conceptos en ventanilla con Nro de Autorizacion
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 09/01/2012
    ''' </remarks>
    Public Function ObtenerTramaAutorizacionPagosVentanilla(ByVal pECreditoRecuperacion As String, ByVal STATUS As String) As String Implements IPagosNTx.ObtenerTramaAutorizacionPagosVentanilla

        Try
            Using objDPagosNTx As New DPagosNTx
                Return objDPagosNTx.ObtenerTramaAutorizacionPagosVentanilla(pECreditoRecuperacion, STATUS)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function callProgramaHost(ByVal argsTrama As String, _
                                     ByVal argsUsuarioTld As String, _
                                     ByVal argsAgenciaTld As String, _
                                     ByVal argsPrograma As String, _
                                     ByVal argsFuncion As String, _
                                     ByRef argsMensaje As String, _
                                     ByRef argsTldDatosTran As String) As String Implements IPagosNTx.callProgramaHost
        Dim sFuncion As String = argsFuncion
        Dim iRes As Integer = -1
        Dim oObj As Object
        Dim sTldDatosTran As String
        Dim sResMensaje As String = String.Empty
        Dim sFechaActual As String
        Dim sHoraActual As String
        Dim sRet
        sFechaActual = Now.Year.ToString.PadLeft(4, "0") & Now.Month.ToString.PadLeft(2, "0") & Now.Day.ToString.PadLeft(2, "0")
        sHoraActual = Now.Hour.ToString.PadLeft(2, "0") & Now.Minute.ToString.PadLeft(2, "0") & Now.Second.ToString.PadLeft(2, "0")

        Try
            oObj = Microsoft.VisualBasic.CreateObject("sdaHtxnWior002.htxnWior002")

            Try
                oObj.asigna_CLogTerm("0")
                oObj.asigna_CFTerm(sFechaActual)
                oObj.asigna_CHTerm(sHoraActual)
                oObj.asigna_CAgencia(argsAgenciaTld)
                oObj.asigna_CSagencia("0")
                oObj.asigna_CUserId(argsUsuarioTld)
                oObj.asigna_CSupervid(" ")
                oObj.asigna_Gsecondt("N")
                oObj.asigna_GAutoriz("N")
                oObj.asigna_ClogExt("0000000")
                oObj.asigna_ClogTermExt("0000000")
                oObj.asigna_Gext("N")
                oObj.asigna_Gcas("N")
                oObj.asigna_Programa(argsPrograma)
                oObj.asigna_Data(argsTrama)
                iRes = oObj.fProcesa()

                If iRes = 0 Or iRes = -2 Then
                    sTldDatosTran = oObj.obtiene_TldDatosTran
                    If Trim(sTldDatosTran) <> "" Then
                        argsTldDatosTran = sTldDatosTran
                    Else
                        sResMensaje = "La Trama Volvió Vacía"
                        iRes = -5
                    End If
                Else
                    sResMensaje = "Error de llamada a la Rutina " + argsPrograma + "en la funcion " + sFuncion + ", devolvió " + CStr(iRes)
                    'Dim objLog As New LogErr
                    'objLog.toWritesol(sObj, "ERROR 0050 - " + sFuncion, 3, "", "Error de llamada a la Rutina " & argsPrograma & ", devolvió " & CStr(iRes), 0, 0)
                    'MsgBox("ERROR 0050 - " + sFuncion + "" + "Error de llamada a la Rutina " + argsPrograma + ", devolvió " + CStr(iRes))
                    'objLog = Nothing
                End If
            Catch ex As Exception
                'Dim objLog As New LogErr
                'objLog.toWritesol(sObj, "ERROR 0051 - " + sFuncion, 3, ex.Source, ex.Message, 0, 0)
                'objLog = Nothing
                'MsgBox("ERROR 0051 - " + sFuncion + ex.Source + ex.Message)
                sResMensaje = "Error de llamada a la Rutina " + argsPrograma + "en la funcion " + sFuncion + ". Falló la Asignación de algún parámetro, para mayor detalle verificar el Log"
            End Try
        Catch ex As Exception
            'Dim objLog As New LogErr
            'objLog.toWritesol(sObj, "ERROR 0052 - " + sFuncion, 3, ex.Source, ex.Message, 0, 0)

            'MsgBox("ERROR 0052 - " + sFuncion + ex.Source + ex.Message)
            'objLog = Nothing
            sResMensaje = "Error de llamada a la Rutina " + argsPrograma + "en la funcion " + sFuncion + ". La aplicación no pudo instanciar el componente, verificar el Log"
        End Try
        oObj = Nothing
        sRet = sResMensaje
        If iRes = 0 Or iRes = -2 Then
            Return "0|" + sRet
        Else
            Return "1|" + sRet
        End If
    End Function

    ' Fin IBK
    'Inicion IBK JJM
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pEPagoConcepto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListadoPagoConcepto(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pEPagoConcepto As String) As String Implements IPagosNTx.ListadoPagoConcepto
        Try
            Using oDPagosNTx As New DPagosNTx
                Return oDPagosNTx.ListadoPagoConcepto(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pEPagoConcepto)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ListadoPagoConceptoDetalle(ByVal pEPagoConcepto As String) As String Implements IPagosNTx.ListadoPagoConceptoDetalle
        Try
            Using oDPagosNTx As New DPagosNTx
                Return oDPagosNTx.ListadoPagoConceptoDetalle(pEPagoConcepto)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ListadoPagoConceptoxNumeroSecuencia(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEPagoConcepto As String) As String Implements IPagosNTx.ListadoPagoConceptoxNumeroSecuencia
        Try
            Using oDPagosNTx As New DPagosNTx
                Return oDPagosNTx.ListadoPagoConceptoxNumeroSecuencia(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pEPagoConcepto)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerConceptoEspecifico(ByVal pEPagoConcepto As String) As String Implements IPagosNTx.ObtenerConceptoEspecifico
        Try
            Using oDPagosNTx As New DPagosNTx
                Return oDPagosNTx.ObtenerConceptoEspecifico(pEPagoConcepto)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ListadoPagoConceptoxNumeroSecuenciaTemporal(ByVal pPageSize As Integer, _
                                               ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, _
                                               ByVal pSortOrder As String, _
                                               ByVal pEPagoConcepto As String) As String Implements IPagosNTx.ListadoPagoConceptoxNumeroSecuenciaTemporal
        Try
            Using oDPagosNTx As New DPagosNTx
                Return oDPagosNTx.ListadoPagoConceptoxNumeroSecuenciaTemporal(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pEPagoConcepto)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerCreditoConsulta(ByVal pPageSize As Integer, _
                                               ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, _
                                               ByVal pSortOrder As String, _
                                                ByVal pENroDocumento As String, ByVal pERazonSocial As String, _
                                                ByVal pETipoDocumento As String) As String Implements IPagosNTx.ObtenerCreditoConsulta
        Try
            Using oDPagosNTx As New DPagosNTx
                Return oDPagosNTx.ObtenerCreditoConsulta(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pENroDocumento, pERazonSocial, pETipoDocumento)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ' Fin IBK
    Function GetDatosCarta(ByVal pNroLote As String) As String Implements IPagosNTx.GetDatosCarta
        Dim oDPagosNTx As DPagosNTx = Nothing
        Dim strResultado As String

        Try
            oDPagosNTx = New DPagosNTx
            strResultado = oDPagosNTx.GetDatosCarta(pNroLote)
        Catch ex As Exception
            Throw ex
        Finally
            oDPagosNTx.Dispose()
        End Try

        Return strResultado
    End Function
    Function GetDatosCartaExcel(ByVal pNroLote As String) As String Implements IPagosNTx.GetDatosCartaExcel
        Dim oDPagosNTx As DPagosNTx = Nothing
        Dim strResultado As String

        Try
            oDPagosNTx = New DPagosNTx
            strResultado = oDPagosNTx.GetDatosCartaExcel(pNroLote)
        Catch ex As Exception
            Throw ex
        Finally
            oDPagosNTx.Dispose()
        End Try

        Return strResultado
    End Function
#End Region

End Class

#End Region

