Imports System.Runtime.InteropServices

#Region "Interface Transaccional"
''' <summary>
''' Interfaz de métodos para la clase ICotizacionSeguimientoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("28F05337-90B9-4edd-9E38-3AC865C7FD10")> _
Public Interface ICotizacionSeguimientoTx


End Interface

#End Region

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase CotizacionSeguimientoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("D0A2B25C-9A92-473b-AA38-7E3764C965ED")> _
Public Interface ICotizacionSeguimientoNTx
    Function ListadoSeguimientoCotizacion(ByVal pPageSize As Integer, _
                                                     ByVal pCurrentPage As Integer, _
                                                     ByVal pSortColumn As String, _
                                                     ByVal pSortOrder As String, _
                                                     ByVal pECotizacion As String) As String
End Interface

#End Region