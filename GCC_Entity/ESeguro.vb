Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad ESeguro
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 27/02/2012
''' </remarks>
<Serializable(), XmlRoot("ESeguro")> _
Public Class ESeguro

#Region " Atributos "

    Private _strCodigousuarioregistro As String
    Private _strNroContrato As String
    Private _strNroPoliza As String
    Private _strCiaSeguro As String
    Private _strCodTipoValor As String
    Private _dtmFechaInicio As Nullable(Of DateTime)
    Private _dtmFechaFin As Nullable(Of DateTime)
    Private _strTipoBien As String
    Private _strCodTipoSeguro As String


#End Region

#Region " Propiedades "

    <XmlElement("Codigousuarioregistro")> _
    Public Property Codigousuarioregistro() As String
        Get
            Return Me._strCodigousuarioregistro
        End Get
        Set(ByVal value As String)
            Me._strCodigousuarioregistro = value
        End Set
    End Property
    <XmlElement("NroContrato")> _
    Public Property NroContraro() As String
        Get
            Return Me._strNroContrato
        End Get
        Set(ByVal value As String)
            Me._strNroContrato = value
        End Set
    End Property
    <XmlElement("NroPoliza")> _
    Public Property NroPoliza() As String
        Get
            Return Me._strNroPoliza
        End Get
        Set(ByVal value As String)
            Me._strNroPoliza = value
        End Set
    End Property
    <XmlElement("CiaSeguro")> _
    Public Property CiaSeguro() As String
        Get
            Return Me._strCiaSeguro
        End Get
        Set(ByVal value As String)
            Me._strCiaSeguro = value
        End Set
    End Property
    <XmlElement("CodigoTipoValor")> _
    Public Property CodigoTipoValor() As String
        Get
            Return Me._strCodTipoValor
        End Get
        Set(ByVal value As String)
            Me._strCodTipoValor = value
        End Set
    End Property
    <XmlElement("FechaInicio")> _
   Public Property FechaInicio() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaInicio
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaInicio = value
        End Set
    End Property

    <XmlElement("FechaFin")> _
    Public Property FechaFin() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaFin
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaFin = value
        End Set
    End Property
    <XmlElement("TipoBien")> _
   Public Property TipoBien() As String
        Get
            Return Me._strTipoBien
        End Get
        Set(ByVal value As String)
            Me._strTipoBien = value
        End Set
    End Property
    <XmlElement("CodigoTipoSeguro")> _
  Public Property CodigoTipoSeguro() As String
        Get
            Return Me._strCodTipoSeguro
        End Get
        Set(ByVal value As String)
            Me._strCodTipoSeguro = value
        End Set
    End Property
#End Region

End Class
