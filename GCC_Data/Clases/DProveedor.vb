Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DProveedorTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("E12B17DB-5E09-4bad-9472-22765070711A") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DProveedorTx")> _
Public Class DProveedorTx
    Inherits ServicedComponent
    Implements IProveedorTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DProveedorTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inserta un Proveedor
    ''' </summary>
    ''' <param name="pEProveedor">Entidad Serializado de Proveedor formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 04/06/2012
    ''' </remarks>
    Public Function InsertarProveedor(ByVal pEProveedor As String) As Boolean Implements IProveedorTx.InsertarProveedor

        Dim oEProveedor As New EProveedor
        Dim prmParameter(13) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEProveedor = CFunciones.DeserializeObject(Of EProveedor)(pEProveedor)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 4, oEProveedor.Codproveedor, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodProcedencia", DbType.String, 1, oEProveedor.CodProcedencia, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodProveedorTipo", DbType.String, 3, oEProveedor.Codigotipopersona, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodTipoDocumento", DbType.String, 100, oEProveedor.CodTipoDocumento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_NumeroDocumento", DbType.String, 20, oEProveedor.NumeroDocumento, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_RazonSocial", DbType.String, 50, oEProveedor.RazonSocial, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_TextoDomicilioLegal", DbType.String, 120, oEProveedor.TextoDomicilioLegal, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_CodPais", DbType.String, 3, oEProveedor.CodPais, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_CodDepartamento", DbType.String, 3, oEProveedor.CodDepartamento, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_CodProvincia", DbType.String, 3, oEProveedor.CodProvincia, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_CodDistrito", DbType.String, 3, oEProveedor.CodDistrito, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_CodUsuario", DbType.String, 12, oEProveedor.Codusuario, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 10, oEProveedor.CodUnico, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_XMLEntity", DbType.Xml)
        prmParameter(13).Value = oEProveedor.XMLEntity
        prmParameter(13).Direction = ParameterDirection.Input

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Proveedor_ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "InsertarProveedor", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Modificar el Proveedor
    ''' </summary>
    ''' <param name="pEProveedor">Entidad Serializada de Proveedor formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 04/06/2012
    ''' </remarks>
    Public Function ModificarProveedor(ByVal pEProveedor As String) As Boolean Implements IProveedorTx.ModificarProveedor
        Dim oEProveedor As New EProveedor
        Dim prmParameter(13) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEProveedor = CFunciones.DeserializeObject(Of EProveedor)(pEProveedor)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 4, oEProveedor.Codproveedor, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodProcedencia", DbType.String, 1, oEProveedor.CodProcedencia, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CodProveedorTipo", DbType.String, 3, oEProveedor.Codigotipopersona, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CodTipoDocumento", DbType.String, 100, oEProveedor.CodTipoDocumento, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_NumeroDocumento", DbType.String, 20, oEProveedor.NumeroDocumento, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_RazonSocial", DbType.String, 50, oEProveedor.RazonSocial, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_TextoDomicilioLegal", DbType.String, 120, oEProveedor.TextoDomicilioLegal, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_CodPais", DbType.String, 3, oEProveedor.CodPais, ParameterDirection.Input)
        prmParameter(8) = New DAABRequest.Parameter("@piv_CodDepartamento", DbType.String, 3, oEProveedor.CodDepartamento, ParameterDirection.Input)
        prmParameter(9) = New DAABRequest.Parameter("@piv_CodProvincia", DbType.String, 3, oEProveedor.CodProvincia, ParameterDirection.Input)
        prmParameter(10) = New DAABRequest.Parameter("@piv_CodDistrito", DbType.String, 3, oEProveedor.CodDistrito, ParameterDirection.Input)
        prmParameter(11) = New DAABRequest.Parameter("@piv_CodUsuario", DbType.String, 12, oEProveedor.Codusuario, ParameterDirection.Input)
        prmParameter(12) = New DAABRequest.Parameter("@piv_CodUnico", DbType.String, 10, oEProveedor.CodUnico, ParameterDirection.Input)
        prmParameter(13) = New DAABRequest.Parameter("@piv_XMLEntity", DbType.Xml)
        prmParameter(13).Value = oEProveedor.XMLEntity
        prmParameter(13).Direction = ParameterDirection.Input

        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Proveedor_upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarProveedor", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
''' Implementación de la clase DProveedorNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("19466438-297C-4313-A76B-58207BAFCD44") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DProveedorNTx")> _
Public Class DProveedorNTx
    Inherits ServicedComponent
    Implements IProveedorNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DProveedorNTx"
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene los datos del Proveedor
    ''' </summary>
    ''' <param name="pstrCodProveedor">Código de Proveedor</param>    
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - WCR
    ''' Fecha de Creacion : 05/06/2012 
    ''' </remarks>
    Public Function ObtenerProveedor(ByVal pstrCodProveedor As String) As String Implements IProveedorNTx.ObtenerProveedor

        Dim oEProveedor As EProveedor = Nothing
        Dim prmParameter(0) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 4, pstrCodProveedor, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Proveedor_get"
            objRequest.Parameters.AddRange(prmParameter)

            Using Reader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                While Reader.Read
                    oEProveedor = New EProveedor
                    With oEProveedor
                        .Codproveedor = Reader("CodProveedor").ToString()
                        .Codigotipopersona = Reader("CodigoTipoPersona").ToString()
                        .CodTipoDocumento = Reader("CodigoTipoDocumento").ToString()
                        .NumeroDocumento = Reader("NumeroDocumento").ToString()
                        .RazonSocial = Reader("NombreInstitucion").ToString()
                        .TextoDomicilioLegal = Reader("TextoDomicilioLegal").ToString()
                        .CodPais = Reader("CodPais").ToString()
                        .CodDepartamento = Reader("CodDptoEstado").ToString()
                        .CodProvincia = Reader("CodProvinciaCiudad").ToString()
                        .CodDistrito = Reader("CodDistrito").ToString()
                        .CodProcedencia = Reader("TipoNacionalidad").ToString()
                        .CodUnico = Reader("CodUnico").ToString()
                    End With
                End While
            End Using
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerProveedor", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If oEProveedor Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of EProveedor)(oEProveedor)
        End If
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    Public Function ListadoProveedor(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pCodigoTipoDocumento As String, _
                                     ByVal pNumeroDocumento As String, _
                                     ByVal pRazonSocial As String) As String Implements IProveedorNTx.ListadoProveedor
        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(6) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_CodigoTipoDocumento", DbType.String, 8000, pCodigoTipoDocumento, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_NumeroDocumento", DbType.String, 8000, pNumeroDocumento, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pic_NombreRazonSocial", DbType.String, 8000, pRazonSocial, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Proveedor_sel"
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
    ''' Lista las cuentas del proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 05/06/2012
    ''' </remarks>
    Public Function ListadoProveedorCuenta(ByVal pCodProveedor As String) As String Implements IProveedorNTx.ListadoProveedorCuenta
        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(0) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 4, pCodProveedor, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ProveedorCuenta_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoProveedorCuenta", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function

#End Region

End Class

#End Region
