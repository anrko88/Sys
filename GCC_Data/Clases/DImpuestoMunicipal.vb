Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DImpuestoMunicipalTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/10/2012
''' </remarks>
<Guid("db31a02e-face-44b0-ba0e-1f97ac462aca") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DImpuestoMunicipalTx")> _
Public Class DImpuestoMunicipalTx
    Inherits ServicedComponent
    Implements IImpuestoMunicipalTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DImpuestoMunicipalTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta ImpuestoMunicipal
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Entidad Serializado de ImpuestoMunicipal formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 11/12/2012
    ''' </remarks>
    Public Function InsertarImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String Implements IImpuestoMunicipalTx.InsertarImpuestoMunicipal
        'Inicio IBK - AAE
        Dim oEImpuestoMunicipal As New EImpuestomunicipal
        Dim strResultado As String
        Dim parLote As IDataParameter
        'Dim prmParameter(33) As DAABRequest.Parameter
        Dim prmParameter(36) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEImpuestoMunicipal = CFunciones.DeserializeObject(Of EImpuestomunicipal)(pEImpuestoMunicipal)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oEImpuestoMunicipal.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinanciamiento", DbType.Int16, 0, oEImpuestoMunicipal.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pii_SecImpuesto", DbType.Int16, 0, oEImpuestoMunicipal.Secimpuesto, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 8, oEImpuestoMunicipal.Codunico, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pid_FecPago", DbType.String, 8, oEImpuestoMunicipal.Fecpago, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pid_FecLimite", DbType.String, 8, oEImpuestoMunicipal.Feclimite, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Municipalidad", DbType.String, 8, oEImpuestoMunicipal.Municipalidad, ParameterDirection.Input)

        prmParameter(7) = New DAABRequest.Parameter("@pin_Monto", DbType.Decimal)
        prmParameter(7).Precision = 20
        prmParameter(7).Scale = 2
        prmParameter(7).Value = oEImpuestoMunicipal.Monto

        prmParameter(8) = New DAABRequest.Parameter("@piv_Concepto", DbType.String, 8, oEImpuestoMunicipal.Concepto, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_Observaciones", DbType.String, 150, oEImpuestoMunicipal.Observaciones, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, oEImpuestoMunicipal.Usuariomodificacion, ParameterDirection.Input)

        prmParameter(11) = New DAABRequest.Parameter("@pin_Comision", DbType.Decimal)
        prmParameter(11).Precision = 20
        prmParameter(11).Scale = 2
        prmParameter(11).Value = oEImpuestoMunicipal.Comision

        prmParameter(12) = New DAABRequest.Parameter("@pid_FechaCobro", DbType.String, 8, oEImpuestoMunicipal.Fechacobro, ParameterDirection.Input)

        prmParameter(13) = New DAABRequest.Parameter("@pin_TipoDeCambio", DbType.Decimal)
        prmParameter(13).Precision = 18
        prmParameter(13).Scale = 6
        prmParameter(13).Value = oEImpuestoMunicipal.Tipodecambio

        prmParameter(14) = New DAABRequest.Parameter("@piv_NroCheque", DbType.String, 8, oEImpuestoMunicipal.Nrocheque, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@piv_NroPartidaRegistral", DbType.String, 8, oEImpuestoMunicipal.Nropartidaregistral, ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@piv_CodigoMoneda", DbType.String, 8, oEImpuestoMunicipal.Codigomoneda, ParameterDirection.Input)

        prmParameter(17) = New DAABRequest.Parameter("@pin_TotalAutovaluo", DbType.Decimal)
        prmParameter(17).Precision = 18
        prmParameter(17).Scale = 6
        prmParameter(17).Value = oEImpuestoMunicipal.TotalAutovaluo

        prmParameter(18) = New DAABRequest.Parameter("@pin_TotalPredial", DbType.Decimal)
        prmParameter(18).Precision = 18
        prmParameter(18).Scale = 6
        prmParameter(18).Value = oEImpuestoMunicipal.TotalPredial

        prmParameter(19) = New DAABRequest.Parameter("@pii_Periodo", DbType.Int16, 0, oEImpuestoMunicipal.Periodo, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@piv_CodPredio", DbType.String, 20, oEImpuestoMunicipal.CodPredio, ParameterDirection.Input)

        prmParameter(21) = New DAABRequest.Parameter("@pin_Autovaluo", DbType.Decimal)
        prmParameter(21).Precision = 18
        prmParameter(21).Scale = 6
        prmParameter(21).Value = oEImpuestoMunicipal.Autovaluo

        prmParameter(22) = New DAABRequest.Parameter("@pin_ImpuestoPredial", DbType.Decimal)
        prmParameter(22).Precision = 18
        prmParameter(22).Scale = 6
        prmParameter(22).Value = oEImpuestoMunicipal.ImpuestoPredial

        prmParameter(23) = New DAABRequest.Parameter("@pin_Arbitrio", DbType.Decimal)
        prmParameter(23).Precision = 18
        prmParameter(23).Scale = 6
        prmParameter(23).Value = oEImpuestoMunicipal.Arbitrio

        prmParameter(24) = New DAABRequest.Parameter("@pin_Multa", DbType.Decimal)
        prmParameter(24).Precision = 18
        prmParameter(24).Scale = 6
        prmParameter(24).Value = oEImpuestoMunicipal.Multa

        prmParameter(25) = New DAABRequest.Parameter("@pin_Fiscalizacion", DbType.String, 1, oEImpuestoMunicipal.FiscalizacionChk, ParameterDirection.Input)

        prmParameter(26) = New DAABRequest.Parameter("@pin_Total", DbType.Decimal)
        prmParameter(26).Precision = 18
        prmParameter(26).Scale = 6
        prmParameter(26).Value = oEImpuestoMunicipal.Total

        prmParameter(27) = New DAABRequest.Parameter("@piv_PagoCliente", DbType.String, 8, oEImpuestoMunicipal.PagoCliente, ParameterDirection.Input)
        prmParameter(28) = New DAABRequest.Parameter("@piv_EstadoPago", DbType.String, 8, oEImpuestoMunicipal.EstadoPago, ParameterDirection.Input)
        prmParameter(29) = New DAABRequest.Parameter("@piv_EstadoCobro", DbType.String, 8, oEImpuestoMunicipal.EstadoCobro, ParameterDirection.Input)

        prmParameter(30) = New DAABRequest.Parameter("@pin_IGV", DbType.Decimal)
        prmParameter(30).Precision = 18
        prmParameter(30).Scale = 6
        prmParameter(30).Value = oEImpuestoMunicipal.IGV

        prmParameter(31) = New DAABRequest.Parameter("@pin_Total2", DbType.Decimal)
        prmParameter(31).Precision = 18
        prmParameter(31).Scale = 6
        prmParameter(31).Value = oEImpuestoMunicipal.Total2

        prmParameter(32) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 8, oEImpuestoMunicipal.Lote, ParameterDirection.Input)
        prmParameter(33) = New DAABRequest.Parameter("@pii_EstadoLogico", DbType.Int16, 0, oEImpuestoMunicipal.Estadologico, ParameterDirection.Input)
        'Inicio IBK - AAE - Agrego parámetros
        prmParameter(34) = New DAABRequest.Parameter("@pic_CobroAdelantado", DbType.String, 1, oEImpuestoMunicipal.CobroAdelantado, ParameterDirection.Input)
        prmParameter(35) = New DAABRequest.Parameter("@pic_outval", DbType.String, 255, "", ParameterDirection.InputOutput)
        prmParameter(36) = New DAABRequest.Parameter("@pic_NoComision", DbType.String, 1, oEImpuestoMunicipal.NoComision, ParameterDirection.Input)
        'Fin IBK
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ImpMunicipal_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InsertarImpuestoMunicipal", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parLote = CType(obRequest.Parameters(35), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        'Return True
        strResultado = CFunciones.CheckStr(parLote.Value.ToString())
        Return strResultado
        'Fin IBK
    End Function

    ''' <summary>
    ''' Modificar ImpuestoMunicipal
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Entidad Serializada de ImpuestoMunicipal formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    ''' 'Inicio IBK - AAE - Retorno un string
    Public Function ModificarImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String Implements IImpuestoMunicipalTx.ModificarImpuestoMunicipal
        'Inicio IBK - AAE
        Dim oEImpuestoMunicipal As New EImpuestomunicipal
        Dim strResultado As String
        Dim parLote As IDataParameter
        'Dim prmParameter(33) As DAABRequest.Parameter
        Dim prmParameter(36) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEImpuestoMunicipal = CFunciones.DeserializeObject(Of EImpuestomunicipal)(pEImpuestoMunicipal)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oEImpuestoMunicipal.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinanciamiento", DbType.Int16, 0, oEImpuestoMunicipal.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pii_SecImpuesto", DbType.Int16, 0, oEImpuestoMunicipal.Secimpuesto, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 8, oEImpuestoMunicipal.Codunico, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pid_FecPago", DbType.String, 8, oEImpuestoMunicipal.Fecpago, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pid_FecLimite", DbType.String, 8, oEImpuestoMunicipal.Feclimite, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Municipalidad", DbType.String, 8, oEImpuestoMunicipal.Municipalidad, ParameterDirection.Input)

        prmParameter(7) = New DAABRequest.Parameter("@pin_Monto", DbType.Decimal)
        prmParameter(7).Precision = 20
        prmParameter(7).Scale = 2
        prmParameter(7).Value = oEImpuestoMunicipal.Monto

        prmParameter(8) = New DAABRequest.Parameter("@piv_Concepto", DbType.String, 8, oEImpuestoMunicipal.Concepto, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_Observaciones", DbType.String, 150, oEImpuestoMunicipal.Observaciones, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, oEImpuestoMunicipal.Usuariomodificacion, ParameterDirection.Input)

        prmParameter(11) = New DAABRequest.Parameter("@pin_Comision", DbType.Decimal)
        prmParameter(11).Precision = 20
        prmParameter(11).Scale = 2
        prmParameter(11).Value = oEImpuestoMunicipal.Comision

        prmParameter(12) = New DAABRequest.Parameter("@pid_FechaCobro", DbType.String, 8, oEImpuestoMunicipal.Fechacobro, ParameterDirection.Input)

        prmParameter(13) = New DAABRequest.Parameter("@pin_TipoDeCambio", DbType.Decimal)
        prmParameter(13).Precision = 18
        prmParameter(13).Scale = 6
        prmParameter(13).Value = oEImpuestoMunicipal.Tipodecambio

        prmParameter(14) = New DAABRequest.Parameter("@piv_NroCheque", DbType.String, 8, oEImpuestoMunicipal.Nrocheque, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@piv_NroPartidaRegistral", DbType.String, 8, oEImpuestoMunicipal.Nropartidaregistral, ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@piv_CodigoMoneda", DbType.String, 8, oEImpuestoMunicipal.Codigomoneda, ParameterDirection.Input)

        prmParameter(17) = New DAABRequest.Parameter("@pin_TotalAutovaluo", DbType.Decimal)
        prmParameter(17).Precision = 18
        prmParameter(17).Scale = 6
        prmParameter(17).Value = oEImpuestoMunicipal.TotalAutovaluo

        prmParameter(18) = New DAABRequest.Parameter("@pin_TotalPredial", DbType.Decimal)
        prmParameter(18).Precision = 18
        prmParameter(18).Scale = 6
        prmParameter(18).Value = oEImpuestoMunicipal.TotalPredial

        prmParameter(19) = New DAABRequest.Parameter("@pii_Periodo", DbType.Int16, 0, oEImpuestoMunicipal.Periodo, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@piv_CodPredio", DbType.String, 20, oEImpuestoMunicipal.CodPredio, ParameterDirection.Input)

        prmParameter(21) = New DAABRequest.Parameter("@pin_Autovaluo", DbType.Decimal)
        prmParameter(21).Precision = 18
        prmParameter(21).Scale = 6
        prmParameter(21).Value = oEImpuestoMunicipal.Autovaluo

        prmParameter(22) = New DAABRequest.Parameter("@pin_ImpuestoPredial", DbType.Decimal)
        prmParameter(22).Precision = 18
        prmParameter(22).Scale = 6
        prmParameter(22).Value = oEImpuestoMunicipal.ImpuestoPredial

        prmParameter(23) = New DAABRequest.Parameter("@pin_Arbitrio", DbType.Decimal)
        prmParameter(23).Precision = 18
        prmParameter(23).Scale = 6
        prmParameter(23).Value = oEImpuestoMunicipal.Arbitrio

        prmParameter(24) = New DAABRequest.Parameter("@pin_Multa", DbType.Decimal)
        prmParameter(24).Precision = 18
        prmParameter(24).Scale = 6
        prmParameter(24).Value = oEImpuestoMunicipal.Multa

        prmParameter(25) = New DAABRequest.Parameter("@pin_Fiscalizacion", DbType.String, 1, oEImpuestoMunicipal.FiscalizacionChk, ParameterDirection.Input)
        prmParameter(26) = New DAABRequest.Parameter("@pin_Total", DbType.Decimal)
        prmParameter(26).Precision = 18
        prmParameter(26).Scale = 6
        prmParameter(26).Value = oEImpuestoMunicipal.Total

        prmParameter(27) = New DAABRequest.Parameter("@piv_PagoCliente", DbType.String, 8, oEImpuestoMunicipal.PagoCliente, ParameterDirection.Input)
        prmParameter(28) = New DAABRequest.Parameter("@piv_EstadoPago", DbType.String, 8, oEImpuestoMunicipal.EstadoPago, ParameterDirection.Input)
        prmParameter(29) = New DAABRequest.Parameter("@piv_EstadoCobro", DbType.String, 8, oEImpuestoMunicipal.EstadoCobro, ParameterDirection.Input)

        prmParameter(30) = New DAABRequest.Parameter("@pin_IGV", DbType.Decimal)
        prmParameter(30).Precision = 18
        prmParameter(30).Scale = 6
        prmParameter(30).Value = oEImpuestoMunicipal.IGV

        prmParameter(31) = New DAABRequest.Parameter("@pin_Total2", DbType.Decimal)
        prmParameter(31).Precision = 18
        prmParameter(31).Scale = 6
        prmParameter(31).Value = oEImpuestoMunicipal.Total2

        prmParameter(32) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 8, oEImpuestoMunicipal.Lote, ParameterDirection.Input)
        prmParameter(33) = New DAABRequest.Parameter("@pii_EstadoLogico", DbType.Int16, 0, oEImpuestoMunicipal.Estadologico, ParameterDirection.Input)
        'Inicio IBK - AAE - Agrego parámetros
        prmParameter(34) = New DAABRequest.Parameter("@pic_CobroAdelantado", DbType.String, 1, oEImpuestoMunicipal.CobroAdelantado, ParameterDirection.Input)
        prmParameter(35) = New DAABRequest.Parameter("@pic_outval", DbType.String, 255, "", ParameterDirection.InputOutput)
        prmParameter(36) = New DAABRequest.Parameter("@pic_NoComision", DbType.String, 1, oEImpuestoMunicipal.NoComision, ParameterDirection.Input)
        'Fin IBK
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ImpMunicipal_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarImpuestoMunicipal", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parLote = CType(obRequest.Parameters(35), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        'Return True
        strResultado = CFunciones.CheckStr(parLote.Value.ToString())
        Return strResultado
        'Fin IBK
    End Function

    ''' <summary>
    ''' Eliminar el ImpuestoMunicipal
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Entidad Serializada de ImpuestoMunicipal formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    Public Function EliminarImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As Boolean Implements IImpuestoMunicipalTx.EliminarImpuestoMunicipal

        Dim oEImpuestoMunicipal As New EImpuestomunicipal
        'Dim prmParameter(1) As DAABRequest.Parameter
        Dim prmParameter(2) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEImpuestoMunicipal = CFunciones.DeserializeObject(Of EImpuestomunicipal)(pEImpuestoMunicipal)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodigosImpuesto", DbType.String, 8000, oEImpuestoMunicipal.CodigosImpuestos, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, oEImpuestoMunicipal.Usuariomodificacion, ParameterDirection.Input)
        'Inicio IBK - AAE
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodNroLote", DbType.String, 8, oEImpuestoMunicipal.Lote, ParameterDirection.Input)
        'Fin IBK
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ImpMunicipal_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "EliminarImpuestoMunicipal", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' AsignarLoteImpuestoMunicipal
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AsignarLoteImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String Implements IImpuestoMunicipalTx.AsignarLoteImpuestoMunicipal
        Dim parCodigoLote As IDataParameter
        Dim oEImpuestoMunicipal As EImpuestomunicipal

        Dim prmParameter(8) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEImpuestoMunicipal = CFunciones.DeserializeObject(Of EImpuestomunicipal)(pEImpuestoMunicipal)

        'Campos         
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodigosImpuesto", DbType.String, 8000, oEImpuestoMunicipal.CodigosImpuestos, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_CodigoLote", DbType.String, 10, oEImpuestoMunicipal.Lote, ParameterDirection.InputOutput)
        prmParameter(2) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, oEImpuestoMunicipal.Usuariomodificacion, ParameterDirection.Input)

        prmParameter(3) = New DAABRequest.Parameter("@piv_RegeneraLote", DbType.String, 8, oEImpuestoMunicipal.TieneLote, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 8, oEImpuestoMunicipal.Lote, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Municipalidad", DbType.String, 3, oEImpuestoMunicipal.Municipalidad, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_TotalAutovaluo", DbType.String, 30, oEImpuestoMunicipal.TotalAutovaluo, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_TotalPredial", DbType.String, 30, oEImpuestoMunicipal.TotalPredial, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_Periodo", DbType.String, 30, oEImpuestoMunicipal.Periodo, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ImpMunicipalLote_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Generar Lote Impuesto Municipal", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parCodigoLote = CType(obRequest.Parameters(1), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckStr(parCodigoLote.Value.ToString())

    End Function

    ''' <summary>
    ''' AsignarChequeImpuestoMunicipal
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AsignarChequeImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As Boolean Implements IImpuestoMunicipalTx.AsignarChequeImpuestoMunicipal

        Dim oEImpuestoMunicipal As EImpuestomunicipal
        Dim prmParameter(5) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEImpuestoMunicipal = CFunciones.DeserializeObject(Of EImpuestomunicipal)(pEImpuestoMunicipal)

        'Campos         
        prmParameter(0) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, oEImpuestoMunicipal.Usuariomodificacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_NroCheque", DbType.String, 20, oEImpuestoMunicipal.Nrocheque, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 10, oEImpuestoMunicipal.Lote, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_FechaCobro", DbType.String, 10, oEImpuestoMunicipal.FechacobroStr, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_FechaPago", DbType.String, 10, oEImpuestoMunicipal.FecpagoStr, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_cantDias", DbType.Int32, 4, oEImpuestoMunicipal.CantDias, ParameterDirection.Input) 'JJM IBK
        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ImpMunicipalCheque_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Asigna Cheque Impuesto Municipal", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally

            obRequest.Factory.Dispose()
        End Try

        Return True

    End Function
    'JJM IBK
    'Eliminar Lote Municipal
    Public Function EliminarLoteImpuestoMunicipal(ByVal pNroLote As String) As Boolean Implements IImpuestoMunicipalTx.EliminarLoteImpuestoMunicipal
        Dim parCodigoLote As IDataParameter
        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad

        'Campos         
        prmParameter(0) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 8, pNroLote, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "UP_GCC_Eliminar_Lote_Municpal"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Generar Lote Impuesto Municipal", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parCodigoLote = CType(obRequest.Parameters(1), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckStr(parCodigoLote.Value.ToString())

    End Function

    
#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DImpuestoMunicipalNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 12/11/2012
''' </remarks>
<Guid("800454bf-b173-4929-9759-e53404d0a4b0") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DImpuestoMunicipalNTx")> _
Public Class DImpuestoMunicipalNTx
    Inherits ServicedComponent
    Implements IImpuestoMunicipalNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DImpuestoMunicipalNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene el ImpuestoMunicipal Contrato
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012 
    ''' </remarks>
    Public Function GetImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String Implements IImpuestoMunicipalNTx.GetImpuestoMunicipal

        Dim odtbListadoCotizacionDoc As DataTable
        Dim prmParameter(2) As DAABRequest.Parameter
        Dim oEImpuestoMunicipal As New EImpuestomunicipal
        oEImpuestoMunicipal = CFunciones.DeserializeObject(Of EImpuestomunicipal)(pEImpuestoMunicipal)

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codsolcredito", DbType.String, 8, oEImpuestoMunicipal.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_Secfinanciamiento", DbType.Int16, 0, oEImpuestoMunicipal.Secfinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pii_Secimpuesto", DbType.Int16, 0, oEImpuestoMunicipal.Secimpuesto, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ImpMunicipal_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCotizacionDoc = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GetImpuestoMunicipal", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
    Public Function ListadoImpuestoMunicipal(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEImpuestoMunicipal As String _
                                                ) As String Implements IImpuestoMunicipalNTx.ListadoImpuestoMunicipal

        Dim odtbListadoImpuestoMunicipal As DataTable
        ' Inicio IBK - AAE
        'Dim prmParameter(17) As DAABRequest.Parameter
        Dim prmParameter(18) As DAABRequest.Parameter
        ' Fin IBK
        Dim oEImpuestoMunicipal As New EImpuestomunicipal
        oEImpuestoMunicipal = CFunciones.DeserializeObject(Of EImpuestomunicipal)(pEImpuestoMunicipal)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@Departamento", DbType.String, 3, oEImpuestoMunicipal.Departamento, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@Provincia", DbType.String, 3, oEImpuestoMunicipal.Provincia, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@Distrito", DbType.String, 3, oEImpuestoMunicipal.Distrito, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@NroContrato", DbType.String, 8, oEImpuestoMunicipal.NroContrato, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@RazonSocial", DbType.String, 100, oEImpuestoMunicipal.RazonSocial, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@TipoDocumento", DbType.String, 3, oEImpuestoMunicipal.TipoDocumento, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@NroDocumento", DbType.String, 15, oEImpuestoMunicipal.NroDocumento, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@Periodo", DbType.String, 4, oEImpuestoMunicipal.Periodo, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@Lote", DbType.String, 10, oEImpuestoMunicipal.Lote, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@EstadoPago", DbType.String, 3, oEImpuestoMunicipal.EstadoPago, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@EstadoCobro", DbType.String, 3, oEImpuestoMunicipal.EstadoCobro, ParameterDirection.Input)
            prmParameter(15) = New DAABRequest.Parameter("@TieneLote", DbType.String, 3, oEImpuestoMunicipal.TieneLote, ParameterDirection.Input)
            prmParameter(16) = New DAABRequest.Parameter("@Codsolcredito", DbType.String, 8, oEImpuestoMunicipal.Codsolcredito, ParameterDirection.Input)
            prmParameter(17) = New DAABRequest.Parameter("@Secfinanciamiento", DbType.Int16, 0, oEImpuestoMunicipal.Secfinanciamiento, ParameterDirection.Input)

            prmParameter(18) = New DAABRequest.Parameter("@Municipalidad", DbType.String, 3, oEImpuestoMunicipal.Municipalidad, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ImpMunicipal_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoImpuestoMunicipal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoImpuestoMunicipal", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoImpuestoMunicipal)
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function ListadoImpuestoMunicipalBienes(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEImpuestoMunicipal As String _
                                                ) As String Implements IImpuestoMunicipalNTx.ListadoImpuestoMunicipalBienes

        Dim odtbListadoImpuestoMunicipal As DataTable
        ' Inicio IBK 
        'Dim prmParameter(9) As DAABRequest.Parameter
        Dim prmParameter(10) As DAABRequest.Parameter
        ' Fin IBK
        Dim oEImpuestoMunicipal As New EImpuestomunicipal
        oEImpuestoMunicipal = CFunciones.DeserializeObject(Of EImpuestomunicipal)(pEImpuestoMunicipal)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@Departamento", DbType.String, 3, oEImpuestoMunicipal.Departamento, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@Provincia", DbType.String, 3, oEImpuestoMunicipal.Provincia, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@Distrito", DbType.String, 3, oEImpuestoMunicipal.Distrito, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@Ubicacion", DbType.String, 100, oEImpuestoMunicipal.Ubicacion, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@Secfinanciamiento", DbType.String, 5, oEImpuestoMunicipal.Secfinanciamiento, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@Codsolcredito", DbType.String, 8, oEImpuestoMunicipal.Codsolcredito, ParameterDirection.Input)

            prmParameter(10) = New DAABRequest.Parameter("@Municipalidad", DbType.String, 8, oEImpuestoMunicipal.Municipalidad, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ImpMunicipalBienes_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoImpuestoMunicipal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoImpuestoMunicipal", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoImpuestoMunicipal)
    End Function

    ''' <summary>
    ''' Obtiene el ImpuestoMunicipal Bienes
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 25/11/2012 
    ''' </remarks>
    Public Function GetImpuestoMunicipalBienes(ByVal pEImpuestoMunicipal As String) As String Implements IImpuestoMunicipalNTx.GetImpuestoMunicipalBienes

        Dim odtbListadoCotizacionDoc As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter
        Dim oEImpuestoMunicipal As New EImpuestomunicipal
        oEImpuestoMunicipal = CFunciones.DeserializeObject(Of EImpuestomunicipal)(pEImpuestoMunicipal)

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codsolcredito", DbType.String, 8, oEImpuestoMunicipal.Codsolcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_Secfinanciamiento", DbType.Int16, 0, oEImpuestoMunicipal.Secfinanciamiento, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ImpMunicipalBienes_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCotizacionDoc = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GetImpuestoMunicipalBienes", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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


    Public Function ListarImpuestoMunicipalLiquidar(ByVal pPageSize As Integer, _
                                   ByVal pCurrentPage As Integer, _
                                   ByVal pSortColumn As String, _
                                   ByVal pSortOrder As String, _
                                   ByVal pCodLote As String) As String Implements IImpuestoMunicipalNTx.ListarImpuestoMunicipalLiquidar

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
            objRequest.Command = "UP_GCC_IMPUESTO_MUNICIPAL_LIQUIDAR_SEL"
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
    Public Function ListarImpuestoMunicipalLiquidarTodo(ByVal pCodLote As String) As String Implements IImpuestoMunicipalNTx.ListarImpuestoMunicipalLiquidarTodo

        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_NroLote", DbType.String, 10, pCodLote, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_IMPUESTO_MUNICIPAL_LIQUIDAR_TODO_SEL"
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
    Public Function ListarImpuestoMunicipalReporteLiquidar(ByVal pCodigoImpuesto As String) As String Implements IImpuestoMunicipalNTx.ListarImpuestoMunicipalReporteLiquidar


        Dim odtoEImpuestoVehicular As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodigosImpuesto", DbType.String, 8000, pCodigoImpuesto, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_IMPUESTO_MUNICIPAL_REPORTE_LIQUIDAR_SEL"
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

    'Inicio IBK
    Public Function GetImpuestoMultasInmueble(ByVal pNroLote As String) As String Implements IImpuestoMunicipalNTx.GetImpuestoMultasInmueble
        Dim odtbImpuestoMultasInmueble As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_NroLote", DbType.String, 8, pNroLote, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_ObtieneImpuestosyMultasLote_get"
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

    Public Function GetCodigoPredioBien(ByVal pCodSolicitud As String, ByVal pSecFinanciamiento As String) As String Implements IImpuestoMunicipalNTx.GetCodigoPredioBien
        Dim odtbImpuestoMultasInmueble As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@CodSolicitudCredito", DbType.String, 10, pCodSolicitud, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@SecFinanciamiento", DbType.String, 4, pSecFinanciamiento, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_SelCodigoPredio_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbImpuestoMultasInmueble = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GetCodigoPredioBien", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
    'fin IBK

    ''' <summary>
    ''' Obtiene el ImpuestoMunicipal Contrato
    ''' </summary>
    ''' <param name="pEImpuestoMunicipal">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 12/11/2012 
    ''' </remarks>
    Public Function ListadoReporteImpuestoMunicipal(ByVal pEImpuestoMunicipal As String) As String Implements IImpuestoMunicipalNTx.ListadoReporteImpuestoMunicipal

        Dim odtbListadoImpuestoMunicipal As DataTable
        Dim prmParameter(13) As DAABRequest.Parameter
        Dim oEImpuestoMunicipal As New EImpuestomunicipal
        oEImpuestoMunicipal = CFunciones.DeserializeObject(Of EImpuestomunicipal)(pEImpuestoMunicipal)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@Departamento", DbType.String, 3, oEImpuestoMunicipal.Departamento, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@Provincia", DbType.String, 3, oEImpuestoMunicipal.Provincia, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@Distrito", DbType.String, 3, oEImpuestoMunicipal.Distrito, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@NroContrato", DbType.String, 8, oEImpuestoMunicipal.NroContrato, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@RazonSocial", DbType.String, 100, oEImpuestoMunicipal.RazonSocial, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@TipoDocumento", DbType.String, 3, oEImpuestoMunicipal.TipoDocumento, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@NroDocumento", DbType.String, 15, oEImpuestoMunicipal.NroDocumento, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@Periodo", DbType.String, 4, oEImpuestoMunicipal.Periodo, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@Lote", DbType.String, 10, oEImpuestoMunicipal.Lote, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@EstadoPago", DbType.String, 3, oEImpuestoMunicipal.EstadoPago, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@EstadoCobro", DbType.String, 3, oEImpuestoMunicipal.EstadoCobro, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@TieneLote", DbType.String, 3, oEImpuestoMunicipal.TieneLote, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@Codsolcredito", DbType.String, 8, oEImpuestoMunicipal.Codsolcredito, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@Secfinanciamiento", DbType.Int16, 0, oEImpuestoMunicipal.Secfinanciamiento, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ImpMunicipal_rep"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoImpuestoMunicipal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoImpuestoMunicipal", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoImpuestoMunicipal)
    End Function

    'Inicio IBK
    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 12/11/2012
    ''' </remarks>
    Public Function ListadoImpuestoMunicipalxLote(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEImpuestoMunicipal As String _
                                                ) As String Implements IImpuestoMunicipalNTx.ListadoImpuestoMunicipalxLote

        Dim odtbListadoImpuestoMunicipal As DataTable
        ' Inicio IBK - AAE
        'Dim prmParameter(17) As DAABRequest.Parameter
        Dim prmParameter(18) As DAABRequest.Parameter
        ' Fin IBK
        Dim oEImpuestoMunicipal As New EImpuestomunicipal
        oEImpuestoMunicipal = CFunciones.DeserializeObject(Of EImpuestomunicipal)(pEImpuestoMunicipal)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@Departamento", DbType.String, 3, oEImpuestoMunicipal.Departamento, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@Provincia", DbType.String, 3, oEImpuestoMunicipal.Provincia, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@Distrito", DbType.String, 3, oEImpuestoMunicipal.Distrito, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@NroContrato", DbType.String, 8, oEImpuestoMunicipal.NroContrato, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@RazonSocial", DbType.String, 100, oEImpuestoMunicipal.RazonSocial, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@TipoDocumento", DbType.String, 3, oEImpuestoMunicipal.TipoDocumento, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@NroDocumento", DbType.String, 15, oEImpuestoMunicipal.NroDocumento, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@Periodo", DbType.String, 4, oEImpuestoMunicipal.Periodo, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@Lote", DbType.String, 10, oEImpuestoMunicipal.Lote, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@EstadoPago", DbType.String, 3, oEImpuestoMunicipal.EstadoPago, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@EstadoCobro", DbType.String, 3, oEImpuestoMunicipal.EstadoCobro, ParameterDirection.Input)
            prmParameter(15) = New DAABRequest.Parameter("@TieneLote", DbType.String, 3, oEImpuestoMunicipal.TieneLote, ParameterDirection.Input)
            prmParameter(16) = New DAABRequest.Parameter("@Codsolcredito", DbType.String, 8, oEImpuestoMunicipal.Codsolcredito, ParameterDirection.Input)
            prmParameter(17) = New DAABRequest.Parameter("@Secfinanciamiento", DbType.Int16, 0, oEImpuestoMunicipal.Secfinanciamiento, ParameterDirection.Input)
            prmParameter(18) = New DAABRequest.Parameter("@Municipalidad", DbType.String, 3, oEImpuestoMunicipal.Municipalidad, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ImpMunicipal_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoImpuestoMunicipal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoImpuestoMunicipal", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoImpuestoMunicipal)
    End Function

    'Inicio IBK
    Public Function GetImpuestoTotalesInmueble(ByVal pEImpuestoMunicipal As String) As String Implements IImpuestoMunicipalNTx.GetImpuestoTotalesInmueble
        Dim odtbListadoImpuestoMunicipal As DataTable

        Dim prmParameter(1) As DAABRequest.Parameter

        Dim oEImpuestoMunicipal As New EImpuestomunicipal
        oEImpuestoMunicipal = CFunciones.DeserializeObject(Of EImpuestomunicipal)(pEImpuestoMunicipal)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try

            prmParameter(0) = New DAABRequest.Parameter("@Periodo", DbType.String, 4, oEImpuestoMunicipal.Periodo, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@Municipalidad", DbType.String, 3, oEImpuestoMunicipal.Municipalidad, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_VALIDA_TOTAL"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoImpuestoMunicipal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoImpuestoMunicipal", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoImpuestoMunicipal)
    End Function
    'Fin IBK
    'JJM IBK
    'Aplica Descuento
    Public Function DescuentoLoteImpuestoMunicipal(ByVal pNroLote As String, ByVal pUsuarioModificacion As String, ByVal pDescuento As Decimal) As String Implements IImpuestoMunicipalNTx.DescuentoLoteImpuestoMunicipal
        Dim prmParameter(2) As DAABRequest.Parameter
        Dim odtbListadoImpuestoMunicipal As DataTable
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        'Deserealiza la Entidad

        'Campos         
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodNroLote", DbType.String, 8, pNroLote, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, pUsuarioModificacion, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_Descuento", DbType.Decimal)
        prmParameter(2).Precision = 18
        prmParameter(2).Scale = 6
        prmParameter(2).Value = pDescuento

        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_DescuentoImpMunicipalLote_upd"
        obRequest.Parameters.AddRange(prmParameter)
        Try
            odtbListadoImpuestoMunicipal = obRequest.Factory.ExecuteDataset(obRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Asigna Cheque Impuesto Municipal", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally

            obRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoImpuestoMunicipal)
        'Return True

    End Function
#End Region

End Class

#End Region
