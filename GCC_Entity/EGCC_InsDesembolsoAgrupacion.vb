Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad EGCC_InsDesembolsoAgrupacion
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGCC_InsDesembolsoAgrupacion")> _
Public Class EGCC_InsDesembolsoAgrupacion

#Region " Atributos "

    Private _strCodagrupacion As String
    Private _strCodproveedor As String
    Private _strCodmonedadocumento As String
    Private _strCodmonedapago As String
    Private _decImporteagrupacion As Nullable(Of Decimal)
    Private _strCodtipooperacion As String
    Private _strCodtiporegistro As String
    Private _strNumerodocumento As String
    Private _strCodinstrucciondesembolso As String
    Private _intCodcorrelativo As Nullable(Of Integer)
    Private _strCodsolicitudcredito As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String
    Private _strCodagrupaciondocumento As String
    Private _decMontoretencion As Nullable(Of Decimal)
    Private _decMontodetraccion As Nullable(Of Decimal)
    Private _decMonto4ta As Nullable(Of Decimal)
    Private _decMontototalpago As Nullable(Of Decimal)

    Private _strCodConceptoCargo As String
    Private _decPorcCalculo As Nullable(Of Decimal)
    'Inicio IBK - AAE
    Private _decImporteIgv As Nullable(Of Decimal)
    'Fin IBK

#End Region

#Region " Propiedades "

    <XmlElement("Codagrupacion")> _
    Public Property Codagrupacion() As String
        Get
            Return Me._strCodagrupacion
        End Get
        Set(ByVal value As String)
            Me._strCodagrupacion = value
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

    <XmlElement("Codmonedadocumento")> _
    Public Property Codmonedadocumento() As String
        Get
            Return Me._strCodmonedadocumento
        End Get
        Set(ByVal value As String)
            Me._strCodmonedadocumento = value
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

    <XmlElement("Importeagrupacion")> _
    Public Property Importeagrupacion() As Nullable(Of Decimal)
        Get
            Return Me._decImporteagrupacion
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImporteagrupacion = value
        End Set
    End Property

    <XmlElement("Codtipooperacion")> _
    Public Property Codtipooperacion() As String
        Get
            Return Me._strCodtipooperacion
        End Get
        Set(ByVal value As String)
            Me._strCodtipooperacion = value
        End Set
    End Property

    <XmlElement("Codtiporegistro")> _
    Public Property Codtiporegistro() As String
        Get
            Return Me._strCodtiporegistro
        End Get
        Set(ByVal value As String)
            Me._strCodtiporegistro = value
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

    <XmlElement("Codinstrucciondesembolso")> _
    Public Property Codinstrucciondesembolso() As String
        Get
            Return Me._strCodinstrucciondesembolso
        End Get
        Set(ByVal value As String)
            Me._strCodinstrucciondesembolso = value
        End Set
    End Property

    <XmlElement("Codcorrelativo")> _
    Public Property Codcorrelativo() As Nullable(Of Integer)
        Get
            Return Me._intCodcorrelativo
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodcorrelativo = value
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

    <XmlElement("Codagrupaciondocumento")> _
    Public Property Codagrupaciondocumento() As String
        Get
            Return Me._strCodagrupaciondocumento
        End Get
        Set(ByVal value As String)
            Me._strCodagrupaciondocumento = value
        End Set
    End Property

    <XmlElement("Montoretencion")> _
    Public Property Montoretencion() As Nullable(Of Decimal)
        Get
            Return Me._decMontoretencion
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoretencion = value
        End Set
    End Property

    <XmlElement("Montodetraccion")> _
    Public Property Montodetraccion() As Nullable(Of Decimal)
        Get
            Return Me._decMontodetraccion
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontodetraccion = value
        End Set
    End Property

    <XmlElement("Monto4ta")> _
    Public Property Monto4ta() As Decimal
        Get
            Return Me._decMonto4ta
        End Get
        Set(ByVal value As Decimal)
            Me._decMonto4ta = value
        End Set
    End Property

    <XmlElement("Montototalpago")> _
    Public Property Montototalpago() As Nullable(Of Decimal)
        Get
            Return Me._decMontototalpago
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontototalpago = value
        End Set
    End Property

    <XmlElement("CodConceptoCargo")> _
    Public Property CodConceptoCargo() As String
        Get
            Return Me._strCodConceptoCargo
        End Get
        Set(ByVal value As String)
            Me._strCodConceptoCargo = value
        End Set
    End Property

    <XmlElement("PorcCalculo")> _
    Public Property PorcCalculo() As Nullable(Of Decimal)
        Get
            Return Me._decPorcCalculo
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPorcCalculo = value
        End Set
    End Property
    'Inicio IBK - AAE
    <XmlElement("ImporteIgv")> _
    Public Property ImporteIgv() As Nullable(Of Decimal)
        Get
            Return Me._decImporteIgv
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImporteIgv = value
        End Set
    End Property
    'Fin IBK
#End Region

End Class