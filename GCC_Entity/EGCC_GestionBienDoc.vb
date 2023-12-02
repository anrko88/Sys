Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad EGCC_GestionBienDoc
''' </summary>
''' <remarks>
''' Creado Por         : JRC - ADMIN
''' Fecha de Creación  : 04/12/2012
''' </remarks>
<Serializable(), XmlRoot("EGCC_GestionBienDoc")> _
Public Class EGCC_GestionBienDoc

#Region " Atributos "

    Private _strCodSolicitudCredito As String
    Private _intSecFinanciamiento As Nullable(Of Integer)
    Private _strCodRelacionado As String
    Private _intCodigodocumento As Nullable(Of Integer)
    Private _strCodTipoModulo As String

    Private _strNombrearchivo As String
    Private _strRutaarchivo As String
    Private _strComentario As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String

#End Region

#Region " Propiedades "

    <XmlElement("CodSolicitudCredito")> _
    Public Property CodSolicitudCredito() As String
        Get
            Return Me._strCodSolicitudCredito
        End Get
        Set(ByVal value As String)
            Me._strCodSolicitudCredito = value
        End Set
    End Property

    <XmlElement("SecFinanciamiento")> _
    Public Property SecFinanciamiento() As Nullable(Of Integer)
        Get
            Return Me._intSecFinanciamiento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intSecFinanciamiento = value
        End Set
    End Property

    <XmlElement("CodRelacionado")> _
    Public Property CodRelacionado() As String
        Get
            Return Me._strCodRelacionado
        End Get
        Set(ByVal value As String)
            Me._strCodRelacionado = value
        End Set
    End Property

    <XmlElement("Codigodocumento")> _
    Public Property Codigodocumento() As Nullable(Of Integer)
        Get
            Return Me._intCodigodocumento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodigodocumento = value
        End Set
    End Property

    <XmlElement("CodTipoModulo")> _
    Public Property CodTipoModulo() As String
        Get
            Return Me._strCodTipoModulo
        End Get
        Set(ByVal value As String)
            Me._strCodTipoModulo = value
        End Set
    End Property

    <XmlElement("Nombrearchivo")> _
    Public Property Nombrearchivo() As String
        Get
            Return Me._strNombrearchivo
        End Get
        Set(ByVal value As String)
            Me._strNombrearchivo = value
        End Set
    End Property

    <XmlElement("Rutaarchivo")> _
    Public Property Rutaarchivo() As String
        Get
            Return Me._strRutaarchivo
        End Get
        Set(ByVal value As String)
            Me._strRutaarchivo = value
        End Set
    End Property

    <XmlElement("Comentario")> _
    Public Property Comentario() As String
        Get
            Return Me._strComentario
        End Get
        Set(ByVal value As String)
            Me._strComentario = value
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


#End Region

End Class
