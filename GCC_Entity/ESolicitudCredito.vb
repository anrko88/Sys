Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Solicitudcredito
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("ESolicitudcredito")> _
Public Class ESolicitudcredito

#Region " Atributos "

    Private _strCodsolicitudcredito As String
    Private _strCodproductofinancieroactivo As String
    Private _strCodproductofinancieropasivo As String
    Private _dtmFechasolicitudcredito As Nullable(Of DateTime)
    Private _strTipoproductofinancieroactivo As String
    Private _strTipoprocedenciacofide As String
    Private _strCodunico As String
    Private _strCodsubprestatario As String
    Private _strCodmoneda As String
    Private _decMontofinanciamiento As Nullable(Of Decimal)
    Private _strDescripinversion As String
    Private _strTipocondicionfinanciera As String
    Private _strDescripobservaciones As String
    Private _strEstadosolicitudcredito As String
    Private _strCodmodulooperacion As String
    Private _strTipooperacion As String
    Private _dtmFecharegistro As Nullable(Of DateTime)
    Private _strCodusuario As String
    Private _strTextoaudicreacion As String
    Private _strTextoaudimodi As String
    Private _strTipoidentificacionoperacion As String
    Private _dtmFechavalor As Nullable(Of DateTime)
    Private _strCodplaza As String
    Private _strNrolinea As String
    Private _strCodoperacionpasiva As String
    Private _decMontoigv As Nullable(Of Decimal)
    Private _dtmFechaactivacion As Nullable(Of DateTime)
    Private _strSctipocliente As String
    Private _intScmescumplim As Nullable(Of Integer)
    Private _intSctipcredito As Nullable(Of Integer)
    Private _strEjecutivoleasing As String
    Private _strCodtipoleasing As String
    Private _strCodbanca As String
    Private _intBasileatipoexposicionanterior As Nullable(Of Integer)
    Private _intBasileatipoexposicion As Nullable(Of Integer)
    Private _intCalificacionanterior As Nullable(Of Integer)
    Private _intCalificacion As Nullable(Of Integer)
    Private _strCodigocotizacion As String
    Private _strCodigoestadocontrato As String
    Private _strCodigofirmanotaria As String
    Private _dtmFechamaxdisponible As Nullable(Of DateTime)
    Private _dtmFechamaxactivacion As Nullable(Of DateTime)
    Private _strRegistropublico As String
    Private _strClasificacioncontrato As String
    Private _intPeridodisponible As Nullable(Of Integer)
    Private _strUso As String
    Private _strUbicacion As String
    Private _strNombreconyuge As String
    Private _strCodigotipodocconyuge As String
    Private _strNumerodocumentoconyuge As String
    Private _strDocumentoseparacion As String
    Private _dtmFechafirmanotaria As Nullable(Of DateTime)

    Private _intFlagTerminoRecepDocumentoClie As Nullable(Of Integer)
    Private _dtmFechaTerminoRecepDocumentoClie As Nullable(Of DateTime)
    Private _intFlagTerminoRecepDocumentoProv As Nullable(Of Integer)
    Private _dtmFechaTerminoRecepDocumentoProv As Nullable(Of DateTime)
    Private _strDescripcionBien As String

    Private _dtmFechaContrato As Nullable(Of DateTime)
    Private _strTipoRubroFinanciamiento As String
    Private _strCodigoTipoInmueble As String
    Private _strCodigoClasificacionContrato As String
    Private _intFlagRegistroPublico As Nullable(Of Integer)
    Private _dtmFechaRegistroPublico As Nullable(Of DateTime)
    Private _intFlagFirmaNotaria As Nullable(Of Integer)
    Private _strArchivoContratoAdjunto As String

    Private _strCodigoProcedencia As String
    Private _strClasificacionBien As String

    Private _strcodigolugarfirmacontrato As String
    Private _strcodigoubigeolugarfirma As String
    Private _intFlagEnvioLegal As Integer
    Private _strAudUsuarioModificacion As String

    Private _strClienteRazonSocial As String
    Private _strClienteDomicilioLegal As String


    ' Estado civil del cliente
    Private _strCodigoEstadoCivil As String

    ' Indica si algún elemento del contrato a sido modificado.
    Private _flagModificado As Nullable(Of Boolean)

    Private _strFechafirmanotaria As String
    Private _strUbigeo As String


    Private _strFechaProbableFinObra As Nullable(Of DateTime)
    Private _strFechaRealFinObra As Nullable(Of DateTime)
    Private _strFechaInscripcionMunicipal As Nullable(Of DateTime)
    Private _strFechaEnvioNotaria As Nullable(Of DateTime)
    Private _strFechaPropiedad As Nullable(Of DateTime)
    Private _strFechaInscripcionRegistral As Nullable(Of DateTime)

    Private _strOficinaRegistral As String
    Private _strCodigoNotaria As String
    Private _strCodEstadoInscripcionRrPp As String
    Private _strCodEstadoMunicipal As String
    Private _strCodEstadoTransferencia As String
    Private _strObservacionContrato As String

    Private _intFlagActivacionLeasing As Nullable(Of Integer)

    'Inicio RPR 25/12/2012
    Private _strNombreTipoPersona As String
    Private _strNombreTipoDocIdentificacion As String
    Private _strNroDocIdentificacion As String
    Private _strSubTipoContrato As String
    Private _strNombreMonedaAPP As String
    Private _strNombreEjecutivoLeasing As String

    Private _strNombreBanca As String
    Private _strDesZonal As String
    Private _strNombreSectorista As String
    Private _decPorcenTasaActiva As Decimal
    Private _strEstadoOperacionActiva As String
    Private _strNombreEstadoOperacionActiva As String

    Private _strCodError As String
    Private _strMsgError As String
    'Fin RPR

    Private _strCodigoUsuario As String
    Private _strNombreUsuario As String
    Private _strPerfilUsuario As String

