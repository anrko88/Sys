Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DDemandaTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/10/2012
''' </remarks>
<Guid("5C7EAE3A-FAB2-4719-B33A-4457393163BB") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DDemandaTx")> _
Public Class DDemandaTx
    Inherits ServicedComponent
    Implements IDemandaTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DDemandaTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta Demanda
    ''' </summary>
    ''' <param name="pEDemanda">Entidad Serializado de Demanda formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    Public Function InsertarDemanda(ByVal pEDemanda As String) As String Implements IDemandaTx.InsertarDemanda

        Dim parCodigoDemanda As IDataParameter
        Dim oEDemanda As New EGcc_Demanda
        Dim prmParameter(12) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEDemanda = CFunciones.DeserializeObject(Of EGcc_Demanda)(pEDemanda)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oEDemanda.CodSolCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinanciamiento", DbType.Int16, 0, oEDemanda.SecFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pii_CodDemanda", DbType.Int16, 0, oEDemanda.CodDemanda, ParameterDirection.InputOutput)
        prmParameter(3) = New DAABRequest.Parameter("@pid_FechaDemanda", DbType.String, 8, oEDemanda.FechaDemandaStr, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_EstadoDemanda", DbType.String, 3, oEDemanda.EstadoDemanda, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Juzgado", DbType.String, 50, oEDemanda.Juzgado, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_CodMoneda", DbType.String, 3, oEDemanda.CodMoneda, ParameterDirection.Input)

        prmParameter(7) = New DAABRequest.Parameter("@pin_MontoDemanda", DbType.Decimal)
        prmParameter(7).Precision = 20
        prmParameter(7).Scale = 2
        prmParameter(7).Value = oEDemanda.MontoDemanda

        prmParameter(8) = New DAABRequest.Parameter("@piv_UsuarioRegistro", DbType.String, 8, oEDemanda.UsuarioRegistro, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, oEDemanda.UsuarioModificacion, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_NroDemanda", DbType.String, 10, oEDemanda.NroDemanda, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_EstadoLogico", DbType.String, 1, oEDemanda.EstadoLogico, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pii_CodSiniestro", DbType.Int16, 0, oEDemanda.CodSiniestro, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Demanda_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InsertarDemanda", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parCodigoDemanda = CType(obRequest.Parameters(2), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckStr(parCodigoDemanda.Value.ToString())
    End Function

    ''' <summary>
    ''' Modificar Demanda
    ''' </summary>
    ''' <param name="pEDemanda">Entidad Serializada de Demanda formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    Public Function ModificarDemanda(ByVal pEDemanda As String) As Boolean Implements IDemandaTx.ModificarDemanda

        Dim oEDemanda As New EGcc_Demanda
        Dim prmParameter(12) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEDemanda = CFunciones.DeserializeObject(Of EGcc_Demanda)(pEDemanda)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oEDemanda.CodSolCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinanciamiento", DbType.Int16, 0, oEDemanda.SecFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pii_CodDemanda", DbType.Int16, 0, oEDemanda.CodDemanda, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pid_FechaDemanda", DbType.String, 8, oEDemanda.FechaDemandaStr, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_EstadoDemanda", DbType.String, 3, oEDemanda.EstadoDemanda, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Juzgado", DbType.String, 50, oEDemanda.Juzgado, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_CodMoneda", DbType.String, 3, oEDemanda.CodMoneda, ParameterDirection.Input)

        prmParameter(7) = New DAABRequest.Parameter("@pin_MontoDemanda", DbType.Decimal)
        prmParameter(7).Precision = 20
        prmParameter(7).Scale = 2
        prmParameter(7).Value = oEDemanda.MontoDemanda

        prmParameter(8) = New DAABRequest.Parameter("@piv_UsuarioRegistro", DbType.String, 8, oEDemanda.UsuarioRegistro, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, oEDemanda.UsuarioModificacion, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_NroDemanda", DbType.String, 10, oEDemanda.NroDemanda, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_EstadoLogico", DbType.String, 1, oEDemanda.EstadoLogico, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pii_CodSiniestro", DbType.Int16, 0, oEDemanda.CodSiniestro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Demanda_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarDemanda", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Eliminar el Demanda
    ''' </summary>
    ''' <param name="pEDemanda">Entidad Serializada de Demanda formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    Public Function EliminarDemanda(ByVal pEDemanda As String) As Boolean Implements IDemandaTx.EliminarDemanda

        Dim oEDemanda As New EGcc_Demanda
        Dim prmParameter(4) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEDemanda = CFunciones.DeserializeObject(Of EGcc_Demanda)(pEDemanda)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oEDemanda.CodSolCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinanciamiento", DbType.Int16, 0, oEDemanda.SecFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pii_CodDemanda", DbType.Int16, 0, oEDemanda.CodDemanda, ParameterDirection.Input)

        'Auditoria
        prmParameter(3) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, oEDemanda.UsuarioModificacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_EstadoLogico", DbType.String, 1, oEDemanda.EstadoLogico, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Demanda_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "EliminarDemanda", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

#Region "Implicados"

    ''' <summary>
    ''' Inserta Implicado
    ''' </summary>
    ''' <param name="pEDemanda">Entidad Serializado de Demanda formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    Public Function InsertarDemandaImplicado(ByVal pEImplicado As String) As Boolean Implements IDemandaTx.InsertarDemandaImplicado

        Dim oEImplicado As New EGCC_Implicado
        Dim prmParameter(10) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEImplicado = CFunciones.DeserializeObject(Of EGCC_Implicado)(pEImplicado)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oEImplicado.CodSolCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_SecFinanciamiento", DbType.Int16, 0, oEImplicado.SecFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodDemanda", DbType.Int16, 0, oEImplicado.CodDemanda, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodImplicado", DbType.Int16, 0, oEImplicado.CodImplicado, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_NombreImplicado", DbType.String, 50, oEImplicado.NombreImplicado, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_CodTipoDocumento", DbType.String, 3, oEImplicado.CodTipoDocumento, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 15, oEImplicado.NroDocumento, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_UsuarioRegistro", DbType.String, 8, oEImplicado.UsuarioRegistro, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, oEImplicado.UsuarioModificacion, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_CodTipoImplicado", DbType.String, 3, oEImplicado.CodTipoImplicado, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_EstadoLogico", DbType.String, 1, oEImplicado.EstadoLogico, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_DemandaImplicado_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InsertarDemanda", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Modificar Implicado
    ''' </summary>
    ''' <param name="pEImplicado">Entidad Serializada de Demanda formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 11/12/2012
    ''' </remarks>
    Public Function ModificarDemandaImplicado(ByVal pEImplicado As String) As Boolean Implements IDemandaTx.ModificarDemandaImplicado

        Dim oEImplicado As New EGCC_Implicado
        Dim prmParameter(10) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEImplicado = CFunciones.DeserializeObject(Of EGCC_Implicado)(pEImplicado)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oEImplicado.CodSolCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_SecFinanciamiento", DbType.Int16, 0, oEImplicado.SecFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodDemanda", DbType.Int16, 0, oEImplicado.CodDemanda, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodImplicado", DbType.Int16, 0, oEImplicado.CodImplicado, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_NombreImplicado", DbType.String, 50, oEImplicado.NombreImplicado, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_CodTipoDocumento", DbType.String, 3, oEImplicado.CodTipoDocumento, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 15, oEImplicado.NroDocumento, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_UsuarioRegistro", DbType.String, 8, oEImplicado.UsuarioRegistro, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, oEImplicado.UsuarioModificacion, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_CodTipoImplicado", DbType.String, 3, oEImplicado.CodTipoImplicado, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_EstadoLogico", DbType.String, 1, oEImplicado.EstadoLogico, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_DemandaImplicado_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarDemanda", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Eliminar el Implicado
    ''' </summary>
    ''' <param name="pEImplicado">Entidad Serializada de Demanda formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012
    ''' </remarks>
    Public Function EliminarDemandaImplicado(ByVal pEImplicado As String) As Boolean Implements IDemandaTx.EliminarDemandaImplicado

        Dim oEImplicado As New EGCC_Implicado
        Dim prmParameter(5) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEImplicado = CFunciones.DeserializeObject(Of EGCC_Implicado)(pEImplicado)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oEImplicado.CodSolCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_SecFinanciamiento", DbType.Int16, 0, oEImplicado.SecFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodDemanda", DbType.Int16, 0, oEImplicado.CodDemanda, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodImplicado", DbType.Int16, 0, oEImplicado.CodImplicado, ParameterDirection.Input)

        'Auditoria
        prmParameter(4) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, oEImplicado.UsuarioModificacion, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_EstadoLogico", DbType.String, 1, oEImplicado.EstadoLogico, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_DemandaImplicado_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "EliminarDemandaImplicado", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

#End Region

#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DDemandaNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 12/11/2012
''' </remarks>
<Guid("EA2A7DD7-DFB0-491a-83E3-46365C35B92E") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DDemandaNTx")> _
Public Class DDemandaNTx
    Inherits ServicedComponent
    Implements IDemandaNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DDemandaNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene el Demanda Contrato
    ''' </summary>
    ''' <param name="pEDemanda">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012 
    ''' </remarks>
    Public Function GetDemandaContrato(ByVal pEDemanda As String) As String Implements IDemandaNTx.GetDemandaContrato

        Dim odtbListadoCotizacionDoc As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter
        Dim oEDemanda As New EGcc_Demanda
        oEDemanda = CFunciones.DeserializeObject(Of EGcc_Demanda)(pEDemanda)

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oEDemanda.CodSolCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pin_SecFinanciamiento", DbType.Int16, 0, oEDemanda.SecFinanciamiento, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DemandaContrato_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCotizacionDoc = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GetDemandaContrato", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function ListadoDemandaContrato(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEDemanda As String _
                                                ) As String Implements IDemandaNTx.ListadoDemandaContrato

        Dim odtbListadoDemanda As DataTable
        Dim prmParameter(14) As DAABRequest.Parameter
        Dim oEDemanda As New EGcc_Demanda
        oEDemanda = CFunciones.DeserializeObject(Of EGcc_Demanda)(pEDemanda)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@NroContrato", DbType.String, 8, oEDemanda.NroContrato, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@EstadoContrato", DbType.String, 3, oEDemanda.EstadoContrato, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@CUCliente", DbType.String, 15, oEDemanda.CUCliente, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@TipoDocumento", DbType.String, 3, oEDemanda.TipoDocumento, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@NroDocumento", DbType.String, 20, oEDemanda.NroDocumento, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@RazonSocial", DbType.String, 200, oEDemanda.RazonSocial, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@ClasificacionBien", DbType.String, 3, oEDemanda.ClasificacionBien, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@Placa", DbType.String, 50, oEDemanda.Placa, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@NroMotor", DbType.String, 50, oEDemanda.NroMotor, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@TipoBien", DbType.String, 3, oEDemanda.TipoBien, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@Ubicacion", DbType.String, 200, oEDemanda.Ubicacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DemandaContrato_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoDemanda = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoDemandaContrato", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoDemanda)
    End Function

    ''' <summary>
    ''' Obtiene el Demanda
    ''' </summary>
    ''' <param name="pEDemanda">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012 
    ''' </remarks>
    Public Function GetDemanda(ByVal pEDemanda As String) As String Implements IDemandaNTx.GetDemanda

        Dim odtbListadoDemanda As DataTable
        Dim prmParameter(2) As DAABRequest.Parameter
        Dim oEDemanda As New EGcc_Demanda
        oEDemanda = CFunciones.DeserializeObject(Of EGcc_Demanda)(pEDemanda)

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oEDemanda.CodSolCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pin_SecFinanciamiento", DbType.Int16, 0, oEDemanda.SecFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pin_CodDemanda", DbType.Int16, 0, oEDemanda.CodDemanda, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Demanda_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoDemanda = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GetDemanda", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If odtbListadoDemanda Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoDemanda)
        End If
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function ListadoDemanda(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEDemanda As String _
                                                ) As String Implements IDemandaNTx.ListadoDemanda

        Dim odtbListadoDemanda As DataTable
        Dim prmParameter(5) As DAABRequest.Parameter
        Dim oEDemanda As New EGcc_Demanda
        oEDemanda = CFunciones.DeserializeObject(Of EGcc_Demanda)(pEDemanda)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oEDemanda.CodSolCredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pin_SecFinanciamiento", DbType.Int16, 0, oEDemanda.SecFinanciamiento, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Demanda_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoDemanda = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoDemanda", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoDemanda)
    End Function

