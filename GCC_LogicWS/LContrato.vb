


#Region "Clase Transaccional"

Public Class LContratoTx

#Region "Contrato"

    ''' <summary>
    ''' Actualiza los datos del contrato durante la etapa de formalización.
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function fblnModificarContrato(ByVal pESolicitudCredito As String, _
                                          ByVal pEGcc_contratootroconcepto As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.fblnModificarContrato(pESolicitudCredito, _
                                                        pEGcc_contratootroconcepto)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Inicio IBK
    Public Function fblnModificarContrato2(ByVal pESolicitudCredito As String, _
                                          ByVal pEGcc_contratootroconcepto As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.fblnModificarContrato2(pESolicitudCredito, _
                                                        pEGcc_contratootroconcepto)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ContratoGuardarYEnviar2(ByVal pSolicitudCredito As String, _
                                           ByVal pEGcc_contratootroconcepto As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.ContratoGuardarYEnviar2(pSolicitudCredito, _
                                                         pEGcc_contratootroconcepto)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Fin IBK

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
    Public Function EnviarCarta(ByVal pSolicitudCredito As String, _
                                ByVal pEGCC_Carta As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.EnviarCarta(pSolicitudCredito, _
                                              pEGCC_Carta)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Envía la carta y actualiza el estado del contrato a estado enviado al cliente.
    ''' </summary>
    ''' <param name="pSolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ContratoGuardarYEnviar(ByVal pSolicitudCredito As String, _
                                           ByVal pEGcc_contratootroconcepto As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.ContratoGuardarYEnviar(pSolicitudCredito, _
                                                         pEGcc_contratootroconcepto)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los datos del contrato durante la etapa de formalización.
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ActualizaDocumentoSeparacion(ByVal pESolicitudCredito As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.ActualizaDocumentoSeparacion(pESolicitudCredito)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Actualiza el nombre del documento del contrato en la tabla Contrato (SolicitudCredito).
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ActualizaArchivoContratoAdjunto(ByVal pESolicitudCredito As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.ActualizaArchivoContratoAdjunto(pESolicitudCredito)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Aprobar(ByVal pSolicitudCredito As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.ActualizaEstado(pSolicitudCredito)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "ContratoNotarial"

    ''' <summary>
    ''' Ingresa nuevo objeto EGcc_contratonotarial
    ''' </summary>
    ''' <param name="pEGCC_ContratoNotarial">Objeto EGcc_contratonotarial(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function fintInsertarContratoNotarial(ByVal pESolicitudCredito As String, _
                                                 ByVal pEGCC_ContratoNotarial As String) As Integer
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.InsertarContratoNotarial(pESolicitudCredito, _
                                                           pEGCC_ContratoNotarial)
        Catch ex As Exception
            Throw ex
        End Try
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
    Public Function fblnModificarContratoNotarial(ByVal pESolicitudCredito As String, _
                                                  ByVal pEGCC_ContratoNotarial As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.ModificarContratoNotarial(pESolicitudCredito, _
                                                           pEGCC_ContratoNotarial)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Elimina una lista de objetos EGCC_ContratoNotarial, identificándolo con sus claves
    ''' CodigoNotarial y NumeroContrato.
    ''' </summary>
    ''' <param name="pListEGcc_contratonotarial">Lista de objetos EGcc_contratonotarial (Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 10/05/2012
    ''' </remarks>    
    Public Function fblnEliminarContratoNotarial(ByVal pESolicitudCredito As String, _
                                                 ByVal pListEGcc_contratonotarial As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.EliminarContratoNotarial(pESolicitudCredito, _
                                                           pListEGcc_contratonotarial)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Actualiza el nombre del archivo adjunto en la tabla GCC_ContratoNotarial.
    ''' </summary>
    ''' <param name="pEGCC_ContratoNotarial">Objeto EGCC_ContratoNotarial (Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ContratoNotarialActualizarNombreArchivo(ByVal pESolicitudCredito As String, _
                                                            ByVal pEGCC_ContratoNotarial As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.ContratoNotarialActualizarNombreArchivo(pESolicitudCredito, _
                                                                          pEGCC_ContratoNotarial)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Adenda"

    ''' <summary>
    ''' Ingresa nuevo objeto EGcc_contratonotarial
    ''' </summary>
    ''' <param name="pEGCC_ContratoNotarial">Objeto EGcc_contratonotarial(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function fintInsertarAdenda(ByVal pEGCC_ContratoNotarial As String) As Integer
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.InsertarAdenda(pEGCC_ContratoNotarial)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Modifica EGCC_ContratoNotarial, cuando contiene una adenda.
    ''' </summary>
    ''' <param name="pEGCC_ContratoNotarial">Objeto EGCC_ContratoNotarial (Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function fblnModificarAdenda(ByVal pEGCC_ContratoNotarial As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.ModificarAdenda(pEGCC_ContratoNotarial)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Elimina una lista de objetos EGCC_ContratoNotarial, identificándolo con sus claves
    ''' CodigoNotarial y NumeroContrato.
    ''' </summary>
    ''' <param name="pListEGcc_contratonotarial">Lista de objetos EGcc_contratonotarial (Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 10/05/2012
    ''' </remarks>    
    Public Function fblnEliminarAdenda(ByVal pListEGcc_contratonotarial As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.EliminarAdenda(pListEGcc_contratonotarial)
        Catch ex As Exception
            Throw ex
        End Try
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
    Public Function fblnInsertarBien(ByVal pESolicitudCredito As String, _
                                     ByVal pESolicitudCreditoEstructura As String, _
                                     ByVal pESolicitudCreditoEstructuraCarac As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.InsertarBien(pESolicitudCredito, _
                                               pESolicitudCreditoEstructura, _
                                               pESolicitudCreditoEstructuraCarac)
        Catch ex As Exception
            Throw ex
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
    Public Function fblnModificarBien(ByVal pESolicitudCredito As String, _
                                      ByVal pESolicitudCreditoEstructura As String, _
                                      ByVal pESolicitudCreditoEstructuraCarac As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.ModificarBien(pESolicitudCredito, _
                                                pESolicitudCreditoEstructura, _
                                                pESolicitudCreditoEstructuraCarac)
        Catch ex As Exception
            Throw ex
        End Try
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
    Public Function fblnEliminarBien(ByVal pESolicitudCredito As String, _
                                     ByVal pESolicitudCreditoEstructuraLst As String) As Boolean
        Dim objLTemporalTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLTemporalTx.EliminarBien(pESolicitudCredito, _
                                               pESolicitudCreditoEstructuraLst)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "ContratoDocumento"

    ''' <summary>
    ''' Modifica el texto predefinido de un objeto EGCC_ContratoDocumento.
    ''' </summary>
    ''' <param name="pESolicitudCredito">Número ESolicitudCredito (Serializado)</param>
    ''' <param name="pEGCC_ContratoDocumento">Objeto EGCC_ContratoDocumento(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 15/05/2012
    ''' </remarks> 
    Public Function fblnModificarTextoPredefinido(ByVal pESolicitudCredito As String, _
                                                  ByVal pEGCC_ContratoDocumento As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.ModificarTextoPredefinido(pESolicitudCredito, _
                                                            pEGCC_ContratoDocumento)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function EstadoAprobarLegal(ByVal pEGCC_ContratoDocumento As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.EstadoAprobarLegal(pEGCC_ContratoDocumento)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' InsertarContratoDocumentoObservacion
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentante"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertarContratoDocumentoObservacion(ByVal pEGcc_contratorepresentante As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")
        Try
            Return objLContratoTx.InsertarContratoDocumentoObservacion(pEGcc_contratorepresentante)
        Catch ex As Exception
            Throw ex
        Finally
            objLContratoTx.Dispose()
            objLContratoTx = Nothing
        End Try

    End Function
    ''' <summary>
    ''' InsertarContratoDocumentoObservacionInafectacion
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentante"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertarContratoDocumentoObservacionInafectacion(ByVal pEGcc_contratorepresentante As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")
        Try
            Return objLContratoTx.InsertarContratoDocumentoObservacionInafectacion(pEGcc_contratorepresentante)
        Catch ex As Exception
            Throw ex
        Finally
            objLContratoTx.Dispose()
            objLContratoTx = Nothing
        End Try

    End Function

    Function EliminaContratoDocumento(ByVal pEGcDocumentoContrato As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")
        Try
            Return objLContratoTx.EliminaContratoDocumento(pEGcDocumentoContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objLContratoTx.Dispose()
            objLContratoTx = Nothing
        End Try
    End Function

#End Region

#Region "ContratoOtroConcepto"

    ''' <summary>
    ''' Modifica GCC_ContratoOtroConcepto
    ''' </summary>
    ''' <param name="pGCC_ContratoOtroConcepto">Objeto GCC_ContratoOtroConcepto(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 15/05/2012
    ''' </remarks> 
    Public Function fblnModificarContratoOtroConcepto(ByVal pGCC_ContratoOtroConcepto As String) As Boolean
        Dim objLContratoOtroConceptoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoOtroConceptoTx.fblnModificarContratoOtroConcepto(pGCC_ContratoOtroConcepto)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Actualizar Adjunto de Documentos/Condiciones
    ''' </summary>
    ''' <param name="PGccContratoOtroConcepto">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoOtroConceptoAdjuntoUpd(ByVal pESolicitudCredito As String, _
                                                   ByVal PGccContratoOtroConcepto As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")
        Try
            Return objLContratoTx.ContratoOtroConceptoAdjuntoUpd(pESolicitudCredito, _
                                                                 PGccContratoOtroConcepto)
        Catch ex As Exception
            Throw ex
        Finally
            objLContratoTx.Dispose()
            objLContratoTx = Nothing
        End Try
    End Function


#End Region

#Region "CheckListComercial"
    ''' <summary>
    ''' Actualiza Check Lista Comercial
    ''' </summary>
    ''' <param name="pEGcc_checklisComercialBien">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : IJM
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>

    Public Function CheckLisComercialUpd(ByVal pEGcc_checklisComercialBien As String, _
                                         ByVal pEGcc_checklisComercialCuentas As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")
        Try
            Return objLContratoTx.CheckLisComercialUpd(pEGcc_checklisComercialBien, pEGcc_checklisComercialCuentas)

        Catch ex As Exception
            Throw ex
        Finally
            objLContratoTx.Dispose()
            objLContratoTx = Nothing
        End Try
    End Function
    Public Function GestionComercialEnviarUpd(ByVal pEGcc_Contrato As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LCheckListTx")
        Try
            Return objLContratoTx.GestionComercialEnviarUpd(pEGcc_Contrato)

        Catch ex As Exception
            Throw ex
        Finally
            objLContratoTx.Dispose()
            objLContratoTx = Nothing
        End Try
    End Function

    'Inicio IBK - AAE - 12/02/2013 - Agrego nueva operacion
    Public Function CheckLisComercialUpd2(ByVal pEGcc_checklisComercialBien As String, _
                                        ByVal pEGcc_checklisComercialCuentas As String, _
                                        ByVal pEgcc_cotizacion As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")
        Try
            Return objLContratoTx.CheckLisComercialUpd2(pEGcc_checklisComercialBien, pEGcc_checklisComercialCuentas, pEgcc_cotizacion)

        Catch ex As Exception
            Throw ex
        Finally
            objLContratoTx.Dispose()
            objLContratoTx = Nothing
        End Try
    End Function
    'Fin IBK



#End Region

#Region "Seguimiento Contrato"
    Public Function InsertaSeguimientoContrato(ByVal pEGcc_SeguimientoContrato As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.InsertaSeguimientoContrato(pEGcc_SeguimientoContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "ContratoRepresentante"

    ''' <summary>
    ''' Ingresa un nuevo representante en la tabla representantes y para los representantes del contrato.
    ''' </summary>
    ''' <param name="pEGcc_representante">Entidad pEGcc_representante serializada</param>
    ''' <param name="pEGcc_contratorepresentante">Entidad EGcc_contratootroconcepto serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function RepresentanteContratoIns(ByVal pEGcc_representante As String, _
                                             ByVal pEGcc_contratorepresentante As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.RepresentanteContratoIns(pEGcc_representante, _
                                                           pEGcc_contratorepresentante)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Ingresa un nuevo representante en la tabla representantes y para los representantes del contrato.
    ''' </summary>
    ''' <param name="pEGcc_representante">Entidad pEGcc_representante serializada</param>
    ''' <param name="pEGcc_contratorepresentante">Entidad EGcc_contratootroconcepto serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function RepresentanteClienteContratoIns(ByVal pESolicitudCredito As String, _
                                                    ByVal pEGcc_representante As String, _
                                                    ByVal pEGcc_contratorepresentante As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")

        Try
            Return objLContratoTx.RepresentanteContratoIns(pESolicitudCredito, _
                                                           pEGcc_representante, _
                                                           pEGcc_contratorepresentante)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    'Inicio-JJM
    Function ArchivoAdjuntoAfectoUpd(ByVal pEGcc_contratodocumento As String) As Boolean
        Dim objLContratoTx As Object = CreateObject("GCC.Logic.LContratoTx")
        Try
            Return objLContratoTx.ContratoDocumentoAdjuntoUpd(pEGcc_contratodocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objLContratoTx.Dispose()
            objLContratoTx = Nothing
        End Try
    End Function
    'Fin-JJM

End Class

#End Region

#Region "Clase No Transaccional"

Public Class LContratoNTx

#Region "Contrato"

    ''' <summary>
    ''' Devuelve los datos del cliente cuyos datos corresponden con el código de contrato enviado.
    ''' </summary>
    ''' <param name="pCodigoContrato">Código (número) de contrato.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function RetornarContratoDatosCliente(ByVal pCodigoContrato As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.RetornarContratoDatosCliente(pCodigoContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pCodigoContrato">Código (número) de contrato.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RetornarContratoCuentas(ByVal pCodigoContrato As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")
        Try
            Return objLContratoNTx.ContratoCuentas(pCodigoContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Inicio IBK RPR
    'Obtener Datos de SolicitudCredito y OperacionActiva
    Public Function ObtenerContrato(ByVal pCodigoContrato As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")
        Try
            Return objLContratoNTx.ObtenerContrato(pCodigoContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Cuentas Asociadas al Contrato (ContratoCuenta)
    Public Function ObtenerCuentasContrato(ByVal pCodigoContrato As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")
        Try
            Return objLContratoNTx.ObtenerCuentasContrato(pCodigoContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Fin IBK

    Public Function RetornarDocumentoObservacion(ByVal PEGccDocumentoObservacion As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")
        Try
            Return objLContratoNTx.RetornarObservacionContratoDocumento(PEGccDocumentoObservacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'INICIO_JJM
    Public Function RetornarObservacionContratoDocumentoInafectacion(ByVal PEGccDocumentoObservacion As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")
        Try
            Return objLContratoNTx.RetornarObservacionContratoDocumentoInafectacion(PEGccDocumentoObservacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'FIN_JJM

    ''' <summary>
    ''' Devuelve el contrato especificacado identificado por el número del crédito.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function RetornarContrato(ByVal pCodigoContrato As String) As String
        Dim objLTemporalNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLTemporalNTx.RetornarContrato(pCodigoContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Listado de los contratos coincidentse con los criterios de búsqueda
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pContrato">Código (número) de contrato. Dato o parte de él (opcional).</param>
    ''' <param name="pCuCliente">Código del cliente. Dato o parte de él (opcional).</param>
    ''' <param name="pRazonSocial">Nombre o razón social del cliente. Dato o parte de él (opcional). </param>
    ''' <param name="pCotizacion">Código de la cotización</param>
    ''' <param name="pFechaIni">Fecha inicial de registro del contrato. Dato (opcional).</param>
    ''' <param name="pFechaFin">Fecha final de registro del contrato. Dato (opcional).</param>
    ''' <param name="pEjecutivo">Código del ejecutivo de leasing</param>
    ''' <param name="pEstado">Código del estado en el cual se encuentra el contrato. Dato (opcional).</param>
    ''' <param name="pZonal">Código zonal</param>
    ''' <param name="pClasificacion"></param>
    ''' <param name="pClasificacionContrato">Código de clasificación del contrato</param>
    ''' <param name="pCodigoBanca">Código de Banca</param>
    ''' <param name="pTipoPersona"></param>
    ''' <param name="pNotaria">Código notarial</param>
    ''' <param name="pKardex">Número de kardex</param>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <remarks>
    ''' Creado Por         : JJM - IBK
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
                                     ByVal pEstadoOperacionActiva As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.ListadoContratos2(pPageSize, _
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
                                                    pKardex, pEstadoOperacionActiva)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Listado de los contratos coincidentse con los criterios de búsqueda
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pContrato">Código (número) de contrato. Dato o parte de él (opcional).</param>
    ''' <param name="pCuCliente">Código del cliente. Dato o parte de él (opcional).</param>
    ''' <param name="pRazonSocial">Nombre o razón social del cliente. Dato o parte de él (opcional). </param>
    ''' <param name="pCotizacion">Código de la cotización</param>
    ''' <param name="pFechaIni">Fecha inicial de registro del contrato. Dato (opcional).</param>
    ''' <param name="pFechaFin">Fecha final de registro del contrato. Dato (opcional).</param>
    ''' <param name="pEjecutivo">Código del ejecutivo de leasing</param>
    ''' <param name="pEstado">Código del estado en el cual se encuentra el contrato. Dato (opcional).</param>
    ''' <param name="pZonal">Código zonal</param>
    ''' <param name="pClasificacion"></param>
    ''' <param name="pClasificacionContrato">Código de clasificación del contrato</param>
    ''' <param name="pCodigoBanca">Código de Banca</param>
    ''' <param name="pTipoPersona"></param>
    ''' <param name="pNotaria">Código notarial</param>
    ''' <param name="pKardex">Número de kardex</param>
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
                                     ByVal pKardex As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.ListadoContratos(pPageSize, _
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
        End Try
    End Function

    ''' <summary>
    ''' Listado de los contratos coincidentse con los criterios de búsqueda
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pContrato">Código del contrato.</param>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 09/05/2012
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
                                     ByVal pCodMoneda As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.ListadoContratosDesembolso(pPageSize, _
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
        End Try
    End Function

    ''' <summary>
    ''' Retorna los datos de la tarifas a aplicar en el contrato
    ''' </summary>
    ''' <param name="codigoContrato">Código del contrato.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function RetornarTarifarioContrato(ByVal codigoContrato As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.RetornarTarifarioContrato(codigoContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Retorna los datos de la tarifas a aplicar para el tipo de contrato seleccionado.
    ''' </summary>
    ''' <param name="CodProductoFinancieroActivo">Código del producto financiero.</param>
    ''' <param name="CodMoneda">Código de la moneda del crédito</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function RetTarifarioPredefContrato(ByVal CodProductoFinancieroActivo As String, _
                                               ByVal CodMoneda As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.RetTarifarioPredefContrato(CodProductoFinancieroActivo, _
                                                              CodMoneda)
        Catch ex As Exception
            Throw ex
        End Try
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
                                     ByVal pKardex As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.ListadoSituacionCreditoContrato(pContrato, pCuCliente, pRazonSocial, pCotizacion, pFechaIni, pFechaFin, _
                                                                        pEjecutivo, pEstado, pZonal, pClasificacion, pClasificacionContrato, pCodigoBanca, _
                                                                        pTipoPersona, pNotaria, pKardex)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RetornarDatosContratoSituacionCredito(ByVal pCodigoContrato As String) As String
        Dim objLTemporalNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLTemporalNTx.RetornarDatosContratoSituacionCredito(pCodigoContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RetornarDatosCronogramaSituacionCredito(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal codigoContrato As String) As String
        Dim objLTemporalNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLTemporalNTx.RetornarDatosCronogramaSituacionCredito(pPageSize, pCurrentPage, pSortColumn, pSortOrder, codigoContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Inicio IBK JJM
    Public Function RetornarDatosCronogramaSituacionCreditoExcel(ByVal codigoContrato As String, ByVal fechavalor As String) As String
        Dim objLTemporalNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLTemporalNTx.RetornarDatosCronogramaSituacionCreditoExcel(codigoContrato, fechavalor)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function RetornarDatosCronogramaPostSituacionCreditoExcel(ByVal codigoContrato As String, ByVal Usuario As String) As String
        Dim objLTemporalNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLTemporalNTx.RetornarDatosCronogramaPostSituacionCreditoExcel(codigoContrato, Usuario)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function RetornarDatosCronogramaSituacionCreditoExcelHeader(ByVal codigoContrato As String) As String
        Dim objLTemporalNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLTemporalNTx.RetornarDatosCronogramaSituacionCreditoExcelHeader(codigoContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function RetornarDatosCronogramaSituacionCreditoExcelResumen(ByVal codigoContrato As String, ByVal Usuario As String) As String
        Dim objLTemporalNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLTemporalNTx.RetornarDatosCronogramaSituacionCreditoExcelResumen(codigoContrato, Usuario)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function RetornarDatosCronogramaSituacionCreditoExcelDetalle(ByVal codigoContrato As String, ByVal Usuario As String) As String
        Dim objLTemporalNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLTemporalNTx.RetornarDatosCronogramaSituacionCreditoExcelDetalle(codigoContrato, Usuario)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Fin IBK JJM
    Public Function RetornarDatosGastosSituacionCredito(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal codigoContrato As String) As String
        Dim objLTemporalNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLTemporalNTx.RetornarDatosGastosSituacionCredito(pPageSize, pCurrentPage, pSortColumn, pSortOrder, codigoContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "ContratoNotarial"

    ''' <summary>
    ''' Listado de los datos notariales relacionados con el contrato
    ''' </summary>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <param name="pNumeroContrato">Código del contrato.</param>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 09/05/2012
    ''' </remarks>
    Public Function ListadoContratoNotarial(ByVal pNumeroContrato As String, _
                                            ByVal pCodigoOrigenAdenda As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.ListadoContratoNotarial(pNumeroContrato, _
                                                           pCodigoOrigenAdenda)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Listado de los datos notariales relacionados con el contrato.
    ''' Lo agrupa por paginas, con los criterios indicados en los parámetros.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pNumeroContrato">Código del contrato.</param>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 09/05/2012
    ''' </remarks>
    Public Function ListadoContratoNotarialPaginado(ByVal pPageSize As Integer, _
                                                    ByVal pCurrentPage As Integer, _
                                                    ByVal pSortColumn As String, _
                                                    ByVal pSortOrder As String, _
                                                    ByVal pNumeroContrato As String, _
                                                    ByVal pCodigoOrigenAdenda As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.ListadoContratoNotarialPaginado(pPageSize, _
                                                                   pCurrentPage, _
                                                                   pSortColumn, _
                                                                   pSortOrder, _
                                                                   pNumeroContrato, _
                                                                   pCodigoOrigenAdenda)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Bien"

    ''' <summary>
    ''' Verifica si la lista de bienes tiene una ubicacion distinta
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 15/05/2012
    ''' </remarks>
    Public Function EsMismaUbicacionYUso(ByVal dt As DataTable) As Boolean
        If dt.Rows.Count < 2 Then
            Return True
        Else
            Dim v(dt.Rows.Count - 1) As String
            Dim tmp As String

            For i As Integer = 0 To dt.Rows.Count - 1
                v(i) = dt.Rows(i).Item("Uso").ToString().Trim() + _
                       dt.Rows(i).Item("Ubicacion").ToString().Trim() + _
                       dt.Rows(i).Item("Provincia").ToString().Trim() + _
                       dt.Rows(i).Item("Departamento").ToString().Trim()
            Next i

            For i As Integer = 0 To dt.Rows.Count - 2
                tmp = v(i)

                For j As Integer = i + 1 To dt.Rows.Count - 1
                    If v(j) <> tmp Then
                        Return False
                    End If
                Next j
            Next i

            Return True
        End If
    End Function

    ''' <summary>
    ''' Listado de bienes asociados a un contrato.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pCodsolicitudcredito">Código (número) de contrato. Dato o parte de él (opcional).</param>
    ''' <param name="pCodProveedor">Código del proveedor.</param>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 15/05/2012
    ''' </remarks>
    Public Function ListarBienes(ByVal pPageSize As Integer, _
                                 ByVal pCurrentPage As Integer, _
                                 ByVal pSortColumn As String, _
                                 ByVal pSortOrder As String, _
                                 ByVal pCodsolicitudcredito As String, _
                                 ByVal pCodProveedor As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.ListadoBien(pPageSize, _
                                               pCurrentPage, _
                                               pSortColumn, _
                                               pSortOrder, _
                                               pCodsolicitudcredito, _
                                               Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Listado de bienes
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pCodsolicitudcredito">Código (número) de contrato. Dato o parte de él (opcional).</param>
    ''' <param name="pCodProveedor">Código del proveedor.</param>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 15/05/2012
    ''' </remarks>
    Public Function ListarBienesProveedor(ByVal pPageSize As Integer, _
                                          ByVal pCurrentPage As Integer, _
                                          ByVal pSortColumn As String, _
                                          ByVal pSortOrder As String, _
                                          ByVal pCodsolicitudcredito As String, _
                                          ByVal pCodProveedor As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.ListadoContratoProveedor(pPageSize, _
                                                            pCurrentPage, _
                                                            pSortColumn, _
                                                            pSortOrder, _
                                                            pCodsolicitudcredito, _
                                                            pCodProveedor)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Anexos"

    ''' <summary>
    ''' Devuelve el contrato especificado identificado por el número del crédito para la generación de los anexos.
    ''' </summary>
    ''' <param name="codigoContrato">Código del contrato.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function RetornarAnexoContrato(ByVal codigoContrato As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.RetornarAnexoContrato(codigoContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "ContratoDocumento"

    ''' <summary>
    ''' Modifica el texto predefinido de un objeto EGCC_ContratoDocumento.
    ''' </summary>
    ''' <param name="pCodigoContratoDocumento">Objeto EGCC_ContratoDocumento(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 15/05/2012
    ''' </remarks> 
    Public Function LeerTextoPredefinido(ByVal pCodigoContratoDocumento As Integer) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.RetornarTextoPredefefinido(pCodigoContratoDocumento)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "ContratoOtroConcepto"


#End Region

#Region "ContratoProveedor"

    ''' <summary>
    ''' Listado de proveedores del contrato, sin repetir por el número de bienes que relacionados al contrato.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pNumeroContrato">Código del contrato.</param>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 15/05/2012
    ''' </remarks>
    Public Function ListarDistinctContratoProveedores(ByVal pPageSize As Integer, _
                                                      ByVal pCurrentPage As Integer, _
                                                      ByVal pSortColumn As String, _
                                                      ByVal pSortOrder As String, _
                                                      ByVal pNumeroContrato As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.ListarDistinctContratoProveedores(pPageSize, _
                                                                     pCurrentPage, _
                                                                     pSortColumn, _
                                                                     pSortOrder, _
                                                                     pNumeroContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Listado de proveedores del contrato
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pNumeroContrato">Código del contrato.</param>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 15/05/2012
    ''' </remarks>
    Public Function ListadoContratoProveedor(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pNumeroContrato As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.ListadoContratoProveedor(pPageSize, _
                                                            pCurrentPage, _
                                                            pSortColumn, _
                                                            pSortOrder, _
                                                            pNumeroContrato)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region


    ''' <summary>
    ''' ListadoSeguimientoGlobal
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pNumeroContrato">Código del contrato.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListadoSeguimientoGlobal(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pCodContrato As String, _
                                            ByVal pCodCotizacion As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.ListadoSeguimientoGlobal(pPageSize, _
                                                              pCurrentPage, _
                                                              pSortColumn, _
                                                              pSortOrder, _
                                                              pCodContrato, _
                                                              pCodCotizacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' ListadoSeguimientoContrato
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pNumeroContrato">Código del contrato.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListadoSeguimientoContrato(ByVal pPageSize As Integer, _
                                                 ByVal pCurrentPage As Integer, _
                                                 ByVal pSortColumn As String, _
                                                 ByVal pSortOrder As String, _
                                                 ByVal pNumeroContrato As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.ListadoSeguimientoContrato(pPageSize, _
                                                              pCurrentPage, _
                                                              pSortColumn, _
                                                              pSortOrder, _
                                                              pNumeroContrato)
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
                                                 ByVal pdFechaActivacionFin As DateTime) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.ListadoReporteSunatContratos(pdFechaCelebracionIni, _
                                                              pdFechaCelebracionFin, _
                                                              pdFechaActivacionIni, _
                                                              pdFechaActivacionFin)
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
                                              ByVal pdFechaActivacionFin As DateTime) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLContratoNTx.ListadoReporteDetalleBien(pdFechaActivacionIni, _
                                                             pdFechaActivacionFin)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "ContratoRepresentante"

    ''' <summary>
    ''' Listado de Representantes
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pCodigoContrato">Código del contrato.</param>
    ''' <param name="pCodUnico">Código único del cliente.</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ListarRepresentesDelCliente(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pCodigoContrato As String, _
                                                ByVal pCodUnico As String) As String
        Dim objLLCheckListNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return objLLCheckListNTx.ListarDelCliente(pPageSize, _
                                                      pCurrentPage, _
                                                      pSortColumn, _
                                                      pSortOrder, _
                                                      pCodigoContrato, _
                                                      pCodUnico)
        Catch ex As Exception
            Throw ex
        Finally
            objLLCheckListNTx.Dispose()
            objLLCheckListNTx = Nothing
        End Try
    End Function

#End Region

#Region "Reporte Cotización-Contrato"

    ''' <summary>
    ''' Listado de las cotizaciones y contratos coincidentes con los criterios de búsqueda
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pECotizacion">Objeto serialización serializado.</param>
    ''' <returns>Devuelve un DataTable serializado, con el contenido de la consulta para la grilla.</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 09/05/2012
    ''' </remarks>
    Public Function ListadoContratoCotizacionRep(ByVal pPageSize As Integer, _
                                                 ByVal pCurrentPage As Integer, _
                                                 ByVal pSortColumn As String, _
                                                 ByVal pSortOrder As String, _
                                                 ByVal pECotizacion As String) As String
        Dim oLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")

        Try
            Return oLContratoNTx.ListadoContratoCotizacionRep(pPageSize, _
                                                              pCurrentPage, _
                                                              pSortColumn, _
                                                              pSortOrder, _
                                                              pECotizacion)
        Catch ex As Exception
            Throw ex
        Finally
            oLContratoNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 29/01/2013 09:29:54 a.m. 
    ''' </remarks>
    Public Function fobjListadoSaldosCreditoReporte(ByVal pFechaInicio As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")
        Dim strResultado As String = String.Empty

        Try
            strResultado = objLContratoNTx.fobjListadoSaldosCreditoReporte(pFechaInicio)
        Catch ex As Exception
            Throw ex
        Finally
            objLContratoNTx = Nothing
        End Try

        Return strResultado
    End Function
    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 05/03/2013. 
    ''' </remarks>
    Public Function fobjListadoSaldosCreditoReporteDolares(ByVal pFechaInicio As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")
        Dim strResultado As String = String.Empty

        Try
            strResultado = objLContratoNTx.fobjListadoSaldosCreditoReporteDolares(pFechaInicio)
        Catch ex As Exception
            Throw ex
        Finally
            objLContratoNTx = Nothing
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
    ''' Fecha de Creación  : 29/01/2013 04:36:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoContratosActivadosReporte(ByVal pTipo As String, _
                                                         ByVal pFecha As String, _
                                                         ByVal pCodigoClasificacionBien As String, _
                                                         ByVal pCodigoTipoBien As String) As String
        Dim objLContratoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")
        Dim strResultado As String = String.Empty

        Try
            strResultado = objLContratoNTx.fobjListadoContratosActivadosReporte(pTipo, pFecha, pCodigoClasificacionBien, pCodigoTipoBien)
        Catch ex As Exception
            Throw ex
        Finally
            objLContratoNTx = Nothing
        End Try

        Return strResultado
    End Function
#End Region

End Class

#End Region
