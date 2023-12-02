//****************************************************************
// Variables Globales
//****************************************************************
var strComboVacio = "<option value='0'>[-Seleccione-]</option>";
var Flag = "0";
var Perfil = "";
var ValorNeto = "";
var ValorIGV = "";
var FlagSuma = '0';
var FlagTotal = '0';
var Aplicacion = '1';
var actualizarGridCompete_D = '0';

var strOperacionModal;

var strFrecPagoAnual_fija = "ANU";
var strFrecPagoAnual_variable = "AND";
var strFrecPagoBimestral_fija = "BIM";
var strFrecPagoBimestral_variable = "BID";
var strFrecPagoMensual_fija = "MEN";
var strFrecPagoMensual_variable = "MED";
var strFrecPagoSemestral_fija = "SEM";
var strFrecPagoSemestral_variable = "SED";
var strFrecPagoTrimestral_fija = "TRI";
var strFrecPagoTrimestral_variable = "TRD";

var strPeriodicidadMensual = "MEN";

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 19/09/2012
//****************************************************************
$(document).ready(function() {

    fn_validaSeccionDatosCronograma();

    //Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));
    Perfil = $("#hddPerfil").val()
    fn_cargaGrillaCuotasVencidasDetalle("A");
    fn_cargaGrillaLiquidacion("B");
    fn_cargaGrillaOtrosConceptos("C");
    fn_cargaGrillaReembolso("D");
    fn_cargaGrillaCronograma("jqGrid_lista_E");
    fn_cargaGrillaCronograma("jqGrid_lista_F");
    fn_iniGrillaDocumento("jqGrid_lista_G");

    var cm = $("#jqGrid_lista_B").jqGrid('getColProp', 'Aplicacion');
    cm.editable = false;
    cm.formatoptions = "";

    /*
    var cm = $("#jqGrid_lista_C").jqGrid('getColProp', 'Aplicacion');
    cm.editable = false;
    cm.formatoptions = "";

    var cm = $("#jqGrid_lista_D").jqGrid('getColProp', 'Aplicacion');
    cm.editable = false;
    cm.formatoptions = "";

    var cm = $("#jqGrid_lista_F").jqGrid('getColProp', 'Aplicacion');
    cm.editable = false;
    cm.formatoptions = "";
    */
    if ($("#hddCodSolicitudCredito").val() == "") {

        if ($("#cmbTipoLiquidacion").val() == "0") {
            $("#cmbTipoLiquidacion").val("P");
        }

        $("#cmbEstadoLiquidacion").val("I");

        $("#dv_botonAnular").hide();
        $("#dv_botonExtornar").hide();
        $("#dv_botonDevolver").hide();
        fn_inicializaCampos("Nuevo");

        $("#cmbPeriodicidad").val(strPeriodicidadMensual);

    } else {

        //$("#dv_botonEjecutar").hide();
        fn_inicializaCampos("Consulta");

        if ($("#cmbEstadoLiquidacion").val() == "I") {
            $("#dv_botonEjecutar").hide();
            $("#dv_botonExtornar").hide();
            $("#dv_botonDevolver").hide();
        }
        else if ($("#cmbEstadoLiquidacion").val() == "A") {
            $("#dv_botonAnular").hide();
            $("#dv_botonEnviar").hide();
            $("#dv_botonDevolver").hide();
            $("#dv_botonGrabar").hide();
            $("#dv_botonGenerar").hide();
            $("#dv_botonEjecutar").hide();
            $("#dv_botonExtornar").hide();
        }
        else if ($("#cmbEstadoLiquidacion").val() == "C") {
            $("#dv_botonGenerar").hide();
            $("#dv_botonEnviar").hide();
            $("#dv_botonGrabar").hide();
            $("#dv_botonExtornar").hide();
        }
        else if ($("#cmbEstadoLiquidacion").val() == "H") {
            $("#dv_botonAnular").hide();
            $("#dv_botonGenerar").hide();
            $("#dv_botonEnviar").hide();
            $("#dv_botonGrabar").hide();
            $("#dv_botonDevolver").hide();
        }
        else {
            $("#dv_botonAnular").hide();
            $("#dv_botonEnviar").hide();
            $("#dv_botonGrabar").hide();
            $("#dv_botonGenerar").hide();
            $("#dv_botonEjecutar").hide();
            $("#dv_botonExtornar").hide();
            $("#dv_botonDevolver").hide();
        }

        fn_obtenerLiquidacion("S");

        $("#hddFlagGenerar").val('1');
        fn_cargaGrillaDocumento();
        $("#dvBotonEliminaComentario").show();
        $("#dvBotonEditaComentario").show();
        $("#dvBotonagregaComentario").show();
        if ($("#hddFlagCuentaPropia").val() == "N") {
            fn_consultaRM();
        }
    }

    // Formateo de Montos
    $('#txtAmortizacion').validNumber({ value: '', decimals: 2, length: 15 });
    $('#txtTipoCambio').validNumber({ value: '', decimals: 3, length: 15 });

    $('#imgBsqContrato').click(function() {
        parent.fn_blockUI();

        var strCodOperacionActiva = $("#txtNroContrato").val();

        if (strCodOperacionActiva == "") {
            VentanaCreditos();
        } else {
            fn_obtenerCredito(strCodOperacionActiva);
        }

    });

    //-------------------------------------------
    //Valida Change del Periodicidad
    //-------------------------------------------
    $('#cmbPeriodicidad').change(function() {
        var strValor = $(this).val();
        fn_oc_periodicidad(strValor);
    });

    $('#txtAmortizacion').change(function() {
        fn_calcularTotales();
    });

    //-------------------------------------------
    //Calcula NroCuotas
    //-------------------------------------------
    $("#txtNroCuotas").focusout(function() {
        var strNroCuotas = $("#txtNroCuotas").val();
        if (fn_util_trim(strNroCuotas) == "") strNroCuotas = "0";
        var intNroCuotas = parseInt(strNroCuotas);
        //IBK - RPH se aunmento a 240 cuotas
        if (intNroCuotas <= 0 || intNroCuotas > 240) {
            //if (intNroCuotas <= 0 || intNroCuotas > 120) {
            $("#txtNroCuotas").val("0");
            parent.fn_util_MuestraLogPage("El Nro. de Cuotas ingresado no es correcto", "E");
        } else {
            fn_of_PlazoGracia();
        }
        //IBK - RPH
        $("#txtNroCuotas").css("color", "#464646");

    });
    //-------------------------------------------
    //Calcula PlazoGracia
    //-------------------------------------------
    $("#txtPlazoGracia").focusout(function() {
        fn_of_PlazoGracia();
        //IBK - RPH
        $("#txtPlazoGracia").css("color", "#464646");
    });

    $('#cmbTipoLiquidacion').change(function() {
        fn_validaSeccionDatosCronograma();
    });

    $('#cmbTipoCambio').change(function() {
        fn_obtenerTipoCambio();
    });

    $('#txtCUClienteCargo').change(function() {
        $('#txtNombreClienteCargo').val("");
    });
    $('#cmbCodMonedaCargo').change(function() {
        fn_obtenerCuentas();
    });
    $('#cmbTipoCuenta').change(function() {
        fn_obtenerCuentas();
    });

    //Valida Tabs
    $("div#divTabs").tabs({
        show: function(event, ui) {
            fn_doResize();
        }
    });
    //    $("#dvBotonEliminaComentario").hide();
    //    $("#dvBotonEditaComentario").hide();
    //    $("#dvBotonagregaComentario").hide();

    //On load Page (siempre al final)
    fn_onLoadPage();

});

function fn_validaSeccionDatosCronograma() {
    if ($('#cmbTipoLiquidacion').val() == "R") {
        $('#dvDatosCronograma').show();
        $('#rw_Amortizacion').show();
    }
    else {
        $('#dvDatosCronograma').hide();
        $('#rw_Amortizacion').hide();
    }
    if ($('#hddTipoTransaccion').val() == "NUEVO") {
        $('#txtAmortizacion').val("0.00");
        $('#txtNroCuotas').val("1");
        $('#txtPlazoGracia').val("0");
        $('#cmbTipoCronograma').val("003");
        $('#cmbFrecuenciaPago').val("MED");
    }
}

