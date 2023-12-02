
Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad CreditoRecuperacion
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 27/11/2012 03:02:49 p.m.
''' </remarks>
<Serializable(), XmlRoot("ECreditoRecuperacion")> _
Public Class ECreditoRecuperacion

#Region " Atributos "

    Private _intItem As Integer = 0
    Private _strCodOperacionActiva As String = String.Empty
    Private _strTipoRubroFinanciamiento As String = String.Empty
    Private _strCodIfi As String = String.Empty
    Private _strTipoRecuperacion As String = String.Empty
    Private _shtNumSecRecuperacion As Short = 0
    Private _dtmFechaRecuperacion As DateTime = New DateTime(1900, 1, 1)
    Private _strCodMoneda As String = String.Empty
    Private _decMontoPrincipal As Decimal = 0
    Private _decMontoInteres As Decimal = 0
    Private _decMontoComision As Decimal = 0
    Private _decMontoIGV As Decimal = 0
    'IBK RPR
    Private _decMontoReembolso As Decimal = 0
    Private _decMontoIGVReembolso As Decimal = 0
    Private _strMotivoAnulacionExtorno As String = String.Empty
    Private _strGlosaConceptoComprobante As String = String.Empty
    'FIN
    Private _decMontoPrincipalSeguro As Decimal = 0
    Private _decMontoInteresSeguro As Decimal = 0
    Private _decMontoMora As Decimal = 0
    Private _decMontoRecuperacion As Decimal = 0
    Private _decMontoTotalConceptos As Decimal = 0
    Private _decMontoAFavor As Decimal = 0
    Private _decMontoRecuperacionNeto As Decimal = 0
    Private _decMontoTotalRecuperado As Decimal = 0
    Private _decMontoInteresCompensatorio As Decimal = 0
    Private _strTipoViaCobranza As String = String.Empty
    Private _strTipoCuenta As String = String.Empty
    Private _strNroCuenta As String = String.Empty
    Private _decCodAutorizacionRecuperacion As Decimal = 0
    Private _decCodAutorizacionRecuperacionExt As Decimal = 0
    Private _dtmFechaValorRecuperacion As DateTime = New DateTime(1900, 1, 1)
    Private _strTipoPrepago As String = String.Empty
    Private _strTipoAplicacionPrepago As String = String.Empty
    Private _strTipoPrelacion As String = String.Empty
    Private _strEstadoRecuperacion As String = String.Empty
    Private _strEstadoEjecucionPrepago As String = String.Empty
    Private _strCodModuloOperacion As String = String.Empty
    Private _strTipoOperacion As String = String.Empty
    Private _dtmFechaRegistro As DateTime = New DateTime(1900, 1, 1)
    Private _strCodUsuario As String = String.Empty
    Private _strTextoAudiCreacion As String = String.Empty
    Private _strTextoAudiModi As String = String.Empty
    Private _strDescripcionCargo As String = String.Empty
    Private _strCodOperacionGINA As String = String.Empty
    Private _dtmFechaProcesoPago As DateTime = New DateTime(1900, 1, 1)
    Private _strCodTiendaPago As String = String.Empty
    Private _strCodTerminalPago As String = String.Empty
    Private _strCodUsuarioPago As String = String.Empty
    Private _strCodModoPago As String = String.Empty
    Private _strCodModoPago2 As String = String.Empty
    Private _strTipoExtorno As String = String.Empty
    Private _strCodMonedaCargo As String = String.Empty
    Private _strIndFechaValor As String = String.Empty
    Private _decMontoIntDiferidoPagado As Decimal = 0
    Private _decSaldoInteresRef As Decimal = 0
    Private _dtmFechaVac As DateTime = New DateTime(1900, 1, 1)
    Private _decIndiceVAC As Decimal = 0
    Private _decMontoAgregadoPagado As Decimal = 0
    Private _decSaldoAgregado As Decimal = 0
    Private _strConceptoAdministrativo As String = String.Empty
    'IBK RPR 14/01/2013
    Private _strCodigoMovimientoBasilea As String = String.Empty
    Private _strFlagCuentaPropia As String = String.Empty
    Private _strCodUnicoClienteCargo As String = String.Empty
    Private _strCodigoConcepto As String = String.Empty

    Private _strCodError As String = String.Empty
    Private _strMsgError As String = String.Empty
    'FIN
    Private _intNumeroSecuencia As Integer = 0
    Private _strFlagIndividual As String = String.Empty

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


    <XmlElement("FechaRecuperacion")> _
    Public Property FechaRecuperacion() As DateTime
        Get
            Return _dtmFechaRecuperacion
        End Get
        Set(ByVal Value As DateTime)
            _dtmFechaRecuperacion = Value
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


    <XmlElement("MontoPrincipal")> _
    Public Property MontoPrincipal() As Decimal
        Get
            Return _decMontoPrincipal
        End Get
        Set(ByVal Value As Decimal)
            _decMontoPrincipal = Value
        End Set
    End Property


    <XmlElement("MontoInteres")> _
    Public Property MontoInteres() As Decimal
        Get
            Return _decMontoInteres
        End Get
        Set(ByVal Value As Decimal)
            _decMontoInteres = Value
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


    <XmlElement("MontoIGV")> _
    Public Property MontoIGV() As Decimal
        Get
            Return _decMontoIGV
        End Get
        Set(ByVal Value As Decimal)
            _decMontoIGV = Value
        End Set
    End Property


    <XmlElement("MontoPrincipalSeguro")> _
    Public Property MontoPrincipalSeguro() As Decimal
        Get
            Return _decMontoPrincipalSeguro
        End Get
        Set(ByVal Value As Decimal)
            _decMontoPrincipalSeguro = Value
        End Set
    End Property


    <XmlElement("MontoInteresSeguro")> _
    Public Property MontoInteresSeguro() As Decimal
        Get
            Return _decMontoInteresSeguro
        End Get
        Set(ByVal Value As Decimal)
            _decMontoInteresSeguro = Value
        End Set
    End Property


    <XmlElement("MontoMora")> _
    Public Property MontoMora() As Decimal
        Get
            Return _decMontoMora
        End Get
        Set(ByVal Value As Decimal)
            _decMontoMora = Value
        End Set
    End Property


    <XmlElement("MontoRecuperacion")> _
    Public Property MontoRecuperacion() As Decimal
        Get
            Return _decMontoRecuperacion
        End Get
        Set(ByVal Value As Decimal)
            _decMontoRecuperacion = Value
        End Set
    End Property


    <XmlElement("MontoTotalConceptos")> _
    Public Property MontoTotalConceptos() As Decimal
        Get
            Return _decMontoTotalConceptos
        End Get
        Set(ByVal Value As Decimal)
            _decMontoTotalConceptos = Value
        End Set
    End Property


    <XmlElement("MontoAFavor")> _
    Public Property MontoAFavor() As Decimal
        Get
            Return _decMontoAFavor
        End Get
        Set(ByVal Value As Decimal)
            _decMontoAFavor = Value
        End Set
    End Property


    <XmlElement("MontoRecuperacionNeto")> _
    Public Property MontoRecuperacionNeto() As Decimal
        Get
            Return _decMontoRecuperacionNeto
        End Get
        Set(ByVal Value As Decimal)
            _decMontoRecuperacionNeto = Value
        End Set
    End Property


    <XmlElement("MontoTotalRecuperado")> _
    Public Property MontoTotalRecuperado() As Decimal
        Get
            Return _decMontoTotalRecuperado
        End Get
        Set(ByVal Value As Decimal)
            _decMontoTotalRecuperado = Value
        End Set
    End Property


    <XmlElement("MontoInteresCompensatorio")> _
    Public Property MontoInteresCompensatorio() As Decimal
        Get
            Return _decMontoInteresCompensatorio
        End Get
        Set(ByVal Value As Decimal)
            _decMontoInteresCompensatorio = Value
        End Set
    End Property


    <XmlElement("TipoViaCobranza")> _
    Public Property TipoViaCobranza() As String
        Get
            Return _strTipoViaCobranza
        End Get
        Set(ByVal Value As String)
            _strTipoViaCobranza = Value
        End Set
    End Property


    <XmlElement("TipoCuenta")> _
    Public Property TipoCuenta() As String
        Get
            Return _strTipoCuenta
        End Get
        Set(ByVal Value As String)
            _strTipoCuenta = Value
        End Set
    End Property


    <XmlElement("NroCuenta")> _
    Public Property NroCuenta() As String
        Get
            Return _strNroCuenta
        End Get
        Set(ByVal Value As String)
            _strNroCuenta = Value
        End Set
    End Property


    <XmlElement("CodAutorizacionRecuperacion")> _
    Public Property CodAutorizacionRecuperacion() As Decimal
        Get
            Return _decCodAutorizacionRecuperacion
        End Get
        Set(ByVal Value As Decimal)
            _decCodAutorizacionRecuperacion = Value
        End Set
    End Property


    <XmlElement("CodAutorizacionRecuperacionExt")> _
    Public Property CodAutorizacionRecuperacionExt() As Decimal
        Get
            Return _decCodAutorizacionRecuperacionExt
        End Get
        Set(ByVal Value As Decimal)
            _decCodAutorizacionRecuperacionExt = Value
        End Set
    End Property


    <XmlElement("FechaValorRecuperacion")> _
    Public Property FechaValorRecuperacion() As DateTime
        Get
            Return _dtmFechaValorRecuperacion
        End Get
        Set(ByVal Value As DateTime)
            _dtmFechaValorRecuperacion = Value
        End Set
    End Property


    <XmlElement("TipoPrepago")> _
    Public Property TipoPrepago() As String
        Get
            Return _strTipoPrepago
        End Get
        Set(ByVal Value As String)
            _strTipoPrepago = Value
        End Set
    End Property


    <XmlElement("TipoAplicacionPrepago")> _
    Public Property TipoAplicacionPrepago() As String
        Get
            Return _strTipoAplicacionPrepago
        End Get
        Set(ByVal Value As String)
            _strTipoAplicacionPrepago = Value
        End Set
    End Property


    <XmlElement("TipoPrelacion")> _
    Public Property TipoPrelacion() As String
        Get
            Return _strTipoPrelacion
        End Get
        Set(ByVal Value As String)
            _strTipoPrelacion = Value
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


    <XmlElement("EstadoEjecucionPrepago")> _
    Public Property EstadoEjecucionPrepago() As String
        Get
            Return _strEstadoEjecucionPrepago
        End Get
        Set(ByVal Value As String)
            _strEstadoEjecucionPrepago = Value
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


    <XmlElement("DescripcionCargo")> _
    Public Property DescripcionCargo() As String
        Get
            Return _strDescripcionCargo
        End Get
        Set(ByVal Value As String)
            _strDescripcionCargo = Value
        End Set
    End Property


    <XmlElement("CodOperacionGINA")> _
    Public Property CodOperacionGINA() As String
        Get
            Return _strCodOperacionGINA
        End Get
        Set(ByVal Value As String)
            _strCodOperacionGINA = Value
        End Set
    End Property


    <XmlElement("FechaProcesoPago")> _
    Public Property FechaProcesoPago() As DateTime
        Get
            Return _dtmFechaProcesoPago
        End Get
        Set(ByVal Value As DateTime)
            _dtmFechaProcesoPago = Value
        End Set
    End Property


    <XmlElement("CodTiendaPago")> _
    Public Property CodTiendaPago() As String
        Get
            Return _strCodTiendaPago
        End Get
        Set(ByVal Value As String)
            _strCodTiendaPago = Value
        End Set
    End Property


    <XmlElement("CodTerminalPago")> _
    Public Property CodTerminalPago() As String
        Get
            Return _strCodTerminalPago
        End Get
        Set(ByVal Value As String)
            _strCodTerminalPago = Value
        End Set
    End Property


    <XmlElement("CodUsuarioPago")> _
    Public Property CodUsuarioPago() As String
        Get
            Return _strCodUsuarioPago
        End Get
        Set(ByVal Value As String)
            _strCodUsuarioPago = Value
        End Set
    End Property


    <XmlElement("CodModoPago")> _
    Public Property CodModoPago() As String
        Get
            Return _strCodModoPago
        End Get
        Set(ByVal Value As String)
            _strCodModoPago = Value
        End Set
    End Property


    <XmlElement("CodModoPago2")> _
    Public Property CodModoPago2() As String
        Get
            Return _strCodModoPago2
        End Get
        Set(ByVal Value As String)
            _strCodModoPago2 = Value
        End Set
    End Property


    <XmlElement("TipoExtorno")> _
    Public Property TipoExtorno() As String
        Get
            Return _strTipoExtorno
        End Get
        Set(ByVal Value As String)
            _strTipoExtorno = Value
        End Set
    End Property


    <XmlElement("CodMonedaCargo")> _
    Public Property CodMonedaCargo() As String
        Get
            Return _strCodMonedaCargo
        End Get
        Set(ByVal Value As String)
            _strCodMonedaCargo = Value
        End Set
    End Property


    <XmlElement("IndFechaValor")> _
    Public Property IndFechaValor() As String
        Get
            Return _strIndFechaValor
        End Get
        Set(ByVal Value As String)
            _strIndFechaValor = Value
        End Set
    End Property


    <XmlElement("MontoIntDiferidoPagado")> _
    Public Property MontoIntDiferidoPagado() As Decimal
        Get
            Return _decMontoIntDiferidoPagado
        End Get
        Set(ByVal Value As Decimal)
            _decMontoIntDiferidoPagado = Value
        End Set
    End Property


    <XmlElement("SaldoInteresRef")> _
    Public Property SaldoInteresRef() As Decimal
        Get
            Return _decSaldoInteresRef
        End Get
        Set(ByVal Value As Decimal)
            _decSaldoInteresRef = Value
        End Set
    End Property


    <XmlElement("FechaVac")> _
    Public Property FechaVac() As DateTime
        Get
            Return _dtmFechaVac
        End Get
        Set(ByVal Value As DateTime)
            _dtmFechaVac = Value
        End Set
    End Property


    <XmlElement("IndiceVAC")> _
    Public Property IndiceVAC() As Decimal
        Get
            Return _decIndiceVAC
        End Get
        Set(ByVal Value As Decimal)
            _decIndiceVAC = Value
        End Set
    End Property


    <XmlElement("MontoAgregadoPagado")> _
    Public Property MontoAgregadoPagado() As Decimal
        Get
            Return _decMontoAgregadoPagado
        End Get
        Set(ByVal Value As Decimal)
            _decMontoAgregadoPagado = Value
        End Set
    End Property


    <XmlElement("SaldoAgregado")> _
    Public Property SaldoAgregado() As Decimal
        Get
            Return _decSaldoAgregado
        End Get
        Set(ByVal Value As Decimal)
            _decSaldoAgregado = Value
        End Set
    End Property


    <XmlElement("ConceptoAdministrativo")> _
    Public Property ConceptoAdministrativo() As String
        Get
            Return _strConceptoAdministrativo
        End Get
        Set(ByVal Value As String)
            _strConceptoAdministrativo = Value
        End Set
    End Property

    <XmlElement("CodigoMovimientoBasilea")> _
    Public Property CodigoMovimientoBasilea() As String
        Get
            Return _strCodigoMovimientoBasilea
        End Get
        Set(ByVal Value As String)
            _strCodigoMovimientoBasilea = Value
        End Set
    End Property

    <XmlElement("FlagCuentaPropia")> _
   Public Property FlagCuentaPropia() As String
        Get
            Return _strFlagCuentaPropia
        End Get
        Set(ByVal Value As String)
            _strFlagCuentaPropia = Value
        End Set
    End Property

    <XmlElement("CodUnicoClienteCargo")> _
   Public Property CodUnicoClienteCargo() As String
        Get
            Return _strCodUnicoClienteCargo
        End Get
        Set(ByVal Value As String)
            _strCodUnicoClienteCargo = Value
        End Set
    End Property

    <XmlElement("CodigoConcepto")> _
    Public Property CodigoConcepto() As String
        Get
            Return _strCodigoConcepto
        End Get
        Set(ByVal Value As String)
            _strCodigoConcepto = Value
        End Set
    End Property

    <XmlElement("CodError")> _
    Public Property CodError() As String
        Get
            Return _strCodError
        End Get
        Set(ByVal value As String)
            _strCodError = value
        End Set
    End Property

    <XmlElement("MsgError")> _
    Public Property MsgError() As String
        Get
            Return _strMsgError
        End Get
        Set(ByVal value As String)
            _strMsgError = value
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


    <XmlElement("FlagIndividual")> _
    Public Property FlagIndividual() As String
        Get
            Return _strFlagIndividual
        End Get
        Set(ByVal Value As String)
            _strFlagIndividual = Value
        End Set
    End Property

    'IBK RPR
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

    <XmlElement("MotivoAnulacionExtorno")> _
    Public Property MotivoAnulacionExtorno() As String
        Get
            Return _strMotivoAnulacionExtorno
        End Get
        Set(ByVal Value As String)
            _strMotivoAnulacionExtorno = Value
        End Set
    End Property

    <XmlElement("GlosaConceptoComprobante")> _
    Public Property GlosaConceptoComprobante() As String
        Get
            Return _strGlosaConceptoComprobante
        End Get
        Set(ByVal Value As String)
            _strGlosaConceptoComprobante = Value
        End Set
    End Property
    'FIN

#End Region

End Class



