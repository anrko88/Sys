Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data


#Region "Clase No Transaccional"
''' <summary>
''' Implementación de la clase LTemporalNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("5339A21F-2A08-4be8-9777-34C9EE395C9B") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LUbigeoNTx")> _
Public Class LUbigeoNTx
    Inherits ServicedComponent
    Implements IUbigeoNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LUbigeoNTx"
#End Region

#Region "Metodos"
    Public Function LeerDepartamento() As String Implements IUbigeoNTx.ListadoDepartamento
        Dim objDUbigeoNTx As DUbigeoNTx = Nothing
        Dim strResultado As String

        Try
            objDUbigeoNTx = New DUbigeoNTx
            strResultado = objDUbigeoNTx.ListadoDepartamento()
        Catch ex As Exception
            Throw ex
        Finally
            objDUbigeoNTx.Dispose()
            objDUbigeoNTx = Nothing
        End Try

        Return strResultado
    End Function


    Public Function LeerProvincia(ByVal pstrDepartamento As String) As String Implements IUbigeoNTx.ListadoProvincia
        Dim objDUbigeoNTx As DUbigeoNTx = Nothing
        Dim strResultado As String

        Try
            objDUbigeoNTx = New DUbigeoNTx
            strResultado = objDUbigeoNTx.ListadoProvincia(pstrDepartamento)
        Catch ex As Exception
            Throw ex
        Finally
            objDUbigeoNTx.Dispose()
            objDUbigeoNTx = Nothing
        End Try

        Return strResultado
    End Function


    Public Function LeerDistrito(ByVal pstrDepartamento As String, ByVal pstrProvincia As String) As String Implements IUbigeoNTx.ListadoDistrito
        Dim objDUbigeoNTx As DUbigeoNTx = Nothing
        Dim strResultado As String

        Try
            objDUbigeoNTx = New DUbigeoNTx
            strResultado = objDUbigeoNTx.ListadoDistrito(pstrDepartamento, pstrProvincia)
        Catch ex As Exception
            Throw ex
        Finally
            objDUbigeoNTx.Dispose()
            objDUbigeoNTx = Nothing
        End Try

        Return strResultado
    End Function
    'IBK JJM
    Public Function LeerMunicipalidad() As String Implements IUbigeoNTx.ListadoMunicipalidad
        Dim objDUbigeoNTx As DUbigeoNTx = Nothing
        Dim strResultado As String

        Try
            objDUbigeoNTx = New DUbigeoNTx
            strResultado = objDUbigeoNTx.ListadoMunicipalidad()
        Catch ex As Exception
            Throw ex
        Finally
            objDUbigeoNTx.Dispose()
            objDUbigeoNTx = Nothing
        End Try

        Return strResultado
    End Function
    Public Function ListadoMunicipalidadPaginado(ByVal pPageSize As Integer, _
                                          ByVal pCurrentPage As Integer, _
                                          ByVal pSortColumn As String, _
                                          ByVal pSortOrder As String, _
                                          ByVal pCodMunicipalidad As String, _
                                          ByVal pMunicipalidad As String) As String Implements IUbigeoNTx.ListadoMunicipalidadPaginado
        Dim objDUbigeoNTx As DUbigeoNTx = Nothing
        Dim strResultado As String

        Try
            objDUbigeoNTx = New DUbigeoNTx
            strResultado = objDUbigeoNTx.ListadoMunicipalidadPaginado(pPageSize, pCurrentPage, pSortColumn, pSortOrder, pCodMunicipalidad, _
                                         pMunicipalidad)
        Catch ex As Exception
            Throw ex
        Finally
            objDUbigeoNTx.Dispose()
            objDUbigeoNTx = Nothing
        End Try

        Return strResultado
    End Function
#End Region

End Class
#End Region

