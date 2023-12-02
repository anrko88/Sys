Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DCesionContratoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 11/01/2013
''' </remarks>
<Guid("626442A6-59F4-49ae-AD44-0951E73B5B5F") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCesionContratoTx")> _
Public Class DCesionContratoTx
    Inherits ServicedComponent
    Implements ICesionContratoTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DCesionContratoTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Modificar CesionContrato
    ''' </summary>
    ''' <param name="pECesionContrato">Entidad Serializada de CesionContrato formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 11/01/2013
    ''' </remarks>
    Public Function ModificarCesionContrato(ByVal pECesionContrato As String) As Boolean Implements ICesionContratoTx.ModificarCesionContrato

        Dim oECesionContrato As New EGCC_CesionContrato
        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oECesionContrato = CFunciones.DeserializeObject(Of EGCC_CesionContrato)(pECesionContrato)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolCredito", DbType.String, 8, oECesionContrato.NroContrato, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_CesionContrato_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarCesionContrato", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Realizar CesionContrato
    ''' </summary>
    ''' <param name="pECesionContrato">Entidad Serializada de CesionContrato formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 18/01/2013
    ''' </remarks>
    Public Function RealizarCesionContrato(ByVal pECesionContrato As String) As Boolean Implements ICesionContratoTx.RealizarCesionContrato

        Dim oECesionContrato As New EGCC_CesionContrato
        Dim prmParameter(2) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oECesionContrato = CFunciones.DeserializeObject(Of EGCC_CesionContrato)(pECesionContrato)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, oECesionContrato.NroContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodCesionario", DbType.Int16, 0, oECesionContrato.CodCesionario, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_AudUsuarioModificacion", DbType.String, 12, oECesionContrato.UsuarioModificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_RealizarCesionContrato_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "RealizarCesionContrato", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
''' Implementación de la clase DCesionContratoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 11/01/2013
''' </remarks>
<Guid("9615729E-C823-4a33-9103-D802E92BC85D") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCesionContratoNTx")> _
Public Class DCesionContratoNTx
    Inherits ServicedComponent
    Implements ICesionContratoNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DCesionContratoNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene el CesionContrato
    ''' </summary>
    ''' <param name="pECesionContrato">Entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - JRC
    ''' Fecha de Creacion : 11/01/2013
    ''' </remarks>
    Public Function GetCesionContrato(ByVal pECesionContrato As String) As String Implements ICesionContratoNTx.GetCesionContrato

        Dim odtbListadoCotizacionDoc As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter
        Dim oECesionContrato As New EGCC_CesionContrato
        oECesionContrato = CFunciones.DeserializeObject(Of EGCC_CesionContrato)(pECesionContrato)

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_NroContrato", DbType.String, 8, oECesionContrato.NroContrato, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CesionContrato_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCotizacionDoc = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "GetCesionContrato", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 11/01/2013
    ''' </remarks>
    Public Function ListadoCesionContrato(ByVal pPageSize As Integer, _
                                                ByVal pCurrentPage As Integer, _
                                                ByVal pSortColumn As String, _
                                                ByVal pSortOrder As String, _
                                                ByVal pECesionContrato As String _
                                                ) As String Implements ICesionContratoNTx.ListadoCesionContrato

        Dim odtbListadoCesionContrato As DataTable
        Dim prmParameter(11) As DAABRequest.Parameter
        Dim oECesionContrato As New EGCC_CesionContrato
        oECesionContrato = CFunciones.DeserializeObject(Of EGCC_CesionContrato)(pECesionContrato)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_NroContrato", DbType.String, 8, oECesionContrato.NroContrato, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_CUCliente", DbType.String, 10, oECesionContrato.CUCliente, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_RazonSocial", DbType.String, 200, oECesionContrato.RazonSocial, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_TipoDocumento", DbType.String, 3, oECesionContrato.TipoDocumento, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@piv_NroDocumento", DbType.String, 20, oECesionContrato.NroDocumento, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@piv_ClasificacionBien", DbType.String, 3, oECesionContrato.ClasificacionBien, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@piv_EstadoContrato", DbType.String, 3, oECesionContrato.EstadoContrato, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@piv_CesionPosicion", DbType.String, 1, oECesionContrato.CesionPosicion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CesionContrato_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoCesionContrato = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoCesionContrato", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoCesionContrato)
    End Function

    ''' <summary>
    ''' Lista todas las cesiones por contrato
    ''' </summary>
    ''' <param name="pFechaInicio">Fecha de inicio de la consulta</param>
    ''' <param name="pFechaTermino">Fecha de término de la consulta</param>
    ''' <returns>Retorna un conjunto de registros</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - VMA
    ''' Fecha de Creación  : 17/01/2013 10:27:54 a.m. 
    ''' </remarks>
    Public Function ListadoCesionContratoReporte(ByVal pFechaInicio As String, _
                                                ByVal pFechaTermino As String) As String Implements ICesionContratoNTx.ListadoCesionContratoReporte
        Dim odtbListado As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@piv_FechaInicio", DbType.String, 10, pFechaInicio, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_FechaTermino", DbType.String, 10, pFechaTermino, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CesionContratoReporte_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoCesionContratoReporte", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function
#End Region

End Class

#End Region
