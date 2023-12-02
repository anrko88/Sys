Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Subproductofinanciero
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("ESubproductofinanciero")> _
Public Class ESubproductofinanciero

#Region " Atributos "

    Private _strCodproductofinancieroactivo As String
    Private _strCodproductofinancieropasivo As String
    Private _strNombresubproductofinanciero As String
    Private _decMontominimo As Nullable(Of Decimal)
    Private _decMontomaximo As Nullable(Of Decimal)
    Private _strTipoplazototal As String
    Private _intCantplazototal As Nullable(Of Integer)
    Private _strTipoplazogracia As String
    Private _intCantplazogracia As Nullable(Of Integer)
    Private _strEstadovigencia As String
    Private _strTextoaudicreacion As String
    Private _strTextoaudimodi As String
    Private _strCodlineahost As String
    Private _strIndintranet As String
    Private _strIndreladeudado As String
    Private _strNombrecortosubproducto As String
    Private _strCodjudicialhost As String
    Private _strInddesembolso As String
    Private _strCodtraslado As String
    Private _strSpftipocliente As String
    Private _strSpfsubproductomes As String
    Private _strCodfuentefto As String
    Private _strCodcarteratransferida As String
    Private _strCodorigencartera As String
    Private _strCodtipotasainteres As String
    Private _intEstadoproducto As Nullable(Of Integer)
    Private _strUsuarioregistro As String
    Private _dtmFecharegistro As Nullable(Of DateTime)

#End Region

