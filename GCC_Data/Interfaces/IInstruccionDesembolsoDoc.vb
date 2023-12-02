Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IInstruccionDesembolsoDocTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("9B3B6B3C-12E7-4eb0-9234-5C0AC7FB34F7")> _
Public Interface IInstruccionDesembolsoDocTx

    ''' <summary>
    ''' Inserta el cotizacioNDocumento para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEInstruccionDesembolsoDoc">Entidad Serializado de InstruccionDesembolsoDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    Function InsertarInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As Boolean

    ''' <summary>
    ''' Modificar el cotizacioNDocumento de una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEInstruccionDesembolsoDoc">Entidad Serializada de InstruccionDesembolsoDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    Function ModificarInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As Boolean

    ''' <summary>
    ''' Eliminar el cotizacioNDocumento de una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEInstruccionDesembolsoDoc">Entidad Serializada de InstruccionDesembolsoDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    Function EliminarInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase InstruccionDesembolsoDocNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("8545DD3D-4784-4051-9E7B-B4D5D7E51356")> _
Public Interface IInstruccionDesembolsoDocNTx

    ''' <summary>
    ''' Obtiene el InstruccionDesembolsoDoc de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <param name="pEInstruccionDesembolsoDoc">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 21/10/2012 
    ''' </remarks>
    Function ObtenerInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As String

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/10/2012
    ''' </remarks>
    Function ListadoInstruccionDesembolsoDoc(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEInstruccionDesembolsoDoc As String _
                                                ) As String

End Interface

#End Region

