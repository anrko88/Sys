Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

<Serializable(), XmlRoot("EGCC_Liquidacion")> _
Public Class EGCC_Liquidacion

#Region " Atributos "

    Private _strCodOperacionActiva As String = String.Empty
    Private _strCodigoLiquidacion As String = String.Empty
    Private _strEstadoLiquidacion As String = String.Empty
    Private _strTipoLiquidacion As String = String.Empty
    Private _dtmFechaProceso As DateTime = New DateTime(1900, 1, 1)
    Private _dtmFechaValor As DateTime = New DateTime(1900, 1, 1)
    Private _strCodTipoCambio As String = String.Empty
    Private _decTipoCambio As Decimal = 0
    Private _decPorcIGV As Decimal
    Private _strTipoCronograma As String
    Private _intNroCuotas As Integer
    Private _strPeriodicidad As String
    Private _strFrecuenciaPago As String
    Private _intPlazoGracia As Integer
    Private _strTipoGracia As String
    Private _dtmFechaPrimerVencimiento As DateTime = New DateTime(1900, 1, 1)
    Private _decAmortizacionCapital As Decimal

    Private _decAmortizacionSeguro As Decimal = 0
    Private _decTeaporc As Nullable(Of Decimal)

    Private _decMontoTotal As Decimal = 0
    Private _decMontoIGV As Decimal = 0
    Private _decValorNeto As Decimal = 0

    Private _strFechaValorInicio As String = String.Empty
    Private _strFechaValorFin As String = String.Empty

    Private _strCodigotipocronograma As String
    Private _intPlazograciacuota As Nullable(Of Integer)
    Private _strCodigotipograciacuota As String
    Private _intNumerocuotas As Nullable(Of Integer)
    Private _strCodigoperiodicidad As String
    Private _strCodigofrecuenciapago As String

    Private _dtmFechaInicioInteres As Nullable(Of DateTime)
    Private _decSaldoCapitalPendiente As Decimal = 0
    Private _decSaldoSeguroPendiente As Decimal = 0
    Private _decInteresCorrido As Decimal = 0
    Private _decInteresCorridoSeguro As Decimal = 0

    Private _strCUCLiente As String = String.Empty
    Private _strRazonSocial As String = String.Empty

    Private _intNumCuotaCalendario As Integer

    Private _strCodError As String = String.Empty
    Private _strMsgError As String = String.Empty

    Private _strFlagOperacion As String = String.Empty
    Private _strFlagAdenda As String = String.Empty
    Private _strMotivoAnulacionExtorno As String = String.Empty
    '--------
    Private _dtmFechaRecuperacion As DateTime = New DateTime(1900, 1, 1)
    Private _strCodMoneda As String = String.Empty
    Private _decMontoPrincipal As Decimal = 0
    Private _decMontoInteres As Decimal = 0
    Private _decMontoComision As Decimal = 0
    'IBK RPR
    Private _decMontoReembolso As Decimal = 0
    Private _decMontoIGVReembolso As Decimal = 0
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
    Private _strTipoPrepago As String = String.Empty
    Private _strTipoAplicacionPrepago As String = String.Empty
    Private _strTipoPrelacion As String = String.Empty

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
    'FIN
    'IBK JJM 10/04/2013
    Private _strNumSecRecuperacion As String = String.Empty
    Private _strTipoRecuperacion As String = String.Empty
    Private str_Aplicacion As Integer = 0

    'Fin
#End Region

