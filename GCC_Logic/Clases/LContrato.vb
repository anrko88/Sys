

Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data


#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LContratoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/05/2012
''' </remarks>
<Guid("FC819235-E064-4472-BAA5-A3D72E0ACF67") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LContratoTx")> _
Public Class LContratoTx
    Inherits ServicedComponent
    Implements IContratoTx


#Region "Constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LContratoTx"

#End Region

#Region "Métodos"

#Region "Contratochecklist"

    <AutoComplete(True)> _
    Public Function CheckLisComercialUpd(ByVal pstrESolicitudcredito As String, _
                                         ByVal pEGcc_checklisComercialCuentas As String) As Boolean Implements IContratoTx.CheckLisComercialUpd
        Dim objContratoTx As DContratoTx = Nothing
        Dim resultado As Boolean
        Dim resultado2 As Boolean
        Dim objDContratoCuentaTx As DContratoCuentaTx = Nothing

        Try
            objContratoTx = New DContratoTx
            objDContratoCuentaTx = New DContratoCuentaTx
            resultado = objContratoTx.ContratoChecklistUpd(pstrESolicitudcredito)

            'Elimina Cuentas
            If resultado Then
                resultado2 = objDContratoCuentaTx.ContratoCuentaDel(pstrESolicitudcredito)

                Dim pEGcc_contratocuenta As String
                Dim objListEGcc_contratoCuenta As ListEGcc_contratoCuenta = CFunciones.DeserializeObject(Of ListEGcc_contratoCuenta)(pEGcc_checklisComercialCuentas)
                'Dim objEGcc_contratocuenta As EGcc_contratocuenta
                'Grabar Contrato Cuenta
                For Each objEGcc_contratocuenta In objListEGcc_contratoCuenta
                    If Not objEGcc_contratocuenta.Cuenta Is Nothing Then
                        If Not objEGcc_contratocuenta.Cuenta.Trim().Equals("") Then
                            pEGcc_contratocuenta = CFunciones.SerializeObject(objEGcc_contratocuenta)
                            resultado = objDContratoCuentaTx.ContratoCuentaIns(pEGcc_contratocuenta)
                        End If
                    End If
                Next objEGcc_contratocuenta
            Else
                Throw New Exception("Error en Insertar Contrato")
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objContratoTx.Dispose()
            objDContratoCuentaTx.Dispose()
        End Try
        Return resultado
    End Function
    'Inicio IBK - AAE - 12/02/2013 - Agrego nueva función
    Public Function CheckLisComercialUpd2(ByVal pstrESolicitudcredito As String, _
                                         ByVal pEGcc_checklisComercialCuentas As String, _
                                         ByVal pEgcc_cotizacion As String) As Boolean Implements IContratoTx.CheckLisComercialUpd2
        Dim objContratoTx As DContratoTx = Nothing
        Dim resultado As Boolean
        Dim resultado2 As Boolean
        Dim objDContratoCuentaTx As DContratoCuentaTx = Nothing

        Try
            objContratoTx = New DContratoTx
            objDContratoCuentaTx = New DContratoCuentaTx
            resultado = objContratoTx.ContratoChecklistUpd2(pstrESolicitudcredito, pEgcc_cotizacion)

            'Elimina Cuentas
            If resultado Then
                resultado2 = objDContratoCuentaTx.ContratoCuentaDel(pstrESolicitudcredito)

                Dim pEGcc_contratocuenta As String
                Dim objListEGcc_contratoCuenta As ListEGcc_contratoCuenta = CFunciones.DeserializeObject(Of ListEGcc_contratoCuenta)(pEGcc_checklisComercialCuentas)
                'Dim objEGcc_contratocuenta As EGcc_contratocuenta
                'Grabar Contrato Cuenta
                For Each objEGcc_contratocuenta In objListEGcc_contratoCuenta
                    If Not objEGcc_contratocuenta.Cuenta Is Nothing Then
                        If Not objEGcc_contratocuenta.Cuenta.Trim().Equals("") Then
                            pEGcc_contratocuenta = CFunciones.SerializeObject(objEGcc_contratocuenta)
                            resultado = objDContratoCuentaTx.ContratoCuentaIns(pEGcc_contratocuenta)
                        End If
                    End If
                Next objEGcc_contratocuenta
            Else
                Throw New Exception("Error en Insertar Contrato")
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objContratoTx.Dispose()
            objDContratoCuentaTx.Dispose()
        End Try
        Return resultado
    End Function
    'Fin IBK

#End Region

#Region "Contrato"

    <AutoComplete(True)> _
    Public Function ActualizaEstado(ByVal pSolicitudCredito As String) As Boolean Implements IContratoTx.ActualizaEstado
        Dim objDContratoTx As DContratoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoTx = New DContratoTx

            blnResultado = objDContratoTx.ActualizaEstado(pSolicitudCredito)
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoTx.Dispose()
            objDContratoTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Actualiza los datos del contrato durante la etapa de formalización.
    ''' Si el contrato se encuentra en estado [04], lo regresa a estado [03].
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <param name="pEGcc_contratootroconcepto">Entidad contratootroconcepto serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnModificarContrato(ByVal pESolicitudCredito As String, _
                                          ByVal pEGcc_contratootroconcepto As String) As Boolean Implements IContratoTx.fblnModificarContrato
        Dim objDContratoTx As DContratoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoTx = New DContratoTx


            blnResultado = objDContratoTx.fblnModificarContrato(pESolicitudCredito)

            If blnResultado Then
                Dim objDContratoOtroConceptoTx As DContratoOtroConceptoTx

                objDContratoOtroConceptoTx = New DContratoOtroConceptoTx
                blnResultado = objDContratoOtroConceptoTx.fblnModificar(pEGcc_contratootroconcepto)

                If blnResultado Then
                    blnResultado = objDContratoTx.fblnModificado(pESolicitudCredito)
                Else
                    Throw New Exception("No se pudo actualizar el contrato.")
                End If
            Else
                Throw New Exception("No se pudo actualizar el contrato.")
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoTx.Dispose()
            objDContratoTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Envía la carta y actuliza el estado del contrato a pendiente de firma
    ''' </summary>
    ''' <param name="pSolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <param name="pEGCC_Carta">Entidad EGCC_Carta serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EnviarCarta(ByVal pSolicitudCredito As String, _
                                ByVal pEGCC_Carta As String) As Boolean Implements IContratoTx.EnviarCarta
        Dim objDContratoTx As DContratoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoTx = New DContratoTx

            blnResultado = objDContratoTx.ActualizaEstado(pSolicitudCredito)

            If blnResultado Then
                Dim objDCartaTx As DCartaTx = Nothing
                objDCartaTx = New DCartaTx
                blnResultado = objDCartaTx.EnviarCarta(pEGCC_Carta)
            Else
                Throw New Exception("No se pudo enviar la carta.")
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoTx.Dispose()
            objDContratoTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Envía la carta y actualiza el estado del contrato a pendiente de firma
    ''' </summary>
    ''' <param name="pSolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ContratoGuardarYEnviar(ByVal pSolicitudCredito As String, _
                                           ByVal pEGcc_contratootroconcepto As String) As Boolean Implements IContratoTx.ContratoGuardarYEnviar
        Dim objDContratoTx As DContratoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoTx = New DContratoTx

            blnResultado = objDContratoTx.ActualizaEstado(pSolicitudCredito)

            If blnResultado Then
                Dim objDContratoOtroConceptoTx As DContratoOtroConceptoTx = Nothing
                objDContratoOtroConceptoTx = New DContratoOtroConceptoTx
                blnResultado = objDContratoOtroConceptoTx.fblnModificar(pEGcc_contratootroconcepto)

                If blnResultado Then
                    blnResultado = objDContratoTx.fblnModificarContrato(pSolicitudCredito)
                Else
                    Throw New Exception("No se pudo actualizar el contrato.")
                End If
            Else
                Throw New Exception("No se pudo actualizar el contrato.")
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoTx.Dispose()
            objDContratoTx = Nothing
        End Try

        Return blnResultado
    End Function

    'Inicio IBK - AAE
    <AutoComplete(True)> _
   Public Function fblnModificarContrato2(ByVal pESolicitudCredito As String, _
                                         ByVal pEGcc_contratootroconcepto As String) As Boolean Implements IContratoTx.fblnModificarContrato2
        Dim objDContratoTx As DContratoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoTx = New DContratoTx


            blnResultado = objDContratoTx.fblnModificarContrato(pESolicitudCredito)

            If blnResultado Then
                Dim objDContratoOtroConceptoTx As DContratoOtroConceptoTx

                objDContratoOtroConceptoTx = New DContratoOtroConceptoTx
                blnResultado = objDContratoOtroConceptoTx.fblnModificar2(pEGcc_contratootroconcepto)

                If blnResultado Then
                    blnResultado = objDContratoTx.fblnModificado(pESolicitudCredito)
                Else
                    Throw New Exception("No se pudo actualizar el contrato.")
                End If
            Else
                Throw New Exception("No se pudo actualizar el contrato.")
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoTx.Dispose()
            objDContratoTx = Nothing
        End Try

        Return blnResultado
    End Function

    <AutoComplete(True)> _
    Public Function ContratoGuardarYEnviar2(ByVal pSolicitudCredito As String, _
                                           ByVal pEGcc_contratootroconcepto As String) As Boolean Implements IContratoTx.ContratoGuardarYEnviar2
        Dim objDContratoTx As DContratoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoTx = New DContratoTx

            blnResultado = objDContratoTx.ActualizaEstado(pSolicitudCredito)

            If blnResultado Then
                Dim objDContratoOtroConceptoTx As DContratoOtroConceptoTx = Nothing
                objDContratoOtroConceptoTx = New DContratoOtroConceptoTx
                blnResultado = objDContratoOtroConceptoTx.fblnModificar2(pEGcc_contratootroconcepto)

                If blnResultado Then
                    blnResultado = objDContratoTx.fblnModificarContrato(pSolicitudCredito)
                Else
                    Throw New Exception("No se pudo actualizar el contrato.")
                End If
            Else
                Throw New Exception("No se pudo actualizar el contrato.")
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoTx.Dispose()
            objDContratoTx = Nothing
        End Try

        Return blnResultado
    End Function

    'Fin IBK - AAE

    ''' <summary>
    ''' Actualiza el nombre del documento de separación del conyugue en la tabla Contrato (SolicitudCredito)
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ActualizaDocumentoSeparacion(ByVal pESolicitudCredito As String) As Boolean Implements IContratoTx.ActualizaDocumentoSeparacion
        Dim objDContratoTx As DContratoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoTx = New DContratoTx

            blnResultado = objDContratoTx.ActualizaDocumentoSeparacion(pESolicitudCredito)

            If blnResultado Then
                blnResultado = objDContratoTx.fblnModificado(pESolicitudCredito)
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objDContratoTx.Dispose()
            objDContratoTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Actualiza el nombre del documento de contrato en la tabla Contrato (SolicitudCredito)
    ''' </summary>
    ''' <param name="pSolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ActualizaArchivoContratoAdjunto(ByVal pSolicitudCredito As String) As Boolean Implements IContratoTx.ActualizaArchivoContratoAdjunto
        Dim objDContratoTx As DContratoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoTx = New DContratoTx

            blnResultado = objDContratoTx.ActualizaArchivoContratoAdjunto(pSolicitudCredito)

        Catch ex As Exception
            Throw ex
        Finally
            objDContratoTx.Dispose()
            objDContratoTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Modifica Activacion de Contrato
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 15/10/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificaActivacionContrato(ByVal pESolicitudCredito As String) As Boolean Implements IContratoTx.ModificaActivacionContrato
        Dim objDSolicitudCreditoTx As DSolicitudCreditoTx = Nothing
        Dim blnResultado As Boolean
        Try
            objDSolicitudCreditoTx = New DSolicitudCreditoTx
            blnResultado = objDSolicitudCreditoTx.ModificaActivacionContrato(pESolicitudCredito)
        Catch ex As Exception
            Throw ex
        Finally
            objDSolicitudCreditoTx.Dispose()
            objDSolicitudCreditoTx = Nothing
        End Try
        Return blnResultado
    End Function

