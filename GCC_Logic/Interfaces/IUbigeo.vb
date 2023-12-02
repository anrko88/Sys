Imports System.Runtime.InteropServices

#Region "Interface No Transaccional"

''' <summary>
''' Interfaz de métodos para la clase ITemporalNTx
''' </summary>
''' <remarks>TSF-JRC | 16/04/2012</remarks>
<Guid("5457C9E0-82FA-48fd-B493-D1B9827D916F")> _
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