Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase IRepresentanteTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("59FE44B3-6E64-4b6b-9207-E17BE8110AA6")> _
Public Interface IRepresentanteTx

    ''' <summary>
    ''' Insertar Representantes del cliente
    ''' </summary>
    ''' <param name="pEGcc_Representante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function RepresentanteClienteIns(ByVal pEGcc_Representante As String) As Integer

    ''' <summary>
    ''' Insertar Representantes
    ''' </summary>
    ''' <param name="pEGcc_Representante">Entidad Representante serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function RepresentanteIns(ByVal pEGcc_Representante As String) As Integer

    ''' <summary>
    ''' Modificar Representantes
    ''' </summary>
    ''' <param name="pEGcc_Representante">Entidad Representante serializada</param>
    ''' <returns>Resultado Integer</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function RepresentanteUpd(ByVal pEGcc_Representante As String) As Boolean

    ''' <summary>
    ''' Modificar Representantes
    ''' </summary>
    ''' <param name="pEGcc_Representante">Entidad serializada</param>
    ''' <returns>Resultado Integer</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function RepresentanteDel(ByVal pEGcc_Representante As String) As Boolean

    ''' <summary>
    ''' Insertar Contrato Representantes
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentante">Entidad Representante serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function RepresentanteContratoIns(ByVal pEGcc_contratorepresentante As String) As Integer

    ''' <summary>
    ''' Delete Contrato Representantes
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentante">Entidad Representante serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function RepresentanteContratoDel(ByVal pEGcc_contratorepresentante As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase RepresentanteNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("D7875E8A-FF34-4643-94EC-40E55742A6A8")> _
Public Interface IRepresentanteNTx


    ''' <summary>
    ''' Listado de Representantes
    ''' </summary>
    ''' <param name="pEGcc_representante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function RepresentantesSel(ByVal pPageSize As Integer, _
                                                  ByVal pCurrentPage As Integer, _
                                                  ByVal pSortColumn As String, _
                                                  ByVal pSortOrder As String, _
                                                  ByVal pFirmaEn As String, _
                                                  ByVal pEGcc_representante As String, _
                                                  ByVal pNumeroContrato As String) As String
    ''' <summary>
    ''' Listado de Representantes por Contrato
    ''' </summary>
    ''' <param name="pEGcc_contratoRepresentante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function RepresentantesContratoSel(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEGcc_contratoRepresentante As String) As String

    ''' <summary>
    ''' RepresentantesItem
    ''' </summary>
    ''' <param name="pNroDocumento"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function RepresentantesItem(ByVal pNroDocumento As String) As String


    ''' <summary>
    ''' Listado de Representantes por Contrato
    ''' </summary>
    ''' <param name="pCodUnico">Código único del cliente, código de identificación.</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function RepresentantesCliente(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pCodUnico As String) As String

    ''' <summary>
    ''' Listado de Representantes por Contrato, excluyendo el contrato indicado en el parámetro
    ''' </summary>
    ''' <param name="pCodUnico">Código único del cliente, código de identificación.</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Function ListarDelCliente(ByVal pPageSize As Integer, _
                              ByVal pCurrentPage As Integer, _
                              ByVal pSortColumn As String, _
                              ByVal pSortOrder As String, _
                              ByVal pCodigoContrato As String, _
                              ByVal pCodUnico As String) As String

End Interface

#End Region