#End Region

#Region "ContratoNotarial"

    ''' <summary>
    ''' Ingresa nuevo objeto EGcc_contratonotarial
    ''' </summary>
    ''' <param name="pEGcc_contratonotarial">Objeto EGcc_contratonotarial(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarContratoNotarial(ByVal pESolicitudCredito As String, _
                                             ByVal pEGcc_contratonotarial As String) As Integer Implements IContratoTx.InsertarContratoNotarial
        Dim objContratoNotarialTx As DContratoNotarialTx = Nothing
        Dim iResultado As Integer

        Try
            objContratoNotarialTx = New DContratoNotarialTx

            iResultado = objContratoNotarialTx.InsertarContratoNotarial(pEGcc_contratonotarial)

            If iResultado > 0 Then
                Dim objDContratoTx As DContratoTx
                objDContratoTx = New DContratoTx

                objDContratoTx.fblnModificado(pESolicitudCredito)
            Else
                Throw New Exception("No se pudo ingresar el documento notarial.")
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNotarialTx.Dispose()
            objContratoNotarialTx = Nothing
        End Try

        Return iResultado
    End Function

    ''' <summary>
    ''' Modifica EGCC_ContratoNotarial
    ''' </summary>
    ''' <param name="pEGCC_ContratoNotarial">Objeto EGCC_ContratoNotarial (Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarContratoNotarial(ByVal pESolicitudCredito As String, _
                                              ByVal pEGCC_ContratoNotarial As String) As Boolean Implements IContratoTx.ModificarContratoNotarial
        Dim objDContratoNotarialTx As DContratoNotarialTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoNotarialTx = New DContratoNotarialTx
            blnResultado = objDContratoNotarialTx.ModificarContratoNotarial(pEGCC_ContratoNotarial)

            ' Actualizar el estado del contrato, indicando que se actualizó uno de sus elementos.
            If blnResultado Then
                Dim objDContratoTx As DContratoTx
                objDContratoTx = New DContratoTx

                blnResultado = objDContratoTx.fblnModificado(pESolicitudCredito)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoNotarialTx.Dispose()
            objDContratoNotarialTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Elimina una lista de objetos EGCC_ContratoNotarial, identificándolo por su
    ''' CodigoNotarial y NumeroContrato.
    ''' </summary>
    ''' <param name="pListEGcc_contratonotarial">Lista de objetos EGcc_contratonotarial (Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 10/05/2012
    ''' </remarks> 
    <AutoComplete(True)> _
    Public Function EliminarContratoNotarial(ByVal pESolicitudCredito As String, _
                                             ByVal pListEGcc_contratonotarial As String) As Boolean Implements IContratoTx.EliminarContratoNotarial
        Dim objDContratoNotarialTx As DContratoNotarialTx = Nothing
        Dim pEGCC_ContratoNotarial As String

        Dim blnResultado As Boolean

        Try
            Dim objListEGcc_contratonotarial As ListEGcc_contratonotarial = CFunciones.DeserializeObject(Of ListEGcc_contratonotarial)(pListEGcc_contratonotarial)

            Dim objEGcc_contratonotarial As EGcc_contratonotarial
            objDContratoNotarialTx = New DContratoNotarialTx
            For Each objEGcc_contratonotarial In objListEGcc_contratonotarial
                pEGCC_ContratoNotarial = CFunciones.SerializeObject(objEGcc_contratonotarial)
                blnResultado = objDContratoNotarialTx.EliminarContratoNotarial(pEGCC_ContratoNotarial)

            Next objEGcc_contratonotarial

            ' Actualizar el estado del contrato, indicando que se actualizó uno de sus elementos.
            If blnResultado Then
                Dim objDContratoTx As DContratoTx
                objDContratoTx = New DContratoTx

                blnResultado = objDContratoTx.fblnModificado(pESolicitudCredito)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoNotarialTx.Dispose()
            objDContratoNotarialTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Actualiza el nombre del archivo adjunto en la tabla GCC_ContratoNotarial.
    ''' a una adenda.
    ''' </summary>
    ''' <param name="pEGCC_ContratoNotarial">Objeto EGCC_ContratoNotarial (Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ContratoNotarialActualizarNombreArchivo(ByVal pESolicitudCredito As String, _
                                                            ByVal pEGCC_ContratoNotarial As String) As Boolean Implements IContratoTx.ContratoNotarialActualizarNombreArchivo
        Dim objDContratoNotarialTx As DContratoNotarialTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoNotarialTx = New DContratoNotarialTx
            blnResultado = objDContratoNotarialTx.ActualizarNombreArchivo(pEGCC_ContratoNotarial)

            ' Actualizar el estado del contrato, indicando que se actualizó uno de sus elementos.
            If blnResultado Then
                Dim objDContratoTx As DContratoTx
                objDContratoTx = New DContratoTx

                blnResultado = objDContratoTx.fblnModificado(pESolicitudCredito)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoNotarialTx.Dispose()
            objDContratoNotarialTx = Nothing
        End Try

        Return blnResultado
    End Function

#End Region

#Region "Adenda"

    ''' <summary>
    ''' Ingresa nuevo objeto EGcc_contratonotarial
    ''' </summary>
    ''' <param name="pEGcc_contratonotarial">Objeto EGcc_contratonotarial(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarAdenda(ByVal pEGcc_contratonotarial As String) As Integer Implements IContratoTx.InsertarAdenda
        Dim objContratoNotarialTx As DContratoNotarialTx = Nothing
        Dim iResultado As Integer

        Try
            objContratoNotarialTx = New DContratoNotarialTx

            iResultado = objContratoNotarialTx.InsertarContratoNotarial(pEGcc_contratonotarial)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNotarialTx.Dispose()
            objContratoNotarialTx = Nothing
        End Try

        Return iResultado
    End Function

    ''' <summary>
    ''' Modifica EGCC_ContratoNotarial, cuando el registro contiene los datos correspondientes
    ''' a una adenda.
    ''' </summary>
    ''' <param name="pEGCC_ContratoNotarial">Objeto EGCC_ContratoNotarial (Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarAdenda(ByVal pEGCC_ContratoNotarial As String) As Boolean Implements IContratoTx.ModificarAdenda
        Dim objDContratoNotarialTx As DContratoNotarialTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoNotarialTx = New DContratoNotarialTx
            blnResultado = objDContratoNotarialTx.ModificarAdenda(pEGCC_ContratoNotarial)
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoNotarialTx.Dispose()
            objDContratoNotarialTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Elimina una lista de objetos EGCC_ContratoNotarial, identificándolo por su
    ''' CodigoNotarial y NumeroContrato.
    ''' </summary>
    ''' <param name="pListEGcc_contratonotarial">Lista de objetos EGcc_contratonotarial (Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 10/05/2012
    ''' </remarks> 
    <AutoComplete(True)> _
    Public Function EliminarAdenda(ByVal pListEGcc_contratonotarial As String) As Boolean Implements IContratoTx.EliminarAdenda
        Dim objDContratoNotarialTx As DContratoNotarialTx = Nothing
        Dim pEGCC_ContratoNotarial As String

        Dim blnResultado As Boolean

        Try
            Dim objListEGcc_contratonotarial As ListEGcc_contratonotarial = CFunciones.DeserializeObject(Of ListEGcc_contratonotarial)(pListEGcc_contratonotarial)

            Dim objEGcc_contratonotarial As EGcc_contratonotarial
            objDContratoNotarialTx = New DContratoNotarialTx
            For Each objEGcc_contratonotarial In objListEGcc_contratonotarial
                pEGCC_ContratoNotarial = CFunciones.SerializeObject(objEGcc_contratonotarial)
                blnResultado = objDContratoNotarialTx.EliminarContratoNotarial(pEGCC_ContratoNotarial)

            Next objEGcc_contratonotarial
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoNotarialTx.Dispose()
            objDContratoNotarialTx = Nothing
        End Try

        Return blnResultado
    End Function

#End Region

#Region "Bien"

    ''' <summary>
    ''' Inserta un nuevo bien en las tablas ESolicitudCreditoEstructura y ESolicitudCreditoEstructuraCarac
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>
    ''' <param name="pESolicitudCreditoEstructuraCarac">Entidad ESolicitudCreditoEstructuraCarac serializada</param>
    ''' <returns>String con el número de Temporal</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarBien(ByVal pESolicitudCredito As String, _
                                 ByVal pESolicitudCreditoEstructura As String, _
                                 ByVal pESolicitudCreditoEstructuraCarac As String) As Integer Implements IContratoTx.InsertarBien
        Dim objBienTx As DBienTx = Nothing
        Dim bResultado As Boolean

        Try
            objBienTx = New DBienTx
            bResultado = objBienTx.fblnInsertar(pESolicitudCreditoEstructura, _
                                                            pESolicitudCreditoEstructuraCarac)

            If bResultado Then
                Dim objDContratoTx As DContratoTx
                objDContratoTx = New DContratoTx

                bResultado = objDContratoTx.fblnModificado(pESolicitudCredito)
            End If

            Return bResultado
        Catch ex As Exception
            Throw ex
        Finally
            objBienTx.Dispose()
            objBienTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los datos de un bien existente = , en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.
    ''' Soporta los tipos de producto Inmueble = , Inmueble y otros.
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructura">Entidad ESolicitudCreditoEstructura serializada</param>
    ''' <param name="pESolicitudCreditoEstructuraCarac">Entidad ESolicitudCreditoEstructuraCarac serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarBien(ByVal pESolicitudCredito As String, _
                                  ByVal pESolicitudCreditoEstructura As String, _
                                  ByVal pESolicitudCreditoEstructuraCarac As String) As Boolean Implements IContratoTx.ModificarBien
        Dim objDBienTx As DBienTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDBienTx = New DBienTx
            blnResultado = objDBienTx.fblnModificar(pESolicitudCreditoEstructura, _
                                                    pESolicitudCreditoEstructuraCarac)

            If blnResultado Then
                Dim objDContratoTx As DContratoTx
                objDContratoTx = New DContratoTx

                blnResultado = objDContratoTx.fblnModificado(pESolicitudCredito)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDBienTx.Dispose()
            objDBienTx = Nothing
        End Try

        Return blnResultado
    End Function

    ''' <summary>
    ''' Elimina un bien cuyas claves coinciden con las enviadas por parámetro, en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.
    ''' </summary>
    ''' <param name="pESolicitudCreditoEstructuraLst">Entidad ESolicitudCreditoEstructuraLst serializada</param>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarBien(ByVal pESolicitudCredito As String, _
                                 ByVal pESolicitudCreditoEstructuraLst As String) As Boolean Implements IContratoTx.EliminarBien
        Dim objDBienTx As DBienTx = Nothing
        Dim blnResultado As Boolean

        Try
            Dim objBienList As ListESolicitudcreditoestructura = CFunciones.DeserializeObject(Of ListESolicitudcreditoestructura)(pESolicitudCreditoEstructuraLst)

            objDBienTx = New DBienTx
            For Each objESolicitudcreditoestructura In objBienList
                Dim pESolicitudcreditoestructura As String

                pESolicitudcreditoestructura = CFunciones.SerializeObject(objESolicitudcreditoestructura)
                blnResultado = objDBienTx.fblnEliminar(pESolicitudcreditoestructura)

            Next objESolicitudcreditoestructura

            If blnResultado Then
                Dim objDContratoTx As DContratoTx
                objDContratoTx = New DContratoTx

                blnResultado = objDContratoTx.fblnModificado(pESolicitudCredito)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDBienTx.Dispose()
            objDBienTx = Nothing
        End Try

        Return blnResultado
    End Function

#End Region

#Region "ContratoDocumento"

    ''' <summary>
    ''' Actualiza los datos del texto predefinido con el nuevo texto ingresado.
    ''' </summary>
    ''' <param name="pEGCC_ContratoDocumento">Objeto ContratoDocumento serializado</param>
    ''' <returns>True si se grabó correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarTextoPredefinido(ByVal pESolicitudCredito As String, _
                                              ByVal pEGCC_ContratoDocumento As String) As Boolean Implements IContratoTx.ModificarTextoPredefinido
        Dim objDContratoDocumentoTx As DContratoDocumentoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoDocumentoTx = New DContratoDocumentoTx
            blnResultado = objDContratoDocumentoTx.ModificarTextoPredefinido(pEGCC_ContratoDocumento)

            If blnResultado Then
                Dim objDContratoTx As DContratoTx
                objDContratoTx = New DContratoTx

                blnResultado = objDContratoTx.fblnModificado(pESolicitudCredito)
            End If

            Return blnResultado
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoDocumentoTx.Dispose()
            objDContratoDocumentoTx = Nothing
        End Try
    End Function

    <AutoComplete(True)> _
   Public Function EstadoAprobarLegal(ByVal pEGCC_ContratoDocumento As String) As Boolean Implements IContratoTx.EstadoAprobarLegal
        Dim objDContratoDocumentoTx As DContratoDocumentoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoDocumentoTx = New DContratoDocumentoTx
            blnResultado = objDContratoDocumentoTx.EstadoAprobarLegal(pEGCC_ContratoDocumento)

            Return blnResultado
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoDocumentoTx.Dispose()
            objDContratoDocumentoTx = Nothing
        End Try
    End Function


#End Region

#Region "ContratoOtroConcepto"

    ''' <summary>
    ''' Actualiza los datos de objeto EGCC_ContratoOtroConcepto.
    ''' </summary>
    ''' <param name="pEGCC_ContratoOtroConcepto">Entidad EGCC_ContratoOtroConcepto serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function fblnModificarContratoOtroConcepto(ByVal pEGCC_ContratoOtroConcepto As String) As Boolean Implements IContratoTx.fblnModificarContratoOtroConcepto
        Dim objDContratoDocumentoTx As DContratoDocumentoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoDocumentoTx = New DContratoDocumentoTx
            blnResultado = objDContratoDocumentoTx.ModificarTextoPredefinido(pEGCC_ContratoOtroConcepto)

            Return blnResultado
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoDocumentoTx.Dispose()
            objDContratoDocumentoTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los datos de objeto EGCC_ContratoOtroConcepto.
    ''' </summary>
    ''' <param name="pEGCC_ContratoOtroConcepto">Entidad EGCC_ContratoOtroConcepto serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ContratoOtroConceptoAdjuntoUpd(ByVal pESolicitudCredito As String, _
                                                   ByVal pEGCC_ContratoOtroConcepto As String) As Boolean Implements IContratoTx.ContratoOtroConceptoAdjuntoUpd
        Dim objDContratoOtroConceptoTx As DContratoOtroConceptoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDContratoOtroConceptoTx = New DContratoOtroConceptoTx
            blnResultado = objDContratoOtroConceptoTx.ContratoOtroConceptoAdjuntoUpd(pEGCC_ContratoOtroConcepto)

            If blnResultado Then
                Dim objDContratoTx As DContratoTx
                objDContratoTx = New DContratoTx

                blnResultado = objDContratoTx.fblnModificado(pESolicitudCredito)
            End If

            Return blnResultado
        Catch ex As Exception
            Throw ex
        Finally
            objDContratoOtroConceptoTx.Dispose()
            objDContratoOtroConceptoTx = Nothing
        End Try
    End Function

#End Region

#Region "Inserta Observaciones Documento"
    <AutoComplete(True)> _
      Public Function InsertarContratoDocumentoObservacion(ByVal pEGcc_contratodocumentoObservacion As String) As Boolean Implements IContratoTx.InsertarContratoDocumentoObservacion
        Dim objContratoDocumentoObservacionTx As DContratoDocumentoTx = Nothing
        Dim iResultado As Integer

        Try
            objContratoDocumentoObservacionTx = New DContratoDocumentoTx
            iResultado = objContratoDocumentoObservacionTx.InsertarContratoDocumentoObservacion(pEGcc_contratodocumentoObservacion)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoDocumentoObservacionTx.Dispose()
            objContratoDocumentoObservacionTx = Nothing
        End Try

        Return iResultado

    End Function
    'InicioJJM
    <AutoComplete(True)> _
     Public Function InsertarContratoDocumentoObservacionInafectacion(ByVal pEGcc_contratodocumentoObservacion As String) As Boolean Implements IContratoTx.InsertarContratoDocumentoObservacionInafectacion
        Dim objContratoDocumentoObservacionTx As DContratoDocumentoTx = Nothing
        Dim iResultado As Integer

        Try
            objContratoDocumentoObservacionTx = New DContratoDocumentoTx
            iResultado = objContratoDocumentoObservacionTx.InsertarContratoDocumentoObservacionInafectacion(pEGcc_contratodocumentoObservacion)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoDocumentoObservacionTx.Dispose()
            objContratoDocumentoObservacionTx = Nothing
        End Try

        Return iResultado

    End Function

    'FinJJM
#End Region

#Region "Elimina Documento Contrato"

    'Public Function EliminaContratoDocumento(ByVal pEGcc_contratodocumento As String) As Boolean Implements IContratoDocumentoTx.EliminaContratoDocumento
    <AutoComplete(True)> _
     Public Function EliminaContratoDocumento(ByVal pEGcc_contratodocumento As String) As Boolean Implements IContratoTx.EliminaContratoDocumento
        Dim objContratoDocumentoTx As DContratoDocumentoTx = Nothing
        Dim iResultado As Integer

        Try
            objContratoDocumentoTx = New DContratoDocumentoTx
            iResultado = objContratoDocumentoTx.EliminaContratoDocumento(pEGcc_contratodocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoDocumentoTx.Dispose()
            objContratoDocumentoTx = Nothing
        End Try

        Return iResultado

    End Function
#End Region

#Region "ContratoRepresentante"

    ''' <summary>
    ''' Ingresa un nuevo representante en la tabla representantes y para los representantes del contrato.
    ''' </summary>
    ''' <param name="pEGcc_representante">Entidad pEGcc_representante serializada</param>
    ''' <param name="pEGcc_contratorepresentante">Entidad EGcc_contratorepresentante serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function RepresentanteContratoIns(ByVal pESolicitudCredito As String, _
                                             ByVal pEGcc_representante As String, _
                                             ByVal pEGcc_contratorepresentante As String) As Boolean Implements IContratoTx.RepresentanteContratoIns
        Dim objDRepresentanteTx As New DRepresentanteTx
        Dim bResultado As Boolean

        Try
            objDRepresentanteTx = New DRepresentanteTx

            Dim codigorepresentante As Integer
            codigorepresentante = objDRepresentanteTx.RepresentanteClienteIns(pEGcc_representante)
            If (codigorepresentante > 0) Then
                Dim oEGccContratoRepresentante As EGcc_contratorepresentante
                Dim sResultado As String
                oEGccContratoRepresentante = CFunciones.DeserializeObject(Of EGcc_contratorepresentante)(pEGcc_contratorepresentante)
                oEGccContratoRepresentante.Codigorepresentante = codigorepresentante

                pEGcc_contratorepresentante = CFunciones.SerializeObject(Of EGcc_contratorepresentante)(oEGccContratoRepresentante)
                Dim objDContratoRepresentanteTx As DContratoRepresentanteTx
                objDContratoRepresentanteTx = New DContratoRepresentanteTx

                sResultado = objDContratoRepresentanteTx.RepresentanteContratoIns(pEGcc_contratorepresentante)
                If sResultado = "1" Then
                    Dim objDContratoTx As DContratoTx
                    objDContratoTx = New DContratoTx

                    bResultado = objDContratoTx.fblnModificado(pESolicitudCredito)
                Else
                    bResultado = False
                End If
            Else
                bResultado = False
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objDRepresentanteTx.Dispose()
            objDRepresentanteTx = Nothing
        End Try

        Return bResultado
    End Function

#End Region

    <AutoComplete(True)> _
   Public Function InsertaSeguimientoContrato(ByVal pEGcc_SeguimientoContrato As String) As Boolean Implements IContratoTx.InsertaSeguimientoContrato
        Dim objSeguimientoContratoTx As DContratoTx = Nothing
        Dim iResultado As Integer

        Try
            objSeguimientoContratoTx = New DContratoTx
            iResultado = objSeguimientoContratoTx.InsertaSeguimientoContrato(pEGcc_SeguimientoContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objSeguimientoContratoTx.Dispose()
            objSeguimientoContratoTx = Nothing
        End Try

        Return iResultado

    End Function
    <AutoComplete(True)> _
   Public Function ContratoDocumentoAdjuntoUpd(ByVal pEGcc_contratodocumento As String) As Boolean Implements IContratoTx.ContratoDocumentoAdjuntoUpd
        Dim objSeguimientoContratoTx As New DContratoDocumentoTx
        Dim iResultado As Integer

        Try
            objSeguimientoContratoTx = New DContratoDocumentoTx
            iResultado = objSeguimientoContratoTx.ArchivoAdjuntoAfectoUpd(pEGcc_contratodocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objSeguimientoContratoTx.Dispose()
            objSeguimientoContratoTx = Nothing
        End Try

        Return iResultado

    End Function

#End Region

End Class

#End Region

#Region "Clase No Transaccional"

''' <summary>
''' Implementación de la clase LContratoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/05/2012
''' </remarks>
<Guid("1CF08620-2E15-4eda-8F3C-2EB5A7D78C77") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LContratoNTx")> _
Public Class LContratoNTx
    Inherits ServicedComponent
    Implements IContratoNTx



#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LContratoNTx"
#End Region

#Region "Métodos"

#Region "Anexos"

    ''' <summary>
    ''' Devuelve el contrato especificado identificado por el número del crédito para la generación de los anexos.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function RetornarAnexoContrato(ByVal pCodigoContrato As String) As String Implements IContratoNTx.RetornarAnexoContrato
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.RetornarAnexoContrato(pCodigoContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function

    ''' <summary>
    ''' Retorna los datos de la tarifas a aplicar en el contrato
    ''' </summary>
    ''' <param name="pCodigoContrato"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function RetornarTarifarioContrato(ByVal pCodigoContrato As String) As String Implements IContratoNTx.RetornarTarifarioContrato
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.RetornarTarifario(pCodigoContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function

#End Region

#Region "Contrato"

    Public Function ContratoCuentas(ByVal codigoContrato As String) As String Implements IContratoNTx.ContratoCuentas

        Dim objContratoCuentaNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoCuentaNTx = New DContratoNTx
            resultado = objContratoCuentaNTx.ContratoCuentas(codigoContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoCuentaNTx.Dispose()
        End Try

        Return resultado

    End Function

    'INICIO IBK - RPR
    Function ObtenerContrato(ByVal codigoContrato As String) As String Implements IContratoNTx.ObtenerContrato
        Try
            Using oDContratoNTx As New DContratoNTx
                Return oDContratoNTx.ObtenerContrato(codigoContrato)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function ObtenerCuentasContrato(ByVal codigoContrato As String) As String Implements IContratoNTx.ObtenerCuentasContrato
        Try
            Using oDContratoNTx As New DContratoNTx
                Return oDContratoNTx.ObtenerCuentasContrato(codigoContrato)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'FIN IBK

    ''' <summary>
    ''' Devuelve los datos del cliente cuyos datos correponden con el código de contrato enviado.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function RetornarContratoDatosCliente(ByVal pCodigoContrato As String) As String Implements IContratoNTx.RetornarContratoDatosCliente
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.DatosCliente(pCodigoContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function

    ''' <summary>
    ''' Devuelve el contrato especificacado identificado por el número del crédito.
    ''' Incluye los datos de la tabla SolicitudCredito y GCC_ContratoOtroConcepto.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function RetornarContrato(ByVal pCodigoContrato As String) As String Implements IContratoNTx.RetornarContrato
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.Retornar(pCodigoContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function

    ''' <summary>
    ''' Listado de los contratos coincidentse con los criterios de búsqueda
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 09/05/2012
    ''' </remarks>
    Public Function ListadoContratos(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pContrato As String, _
                                     ByVal pCuCliente As String, _
                                     ByVal pRazonSocial As String, _
                                     ByVal pCotizacion As String, _
                                     ByVal pFechaIni As String, _
                                     ByVal pFechaFin As String, _
                                     ByVal pEjecutivo As String, _
                                     ByVal pEstado As String, _
                                     ByVal pZonal As String, _
                                     ByVal pClasificacion As String, _
                                     ByVal pClasificacionContrato As String, _
                                     ByVal pCodigoBanca As String, _
                                     ByVal pTipoPersona As String, _
                                     ByVal pNotaria As String, _
                                     ByVal pKardex As String) As String Implements IContratoNTx.ListadoContratos
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.ListadoContratos(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pContrato, _
                                                           pCuCliente, _
                                                           pRazonSocial, _
                                                           pCotizacion, _
                                                           pFechaIni, _
                                                           pFechaFin, _
                                                           pEjecutivo, _
                                                           pEstado, _
                                                           pZonal, _
                                                           pClasificacion, _
                                                           pClasificacionContrato, _
                                                           pCodigoBanca, _
                                                           pTipoPersona, _
                                                           pNotaria, _
                                                           pKardex)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function

    ''' <summary>
    ''' Listado de los contratos coincidentse con los criterios de búsqueda
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <remarks>
    ''' Creado Por         : JJM IBK
    ''' Fecha de Creación  : 25/04/2013
    ''' </remarks>
    Public Function ListadoContratos2(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pContrato As String, _
                                     ByVal pCuCliente As String, _
                                     ByVal pRazonSocial As String, _
                                     ByVal pCotizacion As String, _
                                     ByVal pFechaIni As String, _
                                     ByVal pFechaFin As String, _
                                     ByVal pEjecutivo As String, _
                                     ByVal pEstado As String, _
                                     ByVal pZonal As String, _
                                     ByVal pClasificacion As String, _
                                     ByVal pClasificacionContrato As String, _
                                     ByVal pCodigoBanca As String, _
                                     ByVal pTipoPersona As String, _
                                     ByVal pNotaria As String, _
                                     ByVal pKardex As String, _
                                     ByVal pEstadoOperacionActiva As String) As String Implements IContratoNTx.ListadoContratos2
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.ListadoContratos2(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pContrato, _
                                                           pCuCliente, _
                                                           pRazonSocial, _
                                                           pCotizacion, _
                                                           pFechaIni, _
                                                           pFechaFin, _
                                                           pEjecutivo, _
                                                           pEstado, _
                                                           pZonal, _
                                                           pClasificacion, _
                                                           pClasificacionContrato, _
                                                           pCodigoBanca, _
                                                           pTipoPersona, _
                                                           pNotaria, _
                                                           pKardex, _
                                                           pEstadoOperacionActiva)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function

    ''' <summary>
    ''' Listado de los contratos para desembolso coincidentse con los criterios de búsqueda
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 03/08/2012
    ''' </remarks>
    Public Function ListadoContratosDesembolso(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pContrato As String, _
                                     ByVal pCuCliente As String, _
                                     ByVal pRazonSocial As String, _
                                     ByVal pEjecutivo As String, _
                                     ByVal pEstado As String, _
                                     ByVal pClasificacion As String, _
                                     ByVal pCodigoSubTipoContrato As String, _
                                     ByVal pCodigoBanca As String, _
                                     ByVal pCodMoneda As String) As String Implements IContratoNTx.ListadoContratosDesembolso
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.ListadoContratosDesembolso(pPageSize, _
                                                                   pCurrentPage, _
                                                                   pSortColumn, _
                                                                   pSortOrder, _
                                                                   pContrato, _
                                                                   pCuCliente, _
                                                                   pRazonSocial, _
                                                                   pEjecutivo, _
                                                                   pEstado, _
                                                                   pClasificacion, _
                                                                   pCodigoSubTipoContrato, _
                                                                   pCodigoBanca, _
                                                                   pCodMoneda)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function

    Public Function ListadoSituacionCreditoContrato(ByVal pContrato As String, _
                                     ByVal pCuCliente As String, _
                                     ByVal pRazonSocial As String, _
                                     ByVal pCotizacion As String, _
                                     ByVal pFechaIni As String, _
                                     ByVal pFechaFin As String, _
                                     ByVal pEjecutivo As String, _
                                     ByVal pEstado As String, _
                                     ByVal pZonal As String, _
                                     ByVal pClasificacion As String, _
                                     ByVal pClasificacionContrato As String, _
                                     ByVal pCodigoBanca As String, _
                                     ByVal pTipoPersona As String, _
                                     ByVal pNotaria As String, _
                                     ByVal pKardex As String) As String Implements IContratoNTx.ListadoSituacionCreditoContrato
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.ListadoSituacionCreditoContrato(pContrato, pCuCliente, pRazonSocial, pCotizacion, pFechaIni, pFechaFin, _
                                                                        pEjecutivo, pEstado, pZonal, pClasificacion, pClasificacionContrato, pCodigoBanca, _
                                                                        pTipoPersona, pNotaria, pKardex)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function
    Public Function RetornarDatosContratoSituacionCredito(ByVal codigoContrato As String) As String Implements IContratoNTx.RetornarDatosContratoSituacionCredito
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.RetornarDatosContratoSituacionCredito(codigoContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function
    ''Inicio JJM IBK
    Public Function RetornarDatosCronogramaSituacionCreditoExcel(ByVal codigoContrato As String, ByVal fechavalor As String) As String Implements IContratoNTx.RetornarDatosCronogramaSituacionCreditoExcel
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.RetornarDatosCronogramaSituacionCreditoExcel(codigoContrato, fechavalor)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function
    Public Function RetornarDatosCronogramaPostSituacionCreditoExcel(ByVal codigoContrato As String, ByVal Usuario As String) As String Implements IContratoNTx.RetornarDatosCronogramaPostSituacionCreditoExcel
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.RetornarDatosCronogramaPostSituacionCreditoExcel(codigoContrato, Usuario)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function
    Public Function RetornarDatosCronogramaSituacionCreditoExcelHeader(ByVal codigoContrato As String) As String Implements IContratoNTx.RetornarDatosCronogramaSituacionCreditoExcelHeader
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.RetornarDatosCronogramaSituacionCreditoExcelHeader(codigoContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function
    Public Function RetornarDatosCronogramaSituacionCreditoExcelResumen(ByVal codigoContrato As String, ByVal Usuario As String) As String Implements IContratoNTx.RetornarDatosCronogramaSituacionCreditoExcelResumen
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.RetornarDatosCronogramaSituacionCreditoExcelResumen(codigoContrato, Usuario)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function
    Public Function RetornarDatosCronogramaSituacionCreditoExcelDetalle(ByVal codigoContrato As String, ByVal Usuario As String) As String Implements IContratoNTx.RetornarDatosCronogramaSituacionCreditoExcelDetalle
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.RetornarDatosCronogramaSituacionCreditoExcelDetalle(codigoContrato, Usuario)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function
    '' Fin JJM IBK
    Public Function RetornarDatosCronogramaSituacionCredito(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal codigoContrato As String) As String Implements IContratoNTx.RetornarDatosCronogramaSituacionCredito
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.RetornarDatosCronogramaSituacionCredito(pPageSize, pCurrentPage, pSortColumn, pSortOrder, codigoContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function
    Public Function RetornarDatosGastosSituacionCredito(ByVal pPageSize As Integer, _
                                          ByVal pCurrentPage As Integer, _
                                          ByVal pSortColumn As String, _
                                          ByVal pSortOrder As String, _
                                          ByVal codigoContrato As String) As String Implements IContratoNTx.RetornarDatosGastosSituacionCredito
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.RetornarDatosGastosSituacionCredito(pPageSize, pCurrentPage, pSortColumn, pSortOrder, codigoContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function
#End Region

#Region "ContratoNotarial"

    ''' <summary>
    ''' Listado de ContratoNotarial
    ''' </summary>
    ''' <param name="pNumeroContrato"></param>
    ''' <param name="pCodigoOrigenAdenda">[Notarial/Adenda]</param>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoContratoNotarial(ByVal pNumeroContrato As String, _
                                            ByVal pCodigoOrigenAdenda As String) As String Implements IContratoNTx.ListadoContratoNotarial
        Dim objContratoNotarialNTx As DContratoNotarialNTx = Nothing
        Dim resultado As String

        Try
            objContratoNotarialNTx = New DContratoNotarialNTx
            resultado = objContratoNotarialNTx.ListadoContratoNotarial(pNumeroContrato, _
                                                                       pCodigoOrigenAdenda)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNotarialNTx.Dispose()
        End Try

        Return resultado
    End Function

    ''' <summary>
    ''' Listado de ContratoNotarial
    ''' </summary>
    ''' <param name="pNumeroContrato"></param>
    ''' <param name="pCodigoOrigenAdenda">[Notarial/Adenda]</param>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoContratoNotarialPaginado(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pNumeroContrato As String, _
                                        ByVal pCodigoOrigenAdenda As String) As String Implements IContratoNTx.ListadoContratoNotarialPaginado
        Dim objContratoNotarialNTx As DContratoNotarialNTx = Nothing
        Dim resultado As String

        Try
            objContratoNotarialNTx = New DContratoNotarialNTx
            resultado = objContratoNotarialNTx.ListadoContratoNotarialPaginado(pPageSize, _
                                                                               pCurrentPage, _
                                                                               pSortColumn, _
                                                                               pSortOrder, _
                                                                               pNumeroContrato, _
                                                                               pCodigoOrigenAdenda)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNotarialNTx.Dispose()
        End Try

        Return resultado
    End Function

#End Region

#Region "Bien"

    ''' <summary>
    ''' Listado de bienes, por proveedor
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoBien(ByVal pPageSize As Integer, _
                                ByVal pCurrentPage As Integer, _
                                ByVal pSortColumn As String, _
                                ByVal pSortOrder As String, _
                                ByVal pCodsolicitudcredito As String, _
                                ByVal pCodProveedor As String) As String Implements IContratoNTx.ListadoBien
        Dim objBienNTx As DBienNTx = Nothing
        Dim resultado As String

        Try
            objBienNTx = New DBienNTx
            resultado = objBienNTx.Lista(pPageSize, _
                                         pCurrentPage, _
                                         pSortColumn, _
                                         pSortOrder, _
                                         pCodsolicitudcredito, _
                                         pCodProveedor)
        Catch ex As Exception
            Throw ex
        Finally
            objBienNTx.Dispose()
        End Try

        Return resultado
    End Function

#End Region

#Region "ContratoDocumento"

    ''' <summary>
    ''' Listado de ContratoNotarial
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function RetornarTextoPredefefinido(ByVal pCodigoContratoDocumento As Integer) As String Implements IContratoNTx.RetornarTextoPredefefinido
        Dim objContratoDocumentoNTx As DContratoDocumentoNTx = Nothing
        Dim resultado As String

        Try
            objContratoDocumentoNTx = New DContratoDocumentoNTx
            resultado = objContratoDocumentoNTx.RetornarTextoPredefefinido(pCodigoContratoDocumento)

            Return resultado
        Catch ex As Exception
            Throw ex
        Finally
            objContratoDocumentoNTx.Dispose()
        End Try
    End Function

#End Region

#Region "ContratoOtroConcepto"


#End Region

#Region "ContratoDocumentoObservacion"

    Public Function RetornarObservacionContratoDocumento(ByVal PEGccDocumentoObservacion As String) As String Implements IContratoNTx.RetornarObservacionContratoDocumento
        Dim objContratoDocumentoObservacionNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoDocumentoObservacionNTx = New DContratoNTx
            resultado = objContratoDocumentoObservacionNTx.RetornarObservacionContratoDocumento(PEGccDocumentoObservacion)

            Return resultado
        Catch ex As Exception
            Throw ex
        Finally
            objContratoDocumentoObservacionNTx.Dispose()
        End Try


    End Function
    'INICIO_JJM
    Public Function RetornarObservacionContratoDocumentoInafectacion(ByVal PEGccDocumentoObservacion As String) As String Implements IContratoNTx.RetornarObservacionContratoDocumentoInafectacion
        Dim objContratoDocumentoObservacionNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoDocumentoObservacionNTx = New DContratoNTx
            resultado = objContratoDocumentoObservacionNTx.RetornarObservacionContratoDocumentoInafectacion(PEGccDocumentoObservacion)

            Return resultado
        Catch ex As Exception
            Throw ex
        Finally
            objContratoDocumentoObservacionNTx.Dispose()
        End Try


    End Function

#End Region

#Region "ContratoProveedor"

    ''' <summary>
    ''' Listado de bienes, por proveedor
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoContratoProveedor(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pNumeroContrato As String) As String Implements IContratoNTx.ListadoContratoProveedor
        Dim objContratoProveedorNTx As DContratoProveedorNTx = Nothing
        Dim resultado As String

        Try
            objContratoProveedorNTx = New DContratoProveedorNTx
            resultado = objContratoProveedorNTx.Listado(pPageSize, _
                                                        pCurrentPage, _
                                                        pSortColumn, _
                                                        pSortOrder, _
                                                        pNumeroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoProveedorNTx.Dispose()
        End Try

        Return resultado
    End Function

    ''' <summary>
    ''' Listado de bienes, por proveedor (sin incluir repetidos).
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListarDistinctContratoProveedores(ByVal pPageSize As Integer, _
                                                      ByVal pCurrentPage As Integer, _
                                                      ByVal pSortColumn As String, _
                                                      ByVal pSortOrder As String, _
                                                      ByVal pNumeroContrato As String) As String Implements IContratoNTx.ListarDistinctContratoProveedores
        Dim objContratoProveedorNTx As DContratoProveedorNTx = Nothing
        Dim resultado As String

        Try
            objContratoProveedorNTx = New DContratoProveedorNTx
            resultado = objContratoProveedorNTx.ListarDistintos(pPageSize, _
                                                                pCurrentPage, _
                                                                pSortColumn, _
                                                                pSortOrder, _
                                                                pNumeroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoProveedorNTx.Dispose()
        End Try

        Return resultado
    End Function

#End Region

#Region "LSGConceptosTarifas"

    ''' <summary>
    ''' Retorna la tarifa predefinida para el contrato que corresponde al producto financiero indicado.
    ''' </summary>
    ''' <param name="CodProductoFinancieroActivo">Código del producto financiero</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 09/05/2012
    ''' </remarks>
    Public Function RetTarifarioPredefContrato(ByVal CodProductoFinancieroActivo As String, _
                                               ByVal CodMoneda As String) As String Implements IContratoNTx.RetTarifarioPredefContrato
        Dim objLSGConceptosTarifasNTx As DLSGConceptosTarifasNTx = Nothing
        Dim resultado As String

        Try
            objLSGConceptosTarifasNTx = New DLSGConceptosTarifasNTx
            resultado = objLSGConceptosTarifasNTx.RetornarTarifarioPredefContrato(CodProductoFinancieroActivo, _
                                                                                  CodMoneda)
        Catch ex As Exception
            Throw ex
        Finally
            objLSGConceptosTarifasNTx.Dispose()
        End Try

        Return resultado
    End Function

#End Region

#Region "Seguimiento Contrato"

    Public Function ListadoSeguimientoGlobal(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pCodContrato As String, _
                                         ByVal pCodCotizacion As String) As String Implements IContratoNTx.ListadoSeguimientoGlobal

        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.ListadoSeguimientoGlobal(pPageSize, _
                                                        pCurrentPage, _
                                                        pSortColumn, _
                                                        pSortOrder, _
                                                        pCodContrato, _
                                                        pCodCotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function

    Public Function ListadoSeguimientoContrato(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pNumeroContrato As String) As String Implements IContratoNTx.ListadoSeguimientoContrato
        Dim objContratoNTx As DContratoNTx = Nothing
        Dim resultado As String

        Try
            objContratoNTx = New DContratoNTx
            resultado = objContratoNTx.ListadoSeguimientoContrato(pPageSize, _
                                                        pCurrentPage, _
                                                        pSortColumn, _
                                                        pSortOrder, _
                                                        pNumeroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoNTx.Dispose()
        End Try

        Return resultado
    End Function
    'Public Function ListadoSeguimientoContrato(ByVal PEGccDocumentoObservacion As String) As String Implements IContratoNTx.ListadoSeguimientoContrato
    '    Dim objContratoDocumentoObservacionNTx As DContratoNTx = Nothing
    '    Dim resultado As String

    '    Try
    '        objContratoDocumentoObservacionNTx = New DContratoNTx
    '        resultado = objContratoDocumentoObservacionNTx.RetornarObservacionContratoDocumento(PEGccDocumentoObservacion)

    '        Return resultado
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objContratoDocumentoObservacionNTx.Dispose()
    '    End Try


    'End Function
#End Region

#Region "Representantes"

    ''' <summary>
    ''' Listado de Representantes del cliente para todos los contratos, excluyendo los del actual contrato.
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ListarDelCliente(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pCodigoContrato As String, _
                                     ByVal pCodUnico As String) As String Implements IContratoNTx.ListarDelCliente

        Dim objDRepresentanteNTx As DRepresentanteNTx = Nothing
        Dim strResultado As String

        Try
            objDRepresentanteNTx = New DRepresentanteNTx
            strResultado = objDRepresentanteNTx.ListarDelClienteSinSeleccionar(pPageSize, _
                                                                               pCurrentPage, _
                                                                               pSortColumn, _
                                                                               pSortOrder, _
                                                                               pCodigoContrato, _
                                                                               pCodUnico)
        Catch ex As Exception
            Throw ex
        Finally
            objDRepresentanteNTx.Dispose()
            objDRepresentanteNTx = Nothing
        End Try

        Return strResultado

    End Function

#End Region

#Region "Validar"
    ''' <summary>
    ''' Validar la Duplicidad de Condiciones 
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Código del objeto ContratoDocumento</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>

    Public Function SelCondicionesDocumentoCli(ByVal pEGcc_contratodocumento As String) As String Implements IContratoNTx.SelCondicionesDocumentoCli
        Dim objContratoDocumentoNTx As DContratoDocumentoNTx = Nothing
        Dim resultado As String

        Try
            objContratoDocumentoNTx = New DContratoDocumentoNTx
            resultado = objContratoDocumentoNTx.SelCondicionesDocumentoCli(pEGcc_contratodocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objContratoDocumentoNTx.Dispose()
        End Try

        Return resultado

    End Function



#End Region

#Region "Reporte Cotización-Contrato"

    ''' <summary>
    ''' Listado General de Contrato y Cotizacion para la generación de reportes.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la pagina</param>
    ''' <param name="pCurrentPage">Pagina Actual</param>
    ''' <param name="pSortColumn">Columna a Ordenar</param>
    ''' <param name="pSortOrder">Tipo de Ordenamiento</param>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - EBL
    ''' Fecha de Creacion : 16/05/2012
    ''' </remarks>
    Public Function ListadoContratoCotizacionRep(ByVal pPageSize As Integer, _
                                                 ByVal pCurrentPage As Integer, _
                                                 ByVal pSortColumn As String, _
                                                 ByVal pSortOrder As String, _
                                                 ByVal pECotizacion As String) As String Implements IContratoNTx.ListadoContratoCotizacionRep
        Try
            Using oDContratoNTx As New DContratoNTx
                Return oDContratoNTx.ListadoContratoCotizacionRep(pPageSize, _
                                                                  pCurrentPage, _
                                                                  pSortColumn, _
                                                                  pSortOrder, _
                                                                  pECotizacion)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Listado de Sunat - Contrato.
    ''' </summary>
    ''' <param name="pdFechaCelebracionIni">Fecha Celebración de Contrato Inicial</param>
    ''' <param name="pdFechaCelebracionFin">Fecha Celebración de Contrato Final</param>
    ''' <param name="pdFechaActivacionIni">Fecha de Activación Inicial</param>
    ''' <param name="pdFechaActivacionFin">Fecha de Activación Final</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 22/01/2013
    ''' </remarks>
    Public Function ListadoReporteSunatContratos(ByVal pdFechaCelebracionIni As DateTime, _
                                                 ByVal pdFechaCelebracionFin As DateTime, _
                                                 ByVal pdFechaActivacionIni As DateTime, _
                                                 ByVal pdFechaActivacionFin As DateTime) As String Implements IContratoNTx.ListadoReporteSunatContratos
        Try
            Using oDContratoNTx As New DContratoNTx
                Return oDContratoNTx.ListadoReporteSunatContratos(pdFechaCelebracionIni, _
                                                                  pdFechaCelebracionFin, _
                                                                  pdFechaActivacionIni, _
                                                                  pdFechaActivacionFin)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' Listado de Detalle del Bien Reporte
    ''' </summary>
    ''' <param name="pdFechaActivacionIni">Fecha de Activación Inicial</param>
    ''' <param name="pdFechaActivacionFin">Fecha de Activación Final</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 24/01/2013
    ''' </remarks>
    Public Function ListadoReporteDetalleBien(ByVal pdFechaActivacionIni As DateTime, _
                                              ByVal pdFechaActivacionFin As DateTime) As String Implements IContratoNTx.ListadoReporteDetalleBien
        Try
            Using oDContratoNTx As New DContratoNTx
                Return oDContratoNTx.ListadoReporteDetalleBien(pdFechaActivacionIni, _
                                                               pdFechaActivacionFin)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los bienes por contrato
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 29/01/2013 09:21:54 a.m. 
    ''' </remarks>
    Public Function fobjListadoSaldosCreditoReporte(ByVal pFechaInicio As String) As String Implements IContratoNTx.fobjListadoSaldosCreditoReporte
        Dim objContrato As DContratoNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objContrato = New DContratoNTx
            strResultado = objContrato.fobjListadoSaldosCreditoReporte(pFechaInicio)
        Catch ex As Exception
            Throw ex
        Finally
            If objContrato IsNot Nothing Then
                objContrato.Dispose()
                objContrato = Nothing
            End If
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Lista todos los bienes por contrato
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 05/03/2013 
    ''' </remarks>
    Public Function fobjListadoSaldosCreditoReporteDolares(ByVal pFechaInicio As String) As String Implements IContratoNTx.fobjListadoSaldosCreditoReporteDolares
        Dim objContrato As DContratoNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objContrato = New DContratoNTx
            strResultado = objContrato.fobjListadoSaldosCreditoReporteDolares(pFechaInicio)
        Catch ex As Exception
            Throw ex
        Finally
            If objContrato IsNot Nothing Then
                objContrato.Dispose()
                objContrato = Nothing
            End If
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Lista todos los Créditos Activos
    ''' </summary>
    ''' <param name="pTipo">Tipo de Periodo</param>
    ''' <param name="pFecha">Fecha de Periodo</param>
    ''' <param name="pCodigoClasificacionBien">Codigo Clasificación del Bien</param>
    ''' <param name="pCodigoTipoBien">Codigo del Tipo de Bien</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 29/01/2013 04:33:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoContratosActivadosReporte(ByVal pTipo As String, _
                                                         ByVal pFecha As String, _
                                                         ByVal pCodigoClasificacionBien As String, _
                                                         ByVal pCodigoTipoBien As String) As String Implements IContratoNTx.fobjListadoContratosActivadosReporte
        Dim objContrato As DContratoNTx = Nothing
        Dim strResultado As String = String.Empty

        Try
            objContrato = New DContratoNTx
            strResultado = objContrato.fobjListadoContratosActivadosReporte(pTipo, pFecha, pCodigoClasificacionBien, pCodigoTipoBien)
        Catch ex As Exception
            Throw ex
        Finally
            If objContrato IsNot Nothing Then
                objContrato.Dispose()
                objContrato = Nothing
            End If
        End Try

        Return strResultado
    End Function
#End Region



#End Region


End Class

#End Region
