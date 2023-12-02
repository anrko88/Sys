Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Productofinanciero
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EProductofinanciero")> _
Public Class EProductofinanciero

#Region " Atributos "

    Private _strCodproductofinanciero As String
    Private _strTipoproductoactivopasivo As String
    Private _strNombreproductofinanciero As String
    Private _strCodcategoriaalm As String
    Private _dtmFechacontratoproducto As Nullable(Of DateTime)
    Private _dtmFechafinalvigencia As Nullable(Of DateTime)
    Private _dtmFechaampliacion As Nullable(Of DateTime)
    Private _strTipoproductofinanciero As String
    Private _strTipomodalidadplazo As String
    Private _strTipocredito As String
    Private _strCodmoneda As String
    Private _decMontocontratado As Nullable(Of Decimal)
    Private _decMontodesembolsado As Nullable(Of Decimal)
    Private _decMontopordesembolsar As Nullable(Of Decimal)
    Private _decMontopagado As Nullable(Of Decimal)
    Private _decMontosaldo As Nullable(Of Decimal)
    Private _decMontovencido As Nullable(Of Decimal)
    Private _decMontoperdida As Nullable(Of Decimal)
    Private _decMontocobertura As Nullable(Of Decimal)
    Private _strTiporesponsabledeuda As String
    Private _strTipoparticipacion As String
    Private _strTipoamortizacion As String
    Private _strTipocofinanciamiento As String
    Private _strTipousocofinanciamiento As String
    Private _strDescripdispositivolegal As String
    Private _strDescripacuerdodirectorio As String
    Private _strTipocreditosindicado As String
    Private _strTipopagare As String
    Private _strTipoletras As String
    Private _strTipoprepago As String
    Private _decPorcentasamora As Nullable(Of Decimal)
    Private _strTipoaplicacionmora As String
    Private _intCantdiasgraciamora As Nullable(Of Integer)
    Private _strTipotasabasemorame As String
    Private _strTipotasabasemoramn As String
    Private _strEstadocambiocondicion As String
    Private _strTipocontabilidad As String
    Private _strCodempresa As String
    Private _strEstadoinfluyeflujocaja As String
    Private _strCodrubroflujoingreso As String
    Private _strCodsubrubroflujoingreso As String
    Private _strCodrubroflujoegreso As String
    Private _strCodsubrubroflujoegreso As String
    Private _strEstadoflar As String
    Private _strTipocondicionfinanciera As String
    Private _strEstadovigencia As String
    Private _strCodmodulooperacion As String
    Private _strTipooperacion As String
    Private _dtmFecharegistro As Nullable(Of DateTime)
    Private _strCodusuario As String
    Private _strTextoaudicreacion As String
    Private _strTextoaudimodi As String
    Private _strTipoclasificacion As String
    Private _strUsaigv As String
    Private _decPorcentasamoramn As Nullable(Of Decimal)
    Private _strInddesembolso As String
    Private _strIndrecompra As String
    Private _strIndseguro As String
    Private _strIndtransitorias As String
    Private _strIndcronograma As String
    Private _strIndavisos As String
    Private _strIndliquidacion As String
    Private _strIndbienes As String
    Private _strIndferiados As String
    Private _strIndcargocuenta As String
    Private _strIndrealizdesembolso As String
    Private _strCodproveedor As String

#End Region

