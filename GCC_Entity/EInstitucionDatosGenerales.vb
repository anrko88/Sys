Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Instituciondatosgenerales
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EInstituciondatosgenerales")> _
Public Class EInstituciondatosgenerales

#Region " Atributos "

    Private _strCodproveedor As String
    Private _strCodinstitucion As String
    Private _strNombreinstitucion As String
    Private _strNombreinstitucioncorto As String
    Private _strIdruc As String
    Private _strIdswift As String
    Private _strIdsbs As String
    Private _strTiponacionalidad As String
    Private _strCodpais As String
    Private _strCoddptoestado As String
    Private _strCodprovinciaciudad As String
    Private _strCoddistrito As String
    Private _strCodpostal As String
    Private _strTextodomiciliolegal As String
    Private _strNumtelefono1 As String
    Private _strNumtelefono2 As String
    Private _strNumfax1 As String
    Private _strNumfax2 As String
    Private _strNumtelex As String
    Private _strIdpaginaweb As String
    Private _strNumcuentabcrmn As String
    Private _strNumcuentabcrme As String
    Private _strCodmodulooperacion As String
    Private _strTipooperacion As String
    Private _dtmFecharegistro As Nullable(Of DateTime)
    Private _strCodusuario As String
    Private _strTextoaudicreacion As String
    Private _strTextoaudimodi As String
    Private _strIdidentificacionsbs As String

#End Region

