#Region "Clase Transaccional"

Public Class LCheckListTx

    ''' <summary>
    ''' Modificar Contrato Cuenta
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad serializada</param>
    ''' <returns>Resultado Integer</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 08/05/2012
    ''' </remarks>
    Public Function ContratoCuentaUpd(ByVal pESolicitudCredito As String) As Boolean
        Dim objLCheckListTx As Object = CreateObject("GCC.Logic.LCheckListTx")
        Try
            Return objLCheckListTx.ContratoCuentaUpd(pESolicitudCredito)
        Catch ex As Exception
            Throw ex
        Finally
            objLCheckListTx.Dispose()
            objLCheckListTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Insertar Documentos/Condiciones para el Contrato
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoDocumentoIns(ByVal pEGcc_contratodocumento As String) As Boolean
        Dim objLCheckListTx As Object = CreateObject("GCC.Logic.LCheckListTx")
        Try
            Return objLCheckListTx.ContratoDocumentoIns(pEGcc_contratodocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objLCheckListTx.Dispose()
            objLCheckListTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Actualizar Adjunto de Documentos/Condiciones
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoDocumentoAdjuntoUpd(ByVal pEGcc_contratodocumento As String) As Boolean
        Dim objLCheckListTx As Object = CreateObject("GCC.Logic.LCheckListTx")
        Try
            Return objLCheckListTx.ContratoDocumentoAdjuntoUpd(pEGcc_contratodocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objLCheckListTx.Dispose()
            objLCheckListTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Actualizar Observacion de Documentos/Condiciones
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoDocumentoObsUpd(ByVal pEGcc_contratodocumento As String) As Boolean
        Dim objLCheckListTx As Object = CreateObject("GCC.Logic.LCheckListTx")
        Try
            Return objLCheckListTx.ContratoDocumentoObsUpd(pEGcc_contratodocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objLCheckListTx.Dispose()
            objLCheckListTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Actualizar Estado de Envio Carta de Documentos/Condiciones
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoDocumentoEnviaCartaUpd(ByVal pEGcc_contratodocumento As String) As Boolean
        Dim objLCheckListTx As Object = CreateObject("GCC.Logic.LCheckListTx")
        Try
            Return objLCheckListTx.ContratoDocumentoEnviaCartaUpd(pEGcc_contratodocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objLCheckListTx.Dispose()
            objLCheckListTx = Nothing
        End Try
    End Function
    '(pFlagAprobacionLegal, pNumeroContrato, pCodigoContratoDocumento)
    Public Function ActualizaFlagAprobacionLegal(ByVal pFlagAprobacionLegal As Integer, ByVal pNumeroContrato As String, ByVal pCodigoContratoDocumento As Integer) As Boolean
        Dim objLCheckListTx As Object = CreateObject("GCC.Logic.LCheckListTx")
        Try
            Return objLCheckListTx.ActualizaFlagAprobacionLegal(pFlagAprobacionLegal, pNumeroContrato, pCodigoContratoDocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objLCheckListTx.Dispose()
            objLCheckListTx = Nothing
        End Try
    End Function


#Region "Representante"

    ''' <summary>
    ''' Insertar Representantes
    ''' </summary>
    ''' <param name="pEGcc_Representante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentanteIns(ByVal pEGcc_Representante As String) As Integer
        Dim objLCheckListTx As Object = CreateObject("GCC.Logic.LCheckListTx")
        Try
            Return objLCheckListTx.RepresentanteIns(pEGcc_Representante)
        Catch ex As Exception
            Throw ex
        Finally
            objLCheckListTx.Dispose()
            objLCheckListTx = Nothing
        End Try
    End Function




    ''' <summary>
    ''' Modificar Representantes
    ''' </summary>
    ''' <param name="pEGcc_Representante">Entidad serializada</param>
    ''' <returns>Resultado Integer</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentanteUpd(ByVal pEGcc_Representante As String) As Boolean
        Dim objLCheckListTx As Object = CreateObject("GCC.Logic.LCheckListTx")

        Try
            Return objLCheckListTx.RepresentanteUpd(pEGcc_Representante)
        Catch ex As Exception
            Throw ex
        Finally
            objLCheckListTx.Dispose()
            objLCheckListTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina un bien cuyas claves coinciden con las enviadas por parámetro, en las tablas EGcc_representante.
    ''' </summary>
    ''' <param name="pListEGcc_representante">Entidad ListEGcc_representante serializada</param>
    ''' <returns>True si se eliminó correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>   
    Public Function fblnEliminarRepresentante(ByVal pListEGcc_representante As String) As Boolean
        Dim objLCheckListTx As Object = CreateObject("GCC.Logic.LCheckListTx")

        Try
            Return objLCheckListTx.EliminarRepresentante(pListEGcc_representante)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Representante Contrato"

    ''' <summary>
    ''' Insertar una lista de Contrato Representantes
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentanteList">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentanteClienteContratoIns(ByVal pCodigoContrato As String, _
                                                    ByVal pEGcc_contratorepresentanteList As String) As Boolean
        Dim objLCheckListTx As Object = CreateObject("GCC.Logic.LCheckListTx")

        Try
            Return objLCheckListTx.RepresentanteClienteContratoIns(pCodigoContrato, _
                                                                   pEGcc_contratorepresentanteList)
        Catch ex As Exception
            Throw ex
        Finally
            objLCheckListTx.Dispose()
            objLCheckListTx = Nothing
        End Try
    End Function


    ''' <summary>
    ''' Insertar una lista de Contrato Representantes
    ''' </summary>
    ''' <param name="pCodigoContrato">Entidad contrato serializada</param>
    ''' <param name="pEGcc_contratorepresentanteList">Entidad lista de representantes serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentanteContratoListIns(ByVal pCodigoContrato As String, _
                                                 ByVal pEGcc_contratorepresentanteList As String) As Boolean
        Dim objLCheckListTx As Object = CreateObject("GCC.Logic.LCheckListTx")

        Try
            Return objLCheckListTx.RepresentanteContratoListIns(pCodigoContrato, _
                                                                pEGcc_contratorepresentanteList)
        Catch ex As Exception
            Throw ex
        Finally
            objLCheckListTx.Dispose()
            objLCheckListTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Insertar Contrato Representantes
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentanteContratoIns(ByVal pEGcc_contratorepresentante As String) As Integer
        Dim objLCheckListTx As Object = CreateObject("GCC.Logic.LCheckListTx")
        Try
            Return objLCheckListTx.RepresentanteContratoIns(pEGcc_contratorepresentante)
        Catch ex As Exception
            Throw ex
        Finally
            objLCheckListTx.Dispose()
            objLCheckListTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina Un representante por contrato
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentanteContratoItemDel(ByVal pEGcc_contratorepresentante As String) As Boolean
        Dim objLCheckListTx As Object = CreateObject("GCC.Logic.LCheckListTx")
        Try
            Return objLCheckListTx.RepresentanteContratoItemDel(pEGcc_contratorepresentante)
        Catch ex As Exception
            Throw ex
        Finally
            objLCheckListTx.Dispose()
            objLCheckListTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina Todos los Representantes por contrato
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentante"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RepresentanteContratoDel(ByVal pEGcc_contratorepresentante As String) As Boolean
        Dim objLCheckListTx As Object = CreateObject("GCC.Logic.LCheckListTx")
        Try
            Return objLCheckListTx.RepresentanteContratoDel(pEGcc_contratorepresentante)
        Catch ex As Exception
            Throw ex
        Finally
            objLCheckListTx.Dispose()
            objLCheckListTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina un bien cuyas claves coinciden con las enviadas por parámetro, en las tablas SolicitudCreditoEstructura y SolicitudCreditoEstructuraCarac.
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <param name="pListEGcc_contratorepresentante">Entidad ESolicitudCreditoEstructuraLst serializada</param>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>   
    Public Function RepresentanteContratoListDel(ByVal pESolicitudCredito As String, _
                                                 ByVal pListEGcc_contratorepresentante As String) As Boolean
        Dim objLCheckListTx As Object = CreateObject("GCC.Logic.LCheckListTx")

        Try
            Return objLCheckListTx.RepresentanteContratoListDel(pESolicitudCredito, _
                                                                pListEGcc_contratorepresentante)
        Catch ex As Exception
            Throw ex
        Finally
            objLCheckListTx.Dispose()
            objLCheckListTx = Nothing
        End Try
    End Function

#End Region

End Class

#End Region

#Region "Clase No Transaccional"

Public Class LCheckListNTx

    ''' <summary>
    ''' Verifica si el representante seleccionado se encuentra asociado con algún contrato diferente al enviado por parámetro.
    ''' </summary>
    ''' <param name="pESolicitudCredito"></param>
    ''' <param name="pEGccRepresentante"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function EsRepresentanteAsociadoAOtroContrato(ByVal pESolicitudCredito As String, _
                                                         ByVal pEGccRepresentante As String) As Boolean
        Dim objLTemporalNTx As Object = CreateObject("GCC.Logic.LCheckListNTx")

        Try
            Return objLTemporalNTx.EsRepresentanteAsociadoAOtroContrato(pESolicitudCredito, _
                                                                        pEGccRepresentante)
        Catch ex As Exception
            Throw ex
        Finally
            objLTemporalNTx.Dispose()
            objLTemporalNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Listado de Representantes
    ''' </summary>
    ''' <param name="pEGcc_ContratoCuenta">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoCuentaSel(ByVal pEGcc_ContratoCuenta As String) As String
        Dim objLTemporalNTx As Object = CreateObject("GCC.Logic.LTemporalNTx")
        Try
            Return objLTemporalNTx.ContratoCuentaSel(pEGcc_ContratoCuenta)
        Catch ex As Exception
            Throw ex
        Finally
            objLTemporalNTx.Dispose()
            objLTemporalNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Consulta de Documentos de Contrato
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la página</param>
    ''' <param name="pCurrentPage">Número de página a devolver.</param>
    ''' <param name="pSortColumn">Nombre de la columna por el que se va a contratar el contrato.</param>
    ''' <param name="pSortOrder">Criterio de ordenación de la columna. Posibles valores: 'Asc', 'Desc', ''.</param>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoDocumentoSel(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pEGcc_contratodocumento As String) As String
        Dim objLContratoDocumentoNtx As Object = CreateObject("GCC.Logic.LCheckListNTx")
        Try
            Return objLContratoDocumentoNtx.ContratoDocumentoSel(pPageSize, _
                                                                 pCurrentPage, _
                                                                 pSortColumn, _
                                                                 pSortOrder, _
                                                                 pEGcc_contratodocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objLContratoDocumentoNtx.Dispose()
            objLContratoDocumentoNtx = Nothing
        End Try
    End Function


#Region "Representantes"

    ''' <summary>
    ''' Listado de Representantes
    ''' </summary>
    ''' <param name="pEGcc_representante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentantesSel(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pfirma As String, _
                                                ByVal pEGcc_representante As String, _
                                                ByVal pNumeroContrato As String) As String
        Dim objLLCheckListNTx As Object = CreateObject("GCC.Logic.LCheckListNTx")
        Try
            Return objLLCheckListNTx.RepresentantesSel(pPageSize, _
                                                       pCurrentPage, _
                                                       pSortColumn, _
                                                       pSortOrder, _
                                                       pfirma, _
                                                       pEGcc_representante, _
                                                       pNumeroContrato)
        Catch ex As Exception
            Throw ex
        Finally
            objLLCheckListNTx.Dispose()
            objLLCheckListNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Listado de Representantes
    ''' </summary>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentantesCliente(ByVal pPageSize As Integer, _
                                          ByVal pCurrentPage As Integer, _
                                          ByVal pSortColumn As String, _
                                          ByVal pSortOrder As String, _
                                          ByVal pCodUnico As String) As String
        Dim objLLCheckListNTx As Object = CreateObject("GCC.Logic.LCheckListNTx")
        Try
            Return objLLCheckListNTx.RepresentantesCliente(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pCodUnico)
        Catch ex As Exception
            Throw ex
        Finally
            objLLCheckListNTx.Dispose()
            objLLCheckListNTx = Nothing
        End Try
    End Function

    ''' <summary>
    ''' RepresentantesItem
    ''' </summary>
    ''' <param name="pNroDocumento"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RepresentantesItem(ByVal pNroDocumento As String) As String
        Dim objLLCheckListNTx As Object = CreateObject("GCC.Logic.LCheckListNTx")
        Try
            Return objLLCheckListNTx.RepresentantesItem(pNroDocumento)
        Catch ex As Exception
            Throw ex
        Finally
            objLLCheckListNTx.Dispose()
            objLLCheckListNTx = Nothing
        End Try
    End Function

#End Region

#Region "RepresentantesContrato"

    ''' <summary>
    ''' Listado de Representantes por Contrato
    ''' </summary>
    ''' <param name="pEGcc_contratoRepresentante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentantesContratoSel(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEGcc_contratoRepresentante As String, _
                                                ByVal pFirma As String) As String

        Dim objLTemporalNTx As Object = CreateObject("GCC.Logic.LCheckListNTx")
        Try
            Return objLTemporalNTx.RepresentantesContratoSel(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pEGcc_contratoRepresentante, pFirma)
        Catch ex As Exception
            Throw ex
        Finally
            objLTemporalNTx.Dispose()
            objLTemporalNTx = Nothing
        End Try

    End Function

#End Region
#Region "Documentos"
    ''' <summary>
    ''' Lista de los documentos y condiciones del contrato
    ''' </summary>
    ''' <param name="p_numerocontrato">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 14/08/2012
    ''' </remarks>
    Public Function ContratoDocumentoGet(ByVal p_numerocontrato As String) As String
        Dim objLContratoDocumentoNtx As Object = CreateObject("GCC.Logic.LCheckListNTx")
        Try
            Return objLContratoDocumentoNtx.ContratoDocumentoGet(p_numerocontrato)
        Catch ex As Exception
            Throw ex
        Finally
            objLContratoDocumentoNtx.Dispose()
            objLContratoDocumentoNtx = Nothing
        End Try
    End Function
#End Region

#Region "Validar"
    ''' <summary>
    ''' Validar las Condiciones del Documento de Cliente
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function SelCondicionesDocumentoCli(ByVal pEGcc_contratodocumento As String) As String
        Dim objContratoDocumentoNTx As Object = CreateObject("GCC.Logic.LContratoNTx")
        Try
            Return objContratoDocumentoNTx.SelCondicionesDocumentoCli(pEGcc_contratodocumento)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class

#End Region
