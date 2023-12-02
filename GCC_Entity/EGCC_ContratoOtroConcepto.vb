Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization


''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_contratootroconcepto
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_contratootroconcepto")> _
Public Class EGcc_contratootroconcepto


#Region " Atributos "

    Private _strNumerocontrato As String
    Private _decMontoTEAPorc As Nullable(Of Decimal)
    Private _decMontoPreCuotaPorc As Nullable(Of Decimal)
    Private _strOpcionCompra As String
    Private _decComisionActivacion As Nullable(Of Decimal)
    Private _decComisionEstructuracion As Nullable(Of Decimal)
    Private _strOtrasComisiones As String

    Private _decImportependiente As Nullable(Of Decimal)
    Private _intDiasvencimiento As Nullable(Of Integer)
    Private _decPorcentajecuota As Nullable(Of Decimal)
    Private _strOtraspenalidades As String
    Private _strNombrearchivo As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String
    'Inicio IBK - AAE
    Private _decImporteOpcionCompra As Nullable(Of Decimal)
    'Fin IBK - AAE

#End Region

#Region " Propiedades "

    <XmlElement("Numerocontrato")> _
    Public Property Numerocontrato() As String
        Get
            Return Me._strNumerocontrato
        End Get
        Set(ByVal value As String)
            Me._strNumerocontrato = value
        End Set
    End Property

    <XmlElement("MontoTEAPorc")> _
    Public Property MontoTEAPorc() As Nullable(Of Decimal)
        Get
            Return Me._decMontoTEAPorc
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoTEAPorc = value
        End Set
    End Property

    <XmlElement("MontoPreCuotaPorc")> _
    Public Property MontoPreCuotaPorc() As Nullable(Of Decimal)
        Get
            Return Me._decMontoPreCuotaPorc
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoPreCuotaPorc = value
        End Set
    End Property

    <XmlElement("OpcionCompra")> _
    Public Property OpcionCompra() As String
        Get
            Return Me._strOpcionCompra
        End Get
        Set(ByVal value As String)
            Me._strOpcionCompra = value
        End Set
    End Property

    <XmlElement("ComisionActivacion")> _
    Public Property ComisionActivacion() As Nullable(Of Decimal)
        Get
            Return Me._decComisionActivacion
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decComisionActivacion = value
        End Set
    End Property

    <XmlElement("ComisionEstructuracion")> _
    Public Property ComisionEstructuracion() As Nullable(Of Decimal)
        Get
            Return Me._decComisionEstructuracion
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decComisionEstructuracion = value
        End Set
    End Property

    <XmlElement("OtrasComisiones")> _
    Public Property OtrasComisiones() As String
        Get
            Return Me._strOtrasComisiones
        End Get
        Set(ByVal value As String)
            Me._strOtrasComisiones = value
        End Set
    End Property

    <XmlElement("Importependiente")> _
    Public Property Importependiente() As Nullable(Of Decimal)
        Get
            Return Me._decImportependiente
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImportependiente = value
        End Set
    End Property

    <XmlElement("Diasvencimiento")> _
    Public Property Diasvencimiento() As Nullable(Of Integer)
        Get
            Return Me._intDiasvencimiento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intDiasvencimiento = value
        End Set
    End Property

    <XmlElement("Porcentajecuota")> _
    Public Property Porcentajecuota() As Nullable(Of Decimal)
        Get
            Return Me._decPorcentajecuota
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPorcentajecuota = value
        End Set
    End Property

    <XmlElement("Otraspenalidades")> _
    Public Property Otraspenalidades() As String
        Get
            Return Me._strOtraspenalidades
        End Get
        Set(ByVal value As String)
            Me._strOtraspenalidades = value
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

    'Inicio IBK - AAE
    <XmlElement("ImporteOpcionCompra")> _
    Public Property ImporteOpcionCompra() As Nullable(Of Decimal)
        Get
            Return Me._decImporteOpcionCompra
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImporteOpcionCompra = value
        End Set
    End Property
    'Fin IBK
#End Region

End Class