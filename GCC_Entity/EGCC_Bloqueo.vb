Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_contacto
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 18/07/2012
''' </remarks>
<Serializable(), XmlRoot("EGCC_Bloqueo")> _
Public Class EGCC_Bloqueo

#Region " Atributos "

    Private _intCodigoBloqueo As Nullable(Of Integer)
    Private _strTipoDocumento As String
    Private _strModulo As String
    Private _strNumeroDocumento As String
    Private _strCodigoUsuario As String
    Private _strNombreUsuario As String
    Private _strFechaInicio As String
    Private _strFechaFin As String
    Private _strActivo As String

#End Region

#Region " Propiedades "

    <XmlElement("CodigoBloqueo")> _
    Public Property CodigoBloqueo() As Nullable(Of Integer)
        Get
            Return Me._intCodigoBloqueo
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodigoBloqueo = value
        End Set
    End Property

    <XmlElement("TipoDocumento")> _
    Public Property TipoDocumento() As String
        Get
            Return Me._strTipoDocumento
        End Get
        Set(ByVal value As String)
            Me._strTipoDocumento = value
        End Set
    End Property

    <XmlElement("Modulo")> _
    Public Property Modulo() As String
        Get
            Return Me._strModulo
        End Get
        Set(ByVal value As String)
            Me._strModulo = value
        End Set
    End Property

    <XmlElement("NumeroDocumento")> _
    Public Property NumeroDocumento() As String
        Get
            Return Me._strNumeroDocumento
        End Get
        Set(ByVal value As String)
            Me._strNumeroDocumento = value
        End Set
    End Property

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

    <XmlElement("FechaInicio")> _
    Public Property FechaInicio() As String
        Get
            Return Me._strFechaInicio
        End Get
        Set(ByVal value As String)
            Me._strFechaInicio = value
        End Set
    End Property

    <XmlElement("FechaFin")> _
    Public Property FechaFin() As String
        Get
            Return Me._strFechaFin
        End Get
        Set(ByVal value As String)
            Me._strFechaFin = value
        End Set
    End Property

    <XmlElement("Activo")> _
    Public Property Activo() As String
        Get
            Return Me._strActivo
        End Get
        Set(ByVal value As String)
            Me._strActivo = value
        End Set
    End Property

#End Region

End Class
