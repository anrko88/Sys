Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad EGCC_PagoCuotas
''' </summary>
''' <remarks>
''' Creado Por         : IBK - RPR
''' Fecha de Creación  : 19/12/2012
''' </remarks>
<Serializable(), XmlRoot("EGCC_PagoCuotas")> _
Public Class EGCC_PagoCuotas

#Region " Atributos "

    Private _strCodSolicitudCredito As String
    Private _strSecRecuperacion As String
    Private _strCUCliente As String
    Private _strRazonSocial As String
    Private _strFechaPagoInicio As String
    Private _strFechaPagoFin As String
    Private _strTipoContrato As String
    Private _strCodigoEstado As String
    Private _strCodigoMoneda As String
    Private _strNroAutorizacion As String

    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String
    Private _strUsuarioRegistro As String

    Private _intNroCuotasxPagar As Integer
    Private _intNroCuotasVencidas As Integer
    Private _intNroPagosCuotasxProcesar As Integer
    Private _intNroConceptoPendiente As Integer
    Private _decMontoDesembolsado As Decimal
    Private _decMontoRecuperado As Decimal

    Private _strCodError As String
    Private _strMsgError As String

#End Region

#Region " Propiedades "

    <XmlElement("CodSolicitudCredito")> _
    Public Property CodSolicitudCredito() As String
        Get
            Return Me._strCodSolicitudCredito
        End Get
        Set(ByVal value As String)
            Me._strCodSolicitudCredito = value
        End Set
    End Property

    <XmlElement("SecRecuperacion")> _
    Public Property SecRecuperacion() As String
        Get
            Return Me._strSecRecuperacion
        End Get
        Set(ByVal value As String)
            Me._strSecRecuperacion = value
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

    <XmlElement("CUCliente")> _
    Public Property CUCliente() As String
        Get
            Return Me._strCUCliente
        End Get
        Set(ByVal value As String)
            Me._strCUCliente = value
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

    <XmlElement("FechaPagoInicio")> _
    Public Property FechaPagoInicio() As String
        Get
            Return Me._strFechaPagoInicio
        End Get
        Set(ByVal value As String)
            Me._strFechaPagoInicio = value
        End Set
    End Property

    <XmlElement("FechaPagoFin")> _
    Public Property FechaPagoFin() As String
        Get
            Return Me._strFechaPagoFin
        End Get
        Set(ByVal value As String)
            Me._strFechaPagoFin = value
        End Set
    End Property

    <XmlElement("TipoContrato")> _
    Public Property TipoContrato() As String
        Get
            Return Me._strTipoContrato
        End Get
        Set(ByVal value As String)
            Me._strTipoContrato = value
        End Set
    End Property

    <XmlElement("CodigoEstado")> _
    Public Property CodigoEstado() As String
        Get
            Return Me._strCodigoEstado
        End Get
        Set(ByVal value As String)
            Me._strCodigoEstado = value
        End Set
    End Property

    <XmlElement("CodigoMoneda")> _
    Public Property CodigoMoneda() As String
        Get
            Return Me._strCodigoMoneda
        End Get
        Set(ByVal value As String)
            Me._strCodigoMoneda = value
        End Set
    End Property

    <XmlElement("UsuarioRegistro")> _
    Public Property UsuarioRegistro() As String
        Get
            Return Me._strUsuarioRegistro
        End Get
        Set(ByVal value As String)
            Me._strUsuarioRegistro = value
        End Set
    End Property

    <XmlElement("NroAutorizacion")> _
    Public Property NroAutorizacion() As String
        Get
            Return Me._strNroAutorizacion
        End Get
        Set(ByVal value As String)
            Me._strNroAutorizacion = value
        End Set
    End Property

    <XmlElement("NroCuotasxPagar")> _
    Public Property NroCuotasxPagar() As Integer
        Get
            Return Me._intNroCuotasxPagar
        End Get
        Set(ByVal value As Integer)
            Me._intNroCuotasxPagar = value
        End Set
    End Property

    <XmlElement("NroCuotasVencidas")> _
    Public Property NroCuotasVencidas() As Integer
        Get
            Return Me._intNroCuotasVencidas
        End Get
        Set(ByVal value As Integer)
            Me._intNroCuotasVencidas = value
        End Set
    End Property

    <XmlElement("NroPagosCuotasxProcesar")> _
    Public Property NroPagosCuotasxProcesar() As Integer
        Get
            Return Me._intNroPagosCuotasxProcesar
        End Get
        Set(ByVal value As Integer)
            Me._intNroPagosCuotasxProcesar = value
        End Set
    End Property

    <XmlElement("NroConceptoPendiente")> _
    Public Property NroConceptoPendiente() As Integer
        Get
            Return Me._intNroConceptoPendiente
        End Get
        Set(ByVal value As Integer)
            Me._intNroConceptoPendiente = value
        End Set
    End Property

    <XmlElement("MontoDesembolsado")> _
    Public Property MontoDesembolsado() As Decimal
        Get
            Return Me._decMontoDesembolsado
        End Get
        Set(ByVal value As Decimal)
            Me._decMontoDesembolsado = value
        End Set
    End Property

    <XmlElement("MontoRecuperado")> _
    Public Property MontoRecuperado() As Decimal
        Get
            Return Me._decMontoRecuperado
        End Get
        Set(ByVal value As Decimal)
            Me._decMontoRecuperado = value
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

#End Region

End Class

