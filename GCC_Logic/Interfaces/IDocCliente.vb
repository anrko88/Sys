Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IDocClienteTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("A4C513FF-CA0C-44a5-81B0-1AE78BF9D451")> _
Public Interface IDocClienteTx
    Function fblnGuardarVerificacionCliente(ByVal pEContrato As String, ByVal pEContacto As String, ByVal pstrOpcion As String) As Boolean
    Function AgregarDocCondCliente(ByVal pstrContratoDoc As String) As Integer
    Function EliminarDocCondCliente(ByVal pstrContratoDoc As String) As Boolean
    Function EnviarCartaDocumentoCliente(ByVal pEGcc_contratodocumento As String) As Boolean
End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IDocClienteNTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("2CE38496-C701-45ce-9F60-5846D00CF05C")> _
Public Interface IDocClienteNTx
    Function ObtenerContacto(ByVal pstrCodCotizacion As String, ByVal pstrCodContrato As String) As String
    Function ListadoContratoCotizacion(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal pECotizacion As String) As String
    Function ObtenerContratoCotizacion(ByVal pNumeroContrato As String) As String
    ' Inicio IBK - AAE - 03/10/2012 Se agrega método para listar contratocotización en sol Docs
    Function ListadoContratoCotizacionSolDoc(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal pECotizacion As String) As String
    ' fin IBK
End Interface

#End Region