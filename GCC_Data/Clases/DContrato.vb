
Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports TSF.DAAB

Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DContratoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("14BBC56B-0307-4d66-90B5-1D167217287B") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DContratoTx")> _
Public Class DContratoTx
    Inherits ServicedComponent
    Implements IContratoTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DContratoTx"
#End Region

#Region "Métodos"

    ''' <summary>
    ''' Modifiar el Contrato(SolicitudCredito) para el Checklist
    ''' </summary>
    ''' <param name="pEGcc_checklisComercialBien">Entidad Contrato serializada</param>
    ''' <returns>String con el número de Contrato</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoChecklistUpd(ByVal pEGcc_checklisComercialBien As String) As Boolean Implements IContratoTx.ContratoChecklistUpd
        Dim blnResultado As Boolean
        Dim oEContratoCheckList As New ESolicitudcredito
        Dim prmParameter(5) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEContratoCheckList = CFunciones.DeserializeObject(Of ESolicitudcredito)(pEGcc_checklisComercialBien)

        'Campos
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codsolicitudcredito", DbType.String, 8, oEContratoCheckList.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Uso", DbType.String, 250, oEContratoCheckList.Uso, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Ubicacion", DbType.String, 200, oEContratoCheckList.Ubicacion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_codigolugarfirmacontrato", DbType.String, 8, oEContratoCheckList.codigolugarfirmacontrato, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_codigoubigeolugarfirma", DbType.String, 8, oEContratoCheckList.codigoubigeolugarfirma, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pvi_ubigeo", DbType.String, 6, oEContratoCheckList.Ubigeo, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CheckListContrato_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnResultado = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoChecklistUpd", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            blnResultado = False
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnResultado

    End Function

    ''' <summary>
    ''' Modifiar el Contrato(SolicitudCredito) para el Documentos Clientes
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad de Contrato serializada en string</param>
    ''' <returns>Resultado Booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 15/05/2012
    ''' </remarks>
    Public Function ContratoDocClienteUpd(ByVal pESolicitudCredito As String) As Boolean Implements IContratoTx.ContratoDocClienteUpd
        Dim blnResultado As Boolean
        Dim oEContrato As New ESolicitudcredito
        Dim prmParameter(2) As DAABRequest.Parameter

        'Deserializa la Entidad
        oEContrato = CFunciones.DeserializeObject(Of ESolicitudcredito)(pESolicitudCredito)

        'Campos
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codsolicitudcredito", DbType.String, 8, oEContrato.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_FlagTerminoRecepDocumentoClie", DbType.Int32, 4, oEContrato.FlagTerminoRecepDocumentoClie, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pid_FechaTerminoRecepDocumentoClie", DbType.DateTime, oEContrato.FechaTerminoRecepDocumentoClie, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoDocClie_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnResultado = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoDocClienteUpd", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            blnResultado = False
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnResultado

    End Function

#Region "SolicitudCredito"

    ''' <summary>
    ''' Establece si algún elemento del anexo a sido modificado o no.
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function fblnModificado(ByVal pESolicitudCredito As String) As Boolean Implements IContratoTx.fblnModificado
        Dim oESolicitudCredito As ESolicitudcredito

        Dim prmParameter(2) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oESolicitudCredito = CFunciones.DeserializeObject(Of ESolicitudcredito)(pESolicitudCredito)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_CodSolicitudCredito", DbType.String, 8, oESolicitudCredito.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pib_FlagModificado", DbType.Boolean, 1, oESolicitudCredito.Modificado, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oESolicitudCredito.Codusuario, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_Contrato_Modificado_upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificado", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Actualiza los datos del contrato durante la etapa de formalización.
    ''' </summary>
    ''' <param name="pESolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function fblnModificarContrato(ByVal pESolicitudCredito As String) As Boolean Implements IContratoTx.fblnModificarContrato

        Dim oESolicitudCredito As ESolicitudcredito

        Dim prmParameter(12) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oESolicitudCredito = CFunciones.DeserializeObject(Of ESolicitudcredito)(pESolicitudCredito)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_CodSolicitudCredito", DbType.String, 8, oESolicitudCredito.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_CodigoClasificacionContrato", DbType.String, 100, oESolicitudCredito.CodigoClasificacionContrato, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_CodigoEstadoContrato", DbType.String, 3, oESolicitudCredito.Codigoestadocontrato, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_FechaContrato", DbType.String, 8, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCredito.FechaContrato), ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_FechaRegistroPublico", DbType.String, 8, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCredito.FechaRegistroPublico), ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_FechaFirmaNotaria", DbType.String, 8, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oESolicitudCredito.Fechafirmanotaria), ParameterDirection.Input)

        prmParameter(6) = New DAABRequest.Parameter("@pic_CodigoEstadoCivil", DbType.String, 100, oESolicitudCredito.CodigoEstadoCivil, ParameterDirection.Input)

        prmParameter(7) = New DAABRequest.Parameter("@piv_NombreConyuge", DbType.String, 100, oESolicitudCredito.Nombreconyuge, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pic_CodigoTipoDocConyuge", DbType.String, 100, oESolicitudCredito.Codigotipodocconyuge, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_NumeroDocumentoConyuge", DbType.String, 11, oESolicitudCredito.Numerodocumentoconyuge, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, oESolicitudCredito.Codusuario, ParameterDirection.Input)

        prmParameter(11) = New DAABRequest.Parameter("@piv_ClienteRazonSocial", DbType.String, 100, oESolicitudCredito.ClienteRazonSocial, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_ClienteDomicilioLegal", DbType.String, 120, oESolicitudCredito.ClienteDomicilioLegal, ParameterDirection.Input)



        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_Contrato_upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarContrato", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' GestionComercialEnviarUpd
    ''' </summary>
    ''' <param name="pEGcc_Contrato"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GestionComercialEnviarUpd(ByVal pEGcc_Contrato As String) As Boolean Implements IContratoTx.GestionComercialEnviarUpd

        Dim oESolicitudCredito As New ESolicitudcredito

        Dim prmParameter(6) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oESolicitudCredito = CFunciones.DeserializeObject(Of ESolicitudcredito)(pEGcc_Contrato)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@CodSolicitudCredito", DbType.String, 8, oESolicitudCredito.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@FlagEnvioLegal", DbType.Int32, 1, oESolicitudCredito.FlagEnvioLegal, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@CodigoEstadoContrato", DbType.String, 2, oESolicitudCredito.Codigoestadocontrato, ParameterDirection.Input)
        ' GCCTS_JRC_20120220-Se necesita el Codigo de Usuari de Registro para el Seguimiento de Contrato 
        prmParameter(3) = New DAABRequest.Parameter("@UsuarioRegistro", DbType.String, 250, oESolicitudCredito.AudUsuarioModificacion, ParameterDirection.Input)

        'GCCTS_JRC_20120304-Seguimiento Datos acionales
        prmParameter(4) = New DAABRequest.Parameter("@piv_CodigoUsuario", DbType.String, 10, oESolicitudCredito.CodigoUsuario, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_NombreUsuario", DbType.String, 200, oESolicitudCredito.NombreUsuario, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_PerfilUsuario", DbType.String, 100, oESolicitudCredito.PerfilUsuario, ParameterDirection.Input)
        '---


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_GestionComercial_enviar_upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GestionComercialEnviarUpd", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Actualiza el estado del contrato
    ''' </summary>
    ''' <param name="pSolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ActualizaEstado(ByVal pSolicitudCredito As String) As Boolean Implements IContratoTx.ActualizaEstado

        Dim oESolicitudCredito As ESolicitudcredito

        Dim prmParameter(8) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oESolicitudCredito = CFunciones.DeserializeObject(Of ESolicitudcredito)(pSolicitudCredito)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_CodSolicitudCredito", DbType.String, 8, oESolicitudCredito.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_CodigoEstadoContrato", DbType.String, 100, oESolicitudCredito.Codigoestadocontrato, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_AudUsuarioModificacion", DbType.String, 12, oESolicitudCredito.Codusuario, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_FechaFirmaNotaria", DbType.String, 10, oESolicitudCredito.SFechafirmanotaria, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_ClienteRazonSocial", DbType.String, 100, oESolicitudCredito.ClienteRazonSocial, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_ClienteDomicilioLegal", DbType.String, 120, oESolicitudCredito.ClienteDomicilioLegal, ParameterDirection.Input)

        'GCCTS_JRC_20120304-Seguimiento Datos acionales
        prmParameter(6) = New DAABRequest.Parameter("@piv_CodigoUsuario", DbType.String, 10, oESolicitudCredito.CodigoUsuario, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_NombreUsuario", DbType.String, 200, oESolicitudCredito.NombreUsuario, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_PerfilUsuario", DbType.String, 100, oESolicitudCredito.PerfilUsuario, ParameterDirection.Input)
        '---

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_Contrato_Estadoupd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ActualizaEstado", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Actualiza el nombre del documento de separación del conyugue en la tabla Contrato (SolicitudCredito)
    ''' </summary>
    ''' <param name="pSolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ActualizaDocumentoSeparacion(ByVal pSolicitudCredito As String) As Boolean Implements IContratoTx.ActualizaDocumentoSeparacion

        Dim oESolicitudCredito As New ESolicitudcredito

        Dim prmParameter(2) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oESolicitudCredito = CFunciones.DeserializeObject(Of ESolicitudcredito)(pSolicitudCredito)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_CodSolicitudCredito", DbType.String, 8, oESolicitudCredito.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_DocumentoSeparacion", DbType.String, 255, oESolicitudCredito.Documentoseparacion, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_AudUsuarioModificacion", DbType.String, 12, oESolicitudCredito.Codusuario, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_Contrato_DocumentoSeparacionupd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ActualizaDocumentoSeparacion", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Actualiza el nombre del documento de contrato en la tabla Contrato (SolicitudCredito)
    ''' </summary>
    ''' <param name="pSolicitudCredito">Entidad ESolicitudCredito serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ActualizaArchivoContratoAdjunto(ByVal pSolicitudCredito As String) As Boolean Implements IContratoTx.ActualizaArchivoContratoAdjunto

        Dim oESolicitudCredito As New ESolicitudcredito

        Dim prmParameter(2) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oESolicitudCredito = CFunciones.DeserializeObject(Of ESolicitudcredito)(pSolicitudCredito)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@pic_CodSolicitudCredito", DbType.String, 8, oESolicitudCredito.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_ArchivoContratoAdjunto", DbType.String, 255, oESolicitudCredito.ArchivoContratoAdjunto, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_AudUsuarioModificacion", DbType.String, 12, oESolicitudCredito.Codusuario, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_Contrato_AdjuntarContratoupd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ActualizaDocumentoSeparacion", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    Public Function InsertaSeguimientoContrato(ByVal pEGcc_SeguimientoContrato As String) As Boolean Implements IContratoTx.InsertaSeguimientoContrato
        Dim blnResultado As Boolean
        Dim oESeguimientoContrato As New EGcc_seguimientocontrato
        Dim prmParameter(11) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oESeguimientoContrato = CFunciones.DeserializeObject(Of EGcc_seguimientocontrato)(pEGcc_SeguimientoContrato)

        'Campos
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oESeguimientoContrato.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodigoMotivoRechazo", DbType.String, 250, oESeguimientoContrato.CodigoMotivoRechazo, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodigoEstadoContrato", DbType.String, 200, oESeguimientoContrato.Codigoestadocontrato, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_FechaCambioEstado", DbType.String, 10, oESeguimientoContrato.SFechacambioestado, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_UsuarioRegistro", DbType.String, 8, oESeguimientoContrato.Usuarioregistro, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Observacion", DbType.String, 250, oESeguimientoContrato.Observacion, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_AudUsuarioRegistro", DbType.String, 8, oESeguimientoContrato.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_AudUsuarioModificacion", DbType.String, 8, oESeguimientoContrato.Audusuariomodificacion, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_Adjunto", DbType.String, 250, oESeguimientoContrato.Adjunto, ParameterDirection.Input)

        'GCCTS_JRC_20120304-Seguimiento Datos acionales
        prmParameter(9) = New DAABRequest.Parameter("@piv_CodigoUsuario", DbType.String, 10, oESeguimientoContrato.CodigoUsuario, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_NombreUsuario", DbType.String, 200, oESeguimientoContrato.NombreUsuario, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_PerfilUsuario", DbType.String, 100, oESeguimientoContrato.PerfilUsuario, ParameterDirection.Input)
        '---

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_SeguimientoContrato_Ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnResultado = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InsertaSeguimientoContrato", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            blnResultado = False
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnResultado

    End Function

    'Inicio IBK - AAE - 12/02/2013 - Agrego nueva funcion
    Public Function ContratoChecklistUpd2(ByVal pEGcc_checklisComercialBien As String, ByVal pEgcc_cotizacion As String) As Boolean Implements IContratoTx.ContratoChecklistUpd2
        Dim blnResultado As Boolean
        Dim oEContratoCheckList As New ESolicitudcredito
        Dim oECotizacion As New EGcc_cotizacion
        Dim prmParameter(13) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEContratoCheckList = CFunciones.DeserializeObject(Of ESolicitudcredito)(pEGcc_checklisComercialBien)
        oECotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pEgcc_cotizacion)
        'Campos
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codsolicitudcredito", DbType.String, 8, oEContratoCheckList.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Uso", DbType.String, 250, oEContratoCheckList.Uso, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Ubicacion", DbType.String, 200, oEContratoCheckList.Ubicacion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_codigolugarfirmacontrato", DbType.String, 8, oEContratoCheckList.codigolugarfirmacontrato, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_codigoubigeolugarfirma", DbType.String, 8, oEContratoCheckList.codigoubigeolugarfirma, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pvi_ubigeo", DbType.String, 6, oEContratoCheckList.Ubigeo, ParameterDirection.Input)
        'Agrego nuevos parámetros
        prmParameter(6) = New DAABRequest.Parameter("@piv_razonSocial", DbType.String, 100, oECotizacion.NombreCliente, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_dirCliente", DbType.String, 120, oECotizacion.DireccionCliente, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_Numerolinea", DbType.String, 10, oECotizacion.Numerolinea, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pid_Fechamaxactivacion", DbType.Date, 0, oECotizacion.Fechamaxactivacion, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pid_FechaDisponibilidad", DbType.Date, 0, oECotizacion.FechaOfertaValida, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@pin_Importeopcioncompra", DbType.Decimal)
        prmParameter(11).Precision = 18
        prmParameter(11).Scale = 6
        prmParameter(11).Value = oECotizacion.Importeopcioncompra
        prmParameter(12) = New DAABRequest.Parameter("@pin_Importecomisionactivacion", DbType.Decimal)
        prmParameter(12).Precision = 18
        prmParameter(12).Scale = 6
        prmParameter(12).Value = oECotizacion.Importecomisionactivacion
        prmParameter(13) = New DAABRequest.Parameter("@pin_Importecomisionestructuracion", DbType.Decimal)
        prmParameter(13).Precision = 18
        prmParameter(13).Scale = 6
        prmParameter(13).Value = oECotizacion.Importecomisionestructuracion

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CheckListContrato_upd2"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnResultado = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoChecklistUpd2", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            blnResultado = False
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnResultado

    End Function
    'Fin IBK
#End Region

#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DContratoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("C69AFD90-E2DA-4356-B220-749FDC0BA5F0") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DContratoNTx")> _
Public Class DContratoNTx
    Inherits ServicedComponent
    Implements IContratoNTx


#Region "constantes"

    Private Const C_NOMBRE_CLASE As String = "DContratoNTx"

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : EBL - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoContratos(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pContrato As String, _
                                     ByVal pCuCliente As String, _
                                     ByVal pRazonSocial As String, _
                                     ByVal pCotizacion As String, _
                                     ByVal pFechaIni As String, _
                                     ByVal pFechaFin As String, _
                                     ByVal pEjecutivo As String, _
                                     ByVal pEstado As String, _
                                     ByVal pZonal As String, _
                                     ByVal pClasificacion As String, _
                                     ByVal pClasificacionContrato As String, _
                                     ByVal pCodigoBanca As String, _
                                     ByVal pTipoPersona As String, _
                                     ByVal pNotaria As String, _
                                     ByVal pKardex As String) As String Implements IContratoNTx.ListadoContratos
        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(18) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8000, pContrato, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_CUCliente", DbType.String, 8000, pCuCliente, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pic_NombreRazonSocial", DbType.String, 8000, pRazonSocial, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@pic_CodigoCotizacion", DbType.String, 8000, pCotizacion, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@pic_FechaInicial", DbType.String, 8000, pFechaIni, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@pic_FechaFinal", DbType.String, 8000, pFechaFin, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@pic_EjecutivoLeasing", DbType.String, 100, pEjecutivo, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@pic_Estado", DbType.String, 100, pEstado, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@pic_CodigoZonal", DbType.String, 100, pZonal, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@pic_CodigoClasificacion", DbType.String, 100, pClasificacion, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@pic_CodigoClasifContrato", DbType.String, 100, pClasificacionContrato, ParameterDirection.Input)
            prmParameter(15) = New DAABRequest.Parameter("@pic_CodigoBanca", DbType.String, 100, pCodigoBanca, ParameterDirection.Input)
            prmParameter(16) = New DAABRequest.Parameter("@pic_CodigoTipoPersona", DbType.String, 100, pTipoPersona, ParameterDirection.Input)
            prmParameter(17) = New DAABRequest.Parameter("@pic_CodigoNotaria", DbType.String, 100, pNotaria, ParameterDirection.Input)
            prmParameter(18) = New DAABRequest.Parameter("@pic_NroKardex", DbType.String, 10, pKardex, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Contrato_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoContratos", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : JJM IBK
    ''' Fecha de Creación  : 25/04/2013
    ''' </remarks>
    Public Function ListadoContratos2(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pContrato As String, _
                                     ByVal pCuCliente As String, _
                                     ByVal pRazonSocial As String, _
                                     ByVal pCotizacion As String, _
                                     ByVal pFechaIni As String, _
                                     ByVal pFechaFin As String, _
                                     ByVal pEjecutivo As String, _
                                     ByVal pEstado As String, _
                                     ByVal pZonal As String, _
                                     ByVal pClasificacion As String, _
                                     ByVal pClasificacionContrato As String, _
                                     ByVal pCodigoBanca As String, _
                                     ByVal pTipoPersona As String, _
                                     ByVal pNotaria As String, _
                                     ByVal pKardex As String, _
                                     ByVal pEstadoOperacionActiva As String) As String Implements IContratoNTx.ListadoContratos2
        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(19) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8000, pContrato, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_CUCliente", DbType.String, 8000, pCuCliente, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pic_NombreRazonSocial", DbType.String, 8000, pRazonSocial, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@pic_CodigoCotizacion", DbType.String, 8000, pCotizacion, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@pic_FechaInicial", DbType.String, 8000, pFechaIni, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@pic_FechaFinal", DbType.String, 8000, pFechaFin, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@pic_EjecutivoLeasing", DbType.String, 100, pEjecutivo, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@pic_Estado", DbType.String, 100, pEstado, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@pic_CodigoZonal", DbType.String, 100, pZonal, ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@pic_CodigoClasificacion", DbType.String, 100, pClasificacion, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@pic_CodigoClasifContrato", DbType.String, 100, pClasificacionContrato, ParameterDirection.Input)
            prmParameter(15) = New DAABRequest.Parameter("@pic_CodigoBanca", DbType.String, 100, pCodigoBanca, ParameterDirection.Input)
            prmParameter(16) = New DAABRequest.Parameter("@pic_CodigoTipoPersona", DbType.String, 100, pTipoPersona, ParameterDirection.Input)
            prmParameter(17) = New DAABRequest.Parameter("@pic_CodigoNotaria", DbType.String, 100, pNotaria, ParameterDirection.Input)
            prmParameter(18) = New DAABRequest.Parameter("@pic_NroKardex", DbType.String, 10, pKardex, ParameterDirection.Input)
            prmParameter(19) = New DAABRequest.Parameter("@pic_EstadoOperacionActiva", DbType.String, 10, pEstadoOperacionActiva, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_SolicitudCredito_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoContratos", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function


    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : EBL - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoContratosDesembolso(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pContrato As String, _
                                     ByVal pCuCliente As String, _
                                     ByVal pRazonSocial As String, _
                                     ByVal pEjecutivo As String, _
                                     ByVal pEstado As String, _
                                     ByVal pClasificacion As String, _
                                     ByVal pCodigoSubTipoContrato As String, _
                                     ByVal pCodigoBanca As String, _
                                     ByVal pCodMoneda As String) As String Implements IContratoNTx.ListadoContratosDesembolso
        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(12) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8000, pContrato, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_CUCliente", DbType.String, 8000, pCuCliente, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pic_NombreRazonSocial", DbType.String, 8000, pRazonSocial, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@pic_EjecutivoLeasing", DbType.String, 100, pEjecutivo, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@pic_Estado", DbType.String, 100, pEstado, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@pic_CodigoClasificacion", DbType.String, 100, pClasificacion, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@pic_CodigoSubTipoContrato", DbType.String, 100, pCodigoSubTipoContrato, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@pic_CodigoBanca", DbType.String, 100, pCodigoBanca, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@pic_CodMoneda", DbType.String, 100, pCodMoneda, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoDesembolso_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoContratos", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function

    ''' <summary>
    ''' Devuelve el contrato especificacado identificado por el número del crédito.
    ''' Incluye los datos de la tabla SolicitudCredito y GCC_ContratoOtroConcepto.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function Retornar(ByVal codigoContrato As String) As String Implements IContratoNTx.Retornar
        Dim odtbContrato As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8, codigoContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "Up_gcc_Contrato_ret"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContrato = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbContrato)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Retornar", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Devuelve el contrato especificacado identificado por el número del crédito.
    ''' Incluye los datos de la tabla SolicitudCredito y GCC_ContratoOtroConcepto.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function DatosCliente(ByVal codigoContrato As String) As String Implements IContratoNTx.DatosCliente
        Dim odtbContrato As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8, codigoContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "Up_gcc_ContratoDatosCliente_ret"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContrato = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbContrato)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "DatosCliente", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Listado General de Contrato y Cotizacion 
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la pagina</param>
    ''' <param name="pCurrentPage">Pagina Actual</param>
    ''' <param name="pSortColumn">Columna a Ordenar</param>
    ''' <param name="pSortOrder">Tipo de Ordenamiento</param>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 16/05/2012
    ''' </remarks>
    Public Function ListadoContratoCotizacion(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal pECotizacion As String) As String Implements IContratoNTx.ListadoContratoCotizacion

        'Variables
        Dim odtbListado As New DataTable
        Dim oECotizacion As New EGcc_cotizacion
        Dim prmParameter(14) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oECotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_NroCotizacion", DbType.String, 8, oECotizacion.Codigocotizacion, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_NroContrato", DbType.String, 8, oECotizacion.CodigoContrato, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_CUCliente", DbType.String, 10, oECotizacion.CodUnico, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_RazonSocial", DbType.String, 100, oECotizacion.NombreCliente, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@piv_Ejecutivo", DbType.String, 100, oECotizacion.Codigoejecutivoleasing, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@piv_ClasifBien", DbType.String, 100, oECotizacion.Codigoclasificacionbien, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@piv_Zonal", DbType.String, 100, oECotizacion.CodigoZonal, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@piv_FechaInicio", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oECotizacion.FechaInicio), ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@piv_FechaFin", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oECotizacion.FechaFin), ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@piv_ClasifContrato", DbType.String, 10, oECotizacion.CodigoClasificacionContrato, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@piv_CodigoEstadoContrato", DbType.String, 2, oECotizacion.CodigoEstadoContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoCotizacion_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoContratoCotizacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    Public Function ContratoCuentas(ByVal codigoContrato As String) As String Implements IContratoNTx.ContratoCuentas
        Dim odtbContratoCuenta As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@CodSolicitudCredito", DbType.String, 8, codigoContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoCuenta"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContratoCuenta = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbContratoCuenta)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoCuentas", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    'INICIO IBK - RPR
    Public Function ObtenerContrato(ByVal codigoContrato As String) As String Implements IContratoNTx.ObtenerContrato
        Dim odtbContrato As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8, codigoContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "Up_gcc_SolicitudCredito_ret"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContrato = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbContrato)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerContrato", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function ObtenerCuentasContrato(ByVal codigoContrato As String) As String Implements IContratoNTx.ObtenerCuentasContrato
        Dim odtbContratoCuenta As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodOperacionActiva", DbType.String, 8, codigoContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoCuenta_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContratoCuenta = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbContratoCuenta)
        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerCuentasContrato", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function
    'FIN IBK

    ''' <summary>
    ''' Obtener Datos Contrato y Cotizacion por numero de Contrato
    ''' </summary>
    ''' <param name="pstrNroContrato">Numero de Contrato para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 17/05/2012
    ''' </remarks>
    Public Function ObtenerContratoCotizacion(ByVal pstrNroContrato As String) As String Implements IContratoNTx.ObtenerContratoCotizacion
        'Variables
        Dim odtbObtener As New DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_NroContrato", DbType.String, 8, pstrNroContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoCotizacion_Get"
            objRequest.Parameters.AddRange(prmParameter)

            odtbObtener = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerContratoCotizacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbObtener)
    End Function

    Public Function RetornarObservacionContratoDocumento(ByVal PEGccDocumentoObservacion As String) As String Implements IContratoNTx.RetornarObservacionContratoDocumento
        Dim oEObservacionDocumento As EGcc_documentoobservacion = Nothing
        Dim odtListaObservacion As New DataTable
        Dim prmParameter(2) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEObservacionDocumento = CFunciones.DeserializeObject(Of EGcc_documentoobservacion)(PEGccDocumentoObservacion)
        prmParameter(0) = New DAABRequest.Parameter("@CodigoContratoDocumento", DbType.Int32, 4, oEObservacionDocumento.Codigocontratodocumento, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@NumeroContrato", DbType.String, 8, oEObservacionDocumento.Numerocontrato, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@FlagOrigenObservacion", DbType.Int32, 1, oEObservacionDocumento.FlagOrigenObservacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DocumentoObservacion_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtListaObservacion = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            'Using oReader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
            '    If oReader.Read Then
            '        oEObservacionDocumento = New EGcc_documentoobservacion
            '        With oEObservacionDocumento
            '            .Codigocontratodocumento = CFunciones.CheckInt(oReader("CodigoContratoDocumento"))
            '            .Numerocontrato = CFunciones.CheckStr(oReader("NumeroContrato"))
            '            .Observacion = CFunciones.CheckStr(oReader("observacion"))
            '            .Numeroobservacion = CFunciones.CheckInt(oReader("NumeroObservacion"))
            '            .Codigotipoobservacion = CFunciones.CheckStr(oReader("CodigoTipoObservacion"))
            '            oListaObservacionDocumento.Add(oEObservacionDocumento)
            '        End With
            '    End If
            'End Using

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RetornarObservacionContratoDocumento", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtListaObservacion)
    End Function
    'Inicio_JJM
    Public Function RetornarObservacionContratoDocumentoInafectacion(ByVal PEGccDocumentoObservacion As String) As String Implements IContratoNTx.RetornarObservacionContratoDocumentoInafectacion
        Dim oEObservacionDocumento As EGcc_documentoobservacion = Nothing
        Dim odtListaObservacion As New DataTable
        Dim prmParameter(2) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEObservacionDocumento = CFunciones.DeserializeObject(Of EGcc_documentoobservacion)(PEGccDocumentoObservacion)
        prmParameter(0) = New DAABRequest.Parameter("@CodigoContratoDocumento", DbType.Int32, 4, oEObservacionDocumento.Codigocontratodocumento, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@NumeroContrato", DbType.String, 8, oEObservacionDocumento.Numerocontrato, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@FlagOrigenObservacion", DbType.Int32, 1, oEObservacionDocumento.FlagOrigenObservacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DocumentoObservacion_Inafectacion_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtListaObservacion = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RetornarObservacionContratoDocumento", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtListaObservacion)
    End Function
    'FIN_JJM


    ''' <summary>
    ''' ListadoSeguimientoGlobal
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pSeguimientoContrato"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListadoSeguimientoGlobal(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal pCodContrato As String, _
                                              ByVal pCodCotizacion As String) As String Implements IContratoNTx.ListadoSeguimientoGlobal

        'Variables
        Dim odtbListado As New DataTable
        Dim oEseguimientocontrato As New EGcc_seguimientocontrato  'EGcc_cotizacion
        Dim prmParameter(5) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, pCodContrato, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_CodigoCotizacion", DbType.String, 8, pCodCotizacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_SeguimientoGlobal_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoSeguimientoGlobal", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    ''' <summary>
    ''' ListadoSeguimientoContrato
    ''' </summary>
    ''' <param name="pPageSize"></param>
    ''' <param name="pCurrentPage"></param>
    ''' <param name="pSortColumn"></param>
    ''' <param name="pSortOrder"></param>
    ''' <param name="pSeguimientoContrato"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListadoSeguimientoContrato(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal pSeguimientoContrato As String) As String Implements IContratoNTx.ListadoSeguimientoContrato

        'Variables
        Dim odtbListado As New DataTable
        Dim oEseguimientocontrato As New EGcc_seguimientocontrato  'EGcc_cotizacion
        Dim prmParameter(4) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEseguimientocontrato = CFunciones.DeserializeObject(Of EGcc_seguimientocontrato)(pSeguimientoContrato)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEseguimientocontrato.Codsolicitudcredito, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_SeguimientoContrato_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoSeguimientoContrato", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    ''' <summary>
    ''' Listado General de Contrato y Cotizacion para la generación de reportes.
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la pagina</param>
    ''' <param name="pCurrentPage">Pagina Actual</param>
    ''' <param name="pSortColumn">Columna a Ordenar</param>
    ''' <param name="pSortOrder">Tipo de Ordenamiento</param>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - EBL
    ''' Fecha de Creacion : 16/05/2012
    ''' </remarks>
    Public Function ListadoContratoCotizacionRep(ByVal pPageSize As Integer, _
                                                 ByVal pCurrentPage As Integer, _
                                                 ByVal pSortColumn As String, _
                                                 ByVal pSortOrder As String, _
                                                 ByVal pECotizacion As String) As String Implements IContratoNTx.ListadoContratoCotizacionRep

        'Variables
        Dim odtbListado As DataTable
        Dim oECotizacion As EGcc_cotizacion
        Dim prmParameter(14) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oECotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_NroCotizacion", DbType.String, 8, oECotizacion.Codigocotizacion, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_NroContrato", DbType.String, 8, oECotizacion.CodigoContrato, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_CUCliente", DbType.String, 10, oECotizacion.CodUnico, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_RazonSocial", DbType.String, 100, oECotizacion.NombreCliente, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@piv_Ejecutivo", DbType.String, 100, oECotizacion.Codigoejecutivoleasing, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@piv_ClasifBien", DbType.String, 100, oECotizacion.Codigoclasificacionbien, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@piv_Zonal", DbType.String, 100, oECotizacion.CodigoZonal, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@piv_FechaInicio", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oECotizacion.FechaInicio), ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@piv_FechaFin", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oECotizacion.FechaFin), ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@piv_ClasifContrato", DbType.String, 10, oECotizacion.CodigoClasificacionContrato, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@piv_CodigoEstadoContrato", DbType.String, 2, oECotizacion.CodigoEstadoContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoCotizacionRep_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoContratoCotizacionRep", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    ' Inicio IBK - AAE - 03/10/2012 Se agrega método para listar contratocotización en sol Docs
    Function ListadoContratoCotizacionSolDoc(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal pECotizacion As String) As String Implements IContratoNTx.ListadoContratoCotizacionSolDoc
        'Variables
        Dim odtbListado As New DataTable
        Dim oECotizacion As New EGcc_cotizacion
        Dim prmParameter(14) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oECotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_NroCotizacion", DbType.String, 8, oECotizacion.Codigocotizacion, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_NroContrato", DbType.String, 8, oECotizacion.CodigoContrato, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_CUCliente", DbType.String, 10, oECotizacion.CodUnico, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_RazonSocial", DbType.String, 100, oECotizacion.NombreCliente, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@piv_Ejecutivo", DbType.String, 100, oECotizacion.Codigoejecutivoleasing, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@piv_ClasifBien", DbType.String, 100, oECotizacion.Codigoclasificacionbien, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@piv_Zonal", DbType.String, 100, oECotizacion.CodigoZonal, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@piv_FechaInicio", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oECotizacion.FechaInicio), ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@piv_FechaFin", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oECotizacion.FechaFin), ParameterDirection.Input)
            prmParameter(13) = New DAABRequest.Parameter("@piv_ClasifContrato", DbType.String, 10, oECotizacion.CodigoClasificacionContrato, ParameterDirection.Input)
            prmParameter(14) = New DAABRequest.Parameter("@piv_CodigoEstadoContrato", DbType.String, 2, oECotizacion.CodigoEstadoContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoCotizacionSolDoc_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoContratoCotizacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function
    ' fin IBK 

    Public Function ListadoSituacionCreditoContrato(ByVal pContrato As String, _
                                     ByVal pCuCliente As String, _
                                     ByVal pRazonSocial As String, _
                                     ByVal pCotizacion As String, _
                                     ByVal pFechaIni As String, _
                                     ByVal pFechaFin As String, _
                                     ByVal pEjecutivo As String, _
                                     ByVal pEstado As String, _
                                     ByVal pZonal As String, _
                                     ByVal pClasificacion As String, _
                                     ByVal pClasificacionContrato As String, _
                                     ByVal pCodigoBanca As String, _
                                     ByVal pTipoPersona As String, _
                                     ByVal pNotaria As String, _
                                     ByVal pKardex As String) As String Implements IContratoNTx.ListadoSituacionCreditoContrato
        'Variables
        Dim odtbObtener As New DataTable

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()


        Dim prmParameter(14) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8000, pContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_CUCliente", DbType.String, 8000, pCuCliente, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_NombreRazonSocial", DbType.String, 8000, pRazonSocial, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_CodigoCotizacion", DbType.String, 8000, pCotizacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_FechaInicial", DbType.String, 8000, pFechaIni, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_FechaFinal", DbType.String, 8000, pFechaFin, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_EjecutivoLeasing", DbType.String, 100, pEjecutivo, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pic_Estado", DbType.String, 100, pEstado, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@pic_CodigoZonal", DbType.String, 100, pZonal, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pic_CodigoClasificacion", DbType.String, 100, pClasificacion, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pic_CodigoClasifContrato", DbType.String, 100, pClasificacionContrato, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@pic_CodigoBanca", DbType.String, 100, pCodigoBanca, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@pic_CodigoTipoPersona", DbType.String, 100, pTipoPersona, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@pic_CodigoNotaria", DbType.String, 100, pNotaria, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@pic_NroKardex", DbType.String, 10, pKardex)

        Try

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_SITUACIONCREDITO_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtbObtener = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerContratoCotizacion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbObtener)
    End Function

    ''' <summary>
    ''' Listado de Sunat - Contrato Reporte.
    ''' </summary>
    ''' <param name="pdFechaCelebracionIni">Fecha Celebración de Contrato Inicial</param>
    ''' <param name="pdFechaCelebracionFin">Fecha Celebración de Contrato Final</param>
    ''' <param name="pdFechaActivacionIni">Fecha de Activación Inicial</param>
    ''' <param name="pdFechaActivacionFin">Fecha de Activación Final</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 22/01/2013
    ''' </remarks>
    Public Function ListadoReporteSunatContratos(ByVal pdFechaCelebracionIni As DateTime, _
                                                 ByVal pdFechaCelebracionFin As DateTime, _
                                                 ByVal pdFechaActivacionIni As DateTime, _
                                                 ByVal pdFechaActivacionFin As DateTime) As String Implements IContratoNTx.ListadoReporteSunatContratos
        Dim odtbListadoTemporal As DataTable

        'GCCTS_AEP_05032013 - Se realizó validaciones cuando las fechas tienen el formato de 19000101

        Dim fecCeleini As String
        Dim fecCeleFin As String
        Dim fecActiini As String
        Dim fecActiFin As String

        fecCeleini = CHelperDateTime.FormatDateTimeAsYYYYMMDD(pdFechaCelebracionIni)
        fecCeleFin = CHelperDateTime.FormatDateTimeAsYYYYMMDD(pdFechaCelebracionFin)
        fecActiini = CHelperDateTime.FormatDateTimeAsYYYYMMDD(pdFechaActivacionIni)
        fecActiFin = CHelperDateTime.FormatDateTimeAsYYYYMMDD(pdFechaActivacionFin)
        If fecCeleini = "19000101" Then
            fecCeleini = ""
        Else
            fecCeleini = CHelperDateTime.FormatDateTimeAsYYYYMMDD(pdFechaCelebracionIni)
        End If
        If fecCeleFin = "19000101" Then
            fecCeleFin = ""
        Else
            fecCeleFin = CHelperDateTime.FormatDateTimeAsYYYYMMDD(pdFechaCelebracionFin)
        End If

        If fecActiini = "19000101" Then
            fecActiini = ""
        Else
            fecActiini = CHelperDateTime.FormatDateTimeAsYYYYMMDD(pdFechaActivacionIni)
        End If

        If fecActiFin = "19000101" Then
            fecActiFin = ""
        Else
            fecActiFin = CHelperDateTime.FormatDateTimeAsYYYYMMDD(pdFechaActivacionFin)
        End If

        Dim prmParameter(3) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()




        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@sFechaCelebracionIni", DbType.String, 8000, fecCeleini, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@sFechaCelebracionFin", DbType.String, 8000, fecCeleFin, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@sFechaActivacionIni", DbType.String, 8000, fecActiini, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@sFechaActivacionFin", DbType.String, 8000, fecActiFin, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            'GCCTS_AEP_05032013 - Se cambio el nombre de procedure
            objRequest.Command = "UP_GCC_LeasingLeasebackActivados_SEL"   '"up_gcc_SunatContrato_rpt"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoReporteSunatContratos", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function

    ''' <summary>
    ''' Listado de Detalle del Bien Reporte
    ''' </summary>
    ''' <param name="pdFechaActivacionIni">Fecha de Activación Inicial</param>
    ''' <param name="pdFechaActivacionFin">Fecha de Activación Final</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 24/01/2013
    ''' </remarks>
    Public Function ListadoReporteDetalleBien(ByVal pdFechaActivacionIni As DateTime, _
                                              ByVal pdFechaActivacionFin As DateTime) As String Implements IContratoNTx.ListadoReporteDetalleBien
        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pid_FechaActivacionInicial", DbType.Date, 0, pdFechaActivacionIni, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pid_FechaActivacionFinal", DbType.Date, 0, pdFechaActivacionFin, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DetalleBien_rpt"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoReporteDetalleBien", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function

    ''' <summary>
    ''' Lista todos los Saldos de Crédito
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 29/01/2013 09:15:54 a.m. 
    ''' </remarks>
    Public Function fobjListadoSaldosCreditoReporte(ByVal pFechaInicio As String) As String Implements IContratoNTx.fobjListadoSaldosCreditoReporte
        Dim prmParameter(0) As DAABRequest.Parameter
        Dim odtbListado As DataTable = Nothing

        'Campos para la consulta.
        prmParameter(0) = New DAABRequest.Parameter("@piv_FechaInicio", DbType.String, 10, pFechaInicio, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_SaldosCreditoReporte_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjListadoSaldosCreditoReporte", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    ''' <summary>
    ''' Lista todos los Saldos de Crédito
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 05/03/2013  
    ''' </remarks>
    Public Function fobjListadoSaldosCreditoReporteDolares(ByVal pFechaInicio As String) As String Implements IContratoNTx.fobjListadoSaldosCreditoReporteDolares
        Dim prmParameter(0) As DAABRequest.Parameter
        Dim odtbListado As DataTable = Nothing

        'Campos para la consulta.
        prmParameter(0) = New DAABRequest.Parameter("@piv_FechaInicio", DbType.String, 10, pFechaInicio, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_SaldosCreditoReporte_Dolares_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjListadoSaldosCreditoReporte", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    ''' <summary>
    ''' Lista todos los Créditos Activos
    ''' </summary>
    ''' <param name="pTipo">Tipo de Periodo</param>
    ''' <param name="pFecha">Fecha de Periodo</param>
    ''' <param name="pCodigoClasificacionBien">Codigo Clasificación del Bien</param>
    ''' <param name="pCodigoTipoBien">Codigo del Tipo de Bien</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 29/01/2013 04:30:54 p.m. 
    ''' </remarks>
    Public Function fobjListadoContratosActivadosReporte(ByVal pTipo As String, _
                                                         ByVal pFecha As String, _
                                                         ByVal pCodigoClasificacionBien As String, _
                                                         ByVal pCodigoTipoBien As String) As String Implements IContratoNTx.fobjListadoContratosActivadosReporte
        Dim prmParameter(3) As DAABRequest.Parameter
        Dim odtbListado As DataTable = Nothing

        'Campos para la consulta.
        prmParameter(0) = New DAABRequest.Parameter("@pin_Tipo", DbType.Int32, 4, pTipo, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Fecha", DbType.String, 10, pFecha, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodigoClasificacionBien", DbType.String, 3, pCodigoClasificacionBien, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodigoTipoBien", DbType.String, 3, pCodigoTipoBien, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratosActivadosReporte_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjListadoContratosActivadosReporte", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, "Falló: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function
#End Region

#Region "Anexos"

    ''' <summary>
    ''' Devuelve el contrato especificado identificado por el número del crédito para la generación de los anexos.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function RetornarAnexoContrato(ByVal codigoContrato As String) As String Implements IContratoNTx.RetornarAnexoContrato
        Dim odtbContrato As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8, codigoContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Contrato_AnexoIsel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContrato = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbContrato)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RetornarContratoAnexo", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' Retorna los datos de la tarifas a aplicar en el contrato
    ''' </summary>
    ''' <param name="codigoContrato"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function RetornarTarifario(ByVal codigoContrato As String) As String Implements IContratoNTx.RetornarTarifario
        Dim odtbContrato As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8, codigoContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Contrato_AnexoTarifarioSel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContrato = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbContrato)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RetornarTarifario", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function RetornarDatosContratoSituacionCredito(ByVal codigoContrato As String) As String Implements IContratoNTx.RetornarDatosContratoSituacionCredito
        Dim odtbContrato As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8, codigoContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "Up_gcc_SituacionCredito_Contrato_get"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContrato = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbContrato)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Retornar", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function

    Public Function RetornarDatosCronogramaSituacionCredito(ByVal pPageSize As Integer, _
                                              ByVal pCurrentPage As Integer, _
                                              ByVal pSortColumn As String, _
                                              ByVal pSortOrder As String, _
                                              ByVal codigoContrato As String) As String Implements IContratoNTx.RetornarDatosCronogramaSituacionCredito
        Dim odtbContrato As DataTable
        Dim prmParameter(4) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL

            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8, codigoContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_SITUACION_CREDITO_CRONOGRAMA_GET"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContrato = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbContrato)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Retornar", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function
    'Inicion IBK JJM
    Public Function RetornarDatosCronogramaSituacionCreditoExcel(ByVal codigoContrato As String, ByVal fechavalor As String) As String Implements IContratoNTx.RetornarDatosCronogramaSituacionCreditoExcel
        Dim odtbContrato As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL

            prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8, codigoContrato, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_FechaProceso", DbType.String, 10, fechavalor, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_SITUACION_CREDITO_CRONOGRAMA_EXCEL_GET"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContrato = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbContrato)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Retornar", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function
    Public Function RetornarDatosCronogramaPostSituacionCreditoExcel(ByVal codigoContrato As String, ByVal Usuario As String) As String Implements IContratoNTx.RetornarDatosCronogramaPostSituacionCreditoExcel
        Dim odtbContrato As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL

            prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8, codigoContrato, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 10, Usuario, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_SITUACION_CREDITO_POST_CRONOGRAMA_EXCEL_GET"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContrato = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbContrato)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Retornar", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function
    Public Function RetornarDatosCronogramaSituacionCreditoExcelHeader(ByVal codigoContrato As String) As String Implements IContratoNTx.RetornarDatosCronogramaSituacionCreditoExcelHeader
        Dim odtbContrato As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL

            prmParameter(0) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8, codigoContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_CronogramaCabeceraExcel_SEL"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContrato = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbContrato)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Retornar", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function
    Public Function RetornarDatosCronogramaSituacionCreditoExcelResumen(ByVal codigoContrato As String, ByVal Usuario As String) As String Implements IContratoNTx.RetornarDatosCronogramaSituacionCreditoExcelResumen
        Dim odtbContrato As DataSet
        Dim prmParameter(1) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL

            prmParameter(0) = New DAABRequest.Parameter("@pic_CodOperacionActiva", DbType.String, 8, codigoContrato, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, Usuario, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_SITUACION_CREDITO_CRONOGRAMA_EXCEL_RESUMEN_GET"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContrato = objRequest.Factory.ExecuteDataset(objRequest)

            Return CFunciones.SerializeObject(Of DataSet)(odtbContrato)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Retornar", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function
    Public Function RetornarDatosCronogramaSituacionCreditoExcelDetalle(ByVal codigoContrato As String, ByVal Usuario As String) As String Implements IContratoNTx.RetornarDatosCronogramaSituacionCreditoExcelDetalle
        Dim odtbContrato As DataSet
        Dim prmParameter(1) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL

            prmParameter(0) = New DAABRequest.Parameter("@pic_CodUsuario", DbType.String, 12, Usuario, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_CodOperacionActiva", DbType.String, 8, codigoContrato, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_SITUACION_CREDITO_CRONOGRAMA_EXCEL_CUOTAS_ATRASADAS_GET"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContrato = objRequest.Factory.ExecuteDataset(objRequest)

            Return CFunciones.SerializeObject(Of DataSet)(odtbContrato)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Retornar", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function
    'Fin IBK JJM
    Public Function RetornarDatosGastosSituacionCredito(ByVal pPageSize As Integer, _
                                                  ByVal pCurrentPage As Integer, _
                                                  ByVal pSortColumn As String, _
                                                  ByVal pSortOrder As String, _
                                                  ByVal codigoContrato As String) As String Implements IContratoNTx.RetornarDatosGastosSituacionCredito
        Dim odtbContrato As DataTable
        Dim prmParameter(4) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL

            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8, codigoContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_SITUACIONCREDITO_GASTOS_CONSULTA_SEL"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContrato = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbContrato)
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

