Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Siniestro
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 07/01/2013
''' </remarks>
<Serializable(), XmlRoot("EGCC_Cesionario")> _
Public Class EGCC_Cesionario

#Region " Atributos "

    Private _strCodsolcredito As String
    Private _intCodCesionario As Nullable(Of Integer)
    Private _strRazonSocial As String
    Private _strTipoDocumento As String
    Private _strNombreTipoDocumento As String
    Private _strNroDocumento As String
    Private _strDireccion As String
    Private _strDepartamento As String
    Private _strProvincia As String
    Private _strDistrito As String

    Private _strUsuarioRegistro As String
    Private _strUsuarioModificacion As String
    Private _strEstadoLogico As String

    Private _strCodUnico As String
    Private _strTipoCuenta As String
    Private _strCodMoneda As String
    Private _strNroCuenta As String

    Private _strCodSubprestatario As String

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

    <XmlElement("RazonSocial")> _
    Public Property RazonSocial() As String
        Get
            Return Me._strRazonSocial
        End Get
        Set(ByVal value As String)
            Me._strRazonSocial = value
        End Set
    End Property

    <XmlElement("NombreTipoDocumento")> _
    Public Property NombreTipoDocumento() As String
        Get
            Return Me._strNombreTipoDocumento
        End Get
        Set(ByVal value As String)
            Me._strNombreTipoDocumento = value
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

    <XmlElement("Direccion")> _
    Public Property Direccion() As String
        Get
            Return Me._strDireccion
        End Get
        Set(ByVal value As String)
            Me._strDireccion = value
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

    <XmlElement("EstadoLogico")> _
    Public Property EstadoLogico() As String
        Get
            Return Me._strEstadoLogico
        End Get
        Set(ByVal value As String)
            Me._strEstadoLogico = value
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

    <XmlElement("TipoCuenta")> _
    Public Property TipoCuenta() As String
        Get
            Return Me._strTipoCuenta
        End Get
        Set(ByVal value As String)
            Me._strTipoCuenta = value
        End Set
    End Property

    <XmlElement("CodMoneda")> _
    Public Property CodMoneda() As String
        Get
            Return Me._strCodMoneda
        End Get
        Set(ByVal value As String)
            Me._strCodMoneda = value
        End Set
    End Property

    <XmlElement("NroCuenta")> _
    Public Property NroCuenta() As String
        Get
            Return Me._strNroCuenta
        End Get
        Set(ByVal value As String)
            Me._strNroCuenta = value
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