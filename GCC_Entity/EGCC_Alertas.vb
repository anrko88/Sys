Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_carta
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGCC_Alertas")> _
Public Class EGCC_Alertas

#Region " Atributos "

    Private _strPeriodo As String
    Private _strFechaActual As String
    Private _strRazonSocial As String
    Private _strDireccion As String
    Private _strDistrito As String
    Private _strProvincia As String
    Private _strDepartamento As String
    Private _strNumContrato As String
    Private _strFechaPago As String
    Private _strImporte As String
    Private _strSimMoneda As String
    Private _strMonto As String
    Private _strComision As String
    Private _strTipoCambio As String
    Private _strIgv As String
    Private _strFechaCobro As String
    Private _strTablaDetalle As String
    Private _strRutaWeb As String



    Private _strNroInfraccion As String
    Private _strFecInfraccion As String
    Private _strTipoInfraccion As String
    Private _strNroPlaca As String
    Private _strImporteMulta As String
    Private _strFechaActivacion As String



    Private _strEmpresaTasador As String
    Private _strTasador As String
    Private _strTelefono As String
    Private _strCelular As String
    Private _strCorreo As String

    Private _intTipo As Integer
    Private _strAudUsuarioRegistro As String
    Private _strCodTasador As String

    Private _strNroLote As String
    Private _strTipoConcepto As String

#End Region

