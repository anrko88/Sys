Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad EGCC_InsDesembolsoPago
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGCC_InsDesembolsoPago")> _
Public Class EGCC_InsDesembolsoPago

#Region " Atributos "

    Private _strCodmedioabono As String
    Private _strCodmonedacuenta As String
    Private _strCodtipocuenta As String
    Private _strNumero_cuenta As String
    Private _strNumcuentainterbancaria As String
    Private _strPendiente As String
    Private _strNota As String
    Private _strEmisora As String
    Private _strReceptora As String
    Private _strCodmonedapago As String
    Private _strCodproveedor As String
    Private _strCodmonedapendiente As String
    Private _strCodpagocomision As String
    Private _strComentario As String
    Private _strAdjunto As String
    Private _decImportecomision As Nullable(Of Decimal)
    Private _strCodtipodocumento As String
    Private _strNumerodocumento As String
    Private _strRazonsocial As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String
    Private _strCodinstrucciondesembolso As String
    Private _strCodsolicitudcredito As String
    Private _strCodagrupacion As String
    Private _strCodmonedaagrupacion As String
    'Inicio IBK - AAE - Se agrega codigoEstadoEjecucionPago
    Private _strCodEstadoEjecucionPago As String
    Private _strCodMonedaCargoAbono As String
    Private _decMontoCargoAbono As Nullable(Of Decimal)
    Private _decMontoIGVCargo As Nullable(Of Decimal)
    Private _decCargoNoDom As Nullable(Of Decimal)
    Private _strCtaCargoNoDom As String
    Private _decAbonoNoDom As Nullable(Of Decimal)
    Private _strCtaAbonoNoDom As String
    'Fin IBK

#End Region

