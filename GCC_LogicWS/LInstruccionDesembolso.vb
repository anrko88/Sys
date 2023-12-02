Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

#Region "Clase No Transaccional"

Public Class LInstruccionDesembolsoNTx
    Inherits LUtilitario

    ''' <summary>
    ''' Lista las Instruccion de Desembolso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function ListadoInsDesembolso(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pEInsDesembolso As String) As String

        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.ListadoInsDesembolso(pPageSize, _
                                                   pCurrentPage, _
                                                   pSortColumn, _
                                                   pSortOrder, _
                                                   pEInsDesembolso)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lista las Instruccion de Desembolso Agrupacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function ListadoInsDesembolsoAgrupacion(ByVal pEInsDesembolsoAgrupacion As String) As String

        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.ListadoInsDesembolsoAgrupacion(pEInsDesembolsoAgrupacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="strEGInstruccionDesembolso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidaEjecucionInstruccion(ByVal strEGInstruccionDesembolso As String) As String

        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.ValidaEjecucionInstruccion(strEGInstruccionDesembolso)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lista las Documentos por agrupacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function ListadoInsDesembolsoDocAgrupacion(ByVal pEInsDesembolsoAgrupacion As String) As String

        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.ListadoInsDesembolsoDocAgrupacion(pEInsDesembolsoAgrupacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lista las Instruccion de Desembolso Cargo/Abono
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function ListadoInsDesembolsoCargoAbono(ByVal pEInsDesembolsoAgrupacion As String) As String

        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.ListadoInsDesembolsoCargoAbono(pEInsDesembolsoAgrupacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lista las Medios de Pago de la ID
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function ListadoInsDesembolsoMedioPago(ByVal pEInsDesembolsoPago As String) As String

        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.ListadoInsDesembolsoMedioPago(pEInsDesembolsoPago)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ListadoInsDesembolsoMedioPagoGet(ByVal pEInsDesembolsoPago As String) As String

        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.ListadoInsDesembolsoMedioPagoGet(pEInsDesembolsoPago)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/10/2012
    ''' </remarks>
    Public Function ListadoInstruccionDesembolsoDoc(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pEInstruccionDesembolsoDoc As String) As String

        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.ListadoInstruccionDesembolsoDoc(pPageSize, _
                                                                               pCurrentPage, _
                                                                               pSortColumn, _
                                                                               pSortOrder, _
                                                                               pEInstruccionDesembolsoDoc)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el InstruccionDesembolsoDoc de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <param name="pEInstruccionDesembolsoDoc">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 21/10/2012 
    ''' </remarks>
    Public Function ObtenerInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As String

        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.ObtenerInstruccionDesembolsoDoc(pEInstruccionDesembolsoDoc)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerWIO(ByVal strNroContrato As String, ByVal strCodInsDesembolso As String) As String
        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.obtenerWIO(strNroContrato, strCodInsDesembolso)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Consulta el Estado de WIO
    ''' </summary>
    ''' <param name="strNroContrato"></param>
    ''' <param name="strCodInsDesembolso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConsultaEstadoWIO(ByVal strNroContrato As String, ByVal strCodInsDesembolso As String, ByVal pstrURL As String, ByVal strWIO As String, ByVal strArgProdWIO As String, ByVal strArgPasoWIO As String) As Boolean
        'Inicio IBK - AAE
        Dim ds As DataSet
        Dim strRef As String
        Dim strRta As String
        Dim dt As DataTable
        Dim strNrWIO As String
        Dim blnEncontre As Boolean
        Dim nbrWio As Int64
        Dim pnbrWio As Int64
        Dim wsDesembolso As wsWIODesembolso.wiodesembolso

        Try
            wsDesembolso = New wsWIODesembolso.wiodesembolso()
            If strWIO.Trim() <> "" Then
                Try
                    wsDesembolso.Url = pstrURL

                    pnbrWio = Convert.ToInt64(strWIO)
                    'Obtengo los WIOs que estan en control de procesos
                    strRta = wsDesembolso.ListaOperacionesxPaso(strArgProdWIO, strArgPasoWIO, ds, strRef)
                    dt = ds.Tables(0)
                    blnEncontre = False
                    'Recorro las filas
                    For Each dr As DataRow In dt.Rows()
                        strNrWIO = dr("CodSolicitud").ToString.Trim
                        nbrWio = Convert.ToInt64(strNrWIO)
                        If nbrWio = pnbrWio Then
                            blnEncontre = True
                        End If
                    Next
                    Return blnEncontre
                Catch ex As Exception
                    Throw New Exception("Excepcion obteniendo el estado del WIO:" + strRef + " - " + ex.ToString)
                Finally
                    wsDesembolso.Dispose()
                End Try
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New Exception("Excepcion obteniendo el estado del WIO:" + strRef + " - " + ex.ToString)
        End Try

        'Return True
        'Fin IBK
    End Function

    Public Function finalizaWIO(ByVal pCodSolicitudCredito As String, ByVal pEInsDesembolso As String, ByVal pstrURL As String, ByVal strWIO As String, ByVal strArgProdWIO As String, ByVal strArgPasoWIO As String, ByVal strUser As String, ByVal strNom As String) As String
        Dim ds As DataSet
        Dim strRef As String
        Dim strRef2 As String
        Dim strRet As String
        Dim blnRes As Boolean
        Dim strRta As String
        Dim dt As DataTable
        Dim strNrWIO As String
        Dim nbrNroWio As Int64
        Dim nbrNroInc As Int64
        Dim pnbrWIO As Int64
        Dim nbrWIOAux As Int64
        Dim blnEncontre As Boolean
        Dim wsDesembolso As New wsWIODesembolso.wiodesembolso()
        wsDesembolso.Url = pstrURL
        Try
            'Obtengo los WIOs que estan en control de procesos
            strRta = wsDesembolso.ListaOperacionesxPaso(strArgProdWIO, strArgPasoWIO, ds, strRef)
            dt = ds.Tables(0)
            blnEncontre = False
            'Recorro las filas
            pnbrWIO = Convert.ToInt64(strWIO)
            For Each dr As DataRow In dt.Rows()
                strNrWIO = dr("CodSolicitud").ToString.Trim
                nbrWIOAux = Convert.ToInt64(strNrWIO)
                If nbrWIOAux = pnbrWIO Then
                    nbrNroWio = Convert.ToInt64(strNrWIO)
                    nbrNroInc = Convert.ToInt64(dr("NumIncidente").ToString.Trim)
                End If
            Next
            'llamo a la operación que actuliza el estado
            blnRes = wsDesembolso.Desembolsar(pCodSolicitudCredito, Convert.ToInt32(strArgProdWIO), nbrNroWio, nbrNroInc, strUser, strNom, Date.Now, strRef2)

            If blnRes Then
                strRet = "0|Desembolso de WIO correcto"
            Else
                strRet = "1|Desembolso de WIO incorrecto: " + strRef2
            End If
            Return strRet
        Catch ex As Exception
            Return "1|Excepcion actualizando el estado del WIO:" + strRef + " - " + strRef2 + " - " + ex.ToString
        Finally
            wsDesembolso.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' Consulta el Estado de WIO
    ''' </summary>
    ''' <param name="strNroContrato"></param>
    ''' <param name="strCodInsDesembolso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConsultaAnulacionWIO(ByVal pstrURL As String, ByVal strWIO As String) As Boolean
        'Inicio IBK - AAE

        Dim strRef As String
        Dim strEstado As String
        Dim blnRta As Boolean
        Dim blnRetorno As Boolean = False

        Dim wsDesembolso As wsWIODesembolso.wiodesembolso

        Try
            wsDesembolso = New wsWIODesembolso.wiodesembolso()
            If strWIO.Trim() <> "" Then
                Try
                    wsDesembolso.Url = pstrURL


                    'Obtengo los WIOs que estan en control de procesos
                    blnRta = wsDesembolso.EstadoxNumeroInstruccion(strWIO.Trim(), strEstado, strRef)
                    If (blnRta = True) Then
                        If strEstado.Trim() = "" Or strEstado = "Anulada" Then
                            blnRetorno = True
                        Else
                            blnRetorno = False
                        End If
                    Else
                        Throw New Exception(strRef)
                    End If
                    Return blnRetorno
                Catch ex As Exception
                    Throw New Exception("Excepcion obteniendo el estado del WIO:" + strRef + " - " + ex.ToString)
                Finally
                    wsDesembolso.Dispose()
                End Try
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New Exception("Excepcion obteniendo el estado del WIO:" + strRef + " - " + ex.ToString)
        End Try

        'Return True
        'Fin IBK
    End Function

    'Inicio IBK - AAE - Agrego funcion que obtiene los cargos que son abono en cuenta para validar el saldo
    Public Function getCargosCuentaInsDes(ByVal strNroContrato As String, ByVal strCodInsDesembolso As String) As String

        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.getCargosCuentaInsDes(strNroContrato, strCodInsDesembolso)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function obtenerContablesMedioPago(ByVal strCodMedioPago As String) As String

        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.obtenerContablesMedioPago(strCodMedioPago)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function obtenerContablesSUNAT(ByVal strCodTipoAgrupacion As String) As String

        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.obtenerContablesSUNAT(strCodTipoAgrupacion)
        Catch ex As Exception
            Throw ex
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

    'AAE - funcion que obtiene los medios de pago para hacer el desembolso
    Public Function ListadoInsDesembolsoMedioPago2(ByVal pEInsDesembolsoPago As String) As String

        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.ListadoInsDesembolsoMedioPago2(pEInsDesembolsoPago)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'AAE - funcion que obtiene si una agrupación de facturas tiene notas de creditos
    Public Function TieneNotasCredito(ByVal pstrCodSolicitudCredito As String, ByVal pstrCodInstruccionDesembolso As String, ByVal pstrCodAgrupacion As String, ByVal pstrCodCorrelativo As String) As Boolean
        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.TieneNotasCredito(pstrCodSolicitudCredito, pstrCodInstruccionDesembolso, pstrCodAgrupacion, pstrCodCorrelativo)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function obtenerInfoProveedor(ByVal codProveedor As String) As String
        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.obtenerInfoProveedor(codProveedor)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListadoInsDesembolsoTotales(ByVal pCodContrato As String, ByVal pInsDesembolsoPago As String) As String
        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.ListadoInsDesembolsoTotales(pCodContrato, pInsDesembolsoPago)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Fin IBK

    'Inicio IBK - AAE - Actiavación leasing parcial
    Public Function ListadoInsDesembolsoActParcial(ByVal pEInsDesembolso As String) As String
        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.ListadoInsDesembolsoActParcial(pEInsDesembolso)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListaDesembolsos(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pstrNroContrato As String, _
                                        ByVal pstrCodInstDesembolso As String) As String

        Dim objLDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLDesembolsoNTx.ListaDesembolsos(pPageSize, _
                                                       pCurrentPage, _
                                                       pSortColumn, _
                                                       pSortOrder, _
                                                       pstrNroContrato, _
                                                       pstrCodInstDesembolso)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CronogramaActivacionGet(ByVal pstrNroContrato As String, _
                                                      ByVal pstrInstruccionDesembolso As String _
                                                      ) As String

        Dim objLDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLDesembolsoNTx.CronogramaActivacionGet(pstrNroContrato, pstrInstruccionDesembolso)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ValidaEjecucionInstruccionActParcial(ByVal strEGInstruccionDesembolso As String, ByVal strFlagCheckPrecuota As String) As String

        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.ValidaEjecucionInstruccionActParcial(strEGInstruccionDesembolso, strFlagCheckPrecuota)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ReporteLeasingEnProceso(ByVal FecDesIni As String, ByVal FecDesFin As String, ByVal Moneda As String, ByVal CodSolCredito As String, ByVal Flag As String) As String

        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Try
            Return objLInstruccionDesembolsoNTx.ReporteLeasingEnProceso(FecDesIni, FecDesFin, Moneda, CodSolCredito, Flag)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Fin IBK

    ''' <summary>
    ''' Lista todos los desembolsos mensuales
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <param name="pFechaTermino">Fecha de término de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 22/01/2013 05:41:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoDesembolsoMensualReporte(ByVal pFechaInicio As String, _
                                                        ByVal pFechaTermino As String) As String
        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Dim strResultado As String = String.Empty

        Try
            strResultado = objLInstruccionDesembolsoNTx.fobjListadoDesembolsoMensualReporte(pFechaInicio, _
                                                                                           pFechaTermino)
        Catch ex As Exception
            Throw ex
        Finally
            objLInstruccionDesembolsoNTx = Nothing
        End Try

        Return strResultado
    End Function
    Public Function ListaAgrupacionVoucher(ByVal pstrNroContrato As String, ByVal pstrNroInstruccion As String, ByVal pstrCorrelativo As String) As String
        Dim objLInstruccionDesembolsoNTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoNTx")
        Dim strResultado As String = String.Empty

        Try
            strResultado = objLInstruccionDesembolsoNTx.ListaAgrupacionVoucher(pstrNroContrato, pstrNroInstruccion, pstrCorrelativo)
        Catch ex As Exception
            Throw ex
        Finally
            objLInstruccionDesembolsoNTx = Nothing
        End Try

        Return strResultado
    End Function

#Region "Ejecución Desembolso"

    '-----------------------------------------------------------------------------
    'Nombre             : fstrConsultarCuenta
    'Objetivo           : la cuenta validada y los datos de la misma
    'Parámetros         : pstrTrama : Trama    
    'Creado Por         : TSF - JRC
    'Fecha de Creación  : 09/10/2012 
    '-----------------------------------------------------------------------------
    Public Function fstrConsultarDesembolso(ByVal pstrTrama As String, ByVal pstrUrlws As String, ByRef strCodRetorno As String, ByRef strResultado As String) As String
        Dim wsFCD004 As New bseFCDO004.bseFCDO004
        Dim strRes As String
        wsFCD004.Url = pstrUrlws
        Try
            strRes = wsFCD004.fConsultarDesembolso(pstrTrama)
            strResultado = strRes
            If strRes.Substring(0, 1) = "1" Then
                strCodRetorno = "00"
            Else
                strCodRetorno = "10"
            End If
        Catch ex As Exception
            strResultado = ex.ToString.Substring(0, 254)
            strCodRetorno = "10"
            strRes = "0|" + ex.ToString
        Finally
            wsFCD004.Dispose()
        End Try
        Return strRes
    End Function

#End Region


End Class

#End Region

#Region "Clase Transaccional"

Public Class LInstruccionDesembolsoTx
    Inherits LUtilitario

    ''' <summary>
    ''' Insertar Instruccion Desembolso
    ''' </summary>
    ''' <param name="pEInsDesembolso">Entidad serializada</param>    
    ''' <returns>Devuelve codigo de Instruccion de Desembolso</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 27/09/2012
    ''' </remarks>
    Public Function InsertarInsDesembolso(ByVal pEInsDesembolso As String, ByVal pESolicitudcredito As String) As String
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.InsertarInsDesembolso(pEInsDesembolso, pESolicitudcredito)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Gestion de cambios de una InstruccionDesembolso
    ''' </summary>
    ''' <param name="pEInsDesembolso">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function GestionInsDesembolso(ByVal pEInsDesembolso As String) As String
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.GestionInsDesembolso(pEInsDesembolso)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Gestion de cambios de una InstruccionDesembolso
    ''' </summary>
    ''' <param name="pEInsDesembolso">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function InsertarInsDesembolsoAgrupacion(ByVal pEInsDesembolsoAgrupacion As String) As String
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.InsertarInsDesembolsoAgrupacion(pEInsDesembolsoAgrupacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Insertar InsDesembolso Medio de Pago
    ''' </summary>
    ''' <param name="pEInsDesembolsoPago">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    ''' </remarks>
    Public Function InsertarInsDesembolsoMedioPago(ByVal pEInsDesembolsoPago As String) As String
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.InsertarInsDesembolsoMedioPago(pEInsDesembolsoPago)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' EliminarInsDesembolsoAgrupacion
    ''' </summary>
    ''' <param name="pEDesembolsoAgrupacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EliminarInsDesembolsoAgrupacion(ByVal pEDesembolsoAgrupacion As String) As String
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.EliminarInsDesembolsoAgrupacion(pEDesembolsoAgrupacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Eliminar MedioPago
    ''' </summary>
    ''' <param name="pEInsDesembolsoPago">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function EliminarInsDesembolsoMedioPago(ByVal pEInsDesembolsoPago As String) As String
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.EliminarInsDesembolsoMedioPago(pEInsDesembolsoPago)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Inicio IBK - AAE - Cambio que retorne boolean
    Public Function recalcula(ByVal pEInsDesembolso As String) As String
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Dim strRetorno As String = ""
        Try
            If objLInstruccionDesembolsoTx.recalcula(pEInsDesembolso) Then
                strRetorno = "0|"
            Else
                strRetorno = "1|Error al actualizar TCs"
            End If
            Return strRetorno
        Catch ex As Exception
            Return "1|" + ex.ToString
        End Try
    End Function


    ''' <summary>
    ''' Actualizar Estado ID
    ''' </summary>
    ''' <param name="pEInsDesembolso">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function ActualizarInsDesembolsoEstado(ByVal pEInsDesembolso As String) As String
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.ActualizarInsDesembolsoEstado(pEInsDesembolso)
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    ''' <summary>
    ''' Ingresar Documento
    ''' </summary>
    ''' <param name="pEInstruccionDesembolsoDoc">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Public Function InsertarInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As Boolean
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.InsertarInstruccionDesembolsoDoc(pEInstruccionDesembolsoDoc)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modificar Documento
    ''' </summary>
    ''' <param name="pEInstruccionDesembolsoDoc">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Public Function ModificarInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As Boolean
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.ModificarInstruccionDesembolsoDoc(pEInstruccionDesembolsoDoc)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Eliminar Documento
    ''' </summary>
    ''' <param name="pEInstruccionDesembolsoDoc">Listado de Objeto Cotizacion(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Public Function EliminarInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As Boolean
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.EliminarInstruccionDesembolsoDoc(pEInstruccionDesembolsoDoc)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Inicio IBK - AAE - Se agrega método
    Public Function AnularInstDesembolso(ByVal pEInstruccionDesembolsoDoc As String) As String
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.AnularInstDesembolso(pEInstruccionDesembolsoDoc)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CheckRelacionesDocBienes(ByVal strCodContrato As String) As String
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.CheckRelacionesDocBienes(strCodContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function LogEnvioDesembolso(ByVal pEGCC_LogDesembolsoPagoEjec As String) As Boolean
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.InsertarEjecucionDesembolsoPagoLog(pEGCC_LogDesembolsoPagoEjec)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function actualizarEstadoEjecucionPago(ByVal pEGCC_InsDesembolsoPago As String) As Boolean
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.ActualizarEstadoejecucionInstruccionDesembolsoPago(pEGCC_InsDesembolsoPago)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function EjecutarDesembolsoLPC(ByVal pFlag As String, ByVal pCodInstDesembolso As String, ByVal pRegUsuario As String) As Integer
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.EjecutarDesembolsoLPC(pFlag, pCodInstDesembolso, pRegUsuario)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function LiberarInstDesembolso(ByVal pCodSolicitudCredito As String, ByVal pCodInstDesembolso As String) As Integer
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.LiberarInstDesembolso(pCodSolicitudCredito, pCodInstDesembolso)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
   
    'Fin IBK
    'Inicio IBK - AAE - Activación leasing Parcial
    Public Function ActualizaCronogramaActivacion(ByVal strEGCC_InsDesembolsoActivacion As String, ByVal strCronograma As String) As Boolean
        Dim objLInstruccionDesembolsoTx As Object = CreateObject("GCC.Logic.LInstruccionDesembolsoTx")
        Try
            Return objLInstruccionDesembolsoTx.ActualizaCronogramaActivacion(strEGCC_InsDesembolsoActivacion, strCronograma)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Fn IBK

End Class

#End Region