Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad CesionContrato
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGCC_CesionContrato")> _
Public Class EGCC_CesionContrato

#Region " Atributos "

    Private _strNroContrato As String
    Private _strCUCliente As String
    Private _strRazonSocial As String
    Private _strTipoDocumento As String
    Private _strNroDocumento As String
    Private _strClasificacionBien As String
    Private _strEstadoContrato As String
    Private _strCesionPosicion As String

    Private _intCodCesionario As Nullable(Of Integer)
    Private _strUsuarioModificacion As String

    Private _strCodUnico As String
    Private _strCodSubprestatario As String

#End Region

#Region " Propiedades "

    <XmlElement("NroContrato")> _
    Public Property NroContrato() As String
        Get
            Return Me._strNroContrato
        End Get
        Set(ByVal value As String)
            Me._strNroContrato = value
        End Set
    End Property

    <XmlElement("EstadoContrato")> _
    Public Property EstadoContrato() As String
        Get
            Return Me._strEstadoContrato
        End Get
        Set(ByVal value As String)
            Me._strEstadoContrato = value
        End Set
    End Property

    <XmlElement("CUCliente")> _
    Public Property CUCliente() As String
        Get
            Return Me._strCUCliente
        End Get
        Set(ByVal value As String)
            Me._strCUCliente = value
        End Set
    End Property

    <XmlElement("TipoDocumento")> _
    Public Property TipoDocumento() As String
        Get
            Return Me._strTipoDocumento
        End Get
        Set(ByVal value As String)
            Me._strTipoDocumento = value
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

    <XmlElement("RazonSocial")> _
    Public Property RazonSocial() As String
        Get
            Return Me._strRazonSocial
        End Get
        Set(ByVal value As String)
            Me._strRazonSocial = value
        End Set
    End Property

    <XmlElement("ClasificacionBien")> _
    Public Property ClasificacionBien() As String
        Get
            Return Me._strClasificacionBien
        End Get
        Set(ByVal value As String)
            Me._strClasificacionBien = value
        End Set
    End Property

    <XmlElement("CesionPosicion")> _
    Public Property CesionPosicion() As String
        Get
            Return Me._strCesionPosicion
        End Get
        Set(ByVal value As String)
            Me._strCesionPosicion = value
        End Set
    End Property

    <XmlElement("CodCesionario")> _
    Public Property CodCesionario() As Nullable(Of Integer)
        Get
            Return Me._intCodCesionario
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodCesionario = value
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

    <XmlElement("CodUnico")> _
    Public Property CodUnico() As String
        Get
            Return Me._strCodUnico
        End Get
        Set(ByVal value As String)
            Me._strCodUnico = value
        End Set
    End Property

    <XmlElement("CodSubprestatario")> _
    Public Property CodSubprestatario() As String
        Get
            Return Me._strCodSubprestatario
        End Get
        Set(ByVal value As String)
            Me._strCodSubprestatario = value
        End Set
    End Property

#End Region

End Class