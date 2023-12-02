Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DContratoProveedorTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("1A97650C-3398-4119-96E0-4B52BC1B26AF") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DContratoProveedorTx")> _
Public Class DContratoProveedorTx
    Inherits ServicedComponent
    Implements IContratoProveedorTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DContratoProveedorTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta un nuevo registro en la tabla T_TEMPORAL
    ''' </summary>
    ''' <param name="pEContratoProveedor">Entidad ContratoProveedor serializada</param>
    ''' <returns>String con el número de ContratoProveedor</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/05/2012
    ''' </remarks>
    Public Function InsertarContratoProveedor(ByVal pEContratoProveedor As String) As Integer Implements IContratoProveedorTx.InsertarContratoProveedor
        Dim parSalida1 As IDataParameter
        Dim oEContratoProveedor As New EGcc_contratoproveedor

        Dim prmParameter(13) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEContratoProveedor = CFunciones.DeserializeObject(Of EGcc_contratoproveedor)(pEContratoProveedor)

        'Campos TEMPORAL 

        prmParameter(0) = New DAABRequest.Parameter("@pii_CodigoContratoProveedor", DbType.Int32, 0, oEContratoProveedor.Codigocontratoproveedor, ParameterDirection.InputOutput)
        prmParameter(1) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEContratoProveedor.Numerocontrato, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodigoMoneda", DbType.String, 3, oEContratoProveedor.Codigomoneda, ParameterDirection.Input)


        prmParameter(3) = New DAABRequest.Parameter("@pin_Importe", DbType.Decimal)
        prmParameter(3).Precision = 18
        prmParameter(3).Scale = 6
        prmParameter(3).Value = oEContratoProveedor.Importe

        prmParameter(4) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 4, oEContratoProveedor.Codproveedor, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pii_CodContacto", DbType.Int32, 0, oEContratoProveedor.Codigocontacto, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_TipoCambio", DbType.String, 1, oEContratoProveedor.Tipocambio, ParameterDirection.Input)

        prmParameter(7) = New DAABRequest.Parameter("@pin_MontoTipoCambio", DbType.Decimal)
        prmParameter(7).Precision = 18
        prmParameter(7).Scale = 6
        prmParameter(7).Value = oEContratoProveedor.MontoTipoCambio

        prmParameter(8) = New DAABRequest.Parameter("@pin_TotalImporte", DbType.Decimal)
        prmParameter(8).Precision = 18
        prmParameter(8).Scale = 6
        prmParameter(8).Value = oEContratoProveedor.TotalImporte

        prmParameter(9) = New DAABRequest.Parameter("@piv_AudUsuarioRegistro", DbType.String, 12, oEContratoProveedor.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_NombreContacto", DbType.String, 200, oEContratoProveedor.NombreContacto, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_Correo", DbType.String, 120, oEContratoProveedor.Correo, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_CodigoTipoProveedor", DbType.String, 100, oEContratoProveedor.CodigoTipoProveedor, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_DescripcionBien", DbType.String, 500, oEContratoProveedor.DescripcionBien, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoProveedor_ins"
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
    ''' Modifica un registro existente de la tabla ContratoProveedor
    ''' </summary>
    ''' <param name="pEGcc_contratoProveedor">Entidad Temporal serializada</param>
    ''' <returns>True si se grabo correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ModificaContratoProveedor(ByVal pEGcc_contratoProveedor As String) As Integer Implements IContratoProveedorTx.ModificaContratoProveedor

        Dim oEContratoProveedor As New EGcc_contratoproveedor

        Dim prmParameter(13) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEContratoProveedor = CFunciones.DeserializeObject(Of EGcc_contratoproveedor)(pEGcc_contratoProveedor)

        'Campos TEMPORAL 

        prmParameter(0) = New DAABRequest.Parameter("@pii_CodigoContratoProveedor", DbType.Int32, 0, oEContratoProveedor.Codigocontratoproveedor, ParameterDirection.InputOutput)
        prmParameter(1) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEContratoProveedor.Numerocontrato, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodigoMoneda", DbType.String, 3, oEContratoProveedor.Codigomoneda, ParameterDirection.Input)


        prmParameter(3) = New DAABRequest.Parameter("@pin_Importe", DbType.Decimal)
        prmParameter(3).Precision = 18
        prmParameter(3).Scale = 6
        prmParameter(3).Value = oEContratoProveedor.Importe

        prmParameter(4) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 4, oEContratoProveedor.Codproveedor, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pii_CodContacto", DbType.Int32, 0, oEContratoProveedor.Codigocontacto, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_TipoCambio", DbType.String, 1, oEContratoProveedor.Tipocambio, ParameterDirection.Input)

        prmParameter(7) = New DAABRequest.Parameter("@pin_MontoTipoCambio", DbType.Decimal)
        prmParameter(7).Precision = 18
        prmParameter(7).Scale = 6
        prmParameter(7).Value = oEContratoProveedor.MontoTipoCambio

        prmParameter(8) = New DAABRequest.Parameter("@pin_TotalImporte", DbType.Decimal)
        prmParameter(8).Precision = 18
        prmParameter(8).Scale = 6
        prmParameter(8).Value = oEContratoProveedor.TotalImporte

        prmParameter(9) = New DAABRequest.Parameter("@piv_AudUsuarioModificacion", DbType.String, 12, oEContratoProveedor.Audusuarioregistro, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_NombreContacto", DbType.String, 200, oEContratoProveedor.NombreContacto, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_Correo", DbType.String, 120, oEContratoProveedor.Correo, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_CodigoTipoProveedor", DbType.String, 100, oEContratoProveedor.CodigoTipoProveedor, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_DescripcionBien", DbType.String, 500, oEContratoProveedor.DescripcionBien, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_ContratoProveedor_upd"
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
    ''' Eliminar un registro existente de la tabla ContratoProveedor
    ''' </summary>
    ''' <param name="pEContratoProveedor">Entidad Temporal serializada</param>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function EliminarContratoProveedor(ByVal pEContratoProveedor As String) As Boolean Implements IContratoProveedorTx.EliminarContratoProveedor
        Dim oEContratoProveedor As New EGcc_contratoproveedor


        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEContratoProveedor = CFunciones.DeserializeObject(Of EGcc_contratoproveedor)(pEContratoProveedor)

        'Campos T_CLIENTE	
        prmParameter(0) = New DAABRequest.Parameter("@pii_CodigoContratoProveedor", DbType.Int16, 0, oEContratoProveedor.Codigocontratoproveedor, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_AudUsuarioModificacion", DbType.String, 12, oEContratoProveedor.Audusuariomodificacion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_ContratoProveedor_del"
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
            blnRetorno = False
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function
    Public Function EnviarCartaDocumentoProveedor(ByVal pEGcc_contratoProveedor As String) As Boolean Implements IContratoProveedorTx.EnviarCartaDocumentoProveedor

        Dim oEContratoProv As New EGcc_contratoproveedor
        Dim prmParameter(2) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEContratoProv = CFunciones.DeserializeObject(Of EGcc_contratoproveedor)(pEGcc_contratoProveedor)

        'Campos        
        prmParameter(0) = New DAABRequest.Parameter("@piv_CODCONTRATO", DbType.String, 8, oEContratoProv.Numerocontrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CODCONTRATODOC", DbType.Int32, 0, oEContratoProv.Codigocontratoproveedor, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_USUARIOREGISTRO", DbType.String, 12, oEContratoProv.Audusuarioregistro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_ContratoDocumentoCartaProv_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "EnviarCartaDocumentoCliente", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
''' Implementación de la clase DContratoProveedorNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 17/05/2012
''' </remarks>
<Guid("0D3294FB-B129-4bb5-AB26-0ECA324F68B6") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DContratoProveedorNTx")> _
Public Class DContratoProveedorNTx
    Inherits ServicedComponent
    Implements IContratoProveedorNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DContratoProveedorNTx"
#End Region

#Region "Métodos"

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    Public Function ListadoContratoProveedor(ByVal pNumeroContrato As String) As String Implements IContratoProveedorNTx.ListadoContratoProveedor
        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pic_NumeroContrato", DbType.String, 8, pNumeroContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoProveedor_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoProveedor", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function

    ''' <summary>
    ''' Devuelve los proveedores del contrato, organizado por paginas de resultados.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function Listado(ByVal pPageSize As Integer, _
                            ByVal pCurrentPage As Integer, _
                            ByVal pSortColumn As String, _
                            ByVal pSortOrder As String, _
                            ByVal pContrato As String) As String Implements IContratoProveedorNTx.Listado
        Dim odtbContratoProveedor As DataTable
        Dim prmParameter(4) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8, pContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoProveedorPaginado_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContratoProveedor = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "Listado", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbContratoProveedor)
    End Function

    ''' <summary>
    ''' Devuelve los proveedores del contrato (sin repetir), organizado por paginas de resultados.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListarDistintos(ByVal pPageSize As Integer, _
                                    ByVal pCurrentPage As Integer, _
                                    ByVal pSortColumn As String, _
                                    ByVal pSortOrder As String, _
                                    ByVal pContrato As String) As String Implements IContratoProveedorNTx.ListarDistintos
        Dim odtbContratoProveedor As DataTable
        Dim prmParameter(4) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_CodigoContrato", DbType.String, 8, pContrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoProveedorDistinct_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContratoProveedor = objRequest.Factory.ExecuteDataset(objRequest).Tables(1)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListarDistintos", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbContratoProveedor)
    End Function

    ''' <summary>
    ''' valida que el registro ingresado no se duplique.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 22/05/2012
    ''' </remarks>
    Public Function ValidarContratoProveedor(ByVal pEContratoProveedor As String) As String Implements IContratoProveedorNTx.ValidarContratoProveedor
        Dim odtbContratoProveedor As DataTable
        Dim oEContratoProveedor As New EGcc_contratoproveedor

        'Deserealiza la Entidad
        oEContratoProveedor = CFunciones.DeserializeObject(Of EGcc_contratoproveedor)(pEContratoProveedor)
        Dim prmParameter(3) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pii_CodigoContratoProveedor", DbType.Int16, 0, oEContratoProveedor.Codigocontratoproveedor, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 4, oEContratoProveedor.Codproveedor, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@pii_CodigoContacto", DbType.Int16, 0, oEContratoProveedor.Codigocontacto, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_NumeroContrato", DbType.String, 8, oEContratoProveedor.Numerocontrato, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ContratoProveedor_val"
            objRequest.Parameters.AddRange(prmParameter)

            odtbContratoProveedor = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ValidarContratoProveedor", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbContratoProveedor)
    End Function

#End Region

End Class

#End Region
