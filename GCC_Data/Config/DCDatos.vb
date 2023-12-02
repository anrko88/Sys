Imports TSF.DAAB
Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

'-----------------------------------------------------------------------------
'Nombre             : DCDATOS
'Objetivo           : Descripción de lo que hace la clase
'Creado Por         : TSF - JRC 
'Fecha de Creación  : 16/04/2012
'-----------------------------------------------------------------------------
<JustInTimeActivation(True), _
Synchronization(SynchronizationOption.Supported)> _
Public Class DCDatos

#Region "Constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DCDatos"
#End Region

#Region "Atributos"
    Protected mAplicacion As String
    Protected mMotor As Integer
    Private mConfiguracion As DBConfig
#End Region

#Region "Constructor"
  
    Public Sub New(ByVal strAplicacion As String, Optional ByVal intMotor As Integer = 0)
        If intMotor = 0 Then
            mAplicacion = strAplicacion
            mMotor = DAABRequest.TipoOrigenDatos.ORACLE
        Else
            mAplicacion = strAplicacion
            mMotor = DAABRequest.TipoOrigenDatos.SQL
        End If
    End Sub
#End Region

#Region "Propiedades"
    Protected ReadOnly Property Configuracion() As DBConfig
        Get
            Return GeneraConfiguracion()
        End Get
    End Property
#End Region

#Region "Funciones"
    Private Function GeneraConfiguracion() As DBConfig
        If mConfiguracion Is Nothing Then
            If mAplicacion Is Nothing OrElse mAplicacion = "" Then
                Throw New ApplicationException("No ha especificado el identificador de la Aplicacion a instanciar.")
            End If
            mMotor = 1
            Try
                Dim d As New DConexion(mAplicacion, mMotor, 0)
                mConfiguracion = d.Parametros
            Catch ex As Exception
                Throw ex
            End Try
        End If
        Return mConfiguracion
    End Function

    Public Function CreaRequest() As DAABRequest
        Dim obOrigen As DAABRequest.TipoOrigenDatos

        If Configuracion.Motor = DAABRequest.TipoOrigenDatos.ORACLE Then
            obOrigen = DAABRequest.TipoOrigenDatos.ORACLE
        Else
            obOrigen = DAABRequest.TipoOrigenDatos.SQL
        End If

        Return New DAABRequest(obOrigen, Configuracion.CadenaConexion)
    End Function

    Public Function CreaRequestSQL() As DAABRequest
        mConfiguracion = Nothing
        Dim obOrigen As DAABRequest.TipoOrigenDatos
        obOrigen = DAABRequest.TipoOrigenDatos.SQL
        Return New DAABRequest(obOrigen, Configuracion.CadenaConexion)
    End Function
#End Region

End Class
