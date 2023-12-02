
Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad GccOpcioncompra
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 07/01/2013 06:38:56 p.m.
''' </remarks>
<Serializable(), XmlRoot("EGCC_OpcionCompra")> _
Public Class EGCC_OpcionCompra

#Region " Atributos "

    Private _intItem As Integer = 0
    Private _lngCodOpcionCompra As Long = 0
    Private _strCodSolicitudCredito As String = String.Empty
    Private _decPorcentajeComision As Decimal = 0
    Private _decPorcentajeGastoTransferencia As Decimal = 0
    Private _strCodEstado As String = String.Empty
    Private _dtmAudFechaRegistro As DateTime = New DateTime(1900, 1, 1)
    Private _dtmAudFechaModificacion As DateTime = New DateTime(1900, 1, 1)
    Private _strAudUsuarioRegistro As String = String.Empty
    Private _strAudUsuarioModificacion As String = String.Empty

    Private _strNumeroContrato As String = String.Empty
    Private _strCodigoUnico As String = String.Empty
    Private _strRazonSocial As String = String.Empty
    Private _strClasificacionBien As String = String.Empty
    Private _strTipoBien As String = String.Empty
    Private _strTipoEnvio As String = String.Empty
    Private _strDemanda As String = String.Empty
    Private _strPlacaActual As String = String.Empty
    Private _strNroSerie As String = String.Empty
    Private _shtSecFinanciamiento As Short = 0

    Private _strDescripcionBien As String = String.Empty
    Private _strUbicacion As String = String.Empty
    Private _strDepartamentoNombre As String = String.Empty
    Private _strProvinciaNombre As String = String.Empty
    Private _strDistritoNombre As String = String.Empty    
    Private _strNroMotor As String = String.Empty
    Private _strMarca As String = String.Empty
    Private _strFlagAceptacionCliente As String = String.Empty
    Private _strCodTipoPagoOC As String = String.Empty
    Private _dtmFechaAceptacionCliente As DateTime = New DateTime(1900, 1, 1)
    Private _dtmFechaTransferencia As DateTime = New DateTime(1900, 1, 1)
    Private _dtmFechaTransferenciaRRP As DateTime = New DateTime(1900, 1, 1)
    Private _dtmFechaPagoOC As DateTime = New DateTime(1900, 1, 1)
    Private _strStringFechaAceptacionCliente As String = String.Empty
    Private _strStringFechaTransferencia As String = String.Empty
    Private _strStringFechaTransferenciaRRP As String = String.Empty
    Private _strStringFechaPagoOC As String = String.Empty

    Private _dtmFechaFiltro As DateTime = New DateTime(1900, 1, 1)
    Private _strXMLEntity As String = String.Empty
    Private _strOrdenEnvio As String = String.Empty

    Private _shtCodOperacionDocumento As Short = 0
    Private _shtCodOperacionDocumentoObservacion As Short = 0
    Private _strCodCheckList As String = String.Empty
    Private _strFlagCheckList As String = String.Empty
    Private _dtmFechaCheckList As DateTime = New DateTime(1900, 1, 1)
    Private _strAdjunto As String = String.Empty
    Private _strObservacion As String = String.Empty

    Private _decMontoComision As Decimal = 0
    Private _decMontoGastoTransferencia As Decimal = 0
    Private _intFlgComision As Integer = 0
    Private _intFlgGasto As Integer = 0

#End Region


#Region " Propiedades "
    <XmlElement("Item")> _
    Public Property Item() As Integer
        Get
            Return _intItem
        End Get
        Set(ByVal Value As Integer)
            _intItem = Value
        End Set
    End Property

    <XmlElement("CodOpcionCompra")> _
    Public Property CodOpcionCompra() As Long
        Get
            Return _lngCodOpcionCompra
        End Get
        Set(ByVal Value As Long)
            _lngCodOpcionCompra = Value
        End Set
    End Property


    <XmlElement("CodSolicitudCredito")> _
    Public Property CodSolicitudCredito() As String
        Get
            Return _strCodSolicitudCredito
        End Get
        Set(ByVal Value As String)
            _strCodSolicitudCredito = Value
        End Set
    End Property


    <XmlElement("PorcentajeComision")> _
    Public Property PorcentajeComision() As Decimal
        Get
            Return _decPorcentajeComision
        End Get
        Set(ByVal Value As Decimal)
            _decPorcentajeComision = Value
        End Set
    End Property


    <XmlElement("PorcentajeGastoTransferencia")> _
    Public Property PorcentajeGastoTransferencia() As Decimal
        Get
            Return _decPorcentajeGastoTransferencia
        End Get
        Set(ByVal Value As Decimal)
            _decPorcentajeGastoTransferencia = Value
        End Set
    End Property


    <XmlElement("CodEstado")> _
    Public Property CodEstado() As String
        Get
            Return _strCodEstado
        End Get
        Set(ByVal Value As String)
            _strCodEstado = Value
        End Set
    End Property


    <XmlElement("AudFechaRegistro")> _
    Public Property AudFechaRegistro() As DateTime
        Get
            Return _dtmAudFechaRegistro
        End Get
        Set(ByVal Value As DateTime)
            _dtmAudFechaRegistro = Value
        End Set
    End Property


    <XmlElement("AudFechaModificacion")> _
    Public Property AudFechaModificacion() As DateTime
        Get
            Return _dtmAudFechaModificacion
        End Get
        Set(ByVal Value As DateTime)
            _dtmAudFechaModificacion = Value
        End Set
    End Property


    <XmlElement("AudUsuarioRegistro")> _
    Public Property AudUsuarioRegistro() As String
        Get
            Return _strAudUsuarioRegistro
        End Get
        Set(ByVal Value As String)
            _strAudUsuarioRegistro = Value
        End Set
    End Property


    <XmlElement("AudUsuarioModificacion")> _
    Public Property AudUsuarioModificacion() As String
        Get
            Return _strAudUsuarioModificacion
        End Get
        Set(ByVal Value As String)
            _strAudUsuarioModificacion = Value
        End Set
    End Property

    <XmlElement("NumeroContrato")> _
    Public Property NumeroContrato() As String
        Get
            Return _strNumeroContrato
        End Get
        Set(ByVal Value As String)
            _strNumeroContrato = Value
        End Set
    End Property

    <XmlElement("CodigoUnico")> _
    Public Property CodigoUnico() As String
        Get
            Return _strCodigoUnico
        End Get
        Set(ByVal Value As String)
            _strCodigoUnico = Value
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

    <XmlElement("ClasificacionBien")> _
    Public Property ClasificacionBien() As String
        Get
            Return _strClasificacionBien
        End Get
        Set(ByVal Value As String)
            _strClasificacionBien = Value
        End Set
    End Property

    <XmlElement("TipoBien")> _
    Public Property TipoBien() As String
        Get
            Return _strTipoBien
        End Get
        Set(ByVal Value As String)
            _strTipoBien = Value
        End Set
    End Property

    <XmlElement("TipoEnvio")> _
    Public Property TipoEnvio() As String
        Get
            Return _strTipoEnvio
        End Get
        Set(ByVal Value As String)
            _strTipoEnvio = Value
        End Set
    End Property

    <XmlElement("Demanda")> _
    Public Property Demanda() As String
        Get
            Return _strDemanda
        End Get
        Set(ByVal Value As String)
            _strDemanda = Value
        End Set
    End Property

    <XmlElement("PlacaActual")> _
    Public Property PlacaActual() As String
        Get
            Return _strPlacaActual
        End Get
        Set(ByVal Value As String)
            _strPlacaActual = Value
        End Set
    End Property

    <XmlElement("NroSerie")> _
    Public Property NroSerie() As String
        Get
            Return _strNroSerie
        End Get
        Set(ByVal Value As String)
            _strNroSerie = Value
        End Set
    End Property

    <XmlElement("SecFinanciamiento")> _
    Public Property SecFinanciamiento() As Long
        Get
            Return _shtSecFinanciamiento
        End Get
        Set(ByVal Value As Long)
            _shtSecFinanciamiento = Value
        End Set
    End Property

    <XmlElement("DescripcionBien")> _
    Public Property DescripcionBien() As String
        Get
            Return _strDescripcionBien
        End Get
        Set(ByVal Value As String)
            _strDescripcionBien = Value
        End Set
    End Property

    <XmlElement("Ubicacion")> _
    Public Property Ubicacion() As String
        Get
            Return _strUbicacion
        End Get
        Set(ByVal Value As String)
            _strUbicacion = Value
        End Set
    End Property

    <XmlElement("DepartamentoNombre")> _
    Public Property DepartamentoNombre() As String
        Get
            Return _strDepartamentoNombre
        End Get
        Set(ByVal Value As String)
            _strDepartamentoNombre = Value
        End Set
    End Property

    <XmlElement("ProvinciaNombre")> _
    Public Property ProvinciaNombre() As String
        Get
            Return _strProvinciaNombre
        End Get
        Set(ByVal Value As String)
            _strProvinciaNombre = Value
        End Set
    End Property

    <XmlElement("DistritoNombre")> _
    Public Property DistritoNombre() As String
        Get
            Return _strDistritoNombre
        End Get
        Set(ByVal Value As String)
            _strDistritoNombre = Value
        End Set
    End Property

    <XmlElement("NroMotor")> _
    Public Property NroMotor() As String
        Get
            Return _strNroMotor
        End Get
        Set(ByVal Value As String)
            _strNroMotor = Value
        End Set
    End Property

    <XmlElement("Marca")> _
    Public Property Marca() As String
        Get
            Return _strMarca
        End Get
        Set(ByVal Value As String)
            _strMarca = Value
        End Set
    End Property

    <XmlElement("FlagAceptacionCliente")> _
    Public Property FlagAceptacionCliente() As String
        Get
            Return _strFlagAceptacionCliente
        End Get
        Set(ByVal Value As String)
            _strFlagAceptacionCliente = Value
        End Set
    End Property

    <XmlElement("CodTipoPagoOC")> _
    Public Property CodTipoPagoOC() As String
        Get
            Return _strCodTipoPagoOC
        End Get
        Set(ByVal Value As String)
            _strCodTipoPagoOC = Value
        End Set
    End Property

    <XmlElement("StringFechaAceptacionCliente")> _
    Public Property StringFechaAceptacionCliente() As String
        Get
            Return _strStringFechaAceptacionCliente
        End Get
        Set(ByVal Value As String)
            _strStringFechaAceptacionCliente = Value
        End Set
    End Property

    <XmlElement("StringFechaTransferencia")> _
    Public Property StringFechaTransferencia() As String
        Get
            Return _strStringFechaTransferencia
        End Get
        Set(ByVal Value As String)
            _strStringFechaTransferencia = Value
        End Set
    End Property

    <XmlElement("StringFechaTransferenciaRRPP")> _
    Public Property StringFechaTransferenciaRRPP() As String
        Get
            Return _strStringFechaTransferenciaRRP
        End Get
        Set(ByVal Value As String)
            _strStringFechaTransferenciaRRP = Value
        End Set
    End Property

    <XmlElement("StringFechaPagoOC")> _
    Public Property StringFechaPagoOC() As String
        Get
            Return _strStringFechaPagoOC
        End Get
        Set(ByVal Value As String)
            _strStringFechaPagoOC = Value
        End Set
    End Property

    <XmlElement("FechaAceptacionCliente")> _
    Public Property FechaAceptacionCliente() As DateTime
        Get
            Return _dtmFechaAceptacionCliente
        End Get
        Set(ByVal Value As DateTime)
            _dtmFechaAceptacionCliente = Value
        End Set
    End Property

    <XmlElement("FechaTransferencia")> _
    Public Property FechaTransferencia() As DateTime
        Get
            Return _dtmFechaTransferencia
        End Get
        Set(ByVal Value As DateTime)
            _dtmFechaTransferencia = Value
        End Set
    End Property

    <XmlElement("FechaTransferenciaRRPP")> _
    Public Property FechaTransferenciaRRPP() As DateTime
        Get
            Return _dtmFechaTransferenciaRRP
        End Get
        Set(ByVal Value As DateTime)
            _dtmFechaTransferenciaRRP = Value
        End Set
    End Property

    <XmlElement("FechaPagoOC")> _
    Public Property FechaPagoOC() As DateTime
        Get
            Return _dtmFechaPagoOC
        End Get
        Set(ByVal Value As DateTime)
            _dtmFechaPagoOC = Value
        End Set
    End Property


    <XmlElement("FechaFiltro")> _
    Public Property FechaFiltro() As DateTime
        Get
            Return _dtmFechaFiltro
        End Get
        Set(ByVal Value As DateTime)
            _dtmFechaFiltro = Value
        End Set
    End Property

    <XmlElement("XMLEntity")> _
    Public Property XMLEntity() As String
        Get
            Return _strXMLEntity
        End Get
        Set(ByVal Value As String)
            _strXMLEntity = Value
        End Set
    End Property

    <XmlElement("OrdenEnvio")> _
    Public Property OrdenEnvio() As String
        Get
            Return _strOrdenEnvio
        End Get
        Set(ByVal Value As String)
            _strOrdenEnvio = Value
        End Set
    End Property

    <XmlElement("CodOperacionDocumento")> _
    Public Property CodOperacionDocumento() As Short
        Get
            Return _shtCodOperacionDocumento
        End Get
        Set(ByVal Value As Short)
            _shtCodOperacionDocumento = Value
        End Set
    End Property

    <XmlElement("CodOperacionDocumentoObservacion")> _
    Public Property CodOperacionDocumentoObservacion() As Short
        Get
            Return _shtCodOperacionDocumentoObservacion
        End Get
        Set(ByVal Value As Short)
            _shtCodOperacionDocumentoObservacion = Value
        End Set
    End Property

    <XmlElement("CodCheckList")> _
    Public Property CodCheckList() As String
        Get
            Return _strCodCheckList
        End Get
        Set(ByVal Value As String)
            _strCodCheckList = Value
        End Set
    End Property

    <XmlElement("FlagCheckList")> _
    Public Property FlagCheckList() As String
        Get
            Return _strFlagCheckList
        End Get
        Set(ByVal Value As String)
            _strFlagCheckList = Value
        End Set
    End Property

    <XmlElement("FechaCheckList")> _
    Public Property FechaCheckList() As DateTime
        Get
            Return _dtmFechaCheckList
        End Get
        Set(ByVal Value As DateTime)
            _dtmFechaCheckList = Value
        End Set
    End Property

    <XmlElement("Adjunto")> _
    Public Property Adjunto() As String
        Get
            Return _strAdjunto
        End Get
        Set(ByVal Value As String)
            _strAdjunto = Value
        End Set
    End Property

    <XmlElement("Observacion")> _
    Public Property Observacion() As String
        Get
            Return _strObservacion
        End Get
        Set(ByVal Value As String)
            _strObservacion = Value
        End Set
    End Property

    <XmlElement("FlgComision")> _
    Public Property FlgComision() As Integer
        Get
            Return _intFlgComision
        End Get
        Set(ByVal Value As Integer)
            _intFlgComision = Value
        End Set
    End Property

    <XmlElement("FlgGasto")> _
    Public Property FlgGasto() As Integer
        Get
            Return _intFlgGasto
        End Get
        Set(ByVal Value As Integer)
            _intFlgGasto = Value
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

    <XmlElement("MontoGastoTransferencia")> _
    Public Property MontoGastoTransferencia() As Decimal
        Get
            Return _decMontoGastoTransferencia
        End Get
        Set(ByVal Value As Decimal)
            _decMontoGastoTransferencia = Value
        End Set
    End Property

#End Region

End Class



