Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_demanda
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_demanda")> _
Public Class EGcc_Demanda

#Region " Atributos "

    Private _strCodSolCredito As String
    Private _intSecFinanciamiento As Nullable(Of Integer)
    Private _intCodDemanda As Nullable(Of Integer)
    Private _dtmFechaDemanda As Nullable(Of DateTime)
    Private _strEstadoDemanda As String
    Private _strJuzgado As String
    Private _strCodMoneda As String
    Private _decMontoDemanda As Nullable(Of Decimal)
    Private _strUsuarioRegistro As String
    Private _strUsuarioModificacion As String
    Private _strNroDemanda As String
    Private _strEstadoLogico As String

    Private _strFechaDemandaStr As String

    Private _strNroContrato As String
    Private _strEstadoContrato As String
    Private _strCUCliente As String
    Private _strTipoDocumento As String
    Private _strNroDocumento As String
    Private _strRazonSocial As String
    Private _strClasificacionBien As String
    Private _strPlaca As String
    Private _strNroMotor As String
    Private _strTipoBien As String
    Private _strUbicacion As String

    Private _strTipoSiniestro As String
    Private _intCodSiniestro As Nullable(Of Integer)

#End Region

#Region " Propiedades "

    <XmlElement("EstadoLogico")> _
    Public Property EstadoLogico() As String
        Get
            Return Me._strEstadoLogico
        End Get
        Set(ByVal value As String)
            Me._strEstadoLogico = value
        End Set
    End Property

    <XmlElement("CodSolCredito")> _
   Public Property CodSolCredito() As String
        Get
            Return Me._strCodSolCredito
        End Get
        Set(ByVal value As String)
            Me._strCodSolCredito = value
        End Set
    End Property

    <XmlElement("SecFinanciamiento")> _
    Public Property SecFinanciamiento() As Nullable(Of Integer)
        Get
            Return Me._intSecFinanciamiento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intSecFinanciamiento = value
        End Set
    End Property

    <XmlElement("CodDemanda")> _
    Public Property CodDemanda() As Nullable(Of Integer)
        Get
            Return Me._intCodDemanda
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodDemanda = value
        End Set
    End Property


    <XmlElement("NroDemanda")> _
    Public Property NroDemanda() As String
        Get
            Return Me._strNroDemanda
        End Get
        Set(ByVal value As String)
            Me._strNroDemanda = value
        End Set
    End Property

    <XmlElement("FechaDemanda")> _
    Public Property FechaDemanda() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaDemanda
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaDemanda = value
        End Set
    End Property

    <XmlElement("EstadoDemanda")> _
   Public Property EstadoDemanda() As String
        Get
            Return Me._strEstadoDemanda
        End Get
        Set(ByVal value As String)
            Me._strEstadoDemanda = value
        End Set
    End Property

    <XmlElement("Juzgado")> _
   Public Property Juzgado() As String
        Get
            Return Me._strJuzgado
        End Get
        Set(ByVal value As String)
            Me._strJuzgado = value
        End Set
    End Property

    <XmlElement("CodMoneda")> _
   Public Property CodMoneda() As String
        Get
            Return Me._strCodMoneda
        End Get
        Set(ByVal value As String)
            Me._strCodMoneda = value
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

    <XmlElement("UsuarioModificacion")> _
    Public Property UsuarioModificacion() As String
        Get
            Return Me._strUsuarioModificacion
        End Get
        Set(ByVal value As String)
            Me._strUsuarioModificacion = value
        End Set
    End Property

    <XmlElement("MontoDemanda")> _
    Public Property MontoDemanda() As Nullable(Of Decimal)
        Get
            Return Me._decMontoDemanda
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoDemanda = value
        End Set
    End Property



    <XmlElement("FechaDemandaStr")> _
   Public Property FechaDemandaStr() As String
        Get
            Return Me._strFechaDemandaStr
        End Get
        Set(ByVal value As String)
            Me._strFechaDemandaStr = value
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

    <XmlElement("EstadoContrato")> _
    Public Property EstadoContrato() As String
        Get
            Return Me._strEstadoContrato
        End Get
        Set(ByVal value As String)
            Me._strEstadoContrato = value
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

    <XmlElement("RazonSocial")> _
    Public Property RazonSocial() As String
        Get
            Return Me._strRazonSocial
        End Get
        Set(ByVal value As String)
            Me._strRazonSocial = value
        End Set
    End Property

    <XmlElement("ClasificacionBien")> _
    Public Property ClasificacionBien() As String
        Get
            Return Me._strClasificacionBien
        End Get
        Set(ByVal value As String)
            Me._strClasificacionBien = value
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

    <XmlElement("NroMotor")> _
    Public Property NroMotor() As String
        Get
            Return Me._strNroMotor
        End Get
        Set(ByVal value As String)
            Me._strNroMotor = value
        End Set
    End Property

    <XmlElement("TipoBien")> _
    Public Property TipoBien() As String
        Get
            Return Me._strTipoBien
        End Get
        Set(ByVal value As String)
            Me._strTipoBien = value
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

    <XmlElement("TipoSiniestro")> _
    Public Property TipoSiniestro() As String
        Get
            Return Me._strTipoSiniestro
        End Get
        Set(ByVal value As String)
            Me._strTipoSiniestro = value
        End Set
    End Property

    <XmlElement("CodSiniestro")> _
    Public Property CodSiniestro() As Nullable(Of Integer)
        Get
            Return Me._intCodSiniestro
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodSiniestro = value
        End Set
    End Property

#End Region

End Class
