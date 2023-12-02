Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad ECliente
''' </summary>
''' <remarks>
''' Creado Por         : TSF - JRC
''' Fecha de Creación  : 15/05/2012
''' </remarks>
<Serializable(), XmlRoot("EClienteRM")> _
Public Class EClienteRM

#Region " Atributos "

    Private _intCodigocliente As Integer
    Private _strCodigounico As String
    Private _intCodigotipodocumento As Integer
    Private _strNumerodocumento As String
    Private _strDireccion As String
    Private _strSegmento As String
    Private _strCodigoejecutivo As String
    Private _strNombreejecutivo As String
    Private _strRazonsocialcliente As String
    Private _intCodigotipopersona As Integer
    Private _strCorreo As String
    Private _strTelefono As String
    Private _intCodigotipocliente As Integer
    Private _strNombres As String
    Private _strApellidopaterno As String
    Private _strApellidomaterno As String
    Private _strBanca As String
    Private _decRatingempresa As Decimal
    Private _dtmFecharantingempresa As DateTime
    Private _strCiiu As String
    Private _strZona As String
    Private _strCodigotienda As String
    Private _strNombretienda As String
    Private _strClasificacionibk As String
    Private _dtmFechaibk As DateTime
    Private _strClasificacionsbs As String
    Private _dtmFechasbs As DateTime
    Private _strClasificacionfeve As String
    Private _dtmFechafeve As DateTime
    Private _strCodigogrupo As String
    Private _strNombregrupo As String
    Private _strPais As String
    Private _strCodigousuarioregistro As String
    Private _dtmFecharegistro As DateTime

    Private _strCodClienteLocal As String

    Private _intCodError As Integer
    Private _strMsgError As String

#End Region

#Region " Propiedades "

    <XmlElement("Codigocliente")> _
    Public Property Codigocliente() As Integer
        Get
            Return Me._intCodigocliente
        End Get
        Set(ByVal value As Integer)
            Me._intCodigocliente = value
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

    <XmlElement("Codigotipodocumento")> _
    Public Property Codigotipodocumento() As Integer
        Get
            Return Me._intCodigotipodocumento
        End Get
        Set(ByVal value As Integer)
            Me._intCodigotipodocumento = value
        End Set
    End Property

    <XmlElement("Numerodocumento")> _
    Public Property Numerodocumento() As String
        Get
            Return Me._strNumerodocumento
        End Get
        Set(ByVal value As String)
            Me._strNumerodocumento = value
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

    <XmlElement("Segmento")> _
    Public Property Segmento() As String
        Get
            Return Me._strSegmento
        End Get
        Set(ByVal value As String)
            Me._strSegmento = value
        End Set
    End Property

    <XmlElement("Codigoejecutivo")> _
    Public Property Codigoejecutivo() As String
        Get
            Return Me._strCodigoejecutivo
        End Get
        Set(ByVal value As String)
            Me._strCodigoejecutivo = value
        End Set
    End Property

    <XmlElement("Nombreejecutivo")> _
    Public Property Nombreejecutivo() As String
        Get
            Return Me._strNombreejecutivo
        End Get
        Set(ByVal value As String)
            Me._strNombreejecutivo = value
        End Set
    End Property

    <XmlElement("Razonsocialcliente")> _
    Public Property Razonsocialcliente() As String
        Get
            Return Me._strRazonsocialcliente
        End Get
        Set(ByVal value As String)
            Me._strRazonsocialcliente = value
        End Set
    End Property

    <XmlElement("Codigotipopersona")> _
    Public Property Codigotipopersona() As Integer
        Get
            Return Me._intCodigotipopersona
        End Get
        Set(ByVal value As Integer)
            Me._intCodigotipopersona = value
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

    <XmlElement("Codigotipocliente")> _
    Public Property Codigotipocliente() As Integer
        Get
            Return Me._intCodigotipocliente
        End Get
        Set(ByVal value As Integer)
            Me._intCodigotipocliente = value
        End Set
    End Property

    <XmlElement("Nombres")> _
    Public Property Nombres() As String
        Get
            Return Me._strNombres
        End Get
        Set(ByVal value As String)
            Me._strNombres = value
        End Set
    End Property

    <XmlElement("Apellidopaterno")> _
    Public Property Apellidopaterno() As String
        Get
            Return Me._strApellidopaterno
        End Get
        Set(ByVal value As String)
            Me._strApellidopaterno = value
        End Set
    End Property

    <XmlElement("Apellidomaterno")> _
    Public Property Apellidomaterno() As String
        Get
            Return Me._strApellidomaterno
        End Get
        Set(ByVal value As String)
            Me._strApellidomaterno = value
        End Set
    End Property

    <XmlElement("Banca")> _
    Public Property Banca() As String
        Get
            Return Me._strBanca
        End Get
        Set(ByVal value As String)
            Me._strBanca = value
        End Set
    End Property

    <XmlElement("Ratingempresa")> _
    Public Property Ratingempresa() As Decimal
        Get
            Return Me._decRatingempresa
        End Get
        Set(ByVal value As Decimal)
            Me._decRatingempresa = value
        End Set
    End Property

    <XmlElement("Fecharantingempresa")> _
    Public Property Fecharantingempresa() As DateTime
        Get
            Return Me._dtmFecharantingempresa
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFecharantingempresa = value
        End Set
    End Property

    <XmlElement("Ciiu")> _
    Public Property Ciiu() As String
        Get
            Return Me._strCiiu
        End Get
        Set(ByVal value As String)
            Me._strCiiu = value
        End Set
    End Property

    <XmlElement("Zona")> _
    Public Property Zona() As String
        Get
            Return Me._strZona
        End Get
        Set(ByVal value As String)
            Me._strZona = value
        End Set
    End Property

    <XmlElement("Codigotienda")> _
    Public Property Codigotienda() As String
        Get
            Return Me._strCodigotienda
        End Get
        Set(ByVal value As String)
            Me._strCodigotienda = value
        End Set
    End Property

    <XmlElement("Nombretienda")> _
    Public Property Nombretienda() As String
        Get
            Return Me._strNombretienda
        End Get
        Set(ByVal value As String)
            Me._strNombretienda = value
        End Set
    End Property

    <XmlElement("Clasificacionibk")> _
    Public Property Clasificacionibk() As String
        Get
            Return Me._strClasificacionibk
        End Get
        Set(ByVal value As String)
            Me._strClasificacionibk = value
        End Set
    End Property

    <XmlElement("Fechaibk")> _
    Public Property Fechaibk() As DateTime
        Get
            Return Me._dtmFechaibk
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechaibk = value
        End Set
    End Property

    <XmlElement("Clasificacionsbs")> _
    Public Property Clasificacionsbs() As String
        Get
            Return Me._strClasificacionsbs
        End Get
        Set(ByVal value As String)
            Me._strClasificacionsbs = value
        End Set
    End Property

    <XmlElement("Fechasbs")> _
    Public Property Fechasbs() As DateTime
        Get
            Return Me._dtmFechasbs
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechasbs = value
        End Set
    End Property

    <XmlElement("Clasificacionfeve")> _
    Public Property Clasificacionfeve() As String
        Get
            Return Me._strClasificacionfeve
        End Get
        Set(ByVal value As String)
            Me._strClasificacionfeve = value
        End Set
    End Property

    <XmlElement("Fechafeve")> _
    Public Property Fechafeve() As DateTime
        Get
            Return Me._dtmFechafeve
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechafeve = value
        End Set
    End Property

    <XmlElement("Codigogrupo")> _
    Public Property Codigogrupo() As String
        Get
            Return Me._strCodigogrupo
        End Get
        Set(ByVal value As String)
            Me._strCodigogrupo = value
        End Set
    End Property

    <XmlElement("Nombregrupo")> _
    Public Property Nombregrupo() As String
        Get
            Return Me._strNombregrupo
        End Get
        Set(ByVal value As String)
            Me._strNombregrupo = value
        End Set
    End Property

    <XmlElement("Pais")> _
    Public Property Pais() As String
        Get
            Return Me._strPais
        End Get
        Set(ByVal value As String)
            Me._strPais = value
        End Set
    End Property

    <XmlElement("Codigousuarioregistro")> _
    Public Property Codigousuarioregistro() As String
        Get
            Return Me._strCodigousuarioregistro
        End Get
        Set(ByVal value As String)
            Me._strCodigousuarioregistro = value
        End Set
    End Property

    <XmlElement("Fecharegistro")> _
    Public Property Fecharegistro() As DateTime
        Get
            Return Me._dtmFecharegistro
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFecharegistro = value
        End Set
    End Property

    <XmlElement("CodClienteLocal")> _
    Public Property CodClienteLocal() As String
        Get
            Return Me._strCodClienteLocal
        End Get
        Set(ByVal value As String)
            Me._strCodClienteLocal = value
        End Set
    End Property

    <XmlElement("MsgError")> _
    Public Property MsgError() As String
        Get
            Return Me._strMsgError
        End Get
        Set(ByVal value As String)
            Me._strMsgError = value
        End Set
    End Property


    <XmlElement("CodError")> _
    Public Property CodError() As Integer
        Get
            Return Me._intCodError
        End Get
        Set(ByVal value As Integer)
            Me._intCodError = value
        End Set
    End Property

#End Region

End Class
