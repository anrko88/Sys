Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DContratoDocumentoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("5FC89CD2-3E8C-4f7f-9A59-8A077A54119C") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DContratoDocumentoTx")> _
Public Class DContratoDocumentoTx
    Inherits ServicedComponent
    Implements IContratoDocumentoTx


#Region "Constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DContratoDocumentoTx"

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Insertar Documentos/Condiciones para el Contrato
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoDocumentoIns(ByVal pEGcc_contratodocumento As String) As Boolean Implements IContratoDocumentoTx.ContratoDocumentoIns
        Dim blnResultado As Boolean
        Dim oEGccContratodocumento As New EGcc_contratodocumento
        Dim prmParameter(10) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccContratodocumento = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEGcc_contratodocumento)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_Numerocontrato", DbType.String, 8, oEGccContratodocumento.Numerocontrato, ParameterDirection.Input)
        'prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocontratodocumento", DbType.Int16, 0, oEGccContratodocumento.Codigocontratodocumento, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_Codigodocumento", DbType.Int16, 0, oEGccContratodocumento.Codigodocumento, ParameterDirection.Input)
        'prmParameter(0) = New DAABRequest.Parameter("@pii_Aprobarcomercial", DbType.Int16, 0, oEGccContratodocumento.Aprobarcomercial, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pii_Aprobarlegal", DbType.Int16, 0, oEGccContratodocumento.Aprobarlegal, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Nombrearchivo", DbType.String, 255, oEGccContratodocumento.Nombrearchivo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Adjunto", DbType.String, 255, oEGccContratodocumento.Adjunto, ParameterDirection.Input)
        'prmParameter(0) = New DAABRequest.Parameter("@pid_Incotermmonto", DbType.Decimal, ??, oEGccContratodocumento.Incotermmonto, ParameterDirection.Input)
        'prmParameter(0) = New DAABRequest.Parameter("@piv_Codigotipoincoterm", DbType.String, 100, oEGccContratodocumento.Codigotipoincoterm, ParameterDirection.Input)
        'prmParameter(0) = New DAABRequest.Parameter("@piv_Codigoestadochecklist", DbType.String, 100, oEGccContratodocumento.Codigoestadochecklist, ParameterDirection.Input)
        'prmParameter(0) = New DAABRequest.Parameter("@pii_Flagcartaenvio", DbType.Int16, 0, oEGccContratodocumento.Flagcartaenvio, ParameterDirection.Input)
        'prmParameter(0) = New DAABRequest.Parameter("@piv_Observaciones", DbType.String, 250, oEGccContratodocumento.Observaciones, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Codigoorigencondicion", DbType.String, 100, oEGccContratodocumento.Codigoorigencondicion, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Codigotipocondicion", DbType.String, 100, oEGccContratodocumento.Codigotipocondicion, ParameterDirection.Input)

        prmParameter(7) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEGccContratodocumento.Audestadologico, ParameterDirection.Input)
        'prmParameter(0) = New DAABRequest.Parameter("@pid_Audfecharegistro", DbType.Date, 0, oEGccContratodocumento.Audfecharegistro, ParameterDirection.Input)
        'prmParameter(0) = New DAABRequest.Parameter("@pid_Audfechamodificacion", DbType.Date, 0, oEGccContratodocumento.Audfechamodificacion, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGccContratodocumento.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGccContratodocumento.Audusuariomodificacion, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pii_FlagCartaEnvio", DbType.Int32, 1, oEGccContratodocumento.Flagcartaenvio, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoDocumento_ins"
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
    ''' Actualizar Adjunto de Documentos/Condiciones
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoDocumentoAdjuntoUpd(ByVal pEGcc_contratodocumento As String) As Boolean Implements IContratoDocumentoTx.ContratoDocumentoAdjuntoUpd
        Dim blnResultado As Boolean
        Dim oEGccContratodocumento As New EGcc_contratodocumento
        Dim prmParameter(3) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccContratodocumento = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEGcc_contratodocumento)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_Numerocontrato", DbType.String, 8, oEGccContratodocumento.Numerocontrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Codigocontratodocumento", DbType.Int32, 4, oEGccContratodocumento.Codigocontratodocumento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Adjunto", DbType.String, 255, oEGccContratodocumento.Adjunto, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGccContratodocumento.Audusuariomodificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoDocmentoAdjunto_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnResultado = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoDocumentoAdjuntoUpd", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            blnResultado = False
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnResultado

    End Function

    ''' <summary>
    ''' Actualizar Observacion de Documentos/Condiciones
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoDocumentoObsUpd(ByVal pEGcc_contratodocumento As String) As Boolean Implements IContratoDocumentoTx.ContratoDocumentoObsUpd
        Dim blnResultado As Boolean
        Dim oEGccContratodocumento As New EGcc_contratodocumento
        Dim prmParameter(3) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccContratodocumento = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEGcc_contratodocumento)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_Numerocontrato", DbType.String, 8, oEGccContratodocumento.Numerocontrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Codigocontratodocumento", DbType.Int16, 0, oEGccContratodocumento.Codigocontratodocumento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Observaciones", DbType.String, 250, oEGccContratodocumento.Observaciones, ParameterDirection.Input)
        'prmParameter(3) = New DAABRequest.Parameter("@piv_Audfechamodificacion", DbType.Date, 0, oEGccContratodocumento.Audfechamodificacion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGccContratodocumento.Audusuariomodificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoDocumentoObs_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnResultado = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoDocumentoObsUpd", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            blnResultado = False
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnResultado

    End Function

    ''' <summary>
    ''' Actualizar Archivo Adjunto Inafecciones
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : JJM
    ''' Fecha de Creación  : 07/11/2012
    ''' </remarks>
    Public Function ArchivoAdjuntoAfectoUpd(ByVal pEGcc_contratodocumento As String) As Boolean Implements IContratoDocumentoTx.ArchivoAdjuntoAfectoUpd
        Dim blnResultado As Boolean
        Dim oEGccContratodocumento As New EGcc_contratodocumento
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccContratodocumento = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEGcc_contratodocumento)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@pic_codInmatriculacionDetalle", DbType.Int32, oEGccContratodocumento.Codigodocumento, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_adjunto", DbType.String, 250, oEGccContratodocumento.Adjunto, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Bien_Inafectacion_detalle_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnResultado = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ArchiAdjuntoAfectoUpd", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            blnResultado = False
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnResultado

    End Function

    ''' <summary>
    ''' Actualizar Estado de Envio Carta de Documentos/Condiciones
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoDocumentoEnviaCartaUpd(ByVal pEGcc_contratodocumento As String) As Boolean Implements IContratoDocumentoTx.ContratoDocumentoEnviaCartaUpd
        Dim blnResultado As Boolean
        Dim oEGccContratodocumento As New EGcc_contratodocumento
        Dim prmParameter(3) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccContratodocumento = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEGcc_contratodocumento)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_Numerocontrato", DbType.String, 8, oEGccContratodocumento.Numerocontrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Codigocontratodocumento", DbType.Int16, 0, oEGccContratodocumento.Codigocontratodocumento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Flagcartaenvio", DbType.Int16, 0, oEGccContratodocumento.Flagcartaenvio, ParameterDirection.Input)
        'prmParameter(3) = New DAABRequest.Parameter("@piv_Audfechamodificacion", DbType.Date, 0, oEGccContratodocumento.Audfechamodificacion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGccContratodocumento.Audusuariomodificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoDocumentoEnvCarta_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnResultado = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoDocumentoEnviaCartaUpd", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            blnResultado = False
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnResultado

    End Function

    ''' <summary>
    ''' Actualiza los datos del texto predefinido con el nuevo texto ingresado.
    ''' </summary>
    ''' <param name="pEGCC_ContratoDocumento">Objeto ContratoDocumento serializado</param>
    ''' <returns>True si se grabó correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ModificarTextoPredefinido(ByVal pEGCC_ContratoDocumento As String) As Boolean Implements IContratoDocumentoTx.ModificarTextoPredefinido

        Dim oEGCC_ContratoDocumento As New EGcc_contratodocumento

        Dim prmParameter(2) As DAABRequest.Parameter

        ' Deserealiza la Entidad.
        oEGCC_ContratoDocumento = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEGCC_ContratoDocumento)

        ' Campos para la operación de actualización.
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodigoContratoDocumento", DbType.Int32, 0, oEGCC_ContratoDocumento.Codigocontratodocumento, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_TextoPredefinido", DbType.String, 5000, oEGCC_ContratoDocumento.TextoPredefinido, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_Audusuariomodificacion", DbType.String, 12, oEGCC_ContratoDocumento.Audusuariomodificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_ContratoDocumento_TextoPredef_upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ActualizarTextoPredefinido", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Insertar Documentos/Condiciones para Verificacion del Cliente
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Contrato Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Function InsertarContratoDocCliente(ByVal pEGcc_contratodocumento As String) As Integer Implements IContratoDocumentoTx.InsertarContratoDocCliente

        Dim parCodigoCotizacion As IDataParameter
        Dim oEContratoDoc As New EGcc_contratodocumento
        Dim prmParameter(5) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEContratoDoc = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEGcc_contratodocumento)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_NUMEROCONTRATO", DbType.String, 8, oEContratoDoc.Numerocontrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_NOMBREARCHIVO", DbType.String, 255, oEContratoDoc.Nombrearchivo, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CODIGOORIGENCONDICION", DbType.String, 100, oEContratoDoc.Codigoorigencondicion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CODIGOTIPOCONDICION", DbType.String, 100, oEContratoDoc.Codigotipocondicion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_USUARIOREGISTRO", DbType.String, 12, oEContratoDoc.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_CODIGODOCUMENTO", DbType.Int32, 4, oEContratoDoc.Codigodocumento, ParameterDirection.Output)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoDocumentoCli_Ins"
        obRequest.Parameters.AddRange(prmParameter)



        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InsertarContratoDocCliente", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parCodigoCotizacion = CType(obRequest.Parameters(5), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        'Return CFunciones.CheckStr(parCodigoCotizacion.Value.ToString())
        Return parCodigoCotizacion.Value.ToString()
        'Return True
    End Function

    ''' <summary>
    ''' Elimina logicamente Documentos/Condiciones en Verificacion del Cliente
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Contrato Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Function EliminarContratoDocCliente(ByVal pEGcc_contratodocumento As String) As Boolean Implements IContratoDocumentoTx.EliminarContratoDocCliente

        Dim oEContratoDoc As New EGcc_contratodocumento
        Dim prmParameter(2) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEContratoDoc = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEGcc_contratodocumento)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_CODCONTRATODOC", DbType.Int32, 4, oEContratoDoc.Codigocontratodocumento, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_NUMEROCONTRATO", DbType.String, 8, oEContratoDoc.Numerocontrato, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_USUARIOREGISTRO", DbType.String, 12, oEContratoDoc.Audusuarioregistro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoDocumentoCli_Del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "EliminarContratoDocCliente", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    Public Function InsertarContratoDocumentoObservacion(ByVal pEGcc_contratodocumentoObservacion As String) As Boolean Implements IContratoDocumentoTx.InsertarContratoDocumentoObservacion

        Dim oEContratoDocObs As New EGcc_documentoobservacion
        Dim prmParameter(5) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEContratoDocObs = CFunciones.DeserializeObject(Of EGcc_documentoobservacion)(pEGcc_contratodocumentoObservacion)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@CodigoContratoDocumento", DbType.Int32, 4, oEContratoDocObs.Codigocontratodocumento, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@numeroContrato", DbType.String, 8, oEContratoDocObs.Numerocontrato, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@Observacion", DbType.String, 300, oEContratoDocObs.Observacion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@CodigoTipoObservacion", DbType.String, 100, oEContratoDocObs.Codigotipoobservacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@audUsuarioRegistro", DbType.String, 12, oEContratoDocObs.Audusuarioregistro, ParameterDirection.Input)
        'FlagOrigenObservacion Se Agrego para diferencia check list Legal("2") y Comercial ("1")
        prmParameter(5) = New DAABRequest.Parameter("@FlagOrigenObservacion", DbType.Int32, 4, oEContratoDocObs.FlagOrigenObservacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_DocumentoObservacion_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InsertarContratoDocumentoObservacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function
    'Inicio JJM
    Public Function InsertarContratoDocumentoObservacionInafectacion(ByVal pEGcc_contratodocumentoObservacion As String) As Boolean Implements IContratoDocumentoTx.InsertarContratoDocumentoObservacionInafectacion

        Dim oEContratoDocObs As New EGcc_documentoobservacion
        Dim prmParameter(5) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEContratoDocObs = CFunciones.DeserializeObject(Of EGcc_documentoobservacion)(pEGcc_contratodocumentoObservacion)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@CodigoContratoDocumento", DbType.Int32, 4, oEContratoDocObs.Codigocontratodocumento, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@numeroContrato", DbType.String, 8, oEContratoDocObs.Numerocontrato, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@Observacion", DbType.String, 300, oEContratoDocObs.Observacion, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@CodigoTipoObservacion", DbType.String, 100, oEContratoDocObs.Codigotipoobservacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@audUsuarioRegistro", DbType.String, 12, oEContratoDocObs.Audusuarioregistro, ParameterDirection.Input)
        'FlagOrigenObservacion Se Agrego para diferencia check list Legal("2") y Comercial ("1")
        prmParameter(5) = New DAABRequest.Parameter("@FlagOrigenObservacion", DbType.Int32, 4, oEContratoDocObs.FlagOrigenObservacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_DocumentoObservacion_Inafectacion_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InsertarContratoDocumentoObservacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function
    'FinJJM
    Public Function EliminaContratoDocumento(ByVal pEGcc_contratodocumento As String) As Boolean Implements IContratoDocumentoTx.EliminaContratoDocumento

        Dim oEContratoDocumento As New EGcc_contratodocumento ' EGcc_documentoobservacion
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEContratoDocumento = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEGcc_contratodocumento)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@CodigoContratoDocumento", DbType.Int32, 8, oEContratoDocumento.Codigocontratodocumento, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@numerocontrato", DbType.String, 255, oEContratoDocumento.Numerocontrato, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoDocumento_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InsertarContratoDocumentoObservacion", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    Public Function EstadoAprobarLegal(ByVal pEGcc_contratodocumento As String) As Boolean Implements IContratoDocumentoTx.EstadoAprobarLegal

        Dim oEContratoDocumento As New EGcc_contratodocumento ' EGcc_documentoobservacion
        Dim prmParameter(2) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEContratoDocumento = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEGcc_contratodocumento)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@NumeroContrato", DbType.String, 8, oEContratoDocumento.Numerocontrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@CodigoContratoDocumento", DbType.Int32, 4, oEContratoDocumento.Codigocontratodocumento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@AprobarLegal", DbType.Int32, 1, oEContratoDocumento.Aprobarlegal, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoDocumentoAprobarLegal_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "EstadoAprobarLegal", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Envia Carta Documento Cliente
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Contrato Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - Jrc
    ''' Fecha de Creación  : 24/05/2012
    ''' </remarks>
    Public Function EnviarCartaDocumentoCliente(ByVal pEGcc_contratodocumento As String) As Boolean Implements IContratoDocumentoTx.EnviarCartaDocumentoCliente

        Dim oEContratoDoc As New EGcc_contratodocumento
        Dim prmParameter(2) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEContratoDoc = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEGcc_contratodocumento)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_CODCONTRATO", DbType.String, 8, oEContratoDoc.Numerocontrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CODCONTRATODOC", DbType.Int32, 0, oEContratoDoc.Codigocontratodocumento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_USUARIOREGISTRO", DbType.String, 12, oEContratoDoc.Audusuarioregistro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoDocumentoCartaCli_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "EnviarCartaDocumentoCliente", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    Public Function ActualizaFlagAprobacionLegal(ByVal pFlagAprobacionLegal As Integer, ByVal pNumeroContrato As String, ByVal pCodigoContratoDocumento As Integer) As Boolean Implements IContratoDocumentoTx.ActualizaFlagAprobacionLegal

        'Dim oEContratoDoc As New EGcc_contratodocumento
        Dim prmParameter(2) As DAABRequest.Parameter

        'Deserealiza la Entidad
        'oEContratoDoc = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEGcc_contratodocumento)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_FlagAprobacionLegal", DbType.Int32, 5, pFlagAprobacionLegal, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, pNumeroContrato, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pvi_CodigoContratoDocumento", DbType.Int32, 5, pCodigoContratoDocumento, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoDocumento_Aprov_Legal"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ActualizaFlagAprobacionLegal", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
