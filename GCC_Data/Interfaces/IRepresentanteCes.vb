Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IRepresentanteCesTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/01/2013
''' </remarks>
<Guid("D21A1E29-3656-4f66-9FF9-D589B9485DC0")> _
Public Interface IRepresentanteCesTx

    Function InsertarRepresentanteCes(ByVal pERepresentanteCes As String) As Boolean
    Function ModificarRepresentanteCes(ByVal pERepresentanteCes As String) As Boolean
    Function EliminarRepresentanteCes(ByVal pERepresentanteCes As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase RepresentanteCesNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/01/2013
''' </remarks>
<Guid("189C6F4F-3AC5-4901-A92D-986814AC2203")> _
Public Interface IRepresentanteCesNTx

    Function ObtenerRepresentanteCes(ByVal pERepresentanteCes As String) As String
    Function ListadoRepresentanteCes(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pERepresentanteCes As String _
                                                ) As String

End Interface

#End Region

