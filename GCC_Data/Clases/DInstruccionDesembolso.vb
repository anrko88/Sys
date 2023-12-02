
Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DInstruccionDesembolsoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 25/09/2012
''' </remarks>
<Guid("048FA077-5515-4ba9-B758-A196113960CC") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DInstruccionDesembolsoTx")> _
Public Class DInstruccionDesembolsoTx
    Inherits ServicedComponent
    Implements IInstruccionDesembolsoTx

#Region "Constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DInstruccionDesembolsoTx"

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Inserta un nuevo registro en la tabla InstruccionDesembolso
    ''' </summary>
    ''' <param name="pEInsDesembolso">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function InsertarInsDesembolso(ByVal pEInsDesembolso As String) As String Implements IInstruccionDesembolsoTx.InsertarInsDesembolso

        'Variables
        Dim parNumeroInstruccion As IDataParameter
        Dim oEGCC_InsDesembolso As New EGCC_InsDesembolso
        Dim prmParameter(4) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGCC_InsDesembolso = CFunciones.DeserializeObject(Of EGCC_InsDesembolso)(pEInsDesembolso)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEGCC_InsDesembolso.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Documentos", DbType.String, 1000, oEGCC_InsDesembolso.Documentos, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_UsuarioRegistro", DbType.String, 12, oEGCC_InsDesembolso.UsuarioRegistro, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_ActivacionLeasing", DbType.String, 1, oEGCC_InsDesembolso.FlagActivacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pov_CodInstruccion", DbType.String, 12, oEGCC_InsDesembolso.Codinstrucciondesembolso, ParameterDirection.InputOutput)

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        'Inicio IBK - AAE - Cambio el sp para generar
        'obRequest.Command = "up_gcc_InsDesembolso_ins"
        obRequest.Command = "up_gcc_InsDesembolso_ins2"
        'Fin IBK 
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fstrCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parNumeroInstruccion = CType(obRequest.Parameters(4), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckStr(parNumeroInstruccion.Value.ToString())

    End Function

    ''' <summary>
    ''' Gestion de cambios de una InstruccionDesembolso
    ''' </summary>
    ''' <param name="pEInsDesembolso">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function GestionInsDesembolso(ByVal pEInsDesembolso As String) As String Implements IInstruccionDesembolsoTx.GestionInsDesembolso

        'Variables
        Dim oEGCC_InsDesembolso As New EGCC_InsDesembolso
        Dim prmParameter(3) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGCC_InsDesembolso = CFunciones.DeserializeObject(Of EGCC_InsDesembolso)(pEInsDesembolso)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEGCC_InsDesembolso.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Documentos", DbType.String, 1000, oEGCC_InsDesembolso.Documentos, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_UsuarioRegistro", DbType.String, 12, oEGCC_InsDesembolso.UsuarioRegistro, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, oEGCC_InsDesembolso.Codinstrucciondesembolso, ParameterDirection.Input)

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        'Inicio IBK - AAE - Cambio SP para actualizar
        'obRequest.Command = "up_gcc_InsDesembolso_gst"
        obRequest.Command = "up_gcc_InsDesembolso_gst2"
        ' Fin IBK
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fstrCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckStr(oEGCC_InsDesembolso.Codinstrucciondesembolso)

    End Function

    ''' <summary>
    ''' Gestion de cambios de una InsertarInsDesembolsoAgrupacion
    ''' </summary>
    ''' <param name="pEInsDesembolsoAgrupacion">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function InsertarInsDesembolsoAgrupacion(ByVal pEInsDesembolsoAgrupacion As String) As Boolean Implements IInstruccionDesembolsoTx.InsertarInsDesembolsoAgrupacion

        'Variables
        Dim oEGCC_InsDesembolsoAgrupacion As New EGCC_InsDesembolsoAgrupacion
        Dim prmParameter(14) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGCC_InsDesembolsoAgrupacion = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoAgrupacion)(pEInsDesembolsoAgrupacion)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodAgrupacion", DbType.String, 2, oEGCC_InsDesembolsoAgrupacion.Codagrupacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 4, oEGCC_InsDesembolsoAgrupacion.Codproveedor, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodMonedaDocumento", DbType.String, 3, oEGCC_InsDesembolsoAgrupacion.Codmonedadocumento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodMonedaPago", DbType.String, 3, oEGCC_InsDesembolsoAgrupacion.Codmonedapago, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_CodTipoOperacion", DbType.String, 3, oEGCC_InsDesembolsoAgrupacion.Codtipooperacion, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_NumeroDocumento", DbType.String, 20, oEGCC_InsDesembolsoAgrupacion.Numerodocumento, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_CodInstruccionDesembolso", DbType.String, 8, oEGCC_InsDesembolsoAgrupacion.Codinstrucciondesembolso, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEGCC_InsDesembolsoAgrupacion.Codsolicitudcredito, ParameterDirection.Input)

        prmParameter(8) = New DAABRequest.Parameter("@piv_MontoRetencion", DbType.Decimal)
        prmParameter(8).Precision = 18
        prmParameter(8).Scale = 6
        prmParameter(8).Value = oEGCC_InsDesembolsoAgrupacion.Montoretencion

        prmParameter(9) = New DAABRequest.Parameter("@piv_MontoDetraccion", DbType.Decimal)
        prmParameter(9).Precision = 18
        prmParameter(9).Scale = 6
        prmParameter(9).Value = oEGCC_InsDesembolsoAgrupacion.Montodetraccion

        prmParameter(10) = New DAABRequest.Parameter("@piv_Monto4ta", DbType.Decimal)
        prmParameter(10).Precision = 18
        prmParameter(10).Scale = 6
        prmParameter(10).Value = oEGCC_InsDesembolsoAgrupacion.Monto4ta

        prmParameter(11) = New DAABRequest.Parameter("@piv_MontoTotalPago", DbType.Decimal)
        prmParameter(11).Precision = 18
        prmParameter(11).Scale = 6
        prmParameter(11).Value = oEGCC_InsDesembolsoAgrupacion.Montototalpago

        prmParameter(12) = New DAABRequest.Parameter("@piv_CodConceptoCargo", DbType.String, 3, oEGCC_InsDesembolsoAgrupacion.CodConceptoCargo, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_UsuarioRegistro", DbType.String, 12, oEGCC_InsDesembolsoAgrupacion.Audusuarioregistro, ParameterDirection.Input)

        prmParameter(14) = New DAABRequest.Parameter("@piv_PorcCalculo", DbType.Decimal)
        prmParameter(14).Precision = 18
        prmParameter(14).Scale = 6
        prmParameter(14).Value = oEGCC_InsDesembolsoAgrupacion.PorcCalculo


        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_InsDesembolsoAgrupacion_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fstrCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True

    End Function

    ''' <summary>
    ''' Insertar InsDesembolso Medio de Pago
    ''' </summary>
    ''' <param name="pEInsDesembolsoPago">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function InsertarInsDesembolsoMedioPago(ByVal pEInsDesembolsoPago As String) As Boolean Implements IInstruccionDesembolsoTx.InsertarInsDesembolsoMedioPago

        'Variables
        Dim oEGCC_InsDesembolsoPago As New EGCC_InsDesembolsoPago
        ' Inicio IBK - AAE
        ' comento código original
        'Dim prmParameter(22) As DAABRequest.Parameter
        Dim prmParameter(27) As DAABRequest.Parameter
        ' Fin IBK

        'Deserealiza la Entidad
        oEGCC_InsDesembolsoPago = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoPago)(pEInsDesembolsoPago)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodMedioAbono", DbType.String, 3, oEGCC_InsDesembolsoPago.Codmedioabono, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodMonedaCuenta", DbType.String, 3, oEGCC_InsDesembolsoPago.Codmonedacuenta, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodTipoCuenta", DbType.String, 2, oEGCC_InsDesembolsoPago.Codtipocuenta, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Numero_Cuenta", DbType.String, 13, oEGCC_InsDesembolsoPago.Numero_cuenta, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_NumCuentaInterbancaria", DbType.String, 20, oEGCC_InsDesembolsoPago.Numcuentainterbancaria, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Pendiente", DbType.String, 14, oEGCC_InsDesembolsoPago.Pendiente, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Nota", DbType.String, 8, oEGCC_InsDesembolsoPago.Nota, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_Emisora", DbType.String, 3, oEGCC_InsDesembolsoPago.Emisora, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_Receptora", DbType.String, 3, oEGCC_InsDesembolsoPago.Receptora, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_CodMonedaPago", DbType.String, 3, oEGCC_InsDesembolsoPago.Codmonedapago, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 4, oEGCC_InsDesembolsoPago.Codproveedor, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_CodMonedaPendiente", DbType.String, 3, oEGCC_InsDesembolsoPago.Codmonedapendiente, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_CodPagoComision", DbType.String, 3, oEGCC_InsDesembolsoPago.Codpagocomision, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_Comentario", DbType.String, 300, oEGCC_InsDesembolsoPago.Comentario, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@piv_Adjunto", DbType.String, 300, oEGCC_InsDesembolsoPago.Adjunto, ParameterDirection.Input)

        prmParameter(15) = New DAABRequest.Parameter("@piv_ImporteComision", DbType.Decimal)
        prmParameter(15).Precision = 18
        prmParameter(15).Scale = 6
        prmParameter(15).Value = oEGCC_InsDesembolsoPago.Importecomision

        prmParameter(16) = New DAABRequest.Parameter("@piv_CodTipoDocumento", DbType.String, 20, oEGCC_InsDesembolsoPago.Codtipodocumento, ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@piv_NumeroDocumento", DbType.String, 11, oEGCC_InsDesembolsoPago.Numerodocumento, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@piv_RazonSocial", DbType.String, 150, oEGCC_InsDesembolsoPago.Razonsocial, ParameterDirection.Input)

        prmParameter(19) = New DAABRequest.Parameter("@piv_CodInstruccionDesembolso", DbType.String, 8, oEGCC_InsDesembolsoPago.Codinstrucciondesembolso, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEGCC_InsDesembolsoPago.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@piv_CodAgrupacion", DbType.String, 3, oEGCC_InsDesembolsoPago.Codagrupacion, ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@piv_CodMonedaAgrupacion", DbType.String, 3, oEGCC_InsDesembolsoPago.Codmonedaagrupacion, ParameterDirection.Input)
        'Inicio IBK - AAE
        prmParameter(23) = New DAABRequest.Parameter("@piv_CodEstadoEjecucionPago", DbType.String, 100, oEGCC_InsDesembolsoPago.CodEstadoEjecucionPago, ParameterDirection.Input)
        prmParameter(24) = New DAABRequest.Parameter("@piv_CargoNoDom", DbType.Decimal)
        prmParameter(24).Precision = 18
        prmParameter(24).Scale = 6
        prmParameter(24).Value = oEGCC_InsDesembolsoPago.CargoNoDom
        prmParameter(25) = New DAABRequest.Parameter("@piv_AbonoNoDom", DbType.Decimal)
        prmParameter(25).Precision = 18
        prmParameter(25).Scale = 6
        prmParameter(25).Value = oEGCC_InsDesembolsoPago.AbonoNoDom
        prmParameter(26) = New DAABRequest.Parameter("@piv_CtaCargoNoDom", DbType.String, 13, oEGCC_InsDesembolsoPago.CtaCargoNoDom, ParameterDirection.Input)
        prmParameter(27) = New DAABRequest.Parameter("@piv_CtaAbonoNoDom", DbType.String, 100, oEGCC_InsDesembolsoPago.CtaAbonoNoDom, ParameterDirection.Input)
        ' Fin IBK
        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_InsDesembolsoFormaPago_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InsertarInsDesembolsoMedioPago", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pEDesembolsoAgrupacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EliminarInsDesembolsoAgrupacion(ByVal pEDesembolsoAgrupacion As String) As Boolean Implements IInstruccionDesembolsoTx.EliminarInsDesembolsoAgrupacion

        'Variables
        Dim oEGCC_InsDesembolsoAgrupacion As New EGCC_InsDesembolsoAgrupacion
        Dim prmParameter(5) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGCC_InsDesembolsoAgrupacion = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoAgrupacion)(pEDesembolsoAgrupacion)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEGCC_InsDesembolsoAgrupacion.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccionDesembolso", DbType.String, 8, oEGCC_InsDesembolsoAgrupacion.Codinstrucciondesembolso, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodTipoOperacion", DbType.String, 3, oEGCC_InsDesembolsoAgrupacion.Codtipooperacion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodAgrupacion", DbType.String, 2, oEGCC_InsDesembolsoAgrupacion.Codagrupacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 4, oEGCC_InsDesembolsoAgrupacion.Codproveedor, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_CodMonedaPago", DbType.String, 3, oEGCC_InsDesembolsoAgrupacion.Codmonedapago, ParameterDirection.Input)

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_InsDesembolsoAgrupacion_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fstrCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True

    End Function
    'Inicio IBK - AAE - Cambia la función para que obtenga los documentos, actualize los TC de los docs y luego llame a regenerar la ID
    Public Function recalcula(ByVal pEInsDesembolso As String) As Boolean Implements IInstruccionDesembolsoTx.recalcula

        'Variables
        'Dim oEGCC_InsDesembolso As New EGCC_InsDesembolso
        'Dim prmParameter(3) As DAABRequest.Parameter

        ''Deserealiza la Entidad
        'oEGCC_InsDesembolso = CFunciones.DeserializeObject(Of EGCC_InsDesembolso)(pEInsDesembolso)

        ''Cabecera
        'prmParameter(0) = New DAABRequest.Parameter("@piv_codInstruccionDesembolso", DbType.String, 8, oEGCC_InsDesembolso.Codinstrucciondesembolso, ParameterDirection.Input)
        'prmParameter(1) = New DAABRequest.Parameter("@piv_codsolicitudcredito", DbType.String, 8, oEGCC_InsDesembolso.Codsolicitudcredito, ParameterDirection.Input)
        'prmParameter(2) = New DAABRequest.Parameter("@piv_tcdia", DbType.String, 8, oEGCC_InsDesembolso.tcdia, ParameterDirection.Input)
        'prmParameter(3) = New DAABRequest.Parameter("@piv_nroticket", DbType.String, 8, "", ParameterDirection.Input)

        ''Prepara Ingreso
        'Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        'Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        ''Ejecuta Ingreso
        'obRequest.CommandType = CommandType.StoredProcedure
        'obRequest.Command = "up_gcc_InsDesembolsoTipoCambio_upd"
        'obRequest.Parameters.AddRange(prmParameter)

        Dim oEGCC_InsDesembolso As New EGCC_InsDesembolso
        Dim strDocs As String
        Dim nbrRes As Integer
        Dim strRes As String
        'Deserealiza la Entidad
        oEGCC_InsDesembolso = CFunciones.DeserializeObject(Of EGCC_InsDesembolso)(pEInsDesembolso)

        Try
            'Obtengo los documentos de la ID
            strDocs = obtenerCheckeados(oEGCC_InsDesembolso.Codsolicitudcredito, oEGCC_InsDesembolso.Codinstrucciondesembolso)

            'Actualizo los TC de los documentos
            nbrRes = actualizaTC(oEGCC_InsDesembolso.Codsolicitudcredito)
            If nbrRes = 0 Then
                'Regenero la Inst de desembolso con los TC Actualizados
                strRes = regeneraID(oEGCC_InsDesembolso.Codsolicitudcredito, oEGCC_InsDesembolso.Codinstrucciondesembolso, strDocs, oEGCC_InsDesembolso.UsuarioRegistro)
            Else
                Throw New Exception("No se pudo actualizar TC de documentos, resultado: " + nbrRes.ToString)
            End If


        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fstrCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        End Try

        Return True

    End Function

    ''' <summary>
    ''' Eliminar MedioPago
    ''' </summary>
    ''' <param name="pEInsDesembolsoPago">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function EliminarInsDesembolsoMedioPago(ByVal pEInsDesembolsoPago As String) As Boolean Implements IInstruccionDesembolsoTx.EliminarInsDesembolsoMedioPago

        'Variables
        Dim oEGCC_InsDesembolsoPago As New EGCC_InsDesembolsoPago
        Dim prmParameter(4) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGCC_InsDesembolsoPago = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoPago)(pEInsDesembolsoPago)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodInstruccionDesembolso", DbType.String, 8, oEGCC_InsDesembolsoPago.Codinstrucciondesembolso, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEGCC_InsDesembolsoPago.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 8, oEGCC_InsDesembolsoPago.Codproveedor, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodAgrupacion", DbType.String, 8, oEGCC_InsDesembolsoPago.Codagrupacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_CodMonedaAgrupacion", DbType.String, 8, oEGCC_InsDesembolsoPago.Codmonedaagrupacion, ParameterDirection.Input)

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_InsDesembolsoFormaPago_Del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fstrCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True

    End Function

    ''' <summary>
    ''' Actualizar Estado ID
    ''' </summary>
    ''' <param name="pEInsDesembolso">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function ActualizarInsDesembolsoEstado(ByVal pEInsDesembolso As String) As Boolean Implements IInstruccionDesembolsoTx.ActualizarInsDesembolsoEstado

        'Variables
        Dim oEGCC_InsDesembolso As New EGCC_InsDesembolso
        'Inicio IBK - AAE - Agrego flag LPC
        Dim prmParameter(3) As DAABRequest.Parameter
        'Fin IBK

        'Deserealiza la Entidad
        oEGCC_InsDesembolso = CFunciones.DeserializeObject(Of EGCC_InsDesembolso)(pEInsDesembolso)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEGCC_InsDesembolso.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, oEGCC_InsDesembolso.Codinstrucciondesembolso, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodEstado", DbType.String, 3, oEGCC_InsDesembolso.Codestadoinstruccion, ParameterDirection.Input)
        'Inicio IBK - AAE - Agrego flag LPC
        prmParameter(3) = New DAABRequest.Parameter("@piv_FlagLPC", DbType.Byte, 1, oEGCC_InsDesembolso.FlagLPC, ParameterDirection.Input)
        'Fin IBK


        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_InsDesembolsoEstado_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ActualizarInsDesembolsoEstado", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True

    End Function

    ''' <summary>
    ''' Ejecucion Instruccion
    ''' </summary>
    ''' <param name="pEInsDesembolso">Entidad serializada</param>
    ''' <returns>String con el número de ID</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function EjecutarInsDesembolsoEstado(ByVal pEInsDesembolso As String) As Boolean Implements IInstruccionDesembolsoTx.EjecutarInsDesembolsoEstado

        'Variables
        Dim oEGCC_InsDesembolso As New EGCC_InsDesembolso
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGCC_InsDesembolso = CFunciones.DeserializeObject(Of EGCC_InsDesembolso)(pEInsDesembolso)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEGCC_InsDesembolso.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, oEGCC_InsDesembolso.Codinstrucciondesembolso, ParameterDirection.Input)

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_InsDesembolsoEjecucion_gst"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "EjecutarInsDesembolsoEstado", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True

    End Function


    'Public Function EliminarInsDesembolsoAgrupacion(ByVal pEDesembolsoAgrupacion As String) As Boolean Implements IInstruccionDesembolsoTx.EliminarInsDesembolsoAgrupacion


    'Variables
    'Dim oEGCC_InsDesembolsoPago As New EGCC_InsDesembolsoPago
    'Dim prmParameter(4) As DAABRequest.Parameter

    ''Deserealiza la Entidad
    'oEGCC_InsDesembolsoPago = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoPago)(pEInsDesembolsoPago)

    ''Cabecera
    'prmParameter(0) = New DAABRequest.Parameter("@piv_CodInstruccionDesembolso", DbType.String, 8, oEGCC_InsDesembolsoPago.Codinstrucciondesembolso, ParameterDirection.Input)
    'prmParameter(1) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEGCC_InsDesembolsoPago.Codsolicitudcredito, ParameterDirection.Input)
    'prmParameter(2) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 8, oEGCC_InsDesembolsoPago.Codproveedor, ParameterDirection.Input)
    'prmParameter(3) = New DAABRequest.Parameter("@piv_CodAgrupacion", DbType.String, 8, oEGCC_InsDesembolsoPago.Codagrupacion, ParameterDirection.Input)
    'prmParameter(4) = New DAABRequest.Parameter("@piv_CodMonedaAgrupacion", DbType.String, 8, oEGCC_InsDesembolsoPago.Codmonedaagrupacion, ParameterDirection.Input)

    ''Prepara Ingreso
    'Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
    'Dim obRequest As DAABRequest = obj.CreaRequestSQL()

    ''Ejecuta Ingreso
    'obRequest.CommandType = CommandType.StoredProcedure
    'obRequest.Command = "up_gcc_InsDesembolsoFormaPago_Del"
    'obRequest.Parameters.AddRange(prmParameter)

    'Try
    '    obRequest.Factory.ExecuteNonQuery(obRequest)
    'Catch ex As Exception
    '    Dim oLog As New CLog
    '    Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fstrCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
    '    oLog = Nothing
    '    Throw ex
    'Finally
    '    obRequest.Factory.Dispose()
    'End Try

    'Return True

    ' End Function

    'Inicio IBK - AAE - Se agregan métodos
    'Actualiza el estado de ejecución de una medio de pago
    Public Function ActualizarEstadoejecucionInstruccionDesembolsoPago(ByVal pEInsDesembolsoPago As String) As Boolean Implements IInstruccionDesembolsoTx.ActualizarEstadoejecucionInstruccionDesembolsoPago

        'Variables
        Dim oEGCC_InsDesembolsoPago As New EGCC_InsDesembolsoPago
        Dim prmParameter(8) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGCC_InsDesembolsoPago = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoPago)(pEInsDesembolsoPago)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodInstruccionDesembolso", DbType.String, 8, oEGCC_InsDesembolsoPago.Codinstrucciondesembolso, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEGCC_InsDesembolsoPago.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 8, oEGCC_InsDesembolsoPago.Codproveedor, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodAgrupacion", DbType.String, 8, oEGCC_InsDesembolsoPago.Codagrupacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_CodMonedaAgrupacion", DbType.String, 8, oEGCC_InsDesembolsoPago.Codmonedaagrupacion, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_CodEstadoEjecucionPago", DbType.String, 100, oEGCC_InsDesembolsoPago.CodEstadoEjecucionPago, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_MonedaCargoAbono", DbType.String, 3, oEGCC_InsDesembolsoPago.CodMonedaCargoAbono, ParameterDirection.Input)

        prmParameter(7) = New DAABRequest.Parameter("@piv_MontoCargoAbono", DbType.Decimal)
        prmParameter(7).Precision = 18
        prmParameter(7).Scale = 6
        prmParameter(7).Value = oEGCC_InsDesembolsoPago.MontoCargoAbono

        prmParameter(8) = New DAABRequest.Parameter("@piv_montoIGVCargo", DbType.Decimal)
        prmParameter(8).Precision = 18
        prmParameter(8).Scale = 6
        prmParameter(8).Value = oEGCC_InsDesembolsoPago.MontoIGVCargo



        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_InsDesembolsoMedioPago_upd_estEjec"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fstrCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True

    End Function

    Public Function InsertarEjecucionDesembolsoPagoLog(ByVal pEInsDesembolsoPagoLog As String) As Boolean Implements IInstruccionDesembolsoTx.InsertarEjecucionDesembolsoPagoLog

        'Variables
        Dim oEGCC_InsDesembolsoPagoLog As New EGCC_LogDesembolsoPagoEjecucion
        Dim prmParameter(58) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGCC_InsDesembolsoPagoLog = CFunciones.DeserializeObject(Of EGCC_LogDesembolsoPagoEjecucion)(pEInsDesembolsoPagoLog)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@pic_CodSolicitudCredito", DbType.String, 8, oEGCC_InsDesembolsoPagoLog.CodSolicitudCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_CodInstruccionDesembolso", DbType.String, 8, oEGCC_InsDesembolsoPagoLog.CodInstruccionDesembolso, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_CodAgrupacion", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.CodAgrupacion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_CodMonedaAgrupacion", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.CodMonedaAgrupacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_CodProveedor", DbType.String, 20, oEGCC_InsDesembolsoPagoLog.CodProveedor, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_FechaHora", DbType.String, 25, oEGCC_InsDesembolsoPagoLog.FechaHora, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_FCDCODRET", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDCODRET, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pic_FCDCODTRAN", DbType.String, 4, oEGCC_InsDesembolsoPagoLog.FCDCODTRAN, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pic_FCDPROGRAMA", DbType.String, 8, oEGCC_InsDesembolsoPagoLog.FCDPROGRAMA, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pic_FCDUSUARIO", DbType.String, 8, oEGCC_InsDesembolsoPagoLog.FCDUSUARIO, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pic_FCDNUDOC", DbType.String, 10, oEGCC_InsDesembolsoPagoLog.FCDNUDOC, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@pic_FCDTRANCODE", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDTRANCODE, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pic_FCDFECPROSS", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDFECPROSS, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@pic_FCDFECPROYY", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDFECPROYY, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@pic_FCDFECPROMM", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDFECPROMM, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@pic_FCDFECPRODD", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDFECPRODD, ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@pic_FCDREGEMPLEADO", DbType.String, 8, oEGCC_InsDesembolsoPagoLog.FCDREGEMPLEADO, ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@pic_FCDTIENDAORIGEN", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDTIENDAORIGEN, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@pic_FCDTIPCTACR", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDTIPCTACR, ParameterDirection.Input)
        prmParameter(19) = New DAABRequest.Parameter("@pic_FCDCRCTL1", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDCRCTL1, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@pic_FCDCRCTL2", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDCRCTL2, ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@pic_FCDCRCTL3", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDCRCTL3, ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@pic_FCDCRCTL4", DbType.String, 4, oEGCC_InsDesembolsoPagoLog.FCDCRCTL4, ParameterDirection.Input)
        prmParameter(23) = New DAABRequest.Parameter("@pic_FCDCRACCT", DbType.String, 10, oEGCC_InsDesembolsoPagoLog.FCDCRACCT, ParameterDirection.Input)
        prmParameter(24) = New DAABRequest.Parameter("@pic_FCDCRFILL", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDCRFILL, ParameterDirection.Input)
        prmParameter(25) = New DAABRequest.Parameter("@pic_FCDTIPCTADB", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDTIPCTADB, ParameterDirection.Input)
        prmParameter(26) = New DAABRequest.Parameter("@pic_FCDDBCTL1", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDDBCTL1, ParameterDirection.Input)
        prmParameter(27) = New DAABRequest.Parameter("@pic_FCDDBCTL2", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDDBCTL2, ParameterDirection.Input)
        prmParameter(28) = New DAABRequest.Parameter("@pic_FCDDBCTL3", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDDBCTL3, ParameterDirection.Input)
        prmParameter(29) = New DAABRequest.Parameter("@pic_FCDDBCTL4", DbType.String, 4, oEGCC_InsDesembolsoPagoLog.FCDDBCTL4, ParameterDirection.Input)
        prmParameter(30) = New DAABRequest.Parameter("@pic_FCDDBACCT", DbType.String, 10, oEGCC_InsDesembolsoPagoLog.FCDDBACCT, ParameterDirection.Input)
        prmParameter(31) = New DAABRequest.Parameter("@pic_FCDDBFILL", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDDBFILL, ParameterDirection.Input)
        prmParameter(32) = New DAABRequest.Parameter("@pic_FCDEXTORNO", DbType.String, 1, oEGCC_InsDesembolsoPagoLog.FCDEXTORNO, ParameterDirection.Input)
        prmParameter(33) = New DAABRequest.Parameter("@pic_FCDSHORTDESC", DbType.String, 15, oEGCC_InsDesembolsoPagoLog.FCDSHORTDESC, ParameterDirection.Input)
        prmParameter(34) = New DAABRequest.Parameter("@pic_FCDAMOUNTCR", DbType.String, 15, oEGCC_InsDesembolsoPagoLog.FCDAMOUNTCR, ParameterDirection.Input)
        prmParameter(35) = New DAABRequest.Parameter("@pic_FCDAMOUNTDB", DbType.String, 15, oEGCC_InsDesembolsoPagoLog.FCDAMOUNTDB, ParameterDirection.Input)
        prmParameter(36) = New DAABRequest.Parameter("@pic_FCDCOBROFORZOSO", DbType.String, 1, oEGCC_InsDesembolsoPagoLog.FCDCOBROFORZOSO, ParameterDirection.Input)
        prmParameter(37) = New DAABRequest.Parameter("@pic_FCDCOBROPARCIAL", DbType.String, 1, oEGCC_InsDesembolsoPagoLog.FCDCOBROPARCIAL, ParameterDirection.Input)
        prmParameter(38) = New DAABRequest.Parameter("@pic_FCDCODUNI", DbType.String, 10, oEGCC_InsDesembolsoPagoLog.FCDCODUNI, ParameterDirection.Input)
        prmParameter(39) = New DAABRequest.Parameter("@pic_FCDCTAFLGOC", DbType.String, 1, oEGCC_InsDesembolsoPagoLog.FCDCTAFLGOC, ParameterDirection.Input)
        prmParameter(40) = New DAABRequest.Parameter("@pic_FCDCTAMONCF", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDCTAMONCF, ParameterDirection.Input)
        prmParameter(41) = New DAABRequest.Parameter("@pic_FCDCTACLATC", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDCTACLATC, ParameterDirection.Input)
        prmParameter(42) = New DAABRequest.Parameter("@pic_FCDCTATCCF", DbType.String, 15, oEGCC_InsDesembolsoPagoLog.FCDCTATCCF, ParameterDirection.Input)
        prmParameter(43) = New DAABRequest.Parameter("@pic_FCDCTAIMPEQUIV", DbType.String, 15, oEGCC_InsDesembolsoPagoLog.FCDCTAIMPEQUIV, ParameterDirection.Input)
        prmParameter(44) = New DAABRequest.Parameter("@pic_FILLERINP", DbType.String, 520, oEGCC_InsDesembolsoPagoLog.FILLERINP, ParameterDirection.Input)
        prmParameter(45) = New DAABRequest.Parameter("@pic_FCDLENGTHCOMMAREA", DbType.String, 5, oEGCC_InsDesembolsoPagoLog.FCDLENGTHCOMMAREA, ParameterDirection.Input)
        prmParameter(46) = New DAABRequest.Parameter("@pic_FCDCODRETTOLD", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDCODRETTOLD, ParameterDirection.Input)
        prmParameter(47) = New DAABRequest.Parameter("@pic_FCDCODRETO", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDCODRETO, ParameterDirection.Input)
        prmParameter(48) = New DAABRequest.Parameter("@pic_FCDMSGERROR", DbType.String, 40, oEGCC_InsDesembolsoPagoLog.FCDMSGERROR, ParameterDirection.Input)
        prmParameter(49) = New DAABRequest.Parameter("@pic_FCDFC04NRODOCUMENTO", DbType.String, 10, oEGCC_InsDesembolsoPagoLog.FCDFC04NRODOCUMENTO, ParameterDirection.Input)
        prmParameter(50) = New DAABRequest.Parameter("@pic_FCDFC04IMPORTEDESEMB", DbType.String, 15, oEGCC_InsDesembolsoPagoLog.FCDFC04IMPORTEDESEMB, ParameterDirection.Input)
        prmParameter(51) = New DAABRequest.Parameter("@pic_FCDFC04FLGEXTORNO", DbType.String, 1, oEGCC_InsDesembolsoPagoLog.FCDFC04FLGEXTORNO, ParameterDirection.Input)
        prmParameter(52) = New DAABRequest.Parameter("@pic_FILLEROUT", DbType.String, 198, oEGCC_InsDesembolsoPagoLog.FILLEROUT, ParameterDirection.Input)
        prmParameter(53) = New DAABRequest.Parameter("@pic_AudFechaRegistro", DbType.String, 25, oEGCC_InsDesembolsoPagoLog.AudFechaRegistro, ParameterDirection.Input)
        prmParameter(54) = New DAABRequest.Parameter("@pic_AudFechaModificacion", DbType.String, 25, oEGCC_InsDesembolsoPagoLog.AudFechaModificacion, ParameterDirection.Input)
        prmParameter(55) = New DAABRequest.Parameter("@pic_AudUsuarioRegistro", DbType.String, 12, oEGCC_InsDesembolsoPagoLog.AudUsuarioRegistro, ParameterDirection.Input)
        prmParameter(56) = New DAABRequest.Parameter("@pic_AudUsuarioModificacion", DbType.String, 12, oEGCC_InsDesembolsoPagoLog.AudUsuarioModificacion, ParameterDirection.Input)
        prmParameter(57) = New DAABRequest.Parameter("@pic_CodRetorno", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.CodRetorno, ParameterDirection.Input)
        prmParameter(58) = New DAABRequest.Parameter("@pic_Resultado", DbType.String, 255, oEGCC_InsDesembolsoPagoLog.Resultado, ParameterDirection.Input)

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Log_DesembolsoPagoEjecucion_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fstrCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True

    End Function

    Public Function ActualizarEjecucionDesembolsoPagoLog(ByVal pEInsDesembolsoPagoLog As String) As Boolean Implements IInstruccionDesembolsoTx.ActualizarEjecucionDesembolsoPagoLog

        'Variables
        Dim oEGCC_InsDesembolsoPagoLog As New EGCC_LogDesembolsoPagoEjecucion
        Dim prmParameter(58) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGCC_InsDesembolsoPagoLog = CFunciones.DeserializeObject(Of EGCC_LogDesembolsoPagoEjecucion)(pEInsDesembolsoPagoLog)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@pic_CodSolicitudCredito", DbType.String, 8, oEGCC_InsDesembolsoPagoLog.CodSolicitudCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_CodInstruccionDesembolso", DbType.String, 8, oEGCC_InsDesembolsoPagoLog.CodInstruccionDesembolso, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_CodAgrupacion", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.CodAgrupacion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_CodMonedaAgrupacion", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.CodMonedaAgrupacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_CodProveedor", DbType.String, 20, oEGCC_InsDesembolsoPagoLog.CodProveedor, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_FechaHora", DbType.String, 25, oEGCC_InsDesembolsoPagoLog.FechaHora, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_FCDCODRET", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDCODRET, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pic_FCDCODTRAN", DbType.String, 4, oEGCC_InsDesembolsoPagoLog.FCDCODTRAN, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pic_FCDPROGRAMA", DbType.String, 8, oEGCC_InsDesembolsoPagoLog.FCDPROGRAMA, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pic_FCDUSUARIO", DbType.String, 8, oEGCC_InsDesembolsoPagoLog.FCDUSUARIO, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pic_FCDNUDOC", DbType.String, 10, oEGCC_InsDesembolsoPagoLog.FCDNUDOC, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@pic_FCDTRANCODE", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDTRANCODE, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pic_FCDFECPROSS", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDFECPROSS, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@pic_FCDFECPROYY", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDFECPROYY, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@pic_FCDFECPROMM", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDFECPROMM, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@pic_FCDFECPRODD", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDFECPRODD, ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@pic_FCDREGEMPLEADO", DbType.String, 8, oEGCC_InsDesembolsoPagoLog.FCDREGEMPLEADO, ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@pic_FCDTIENDAORIGEN", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDTIENDAORIGEN, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@pic_FCDTIPCTACR", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDTIPCTACR, ParameterDirection.Input)
        prmParameter(19) = New DAABRequest.Parameter("@pic_FCDCRCTL1", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDCRCTL1, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@pic_FCDCRCTL2", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDCRCTL2, ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@pic_FCDCRCTL3", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDCRCTL3, ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@pic_FCDCRCTL4", DbType.String, 4, oEGCC_InsDesembolsoPagoLog.FCDCRCTL4, ParameterDirection.Input)
        prmParameter(23) = New DAABRequest.Parameter("@pic_FCDCRACCT", DbType.String, 10, oEGCC_InsDesembolsoPagoLog.FCDCRACCT, ParameterDirection.Input)
        prmParameter(24) = New DAABRequest.Parameter("@pic_FCDCRFILL", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDCRFILL, ParameterDirection.Input)
        prmParameter(25) = New DAABRequest.Parameter("@pic_FCDTIPCTADB", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDTIPCTADB, ParameterDirection.Input)
        prmParameter(26) = New DAABRequest.Parameter("@pic_FCDDBCTL1", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDDBCTL1, ParameterDirection.Input)
        prmParameter(27) = New DAABRequest.Parameter("@pic_FCDDBCTL2", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDDBCTL2, ParameterDirection.Input)
        prmParameter(28) = New DAABRequest.Parameter("@pic_FCDDBCTL3", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDDBCTL3, ParameterDirection.Input)
        prmParameter(29) = New DAABRequest.Parameter("@pic_FCDDBCTL4", DbType.String, 4, oEGCC_InsDesembolsoPagoLog.FCDDBCTL4, ParameterDirection.Input)
        prmParameter(30) = New DAABRequest.Parameter("@pic_FCDDBACCT", DbType.String, 10, oEGCC_InsDesembolsoPagoLog.FCDDBACCT, ParameterDirection.Input)
        prmParameter(31) = New DAABRequest.Parameter("@pic_FCDDBFILL", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDDBFILL, ParameterDirection.Input)
        prmParameter(32) = New DAABRequest.Parameter("@pic_FCDEXTORNO", DbType.String, 1, oEGCC_InsDesembolsoPagoLog.FCDEXTORNO, ParameterDirection.Input)
        prmParameter(33) = New DAABRequest.Parameter("@pic_FCDSHORTDESC", DbType.String, 15, oEGCC_InsDesembolsoPagoLog.FCDSHORTDESC, ParameterDirection.Input)
        prmParameter(34) = New DAABRequest.Parameter("@pic_FCDAMOUNTCR", DbType.String, 15, oEGCC_InsDesembolsoPagoLog.FCDAMOUNTCR, ParameterDirection.Input)
        prmParameter(35) = New DAABRequest.Parameter("@pic_FCDAMOUNTDB", DbType.String, 15, oEGCC_InsDesembolsoPagoLog.FCDAMOUNTDB, ParameterDirection.Input)
        prmParameter(36) = New DAABRequest.Parameter("@pic_FCDCOBROFORZOSO", DbType.String, 1, oEGCC_InsDesembolsoPagoLog.FCDCOBROFORZOSO, ParameterDirection.Input)
        prmParameter(37) = New DAABRequest.Parameter("@pic_FCDCOBROPARCIAL", DbType.String, 1, oEGCC_InsDesembolsoPagoLog.FCDCOBROPARCIAL, ParameterDirection.Input)
        prmParameter(38) = New DAABRequest.Parameter("@pic_FCDCODUNI", DbType.String, 10, oEGCC_InsDesembolsoPagoLog.FCDCODUNI, ParameterDirection.Input)
        prmParameter(39) = New DAABRequest.Parameter("@pic_FCDCTAFLGOC", DbType.String, 1, oEGCC_InsDesembolsoPagoLog.FCDCTAFLGOC, ParameterDirection.Input)
        prmParameter(40) = New DAABRequest.Parameter("@pic_FCDCTAMONCF", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.FCDCTAMONCF, ParameterDirection.Input)
        prmParameter(41) = New DAABRequest.Parameter("@pic_FCDCTACLATC", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDCTACLATC, ParameterDirection.Input)
        prmParameter(42) = New DAABRequest.Parameter("@pic_FCDCTATCCF", DbType.String, 15, oEGCC_InsDesembolsoPagoLog.FCDCTATCCF, ParameterDirection.Input)
        prmParameter(43) = New DAABRequest.Parameter("@pic_FCDCTAIMPEQUIV", DbType.String, 15, oEGCC_InsDesembolsoPagoLog.FCDCTAIMPEQUIV, ParameterDirection.Input)
        prmParameter(44) = New DAABRequest.Parameter("@pic_FILLERINP", DbType.String, 520, oEGCC_InsDesembolsoPagoLog.FILLERINP, ParameterDirection.Input)
        prmParameter(45) = New DAABRequest.Parameter("@pic_FCDLENGTHCOMMAREA", DbType.String, 5, oEGCC_InsDesembolsoPagoLog.FCDLENGTHCOMMAREA, ParameterDirection.Input)
        prmParameter(46) = New DAABRequest.Parameter("@pic_FCDCODRETTOLD", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDCODRETTOLD, ParameterDirection.Input)
        prmParameter(47) = New DAABRequest.Parameter("@pic_FCDCODRETO", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.FCDCODRETO, ParameterDirection.Input)
        prmParameter(48) = New DAABRequest.Parameter("@pic_FCDMSGERROR", DbType.String, 40, oEGCC_InsDesembolsoPagoLog.FCDMSGERROR, ParameterDirection.Input)
        prmParameter(49) = New DAABRequest.Parameter("@pic_FCDFC04NRODOCUMENTO", DbType.String, 10, oEGCC_InsDesembolsoPagoLog.FCDFC04NRODOCUMENTO, ParameterDirection.Input)
        prmParameter(50) = New DAABRequest.Parameter("@pic_FCDFC04IMPORTEDESEMB", DbType.String, 15, oEGCC_InsDesembolsoPagoLog.FCDFC04IMPORTEDESEMB, ParameterDirection.Input)
        prmParameter(51) = New DAABRequest.Parameter("@pic_FCDFC04FLGEXTORNO", DbType.String, 1, oEGCC_InsDesembolsoPagoLog.FCDFC04FLGEXTORNO, ParameterDirection.Input)
        prmParameter(52) = New DAABRequest.Parameter("@pic_FILLEROUT", DbType.String, 198, oEGCC_InsDesembolsoPagoLog.FILLEROUT, ParameterDirection.Input)
        prmParameter(53) = New DAABRequest.Parameter("@pic_AudFechaRegistro", DbType.String, 25, oEGCC_InsDesembolsoPagoLog.AudFechaRegistro, ParameterDirection.Input)
        prmParameter(54) = New DAABRequest.Parameter("@pic_AudFechaModificacion", DbType.String, 25, oEGCC_InsDesembolsoPagoLog.AudFechaModificacion, ParameterDirection.Input)
        prmParameter(55) = New DAABRequest.Parameter("@pic_AudUsuarioRegistro", DbType.String, 12, oEGCC_InsDesembolsoPagoLog.AudUsuarioRegistro, ParameterDirection.Input)
        prmParameter(56) = New DAABRequest.Parameter("@pic_AudUsuarioModificacion", DbType.String, 12, oEGCC_InsDesembolsoPagoLog.AudUsuarioModificacion, ParameterDirection.Input)
        prmParameter(57) = New DAABRequest.Parameter("@pic_CodRetorno", DbType.String, 2, oEGCC_InsDesembolsoPagoLog.CodRetorno, ParameterDirection.Input)
        prmParameter(58) = New DAABRequest.Parameter("@pic_Resultado", DbType.String, 255, oEGCC_InsDesembolsoPagoLog.Resultado, ParameterDirection.Input)

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Log_DesembolsoPagoEjecucion_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fstrCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True

    End Function

    Public Function EliminarEjecucionDesembolsoPagoLog(ByVal pEInsDesembolsoPagoLog As String) As Boolean Implements IInstruccionDesembolsoTx.EliminarEjecucionDesembolsoPagoLog

        'Variables
        Dim oEGCC_InsDesembolsoPagoLog As New EGCC_LogDesembolsoPagoEjecucion
        Dim prmParameter(5) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGCC_InsDesembolsoPagoLog = CFunciones.DeserializeObject(Of EGCC_LogDesembolsoPagoEjecucion)(pEInsDesembolsoPagoLog)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@pic_CodSolicitudCredito", DbType.String, 8, oEGCC_InsDesembolsoPagoLog.CodSolicitudCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_CodInstruccionDesembolso", DbType.String, 8, oEGCC_InsDesembolsoPagoLog.CodInstruccionDesembolso, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_CodAgrupacion", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.CodAgrupacion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_CodMonedaAgrupacion", DbType.String, 3, oEGCC_InsDesembolsoPagoLog.CodMonedaAgrupacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_CodProveedor", DbType.String, 20, oEGCC_InsDesembolsoPagoLog.CodProveedor, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_FechaHora", DbType.String, 25, oEGCC_InsDesembolsoPagoLog.FechaHora, ParameterDirection.Input)


        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Log_DesembolsoPagoEjecucion_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fstrCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True

    End Function

    Public Function EjecutarDesembolsoLPC(ByVal pFlag As String, ByVal pCodInstDesembolso As String, ByVal pRegUsuario As String) As Integer Implements IInstruccionDesembolsoTx.EjecutarDesembolsoLPC

        'Variables
        Dim prmParameter(3) As DAABRequest.Parameter
        Dim resultado As Integer = -1
        'Dim nbrFlag As Integer = Convert.ToInt16(pFlag)
        Dim strRes As String
        Dim parResultadoValidacion As IDataParameter
        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@argFlag", DbType.String, 1, pFlag, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@argCodInstruccionDesembolso", DbType.String, 8, pCodInstDesembolso, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@RegistroUsuarioEjecutor", DbType.String, 15, pRegUsuario, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@outVal", DbType.Int32, 0, ParameterDirection.InputOutput)


        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_InstruccionDesembolso_Ins"
        obRequest.Parameters.AddRange(prmParameter)
        'Inicio IBK - AAE - Se extiende el timeout a 1 minuto
        ' Fin IBK


        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            parResultadoValidacion = CType(obRequest.Parameters(3), IDataParameter)
            strRes = CFunciones.CheckStr(parResultadoValidacion.Value.ToString())
            resultado = Convert.ToInt16(strRes)
            'strRes = obRequest.Parameters("@outVal").ToString()
            'resultado = Convert.ToInt16(strRes)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fstrCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally

            obRequest.Factory.Dispose()
        End Try

        Return resultado

    End Function

    Function obtenerCheckeados(ByVal pCodContrato As String, ByVal pInsDesembolso As String) As String Implements IInstruccionDesembolsoTx.obtenerCheckeados
        'Variables
        Dim odtbListadoIDAgrupacion As DataTable
        Dim strCheckeado As String = ""
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, pCodContrato, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, pInsDesembolso, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_InsDesembolso_docs_checkeados"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoIDAgrupacion = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
            For Each dr As DataRow In odtbListadoIDAgrupacion.Rows
                strCheckeado = strCheckeado + dr("doc").ToString + ","
            Next
            strCheckeado = strCheckeado + "''"

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoInsDesembolsoAgrupacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return strCheckeado
    End Function

    Function actualizaTCID(ByVal pEInsDesembolso As String) As Boolean Implements IInstruccionDesembolsoTx.actualizaTCID

        'Variables
        Dim oEGCC_InsDesembolso As New EGCC_InsDesembolso
        Dim prmParameter(3) As DAABRequest.Parameter

        ''Deserealiza la Entidad
        oEGCC_InsDesembolso = CFunciones.DeserializeObject(Of EGCC_InsDesembolso)(pEInsDesembolso)

        ''Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_codInstruccionDesembolso", DbType.String, 8, oEGCC_InsDesembolso.Codinstrucciondesembolso, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_codsolicitudcredito", DbType.String, 8, oEGCC_InsDesembolso.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_tcdia", DbType.String, 8, oEGCC_InsDesembolso.tcdia, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_nroticket", DbType.String, 8, "", ParameterDirection.Input)

        ''Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_InsDesembolsoTipoCambio_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fstrCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True

    End Function


    Function actualizaTC(ByVal pCodContrato As String) As Integer Implements IInstruccionDesembolsoTx.actualizaTC

        Dim oESolCredEstructDoc As New ESolicitudcreditoestructuradoc
        Dim prmParameter(1) As DAABRequest.Parameter
        Dim parResultado As IDataParameter

        prmParameter(0) = New DAABRequest.Parameter("@argNroCreditoLpc", DbType.String, 8, pCodContrato, ParameterDirection.Input)
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

    Function regeneraID(ByVal pCodContrato As String, ByVal pCodInstDesembolso As String, ByVal pCheckeados As String, ByVal pUsuario As String) As String Implements IInstruccionDesembolsoTx.regeneraID
        'Variables
        Dim prmParameter(3) As DAABRequest.Parameter


        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, pCodContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Documentos", DbType.String, 1000, pCheckeados, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_UsuarioRegistro", DbType.String, 12, pUsuario, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, pCodInstDesembolso, ParameterDirection.Input)

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        'Inicio IBK - AAE - Cambio por la v2    
        'obRequest.Command = "up_gcc_InsDesembolso_gst"
        obRequest.Command = "up_gcc_InsDesembolso_gst2"
        ' Fin IBK
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fstrCotizacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckStr(pCodInstDesembolso)
    End Function


    Function AnularInstDesembolso(ByVal pEInstruccionDesembolsoDoc As String) As String Implements IInstruccionDesembolsoTx.AnularInstDesembolso

        'Variables
        Dim oEGCC_InsDesembolso As New EGCC_InsDesembolso
        Dim parResultado As IDataParameter
        Dim prmParameter(2) As DAABRequest.Parameter


        'Deserealiza la Entidad
        oEGCC_InsDesembolso = CFunciones.DeserializeObject(Of EGCC_InsDesembolso)(pEInstruccionDesembolsoDoc)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEGCC_InsDesembolso.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, oEGCC_InsDesembolso.Codinstrucciondesembolso, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@resultado", DbType.String, 18, "", ParameterDirection.Output)
        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_InsDesembolsoEstadoAnular_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ActualizarInsDesembolsoEstado", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parResultado = CType(obRequest.Parameters(2), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return parResultado.Value.ToString()
    End Function

    Function CheckRelacionesDocBienes(ByVal strCodContrato As String) As String Implements IInstruccionDesembolsoTx.CheckRelacionesDocBienes
        'Variables
        Dim parResultado As IDataParameter
        Dim prmParameter(1) As DAABRequest.Parameter



        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodsolicitudCredito", DbType.String, 8, strCodContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_res", DbType.String, 100, "", ParameterDirection.Output)
        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_desembolso_rel_doc_bien_check"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ActualizarInsDesembolsoEstado", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parResultado = CType(obRequest.Parameters(1), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return parResultado.Value.ToString()
    End Function

    Function LiberarInstDesembolso(ByVal pCodSolicitudCredito As String, ByVal pCodInstDesembolso As String) As Integer Implements IInstruccionDesembolsoTx.LiberarInstDesembolso
        'Variables
        Dim parResultado As IDataParameter
        Dim prmParameter(2) As DAABRequest.Parameter



        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@pCodSolicitudCredito", DbType.String, 8, pCodSolicitudCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pCodInstDesembolso", DbType.String, 8, pCodInstDesembolso, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_res", DbType.Int32, 100, "", ParameterDirection.Output)
        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_instDesembolso_liberar"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ActualizarInsDesembolsoEstado", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parResultado = CType(obRequest.Parameters(1), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckInt(parResultado.Value.ToString())
    End Function

    'Fin IBK
    'Inicio IBK - AAE - Activación leasing Parcial
    Function ActualizarInformacionActivacion(ByVal strEGCC_InsDesembolsoActivacion As String) As Boolean Implements IInstruccionDesembolsoTx.ActualizarInformacionActivacion
        'Variables
        Dim prmParameter(22) As DAABRequest.Parameter
        Dim oEGCC_InsDesembolsoActivacion As EGCC_InsDesembolsoActivacion
        'Deserealiza la Entidad
        oEGCC_InsDesembolsoActivacion = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoActivacion)(strEGCC_InsDesembolsoActivacion)

        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEGCC_InsDesembolsoActivacion.CodSolicitudCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_codInstruccionDesembolso", DbType.String, 8, oEGCC_InsDesembolsoActivacion.CodInstruccionDesembolso, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodigoTipoPersona", DbType.String, 100, oEGCC_InsDesembolsoActivacion.Codigotipopersona, ParameterDirection.Input)

        prmParameter(3) = New DAABRequest.Parameter("@piv_ValorVenta", DbType.Decimal)
        prmParameter(3).Precision = 18
        prmParameter(3).Scale = 6
        prmParameter(3).Value = oEGCC_InsDesembolsoActivacion.Valorventa
        prmParameter(4) = New DAABRequest.Parameter("@piv_ValorVentaIGV", DbType.Decimal)
        prmParameter(4).Precision = 18
        prmParameter(4).Scale = 6
        prmParameter(4).Value = oEGCC_InsDesembolsoActivacion.Valorventaigv
        prmParameter(5) = New DAABRequest.Parameter("@piv_PrecioVenta", DbType.Decimal)
        prmParameter(5).Precision = 18
        prmParameter(5).Scale = 6
        prmParameter(5).Value = oEGCC_InsDesembolsoActivacion.Precioventa
        prmParameter(6) = New DAABRequest.Parameter("@piv_RiesgoNeto", DbType.Decimal)
        prmParameter(6).Precision = 18
        prmParameter(6).Scale = 6
        prmParameter(6).Value = oEGCC_InsDesembolsoActivacion.Riesgoneto
        prmParameter(7) = New DAABRequest.Parameter("@piv_ImporteCuotaInicial", DbType.Decimal)
        prmParameter(7).Precision = 18
        prmParameter(7).Scale = 6
        prmParameter(7).Value = oEGCC_InsDesembolsoActivacion.Importecuotainicial
        prmParameter(8) = New DAABRequest.Parameter("@piv_TEAPorc", DbType.Decimal)
        prmParameter(8).Precision = 18
        prmParameter(8).Scale = 6
        prmParameter(8).Value = oEGCC_InsDesembolsoActivacion.Teaporc

        prmParameter(9) = New DAABRequest.Parameter("@piv_CodigoTipoCronograma", DbType.String, 100, oEGCC_InsDesembolsoActivacion.Codigotipocronograma, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_NumeroCuotas", DbType.UInt32, 0, oEGCC_InsDesembolsoActivacion.Numerocuotas, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_CodigoPeriodicidad", DbType.String, 100, oEGCC_InsDesembolsoActivacion.Codigoperiodicidad, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_CodigoFrecuenciaPago", DbType.String, 100, oEGCC_InsDesembolsoActivacion.Codigofrecuenciapago, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_PlazoGraciaCuota", DbType.UInt32, 0, oEGCC_InsDesembolsoActivacion.Plazograciacuota, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@piv_CodigoTipoGraciaCuota", DbType.String, 100, oEGCC_InsDesembolsoActivacion.Codigotipograciacuota, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@piv_FechaMaxActivacion", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEGCC_InsDesembolsoActivacion.Fechamaxactivacion), ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@piv_FechaOfertaValida", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEGCC_InsDesembolsoActivacion.Fechaprimervencimiento), ParameterDirection.Input)

        prmParameter(17) = New DAABRequest.Parameter("@piv_CodigoBienTipoSeguro", DbType.String, 100, oEGCC_InsDesembolsoActivacion.Codigobientiposeguro, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@piv_BienImportePrima", DbType.Decimal)
        prmParameter(18).Precision = 18
        prmParameter(18).Scale = 6
        prmParameter(18).Value = oEGCC_InsDesembolsoActivacion.Bienimporteprima
        prmParameter(19) = New DAABRequest.Parameter("@piv_BienNroCuotasFinanciar", DbType.UInt32, 0, oEGCC_InsDesembolsoActivacion.Biennrocuotasfinanciar, ParameterDirection.Input)

        prmParameter(20) = New DAABRequest.Parameter("@piv_CodigoDesgravamenTipoSeguro", DbType.String, 100, oEGCC_InsDesembolsoActivacion.Codigodesgravamentiposeguro, ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@piv_DesgravamenImportePrima", DbType.Decimal)
        prmParameter(21).Precision = 18
        prmParameter(21).Scale = 6
        prmParameter(21).Value = oEGCC_InsDesembolsoActivacion.Desgravamenimporteprima
        prmParameter(22) = New DAABRequest.Parameter("@piv_DesgravamenNroCuotasFinanciar", DbType.UInt32, 0, oEGCC_InsDesembolsoActivacion.Desgravamennrocuotasfinanciar, ParameterDirection.Input)

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_InstDesembolsoActivacion_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "up_gcc_InstDesembolsoActivacion_upd", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    Function EliminarCronogramaActivacion(ByVal pCodNroContrato As String, ByVal pCodInstruccionDesembolso As String) As Boolean Implements IInstruccionDesembolsoTx.EliminarCronogramaActivacion
        'Variables
        Dim prmParameter(1) As DAABRequest.Parameter



        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, pCodNroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_codInstruccionDesembolso", DbType.String, 8, pCodInstruccionDesembolso, ParameterDirection.Input)

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CronogramaActivacion_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "[up_gcc_CronogramaActivacion_del]", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    Function InsertarCronogramaActivacion(ByVal pCodNroContrato As String, ByVal pCodInstruccionDesembolso As String, ByVal pECotizacioncronograma As String) As Boolean Implements IInstruccionDesembolsoTx.InsertarCronogramaActivacion
        'Variables
        Dim oEGcc_cotizacioncronograma As New EGcc_cotizacioncronograma
        Dim prmParameter(22) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGcc_cotizacioncronograma = CFunciones.DeserializeObject(Of EGcc_cotizacioncronograma)(pECotizacioncronograma)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, pCodNroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_codInstruccionDesembolso", DbType.String, 8, pCodInstruccionDesembolso, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_VersionCronograma", DbType.String, 18, oEGcc_cotizacioncronograma.Versioncotizacion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_NumeroCuota", DbType.UInt32, 0, oEGcc_cotizacioncronograma.Numerocuota, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pid_FechaVencimiento", DbType.String, 10, oEGcc_cotizacioncronograma.SFechavencimiento, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_CantDiasCuota", DbType.String, 8, oEGcc_cotizacioncronograma.Cantdiascuota, ParameterDirection.Input)


        prmParameter(6) = New DAABRequest.Parameter("@piv_MontoSaldoAdeudado", DbType.Decimal)
        prmParameter(6).Precision = 18
        prmParameter(6).Scale = 6
        prmParameter(6).Value = oEGcc_cotizacioncronograma.Montosaldoadeudado
        prmParameter(7) = New DAABRequest.Parameter("@piv_MontoInteresBien", DbType.Decimal)
        prmParameter(7).Precision = 18
        prmParameter(7).Scale = 6
        prmParameter(7).Value = oEGcc_cotizacioncronograma.Montointeresbien
        prmParameter(8) = New DAABRequest.Parameter("@piv_MontoPrincipalBien", DbType.Decimal)
        prmParameter(8).Precision = 18
        prmParameter(8).Scale = 6
        prmParameter(8).Value = oEGcc_cotizacioncronograma.Montoprincipalbien
        prmParameter(9) = New DAABRequest.Parameter("@piv_MontoTotalCuota", DbType.Decimal)
        prmParameter(9).Precision = 18
        prmParameter(9).Scale = 6
        prmParameter(9).Value = oEGcc_cotizacioncronograma.Montototalcuota
        prmParameter(10) = New DAABRequest.Parameter("@piv_SaldoSeguro", DbType.Decimal)
        prmParameter(10).Precision = 18
        prmParameter(10).Scale = 6
        prmParameter(10).Value = oEGcc_cotizacioncronograma.Saldoseguro
        prmParameter(11) = New DAABRequest.Parameter("@piv_InteresSeguroBien", DbType.Decimal)
        prmParameter(11).Precision = 18
        prmParameter(11).Scale = 6
        prmParameter(11).Value = oEGcc_cotizacioncronograma.Interessegurobien
        prmParameter(12) = New DAABRequest.Parameter("@piv_PrincipalSeguroBien", DbType.Decimal)
        prmParameter(12).Precision = 18
        prmParameter(12).Scale = 6
        prmParameter(12).Value = oEGcc_cotizacioncronograma.Principalsegurobien
        prmParameter(13) = New DAABRequest.Parameter("@piv_MontoCuotaSeguroBien", DbType.Decimal)
        prmParameter(13).Precision = 18
        prmParameter(13).Scale = 6
        prmParameter(13).Value = oEGcc_cotizacioncronograma.Montocuotasegurobien



        prmParameter(14) = New DAABRequest.Parameter("@piv_SaldoSeguroDes", DbType.Decimal)
        prmParameter(14).Precision = 18
        prmParameter(14).Scale = 6
        prmParameter(14).Value = oEGcc_cotizacioncronograma.SaldoSeguroDes
        prmParameter(15) = New DAABRequest.Parameter("@piv_InteresSeguroDes", DbType.Decimal)
        prmParameter(15).Precision = 18
        prmParameter(15).Scale = 6
        prmParameter(15).Value = oEGcc_cotizacioncronograma.InteresSeguroDes
        prmParameter(16) = New DAABRequest.Parameter("@piv_PrincipalSeguroDes", DbType.Decimal)
        prmParameter(16).Precision = 18
        prmParameter(16).Scale = 6
        prmParameter(16).Value = oEGcc_cotizacioncronograma.PrincipalSeguroDes
        prmParameter(17) = New DAABRequest.Parameter("@piv_CuotaSeguroDes", DbType.Decimal)
        prmParameter(17).Precision = 18
        prmParameter(17).Scale = 6
        prmParameter(17).Value = oEGcc_cotizacioncronograma.CuotaSeguroDes



        prmParameter(18) = New DAABRequest.Parameter("@piv_MontoTotalCuotaIGV", DbType.Decimal)
        prmParameter(18).Precision = 18
        prmParameter(18).Scale = 6
        prmParameter(18).Value = oEGcc_cotizacioncronograma.Montototalcuotaigv
        prmParameter(19) = New DAABRequest.Parameter("@piv_TotalAPagar", DbType.Decimal)
        prmParameter(19).Precision = 18
        prmParameter(19).Scale = 6
        prmParameter(19).Value = oEGcc_cotizacioncronograma.Totalapagar



        prmParameter(20) = New DAABRequest.Parameter("@piv_AudEstadoLogico", DbType.UInt32, 8, oEGcc_cotizacioncronograma.Audestadologico, ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@piv_AudUsuarioRegistro", DbType.String, 8, oEGcc_cotizacioncronograma.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@piv_AudUsuarioModificacion", DbType.String, 8, oEGcc_cotizacioncronograma.Audusuariomodificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CronogramaActivacion_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "up_gcc_CronogramaActivacion_ins", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    Function ActualizarNroWIOActivacionParcial(ByVal pCodNroContrato As String, ByVal pNroInstruccionWIO As String) As Boolean Implements IInstruccionDesembolsoTx.ActualizarNroWIOActivacionParcial
        'Variables
        Dim prmParameter(1) As DAABRequest.Parameter



        'Cabecera
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, pCodNroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_NroInstruccionWIO", DbType.String, 18, pNroInstruccionWIO, ParameterDirection.Input)

        'Prepara Ingreso
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        'Ejecuta Ingreso
        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_InstDesembolsoActPar_NroWIO_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "[up_gcc_InstDesembolsoActPar_NroWIO_upd]", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function


    'Fin IBK

#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DInstruccionDesembolsoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 25/09/2012
''' </remarks>
<Guid("4EA0B54E-4192-4048-875D-002517A60F00") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DInstruccionDesembolsoNTx")> _
Public Class DInstruccionDesembolsoNTx
    Inherits ServicedComponent
    Implements IInstruccionDesembolsoNTx

#Region "constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DInstruccionDesembolsoNTx"

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Lista las Instruccion de Desembolso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function ListadoInsDesembolso(ByVal pPageSize As Integer, _
                                        ByVal pCurrentPage As Integer, _
                                        ByVal pSortColumn As String, _
                                        ByVal pSortOrder As String, _
                                        ByVal pEInsDesembolso As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolso

        'Variables
        Dim odtbListadoID As DataTable
        Dim oEGCC_InsDesembolso As New EGCC_InsDesembolso
        'Inicio IBK - AAE - Agrego NroWIO
        'Dim prmParameter(12) As DAABRequest.Parameter
        Dim prmParameter(13) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEGCC_InsDesembolso = CFunciones.DeserializeObject(Of EGCC_InsDesembolso)(pEInsDesembolso)

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEGCC_InsDesembolso.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 10, oEGCC_InsDesembolso.CUCliente, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_NombreCliente", DbType.String, 100, oEGCC_InsDesembolso.RazonSocial, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, oEGCC_InsDesembolso.Codinstrucciondesembolso, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@piv_FechaInicio", DbType.String, 8, oEGCC_InsDesembolso.FechaInicio, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@piv_FechaFin", DbType.String, 8, oEGCC_InsDesembolso.FechaFin, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@piv_TipoContrato", DbType.String, 3, oEGCC_InsDesembolso.TipoContrato, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@piv_CodigoEstado", DbType.String, 3, oEGCC_InsDesembolso.CodigoEstado, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@piv_CodigoMoneda", DbType.String, 3, oEGCC_InsDesembolso.CodigoMoneda, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@piv_NroWIO", DbType.String, 18, oEGCC_InsDesembolso.NroWIO, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_InsDesembolso_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoID = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoInsDesembolso", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoID)
    End Function

    ''' <summary>
    ''' Lista las Instruccion de Desembolso Agrupacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function ListadoInsDesembolsoAgrupacion(ByVal pEInsDesembolsoAgrupacion As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolsoAgrupacion

        'Variables
        Dim odtbListadoIDAgrupacion As DataTable
        Dim oEGCC_InsDesembolsoAgrupacion As New EGCC_InsDesembolsoAgrupacion
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEGCC_InsDesembolsoAgrupacion = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoAgrupacion)(pEInsDesembolsoAgrupacion)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEGCC_InsDesembolsoAgrupacion.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, oEGCC_InsDesembolsoAgrupacion.Codinstrucciondesembolso, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_InsDesembolsoAgrupacion_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoIDAgrupacion = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoInsDesembolsoAgrupacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoIDAgrupacion)
    End Function

    ''' <summary>
    ''' Lista las Documentos por agrupacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function ListadoInsDesembolsoDocAgrupacion(ByVal pEInsDesembolsoAgrupacion As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolsoDocAgrupacion

        'Variables
        Dim odtbListadoIDAgrupacion As DataTable
        Dim oEGCC_InsDesembolsoAgrupacion As New EGCC_InsDesembolsoAgrupacion
        Dim prmParameter(2) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEGCC_InsDesembolsoAgrupacion = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoAgrupacion)(pEInsDesembolsoAgrupacion)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEGCC_InsDesembolsoAgrupacion.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, oEGCC_InsDesembolsoAgrupacion.Codinstrucciondesembolso, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_CodAgrupacion", DbType.Int32, 0, oEGCC_InsDesembolsoAgrupacion.Codcorrelativo, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_InsDesembolsoDocAgrupacion_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoIDAgrupacion = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoInsDesembolsoDocAgrupacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoIDAgrupacion)
    End Function

    ''' <summary>
    ''' Lista las Instruccion de Desembolso CargoAbono
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function ListadoInsDesembolsoCargoAbono(ByVal pEInsDesembolsoAgrupacion As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolsoCargoAbono

        'Variables
        Dim odtbListadoIDAgrupacion As DataTable
        Dim oEGCC_InsDesembolsoAgrupacion As New EGCC_InsDesembolsoAgrupacion
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEGCC_InsDesembolsoAgrupacion = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoAgrupacion)(pEInsDesembolsoAgrupacion)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEGCC_InsDesembolsoAgrupacion.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, oEGCC_InsDesembolsoAgrupacion.Codinstrucciondesembolso, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_InsDesembolsoCargoAbono_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoIDAgrupacion = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoInsDesembolsoAgrupacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoIDAgrupacion)

    End Function

    ''' <summary>
    ''' Lista las Instruccion de Desembolso Medios Pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function ListadoInsDesembolsoMedioPago(ByVal pEInsDesembolsoPago As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolsoMedioPago

        'Variables
        Dim odtbListadoIDMedioPago As DataTable
        Dim oEGCC_InsDesembolsoPago As New EGCC_InsDesembolsoPago
        Dim prmParameter(4) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEGCC_InsDesembolsoPago = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoPago)(pEInsDesembolsoPago)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEGCC_InsDesembolsoPago.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, oEGCC_InsDesembolsoPago.Codinstrucciondesembolso, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_CodAgrupacion", DbType.String, 2, oEGCC_InsDesembolsoPago.Codagrupacion, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 4, oEGCC_InsDesembolsoPago.Codproveedor, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_CodMonedaAgrupacion", DbType.String, 3, oEGCC_InsDesembolsoPago.Codmonedaagrupacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_InsDesembolsoMedioPago_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoIDMedioPago = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoInsDesembolsoMedioPago", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoIDMedioPago)
    End Function
    Public Function ListadoInsDesembolsoMedioPagoGet(ByVal pEInsDesembolsoPago As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolsoMedioPagoGet

        'Variables
        Dim odtbListadoIDMedioPago As DataTable
        Dim oEGCC_InsDesembolsoPago As New EGCC_InsDesembolsoPago
        Dim prmParameter(4) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEGCC_InsDesembolsoPago = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoPago)(pEInsDesembolsoPago)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEGCC_InsDesembolsoPago.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, oEGCC_InsDesembolsoPago.Codinstrucciondesembolso, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_CodAgrupacion", DbType.String, 2, oEGCC_InsDesembolsoPago.Codagrupacion, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 4, oEGCC_InsDesembolsoPago.Codproveedor, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_CodMonedaAgrupacion", DbType.String, 3, oEGCC_InsDesembolsoPago.Codmonedaagrupacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_InsDesembolsoMedioPago_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoIDMedioPago = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoInsDesembolsoMedioPagoGet", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoIDMedioPago)
    End Function

    ''' <summary>
    ''' Obtener Instruccion de Desembolso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/09/2012
    ''' </remarks>
    Public Function ObtenerInsDesembolso(ByVal pEInsDesembolso As String) As String Implements IInstruccionDesembolsoNTx.ObtenerInsDesembolso

        'Variables
        Dim odtbListadoID As DataTable
        Dim oEGCC_InsDesembolso As New EGCC_InsDesembolso
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEGCC_InsDesembolso = CFunciones.DeserializeObject(Of EGCC_InsDesembolso)(pEInsDesembolso)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEGCC_InsDesembolso.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, oEGCC_InsDesembolso.Codinstrucciondesembolso, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_InsDesembolso_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoID = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerInsDesembolso", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoID)
    End Function


    Public Function ValidaEjecucionInstruccion(ByVal strEGInstruccionDesembolso As String) As String Implements IInstruccionDesembolsoNTx.ValidaEjecucionInstruccion

        'Variables
        'Dim odtbListadoID As DataTable
        Dim parResultadoValidacion As IDataParameter
        Dim odtbListadoID As String
        Dim oEGCC_InsDesembolso As New EGCC_InsDesembolso
        Dim prmParameter(2) As DAABRequest.Parameter


        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        'Dim  As String
        'Deserealiza la Entidad
        oEGCC_InsDesembolso = CFunciones.DeserializeObject(Of EGCC_InsDesembolso)(strEGInstruccionDesembolso)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodInstruccionDesembolso", DbType.String, 8, oEGCC_InsDesembolso.Codinstrucciondesembolso, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEGCC_InsDesembolso.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@pov_resultado", DbType.String, 150, "", ParameterDirection.InputOutput)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_insDesembolsoValida_wio"
            objRequest.Parameters.AddRange(prmParameter)
            'odtbListadoID = objRequest.Factory.Execute(objRequest).Tables(0)
            'odtbListadoID = objRequest.Factory.ExecuteScalar(objRequest)
            objRequest.Factory.ExecuteNonQuery(objRequest)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ValidaEjecucionInstruccion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            parResultadoValidacion = CType(objRequest.Parameters(2), IDataParameter)
            objRequest.Factory.Dispose()


        End Try
        'Return CFunciones.SerializeObject(Of DataTable)(odtbListadoID)
        Return CFunciones.CheckStr(parResultadoValidacion.Value.ToString())


    End Function

    'Inicio IBK - AAE - Se agregan los métodos
    Public Function getCargosCuentaInsDes(ByVal pESolicitudcredito As String, ByVal pEInsDesembolso As String) As String Implements IInstruccionDesembolsoNTx.getCargosCuentaInsDes
        'Variables
        Dim odtbListadoID As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, pESolicitudcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, pEInsDesembolso, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_InsDesembolso_CargosCtas"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoID = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerInsDesembolso", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoID)
    End Function

    Public Function ListadoInsDesembolsoMedioPago2(ByVal pEInsDesembolsoPago As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolsoMedioPago2

        'Variables
        Dim odtbListadoIDMedioPago As DataTable
        Dim oEGCC_InsDesembolsoPago As New EGCC_InsDesembolsoPago
        Dim prmParameter(4) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEGCC_InsDesembolsoPago = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoPago)(pEInsDesembolsoPago)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEGCC_InsDesembolsoPago.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, oEGCC_InsDesembolsoPago.Codinstrucciondesembolso, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_CodAgrupacion", DbType.String, 2, oEGCC_InsDesembolsoPago.Codagrupacion, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 4, oEGCC_InsDesembolsoPago.Codproveedor, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_CodMonedaAgrupacion", DbType.String, 3, oEGCC_InsDesembolsoPago.Codmonedaagrupacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_InsDesembolsoMedioPago_sel2"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoIDMedioPago = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoInsDesembolsoMedioPago", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoIDMedioPago)
    End Function

    Function obtenerInfoProveedor(ByVal pCodProv As String) As String Implements IInstruccionDesembolsoNTx.obtenerInfoProveedor
        'Variables
        Dim odtbListadoIDMedioPago As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 8, pCodProv, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_getDatosProv"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoIDMedioPago = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "obtenerProveedor", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoIDMedioPago)
    End Function

    Function ListadoInsDesembolsoTotales(ByVal pCodContrato As String, ByVal pInsDesembolsoPago As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolsoTotales
        'Variables
        Dim odtbListadoIDMedioPago As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, pCodContrato, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, pInsDesembolsoPago, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            'objRequest.Command = "up_gcc_InsDesembolso_sel_totales"
            objRequest.Command = "up_gcc_InsDesembolso_sel_totales2"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoIDMedioPago = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "obtenerProveedor", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoIDMedioPago)
    End Function

    Function obtenerWIO(ByVal pCodContrato As String, ByVal pInsDesembolsoPago As String) As String Implements IInstruccionDesembolsoNTx.obtenerWIO
        'Variables
        Dim odtbListadoIDMedioPago As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, pCodContrato, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, pInsDesembolsoPago, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_InsDesembolso_get_WIO"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoIDMedioPago = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "obtenerProveedor", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoIDMedioPago)
    End Function

    Function obtenerContablesMedioPago(ByVal strCodMedioPago As String) As String Implements IInstruccionDesembolsoNTx.obtenerContablesMedioPago
        'Variables
        Dim odtbListadoIDMedioPago As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_codMedioPago", DbType.String, 8, strCodMedioPago, ParameterDirection.Input)



            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_obtenerContablesMedioPago_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoIDMedioPago = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "obtenerContablemediopago", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoIDMedioPago)
    End Function

    Function obtenerContablesSUNAT(ByVal strCodTipoAgrupacion As String) As String Implements IInstruccionDesembolsoNTx.obtenerContablesSUNAT
        'Variables
        Dim odtbListadoIDMedioPago As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_codTipoAgrupacion", DbType.String, 8, strCodTipoAgrupacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_obtenerContablesAgrupacion_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoIDMedioPago = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "obtenerContablesSUNAT", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoIDMedioPago)
    End Function

    Function TieneNotasCredito(ByVal pstrCodSolicitudCredito As String, ByVal pstrCodInstruccionDesembolso As String, ByVal pstrCodAgrupacion As String, ByVal pstrCodCorrelativo As String) As Boolean Implements IInstruccionDesembolsoNTx.TieneNotasCredito
        'Variables

        Dim parResultadoValidacion As IDataParameter
        Dim prmParameter(4) As DAABRequest.Parameter
        Dim blnRes As Boolean = False
        Dim strRes As String = ""
        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, pstrCodSolicitudCredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, pstrCodInstruccionDesembolso, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_CodAgrupacion", DbType.String, 2, pstrCodAgrupacion, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_CodCorrelativo", DbType.String, pstrCodCorrelativo.Length, pstrCodCorrelativo, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@resultado", DbType.String, 1, "", ParameterDirection.InputOutput)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_INSDESEMBOLSO_AGRP_TIENE_NCRED"
            objRequest.Parameters.AddRange(prmParameter)
            'odtbListadoID = objRequest.Factory.Execute(objRequest).Tables(0)
            'odtbListadoID = objRequest.Factory.ExecuteScalar(objRequest)
            objRequest.Factory.ExecuteNonQuery(objRequest)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "UP_GCC_INSDESEMBOLSO_AGRP_TIENE_NCRED", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            parResultadoValidacion = CType(objRequest.Parameters(4), IDataParameter)
            objRequest.Factory.Dispose()


        End Try
        'Return CFunciones.SerializeObject(Of DataTable)(odtbListadoID)
        strRes = CFunciones.CheckStr(parResultadoValidacion.Value.ToString())
        If strRes.Trim = "0" Then
            blnRes = False
        Else
            blnRes = True
        End If

        Return blnRes
    End Function


    'Fin IBK
    ' Inicio - IBK - AAE - Activación Leasing Parcial
    Public Function ListadoInsDesembolsoActParcial(ByVal pEInsDesembolsoPago As String) As String Implements IInstruccionDesembolsoNTx.ListadoInsDesembolsoActParcial

        'Variables
        Dim odtbListadoIDMedioPago As DataTable
        Dim oEGCC_InsDesembolsoPago As New EGCC_InsDesembolso
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEGCC_InsDesembolsoPago = CFunciones.DeserializeObject(Of EGCC_InsDesembolso)(pEInsDesembolsoPago)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, oEGCC_InsDesembolsoPago.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodInstruccion", DbType.String, 8, oEGCC_InsDesembolsoPago.Codinstrucciondesembolso, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_InsDesembolsoAct_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoIDMedioPago = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoInsDesembolsoAct", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoIDMedioPago)
    End Function

    Function ListaDesembolsos(ByVal pPageSize As Integer, _
                                      ByVal pCurrentPage As Integer, _
                                      ByVal pSortColumn As String, _
                                      ByVal pSortOrder As String, _
                                      ByVal pstrNroContrato As String, _
                                      ByVal pstrCodInstDesembolso As String) As String Implements IInstruccionDesembolsoNTx.ListaDesembolsos

        'Variables
        Dim odtbListadoID As DataTable
        Dim prmParameter(5) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()



        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_CodContrato", DbType.String, 8, pstrNroContrato, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_CodInstDesembolso", DbType.String, 8, pstrCodInstDesembolso, ParameterDirection.Input)



            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ListaDesembolso_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoID = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListaDesembolso", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoID)

    End Function

    Function CronogramaActivacionGet(ByVal pstrNroContrato As String, _
                                                      ByVal pstrInstruccionDesembolso As String _
                                                      ) As String Implements IInstruccionDesembolsoNTx.CronogramaActivacionGet

        'Variables
        Dim odtbListadoID As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()



        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pstrNroContrato", DbType.String, 8, pstrNroContrato, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pstrInstruccionDesembolso", DbType.String, 8, pstrInstruccionDesembolso, ParameterDirection.Input)



            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CronogramaActivacion_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoID = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListaDesembolso", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoID)

    End Function

    Public Function ValidaEjecucionInstruccionActParcial(ByVal strEGInstruccionDesembolso As String, ByVal strFlagCheckPrecuota As String) As String Implements IInstruccionDesembolsoNTx.ValidaEjecucionInstruccionActParcial

        'Variables
        'Dim odtbListadoID As DataTable
        Dim parResultadoValidacion As IDataParameter

        Dim oEGCC_InsDesembolso As New EGCC_InsDesembolso
        Dim prmParameter(3) As DAABRequest.Parameter


        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        'Dim  As String
        'Deserealiza la Entidad
        oEGCC_InsDesembolso = CFunciones.DeserializeObject(Of EGCC_InsDesembolso)(strEGInstruccionDesembolso)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodsolicitudCredito", DbType.String, 8, oEGCC_InsDesembolso.Codsolicitudcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_InstDesembolso", DbType.String, 8, oEGCC_InsDesembolso.Codinstrucciondesembolso, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_flagCheckPrecuota", DbType.String, 1, strFlagCheckPrecuota, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_res", DbType.String, 100, "", ParameterDirection.InputOutput)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_InsDesembolsoActivacion_chk"
            objRequest.Parameters.AddRange(prmParameter)
            'odtbListadoID = objRequest.Factory.Execute(objRequest).Tables(0)
            'odtbListadoID = objRequest.Factory.ExecuteScalar(objRequest)
            objRequest.Factory.ExecuteNonQuery(objRequest)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ValidaEjecucionInstruccionActPar", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            parResultadoValidacion = CType(objRequest.Parameters(3), IDataParameter)
            objRequest.Factory.Dispose()


        End Try
        'Return CFunciones.SerializeObject(Of DataTable)(odtbListadoID)
        Return CFunciones.CheckStr(parResultadoValidacion.Value.ToString())


    End Function

    Function ReporteLeasingEnProceso(ByVal FecDesIni As String, ByVal FecDesFin As String, ByVal Moneda As String, ByVal CodSolCredito As String, ByVal Flag As String) As String Implements IInstruccionDesembolsoNTx.ReporteLeasingEnProceso

        'Variables
        Dim odtbListadoID As DataTable
        Dim prmParameter(4) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()



        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@FecDesIni", DbType.String, 8, FecDesIni, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@FecDesFin", DbType.String, 8, FecDesFin, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@sMoneda", DbType.String, 3, Moneda, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@Solicitud", DbType.String, 8, CodSolCredito, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@Flag", DbType.String, 1, Flag, ParameterDirection.Input)



            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_CO_SEL_RptDesembolsosParciales"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoID = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "UP_CO_SEL_RptDesembolsosParciales", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoID)

    End Function

    'Fin IBK

    ''' <summary>
    ''' Lista todos los desembolsos mensuales
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <param name="pFechaTermino">Fecha de término de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 22/01/2013 05:41:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoDesembolsoMensualReporte(ByVal pFechaInicio As String, _
                                                        ByVal pFechaTermino As String) As String Implements IInstruccionDesembolsoNTx.fobjListadoDesembolsoMensualReporte
        Dim prmParameter(1) As DAABRequest.Parameter
        Dim odtbListado As DataTable = Nothing

        'Campos para la consulta.
        prmParameter(0) = New DAABRequest.Parameter("@piv_FechaInicio", DbType.String, 10, pFechaInicio, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_FechaTermino", DbType.String, 10, pFechaTermino, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DesembolsoMensualReporte_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjListadoDesembolsoMensualReporte", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function
    Public Function ListaAgrupacionVoucher(ByVal pstrNroContrato As String, ByVal pstrNroInstruccion As String, ByVal pstrCorrelativo As String) As String Implements IInstruccionDesembolsoNTx.ListaAgrupacionVoucher
        Dim odtbListado As DataSet
        Dim prmParameter(2) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL    

            prmParameter(0) = New DAABRequest.Parameter("@piv_codsolicitudCredito", DbType.String, 8, pstrNroContrato, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_codInstruccionDesembolso", DbType.String, 8, pstrNroInstruccion, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_CodigoCorrelativo", DbType.Int32, 4, pstrCorrelativo, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_ReporteVoucher_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest)

            Return CFunciones.SerializeObject(Of DataSet)(odtbListado)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Retornar", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function
#End Region

End Class

#End Region
