Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Siniestro
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("ESiniestro")> _
Public Class ESiniestro

#Region " Atributos "

    Private _strCodsolcredito As String
    Private _intSecfinanciamiento As Nullable(Of Integer)
    Private _intSecsiniestro As Nullable(Of Integer)
    Private _strNroSiniestro As String
    Private _strCodunico As String
    Private _decMontoindemnizacion As Nullable(Of Decimal)
    Private _strMoneda As String
    Private _dtmFecsiniestro As Nullable(Of DateTime)
    Private _strTipo As String
    Private _strSituacion As String
    Private _strAplicacion As String
    Private _strTransferencia As String
    Private _strContrato As String
    Private _strSeguro As String
    Private _strObservaciones As String
    Private _dtmFecregistro As Nullable(Of DateTime)
    Private _strUsuariomodificacion As String
    Private _dtmFecmodificacion As Nullable(Of DateTime)
    Private _strOrigen As String
    Private _dtmFecsituacion As Nullable(Of DateTime)
    Private _dtmFecaplicacion As Nullable(Of DateTime)
    Private _dtmFecconocimiento As Nullable(Of DateTime)
    Private _dtmFecrecindemnizacion As Nullable(Of DateTime)

    Private _dtmFecDescargoMunicipal As Nullable(Of DateTime)
    Private _dtmFecTransferencia As Nullable(Of DateTime)
    Private _strNroChequeAseguradora As String
    Private _strCodEstadoBien As String
    Private _strNroPoliza As String
    Private _strCodTipoPoliza As String
    Private _strCodBancoEmiteCheque As String
    Private _strCodAplicaFondo As String
    Private _strNroCuenta As String
    Private _strCodTipoCuenta As String
    Private _strCodMonedaCuenta As String
    Private _dtmFecConocimientoBanco As Nullable(Of DateTime)
    Private _strCodTipoSiniestro As String

    Private _intCodDemanda As Integer
    Private _strEstadoLogico As String

    Private _strFecSiniestroStr As String
    Private _strFecSituacionStr As String
    Private _strFecAplicacionStr As String
    Private _strFecConocimientoStr As String
    Private _strFecRecIndemnizacionStr As String
    Private _strFecDescargoMunicipalStr As String
    Private _strFecTransferenciaStr As String
    Private _strFecConocimientoBancoStr As String

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

    <XmlElement("NroSiniestro")> _
    Public Property NroSiniestro() As String
        Get
            Return Me._strNroSiniestro
        End Get
        Set(ByVal value As String)
            Me._strNroSiniestro = value
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
    Public Property Secfinanciamiento() As Nullable(Of Integer)
        Get
            Return Me._intSecfinanciamiento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intSecfinanciamiento = value
        End Set
    End Property

    <XmlElement("Secsiniestro")> _
    Public Property Secsiniestro() As Nullable(Of Integer)
        Get
            Return Me._intSecsiniestro
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intSecsiniestro = value
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

    <XmlElement("Montoindemnizacion")> _
    Public Property Montoindemnizacion() As Nullable(Of Decimal)
        Get
            Return Me._decMontoindemnizacion
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoindemnizacion = value
        End Set
    End Property

    <XmlElement("Moneda")> _
    Public Property Moneda() As String
        Get
            Return Me._strMoneda
        End Get
        Set(ByVal value As String)
            Me._strMoneda = value
        End Set
    End Property

    <XmlElement("Fecsiniestro")> _
    Public Property Fecsiniestro() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecsiniestro
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecsiniestro = value
        End Set
    End Property

    <XmlElement("Tipo")> _
    Public Property Tipo() As String
        Get
            Return Me._strTipo
        End Get
        Set(ByVal value As String)
            Me._strTipo = value
        End Set
    End Property

    <XmlElement("Situacion")> _
    Public Property Situacion() As String
        Get
            Return Me._strSituacion
        End Get
        Set(ByVal value As String)
            Me._strSituacion = value
        End Set
    End Property

    <XmlElement("Aplicacion")> _
    Public Property Aplicacion() As String
        Get
            Return Me._strAplicacion
        End Get
        Set(ByVal value As String)
            Me._strAplicacion = value
        End Set
    End Property

    <XmlElement("Transferencia")> _
    Public Property Transferencia() As String
        Get
            Return Me._strTransferencia
        End Get
        Set(ByVal value As String)
            Me._strTransferencia = value
        End Set
    End Property

    <XmlElement("Contrato")> _
    Public Property Contrato() As String
        Get
            Return Me._strContrato
        End Get
        Set(ByVal value As String)
            Me._strContrato = value
        End Set
    End Property

    <XmlElement("Seguro")> _
    Public Property Seguro() As String
        Get
            Return Me._strSeguro
        End Get
        Set(ByVal value As String)
            Me._strSeguro = value
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

    <XmlElement("Origen")> _
    Public Property Origen() As String
        Get
            Return Me._strOrigen
        End Get
        Set(ByVal value As String)
            Me._strOrigen = value
        End Set
    End Property

    <XmlElement("Fecsituacion")> _
    Public Property Fecsituacion() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecsituacion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecsituacion = value
        End Set
    End Property

    <XmlElement("Fecaplicacion")> _
    Public Property Fecaplicacion() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecaplicacion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecaplicacion = value
        End Set
    End Property

    <XmlElement("Fecconocimiento")> _
    Public Property Fecconocimiento() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecconocimiento
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecconocimiento = value
        End Set
    End Property

    <XmlElement("Fecrecindemnizacion")> _
    Public Property Fecrecindemnizacion() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecrecindemnizacion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecrecindemnizacion = value
        End Set
    End Property



    <XmlElement("FecDescargoMunicipal")> _
    Public Property FecDescargoMunicipal() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecDescargoMunicipal
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecDescargoMunicipal = value
        End Set
    End Property

    <XmlElement("FecTransferencia")> _
    Public Property FecTransferencia() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecTransferencia
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecTransferencia = value
        End Set
    End Property

    <XmlElement("FecConocimientoBanco")> _
    Public Property FecConocimientoBanco() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecConocimientoBanco
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecConocimientoBanco = value
        End Set
    End Property






    <XmlElement("NroChequeAseguradora")> _
    Public Property NroChequeAseguradora() As String
        Get
            Return Me._strNroChequeAseguradora
        End Get
        Set(ByVal value As String)
            Me._strNroChequeAseguradora = value
        End Set
    End Property

    <XmlElement("CodEstadoBien")> _
    Public Property CodEstadoBien() As String
        Get
            Return Me._strCodEstadoBien
        End Get
        Set(ByVal value As String)
            Me._strCodEstadoBien = value
        End Set
    End Property

    <XmlElement("NroPoliza")> _
    Public Property NroPoliza() As String
        Get
            Return Me._strNroPoliza
        End Get
        Set(ByVal value As String)
            Me._strNroPoliza = value
        End Set
    End Property

    <XmlElement("CodTipoPoliza")> _
    Public Property CodTipoPoliza() As String
        Get
            Return Me._strCodTipoPoliza
        End Get
        Set(ByVal value As String)
            Me._strCodTipoPoliza = value
        End Set
    End Property

    <XmlElement("CodBancoEmiteCheque")> _
    Public Property CodBancoEmiteCheque() As String
        Get
            Return Me._strCodBancoEmiteCheque
        End Get
        Set(ByVal value As String)
            Me._strCodBancoEmiteCheque = value
        End Set
    End Property

    <XmlElement("CodAplicaFondo")> _
    Public Property CodAplicaFondo() As String
        Get
            Return Me._strCodAplicaFondo
        End Get
        Set(ByVal value As String)
            Me._strCodAplicaFondo = value
        End Set
    End Property

    <XmlElement("NroCuenta")> _
    Public Property NroCuenta() As String
        Get
            Return Me._strNroCuenta
        End Get
        Set(ByVal value As String)
            Me._strNroCuenta = value
        End Set
    End Property

    <XmlElement("CodTipoCuenta")> _
    Public Property CodTipoCuenta() As String
        Get
            Return Me._strCodTipoCuenta
        End Get
        Set(ByVal value As String)
            Me._strCodTipoCuenta = value
        End Set
    End Property

    <XmlElement("CodMonedaCuenta")> _
    Public Property CodMonedaCuenta() As String
        Get
            Return Me._strCodMonedaCuenta
        End Get
        Set(ByVal value As String)
            Me._strCodMonedaCuenta = value
        End Set
    End Property

    <XmlElement("CodTipoSiniestro")> _
    Public Property CodTipoSiniestro() As String
        Get
            Return Me._strCodTipoSiniestro
        End Get
        Set(ByVal value As String)
            Me._strCodTipoSiniestro = value
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





    <XmlElement("FecSiniestroStr")> _
    Public Property FecSiniestroStr() As String
        Get
            Return Me._strFecSiniestroStr
        End Get
        Set(ByVal value As String)
            Me._strFecSiniestroStr = value
        End Set
    End Property

    <XmlElement("FecSituacionStr")> _
    Public Property FecSituacionStr() As String
        Get
            Return Me._strFecSituacionStr
        End Get
        Set(ByVal value As String)
            Me._strFecSituacionStr = value
        End Set
    End Property

    <XmlElement("FecAplicacionStr")> _
    Public Property FecAplicacionStr() As String
        Get
            Return Me._strFecAplicacionStr
        End Get
        Set(ByVal value As String)
            Me._strFecAplicacionStr = value
        End Set
    End Property

    <XmlElement("FecConocimientoStr")> _
    Public Property FecConocimientoStr() As String
        Get
            Return Me._strFecConocimientoStr
        End Get
        Set(ByVal value As String)
            Me._strFecConocimientoStr = value
        End Set
    End Property

    <XmlElement("FecRecIndemnizacionStr")> _
    Public Property FecRecIndemnizacionStr() As String
        Get
            Return Me._strFecRecIndemnizacionStr
        End Get
        Set(ByVal value As String)
            Me._strFecRecIndemnizacionStr = value
        End Set
    End Property

    <XmlElement("FecDescargoMunicipalStr")> _
    Public Property FecDescargoMunicipalStr() As String
        Get
            Return Me._strFecDescargoMunicipalStr
        End Get
        Set(ByVal value As String)
            Me._strFecDescargoMunicipalStr = value
        End Set
    End Property

    <XmlElement("FecTransferenciaStr")> _
    Public Property FecTransferenciaStr() As String
        Get
            Return Me._strFecTransferenciaStr
        End Get
        Set(ByVal value As String)
            Me._strFecTransferenciaStr = value
        End Set
    End Property

    <XmlElement("FecConocimientoBancoStr")> _
    Public Property FecConocimientoBancoStr() As String
        Get
            Return Me._strFecConocimientoBancoStr
        End Get
        Set(ByVal value As String)
            Me._strFecConocimientoBancoStr = value
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


#End Region

End Class