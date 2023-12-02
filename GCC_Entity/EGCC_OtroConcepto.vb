Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_otroconcepto
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_otroconcepto")> _
Public Class EGcc_otroconcepto

#Region " Atributos "

    Private _strCodsolicitudcredito As String = String.Empty
    Private _intSecfinanciamiento As Nullable(Of Integer) = 0
    Private _intCodigootroconcepto As Nullable(Of Integer) = 0
    Private _strCodigooperacion As String = String.Empty
    Private _dtmFecharegistro As Nullable(Of DateTime) = New DateTime(1900, 1, 1)
    Private _decImporte As Nullable(Of Decimal) = 0
    Private _decImporteigv As Nullable(Of Decimal) = 0
    Private _strCodigoestadopago As String = String.Empty
    Private _dtmFechapago As DateTime = New DateTime(1900, 1, 1)
    Private _strCodigotipocuenta As String = String.Empty
    Private _strCuentacargo As String = String.Empty
    Private _strCodigoestadocobro As String = String.Empty
    Private _dtmFechacobro As DateTime = New DateTime(1900, 1, 1)
    Private _strCodigomoneda As String = String.Empty
    Private _strCodigootroconcepto2 As String = String.Empty
    Private _strDescripcionotroconcepto As String = String.Empty
    Private _strNumSecuenciaAutorizacion As String = String.Empty
    Private _intNumSecuencia As Nullable(Of Integer) = 0
    Private _strUsuario As String = String.Empty

    Private _strCodUnico As String = String.Empty
    Private _strFechaRegistroInicio As String = String.Empty
    Private _strFechaRegistroFin As String = String.Empty
    Private _strRazonSocial As String = String.Empty
    Private _strTipoContrato As String = String.Empty
#End Region

#Region " Propiedades "

    <XmlElement("Codsolicitudcredito")> _
    Public Property Codsolicitudcredito() As String
        Get
            Return Me._strCodsolicitudcredito
        End Get
        Set(ByVal value As String)
            Me._strCodsolicitudcredito = value
        End Set
    End Property

    <XmlElement("Secfinanciamiento")> _
    Public Property Secfinanciamiento() As Integer
        Get
            Return Me._intSecfinanciamiento
        End Get
        Set(ByVal value As Integer)
            Me._intSecfinanciamiento = value
        End Set
    End Property

    <XmlElement("Codigootroconcepto")> _
    Public Property Codigootroconcepto() As Nullable(Of Integer)
        Get
            Return Me._intCodigootroconcepto
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodigootroconcepto = value
        End Set
    End Property

    <XmlElement("Codigooperacion")> _
    Public Property Codigooperacion() As String
        Get
            Return Me._strCodigooperacion
        End Get
        Set(ByVal value As String)
            Me._strCodigooperacion = value
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

    <XmlElement("Importe")> _
    Public Property Importe() As Decimal
        Get
            Return Me._decImporte
        End Get
        Set(ByVal value As Decimal)
            Me._decImporte = value
        End Set
    End Property

    <XmlElement("Importeigv")> _
    Public Property Importeigv() As Decimal
        Get
            Return Me._decImporteigv
        End Get
        Set(ByVal value As Decimal)
            Me._decImporteigv = value
        End Set
    End Property

    <XmlElement("Codigoestadopago")> _
    Public Property Codigoestadopago() As String
        Get
            Return Me._strCodigoestadopago
        End Get
        Set(ByVal value As String)
            Me._strCodigoestadopago = value
        End Set
    End Property

    <XmlElement("Fechapago")> _
    Public Property Fechapago() As DateTime
        Get
            Return Me._dtmFechapago
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechapago = value
        End Set
    End Property

    <XmlElement("Codigotipocuenta")> _
    Public Property Codigotipocuenta() As String
        Get
            Return Me._strCodigotipocuenta
        End Get
        Set(ByVal value As String)
            Me._strCodigotipocuenta = value
        End Set
    End Property

    <XmlElement("Cuentacargo")> _
    Public Property Cuentacargo() As String
        Get
            Return Me._strCuentacargo
        End Get
        Set(ByVal value As String)
            Me._strCuentacargo = value
        End Set
    End Property

    <XmlElement("Codigoestadocobro")> _
    Public Property Codigoestadocobro() As String
        Get
            Return Me._strCodigoestadocobro
        End Get
        Set(ByVal value As String)
            Me._strCodigoestadocobro = value
        End Set
    End Property

    <XmlElement("Fechacobro")> _
    Public Property Fechacobro() As DateTime
        Get
            Return Me._dtmFechacobro
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechacobro = value
        End Set
    End Property

    <XmlElement("Codigomoneda")> _
    Public Property Codigomoneda() As String
        Get
            Return Me._strCodigomoneda
        End Get
        Set(ByVal value As String)
            Me._strCodigomoneda = value
        End Set
    End Property

    <XmlElement("Codigootroconcepto2")> _
    Public Property Codigootroconcepto2() As String
        Get
            Return Me._strCodigootroconcepto2
        End Get
        Set(ByVal value As String)
            Me._strCodigootroconcepto2 = value
        End Set
    End Property
    <XmlElement("Descripcionotroconcepto")> _
   Public Property Descripcionotroconcepto() As String
        Get
            Return Me._strDescripcionotroconcepto
        End Get
        Set(ByVal value As String)
            Me._strDescripcionotroconcepto = value
        End Set
    End Property

    <XmlElement("NumSecuenciaAutorizacion")> _
  Public Property NumSecuenciaAutorizacion() As String
        Get
            Return Me._strNumSecuenciaAutorizacion
        End Get
        Set(ByVal value As String)
            Me._strNumSecuenciaAutorizacion = value
        End Set
    End Property
    <XmlElement("NumSecuencia")> _
        Public Property NumSecuencia() As Integer
        Get
            Return Me._intNumSecuencia
        End Get
        Set(ByVal value As Integer)
            Me._intNumSecuencia = value
        End Set
    End Property
    <XmlElement("CodUsuario")> _
    Public Property CodUsuario() As String
        Get
            Return Me._strUsuario
        End Get
        Set(ByVal value As String)
            Me._strUsuario = value
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
    <XmlElement("FechaRegistroInicio")> _
      Public Property FechaRegistroInicio() As String
        Get
            Return Me._strFechaRegistroInicio
        End Get
        Set(ByVal value As String)
            Me._strFechaRegistroInicio = value
        End Set
    End Property
    <XmlElement("FechaRegistroFin")> _
     Public Property FechaRegistroFin() As String
        Get
            Return Me._strFechaRegistroFin
        End Get
        Set(ByVal value As String)
            Me._strFechaRegistroFin = value
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
    <XmlElement("TipoContrato")> _
       Public Property TipoContrato() As String
        Get
            Return Me._strTipoContrato
        End Get
        Set(ByVal value As String)
            Me._strTipoContrato = value
        End Set
    End Property
#End Region

End Class
