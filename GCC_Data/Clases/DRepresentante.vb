Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports Microsoft.VisualBasic

Imports TSF.DAAB

Imports GCC.Common
Imports GCC.Entity


#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DRepresentanteTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("5493289F-3549-43b6-B1C7-3E242619CAA7") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DRepresentanteTx")> _
Public Class DRepresentanteTx
    Inherits ServicedComponent
    Implements IRepresentanteTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DRepresentanteTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Insertar Representantes del cliente
    ''' </summary>
    ''' <param name="pEGcc_Representante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentanteClienteIns(ByVal pEGcc_Representante As String) As Integer Implements IRepresentanteTx.RepresentanteClienteIns

        Dim idpCodigoRepresentante As IDataParameter
        Dim oEGccRepresentante As EGcc_representante
        Dim prmParameter(15) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccRepresentante = CFunciones.DeserializeObject(Of EGcc_representante)(pEGcc_Representante)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@pii_Codigorepresentante", DbType.Int16, 0, oEGccRepresentante.Codigorepresentante, ParameterDirection.Output)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Nombrerepresentante", DbType.String, 200, oEGccRepresentante.Nombrerepresentante, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Nrodocumento", DbType.String, 11, oEGccRepresentante.Nrodocumento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Correo", DbType.String, 120, oEGccRepresentante.Correo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Direccion", DbType.String, 250, oEGccRepresentante.Direccion, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Telefono", DbType.String, 20, oEGccRepresentante.Telefono, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pii_Codigoestadorepresentante", DbType.Int16, 0, oEGccRepresentante.Codigoestadorepresentante, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_Partidaregistral", DbType.String, 50, oEGccRepresentante.Partidaregistral, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_Oficinaregistral", DbType.String, 50, oEGccRepresentante.Oficinaregistral, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_Codigoubigeo", DbType.String, 6, oEGccRepresentante.Codigoubigeo, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_Codigotiporepresentante", DbType.String, 100, oEGccRepresentante.Codigotiporepresentante, ParameterDirection.Input)

        prmParameter(11) = New DAABRequest.Parameter("@piv_Audestadologico", DbType.Int16, 0, 1, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGccRepresentante.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGccRepresentante.Audusuariomodificacion, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@pic_CodUnico", DbType.String, 10, oEGccRepresentante.CodUnico, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@pic_CodigoTipoDocumento", DbType.String, 100, oEGccRepresentante.CodigoTipoDocumento, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Representante_Cliente_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RepresentanteClienteIns", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing

            Throw ex
        Finally
            idpCodigoRepresentante = CType(obRequest.Parameters(0), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckInt(idpCodigoRepresentante.Value.ToString())

    End Function

    ''' <summary>
    ''' Insertar Representantes
    ''' </summary>
    ''' <param name="pEGcc_Representante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentanteIns(ByVal pEGcc_Representante As String) As Integer Implements IRepresentanteTx.RepresentanteIns

        Dim idpCodigoRepresentante As IDataParameter
        Dim oEGccRepresentante As New EGcc_representante
        Dim prmParameter(13) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccRepresentante = CFunciones.DeserializeObject(Of EGcc_representante)(pEGcc_Representante)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@pii_Codigorepresentante", DbType.Int16, 0, oEGccRepresentante.Codigorepresentante, ParameterDirection.Output)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Nombrerepresentante", DbType.String, 200, oEGccRepresentante.Nombrerepresentante, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Nrodocumento", DbType.String, 11, oEGccRepresentante.Nrodocumento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Correo", DbType.String, 120, oEGccRepresentante.Correo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Direccion", DbType.String, 250, oEGccRepresentante.Direccion, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Telefono", DbType.String, 20, oEGccRepresentante.Telefono, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pii_Codigoestadorepresentante", DbType.Int16, 0, oEGccRepresentante.Codigoestadorepresentante, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_Partidaregistral", DbType.String, 50, oEGccRepresentante.Partidaregistral, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_Oficinaregistral", DbType.String, 50, oEGccRepresentante.Oficinaregistral, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_Codigoubigeo", DbType.String, 6, oEGccRepresentante.Codigoubigeo, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_Codigotiporepresentante", DbType.String, 100, oEGccRepresentante.Codigotiporepresentante, ParameterDirection.Input)

        prmParameter(11) = New DAABRequest.Parameter("@piv_Audestadologico", DbType.Int16, 0, 1, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGccRepresentante.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGccRepresentante.Audusuariomodificacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Representante_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoChecklistUpd", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            idpCodigoRepresentante = CType(obRequest.Parameters(0), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckInt(idpCodigoRepresentante.Value.ToString())

    End Function

    ''' <summary>
    ''' Modificar Representantes
    ''' </summary>
    ''' <param name="pEGcc_Representante">Entidad serializada</param>
    ''' <returns>Resultado Integer</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentanteUpd(ByVal pEGcc_Representante As String) As Boolean Implements IRepresentanteTx.RepresentanteUpd

        Dim blnResultado As Boolean
        Dim oEGccRepresentante As EGcc_representante
        Dim prmParameter(13) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccRepresentante = CFunciones.DeserializeObject(Of EGcc_representante)(pEGcc_Representante)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@pii_Codigorepresentante", DbType.Int16, 0, oEGccRepresentante.Codigorepresentante, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Nombrerepresentante", DbType.String, 200, oEGccRepresentante.Nombrerepresentante, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Nrodocumento", DbType.String, 11, oEGccRepresentante.Nrodocumento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Correo", DbType.String, 120, oEGccRepresentante.Correo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Direccion", DbType.String, 250, oEGccRepresentante.Direccion, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Telefono", DbType.String, 20, oEGccRepresentante.Telefono, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pii_Codigoestadorepresentante", DbType.Int16, 0, oEGccRepresentante.Codigoestadorepresentante, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_Partidaregistral", DbType.String, 50, oEGccRepresentante.Partidaregistral, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_Oficinaregistral", DbType.String, 50, oEGccRepresentante.Oficinaregistral, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_Codigoubigeo", DbType.String, 6, oEGccRepresentante.Codigoubigeo, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_Codigotiporepresentante", DbType.String, 100, oEGccRepresentante.Codigotiporepresentante, ParameterDirection.Input)

        prmParameter(11) = New DAABRequest.Parameter("@piv_Audestadologico", DbType.Int16, 0, oEGccRepresentante.Audestadologico, ParameterDirection.Input)
        'prmParameter(12) = New DAABRequest.Parameter("@piv_Audfecharegistro", DbType.Date, 0, oEGccRepresentante.Audfecharegistro, ParameterDirection.Input)
        'prmParameter(13) = New DAABRequest.Parameter("@piv_Audfechamodificacion", DbType.Date, 0, oEGccRepresentante.Audfechamodificacion, ParameterDirection.Input)
        'prmParameter(12) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGccRepresentante.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGccRepresentante.Audusuariomodificacion, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@pic_CodigoTipoDocumento", DbType.String, 100, oEGccRepresentante.CodigoTipoDocumento, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Representante_upd"
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
    ''' Modificar Representantes
    ''' </summary>
    ''' <param name="pEGcc_Representante">Entidad serializada</param>
    ''' <returns>Resultado Integer</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentanteDel(ByVal pEGcc_Representante As String) As Boolean Implements IRepresentanteTx.RepresentanteDel

        Dim blnResultado As Boolean
        Dim oEGccRepresentante As New EGcc_representante
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccRepresentante = CFunciones.DeserializeObject(Of EGcc_representante)(pEGcc_Representante)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@pii_Codigorepresentante", DbType.Int16, 0, oEGccRepresentante.Codigorepresentante, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@PIC_AudUsuarioModificacion", DbType.String, 12, oEGccRepresentante.Audusuariomodificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Representante_del"
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
    ''' Insertar Contrato Representantes
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    <Obsolete("Utilize la clase DContratoRepresentanteTx para realizar esta tarea.")> _
    Public Function RepresentanteContratoIns(ByVal pEGcc_contratorepresentante As String) As Integer Implements IRepresentanteTx.RepresentanteContratoIns

        Dim idpCodigoRepresentante As IDataParameter
        Dim oEGccContratoRepresentante As New EGcc_contratorepresentante
        Dim prmParameter(5) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccContratoRepresentante = CFunciones.DeserializeObject(Of EGcc_contratorepresentante)(pEGcc_contratorepresentante)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@pii_Codigorepresentante", DbType.Int16, 0, oEGccContratoRepresentante.Codigorepresentante, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEGccContratoRepresentante.Numerocontrato, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodigoTipoRepresentante", DbType.String, 100, oEGccContratoRepresentante.Codigotiporepresentante, ParameterDirection.Input)

        prmParameter(3) = New DAABRequest.Parameter("@piv_Audestadologico", DbType.Int16, 0, oEGccContratoRepresentante.Audestadologico, ParameterDirection.Input)
        'prmParameter(0) = New DAABRequest.Parameter("@piv_Audfecharegistro", DbType.Date, 0, oEGccContratoRepresentante.Audfecharegistro, ParameterDirection.Input)
        'prmParameter(0) = New DAABRequest.Parameter("@piv_Audfechamodificacion", DbType.Date, 0, oEGccContratoRepresentante.Audfechamodificacion, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGccContratoRepresentante.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGccContratoRepresentante.Audusuariomodificacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_RepresentanteContrato_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RepresentanteContratoIns", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            idpCodigoRepresentante = CType(obRequest.Parameters(0), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckInt(idpCodigoRepresentante.Value.ToString())

    End Function

    ''' <summary>
    ''' Delete Contrato Representantes
    ''' </summary>
    ''' <param name="pEGcc_contratorepresentante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    <Obsolete("Utilize la clase DContratoRepresentanteTx para realizar esta tarea.")> _
    Public Function RepresentanteContratoDel(ByVal pEGcc_contratorepresentante As String) As Boolean Implements IRepresentanteTx.RepresentanteContratoDel

        Dim blnResultado As Boolean
        Dim oEGccContratoRepresentante As New EGcc_contratorepresentante
        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGccContratoRepresentante = CFunciones.DeserializeObject(Of EGcc_contratorepresentante)(pEGcc_contratorepresentante)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEGccContratoRepresentante.Numerocontrato, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_RepresentanteContrato_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnResultado = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RepresentanteContratoDel", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            blnResultado = False
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnResultado

    End Function

#End Region

End Class

#End Region

#Region "Clase NO Transaccional"


''' <summary>
''' Implementación de la clase DRepresentanteNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("2CDFC795-C058-4be9-89F4-63836636F01D") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DRepresentanteNTx")> _
Public Class DRepresentanteNTx
    Inherits ServicedComponent
    Implements IRepresentanteNTx


#Region "constantes"

    Private Const C_NOMBRE_CLASE As String = "DRepresentanteNTx"

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Listado de Representantes
    ''' </summary>
    ''' <param name="pEGcc_representante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : IJM
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentantesSel(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pFirmaEn As String, _
                                                ByVal pEGcc_representante As String, _
                                                ByVal pNumeroContrato As String) As String Implements IRepresentanteNTx.RepresentantesSel

        'Deserealiza la Entidad
        Dim odtbRepresentante As DataTable = Nothing
        Dim oEGcc_representante As New EGcc_representante
        oEGcc_representante = CFunciones.DeserializeObject(Of EGcc_representante)(pEGcc_representante)

        'Deserealiza la Entidad
        Dim prmParameter(7) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)


        prmParameter(4) = New DAABRequest.Parameter("@pii_FirmaEn", DbType.String, 3, pFirmaEn, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pii_CodigoUbigeo", DbType.String, 6, oEGcc_representante.Codigoubigeo, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pii_CodigoTipoRepresentante", DbType.String, 3, oEGcc_representante.Codigotiporepresentante, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pii_NumeroContrato", DbType.String, 8, pNumeroContrato, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Representante_sel"
            objRequest.Parameters.AddRange(prmParameter)
            'Obtiene el ResulSet
            odtbRepresentante = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RepresentantesSel", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbRepresentante)
    End Function


    Public Function RepresentantesItem(ByVal pNroDocumento As String) As String Implements IRepresentanteNTx.RepresentantesItem

        'Deserealiza la Entidad
        Dim odtbRepresentante As DataTable = Nothing
        'Dim oEGcc_representante As New EGcc_representante
        'oEGcc_representante = CFunciones.DeserializeObject(Of EGcc_representante)(pEGcc_representante)

        'Deserealiza la Entidad
        Dim prmParameter(0) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@NroDocumento", DbType.String, 8, pNroDocumento, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Representante_Item"
            objRequest.Parameters.AddRange(prmParameter)
            'Obtiene el ResulSet
            odtbRepresentante = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RepresentantesItem", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbRepresentante)
    End Function

    ''' <summary>
    ''' Listado de Representantes por Contrato
    ''' </summary>
    ''' <param name="pEGcc_contratoRepresentante">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentantesContratoSel(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEGcc_contratoRepresentante As String) As String Implements IRepresentanteNTx.RepresentantesContratoSel

        'Deserealiza la Entidad
        Dim odtbContratoDocumento As DataTable = Nothing
        Dim oEGcc_contratorepresentante As New EGcc_contratorepresentante
        oEGcc_contratorepresentante = CFunciones.DeserializeObject(Of EGcc_contratorepresentante)(pEGcc_contratoRepresentante)

        'Deserealiza la Entidad
        Dim prmParameter(2) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@piv_Numerocontrato", DbType.String, 8, oEGcc_contratorepresentante.Numerocontrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Codigoubigeo", DbType.String, 6, oEGcc_contratorepresentante.Codigoubigeo, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Codigotiporepresentante", DbType.String, 100, oEGcc_contratorepresentante.Codigotiporepresentante, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoRepresentante_sel"
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
    ''' Listado de Representantes por cliente
    ''' </summary>
    ''' <param name="pCodUnico">Código único del cliente, código de identificación.</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function RepresentantesCliente(ByVal pPageSize As Integer, _
                                          ByVal pCurrentPage As Integer, _
                                          ByVal pSortColumn As String, _
                                          ByVal pSortOrder As String, _
                                          ByVal pCodUnico As String) As String Implements IRepresentanteNTx.RepresentantesCliente

        'Deserealiza la Entidad
        Dim odtbContratoDocumento As DataTable

        'Deserealiza la Entidad
        Dim prmParameter(4) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_CodUnico", DbType.String, 10, pCodUnico, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoRepresentante_cliente_sel"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            odtbContratoDocumento = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RepresentantesCliente", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbContratoDocumento)
    End Function

    ''' <summary>
    ''' Listado de Representantes del cliente por el número de contrato indicado en el parámetro
    ''' </summary>
    ''' <param name="pCodigoContrato">Número de contrato.</param>
    ''' <param name="pCodUnico">Código único del cliente, código de identificación.</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ListarDelClienteSinSeleccionar(ByVal pPageSize As Integer, _
                                                   ByVal pCurrentPage As Integer, _
                                                   ByVal pSortColumn As String, _
                                                   ByVal pSortOrder As String, _
                                                   ByVal pCodigoContrato As String, _
                                                   ByVal pCodUnico As String) As String Implements IRepresentanteNTx.ListarDelCliente

        'Deserealiza la Entidad
        Dim odtbContratoDocumento As DataTable

        'Deserealiza la Entidad
        Dim prmParameter(5) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pCodigoContrato, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_CodUnico", DbType.String, 10, pCodUnico, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoRepresentante_contrato_sel"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            odtbContratoDocumento = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarDelCliente", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
