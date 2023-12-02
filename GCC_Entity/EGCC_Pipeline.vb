Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad EGCC_Pipeline
''' </summary>
''' <remarks>
''' Creado Por         : TSF - AEP
''' Fecha de Creación  : 27/08/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_Pipeline")> _
Public Class EGCC_Pipeline

#Region " Atributos "
    Private _strCodigoCotizacion As String
    Private _strCodigoSolicitud As String
    Private _strCodigoMotivo As String
    Private _strCodigoEstado As String
    Private _decPorcentajeAnterior As Nullable(Of Decimal)
    Private _strCodUnico As String
    Private _strRazonSocial As String
    Private _strCodEjecutivoLeasing As String
    Private _decPorcentajeMesActual As Nullable(Of Decimal)
    Private _decPorcentajeMesSiguiente As Nullable(Of Decimal)
    Private _decPorcentajeAnioSiguiente As Nullable(Of Decimal)
    'Inicio IBK - AAE - Se agregan campos
    Private _decMontoLeasing As Nullable(Of Decimal)
    Private _decMontoDesembolsado As Nullable(Of Decimal)
    Private _strCodMoneda As String
    Private _decPorcentajeDesembolsado As Nullable(Of Decimal)
    Private _strComentario As String
    Private _strCodBanca As String
    'Fin IBK - AAE
#End Region
#Region " Propiedades "
    <XmlElement("Codigocotizacion")> _
   Public Property Codigocotizacion() As String
        Get
            Return Me._strCodigocotizacion
        End Get
        Set(ByVal value As String)
            Me._strCodigocotizacion = value
        End Set
    End Property
    <XmlElement("CodigoSolicitud")> _
  Public Property CodigoSolicitud() As String
        Get
            Return Me._strCodigoSolicitud
        End Get
        Set(ByVal value As String)
            Me._strCodigoSolicitud = value
        End Set
    End Property
    <XmlElement("CodigoMotivo")> _
Public Property CodigoMotivo() As String
        Get
            Return Me._strCodigoMotivo
        End Get
        Set(ByVal value As String)
            Me._strCodigoMotivo = value
        End Set
    End Property
    <XmlElement("CodigoEstado")> _
Public Property CodigoEstado() As String
        Get
            Return Me._strCodigoEstado
        End Get
        Set(ByVal value As String)
            Me._strCodigoEstado = value
        End Set
    End Property
    <XmlElement("PorcentajeAnterior")> _
Public Property PorcentajeAnterior() As Nullable(Of Decimal)
        Get
            Return Me._decPorcentajeAnterior
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPorcentajeAnterior = value
        End Set
    End Property
    <XmlElement("PorcentajeMesActual")> _
Public Property PorcentajeMesActual() As Nullable(Of Decimal)
        Get
            Return Me._decPorcentajeMesActual
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPorcentajeMesActual = value
        End Set
    End Property
    <XmlElement("PorcentajeAnioSiguiente")> _
Public Property PorcentajeAnioSiguiente() As Nullable(Of Decimal)
        Get
            Return Me._decPorcentajeAnioSiguiente
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPorcentajeAnioSiguiente = value
        End Set
    End Property
    <XmlElement("PorcentajeMesSiguiente")> _
Public Property PorcentajeMesSiguiente() As Nullable(Of Decimal)
        Get
            Return Me._decPorcentajeMesSiguiente
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPorcentajeMesSiguiente = value
        End Set
    End Property
    <XmlElement("CodUnico")> _
Public Property CodUnico() As String
        Get
            Return Me._strCodUnico
        End Get
        Set(ByVal value As String)
            Me._strCodUnico = value
        End Set
    End Property
    <XmlElement("RazonSocial")> _
Public Property RazonSocial() As String
        Get
            Return Me._strRazonSocial
        End Get
        Set(ByVal value As String)
            Me._strRazonSocial = value
        End Set
    End Property
    <XmlElement("EjecutivoLeasing")> _
Public Property EjecutivoLeasing() As String
        Get
            Return Me._strCodEjecutivoLeasing
        End Get
        Set(ByVal value As String)
            Me._strCodEjecutivoLeasing = value
        End Set
    End Property
    'Inicio IBK - AAE Se agregan propiedades de campos agregados
    <XmlElement("MontoLeasing")> _
  Public Property MontoLeasing() As Nullable(Of Decimal)
        Get
            Return Me._decMontoLeasing
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoLeasing = value
        End Set
    End Property
    <XmlElement("MontoDesembolsado")> _
Public Property MontoDesembolsado() As Nullable(Of Decimal)
        Get
            Return Me._decMontoDesembolsado
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decMontoDesembolsado = value
        End Set
    End Property
    <XmlElement("PorcentajeDesembolsado")> _
Public Property PorcentajeDesembolsado() As Nullable(Of Decimal)
        Get
            Return Me._decPorcentajeDesembolsado
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPorcentajeDesembolsado = value
        End Set
    End Property
    <XmlElement("CodMoneda")> _
Public Property CodMoneda() As String
        Get
            Return Me._strCodMoneda
        End Get
        Set(ByVal value As String)
            Me._strCodMoneda = value
        End Set
    End Property
    <XmlElement("Comentario")> _
Public Property Comentario() As String
        Get
            Return Me._strComentario
        End Get
        Set(ByVal value As String)
            Me._strComentario = value
        End Set
    End Property
    <XmlElement("CodBanca")> _
Public Property CodBanca() As String
        Get
            Return Me._strCodBanca
        End Get
        Set(ByVal value As String)
            Me._strCodBanca = value
        End Set
    End Property
    ' Fin IBK - AAE
#End Region
End Class
