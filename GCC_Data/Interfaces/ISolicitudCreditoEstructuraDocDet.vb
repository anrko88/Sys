Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ISolicitudCreditoEstructuraDocDetTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/06/2012
''' </remarks>
<Guid("51868120-21FA-4ac1-8BA6-7E6ED78B5C71")> _
Public Interface ISolicitudCreditoEstructuraDocDetTx
    Function InsertarContratoEstructDocDet(ByVal pEContratoEstructDocDet As String) As Boolean
    Function EliminarContratoEstructDocDet(ByVal pEContratoEstructDocDet As String) As Boolean
    'inicio IBK
    Function AgregarRelacion(ByVal pstrNroContrato As String, _
                             ByVal pstrSecBien As String, _
                             ByVal pstrArrayDocs As String, _
                             ByVal nbrArraySize As Integer) As Boolean
    ' Fin IBK
End Interface

#End Region


#Region "Interface NO Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ISolicitudCreditoEstructuraDocDetTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/06/2012
''' </remarks>
<Guid("4F2BB5F4-50DB-4f59-A802-1C165105A07F")> _
Public Interface ISolicitudCreditoEstructuraDocDetNTx

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 17/05/2012
    ''' </remarks>
    Function ListadoContratoEstructDocDet(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEContratoEstructDoc As String) As String

    ' Inicio IBK
    Function ListaRelacionesContrato(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pNroContrato As String) As String
    ' Fin IBK
End Interface

#End Region