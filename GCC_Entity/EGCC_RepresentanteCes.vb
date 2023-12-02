Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_representante
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGCC_RepresentanteCes")> _
Public Class EGCC_RepresentanteCes

#Region " Atributos "

    Private _strCodsolcredito As String
    Private _intCodCesionario As Nullable(Of Integer)
    Private _intCodigorepresentante As Nullable(Of Integer)
    Private _strNombrerepresentante As String
    Private _strCodigoTipoDocumento As String
    Private _strNrodocumento As String
    Private _strPartidaregistral As String
    Private _strOficinaregistral As String
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String
    Private _strEstadoLogico As String


#End Region

#Region " Propiedades "

    <XmlElement("Codsolcredito")> _
    Public Property Codsolcredito() As String
        Get
            Return Me._strCodsolcredito
        End Get
        Set(ByVal value As String)
            Me._strCodsolcredito = value
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

    <XmlElement("CodigoTipoDocumento")> _
    Public Property CodigoTipoDocumento() As String
        Get
            Return Me._strCodigoTipoDocumento
        End Get
        Set(ByVal value As String)
            Me._strCodigoTipoDocumento = value
        End Set
    End Property

    <XmlElement("Codigorepresentante")> _
    Public Property Codigorepresentante() As Nullable(Of Integer)
        Get
            Return Me._intCodigorepresentante
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodigorepresentante = value
        End Set
    End Property

    <XmlElement("Nombrerepresentante")> _
    Public Property Nombrerepresentante() As String
        Get
            Return Me._strNombrerepresentante
        End Get
        Set(ByVal value As String)
            Me._strNombrerepresentante = value
        End Set
    End Property

    <XmlElement("Nrodocumento")> _
    Public Property Nrodocumento() As String
        Get
            Return Me._strNrodocumento
        End Get
        Set(ByVal value As String)
            Me._strNrodocumento = value
        End Set
    End Property

    <XmlElement("Partidaregistral")> _
    Public Property Partidaregistral() As String
        Get
            Return Me._strPartidaregistral
        End Get
        Set(ByVal value As String)
            Me._strPartidaregistral = value
        End Set
    End Property

    <XmlElement("Oficinaregistral")> _
    Public Property Oficinaregistral() As String
        Get
            Return Me._strOficinaregistral
        End Get
        Set(ByVal value As String)
            Me._strOficinaregistral = value
        End Set
    End Property

    <XmlElement("Audusuarioregistro")> _
    Public Property Audusuarioregistro() As String
        Get
            Return Me._strAudusuarioregistro
        End Get
        Set(ByVal value As String)
            Me._strAudusuarioregistro = value
        End Set
    End Property

    <XmlElement("Audusuariomodificacion")> _
    Public Property Audusuariomodificacion() As String
        Get
            Return Me._strAudusuariomodificacion
        End Get
        Set(ByVal value As String)
            Me._strAudusuariomodificacion = value
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
