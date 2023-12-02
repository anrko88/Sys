Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Cuentadireccion
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("ECuentadireccion")> _
Public Class ECuentadireccion

#Region " Atributos "

    Private _strTipocuenta As String
    Private _strClavecuenta As String
    Private _strNombrecuenta As String
    Private _strDireccion As String
    Private _strCodigocuenta As String
    Private _strCodigotipoorigen As String
    Private _strCodigomoneda As String

#End Region

#Region " Propiedades "

    <XmlElement("Tipocuenta")> _
    Public Property Tipocuenta() As String
        Get
            Return Me._strTipocuenta
        End Get
        Set(ByVal value As String)
            Me._strTipocuenta = value
        End Set
    End Property

    <XmlElement("Clavecuenta")> _
    Public Property Clavecuenta() As String
        Get
            Return Me._strClavecuenta
        End Get
        Set(ByVal value As String)
            Me._strClavecuenta = value
        End Set
    End Property

    <XmlElement("Nombrecuenta")> _
    Public Property Nombrecuenta() As String
        Get
            Return Me._strNombrecuenta
        End Get
        Set(ByVal value As String)
            Me._strNombrecuenta = value
        End Set
    End Property

    <XmlElement("Direccion")> _
    Public Property Direccion() As String
        Get
            Return Me._strDireccion
        End Get
        Set(ByVal value As String)
            Me._strDireccion = value
        End Set
    End Property

    <XmlElement("Codigocuenta")> _
    Public Property Codigocuenta() As String
        Get
            Return Me._strCodigocuenta
        End Get
        Set(ByVal value As String)
            Me._strCodigocuenta = value
        End Set
    End Property

    <XmlElement("Codigotipoorigen")> _
    Public Property Codigotipoorigen() As String
        Get
            Return Me._strCodigotipoorigen
        End Get
        Set(ByVal value As String)
            Me._strCodigotipoorigen = value
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