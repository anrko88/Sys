Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DDocumentoLeasingTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("1A215EED-6420-4431-99E8-F7AA13499943") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DDocumentoLeasingTx")> _
Public Class DDocumentoLeasingTx
    Inherits ServicedComponent
    Implements IDocumentoLeasingTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DDocumentoLeasingTx"
#End Region

#Region "Metodos"


#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DDocumentoLeasingNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("313C8EEF-2655-47a4-8F9D-CC90E09EB37C") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DDocumentoLeasingNTx")> _
Public Class DDocumentoLeasingNTx
    Inherits ServicedComponent
    Implements IDocumentoLeasingNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DDocumentoLeasingNTx"
#End Region

#Region "Metodos"

#End Region

End Class

#End Region
