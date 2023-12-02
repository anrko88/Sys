Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_contratoproveedor
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_contratoproveedor")> _
Public Class EGcc_contratoproveedor

#Region " Atributos "

    Private _intCodigocontratoproveedor As Nullable(Of Integer)
    Private _strNumerocontrato As String
    Private _strCodigomoneda As String
    Private _decImporte As Nullable(Of Decimal)
    Private _strCodproveedor As String
    Private _intCodigocontacto As Nullable(Of Integer)
    Private _strTipocambio As String
    Private _decMontoTipoCambio As Nullable(Of Decimal)
    Private _decTotalImporte As Nullable(Of Decimal)
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String

    Private _strNombreContacto As String
    Private _strCorreo As String
    Private _strTipoNacionalidad As String
    Private _strCodigoTipoProveedor As String
    Private _strDescripcionBien As String

    Private _intFlagEnvioCarta As Nullable(Of Integer)



#End Region

#Region " Propiedades "

    <XmlElement("FlagEnvioCarta")> _
    Public Property FlagEnvioCarta() As Nullable(Of Integer)
        Get
            Return Me._intFlagEnvioCarta
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intFlagEnvioCarta = value
        End Set
    End Property

    <XmlElement("Codigocontratoproveedor")> _
    Public Property Codigocontratoproveedor() As Nullable(Of Integer)
        Get
            Return Me._intCodigocontratoproveedor
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodigocontratoproveedor = value
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

    <XmlElement("Codigomoneda")> _
    Public Property Codigomoneda() As String
        Get
            Return Me._strCodigomoneda
        End Get
        Set(ByVal value As String)
            Me._strCodigomoneda = value
        End Set
    End Property

    <XmlElement("Importe")> _
    Public Property Importe() As Nullable(Of Decimal)
        Get
            Return Me._decImporte
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImporte = value
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

    <XmlElement("Codigocontacto")> _
    Public Property Codigocontacto() As Nullable(Of Integer)
        Get
            Return Me._intCodigocontacto
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodigocontacto = value
        End Set
    End Property

    <XmlElement("Tipocambio")> _
    Public Property Tipocambio() As String
        Get
            Return Me._strTipocambio
        End Get
        Set(ByVal value As String)
            Me._strTipocambio = value
        End Set
    End Property

    <XmlElement("MontoTipoCambio")> _
    Public Property MontoTipoCambio() As Nullable(Of Decimal)
        Get
            Return Me._decMontoTipoCambio
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoTipoCambio = value
        End Set
    End Property

    <XmlElement("TotalImporte")> _
     Public Property TotalImporte() As Nullable(Of Decimal)
        Get
            Return Me._decTotalImporte
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decTotalImporte = value
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

    <XmlElement("NombreContacto")> _
    Public Property NombreContacto() As String
        Get
            Return Me._strNombreContacto
        End Get
        Set(ByVal value As String)
            Me._strNombreContacto = value
        End Set
    End Property

    <XmlElement("Correo")> _
    Public Property Correo() As String
        Get
            Return Me._strCorreo
        End Get
        Set(ByVal value As String)
            Me._strCorreo = value
        End Set
    End Property


    <XmlElement("DescripcionBien")> _
    Public Property DescripcionBien() As String
        Get
            Return Me._strDescripcionBien
        End Get
        Set(ByVal value As String)
            Me._strDescripcionBien = value
        End Set
    End Property

    <XmlElement("TipoNacionalidad")> _
    Public Property TipoNacionalidad() As String
        Get
            Return Me._strTipoNacionalidad
        End Get
        Set(ByVal value As String)
            Me._strTipoNacionalidad = value
        End Set
    End Property

    <XmlElement("CodigoTipoProveedor")> _
    Public Property CodigoTipoProveedor() As String
        Get
            Return Me._strCodigoTipoProveedor
        End Get
        Set(ByVal value As String)
            Me._strCodigoTipoProveedor = value
        End Set
    End Property

#End Region

End Class

<Serializable(), XmlRoot("ListEContratoProveedor")> _
Public Class ListEContratoProveedor
    Inherits List(Of EGcc_contratoproveedor)
End Class