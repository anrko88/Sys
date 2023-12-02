Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Ubicaciongeografica
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EUbicaciongeografica")> _
Public Class EUbicaciongeografica

#Region " Atributos "

    Private _strCodpais As String
    Private _strCoddptoestado As String
    Private _strCodprovinciaciudad As String
    Private _strCoddistrito As String
    Private _strNombreubicaciongeografica As String
    Private _strCodpostal As String
    Private _strEstadovigencia As String

#End Region

#Region " Propiedades "

    <XmlElement("Codpais")> _
    Public Property Codpais() As String
        Get
            Return Me._strCodpais
        End Get
        Set(ByVal value As String)
            Me._strCodpais = value
        End Set
    End Property

    <XmlElement("Coddptoestado")> _
    Public Property Coddptoestado() As String
        Get
            Return Me._strCoddptoestado
        End Get
        Set(ByVal value As String)
            Me._strCoddptoestado = value
        End Set
    End Property

    <XmlElement("Codprovinciaciudad")> _
    Public Property Codprovinciaciudad() As String
        Get
            Return Me._strCodprovinciaciudad
        End Get
        Set(ByVal value As String)
            Me._strCodprovinciaciudad = value
        End Set
    End Property

    <XmlElement("Coddistrito")> _
    Public Property Coddistrito() As String
        Get
            Return Me._strCoddistrito
        End Get
        Set(ByVal value As String)
            Me._strCoddistrito = value
        End Set
    End Property

    <XmlElement("Nombreubicaciongeografica")> _
    Public Property Nombreubicaciongeografica() As String
        Get
            Return Me._strNombreubicaciongeografica
        End Get
        Set(ByVal value As String)
            Me._strNombreubicaciongeografica = value
        End Set
    End Property

    <XmlElement("Codpostal")> _
    Public Property Codpostal() As String
        Get
            Return Me._strCodpostal
        End Get
        Set(ByVal value As String)
            Me._strCodpostal = value
        End Set
    End Property

    <XmlElement("Estadovigencia")> _
    Public Property Estadovigencia() As String
        Get
            Return Me._strEstadovigencia
        End Get
        Set(ByVal value As String)
            Me._strEstadovigencia = value
        End Set
    End Property


#End Region

End Class