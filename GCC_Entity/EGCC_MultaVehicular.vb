Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Impuestovehicular
''' </summary>
''' <remarks>
''' Creado Por         : TSF - AEP
''' Fecha de Creación  : 23/11/2012
''' </remarks>
<Serializable(), XmlRoot("EGCC_MultaVehicular")> _
Public Class EGCC_MultaVehicular

#Region " Atributos "

    Private _strCodsolcredito As String
    Private _intSecfinanciamiento As Nullable(Of Integer)
    Private _intSecMulta As Nullable(Of Integer)
    Private _strCodunico As String
    Private _dtmFecpago As Nullable(Of DateTime)
    Private _dtmFeccobro As Nullable(Of DateTime)
    Private _decMonto As Nullable(Of Decimal)
    Private _decComision As Nullable(Of Decimal)
    Private _strObservaciones As String
    Private _dtmFecregistro As Nullable(Of DateTime)
    Private _strUsuariomodificacion As String
    Private _dtmFecmodificacion As Nullable(Of DateTime)
    Private _intImpuestovehicularanio As Nullable(Of Integer)
    Private _dtmFechalimite As Nullable(Of DateTime)
    Private _strCodigomoneda As String
    Private _decTipodecambio As Nullable(Of Decimal)
    Private _strNrocheque As String

    Private _strRazonSocialNombre As String
    Private _strCodigoTipoDocumento As String
    Private _strNumeroDocumento As String
    Private _strPlaca As String
    Private _strAnioFabricacion As String
    Private _strPeriodo As String
    Private _strCuota As String
    Private _strPagoCliente As String
    Private _dtmFechaInscripcionIni As Nullable(Of DateTime)
    Private _dtmFechaInscripcionFin As Nullable(Of DateTime)
    Private _strEstadoCobro As String
    Private _strEstado As String
    Private _strEstadoPago As String
    Private _dtmFechaDeclaracion As Nullable(Of DateTime)
    Private _strCodigosImpuesto As String
    Private _strCodNroLote As String
    Private _strCodConcepto As String
    Private _strCodInfraccion As String
    Private _strCodTipoBien As String
    Private _strCodMunicipalidad As String
    Private _strInfraccion As String
    Private _strNroInfraccion As String
    Private _dtmFecInfraccion As Nullable(Of DateTime)
    Private _dtmFecIngreso As Nullable(Of DateTime)
    Private _dtmFecRecepcionBanco As Nullable(Of DateTime)
    Private _decImporteDescuento As Nullable(Of Decimal)
    'Inicio IBK - AAE - 15/02/2013 - Agrego atributos
    Private _strCobroAdelantado As String = "N"
    Private _nbrCantDias As Nullable(Of Integer)
    Private _strNoComision As String = "0"
    Private _dtmFechaNotificacionLeasing As Nullable(Of DateTime)
    Private _dtmFechaVencimiento As Nullable(Of DateTime)
    'Fin IBK




#End Region

#Region " Propiedades "

    <XmlElement("FechaIngreso")> _
Public Property FechaIngreso() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecIngreso
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecIngreso = value
        End Set
    End Property
    <XmlElement("FechaRecepcionBanco")> _
Public Property FechaRecepcionBanco() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecRecepcionBanco
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecRecepcionBanco = value
        End Set
    End Property

    <XmlElement("Infraccion")> _
Public Property Infraccion() As String
        Get
            Return Me._strInfraccion
        End Get
        Set(ByVal value As String)
            Me._strInfraccion = value
        End Set
    End Property

    <XmlElement("CodInfraccion")> _
Public Property CodInfraccion() As String
        Get
            Return Me._strCodInfraccion
        End Get
        Set(ByVal value As String)
            Me._strCodInfraccion = value
        End Set
    End Property
    <XmlElement("CodTipoBien")> _
Public Property CodTipoBien() As String
        Get
            Return Me._strCodTipoBien
        End Get
        Set(ByVal value As String)
            Me._strCodTipoBien = value
        End Set
    End Property

    <XmlElement("CodMunicipalidad")> _
Public Property CodMunicipalidad() As String
        Get
            Return Me._strCodMunicipalidad
        End Get
        Set(ByVal value As String)
            Me._strCodMunicipalidad = value
        End Set
    End Property
    <XmlElement("CodConcepto")> _
Public Property CodConcepto() As String
        Get
            Return Me._strCodConcepto
        End Get
        Set(ByVal value As String)
            Me._strCodConcepto = value
        End Set
    End Property

    <XmlElement("FechaInfraccion")> _
Public Property FechaInfraccion() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecInfraccion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecInfraccion = value
        End Set
    End Property

    <XmlElement("NroInfraccion")> _
Public Property NroInfraccion() As String
        Get
            Return Me._strNroInfraccion
        End Get
        Set(ByVal value As String)
            Me._strNroInfraccion = value
        End Set
    End Property

    <XmlElement("CodNroLote")> _
Public Property CodNroLote() As String
        Get
            Return Me._strCodNroLote
        End Get
        Set(ByVal value As String)
            Me._strCodNroLote = value
        End Set
    End Property

    <XmlElement("CodigosImpuesto")> _
Public Property CodigosImpuesto() As String
        Get
            Return Me._strCodigosImpuesto
        End Get
        Set(ByVal value As String)
            Me._strCodigosImpuesto = value
        End Set
    End Property

    <XmlElement("Estado")> _
Public Property Estado() As String
        Get
            Return Me._strEstado
        End Get
        Set(ByVal value As String)
            Me._strEstado = value
        End Set
    End Property

    <XmlElement("PagoCliente")> _
Public Property PagoCliente() As String
        Get
            Return Me._strPagoCliente
        End Get
        Set(ByVal value As String)
            Me._strPagoCliente = value
        End Set
    End Property

    <XmlElement("Cuota")> _
Public Property Cuota() As String
        Get
            Return Me._strCuota
        End Get
        Set(ByVal value As String)
            Me._strCuota = value
        End Set
    End Property
    <XmlElement("EstadoCobro")> _
Public Property EstadoCobro() As String
        Get
            Return Me._strEstadoCobro
        End Get
        Set(ByVal value As String)
            Me._strEstadoCobro = value
        End Set
    End Property

    <XmlElement("EstadoPago")> _
Public Property EstadoPago() As String
        Get
            Return Me._strEstadoPago
        End Get
        Set(ByVal value As String)
            Me._strEstadoPago = value
        End Set
    End Property

    <XmlElement("FechaDeclaracion")> _
Public Property FechaDeclaracion() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaDeclaracion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaDeclaracion = value
        End Set
    End Property
    <XmlElement("FechaInscripcionFin")> _
Public Property FechaInscripcionFin() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaInscripcionFin
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaInscripcionFin = value
        End Set
    End Property


    <XmlElement("FechaInscripcionIni")> _
Public Property FechaInscripcionIni() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaInscripcionIni
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaInscripcionIni = value
        End Set
    End Property

    <XmlElement("Periodo")> _
Public Property Periodo() As String
        Get
            Return Me._strPeriodo
        End Get
        Set(ByVal value As String)
            Me._strPeriodo = value
        End Set
    End Property

    <XmlElement("AnioFabricacion")> _
 Public Property AnioFabricacion() As String
        Get
            Return Me._strAnioFabricacion
        End Get
        Set(ByVal value As String)
            Me._strAnioFabricacion = value
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

    <XmlElement("NumeroDocumento")> _
   Public Property NumeroDocumento() As String
        Get
            Return Me._strNumeroDocumento
        End Get
        Set(ByVal value As String)
            Me._strNumeroDocumento = value
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

    <XmlElement("RazonSocialNombre")> _
    Public Property RazonSocialNombre() As String
        Get
            Return Me._strRazonSocialNombre
        End Get
        Set(ByVal value As String)
            Me._strRazonSocialNombre = value
        End Set
    End Property


    <XmlElement("Codsolcredito")> _
    Public Property Codsolcredito() As String
        Get
            Return Me._strCodsolcredito
        End Get
        Set(ByVal value As String)
            Me._strCodsolcredito = value
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

    <XmlElement("SecMulta")> _
    Public Property Secimpuesto() As Integer
        Get
            Return Me._intSecMulta
        End Get
        Set(ByVal value As Integer)
            Me._intSecMulta = value
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

    <XmlElement("Fecpago")> _
    Public Property Fecpago() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecpago
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecpago = value
        End Set
    End Property

    <XmlElement("Feccobro")> _
    Public Property Feccobro() As Nullable(Of DateTime)
        Get
            Return Me._dtmFeccobro
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFeccobro = value
        End Set
    End Property

    <XmlElement("Monto")> _
    Public Property Monto() As Nullable(Of Decimal)
        Get
            Return Me._decMonto
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMonto = value
        End Set
    End Property
    <XmlElement("ImporteDescuento")> _
 Public Property ImporteDescuento() As Nullable(Of Decimal)
        Get
            Return Me._decImporteDescuento
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImporteDescuento = value
        End Set
    End Property

    <XmlElement("Comision")> _
    Public Property Comision() As Decimal
        Get
            Return Me._decComision
        End Get
        Set(ByVal value As Decimal)
            Me._decComision = value
        End Set
    End Property

    <XmlElement("Observaciones")> _
    Public Property Observaciones() As String
        Get
            Return Me._strObservaciones
        End Get
        Set(ByVal value As String)
            Me._strObservaciones = value
        End Set
    End Property

    <XmlElement("Fecregistro")> _
    Public Property Fecregistro() As DateTime
        Get
            Return Me._dtmFecregistro
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFecregistro = value
        End Set
    End Property

    <XmlElement("Usuariomodificacion")> _
    Public Property Usuariomodificacion() As String
        Get
            Return Me._strUsuariomodificacion
        End Get
        Set(ByVal value As String)
            Me._strUsuariomodificacion = value
        End Set
    End Property

    <XmlElement("Fecmodificacion")> _
    Public Property Fecmodificacion() As DateTime
        Get
            Return Me._dtmFecmodificacion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFecmodificacion = value
        End Set
    End Property

    <XmlElement("Impuestovehicularanio")> _
    Public Property Impuestovehicularanio() As Integer
        Get
            Return Me._intImpuestovehicularanio
        End Get
        Set(ByVal value As Integer)
            Me._intImpuestovehicularanio = value
        End Set
    End Property

    <XmlElement("Fechalimite")> _
    Public Property Fechalimite() As DateTime
        Get
            Return Me._dtmFechalimite
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechalimite = value
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

    <XmlElement("Tipodecambio")> _
    Public Property Tipodecambio() As Decimal
        Get
            Return Me._decTipodecambio
        End Get
        Set(ByVal value As Decimal)
            Me._decTipodecambio = value
        End Set
    End Property

    <XmlElement("Nrocheque")> _
    Public Property Nrocheque() As String
        Get
            Return Me._strNrocheque
        End Get
        Set(ByVal value As String)
            Me._strNrocheque = value
        End Set
    End Property
    'Inicio IBK - AAE - 15/02/2013 
    <XmlElement("CobroAdelantado")> _
    Public Property CobroAdelantado() As String
        Get
            Return Me._strCobroAdelantado
        End Get
        Set(ByVal value As String)
            Me._strCobroAdelantado = value
        End Set
    End Property
    <XmlElement("CantDias")> _
    Public Property CantDias() As Integer
        Get
            Return Me._nbrCantDias
        End Get
        Set(ByVal value As Integer)
            Me._nbrCantDias = value
        End Set
    End Property
    <XmlElement("NoComision")> _
    Public Property NoComision() As String
        Get
            Return Me._strNoComision
        End Get
        Set(ByVal value As String)
            Me._strNoComision = value
        End Set
    End Property
    <XmlElement("FechaNotificacionLeasing")> _
    Public Property FechaNotificacionLeasing() As DateTime
        Get
            Return Me._dtmFechaNotificacionLeasing
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechaNotificacionLeasing = value
        End Set
    End Property
    <XmlElement("FechaVencimiento")> _
    Public Property FechaVencimiento() As DateTime
        Get
            Return Me._dtmFechaVencimiento
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechaVencimiento = value
        End Set
    End Property
    'Fin IBK

#End Region

End Class
