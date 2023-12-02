Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Solicitudcreditoestructura
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("ESolicitudcreditoestructura")> _
Public Class ESolicitudcreditoestructura

#Region " Atributos "

    Private _strCodsolicitudcredito As String
    Private _intSecfinanciamiento As Nullable(Of Integer)
    Private _strTiporubrofinanciamiento As String
    Private _decMontoaportecofide As Nullable(Of Decimal)
    Private _decMontoaporteifi As Nullable(Of Decimal)
    Private _decMontoaportesubprestatario As Nullable(Of Decimal)
    Private _strCodmodulooperacion As String
    Private _strTipooperacion As String
    Private _dtmFecharegistro As Nullable(Of DateTime)
    Private _strCodusuario As String
    Private _strTextoaudicreacion As String
    Private _strTextoaudimodi As String
    Private _decMontovalorvivienda As Nullable(Of Decimal)
    Private _decMontovalortazacion As Nullable(Of Decimal)
    Private _strDetrubrofinanciamiento As String
    Private _strTipoproducto As String
    Private _intCantidadproducto As Nullable(Of Integer)
    Private _decMontoaportecofideigv As Nullable(Of Decimal)
    Private _decMontoaportesubprestatarioigv As Nullable(Of Decimal)
    Private _strIndbien As String
    Private _intSecdetalle As Nullable(Of Integer)
    Private _intNrodesembolso As Nullable(Of Integer)
    Private _decMontoaportecofideoriginal As Nullable(Of Decimal)
    Private _decMontoaportecofideigvoriginal As Nullable(Of Decimal)
    Private _decTipocambio As Nullable(Of Decimal)
    Private _strMonedabien As String
    Private _strMonedabienNombre As String
    Private _dtmFechainscripcion As Nullable(Of DateTime)
    Private _strUso As String
    Private _strUbicacion As String
    Private _strCodigotipobien As String
    Private _strCodigoestadobien As String
    Private _strCodproveedor As String
    Private _strValorBien As String
    Private _strAudusuariomodificacion As String
    Private _strAudusuariocreacion As String

    Private _strNumeroContrato As String = String.Empty
    Private _strCodUnico As String = String.Empty
    Private _strRazonSocial As String = String.Empty
    Private _strDepartamento As String = String.Empty
    Private _strFechaTransferencia As Nullable(Of DateTime)

    Private _strTipoDocumento As String = String.Empty
    Private _strNumeroDocumento As String = String.Empty
    Private _strKardex As String = String.Empty

    Private _strTipoBien As String = String.Empty
    Private _strBanca As String = String.Empty
    Private _strEjecutivoBanca As String = String.Empty
    Private _strObservacionContrato As String = String.Empty
    Private _strObservacionBien As String = String.Empty
    Private _strEstadoContrato As String = String.Empty
    Private _strMoneda As String = String.Empty
    Private _strDescripcionBien As String = String.Empty
    Private _strClasificacionBien As String = String.Empty
    Private _decMontoValorBien As Decimal = 0
    Private _strProvincia As String = String.Empty
    Private _strDistrito As String = String.Empty
    Private _strEstadoBien As String = String.Empty
    Private _strPartidaRegistral As String = String.Empty
    Private _strOficinaRegistral As String = String.Empty
    Private _strComentario As String = String.Empty
    Private _strPlaca As String = String.Empty
    Private _strPlacaAntigua As String = String.Empty
    Private _intAnio As Integer = 0
    Private _strNroSerie As String = String.Empty
    Private _strNroMotor As String = String.Empty
    Private _strMarca As String = String.Empty
    Private _strModelo As String = String.Empty
    Private _strColor As String = String.Empty
    Private _strCarroceria As String = String.Empty
    Private _strMedidas As String = String.Empty
    Private _intFlag As Integer = 0

    Private _strDepartamentoNombre As String = String.Empty
    Private _strProvinciaNombre As String = String.Empty
    Private _strDistritoNombre As String = String.Empty

    Private _strFechaAdquisicion As Nullable(Of DateTime)
    Private _strFechaBaja As Nullable(Of DateTime)
    Private _strFechaEliminacion As Nullable(Of DateTime)
    Private _strFechaInscripcionMunicipal As Nullable(Of DateTime)
    Private _strFechaInscripcionRegistral As Nullable(Of DateTime)
    Private _strFechaEmisionTarjeta As Nullable(Of DateTime)


    Private _strComentarioBaja As String = String.Empty


    Private _strCodigoNotaria As String = String.Empty
    Private _strCodEstadoInscripcionRrPp As String = String.Empty
    Private _strCodEstadoMunicipal As String = String.Empty
    Private _strPropiedad As String = String.Empty
    Private _strcodTransmision As String = String.Empty
    Private _strcodTipoCarroceria As String = String.Empty
    Private _strcodTraccion As String = String.Empty
    Private _strCodTipoMotor As String = String.Empty
    Private _strPotenciaMotor As String = String.Empty
    Private _strCodCumbustible As String = String.Empty
    Private _strCilindros As String = String.Empty
    Private _strLongitud As String = String.Empty
    Private _strPesoNeto As String
    Private _strCargaUtil As String
    Private _strPesoBruto As String
    Private _strAsientos As Integer = 0
    Private _strEjes As Integer = 0
    Private _strAncho As String
    Private _strPuertas As Integer = 0
    Private _strAlto As String
    Private _strClase As String = String.Empty
    Private _strFormulaRodante As String = String.Empty
    Private _strcodClase As String = String.Empty
    Private _strcilindraje As String = String.Empty

    Private _strFechaEnvioSat As Nullable(Of DateTime)
    Private _strFechaEnvioRrPp As Nullable(Of DateTime)
    Private _strFechaEnvioNotaria As Nullable(Of DateTime)
    Private _strFechaPropiedad As Nullable(Of DateTime)

    Private _strCodInafectacion As String = String.Empty
    Private _strCodPagoImpuestos As String = String.Empty

    Private _strPasajeros As Integer = 0
    Private _strRuedas As Integer = 0

    Private _strFechaProbableFinObra As Nullable(Of DateTime)
    Private _strFechaRealFinObra As Nullable(Of DateTime)
    Private _strCodEstadoTransferencia As String
    Private _strCodigoPredio As String
    Private _strFlagInafectacion As String
    Private _strFlagPagoImpuesto As String
    Private _intFlag_Origen As Nullable(Of Integer)


    Private _decPrecioventa As Nullable(Of Decimal)
    Private _decPreciototal As Nullable(Of Decimal)
    Private _strCodigoSubTipoContrato As String

    'Inicio IBK
    Private _strMunicipalidad As String = String.Empty

