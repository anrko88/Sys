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
<Serializable(), XmlRoot("EGcc_representante")> _
Public Class EGcc_representante

#Region " Atributos "

    Private _intCodigorepresentante As Nullable(Of Integer)
    Private _strNombrerepresentante As String
    Private _strCodigocontratorepresentante As String
    Private _strNrodocumento As String
    Private _strCorreo As String
    Private _strDireccion As String
    Private _strTelefono As String
    Private _intCodigoestadorepresentante As Nullable(Of Integer)
    Private _strPartidaregistral As String
    Private _strOficinaregistral As String
    Private _strCodigoubigeo As String
    Private _strCodigotiporepresentante As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String
    Private _strCodUnico As String
    Private _strCodigoTipoDocumento As String

#End Region

#Region " Propiedades "

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

    <XmlElement("Codigocontratorepresentante")> _
    Public Property Codigocontratorepresentante() As String
        Get
            Return Me._strCodigocontratorepresentante
        End Get
        Set(ByVal value As String)
            Me._strCodigocontratorepresentante = value
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

    <XmlElement("Correo")> _
    Public Property Correo() As String
        Get
            Return Me._strCorreo
        End Get
        Set(ByVal value As String)
            Me._strCorreo = value
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

    <XmlElement("Telefono")> _
    Public Property Telefono() As String
        Get
            Return Me._strTelefono
        End Get
        Set(ByVal value As String)
            Me._strTelefono = value
        End Set
    End Property

    <XmlElement("Codigoestadorepresentante")> _
    Public Property Codigoestadorepresentante() As Nullable(Of Integer)
        Get
            Return Me._intCodigoestadorepresentante
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodigoestadorepresentante = value
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

    <XmlElement("Codigoubigeo")> _
    Public Property Codigoubigeo() As String
        Get
            Return Me._strCodigoubigeo
        End Get
        Set(ByVal value As String)
            Me._strCodigoubigeo = value
        End Set
    End Property

    <XmlElement("Codigotiporepresentante")> _
    Public Property Codigotiporepresentante() As String
        Get
            Return Me._strCodigotiporepresentante
        End Get
        Set(ByVal value As String)
            Me._strCodigotiporepresentante = value
        End Set
    End Property

    <XmlElement("Audestadologico")> _
    Public Property Audestadologico() As Nullable(Of Integer)
        Get
            Return Me._intAudestadologico
        End Get
        Set(ByVal value As Nullable(Of Integer))
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

    <XmlElement("CodUnico")> _
    Public Property CodUnico() As String
        Get
            Return Me._strCodUnico
        End Get
        Set(ByVal value As String)
            Me._strCodUnico = value
        End Set
    End Property

#End Region

End Class

''' <summary>
''' Clase que hereda de List(Of EGcc_representante) para la implementación de la lista
''' </summary>
''' <remarks></remarks>
<Serializable(), XmlRoot("ListEGcc_representante")> _
Public Class ListEGcc_representante
    Inherits List(Of EGcc_representante)

End Class