//****************************************************************
// Funcion		:: 	fn_seteaCamposObligatorios
// Descripción	::	Setea campos obligatorios!
// Log			:: 	IBK - RPR - 03/01/2013
//****************************************************************
function fn_seteaCamposObligatorios() {

    fn_util_SeteaObligatorio($("#txtNroContrato"), "input");
    fn_util_SeteaObligatorio($("#txtFechaValor"), "input");
    //fn_util_SeteaObligatorio($("#txtFechaProceso"), "input");
    fn_util_SeteaObligatorio($("#cmbTipoCambio"), "input");
    fn_util_SeteaObligatorio($("#txtTipoCambio"), "input");
    fn_util_SeteaObligatorio($("#cmbTipoLiquidacion"), "input");

    fn_util_SeteaObligatorio($('#txtCUClienteCargo'), "input");
    fn_util_SeteaObligatorio($("#cmbCodMonedaCargo"), "input");
    fn_util_SeteaObligatorio($("#cmbNroCuenta"), "input");
    fn_util_SeteaObligatorio($("#cmbTipoCuenta"), "input");

    //Reprogramacion:
    fn_util_SeteaObligatorio($("#txtAmortizacion"), "input");
    fn_util_SeteaObligatorio($("#txtFechaPrimerVencimiento"), "input");
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			::  IBK - RPR - 03/01/2013
//****************************************************************
function fn_inicializaCampos(strOperacion) {

    //Cabecera de Contrato
    fn_util_inactivaInput("txtCuCliente", "I");
    fn_util_inactivaInput("txtRazonSocial", "I");
    fn_util_inactivaInput("txtTipoPersona", "I");
    fn_util_inactivaInput("txtTipoDocumento", "I");
    fn_util_inactivaInput("txtNumeroDocumento", "I");
    fn_util_inactivaInput("txtTipoContrato", "I");
    fn_util_inactivaInput("txtMoneda", "I");
    fn_util_inactivaInput("txtEjecutivoLeasing", "I");
    fn_util_inactivaInput("txtNombreBanca", "I");
    fn_util_inactivaInput("txtClasificacionBien", "I");
    fn_util_inactivaInput("txtNombreSectorista", "I");
    fn_util_inactivaInput("txtPorcenTasaActiva", "I");
    fn_util_inactivaInput("txtEstadoContrato", "I");
    fn_util_inactivaInput("txtNombreEstadoOperacionActiva", "I");

    $('#txtNroContrato').validText({ type: 'number', length: 8 });
    $('#txtCuCliente').validText({ type: 'number', length: 10 });
    $('#txtCUClienteCargo').validText({ type: 'number', length: 10 });

    fn_util_inactivaInput("cmbEstadoLiquidacion", "S");
    fn_util_inactivaInput("txtMonedaLiquidacion", "I");

    //Datos del Pago
    fn_util_inactivaInput("txtCodigoLiquidacion", "I");
    fn_util_inactivaInput("txtTipoLiquidacion", "I");
    fn_util_inactivaInput("txtNumSecRecuperacion", "I");
    fn_util_inactivaInput("txtCodAutorizacionRecuperacion", "I");
    fn_util_inactivaInput("txtValorNeto", "I");
    fn_util_inactivaInput("txtIGV", "I");
    fn_util_inactivaInput("txtTotal", "I");

    //Vias de cobranza no soportadas
    $("#rdbViaCondonacion").hide();
    $("#rdbViaVentanilla").hide();

    //Estado del Pago
    $('#txtFechaProceso').addClass("css_input_inactivo");
    $('#txtFechaProceso').datepicker().datepicker('disable');

    //Cargo en Cuenta
    fn_util_inactivaInput("txtNombreClienteCargo", "I");

    //Ventanilla
    fn_util_inactivaInput("txtCodOperacionGINA", "I");
    $('#txtFechaProcesoPago').datepicker().datepicker('disable');
    fn_util_inactivaInput("txtCodTerminalPago", "I");
    fn_util_inactivaInput("cmbTiendaPago", "S");
    fn_util_inactivaInput("txtCodUsuarioPago", "I");
    fn_util_inactivaInput("txtCodModoPago", "I");
    fn_util_inactivaInput("txtCodModoPago2", "I");

    fn_seteaCamposObligatorios();

    // Gestion de Procesos
    if (Perfil == "10") {
        $('#dv_botonEjecutar').show();
        $('#dv_botonExportar').show();
        $('#dv_botonAnular').show();
        $('#dv_botonExtornar').show();

        $("#dv_botonEnviar").hide();
        $("#dv_botonGrabar").hide();
        $("#dv_botonGenerar").hide();
    }
    else {
        $('#dv_botonEjecutar').hide();
        $('#dv_botonExtornar').hide();
        $("#dv_botonDevolver").hide();
    }

    if ($('#hddTipoTransaccion').val() == "NUEVO") {
        $("#dv_botonExtornar").hide();

        fn_setFlagCuentaPropia("S", "Nuevo");
    }
    else {

        fn_setFlagCuentaPropia($("#hddFlagCuentaPropia").val(), "Consulta");

        if ($('#hddTipoTransaccion').val() == "EDITAR") {
            fn_util_inactivaInput("txtNroContrato", "I");
            $('#imgBsqContrato').hide();
        }

        //$('#txtFechaValor').addClass("css_input_inactivo");
        //$('#txtFechaValor').datepicker().datepicker('disable');
        //fn_util_inactivaInput("cmbNroCuotas", "S");
        //fn_util_inactivaInput("cmbMovimientoBasilea", "S");

        //Via de Cobranza

        fn_setTipoViaCobranza($("#hddTipoViaCobranza").val());
        /*
        fn_util_inactivaInput("rdbViaCuenta", "S");
        fn_util_inactivaInput("rdbViaVentanilla", "S");
        fn_util_inactivaInput("rdbViaCondonacion", "S");
        fn_util_inactivaInput("rdbViaAdministrativo", "S");

        fn_util_inactivaInput("rdbCuentaPropia", "S");
        fn_util_inactivaInput("rdbOtraCuenta", "S");

        fn_util_inactivaInput("txtCUClienteCargo", "I");
        fn_util_inactivaInput("txtNombreClienteCargo", "I");
        $('#imgBsqClienteRM').hide();
        fn_util_inactivaInput("cmbCodMonedaCargo", "S");
        fn_util_inactivaInput("cmbTipoCuenta", "S");
        fn_util_inactivaInput("cmbNroCuenta", "S");
        */
    }

}

//****************************************************************
// Funcion		:: 	fn_setTipoViaCobranza
// Descripción	::	Setea el Tipo de Via de Cobranza
// Log			:: 	IBK - RPR - 26/12/2012
//****************************************************************
function fn_setTipoViaCobranza(pstrTipoViaCobranza) {
    $("#hddTipoViaCobranza").val(pstrTipoViaCobranza);
    if (pstrTipoViaCobranza == "C") {
        $('input[id=rdbViaCuenta]').attr('checked', true);
        fn_setCobranzaCuenta(true);

    }
    else if (pstrTipoViaCobranza == "V") {
        $('input[id=rdbViaVentanilla]').attr('checked', true);
        fn_setCobranzaCuenta(false);
    }
    else if (pstrTipoViaCobranza == "D") {
        $('input[id=rdbViaCondonacion]').attr('checked', true);
        fn_setCobranzaCuenta(false);
    }
    else if (pstrTipoViaCobranza == "A") {
        $('input[id=rdbViaAdministrativo]').attr('checked', true);
        fn_setCobranzaCuenta(false);
    }
    else {
        $('input[id=rdbViaCuenta]').attr('checked', false);
        $('input[id=rdbViaVentanilla]').attr('checked', false);
        $('input[id=rdbViaCondonacion]').attr('checked', false);
        $('input[id=rdbViaAdministrativo]').attr('checked', false);
        fn_setCobranzaCuenta(false);
    }
}

//****************************************************************
// Funcion		:: 	fn_setCobranzaCuenta
// Descripción	::	Habilita / deshabilita campos para cargo en cuenta
// Log			:: 	IBK - RPR - 07/01/2013
//****************************************************************
function fn_setCobranzaCuenta(flagHabilitar) {
    if (flagHabilitar == true) {
        $('#tb_CargoCuenta').show();
    }
    else {
        $('#tb_CargoCuenta').hide();
    }
}

//****************************************************************
// Funcion		:: 	fn_setFlagCuentaPropia
// Descripción	::	Setea el Flag
// Log			:: 	IBK - RPR - 14/01/2012
//****************************************************************
function fn_setFlagCuentaPropia(pstrFlag, strOperacion) {

    if (strOperacion == "Nuevo" && ($("#hddFlagCuentaPropia").val() != pstrFlag)) {
        $('#txtCUClienteCargo').val("");
        $('#txtNombreClienteCargo').val("");

        $('#cmbCodMonedaCargo').val(0);
        $('#cmbTipoCuenta').val(0);
        $('#cmbNroCuenta').html(strComboVacio);
    }

    $("#hddFlagCuentaPropia").val(pstrFlag);

    if (pstrFlag == "N") {
        $('#rowCUClienteCargo').show();
        $('input[id=rdbOtraCuenta]').attr('checked', true);
    }
    else {
        $('#rowCUClienteCargo').hide();
        $('input[id=rdbCuentaPropia]').attr('checked', true);
    }
}

//****************************************************************
// Funcion		:: 	fn_PonerDatosContrato
// Descripción	::	Poner Datos del Contrato luego de la consulta x Cod Contrato
// Log			:: 	IBK - RPR - 25/12/2012
//****************************************************************

function VentanaCreditos() {
    parent.fn_util_AbreModal("Pagos :: Pago Cuotas", "Pagos/frmCreditoConsulta.aspx", 850, 600, function() { });
}
function fn_obtenerCredito(pCredito) {

    //Limpiar Cabecera
    $('#txtCuCliente').val("");
    $('#txtRazonSocial').val("");
    $('#txtTipoPersona').val("");
    $('#txtTipoDocumento').val("");
    $('#txtNumeroDocumento').val("");
    $('#txtTipoContrato').val("");
    $('#txtMoneda').val("");
    $('#txtEjecutivoLeasing').val("");
    $('#txtNombreBanca').val("");
    $('#txtClasificacionBien').val("");
    $('#txtNombreSectorista').val("");
    $('#txtPorcenTasaActiva').val("");
    $('#txtEstadoContrato').val("");
    $('#txtNombreEstadoOperacionActiva').val("");

    //Valores
    if ($('#hddTipoTransaccion').val() == "EXTORNO") {
        fn_obtenerOperacionExtornable(pCredito);
    }
    else {
        var paramArray = ["strCodSolicitudCredito", pCredito];

        fn_util_AjaxWM("frmPagoCuotasRegistro.aspx/ConsultaContrato",
                       paramArray,
                       fn_PonerDatosContrato,
                       function(resultado) {
                           parent.fn_unBlockUI();
                           parent.fn_mdl_mensajeIco("Se produjo un error al obtener los datos del contrato." + resultado.responseText, "util/images/error.gif", "ERROR EN CONSULTA");
                       }
                );
    }

    //Resize Pantalla
    fn_doResize();
}

function fn_PonerDatosContrato(response) {
    //EstadoOperacionActiva not in ('A', 'C', 'D') and EstadoOperacionActiva is not null and
    var objEContrato = response;

    if (objEContrato.CodError == 1) {
        $('#hddCodSolicitudCredito').val("");
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objEContrato.MsgError, "util/images/error.gif", "ERROR EN CONSULTA");
    } else {
        if (objEContrato.EstadoOperacionActiva == '' || objEContrato.EstadoOperacionActiva == 'A' || objEContrato.EstadoOperacionActiva == 'C' || objEContrato.EstadoOperacionActiva == 'D') {
            parent.fn_unBlockUI();
            parent.fn_mdl_mensajeIco('El crédito no se puede liquidar porque no está activo.', "util/images/error.gif", "ERROR EN CONSULTA");
        }
        else {
            $('#hddCodSolicitudCredito').val(objEContrato.Codsolicitudcredito);
            $('#txtCuCliente').val(objEContrato.Codunico);
            $('#txtRazonSocial').val(objEContrato.ClienteRazonSocial);
            $('#txtTipoPersona').val(objEContrato.NombreTipoPersona);
            $('#txtTipoDocumento').val(objEContrato.NombreTipoDocIdentificacion);
            $('#txtNumeroDocumento').val(objEContrato.NroDocIdentificacion);
            $('#txtTipoContrato').val(objEContrato.SubTipoContrato);
            $('#txtMoneda').val(objEContrato.NombreMonedaAPP);
            $('#txtMonedaLiquidacion').val(objEContrato.NombreMonedaAPP);
            $('#txtEjecutivoLeasing').val(objEContrato.NombreEjecutivoLeasing);

            $('#txtNombreBanca').val(objEContrato.NombreBanca);
            $('#txtClasificacionBien').val(objEContrato.ClasificacionBien);
            var strTEA = objEContrato.PorcenTasaActiva;
            $("#txtPorcenTasaActiva").val(Fn_util_ReturnValidDecimal2(strTEA.toString()));
            $('#txtEstadoContrato').val(objEContrato.Estadosolicitudcredito);
            $('#txtNombreEstadoOperacionActiva').val(objEContrato.NombreEstadoOperacionActiva);

            $('#hddCodMonedaContrato').val(objEContrato.Codmoneda);
            $('#cmbCodMoneda').val(objEContrato.Codmoneda);
            $('#txtNroContrato').val(objEContrato.Codsolicitudcredito);
            $('#cmbTipoCambio').val('PRF');

            parent.fn_unBlockUI();

            //Log
            parent.fn_util_MuestraLogPage("El sistema ubicó el N° Contrato: " + objEContrato.Codsolicitudcredito + ". Se muestran los datos.", "I");
            fn_obtenerTipoCambio();
        }
    }


    fn_doResize();
}

function fn_obtenerTipoCambio() {

    var codTipoModalidadCambio = $('#cmbTipoCambio').val();

    if (codTipoModalidadCambio == "0" || codTipoModalidadCambio == "ESP") {
        $("#txtTipoCambio").val("");
    }
    else {

        var arrParametros = ["strCodMoneda", "002",
                             "strFecha", $("#txtFechaProceso").val(),
                             "strTipoModalidadCambio", codTipoModalidadCambio
                             ];

        parent.fn_blockUI();

        fn_util_AjaxSyncWM("frmLiquidacionesRegistro.aspx/ObtenerTipoCambio",
			arrParametros,
			function(objEMonedaTipoCambio) {
			    if (objEMonedaTipoCambio.CodError != 0) {
			        parent.fn_unBlockUI();
			        $("#txtTipoCambio").val("");
			        parent.fn_mdl_mensajeIco("No existe T.C. para la fecha " + $("#txtFechaProceso").val(), "util/images/error.gif", "Validacion de Datos");
			    } else {
			        parent.fn_unBlockUI();
			        if ($("#hddCodMonedaContrato").val("002")) {
			            $("#txtTipoCambio").val(Fn_util_ReturnValidDecimal2(objEMonedaTipoCambio.Montovalorventa.toString()));
			        } else {
			            $("#txtTipoCambio").val(Fn_util_ReturnValidDecimal2(objEMonedaTipoCambio.Montovalorcompra.toString()));
			        }
			    }
			    fn_doResize();
			},
			function(resultado) {
			    parent.fn_unBlockUI();
			    var error = eval("(" + resultado.responseText + ")");
			    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al Grabar la Liquidacion");
			}
         );
    }

}

function fn_generarLiquidacion() {
    fn_obtenerLiquidacion("L");
    Flag = '1';
}