#End Region

#Region " Propiedades "

    <XmlElement("FlagInafectacion")> _
Public Property FlagInafectacion() As String
        Get
            Return _strFlagInafectacion
        End Get
        Set(ByVal value As String)
            _strFlagInafectacion = value
        End Set
    End Property
    <XmlElement("FlagPagoImpuestos")> _
Public Property FlagPagoImpuestos() As String
        Get
            Return _strFlagPagoImpuesto
        End Get
        Set(ByVal value As String)
            _strFlagPagoImpuesto = value
        End Set
    End Property
    <XmlElement("Ruedas")> _
Public Property Ruedas() As Integer
        Get
            Return _strRuedas
        End Get
        Set(ByVal value As Integer)
            _strRuedas = value
        End Set
    End Property

    <XmlElement("Pasajeros")> _
Public Property Pasajeros() As Integer
        Get
            Return _strPasajeros
        End Get
        Set(ByVal value As Integer)
            _strPasajeros = value
        End Set
    End Property

    <XmlElement("CodigoPredio")> _
Public Property CodigoPredio() As String
        Get
            Return _strCodigoPredio
        End Get
        Set(ByVal value As String)
            _strCodigoPredio = value
        End Set
    End Property
    <XmlElement("Flag_origen")> _
Public Property Flag_origen() As Nullable(Of Integer)
        Get
            Return _intFlag_Origen
        End Get
        Set(ByVal value As Nullable(Of Integer))
            _intFlag_Origen = value
        End Set
    End Property

    <XmlElement("ComentarioBaja")> _
  Public Property ComentarioBaja() As String
        Get
            Return _strComentarioBaja
        End Get
        Set(ByVal value As String)
            _strComentarioBaja = value
        End Set
    End Property

    <XmlElement("CodEstadoTransferencia")> _
   Public Property CodEstadoTransferencia() As String
        Get
            Return _strCodEstadoTransferencia
        End Get
        Set(ByVal value As String)
            _strCodEstadoTransferencia = value
        End Set
    End Property
    <XmlElement("FechaEmisionTarjeta")> _
    Public Property FechaEmisionTarjeta() As Nullable(Of DateTime)
        Get
            Return _strFechaEmisionTarjeta
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _strFechaEmisionTarjeta = value
        End Set
    End Property
    <XmlElement("FechaProbableFinObra")> _
    Public Property FechaProbableFinObra() As Nullable(Of DateTime)
        Get
            Return _strFechaProbableFinObra
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _strFechaProbableFinObra = value
        End Set
    End Property
    <XmlElement("FechaEliminacion")> _
 Public Property FechaEliminacion() As Nullable(Of DateTime)
        Get
            Return _strFechaEliminacion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _strFechaEliminacion = value
        End Set
    End Property
    <XmlElement("FechaRealFinObra")> _
    Public Property FechaRealFinObra() As Nullable(Of DateTime)
        Get
            Return _strFechaRealFinObra
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _strFechaRealFinObra = value
        End Set
    End Property
    <XmlElement("FechaBaja")> _