#Region " Propiedades "

    <XmlElement("Codproveedor")> _
    Public Property Codproveedor() As String
        Get
            Return Me._strCodproveedor
        End Get
        Set(ByVal value As String)
            Me._strCodproveedor = value
        End Set
    End Property

    <XmlElement("Codinstitucion")> _
    Public Property Codinstitucion() As String
        Get
            Return Me._strCodinstitucion
        End Get
        Set(ByVal value As String)
            Me._strCodinstitucion = value
        End Set
    End Property

    <XmlElement("Nombreinstitucion")> _
    Public Property Nombreinstitucion() As String
        Get
            Return Me._strNombreinstitucion
        End Get
        Set(ByVal value As String)
            Me._strNombreinstitucion = value
        End Set
    End Property

    <XmlElement("Nombreinstitucioncorto")> _
    Public Property Nombreinstitucioncorto() As String
        Get
            Return Me._strNombreinstitucioncorto
        End Get
        Set(ByVal value As String)
            Me._strNombreinstitucioncorto = value
        End Set
    End Property

    <XmlElement("Idruc")> _
    Public Property Idruc() As String
        Get
            Return Me._strIdruc
        End Get
        Set(ByVal value As String)
            Me._strIdruc = value
        End Set
    End Property

    <XmlElement("Idswift")> _
    Public Property Idswift() As String
        Get
            Return Me._strIdswift
        End Get
        Set(ByVal value As String)
            Me._strIdswift = value
        End Set
    End Property

    <XmlElement("Idsbs")> _
    Public Property Idsbs() As String
        Get
            Return Me._strIdsbs
        End Get
        Set(ByVal value As String)
            Me._strIdsbs = value
        End Set
    End Property

    <XmlElement("Tiponacionalidad")> _
    Public Property Tiponacionalidad() As String
        Get
            Return Me._strTiponacionalidad
        End Get
        Set(ByVal value As String)
            Me._strTiponacionalidad = value
        End Set
    End Property

    <XmlElement("Codpais")> _
    Public Property Codpais() As String
        Get
            Return Me._strCodpais
        End Get
        Set(ByVal value As String)
            Me._strCodpais = value
        End Set
    End Property

    <XmlElement("Coddptoestado")> _
    Public Property Coddptoestado() As String
        Get
            Return Me._strCoddptoestado
        End Get
        Set(ByVal value As String)
            Me._strCoddptoestado = value
        End Set
    End Property

    <XmlElement("Codprovinciaciudad")> _
    Public Property Codprovinciaciudad() As String
        Get
            Return Me._strCodprovinciaciudad
        End Get
        Set(ByVal value As String)
            Me._strCodprovinciaciudad = value
        End Set
    End Property

    <XmlElement("Coddistrito")> _
    Public Property Coddistrito() As String
        Get
            Return Me._strCoddistrito
        End Get
        Set(ByVal value As String)
            Me._strCoddistrito = value
        End Set
    End Property

    <XmlElement("Codpostal")> _
    Public Property Codpostal() As String
        Get
            Return Me._strCodpostal
        End Get
        Set(ByVal value As String)
            Me._strCodpostal = value
        End Set
    End Property

    <XmlElement("Textodomiciliolegal")> _
    Public Property Textodomiciliolegal() As String
        Get
            Return Me._strTextodomiciliolegal
        End Get
        Set(ByVal value As String)
            Me._strTextodomiciliolegal = value
        End Set
    End Property

    <XmlElement("Numtelefono1")> _
    Public Property Numtelefono1() As String
        Get
            Return Me._strNumtelefono1
        End Get
        Set(ByVal value As String)
            Me._strNumtelefono1 = value
        End Set
    End Property

    <XmlElement("Numtelefono2")> _
    Public Property Numtelefono2() As String
        Get
            Return Me._strNumtelefono2
        End Get
        Set(ByVal value As String)
            Me._strNumtelefono2 = value
        End Set
    End Property

    <XmlElement("Numfax1")> _
    Public Property Numfax1() As String
        Get
            Return Me._strNumfax1
        End Get
        Set(ByVal value As String)
            Me._strNumfax1 = value
        End Set
    End Property

    <XmlElement("Numfax2")> _
    Public Property Numfax2() As String
        Get
            Return Me._strNumfax2
        End Get
        Set(ByVal value As String)
            Me._strNumfax2 = value
        End Set
    End Property

    <XmlElement("Numtelex")> _
    Public Property Numtelex() As String
        Get
            Return Me._strNumtelex
        End Get
        Set(ByVal value As String)
            Me._strNumtelex = value
        End Set
    End Property

    <XmlElement("Idpaginaweb")> _
    Public Property Idpaginaweb() As String
        Get
            Return Me._strIdpaginaweb
        End Get
        Set(ByVal value As String)
            Me._strIdpaginaweb = value
        End Set
    End Property

    <XmlElement("Numcuentabcrmn")> _
    Public Property Numcuentabcrmn() As String
        Get
            Return Me._strNumcuentabcrmn
        End Get
        Set(ByVal value As String)
            Me._strNumcuentabcrmn = value
        End Set
    End Property

    <XmlElement("Numcuentabcrme")> _
    Public Property Numcuentabcrme() As String
        Get
            Return Me._strNumcuentabcrme
        End Get
        Set(ByVal value As String)
            Me._strNumcuentabcrme = value
        End Set
    End Property

    <XmlElement("Codmodulooperacion")> _
    Public Property Codmodulooperacion() As String
        Get
            Return Me._strCodmodulooperacion
        End Get
        Set(ByVal value As String)
            Me._strCodmodulooperacion = value
        End Set
    End Property

    <XmlElement("Tipooperacion")> _
    Public Property Tipooperacion() As String
        Get
            Return Me._strTipooperacion
        End Get
        Set(ByVal value As String)
            Me._strTipooperacion = value
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

    <XmlElement("Codusuario")> _
    Public Property Codusuario() As String
        Get
            Return Me._strCodusuario
        End Get
        Set(ByVal value As String)
            Me._strCodusuario = value
        End Set
    End Property

    <XmlElement("Textoaudicreacion")> _
    Public Property Textoaudicreacion() As String
        Get
            Return Me._strTextoaudicreacion
        End Get
        Set(ByVal value As String)
            Me._strTextoaudicreacion = value
        End Set
    End Property

    <XmlElement("Textoaudimodi")> _
    Public Property Textoaudimodi() As String
        Get
            Return Me._strTextoaudimodi
        End Get
        Set(ByVal value As String)
            Me._strTextoaudimodi = value
        End Set
    End Property

    <XmlElement("Ididentificacionsbs")> _
    Public Property Ididentificacionsbs() As String
        Get
            Return Me._strIdidentificacionsbs
        End Get
        Set(ByVal value As String)
            Me._strIdidentificacionsbs = value
        End Set
    End Property


#End Region

End Class

