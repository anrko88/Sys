Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_documentosleasing
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_documentosleasing")> _
Public Class EGcc_documentosleasing

#Region " Atributos "

    Private _intCodigodocumento As Nullable(Of Integer)
    Private _strNombredocumento As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String

#End Region

#Region " Propiedades "

    <XmlElement("Codigodocumento")> _
    Public Property Codigodocumento() As Integer
        Get
            Return Me._intCodigodocumento
        End Get
        Set(ByVal value As Integer)
            Me._intCodigodocumento = value
        End Set
    End Property

    <XmlElement("Nombredocumento")> _
    Public Property Nombredocumento() As String
        Get
            Return Me._strNombredocumento
        End Get
        Set(ByVal value As String)
            Me._strNombredocumento = value
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


#End Region

End Class
