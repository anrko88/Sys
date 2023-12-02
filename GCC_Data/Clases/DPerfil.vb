Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DPerfilNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - KCC
''' Fecha de Creación  : 23/05/2012
''' </remarks>
<Guid("467E0E72-BA60-4457-B079-9015FF701649") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DPerfilNTx")> _
Public Class DPerfilNTx
    Inherits ServicedComponent
    Implements IPerfilNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DPerfilNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Lista todos los perfiles activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 25/05/2012
    ''' </remarks>
    Public Function ListarPerfil() As String Implements IPerfilNTx.ListarPerfil
        Dim odtbListado As DataTable

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Perfil_Sel"
            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarPerfil", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function


    ''' <summary>
    ''' Obtiene el Peril interno del aplicativo por un codigo envia de sda
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 25/05/2012
    ''' </remarks>
    Public Function ObtenerPerfilxSda(ByVal intCodSda As Integer) As String Implements IPerfilNTx.ObtenerPerfilxSda
        Dim odtbPerfil As DataTable

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim prmParameter(0) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@pii_CodSda", DbType.Int32, 0, intCodSda, ParameterDirection.Input)

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_PerfilSDA_Get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbPerfil = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerPerfilxSda", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbPerfil)
    End Function
#End Region

End Class

#End Region
