Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DCesionarioTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/01/2013
''' </remarks>
<Guid("B766DD91-6EBC-4dd7-BA23-69C6AF9F6F65") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCesionarioTx")> _
Public Class DCesionarioTx
    Inherits ServicedComponent
    Implements ICesionarioTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DCesionarioTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta el Cesionario
    ''' </summary>
    ''' <param name="pECesionario">Entidad Serializado formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 08/01/2013
    ''' </remarks>
    Public Function InsertarCesionario(ByVal pECesionario As String) As String Implements ICesionarioTx.InsertarCesionario

        Dim parCodigoCesionario As IDataParameter
        Dim oECesionario As New EGCC_Cesionario
        Dim prmParameter(15) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oECesionario = CFunciones.DeserializeObject(Of EGCC_Cesionario)(pECesionario)

        'Campos 
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oECesionario.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodCesionario", DbType.Int16, 0, oECesionario.CodCesionario, ParameterDirection.InputOutput)
        prmParameter(2) = New DAABRequest.Parameter("@piv_RazonSocial", DbType.String, 250, oECesionario.RazonSocial, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodigoTipoDocumento", DbType.String, 100, oECesionario.TipoDocumento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 11, oECesionario.NroDocumento, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Direccion", DbType.String, 250, oECesionario.Direccion, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_CodDepartamento", DbType.String, 10, oECesionario.Departamento, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_CodProvincia", DbType.String, 10, oECesionario.Provincia, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_CodDistrito", DbType.String, 10, oECesionario.Distrito, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_AudUsuarioRegistro", DbType.String, 12, oECesionario.UsuarioRegistro, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_AudUsuarioModificacion", DbType.String, 12, oECesionario.UsuarioModificacion, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_AudEstadoLogico", DbType.Int16, 0, oECesionario.EstadoLogico, ParameterDirection.Input)

        prmParameter(12) = New DAABRequest.Parameter("@piv_CodigoTipoCuenta", DbType.String, 100, oECesionario.TipoCuenta, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_CodigoMoneda", DbType.String, 18, oECesionario.CodMoneda, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@piv_Cuenta", DbType.String, 25, oECesionario.NroCuenta, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 10, oECesionario.CodUnico, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Cesionario_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnInsertarCesionario", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parCodigoCesionario = CType(obRequest.Parameters(1), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckStr(parCodigoCesionario.Value.ToString())

    End Function

    ''' <summary>
    ''' Modificar el Cesionario
    ''' </summary>
    ''' <param name="pECesionario">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 08/01/2013
    ''' </remarks>
    Public Function ModificarCesionario(ByVal pECesionario As String) As Boolean Implements ICesionarioTx.ModificarCesionario

        Dim oECesionario As New EGCC_Cesionario
        Dim prmParameter(15) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oECesionario = CFunciones.DeserializeObject(Of EGCC_Cesionario)(pECesionario)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oECesionario.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodCesionario", DbType.Int16, 0, oECesionario.CodCesionario, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_RazonSocial", DbType.String, 250, oECesionario.RazonSocial, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodigoTipoDocumento", DbType.String, 100, oECesionario.TipoDocumento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 11, oECesionario.NroDocumento, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Direccion", DbType.String, 250, oECesionario.Direccion, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_CodDepartamento", DbType.String, 10, oECesionario.Departamento, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_CodProvincia", DbType.String, 10, oECesionario.Provincia, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_CodDistrito", DbType.String, 10, oECesionario.Distrito, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_AudUsuarioRegistro", DbType.String, 12, oECesionario.UsuarioRegistro, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_AudUsuarioModificacion", DbType.String, 12, oECesionario.UsuarioModificacion, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_AudEstadoLogico", DbType.Int16, 0, oECesionario.EstadoLogico, ParameterDirection.Input)

        prmParameter(12) = New DAABRequest.Parameter("@piv_CodigoTipoCuenta", DbType.String, 100, oECesionario.TipoCuenta, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_CodigoMoneda", DbType.String, 18, oECesionario.CodMoneda, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@piv_Cuenta", DbType.String, 25, oECesionario.NroCuenta, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 10, oECesionario.CodUnico, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Cesionario_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarCesionario", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Eliminar el Cesionario
    ''' </summary>
    ''' <param name="pECesionario">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 08/01/2013
    ''' </remarks>
    Public Function EliminarCesionario(ByVal pECesionario As String) As Boolean Implements ICesionarioTx.EliminarCesionario

        Dim oECesionario As New EGCC_Cesionario
        Dim prmParameter(3) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oECesionario = CFunciones.DeserializeObject(Of EGCC_Cesionario)(pECesionario)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oECesionario.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodCesionario", DbType.Int16, 0, oECesionario.CodCesionario, ParameterDirection.Input)

        'Auditoria
        prmParameter(2) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oECesionario.EstadoLogico, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oECesionario.UsuarioModificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Cesionario_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarCesionario", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
''' Implementación de la clase DCesionarioNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/01/2013
''' </remarks>
<Guid("9E2217AC-765D-4c92-AEC3-A70AAB8E2FB9") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCesionarioNTx")> _
Public Class DCesionarioNTx
    Inherits ServicedComponent
    Implements ICesionarioNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DCesionarioNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene el Cesionario
    ''' </summary>
    ''' <param name="pECesionario">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 08/01/2013 
    ''' </remarks>
    Public Function ObtenerCesionario(ByVal pECesionario As String) As String Implements ICesionarioNTx.ObtenerCesionario

        Dim odtbListadoCotizacionDoc As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        Dim oECesionario As New EGCC_Cesionario
        oECesionario = CFunciones.DeserializeObject(Of EGCC_Cesionario)(pECesionario)

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oECesionario.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodCesionario", DbType.Int16, 0, oECesionario.CodCesionario, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Cesionario_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCotizacionDoc = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerCesionario", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If odtbListadoCotizacionDoc Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoCotizacionDoc)
        End If
    End Function

    ''' <summary>
    ''' Lista todos los Cesionarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 08/01/2013
    ''' </remarks>
    Public Function ListadoCesionario(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pECesionario As String _
                                                ) As String Implements ICesionarioNTx.ListadoCesionario

        Dim odtbListadoCesionario As DataTable
        Dim prmParameter(4) As DAABRequest.Parameter

        Dim oECesionario As New EGCC_Cesionario
        oECesionario = CFunciones.DeserializeObject(Of EGCC_Cesionario)(pECesionario)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oECesionario.Codsolcredito, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Cesionario_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCesionario = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoCesionario", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoCesionario)
    End Function

#End Region

End Class

#End Region

