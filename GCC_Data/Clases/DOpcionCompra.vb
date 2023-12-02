Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DOpcionCompraTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 07/01/2013 06:41:54 p.m.
''' </remarks>
<Guid("F272AD9A-E3E1-475B-90ED-EB262F0DC2D3") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DOpcionCompraTx")> _
Public Class DOpcionCompraTx
    Inherits ServicedComponent
    Implements IOpcionCompraTx

#Region "   Constantes  "
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DOpcionCompraTx"
#End Region

#Region "   Metodos     "

    ''' <summary>
    ''' Inserta un nuevo registro en la tabla GCC_OpcionCompra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m. 
    ''' </remarks>	
    Public Function fblnInsertarOpcionCompra(ByVal pEGCC_OpcionCompra As String) As Boolean Implements IOpcionCompraTx.fblnInsertarOpcionCompra
        Dim oEOpcionCompra As New EGCC_OpcionCompra
        Dim prmParameter(8) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEOpcionCompra = CFunciones.DeserializeObject(Of EGCC_OpcionCompra)(pEGCC_OpcionCompra)

        'Campos GCC_OpcionCompra 
        prmParameter(0) = New DAABRequest.Parameter("@pil_CodOpcionCompra", DbType.Int64, 0, oEOpcionCompra.CodOpcionCompra, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEOpcionCompra.CodSolicitudCredito, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pin_PorcentajeComision", DbType.Decimal)
        prmParameter(2).Precision = 18
        prmParameter(2).Scale = 6
        prmParameter(2).Value = oEOpcionCompra.PorcentajeComision
        prmParameter(3) = New DAABRequest.Parameter("@pin_PorcentajeGastoTransferencia", DbType.Decimal)
        prmParameter(3).Precision = 18
        prmParameter(3).Scale = 6
        prmParameter(3).Value = oEOpcionCompra.PorcentajeGastoTransferencia
        prmParameter(4) = New DAABRequest.Parameter("@piv_CodEstado", DbType.String, 3, oEOpcionCompra.CodEstado, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pid_AudFechaRegistro", DbType.DateTime, 0, oEOpcionCompra.AudFechaRegistro, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pid_AudFechaModificacion", DbType.DateTime, 0, oEOpcionCompra.AudFechaModificacion, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_AudUsuarioRegistro", DbType.String, 12, oEOpcionCompra.AudUsuarioRegistro, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_AudUsuarioModificacion", DbType.String, 12, oEOpcionCompra.AudUsuarioModificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()
        Dim bolRetorno As Boolean = False

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "usp_ins_OpcionCompra"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            bolRetorno = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnInsertarOpcionCompra", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try
        Return bolRetorno
    End Function

    ''' <summary>
    ''' Inserta un nuevo registro en la tabla GCC_OpcionCompraEnvio
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 10/01/2013 06:41:54 p.m. 
    ''' </remarks>	
    Public Function fblnInsertarOpcionCompraEnvio(ByVal pEGCC_OpcionCompra As String) As Boolean Implements IOpcionCompraTx.fblnInsertarOpcionCompraEnvio
        Dim oEOpcionCompra As New EGCC_OpcionCompra
        Dim prmParameter(3) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEOpcionCompra = CFunciones.DeserializeObject(Of EGCC_OpcionCompra)(pEGCC_OpcionCompra)

        'Campos GCC_OpcionCompra 
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodTipoEnvio", DbType.String, 3, oEOpcionCompra.TipoEnvio, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_OrdenEnvio", DbType.String, 1, oEOpcionCompra.OrdenEnvio, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_XMLEntity", DbType.Xml)
        prmParameter(2).Value = oEOpcionCompra.XMLEntity
        prmParameter(2).Direction = ParameterDirection.Input
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodUsuario", DbType.String, 12, oEOpcionCompra.AudUsuarioRegistro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()
        Dim bolRetorno As Boolean = False

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_OpcionCompraEnvio_Ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            bolRetorno = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnInsertarOpcionCompraEnvio", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try
        Return bolRetorno
    End Function

    ''' <summary>
    ''' Actualiza los registros de la tabla SolicitudCreditoEstructuraCarax
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 16/01/2013 06:41:54 p.m. 
    ''' </remarks>	
    Public Function fblnAprobacionBien(ByVal pEGCC_OpcionCompra As String) As Boolean Implements IOpcionCompraTx.fblnAprobacionBien
        Dim oEOpcionCompra As New EGCC_OpcionCompra
        Dim prmParameter(2) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEOpcionCompra = CFunciones.DeserializeObject(Of EGCC_OpcionCompra)(pEGCC_OpcionCompra)

        'Campos GCC_OpcionCompra 
        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEOpcionCompra.NumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_XMLEntity", DbType.Xml)
        prmParameter(1).Value = oEOpcionCompra.XMLEntity
        prmParameter(1).Direction = ParameterDirection.Input
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodUsuario", DbType.String, 12, oEOpcionCompra.AudUsuarioRegistro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()
        Dim bolRetorno As Boolean = False

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_BienOpcionCompraAprobacion_Upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            bolRetorno = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnAprobacionBien", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try
        Return bolRetorno
    End Function

    ''' <summary>
    ''' Modifica un registro existente de la tabla GCC_OpcionCompra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m.
    ''' </remarks>
    Public Function fblnModificarOpcionCompra(ByVal pEGCC_OpcionCompra As String) As Boolean Implements IOpcionCompraTx.fblnModificarOpcionCompra
        Dim oEOpcionCompra As New EGCC_OpcionCompra

        Dim prmParameter(8) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEOpcionCompra = CFunciones.DeserializeObject(Of EGCC_OpcionCompra)(pEGCC_OpcionCompra)

        'Campos GCC_OpcionCompra		

        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEOpcionCompra.NumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pil_CodOpcionCompra", DbType.Int64, 0, oEOpcionCompra.CodOpcionCompra, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pin_PorcentajeComision", DbType.Decimal)
        prmParameter(2).Precision = 18
        prmParameter(2).Scale = 6
        prmParameter(2).Value = oEOpcionCompra.PorcentajeComision
        prmParameter(3) = New DAABRequest.Parameter("@pin_PorcentajeGastoTransferencia", DbType.Decimal)
        prmParameter(3).Precision = 18
        prmParameter(3).Scale = 6
        prmParameter(3).Value = oEOpcionCompra.PorcentajeGastoTransferencia
        prmParameter(4) = New DAABRequest.Parameter("@piv_CodUsuario", DbType.String, 12, oEOpcionCompra.AudUsuarioRegistro, ParameterDirection.Input)

        prmParameter(5) = New DAABRequest.Parameter("@pin_MontoComision", DbType.Decimal)
        prmParameter(5).Precision = 18
        prmParameter(5).Scale = 6
        prmParameter(5).Value = oEOpcionCompra.MontoComision

        prmParameter(6) = New DAABRequest.Parameter("@pin_MontoGastoTransferencia", DbType.Decimal)
        prmParameter(6).Precision = 18
        prmParameter(6).Scale = 6
        prmParameter(6).Value = oEOpcionCompra.MontoGastoTransferencia

        prmParameter(7) = New DAABRequest.Parameter("@pil_FlgComision", DbType.Int16, 0, oEOpcionCompra.FlgComision, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pil_FlgGasto", DbType.Int16, 0, oEOpcionCompra.FlgGasto, ParameterDirection.Input)



        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim bolRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_OpcionCompra_Upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            bolRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarOpcionCompra", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return bolRetorno
    End Function

    ''' <summary>
    ''' Modifica un registro existente de la tabla SolicitudCreditoEstructuraCarac
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 09/01/2013 06:41:54 p.m.
    ''' </remarks>
    Public Function fblnModificarBienOpcionCompra(ByVal pEGCC_OpcionCompra As String) As Boolean Implements IOpcionCompraTx.fblnModificarBienOpcionCompra
        Dim oEOpcionCompra As New EGCC_OpcionCompra

        Dim prmParameter(10) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEOpcionCompra = CFunciones.DeserializeObject(Of EGCC_OpcionCompra)(pEGCC_OpcionCompra)

        'Campos GCC_OpcionCompra		

        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEOpcionCompra.NumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pil_CodOpcionCompra", DbType.Int64, 0, oEOpcionCompra.CodOpcionCompra, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pis_SecFinanciamiento", DbType.Int16, 0, oEOpcionCompra.SecFinanciamiento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_FlagAceptacion", DbType.String, 1, oEOpcionCompra.FlagAceptacionCliente, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pid_FechaAceptacion", DbType.DateTime, 10, oEOpcionCompra.FechaAceptacionCliente, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pid_FechaTransferencia", DbType.DateTime, 10, oEOpcionCompra.FechaTransferencia, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pid_FechaTransferenciaRRPP", DbType.DateTime, 10, oEOpcionCompra.FechaTransferenciaRRPP, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pid_FechaPagoOC", DbType.DateTime, 10, oEOpcionCompra.FechaPagoOC, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_CodTipoPagoOC", DbType.String, 3, oEOpcionCompra.CodTipoPagoOC, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pii_TotalBienes", DbType.Int32, 0, oEOpcionCompra.Item, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_CodUsuario", DbType.String, 12, oEOpcionCompra.AudUsuarioRegistro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim bolRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_BienOpcionCompra_Upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            bolRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarBienOpcionCompra", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return bolRetorno
    End Function


    ''' <summary>
    ''' Eliminar un registro existente de la tabla GCC_OpcionCompra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m.
    ''' </remarks>
    Public Function fblnEliminarOpcionCompra(ByVal pEGCC_OpcionCompra As String) As Boolean Implements IOpcionCompraTx.fblnEliminarOpcionCompra
        Dim oEOpcionCompra As New EGCC_OpcionCompra


        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEOpcionCompra = CFunciones.DeserializeObject(Of EGCC_OpcionCompra)(pEGCC_OpcionCompra)

        'Campos GCC_OpcionCompra			

        prmParameter(0) = New DAABRequest.Parameter("@pil_CodOpcionCompra", DbType.Int64, 0, oEOpcionCompra.CodOpcionCompra, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim bolRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "usp_del_OpcionCompra"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            bolRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnEliminarGCC_OpcionCompra", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return bolRetorno
    End Function

    ''' <summary>
    ''' Inserta un registro en la tabla GCC_OpcionCompraDocumento
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se registro correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 29/01/2013 06:41:54 p.m.
    ''' </remarks>
    Public Function fblnInsertarOpcionCompraDocumento(ByVal pEGCC_OpcionCompra As String) As Boolean Implements IOpcionCompraTx.fblnInsertarOpcionCompraDocumento
        Dim oEOpcionCompra As New EGCC_OpcionCompra

        Dim prmParameter(7) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEOpcionCompra = CFunciones.DeserializeObject(Of EGCC_OpcionCompra)(pEGCC_OpcionCompra)

        'Campos GCC_OpcionCompra			

        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEOpcionCompra.NumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pis_CodOpcionCompraDocumento", DbType.Int16, 0, oEOpcionCompra.CodOperacionDocumento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodCheckList", DbType.String, 3, oEOpcionCompra.CodCheckList, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_FlagCheckList", DbType.String, 1, oEOpcionCompra.FlagCheckList, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pid_FechaCheckList", DbType.DateTime, 0, oEOpcionCompra.FechaCheckList, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Adjunto", DbType.String, 255, oEOpcionCompra.Adjunto, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Usuario", DbType.String, 12, oEOpcionCompra.AudUsuarioRegistro, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pis_Flag", DbType.Int32, 0, oEOpcionCompra.Item, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim bolRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_OpcionCompraDocumento_Ins"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            bolRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnInsertarOpcionCompraDocumento", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return bolRetorno
    End Function

    ''' <summary>
    ''' Inserta un registro en la tabla GCC_OpcionCompraDocumentoObservacion
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>True si se registro correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 29/01/2013 06:41:54 p.m.
    ''' </remarks>
    Public Function fblnInsertarOpcionCompraDocumentoObservacion(ByVal pEGCC_OpcionCompra As String) As Boolean Implements IOpcionCompraTx.fblnInsertarOpcionCompraDocumentoObservacion
        Dim oEOpcionCompra As New EGCC_OpcionCompra

        Dim prmParameter(4) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEOpcionCompra = CFunciones.DeserializeObject(Of EGCC_OpcionCompra)(pEGCC_OpcionCompra)

        'Campos GCC_OpcionCompra			

        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEOpcionCompra.NumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pis_CodOpcionCompraDocumento", DbType.Int16, 0, oEOpcionCompra.CodOperacionDocumento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodCheckList", DbType.String, 3, oEOpcionCompra.CodCheckList, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Observacion", DbType.String, 300, oEOpcionCompra.Observacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Usuario", DbType.String, 12, oEOpcionCompra.AudUsuarioRegistro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim bolRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_OpcionCompraDocumentoObservacion_Ins"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            bolRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnInsertarOpcionCompraDocumentoObservacion", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return bolRetorno
    End Function

#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DOpcionCompraNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 07/01/2013 06:41:54 p.m.
''' </remarks>
<Guid("3A89FC8C-A4C5-4C9F-88EF-3C32B3DBEC54") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DOpcionCompraNTx")> _
Public Class DOpcionCompraNTx
    Inherits ServicedComponent
    Implements IOpcionCompraNTx

#Region "   Constantes  "
    Private Const C_NOMBRE_CLASE As String = "DOpcionCompraNTx"
#End Region

#Region "   Metodos     "

    ''' <summary>
    ''' Obtiene los valores de un registro de la tabla GCC_OpcionCompra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjLeerOpcionCompra(ByVal pEGCC_OpcionCompra As String) As String Implements IOpcionCompraNTx.fobjLeerOpcionCompra
        Dim oEOpcionCompra As EGCC_OpcionCompra = Nothing
        Dim odtbListadoOpcionCompra As DataTable = Nothing
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEOpcionCompra = CFunciones.DeserializeObject(Of EGCC_OpcionCompra)(pEGCC_OpcionCompra)

        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEOpcionCompra.NumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pil_CodOpcionCompra", DbType.Int64, 0, oEOpcionCompra.CodOpcionCompra, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_OpcionCompra_Get"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoOpcionCompra = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjLeerOpcionCompra", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoOpcionCompra)
    End Function

    ''' <summary>
    ''' Obtiene los valores de un registro de la vista UV_GCC_BIENOPCIOMCOMPRA_SEL
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un registro</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 08/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjLeerBienOpcionCompra(ByVal pEGCC_OpcionCompra As String) As String Implements IOpcionCompraNTx.fobjLeerBienOpcionCompra
        Dim oEOpcionCompra As EGCC_OpcionCompra = Nothing
        Dim odtbBienOpcionCompra As DataTable = Nothing
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEOpcionCompra = CFunciones.DeserializeObject(Of EGCC_OpcionCompra)(pEGCC_OpcionCompra)

        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEOpcionCompra.NumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pis_SecFinanciamiento", DbType.Int16, 0, oEOpcionCompra.SecFinanciamiento, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_BienOpcionCompra_Get"
            objRequest.Parameters.AddRange(prmParameter)

            odtbBienOpcionCompra = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjLeerBienOpcionCompra", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbBienOpcionCompra)
    End Function


    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de página</param>
    ''' <param name="pCurrentPage">Página actual</param>
    ''' <param name="pSortColumn">Ordenar por columna</param>
    ''' <param name="pSortOrder">Dirección de ordenamiento</param>    
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 07/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoOpcionCompra(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pEGCC_OpcionCompra As String) As String Implements IOpcionCompraNTx.fobjListadoOpcionCompra
        Dim oEOpcionCompra As EGCC_OpcionCompra = Nothing
        Dim prmParameter(13) As DAABRequest.Parameter
        Dim odtbListadoOpcionCompra As DataTable = Nothing

        oEOpcionCompra = CFunciones.DeserializeObject(Of EGCC_OpcionCompra)(pEGCC_OpcionCompra)

        'Campos para la consulta.
        prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 0, pPageSize, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 0, pCurrentPage, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

        prmParameter(4) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEOpcionCompra.NumeroContrato, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_CodigoUnico", DbType.String, 10, oEOpcionCompra.CodigoUnico, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_RazonSocial", DbType.String, 150, oEOpcionCompra.RazonSocial, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_ClasificacionBien", DbType.String, 3, oEOpcionCompra.ClasificacionBien, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_TipoBien", DbType.String, 3, oEOpcionCompra.TipoBien, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_TipoEnvio", DbType.String, 3, oEOpcionCompra.TipoEnvio, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_Demanda", DbType.String, 3, oEOpcionCompra.Demanda, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_PlacaActual", DbType.String, 10, oEOpcionCompra.PlacaActual, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_NroSerie", DbType.String, 20, oEOpcionCompra.NroSerie, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@pid_FechaFiltro", DbType.DateTime, 10, oEOpcionCompra.FechaFiltro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_OpcionCompra_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoOpcionCompra = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjListadoOpcionCompra", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoOpcionCompra)
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 11/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoOpcionCompraTodo(ByVal pEGCC_OpcionCompra As String) As String Implements IOpcionCompraNTx.fobjListadoOpcionCompraTodo
        Dim oEOpcionCompra As EGCC_OpcionCompra = Nothing
        Dim prmParameter(9) As DAABRequest.Parameter
        Dim odtbListadoOpcionCompra As DataTable = Nothing

        oEOpcionCompra = CFunciones.DeserializeObject(Of EGCC_OpcionCompra)(pEGCC_OpcionCompra)

        'Campos para la consulta.
        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEOpcionCompra.NumeroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodigoUnico", DbType.String, 10, oEOpcionCompra.CodigoUnico, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_RazonSocial", DbType.String, 150, oEOpcionCompra.RazonSocial, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_ClasificacionBien", DbType.String, 3, oEOpcionCompra.ClasificacionBien, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_TipoBien", DbType.String, 3, oEOpcionCompra.TipoBien, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_TipoEnvio", DbType.String, 3, oEOpcionCompra.TipoEnvio, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Demanda", DbType.String, 3, oEOpcionCompra.Demanda, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_PlacaActual", DbType.String, 10, oEOpcionCompra.PlacaActual, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_NroSerie", DbType.String, 20, oEOpcionCompra.NroSerie, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pid_FechaFiltro", DbType.DateTime, 10, oEOpcionCompra.FechaFiltro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_OpcionCompraTodo_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoOpcionCompra = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjListadoOpcionCompraTodo", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoOpcionCompra)
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 11/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoOpcionCompraEnvio(ByVal pEGCC_OpcionCompra As String) As String Implements IOpcionCompraNTx.fobjListadoOpcionCompraEnvio
        Dim oEOpcionCompra As EGCC_OpcionCompra = Nothing
        Dim prmParameter(0) As DAABRequest.Parameter
        Dim odtbListadoOpcionCompra As DataTable = Nothing

        oEOpcionCompra = CFunciones.DeserializeObject(Of EGCC_OpcionCompra)(pEGCC_OpcionCompra)

        'Campos para la consulta.
        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEOpcionCompra.NumeroContrato, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_OpcionCompraEnvio_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoOpcionCompra = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjListadoOpcionCompraEnvio", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoOpcionCompra)
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de página</param>
    ''' <param name="pCurrentPage">Página actual</param>
    ''' <param name="pSortColumn">Ordenar por columna</param>
    ''' <param name="pSortOrder">Dirección de ordenamiento</param>    
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 08/01/2013 06:41:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoBienOpcionCompra(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEGCC_OpcionCompra As String) As String Implements IOpcionCompraNTx.fobjListadoBienOpcionCompra
        Dim oEOpcionCompra As EGCC_OpcionCompra = Nothing
        Dim prmParameter(4) As DAABRequest.Parameter
        Dim odtbListadoBienOpcionCompra As DataTable = Nothing

        oEOpcionCompra = CFunciones.DeserializeObject(Of EGCC_OpcionCompra)(pEGCC_OpcionCompra)

        'Campos para la consulta.
        prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 0, pPageSize, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 0, pCurrentPage, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

        prmParameter(4) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEOpcionCompra.NumeroContrato, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_BienOpcionCompra_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoBienOpcionCompra = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjListadoBienOpcionCompra", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoBienOpcionCompra)
    End Function

    ''' <summary>
    ''' Lista todos los bienes por contrato
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <param name="pFechaTermino">Fecha de término de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 16/01/2013 03:04:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoBienOpcionCompraReporte(ByVal pFechaInicio As String, _
                                                        ByVal pFechaTermino As String) As String Implements IOpcionCompraNTx.fobjListadoBienOpcionCompraReporte
        Dim prmParameter(1) As DAABRequest.Parameter
        Dim odtbListado As DataTable = Nothing

        'Campos para la consulta.
        prmParameter(0) = New DAABRequest.Parameter("@piv_FechaInicio", DbType.String, 10, pFechaInicio, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_FechaTermino", DbType.String, 10, pFechaTermino, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_BienOpcionCompraReporte_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjListadoBienOpcionCompraReporte", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    ''' <summary>
    ''' Listado de Opción de Compra Reporte
    ''' </summary>
    ''' <param name="pdFechaActivacionIni">Fecha de Activación Inicial</param>
    ''' <param name="pdFechaActivacionFin">Fecha de Activación Final</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 28/01/2013
    ''' </remarks>
    Public Function ListadoReporteOpcionCompra(ByVal pdFechaActivacionIni As DateTime, _
                                               ByVal pdFechaActivacionFin As DateTime) As String Implements IOpcionCompraNTx.ListadoReporteOpcionCompra
        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pid_FechaActivacionInicial", DbType.Date, 0, pdFechaActivacionIni, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pid_FechaActivacionFinal", DbType.Date, 0, pdFechaActivacionFin, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_OpcionCompra_rpt"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoReporteOpcionCompra", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function


    ''' <summary>
    ''' Listado de Documento de Opción de Compra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>  
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 29/01/2013
    ''' </remarks>
    Public Function ListadoOpcionCompraDocumento(ByVal pEGCC_OpcionCompra As String) As String Implements IOpcionCompraNTx.ListadoOpcionCompraDocumento
        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter
        Dim oEOpcionCompra As EGCC_OpcionCompra = Nothing

        oEOpcionCompra = CFunciones.DeserializeObject(Of EGCC_OpcionCompra)(pEGCC_OpcionCompra)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEOpcionCompra.NumeroContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_OpcionCompraDocumento_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoOpcionCompraDocumento", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function

    ''' <summary>
    ''' Listado de Observaciones de Documento de Opción de Compra
    ''' </summary>
    ''' <param name="pEGCC_OpcionCompra">Entidad OpcionCompra serializada</param>  
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 29/01/2013
    ''' </remarks>
    Public Function ListadoOpcionCompraDocumentoObservacion(ByVal pEGCC_OpcionCompra As String) As String Implements IOpcionCompraNTx.ListadoOpcionCompraDocumentoObservacion
        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter
        Dim oEOpcionCompra As EGCC_OpcionCompra = Nothing

        oEOpcionCompra = CFunciones.DeserializeObject(Of EGCC_OpcionCompra)(pEGCC_OpcionCompra)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEOpcionCompra.NumeroContrato, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pis_CodOpcionCompraDocumento", DbType.Int16, 0, oEOpcionCompra.CodOperacionDocumento, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_OpcionCompraDocumentoObservacion_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoOpcionCompraDocumentoObservacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function
#End Region

End Class
#End Region
