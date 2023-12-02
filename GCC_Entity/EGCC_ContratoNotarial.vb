Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_contratonotarial
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_contratonotarial")> _
Public Class EGcc_contratonotarial

#Region " Atributos "

    Private _intCodigonotarial As Nullable(Of Integer)
    Private _strNumerocontrato As String
    Private _strCodigonotaria As String
    Private _strCodigoubigeo As String
    Private _strKardex As String
    Private _dtmFecha As Nullable(Of DateTime)
    Private _strCodigotipominuta As String
    Private _strObservacion As String
    Private _strNombrearchivo As String
    Private _strMotivo As String
    Private _strCodigoorigenadenda As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String
    Private _dtmFechaEscrituraPublica As Nullable(Of DateTime)
    Private _strCodigoPorCuenta As String


    Private _intNumero As Nullable(Of Integer)

#End Region

#Region "Propiedades"


    <XmlElement("Numero")> _
    Public Property Numero() As Nullable(Of Integer)
        Get
            Return Me._intNumero
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intNumero = value
        End Set
    End Property

    <XmlElement("Codigonotarial")> _
    Public Property Codigonotarial() As Nullable(Of Integer)
        Get
            Return Me._intCodigonotarial
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodigonotarial = value
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

    <XmlElement("Codigonotaria")> _
    Public Property Codigonotaria() As String
        Get
            Return Me._strCodigonotaria
        End Get
        Set(ByVal value As String)
            Me._strCodigonotaria = value
        End Set
    End Property

    <XmlElement("Codigoubigeo")> _
    Public Property Codigoubigeo() As String
        Get
            Return Me._strCodigoubigeo
        End Get
        Set(ByVal value As String)
            Me._strCodigoubigeo = value
        End Set
    End Property

    <XmlElement("Kardex")> _
    Public Property Kardex() As String
        Get
            Return Me._strKardex
        End Get
        Set(ByVal value As String)
            Me._strKardex = value
        End Set
    End Property

    <XmlElement("Fecha")> _
    Public Property Fecha() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecha
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecha = value
        End Set
    End Property

    <XmlElement("Codigotipominuta")> _
    Public Property Codigotipominuta() As String
        Get
            Return Me._strCodigotipominuta
        End Get
        Set(ByVal value As String)
            Me._strCodigotipominuta = value
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

    <XmlElement("Nombrearchivo")> _
    Public Property Nombrearchivo() As String
        Get
            Return Me._strNombrearchivo
        End Get
        Set(ByVal value As String)
            Me._strNombrearchivo = value
        End Set
    End Property

    <XmlElement("Motivo")> _
    Public Property Motivo() As String
        Get
            Return Me._strMotivo
        End Get
        Set(ByVal value As String)
            Me._strMotivo = value
        End Set
    End Property

    <XmlElement("Codigoorigenadenda")> _
    Public Property Codigoorigenadenda() As String
        Get
            Return Me._strCodigoorigenadenda
        End Get
        Set(ByVal value As String)
            Me._strCodigoorigenadenda = value
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

    <XmlElement("FechaEscrituraPublica")> _
    Public Property FechaEscrituraPublica() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaEscrituraPublica
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaEscrituraPublica = value
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

    <XmlElement("CodigoPorCuenta")> _
    Public Property CodigoPorCuenta() As String
        Get
            Return Me._strCodigoPorCuenta
        End Get
        Set(ByVal value As String)
            Me._strCodigoPorCuenta = value
        End Set
    End Property


#End Region

    Public Sub New()
        _intCodigonotarial = Nothing

        _dtmFecha = Nothing

        _intAudestadologico = Nothing
        _dtmAudfecharegistro = Nothing
        _dtmAudfechamodificacion = Nothing

        _dtmFechaEscrituraPublica = Nothing

        _intNumero = Nothing
    End Sub
End Class

''' <summary>
''' Clase que hereda de List(Of EGcc_contratonotarial) para la implementación de la lista
''' </summary>
''' <remarks></remarks>
<Serializable(), XmlRoot("ListEGcc_contratonotarial")> _
Public Class ListEGcc_contratonotarial
    Inherits List(Of EGcc_contratonotarial)

End Class