#Region " Propiedades "

    <XmlElement("Periodo")> _
    Public Property Periodo() As String
        Get
            Return Me._strPeriodo
        End Get
        Set(ByVal value As String)
            Me._strPeriodo = value
        End Set
    End Property

    <XmlElement("FechaActual")> _
    Public Property FechaActual() As String
        Get
            Return Me._strFechaActual
        End Get
        Set(ByVal value As String)
            Me._strFechaActual = value
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

    <XmlElement("Direccion")> _
    Public Property Direccion() As String
        Get
            Return Me._strDireccion
        End Get
        Set(ByVal value As String)
            Me._strDireccion = value
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

    <XmlElement("NumContrato")> _
    Public Property NumContrato() As String
        Get
            Return Me._strNumContrato
        End Get
        Set(ByVal value As String)
            Me._strNumContrato = value
        End Set
    End Property

    <XmlElement("FechaPago")> _
    Public Property FechaPago() As String
        Get
            Return Me._strFechaPago
        End Get
        Set(ByVal value As String)
            Me._strFechaPago = value
        End Set
    End Property

    <XmlElement("Importe")> _
    Public Property Importe() As String
        Get
            Return Me._strImporte
        End Get
        Set(ByVal value As String)
            Me._strImporte = value
        End Set
    End Property

    <XmlElement("SimMoneda")> _
    Public Property SimMoneda() As String
        Get
            Return Me._strSimMoneda
        End Get
        Set(ByVal value As String)
            Me._strSimMoneda = value
        End Set
    End Property

    <XmlElement("Monto")> _
    Public Property Monto() As String
        Get
            Return Me._strMonto
        End Get
        Set(ByVal value As String)
            Me._strMonto = value
        End Set
    End Property

    <XmlElement("Comision")> _
    Public Property Comision() As String
        Get
            Return Me._strComision
        End Get
        Set(ByVal value As String)
            Me._strComision = value
        End Set
    End Property

    <XmlElement("TipoCambio")> _
    Public Property TipoCambio() As String
        Get
            Return Me._strTipoCambio
        End Get
        Set(ByVal value As String)
            Me._strTipoCambio = value
        End Set
    End Property

    <XmlElement("Igv")> _
    Public Property Igv() As String
        Get
            Return Me._strIgv
        End Get
        Set(ByVal value As String)
            Me._strIgv = value
        End Set
    End Property

    <XmlElement("FechaCobro")> _
    Public Property FechaCobro() As String
        Get
            Return Me._strFechaCobro
        End Get
        Set(ByVal value As String)
            Me._strFechaCobro = value
        End Set
    End Property


    <XmlElement("TablaDetalle")> _
    Public Property TablaDetalle() As String
        Get
            Return Me._strTablaDetalle
        End Get
        Set(ByVal value As String)
            Me._strTablaDetalle = value
        End Set
    End Property

    <XmlElement("RutaWeb")> _
    Public Property RutaWeb() As String
        Get
            Return Me._strRutaWeb
        End Get
        Set(ByVal value As String)
            Me._strRutaWeb = value
        End Set
    End Property

    <XmlElement("NroInfraccion")> _
    Public Property NroInfraccion() As String
        Get
            Return Me._strNroInfraccion
        End Get
        Set(ByVal value As String)
            Me._strNroInfraccion = value
        End Set
    End Property

    <XmlElement("FecInfraccion")> _
    Public Property FecInfraccion() As String
        Get
            Return Me._strFecInfraccion
        End Get
        Set(ByVal value As String)
            Me._strFecInfraccion = value
        End Set
    End Property

    <XmlElement("TipoInfraccion")> _
    Public Property TipoInfraccion() As String
        Get
            Return Me._strTipoInfraccion
        End Get
        Set(ByVal value As String)
            Me._strTipoInfraccion = value
        End Set
    End Property

    <XmlElement("NroPlaca")> _
    Public Property NroPlaca() As String
        Get
            Return Me._strNroPlaca
        End Get
        Set(ByVal value As String)
            Me._strNroPlaca = value
        End Set
    End Property

    <XmlElement("ImporteMulta")> _
    Public Property ImporteMulta() As String
        Get
            Return Me._strImporteMulta
        End Get
        Set(ByVal value As String)
            Me._strImporteMulta = value
        End Set
    End Property


    <XmlElement("EmpresaTasador")> _
    Public Property EmpresaTasador() As String
        Get
            Return Me._strEmpresaTasador
        End Get
        Set(ByVal value As String)
            Me._strEmpresaTasador = value
        End Set
    End Property

    <XmlElement("Tasador")> _
    Public Property Tasador() As String
        Get
            Return Me._strTasador
        End Get
        Set(ByVal value As String)
            Me._strTasador = value
        End Set
    End Property

    <XmlElement("Telefono")> _
    Public Property Telefono() As String
        Get
            Return Me._strTelefono
        End Get
        Set(ByVal value As String)
            Me._strTelefono = value
        End Set
    End Property

    <XmlElement("Correo")> _
    Public Property Correo() As String
        Get
            Return Me._strCorreo
        End Get
        Set(ByVal value As String)
            Me._strCorreo = value
        End Set
    End Property


    <XmlElement("Celular")> _
    Public Property Celular() As String
        Get
            Return Me._strCelular
        End Get
        Set(ByVal value As String)
            Me._strCelular = value
        End Set
    End Property


    <XmlElement("FechaActivacion")> _
    Public Property FechaActivacion() As String
        Get
            Return Me._strFechaActivacion
        End Get
        Set(ByVal value As String)
            Me._strFechaActivacion = value
        End Set
    End Property


    <XmlElement("Tipo")> _
    Public Property Tipo() As Integer
        Get
            Return Me._intTipo
        End Get
        Set(ByVal value As Integer)
            Me._intTipo = value
        End Set
    End Property

    <XmlElement("AudUsuarioRegistro")> _
    Public Property AudUsuarioRegistro() As String
        Get
            Return Me._strAudUsuarioRegistro
        End Get
        Set(ByVal value As String)
            Me._strAudUsuarioRegistro = value
        End Set
    End Property

    <XmlElement("CodTasador")> _
    Public Property CodTasador() As String
        Get
            Return Me._strCodTasador
        End Get
        Set(ByVal value As String)
            Me._strCodTasador = value
        End Set
    End Property

    <XmlElement("NroLote")> _
    Public Property NroLote() As String
        Get
            Return Me._strNroLote
        End Get
        Set(ByVal value As String)
            Me._strNroLote = value
        End Set
    End Property

    <XmlElement("TipoConcepto")> _
    Public Property TipoConcepto() As String
        Get
            Return Me._strTipoConcepto
        End Get
        Set(ByVal value As String)
            Me._strTipoConcepto = value
        End Set
    End Property

#End Region


End Class
