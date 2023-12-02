Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Impuestomunicipal
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EImpuestomunicipal")> _
Public Class EImpuestomunicipal

#Region " Atributos "

    Private _strCodsolcredito As String = String.Empty
    Private _intSecfinanciamiento As Nullable(Of Integer)
    Private _intSecimpuesto As Nullable(Of Integer)
    Private _strCodunico As String = String.Empty
    Private _dtmFecpago As Nullable(Of DateTime)
    Private _dtmFeclimite As Nullable(Of DateTime)
    Private _strMunicipalidad As String = String.Empty
    Private _decMonto As Nullable(Of Decimal)
    Private _strConcepto As String = String.Empty
    Private _strObservaciones As String = String.Empty
    Private _dtmFecregistro As Nullable(Of DateTime)
    Private _strUsuariomodificacion As String = String.Empty
    Private _dtmFecmodificacion As Nullable(Of DateTime)
    Private _decComision As Nullable(Of Decimal)
    Private _dtmFechacobro As Nullable(Of DateTime)
    Private _decTipodecambio As Nullable(Of Decimal)
    Private _strNrocheque As String = String.Empty
    Private _strNropartidaregistral As String = String.Empty
    Private _intAño As Nullable(Of Integer)
    Private _strCodigomoneda As String = String.Empty
    Private _strLote As String = String.Empty

    Private _decTotalAutovaluo As Nullable(Of Decimal)
    Private _decTotalPredial As Nullable(Of Decimal)
    Private _intPeriodo As Nullable(Of Integer)
    Private _decCodPredio As String = String.Empty
    Private _decAutovaluo As Nullable(Of Decimal)
    Private _decImpuestoPredial As Nullable(Of Decimal)
    Private _decArbitrio As Nullable(Of Decimal)
    Private _decMulta As Nullable(Of Decimal)
    Private _decFiscalizacion As Nullable(Of Decimal)
    Private _decTotal As Nullable(Of Decimal)
    Private _strPagoCliente As String = String.Empty
    Private _strEstadoPago As String = String.Empty
    Private _strEstadoCobro As String = String.Empty
    Private _decIGV As Nullable(Of Decimal)
    Private _decTotal2 As Nullable(Of Decimal)

    Private _strDepartamento As String = String.Empty
    Private _strProvincia As String = String.Empty
    Private _strDistrito As String = String.Empty
    Private _strNroContrato As String = String.Empty
    Private _strRazonSocial As String = String.Empty
    Private _strTipoDocumento As String = String.Empty
    Private _strNroDocumento As String = String.Empty
    Private _strPeriodo As String = String.Empty

    Private _strUbicacion As String = String.Empty
    Private _strCodigosImpuesto As String = String.Empty
    Private _strEstadologico As Integer = 0
    Private _strTieneLote As String = String.Empty

    Private _strFecpagoStr As String = String.Empty
    Private _strFeclimiteStr As String = String.Empty
    Private _strFechacobroStr As String = String.Empty

    Private _strCodigosImpuestos As String = String.Empty
    Private _strFiscalizacionChk As String = String.Empty
    'Inicio IBK - AAE - 15/02/2013 - Agrego atributos
    Private _strCobroAdelantado As String = "N"
    Private _nbrCantDias As Nullable(Of Integer) = 0
    Private _strNoComision As String = "0"
    'Fin IBK
#End Region

