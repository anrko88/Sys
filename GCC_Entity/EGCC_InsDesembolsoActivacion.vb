Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_cotizacion
''' </summary>
''' <remarks>
''' Creado Por         : IBK - AAE
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGCC_InsDesembolsoActivacion")> _
Public Class EGCC_InsDesembolsoActivacion
#Region " Atributos "

    Private _strCodSolicitudCredito As String
    Private _strCodInstruccionDesembolso As String
    Private _strCodigotipopersona As String
    Private _decValorventa As Nullable(Of Decimal)
    Private _decValorventaigv As Nullable(Of Decimal)
    Private _decPrecioventa As Nullable(Of Decimal)
    Private _decImportecuotainicial As Nullable(Of Decimal)
    Private _decRiesgoneto As Nullable(Of Decimal)
    Private _strCodigotipocronograma As String
    Private _intPlazograciacuota As Nullable(Of Integer)
    Private _strCodigotipograciacuota As String
    Private _dtmFechamaxactivacion As Nullable(Of DateTime)
    Private _intNumerocuotas As Nullable(Of Integer)
    Private _strCodigoperiodicidad As String
    Private _strCodigofrecuenciapago As String
    Private _dtmFechaprimervencimiento As Nullable(Of DateTime)
    Private _decTeaporc As Nullable(Of Decimal)
    Private _strCodigobientiposeguro As String
    Private _decBienimporteprima As Nullable(Of Decimal)
    Private _intBiennrocuotasfinanciar As Nullable(Of Integer)
    Private _strCodigodesgravamentiposeguro As String
    Private _decDesgravamenimporteprima As Nullable(Of Decimal)
    Private _intDesgravamennrocuotasfinanciar As Nullable(Of Integer)


#End Region

#Region " Propiedades "

    <XmlElement("CodSolicitudCredito")> _
    Public Property CodSolicitudCredito() As String
        Get
            Return Me._strCodSolicitudCredito
        End Get
        Set(ByVal value As String)
            Me._strCodSolicitudCredito = value
        End Set
    End Property

    <XmlElement("CodInstruccionDesembolso")> _
    Public Property CodInstruccionDesembolso() As String
        Get
            Return Me._strCodInstruccionDesembolso
        End Get
        Set(ByVal value As String)
            Me._strCodInstruccionDesembolso = value
        End Set
    End Property

    <XmlElement("Codigotipopersona")> _
    Public Property Codigotipopersona() As String
        Get
            Return Me._strCodigotipopersona
        End Get
        Set(ByVal value As String)
            Me._strCodigotipopersona = value
        End Set
    End Property

    <XmlElement("Valorventa")> _
    Public Property Valorventa() As Nullable(Of Decimal)
        Get
            Return Me._decValorventa
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decValorventa = value
        End Set
    End Property

    <XmlElement("Valorventaigv")> _
    Public Property Valorventaigv() As Nullable(Of Decimal)
        Get
            Return Me._decValorventaigv
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decValorventaigv = value
        End Set
    End Property

    <XmlElement("Precioventa")> _
    Public Property Precioventa() As Nullable(Of Decimal)
        Get
            Return Me._decPrecioventa
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPrecioventa = value
        End Set
    End Property

    <XmlElement("Importecuotainicial")> _
   Public Property Importecuotainicial() As Nullable(Of Decimal)
        Get
            Return Me._decImportecuotainicial
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decImportecuotainicial = value
        End Set
    End Property

    <XmlElement("Riesgoneto")> _
    Public Property Riesgoneto() As Nullable(Of Decimal)
        Get
            Return Me._decRiesgoneto
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decRiesgoneto = value
        End Set
    End Property

    <XmlElement("Codigotipocronograma")> _
    Public Property Codigotipocronograma() As String
        Get
            Return Me._strCodigotipocronograma
        End Get
        Set(ByVal value As String)
            Me._strCodigotipocronograma = value
        End Set
    End Property

    <XmlElement("Plazograciacuota")> _
    Public Property Plazograciacuota() As Nullable(Of Integer)
        Get
            Return Me._intPlazograciacuota
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intPlazograciacuota = value
        End Set
    End Property

    <XmlElement("Codigotipograciacuota")> _
    Public Property Codigotipograciacuota() As String
        Get
            Return Me._strCodigotipograciacuota
        End Get
        Set(ByVal value As String)
            Me._strCodigotipograciacuota = value
        End Set
    End Property

    <XmlElement("Fechamaxactivacion")> _
    Public Property Fechamaxactivacion() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechamaxactivacion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechamaxactivacion = value
        End Set
    End Property

    <XmlElement("Numerocuotas")> _
    Public Property Numerocuotas() As Nullable(Of Integer)
        Get
            Return Me._intNumerocuotas
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intNumerocuotas = value
        End Set
    End Property

    <XmlElement("Codigoperiodicidad")> _
    Public Property Codigoperiodicidad() As String
        Get
            Return Me._strCodigoperiodicidad
        End Get
        Set(ByVal value As String)
            Me._strCodigoperiodicidad = value
        End Set
    End Property

    <XmlElement("Codigofrecuenciapago")> _
    Public Property Codigofrecuenciapago() As String
        Get
            Return Me._strCodigofrecuenciapago
        End Get
        Set(ByVal value As String)
            Me._strCodigofrecuenciapago = value
        End Set
    End Property

    <XmlElement("Fechaprimervencimiento")> _
    Public Property Fechaprimervencimiento() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaprimervencimiento
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaprimervencimiento = value
        End Set
    End Property

    <XmlElement("Teaporc")> _
    Public Property Teaporc() As Nullable(Of Decimal)
        Get
            Return Me._decTeaporc
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decTeaporc = value
        End Set
    End Property

    <XmlElement("Codigobientiposeguro")> _
    Public Property Codigobientiposeguro() As String
        Get
            Return Me._strCodigobientiposeguro
        End Get
        Set(ByVal value As String)
            Me._strCodigobientiposeguro = value
        End Set
    End Property

    <XmlElement("Bienimporteprima")> _
    Public Property Bienimporteprima() As Nullable(Of Decimal)
        Get
            Return Me._decBienimporteprima
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decBienimporteprima = value
        End Set
    End Property

    <XmlElement("Biennrocuotasfinanciar")> _
    Public Property Biennrocuotasfinanciar() As Nullable(Of Integer)
        Get
            Return Me._intBiennrocuotasfinanciar
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intBiennrocuotasfinanciar = value
        End Set
    End Property

    <XmlElement("Codigodesgravamentiposeguro")> _
    Public Property Codigodesgravamentiposeguro() As String
        Get
            Return Me._strCodigodesgravamentiposeguro
        End Get
        Set(ByVal value As String)
            Me._strCodigodesgravamentiposeguro = value
        End Set
    End Property

    <XmlElement("Desgravamenimporteprima")> _
    Public Property Desgravamenimporteprima() As Nullable(Of Decimal)
        Get
            Return Me._decDesgravamenimporteprima
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decDesgravamenimporteprima = value
        End Set
    End Property

    <XmlElement("Desgravamennrocuotasfinanciar")> _
    Public Property Desgravamennrocuotasfinanciar() As Nullable(Of Integer)
        Get
            Return Me._intDesgravamennrocuotasfinanciar
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intDesgravamennrocuotasfinanciar = value
        End Set
    End Property

#End Region

End Class