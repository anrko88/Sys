Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_contratorepresentante
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_contratorepresentante")> _
Public Class EGcc_contratorepresentante
    Inherits EGcc_representante

#Region " Atributos "

    Private _strNumerocontrato As String

#End Region

#Region " Propiedades "

    <XmlElement("Numerocontrato")> _
    Public Property Numerocontrato() As String
        Get
            Return Me._strNumerocontrato
        End Get
        Set(ByVal value As String)
            Me._strNumerocontrato = value
        End Set
    End Property

#End Region

End Class

''' <summary>
''' Clase que hereda de List(Of EGcc_contratorepresentante) para la implementación de la lista
''' </summary>
''' <remarks></remarks>
<Serializable(), XmlRoot("ListEGcc_contratorepresentante")> _
Public Class ListEGcc_contratorepresentante
    Inherits List(Of EGcc_contratorepresentante)

End Class