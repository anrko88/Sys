Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Inmatriculacion
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EInmatriculacion")> _
Public Class EInmatriculacion

#Region " Atributos "

    Private _strCodsolicitudcredito As String
    Private _intSecfinanciamiento As Nullable(Of Integer)
    Private _strTiporubrofinanciamiento As String
    Private _strTipoproducto As String
    Private _dtmFecharecepcion As Nullable(Of DateTime)
    Private _dtmFechadeclaracionjurada As Nullable(Of DateTime)
    Private _dtmFechacartapoder As Nullable(Of DateTime)
    Private _strNromotor As String
    Private _dtmFechaentrega As Nullable(Of DateTime)
    Private _dtmRecepciontarjetaprop As Nullable(Of DateTime)
    Private _strOtrosdocumentos As String
    Private _dtmFecharegistro As Nullable(Of DateTime)
    Private _strUsuarioregistro As String
    Private _dtmFechamodificacion As Nullable(Of DateTime)
    Private _strUsuariomodificacion As String
    Private _strPlacaactual As String
    Private _strPlacaanterior As String
    Private _strPropietario As String

#End Region

#Region " Propiedades "

    <XmlElement("Codsolicitudcredito")> _
    Public Property Codsolicitudcredito() As String
        Get
            Return Me._strCodsolicitudcredito
        End Get
        Set(ByVal value As String)
            Me._strCodsolicitudcredito = value
        End Set
    End Property

    <XmlElement("Secfinanciamiento")> _
    Public Property Secfinanciamiento() As Integer
        Get
            Return Me._intSecfinanciamiento
        End Get
        Set(ByVal value As Integer)
            Me._intSecfinanciamiento = value
        End Set
    End Property

    <XmlElement("Tiporubrofinanciamiento")> _
    Public Property Tiporubrofinanciamiento() As String
        Get
            Return Me._strTiporubrofinanciamiento
        End Get
        Set(ByVal value As String)
            Me._strTiporubrofinanciamiento = value
        End Set
    End Property

    <XmlElement("Tipoproducto")> _
    Public Property Tipoproducto() As String
        Get
            Return Me._strTipoproducto
        End Get
        Set(ByVal value As String)
            Me._strTipoproducto = value
        End Set
    End Property

    <XmlElement("Fecharecepcion")> _
    Public Property Fecharecepcion() As DateTime
        Get
            Return Me._dtmFecharecepcion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFecharecepcion = value
        End Set
    End Property

    <XmlElement("Fechadeclaracionjurada")> _
    Public Property Fechadeclaracionjurada() As DateTime
        Get
            Return Me._dtmFechadeclaracionjurada
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechadeclaracionjurada = value
        End Set
    End Property

    <XmlElement("Fechacartapoder")> _
    Public Property Fechacartapoder() As DateTime
        Get
            Return Me._dtmFechacartapoder
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechacartapoder = value
        End Set
    End Property

    <XmlElement("Nromotor")> _
    Public Property Nromotor() As String
        Get
            Return Me._strNromotor
        End Get
        Set(ByVal value As String)
            Me._strNromotor = value
        End Set
    End Property

    <XmlElement("Fechaentrega")> _
    Public Property Fechaentrega() As DateTime
        Get
            Return Me._dtmFechaentrega
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechaentrega = value
        End Set
    End Property

    <XmlElement("Recepciontarjetaprop")> _
    Public Property Recepciontarjetaprop() As DateTime
        Get
            Return Me._dtmRecepciontarjetaprop
        End Get
        Set(ByVal value As DateTime)
            Me._dtmRecepciontarjetaprop = value
        End Set
    End Property

    <XmlElement("Otrosdocumentos")> _
    Public Property Otrosdocumentos() As String
        Get
            Return Me._strOtrosdocumentos
        End Get
        Set(ByVal value As String)
            Me._strOtrosdocumentos = value
        End Set
    End Property

    <XmlElement("Fecharegistro")> _
    Public Property Fecharegistro() As DateTime
        Get
            Return Me._dtmFecharegistro
        End Get
        Set(ByVal value As DateTime)
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

    <XmlElement("Fechamodificacion")> _
    Public Property Fechamodificacion() As DateTime
        Get
            Return Me._dtmFechamodificacion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechamodificacion = value
        End Set
    End Property

    <XmlElement("Usuariomodificacion")> _
    Public Property Usuariomodificacion() As String
        Get
            Return Me._strUsuariomodificacion
        End Get
        Set(ByVal value As String)
            Me._strUsuariomodificacion = value
        End Set
    End Property

    <XmlElement("Placaactual")> _
    Public Property Placaactual() As String
        Get
            Return Me._strPlacaactual
        End Get
        Set(ByVal value As String)
            Me._strPlacaactual = value
        End Set
    End Property

    <XmlElement("Placaanterior")> _
    Public Property Placaanterior() As String
        Get
            Return Me._strPlacaanterior
        End Get
        Set(ByVal value As String)
            Me._strPlacaanterior = value
        End Set
    End Property

    <XmlElement("Propietario")> _
    Public Property Propietario() As String
        Get
            Return Me._strPropietario
        End Get
        Set(ByVal value As String)
            Me._strPropietario = value
        End Set
    End Property


#End Region

End Class
