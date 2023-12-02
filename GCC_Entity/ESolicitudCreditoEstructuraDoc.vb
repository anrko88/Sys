Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Solicitudcreditoestructuradoc
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("ESolicitudcreditoestructuradoc")> _
Public Class ESolicitudcreditoestructuradoc

#Region " Atributos "

    Private _strCodsolicitudcredito As String
    Private _intCodigodocumento As Nullable(Of Integer)
    Private _dtmFechaemision As Nullable(Of DateTime)
    Private _strTipodocumento As String
    Private _strNrodocumento As String
    Private _strDescripcion As String
    Private _decMontodocumento As Nullable(Of Decimal)
    Private _decMontoigv As Nullable(Of Decimal)
    Private _intNrodesembolso As Nullable(Of Integer)
    Private _strMonedaoriginal As String
    Private _decImporteoriginal As Nullable(Of Decimal)
    Private _decIgvoriginal As Nullable(Of Decimal)
    Private _strFacturarelnotacredito As String
    Private _decTipocambionotacredito As Nullable(Of Decimal)
    Private _intSecfinanciamiento As Nullable(Of Integer)
    Private _strConceptodetraccion As String
    Private _decTcsbs As Nullable(Of Decimal)
    Private _decTcutilizado As Nullable(Of Decimal)
    Private _decDiferencialxtipodecambio As Nullable(Of Decimal)
    Private _decMontodetraccion As Nullable(Of Decimal)
    Private _strIndiceretencion As String
    Private _strIndicedetraccion As String
    Private _decPorcentajedetraccionaplicada As Nullable(Of Decimal)
    Private _dtmFecharegistro As Nullable(Of DateTime)
    Private _strCodigoprocedencia As String
    Private _strNumeroseriedocumento As String
    Private _decTotal As Nullable(Of Decimal)
    Private _decTipocambioespecial As Nullable(Of Decimal)
    Private _decRetencionimporte As Nullable(Of Decimal)
    Private _intDesembolso As Nullable(Of Integer)
    Private _intEstadodocumento As Nullable(Of Integer)


    Private _strCodProveedor As String
    Private _dtmFechaVencimiento As Nullable(Of DateTime)
    Private _strCodigoTipoAduana As String
    Private _intAnioDUA As Nullable(Of Integer)
    Private _strNroComprobanteDUA As String
    Private _intFlagAdelantoProveedor As Nullable(Of Integer)
    Private _decMontoAdelantoProveedor As Nullable(Of Decimal)
    Private _decMontoPendienteProveedor As Nullable(Of Decimal)
    Private _intFlagTipoCambioEspecial As Nullable(Of Integer)
    Private _intFlagTipoCambioSunat As Nullable(Of Integer)
    Private _strCodigoTipoComprobante As String
    Private _strNumeroSerieDocumentoAdd As String
    Private _strNroDocumentoAdd As String
    Private _dtmFechaEmisionAdd As Nullable(Of DateTime)
    Private _decMontoGravado As Nullable(Of Decimal)
    Private _decMontoNoGravado As Nullable(Of Decimal)
    Private _strCodigoTipoServicio As String
    Private _decServicioPorc As Nullable(Of Decimal)
    Private _decMontoServicioSoles As Nullable(Of Decimal)
    Private _decMontoServicioDolar As Nullable(Of Decimal)
    Private _strNroConstancia As String
    Private _dtmFechaEmisionServicio As Nullable(Of DateTime)

    Private _intCodigoTipoDocumentoProveedor As Nullable(Of Integer)
    Private _strNumeroDocumentoProveedor As String
    Private _strNombreProveedor As String
    Private _strNombreTipoComprobDoc1 As String
    Private _strNombreTipoAduana As String
    Private _strNombreTipoServicio As String
    Private _strNombreTipoComprobDoc2 As String

    Private _strFechaEmision As String
    Private _strFechaVencimiento As String
    Private _strFechaEmisionServicio As String
    Private _strFechaEmisionAdd As String

    Private _strCodigoTipoBien As String = String.Empty

    Private _strKeyFechaEmision As String = String.Empty
    Private _strKeyTipoComprobante As String = String.Empty
    Private _strKeyNumeroComprobante As String = String.Empty
    Private _strKeyCodProveedor As String = String.Empty

    Private _strNroInstruccionWIO As String
    Private _strNroSecuenciaWIO As String

    Private _strCodUnico As String
    Private _strCodSolicitudcreditoAdd As String

    Private _decPorc4ta As Nullable(Of Decimal)
    Private _decMonto4taSoles As Nullable(Of Decimal)
    Private _decMonto4taDolares As Nullable(Of Decimal)

