
Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

Imports System.Text
Imports System.Configuration

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LHostTx
''' </summary>
''' <remarks>
''' Creado Por         : IBK - RPR
''' Fecha de Creación  : 09/01/2013
''' </remarks>
<Guid("F869C054-6C60-4e44-B8C8-4B3D35A01564") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LHostTx")> _
Public Class LHostTx
    Inherits ServicedComponent
    Implements IHostTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LHostTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Consumir funciones de NPCli.dll para llamadas a Host
    ''' </summary>
    ''' <remarks>
    ''' Creado Por         : IBK - RPR
    ''' Fecha de Creación  : 07/01/2013
    ''' </remarks>
    <AutoComplete(True)> _
     Public Function TransaccionGINA(ByVal strTramaIn As String, ByRef strTramaOut As String) As Boolean Implements IHostTx.TransaccionGINA

        Try
            Using oDHostTx As New DHostTx
                Return oDHostTx.TransaccionGINA(strTramaIn, strTramaOut)
            End Using
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

End Class

#End Region

#Region "Clase No Transaccional"


<Guid("C251AFD1-FFFB-43db-858A-DBDAA21FBD8D") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LHostNTx")> _
Public Class LHostNTx
    Inherits ServicedComponent
    Implements IHostNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LHostNTx"
#End Region

#Region "Metodos"


#End Region

End Class

#End Region

