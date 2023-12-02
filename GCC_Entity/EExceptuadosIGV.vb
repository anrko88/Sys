Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Exceptuadosigv
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EExceptuadosigv")> _
Public Class EExceptuadosigv

#Region " Atributos "

    Private _strCodproveedor As String
    Private _strRuc As String
    Private _strCodunico As String
    Private _strIndicador As String
    Private _strNombre As String
    Private _strIndretencion As String
    Private _strIndbuencont As String
    Private _strIndagenteret As String

#End Region

#Region " Propiedades "

    <XmlElement("Codproveedor")> _
    Public Property Codproveedor() As String
        Get
            Return Me._strCodproveedor
        End Get
        Set(ByVal value As String)
            Me._strCodproveedor = value
        End Set
    End Property

    <XmlElement("Ruc")> _
    Public Property Ruc() As String
        Get
            Return Me._strRuc
        End Get
        Set(ByVal value As String)
            Me._strRuc = value
        End Set
    End Property

    <XmlElement("Codunico")> _
    Public Property Codunico() As String
        Get
            Return Me._strCodunico
        End Get
        Set(ByVal value As String)
            Me._strCodunico = value
        End Set
    End Property

    <XmlElement("Indicador")> _
    Public Property Indicador() As String
        Get
            Return Me._strIndicador
        End Get
        Set(ByVal value As String)
            Me._strIndicador = value
        End Set
    End Property

    <XmlElement("Nombre")> _
    Public Property Nombre() As String
        Get
            Return Me._strNombre
        End Get
        Set(ByVal value As String)
            Me._strNombre = value
        End Set
    End Property

    <XmlElement("Indretencion")> _
    Public Property Indretencion() As String
        Get
            Return Me._strIndretencion
        End Get
        Set(ByVal value As String)
            Me._strIndretencion = value
        End Set
    End Property

    <XmlElement("Indbuencont")> _
    Public Property Indbuencont() As String
        Get
            Return Me._strIndbuencont
        End Get
        Set(ByVal value As String)
            Me._strIndbuencont = value
        End Set
    End Property

    <XmlElement("Indagenteret")> _
    Public Property Indagenteret() As String
        Get
            Return Me._strIndagenteret
        End Get
        Set(ByVal value As String)
            Me._strIndagenteret = value
        End Set
    End Property


#End Region

End Class
