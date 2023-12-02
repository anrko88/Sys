Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ICesionarioTx
''' </summary>
''' <remarks>TSF-JRC | 08/01/2013</remarks>
<Guid("49F272C5-FFB1-4c47-8C6C-D0579130C8B0")> _
Public Interface ICesionarioTx

#Region "Mnt Cesionario"
    Function InsertarCesionario(ByVal pECesionario As String) As String
    Function ModificarCesionario(ByVal pECesionario As String) As Boolean
    Function EliminarCesionario(ByVal pECesionario As String) As Boolean
#End Region

#Region "Mnt Representante"
    Function InsertarRepresentante(ByVal pERepresentante As String) As String
    Function ModificarRepresentante(ByVal pERepresentante As String) As Boolean
    Function EliminarRepresentante(ByVal pERepresentante As String) As Boolean
#End Region

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ICesionarioNTx
''' </summary>
''' <remarks>TSF-JRC | 08/01/2013</remarks>
<Guid("39E14092-0780-44f1-A65F-9C4249C29439")> _
Public Interface ICesionarioNTx

#Region "Mnt Cesionario"
    Function ObtenerCesionario(ByVal pECesionario As String) As String
    Function ListadoCesionario(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pECesionario As String _
                                                ) As String
#End Region

#Region "Mnt Representante"
    Function ObtenerRepresentante(ByVal pERepresentante As String) As String
    Function ListadoRepresentante(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pERepresentante As String _
                                    ) As String
#End Region

End Interface

#End Region