#Region " Propiedades "

    <XmlElement("Codproductofinancieroactivo")> _
    Public Property Codproductofinancieroactivo() As String
        Get
            Return Me._strCodproductofinancieroactivo
        End Get
        Set(ByVal value As String)
            Me._strCodproductofinancieroactivo = value
        End Set
    End Property

    <XmlElement("Codproductofinancieropasivo")> _
    Public Property Codproductofinancieropasivo() As String
        Get
            Return Me._strCodproductofinancieropasivo
        End Get
        Set(ByVal value As String)
            Me._strCodproductofinancieropasivo = value
        End Set
    End Property

    <XmlElement("Nombresubproductofinanciero")> _
    Public Property Nombresubproductofinanciero() As String
        Get
            Return Me._strNombresubproductofinanciero
        End Get
        Set(ByVal value As String)
            Me._strNombresubproductofinanciero = value
        End Set
    End Property

    <XmlElement("Montominimo")> _
    Public Property Montominimo() As Decimal
        Get
            Return Me._decMontominimo
        End Get
        Set(ByVal value As Decimal)
            Me._decMontominimo = value
        End Set
    End Property

    <XmlElement("Montomaximo")> _
    Public Property Montomaximo() As Decimal
        Get
            Return Me._decMontomaximo
        End Get
        Set(ByVal value As Decimal)
            Me._decMontomaximo = value
        End Set
    End Property

    <XmlElement("Tipoplazototal")> _
    Public Property Tipoplazototal() As String
        Get
            Return Me._strTipoplazototal
        End Get
        Set(ByVal value As String)
            Me._strTipoplazototal = value
        End Set
    End Property

    <XmlElement("Cantplazototal")> _
    Public Property Cantplazototal() As Integer
        Get
            Return Me._intCantplazototal
        End Get
        Set(ByVal value As Integer)
            Me._intCantplazototal = value
        End Set
    End Property

    <XmlElement("Tipoplazogracia")> _
    Public Property Tipoplazogracia() As String
        Get
            Return Me._strTipoplazogracia
        End Get
        Set(ByVal value As String)
            Me._strTipoplazogracia = value
        End Set
    End Property

    <XmlElement("Cantplazogracia")> _
    Public Property Cantplazogracia() As Integer
        Get
            Return Me._intCantplazogracia
        End Get
        Set(ByVal value As Integer)
            Me._intCantplazogracia = value
        End Set
    End Property

    <XmlElement("Estadovigencia")> _
    Public Property Estadovigencia() As String
        Get
            Return Me._strEstadovigencia
        End Get
        Set(ByVal value As String)
            Me._strEstadovigencia = value
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

    <XmlElement("Codlineahost")> _
    Public Property Codlineahost() As String
        Get
            Return Me._strCodlineahost
        End Get
        Set(ByVal value As String)
            Me._strCodlineahost = value
        End Set
    End Property

    <XmlElement("Indintranet")> _
    Public Property Indintranet() As String
        Get
            Return Me._strIndintranet
        End Get
        Set(ByVal value As String)
            Me._strIndintranet = value
        End Set
    End Property

    <XmlElement("Indreladeudado")> _
    Public Property Indreladeudado() As String
        Get
            Return Me._strIndreladeudado
        End Get
        Set(ByVal value As String)
            Me._strIndreladeudado = value
        End Set
    End Property

    <XmlElement("Nombrecortosubproducto")> _
    Public Property Nombrecortosubproducto() As String
        Get
            Return Me._strNombrecortosubproducto
        End Get
        Set(ByVal value As String)
            Me._strNombrecortosubproducto = value
        End Set
    End Property

    <XmlElement("Codjudicialhost")> _
    Public Property Codjudicialhost() As String
        Get
            Return Me._strCodjudicialhost
        End Get
        Set(ByVal value As String)
            Me._strCodjudicialhost = value
        End Set
    End Property

    <XmlElement("Inddesembolso")> _
    Public Property Inddesembolso() As String
        Get
            Return Me._strInddesembolso
        End Get
        Set(ByVal value As String)
            Me._strInddesembolso = value
        End Set
    End Property

    <XmlElement("Codtraslado")> _
    Public Property Codtraslado() As String
        Get
            Return Me._strCodtraslado
        End Get
        Set(ByVal value As String)
            Me._strCodtraslado = value
        End Set
    End Property

    <XmlElement("Spftipocliente")> _
    Public Property Spftipocliente() As String
        Get
            Return Me._strSpftipocliente
        End Get
        Set(ByVal value As String)
            Me._strSpftipocliente = value
        End Set
    End Property

    <XmlElement("Spfsubproductomes")> _
    Public Property Spfsubproductomes() As String
        Get
            Return Me._strSpfsubproductomes
        End Get
        Set(ByVal value As String)
            Me._strSpfsubproductomes = value
        End Set
    End Property

    <XmlElement("Codfuentefto")> _
    Public Property Codfuentefto() As String
        Get
            Return Me._strCodfuentefto
        End Get
        Set(ByVal value As String)
            Me._strCodfuentefto = value
        End Set
    End Property

    <XmlElement("Codcarteratransferida")> _
    Public Property Codcarteratransferida() As String
        Get
            Return Me._strCodcarteratransferida
        End Get
        Set(ByVal value As String)
            Me._strCodcarteratransferida = value
        End Set
    End Property

    <XmlElement("Codorigencartera")> _
    Public Property Codorigencartera() As String
        Get
            Return Me._strCodorigencartera
        End Get
        Set(ByVal value As String)
            Me._strCodorigencartera = value
        End Set
    End Property

    <XmlElement("Codtipotasainteres")> _
    Public Property Codtipotasainteres() As String
        Get
            Return Me._strCodtipotasainteres
        End Get
        Set(ByVal value As String)
            Me._strCodtipotasainteres = value
        End Set
    End Property

    <XmlElement("Estadoproducto")> _
    Public Property Estadoproducto() As Integer
        Get
            Return Me._intEstadoproducto
        End Get
        Set(ByVal value As Integer)
            Me._intEstadoproducto = value
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

    <XmlElement("Fecharegistro")> _
    Public Property Fecharegistro() As DateTime
        Get
            Return Me._dtmFecharegistro
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFecharegistro = value
        End Set
    End Property


#End Region

End Class