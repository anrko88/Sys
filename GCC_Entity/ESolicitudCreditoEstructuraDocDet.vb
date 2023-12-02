Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Solicitudcreditoestructuradocdet
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("ESolicitudcreditoestructuradocdet")> _
Public Class ESolicitudcreditoestructuradocdet

#Region " Atributos "

    Private _strCodsolicitudcredito As String
    Private _intSecfinanciamiento As Nullable(Of Integer)
    Private _intCodigodocumento As Nullable(Of Integer)
    Private _intCodigodetalle As Nullable(Of Integer)
    Private _strNrodocumento As String
    Private _strTiporubrofinanciamiento As String
    Private _strTipoproducto As String
    Private _strDetrubrofinanciamiento As String
    Private _intCantidad As Nullable(Of Integer)
    Private _decMonto As Nullable(Of Decimal)
    Private _decMontoigv As Nullable(Of Decimal)
    Private _strIndbien As String
    Private _decImporteoriginal As Nullable(Of Decimal)
    Private _decIgvoriginal As Nullable(Of Decimal)
    Private _strMonedaoriginal As String
    Private _strCodproveedor As String

    Private _strTipodocumento As String
    Private _dtmFechaemision As Nullable(Of DateTime)
    Private _strStringFechaemision As String

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

    <XmlElement("Codigodocumento")> _
    Public Property Codigodocumento() As Integer
        Get
            Return Me._intCodigodocumento
        End Get
        Set(ByVal value As Integer)
            Me._intCodigodocumento = value
        End Set
    End Property

    <XmlElement("Codigodetalle")> _
    Public Property Codigodetalle() As Integer
        Get
            Return Me._intCodigodetalle
        End Get
        Set(ByVal value As Integer)
            Me._intCodigodetalle = value
        End Set
    End Property

    <XmlElement("Nrodocumento")> _
    Public Property Nrodocumento() As String
        Get
            Return Me._strNrodocumento
        End Get
        Set(ByVal value As String)
            Me._strNrodocumento = value
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

    <XmlElement("Detrubrofinanciamiento")> _
    Public Property Detrubrofinanciamiento() As String
        Get
            Return Me._strDetrubrofinanciamiento
        End Get
        Set(ByVal value As String)
            Me._strDetrubrofinanciamiento = value
        End Set
    End Property

    <XmlElement("Cantidad")> _
    Public Property Cantidad() As Integer
        Get
            Return Me._intCantidad
        End Get
        Set(ByVal value As Integer)
            Me._intCantidad = value
        End Set
    End Property

    <XmlElement("Monto")> _
    Public Property Monto() As Decimal
        Get
            Return Me._decMonto
        End Get
        Set(ByVal value As Decimal)
            Me._decMonto = value
        End Set
    End Property

    <XmlElement("Montoigv")> _
    Public Property Montoigv() As Decimal
        Get
            Return Me._decMontoigv
        End Get
        Set(ByVal value As Decimal)
            Me._decMontoigv = value
        End Set
    End Property

    <XmlElement("Indbien")> _
    Public Property Indbien() As String
        Get
            Return Me._strIndbien
        End Get
        Set(ByVal value As String)
            Me._strIndbien = value
        End Set
    End Property

    <XmlElement("Importeoriginal")> _
    Public Property Importeoriginal() As Decimal
        Get
            Return Me._decImporteoriginal
        End Get
        Set(ByVal value As Decimal)
            Me._decImporteoriginal = value
        End Set
    End Property

    <XmlElement("Igvoriginal")> _
    Public Property Igvoriginal() As Decimal
        Get
            Return Me._decIgvoriginal
        End Get
        Set(ByVal value As Decimal)
            Me._decIgvoriginal = value
        End Set
    End Property

    <XmlElement("Monedaoriginal")> _
    Public Property Monedaoriginal() As String
        Get
            Return Me._strMonedaoriginal
        End Get
        Set(ByVal value As String)
            Me._strMonedaoriginal = value
        End Set
    End Property

    <XmlElement("Codproveedor")> _
    Public Property Codproveedor() As String
        Get
            Return Me._strCodproveedor
        End Get
        Set(ByVal value As String)
            Me._strCodproveedor = value
        End Set
    End Property

    <XmlElement("Tipodocumento")> _
    Public Property Tipodocumento() As String
        Get
            Return Me._strTipodocumento
        End Get
        Set(ByVal value As String)
            Me._strTipodocumento = value
        End Set
    End Property

    <XmlElement("Fechaemision")> _
    Public Property Fechaemision() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaemision
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaemision = value
        End Set
    End Property

    <XmlElement("StringFechaemision")> _
   Public Property StringFechaemision() As String
        Get
            Return Me._strStringFechaemision
        End Get
        Set(ByVal value As String)
            Me._strStringFechaemision = value
        End Set
    End Property

#End Region

End Class