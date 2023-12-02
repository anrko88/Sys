Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad ELinea
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 25/05/2012
''' </remarks>
<Serializable(), XmlRoot("ELinea")> _
Public Class ELinea

#Region " Atributos "

    Private _strNumeroLinea As String
    Private _strCodigoLineaOperacion As String
    Private _strClaveCL As String
    Private _strRazonSocial As String

#End Region

#Region " Propiedades "

    <XmlElement("NumeroLinea")> _
    Public Property NumeroLinea() As String
        Get
            Return Me._strNumeroLinea
        End Get
        Set(ByVal value As String)
            Me._strNumeroLinea = value
        End Set
    End Property

    <XmlElement("CodigoLineaOperacion")> _
    Public Property CodigoLineaOperacion() As String
        Get
            Return Me._strCodigoLineaOperacion
        End Get
        Set(ByVal value As String)
            Me._strCodigoLineaOperacion = value
        End Set
    End Property

    <XmlElement("ClaveCL")> _
    Public Property ClaveCL() As String
        Get
            Return Me._strClaveCL
        End Get
        Set(ByVal value As String)
            Me._strClaveCL = value
        End Set
    End Property

    <XmlElement("RazonSocial")> _
    Public Property RazonSocial() As String
        Get
            Return Me._strRazonSocial
        End Get
        Set(ByVal value As String)
            Me._strRazonSocial = value
        End Set
    End Property

#End Region

End Class



''' <summary>
''' Clase que hereda de List(Of ELinea) para la implementación de la lista
''' </summary>
''' <remarks></remarks>
<Serializable(), XmlRoot("ListELinea")> _
Public Class ListELinea
    Inherits List(Of ELinea)

End Class