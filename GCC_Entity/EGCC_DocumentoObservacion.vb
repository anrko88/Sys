Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_documentoobservacion
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_documentoobservacion")> _
Public Class EGcc_documentoobservacion

#Region " Atributos "

    Private _intNumeroobservacion As Nullable(Of Integer)
    Private _intCodigocontratodocumento As Nullable(Of Integer)
    Private _strNumerocontrato As String
    Private _intCodigodocumento As Nullable(Of Integer)
    Private _strObservacion As String
    Private _strCodigotipoobservacion As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String
    Private _strFlagOrigenObservacion As Integer

#End Region

#Region " Propiedades "

    <XmlElement("Numeroobservacion")> _
    Public Property Numeroobservacion() As Integer
        Get
            Return Me._intNumeroobservacion
        End Get
        Set(ByVal value As Integer)
            Me._intNumeroobservacion = value
        End Set
    End Property

    <XmlElement("Codigocontratodocumento")> _
    Public Property Codigocontratodocumento() As Integer
        Get
            Return Me._intCodigocontratodocumento
        End Get
        Set(ByVal value As Integer)
            Me._intCodigocontratodocumento = value
        End Set
    End Property

    <XmlElement("Numerocontrato")> _
    Public Property Numerocontrato() As String
        Get
            Return Me._strNumerocontrato
        End Get
        Set(ByVal value As String)
            Me._strNumerocontrato = value
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

    <XmlElement("Observacion")> _
    Public Property Observacion() As String
        Get
            Return Me._strObservacion
        End Get
        Set(ByVal value As String)
            Me._strObservacion = value
        End Set
    End Property

    <XmlElement("Codigotipoobservacion")> _
    Public Property Codigotipoobservacion() As String
        Get
            Return Me._strCodigotipoobservacion
        End Get
        Set(ByVal value As String)
            Me._strCodigotipoobservacion = value
        End Set
    End Property

    <XmlElement("Audestadologico")> _
    Public Property Audestadologico() As Integer
        Get
            Return Me._intAudestadologico
        End Get
        Set(ByVal value As Integer)
            Me._intAudestadologico = value
        End Set
    End Property

    <XmlElement("Audfecharegistro")> _
    Public Property Audfecharegistro() As DateTime
        Get
            Return Me._dtmAudfecharegistro
        End Get
        Set(ByVal value As DateTime)
            Me._dtmAudfecharegistro = value
        End Set
    End Property

    <XmlElement("Audfechamodificacion")> _
    Public Property Audfechamodificacion() As DateTime
        Get
            Return Me._dtmAudfechamodificacion
        End Get
        Set(ByVal value As DateTime)
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

    <XmlElement("FlagOrigenObservacion")> _
    Public Property FlagOrigenObservacion() As Integer
        Get
            Return Me._strFlagOrigenObservacion
        End Get
        Set(ByVal value As Integer)
            Me._strFlagOrigenObservacion = value
        End Set
    End Property

#End Region

End Class