#Region "Implicados"

    ''' <summary>
    ''' Lista Implicados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function ListadoDemandaImplicado(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEImplicado As String _
                                                ) As String Implements IDemandaNTx.ListadoDemandaImplicado

        Dim odtbListadoDemanda As DataTable
        Dim prmParameter(6) As DAABRequest.Parameter
        Dim oEImplicado As New EGCC_Implicado
        oEImplicado = CFunciones.DeserializeObject(Of EGCC_Implicado)(pEImplicado)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oEImplicado.CodSolCredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_SecFinanciamiento", DbType.Int16, 0, oEImplicado.SecFinanciamiento, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_CodDemanda", DbType.Int16, 0, oEImplicado.CodDemanda, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DemandaImplicado_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoDemanda = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoDemandaImplicado", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoDemanda)
    End Function

    ''' <summary>
    ''' Obtiene el Implicado
    ''' </summary>
    ''' <param name="pEImplicado">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012 
    ''' </remarks>
    Public Function GetDemandaImplicado(ByVal pEImplicado As String) As String Implements IDemandaNTx.GetDemandaImplicado

        Dim odtbListadoDemanda As DataTable
        Dim prmParameter(3) As DAABRequest.Parameter
        Dim oEImplicado As New EGCC_Implicado
        oEImplicado = CFunciones.DeserializeObject(Of EGCC_Implicado)(pEImplicado)

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oEImplicado.CodSolCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_SecFinanciamiento", DbType.Int16, 0, oEImplicado.SecFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodDemanda", DbType.Int16, 0, oEImplicado.CodDemanda, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodImplicado", DbType.Int16, 0, oEImplicado.CodImplicado, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DemandaImplicado_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoDemanda = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GetDemanda", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If odtbListadoDemanda Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoDemanda)
        End If
    End Function

#End Region

#End Region

End Class

#End Region
