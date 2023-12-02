Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Subprestatario
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("ESubprestatario")> _
Public Class ESubprestatario

#Region " Atributos "

    Private _strCodsubprestatario As String
    Private _strCodunico As String
    Private _strNombresubprestatario As String
    Private _strTipoempresatamanio As String
    Private _strCoddocidentificaciontipo As String
    Private _strNumdocidentificacion As String
    Private _strCodciiu As String
    Private _strCodsectorista As String
    Private _strNombresectorista As String
    Private _strCalificacion As String
    Private _strDireccion As String
    Private _strDepartamento As String
    Private _strProvincia As String
    Private _strDistrito As String
    Private _strCalificacionanterior As String
    Private _dtmFechacreacion As Nullable(Of DateTime)
    Private _dtmFechaactualizacion As Nullable(Of DateTime)
    Private _strMailsectorista As String
    Private _strCalificacionnueva As String
    Private _strRmtipocliente As String
    Private _strRmtipoclienteant As String
    Private _dtmRmfeccambiotipocliente As Nullable(Of DateTime)
    Private _strSegmento As String
    Private _strCodtipoexposicion As String
    Private _strCodtipoexposicionant As String
    Private _strCodtipocalificacion As String
    Private _strCodtipocalificacionant As String
    Private _strClientepremium As String
    Private _intBasileatipoexposicion As Integer
    Private _intBasileatipoexposicionanterior As Integer
    Private _intBasileatipoexposicionnueva As Integer
    Private _strDireccioncorrespondencia As String
    Private _strUbigeocorrespondencia As String
    Private _strCodigotipopersona As String
    Private _strCodigoestadocivil As String
    Private _strCodigopais As String

#End Region

