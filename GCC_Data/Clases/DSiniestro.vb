Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DSiniestroTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/10/2012
''' </remarks>
<Guid("2AE3FA9C-A7A0-4c1d-B4DD-FF2FFD8D1E4F") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DSiniestroTx")> _
Public Class DSiniestroTx
    Inherits ServicedComponent
    Implements ISiniestroTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DSiniestroTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta Siniestro
    ''' </summary>
    ''' <param name="pESiniestro">Entidad Serializado de Siniestro formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 11/12/2012
    ''' </remarks>
    Public Function InsertarSiniestro(ByVal pESiniestro As String) As Integer Implements ISiniestroTx.InsertarSiniestro

        Dim oESiniestro As New ESiniestro
        Dim parCodigoSiniestro As IDataParameter
        Dim prmParameter(35) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oESiniestro = CFunciones.DeserializeObject(Of ESiniestro)(pESiniestro)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oESiniestro.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pin_SecFinanciamiento", DbType.Int16, 0, oESiniestro.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pin_SecSiniestro", DbType.Int16, 0, oESiniestro.Secsiniestro, ParameterDirection.InputOutput)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 10, oESiniestro.Codunico, ParameterDirection.Input)

        prmParameter(4) = New DAABRequest.Parameter("@pin_MontoIndemnizacion", DbType.Decimal)
        prmParameter(4).Precision = 20
        prmParameter(4).Scale = 2
        prmParameter(4).Value = oESiniestro.Montoindemnizacion

        prmParameter(5) = New DAABRequest.Parameter("@piv_Moneda", DbType.String, 3, oESiniestro.Moneda, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pid_FecSiniestro", DbType.String, 8, oESiniestro.FecSiniestroStr, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_Tipo", DbType.String, 3, oESiniestro.Tipo, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_Situacion", DbType.String, 3, oESiniestro.Situacion, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_Aplicacion", DbType.String, 3, oESiniestro.Aplicacion, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_Transferencia", DbType.String, 3, oESiniestro.Transferencia, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_Contrato", DbType.String, 3, oESiniestro.Contrato, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_Seguro", DbType.String, 3, oESiniestro.Seguro, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_observaciones", DbType.String, 2000, oESiniestro.Observaciones, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, oESiniestro.Usuariomodificacion, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@piv_Origen", DbType.String, 5, oESiniestro.Origen, ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@pid_FecSituacion", DbType.String, 8, oESiniestro.FecSituacionStr, ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@pid_FecAplicacion", DbType.String, 8, oESiniestro.FecAplicacionStr, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@pid_FecConocimiento", DbType.String, 8, oESiniestro.FecConocimientoStr, ParameterDirection.Input)
        prmParameter(19) = New DAABRequest.Parameter("@pid_FecRecIndemnizacion", DbType.String, 8, oESiniestro.FecRecIndemnizacionStr, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@pid_FecDescargoMunicipal", DbType.String, 8, oESiniestro.FecDescargoMunicipalStr, ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@pid_FecTransferencia", DbType.String, 8, oESiniestro.FecTransferenciaStr, ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@piv_NroChequeAseguradora", DbType.String, 20, oESiniestro.NroChequeAseguradora, ParameterDirection.Input)
        prmParameter(23) = New DAABRequest.Parameter("@piv_CodEstadoBien", DbType.String, 3, oESiniestro.CodEstadoBien, ParameterDirection.Input)
        prmParameter(24) = New DAABRequest.Parameter("@piv_NroPoliza", DbType.String, 20, oESiniestro.NroPoliza, ParameterDirection.Input)
        prmParameter(25) = New DAABRequest.Parameter("@piv_CodTipoPoliza", DbType.String, 3, oESiniestro.CodTipoPoliza, ParameterDirection.Input)
        prmParameter(26) = New DAABRequest.Parameter("@piv_CodBancoEmiteCheque", DbType.String, 3, oESiniestro.CodBancoEmiteCheque, ParameterDirection.Input)
        prmParameter(27) = New DAABRequest.Parameter("@piv_CodAplicaFondo", DbType.String, 3, oESiniestro.CodAplicaFondo, ParameterDirection.Input)
        prmParameter(28) = New DAABRequest.Parameter("@piv_NroCuenta", DbType.String, 13, oESiniestro.NroCuenta, ParameterDirection.Input)
        prmParameter(29) = New DAABRequest.Parameter("@piv_CodTipoCuenta", DbType.String, 3, oESiniestro.CodTipoCuenta, ParameterDirection.Input)
        prmParameter(30) = New DAABRequest.Parameter("@piv_CodMonedaCuenta", DbType.String, 3, oESiniestro.CodMonedaCuenta, ParameterDirection.Input)
        prmParameter(31) = New DAABRequest.Parameter("@pid_FecConocimientoBanco", DbType.String, 8, oESiniestro.FecConocimientoBancoStr, ParameterDirection.Input)
        prmParameter(32) = New DAABRequest.Parameter("@piv_CodTipoSiniestro", DbType.String, 3, oESiniestro.CodTipoSiniestro, ParameterDirection.Input)
        prmParameter(33) = New DAABRequest.Parameter("@pin_CodDemanda", DbType.Int16, 0, oESiniestro.CodDemanda, ParameterDirection.Input)
        prmParameter(34) = New DAABRequest.Parameter("@pin_NroSiniestro", DbType.String, 10, oESiniestro.NroSiniestro, ParameterDirection.Input)
        prmParameter(35) = New DAABRequest.Parameter("@piv_EstadoLogico", DbType.String, 1, oESiniestro.EstadoLogico, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Siniestro_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InsertarSiniestro", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parCodigoSiniestro = CType(obRequest.Parameters(2), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckInt(parCodigoSiniestro.Value.ToString())
    End Function

    ''' <summary>
    ''' Modificar Siniestro
    ''' </summary>
    ''' <param name="pESiniestro">Entidad Serializada de Siniestro formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    Public Function ModificarSiniestro(ByVal pESiniestro As String) As Boolean Implements ISiniestroTx.ModificarSiniestro

        Dim oESiniestro As New ESiniestro
        Dim prmParameter(35) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oESiniestro = CFunciones.DeserializeObject(Of ESiniestro)(pESiniestro)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oESiniestro.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pin_SecFinanciamiento", DbType.Int16, 0, oESiniestro.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pin_SecSiniestro", DbType.Int16, 0, oESiniestro.Secsiniestro, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 10, oESiniestro.Codunico, ParameterDirection.Input)

        prmParameter(4) = New DAABRequest.Parameter("@pin_MontoIndemnizacion", DbType.Decimal)
        prmParameter(4).Precision = 18
        prmParameter(4).Scale = 6
        prmParameter(4).Value = oESiniestro.Montoindemnizacion

        prmParameter(5) = New DAABRequest.Parameter("@piv_Moneda", DbType.String, 3, oESiniestro.Moneda, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pid_FecSiniestro", DbType.String, 8, oESiniestro.FecSiniestroStr, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_Tipo", DbType.String, 3, oESiniestro.Tipo, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_Situacion", DbType.String, 3, oESiniestro.Situacion, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_Aplicacion", DbType.String, 3, oESiniestro.Aplicacion, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_Transferencia", DbType.String, 3, oESiniestro.Transferencia, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_Contrato", DbType.String, 3, oESiniestro.Contrato, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_Seguro", DbType.String, 3, oESiniestro.Seguro, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_observaciones", DbType.String, 2000, oESiniestro.Observaciones, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, oESiniestro.Usuariomodificacion, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@piv_Origen", DbType.String, 5, oESiniestro.Origen, ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@pid_FecSituacion", DbType.String, 8, oESiniestro.FecSituacionStr, ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@pid_FecAplicacion", DbType.String, 8, oESiniestro.FecAplicacionStr, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@pid_FecConocimiento", DbType.String, 8, oESiniestro.FecConocimientoStr, ParameterDirection.Input)
        prmParameter(19) = New DAABRequest.Parameter("@pid_FecRecIndemnizacion", DbType.String, 8, oESiniestro.FecRecIndemnizacionStr, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@pid_FecDescargoMunicipal", DbType.String, 8, oESiniestro.FecDescargoMunicipalStr, ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@pid_FecTransferencia", DbType.String, 8, oESiniestro.FecTransferenciaStr, ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@piv_NroChequeAseguradora", DbType.String, 20, oESiniestro.NroChequeAseguradora, ParameterDirection.Input)
        prmParameter(23) = New DAABRequest.Parameter("@piv_CodEstadoBien", DbType.String, 3, oESiniestro.CodEstadoBien, ParameterDirection.Input)
        prmParameter(24) = New DAABRequest.Parameter("@piv_NroPoliza", DbType.String, 20, oESiniestro.NroPoliza, ParameterDirection.Input)
        prmParameter(25) = New DAABRequest.Parameter("@piv_CodTipoPoliza", DbType.String, 3, oESiniestro.CodTipoPoliza, ParameterDirection.Input)
        prmParameter(26) = New DAABRequest.Parameter("@piv_CodBancoEmiteCheque", DbType.String, 3, oESiniestro.CodBancoEmiteCheque, ParameterDirection.Input)
        prmParameter(27) = New DAABRequest.Parameter("@piv_CodAplicaFondo", DbType.String, 3, oESiniestro.CodAplicaFondo, ParameterDirection.Input)
        prmParameter(28) = New DAABRequest.Parameter("@piv_NroCuenta", DbType.String, 13, oESiniestro.NroCuenta, ParameterDirection.Input)
        prmParameter(29) = New DAABRequest.Parameter("@piv_CodTipoCuenta", DbType.String, 3, oESiniestro.CodTipoCuenta, ParameterDirection.Input)
        prmParameter(30) = New DAABRequest.Parameter("@piv_CodMonedaCuenta", DbType.String, 3, oESiniestro.CodMonedaCuenta, ParameterDirection.Input)
        prmParameter(31) = New DAABRequest.Parameter("@pid_FecConocimientoBanco", DbType.String, 8, oESiniestro.FecConocimientoBancoStr, ParameterDirection.Input)
        prmParameter(32) = New DAABRequest.Parameter("@piv_CodTipoSiniestro", DbType.String, 3, oESiniestro.CodTipoSiniestro, ParameterDirection.Input)
        prmParameter(33) = New DAABRequest.Parameter("@pin_CodDemanda", DbType.Int16, 0, oESiniestro.CodDemanda, ParameterDirection.Input)
        prmParameter(34) = New DAABRequest.Parameter("@pin_NroSiniestro", DbType.String, 10, oESiniestro.NroSiniestro, ParameterDirection.Input)
        prmParameter(35) = New DAABRequest.Parameter("@piv_EstadoLogico", DbType.String, 1, oESiniestro.EstadoLogico, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Siniestro_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarSiniestro", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Eliminar el Siniestro
    ''' </summary>
    ''' <param name="pESiniestro">Entidad Serializada de Siniestro formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    Public Function EliminarSiniestro(ByVal pESiniestro As String) As Boolean Implements ISiniestroTx.EliminarSiniestro

        Dim oESiniestro As New ESiniestro
        Dim prmParameter(4) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oESiniestro = CFunciones.DeserializeObject(Of ESiniestro)(pESiniestro)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oESiniestro.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pin_SecFinanciamiento", DbType.Int16, 0, oESiniestro.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pin_SecSiniestro", DbType.Int16, 0, oESiniestro.Secsiniestro, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 12, oESiniestro.Usuariomodificacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_EstadoLogico", DbType.String, 1, oESiniestro.EstadoLogico, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Siniestro_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "EliminarSiniestro", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
''' Implementación de la clase DSiniestroNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 12/11/2012
''' </remarks>
<Guid("89C93672-D770-4F62-9C69-55FD41892C90") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DSiniestroNTx")> _
Public Class DSiniestroNTx
    Inherits ServicedComponent
    Implements ISiniestroNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DSiniestroNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene el Siniestro Contrato
    ''' </summary>
    ''' <param name="pESiniestro">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012 
    ''' </remarks>
    Public Function GetSiniestroContrato(ByVal pESiniestro As String) As String Implements ISiniestroNTx.GetSiniestroContrato

        Dim odtbListadoCotizacionDoc As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter
        Dim oESiniestro As New ESiniestro
        oESiniestro = CFunciones.DeserializeObject(Of ESiniestro)(pESiniestro)

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oESiniestro.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pin_SecFinanciamiento", DbType.Int16, 0, oESiniestro.Secfinanciamiento, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_SiniestroContrato_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCotizacionDoc = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GetSiniestro", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
    Public Function ListadoSiniestroContrato(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pESiniestro As String _
                                                ) As String Implements ISiniestroNTx.ListadoSiniestroContrato

        Dim odtbListadoSiniestro As DataTable
        Dim prmParameter(14) As DAABRequest.Parameter
        Dim oESiniestro As New ESiniestro
        oESiniestro = CFunciones.DeserializeObject(Of ESiniestro)(pESiniestro)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@NroContrato", DbType.String, 8, oESiniestro.NroContrato, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@EstadoContrato", DbType.String, 3, oESiniestro.EstadoContrato, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@CUCliente", DbType.String, 15, oESiniestro.CUCliente, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@TipoDocumento", DbType.String, 3, oESiniestro.TipoDocumento, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@NroDocumento", DbType.String, 20, oESiniestro.NroDocumento, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@RazonSocial", DbType.String, 200, oESiniestro.RazonSocial, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@ClasificacionBien", DbType.String, 3, oESiniestro.ClasificacionBien, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@Placa", DbType.String, 50, oESiniestro.Placa, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@NroMotor", DbType.String, 50, oESiniestro.NroMotor, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@TipoBien", DbType.String, 3, oESiniestro.TipoBien, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@Ubicacion", DbType.String, 200, oESiniestro.Ubicacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_SiniestroContrato_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoSiniestro = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoSiniestro", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoSiniestro)
    End Function

    ''' <summary>
    ''' Obtiene el Siniestro
    ''' </summary>
    ''' <param name="pESiniestro">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012 
    ''' </remarks>
    Public Function GetSiniestro(ByVal pESiniestro As String) As String Implements ISiniestroNTx.GetSiniestro

        Dim odtbListadoCotizacionDoc As DataTable
        Dim prmParameter(2) As DAABRequest.Parameter
        Dim oESiniestro As New ESiniestro
        oESiniestro = CFunciones.DeserializeObject(Of ESiniestro)(pESiniestro)

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oESiniestro.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pin_SecFinanciamiento", DbType.Int16, 0, oESiniestro.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pin_SecSiniestro", DbType.Int16, 0, oESiniestro.Secsiniestro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Siniestro_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCotizacionDoc = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GetSiniestro", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
    ''' Obtiene el Siniestro
    ''' </summary>
    ''' <param name="pESiniestro">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 18/01/2013
    ''' </remarks>
    Public Function GetSiniestroConsulta(ByVal pESiniestro As String) As String Implements ISiniestroNTx.GetSiniestroConsulta

        Dim odtbListadoCotizacionDoc As DataTable
        Dim prmParameter(2) As DAABRequest.Parameter
        Dim oESiniestro As New ESiniestro
        oESiniestro = CFunciones.DeserializeObject(Of ESiniestro)(pESiniestro)

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oESiniestro.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pin_SecFinanciamiento", DbType.Int16, 0, oESiniestro.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pin_SecSiniestro", DbType.Int16, 0, oESiniestro.Secsiniestro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_SiniestroConsulta_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCotizacionDoc = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GetSiniestroConsulta", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
    ''' Listado de Siniestro para exportar
    ''' </summary>
    ''' <param name="pESiniestro">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 21/01/2013
    ''' </remarks>
    Public Function ListadoReporteSiniestro(ByVal pESiniestro As String) As String Implements ISiniestroNTx.ListadoReporteSiniestro

        Dim odtbListadoCotizacionDoc As DataTable
        Dim prmParameter(10) As DAABRequest.Parameter
        Dim oESiniestro As New ESiniestro
        oESiniestro = CFunciones.DeserializeObject(Of ESiniestro)(pESiniestro)

        prmParameter(0) = New DAABRequest.Parameter("@NroContrato", DbType.String, 8, oESiniestro.NroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@EstadoContrato", DbType.String, 3, oESiniestro.EstadoContrato, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@CUCliente", DbType.String, 15, oESiniestro.CUCliente, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@TipoDocumento", DbType.String, 3, oESiniestro.TipoDocumento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@NroDocumento", DbType.String, 20, oESiniestro.NroDocumento, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@RazonSocial", DbType.String, 200, oESiniestro.RazonSocial, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@ClasificacionBien", DbType.String, 3, oESiniestro.ClasificacionBien, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@Placa", DbType.String, 50, oESiniestro.Placa, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@NroMotor", DbType.String, 50, oESiniestro.NroMotor, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@TipoBien", DbType.String, 3, oESiniestro.TipoBien, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@Ubicacion", DbType.String, 200, oESiniestro.Ubicacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Siniestro_rpt"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCotizacionDoc = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoReporteSiniestro", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
    Public Function ListadoSiniestro(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pESiniestro As String _
                                                ) As String Implements ISiniestroNTx.ListadoSiniestro

        Dim odtbListadoSiniestro As DataTable
        Dim prmParameter(7) As DAABRequest.Parameter
        Dim oESiniestro As New ESiniestro
        oESiniestro = CFunciones.DeserializeObject(Of ESiniestro)(pESiniestro)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oESiniestro.Codsolcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pin_SecFinanciamiento", DbType.Int16, 0, oESiniestro.Secfinanciamiento, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_CodTipoSiniestro", DbType.String, 3, oESiniestro.CodTipoSiniestro, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_NroSiniestro", DbType.String, 10, oESiniestro.NroSiniestro, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Siniestro_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoSiniestro = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoSiniestro", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoSiniestro)
    End Function

#End Region

End Class

#End Region
