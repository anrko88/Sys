Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Operacionactivacalendario
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EOperacionactivacalendario")> _
Public Class EOperacionactivacalendario

#Region " Atributos "

    Private _intNumcuotacalendario As Nullable(Of Integer)
    Private _intCodigocronograma As Nullable(Of Integer)
    Private _strNumerocotizacion As String
    Private _strCodifi As String
    Private _strTiporubrofinanciamiento As String
    Private _strCodoperacionactiva As String
    Private _dtmFechavencimientocuota As Nullable(Of DateTime)
    Private _intCantdiascuota As Nullable(Of Integer)
    Private _decMontosaldoadeudado As Nullable(Of Decimal)
    Private _decMontoprincipal As Nullable(Of Decimal)
    Private _decMontointeres As Nullable(Of Decimal)
    Private _decMontocomision As Nullable(Of Decimal)
    Private _decPorcentasainteres As Nullable(Of Decimal)
    Private _decMontototalpago As Nullable(Of Decimal)
    Private _decMontointeresadeudado As Nullable(Of Decimal)
    Private _decMontocapitalinteres As Nullable(Of Decimal)
    Private _decMontointeresacumulado As Nullable(Of Decimal)
    Private _dtmFechacancelacioncuota As Nullable(Of DateTime)
    Private _strEstadocuotacalendario As String
    Private _strTipotramocalendario As String
    Private _strCodmodulooperacion As String
    Private _strTipooperacion As String
    Private _dtmFecharegistro As Nullable(Of DateTime)
    Private _strCodusuario As String
    Private _strTextoaudicreacion As String
    Private _strTextoaudimodi As String
    Private _decMontosaldoigv As Nullable(Of Decimal)
    Private _decMontoprincipaligv As Nullable(Of Decimal)
    Private _decMontointeresigv As Nullable(Of Decimal)
    Private _decMontototaligv As Nullable(Of Decimal)
    Private _decMontosaldoseguro As Nullable(Of Decimal)
    Private _decMontoprincipalseguro As Nullable(Of Decimal)
    Private _decMontointeresseguro As Nullable(Of Decimal)
    Private _decMontototalseguro As Nullable(Of Decimal)
    Private _decMontototalpagar As Nullable(Of Decimal)
    Private _strTipotasainteres As String
    Private _intNumerocuota As Nullable(Of Integer)
    Private _intNumerocontrato As Nullable(Of Integer)
    Private _decMontototaladeudado As Nullable(Of Decimal)
    Private _decMontototalcuota As Nullable(Of Decimal)
    Private _decMontototalcuotaigv As Nullable(Of Decimal)
    Private _decMontocuotasegurobien As Nullable(Of Decimal)
    Private _intVersioncronograma As Nullable(Of Integer)
    Private _strEstadocronograma As String
    Private _decMontomora As Nullable(Of Decimal)
    Private _decPorcenmora As Nullable(Of Decimal)
    Private _intCantdiasmora As Nullable(Of Integer)
    Private _decMontointeresmoratorio As Nullable(Of Decimal)
    Private _decMontointerescompensatorio As Nullable(Of Decimal)

#End Region

