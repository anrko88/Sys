Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Tasainteresreuters
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("ETasainteresreuters")> _
Public Class ETasainteresreuters

#Region " Atributos "

    Private _dtmFechainterfacereuters As Nullable(Of DateTime)
    Private _strTipotasabase As String
    Private _strTipoaplicaciontasa As String
    Private _decPorcentasainteres As Nullable(Of Decimal)
    Private _strEstadocarga As String
    Private _dtmFechacarga As Nullable(Of DateTime)
    Private _strTextoaudicreacion As String
    Private _strTextoaudimodi As String

#End Region

#Region " Propiedades "

    <XmlElement("Fechainterfacereuters")> _
    Public Property Fechainterfacereuters() As DateTime
        Get
            Return Me._dtmFechainterfacereuters
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechainterfacereuters = value
        End Set
    End Property

    <XmlElement("Tipotasabase")> _
    Public Property Tipotasabase() As String
        Get
            Return Me._strTipotasabase
        End Get
        Set(ByVal value As String)
            Me._strTipotasabase = value
        End Set
    End Property

    <XmlElement("Tipoaplicaciontasa")> _
    Public Property Tipoaplicaciontasa() As String
        Get
            Return Me._strTipoaplicaciontasa
        End Get
        Set(ByVal value As String)
            Me._strTipoaplicaciontasa = value
        End Set
    End Property

    <XmlElement("Porcentasainteres")> _
    Public Property Porcentasainteres() As Decimal
        Get
            Return Me._decPorcentasainteres
        End Get
        Set(ByVal value As Decimal)
            Me._decPorcentasainteres = value
        End Set
    End Property

    <XmlElement("Estadocarga")> _
    Public Property Estadocarga() As String
        Get
            Return Me._strEstadocarga
        End Get
        Set(ByVal value As String)
            Me._strEstadocarga = value
        End Set
    End Property

    <XmlElement("Fechacarga")> _
    Public Property Fechacarga() As DateTime
        Get
            Return Me._dtmFechacarga
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechacarga = value
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