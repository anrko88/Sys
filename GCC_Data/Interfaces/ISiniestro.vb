Imports System.Runtime.InteropServices

#Region "Interface Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ISiniestroTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 12/11/2012
''' </remarks>
<Guid("D8AE8159-6A2F-4ed5-81B0-B3FA30C76BAD")> _
Public Interface ISiniestroTx

    Function InsertarSiniestro(ByVal pESiniestro As String) As Integer
    Function ModificarSiniestro(ByVal pESiniestro As String) As Boolean
    Function EliminarSiniestro(ByVal pESiniestro As String) As Boolean

End Interface

#End Region

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ISiniestroNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 12/11/2012
''' </remarks>
<Guid("3EB905C1-682C-4139-8FA4-282F94715B84")> _
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
