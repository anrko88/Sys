Imports System.Runtime.InteropServices

#Region "Interface Transaccional"
''' <summary>
''' Interfaz de métodos para la clase ICotizacionCronogramaTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("48E35D82-9D6C-428b-9734-6B995F657B35")> _
Public Interface ICotizacionCronogramaTx

    ''' <summary>
    ''' Inserta el cronograma
    ''' </summary>
    ''' <param name="pECotizacioncronograma">Entidad Serializado de Contacto formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012
    ''' </remarks>
    Function InsertarCronograma(ByVal pECotizacioncronograma As String) As Boolean

    ''' <summary>
    ''' Modificar el cronograma
    ''' </summary>
    ''' <param name="pECotizacioncronograma">Entidad Serializado de Contacto formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 25/05/2012
    ''' </remarks>
    Function ModificarCronograma(ByVal pECotizacioncronograma As String) As Boolean

    ''' <summary>
    ''' Eliminar el cronograma
    ''' </summary>
    ''' <param name="pECotizacioncronograma">Entidad Serializado de Contacto formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 25/05/2012
    ''' </remarks>
    Function EliminarCronograma(ByVal pECotizacioncronograma As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase CotizacionCronogramaNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("F623AD82-F497-4880-AECF-BA2E4A1D2F04")> _
Public Interface ICotizacionCronogramaNTx
    Function ObtenerCotizacionCronograma(ByVal pECotizacion As String) As String
    Function ObtenerCronogramaActual(ByVal pstrNumeroCotizacion As String) As String
    Function CotizacionCronogramaGet(ByVal pECotizacion As String) As String
End Interface

#End Region