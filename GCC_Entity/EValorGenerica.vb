Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Valorgenerica
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EValorgenerica")> _
Public Class EValorgenerica

#Region " Atributos "

    Private _intId_registro As Integer = 0
    Private _strId_tabla As String
    Private _strClave1 As String
    Private _strClave2 As String
    Private _strClave3 As String
    Private _strClave4 As String
    Private _strClave5 As String
    Private _strValor1 As String
    Private _strValor2 As String
    Private _strValor3 As String
    Private _strValor4 As String
    Private _strValor5 As String
    Private _strValor6 As String
    Private _strValor7 As String
    Private _strValor8 As String
    Private _strValor9 As String
    Private _strValor10 As String
    Private _strValor11 As String
    Private _strValor12 As String
    Private _strValor13 As String
    Private _strValor14 As String
    Private _strValor15 As String
    Private _strCodmodulooperacion As String
    Private _strTipooperacion As String
    Private _dtmFecharegistro As Nullable(Of DateTime)
    Private _strCodusuario As String
    Private _strTextoaudicreacion As String
    Private _strTextoaudimodi As String

#End Region

#Region " Propiedades "

    <XmlElement("Id_registro")> _
    Public Property Id_registro() As Integer
        Get
            Return Me._intId_registro
        End Get
        Set(ByVal value As Integer)
            Me._intId_registro = value
        End Set
    End Property

    <XmlElement("Id_tabla")> _
    Public Property Id_tabla() As String
        Get
            Return Me._strId_tabla
        End Get
        Set(ByVal value As String)
            Me._strId_tabla = value
        End Set
    End Property

    <XmlElement("Clave1")> _
    Public Property Clave1() As String
        Get
            Return Me._strClave1
        End Get
        Set(ByVal value As String)
            Me._strClave1 = value
        End Set
    End Property

    <XmlElement("Clave2")> _
    Public Property Clave2() As String
        Get
            Return Me._strClave2
        End Get
        Set(ByVal value As String)
            Me._strClave2 = value
        End Set
    End Property

    <XmlElement("Clave3")> _
    Public Property Clave3() As String
        Get
            Return Me._strClave3
        End Get
        Set(ByVal value As String)
            Me._strClave3 = value
        End Set
    End Property

    <XmlElement("Clave4")> _
    Public Property Clave4() As String
        Get
            Return Me._strClave4
        End Get
        Set(ByVal value As String)
            Me._strClave4 = value
        End Set
    End Property

    <XmlElement("Clave5")> _
    Public Property Clave5() As String
        Get
            Return Me._strClave5
        End Get
        Set(ByVal value As String)
            Me._strClave5 = value
        End Set
    End Property

    <XmlElement("Valor1")> _
    Public Property Valor1() As String
        Get
            Return Me._strValor1
        End Get
        Set(ByVal value As String)
            Me._strValor1 = value
        End Set
    End Property

    <XmlElement("Valor2")> _
    Public Property Valor2() As String
        Get
            Return Me._strValor2
        End Get
        Set(ByVal value As String)
            Me._strValor2 = value
        End Set
    End Property

    <XmlElement("Valor3")> _
    Public Property Valor3() As String
        Get
            Return Me._strValor3
        End Get
        Set(ByVal value As String)
            Me._strValor3 = value
        End Set
    End Property

    <XmlElement("Valor4")> _
    Public Property Valor4() As String
        Get
            Return Me._strValor4
        End Get
        Set(ByVal value As String)
            Me._strValor4 = value
        End Set
    End Property

    <XmlElement("Valor5")> _
    Public Property Valor5() As String
        Get
            Return Me._strValor5
        End Get
        Set(ByVal value As String)
            Me._strValor5 = value
        End Set
    End Property

    <XmlElement("Valor6")> _
    Public Property Valor6() As String
        Get
            Return Me._strValor6
        End Get
        Set(ByVal value As String)
            Me._strValor6 = value
        End Set
    End Property

    <XmlElement("Valor7")> _
    Public Property Valor7() As String
        Get
            Return Me._strValor7
        End Get
        Set(ByVal value As String)
            Me._strValor7 = value
        End Set
    End Property

    <XmlElement("Valor8")> _
    Public Property Valor8() As String
        Get
            Return Me._strValor8
        End Get
        Set(ByVal value As String)
            Me._strValor8 = value
        End Set
    End Property

    <XmlElement("Valor9")> _
    Public Property Valor9() As String
        Get
            Return Me._strValor9
        End Get
        Set(ByVal value As String)
            Me._strValor9 = value
        End Set
    End Property

    <XmlElement("Valor10")> _
    Public Property Valor10() As String
        Get
            Return Me._strValor10
        End Get
        Set(ByVal value As String)
            Me._strValor10 = value
        End Set
    End Property

    <XmlElement("Valor11")> _
    Public Property Valor11() As String
        Get
            Return Me._strValor11
        End Get
        Set(ByVal value As String)
            Me._strValor11 = value
        End Set
    End Property

    <XmlElement("Valor12")> _
    Public Property Valor12() As String
        Get
            Return Me._strValor12
        End Get
        Set(ByVal value As String)
            Me._strValor12 = value
        End Set
    End Property

    <XmlElement("Valor13")> _
    Public Property Valor13() As String
        Get
            Return Me._strValor13
        End Get
        Set(ByVal value As String)
            Me._strValor13 = value
        End Set
    End Property

    <XmlElement("Valor14")> _
    Public Property Valor14() As String
        Get
            Return Me._strValor14
        End Get
        Set(ByVal value As String)
            Me._strValor14 = value
        End Set
    End Property

    <XmlElement("Valor15")> _
    Public Property Valor15() As String
        Get
            Return Me._strValor15
        End Get
        Set(ByVal value As String)
            Me._strValor15 = value
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


#End Region

End Class