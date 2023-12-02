Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DTemporalTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 12/12/2011 11:40:51 a.m.
''' </remarks>
<Guid("FD36B6A7-1B13-4d63-BD3A-EA13AF2F4F76") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DTemporalTx")> _
Public Class DTemporalTx
    Inherits ServicedComponent
    Implements ITemporalTx

#Region "Constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DTemporalTx"

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Inserta un nuevo registro en la tabla T_TEMPORAL
    ''' </summary>
    ''' <param name="pETemporal">Entidad Temporal serializada</param>
    ''' <returns>String con el número de Temporal</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function InsertarTemporal(ByVal pETemporal As String) As Integer Implements ITemporalTx.InsertarTemporal
        Dim parSalida1 As IDataParameter
        Dim oETemporal As New ETemporal

        Dim prmParameter(6) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oETemporal = CFunciones.DeserializeObject(Of ETemporal)(pETemporal)

        'Campos TEMPORAL 
        prmParameter(0) = New DAABRequest.Parameter("@poi_Codigo", DbType.Int16, 0, oETemporal.Codigo, ParameterDirection.InputOutput)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Texto", DbType.String, 50, oETemporal.Texto, ParameterDirection.Input)
        ' prmParameter(2) = New DAABRequest.Parameter("@piv_Fecha", DbType.Date, 0, oETemporal.Fecha, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pic_Fecha", DbType.String, 8, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oETemporal.Fecha), ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Numero", DbType.Int16, 0, oETemporal.Numero, ParameterDirection.Input)

        prmParameter(4) = New DAABRequest.Parameter("@pin_Decimales", DbType.Decimal)
        prmParameter(4).Precision = 14
        prmParameter(4).Scale = 2
        prmParameter(4).Value = oETemporal.Decimales

        prmParameter(5) = New DAABRequest.Parameter("@piv_Flag", DbType.String, 1, oETemporal.Flag, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Comentario ", DbType.String, 500, oETemporal.Comentario, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "usp_ins_Temporal"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fstrTemporal", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            parSalida1 = CType(obRequest.Parameters(0), IDataParameter)
            obRequest.Factory.Dispose()
        End Try

        Return CFunciones.CheckInt(parSalida1.Value.ToString())

    End Function

    ''' <summary>
    ''' Modifica un registro existente de la tabla T_CLIENTE
    ''' </summary>
    ''' <param name="pETemporal">Entidad Temporal serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ModificarTemporal(ByVal pETemporal As String) As Boolean Implements ITemporalTx.ModificarTemporal
        Dim oETemporal As New ETemporal

        Dim prmParameter(6) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oETemporal = CFunciones.DeserializeObject(Of ETemporal)(pETemporal)

        'Campos TEMPORAL 
        prmParameter(0) = New DAABRequest.Parameter("@poi_Codigo", DbType.Int16, 0, oETemporal.Codigo, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_Texto", DbType.String, 50, oETemporal.Texto, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pid_Fecha", DbType.Date, 0, oETemporal.Fecha, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_Numero", DbType.Int16, 0, oETemporal.Numero, ParameterDirection.Input)

        prmParameter(4) = New DAABRequest.Parameter("@pin_Decimales", DbType.Decimal)
        prmParameter(4).Precision = 14
        prmParameter(4).Scale = 2
        prmParameter(4).Value = oETemporal.Decimales

        prmParameter(5) = New DAABRequest.Parameter("@piv_Flag", DbType.String, 1, oETemporal.Flag, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_Comentario ", DbType.String, 500, oETemporal.Comentario, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "usp_upd_Temporal"
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

    ''' <summary>
    ''' Eliminar un registro existente de la tabla T_CLIENTE
    ''' </summary>
    ''' <param name="pETemporal">Entidad Temporal serializada</param>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function EliminarTemporal(ByVal pETemporal As String) As Boolean Implements ITemporalTx.EliminarTemporal
        Dim oETemporal As New ETemporal


        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oETemporal = CFunciones.DeserializeObject(Of ETemporal)(pETemporal)

        'Campos T_CLIENTE	
        prmParameter(0) = New DAABRequest.Parameter("@pii_Codigo", DbType.Int16, 0, oETemporal.Codigo, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "usp_del_Temporal"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)
            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnEliminarT_CLIENTE", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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

#Region "Clase NO Transaccional"

''' <summary>
''' Implementación de la clase DTemporalNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("D63D4495-E2E6-4951-9029-8268DF8886F4") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DTemporalNTx")> _
Public Class DTemporalNTx
    Inherits ServicedComponent
    Implements ITemporalNTx

#Region "constantes"

    Private Const C_NOMBRE_CLASE As String = "DTemporalNTx"

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene los valores de un registro de la tabla T_CLIENTE
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function LeerTemporal(ByVal pETemporal As String) As String Implements ITemporalNTx.fobjLeerTemporal
        Dim oETemporal As ETemporal = Nothing

        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oETemporal = CFunciones.DeserializeObject(Of ETemporal)(pETemporal)
        prmParameter(0) = New DAABRequest.Parameter("@pii_Codigo", DbType.Int32, 0, oETemporal.Codigo, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "usp_sel_Temporal"
            objRequest.Parameters.AddRange(prmParameter)

            Using oReader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                If oReader.Read Then
                    oETemporal = New ETemporal
                    With oETemporal
                        .Codigo = CFunciones.CheckInt(oReader("codigo"))
                        .Texto = CFunciones.CheckStr(oReader("texto"))

                        If oReader("fecha") Is System.DBNull.Value Then
                            .Fecha = Nothing
                        Else
                            .Fecha = CFunciones.CheckDate(oReader("fecha"))
                        End If

                        .Numero = CFunciones.CheckInt(oReader("numero"))
                        .Decimales = CFunciones.CheckDecimal(oReader("decimales"))
                        .Comentario = CFunciones.CheckStr(oReader("comentario"))
                        .Flag = CFunciones.CheckStr(oReader("flag"))
                    End With
                End If
            End Using

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjLeerTemporal", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of ETemporal)(oETemporal)
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListadoTemporal(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pCodigo As String, _
                                    ByVal pFecha As String, _
                                    ByVal pNumero As String, _
                                    ByVal pDecimales As String, _
                                    ByVal pComentario As String, _
                                    ByVal pTexto As String, _
                                    ByVal pFlag As String) As String Implements ITemporalNTx.ListadoTemporal
        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(10) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pii_Codigo", DbType.String, 8000, pCodigo, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pii_Fecha", DbType.String, 8000, pFecha, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pii_Numero", DbType.String, 8000, pNumero, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@pii_Decimales", DbType.String, 8000, pDecimales, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@pii_Comentario", DbType.String, 8000, pComentario, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@pii_Texto", DbType.String, 8000, pTexto, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@pii_Flag", DbType.String, 8000, pFlag, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "usp_sel_Temporal"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjListadoTemporal", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function
    'IBK - RPH
    Public Function ListarSeguros(ByVal pPageSize As Integer, _
                                  ByVal pCurrentPage As Integer, _
                                  ByVal pSortColumn As String, _
                                  ByVal pSortOrder As String, _
                                  ByVal pESeguros As String) As String Implements ITemporalNTx.ListarSeguros

        Dim odtbListadoSeguros As DataTable
        Dim oEGcc_Seguros As New ESeguro
        Dim prmParameter(11) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        oEGcc_Seguros = CFunciones.DeserializeObject(Of ESeguro)(pESeguros)

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_Codigocontrato", DbType.String, 8, oEGcc_Seguros.NroContraro, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@piv_Nropoliza", DbType.String, 8, oEGcc_Seguros.NroPoliza, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@piv_Ciaseguro", DbType.String, 40, oEGcc_Seguros.CiaSeguro, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@piv_Codigotipovalor", DbType.String, 8, oEGcc_Seguros.CodigoTipoValor, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@pid_FechaInicio", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEGcc_Seguros.FechaInicio), ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@pid_FechaFin", DbType.String, 20, CHelperDateTime.FormatDateTimeAsYYYYMMDD(oEGcc_Seguros.FechaFin), ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@piv_Tipobien", DbType.String, 40, oEGcc_Seguros.TipoBien, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@piv_Codigotiposeguro", DbType.String, 4, oEGcc_Seguros.CodigoTipoSeguro, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_RepSeguros_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoSeguros = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjListarSeguros", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoSeguros)
    End Function
    Public Function ListarSegurosDetalle(ByVal pPageSize As Integer, _
                                         ByVal pCurrentPage As Integer, _
                                         ByVal pSortColumn As String, _
                                         ByVal pSortOrder As String, _
                                         ByVal pCodigoContrato As String) As String Implements ITemporalNTx.ListarSegurosDetalle

        Dim odtbListadoSeguros As DataTable
        'Dim oEGcc_Seguros As New ESeguro
        Dim prmParameter(4) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        'Deserealiza la Entidad
        'oEGcc_Seguros = CFunciones.DeserializeObject(Of ESeguro)(pESeguros)

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)

            prmParameter(4) = New DAABRequest.Parameter("@piv_Codigocontrato", DbType.String, 8, pCodigoContrato, ParameterDirection.Input)
            
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_RepSegurosDetalle_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoSeguros = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fobjListarSeguros", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoSeguros)
    End Function
#End Region

End Class

#End Region
