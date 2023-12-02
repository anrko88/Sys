Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Seguridad
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 22/02/2012
''' </remarks>
<Serializable(), XmlRoot("ESeguridad")> _
Public Class ESeguridad

#Region " Atributos "

    Private _strCodigoUsuario As String
    Private _strNombreUsuario As String
    Private _strPerfilUsuario As String
    Private _strAccesoUsuario As String
    Private _strPIN As String
    Private _strNombreBD As String
    Private _strServidorBD As String
    Private _strSD As String
    Private _strAmbiente As String
    Private _strAmbienteDesarrollo As String
    Private _strMensajeError As String

#End Region

#Region " Propiedades "

    <XmlElement("CodigoUsuario")> _
    Public Property CodigoUsuario() As String
        Get
            Return Me._strCodigoUsuario
        End Get
        Set(ByVal value As String)
            Me._strCodigoUsuario = value
        End Set
    End Property

    <XmlElement("NombreUsuario")> _
    Public Property NombreUsuario() As String
        Get
            Return Me._strNombreUsuario
        End Get
        Set(ByVal value As String)
            Me._strNombreUsuario = value
        End Set
    End Property

    <XmlElement("PerfilUsuario")> _
    Public Property PerfilUsuario() As String
        Get
            Return Me._strPerfilUsuario
        End Get
        Set(ByVal value As String)
            Me._strPerfilUsuario = value
        End Set
    End Property

    <XmlElement("AccesoUsuario")> _
    Public Property AccesoUsuario() As String
        Get
            Return Me._strAccesoUsuario
        End Get
        Set(ByVal value As String)
            Me._strAccesoUsuario = value
        End Set
    End Property

    <XmlElement("PIN")> _
    Public Property PIN() As String
        Get
            Return Me._strPIN
        End Get
        Set(ByVal value As String)
            Me._strPIN = value
        End Set
    End Property

    <XmlElement("NombreBD")> _
    Public Property NombreBD() As String
        Get
            Return Me._strNombreBD
        End Get
        Set(ByVal value As String)
            Me._strNombreBD = value
        End Set
    End Property

    <XmlElement("ServidorBD")> _
    Public Property ServidorBD() As String
        Get
            Return Me._strServidorBD
        End Get
        Set(ByVal value As String)
            Me._strServidorBD = value
        End Set
    End Property

    <XmlElement("SD")> _
    Public Property SD() As String
        Get
            Return Me._strSD
        End Get
        Set(ByVal value As String)
            Me._strSD = value
        End Set
    End Property

    <XmlElement("Ambiente")> _
    Public Property Ambiente() As String
        Get
            Return Me._strAmbiente
        End Get
        Set(ByVal value As String)
            Me._strAmbiente = value
        End Set
    End Property

    <XmlElement("AmbienteDesarrollo")> _
    Public Property AmbienteDesarrollo() As String
        Get
            Return Me._strAmbienteDesarrollo
        End Get
        Set(ByVal value As String)
            Me._strAmbienteDesarrollo = value
        End Set
    End Property

    <XmlElement("MensajeError")> _
    Public Property MensajeError() As String
        Get
            Return Me._strMensajeError
        End Get
        Set(ByVal value As String)
            Me._strMensajeError = value
        End Set
    End Property

#End Region

End Class
