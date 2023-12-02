Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Pais
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EPais")> _
Public Class EPais

#Region " Atributos "

    Private _strCodpais As String
    Private _strIdpaissbs As String
    Private _strDescrippais As String
    Private _strTextoaudicreacion As String
    Private _strTextoaudimodi As String

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

    <XmlElement("Idpaissbs")> _
    Public Property Idpaissbs() As String
        Get
            Return Me._strIdpaissbs
        End Get
        Set(ByVal value As String)
            Me._strIdpaissbs = value
        End Set
    End Property

    <XmlElement("Descrippais")> _
    Public Property Descrippais() As String
        Get
            Return Me._strDescrippais
        End Get
        Set(ByVal value As String)
            Me._strDescrippais = value
        End Set
    End Property

    <XmlElement("Textoaudicreacion")> _
    Public Property Textoaudicreacion() As String
        Get
            Return Me._strTextoaudicreacion
        End Get
        Set(ByVal value As String)
            Me._strTextoaudicreacion = value
        End Set
    End Property

    <XmlElement("Textoaudimodi")> _
    Public Property Textoaudimodi() As String
        Get
            Return Me._strTextoaudimodi
        End Get
        Set(ByVal value As String)
            Me._strTextoaudimodi = value
        End Set
    End Property


#End Region

End Class
