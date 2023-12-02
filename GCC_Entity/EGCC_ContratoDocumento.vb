Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_contratodocumento
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_contratodocumento")> _
Public Class EGcc_contratodocumento

#Region " Atributos "

    Private _intCodigocontratodocumento As Nullable(Of Integer)
    Private _strNumerocontrato As String
    Private _intCodigodocumento As Nullable(Of Integer)
    Private _intAprobarcomercial As Nullable(Of Integer)
    Private _intAprobarlegal As Nullable(Of Integer)
    Private _strNombrearchivo As String
    Private _strAdjunto As String
    Private _decIncotermmonto As Nullable(Of Decimal)
    Private _strCodigotipoincoterm As String
    Private _strCodigoestadochecklist As String
    Private _intFlagcartaenvio As Nullable(Of Integer)
    Private _strObservaciones As String
    Private _strCodigoorigencondicion As String
    Private _strCodigotipocondicion As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String
    Private _strTextoPredefinido As String

    Private _intFlagFiltro As Nullable(Of Integer)
    Private _intExiste As Nullable(Of Integer)

    Private _intSecFinanciamiento As Nullable(Of Integer)
    Private _intEstadoDocumento As Nullable(Of Integer)
    Private _intEstadoDocContrato As Nullable(Of Integer)
    Private _intEstadoDocBien As Nullable(Of Integer)

#End Region

#Region " Propiedades "

    <XmlElement("EstadoDocContrato")> _
Public Property EstadoDocContrato() As Nullable(Of Integer)
        Get
            Return Me._intEstadoDocContrato
        End Get
        Set(ByVal Value As Nullable(Of Integer))
            Me._intEstadoDocContrato = Value
        End Set
    End Property
    <XmlElement("EstadoDocBien")> _
Public Property EstadoDocBien() As Nullable(Of Integer)
        Get
            Return Me._intEstadoDocBien
        End Get
        Set(ByVal Value As Nullable(Of Integer))
            Me._intEstadoDocBien = Value
        End Set
    End Property

    <XmlElement("EstadoDocumento")> _
 Public Property EstadoDocumento() As Nullable(Of Integer)
        Get
            Return Me._intEstadoDocumento
        End Get
        Set(ByVal Value As Nullable(Of Integer))
            Me._intEstadoDocumento = Value
        End Set
    End Property

    <XmlElement("SecFinanciamiento")> _
  Public Property SecFinanciamiento() As Nullable(Of Integer)
        Get
            Return Me._intSecFinanciamiento
        End Get
        Set(ByVal Value As Nullable(Of Integer))
            Me._intSecFinanciamiento = Value
        End Set
    End Property


    <XmlElement("Existe")> _
   Public Property Existe() As Nullable(Of Integer)
        Get
            Return Me._intExiste
        End Get
        Set(ByVal Value As Nullable(Of Integer))
            Me._intExiste = Value
        End Set
    End Property

    <XmlElement("Codigocontratodocumento")> _
    Public Property Codigocontratodocumento() As Nullable(Of Integer)
        Get
            Return Me._intCodigocontratodocumento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodigocontratodocumento = value
        End Set
    End Property

    <XmlElement("Numerocontrato")> _
    Public Property Numerocontrato() As String
        Get
            Return Me._strNumerocontrato
        End Get
        Set(ByVal value As String)
            Me._strNumerocontrato = value
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

    <XmlElement("Aprobarcomercial")> _
    Public Property Aprobarcomercial() As Nullable(Of Integer)
        Get
            Return Me._intAprobarcomercial
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intAprobarcomercial = value
        End Set
    End Property

    <XmlElement("Aprobarlegal")> _
    Public Property Aprobarlegal() As Nullable(Of Integer)
        Get
            Return Me._intAprobarlegal
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intAprobarlegal = value
        End Set
    End Property

    <XmlElement("Nombrearchivo")> _
    Public Property Nombrearchivo() As String
        Get
            Return Me._strNombrearchivo
        End Get
        Set(ByVal value As String)
            Me._strNombrearchivo = value
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

    <XmlElement("Incotermmonto")> _
    Public Property Incotermmonto() As Nullable(Of Decimal)
        Get
            Return Me._decIncotermmonto
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decIncotermmonto = value
        End Set
    End Property

    <XmlElement("Codigotipoincoterm")> _
    Public Property Codigotipoincoterm() As String
        Get
            Return Me._strCodigotipoincoterm
        End Get
        Set(ByVal value As String)
            Me._strCodigotipoincoterm = value
        End Set
    End Property

    <XmlElement("Codigoestadochecklist")> _
    Public Property Codigoestadochecklist() As String
        Get
            Return Me._strCodigoestadochecklist
        End Get
        Set(ByVal value As String)
            Me._strCodigoestadochecklist = value
        End Set
    End Property

    <XmlElement("Flagcartaenvio")> _
    Public Property Flagcartaenvio() As Nullable(Of Integer)
        Get
            Return Me._intFlagcartaenvio
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intFlagcartaenvio = value
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

    <XmlElement("Codigoorigencondicion")> _
    Public Property Codigoorigencondicion() As String
        Get
            Return Me._strCodigoorigencondicion
        End Get
        Set(ByVal value As String)
            Me._strCodigoorigencondicion = value
        End Set
    End Property

    <XmlElement("Codigotipocondicion")> _
    Public Property Codigotipocondicion() As String
        Get
            Return Me._strCodigotipocondicion
        End Get
        Set(ByVal value As String)
            Me._strCodigotipocondicion = value
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

    <XmlElement("TextoPredefinido")> _
    Public Property TextoPredefinido() As String
        Get
            Return Me._strTextoPredefinido
        End Get
        Set(ByVal value As String)
            Me._strTextoPredefinido = value
        End Set
    End Property

    Public Property Flagfiltro() As Nullable(Of Integer)
        Get
            Return _intFlagFiltro
        End Get
        Set(ByVal value As Nullable(Of Integer))
            _intFlagFiltro = value
        End Set
    End Property

#End Region

End Class