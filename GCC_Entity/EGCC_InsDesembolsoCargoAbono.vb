Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad EGCC_InsDesembolsoCargoAbono
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 25/09/2012
''' </remarks>
<Serializable(), XmlRoot("EGCC_InsDesembolsoCargoAbono")> _
Public Class EGCC_InsDesembolsoCargoAbono

#Region " Atributos "

    Private _strCodproveedor As String
    Private _strCodmonedapago As String
    Private _strCodconceptopago As String
    Private _strCodtipooperacion As String
    Private _decImporteagrupacion As Nullable(Of Decimal)
    Private _decImporteadelanto As Nullable(Of Decimal)
    Private _decImportepago As Nullable(Of Decimal)
    Private _strFlagadelanto As String
    Private _intCoddesembolsodet As Nullable(Of Integer)
    Private _strCodinstrucciondesembolso As String
    Private _intCodcorrelativo As Nullable(Of Integer)
    Private _strCodsolicitudcredito As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String
    Private _strCodagrupaciondocumento As String

    Private _decPorcCalculo As Nullable(Of Decimal)


#End Region

#Region " Propiedades "

    <XmlElement("Codproveedor")> _
    Public Property Codproveedor() As String
        Get
            Return Me._strCodproveedor
        End Get
        Set(ByVal value As String)
            Me._strCodproveedor = value
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

    <XmlElement("Codconceptopago")> _
    Public Property Codconceptopago() As String
        Get
            Return Me._strCodconceptopago
        End Get
        Set(ByVal value As String)
            Me._strCodconceptopago = value
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

    <XmlElement("Importeagrupacion")> _
    Public Property Importeagrupacion() As Nullable(Of Decimal)
        Get
            Return Me._decImporteagrupacion
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImporteagrupacion = value
        End Set
    End Property

    <XmlElement("Importeadelanto")> _
    Public Property Importeadelanto() As Nullable(Of Decimal)
        Get
            Return Me._decImporteadelanto
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImporteadelanto = value
        End Set
    End Property

    <XmlElement("Importepago")> _
    Public Property Importepago() As Nullable(Of Decimal)
        Get
            Return Me._decImportepago
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImportepago = value
        End Set
    End Property

    <XmlElement("Flagadelanto")> _
    Public Property Flagadelanto() As String
        Get
            Return Me._strFlagadelanto
        End Get
        Set(ByVal value As String)
            Me._strFlagadelanto = value
        End Set
    End Property

    <XmlElement("Coddesembolsodet")> _
    Public Property Coddesembolsodet() As Nullable(Of Integer)
        Get
            Return Me._intCoddesembolsodet
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCoddesembolsodet = value
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

    <XmlElement("PorcCalculo")> _
    Public Property PorcCalculo() As Nullable(Of Decimal)
        Get
            Return Me._decPorcCalculo
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPorcCalculo = value
        End Set
    End Property

#End Region

End Class