#Region " Propiedades "

    <XmlElement("CodOperacionActiva")> _
    Public Property CodOperacionActiva() As String
        Get
            Return _strCodOperacionActiva
        End Get
        Set(ByVal Value As String)
            _strCodOperacionActiva = Value
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

    <XmlElement("FlagOperacion")> _
    Public Property FlagOperacion() As String
        Get
            Return _strFlagOperacion
        End Get
        Set(ByVal value As String)
            _strFlagOperacion = value
        End Set
    End Property

    <XmlElement("FlagAdenda")> _
    Public Property FlagAdenda() As String
        Get
            Return _strFlagAdenda
        End Get
        Set(ByVal value As String)
            _strFlagAdenda = value
        End Set
    End Property

    <XmlElement("MotivoAnulacionExtorno")> _
    Public Property MotivoAnulacionExtorno() As String
        Get
            Return _strMotivoAnulacionExtorno
        End Get
        Set(ByVal value As String)
            _strMotivoAnulacionExtorno = value
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

    <XmlElement("CodTipoCambio")> _
    Public Property CodTipoCambio() As String
        Get
            Return _strCodTipoCambio
        End Get
        Set(ByVal Value As String)
            _strCodTipoCambio = Value
        End Set
    End Property

    <XmlElement("TipoCambio")> _
    Public Property TipoCambio() As Decimal
        Get
            Return _decTipoCambio
        End Get
        Set(ByVal Value As Decimal)
            _decTipoCambio = Value
        End Set
    End Property

    <XmlElement("PorcIGV")> _
    Public Property PorcIGV() As Decimal
        Get
            Return _decPorcIGV
        End Get
        Set(ByVal Value As Decimal)
            _decPorcIGV = Value
        End Set
    End Property

    <XmlElement("TipoCronograma")> _
    Public Property TipoCronograma() As String
        Get
            Return _strTipoCronograma
        End Get
        Set(ByVal Value As String)
            _strTipoCronograma = Value
        End Set
    End Property

    <XmlElement("NroCuotas")> _
    Public Property NroCuotas() As Integer
        Get
            Return _intNroCuotas
        End Get
        Set(ByVal Value As Integer)
            _intNroCuotas = Value
        End Set
    End Property

    <XmlElement("Periodicidad")> _
    Public Property Periodicidad() As String
        Get
            Return _strPeriodicidad
        End Get
        Set(ByVal Value As String)
            _strPeriodicidad = Value
        End Set
    End Property

    <XmlElement("FrecuenciaPago")> _
    Public Property FrecuenciaPago() As String
        Get
            Return _strFrecuenciaPago
        End Get
        Set(ByVal Value As String)
            _strFrecuenciaPago = Value
        End Set
    End Property

    <XmlElement("PlazoGracia")> _
    Public Property PlazoGracia() As Integer
        Get
            Return _intPlazoGracia
        End Get
        Set(ByVal Value As Integer)
            _intPlazoGracia = Value
        End Set
    End Property

    <XmlElement("TipoGracia")> _
    Public Property TipoGracia() As String
        Get
            Return _strTipoGracia
        End Get
        Set(ByVal Value As String)
            _strTipoGracia = Value
        End Set
    End Property

    <XmlElement("FechaProceso")> _
    Public Property FechaProceso() As DateTime
        Get
            Return _dtmFechaProceso
        End Get
        Set(ByVal Value As DateTime)
            _dtmFechaProceso = Value
        End Set
    End Property

    <XmlElement("FechaValor")> _
    Public Property FechaValor() As DateTime
        Get
            Return _dtmFechaValor
        End Get
        Set(ByVal Value As DateTime)
            _dtmFechaValor = Value
        End Set
    End Property

    <XmlElement("FechaValorInicio")> _
    Public Property FechaValorInicio() As String
        Get
            Return _strFechaValorInicio
        End Get
        Set(ByVal Value As String)
            _strFechaValorInicio = Value
        End Set
    End Property

    <XmlElement("FechaValorFin")> _
    Public Property FechaValorFin() As String
        Get
            Return _strFechaValorFin
        End Get
        Set(ByVal Value As String)
            _strFechaValorFin = Value
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

    <XmlElement("MontoTotal")> _
    Public Property MontoTotal() As Decimal
        Get
            Return _decMontoTotal
        End Get
        Set(ByVal Value As Decimal)
            _decMontoTotal = Value
        End Set
    End Property

    <XmlElement("ValorNeto")> _
    Public Property ValorNeto() As Decimal
        Get
            Return _decValorNeto
        End Get
        Set(ByVal Value As Decimal)
            _decValorNeto = Value
        End Set
    End Property

    <XmlElement("TipoLiquidacion")> _
    Public Property TipoLiquidacion() As String
        Get
            Return _strTipoLiquidacion
        End Get
        Set(ByVal Value As String)
            _strTipoLiquidacion = Value
        End Set
    End Property

    <XmlElement("EstadoLiquidacion")> _
    Public Property EstadoLiquidacion() As String
        Get
            Return _strEstadoLiquidacion
        End Get
        Set(ByVal Value As String)
            _strEstadoLiquidacion = Value
        End Set
    End Property

    <XmlElement("Codigotipocronograma")> _
    Public Property Codigotipocronograma() As String
        Get
            Return Me._strCodigotipocronograma
        End Get
        Set(ByVal value As String)
            Me._strCodigotipocronograma = value
        End Set
    End Property

    <XmlElement("Plazograciacuota")> _
    Public Property Plazograciacuota() As Nullable(Of Integer)
        Get
            Return Me._intPlazograciacuota
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intPlazograciacuota = value
        End Set
    End Property

    <XmlElement("Codigotipograciacuota")> _
    Public Property Codigotipograciacuota() As String
        Get
            Return Me._strCodigotipograciacuota
        End Get
        Set(ByVal value As String)
            Me._strCodigotipograciacuota = value
        End Set
    End Property

    <XmlElement("Numerocuotas")> _
    Public Property Numerocuotas() As Nullable(Of Integer)
        Get
            Return Me._intNumerocuotas
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intNumerocuotas = value
        End Set
    End Property

    <XmlElement("Codigoperiodicidad")> _
    Public Property Codigoperiodicidad() As String
        Get
            Return Me._strCodigoperiodicidad
        End Get
        Set(ByVal value As String)
            Me._strCodigoperiodicidad = value
        End Set
    End Property

    <XmlElement("Codigofrecuenciapago")> _
    Public Property Codigofrecuenciapago() As String
        Get
            Return Me._strCodigofrecuenciapago
        End Get
        Set(ByVal value As String)
            Me._strCodigofrecuenciapago = value
        End Set
    End Property

    <XmlElement("FechaPrimerVencimiento")> _
    Public Property FechaPrimerVencimiento() As DateTime
        Get
            Return Me._dtmFechaPrimerVencimiento
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechaPrimerVencimiento = value
        End Set
    End Property

    <XmlElement("Teaporc")> _
    Public Property Teaporc() As Nullable(Of Decimal)
        Get
            Return Me._decTeaporc
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decTeaporc = value
        End Set
    End Property

    <XmlElement("AmortizacionCapital")> _
    Public Property AmortizacionCapital() As Decimal
        Get
            Return _decAmortizacionCapital
        End Get
        Set(ByVal Value As Decimal)
            _decAmortizacionCapital = Value
        End Set
    End Property

    <XmlElement("AmortizacionSeguro")> _
    Public Property AmortizacionSeguro() As Decimal
        Get
            Return _decAmortizacionSeguro
        End Get
        Set(ByVal Value As Decimal)
            _decAmortizacionSeguro = Value
        End Set
    End Property

    <XmlElement("SaldoCapitalPendiente")> _
    Public Property SaldoCapitalPendiente() As Decimal
        Get
            Return _decSaldoCapitalPendiente
        End Get
        Set(ByVal Value As Decimal)
            _decSaldoCapitalPendiente = Value
        End Set
    End Property

    <XmlElement("SaldoSeguroPendiente")> _
    Public Property SaldoSeguroPendiente() As Decimal
        Get
            Return _decSaldoSeguroPendiente
        End Get
        Set(ByVal Value As Decimal)
            _decSaldoSeguroPendiente = Value
        End Set
    End Property

    <XmlElement("InteresCorrido")> _
    Public Property InteresCorrido() As Decimal
        Get
            Return _decInteresCorrido
        End Get
        Set(ByVal Value As Decimal)
            _decInteresCorrido = Value
        End Set
    End Property

    <XmlElement("InteresCorrido")> _
    Public Property InteresCorridoSeguro() As Decimal
        Get
            Return _decInteresCorridoSeguro
        End Get
        Set(ByVal Value As Decimal)
            _decInteresCorridoSeguro = Value
        End Set
    End Property

    <XmlElement("FechaInicioInteres")> _
    Public Property FechaInicioInteres() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaInicioInteres
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaInicioInteres = value
        End Set
    End Property

    <XmlElement("CUCliente")> _
    Public Property CUCliente() As String
        Get
            Return _strCUCLiente
        End Get
        Set(ByVal Value As String)
            _strCUCLiente = Value
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

    <XmlElement("NumCuotaCalendario")> _
    Public Property NumCuotaCalendario() As Integer
        Get
            Return _intNumCuotaCalendario
        End Get
        Set(ByVal Value As Integer)
            _intNumCuotaCalendario = Value
        End Set
    End Property
    'FIN
    'JJM IBK
    <XmlElement("NumSecRecuperacion")> _
   Public Property NumSecRecuperacion() As String
        Get
            Return _strNumSecRecuperacion
        End Get
        Set(ByVal Value As String)
            _strNumSecRecuperacion = Value
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
    <XmlElement("Aplicacion")> _
   Public Property Aplicacion() As Integer
        Get
            Return str_Aplicacion
        End Get
        Set(ByVal Value As Integer)
            str_Aplicacion = Value
        End Set
    End Property
    'Fin
#End Region

End Class



