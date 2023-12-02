Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_cotizacioncronograma
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_cotizacioncronograma")> _
Public Class EGcc_cotizacioncronograma

#Region " Atributos "

    Private _intNumerocuota As Nullable(Of Integer)
    Private _strCodigocotizacion As String
    Private _strVersioncotizacion As String
    Private _dtmFechavencimiento As Nullable(Of DateTime)
    Private _intCantdiascuota As Nullable(Of Integer)
    Private _decMontosaldoadeudado As Nullable(Of Decimal)
    Private _decMontointeresbien As Nullable(Of Decimal)
    Private _decMontoprincipalbien As Nullable(Of Decimal)
    Private _decMontototalcuota As Nullable(Of Decimal)
    Private _decSaldoseguro As Nullable(Of Decimal)
    Private _decInteressegurobien As Nullable(Of Decimal)
    Private _decPrincipalsegurobien As Nullable(Of Decimal)
    Private _decMontocuotasegurobien As Nullable(Of Decimal)
    Private _decTotalapagar As Nullable(Of Decimal)
    Private _decMontototalcuotaigv As Nullable(Of Decimal)
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String

    Private _decSaldoSeguroDes As Nullable(Of Decimal)
    Private _decInteresSeguroDes As Nullable(Of Decimal)
    Private _decPrincipalSeguroDes As Nullable(Of Decimal)
    Private _decCuotaSeguroDes As Nullable(Of Decimal)

    Private _strMontosaldoadeudado As String
    Private _strMontointeresbien As String
    Private _strMontoprincipalbien As String
    Private _strMontototalcuota As String
    Private _strSaldoseguro As String
    Private _strInteressegurobien As String
    Private _strPrincipalsegurobien As String
    Private _strMontocuotasegurobien As String
    Private _strTotalapagar As String
    Private _strMontototalcuotaigv As String
    Private _strSaldoSeguroDes As String
    Private _strInteresSeguroDes As String
    Private _strPrincipalSeguroDes As String
    Private _strCuotaSeguroDes As String
    Private _strFechavencimiento As String


#End Region

