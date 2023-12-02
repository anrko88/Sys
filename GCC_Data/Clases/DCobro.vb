Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports TSF.DAAB

Imports GCC.Common
Imports GCC.Entity


#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DCobroNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 27/11/2012
''' </remarks>
<Guid("6760BC27-0877-4EB0-96F2-FBF334969BD5") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCobroTx")> _
Public Class DCobroTx
    Inherits ServicedComponent
    Implements ICobroTx

#Region "   Constantes  "

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DCobroTx"

#End Region

#Region "   Métodos     "
    ''' <summary>
    ''' Inserta un nuevo cobro en las tablas CreditoRecuperacion y CreditoRecuperacionComision
    ''' </summary>
    ''' <param name="pECreditoRecuperacionComision">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    Public Function fblnInsertarCobro(ByVal pECreditoRecuperacionComision As String) As Boolean Implements ICobroTx.fblnInsertarCobro

        Dim oECreditoRecuperacionComision As New ECreditoRecuperacionComision
        Dim prmParameter(12) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oECreditoRecuperacionComision = CFunciones.DeserializeObject(Of ECreditoRecuperacionComision)(pECreditoRecuperacionComision)

        ' Campos para la operación de inserción
        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oECreditoRecuperacionComision.CodOperacionActiva, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodigoConcepto", DbType.String, 3, oECreditoRecuperacionComision.CodComisionTipo, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodMoneda", DbType.String, 3, oECreditoRecuperacionComision.CodMoneda, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pin_Importe", DbType.Decimal)
        prmParameter(3).Precision = 20
        prmParameter(3).Scale = 5
        prmParameter(3).Value = oECreditoRecuperacionComision.MontoReembolso
        prmParameter(4) = New DAABRequest.Parameter("@pin_Comision", DbType.Decimal)
        prmParameter(4).Precision = 20
        prmParameter(4).Scale = 5
        prmParameter(4).Value = oECreditoRecuperacionComision.MontoComision
        prmParameter(5) = New DAABRequest.Parameter("@pin_ComisionIGV", DbType.Decimal)
        prmParameter(5).Precision = 20
        prmParameter(5).Scale = 5
        prmParameter(5).Value = oECreditoRecuperacionComision.MontoIGV
        prmParameter(6) = New DAABRequest.Parameter("@pid_FechaCobro", DbType.DateTime, 10, oECreditoRecuperacionComision.FechaCobro, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_CodUsuario", DbType.String, 12, oECreditoRecuperacionComision.CodUsuario, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pii_NumeroSecuencia", DbType.Int32, 0, oECreditoRecuperacionComision.NumeroSecuencia, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_FlagIndividual", DbType.String, 1, oECreditoRecuperacionComision.FlagIndividual, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_Observaciones", DbType.String, 150, oECreditoRecuperacionComision.Observaciones, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_EstadoCobro", DbType.String, 1, oECreditoRecuperacionComision.EstadoCobro, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pil_IndicadorRegistro", DbType.Int64, 0, oECreditoRecuperacionComision.IndicadorRegistro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoCobro_Ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            Return True
        Catch ex As Exception
            Dim oLog As New CLog

            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnInsertarCobro", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing

            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Actualiza un nuevo cobro en las tablas CreditoRecuperacion y CreditoRecuperacionComision
    ''' </summary>
    ''' <param name="pECreditoRecuperacionComision">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    Public Function fblnModificarCobro(ByVal pECreditoRecuperacionComision As String) As Boolean Implements ICobroTx.fblnModificarCobro

        Dim oECreditoRecuperacionComision As New ECreditoRecuperacionComision
        Dim prmParameter(14) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oECreditoRecuperacionComision = CFunciones.DeserializeObject(Of ECreditoRecuperacionComision)(pECreditoRecuperacionComision)

        ' Campos para la operación de inserción
        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oECreditoRecuperacionComision.CodOperacionActiva, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_TipoRubroFinanciamiento", DbType.String, 3, oECreditoRecuperacionComision.TipoRubroFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodIfi", DbType.String, 4, oECreditoRecuperacionComision.CodIfi, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_TipoRecuperacion", DbType.String, 1, oECreditoRecuperacionComision.TipoRecuperacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pii_NumSecRecuperacion", DbType.Int16, 0, oECreditoRecuperacionComision.NumSecRecuperacion, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_CodigoConcepto", DbType.String, 3, oECreditoRecuperacionComision.CodComisionTipo, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pii_NumSecRecupComi", DbType.Int16, 0, oECreditoRecuperacionComision.NumSecRecupComi, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_CodMoneda", DbType.String, 3, oECreditoRecuperacionComision.CodMoneda, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pin_Importe", DbType.Decimal)
        prmParameter(8).Precision = 20
        prmParameter(8).Scale = 5
        prmParameter(8).Value = oECreditoRecuperacionComision.MontoReembolso
        prmParameter(9) = New DAABRequest.Parameter("@pin_Comision", DbType.Decimal)
        prmParameter(9).Precision = 20
        prmParameter(9).Scale = 5
        prmParameter(9).Value = oECreditoRecuperacionComision.MontoComision
        prmParameter(10) = New DAABRequest.Parameter("@pin_ComisionIGV", DbType.Decimal)
        prmParameter(10).Precision = 20
        prmParameter(10).Scale = 5
        prmParameter(10).Value = oECreditoRecuperacionComision.MontoIGV
        prmParameter(11) = New DAABRequest.Parameter("@pid_FechaCobro", DbType.DateTime, 10, oECreditoRecuperacionComision.FechaCobro, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pii_NumeroSecuencia", DbType.Int32, 0, oECreditoRecuperacionComision.NumeroSecuencia, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_FlagIndividual", DbType.String, 1, oECreditoRecuperacionComision.FlagIndividual, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@piv_Observaciones", DbType.String, 150, oECreditoRecuperacionComision.Observaciones, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoCobro_Upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            Return True
        Catch ex As Exception
            Dim oLog As New CLog

            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarCobro", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing

            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Elimina un nuevo cobro en las tablas CreditoRecuperacion y CreditoRecuperacionComision
    ''' </summary>
    ''' <param name="pECreditoRecuperacionComision">Entidad ESolicitudCreditoEstructura serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 30/11/2012
    ''' </remarks>
    Public Function fblnEliminarCobro(ByVal pECreditoRecuperacionComision As String) As Boolean Implements ICobroTx.fblnEliminarCobro

        Dim oECreditoRecuperacionComision As New ECreditoRecuperacionComision
        Dim prmParameter(6) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oECreditoRecuperacionComision = CFunciones.DeserializeObject(Of ECreditoRecuperacionComision)(pECreditoRecuperacionComision)

        ' Campos para la operación de inserción
        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oECreditoRecuperacionComision.CodOperacionActiva, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_TipoRubroFinanciamiento", DbType.String, 3, oECreditoRecuperacionComision.TipoRubroFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodIfi", DbType.String, 4, oECreditoRecuperacionComision.CodIfi, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_TipoRecuperacion", DbType.String, 1, oECreditoRecuperacionComision.TipoRecuperacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pii_NumSecRecuperacion", DbType.Int16, 0, oECreditoRecuperacionComision.NumSecRecuperacion, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_CodigoConcepto", DbType.String, 3, oECreditoRecuperacionComision.CodComisionTipo, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pii_NumSecRecupComi", DbType.Int16, 0, oECreditoRecuperacionComision.NumSecRecupComi, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoCobro_Del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            Return True
        Catch ex As Exception
            Dim oLog As New CLog

            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnEliminarCobro", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing

            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Inserta un registro en la tabla GCC_FraccionarCobro
    ''' </summary>
    ''' <param name="pstrEFraccionarCobro">Entidad EGCC_Fraccionar serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 13/12/2012
    ''' </remarks>
    Public Function fblnInsertarFraccionarCobro(ByVal pstrEFraccionarCobro As String) As Boolean Implements ICobroTx.fblnInsertarFraccionarCobro

        Dim oEFraccionarCobro As New EGCC_FraccionarCobro
        Dim prmParameter(15) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEFraccionarCobro = CFunciones.DeserializeObject(Of EGCC_FraccionarCobro)(pstrEFraccionarCobro)

        ' Campos para la operación de inserción
        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEFraccionarCobro.CodOperacionActiva, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_TipoRubroFinancionamiento", DbType.String, 3, oEFraccionarCobro.TipoRubroFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodIfi", DbType.String, 4, oEFraccionarCobro.CodIfi, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_TipoRecuperacion", DbType.String, 1, oEFraccionarCobro.TipoRecuperacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pis_NumSecRecuperacion", DbType.Int16, 0, oEFraccionarCobro.NumSecRecuperacion, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_CodComisionTipo", DbType.String, 3, oEFraccionarCobro.CodComisionTipo, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pis_NumSecRecupComi", DbType.Int16, 0, oEFraccionarCobro.NumSecRecupComi, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pis_NroCuota", DbType.Int16, 0, oEFraccionarCobro.NroCuota, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pid_FechaCobro", DbType.DateTime, 10, oEFraccionarCobro.FechaCobro, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pin_MontoFraccionar", DbType.Decimal)
        prmParameter(9).Precision = 20
        prmParameter(9).Scale = 5
        prmParameter(9).Value = oEFraccionarCobro.MontoFraccionar
        prmParameter(10) = New DAABRequest.Parameter("@pin_MontoComision", DbType.Decimal)
        prmParameter(10).Precision = 20
        prmParameter(10).Scale = 5
        prmParameter(10).Value = oEFraccionarCobro.MontoComision
        prmParameter(11) = New DAABRequest.Parameter("@pin_MontoIGVComision", DbType.Decimal)
        prmParameter(11).Precision = 20
        prmParameter(11).Scale = 5
        prmParameter(11).Value = oEFraccionarCobro.MontoIGVComision
        prmParameter(12) = New DAABRequest.Parameter("@pin_MontoInteres", DbType.Decimal)
        prmParameter(12).Precision = 20
        prmParameter(12).Scale = 5
        prmParameter(12).Value = oEFraccionarCobro.MontoInteres
        prmParameter(13) = New DAABRequest.Parameter("@pin_MontoTotal", DbType.Decimal)
        prmParameter(13).Precision = 20
        prmParameter(13).Scale = 5
        prmParameter(13).Value = oEFraccionarCobro.MontoTotal
        prmParameter(14) = New DAABRequest.Parameter("@pis_Dias", DbType.Int16, 0, oEFraccionarCobro.Dias, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@piv_UsuarioRegistro", DbType.String, 8, oEFraccionarCobro.UsuarioRegistro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_FraccionarCobro_Ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            Return True
        Catch ex As Exception
            Dim oLog As New CLog

            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnInsertarFraccionarCobro", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing

            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try
    End Function


    ''' <summary>
    ''' Actualiza un cobro en las tablas GCC_FraccionarCobro
    ''' </summary>
    ''' <param name="pstrEFraccionarCobro">Entidad EGCC_FraccionarCobro serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 13/12/2012
    ''' </remarks>
    Public Function fblnModificarFraccionarCobro(ByVal pstrEFraccionarCobro As String) As Boolean Implements ICobroTx.fblnModificarFraccionarCobro

        Dim oEFraccionarCobro As New EGCC_FraccionarCobro
        Dim prmParameter(15) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEFraccionarCobro = CFunciones.DeserializeObject(Of EGCC_FraccionarCobro)(pstrEFraccionarCobro)

        ' Campos para la operación de inserción
        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEFraccionarCobro.CodOperacionActiva, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_TipoRubroFinancionamiento", DbType.String, 3, oEFraccionarCobro.TipoRubroFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodIfi", DbType.String, 4, oEFraccionarCobro.CodIfi, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_TipoRecuperacion", DbType.String, 1, oEFraccionarCobro.TipoRecuperacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pis_NumSecRecuperacion", DbType.Int16, 0, oEFraccionarCobro.NumSecRecuperacion, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_CodComisionTipo", DbType.String, 3, oEFraccionarCobro.CodComisionTipo, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pis_NumSecRecupComi", DbType.Int16, 0, oEFraccionarCobro.NumSecRecupComi, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pis_NroCuota", DbType.Int16, 0, oEFraccionarCobro.NroCuota, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pid_FechaCobro", DbType.DateTime, 10, oEFraccionarCobro.FechaCobro, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pin_MontoFraccionar", DbType.Decimal)
        prmParameter(9).Precision = 20
        prmParameter(9).Scale = 5
        prmParameter(9).Value = oEFraccionarCobro.MontoFraccionar
        prmParameter(10) = New DAABRequest.Parameter("@pin_MontoComision", DbType.Decimal)
        prmParameter(10).Precision = 20
        prmParameter(10).Scale = 5
        prmParameter(10).Value = oEFraccionarCobro.MontoComision
        prmParameter(11) = New DAABRequest.Parameter("@pin_MontoIGVComision", DbType.Decimal)
        prmParameter(11).Precision = 20
        prmParameter(11).Scale = 5
        prmParameter(11).Value = oEFraccionarCobro.MontoIGVComision
        prmParameter(12) = New DAABRequest.Parameter("@pin_MontoInteres", DbType.Decimal)
        prmParameter(12).Precision = 20
        prmParameter(12).Scale = 5
        prmParameter(12).Value = oEFraccionarCobro.MontoInteres
        prmParameter(13) = New DAABRequest.Parameter("@pin_MontoTotal", DbType.Decimal)
        prmParameter(13).Precision = 20
        prmParameter(13).Scale = 5
        prmParameter(13).Value = oEFraccionarCobro.MontoTotal
        prmParameter(14) = New DAABRequest.Parameter("@pis_Dias", DbType.Int16, 0, oEFraccionarCobro.Dias, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@piv_UsuarioModificacion", DbType.String, 8, oEFraccionarCobro.UsuarioModificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_FraccionarCobro_Upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            Return True
        Catch ex As Exception
            Dim oLog As New CLog

            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarFraccionarCobro", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing

            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Elimina un cobro en las tablas GCC_FraccionarCobro
    ''' </summary>
    ''' <param name="pstrEFraccionarCobro">Entidad EGCC_FraccionarCobro serializada</param>    
    ''' <returns>Boolean si se registro es True en caso contrario False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/12/2012
    ''' </remarks>
    Public Function fblnEliminarFraccionarCobro(ByVal pstrEFraccionarCobro As String) As Boolean Implements ICobroTx.fblnEliminarFraccionarCobro

        Dim oEFraccionarCobro As New EGCC_FraccionarCobro
        Dim prmParameter(7) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEFraccionarCobro = CFunciones.DeserializeObject(Of EGCC_FraccionarCobro)(pstrEFraccionarCobro)

        ' Campos para la operación de inserción
        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEFraccionarCobro.CodOperacionActiva, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_TipoRubroFinanciamiento", DbType.String, 3, oEFraccionarCobro.TipoRubroFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodIfi", DbType.String, 4, oEFraccionarCobro.CodIfi, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_TipoRecuperacion", DbType.String, 1, oEFraccionarCobro.TipoRecuperacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pii_NumSecRecuperacion", DbType.Int16, 0, oEFraccionarCobro.NumSecRecuperacion, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_CodigoConcepto", DbType.String, 3, oEFraccionarCobro.CodComisionTipo, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pii_NumSecRecupComi", DbType.Int16, 0, oEFraccionarCobro.NumSecRecupComi, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pis_NroCuota", DbType.Int16, 0, oEFraccionarCobro.NroCuota, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_FraccionarCobro_Del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            Return True
        Catch ex As Exception
            Dim oLog As New CLog

            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnEliminarFraccionarCobro", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing

            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try
    End Function
#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DCobroNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 27/11/2012
''' </remarks>
<Guid("ADBCD93E-8155-40F8-854D-90C18FE3F872") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCobroNTx")> _
Public Class DCobroNTx
    Inherits ServicedComponent
    Implements ICobroNTx


#Region "   Constantes  "

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DCobroNTx"

#End Region

#Region "   Métodos     "

    ''' <summary>
    ''' Obtener dato del cobro   
    ''' </summary>
    ''' <param name="pECreditoRecuperacionComision">Código concepto</param>
    ''' <returns>Objeto Datatable(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 29/11/2012
    ''' </remarks>
    Public Function ObtenerCobro(ByVal pECreditoRecuperacionComision As String) As String Implements ICobroNTx.ObtenerCobro
        Dim odtbListadoBien As DataTable
        Dim prmParameter(6) As DAABRequest.Parameter
        Dim objECreditoRecuperacionComision As New ECreditoRecuperacionComision

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try

            'Deserealiza la Entidad
            objECreditoRecuperacionComision = CFunciones.DeserializeObject(Of ECreditoRecuperacionComision)(pECreditoRecuperacionComision)

            'Campos para la consulta.
            prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, objECreditoRecuperacionComision.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_TipoRubroFinanciamiento", DbType.String, 3, objECreditoRecuperacionComision.TipoRubroFinanciamiento, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_CodIfi", DbType.String, 4, objECreditoRecuperacionComision.CodIfi, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_TipoRecuperacion", DbType.String, 1, objECreditoRecuperacionComision.TipoRecuperacion, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pii_NumSecRecuperacion", DbType.Int16, 0, objECreditoRecuperacionComision.NumSecRecuperacion, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_CodigoConcepto", DbType.String, 3, objECreditoRecuperacionComision.CodComisionTipo, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pii_NumSecRecupComi", DbType.Int16, 0, objECreditoRecuperacionComision.NumSecRecupComi, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoCobro_Get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerCobro", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Devuelve el conjunto de cobros asociados a contratos hasta un máximo especificado (PageSize), agrupados en páginas
    ''' (CurrentPage). Permite especificar el orden (SortOrder).
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de página</param>
    ''' <param name="pCurrentPage">Página actual</param>
    ''' <param name="pSortColumn">Ordenar por columna</param>
    ''' <param name="pSortOrder">Dirección de ordenamiento</param>
    ''' <param name="pECreditoRecuperacionComision">Entidad serializada del objeto CreditoRecuperacionComision</param>
    ''' <returns>Listado de Cobro(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 27/11/2012
    ''' </remarks>
    Public Function ListadoCobro(ByVal pPageSize As Integer, _
                                  ByVal pCurrentPage As Integer, _
                                  ByVal pSortColumn As String, _
                                  ByVal pSortOrder As String, _
                                  ByVal pECreditoRecuperacionComision As String) As String Implements ICobroNTx.ListadoCobro
        Dim odtbListadoBien As DataTable
        Dim prmParameter(17) As DAABRequest.Parameter
        Dim objECreditoRecuperacionComision As New ECreditoRecuperacionComision

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        objECreditoRecuperacionComision = CFunciones.DeserializeObject(Of ECreditoRecuperacionComision)(pECreditoRecuperacionComision)

        Try
            'Campos para la consulta.
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, objECreditoRecuperacionComision.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_NumeroLote", DbType.String, 10, objECreditoRecuperacionComision.NumeroLote, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_RazonSocial", DbType.String, 150, objECreditoRecuperacionComision.RazonSocial, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_CodigoTipoDocumento", DbType.String, 100, objECreditoRecuperacionComision.CodigoTipoDocumento, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@piv_NumeroDocumento", DbType.String, 11, objECreditoRecuperacionComision.NumeroDocumento, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@piv_CodigoConcepto", DbType.String, 3, objECreditoRecuperacionComision.CodComisionTipo, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@piv_CodigoUnico", DbType.String, 10, objECreditoRecuperacionComision.CodigoUnico, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@piv_EstadoPago", DbType.String, 10, objECreditoRecuperacionComision.EstadoPago, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@piv_EstadoCobro", DbType.String, 10, objECreditoRecuperacionComision.EstadoCobro, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@piv_FlagIndividual", DbType.String, 1, objECreditoRecuperacionComision.FlagIndividual, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@pis_NumSecRecuperacion", DbType.Int16, 0, objECreditoRecuperacionComision.NumSecRecuperacion, ParameterDirection.Input)
            prmParameter(15) = New DAABRequest.Parameter("@pis_NumSecRecupComi", DbType.Int16, 0, objECreditoRecuperacionComision.NumSecRecupComi, ParameterDirection.Input)
            prmParameter(16) = New DAABRequest.Parameter("@piv_FlagRegistro", DbType.String, 1, objECreditoRecuperacionComision.FlagRegistro, ParameterDirection.Input)
            prmParameter(17) = New DAABRequest.Parameter("@pil_IndicadorRegistro", DbType.Int64, 0, objECreditoRecuperacionComision.IndicadorRegistro, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CreditoRecuperacionComision_Sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListaCobro", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Devuelve el conjunto de cobros asociados a contratos anterior, actual y siguiente 
    ''' (CurrentPage). Permite especificar el orden (SortOrder).
    ''' </summary>
    ''' <param name="pSortColumn">Ordenar por columna</param>
    ''' <param name="pSortOrder">Dirección de ordenamiento</param>
    ''' <param name="pECreditoRecuperacionComision">Entidad serializada del objeto CreditoRecuperacionComision</param>
    ''' <returns>Listado de Cobro(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 03/12/2012
    ''' </remarks>
    Public Function ListadoCobroPaginar(ByVal pSortColumn As String, _
                                          ByVal pSortOrder As String, _
                                          ByVal pECreditoRecuperacionComision As String) As String Implements ICobroNTx.ListadoCobroPaginar
        Dim odtbListadoBien As DataTable
        Dim prmParameter(14) As DAABRequest.Parameter
        Dim objECreditoRecuperacionComision As New ECreditoRecuperacionComision

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        objECreditoRecuperacionComision = CFunciones.DeserializeObject(Of ECreditoRecuperacionComision)(pECreditoRecuperacionComision)

        Try
            'Campos para la consulta.
            prmParameter(0) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(2) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, objECreditoRecuperacionComision.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_NumeroLote", DbType.String, 10, objECreditoRecuperacionComision.NumeroLote, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_RazonSocial", DbType.String, 150, objECreditoRecuperacionComision.RazonSocial, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_CodigoTipoDocumento", DbType.String, 100, objECreditoRecuperacionComision.CodigoTipoDocumento, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_NumeroDocumento", DbType.String, 11, objECreditoRecuperacionComision.NumeroDocumento, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_CodigoConcepto", DbType.String, 3, objECreditoRecuperacionComision.CodComisionTipo, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@piv_CodigoUnico", DbType.String, 10, objECreditoRecuperacionComision.CodigoUnico, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@piv_EstadoPago", DbType.String, 10, objECreditoRecuperacionComision.EstadoPago, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@piv_EstadoCobro", DbType.String, 10, objECreditoRecuperacionComision.EstadoCobro, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@pis_NumSecRecuperacion", DbType.Int16, 0, objECreditoRecuperacionComision.NumSecRecuperacion, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@pis_NumSecRecupComi", DbType.Int16, 0, objECreditoRecuperacionComision.NumSecRecupComi, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@pii_Item", DbType.Int32, 0, objECreditoRecuperacionComision.Item, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@pil_IndicadorRegistro", DbType.Int64, 0, objECreditoRecuperacionComision.IndicadorRegistro, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CreditoRecuperacionComisionPagina_Sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoCobroPaginar", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function


    ''' <summary>
    ''' Devuelve el conjunto de conceptos    
    ''' </summary>
    ''' <param name="pstrIDTabla">ID Tabla valor generica</param>
    ''' <returns>Listado de Concepto(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 27/11/2012
    ''' </remarks>
    Public Function ListadoConcepto(ByVal pstrIDTabla As String) As String Implements ICobroNTx.ListadoConcepto
        Dim odtbListadoBien As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos para la consulta.
            prmParameter(0) = New DAABRequest.Parameter("@piv_IDTabla", DbType.String, 18, pstrIDTabla, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ValorTablaGenericaConcepto_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoConcepto", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Devuelve los datos de un contrato    
    ''' </summary>
    ''' <param name="pstrNumeroContrato">Número de contrato</param>
    ''' <returns>Objeto Datatable(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 28/11/2012
    ''' </remarks>
    Public Function ObtenerContratoCobro(ByVal pstrNumeroContrato As String) As String Implements ICobroNTx.ObtenerContratoCobro
        Dim odtbListadoBien As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos para la consulta.
            prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, pstrNumeroContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoCobro_Sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerContratoCobro", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Realiza el calculo de la comisión del cobro   
    ''' </summary>
    ''' <param name="pstrCodigoConcepto">Código concepto</param>
    ''' <param name="pdecImporte">Importe</param>
    ''' <param name="pstrCodMoneda">Código moneda</param>
    ''' <returns>Objeto Datatable(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 29/11/2012
    ''' </remarks>
    Public Function CalculoComision(ByVal pstrCodigoConcepto As String, _
                                   ByVal pdecImporte As Decimal, _
                                   ByVal pstrCodMoneda As String) As String Implements ICobroNTx.CalculoComision
        Dim odtbListadoBien As DataTable
        Dim prmParameter(2) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos para la consulta.
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodigoConcepto", DbType.String, 3, pstrCodigoConcepto, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pid_Importe", DbType.Decimal)
            prmParameter(1).Precision = 20
            prmParameter(1).Scale = 5
            prmParameter(1).Value = pdecImporte
            prmParameter(2) = New DAABRequest.Parameter("@piv_CodMoneda", DbType.String, 3, pstrCodMoneda, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CalculoComision_Sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBien)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "CalculoComision", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Realiza validación de la fecha ingresado no sea feriado 
    ''' </summary>
    ''' <param name="pstrFecha">Fecha</param>
    ''' <returns>Objeto Datatable(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 29/11/2012
    ''' </remarks>
    Public Function ValidarFeriado(ByVal pstrFecha As String) As String Implements ICobroNTx.ValidarFeriado
        Dim odtbListadoBien As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter
        Dim strResultado As String = String.Empty
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos para la consulta.
            Dim dtFecha As DateTime = CFunciones.CheckDate(pstrFecha)
            prmParameter(0) = New DAABRequest.Parameter("@pid_Fecha", DbType.DateTime, 10, pstrFecha, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ValidarFeriado_Sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            If odtbListadoBien IsNot Nothing Then
                strResultado = odtbListadoBien.Rows(0).Item("Mensaje").ToString()
            End If

            Return strResultado
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ValidarFeriado", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Devuelve instancia para el registro
    ''' </summary>
    ''' <param name="pstrUsuario">Registro de usuario</param>
    ''' <returns>String</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/12/2012
    ''' </remarks>
    Public Function InstanciaRegistro(ByVal pstrUsuario As String) As String Implements ICobroNTx.InstanciaRegistro
        Dim odtbListadoBien As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter
        Dim strResultado As String = String.Empty
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos para la consulta.

            prmParameter(0) = New DAABRequest.Parameter("@piv_CodUsuario", DbType.String, 20, pstrUsuario, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ObtenerMaximoCobro_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoBien = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            If odtbListadoBien IsNot Nothing Then
                strResultado = odtbListadoBien.Rows(0).Item("IndicadorRegistro").ToString()
            End If

            Return strResultado
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InstanciaRegistro", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Devuelve el conjunto de datos de fraccionamiento    
    ''' </summary>
    ''' <param name="pstrEFraccionarCobro">Entidad serializada del objeto GCC_FraccionarCobro</param>
    ''' <returns>Listado de Fraccionamiento(Serializado)</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 13/12/2012
    ''' </remarks>
    Public Function ListadoFraccionamiento(ByVal pstrEFraccionarCobro As String) As String Implements ICobroNTx.ListadoFraccionamiento
        Dim odtbListado As DataTable
        Dim prmParameter(6) As DAABRequest.Parameter
        Dim objEFraccionarCobro As New EGCC_FraccionarCobro

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        objEFraccionarCobro = CFunciones.DeserializeObject(Of EGCC_FraccionarCobro)(pstrEFraccionarCobro)

        Try

            prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, objEFraccionarCobro.CodOperacionActiva, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_TipoRubroFinancionamiento", DbType.String, 3, objEFraccionarCobro.TipoRubroFinanciamiento, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_CodIfi", DbType.String, 4, objEFraccionarCobro.CodIfi, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_TipoRecuperacion", DbType.String, 1, objEFraccionarCobro.TipoRecuperacion, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pis_NumSecRecuperacion", DbType.Int16, 0, objEFraccionarCobro.NumSecRecuperacion, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_CodComisionTipo", DbType.String, 3, objEFraccionarCobro.CodComisionTipo, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pis_NumSecRecupComi", DbType.Int16, 0, objEFraccionarCobro.NumSecRecupComi, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_FraccionarCobro_Sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoFraccionamiento", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function
#End Region


End Class

#End Region
