Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Ubigeo
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EUbigeo")> _
Public Class EUbigeo

#Region " Atributos "

    Private _strCodigoubigeo As String
    Private _strDepartamento As String
    Private _strProvincia As String
    Private _strDistrito As String
    Private _strDescripcion As String

#End Region

#Region " Propiedades "

    <XmlElement("Codigoubigeo")> _
    Public Property Codigoubigeo() As String
        Get
            Return Me._strCodigoubigeo
        End Get
        Set(ByVal value As String)
            Me._strCodigoubigeo = value
        End Set
    End Property

    <XmlElement("Departamento")> _
    Public Property Departamento() As String
        Get
            Return Me._strDepartamento
        End Get
        Set(ByVal value As String)
            Me._strDepartamento = value
        End Set
    End Property

    <XmlElement("Provincia")> _
    Public Property Provincia() As String
        Get
            Return Me._strProvincia
        End Get
        Set(ByVal value As String)
            Me._strProvincia = value
        End Set
    End Property

    <XmlElement("Distrito")> _
    Public Property Distrito() As String
        Get
            Return Me._strDistrito
        End Get
        Set(ByVal value As String)
            Me._strDistrito = value
        End Set
    End Property

    <XmlElement("Descripcion")> _
    Public Property Descripcion() As String
        Get
            Return Me._strDescripcion
        End Get
        Set(ByVal value As String)
            Me._strDescripcion = value
        End Set
    End Property


#End Region

End Class