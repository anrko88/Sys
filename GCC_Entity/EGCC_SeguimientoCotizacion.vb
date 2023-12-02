Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_seguimientocotizacion
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_seguimientocotizacion")> _
Public Class EGcc_seguimientocotizacion

#Region " Atributos "

    Private _intCodigoseguimiento As Nullable(Of Integer)
    Private _strCodigocotizacion As String
    Private _strCodigotipoestado As String
    Private _dtmFechacambioestado As Nullable(Of DateTime)
    Private _strCodigotipomotivo As String
    Private _strArchivoadjunto As String
    Private _strComentario As String
    Private _dtmFecharegistro As Nullable(Of DateTime)
    Private _strUsuarioregistro As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String

#End Region

#Region " Propiedades "

    <XmlElement("Codigoseguimiento")> _
    Public Property Codigoseguimiento() As Nullable(Of Integer)
        Get
            Return Me._intCodigoseguimiento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodigoseguimiento = value
        End Set
    End Property

    <XmlElement("Codigocotizacion")> _
    Public Property Codigocotizacion() As String
        Get
            Return Me._strCodigocotizacion
        End Get
        Set(ByVal value As String)
            Me._strCodigocotizacion = value
        End Set
    End Property

    <XmlElement("Codigotipoestado")> _
    Public Property Codigotipoestado() As String
        Get
            Return Me._strCodigotipoestado
        End Get
        Set(ByVal value As String)
            Me._strCodigotipoestado = value
        End Set
    End Property

    <XmlElement("Fechacambioestado")> _
    Public Property Fechacambioestado() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechacambioestado
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechacambioestado = value
        End Set
    End Property

    <XmlElement("Codigotipomotivo")> _
    Public Property Codigotipomotivo() As String
        Get
            Return Me._strCodigotipomotivo
        End Get
        Set(ByVal value As String)
            Me._strCodigotipomotivo = value
        End Set
    End Property

    <XmlElement("Archivoadjunto")> _
    Public Property Archivoadjunto() As String
        Get
            Return Me._strArchivoadjunto
        End Get
        Set(ByVal value As String)
            Me._strArchivoadjunto = value
        End Set
    End Property

    <XmlElement("Comentario")> _
    Public Property Comentario() As String
        Get
            Return Me._strComentario
        End Get
        Set(ByVal value As String)
            Me._strComentario = value
        End Set
    End Property

    <XmlElement("Fecharegistro")> _
    Public Property Fecharegistro() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecharegistro
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecharegistro = value
        End Set
    End Property

    <XmlElement("Usuarioregistro")> _
    Public Property Usuarioregistro() As String
        Get
            Return Me._strUsuarioregistro
        End Get
        Set(ByVal value As String)
            Me._strUsuarioregistro = value
        End Set
    End Property

    <XmlElement("Audestadologico")> _
    Public Property Audestadologico() As Nullable(Of Integer)
        Get
            Return Me._intAudestadologico
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intAudestadologico = value
        End Set
    End Property

    <XmlElement("Audfecharegistro")> _
    Public Property Audfecharegistro() As Nullable(Of DateTime)
        Get
            Return Me._dtmAudfecharegistro
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmAudfecharegistro = value
        End Set
    End Property

    <XmlElement("Audfechamodificacion")> _
    Public Property Audfechamodificacion() As Nullable(Of DateTime)
        Get
            Return Me._dtmAudfechamodificacion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmAudfechamodificacion = value
        End Set
    End Property

    <XmlElement("Audusuarioregistro")> _
    Public Property Audusuarioregistro() As String
        Get
            Return Me._strAudusuarioregistro
        End Get
        Set(ByVal value As String)
            Me._strAudusuarioregistro = value
        End Set
    End Property

    <XmlElement("Audusuariomodificacion")> _
    Public Property Audusuariomodificacion() As String
        Get
            Return Me._strAudusuariomodificacion
        End Get
        Set(ByVal value As String)
            Me._strAudusuariomodificacion = value
        End Set
    End Property


#End Region

End Class
