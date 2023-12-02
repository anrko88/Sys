Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Infraccion
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EInfraccion")> _
Public Class EInfraccion

#Region " Atributos "

    Private _strCodsolcredito As String
    Private _intSecfinanciamiento As Nullable(Of Integer)
    Private _intSecinfraccion As Nullable(Of Integer)
    Private _decImporte As Nullable(Of Decimal)
    Private _strCodunico As String
    Private _strPlaca As String
    Private _strNuminfraccion As String
    Private _strTipoinfraccion As String
    Private _strMunicipalidad As String
    Private _strSituacion As String
    Private _dtmFecrecepcion As Nullable(Of DateTime)
    Private _dtmFecinfraccion As Nullable(Of DateTime)
    Private _dtmFecnotificacion As Nullable(Of DateTime)
    Private _strObservaciones As String
    Private _dtmFecregistro As Nullable(Of DateTime)
    Private _strUsuariomodificacion As String
    Private _dtmFecmodificacion As Nullable(Of DateTime)
    Private _dtmFeccancelacion As Nullable(Of DateTime)
    Private _decImportecondescuento As Nullable(Of Decimal)
    Private _decComision As Nullable(Of Decimal)
    Private _decComisionigv As Nullable(Of Decimal)
    Private _decTotalcobrado As Nullable(Of Decimal)
    Private _dtmFecharecuperacion As Nullable(Of DateTime)
    Private _dtmFechavalorrecuperacion As Nullable(Of DateTime)
    Private _decImporteoriginal As Nullable(Of Decimal)
    Private _strUsuarioregistro As String
    Private _strNrocheque As String
    Private _strCodigomoneda As String

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
    Public Property Secfinanciamiento() As Integer
        Get
            Return Me._intSecfinanciamiento
        End Get
        Set(ByVal value As Integer)
            Me._intSecfinanciamiento = value
        End Set
    End Property

    <XmlElement("Secinfraccion")> _
    Public Property Secinfraccion() As Integer
        Get
            Return Me._intSecinfraccion
        End Get
        Set(ByVal value As Integer)
            Me._intSecinfraccion = value
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

    <XmlElement("Codunico")> _
    Public Property Codunico() As String
        Get
            Return Me._strCodunico
        End Get
        Set(ByVal value As String)
            Me._strCodunico = value
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

    <XmlElement("Numinfraccion")> _
    Public Property Numinfraccion() As String
        Get
            Return Me._strNuminfraccion
        End Get
        Set(ByVal value As String)
            Me._strNuminfraccion = value
        End Set
    End Property

    <XmlElement("Tipoinfraccion")> _
    Public Property Tipoinfraccion() As String
        Get
            Return Me._strTipoinfraccion
        End Get
        Set(ByVal value As String)
            Me._strTipoinfraccion = value
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

    <XmlElement("Situacion")> _
    Public Property Situacion() As String
        Get
            Return Me._strSituacion
        End Get
        Set(ByVal value As String)
            Me._strSituacion = value
        End Set
    End Property

    <XmlElement("Fecrecepcion")> _
    Public Property Fecrecepcion() As DateTime
        Get
            Return Me._dtmFecrecepcion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFecrecepcion = value
        End Set
    End Property

    <XmlElement("Fecinfraccion")> _
    Public Property Fecinfraccion() As DateTime
        Get
            Return Me._dtmFecinfraccion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFecinfraccion = value
        End Set
    End Property

    <XmlElement("Fecnotificacion")> _
    Public Property Fecnotificacion() As DateTime
        Get
            Return Me._dtmFecnotificacion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFecnotificacion = value
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
    Public Property Fecregistro() As DateTime
        Get
            Return Me._dtmFecregistro
        End Get
        Set(ByVal value As DateTime)
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
    Public Property Fecmodificacion() As DateTime
        Get
            Return Me._dtmFecmodificacion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFecmodificacion = value
        End Set
    End Property

    <XmlElement("Feccancelacion")> _
    Public Property Feccancelacion() As DateTime
        Get
            Return Me._dtmFeccancelacion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFeccancelacion = value
        End Set
    End Property

    <XmlElement("Importecondescuento")> _
    Public Property Importecondescuento() As Decimal
        Get
            Return Me._decImportecondescuento
        End Get
        Set(ByVal value As Decimal)
            Me._decImportecondescuento = value
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

    <XmlElement("Comisionigv")> _
    Public Property Comisionigv() As Decimal
        Get
            Return Me._decComisionigv
        End Get
        Set(ByVal value As Decimal)
            Me._decComisionigv = value
        End Set
    End Property

    <XmlElement("Totalcobrado")> _
    Public Property Totalcobrado() As Decimal
        Get
            Return Me._decTotalcobrado
        End Get
        Set(ByVal value As Decimal)
            Me._decTotalcobrado = value
        End Set
    End Property

    <XmlElement("Fecharecuperacion")> _
    Public Property Fecharecuperacion() As DateTime
        Get
            Return Me._dtmFecharecuperacion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFecharecuperacion = value
        End Set
    End Property

    <XmlElement("Fechavalorrecuperacion")> _
    Public Property Fechavalorrecuperacion() As DateTime
        Get
            Return Me._dtmFechavalorrecuperacion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechavalorrecuperacion = value
        End Set
    End Property

    <XmlElement("Importeoriginal")> _
    Public Property Importeoriginal() As Decimal
        Get
            Return Me._decImporteoriginal
        End Get
        Set(ByVal value As Decimal)
            Me._decImporteoriginal = value
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

    <XmlElement("Nrocheque")> _
    Public Property Nrocheque() As String
        Get
            Return Me._strNrocheque
        End Get
        Set(ByVal value As String)
            Me._strNrocheque = value
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


#End Region

End Class