#End Region

#Region " Propiedades "

    <XmlElement("Modificado")> _
    Public Property Modificado() As Nullable(Of Boolean)
        Get
            Return Me._flagModificado
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            Me._flagModificado = value
        End Set
    End Property

    <XmlElement("CodigoEstadoCivil")> _
    Public Property CodigoEstadoCivil() As String
        Get
            Return Me._strCodigoEstadoCivil
        End Get
        Set(ByVal value As String)
            Me._strCodigoEstadoCivil = value
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

    <XmlElement("Codproductofinancieroactivo")> _
    Public Property Codproductofinancieroactivo() As String
        Get
            Return Me._strCodproductofinancieroactivo
        End Get
        Set(ByVal value As String)
            Me._strCodproductofinancieroactivo = value
        End Set
    End Property

    <XmlElement("Codproductofinancieropasivo")> _
    Public Property Codproductofinancieropasivo() As String
        Get
            Return Me._strCodproductofinancieropasivo
        End Get
        Set(ByVal value As String)
            Me._strCodproductofinancieropasivo = value
        End Set
    End Property

    <XmlElement("Fechasolicitudcredito")> _
    Public Property Fechasolicitudcredito() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechasolicitudcredito
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechasolicitudcredito = value
        End Set
    End Property

    <XmlElement("Tipoproductofinancieroactivo")> _
    Public Property Tipoproductofinancieroactivo() As String
        Get
            Return Me._strTipoproductofinancieroactivo
        End Get
        Set(ByVal value As String)
            Me._strTipoproductofinancieroactivo = value
        End Set
    End Property

    <XmlElement("Tipoprocedenciacofide")> _
    Public Property Tipoprocedenciacofide() As String
        Get
            Return Me._strTipoprocedenciacofide
        End Get
        Set(ByVal value As String)
            Me._strTipoprocedenciacofide = value
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

    <XmlElement("Codsubprestatario")> _
    Public Property Codsubprestatario() As String
        Get
            Return Me._strCodsubprestatario
        End Get
        Set(ByVal value As String)
            Me._strCodsubprestatario = value
        End Set
    End Property

    <XmlElement("Codmoneda")> _
    Public Property Codmoneda() As String
        Get
            Return Me._strCodmoneda
        End Get
        Set(ByVal value As String)
            Me._strCodmoneda = value
        End Set
    End Property

    <XmlElement("Montofinanciamiento")> _
    Public Property Montofinanciamiento() As Nullable(Of Decimal)
        Get
            Return Me._decMontofinanciamiento
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontofinanciamiento = value
        End Set
    End Property

    <XmlElement("Descripinversion")> _
    Public Property Descripinversion() As String
        Get
            Return Me._strDescripinversion
        End Get
        Set(ByVal value As String)
            Me._strDescripinversion = value
        End Set
    End Property

    <XmlElement("Tipocondicionfinanciera")> _
    Public Property Tipocondicionfinanciera() As String
        Get
            Return Me._strTipocondicionfinanciera
        End Get
        Set(ByVal value As String)
            Me._strTipocondicionfinanciera = value
        End Set
    End Property

    <XmlElement("Descripobservaciones")> _
    Public Property Descripobservaciones() As String
        Get
            Return Me._strDescripobservaciones
        End Get
        Set(ByVal value As String)
            Me._strDescripobservaciones = value
        End Set
    End Property

    <XmlElement("Estadosolicitudcredito")> _
    Public Property Estadosolicitudcredito() As String
        Get
            Return Me._strEstadosolicitudcredito
        End Get
        Set(ByVal value As String)
            Me._strEstadosolicitudcredito = value
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

    <XmlElement("Tipoidentificacionoperacion")> _
    Public Property Tipoidentificacionoperacion() As String
        Get
            Return Me._strTipoidentificacionoperacion
        End Get
        Set(ByVal value As String)
            Me._strTipoidentificacionoperacion = value
        End Set
    End Property

    <XmlElement("Fechavalor")> _
    Public Property Fechavalor() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechavalor
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechavalor = value
        End Set
    End Property

    <XmlElement("Codplaza")> _
    Public Property Codplaza() As String
        Get
            Return Me._strCodplaza
        End Get
        Set(ByVal value As String)
            Me._strCodplaza = value
        End Set
    End Property

    <XmlElement("Nrolinea")> _
    Public Property Nrolinea() As String
        Get
            Return Me._strNrolinea
        End Get
        Set(ByVal value As String)
            Me._strNrolinea = value
        End Set
    End Property

    <XmlElement("Codoperacionpasiva")> _
    Public Property Codoperacionpasiva() As String
        Get
            Return Me._strCodoperacionpasiva
        End Get
        Set(ByVal value As String)
            Me._strCodoperacionpasiva = value
        End Set
    End Property

    <XmlElement("Montoigv")> _
    Public Property Montoigv() As Nullable(Of Decimal)
        Get
            Return Me._decMontoigv
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoigv = value
        End Set
    End Property

    <XmlElement("Fechaactivacion")> _
    Public Property Fechaactivacion() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaactivacion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaactivacion = value
        End Set
    End Property

    <XmlElement("Sctipocliente")> _
    Public Property Sctipocliente() As String
        Get
            Return Me._strSctipocliente
        End Get
        Set(ByVal value As String)
            Me._strSctipocliente = value
        End Set
    End Property

    <XmlElement("Scmescumplim")> _
    Public Property Scmescumplim() As Nullable(Of Integer)
        Get
            Return Me._intScmescumplim
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intScmescumplim = value
        End Set
    End Property

    <XmlElement("Sctipcredito")> _
    Public Property Sctipcredito() As Nullable(Of Integer)
        Get
            Return Me._intSctipcredito
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intSctipcredito = value
        End Set
    End Property

    <XmlElement("Ejecutivoleasing")> _
    Public Property Ejecutivoleasing() As String
        Get
            Return Me._strEjecutivoleasing
        End Get
        Set(ByVal value As String)
            Me._strEjecutivoleasing = value
        End Set
    End Property

    <XmlElement("Codtipoleasing")> _
    Public Property Codtipoleasing() As String
        Get
            Return Me._strCodtipoleasing
        End Get
        Set(ByVal value As String)
            Me._strCodtipoleasing = value
        End Set
    End Property

    <XmlElement("Codbanca")> _
    Public Property Codbanca() As String
        Get
            Return Me._strCodbanca
        End Get
        Set(ByVal value As String)
            Me._strCodbanca = value
        End Set
    End Property

    <XmlElement("Basileatipoexposicionanterior")> _
    Public Property Basileatipoexposicionanterior() As Nullable(Of Integer)
        Get
            Return Me._intBasileatipoexposicionanterior
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intBasileatipoexposicionanterior = value
        End Set
    End Property

    <XmlElement("Basileatipoexposicion")> _
    Public Property Basileatipoexposicion() As Nullable(Of Integer)
        Get
            Return Me._intBasileatipoexposicion
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intBasileatipoexposicion = value
        End Set
    End Property

    <XmlElement("Calificacionanterior")> _
    Public Property Calificacionanterior() As Nullable(Of Integer)
        Get
            Return Me._intCalificacionanterior
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCalificacionanterior = value
        End Set
    End Property

    <XmlElement("Calificacion")> _
    Public Property Calificacion() As Nullable(Of Integer)
        Get
            Return Me._intCalificacion
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCalificacion = value
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

    <XmlElement("Codigoestadocontrato")> _
    Public Property Codigoestadocontrato() As String
        Get
            Return Me._strCodigoestadocontrato
        End Get
        Set(ByVal value As String)
            Me._strCodigoestadocontrato = value
        End Set
    End Property

    <XmlElement("Codigofirmanotaria")> _
    Public Property Codigofirmanotaria() As String
        Get
            Return Me._strCodigofirmanotaria
        End Get
        Set(ByVal value As String)
            Me._strCodigofirmanotaria = value
        End Set
    End Property

    <XmlElement("Fechamaxdisponible")> _
    Public Property Fechamaxdisponible() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechamaxdisponible
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechamaxdisponible = value
        End Set
    End Property

    <XmlElement("Fechamaxactivacion")> _
    Public Property Fechamaxactivacion() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechamaxactivacion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechamaxactivacion = value
        End Set
    End Property

    <XmlElement("Registropublico")> _
    Public Property Registropublico() As String
        Get
            Return Me._strRegistropublico
        End Get
        Set(ByVal value As String)
            Me._strRegistropublico = value
        End Set
    End Property

    <XmlElement("Clasificacioncontrato")> _
    Public Property Clasificacioncontrato() As String
        Get
            Return Me._strClasificacioncontrato
        End Get
        Set(ByVal value As String)
            Me._strClasificacioncontrato = value
        End Set
    End Property

    <XmlElement("Peridodisponible")> _
    Public Property Peridodisponible() As Nullable(Of Integer)
        Get
            Return Me._intPeridodisponible
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intPeridodisponible = value
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

    <XmlElement("Nombreconyuge")> _
    Public Property Nombreconyuge() As String
        Get
            Return Me._strNombreconyuge
        End Get
        Set(ByVal value As String)
            Me._strNombreconyuge = value
        End Set
    End Property

    <XmlElement("Codigotipodocconyuge")> _
    Public Property Codigotipodocconyuge() As String
        Get
            Return Me._strCodigotipodocconyuge
        End Get
        Set(ByVal value As String)
            Me._strCodigotipodocconyuge = value
        End Set
    End Property

    <XmlElement("Numerodocumentoconyuge")> _
    Public Property Numerodocumentoconyuge() As String
        Get
            Return Me._strNumerodocumentoconyuge
        End Get
        Set(ByVal value As String)
            Me._strNumerodocumentoconyuge = value
        End Set
    End Property

    <XmlElement("Documentoseparacion")> _
    Public Property Documentoseparacion() As String
        Get
            Return Me._strDocumentoseparacion
        End Get
        Set(ByVal value As String)
            Me._strDocumentoseparacion = value
        End Set
    End Property

    <XmlElement("Fechafirmanotaria")> _
    Public Property Fechafirmanotaria() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechafirmanotaria
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechafirmanotaria = value
        End Set
    End Property
    <XmlElement("SFechafirmanotaria")> _
    Public Property SFechafirmanotaria() As String
        Get
            Return Me._strFechafirmanotaria
        End Get
        Set(ByVal value As String)
            Me._strFechafirmanotaria = value
        End Set
    End Property

    <XmlElement("FlagTerminoRecepDocumentoClie")> _
    Public Property FlagTerminoRecepDocumentoClie() As Nullable(Of Integer)
        Get
            Return Me._intFlagTerminoRecepDocumentoClie
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intFlagTerminoRecepDocumentoClie = value
        End Set
    End Property

    <XmlElement("FlagTerminoRecepDocumentoProv")> _
    Public Property FlagTerminoRecepDocumentoProv() As Nullable(Of Integer)
        Get
            Return Me._intFlagTerminoRecepDocumentoProv
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intFlagTerminoRecepDocumentoProv = value
        End Set
    End Property

    <XmlElement("FechaTerminoRecepDocumentoClie")> _
    Public Property FechaTerminoRecepDocumentoClie() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaTerminoRecepDocumentoClie
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaTerminoRecepDocumentoClie = value
        End Set
    End Property

    <XmlElement("FechaTerminoRecepDocumentoProv")> _
    Public Property FechaTerminoRecepDocumentoProv() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaTerminoRecepDocumentoProv
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaTerminoRecepDocumentoProv = value
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


    <XmlElement("CodigoProcedencia")> _
    Public Property CodigoProcedencia() As String
        Get
            Return Me._strCodigoProcedencia
        End Get
        Set(ByVal value As String)
            Me._strCodigoProcedencia = value
        End Set
    End Property

    <XmlElement("ArchivoContratoAdjunto")> _
    Public Property ArchivoContratoAdjunto() As String
        Get
            Return Me._strArchivoContratoAdjunto
        End Get
        Set(ByVal value As String)
            Me._strArchivoContratoAdjunto = value
        End Set
    End Property

    <XmlElement("CodigoClasificacionContrato")> _
    Public Property CodigoClasificacionContrato() As String
        Get
            Return Me._strCodigoClasificacionContrato
        End Get
        Set(ByVal value As String)
            Me._strCodigoClasificacionContrato = value
        End Set
    End Property

    <XmlElement("CodigoTipoInmueble")> _
    Public Property CodigoTipoInmueble() As String
        Get
            Return Me._strCodigoTipoInmueble
        End Get
        Set(ByVal value As String)
            Me._strCodigoTipoInmueble = value
        End Set
    End Property

    <XmlElement("TipoRubroFinanciamiento")> _
    Public Property TipoRubroFinanciamiento() As String
        Get
            Return Me._strTipoRubroFinanciamiento
        End Get
        Set(ByVal value As String)
            Me._strTipoRubroFinanciamiento = value
        End Set
    End Property

    <XmlElement("FlagRegistroPublico")> _
    Public Property FlagRegistroPublico() As Nullable(Of Integer)
        Get
            Return Me._intFlagRegistroPublico
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intFlagRegistroPublico = value
        End Set
    End Property

    <XmlElement("FlagFirmaNotaria")> _
    Public Property FlagFirmaNotaria() As Nullable(Of Integer)
        Get
            Return Me._intFlagFirmaNotaria
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intFlagFirmaNotaria = value
        End Set
    End Property

    <XmlElement("FechaRegistroPublico")> _
    Public Property FechaRegistroPublico() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaRegistroPublico
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaRegistroPublico = value
        End Set
    End Property

    <XmlElement("FechaContrato")> _
    Public Property FechaContrato() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaContrato
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaContrato = value
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


    <XmlElement("codigolugarfirmacontrato")> _
    Public Property codigolugarfirmacontrato() As String
        Get
            Return Me._strcodigolugarfirmacontrato
        End Get
        Set(ByVal value As String)
            Me._strcodigolugarfirmacontrato = value
        End Set
    End Property

    <XmlElement("codigoubigeolugarfirma")> _
    Public Property codigoubigeolugarfirma() As String
        Get
            Return Me._strcodigoubigeolugarfirma
        End Get
        Set(ByVal value As String)
            Me._strcodigoubigeolugarfirma = value
        End Set
    End Property

    <XmlElement("FlagEnvioLegal")> _
    Public Property FlagEnvioLegal() As Nullable(Of Integer)
        Get
            Return Me._intFlagEnvioLegal
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intFlagEnvioLegal = value
        End Set
    End Property


    <XmlElement("AudUsuarioModificacion")> _
    Public Property AudUsuarioModificacion() As String
        Get
            Return Me._strAudUsuarioModificacion
        End Get
        Set(ByVal value As String)
            Me._strAudUsuarioModificacion = value
        End Set
    End Property

    <XmlElement("Ubigeo")> _
    Public Property Ubigeo() As String
        Get
            Return Me._strUbigeo
        End Get
        Set(ByVal value As String)
            Me._strUbigeo = value
        End Set
    End Property
    <XmlElement("ClienteRazonSocial")> _
    Public Property ClienteRazonSocial() As String
        Get
            Return _strClienteRazonSocial
        End Get
        Set(ByVal value As String)
            _strClienteRazonSocial = value
        End Set
    End Property
    <XmlElement("ClienteDomicilioLegal")> _
    Public Property ClienteDomicilioLegal() As String
        Get
            Return Me._strClienteDomicilioLegal
        End Get
        Set(ByVal value As String)
            Me._strClienteDomicilioLegal = value
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
    <XmlElement("FechaRealFinObra")> _
    Public Property FechaRealFinObra() As Nullable(Of DateTime)
        Get
            Return _strFechaRealFinObra
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _strFechaRealFinObra = value
        End Set
    End Property
    <XmlElement("FechaInscripcionMunicipal")> _
    Public Property FechaInscripcionMunicipal() As Nullable(Of DateTime)
        Get
            Return _strFechaInscripcionMunicipal
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _strFechaInscripcionMunicipal = value
        End Set
    End Property
    <XmlElement("FechaEnvioNotaria")> _
    Public Property FechaEnvioNotaria() As Nullable(Of DateTime)
        Get
            Return _strFechaEnvioNotaria
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _strFechaEnvioNotaria = value
        End Set
    End Property
    <XmlElement("FechaPropiedad")> _
    Public Property FechaPropiedad() As Nullable(Of DateTime)
        Get
            Return _strFechaPropiedad
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _strFechaPropiedad = value
        End Set
    End Property
    <XmlElement("FechaInscripcionRegistral")> _
    Public Property FechaInscripcionRegistral() As Nullable(Of DateTime)
        Get
            Return _strFechaInscripcionRegistral
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _strFechaInscripcionRegistral = value
        End Set
    End Property
    <XmlElement("OficinaRegistral")> _
    Public Property OficinaRegistral() As String
        Get
            Return _strOficinaRegistral
        End Get
        Set(ByVal value As String)
            _strOficinaRegistral = value
        End Set
    End Property
    <XmlElement("CodigoNotaria")> _
    Public Property CodigoNotaria() As String
        Get
            Return _strCodigoNotaria
        End Get
        Set(ByVal value As String)
            _strCodigoNotaria = value
        End Set
    End Property
    <XmlElement("CodEstadoInscripcionRrPp")> _
    Public Property CodEstadoInscripcionRrPp() As String
        Get
            Return _strCodEstadoInscripcionRrPp
        End Get
        Set(ByVal value As String)
            _strCodEstadoInscripcionRrPp = value
        End Set
    End Property
    <XmlElement("CodEstadoMunicipal")> _
    Public Property CodEstadoMunicipal() As String
        Get
            Return _strCodEstadoMunicipal
        End Get
        Set(ByVal value As String)
            _strCodEstadoMunicipal = value
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
    <XmlElement("ObservacionContrato")> _
    Public Property ObservacionContrato() As String
        Get
            Return _strObservacionContrato
        End Get
        Set(ByVal value As String)
            _strObservacionContrato = value
        End Set
    End Property

    <XmlElement("FlagActivacionLeasing")> _
    Public Property FlagActivacionLeasing() As Nullable(Of Integer)
        Get
            Return Me._intFlagActivacionLeasing
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intFlagActivacionLeasing = value
        End Set
    End Property

    'Inicio RPR 25/12/2012
    <XmlElement("NombreTipoPersona")> _
    Public Property NombreTipoPersona() As String
        Get
            Return _strNombreTipoPersona
        End Get
        Set(ByVal value As String)
            _strNombreTipoPersona = value
        End Set
    End Property

    <XmlElement("NombreTipoDocIdentificacion")> _
    Public Property NombreTipoDocIdentificacion() As String
        Get
            Return _strNombreTipoDocIdentificacion
        End Get
        Set(ByVal value As String)
            _strNombreTipoDocIdentificacion = value
        End Set
    End Property

    <XmlElement("NroDocIdentificacion")> _
    Public Property NroDocIdentificacion() As String
        Get
            Return _strNroDocIdentificacion
        End Get
        Set(ByVal value As String)
            _strNroDocIdentificacion = value
        End Set
    End Property

    <XmlElement("SubTipoContrato")> _
    Public Property SubTipoContrato() As String
        Get
            Return _strSubTipoContrato
        End Get
        Set(ByVal value As String)
            _strSubTipoContrato = value
        End Set
    End Property

    <XmlElement("NombreMonedaAPP")> _
    Public Property NombreMonedaAPP() As String
        Get
            Return _strNombreMonedaAPP
        End Get
        Set(ByVal value As String)
            _strNombreMonedaAPP = value
        End Set
    End Property

    <XmlElement("NombreEjecutivoLeasing")> _
    Public Property NombreEjecutivoLeasing() As String
        Get
            Return _strNombreEjecutivoLeasing
        End Get
        Set(ByVal value As String)
            _strNombreEjecutivoLeasing = value
        End Set
    End Property

    <XmlElement("NombreBanca")> _
  Public Property NombreBanca() As String
        Get
            Return _strNombreBanca
        End Get
        Set(ByVal value As String)
            _strNombreBanca = value
        End Set
    End Property

    <XmlElement("DesZonal")> _
    Public Property DesZonal() As String
        Get
            Return _strDesZonal
        End Get
        Set(ByVal value As String)
            _strDesZonal = value
        End Set
    End Property

    <XmlElement("NombreSectorista")> _
    Public Property NombreSectorista() As String
        Get
            Return _strNombreSectorista
        End Get
        Set(ByVal value As String)
            _strNombreSectorista = value
        End Set
    End Property

    <XmlElement("PorcenTasaActiva")> _
    Public Property PorcenTasaActiva() As Decimal
        Get
            Return _decPorcenTasaActiva
        End Get
        Set(ByVal value As Decimal)
            _decPorcenTasaActiva = value
        End Set
    End Property

    <XmlElement("EstadoOperacionActiva")> _
    Public Property EstadoOperacionActiva() As String
        Get
            Return _strEstadoOperacionActiva
        End Get
        Set(ByVal value As String)
            _strEstadoOperacionActiva = value
        End Set
    End Property

    <XmlElement("NombreEstadoOperacionActiva")> _
    Public Property NombreEstadoOperacionActiva() As String
        Get
            Return _strNombreEstadoOperacionActiva
        End Get
        Set(ByVal value As String)
            _strNombreEstadoOperacionActiva = value
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
    'Fin RPR




    <XmlElement("CodigoUsuario")> _
    Public Property CodigoUsuario() As String
        Get
            Return Me._strCodigoUsuario
        End Get
        Set(ByVal value As String)
            Me._strCodigoUsuario = value
        End Set
    End Property

    <XmlElement("NombreUsuario")> _
    Public Property NombreUsuario() As String
        Get
            Return Me._strNombreUsuario
        End Get
        Set(ByVal value As String)
            Me._strNombreUsuario = value
        End Set
    End Property

    <XmlElement("PerfilUsuario")> _
    Public Property PerfilUsuario() As String
        Get
            Return Me._strPerfilUsuario
        End Get
        Set(ByVal value As String)
            Me._strPerfilUsuario = value
        End Set
    End Property

#End Region

End Class