Public Property FechaBaja() As Nullable(Of DateTime)
        Get
            Return Me._strFechaBaja
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._strFechaBaja = value
        End Set
    End Property

    <XmlElement("FechaEnvioRRPP")> _
Public Property FechaEnvioRrPp() As Nullable(Of DateTime)
        Get
            Return Me._strFechaEnvioRrPp
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._strFechaEnvioRrPp = value
        End Set
    End Property

    <XmlElement("Clase")> _
Public Property Clase() As String
        Get
            Return Me._strClase
        End Get
        Set(ByVal value As String)
            Me._strClase = value
        End Set
    End Property
    <XmlElement("CodPagoImpuesto")> _
Public Property CodPagoImpuesto() As String
        Get
            Return Me._strCodPagoImpuestos
        End Get
        Set(ByVal value As String)
            Me._strCodPagoImpuestos = value
        End Set
    End Property
    <XmlElement("CodInafectacion")> _
Public Property CodInafectacion() As String
        Get
            Return Me._strCodInafectacion
        End Get
        Set(ByVal value As String)
            Me._strCodInafectacion = value
        End Set
    End Property
    <XmlElement("FechaPropiedad")> _
Public Property FechaPropiedad() As Nullable(Of DateTime)
        Get
            Return Me._strFechaPropiedad
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._strFechaPropiedad = value
        End Set
    End Property
    <XmlElement("FechaEnvioNotaria")> _
Public Property FechaEnvioNotaria() As Nullable(Of DateTime)
        Get
            Return Me._strFechaEnvioNotaria
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._strFechaEnvioNotaria = value
        End Set
    End Property
    <XmlElement("FechaEnvioSAT")> _
Public Property FechaEnvioSat() As Nullable(Of DateTime)
        Get
            Return Me._strFechaEnvioSat
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._strFechaEnvioSat = value
        End Set
    End Property
    <XmlElement("Cilindraje")> _
Public Property Cilindraje() As String
        Get
            Return Me._strcilindraje
        End Get
        Set(ByVal value As String)
            Me._strcilindraje = value
        End Set
    End Property
    <XmlElement("CodClase")> _
Public Property CodClase() As String
        Get
            Return Me._strcodClase
        End Get
        Set(ByVal value As String)
            Me._strcodClase = value
        End Set
    End Property
    <XmlElement("FormulaRodante")> _
Public Property FormulaRodante() As String
        Get
            Return Me._strFormulaRodante
        End Get
        Set(ByVal value As String)
            Me._strFormulaRodante = value
        End Set
    End Property

    <XmlElement("Alto")> _
