Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

Imports Microsoft.VisualBasic

Imports TSF.DAAB

Imports GCC.Common
Imports GCC.Entity

#Region "Clase Transaccional"

''' <summary>
''' Implementación de la clase DUtilTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 04/09/2012
''' </remarks>
<Guid("96CC3541-BB73-44b1-B043-61253A839932") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Required, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DUtilTx")> _
Public Class DUtilTx
    Inherits ServicedComponent
    Implements IUtilTx

#Region "Constantes"

    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DUtilTx"

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <param name="pstrCodigoContrato">Código del Contrato</param>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 05/09/2012
    ''' </remarks>
    Public Function fblnGestionFlujo(ByVal pstrCodigoContrato As String, _
                                 ByVal pstrCodigoModulo As String, _
                                 ByVal pstrUsuarioRegistro As String) As Boolean Implements IUtilTx.fblnGestionFlujo

        Dim prmParameter(2) As DAABRequest.Parameter
        ' Campos clave del objeto para poder eliminarlos
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSolicitudCredito", DbType.String, 8, pstrCodigoContrato, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_CodigoModulo", DbType.String, 3, pstrCodigoModulo, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_UsuarioRegistro", DbType.String, 12, pstrUsuarioRegistro, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "up_gcc_FlujoControl_gst"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnGestionFlujo", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function



    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 21/03/2013
    ''' </remarks>
    Public Function InsertarContactoSuprestatario(ByVal pstrCodigoSuprestatario As String, _
                                 ByVal pstrNombre As String, _
                                 ByVal pstrCorreo As String, _
                                 ByVal pstrTelefono As String) As Boolean Implements IUtilTx.InsertarContactoSuprestatario

        Dim prmParameter(3) As DAABRequest.Parameter
        ' Campos clave del objeto para poder eliminarlos
        prmParameter(0) = New DAABRequest.Parameter("@piv_codsuprestatario", DbType.String, 20, pstrCodigoSuprestatario, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_nombre", DbType.String, 100, pstrNombre, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_correo", DbType.String, 100, pstrCorreo, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_telefono", DbType.String, 20, pstrTelefono, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "UP_GCC_CONTACTOSUPRESTATARIO_INS"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnGestionFlujo", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 21/03/2013
    ''' </remarks>
    Public Function ModificarContactoSuprestatario(ByVal pstrCodSuprestatarioContacto As Integer, ByVal pstrCodigoSuprestatario As String, _
                                 ByVal pstrNombre As String, _
                                 ByVal pstrCorreo As String, _
                                 ByVal pstrTelefono As String, _
                                 ByVal pstrEstado As String) As Boolean Implements IUtilTx.ModificarContactoSuprestatario

        Dim prmParameter(5) As DAABRequest.Parameter
        ' Campos clave del objeto para poder eliminarlos
        prmParameter(0) = New DAABRequest.Parameter("@piv_codSuprestatarioContacto", DbType.String, 20, pstrCodSuprestatarioContacto, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_codsuprestatario", DbType.String, 20, pstrCodigoSuprestatario, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_nombre", DbType.String, 100, pstrNombre, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_correo", DbType.String, 100, pstrCorreo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_telefono", DbType.String, 20, pstrTelefono, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@piv_estado", DbType.String, 1, pstrEstado, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "UP_GCC_CONTACTOSUPRESTATARIO_UPD"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnGestionFlujo", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 22/03/2013
    ''' </remarks>
    Public Function InsertarContactoPreferente(ByVal pstrCodSuprestatarioContacto As Integer, ByVal pstrCodigoSuprestatario As String, _
                                               ByVal pstrNombre As String, ByVal pstrCorreo As String, ByVal pstrTelefono As String) As Boolean Implements IUtilTx.InsertarContactoPreferente

        Dim prmParameter(4) As DAABRequest.Parameter
        ' Campos clave del objeto para poder eliminarlos
        prmParameter(0) = New DAABRequest.Parameter("@piv_codSuprestatarioContacto", DbType.Int32, 8, pstrCodSuprestatarioContacto, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_codsuprestatario", DbType.String, 20, pstrCodigoSuprestatario, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@nombre", DbType.String, 50, pstrNombre, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@correo", DbType.String, 100, pstrCorreo, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@telefono", DbType.String, 20, pstrTelefono, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "UP_GCC_CONTACTO_PREFERENTE_INS"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnGestionFlujo", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            'Finalizamos el grabado en el log
            Throw ex

        Finally
            objRequest.Factory.Dispose()
        End Try

        Return blnRetorno
    End Function

    ''' <summary>
    ''' Gestión de Flujo GCC
    ''' </summary>
    ''' <returns>True si se elimino correctamente. En su defecto devuelve False</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - AEP
    ''' Fecha de Creación  : 22/03/2013
    ''' </remarks>
    Public Function ActualizarSubprestatario(ByVal pstrCodigoSuprestatario As String, _
                                               ByVal pstrDireccion As String) As Boolean Implements IUtilTx.ActualizarSubprestatario

        Dim prmParameter(1) As DAABRequest.Parameter
        ' Campos clave del objeto para poder eliminarlos
        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSuprestatario", DbType.String, 20, pstrCodigoSuprestatario, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_direccion", DbType.String, 200, pstrDireccion, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim blnRetorno As Boolean = False

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "UP_GCC_SUPRESTATARIO_UPD"
        objRequest.Parameters.AddRange(prmParameter)
        Try
            objRequest.Factory.ExecuteNonQuery(objRequest)

            blnRetorno = True
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "fblnGestionFlujo", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
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
''' Implementación de la clase DTemporalNTx
''' </summary>
''' <remarks>
''' Creado Por         : TSF - IJM
''' Fecha de Creación  : 02/05/2012
''' </remarks>
<Guid("A960D9CF-0534-43f4-8879-C9C985A79F48") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase DUtilNTx")> _
Public Class DUtilNtx
    Inherits ServicedComponent
    Implements IUtilNTx



#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "DUtilNTx"
#End Region

#Region "Métodos"

    'Inicio IBK RPR
    Public Function ObtenerFechaCierre(ByVal pCodModulo As String) As String Implements IUtilNTx.ObtenerFechaCierre
        Dim odtbFechaCierre As DataTable = Nothing

        Dim prmParameter(0) As DAABRequest.Parameter

        prmParameter(0) = New DAABRequest.Parameter("@pic_Modulo", DbType.String, 2, pCodModulo, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_FechaHora_Cierre_Sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbFechaCierre = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerFechaCierre", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbFechaCierre)
    End Function
    'Fin IBK

    Public Function ObtenerTipoCambio(ByVal pCodMoneda As String, ByVal pTipoModalidadCambio As String, ByVal pFechaInicioVigencia As String) As String Implements IUtilNTx.ObtenerTipoCambio
        Dim odtbTipoCambio As DataTable = Nothing

        Dim prmParameter(2) As DAABRequest.Parameter

        prmParameter(0) = New DAABRequest.Parameter("@piv_CodMoneda", DbType.String, 3, pCodMoneda, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_TipoModalidadCambio", DbType.String, 3, pTipoModalidadCambio, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_FechaInicioVigencia", DbType.String, 8, pFechaInicioVigencia, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_tipoCambio_get"
            objRequest.Parameters.AddRange(prmParameter)

            odtbTipoCambio = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerTipoCambio", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbTipoCambio)
    End Function

    Public Function ListadoMoneda() As String Implements IUtilNTx.ListadoMoneda
        Dim odtbListadoMoneda As DataTable = Nothing
        'Dim prmParameter(1) As DAABRequest.Parameter


        'Deserealiza la Entidad
        'prmParameter(0) = New DAABRequest.Parameter("@id_tabla", DbType.String, 18, pstrTablaGenerica, ParameterDirection.Input)
        'prmParameter(1) = New DAABRequest.Parameter("@valor2", DbType.String, 18, pstrCodigo, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Moneda_sel"
            'objRequest.Parameters.AddRange(prmParameter)

            'Obtiene el ResulSet
            odtbListadoMoneda = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoMoneda", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoMoneda)
    End Function

    Public Function ListadoPais() As String Implements IUtilNTx.ListadoPais
        Dim odtbListadoPais As DataTable = Nothing

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_Pais_sel"

            'Obtiene el ResulSet
            odtbListadoPais = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoPais", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoPais)
    End Function

    ''' <summary>
    ''' Obtener un la descripcion de un valor especifico en tabla valor generico
    ''' </summary>
    ''' <param name="pstrDominio">Id de la tabla a consultar</param>
    ''' <param name="pstrParametro">valor de la tabla a identificar la descripcion</param>
    ''' <returns>Datatable serializado</returns>
    ''' <remarks>
    ''' Creado Por        : TSF - KCC
    ''' Fecha de Creacion : 04/05/2012
    ''' </remarks>
    Public Function ObtenerValorGenerico(ByVal pstrDominio As String, ByVal pstrParametro As String) As String Implements IUtilNTx.ObtenerValorGenerico
        Try
            Dim odtbListado As DataTable = Nothing
            Dim prmParameter(1) As DAABRequest.Parameter

            prmParameter(0) = New DAABRequest.Parameter("@piv_TABLA", DbType.String, 18, pstrDominio, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@piv_VALOR", DbType.String, 100, pstrParametro, ParameterDirection.Input)

            Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
            Dim objRequest As DAABRequest = obj.CreaRequestSQL()

            Try
                objRequest.CommandType = CommandType.StoredProcedure
                objRequest.Command = "up_gcc_ValorTablaGenerica_Get"
                objRequest.Parameters.AddRange(prmParameter)

                'Obtiene el ResulSet
                odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Catch ex As Exception
                Dim oLog As New CLog
                Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerValorGenerico", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
                oLog = Nothing
                Throw ex
            Finally
                objRequest.Factory.Dispose()
            End Try
            Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lista los estados del contrato a partir del estado "En Elaboracion".
    ''' </summary>
    ''' <returns>Lista de estados, ordenados por el orden que siguen en el flujo (valor4).</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - EBL
    ''' Fecha de Creación  : 16/04/2012
    ''' </remarks>
    Public Function ListarEstadosBusquedaContrato() As String Implements IUtilNTx.ListarEstadosBusquedaContrato
        Try
            Dim odtbListado As DataTable = Nothing

            Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
            Dim objRequest As DAABRequest = obj.CreaRequestSQL()

            Try
                objRequest.CommandType = CommandType.StoredProcedure
                objRequest.Command = "up_gcc_ValorTablaGenerica_EstadosContratoSel"

                'Obtiene el ResulSet
                odtbListado = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

            Catch ex As Exception
                Dim oLog As New CLog
                Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerValorGenerico", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
                oLog = Nothing
                Throw ex
            Finally
                objRequest.Factory.Dispose()
            End Try
            Return CFunciones.SerializeObject(Of DataTable)(odtbListado)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' ListarRegistroCompra
    ''' </summary>
    ''' <param name="strFechaIni"></param>
    ''' <param name="strFechaFin"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListarRegistroCompra(ByVal strFechaIni As String, ByVal strFechaFin As String) As String Implements IUtilNTx.ListarRegistroCompra
        Dim odtbRegistroCompra As DataTable = Nothing

        Dim prmParameter(1) As DAABRequest.Parameter

        prmParameter(0) = New DAABRequest.Parameter("@piv_FechaIni", DbType.String, 8, strFechaIni, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_FechaFin", DbType.String, 8, strFechaFin, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_RegistroCompra_sel"
            objRequest.Parameters.AddRange(prmParameter)

            odtbRegistroCompra = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerTipoCambio", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbRegistroCompra)

    End Function

#End Region

    Public Function ListarPipeline() As String Implements IUtilNTx.ListarPipeline
        Dim odtbRegistroCompra As DataTable = Nothing


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ReportePipeline_Sel"

            odtbRegistroCompra = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerTipoCambio", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbRegistroCompra)

    End Function

    Public Function ListarRetenciones(ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String Implements IUtilNTx.ListarRetenciones
        Dim odtbRetenciones As DataTable = Nothing

        Dim prmParameter(1) As DAABRequest.Parameter

        prmParameter(0) = New DAABRequest.Parameter("@piv_FechaInicial", DbType.String, 20, pFechaInicial, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_FechaFinal", DbType.String, 20, pFechaFinal, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_PRO_ResultadoRetencion"
            objRequest.Parameters.AddRange(prmParameter)
            odtbRetenciones = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerTipoCambio", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbRetenciones)


    End Function
    'IBK JJM
    Public Function ListarRetencionesSunat(ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String Implements IUtilNTx.ListarRetencionesSunat
        Dim odtbRetenciones As DataTable = Nothing

        Dim prmParameter(1) As DAABRequest.Parameter

        prmParameter(0) = New DAABRequest.Parameter("@piv_FechaInicial", DbType.String, 20, pFechaInicial, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@piv_FechaFinal", DbType.String, 20, pFechaFinal, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_PRO_ResultadoRetencionSunat"
            objRequest.Parameters.AddRange(prmParameter)
            odtbRetenciones = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerTipoCambio", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbRetenciones)


    End Function

    'Inicio IBK - AAE - Agrego funciones
    Public Function ListarRegistrosVentas(ByVal strFlag As String, ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String Implements IUtilNTx.ListarRegistrosVentas
        Dim odtbRetenciones As DataTable = Nothing

        Dim prmParameter(2) As DAABRequest.Parameter

        prmParameter(0) = New DAABRequest.Parameter("@Flag", DbType.String, 1, strFlag, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pFechaProcesoIni", DbType.String, 20, pFechaInicial, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@pFechaProcesoFin", DbType.String, 20, pFechaFinal, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_CO_Pro_RegVentas2"
            objRequest.Parameters.AddRange(prmParameter)
            odtbRetenciones = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerTipoCambio", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbRetenciones)
    End Function
    'Inicion IBK - JJM - Agrego funciones
    Public Function ListarRegistrosCompras(ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String Implements IUtilNTx.ListarRegistrosCompras
        Dim odtbRetenciones As DataTable = Nothing

        Dim prmParameter(1) As DAABRequest.Parameter


        prmParameter(0) = New DAABRequest.Parameter("@FechaProceso", DbType.String, 8, pFechaInicial, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@FechaFinal", DbType.String, 8, pFechaFinal, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_RegistroCompras"
            objRequest.Parameters.AddRange(prmParameter)
            odtbRetenciones = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerTipoCambio", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbRetenciones)
    End Function

    Function ListarPipeline2(ByVal strCotiza As String, _
                                    ByVal pCUCliente As String, _
                                    ByVal pRazonSocialCli As String, _
                                    ByVal pCodEjecutivo As String, _
                                    ByVal pCodBanca As String, _
                                    ByVal pCodEstado As String) As String Implements IUtilNTx.ListarPipeline2
        Dim odtbRegistroCompra As DataTable = Nothing

        Dim prmParameter(5) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()
        Dim nbrLength As Integer = strCotiza.Length
        Try
            prmParameter(0) = New DAABRequest.Parameter("@pic_cotizaciones", DbType.String, nbrLength, strCotiza, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pic_CUCliente", DbType.String, 10, pCUCliente, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@pic_RazonSocialCli", DbType.String, 50, pRazonSocialCli, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@pic_CodEjecutivo", DbType.String, 20, pCodEjecutivo, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_CodBanca", DbType.String, 2, pCodBanca, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_CodEstado", DbType.String, 3, pCodEstado, ParameterDirection.Input)

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_ReportePipeline_Sel2"
            objRequest.Parameters.AddRange(prmParameter)
            odtbRegistroCompra = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerTipoCambio", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbRegistroCompra)
    End Function


    Public Function ListarNotasAbono(ByVal pFechaInicial As String, ByVal pFechaFinal As String) As String Implements IUtilNTx.ListarNotasAbono
        Dim odtbRetenciones As DataTable = Nothing

        Dim prmParameter(1) As DAABRequest.Parameter


        prmParameter(0) = New DAABRequest.Parameter("@FechaInicio", DbType.String, 8, pFechaInicial, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@FechaFin", DbType.String, 8, pFechaFinal, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "up_gcc_NotasAbono_sel"
            objRequest.Parameters.AddRange(prmParameter)
            odtbRetenciones = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerTipoCambio", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbRetenciones)
    End Function

    'Fin IBK
    'Fin IBK

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TS - AEP
    ''' Fecha de Creación  : 19/03/2013
    ''' </remarks>
    Public Function ListadoClienteSuprestatario(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pCodigo As String, _
                                     ByVal pCuCliente As String, _
                                     ByVal pTipoDocumento As String, _
                                     ByVal pNumeroDocumento As String, _
                                     ByVal pNombreCliente As String, _
                                     ByVal pDireccion As String, _
                                     ByVal pNombre As String, _
                                     ByVal pCorreo As String, _
                                     ByVal pTelefono As String) As String Implements IUtilNTx.ListadoClienteSuprestatario
        Dim odtbListadoTemporal As DataTable
        Dim prmParameter(12) As DAABRequest.Parameter

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            'Campos TEMPORAL
            prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
            prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
            prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
            prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
            prmParameter(4) = New DAABRequest.Parameter("@pic_Codigo", DbType.String, 100, pCodigo, ParameterDirection.Input)
            prmParameter(5) = New DAABRequest.Parameter("@pic_CuCliente", DbType.String, 100, pCuCliente, ParameterDirection.Input)
            prmParameter(6) = New DAABRequest.Parameter("@pic_TipoDocumento", DbType.String, 100, pTipoDocumento, ParameterDirection.Input)
            prmParameter(7) = New DAABRequest.Parameter("@pic_NroDocumento", DbType.String, 100, pNumeroDocumento, ParameterDirection.Input)
            prmParameter(8) = New DAABRequest.Parameter("@pic_NombreCliente", DbType.String, 100, pNombreCliente, ParameterDirection.Input)
            prmParameter(9) = New DAABRequest.Parameter("@pic_Direccion", DbType.String, 200, pDireccion, ParameterDirection.Input)
            prmParameter(10) = New DAABRequest.Parameter("@pic_nombre", DbType.String, 200, pNombre, ParameterDirection.Input)
            prmParameter(11) = New DAABRequest.Parameter("@pic_correo", DbType.String, 200, pCorreo, ParameterDirection.Input)
            prmParameter(12) = New DAABRequest.Parameter("@pic_telefono", DbType.String, 200, pTelefono, ParameterDirection.Input)


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_CLIENTE_SUPRESTATARIO_SEL"
            objRequest.Parameters.AddRange(prmParameter)

            odtbListadoTemporal = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)
        Catch ex As Exception
            'Grabar en archivo de texto el error real
            Dim oLog As New CLog
            oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ListadoContratos", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)

            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try
        Return CFunciones.SerializeObject(Of DataTable)(odtbListadoTemporal)
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TS - AEP
    ''' Fecha de Creación  : 19/03/2013
    ''' </remarks>
    Public Function ListarContactoSuprestatario(ByVal pPageSize As Integer, _
                                     ByVal pCurrentPage As Integer, _
                                     ByVal pSortColumn As String, _
                                     ByVal pSortOrder As String, _
                                     ByVal pCodSuprestario As String, _
                                     ByVal pNombre As String, _
                                     ByVal pCorreo As String, _
                                     ByVal pTelefono As String) As String Implements IUtilNTx.ListarContactoSuprestatario
        Dim odtbRetenciones As DataTable = Nothing

        Dim prmParameter(7) As DAABRequest.Parameter

        prmParameter(0) = New DAABRequest.Parameter("@pii_PageSize", DbType.Int16, 0, pPageSize, ParameterDirection.Input)
        prmParameter(1) = New DAABRequest.Parameter("@pii_CurrentPage", DbType.Int16, 0, pCurrentPage, ParameterDirection.Input)
        prmParameter(2) = New DAABRequest.Parameter("@piv_SortColumn", DbType.String, 128, pSortColumn, ParameterDirection.Input)
        prmParameter(3) = New DAABRequest.Parameter("@piv_SortOrder", DbType.String, 4, pSortOrder, ParameterDirection.Input)
        prmParameter(4) = New DAABRequest.Parameter("@piv_codSuprestatario", DbType.String, 20, pCodSuprestario, ParameterDirection.Input)
        prmParameter(5) = New DAABRequest.Parameter("@pic_nombre", DbType.String, 200, pNombre, ParameterDirection.Input)
        prmParameter(6) = New DAABRequest.Parameter("@pic_correo", DbType.String, 200, pCorreo, ParameterDirection.Input)
        prmParameter(7) = New DAABRequest.Parameter("@pic_telefono", DbType.String, 200, pTelefono, ParameterDirection.Input)

        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_CONTACTOTOTAL_SEL"
            objRequest.Parameters.AddRange(prmParameter)
            odtbRetenciones = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerTipoCambio", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbRetenciones)
    End Function

    ''' <summary>
    ''' Lista todos las operaciones de una consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TS - AEP
    ''' Fecha de Creación  : 22/03/2013
    ''' </remarks>
    Public Function ObteberContactoPreferente(ByVal pCodSuprestario As String) As String Implements IUtilNTx.ObteberContactoPreferente
        Dim odtbRetenciones As DataTable = Nothing

        Dim prmParameter(0) As DAABRequest.Parameter

        prmParameter(0) = New DAABRequest.Parameter("@piv_CodSuprestatario", DbType.String, 20, pCodSuprestario, ParameterDirection.Input)


        Dim obj As DCDatos = New DCDatos(EConstante.C_NOMBRE_APLICATIVO, 1)
        Dim objRequest As DAABRequest = obj.CreaRequestSQL()

        Try
            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "UP_GCC_CONTACTO_PREFERENTE_GET"
            objRequest.Parameters.AddRange(prmParameter)
            odtbRetenciones = objRequest.Factory.ExecuteDataset(objRequest).Tables(0)

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, GCC.Entity.EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ObtenerTipoCambio", Entity.EConstante.C_SUCESO_ERROR, Entity.EConstante.C_ERROR_APLICACION, CFunciones.fConcatenar("Falló: ", EConstante.C_NOMBRE_APLICATIVO, " - ", C_NOMBRE_CLASE), ex.StackTrace, ex.Message)
            oLog = Nothing
            Throw ex
        Finally
            objRequest.Factory.Dispose()
        End Try

        Return CFunciones.SerializeObject(Of DataTable)(odtbRetenciones)
    End Function
End Class

#End Region
