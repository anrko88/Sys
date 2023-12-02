Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Operacionactiva
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EOperacionactiva")> _
Public Class EOperacionactiva

#Region " Atributos "

    Private _strCodifi As String
    Private _strTiporubrofinanciamiento As String
    Private _strCodoperacionactiva As String
    Private _strCoddocregistro As String
    Private _strCodsucursalifi As String
    Private _decMontofinanciamiento As Nullable(Of Decimal)
    Private _intNumseccondicion As Nullable(Of Integer)
    Private _strTipoplazototal As String
    Private _intCantplazototal As Nullable(Of Integer)
    Private _intCantcuotacapital As Nullable(Of Integer)
    Private _strTipoplazogracia As String
    Private _intCantplazogracia As Nullable(Of Integer)
    Private _intCantcuotagracia As Nullable(Of Integer)
    Private _strTipofrecpagocapital As String
    Private _dtmFechaprimerpagointeres As Nullable(Of DateTime)
    Private _dtmFechaaproxultimodesembolso As Nullable(Of DateTime)
    Private _dtmFechavaloropersindesembolso As Nullable(Of DateTime)
    Private _decPorcentasaactiva As Nullable(Of Decimal)
    Private _decPorcenspreadcofide As Nullable(Of Decimal)
    Private _strTipofrecpagointeres As String
    Private _strTipoamortizacion As String
    Private _strEstadomora As String
    Private _strTipoaplicacionmora As String
    Private _decPorcentasamora As Nullable(Of Decimal)
    Private _decPorcenspreadpasivo As Nullable(Of Decimal)
    Private _decPorcencomision As Nullable(Of Decimal)
    Private _intCantdiasgraciamora As Nullable(Of Integer)
    Private _strTipotasabase As String
    Private _strTipotasapasiva As String
    Private _strTipomodalidadpago As String
    Private _strTipotasacobranza As String
    Private _strTipoperiodocapitalinteres As String
    Private _strEstadocapitalizacionintereses As String
    Private _strEstadopagointeresprimercuota As String
    Private _strEstadopagointeresgracia As String
    Private _strTipointeresadelantodesembolso As String
    Private _strTipotasainteresconfirmada As String
    Private _dtmFechavencimientooperacion As Nullable(Of DateTime)
    Private _strTipoanio As String
    Private _strTipomes As String
    Private _strTipocalendario As String
    Private _strEstadorecalendarizacion As String
    Private _strEstadovalidaanalista As String
    Private _strCodusuarioanalista As String
    Private _strEstadovalidaejecutivo As String
    Private _strCodusuarioejecutivo As String
    Private _strEstadooperacionactiva As String
    Private _strEstadovac As String
    Private _strCodoperacionactivaflar As String
    Private _strCodoperacionpasivaflar As String
    Private _strEstadocondicionpasivadistinta As String
    Private _strEstadoregeneracalendarioactivo As String
    Private _strTipooperacionactiva As String
    Private _strCodoperacionactivarel As String
    Private _dtmFecharegistroifi As Nullable(Of DateTime)
    Private _strTipoviviendafinanciada As String
    Private _strCodmodulooperacion As String
    Private _strTipooperacion As String
    Private _dtmFecharegistro As Nullable(Of DateTime)
    Private _strCodusuario As String
    Private _strTextoaudicreacion As String
    Private _strTextoaudimodi As String
    Private _strEstadooperacion As String
    Private _strIndigv As String
    Private _strAplicacionigv As String
    Private _strIndseguro As String
    Private _strIndrecompra As String
    Private _strIndtransitorias As String
    Private _strIndcronograma As String
    Private _strIndavisos As String
    Private _strIndliquidacion As String
    Private _intDiasvencimiento As Nullable(Of Integer)
    Private _decMontointeresdiferido As Nullable(Of Decimal)
    Private _strIndcuotadoble As String
    Private _strCodestudio As String
    Private _strIndferiados As String
    Private _dtmFechacambiojudicial As Nullable(Of DateTime)
    Private _strIndcargocuenta As String
    Private _strIndpasevencido As String
    Private _decTir As Nullable(Of Decimal)
    Private _strIndrecuperacion As String
    Private _strIndrecolocacion As String
    Private _decIgvdiferido As Nullable(Of Decimal)
    Private _decMontointdiferidopagado As Nullable(Of Decimal)
    Private _decMontoagregado As Nullable(Of Decimal)
    Private _decMontoagregadopagado As Nullable(Of Decimal)
    Private _decPorcenrecaudacioncofigas As Nullable(Of Decimal)
    Private _strCodtipoexposicion As String
    Private _strCodestadocolocacion As String
    Private _strCodtipogracia As String
    Private _intBasileatipoexposicion As Integer
    Private _strCodsolicitudcredito As String