Public Property Alto() As String
        Get
            Return Me._strAlto
        End Get
        Set(ByVal value As String)
            Me._strAlto = value
        End Set
    End Property
    <XmlElement("Puertas")> _
Public Property Puertas() As Integer
        Get
            Return Me._strPuertas
        End Get
        Set(ByVal value As Integer)
            Me._strPuertas = value
        End Set
    End Property

    <XmlElement("Ancho")> _
Public Property Ancho() As String
        Get
            Return Me._strAncho
        End Get
        Set(ByVal value As String)
            Me._strAncho = value
        End Set
    End Property
    <XmlElement("Ejes")> _
Public Property Ejes() As Integer
        Get
            Return Me._strEjes
        End Get
        Set(ByVal value As Integer)
            Me._strEjes = value
        End Set
    End Property
    <XmlElement("Asientos")> _
Public Property Asientos() As Integer
        Get
            Return Me._strAsientos
        End Get
        Set(ByVal value As Integer)
            Me._strAsientos = value
        End Set
    End Property

    <XmlElement("PesoBruto")> _
Public Property PesoBruto() As String
        Get
            Return Me._strPesoBruto
        End Get
        Set(ByVal value As String)
            Me._strPesoBruto = value
        End Set
    End Property

    <XmlElement("CargaUtil")> _
Public Property CargaUtil() As String
        Get
            Return Me._strCargaUtil
        End Get
        Set(ByVal value As String)
            Me._strCargaUtil = value
        End Set
    End Property

    <XmlElement("PesoNeto")> _
Public Property PesoNeto() As String
        Get
            Return Me._strPesoNeto
        End Get
        Set(ByVal value As String)
            Me._strPesoNeto = value
        End Set
    End Property

    <XmlElement("Longitud")> _
Public Property Longitud() As String
        Get
            Return Me._strLongitud
        End Get
        Set(ByVal value As String)
            Me._strLongitud = value
        End Set
    End Property

    <XmlElement("Cilindros")> _
Public Property Cilindros() As String
        Get
            Return Me._strCilindros
        End Get
        Set(ByVal value As String)
            Me._strCilindros = value
        End Set
    End Property

    <XmlElement("CodCombustible")> _
Public Property CodCombustible() As String
        Get
            Return Me._strCodCumbustible
        End Get
        Set(ByVal value As String)
            Me._strCodCumbustible = value
        End Set
    End Property

    <XmlElement("CodPotenciaMotor")> _
Public Property CodPotenciaMotor() As String
        Get
            Return Me._strPotenciaMotor
        End Get
        Set(ByVal value As String)
            Me._strPotenciaMotor = value
        End Set
    End Property
    <XmlElement("CodTipoMotor")> _
Public Property CodTipoMotor() As String
        Get
            Return Me._strCodTipoMotor
        End Get
        Set(ByVal value As String)
            Me._strCodTipoMotor = value
        End Set
    End Property

    <XmlElement("CodTraccion")> _
Public Property CodTraccion() As String
        Get
            Return Me._strcodTraccion
        End Get
        Set(ByVal value As String)
            Me._strcodTraccion = value
        End Set
    End Property
    <XmlElement("CodTransmision")> _
Public Property CodTransmision() As String
        Get
            Return Me._strcodTransmision
        End Get
        Set(ByVal value As String)
            Me._strcodTransmision = value
        End Set
    End Property


    <XmlElement("FechaAdquisicion")> _
Public Property FechaAdquisicion() As Nullable(Of DateTime)
        Get
            Return Me._strFechaAdquisicion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._strFechaAdquisicion = value
        End Set
    End Property
    <XmlElement("FechaInscripcionMunicipal")> _
Public Property FechaInscripcionMunicipal() As Nullable(Of DateTime)
        Get
            Return Me._strFechaInscripcionMunicipal
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._strFechaInscripcionMunicipal = value
        End Set
    End Property
    <XmlElement("FechaInscripcionRegistral")> _
