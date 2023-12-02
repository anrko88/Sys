Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DInstruccionDesembolsoDocTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/10/2012
''' </remarks>
<Guid("4BC77E65-35EB-4959-A3C0-CEE83C512FEB") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DInstruccionDesembolsoDocTx")> _
Public Class DInstruccionDesembolsoDocTx
    Inherits ServicedComponent
    Implements IInstruccionDesembolsoDocTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DInstruccionDesembolsoDocTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta el cotizacioNDocumento para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEInstruccionDesembolsoDoc">Entidad Serializado de InstruccionDesembolsoDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    Public Function InsertarInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As Boolean Implements IInstruccionDesembolsoDocTx.InsertarInstruccionDesembolsoDoc

        Dim oEInstruccionDesembolsoDoc As New EGCC_InsDesembolsoDoc
        Dim prmParameter(7) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEInstruccionDesembolsoDoc = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoDoc)(pEInstruccionDesembolsoDoc)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodInstruccionDesembolso", DbType.String, 8, oEInstruccionDesembolsoDoc.CodInstruccionDesembolso, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEInstruccionDesembolsoDoc.CodSolicitudCredito, ParameterDirection.Input)

        prmParameter(2) = New DAABRequest.Parameter("@piv_Nombrearchivo", DbType.String, 255, oEInstruccionDesembolsoDoc.Nombrearchivo, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Rutaarchivo", DbType.String, 255, oEInstruccionDesembolsoDoc.Rutaarchivo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Comentario", DbType.String, 250, oEInstruccionDesembolsoDoc.Comentario, ParameterDirection.Input)

        'Auditoria
        prmParameter(5) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEInstruccionDesembolsoDoc.Audestadologico, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Audusuarioregistro", DbType.String, 12, oEInstruccionDesembolsoDoc.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEInstruccionDesembolsoDoc.Audusuariomodificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_InsDesembolsoDoc_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnInsertarInstruccionDesembolsoDoc", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
    ''' <param name="pEInstruccionDesembolsoDoc">Entidad Serializada de InstruccionDesembolsoDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    Public Function ModificarInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As Boolean Implements IInstruccionDesembolsoDocTx.ModificarInstruccionDesembolsoDoc

        Dim oEInstruccionDesembolsoDoc As New EGCC_InsDesembolsoDoc
        Dim prmParameter(7) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEInstruccionDesembolsoDoc = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoDoc)(pEInstruccionDesembolsoDoc)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodInstruccionDesembolso", DbType.String, 8, oEInstruccionDesembolsoDoc.CodInstruccionDesembolso, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEInstruccionDesembolsoDoc.CodSolicitudCredito, ParameterDirection.Input)

        prmParameter(2) = New DAABRequest.Parameter("@pii_Codigodocumento", DbType.Int16, 0, oEInstruccionDesembolsoDoc.Codigodocumento, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Nombrearchivo", DbType.String, 255, oEInstruccionDesembolsoDoc.Nombrearchivo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Rutaarchivo", DbType.String, 255, oEInstruccionDesembolsoDoc.Rutaarchivo, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_Comentario", DbType.String, 250, oEInstruccionDesembolsoDoc.Comentario, ParameterDirection.Input)

        'Auditoria
        prmParameter(6) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEInstruccionDesembolsoDoc.Audestadologico, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEInstruccionDesembolsoDoc.Audusuariomodificacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_InsDesembolsoDoc_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarInstruccionDesembolsoDoc", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
    ''' <param name="pEInstruccionDesembolsoDoc">Entidad Serializada de InstruccionDesembolsoDoc formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 15/10/2012
    ''' </remarks>
    Public Function EliminarInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As Boolean Implements IInstruccionDesembolsoDocTx.EliminarInstruccionDesembolsoDoc

        Dim oEInstruccionDesembolsoDoc As New EGCC_InsDesembolsoDoc
        Dim prmParameter(4) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEInstruccionDesembolsoDoc = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoDoc)(pEInstruccionDesembolsoDoc)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodInstruccionDesembolso", DbType.String, 8, oEInstruccionDesembolsoDoc.CodInstruccionDesembolso, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEInstruccionDesembolsoDoc.CodSolicitudCredito, ParameterDirection.Input)

        prmParameter(2) = New DAABRequest.Parameter("@pii_Codigodocumento", DbType.Int16, 0, oEInstruccionDesembolsoDoc.Codigodocumento, ParameterDirection.Input)

        'Auditoria
        prmParameter(3) = New DAABRequest.Parameter("@pii_Audestadologico", DbType.Int16, 0, oEInstruccionDesembolsoDoc.Audestadologico, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_Audusuariomodificacion", DbType.String, 12, oEInstruccionDesembolsoDoc.Audusuariomodificacion, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_InsDesembolsoDoc_del"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarInstruccionDesembolsoDoc", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
''' Implementación de la clase DInstruccionDesembolsoDocNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("5E2EB0EF-71DC-43fd-BFFD-A9F083A033C4") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DInstruccionDesembolsoDocNTx")> _
Public Class DInstruccionDesembolsoDocNTx
    Inherits ServicedComponent
    Implements IInstruccionDesembolsoDocNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DInstruccionDesembolsoDocNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene el InstruccionDesembolsoDoc de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <param name="pEInstruccionDesembolsoDoc">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 21/10/2012 
    ''' </remarks>
    Public Function ObtenerInstruccionDesembolsoDoc(ByVal pEInstruccionDesembolsoDoc As String) As String Implements IInstruccionDesembolsoDocNTx.ObtenerInstruccionDesembolsoDoc

        Dim odtbListadoCotizacionDoc As DataTable
        Dim prmParameter(2) As DAABRequest.Parameter
        Dim oEInstruccionDesembolsoDoc As New EGCC_InsDesembolsoDoc
        oEInstruccionDesembolsoDoc = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoDoc)(pEInstruccionDesembolsoDoc)

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodInstruccionDesembolso", DbType.String, 8, oEInstruccionDesembolsoDoc.CodInstruccionDesembolso, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEInstruccionDesembolsoDoc.CodSolicitudCredito, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pii_Codigodocumento", DbType.Int16, 0, oEInstruccionDesembolsoDoc.Codigodocumento, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_InsDesembolsoDoc_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCotizacionDoc = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerInstruccionDesembolsoDoc", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
    Public Function ListadoInstruccionDesembolsoDoc(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pEInstruccionDesembolsoDoc As String _
                                                ) As String Implements IInstruccionDesembolsoDocNTx.ListadoInstruccionDesembolsoDoc

        Dim odtbListadoInstruccionDesembolsoDoc As DataTable
        Dim prmParameter(5) As DAABRequest.Parameter
        Dim oEInstruccionDesembolsoDoc As New EGCC_InsDesembolsoDoc
        oEInstruccionDesembolsoDoc = CFunciones.DeserializeObject(Of EGCC_InsDesembolsoDoc)(pEInstruccionDesembolsoDoc)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_CodInstruccionDesembolso", DbType.String, 8, oEInstruccionDesembolsoDoc.CodInstruccionDesembolso, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oEInstruccionDesembolsoDoc.CodSolicitudCredito, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_InsDesembolsoDoc_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoInstruccionDesembolsoDoc = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoInstruccionDesembolsoDoc", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoInstruccionDesembolsoDoc)
    End Function

#End Region

End Class

#End Region
