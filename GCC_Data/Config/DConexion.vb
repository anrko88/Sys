Option Strict Off

Imports System.EnterpriseServices
Imports System.Runtime.InteropServices
Imports System.Configuration

'-----------------------------------------------------------------------------
'Nombre             : DConexion
'Objetivo           : Permite obtener la cadena de conexión
'Creado Por         : TSF - JRC 
'Fecha de Creación  : 16/04/2012
'-----------------------------------------------------------------------------
<JustInTimeActivation(True), _
Synchronization(SynchronizationOption.Supported)> _
Public Class DConexion

#Region " constantes "
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DConexion"
#End Region

#Region " Atributos "
    Private mConfig As DBConfig
#End Region

#Region " Propiedades "
    Public ReadOnly Property Parametros() As DBConfig
        Get
            Return mConfig
        End Get
    End Property
#End Region

#Region " Constructores, Métodos y Funciones "

    Sub New(ByVal piAplicacion As String, ByVal piMotor As Integer, Optional ByVal intBD As Integer = 0)
        Try
            mConfig = New DBConfig
            mConfig = CargarCadenaConexion(piAplicacion, piMotor, intBD)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function CargarCadenaConexion(ByVal piAplicacion As String, _
                                          ByVal piMotor As Integer, _
                                          Optional ByVal intBD As Integer = 0) As DBConfig
        mConfig = New DBConfig
        Try
            If piMotor = 0 Then    'Oracle
                mConfig = ObtenerXMLValoresBDORACLE()
            Else                   'SQL Server
                mConfig = ObtenerXMLValoresBDSQL()
            End If
            mConfig.Motor = piMotor

            If mConfig.Cifrado.ToUpper() = GCC.Entity.EConstante.C_CIFRADO.ToString().ToUpper() Then
                Dim objMask As Object = CreateObject("sdaMask.Mask")
                Dim strValorDescifrado As String = ""

                strValorDescifrado = objMask.DesCifra(mConfig.Usuario, mConfig.Password)
                mConfig.Password = strValorDescifrado

                objMask = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try

        Return mConfig
    End Function
#End Region

#Region " Funciones para leer XML "
    Private Function ObtenerXMLValoresBDSQL() As DBConfig
        Dim itemBD As New DBConfig
        Try
            itemBD.Servidor = ConfigurationManager.AppSettings("ServidorSql")
            itemBD.BaseDatos = ConfigurationManager.AppSettings("BaseDatosSql")
            itemBD.Usuario = ConfigurationManager.AppSettings("UsuarioSql")
            itemBD.Password = ConfigurationManager.AppSettings("PasswordSql")
            itemBD.Provider = ConfigurationManager.AppSettings("ProviderSql")
            itemBD.MaxPoolSize = ConfigurationManager.AppSettings("MaxPoolSizeSql")
            itemBD.MinPoolSize = ConfigurationManager.AppSettings("MinPoolSizeSql")
            itemBD.ConnectionLifetime = ConfigurationManager.AppSettings("ConnectionLifetimeSql")
            itemBD.Pooling = ConfigurationManager.AppSettings("PoolingSql")
            itemBD.Cifrado = ConfigurationManager.AppSettings("CifradoSql")
        Catch ex As Exception
            Throw ex
        End Try

        Return itemBD
    End Function

    Private Function ObtenerXMLValoresBDORACLE() As DBConfig
        Dim itemBD As New DBConfig
        Try
            itemBD.Servidor = ConfigurationManager.AppSettings("ServidorOracle")
            itemBD.BaseDatos = ConfigurationManager.AppSettings("BaseDatosOracle")
            itemBD.Usuario = ConfigurationManager.AppSettings("UsuarioOracle")
            itemBD.Password = ConfigurationManager.AppSettings("PasswordOracle")
            itemBD.Provider = ConfigurationManager.AppSettings("ProviderOracle")
            itemBD.MaxPoolSize = ConfigurationManager.AppSettings("MaxPoolSizeOracle")
            itemBD.MinPoolSize = ConfigurationManager.AppSettings("MinPoolSizeOracle")
            itemBD.ConnectionLifetime = ConfigurationManager.AppSettings("ConnectionLifetimeOracle")
            itemBD.Pooling = ConfigurationManager.AppSettings("PoolingOracle")
            itemBD.Cifrado = ConfigurationManager.AppSettings("CifradoOracle")
        Catch ex As Exception
            Throw ex
        End Try

        Return itemBD
    End Function
#End Region

End Class