#Region " Propiedades "

    <XmlElement("Numcuotacalendario")> _
    Public Property Numcuotacalendario() As Integer
        Get
            Return Me._intNumcuotacalendario
        End Get
        Set(ByVal value As Integer)
            Me._intNumcuotacalendario = value
        End Set
    End Property

    <XmlElement("Codigocronograma")> _
    Public Property Codigocronograma() As Integer
        Get
            Return Me._intCodigocronograma
        End Get
        Set(ByVal value As Integer)
            Me._intCodigocronograma = value
        End Set
    End Property

    <XmlElement("Numerocotizacion")> _
    Public Property Numerocotizacion() As String
        Get
            Return Me._strNumerocotizacion
        End Get
        Set(ByVal value As String)
            Me._strNumerocotizacion = value
        End Set
    End Property

    <XmlElement("Codifi")> _
    Public Property Codifi() As String
        Get
            Return Me._strCodifi
        End Get
        Set(ByVal value As String)
            Me._strCodifi = value
        End Set
    End Property

    <XmlElement("Tiporubrofinanciamiento")> _
    Public Property Tiporubrofinanciamiento() As String
        Get
            Return Me._strTiporubrofinanciamiento
        End Get
        Set(ByVal value As String)
            Me._strTiporubrofinanciamiento = value
        End Set
    End Property

    <XmlElement("Codoperacionactiva")> _
    Public Property Codoperacionactiva() As String
        Get
            Return Me._strCodoperacionactiva
        End Get
        Set(ByVal value As String)
            Me._strCodoperacionactiva = value
        End Set
    End Property

    <XmlElement("Fechavencimientocuota")> _
    Public Property Fechavencimientocuota() As DateTime
        Get
            Return Me._dtmFechavencimientocuota
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechavencimientocuota = value
        End Set
    End Property

    <XmlElement("Cantdiascuota")> _
    Public Property Cantdiascuota() As Integer
        Get
            Return Me._intCantdiascuota
        End Get
        Set(ByVal value As Integer)
            Me._intCantdiascuota = value
        End Set
    End Property

    <XmlElement("Montosaldoadeudado")> _
    Public Property Montosaldoadeudado() As Decimal
        Get
            Return Me._decMontosaldoadeudado
        End Get
        Set(ByVal value As Decimal)
            Me._decMontosaldoadeudado = value
        End Set
    End Property

    <XmlElement("Montoprincipal")> _
    Public Property Montoprincipal() As Decimal
        Get
            Return Me._decMontoprincipal
        End Get
        Set(ByVal value As Decimal)
            Me._decMontoprincipal = value
        End Set
    End Property

    <XmlElement("Montointeres")> _
    Public Property Montointeres() As Decimal
        Get
            Return Me._decMontointeres
        End Get
        Set(ByVal value As Decimal)
            Me._decMontointeres = value
        End Set
    End Property

    <XmlElement("Montocomision")> _
    Public Property Montocomision() As Decimal
        Get
            Return Me._decMontocomision
        End Get
        Set(ByVal value As Decimal)
            Me._decMontocomision = value
        End Set
    End Property

    <XmlElement("Porcentasainteres")> _
    Public Property Porcentasainteres() As Decimal
        Get
            Return Me._decPorcentasainteres
        End Get
        Set(ByVal value As Decimal)
            Me._decPorcentasainteres = value
        End Set
    End Property

    <XmlElement("Montototalpago")> _
    Public Property Montototalpago() As Decimal
        Get
            Return Me._decMontototalpago
        End Get
        Set(ByVal value As Decimal)
            Me._decMontototalpago = value
        End Set
    End Property

    <XmlElement("Montointeresadeudado")> _
    Public Property Montointeresadeudado() As Decimal
        Get
            Return Me._decMontointeresadeudado
        End Get
        Set(ByVal value As Decimal)
            Me._decMontointeresadeudado = value
        End Set
    End Property

    <XmlElement("Montocapitalinteres")> _
    Public Property Montocapitalinteres() As Decimal
        Get
            Return Me._decMontocapitalinteres
        End Get
        Set(ByVal value As Decimal)
            Me._decMontocapitalinteres = value
        End Set
    End Property

    <XmlElement("Montointeresacumulado")> _
    Public Property Montointeresacumulado() As Decimal
        Get
            Return Me._decMontointeresacumulado
        End Get
        Set(ByVal value As Decimal)
            Me._decMontointeresacumulado = value
        End Set
    End Property

    <XmlElement("Fechacancelacioncuota")> _
    Public Property Fechacancelacioncuota() As DateTime
        Get
            Return Me._dtmFechacancelacioncuota
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechacancelacioncuota = value
        End Set
    End Property

    <XmlElement("Estadocuotacalendario")> _
    Public Property Estadocuotacalendario() As String
        Get
            Return Me._strEstadocuotacalendario
        End Get
        Set(ByVal value As String)
            Me._strEstadocuotacalendario = value
        End Set
    End Property

    <XmlElement("Tipotramocalendario")> _
    Public Property Tipotramocalendario() As String
        Get
            Return Me._strTipotramocalendario
        End Get
        Set(ByVal value As String)
            Me._strTipotramocalendario = value
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

    <XmlElement("Fecharegistro")> _
    Public Property Fecharegistro() As DateTime
        Get
            Return Me._dtmFecharegistro
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFecharegistro = value
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

    <XmlElement("Montosaldoigv")> _
    Public Property Montosaldoigv() As Decimal
        Get
            Return Me._decMontosaldoigv
        End Get
        Set(ByVal value As Decimal)
            Me._decMontosaldoigv = value
        End Set
    End Property

    <XmlElement("Montoprincipaligv")> _
    Public Property Montoprincipaligv() As Decimal
        Get
            Return Me._decMontoprincipaligv
        End Get
        Set(ByVal value As Decimal)
            Me._decMontoprincipaligv = value
        End Set
    End Property

    <XmlElement("Montointeresigv")> _
    Public Property Montointeresigv() As Decimal
        Get
            Return Me._decMontointeresigv
        End Get
        Set(ByVal value As Decimal)
            Me._decMontointeresigv = value
        End Set
    End Property

    <XmlElement("Montototaligv")> _
    Public Property Montototaligv() As Decimal
        Get
            Return Me._decMontototaligv
        End Get
        Set(ByVal value As Decimal)
            Me._decMontototaligv = value
        End Set
    End Property

    <XmlElement("Montosaldoseguro")> _
    Public Property Montosaldoseguro() As Decimal
        Get
            Return Me._decMontosaldoseguro
        End Get
        Set(ByVal value As Decimal)
            Me._decMontosaldoseguro = value
        End Set
    End Property

    <XmlElement("Montoprincipalseguro")> _
    Public Property Montoprincipalseguro() As Decimal
        Get
            Return Me._decMontoprincipalseguro
        End Get
        Set(ByVal value As Decimal)
            Me._decMontoprincipalseguro = value
        End Set
    End Property

    <XmlElement("Montointeresseguro")> _
    Public Property Montointeresseguro() As Decimal
        Get
            Return Me._decMontointeresseguro
        End Get
        Set(ByVal value As Decimal)
            Me._decMontointeresseguro = value
        End Set
    End Property

    <XmlElement("Montototalseguro")> _
    Public Property Montototalseguro() As Decimal
        Get
            Return Me._decMontototalseguro
        End Get
        Set(ByVal value As Decimal)
            Me._decMontototalseguro = value
        End Set
    End Property

    <XmlElement("Montototalpagar")> _
    Public Property Montototalpagar() As Decimal
        Get
            Return Me._decMontototalpagar
        End Get
        Set(ByVal value As Decimal)
            Me._decMontototalpagar = value
        End Set
    End Property

    <XmlElement("Tipotasainteres")> _
    Public Property Tipotasainteres() As String
        Get
            Return Me._strTipotasainteres
        End Get
        Set(ByVal value As String)
            Me._strTipotasainteres = value
        End Set
    End Property

    <XmlElement("Numerocuota")> _
    Public Property Numerocuota() As Integer
        Get
            Return Me._intNumerocuota
        End Get
        Set(ByVal value As Integer)
            Me._intNumerocuota = value
        End Set
    End Property

    <XmlElement("Numerocontrato")> _
    Public Property Numerocontrato() As Integer
        Get
            Return Me._intNumerocontrato
        End Get
        Set(ByVal value As Integer)
            Me._intNumerocontrato = value
        End Set
    End Property

    <XmlElement("Montototaladeudado")> _
    Public Property Montototaladeudado() As Decimal
        Get
            Return Me._decMontototaladeudado
        End Get
        Set(ByVal value As Decimal)
            Me._decMontototaladeudado = value
        End Set
    End Property

    <XmlElement("Montototalcuota")> _
    Public Property Montototalcuota() As Decimal
        Get
            Return Me._decMontototalcuota
        End Get
        Set(ByVal value As Decimal)
            Me._decMontototalcuota = value
        End Set
    End Property

    <XmlElement("Montototalcuotaigv")> _
    Public Property Montototalcuotaigv() As Decimal
        Get
            Return Me._decMontototalcuotaigv
        End Get
        Set(ByVal value As Decimal)
            Me._decMontototalcuotaigv = value
        End Set
    End Property

    <XmlElement("Montocuotasegurobien")> _
    Public Property Montocuotasegurobien() As Decimal
        Get
            Return Me._decMontocuotasegurobien
        End Get
        Set(ByVal value As Decimal)
            Me._decMontocuotasegurobien = value
        End Set
    End Property

    <XmlElement("Versioncronograma")> _
    Public Property Versioncronograma() As Integer
        Get
            Return Me._intVersioncronograma
        End Get
        Set(ByVal value As Integer)
            Me._intVersioncronograma = value
        End Set
    End Property

    <XmlElement("Estadocronograma")> _
    Public Property Estadocronograma() As String
        Get
            Return Me._strEstadocronograma
        End Get
        Set(ByVal value As String)
            Me._strEstadocronograma = value
        End Set
    End Property

    <XmlElement("Montomora")> _
    Public Property Montomora() As Decimal
        Get
            Return Me._decMontomora
        End Get
        Set(ByVal value As Decimal)
            Me._decMontomora = value
        End Set
    End Property

    <XmlElement("Porcenmora")> _
    Public Property Porcenmora() As Decimal
        Get
            Return Me._decPorcenmora
        End Get
        Set(ByVal value As Decimal)
            Me._decPorcenmora = value
        End Set
    End Property

    <XmlElement("Cantdiasmora")> _
    Public Property Cantdiasmora() As Integer
        Get
            Return Me._intCantdiasmora
        End Get
        Set(ByVal value As Integer)
            Me._intCantdiasmora = value
        End Set
    End Property

    <XmlElement("Montointeresmoratorio")> _
    Public Property Montointeresmoratorio() As Decimal
        Get
            Return Me._decMontointeresmoratorio
        End Get
        Set(ByVal value As Decimal)
            Me._decMontointeresmoratorio = value
        End Set
    End Property

    <XmlElement("Montointerescompensatorio")> _
    Public Property Montointerescompensatorio() As Decimal
        Get
            Return Me._decMontointerescompensatorio
        End Get
        Set(ByVal value As Decimal)
            Me._decMontointerescompensatorio = value
        End Set
    End Property


#End Region

End Class