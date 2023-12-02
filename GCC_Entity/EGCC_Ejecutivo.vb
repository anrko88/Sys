Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad EGCC_Ejecutivo
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 19/03/2013
''' </remarks>
<Serializable(), XmlRoot("EGCC_Ejecutivo")> _
Public Class EGCC_Ejecutivo

#Region " Atributos "

    Private _strIDTabla As String
    Private _strIDRegistro As String
    Private _strCodigo As String
    Private _strCodigoEjecutivo As String
    Private _strNombreEjecutivo As String
    Private _strCodELeasing As String
    Private _strCodigoEjecutivoLeasing As String
    Private _strNombreEjecutivoLeasing As String

    Private _strTipo As String

    Private _strUsuarioRegistro As String
    Private _strUsuarioModificacion As String
    Private _strEstadoLogico As String

#End Region

#Region " Propiedades "

    <XmlElement("IDTabla")> _
    Public Property ID_Tabla() As String
        Get
            Return Me._strIDTabla
        End Get
        Set(ByVal value As String)
            Me._strIDTabla = value
        End Set
    End Property

    <XmlElement("IDRegistro")> _
    Public Property IDRegistro() As String
        Get
            Return Me._strIDRegistro
        End Get
        Set(ByVal value As String)
            Me._strIDRegistro = value
        End Set
    End Property

    <XmlElement("Codigo")> _
    Public Property Codigo() As String
        Get
            Return Me._strCodigo
        End Get
        Set(ByVal value As String)
            Me._strCodigo = value
        End Set
    End Property

    <XmlElement("CodigoEjecutivo")> _
    Public Property CodigoEjecutivo() As String
        Get
            Return Me._strCodigoEjecutivo
        End Get
        Set(ByVal value As String)
            Me._strCodigoEjecutivo = value
        End Set
    End Property

    <XmlElement("NombreEjecutivo")> _
    Public Property NombreEjecutivo() As String
        Get
            Return Me._strNombreEjecutivo
        End Get
        Set(ByVal value As String)
            Me._strNombreEjecutivo = value
        End Set
    End Property

    <XmlElement("CodELeasing")> _
    Public Property CodELeasing() As String
        Get
            Return Me._strCodELeasing
        End Get
        Set(ByVal value As String)
            Me._strCodELeasing = value
        End Set
    End Property

    <XmlElement("NombreEjecutivoLeasing")> _
    Public Property NombreEjecutivoLeasing() As String
        Get
            Return Me._strNombreEjecutivoLeasing
        End Get
        Set(ByVal value As String)
            Me._strNombreEjecutivoLeasing = value
        End Set
    End Property

    <XmlElement("Tipo")> _
    Public Property Tipo() As String
        Get
            Return Me._strTipo
        End Get
        Set(ByVal value As String)
            Me._strTipo = value
        End Set
    End Property

    <XmlElement("UsuarioRegistro")> _
   Public Property UsuarioRegistro() As String
        Get
            Return Me._strUsuarioRegistro
        End Get
        Set(ByVal value As String)
            Me._strUsuarioRegistro = value
        End Set
    End Property

    <XmlElement("UsuarioModificacion")> _
    Public Property UsuarioModificacion() As String
        Get
            Return Me._strUsuarioModificacion
        End Get
        Set(ByVal value As String)
            Me._strUsuarioModificacion = value
        End Set
    End Property

    <XmlElement("EstadoLogico")> _
    Public Property EstadoLogico() As String
        Get
            Return Me._strEstadoLogico
        End Get
        Set(ByVal value As String)
            Me._strEstadoLogico = value
        End Set
    End Property

#End Region

End Class