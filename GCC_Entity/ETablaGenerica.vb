Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Tablagenerica
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("ETablagenerica")> _
Public Class ETablagenerica

#Region " Atributos "

    Private _strId_tabla As String
    Private _strNombretabla As String
    Private _strCampoclave1 As String
    Private _intNumerocamposclave As Nullable(Of Integer)
    Private _strCampoclave2 As String
    Private _intNumerototalcampos As Nullable(Of Integer)
    Private _strCampoclave3 As String
    Private _strCampoclave4 As String
    Private _strCampoclave5 As String
    Private _strCodmodulooperacion As String
    Private _strTipooperacion As String
    Private _dtmFecharegistro As Nullable(Of DateTime)
    Private _strCodusuario As String
    Private _strTextoaudicreacion As String
    Private _strTextoaudimodi As String

#End Region

#Region " Propiedades "

    <XmlElement("Id_tabla")> _
    Public Property Id_tabla() As String
        Get
            Return Me._strId_tabla
        End Get
        Set(ByVal value As String)
            Me._strId_tabla = value
        End Set
    End Property

    <XmlElement("Nombretabla")> _
    Public Property Nombretabla() As String
        Get
            Return Me._strNombretabla
        End Get
        Set(ByVal value As String)
            Me._strNombretabla = value
        End Set
    End Property

    <XmlElement("Campoclave1")> _
    Public Property Campoclave1() As String
        Get
            Return Me._strCampoclave1
        End Get
        Set(ByVal value As String)
            Me._strCampoclave1 = value
        End Set
    End Property

    <XmlElement("Numerocamposclave")> _
    Public Property Numerocamposclave() As Integer
        Get
            Return Me._intNumerocamposclave
        End Get
        Set(ByVal value As Integer)
            Me._intNumerocamposclave = value
        End Set
    End Property

    <XmlElement("Campoclave2")> _
    Public Property Campoclave2() As String
        Get
            Return Me._strCampoclave2
        End Get
        Set(ByVal value As String)
            Me._strCampoclave2 = value
        End Set
    End Property

    <XmlElement("Numerototalcampos")> _
    Public Property Numerototalcampos() As Integer
        Get
            Return Me._intNumerototalcampos
        End Get
        Set(ByVal value As Integer)
            Me._intNumerototalcampos = value
        End Set
    End Property

    <XmlElement("Campoclave3")> _
    Public Property Campoclave3() As String
        Get
            Return Me._strCampoclave3
        End Get
        Set(ByVal value As String)
            Me._strCampoclave3 = value
        End Set
    End Property

    <XmlElement("Campoclave4")> _
    Public Property Campoclave4() As String
        Get
            Return Me._strCampoclave4
        End Get
        Set(ByVal value As String)
            Me._strCampoclave4 = value
        End Set
    End Property

    <XmlElement("Campoclave5")> _
    Public Property Campoclave5() As String
        Get
            Return Me._strCampoclave5
        End Get
        Set(ByVal value As String)
            Me._strCampoclave5 = value
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
