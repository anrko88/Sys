Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad EUsuarioUltimus
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EUsuarioUltimus")> _
Public Class EUsuarioUltimus

#Region " Atributos "

    Private _strCodigousuario As String
    Private _strNombreusuario As String

#End Region

#Region " Propiedades "

    <XmlElement("Codigousuario")> _
    Public Property Codigousuario() As String
        Get
            Return Me._strCodigousuario
        End Get
        Set(ByVal value As String)
            Me._strCodigousuario = value
        End Set
    End Property

    <XmlElement("Nombreusuario")> _
    Public Property Nombreusuario() As String
        Get
            Return Me._strNombreusuario
        End Get
        Set(ByVal value As String)
            Me._strNombreusuario = value
        End Set
    End Property

#End Region

End Class

''' <summary>
''' Clase que hereda de List(Of EUsuarioUltimus) para la implementación de la lista
''' </summary>
''' <remarks></remarks>
<Serializable(), XmlRoot("ListEUsuarioUltimus")> _
Public Class ListEUsuarioUltimus
    Inherits List(Of EUsuarioUltimus)

End Class
