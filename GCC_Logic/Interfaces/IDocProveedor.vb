
Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IDocProveedorTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("665428B9-035C-49d9-8D64-BE5F771C60EB")> _
Public Interface IDocProveedorTx
    Function InsertarProveedor(ByVal pEProveedor As String) As Boolean
    Function ModificarProveedor(ByVal pEProveedor As String) As Boolean
    Function InsertarContratoProveedor(ByVal pEContratoProveedor As String) As Integer
    Function ModificarContratoProveedor(ByVal pEContratoProveedor As String) As Integer
    Function fblnEliminarContratoProveedor(ByVal pEContratoProveedor As String) As Boolean
    Function ModificaSolicitudDocumentoProveedor(ByVal pESolicitudCredito As String) As Boolean
    Function EnviarCartaDocumentoProveedor(ByVal pEGcc_contratoProveedor As String) As Boolean


End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IDocProveedorNTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("5137BE09-DB01-4cf4-8931-899068C50CC7")> _
Public Interface IDocProveedorNTx
    Function ObtenerProveedor(ByVal pstrCodProveedor As String) As String
    Function ListadoProveedor(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pCodigoTipoDocumento As String, _
                                    ByVal pNumeroDocumento As String, _
                                    ByVal pRazonSocial As String) As String
    Function ListadoProveedorCuenta(ByVal pCodProveedor As String) As String
    Function ListadoContacto(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pCodProveedor As String) As String

    Function ListadoContratoProveedor(ByVal pNumeroContrato As String) As String
    Function ValidarContratoProveedor(ByVal pEContratoProveedor As String) As String
End Interface


#End Region

