
Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad CreditoRecuperacionComision
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 27/11/2012 03:02:49 p.m.
''' </remarks>
<Serializable(), XmlRoot("ECreditoRecuperacionComision")> _
Public Class ECreditoRecuperacionComision

#Region " Atributos "

    Private _intItem As Integer = 0
    Private _strCodOperacionActiva As String = String.Empty
    Private _strTipoRubroFinanciamiento As String = String.Empty
    Private _strCodIfi As String = String.Empty
    Private _strTipoRecuperacion As String = String.Empty
    Private _shtNumSecRecuperacion As Short = 0
    Private _strCodComisionTipo As String = String.Empty
    Private _strTipoValorComision As String = String.Empty
    Private _strTipoAplicacionComision As String = String.Empty
    Private _shtNumSecRecupComi As Short = 0
    Private _decPorcenComision As Decimal = 0
    Private _decMontoComision As Decimal = 0
    Private _strCodModuloOperacion As String = String.Empty
    Private _strTipoOperacion As String = String.Empty
    Private _dtmFechaRegistro As DateTime = New DateTime(1900, 1, 1)
    Private _strCodUsuario As String = String.Empty
    Private _strTextoAudiCreacion As String = String.Empty
    Private _strTextoAudiModi As String = String.Empty
    Private _decMontoIGV As Decimal = 0
    Private _decMontoReajusteComision As Decimal = 0
    Private _decMontoReajusteIGV As Decimal = 0
    Private _decMontoReembolso As Decimal = 0
    Private _decMontoIGVReembolso As Decimal = 0

    Private _strNumeroLote As String = String.Empty
    Private _strRazonSocial As String = String.Empty
    Private _strCodigoTipoDocumento As String = String.Empty
    Private _strNumeroDocumento As String = String.Empty
    Private _strCodigoConcepto As String = String.Empty
    Private _strCodigoUnico As String = String.Empty
    Private _strFlagIndividual As String = String.Empty
    Private _strFlagRegistro As String = String.Empty
    Private _strEstadoPago As String = String.Empty
    Private _strEstadoCobro As String = String.Empty
    Private _strCodMoneda As String = String.Empty
    Private _dtmFechaCobro As DateTime = New DateTime(1900, 1, 1)
    Private _intNumeroSecuencia As Integer = 0
    Private _strObservaciones As String = String.Empty

    Private _strTipoDocumento As String = String.Empty
    Private _strEstadoContrato As String = String.Empty
    Private _decTotal As Decimal = 0
    Private _strEstadoRecuperacion As String = String.Empty
    Private _strStringFechaCobro As String = String.Empty
    Private _strStringFechaPago As String = String.Empty

    Private _strAnterior As String = String.Empty
    Private _strActual As String = String.Empty
    Private _strSiguiente As String = String.Empty
    Private _intTotalRegistros As Integer = 0
    Private _lngIndicadorRegistro As Long = 0
    Private _strStringFechaActivacion As String = String.Empty
    Private _strStringFechaVencimientoOperacion As String = String.Empty
    Private _intCantidadFraccionar As Integer = 0

    'Inicio IBK RPR
    Private _strCodigoLiquidacion As String = String.Empty
    Private _intNumCuotaCalendario As Integer = 0
    Private _intAplicacion As Integer = 1
    'Fin IBK

#End Region


#Region " Propiedades "
    <XmlElement("Item")> _
    Public Property Item() As Integer
        Get
            Return _intItem
        End Get
        Set(ByVal Value As Integer)
            _intItem = Value
        End Set
    End Property

    <XmlElement("CodOperacionActiva")> _
    Public Property CodOperacionActiva() As String
        Get
            Return _strCodOperacionActiva
        End Get
        Set(ByVal Value As String)
            _strCodOperacionActiva = Value
        End Set
    End Property


    <XmlElement("TipoRubroFinanciamiento")> _
    Public Property TipoRubroFinanciamiento() As String
        Get
            Return _strTipoRubroFinanciamiento
        End Get
        Set(ByVal Value As String)
            _strTipoRubroFinanciamiento = Value
        End Set
    End Property


    <XmlElement("CodIfi")> _
    Public Property CodIfi() As String
        Get
            Return _strCodIfi
        End Get
        Set(ByVal Value As String)
            _strCodIfi = Value
        End Set
    End Property


    <XmlElement("TipoRecuperacion")> _
    Public Property TipoRecuperacion() As String
        Get
            Return _strTipoRecuperacion
        End Get
        Set(ByVal Value As String)
            _strTipoRecuperacion = Value
        End Set
    End Property


    <XmlElement("NumSecRecuperacion")> _
    Public Property NumSecRecuperacion() As Short
        Get
            Return _shtNumSecRecuperacion
        End Get
        Set(ByVal Value As Short)
            _shtNumSecRecuperacion = Value
        End Set
    End Property


    <XmlElement("CodComisionTipo")> _
    Public Property CodComisionTipo() As String
        Get
            Return _strCodComisionTipo
        End Get
        Set(ByVal Value As String)
            _strCodComisionTipo = Value
        End Set
    End Property


    <XmlElement("TipoValorComision")> _
    Public Property TipoValorComision() As String
        Get
            Return _strTipoValorComision
        End Get
        Set(ByVal Value As String)
            _strTipoValorComision = Value
        End Set
    End Property


    <XmlElement("TipoAplicacionComision")> _
    Public Property TipoAplicacionComision() As String
        Get
            Return _strTipoAplicacionComision
        End Get
        Set(ByVal Value As String)
            _strTipoAplicacionComision = Value
        End Set
    End Property


    <XmlElement("NumSecRecupComi")> _
    Public Property NumSecRecupComi() As Short
        Get
            Return _shtNumSecRecupComi
        End Get
        Set(ByVal Value As Short)
            _shtNumSecRecupComi = Value
        End Set
    End Property


    <XmlElement("PorcenComision")> _
    Public Property PorcenComision() As Decimal
        Get
            Return _decPorcenComision
        End Get
        Set(ByVal Value As Decimal)
            _decPorcenComision = Value
        End Set
    End Property


    <XmlElement("MontoComision")> _
    Public Property MontoComision() As Decimal
        Get
            Return _decMontoComision
        End Get
        Set(ByVal Value As Decimal)
            _decMontoComision = Value
        End Set
    End Property


    <XmlElement("CodModuloOperacion")> _
    Public Property CodModuloOperacion() As String
        Get
            Return _strCodModuloOperacion
        End Get
        Set(ByVal Value As String)
            _strCodModuloOperacion = Value
        End Set
    End Property


    <XmlElement("TipoOperacion")> _
    Public Property TipoOperacion() As String
        Get
            Return _strTipoOperacion
        End Get
        Set(ByVal Value As String)
            _strTipoOperacion = Value
        End Set
    End Property


    <XmlElement("FechaRegistro")> _
    Public Property FechaRegistro() As DateTime
        Get
            Return _dtmFechaRegistro
        End Get
        Set(ByVal Value As DateTime)
            _dtmFechaRegistro = Value
        End Set
    End Property


    <XmlElement("CodUsuario")> _
    Public Property CodUsuario() As String
        Get
            Return _strCodUsuario
        End Get
        Set(ByVal Value As String)
            _strCodUsuario = Value
        End Set
    End Property


    <XmlElement("TextoAudiCreacion")> _
    Public Property TextoAudiCreacion() As String
        Get
            Return _strTextoAudiCreacion
        End Get
        Set(ByVal Value As String)
            _strTextoAudiCreacion = Value
        End Set
    End Property


    <XmlElement("TextoAudiModi")> _
    Public Property TextoAudiModi() As String
        Get
            Return _strTextoAudiModi
        End Get
        Set(ByVal Value As String)
            _strTextoAudiModi = Value
        End Set
    End Property


    <XmlElement("MontoIGV")> _
    Public Property MontoIGV() As Decimal
        Get
            Return _decMontoIGV
        End Get
        Set(ByVal Value As Decimal)
            _decMontoIGV = Value
        End Set
    End Property


    <XmlElement("MontoReajusteComision")> _
    Public Property MontoReajusteComision() As Decimal
        Get
            Return _decMontoReajusteComision
        End Get
        Set(ByVal Value As Decimal)
            _decMontoReajusteComision = Value
        End Set
    End Property


    <XmlElement("MontoReajusteIGV")> _
    Public Property MontoReajusteIGV() As Decimal
        Get
            Return _decMontoReajusteIGV
        End Get
        Set(ByVal Value As Decimal)
            _decMontoReajusteIGV = Value
        End Set
    End Property


    <XmlElement("MontoReembolso")> _
    Public Property MontoReembolso() As Decimal
        Get
            Return _decMontoReembolso
        End Get
        Set(ByVal Value As Decimal)
            _decMontoReembolso = Value
        End Set
    End Property


    <XmlElement("MontoIGVReembolso")> _
    Public Property MontoIGVReembolso() As Decimal
        Get
            Return _decMontoIGVReembolso
        End Get
        Set(ByVal Value As Decimal)
            _decMontoIGVReembolso = Value
        End Set
    End Property

    <XmlElement("NumeroLote")> _
    Public Property NumeroLote() As String
        Get
            Return _strNumeroLote
        End Get
        Set(ByVal Value As String)
            _strNumeroLote = Value
        End Set
    End Property

    <XmlElement("RazonSocial")> _
     Public Property RazonSocial() As String
        Get
            Return _strRazonSocial
        End Get
        Set(ByVal Value As String)
            _strRazonSocial = Value
        End Set
    End Property

    <XmlElement("CodigoTipoDocumento")> _
     Public Property CodigoTipoDocumento() As String
        Get
            Return _strCodigoTipoDocumento
        End Get
        Set(ByVal Value As String)
            _strCodigoTipoDocumento = Value
        End Set
    End Property

    <XmlElement("NumeroDocumento")> _
     Public Property NumeroDocumento() As String
        Get
            Return _strNumeroDocumento
        End Get
        Set(ByVal Value As String)
            _strNumeroDocumento = Value
        End Set
    End Property

    <XmlElement("CodigoUnico")> _
     Public Property CodigoUnico() As String
        Get
            Return _strCodigoUnico
        End Get
        Set(ByVal Value As String)
            _strCodigoUnico = Value
        End Set
    End Property

    <XmlElement("FlagIndividual")> _
     Public Property FlagIndividual() As String
        Get
            Return _strFlagIndividual
        End Get
        Set(ByVal Value As String)
            _strFlagIndividual = Value
        End Set
    End Property

    <XmlElement("FlagRegistro")> _
     Public Property FlagRegistro() As String
        Get
            Return _strFlagRegistro
        End Get
        Set(ByVal Value As String)
            _strFlagRegistro = Value
        End Set
    End Property

    <XmlElement("EstadoPago")> _
     Public Property EstadoPago() As String
        Get
            Return _strEstadoPago
        End Get
        Set(ByVal Value As String)
            _strEstadoPago = Value
        End Set
    End Property

    <XmlElement("EstadoCobro")> _
     Public Property EstadoCobro() As String
        Get
            Return _strEstadoCobro
        End Get
        Set(ByVal Value As String)
            _strEstadoCobro = Value
        End Set
    End Property

    <XmlElement("CodMoneda")> _
     Public Property CodMoneda() As String
        Get
            Return _strCodMoneda
        End Get
        Set(ByVal Value As String)
            _strCodMoneda = Value
        End Set
    End Property

    <XmlElement("FechaCobro")> _
     Public Property FechaCobro() As DateTime
        Get
            Return _dtmFechaCobro
        End Get
        Set(ByVal Value As DateTime)
            _dtmFechaCobro = Value
        End Set
    End Property

    <XmlElement("NumeroSecuencia")> _
     Public Property NumeroSecuencia() As Integer
        Get
            Return _intNumeroSecuencia
        End Get
        Set(ByVal Value As Integer)
            _intNumeroSecuencia = Value
        End Set
    End Property

    <XmlElement("Observaciones")> _
     Public Property Observaciones() As String
        Get
            Return _strObservaciones
        End Get
        Set(ByVal Value As String)
            _strObservaciones = Value
        End Set
    End Property

    <XmlElement("Anterior")> _
     Public Property Anterior() As String
        Get
            Return _strAnterior
        End Get
        Set(ByVal Value As String)
            _strAnterior = Value
        End Set
    End Property

    <XmlElement("Actual")> _
     Public Property Actual() As String
        Get
            Return _strActual
        End Get
        Set(ByVal Value As String)
            _strActual = Value
        End Set
    End Property

    <XmlElement("Siguiente")> _
     Public Property Siguiente() As String
        Get
            Return _strSiguiente
        End Get
        Set(ByVal Value As String)
            _strSiguiente = Value
        End Set
    End Property

    <XmlElement("TipoDocumento")> _
    Public Property TipoDocumento() As String
        Get
            Return _strTipoDocumento
        End Get
        Set(ByVal Value As String)
            _strTipoDocumento = Value
        End Set
    End Property

    <XmlElement("EstadoContrato")> _
    Public Property EstadoContrato() As String
        Get
            Return _strEstadoContrato
        End Get
        Set(ByVal Value As String)
            _strEstadoContrato = Value
        End Set
    End Property

    <XmlElement("EstadoRecuperacion")> _
    Public Property EstadoRecuperacion() As String
        Get
            Return _strEstadoRecuperacion
        End Get
        Set(ByVal Value As String)
            _strEstadoRecuperacion = Value
        End Set
    End Property

    <XmlElement("Total")> _
    Public Property Total() As Decimal
        Get
            Return _decTotal
        End Get
        Set(ByVal Value As Decimal)
            _decTotal = Value
        End Set
    End Property

    <XmlElement("StringFechaCobro")> _
    Public Property StringFechaCobro() As String
        Get
            Return _strStringFechaCobro
        End Get
        Set(ByVal Value As String)
            _strStringFechaCobro = Value
        End Set
    End Property

    <XmlElement("StringFechaPago")> _
    Public Property StringFechaPago() As String
        Get
            Return _strStringFechaPago
        End Get
        Set(ByVal Value As String)
            _strStringFechaPago = Value
        End Set
    End Property

    <XmlElement("TotalRegistros")> _
    Public Property TotalRegistros() As Integer
        Get
            Return _intTotalRegistros
        End Get
        Set(ByVal Value As Integer)
            _intTotalRegistros = Value
        End Set
    End Property

    <XmlElement("IndicadorRegistro")> _
    Public Property IndicadorRegistro() As Integer
        Get
            Return _lngIndicadorRegistro
        End Get
        Set(ByVal Value As Integer)
            _lngIndicadorRegistro = Value
        End Set
    End Property

    <XmlElement("StringFechaActivacion")> _
    Public Property StringFechaActivacion() As String
        Get
            Return _strStringFechaActivacion
        End Get
        Set(ByVal Value As String)
            _strStringFechaActivacion = Value
        End Set
    End Property

    <XmlElement("StringFechaVencimientoOperacion")> _
    Public Property StringFechaVencimientoOperacion() As String
        Get
            Return _strStringFechaVencimientoOperacion
        End Get
        Set(ByVal Value As String)
            _strStringFechaVencimientoOperacion = Value
        End Set
    End Property

    <XmlElement("CantidadFraccionar")> _
    Public Property CantidadFraccionar() As Integer
        Get
            Return _intCantidadFraccionar
        End Get
        Set(ByVal Value As Integer)
            _intCantidadFraccionar = Value
        End Set
    End Property

    'Inicio IBK RPR
    <XmlElement("Aplicacion")> _
    Public Property Aplicacion() As Integer
        Get
            Return _intAplicacion
        End Get
        Set(ByVal Value As Integer)
            _intAplicacion = Value
        End Set
    End Property

    <XmlElement("CodigoLiquidacion")> _
    Public Property CodigoLiquidacion() As String
        Get
            Return _strCodigoLiquidacion
        End Get
        Set(ByVal Value As String)
            _strCodigoLiquidacion = Value
        End Set
    End Property

    <XmlElement("NumCuotaCalendario")> _
    Public Property NumCuotaCalendario() As String
        Get
            Return _intNumCuotaCalendario
        End Get
        Set(ByVal Value As String)
            _intNumCuotaCalendario = Value
        End Set
    End Property
    'Fin IBK

#End Region

End Class