#Region " Propiedades "

    <XmlElement("Codsubprestatario")> _
    Public Property Codsubprestatario() As String
        Get
            Return Me._strCodsubprestatario
        End Get
        Set(ByVal value As String)
            Me._strCodsubprestatario = value
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

    <XmlElement("Nombresubprestatario")> _
    Public Property Nombresubprestatario() As String
        Get
            Return Me._strNombresubprestatario
        End Get
        Set(ByVal value As String)
            Me._strNombresubprestatario = value
        End Set
    End Property

    <XmlElement("Tipoempresatamanio")> _
    Public Property Tipoempresatamanio() As String
        Get
            Return Me._strTipoempresatamanio
        End Get
        Set(ByVal value As String)
            Me._strTipoempresatamanio = value
        End Set
    End Property

    <XmlElement("Coddocidentificaciontipo")> _
    Public Property Coddocidentificaciontipo() As String
        Get
            Return Me._strCoddocidentificaciontipo
        End Get
        Set(ByVal value As String)
            Me._strCoddocidentificaciontipo = value
        End Set
    End Property

    <XmlElement("Numdocidentificacion")> _
    Public Property Numdocidentificacion() As String
        Get
            Return Me._strNumdocidentificacion
        End Get
        Set(ByVal value As String)
            Me._strNumdocidentificacion = value
        End Set
    End Property

    <XmlElement("Codciiu")> _
    Public Property Codciiu() As String
        Get
            Return Me._strCodciiu
        End Get
        Set(ByVal value As String)
            Me._strCodciiu = value
        End Set
    End Property

    <XmlElement("Codsectorista")> _
    Public Property Codsectorista() As String
        Get
            Return Me._strCodsectorista
        End Get
        Set(ByVal value As String)
            Me._strCodsectorista = value
        End Set
    End Property

    <XmlElement("Nombresectorista")> _
    Public Property Nombresectorista() As String
        Get
            Return Me._strNombresectorista
        End Get
        Set(ByVal value As String)
            Me._strNombresectorista = value
        End Set
    End Property

    <XmlElement("Calificacion")> _
    Public Property Calificacion() As String
        Get
            Return Me._strCalificacion
        End Get
        Set(ByVal value As String)
            Me._strCalificacion = value
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

    <XmlElement("Calificacionanterior")> _
    Public Property Calificacionanterior() As String
        Get
            Return Me._strCalificacionanterior
        End Get
        Set(ByVal value As String)
            Me._strCalificacionanterior = value
        End Set
    End Property

    <XmlElement("Fechacreacion")> _
    Public Property Fechacreacion() As DateTime
        Get
            Return Me._dtmFechacreacion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechacreacion = value
        End Set
    End Property

    <XmlElement("Fechaactualizacion")> _
    Public Property Fechaactualizacion() As DateTime
        Get
            Return Me._dtmFechaactualizacion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechaactualizacion = value
        End Set
    End Property

    <XmlElement("Mailsectorista")> _
    Public Property Mailsectorista() As String
        Get
            Return Me._strMailsectorista
        End Get
        Set(ByVal value As String)
            Me._strMailsectorista = value
        End Set
    End Property

    <XmlElement("Calificacionnueva")> _
    Public Property Calificacionnueva() As String
        Get
            Return Me._strCalificacionnueva
        End Get
        Set(ByVal value As String)
            Me._strCalificacionnueva = value
        End Set
    End Property

    <XmlElement("Rmtipocliente")> _
    Public Property Rmtipocliente() As String
        Get
            Return Me._strRmtipocliente
        End Get
        Set(ByVal value As String)
            Me._strRmtipocliente = value
        End Set
    End Property

    <XmlElement("Rmtipoclienteant")> _
    Public Property Rmtipoclienteant() As String
        Get
            Return Me._strRmtipoclienteant
        End Get
        Set(ByVal value As String)
            Me._strRmtipoclienteant = value
        End Set
    End Property

    <XmlElement("Rmfeccambiotipocliente")> _
    Public Property Rmfeccambiotipocliente() As DateTime
        Get
            Return Me._dtmRmfeccambiotipocliente
        End Get
        Set(ByVal value As DateTime)
            Me._dtmRmfeccambiotipocliente = value
        End Set
    End Property

    <XmlElement("Segmento")> _
    Public Property Segmento() As String
        Get
            Return Me._strSegmento
        End Get
        Set(ByVal value As String)
            Me._strSegmento = value
        End Set
    End Property

    <XmlElement("Codtipoexposicion")> _
    Public Property Codtipoexposicion() As String
        Get
            Return Me._strCodtipoexposicion
        End Get
        Set(ByVal value As String)
            Me._strCodtipoexposicion = value
        End Set
    End Property

    <XmlElement("Codtipoexposicionant")> _
    Public Property Codtipoexposicionant() As String
        Get
            Return Me._strCodtipoexposicionant
        End Get
        Set(ByVal value As String)
            Me._strCodtipoexposicionant = value
        End Set
    End Property

    <XmlElement("Codtipocalificacion")> _
    Public Property Codtipocalificacion() As String
        Get
            Return Me._strCodtipocalificacion
        End Get
        Set(ByVal value As String)
            Me._strCodtipocalificacion = value
        End Set
    End Property

    <XmlElement("Codtipocalificacionant")> _
    Public Property Codtipocalificacionant() As String
        Get
            Return Me._strCodtipocalificacionant
        End Get
        Set(ByVal value As String)
            Me._strCodtipocalificacionant = value
        End Set
    End Property

    <XmlElement("Clientepremium")> _
    Public Property Clientepremium() As String
        Get
            Return Me._strClientepremium
        End Get
        Set(ByVal value As String)
            Me._strClientepremium = value
        End Set
    End Property

    <XmlElement("Basileatipoexposicion")> _
    Public Property Basileatipoexposicion() As Integer
        Get
            Return Me._intBasileatipoexposicion
        End Get
        Set(ByVal value As Integer)
            Me._intBasileatipoexposicion = value
        End Set
    End Property

    <XmlElement("Basileatipoexposicionanterior")> _
    Public Property Basileatipoexposicionanterior() As Integer
        Get
            Return Me._intBasileatipoexposicionanterior
        End Get
        Set(ByVal value As Integer)
            Me._intBasileatipoexposicionanterior = value
        End Set
    End Property

    <XmlElement("Basileatipoexposicionnueva")> _
    Public Property Basileatipoexposicionnueva() As Integer
        Get
            Return Me._intBasileatipoexposicionnueva
        End Get
        Set(ByVal value As Integer)
            Me._intBasileatipoexposicionnueva = value
        End Set
    End Property

    <XmlElement("Direccioncorrespondencia")> _
    Public Property Direccioncorrespondencia() As String
        Get
            Return Me._strDireccioncorrespondencia
        End Get
        Set(ByVal value As String)
            Me._strDireccioncorrespondencia = value
        End Set
    End Property

    <XmlElement("Ubigeocorrespondencia")> _
    Public Property Ubigeocorrespondencia() As String
        Get
            Return Me._strUbigeocorrespondencia
        End Get
        Set(ByVal value As String)
            Me._strUbigeocorrespondencia = value
        End Set
    End Property

    <XmlElement("Codigotipopersona")> _
    Public Property Codigotipopersona() As String
        Get
            Return Me._strCodigotipopersona
        End Get
        Set(ByVal value As String)
            Me._strCodigotipopersona = value
        End Set
    End Property

    <XmlElement("Codigoestadocivil")> _
    Public Property Codigoestadocivil() As String
        Get
            Return Me._strCodigoestadocivil
        End Get
        Set(ByVal value As String)
            Me._strCodigoestadocivil = value
        End Set
    End Property

    <XmlElement("Codigopais")> _
    Public Property Codigopais() As String
        Get
            Return Me._strCodigopais
        End Get
        Set(ByVal value As String)
            Me._strCodigopais = value
        End Set
    End Property


#End Region

End Class