Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

#Region "Clase No Transaccional"

Public Class LPagosNTx
    Inherits LUtilitario

    Public Function SeleccionarAplicacionComision(ByVal pECreditoRecuperacionComision As String) As Boolean
        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.SeleccionarAplicacionComision(pECreditoRecuperacionComision)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosNTx = Nothing
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
                                        ByVal pEPagoCuotas As String) As String

        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ListadoPagoCuotas(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pEPagoCuotas)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListadoLiquidaciones(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pEGCC_Liquidacion As String) As String

        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ListadoLiquidaciones(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pEGCC_Liquidacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosNTx.dispose()
            objLPagosNTx = Nothing
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
    Public Function ObtenerPagoCuotas(ByVal pECreditoRecuperacion As String) As String

        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ObtenerPagoCuotas(pECreditoRecuperacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerLiquidacion(ByVal pEGCC_Liquidacion As String) As String

        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ObtenerLiquidacion(pEGCC_Liquidacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosNTx.Dispose()
            objLPagosNTx = Nothing
        End Try

    End Function

    ''' <summary>
    ''' Obtiene Totales de un Pago de Cuotas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 29/12/2012
    ''' </remarks>
    Public Function ObtenerPagoCuotasTotales(ByVal pECreditoRecuperacion As String) As String

        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ObtenerPagoCuotasTotales(pECreditoRecuperacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtener el detalle de cuotas pagadas en un CreditoRecuperacion
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 27/12/2012
    ''' </remarks>
    Public Function ObtenerDetalleCuotas(ByVal pECreditoRecuperacion As String) As String
        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ObtenerDetalleCuotas(pECreditoRecuperacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosNTx.Dispose()
            objLPagosNTx = Nothing
        End Try
    End Function

    Public Function ObtenerCuotaAtrasadaComision(ByVal pEGCC_Liquidacion As String) As String
        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ObtenerCuotaAtrasadaComision(pEGCC_Liquidacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosNTx.Dispose()
            objLPagosNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtener las n proximas cuotas a pagar de OperacionActivaCalendario
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <param name="pintNroCuotas">Numero de cuotas a obtener</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 31/12/2012
    ''' </remarks>
    Public Function ObtenerProximasCuotas(ByVal pECreditoRecuperacion As String, ByVal pintNroCuotas As Integer) As String
        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ObtenerProximasCuotas(pECreditoRecuperacion, pintNroCuotas)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosNTx = Nothing
        End Try
    End Function


    ''' <summary>
    ''' Obtener el detalle de comisiones pagadas en un CreditoRecuperacion
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 27/12/2012
    ''' </remarks>
    Public Function ObtenerDetalleComisiones(ByVal pECreditoRecuperacion As String) As String
        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ObtenerDetalleComisiones(pECreditoRecuperacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Calcular las comisiones a cobrar por las proximas n cuotas seleccionadas
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 03/01/2012
    ''' </remarks>
    Public Function ObtenerProximasComisiones(ByVal pECreditoRecuperacion As String) As String
        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ObtenerProximasComisiones(pECreditoRecuperacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene trama a enviar a Transactor para autorizar el pago de cuotas o conceptos en ventanilla con Nro de Autorizacion
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 10/01/2012
    ''' </remarks>
    Public Function ObtenerTramaAutorizacionPagosVentanilla(ByVal pECreditoRecuperacion As String, ByVal STATUS As String) As String
        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ObtenerTramaAutorizacionPagosVentanilla(pECreditoRecuperacion, STATUS)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosNTx = Nothing
        End Try
    End Function


    Public Function callProgramaHost(ByVal argsTrama As String, _
                                     ByVal argsUsuarioTld As String, _
                                     ByVal argsAgenciaTld As String, _
                                     ByVal argsPrograma As String, _
                                     ByVal argsFuncion As String, _
                                     ByRef argsMensaje As String, _
                                     ByRef argsTldDatosTran As String) As Boolean

        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Dim resHost As String
        Dim arrResHost As String()
        Dim strRes As String
        Dim strDatos As String
        Try
            resHost = objLInstruccionDesembolsoNTx.callProgramaHost(argsTrama, argsUsuarioTld, argsAgenciaTld, argsPrograma, argsFuncion, argsMensaje, argsTldDatosTran)
            arrResHost = resHost.Split("|")
            strRes = arrResHost(0)
            strDatos = resHost.Substring(2)
            If strRes = "0" Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
        'Dim sFuncion As String = argsFuncion
        'Dim iRes As Integer = -1
        'Dim oObj As Object
        'Dim sTldDatosTran As String
        'Dim sResMensaje As String = String.Empty
        'Dim sFechaActual As String
        'Dim sHoraActual As String

        'sFechaActual = Now.Year.ToString.PadLeft(4, "0") & Now.Month.ToString.PadLeft(2, "0") & Now.Day.ToString.PadLeft(2, "0")
        'sHoraActual = Now.Hour.ToString.PadLeft(2, "0") & Now.Minute.ToString.PadLeft(2, "0") & Now.Second.ToString.PadLeft(2, "0")

        'Try
        '    oObj = Microsoft.VisualBasic.CreateObject("sdaHtxnWior002.htxnWior002")

        '    Try
        '        oObj.asigna_CLogTerm("0")
        '        oObj.asigna_CFTerm(sFechaActual)
        '        oObj.asigna_CHTerm(sHoraActual)
        '        oObj.asigna_CAgencia(argsAgenciaTld)
        '        oObj.asigna_CSagencia("0")
        '        oObj.asigna_CUserId(argsUsuarioTld)
        '        oObj.asigna_CSupervid(" ")
        '        oObj.asigna_Gsecondt("N")
        '        oObj.asigna_GAutoriz("N")
        '        oObj.asigna_ClogExt("0000000")
        '        oObj.asigna_ClogTermExt("0000000")
        '        oObj.asigna_Gext("N")
        '        oObj.asigna_Gcas("N")
        '        oObj.asigna_Programa(argsPrograma)
        '        oObj.asigna_Data(argsTrama)
        '        iRes = oObj.fProcesa()

        '        If iRes = 0 Or iRes = -2 Then
        '            sTldDatosTran = oObj.obtiene_TldDatosTran
        '            If Trim(sTldDatosTran) <> "" Then
        '                argsTldDatosTran = sTldDatosTran
        '            Else
        '                sResMensaje = "La Trama Volvió Vacía"
        '                iRes = -5
        '            End If
        '        Else
        '            sResMensaje = "Error de llamada a la Rutina " + argsPrograma + "en la funcion " + sFuncion + ", devolvió " + CStr(iRes)
        '            'Dim objLog As New LogErr
        '            'objLog.toWritesol(sObj, "ERROR 0050 - " + sFuncion, 3, "", "Error de llamada a la Rutina " & argsPrograma & ", devolvió " & CStr(iRes), 0, 0)
        '            'MsgBox("ERROR 0050 - " + sFuncion + "" + "Error de llamada a la Rutina " + argsPrograma + ", devolvió " + CStr(iRes))
        '            'objLog = Nothing
        '        End If
        '    Catch ex As Exception
        '        'Dim objLog As New LogErr
        '        'objLog.toWritesol(sObj, "ERROR 0051 - " + sFuncion, 3, ex.Source, ex.Message, 0, 0)
        '        'objLog = Nothing
        '        'MsgBox("ERROR 0051 - " + sFuncion + ex.Source + ex.Message)
        '        sResMensaje = "Error de llamada a la Rutina " + argsPrograma + "en la funcion " + sFuncion + ". Falló la Asignación de algún parámetro, para mayor detalle verificar el Log"
        '    End Try
        'Catch ex As Exception
        '    'Dim objLog As New LogErr
        '    'objLog.toWritesol(sObj, "ERROR 0052 - " + sFuncion, 3, ex.Source, ex.Message, 0, 0)

        '    'MsgBox("ERROR 0052 - " + sFuncion + ex.Source + ex.Message)
        '    'objLog = Nothing
        '    sResMensaje = "Error de llamada a la Rutina " + argsPrograma + "en la funcion " + sFuncion + ". La aplicación no pudo instanciar el componente, verificar el Log"
        'End Try
        'oObj = Nothing
        'argsMensaje = sResMensaje
        'If iRes = 0 Or iRes = -2 Then
        '    Return True
        'Else
        '    Return False
        'End If
    End Function


    Public Function ListadoPagoConcepto(ByVal pPageSize As Integer, _
                                       ByVal pCurrentPage As Integer, _
                                       ByVal pSortColumn As String, _
                                       ByVal pSortOrder As String, _
                                       ByVal pEPagoConcepto As String) As String

        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ListadoPagoConcepto(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pEPagoConcepto)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ListadoPagoConceptoDetalle(ByVal pEPagoConcepto As String) As String

        Dim objLPagosNTx As New Object
        objLPagosNTx = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ListadoPagoConceptoDetalle(pEPagoConcepto)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ListadoPagoConceptoxNumeroSecuencia(ByVal pPageSize As Integer, _
                                      ByVal pCurrentPage As Integer, _
                                      ByVal pSortColumn As String, _
                                      ByVal pSortOrder As String, _
                                      ByVal pEPagoConcepto As String) As String

        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ListadoPagoConceptoxNumeroSecuencia(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pEPagoConcepto)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerConceptoEspecifico(ByVal pEPagoConcepto As String) As String
        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ObtenerConceptoEspecifico(pEPagoConcepto)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ListadoPagoConceptoxNumeroSecuenciaTemporal(ByVal pPageSize As Integer, _
                                      ByVal pCurrentPage As Integer, _
                                      ByVal pSortColumn As String, _
                                      ByVal pSortOrder As String, _
                                      ByVal pEPagoConcepto As String) As String

        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ListadoPagoConceptoxNumeroSecuenciaTemporal(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pEPagoConcepto)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerCreditoConsulta(ByVal pPageSize As Integer, _
                                      ByVal pCurrentPage As Integer, _
                                      ByVal pSortColumn As String, _
                                      ByVal pSortOrder As String, _
                                      ByVal pENroDocumento As String, ByVal pERazonSocial As String, _
                                                ByVal pETipoDocumento As String) As String

        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosNTx.ObtenerCreditoConsulta(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pENroDocumento, pERazonSocial, pETipoDocumento)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDatosCarta(ByVal pNroLote As String) As String
        Dim objLPagosTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosTx.GetDatosCarta(pNroLote)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosTx = Nothing
        End Try
    End Function
    Public Function GetDatosCartaExcel(ByVal pNroLote As String) As String
        Dim objLPagosTx As Object = CreateObject("GCC.Logic.LPagosNTx")
        Try
            Return objLPagosTx.GetDatosCartaExcel(pNroLote)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosTx = Nothing
        End Try
    End Function
End Class

#End Region

#Region "Clase Transaccional"

Public Class LPagosTx
    Inherits LUtilitario

    ''' <summary>
    ''' Ingresar Pago de Cuotas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    Public Function IngresarPagoCuotas(ByVal pECreditoRecuperacion As String) As String

        Dim objLPagosTx As Object = CreateObject("GCC.Logic.LPagosTx")
        Try
            Return objLPagosTx.IngresarPagoCuotas(pECreditoRecuperacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosTx.dispose()
            objLPagosTx = Nothing
        End Try
    End Function

    Public Function TMPInsertarCronograma(ByVal pECreditoRecuperacion As String) As Boolean

        Dim objLPagosTx As Object = CreateObject("GCC.Logic.LPagosTx")
        Try
            Return objLPagosTx.TMPInsertarCronograma(pECreditoRecuperacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosTx.dispose()
            objLPagosTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Actualizar Estado Pago de Cuotas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    Public Function ActualizarEstadoPagoCuotas(ByVal pECreditoRecuperacion As String) As Boolean

        Dim objLPagosTx As Object = CreateObject("GCC.Logic.LPagosTx")
        Try
            Return objLPagosTx.ActualizarEstadoPagoCuotas(pECreditoRecuperacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosTx.dispose()
            objLPagosTx = Nothing
        End Try
    End Function

    Public Function ProcesarLiquidacion(ByVal pEGCC_Liquidacion As String) As String
        Dim objLPagosNTx As Object = CreateObject("GCC.Logic.LPagosTx")
        Try
            Return objLPagosNTx.ProcesarLiquidacion(pEGCC_Liquidacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosNTx.Dispose()
            objLPagosNTx = Nothing
        End Try
    End Function

    Public Function ActualizarEstadoLiquidacion(ByVal pEGCC_Liquidacion As String) As Boolean

        Dim objLPagosTx As Object = CreateObject("GCC.Logic.LPagosTx")
        Try
            Return objLPagosTx.ActualizarEstadoLiquidacion(pEGCC_Liquidacion)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosTx.dispose()
            objLPagosTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Consumir funciones de NPCli.dll para llamadas a Host
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 10/01/2012
    ''' </remarks>
    Public Function TransaccionGINA(ByVal strTramaIn As String, ByRef strTramaOut As String) As Boolean
        Dim objLHostTx As Object = CreateObject("GCC.Logic.LHostTx")
        Try
            Return objLHostTx.TransaccionGINA(strTramaIn, strTramaOut)
        Catch ex As Exception
            Throw ex
        Finally
            objLHostTx.dispose()
            objLHostTx = Nothing
        End Try
    End Function

    Public Function ActualizaConceptoDetalleTemporal(ByVal pEPagoConcepto As String) As Boolean
        Dim objLPagosTX As Object = CreateObject("GCC.Logic.LPagosTx")
        Try
            Return objLPagosTX.ActualizaConceptoDetalle(pEPagoConcepto)
        Catch ex As Exception
            Throw ex
        Finally
            objLPagosTX.dispose()
            objLPagosTX = Nothing
        End Try
    End Function

    Public Function GrabaConceptoDetalleTemporal(ByVal pEPagoConcepto As String) As Boolean
        Dim objLPagosTX As Object = CreateObject("GCC.Logic.LPagosTx")
        Try
            Return objLPagosTX.InsertaConceptoDetalle(pEPagoConcepto)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function EliminaPagoConcepto(ByVal pEPagoConcepto As String) As Boolean
        Dim objLPagosTX As Object = CreateObject("GCC.Logic.LPagosTx")
        Try
            Return objLPagosTX.EliminaConceptoDetalle(pEPagoConcepto)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
 
    Public Function IngresarPagoConceptos(ByVal pECreditoRecuperacion As String) As String

        Dim objLPagosTx As Object = CreateObject("GCC.Logic.LPagosTx")
        Try
            Return objLPagosTx.IngresarPagoConceptos(pECreditoRecuperacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ActualizarTMPLiquidacion(ByVal pEGCC_Liquidacion As String) As String

        Dim objLPagosTx As Object = CreateObject("GCC.Logic.LPagosTx")
        Try
            Return objLPagosTx.ActualizarTMPLiquidacion(pEGCC_Liquidacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ActualizarTMPLiquidacionAplicacion(ByVal pEGCC_Liquidacion As String) As String

        Dim objLPagosTx As Object = CreateObject("GCC.Logic.LPagosTx")
        Try
            Return objLPagosTx.ActualizarTMPLiquidacionAplicacion(pEGCC_Liquidacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

 
End Class

#End Region