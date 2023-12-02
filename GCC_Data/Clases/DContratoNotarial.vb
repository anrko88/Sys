Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports TSF.DAAB

Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DContratoNotarialTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("1FBFFA58-A240-49fc-8FF6-DE18943D6746") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DContratoNotarialTx")> _
Public Class DContratoNotarialTx
    Inherits ServicedComponent
    Implements IContratoNotarialTx


#Region "Constantes"


    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DContratoNotarialTx"

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Inserta un nuevo registro en la tabla T_TEMPORAL
    ''' </summary>
    ''' <param name="pEGCC_ContratoNotarial">Entidad Temporal serializada</param>
    ''' <returns>String con el número de Temporal</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function InsertarContratoNotarial(ByVal pEGCC_ContratoNotarial As String) As Integer Implements IContratoNotarialTx.InsertarContratoNotarial
        Dim oEGCC_ContratoNotarial As EGcc_contratonotarial

        Dim parSalida1 As IDataParameter
        Dim prmParameter(12) As DAABRequest.Parameter

        'Deserializa la Entidad
        oEGCC_ContratoNotarial = CFunciones.DeserializeObject(Of EGcc_contratonotarial)(pEGCC_ContratoNotarial)

        'Campos TEMPORAL 
        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, oEGCC_ContratoNotarial.Numerocontrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_CodigoNotaria", DbType.String, 100, oEGCC_ContratoNotarial.Codigonotaria, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_CodUbigeo", DbType.String, 6, oEGCC_ContratoNotarial.Codigoubigeo, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Kardex", DbType.String, 15, oEGCC_ContratoNotarial.Kardex, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_Fecha", DbType.String, 8, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEGCC_ContratoNotarial.Fecha), ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_CodigoTipoMinuta", DbType.String, 100, oEGCC_ContratoNotarial.Codigotipominuta, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Observacion", DbType.String, 250, oEGCC_ContratoNotarial.Observacion, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_NombreArchivo", DbType.String, 250, oEGCC_ContratoNotarial.Nombrearchivo, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_Motivo", DbType.String, 500, oEGCC_ContratoNotarial.Motivo, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pic_CodigoOrigenAdenda", DbType.String, 100, oEGCC_ContratoNotarial.Codigoorigenadenda, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pic_AudUsuarioRegistro", DbType.String, 12, oEGCC_ContratoNotarial.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@picFechaEscrituraPublica", DbType.String, 8, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEGCC_ContratoNotarial.FechaEscrituraPublica), ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@picCodigoPorCuenta", DbType.String, 100, oEGCC_ContratoNotarial.CodigoPorCuenta, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoNotarial_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "oEGCC_ContratoNotarial", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parSalida1 = CType(obRequest.Parameters(0), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckInt(parSalida1.Value.ToString())

    End Function

    ''' <summary>
    ''' Modifica un registro existente de la tabla EGCC_ContratoNotarial
    ''' </summary>
    ''' <param name="pEGCC_ContratoNotarial">Entidad Temporal serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ModificarContratoNotarial(ByVal pEGCC_ContratoNotarial As String) As Boolean Implements IContratoNotarialTx.ModificarContratoNotarial
        Dim oEGCC_ContratoNotarial As EGcc_contratonotarial

        Dim prmParameter(13) As DAABRequest.Parameter

        'Deserializa la Entidad
        oEGCC_ContratoNotarial = CFunciones.DeserializeObject(Of EGcc_contratonotarial)(pEGCC_ContratoNotarial)

        'Campos TEMPORAL 
        prmParameter(0) = New DAABRequest.Parameter("@pii_CodigoNotarial", DbType.Int16, 0, oEGCC_ContratoNotarial.Codigonotarial, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, oEGCC_ContratoNotarial.Numerocontrato, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_CodigoNotaria", DbType.String, 100, oEGCC_ContratoNotarial.Codigonotaria, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_CodUbigeo", DbType.String, 6, oEGCC_ContratoNotarial.Codigoubigeo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Kardex", DbType.String, 15, oEGCC_ContratoNotarial.Kardex, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_Fecha", DbType.String, 8, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEGCC_ContratoNotarial.Fecha), ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_CodigoTipoMinuta", DbType.String, 100, oEGCC_ContratoNotarial.Codigotipominuta, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_Observacion", DbType.String, 250, oEGCC_ContratoNotarial.Observacion, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_NombreArchivo", DbType.String, 250, oEGCC_ContratoNotarial.Nombrearchivo, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_Motivo", DbType.String, 500, oEGCC_ContratoNotarial.Motivo, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pic_CodigoOrigenAdenda", DbType.String, 100, oEGCC_ContratoNotarial.Codigoorigenadenda, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@pic_AudUsuarioRegistro", DbType.String, 12, oEGCC_ContratoNotarial.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@picFechaEscrituraPublica", DbType.String, 8, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEGCC_ContratoNotarial.FechaEscrituraPublica), ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@picCodigoPorCuenta", DbType.String, 100, oEGCC_ContratoNotarial.CodigoPorCuenta, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_ContratoNotarial_upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarContratoNotarial", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Modifica un registro existente de la tabla EGCC_ContratoNotarial, cuando el dato contenido pertenece a una adenda.
    ''' </summary>
    ''' <param name="pEGCC_ContratoNotarial">Entidad Temporal serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ModificarAdenda(ByVal pEGCC_ContratoNotarial As String) As Boolean Implements IContratoNotarialTx.ModificarAdenda
        Dim oEGCC_ContratoNotarial As EGcc_contratonotarial

        Dim prmParameter(10) As DAABRequest.Parameter

        'Deserializa la Entidad
        oEGCC_ContratoNotarial = CFunciones.DeserializeObject(Of EGcc_contratonotarial)(pEGCC_ContratoNotarial)

        'Campos contrato notarial 
        prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, oEGCC_ContratoNotarial.Numerocontrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_CodigoNotarial", DbType.Int16, 0, oEGCC_ContratoNotarial.Codigonotarial, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_Fecha", DbType.String, 8, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEGCC_ContratoNotarial.Fecha), ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@picFechaEscrituraPublica", DbType.String, 8, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEGCC_ContratoNotarial.FechaEscrituraPublica), ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_NombreArchivo", DbType.String, 255, oEGCC_ContratoNotarial.Nombrearchivo, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Motivo", DbType.String, 500, oEGCC_ContratoNotarial.Motivo, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@picCodigoPorCuenta", DbType.String, 100, oEGCC_ContratoNotarial.CodigoPorCuenta, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pic_CodigoNotaria", DbType.String, 100, oEGCC_ContratoNotarial.Codigonotaria, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_Kardex", DbType.String, 15, oEGCC_ContratoNotarial.Kardex, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@pic_CodUbigeo", DbType.String, 6, oEGCC_ContratoNotarial.Codigoubigeo, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@pic_AudUsuarioRegistro", DbType.String, 12, oEGCC_ContratoNotarial.Audusuarioregistro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_ContratoNotarialAdenda_upd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarContratoNotarial", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Elimina una lista de objetos EGCC_ContratoNotarial, identificándolo por su
    ''' CodigoNotarial y NumeroContrato.
    ''' </summary>
    ''' <param name="pEGcc_contratonotarial">Objeto Temporal(Serializado)</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 10/05/2012
    ''' </remarks> 
    Public Function EliminarContratoNotarial(ByVal pEGcc_contratonotarial As String) As Boolean Implements IContratoNotarialTx.EliminarContratoNotarial
        Dim opEGcc_contratonotarial As EGcc_contratonotarial


        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        opEGcc_contratonotarial = CFunciones.DeserializeObject(Of EGcc_contratonotarial)(pEGcc_contratonotarial)

        'Campos T_CLIENTE	
        prmParameter(0) = New DAABRequest.Parameter("@pii_CodigoNotarial", DbType.Int16, 0, opEGcc_contratonotarial.Codigonotarial, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 0, opEGcc_contratonotarial.Numerocontrato, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_ContratoNotarial_del"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "EliminarContratoNotarial", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Actualiza el nombre del archivo adjunto en la tabla GCC_ContratoNotarial.
    ''' </summary>
    ''' <param name="pEGCC_ContratoNotarial">Entidad EGCC_ContratoNotarial serializada</param>
    ''' <returns>True si se grabó correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ActualizarNombreArchivo(ByVal pEGCC_ContratoNotarial As String) As Boolean Implements IContratoNotarialTx.ActualizarNombreArchivo
        Dim oEGCC_ContratoNotarial As EGcc_contratonotarial

        Dim prmParameter(3) As DAABRequest.Parameter

        'Deserializa la Entidad
        oEGCC_ContratoNotarial = CFunciones.DeserializeObject(Of EGcc_contratonotarial)(pEGCC_ContratoNotarial)

        'Campos TEMPORAL 
        prmParameter(0) = New DAABRequest.Parameter("@pii_CodigoNotarial", DbType.Int16, 0, oEGCC_ContratoNotarial.Codigonotarial, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, oEGCC_ContratoNotarial.Numerocontrato, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_NombreArchivo", DbType.String, 255, oEGCC_ContratoNotarial.Codigonotaria, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pic_AudUsuarioModificacion", DbType.String, 12, oEGCC_ContratoNotarial.Codigonotaria, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_ContratoNotarial_NombreArchivoupd"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ActualizarNombreArchivo", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DContratoNotarialNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - EBL
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("B24EAF2A-8DFF-4944-87E4-C4B1848D4A33") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DContratoNotarialNTx")> _
Public Class DContratoNotarialNTx
    Inherits ServicedComponent
    Implements IContratoNotarialNTx

#Region "Constantes"

    Private Const C_NOMBRE_CLASE As String = "DContratoNotarialNTx"

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pNumeroContrato"></param>
    ''' <param name="pCodigoOrigenAdenda">[Notarial/Adenda]</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoContratoNotarial(ByVal pNumeroContrato As String, _
                                            ByVal pCodigoOrigenAdenda As String) As String Implements IContratoNotarialNTx.ListadoContratoNotarial
        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pNumeroContrato, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_CodigoOrigenAdenda", DbType.String, 100, pCodigoOrigenAdenda, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoNotarial_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoContratoNotarial", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <param name="pNumeroContrato"></param>
    ''' <param name="pCodigoOrigenAdenda">[Notarial/Adenda]</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoContratoNotarialPaginado(ByVal pPageSize As Integer, _
                                                    ByVal pCurrentPage As Integer, _
                                                    ByVal pSortColumn As String, _
                                                    ByVal pSortOrder As String, _
                                                    ByVal pNumeroContrato As String, _
                                                    ByVal pCodigoOrigenAdenda As String) As String Implements IContratoNotarialNTx.ListadoContratoNotarialPaginado
        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(5) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pNumeroContrato, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_CodigoOrigenAdenda", DbType.String, 100, pCodigoOrigenAdenda, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoNotarialPaginado_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoContratoNotarialPaginado", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function

#End Region

End Class

#End Region
