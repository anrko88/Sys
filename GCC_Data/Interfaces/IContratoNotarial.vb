Imports System.Runtime.InteropServices


#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ICartaTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("08E6B5C9-C3D3-4c8c-8F70-7427B2EE3C53")> _
Public Interface IContratoNotarialTx


    Function InsertarContratoNotarial(ByVal pEGCC_ContratoNotarial As String) As Integer

    Function ModificarContratoNotarial(ByVal pEGCC_ContratoNotarial As String) As Boolean

    Function ModificarAdenda(ByVal pEGCC_ContratoNotarial As String) As Boolean

    Function EliminarContratoNotarial(ByVal pEGcc_contratonotarial As String) As Boolean

    ''' <summary>
    ''' Actualiza el nombre del archivo adjunto en la tabla GCC_ContratoNotarial.
    ''' </summary>
    ''' <param name="pEGCC_ContratoNotarial">Entidad EGCC_ContratoNotarial serializada</param>
    ''' <returns>True si se grabó correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function ActualizarNombreArchivo(ByVal pEGCC_ContratoNotarial As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase CartaNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("93935D96-9672-40f3-A08C-58AC6397CCCF")> _
Public Interface IContratoNotarialNTx


    Function ListadoContratoNotarial(ByVal pNumeroContrato As String, _
                                     ByVal pCodigoOrigenAdenda As String) As String

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pNumeroContrato"></param>
    ''' <param name="pCodigoOrigenAdenda">[Notarial/Adenda]</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Function ListadoContratoNotarialPaginado(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pNumeroContrato As String, _
                                             ByVal pCodigoOrigenAdenda As String) As String

End Interface


#End Region
