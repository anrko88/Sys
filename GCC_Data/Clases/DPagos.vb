Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DPagosTx
''' </summary>
''' <remarks>
''' Creado Por         : IBK - RPR
''' Fecha de Creación  : 07/01/2013
''' </remarks>
<Guid("2BD91C4B-AD70-402c-BE13-47CDF2E75DF1") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DPagosTx")> _
Public Class DPagosTx
    Inherits ServicedComponent
    Implements IPagosTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DPagosTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Ingresar Pago de Cuotas
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 06/01/2013
    ''' </remarks>
    Public Function IngresarPagoCuotas(ByVal pECreditoRecuperacion As String) As String Implements IPagosTx.IngresarPagoCuotas
        'Variables
        Dim odtbRetorno As New DataTable
        Dim oECreditoRecuperacion As ECreditoRecuperacion = CFunciones.DeserializeObject(Of ECreditoRecuperacion)(pECreditoRecuperacion)
        Dim prmParameter(18) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@CodOperacionActiva", DbType.String, 8, oECreditoRecuperacion.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@TipoRubroFinanciamiento", DbType.String, 3, oECreditoRecuperacion.TipoRubroFinanciamiento, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@CodIfi", DbType.String, 4, oECreditoRecuperacion.CodIfi, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@TipoRecuperacion", DbType.String, 1, oECreditoRecuperacion.TipoRecuperacion, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@CodUsuario", DbType.String, 12, oECreditoRecuperacion.CodUsuario, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@FechaValorRecuperacion", DbType.String, 8, oECreditoRecuperacion.FechaValorRecuperacion.ToString("yyyyMMdd"), ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@TipoViaCobranza", DbType.String, 1, oECreditoRecuperacion.TipoViaCobranza, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@CodMonedaCargo", DbType.String, 3, oECreditoRecuperacion.CodMonedaCargo, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@TipoCuenta", DbType.String, 2, oECreditoRecuperacion.TipoCuenta, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@NroCuenta", DbType.String, 16, oECreditoRecuperacion.NroCuenta, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@EstadoRecuperacion", DbType.String, 1, oECreditoRecuperacion.EstadoRecuperacion, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@ConceptoAdministrativo", DbType.String, 3, oECreditoRecuperacion.ConceptoAdministrativo, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@CodigoMovimientoBasilea", DbType.String, 2, oECreditoRecuperacion.CodigoMovimientoBasilea, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@TipoExtorno", DbType.String, 1, oECreditoRecuperacion.TipoExtorno, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@TipoPrepago", DbType.String, 3, oECreditoRecuperacion.TipoPrepago, ParameterDirection.Input)
            prmParameter(15) = New DAABRequest.Parameter("@TipoAplicacionPrepago", DbType.String, 1, oECreditoRecuperacion.TipoAplicacionPrepago, ParameterDirection.Input)
            prmParameter(16) = New DAABRequest.Parameter("@TipoPrelacion", DbType.String, 1, oECreditoRecuperacion.TipoPrelacion, ParameterDirection.Input)
            prmParameter(17) = New DAABRequest.Parameter("@FlagCuentaPropia", DbType.String, 1, oECreditoRecuperacion.FlagCuentaPropia, ParameterDirection.Input)
            prmParameter(18) = New DAABRequest.Parameter("@CodUnicoClienteCargo", DbType.String, 10, oECreditoRecuperacion.CodUnicoClienteCargo, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_PagoCuotas_ins"
            objRequest.Parameters.AddRange(prmParameter)

            odtbRetorno = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "IngresarPagoCuotas", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbRetorno)
    End Function

    Public Function ProcesarLiquidacion(ByVal pECreditoRecuperacion As String) As String Implements IPagosTx.ProcesarLiquidacion

        'Variables
        Dim odtbDetalleLiquidacion As New DataSet
        Dim oECreditoRecuperacion As EGCC_Liquidacion = CFunciones.DeserializeObject(Of EGCC_Liquidacion)(pECreditoRecuperacion)
        Dim prmParameter(26) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoLiquidacion", DbType.String, 8, oECreditoRecuperacion.CodigoLiquidacion, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_CodOperacionActiva", DbType.String, 8, oECreditoRecuperacion.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@pic_TipoLiquidacion", DbType.String, 1, oECreditoRecuperacion.TipoLiquidacion, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@pic_FechaValor", DbType.String, 8, oECreditoRecuperacion.FechaValor.ToString("yyyyMMdd"), ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_FechaProceso", DbType.String, 8, oECreditoRecuperacion.FechaProceso.ToString("yyyyMMdd"), ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_CodTipoCambio", DbType.String, 3, oECreditoRecuperacion.CodTipoCambio, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pid_TipoCambio", DbType.Decimal, 0, oECreditoRecuperacion.TipoCambio, ParameterDirection.Input)

            prmParameter(7) = New DAABRequest.Parameter("@pid_PorcIGV", DbType.Decimal, 0, oECreditoRecuperacion.PorcIGV, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@pic_TipoCronograma", DbType.String, 3, oECreditoRecuperacion.TipoCronograma, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@pii_NroCuotas", DbType.Decimal, 0, oECreditoRecuperacion.NroCuotas, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@pic_Periodicidad", DbType.String, 3, oECreditoRecuperacion.Periodicidad, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@pic_FrecuenciaPago", DbType.String, 3, oECreditoRecuperacion.FrecuenciaPago, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@pii_PlazoGracia", DbType.Decimal, 0, oECreditoRecuperacion.PlazoGracia, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@pic_TipoGracia", DbType.String, 3, oECreditoRecuperacion.TipoGracia, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@pic_FechaPrimerVencimiento", DbType.String, 8, oECreditoRecuperacion.FechaPrimerVencimiento.ToString("yyyyMMdd"), ParameterDirection.Input)
            prmParameter(15) = New DAABRequest.Parameter("@pid_AmortizacionCapital", DbType.Decimal, 0, oECreditoRecuperacion.AmortizacionCapital, ParameterDirection.Input)

            prmParameter(16) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oECreditoRecuperacion.CodUsuario, ParameterDirection.Input)

            prmParameter(17) = New DAABRequest.Parameter("@pic_FlagAdenda", DbType.String, 1, oECreditoRecuperacion.FlagAdenda, ParameterDirection.Input)
            prmParameter(18) = New DAABRequest.Parameter("@pic_TipoViaCobranza", DbType.String, 1, oECreditoRecuperacion.TipoViaCobranza, ParameterDirection.Input)
            prmParameter(19) = New DAABRequest.Parameter("@pic_FlagCuentaPropia", DbType.String, 1, oECreditoRecuperacion.FlagCuentaPropia, ParameterDirection.Input)
            prmParameter(20) = New DAABRequest.Parameter("@pic_CodUnicoClienteCargo", DbType.String, 10, oECreditoRecuperacion.CodUnicoClienteCargo, ParameterDirection.Input)
            prmParameter(21) = New DAABRequest.Parameter("@pic_CodMonedaCargo", DbType.String, 3, oECreditoRecuperacion.CodMonedaCargo, ParameterDirection.Input)
            prmParameter(22) = New DAABRequest.Parameter("@pic_TipoCuenta", DbType.String, 2, oECreditoRecuperacion.TipoCuenta, ParameterDirection.Input)
            prmParameter(23) = New DAABRequest.Parameter("@pic_NroCuenta", DbType.String, 16, oECreditoRecuperacion.NroCuenta, ParameterDirection.Input)

            prmParameter(24) = New DAABRequest.Parameter("@pid_ValorNeto", DbType.Decimal, 0, oECreditoRecuperacion.ValorNeto, ParameterDirection.Input)
            prmParameter(25) = New DAABRequest.Parameter("@pid_MontoIGV", DbType.Decimal, 0, oECreditoRecuperacion.MontoIGV, ParameterDirection.Input)

            prmParameter(26) = New DAABRequest.Parameter("@FlagOperacion", DbType.String, 1, oECreditoRecuperacion.FlagOperacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Liquidacion_pro"
            objRequest.Parameters.AddRange(prmParameter)

            odtbDetalleLiquidacion = objRequest.Factory.ExecuteDataset(objRequest)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerDetalleCuotas", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataSet)(odtbDetalleLiquidacion)
    End Function

    ''' <summary>
    ''' Actualizar Estado de Pago de Cuotas
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 06/01/2013
    ''' </remarks>
    Public Function ActualizarEstadoPagoCuotas(ByVal pECreditoRecuperacion As String) As Boolean Implements IPagosTx.ActualizarEstadoPagoCuotas
        'Variables
        Dim oECreditoRecuperacion As ECreditoRecuperacion = CFunciones.DeserializeObject(Of ECreditoRecuperacion)(pECreditoRecuperacion)
        Dim prmParameter(6) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@CodOperacionActiva", DbType.String, 8, oECreditoRecuperacion.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@TipoRubroFinanciamiento", DbType.String, 3, oECreditoRecuperacion.TipoRubroFinanciamiento, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@CodIfi", DbType.String, 4, oECreditoRecuperacion.CodIfi, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@TipoRecuperacion", DbType.String, 1, oECreditoRecuperacion.TipoRecuperacion, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@NumSecRecuperacion", DbType.Int32, 0, oECreditoRecuperacion.NumSecRecuperacion, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@EstadoRecuperacion", DbType.String, 1, oECreditoRecuperacion.EstadoRecuperacion, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@MotivoAnulacionExtorno", DbType.String, 256, oECreditoRecuperacion.MotivoAnulacionExtorno, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_PagoCuotas_upd"
            objRequest.Parameters.AddRange(prmParameter)

            objRequest.Factory.ExecuteNonQuery(objRequest)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ActualizarEstadoPagoCuotas", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
            Return False
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return True

    End Function

    Public Function ActualizarEstadoLiquidacion(ByVal pECreditoRecuperacion As String) As Boolean Implements IPagosTx.ActualizarEstadoLiquidacion
        'Variables
        Dim oECreditoRecuperacion As EGCC_Liquidacion = CFunciones.DeserializeObject(Of EGCC_Liquidacion)(pECreditoRecuperacion)
        Dim prmParameter(3) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoLiquidacion", DbType.String, 8, oECreditoRecuperacion.CodigoLiquidacion, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_EstadoLiquidacion", DbType.String, 1, oECreditoRecuperacion.EstadoLiquidacion, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@pic_MotivoAnulacionExtorno", DbType.String, 256, oECreditoRecuperacion.MotivoAnulacionExtorno, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oECreditoRecuperacion.CodUsuario, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Liquidacion_upd"
            objRequest.Parameters.AddRange(prmParameter)

            objRequest.Factory.ExecuteNonQuery(objRequest)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ActualizarEstadoLiquidacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
            Return False
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return True

    End Function

    ''' <summary>
    ''' Inserta Concepto Detalle
    ''' </summary>
    ''' <param name="pEPagoConcepto">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - JJM
    ''' Fecha de Creacion : 09/01/2013
    ''' </remarks>
    Public Function InsertaConceptoDetalle(ByVal pEPagoConcepto As String) As Boolean Implements IPagosTx.InsertaConceptoDetalle
        'Variables

        Dim oEPagoConcepto As New EGcc_otroconcepto
        Dim prmParameter(7) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False
        'Deserealiza la Entidad
        oEPagoConcepto = CFunciones.DeserializeObject(Of EGcc_otroconcepto)(pEPagoConcepto)


        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oEPagoConcepto.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_CodTipoValor", DbType.String, 3, oEPagoConcepto.Codigootroconcepto2, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_DescripTipoValor", DbType.String, 30, oEPagoConcepto.Descripcionotroconcepto, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pid_Monto", DbType.Decimal)
        prmParameter(3).Precision = 20
        prmParameter(3).Scale = 6
        prmParameter(3).Value = oEPagoConcepto.Importe
        prmParameter(4) = New DAABRequest.Parameter("@pid_MontoIGV", DbType.Decimal)
        prmParameter(4).Precision = 20
        prmParameter(4).Scale = 6
        prmParameter(4).Value = oEPagoConcepto.Importeigv
        prmParameter(5) = New DAABRequest.Parameter("@piv_NumSecuencia", DbType.Int16, 4, oEPagoConcepto.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_NumSecuenciaAutorizacion", DbType.String, 20, oEPagoConcepto.NumSecuenciaAutorizacion, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_Usuario", DbType.String, 10, oEPagoConcepto.CodUsuario, ParameterDirection.Input)

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_TMPPagoConceptoDetalle_Ins"
        objRequest.Parameters.AddRange(prmParameter)

        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarTemporal", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function
    ''' <summary>
    ''' Actualiza Concepto Detalle
    ''' </summary>
    ''' <param name="pEPagoConcepto">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - JJM
    ''' Fecha de Creacion : 09/01/2013
    ''' </remarks>
    Public Function ActualizaConceptoDetalle(ByVal pEPagoConcepto As String) As Boolean Implements IPagosTx.ActualizaConceptoDetalle
        'Variables

        Dim oEPagoConcepto As New EGcc_otroconcepto
        Dim prmParameter(7) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False
        'Deserealiza la Entidad
        oEPagoConcepto = CFunciones.DeserializeObject(Of EGcc_otroconcepto)(pEPagoConcepto)


        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oEPagoConcepto.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_CodTipoValor", DbType.String, 3, oEPagoConcepto.Codigootroconcepto2, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_DescripTipoValor", DbType.String, 30, oEPagoConcepto.Descripcionotroconcepto, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pid_Monto", DbType.Decimal)
        prmParameter(3).Precision = 20
        prmParameter(3).Scale = 6
        prmParameter(3).Value = oEPagoConcepto.Importe
        prmParameter(4) = New DAABRequest.Parameter("@pid_MontoIGV", DbType.Decimal)
        prmParameter(4).Precision = 20
        prmParameter(4).Scale = 6
        prmParameter(4).Value = oEPagoConcepto.Importeigv
        prmParameter(5) = New DAABRequest.Parameter("@piv_NumSecuencia", DbType.Int16, 4, oEPagoConcepto.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_NumSecuenciaAutorizacion", DbType.String, 20, oEPagoConcepto.NumSecuenciaAutorizacion, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_Usuario", DbType.String, 10, oEPagoConcepto.CodUsuario, ParameterDirection.Input)

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_TMPPagoConceptoDetalle_Upd"
        objRequest.Parameters.AddRange(prmParameter)

        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarTemporal", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function
    ''' <summary>
    ''' Elimina Concepto Detalle
    ''' </summary>
    ''' <param name="pEPagoConcepto">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - JJM
    ''' Fecha de Creacion : 09/01/2013
    ''' </remarks>
    Public Function EliminaConceptoDetalle(ByVal pEPagoConcepto As String) As Boolean Implements IPagosTx.EliminaConceptoDetalle
        'Variables

        Dim oEPagoConcepto As New EGcc_otroconcepto
        Dim prmParameter(0) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False
        'Deserealiza la Entidad
        oEPagoConcepto = CFunciones.DeserializeObject(Of EGcc_otroconcepto)(pEPagoConcepto)


        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oEPagoConcepto.Codsolicitudcredito, ParameterDirection.Input)


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_TMPPagoConceptoDetalle_Del"
        objRequest.Parameters.AddRange(prmParameter)

        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarTemporal", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function
    ''' <summary>
    ''' Ingresar Pago de Conceptos
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - JJM
    ''' Fecha de Creacion : 21/01/2013
    ''' </remarks>
    Public Function IngresarPagoConceptos(ByVal pECreditoRecuperacion As String) As String Implements IPagosTx.IngresarPagoConceptos
        'Variables
        Dim odtbRetorno As New DataTable
        Dim oECreditoRecuperacion As ECreditoRecuperacion = CFunciones.DeserializeObject(Of ECreditoRecuperacion)(pECreditoRecuperacion)
        Dim prmParameter(26) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@CodOperacionActiva", DbType.String, 8, oECreditoRecuperacion.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@TipoRubroFinanciamiento", DbType.String, 3, oECreditoRecuperacion.TipoRubroFinanciamiento, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@CodIfi", DbType.String, 4, oECreditoRecuperacion.CodIfi, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@TipoRecuperacion", DbType.String, 1, oECreditoRecuperacion.TipoRecuperacion, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@CodUsuario", DbType.String, 12, oECreditoRecuperacion.CodUsuario, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@FechaValorRecuperacion", DbType.String, 8, oECreditoRecuperacion.FechaValorRecuperacion.ToString("yyyyMMdd"), ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@TipoViaCobranza", DbType.String, 1, oECreditoRecuperacion.TipoViaCobranza, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@CodMonedaCargo", DbType.String, 3, oECreditoRecuperacion.CodMonedaCargo, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@TipoCuenta", DbType.String, 2, oECreditoRecuperacion.TipoCuenta, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@NroCuenta", DbType.String, 16, oECreditoRecuperacion.NroCuenta, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@EstadoRecuperacion", DbType.String, 1, oECreditoRecuperacion.EstadoRecuperacion, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@ConceptoAdministrativo", DbType.String, 3, oECreditoRecuperacion.ConceptoAdministrativo, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@CodigoMovimientoBasilea", DbType.String, 2, oECreditoRecuperacion.CodigoMovimientoBasilea, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@TipoExtorno", DbType.String, 1, oECreditoRecuperacion.TipoExtorno, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@TipoPrepago", DbType.String, 3, oECreditoRecuperacion.TipoPrepago, ParameterDirection.Input)
            prmParameter(15) = New DAABRequest.Parameter("@TipoAplicacionPrepago", DbType.String, 1, oECreditoRecuperacion.TipoAplicacionPrepago, ParameterDirection.Input)
            prmParameter(16) = New DAABRequest.Parameter("@TipoPrelacion", DbType.String, 1, oECreditoRecuperacion.TipoPrelacion, ParameterDirection.Input)
            prmParameter(17) = New DAABRequest.Parameter("@FlagCuentaPropia", DbType.String, 1, oECreditoRecuperacion.FlagCuentaPropia, ParameterDirection.Input)
            prmParameter(18) = New DAABRequest.Parameter("@CodUnicoClienteCargo", DbType.String, 10, oECreditoRecuperacion.CodUnicoClienteCargo, ParameterDirection.Input)

            'Adicionales con respecto a pago de cuotas
            prmParameter(19) = New DAABRequest.Parameter("@CodComisionTipo", DbType.String, 3, oECreditoRecuperacion.CodigoConcepto, ParameterDirection.Input)
            prmParameter(20) = New DAABRequest.Parameter("@CodMoneda", DbType.String, 3, oECreditoRecuperacion.CodMoneda, ParameterDirection.Input)
            prmParameter(21) = New DAABRequest.Parameter("@MontoReembolso", DbType.Decimal, 0, oECreditoRecuperacion.MontoReembolso, ParameterDirection.Input)
            prmParameter(22) = New DAABRequest.Parameter("@MontoIGVReembolso", DbType.Decimal, 0, oECreditoRecuperacion.MontoIGVReembolso, ParameterDirection.Input)
            prmParameter(23) = New DAABRequest.Parameter("@MontoComision", DbType.Decimal, 0, oECreditoRecuperacion.MontoComision, ParameterDirection.Input)
            prmParameter(24) = New DAABRequest.Parameter("@MontoIGV", DbType.Decimal, 0, oECreditoRecuperacion.MontoIGV, ParameterDirection.Input)
            prmParameter(25) = New DAABRequest.Parameter("@NumSecRecuperacion", DbType.Decimal, 0, oECreditoRecuperacion.NumSecRecuperacion, ParameterDirection.Input)

            prmParameter(26) = New DAABRequest.Parameter("@GlosaConceptoComprobante", DbType.String, 1024, oECreditoRecuperacion.GlosaConceptoComprobante, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_PagoConceptos_ins"
            objRequest.Parameters.AddRange(prmParameter)

            odtbRetorno = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "IngresarPagoConceptos", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbRetorno)
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pEPagoConcepto">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - JJM
    ''' Fecha de Creacion : 02/04/2013
    ''' </remarks>
    Public Function ActualizarTMPLiquidacion(ByVal pEGCC_Liquidacion As String) As String Implements IPagosTx.ActualizarTMPLiquidacion
        'Variables

        Dim oEGCC_Liquidacion As New EGCC_Liquidacion
        Dim prmParameter(6) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As String = "1"
        'Deserealiza la Entidad
        oEGCC_Liquidacion = CFunciones.DeserializeObject(Of EGCC_Liquidacion)(pEGCC_Liquidacion)


        prmParameter(0) = New DAABRequest.Parameter("@pic_ValorNeto", DbType.Decimal)
        prmParameter(0).Precision = 20
        prmParameter(0).Scale = 5
        prmParameter(0).Value = oEGCC_Liquidacion.ValorNeto

        prmParameter(1) = New DAABRequest.Parameter("@pic_ValorIGV", DbType.Decimal)
        prmParameter(1).Precision = 20
        prmParameter(1).Scale = 5
        prmParameter(1).Value = oEGCC_Liquidacion.MontoIGV

        prmParameter(2) = New DAABRequest.Parameter("@pic_CodOperacionActiva", DbType.String, 8, oEGCC_Liquidacion.CodOperacionActiva, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_NumSecRecuperacion", DbType.Int32, 4, oEGCC_Liquidacion.NumSecRecuperacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_TipoRecuperacion", DbType.String, 1, oEGCC_Liquidacion.TipoRecuperacion, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oEGCC_Liquidacion.CodUsuario, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_Aplicacion", DbType.Int32, 4, oEGCC_Liquidacion.Aplicacion, ParameterDirection.Input)


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_TMPLiquidacion_upd"
        objRequest.Parameters.AddRange(prmParameter)

        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarTemporal", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pEPagoConcepto">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - JJM
    ''' Fecha de Creacion : 02/04/2013
    ''' </remarks>
    Public Function ActualizarTMPLiquidacionAplicacion(ByVal pEGCC_Liquidacion As String) As String Implements IPagosTx.ActualizarTMPLiquidacionAplicacion
        'Variables

        Dim oEGCC_Liquidacion As New EGCC_Liquidacion
        Dim prmParameter(4) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As String = "1"
        'Deserealiza la Entidad
        oEGCC_Liquidacion = CFunciones.DeserializeObject(Of EGCC_Liquidacion)(pEGCC_Liquidacion)

        prmParameter(0) = New DAABRequest.Parameter("@pic_CodOperacionActiva", DbType.String, 8, oEGCC_Liquidacion.CodOperacionActiva, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_NumSecRecuperacion", DbType.Int32, 4, oEGCC_Liquidacion.NumSecRecuperacion, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_TipoRecuperacion", DbType.String, 1, oEGCC_Liquidacion.TipoRecuperacion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oEGCC_Liquidacion.CodUsuario, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_Aplicacion", DbType.Int32, 4, oEGCC_Liquidacion.Aplicacion, ParameterDirection.Input)


        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_TMPLiquidacionAplicacion_upd"
        objRequest.Parameters.AddRange(prmParameter)

        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarTemporal", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    Public Function TMPInsertarCronograma(ByVal pECotizacioncronograma As String) As Boolean Implements IPagosTx.TMPInsertarCronograma

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
        obRequest.Command = "up_gcc_TMPLiquidacionCalendarioNuevoGCC_ins"
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

#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DPagosNTx
''' </summary>
''' <remarks>
''' Creado Por         : IBK - RPR
''' Fecha de Creación  : 26/12/2012
''' </remarks>
<Guid("58AA83EB-38C5-458f-A423-A6F018687C7A") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DPagosNTx")> _
Public Class DPagosNTx
    Inherits ServicedComponent
    Implements IPagosNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DPagosNTx"
#End Region

#Region "Metodos"

    Public Function SeleccionarAplicacionComision(ByVal pECreditoRecuperacionComision As String) As Boolean Implements IPagosNTx.SeleccionarAplicacionComision

        'Variables
        Dim oECreditoRecuperacionComision As ECreditoRecuperacionComision = CFunciones.DeserializeObject(Of ECreditoRecuperacionComision)(pECreditoRecuperacionComision)
        Dim prmParameter(6) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodOperacionActiva", DbType.String, 8, oECreditoRecuperacionComision.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_CodigoLiquidacion", DbType.String, 8, oECreditoRecuperacionComision.CodigoLiquidacion, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@pic_TipoRecuperacion", DbType.String, 1, oECreditoRecuperacionComision.TipoRecuperacion, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@pii_NumCuotaCalendario", DbType.Int32, 0, oECreditoRecuperacionComision.NumCuotaCalendario, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pii_NumSecRecupComi", DbType.Int32, 0, oECreditoRecuperacionComision.NumSecRecupComi, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pii_Aplicacion", DbType.Int32, 0, oECreditoRecuperacionComision.Aplicacion, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oECreditoRecuperacionComision.CodUsuario, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_SeleccionarAplicacionComision_upd"
            objRequest.Parameters.AddRange(prmParameter)

            objRequest.Factory.ExecuteDataset(objRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerDetalleCuotas", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
            Return False
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return True

    End Function

    ''' <summary>
    ''' Listado General de Pago de Cuotas
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la pagina</param>
    ''' <param name="pCurrentPage">Pagina Actual</param>
    ''' <param name="pSortColumn">Columna a Ordenar</param>
    ''' <param name="pSortOrder">Tipo de Ordenamiento</param>
    ''' <param name="pEPagoCuotas">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 26/12/2012
    ''' </remarks>
    Public Function ListadoPagoCuotas(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pEPagoCuotas As String) As String Implements IPagosNTx.ListadoPagoCuotas
        'Variables
        Dim odtbListado As New DataTable
        Dim oEPagoCuotas As New EGCC_PagoCuotas
        Dim prmParameter(12) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEPagoCuotas = CFunciones.DeserializeObject(Of EGCC_PagoCuotas)(pEPagoCuotas)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEPagoCuotas.CodSolicitudCredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 10, oEPagoCuotas.CUCliente, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_NombreCliente", DbType.String, 256, oEPagoCuotas.RazonSocial, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_FechaPagoInicio", DbType.String, 8, oEPagoCuotas.FechaPagoInicio, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@piv_FechaPagoFin", DbType.String, 8, oEPagoCuotas.FechaPagoFin, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@piv_TipoContrato", DbType.String, 8, oEPagoCuotas.TipoContrato, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@piv_CodigoEstado", DbType.String, 8, oEPagoCuotas.CodigoEstado, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@piv_NroAutorizacion", DbType.String, 8, oEPagoCuotas.NroAutorizacion, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@piv_CodigoMoneda", DbType.String, 8, oEPagoCuotas.CodigoMoneda, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_PagoCuotas_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoPagoCuotas", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    Public Function ListadoLiquidaciones(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pEGCC_Liquidacion As String) As String Implements IPagosNTx.ListadoLiquidaciones
        'Variables
        Dim odtbListado As New DataTable
        Dim oEGCC_Liquidacion As New EGCC_Liquidacion
        Dim prmParameter(13) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEGCC_Liquidacion = CFunciones.DeserializeObject(Of EGCC_Liquidacion)(pEGCC_Liquidacion)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_CodOperacionActiva", DbType.String, 8, oEGCC_Liquidacion.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_CodigoLiquidacion", DbType.String, 8, oEGCC_Liquidacion.CodigoLiquidacion, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 10, oEGCC_Liquidacion.CUCliente, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_NombreCliente", DbType.String, 256, oEGCC_Liquidacion.RazonSocial, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@piv_FechaValorInicio", DbType.String, 8, oEGCC_Liquidacion.FechaValorInicio, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@piv_FechaValorFin", DbType.String, 8, oEGCC_Liquidacion.FechaValorFin, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@piv_TipoLiquidacion", DbType.String, 8, oEGCC_Liquidacion.TipoLiquidacion, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@piv_EstadoLiquidacion", DbType.String, 8, oEGCC_Liquidacion.EstadoLiquidacion, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@piv_CodMoneda", DbType.String, 8, oEGCC_Liquidacion.CodMoneda, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@piv_FlagAdendaContrato", DbType.String, 1, oEGCC_Liquidacion.FlagAdenda, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Liquidacion_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoLiquidaciones", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    ''' <summary>
    ''' Obtiene Pago de Cuotas
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 26/12/2012
    ''' </remarks>
    Public Function ObtenerPagoCuotas(ByVal pECreditoRecuperacion As String) As String Implements IPagosNTx.ObtenerPagoCuotas
        'Variables
        Dim odtbListado As New DataTable
        Dim oECreditoRecuperacion As ECreditoRecuperacion = CFunciones.DeserializeObject(Of ECreditoRecuperacion)(pECreditoRecuperacion)
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oECreditoRecuperacion.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_NumSecRecuperacion", DbType.Int32, 0, oECreditoRecuperacion.NumSecRecuperacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_PagoCuotas_get"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerPagoCuotas", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    Public Function ObtenerLiquidacion(ByVal pEGCC_Liquidacion As String) As String Implements IPagosNTx.ObtenerLiquidacion
        'Variables
        Dim odtbListado As New DataTable
        Dim oEGCC_Liquidacion As EGCC_Liquidacion = CFunciones.DeserializeObject(Of EGCC_Liquidacion)(pEGCC_Liquidacion)
        Dim prmParameter(0) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoLiquidacion", DbType.String, 8, oEGCC_Liquidacion.CodigoLiquidacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Liquidacion_get"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerLiquidacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    Public Function ObtenerCuotaAtrasadaComision(ByVal pEGCC_Liquidacion As String) As String Implements IPagosNTx.ObtenerCuotaAtrasadaComision
        'Variables
        Dim odtbListado As New DataTable
        Dim oEGCC_Liquidacion As EGCC_Liquidacion = CFunciones.DeserializeObject(Of EGCC_Liquidacion)(pEGCC_Liquidacion)
        Dim prmParameter(3) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodOperacionActiva", DbType.String, 8, oEGCC_Liquidacion.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_CodigoLiquidacion", DbType.String, 8, oEGCC_Liquidacion.CodigoLiquidacion, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@pii_NumCuotaCalendario", DbType.Decimal, 0, oEGCC_Liquidacion.NumCuotaCalendario, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oEGCC_Liquidacion.CodUsuario, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_LiquidacionCuotaAtrasadaComision_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerLiquidacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    ''' <summary>
    ''' Obtiene Totales del Pago de Cuotas
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 29/12/2012
    ''' </remarks>
    Public Function ObtenerPagoCuotasTotales(ByVal pECreditoRecuperacion As String) As String Implements IPagosNTx.ObtenerPagoCuotasTotales

        'Variables
        Dim dtTotalesPagoCuotas As New DataTable
        Dim oECreditoRecuperacion As ECreditoRecuperacion = CFunciones.DeserializeObject(Of ECreditoRecuperacion)(pECreditoRecuperacion)
        Dim prmParameter(3) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodOperacionActiva", DbType.String, 8, oECreditoRecuperacion.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_TipoRecuperacion", DbType.String, 1, oECreditoRecuperacion.TipoRecuperacion, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oECreditoRecuperacion.CodUsuario, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@pic_FechaValor", DbType.String, 8, oECreditoRecuperacion.FechaValorRecuperacion.ToString("yyyyMMdd"), ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_PagoCuotasTotales_sel"
            objRequest.Parameters.AddRange(prmParameter)

            dtTotalesPagoCuotas = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerPagoCuotasTotales", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(dtTotalesPagoCuotas)

    End Function

    ''' <summary>
    ''' Obtener el detalle de cuotas pagadas en un CreditoRecuperacion
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 27/12/2012
    ''' </remarks>
    Public Function ObtenerDetalleCuotas(ByVal pECreditoRecuperacion As String) As String Implements IPagosNTx.ObtenerDetalleCuotas

        'Variables
        Dim odtbDetalleCuotas As New DataTable
        Dim oECreditoRecuperacion As ECreditoRecuperacion = CFunciones.DeserializeObject(Of ECreditoRecuperacion)(pECreditoRecuperacion)
        Dim prmParameter(4) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodOperacionActiva", DbType.String, 8, oECreditoRecuperacion.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_TipoRubroFinanciamiento", DbType.String, 3, oECreditoRecuperacion.TipoRubroFinanciamiento, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@pic_CodIfi", DbType.String, 4, oECreditoRecuperacion.CodIfi, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@pic_TipoRecuperacion", DbType.String, 1, oECreditoRecuperacion.TipoRecuperacion, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pii_NumSecRecuperacion", DbType.Int32, 0, oECreditoRecuperacion.NumSecRecuperacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DetalleRecupCuotas_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbDetalleCuotas = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerDetalleCuotas", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbDetalleCuotas)
    End Function

    ''' <summary>
    ''' Obtener las siguientes n cuotas a pagar en un credito
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado para la busqueda</param>
    ''' <param name="pNroCuotas">Numero de Cuotas a obtener</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 31/12/2012
    ''' </remarks>
    Public Function ObtenerProximasCuotas(ByVal pECreditoRecuperacion As String, ByVal pNroCuotas As Integer) As String Implements IPagosNTx.ObtenerProximasCuotas

        'Variables
        Dim odtbDetalleCuotas As New DataTable
        Dim oECreditoRecuperacion As ECreditoRecuperacion = CFunciones.DeserializeObject(Of ECreditoRecuperacion)(pECreditoRecuperacion)
        Dim prmParameter(5) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodOperacionActiva", DbType.String, 8, oECreditoRecuperacion.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_TipoRubroFinanciamiento", DbType.String, 3, oECreditoRecuperacion.TipoRubroFinanciamiento, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@pic_CodIfi", DbType.String, 4, oECreditoRecuperacion.CodIfi, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@pic_FechaValor", DbType.String, 8, oECreditoRecuperacion.FechaValorRecuperacion.ToString("yyyyMMdd"), ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pii_NroCuotas", DbType.Int32, 0, pNroCuotas, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oECreditoRecuperacion.CodUsuario, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DetalleRecupCuotas_pro"
            objRequest.Parameters.AddRange(prmParameter)

            odtbDetalleCuotas = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerProximasCuotas", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbDetalleCuotas)
    End Function

    ''' <summary>
    ''' Obtener el detalle de comisiones pagadas en un CreditoRecuperacion
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 27/12/2012
    ''' </remarks>
    Public Function ObtenerDetalleComisiones(ByVal pECreditoRecuperacion As String) As String Implements IPagosNTx.ObtenerDetalleComisiones

        'Variables
        Dim odtbDetalleComisiones As New DataTable
        Dim oECreditoRecuperacion As New ECreditoRecuperacion
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oECreditoRecuperacion = CFunciones.DeserializeObject(Of ECreditoRecuperacion)(pECreditoRecuperacion)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodOperacionActiva", DbType.String, 8, oECreditoRecuperacion.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_NumSecRecuperacion", DbType.Int32, 0, oECreditoRecuperacion.NumSecRecuperacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_TarifarioPagos_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbDetalleComisiones = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerDetalleComisiones", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbDetalleComisiones)
    End Function

    ''' <summary>
    ''' Calcular las comisiones a cobrar por las proximas n cuotas seleccionadas
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 03/01/2013
    ''' </remarks>
    Public Function ObtenerProximasComisiones(ByVal pECreditoRecuperacion As String) As String Implements IPagosNTx.ObtenerProximasComisiones

        'Variables
        Dim odtbDetalleComisiones As New DataTable
        Dim oECreditoRecuperacion As New ECreditoRecuperacion
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oECreditoRecuperacion = CFunciones.DeserializeObject(Of ECreditoRecuperacion)(pECreditoRecuperacion)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodOperacionActiva", DbType.String, 8, oECreditoRecuperacion.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oECreditoRecuperacion.CodUsuario, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_TarifarioPagos_Pro"
            objRequest.Parameters.AddRange(prmParameter)

            odtbDetalleComisiones = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerProximasComisiones", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbDetalleComisiones)
    End Function

    ''' <summary>
    ''' Obtiene trama a enviar a Transactor para autorizar el pago de cuotas o conceptos en ventanilla con Nro de Autorizacion
    ''' </summary>
    ''' <param name="pECreditoRecuperacion">Entidad Serializado para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : IBK - RPR
    ''' Fecha de Creacion : 09/01/2013
    ''' </remarks>
    Public Function ObtenerTramaAutorizacionPagosVentanilla(ByVal pECreditoRecuperacion As String, ByVal STATUS As String) As String Implements IPagosNTx.ObtenerTramaAutorizacionPagosVentanilla

        'Variables
        Dim odtbTrama As New DataTable
        Dim oECreditoRecuperacion As New ECreditoRecuperacion
        Dim prmParameter(2) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oECreditoRecuperacion = CFunciones.DeserializeObject(Of ECreditoRecuperacion)(pECreditoRecuperacion)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodOperacionActiva", DbType.String, 8, oECreditoRecuperacion.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CodAutorizacion", DbType.Int32, 0, oECreditoRecuperacion.CodAutorizacionRecuperacion, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@pic_Estado", DbType.String, 1, STATUS, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_PagosTramaAutorizacionTransactor_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbTrama = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerTramaAutorizacionPagosVentanilla", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return odtbTrama.Rows(0).Item("Trama").ToString
    End Function

    'IBK JJM 
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pEPagoConcepto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListadoPagoConcepto(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pEPagoConcepto As String) As String Implements IPagosNTx.ListadoPagoConcepto
        'Variables
        Dim odtbListado As New DataTable
        Dim oEPagoConcepto As New EGcc_otroconcepto
        Dim prmParameter(12) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEPagoConcepto = CFunciones.DeserializeObject(Of EGcc_otroconcepto)(pEPagoConcepto)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEPagoConcepto.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 10, oEPagoConcepto.CodUnico, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_NombreCliente", DbType.String, 256, oEPagoConcepto.RazonSocial, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_FechaPagoInicio", DbType.String, 8, oEPagoConcepto.FechaRegistroInicio, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@piv_FechaPagoFin", DbType.String, 8, oEPagoConcepto.FechaRegistroFin, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@piv_TipoContrato", DbType.String, 8, oEPagoConcepto.TipoContrato, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@piv_CodigoEstado", DbType.String, 8, oEPagoConcepto.Codigoestadopago, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@piv_NroAutorizacion", DbType.String, 8, oEPagoConcepto.NumSecuenciaAutorizacion, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@piv_CodigoMoneda", DbType.String, 8, oEPagoConcepto.Codigomoneda, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_RECUPPAGOSCONCEPPAGINADO_SEL"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoPagoCuotas", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function
    Public Function ListadoPagoConceptoDetalle(ByVal pEPagoConcepto As String) As String Implements IPagosNTx.ListadoPagoConceptoDetalle
        'Variables 
        Dim odtbListado As New DataTable
        Dim oEPagoConcepto As New EGcc_otroconcepto
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEPagoConcepto = CFunciones.DeserializeObject(Of EGcc_otroconcepto)(pEPagoConcepto)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEPagoConcepto.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_NumSecRecuperacion", DbType.Int16, 0, oEPagoConcepto.Secfinanciamiento, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_PagoConceptos_get"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoPagoConcepto", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function
    Public Function ListadoPagoConceptoxNumeroSecuencia(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pEPagoConcepto As String) As String Implements IPagosNTx.ListadoPagoConceptoxNumeroSecuencia
        'Variables
        Dim odtbListado As New DataTable
        Dim oEPagoConcepto As New EGcc_otroconcepto
        Dim prmParameter(5) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEPagoConcepto = CFunciones.DeserializeObject(Of EGcc_otroconcepto)(pEPagoConcepto)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEPagoConcepto.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pii_NumSecRecuperacion", DbType.Int16, 0, oEPagoConcepto.Secfinanciamiento, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_PagoConceptosDetallexNumeroSecuencia_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoPagoConceptos", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function
    Public Function ObtenerConceptoEspecifico(ByVal pEPagoConcepto As String) As String Implements IPagosNTx.ObtenerConceptoEspecifico
        'Variables
        Dim odtbListado As New DataTable
        Dim oEPagoConcepto As New EGcc_otroconcepto
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEPagoConcepto = CFunciones.DeserializeObject(Of EGcc_otroconcepto)(pEPagoConcepto)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oEPagoConcepto.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_NumSecRecuperacion", DbType.Int16, 0, oEPagoConcepto.Secfinanciamiento, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ListaConceptoEspecifico_get"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoPagoConcepto", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function
    Public Function ListadoPagoConceptoxNumeroSecuenciaTemporal(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pEPagoConcepto As String) As String Implements IPagosNTx.ListadoPagoConceptoxNumeroSecuenciaTemporal
        'Variables
        Dim odtbListado As New DataTable
        Dim oEPagoConcepto As New EGcc_otroconcepto
        Dim prmParameter(6) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEPagoConcepto = CFunciones.DeserializeObject(Of EGcc_otroconcepto)(pEPagoConcepto)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEPagoConcepto.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_NumSecuencia", DbType.Int16, 4, oEPagoConcepto.Secfinanciamiento, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_NumSecuenciaAutorizacion", DbType.String, 20, oEPagoConcepto.NumSecuenciaAutorizacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_PagoConceptosDetalleTemporal_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoPagoConcepto", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function
    Public Function ObtenerCreditoConsulta(ByVal pPageSize As Integer, _
                                           ByVal pCurrentPage As Integer, _
                                           ByVal pSortColumn As String, _
                                           ByVal pSortOrder As String, _
                                           ByVal pENroDocumento As String, _
                                           ByVal pERazonSocial As String, _
                                           ByVal pETipoDocumento As String) As String Implements IPagosNTx.ObtenerCreditoConsulta
        'Variables
        Dim odtbListado As New DataTable
        Dim oEPagoConcepto As New EGcc_otroconcepto
        Dim prmParameter(6) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        'oEPagoConcepto = CFunciones.DeserializeObject(Of EGcc_otroconcepto)(pEPagoConcepto)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 20, pENroDocumento, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_RazonSocial", DbType.String, 200, pERazonSocial, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_TipoDocumento", DbType.String, 20, pETipoDocumento, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Credito_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Consulta Credito", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    Public Function GetDatosCarta(ByVal pNroLote As String) As String Implements IPagosNTx.GetDatosCarta
        Dim odtbImpuestoMultasInmueble As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 8, pNroLote, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_PAGOS_CARTAIMPUESTO_GET"
            objRequest.Parameters.AddRange(prmParameter)
            odtbImpuestoMultasInmueble = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GetImpuestoMunicipalBienes", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If odtbImpuestoMultasInmueble Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of DataTable)(odtbImpuestoMultasInmueble)
        End If
    End Function
    Public Function GetDatosCartaExcel(ByVal pNroLote As String) As String Implements IPagosNTx.GetDatosCartaExcel
        Dim odtbImpuestoMultasInmueble As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 8, pNroLote, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_PAGOS_CARTAIMPUESTO_EXCEL_GET"
            objRequest.Parameters.AddRange(prmParameter)
            odtbImpuestoMultasInmueble = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GetImpuestoMunicipalBienes", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If odtbImpuestoMultasInmueble Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of DataTable)(odtbImpuestoMultasInmueble)
        End If
    End Function

#End Region

End Class

#End Region
