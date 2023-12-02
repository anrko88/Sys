Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Solicitudcreditoestructuratasacion
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("ESolicitudcreditoestructuratasacion")> _
Public Class ESolicitudcreditoestructuratasacion

#Region " Atributos "

    Private _strCodtasacion As String
    Private _strCodsolicitudcredito As String
    Private _intSecfinanciamiento As Nullable(Of Integer)
    Private _strCodtasador As String
    Private _dtmFechatasacion As Nullable(Of DateTime)
    Private _dtmFechaencargo As Nullable(Of DateTime)
    Private _decValorejecucion As Nullable(Of Decimal)
    Private _decValorcomercial As Nullable(Of Decimal)
    Private _strCodmoneda As String
    Private _strObservaciones As String
    Private _dtmFecharegistro As Nullable(Of DateTime)
    Private _strUsuarioregistro As String
    Private _strCodmotivonotasacion As String
    Private _dtmFechapago As Nullable(Of DateTime)
    Private _dtmFechacobro As Nullable(Of DateTime)
    Private _decImporte As Nullable(Of Decimal)
    Private _decComision As Nullable(Of Decimal)
    Private _decTipodecambio As Nullable(Of Decimal)
    Private _strNrocheque As String
    Private _strNropartidaregistral As String
    Private _intAno As Nullable(Of Integer)
    Private _strCodigoconcepto As String
    Private _strNroinforme As String
    Private _strfechaProxTasacion As Nullable(Of DateTime)



    Private _strFlagEnvioCarta As Nullable(Of Integer)
    Private _strCodEstadoTasacion As String
    Private _strVfechaProxTasacion As String

    Private _strVFechatasacion As String
    Private _strVFechaencargo As String



#End Region

#Region " Propiedades "

    <XmlElement("Codtasacion")> _
    Public Property Codtasacion() As String
        Get
            Return Me._strCodtasacion
        End Get
        Set(ByVal value As String)
            Me._strCodtasacion = value
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

    <XmlElement("Secfinanciamiento")> _
    Public Property Secfinanciamiento() As Integer
        Get
            Return Me._intSecfinanciamiento
        End Get
        Set(ByVal value As Integer)
            Me._intSecfinanciamiento = value
        End Set
    End Property

    <XmlElement("Codtasador")> _
    Public Property Codtasador() As String
        Get
            Return Me._strCodtasador
        End Get
        Set(ByVal value As String)
            Me._strCodtasador = value
        End Set
    End Property

    <XmlElement("Fechatasacion")> _
    Public Property Fechatasacion() As DateTime
        Get
            Return Me._dtmFechatasacion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechatasacion = value
        End Set
    End Property

    <XmlElement("Fechaencargo")> _
    Public Property Fechaencargo() As DateTime
        Get
            Return Me._dtmFechaencargo
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechaencargo = value
        End Set
    End Property

    <XmlElement("Valorejecucion")> _
    Public Property Valorejecucion() As Decimal
        Get
            Return Me._decValorejecucion
        End Get
        Set(ByVal value As Decimal)
            Me._decValorejecucion = value
        End Set
    End Property

    <XmlElement("Valorcomercial")> _
    Public Property Valorcomercial() As Decimal
        Get
            Return Me._decValorcomercial
        End Get
        Set(ByVal value As Decimal)
            Me._decValorcomercial = value
        End Set
    End Property

    <XmlElement("Codmoneda")> _
    Public Property Codmoneda() As String
        Get
            Return Me._strCodmoneda
        End Get
        Set(ByVal value As String)
            Me._strCodmoneda = value
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

    <XmlElement("Fecharegistro")> _
    Public Property Fecharegistro() As DateTime
        Get
            Return Me._dtmFecharegistro
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFecharegistro = value
        End Set
    End Property

    <XmlElement("Usuarioregistro")> _
    Public Property Usuarioregistro() As String
        Get
            Return Me._strUsuarioregistro
        End Get
        Set(ByVal value As String)
            Me._strUsuarioregistro = value
        End Set
    End Property

    <XmlElement("Codmotivonotasacion")> _
    Public Property Codmotivonotasacion() As String
        Get
            Return Me._strCodmotivonotasacion
        End Get
        Set(ByVal value As String)
            Me._strCodmotivonotasacion = value
        End Set
    End Property

    <XmlElement("Fechapago")> _
    Public Property Fechapago() As DateTime
        Get
            Return Me._dtmFechapago
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechapago = value
        End Set
    End Property

    <XmlElement("Fechacobro")> _
    Public Property Fechacobro() As DateTime
        Get
            Return Me._dtmFechacobro
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechacobro = value
        End Set
    End Property

    <XmlElement("Importe")> _
    Public Property Importe() As Decimal
        Get
            Return Me._decImporte
        End Get
        Set(ByVal value As Decimal)
            Me._decImporte = value
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

    <XmlElement("Nropartidaregistral")> _
    Public Property Nropartidaregistral() As String
        Get
            Return Me._strNropartidaregistral
        End Get
        Set(ByVal value As String)
            Me._strNropartidaregistral = value
        End Set
    End Property

    <XmlElement("Ano")> _
    Public Property Ano() As Integer
        Get
            Return Me._intAno
        End Get
        Set(ByVal value As Integer)
            Me._intAno = value
        End Set
    End Property

    <XmlElement("Codigoconcepto")> _
    Public Property Codigoconcepto() As String
        Get
            Return Me._strCodigoconcepto
        End Get
        Set(ByVal value As String)
            Me._strCodigoconcepto = value
        End Set
    End Property

    <XmlElement("Nroinforme")> _
    Public Property Nroinforme() As String
        Get
            Return Me._strNroinforme
        End Get
        Set(ByVal value As String)
            Me._strNroinforme = value
        End Set
    End Property


    <XmlElement("fechaProxTasacion")> _
    Public Property fechaProxTasacion() As DateTime
        Get
            Return Me._strfechaProxTasacion
        End Get
        Set(ByVal value As DateTime)
            Me._strfechaProxTasacion = value
        End Set
    End Property

    <XmlElement("FlagEnvioCarta")> _
       Public Property FlagEnvioCarta() As Integer
        Get
            Return Me._strFlagEnvioCarta
        End Get
        Set(ByVal value As Integer)
            Me._strFlagEnvioCarta = value
        End Set
    End Property

    <XmlElement("CodEstadoTasacion")> _
   Public Property CodEstadoTasacion() As String
        Get
            Return Me._strCodEstadoTasacion
        End Get
        Set(ByVal value As String)
            Me._strCodEstadoTasacion = value
        End Set
    End Property


    <XmlElement("VfechaProxTasacion")> _
        Public Property VfechaProxTasacion() As String
        Get
            Return Me._strVfechaProxTasacion
        End Get
        Set(ByVal value As String)
            Me._strVfechaProxTasacion = value
        End Set
    End Property
 

    <XmlElement("VFechatasacion")> _
    Public Property VFechatasacion() As String
        Get
            Return Me._strVFechatasacion
        End Get
        Set(ByVal value As String)
            Me._strVFechatasacion = value
        End Set
    End Property
    <XmlElement("VFechaencargo")> _
   Public Property VFechaencargo() As String
        Get
            Return Me._strVFechaencargo
        End Get
        Set(ByVal value As String)
            Me._strVFechaencargo = value
        End Set
    End Property


#End Region

End Class