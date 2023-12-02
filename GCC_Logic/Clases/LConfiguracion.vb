Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports GCC.Entity
Imports GCC.Common
Imports GCC.Data

#Region "Clase No Transaccional"
'-----------------------------------------------------------------------------
'Nombre             : LConfiguracionNTx
'Objetivo           : Implementación de la clase LConfiguracionNTx
'Creado Por         : TSF - KCC
'Fecha de Creación  : 22/05/2012
'-----------------------------------------------------------------------------
<Guid("2F515351-2A67-49f3-A53D-4362596EB03A") _
, JustInTimeActivation(True) _
, Transaction(TransactionOption.Disabled, Isolation:=TransactionIsolationLevel.Serializable) _
, Synchronization(SynchronizationOption.Required) _
, Description("Implementación de la clase LConfiguracionNTx")> _
Public Class LConfiguracionNTx
    Inherits ServicedComponent
    Implements IConfiguracionNTx

#Region "constantes"
    Private Const C_NOMBRE_CLASE As String = "LConfiguracionNTx"
#End Region

#Region " Seguridad "
    ''' <summary>
    ''' Permite Validar Acceso de Perfil desde el SDA
    ''' </summary>
    ''' <param name="strUsuario">Usuario</param>
    ''' <param name="strPin">Pin</param>
    ''' <param name="strDBServer">Servidor</param>
    ''' <param name="strDBName">Base de Datos</param>
    ''' <param name="strPerfil">Perfil de SDA</param>
    ''' <returns>Una entidad serializada</returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC 
    ''' Fecha de Creación  : 22/05/2012
    '''</remarks>
    Public Function ValidarAccesoUsuario(ByVal strUsuario As String, _
                                         ByVal strPin As String, _
                                         ByVal strDBServer As String, _
                                         ByVal strDBName As String, _
                                         ByVal strPerfil As String) As String Implements IConfiguracionNTx.ValidarAccesoUsuario

        Dim itAcceso As New ESeguridad
        Dim strXML As String = ""
        Try
            Dim iRes As Integer = -1
            Dim oSDAPin As Object = Nothing
            oSDAPin = CreateObject("sdaPinPerfil.Pin")
            oSDAPin.asigna_usuario(strUsuario)
            oSDAPin.asigna_perfil(strPerfil)
            oSDAPin.asigna_pin(strPin)
            iRes = oSDAPin.pLeerPinAccesoPerfil(strDBServer, strDBName)

            If iRes = 0 Then
                itAcceso.SD = oSDAPin.obtiene_sd
                itAcceso.Ambiente = oSDAPin.obtiene_ambiente
                itAcceso.AmbienteDesarrollo = oSDAPin.obtiene_ambientedes
                itAcceso.CodigoUsuario = oSDAPin.obtiene_usuario
                itAcceso.NombreUsuario = oSDAPin.obtiene_usuarioname
                itAcceso.PerfilUsuario = oSDAPin.obtiene_perfil
                itAcceso.AccesoUsuario = oSDAPin.obtiene_acceso
                itAcceso.ServidorBD = oSDAPin.obtiene_dbserver
                itAcceso.NombreBD = oSDAPin.obtiene_basedato
                itAcceso.PIN = strPin
                itAcceso.MensajeError = "OK"
            Else
                itAcceso.MensajeError = "No se ha obtenido correctamente los datos de validacion del usuario desde el sistema SDA.|COM+: sdaPinPerfil.pin, PARAM: User: " & strUsuario & ", PIN: " & strPin & ", Server: " & strDBServer & ", DB: " & strDBName & ", Perfil: " & strPerfil
            End If

        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ValidarAccesoUsuario", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, "Fallo: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            itAcceso.MensajeError = "No se ha obtenido correctamente los datos de validacin del usuario desde el sistema SDA.|COM+: sdaPinPerfil.pin, PARAM: User: " & strUsuario & ", PIN: " & strPin & ", Server: " & strDBServer & ", DB: " & strDBName & ", Perfil: " & strPerfil & " - TRACE: " & ex.Message
        End Try

        Try
            strXML = CFunciones.SerializeObject(itAcceso)
        Catch ex As Exception
            Dim oLog As New CLog
            Dim iRes As Integer = oLog.toWrite(EConstante.C_NOMBRE_APLICATIVO, EConstante.C_AMBIENTE_DESARROLLO, C_NOMBRE_CLASE, "ValidarAccesoUsuario", EConstante.C_SUCESO_ERROR, EConstante.C_ERROR_APLICACION, "Fallo: " & EConstante.C_NOMBRE_APLICATIVO & " - " & C_NOMBRE_CLASE, ex.StackTrace, ex.Message)
            oLog = Nothing
            strXML = "ERROR: Ocurrio un error al serializar el objeto a devolver."
        End Try

        Return strXML
    End Function

    ''' <summary>
    ''' Obtiene los datos del perfil ingresado desde SDA
    ''' </summary>
    ''' <param name="pintCodRolSDA">Codigo del Perfil ingresada</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 23/05/2012
    '''</remarks>
    Public Function ObtenerRolSDA(ByVal pintCodRolSDA As Integer) As String Implements IConfiguracionNTx.ObtenerRolSDA
        Try
            Using objDPerfilNTx As New DPerfilNTx
                Return objDPerfilNTx.ObtenerPerfilxSda(pintCodRolSDA)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de todos los roles activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Creado Por         : TSF - KCC
    ''' Fecha de Creación  : 23/05/2012
    ''' </remarks>
    Public Function ListarRolActivo() As String Implements IConfiguracionNTx.ListarRolActivo
        Try
            Using objDPerfilNTx As New DPerfilNTx
                Return objDPerfilNTx.ListarPerfil()
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
#End Region