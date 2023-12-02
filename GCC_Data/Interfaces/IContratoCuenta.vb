Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IContratoCuentaTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("E73C7BA7-74B4-4c94-A1C0-05471ACCD51E")> _
Public Interface IContratoCuentaTx
    ''' <summary>
    ''' Modificar Contrato Cuenta
    ''' </summary>
    ''' <param name="pEGcc_ContratoCuenta">Entidad serializada</param>
    ''' <returns>Resultado Integer</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function ContratoCuentaIns(ByVal pEGcc_ContratoCuenta As String) As Boolean
    Function ContratoCuentaDel(ByVal pstrESolicitudcredito As String) As Boolean


End Interface

#End Region

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase ContratoCuentaNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("895DDD75-6DE1-4bc2-AE24-313B8CB8257F")> _
Public Interface IContratoCuentaNTx

    ''' <summary>
    ''' Listado de Representantes
    ''' </summary>
    ''' <param name="pEGcc_ContratoCuenta">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function ContratoCuentaSel(ByVal pEGcc_ContratoCuenta As String) As String

    Function ObtenerCtaLeasingWioSel(ByVal pstrNroContrato As String) As String
End Interface

#End Region

