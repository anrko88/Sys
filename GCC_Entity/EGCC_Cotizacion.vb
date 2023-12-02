Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_cotizacion
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_cotizacion")> _
Public Class EGcc_cotizacion

#Region " Atributos "

    Private _strCodigocotizacion As String
    Private _strCodigobanca As String
    Private _strCodigoestadocotizacion As String
    Private _strCodigoejecutivoleasing As String
    Private _strCodigoejecutivobanca As String
    Private _strCodigogrupozonal As String
    Private _intGenerarcarta As Nullable(Of Integer)
    Private _strCodigoclasificacionbien As String
    Private _strCodigoprocedencia As String
    Private _strCodigomoneda As String
    Private _strCodigotipoinmueble As String
    Private _decValorventa As Nullable(Of Decimal)
    Private _decValorventaigv As Nullable(Of Decimal)
    Private _decPrecioventa As Nullable(Of Decimal)
    Private _decCuotainicialporc As Nullable(Of Decimal)
    Private _strCodigoestadobien As String
    Private _decImportecuotainicial As Nullable(Of Decimal)
    Private _decRiesgoneto As Nullable(Of Decimal)
    Private _strCodigotipocronograma As String
    Private _intPlazograciacuota As Nullable(Of Integer)
    Private _strCodigotipograciacuota As String
    Private _dtmFechamaxactivacion As Nullable(Of DateTime)
    Private _intNumerocuotas As Nullable(Of Integer)
    Private _strCodigoperiodicidad As String
    Private _strCodigofrecuenciapago As String
    Private _dtmFechaprimervencimiento As Nullable(Of DateTime)
    Private _decTeaporc As Nullable(Of Decimal)
    Private _decSpreadporc As Nullable(Of Decimal)
    Private _decCostofondoporc As Nullable(Of Decimal)
    Private _decPrecuotaporc As Nullable(Of Decimal)
    Private _intPlazograciaprecuota As Nullable(Of Integer)
    Private _strCodigobientiposeguro As String
    Private _decBienimporteprima As Nullable(Of Decimal)
    Private _intBiennrocuotasfinanciar As Nullable(Of Integer)
    Private _strCodigodesgravamentiposeguro As String
    Private _decDesgravamenimporteprima As Nullable(Of Decimal)
    Private _intDesgravamennrocuotasfinanciar As Nullable(Of Integer)
    Private _decOpcioncompraporc As Nullable(Of Decimal)
    Private _decComisionactivacionporc As Nullable(Of Decimal)
    Private _decComisionestructuracionporc As Nullable(Of Decimal)
    Private _intMostrarteacartas As Nullable(Of Integer)
    Private _intMostrarmontocomision As Nullable(Of Integer)
    Private _decImporteopcioncompra As Nullable(Of Decimal)
    Private _decImportecomisionactivacion As Nullable(Of Decimal)
    Private _decImportecomisionestructuracion As Nullable(Of Decimal)
    Private _intPeriododisponible As Nullable(Of Integer)
    Private _strOtrascomisiones As String
    Private _strCorreoelectronico As String
    Private _dtmFechaingreso As Nullable(Of DateTime)
    Private _intVersioncotizacion As Nullable(Of Integer)
    Private _dtmVersionfecha As Nullable(Of DateTime)
    Private _dtmFechalimitevalidezcotizacion As Nullable(Of DateTime)
    Private _strCodigosupervisoraprobo As String
    Private _strNumerolinea As String
    Private _decTasalinea As Nullable(Of Decimal)
    Private _intNumerocronograma As Nullable(Of Integer)
    Private _strCodigotipopersona As String
    Private _strCodproductofinancieroactivo As String
    Private _strCodproductofinancieropasivo As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String
    Private _dtmFechacarta As Nullable(Of DateTime)
    Private _strCodsubprestatario As String

    Private _dtmFechaOfertaValida As Nullable(Of DateTime)
    Private _strCodigoSubTipoContrato
    Private _intFlagLinea As Nullable(Of Integer)
    Private _intFlagCliente As Nullable(Of Integer)
    Private _strNombreCliente As String
    Private _strCodigoTipoDocumento As String
    Private _strNumeroDocumento As String
    Private _strCodigoTipoBien As String
    Private _strDescripcionProveedor As String

    Private _strCodUnico As String
    Private _dtmFechaInicio As Nullable(Of DateTime)
    Private _dtmFechaFin As Nullable(Of DateTime)

    'Datos Para la Busqueda del listado 
    Private _strCodigoContrato As String
    Private _strCodigoZonal As String
    Private _strCodigoClasificacionContrato As String

    'Datos Contacto
    Private _intCodigoContacto As Nullable(Of Integer)
    Private _strNombreContacto As String
    Private _strCorreoContacto As String

    'Version
    Private _intAplicaVersionamiento As Nullable(Of Integer)

    'MotivoRechazo
    Private _strMotivoRechazo As String

    'Contrato
    Private _strCodigoEstadoContrato As String

    'Nuevos
    Private _strDesEjecutivoBanca As String
    Private _strDesZonal As String
    Private _decMontoPorcIGV As Nullable(Of Decimal)
    Private _strFlagCuotaInicial As String

    Private _strDireccionCliente As String

    Private _strCodigoUsuario As String
    Private _strNombreUsuario As String
    Private _strPerfilUsuario As String

    'IBK - RPH
    Private _strFlagOpcionCompras As String
    Private _strFlagComisionAct As String
    Private _strFlagComisionEst As String
    Private _ArchivoCronograma As String
    'Fin 


