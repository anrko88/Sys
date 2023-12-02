
Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad GCC_FraccionarCobro
''' </summary>
''' <remarks>
''' Creado Por         : TSF - WCR
''' Fecha de Creación  : 13/12/2012 10:26:06 a.m.
''' </remarks>
<Serializable(), XmlRoot("EGCC_FraccionarCobro")> _
Public Class EGCC_FraccionarCobro

#Region " Atributos "
		
        Private _intItem As Integer = 0
        Private _strCodOperacionActiva As String = String.Empty
        Private _strTipoRubroFinanciamiento As String = String.Empty
        Private _strCodIfi As String = String.Empty
        Private _strTipoRecuperacion As String = String.Empty
        Private _shtNumSecRecuperacion As Short = 0
        Private _strCodComisionTipo As String = String.Empty
        Private _strTipoValorComision As String = String.Empty
        Private _strTipoAplicacionComision As String = String.Empty
        Private _shtNumSecRecupComi As Short = 0
        Private _shtNroCuota As Short = 0
        Private _dtmFechaCobro As DateTime = New DateTime(1900, 1, 1)
        Private _decMontoFraccionar As Decimal = 0
        Private _decMontoComision As Decimal = 0
        Private _decMontoIGVComision As Decimal = 0
        Private _decMontoInteres As Decimal = 0
        Private _decMontoTotal As Decimal = 0
        Private _shtDias As Short = 0
        Private _strCodigoEstado As String = String.Empty
        Private _strUsuarioRegistro As String = String.Empty
        Private _dtmFecRegistro As DateTime = New DateTime(1900, 1, 1)
        Private _strUsuarioModificacion As String = String.Empty
        Private _dtmFecModificacion As DateTime = New DateTime(1900, 1, 1)

#End Region

 
#Region " Propiedades "
		<XmlElement("Item")> _
		Public Property Item As Integer
			Get
				Return _intItem
			End Get
			Set(ByVal Value As Integer)
				_intItem = Value
			End Set
		End Property				

		<XmlElement("CodOperacionActiva")> _
		Public Property CodOperacionActiva As String
			Get
				Return _strCodOperacionActiva
			End Get
			Set(ByVal Value As String)
				_strCodOperacionActiva = Value
			End Set
		End Property


		<XmlElement("TipoRubroFinanciamiento")> _
		Public Property TipoRubroFinanciamiento As String
			Get
				Return _strTipoRubroFinanciamiento
			End Get
			Set(ByVal Value As String)
				_strTipoRubroFinanciamiento = Value
			End Set
		End Property


		<XmlElement("CodIfi")> _
		Public Property CodIfi As String
			Get
				Return _strCodIfi
			End Get
			Set(ByVal Value As String)
				_strCodIfi = Value
			End Set
		End Property


		<XmlElement("TipoRecuperacion")> _
		Public Property TipoRecuperacion As String
			Get
				Return _strTipoRecuperacion
			End Get
			Set(ByVal Value As String)
				_strTipoRecuperacion = Value
			End Set
		End Property


		<XmlElement("NumSecRecuperacion")> _
		Public Property NumSecRecuperacion As Short
			Get
				Return _shtNumSecRecuperacion
			End Get
			Set(ByVal Value As Short)
				_shtNumSecRecuperacion = Value
			End Set
		End Property


		<XmlElement("CodComisionTipo")> _
		Public Property CodComisionTipo As String
			Get
				Return _strCodComisionTipo
			End Get
			Set(ByVal Value As String)
				_strCodComisionTipo = Value
			End Set
		End Property


		<XmlElement("TipoValorComision")> _
		Public Property TipoValorComision As String
			Get
				Return _strTipoValorComision
			End Get
			Set(ByVal Value As String)
				_strTipoValorComision = Value
			End Set
		End Property


		<XmlElement("TipoAplicacionComision")> _
		Public Property TipoAplicacionComision As String
			Get
				Return _strTipoAplicacionComision
			End Get
			Set(ByVal Value As String)
				_strTipoAplicacionComision = Value
			End Set
		End Property


		<XmlElement("NumSecRecupComi")> _
		Public Property NumSecRecupComi As Short
			Get
				Return _shtNumSecRecupComi
			End Get
			Set(ByVal Value As Short)
				_shtNumSecRecupComi = Value
			End Set
		End Property


		<XmlElement("NroCuota")> _
		Public Property NroCuota As Short
			Get
				Return _shtNroCuota
			End Get
			Set(ByVal Value As Short)
				_shtNroCuota = Value
			End Set
		End Property


		<XmlElement("FechaCobro")> _
		Public Property FechaCobro As DateTime
			Get
				Return _dtmFechaCobro
			End Get
			Set(ByVal Value As DateTime)
				_dtmFechaCobro = Value
			End Set
		End Property


		<XmlElement("MontoFraccionar")> _
		Public Property MontoFraccionar As Decimal
			Get
				Return _decMontoFraccionar
			End Get
			Set(ByVal Value As Decimal)
				_decMontoFraccionar = Value
			End Set
		End Property


		<XmlElement("MontoComision")> _
		Public Property MontoComision As Decimal
			Get
				Return _decMontoComision
			End Get
			Set(ByVal Value As Decimal)
				_decMontoComision = Value
			End Set
		End Property


		<XmlElement("MontoIGVComision")> _
		Public Property MontoIGVComision As Decimal
			Get
				Return _decMontoIGVComision
			End Get
			Set(ByVal Value As Decimal)
				_decMontoIGVComision = Value
			End Set
		End Property


		<XmlElement("MontoInteres")> _
		Public Property MontoInteres As Decimal
			Get
				Return _decMontoInteres
			End Get
			Set(ByVal Value As Decimal)
				_decMontoInteres = Value
			End Set
		End Property


		<XmlElement("MontoTotal")> _
		Public Property MontoTotal As Decimal
			Get
				Return _decMontoTotal
			End Get
			Set(ByVal Value As Decimal)
				_decMontoTotal = Value
			End Set
		End Property


		<XmlElement("Dias")> _
		Public Property Dias As Short
			Get
				Return _shtDias
			End Get
			Set(ByVal Value As Short)
				_shtDias = Value
			End Set
		End Property


		<XmlElement("CodigoEstado")> _
		Public Property CodigoEstado As String
			Get
				Return _strCodigoEstado
			End Get
			Set(ByVal Value As String)
				_strCodigoEstado = Value
			End Set
		End Property


		<XmlElement("UsuarioRegistro")> _
		Public Property UsuarioRegistro As String
			Get
				Return _strUsuarioRegistro
			End Get
			Set(ByVal Value As String)
				_strUsuarioRegistro = Value
			End Set
		End Property


		<XmlElement("FecRegistro")> _
		Public Property FecRegistro As DateTime
			Get
				Return _dtmFecRegistro
			End Get
			Set(ByVal Value As DateTime)
				_dtmFecRegistro = Value
			End Set
		End Property


		<XmlElement("UsuarioModificacion")> _
		Public Property UsuarioModificacion As String
			Get
				Return _strUsuarioModificacion
			End Get
			Set(ByVal Value As String)
				_strUsuarioModificacion = Value
			End Set
		End Property


		<XmlElement("FecModificacion")> _
		Public Property FecModificacion As DateTime
			Get
				Return _dtmFecModificacion
			End Get
			Set(ByVal Value As DateTime)
				_dtmFecModificacion = Value
			End Set
		End Property

		
#End Region

End Class


''' <summary>
''' Clase que hereda de List(Of EGcc_representante) para la implementación de la lista
''' </summary>
''' <remarks></remarks>
<Serializable(), XmlRoot("ListEGCC_FraccionarCobro")> _
Public Class ListEGCC_FraccionarCobro
    Inherits List(Of EGCC_FraccionarCobro)
End Class



