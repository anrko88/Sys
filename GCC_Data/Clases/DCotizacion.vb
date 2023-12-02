
Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DCotizacionTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("F01AE86E-FEF6-44b3-ABC7-F0E63BDDB50F") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCotizacionTx")> _
Public Class DCotizacionTx
    Inherits ServicedComponent
    Implements ICotizacionTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DCotizacionTx"
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Inserta un nuevo registro en la tabla Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Cotizacion serializada</param>
    ''' <returns>String con el número de Cotizacion</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function InsertarCotizacion(ByVal pECotizacion As String) As String Implements ICotizacionTx.InsertarCotizacion

        'Variables
        Dim parCodigoCotizacion As IDataParameter
        Dim oEGcc_cotizacion As New EGcc_cotizacion
        Dim prmParameter(76) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGcc_cotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oEGcc_cotizacion.Codigocotizacion, ParameterDirection.InputOutput)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Codigoestadocotizacion", DbType.String, 100, oEGcc_cotizacion.Codigoestadocotizacion, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pii_Generarcarta", DbType.Int16, 0, oEGcc_cotizacion.Generarcarta, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pii_FlagCliente", DbType.Int16, 0, oEGcc_cotizacion.FlagCliente, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Codsubprestatario", DbType.String, 6, oEGcc_cotizacion.Codsubprestatario, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_NombreCliente", DbType.String, 100, oEGcc_cotizacion.NombreCliente, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Codigotipopersona", DbType.String, 100, oEGcc_cotizacion.Codigotipopersona, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_CodigoTipoDocumento", DbType.String, 100, oEGcc_cotizacion.CodigoTipoDocumento, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_NumeroDocumento", DbType.String, 11, oEGcc_cotizacion.NumeroDocumento, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pii_FlagLinea", DbType.Int16, 0, oEGcc_cotizacion.FlagLinea, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pin_Tasalinea", DbType.Decimal)
        prmParameter(10).Precision = 18
        prmParameter(10).Scale = 6
        prmParameter(10).Value = oEGcc_cotizacion.Tasalinea
        prmParameter(11) = New DAABRequest.Parameter("@piv_Numerolinea", DbType.String, 10, oEGcc_cotizacion.Numerolinea, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_Codigoejecutivobanca", DbType.String, 100, oEGcc_cotizacion.Codigoejecutivobanca, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_Codigobanca", DbType.String, 100, oEGcc_cotizacion.Codigobanca, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@piv_Codigogrupozonal", DbType.String, 100, oEGcc_cotizacion.Codigogrupozonal, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@piv_Codigoejecutivoleasing", DbType.String, 100, oEGcc_cotizacion.Codigoejecutivoleasing, ParameterDirection.Input)

        'DatosGenerales :: Cotizacion
        prmParameter(16) = New DAABRequest.Parameter("@piv_Codproductofinancieroactivo", DbType.String, 6, oEGcc_cotizacion.Codproductofinancieroactivo, ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@piv_Codproductofinancieropasivo", DbType.String, 6, oEGcc_cotizacion.Codproductofinancieropasivo, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@piv_CodigoSubTipoContrato", DbType.String, 100, oEGcc_cotizacion.CodigoSubTipoContrato, ParameterDirection.Input)
        prmParameter(19) = New DAABRequest.Parameter("@piv_Codigomoneda", DbType.String, 3, oEGcc_cotizacion.Codigomoneda, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@piv_Codigoprocedencia", DbType.String, 100, oEGcc_cotizacion.Codigoprocedencia, ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@piv_Codigoclasificacionbien", DbType.String, 100, oEGcc_cotizacion.Codigoclasificacionbien, ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@piv_CodigoTipoBien", DbType.String, 100, oEGcc_cotizacion.CodigoTipoBien, ParameterDirection.Input)
        prmParameter(23) = New DAABRequest.Parameter("@piv_Codigotipoinmueble", DbType.String, 100, oEGcc_cotizacion.Codigotipoinmueble, ParameterDirection.Input)
        prmParameter(24) = New DAABRequest.Parameter("@piv_Codigoestadobien", DbType.String, 100, oEGcc_cotizacion.Codigoestadobien, ParameterDirection.Input)
        prmParameter(25) = New DAABRequest.Parameter("@pin_Precioventa", DbType.Decimal)
        prmParameter(25).Precision = 18
        prmParameter(25).Scale = 6
        prmParameter(25).Value = oEGcc_cotizacion.Precioventa
        prmParameter(26) = New DAABRequest.Parameter("@pin_Valorventaigv", DbType.Decimal)
        prmParameter(26).Precision = 18
        prmParameter(26).Scale = 6
        prmParameter(26).Value = oEGcc_cotizacion.Valorventaigv
        prmParameter(27) = New DAABRequest.Parameter("@pin_Valorventa", DbType.Decimal)
        prmParameter(27).Precision = 18
        prmParameter(27).Scale = 6
        prmParameter(27).Value = oEGcc_cotizacion.Valorventa
        prmParameter(28) = New DAABRequest.Parameter("@pin_Importecuotainicial", DbType.Decimal)
        prmParameter(28).Precision = 18
        prmParameter(28).Scale = 6
        prmParameter(28).Value = oEGcc_cotizacion.Importecuotainicial
        prmParameter(29) = New DAABRequest.Parameter("@pin_Cuotainicialporc", DbType.Decimal)
        prmParameter(29).Precision = 18
        prmParameter(29).Scale = 6
        prmParameter(29).Value = oEGcc_cotizacion.Cuotainicialporc
        prmParameter(30) = New DAABRequest.Parameter("@pin_Riesgoneto", DbType.Decimal)
        prmParameter(30).Precision = 18
        prmParameter(30).Scale = 6
        prmParameter(30).Value = oEGcc_cotizacion.Riesgoneto

        'DatosGenerales :: Cronograma
        prmParameter(31) = New DAABRequest.Parameter("@piv_Codigotipocronograma", DbType.String, 100, oEGcc_cotizacion.Codigotipocronograma, ParameterDirection.Input)
        prmParameter(32) = New DAABRequest.Parameter("@pii_Numerocuotas", DbType.Int16, 0, oEGcc_cotizacion.Numerocuotas, ParameterDirection.Input)
        prmParameter(33) = New DAABRequest.Parameter("@piv_Codigoperiodicidad", DbType.String, 100, oEGcc_cotizacion.Codigoperiodicidad, ParameterDirection.Input)
        prmParameter(34) = New DAABRequest.Parameter("@piv_Codigofrecuenciapago", DbType.String, 100, oEGcc_cotizacion.Codigofrecuenciapago, ParameterDirection.Input)
        prmParameter(35) = New DAABRequest.Parameter("@pii_Plazograciacuota", DbType.Int16, 0, oEGcc_cotizacion.Plazograciacuota, ParameterDirection.Input)
        prmParameter(36) = New DAABRequest.Parameter("@piv_Codigotipograciacuota", DbType.String, 100, oEGcc_cotizacion.Codigotipograciacuota, ParameterDirection.Input)
        prmParameter(37) = New DAABRequest.Parameter("@pid_Fechaprimervencimiento", DbType.Date, 0, oEGcc_cotizacion.Fechaprimervencimiento, ParameterDirection.Input)

        'DatosGenerales :: Tasas
        prmParameter(38) = New DAABRequest.Parameter("@pin_Teaporc", DbType.Decimal)
        prmParameter(38).Precision = 18
        prmParameter(38).Scale = 6
        prmParameter(38).Value = oEGcc_cotizacion.Teaporc
        prmParameter(39) = New DAABRequest.Parameter("@pin_Costofondoporc", DbType.Decimal)
        prmParameter(39).Precision = 18
        prmParameter(39).Scale = 6
        prmParameter(39).Value = oEGcc_cotizacion.Costofondoporc
        prmParameter(40) = New DAABRequest.Parameter("@pin_Spreadporc", DbType.Decimal)
        prmParameter(40).Precision = 18
        prmParameter(40).Scale = 6
        prmParameter(40).Value = oEGcc_cotizacion.Spreadporc
        prmParameter(41) = New DAABRequest.Parameter("@pin_Precuotaporc", DbType.Decimal)
        prmParameter(41).Precision = 18
        prmParameter(41).Scale = 6
        prmParameter(41).Value = oEGcc_cotizacion.Precuotaporc
        prmParameter(42) = New DAABRequest.Parameter("@pii_Plazograciaprecuota", DbType.Int16, 0, oEGcc_cotizacion.Plazograciaprecuota, ParameterDirection.Input)


        'DatosGenerales :: Comsiones
        prmParameter(43) = New DAABRequest.Parameter("@pin_Opcioncompraporc", DbType.Decimal)
        prmParameter(43).Precision = 18
        prmParameter(43).Scale = 6
        prmParameter(43).Value = oEGcc_cotizacion.Opcioncompraporc
        prmParameter(44) = New DAABRequest.Parameter("@pin_Importeopcioncompra", DbType.Decimal)
        prmParameter(44).Precision = 18
        prmParameter(44).Scale = 6
        prmParameter(44).Value = oEGcc_cotizacion.Importeopcioncompra
        prmParameter(45) = New DAABRequest.Parameter("@pin_Comisionactivacionporc", DbType.Decimal)
        prmParameter(45).Precision = 18
        prmParameter(45).Scale = 6
        prmParameter(45).Value = oEGcc_cotizacion.Comisionactivacionporc
        prmParameter(46) = New DAABRequest.Parameter("@pin_Importecomisionactivacion", DbType.Decimal)
        prmParameter(46).Precision = 18
        prmParameter(46).Scale = 6
        prmParameter(46).Value = oEGcc_cotizacion.Importecomisionactivacion
        prmParameter(47) = New DAABRequest.Parameter("@pin_Comisionestructuracionporc", DbType.Decimal)
        prmParameter(47).Precision = 18
        prmParameter(47).Scale = 6
        prmParameter(47).Value = oEGcc_cotizacion.Comisionestructuracionporc
        prmParameter(48) = New DAABRequest.Parameter("@pin_Importecomisionestructuracion", DbType.Decimal)
        prmParameter(48).Precision = 18
        prmParameter(48).Scale = 6
        prmParameter(48).Value = oEGcc_cotizacion.Importecomisionestructuracion

        'DatosGenerales :: SeguroBien
        prmParameter(49) = New DAABRequest.Parameter("@piv_Codigobientiposeguro", DbType.String, 100, oEGcc_cotizacion.Codigobientiposeguro, ParameterDirection.Input)
        prmParameter(50) = New DAABRequest.Parameter("@pin_Bienimporteprima", DbType.Decimal)
        prmParameter(50).Precision = 18
        prmParameter(50).Scale = 6
        prmParameter(50).Value = oEGcc_cotizacion.Bienimporteprima
        prmParameter(51) = New DAABRequest.Parameter("@pii_Biennrocuotasfinanciar", DbType.Int16, 0, oEGcc_cotizacion.Biennrocuotasfinanciar, ParameterDirection.Input)

        'DatosGenerales :: SeguroDegravamen
        prmParameter(52) = New DAABRequest.Parameter("@piv_Codigodesgravamentiposeguro", DbType.String, 100, oEGcc_cotizacion.Codigodesgravamentiposeguro, ParameterDirection.Input)
        prmParameter(53) = New DAABRequest.Parameter("@pin_Desgravamenimporteprima", DbType.Decimal)
        prmParameter(53).Precision = 18
        prmParameter(53).Scale = 6
        prmParameter(53).Value = oEGcc_cotizacion.Desgravamenimporteprima
        prmParameter(54) = New DAABRequest.Parameter("@pii_Desgravamennrocuotasfinanciar", DbType.Int16, 0, oEGcc_cotizacion.Desgravamennrocuotasfinanciar, ParameterDirection.Input)

        'Opciones
        prmParameter(55) = New DAABRequest.Parameter("@pii_Mostrarteacartas", DbType.Int16, 0, oEGcc_cotizacion.Mostrarteacartas, ParameterDirection.Input)
        prmParameter(56) = New DAABRequest.Parameter("@pii_Mostrarmontocomision", DbType.Int16, 0, oEGcc_cotizacion.Mostrarmontocomision, ParameterDirection.Input)
        prmParameter(57) = New DAABRequest.Parameter("@pid_Fechaingreso", DbType.Date, 0, oEGcc_cotizacion.Fechaingreso, ParameterDirection.Input)
        prmParameter(58) = New DAABRequest.Parameter("@pid_FechaOfertaValida", DbType.Date, 0, oEGcc_cotizacion.FechaOfertaValida, ParameterDirection.Input)
        prmParameter(59) = New DAABRequest.Parameter("@pii_Periododisponible", DbType.Int16, 0, oEGcc_cotizacion.Periododisponible, ParameterDirection.Input)
        prmParameter(60) = New DAABRequest.Parameter("@pid_Fechamaxactivacion", DbType.Date, 0, oEGcc_cotizacion.Fechamaxactivacion, ParameterDirection.Input)
        prmParameter(61) = New DAABRequest.Parameter("@piv_Otrascomisiones", DbType.String, 250, oEGcc_cotizacion.Otrascomisiones, ParameterDirection.Input)
        prmParameter(62) = New DAABRequest.Parameter("@piv_DescripcionProveedor", DbType.String, 250, oEGcc_cotizacion.DescripcionProveedor, ParameterDirection.Input)

        'Nuevos
        prmParameter(63) = New DAABRequest.Parameter("@piv_DesEjecutivoBanca", DbType.String, 150, oEGcc_cotizacion.DesEjecutivoBanca, ParameterDirection.Input)
        prmParameter(64) = New DAABRequest.Parameter("@piv_DesZonal", DbType.String, 150, oEGcc_cotizacion.DesZonal, ParameterDirection.Input)
        prmParameter(65) = New DAABRequest.Parameter("@pin_MontoPorcIGV", DbType.Decimal)
        prmParameter(65).Precision = 18
        prmParameter(65).Scale = 6
        prmParameter(65).Value = oEGcc_cotizacion.MontoPorcIGV
        prmParameter(66) = New DAABRequest.Parameter("@piv_FlagCuotaInicial", DbType.String, 1, oEGcc_cotizacion.FlagCuotaInicial, ParameterDirection.Input)

        prmParameter(67) = New DAABRequest.Parameter("@piv_DireccionCliente", DbType.String, 120, oEGcc_cotizacion.DireccionCliente, ParameterDirection.Input)

        'Auditoria
        prmParameter(68) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEGcc_cotizacion.Audestadologico, ParameterDirection.Input)
        prmParameter(69) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGcc_cotizacion.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(70) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGcc_cotizacion.Audusuariomodificacion, ParameterDirection.Input)


        'IBK - RPH
        prmParameter(71) = New DAABRequest.Parameter("@piv_FlagOpcionCompras", DbType.String, 1, oEGcc_cotizacion.FlagOpcionCompras, ParameterDirection.Input)
        prmParameter(72) = New DAABRequest.Parameter("@piv_FlagComisionActivacion", DbType.String, 1, oEGcc_cotizacion.FlagComisionActivacion, ParameterDirection.Input)
        prmParameter(73) = New DAABRequest.Parameter("@piv_FlagComisionEstructuracion", DbType.String, 1, oEGcc_cotizacion.FlagComisionEstructuracion, ParameterDirection.Input)
        'Fin

        'GCCTS_JRC_20120304-Seguimiento Datos acionales
        prmParameter(74) = New DAABRequest.Parameter("@piv_CodigoUsuario", DbType.String, 10, oEGcc_cotizacion.CodigoUsuario, ParameterDirection.Input)
        prmParameter(75) = New DAABRequest.Parameter("@piv_NombreUsuario", DbType.String, 200, oEGcc_cotizacion.NombreUsuario, ParameterDirection.Input)
        prmParameter(76) = New DAABRequest.Parameter("@piv_PerfilUsuario", DbType.String, 100, oEGcc_cotizacion.PerfilUsuario, ParameterDirection.Input)
        '---

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Cotizacion_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fstrCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parCodigoCotizacion = CType(obRequest.Parameters(0), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckStr(parCodigoCotizacion.Value.ToString())

    End Function

    ''' <summary>
    ''' Modifica un registro existente de la tabla Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ModificarCotizacion(ByVal pECotizacion As String) As Boolean Implements ICotizacionTx.ModificarCotizacion


        'Variables
        Dim blnRetorno As Boolean = False
        Dim oEGcc_cotizacion As New EGcc_cotizacion
        Dim prmParameter(76) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGcc_cotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oEGcc_cotizacion.Codigocotizacion, ParameterDirection.InputOutput)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Codigoestadocotizacion", DbType.String, 100, oEGcc_cotizacion.Codigoestadocotizacion, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pii_Generarcarta", DbType.Int16, 0, oEGcc_cotizacion.Generarcarta, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pii_FlagCliente", DbType.Int16, 0, oEGcc_cotizacion.FlagCliente, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Codsubprestatario", DbType.String, 6, oEGcc_cotizacion.Codsubprestatario, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_NombreCliente", DbType.String, 100, oEGcc_cotizacion.NombreCliente, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Codigotipopersona", DbType.String, 100, oEGcc_cotizacion.Codigotipopersona, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_CodigoTipoDocumento", DbType.String, 100, oEGcc_cotizacion.CodigoTipoDocumento, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_NumeroDocumento", DbType.String, 11, oEGcc_cotizacion.NumeroDocumento, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pii_FlagLinea", DbType.Int16, 0, oEGcc_cotizacion.FlagLinea, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pin_Tasalinea", DbType.Decimal)
        prmParameter(10).Precision = 18
        prmParameter(10).Scale = 6
        prmParameter(10).Value = oEGcc_cotizacion.Tasalinea
        prmParameter(11) = New DAABRequest.Parameter("@piv_Numerolinea", DbType.String, 10, oEGcc_cotizacion.Numerolinea, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_Codigoejecutivobanca", DbType.String, 100, oEGcc_cotizacion.Codigoejecutivobanca, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_Codigobanca", DbType.String, 100, oEGcc_cotizacion.Codigobanca, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@piv_Codigogrupozonal", DbType.String, 100, oEGcc_cotizacion.Codigogrupozonal, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@piv_Codigoejecutivoleasing", DbType.String, 100, oEGcc_cotizacion.Codigoejecutivoleasing, ParameterDirection.Input)

        'DatosGenerales :: Cotizacion
        prmParameter(16) = New DAABRequest.Parameter("@piv_Codproductofinancieroactivo", DbType.String, 6, oEGcc_cotizacion.Codproductofinancieroactivo, ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@piv_Codproductofinancieropasivo", DbType.String, 6, oEGcc_cotizacion.Codproductofinancieropasivo, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@piv_CodigoSubTipoContrato", DbType.String, 100, oEGcc_cotizacion.CodigoSubTipoContrato, ParameterDirection.Input)
        prmParameter(19) = New DAABRequest.Parameter("@piv_Codigomoneda", DbType.String, 3, oEGcc_cotizacion.Codigomoneda, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@piv_Codigoprocedencia", DbType.String, 100, oEGcc_cotizacion.Codigoprocedencia, ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@piv_Codigoclasificacionbien", DbType.String, 100, oEGcc_cotizacion.Codigoclasificacionbien, ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@piv_CodigoTipoBien", DbType.String, 100, oEGcc_cotizacion.CodigoTipoBien, ParameterDirection.Input)
        prmParameter(23) = New DAABRequest.Parameter("@piv_Codigotipoinmueble", DbType.String, 100, oEGcc_cotizacion.Codigotipoinmueble, ParameterDirection.Input)
        prmParameter(24) = New DAABRequest.Parameter("@piv_Codigoestadobien", DbType.String, 100, oEGcc_cotizacion.Codigoestadobien, ParameterDirection.Input)
        prmParameter(25) = New DAABRequest.Parameter("@pin_Precioventa", DbType.Decimal)
        prmParameter(25).Precision = 18
        prmParameter(25).Scale = 6
        prmParameter(25).Value = oEGcc_cotizacion.Precioventa
        prmParameter(26) = New DAABRequest.Parameter("@pin_Valorventaigv", DbType.Decimal)
        prmParameter(26).Precision = 18
        prmParameter(26).Scale = 6
        prmParameter(26).Value = oEGcc_cotizacion.Valorventaigv
        prmParameter(27) = New DAABRequest.Parameter("@pin_Valorventa", DbType.Decimal)
        prmParameter(27).Precision = 18
        prmParameter(27).Scale = 6
        prmParameter(27).Value = oEGcc_cotizacion.Valorventa
        prmParameter(28) = New DAABRequest.Parameter("@pin_Importecuotainicial", DbType.Decimal)
        prmParameter(28).Precision = 18
        prmParameter(28).Scale = 6
        prmParameter(28).Value = oEGcc_cotizacion.Importecuotainicial
        prmParameter(29) = New DAABRequest.Parameter("@pin_Cuotainicialporc", DbType.Decimal)
        prmParameter(29).Precision = 18
        prmParameter(29).Scale = 6
        prmParameter(29).Value = oEGcc_cotizacion.Cuotainicialporc
        prmParameter(30) = New DAABRequest.Parameter("@pin_Riesgoneto", DbType.Decimal)
        prmParameter(30).Precision = 18
        prmParameter(30).Scale = 6
        prmParameter(30).Value = oEGcc_cotizacion.Riesgoneto

        'DatosGenerales :: Cronograma
        prmParameter(31) = New DAABRequest.Parameter("@piv_Codigotipocronograma", DbType.String, 100, oEGcc_cotizacion.Codigotipocronograma, ParameterDirection.Input)
        prmParameter(32) = New DAABRequest.Parameter("@pii_Numerocuotas", DbType.Int16, 0, oEGcc_cotizacion.Numerocuotas, ParameterDirection.Input)
        prmParameter(33) = New DAABRequest.Parameter("@piv_Codigoperiodicidad", DbType.String, 100, oEGcc_cotizacion.Codigoperiodicidad, ParameterDirection.Input)
        prmParameter(34) = New DAABRequest.Parameter("@piv_Codigofrecuenciapago", DbType.String, 100, oEGcc_cotizacion.Codigofrecuenciapago, ParameterDirection.Input)
        prmParameter(35) = New DAABRequest.Parameter("@pii_Plazograciacuota", DbType.Int16, 0, oEGcc_cotizacion.Plazograciacuota, ParameterDirection.Input)
        prmParameter(36) = New DAABRequest.Parameter("@piv_Codigotipograciacuota", DbType.String, 100, oEGcc_cotizacion.Codigotipograciacuota, ParameterDirection.Input)
        prmParameter(37) = New DAABRequest.Parameter("@pid_Fechaprimervencimiento", DbType.Date, 0, oEGcc_cotizacion.Fechaprimervencimiento, ParameterDirection.Input)

        'DatosGenerales :: Tasas
        prmParameter(38) = New DAABRequest.Parameter("@pin_Teaporc", DbType.Decimal)
        prmParameter(38).Precision = 18
        prmParameter(38).Scale = 6
        prmParameter(38).Value = oEGcc_cotizacion.Teaporc
        prmParameter(39) = New DAABRequest.Parameter("@pin_Costofondoporc", DbType.Decimal)
        prmParameter(39).Precision = 18
        prmParameter(39).Scale = 6
        prmParameter(39).Value = oEGcc_cotizacion.Costofondoporc
        prmParameter(40) = New DAABRequest.Parameter("@pin_Spreadporc", DbType.Decimal)
        prmParameter(40).Precision = 18
        prmParameter(40).Scale = 6
        prmParameter(40).Value = oEGcc_cotizacion.Spreadporc
        prmParameter(41) = New DAABRequest.Parameter("@pin_Precuotaporc", DbType.Decimal)
        prmParameter(41).Precision = 18
        prmParameter(41).Scale = 6
        prmParameter(41).Value = oEGcc_cotizacion.Precuotaporc
        prmParameter(42) = New DAABRequest.Parameter("@pii_Plazograciaprecuota", DbType.Int16, 0, oEGcc_cotizacion.Plazograciaprecuota, ParameterDirection.Input)


        'DatosGenerales :: Comsiones
        prmParameter(43) = New DAABRequest.Parameter("@pin_Opcioncompraporc", DbType.Decimal)
        prmParameter(43).Precision = 18
        prmParameter(43).Scale = 6
        prmParameter(43).Value = oEGcc_cotizacion.Opcioncompraporc
        prmParameter(44) = New DAABRequest.Parameter("@pin_Importeopcioncompra", DbType.Decimal)
        prmParameter(44).Precision = 18
        prmParameter(44).Scale = 6
        prmParameter(44).Value = oEGcc_cotizacion.Importeopcioncompra
        prmParameter(45) = New DAABRequest.Parameter("@pin_Comisionactivacionporc", DbType.Decimal)
        prmParameter(45).Precision = 18
        prmParameter(45).Scale = 6
        prmParameter(45).Value = oEGcc_cotizacion.Comisionactivacionporc
        prmParameter(46) = New DAABRequest.Parameter("@pin_Importecomisionactivacion", DbType.Decimal)
        prmParameter(46).Precision = 18
        prmParameter(46).Scale = 6
        prmParameter(46).Value = oEGcc_cotizacion.Importecomisionactivacion
        prmParameter(47) = New DAABRequest.Parameter("@pin_Comisionestructuracionporc", DbType.Decimal)
        prmParameter(47).Precision = 18
        prmParameter(47).Scale = 6
        prmParameter(47).Value = oEGcc_cotizacion.Comisionestructuracionporc
        prmParameter(48) = New DAABRequest.Parameter("@pin_Importecomisionestructuracion", DbType.Decimal)
        prmParameter(48).Precision = 18
        prmParameter(48).Scale = 6
        prmParameter(48).Value = oEGcc_cotizacion.Importecomisionestructuracion

        'DatosGenerales :: SeguroBien
        prmParameter(49) = New DAABRequest.Parameter("@piv_Codigobientiposeguro", DbType.String, 100, oEGcc_cotizacion.Codigobientiposeguro, ParameterDirection.Input)
        prmParameter(50) = New DAABRequest.Parameter("@pin_Bienimporteprima", DbType.Decimal)
        prmParameter(50).Precision = 18
        prmParameter(50).Scale = 6
        prmParameter(50).Value = oEGcc_cotizacion.Bienimporteprima
        prmParameter(51) = New DAABRequest.Parameter("@pii_Biennrocuotasfinanciar", DbType.Int16, 0, oEGcc_cotizacion.Biennrocuotasfinanciar, ParameterDirection.Input)

        'DatosGenerales :: SeguroDegravamen
        prmParameter(52) = New DAABRequest.Parameter("@piv_Codigodesgravamentiposeguro", DbType.String, 100, oEGcc_cotizacion.Codigodesgravamentiposeguro, ParameterDirection.Input)
        prmParameter(53) = New DAABRequest.Parameter("@pin_Desgravamenimporteprima", DbType.Decimal)
        prmParameter(53).Precision = 18
        prmParameter(53).Scale = 6
        prmParameter(53).Value = oEGcc_cotizacion.Desgravamenimporteprima
        prmParameter(54) = New DAABRequest.Parameter("@pii_Desgravamennrocuotasfinanciar", DbType.Int16, 0, oEGcc_cotizacion.Desgravamennrocuotasfinanciar, ParameterDirection.Input)

        'Opciones
        prmParameter(55) = New DAABRequest.Parameter("@pii_Mostrarteacartas", DbType.Int16, 0, oEGcc_cotizacion.Mostrarteacartas, ParameterDirection.Input)
        prmParameter(56) = New DAABRequest.Parameter("@pii_Mostrarmontocomision", DbType.Int16, 0, oEGcc_cotizacion.Mostrarmontocomision, ParameterDirection.Input)
        prmParameter(57) = New DAABRequest.Parameter("@pid_Fechaingreso", DbType.Date, 0, oEGcc_cotizacion.Fechaingreso, ParameterDirection.Input)
        prmParameter(58) = New DAABRequest.Parameter("@pid_FechaOfertaValida", DbType.Date, 0, oEGcc_cotizacion.FechaOfertaValida, ParameterDirection.Input)
        prmParameter(59) = New DAABRequest.Parameter("@pii_Periododisponible", DbType.Int16, 0, oEGcc_cotizacion.Periododisponible, ParameterDirection.Input)
        prmParameter(60) = New DAABRequest.Parameter("@pid_Fechamaxactivacion", DbType.Date, 0, oEGcc_cotizacion.Fechamaxactivacion, ParameterDirection.Input)
        prmParameter(61) = New DAABRequest.Parameter("@piv_Otrascomisiones", DbType.String, 250, oEGcc_cotizacion.Otrascomisiones, ParameterDirection.Input)
        prmParameter(62) = New DAABRequest.Parameter("@piv_DescripcionProveedor", DbType.String, 250, oEGcc_cotizacion.DescripcionProveedor, ParameterDirection.Input)

        'Nuevos
        prmParameter(63) = New DAABRequest.Parameter("@piv_DesEjecutivoBanca", DbType.String, 150, oEGcc_cotizacion.DesEjecutivoBanca, ParameterDirection.Input)
        prmParameter(64) = New DAABRequest.Parameter("@piv_DesZonal", DbType.String, 150, oEGcc_cotizacion.DesZonal, ParameterDirection.Input)
        prmParameter(65) = New DAABRequest.Parameter("@pin_MontoPorcIGV", DbType.Decimal)
        prmParameter(65).Precision = 18
        prmParameter(65).Scale = 6
        prmParameter(65).Value = oEGcc_cotizacion.MontoPorcIGV
        prmParameter(66) = New DAABRequest.Parameter("@piv_FlagCuotaInicial", DbType.String, 1, oEGcc_cotizacion.FlagCuotaInicial, ParameterDirection.Input)

        prmParameter(67) = New DAABRequest.Parameter("@piv_DireccionCliente", DbType.String, 120, oEGcc_cotizacion.DireccionCliente, ParameterDirection.Input)

        'Auditoria
        prmParameter(68) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEGcc_cotizacion.Audestadologico, ParameterDirection.Input)
        prmParameter(69) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGcc_cotizacion.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(70) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGcc_cotizacion.Audusuariomodificacion, ParameterDirection.Input)

        'IBK - RPH
        prmParameter(71) = New DAABRequest.Parameter("@piv_FlagOpcionCompras", DbType.String, 1, oEGcc_cotizacion.FlagOpcionCompras, ParameterDirection.Input)
        prmParameter(72) = New DAABRequest.Parameter("@piv_FlagComisionActivacion", DbType.String, 1, oEGcc_cotizacion.FlagComisionActivacion, ParameterDirection.Input)
        prmParameter(73) = New DAABRequest.Parameter("@piv_FlagComisionEstructuracion", DbType.String, 1, oEGcc_cotizacion.FlagComisionEstructuracion, ParameterDirection.Input)
        'Fin

        'GCCTS_JRC_20120304-Seguimiento Datos acionales
        prmParameter(74) = New DAABRequest.Parameter("@piv_CodigoUsuario", DbType.String, 10, oEGcc_cotizacion.CodigoUsuario, ParameterDirection.Input)
        prmParameter(75) = New DAABRequest.Parameter("@piv_NombreUsuario", DbType.String, 200, oEGcc_cotizacion.NombreUsuario, ParameterDirection.Input)
        prmParameter(76) = New DAABRequest.Parameter("@piv_PerfilUsuario", DbType.String, 100, oEGcc_cotizacion.PerfilUsuario, ParameterDirection.Input)
        '---

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Cotizacion_upd"
        obRequest.Parameters.AddRange(prmParameter)
        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    'Inicio IBK - RPH
    Public Function RegistrarRutaCronograma(ByVal pECotizacion As String) As Boolean Implements ICotizacionTx.RegistrarRutaCronograma
        Dim blnRetorno As Boolean = False
        Dim oEGcc_cotizacion As New EGcc_cotizacion
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGcc_cotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oEGcc_cotizacion.Codigocotizacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_ArchivoCronograma", DbType.String, 100, oEGcc_cotizacion.ArchivoCronogramaAdjunto, ParameterDirection.Input)

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CotizacionArchivoCronograma_upd"
        obRequest.Parameters.AddRange(prmParameter)
        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function
    'Fin

    ''' <summary>
    ''' Modifica el estado de la Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Function ModificarCotizacionEstado(ByVal pECotizacion As String) As Boolean Implements ICotizacionTx.ModificarCotizacionEstado

        'Variables
        Dim blnRetorno As Boolean = False
        Dim oEGcc_cotizacion As New EGcc_cotizacion
        Dim prmParameter(4) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGcc_cotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oEGcc_cotizacion.Codigocotizacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Codigoestadocotizacion", DbType.String, 100, oEGcc_cotizacion.Codigoestadocotizacion, ParameterDirection.Input)

        'Auditoria
        prmParameter(2) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEGcc_cotizacion.Audestadologico, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGcc_cotizacion.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGcc_cotizacion.Audusuariomodificacion, ParameterDirection.Input)


        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CotizacionEstado_upd"
        obRequest.Parameters.AddRange(prmParameter)
        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Modifica el estado de carta de la Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Function ModificarCotizacionCarta(ByVal pECotizacion As String) As Boolean Implements ICotizacionTx.ModificarCotizacionCarta

        'Variables
        Dim blnRetorno As Boolean = False
        Dim oEGcc_cotizacion As New EGcc_cotizacion
        Dim prmParameter(6) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGcc_cotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oEGcc_cotizacion.Codigocotizacion, ParameterDirection.Input)

        'Auditoria
        prmParameter(1) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEGcc_cotizacion.Audestadologico, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGcc_cotizacion.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGcc_cotizacion.Audusuariomodificacion, ParameterDirection.Input)

        'GCCTS_JRC_20120304-Seguimiento Datos acionales
        prmParameter(4) = New DAABRequest.Parameter("@piv_CodigoUsuario", DbType.String, 10, oEGcc_cotizacion.CodigoUsuario, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_NombreUsuario", DbType.String, 200, oEGcc_cotizacion.NombreUsuario, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_PerfilUsuario", DbType.String, 100, oEGcc_cotizacion.PerfilUsuario, ParameterDirection.Input)
        '---

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CotizacionCarta_upd"
        obRequest.Parameters.AddRange(prmParameter)
        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Aprueba la Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Function CotizacionAprobar(ByVal pECotizacion As String) As Boolean Implements ICotizacionTx.CotizacionAprobar

        'Variables
        Dim blnRetorno As Boolean = False
        Dim oEGcc_cotizacion As New EGcc_cotizacion
        Dim prmParameter(7) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGcc_cotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oEGcc_cotizacion.Codigocotizacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Codigosupervisoraprobo", DbType.String, 12, oEGcc_cotizacion.Codigocotizacion, ParameterDirection.Input)

        'Auditoria
        prmParameter(2) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEGcc_cotizacion.Audestadologico, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGcc_cotizacion.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGcc_cotizacion.Audusuariomodificacion, ParameterDirection.Input)

        'GCCTS_JRC_20120304-Seguimiento Datos acionales
        prmParameter(5) = New DAABRequest.Parameter("@piv_CodigoUsuario", DbType.String, 10, oEGcc_cotizacion.CodigoUsuario, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_NombreUsuario", DbType.String, 200, oEGcc_cotizacion.NombreUsuario, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_PerfilUsuario", DbType.String, 100, oEGcc_cotizacion.PerfilUsuario, ParameterDirection.Input)
        '---

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CotizacionAprobar_gst"
        obRequest.Parameters.AddRange(prmParameter)
        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Gestion Versionamiento de la  Cotizacion
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Function CotizacionVersionamiento(ByVal pECotizacion As String) As Boolean Implements ICotizacionTx.CotizacionVersionamiento

        'Variables
        Dim blnRetorno As Boolean = False
        Dim oEGcc_cotizacion As New EGcc_cotizacion
        Dim prmParameter(3) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGcc_cotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oEGcc_cotizacion.Codigocotizacion, ParameterDirection.Input)

        'Auditoria
        prmParameter(1) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEGcc_cotizacion.Audestadologico, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGcc_cotizacion.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGcc_cotizacion.Audusuariomodificacion, ParameterDirection.Input)

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CotizacionVersion_gst"
        obRequest.Parameters.AddRange(prmParameter)
        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Rechazar la Cotizacion
    ''' </summary>
    ''' <param name="pECotizacionSeguimiento">Entidad Cotizacion serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Function CotizacionRechazar(ByVal pECotizacionSeguimiento As String) As Boolean Implements ICotizacionTx.CotizacionRechazar

        'Variables
        Dim blnRetorno As Boolean = False
        Dim oEGcc_seguimientocotizacion As New EGcc_seguimientocotizacion
        Dim prmParameter(7) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGcc_seguimientocotizacion = CFunciones.DeserializeObject(Of EGcc_seguimientocotizacion)(pECotizacionSeguimiento)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oEGcc_seguimientocotizacion.Codigocotizacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Comentario", DbType.String, 250, oEGcc_seguimientocotizacion.Comentario, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pid_Fecha", DbType.Date, 0, oEGcc_seguimientocotizacion.Fechacambioestado, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_TipoMotivo", DbType.String, 100, oEGcc_seguimientocotizacion.Codigotipomotivo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Archivo", DbType.String, 255, oEGcc_seguimientocotizacion.Archivoadjunto, ParameterDirection.Input)

        'Auditoria
        prmParameter(5) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEGcc_seguimientocotizacion.Audestadologico, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGcc_seguimientocotizacion.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGcc_seguimientocotizacion.Audusuariomodificacion, ParameterDirection.Input)

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CotizacionRechazar_gst"
        obRequest.Parameters.AddRange(prmParameter)
        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function




#End Region

#Region "Web Services"
    ''' <summary>
    ''' Web Services ModificarEstadoCotizacionWS
    ''' </summary>
    ''' <param name="pstrNumeroCotizacion"></param>
    ''' <param name="pstrCodigoEstado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Public Function ModificarEstadoCotizacionWS(ByVal pstrNumeroCotizacion As String, ByVal pstrCodUnico As String, ByVal pstrCodigoEstado As String) As Boolean Implements ICotizacionTx.ModificarEstadoCotizacionWS

        'Variables
        Dim blnRetorno As Boolean = False
        Dim oEGcc_cotizacion As New EGcc_cotizacion
        Dim prmParameter(2) As DAABRequest.Parameter

        'Deserealiza la Entidad
        'oEGcc_cotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pstrNumeroCotizacion)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, pstrNumeroCotizacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 12, pstrCodUnico, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodigoEstadoBien", DbType.String, 3, pstrCodigoEstado, ParameterDirection.Input)

        'Auditoria
        'prmParameter(2) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEGcc_cotizacion.Audestadologico, ParameterDirection.Input)
        'prmParameter(3) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGcc_cotizacion.Audusuarioregistro, ParameterDirection.Input)
        'prmParameter(4) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGcc_cotizacion.Audusuariomodificacion, ParameterDirection.Input)

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CotizacionEstadoWS_upd"
        obRequest.Parameters.AddRange(prmParameter)
        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarEstadoCotizacionWS", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function
    Public Function ModificarCotizacionWS(ByVal pECotizacion As String) As Boolean Implements ICotizacionTx.ModificarCotizacionWS

        'Variables
        Dim blnRetorno As Boolean = False
        Dim oEGcc_cotizacion As New EGcc_cotizacion
        Dim prmParameter(22) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGcc_cotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oEGcc_cotizacion.Codigocotizacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 20, oEGcc_cotizacion.CodUnico, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_NumeroLinea", DbType.String, 20, oEGcc_cotizacion.Numerolinea, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodProductoFinancieroPasivo", DbType.String, 6, oEGcc_cotizacion.Codproductofinancieropasivo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_CodigoClasificacionBien", DbType.String, 6, oEGcc_cotizacion.Codigoclasificacionbien, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_CodigoMoneda", DbType.String, 3, oEGcc_cotizacion.Codigomoneda, ParameterDirection.Input)

        prmParameter(6) = New DAABRequest.Parameter("@piv_ValorVenta", DbType.Decimal)
        prmParameter(6).Precision = 18
        prmParameter(6).Scale = 6
        prmParameter(6).Value = oEGcc_cotizacion.Codigomoneda

        prmParameter(7) = New DAABRequest.Parameter("@piv_ValorVentaIgv", DbType.Decimal)
        prmParameter(7).Precision = 18
        prmParameter(7).Scale = 6
        prmParameter(7).Value = oEGcc_cotizacion.Valorventaigv

        prmParameter(8) = New DAABRequest.Parameter("@piv_PrecioVenta", DbType.Decimal)
        prmParameter(8).Precision = 18
        prmParameter(8).Scale = 6
        prmParameter(8).Value = oEGcc_cotizacion.Precioventa

        prmParameter(9) = New DAABRequest.Parameter("@piv_CuotaInicialPorc", DbType.Decimal)
        prmParameter(9).Precision = 18
        prmParameter(9).Scale = 6
        prmParameter(9).Value = oEGcc_cotizacion.Cuotainicialporc



        prmParameter(10) = New DAABRequest.Parameter("@piv_RiesgoNeto", DbType.Decimal)
        prmParameter(10).Precision = 18
        prmParameter(10).Scale = 6
        prmParameter(10).Value = oEGcc_cotizacion.Riesgoneto


        prmParameter(11) = New DAABRequest.Parameter("@piv_FechaMaxActivacion", DbType.String, 10, oEGcc_cotizacion.Fechamaxactivacion, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_NumeroCuotas", DbType.Int32, 4, oEGcc_cotizacion.Numerocuotas, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_PeriodoDisponible", DbType.String, 10, oEGcc_cotizacion.Periododisponible, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@piv_CodigoFrecuenciaPago", DbType.String, 10, oEGcc_cotizacion.Codigofrecuenciapago, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@piv_FechaPrimerVencimiento", DbType.String, 10, oEGcc_cotizacion.Fechaprimervencimiento, ParameterDirection.Input)

        prmParameter(16) = New DAABRequest.Parameter("@piv_TEAPorc", DbType.Decimal)
        prmParameter(16).Precision = 18
        prmParameter(16).Scale = 6
        prmParameter(16).Value = oEGcc_cotizacion.Teaporc

        prmParameter(17) = New DAABRequest.Parameter("@piv_SpreadPorc", DbType.Decimal)
        prmParameter(17).Precision = 18
        prmParameter(17).Scale = 6
        prmParameter(17).Value = oEGcc_cotizacion.Spreadporc

        prmParameter(18) = New DAABRequest.Parameter("@piv_PrecuotaPorc", DbType.Decimal)
        prmParameter(18).Precision = 18
        prmParameter(18).Scale = 6
        prmParameter(18).Value = oEGcc_cotizacion.Precuotaporc


        prmParameter(19) = New DAABRequest.Parameter("@piv_ImporteOpcionCompra", DbType.Decimal)
        prmParameter(19).Precision = 18
        prmParameter(19).Scale = 6
        prmParameter(19).Value = oEGcc_cotizacion.Importeopcioncompra

        prmParameter(20) = New DAABRequest.Parameter("@piv_ImporteComisionActivacion", DbType.Decimal)
        prmParameter(20).Precision = 18
        prmParameter(20).Scale = 6
        prmParameter(20).Value = oEGcc_cotizacion.Importecomisionactivacion

        prmParameter(21) = New DAABRequest.Parameter("@piv_CodigoEstadoCotizacion", DbType.String, 3, oEGcc_cotizacion.Codigoestadocotizacion, ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@piv_AudFechaModificacion", DbType.String, 10, oEGcc_cotizacion.Audfechamodificacion, ParameterDirection.Input)


        'Auditoria
        'prmParameter(2) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEGcc_cotizacion.Audestadologico, ParameterDirection.Input)
        'prmParameter(3) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGcc_cotizacion.Audusuarioregistro, ParameterDirection.Input)
        'prmParameter(4) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGcc_cotizacion.Audusuariomodificacion, ParameterDirection.Input)


        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CotizacionWS_Upd"
        obRequest.Parameters.AddRange(prmParameter)
        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

#End Region


End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DCotizacionNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("B0ADB13B-16A8-4e78-ACF4-F0C57A7C8394") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCotizacionNTx")> _
Public Class DCotizacionNTx
    Inherits ServicedComponent
    Implements ICotizacionNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DCotizacionNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoCotizacion(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pECotizacion As String) As String Implements ICotizacionNTx.ListadoCotizacion

        'Variables
        Dim odtbListadoCotizacion As DataTable
        Dim oEGcc_cotizacion As New EGcc_cotizacion
        Dim prmParameter(11) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEGcc_cotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oEGcc_cotizacion.Codigocotizacion, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 10, oEGcc_cotizacion.CodUnico, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_NombreCliente", DbType.String, 100, oEGcc_cotizacion.NombreCliente, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_Codigoejecutivoleasing", DbType.String, 100, oEGcc_cotizacion.Codigoejecutivoleasing, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@piv_Codigoclasificacionbien", DbType.String, 100, oEGcc_cotizacion.Codigoclasificacionbien, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@pii_Codigoestadocotizacion", DbType.String, 100, oEGcc_cotizacion.Codigoestadocotizacion, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@pid_FechaInicio", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEGcc_cotizacion.FechaInicio), ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@pid_FechaFin", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEGcc_cotizacion.FechaFin), ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Cotizacion_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCotizacion = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjListadoCotizacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoCotizacion)
    End Function

    ''' <summary>
    ''' Obtiene una cotizacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function GetCotizacion(ByVal pECotizacion As String) As String Implements ICotizacionNTx.GetCotizacion

        'Variables
        Dim odtbListadoCotizacion As DataTable
        Dim oEGcc_cotizacion As New EGcc_cotizacion
        Dim prmParameter(0) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEGcc_cotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        Try
            'Campos TEMPORAL            
            prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oEGcc_cotizacion.Codigocotizacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Cotizacion_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCotizacion = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GetCotizacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoCotizacion)
    End Function

    ''' <summary>
    ''' Lista Los datos de la cotizcion por un nuemro o codigo unico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 24/05/2012
    ''' </remarks>
    Public Function ConsultaCotizacion(ByVal pstrNumeroCotizacion As String, _
                                    ByVal pstrCodigoUnico As String) As String Implements ICotizacionNTx.ConsultaCotizacion

        'Variables
        Dim odtbListado As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, pstrNumeroCotizacion, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 10, pstrCodigoUnico, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CotizacionWS_Sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ConsultaCotizacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    'Inicio AAE
    Public Function GetCodUsuarioEjecutivo(ByVal pstrNroCotizacion As String, ByVal nbrEsCotizacion As Integer) As String Implements ICotizacionNTx.GetCodUsuarioEjecutivo
        'Variables
        Dim odtbListado As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, pstrNroCotizacion, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_Cotizacion", DbType.Int16, 1, nbrEsCotizacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CodigoUsuarioEjecutivo_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ConsultaCUEjecutivo", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    'AAE - Retorna una tabla serializada con los CU de los administradores de leasing
    Public Function GetCodUsuarioAdministradoresLeasing() As String Implements ICotizacionNTx.GetCodUsuarioAdministradoresLeasing
        'Variables
        Dim odtbListado As DataTable

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CodigoUsuarioAdministradoresLeasing_sel"
            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GetCUAdministradoresLeasing", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    Public Function GetCodUsuarioAdministradoresComercial() As String Implements ICotizacionNTx.GetCodUsuarioAdministradoresComercial
        'Variables
        Dim odtbListado As DataTable

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CodigoUsuarioSupervisoresLeasing_sel"
            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "up_gcc_CUEjecutivosLeasing_sel", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function
    'Fin AAE
    'IBK - RPH
    Public Function ListadoCliente(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pRazonSocial As String) As String Implements ICotizacionNTx.ListadoCliente

        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(4) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_NombreRazonSocial", DbType.String, 8000, pRazonSocial, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Cliente_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoProveedor", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function
    'Fin RPH
#End Region

#Region "Web Services"

    Public Function ConsultarCotizacionWS(ByVal pstrNumeroCotizacion As String, _
                                  ByVal pstrCodigoUnico As String) As String Implements ICotizacionNTx.ConsultarCotizacionWS
        'Variables
        Dim odtbListado As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, pstrNumeroCotizacion, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 10, pstrCodigoUnico, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CotizacionWS_Sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ConsultarCotizacionWS", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function


#End Region
End Class

#End Region