#End Region

#Region " Propiedades "

    <XmlElement("Codsolicitudcredito")> _
    Public Property Codsolicitudcredito() As String
        Get
            Return Me._strCodsolicitudcredito
        End Get
        Set(ByVal value As String)
            Me._strCodsolicitudcredito = value
        End Set
    End Property

    <XmlElement("Codigodocumento")> _
    Public Property Codigodocumento() As Nullable(Of Integer)
        Get
            Return Me._intCodigodocumento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodigodocumento = value
        End Set
    End Property

    <XmlElement("Fechaemision")> _
    Public Property Fechaemision() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaemision
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaemision = value
        End Set
    End Property

    <XmlElement("Tipodocumento")> _
    Public Property Tipodocumento() As String
        Get
            Return Me._strTipodocumento
        End Get
        Set(ByVal value As String)
            Me._strTipodocumento = value
        End Set
    End Property

    <XmlElement("Nrodocumento")> _
    Public Property Nrodocumento() As String
        Get
            Return Me._strNrodocumento
        End Get
        Set(ByVal value As String)
            Me._strNrodocumento = value
        End Set
    End Property

    <XmlElement("Descripcion")> _
    Public Property Descripcion() As String
        Get
            Return Me._strDescripcion
        End Get
        Set(ByVal value As String)
            Me._strDescripcion = value
        End Set
    End Property

    <XmlElement("Montodocumento")> _
    Public Property Montodocumento() As Nullable(Of Decimal)
        Get
            Return Me._decMontodocumento
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontodocumento = value
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

    <XmlElement("Nrodesembolso")> _
    Public Property Nrodesembolso() As Nullable(Of Integer)
        Get
            Return Me._intNrodesembolso
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intNrodesembolso = value
        End Set
    End Property

    <XmlElement("Monedaoriginal")> _
    Public Property Monedaoriginal() As String
        Get
            Return Me._strMonedaoriginal
        End Get
        Set(ByVal value As String)
            Me._strMonedaoriginal = value
        End Set
    End Property

    <XmlElement("Importeoriginal")> _
    Public Property Importeoriginal() As Nullable(Of Decimal)
        Get
            Return Me._decImporteoriginal
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImporteoriginal = value
        End Set
    End Property

    <XmlElement("Igvoriginal")> _
    Public Property Igvoriginal() As Nullable(Of Decimal)
        Get
            Return Me._decIgvoriginal
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decIgvoriginal = value
        End Set
    End Property

    <XmlElement("Facturarelnotacredito")> _
    Public Property Facturarelnotacredito() As String
        Get
            Return Me._strFacturarelnotacredito
        End Get
        Set(ByVal value As String)
            Me._strFacturarelnotacredito = value
        End Set
    End Property

    <XmlElement("Tipocambionotacredito")> _
    Public Property Tipocambionotacredito() As Nullable(Of Decimal)
        Get
            Return Me._decTipocambionotacredito
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decTipocambionotacredito = value
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

    <XmlElement("Conceptodetraccion")> _
    Public Property Conceptodetraccion() As String
        Get
            Return Me._strConceptodetraccion
        End Get
        Set(ByVal value As String)
            Me._strConceptodetraccion = value
        End Set
    End Property

    <XmlElement("Tcsbs")> _
    Public Property Tcsbs() As Nullable(Of Decimal)
        Get
            Return Me._decTcsbs
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decTcsbs = value
        End Set
    End Property

    <XmlElement("Tcutilizado")> _
    Public Property Tcutilizado() As Nullable(Of Decimal)
        Get
            Return Me._decTcutilizado
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decTcutilizado = value
        End Set
    End Property

    <XmlElement("Diferencialxtipodecambio")> _
    Public Property Diferencialxtipodecambio() As Nullable(Of Decimal)
        Get
            Return Me._decDiferencialxtipodecambio
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decDiferencialxtipodecambio = value
        End Set
    End Property

    <XmlElement("Montodetraccion")> _
    Public Property Montodetraccion() As Nullable(Of Decimal)
        Get
            Return Me._decMontodetraccion
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontodetraccion = value
        End Set
    End Property

    <XmlElement("Indiceretencion")> _
    Public Property Indiceretencion() As String
        Get
            Return Me._strIndiceretencion
        End Get
        Set(ByVal value As String)
            Me._strIndiceretencion = value
        End Set
    End Property

    <XmlElement("Indicedetraccion")> _
    Public Property Indicedetraccion() As String
        Get
            Return Me._strIndicedetraccion
        End Get
        Set(ByVal value As String)
            Me._strIndicedetraccion = value
        End Set
    End Property

    <XmlElement("Porcentajedetraccionaplicada")> _
    Public Property Porcentajedetraccionaplicada() As Nullable(Of Decimal)
        Get
            Return Me._decPorcentajedetraccionaplicada
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPorcentajedetraccionaplicada = value
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

    <XmlElement("Codigoprocedencia")> _
    Public Property Codigoprocedencia() As String
        Get
            Return Me._strCodigoprocedencia
        End Get
        Set(ByVal value As String)
            Me._strCodigoprocedencia = value
        End Set
    End Property

    <XmlElement("Numeroseriedocumento")> _
    Public Property Numeroseriedocumento() As String
        Get
            Return Me._strNumeroseriedocumento
        End Get
        Set(ByVal value As String)
            Me._strNumeroseriedocumento = value
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

    <XmlElement("Tipocambioespecial")> _
    Public Property Tipocambioespecial() As Nullable(Of Decimal)
        Get
            Return Me._decTipocambioespecial
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decTipocambioespecial = value
        End Set
    End Property

    <XmlElement("Retencionimporte")> _
    Public Property Retencionimporte() As Nullable(Of Decimal)
        Get
            Return Me._decRetencionimporte
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decRetencionimporte = value
        End Set
    End Property

    <XmlElement("Desembolso")> _
    Public Property Desembolso() As Nullable(Of Integer)
        Get
            Return Me._intDesembolso
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intDesembolso = value
        End Set
    End Property

    <XmlElement("Estadodocumento")> _
    Public Property Estadodocumento() As Nullable(Of Integer)
        Get
            Return Me._intEstadodocumento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intEstadodocumento = value
        End Set
    End Property







    <XmlElement("CodProveedor")> _
    Public Property CodProveedor() As String
        Get
            Return Me._strCodProveedor
        End Get
        Set(ByVal value As String)
            Me._strCodProveedor = value
        End Set
    End Property

    <XmlElement("FechaVencimiento")> _
    Public Property FechaVencimiento() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaVencimiento
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaVencimiento = value
        End Set
    End Property

    <XmlElement("CodigoTipoAduana")> _
    Public Property CodigoTipoAduana() As String
        Get
            Return Me._strCodigoTipoAduana
        End Get
        Set(ByVal value As String)
            Me._strCodigoTipoAduana = value
        End Set
    End Property

    <XmlElement("AnioDUA")> _
    Public Property AnioDUA() As Nullable(Of Integer)
        Get
            Return Me._intAnioDUA
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intAnioDUA = value
        End Set
    End Property

    <XmlElement("NroComprobanteDUA")> _
    Public Property NroComprobanteDUA() As String
        Get
            Return Me._strNroComprobanteDUA
        End Get
        Set(ByVal value As String)
            Me._strNroComprobanteDUA = value
        End Set
    End Property

    <XmlElement("FlagAdelantoProveedor")> _
    Public Property FlagAdelantoProveedor() As Nullable(Of Integer)
        Get
            Return Me._intFlagAdelantoProveedor
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intFlagAdelantoProveedor = value
        End Set
    End Property

    <XmlElement("MontoAdelantoProveedor")> _
    Public Property MontoAdelantoProveedor() As Nullable(Of Decimal)
        Get
            Return Me._decMontoAdelantoProveedor
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoAdelantoProveedor = value
        End Set
    End Property

    <XmlElement("MontoPendienteProveedor")> _
    Public Property MontoPendienteProveedor() As Nullable(Of Decimal)
        Get
            Return Me._decMontoPendienteProveedor
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoPendienteProveedor = value
        End Set
    End Property

    <XmlElement("FlagTipoCambioEspecial")> _
    Public Property FlagTipoCambioEspecial() As Nullable(Of Integer)
        Get
            Return Me._intFlagTipoCambioEspecial
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intFlagTipoCambioEspecial = value
        End Set
    End Property

    <XmlElement("FlagTipoCambioSunat")> _
    Public Property FlagTipoCambioSunat() As Nullable(Of Integer)
        Get
            Return Me._intFlagTipoCambioSunat
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intFlagTipoCambioSunat = value
        End Set
    End Property

    <XmlElement("CodigoTipoComprobante")> _
    Public Property CodigoTipoComprobante() As String
        Get
            Return Me._strCodigoTipoComprobante
        End Get
        Set(ByVal value As String)
            Me._strCodigoTipoComprobante = value
        End Set
    End Property

    <XmlElement("NumeroSerieDocumentoAdd")> _
    Public Property NumeroSerieDocumentoAdd() As String
        Get
            Return Me._strNumeroSerieDocumentoAdd
        End Get
        Set(ByVal value As String)
            Me._strNumeroSerieDocumentoAdd = value
        End Set
    End Property

    <XmlElement("NroDocumentoAdd")> _
        Public Property NroDocumentoAdd() As String
        Get
            Return Me._strNroDocumentoAdd
        End Get
        Set(ByVal value As String)
            Me._strNroDocumentoAdd = value
        End Set
    End Property

    <XmlElement("FechaEmisionAdd")> _
    Public Property FechaEmisionAdd() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaEmisionAdd
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaEmisionAdd = value
        End Set
    End Property

    <XmlElement("MontoGravado")> _
    Public Property MontoGravado() As Nullable(Of Decimal)
        Get
            Return Me._decMontoGravado
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoGravado = value
        End Set
    End Property

    <XmlElement("MontoNoGravado")> _
    Public Property MontoNoGravado() As Nullable(Of Decimal)
        Get
            Return Me._decMontoNoGravado
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoNoGravado = value
        End Set
    End Property

    <XmlElement("CodigoTipoServicio")> _
    Public Property CodigoTipoServicio() As String
        Get
            Return Me._strCodigoTipoServicio
        End Get
        Set(ByVal value As String)
            Me._strCodigoTipoServicio = value
        End Set
    End Property

    <XmlElement("ServicioPorc")> _
    Public Property ServicioPorc() As Nullable(Of Decimal)
        Get
            Return Me._decServicioPorc
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decServicioPorc = value
        End Set
    End Property

    <XmlElement("MontoServicioSoles")> _
    Public Property MontoServicioSoles() As Nullable(Of Decimal)
        Get
            Return Me._decMontoServicioSoles
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoServicioSoles = value
        End Set
    End Property

    <XmlElement("MontoServicioDolar")> _
    Public Property MontoServicioDolar() As Nullable(Of Decimal)
        Get
            Return Me._decMontoServicioDolar
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoServicioDolar = value
        End Set
    End Property

    <XmlElement("NroConstancia")> _
        Public Property NroConstancia() As String
        Get
            Return Me._strNroConstancia
        End Get
        Set(ByVal value As String)
            Me._strNroConstancia = value
        End Set
    End Property

    <XmlElement("FechaEmisionServicio")> _
    Public Property FechaEmisionServicio() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaEmisionServicio
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaEmisionServicio = value
        End Set
    End Property

    <XmlElement("CodigoTipoDocumentoProveedor")> _
   Public Property CodigoTipoDocumentoProveedor() As Nullable(Of Integer)
        Get
            Return Me._intCodigoTipoDocumentoProveedor
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodigoTipoDocumentoProveedor = value
        End Set
    End Property

    <XmlElement("NumeroDocumentoProveedor")> _
       Public Property NumeroDocumentoProveedor() As String
        Get
            Return Me._strNumeroDocumentoProveedor
        End Get
        Set(ByVal value As String)
            Me._strNumeroDocumentoProveedor = value
        End Set
    End Property

    <XmlElement("NombreProveedor")> _
       Public Property NombreProveedor() As String
        Get
            Return Me._strNombreProveedor
        End Get
        Set(ByVal value As String)
            Me._strNombreProveedor = value
        End Set
    End Property

    <XmlElement("NombreTipoComprobDoc1")> _
       Public Property NombreTipoComprobDoc1() As String
        Get
            Return Me._strNombreTipoComprobDoc1
        End Get
        Set(ByVal value As String)
            Me._strNombreTipoComprobDoc1 = value
        End Set
    End Property

    <XmlElement("NombreTipoAduana")> _
    Public Property NombreTipoAduana() As String
        Get
            Return Me._strNombreTipoAduana
        End Get
        Set(ByVal value As String)
            Me._strNombreTipoAduana = value
        End Set
    End Property

    <XmlElement("NombreTipoServicio")> _
    Public Property NombreTipoServicio() As String
        Get
            Return Me._strNombreTipoServicio
        End Get
        Set(ByVal value As String)
            Me._strNombreTipoServicio = value
        End Set
    End Property

    <XmlElement("NombreTipoComprobDoc2")> _
    Public Property NombreTipoComprobDoc2() As String
        Get
            Return Me._strNombreTipoComprobDoc2
        End Get
        Set(ByVal value As String)
            Me._strNombreTipoComprobDoc2 = value
        End Set
    End Property

    <XmlElement("StringFechaEmision")> _
    Public Property StringFechaEmision() As String
        Get
            Return Me._strFechaEmision
        End Get
        Set(ByVal value As String)
            Me._strFechaEmision = value
        End Set
    End Property

    <XmlElement("StringFechaVencimiento")> _
   Public Property StringFechaVencimiento() As String
        Get
            Return Me._strFechaVencimiento
        End Get
        Set(ByVal value As String)
            Me._strFechaVencimiento = value
        End Set
    End Property

    <XmlElement("StringFechaEmisionServicio")> _
    Public Property StringFechaEmisionServicio() As String
        Get
            Return Me._strFechaEmisionServicio
        End Get
        Set(ByVal value As String)
            Me._strFechaEmisionServicio = value
        End Set
    End Property

    <XmlElement("StringFechaEmisionAdd")> _
    Public Property StringFechaEmisionAdd() As String
        Get
            Return Me._strFechaEmisionAdd
        End Get
        Set(ByVal value As String)
            Me._strFechaEmisionAdd = value
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

    <XmlElement("KeyFechaEmision")> _
    Public Property KeyFechaEmision() As String
        Get
            Return Me._strKeyFechaEmision
        End Get
        Set(ByVal value As String)
            Me._strKeyFechaEmision = value
        End Set
    End Property

    <XmlElement("KeyTipoComprobante")> _
    Public Property KeyTipoComprobante() As String
        Get
            Return Me._strKeyTipoComprobante
        End Get
        Set(ByVal value As String)
            Me._strKeyTipoComprobante = value
        End Set
    End Property

    <XmlElement("KeyNumeroComprobante")> _
    Public Property KeyNumeroComprobante() As String
        Get
            Return Me._strKeyNumeroComprobante
        End Get
        Set(ByVal value As String)
            Me._strKeyNumeroComprobante = value
        End Set
    End Property

    <XmlElement("KeyCodProveedor")> _
    Public Property KeyCodProveedor() As String
        Get
            Return Me._strKeyCodProveedor
        End Get
        Set(ByVal value As String)
            Me._strKeyCodProveedor = value
        End Set
    End Property

    <XmlElement("NroInstruccionWIO")> _
   Public Property NroInstruccionWIO() As String
        Get
            Return Me._strNroInstruccionWIO
        End Get
        Set(ByVal value As String)
            Me._strNroInstruccionWIO = value
        End Set
    End Property

    <XmlElement("NroSecuenciaWIO")> _
    Public Property NroSecuenciaWIO() As String
        Get
            Return Me._strNroSecuenciaWIO
        End Get
        Set(ByVal value As String)
            Me._strNroSecuenciaWIO = value
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

    <XmlElement("CodSolicitudcreditoAdd")> _
Public Property CodSolicitudcreditoAdd() As String
        Get
            Return Me._strCodSolicitudcreditoAdd
        End Get
        Set(ByVal value As String)
            Me._strCodSolicitudcreditoAdd = value
        End Set
    End Property

    <XmlElement("Porc4ta")> _
    Public Property Porc4ta() As Nullable(Of Decimal)
        Get
            Return Me._decPorc4ta
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPorc4ta = value
        End Set
    End Property

    <XmlElement("Monto4taSoles")> _
    Public Property Monto4taSoles() As Nullable(Of Decimal)
        Get
            Return Me._decMonto4taSoles
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMonto4taSoles = value
        End Set
    End Property

    <XmlElement("Monto4taDolares")> _
    Public Property Monto4taDolares() As Nullable(Of Decimal)
        Get
            Return Me._decMonto4taDolares
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMonto4taDolares = value
        End Set
    End Property

#End Region

End Class