Public Property FechaInscripcionRegistral() As Nullable(Of DateTime)
        Get
            Return Me._strFechaInscripcionRegistral
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._strFechaInscripcionRegistral = value
        End Set
    End Property
    <XmlElement("CodigoNotaria")> _
Public Property CodigoNotaria() As String
        Get
            Return Me._strCodigoNotaria
        End Get
        Set(ByVal value As String)
            Me._strCodigoNotaria = value
        End Set
    End Property
    <XmlElement("CodEstadoInscripcionRRPP")> _
Public Property CodEstadoInscripcionRrPp() As String
        Get
            Return Me._strCodEstadoInscripcionRrPp
        End Get
        Set(ByVal value As String)
            Me._strCodEstadoInscripcionRrPp = value
        End Set
    End Property
    <XmlElement("CodEstadoMunicipal")> _
Public Property CodEstadoMunicipal() As String
        Get
            Return Me._strCodEstadoMunicipal
        End Get
        Set(ByVal value As String)
            Me._strCodEstadoMunicipal = value
        End Set
    End Property
    <XmlElement("Propiedad")> _
Public Property Propiedad() As String
        Get
            Return Me._strPropiedad
        End Get
        Set(ByVal value As String)
            Me._strPropiedad = value
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

    <XmlElement("Secfinanciamiento")> _
    Public Property Secfinanciamiento() As Nullable(Of Integer)
        Get
            Return Me._intSecfinanciamiento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intSecfinanciamiento = value
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

    <XmlElement("Montoaportecofide")> _
    Public Property Montoaportecofide() As Nullable(Of Decimal)
        Get
            Return Me._decMontoaportecofide
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoaportecofide = value
        End Set
    End Property

    <XmlElement("Montoaporteifi")> _
    Public Property Montoaporteifi() As Nullable(Of Decimal)
        Get
            Return Me._decMontoaporteifi
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoaporteifi = value
        End Set
    End Property

    <XmlElement("Montoaportesubprestatario")> _
    Public Property Montoaportesubprestatario() As Nullable(Of Decimal)
        Get
            Return Me._decMontoaportesubprestatario
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoaportesubprestatario = value
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
    Public Property Fecharegistro() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecharegistro
        End Get
        Set(ByVal value As Nullable(Of DateTime))
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

    <XmlElement("Montovalorvivienda")> _
    Public Property Montovalorvivienda() As Nullable(Of Decimal)
        Get
            Return Me._decMontovalorvivienda
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontovalorvivienda = value
        End Set
    End Property

    <XmlElement("Montovalortazacion")> _
    Public Property Montovalortazacion() As Nullable(Of Decimal)
        Get
            Return Me._decMontovalortazacion
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontovalortazacion = value
        End Set
    End Property

    <XmlElement("Detrubrofinanciamiento")> _
    Public Property Detrubrofinanciamiento() As String
        Get
            Return Me._strDetrubrofinanciamiento
        End Get
        Set(ByVal value As String)
            Me._strDetrubrofinanciamiento = value
        End Set
    End Property

    <XmlElement("Tipoproducto")> _
    Public Property Tipoproducto() As String
        Get
            Return Me._strTipoproducto
        End Get
        Set(ByVal value As String)
            Me._strTipoproducto = value
        End Set
    End Property

    <XmlElement("Cantidadproducto")> _
    Public Property Cantidadproducto() As Nullable(Of Integer)
        Get
            Return Me._intCantidadproducto
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCantidadproducto = value
        End Set
    End Property

    <XmlElement("Montoaportecofideigv")> _
    Public Property Montoaportecofideigv() As Nullable(Of Decimal)
        Get
            Return Me._decMontoaportecofideigv
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoaportecofideigv = value
        End Set
    End Property

    <XmlElement("Montoaportesubprestatarioigv")> _
    Public Property Montoaportesubprestatarioigv() As Nullable(Of Decimal)
        Get
            Return Me._decMontoaportesubprestatarioigv
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoaportesubprestatarioigv = value
        End Set
    End Property

    <XmlElement("Indbien")> _
    Public Property Indbien() As String
        Get
            Return Me._strIndbien
        End Get
        Set(ByVal value As String)
            Me._strIndbien = value
        End Set
    End Property

    <XmlElement("Secdetalle")> _
    Public Property Secdetalle() As Nullable(Of Integer)
        Get
            Return Me._intSecdetalle
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intSecdetalle = value
        End Set
    End Property

    <XmlElement("Nrodesembolso")> _
    Public Property Nrodesembolso() As Nullable(Of Integer)
        Get
            Return Me._intNrodesembolso
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intNrodesembolso = value
        End Set
    End Property

    <XmlElement("Montoaportecofideoriginal")> _
    Public Property Montoaportecofideoriginal() As Nullable(Of Decimal)
        Get
            Return Me._decMontoaportecofideoriginal
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoaportecofideoriginal = value
        End Set
    End Property

    <XmlElement("Montoaportecofideigvoriginal")> _
    Public Property Montoaportecofideigvoriginal() As Nullable(Of Decimal)
        Get
            Return Me._decMontoaportecofideigvoriginal
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoaportecofideigvoriginal = value
        End Set
    End Property

    <XmlElement("Tipocambio")> _
    Public Property Tipocambio() As Nullable(Of Decimal)
        Get
            Return Me._decTipocambio
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decTipocambio = value
        End Set
    End Property

    <XmlElement("Monedabien")> _
    Public Property Monedabien() As String
        Get
            Return Me._strMonedabien
        End Get
        Set(ByVal value As String)
            Me._strMonedabien = value
        End Set
    End Property
    <XmlElement("MonedabienNombre")> _
 Public Property MonedabienNombre() As String
        Get
            Return Me._strMonedabienNombre
        End Get
        Set(ByVal value As String)
            Me._strMonedabienNombre = value
        End Set
    End Property

    <XmlElement("Fechainscripcion")> _
    Public Property Fechainscripcion() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechainscripcion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechainscripcion = value
        End Set
    End Property

    <XmlElement("Uso")> _
    Public Property Uso() As String
        Get
            Return Me._strUso
        End Get
        Set(ByVal value As String)
            Me._strUso = value
        End Set
    End Property

    <XmlElement("Ubicacion")> _
    Public Property Ubicacion() As String
        Get
            Return Me._strUbicacion
        End Get
        Set(ByVal value As String)
            Me._strUbicacion = value
        End Set
    End Property

    <XmlElement("Codigotipobien")> _
    Public Property Codigotipobien() As String
        Get
            Return Me._strCodigotipobien
        End Get
        Set(ByVal value As String)
            Me._strCodigotipobien = value
        End Set
    End Property

    <XmlElement("Codigoestadobien")> _
    Public Property Codigoestadobien() As String
        Get
            Return Me._strCodigoestadobien
        End Get
        Set(ByVal value As String)
            Me._strCodigoestadobien = value
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

    <XmlElement("ValorBien")> _
    Public Property ValorBien() As String
        Get
            Return _strValorBien
        End Get
        Set(ByVal value As String)
            _strValorBien = value
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

    <XmlElement("NumeroContrato")> _
    Public Property NumeroContrato() As String
        Get
            Return Me._strNumeroContrato
        End Get
        Set(ByVal value As String)
            Me._strNumeroContrato = value
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

    <XmlElement("RazonSocial")> _
    Public Property RazonSocial() As String
        Get
            Return Me._strRazonSocial
        End Get
        Set(ByVal value As String)
            Me._strRazonSocial = value
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

    <XmlElement("Departamento")> _
    Public Property FechaTransferencia() As Nullable(Of DateTime)
        Get
            Return Me._strFechaTransferencia
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._strFechaTransferencia = value
        End Set
    End Property

    <XmlElement("EstadoContrato")> _
    Public Property EstadoContrato() As String
        Get
            Return Me._strEstadoContrato
        End Get
        Set(ByVal value As String)
            Me._strEstadoContrato = value
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

    <XmlElement("NumeroDocumento")> _
   Public Property NumeroDocumento() As String
        Get
            Return Me._strNumeroDocumento
        End Get
        Set(ByVal value As String)
            Me._strNumeroDocumento = value
        End Set
    End Property
    <XmlElement("Kardex")> _
