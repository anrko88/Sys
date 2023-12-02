Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports TSF.DAAB

Imports GCC.Common
Imports GCC.Entity
#Region "Clase Transaccional"
''' <summary>
''' Implementación de la clase DTipoCambioTx
''' </summary>
''' <remarks>
''' Creado Por         : JJM - IBK
''' Fecha de Creación  : 22/01/2013
''' </remarks>
<Guid("5bc67b30-9cec-4926-8308-112fc394cfd7") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DTipoCambioTx")> _
Public Class DTipoCambioTx
    Inherits ServicedComponent
    Implements ITipoCambioTx
#Region "Constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DTipoCambioTx"

#End Region
#Region "Métodos"
    Public Function InsertaTipoCambio(ByVal pETipoCambio As String) As Boolean Implements ITipoCambioTx.InsertaTipoCambio
        'Variables

        Dim oETipoCambio As New EMonedatipocambio
        Dim prmParameter(5) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False
        'Deserealiza la Entidad
        oETipoCambio = CFunciones.DeserializeObject(Of EMonedatipocambio)(pETipoCambio)


        prmParameter(0) = New DAABRequest.Parameter("@CodMoneda", DbType.String, 3, oETipoCambio.Codmoneda, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@Modalidad", DbType.String, 3, oETipoCambio.Tipomodalidadcambio, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@FechaInicio", DbType.String, 10, oETipoCambio.Fechainiciovigencia, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@FechaFinal", DbType.String, 10, oETipoCambio.Fechafinalvigencia, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@MontoCompra", DbType.Decimal)
        prmParameter(4).Scale = 5
        prmParameter(4).Precision = 15
        prmParameter(4).Value = oETipoCambio.Montovalorcompra
        prmParameter(5) = New DAABRequest.Parameter("@MontoVenta", DbType.Decimal)
        prmParameter(5).Scale = 5
        prmParameter(5).Precision = 15
        prmParameter(5).Value = oETipoCambio.Montovalorventa

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_INS_MonedaTipoCambio"
        objRequest.Parameters.AddRange(prmParameter)

        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarTemporal", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function
    Public Function ActualizaTipoCambio(ByVal pETipoCambio As String) As Boolean Implements ITipoCambioTx.ActualizaTipoCambio
        'Variables

        Dim oETipoCambio As New EMonedatipocambio
        Dim prmParameter(5) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False
        'Deserealiza la Entidad
        oETipoCambio = CFunciones.DeserializeObject(Of EMonedatipocambio)(pETipoCambio)


        prmParameter(0) = New DAABRequest.Parameter("@CodMoneda", DbType.String, 3, oETipoCambio.Codmoneda, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@TipoModalidadCambio", DbType.String, 3, oETipoCambio.Tipomodalidadcambio, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@FechaInicioVigencia", DbType.String, 10, oETipoCambio.Fechainiciovigencia, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@FechaFinalVigencia", DbType.String, 10, oETipoCambio.Fechafinalvigencia, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@MontoCompra", DbType.Decimal)
        prmParameter(4).Scale = 5
        prmParameter(4).Precision = 15
        prmParameter(4).Value = oETipoCambio.Montovalorcompra
        prmParameter(5) = New DAABRequest.Parameter("@MontoVenta", DbType.Decimal)
        prmParameter(5).Scale = 5
        prmParameter(5).Precision = 15
        prmParameter(5).Value = oETipoCambio.Montovalorventa

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_FC_UPD_TipoCambio"
        objRequest.Parameters.AddRange(prmParameter)

        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarTemporal", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function
    Public Function EliminarTipoCambio(ByVal pETipoCambio As String) As Boolean Implements ITipoCambioTx.EliminaTipoCambio
        'Variables

        Dim oETipoCambio As New EMonedatipocambio
        Dim prmParameter(3) As DAABRequest.Parameter

        'Prepara Consulta
        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False
        'Deserealiza la Entidad
        oETipoCambio = CFunciones.DeserializeObject(Of EMonedatipocambio)(pETipoCambio)


        prmParameter(0) = New DAABRequest.Parameter("@Moneda", DbType.String, 3, oETipoCambio.Codmoneda, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@FechaIni", DbType.String, 10, oETipoCambio.Fechainiciovigencia, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@FechaFin", DbType.String, 10, oETipoCambio.Fechafinalvigencia, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@Modalidad", DbType.String, 3, oETipoCambio.Tipomodalidadcambio, ParameterDirection.Input)

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_DEL_MonedaTipoCambio"
        objRequest.Parameters.AddRange(prmParameter)

        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnModificarTemporal", GCC.Entity.EConstante.C_SUCESO_ERROR, GCC.Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function
#End Region
End Class
#End Region
#Region "Clase No Transaccional"
''' <summary>
''' Implementación de la clase DTipoCambioNTx
''' </summary>
''' <remarks>
''' Creado Por         : JJM - IBK
''' Fecha de Creación  : 22/01/2013
''' </remarks>
<Guid("21f8b08e-fa4a-4027-aa4d-5d8ad1466637") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DTipoCambioNTx")> _
Public Class DTipoCambioNTx
    Inherits ServicedComponent
    Implements ITipoCambioNTx

#Region "Constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DTipoCambioNTx"

#End Region
#Region "Métodos"
    Public Function ListaTipoCambio(ByVal pPageSize As Integer, _
                                  ByVal pCurrentPage As Integer, _
                                  ByVal pSortColumn As String, _
                                  ByVal pSortOrder As String, _
                                  ByVal pETipoCambio As String) As String Implements ITipoCambioNTx.ListaTipoCambio
        Dim odtbListadoTipoCambio As DataTable
        Dim oETipoCambio As New EMonedatipocambio

        Dim prmParameter(7) As DAABRequest.Parameter


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            ' Deserealiza la Entidad
            oETipoCambio = CFunciones.DeserializeObject(Of EMonedatipocambio)(pETipoCambio)

            ' Campos para la elaboración de la consulta
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 4, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 4, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@TipoModalidadCambio", DbType.String, 10, oETipoCambio.Tipomodalidadcambio, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@FechaInicioVigencia", DbType.String, 10, oETipoCambio.Fechainiciovigencia, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@FechaFinalVigencia", DbType.String, 10, oETipoCambio.Fechafinalvigencia, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@TipoOperacion", DbType.String, 1, oETipoCambio.Tipooperacion, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_SEL_MonedaTipoCambio"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoTipoCambio = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTipoCambio)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Tipo Cambio", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function
    Public Function ValidaTipoCambio(ByVal pETipoCambio As String) As String Implements ITipoCambioNTx.ValidaTipoCambio
        Dim odtbListadoTipoCambio As DataTable
        Dim oETipoCambio As New EMonedatipocambio

        Dim prmParameter(3) As DAABRequest.Parameter


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            ' Deserealiza la Entidad
            oETipoCambio = CFunciones.DeserializeObject(Of EMonedatipocambio)(pETipoCambio)

            ' Campos para la elaboración de la consulta           
            prmParameter(0) = New DAABRequest.Parameter("@Moneda", DbType.String, 3, oETipoCambio.Codmoneda, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@FechaIni", DbType.String, 10, oETipoCambio.Fechainiciovigencia, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@FechaFin", DbType.String, 10, oETipoCambio.Fechafinalvigencia, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@Modalidad", DbType.String, 3, oETipoCambio.Tipomodalidadcambio, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Sel_MonedaTipoCambio_get"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoTipoCambio = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTipoCambio)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Tipo Cambio", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
    End Function
#End Region
End Class
#End Region


