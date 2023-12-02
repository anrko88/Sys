Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase No Transaccional"
''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
<Guid("5A5331DB-219E-4de1-8E58-B783A65AD9A7") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DUbigeo")> _
Public Class DUbigeoNTx
    Inherits ServicedComponent
    Implements IUbigeoNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DUbigeoNTx"
#End Region


#Region "metodo"

    'Private Const C_NOMBRE_CLASE As String = "DTemporalNTx"
    Public Function ListadoDepartamento() As String Implements IUbigeoNTx.ListadoDepartamento

        Dim odtbListadoDepartamento As DataTable = Nothing
        'Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        'prmParameter(0) = New DAABRequest.Parameter("@id_tabla", DbType.String, 18, pstrTablaGenerica, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_UbigeoDepartamento_sel"
            'objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            odtbListadoDepartamento = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoDepartamento", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoDepartamento)

    End Function

    Public Function ListadoProvincia(ByVal pstrDepartamento As String) As String Implements IUbigeoNTx.ListadoProvincia

        Dim odtbListadoProvincias As DataTable = Nothing
        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@CodigoDepartamento", DbType.String, 18, pstrDepartamento, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_UbigeoProvincia_sel"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            odtbListadoProvincias = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoProvincia", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoProvincias)

    End Function
    Public Function ListadoDistrito(ByVal pstrDepartamento As String, ByVal pstrProvincia As String) As String Implements IUbigeoNTx.ListadoDistrito

        Dim odtbListadoDistrito As DataTable = Nothing
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@CodigoDepartamento", DbType.String, 18, pstrDepartamento, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@CodigoProvincia", DbType.String, 18, pstrProvincia, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_UbigeoDistrito_sel"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            odtbListadoDistrito = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoDistrito", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoDistrito)

    End Function

    'IBK JJM
    Public Function ListadoMunicipalidad() As String Implements IUbigeoNTx.ListadoMunicipalidad

        Dim odtbListadoMunicipalidad As DataTable = Nothing
        
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Municipalidad_sel"
          
            odtbListadoMunicipalidad = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoMunicipalidad", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoMunicipalidad)

    End Function

    Public Function ListadoMunicipalidadPaginado(ByVal pPageSize As Integer, _
                                            ByVal pCurrentPage As Integer, _
                                            ByVal pSortColumn As String, _
                                            ByVal pSortOrder As String, _
                                            ByVal pCodMunicipalidad As String, _
                                            ByVal pMunicipalidad As String) As String Implements IUbigeoNTx.ListadoMunicipalidadPaginado

       
        'Deserealiza la Entidad
        Dim odtbListadoMunicipalidad As DataTable = Nothing

        'Deserealiza la Entidad
        Dim prmParameter(5) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int32, 3, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int32, 3, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_CodigoMunicipalidad", DbType.String, 3, pCodMunicipalidad, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_Municipalidad", DbType.String, 100, pMunicipalidad, ParameterDirection.Input)
            

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_MUNICIPALIDAD_PAGINADO_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoMunicipalidad = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoMunicipalidad", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoMunicipalidad)

    End Function



#End Region
End Class
#End Region

