Imports System.Runtime.InteropServices

#Region "Interface Transaccional"
''' <summary>
''' Interfaz de métodos para la clase IContratoProveedorTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("22F693CA-577B-4d8b-B57F-776FCACCD2F0")> _
Public Interface IContratoProveedorTx

    Function InsertarContratoProveedor(ByVal pEContratoProveedor As String) As Integer
    Function ModificaContratoProveedor(ByVal pEContratoProveedor As String) As Integer
    Function EliminarContratoProveedor(ByVal pEContratoProveedor As String) As Boolean
    Function EnviarCartaDocumentoProveedor(ByVal pEGcc_contratoProveedor As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ContratoProveedorNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("1D2BB732-0C3F-46f9-8D67-B1452C55D11D")> _
Public Interface IContratoProveedorNTx

    Function ListadoContratoProveedor(ByVal pNumeroContrato As String) As String

    ''' <summary>
    ''' Devuelve los proveedores del contrato (sin repetir), organizado por paginas de resultados.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function ListarDistintos(ByVal pPageSize As Integer, _
                             ByVal pCurrentPage As Integer, _
                             ByVal pSortColumn As String, _
                             ByVal pSortOrder As String, _
                             ByVal pContrato As String) As String

    Function Listado(ByVal pPageSize As Integer, _
                     ByVal pCurrentPage As Integer, _
                     ByVal pSortColumn As String, _
                     ByVal pSortOrder As String, _
                     ByVal pContrato As String) As String

    Function ValidarContratoProveedor(ByVal pEContratoProveedor As String) As String

End Interface

#End Region
