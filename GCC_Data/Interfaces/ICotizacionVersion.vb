Imports System.Runtime.InteropServices

#Region "Interface Transaccional"
''' <summary>
''' Interfaz de métodos para la clase ICotizacionVersionTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("D5DDF8AE-28C1-4f2a-8D39-BD5FF3919FE8")> _
Public Interface ICotizacionVersionTx


End Interface

#End Region

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase CotizacionVersionNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("F38A036A-9081-470c-98BA-1D8A9F754D2E")> _
Public Interface ICotizacionVersionNTx
    Function ListadoCotizacionVersion(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pECotizacion As String) As String
    Function ObtenerCotizacionVersion(ByVal pECotizacion As String) As String
End Interface

#End Region

