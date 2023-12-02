Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IGestionBienDocTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("0AEB51BC-2CC0-494c-8E59-0008096A45A5")> _
Public Interface IGestionBienDocTx

    ''' <summary>
    ''' Inserta el cotizacioNDocumento para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEGestionBienDoc">Entidad Serializado de GestionBienDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    Function InsertarGestionBienDoc(ByVal pEGestionBienDoc As String) As Boolean

    ''' <summary>
    ''' Modificar el cotizacioNDocumento de una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEGestionBienDoc">Entidad Serializada de GestionBienDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    Function ModificarGestionBienDoc(ByVal pEGestionBienDoc As String) As Boolean

    ''' <summary>
    ''' Eliminar el cotizacioNDocumento de una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEGestionBienDoc">Entidad Serializada de GestionBienDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    Function EliminarGestionBienDoc(ByVal pEGestionBienDoc As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase GestionBienDocNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("7E5FE857-EFA5-467f-BDCE-DB83BE50B24A")> _
Public Interface IGestionBienDocNTx

    ''' <summary>
    ''' Obtiene el GestionBienDoc de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <param name="pEGestionBienDoc">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 21/10/2012 
    ''' </remarks>
    Function ObtenerGestionBienDoc(ByVal pEGestionBienDoc As String) As String

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/10/2012
    ''' </remarks>
    Function ListadoGestionBienDoc(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEGestionBienDoc As String _
                                                ) As String

End Interface

#End Region