''' Implementación de la clase DContratoDocumentoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("71121612-27E8-42f5-BDDC-6700F95585FC") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DContratoDocumentoNTx")> _
Public Class DContratoDocumentoNTx
    Inherits ServicedComponent
    Implements IContratoDocumentoNTx


#Region "constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DContratoDocumentoNTx"

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Consulta de Documentos de Contrato
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoDocumentoSel(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pEGcc_contratodocumento As String) As String Implements IContratoDocumentoNTx.ContratoDocumentoSel

        'Deserealiza la Entidad
        Dim odtbContratoDocumento As DataTable = Nothing
        Dim oEGccContratodocumento As New EGcc_contratodocumento
        oEGccContratodocumento = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEGcc_contratodocumento)

        'Deserealiza la Entidad
        Dim prmParameter(6) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int32, 4, pPageSize, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int32, 4, pCurrentPage, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@numeroContrato", DbType.String, 8, oEGccContratodocumento.Numerocontrato, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@Flagfiltro", DbType.Int32, 4, oEGccContratodocumento.Flagfiltro, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@FlagCartaEnvio", DbType.Int32, 4, oEGccContratodocumento.Flagcartaenvio, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoDocumento_sel"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            odtbContratoDocumento = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoDocumentoSel", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbContratoDocumento)
    End Function

    ''' <summary>
    ''' Devuelve el texto predefinido del objeto ContratoDocumento.
    ''' </summary>
    ''' <param name="pCodigoContratoDocumento">Código del objeto ContratoDocumento</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RetornarTextoPredefefinido(ByVal pCodigoContratoDocumento As Integer) As String Implements IContratoDocumentoNTx.RetornarTextoPredefefinido
        Dim odtbContratoDocumento As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodigoContratoDocumento", DbType.Int32, 0, pCodigoContratoDocumento, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_ContratoDocumento_TextoPredef_ret"
        objRequest.Parameters.AddRange(prmParameter)

        Try
            'Obtiene el ResulSet
            odtbContratoDocumento = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbContratoDocumento)

        Catch ex As Exception
            Dim oLog As New CLog

            oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RetornarTextoPredefefinido", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function
    ''' <summary>
    ''' Validar la Duplicidad de Condiciones 
    ''' </summary>
    ''' <param name="pEGcc_contratodocumento">Código del objeto ContratoDocumento</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function SelCondicionesDocumentoCli(ByVal pEGcc_contratodocumento As String) As String Implements IContratoDocumentoNTx.SelCondicionesDocumentoCli
        Dim oEGccContratodocumento As EGcc_contratodocumento = Nothing

        Dim prmParameter(2) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccContratodocumento = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(pEGcc_contratodocumento)


        prmParameter(0) = New DAABRequest.Parameter("@numeroContrato", DbType.String, 8, oEGccContratodocumento.Numerocontrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@CodigoTipoCondicion", DbType.String, 100, oEGccContratodocumento.Codigotipocondicion, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@FlagCartaEnvio", DbType.Int32, 8, oEGccContratodocumento.Flagcartaenvio, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_CondicionDocExiste_sel"
        objRequest.Parameters.AddRange(prmParameter)

        Try

            Using Reader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                If Reader.Read Then
                    oEGccContratodocumento = New EGcc_contratodocumento
                    With oEGccContratodocumento
                        .Existe = CFunciones.CheckInt(Reader("Existe"))
                    End With
                End If
            End Using

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RetornarTextoPredefefinido", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of EGcc_contratodocumento)(oEGccContratodocumento)
    End Function

    ''' <summary>
    ''' Consulta de Documentos de Contrato
    ''' </summary>
    ''' <param name="p_numerocontrato">Entidad Documento serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoDocumentoGet(ByVal p_numerocontrato As String) As String Implements IContratoDocumentoNTx.ContratoDocumentoGet

        'Deserealiza la Entidad
        Dim odtbContratoDocumento As DataTable = Nothing
        'Dim oEGccContratodocumento As New EGcc_contratodocumento
        'oEGccContratodocumento = CFunciones.DeserializeObject(Of EGcc_contratodocumento)(p_numerocontrato)

        'Deserealiza la Entidad
        Dim prmParameter(0) As DAABRequest.Parameter

        prmParameter(0) = New DAABRequest.Parameter("@numeroContrato", DbType.String, 8, p_numerocontrato, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_DocumentoCondicionContrato_sel"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            odtbContratoDocumento = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoDocumentoGet", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbContratoDocumento)
    End Function

#End Region


End Class

#End Region
