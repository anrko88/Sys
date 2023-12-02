Imports GCC.UI
Imports System.Data
Imports System.Web.Services
Imports System.Collections.Generic

Imports GCC.Entity
Imports GCC.LogicWS

Imports lpcCronograma

Partial Class Cotizacion_frmCotizacionRegistro
    Inherits GCCBase

    Dim objLog As New GCCLog("frmCotizacionRegistro.aspx.vb")
    'Inicio AAE - Declaro clase para llamar método de instance
    Private Shared ReadOnly oGCC_Cotizacion_frmCotizacionRegistro As New GCC_Cotizacion_frmCotizacionRegistro
    'Fin AAE 
#Region "Eventos"

    ''' <summary>
    ''' Evento al cargar la Página
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 22/02/2011
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            objLog.escribe("DEBUG", "Metodo Load de la página", "Page_Load")

            'Valida Sesión
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                objLog.escribe("ERROR", "Usuario Sesión no encontrado. Re-dirigido al logueo.", "Page_Load")
                Throw New ApplicationException("Su sesión ha caducado, por favor vuelva a ingresar al sistema.")
            End If

            If Not Page.IsPostBack Then

                'Inicializa 
                Me.lblOperacion.InnerHtml = GCCConstante.C_TX_LABEL_NUEVO
                Me.hddTipoTransaccion.Value = GCCConstante.C_TX_NUEVO
                InicializaCombos()

                'Path Archivo
                Me.hddPathArchivo.Value = GCCUtilitario.fstrObtieneKeyWebConfig("FileServer")

                'Eventos
                txtCUCliente.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + imgBsqClienteRM.UniqueID + "').click();return false;}} else {return true}; ")
                'Inicio IBK
                txtNumeroDocumento.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + imgBsqClienteRM.UniqueID + "').click();return false;}} else {return true}; ")
                'Fin IBK
                'Fecha Hoy
                Dim dtFecha As Date = Now
                Me.hddFechaActual.Value = dtFecha.ToString("dd/MM/yyyy")

                Dim dtFechaFormat As Date = Now
                'dtFechaFormat = dtFecha.ToString("dd/MM/yyyy")
                dtFechaFormat = dtFechaFormat.AddDays(30)
                Me.txtFechaOfertaValida.Value = Format(dtFechaFormat, "dd/MM/yyyy")

                'Limpia Cronograma
                HttpContext.Current.Session("DTB_CRONOGRAMA") = Nothing

                'Verifica Edicion
                Dim strCodigoCotizacion As String = Request.QueryString("hddCodigoCotizacion")
                If Not strCodigoCotizacion Is Nothing Then
                    GestionBloqueo(strCodigoCotizacion)
                    Me.lblOperacion.InnerHtml = GCCConstante.C_TX_LABEL_EDITAR
                    Me.hddTipoTransaccion.Value = GCCConstante.C_TX_EDITAR
                    CargaEditarCotizacion(strCodigoCotizacion)
                Else
                    GCCUtilitario.CargarComboValorGenericoAnidado(Me.cmbFrecuenciaPago, GCCConstante.C_TABLAGENERICA_FRECUENCIA_PAGO, GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_MENSUAL)
                End If

                'IBK - RPH: Obtengo el codigo del perfil
                If Not String.IsNullOrEmpty(GCCSession.PerfilUsuario) Then
                    hidPerfil.Value = GCCSession.PerfilUsuario
                End If
                'Fin

            End If
        Catch ex As ApplicationException
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            If String.IsNullOrEmpty(GCCSession.CodigoUsuario) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "scriptSalir", "parent.fn_mensajeErrorUsuario('" & ex.Message & "','" & GCCUtilitario.fstrObtieneKeyWebConfig("PaginaInicio") & "')", True)
            Else
                GCCUtilitario.ShowLoad(ex.Message, Me)
            End If
        Catch ex As Exception
            objLog.escribe("FATAL", "Excepcion : " & ex.Message, "Page_Load")
            GCCUtilitario.ShowLoad("ERROR => " + ex.Message, Me)
        End Try

    End Sub

#End Region

