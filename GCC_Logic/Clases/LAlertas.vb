Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LBloqueoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/05/2012
''' </remarks>
<Guid("2DC15208-FF74-4e1e-B3D9-E2D2E7DC34C2") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LAlertasTx")> _
Public Class LAlertasTx
    Inherits ServicedComponent
    Implements IAlertasTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LAlertasTx"
#End Region

#Region "Metodos"

#Region "Alertas"

    ''' <summary>
    ''' Inserta bloqueo del sistema
    ''' </summary>
    ''' <param name="pEBloqueo">Entidad Alertas</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - SCA
    ''' Fecha de Creacion : 29/01/2013
    ''' </remarks>
    ''' 
    <AutoComplete(True)> _
    Public Function fInsertarAlertas(ByVal pEAlertas As String) As Boolean Implements IAlertasTx.fInsertarAlertas
        Dim objDAlertasTx As DAlertasTx = Nothing
        Dim iResultado As Integer

        Try
            objDAlertasTx = New DAlertasTx
            iResultado = objDAlertasTx.fInsertarAlertas(pEAlertas)
        Catch ex As Exception
            Throw ex
        Finally
            objDAlertasTx.Dispose()
            objDAlertasTx = Nothing
        End Try

        Return iResultado
    End Function

#End Region

#End Region

End Class

#End Region

#Region "Clase No Transaccional"
''' <summary>
''' Implementación de la clase LAlertasNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - SCA
''' Fecha de Creación  : 29/01/2013
''' </remarks>
<Guid("CEE48E38-6180-471a-8DC1-4F8ADCBF7B58") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LAlertasNTx")> _
Public Class LAlertasNTx
    Inherits ServicedComponent
    Implements IAlertasNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LAlertasNTx"
#End Region

#Region "Metodos"

#Region "Alertas"


#End Region

#End Region

End Class

#End Region

