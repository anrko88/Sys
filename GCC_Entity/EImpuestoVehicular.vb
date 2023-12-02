Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Impuestovehicular
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EImpuestovehicular")> _
Public Class EImpuestovehicular

#Region " Atributos "

    Private _strCodsolcredito As String = String.Empty
    Private _intSecfinanciamiento As Nullable(Of Integer) = 0
    Private _intSecimpuesto As Nullable(Of Integer) = 0
    Private _strCodunico As String = String.Empty
    Private _dtmFecpago As Nullable(Of DateTime) = "01/01/1999"
    Private _dtmFeccobro As Nullable(Of DateTime) = "01/01/1999"
    Private _decMonto As Nullable(Of Decimal)
    Private _decComision As Nullable(Of Decimal)
    Private _strObservaciones As String = String.Empty
    Private _dtmFecregistro As Nullable(Of DateTime) = "01/01/1999"
    Private _strUsuariomodificacion As String = String.Empty
    Private _dtmFecmodificacion As Nullable(Of DateTime) = "01/01/1999"
    Private _intImpuestovehicularanio As Nullable(Of Integer) = 0
    Private _dtmFechalimite As Nullable(Of DateTime) = "01/01/1999"
    Private _strCodigomoneda As String = String.Empty
    Private _decTipodecambio As Nullable(Of Decimal)
    Private _strNrocheque As String = String.Empty

    Private _strRazonSocialNombre As String = String.Empty
    Private _strCodigoTipoDocumento As String = String.Empty
    Private _strNumeroDocumento As String = String.Empty
    Private _strPlaca As String = String.Empty
    Private _strAnioFabricacion As String = String.Empty
    Private _strPeriodo As String = String.Empty
    Private _strCuota As String = String.Empty
    Private _strPagoCliente As String = String.Empty
    Private _dtmFechaInscripcionIni As Nullable(Of DateTime) = "01/01/1999"
    Private _dtmFechaInscripcionFin As Nullable(Of DateTime) = "01/01/1999"
    Private _strEstadoCobro As String = String.Empty
    Private _strEstado As String = String.Empty
    Private _strEstadoPago As String = String.Empty
    Private _dtmFechaDeclaracion As Nullable(Of DateTime) = "01/01/1999"
    Private _strCodigosImpuesto As String = String.Empty
    Private _strCodNroLote As String = String.Empty
    Private _strMunicipalidad As String = String.Empty
    Private _strRegeneraLote As String = String.Empty
    'Inicio IBK - AAE - 15/02/2013 - Agrego atributos
    Private _strCobroAdelantado As String = "N"
    Private _nbrCantDias As Nullable(Of Integer) = 0
    Private _strNoComision As String = "0"
    'Fin IBK
    'Inicio JJM IBK
    Private _strDescCodEstadoLote As String = String.Empty
    Private _strCodEstadoLote As String = String.Empty
    Private _decTotalLote As Nullable(Of Decimal)
    Private _strFechaPago As String = String.Empty
    Private _strDescConcepto As String = String.Empty
    Private _strCodigoConcepto As String = String.Empty

    Private _strMontoDevuelto As String = String.Empty
    Private _strMontoReembolso As String = String.Empty
    Private _strMontoCheque As String = String.Empty
    Private _strFechaCobro As String = String.Empty

    'Fin JJM IBK
#End Region

