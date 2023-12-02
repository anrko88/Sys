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
<Guid("87556147-9F99-4fa9-A741-1D5C5EE24A3D") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LBloqueoTx")> _
Public Class LBloqueoTx
    Inherits ServicedComponent
    Implements IBloqueoTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LBloqueoTx"
#End Region

#Region "Metodos"

#Region "Bloqueo"

    ''' <summary>
    ''' Inserta bloqueo del sistema
    ''' </summary>
    ''' <param name="pEBloqueo">Entidad Bloqueo</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 18/07/2012
    ''' </remarks>
    ''' 
    <AutoComplete(True)> _
    Public Function InsertarBloqueo(ByVal pEBloqueo As String) As Boolean Implements IBloqueoTx.InsertarBloqueo
        Dim objDBloqueoTx As DBloqueoTx = Nothing
        Dim iResultado As Integer

        Try
            objDBloqueoTx = New DBloqueoTx
            iResultado = objDBloqueoTx.InsertarBloqueo(pEBloqueo)
        Catch ex As Exception
            Throw ex
        Finally
            objDBloqueoTx.Dispose()
            objDBloqueoTx = Nothing
        End Try

        Return iResultado
    End Function

    ''' <summary>
    ''' Modifica Bloqueo
    ''' </summary>
    ''' <param name="pEBloqueo">Listado de Objeto EBloqueo(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 04/06/2012
    ''' </remarks>
    ''' 
    <AutoComplete(True)> _
    Public Function ModificarBloqueo(ByVal pEBloqueo As String) As Boolean Implements IBloqueoTx.ModificarBloqueo
        Dim objDBloqueoTx As DBloqueoTx = Nothing
        Dim blnResultado As Boolean

        Try
            objDBloqueoTx = New DBloqueoTx
            blnResultado = objDBloqueoTx.ModificarBloqueo(pEBloqueo)
        Catch ex As Exception
            Throw ex
        Finally
            objDBloqueoTx.Dispose()
            objDBloqueoTx = Nothing
        End Try

        Return blnResultado
    End Function

#End Region

#End Region

End Class

#End Region

#Region "Clase No Transaccional"
''' <summary>
''' Implementación de la clase LBloqueoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/05/2012
''' </remarks>
<Guid("ECDFA98C-BF26-429c-9726-7460F50944E6") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LBloqueoNTx")> _
Public Class LBloqueoNTx
    Inherits ServicedComponent
    Implements IBloqueoNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LBloqueoNTx"
#End Region

#Region "Metodos"

#Region "Bloqueo"

    ''' <summary>
    ''' Obtiene datos del Bloqueo
    ''' </summary>
    ''' <param name="pEBloqueo">Codigo Bloqueo</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 05/06/2012 
    ''' </remarks>
    Public Function ObtenerBloqueo(ByVal pEBloqueo As String) As String Implements IBloqueoNTx.ObtenerBloqueo
        Try
            Using oDBloqueoNTx As New DBloqueoNTx
                Return oDBloqueoNTx.ObtenerBloqueo(pEBloqueo)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#End Region

End Class

#End Region