#Region " Propiedades "

    <XmlElement("Numerocuota")> _
    Public Property Numerocuota() As Nullable(Of Integer)
        Get
            Return Me._intNumerocuota
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intNumerocuota = value
        End Set
    End Property

    <XmlElement("Codigocotizacion")> _
    Public Property Codigocotizacion() As String
        Get
            Return Me._strCodigocotizacion
        End Get
        Set(ByVal value As String)
            Me._strCodigocotizacion = value
        End Set
    End Property

    <XmlElement("Versioncotizacion")> _
    Public Property Versioncotizacion() As String
        Get
            Return Me._strVersioncotizacion
        End Get
        Set(ByVal value As String)
            Me._strVersioncotizacion = value
        End Set
    End Property

    <XmlElement("Fechavencimiento")> _
    Public Property Fechavencimiento() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechavencimiento
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechavencimiento = value
        End Set
    End Property

    <XmlElement("Cantdiascuota")> _
    Public Property Cantdiascuota() As Nullable(Of Integer)
        Get
            Return Me._intCantdiascuota
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCantdiascuota = value
        End Set
    End Property

    <XmlElement("Montosaldoadeudado")> _
    Public Property Montosaldoadeudado() As Nullable(Of Decimal)
        Get
            Return Me._decMontosaldoadeudado
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontosaldoadeudado = value
        End Set
    End Property

    <XmlElement("Montointeresbien")> _
    Public Property Montointeresbien() As Nullable(Of Decimal)
        Get
            Return Me._decMontointeresbien
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontointeresbien = value
        End Set
    End Property

    <XmlElement("Montoprincipalbien")> _
    Public Property Montoprincipalbien() As Nullable(Of Decimal)
        Get
            Return Me._decMontoprincipalbien
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoprincipalbien = value
        End Set
    End Property

    <XmlElement("Montototalcuota")> _
    Public Property Montototalcuota() As Nullable(Of Decimal)
        Get
            Return Me._decMontototalcuota
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontototalcuota = value
        End Set
    End Property

    <XmlElement("Saldoseguro")> _
    Public Property Saldoseguro() As Nullable(Of Decimal)
        Get
            Return Me._decSaldoseguro
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decSaldoseguro = value
        End Set
    End Property

    <XmlElement("Interessegurobien")> _
    Public Property Interessegurobien() As Nullable(Of Decimal)
        Get
            Return Me._decInteressegurobien
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decInteressegurobien = value
        End Set
    End Property

    <XmlElement("Principalsegurobien")> _
    Public Property Principalsegurobien() As Nullable(Of Decimal)
        Get
            Return Me._decPrincipalsegurobien
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPrincipalsegurobien = value
        End Set
    End Property

    <XmlElement("Montocuotasegurobien")> _
    Public Property Montocuotasegurobien() As Nullable(Of Decimal)
        Get
            Return Me._decMontocuotasegurobien
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontocuotasegurobien = value
        End Set
    End Property

    <XmlElement("Totalapagar")> _
    Public Property Totalapagar() As Nullable(Of Decimal)
        Get
            Return Me._decTotalapagar
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decTotalapagar = value
        End Set
    End Property

    <XmlElement("Montototalcuotaigv")> _
    Public Property Montototalcuotaigv() As Nullable(Of Decimal)
        Get
            Return Me._decMontototalcuotaigv
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontototalcuotaigv = value
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



    <XmlElement("SaldoSeguroDes")> _
    Public Property SaldoSeguroDes() As Nullable(Of Decimal)
        Get
            Return Me._decSaldoSeguroDes
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decSaldoSeguroDes = value
        End Set
    End Property

    <XmlElement("InteresSeguroDes")> _
    Public Property InteresSeguroDes() As Nullable(Of Decimal)
        Get
            Return Me._decInteresSeguroDes
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decInteresSeguroDes = value
        End Set
    End Property

    <XmlElement("PrincipalSeguroDes")> _
    Public Property PrincipalSeguroDes() As Nullable(Of Decimal)
        Get
            Return Me._decPrincipalSeguroDes
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPrincipalSeguroDes = value
        End Set
    End Property

    <XmlElement("CuotaSeguroDes")> _
    Public Property CuotaSeguroDes() As Nullable(Of Decimal)
        Get
            Return Me._decCuotaSeguroDes
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decCuotaSeguroDes = value
        End Set
    End Property




    <XmlElement("SMontosaldoadeudado")> _
    Public Property SMontosaldoadeudado() As String
        Get
            Return Me._strMontosaldoadeudado
        End Get
        Set(ByVal value As String)
            Me._strMontosaldoadeudado = value
        End Set
    End Property

    <XmlElement("SMontointeresbien")> _
    Public Property SMontointeresbien() As String
        Get
            Return Me._strMontointeresbien
        End Get
        Set(ByVal value As String)
            Me._strMontointeresbien = value
        End Set
    End Property

    <XmlElement("SMontoprincipalbien")> _
    Public Property SMontoprincipalbien() As String
        Get
            Return Me._strMontoprincipalbien
        End Get
        Set(ByVal value As String)
            Me._strMontoprincipalbien = value
        End Set
    End Property

    <XmlElement("SMontototalcuota")> _
     Public Property SMontototalcuota() As String
        Get
            Return Me._strMontototalcuota
        End Get
        Set(ByVal value As String)
            Me._strMontototalcuota = value
        End Set
    End Property

    <XmlElement("SSaldoseguro")> _
     Public Property SSaldoseguro() As String
        Get
            Return Me._strSaldoseguro
        End Get
        Set(ByVal value As String)
            Me._strSaldoseguro = value
        End Set
    End Property


    <XmlElement("SInteressegurobien")> _
     Public Property SInteressegurobien() As String
        Get
            Return Me._strInteressegurobien
        End Get
        Set(ByVal value As String)
            Me._strInteressegurobien = value
        End Set
    End Property


    <XmlElement("SPrincipalsegurobien")> _
     Public Property SPrincipalsegurobien() As String
        Get
            Return Me._strPrincipalsegurobien
        End Get
        Set(ByVal value As String)
            Me._strPrincipalsegurobien = value
        End Set
    End Property

    <XmlElement("SMontocuotasegurobien")> _
     Public Property SMontocuotasegurobien() As String
        Get
            Return Me._strMontocuotasegurobien
        End Get
        Set(ByVal value As String)
            Me._strMontocuotasegurobien = value
        End Set
    End Property

    <XmlElement("STotalapagar")> _
     Public Property STotalapagar() As String
        Get
            Return Me._strTotalapagar
        End Get
        Set(ByVal value As String)
            Me._strTotalapagar = value
        End Set
    End Property

    <XmlElement("SMontototalcuotaigv")> _
     Public Property SMontototalcuotaigv() As String
        Get
            Return Me._strMontototalcuotaigv
        End Get
        Set(ByVal value As String)
            Me._strMontototalcuotaigv = value
        End Set
    End Property

    <XmlElement("SSaldoSeguroDes")> _
     Public Property SSaldoSeguroDes() As String
        Get
            Return Me._strSaldoSeguroDes
        End Get
        Set(ByVal value As String)
            Me._strSaldoSeguroDes = value
        End Set
    End Property

    <XmlElement("SInteresSeguroDes")> _
     Public Property SInteresSeguroDes() As String
        Get
            Return Me._strInteresSeguroDes
        End Get
        Set(ByVal value As String)
            Me._strInteresSeguroDes = value
        End Set
    End Property

    <XmlElement("SPrincipalSeguroDes")> _
     Public Property SPrincipalSeguroDes() As String
        Get
            Return Me._strPrincipalSeguroDes
        End Get
        Set(ByVal value As String)
            Me._strPrincipalSeguroDes = value
        End Set
    End Property

    <XmlElement("SCuotaSeguroDes")> _
     Public Property SCuotaSeguroDes() As String
        Get
            Return Me._strCuotaSeguroDes
        End Get
        Set(ByVal value As String)
            Me._strCuotaSeguroDes = value
        End Set
    End Property

    <XmlElement("SFechavencimiento")> _
     Public Property SFechavencimiento() As String
        Get
            Return Me._strFechavencimiento
        End Get
        Set(ByVal value As String)
            Me._strFechavencimiento = value
        End Set
    End Property

#End Region

End Class


''' <summary>
''' Clase que hereda de List(Of EGcc_cotizacioncronograma) para la implementación de la lista
''' </summary>
''' <remarks></remarks>
<Serializable(), XmlRoot("ListEGcc_cotizacioncronograma")> _
Public Class ListEGcc_cotizacioncronograma
    Inherits List(Of EGcc_cotizacioncronograma)

End Class