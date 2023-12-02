Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ICesionarioTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/01/2013
''' </remarks>
<Guid("C473A460-62EC-47c4-848A-2BDCBC587525")> _
Public Interface ICesionarioTx

    Function InsertarCesionario(ByVal pECesionario As String) As String
    Function ModificarCesionario(ByVal pECesionario As String) As Boolean
    Function EliminarCesionario(ByVal pECesionario As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase CesionarioNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/01/2013
''' </remarks>
<Guid("5186142E-1F30-4302-A643-051FE2850EC2")> _
Public Interface ICesionarioNTx

    Function ObtenerCesionario(ByVal pECesionario As String) As String
    Function ListadoCesionario(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pECesionario As String _
                                                ) As String

End Interface

#End Region

