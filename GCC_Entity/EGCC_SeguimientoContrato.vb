Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_seguimientocontrato
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_seguimientocontrato")> _
Public Class EGcc_seguimientocontrato

#Region " Atributos "

    Private _intCodigoseguimiento As Nullable(Of Integer)
    Private _strCodsolicitudcredito As String
    Private _strCodigoestadocontrato As String
    Private _dtmFechacambioestado As Nullable(Of DateTime)
    Private _strObservacion As String
    Private _strUsuarioregistro As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String
    Private _strCodigoMotivoRechazo As String
    Private _strAdjunto As String

    Private _strFechacambioestado As String

    Private _strCodigoUsuario As String
    Private _strNombreUsuario As String
    Private _strPerfilUsuario As String

#End Region

#Region " Propiedades "

    <XmlElement("Codigoseguimiento")> _
    Public Property Codigoseguimiento() As Integer
        Get
            Return Me._intCodigoseguimiento
        End Get
        Set(ByVal value As Integer)
            Me._intCodigoseguimiento = value
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

    <XmlElement("Codigoestadocontrato")> _
    Public Property Codigoestadocontrato() As String
        Get
            Return Me._strCodigoestadocontrato
        End Get
        Set(ByVal value As String)
            Me._strCodigoestadocontrato = value
        End Set
    End Property

    <XmlElement("Fechacambioestado")> _
    Public Property Fechacambioestado() As DateTime
        Get
            Return Me._dtmFechacambioestado
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechacambioestado = value
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


    <XmlElement("CodigoMotivoRechazo")> _
    Public Property CodigoMotivoRechazo() As String
        Get
            Return Me._strCodigoMotivoRechazo
        End Get
        Set(ByVal value As String)
            Me._strCodigoMotivoRechazo = value
        End Set
    End Property
    <XmlElement("CodigoMotivoRechazo")> _
        Public Property Adjunto() As String
        Get
            Return Me._strAdjunto
        End Get
        Set(ByVal value As String)
            Me._strAdjunto = value
        End Set
    End Property

    <XmlElement("SFechacambioestado")> _
Public Property SFechacambioestado() As String
        Get
            Return Me._strFechacambioestado
        End Get
        Set(ByVal value As String)
            Me._strFechacambioestado = value
        End Set
    End Property

    <XmlElement("CodigoUsuario")> _
    Public Property CodigoUsuario() As String
        Get
            Return Me._strCodigoUsuario
        End Get
        Set(ByVal value As String)
            Me._strCodigoUsuario = value
        End Set
    End Property

    <XmlElement("NombreUsuario")> _
    Public Property NombreUsuario() As String
        Get
            Return Me._strNombreUsuario
        End Get
        Set(ByVal value As String)
            Me._strNombreUsuario = value
        End Set
    End Property

    <XmlElement("PerfilUsuario")> _
    Public Property PerfilUsuario() As String
        Get
            Return Me._strPerfilUsuario
        End Get
        Set(ByVal value As String)
            Me._strPerfilUsuario = value
        End Set
    End Property

#End Region

End Class