Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DSubPrestatarioTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("0A098253-A4D3-4aa9-8D76-C81398FF51A9") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DSubPrestatarioTx")> _
Public Class DSubPrestatarioTx
    Inherits ServicedComponent
    Implements ISubPrestatarioTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DSubPrestatarioTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta el contacto para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pESubPrestatario">Entidad Serializado de SubPrestatario formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 21/05/2012
    ''' </remarks>
    Public Function InsertarSubPrestatario(ByVal pESubPrestatario As String) As String Implements ISubPrestatarioTx.InsertarSubPrestatario

        Dim oESubPrestatario As New ESubprestatario
        Dim parCodSuprestatario As IDataParameter
        Dim prmParameter(4) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oESubPrestatario = CFunciones.DeserializeObject(Of ESubprestatario)(pESubPrestatario)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CODSUPRESTATARIO", DbType.String, 6, oESubPrestatario.Codsubprestatario, ParameterDirection.InputOutput)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CODUNICO", DbType.String, 10, oESubPrestatario.Codunico, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_NOMBRE", DbType.String, 40, oESubPrestatario.Nombresubprestatario, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_TIPODOC", DbType.String, 1, oESubPrestatario.Coddocidentificaciontipo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_NRODOC", DbType.String, 11, oESubPrestatario.Numdocidentificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_SubPrestatario_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnInsertarSubPrestatario", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parCodSuprestatario = CType(obRequest.Parameters(0), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckStr(parCodSuprestatario.Value.ToString())

    End Function

    ''' <summary>
    ''' Modificar el contacto de una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pESubPrestatario">Entidad Serializada de SubPrestatario formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 21/05/2012
    ''' </remarks>
    Public Function ModificarSubPrestatario(ByVal pESubPrestatario As String) As Boolean Implements ISubPrestatarioTx.ModificarSubPrestatario

        Dim oESubPrestatario As New ESubprestatario
        Dim prmParameter(4) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oESubPrestatario = CFunciones.DeserializeObject(Of ESubprestatario)(pESubPrestatario)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CODSUPRESTATARIO", DbType.String, 6, oESubPrestatario.Codsubprestatario, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CODUNICO", DbType.String, 10, oESubPrestatario.Codunico, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_NOMBRE", DbType.String, 40, oESubPrestatario.Nombresubprestatario, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_TIPODOC", DbType.String, 1, oESubPrestatario.Coddocidentificaciontipo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_NRODOC", DbType.String, 11, oESubPrestatario.Numdocidentificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_SubPrestatario_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarSubPrestatario", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
''' Implementación de la clase DSubPrestatarioNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("1B9D8CD7-2CEC-4fa0-8EFD-B2D9EE019ABF") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DSubPrestatarioNTx")> _
Public Class DSubPrestatarioNTx
    Inherits ServicedComponent
    Implements ISubPrestatarioNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DSubPrestatarioNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene el SubPrestatario de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <param name="pstrCodSuprestatario">Codigo Suprestatario</param>
    ''' <param name="pstrCodUnico">Codigo Unico</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 21/05/2012 
    ''' </remarks>
    Public Function ObtenerSubPrestatario(ByVal pstrCodSuprestatario As String, ByVal pstrCodUnico As String) As String Implements ISubPrestatarioNTx.ObtenerSubPrestatario

        Dim odtbListadoSubPrestatario As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_CODSUPRESTATARIO", DbType.String, 6, pstrCodSuprestatario, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CODUNICO", DbType.String, 10, pstrCodUnico, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_SubPrestatario_get"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoSubPrestatario = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerSubPrestatario", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If odtbListadoSubPrestatario Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoSubPrestatario)
        End If

    End Function

#End Region

End Class

#End Region
