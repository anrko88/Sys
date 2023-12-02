Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Solicitudcreditoestructuracarac
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("ESolicitudcreditoestructuracarac")> _
Public Class ESolicitudcreditoestructuracarac

#Region " Atributos "

    Private _strCodsolicitudcredito As String
    Private _intSecfinanciamiento As Nullable(Of Integer)
    Private _strTiporubrofinanciamiento As String
    Private _strTipoproducto As String
    Private _strClase As String
    Private _strMarca As String
    Private _strModelo As String
    Private _strCarroceria As String
    Private _strNombre As String
    Private _intAnio As Nullable(Of Integer)
    Private _strColor As String
    Private _strNroserie As String
    Private _strNromotor As String
    Private _strPlaca As String
    Private _dtmFecharegistro As Nullable(Of DateTime)
    Private _strTipodireccion As String
    Private _strDireccion As String
    Private _strMzlote As String
    Private _intNumero As Nullable(Of Integer)
    Private _strDistrito As String
    Private _strProvincia As String
    Private _strDepartamento As String
    Private _strExtension As String
    Private _strAreaconstruida As String
    Private _strPertenece As String
    Private _strNombrepertenece As String
    Private _strComentario As String
    Private _dtmFechatransferencia As Nullable(Of DateTime)
    Private _strUsuariotransferencia As String
    Private _dtmFechaculminacion As Nullable(Of DateTime)
    Private _strSuministroluz As String
    Private _strSuministroagua As String
    Private _strEmpprestadoraluz As String
    Private _strEmpprestadoraagua As String
    Private _strCodtipoinmueble As String
    Private _dtmFecenvioinscripcion As Nullable(Of DateTime)
    Private _strUrbanizacion As String
    Private _strCodtipovehiculo As String
    Private _intIdtaller As Nullable(Of Integer)
    Private _decPorcentajerecaudo As Nullable(Of Decimal)
    Private _strPlacaanterior As String
    Private _strCodconcesionario As String
    Private _strEstadocofigas As String
    Private _dtmFeccomsuministroagua As Nullable(Of DateTime)
    Private _dtmFeccomsuministroluz As Nullable(Of DateTime)
    Private _strPlacaantigua As String
    Private _strPartidaRegistral As String
    Private _strOficinaRegistral As String
    Private _strMedidas As String
    Private _strAudusuariomodificacion As String
    Private _strCodigoPredio As String


    '============================================='
    'INAFECTACIÓN
    '============================================='

    Private _dtmFecEnvioCarta As Nullable(Of DateTime)
    Private _dtmFecRecepcionDocumento As Nullable(Of DateTime)
    Private _dtmFecPresentacionSat As Nullable(Of DateTime)
    Private _strFechaNotificacion As Nullable(Of DateTime)
    Private _strNroResolucion As String
    Private _strPeriodo As String
    Private _strEstadoResolucion As String
    Private _strEstadoInafectacion As String
    Private _strCodigoInafectacion As String

    '============================================='
    'INSCRIPCION MUNICIPAL
    '============================================='

    Private _strActo As String
    Private _strAsientoRegistral As String
    Private _strEstadoInscripcion As String
    Private _strCodigoInscripcion As String


#End Region

#Region " Propiedades "

    <XmlElement("FechaNotificacion")> _
Public Property FechaNotificacion() As Nullable(Of DateTime)
        Get
            Return _strFechaNotificacion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            _strFechaNotificacion = value
        End Set
    End Property
    <XmlElement("CodigoInscripcion")> _
Public Property CodigoInscripcion() As String
        Get
            Return Me._strCodigoInscripcion
        End Get
        Set(ByVal value As String)
            Me._strCodigoInscripcion = value
        End Set
    End Property

    <XmlElement("EstadoInscripcion")> _
Public Property EstadoInscripcion() As String
        Get
            Return Me._strEstadoInscripcion
        End Get
        Set(ByVal value As String)
            Me._strEstadoInscripcion = value
        End Set
    End Property

    <XmlElement("AsientoRegistral")> _
