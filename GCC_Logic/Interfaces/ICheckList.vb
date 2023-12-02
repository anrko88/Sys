Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ICheckListTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("3127AEC2-2601-4616-8D45-A191B8AE83AE")> _
Public Interface ICheckListTx

    ''' <summary>
    ''' Modificar Contrato Cuenta
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad serializada</param>
    ''' <returns>Resultado Integer</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 08/05/2012
    ''' </remarks>
    Function ContratoCuentaUpd(ByVal pESolicitudCredito As String) As Boolean

    ''' <summary>
    ''' Insertar Documentos/Condiciones para el Contrato
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function ContratoDocumentoIns(ByVal pEGcc_contratodocumento As String) As Boolean

    ''' <summary>
    ''' Actualizar Adjunto de Documentos/Condiciones
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function ContratoDocumentoAdjuntoUpd(ByVal pEGcc_contratodocumento As String) As Boolean

    ''' <summary>
    ''' Actualizar Observacion de Documentos/Condiciones
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function ContratoDocumentoObsUpd(ByVal pEGcc_contratodocumento As String) As Boolean

    ''' <summary>
    ''' Actualizar Estado de Envio Carta de Documentos/Condiciones
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function ContratoDocumentoEnviaCartaUpd(ByVal pEGcc_contratodocumento As String) As Boolean

    ''' <summary>
    ''' Insertar Representantes
    ''' </summary>
    ''' <param name="pEGcc_Representante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function RepresentanteIns(ByVal pEGcc_Representante As String) As Integer

    ''' <summary>
    ''' Modificar Representantes
    ''' </summary>
    ''' <param name="pEGcc_Representante">Entidad serializada</param>
    ''' <returns>Resultado Integer</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function RepresentanteUpd(ByVal pEGcc_Representante As String) As Boolean

    Function EliminarRepresentante(ByVal pListEGcc_representante As String) As Boolean

    ''' <summary>
    ''' Insertar Contrato Representantes
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function RepresentanteContratoIns(ByVal pEGcc_contratorepresentante As String) As Integer

    ''' <summary>
    ''' Delete Contrato Representantes
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function RepresentanteContratoItemDel(ByVal pEGcc_contratorepresentante As String) As Boolean
    Function RepresentanteContratoDel(ByVal pEGcc_contratorepresentante As String) As Boolean

    ''' <summary>
    ''' Insertar una lista de Contrato Representantes
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentanteList">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function RepresentanteContratoListIns(ByVal pESolicitudCredito As String, _
                                          ByVal pEGcc_contratorepresentanteList As String) As Boolean

    ''' <summary>
    ''' RepresentanteContratoListDel
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentanteList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function RepresentanteContratoListDel(ByVal objESolicitudCredito As String, _
                                          ByVal pEGcc_contratorepresentanteList As String) As Boolean

    ''' <summary>
    ''' GestionComercialEnviarUpd
    ''' </summary>
    ''' <param name="pEGcc_Contrato"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GestionComercialEnviarUpd(ByVal pEGcc_Contrato As String) As Boolean

    ''' <summary>
    ''' ActualizaFlagAprobacionLegal
    ''' </summary>
    ''' <param name="pFlagAprobacionLegal"></param>
    ''' <param name="pNumeroContrato"></param>
    ''' <param name="pCodigoContratoDocumento"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ActualizaFlagAprobacionLegal(ByVal pFlagAprobacionLegal As Integer, ByVal pNumeroContrato As String, ByVal pCodigoContratoDocumento As Integer) As Boolean


End Interface

#End Region

#Region "Interface No Transaccional"


''' <summary>
''' Interfaz de métodos para la clase ICheckListNTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("046103D3-0869-4d04-B39C-F3DFF0ED49C7")> _
Public Interface ICheckListNTx


    ''' <summary>
    ''' Listado de Representantes
    ''' </summary>
    ''' <param name="pEGcc_ContratoCuenta">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function ContratoCuentaSel(ByVal pEGcc_ContratoCuenta As String) As String

    ''' <summary>
    ''' Consulta de Documentos de Contrato
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function ContratoDocumentoSel(ByVal pPageSize As Integer, _
                                               ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, _
                                               ByVal pSortOrder As String, _
                                               ByVal pEGcc_contratodocumento As String) As String

    ''' <summary>
    ''' Listado de Representantes
    ''' </summary>
    ''' <param name="pEGcc_representante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function RepresentantesSel(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pFirmaEn As String, _
                                                ByVal pEGcc_representante As String, _
                                                ByVal pNumeroContrato As String) As String

    ''' <summary>
    ''' Listado de Representantes por Contrato
    ''' </summary>
    ''' <param name="pEGcc_contratoRepresentante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function RepresentantesContratoSel(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEGcc_contratoRepresentante As String, _
                                                ByVal pFirma As String) As String


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pESolicitudCredito"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListadoCheckListComercial(ByVal pESolicitudCredito As String) As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pCodUnico"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function RepresentantesCliente(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pCodUnico As String) As String
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pNroDocumento"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function RepresentantesItem(ByVal pNroDocumento As String) As String

    ''' <summary>
    ''' Listado de documentos y condiciones por contrato
    ''' </summary>
    ''' <param name="p_numerocontrato">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 14/08/2012
    ''' </remarks>
    Function ContratoDocumentoGet(ByVal p_numerocontrato As String) As String

End Interface

#End Region
