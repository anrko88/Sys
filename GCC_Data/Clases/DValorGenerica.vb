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
<Guid("FF3FC0BC-4E95-4073-B765-D675C7E91428") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DUtilNTx")> _
Public Class DValorGenericaNTx
    Inherits ServicedComponent
    Implements IValorGenericaNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DValorGenericaNTx"
#End Region



#Region "metodo"

    'Private Const C_NOMBRE_CLASE As String = "DTemporalNTx"
    Public Function ListadoTablaGenerica(ByVal pstrTablaGenerica As String) As String Implements IValorGenericaNTx.ListadoValorGenerica

        Dim odtbListadoValorGenerica As DataTable = Nothing
        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@id_tabla", DbType.String, 18, pstrTablaGenerica, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ValorTablaGenerica_sel"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            odtbListadoValorGenerica = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoTablaGenerica", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoValorGenerica)

    End Function

    'IBK - RPH se agrega un parametro mas para el filtro por provincia
    Public Function ListarNotarias(ByVal pCodDepartamento As String, _
                                   ByVal pCodProvincia As String) As String Implements IValorGenericaNTx.ListarNotarias

        Dim odtbListadoValorGenerica As DataTable
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@CodDepartamento", DbType.String, 2, pCodDepartamento, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@CodProvincia", DbType.String, 2, pCodProvincia, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Notaria_sel"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            odtbListadoValorGenerica = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoNotarias", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoValorGenerica)

    End Function
    'IBK - RPH
    Public Function ObtenerContactoNotarias(ByVal pCodNotaria As String) As String Implements IValorGenericaNTx.ObtenerContactoNotarias
        Dim odtbListadoValorGenerica As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@CodNotaria", DbType.String, 3, pCodNotaria, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContactoNotaria_sel"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            odtbListadoValorGenerica = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoNotarias", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoValorGenerica)
    End Function

    Public Function ListadoValorGenericaAnidada(ByVal pstrTablaGenerica As String, ByVal pstrCodigo As String) As String Implements IValorGenericaNTx.ListadoValorGenericaAnidada
        Dim odtbListadoValorGenericaAnidada As DataTable = Nothing
        Dim prmParameter(1) As DAABRequest.Parameter


        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@id_tabla", DbType.String, 18, pstrTablaGenerica, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@valor2", DbType.String, 18, pstrCodigo, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ValorTablaGenericoAnidada_sel"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            odtbListadoValorGenericaAnidada = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoValorGenericaAnidada", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoValorGenericaAnidada)
    End Function


    '10/01/2013 IBK - RPR Para listar valores genericos con logica de LPC 
    Public Function ListadoValorGenericaEspecial(ByVal pEValorGenerica As String, ByVal type As Integer) As String Implements IValorGenericaNTx.ListadoValorGenericaEspecial

        'Deserealiza la Entidad
        Dim oEValorGenerica As New EValorgenerica
        oEValorGenerica = CFunciones.DeserializeObject(Of EValorgenerica)(pEValorGenerica)

        Dim odtbListadoValorGenerica As DataTable = Nothing
        Dim prmParameter(22) As DAABRequest.Parameter

        prmParameter(0) = New DAABRequest.Parameter("@type", DbType.Int32, 0, type, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@id_tabla", DbType.String, 18, oEValorGenerica.Id_tabla, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@id_registro", DbType.Int32, 0, oEValorGenerica.Id_registro, ParameterDirection.Input)

        prmParameter(3) = New DAABRequest.Parameter("@clave1", DbType.String, 100, oEValorGenerica.Clave1, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@clave2", DbType.String, 100, oEValorGenerica.Clave2, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@clave3", DbType.String, 100, oEValorGenerica.Clave3, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@clave4", DbType.String, 100, oEValorGenerica.Clave4, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@clave5", DbType.String, 100, oEValorGenerica.Clave5, ParameterDirection.Input)

        prmParameter(8) = New DAABRequest.Parameter("@valor1", DbType.String, 100, oEValorGenerica.Valor1, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@valor2", DbType.String, 100, oEValorGenerica.Valor2, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@valor3", DbType.String, 100, oEValorGenerica.Valor3, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@valor4", DbType.String, 100, oEValorGenerica.Valor4, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@valor5", DbType.String, 100, oEValorGenerica.Valor5, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@valor6", DbType.String, 100, oEValorGenerica.Valor6, ParameterDirection.Input)
        prmParameter(14) = New DAABRequest.Parameter("@valor7", DbType.String, 100, oEValorGenerica.Valor7, ParameterDirection.Input)
        prmParameter(15) = New DAABRequest.Parameter("@valor8", DbType.String, 100, oEValorGenerica.Valor8, ParameterDirection.Input)
        prmParameter(16) = New DAABRequest.Parameter("@valor9", DbType.String, 100, oEValorGenerica.Valor9, ParameterDirection.Input)
        prmParameter(17) = New DAABRequest.Parameter("@valor10", DbType.String, 100, oEValorGenerica.Valor10, ParameterDirection.Input)
        prmParameter(18) = New DAABRequest.Parameter("@valor11", DbType.String, 100, oEValorGenerica.Valor11, ParameterDirection.Input)
        prmParameter(19) = New DAABRequest.Parameter("@valor12", DbType.String, 100, oEValorGenerica.Valor12, ParameterDirection.Input)
        prmParameter(20) = New DAABRequest.Parameter("@valor13", DbType.String, 100, oEValorGenerica.Valor13, ParameterDirection.Input)
        prmParameter(21) = New DAABRequest.Parameter("@valor14", DbType.String, 100, oEValorGenerica.Valor14, ParameterDirection.Input)
        prmParameter(22) = New DAABRequest.Parameter("@valor15", DbType.String, 100, oEValorGenerica.Valor15, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_ValorGenerica_SEL"
            objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            odtbListadoValorGenerica = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoTablaGenericaEspecial", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoValorGenerica)

    End Function
    'Fin RPR

#End Region

End Class

#End Region
