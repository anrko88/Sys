Imports System.EnterpriseServices
Imports System.Runtime.InteropServices

'-----------------------------------------------------------------------------
'Nombre             : DBConfig
'Objetivo           : Permite obtener los valores de configuración de la 
'                     cadena de conexión
'Creado Por         : TSF - JRC 
'Fecha de Creación  : 16/04/2012
'-----------------------------------------------------------------------------
<JustInTimeActivation(True), _
Synchronization(SynchronizationOption.Supported)> _
Public Class DBConfig

#Region "constantes"
    Protected Const C_NOMBRE_APLICATIVO As String = "GCC"
    Private Const C_NOMBRE_CLASE As String = "DBConfig"
#End Region

#Region "Atributos"
    Private _servidor As String
    Private _baseDatos As String
    Private _usuario As String
    Private _passWord As String
    Private _provider As String
    Private _idioma As String
    Private _sistema As String
    Private _maxPoolSize As String
    Private _minPoolSize As String
    Private _connectionLifetime As String
    Private _pooling As String
    Private _motor As Integer
    Private _cifrado As String
#End Region

#Region "Propiedades"
    Public Property Servidor() As String
        Get
            Return Me._servidor
        End Get
        Set(ByVal value As String)
            Me._servidor = value
        End Set
    End Property

    Public Property BaseDatos() As String
        Get
            Return Me._baseDatos
        End Get
        Set(ByVal value As String)
            Me._baseDatos = value
        End Set
    End Property

    Public Property Usuario() As String
        Get
            Return Me._usuario
        End Get
        Set(ByVal value As String)
            Me._usuario = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return Me._passWord
        End Get
        Set(ByVal value As String)
            Me._passWord = value
        End Set
    End Property

    Public Property Provider() As String
        Get
            Return Me._provider
        End Get
        Set(ByVal value As String)
            Me._provider = value
        End Set
    End Property

    Public Property Idioma() As String
        Get
            Return Me._idioma
        End Get
        Set(ByVal value As String)
            Me._idioma = value
        End Set
    End Property

    Public Property Sistema() As String
        Get
            Return Me._sistema
        End Get
        Set(ByVal value As String)
            Me._sistema = value
        End Set
    End Property

    Public Property MaxPoolSize() As String
        Get
            Return Me._maxPoolSize
        End Get
        Set(ByVal value As String)
            Me._maxPoolSize = value
        End Set
    End Property

    Public Property MinPoolSize() As String
        Get
            Return Me._minPoolSize
        End Get
        Set(ByVal value As String)
            Me._minPoolSize = value
        End Set
    End Property

    Public Property ConnectionLifetime() As String
        Get
            Return Me._connectionLifetime
        End Get
        Set(ByVal value As String)
            Me._connectionLifetime = value
        End Set
    End Property

    Public Property Pooling() As String
        Get
            Return Me._pooling
        End Get
        Set(ByVal value As String)
            Me._pooling = value
        End Set
    End Property

    Public Property Motor() As Integer
        Get
            Return Me._motor
        End Get
        Set(ByVal value As Integer)
            Me._motor = value
        End Set
    End Property

    Public Property Cifrado() As String
        Get
            Return Me._cifrado
        End Get
        Set(ByVal value As String)
            Me._cifrado = value
        End Set
    End Property

    Public ReadOnly Property CadenaConexion() As String
        Get
            Dim stbConexion As New System.Text.StringBuilder
            If (_motor = 0) Then
                With stbConexion
                    .Append("User Id=")
                    .Append(_usuario)
                    .Append(";Data Source=")
                    .Append(_baseDatos)
                    .Append(";password=")
                    .Append(_passWord)
                    .Append(";Pooling=")
                    .Append(_pooling)
                    .Append(";Max Pool Size=")
                    .Append(_maxPoolSize)
                    .Append(";Min Pool Size=")
                    .Append(_minPoolSize)
                    .Append(";Connection Lifetime=")
                    .Append(_connectionLifetime)
                End With
                Return stbConexion.ToString
            Else
                With stbConexion
                    .Append("trusted_connection=yes; server=")
                    .Append(_servidor)
                    .Append(";database=")
                    .Append(_baseDatos)
                    .Append(";User ID=")
                    .Append(_usuario)
                    .Append(";Pwd=")
                    .Append(_passWord)
                End With
                'cadena = "Data Source=" + _servidor + ";Initial Catalog=" + _baseDatos + ";User ID=" + _usuario + ";Password=" + _passWord
                'cadena = cadena + ";Persist Security Info=True;User Instance=False;Connection TimeOut=300;" ' ";Integrated Security=SSPI;" 
                Return stbConexion.ToString
            End If
        End Get
    End Property
#End Region

End Class