#Region " Propiedades "

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
    Public Property Secfinanciamiento() As Nullable(Of Integer)
        Get
            Return Me._intSecfinanciamiento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intSecfinanciamiento = value
        End Set
    End Property

    <XmlElement("Secimpuesto")> _
    Public Property Secimpuesto() As Nullable(Of Integer)
        Get
            Return Me._intSecimpuesto
        End Get
        Set(ByVal value As Nullable(Of Integer))
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

    <XmlElement("Feclimite")> _
    Public Property Feclimite() As Nullable(Of DateTime)
        Get
            Return Me._dtmFeclimite
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFeclimite = value
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

    <XmlElement("Monto")> _
    Public Property Monto() As Nullable(Of Decimal)
        Get
            Return Me._decMonto
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMonto = value
        End Set
    End Property

    <XmlElement("Concepto")> _
    Public Property Concepto() As String
        Get
            Return Me._strConcepto
        End Get
        Set(ByVal value As String)
            Me._strConcepto = value
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
    <XmlElement("Comision")> _
    Public Property Comision() As Nullable(Of Decimal)
        Get
            Return Me._decComision
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decComision = value
        End Set
    End Property

    <XmlElement("Fechacobro")> _
    Public Property Fechacobro() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechacobro
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechacobro = value
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

    <XmlElement("Nropartidaregistral")> _
    Public Property Nropartidaregistral() As String
        Get
            Return Me._strNropartidaregistral
        End Get
        Set(ByVal value As String)
            Me._strNropartidaregistral = value
        End Set
    End Property

    <XmlElement("Año")> _
    Public Property Año() As Nullable(Of Integer)
        Get
            Return Me._intAño
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intAño = value
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




    <XmlElement("Periodo")> _
    Public Property Periodo() As Nullable(Of Integer)
        Get
            Return Me._intPeriodo
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intPeriodo = value
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

    <XmlElement("EstadoCobro")> _
    Public Property EstadoCobro() As String
        Get
            Return Me._strEstadoCobro
        End Get
        Set(ByVal value As String)
            Me._strEstadoCobro = value
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

    <XmlElement("TotalAutovaluo")> _
    Public Property TotalAutovaluo() As Nullable(Of Decimal)
        Get
            Return Me._decTotalAutovaluo
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decTotalAutovaluo = value
        End Set
    End Property

    <XmlElement("TotalPredial")> _
    Public Property TotalPredial() As Nullable(Of Decimal)
        Get
            Return Me._decTotalPredial
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decTotalPredial = value
        End Set
    End Property

    <XmlElement("CodPredio")> _
    Public Property CodPredio() As String
        Get
            Return Me._decCodPredio
        End Get
        Set(ByVal value As String)
            Me._decCodPredio = value
        End Set
    End Property

    <XmlElement("Autovaluo")> _
    Public Property Autovaluo() As Nullable(Of Decimal)
        Get
            Return Me._decAutovaluo
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decAutovaluo = value
        End Set
    End Property

    <XmlElement("ImpuestoPredial")> _
    Public Property ImpuestoPredial() As Nullable(Of Decimal)
        Get
            Return Me._decImpuestoPredial
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImpuestoPredial = value
        End Set
    End Property

    <XmlElement("Arbitrio")> _
    Public Property Arbitrio() As Nullable(Of Decimal)
        Get
            Return Me._decArbitrio
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decArbitrio = value
        End Set
    End Property

    <XmlElement("Multa")> _
    Public Property Multa() As Nullable(Of Decimal)
        Get
            Return Me._decMulta
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMulta = value
        End Set
    End Property

    <XmlElement("Fiscalizacion")> _
    Public Property Fiscalizacion() As Nullable(Of Decimal)
        Get
            Return Me._decFiscalizacion
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decFiscalizacion = value
        End Set
    End Property

    <XmlElement("Total")> _
    Public Property Total() As Nullable(Of Decimal)
        Get
            Return Me._decTotal
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decTotal = value
        End Set
    End Property

    <XmlElement("IGV")> _
    Public Property IGV() As Nullable(Of Decimal)
        Get
            Return Me._decIGV
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decIGV = value
        End Set
    End Property


    <XmlElement("Total2")> _
    Public Property Total2() As Nullable(Of Decimal)
        Get
            Return Me._decTotal2
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decTotal2 = value
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

    <XmlElement("NroContrato")> _
    Public Property NroContrato() As String
        Get
            Return Me._strNroContrato
        End Get
        Set(ByVal value As String)
            Me._strNroContrato = value
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

    <XmlElement("TipoDocumento")> _
    Public Property TipoDocumento() As String
        Get
            Return Me._strTipoDocumento
        End Get
        Set(ByVal value As String)
            Me._strTipoDocumento = value
        End Set
    End Property

    <XmlElement("NroDocumento")> _
    Public Property NroDocumento() As String
        Get
            Return Me._strNroDocumento
        End Get
        Set(ByVal value As String)
            Me._strNroDocumento = value
        End Set
    End Property

    <XmlElement("PeriodoBsq")> _
    Public Property PeriodoBsq() As String
        Get
            Return Me._strPeriodo
        End Get
        Set(ByVal value As String)
            Me._strPeriodo = value
        End Set
    End Property

    <XmlElement("Lote")> _
    Public Property Lote() As String
        Get
            Return Me._strLote
        End Get
        Set(ByVal value As String)
            Me._strLote = value
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

    <XmlElement("CodigosImpuesto")> _
    Public Property CodigosImpuesto() As String
        Get
            Return Me._strCodigosImpuesto
        End Get
        Set(ByVal value As String)
            Me._strCodigosImpuesto = value
        End Set
    End Property

    <XmlElement("Estadologico")> _
    Public Property Estadologico() As String
        Get
            Return Me._strEstadologico
        End Get
        Set(ByVal value As String)
            Me._strEstadologico = value
        End Set
    End Property


    <XmlElement("TieneLote")> _
    Public Property TieneLote() As String
        Get
            Return Me._strTieneLote
        End Get
        Set(ByVal value As String)
            Me._strTieneLote = value
        End Set
    End Property



    <XmlElement("FecpagoStr")> _
    Public Property FecpagoStr() As String
        Get
            Return Me._strFecpagoStr
        End Get
        Set(ByVal value As String)
            Me._strFecpagoStr = value
        End Set
    End Property

    <XmlElement("FeclimiteStr")> _
    Public Property FeclimiteStr() As String
        Get
            Return Me._strFeclimiteStr
        End Get
        Set(ByVal value As String)
            Me._strFeclimiteStr = value
        End Set
    End Property

    <XmlElement("FechacobroStr")> _
    Public Property FechacobroStr() As String
        Get
            Return Me._strFechacobroStr
        End Get
        Set(ByVal value As String)
            Me._strFechacobroStr = value
        End Set
    End Property

    <XmlElement("CodigosImpuestos")> _
    Public Property CodigosImpuestos() As String
        Get
            Return Me._strCodigosImpuestos
        End Get
        Set(ByVal value As String)
            Me._strCodigosImpuestos = value
        End Set
    End Property
    <XmlElement("FiscalizacionChk")> _
        Public Property FiscalizacionChk() As String
        Get
            Return Me._strFiscalizacionChk
        End Get
        Set(ByVal value As String)
            Me._strFiscalizacionChk = value
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
            Return IIf(String.IsNullOrEmpty(Me._nbrCantDias), 0, Me._nbrCantDias)
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
#End Region

End Class
