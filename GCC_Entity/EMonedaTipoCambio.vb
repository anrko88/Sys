Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Monedatipocambio
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EMonedatipocambio")> _
Public Class EMonedatipocambio

#Region " Atributos "

    Private _strCodmoneda As String
    Private _strTipomodalidadcambio As String = String.Empty
    Private _dtmFechainiciovigencia As String = String.Empty
    Private _dtmFechafinalvigencia As String = String.Empty
    Private _decMontovalorcompra As Nullable(Of Decimal)
    Private _decMontovalorventa As Nullable(Of Decimal)
    Private _dtmFechacarga As Nullable(Of DateTime)
    Private _strCodmodulooperacion As String = String.Empty
    Private _strTipooperacion As String = String.Empty
    Private _strCodusuario As String = String.Empty
    Private _strTextoaudicreacion As String = String.Empty
    Private _strTextoaudimodi As String = String.Empty
    Private _strError As String = String.Empty


#End Region

#Region " Propiedades "

    <XmlElement("Codmoneda")> _
    Public Property Codmoneda() As String
        Get
            Return Me._strCodmoneda
        End Get
        Set(ByVal value As String)
            Me._strCodmoneda = value
        End Set
    End Property

    <XmlElement("Tipomodalidadcambio")> _
    Public Property Tipomodalidadcambio() As String
        Get
            Return Me._strTipomodalidadcambio
        End Get
        Set(ByVal value As String)
            Me._strTipomodalidadcambio = value
        End Set
    End Property

    <XmlElement("Fechainiciovigencia")> _
    Public Property Fechainiciovigencia() As String
        Get
            Return Me._dtmFechainiciovigencia
        End Get
        Set(ByVal value As String)
            Me._dtmFechainiciovigencia = value
        End Set
    End Property

    <XmlElement("Fechafinalvigencia")> _
    Public Property Fechafinalvigencia() As String
        Get
            Return Me._dtmFechafinalvigencia
        End Get
        Set(ByVal value As String)
            Me._dtmFechafinalvigencia = value
        End Set
    End Property

    <XmlElement("Montovalorcompra")> _
    Public Property Montovalorcompra() As Nullable(Of Decimal)
        Get
            Return Me._decMontovalorcompra
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontovalorcompra = value
        End Set
    End Property

    <XmlElement("Montovalorventa")> _
    Public Property Montovalorventa() As Nullable(Of Decimal)
        Get
            Return Me._decMontovalorventa
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontovalorventa = value
        End Set
    End Property

    <XmlElement("Fechacarga")> _
    Public Property Fechacarga() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechacarga
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechacarga = value
        End Set
    End Property

    <XmlElement("Codmodulooperacion")> _
    Public Property Codmodulooperacion() As String
        Get
            Return Me._strCodmodulooperacion
        End Get
        Set(ByVal value As String)
            Me._strCodmodulooperacion = value
        End Set
    End Property

    <XmlElement("Tipooperacion")> _
    Public Property Tipooperacion() As String
        Get
            Return Me._strTipooperacion
        End Get
        Set(ByVal value As String)
            Me._strTipooperacion = value
        End Set
    End Property

    <XmlElement("Codusuario")> _
    Public Property Codusuario() As String
        Get
            Return Me._strCodusuario
        End Get
        Set(ByVal value As String)
            Me._strCodusuario = value
        End Set
    End Property

    <XmlElement("Textoaudicreacion")> _
    Public Property Textoaudicreacion() As String
        Get
            Return Me._strTextoaudicreacion
        End Get
        Set(ByVal value As String)
            Me._strTextoaudicreacion = value
        End Set
    End Property

    <XmlElement("Textoaudimodi")> _
    Public Property Textoaudimodi() As String
        Get
            Return Me._strTextoaudimodi
        End Get
        Set(ByVal value As String)
            Me._strTextoaudimodi = value
        End Set
    End Property
    <XmlElement("CodError")> _
   Public Property CodError() As String
        Get
            Return Me._strError
        End Get
        Set(ByVal value As String)
            Me._strError = value
        End Set
    End Property


#End Region

End Class
