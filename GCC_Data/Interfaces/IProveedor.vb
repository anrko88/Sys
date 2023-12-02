
Imports System.Runtime.InteropServices

#Region "Interface Transaccional"
''' <summary>
''' Interfaz de métodos para la clase IProveedorTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("4B9E67AA-F3F4-46f7-8436-740B7617F517")> _
Public Interface IProveedorTx

    Function InsertarProveedor(ByVal pEProveedor As String) As Boolean
    Function ModificarProveedor(ByVal pEProveedor As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase ProveedorNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("E98EC3CF-52DB-4977-BE0E-6692627E8560")> _
Public Interface IProveedorNTx
    Function ObtenerProveedor(ByVal pstrCodProveedor As String) As String
    Function ListadoProveedor(ByVal pPageSize As Integer, _
                                 ByVal pCurrentPage As Integer, _
                                 ByVal pSortColumn As String, _
                                 ByVal pSortOrder As String, _
                                 ByVal pCodigoTipoDocumento As String, _
                                 ByVal pNumeroDocumento As String, _
                                 ByVal pRazonSocial As String) As String
    Function ListadoProveedorCuenta(ByVal pCodProveedor As String) As String
End Interface

#End Region

