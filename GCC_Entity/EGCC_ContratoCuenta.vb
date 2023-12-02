Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_contratocuenta
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_contratocuenta")> _
Public Class EGcc_contratocuenta

#Region " Atributos "

    Private _intSecuencia As Nullable(Of Integer)
    Private _strCodsolicitudcredito As String
    Private _strCuenta As String
    Private _strCodigotipocuenta As String
    Private _strCodigomoneda As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String

#End Region

#Region " Propiedades "

    <XmlElement("Secuencia")> _
    Public Property Secuencia() As Integer
        Get
            Return Me._intSecuencia
        End Get
        Set(ByVal value As Integer)
            Me._intSecuencia = value
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

    <XmlElement("Cuenta")> _
    Public Property Cuenta() As String
        Get
            Return Me._strCuenta
        End Get
        Set(ByVal value As String)
            Me._strCuenta = value
        End Set
    End Property

    <XmlElement("Codigotipocuenta")> _
    Public Property Codigotipocuenta() As String
        Get
            Return Me._strCodigotipocuenta
        End Get
        Set(ByVal value As String)
            Me._strCodigotipocuenta = value
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

    <XmlElement("Audestadologico")> _
    Public Property Audestadologico() As Integer
        Get
            Return Me._intAudestadologico
        End Get
        Set(ByVal value As Integer)
            Me._intAudestadologico = value
        End Set
    End Property

    <XmlElement("Audfecharegistro")> _
    Public Property Audfecharegistro() As DateTime
        Get
            Return Me._dtmAudfecharegistro
        End Get
        Set(ByVal value As DateTime)
            Me._dtmAudfecharegistro = value
        End Set
    End Property

    <XmlElement("Audfechamodificacion")> _
    Public Property Audfechamodificacion() As DateTime
        Get
            Return Me._dtmAudfechamodificacion
        End Get
        Set(ByVal value As DateTime)
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


#End Region

End Class

''' <summary>
''' Clase que hereda de List(Of EGcc_contratonotarial) para la implementación de la lista
''' </summary>
''' <remarks></remarks>
<Serializable(), XmlRoot("ListEGcc_contratoCuenta")> _
Public Class ListEGcc_contratoCuenta
    Inherits List(Of EGcc_contratocuenta)
End Class