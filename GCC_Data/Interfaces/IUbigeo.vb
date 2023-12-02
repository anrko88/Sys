

Imports System.Runtime.InteropServices

#Region "Interface No Transaccional"
''' <summary>
''' Interfaz de métodos para la clase TemporalNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
''' 
<Guid("E39E657F-D676-4f50-9C97-423A93CF5BEF")> _
Public Interface IUbigeoNTx

    Function ListadoDepartamento() As String
    Function ListadoProvincia(ByVal pstrDepartamento As String) As String
    Function ListadoDistrito(ByVal pstrDepartamento As String, ByVal pstrProvincia As String) As String
    Function ListadoMunicipalidad() As String
    Function ListadoMunicipalidadPaginado(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pCodMunicipalidad As String, _
                                            ByVal pMunicipalidad As String) As String 'JJM IBK
End Interface

#End Region