Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ISolicitudCreditoEstructuraDocTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - KCC
''' Fecha de Creación  : 04/06/2012
''' </remarks>
<Guid("FA48691D-E5F0-4bf0-AE9E-B88C557C6627")> _
Public Interface ISolicitudCreditoEstructuraDocTx

    Function InsertarContratoEstructDoc(ByVal pEContratoEstructDoc As String) As Boolean
    Function ModificarContratoEstructDoc(ByVal pEContratoEstructDoc As String) As Boolean
    Function EliminarContratoEstructDoc(ByVal pEContratoEstructDoc As String) As Boolean
    Function ActualizarGrupoContratoEstruct(ByVal pXmlEContratoEstructDoc As String, _
                                           ByVal pstrNumeroIOWIO As String, _
                                           ByVal pintNroSecuenciaWIO As Integer) As Boolean
    Function InsertarContratoWIO(ByVal pstNroContrato As String, ByVal pstrNumeroInstruccion As String, ByVal pstrNroLinea As String) As Boolean
    Function ModificaEstadoDocumentoWS(ByVal strEGcc_desembolso As String, ByVal pTipoAprobacion As String) As Boolean
    Function ActualizarIGVDesembolso(ByVal strNumContrato As String) As Integer

End Interface

#End Region

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase ISolicitudCreditoEstructuraDocNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - KCC
''' Fecha de Creación  : 05/06/2012
''' </remarks>
<Guid("74F0B40D-C25E-452C-830F-5F34C82A11D3")> _
Public Interface ISolicitudCreditoEstructuraDocNTx
    Function ListarContratoEstructDoc(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEContratoEstructDoc As String) As String

    Function ListarContratoEstructDocConsulta(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEContratoEstructDoc As String) As String

    Function ListarContratoEstructDocAsociar(ByVal pPageSize As Integer, _
                                               ByVal pCurrentPage As Integer, _
                                               ByVal pSortColumn As String, _
                                               ByVal pSortOrder As String, _
                                               ByVal pEContratoEstructDoc As String) As String
    Function ValidaContratoEstructDoc(ByVal pEContratoEstructDoc As String) As String
    Function ObtenerContratoEstructDoc(ByVal pEContratoEstructDoc As String) As String
    Function ObtenerBienLeasingWIO(ByVal pXmlEContratoEstructDoc As String) As String
    Function ObtenerAgenteRetencion(ByVal pstrNroDocumento As String) As Integer

    'Inicio IBK
    Function ListaDocumentosContrato(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pNroContrato As String) As String
    'Fin IBK
End Interface

#End Region