#End Region

#Region " Propiedades "

    <XmlElement("Codigocotizacion")> _
    Public Property Codigocotizacion() As String
        Get
            Return Me._strCodigocotizacion
        End Get
        Set(ByVal value As String)
            Me._strCodigocotizacion = value
        End Set
    End Property

    <XmlElement("Codigobanca")> _
    Public Property Codigobanca() As String
        Get
            Return Me._strCodigobanca
        End Get
        Set(ByVal value As String)
            Me._strCodigobanca = value
        End Set
    End Property

    <XmlElement("Codigoestadocotizacion")> _
    Public Property Codigoestadocotizacion() As String
        Get
            Return Me._strCodigoestadocotizacion
        End Get
        Set(ByVal value As String)
            Me._strCodigoestadocotizacion = value
        End Set
    End Property

    <XmlElement("Codigoejecutivoleasing")> _
    Public Property Codigoejecutivoleasing() As String
        Get
            Return Me._strCodigoejecutivoleasing
        End Get
        Set(ByVal value As String)
            Me._strCodigoejecutivoleasing = value
        End Set
    End Property

    <XmlElement("Codigoejecutivobanca")> _
    Public Property Codigoejecutivobanca() As String
        Get
            Return Me._strCodigoejecutivobanca
        End Get
        Set(ByVal value As String)
            Me._strCodigoejecutivobanca = value
        End Set
    End Property

    <XmlElement("Codigogrupozonal")> _
    Public Property Codigogrupozonal() As String
        Get
            Return Me._strCodigogrupozonal
        End Get
        Set(ByVal value As String)
            Me._strCodigogrupozonal = value
        End Set
    End Property

    <XmlElement("Generarcarta")> _
    Public Property Generarcarta() As Nullable(Of Integer)
        Get
            Return Me._intGenerarcarta
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intGenerarcarta = value
        End Set
    End Property

    <XmlElement("Codigoclasificacionbien")> _
    Public Property Codigoclasificacionbien() As String
        Get
            Return Me._strCodigoclasificacionbien
        End Get
        Set(ByVal value As String)
            Me._strCodigoclasificacionbien = value
        End Set
    End Property

    <XmlElement("Codigoprocedencia")> _
    Public Property Codigoprocedencia() As String
        Get
            Return Me._strCodigoprocedencia
        End Get
        Set(ByVal value As String)
            Me._strCodigoprocedencia = value
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

    <XmlElement("Codigotipoinmueble")> _
    Public Property Codigotipoinmueble() As String
        Get
            Return Me._strCodigotipoinmueble
        End Get
        Set(ByVal value As String)
            Me._strCodigotipoinmueble = value
        End Set
    End Property

    <XmlElement("Valorventa")> _
    Public Property Valorventa() As Nullable(Of Decimal)
        Get
            Return Me._decValorventa
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decValorventa = value
        End Set
    End Property

    <XmlElement("Valorventaigv")> _
    Public Property Valorventaigv() As Nullable(Of Decimal)
        Get
            Return Me._decValorventaigv
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decValorventaigv = value
        End Set
    End Property

    <XmlElement("Precioventa")> _
    Public Property Precioventa() As Nullable(Of Decimal)
        Get
            Return Me._decPrecioventa
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPrecioventa = value
        End Set
    End Property

    <XmlElement("Cuotainicialporc")> _
    Public Property Cuotainicialporc() As Nullable(Of Decimal)
        Get
            Return Me._decCuotainicialporc
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decCuotainicialporc = value
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

    <XmlElement("Importecuotainicial")> _
    Public Property Importecuotainicial() As Nullable(Of Decimal)
        Get
            Return Me._decImportecuotainicial
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImportecuotainicial = value
        End Set
    End Property

    <XmlElement("Riesgoneto")> _
    Public Property Riesgoneto() As Nullable(Of Decimal)
        Get
            Return Me._decRiesgoneto
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decRiesgoneto = value
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

    <XmlElement("Fechamaxactivacion")> _
    Public Property Fechamaxactivacion() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechamaxactivacion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechamaxactivacion = value
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

    <XmlElement("Fechaprimervencimiento")> _
    Public Property Fechaprimervencimiento() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaprimervencimiento
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaprimervencimiento = value
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

    <XmlElement("Spreadporc")> _
    Public Property Spreadporc() As Nullable(Of Decimal)
        Get
            Return Me._decSpreadporc
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decSpreadporc = value
        End Set
    End Property

    <XmlElement("Costofondoporc")> _
    Public Property Costofondoporc() As Nullable(Of Decimal)
        Get
            Return Me._decCostofondoporc
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decCostofondoporc = value
        End Set
    End Property

    <XmlElement("Precuotaporc")> _
    Public Property Precuotaporc() As Nullable(Of Decimal)
        Get
            Return Me._decPrecuotaporc
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPrecuotaporc = value
        End Set
    End Property

    <XmlElement("Plazograciaprecuota")> _
    Public Property Plazograciaprecuota() As Nullable(Of Integer)
        Get
            Return Me._intPlazograciaprecuota
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intPlazograciaprecuota = value
        End Set
    End Property

    <XmlElement("Codigobientiposeguro")> _
    Public Property Codigobientiposeguro() As String
        Get
            Return Me._strCodigobientiposeguro
        End Get
        Set(ByVal value As String)
            Me._strCodigobientiposeguro = value
        End Set
    End Property

    <XmlElement("Bienimporteprima")> _
    Public Property Bienimporteprima() As Nullable(Of Decimal)
        Get
            Return Me._decBienimporteprima
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decBienimporteprima = value
        End Set
    End Property

    <XmlElement("Biennrocuotasfinanciar")> _
    Public Property Biennrocuotasfinanciar() As Nullable(Of Integer)
        Get
            Return Me._intBiennrocuotasfinanciar
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intBiennrocuotasfinanciar = value
        End Set
    End Property

    <XmlElement("Codigodesgravamentiposeguro")> _
    Public Property Codigodesgravamentiposeguro() As String
        Get
            Return Me._strCodigodesgravamentiposeguro
        End Get
        Set(ByVal value As String)
            Me._strCodigodesgravamentiposeguro = value
        End Set
    End Property

    <XmlElement("Desgravamenimporteprima")> _
    Public Property Desgravamenimporteprima() As Nullable(Of Decimal)
        Get
            Return Me._decDesgravamenimporteprima
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decDesgravamenimporteprima = value
        End Set
    End Property

    <XmlElement("Desgravamennrocuotasfinanciar")> _
    Public Property Desgravamennrocuotasfinanciar() As Nullable(Of Integer)
        Get
            Return Me._intDesgravamennrocuotasfinanciar
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intDesgravamennrocuotasfinanciar = value
        End Set
    End Property

    <XmlElement("Opcioncompraporc")> _
    Public Property Opcioncompraporc() As Nullable(Of Decimal)
        Get
            Return Me._decOpcioncompraporc
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decOpcioncompraporc = value
        End Set
    End Property

    <XmlElement("Comisionactivacionporc")> _
    Public Property Comisionactivacionporc() As Nullable(Of Decimal)
        Get
            Return Me._decComisionactivacionporc
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decComisionactivacionporc = value
        End Set
    End Property

    <XmlElement("Comisionestructuracionporc")> _
    Public Property Comisionestructuracionporc() As Nullable(Of Decimal)
        Get
            Return Me._decComisionestructuracionporc
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decComisionestructuracionporc = value
        End Set
    End Property

    <XmlElement("Mostrarteacartas")> _
    Public Property Mostrarteacartas() As Nullable(Of Integer)
        Get
            Return Me._intMostrarteacartas
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intMostrarteacartas = value
        End Set
    End Property

    <XmlElement("Mostrarmontocomision")> _
    Public Property Mostrarmontocomision() As Nullable(Of Integer)
        Get
            Return Me._intMostrarmontocomision
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intMostrarmontocomision = value
        End Set
    End Property

    <XmlElement("Importeopcioncompra")> _
    Public Property Importeopcioncompra() As Nullable(Of Decimal)
        Get
            Return Me._decImporteopcioncompra
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImporteopcioncompra = value
        End Set
    End Property

    <XmlElement("Importecomisionactivacion")> _
    Public Property Importecomisionactivacion() As Nullable(Of Decimal)
        Get
            Return Me._decImportecomisionactivacion
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImportecomisionactivacion = value
        End Set
    End Property

    <XmlElement("Importecomisionestructuracion")> _
    Public Property Importecomisionestructuracion() As Nullable(Of Decimal)
        Get
            Return Me._decImportecomisionestructuracion
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImportecomisionestructuracion = value
        End Set
    End Property

    <XmlElement("Periododisponible")> _
    Public Property Periododisponible() As Nullable(Of Integer)
        Get
            Return Me._intPeriododisponible
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intPeriododisponible = value
        End Set
    End Property

    <XmlElement("Otrascomisiones")> _
    Public Property Otrascomisiones() As String
        Get
            Return Me._strOtrascomisiones
        End Get
        Set(ByVal value As String)
            Me._strOtrascomisiones = value
        End Set
    End Property

    <XmlElement("Correoelectronico")> _
    Public Property Correoelectronico() As String
        Get
            Return Me._strCorreoelectronico
        End Get
        Set(ByVal value As String)
            Me._strCorreoelectronico = value
        End Set
    End Property

    <XmlElement("Fechaingreso")> _
    Public Property Fechaingreso() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaingreso
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaingreso = value
        End Set
    End Property

    <XmlElement("Versioncotizacion")> _
    Public Property Versioncotizacion() As Nullable(Of Integer)
        Get
            Return Me._intVersioncotizacion
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intVersioncotizacion = value
        End Set
    End Property

    <XmlElement("Versionfecha")> _
    Public Property Versionfecha() As Nullable(Of DateTime)
        Get
            Return Me._dtmVersionfecha
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmVersionfecha = value
        End Set
    End Property

    <XmlElement("Fechalimitevalidezcotizacion")> _
    Public Property Fechalimitevalidezcotizacion() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechalimitevalidezcotizacion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechalimitevalidezcotizacion = value
        End Set
    End Property

    <XmlElement("Codigosupervisoraprobo")> _
    Public Property Codigosupervisoraprobo() As String
        Get
            Return Me._strCodigosupervisoraprobo
        End Get
        Set(ByVal value As String)
            Me._strCodigosupervisoraprobo = value
        End Set
    End Property

    <XmlElement("Numerolinea")> _
    Public Property Numerolinea() As String
        Get
            Return Me._strNumerolinea
        End Get
        Set(ByVal value As String)
            Me._strNumerolinea = value
        End Set
    End Property

    <XmlElement("Tasalinea")> _
    Public Property Tasalinea() As Nullable(Of Decimal)
        Get
            Return Me._decTasalinea
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decTasalinea = value
        End Set
    End Property

    <XmlElement("Numerocronograma")> _
    Public Property Numerocronograma() As Nullable(Of Integer)
        Get
            Return Me._intNumerocronograma
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intNumerocronograma = value
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

    <XmlElement("Fechacarta")> _
    Public Property Fechacarta() As DateTime
        Get
            Return Me._dtmFechacarta
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechacarta = value
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

    <XmlElement("CodigoTipoBien")> _
    Public Property CodigoTipoBien() As String
        Get
            Return Me._strCodigoTipoBien
        End Get
        Set(ByVal value As String)
            Me._strCodigoTipoBien = value
        End Set
    End Property

    <XmlElement("DescripcionProveedor")> _
    Public Property DescripcionProveedor() As String
        Get
            Return Me._strDescripcionProveedor
        End Get
        Set(ByVal value As String)
            Me._strDescripcionProveedor = value
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

    <XmlElement("NombreCliente")> _
    Public Property NombreCliente() As String
        Get
            Return Me._strNombreCliente
        End Get
        Set(ByVal value As String)
            Me._strNombreCliente = value
        End Set
    End Property

    <XmlElement("CodigoTipoDocumento")> _
    Public Property CodigoTipoDocumento() As String
        Get
            Return Me._strCodigoTipoDocumento
        End Get
        Set(ByVal value As String)
            Me._strCodigoTipoDocumento = value
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

    <XmlElement("FechaOfertaValida")> _
    Public Property FechaOfertaValida() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaOfertaValida
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaOfertaValida = value
        End Set
    End Property

    <XmlElement("FlagCliente")> _
    Public Property FlagCliente() As Nullable(Of Integer)
        Get
            Return Me._intFlagCliente
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intFlagCliente = value
        End Set
    End Property

    <XmlElement("FlagLinea")> _
    Public Property FlagLinea() As Nullable(Of Integer)
        Get
            Return Me._intFlagLinea
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intFlagLinea = value
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

    <XmlElement("FechaInicio")> _
    Public Property FechaInicio() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaInicio
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaInicio = value
        End Set
    End Property

    <XmlElement("FechaFin")> _
    Public Property FechaFin() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaFin
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaFin = value
        End Set
    End Property

    <XmlElement("CodigoContrato")> _
    Public Property CodigoContrato() As String
        Get
            Return Me._strCodigoContrato
        End Get
        Set(ByVal value As String)
            Me._strCodigoContrato = value
        End Set
    End Property

    <XmlElement("CodigoZonal")> _
    Public Property CodigoZonal() As String
        Get
            Return Me._strCodigoZonal
        End Get
        Set(ByVal value As String)
            Me._strCodigoZonal = value
        End Set
    End Property

    <XmlElement("CodigoClasificacionContrato")> _
    Public Property CodigoClasificacionContrato() As String
        Get
            Return _strCodigoClasificacionContrato
        End Get
        Set(ByVal value As String)
            _strCodigoClasificacionContrato = value
        End Set
    End Property

    <XmlElement("CodigoContacto")> _
    Public Property CodigoContacto() As Nullable(Of Integer)
        Get
            Return Me._intCodigoContacto
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodigoContacto = value
        End Set
    End Property

    <XmlElement("NombreContacto")> _
    Public Property NombreContacto() As String
        Get
            Return Me._strNombreContacto
        End Get
        Set(ByVal value As String)
            Me._strNombreContacto = value
        End Set
    End Property

    <XmlElement("CorreoContacto")> _
    Public Property CorreoContacto() As String
        Get
            Return Me._strCorreoContacto
        End Get
        Set(ByVal value As String)
            Me._strCorreoContacto = value
        End Set
    End Property

    <XmlElement("AplicaVersionamiento")> _
    Public Property AplicaVersionamiento() As Nullable(Of Integer)
        Get
            Return Me._intAplicaVersionamiento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intAplicaVersionamiento = value
        End Set
    End Property

    <XmlElement("MotivoRechazo")> _
    Public Property MotivoRechazo() As String
        Get
            Return Me._strMotivoRechazo
        End Get
        Set(ByVal value As String)
            Me._strMotivoRechazo = value
        End Set
    End Property

    <XmlElement("CodigoEstadoContrato")> _
    Public Property CodigoEstadoContrato() As String
        Get
            Return Me._strCodigoEstadoContrato
        End Get
        Set(ByVal value As String)
            Me._strCodigoEstadoContrato = value
        End Set
    End Property


    <XmlElement("DesEjecutivoBanca")> _
    Public Property DesEjecutivoBanca() As String
        Get
            Return Me._strDesEjecutivoBanca
        End Get
        Set(ByVal value As String)
            Me._strDesEjecutivoBanca = value
        End Set
    End Property

    <XmlElement("DesZonal")> _
    Public Property DesZonal() As String
        Get
            Return Me._strDesZonal
        End Get
        Set(ByVal value As String)
            Me._strDesZonal = value
        End Set
    End Property

    <XmlElement("FlagCuotaInicial")> _
    Public Property FlagCuotaInicial() As String
        Get
            Return Me._strFlagCuotaInicial
        End Get
        Set(ByVal value As String)
            Me._strFlagCuotaInicial = value
        End Set
    End Property

    <XmlElement("MontoPorcIGV")> _
    Public Property MontoPorcIGV() As Nullable(Of Decimal)
        Get
            Return Me._decMontoPorcIGV
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoPorcIGV = value
        End Set
    End Property

    <XmlElement("DireccionCliente")> _
    Public Property DireccionCliente() As String
        Get
            Return Me._strDireccionCliente
        End Get
        Set(ByVal value As String)
            Me._strDireccionCliente = value
        End Set
    End Property


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
    'IBK - RPH
    <XmlElement("FlagOpcionCompras")> _
    Public Property FlagOpcionCompras() As String

        Get
            Return Me._strFlagOpcionCompras
        End Get
        Set(ByVal value As String)
            Me._strFlagOpcionCompras = value
        End Set
    End Property
    <XmlElement("FlagComisionActivacion")> _
    Public Property FlagComisionActivacion() As String

        Get
            Return Me._strFlagComisionAct
        End Get
        Set(ByVal value As String)
            Me._strFlagComisionAct = value
        End Set
    End Property
    <XmlElement("FlagComisionEstructuracion")> _
    Public Property FlagComisionEstructuracion() As String
        Get
            Return Me._strFlagComisionEst
        End Get
        Set(ByVal value As String)
            Me._strFlagComisionEst = value
        End Set
    End Property

    <XmlElement("ArchivoCronogramaAdjunto")> _
    Public Property ArchivoCronogramaAdjunto() As String
        Get
            Return Me._ArchivoCronograma
        End Get
        Set(ByVal value As String)
            Me._ArchivoCronograma = value
        End Set
    End Property
    'Fin
#End Region

End Class