#Region " Propiedades "

    <XmlElement("Codproductofinanciero")> _
    Public Property Codproductofinanciero() As String
        Get
            Return Me._strCodproductofinanciero
        End Get
        Set(ByVal value As String)
            Me._strCodproductofinanciero = value
        End Set
    End Property

    <XmlElement("Tipoproductoactivopasivo")> _
    Public Property Tipoproductoactivopasivo() As String
        Get
            Return Me._strTipoproductoactivopasivo
        End Get
        Set(ByVal value As String)
            Me._strTipoproductoactivopasivo = value
        End Set
    End Property

    <XmlElement("Nombreproductofinanciero")> _
    Public Property Nombreproductofinanciero() As String
        Get
            Return Me._strNombreproductofinanciero
        End Get
        Set(ByVal value As String)
            Me._strNombreproductofinanciero = value
        End Set
    End Property

    <XmlElement("Codcategoriaalm")> _
    Public Property Codcategoriaalm() As String
        Get
            Return Me._strCodcategoriaalm
        End Get
        Set(ByVal value As String)
            Me._strCodcategoriaalm = value
        End Set
    End Property

    <XmlElement("Fechacontratoproducto")> _
    Public Property Fechacontratoproducto() As DateTime
        Get
            Return Me._dtmFechacontratoproducto
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechacontratoproducto = value
        End Set
    End Property

    <XmlElement("Fechafinalvigencia")> _
    Public Property Fechafinalvigencia() As DateTime
        Get
            Return Me._dtmFechafinalvigencia
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechafinalvigencia = value
        End Set
    End Property

    <XmlElement("Fechaampliacion")> _
    Public Property Fechaampliacion() As DateTime
        Get
            Return Me._dtmFechaampliacion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechaampliacion = value
        End Set
    End Property

    <XmlElement("Tipoproductofinanciero")> _
    Public Property Tipoproductofinanciero() As String
        Get
            Return Me._strTipoproductofinanciero
        End Get
        Set(ByVal value As String)
            Me._strTipoproductofinanciero = value
        End Set
    End Property

    <XmlElement("Tipomodalidadplazo")> _
    Public Property Tipomodalidadplazo() As String
        Get
            Return Me._strTipomodalidadplazo
        End Get
        Set(ByVal value As String)
            Me._strTipomodalidadplazo = value
        End Set
    End Property

    <XmlElement("Tipocredito")> _
    Public Property Tipocredito() As String
        Get
            Return Me._strTipocredito
        End Get
        Set(ByVal value As String)
            Me._strTipocredito = value
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

    <XmlElement("Montocontratado")> _
    Public Property Montocontratado() As Decimal
        Get
            Return Me._decMontocontratado
        End Get
        Set(ByVal value As Decimal)
            Me._decMontocontratado = value
        End Set
    End Property

    <XmlElement("Montodesembolsado")> _
    Public Property Montodesembolsado() As Decimal
        Get
            Return Me._decMontodesembolsado
        End Get
        Set(ByVal value As Decimal)
            Me._decMontodesembolsado = value
        End Set
    End Property

    <XmlElement("Montopordesembolsar")> _
    Public Property Montopordesembolsar() As Decimal
        Get
            Return Me._decMontopordesembolsar
        End Get
        Set(ByVal value As Decimal)
            Me._decMontopordesembolsar = value
        End Set
    End Property

    <XmlElement("Montopagado")> _
    Public Property Montopagado() As Decimal
        Get
            Return Me._decMontopagado
        End Get
        Set(ByVal value As Decimal)
            Me._decMontopagado = value
        End Set
    End Property

    <XmlElement("Montosaldo")> _
    Public Property Montosaldo() As Decimal
        Get
            Return Me._decMontosaldo
        End Get
        Set(ByVal value As Decimal)
            Me._decMontosaldo = value
        End Set
    End Property

    <XmlElement("Montovencido")> _
    Public Property Montovencido() As Decimal
        Get
            Return Me._decMontovencido
        End Get
        Set(ByVal value As Decimal)
            Me._decMontovencido = value
        End Set
    End Property

    <XmlElement("Montoperdida")> _
    Public Property Montoperdida() As Decimal
        Get
            Return Me._decMontoperdida
        End Get
        Set(ByVal value As Decimal)
            Me._decMontoperdida = value
        End Set
    End Property

    <XmlElement("Montocobertura")> _
    Public Property Montocobertura() As Decimal
        Get
            Return Me._decMontocobertura
        End Get
        Set(ByVal value As Decimal)
            Me._decMontocobertura = value
        End Set
    End Property

    <XmlElement("Tiporesponsabledeuda")> _
    Public Property Tiporesponsabledeuda() As String
        Get
            Return Me._strTiporesponsabledeuda
        End Get
        Set(ByVal value As String)
            Me._strTiporesponsabledeuda = value
        End Set
    End Property

    <XmlElement("Tipoparticipacion")> _
    Public Property Tipoparticipacion() As String
        Get
            Return Me._strTipoparticipacion
        End Get
        Set(ByVal value As String)
            Me._strTipoparticipacion = value
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

    <XmlElement("Tipocofinanciamiento")> _
    Public Property Tipocofinanciamiento() As String
        Get
            Return Me._strTipocofinanciamiento
        End Get
        Set(ByVal value As String)
            Me._strTipocofinanciamiento = value
        End Set
    End Property

    <XmlElement("Tipousocofinanciamiento")> _
    Public Property Tipousocofinanciamiento() As String
        Get
            Return Me._strTipousocofinanciamiento
        End Get
        Set(ByVal value As String)
            Me._strTipousocofinanciamiento = value
        End Set
    End Property

    <XmlElement("Descripdispositivolegal")> _
    Public Property Descripdispositivolegal() As String
        Get
            Return Me._strDescripdispositivolegal
        End Get
        Set(ByVal value As String)
            Me._strDescripdispositivolegal = value
        End Set
    End Property

    <XmlElement("Descripacuerdodirectorio")> _
    Public Property Descripacuerdodirectorio() As String
        Get
            Return Me._strDescripacuerdodirectorio
        End Get
        Set(ByVal value As String)
            Me._strDescripacuerdodirectorio = value
        End Set
    End Property

    <XmlElement("Tipocreditosindicado")> _
    Public Property Tipocreditosindicado() As String
        Get
            Return Me._strTipocreditosindicado
        End Get
        Set(ByVal value As String)
            Me._strTipocreditosindicado = value
        End Set
    End Property

    <XmlElement("Tipopagare")> _
    Public Property Tipopagare() As String
        Get
            Return Me._strTipopagare
        End Get
        Set(ByVal value As String)
            Me._strTipopagare = value
        End Set
    End Property

    <XmlElement("Tipoletras")> _
    Public Property Tipoletras() As String
        Get
            Return Me._strTipoletras
        End Get
        Set(ByVal value As String)
            Me._strTipoletras = value
        End Set
    End Property

    <XmlElement("Tipoprepago")> _
    Public Property Tipoprepago() As String
        Get
            Return Me._strTipoprepago
        End Get
        Set(ByVal value As String)
            Me._strTipoprepago = value
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

    <XmlElement("Tipoaplicacionmora")> _
    Public Property Tipoaplicacionmora() As String
        Get
            Return Me._strTipoaplicacionmora
        End Get
        Set(ByVal value As String)
            Me._strTipoaplicacionmora = value
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

    <XmlElement("Tipotasabasemorame")> _
    Public Property Tipotasabasemorame() As String
        Get
            Return Me._strTipotasabasemorame
        End Get
        Set(ByVal value As String)
            Me._strTipotasabasemorame = value
        End Set
    End Property

    <XmlElement("Tipotasabasemoramn")> _
    Public Property Tipotasabasemoramn() As String
        Get
            Return Me._strTipotasabasemoramn
        End Get
        Set(ByVal value As String)
            Me._strTipotasabasemoramn = value
        End Set
    End Property

    <XmlElement("Estadocambiocondicion")> _
    Public Property Estadocambiocondicion() As String
        Get
            Return Me._strEstadocambiocondicion
        End Get
        Set(ByVal value As String)
            Me._strEstadocambiocondicion = value
        End Set
    End Property

    <XmlElement("Tipocontabilidad")> _
    Public Property Tipocontabilidad() As String
        Get
            Return Me._strTipocontabilidad
        End Get
        Set(ByVal value As String)
            Me._strTipocontabilidad = value
        End Set
    End Property

    <XmlElement("Codempresa")> _
    Public Property Codempresa() As String
        Get
            Return Me._strCodempresa
        End Get
        Set(ByVal value As String)
            Me._strCodempresa = value
        End Set
    End Property

    <XmlElement("Estadoinfluyeflujocaja")> _
    Public Property Estadoinfluyeflujocaja() As String
        Get
            Return Me._strEstadoinfluyeflujocaja
        End Get
        Set(ByVal value As String)
            Me._strEstadoinfluyeflujocaja = value
        End Set
    End Property

    <XmlElement("Codrubroflujoingreso")> _
    Public Property Codrubroflujoingreso() As String
        Get
            Return Me._strCodrubroflujoingreso
        End Get
        Set(ByVal value As String)
            Me._strCodrubroflujoingreso = value
        End Set
    End Property

    <XmlElement("Codsubrubroflujoingreso")> _
    Public Property Codsubrubroflujoingreso() As String
        Get
            Return Me._strCodsubrubroflujoingreso
        End Get
        Set(ByVal value As String)
            Me._strCodsubrubroflujoingreso = value
        End Set
    End Property

    <XmlElement("Codrubroflujoegreso")> _
    Public Property Codrubroflujoegreso() As String
        Get
            Return Me._strCodrubroflujoegreso
        End Get
        Set(ByVal value As String)
            Me._strCodrubroflujoegreso = value
        End Set
    End Property

    <XmlElement("Codsubrubroflujoegreso")> _
    Public Property Codsubrubroflujoegreso() As String
        Get
            Return Me._strCodsubrubroflujoegreso
        End Get
        Set(ByVal value As String)
            Me._strCodsubrubroflujoegreso = value
        End Set
    End Property

    <XmlElement("Estadoflar")> _
    Public Property Estadoflar() As String
        Get
            Return Me._strEstadoflar
        End Get
        Set(ByVal value As String)
            Me._strEstadoflar = value
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

    <XmlElement("Estadovigencia")> _
    Public Property Estadovigencia() As String
        Get
            Return Me._strEstadovigencia
        End Get
        Set(ByVal value As String)
            Me._strEstadovigencia = value
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

    <XmlElement("Tipoclasificacion")> _
    Public Property Tipoclasificacion() As String
        Get
            Return Me._strTipoclasificacion
        End Get
        Set(ByVal value As String)
            Me._strTipoclasificacion = value
        End Set
    End Property

    <XmlElement("Usaigv")> _
    Public Property Usaigv() As String
        Get
            Return Me._strUsaigv
        End Get
        Set(ByVal value As String)
            Me._strUsaigv = value
        End Set
    End Property

    <XmlElement("Porcentasamoramn")> _
    Public Property Porcentasamoramn() As Decimal
        Get
            Return Me._decPorcentasamoramn
        End Get
        Set(ByVal value As Decimal)
            Me._decPorcentasamoramn = value
        End Set
    End Property

    <XmlElement("Inddesembolso")> _
    Public Property Inddesembolso() As String
        Get
            Return Me._strInddesembolso
        End Get
        Set(ByVal value As String)
            Me._strInddesembolso = value
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

    <XmlElement("Indseguro")> _
    Public Property Indseguro() As String
        Get
            Return Me._strIndseguro
        End Get
        Set(ByVal value As String)
            Me._strIndseguro = value
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

    <XmlElement("Indbienes")> _
    Public Property Indbienes() As String
        Get
            Return Me._strIndbienes
        End Get
        Set(ByVal value As String)
            Me._strIndbienes = value
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

    <XmlElement("Indcargocuenta")> _
    Public Property Indcargocuenta() As String
        Get
            Return Me._strIndcargocuenta
        End Get
        Set(ByVal value As String)
            Me._strIndcargocuenta = value
        End Set
    End Property

    <XmlElement("Indrealizdesembolso")> _
    Public Property Indrealizdesembolso() As String
        Get
            Return Me._strIndrealizdesembolso
        End Get
        Set(ByVal value As String)
            Me._strIndrealizdesembolso = value
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


#End Region

End Class