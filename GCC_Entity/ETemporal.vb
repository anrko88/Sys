Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Temporal
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 16/04/2012
''' </remarks>
<Serializable(), XmlRoot("ETemporal")> _
Public Class ETemporal

#Region " Atributos "

    Private _intCodigo As Integer = 0
    Private _strTexto As String
    Private _dtmFecha As Nullable(Of DateTime)
    Private _sFecha As String
    Private _intNumero As Nullable(Of Integer)
    Private _decDecimales As Nullable(Of Decimal)
    Private _strComentario As String
    Private _strFlag As String

#End Region

#Region " Propiedades "

    <XmlElement("Codigo")> _
    Public Property Codigo() As Integer
        Get
            Return Me._intCodigo
        End Get
        Set(ByVal value As Integer)
            Me._intCodigo = value
        End Set
    End Property

    <XmlElement("Texto")> _
    Public Property Texto() As String
        Get
            Return Me._strTexto
        End Get
        Set(ByVal value As String)
            Me._strTexto = value
        End Set
    End Property

    <XmlElement("SFecha")> _
    Public Property SFecha() As String
        Get
            Return Me._sFecha
        End Get
        Set(ByVal value As String)
            Me._sFecha = value
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

    <XmlElement("Numero")> _
    Public Property Numero() As Nullable(Of Integer)
        Get
            Return Me._intNumero
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intNumero = value
        End Set
    End Property

    <XmlElement("Decimales")> _
    Public Property Decimales() As Nullable(Of Decimal)
        Get
            Return Me._decDecimales
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decDecimales = value
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

    <XmlElement("Flag")> _
    Public Property Flag() As String
        Get
            Return Me._strFlag
        End Get
        Set(ByVal value As String)
            Me._strFlag = value
        End Set
    End Property


#End Region

    Public Sub New()
        Fecha = Nothing
        Numero = Nothing
        Decimales = Nothing
    End Sub

End Class