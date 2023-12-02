Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad EGCC_InsDesembolso
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 25/09/2012
''' </remarks>
<Serializable(), XmlRoot("EGCC_InsDesembolso")> _
Public Class EGCC_InsDesembolso

#Region " Atributos "

    Private _strCodinstrucciondesembolso As String
    Private _strCodsolicitudcredito As String
    Private _strCodestadoinstruccion As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String

    Private _strCUCliente As String
    Private _strRazonSocial As String
    Private _strFechaInicio As String
    Private _strFechaFin As String
    Private _strTipoContrato As String
    Private _strCodigoEstado As String
    Private _strCodigoMoneda As String

    Private _strDocumentos As String
    Private _strConInstruccion As String

    Private _strUsuarioRegistro As String


    Private _dectcdia As Nullable(Of Decimal)
    'Inicio IBK - AAE
    Private _intFlagLPC As Nullable(Of Integer)
    Private _strNroWIO As String
    Private _strFlagActivacion As String
    'Fin IBK
#End Region

#Region " Propiedades "

    <XmlElement("tcdia")> _
    Public Property tcdia() As Nullable(Of Decimal)
        Get
            Return Me._dectcdia
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._dectcdia = value
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

    <XmlElement("Codestadoinstruccion")> _
    Public Property Codestadoinstruccion() As String
        Get
            Return Me._strCodestadoinstruccion
        End Get
        Set(ByVal value As String)
            Me._strCodestadoinstruccion = value
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

    <XmlElement("FechaInicio")> _
    Public Property FechaInicio() As String
        Get
            Return Me._strFechaInicio
        End Get
        Set(ByVal value As String)
            Me._strFechaInicio = value
        End Set
    End Property

    <XmlElement("FechaFin")> _
    Public Property FechaFin() As String
        Get
            Return Me._strFechaFin
        End Get
        Set(ByVal value As String)
            Me._strFechaFin = value
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

    <XmlElement("Documentos")> _
    Public Property Documentos() As String
        Get
            Return Me._strDocumentos
        End Get
        Set(ByVal value As String)
            Me._strDocumentos = value
        End Set
    End Property

    <XmlElement("ConInstruccion")> _
    Public Property ConInstruccion() As String
        Get
            Return Me._strConInstruccion
        End Get
        Set(ByVal value As String)
            Me._strConInstruccion = value
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
    'Inicio IBK - AAE
    <XmlElement("FlagLPC")> _
    Public Property FlagLPC() As Nullable(Of Integer)
        Get
            Return Me._intFlagLPC
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intFlagLPC = value
        End Set
    End Property

    <XmlElement("NroWIO")> _
    Public Property NroWIO() As String
        Get
            Return Me._strNroWIO
        End Get
        Set(ByVal value As String)
            Me._strNroWIO = value
        End Set
    End Property

    <XmlElement("FlagActivacion")> _
    Public Property FlagActivacion() As String
        Get
            Return Me._strFlagActivacion
        End Get
        Set(ByVal value As String)
            Me._strFlagActivacion = value
        End Set
    End Property
    'Fin IBK

#End Region

End Class

