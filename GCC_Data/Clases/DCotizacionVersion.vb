Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DCotizacionVersionTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("C2CAB039-428C-4697-AE78-7CB8386A5B13") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCotizacionVersionTx")> _
Public Class DCotizacionVersionTx
    Inherits ServicedComponent
    Implements ICotizacionVersionTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DCotizacionVersionTx"
#End Region

#Region "Metodos"


#End Region

End Class

#End Region

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DCotizacionVersionNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("CE64C1F1-3702-4b86-965F-39970DE65BC4") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DCotizacionVersionNTx")> _
Public Class DCotizacionVersionNTx
    Inherits ServicedComponent
    Implements ICotizacionVersionNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DCotizacionVersionNTx"
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Listado General de Cotizacion Version
    ''' </summary>
    ''' <param name="pPageSize">Tamaño de la pagina</param>
    ''' <param name="pCurrentPage">Pagina Actual</param>
    ''' <param name="pSortColumn">Columna a Ordenar</param>
    ''' <param name="pSortOrder">Tipo de Ordenamiento</param>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 23/05/2012
    ''' </remarks>
    Public Function ListadoCotizacionVersion(ByVal pPageSize As Integer, _
                                             ByVal pCurrentPage As Integer, _
                                             ByVal pSortColumn As String, _
                                             ByVal pSortOrder As String, _
                                             ByVal pECotizacion As String) As String Implements ICotizacionVersionNTx.ListadoCotizacionVersion

        'Variables
        Dim odtbListado As New DataTable
        Dim oECotizacion As New EGcc_cotizacion
        Dim prmParameter(4) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oECotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_NroCotizacion", DbType.String, 8, oECotizacion.Codigocotizacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CotizacionVersion_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoCotizacionVersion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
    End Function

    ''' <summary>
    ''' Obtiene una version de una cotizacion especifica
    ''' </summary>
    ''' <param name="pECotizacion">Entidad Serializado de la cotizacion para la busqueda</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 24/05/2012
    ''' </remarks>
    Public Function ObtenerCotizacionVersion(ByVal pECotizacion As String) As String Implements ICotizacionVersionNTx.ObtenerCotizacionVersion
        'Variables
        Dim odtbVersion As New DataTable
        Dim oECotizacion As New EGcc_cotizacion
        Dim prmParameter(1) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oECotizacion = CFunciones.DeserializeObject(Of EGcc_cotizacion)(pECotizacion)

        Try
            prmParameter(0) = New DAABRequest.Parameter("@piv_NroCotizacion", DbType.String, 8, oECotizacion.Codigocotizacion, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_VersionCotizacion", DbType.String, 8, oECotizacion.Versioncotizacion, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_CotizacionVersion_Get"
            objRequest.Parameters.AddRange(prmParameter)

            odtbVersion = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerCotizacionVersion", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbVersion)
    End Function
#End Region

End Class

#End Region