#Region " Propiedades "

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

    <XmlElement("Secimpuesto")> _
    Public Property Secimpuesto() As Integer
        Get
            Return Me._intSecimpuesto
        End Get
        Set(ByVal value As Integer)
            Me._intSecimpuesto = value
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

    <XmlElement("Comision")> _
    Public Property Comision() As Nullable(Of Decimal)
        Get
            Return Me._decComision
        End Get
        Set(ByVal value As Nullable(Of Decimal))
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
    Public Property Fecregistro() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecregistro
        End Get
        Set(ByVal value As Nullable(Of DateTime))
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
    Public Property Fecmodificacion() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecmodificacion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecmodificacion = value
        End Set
    End Property

    <XmlElement("Impuestovehicularanio")> _
    Public Property Impuestovehicularanio() As Nullable(Of Integer)
        Get
            Return Me._intImpuestovehicularanio
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intImpuestovehicularanio = value
        End Set
    End Property

    <XmlElement("Fechalimite")> _
    Public Property Fechalimite() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechalimite
        End Get
        Set(ByVal value As Nullable(Of DateTime))
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
    Public Property Tipodecambio() As Nullable(Of Decimal)
        Get
            Return Me._decTipodecambio
        End Get
        Set(ByVal value As Nullable(Of Decimal))
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

    <XmlElement("Municipalidad")> _
       Public Property Municipalidad() As String
        Get
            Return Me._strMunicipalidad
        End Get
        Set(ByVal value As String)
            Me._strMunicipalidad = value
        End Set
    End Property

    <XmlElement("RegeneraLote")> _
    Public Property RegeneraLote() As String
        Get
            Return Me._strRegeneraLote
        End Get
        Set(ByVal value As String)
            Me._strRegeneraLote = value
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
   Public Property CantDias() As Nullable(Of Integer)
        Get
            Return IIf(String.IsNullOrEmpty(_nbrCantDias), 0, Me._nbrCantDias)
        End Get
        Set(ByVal value As Nullable(Of Integer))
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
    'Fin IBK
    'Inicio JJM IBK
    <XmlElement("DescCodEstadoLote")> _
    Public Property DescCodEstadoLote() As String
        Get
            Return Me._strDescCodEstadoLote
        End Get
        Set(ByVal value As String)
            Me._strDescCodEstadoLote = value
        End Set
    End Property
    <XmlElement("CodEstadoLote")> _
    Public Property CodEstadoLote() As String
        Get
            Return Me._strCodEstadoLote
        End Get
        Set(ByVal value As String)
            Me._strCodEstadoLote = value
        End Set
    End Property

    <XmlElement("TotalLote")> _
    Public Property TotalLote() As Nullable(Of Decimal)
        Get
            Return Me._decTotalLote
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decTotalLote = value
        End Set
    End Property
    <XmlElement("FechaPagoLote")> _
    Public Property FechaPagoLote() As String
        Get
            Return Me._strFechaPago
        End Get
        Set(ByVal value As String)
            Me._strFechaPago = value
        End Set
    End Property
    <XmlElement("DescConcepto")> _
    Public Property DescConcepto() As String
        Get
            Return Me._strDescConcepto
        End Get
        Set(ByVal value As String)
            Me._strDescConcepto = value
        End Set
    End Property
    <XmlElement("CodigoConcepto")> _
    Public Property CodigoConcepto() As String
        Get
            Return Me._strCodigoConcepto
        End Get
        Set(ByVal value As String)
            Me._strCodigoConcepto = value
        End Set
    End Property

    <XmlElement("MontoDevuelto")> _
    Public Property MontoDevuelto() As String
        Get
            Return Me._strMontoDevuelto
        End Get
        Set(ByVal value As String)
            Me._strMontoDevuelto = value
        End Set
    End Property

    <XmlElement("MontoReembolso")> _
    Public Property MontoReembolso() As String
        Get
            Return Me._strMontoReembolso
        End Get
        Set(ByVal value As String)
            Me._strMontoReembolso = value
        End Set
    End Property

    <XmlElement("MontoCheque")> _
    Public Property MontoCheque() As String
        Get
            Return Me._strMontoCheque
        End Get
        Set(ByVal value As String)
            Me._strMontoCheque = value
        End Set
    End Property

    <XmlElement("FechaCobro")> _
    Public Property FechaCobro() As String
        Get
            Return Me._strFechaCobro
        End Get
        Set(ByVal value As String)
            Me._strFechaCobro = value
        End Set
    End Property
    'Fin JJM IBK
#End Region

End Class