function fn_grabarLiquidacion(flagEnviar) {

    if ($("#hddFlagGenerar").val() == '0') {
        parent.fn_mdl_mensajeIco("Primero debe generar la liquidación.", "util/images/warning.gif", "Validación de Datos");
    }
    else {
        var strCodigoLiquidacion = $('#txtCodigoLiquidacion').val();
        var strTipoLiquidacion = $('#cmbTipoLiquidacion').val();

        var arrParametros = ["pstrCodOperacionActiva", $("#hddCodSolicitudCredito").val(),
                             "pstrCodigoLiquidacion", strCodigoLiquidacion,
                             "pstrTipoLiquidacion", strTipoLiquidacion,
                             "pstrFechaValor", $("#txtFechaValor").val(),
                             "pstrFechaProceso", $("#txtFechaProceso").val(),
                             "pstrCodTipoCambio", $("#cmbTipoCambio").val(),
                             "pstrTipoCambio", fn_util_ValidaDecimal($("#txtTipoCambio").val()),
                             "pstrPorcIGV", fn_util_ValidaDecimal($("#hddPorcIGV").val()),
                             "pstrTipoCronograma", $("#cmbTipoCronograma").val(),
                             "pstrNroCuotas", $("#txtNroCuotas").val(),
                             "pstrPeriodicidad", $("#cmbPeriodicidad").val(),
                             "pstrFrecuenciaPago", $("#cmbFrecuenciaPago").val(),
                             "pstrPlazoGracia", $("#txtPlazoGracia").val(),
                             "pstrTipoGracia", $("#cmbTipoGracia").val(),
                             "pstrFechaPrimerVencimiento", $("#txtFechaPrimerVencimiento").val(),
                             "pstrAmortizacionCapital", $("#txtAmortizacion").val(),
                             "pstrValorNeto", fn_util_ValidaDecimal($("#txtValorNeto").val()),
                             "pstrMontoIGV", fn_util_ValidaDecimal($("#txtIGV").val()),
                             "pstrMontoTotal", fn_util_ValidaDecimal($("#txtTotal").val()),
                             "pstrFlagAdenda", $("#cbFlagAdenda").is(':checked'),
                             "pstrTipoViaCobranza", $("#hddTipoViaCobranza").val(),
                             "pstrFlagCuentaPropia", $("#hddFlagCuentaPropia").val(),
                             "pstrCodUnicoClienteCargo", ($('#hddFlagCuentaPropia').val() == 'S' ? $("#txtCuCliente").val() : $("#txtCUClienteCargo").val()),
                             "pstrCodMonedaCargo", $("#cmbCodMonedaCargo").val(),
                             "pstrTipoCuenta", $("#cmbTipoCuenta").val(),
                             "pstrNroCuenta", $("#cmbNroCuenta").val()];

        parent.fn_blockUI();

        fn_util_AjaxSyncWM("frmLiquidacionesRegistro.aspx/GrabarLiquidacion",
			 arrParametros,
			 fn_retornoIngresoLiquidacion,
			function(resultado) {
			    parent.fn_unBlockUI();
			    var error = eval("(" + resultado.responseText + ")");
			    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al Grabar la Liquidacion");
			}
            );

        if (flagEnviar == '1') {
            parent.fn_mdl_confirma(
                "¿Está seguro que desea enviar la Liquidación a Ejecución?.",
		        function() {
		            var strTipoRecuperacion = "R";
		            var arrParametros = ["pstrCodigoLiquidacion", strCodigoLiquidacion,
								         ];

		            parent.fn_blockUI();

		            fn_util_AjaxSyncWM("frmLiquidacionesRegistro.aspx/EnviarLiquidacion",
						         arrParametros,
						         fn_retornoEnvioLiquidacion,
	                            function(resultado) {
	                                parent.fn_unBlockUI();
	                                var error = eval("(" + resultado.responseText + ")");
	                                parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al enviar Liquidacion");
	                            }
			        );

		        },
                "Util/images/question.gif",
                function() { },
                'Liquidaciones SGL');
        }
    }

}

function fn_retornoIngresoLiquidacion(response) {

    var objEGCC_Liquidacion = response;

    if (objEGCC_Liquidacion.CodError != 0) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objEGCC_Liquidacion.MsgError, "util/images/error.gif", "Error al grabar la Liquidacion");
    } else {

        parent.fn_unBlockUI();

        $('#txtCodigoLiquidacion').val(objEGCC_Liquidacion.CodigoLiquidacion);

        //Log
        parent.fn_util_MuestraLogPage("Se graboó la Liquidación N°: " + objEGCC_Liquidacion.CodigoLiquidacion, "I");
    }

    fn_doResize();
}

//****************************************************************
// Funcion		:: 	fn_cargaGrillaDetalleCuotas
// Descripción	::	Carga Grilla
// Log			:: 	IBK - RPR - 27/12/2012
//****************************************************************
function fn_cargaGrillaLiquidacion(strGrilla) {

    var grid_table_id = "jqGrid_lista_" + strGrilla;
    var grid_pager_id = "jqGrid_pager_" + strGrilla;
    var lastSel = -1;
    $("#" + grid_table_id).jqGrid({
        datatype: function() { },
        jsonReader:
        {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Concepto', 'Dias', 'Valor Neto', 'IGV', 'Total', 'Aplicacion', '', '', '', '', '', ''],
        colModel: [
		        { name: 'Concepto', index: 'Concepto', align: "center" },
		        { name: 'Dias', index: 'Dias', align: "center" },
		        { name: 'ValorNeto', index: 'ValorNeto', align: "right", editable: true, formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_UnformatDecimal },
		        { name: 'ValorIGV', index: 'ValorIGV', align: "right", editable: true, formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_UnformatDecimal },
		        { name: 'Total', index: 'Total', align: "right", editable: true, formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_UnformatDecimal },
		        { name: 'Aplicacion', index: 'Aplicacion', editable: true, edittype: 'checkbox', editoptions: { value: "True:False" }, formatter: "checkbox", formatoptions: { disabled: false }, align: "center" },
		        { name: 'CodOperacionActiva', index: 'CodOperacionActiva', hidden: true },
		        { name: 'TipoRecuperacion', index: 'TipoRecuperacion', hidden: true },
		        { name: 'NumSecRecuperacion', index: 'NumSecRecuperacion', hidden: true },
		        { name: 'CodUsuario', index: 'CodUsuario', hidden: true },
		        { name: 'ValorNetoOriginal', index: 'ValorNetoOriginal', align: "right", hidden: true, formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_UnformatDecimal },
		        { name: 'ValorIGVOriginal', index: 'ValorIGVOriginal', align: "right", hidden: true, formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_UnformatDecimal}],


        width: glb_intWidthPantalla - 120,
        height: '100%',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 99999,
        sortname: 'Codigocotizacion',
        sortorder: 'desc',
        pager: "#" + grid_pager_id,
        footerrow: true,
        viewrecords: true,
        gridview: true,
        editurl: 'clientArray',
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            if (id && id !== lastSel) {
                $("#" + grid_table_id).jqGrid('restoreRow', lastSel);
                lastSel = id;
            }
        },
        ondblClickRow: function(id, ri, ci) {
            //$("#" + grid_table_id).jqGrid('editRow', id, { keys: true, aftersavefunc: fn_seleccionarAplicacionNuevasComisiones(grid_table_id)});

            var datarow = $("#" + grid_table_id).jqGrid('getRowData', id);
            ValorIGV = (datarow.ValorIGV == '0' ? datarow.ValorIGVOriginal : datarow.ValorIGV);
            ValorNeto = (datarow.ValorNeto == '0' ? datarow.ValorNetoOriginal : datarow.ValorNeto);
            Flag = '1';
            $("#" + grid_table_id).jqGrid('editRow', id, true, null, null, null, {}, aftersavefunc);
            return;
        }
        //        gridComplete: function(id) {
        //            jQuery("#" + grid_table_id + " .jqgrow td input").each(function() {
        //                jQuery(this).click(function() {
        //                    fn_seleccionarCreditoVigente(grid_table_id);
        //                });
        //            });
        //            fn_doResize();
        //        }
    });
    jQuery("#" + grid_table_id).jqGrid('navGrid', "#" + grid_pager_id, { edit: false, add: false, del: false });
    $("#search_" + grid_table_id).hide();

    function aftersavefunc(result) {
        var datarow = $("#" + grid_table_id).jqGrid('getRowData', result);
        fn_seleccionarCreditoVigente(grid_table_id);
        //$("#" + grid_table_id).trigger("reloadGrid");
        fn_ActualizaTotal(grid_table_id, result, ValorIGV, ValorNeto, datarow.CodOperacionActiva, datarow.TipoRecuperacion, datarow.NumSecRecuperacion, datarow.CodUsuario, 'true')
        $("#" + grid_table_id).trigger("reloadGrid");
    }
}

