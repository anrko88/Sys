Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IContratoOtroConceptoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("8B008E81-882C-48b1-A90F-F93077B28B83")> _
Public Interface IContratoOtroConceptoTx

    Function fblnModificar(ByVal pEGCC_ContratoOtroConcepto As String) As Boolean
    Function ContratoOtroConceptoAdjuntoUpd(ByVal pEGCC_ContratoOtroConcepto As String) As Boolean
    'Inicio IBK - AAe
    Function fblnModificar2(ByVal pEGCC_ContratoOtroConcepto As String) As Boolean
    'Fin IBK

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ContratoOtroConceptoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("1609AD25-BC94-4aa8-B4F9-57DCD1F93B8C")> _
Public Interface IContratoOtroConceptoNTx


End Interface

#End Region
