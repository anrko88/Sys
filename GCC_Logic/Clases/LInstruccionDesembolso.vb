
Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LInstruccionDesembolsoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 25/09/2012
''' </remarks>
<Guid("41F5A2B2-65FD-4c39-A393-12B546E43CCC") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LInstruccionDesembolsoTx")> _
Public Class LInstruccionDesembolsoTx
    Inherits ServicedComponent
    Implements IInstruccionDesembolsoTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LInstruccionDesembolsoTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta un nuevo registro en la tabla InstruccionDesembolso
    ''' </summary>
    ''' <param name="pEInsDesembolso">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    <AutoComplete(True)> _
     Public Function InsertarInsDesembolso(ByVal pEInsDesembolso As String, ByVal pESolicitudcredito As String) As String Implements IInstruccionDesembolsoTx.InsertarInsDesembolso
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim objDSolicitudCreditoTx As DSolicitudCreditoTx = Nothing
        Dim strNumeroInstruccion As String

        Try
            'Verifica Activacion
            objDSolicitudCreditoTx = New DSolicitudCreditoTx
            objDSolicitudCreditoTx.ModificaActivacionContrato(pESolicitudcredito)

            'Genera Instrucción
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            strNumeroInstruccion = objDInstruccionDesembolsoTx.InsertarInsDesembolso(pEInsDesembolso)

        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return strNumeroInstruccion
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
    <AutoComplete(True)> _
     Public Function GestionInsDesembolso(ByVal pEInsDesembolso As String) As String Implements IInstruccionDesembolsoTx.GestionInsDesembolso
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim strNumeroInstruccion As String

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            strNumeroInstruccion = objDInstruccionDesembolsoTx.GestionInsDesembolso(pEInsDesembolso)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return strNumeroInstruccion
    End Function

    ''' <summary>
    ''' Gestion de cambios de una InstruccionDesembolso
    ''' </summary>
    ''' <param name="pEInsDesembolsoAgrupacion">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    <AutoComplete(True)> _
     Public Function InsertarInsDesembolsoAgrupacion(ByVal pEInsDesembolsoAgrupacion As String) As Boolean Implements IInstruccionDesembolsoTx.InsertarInsDesembolsoAgrupacion
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim booNumeroInstruccion As Boolean = False

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            booNumeroInstruccion = objDInstruccionDesembolsoTx.InsertarInsDesembolsoAgrupacion(pEInsDesembolsoAgrupacion)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return booNumeroInstruccion
    End Function

    ''' <summary>
    ''' Gestion de cambios de una InstruccionDesembolso
    ''' </summary>
    ''' <param name="pEInsDesembolsoPago">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    <AutoComplete(True)> _
     Public Function InsertarInsDesembolsoMedioPago(ByVal pEInsDesembolsoPago As String) As Boolean Implements IInstruccionDesembolsoTx.InsertarInsDesembolsoMedioPago
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim booNumeroInstruccion As Boolean = False

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            booNumeroInstruccion = objDInstruccionDesembolsoTx.InsertarInsDesembolsoMedioPago(pEInsDesembolsoPago)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return booNumeroInstruccion
    End Function

    ''' <summary>
    ''' EliminarInsDesembolsoAgrupacion
    ''' </summary>
    ''' <param name="pEDesembolsoAgrupacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <AutoComplete(True)> _
     Public Function EliminarInsDesembolsoAgrupacion(ByVal pEDesembolsoAgrupacion As String) As Boolean Implements IInstruccionDesembolsoTx.EliminarInsDesembolsoAgrupacion

        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim booNumeroInstruccion As Boolean = False

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            booNumeroInstruccion = objDInstruccionDesembolsoTx.EliminarInsDesembolsoAgrupacion(pEDesembolsoAgrupacion)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return booNumeroInstruccion
    End Function

    ''' <summary>
    ''' Gestion de cambios de una InstruccionDesembolso
    ''' </summary>
    ''' <param name="pEInsDesembolsoPago">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    <AutoComplete(True)> _
     Public Function EliminarInsDesembolsoMedioPago(ByVal pEInsDesembolsoPago As String) As Boolean Implements IInstruccionDesembolsoTx.EliminarInsDesembolsoMedioPago
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim booNumeroInstruccion As Boolean = False

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            booNumeroInstruccion = objDInstruccionDesembolsoTx.EliminarInsDesembolsoMedioPago(pEInsDesembolsoPago)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return booNumeroInstruccion
    End Function

    <AutoComplete(True)> _
  Public Function recalcula(ByVal pEInsDesembolso As String) As Boolean Implements IInstruccionDesembolsoTx.recalcula
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim booNumeroInstruccion As Boolean = False

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            booNumeroInstruccion = objDInstruccionDesembolsoTx.recalcula(pEInsDesembolso)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return booNumeroInstruccion
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
    <AutoComplete(True)> _
     Public Function ActualizarInsDesembolsoEstado(ByVal pEInsDesembolso As String) As Boolean Implements IInstruccionDesembolsoTx.ActualizarInsDesembolsoEstado
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim booNumeroInstruccion As Boolean = False

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            booNumeroInstruccion = objDInstruccionDesembolsoTx.ActualizarInsDesembolsoEstado(pEInsDesembolso)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return booNumeroInstruccion
    End Function

    ''' <summary>
    ''' Ejecucion Instruccion
    ''' </summary>
    ''' <param name="pEInsDesembolso">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    <AutoComplete(True)> _
     Public Function EjecutarInsDesembolsoEstado(ByVal pEInsDesembolso As String) As Boolean Implements IInstruccionDesembolsoTx.EjecutarInsDesembolsoEstado
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim booNumeroInstruccion As Boolean = False

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            booNumeroInstruccion = objDInstruccionDesembolsoTx.EjecutarInsDesembolsoEstado(pEInsDesembolso)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return booNumeroInstruccion
    End Function

    'Inicio IBK - AAE
    <AutoComplete(True)> _
     Public Function AnularInstDesembolso(ByVal pEInstruccionDesembolsoDoc As String) As String Implements IInstruccionDesembolsoTx.AnularInstDesembolso
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim strRes As String = ""

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            strRes = objDInstruccionDesembolsoTx.AnularInstDesembolso(pEInstruccionDesembolsoDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return strRes
    End Function

    <AutoComplete(True)> _
    Public Function CheckRelacionesDocBienes(ByVal strCodContrato As String) As String Implements IInstruccionDesembolsoTx.CheckRelacionesDocBienes
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim strRes As String = ""

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            strRes = objDInstruccionDesembolsoTx.CheckRelacionesDocBienes(strCodContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return strRes
    End Function

    <AutoComplete(True)> _
     Public Function ActualizarEstadoejecucionInstruccionDesembolsoPago(ByVal pEInsDesembolso As String) As Boolean Implements IInstruccionDesembolsoTx.ActualizarEstadoejecucionInstruccionDesembolsoPago
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim booNumeroInstruccion As Boolean = False

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            booNumeroInstruccion = objDInstruccionDesembolsoTx.ActualizarEstadoejecucionInstruccionDesembolsoPago(pEInsDesembolso)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return booNumeroInstruccion
    End Function

    <AutoComplete(True)> _
     Public Function InsertarEjecucionDesembolsoPagoLog(ByVal pEInsDesembolsoPagoLog As String) As Boolean Implements IInstruccionDesembolsoTx.InsertarEjecucionDesembolsoPagoLog
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim booNumeroInstruccion As Boolean = False

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            booNumeroInstruccion = objDInstruccionDesembolsoTx.InsertarEjecucionDesembolsoPagoLog(pEInsDesembolsoPagoLog)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return booNumeroInstruccion
    End Function

    <AutoComplete(True)> _
     Public Function ActualizarEjecucionDesembolsoPagoLog(ByVal pEInsDesembolsoPagoLog As String) As Boolean Implements IInstruccionDesembolsoTx.ActualizarEjecucionDesembolsoPagoLog
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim booNumeroInstruccion As Boolean = False

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            booNumeroInstruccion = objDInstruccionDesembolsoTx.ActualizarEjecucionDesembolsoPagoLog(pEInsDesembolsoPagoLog)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return booNumeroInstruccion
    End Function

    <AutoComplete(True)> _
     Public Function EliminarEjecucionDesembolsoPagoLog(ByVal pEInsDesembolsoPagoLog As String) As Boolean Implements IInstruccionDesembolsoTx.EliminarEjecucionDesembolsoPagoLog
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim booNumeroInstruccion As Boolean = False

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            booNumeroInstruccion = objDInstruccionDesembolsoTx.EliminarEjecucionDesembolsoPagoLog(pEInsDesembolsoPagoLog)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return booNumeroInstruccion
    End Function

    <AutoComplete(True)> _
     Public Function EjecutarDesembolsoLPC(ByVal pFlag As String, ByVal pCodInstDesembolso As String, ByVal pRegUsuario As String) As Integer Implements IInstruccionDesembolsoTx.EjecutarDesembolsoLPC
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim nbrRes As Integer = 0

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            nbrRes = objDInstruccionDesembolsoTx.EjecutarDesembolsoLPC(pFlag, pCodInstDesembolso, pRegUsuario)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return nbrRes
    End Function


    <AutoComplete(True)> _
     Public Function LiberarInstDesembolso(ByVal pCodSolicitudCredito As String, ByVal pCodInstDesembolso As String) As Integer Implements IInstruccionDesembolsoTx.LiberarInstDesembolso
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim nbrRes As Integer = 0

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx
            nbrRes = objDInstruccionDesembolsoTx.LiberarInstDesembolso(pCodSolicitudCredito, pCodInstDesembolso)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return nbrRes
    End Function

    'Fin IBK
    'Inicio IBK - AAE - Actiavación de leasing Parcial
    <AutoComplete(True)> _
    Public Function ActualizaCronogramaActivacion(ByVal strEGCC_InsDesembolsoActivacion As String, ByVal strCronograma As String) As Boolean Implements IInstruccionDesembolsoTx.ActualizaCronogramaActivacion
        Dim objDInstruccionDesembolsoTx As DInstruccionDesembolsoTx = Nothing
        Dim blnResAct As Boolean
        Dim blnResDelCro As Boolean
        Dim blnResInsCro As Boolean
        Dim blnRes As Boolean = True
        Dim strCodContrato As String
        Dim strCodInstruccionDesembolso As String
        Dim oEGCC_InsDesembolsoActivacion As EGCC_InsDesembolsoActivacion

        Try
            objDInstruccionDesembolsoTx = New DInstruccionDesembolsoTx

            'Actualizo información de activación
            blnResAct = objDInstruccionDesembolsoTx.ActualizarInformacionActivacion(strEGCC_InsDesembolsoActivacion)

            If blnResAct = False Then
                blnRes = False
            End If
            If blnRes Then
                'Elimino el cronograma generado
                oEGCC_InsDesembolsoActivacion = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoActivacion)(strEGCC_InsDesembolsoActivacion)
                strCodContrato = oEGCC_InsDesembolsoActivacion.CodSolicitudCredito
                strCodInstruccionDesembolso = oEGCC_InsDesembolsoActivacion.CodInstruccionDesembolso
                blnResDelCro = objDInstruccionDesembolsoTx.EliminarCronogramaActivacion(strCodContrato, strCodInstruccionDesembolso)
                If blnResDelCro = False Then
                    blnRes = False
                End If
                If blnRes Then
                    ' Inserto el nuevo cronograma
                    If Not strCronograma Is Nothing Then
                        If Not strCronograma.Trim().Equals("") Then
                            Dim objEListCronograma As ListEGcc_cotizacioncronograma = CFunciones.DeserializeObject(Of ListEGcc_cotizacioncronograma)(strCronograma)
                            For Each objECronograma In objEListCronograma
                                If blnRes = True Then
                                    objECronograma.Codigocotizacion = strCodContrato
                                    objECronograma.Versioncotizacion = "1"
                                    blnResInsCro = objDInstruccionDesembolsoTx.InsertarCronogramaActivacion(strCodContrato, strCodInstruccionDesembolso, CFunciones.SerializeObject(Of EGcc_cotizacioncronograma)(objECronograma))
                                    If blnResInsCro = False Then
                                        blnRes = False
                                    End If
                                End If
                            Next
                        Else 'If Not pEListCronograma.Trim().Equals("") Then
                            blnRes = False
                        End If 'Else Not pEListCronograma.Trim().Equals("") Then
                    Else ' If Not pEListCronograma Is Nothing Then
                        blnRes = False
                    End If 'Else Not pEListCronograma Is Nothing Then
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoTx.Dispose()
            objDInstruccionDesembolsoTx = Nothing
        End Try

        Return blnRes
    End Function
    'Fin IBK

#Region "Documentos_Comentarios"

    ''' <summary>
    ''' Inserta el cotizacioNDocumento para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEInstruccionDesembolsoDoc">Entidad Serializado de InstruccionDesembolsoDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As Boolean Implements IInstruccionDesembolsoTx.InsertarInstruccionDesembolsoDoc
        Dim objDInstruccionDesembolsoDocTx As DInstruccionDesembolsoDocTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDInstruccionDesembolsoDocTx = New DInstruccionDesembolsoDocTx
            blnResultado = objDInstruccionDesembolsoDocTx.InsertarInstruccionDesembolsoDoc(pEInstruccionDesembolsoDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoDocTx.Dispose()
            objDInstruccionDesembolsoDocTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Modificar el cotizacioNDocumento de una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEInstruccionDesembolsoDoc">Entidad Serializada de InstruccionDesembolsoDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As Boolean Implements IInstruccionDesembolsoTx.ModificarInstruccionDesembolsoDoc
        Dim objDInstruccionDesembolsoDocTx As DInstruccionDesembolsoDocTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDInstruccionDesembolsoDocTx = New DInstruccionDesembolsoDocTx
            blnResultado = objDInstruccionDesembolsoDocTx.ModificarInstruccionDesembolsoDoc(pEInstruccionDesembolsoDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoDocTx.Dispose()
            objDInstruccionDesembolsoDocTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Eliminar el cotizacioNDocumento de una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEInstruccionDesembolsoDoc">Entidad Serializada de InstruccionDesembolsoDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As Boolean Implements IInstruccionDesembolsoTx.EliminarInstruccionDesembolsoDoc
        Dim objDInstruccionDesembolsoDocTx As DInstruccionDesembolsoDocTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDInstruccionDesembolsoDocTx = New DInstruccionDesembolsoDocTx
            blnResultado = objDInstruccionDesembolsoDocTx.EliminarInstruccionDesembolsoDoc(pEInstruccionDesembolsoDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoDocTx.Dispose()
            objDInstruccionDesembolsoDocTx = Nothing
        End Try
        Return blnResultado
    End Function

#End Region

#End Region

End Class

#End Region

#Region "Clase No Transaccional"

''' <summary>
''' Implementación de la clase LInstruccionDesembolsoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 25/09/2012
''' </remarks>
<Guid("F03D8C6C-D70A-468a-BC27-7BF3A8C8002E") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LInstruccionDesembolsoNTx")> _
Public Class LInstruccionDesembolsoNTx
    Inherits ServicedComponent
    Implements IInstruccionDesembolsoNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LInstruccionDesembolsoNTx"