#Region " Propiedades "

    <XmlElement("Codmedioabono")> _
    Public Property Codmedioabono() As String
        Get
            Return Me._strCodmedioabono
        End Get
        Set(ByVal value As String)
            Me._strCodmedioabono = value
        End Set
    End Property

    <XmlElement("Codmonedacuenta")> _
    Public Property Codmonedacuenta() As String
        Get
            Return Me._strCodmonedacuenta
        End Get
        Set(ByVal value As String)
            Me._strCodmonedacuenta = value
        End Set
    End Property

    <XmlElement("Codtipocuenta")> _
    Public Property Codtipocuenta() As String
        Get
            Return Me._strCodtipocuenta
        End Get
        Set(ByVal value As String)
            Me._strCodtipocuenta = value
        End Set
    End Property

    <XmlElement("Numero_cuenta")> _
    Public Property Numero_cuenta() As String
        Get
            Return Me._strNumero_cuenta
        End Get
        Set(ByVal value As String)
            Me._strNumero_cuenta = value
        End Set
    End Property

    <XmlElement("Numcuentainterbancaria")> _
    Public Property Numcuentainterbancaria() As String
        Get
            Return Me._strNumcuentainterbancaria
        End Get
        Set(ByVal value As String)
            Me._strNumcuentainterbancaria = value
        End Set
    End Property

    <XmlElement("Pendiente")> _
    Public Property Pendiente() As String
        Get
            Return Me._strPendiente
        End Get
        Set(ByVal value As String)
            Me._strPendiente = value
        End Set
    End Property

    <XmlElement("Nota")> _
    Public Property Nota() As String
        Get
            Return Me._strNota
        End Get
        Set(ByVal value As String)
            Me._strNota = value
        End Set
    End Property

    <XmlElement("Emisora")> _
    Public Property Emisora() As String
        Get
            Return Me._strEmisora
        End Get
        Set(ByVal value As String)
            Me._strEmisora = value
        End Set
    End Property

    <XmlElement("Receptora")> _
    Public Property Receptora() As String
        Get
            Return Me._strReceptora
        End Get
        Set(ByVal value As String)
            Me._strReceptora = value
        End Set
    End Property

    <XmlElement("Codmonedapago")> _
    Public Property Codmonedapago() As String
        Get
            Return Me._strCodmonedapago
        End Get
        Set(ByVal value As String)
            Me._strCodmonedapago = value
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

    <XmlElement("Codmonedapendiente")> _
    Public Property Codmonedapendiente() As String
        Get
            Return Me._strCodmonedapendiente
        End Get
        Set(ByVal value As String)
            Me._strCodmonedapendiente = value
        End Set
    End Property

    <XmlElement("Codpagocomision")> _
    Public Property Codpagocomision() As String
        Get
            Return Me._strCodpagocomision
        End Get
        Set(ByVal value As String)
            Me._strCodpagocomision = value
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

    <XmlElement("Adjunto")> _
    Public Property Adjunto() As String
        Get
            Return Me._strAdjunto
        End Get
        Set(ByVal value As String)
            Me._strAdjunto = value
        End Set
    End Property

    <XmlElement("Importecomision")> _
    Public Property Importecomision() As Nullable(Of Decimal)
        Get
            Return Me._decImportecomision
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImportecomision = value
        End Set
    End Property

    <XmlElement("Codtipodocumento")> _
    Public Property Codtipodocumento() As String
        Get
            Return Me._strCodtipodocumento
        End Get
        Set(ByVal value As String)
            Me._strCodtipodocumento = value
        End Set
    End Property

    <XmlElement("Numerodocumento")> _
    Public Property Numerodocumento() As String
        Get
            Return Me._strNumerodocumento
        End Get
        Set(ByVal value As String)
            Me._strNumerodocumento = value
        End Set
    End Property

    <XmlElement("Razonsocial")> _
    Public Property Razonsocial() As String
        Get
            Return Me._strRazonsocial
        End Get
        Set(ByVal value As String)
            Me._strRazonsocial = value
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

    <XmlElement("Codinstrucciondesembolso")> _
    Public Property Codinstrucciondesembolso() As String
        Get
            Return Me._strCodinstrucciondesembolso
        End Get
        Set(ByVal value As String)
            Me._strCodinstrucciondesembolso = value
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

    <XmlElement("Codagrupacion")> _
    Public Property Codagrupacion() As String
        Get
            Return Me._strCodagrupacion
        End Get
        Set(ByVal value As String)
            Me._strCodagrupacion = value
        End Set
    End Property

    <XmlElement("Codmonedaagrupacion")> _
    Public Property Codmonedaagrupacion() As String
        Get
            Return Me._strCodmonedaagrupacion
        End Get
        Set(ByVal value As String)
            Me._strCodmonedaagrupacion = value
        End Set
    End Property

    'Inicio IBK - AAE - Se agregan métodos para atributo codEstadoEjecucionPago
    <XmlElement("CodEstadoEjecucionPago")> _
    Public Property CodEstadoEjecucionPago() As String
        Get
            Return Me._strCodEstadoEjecucionPago
        End Get
        Set(ByVal value As String)
            Me._strCodEstadoEjecucionPago = value
        End Set
    End Property

    <XmlElement("CodMonedaCargoAbono")> _
    Public Property CodMonedaCargoAbono() As String
        Get
            Return Me._strCodMonedaCargoAbono
        End Get
        Set(ByVal value As String)
            Me._strCodMonedaCargoAbono = value
        End Set
    End Property

    <XmlElement("MontoCargoAbono")> _
    Public Property MontoCargoAbono() As Nullable(Of Decimal)
        Get
            Return Me._decMontoCargoAbono
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoCargoAbono = value
        End Set
    End Property

    <XmlElement("MontoIGVCargo")> _
    Public Property MontoIGVCargo() As Nullable(Of Decimal)
        Get
            Return Me._decMontoIGVCargo
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoIGVCargo = value
        End Set
    End Property

    <XmlElement("CargoNoDom")> _
   Public Property CargoNoDom() As Nullable(Of Decimal)
        Get
            Return Me._decCargoNoDom
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decCargoNoDom = value
        End Set
    End Property

    <XmlElement("CtaCargoNoDom")> _
   Public Property CtaCargoNoDom() As String
        Get
            Return Me._strCtaCargoNoDom
        End Get
        Set(ByVal value As String)
            Me._strCtaCargoNoDom = value
        End Set
    End Property

    <XmlElement("AbonoNoDom")> _
   Public Property AbonoNoDom() As Nullable(Of Decimal)
        Get
            Return Me._decAbonoNoDom
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decAbonoNoDom = value
        End Set
    End Property

    <XmlElement("CtaAbonoNoDom")> _
  Public Property CtaAbonoNoDom() As String
        Get
            Return Me._strCtaAbonoNoDom
        End Get
        Set(ByVal value As String)
            Me._strCtaAbonoNoDom = value
        End Set
    End Property
    ' Fin IBK

#End Region

End Class