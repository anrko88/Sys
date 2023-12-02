Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Diasferiados
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EDiasferiados")> _
Public Class EDiasferiados

#Region " Atributos "

    Private _dtmDiaferiado As Nullable(Of DateTime)
    Private _dtmDiahabilanterior As Nullable(Of DateTime)
    Private _dtmDiahabilposterior As Nullable(Of DateTime)
    Private _strDescripcionferiado As String
    Private _intEstadovisualizar As Nullable(Of Integer)

#End Region

#Region " Propiedades "

    <XmlElement("Diaferiado")> _
    Public Property Diaferiado() As DateTime
        Get
            Return Me._dtmDiaferiado
        End Get
        Set(ByVal value As DateTime)
            Me._dtmDiaferiado = value
        End Set
    End Property

    <XmlElement("Diahabilanterior")> _
    Public Property Diahabilanterior() As DateTime
        Get
            Return Me._dtmDiahabilanterior
        End Get
        Set(ByVal value As DateTime)
            Me._dtmDiahabilanterior = value
        End Set
    End Property

    <XmlElement("Diahabilposterior")> _
    Public Property Diahabilposterior() As DateTime
        Get
            Return Me._dtmDiahabilposterior
        End Get
        Set(ByVal value As DateTime)
            Me._dtmDiahabilposterior = value
        End Set
    End Property

    <XmlElement("Descripcionferiado")> _
    Public Property Descripcionferiado() As String
        Get
            Return Me._strDescripcionferiado
        End Get
        Set(ByVal value As String)
            Me._strDescripcionferiado = value
        End Set
    End Property

    <XmlElement("Estadovisualizar")> _
    Public Property Estadovisualizar() As Integer
        Get
            Return Me._intEstadovisualizar
        End Get
        Set(ByVal value As Integer)
            Me._intEstadovisualizar = value
        End Set
    End Property


#End Region

End Class