Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad Gcc_cotizacionversion
''' </summary>
''' <remarks>
''' Creado Por         : TSF - ADMIN
''' Fecha de Creación  : 30/04/2012
''' </remarks>
<Serializable(), XmlRoot("EGcc_cotizacionversion")> _
Public Class EGcc_cotizacionversion

#Region " Atributos "

    Private _strCodigocotizacion As String
    Private _strVersioncotizacion As String
    Private _intCodigobanca As Nullable(Of Integer)
    Private _intCodigoestadocotizacion As Nullable(Of Integer)
    Private _intCodigoejecutivoleasing As Nullable(Of Integer)
    Private _intCodigoejecutivobanca As Nullable(Of Integer)
    Private _intCodigogrupozonal As Nullable(Of Integer)
    Private _intGenerarcarta As Nullable(Of Integer)
    Private _dtmFechacarta As Nullable(Of DateTime)
    Private _intCodigoclasificacionbien As Nullable(Of Integer)
    Private _intCodigoprocedencia As Nullable(Of Integer)
    Private _strCodigomoneda As String
    Private _intCodigotipoinmueble As Nullable(Of Integer)
    Private _decValorventa As Nullable(Of Decimal)
    Private _decIgv As Nullable(Of Decimal)
    Private _decPrecioventa As Nullable(Of Decimal)
    Private _decCuotainicialporc As Nullable(Of Decimal)
    Private _intCodigoestadobien As Nullable(Of Integer)
    Private _decImportecuotainicial As Nullable(Of Decimal)
    Private _decRiesgoneto As Nullable(Of Decimal)
    Private _intCodigotipocronogrma As Nullable(Of Integer)
    Private _intPlazograciacuota As Nullable(Of Integer)
    Private _intCodigotipograciacuota As Nullable(Of Integer)
    Private _dtmFechamaxactivacion As Nullable(Of DateTime)
    Private _intNumerocuotas As Nullable(Of Integer)
    Private _intCodigoperiodicidad As Nullable(Of Integer)
    Private _intCodigofrecuenciapago As Nullable(Of Integer)
    Private _dtmFechaprimervencimiento As Nullable(Of DateTime)
    Private _decTeaporc As Nullable(Of Decimal)
    Private _decSpreadporc As Nullable(Of Decimal)
    Private _decCostofondoporc As Nullable(Of Decimal)
    Private _decPrecuotaporc As Nullable(Of Decimal)
    Private _intPlazograciaprecuota As Nullable(Of Integer)
    Private _intCodigobientiposeguro As Nullable(Of Integer)
    Private _decBienimporteprima As Nullable(Of Decimal)
    Private _intBiennrocuotasfinanciar As Nullable(Of Integer)
    Private _intCodigodesgravamentiposeguro As Nullable(Of Integer)
    Private _decDesgravamenimporteprima As Nullable(Of Decimal)
    Private _intDesgravamennrocuotasfinanciar As Nullable(Of Integer)
    Private _decOpcioncompraporc As Nullable(Of Decimal)
    Private _decComisionactivacionporc As Nullable(Of Decimal)
    Private _decComisionestructuracionporc As Nullable(Of Decimal)
    Private _intMostrarteacartas As Nullable(Of Integer)
    Private _intMostrarmontocomision As Nullable(Of Integer)
    Private _decImporteopcioncompra As Nullable(Of Decimal)
    Private _decImportecomisionactivacion As Nullable(Of Decimal)
    Private _decImportecomisionestructuracion As Nullable(Of Decimal)
    Private _intPeriododisponible As Nullable(Of Integer)
    Private _strOtrascomisiones As String
    Private _strCorreoelectronico As String
    Private _dtmFechaingreso As Nullable(Of DateTime)
    Private _dtmVersionfecha As Nullable(Of DateTime)
    Private _dtmFechalimitevalidezcotizacion As Nullable(Of DateTime)
    Private _intCodigosupervisoraprobo As Nullable(Of Integer)
    Private _strNumerolinea As String
    Private _decTasalinea As Nullable(Of Decimal)
    Private _intNumerocronograma As Nullable(Of Integer)
    Private _intCodigotipopersona As Nullable(Of Integer)
    Private _strCodigocategoria As String
    Private _strCodigoproducto As String
    Private _intAudestadologico As Nullable(Of Integer)
    Private _dtmAudfecharegistro As Nullable(Of DateTime)
    Private _dtmAudfechamodificacion As Nullable(Of DateTime)
    Private _strAudusuarioregistro As String
    Private _strAudusuariomodificacion As String

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

    <XmlElement("Versioncotizacion")> _
    Public Property Versioncotizacion() As String
        Get
            Return Me._strVersioncotizacion
        End Get
        Set(ByVal value As String)
            Me._strVersioncotizacion = value
        End Set
    End Property

    <XmlElement("Codigobanca")> _
    Public Property Codigobanca() As Integer
        Get
            Return Me._intCodigobanca
        End Get
        Set(ByVal value As Integer)
            Me._intCodigobanca = value
        End Set
    End Property

    <XmlElement("Codigoestadocotizacion")> _
    Public Property Codigoestadocotizacion() As Integer
        Get
            Return Me._intCodigoestadocotizacion
        End Get
        Set(ByVal value As Integer)
            Me._intCodigoestadocotizacion = value
        End Set
    End Property

    <XmlElement("Codigoejecutivoleasing")> _
    Public Property Codigoejecutivoleasing() As Integer
        Get
            Return Me._intCodigoejecutivoleasing
        End Get
        Set(ByVal value As Integer)
            Me._intCodigoejecutivoleasing = value
        End Set
    End Property

    <XmlElement("Codigoejecutivobanca")> _
    Public Property Codigoejecutivobanca() As Integer
        Get
            Return Me._intCodigoejecutivobanca
        End Get
        Set(ByVal value As Integer)
            Me._intCodigoejecutivobanca = value
        End Set
    End Property

    <XmlElement("Codigogrupozonal")> _
    Public Property Codigogrupozonal() As Integer
        Get
            Return Me._intCodigogrupozonal
        End Get
        Set(ByVal value As Integer)
            Me._intCodigogrupozonal = value
        End Set
    End Property

    <XmlElement("Generarcarta")> _
    Public Property Generarcarta() As Integer
        Get
            Return Me._intGenerarcarta
        End Get
        Set(ByVal value As Integer)
            Me._intGenerarcarta = value
        End Set
    End Property

    <XmlElement("Fechacarta")> _
    Public Property Fechacarta() As DateTime
        Get
            Return Me._dtmFechacarta
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechacarta = value
        End Set
    End Property

    <XmlElement("Codigoclasificacionbien")> _
    Public Property Codigoclasificacionbien() As Integer
        Get
            Return Me._intCodigoclasificacionbien
        End Get
        Set(ByVal value As Integer)
            Me._intCodigoclasificacionbien = value
        End Set
    End Property

    <XmlElement("Codigoprocedencia")> _
    Public Property Codigoprocedencia() As Integer
        Get
            Return Me._intCodigoprocedencia
        End Get
        Set(ByVal value As Integer)
            Me._intCodigoprocedencia = value
        End Set
    End Property

    <XmlElement("Codigomoneda")> _
    Public Property Codigomoneda() As String
        Get
            Return Me._strCodigomoneda
        End Get
        Set(ByVal value As String)
            Me._strCodigomoneda = value
        End Set
    End Property

    <XmlElement("Codigotipoinmueble")> _
    Public Property Codigotipoinmueble() As Integer
        Get
            Return Me._intCodigotipoinmueble
        End Get
        Set(ByVal value As Integer)
            Me._intCodigotipoinmueble = value
        End Set
    End Property

    <XmlElement("Valorventa")> _
    Public Property Valorventa() As Decimal
        Get
            Return Me._decValorventa
        End Get
        Set(ByVal value As Decimal)
            Me._decValorventa = value
        End Set
    End Property

    <XmlElement("Igv")> _
    Public Property Igv() As Decimal
        Get
            Return Me._decIgv
        End Get
        Set(ByVal value As Decimal)
            Me._decIgv = value
        End Set
    End Property

    <XmlElement("Precioventa")> _
    Public Property Precioventa() As Decimal
        Get
            Return Me._decPrecioventa
        End Get
        Set(ByVal value As Decimal)
            Me._decPrecioventa = value
        End Set
    End Property

    <XmlElement("Cuotainicialporc")> _
    Public Property Cuotainicialporc() As Decimal
        Get
            Return Me._decCuotainicialporc
        End Get
        Set(ByVal value As Decimal)
            Me._decCuotainicialporc = value
        End Set
    End Property

    <XmlElement("Codigoestadobien")> _
    Public Property Codigoestadobien() As Integer
        Get
            Return Me._intCodigoestadobien
        End Get
        Set(ByVal value As Integer)
            Me._intCodigoestadobien = value
        End Set
    End Property

    <XmlElement("Importecuotainicial")> _
    Public Property Importecuotainicial() As Decimal
        Get
            Return Me._decImportecuotainicial
        End Get
        Set(ByVal value As Decimal)
            Me._decImportecuotainicial = value
        End Set
    End Property

    <XmlElement("Riesgoneto")> _
    Public Property Riesgoneto() As Decimal
        Get
            Return Me._decRiesgoneto
        End Get
        Set(ByVal value As Decimal)
            Me._decRiesgoneto = value
        End Set
    End Property

    <XmlElement("Codigotipocronogrma")> _
    Public Property Codigotipocronogrma() As Integer
        Get
            Return Me._intCodigotipocronogrma
        End Get
        Set(ByVal value As Integer)
            Me._intCodigotipocronogrma = value
        End Set
    End Property

    <XmlElement("Plazograciacuota")> _
    Public Property Plazograciacuota() As Integer
        Get
            Return Me._intPlazograciacuota
        End Get
        Set(ByVal value As Integer)
            Me._intPlazograciacuota = value
        End Set
    End Property

    <XmlElement("Codigotipograciacuota")> _
    Public Property Codigotipograciacuota() As Integer
        Get
            Return Me._intCodigotipograciacuota
        End Get
        Set(ByVal value As Integer)
            Me._intCodigotipograciacuota = value
        End Set
    End Property

    <XmlElement("Fechamaxactivacion")> _
    Public Property Fechamaxactivacion() As DateTime
        Get
            Return Me._dtmFechamaxactivacion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechamaxactivacion = value
        End Set
    End Property

    <XmlElement("Numerocuotas")> _
    Public Property Numerocuotas() As Integer
        Get
            Return Me._intNumerocuotas
        End Get
        Set(ByVal value As Integer)
            Me._intNumerocuotas = value
        End Set
    End Property

    <XmlElement("Codigoperiodicidad")> _
    Public Property Codigoperiodicidad() As Integer
        Get
            Return Me._intCodigoperiodicidad
        End Get
        Set(ByVal value As Integer)
            Me._intCodigoperiodicidad = value
        End Set
    End Property

    <XmlElement("Codigofrecuenciapago")> _
    Public Property Codigofrecuenciapago() As Integer
        Get
            Return Me._intCodigofrecuenciapago
        End Get
        Set(ByVal value As Integer)
            Me._intCodigofrecuenciapago = value
        End Set
    End Property

    <XmlElement("Fechaprimervencimiento")> _
    Public Property Fechaprimervencimiento() As DateTime
        Get
            Return Me._dtmFechaprimervencimiento
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechaprimervencimiento = value
        End Set
    End Property

    <XmlElement("Teaporc")> _
    Public Property Teaporc() As Decimal
        Get
            Return Me._decTeaporc
        End Get
        Set(ByVal value As Decimal)
            Me._decTeaporc = value
        End Set
    End Property

    <XmlElement("Spreadporc")> _
    Public Property Spreadporc() As Decimal
        Get
            Return Me._decSpreadporc
        End Get
        Set(ByVal value As Decimal)
            Me._decSpreadporc = value
        End Set
    End Property

    <XmlElement("Costofondoporc")> _
    Public Property Costofondoporc() As Decimal
        Get
            Return Me._decCostofondoporc
        End Get
        Set(ByVal value As Decimal)
            Me._decCostofondoporc = value
        End Set
    End Property

    <XmlElement("Precuotaporc")> _
    Public Property Precuotaporc() As Decimal
        Get
            Return Me._decPrecuotaporc
        End Get
        Set(ByVal value As Decimal)
            Me._decPrecuotaporc = value
        End Set
    End Property

    <XmlElement("Plazograciaprecuota")> _
    Public Property Plazograciaprecuota() As Integer
        Get
            Return Me._intPlazograciaprecuota
        End Get
        Set(ByVal value As Integer)
            Me._intPlazograciaprecuota = value
        End Set
    End Property

    <XmlElement("Codigobientiposeguro")> _
    Public Property Codigobientiposeguro() As Integer
        Get
            Return Me._intCodigobientiposeguro
        End Get
        Set(ByVal value As Integer)
            Me._intCodigobientiposeguro = value
        End Set
    End Property

    <XmlElement("Bienimporteprima")> _
    Public Property Bienimporteprima() As Decimal
        Get
            Return Me._decBienimporteprima
        End Get
        Set(ByVal value As Decimal)
            Me._decBienimporteprima = value
        End Set
    End Property

    <XmlElement("Biennrocuotasfinanciar")> _
    Public Property Biennrocuotasfinanciar() As Integer
        Get
            Return Me._intBiennrocuotasfinanciar
        End Get
        Set(ByVal value As Integer)
            Me._intBiennrocuotasfinanciar = value
        End Set
    End Property

    <XmlElement("Codigodesgravamentiposeguro")> _
    Public Property Codigodesgravamentiposeguro() As Integer
        Get
            Return Me._intCodigodesgravamentiposeguro
        End Get
        Set(ByVal value As Integer)
            Me._intCodigodesgravamentiposeguro = value
        End Set
    End Property

    <XmlElement("Desgravamenimporteprima")> _
    Public Property Desgravamenimporteprima() As Decimal
        Get
            Return Me._decDesgravamenimporteprima
        End Get
        Set(ByVal value As Decimal)
            Me._decDesgravamenimporteprima = value
        End Set
    End Property

    <XmlElement("Desgravamennrocuotasfinanciar")> _
    Public Property Desgravamennrocuotasfinanciar() As Integer
        Get
            Return Me._intDesgravamennrocuotasfinanciar
        End Get
        Set(ByVal value As Integer)
            Me._intDesgravamennrocuotasfinanciar = value
        End Set
    End Property

    <XmlElement("Opcioncompraporc")> _
    Public Property Opcioncompraporc() As Decimal
        Get
            Return Me._decOpcioncompraporc
        End Get
        Set(ByVal value As Decimal)
            Me._decOpcioncompraporc = value
        End Set
    End Property

    <XmlElement("Comisionactivacionporc")> _
    Public Property Comisionactivacionporc() As Decimal
        Get
            Return Me._decComisionactivacionporc
        End Get
        Set(ByVal value As Decimal)
            Me._decComisionactivacionporc = value
        End Set
    End Property

    <XmlElement("Comisionestructuracionporc")> _
    Public Property Comisionestructuracionporc() As Decimal
        Get
            Return Me._decComisionestructuracionporc
        End Get
        Set(ByVal value As Decimal)
            Me._decComisionestructuracionporc = value
        End Set
    End Property

    <XmlElement("Mostrarteacartas")> _
    Public Property Mostrarteacartas() As Integer
        Get
            Return Me._intMostrarteacartas
        End Get
        Set(ByVal value As Integer)
            Me._intMostrarteacartas = value
        End Set
    End Property

    <XmlElement("Mostrarmontocomision")> _
    Public Property Mostrarmontocomision() As Integer
        Get
            Return Me._intMostrarmontocomision
        End Get
        Set(ByVal value As Integer)
            Me._intMostrarmontocomision = value
        End Set
    End Property

    <XmlElement("Importeopcioncompra")> _
    Public Property Importeopcioncompra() As Decimal
        Get
            Return Me._decImporteopcioncompra
        End Get
        Set(ByVal value As Decimal)
            Me._decImporteopcioncompra = value
        End Set
    End Property

    <XmlElement("Importecomisionactivacion")> _
    Public Property Importecomisionactivacion() As Decimal
        Get
            Return Me._decImportecomisionactivacion
        End Get
        Set(ByVal value As Decimal)
            Me._decImportecomisionactivacion = value
        End Set
    End Property

    <XmlElement("Importecomisionestructuracion")> _
    Public Property Importecomisionestructuracion() As Decimal
        Get
            Return Me._decImportecomisionestructuracion
        End Get
        Set(ByVal value As Decimal)
            Me._decImportecomisionestructuracion = value
        End Set
    End Property

    <XmlElement("Periododisponible")> _
    Public Property Periododisponible() As Integer
        Get
            Return Me._intPeriododisponible
        End Get
        Set(ByVal value As Integer)
            Me._intPeriododisponible = value
        End Set
    End Property

    <XmlElement("Otrascomisiones")> _
    Public Property Otrascomisiones() As String
        Get
            Return Me._strOtrascomisiones
        End Get
        Set(ByVal value As String)
            Me._strOtrascomisiones = value
        End Set
    End Property

    <XmlElement("Correoelectronico")> _
    Public Property Correoelectronico() As String
        Get
            Return Me._strCorreoelectronico
        End Get
        Set(ByVal value As String)
            Me._strCorreoelectronico = value
        End Set
    End Property

    <XmlElement("Fechaingreso")> _
    Public Property Fechaingreso() As DateTime
        Get
            Return Me._dtmFechaingreso
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechaingreso = value
        End Set
    End Property

    <XmlElement("Versionfecha")> _
    Public Property Versionfecha() As DateTime
        Get
            Return Me._dtmVersionfecha
        End Get
        Set(ByVal value As DateTime)
            Me._dtmVersionfecha = value
        End Set
    End Property

    <XmlElement("Fechalimitevalidezcotizacion")> _
    Public Property Fechalimitevalidezcotizacion() As DateTime
        Get
            Return Me._dtmFechalimitevalidezcotizacion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmFechalimitevalidezcotizacion = value
        End Set
    End Property

    <XmlElement("Codigosupervisoraprobo")> _
    Public Property Codigosupervisoraprobo() As Integer
        Get
            Return Me._intCodigosupervisoraprobo
        End Get
        Set(ByVal value As Integer)
            Me._intCodigosupervisoraprobo = value
        End Set
    End Property

    <XmlElement("Numerolinea")> _
    Public Property Numerolinea() As String
        Get
            Return Me._strNumerolinea
        End Get
        Set(ByVal value As String)
            Me._strNumerolinea = value
        End Set
    End Property

    <XmlElement("Tasalinea")> _
    Public Property Tasalinea() As Decimal
        Get
            Return Me._decTasalinea
        End Get
        Set(ByVal value As Decimal)
            Me._decTasalinea = value
        End Set
    End Property

    <XmlElement("Numerocronograma")> _
    Public Property Numerocronograma() As Integer
        Get
            Return Me._intNumerocronograma
        End Get
        Set(ByVal value As Integer)
            Me._intNumerocronograma = value
        End Set
    End Property

    <XmlElement("Codigotipopersona")> _
    Public Property Codigotipopersona() As Integer
        Get
            Return Me._intCodigotipopersona
        End Get
        Set(ByVal value As Integer)
            Me._intCodigotipopersona = value
        End Set
    End Property

    <XmlElement("Codigocategoria")> _
    Public Property Codigocategoria() As String
        Get
            Return Me._strCodigocategoria
        End Get
        Set(ByVal value As String)
            Me._strCodigocategoria = value
        End Set
    End Property

    <XmlElement("Codigoproducto")> _
    Public Property Codigoproducto() As String
        Get
            Return Me._strCodigoproducto
        End Get
        Set(ByVal value As String)
            Me._strCodigoproducto = value
        End Set
    End Property

    <XmlElement("Audestadologico")> _
    Public Property Audestadologico() As Integer
        Get
            Return Me._intAudestadologico
        End Get
        Set(ByVal value As Integer)
            Me._intAudestadologico = value
        End Set
    End Property

    <XmlElement("Audfecharegistro")> _
    Public Property Audfecharegistro() As DateTime
        Get
            Return Me._dtmAudfecharegistro
        End Get
        Set(ByVal value As DateTime)
            Me._dtmAudfecharegistro = value
        End Set
    End Property

    <XmlElement("Audfechamodificacion")> _
    Public Property Audfechamodificacion() As DateTime
        Get
            Return Me._dtmAudfechamodificacion
        End Get
        Set(ByVal value As DateTime)
            Me._dtmAudfechamodificacion = value
        End Set
    End Property

    <XmlElement("Audusuarioregistro")> _
    Public Property Audusuarioregistro() As String
        Get
            Return Me._strAudusuarioregistro
        End Get
        Set(ByVal value As String)
            Me._strAudusuarioregistro = value
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