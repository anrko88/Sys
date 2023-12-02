Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DBloqueoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("7B3A07BD-D089-430a-B398-188E56D2D245") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DBloqueoTx")> _
Public Class DBloqueoTx
    Inherits ServicedComponent
    Implements IBloqueoTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DBloqueoTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta bloqueo del sistema
    ''' </summary>
    ''' <param name="pEBloqueo">Entidad Bloqueo</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 18/07/2012
    ''' </remarks>
    Public Function InsertarBloqueo(ByVal pEBloqueo As String) As Boolean Implements IBloqueoTx.InsertarBloqueo

        Dim oEBloqueo As New EGCC_Bloqueo
        Dim prmParameter(5) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEBloqueo = CFunciones.DeserializeObject(Of EGCC_Bloqueo)(pEBloqueo)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_TipoDocumento", DbType.String, 3, oEBloqueo.TipoDocumento, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Modulo", DbType.String, 3, oEBloqueo.Modulo, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_NumeroDocumento", DbType.String, 50, oEBloqueo.NumeroDocumento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodigoUsuario", DbType.String, 25, oEBloqueo.CodigoUsuario, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_NombreUsuario", DbType.String, 250, oEBloqueo.NombreUsuario, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pin_Activo", DbType.String, 1, oEBloqueo.Activo, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Bloqueo_Ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnInsertarBloqueo", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Modificar el Bloqueo del Sistema
    ''' </summary>
    ''' <param name="pEBloqueo">Entidad Bloqueo</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 18/07/2012
    ''' </remarks>
    Public Function ModificarBloqueo(ByVal pEBloqueo As String) As Boolean Implements IBloqueoTx.ModificarBloqueo

        Dim oEBloqueo As New EGCC_Bloqueo
        Dim prmParameter(6) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEBloqueo = CFunciones.DeserializeObject(Of EGCC_Bloqueo)(pEBloqueo)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@pin_CodigoBloqueo", DbType.Int16, oEBloqueo.CodigoBloqueo, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_TipoDocumento", DbType.String, 3, oEBloqueo.TipoDocumento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Modulo", DbType.String, 3, oEBloqueo.Modulo, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_NumeroDocumento", DbType.String, 50, oEBloqueo.NumeroDocumento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_CodigoUsuario", DbType.String, 25, oEBloqueo.CodigoUsuario, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_NombreUsuario", DbType.String, 250, oEBloqueo.NombreUsuario, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pin_Activo", DbType.String, 1, oEBloqueo.Activo, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Bloqueo_Upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarBloqueo", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DBloqueoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("1EB1403C-F8B1-4186-B187-0DC585A7EB2F") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DBloqueoNTx")> _
Public Class DBloqueoNTx
    Inherits ServicedComponent
    Implements IBloqueoNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DBloqueoNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene el Bloqueo del Sistema
    ''' </summary>
    ''' <param name="pEBloqueo">Entidad Bloqueo</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 18/07/2012 
    ''' </remarks>
    Public Function ObtenerBloqueo(ByVal pEBloqueo As String) As String Implements IBloqueoNTx.ObtenerBloqueo

        Dim oEBloqueo As New EGCC_Bloqueo
        Dim odtbBloqueo As DataTable
        Dim prmParameter(3) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEBloqueo = CFunciones.DeserializeObject(Of EGCC_Bloqueo)(pEBloqueo)

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_TipoDocumento", DbType.String, 3, oEBloqueo.TipoDocumento, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Modulo", DbType.String, 3, oEBloqueo.Modulo, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_NumeroDocumento", DbType.String, 50, oEBloqueo.NumeroDocumento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodigoUsuario", DbType.String, 25, oEBloqueo.CodigoUsuario, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Bloqueo_get"
            objRequest.Parameters.AddRange(prmParameter)

            odtbBloqueo = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerBloqueo", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If oEBloqueo Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of DataTable)(odtbBloqueo)
        End If
    End Function

#End Region

End Class

#End Region
