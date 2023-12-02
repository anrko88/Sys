Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Proveedor
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' Modificado Por     : TSF - AEP
''' Fecha de Modificación: 02/08/2012
''' Descripción: Se agrego una propiedad CodUnico.
''' </remarks>
<Serializable(), XmlRoot("EProveedor")> _
Public Class EProveedor

#Region " Atributos "

    Private _strCodproveedor As String = String.Empty
    Private _strCodinstitucion As String = String.Empty
    Private _strCodproveedortipo As String = String.Empty
    Private _strTipooficproveedor As String = String.Empty
    Private _strEstadoproveedor As String = String.Empty
    Private _strTipobancamercado As String = String.Empty
    Private _strCodmodulooperacion As String = String.Empty
    Private _strTipooperacion As String = String.Empty
    Private _dtmFecharegistro As Nullable(Of DateTime)
    Private _strCodusuario As String = String.Empty
    Private _strTextoaudicreacion As String = String.Empty
    Private _strTextoaudimodi As String = String.Empty
    Private _strTipoproducto As String = String.Empty
    Private _strIndproductoactivo As String = String.Empty
    Private _strRuc As String = String.Empty

    Private _intCodigocontacto As Nullable(Of Integer)
    Private _strCorreo As String
    Private _strDireccioncorrespondencia As String

    Private _strCodProcedencia As String = String.Empty
    Private _strCodigoTipoPersona As String = String.Empty
    Private _strCodTipoDocumento As String = String.Empty
    Private _strNumeroDocumento As String = String.Empty
    Private _strRazonSocial As String = String.Empty
    Private _strCodPais As String = String.Empty
    Private _strCodDepartamento As String = String.Empty
    Private _strCodProvincia As String = String.Empty
    Private _strCodDistrito As String = String.Empty
    Private _strTextoDomicilioLegal As String = String.Empty
    Private _strXMLEntity As String = String.Empty

    Private _strCodUnico As String = String.Empty

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

    <XmlElement("Codproveedortipo")> _
    Public Property Codproveedortipo() As String
        Get
            Return Me._strCodproveedortipo
        End Get
        Set(ByVal value As String)
            Me._strCodproveedortipo = value
        End Set
    End Property

    <XmlElement("Tipooficproveedor")> _
    Public Property Tipooficproveedor() As String
        Get
            Return Me._strTipooficproveedor
        End Get
        Set(ByVal value As String)
            Me._strTipooficproveedor = value
        End Set
    End Property

    <XmlElement("Estadoproveedor")> _
    Public Property Estadoproveedor() As String
        Get
            Return Me._strEstadoproveedor
        End Get
        Set(ByVal value As String)
            Me._strEstadoproveedor = value
        End Set
    End Property

    <XmlElement("Tipobancamercado")> _
    Public Property Tipobancamercado() As String
        Get
            Return Me._strTipobancamercado
        End Get
        Set(ByVal value As String)
            Me._strTipobancamercado = value
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

    <XmlElement("Tipoproducto")> _
    Public Property Tipoproducto() As String
        Get
            Return Me._strTipoproducto
        End Get
        Set(ByVal value As String)
            Me._strTipoproducto = value
        End Set
    End Property

    <XmlElement("Indproductoactivo")> _
    Public Property Indproductoactivo() As String
        Get
            Return Me._strIndproductoactivo
        End Get
        Set(ByVal value As String)
            Me._strIndproductoactivo = value
        End Set
    End Property

    <XmlElement("Ruc")> _
    Public Property Ruc() As String
        Get
            Return Me._strRuc
        End Get
        Set(ByVal value As String)
            Me._strRuc = value
        End Set
    End Property

    <XmlElement("Codigotipopersona")> _
    Public Property Codigotipopersona() As String
        Get
            Return Me._strCodigoTipoPersona
        End Get
        Set(ByVal value As String)
            Me._strCodigoTipoPersona = value
        End Set
    End Property

    <XmlElement("Codigocontacto")> _
    Public Property Codigocontacto() As Integer
        Get
            Return Me._intCodigocontacto
        End Get
        Set(ByVal value As Integer)
            Me._intCodigocontacto = value
        End Set
    End Property

    <XmlElement("Correo")> _
    Public Property Correo() As String
        Get
            Return Me._strCorreo
        End Get
        Set(ByVal value As String)
            Me._strCorreo = value
        End Set
    End Property

    <XmlElement("Direccioncorrespondencia")> _
    Public Property Direccioncorrespondencia() As String
        Get
            Return Me._strDireccioncorrespondencia
        End Get
        Set(ByVal value As String)
            Me._strDireccioncorrespondencia = value
        End Set
    End Property

    <XmlElement("CodProcedencia")> _
    Public Property CodProcedencia() As String
        Get
            Return Me._strCodProcedencia
        End Get
        Set(ByVal value As String)
            Me._strCodProcedencia = value
        End Set
    End Property

    <XmlElement("CodTipoDocumento")> _
    Public Property CodTipoDocumento() As String
        Get
            Return Me._strCodTipoDocumento
        End Get
        Set(ByVal value As String)
            Me._strCodTipoDocumento = value
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

    <XmlElement("RazonSocial")> _
    Public Property RazonSocial() As String
        Get
            Return Me._strRazonSocial
        End Get
        Set(ByVal value As String)
            Me._strRazonSocial = value
        End Set
    End Property

    <XmlElement("CodPais")> _
    Public Property CodPais() As String
        Get
            Return Me._strCodPais
        End Get
        Set(ByVal value As String)
            Me._strCodPais = value
        End Set
    End Property

    <XmlElement("CodDepartamento")> _
    Public Property CodDepartamento() As String
        Get
            Return Me._strCodDepartamento
        End Get
        Set(ByVal value As String)
            Me._strCodDepartamento = value
        End Set
    End Property

    <XmlElement("CodProvincia")> _
        Public Property CodProvincia() As String
        Get
            Return Me._strCodProvincia
        End Get
        Set(ByVal value As String)
            Me._strCodProvincia = value
        End Set
    End Property

    <XmlElement("CodDistrito")> _
    Public Property CodDistrito() As String
        Get
            Return Me._strCodDistrito
        End Get
        Set(ByVal value As String)
            Me._strCodDistrito = value
        End Set
    End Property

    <XmlElement("TextoDomicilioLegal")> _
        Public Property TextoDomicilioLegal() As String
        Get
            Return Me._strTextoDomicilioLegal
        End Get
        Set(ByVal value As String)
            Me._strTextoDomicilioLegal = value
        End Set
    End Property

    <XmlElement("XMLEntity")> _
    Public Property XMLEntity() As String
        Get
            Return Me._strXMLEntity
        End Get
        Set(ByVal value As String)
            Me._strXMLEntity = value
        End Set
    End Property

    <XmlElement("Codproveedor")> _
 Public Property CodUnico() As String
        Get
            Return Me._strCodUnico
        End Get
        Set(ByVal value As String)
            Me._strCodUnico = value
        End Set
    End Property

#End Region

End Class