#End Region

#Region " Propiedades "

    <XmlElement("Codifi")> _
    Public Property Codifi() As String
        Get
            Return Me._strCodifi
        End Get
        Set(ByVal value As String)
            Me._strCodifi = value
        End Set
    End Property

    <XmlElement("Tiporubrofinanciamiento")> _
    Public Property Tiporubrofinanciamiento() As String
        Get
            Return Me._strTiporubrofinanciamiento
        End Get
        Set(ByVal value As String)
            Me._strTiporubrofinanciamiento = value
        End Set
    End Property

    <XmlElement("Codoperacionactiva")> _
    Public Property Codoperacionactiva() As String
        Get
            Return Me._strCodoperacionactiva
        End Get
        Set(ByVal value As String)
            Me._strCodoperacionactiva = value
        End Set
    End Property

    <XmlElement("Coddocregistro")> _
    Public Property Coddocregistro() As String
        Get
            Return Me._strCoddocregistro
        End Get
        Set(ByVal value As String)
            Me._strCoddocregistro = value
        End Set
    End Property

    <XmlElement("Codsucursalifi")> _
    Public Property Codsucursalifi() As String
        Get
            Return Me._strCodsucursalifi
        End Get
        Set(ByVal value As String)
            Me._strCodsucursalifi = value
        End Set
    End Property

    <XmlElement("Montofinanciamiento")> _
    Public Property Montofinanciamiento() As Decimal
        Get
            Return Me._decMontofinanciamiento
        End Get
        Set(ByVal value As Decimal)
            Me._decMontofinanciamiento = value
        End Set
    End Property

    <XmlElement("Numseccondicion")> _
    Public Property Numseccondicion() As Integer
        Get
            Return Me._intNumseccondicion
        End Get
        Set(ByVal value As Integer)
            Me._intNumseccondicion = value
        End Set
    End Property

    <XmlElement("Tipoplazototal")> _
    Public Property Tipoplazototal() As String
        Get
            Return Me._strTipoplazototal
        End Get
        Set(ByVal value As String)
            Me._strTipoplazototal = value
        End Set
    End Property

    <XmlElement("Cantplazototal")> _
    Public Property Cantplazototal() As Integer
        Get
            Return Me._intCantplazototal
        End Get
        Set(ByVal value As Integer)
            Me._intCantplazototal = value
        End Set
    End Property

    <XmlElement("Cantcuotacapital")> _
    Public Property Cantcuotacapital() As Integer
        Get
            Return Me._intCantcuotacapital
        End Get
        Set(ByVal value As Integer)
            Me._intCantcuotacapital = value
        End Set
    End Property

    <XmlElement("Tipoplazogracia")> _
    Public Property Tipoplazogracia() As String
        Get
            Return Me._strTipoplazogracia
        End Get
        Set(ByVal value As String)
            Me._strTipoplazogracia = value
        End Set
    End Property

    <XmlElement("Cantplazogracia")> _
    Public Property Cantplazogracia() As Integer
        Get
            Return Me._intCantplazogracia
        End Get
        Set(ByVal value As Integer)
            Me._intCantplazogracia = value
        End Set
    End Property

    <XmlElement("Cantcuotagracia")> _
    Public Property Cantcuotagracia() As Integer
        Get
            Return Me._intCantcuotagracia
        End Get
        Set(ByVal value As Integer)
            Me._intCantcuotagracia = value
        End Set
    End Property

    <XmlElement("Tipofrecpagocapital")> _
    Public Property Tipofrecpagocapital() As String
        Get
            Return Me._strTipofrecpagocapital
        End Get
        Set(ByVal value As String)
            Me._strTipofrecpagocapital = value
        End Set
    End Property

    <XmlElement("Fechaprimerpagointeres")> _
    Public Property Fechaprimerpagointeres() As DateTime
        Get
            Return Me._dtmFechaprimerpagointeres
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechaprimerpagointeres = value
        End Set
    End Property

    <XmlElement("Fechaaproxultimodesembolso")> _
    Public Property Fechaaproxultimodesembolso() As DateTime
        Get
            Return Me._dtmFechaaproxultimodesembolso
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechaaproxultimodesembolso = value
        End Set
    End Property

    <XmlElement("Fechavaloropersindesembolso")> _
    Public Property Fechavaloropersindesembolso() As DateTime
        Get
            Return Me._dtmFechavaloropersindesembolso
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechavaloropersindesembolso = value
        End Set
    End Property

    <XmlElement("Porcentasaactiva")> _
    Public Property Porcentasaactiva() As Decimal
        Get
            Return Me._decPorcentasaactiva
        End Get
        Set(ByVal value As Decimal)
            Me._decPorcentasaactiva = value
        End Set
    End Property

    <XmlElement("Porcenspreadcofide")> _
    Public Property Porcenspreadcofide() As Decimal
        Get
            Return Me._decPorcenspreadcofide
        End Get
        Set(ByVal value As Decimal)
            Me._decPorcenspreadcofide = value
        End Set
    End Property

    <XmlElement("Tipofrecpagointeres")> _
    Public Property Tipofrecpagointeres() As String
        Get
            Return Me._strTipofrecpagointeres
        End Get
        Set(ByVal value As String)
            Me._strTipofrecpagointeres = value
        End Set
    End Property

    <XmlElement("Tipoamortizacion")> _
    Public Property Tipoamortizacion() As String
        Get
            Return Me._strTipoamortizacion
        End Get
        Set(ByVal value As String)
            Me._strTipoamortizacion = value
        End Set
    End Property

    <XmlElement("Estadomora")> _
    Public Property Estadomora() As String
        Get
            Return Me._strEstadomora
        End Get
        Set(ByVal value As String)
            Me._strEstadomora = value
        End Set
    End Property

    <XmlElement("Tipoaplicacionmora")> _
    Public Property Tipoaplicacionmora() As String
        Get
            Return Me._strTipoaplicacionmora
        End Get
        Set(ByVal value As String)
            Me._strTipoaplicacionmora = value
        End Set
    End Property

    <XmlElement("Porcentasamora")> _
    Public Property Porcentasamora() As Decimal
        Get
            Return Me._decPorcentasamora
        End Get
        Set(ByVal value As Decimal)
            Me._decPorcentasamora = value
        End Set
    End Property

    <XmlElement("Porcenspreadpasivo")> _
    Public Property Porcenspreadpasivo() As Decimal
        Get
            Return Me._decPorcenspreadpasivo
        End Get
        Set(ByVal value As Decimal)
            Me._decPorcenspreadpasivo = value
        End Set
    End Property

    <XmlElement("Porcencomision")> _
    Public Property Porcencomision() As Decimal
        Get
            Return Me._decPorcencomision
        End Get
        Set(ByVal value As Decimal)
            Me._decPorcencomision = value
        End Set
    End Property

    <XmlElement("Cantdiasgraciamora")> _
    Public Property Cantdiasgraciamora() As Integer
        Get
            Return Me._intCantdiasgraciamora
        End Get
        Set(ByVal value As Integer)
            Me._intCantdiasgraciamora = value
        End Set
    End Property

    <XmlElement("Tipotasabase")> _
    Public Property Tipotasabase() As String
        Get
            Return Me._strTipotasabase
        End Get
        Set(ByVal value As String)
            Me._strTipotasabase = value
        End Set
    End Property

    <XmlElement("Tipotasapasiva")> _
    Public Property Tipotasapasiva() As String
        Get
            Return Me._strTipotasapasiva
        End Get
        Set(ByVal value As String)
            Me._strTipotasapasiva = value
        End Set
    End Property

    <XmlElement("Tipomodalidadpago")> _
    Public Property Tipomodalidadpago() As String
        Get
            Return Me._strTipomodalidadpago
        End Get
        Set(ByVal value As String)
            Me._strTipomodalidadpago = value
        End Set
    End Property

    <XmlElement("Tipotasacobranza")> _
    Public Property Tipotasacobranza() As String
        Get
            Return Me._strTipotasacobranza
        End Get
        Set(ByVal value As String)
            Me._strTipotasacobranza = value
        End Set
    End Property

    <XmlElement("Tipoperiodocapitalinteres")> _
    Public Property Tipoperiodocapitalinteres() As String
        Get
            Return Me._strTipoperiodocapitalinteres
        End Get
        Set(ByVal value As String)
            Me._strTipoperiodocapitalinteres = value
        End Set
    End Property

    <XmlElement("Estadocapitalizacionintereses")> _
    Public Property Estadocapitalizacionintereses() As String
        Get
            Return Me._strEstadocapitalizacionintereses
        End Get
        Set(ByVal value As String)
            Me._strEstadocapitalizacionintereses = value
        End Set
    End Property

    <XmlElement("Estadopagointeresprimercuota")> _
    Public Property Estadopagointeresprimercuota() As String
        Get
            Return Me._strEstadopagointeresprimercuota
        End Get
        Set(ByVal value As String)
            Me._strEstadopagointeresprimercuota = value
        End Set
    End Property

    <XmlElement("Estadopagointeresgracia")> _
    Public Property Estadopagointeresgracia() As String
        Get
            Return Me._strEstadopagointeresgracia
        End Get
        Set(ByVal value As String)
            Me._strEstadopagointeresgracia = value
        End Set
    End Property

    <XmlElement("Tipointeresadelantodesembolso")> _
    Public Property Tipointeresadelantodesembolso() As String
        Get
            Return Me._strTipointeresadelantodesembolso
        End Get
        Set(ByVal value As String)
            Me._strTipointeresadelantodesembolso = value
        End Set
    End Property

    <XmlElement("Tipotasainteresconfirmada")> _
    Public Property Tipotasainteresconfirmada() As String
        Get
            Return Me._strTipotasainteresconfirmada
        End Get
        Set(ByVal value As String)
            Me._strTipotasainteresconfirmada = value
        End Set
    End Property

    <XmlElement("Fechavencimientooperacion")> _
    Public Property Fechavencimientooperacion() As DateTime
        Get
            Return Me._dtmFechavencimientooperacion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechavencimientooperacion = value
        End Set
    End Property

    <XmlElement("Tipoanio")> _
    Public Property Tipoanio() As String
        Get
            Return Me._strTipoanio
        End Get
        Set(ByVal value As String)
            Me._strTipoanio = value
        End Set
    End Property

    <XmlElement("Tipomes")> _
    Public Property Tipomes() As String
        Get
            Return Me._strTipomes
        End Get
        Set(ByVal value As String)
            Me._strTipomes = value
        End Set
    End Property

    <XmlElement("Tipocalendario")> _
    Public Property Tipocalendario() As String
        Get
            Return Me._strTipocalendario
        End Get
        Set(ByVal value As String)
            Me._strTipocalendario = value
        End Set
    End Property

    <XmlElement("Estadorecalendarizacion")> _
    Public Property Estadorecalendarizacion() As String
        Get
            Return Me._strEstadorecalendarizacion
        End Get
        Set(ByVal value As String)
            Me._strEstadorecalendarizacion = value
        End Set
    End Property

    <XmlElement("Estadovalidaanalista")> _
    Public Property Estadovalidaanalista() As String
        Get
            Return Me._strEstadovalidaanalista
        End Get
        Set(ByVal value As String)
            Me._strEstadovalidaanalista = value
        End Set
    End Property

    <XmlElement("Codusuarioanalista")> _
    Public Property Codusuarioanalista() As String
        Get
            Return Me._strCodusuarioanalista
        End Get
        Set(ByVal value As String)
            Me._strCodusuarioanalista = value
        End Set
    End Property

    <XmlElement("Estadovalidaejecutivo")> _
    Public Property Estadovalidaejecutivo() As String
        Get
            Return Me._strEstadovalidaejecutivo
        End Get
        Set(ByVal value As String)
            Me._strEstadovalidaejecutivo = value
        End Set
    End Property

    <XmlElement("Codusuarioejecutivo")> _
    Public Property Codusuarioejecutivo() As String
        Get
            Return Me._strCodusuarioejecutivo
        End Get
        Set(ByVal value As String)
            Me._strCodusuarioejecutivo = value
        End Set
    End Property

    <XmlElement("Estadooperacionactiva")> _
    Public Property Estadooperacionactiva() As String
        Get
            Return Me._strEstadooperacionactiva
        End Get
        Set(ByVal value As String)
            Me._strEstadooperacionactiva = value
        End Set
    End Property

    <XmlElement("Estadovac")> _
    Public Property Estadovac() As String
        Get
            Return Me._strEstadovac
        End Get
        Set(ByVal value As String)
            Me._strEstadovac = value
        End Set
    End Property

    <XmlElement("Codoperacionactivaflar")> _
    Public Property Codoperacionactivaflar() As String
        Get
            Return Me._strCodoperacionactivaflar
        End Get
        Set(ByVal value As String)
            Me._strCodoperacionactivaflar = value
        End Set
    End Property

    <XmlElement("Codoperacionpasivaflar")> _
    Public Property Codoperacionpasivaflar() As String
        Get
            Return Me._strCodoperacionpasivaflar
        End Get
        Set(ByVal value As String)
            Me._strCodoperacionpasivaflar = value
        End Set
    End Property

    <XmlElement("Estadocondicionpasivadistinta")> _
    Public Property Estadocondicionpasivadistinta() As String
        Get
            Return Me._strEstadocondicionpasivadistinta
        End Get
        Set(ByVal value As String)
            Me._strEstadocondicionpasivadistinta = value
        End Set
    End Property

    <XmlElement("Estadoregeneracalendarioactivo")> _
    Public Property Estadoregeneracalendarioactivo() As String
        Get
            Return Me._strEstadoregeneracalendarioactivo
        End Get
        Set(ByVal value As String)
            Me._strEstadoregeneracalendarioactivo = value
        End Set
    End Property

    <XmlElement("Tipooperacionactiva")> _
    Public Property Tipooperacionactiva() As String
        Get
            Return Me._strTipooperacionactiva
        End Get
        Set(ByVal value As String)
            Me._strTipooperacionactiva = value
        End Set
    End Property

    <XmlElement("Codoperacionactivarel")> _
    Public Property Codoperacionactivarel() As String
        Get
            Return Me._strCodoperacionactivarel
        End Get
        Set(ByVal value As String)
            Me._strCodoperacionactivarel = value
        End Set
    End Property

    <XmlElement("Fecharegistroifi")> _
    Public Property Fecharegistroifi() As DateTime
        Get
            Return Me._dtmFecharegistroifi
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFecharegistroifi = value
        End Set
    End Property

    <XmlElement("Tipoviviendafinanciada")> _
    Public Property Tipoviviendafinanciada() As String
        Get
            Return Me._strTipoviviendafinanciada
        End Get
        Set(ByVal value As String)
            Me._strTipoviviendafinanciada = value
        End Set
    End Property

    <XmlElement("Codmodulooperacion")> _
    Public Property Codmodulooperacion() As String
        Get
            Return Me._strCodmodulooperacion
        End Get
        Set(ByVal value As String)
            Me._strCodmodulooperacion = value
        End Set
    End Property

    <XmlElement("Tipooperacion")> _
    Public Property Tipooperacion() As String
        Get
            Return Me._strTipooperacion
        End Get
        Set(ByVal value As String)
            Me._strTipooperacion = value
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

    <XmlElement("Codusuario")> _
    Public Property Codusuario() As String
        Get
            Return Me._strCodusuario
        End Get
        Set(ByVal value As String)
            Me._strCodusuario = value
        End Set
    End Property

    <XmlElement("Textoaudicreacion")> _
    Public Property Textoaudicreacion() As String
        Get
            Return Me._strTextoaudicreacion
        End Get
        Set(ByVal value As String)
            Me._strTextoaudicreacion = value
        End Set
    End Property

    <XmlElement("Textoaudimodi")> _
    Public Property Textoaudimodi() As String
        Get
            Return Me._strTextoaudimodi
        End Get
        Set(ByVal value As String)
            Me._strTextoaudimodi = value
        End Set
    End Property

    <XmlElement("Estadooperacion")> _
    Public Property Estadooperacion() As String
        Get
            Return Me._strEstadooperacion
        End Get
        Set(ByVal value As String)
            Me._strEstadooperacion = value
        End Set
    End Property

    <XmlElement("Indigv")> _
    Public Property Indigv() As String
        Get
            Return Me._strIndigv
        End Get
        Set(ByVal value As String)
            Me._strIndigv = value
        End Set
    End Property

    <XmlElement("Aplicacionigv")> _
    Public Property Aplicacionigv() As String
        Get
            Return Me._strAplicacionigv
        End Get
        Set(ByVal value As String)
            Me._strAplicacionigv = value
        End Set
    End Property

    <XmlElement("Indseguro")> _
    Public Property Indseguro() As String
        Get
            Return Me._strIndseguro
        End Get
        Set(ByVal value As String)
            Me._strIndseguro = value
        End Set
    End Property

    <XmlElement("Indrecompra")> _
    Public Property Indrecompra() As String
        Get
            Return Me._strIndrecompra
        End Get
        Set(ByVal value As String)
            Me._strIndrecompra = value
        End Set
    End Property

    <XmlElement("Indtransitorias")> _
    Public Property Indtransitorias() As String
        Get
            Return Me._strIndtransitorias
        End Get
        Set(ByVal value As String)
            Me._strIndtransitorias = value
        End Set
    End Property

    <XmlElement("Indcronograma")> _
    Public Property Indcronograma() As String
        Get
            Return Me._strIndcronograma
        End Get
        Set(ByVal value As String)
            Me._strIndcronograma = value
        End Set
    End Property

    <XmlElement("Indavisos")> _
    Public Property Indavisos() As String
        Get
            Return Me._strIndavisos
        End Get
        Set(ByVal value As String)
            Me._strIndavisos = value
        End Set
    End Property

    <XmlElement("Indliquidacion")> _
    Public Property Indliquidacion() As String
        Get
            Return Me._strIndliquidacion
        End Get
        Set(ByVal value As String)
            Me._strIndliquidacion = value
        End Set
    End Property

    <XmlElement("Diasvencimiento")> _
    Public Property Diasvencimiento() As Integer
        Get
            Return Me._intDiasvencimiento
        End Get
        Set(ByVal value As Integer)
            Me._intDiasvencimiento = value
        End Set
    End Property

    <XmlElement("Montointeresdiferido")> _
    Public Property Montointeresdiferido() As Decimal
        Get
            Return Me._decMontointeresdiferido
        End Get
        Set(ByVal value As Decimal)
            Me._decMontointeresdiferido = value
        End Set
    End Property

    <XmlElement("Indcuotadoble")> _
    Public Property Indcuotadoble() As String
        Get
            Return Me._strIndcuotadoble
        End Get
        Set(ByVal value As String)
            Me._strIndcuotadoble = value
        End Set
    End Property

    <XmlElement("Codestudio")> _
    Public Property Codestudio() As String
        Get
            Return Me._strCodestudio
        End Get
        Set(ByVal value As String)
            Me._strCodestudio = value
        End Set
    End Property

    <XmlElement("Indferiados")> _
    Public Property Indferiados() As String
        Get
            Return Me._strIndferiados
        End Get
        Set(ByVal value As String)
            Me._strIndferiados = value
        End Set
    End Property

    <XmlElement("Fechacambiojudicial")> _
    Public Property Fechacambiojudicial() As DateTime
        Get
            Return Me._dtmFechacambiojudicial
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechacambiojudicial = value
        End Set
    End Property

    <XmlElement("Indcargocuenta")> _
    Public Property Indcargocuenta() As String
        Get
            Return Me._strIndcargocuenta
        End Get
        Set(ByVal value As String)
            Me._strIndcargocuenta = value
        End Set
    End Property

    <XmlElement("Indpasevencido")> _
    Public Property Indpasevencido() As String
        Get
            Return Me._strIndpasevencido
        End Get
        Set(ByVal value As String)
            Me._strIndpasevencido = value
        End Set
    End Property

    <XmlElement("Tir")> _
    Public Property Tir() As Decimal
        Get
            Return Me._decTir
        End Get
        Set(ByVal value As Decimal)
            Me._decTir = value
        End Set
    End Property

    <XmlElement("Indrecuperacion")> _
    Public Property Indrecuperacion() As String
        Get
            Return Me._strIndrecuperacion
        End Get
        Set(ByVal value As String)
            Me._strIndrecuperacion = value
        End Set
    End Property

    <XmlElement("Indrecolocacion")> _
    Public Property Indrecolocacion() As String
        Get
            Return Me._strIndrecolocacion
        End Get
        Set(ByVal value As String)
            Me._strIndrecolocacion = value
        End Set
    End Property

    <XmlElement("Igvdiferido")> _
    Public Property Igvdiferido() As Decimal
        Get
            Return Me._decIgvdiferido
        End Get
        Set(ByVal value As Decimal)
            Me._decIgvdiferido = value
        End Set
    End Property

    <XmlElement("Montointdiferidopagado")> _
    Public Property Montointdiferidopagado() As Decimal
        Get
            Return Me._decMontointdiferidopagado
        End Get
        Set(ByVal value As Decimal)
            Me._decMontointdiferidopagado = value
        End Set
    End Property

    <XmlElement("Montoagregado")> _
    Public Property Montoagregado() As Decimal
        Get
            Return Me._decMontoagregado
        End Get
        Set(ByVal value As Decimal)
            Me._decMontoagregado = value
        End Set
    End Property

    <XmlElement("Montoagregadopagado")> _
    Public Property Montoagregadopagado() As Decimal
        Get
            Return Me._decMontoagregadopagado
        End Get
        Set(ByVal value As Decimal)
            Me._decMontoagregadopagado = value
        End Set
    End Property

    <XmlElement("Porcenrecaudacioncofigas")> _
    Public Property Porcenrecaudacioncofigas() As Decimal
        Get
            Return Me._decPorcenrecaudacioncofigas
        End Get
        Set(ByVal value As Decimal)
            Me._decPorcenrecaudacioncofigas = value
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

    <XmlElement("Codestadocolocacion")> _
    Public Property Codestadocolocacion() As String
        Get
            Return Me._strCodestadocolocacion
        End Get
        Set(ByVal value As String)
            Me._strCodestadocolocacion = value
        End Set
    End Property

    <XmlElement("Codtipogracia")> _
    Public Property Codtipogracia() As String
        Get
            Return Me._strCodtipogracia
        End Get
        Set(ByVal value As String)
            Me._strCodtipogracia = value
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

    <XmlElement("Codsolicitudcredito")> _
    Public Property Codsolicitudcredito() As String
        Get
            Return Me._strCodsolicitudcredito
        End Get
        Set(ByVal value As String)
            Me._strCodsolicitudcredito = value
        End Set
    End Property


#End Region

End Class