#Region "WebMethods"

    ''' <summary>
    ''' GuardarCotizacion (Insert y Edit)
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GuardarCotizacion( _
                                            ByVal pstrTipoTransaccion As String, _
                                            ByVal pstrNumeroCotizacion As String, _
                                            ByVal pstrEstado As String, _
                                            ByVal pstrGeneraCarta As String, _
                                            ByVal pstrValidaCliente As String, _
                                            ByVal pstrCUCliente As String, _
                                            ByVal pstrNombreCliente As String, _
                                            ByVal pstrTipoPersona As String, _
                                            ByVal pstrTipoDocumento As String, _
                                            ByVal pstrNumeroDocumento As String, _
                                            ByVal pstrValidaLinea As String, _
                                            ByVal pstrTasaLinea As String, _
                                            ByVal pstrLinea As String, _
                                            ByVal pstrEjecutivoBanca As String, _
                                            ByVal pstrBancaAtencion As String, _
                                            ByVal pstrZonal As String, _
                                            ByVal pstrContacto As String, _
                                            ByVal pstrCorreo As String, _
                                            ByVal pstrEjecutivoLeasing As String, _
 _
                                            ByVal pstrProdFinanActivo As String, _
                                            ByVal pstrProdFinanPasivo As String, _
                                            ByVal pstrTipoContrato As String, _
                                            ByVal pstrSubTipoContrato As String, _
                                            ByVal pstrMoneda As String, _
                                            ByVal pstrprocedencia As String, _
                                            ByVal pstrClasificacionBien As String, _
                                            ByVal pstrTipoBien As String, _
                                            ByVal pstrTipoInmueble As String, _
                                            ByVal pstrEstadoBien As String, _
                                            ByVal pstrPrecioVenta As String, _
                                            ByVal pstrMontoIGV As String, _
                                            ByVal pstrValorVenta As String, _
                                            ByVal pstrCuotaInicial As String, _
                                            ByVal pstrCuotaInicialPorc As String, _
                                            ByVal pstrRiesgoNeto As String, _
 _
                                            ByVal pstrTipoCronograma As String, _
                                            ByVal pstrNroCuotas As String, _
                                            ByVal pstrPeriodicidad As String, _
                                            ByVal pstrFrecuenciaPago As String, _
                                            ByVal pstrPlazoGracia As String, _
                                            ByVal pstrTipoGracia As String, _
                                            ByVal pstrFechavence As String, _
 _
                                            ByVal pstrTea As String, _
                                            ByVal pstrCostoFondos As String, _
                                            ByVal pstrSpread As String, _
                                            ByVal pstrPrecuota As String, _
                                            ByVal pstrPlazoGraciaPrecuota As String, _
 _
                                            ByVal pstrOpcionCompraPorc As String, _
                                            ByVal pstrOpcionCompraMonto As String, _
                                            ByVal pstrComisionActivacionProc As String, _
                                            ByVal pstrComisionActivacionMonto As String, _
                                            ByVal pstrComisionEstructuracionPorc As String, _
                                            ByVal pstrComisionEstructuracionMonto As String, _
 _
                                            ByVal pstrTipoBienSeguro As String, _
                                            ByVal pstrImportePrimaSeguroBien As String, _
                                            ByVal pstrNumCuotasfinanciadas As String, _
 _
                                            ByVal pstrTipoSeguro As String, _
                                            ByVal pstrImportePrimaDesgravamen As String, _
                                            ByVal pstrNumCuotaFinanciar As String, _
 _
                                            ByVal pstrMostrarTea As String, _
                                            ByVal pstrMostrarComision As String, _
                                            ByVal pstrFechaIngreso As String, _
                                            ByVal pstrFechaOfertaValida As String, _
                                            ByVal pstrPeriodoDisponibilidad As String, _
                                            ByVal pstrFechaMaxActivacion As String, _
                                            ByVal pstrOtrasComisiones As String, _
                                            ByVal pstrProveedores As String, _
 _
                                            ByVal pstrCodSuprestatario As String, _
                                            ByVal pstrCodigoContacto As String, _
                                            ByVal pstrAplicaVersion As String, _
                                            ByVal pstrEnviar As String, _
                                            ByVal pstrVersion As String, _
 _
                                            ByVal pstrDesEjecutivoBanca As String, _
                                            ByVal pstrDesZonal As String, _
                                            ByVal pstrPorcIGV As String, _
                                            ByVal pstrTipoCuota As String, _
                                            ByVal pstrDireccionCliente As String, _
                                            ByVal pstrOpcionCompra As String, _
                                            ByVal pstrComisionAct As String, _
                                            ByVal pstrComisionEstr As String _
                                            ) As String
        Try
            Dim objEGcc_cotizacion As New EGcc_cotizacion
            Dim objLCotizacionTx As New LCotizacionTx
            Dim strEGcc_cotizacion As String

            'Variables Locales
            Dim strProdFinanActivo As String = ""
            Dim strProdFinanPasivo As String = ""
            ' Inicio IBK - AAE - 03/10/2012 - Variables
            Dim strNuEstado As String = ""
            ' Fin IBK - AAE

            'Inicia Valores
            With objEGcc_cotizacion

                .Codigocotizacion = GCCUtilitario.NullableString(pstrNumeroCotizacion)
                .Codigoestadocotizacion = GCCUtilitario.NullableString(pstrEstado)
                .Versioncotizacion = GCCUtilitario.StringToInteger(pstrVersion)

                .Generarcarta = GCCUtilitario.StringToInteger(pstrGeneraCarta)
                .FlagCliente = GCCUtilitario.StringToInteger(pstrValidaCliente)
                .CodUnico = GCCUtilitario.NullableString(pstrCUCliente)
                .Codsubprestatario = GCCUtilitario.NullableString(pstrCodSuprestatario)
                .NombreCliente = GCCUtilitario.NullableString(pstrNombreCliente)
                .Codigotipopersona = GCCUtilitario.NullableString(pstrTipoPersona)
                .CodigoTipoDocumento = GCCUtilitario.NullableString(pstrTipoDocumento)
                .NumeroDocumento = GCCUtilitario.NullableString(pstrNumeroDocumento)
                .FlagLinea = GCCUtilitario.StringToInteger(pstrValidaLinea)
                .Tasalinea = GCCUtilitario.StringToDecimal(pstrTasaLinea)

                'Valida Linea
                If pstrLinea.Trim().Equals("") Or pstrLinea.Trim().Equals("null") Then
                    pstrLinea = "0"
                End If
                .Numerolinea = GCCUtilitario.NullableString(pstrLinea)

                ' Inicio IBK - AAE - 03/10/2012 - Si el estado de la cotización es Pendiente F10 o
                ' En Evaluación F10 y tengo línea entonces la paso a aprobado
                strNuEstado = GCCUtilitario.NullableString(pstrEstado)
                If pstrLinea <> "0" And pstrValidaLinea = "1" Then
                    If GCCUtilitario.NullableString(pstrEstado) = GCCConstante.C_ESTADOCOTIZACION_EVALUACION_F10 Or GCCUtilitario.NullableString(pstrEstado) = GCCConstante.C_ESTADOCOTIZACION_PENDIENTE_F10 Then
                        strNuEstado = GCCUtilitario.NullableString(GCCConstante.C_ESTADOCOTIZACION_APROBADO)
                    End If
                End If
                .Codigoestadocotizacion = strNuEstado
                ' Fin IBK - AAE 03/10/2012


                .Codigoejecutivobanca = GCCUtilitario.NullableString(pstrEjecutivoBanca)
                .Codigobanca = GCCUtilitario.NullableString(pstrBancaAtencion)
                .Codigogrupozonal = GCCUtilitario.NullableString(pstrZonal)
                .Codigoejecutivoleasing = GCCUtilitario.NullableString(pstrEjecutivoLeasing)

                'Valida Producto
                If pstrTipoContrato.Trim().Equals(GCCConstante.C_CODGCC_PROD_LEASING) Then
                    pstrProdFinanActivo = GCCConstante.C_CODLPC_PROD_LEASING
                    pstrProdFinanPasivo = GCCConstante.C_CODLPC_PROD_LEASING_PAS
                End If
                If pstrTipoContrato.Trim().Equals(GCCConstante.C_CODGCC_PROD_LEASEBACK) Then
                    pstrProdFinanActivo = GCCConstante.C_CODLPC_PROD_LEASEBACK
                    pstrProdFinanPasivo = GCCConstante.C_CODLPC_PROD_LEASEBACK_PAS
                End If
                If pstrTipoContrato.Trim().Equals(GCCConstante.C_CODGCC_PROD_IMPORTACION) Then
                    pstrProdFinanActivo = GCCConstante.C_CODLPC_PROD_IMPORTACION
                    pstrProdFinanPasivo = GCCConstante.C_CODLPC_PROD_IMPORTACION_PAS
                End If
                .Codproductofinancieroactivo = GCCUtilitario.NullableString(pstrProdFinanActivo)
                .Codproductofinancieropasivo = GCCUtilitario.NullableString(pstrProdFinanPasivo)
                .CodigoSubTipoContrato = GCCUtilitario.NullableString(pstrSubTipoContrato)
                .Codigomoneda = GCCUtilitario.NullableString(pstrMoneda)
                .Codigoprocedencia = GCCUtilitario.NullableString(pstrprocedencia)
                .Codigoclasificacionbien = GCCUtilitario.NullableString(pstrClasificacionBien)
                .CodigoTipoBien = GCCUtilitario.NullableString(pstrTipoBien)
                .Codigotipoinmueble = GCCUtilitario.NullableString(pstrTipoInmueble)
                .Codigoestadobien = GCCUtilitario.NullableString(pstrEstadoBien)
                .Precioventa = GCCUtilitario.StringToDecimal(pstrPrecioVenta)
                .Valorventaigv = GCCUtilitario.StringToDecimal(pstrMontoIGV)
                .Valorventa = GCCUtilitario.StringToDecimal(pstrValorVenta)
                .Importecuotainicial = GCCUtilitario.StringToDecimal(pstrCuotaInicial)
                .Cuotainicialporc = GCCUtilitario.StringToDecimal(pstrCuotaInicialPorc)
                .Riesgoneto = GCCUtilitario.StringToDecimal(pstrRiesgoNeto)

                .Codigotipocronograma = GCCUtilitario.NullableString(pstrTipoCronograma)
                .Numerocuotas = GCCUtilitario.StringToInteger(pstrNroCuotas)
                .Codigoperiodicidad = GCCUtilitario.NullableString(pstrPeriodicidad)
                .Codigofrecuenciapago = GCCUtilitario.NullableString(pstrFrecuenciaPago)
                .Plazograciacuota = GCCUtilitario.StringToInteger(pstrPlazoGracia)
                .Codigotipograciacuota = GCCUtilitario.NullableString(pstrTipoGracia)
                .Fechaprimervencimiento = GCCUtilitario.StringToDateTime(pstrFechavence)

                .Teaporc = GCCUtilitario.StringToDecimal(pstrTea)
                .Costofondoporc = GCCUtilitario.StringToDecimal(pstrCostoFondos)
                .Spreadporc = GCCUtilitario.StringToDecimal(pstrSpread)
                .Precuotaporc = GCCUtilitario.StringToDecimal(pstrPrecuota)
                .Plazograciaprecuota = GCCUtilitario.StringToInteger(pstrPlazoGraciaPrecuota)

                .Opcioncompraporc = GCCUtilitario.StringToDecimal(pstrOpcionCompraPorc)
                .Importeopcioncompra = GCCUtilitario.StringToDecimal(pstrOpcionCompraMonto)
                .Comisionactivacionporc = GCCUtilitario.StringToDecimal(pstrComisionActivacionProc)
                .Importecomisionactivacion = GCCUtilitario.StringToDecimal(pstrComisionActivacionMonto)
                .Comisionestructuracionporc = GCCUtilitario.StringToDecimal(pstrComisionEstructuracionPorc)
                .Importecomisionestructuracion = GCCUtilitario.StringToDecimal(pstrComisionEstructuracionMonto)

                .Codigobientiposeguro = GCCUtilitario.NullableString(pstrTipoBienSeguro)
                .Bienimporteprima = GCCUtilitario.StringToDecimal(pstrImportePrimaSeguroBien)
                .Biennrocuotasfinanciar = GCCUtilitario.StringToInteger(pstrNumCuotasfinanciadas)

                .Codigodesgravamentiposeguro = pstrTipoSeguro
                .Desgravamenimporteprima = GCCUtilitario.StringToDecimal(pstrImportePrimaDesgravamen)
                .Desgravamennrocuotasfinanciar = GCCUtilitario.StringToInteger(pstrNumCuotaFinanciar)

                .Mostrarteacartas = GCCUtilitario.StringToInteger(pstrMostrarTea)
                .Mostrarmontocomision = GCCUtilitario.StringToInteger(pstrMostrarComision)
                .Fechaingreso = GCCUtilitario.StringToDateTime(pstrFechaIngreso)
                .FechaOfertaValida = GCCUtilitario.StringToDateTime(pstrFechaOfertaValida)
                .Periododisponible = GCCUtilitario.StringToInteger(pstrPeriodoDisponibilidad)
                .Fechamaxactivacion = GCCUtilitario.StringToDateTime(pstrFechaMaxActivacion)
                .Otrascomisiones = GCCUtilitario.NullableString(pstrOtrasComisiones)
                .DescripcionProveedor = GCCUtilitario.NullableString(pstrProveedores)

                .CodigoContacto = GCCUtilitario.StringToInteger(pstrCodigoContacto)
                .NombreContacto = GCCUtilitario.NullableString(pstrContacto)
                .CorreoContacto = GCCUtilitario.NullableString(pstrCorreo)

                .DesEjecutivoBanca = GCCUtilitario.NullableString(pstrDesEjecutivoBanca)
                .DesZonal = GCCUtilitario.NullableString(pstrDesZonal)
                .MontoPorcIGV = GCCUtilitario.StringToDecimal(pstrPorcIGV)
                .FlagCuotaInicial = GCCUtilitario.NullableString(pstrTipoCuota)

                'Version
                .AplicaVersionamiento = GCCUtilitario.StringToInteger(pstrAplicaVersion)
                .Audestadologico = 1
                '.AudFechaRegistro = 	
                '.AudFechaModificacion = 	
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

                .DireccionCliente = GCCUtilitario.NullableString(pstrDireccionCliente)

                'IBK - RPH
                .FlagOpcionCompras = GCCUtilitario.NullableString(pstrOpcionCompra)
                .FlagComisionActivacion = GCCUtilitario.NullableString(pstrComisionAct)
                .FlagComisionEstructuracion = GCCUtilitario.NullableString(pstrComisionEstr)
                'fin

                '.VersionCotizacion = 	
                '.VersionFecha = 	

                'pstrEstado

                'Valida Estado (Si hacer grabar y Enviar)
                If Not pstrEnviar.Trim().Equals("9") Then
                    If pstrEnviar.Trim().Equals("0") Then
                        .Codigoestadocotizacion = GCCConstante.C_ESTADOCOTIZACION_PENDCARTA
                    ElseIf pstrEnviar.Trim().Equals("1") Then
                        .Codigoestadocotizacion = GCCConstante.C_ESTADOCOTIZACION_EVASUPERV
                    ElseIf pstrEnviar.Trim().Equals("2") Then
                        .Codigoestadocotizacion = GCCConstante.C_ESTADOCOTIZACION_INGRESADO
                    ElseIf pstrEnviar.Trim().Equals("3") Then
                        .Codigoestadocotizacion = GCCConstante.C_ESTADOCOTIZACION_PENDCARTA
                    End If
                End If

                'BORRAR => .Fechalimitevalidezcotizacion =                 
                '.CodigoSupervisorAprobo = 	
                '.FechaCarta = 

                'GCCTS_JRC_20120304-Seguimiento Datos acionales
                .CodigoUsuario = GCCSession.CodigoUsuario.ToString()
                .NombreUsuario = GCCSession.NombreUsuario.ToString()
                .PerfilUsuario = GCCSession.DescripcionPerfil.ToString()

            End With
            strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)

            'Prepara Cronograma
            Dim objListECronograma As New ListEGcc_cotizacioncronograma
            Dim strCronograma As String = ""
            Dim objCronograma As Object = HttpContext.Current.Session("DTB_CRONOGRAMA")
            If Not objCronograma Is Nothing Then
                objListECronograma = CType(objCronograma, ListEGcc_cotizacioncronograma)
                'objListECronograma = PreparaCronograma(objDtbCronograma, objEGcc_cotizacion.Codigocotizacion, objEGcc_cotizacion.Versioncotizacion)
                strCronograma = GCCUtilitario.SerializeObject(objListECronograma)
            End If


            'Ejecuta Transaccion
            Dim blnResult As Boolean = False
            ' Inicio IBK
            Dim enviaGeneracion As String
            Dim strNumeroCotizacion As String = ""
            If pstrTipoTransaccion.Trim().Equals(GCCConstante.C_TX_NUEVO) Then
                strNumeroCotizacion = objLCotizacionTx.InsertarCotizacion(strEGcc_cotizacion, strCronograma)
                blnResult = True
                enviaGeneracion = "1"
            Else
                objLCotizacionTx.ModificarCotizacion(strEGcc_cotizacion, strCronograma)
                strNumeroCotizacion = objEGcc_cotizacion.Codigocotizacion
                blnResult = True
                enviaGeneracion = ""
            End If
            ' Fin IBK
            'Inicio AAE - como el método de Base es de instancia solo se puede llamar de instancias.
            If pstrEnviar.Trim().Equals("1") Then
                Dim mbool As Boolean = oGCC_Cotizacion_frmCotizacionRegistro.EnviaCorreo(strNumeroCotizacion, "", pstrNombreCliente, "MailCotizacionSupervisor")
            End If
            If pstrEnviar.Trim().Equals("2") Then
                Dim mbool As Boolean = oGCC_Cotizacion_frmCotizacionRegistro.EnviaCorreo(strNumeroCotizacion, "", pstrNombreCliente, "MailCotizacionSupervisorDevuelta")
            End If
            If pstrEnviar.Trim().Equals("3") Then
                Dim mbool As Boolean = oGCC_Cotizacion_frmCotizacionRegistro.EnviaCorreo(strNumeroCotizacion, "", pstrNombreCliente, "MailCotizacionSupervisorAprobada")
            End If
            'Fin AAE

            'Inicio IBK - RPH Al grabar o al grabar y enviar la Cotizacion
            If (pstrEnviar.Trim().Equals("0") Or pstrEnviar.Trim().Equals("9")) And enviaGeneracion <> "" Then
                ''If pstrEnviar.Trim().Equals("0") Then
                Dim mbool As Boolean = oGCC_Cotizacion_frmCotizacionRegistro.EnviaCorreo(strNumeroCotizacion, "", pstrNombreCliente, "MailCotizacionSupervisorCreada")
            End If

            'Valida Resultado
            If blnResult Then
                Return strNumeroCotizacion.Trim.PadLeft(8, "0"c)
            Else
                Return "0"
            End If

        Catch ex As Exception
            Return ex.Message.ToString
        End Try

    End Function

    ''' <summary>
    ''' Consulta Datos RM
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    ''' ''' Inicio IBK - Se modifica el método
    <WebMethod()> _
    Public Shared Function ConsultaClienteRM(ByVal pstrCodUnico As String, ByVal pstrTipoBusqueda As Integer) As EClienteRM
        '-------------------------------------------
        ' Consulta Cliente RM
        '-------------------------------------------
        'Inicio IBK
        'Dim intNumConsulta As Integer = 1
        'Dim strCodUnico As String = pstrCodUnico
        Dim intNumConsulta As Integer = pstrTipoBusqueda
        Dim strCodUnico As String = "" 'pstrCodUnico
        Dim strTipoDoc As String = ""
        Dim strNroRuc As String = ""
        Dim strMensaje As String = ""

        If pstrTipoBusqueda = "1" Then
            strCodUnico = pstrCodUnico

            'If (strCodUnico = "00000000") Then
            '    strTipoDoc = "2"
            '    strNroRuc = pstrCodUnico
            'End If

        Else
            strTipoDoc = "2"
            strNroRuc = pstrCodUnico
        End If
        'Fin IBK
        Try
            'Consulta RM
            Dim oEClienteRM As EClienteRM = GCCUtilitario.fObtenerDatosRMCliente(intNumConsulta, strCodUnico, strTipoDoc, strNroRuc, strMensaje)

            'Verifica Existencia en RM
            If Not oEClienteRM Is Nothing Then
                'Verifica existencia en Subprestatario
                Dim objCotizacionNTx As New LCotizacionNTx
                Dim strSubPrestatario As String = objCotizacionNTx.ObtenerSubPrestatario("", pstrCodUnico)
                If Not strSubPrestatario Is Nothing Then
                    If Not strSubPrestatario.Trim().Equals("") Then

                        Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(strSubPrestatario)
                        If Not dtCotizacion Is Nothing Then
                            If dtCotizacion.Rows.Count > 0 Then
                                oEClienteRM.CodClienteLocal = dtCotizacion.Rows(0).Item("CodSubprestatario").ToString
                            End If
                        End If

                    End If
                End If

                oEClienteRM.CodError = 0
                Return oEClienteRM
            Else
                oEClienteRM = New EClienteRM()
                If strMensaje.Trim().Equals("") Then
                    oEClienteRM.CodError = 1
                    oEClienteRM.MsgError = "El Código Unico ingresado no Existe."
                    Return oEClienteRM
                Else
                    oEClienteRM.CodError = 1
                    oEClienteRM.MsgError = "El servicio de RM lanzó una excepción.(" + strMensaje + ")"
                    Return oEClienteRM
                End If
            End If
        Catch ex As Exception
            Dim oEClienteRM As New EClienteRM
            oEClienteRM.CodError = 1
            oEClienteRM.MsgError = "No se pudo cargar los datos de RM."
            Return oEClienteRM
        End Try

    End Function

    ''' <summary>
    ''' Consulta Datos Ultimus
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ConsultaUltimus(ByVal pstrCodEjecutivoBanca As String) As String

        Dim objLWebservice As New LWebService
        Dim strError As String = ""
        Dim strMensaje As String = ""
        Dim strDominioUsuario As String = GCCUtilitario.fstrObtieneKeyWebConfig("DominioUsuario")
        Dim strWsUltimusWbc As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsUltimusWBC")
        Dim strNemonicoEjecutivo As String = GCCUtilitario.fstrObtieneKeyWebConfig("NemonicoEN")

        Dim strEjecutivoBanca As String = pstrCodEjecutivoBanca
        Dim strGrupo As String = ""

        Dim strZonal As String = ""
        Dim strCodZonal As String = ""

        Dim strBanca As String = ""
        Dim strCodBanca As String = ""

        Try

            'Zonal
            objLWebservice.pObtenerDepartamentoZona(strDominioUsuario & "/" & strEjecutivoBanca, "", strZonal, strWsUltimusWbc, strError)

            'Banca
            Dim blnOkBanca As Boolean = False
            blnOkBanca = objLWebservice.fboolObtenerPrimerGrupoLogico(strDominioUsuario & "/" & strEjecutivoBanca, 0, "", strNemonicoEjecutivo, strGrupo, strWsUltimusWbc, strError)

            If blnOkBanca Then
                If Not String.IsNullOrEmpty(strGrupo) Then
                    Dim arrNemonicos As String() = strGrupo.Split("_"c)
                    If arrNemonicos.Length > 3 Then
                        Dim sBanca As String = CType(arrNemonicos(3), String)
                        If sBanca.Length > 1 Then
                            Select Case sBanca.Substring(0, 2).ToString.ToUpper
                                Case GCCConstante.C_DIV_BC
                                    strBanca = GCCConstante.C_DIVISION_BANCA_CORPORATIVA
                                    strCodBanca = GCCConstante.C_CODIGO_BANCA_BC
                                Case GCCConstante.C_DIV_BE
                                    'Inicio IBK - AAE

                                    If sBanca.Substring(0, 3).ToString.ToUpper = GCCConstante.C_DIV_BEP Then
                                        strBanca = GCCConstante.C_DIVISION_BANCA_EMPRESA_PROV
                                        strCodBanca = GCCConstante.C_CODIGO_BANCA_BEP
                                    ElseIf sBanca.Substring(0, 3).ToString.ToUpper = GCCConstante.C_DIV_BEL Then
                                        strBanca = GCCConstante.C_DIVISION_BANCA_EMPRESA_LIMA
                                        strCodBanca = GCCConstante.C_CODIGO_BANCA_BE
                                    Else
                                        strBanca = GCCConstante.C_DIVISION_BANCA_EMPRESA
                                        strCodBanca = GCCConstante.C_CODIGO_BANCA_BE
                                    End If

                                    'strBanca = GCCConstante.C_DIVISION_BANCA_EMPRESA
                                    'strCodBanca = GCCConstante.C_CODIGO_BANCA_BE
                                    'Fin IBK
                                Case GCCConstante.C_DIV_BI
                                    strBanca = GCCConstante.C_DIVISION_BANCA_INSTITUCIONAL
                                    strCodBanca = GCCConstante.C_CODIGO_BANCA_BI
                                Case GCCConstante.C_DIV_LS
                                    strBanca = GCCConstante.C_DIVISION_LEASING
                                    strCodBanca = GCCConstante.C_CODIGO_BANCA_LS
                                Case GCCConstante.C_DIV_IM
                                    strBanca = GCCConstante.C_DIVISION_INMOBILIARIA
                                    strCodBanca = GCCConstante.C_CODIGO_BANCA_IM
                            End Select
                            If strBanca.Trim().Equals("") Then
                                strBanca = GCCConstante.C_DIVISION_OTROS
                                strCodBanca = GCCConstante.C_CODIGO_BANCA_OTROS
                            End If
                            'wbc.UI.wbcSession.PoolEjecutivos = wbcSession.DominioUsuario & "/" & txtEjeNegocio.ToolTip '"GROUP:" & sGrupo
                        Else
                            Throw New ApplicationException("No se ha podido obtener la División a la que pertenece el Ejecutivo. Consulte al Administrador (4).")
                        End If
                    Else
                        Throw New ApplicationException("No se ha podido obtener la División a la que pertenece el Ejecutivo. Consulte al Administrador (3).")
                    End If
                Else
                    Throw New ApplicationException("No se ha podido obtener la División a la que pertenece el Ejecutivo. Consulte al Administrador (2).")
                End If
            Else
                Throw New ApplicationException("No se ha podido obtener la División a la que pertenece el Ejecutivo. Consulte al Administrador (1).")
            End If

            'Ejecutivos
            Dim sbResultado As New StringBuilder
            Dim objListEUsuarioUltimus As New ListEUsuarioUltimus
            Dim strGrpLogico As String = "WIO_GL_EL"
            objLWebservice.fboolObtenerUsuariosxGrupo(strGrpLogico, objListEUsuarioUltimus, strWsUltimusWbc, strError)
            If objListEUsuarioUltimus IsNot Nothing Then
                Dim objEUsuarioUltimus As New EUsuarioUltimus
                sbResultado.Append(GCCUtilitario.ArmaComboOpcion("0", "[-Seleccione-]"))
                For Each objEUsuarioUltimus In objListEUsuarioUltimus
                    sbResultado.Append(GCCUtilitario.ArmaComboOpcion(objEUsuarioUltimus.Codigousuario, objEUsuarioUltimus.Nombreusuario))
                Next
            End If

            Return strZonal + "|" + strCodBanca + "|" + strBanca + "|" + sbResultado.ToString
        Catch ex As Exception
            Return ex.Message.ToString
        End Try

    End Function

    ''' <summary>
    ''' Consulta Lineas
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 25/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ConsultaLineas(ByVal pstrCodUnico As String, ByVal pstrCodProducto As String) As String

        Dim sbResultado As New StringBuilder
        Dim objListELinea As New ListELinea

        Try
            'Consulta Lineas
            sbResultado = ListaLineas(pstrCodUnico, objListELinea)

        Catch ex As Exception
            sbResultado = New StringBuilder
            'sbResultado.Append(GCCUtilitario.ArmaComboOpcion("0", "- Seleccione -"))
            sbResultado.Append(GCCUtilitario.Concatenar("1|", ex.Message))
        End Try

        Return sbResultado.ToString

    End Function

    ''' <summary>
    ''' GuardarCotizacion (Insert y Edit)
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function GeneraCronograma( _
                                            ByVal pstrTipoTransaccion As String, _
                                            ByVal pstrNumeroCotizacion As String, _
                                            ByVal pstrEstado As String, _
                                            ByVal pstrGeneraCarta As String, _
                                            ByVal pstrValidaCliente As String, _
                                            ByVal pstrCUCliente As String, _
                                            ByVal pstrNombreCliente As String, _
                                            ByVal pstrTipoPersona As String, _
                                            ByVal pstrTipoDocumento As String, _
                                            ByVal pstrNumeroDocumento As String, _
                                            ByVal pstrValidaLinea As String, _
                                            ByVal pstrTasaLinea As String, _
                                            ByVal pstrLinea As String, _
                                            ByVal pstrEjecutivoBanca As String, _
                                            ByVal pstrBancaAtencion As String, _
                                            ByVal pstrZonal As String, _
                                            ByVal pstrContacto As String, _
                                            ByVal pstrCorreo As String, _
                                            ByVal pstrEjecutivoLeasing As String, _
 _
                                            ByVal pstrProdFinanActivo As String, _
                                            ByVal pstrProdFinanPasivo As String, _
                                            ByVal pstrTipoContrato As String, _
                                            ByVal pstrSubTipoContrato As String, _
                                            ByVal pstrMoneda As String, _
                                            ByVal pstrprocedencia As String, _
                                            ByVal pstrClasificacionBien As String, _
                                            ByVal pstrTipoBien As String, _
                                            ByVal pstrTipoInmueble As String, _
                                            ByVal pstrEstadoBien As String, _
                                            ByVal pstrPrecioVenta As String, _
                                            ByVal pstrMontoIGV As String, _
                                            ByVal pstrValorVenta As String, _
                                            ByVal pstrCuotaInicial As String, _
                                            ByVal pstrCuotaInicialPorc As String, _
                                            ByVal pstrRiesgoNeto As String, _
 _
                                            ByVal pstrTipoCronograma As String, _
                                            ByVal pstrNroCuotas As String, _
                                            ByVal pstrPeriodicidad As String, _
                                            ByVal pstrFrecuenciaPago As String, _
                                            ByVal pstrPlazoGracia As String, _
                                            ByVal pstrTipoGracia As String, _
                                            ByVal pstrFechavence As String, _
 _
                                            ByVal pstrTea As String, _
                                            ByVal pstrCostoFondos As String, _
                                            ByVal pstrSpread As String, _
                                            ByVal pstrPrecuota As String, _
                                            ByVal pstrPlazoGraciaPrecuota As String, _
 _
                                            ByVal pstrOpcionCompraPorc As String, _
                                            ByVal pstrOpcionCompraMonto As String, _
                                            ByVal pstrComisionActivacionProc As String, _
                                            ByVal pstrComisionActivacionMonto As String, _
                                            ByVal pstrComisionEstructuracionPorc As String, _
                                            ByVal pstrComisionEstructuracionMonto As String, _
 _
                                            ByVal pstrTipoBienSeguro As String, _
                                            ByVal pstrImportePrimaSeguroBien As String, _
                                            ByVal pstrNumCuotasfinanciadas As String, _
 _
                                            ByVal pstrTipoSeguro As String, _
                                            ByVal pstrImportePrimaDesgravamen As String, _
                                            ByVal pstrNumCuotaFinanciar As String, _
 _
                                            ByVal pstrMostrarTea As String, _
                                            ByVal pstrMostrarComision As String, _
                                            ByVal pstrFechaIngreso As String, _
                                            ByVal pstrFechaOfertaValida As String, _
                                            ByVal pstrPeriodoDisponibilidad As String, _
                                            ByVal pstrFechaMaxActivacion As String, _
                                            ByVal pstrOtrasComisiones As String, _
                                            ByVal pstrProveedores As String, _
 _
                                            ByVal pstrCodSuprestatario As String, _
                                            ByVal pstrCodigoContacto As String, _
                                            ByVal pstrAplicaVersion As String, _
                                            ByVal pstrEnviar As String _
                                            ) As JQGridJsonResponse
        Try
            Dim objEGcc_cotizacion As New EGcc_cotizacion
            Dim objLCotizacionTx As New LCotizacionTx

            'Inicia Valores
            With objEGcc_cotizacion

                .Codigocotizacion = GCCUtilitario.NullableString(pstrNumeroCotizacion)
                .Codigoestadocotizacion = GCCUtilitario.NullableString(pstrEstado)
                .Generarcarta = GCCUtilitario.StringToInteger(pstrGeneraCarta)
                .FlagCliente = GCCUtilitario.StringToInteger(pstrValidaCliente)
                .CodUnico = GCCUtilitario.NullableString(pstrCUCliente)
                .Codsubprestatario = GCCUtilitario.NullableString(pstrCodSuprestatario)
                .NombreCliente = GCCUtilitario.NullableString(pstrNombreCliente)
                .Codigotipopersona = GCCUtilitario.NullableString(pstrTipoPersona)
                .CodigoTipoDocumento = GCCUtilitario.NullableString(pstrTipoDocumento)
                .NumeroDocumento = GCCUtilitario.NullableString(pstrNumeroDocumento)
                .FlagLinea = GCCUtilitario.StringToInteger(pstrValidaLinea)
                .Tasalinea = GCCUtilitario.StringToDecimal(pstrTasaLinea)
                .Numerolinea = GCCUtilitario.NullableString(pstrLinea)
                .Codigoejecutivobanca = GCCUtilitario.NullableString(pstrEjecutivoBanca)
                .Codigobanca = GCCUtilitario.NullableString(pstrBancaAtencion)
                .Codigogrupozonal = GCCUtilitario.NullableString(pstrZonal)
                .Codigoejecutivoleasing = GCCUtilitario.NullableString(pstrEjecutivoLeasing)

                .Codproductofinancieroactivo = GCCUtilitario.NullableString(pstrProdFinanActivo)
                .Codproductofinancieropasivo = GCCUtilitario.NullableString(pstrProdFinanPasivo)
                .CodigoSubTipoContrato = GCCUtilitario.NullableString(pstrSubTipoContrato)
                .Codigomoneda = GCCUtilitario.NullableString(pstrMoneda)
                .Codigoprocedencia = GCCUtilitario.NullableString(pstrprocedencia)
                .Codigoclasificacionbien = GCCUtilitario.NullableString(pstrClasificacionBien)
                .CodigoTipoBien = GCCUtilitario.NullableString(pstrTipoBien)
                .Codigotipoinmueble = GCCUtilitario.NullableString(pstrTipoInmueble)
                .Codigoestadobien = GCCUtilitario.NullableString(pstrEstadoBien)
                .Precioventa = GCCUtilitario.StringToDecimal(pstrPrecioVenta)
                .Valorventaigv = GCCUtilitario.StringToDecimal(pstrMontoIGV)
                .Valorventa = GCCUtilitario.StringToDecimal(pstrValorVenta)
                .Importecuotainicial = GCCUtilitario.StringToDecimal(pstrCuotaInicial)
                .Cuotainicialporc = GCCUtilitario.StringToDecimal(pstrCuotaInicialPorc)
                .Riesgoneto = GCCUtilitario.StringToDecimal(pstrRiesgoNeto)

                .Codigotipocronograma = GCCUtilitario.NullableString(pstrTipoCronograma)
                .Numerocuotas = GCCUtilitario.StringToInteger(pstrNroCuotas)
                .Codigoperiodicidad = GCCUtilitario.NullableString(pstrPeriodicidad)
                .Codigofrecuenciapago = GCCUtilitario.NullableString(pstrFrecuenciaPago)
                .Plazograciacuota = GCCUtilitario.StringToInteger(pstrPlazoGracia)
                .Codigotipograciacuota = GCCUtilitario.NullableString(pstrTipoGracia)
                .Fechaprimervencimiento = GCCUtilitario.StringToDateTime(pstrFechavence)

                .Teaporc = GCCUtilitario.StringToDecimal(pstrTea)
                .Costofondoporc = GCCUtilitario.StringToDecimal(pstrCostoFondos)
                .Spreadporc = GCCUtilitario.StringToDecimal(pstrSpread)
                .Precuotaporc = GCCUtilitario.StringToDecimal(pstrPrecuota)
                .Plazograciaprecuota = GCCUtilitario.StringToInteger(pstrPlazoGraciaPrecuota)

                .Opcioncompraporc = GCCUtilitario.StringToDecimal(pstrOpcionCompraPorc)
                .Importeopcioncompra = GCCUtilitario.StringToDecimal(pstrOpcionCompraMonto)
                .Comisionactivacionporc = GCCUtilitario.StringToDecimal(pstrComisionActivacionProc)
                .Importecomisionactivacion = GCCUtilitario.StringToDecimal(pstrComisionActivacionMonto)
                .Comisionestructuracionporc = GCCUtilitario.StringToDecimal(pstrComisionEstructuracionPorc)
                .Importecomisionestructuracion = GCCUtilitario.StringToDecimal(pstrComisionEstructuracionMonto)

                .Codigobientiposeguro = GCCUtilitario.NullableString(pstrTipoBienSeguro)
                .Bienimporteprima = GCCUtilitario.StringToDecimal(pstrImportePrimaSeguroBien)
                .Biennrocuotasfinanciar = GCCUtilitario.StringToInteger(pstrNumCuotasfinanciadas)

                .Codigodesgravamentiposeguro = pstrTipoSeguro
                .Desgravamenimporteprima = GCCUtilitario.StringToDecimal(pstrImportePrimaDesgravamen)
                .Desgravamennrocuotasfinanciar = GCCUtilitario.StringToInteger(pstrNumCuotaFinanciar)

                .Mostrarteacartas = GCCUtilitario.StringToInteger(pstrMostrarTea)
                .Mostrarmontocomision = GCCUtilitario.StringToInteger(pstrMostrarComision)
                .Fechaingreso = GCCUtilitario.StringToDateTime(pstrFechaIngreso)
                .FechaOfertaValida = GCCUtilitario.StringToDateTime(pstrFechaOfertaValida)
                .Periododisponible = GCCUtilitario.StringToInteger(pstrPeriodoDisponibilidad)
                .Fechamaxactivacion = GCCUtilitario.StringToDateTime(pstrFechaMaxActivacion)
                .Otrascomisiones = GCCUtilitario.NullableString(pstrOtrasComisiones)
                .DescripcionProveedor = GCCUtilitario.NullableString(pstrProveedores)

                .CodigoContacto = GCCUtilitario.StringToInteger(pstrCodigoContacto)
                .NombreContacto = GCCUtilitario.NullableString(pstrContacto)
                .CorreoContacto = GCCUtilitario.NullableString(pstrCorreo)

                .Audestadologico = 1
                '.AudFechaRegistro = 	
                '.AudFechaModificacion = 	
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)

                'Version
                .AplicaVersionamiento = GCCUtilitario.StringToInteger(pstrAplicaVersion)
                '.VersionCotizacion = 	
                '.VersionFecha = 	

                'Valida Estado (Si hacer grabar y Enviar)
                If Not pstrEnviar.Trim().Equals("9") Then
                    If pstrEnviar.Trim().Equals("0") Then
                        .Codigoestadocotizacion = GCCConstante.C_ESTADOCOTIZACION_PENDCARTA
                    ElseIf pstrEnviar.Trim().Equals("1") Then
                        .Codigoestadocotizacion = GCCConstante.C_ESTADOCOTIZACION_EVASUPERV
                    ElseIf pstrEnviar.Trim().Equals("2") Then
                        .Codigoestadocotizacion = GCCConstante.C_ESTADOCOTIZACION_DESAPROBADO
                    ElseIf pstrEnviar.Trim().Equals("3") Then
                        .Codigoestadocotizacion = GCCConstante.C_ESTADOCOTIZACION_PENDCARTA
                    End If
                End If

                'BORRAR => .Fechalimitevalidezcotizacion =                 
                '.CodigoSupervisorAprobo = 	
                '.FechaCarta = 

            End With

            'Arma Consulta Cronograma
            Dim objListECronograma As New ListEGcc_cotizacioncronograma
            Dim objInput As New lpcCronograma.QuotationInput
            Dim objQry As New lpcCronograma.clsCronograma
            Dim objTbl As New DataTable

            If fGenerarQuotationInput(objInput, objEGcc_cotizacion) Then
                objTbl = objQry.fGenerateQuoteDs(objInput)

                'Setea en Entidad
                objListECronograma = PreparaCronograma(objTbl, objEGcc_cotizacion.Codigocotizacion, "", objEGcc_cotizacion, True)
            End If

            'Setea en Sesion
            HttpContext.Current.Session("DTB_CRONOGRAMA") = objListECronograma

            'Datos Paginacion
            Dim intTotalRegistros As Integer = objListECronograma.Count
            Dim intTotalxPagina As Integer = 50
            Dim intTotalPaginas As Integer = 0
            Dim intPaginaActual As Integer = 1

            Dim decPaginasTotal As Decimal = intTotalRegistros / intTotalxPagina
            If decPaginasTotal <= 1 Then
                intTotalPaginas = 1
            ElseIf decPaginasTotal <= 2 Then
                intTotalPaginas = 2
                'IBK - RPH se aumento el nro de cuotas a 240
            ElseIf decPaginasTotal <= 3 Then
                intTotalPaginas = 3
            ElseIf decPaginasTotal <= 4 Then
                intTotalPaginas = 4
            Else
                intTotalPaginas = 5
            End If
            'Fin

            'Resize Gronograma Datatable
            Dim objListECronogramaNuevo As New ListEGcc_cotizacioncronograma
            Dim objECronograma As New EGcc_cotizacioncronograma
            Dim intContador As Integer = 0
            For Each objECronograma In objListECronograma
                If ((intPaginaActual - 1) * intTotalxPagina) <= intContador And (intPaginaActual * intTotalxPagina) >= intContador Then
                    objListECronogramaNuevo.Add(objECronograma)
                End If
                intContador = intContador + 1
            Next

            'Devuelve
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseClass(intTotalPaginas, intPaginaActual, intTotalRegistros, objListECronogramaNuevo)

        Catch ex As Exception
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseError(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' GuardarCotizacion (Insert y Edit)
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function PaginaCronograma(ByVal pstrPaginaActual As String) As JQGridJsonResponse
        Try

            'Arma Consulta Cronograma
            Dim objListECronograma As New ListEGcc_cotizacioncronograma
            objListECronograma = HttpContext.Current.Session("DTB_CRONOGRAMA")

            If Not objListECronograma Is Nothing Then

                'Datos Paginacion
                Dim intTotalRegistros As Integer = objListECronograma.Count
                Dim intTotalxPagina As Integer = 50
                Dim intTotalPaginas As Integer = 0
                Dim intPaginaActual As Integer = GCCUtilitario.CheckInt(pstrPaginaActual)

                Dim decPaginasTotal As Decimal = intTotalRegistros / intTotalxPagina
                If decPaginasTotal <= 1 Then
                    intTotalPaginas = 1
                ElseIf decPaginasTotal <= 2 Then
                    intTotalPaginas = 2
                    'IBK - RPH se aumento el nro de cuotas a 240
                ElseIf decPaginasTotal <= 3 Then
                    intTotalPaginas = 3
                ElseIf decPaginasTotal <= 4 Then
                    intTotalPaginas = 4
                Else
                    intTotalPaginas = 5
                End If
                'Fin 

                'Resize Gronograma Datatable
                Dim objListECronogramaNuevo As New ListEGcc_cotizacioncronograma
                Dim objECronograma As New EGcc_cotizacioncronograma
                Dim intContador As Integer = 0
                For Each objECronograma In objListECronograma
                    If ((intPaginaActual - 1) * intTotalxPagina) <= intContador And (intPaginaActual * intTotalxPagina) >= intContador Then
                        objListECronogramaNuevo.Add(objECronograma)
                    End If
                    intContador = intContador + 1
                Next

                'Devuelve
                Dim objJQGridJsonResponse As New JQGridJsonResponse
                Return objJQGridJsonResponse.JQGridJsonResponseClass(intTotalPaginas, intPaginaActual, intTotalRegistros, objListECronogramaNuevo)

            Else
                Dim objJQGridJsonResponse As New JQGridJsonResponse
                Return objJQGridJsonResponse.JQGridJsonResponseError("Consulta Vacia")
            End If

        Catch ex As Exception
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseError(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Listado de Documentos de Cotizacion
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListadoCotizacionDocumento(ByVal pPageSize As Integer, _
                                                         ByVal pCurrentPage As Integer, _
                                                         ByVal pSortColumn As String, _
                                                         ByVal pSortOrder As String, _
                                                         ByVal pstrNroCotizacion As String _
                                                       ) As JQGridJsonResponse

        'Variables
        Dim objCotizacionNTx As New LCotizacionNTx

        Try

            'Inicializa Objeto
            Dim objEGcc_cotizaciondocumento As New EGcc_cotizaciondocumento
            Dim strEGcc_cotizacionDocumento As String
            With objEGcc_cotizaciondocumento
                .Codigocotizacion = GCCUtilitario.NullableString(pstrNroCotizacion)
            End With
            strEGcc_cotizacionDocumento = GCCUtilitario.SerializeObject(objEGcc_cotizaciondocumento)

            'Ejecuta Consulta
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.ListadoCotizacionDocumento(pPageSize, _
                                                                                                                                       pCurrentPage, _
                                                                                                                                       pSortColumn, _
                                                                                                                                       pSortOrder, _
                                                                                                                                       strEGcc_cotizacionDocumento))

            ' Número de página actual.
            Dim currentPage As Integer = pCurrentPage
            ' Total de registros a mostrar.
            Dim totalRecords As Integer
            If dtCotizacion.Rows.Count = 0 Then
                totalRecords = 0
            Else
                totalRecords = Convert.ToInt32(dtCotizacion.Rows(0)("RecordCount"))
            End If

            ' Número total de páginas
            Dim totalPages As Integer = TotalPaginas(totalRecords, pPageSize)
            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseDataTable(totalPages, currentPage, totalRecords, dtCotizacion)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Eliminar DocumentoCometario
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 22/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function EliminaDocumentoComentario(ByVal pstrCodigoCotizacion As String, _
                                                         ByVal pstrCodigoDocumento As String _
                                                       ) As String

        ''Variables
        Dim objCotizacionTx As New LCotizacionTx

        Try

            'Inicializa Objeto
            Dim objEGcc_cotizaciondocumento As New EGcc_cotizaciondocumento
            Dim strEGcc_cotizacionDocumento As String
            With objEGcc_cotizaciondocumento
                .Codigocotizacion = GCCUtilitario.NullableString(pstrCodigoCotizacion)
                .Codigodocumento = GCCUtilitario.CheckInt(pstrCodigoDocumento)

                .Audestadologico = 0
                .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With
            strEGcc_cotizacionDocumento = GCCUtilitario.SerializeObject(objEGcc_cotizaciondocumento)

            'Ejecuta Consulta
            Dim blnResultado As Boolean = objCotizacionTx.EliminarCotizacionDocumento(strEGcc_cotizacionDocumento)

            Return ""

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Listado de Cronograma de Cotizacion
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 28/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ListadoCotizacionCronograma(ByVal pstrNroCotizacion As String, _
                                                       ByVal pstrVersionCotizacion As String _
                                                       ) As JQGridJsonResponse

        'Variables
        Dim objCotizacionNTx As New LCotizacionNTx

        Try
            'Inicializa Objeto
            Dim objECotizacion As New EGcc_cotizacion
            Dim strECotizacion As String
            With objECotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pstrNroCotizacion)
                .Versioncotizacion = GCCUtilitario.StringToInteger(pstrVersionCotizacion)
            End With
            strECotizacion = GCCUtilitario.SerializeObject(objECotizacion)

            'Ejecuta Consulta
            Dim dtCronograma As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.CotizacionCronogramaGet(strECotizacion))
            Dim objListECronograma As New ListEGcc_cotizacioncronograma
            objListECronograma = PreparaCronograma(dtCronograma, pstrNroCotizacion, pstrVersionCotizacion, objECotizacion, False)

            HttpContext.Current.Session("DTB_CRONOGRAMA") = objListECronograma

            'Datos Paginacion
            Dim intTotalRegistros As Integer = objListECronograma.Count
            Dim intTotalxPagina As Integer = 50
            Dim intTotalPaginas As Integer = 0
            Dim intPaginaActual As Integer = 1

            Dim decPaginasTotal As Decimal = intTotalRegistros / intTotalxPagina
            If decPaginasTotal <= 1 Then
                intTotalPaginas = 1
            ElseIf decPaginasTotal <= 2 Then
                intTotalPaginas = 2
                'IBK - RPH se aumento el nro de cuotas a 240
            ElseIf decPaginasTotal <= 3 Then
                intTotalPaginas = 3
            ElseIf decPaginasTotal <= 4 Then
                intTotalPaginas = 4
            Else
                intTotalPaginas = 5
            End If
            'Fin

            'Resize Gronograma Datatable
            Dim objListECronogramaNuevo As New ListEGcc_cotizacioncronograma
            Dim objECronograma As New EGcc_cotizacioncronograma
            Dim intContador As Integer = 0
            For Each objECronograma In objListECronograma
                If ((intPaginaActual - 1) * intTotalxPagina) <= intContador And (intPaginaActual * intTotalxPagina) >= intContador Then
                    objListECronogramaNuevo.Add(objECronograma)
                End If
                intContador = intContador + 1
            Next

            Dim objJQGridJsonResponse As New JQGridJsonResponse
            Return objJQGridJsonResponse.JQGridJsonResponseClass(intTotalPaginas, intPaginaActual, intTotalRegistros, objListECronogramaNuevo)

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Consulta Ejecutivo Leasing
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ConsultaEjecutivoLeasing() As String

        Dim objLWebservice As New LWebService
        Dim strError As String = ""
        Dim strMensaje As String = ""
        Dim strDominioUsuario As String = GCCUtilitario.fstrObtieneKeyWebConfig("DominioUsuario")
        Dim strWsUltimusWbc As String = GCCUtilitario.fstrObtieneKeyWebConfig("wsUltimusWBC")
        Dim strNemonicoEjecutivo As String = GCCUtilitario.fstrObtieneKeyWebConfig("NemonicoEN")

        Try

            'Ejecutivos
            Dim sbResultado As New StringBuilder
            Dim objListEUsuarioUltimus As New ListEUsuarioUltimus
            Dim strGrpLogico As String = "WIO_GL_EL"
            objLWebservice.fboolObtenerUsuariosxGrupo(strGrpLogico, objListEUsuarioUltimus, strWsUltimusWbc, strError)
            If objListEUsuarioUltimus IsNot Nothing Then
                Dim objEUsuarioUltimus As New EUsuarioUltimus
                sbResultado.Append(GCCUtilitario.ArmaComboOpcion("0", "[-Seleccione-]"))
                For Each objEUsuarioUltimus In objListEUsuarioUltimus
                    sbResultado.Append(GCCUtilitario.ArmaComboOpcion(objEUsuarioUltimus.Codigousuario, objEUsuarioUltimus.Nombreusuario))
                Next
            End If

            Return sbResultado.ToString
        Catch ex As Exception
            Return ex.Message.ToString
        End Try

    End Function

    ''' <summary>
    ''' Actualiza Bloquero
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    <WebMethod()> _
    Public Shared Function ActualizaBloqueo(ByVal pstrCodBloqueo As String) As String

        Try
            Dim objEBloqueo As New EGCC_Bloqueo
            With objEBloqueo
                .CodigoBloqueo = GCCUtilitario.CheckInt(pstrCodBloqueo)
                .CodigoUsuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                .NombreUsuario = GCCUtilitario.NullableString(GCCSession.NombreUsuario)
                .Activo = "1"
            End With
            Dim objLUtilTX As New LUtilTX
            objLUtilTX.ModificarBloqueo(GCCUtilitario.SerializeObject(objEBloqueo))

            Return "0"
        Catch ex As Exception
            Return "1"
        End Try

    End Function

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Inicializa Combos
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 14/05/2012
    ''' </remarks>
    Protected Sub InicializaCombos()

        'Carga Combos
        GCCUtilitario.CargarComboValorGenerico(Me.cmbEstado, GCCConstante.C_TABLAGENERICA_ESTADO_COTIZACION)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoPersona, GCCConstante.C_TABLAGENERICA_TIPO_PERSONA)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoDocumento, GCCConstante.C_TABLAGENERICA_TIPO_DOCUMENTO)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoContrato, GCCConstante.C_TABLAGENERICA_TIPO_CONTRATO)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbSubTipoContrato, GCCConstante.C_TABLAGENERICA_SUB_TIPO_CONTRATO)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbprocedencia, GCCConstante.C_TABLAGENERICA_PROCEDENCIA)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbClasificacionBien, GCCConstante.C_TABLAGENERICA_CLASIFICACION_BIEN)
        'GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoInmueble, GCCConstante.C_TABLAGENERICA_TIPO_INMUEBLE)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbEstadoBien, GCCConstante.C_TABLAGENERICA_ESTADO_BIEN)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoCronograma, GCCConstante.C_TABLAGENERICA_TIPO_CRONOGRAMA)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoGracia, GCCConstante.C_TABLAGENERICA_TIPO_GRACIA)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoBienSeguro, GCCConstante.C_TABLAGENERICA_TIPO_BIEN_SEGURO)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbTipoSeguro, GCCConstante.C_TABLAGENERICA_TIPO_SEGURO)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbBancaAtencion, GCCConstante.C_TABLAGENERICA_BANCA_ATENCION)

        'GCCUtilitario.CargarComboValorGenerico(Me.cmbEjecutivoBanca, GCCConstante.C_TABLAGENERICA_EJECUTIVO_BANCA)
        GCCUtilitario.CargarComboValorGenerico(Me.cmbEjecutivoLeasing, GCCConstante.C_TABLAGENERICA_EJECUTIVO)

        GCCUtilitario.CargarComboMoneda(Me.cmbMoneda)

    End Sub

    ''' <summary>
    ''' Get Cotizacion
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Protected Sub CargaEditarCotizacion(ByVal pstrCodigoCotizacion As String)

        Try

            Dim objCotizacionNTx As New LCotizacionNTx
            Dim msgError As String = ""
            'Inicializa Objeto
            Dim objEGcc_cotizacion As New EGcc_cotizacion
            Dim strEGcc_cotizacion As String
            With objEGcc_cotizacion
                .Codigocotizacion = GCCUtilitario.NullableString(pstrCodigoCotizacion)
            End With
            strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)

            'Ejecuta Consulta
            Dim dtCotizacion As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objCotizacionNTx.GetCotizacion(strEGcc_cotizacion))

            'Valida si existe
            If dtCotizacion.Rows.Count > 0 Then

                '*****************************
                'CABECERA
                '*****************************
                Me.hddCodigoCotizacion.Value = dtCotizacion.Rows(0).Item("CodigoCotizacion").ToString
                Me.txtNumeroCotizacion.Value = dtCotizacion.Rows(0).Item("CodigoCotizacion").ToString
                Me.hddVersionCotizacion.Value = dtCotizacion.Rows(0).Item("versionCotizacion").ToString

                Dim pstrProdFinanActivo As String = dtCotizacion.Rows(0).Item("Codproductofinancieroactivo").ToString
                Dim pstrProdFinanPasivo As String = dtCotizacion.Rows(0).Item("Codproductofinancieropasivo").ToString
                Dim pstrTipoContrato As String = ""
                If pstrProdFinanActivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_LEASING) And pstrProdFinanPasivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_LEASING_PAS) Then
                    pstrTipoContrato = GCCConstante.C_CODGCC_PROD_LEASING
                End If
                If pstrProdFinanActivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_LEASEBACK) And pstrProdFinanPasivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_LEASEBACK_PAS) Then
                    pstrTipoContrato = GCCConstante.C_CODGCC_PROD_LEASEBACK
                End If
                If pstrProdFinanActivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_IMPORTACION) And pstrProdFinanPasivo.Trim().Equals(GCCConstante.C_CODLPC_PROD_IMPORTACION_PAS) Then
                    pstrTipoContrato = GCCConstante.C_CODGCC_PROD_IMPORTACION
                End If
                GCCUtilitario.SeleccionaCombo(cmbTipoContrato, pstrTipoContrato)

                Dim strCodUnico As String = dtCotizacion.Rows(0).Item("CodUnico").ToString
                Me.txtCUCliente.Value = strCodUnico
                Me.hddCodUnico.Value = strCodUnico
                Me.txtNombreCliente.Value = dtCotizacion.Rows(0).Item("NombreCliente").ToString
                Me.txtNumeroDocumento.Value = dtCotizacion.Rows(0).Item("NumeroDocumento").ToString
                Me.hddCodSuprestatario.Value = dtCotizacion.Rows(0).Item("CodSubprestatario").ToString

                GCCUtilitario.SeleccionaCombo(cmbEstado, dtCotizacion.Rows(0).Item("Codigoestadocotizacion").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbTipoPersona, dtCotizacion.Rows(0).Item("Codigotipopersona").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbTipoDocumento, dtCotizacion.Rows(0).Item("CodigoTipoDocumento").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbBancaAtencion, dtCotizacion.Rows(0).Item("Codigobanca").ToString.Trim)
                Me.txtEjecutivoBanca.Value = dtCotizacion.Rows(0).Item("DesEjecutivoBanca").ToString
                Me.txtZonal.Value = dtCotizacion.Rows(0).Item("DesZonal").ToString

                'Gestion EjecutivoBanca
                Dim strCodigoejecutivobanca As String = dtCotizacion.Rows(0).Item("Codigoejecutivobanca").ToString.Trim
                GCCUtilitario.SeleccionaCombo(cmbEjecutivoBanca, strCodigoejecutivobanca)
                GCCUtilitario.CargarComboValorGenericoAnidado(Me.cmbZonal, GCCConstante.C_TABLAGENERICA_ZONAL, strCodigoejecutivobanca)
                GCCUtilitario.SeleccionaCombo(cmbZonal, dtCotizacion.Rows(0).Item("Codigogrupozonal").ToString.Trim)

                'Ejecutivo Leasing               
                GCCUtilitario.SeleccionaCombo(cmbEjecutivoLeasing, dtCotizacion.Rows(0).Item("Codigoejecutivoleasing").ToString.Trim)
                Me.hddCodigoContacto.Value = dtCotizacion.Rows(0).Item("Codigocontacto").ToString
                Me.txtContacto.Value = dtCotizacion.Rows(0).Item("Nombrecontacto").ToString
                Me.txtCorreo.Value = dtCotizacion.Rows(0).Item("Correocontacto").ToString

                'Valida Check Cliente
                Dim intFlagCliente As Integer = GCCUtilitario.CheckInt(dtCotizacion.Rows(0).Item("FlagCliente"))
                Me.hddValidaCliente.Value = intFlagCliente
                If intFlagCliente = 1 Then
                    Me.chkValidaCliente.Checked = True
                Else
                    Me.chkValidaCliente.Checked = False
                End If

                'Valida Check Linea
                Dim intFlagLinea As Integer = GCCUtilitario.CheckInt(dtCotizacion.Rows(0).Item("FlagLinea"))
                Me.hddValidaLinea.Value = intFlagLinea
                If intFlagLinea = 1 Then
                    Me.chkLinea.Checked = True
                Else
                    Me.chkLinea.Checked = False
                End If
                'Valida ListaLineas
                If Not strCodUnico Is Nothing Then
                    If Not strCodUnico.Trim.Equals("") Then
                        Dim sbResultado As New StringBuilder
                        Try
                            Dim objListELinea As New ListELinea
                            sbResultado = ListaLineas(strCodUnico, objListELinea)
                            'Combo
                            cmbLinea.DataSource = objListELinea
                            cmbLinea.DataTextField = "ClaveCL"
                            cmbLinea.DataValueField = "ClaveCL"
                            cmbLinea.DataBind()
                            GCCUtilitario.pInsertarPrimerItemHtmlSelect(cmbLinea, "[-Seleccione-]", "0")
                            GCCUtilitario.SeleccionaCombo(cmbLinea, dtCotizacion.Rows(0).Item("NumeroLinea").ToString.Trim)
                        Catch ex As Exception
                            GCCUtilitario.pInsertarPrimerItemHtmlSelect(cmbLinea, "[-Seleccione-]", "0")
                            msgError = "No se puede conectar con Lineas"
                        End Try
                    End If
                End If

                'Valida Check Carta
                Dim intGenerarcarta As Integer = GCCUtilitario.CheckInt(dtCotizacion.Rows(0).Item("Generarcarta"))
                Dim strFechaCarta As String = GCCUtilitario.CheckDateString(dtCotizacion.Rows(0).Item("FechaCarta").ToString, "C")
                Me.hddGeneraCarta.Value = intGenerarcarta
                If Not strFechaCarta Is Nothing Then
                    Me.lblFechacarta.InnerHtml = strFechaCarta
                End If
                If intGenerarcarta = 1 Then
                    Me.chkGeneraCarta.Checked = True
                Else
                    Me.chkGeneraCarta.Checked = False
                End If

                '********************************
                'DATOS GENERALES :: COTIZACION
                '********************************   
                GCCUtilitario.SeleccionaCombo(cmbSubTipoContrato, dtCotizacion.Rows(0).Item("CodigoSubTipoContrato").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbMoneda, dtCotizacion.Rows(0).Item("Codigomoneda").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbClasificacionBien, dtCotizacion.Rows(0).Item("Codigoclasificacionbien").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbprocedencia, dtCotizacion.Rows(0).Item("Codigoprocedencia").ToString.Trim)
                GCCUtilitario.CargarComboValorGenericoAnidado(cmbTipoBien, GCCConstante.C_TABLAGENERICA_TIPO_BIEN, dtCotizacion.Rows(0).Item("Codigoclasificacionbien").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbTipoBien, dtCotizacion.Rows(0).Item("CodigoTipoBien").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbEstadoBien, dtCotizacion.Rows(0).Item("Codigoestadobien").ToString.Trim)
                Me.txtPrecioVenta.Value = dtCotizacion.Rows(0).Item("Precioventa").ToString
                Me.txtPorcIGV.Value = dtCotizacion.Rows(0).Item("MontoPorcIGV").ToString
                Me.txtMontoIGV.Value = dtCotizacion.Rows(0).Item("Valorventaigv").ToString
                Me.txtValorVenta.Value = dtCotizacion.Rows(0).Item("Valorventa").ToString
                Me.txtCuotaInicial.Value = dtCotizacion.Rows(0).Item("Importecuotainicial").ToString
                Me.txtCuotaInicialPorc.Value = dtCotizacion.Rows(0).Item("Cuotainicialporc").ToString
                Me.txtRiesgoNeto.Value = dtCotizacion.Rows(0).Item("Riesgoneto").ToString

                'Valida Cuota
                Dim intFlagCuota As Integer = GCCUtilitario.CheckInt(dtCotizacion.Rows(0).Item("FlagCuotaInicial"))
                Me.hddTipoCuota.Value = intFlagCuota

                '********************************
                'DATOS GENERALES :: CRONOGRAMA
                '********************************
                GCCUtilitario.CargarComboValorGenerico(Me.cmbPeriodicidad, GCCConstante.C_TABLAGENERICA_PERIOCIDAD)
                GCCUtilitario.CargarComboValorGenericoAnidado(Me.cmbFrecuenciaPago, GCCConstante.C_TABLAGENERICA_FRECUENCIA_PAGO, dtCotizacion.Rows(0).Item("Codigoperiodicidad").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbTipoCronograma, dtCotizacion.Rows(0).Item("Codigotipocronograma").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbPeriodicidad, dtCotizacion.Rows(0).Item("Codigoperiodicidad").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbFrecuenciaPago, dtCotizacion.Rows(0).Item("Codigofrecuenciapago").ToString.Trim)
                GCCUtilitario.SeleccionaCombo(cmbTipoGracia, dtCotizacion.Rows(0).Item("Codigotipograciacuota").ToString.Trim)
                Me.txtNroCuotas.Value = dtCotizacion.Rows(0).Item("Numerocuotas").ToString.Trim
                Me.txtPlazoGracia.Value = dtCotizacion.Rows(0).Item("Plazograciacuota").ToString.Trim
                Me.txtFechavence.Value = GCCUtilitario.CheckDateString(dtCotizacion.Rows(0).Item("Fechaprimervencimiento").ToString.Trim, "C")

                '********************************
                'DATOS GENERALES :: TASAS
                '********************************
                Me.txtTEA.Value = dtCotizacion.Rows(0).Item("Teaporc").ToString.Trim
                Me.txtCostoFondos.Value = dtCotizacion.Rows(0).Item("Costofondoporc").ToString.Trim
                Me.txtSpread.Value = dtCotizacion.Rows(0).Item("Spreadporc").ToString.Trim
                Me.txtPrecuota.Value = dtCotizacion.Rows(0).Item("Precuotaporc").ToString.Trim
                Me.txtPlazoGraciaPrecuota.Value = dtCotizacion.Rows(0).Item("Plazograciaprecuota").ToString.Trim

                '********************************
                'DATOS GENERALES :: COMISIONES
                '********************************                
                Me.txtOpcionCompraPorc.Value = dtCotizacion.Rows(0).Item("Opcioncompraporc").ToString.Trim
                Me.txtOpcionCompraMonto.Value = dtCotizacion.Rows(0).Item("Importeopcioncompra").ToString.Trim
                Me.txtComisionActivacionProc.Value = dtCotizacion.Rows(0).Item("Comisionactivacionporc").ToString.Trim
                Me.txtComisionActivacionMonto.Value = dtCotizacion.Rows(0).Item("Importecomisionactivacion").ToString.Trim
                Me.txtComisionEstructuracionPorc.Value = dtCotizacion.Rows(0).Item("Comisionestructuracionporc").ToString.Trim
                Me.txtComisionEstructuracionMonto.Value = dtCotizacion.Rows(0).Item("Importecomisionestructuracion").ToString.Trim

                '********************************
                'DATOS GENERALES :: SEGURO BIEN
                '********************************                
                GCCUtilitario.SeleccionaCombo(cmbTipoBienSeguro, dtCotizacion.Rows(0).Item("Codigobientiposeguro").ToString.Trim)
                Me.txtImportePrimaSeguroBien.Value = dtCotizacion.Rows(0).Item("Bienimporteprima").ToString.Trim
                Me.txtNumCuotasfinanciadas.Value = dtCotizacion.Rows(0).Item("Biennrocuotasfinanciar").ToString.Trim

                '********************************
                'DATOS GENERALES :: SEGURO DEGRA
                '********************************                
                GCCUtilitario.SeleccionaCombo(cmbTipoSeguro, dtCotizacion.Rows(0).Item("Codigodesgravamentiposeguro").ToString.Trim)
                Me.txtImportePrimaDesgravamen.Value = dtCotizacion.Rows(0).Item("Desgravamenimporteprima").ToString.Trim
                Me.txtNumCuotaFinanciar.Value = dtCotizacion.Rows(0).Item("Desgravamennrocuotasfinanciar").ToString.Trim

                '********************************
                'OPCIONES
                '********************************   
                Me.txtFechaIngreso.Value = GCCUtilitario.CheckDateString(dtCotizacion.Rows(0).Item("Fechaingreso").ToString.Trim, "C")
                Me.txtFechaOfertaValida.Value = GCCUtilitario.CheckDateString(dtCotizacion.Rows(0).Item("FechaOfertaValida").ToString.Trim, "C")
                Me.txtPeriodoDisponibilidad.Value = dtCotizacion.Rows(0).Item("Periododisponible").ToString.Trim
                Me.txtFechaMaxActivacion.Value = GCCUtilitario.CheckDateString(dtCotizacion.Rows(0).Item("Fechamaxactivacion").ToString.Trim, "C")
                Me.txaOtrasComisiones.Value = dtCotizacion.Rows(0).Item("Otrascomisiones").ToString.Trim
                Me.txtProveedores.Value = dtCotizacion.Rows(0).Item("DescripcionProveedor").ToString.Trim

                Dim intMostrarteacartas As Integer = GCCUtilitario.CheckInt(dtCotizacion.Rows(0).Item("Mostrarteacartas"))
                Dim intMostrarmontocomision As Integer = GCCUtilitario.CheckInt(dtCotizacion.Rows(0).Item("Mostrarmontocomision"))

                Me.hddMostrarTea.Value = intMostrarteacartas
                Me.hddMostrarComision.Value = intMostrarmontocomision

                Me.hddFlagRetorno.Value = dtCotizacion.Rows(0).Item("FlagRetorno").ToString.Trim
                Me.hddDireccionCliente.Value = dtCotizacion.Rows(0).Item("DireccionCliente").ToString.Trim

                'Inicio IBK - RPH
                Dim intFlagCompra As Integer
                intFlagCompra = IIf(dtCotizacion.Rows(0).Item("FlagOpcionCompras") Is DBNull.Value, 0, dtCotizacion.Rows(0).Item("FlagOpcionCompras"))
                Me.hdOpCompra.Value = intFlagCompra

                Dim intFlagComActivacion As Integer
                intFlagComActivacion = IIf(dtCotizacion.Rows(0).Item("FlagComisionActivacion") Is DBNull.Value, 0, dtCotizacion.Rows(0).Item("FlagComisionActivacion"))
                Me.hdComiAct.Value = intFlagComActivacion

                Dim intFlagComEstructuracion As Integer
                intFlagComEstructuracion = IIf(dtCotizacion.Rows(0).Item("FlagComisionEstructuracion") Is DBNull.Value, 0, dtCotizacion.Rows(0).Item("FlagComisionEstructuracion"))
                Me.hdComEstruc.Value = intFlagComEstructuracion

                'If Not dtCotizacion.Rows(0).Item("ArchivoCronogramaAdjunto") Is DBNull.Value Or dtCotizacion.Rows(0).Item("ArchivoCronogramaAdjunto") = "" Then
                If Not dtCotizacion.Rows(0).Item("ArchivoCronogramaAdjunto") Is DBNull.Value Then
                    hddAdjuntarArchivo.Value = dtCotizacion.Rows(0).Item("ArchivoCronogramaAdjunto").ToString.Trim
                End If
                'Fin

                'Error
                Me.hddError.Value = msgError

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Total paginas
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/05/2012
    ''' </remarks>
    Private Shared Function TotalPaginas(ByVal total As Integer, ByVal pPageSize As Integer) As Integer
        If (total Mod pPageSize > 0) Then
            Return total \ pPageSize + 1
        Else
            Return total \ pPageSize
        End If
    End Function

    ''' <summary>
    ''' Generar Input
    ''' </summary>
    ''' <param name="argInput"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function fGenerarQuotationInput(ByRef argInput As lpcCronograma.QuotationInput, ByVal pobjEGcc_cotizacion As EGcc_cotizacion) As Boolean
        Try

            'Valida Tipo Cronograma
            Dim strTipoCronograma As String = pobjEGcc_cotizacion.Codigotipocronograma
            Dim strTipoCronogramaLcp As String = ""
            If strTipoCronograma.Trim().Equals(GCCConstante.C_CODIGO_TIPO_CRONOGRAMA_CAPITAL_CONSTANTE) Then
                strTipoCronogramaLcp = lpcCronograma.QuotationEnums.Enum_Tp_cronograma.Capital_constante
            Else
                strTipoCronogramaLcp = lpcCronograma.QuotationEnums.Enum_Tp_cronograma.Cuota_constante
            End If

            'Valida Periodicidad
            Dim strTipoPeriodicidad As String = pobjEGcc_cotizacion.Codigoperiodicidad
            Dim strTipoPeriodicidadLcp As String = ""
            If strTipoPeriodicidad.Trim().Equals(GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_ANUAL) Then
                strTipoPeriodicidadLcp = lpcCronograma.QuotationEnums.Enum_Tp_frecuencia.Anual
            ElseIf strTipoPeriodicidad.Trim().Equals(GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_MENSUAL) Then
                strTipoPeriodicidadLcp = lpcCronograma.QuotationEnums.Enum_Tp_frecuencia.Mensual
            ElseIf strTipoPeriodicidad.Trim().Equals(GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_SEMESTRAL) Then
                strTipoPeriodicidadLcp = lpcCronograma.QuotationEnums.Enum_Tp_frecuencia.Semestral
            ElseIf strTipoPeriodicidad.Trim().Equals(GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_TRIMESTRAL) Then
                strTipoPeriodicidadLcp = lpcCronograma.QuotationEnums.Enum_Tp_frecuencia.Trimestral
            ElseIf strTipoPeriodicidad.Trim().Equals(GCCConstante.C_CODIGO_TIPO_PERIODICIDAD_BIMESTRAL) Then
                strTipoPeriodicidadLcp = lpcCronograma.QuotationEnums.Enum_Tp_frecuencia.Bimestral
            End If

            'Valida FrecuenciaPago
            Dim strFrecuenciaPago As String = pobjEGcc_cotizacion.Codigofrecuenciapago
            Dim strFrecuenciaPagoLcp As Integer = 0
            If strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_ANUAL_FIJA) Or _
                strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_BIMESTRAL_FIJA) Or _
                strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_MENSUAL_FIJA) Or _
                strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_SEMESTRAL_FIJA) Or _
                strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_TRIMESTRAL_FIJA) Then
                strFrecuenciaPagoLcp = 1
            End If

            'Valida Cantidad Dias
            Dim intCantDias As Integer = 0
            If strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_ANUAL_VARIABLE) Then
                intCantDias = 360
            ElseIf strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_BIMESTRAL_VARIABLE) Then
                intCantDias = 60
            ElseIf strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_MENSUAL_VARIABLE) Then
                intCantDias = 30
            ElseIf strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_SEMESTRAL_VARIABLE) Then
                intCantDias = 180
            ElseIf strFrecuenciaPago.Trim().Equals(GCCConstante.C_CODIGO_TIPO_FRECPAGO_TRIMESTRAL_VARIABLE) Then
                intCantDias = 90
            End If

            'Setea Parametros para el Cronograma
            With argInput
                .Tp_cronograma = strTipoCronogramaLcp
                .Im_prestamo = pobjEGcc_cotizacion.Riesgoneto
                .De_tea = pobjEGcc_cotizacion.Teaporc
                .De_plazo = pobjEGcc_cotizacion.Numerocuotas

                If Not pobjEGcc_cotizacion.Fechamaxactivacion.HasValue Then
                    .Dt_desembolso = Now
                    .fechas = 1
                Else
                    .Dt_desembolso = pobjEGcc_cotizacion.Fechamaxactivacion
                End If
                If Not pobjEGcc_cotizacion.Fechaprimervencimiento.HasValue Then
                    Dim dtmFecActivacion As DateTime = Now

                    If pobjEGcc_cotizacion.Fechamaxactivacion.HasValue Then
                        dtmFecActivacion = pobjEGcc_cotizacion.Fechamaxactivacion
                    End If

                    .Dt_primer_vcmto = dtmFecActivacion.AddDays(intCantDias)
                    .fechas = 1
                Else
                    .Dt_primer_vcmto = pobjEGcc_cotizacion.Fechaprimervencimiento
                End If

                '.Il_cuota_doble = CBool(Me.selIl_cuota_doble.SelectedValue)
                .Tp_frecuencia = strTipoPeriodicidadLcp
                .Il_fijo = strFrecuenciaPagoLcp
                .De_plazo_gracia = pobjEGcc_cotizacion.Plazograciacuota
                .CostoFondo = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Costofondoporc)
                .PorcCuotaInicial = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Cuotainicialporc)
                .PrecioVenta = pobjEGcc_cotizacion.Precioventa
                .ValorIgv = pobjEGcc_cotizacion.Valorventaigv ' OJO :: Monto del IGV
                .Moneda = pobjEGcc_cotizacion.Codigomoneda
                .RiesgoNeto = pobjEGcc_cotizacion.Riesgoneto
                .MostrarTEACartas = pobjEGcc_cotizacion.Mostrarteacartas
                .MostrarOpcCompras = 1 'IIf(Me.chkMostrarOpcion.Checked = True, 1, 0) => no hay campo
                .MostrarComisionAct = pobjEGcc_cotizacion.Mostrarmontocomision
                .MostrarEstructuracionCartas = 1 'IIf(Me.chkMostrarComEstructuracion.Checked = True, 1, 0) => no hay campo
                .ImporteCuotaIni = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Importecuotainicial)
                .Observacion = pobjEGcc_cotizacion.Otrascomisiones

                If Not pobjEGcc_cotizacion.FechaOfertaValida.HasValue Then
                    .FechaValidez = Now
                Else
                    .FechaValidez = pobjEGcc_cotizacion.FechaOfertaValida
                End If


                .TipoBien = pobjEGcc_cotizacion.Codigoclasificacionbien 'Verificar
                .OpcCompra = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Importeopcioncompra)
                .ComActivacion = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Importecomisionactivacion)
                .ComEstruc = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Importecomisionestructuracion)

                .TipoSeguro = pobjEGcc_cotizacion.Codigobientiposeguro
                .Im_seguro = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Bienimporteprima)
                .De_plazo_Seg = GCCUtilitario.CheckInt(pobjEGcc_cotizacion.Biennrocuotasfinanciar)

                'If pobjEGcc_cotizacion.Codigodesgravamentiposeguro <> 0 Then
                .SeguroDes = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Desgravamenimporteprima)
                .De_plazo_Seg_Des = GCCUtilitario.CheckInt(pobjEGcc_cotizacion.Desgravamennrocuotasfinanciar)
                'End If

                'IBK - RPH se agrega el tipo de Gracia
                .TipoGracia = pobjEGcc_cotizacion.Codigotipograciacuota
                'Fin

            End With
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Prepara Cronograma para insertar
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 16/05/2012
    ''' </remarks>
    Public Shared Function PreparaCronograma(ByVal pdtbCronograma As DataTable, ByVal pstrCodigoCotizacion As String, ByVal pstrVersionCotizacion As String, ByVal pobjEGcc_cotizacion As EGcc_cotizacion, ByVal booValidaPrimeraFila As Boolean) As ListEGcc_cotizacioncronograma

        Try
            'Declara Variables
            Dim objCotizacionNTx As New LCotizacionNTx
            Dim objListECronograma As New ListEGcc_cotizacioncronograma

            'Valida Cuota Inicial
            Dim intContadorCronograma As Integer = 0
            Dim decCuotaInicial As Decimal = GCCUtilitario.CheckDecimal(pobjEGcc_cotizacion.Importecuotainicial)
            Dim blnMuestraFila As Boolean = True

            'Valida si existe
            If pdtbCronograma.Rows.Count > 0 Then

                For Each oRow As DataRow In pdtbCronograma.Rows

                    If Not decCuotaInicial > 0 And intContadorCronograma = 0 And booValidaPrimeraFila Then
                        blnMuestraFila = False
                    Else
                        blnMuestraFila = True
                    End If

                    If blnMuestraFila Then

                        Dim objECronograma As New EGcc_cotizacioncronograma
                        With objECronograma
                            .Numerocuota = GCCUtilitario.CheckInt(oRow.Item("Nu_cuota").ToString())
                            .Codigocotizacion = pstrCodigoCotizacion
                            .Versioncotizacion = pstrVersionCotizacion
                            .Fechavencimiento = GCCUtilitario.CheckDate(oRow.Item("Dt_vcmto").ToString())
                            .Cantdiascuota = GCCUtilitario.StringToInteger(oRow.Item("Nu_dias").ToString())
                            .Montosaldoadeudado = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo").ToString())
                            .Montointeresbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes").ToString())
                            .Montoprincipalbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal").ToString())
                            .Montototalcuota = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota").ToString())
                            .Saldoseguro = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro").ToString())
                            .Interessegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro").ToString())
                            .Principalsegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro").ToString())
                            .Montocuotasegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro").ToString())

                            .SaldoSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro_des").ToString())
                            .InteresSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro_des").ToString())
                            .PrincipalSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro_des").ToString())
                            .CuotaSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro_des").ToString())

                            .Totalapagar = GCCUtilitario.CheckDecimal(oRow.Item("Im_total").ToString())
                            .Montototalcuotaigv = GCCUtilitario.CheckDecimal(oRow.Item("Im_igv").ToString())

                            'Mostrar
                            .SFechavencimiento = GCCUtilitario.CheckDate(oRow.Item("Dt_vcmto").ToString())
                            .SMontosaldoadeudado = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontointeresbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontoprincipalbien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontototalcuota = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SSaldoseguro = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SInteressegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SPrincipalsegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontocuotasegurobien = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SSaldoSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_saldo_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SInteresSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_interes_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SPrincipalSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_principal_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SCuotaSeguroDes = GCCUtilitario.CheckDecimal(oRow.Item("Im_cuota_seguro_des").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .SMontototalcuotaigv = GCCUtilitario.CheckDecimal(oRow.Item("Im_igv").ToString()).ToString(GCCConstante.C_FormatMiles)
                            .STotalapagar = GCCUtilitario.CheckDecimal(oRow.Item("Im_total").ToString()).ToString(GCCConstante.C_FormatMiles)

                            .Audestadologico = 1
                            '.AudFechaRegistro = 	
                            '.AudFechaModificacion = 	
                            .Audusuarioregistro = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                            .Audusuariomodificacion = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                        End With
                        objListECronograma.Add(objECronograma)

                    End If

                    intContadorCronograma = intContadorCronograma + 1

                Next

                Return objListECronograma

            End If

            Return Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Lista Lineas
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 21/06/2012
    ''' </remarks>
    Public Shared Function ListaLineas(ByVal pstrCodUnico As String, ByRef objListELinea As ListELinea) As StringBuilder

        '-------------------------------------------
        ' Variables para Consulta
        '-------------------------------------------
        Dim strMensaje As String = "No se encontraron Lineas para el Cliente"
        Dim sbResultado As New StringBuilder
        sbResultado.Append(GCCUtilitario.ArmaComboOpcion("0", "- Seleccione -"))

        'Parametros
        Dim strCodigoUnico As String = pstrCodUnico
        Dim strCodigoProducto As String = GCCConstante.C_CODIGO_LEASING_LINEAS

        Try

            'Consulta lineas
            Dim objLWebService As New LWebService
            Dim strLineas As String = objLWebService.fObtenerLineaOP(strCodigoUnico.Trim.PadLeft(14, "0"), GCCConstante.eTipoOperacion.Linea, GCCUtilitario.CheckInt(strCodigoProducto))
            Dim strOperaciones As String = objLWebService.fObtenerLineaOP(strCodigoUnico.Trim.PadLeft(14, "0"), GCCConstante.eTipoOperacion.Operacion, GCCUtilitario.CheckInt(strCodigoProducto))

            'Lineas
            If Not strLineas Is Nothing Then
                If Not strLineas.Trim().Equals("") Then
                    Dim odtbLineas As DataTable = GCCUtilitario.DeserializeObjectXML(Of DataTable)(strLineas)
                    If Not odtbLineas Is Nothing Then
                        If odtbLineas.Rows.Count > 0 Then
                            For Each oRow As DataRow In odtbLineas.Rows
                                Dim objELinea As New ELinea
                                With objELinea
                                    .CodigoLineaOperacion = oRow.Item("CODIGOLINEAOPERACION").ToString()
                                    .NumeroLinea = oRow.Item("NUMEROLINEA").ToString()
                                    .ClaveCL = oRow.Item("CLAVECL").ToString()
                                    .RazonSocial = oRow.Item("RAZONSOCIAL").ToString()
                                End With
                                If Not objELinea.ClaveCL.Trim.Equals("") Then
                                    sbResultado.Append(GCCUtilitario.ArmaComboOpcion(objELinea.ClaveCL, objELinea.ClaveCL))
                                    objListELinea.Add(objELinea)
                                End If
                            Next oRow
                        End If
                    End If
                End If
            End If

            'Operaciones
            If Not strOperaciones Is Nothing Then
                If Not strOperaciones.Trim().Equals("") Then
                    Dim odtbOperaciones As DataTable = GCCUtilitario.DeserializeObjectXML(Of DataTable)(strOperaciones)
                    If Not odtbOperaciones Is Nothing Then
                        If odtbOperaciones.Rows.Count > 0 Then
                            For Each oRow As DataRow In odtbOperaciones.Rows
                                Dim objELineaOp As New ELinea
                                With objELineaOp
                                    .CodigoLineaOperacion = oRow.Item("CODIGOLINEAOPERACION").ToString()
                                    .NumeroLinea = oRow.Item("NUMEROLINEA").ToString()
                                    .ClaveCL = oRow.Item("CLAVECL").ToString()
                                    .RazonSocial = oRow.Item("RAZONSOCIAL").ToString()
                                End With
                                If Not objELineaOp.ClaveCL.Trim.Equals("") Then
                                    sbResultado.Append(GCCUtilitario.ArmaComboOpcion(objELineaOp.ClaveCL, objELineaOp.ClaveCL))
                                    objListELinea.Add(objELineaOp)
                                End If
                            Next oRow
                        End If
                    End If
                End If
            End If

            Return sbResultado

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Gestión Bloquero
    ''' </summary>    
    ''' <remarks>
    ''' Creado Por         : TSF - JRC
    ''' Fecha de Creación  : 18/07/2012
    ''' </remarks>
    Protected Sub GestionBloqueo(ByVal strCodigoCotizacion As String)

        Try
            'Variables
            Dim objEBloqueo As New EGCC_Bloqueo
            Dim blnNuevoBloqueo As New Boolean
            blnNuevoBloqueo = False

            'Pregunta Bloqueo
            With objEBloqueo
                .TipoDocumento = GCCConstante.C_BLOQUEO_MODULO_COTIZACION
                .Modulo = GCCConstante.C_BLOQUEO_DOC_COTIZACION
                .NumeroDocumento = strCodigoCotizacion
                .CodigoUsuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
            End With
            Dim objLUtilNTX As New LUtilNTX
            Dim dtBloqueo As DataTable = GCCUtilitario.DeserializeObject(Of DataTable)(objLUtilNTX.ObtenerBloqueo(GCCUtilitario.SerializeObject(objEBloqueo)))

            'Valida Bloqueo Existente
            If Not dtBloqueo Is Nothing Then
                If dtBloqueo.Rows.Count > 0 Then

                    Dim strUsuarioBloqueo As String = dtBloqueo.Rows(0).Item("CodigoUsuario").ToString
                    If strUsuarioBloqueo.Trim().Equals(GCCUtilitario.NullableString(GCCSession.CodigoUsuario)) Then
                        Me.hddBloqueoExistente.Value = "0"
                    Else
                        Me.hddBloqueoExistente.Value = "1"
                        Me.hddBloqueoCodigo.Value = dtBloqueo.Rows(0).Item("CodigoBloqueo").ToString
                        Me.hddBloqueoCodUsuario.Value = dtBloqueo.Rows(0).Item("CodigoUsuario").ToString
                        Me.hddBloqueoNomUsuario.Value = dtBloqueo.Rows(0).Item("NombreUsuario").ToString
                        Me.hddBloqueoFecha.Value = dtBloqueo.Rows(0).Item("FechaInicio").ToString
                    End If

                Else
                    blnNuevoBloqueo = True
                End If
            Else
                blnNuevoBloqueo = True
            End If

            'Ingresa Nuevo Bloqueo
            If blnNuevoBloqueo Then
                Me.hddBloqueoExistente.Value = "0"
                With objEBloqueo
                    .TipoDocumento = GCCConstante.C_BLOQUEO_MODULO_COTIZACION
                    .Modulo = GCCConstante.C_BLOQUEO_DOC_COTIZACION
                    .NumeroDocumento = strCodigoCotizacion
                    .CodigoUsuario = GCCUtilitario.NullableString(GCCSession.CodigoUsuario)
                    .NombreUsuario = GCCUtilitario.NullableString(GCCSession.NombreUsuario)
                    .Activo = "1"
                End With
                Dim objLUtilTX As New LUtilTX
                objLUtilTX.InsertarBloqueo(GCCUtilitario.SerializeObject(objEBloqueo))
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    'Inicio IBK - RPH
    <WebMethod()> _
   Public Shared Function GenerarArchivoCronograma(ByVal pstrCodigoCotizacion As String) As String
        Dim objLCotizacionTx As New LCotizacionTx
        Dim objEGcc_cotizacion As New EGcc_cotizacion
        Dim strRuta As String = fObtenerCarta(pstrCodigoCotizacion)
        Dim strRutaCarta As String = String.Concat(GCCUtilitario.fstrObtieneKeyWebConfig("FileServer"), strRuta)
        Dim strEGcc_cotizacion As String

        Try
            If strRutaCarta <> "" Then
                With objEGcc_cotizacion
                    .Codigocotizacion = GCCUtilitario.NullableString(pstrCodigoCotizacion)
                    .ArchivoCronogramaAdjunto = GCCUtilitario.NullableString(strRuta)
                End With
                strEGcc_cotizacion = GCCUtilitario.SerializeObject(objEGcc_cotizacion)
                'Actualizo la ruta del archivo
                Dim booResultado As Boolean = objLCotizacionTx.RegistrarRutaCronograma(strEGcc_cotizacion)
            End If


            Return "0|" + strRutaCarta
        Catch ex As Exception
            Return "1|" + ex.ToString()
        End Try

    End Function

    Private Shared Function fObtenerCarta(ByVal pstrCodigoCotizacion As String) As String
        Dim oGCC_Anexo As New GCC_Anexo

        Dim strNameFile As String = oGCC_Anexo.CronogramaCotizacionLeasing(pstrCodigoCotizacion)

        Return strNameFile
    End Function
    'Fin IBK - RPH


#End Region
    'Inicio AAE 
    Private Class GCC_Cotizacion_frmCotizacionRegistro : Inherits GCCBase

        Public Function EnviaCorreo(ByVal pstrCodigoCotizacion As String, _
                                            ByVal pstrCorreo As String, _
                                            ByVal pstrNombreCliente As String, _
                                            ByVal pstrXmlKey As String) As Boolean
            Dim mbool As Boolean
            Try
                mbool = EnviarMail(pstrCodigoCotizacion, pstrCorreo, "", pstrXmlKey, "", "", pstrNombreCliente, "", "", "", "", "", "", "")
                Return mbool
            Catch ex As Exception
                GCCUtilitario.Show(ex.Message, Me)
            End Try
        End Function
    End Class
    'Fin AAE

End Class

