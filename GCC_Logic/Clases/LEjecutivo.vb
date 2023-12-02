Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase LEjecutivoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 19/03/2013
''' </remarks>
<Guid("154FD943-4DA5-4899-B20F-696DFDEB580C") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LAlertasTx")> _
Public Class LEjecutivoTx
    Inherits ServicedComponent
    Implements IEjecutivoTx

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "LEjecutivoTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 19/03/2013
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function InsertarEjecutivo(ByVal pEEjecutivo As String) As Boolean Implements IEjecutivoTx.InsertarEjecutivo
        Dim oDEjecutivoTx As DEjecutivoTx = Nothing
        Dim blnResultado As Boolean
        Try
            oDEjecutivoTx = New DEjecutivoTx
            blnResultado = oDEjecutivoTx.InsertarEjecutivo(pEEjecutivo)
        Catch ex As Exception
            Throw ex
        Finally
            oDEjecutivoTx.Dispose()
            oDEjecutivoTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Modificar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 19/03/2013
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function ModificarEjecutivo(ByVal pEEjecutivo As String) As Boolean Implements IEjecutivoTx.ModificarEjecutivo
        Dim oDEjecutivoTx As DEjecutivoTx = Nothing
        Dim blnResultado As Boolean
        Try
            oDEjecutivoTx = New DEjecutivoTx
            blnResultado = oDEjecutivoTx.ModificarEjecutivo(pEEjecutivo)
        Catch ex As Exception
            Throw ex
        Finally
            oDEjecutivoTx.Dispose()
            oDEjecutivoTx = Nothing
        End Try
        Return blnResultado
    End Function

    ''' <summary>
    ''' Eliminar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 19/03/2013
    ''' </remarks>
    <AutoComplete(True)> _
    Public Function EliminarEjecutivo(ByVal pEEjecutivo As String) As Boolean Implements IEjecutivoTx.EliminarEjecutivo
        Dim oDEjecutivoTx As DEjecutivoTx = Nothing
        Dim blnResultado As Boolean
        Try
            oDEjecutivoTx = New DEjecutivoTx
            blnResultado = oDEjecutivoTx.EliminarEjecutivo(pEEjecutivo)
        Catch ex As Exception
            Throw ex
        Finally
            oDEjecutivoTx.Dispose()
            oDEjecutivoTx = Nothing
        End Try
        Return blnResultado
    End Function

#End Region

End Class

#End Region

#Region "Clase No Transaccional"

''' <summary>
''' Implementación de la clase LAlertasNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 29/01/2013
''' </remarks>
<Guid("33A987E1-D1D7-42f3-B1BD-92E3A34AD59E") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LAlertasNTx")> _
Public Class LEjecutivoNTx
    Inherits ServicedComponent
    Implements IEjecutivoNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LEjecutivoNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 19/03/2013
    ''' </remarks>
    Public Function ListadoEjecutivo(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pEEjecutivo As String) As String Implements IEjecutivoNTx.ListadoEjecutivo
        Dim objDEjecutivoNTx As DEjecutivoNTx = Nothing
        Dim strResultado As String

        Try
            objDEjecutivoNTx = New DEjecutivoNTx
            strResultado = objDEjecutivoNTx.ListadoEjecutivo(pPageSize, _
                                                           pCurrentPage, _
                                                           pSortColumn, _
                                                           pSortOrder, _
                                                           pEEjecutivo)
        Catch ex As Exception
            Throw ex
        Finally
            objDEjecutivoNTx.Dispose()
        End Try

        Return strResultado
    End Function

    ''' <summary>
    ''' Obtiene el Ejecutivo de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 19/03/2013
    ''' </remarks>
    Public Function ObtenerEjecutivo(ByVal pstrCodTabla As String, ByVal pstrCodigo As String) As String Implements IEjecutivoNTx.ObtenerEjecutivo
        Dim objDEjecutivoNTx As DEjecutivoNTx = Nothing
        Dim strResultado As String

        Try
            objDEjecutivoNTx = New DEjecutivoNTx
            strResultado = objDEjecutivoNTx.ObtenerEjecutivo(pstrCodTabla, pstrCodigo)
        Catch ex As Exception
            Throw ex
        Finally
            objDEjecutivoNTx.Dispose()
        End Try

        Return strResultado
    End Function

#End Region

End Class

#End Region

