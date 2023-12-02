Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DCotizacionDocumentoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("41C47807-E73D-4fe4-BAF0-FC3ACE731F2C") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCotizacionDocumentoTx")> _
Public Class DCotizacionDocumentoTx
    Inherits ServicedComponent
    Implements ICotizacionDocumentoTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DCotizacionDocumentoTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta el cotizacioNDocumento para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad Serializado de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012
    ''' </remarks>
    Public Function InsertarCotizacionDocumento(ByVal pECotizacionDocumento As String) As Boolean Implements ICotizacionDocumentoTx.InsertarCotizacionDocumento

        Dim oECotizacionDocumento As New EGcc_cotizaciondocumento
        Dim prmParameter(6) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oECotizacionDocumento = CFunciones.DeserializeObject(Of EGcc_cotizaciondocumento)(pECotizacionDocumento)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oECotizacionDocumento.Codigocotizacion, ParameterDirection.Input)
        'prmParameter(1) = New DAABRequest.Parameter("@piv_CODIGODOCUMENTO", DbType.Int16, 0, oECotizacionDocumento.Codigodocumento, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Nombrearchivo", DbType.String, 255, oECotizacionDocumento.Nombrearchivo, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Rutaarchivo", DbType.String, 255, oECotizacionDocumento.Rutaarchivo, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Comentario", DbType.String, 250, oECotizacionDocumento.Comentario, ParameterDirection.Input)

        'Auditoria
        prmParameter(4) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oECotizacionDocumento.Audestadologico, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oECotizacionDocumento.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oECotizacionDocumento.Audusuariomodificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CotizacionDocumento_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnInsertarCotizacionDocumento", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Modificar el cotizacioNDocumento de una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad Serializada de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012
    ''' </remarks>
    Public Function ModificarCotizacionDocumento(ByVal pECotizacionDocumento As String) As Boolean Implements ICotizacionDocumentoTx.ModificarCotizacionDocumento

        Dim oECotizacionDocumento As New EGcc_cotizaciondocumento
        Dim prmParameter(6) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oECotizacionDocumento = CFunciones.DeserializeObject(Of EGcc_cotizaciondocumento)(pECotizacionDocumento)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oECotizacionDocumento.Codigocotizacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_Codigodocumento", DbType.Int16, 0, oECotizacionDocumento.Codigodocumento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_Nombrearchivo", DbType.String, 255, oECotizacionDocumento.Nombrearchivo, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Rutaarchivo", DbType.String, 255, oECotizacionDocumento.Rutaarchivo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Comentario", DbType.String, 250, oECotizacionDocumento.Comentario, ParameterDirection.Input)

        'Auditoria
        prmParameter(5) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oECotizacionDocumento.Audestadologico, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oECotizacionDocumento.Audusuariomodificacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CotizacionDocumento_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarCotizacionDocumento", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Eliminar el cotizacioNDocumento de una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad Serializada de CotizacionDocumento formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012
    ''' </remarks>
    Public Function EliminarCotizacionDocumento(ByVal pECotizacionDocumento As String) As Boolean Implements ICotizacionDocumentoTx.EliminarCotizacionDocumento

        Dim oECotizacionDocumento As New EGcc_cotizaciondocumento
        Dim prmParameter(3) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oECotizacionDocumento = CFunciones.DeserializeObject(Of EGcc_cotizaciondocumento)(pECotizacionDocumento)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oECotizacionDocumento.Codigocotizacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_Codigodocumento", DbType.Int16, 0, oECotizacionDocumento.Codigodocumento, ParameterDirection.Input)

        'Auditoria
        prmParameter(2) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oECotizacionDocumento.Audestadologico, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oECotizacionDocumento.Audusuariomodificacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CotizacionDocumento_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarCotizacionDocumento", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
''' Implementación de la clase DCotizacionDocumentoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("308673B2-A9A0-4c7a-A49D-330A67E9904A") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCotizacionDocumentoNTx")> _
Public Class DCotizacionDocumentoNTx
    Inherits ServicedComponent
    Implements ICotizacionDocumentoNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DCotizacionDocumentoNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene el CotizacionDocumento de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <param name="pECotizacionDocumento">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 21/05/2012 
    ''' </remarks>
    Public Function ObtenerCotizacionDocumento(ByVal pECotizacionDocumento As String) As String Implements ICotizacionDocumentoNTx.ObtenerCotizacionDocumento

        Dim odtbListadoCotizacionDoc As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter
        Dim oECotizacionDocumento As New EGcc_cotizaciondocumento
        oECotizacionDocumento = CFunciones.DeserializeObject(Of EGcc_cotizaciondocumento)(pECotizacionDocumento)

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oECotizacionDocumento.Codigocotizacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_Codigodocumento", DbType.Int16, 0, oECotizacionDocumento.Codigodocumento, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CotizacionDocumento_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCotizacionDoc = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerCotizacionDocumento", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/05/2012
    ''' </remarks>
    Public Function ListadoCotizacionDocumento(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pECotizacionDocumento As String _
                                                ) As String Implements ICotizacionDocumentoNTx.ListadoCotizacionDocumento

        Dim odtbListadoCotizacionDocumento As DataTable
        Dim prmParameter(4) As DAABRequest.Parameter
        Dim oECotizacionDocumento As New EGcc_cotizaciondocumento
        oECotizacionDocumento = CFunciones.DeserializeObject(Of EGcc_cotizaciondocumento)(pECotizacionDocumento)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_Codigocotizacion", DbType.String, 8, oECotizacionDocumento.Codigocotizacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CotizacionDocumento_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCotizacionDocumento = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoCotizacionDocumento", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoCotizacionDocumento)
    End Function

#End Region

End Class

#End Region