Public Property AsientoRegistral() As String
        Get
            Return Me._strAsientoRegistral
        End Get
        Set(ByVal value As String)
            Me._strAsientoRegistral = value
        End Set
    End Property

    <XmlElement("Acto")> _
 Public Property Acto() As String
        Get
            Return Me._strActo
        End Get
        Set(ByVal value As String)
            Me._strActo = value
        End Set
    End Property

    <XmlElement("FecPresentacionSat")> _
  Public Property FecPresentacionSat() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecPresentacionSat
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecPresentacionSat = value
        End Set
    End Property

    <XmlElement("FecRecepcionDocumento")> _
   Public Property FecRecepcionDocumento() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecRecepcionDocumento
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecRecepcionDocumento = value
        End Set
    End Property
    <XmlElement("FecEnvioCarta")> _
    Public Property FecEnvioCarta() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecEnvioCarta
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecEnvioCarta = value
        End Set
    End Property

    <XmlElement("NroResolucion")> _
 Public Property NroResolucion() As String
        Get
            Return Me._strNroResolucion
        End Get
        Set(ByVal value As String)
            Me._strNroResolucion = value
        End Set
    End Property
    <XmlElement("CodInafectacion")> _
Public Property CodInafectacion() As String
        Get
            Return Me._strCodigoInafectacion
        End Get
        Set(ByVal value As String)
            Me._strCodigoInafectacion = value
        End Set
    End Property
    <XmlElement("EstadoResolucion")> _
Public Property EstadoResolucion() As String
        Get
            Return Me._strEstadoResolucion
        End Get
        Set(ByVal value As String)
            Me._strEstadoResolucion = value
        End Set
    End Property
    <XmlElement("Periodo")> _
Public Property Periodo() As String
        Get
            Return Me._strPeriodo
        End Get
        Set(ByVal value As String)
            Me._strPeriodo = value
        End Set
    End Property
    <XmlElement("EstadoInafectacion")> _
