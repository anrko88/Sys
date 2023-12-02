Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"
''' <summary>
''' Implementación de la clase DTipoCambioTx
''' </summary>
''' <remarks>
''' Creado Por         : JJM - IBK
''' Fecha de Creación  : 22/01/2013
''' </remarks>
<Guid("45a4fc2c-249a-43cf-816a-396f6b8d02bc") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LTipoCambioTx")> _
Public Class LTipoCambioTx
    Inherits ServicedComponent
    Implements ITipoCambioTx
#Region "Constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LTipoCambioTx"

#End Region
#Region "Métodos"
    <AutoComplete(True)> _
   Public Function InsertaTipoCambio(ByVal pETipoCambio As String) As Boolean Implements ITipoCambioTx.InsertaTipoCambio
        Try
            Using oDTipoCambioTx As New DTipoCambioTx
                Return oDTipoCambioTx.InsertaTipoCambio(pETipoCambio)
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    <AutoComplete(True)> _
   Public Function ActualizaTipoCambio(ByVal pETipoCambio As String) As Boolean Implements ITipoCambioTx.ActualizaTipoCambio
        Try
            Using oDTipoCambioTx As New DTipoCambioTx
                Return oDTipoCambioTx.ActualizaTipoCambio(pETipoCambio)
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    <AutoComplete(True)> _
   Public Function EliminaTipoCambio(ByVal pETipoCambio As String) As Boolean Implements ITipoCambioTx.EliminaTipoCambio
        Try
            Using oDTipoCambioTx As New DTipoCambioTx
                Return oDTipoCambioTx.EliminarTipoCambio(pETipoCambio)
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region
End Class
#End Region

#Region "Clase No Transaccional"
''' <summary>
''' Implementación de la clase DTipoCambioNTx
''' </summary>
''' <remarks>
''' Creado Por         : JJM - IBK
''' Fecha de Creación  : 22/01/2013
''' </remarks>
<Guid("5a56fdb9-06be-4e09-a4ba-809e2d6a2cf4") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LTipoCambioNTx")> _
Public Class LTipoCambioNTx
    Inherits ServicedComponent
    Implements ITipoCambioNTx

#Region "Constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LTipoCambioNTx"

#End Region
#Region "Métodos"
    Public Function ListaTipoCambio(ByVal pPageSize As Integer, _
                                  ByVal pCurrentPage As Integer, _
                                  ByVal pSortColumn As String, _
                                  ByVal pSortOrder As String, _
                                  ByVal pETipoCambio As String) As String Implements ITipoCambioNTx.ListaTipoCambio
        Dim objTipoCambioNTx As DTipoCambioNTx = Nothing
        Dim resultado As String

        Try
            objTipoCambioNTx = New DTipoCambioNTx
            resultado = objTipoCambioNTx.ListaTipoCambio(pPageSize, _
                                         pCurrentPage, _
                                         pSortColumn, _
                                         pSortOrder, _
                                         pETipoCambio)
        Catch ex As Exception
            Throw ex
        Finally
            objTipoCambioNTx.Dispose()
        End Try

        Return resultado

    End Function
    Public Function ValidaTipoCambio(ByVal pETipoCambio As String) As String Implements ITipoCambioNTx.ValidaTipoCambio
        Dim objTipoCambioNTx As DTipoCambioNTx = Nothing
        Dim resultado As String

        Try
            objTipoCambioNTx = New DTipoCambioNTx
            resultado = objTipoCambioNTx.ValidaTipoCambio(pETipoCambio)
        Catch ex As Exception
            Throw ex
        Finally
            objTipoCambioNTx.Dispose()
        End Try

        Return resultado

    End Function
#End Region
End Class
#End Region



