Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ISiniestroTx
''' </summary>
''' <remarks>TSF-JRC | 12/11/2012</remarks>
<Guid("4BA02E5E-5208-4346-9496-C75642A65CFE")> _
Public Interface ISiniestroTx

    Function InsertarSiniestro(ByVal pESiniestro As String) As Boolean
    Function ModificarSiniestro(ByVal pESiniestro As String) As Boolean
    Function EliminarSiniestro(ByVal pESiniestro As String) As Boolean

End Interface

#End Region


#Region "Interface NO Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ISiniestroNTx
''' </summary>
''' <remarks>TSF-JRC | 12/11/2012</remarks>
<Guid("2BA2FED7-5FBD-499D-A5D3-8BDE885E2FFC")> _
Public Interface ISiniestroNTx

    Function ListadoSiniestroContrato(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pESiniestro As String) As String
    Function GetSiniestroContrato(ByVal pESiniestro As String) As String
    Function ListadoSiniestro(ByVal pPageSize As Integer, _
                                ByVal pCurrentPage As Integer, _
                                ByVal pSortColumn As String, _
                                ByVal pSortOrder As String, _
                                ByVal pESiniestro As String) As String
    Function GetSiniestro(ByVal pESiniestro As String) As String

    ''' <summary>
    ''' Listado de Siniestro para exportar
    ''' </summary>
    ''' <param name="pESiniestro">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 21/01/2013
    ''' </remarks>
    Function ListadoReporteSiniestro(ByVal pESiniestro As String) As String

    ''' <summary>
    ''' Obtiene el Siniestro
    ''' </summary>
    ''' <param name="pESiniestro">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 18/01/2013
    ''' </remarks>
    Function GetSiniestroConsulta(ByVal pESiniestro As String) As String
End Interface

#End Region
