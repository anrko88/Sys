Imports System.Runtime.InteropServices


#Region "Interface Transaccional"
''' <summary>
''' Interfaz de métodos para la clase IContratoDocumentoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("167709EE-2D3B-49df-8483-EA25B4911368")> _
Public Interface IContratoDocumentoTx

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
    ''' Creado Por         : JJM
    ''' Fecha de Creación  : 07/11/2012
    ''' </remarks>
    Function ArchivoAdjuntoAfectoUpd(ByVal pEGcc_contratodocumento As String) As Boolean
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

    Function ModificarTextoPredefinido(ByVal pEGCC_ContratoDocumento As String) As Boolean

    Function InsertarContratoDocCliente(ByVal pEGcc_contratodocumento As String) As Integer
    Function EliminarContratoDocCliente(ByVal pEGcc_contratodocumento As String) As Boolean
    Function InsertarContratoDocumentoObservacion(ByVal pEGcc_contratodocumentoObservacion As String) As Boolean
    'InicioJJM
    Function InsertarContratoDocumentoObservacionInafectacion(ByVal pEGcc_contratodocumentoObservacion As String) As Boolean
    'FinJJM
    Function EliminaContratoDocumento(ByVal pEGcc_contratodocumento As String) As Boolean
    Function EstadoAprobarLegal(ByVal pEGcc_contratodocumento As String) As Boolean

    ''' <summary>
    ''' Envia Carta Documento Cliente
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Contrato Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - Jrc
    ''' Fecha de Creación  : 24/05/2012
    ''' </remarks>
    Function EnviarCartaDocumentoCliente(ByVal pEGcc_contratodocumento As String) As Boolean
    Function ActualizaFlagAprobacionLegal(ByVal pFlagAprobacionLegal As Integer, ByVal pNumeroContrato As String, ByVal pCodigoContratoDocumento As Integer) As Boolean


End Interface

#End Region

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase ContratoDocumentoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("A4CE9C1A-351A-464a-A973-7305D7EFA7F9")> _
Public Interface IContratoDocumentoNTx

    Function RetornarTextoPredefefinido(ByVal pCodigoContratoDocumento As Integer) As String

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
    ''' Valida la duplicidad de las Condiciones del Documento
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : 
    ''' Fecha de Creación  : 
    ''' </remarks>
    Function SelCondicionesDocumentoCli(ByVal pEGcc_contratodocumento As String) As String


    ''' <summary>
    ''' Lista los documentos y condiciones del contrato
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : TSF-AEP
    ''' Fecha de Creación  : 14/08/2012
    ''' </remarks>
    Function ContratoDocumentoGet(ByVal p_numerocontrato As String) As String

End Interface

#End Region

