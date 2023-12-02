Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DCotizacionCronogramaTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("7B2D8C76-E94D-4f65-8EB4-E52D36E620E4") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCotizacionCronogramaTx")> _
Public Class DCotizacionCronogramaTx
    Inherits ServicedComponent
    Implements ICotizacionCronogramaTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DCotizacionCronogramaTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta el cronograma
    ''' </summary>
    ''' <param name="pECotizacioncronograma">Entidad Serializado de Contacto formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012
    ''' </remarks>
    Public Function InsertarCronograma(ByVal pECotizacioncronograma As String) As Boolean Implements ICotizacionCronogramaTx.InsertarCronograma

        Dim oEGcc_cotizacioncronograma As New EGcc_cotizacioncronograma
        Dim prmParameter(21) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGcc_cotizacioncronograma = CFunciones.DeserializeObject(Of EGcc_cotizacioncronograma)(pECotizacioncronograma)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@pii_NumeroCuota", DbType.String, 8, oEGcc_cotizacioncronograma.Numerocuota, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodigoCotizacion", DbType.String, 8, oEGcc_cotizacioncronograma.Codigocotizacion, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_VersionCotizacion", DbType.String, 8, oEGcc_cotizacioncronograma.Versioncotizacion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pid_FechaVencimiento", DbType.String, 10, oEGcc_cotizacioncronograma.SFechavencimiento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pii_CantDiasCuota", DbType.String, 8, oEGcc_cotizacioncronograma.Cantdiascuota, ParameterDirection.Input)


        prmParameter(5) = New DAABRequest.Parameter("@pin_MontoSaldoAdeudado", DbType.Decimal)
        prmParameter(5).Precision = 18
        prmParameter(5).Scale = 6
        prmParameter(5).Value = oEGcc_cotizacioncronograma.Montosaldoadeudado
        prmParameter(6) = New DAABRequest.Parameter("@pin_MontoInteresBien", DbType.Decimal)
        prmParameter(6).Precision = 18
        prmParameter(6).Scale = 6
        prmParameter(6).Value = oEGcc_cotizacioncronograma.Montointeresbien
        prmParameter(7) = New DAABRequest.Parameter("@pin_MontoPrincipalBien", DbType.Decimal)
        prmParameter(7).Precision = 18
        prmParameter(7).Scale = 6
        prmParameter(7).Value = oEGcc_cotizacioncronograma.Montoprincipalbien
        prmParameter(8) = New DAABRequest.Parameter("@pin_MontoTotalCuota", DbType.Decimal)
        prmParameter(8).Precision = 18
        prmParameter(8).Scale = 6
        prmParameter(8).Value = oEGcc_cotizacioncronograma.Montototalcuota
        prmParameter(9) = New DAABRequest.Parameter("@pin_SaldoSeguro", DbType.Decimal)
        prmParameter(9).Precision = 18
        prmParameter(9).Scale = 6
        prmParameter(9).Value = oEGcc_cotizacioncronograma.Saldoseguro
        prmParameter(10) = New DAABRequest.Parameter("@pin_InteresSeguroBien", DbType.Decimal)
        prmParameter(10).Precision = 18
        prmParameter(10).Scale = 6
        prmParameter(10).Value = oEGcc_cotizacioncronograma.Interessegurobien
        prmParameter(11) = New DAABRequest.Parameter("@pin_PrincipalSeguroBien", DbType.Decimal)
        prmParameter(11).Precision = 18
        prmParameter(11).Scale = 6
        prmParameter(11).Value = oEGcc_cotizacioncronograma.Principalsegurobien
        prmParameter(12) = New DAABRequest.Parameter("@pin_MontoCuotaSeguroBien", DbType.Decimal)
        prmParameter(12).Precision = 18
        prmParameter(12).Scale = 6
        prmParameter(12).Value = oEGcc_cotizacioncronograma.Montocuotasegurobien



        prmParameter(13) = New DAABRequest.Parameter("@pin_SaldoSeguroDes", DbType.Decimal)
        prmParameter(13).Precision = 18
        prmParameter(13).Scale = 6
        prmParameter(13).Value = oEGcc_cotizacioncronograma.SaldoSeguroDes
        prmParameter(14) = New DAABRequest.Parameter("@pin_InteresSeguroDes", DbType.Decimal)
        prmParameter(14).Precision = 18
        prmParameter(14).Scale = 6
        prmParameter(14).Value = oEGcc_cotizacioncronograma.InteresSeguroDes
        prmParameter(15) = New DAABRequest.Parameter("@pin_PrincipalSeguroDes", DbType.Decimal)
        prmParameter(15).Precision = 18
        prmParameter(15).Scale = 6
        prmParameter(15).Value = oEGcc_cotizacioncronograma.PrincipalSeguroDes
        prmParameter(16) = New DAABRequest.Parameter("@pin_CuotaSeguroDes", DbType.Decimal)
        prmParameter(16).Precision = 18
        prmParameter(16).Scale = 6
        prmParameter(16).Value = oEGcc_cotizacioncronograma.CuotaSeguroDes



        prmParameter(17) = New DAABRequest.Parameter("@pin_MontoTotalCuotaIGV", DbType.Decimal)
        prmParameter(17).Precision = 18
        prmParameter(17).Scale = 6
        prmParameter(17).Value = oEGcc_cotizacioncronograma.Montototalcuotaigv
        prmParameter(18) = New DAABRequest.Parameter("@pin_TotalAPagar", DbType.Decimal)
        prmParameter(18).Precision = 18
        prmParameter(18).Scale = 6
        prmParameter(18).Value = oEGcc_cotizacioncronograma.Totalapagar



        prmParameter(19) = New DAABRequest.Parameter("@pii_AudEstadoLogico", DbType.String, 8, oEGcc_cotizacioncronograma.Audestadologico, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@piv_AudUsuarioRegistro", DbType.String, 8, oEGcc_cotizacioncronograma.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@piv_AudUsuarioModificacion", DbType.String, 8, oEGcc_cotizacioncronograma.Audusuariomodificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CotizacionCronograma_Ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InsertarCronograma", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Modificar el cronograma
    ''' </summary>
    ''' <param name="pECotizacioncronograma">Entidad Serializado de Contacto formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 25/05/2012
    ''' </remarks>
    Public Function ModificarCronograma(ByVal pECotizacioncronograma As String) As Boolean Implements ICotizacionCronogramaTx.ModificarCronograma

        Dim oEGcc_cotizacioncronograma As New EGcc_cotizacioncronograma
        Dim prmParameter(20) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGcc_cotizacioncronograma = CFunciones.DeserializeObject(Of EGcc_cotizacioncronograma)(pECotizacioncronograma)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@pii_NumeroCuota", DbType.String, 8, oEGcc_cotizacioncronograma.Numerocuota, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodigoCotizacion", DbType.String, 8, oEGcc_cotizacioncronograma.Codigocotizacion, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_VersionCotizacion", DbType.String, 8, oEGcc_cotizacioncronograma.Versioncotizacion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pid_FechaVencimiento", DbType.String, 8, oEGcc_cotizacioncronograma.Fechavencimiento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pii_CantDiasCuota", DbType.String, 8, oEGcc_cotizacioncronograma.Cantdiascuota, ParameterDirection.Input)

        prmParameter(5) = New DAABRequest.Parameter("@pin_MontoSaldoAdeudado", DbType.Decimal)
        prmParameter(5).Precision = 18
        prmParameter(5).Scale = 6
        prmParameter(5).Value = oEGcc_cotizacioncronograma.Montosaldoadeudado
        prmParameter(6) = New DAABRequest.Parameter("@pin_MontoInteresBien", DbType.Decimal)
        prmParameter(6).Precision = 18
        prmParameter(6).Scale = 6
        prmParameter(6).Value = oEGcc_cotizacioncronograma.Montointeresbien
        prmParameter(7) = New DAABRequest.Parameter("@pin_MontoPrincipalBien", DbType.Decimal)
        prmParameter(7).Precision = 18
        prmParameter(7).Scale = 6
        prmParameter(7).Value = oEGcc_cotizacioncronograma.Montoprincipalbien
        prmParameter(8) = New DAABRequest.Parameter("@pin_MontoTotalCuota", DbType.Decimal)
        prmParameter(8).Precision = 18
        prmParameter(8).Scale = 6
        prmParameter(8).Value = oEGcc_cotizacioncronograma.Montototalcuota
        prmParameter(9) = New DAABRequest.Parameter("@pin_SaldoSeguro", DbType.Decimal)
        prmParameter(9).Precision = 18
        prmParameter(9).Scale = 6
        prmParameter(9).Value = oEGcc_cotizacioncronograma.Saldoseguro
        prmParameter(10) = New DAABRequest.Parameter("@pin_InteresSeguroBien", DbType.Decimal)
        prmParameter(10).Precision = 18
        prmParameter(10).Scale = 6
        prmParameter(10).Value = oEGcc_cotizacioncronograma.Interessegurobien
        prmParameter(11) = New DAABRequest.Parameter("@pin_PrincipalSeguroBien", DbType.Decimal)
        prmParameter(11).Precision = 18
        prmParameter(11).Scale = 6
        prmParameter(11).Value = oEGcc_cotizacioncronograma.Principalsegurobien
        prmParameter(12) = New DAABRequest.Parameter("@pin_MontoCuotaSeguroBien", DbType.Decimal)
        prmParameter(12).Precision = 18
        prmParameter(12).Scale = 6
        prmParameter(12).Value = oEGcc_cotizacioncronograma.Montocuotasegurobien



        prmParameter(13) = New DAABRequest.Parameter("@pin_SaldoSeguroDes", DbType.Decimal)
        prmParameter(13).Precision = 18
        prmParameter(13).Scale = 6
        prmParameter(13).Value = oEGcc_cotizacioncronograma.SaldoSeguroDes
        prmParameter(14) = New DAABRequest.Parameter("@pin_InteresSeguroDes", DbType.Decimal)
        prmParameter(14).Precision = 18
        prmParameter(14).Scale = 6
        prmParameter(14).Value = oEGcc_cotizacioncronograma.InteresSeguroDes
        prmParameter(15) = New DAABRequest.Parameter("@pin_PrincipalSeguroDes", DbType.Decimal)
        prmParameter(15).Precision = 18
        prmParameter(15).Scale = 6
        prmParameter(15).Value = oEGcc_cotizacioncronograma.PrincipalSeguroDes
        prmParameter(16) = New DAABRequest.Parameter("@pin_CuotaSeguroDes", DbType.Decimal)
        prmParameter(16).Precision = 18
        prmParameter(16).Scale = 6
        prmParameter(16).Value = oEGcc_cotizacioncronograma.CuotaSeguroDes



        prmParameter(17) = New DAABRequest.Parameter("@pin_MontoTotalCuotaIGV", DbType.Decimal)
        prmParameter(17).Precision = 18
        prmParameter(17).Scale = 6
        prmParameter(17).Value = oEGcc_cotizacioncronograma.Montototalcuotaigv
        prmParameter(18) = New DAABRequest.Parameter("@pin_TotalAPagar", DbType.Decimal)
        prmParameter(18).Precision = 18
        prmParameter(18).Scale = 6
        prmParameter(18).Value = oEGcc_cotizacioncronograma.Totalapagar



        prmParameter(19) = New DAABRequest.Parameter("@pii_AudEstadoLogico", DbType.String, 8, oEGcc_cotizacioncronograma.Audestadologico, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@piv_AudUsuarioModificacion", DbType.String, 8, oEGcc_cotizacioncronograma.Audusuariomodificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CotizacionCronograma_Upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarCronograma", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Eliminar el cronograma
    ''' </summary>
    ''' <param name="pECotizacioncronograma">Entidad Serializado de Contacto formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 25/05/2012
    ''' </remarks>
    Public Function EliminarCronograma(ByVal pECotizacioncronograma As String) As Boolean Implements ICotizacionCronogramaTx.EliminarCronograma

        Dim oEGcc_cotizacioncronograma As New EGcc_cotizacioncronograma
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGcc_cotizacioncronograma = CFunciones.DeserializeObject(Of EGcc_cotizacioncronograma)(pECotizacioncronograma)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodigoCotizacion", DbType.String, 8, oEGcc_cotizacioncronograma.Codigocotizacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_VersionCotizacion", DbType.String, 8, oEGcc_cotizacioncronograma.Versioncotizacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CotizacionCronograma_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "EliminarCronograma", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
''' Implementación de la clase DCotizacionCronogramaNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("761D32C7-AF20-4754-B144-F78E532E94C9") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCotizacionCronogramaNTx")> _
Public Class DCotizacionCronogramaNTx
    Inherits ServicedComponent
    Implements ICotizacionCronogramaNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DCotizacionCronogramaNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene una version de una cotizacion cronograma especifica
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 24/05/2012
    ''' </remarks>
    Public Function ObtenerCotizacionCronograma(ByVal pECotizacion As String) As String Implements ICotizacionCronogramaNTx.ObtenerCotizacionCronograma
        'Variables
        Dim odtbCronograma As New DataTable
        Dim oECotizacion As New EGcc_cotizacion
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oECotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_NroCotizacion", DbType.String, 8, oECotizacion.Codigocotizacion, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_VersionCotizacion", DbType.String, 8, oECotizacion.Versioncotizacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CotizacionCronograma_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbCronograma = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerCotizacionCronograma", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbCronograma)
    End Function

    ''' <summary>
    ''' Obtiene cronograma actual de una cotizcion
    ''' </summary>
    ''' <param name="pstrNumeroCotizacion">Numero Cotizacion</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 25/05/2012
    ''' </remarks>
    Public Function ObtenerCronogramaActual(ByVal pstrNumeroCotizacion As String) As String Implements ICotizacionCronogramaNTx.ObtenerCronogramaActual
        'Variables
        Dim odtbCronograma As New DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_NroCotizacion", DbType.String, 8, pstrNumeroCotizacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CotCronogramaMaximo_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbCronograma = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerCronogramaActual", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbCronograma)
    End Function

    ''' <summary>
    ''' Obtiene una version de una cotizacion cronograma especifica
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 24/05/2012
    ''' </remarks>
    Public Function CotizacionCronogramaGet(ByVal pECotizacion As String) As String Implements ICotizacionCronogramaNTx.CotizacionCronogramaGet
        'Variables
        Dim odtbCronograma As New DataTable
        Dim oECotizacion As New EGcc_cotizacion
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oECotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_NroCotizacion", DbType.String, 8, oECotizacion.Codigocotizacion, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_VersionCotizacion", DbType.String, 8, oECotizacion.Versioncotizacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CotizacionCronograma_get"
            objRequest.Parameters.AddRange(prmParameter)

            odtbCronograma = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "CotizacionCronogramaGet", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbCronograma)
    End Function

#End Region

End Class

#End Region
