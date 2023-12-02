Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad EGCC_Implicado
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 12/11/2012
''' </remarks>
<Serializable(), XmlRoot("EGCC_Implicado")> _
Public Class EGCC_Implicado

#Region " Atributos "

    Private _strCodSolCredito As String
    Private _intSecFinanciamiento As Nullable(Of Integer)
    Private _intCodDemanda As Nullable(Of Integer)
    Private _intCodImplicado As Nullable(Of Integer)
    Private _strNombreImplicado As String
    Private _strCodTipoDocumento As String
    Private _strNroDocumento As String
    Private _strUsuarioRegistro As String
    Private _strUsuarioModificacion As String
    Private _strCodTipoImplicado As String
    Private _strEstadoLogico As String

#End Region

#Region " Propiedades "

    <XmlElement("CodSolCredito")> _
    Public Property CodSolCredito() As String
        Get
            Return Me._strCodSolCredito
        End Get
        Set(ByVal value As String)
            Me._strCodSolCredito = value
        End Set
    End Property

    <XmlElement("SecFinanciamiento")> _
    Public Property SecFinanciamiento() As Nullable(Of Integer)
        Get
            Return Me._intSecFinanciamiento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intSecFinanciamiento = value
        End Set
    End Property

    <XmlElement("CodImplicado")> _
    Public Property CodImplicado() As Nullable(Of Integer)
        Get
            Return Me._intCodImplicado
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodImplicado = value
        End Set
    End Property

    <XmlElement("CodDemanda")> _
        Public Property CodDemanda() As Nullable(Of Integer)
        Get
            Return Me._intCodDemanda
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodDemanda = value
        End Set
    End Property

    <XmlElement("NombreImplicado")> _
    Public Property NombreImplicado() As String
        Get
            Return Me._strNombreImplicado
        End Get
        Set(ByVal value As String)
            Me._strNombreImplicado = value
        End Set
    End Property

    <XmlElement("CodTipoDocumento")> _
    Public Property CodTipoDocumento() As String
        Get
            Return Me._strCodTipoDocumento
        End Get
        Set(ByVal value As String)
            Me._strCodTipoDocumento = value
        End Set
    End Property

    <XmlElement("NroDocumento")> _
    Public Property NroDocumento() As String
        Get
            Return Me._strNroDocumento
        End Get
        Set(ByVal value As String)
            Me._strNroDocumento = value
        End Set
    End Property

    <XmlElement("UsuarioRegistro")> _
    Public Property UsuarioRegistro() As String
        Get
            Return Me._strUsuarioRegistro
        End Get
        Set(ByVal value As String)
            Me._strUsuarioRegistro = value
        End Set
    End Property

    <XmlElement("UsuarioModificacion")> _
    Public Property UsuarioModificacion() As String
        Get
            Return Me._strUsuarioModificacion
        End Get
        Set(ByVal value As String)
            Me._strUsuarioModificacion = value
        End Set
    End Property

    <XmlElement("CodTipoImplicado")> _
    Public Property CodTipoImplicado() As String
        Get
            Return Me._strCodTipoImplicado
        End Get
        Set(ByVal value As String)
            Me._strCodTipoImplicado = value
        End Set
    End Property

    <XmlElement("EstadoLogico")> _
    Public Property EstadoLogico() As String
        Get
            Return Me._strEstadoLogico
        End Get
        Set(ByVal value As String)
            Me._strEstadoLogico = value
        End Set
    End Property

#End Region

End Class
