Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Lsgconceptostarifas
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("ELsgconceptostarifas")> _
Public Class ELsgconceptostarifas

#Region " Atributos "

    Private _intCodconceptotarifa As Nullable(Of Integer)
    Private _strTipoconceptotarifa As String
    Private _strConceptotarifa As String
    Private _strCodtarifa As String
    Private _strCodmoneda As String
    Private _decTasa As Nullable(Of Decimal)
    Private _decMinimo As Nullable(Of Decimal)
    Private _decMaximo As Nullable(Of Decimal)
    Private _strOportunidadcobro As String

#End Region

#Region " Propiedades "

    <XmlElement("Codconceptotarifa")> _
    Public Property Codconceptotarifa() As Nullable(Of Integer)
        Get
            Return Me._intCodconceptotarifa
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intCodconceptotarifa = value
        End Set
    End Property

    <XmlElement("Tipoconceptotarifa")> _
    Public Property Tipoconceptotarifa() As String
        Get
            Return Me._strTipoconceptotarifa
        End Get
        Set(ByVal value As String)
            Me._strTipoconceptotarifa = value
        End Set
    End Property

    <XmlElement("Conceptotarifa")> _
    Public Property Conceptotarifa() As String
        Get
            Return Me._strConceptotarifa
        End Get
        Set(ByVal value As String)
            Me._strConceptotarifa = value
        End Set
    End Property

    <XmlElement("Codtarifa")> _
    Public Property Codtarifa() As String
        Get
            Return Me._strCodtarifa
        End Get
        Set(ByVal value As String)
            Me._strCodtarifa = value
        End Set
    End Property

    <XmlElement("Codmoneda")> _
    Public Property Codmoneda() As String
        Get
            Return Me._strCodmoneda
        End Get
        Set(ByVal value As String)
            Me._strCodmoneda = value
        End Set
    End Property

    <XmlElement("Tasa")> _
    Public Property Tasa() As Nullable(Of Decimal)
        Get
            Return Me._decTasa
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decTasa = value
        End Set
    End Property

    <XmlElement("Minimo")> _
    Public Property Minimo() As Nullable(Of Decimal)
        Get
            Return Me._decMinimo
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMinimo = value
        End Set
    End Property

    <XmlElement("Maximo")> _
    Public Property Maximo() As Decimal
        Get
            Return Me._decMaximo
        End Get
        Set(ByVal value As Decimal)
            Me._decMaximo = value
        End Set
    End Property

    <XmlElement("Oportunidadcobro")> _
    Public Property Oportunidadcobro() As String
        Get
            Return Me._strOportunidadcobro
        End Get
        Set(ByVal value As String)
            Me._strOportunidadcobro = value
        End Set
    End Property


#End Region

End Class
