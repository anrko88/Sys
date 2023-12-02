Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DSolicitudCreditoEstructuraDocTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - KCC
''' Fecha de Creación  : 04/06/2012
''' </remarks>
<Guid("3F6F9BAA-354A-4672-B855-43883FBB0F70") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DSolicitudCreditoEstructuraDocTx")> _
Public Class DSolicitudCreditoEstructuraDocTx
    Inherits ServicedComponent
    Implements ISolicitudCreditoEstructuraDocTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DSolicitudCreditoEstructuraDocTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta el cotizacioNDocumento para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEContratoEstructDoc">Entidad Serializado de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 04/06/2012
    ''' </remarks>
    Public Function InsertarContratoEstructDoc(ByVal pEContratoEstructDoc As String) As Boolean Implements ISolicitudCreditoEstructuraDocTx.InsertarContratoEstructDoc

        Dim oESolCredEstructDoc As New ESolicitudcreditoestructuradoc
        Dim prmParameter(41) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oESolCredEstructDoc = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuradoc)(pEContratoEstructDoc)

        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oESolCredEstructDoc.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 4, oESolCredEstructDoc.CodProveedor, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_TipoDocumento", DbType.String, 2, oESolCredEstructDoc.Tipodocumento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_NumeroSerieDocumento", DbType.String, 50, oESolCredEstructDoc.Numeroseriedocumento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 20, oESolCredEstructDoc.Nrodocumento, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pid_FechaVencimiento", DbType.DateTime, 10, oESolCredEstructDoc.FechaVencimiento, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_CodigoTipoAduana", DbType.String, 100, oESolCredEstructDoc.CodigoTipoAduana, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pii_AnioDUA", DbType.Int32, 4, oESolCredEstructDoc.AnioDUA, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_NroComprobanteDUA", DbType.String, 20, oESolCredEstructDoc.NroComprobanteDUA, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_MonedaOriginal", DbType.String, 3, oESolCredEstructDoc.Monedaoriginal, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_CodigoProcedencia", DbType.String, 100, oESolCredEstructDoc.Codigoprocedencia, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@pid_FechaEmision", DbType.DateTime, 10, oESolCredEstructDoc.Fechaemision, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pin_MontoGravado", DbType.Decimal, oESolCredEstructDoc.MontoGravado, ParameterDirection.Input)
        prmParameter(12).Scale = 6
        prmParameter(12).Precision = 18
        prmParameter(13) = New DAABRequest.Parameter("@pin_IGVOriginal", DbType.Decimal, oESolCredEstructDoc.Igvoriginal, ParameterDirection.Input)
        prmParameter(13).Scale = 6
        prmParameter(13).Precision = 18
        prmParameter(14) = New DAABRequest.Parameter("@pin_MontoIGV", DbType.Decimal, oESolCredEstructDoc.Montoigv, ParameterDirection.Input)
        prmParameter(14).Scale = 6
        prmParameter(14).Precision = 18
        prmParameter(15) = New DAABRequest.Parameter("@pin_MontoNoGravado", DbType.Decimal, oESolCredEstructDoc.MontoNoGravado, ParameterDirection.Input)
        prmParameter(15).Scale = 6
        prmParameter(15).Precision = 18
        prmParameter(16) = New DAABRequest.Parameter("@pin_Total", DbType.Decimal, oESolCredEstructDoc.Total, ParameterDirection.Input)
        prmParameter(16).Scale = 6
        prmParameter(16).Precision = 18
        prmParameter(17) = New DAABRequest.Parameter("@pii_FlagAdelantoProveedor", DbType.Int32, 4, oESolCredEstructDoc.FlagAdelantoProveedor, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@pin_MontoAdelantoProveedor", DbType.Decimal, oESolCredEstructDoc.MontoAdelantoProveedor, ParameterDirection.Input)
        prmParameter(18).Scale = 6
        prmParameter(18).Precision = 18
        prmParameter(19) = New DAABRequest.Parameter("@pin_MontoPendienteProveedor", DbType.Decimal, oESolCredEstructDoc.MontoPendienteProveedor, ParameterDirection.Input)
        prmParameter(19).Scale = 6
        prmParameter(19).Precision = 18
        prmParameter(20) = New DAABRequest.Parameter("@TCUtilizado", DbType.Decimal, oESolCredEstructDoc.Tcutilizado, ParameterDirection.Input)
        prmParameter(20).Scale = 6
        prmParameter(20).Precision = 15
        prmParameter(21) = New DAABRequest.Parameter("@pin_FlagTipoCambioEspecial", DbType.Int32, 4, oESolCredEstructDoc.FlagTipoCambioEspecial, ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@pin_TipoCambioEspecial", DbType.Decimal, oESolCredEstructDoc.Tipocambioespecial, ParameterDirection.Input)
        prmParameter(22).Scale = 6
        prmParameter(22).Precision = 18
        prmParameter(23) = New DAABRequest.Parameter("@pii_FlagTipoCambioSunat", DbType.Int32, 4, oESolCredEstructDoc.FlagTipoCambioSunat, ParameterDirection.Input)
        prmParameter(24) = New DAABRequest.Parameter("@pin_TCSBS", DbType.Decimal, oESolCredEstructDoc.Tcsbs, ParameterDirection.Input)
        prmParameter(24).Scale = 6
        prmParameter(24).Precision = 15
        prmParameter(25) = New DAABRequest.Parameter("@piv_IndiceDetraccion", DbType.String, 1, oESolCredEstructDoc.Indicedetraccion, ParameterDirection.Input)
        prmParameter(26) = New DAABRequest.Parameter("@piv_IndiceRetencion", DbType.String, 1, oESolCredEstructDoc.Indiceretencion, ParameterDirection.Input)
        prmParameter(27) = New DAABRequest.Parameter("@piv_CodigoTipoServicio", DbType.String, 100, oESolCredEstructDoc.CodigoTipoServicio, ParameterDirection.Input)
        prmParameter(28) = New DAABRequest.Parameter("@pin_ServicioPorc", DbType.Decimal, oESolCredEstructDoc.ServicioPorc, ParameterDirection.Input)
        prmParameter(28).Scale = 6
        prmParameter(28).Precision = 18
        prmParameter(29) = New DAABRequest.Parameter("@pin_MontoServicioSoles", DbType.Decimal, oESolCredEstructDoc.MontoServicioSoles, ParameterDirection.Input)
        prmParameter(29).Scale = 6
        prmParameter(29).Precision = 18
        prmParameter(30) = New DAABRequest.Parameter("@pin_MontoServicioDolar", DbType.Decimal, oESolCredEstructDoc.MontoServicioDolar, ParameterDirection.Input)
        prmParameter(30).Scale = 6
        prmParameter(30).Precision = 18
        prmParameter(31) = New DAABRequest.Parameter("@piv_NroConstancia", DbType.String, 50, oESolCredEstructDoc.NroConstancia, ParameterDirection.Input)
        prmParameter(32) = New DAABRequest.Parameter("@pid_FechaEmisionServicio", DbType.DateTime, 10, oESolCredEstructDoc.FechaEmisionServicio, ParameterDirection.Input)
        prmParameter(33) = New DAABRequest.Parameter("@piv_CodigoTipoComprobante", DbType.String, 100, oESolCredEstructDoc.CodigoTipoComprobante, ParameterDirection.Input)
        prmParameter(34) = New DAABRequest.Parameter("@piv_NumeroSerieDocumentoAdd", DbType.String, 50, oESolCredEstructDoc.NumeroSerieDocumentoAdd, ParameterDirection.Input)
        prmParameter(35) = New DAABRequest.Parameter("@piv_NroDocumentoAdd", DbType.String, 20, oESolCredEstructDoc.NroDocumentoAdd, ParameterDirection.Input)
        prmParameter(36) = New DAABRequest.Parameter("@pid_FechaEmisionAdd", DbType.DateTime, oESolCredEstructDoc.FechaEmisionAdd, ParameterDirection.Input)
        prmParameter(37) = New DAABRequest.Parameter("@pii_EstadoDocumento", DbType.Int32, 4, oESolCredEstructDoc.Estadodocumento, ParameterDirection.Input)

        prmParameter(38) = New DAABRequest.Parameter("@piv_CodigoSolicitudCredito", DbType.String, 8, oESolCredEstructDoc.CodSolicitudcreditoAdd, ParameterDirection.Input)

        prmParameter(39) = New DAABRequest.Parameter("@pin_Porc4ta", DbType.Decimal, oESolCredEstructDoc.Porc4ta, ParameterDirection.Input)
        prmParameter(39).Scale = 6
        prmParameter(39).Precision = 18
        prmParameter(40) = New DAABRequest.Parameter("@pin_Monto4taSoles", DbType.Decimal, oESolCredEstructDoc.Monto4taSoles, ParameterDirection.Input)
        prmParameter(40).Scale = 6
        prmParameter(40).Precision = 18
        prmParameter(41) = New DAABRequest.Parameter("@pin_Monto4taDolares", DbType.Decimal, oESolCredEstructDoc.Monto4taDolares, ParameterDirection.Input)
        prmParameter(41).Scale = 6
        prmParameter(41).Precision = 18

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_DesembolsoDocumento_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InsertarContratoEstructDoc", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Modifica el cotizacioNDocumento para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEContratoEstructDoc">Entidad Serializado de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 04/06/2012
    ''' </remarks>
    Public Function ModificarContratoEstructDoc(ByVal pEContratoEstructDoc As String) As Boolean Implements ISolicitudCreditoEstructuraDocTx.ModificarContratoEstructDoc

        Dim oESolCredEstructDoc As New ESolicitudcreditoestructuradoc
        Dim prmParameter(45) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oESolCredEstructDoc = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuradoc)(pEContratoEstructDoc)

        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oESolCredEstructDoc.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 4, oESolCredEstructDoc.CodProveedor, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_TipoDocumento", DbType.String, 2, oESolCredEstructDoc.Tipodocumento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_NumeroSerieDocumento", DbType.String, 50, oESolCredEstructDoc.Numeroseriedocumento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 26, oESolCredEstructDoc.Nrodocumento, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pid_FechaVencimiento", DbType.DateTime, 10, oESolCredEstructDoc.FechaVencimiento, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_CodigoTipoAduana", DbType.String, 100, oESolCredEstructDoc.CodigoTipoAduana, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pii_AnioDUA", DbType.Int32, 4, oESolCredEstructDoc.AnioDUA, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_NroComprobanteDUA", DbType.String, 20, oESolCredEstructDoc.NroComprobanteDUA, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_MonedaOriginal", DbType.String, 3, oESolCredEstructDoc.Monedaoriginal, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_CodigoProcedencia", DbType.String, 100, oESolCredEstructDoc.Codigoprocedencia, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@pid_FechaEmision", DbType.DateTime, 10, oESolCredEstructDoc.Fechaemision, ParameterDirection.Input)

        ' prmParameter(11) = New DAABRequest.Parameter("@pid_FechaEmision", DbType.String, 10, oESolCredEstructDoc.StringFechaEmision, ParameterDirection.Input)

        prmParameter(12) = New DAABRequest.Parameter("@pin_MontoGravado", DbType.Decimal, oESolCredEstructDoc.MontoGravado, ParameterDirection.Input)
        prmParameter(12).Scale = 6
        prmParameter(12).Precision = 18
        prmParameter(13) = New DAABRequest.Parameter("@pin_IGVOriginal", DbType.Decimal, oESolCredEstructDoc.Igvoriginal, ParameterDirection.Input)
        prmParameter(13).Scale = 6
        prmParameter(13).Precision = 18
        prmParameter(14) = New DAABRequest.Parameter("@pin_MontoIGV", DbType.Decimal, oESolCredEstructDoc.Montoigv, ParameterDirection.Input)
        prmParameter(14).Scale = 6
        prmParameter(14).Precision = 18
        prmParameter(15) = New DAABRequest.Parameter("@pin_MontoNoGravado", DbType.Decimal, oESolCredEstructDoc.MontoNoGravado, ParameterDirection.Input)
        prmParameter(15).Scale = 6
        prmParameter(15).Precision = 18
        prmParameter(16) = New DAABRequest.Parameter("@pin_Total", DbType.Decimal, oESolCredEstructDoc.Total, ParameterDirection.Input)
        prmParameter(16).Scale = 6
        prmParameter(16).Precision = 18
        prmParameter(17) = New DAABRequest.Parameter("@pii_FlagAdelantoProveedor", DbType.Int32, 4, oESolCredEstructDoc.FlagAdelantoProveedor, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@pin_MontoAdelantoProveedor", DbType.Decimal, oESolCredEstructDoc.MontoAdelantoProveedor, ParameterDirection.Input)
        prmParameter(18).Scale = 6
        prmParameter(18).Precision = 18
        prmParameter(19) = New DAABRequest.Parameter("@pin_MontoPendienteProveedor", DbType.Decimal, oESolCredEstructDoc.MontoPendienteProveedor, ParameterDirection.Input)
        prmParameter(19).Scale = 6
        prmParameter(19).Precision = 18
        prmParameter(20) = New DAABRequest.Parameter("@TCUtilizado", DbType.Decimal, oESolCredEstructDoc.Tcutilizado, ParameterDirection.Input)
        prmParameter(20).Scale = 6
        prmParameter(20).Precision = 18
        prmParameter(21) = New DAABRequest.Parameter("@pin_FlagTipoCambioEspecial", DbType.Int32, 4, oESolCredEstructDoc.FlagTipoCambioEspecial, ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@pin_TipoCambioEspecial", DbType.Decimal, oESolCredEstructDoc.Tipocambioespecial, ParameterDirection.Input)
        prmParameter(22).Scale = 6
        prmParameter(22).Precision = 18
        prmParameter(23) = New DAABRequest.Parameter("@pii_FlagTipoCambioSunat", DbType.Int32, 4, oESolCredEstructDoc.FlagTipoCambioSunat, ParameterDirection.Input)
        prmParameter(24) = New DAABRequest.Parameter("@pin_TCSBS", DbType.Decimal, oESolCredEstructDoc.Tcsbs, ParameterDirection.Input)
        prmParameter(24).Scale = 6
        prmParameter(24).Precision = 15
        prmParameter(25) = New DAABRequest.Parameter("@piv_IndiceDetraccion", DbType.String, 1, oESolCredEstructDoc.Indicedetraccion, ParameterDirection.Input)
        prmParameter(26) = New DAABRequest.Parameter("@piv_IndiceRetencion", DbType.String, 1, oESolCredEstructDoc.Indiceretencion, ParameterDirection.Input)
        prmParameter(27) = New DAABRequest.Parameter("@piv_CodigoTipoServicio", DbType.String, 100, oESolCredEstructDoc.CodigoTipoServicio, ParameterDirection.Input)
        prmParameter(28) = New DAABRequest.Parameter("@pin_ServicioPorc", DbType.Decimal, oESolCredEstructDoc.ServicioPorc, ParameterDirection.Input)
        prmParameter(28).Scale = 6
        prmParameter(28).Precision = 18
        prmParameter(29) = New DAABRequest.Parameter("@pin_MontoServicioSoles", DbType.Decimal, oESolCredEstructDoc.MontoServicioSoles, ParameterDirection.Input)
        prmParameter(29).Scale = 6
        prmParameter(29).Precision = 18
        prmParameter(30) = New DAABRequest.Parameter("@pin_MontoServicioDolar", DbType.Decimal, oESolCredEstructDoc.MontoServicioDolar, ParameterDirection.Input)
        prmParameter(30).Scale = 6
        prmParameter(30).Precision = 18
        prmParameter(31) = New DAABRequest.Parameter("@piv_NroConstancia", DbType.String, 50, oESolCredEstructDoc.NroConstancia, ParameterDirection.Input)
        prmParameter(32) = New DAABRequest.Parameter("@pid_FechaEmisionServicio", DbType.DateTime, 10, oESolCredEstructDoc.FechaEmisionServicio, ParameterDirection.Input)
        prmParameter(33) = New DAABRequest.Parameter("@piv_CodigoTipoComprobante", DbType.String, 100, oESolCredEstructDoc.CodigoTipoComprobante, ParameterDirection.Input)
        prmParameter(34) = New DAABRequest.Parameter("@piv_NumeroSerieDocumentoAdd", DbType.String, 50, oESolCredEstructDoc.NumeroSerieDocumentoAdd, ParameterDirection.Input)
        prmParameter(35) = New DAABRequest.Parameter("@piv_NroDocumentoAdd", DbType.String, 26, oESolCredEstructDoc.NroDocumentoAdd, ParameterDirection.Input)
        prmParameter(36) = New DAABRequest.Parameter("@pid_FechaEmisionAdd", DbType.DateTime, oESolCredEstructDoc.FechaEmisionAdd, ParameterDirection.Input)
        prmParameter(37) = New DAABRequest.Parameter("@pii_EstadoDocumento", DbType.Int32, 4, oESolCredEstructDoc.Estadodocumento, ParameterDirection.Input)

        prmParameter(38) = New DAABRequest.Parameter("@piv_KeyTipoComprobante", DbType.String, 2, oESolCredEstructDoc.KeyTipoComprobante, ParameterDirection.Input)
        prmParameter(39) = New DAABRequest.Parameter("@piv_KeyNumeroComprobante", DbType.String, 26, oESolCredEstructDoc.KeyNumeroComprobante, ParameterDirection.Input)
        prmParameter(40) = New DAABRequest.Parameter("@pid_KeyFechaEmision", DbType.String, 10, oESolCredEstructDoc.KeyFechaEmision, ParameterDirection.Input)
        prmParameter(41) = New DAABRequest.Parameter("@piv_KeyCodProveedor", DbType.String, 4, oESolCredEstructDoc.KeyCodProveedor, ParameterDirection.Input)

        prmParameter(42) = New DAABRequest.Parameter("@piv_CodigoSolicitudCredito", DbType.String, 8, oESolCredEstructDoc.CodSolicitudcreditoAdd, ParameterDirection.Input)

        prmParameter(43) = New DAABRequest.Parameter("@pin_Porc4ta", DbType.Decimal, oESolCredEstructDoc.Porc4ta, ParameterDirection.Input)
        prmParameter(43).Scale = 6
        prmParameter(43).Precision = 18
        prmParameter(44) = New DAABRequest.Parameter("@pin_Monto4taSoles", DbType.Decimal, oESolCredEstructDoc.Monto4taSoles, ParameterDirection.Input)
        prmParameter(44).Scale = 6
        prmParameter(44).Precision = 18
        prmParameter(45) = New DAABRequest.Parameter("@pin_Monto4taDolares", DbType.Decimal, oESolCredEstructDoc.Monto4taDolares, ParameterDirection.Input)
        prmParameter(45).Scale = 6
        prmParameter(45).Precision = 18

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_DesembolsoDocumento_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarContratoEstructDoc", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Eliminar de manera logica un contrato de estructura de documento
    ''' </summary>
    ''' <param name="pEContratoEstructDoc">Entidad Serializado de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 06/06/2012
    ''' </remarks>
    Public Function EliminarContratoEstructDoc(ByVal pEContratoEstructDoc As String) As Boolean Implements ISolicitudCreditoEstructuraDocTx.EliminarContratoEstructDoc

        Dim oESolCredEstructDoc As New ESolicitudcreditoestructuradoc
        Dim prmParameter(5) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oESolCredEstructDoc = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuradoc)(pEContratoEstructDoc)

        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oESolCredEstructDoc.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_TipoDocumento", DbType.String, 2, oESolCredEstructDoc.Tipodocumento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 20, oESolCredEstructDoc.Nrodocumento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pid_FechaEmision", DbType.DateTime, oESolCredEstructDoc.Fechaemision, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_CodigoProveedor", DbType.String, 4, oESolCredEstructDoc.CodProveedor, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pii_EstadoDocumento", DbType.Int32, 4, oESolCredEstructDoc.Estadodocumento, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_DesembolsoDocumento_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "EliminarContratoEstructDoc", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Actualizar un Grupo de Documentos 
    ''' </summary>
    ''' <param name="pXmlEContratoEstructDoc">Xml serializado</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 19/06/2012
    ''' </remarks>
    Public Function ActualizarGrupoContratoEstruct(ByVal pXmlEContratoEstructDoc As String, _
                                                   ByVal pstrNumeroIOWIO As String, _
                                                   ByVal pintNroSecuenciaWIO As Integer) As Boolean Implements ISolicitudCreditoEstructuraDocTx.ActualizarGrupoContratoEstruct

        Dim prmParameter(2) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@piv_XMLEntity", DbType.Xml, pXmlEContratoEstructDoc, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_NumeroIO", DbType.String, 18, pstrNumeroIOWIO, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pin_SecuenciaWIO", DbType.Int32, 4, pintNroSecuenciaWIO, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_XmlDesembolsoDocumento_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ActualizarGrupoContratoEstruct", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Inserta la relacion de la Bd de SGL y WIO 
    ''' </summary>
    ''' <param name="pstNroContrato">Numero de contrato</param>
    ''' <param name="pstrNumeroInstruccion">numero de instruccion creada</param>
    ''' <param name="pstrNroLinea">Numero de linea seleccionado</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 19/06/2012 
    ''' </remarks>
    Public Function InsertarContratoWIO(ByVal pstNroContrato As String, ByVal pstrNumeroInstruccion As String, ByVal pstrNroLinea As String) As Boolean Implements ISolicitudCreditoEstructuraDocTx.InsertarContratoWIO
        Dim prmParameter(2) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@piv_NroContrato", DbType.String, 8, pstNroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_NroInstruccion", DbType.String, 18, pstrNumeroInstruccion, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_NroLinea", DbType.String, 18, pstrNroLinea, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoWio_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InsertarContratoWIO", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True

    End Function

    ''' <summary>
    ''' ModificaEstadoDocumentoWS
    ''' </summary>
    ''' <param name="strEGcc_desembolso"></param>
    ''' Creado Por        : TSF - IJM
    ''' Fecha de Creacion : 03/09/2012 
    ''' <param name="pTipoAprobacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ModificaEstadoDocumentoWS(ByVal strEGcc_desembolso As String, ByVal pTipoAprobacion As String) As Boolean Implements ISolicitudCreditoEstructuraDocTx.ModificaEstadoDocumentoWS

        Dim oESolCredEstructDoc As New ESolicitudcreditoestructuradoc
        Dim prmParameter(3) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oESolCredEstructDoc = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuradoc)(strEGcc_desembolso)

        prmParameter(0) = New DAABRequest.Parameter("@codSolicitudCredito", DbType.String, 8, oESolCredEstructDoc.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@NroInstruccionWIO", DbType.String, 18, oESolCredEstructDoc.NroInstruccionWIO, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@NroSecuenciaWIO", DbType.Int32, 4, oESolCredEstructDoc.NroSecuenciaWIO, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@Accion", DbType.String, 1, pTipoAprobacion, ParameterDirection.Input)
        'prmParameter(4) = New DAABRequest.Parameter("@piv_CodigoProveedor", DbType.String, 4, oESolCredEstructDoc.CodProveedor, ParameterDirection.Input)
        'prmParameter(5) = New DAABRequest.Parameter("@pii_EstadoDocumento", DbType.Int32, 4, oESolCredEstructDoc.Estadodocumento, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoEstadoDocumentoWio_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificaEstadoDocumentoWS", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' ActualizarIGVDesembolso
    ''' </summary>
    ''' <param name="strNumContrato"></param>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 18/09/2012 
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ActualizarIGVDesembolso(ByVal strNumContrato As String) As Integer Implements ISolicitudCreditoEstructuraDocTx.ActualizarIGVDesembolso

        Dim oESolCredEstructDoc As New ESolicitudcreditoestructuradoc
        Dim prmParameter(1) As DAABRequest.Parameter
        Dim parResultado As IDataParameter

        prmParameter(0) = New DAABRequest.Parameter("@argNroCreditoLpc", DbType.String, 8, strNumContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@resultado", DbType.String, 18, oESolCredEstructDoc.NroInstruccionWIO, ParameterDirection.Output)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "UP_GCC_ActualizarDatosDesembolso_Sel"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ActualizarIGVDesembolso", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parResultado = CType(obRequest.Parameters(1), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckInt(parResultado.Value.ToString())
    End Function

#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DSolicitudCreditoEstructuraDocNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - KCC
''' Fecha de Creación  : 05/06/2012
''' </remarks>
<Guid("F0A2796D-1DBB-40c7-BC27-D9761EF69FC3") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DSolicitudCreditoEstructuraDocNTx")> _
Public Class DSolicitudCreditoEstructuraDocNTx
    Inherits ServicedComponent
    Implements ISolicitudCreditoEstructuraDocNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DSolicitudCreditoEstructuraDocNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Lista todos los estructura documentos de un contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 05/06/2012
    ''' </remarks>
    Public Function ListarContratoEstructDoc(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEContratoEstructDoc As String) As String Implements ISolicitudCreditoEstructuraDocNTx.ListarContratoEstructDoc
        Dim odtbListado As New DataTable
        Dim oEContratoEstructDoc As ESolicitudcreditoestructuradoc = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuradoc)(pEContratoEstructDoc)
        Dim prmParameter(4) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEContratoEstructDoc.Codsolicitudcredito, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DesembolsoDocumento_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarContratoEstructDoc", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    ''' <summary>
    ''' Lista todos los estructura documentos de un contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 07/03/2013
    ''' </remarks>
    Public Function ListarContratoEstructDocConsulta(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEContratoEstructDoc As String) As String Implements ISolicitudCreditoEstructuraDocNTx.ListarContratoEstructDocConsulta
        Dim odtbListado As New DataTable
        Dim oEContratoEstructDoc As ESolicitudcreditoestructuradoc = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuradoc)(pEContratoEstructDoc)
        Dim prmParameter(4) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEContratoEstructDoc.Codsolicitudcredito, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DesembolsoDocumentoConsultaCredito_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarContratoEstructDoc", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    ''' <summary>
    ''' Lista todos los estructura documentos de un contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 05/06/2012
    ''' </remarks>
    Public Function ListarContratoEstructDocAsociar(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEContratoEstructDoc As String) As String Implements ISolicitudCreditoEstructuraDocNTx.ListarContratoEstructDocAsociar
        Dim odtbListado As New DataTable
        Dim oEContratoEstructDoc As ESolicitudcreditoestructuradoc = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuradoc)(pEContratoEstructDoc)
        Dim prmParameter(6) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            'prmParameter(4) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEContratoEstructDoc.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_codunico", DbType.String, 10, oEContratoEstructDoc.CodUnico, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_TipoDocumento", DbType.String, 2, oEContratoEstructDoc.Tipodocumento, ParameterDirection.Input)
            'prmParameter(6) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 20, oEContratoEstructDoc.Nrodocumento, ParameterDirection.Input)
            'prmParameter(7) = New DAABRequest.Parameter("@pid_FechaEmision", DbType.DateTime, oEContratoEstructDoc.Fechaemision, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_CodigoProveedor", DbType.String, 4, oEContratoEstructDoc.CodProveedor, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DesembolsoDocumentoAsociar_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarContratoEstructDoc", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    ''' <summary>
    ''' Valida la duplicidad de un contrato de estructura de documento
    ''' </summary>
    ''' <param name="pEContratoEstructDoc">Entidad Serializado de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 09/08/2012
    ''' </remarks>
    Public Function ValidaContratoEstructDoc(ByVal pEContratoEstructDoc As String) As String Implements ISolicitudCreditoEstructuraDocNTx.ValidaContratoEstructDoc
        Dim odtbObtener As New DataTable
        Dim oESolCredEstructDoc As New ESolicitudcreditoestructuradoc
        Dim prmParameter(9) As DAABRequest.Parameter

        'Deserealiza la Entidad
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            oESolCredEstructDoc = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuradoc)(pEContratoEstructDoc)
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oESolCredEstructDoc.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_TipoDocumento", DbType.String, 2, oESolCredEstructDoc.Tipodocumento, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 20, oESolCredEstructDoc.Nrodocumento, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@pid_FechaEmision", DbType.DateTime, oESolCredEstructDoc.Fechaemision, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_CodigoProveedor", DbType.String, 4, oESolCredEstructDoc.CodProveedor, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_KeyTipoComprobante", DbType.String, 2, oESolCredEstructDoc.KeyTipoComprobante, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_KeyNumeroComprobante", DbType.String, 20, oESolCredEstructDoc.KeyNumeroComprobante, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@pid_KeyFechaEmision", DbType.DateTime, oESolCredEstructDoc.KeyFechaEmision, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@piv_KeyCodigoProveedor", DbType.String, 4, oESolCredEstructDoc.KeyCodProveedor, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@pii_EstadoDocumento", DbType.Int32, 4, oESolCredEstructDoc.Estadodocumento, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DesembolsoDocumento_val"
            objRequest.Parameters.AddRange(prmParameter)
            odtbObtener = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerContratoEstructDoc", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbObtener)
    End Function

    ''' <summary>
    ''' Obtiene el dato especifico de los estructura documentos de un contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 05/06/2012
    ''' </remarks>
    Public Function ObtenerContratoEstructDoc(ByVal pEContratoEstructDoc As String) As String Implements ISolicitudCreditoEstructuraDocNTx.ObtenerContratoEstructDoc
        Dim odtbObtener As New DataTable
        Dim prmParameter(4) As DAABRequest.Parameter

        Dim oEContratoEstructDoc As ESolicitudcreditoestructuradoc = CFunciones.DeserializeObject(Of ESolicitudcreditoestructuradoc)(pEContratoEstructDoc)
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEContratoEstructDoc.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_TipoDocumento", DbType.String, 2, oEContratoEstructDoc.Tipodocumento, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 20, oEContratoEstructDoc.Nrodocumento, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@pid_FechaEmision", DbType.DateTime, oEContratoEstructDoc.Fechaemision, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_CodigoProveedor", DbType.String, 4, oEContratoEstructDoc.CodProveedor, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DesembolsoDocumento_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbObtener = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerContratoEstructDoc", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbObtener)
    End Function

    ''' <summary>
    ''' Obtiene la Estructura para insertar en WIO los bienes
    ''' </summary>
    ''' <param name="pXmlEContratoEstructDoc"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 20/06/2012
    ''' </remarks>
    Public Function ObtenerBienLeasingWIO(ByVal pXmlEContratoEstructDoc As String) As String Implements ISolicitudCreditoEstructuraDocNTx.ObtenerBienLeasingWIO
        Dim odtbObtener As New DataTable
        Dim prmParameter(0) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@piv_XMLEntity", DbType.Xml, pXmlEContratoEstructDoc, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_BienLeasingWIO_sel"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            odtbObtener = obRequest.Factory.ExecuteDataset(obRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerBienLeasingWIO", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbObtener)
    End Function

    ''' <summary>
    ''' Obtiene si el Proveedor es Agente de retencion
    ''' </summary>
    ''' <param name="pstrNroDocumento"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 27/06/2012
    ''' </remarks>
    Public Function ObtenerAgenteRetencion(ByVal pstrNroDocumento As String) As Integer Implements ISolicitudCreditoEstructuraDocNTx.ObtenerAgenteRetencion
        Dim intAgente As Integer
        Dim prmParameter(0) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@piv_NRODOCUMENTO", DbType.String, 20, pstrNroDocumento, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_AgenteRetencion_get"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            intAgente = CInt(obRequest.Factory.ExecuteScalar(obRequest))

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerAgenteRetencion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try
        Return intAgente
    End Function

    'Inicio IBK
    ''' <summary>
    ''' Lista bienes del contrato
    ''' </summary>
    ''' <param name="pNroContrato"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - AAE
    ''' Fecha de Creacion : 24/09/2012
    ''' </remarks>
    Public Function ListaDocumentosContrato(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pNroContrato As String) As String Implements ISolicitudCreditoEstructuraDocNTx.ListaDocumentosContrato

        Dim odtbBienes As DataTable
        Dim prmParameter(4) As DAABRequest.Parameter
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@NroContrato", DbType.String, 8, pNroContrato, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DocumentosContrato_sel"
            objRequest.Parameters.AddRange(prmParameter)
            'Obtiene el ResulSet
            odtbBienes = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "BienesContrato", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbBienes)

    End Function
    'Fin IBK
#End Region

End Class

#End Region