Public Property Kardex() As String
        Get
            Return Me._strKardex
        End Get
        Set(ByVal value As String)
            Me._strKardex = value
        End Set
    End Property
    <XmlElement("TipoBien")> _
Public Property TipoBien() As String
        Get
            Return Me._strTipoBien
        End Get
        Set(ByVal value As String)
            Me._strTipoBien = value
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
    <XmlElement("EjecutivoBanca")> _
Public Property EjecutivoBanca() As String
        Get
            Return Me._strEjecutivoBanca
        End Get
        Set(ByVal value As String)
            Me._strEjecutivoBanca = value
        End Set
    End Property
    <XmlElement("ObservacionContrato")> _
Public Property ObservacionContrato() As String
        Get
            Return Me._strObservacionContrato
        End Get
        Set(ByVal value As String)
            Me._strObservacionContrato = value
        End Set
    End Property
    <XmlElement("ObservacionBien")> _
Public Property ObservacionBien() As String
        Get
            Return Me._strObservacionBien
        End Get
        Set(ByVal value As String)
            Me._strObservacionBien = value
        End Set
    End Property

    <XmlElement("Moneda")> _
    Public Property Moneda() As String
        Get
            Return Me._strMoneda
        End Get
        Set(ByVal value As String)
            Me._strMoneda = value
        End Set
    End Property

    <XmlElement("DescripcionBien")> _
    Public Property DescripcionBien() As String
        Get
            Return Me._strDescripcionBien
        End Get
        Set(ByVal value As String)
            Me._strDescripcionBien = value
        End Set
    End Property

    <XmlElement("ClasificacionBien")> _
    Public Property ClasificacionBien() As String
        Get
            Return Me._strClasificacionBien
        End Get
        Set(ByVal value As String)
            Me._strClasificacionBien = value
        End Set
    End Property

    <XmlElement("MontoValorBien")> _
    Public Property MontoValorBien() As Decimal
        Get
            Return Me._decMontoValorBien
        End Get
        Set(ByVal value As Decimal)
            Me._decMontoValorBien = value
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

    <XmlElement("EstadoBien")> _
    Public Property EstadoBien() As String
        Get
            Return Me._strEstadoBien
        End Get
        Set(ByVal value As String)
            Me._strEstadoBien = value
        End Set
    End Property

    <XmlElement("PartidaRegistral")> _
    Public Property PartidaRegistral() As String
        Get
            Return Me._strPartidaRegistral
        End Get
        Set(ByVal value As String)
            Me._strPartidaRegistral = value
        End Set
    End Property

    <XmlElement("OficinaRegistral")> _
    Public Property OficinaRegistral() As String
        Get
            Return Me._strOficinaRegistral
        End Get
        Set(ByVal value As String)
            Me._strOficinaRegistral = value
        End Set
    End Property

    <XmlElement("Comentario")> _
    Public Property Comentario() As String
        Get
            Return Me._strComentario
        End Get
        Set(ByVal value As String)
            Me._strComentario = value
        End Set
    End Property

    <XmlElement("Placa")> _
    Public Property Placa() As String
        Get
            Return Me._strPlaca
        End Get
        Set(ByVal value As String)
            Me._strPlaca = value
        End Set
    End Property

    <XmlElement("PlacaAntigua")> _
    Public Property PlacaAntigua() As String
        Get
            Return Me._strPlacaAntigua
        End Get
        Set(ByVal value As String)
            Me._strPlacaAntigua = value
        End Set
    End Property

    <XmlElement("Anio")> _
    Public Property Anio() As Integer
        Get
            Return Me._intAnio
        End Get
        Set(ByVal value As Integer)
            Me._intAnio = value
        End Set
    End Property

    <XmlElement("NroSerie")> _
    Public Property NroSerie() As String
        Get
            Return Me._strNroSerie
        End Get
        Set(ByVal value As String)
            Me._strNroSerie = value
        End Set
    End Property

    <XmlElement("NroMotor")> _
    Public Property NroMotor() As String
        Get
            Return Me._strNroMotor
        End Get
        Set(ByVal value As String)
            Me._strNroMotor = value
        End Set
    End Property

    <XmlElement("NroMotor")> _
    Public Property Marca() As String
        Get
            Return Me._strMarca
        End Get
        Set(ByVal value As String)
            Me._strMarca = value
        End Set
    End Property

    <XmlElement("Modelo")> _
    Public Property Modelo() As String
        Get
            Return Me._strModelo
        End Get
        Set(ByVal value As String)
            Me._strModelo = value
        End Set
    End Property

    <XmlElement("Color")> _
    Public Property Color() As String
        Get
            Return Me._strColor
        End Get
        Set(ByVal value As String)
            Me._strColor = value
        End Set
    End Property

    <XmlElement("Carroceria")> _
    Public Property Carroceria() As String
        Get
            Return Me._strCarroceria
        End Get
        Set(ByVal value As String)
            Me._strCarroceria = value
        End Set
    End Property
    <XmlElement("CodTipoCarroceria")> _
 Public Property CodTipoCarroceria() As String
        Get
            Return Me._strcodTipoCarroceria
        End Get
        Set(ByVal value As String)
            Me._strcodTipoCarroceria = value
        End Set
    End Property
    <XmlElement("Medidas")> _
    Public Property Medidas() As String
        Get
            Return Me._strMedidas
        End Get
        Set(ByVal value As String)
            Me._strMedidas = value
        End Set
    End Property


    <XmlElement("Flag")> _
    Public Property Flag() As Integer
        Get
            Return Me._intFlag
        End Get
        Set(ByVal value As Integer)
            Me._intFlag = value
        End Set
    End Property

    <XmlElement("DepartamentoNombre")> _
    Public Property DepartamentoNombre() As String
        Get
            Return Me._strDepartamentoNombre
        End Get
        Set(ByVal value As String)
            Me._strDepartamentoNombre = value
        End Set
    End Property

    <XmlElement("ProvinciaNombre")> _
    Public Property ProvinciaNombre() As String
        Get
            Return Me._strProvinciaNombre
        End Get
        Set(ByVal value As String)
            Me._strProvinciaNombre = value
        End Set
    End Property

    <XmlElement("DistritoNombre")> _
    Public Property DistritoNombre() As String
        Get
            Return Me._strDistritoNombre
        End Get
        Set(ByVal value As String)
            Me._strDistritoNombre = value
        End Set
    End Property


    'IBK - RPH
    <XmlElement("Precioventa")> _
   Public Property Precioventa() As Nullable(Of Decimal)
        Get
            Return Me._decPrecioventa
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPrecioventa = value
        End Set
    End Property
    <XmlElement("CodigoSubTipoContrato")> _
    Public Property CodigoSubTipoContrato() As String
        Get
            Return Me._strCodigoSubTipoContrato
        End Get
        Set(ByVal value As String)
            Me._strCodigoSubTipoContrato = value
        End Set
    End Property
    <XmlElement("AudUsuarioCreacion")> _
    Public Property AudUsuarioCreacion() As String
        Get
            Return Me._strAudusuariocreacion
        End Get
        Set(ByVal value As String)
            Me._strAudusuariocreacion = value
        End Set
    End Property
    <XmlElement("PrecioTotal")> _
  Public Property PrecioTotal() As Nullable(Of Decimal)
        Get
            Return Me._decPreciototal
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPreciototal = value
        End Set
    End Property
 
    'Fin
    'Inicio IBK JJM
    <XmlElement("Municipalidad")> _
    Public Property Municipalidad() As String
        Get
            Return _strMunicipalidad
        End Get
        Set(ByVal value As String)
            _strMunicipalidad = value
        End Set
    End Property
    'Fin IBK JJM




#End Region

End Class

''' <summary>
''' Clase que hereda de List(Of ESolicitudcreditoestructura) para la implementación de la lista
''' </summary>
''' <remarks></remarks>
<Serializable(), XmlRoot("ListESolicitudcreditoestructura")> _
Public Class ListESolicitudcreditoestructura
    Inherits List(Of ESolicitudcreditoestructura)

End Class