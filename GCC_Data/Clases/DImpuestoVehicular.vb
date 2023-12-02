
Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"
''' <summary>
''' Implementación de la clase DPipelineTX
''' </summary>
''' <remarks>
''' Creado Por         : TSF - AEP
''' Fecha de Creación  : 12/11/2012
''' </remarks>
<Guid("71C9E3D3-894F-48d2-8CA8-07F1040D8373") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DImpuestoVehicularTX")> _
Public Class DImpuestoVehicularTX
    Inherits ServicedComponent
    Implements IImpuestoVehicularTX
#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DImpuestoVehicularTX"
#End Region
#Region "Metodos"
    ''' <summary>
    ''' Insertar Representantes del cliente
    ''' </summary>
    ''' <param name="pEImpuestoVehicular">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 14/11/2012
    ''' </remarks>
    Public Function GrabarImpuestoVehicular(ByVal pEImpuestoVehicular As String) As String Implements IImpuestoVehicularTX.GrabarImpuestoVehicular
        'Inicio IBK - AAE
        'Dim blnResultado As Boolean
        Dim strResultado As String
        Dim oEImpuestoVehicular As EImpuestovehicular
        'Incio IBK
        Dim parLote As IDataParameter
        'Dim prmParameter(13) As DAABRequest.Parameter
        Dim prmParameter(17) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEImpuestoVehicular = CFunciones.DeserializeObject(Of EImpuestovehicular)(pEImpuestoVehicular)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oEImpuestoVehicular.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_secFinanciamiento", DbType.Int16, 0, oEImpuestoVehicular.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_CodUnico", DbType.String, 12, oEImpuestoVehicular.Codunico, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_FecDeclaracion", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEImpuestoVehicular.FechaDeclaracion), ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_Periodo", DbType.String, 4, oEImpuestoVehicular.Periodo, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pid_Importe", DbType.Decimal)
        prmParameter(5).Precision = 18
        prmParameter(5).Scale = 6
        prmParameter(5).Value = oEImpuestoVehicular.Monto
        prmParameter(6) = New DAABRequest.Parameter("@pic_cuota", DbType.String, 10, oEImpuestoVehicular.Cuota, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pago_cliente", DbType.String, 10, oEImpuestoVehicular.PagoCliente, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pic_FechaPago", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEImpuestoVehicular.Fecpago), ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pic_estadoPago", DbType.String, 10, oEImpuestoVehicular.EstadoPago, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pic_FechaCobro", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEImpuestoVehicular.Feccobro), ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@pic_EstadoCobro", DbType.String, 10, oEImpuestoVehicular.EstadoCobro, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_Observaciones", DbType.String, 300, oEImpuestoVehicular.Observaciones, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@pic_CodMoneda", DbType.String, 10, oEImpuestoVehicular.Codigomoneda, ParameterDirection.Input)
        'Inicio IBK - AAE - Agrego parámetros
        prmParameter(14) = New DAABRequest.Parameter("@pic_NroLote", DbType.String, 8, oEImpuestoVehicular.CodNroLote, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@pic_CobroAdelantado", DbType.String, 1, oEImpuestoVehicular.CobroAdelantado, ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@pic_outval", DbType.String, 255, "", ParameterDirection.InputOutput)
        prmParameter(17) = New DAABRequest.Parameter("@pic_NoComision", DbType.String, 1, oEImpuestoVehicular.NoComision, ParameterDirection.Input)
        'Fin IBK

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "UP_GCC_IMPUESTO_VEHICULAR_INS"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            'Inicio IBK - AAE - ejecuto la acción y retrno outval
            'If obRequest.Factory.ExecuteNonQuery(obRequest) Then
            '    blnResultado = True
            'End If
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GrabarPipelineIns", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parLote = CType(obRequest.Parameters(16), IDataParameter)
            obRequest.Factory.Dispose()
        End Try
        strResultado = CFunciones.CheckStr(parLote.Value.ToString())
        Return strResultado
        'Fin IBK

    End Function
    ''' <summary>
    ''' Modificar Representantes del cliente
    ''' </summary>
    ''' <param name="pEImpuestoVehicular">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 15/11/2012
    ''' </remarks>
    ''' 'Inicio IBK - AAE - Retorno un string
    Public Function ModificarImpuestoVehicular(ByVal pEImpuestoVehicular As String) As String Implements IImpuestoVehicularTX.ModificarImpuestoVehicular
        'Inicio IBK - AAE
        'Dim blnResultado As Boolean
        Dim strResultado As String
        Dim oEImpuestoVehicular As EImpuestovehicular
        ' Inicio IBK
        Dim parLote As IDataParameter
        'Dim prmParameter(15) As DAABRequest.Parameter
        Dim prmParameter(19) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEImpuestoVehicular = CFunciones.DeserializeObject(Of EImpuestovehicular)(pEImpuestoVehicular)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oEImpuestoVehicular.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_secFinanciamiento", DbType.Int16, 0, oEImpuestoVehicular.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_CodUnico", DbType.String, 12, oEImpuestoVehicular.Codunico, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_FecDeclaracion", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEImpuestoVehicular.FechaDeclaracion), ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_Periodo", DbType.String, 4, oEImpuestoVehicular.Periodo, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pid_Importe", DbType.Decimal)
        prmParameter(5).Precision = 18
        prmParameter(5).Scale = 6
        prmParameter(5).Value = oEImpuestoVehicular.Monto
        prmParameter(6) = New DAABRequest.Parameter("@pic_cuota", DbType.String, 10, oEImpuestoVehicular.Cuota, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pago_cliente", DbType.String, 10, oEImpuestoVehicular.PagoCliente, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pic_FechaPago", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEImpuestoVehicular.Fecpago), ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pic_estadoPago", DbType.String, 10, oEImpuestoVehicular.EstadoPago, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pic_FechaCobro", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEImpuestoVehicular.Feccobro), ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@pic_EstadoCobro", DbType.String, 10, oEImpuestoVehicular.EstadoCobro, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_Observaciones", DbType.String, 300, oEImpuestoVehicular.Observaciones, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@pic_secImpuesto", DbType.Int16, 0, oEImpuestoVehicular.Secimpuesto, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@Pic_Estado", DbType.String, 10, oEImpuestoVehicular.Estado, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@pic_CodMoneda", DbType.String, 10, oEImpuestoVehicular.Codigomoneda, ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@pic_NroLote", DbType.String, 8, oEImpuestoVehicular.CodNroLote, ParameterDirection.Input)
        'Inicio IBK - AAE - Agrego parámetros
        prmParameter(17) = New DAABRequest.Parameter("@pic_CobroAdelantado", DbType.String, 1, oEImpuestoVehicular.CobroAdelantado, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@pic_outval", DbType.String, 255, "", ParameterDirection.InputOutput)
        prmParameter(19) = New DAABRequest.Parameter("@pic_NoComision", DbType.String, 1, oEImpuestoVehicular.NoComision, ParameterDirection.Input)
        'Fin IBK
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "UP_GCC_IMPUESTO_VEHICULAR_UPD"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            'Inicio IBK - AAE - ejecuto la acción y retrno outval
            'If obRequest.Factory.ExecuteNonQuery(obRequest) Then
            '    blnResultado = True
            'End If
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GrabarPipelineIns", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parLote = CType(obRequest.Parameters(18), IDataParameter)
            obRequest.Factory.Dispose()
        End Try
        strResultado = CFunciones.CheckStr(parLote.Value.ToString())
        Return strResultado
        'End Try

        'Return blnResultado
        'Fin IBK
    End Function
    Public Function AsignarLoteImpuestoVehicular(ByVal pEImpuestoVehicular As String) As String Implements IImpuestoVehicularTX.AsignarLoteImpuestoVehicular
        Dim parCodigoLote As IDataParameter
        Dim oEImpuestoVehicular As EImpuestovehicular
        'Inicio IBK
        'Dim prmParameter(1) As DAABRequest.Parameter
        Dim prmParameter(2) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEImpuestoVehicular = CFunciones.DeserializeObject(Of EImpuestovehicular)(pEImpuestoVehicular)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoLote", DbType.String, 8, oEImpuestoVehicular.CodNroLote, ParameterDirection.InputOutput)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodigosImpuesto", DbType.String, 8000, oEImpuestoVehicular.CodigosImpuesto, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_RegeneraLote", DbType.String, 1, oEImpuestoVehicular.RegeneraLote, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "UP_GCC_IMPUESTO_VEHICULAR_ASIGNAR_LOTE_INS"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Generar Lote Impuesto Vehicular", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parCodigoLote = CType(obRequest.Parameters(0), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckStr(parCodigoLote.Value.ToString())

    End Function
    'Inicio IBK - AAE - Agrego parAMETROS
    Public Function EliminarImpuestoVehicular(ByVal pIntCodigoImpuesto As String, ByVal pStrCodigoUsuario As String, ByVal pstrLote As String) As Boolean Implements IImpuestoVehicularTX.EliminarImpuestoVehicular
        Dim blnResultado As Boolean
        'Dim prmParameter(1) As DAABRequest.Parameter
        Dim prmParameter(2) As DAABRequest.Parameter


        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodigosImpuesto", DbType.String, 8000, pIntCodigoImpuesto, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 20, pStrCodigoUsuario, ParameterDirection.Input)
        'Inicio IBK - AAE
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodNroLote", DbType.String, 8, pstrLote, ParameterDirection.Input)
        'Fin IBK

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "UP_GCC_IMPUESTO_VEHICULAR_DEL"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            If obRequest.Factory.ExecuteNonQuery(obRequest) Then
                blnResultado = True
            End If
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarImpuestoVehicularUpd", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex

        End Try

        Return blnResultado

    End Function
    Public Function AsignarChequeImpuestoVehicular(ByVal pEImpuestoVehicular As String) As Boolean Implements IImpuestoVehicularTX.AsignarChequeImpuestoVehicular
        Dim blnResultado As Boolean
        'Inicio IBK - AAE - Agrego parametros
        Dim oEImpuestoVehicular As EImpuestovehicular

        Dim prmParameter(5) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEImpuestoVehicular = CFunciones.DeserializeObject(Of EImpuestovehicular)(pEImpuestoVehicular)


        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, oEImpuestoVehicular.Usuariomodificacion, ParameterDirection.Input)
        'Inicio IBK
        'prmParameter(1) = New DAABRequest.Parameter("@piv_NroCheque", DbType.String, 20, oEImpuestoVehicular.Nrocheque, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_NroCheque", DbType.String, 50, oEImpuestoVehicular.Nrocheque, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 20, oEImpuestoVehicular.CodNroLote, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_FechaCobro", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEImpuestoVehicular.Feccobro), ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_FechaPago", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEImpuestoVehicular.Fecpago), ParameterDirection.Input)
        'Inicio IBK - AAE
        prmParameter(5) = New DAABRequest.Parameter("@pic_cantDias", DbType.Int32, 4, oEImpuestoVehicular.CantDias, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "UP_GCC_IMPUESTO_VEHICULAR_ASIGNARCHEQUE_UPD"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            If obRequest.Factory.ExecuteNonQuery(obRequest) Then
                blnResultado = True
            End If
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GenerarChequeImpuestoVehicular", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex

        End Try

        Return blnResultado

    End Function
    'Inicio IBK - AAE - obtengo el nro de lote
    Public Function AsignarLoteImpuestoVehicular2(ByVal pEImpuestoVehicular As String) As String Implements IImpuestoVehicularTX.AsignarLoteImpuestoVehicular2
        Dim parCodigoLote As IDataParameter
        Dim oEImpuestoVehicular As EImpuestovehicular
        'Inicio IBK
        'Dim prmParameter(1) As DAABRequest.Parameter
        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEImpuestoVehicular = CFunciones.DeserializeObject(Of EImpuestovehicular)(pEImpuestoVehicular)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoLote", DbType.String, 8, oEImpuestoVehicular.CodNroLote, ParameterDirection.InputOutput)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "UP_GCC_IMPUESTO_VEHICULAR_ASIGNAR_LOTE_GET2"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Generar Lote Impuesto Vehicular", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parCodigoLote = CType(obRequest.Parameters(0), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckStr(parCodigoLote.Value.ToString())

    End Function

    <AutoComplete(True)> _
    Public Function ReGenerarLote(ByVal strLote As String) As String Implements IImpuestoVehicularTX.ReGenerarLote
        Dim prmParameter(0) As DAABRequest.Parameter
        Dim strRes As String = ""
        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodLote", DbType.String, 8, strLote, ParameterDirection.InputOutput)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "UP_GCC_LOTE_GEN"
        obRequest.Parameters.AddRange(prmParameter)
        Try
            If obRequest.Factory.ExecuteNonQuery(obRequest) Then
                strRes = strLote
            End If

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Generar Lote Impuesto Vehicular", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return strRes


    End Function
    'Fin IBK
    '============= MULTA VEHICULAR ================

    ''' <summary>
    ''' Insertar Representantes del cliente
    ''' </summary>
    ''' <param name="pEMultaVehicular">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 14/11/2012
    ''' </remarks>
    Public Function GrabarMultaVehicular(ByVal pEMultaVehicular As String) As String Implements IImpuestoVehicularTX.GrabarMultaVehicular
        'Inicio IBK - AAE
        'Dim blnResultado As Boolean
        Dim strResultado As String
        Dim oEMultaVehicular As EGCC_MultaVehicular
        'Inicio IBK
        Dim parLote As IDataParameter
        'Dim prmParameter(17) As DAABRequest.Parameter
        Dim prmParameter(22) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEMultaVehicular = CFunciones.DeserializeObject(Of EGCC_MultaVehicular)(pEMultaVehicular)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oEMultaVehicular.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_secFinanciamiento", DbType.Int16, 0, oEMultaVehicular.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_NroInfraccion", DbType.String, 12, oEMultaVehicular.NroInfraccion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_FecInfraccion", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEMultaVehicular.FechaInfraccion), ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_CodConcepto", DbType.String, 10, oEMultaVehicular.CodConcepto, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_CodInfraccion", DbType.String, 10, oEMultaVehicular.CodInfraccion, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_Infraccion", DbType.String, 40, oEMultaVehicular.Infraccion, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pic_FecIngreso", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEMultaVehicular.FechaIngreso), ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pic_FecRecepcionBanco", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEMultaVehicular.FechaRecepcionBanco), ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pid_Importe", DbType.Decimal)
        prmParameter(9).Precision = 18
        prmParameter(9).Scale = 6
        prmParameter(9).Value = oEMultaVehicular.Monto
        prmParameter(10) = New DAABRequest.Parameter("@pid_ImporteDcto", DbType.Decimal)
        prmParameter(10).Precision = 18
        prmParameter(10).Scale = 6
        prmParameter(10).Value = oEMultaVehicular.ImporteDescuento
        prmParameter(11) = New DAABRequest.Parameter("@pic_CodMunicipalidad", DbType.String, 10, oEMultaVehicular.CodMunicipalidad, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pago_cliente", DbType.String, 10, oEMultaVehicular.PagoCliente, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@pic_FechaPago", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEMultaVehicular.Fecpago), ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@pic_estadoPago", DbType.String, 10, oEMultaVehicular.EstadoPago, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@pic_FechaCobro", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEMultaVehicular.Feccobro), ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@pic_EstadoCobro", DbType.String, 10, oEMultaVehicular.EstadoCobro, ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@piv_Observaciones", DbType.String, 300, oEMultaVehicular.Observaciones, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 8, oEMultaVehicular.CodNroLote, ParameterDirection.Input)
        'Inicio IBK - AAE - Agrego parámetros
        prmParameter(19) = New DAABRequest.Parameter("@pic_CobroAdelantado", DbType.String, 1, oEMultaVehicular.CobroAdelantado, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@pic_outval", DbType.String, 255, "", ParameterDirection.InputOutput)
        prmParameter(21) = New DAABRequest.Parameter("@pic_NoComision", DbType.String, 1, oEMultaVehicular.NoComision, ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@pic_FechaNotifLeasing", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEMultaVehicular.FechaNotificacionLeasing), ParameterDirection.Input)
        'Fin IBK
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "UP_GCC_MULTA_VEHICULAR_INS"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            'Inicio IBK - AAE - ejecuto la acción y retrno outval
            'If obRequest.Factory.ExecuteNonQuery(obRequest) Then
            '    blnResultado = True
            'End If
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GrabarMultaVehicularIns", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parLote = CType(obRequest.Parameters(20), IDataParameter)
            obRequest.Factory.Dispose()
        End Try
        strResultado = CFunciones.CheckStr(parLote.Value.ToString())
        Return strResultado
        'Fin IBK
        'Return blnResultado

    End Function

    ''' <summary>
    ''' Modificar Representantes del cliente
    ''' </summary>
    ''' <param name="pEMultaVehicular">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 15/11/2012
    ''' </remarks>
    ''' 'Inicio IBK - AAE - Retorno un string
    Public Function ModificarMultaVehicular(ByVal pEMultaVehicular As String) As String Implements IImpuestoVehicularTX.ModificarMultaVehicular
        'Inicio IBK - AAE
        'Dim blnResultado As Boolean
        Dim strResultado As String
        Dim oEMultaVehicular As EGCC_MultaVehicular
        'Inicio IBK
        Dim parLote As IDataParameter
        'Dim prmParameter(18) As DAABRequest.Parameter
        Dim prmParameter(23) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEMultaVehicular = CFunciones.DeserializeObject(Of EGCC_MultaVehicular)(pEMultaVehicular)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@pic_Codsolicitudcredito", DbType.String, 8, oEMultaVehicular.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_secFinanciamiento", DbType.Int16, 0, oEMultaVehicular.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_NroInfraccion", DbType.String, 12, oEMultaVehicular.NroInfraccion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_FecInfraccion", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEMultaVehicular.FechaInfraccion), ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_CodConcepto", DbType.String, 10, oEMultaVehicular.CodConcepto, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_CodInfraccion", DbType.String, 10, oEMultaVehicular.CodInfraccion, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_Infraccion", DbType.String, 40, oEMultaVehicular.Infraccion, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pic_FecIngreso", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEMultaVehicular.FechaIngreso), ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pic_FecRecepcionBanco", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEMultaVehicular.FechaRecepcionBanco), ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pid_Importe", DbType.Decimal)
        prmParameter(9).Precision = 18
        prmParameter(9).Scale = 6
        prmParameter(9).Value = oEMultaVehicular.Monto
        prmParameter(10) = New DAABRequest.Parameter("@pid_ImporteDcto", DbType.Decimal)
        prmParameter(10).Precision = 18
        prmParameter(10).Scale = 6
        prmParameter(10).Value = oEMultaVehicular.ImporteDescuento
        prmParameter(11) = New DAABRequest.Parameter("@pic_CodMunicipalidad", DbType.String, 10, oEMultaVehicular.CodMunicipalidad, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pago_cliente", DbType.String, 10, oEMultaVehicular.PagoCliente, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@pic_FechaPago", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEMultaVehicular.Fecpago), ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@pic_estadoPago", DbType.String, 10, oEMultaVehicular.EstadoPago, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@pic_FechaCobro", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEMultaVehicular.Feccobro), ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@pic_EstadoCobro", DbType.String, 10, oEMultaVehicular.EstadoCobro, ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@piv_Observaciones", DbType.String, 300, oEMultaVehicular.Observaciones, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@pic_secMulta", DbType.Int16, 0, oEMultaVehicular.Secimpuesto, ParameterDirection.Input)
        prmParameter(19) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 8, oEMultaVehicular.CodNroLote, ParameterDirection.Input)
        'Inicio IBK - AAE - Agrego parámetros
        prmParameter(20) = New DAABRequest.Parameter("@pic_CobroAdelantado", DbType.String, 1, oEMultaVehicular.CobroAdelantado, ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@pic_outval", DbType.String, 255, "", ParameterDirection.InputOutput)
        prmParameter(22) = New DAABRequest.Parameter("@pic_NoComision", DbType.String, 1, oEMultaVehicular.NoComision, ParameterDirection.Input)
        prmParameter(23) = New DAABRequest.Parameter("@pic_FechaNotifLeasing", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEMultaVehicular.FechaNotificacionLeasing), ParameterDirection.Input)
        'Fin IBK
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "UP_GCC_MULTA_VEHICULAR_UPD"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            'Inicio IBK - AAE - ejecuto la acción y retrno outval
            'If obRequest.Factory.ExecuteNonQuery(obRequest) Then
            '    blnResultado = True
            'End If
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarImpuestoVehicularUpd", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parLote = CType(obRequest.Parameters(21), IDataParameter)
            obRequest.Factory.Dispose()
        End Try
        strResultado = CFunciones.CheckStr(parLote.Value.ToString())
        Return strResultado
        'End Try

        'Return blnResultado
        'Fin IBK
    End Function
    ''' <summary>
    ''' Modificar Representantes del cliente
    ''' </summary>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 15/11/2012
    ''' </remarks>
    '''  ' Inicio IBK - Agrego Parametro
    Public Function EliminarMultaVehicular(ByVal pIntCodigoMulta As String, ByVal pStrCodigoUsuario As String, ByVal pstrLote As String) As Boolean Implements IImpuestoVehicularTX.EliminarMultaVehicular
        Dim blnResultado As Boolean
        'Dim prmParameter(1) As DAABRequest.Parameter
        Dim prmParameter(2) As DAABRequest.Parameter

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodigosMulta", DbType.String, 8000, pIntCodigoMulta, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 20, pStrCodigoUsuario, ParameterDirection.Input)
        'Inicio IBK - AAE
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodNroLote", DbType.String, 8, pstrLote, ParameterDirection.Input)
        'Fin IBK

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "UP_GCC_MULTA_VEHICULAR_DEL"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            If obRequest.Factory.ExecuteNonQuery(obRequest) Then
                blnResultado = True
            End If
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarImpuestoVehicularUpd", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex

        End Try

        Return blnResultado

    End Function

    Public Function AsignarLoteMultaVehicular(ByVal pEImpuestoVehicular As String) As String Implements IImpuestoVehicularTX.AsignarLoteMultaVehicular
        Dim parCodigoLote As IDataParameter
        Dim oEImpuestoVehicular As EImpuestovehicular
        'Iniccio IBK
        'Dim prmParameter(1) As DAABRequest.Parameter
        Dim prmParameter(3) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEImpuestoVehicular = CFunciones.DeserializeObject(Of EImpuestovehicular)(pEImpuestoVehicular)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoLote", DbType.String, 8, oEImpuestoVehicular.CodNroLote, ParameterDirection.InputOutput)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodigosImpuesto", DbType.String, 8000, oEImpuestoVehicular.CodigosImpuesto, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_RegeneraLote", DbType.String, 1, IIf(oEImpuestoVehicular.CodNroLote = "", "0", "1"), ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Municipalidad", DbType.String, 3, oEImpuestoVehicular.Municipalidad, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "UP_GCC_MULTA_VEHICULAR_ASIGNAR_LOTE_INS"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Generar Lote Multa Vehicular", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parCodigoLote = CType(obRequest.Parameters(0), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckStr(parCodigoLote.Value.ToString())

    End Function
    Public Function AsignarChequeMultaVehicular(ByVal pEImpuestoVehicular As String) As Boolean Implements IImpuestoVehicularTX.AsignarChequeMultaVehicular
        Dim blnResultado As Boolean
        Dim oEImpuestoVehicular As EGCC_MultaVehicular
        'Inicio IBK - AAE - Agrego param
        'Dim prmParameter(4) As DAABRequest.Parameter
        Dim prmParameter(5) As DAABRequest.Parameter
        'Fin IBK

        'Deserealiza la Entidad
        oEImpuestoVehicular = CFunciones.DeserializeObject(Of EGCC_MultaVehicular)(pEImpuestoVehicular)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, oEImpuestoVehicular.Usuariomodificacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_NroCheque", DbType.String, 20, oEImpuestoVehicular.Nrocheque, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 20, oEImpuestoVehicular.CodNroLote, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_FechaCobro", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEImpuestoVehicular.Feccobro), ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_FechaPago", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEImpuestoVehicular.Fecpago), ParameterDirection.Input)
        'Inicio IBK - AAE
        prmParameter(5) = New DAABRequest.Parameter("@pic_cantDias", DbType.Int32, 4, oEImpuestoVehicular.CantDias, ParameterDirection.Input)
        'Fin IBK


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "UP_GCC_MULTA_VEHICULAR_ASIGNARCHEQUE_UPD"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            If obRequest.Factory.ExecuteNonQuery(obRequest) Then
                blnResultado = True
            End If
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GenerarChequeMultaVehicular", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex

        End Try

        Return blnResultado

    End Function
    Public Function EliminarLote(ByVal pLote As String) As Boolean Implements IImpuestoVehicularTX.EliminarLote
        Dim blnResultado As Boolean
        Dim prmParameter(0) As DAABRequest.Parameter


        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 8, pLote, ParameterDirection.Input)



        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "UP_GCC_Eliminar_Lote"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            If obRequest.Factory.ExecuteNonQuery(obRequest) Then
                blnResultado = True
            End If
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarImpuestoVehicularUpd", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex

        End Try

        Return blnResultado

    End Function
    'Inicio IBK - AAE
    Public Function AnularLote(ByVal pLote As String) As String Implements IImpuestoVehicularTX.AnularLote

        Dim parCodigoLote As IDataParameter
        Dim prmParameter(1) As DAABRequest.Parameter


        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 8, pLote, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_outVal", DbType.String, 100, "", ParameterDirection.InputOutput)



        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "UP_GCC_Anular_Lote"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarImpuestoVehicularUpd", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parCodigoLote = CType(obRequest.Parameters(1), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckStr(parCodigoLote.Value.ToString())

    End Function
    'Fin IBK


#End Region


End Class

#End Region

#Region "Clase NO Transaccional"
''' <summary>
''' Implementación de la clase DImpuestoVehicularNTX
''' </summary>
''' <remarks>
''' Creado Por         : TSF - AEP
''' Fecha de Creación  : 12/11/2012
''' </remarks>
<Guid("AE53D405-700E-4bbc-A7D2-96DA1F53089F") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DImpuestoVehicularNTX")> _
Public Class DImpuestoVehicularNTX
    Inherits ServicedComponent
    Implements IImpuestoVehicularNTX

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DImpuestoVehicularNTX"
#End Region

#Region "Metodos"

    Public Function LiquidarLote(ByVal pUsuarioModificacion As String, ByVal pNroLote As String, ByVal pCodigoConcepto As String) As String Implements IImpuestoVehicularNTX.LiquidarLote
        Dim parMensajeError As IDataParameter
        Dim parOutVal As IDataParameter
        'Iniccio IBK
        Dim prmParameter(4) As DAABRequest.Parameter
        Dim pMensaje As String = String.Empty
        Dim pOutVal As String = String.Empty
        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 100, pUsuarioModificacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 10, pNroLote, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodigoConcepto", DbType.String, 3, pCodigoConcepto, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@MensajeError", DbType.String, 1000, pMensaje, ParameterDirection.Output)
        prmParameter(4) = New DAABRequest.Parameter("@outval", DbType.String, 2, pOutVal, ParameterDirection.Output)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoCobroImpuesto_Ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Liquida Lote Impuesto Vehicular", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            oLog = Nothing
            Throw ex
        Finally
            parMensajeError = CType(obRequest.Parameters(3), IDataParameter)
            parOutVal = CType(obRequest.Parameters(4), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckStr(parMensajeError.Value.ToString()) + "|" + CFunciones.CheckStr(parOutVal.Value.ToString())


    End Function

    Public Function ListarImpuestoVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pEImpuestoVehicular As String) As String Implements IImpuestoVehicularNTX.ListarImpuestoVehicular

        Dim oEImpuestoVehicular As New EImpuestovehicular
        oEImpuestoVehicular = CFunciones.DeserializeObject(Of EImpuestovehicular)(pEImpuestoVehicular)
        'Deserealiza la Entidad
        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(16) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, oEImpuestoVehicular.Codsolcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_CodUnico", DbType.String, 10, oEImpuestoVehicular.Codunico, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pic_NombreRazonSocial", DbType.String, 100, oEImpuestoVehicular.RazonSocialNombre, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@pic_CodigoTipoDocumento", DbType.String, 10, oEImpuestoVehicular.CodigoTipoDocumento, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@pic_NumeroDocumento", DbType.String, 20, oEImpuestoVehicular.NumeroDocumento, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@piv_placa", DbType.String, 40, oEImpuestoVehicular.Placa, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@pic_AnioFabricacion", DbType.String, 4, oEImpuestoVehicular.AnioFabricacion, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@pic_Periodo", DbType.String, 4, oEImpuestoVehicular.Periodo, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@pid_FechaInscripcionIni", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEImpuestoVehicular.FechaInscripcionIni), ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@pid_FechaInscripcionFin", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEImpuestoVehicular.FechaInscripcionFin), ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@pic_EstadoCobro", DbType.String, 10, oEImpuestoVehicular.EstadoCobro, ParameterDirection.Input)
            prmParameter(15) = New DAABRequest.Parameter("@pic_EstadoPago", DbType.String, 10, oEImpuestoVehicular.EstadoPago, ParameterDirection.Input)
            prmParameter(16) = New DAABRequest.Parameter("@pic_NroLote", DbType.String, 10, oEImpuestoVehicular.CodNroLote, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_IMPUESTO_VEHICULAR_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function


    Public Function ListarImpuestoVehicularReporte(ByVal pEImpuestoVehicular As String) As String Implements IImpuestoVehicularNTX.ListarImpuestoVehicularReporte

        Dim oEImpuestoVehicularReporte As New EImpuestovehicular
        oEImpuestoVehicularReporte = CFunciones.DeserializeObject(Of EImpuestovehicular)(pEImpuestoVehicular)
        'Deserealiza la Entidad
        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(12) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
            'prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
            'prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            'prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, oEImpuestoVehicularReporte.Codsolcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_CodUnico", DbType.String, 10, oEImpuestoVehicularReporte.Codunico, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@pic_NombreRazonSocial", DbType.String, 100, oEImpuestoVehicularReporte.RazonSocialNombre, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@pic_CodigoTipoDocumento", DbType.String, 10, oEImpuestoVehicularReporte.CodigoTipoDocumento, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_NumeroDocumento", DbType.String, 20, oEImpuestoVehicularReporte.NumeroDocumento, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_placa", DbType.String, 40, oEImpuestoVehicularReporte.Placa, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pic_AnioFabricacion", DbType.String, 4, oEImpuestoVehicularReporte.AnioFabricacion, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@pic_Periodo", DbType.String, 4, oEImpuestoVehicularReporte.Periodo, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@pid_FechaInscripcionIni", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEImpuestoVehicularReporte.FechaInscripcionIni), ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@pid_FechaInscripcionFin", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEImpuestoVehicularReporte.FechaInscripcionFin), ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@pic_EstadoCobro", DbType.String, 10, oEImpuestoVehicularReporte.EstadoCobro, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@pic_EstadoPago", DbType.String, 10, oEImpuestoVehicularReporte.EstadoPago, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@pic_NroLote", DbType.String, 10, oEImpuestoVehicularReporte.CodNroLote, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_IMPUESTO_VEHICULAR_REP_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarImpuestoVehicularReporte", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Public Function ListarImpuestoVehicularLiquidar(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pCodLote As String) As String Implements IImpuestoVehicularNTX.ListarImpuestoVehicularLiquidar

        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(4) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_NroLote", DbType.String, 10, pCodLote, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_IMPUESTO_VEHICULAR_LIQUIDAR_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Public Function ListarImpuestoVehicularLiquidarTodo(ByVal pCodLote As String) As String Implements IImpuestoVehicularNTX.ListarImpuestoVehicularLiquidarTodo

        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_NroLote", DbType.String, 10, pCodLote, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_IMPUESTO_VEHICULAR_LIQUIDAR_TODO_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Public Function ListarBienImpuestoVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pPlaca As String, _
                                    ByVal pSecfinanciamiento As String, _
                                           ByVal pNroMotor As String, _
                                           ByVal pCUCliente As String, _
                                           ByVal pCodContrato As String) As String Implements IImpuestoVehicularNTX.ListarBienImpuestoVehicular


        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        'Inicio IBK
        'Dim prmParameter(5) As DAABRequest.Parameter
        Dim prmParameter(8) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_placa", DbType.String, 40, pPlaca, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@Secfinanciamiento", DbType.String, 40, pSecfinanciamiento, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@NroMotor", DbType.String, 40, pNroMotor, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@CUCliente", DbType.String, 40, pCUCliente, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@CodContrato", DbType.String, 40, pCodContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_BIEN_IMPUESTO_VEHICULAR_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Public Function ListarLoteImpuestoVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pPlaca As String, _
                                    ByVal pTipo As Integer, _
                                   ByVal pNroLote As String) As String Implements IImpuestoVehicularNTX.ListarLoteImpuestoVehicular


        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        ' Inicio IBK
        'Dim prmParameter(5) As DAABRequest.Parameter
        Dim prmParameter(6) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_placa", DbType.String, 40, pPlaca, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_tipo", DbType.Int16, 0, pTipo, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_Lote", DbType.String, 8, pNroLote, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_IMPUESTO_VEHICULAR_LOTE_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    'GCCTS_AEP_20130212 - Creación de listado para impuestos vehiculares

    Public Function ListarLoteImpuestoVehicularConsulta(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pPlaca As String, _
                                    ByVal pTipo As Integer) As String Implements IImpuestoVehicularNTX.ListarLoteImpuestoVehicularConsulta


        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        ' Inicio IBK
        Dim prmParameter(5) As DAABRequest.Parameter
        'Dim prmParameter(6) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_placa", DbType.String, 40, pPlaca, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_tipo", DbType.Int16, 0, pTipo, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_CONSULTA_IMPUESTO_VEHICULAR_LOTE_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Public Function ListarImpuestoVehicularReporteLiquidar(ByVal pCodigoImpuesto As String) As String Implements IImpuestoVehicularNTX.ListarImpuestoVehicularReporteLiquidar


        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodigosImpuesto", DbType.String, 8000, pCodigoImpuesto, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_IMPUESTO_VEHICULAR_REPORTE_LIQUIDAR_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Public Function ObtenerDatosImpuesto(ByVal pstrPlaca As String, ByVal pCodImpuesto As Integer) As String Implements IImpuestoVehicularNTX.ObtenerDatosImpuesto


        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(1) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_Placa", DbType.String, 40, pstrPlaca, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_SecImpuesto", DbType.Int16, 0, pCodImpuesto, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_IMPUESTO_VEHICULAR_GET"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Public Function ListarCuotasPeriodo(ByVal piiCodigoBien As Integer, ByVal piiPeriodo As Integer, ByVal picCodigoContrato As String) As String Implements IImpuestoVehicularNTX.ListarCuotasPeriodo


        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(2) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_SecFinanciamiento", DbType.Int16, 0, piiCodigoBien, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_Periodo", DbType.Int16, 0, piiPeriodo, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8, picCodigoContrato, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_IMPUESTO_VEHICULAR_PERIODO_GET"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Public Function ObtenerPeriodosValidacion(ByVal pstrCadigosImpuesto As String) As String Implements IImpuestoVehicularNTX.ObtenerPeriodosValidacion


        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@CODIGO_IMPUESTO", DbType.String, 4000, pstrCadigosImpuesto, ParameterDirection.Input)



            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_IMPUESTO_VEHICULAR_VALIDACION_GET"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Public Function ObtenerTotalCuotas(ByVal pstrCadigosBien As String, ByVal pstrCodigosPeriodo As String, ByVal pstrContratos As String) As String Implements IImpuestoVehicularNTX.ObtenerTotalCuotas


        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(2) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@CODIGO_BIEN", DbType.String, 4000, pstrCadigosBien, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@PERIODO", DbType.String, 4000, pstrCodigosPeriodo, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@CONTRATOS", DbType.String, 4000, pstrContratos, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_IMPUESTO_VEHICULAR_TOTALCUOTAS_GET"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Public Function ListarMultaVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pEMultaVehicular As String) As String Implements IImpuestoVehicularNTX.ListarMultaVehicular

        Dim oEMultaVehicular As New EGCC_MultaVehicular
        oEMultaVehicular = CFunciones.DeserializeObject(Of EGCC_MultaVehicular)(pEMultaVehicular)
        'Deserealiza la Entidad
        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(17) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, oEMultaVehicular.Codsolcredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_CodUnico", DbType.String, 8, oEMultaVehicular.Codunico, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pic_NombreRazonSocial", DbType.String, 100, oEMultaVehicular.RazonSocialNombre, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@pic_CodigoTipoDocumento", DbType.String, 10, oEMultaVehicular.CodigoTipoDocumento, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@pic_TipoBien", DbType.String, 10, oEMultaVehicular.CodTipoBien, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@pic_NumeroDocumento", DbType.String, 10, oEMultaVehicular.NumeroDocumento, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@pic_NroLote", DbType.String, 10, oEMultaVehicular.CodNroLote, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@pic_Concepto", DbType.String, 10, oEMultaVehicular.CodConcepto, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@piv_placa", DbType.String, 40, oEMultaVehicular.Placa, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@pic_CodInfraccion", DbType.String, 4, oEMultaVehicular.CodInfraccion, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@pic_Infraccion", DbType.String, 4, oEMultaVehicular.Infraccion, ParameterDirection.Input)
            prmParameter(15) = New DAABRequest.Parameter("@pic_CodDistrito", DbType.String, 10, oEMultaVehicular.CodMunicipalidad, ParameterDirection.Input)
            prmParameter(16) = New DAABRequest.Parameter("@pic_EstadoCobro", DbType.String, 10, oEMultaVehicular.EstadoCobro, ParameterDirection.Input)
            prmParameter(17) = New DAABRequest.Parameter("@pic_EstadoPago", DbType.String, 10, oEMultaVehicular.EstadoPago, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_MULTA_VEHICULAR_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Public Function ListarBienMultaVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pCodMunicipalidad As String, _
                                   ByVal pPlaca As String, _
                                    ByVal pSecfinanciamiento As String, _
                                           ByVal pNroMotor As String, _
                                           ByVal pCUCliente As String, _
                                           ByVal pCodContrato As String) As String Implements IImpuestoVehicularNTX.ListarBienMultaVehicular



        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        'Inicio IBK
        'Dim prmParameter(6) As DAABRequest.Parameter
        Dim prmParameter(9) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_placa", DbType.String, 40, pPlaca, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_Municipalidad", DbType.String, 10, pCodMunicipalidad, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@Secfinanciamiento", DbType.String, 40, pSecfinanciamiento, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@NroMotor", DbType.String, 40, pNroMotor, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@CUCliente", DbType.String, 40, pCUCliente, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@CodContrato", DbType.String, 40, pCodContrato, ParameterDirection.Input)
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_BIEN_MULTA_VEHICULAR_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarMultaVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Function ObtenerDatosMulta(ByVal pstrPlaca As String, ByVal pintSecMulta As Integer) As String Implements IImpuestoVehicularNTX.ObtenerDatosMulta


        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(1) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_Placa", DbType.String, 40, pstrPlaca, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_SecMulta", DbType.Int16, 0, pintSecMulta, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_MULTA_VEHICULAR_GET"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    ''' <summary>
    ''' Obtiene un registro de la multa vehicular
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 21/01/2013
    ''' </remarks>
    Public Function ObtenerDatosMultaConsulta(ByVal pEMultaVehicular As String) As String Implements IImpuestoVehicularNTX.ObtenerDatosMultaConsulta


        Dim odtoEImpuestoVehicular As DataTable = Nothing
        Dim oEMultaVehicular As New EGCC_MultaVehicular
        oEMultaVehicular = CFunciones.DeserializeObject(Of EGCC_MultaVehicular)(pEMultaVehicular)
        'Deserealiza la Entidad
        Dim prmParameter(14) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEMultaVehicular.Codsolcredito, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 8, oEMultaVehicular.Codunico, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_NombreRazonSocial", DbType.String, 100, oEMultaVehicular.RazonSocialNombre, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_CodigoTipoDocumento", DbType.String, 10, oEMultaVehicular.CodigoTipoDocumento, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_TipoBien", DbType.String, 10, oEMultaVehicular.CodTipoBien, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_NumeroDocumento", DbType.String, 10, oEMultaVehicular.NumeroDocumento, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 10, oEMultaVehicular.CodNroLote, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_Concepto", DbType.String, 10, oEMultaVehicular.CodConcepto, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@piv_Placa", DbType.String, 40, oEMultaVehicular.Placa, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@piv_CodInfraccion", DbType.String, 4, oEMultaVehicular.CodInfraccion, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@piv_Infraccion", DbType.String, 4, oEMultaVehicular.Infraccion, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@piv_CodDistrito", DbType.String, 10, oEMultaVehicular.CodMunicipalidad, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@piv_EstadoCobro", DbType.String, 10, oEMultaVehicular.EstadoCobro, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@piv_EstadoPago", DbType.String, 10, oEMultaVehicular.EstadoPago, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@pii_SecMulta", DbType.Int32, 10, oEMultaVehicular.Secimpuesto, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_MultaVehicularConsulta_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerDatosMultaConsulta", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Public Function ListarLoteMultaVehicular(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pPlaca As String, _
                                   ByVal pCodMunicipal As String, _
                                   ByVal pTipo As Integer, _
                                   ByVal pNroLote As String) As String Implements IImpuestoVehicularNTX.ListarLoteMultaVehicular



        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        'Inicio IBK
        'Dim prmParameter(6) As DAABRequest.Parameter
        Dim prmParameter(7) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_placa", DbType.String, 40, pPlaca, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_CodMunicipalidad", DbType.String, 40, pCodMunicipal, ParameterDirection.Input)
            'prmParameter(6) = New DAABRequest.Parameter("@piv_tipo", DbType.Int16, 0, pTipo, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_tipo", DbType.Int16, 4, pTipo, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_Lote", DbType.String, 8, pNroLote, ParameterDirection.Input)
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_MULTA_VEHICULAR_LOTE_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Public Function ListarEscalaInfraccionesMulta() As String Implements IImpuestoVehicularNTX.ListarEscalaInfraccionesMulta


        Dim odtEscalaInfraccion As DataTable = Nothing

        'Deserealiza la Entidad

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_MULTA_VEHICULAR_ESCALA_INFRACCION_SEL"
            odtEscalaInfraccion = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtEscalaInfraccion)
    End Function

    Public Function ListarMultaVehicularLiquidar(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pCodLote As String) As String Implements IImpuestoVehicularNTX.ListarMultaVehicularLiquidar

        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(4) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_NroLote", DbType.String, 10, pCodLote, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_MULTA_VEHICULAR_LIQUIDAR_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Public Function ListarMultaVehicularLiquidarTodo(ByVal pCodLote As String) As String Implements IImpuestoVehicularNTX.ListarMultaVehicularLiquidarTodo

        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_NroLote", DbType.String, 10, pCodLote, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_MULTA_VEHICULAR_LIQUIDAR_TODO_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Public Function ListarMultaVehicularReporteLiquidar(ByVal pCodigoImpuesto As String) As String Implements IImpuestoVehicularNTX.ListarMultaVehicularReporteLiquidar


        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodigosImpuesto", DbType.String, 8000, pCodigoImpuesto, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_MULTA_VEHICULAR_REPORTE_LIQUIDAR_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)
    End Function

    Public Function ListarAltertaImpuestoVehicular(ByVal pNroLote, _
                                                   ByVal pNroCheque) As String Implements IImpuestoVehicularNTX.ListarAltertaImpuestoVehicular
        'Dim oEImpuestoVehicular As New EImpuestovehicular
        'oEImpuestoVehicular = CFunciones.DeserializeObject(Of EImpuestovehicular)(pEImpuestoVehicular)
        'Deserealiza la Entidad
        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(1) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 10, pNroLote, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_NroCheque", DbType.String, 20, pNroCheque, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_ALERTA_IMP_VEHICULAR_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtoEImpuestoVehicular = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarAltertaImpuestoVehicular", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtoEImpuestoVehicular)


    End Function
    'Inicio IBK - AAE - Agrego funcion
    Public Function CheckLote(ByVal pNroLote As String, ByVal pflag As String) As String Implements IImpuestoVehicularNTX.CheckLote
        Dim parLote As IDataParameter
        Dim odtbImpuestoMultasInmueble As DataTable
        Dim prmParameter(2) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 8, pNroLote, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@flag", DbType.String, 1, pflag, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@resultado", DbType.String, 10, "", ParameterDirection.InputOutput)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_Lote_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbImpuestoMultasInmueble = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GetImpuestoMunicipalBienes", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parLote = CType(objRequest.Parameters(2), IDataParameter)
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckStr(parLote.Value.ToString())
    End Function

    Public Function ObtenerHeaderLote(ByVal pNroLote As String, ByVal pflag As String) As String Implements IImpuestoVehicularNTX.ObtenerHeaderLote

        Dim odtbImpuestoMultasInmueble As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 8, pNroLote, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@flag", DbType.String, 1, pflag, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_LoteHeader_get"
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

        Return CFunciones.SerializeObject(Of DataTable)(odtbImpuestoMultasInmueble)
    End Function

    'Fin IBK
    'Inicio JJM IBK
    Public Function GetImpuestoMultas(ByVal pNroLote As String) As String Implements IImpuestoVehicularNTX.GetImpuestoMultas
        Dim odtbImpuestoMultasInmueble As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 8, pNroLote, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_ObtieneMultasLote_get"
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
    Public Function GetImpuestoVehicular(ByVal pNroLote As String) As String Implements IImpuestoVehicularNTX.GetImpuestoVehicular
        Dim odtbImpuestoMultasInmueble As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 8, pNroLote, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_ObtieneImpuestoVehicularLote_get"
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
    'Fin JJM IBK

#End Region


End Class

#End Region
