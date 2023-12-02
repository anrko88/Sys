Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DGestionBienDocTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/10/2012
''' </remarks>
<Guid("7C683308-94D4-4cfe-91B2-E61B6947734D") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DGestionBienDocTx")> _
Public Class DGestionBienDocTx
    Inherits ServicedComponent
    Implements IGestionBienDocTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DGestionBienDocTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta el cotizacioNDocumento para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEGestionBienDoc">Entidad Serializado de GestionBienDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    Public Function InsertarGestionBienDoc(ByVal pEGestionBienDoc As String) As Boolean Implements IGestionBienDocTx.InsertarGestionBienDoc

        Dim oEGestionBienDoc As New EGCC_GestionBienDoc
        Dim prmParameter(10) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGestionBienDoc = CFunciones.DeserializeObject(Of EGCC_GestionBienDoc)(pEGestionBienDoc)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEGestionBienDoc.CodSolicitudCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinanciamiento", DbType.Int16, 0, oEGestionBienDoc.SecFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pii_CodRelacionado", DbType.String, 10, oEGestionBienDoc.CodRelacionado, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pii_Codigodocumento", DbType.Int16, 0, oEGestionBienDoc.Codigodocumento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pii_CodTipoModulo", DbType.String, 3, oEGestionBienDoc.CodTipoModulo, ParameterDirection.Input)

        prmParameter(5) = New DAABRequest.Parameter("@piv_Nombrearchivo", DbType.String, 255, oEGestionBienDoc.Nombrearchivo, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Rutaarchivo", DbType.String, 255, oEGestionBienDoc.Rutaarchivo, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_Comentario", DbType.String, 250, oEGestionBienDoc.Comentario, ParameterDirection.Input)

        'Auditoria
        prmParameter(8) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEGestionBienDoc.Audestadologico, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGestionBienDoc.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGestionBienDoc.Audusuariomodificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_GestionBienDoc_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnInsertarGestionBienDoc", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
    ''' <param name="pEGestionBienDoc">Entidad Serializada de GestionBienDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    Public Function ModificarGestionBienDoc(ByVal pEGestionBienDoc As String) As Boolean Implements IGestionBienDocTx.ModificarGestionBienDoc

        Dim oEGestionBienDoc As New EGCC_GestionBienDoc
        Dim prmParameter(10) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGestionBienDoc = CFunciones.DeserializeObject(Of EGCC_GestionBienDoc)(pEGestionBienDoc)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEGestionBienDoc.CodSolicitudCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinanciamiento", DbType.Int16, 0, oEGestionBienDoc.SecFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pii_CodRelacionado", DbType.String, 10, oEGestionBienDoc.CodRelacionado, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pii_Codigodocumento", DbType.Int16, 0, oEGestionBienDoc.Codigodocumento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pii_CodTipoModulo", DbType.String, 3, oEGestionBienDoc.CodTipoModulo, ParameterDirection.Input)

        prmParameter(5) = New DAABRequest.Parameter("@piv_Nombrearchivo", DbType.String, 255, oEGestionBienDoc.Nombrearchivo, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Rutaarchivo", DbType.String, 255, oEGestionBienDoc.Rutaarchivo, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_Comentario", DbType.String, 250, oEGestionBienDoc.Comentario, ParameterDirection.Input)

        'Auditoria
        prmParameter(8) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEGestionBienDoc.Audestadologico, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEGestionBienDoc.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGestionBienDoc.Audusuariomodificacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_GestionBienDoc_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarGestionBienDoc", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
    ''' <param name="pEGestionBienDoc">Entidad Serializada de GestionBienDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    Public Function EliminarGestionBienDoc(ByVal pEGestionBienDoc As String) As Boolean Implements IGestionBienDocTx.EliminarGestionBienDoc

        Dim oEGestionBienDoc As New EGCC_GestionBienDoc
        Dim prmParameter(6) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEGestionBienDoc = CFunciones.DeserializeObject(Of EGCC_GestionBienDoc)(pEGestionBienDoc)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEGestionBienDoc.CodSolicitudCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinanciamiento", DbType.Int16, 0, oEGestionBienDoc.SecFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pii_CodRelacionado", DbType.String, 10, oEGestionBienDoc.CodRelacionado, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pii_Codigodocumento", DbType.Int16, 0, oEGestionBienDoc.Codigodocumento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pii_CodTipoModulo", DbType.String, 3, oEGestionBienDoc.CodTipoModulo, ParameterDirection.Input)

        'Auditoria
        prmParameter(5) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEGestionBienDoc.Audestadologico, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEGestionBienDoc.Audusuariomodificacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_GestionBienDoc_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarGestionBienDoc", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
''' Implementación de la clase DGestionBienDocNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("B6DC9DA3-A22F-407c-89E6-9A27EAA3CB7C") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DGestionBienDocNTx")> _
Public Class DGestionBienDocNTx
    Inherits ServicedComponent
    Implements IGestionBienDocNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DGestionBienDocNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene el GestionBienDoc de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <param name="pEGestionBienDoc">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 21/10/2012 
    ''' </remarks>
    Public Function ObtenerGestionBienDoc(ByVal pEGestionBienDoc As String) As String Implements IGestionBienDocNTx.ObtenerGestionBienDoc

        Dim odtbListadoCotizacionDoc As DataTable
        Dim prmParameter(4) As DAABRequest.Parameter
        Dim oEGestionBienDoc As New EGCC_GestionBienDoc
        oEGestionBienDoc = CFunciones.DeserializeObject(Of EGCC_GestionBienDoc)(pEGestionBienDoc)

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEGestionBienDoc.CodSolicitudCredito, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_SecFinanciamiento", DbType.Int16, 0, oEGestionBienDoc.SecFinanciamiento, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pii_CodRelacionado", DbType.String, 10, oEGestionBienDoc.CodRelacionado, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@pii_Codigodocumento", DbType.Int16, 0, oEGestionBienDoc.Codigodocumento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@pii_CodTipoModulo", DbType.String, 3, oEGestionBienDoc.CodTipoModulo, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_GestionBienDoc_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCotizacionDoc = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerGestionBienDoc", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
    ''' Fecha de Creación  : 17/10/2012
    ''' </remarks>
    Public Function ListadoGestionBienDoc(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEGestionBienDoc As String _
                                                ) As String Implements IGestionBienDocNTx.ListadoGestionBienDoc

        Dim odtbListadoGestionBienDoc As DataTable
        Dim prmParameter(7) As DAABRequest.Parameter
        Dim oEGestionBienDoc As New EGCC_GestionBienDoc
        oEGestionBienDoc = CFunciones.DeserializeObject(Of EGCC_GestionBienDoc)(pEGestionBienDoc)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEGestionBienDoc.CodSolicitudCredito, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pii_SecFinanciamiento", DbType.Int16, 0, oEGestionBienDoc.SecFinanciamiento, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pii_CodRelacionado", DbType.String, 10, oEGestionBienDoc.CodRelacionado, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@pii_CodTipoModulo", DbType.String, 3, oEGestionBienDoc.CodTipoModulo, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_GestionBienDoc_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoGestionBienDoc = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoGestionBienDoc", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoGestionBienDoc)
    End Function

#End Region

End Class

#End Region

