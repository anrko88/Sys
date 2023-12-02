Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_contacto
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_contacto")> _
Public Class EGcc_contacto

#Region " Atributos "

    Private _intCodigocontacto As Nullable(Of Integer)
    Private _strNombre As String
    Private _strCorreo As String
    Private _strTelefono As String
    Private _strAnexo As String
    Private _strCodigounico As String
    Private _strCodigocotizacion As String
    Private _strCodproveedor As String
    Private _strCodsolicitudcredito As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String
    Private _strCodigoCargo As String

#End Region

#Region " Propiedades "

    <XmlElement("Codigocontacto")> _
    Public Property Codigocontacto() As Nullable(Of Integer)
        Get
            Return Me._intCodigocontacto
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodigocontacto = value
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

    <XmlElement("Correo")> _
    Public Property Correo() As String
        Get
            Return Me._strCorreo
        End Get
        Set(ByVal value As String)
            Me._strCorreo = value
        End Set
    End Property

    <XmlElement("Telefono")> _
    Public Property Telefono() As String
        Get
            Return Me._strTelefono
        End Get
        Set(ByVal value As String)
            Me._strTelefono = value
        End Set
    End Property

    <XmlElement("Anexo")> _
    Public Property Anexo() As String
        Get
            Return Me._strAnexo
        End Get
        Set(ByVal value As String)
            Me._strAnexo = value
        End Set
    End Property

    <XmlElement("Codigounico")> _
    Public Property Codigounico() As String
        Get
            Return Me._strCodigounico
        End Get
        Set(ByVal value As String)
            Me._strCodigounico = value
        End Set
    End Property

    <XmlElement("Codigocotizacion")> _
    Public Property Codigocotizacion() As String
        Get
            Return Me._strCodigocotizacion
        End Get
        Set(ByVal value As String)
            Me._strCodigocotizacion = value
        End Set
    End Property

    <XmlElement("Codproveedor")> _
    Public Property Codproveedor() As String
        Get
            Return Me._strCodproveedor
        End Get
        Set(ByVal value As String)
            Me._strCodproveedor = value
        End Set
    End Property

    <XmlElement("Codsolicitudcredito")> _
    Public Property Codsolicitudcredito() As String
        Get
            Return Me._strCodsolicitudcredito
        End Get
        Set(ByVal value As String)
            Me._strCodsolicitudcredito = value
        End Set
    End Property

    <XmlElement("Audestadologico")> _
    Public Property Audestadologico() As Integer
        Get
            Return Me._intAudestadologico
        End Get
        Set(ByVal value As Integer)
            Me._intAudestadologico = value
        End Set
    End Property

    <XmlElement("Audfecharegistro")> _
    Public Property Audfecharegistro() As Nullable(Of DateTime)
        Get
            Return Me._dtmAudfecharegistro
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmAudfecharegistro = value
        End Set
    End Property

    <XmlElement("Audfechamodificacion")> _
    Public Property Audfechamodificacion() As Nullable(Of DateTime)
        Get
            Return Me._dtmAudfechamodificacion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmAudfechamodificacion = value
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


    <XmlElement("CodigoCargo")> _
    Public Property CodigoCargo() As String
        Get
            Return Me._strCodigoCargo
        End Get
        Set(ByVal value As String)
            Me._strCodigoCargo = value
        End Set
    End Property

#End Region

End Class