#End Region

#Region "Metodos"

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
                                        ByVal pEInsDesembolso As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolso
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.ListadoInsDesembolso(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pEInsDesembolso)
            End Using
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
    Public Function ListadoInsDesembolsoAgrupacion(ByVal pEInsDesembolsoAgrupacion As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolsoAgrupacion
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.ListadoInsDesembolsoAgrupacion(pEInsDesembolsoAgrupacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ValidaEjecucionInstruccion(ByVal strEGInstruccionDesembolso As String) As String Implements IInstruccionDesembolsoNTx.ValidaEjecucionInstruccion
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.ValidaEjecucionInstruccion(strEGInstruccionDesembolso)
            End Using
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
    Public Function ListadoInsDesembolsoDocAgrupacion(ByVal pEInsDesembolsoAgrupacion As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolsoDocAgrupacion
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.ListadoInsDesembolsoDocAgrupacion(pEInsDesembolsoAgrupacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lista las Instruccion de Desembolso CargoAbono
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function ListadoInsDesembolsoCargoAbono(ByVal pEInsDesembolsoAgrupacion As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolsoCargoAbono
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.ListadoInsDesembolsoCargoAbono(pEInsDesembolsoAgrupacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lista las Instruccion de Desembolso Medios Pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    ''' 

    Public Function ListadoInsDesembolsoMedioPago(ByVal pEInsDesembolsoPago As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolsoMedioPago
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.ListadoInsDesembolsoMedioPago(pEInsDesembolsoPago)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function ListadoInsDesembolsoMedioPagoGet(ByVal pEInsDesembolsoPago As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolsoMedioPagoGet
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.ListadoInsDesembolsoMedioPagoGet(pEInsDesembolsoPago)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtener Instruccion de Desembolso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function ObtenerInsDesembolso(ByVal pEInsDesembolso As String) As String Implements IInstruccionDesembolsoNTx.ObtenerInsDesembolso
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.ObtenerInsDesembolso(pEInsDesembolso)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#Region "Documentos_Comentarios"

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/10/2012
    ''' </remarks>
    Function ListadoInstruccionDesembolsoDoc(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEInstruccionDesembolsoDoc As String _
                                                ) As String Implements IInstruccionDesembolsoNTx.ListadoInstruccionDesembolsoDoc
        Dim objDInstruccionDesembolsoDocNTx As DInstruccionDesembolsoDocNTx = Nothing
        Dim strResultado As String

        Try
            objDInstruccionDesembolsoDocNTx = New DInstruccionDesembolsoDocNTx
            strResultado = objDInstruccionDesembolsoDocNTx.ListadoInstruccionDesembolsoDoc(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pEInstruccionDesembolsoDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoDocNTx.Dispose()
        End Try

        Return strResultado
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
    Function ObtenerInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As String Implements IInstruccionDesembolsoNTx.ObtenerInstruccionDesembolsoDoc
        Dim objDInstruccionDesembolsoDocNTx As DInstruccionDesembolsoDocNTx = Nothing
        Dim strResultado As String

        Try
            objDInstruccionDesembolsoDocNTx = New DInstruccionDesembolsoDocNTx
            strResultado = objDInstruccionDesembolsoDocNTx.ObtenerInstruccionDesembolsoDoc(pEInstruccionDesembolsoDoc)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoDocNTx.Dispose()
        End Try

        Return strResultado
    End Function

#End Region

    'Inicio IBK - AAE - Se agregan los métodos
    Public Function getCargosCuentaInsDes(ByVal pESolicitudcredito As String, ByVal pEInsDesembolso As String) As String Implements IInstruccionDesembolsoNTx.getCargosCuentaInsDes
        Dim objDInstruccionDesembolsoNTx As DInstruccionDesembolsoNTx = Nothing
        Dim strResultado As String

        Try
            objDInstruccionDesembolsoNTx = New DInstruccionDesembolsoNTx
            strResultado = objDInstruccionDesembolsoNTx.getCargosCuentaInsDes(pESolicitudcredito, pEInsDesembolso)
        Catch ex As Exception
            Throw ex
        Finally
            objDInstruccionDesembolsoNTx.Dispose()
        End Try

        Return strResultado
    End Function

    Public Function ListadoInsDesembolsoMedioPago2(ByVal pEInsDesembolsoPago As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolsoMedioPago2
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.ListadoInsDesembolsoMedioPago2(pEInsDesembolsoPago)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function obtenerInfoProveedor(ByVal pCodProv As String) As String Implements IInstruccionDesembolsoNTx.obtenerInfoProveedor
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.obtenerInfoProveedor(pCodProv)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function ListadoInsDesembolsoTotales(ByVal pCodContrato As String, ByVal pInsDesembolsoPago As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolsoTotales
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.ListadoInsDesembolsoTotales(pCodContrato, pInsDesembolsoPago)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function obtenerWIO(ByVal pCodContrato As String, ByVal pInsDesembolsoPago As String) As String Implements IInstruccionDesembolsoNTx.obtenerWIO
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.obtenerWIO(pCodContrato, pInsDesembolsoPago)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function obtenerContablesMedioPago(ByVal strCodMedioPago As String) As String Implements IInstruccionDesembolsoNTx.obtenerContablesMedioPago
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.obtenerContablesMedioPago(strCodMedioPago)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function TieneNotasCredito(ByVal pstrCodSolicitudCredito As String, ByVal pstrCodInstruccionDesembolso As String, ByVal pstrCodAgrupacion As String, ByVal pstrCodCorrelativo As String) As Boolean Implements IInstruccionDesembolsoNTx.TieneNotasCredito
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.TieneNotasCredito(pstrCodSolicitudCredito, pstrCodInstruccionDesembolso, pstrCodAgrupacion, pstrCodCorrelativo)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ' Inicio - IBK - AAE - Activación Leasing Parcial
    Function ListadoInsDesembolsoActParcial(ByVal pEInsDesembolso As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolsoActParcial
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.ListadoInsDesembolsoActParcial(pEInsDesembolso)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function ListaDesembolsos(ByVal pPageSize As Integer, _
                                       ByVal pCurrentPage As Integer, _
                                       ByVal pSortColumn As String, _
                                       ByVal pSortOrder As String, _
                                       ByVal pstrNroContrato As String, _
                                       ByVal pstrCodInstDesembolso As String) As String Implements IInstruccionDesembolsoNTx.ListaDesembolsos

        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.ListaDesembolsos(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pstrNroContrato, pstrCodInstDesembolso)
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Function CronogramaActivacionGet(ByVal pstrNroContrato As String, _
                                                      ByVal pstrInstruccionDesembolso As String _
                                                      ) As String Implements IInstruccionDesembolsoNTx.CronogramaActivacionGet

        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.CronogramaActivacionGet(pstrNroContrato, pstrInstruccionDesembolso)
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Function ValidaEjecucionInstruccionActParcial(ByVal strEGInstruccionDesembolso As String, ByVal strFlagCheckPrecuota As String) As String Implements IInstruccionDesembolsoNTx.ValidaEjecucionInstruccionActParcial
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.ValidaEjecucionInstruccionActParcial(strEGInstruccionDesembolso, strFlagCheckPrecuota)
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Function ReporteLeasingEnProceso(ByVal FecDesIni As String, ByVal FecDesFin As String, ByVal Moneda As String, ByVal CodSolCredito As String, ByVal Flag As String) As String Implements IInstruccionDesembolsoNTx.ReporteLeasingEnProceso
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.ReporteLeasingEnProceso(FecDesIni, FecDesFin, Moneda, CodSolCredito, Flag)
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    ' Fin IBK 

    Function obtenerContablesSUNAT(ByVal strCodTipoAgrupacion As String) As String Implements IInstruccionDesembolsoNTx.obtenerContablesSUNAT
        Try
            Using oDInstruccionDesembolsoNTx As New DInstruccionDesembolsoNTx
                Return oDInstruccionDesembolsoNTx.obtenerContablesSUNAT(strCodTipoAgrupacion)
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
                                     ByRef argsTldDatosTran As String) As String Implements IInstruccionDesembolsoNTx.callProgramaHost
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

    'Replico funciones en COM
    Function fObtenerLineaOP(ByVal pstrCodigoUnico As String, ByVal pintTipo As Integer, ByVal pintCodigoProducto As Integer) As String Implements IInstruccionDesembolsoNTx.fObtenerLineaOP
        Try
            Dim objLConsulta As Object = CreateObject("F15.Logic.LLineaOPNTx")
            Return objLConsulta.fObtenerLineaOP(pstrCodigoUnico, pintTipo, pintCodigoProducto)
        Catch ex As Exception
            Throw ex
            Return ""
        End Try
    End Function

    Function fObtenerDatosLineaOP(ByVal pstrCodigoLinea As String) As String Implements IInstruccionDesembolsoNTx.fObtenerDatosLineaOP
        Try
            Dim objLConsulta As Object = CreateObject("F15.Logic.LLineaOPNTx")
            Return objLConsulta.fObtenerDatosLineaOP(pstrCodigoLinea)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function fObtenerTasasLineas(ByVal pstrCodUnico As String, ByVal pstrCodigoLinea As String) As String Implements IInstruccionDesembolsoNTx.fObtenerTasasLineas
        Try
            Dim objLConsulta As Object = CreateObject("F15.Logic.LLineaOPNTx")
            Return objLConsulta.fObtenerTasaComisiones(pstrCodUnico, pstrCodigoLinea)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function fObtenerParamDomWio(ByVal pstrCodDominio As String, ByVal pstrCodParam As String, ByVal pstrTipo As String) As String Implements IInstruccionDesembolsoNTx.fObtenerParamDomWio
        Try
            Dim objLConsultaParametro As Object = CreateObject("F15.Logic.LConsultaNTx")
            Return objLConsultaParametro.fObtenerParametroDominio(pstrCodDominio, pstrCodParam, pstrTipo)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function fintObtenerSecuenciaLs(ByVal pstrNumeroIO As String) As Integer Implements IInstruccionDesembolsoNTx.fintObtenerSecuenciaLs
        Try
            Dim objLConsulta As Object = CreateObject("F15.Logic.LInstruccionOperativaNTx")
            Return objLConsulta.fintObtenerSecuenciaLs(pstrNumeroIO)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ' Fin IBK

    ''' <summary>
    ''' Lista todos los desembolsos mensuales
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <param name="pFechaTermino">Fecha de término de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 22/01/2013 05:29:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoDesembolsoMensualReporte(ByVal pFechaInicio As String, _
                                                        ByVal pFechaTermino As String) As String Implements IInstruccionDesembolsoNTx.fobjListadoDesembolsoMensualReporte
        Dim objInstruccionDesembolso As DInstruccionDesembolsoNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objInstruccionDesembolso = New DInstruccionDesembolsoNTx
            strResultado = objInstruccionDesembolso.fobjListadoDesembolsoMensualReporte(pFechaInicio, _
                                                                                pFechaTermino)
        Catch ex As Exception
            Throw ex
        Finally
            If objInstruccionDesembolso IsNot Nothing Then
                objInstruccionDesembolso.Dispose()
                objInstruccionDesembolso = Nothing
            End If
        End Try

        Return strResultado
    End Function
    Public Function ListaAgrupacionVoucher(ByVal pstrNroContrato As String, ByVal pstrNroInstruccion As String, ByVal pstrCorrelativo As String) As String Implements IInstruccionDesembolsoNTx.ListaAgrupacionVoucher
        Dim objInstruccionDesembolso As DInstruccionDesembolsoNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objInstruccionDesembolso = New DInstruccionDesembolsoNTx
            strResultado = objInstruccionDesembolso.ListaAgrupacionVoucher(pstrNroContrato, pstrNroInstruccion, pstrCorrelativo)
        Catch ex As Exception
            Throw ex
        Finally
            If objInstruccionDesembolso IsNot Nothing Then
                objInstruccionDesembolso.Dispose()
                objInstruccionDesembolso = Nothing
            End If
        End Try

        Return strResultado
    End Function
#End Region

End Class

#End Region

