Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DEjecutivoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 19/03/2013
''' </remarks>
<Guid("1805E5F5-1EB3-4d96-82F1-2C7C2EBE5B3C") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DEjecutivoTx")> _
Public Class DEjecutivoTx
    Inherits ServicedComponent
    Implements IEjecutivoTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DEjecutivoTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta el contacto para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEEjecutivo">Entidad Serializado de Ejecutivo formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 19/03/2013
    ''' </remarks>
    Public Function InsertarEjecutivo(ByVal pEEjecutivo As String) As Boolean Implements IEjecutivoTx.InsertarEjecutivo

        Dim oEEjecutivo As New EGCC_Ejecutivo
        Dim prmParameter(3) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEEjecutivo = CFunciones.DeserializeObject(Of EGCC_Ejecutivo)(pEEjecutivo)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_tabla", DbType.String, 6, oEEjecutivo.ID_Tabla, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_codEjecutivo", DbType.String, 10, oEEjecutivo.CodigoEjecutivo, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_nomEjecutivo", DbType.String, 100, oEEjecutivo.NombreEjecutivo, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_codigoEL", DbType.String, 10, oEEjecutivo.CodELeasing, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Ejecutivos_Ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnInsertarEjecutivo", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Modificar el contacto de una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEEjecutivo">Entidad Serializada de Ejecutivo formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 19/03/2013
    ''' </remarks>
    Public Function ModificarEjecutivo(ByVal pEEjecutivo As String) As Boolean Implements IEjecutivoTx.ModificarEjecutivo

        Dim oEEjecutivo As New EGCC_Ejecutivo
        Dim prmParameter(4) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEEjecutivo = CFunciones.DeserializeObject(Of EGCC_Ejecutivo)(pEEjecutivo)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_tabla", DbType.String, 6, oEEjecutivo.ID_Tabla, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_codigo", DbType.String, 3, oEEjecutivo.Codigo, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_codEjecutivo", DbType.String, 10, oEEjecutivo.CodigoEjecutivo, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_nomEjecutivo", DbType.String, 100, oEEjecutivo.NombreEjecutivo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_codigoEL", DbType.String, 10, oEEjecutivo.CodELeasing, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Ejecutivos_Upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarEjecutivo", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Eliminar
    ''' </summary>
    ''' <param name="pEEjecutivo">Entidad Serializada de Ejecutivo formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 19/03/2013
    ''' </remarks>
    Public Function EliminarEjecutivo(ByVal pEEjecutivo As String) As Boolean Implements IEjecutivoTx.EliminarEjecutivo

        Dim oEEjecutivo As New EGCC_Ejecutivo
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEEjecutivo = CFunciones.DeserializeObject(Of EGCC_Ejecutivo)(pEEjecutivo)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_tabla", DbType.String, 6, oEEjecutivo.ID_Tabla, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_codigo", DbType.String, 3, oEEjecutivo.Codigo, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Ejecutivos_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "EliminarEjecutivo", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
''' Implementación de la clase DEjecutivoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 19/03/2013
''' </remarks>
<Guid("C8DC8BEC-92C8-442a-A74C-8B5F4467F984") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DEjecutivoNTx")> _
Public Class DEjecutivoNTx
    Inherits ServicedComponent
    Implements IEjecutivoNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DEjecutivoNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene el Ejecutivo de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 19/03/2013
    ''' </remarks>
    Public Function ObtenerEjecutivo(ByVal pstrCodTabla As String, ByVal pstrCodigo As String) As String Implements IEjecutivoNTx.ObtenerEjecutivo

        Dim oEEjecutivo As EGcc_contacto = Nothing
        Dim prmParameter(1) As DAABRequest.Parameter
        Dim odtbListado As DataTable

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_tabla", DbType.String, 6, pstrCodTabla, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_codigo", DbType.String, 3, pstrCodigo, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Ejecutivos_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerEjecutivo", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)

    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 19/03/2013
    ''' </remarks>
    Public Function ListadoEjecutivo(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pEEjecutivo As String) As String Implements IEjecutivoNTx.ListadoEjecutivo

        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(7) As DAABRequest.Parameter

        Dim oEEjecutivo As New EGCC_Ejecutivo
        oEEjecutivo = CFunciones.DeserializeObject(Of EGCC_Ejecutivo)(pEEjecutivo)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_tabla", DbType.String, 6, oEEjecutivo.ID_Tabla, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_tipo", DbType.String, 6, oEEjecutivo.Tipo, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_codigo", DbType.String, 3, oEEjecutivo.CodigoEjecutivo, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_nombre", DbType.String, 100, oEEjecutivo.NombreEjecutivo, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Ejecutivos_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoEjecutivo", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function

#End Region

End Class

#End Region
