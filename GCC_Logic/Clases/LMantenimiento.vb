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
<Guid("77606561-9D97-4af6-829F-9C843966C1D1") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LUtilNTx")> _
Public Class LValorGenericaNTx
    Inherits ServicedComponent
    Implements IValorGenericaNTx


#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LValorGenericaNTx"
#End Region

#Region "Metodos"
    Public Function LeerValorGenerica(ByVal pstrTablaGenerica As String) As String Implements IValorGenericaNTx.ListadoValorGenerica
        Dim objDValorGenericaNTx As DValorGenericaNTx = Nothing
        Dim strResultado As String

        Try
            objDValorGenericaNTx = New DValorGenericaNTx
            strResultado = objDValorGenericaNTx.ListadoTablaGenerica(pstrTablaGenerica)
        Catch ex As Exception
            Throw ex
        Finally
            objDValorGenericaNTx.Dispose()
            objDValorGenericaNTx = Nothing
        End Try

        Return strResultado
    End Function

    Public Function LeerValorGenericaAnidada(ByVal pstrTablaGenerica As String, ByVal pstrCodigo As String) As String Implements IValorGenericaNTx.ListadoValorGenericaAnidada
        Dim objDValorGenericaNTx As DValorGenericaNTx = Nothing
        Dim strResultado As String

        Try
            objDValorGenericaNTx = New DValorGenericaNTx
            strResultado = objDValorGenericaNTx.ListadoValorGenericaAnidada(pstrTablaGenerica, pstrCodigo)
        Catch ex As Exception
            Throw ex
        Finally
            objDValorGenericaNTx.Dispose()
            objDValorGenericaNTx = Nothing

        End Try
        Return strResultado

    End Function


    '10/01/2013 IBK RPR
    Public Function ListadoValorGenericaEspecial(ByVal pEValorGenerica As String, ByVal type As Integer) As String Implements IValorGenericaNTx.ListadoValorGenericaEspecial
        Try
            Using oDValorGenericaNTx As New DValorGenericaNTx
                Return oDValorGenericaNTx.ListadoValorGenericaEspecial(pEValorGenerica, type)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'FIN RPR

#End Region

End Class
#End Region

