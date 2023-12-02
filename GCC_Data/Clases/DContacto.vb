Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Imports TSF.DAAB
Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DContactoTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/05/2012
''' </remarks>
<Guid("E3AAC655-16D8-49bd-9FE5-6246EBA9E62A") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DContactoTx")> _
Public Class DContactoTx
    Inherits ServicedComponent
    Implements IContactoTx

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DContactoTx"
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Inserta el contacto para una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEContacto">Entidad Serializado de Contacto formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012
    ''' </remarks>
    Public Function InsertarContacto(ByVal pEContacto As String) As Boolean Implements IContactoTx.InsertarContacto

        Dim oEContacto As New EGcc_contacto
        Dim prmParameter(6) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEContacto = CFunciones.DeserializeObject(Of EGcc_contacto)(pEContacto)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@piv_CODIGOCOTIZACION", DbType.String, 8, oEContacto.Codigocotizacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CODSOLICITUDCREDITO", DbType.String, 8, oEContacto.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_NOMBRE", DbType.String, 200, oEContacto.Nombre, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_CORREO", DbType.String, 120, oEContacto.Correo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_TELEFONO", DbType.String, 15, oEContacto.Telefono, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_ANEXO", DbType.String, 15, oEContacto.Anexo, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_CODIGOCARGO", DbType.String, 15, oEContacto.CodigoCargo, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Contacto_Ins"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnInsertarContacto", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            obRequest.Factory.Dispose()
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Modificar el contacto de una cotizacion y contrato especifico
    ''' </summary>
    ''' <param name="pEContacto">Entidad Serializada de Contacto formato string</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012
    ''' </remarks>
    Public Function ModificarContacto(ByVal pEContacto As String) As Boolean Implements IContactoTx.ModificarContacto

        Dim oEContacto As New EGcc_contacto
        Dim prmParameter(7) As DAABRequest.Parameter

        'Deserealiza la Entidad
        oEContacto = CFunciones.DeserializeObject(Of EGcc_contacto)(pEContacto)

        'Campos   
        prmParameter(0) = New DAABRequest.Parameter("@pin_CODIGOCONTACTO", DbType.Int32, 4, oEContacto.Codigocontacto, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CODIGOCOTIZACION", DbType.String, 8, oEContacto.Codigocotizacion, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_CODSOLICITUDCREDITO", DbType.String, 8, oEContacto.Codsolicitudcredito, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_NOMBRE", DbType.String, 200, oEContacto.Nombre, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_CORREO", DbType.String, 120, oEContacto.Correo, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_TELEFONO", DbType.String, 15, oEContacto.Telefono, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@piv_ANEXO", DbType.String, 15, oEContacto.Anexo, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@piv_CODIGOCARGO", DbType.String, 15, oEContacto.CodigoCargo, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(C_NOMBRE_APLICATIVO, 1)
        Dim obRequest As DAABRequest = obj.CreaRequestSQL()

        obRequest.CommandType = CommandType.StoredProcedure
        obRequest.Command = "up_gcc_Contacto_Upd"
        obRequest.Parameters.AddRange(prmParameter)

        Try
            obRequest.Factory.ExecuteNonQuery(obRequest)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ModificarContacto", GCC.Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
''' Implementación de la clase DContactoNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Guid("B3E2ECDF-1C29-4190-9190-664794238F2B") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DContactoNTx")> _
Public Class DContactoNTx
    Inherits ServicedComponent
    Implements IContactoNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DContactoNTx"
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Obtiene el Contacto de una cotizacion y Contrato especifico
    ''' </summary>
    ''' <param name="pstrCodCotizacion">Numero de Cotizacion</param>
    ''' <param name="pstrCodContrato">Numero de Contrato</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 15/05/2012 
    ''' </remarks>
    Public Function ObtenerContacto(ByVal pstrCodCotizacion As String, ByVal pstrCodContrato As String) As String Implements IContactoNTx.ObtenerContacto

        Dim oEContacto As EGcc_contacto = Nothing
        Dim prmParameter(1) As DAABRequest.Parameter

        'Deserealiza la Entidad
        prmParameter(0) = New DAABRequest.Parameter("@piv_CODIGOCOTIZACION", DbType.String, 8, pstrCodCotizacion, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CODCONTRATO", DbType.String, 8, pstrCodContrato, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Contacto_get"
            objRequest.Parameters.AddRange(prmParameter)

            Using Reader As IDataReader = objRequest.Factory.ExecuteReader(objRequest).ReturnDataReader
                While Reader.Read
                    oEContacto = New EGcc_contacto
                    With oEContacto
                        .Codigocontacto = Reader("CODIGOCONTACTO")
                        .Nombre = Reader("NOMBRE")
                        .Correo = Reader("CORREO")
                        .Telefono = Reader("TELEFONO")
                        .Anexo = Reader("ANEXO")
                        .CodigoCargo = Reader("codigoCargo")
                    End With
                End While
            End Using
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerContacto", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        If oEContacto Is Nothing Then
            Return ""
        Else
            Return CFunciones.SerializeObject(Of EGcc_contacto)(oEContacto)
        End If
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - WCR
    ''' Fecha de Creación  : 17/05/2012
    ''' </remarks>
    Public Function ListadoContacto(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pCodProveedor As String) As String Implements IContactoNTx.ListadoContacto
        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(4) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@piv_CodProveedor", DbType.String, 4, pCodProveedor, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Contacto_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoContacto", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function
#End Region

End Class

#End Region