Public Property EstadoInafectacion() As String
        Get
            Return Me._strEstadoInafectacion
        End Get
        Set(ByVal value As String)
            Me._strEstadoInafectacion = value
        End Set
    End Property

    <XmlElement("CodigoPredio")> _
   Public Property CodigoPredio() As String
        Get
            Return Me._strCodigoPredio
        End Get
        Set(ByVal value As String)
            Me._strCodigoPredio = value
        End Set
    End Property

    <XmlElement("Codsolicitudcredito")> _
    Public Property Codsolicitudcredito() As String
        Get
            Return Me._strCodsolicitudcredito
        End Get
        Set(ByVal value As String)
            Me._strCodsolicitudcredito = value
        End Set
    End Property

    <XmlElement("Secfinanciamiento")> _
    Public Property Secfinanciamiento() As Nullable(Of Integer)
        Get
            Return Me._intSecfinanciamiento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intSecfinanciamiento = value
        End Set
    End Property

    <XmlElement("Tiporubrofinanciamiento")> _
    Public Property Tiporubrofinanciamiento() As String
        Get
            Return Me._strTiporubrofinanciamiento
        End Get
        Set(ByVal value As String)
            Me._strTiporubrofinanciamiento = value
        End Set
    End Property

    <XmlElement("Tipoproducto")> _
    Public Property Tipoproducto() As String
        Get
            Return Me._strTipoproducto
        End Get
        Set(ByVal value As String)
            Me._strTipoproducto = value
        End Set
    End Property

    <XmlElement("Clase")> _
    Public Property Clase() As String
        Get
            Return Me._strClase
        End Get
        Set(ByVal value As String)
            Me._strClase = value
        End Set
    End Property

    <XmlElement("Marca")> _
    Public Property Marca() As String
        Get
            Return Me._strMarca
        End Get
        Set(ByVal value As String)
            Me._strMarca = value
        End Set
    End Property

    <XmlElement("Modelo")> _
    Public Property Modelo() As String
        Get
            Return Me._strModelo
        End Get
        Set(ByVal value As String)
            Me._strModelo = value
        End Set
    End Property

    <XmlElement("Carroceria")> _
    Public Property Carroceria() As String
        Get
            Return Me._strCarroceria
        End Get
        Set(ByVal value As String)
            Me._strCarroceria = value
        End Set
    End Property

    <XmlElement("Nombre")> _
    Public Property Nombre() As String
        Get
            Return Me._strNombre
        End Get
        Set(ByVal value As String)
            Me._strNombre = value
        End Set
    End Property

    <XmlElement("Anio")> _
    Public Property Anio() As Nullable(Of Integer)
        Get
            Return Me._intAnio
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intAnio = value
        End Set
    End Property

    <XmlElement("Color")> _
    Public Property Color() As String
        Get
            Return Me._strColor
        End Get
        Set(ByVal value As String)
            Me._strColor = value
        End Set
    End Property

    <XmlElement("Nroserie")> _
    Public Property Nroserie() As String
        Get
            Return Me._strNroserie
        End Get
        Set(ByVal value As String)
            Me._strNroserie = value
        End Set
    End Property

    <XmlElement("Nromotor")> _
    Public Property Nromotor() As String
        Get
            Return Me._strNromotor
        End Get
        Set(ByVal value As String)
            Me._strNromotor = value
        End Set
    End Property

    <XmlElement("Placa")> _
    Public Property Placa() As String
        Get
            Return Me._strPlaca
        End Get
        Set(ByVal value As String)
            Me._strPlaca = value
        End Set
    End Property

    <XmlElement("Fecharegistro")> _
    Public Property Fecharegistro() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecharegistro
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecharegistro = value
        End Set
    End Property

    <XmlElement("Tipodireccion")> _
    Public Property Tipodireccion() As String
        Get
            Return Me._strTipodireccion
        End Get
        Set(ByVal value As String)
            Me._strTipodireccion = value
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

    <XmlElement("Mzlote")> _
    Public Property Mzlote() As String
        Get
            Return Me._strMzlote
        End Get
        Set(ByVal value As String)
            Me._strMzlote = value
        End Set
    End Property

    <XmlElement("Numero")> _
    Public Property Numero() As Nullable(Of Integer)
        Get
            Return Me._intNumero
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intNumero = value
        End Set
    End Property

    <XmlElement("Distrito")> _
    Public Property Distrito() As String
        Get
            Return Me._strDistrito
        End Get
        Set(ByVal value As String)
            Me._strDistrito = value
        End Set
    End Property

    <XmlElement("Provincia")> _
    Public Property Provincia() As String
        Get
            Return Me._strProvincia
        End Get
        Set(ByVal value As String)
            Me._strProvincia = value
        End Set
    End Property

    <XmlElement("Departamento")> _
    Public Property Departamento() As String
        Get
            Return Me._strDepartamento
        End Get
        Set(ByVal value As String)
            Me._strDepartamento = value
        End Set
    End Property

    <XmlElement("Extension")> _
    Public Property Extension() As String
        Get
            Return Me._strExtension
        End Get
        Set(ByVal value As String)
            Me._strExtension = value
        End Set
    End Property

    <XmlElement("Areaconstruida")> _
    Public Property Areaconstruida() As String
        Get
            Return Me._strAreaconstruida
        End Get
        Set(ByVal value As String)
            Me._strAreaconstruida = value
        End Set
    End Property

    <XmlElement("Pertenece")> _
    Public Property Pertenece() As String
        Get
            Return Me._strPertenece
        End Get
        Set(ByVal value As String)
            Me._strPertenece = value
        End Set
    End Property

    <XmlElement("Nombrepertenece")> _
    Public Property Nombrepertenece() As String
        Get
            Return Me._strNombrepertenece
        End Get
        Set(ByVal value As String)
            Me._strNombrepertenece = value
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

    <XmlElement("Fechatransferencia")> _
    Public Property Fechatransferencia() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechatransferencia
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechatransferencia = value
        End Set
    End Property

    <XmlElement("Usuariotransferencia")> _
    Public Property Usuariotransferencia() As String
        Get
            Return Me._strUsuariotransferencia
        End Get
        Set(ByVal value As String)
            Me._strUsuariotransferencia = value
        End Set
    End Property

    <XmlElement("Fechaculminacion")> _
    Public Property Fechaculminacion() As Nullable(Of DateTime)
        Get
            Return Me._dtmFechaculminacion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFechaculminacion = value
        End Set
    End Property

    <XmlElement("Suministroluz")> _
    Public Property Suministroluz() As String
        Get
            Return Me._strSuministroluz
        End Get
        Set(ByVal value As String)
            Me._strSuministroluz = value
        End Set
    End Property

    <XmlElement("Suministroagua")> _
    Public Property Suministroagua() As String
        Get
            Return Me._strSuministroagua
        End Get
        Set(ByVal value As String)
            Me._strSuministroagua = value
        End Set
    End Property

    <XmlElement("Empprestadoraluz")> _
    Public Property Empprestadoraluz() As String
        Get
            Return Me._strEmpprestadoraluz
        End Get
        Set(ByVal value As String)
            Me._strEmpprestadoraluz = value
        End Set
    End Property

    <XmlElement("Empprestadoraagua")> _
    Public Property Empprestadoraagua() As String
        Get
            Return Me._strEmpprestadoraagua
        End Get
        Set(ByVal value As String)
            Me._strEmpprestadoraagua = value
        End Set
    End Property

    <XmlElement("Codtipoinmueble")> _
    Public Property Codtipoinmueble() As String
        Get
            Return Me._strCodtipoinmueble
        End Get
        Set(ByVal value As String)
            Me._strCodtipoinmueble = value
        End Set
    End Property

    <XmlElement("Fecenvioinscripcion")> _
    Public Property Fecenvioinscripcion() As Nullable(Of DateTime)
        Get
            Return Me._dtmFecenvioinscripcion
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFecenvioinscripcion = value
        End Set
    End Property

    <XmlElement("Urbanizacion")> _
    Public Property Urbanizacion() As String
        Get
            Return Me._strUrbanizacion
        End Get
        Set(ByVal value As String)
            Me._strUrbanizacion = value
        End Set
    End Property

    <XmlElement("Codtipovehiculo")> _
    Public Property Codtipovehiculo() As String
        Get
            Return Me._strCodtipovehiculo
        End Get
        Set(ByVal value As String)
            Me._strCodtipovehiculo = value
        End Set
    End Property

    <XmlElement("Idtaller")> _
    Public Property Idtaller() As Nullable(Of Integer)
        Get
            Return Me._intIdtaller
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me._intIdtaller = value
        End Set
    End Property

    <XmlElement("Porcentajerecaudo")> _
    Public Property Porcentajerecaudo() As Nullable(Of Decimal)
        Get
            Return Me._decPorcentajerecaudo
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            Me._decPorcentajerecaudo = value
        End Set
    End Property

    <XmlElement("Placaanterior")> _
    Public Property Placaanterior() As String
        Get
            Return Me._strPlacaanterior
        End Get
        Set(ByVal value As String)
            Me._strPlacaanterior = value
        End Set
    End Property

    <XmlElement("Codconcesionario")> _
    Public Property Codconcesionario() As String
        Get
            Return Me._strCodconcesionario
        End Get
        Set(ByVal value As String)
            Me._strCodconcesionario = value
        End Set
    End Property

    <XmlElement("Estadocofigas")> _
    Public Property Estadocofigas() As String
        Get
            Return Me._strEstadocofigas
        End Get
        Set(ByVal value As String)
            Me._strEstadocofigas = value
        End Set
    End Property

    <XmlElement("Feccomsuministroagua")> _
    Public Property Feccomsuministroagua() As Nullable(Of DateTime)
        Get
            Return Me._dtmFeccomsuministroagua
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFeccomsuministroagua = value
        End Set
    End Property

    <XmlElement("Feccomsuministroluz")> _
    Public Property Feccomsuministroluz() As Nullable(Of DateTime)
        Get
            Return Me._dtmFeccomsuministroluz
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            Me._dtmFeccomsuministroluz = value
        End Set
    End Property

    <XmlElement("Placaantigua")> _
    Public Property Placaantigua() As String
        Get
            Return Me._strPlacaantigua
        End Get
        Set(ByVal value As String)
            Me._strPlacaantigua = value
        End Set
    End Property

    <XmlElement("PartidaRegistral")> _
    Public Property PartidaRegistral() As String
        Get
            Return _strPartidaRegistral
        End Get
        Set(ByVal value As String)
            _strPartidaRegistral = value
        End Set
    End Property

    <XmlElement("OficinaRegistral")> _
    Public Property OficinaRegistral() As String
        Get
            Return _strOficinaRegistral
        End Get
        Set(ByVal value As String)
            _strOficinaRegistral = value
        End Set
    End Property

    <XmlElement("Medidas")> _
    Public Property Medidas() As String
        Get
            Return _strMedidas
        End Get
        Set(ByVal value As String)
            _strMedidas = value
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

#End Region

End Class