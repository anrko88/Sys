Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IContratoProveedorTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("834681EB-DBC3-4449-9DEE-CDB1BB22BB4C")> _
Public Interface ISolicitudCreditoTx

    Function ModificaSolicitudDocumentoProveedor(ByVal pESolicitudCredito As String) As Boolean
    Function ModificaActivacionContrato(ByVal pESolicitudCredito As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase ISolicitudCreditoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - IJM
''' Fecha de Creación  : 16/05/2012
''' </remarks>
<Guid("574DD57C-7C28-4404-8A31-DD4799B62124")> _
Public Interface ISolicitudCreditoNTx

    ''' <summary>
    ''' Lista Cotizacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function ListadoCheckListComercial(ByVal pESolicitudCredito As String) As String

    ' Inicio IBK
    Function ListaBienesContrato(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pNroContrato As String) As String
    ' Fin IBK
End Interface

#End Region