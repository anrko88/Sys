Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_carta
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_carta")> _
Public Class EGcc_carta

#Region " Atributos "

    Private _intCodigocarta As Nullable(Of Integer)
    Private _strNumerocarta As String
    Private _dtmFecharegistro As Nullable(Of DateTime)
    Private _strUsuarioregistro As String
    Private _strCodigocotizacion As String
    Private _strCodsolicitudcredito As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String

#End Region

#Region " Propiedades "

    <XmlElement("Codigocarta")> _
    Public Property Codigocarta() As Nullable(Of Integer)
        Get
            Return Me._intCodigocarta
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodigocarta = value
        End Set
    End Property

    <XmlElement("Numerocarta")> _
    Public Property Numerocarta() As String
        Get
            Return Me._strNumerocarta
        End Get
        Set(ByVal value As String)
            Me._strNumerocarta = value
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

    <XmlElement("Codigocotizacion")> _
    Public Property Codigocotizacion() As String
        Get
            Return Me._strCodigocotizacion
        End Get
        Set(ByVal value As String)
            Me._strCodigocotizacion = value
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

    <XmlElement("Codsolicitudcredito")> _
    Public Property Codsolicitudcredito() As String
        Get
            Return Me._strCodsolicitudcredito
        End Get
        Set(ByVal value As String)
            Me._strCodsolicitudcredito = value
        End Set
    End Property

#End Region

End Class