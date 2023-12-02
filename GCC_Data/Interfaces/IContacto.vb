Imports System.Runtime.InteropServices

#Region "Interface Transaccional"
''' <summary>
''' Interfaz de métodos para la clase IContactoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("214574A1-6F31-418d-84AE-33EE5F31459F")> _
Public Interface IContactoTx
    Function InsertarContacto(ByVal pEContacto As String) As Boolean
    Function ModificarContacto(ByVal pEContacto As String) As Boolean
End Interface

#End Region

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase ContactoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("5D15DB88-4E50-40fa-A8AE-61747009E2D4")> _
Public Interface IContactoNTx
    Function ObtenerContacto(ByVal pstrCodCotizacion As String, ByVal pstrCodContrato As String) As String
    Function ListadoContacto(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pCodProveedor As String) As String
End Interface

#End Region