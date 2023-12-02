Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IDesembolsoTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("3510D544-8DDB-4e6e-AA00-90C542FD2B00")> _
Public Interface IDesembolsoTx
    Function InsertarContratoEstructDoc(ByVal pEContratoEstructDoc As String) As Boolean
    Function ModificarContratoEstructDoc(ByVal pEContratoEstructDoc As String) As Boolean
    Function EliminarContratoEstructDoc(ByVal pEContratoEstructDoc As String) As Boolean
    Function InsertarContratoEstructDocDet(ByVal pEContratoEstructDocDet As String) As Boolean
    Function EliminarContratoEstructDocDet(ByVal pEContratoEstructDocDet As String) As Boolean
    Function RegistrarWIO(ByVal pstrClienteWIO As String, _
                          ByVal pstrInstruccionWIO As String, _
                          ByVal pstrCaracteristicasWIO As String, _
                          ByVal pstrSeguimientoWIO As String, _
                          ByVal pxmlCtaCargoLeasing As String, _
                          ByVal pxmlBienLeasing As String, _
                          ByVal pXmlEContratoEstructDoc As String, _
                          ByVal pstrNroContrato As String, _
                          ByVal pstrNroLineaOp As String, _
                          ByVal pstrEsActivacion As String) As String

    Function RegistrarLineaWIO(ByVal pstrLineaOPWIO As String, _
                                    ByVal pstrTasaComisionWIO As String, _
                                    ByVal pstrNroInstruccion As String) As Boolean

    Function ModificaEstadoDocumentoWS(ByVal strEGcc_desembolso As String, ByVal pTipoAprobacion As String) As Boolean

    Function ActualizarIGVDesembolso(ByVal strNumContrato As String) As Integer

    'Inicio IBK
    Function AgregarRelacion(ByVal pstrNroContrato As String, _
                             ByVal pstrSecBien As String, _
                             ByVal pstrArrayDocs As String, _
                             ByVal nbrArraySize As Integer) As Boolean
    'Fin IBK

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IDesembolsoNTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("3D8D69C7-747C-482e-8331-DE92102474E4")> _
Public Interface IDesembolsoNTx
    Function ListadoContratoEstructDocDet(ByVal pPageSize As Integer, _
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
    Function ObtenerContratoEstructDoc(ByVal pEContratoEstructDocc As String) As String
    Function ObtenerBienLeasingWIO(ByVal pXmlEContratoEstructDoc As String) As String
    Function ObtenerCtaLeasingWioSel(ByVal pstrNroContrato As String) As String
    Function ObtenerAgenteRetencion(ByVal pstrNroDocumento As String) As Integer
    'Inicio IBK
    Function ListaBienesContrato(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pNroContrato As String) As String

    Function ListaDocumentosContrato(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pNroContrato As String) As String

    Function ListaRelacionesContrato(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pNroContrato As String) As String
    'Fin IBK
End Interface

#End Region

