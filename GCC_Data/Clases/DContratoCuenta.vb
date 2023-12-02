Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DContratoCuentaTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("45072D32-E5A9-44d6-AE45-703D42D94C85") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DContratoCuentaTx")> _
Public Class DContratoCuentaTx
    Inherits ServicedComponent
    Implements IContratoCuentaTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DContratoCuentaTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Modificar Contrato Cuenta
    ''' </summary>
    ''' <param name="pEGcc_ContratoCuenta">Entidad serializada</param>
    ''' <returns>Resultado Integer</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoCuentaIns(ByVal pEGcc_ContratoCuenta As String) As Boolean Implements IContratoCuentaTx.ContratoCuentaIns

        Dim blnResultado As Boolean
        Dim oEGcc_ContratoCuenta As New EGcc_contratocuenta
        Dim prmParameter(3) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGcc_ContratoCuenta = CFunciones.DeserializeObject(Of EGcc_contratocuenta)(pEGcc_ContratoCuenta)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codsolicitudcredito", DbType.String, 8, oEGcc_ContratoCuenta.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Cuenta", DbType.String, 25, oEGcc_ContratoCuenta.Cuenta, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Codigotipocuenta", DbType.String, 25, oEGcc_ContratoCuenta.Codigotipocuenta, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Codigomoneda", DbType.String, 5, oEGcc_ContratoCuenta.Codigomoneda, ParameterDirection.Input)
        'prmParameter(4) = New DAABRequest.Parameter("@piv_Codigomoneda", DbType.String, 18, oEGcc_ContratoCuenta.Codigomoneda, ParameterDirection.Input)

        'prmParameter(5) = New DAABRequest.Parameter("@piv_Audestadologico", DbType.Int16, 0, oEGcc_ContratoCuenta.Audestadologico, ParameterDirection.Input)
        'prmParameter(6) = New DAABRequest.Parameter("@piv_Audfecharegistro", DbType.Date, 0, oEGcc_ContratoCuenta.Audfecharegistro, ParameterDirection.Input)
        'prmParameter(7) = New DAABRequest.Parameter("@piv_Audfechamodificacion", DbType.Date, 0, oEGcc_ContratoCuenta.Audfechamodificacion, ParameterDirection.Input)
        'prmParameter(6) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGcc_ContratoCuenta.Audusuarioregistro, ParameterDirection.Input)
        'prmParameter(7) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGcc_ContratoCuenta.Audusuariomodificacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoCuenta_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
            blnResultado = True
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoCuentaIns", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            blnResultado = False
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return blnResultado

    End Function

    Public Function ContratoCuentaDel(ByVal pstrESolicitudcredito As String) As Boolean Implements IContratoCuentaTx.ContratoCuentaDel

        'Deserealiza la Entidad
        Dim oESolicitudcredito As New ESolicitudcredito
        oESolicitudcredito = CFunciones.DeserializeObject(Of ESolicitudcredito)(pstrESolicitudcredito)

        'Deserealiza la Entidad
        Dim prmParameter(0) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@CodSolicitudCredito", DbType.String, 8, oESolicitudcredito.Codsolicitudcredito, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoCuenta_del"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            objRequest.Factory.ExecuteNonQuery(objRequest)
            'ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoCuentaDel", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return 1
    End Function

#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DContratoCuentaNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("24F2B10F-2E28-4d56-B011-9FF5FCC5DE24") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DContratoCuentaNTx")> _
Public Class DContratoCuentaNTx
    Inherits ServicedComponent
    Implements IContratoCuentaNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DContratoCuentaNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Listado de Representantes
    ''' </summary>
    ''' <param name="pEGcc_ContratoCuenta">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 07/05/2012
    ''' </remarks>
    Public Function ContratoCuentaSel(ByVal pEGcc_ContratoCuenta As String) As String Implements IContratoCuentaNTx.ContratoCuentaSel

        'Deserealiza la Entidad
        Dim odtbContratoDocumento As DataTable = Nothing
        Dim oEGcc_ContratoCuenta As New EGcc_contratocuenta
        oEGcc_ContratoCuenta = CFunciones.DeserializeObject(Of EGcc_contratocuenta)(pEGcc_ContratoCuenta)

        'Deserealiza la Entidad
        Dim prmParameter(0) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codsolicitudcredito", DbType.String, 8, oEGcc_ContratoCuenta.Codsolicitudcredito, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoCuenta_sel"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            odtbContratoDocumento = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ContratoCuentaSel", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbContratoDocumento)
    End Function

    ''' <summary>
    ''' Obtener las Cuentas de un contrato para WIO
    ''' </summary>
    ''' <param name="pstrNroContrato">Entidad serializada</param>
    ''' <returns>Resultado booleano</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 20/06/2012
    ''' </remarks>
    Public Function ObtenerCtaLeasingWioSel(ByVal pstrNroContrato As String) As String Implements IContratoCuentaNTx.ObtenerCtaLeasingWioSel

        'Deserealiza la Entidad
        Dim odtbContratoDocumento As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(0) As DAABRequest.Parameter
        prmParameter(0) = New DAABRequest.Parameter("@piv_NroContrato", DbType.String, 8, pstrNroContrato, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CtaCargoLeasingWIO_sel"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            odtbContratoDocumento = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerCtaLeasingWioSel", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