function fn_cargaGrillaOtrosConceptos(strGrilla) {

    var grid_table_id = "jqGrid_lista_" + strGrilla;
    var grid_pager_id = "jqGrid_pager_" + strGrilla;

    $("#" + grid_table_id).jqGrid({
        datatype: function() { },
        jsonReader:
        {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "NumSecRecuperacion"
        },
        colNames: ['NumSecRecuperacion', 'Concepto', 'Fecha Vcto', 'Reembolso', 'IGV Reembolso', 'Comision', 'Int. Moratorio', 'IGV Int. Moratorio', 'Interes', 'IGV Comision', 'IGV', 'Total', 'Aplicacion Interes'],
        colModel: [
                { name: 'NumSecRecuperacion', index: 'NumSecRecuperacion', hidden: true },
		        { name: 'Concepto', index: 'Concepto', align: "left" },
		        { name: 'FechaValorRecuperacion', index: 'FechaValorRecuperacion', align: "center", formatter: fn_util_ValidaStringFecha },
		        { name: 'MontoReembolso', index: 'MontoReembolso', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		        { name: 'MontoIGVReembolso', index: 'MontoIGVReembolso', hidden: true, formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		        { name: 'MontoComision', index: 'MontoComision', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		        { name: 'Interes', index: 'Interes', hidden: true, align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		        { name: 'InteresIGV', index: 'InteresIGV', hidden: true, formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		        { name: 'InteresTotal', index: 'InteresTotal', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		        { name: 'MontoIGV', index: 'MontoIGV', hidden: true, align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		        { name: 'MontoIGVTotal', index: 'MontoIGVTotal', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		        { name: 'Total', index: 'Total', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		        { name: 'Aplicacion', index: 'Aplicacion', editable: true, edittype: 'checkbox', editoptions: { value: "True:False" }, formatter: "checkbox", formatoptions: { disabled: false }, align: "center" }, ],

        width: glb_intWidthPantalla - 120,
        height: '100%',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 99999,
        sortname: 'NumSecRecuperacion',
        sortorder: 'desc',
        pager: "#" + grid_pager_id,
        viewrecords: true,
        gridview: true,
        footerrow: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) { },
        ondblClickRow: function(id) { },
        gridComplete: function(id) {
            jQuery("#" + grid_table_id + " .jqgrow td input").each(function() {
                jQuery(this).click(function() {
                    fn_seleccionarAplicacionOtroConcepto(grid_table_id);
                });
            });
            fn_doResize();
        }
    });
    jQuery("#" + grid_table_id).jqGrid('navGrid', "#" + grid_pager_id, { edit: false, add: false, del: false });
    $("#search_" + grid_table_id).hide();
}

function fn_cargaGrillaReembolso(strGrilla) {

    var grid_table_id = "jqGrid_lista_" + strGrilla;
    var grid_pager_id = "jqGrid_pager_" + strGrilla;

    var lastSel = -1;
    $("#" + grid_table_id).jqGrid({
        datatype: function() { },
        jsonReader:
        {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Concepto', 'Valor Neto', 'IGV', 'Total', 'Aplicacion', '', '', '', ''],
        colModel: [
		        { name: 'Concepto', index: 'Concepto', align: "left" },
		        { name: 'ValorNeto', index: 'ValorNeto', align: "right", editable: true, formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_UnformatDecimal },
		        { name: 'ValorIGV', index: 'ValorIGV', align: "right", editable: true, formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_UnformatDecimal },
		        { name: 'Total', index: 'Total', align: "right", editable: true, formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_UnformatDecimal },
		        { name: 'Aplicacion', index: 'Aplicacion', editable: true, edittype: 'checkbox', editoptions: { value: "true:false" }, formatter: "checkbox", formatoptions: { disabled: false }, align: "center" },
		        { name: 'CodOperacionActiva', index: 'CodOperacionActiva', hidden: true },
		        { name: 'TipoRecuperacion', index: 'TipoRecuperacion', hidden: true },
		        { name: 'NumSecRecuperacion', index: 'NumSecRecuperacion', hidden: true },
		        { name: 'CodUsuario', index: 'CodUsuario', hidden: true}],

        width: glb_intWidthPantalla - 120,
        height: '100%',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 99999,
        sortname: 'Codigocotizacion',
        sortorder: 'desc',
        pager: "#" + grid_pager_id,
        footerrow: true,
        viewrecords: true,
        gridview: true,
        editurl: 'clientArray',
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            if (id && id !== lastSel) {
                $("#" + grid_table_id).jqGrid('restoreRow', lastSel);
                lastSel = id;
            }
        },
        ondblClickRow: function(id, ri, ci) {
            //$("#" + grid_table_id).jqGrid('editRow', id, { keys: true, aftersavefunc: fn_seleccionarAplicacionNuevasComisiones(grid_table_id)});
            var datarow = $("#" + grid_table_id).jqGrid('getRowData', id);
            $("#" + grid_table_id).jqGrid('editRow', id, true, null, null, null, {}, aftersavefunc);
            ValorIGV = datarow.ValorIGV;
            ValorNeto = datarow.ValorNeto;
            Flag = '1';
            return;
        },
        gridComplete: function(id) {
            setearGridComplete(grid_table_id);
        }
    });
    jQuery("#" + grid_table_id).jqGrid('navGrid', "#" + grid_pager_id, { edit: false, add: false, del: false });
    $("#search_" + grid_table_id).hide();

    function aftersavefunc(result) {
        actualizarGridCompete_D = '1';

        var datarow = $("#" + grid_table_id).jqGrid('getRowData', result);
        fn_seleccionarAplicacionNuevasComisiones(grid_table_id);
        fn_ActualizaTotal2(grid_table_id, result, ValorIGV, ValorNeto, datarow.CodOperacionActiva, datarow.TipoRecuperacion, datarow.NumSecRecuperacion, datarow.CodUsuario, 'true')
        $("#" + grid_table_id).trigger("reloadGrid");
    }
}

function setearGridComplete(grid_table_id) {
    jQuery("#" + grid_table_id + " .jqgrow td input").each(function() {
        jQuery(this).click(function() {
            fn_seleccionarAplicacionNuevasComisiones(grid_table_id);

            //var value = ($(this).prop('checked')) ? "True" : "False";
            var iRow = $("#" + grid_table_id).getInd($(this).parent('td').parent('tr').attr('id'));
            var datarow = $("#" + grid_table_id).jqGrid('getRowData', iRow);
            fn_ActualizaAplicacion(datarow.CodOperacionActiva, datarow.TipoRecuperacion, datarow.NumSecRecuperacion, datarow.CodUsuario, datarow.Aplicacion)
        });
    });
    fn_doResize();
}

function fn_cargaGrillaCuotasVencidasDetalle(strGrilla) {

    var grid_table_id = "jqGrid_lista_" + strGrilla;
    var grid_pager_id = "jqGrid_pager_" + strGrilla;

    $("#" + grid_table_id).jqGrid({
        datatype: function() { },
        jsonReader:
    {
        root: "Items",
        page: "CurrentPage",
        total: "PageCount",
        records: "RecordCount",
        repeatitems: false,
        id: "NumCuotaCalendario"
    },
        colNames: ['Cuota', 'Fec. Venc.', 'Días Vencidos', 'Principal', 'Interés Vigente', 'Principal Seguro', 'Interes Seguro', 'IGV', 'Total Cuota', 'Comisiones', 'Total'],
        colModel: [
	        { name: 'NumCuotaCalendario', index: 'NumCuotaCalendario', align: "center" },
	        { name: 'FechaVencimientoCuota', index: 'FechaVencimientoCuota', align: "center", formatter: fn_util_ValidaStringFecha },
	        { name: 'DiasVenc', index: 'DiasVenc', align: "center" },
	        { name: 'MontoPrincipal', index: 'MontoPrincipal', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
	        { name: 'MontoInteres', index: 'MontoInteres', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
	        { name: 'MontoPrincipalSeguro', index: 'MontoPrincipalSeguro', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
	        { name: 'MontoInteresSeguro', index: 'MontoInteresSeguro', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
	        { name: 'MontoIGV', index: 'MontoIGV', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
	        { name: 'Total', index: 'Total', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
            { name: 'Comisiones', index: 'Comisiones', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
	        { name: 'TotalPago', index: 'TotalPago', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal }
	         ],

        width: glb_intWidthPantalla - 120,
        height: '100%',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 99999,
        sortname: 'NumCuotaCalendario',
        sortorder: 'desc',
        pager: "#" + grid_pager_id,
        viewrecords: true,
        footerrow: true,
        gridview: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) { },
        ondblClickRow: function(id) { },
        gridComplete: function(id) { },
        subGrid: true,
        subGridOptions: {
            "openicon": "ui-icon-arrowreturn-1-e"
        },
        subGridRowExpanded: function(subgrid_id, row_id) {
            var rowDataPadre = $("#" + grid_table_id).jqGrid('getRowData', row_id);

            var subgrid_table_id, pager_id;
            subgrid_table_id = subgrid_id + "_t";
            pager_id = "p_" + subgrid_table_id;

            $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pager_id + "'></div>");

            var gstrGrid = subgrid_table_id;
            var gstrCodPadre = rowDataPadre.NumCuotaCalendario;

            jQuery("#" + subgrid_table_id).jqGrid({
                datatype: function() {
                    fn_CargarCuotaAtrasadaComision(subgrid_table_id, rowDataPadre.NumCuotaCalendario);
                },
                jsonReader:
				{
				    root: "Items",
				    page: "CurrentPage",
				    total: "PageCount",
				    records: "RecordCount",
				    repeatitems: false,
				    id: "NumSecRecupComi"
				},
                colNames: ['NumCuotaCalendario', 'Tarifa', 'Tipo', 'Aplicacion', '%', 'Monto', 'Aplicacion', ''],
                colModel: [
                        { name: 'NumCuotaCalendario', index: 'NumCuotaCalendario', align: "left", hidden: true },
		                { name: 'DescripComision', index: 'DescripComision', align: "left" },
		                { name: 'DescripTipoValor', index: 'DescripTipoValor', align: "left" },
		                { name: 'DescripTipoAplicacion', index: 'DescripTipoAplicacion', align: "left" },
		                { name: 'Porcentaje', index: 'Estado', align: "right" },
		                { name: 'Monto', index: 'Monto', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		                { name: 'Aplicacion', index: 'Aplicacion', editable: true, edittype: 'checkbox', editoptions: { value: "True:False" }, formatter: "checkbox", formatoptions: { disabled: false }, align: "center" },
                        { name: '', index: '', width: 2, align: "center" }
                ],
                width: glb_intWidthPantalla - 105,
                height: '100%',
                editurl: 'clientArray',
                pager: pager_id,
                editurl: 'clientArray',
                loadtext: 'Cargando datos...',
                emptyrecords: 'No hay resultados',
                rowNum: 99999,
                sortname: 'NumSecRecupComi',
                sortorder: 'desc',
                viewrecords: true,
                gridview: true,
                altRows: true,
                altclass: 'gridAltClass',
                multiselect: false,
                gridComplete: function(id) {
                    jQuery("#" + subgrid_table_id + " .jqgrow td input").each(function() {
                        jQuery(this).click(function() {
                            fn_seleccionarAplicacionComision(grid_table_id, subgrid_table_id);
                        });
                    });
                    fn_doResize();
                }
            });

            jQuery("#" + subgrid_table_id).jqGrid('navGrid', "#" + pager_id, { edit: false, add: false, del: false, search: false })

        }
    });
    jQuery("#" + grid_table_id).jqGrid('navGrid', "#" + grid_pager_id, { edit: false, add: false, del: false });
    $("#search_" + grid_table_id).hide();

}

function fn_seleccionarAplicacionComision(strNombreGrilla, subgrid_table_id) {
    var lista = jQuery("#" + subgrid_table_id).getDataIDs();
    var totalComisiones = 0, numCuotaCalendario = -1;
    for (var i = 0; i < lista.length; i++) {

        var strAplicacion = "1";
        if ($("#" + subgrid_table_id).jqGrid('getCell', lista[i], 'Aplicacion') == "False") {
            strAplicacion = "0";
        }
        else {
            totalComisiones += $("#" + subgrid_table_id).jqGrid('getCell', lista[i], 'Monto');
        }

        numCuotaCalendario = $("#" + subgrid_table_id).jqGrid('getCell', lista[i], 'NumCuotaCalendario');

        var strNumSecRecupComi = lista[i], strNumCuotaCalendario = $("#" + subgrid_table_id).jqGrid('getCell', lista[i], 'NumCuotaCalendario');

        var arrParametros = ["pstrCodOperacionActiva", $("#hddCodSolicitudCredito").val(),
                             "pstrCodigoLiquidacion", $("#txtCodigoLiquidacion").val(),
                             "pstrTipoRecuperacion", "P",
                             "pstrNumCuotaCalendario", strNumCuotaCalendario,
                             "pstrNumSecRecupComi", strNumSecRecupComi,
                             "pstrAplicacion", strAplicacion];

        fn_util_AjaxSyncWM("frmPagoCuotasRegistro.aspx/SeleccionarAplicacionComision",
						 arrParametros,
						 function() { },
						function(resultado) {
						    parent.fn_unBlockUI();
						    var error = eval("(" + resultado.responseText + ")");
						    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error");
						}
			);
    }

    if (numCuotaCalendario != -1) {
        $("#" + strNombreGrilla).jqGrid('setCell', numCuotaCalendario, 'Comisiones', totalComisiones.toString());
        var totalCuota = $("#" + strNombreGrilla).jqGrid('getCell', numCuotaCalendario, 'Total');
        var totalPago = totalComisiones + totalCuota;
        $("#" + strNombreGrilla).jqGrid('setCell', numCuotaCalendario, 'TotalPago', totalPago.toString());
    }

    fn_calcularTotales();
}

function fn_seleccionarAplicacionOtroConcepto(grid_table_id) {
    var lista = jQuery("#" + grid_table_id).getDataIDs();
    for (var i = 0; i < lista.length; i++) {

        var strAplicacion = "1";
        if ($("#" + grid_table_id).jqGrid('getCell', lista[i], 'Aplicacion') == "False") {
            strAplicacion = "0";
        }

        var MontoReembolso = $("#" + grid_table_id).jqGrid('getCell', lista[i], 'MontoReembolso');
        var MontoIGVReembolso = $("#" + grid_table_id).jqGrid('getCell', lista[i], 'MontoIGVReembolso');
        var MontoComision = $("#" + grid_table_id).jqGrid('getCell', lista[i], 'MontoComision');
        var MontoIGV = $("#" + grid_table_id).jqGrid('getCell', lista[i], 'MontoIGV');
        var Interes = $("#" + grid_table_id).jqGrid('getCell', lista[i], 'Interes');
        var InteresIGV = $("#" + grid_table_id).jqGrid('getCell', lista[i], 'InteresIGV');
        var Total = 0;

        if (strAplicacion == "1") {
            Total = MontoReembolso + MontoIGVReembolso + MontoComision + MontoIGV + Interes + InteresIGV;
        }
        else {
            Total = MontoReembolso + MontoIGVReembolso + MontoComision + MontoIGV;
        }

        $("#" + grid_table_id).jqGrid('setCell', lista[i], 'Total', Total.toString());
    }

    fn_calcularTotales();
}

function fn_seleccionarAplicacionNuevasComisiones(grid_table_id) {

    var lista = jQuery("#" + grid_table_id).getDataIDs();

    for (var i = 0; i < lista.length; i++) {

        var strAplicacion = "1";
        if ($("#" + grid_table_id).jqGrid('getCell', lista[i], 'Aplicacion') == "false") {
            strAplicacion = "0";
        }

        var ValorNeto = $("#" + grid_table_id).jqGrid('getCell', lista[i], 'ValorNeto');
        var ValorIGV = $("#" + grid_table_id).jqGrid('getCell', lista[i], 'ValorIGV');

        if (strAplicacion == "1") {
            Total = fn_util_ValidaDecimal(ValorNeto) + fn_util_ValidaDecimal(ValorIGV);
        }
        else {
            Total = 0;
        }

        $("#" + grid_table_id).jqGrid('setCell', lista[i], 'Total', Total.toString());
    }

    fn_calcularTotales();
}

function fn_CargarCuotaAtrasadaComision(subgrid_table_id, NumCuotaCalendario) {


    var arrParametros = ["pstrCodOperacionActiva", $("#hddCodSolicitudCredito").val(),
                         "pstrCodigoLiquidacion", $("#txtCodigoLiquidacion").val(),
                         "pstrNumCuotaCalendario", NumCuotaCalendario
                        ];

    fn_util_AjaxWM("frmLiquidacionesRegistro.aspx/ObtenerCuotaAtrasadaComision",
                arrParametros,
                function(jsondata) {
                    var subgrid = jQuery("#" + subgrid_table_id)[0];
                    subgrid.addJSONData(jsondata);

                    fn_doResize();
                },
                function(request) {
                    alert(jQuery.parseJSON(request.responseText).Message);
                }
            );

}


//****************************************************************
// Funcion		:: 	fn_llenaGrillaDetalleCuotas
// Descripción	::	Llena grilla
// Log			:: 	IBK - RPR - 27/12/2012
//****************************************************************
function fn_obtenerLiquidacion(flagOperacion) {

    try {

        var strCodSolicitudCredito = $("#hddCodSolicitudCredito").val();
        var strFechaValor = $("#txtFechaValor").val();
        var strTipoLiquidacion = $("#cmbTipoLiquidacion").val();

        //String Validación
        var strError = new StringBuilderEx();

        //Declara
        if (strTipoLiquidacion == "R") {
            var objcmbTipoCronograma = $('select[id=cmbTipoCronograma]');
            var objtxtNroCuotas = $('input[id=txtNroCuotas]:text');
            var objcmbPeriodicidad = $('select[id=cmbPeriodicidad]');
            var objcmbFrecuenciaPago = $('select[id=cmbFrecuenciaPago]');

            strError.append(fn_util_ValidateControl(objcmbTipoCronograma[0], 'un Tipo Cronograma válido', 1, ''));
            strError.append(fn_util_ValidateControl(objtxtNroCuotas[0], 'un Nro. Cuotas válido', 1, ''));
            strError.append(fn_util_ValidateControl(objcmbPeriodicidad[0], 'un Periodicidad Neto válido', 1, ''));
            strError.append(fn_util_ValidateControl(objcmbFrecuenciaPago[0], 'una Frecuencia de Pago Neto válido', 1, ''));

            //Valida Fecha Primer Vencimiento
            var strMensajeFechavenc = fn_validaFechaPrimerVencimiento();
            if (fn_util_trim(strMensajeFechavenc) != "") {
                strError.append(strMensajeFechavenc);
            }
        }

        //Valida si hay Error
        if (strError.toString() != '') {
            parent.fn_unBlockUI();
            parent.fn_mdl_alert(strError.toString(), function() { });
            strError = null;
        }
        else if (strCodSolicitudCredito != "" && strFechaValor != "") {

            var arrParametros = ["pstrCodigoLiquidacion", $("#txtCodigoLiquidacion").val(),
                                 "pstrCodSolicitudCredito", strCodSolicitudCredito,
                                 "pstrTipoLiquidacion", strTipoLiquidacion,
                                 "pstrCodMoneda", $("#hddCodMonedaContrato").val(),
                                 "pstrFechaValor", strFechaValor,
                                 "pstrFechaProceso", $("#txtFechaProceso").val(),
                                 "pstrCodTipoCambio", $("#cmbTipoCambio").val(),
                                 "pstrTipoCambio", fn_util_ValidaDecimal($("#txtTipoCambio").val()),
            //DatosGenerales :: Cronograma
                                 "pstrTea", $("#txtPorcenTasaActiva").val(),
                                 "pstrAmortizacionCapital", fn_util_ValidaDecimal($("#txtAmortizacion").val()),
                                 "pstrAmortizacionSeguro", "0.00",
                                 "pstrTipoCronograma", $("#cmbTipoCronograma").val(),
                                 "pstrNroCuotas", $("#txtNroCuotas").val(),
                                 "pstrPeriodicidad", $("#cmbPeriodicidad").val(),
                                 "pstrFrecuenciaPago", $("#cmbFrecuenciaPago").val(),
                                 "pstrPlazoGracia", $("#txtPlazoGracia").val(),
                                 "pstrTipoGracia", $("#cmbTipoGracia").val(),
                                 "pstrFechavence", Fn_util_DateToString($("#txtFechaPrimerVencimiento").val()),
            //Operacion
                                 "pstrFlagOperacion", flagOperacion];

            fn_util_AjaxWM("frmLiquidacionesRegistro.aspx/ObtenerLiquidacion",
                arrParametros,
                function(jsondata) {

                    jqGrid_lista_A.addJSONData(jsondata[0]);
                    var cntA = $("#jqGrid_lista_A").getGridParam("reccount");

                    if (cntA == 0) {
                        $("#rwVencidas").hide();
                    }
                    else {
                        $("#rwVencidas").show();
                    }

                    jqGrid_lista_B.addJSONData(jsondata[1]);

                    jqGrid_lista_C.addJSONData(jsondata[2]);
                    var cntC = $("#jqGrid_lista_C").getGridParam("reccount");
                    if (cntC == 0) {
                        $("#rwConceptos").hide();
                    } else {
                        $("#rwConceptos").show();
                    }

                    jqGrid_lista_D.addJSONData(jsondata[3]);
                    jqGrid_lista_E.addJSONData(jsondata[4]);
                    jqGrid_lista_F.addJSONData(jsondata[5]);

                    fn_calcularTotales();
                    $("#hddFlagGenerar").val('1');
                    fn_doResize();
                },
                function(request) {
                    alert(jQuery.parseJSON(request.responseText).Message);
                }
            );

        }

    } catch (ex) {
        fn_util_alert(ex.message);
    }

}

//****************************************************************
// Funcion		:: 	fn_calcularTotales
// Descripción	::	Calcula totales de cuotas y comisiones
// Log			:: 	IBK - RPR - 04/01/2013
//****************************************************************

function getColumnIndexByName(grid, columnName) {
    var cm = grid.jqGrid('getGridParam', 'colModel'), i = 0, l = cm.length;
    for (; i < l; i++) {
        if (cm[i].name === columnName) {
            return i; // return the index
        }
    }
    return -1;
}

function fn_calcularTotalesGrid(strGrid, columnName) {

    var $grid = jQuery(strGrid), rows = $grid[0].rows, cRows = rows.length, iRow, rowId, row, cellsOfRow;

    var TotalConceptos = 0;

    for (iRow = 0; iRow < cRows; iRow++) {
        row = rows[iRow];
        if ($(row).hasClass("jqgrow")) {
            cellsOfRow = row.cells;

            var iColAplicacion = getColumnIndexByName($(strGrid), 'Aplicacion');
            var iColMonto = getColumnIndexByName($(strGrid), columnName);
            var arrChecked = $(cellsOfRow[iColAplicacion]).children("input:checked");

            if (arrChecked.length > 0) {
                var tmp = $(cellsOfRow[iColMonto]).text();
                TotalConceptos += fn_util_ValidaDecimal(tmp);
            }
        }
    }

    return TotalConceptos;
}


function fn_calcularTotales() {

    try {

        //TODO: Separar el IGV por las comisiones de cuotas atrasadas. Validar contra comprobantes LPC.
        var CuotasAtrasadasIGV = $("#jqGrid_lista_A").jqGrid('getCol', 'MontoIGV', false, 'sum');
        var CuotasAtrasadasTotal = $("#jqGrid_lista_A").jqGrid('getCol', 'Total', false, 'sum');
        var CuotasAtrasadasComisiones = $("#jqGrid_lista_A").jqGrid('getCol', 'Comisiones', false, 'sum');
        var CuotasAtrasadasValorNeto = CuotasAtrasadasTotal - CuotasAtrasadasIGV + CuotasAtrasadasComisiones;
        var CuotasAtrasadasTotalPago = CuotasAtrasadasTotal + CuotasAtrasadasComisiones;

        $("#jqGrid_lista_A").jqGrid('footerData', 'set', { NumCuotaCalendario: 'Totales', MontoIGV: CuotasAtrasadasIGV.toString(), Total: CuotasAtrasadasTotal.toString(), Comisiones: CuotasAtrasadasComisiones.toString(), TotalPago: CuotasAtrasadasTotalPago.toString() });

        var CuotasVigentesValorNeto = fn_calcularTotalesGrid("#jqGrid_lista_B", "ValorNeto");
        var CuotasVigentesIGV = fn_calcularTotalesGrid("#jqGrid_lista_B", "ValorIGV");
        var CuotasVigentesTotal = fn_calcularTotalesGrid("#jqGrid_lista_B", "Total");
        $("#jqGrid_lista_B").jqGrid('footerData', 'set', { Concepto: 'Totales', ValorNeto: CuotasVigentesValorNeto.toString(), ValorIGV: CuotasVigentesIGV.toString(), Total: CuotasVigentesTotal.toString() });

        var MontoReembolso = $("#jqGrid_lista_C").jqGrid('getCol', 'MontoReembolso', false, 'sum');
        var MontoIGVReembolso = $("#jqGrid_lista_C").jqGrid('getCol', 'MontoIGVReembolso', false, 'sum');
        var MontoComision = $("#jqGrid_lista_C").jqGrid('getCol', 'MontoComision', false, 'sum');
        var MontoIGV = $("#jqGrid_lista_C").jqGrid('getCol', 'MontoIGV', false, 'sum');
        var Reajuste = fn_calcularTotalesGrid("#jqGrid_lista_C", "Interes");
        var ReajusteIGV = fn_calcularTotalesGrid("#jqGrid_lista_C", "InteresIGV");
        var TotalConceptos = $("#jqGrid_lista_C").jqGrid('getCol', 'Total', false, 'sum');
        $("#jqGrid_lista_C").jqGrid('footerData', 'set', { Concepto: 'Totales', MontoReembolso: MontoReembolso.toString(), MontoIGVReembolso: MontoIGVReembolso.toString(), MontoComision: MontoComision.toString(), MontoIGV: MontoIGV.toString(), Interes: Reajuste.toString(), InteresIGV: ReajusteIGV.toString(), Total: TotalConceptos.toString() });

        var NuevasComisionesValorNeto = fn_calcularTotalesGrid("#jqGrid_lista_D", "ValorNeto");
        var NuevasComisionesIGV = fn_calcularTotalesGrid("#jqGrid_lista_D", "ValorIGV");
        var NuevasComisionesTotal = fn_calcularTotalesGrid("#jqGrid_lista_D", "Total");
        $("#jqGrid_lista_D").jqGrid('footerData', 'set', { Concepto: 'Totales', ValorNeto: NuevasComisionesValorNeto.toString(), ValorIGV: NuevasComisionesIGV.toString(), MontoComision: MontoComision.toString(), MontoIGV: MontoIGV.toString(), Total: NuevasComisionesTotal.toString() });

        var TotalValorNeto, TotalIGV, Total;
        if ($('#cmbTipoLiquidacion').val() == 'P' || $('#cmbTipoLiquidacion').val() == 'C') {
            TotalValorNeto = CuotasAtrasadasValorNeto + CuotasVigentesValorNeto + MontoReembolso + MontoComision + Reajuste + NuevasComisionesValorNeto;
            TotalIGV = CuotasAtrasadasIGV + CuotasVigentesIGV + MontoIGVReembolso + MontoIGV + ReajusteIGV + NuevasComisionesIGV;
            Total = TotalValorNeto + TotalIGV;
        }
        else {
            var Amortizacion = fn_util_ValidaDecimal($('#txtAmortizacion').val());
            var PorcIGV = fn_util_ValidaDecimal($('#hddPorcIGV').val());
            var AmortizacionIGV = fn_util_ValidaDecimal((Amortizacion * PorcIGV).toFixed(2));
            TotalValorNeto = CuotasAtrasadasValorNeto + NuevasComisionesValorNeto;
            TotalIGV = CuotasAtrasadasIGV + NuevasComisionesIGV + AmortizacionIGV;
            Total = TotalValorNeto + Amortizacion + TotalIGV;
        }

        $('#txtValorNeto').val(Fn_util_ReturnValidDecimal2(TotalValorNeto.toString()));
        $('#txtIGV').val(Fn_util_ReturnValidDecimal2(TotalIGV.toString()));
        $('#txtTotal').val(Fn_util_ReturnValidDecimal2(Total.toString()));

        if (actualizarGridCompete_D == '1') {
            setearGridComplete("jqGrid_lista_D");
            actualizarGridCompete_D = '0';
        }

    } catch (ex) {
        alert(ex.message);
    }

}

function fn_iniGrillaDocumento(strNombreGrilla) {

    $("#" + strNombreGrilla).jqGrid({
        datatype: function() { },
        jsonReader:
        {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Codigo', 'Nombre Archivo', 'Adjunto', 'Comentario'],
        colModel: [
                { name: 'CodigoDocumento', index: 'CodigoDocumento', hidden: true },
		        { name: 'NombreArchivo', index: 'NombreArchivo', width: 200, align: "left", sorttype: "string", defaultValue: "" },
		        { name: 'RutaArchivo', index: 'RutaArchivo', width: 100, align: "Center", sortable: false, formatter: fn_icoDownload },
		        { name: 'Comentario', index: 'Comentario', width: 550, align: "left", sorttype: "string", defaultValue: "" }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_G',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'CodigoDocumento',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#" + strNombreGrilla).jqGrid('getRowData', id);
            $("#hddCodigoDocumento").val(rowData.CodigoDocumento);
        },
        ondblClickRow: function(id) {
        }
    });
    jQuery("#" + strNombreGrilla).jqGrid('navGrid', '#jqGrid_pager_G', { edit: false, add: false, del: false });
    $("#search_" + strNombreGrilla).hide();

    //Abrir Archivo
    function fn_icoDownload(cellvalue, options, rowObject) {
        var strNombreArchivo = rowObject.RutaArchivo.split('\\').pop();
        strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
        if (fn_util_trim(rowObject.RutaArchivo) != "") {
            return "<img src='../Util/images/ico_download.gif' alt='" + strNombreArchivo + "' title='" + strNombreArchivo + "' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.RutaArchivo) + "');\" style='cursor:pointer;'/>";
        } else {
            return ".";
        }
    };

}

//****************************************************************
// Funcion		:: 	fn_cancelar  
// Descripción	::	Regresar al listado de pagos
// Log			:: 	IBK - RPR - 04/01/2013
//****************************************************************
function fn_cancelar() {

    parent.fn_mdl_confirma(
                    "¿Está seguro de volver?",
                    function() {
                        fn_util_globalRedirect("/Pagos/frmLiquidacionesListado.aspx");
                    },
                    "Util/images/question.gif",
                    function() { },
                    'Confirmación');
}


//****************************************************************
// Funcion		:: 	fn_PoneDatosClienteRM
// Descripción	::	Pone Datos del Cliente RM
// Log			:: 	JRC - 15/05/2012
//****************************************************************

function fn_consultaRM() {
    parent.fn_blockUI();
    if ($('#txtCUCliente').val() == "") {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("Para realizar la búsqueda del Cliente debe ingresar el Código Unico", "util/images/error.gif", "ADVERTENCIA");
    } else {

        //Lmpia RM
        $('#txtNombreClienteCargo').val("");

        //Valores            
        var strCodigo = $("#txtCUClienteCargo").val();
        var paramArray = ["pstrCodUnico", strCodigo,
                          "pstrTipoBusqueda", "1"];

        fn_util_AjaxWM("../Cotizacion/frmCotizacionRegistro.aspx/ConsultaClienteRM",
                   paramArray,
                   fn_PoneDatosClienteRM,
                   function(resultado) {
                       parent.fn_unBlockUI();
                       parent.fn_mdl_mensajeIco("Se produjo un error al cargar los datos de RM", "util/images/error.gif", "ERROR EN CONSULTA RM");
                   }
            );

        //Resize Pantalla
        fn_doResize();
    }
}

function fn_PoneDatosClienteRM(response) {

    var objEClienteRM = response;

    if (objEClienteRM.CodError == 1) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objEClienteRM.MsgError, "util/images/error.gif", "ERROR EN CONSULTA RM");
    } else {

        $('#txtCUClienteCargo').val(objEClienteRM.Codigounico);
        $('#txtNombreClienteCargo').val(objEClienteRM.Razonsocialcliente);

        if ($('#hddNumSecRecuperacion').val() == "") {
            fn_obtenerCuentas();
        }

        parent.fn_unBlockUI();
    }
}

function fn_obtenerCuentas() {

    $("#cmbNroCuenta").html(strComboVacio);

    var codMonedaCargo = $("#cmbCodMonedaCargo").val();
    var tipoCuenta = $("#cmbTipoCuenta").val();
    var CUCliente = $('#txtCuCliente').val();

    if ($('#hddFlagCuentaPropia').val() == 'N') {
        CUCliente = $('#txtCUClienteCargo').val();
    }

    if (codMonedaCargo != '0' && tipoCuenta != '0' && CUCliente != '') {

        parent.fn_blockUI();

        try {

            var arrParametros = ["pstrCUCliente", CUCliente,
                                 "pstrCodMonedaCargo", codMonedaCargo,
                                 "pstrTipoCuenta", tipoCuenta];

            fn_util_AjaxWM("frmPagoCuotasRegistro.aspx/ObtenerCuentas",
                arrParametros,
                function(request) {

                    var cmbNroCuenta = document.getElementById("cmbNroCuenta");

                    for (var i = 1; i <= 2 * request[0] - 1; i += 2) {
                        var opt1 = document.createElement("option");
                        opt1.appendChild(document.createTextNode(request[i].toString() + " (" + request[i + 1].toString() + ")"));
                        opt1.value = request[i].toString().replace("-", "");
                        cmbNroCuenta.appendChild(opt1);
                    }

                    fn_doResize();

                    parent.fn_unBlockUI();
                },
                function(request) {
                    parent.fn_unBlockUI();
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

        } catch (ex) {
            parent.fn_unBlockUI();
            fn_util_alert(ex.message);
        }

    }
}

//****************************************************************
// Funcion		:: 	fn_ejecutarPagoCuotas
// Descripción	::	Ejecutar Pago de Cuotas
// Log			:: 	IBK - RPR - 07/01/2013
//****************************************************************
function fn_ejecutarPagoCuotas() {

    if ($("#hddCodSolicitudCredito").val() == "") {
        parent.fn_mdl_mensajeIco("Por favor ingrese un número de contrato.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    if ($("#txtFechaValor").val() == "") {
        parent.fn_mdl_mensajeIco("Por favor ingrese la fecha valor de pago.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    if ($("#cmbNroCuotas").val() == "0") {
        parent.fn_mdl_mensajeIco("Por favor ingrese el nro de cuotas a cancelar.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    if ($("#cmbMovimientoBasilea").val() == "0") {
        parent.fn_mdl_mensajeIco("Por favor ingrese el Motivo Basilea del pago.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    if ($("#hddTipoViaCobranza").val() == "") {
        parent.fn_mdl_mensajeIco("Por favor seleccione una vía de cobranza.", "util/images/error.gif", "Validación de Datos");
        return;
    }
    else if ($("#hddTipoViaCobranza").val() == "C") {

        if ($("#cmbCodMonedaCargo").val() == "0" || $("#cmbTipoCuenta").val() == "0" || $("#cmbNroCuenta").val() == "0") {
            parent.fn_mdl_mensajeIco("Por favor seleccione un tipo y número de cuenta de cargo.", "util/images/error.gif", "Validación de Datos");
            return;
        }
        else if ($("#hddFlagCuentaPropia").val() == "N" && ($("#txtCUClienteCargo").val() == "" || $("#txtNombreClienteCargo").val() == "")) {
            parent.fn_mdl_mensajeIco("Por favor ingrese el CU del Cliente titular de la cuenta de cargo.", "util/images/error.gif", "Validación de Datos");
            return;
        }
    }

    if ($("#txtMontoPago").val() <= 0) {
        parent.fn_mdl_mensajeIco("El monto pago no puede ser menor o igual a cero .", "util/images/error.gif", "Validación de Datos");
        return;
    }

    var NroCuotasxPagar = $('#hddNroCuotasxPagar').val();
    var NroCuotasVencidas = $('#hddNroCuotasVencidas').val();
    var NroPagosCuotasxProcesar = $('#hddNroPagosCuotasxProcesar').val();
    var NroConceptoPendiente = $('#hddNroConceptoPendiente').val();

    if (NroCuotasxPagar <= 0) {
        parent.fn_mdl_mensajeIco("El crédito no tiene cuotas pendientes de pago.", "util/images/error.gif", "Validación de Datos");
        return;
    }
    if (NroPagosCuotasxProcesar > 0) {
        parent.fn_mdl_mensajeIco("Existen otros pagos de cuotas registrados para este crédito. No es posible instruir un nuevo pago hasta que los pagos existentes sean ejecutados o anulados.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    var strMensajeConfirmacion = "¿Está seguro que desea ejecutar el Pago de Cuotas?";

    if (NroConceptoPendiente > 0) {
        strMensajeConfirmacion = "Existen otros conceptos pendientes de pago para este crédito. ¿Está seguro que desea ejecutar el Pago de Cuotas?";
    }

    parent.fn_mdl_confirma(
        strMensajeConfirmacion,
		function() {

		    var arrParametros = ["pstrCodOperacionActiva", $("#hddCodSolicitudCredito").val(),
								 "pstrFechaValorRecuperacion", $("#txtFechaValor").val(),
								 "pstrTipoViaCobranza", $("#hddTipoViaCobranza").val(),
								 "pstrFlagCuentaPropia", $("#hddFlagCuentaPropia").val(),
								 "pstrCodUnicoClienteCargo", $("#txtCUClienteCargo").val(),
								 "pstrCodMonedaCargo", $("#cmbCodMonedaCargo").val(),
								 "pstrTipoCuenta", $("#cmbTipoCuenta").val(),
								 "pstrNroCuenta", $("#cmbNroCuenta").val(),
								 "pstrCodigoMovimientoBasilea", $("#cmbMovimientoBasilea").val()];

		    parent.fn_blockUI();

		    fn_util_AjaxSyncWM("frmPagoCuotasRegistro.aspx/EjecutaPagoCuotas",
						 arrParametros,
						 fn_retornoEjecucionPagoCuotas,
						function(resultado) {
						    parent.fn_unBlockUI();
						    var error = eval("(" + resultado.responseText + ")");
						    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al Ejecutar Pago");
						}
			);

		},
        "Util/images/question.gif",
        function() { },
        'Ejecutar Pago de Cuotas');
}

//****************************************************************
// Funcion		:: 	fn_anularPagoCuotas
// Descripción	::	Anular Pago de Cuotas
// Log			:: 	IBK - RPR - 24/01/2013
//****************************************************************

function fn_anularLiquidacion() {

    if ($("#cmbEstadoLiquidacion").val() == 'A') {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("La Liquidacion ya se encuentra Anulada.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    if ($("#cmbEstadoLiquidacion").val() == 'H') {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("No es posible anular esta Liquidacion porque ya ha sido Ejecutada. Utilice la opción de Extornos.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    if ($("#cmbEstadoLiquidacion").val() != 'I' && $("#cmbEstadoLiquidacion").val() != 'C') {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("La Liquidacion debe estar en estado Ingresado o Enviado a Host para poder ser anulado.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    strOperacionModal = "Anular";
    parent.fn_util_AbreModal("Liquidación :: Anulación", "Pagos/frmConfirmarOperacion.aspx", 600, 300, function() { });
}

function fn_anular(strMotivoAnulacion) {

    var arrParametros = ["pstrCodigoLiquidacion", $("#txtCodigoLiquidacion").val(),
                         "pstrMotivoAnulacionExtorno", strMotivoAnulacion];

    parent.fn_blockUI();

    fn_util_AjaxSyncWM("frmLiquidacionesRegistro.aspx/AnularLiquidacion",
				 arrParametros,
				 fn_retornoAnulacionLiquidacion,
				function(resultado) {
				    parent.fn_unBlockUI();
				    var error = eval("(" + resultado.responseText + ")");
				    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al Anular Liquidacion");
				}
	);

}

function fn_ejecutarLiquidacion() {

    if ($("#cmbEstadoLiquidacion").val() == 'A') {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("La Liquidacion se encuentra Anulada, no es posible Ejecutar.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    if ($("#cmbEstadoLiquidacion").val() == 'H') {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("La Liquidacion ya ha sido Ejecutada.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    if ($("#cmbEstadoLiquidacion").val() != 'C') {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("La Liquidacion debe estar en estado Pendiente Ejecucion para poder ser Ejecutada.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    parent.fn_mdl_confirma(
        "¿Está seguro que desea ejecutar la Liquidacion?.",
		function() {
		    var strTipoRecuperacion = "R";
		    var arrParametros = ["pstrCodigoLiquidacion", $("#txtCodigoLiquidacion").val(),
								 ];

		    parent.fn_blockUI();

		    fn_util_AjaxSyncWM("frmLiquidacionesRegistro.aspx/EjecutarLiquidacion",
						 arrParametros,
						 fn_retornoEjecucionLiquidacion,
						function(resultado) {
						    parent.fn_unBlockUI();
						    var error = eval("(" + resultado.responseText + ")");
						    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al Ejecutar Liquidacion");
						}
			);

		},
        "Util/images/question.gif",
        function() { },
        'Ejecucion de Liquidacion');
}

function fn_extornarLiquidacion() {

    if ($("#cmbEstadoLiquidacion").val() == 'E') {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("La liquidacion ya se encuentra Extornada.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    if ($("#cmbEstadoLiquidacion").val() == 'A') {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("La liquidacion se encuentra Anulada, no es posible Extornar.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    if ($("#cmbEstadoLiquidacion").val() != 'H') {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("La liquidacion debe haber sido Ejecutada para poder ser Extornada.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    strOperacionModal = "Extornar";
    parent.fn_util_AbreModal("Liquidación :: Extorno", "Pagos/frmConfirmarOperacion.aspx", 600, 300, function() { });
}

function fn_extornar(strMotivoExtorno) {

    var arrParametros = ["pstrCodigoLiquidacion", $("#txtCodigoLiquidacion").val(),
                         "pstrMotivoAnulacionExtorno", strMotivoExtorno];

    parent.fn_blockUI();

    fn_util_AjaxSyncWM("frmLiquidacionesRegistro.aspx/ExtornarLiquidacion",
				 arrParametros,
				 fn_retornoExtornoLiquidacion,
				function(resultado) {
				    parent.fn_unBlockUI();
				    var error = eval("(" + resultado.responseText + ")");
				    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al extornar liquidacion");
				}
	);
}

function fn_devolverLiquidacion() {

    if ($("#cmbEstadoLiquidacion").val() == 'E') {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("La liquidacion se encuentra Extornada, no es posible devolver.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    if ($("#cmbEstadoLiquidacion").val() == 'A') {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("La liquidacion se encuentra Anulada, no es posible devolver.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    if ($("#cmbEstadoLiquidacion").val() != 'C') {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("La liquidacion debe haber sido enviada para poder ser devuelta.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    strOperacionModal = "Devolver";
    parent.fn_util_AbreModal("Liquidación :: Devolver Liquidación", "Pagos/frmConfirmarOperacion.aspx", 600, 300, function() { });
}

function fn_devolver(strMotivoExtorno) {

    var arrParametros = ["pstrCodigoLiquidacion", $("#txtCodigoLiquidacion").val(),
                         "pstrMotivoAnulacionExtorno", strMotivoExtorno];

    parent.fn_blockUI();

    fn_util_AjaxSyncWM("frmLiquidacionesRegistro.aspx/DevolverLiquidacion",
				 arrParametros,
				 fn_retornoDevolucionLiquidacion,
				function(resultado) {
				    parent.fn_unBlockUI();
				    var error = eval("(" + resultado.responseText + ")");
				    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al devolver liquidacion");
				}
	);
}

//****************************************************************
// Funcion		:: 	fn_retornoAnulacionPagoCuotas
// Descripción	::	Recibe respuesta de la anulacion de Pago de Cuotas
// Log			:: 	IBK - RPR - 25/01/2013
//****************************************************************
function fn_retornoAnulacionLiquidacion(response) {

    var objECreditoRecuperacion = response;

    if (objECreditoRecuperacion.CodError != 0) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objECreditoRecuperacion.MsgError, "util/images/error.gif", "Validación de Datos");
    } else {

        parent.fn_unBlockUI();

        //Log
        parent.fn_util_MuestraLogPage("Anulación correcta. Se muestran los datos actualizados.", "I");

        fn_util_globalRedirect("/Pagos/frmLiquidacionesRegistro.aspx?hddCodSolicitudCredito=" + $("#hddCodSolicitudCredito").val() + "&hddCodigoLiquidacion=" + objECreditoRecuperacion.CodigoLiquidacion);
    }

    fn_doResize();
}
function fn_retornoExtornoLiquidacion(response) {

    var objECreditoRecuperacion = response;

    if (objECreditoRecuperacion.CodError != 0) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objECreditoRecuperacion.MsgError, "util/images/error.gif", "Validación de Datos");
    } else {

        parent.fn_unBlockUI();

        //Log
        parent.fn_util_MuestraLogPage("Extorno correcto. Se muestran los datos actualizados.", "I");

        fn_util_globalRedirect("/Pagos/frmLiquidacionesRegistro.aspx?hddCodSolicitudCredito=" + $("#hddCodSolicitudCredito").val() + "&hddCodigoLiquidacion=" + objECreditoRecuperacion.CodigoLiquidacion);
    }

    fn_doResize();
}

function fn_retornoEjecucionLiquidacion(response) {

    var objECreditoRecuperacion = response;

    if (objECreditoRecuperacion.CodError != 0) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objECreditoRecuperacion.MsgError, "util/images/error.gif", "Validación de Datos");
    } else {

        parent.fn_unBlockUI();

        //Log
        parent.fn_util_MuestraLogPage("Ejecucion correcta. Se muestran los datos actualizados.", "I");

        fn_util_globalRedirect("/Pagos/frmLiquidacionesRegistro.aspx?hddCodSolicitudCredito=" + $("#hddCodSolicitudCredito").val() + "&hddCodigoLiquidacion=" + objECreditoRecuperacion.CodigoLiquidacion);
    }
    fn_cargaGrillaDocumento();
    fn_doResize();
}

function fn_retornoEnvioLiquidacion(response) {

    var objECreditoRecuperacion = response;

    if (objECreditoRecuperacion.CodError != 0) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objECreditoRecuperacion.MsgError, "util/images/error.gif", "Validación de Datos");
    } else {

        parent.fn_unBlockUI();

        //Log
        parent.fn_util_MuestraLogPage("Envio correcto. Se muestran los datos actualizados.", "I");

        fn_util_globalRedirect("/Pagos/frmLiquidacionesRegistro.aspx?hddCodSolicitudCredito=" + $("#hddCodSolicitudCredito").val() + "&hddCodigoLiquidacion=" + objECreditoRecuperacion.CodigoLiquidacion);
    }

    fn_doResize();
}
function fn_retornoDevolucionLiquidacion(response) {

    var objECreditoRecuperacion = response;

    if (objECreditoRecuperacion.CodError != 0) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objECreditoRecuperacion.MsgError, "util/images/error.gif", "Validación de Datos");
    } else {

        parent.fn_unBlockUI();

        //Log
        parent.fn_util_MuestraLogPage("Devolucion correcta. Se muestran los datos actualizados.", "I");

        fn_util_globalRedirect("/Pagos/frmLiquidacionesRegistro.aspx?hddCodSolicitudCredito=" + $("#hddCodSolicitudCredito").val() + "&hddCodigoLiquidacion=" + objECreditoRecuperacion.CodigoLiquidacion);
    }

    fn_doResize();
}

//****************************************************************
// Funcion		:: 	fn_abreArchivo 
// Descripción	::	Abre Archivo
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_abreArchivo(pstrRuta) {
    window.open("../frmDownload.aspx?nombreArchivo=" + pstrRuta);
    return false;
}

function fn_isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}
/* FIN IBK */

//****************************************************************
// Funcion		:: 	fn_obtenerOperacionExtornable
// Descripción	::	Obtener operacion a extornar
// Log			:: 	IBK - RPR
//****************************************************************

function fn_obtenerOperacionExtornable(strCodOperacionActiva) {

    var paramArray = ["pstrCodOperacionActiva", strCodOperacionActiva];

    fn_util_AjaxWM("frmPagoCuotasRegistro.aspx/ObtenerOperacionExtornable",
                       paramArray,
                       fn_retornoObtenerOperacionExtornable,
                       function(resultado) {
                           parent.fn_unBlockUI();
                           alert(resultado.responseText);
                           parent.fn_mdl_mensajeIco("Se produjo un error al obtener los datos del contrato", "util/images/error.gif", "ERROR EN CONSULTA");
                       }
                );
}

function fn_retornoObtenerOperacionExtornable(response) {

    var objECreditoRecuperacion = response;

    if (objECreditoRecuperacion.CodError == 1) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objECreditoRecuperacion.MsgError, "util/images/error.gif", "Validación de Datos");
    } else {
        parent.fn_unBlockUI();

        if (objECreditoRecuperacion.NumSecRecuperacion >= 1) {
            window.location = "frmPagoCuotasRegistro.aspx?hddCodSolicitudCredito=" + objECreditoRecuperacion.CodOperacionActiva + "&hddNumSecRecuperacion=" + objECreditoRecuperacion.NumSecRecuperacion + "&op=E";
        }

        //Log
        parent.fn_util_MuestraLogPage("El sistema ubicó el N° Contrato: " + objECreditoRecuperacion.CodOperacionActiva + ". Se muestran los datos.", "I");
    }

    fn_doResize();
}
//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrillaCronograma(strNombreGrilla) {

    $("#" + strNombreGrilla).jqGrid({
        datatype: function() { },
        jsonReader:
        {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "NumCuotaCalendario"
        },
        colNames: ['#', 'Fec.Venc.', 'Días', 'Saldo Capital', 'Principal', 'Interés', 'Saldo Seguro', 'Principal Seguro', 'Intereses Seguro', 'IGV', 'Total a Pagar', 'Estado', 'Fecha de Pago', ''],
        colModel: [
		        { name: 'NumCuotaCalendario', index: 'NumCuotaCalendario', width: 40, align: "center" },
		        { name: 'FechaVencimientoCuota', index: 'FechaVencimientoCuota', align: "center", formatter: Fn_util_ValidaFechaVacia },
		        { name: 'CantDiasCuota', index: 'CantDiasCuota', align: "center" },
		        { name: 'MontoSaldoAdeudado', index: 'MontoSaldoAdeudado', align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'MontoPrincipal', index: 'MontoPrincipal', align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MontoInteres', index: 'MontoInteres', align: "right", formatter: Fn_util_ReturnValidDecimal2 },

		        { name: 'MontoSaldoSeguro', index: 'MontoSaldoSeguro', align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MontoPrincipalSeguro', index: 'MontoPrincipalSeguro', align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MontoInteresSeguro', index: 'MontoInteresSeguro', align: "right", formatter: Fn_util_ReturnValidDecimal2 },

		        { name: 'MontoTotalIGV', index: 'MontoTotalIGV', align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MontoTotalPagar', index: 'MontoTotalPagar', width: 120, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
        	    { name: 'Estado', index: 'Estado', align: "right" },
                { name: 'FechaCancelacionCuota', index: 'FechaCancelacionCuota', align: "right" },
		        { name: 'AA', index: 'AA', width: 9, align: "right" }
        ],
        width: glb_intWidthPantalla - 120,
        height: '100%',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 50,
        sortname: 'NumCuotaCalendario',
        sortorder: 'asc',
        viewrecords: true,
        gridview: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) { }
    });
}

function fn_Exportar() {

    if ($("#hddFlagGenerar").val() == '0') {
        parent.fn_mdl_mensajeIco("Primero debe generar la liquidación.", "util/images/warning.gif", "Validación de Datos");
    }
    else {
        $("#btnGenerar").click();
    }
}


//****************************************************************
// Funcion		:: 	fn_eliminarDocumentoComentario 
// Descripción	::	Editar 
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_eliminarDocumentoComentario() {
    var strCodigoDocumento = $("#hddCodigoDocumento").val();
    if ($("#hddFlagGenerar").val() != '0') {
        parent.fn_mdl_alert("Debe seleccionar un documento.", function() { });
    } else {
        //Variables
        var strNroInstruccion = $("#txtNroContrato").val();
        var strNroContrato = $("#txtNroContrato").val();


        var paramArray = ["pstrCodInstruccion", strNroInstruccion, "pstrCodContrato", strNroContrato, "pstrCodigoDocumento", strCodigoDocumento];

        if (fn_util_trim(strCodigoDocumento) == "") {
            parent.fn_mdl_alert("Debe seleccionar un documento.", function() { });
        }
        else {
            //Confirmacion de Eliminacion
            parent.fn_mdl_confirma(
                            "¿Está seguro que desea eliminar el Documento/Comentario?  ", //Mensaje - Obligatorio
                            function() {
                                parent.fn_blockUI();
                                fn_util_AjaxWM("frmLiquidacionesRegistro.aspx/EliminaDocumentoComentario",
                                                   paramArray,
                                                   function(resultado) {
                                                       fn_cargaGrillaDocumento();
                                                       parent.fn_unBlockUI();
                                                   },
                                                   function(resultado) {
                                                       var error = eval("(" + resultado.responseText + ")");
                                                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
                                                   }
                                    );

                            },
                            "Util/images/question.gif",
                            function() { },
                            'ELIMINAR DOCUMENTO/COMENTARIO'
            );

        }


    }
}

//****************************************************************
// Funcion		:: 	fn_cotizacionRechazar 
// Descripción	::	Abre Modal de Motivo de Rechazo
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_abreNuevoDocumentoComentario() {
    var strNroInstruccion = $("#txtCodigoLiquidacion").val();
    var strNroContrato = $("#txtNroContrato").val();
    parent.fn_util_AbreModal("Liquidación :: Documentos y Comentarios", "InsDesembolso/frmDocumentoComentario.aspx?hddCodigoInstruccion=" + strNroInstruccion + "&hddCodigoContrato=" + strNroContrato, 650, 320, function() { });

}

//****************************************************************
// Funcion		:: 	fn_editarComentario 
// Descripción	::	Editar 
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_editarDocumentoComentario() {
    var strNroInstruccion = $("#txtCodigoLiquidacion").val(); ;
    var strNroContrato = $("#txtNroContrato").val();
    var strCodigoDocumento = $("#hddCodigoDocumento").val();
    if (fn_util_trim(strCodigoDocumento) == "") {
        parent.fn_mdl_alert("Debe seleccionar un documento.", function() { });
    } else {
        parent.fn_util_AbreModal("Ins. Desembolso :: Documentos y Comentarios", "InsDesembolso/frmDocumentoComentario.aspx?hddCodigoInstruccion=" + strNroInstruccion + "&hddCodigoContrato=" + strNroContrato + "&hddCodigoDocumento=" + strCodigoDocumento, 650, 320, function() { });
    }
}

//****************************************************************
// Funcion		:: 	fn_abreArchivo 
// Descripción	::	Abre Archivo
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_abreArchivo(pstrRuta) {
    window.open("../frmDownload.aspx?nombreArchivo=" + pstrRuta);
    return false;
}

//****************************************************************
// Funcion		:: 	fn_cargaGrillaDocumento 
// Descripción	::	Abre Modal de Motivo de Rechazo
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrillaDocumento() {
    if ($("#hddFlagGenerar").val() != '0') {
        try {

            var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_G", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_G", "page"), // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_G", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_G", "sortorder"), // Criterio de ordenación
                             "pstrCodInstruccion", $("#txtCodigoLiquidacion").val(),
                             "pstrCodContrato", $("#txtNroContrato").val()
                            ];

            fn_util_AjaxWM("frmLiquidacionesRegistro.aspx/ListadoInsDesembolsoDocumento",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_G.addJSONData(jsondata);
                    fn_doResize();
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

        } catch (ex) {
            fn_util_alert(ex.message);
        }
    }
}
function fn_seleccionarCreditoVigente(grid_table_id) {

    var lista = jQuery("#" + grid_table_id).getDataIDs();
    var Total = 0;
    for (var i = 0; i < lista.length; i++) {

        var strAplicacion = "1";
        if ($("#" + grid_table_id).jqGrid('getCell', lista[i], 'Aplicacion') == "False") {
            strAplicacion = "0";
        }

        var ValorNeto = fn_util_ValidaDecimal($("#" + grid_table_id).jqGrid('getCell', lista[i], 'ValorNeto'));
        var ValorIGV = fn_util_ValidaDecimal($("#" + grid_table_id).jqGrid('getCell', lista[i], 'ValorIGV'));

        if (strAplicacion == "1") {
            Total = ValorNeto + ValorIGV;
        }
        else {
            Total = 0;
        }
        if (FlagSuma == '1') {
            $("#" + grid_table_id).jqGrid('setCell', lista[i], 'Total', Total.toString());
        }
        if (FlagSuma == '0') {
            $("#" + grid_table_id).jqGrid('setCell', lista[i], 'Total', Total.toString());
        }
    }

    fn_calcularTotales();

}
function fn_ActualizaTotal(grid_table_id, id, igv, neto, CodigoOperacion, Tipo, NumSecuencia, CodUsuario, Aplicacion) {

    var total = fn_util_ValidaDecimal(ValorIGV) + fn_util_ValidaDecimal(ValorNeto);
    var lista = jQuery("#" + grid_table_id).getDataIDs();
    var opc = '';
    for (var i = 0; i < lista.length; i++) {
        opc = $("#" + grid_table_id).jqGrid('getCell', lista[i], 'Concepto');

        if (id - 1 == i) {
            if ($("#" + grid_table_id).jqGrid('getCell', lista[i], 'Total') != total) {
                if (opc == 'Opcion de Compra') {
                    var arrParametros = ["pstrValorNeto", $("#" + grid_table_id).jqGrid('getCell', lista[i], 'ValorNeto'),
                                     "pstrValorIGV", $("#" + grid_table_id).jqGrid('getCell', lista[i], 'ValorIGV'),
                                     "pstrCodOperacionActiva", CodigoOperacion,
                                     "pstrNumSecRecuperacion", NumSecuencia,
                                     "pstrTipoRecuperacion", Tipo,
                                     "pstrCodUsuario", CodUsuario,
                                     "pstrAplicacion", Aplicacion == 'true' ? '1' : '0'];

                    fn_util_AjaxWM("frmLiquidacionesRegistro.aspx/ActualizarTMPLiquidacion",
                     arrParametros,
                     fn_MensajeYRedireccionar,
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
                }
                else {
                    if (ValorNeto == $("#" + grid_table_id).jqGrid('getCell', lista[i], 'ValorNeto') && ValorIGV != $("#" + grid_table_id).jqGrid('getCell', lista[i], 'ValorIGV')) {
                        var arrParametros = ["pstrValorNeto", $("#" + grid_table_id).jqGrid('getCell', lista[i], 'ValorNeto'),
                                     "pstrValorIGV", $("#" + grid_table_id).jqGrid('getCell', lista[i], 'ValorIGV'),
                                     "pstrCodOperacionActiva", CodigoOperacion,
                                     "pstrNumSecRecuperacion", NumSecuencia,
                                     "pstrTipoRecuperacion", Tipo,
                                     "pstrCodUsuario", CodUsuario,
                                     "pstrAplicacion", Aplicacion == 'true' ? '1' : '0'];

                        fn_util_AjaxWM("frmLiquidacionesRegistro.aspx/ActualizarTMPLiquidacion",
                     arrParametros,
                     fn_MensajeYRedireccionar,
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
                    }
                }

            }
            if (opc != 'Opcion de Compra' && grid_table_id == 'jqGrid_lista_B') {
                $("#" + grid_table_id).jqGrid('setCell', lista[i], 'ValorNeto', ValorNeto);
                FlagSuma = '1';
            }
            else { FlagSuma = '0'; }
        }
    }
    //fn_calcularTotales();
    fn_seleccionarCreditoVigente(grid_table_id);
}
function fn_ActualizaAplicacion(CodigoOperacion, Tipo, NumSecuencia, CodUsuario, Aplicacion) {

    var arrParametros = ["pstrCodOperacionActiva", CodigoOperacion,
                                     "pstrNumSecRecuperacion", NumSecuencia,
                                     "pstrTipoRecuperacion", Tipo,
                                     "pstrCodUsuario", CodUsuario,
                                     "pstrAplicacion", Aplicacion == 'true' ? '1' : '0'];

    fn_util_AjaxWM("frmLiquidacionesRegistro.aspx/ActualizarTMPLiquidacionAplicacion",
                     arrParametros,
                     fn_MensajeYRedireccionar,
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });


}
//****************************************************************
// Función		:: 	fn_MensajeYRedireccionarSolicitud
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
//                  Luego lo redirecciona al formulario de búsquedas ("frmMantenimientoBienListado.aspx").
// Log			:: 	AEP - 09/10/2012
//****************************************************************
var fn_MensajeYRedireccionar = function() {
    //    parent.fn_unBlockUI();
    //    fn_SetearCamposObligatorios();
    //    parent.fn_mdl_alert('Los datos se grabaron satisfactoriamente', function() { });
};
function fn_ActualizaTotal2(grid_table_id, id, igv, neto, CodigoOperacion, Tipo, NumSecuencia, CodUsuario, Aplicacion) {

    var total = fn_util_ValidaDecimal(ValorIGV) + fn_util_ValidaDecimal(ValorNeto);
    var lista = jQuery("#" + grid_table_id).getDataIDs();
    var opc = '';
    for (var i = 0; i < lista.length; i++) {
        opc = $("#" + grid_table_id).jqGrid('getCell', lista[i], 'Concepto');
        if (id - 1 == i) {
            var arrParametros = ["pstrValorNeto", $("#" + grid_table_id).jqGrid('getCell', lista[i], 'ValorNeto'),
                                     "pstrValorIGV", $("#" + grid_table_id).jqGrid('getCell', lista[i], 'ValorIGV'),
                                     "pstrCodOperacionActiva", CodigoOperacion,
                                     "pstrNumSecRecuperacion", NumSecuencia,
                                     "pstrTipoRecuperacion", Tipo,
                                     "pstrCodUsuario", CodUsuario,
                                     "pstrAplicacion", Aplicacion == 'true' ? '1' : '0'];

            fn_util_AjaxWM("frmLiquidacionesRegistro.aspx/ActualizarTMPLiquidacion",
                     arrParametros,
                     fn_MensajeYRedireccionar,
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
        }
    }
    //fn_calcularTotales();
    fn_seleccionarCreditoVigente(grid_table_id);
}

function fn_validaFechaPrimerVencimiento() {
    var strMensaje = "";
    var strFrecPago = $("#cmbFrecuenciaPago").val();

    var strFechavence = $("#txtFechaPrimerVencimiento").val();
    if (fn_util_trim(strFechavence) == "") {
        strMensaje = "- Debe ingresar la fecha de Primer Vencimiento<br />";
    } else {
        var strFechaValor = $("#txtFechaValor").val();
        if (!fn_util_ComparaFecha(strFechaValor, strFechavence)) {
            strMensaje = "- La fecha de primer vencimiento debe ser mayor a la fecha valor de liquidacion.<br />";
        }
    }

    return strMensaje;

}


//************************************************************
// Función		:: 	fn_oc_periodicidad
// Descripcion 	:: 	Método Clasificacion
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_oc_periodicidad(strValor) {

    //var arrValor = strValor.split("*");

    var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL033", "pstrCodigoGenerico", strValor];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbFrecuenciaPago').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}

//****************************************************************
// Funcion		:: 	fn_of_PlazoGracia 
// Descripción	::	Valida PlazoGracia
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_of_PlazoGracia() {
    var txtPlazoGracia = $("#txtPlazoGracia").val();
    if (fn_util_trim(txtPlazoGracia) == "") {
        txtPlazoGracia = "0";
    }

    var strNroCuotas = $("#txtNroCuotas").val();
    if (fn_util_trim(strNroCuotas) == "") strNroCuotas = "0";
    var intNroCuotas = parseInt(strNroCuotas);
    var intPlazoGracia = parseInt(txtPlazoGracia);
    if (intPlazoGracia < 0 || intNroCuotas <= intPlazoGracia) {
        $("#txtPlazoGracia").val("0");
        parent.fn_util_MuestraLogPage("El Plazo de Gracia ingresado no es correcto", "E");
    }
    if (intPlazoGracia == 0) {
        $("#txtPlazoGracia").val("0");
    }
}