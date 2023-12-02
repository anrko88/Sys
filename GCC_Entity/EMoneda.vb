Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Moneda
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EMoneda")> _
Public Class EMoneda

#Region " Atributos "

    Private _strCodmoneda As String
    Private _strIdmonedasbs As String
    Private _strIdmonedaswift As String
    Private _strIdmonedareuters As String
    Private _strNombremoneda As String
    Private _strDescripcortomoneda As String
    Private _strCodpais As String
    Private _strCodmodulooperacion As String
    Private _strTipooperacion As String
    Private _dtmFecharegistro As Nullable(Of DateTime)
    Private _strCodusuario As String
    Private _strTextoaudicreacion As String
    Private _strTextoaudimodi As String

#End Region

#Region " Propiedades "

    <XmlElement("Codmoneda")> _
    Public Property Codmoneda() As String
        Get
            Return Me._strCodmoneda
        End Get
        Set(ByVal value As String)
            Me._strCodmoneda = value
        End Set
    End Property

    <XmlElement("Idmonedasbs")> _
    Public Property Idmonedasbs() As String
        Get
            Return Me._strIdmonedasbs
        End Get
        Set(ByVal value As String)
            Me._strIdmonedasbs = value
        End Set
    End Property

    <XmlElement("Idmonedaswift")> _
    Public Property Idmonedaswift() As String
        Get
            Return Me._strIdmonedaswift
        End Get
        Set(ByVal value As String)
            Me._strIdmonedaswift = value
        End Set
    End Property

    <XmlElement("Idmonedareuters")> _
    Public Property Idmonedareuters() As String
        Get
            Return Me._strIdmonedareuters
        End Get
        Set(ByVal value As String)
            Me._strIdmonedareuters = value
        End Set
    End Property

    <XmlElement("Nombremoneda")> _
    Public Property Nombremoneda() As String
        Get
            Return Me._strNombremoneda
        End Get
        Set(ByVal value As String)
            Me._strNombremoneda = value
        End Set
    End Property

    <XmlElement("Descripcortomoneda")> _
    Public Property Descripcortomoneda() As String
        Get
            Return Me._strDescripcortomoneda
        End Get
        Set(ByVal value As String)
            Me._strDescripcortomoneda = value
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


#End Region

End Class
