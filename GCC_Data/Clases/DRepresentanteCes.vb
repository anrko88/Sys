Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DRepresentanteCesTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/01/2013
''' </remarks>
<Guid("577C444A-CE71-460c-9213-DC3A840343FD") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DRepresentanteCesTx")> _
Public Class DRepresentanteCesTx
    Inherits ServicedComponent
    Implements IRepresentanteCesTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DRepresentanteCesTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta el Representante
    ''' </summary>
    ''' <param name="pERepresentanteCes">Entidad Serializado formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 08/01/2013
    ''' </remarks>
    Public Function InsertarRepresentanteCes(ByVal pERepresentanteCes As String) As Boolean Implements IRepresentanteCesTx.InsertarRepresentanteCes

        Dim oERepresentanteCes As New EGCC_RepresentanteCes
        Dim prmParameter(10) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oERepresentanteCes = CFunciones.DeserializeObject(Of EGCC_RepresentanteCes)(pERepresentanteCes)

        'Campos 
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oERepresentanteCes.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodCesionario", DbType.Int16, 0, oERepresentanteCes.CodCesionario, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodRepresentante", DbType.Int16, 0, oERepresentanteCes.Codigorepresentante, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_NomRepresentante", DbType.String, 250, oERepresentanteCes.Nombrerepresentante, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_CodigoTipoDocumento", DbType.String, 100, oERepresentanteCes.CodigoTipoDocumento, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 11, oERepresentanteCes.Nrodocumento, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_PartidaRegistral", DbType.String, 50, oERepresentanteCes.Partidaregistral, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_OficinaRegistral", DbType.String, 50, oERepresentanteCes.Oficinaregistral, ParameterDirection.Input)

        prmParameter(8) = New DAABRequest.Parameter("@piv_AudUsuarioRegistro", DbType.String, 12, oERepresentanteCes.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_AudUsuarioModificacion", DbType.String, 12, oERepresentanteCes.Audusuariomodificacion, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_AudEstadoLogico", DbType.Int16, 0, oERepresentanteCes.EstadoLogico, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_RepresentanteCes_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnInsertarRepresentanteCes", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Modificar el Representante
    ''' </summary>
    ''' <param name="pERepresentanteCes">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 08/01/2013
    ''' </remarks>
    Public Function ModificarRepresentanteCes(ByVal pERepresentanteCes As String) As Boolean Implements IRepresentanteCesTx.ModificarRepresentanteCes

        Dim oERepresentanteCes As New EGCC_RepresentanteCes
        Dim prmParameter(10) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oERepresentanteCes = CFunciones.DeserializeObject(Of EGCC_RepresentanteCes)(pERepresentanteCes)

        'Campos 
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oERepresentanteCes.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodCesionario", DbType.Int16, 0, oERepresentanteCes.CodCesionario, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodRepresentante", DbType.Int16, 0, oERepresentanteCes.Codigorepresentante, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_NomRepresentante", DbType.String, 250, oERepresentanteCes.Nombrerepresentante, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_CodigoTipoDocumento", DbType.String, 100, oERepresentanteCes.CodigoTipoDocumento, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 11, oERepresentanteCes.Nrodocumento, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_PartidaRegistral", DbType.String, 50, oERepresentanteCes.Partidaregistral, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_OficinaRegistral", DbType.String, 50, oERepresentanteCes.Oficinaregistral, ParameterDirection.Input)

        prmParameter(8) = New DAABRequest.Parameter("@piv_AudUsuarioRegistro", DbType.String, 12, oERepresentanteCes.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_AudUsuarioModificacion", DbType.String, 12, oERepresentanteCes.Audusuariomodificacion, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_AudEstadoLogico", DbType.Int16, 0, oERepresentanteCes.EstadoLogico, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_RepresentanteCes_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarRepresentanteCes", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Eliminar el Representante
    ''' </summary>
    ''' <param name="pERepresentanteCes">Entidad Serializada formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 08/01/2013
    ''' </remarks>
    Public Function EliminarRepresentanteCes(ByVal pERepresentanteCes As String) As Boolean Implements IRepresentanteCesTx.EliminarRepresentanteCes

        Dim oERepresentanteCes As New EGCC_RepresentanteCes
        Dim prmParameter(4) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oERepresentanteCes = CFunciones.DeserializeObject(Of EGCC_RepresentanteCes)(pERepresentanteCes)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oERepresentanteCes.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodCesionario", DbType.Int16, 0, oERepresentanteCes.CodCesionario, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodRepresentante", DbType.Int16, 0, oERepresentanteCes.Codigorepresentante, ParameterDirection.Input)

        'Auditoria
        prmParameter(3) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oERepresentanteCes.EstadoLogico, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oERepresentanteCes.Audusuariomodificacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_RepresentanteCes_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarRepresentanteCes", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
''' Implementación de la clase DRepresentanteCesNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 08/01/2013
''' </remarks>
<Guid("A8CA8086-5496-4c87-8CF9-8E5A1975F14A") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DRepresentanteCesNTx")> _
Public Class DRepresentanteCesNTx
    Inherits ServicedComponent
    Implements IRepresentanteCesNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DRepresentanteCesNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene el Representante
    ''' </summary>
    ''' <param name="pERepresentanteCes">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 08/01/2013
    ''' </remarks>
    Public Function ObtenerRepresentanteCes(ByVal pERepresentanteCes As String) As String Implements IRepresentanteCesNTx.ObtenerRepresentanteCes

        Dim odtbListadoCotizacionDoc As DataTable
        Dim prmParameter(2) As DAABRequest.Parameter
        Dim oERepresentanteCes As New EGCC_RepresentanteCes
        oERepresentanteCes = CFunciones.DeserializeObject(Of EGCC_RepresentanteCes)(pERepresentanteCes)

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oERepresentanteCes.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodCesionario", DbType.Int16, 0, oERepresentanteCes.CodCesionario, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodRepresentante", DbType.Int16, 0, oERepresentanteCes.Codigorepresentante, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_RepresentanteCes_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCotizacionDoc = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerRepresentanteCes", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
    ''' Lista todos los Representantes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 08/01/2013
    ''' </remarks>
    Public Function ListadoRepresentanteCes(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pERepresentanteCes As String _
                                                ) As String Implements IRepresentanteCesNTx.ListadoRepresentanteCes

        Dim odtbListadoRepresentanteCes As DataTable
        Dim prmParameter(5) As DAABRequest.Parameter
        Dim oERepresentanteCes As New EGCC_RepresentanteCes
        oERepresentanteCes = CFunciones.DeserializeObject(Of EGCC_RepresentanteCes)(pERepresentanteCes)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oERepresentanteCes.Codsolcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_CodCesionario", DbType.Int16, 0, oERepresentanteCes.CodCesionario, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_RepresentanteCes_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoRepresentanteCes = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoRepresentanteCes", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoRepresentanteCes)
    End Function

#End Region

End Class

#End Region

