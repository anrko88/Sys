//****************************************************************
// Variables Globales
//****************************************************************
var strComboVacio = "<option value='0'>[-Seleccione-]</option>";

var numCuentasAsociadas = 0;
var tipoCuentaAsociada = new Array();
var monedaCuentaAsociada = new Array();
var numeroCuentaAsociada = new Array();

var strOperacionModal;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 19/09/2012
//****************************************************************
$(document).ready(function() {

    //Setea Calendario
    fn_util_SeteaCalendario($("#txtFechaRecuperacion"));
    fn_util_SeteaCalendario($("#txtFechaProcesoPago"));
    fn_util_SeteaCalendarioFunction($("#txtFechaValor"), function() { fn_llenaGrillaProximasCuotas(); });

    fn_cargaGrillaDetalleCuotas();
    fn_cargaGrillaDetalleComisiones();

    if ($("#hddCodSolicitudCredito").val() == "") {

        $("#dv_botonAnular").hide();
        fn_inicializaCampos("Nuevo");

    } else {

        $("#dv_botonEjecutar").hide();
        fn_inicializaCampos("Consulta");
           
        // Solo se puede anular cuando el pago esta ingresado(I) o enviado a host(C)
        if ($('#cmbEstadoRecuperacion').val() != "I" && $('#cmbEstadoRecuperacion').val() != "C") {
            $("#dv_botonAnular").hide();
        }

        // Solo se puede extornar cuando el pago esta Ejecutado(H)
        if ($('#cmbEstadoRecuperacion').val() != "H") {
            $("#dv_botonExtornar").hide();
        }
        
        // Desactivar checkbox de exoneracion de comisiones
        var cm = $("#jqGrid_lista_D").jqGrid('getColProp', 'Aplicacion');
        cm.editable = false;
        cm.formatoptions = "";

        fn_obtenerTotalesPagoCuotas($("#hddCodSolicitudCredito").val(), $("#txtFechaValor").val());

        if ($("#hddFlagCuentaPropia").val() == "N") {
            fn_consultaRM();
        }
    }

    // Formateo de Montos

    $('#txtTotalPrincipal').val(Fn_util_ReturnValidDecimal2($('#txtTotalPrincipal').val()));
    $('#txtTotalInteresVigente').val(Fn_util_ReturnValidDecimal2($('#txtTotalInteresVigente').val()));
    $('#txtTotalSeguro').val(Fn_util_ReturnValidDecimal2($('#txtTotalSeguro').val()));
    $('#txtTotalInteresSeguro').val(Fn_util_ReturnValidDecimal2($('#txtTotalInteresSeguro').val()));
    $('#txtTotalIGV').val(Fn_util_ReturnValidDecimal2($('#txtTotalIGV').val()));
    $('#txtTotalMora').val(Fn_util_ReturnValidDecimal2($('#txtTotalMora').val()));
    $('#txtTotalInteresVencido').val(Fn_util_ReturnValidDecimal2($('#txtTotalInteresVencido').val()));
    $('#txtTotalConceptos').val(Fn_util_ReturnValidDecimal2($('#txtTotalConceptos').val()));

    $('#txtTotalRecuperacion').val(Fn_util_ReturnValidDecimal2($('#txtTotalRecuperacion').val()));
    $('#txtMontoPago').val($('#txtTotalRecuperacion').val());

    $('#imgBsqContrato').click(function() {

        var strCodigo = $("#txtNroContrato").val();

        if (strCodigo == "") {
            VentanaCreditos();
        } else {
            fn_obtenerCredito(strCodigo)
        }

    });

    //-------------------------------------------
    //Valida Change del Numero de Cuotas a Pagar
    //-------------------------------------------
    $('#cmbNroCuotas').change(function() {
        fn_llenaGrillaProximasCuotas();
    });

    $('#txtCUClienteCargo').change(function() {
        $('#txtNombreClienteCargo').val("");
    });
    $('#cmbCodMonedaCargo').change(function() {
        fn_obtenerCuentas(function() { });
    });
    $('#cmbTipoCuenta').change(function() {
        fn_obtenerCuentas(function() { });
    });

    if ($('#cmbEstadoRecuperacion').val() != "E" && $('#cmbEstadoRecuperacion').val() != "A" && $('#txtMotivo').val() == "") {
        $('#rw_MotivoAnulacionExtorno').hide();
    }

    if ($('#hddPerfilUsuario').val() != "1" && $('#hddPerfilUsuario').val() != "6" && $('#hddPerfilUsuario').val() != "10" && $('#hddPerfilUsuario').val() != "11") {
        $('#dv_botonAnular').hide();
        $('#dv_botonExtornar').hide();
    }

    //Valida Tabs
    $("div#divTabs").tabs({
        show: function(event, ui) {
            fn_doResize();
        }
    });

    //On load Page (siempre al final)
    fn_onLoadPage();

});

//****************************************************************
// Funcion		:: 	fn_seteaCamposObligatorios
// Descripción	::	Setea campos obligatorios!
// Log			:: 	IBK - RPR - 03/01/2013
//****************************************************************
function fn_seteaCamposObligatorios() {

    fn_util_SeteaObligatorio($("#txtNroContrato"), "input");
    fn_util_SeteaObligatorio($("#txtFechaValor"), "input");
    fn_util_SeteaObligatorio($("#cmbNroCuotas"), "input");
    fn_util_SeteaObligatorio($("#cmbMovimientoBasilea"), "input");

    fn_util_SeteaObligatorio($('#txtCUClienteCargo'), "input");
    fn_util_SeteaObligatorio($("#cmbCodMonedaCargo"), "input");
    fn_util_SeteaObligatorio($("#cmbNroCuenta"), "input");
    fn_util_SeteaObligatorio($("#cmbTipoCuenta"), "input");

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

    $('#txtNroContrato').validText({ type: 'number', length: 8 });
    $('#txtCuCliente').validText({ type: 'number', length: 10 });
    $('#txtCUClienteCargo').validText({ type: 'number', length: 10 });

    //Datos del Pago
    fn_util_inactivaInput("txtNumSecRecuperacion", "I");
    fn_util_inactivaInput("txtCodAutorizacionRecuperacion", "I");
    fn_util_inactivaInput("cmbCodMoneda", "S");
    fn_util_inactivaInput("txtMontoPago", "I");

    //Estado del Pago
    $('#txtFechaRecuperacion').datepicker().datepicker('disable');
    fn_util_inactivaInput("cmbEstadoRecuperacion", "S");
    fn_util_inactivaInput("txtMotivo", "I");
    
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

    if ($('#hddTipoTransaccion').val() == "NUEVO") {
        $("#dv_botonExtornar").hide();
        fn_seteaCamposObligatorios();
        fn_setFlagCuentaPropia("S", "Nuevo");
    }
    else{
        
        fn_setFlagCuentaPropia($("#hddFlagCuentaPropia").val(), "Consulta");

        if ($('#hddTipoTransaccion').val() == "EDITAR") {
            $("#dv_botonExtornar").hide();
            fn_util_inactivaInput("txtNroContrato", "I");
            $('#imgBsqContrato').hide();
        }
        else if ($('#hddTipoTransaccion').val() == "EXTORNO") {
            $("#dv_botonEjecutar").hide();
            $("#dv_botonAnular").hide();

            if ($('#cmbEstadoRecuperacion').val() == "E"){
                fn_util_inactivaInput("txtNroContrato", "I");
                $('#imgBsqContrato').hide();
            }
            else {
                fn_util_SeteaObligatorio($("#txtNroContrato"), "input");
            }
        }
        
        $('#txtFechaValor').addClass("css_input_inactivo");
        $('#txtFechaValor').datepicker().datepicker('disable');
        fn_util_inactivaInput("cmbNroCuotas", "S");
        fn_util_inactivaInput("cmbMovimientoBasilea", "S");

        //Via de Cobranza
        fn_setTipoViaCobranza($("#hddTipoViaCobranza").val());
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
    $('#txtCuCliente').val("");
    $('#txtRazonSocial').val("");
    $('#txtTipoPersona').val("");
    $('#txtTipoDocumento').val("");
    $('#txtNumeroDocumento').val("");
    $('#txtTipoContrato').val("");
    $('#txtMoneda').val("");
    $('#txtEjecutivoLeasing').val("");

    //Valores
    if ($('#hddTipoTransaccion').val() == "EXTORNO") {
        fn_obtenerOperacionExtornable(pCredito);
    }
    else
    {
        var paramArray = ["strCodSolicitudCredito", pCredito];

        fn_util_AjaxWM("frmPagoCuotasRegistro.aspx/ConsultaContrato",
                       paramArray,
                       function(response) {
                                fn_PonerDatosContrato(response);
                                fn_util_AjaxWM("frmPagoCuotasRegistro.aspx/ObtenerCuentasContrato",
                                               paramArray,
                                               fn_PonerCuentasContrato,
                                               function(resultado) {
                                                   parent.fn_unBlockUI();
                                                   parent.fn_mdl_mensajeIco("Se produjo un error al obtener las cuentas del contrato", "util/images/error.gif", "ERROR EN CONSULTA");
                                               }
                                        );
                       },
                       function(resultado) {
                           parent.fn_unBlockUI();
                           parent.fn_mdl_mensajeIco("Se produjo un error al obtener los datos del contrato", "util/images/error.gif", "ERROR EN CONSULTA");
                       }
                );


    }

    //Resize Pantalla
    fn_doResize();
}

function fn_PonerCuentasContrato(response) {

    parent.fn_blockUI();
    
    numCuentasAsociadas = response[0];
    
    for (i = 1; i <= numCuentasAsociadas; i++) {
        var datos = response[i].split("|");
        tipoCuentaAsociada[i] = datos[0];
        monedaCuentaAsociada[i] = datos[1];
        numeroCuentaAsociada[i] = datos[2];
    }

    if (numCuentasAsociadas > 0) {
        $("#cmbCodMonedaCargo").val(monedaCuentaAsociada[1]);
        $("#cmbTipoCuenta").val(tipoCuentaAsociada[1]);
        fn_obtenerCuentas(function() {
            $("#cmbNroCuenta").val(numeroCuentaAsociada[1]);
            if ($("#cmbNroCuenta").val() != numeroCuentaAsociada[1]) {
                parent.fn_mdl_mensajeIco("La cuenta Nro. " + numeroCuentaAsociada[1].toString() + ", asociada al contrato, no se encontró en IM/ST. Actualice las cuentas registradas para el Contrato.", "util/images/error.gif", "Validacion de Cuentas");
            }
            parent.fn_unBlockUI();
        });
    }

    parent.fn_unBlockUI();
}

function fn_PonerDatosContrato(response) {

    var objEContrato = response;

    if (objEContrato.CodError == 1) {
        $('#hddCodSolicitudCredito').val("");
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objEContrato.MsgError, "util/images/error.gif", "ERROR EN CONSULTA");
    } else {
        $('#hddCodSolicitudCredito').val(objEContrato.Codsolicitudcredito);
        $('#txtCuCliente').val(objEContrato.Codunico);
        $('#txtRazonSocial').val(objEContrato.ClienteRazonSocial);
        $('#txtTipoPersona').val(objEContrato.NombreTipoPersona);
        $('#txtTipoDocumento').val(objEContrato.NombreTipoDocIdentificacion);
        $('#txtNumeroDocumento').val(objEContrato.NroDocIdentificacion);
        $('#txtTipoContrato').val(objEContrato.SubTipoContrato);
        $('#txtMoneda').val(objEContrato.NombreMonedaAPP);
        $('#txtEjecutivoLeasing').val(objEContrato.NombreEjecutivoLeasing);

        $('#hddCodMonedaContrato').val(objEContrato.Codmoneda);
        $('#cmbCodMoneda').val(objEContrato.Codmoneda);
        $('#txtNroContrato').val(objEContrato.Codsolicitudcredito);
        
        //Consulta datos agregados del credito
        fn_obtenerTotalesPagoCuotas(objEContrato.Codsolicitudcredito, $('#txtFechaValor').val());
        
        parent.fn_unBlockUI();

        //Log
        parent.fn_util_MuestraLogPage("El sistema ubicó el N° Contrato: " + objEContrato.Codsolicitudcredito + ". Se muestran los datos.", "I");
    }

    fn_doResize();
}

function fn_obtenerTotalesPagoCuotas(strNroContrato, strFechaValor) {

    //parent.fn_blockUI();
    
    //Valores
    var paramArray =   ["pstrCodSolicitudCredito", strNroContrato,
                        "pstrFechaValor", strFechaValor];

    fn_util_AjaxWM("frmPagoCuotasRegistro.aspx/ObtenerPagoCuotasTotales",
                   paramArray,
                   fn_PonerTotalesContrato,
                   function(resultado) {
                       parent.fn_unBlockUI();
                       parent.fn_mdl_mensajeIco("Se produjo un error al obtener los datos del contrato", "util/images/error.gif", "ERROR EN CONSULTA");
                   }
            );

    //Resize Pantalla
    fn_doResize();
}

function fn_PonerTotalesContrato(response) {
   
    var objEPagoCuotas = response;

    if (objEPagoCuotas.CodError == 1) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objEPagoCuotas.MsgError, "util/images/error.gif", "ERROR EN CONSULTA");
    } else {
        parent.fn_unBlockUI();

        $('#txtMontoDesembolsado').val(Fn_util_ReturnValidDecimal2(objEPagoCuotas.MontoDesembolsado.toString()));
        $('#txtMontoRecuperado').val(Fn_util_ReturnValidDecimal2(objEPagoCuotas.MontoRecuperado.toString()));

        $('#txtSaldoCapital').val(Fn_util_ReturnValidDecimal2((objEPagoCuotas.MontoDesembolsado - objEPagoCuotas.MontoRecuperado).toString()));

        if ($('#hddTipoTransaccion').val() == "NUEVO") {

            var NroCuotasxPagar = objEPagoCuotas.NroCuotasxPagar;
            var NroCuotasVencidas = objEPagoCuotas.NroCuotasVencidas;
            var NroPagosCuotasxProcesar = objEPagoCuotas.NroPagosCuotasxProcesar;
            var NroConceptoPendiente = objEPagoCuotas.NroConceptoPendiente;
            
            $('#hddNroCuotasxPagar').val(NroCuotasxPagar);
            $('#hddNroCuotasVencidas').val(NroCuotasVencidas);
            $('#hddNroPagosCuotasxProcesar').val(NroPagosCuotasxProcesar);
            $('#hddNroConceptoPendiente').val(NroConceptoPendiente);

            if (NroCuotasxPagar <= 0) {
                parent.fn_mdl_mensajeIco("El crédito no tiene cuotas pendientes de pago.", "util/images/error.gif", "Validación de Datos");
            }
            else if(NroPagosCuotasxProcesar > 0){
                parent.fn_mdl_mensajeIco("Existen otros pagos de cuotas registrados para este crédito. No es posible instruir un nuevo pago hasta que los pagos existentes sean ejecutados o anulados.", "util/images/error.gif", "Validación de Datos");
            }
            else {

                $('#cmbNroCuotas').html(strComboVacio);
                
                var cmbNroCuotas = document.getElementById("cmbNroCuotas");
                for (var i = 1; i <= NroCuotasxPagar; i++) {
                    var op = document.createElement("option");
                    op.appendChild(document.createTextNode(i.toString() + " cuotas"));
                    op.value = i;
                    cmbNroCuotas.appendChild(op);
                }

                $('#cmbNroCuotas').val(Math.min(NroCuotasVencidas + 1, NroCuotasxPagar));

                fn_llenaGrillaProximasCuotas();
            }
        }
        
    }

    fn_doResize();
}

//****************************************************************
// Funcion		:: 	fn_cargaGrillaDetalleCuotas
// Descripción	::	Carga Grilla
// Log			:: 	IBK - RPR - 27/12/2012
//****************************************************************
function fn_cargaGrillaDetalleCuotas() {


    $("#jqGrid_lista_C").jqGrid({
        datatype: function() {
            fn_llenaGrillaDetalleCuotas();
        },
        jsonReader:
        {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Cuota', 'Fec. Venc.', 'Días Vencidos', 'Estado', 'Principal', 'Interés Vigente', 'Principal Seguro', 'Interes Seguro', 'IGV', '%Mora', 'Mora', '%Interes', 'Interes Compensatorio', 'Total'],
        colModel: [
		        { name: 'NumCuotaCalendario', index: 'NumCuotaCalendario', align: "center" },
		        { name: 'FechaVencimientoCuota', index: 'FechaVencimientoCuota', align: "center", formatter: fn_util_ValidaStringFecha },
		        { name: 'DiasVenc', index: 'DiasVenc', align: "center" },
		        { name: 'Estado', index: 'Estado', align: "center" },
		        { name: 'MontoPrincipal', index: 'MontoPrincipal', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		        { name: 'MontoInteres', index: 'MontoInteres', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		        { name: 'MontoPrincipalSeguro', index: 'MontoPrincipalSeguro', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		        { name: 'MontoInteresSeguro', index: 'MontoInteresSeguro', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		        { name: 'MontoIGV', index: 'MontoIGV', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		        { name: 'PorcenTasaMora', index: 'PorcenTasaMora', align: "right" },
		        { name: 'MontoMora', index: 'MontoMora', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		        { name: 'PorInteres', index: 'PorInteres', align: "right" },
		        { name: 'IntCompen', index: 'IntCompen', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
                { name: 'Total', index: 'Total', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal } ],
        width: glb_intWidthPantalla - 120,
        height: '100%',
        pager: '#jqGrid_pager_C',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 50,
        //rowList: [10, 20, 30],
        sortname: 'Codigocotizacion',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) { },
        ondblClickRow: function(id) { },
        gridComplete: function(id) { }
    });
    jQuery("#jqGrid_lista_C").jqGrid('navGrid', '#jqGrid_lista_C', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_C").hide();

}


//****************************************************************
// Funcion		:: 	fn_llenaGrillaDetalleCuotas
// Descripción	::	Llena grilla
// Log			:: 	IBK - RPR - 27/12/2012
//****************************************************************
function fn_llenaGrillaDetalleCuotas() {

    try {

        var strCodSolicitudCredito = $("#hddCodSolicitudCredito").val();
        var strNumSecRecuperacion = $("#hddNumSecRecuperacion").val();

        if (strCodSolicitudCredito != "" && strNumSecRecuperacion != "") {

            var arrParametros = ["pstrCodSolicitudCredito", strCodSolicitudCredito,
                             "pstrNumSecRecuperacion", strNumSecRecuperacion
                            ];

            fn_util_AjaxWM("frmPagoCuotasRegistro.aspx/ObtenerDetalleCuotas",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_C.addJSONData(jsondata);
                    fn_doResize();
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
            );
        }


    } catch (ex) {
        fn_util_alert(ex.message);
    }

}

//****************************************************************
// Funcion		:: 	fn_llenaGrillaProximasCuotas
// Descripción	::	Llena grilla
// Log			:: 	IBK - RPR - 31/12/2012
//****************************************************************
function fn_llenaGrillaProximasCuotas() {

    try {

        var arrParametros = ["pstrCodSolicitudCredito", $("#hddCodSolicitudCredito").val(),
                             "pstrFechaValor", $("#txtFechaValor").val(),
                             "pstrNroCuotas", $("#cmbNroCuotas").val()
                            ];

        parent.fn_blockUI();

        fn_util_AjaxWM("frmPagoCuotasRegistro.aspx/ObtenerProximasCuotas",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_C.addJSONData(jsondata);
                    fn_llenaGrillaProximasComisiones();
                    fn_doResize();
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


//****************************************************************
// Funcion		:: 	fn_cargaGrillaDetalleComisiones
// Descripción	::	Carga Grilla
// Log			:: 	IBK - RPR - 27/12/2012
//****************************************************************
function fn_cargaGrillaDetalleComisiones() {

    $("#jqGrid_lista_D").jqGrid({
        datatype: function() {
            fn_llenaGrillaDetalleComisiones();
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
        colNames: ['Tarifa', 'Tipo', 'Aplicacion', '%', 'Monto', 'Aplicacion'],
        colModel: [
		        { name: 'DescripComision', index: 'DescripComision', align: "left" },
		        { name: 'DescripTipoValor', index: 'DescripTipoValor', align: "left" },
		        { name: 'DescripTipoAplicacion', index: 'DescripTipoAplicacion', align: "left" },
		        { name: 'Porcentaje', index: 'Estado', align: "right" },
		        { name: 'Monto', index: 'Monto', align: "right", formatter: Fn_util_ReturnValidDecimal2, unformat: fn_util_ValidaDecimal },
		        { name: 'Aplicacion', index: 'Aplicacion', editable: true, edittype: 'checkbox', editoptions: { value: "True:False" }, formatter: "checkbox", formatoptions: { disabled: false }, align: "center" }
        ],
        width: glb_intWidthPantalla - 120,
        height: '100%',
        pager: '#jqGrid_pager_D',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 50,
        //rowList: [10, 20, 30],
        sortname: 'DescripComision',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) { },
        ondblClickRow: function(id) { },
        gridComplete: function(id) {
            jQuery(".jqgrow td input").each(function() {
                jQuery(this).click(function() {
                    fn_calcularTotales();
                });
            });
        }
    });
    jQuery("#jqGrid_lista_D").jqGrid('navGrid', '#jqGrid_lista_D', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_D").hide();

}

//****************************************************************
// Funcion		:: 	fn_llenaGrillaDetalleComisiones
// Descripción	::	Llena grilla
// Log			:: 	IBK - RPR - 27/12/2012
//****************************************************************
function fn_llenaGrillaDetalleComisiones() {

    try {
        var strCodSolicitudCredito = $("#hddCodSolicitudCredito").val();
        var strNumSecRecuperacion = $("#hddNumSecRecuperacion").val();

        if (strCodSolicitudCredito != "" && strNumSecRecuperacion != "") {

            var arrParametros = ["pstrCodSolicitudCredito", strCodSolicitudCredito,
                             "pstrNumSecRecuperacion", strNumSecRecuperacion
                            ];

            fn_util_AjaxWM("frmPagoCuotasRegistro.aspx/ObtenerDetalleComisiones",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_D.addJSONData(jsondata);
                    fn_calcularTotales();                   
                    fn_doResize();
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
            );
        }

    } catch (ex) {
        fn_util_alert(ex.message);
    }

}

//****************************************************************
// Funcion		:: 	fn_llenaGrillaProximasComisiones
// Descripción	::	Llena grilla
// Log			:: 	IBK - RPR - 03/01/2013
//****************************************************************
function fn_llenaGrillaProximasComisiones() {

    try {

        var arrParametros = ["pstrCodSolicitudCredito", $("#hddCodSolicitudCredito").val()
                            ];

        fn_util_AjaxWM("frmPagoCuotasRegistro.aspx/ObtenerProximasComisiones",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_D.addJSONData(jsondata);
                    fn_calcularTotales();
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

function fn_calcularTotalesComisiones() {

    var $grid = jQuery("#jqGrid_lista_D"), rows = $grid[0].rows, cRows = rows.length, iRow, rowId, row, cellsOfRow;
    
    var TotalConceptos = 0;
    
    for (iRow = 0; iRow < cRows; iRow++) {
        row = rows[iRow];
        if ($(row).hasClass("jqgrow")) {
            cellsOfRow = row.cells;

            var iColAplicacion = getColumnIndexByName($('#jqGrid_lista_D'), 'Aplicacion');
            var iColMonto = getColumnIndexByName($('#jqGrid_lista_D'), 'Monto');
            var arrChecked = $(cellsOfRow[iColAplicacion]).children("input:checked");

            if (arrChecked.length > 0) {
                var tmp = $(cellsOfRow[iColMonto]).text();
                TotalConceptos += fn_util_ValidaDecimal(tmp);
            }
        }
    }

    $('#txtTotalConceptos').val(Fn_util_ReturnValidDecimal2(TotalConceptos.toString()));
    return TotalConceptos;
}

function fn_calcularTotales() {
    
    try {

        var TotalPrincipal = $('#jqGrid_lista_C').jqGrid('getCol', 'MontoPrincipal', false, 'sum');
        $('#txtTotalPrincipal').val(Fn_util_ReturnValidDecimal2(TotalPrincipal.toString()));

        var TotalInteresVigente = $('#jqGrid_lista_C').jqGrid('getCol', 'MontoInteres', false, 'sum');
        $('#txtTotalInteresVigente').val(Fn_util_ReturnValidDecimal2(TotalInteresVigente.toString()));

        var TotalSeguro = $('#jqGrid_lista_C').jqGrid('getCol', 'MontoPrincipalSeguro', false, 'sum');
        $('#txtTotalSeguro').val(Fn_util_ReturnValidDecimal2(TotalSeguro.toString()));

        var TotalInteresSeguro = $('#jqGrid_lista_C').jqGrid('getCol', 'MontoInteresSeguro', false, 'sum');
        $('#txtTotalInteresSeguro').val(Fn_util_ReturnValidDecimal2(TotalInteresSeguro.toString()));

        var TotalIGV = $('#jqGrid_lista_C').jqGrid('getCol', 'MontoIGV', false, 'sum');
        $('#txtTotalIGV').val(Fn_util_ReturnValidDecimal2(TotalIGV.toString()));

        var TotalMora = $('#jqGrid_lista_C').jqGrid('getCol', 'MontoMora', false, 'sum');
        $('#txtTotalMora').val(Fn_util_ReturnValidDecimal2(TotalMora.toString()));

        var TotalInteresVencido = $('#jqGrid_lista_C').jqGrid('getCol', 'IntCompen', false, 'sum');
        $('#txtTotalInteresVencido').val(Fn_util_ReturnValidDecimal2(TotalInteresVencido.toString()));

        var TotalConceptos = fn_calcularTotalesComisiones();
        
        var TotalRecuperacion = TotalPrincipal + TotalInteresVigente + TotalSeguro + TotalInteresSeguro + TotalIGV + TotalMora + TotalInteresVencido + TotalConceptos;
        $('#txtTotalRecuperacion').val(Fn_util_ReturnValidDecimal2(TotalRecuperacion.toString()));
        $('#txtMontoPago').val(Fn_util_ReturnValidDecimal2(TotalRecuperacion.toString()));

    } catch (ex) {
        fn_util_alert(ex.message);
    }

}


//****************************************************************
// Funcion		:: 	fn_cancelar  
// Descripción	::	Regresar al listado de pagos
// Log			:: 	IBK - RPR - 04/01/2013
//****************************************************************
function fn_cancelar() {

    var strRutaRedireccion;

    if ($('#hddTipoTransaccion').val() == "EXTORNO") {
        strRutaRedireccion = "/Pagos/frmPagoCuotasListado.aspx?op=E";
    }
    else {
        strRutaRedireccion = "/Pagos/frmPagoCuotasListado.aspx";
    }
    
    parent.fn_mdl_confirma(
                    "¿Está seguro de volver?",
                    function() {
                        fn_util_globalRedirect(strRutaRedireccion);
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
            fn_obtenerCuentas(function() { });
        }

        parent.fn_unBlockUI();
    }
}

function fn_obtenerCuentas(fn_onSuccess) {

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

            fn_util_AjaxSyncWM("frmPagoCuotasRegistro.aspx/ObtenerCuentas",
                arrParametros,
                function(request) {

                    var cmbNroCuenta = document.getElementById("cmbNroCuenta");

                    for (var i = 1; i <= 2 * request[0] - 1; i += 2) {
                        var opt1 = document.createElement("option");
                        var strNroCuentaFormateado = request[i].toString() + " (" + request[i + 1].toString() + ")";
                        var strNroCuenta = request[i].toString().replace("-", "");
                        
                        for (var j = 1; j <= numCuentasAsociadas; j++) {
                            if (tipoCuentaAsociada[j] == tipoCuenta && monedaCuentaAsociada[j] == codMonedaCargo && numeroCuentaAsociada[j] == strNroCuenta) {
                                strNroCuentaFormateado = strNroCuentaFormateado + " (Cuenta Asociada)";
                                break;
                            }
                        }
                        opt1.appendChild(document.createTextNode(strNroCuentaFormateado));
                        opt1.value = strNroCuenta;
                        cmbNroCuenta.appendChild(opt1);
                    }

                    fn_onSuccess();

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

            fn_seleccionarConceptosAplicables();
		    
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

function fn_seleccionarConceptosAplicables() {

    var lista = jQuery("#jqGrid_lista_D").getDataIDs();
    for (i = 0; i < lista.length; i++) {
        var strAplicacion = "1";
        if ($('#jqGrid_lista_D').jqGrid('getCell', lista[i], 'Aplicacion') == "False") {
            strAplicacion = "0";
        }
        var strNumSecRecupComi = lista[i];

        var arrParametros = ["pstrCodOperacionActiva", $("#hddCodSolicitudCredito").val(),
                             "pstrCodigoLiquidacion", "",
                             "pstrTipoRecuperacion", "R",
                             "pstrNumCuotaCalendario", "-1",
                             "pstrNumSecRecupComi", strNumSecRecupComi,
                             "pstrAplicacion", strAplicacion];

        fn_util_AjaxSyncWM("frmPagoCuotasRegistro.aspx/SeleccionarAplicacionComision",
						 arrParametros,
						 function() { },
						function(resultado) {
						    parent.fn_unBlockUI();
						    var error = eval("(" + resultado.responseText + ")");
						    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al Ejecutar Pago");
						}
			);
    }

}

//****************************************************************
// Funcion		:: 	fn_anularPagoCuotas
// Descripción	::	Anular Pago de Cuotas
// Log			:: 	IBK - RPR - 24/01/2013
//****************************************************************
function fn_anular(strMotivoAnulacion) {

    var strTipoRecuperacion = "R";
    var arrParametros = ["pstrCodOperacionActiva", $("#hddCodSolicitudCredito").val(),
                                 "pstrTipoRecuperacion", strTipoRecuperacion,
		                         "pstrNumSecRecuperacion", $("#hddNumSecRecuperacion").val(),
		                         "pstrCodAutorizacionRecuperacion", $("#txtCodAutorizacionRecuperacion").val(),
		                         "pstrEstadoRecuperacion", $("#hddEstadoRecuperacion").val(),
								 "pstrFechaRecuperacion", $("#txtFechaRecuperacion").val(),
								 "pstrTipoViaCobranza", $("#hddTipoViaCobranza").val(),
								 "pstrFlagCuentaPropia", $("#hddFlagCuentaPropia").val(),
								 "pstrCodUnicoClienteCargo", $("#txtCUClienteCargo").val(),
								 "pstrCodMonedaCargo", $("#cmbCodMonedaCargo").val(),
								 "pstrTipoCuenta", $("#cmbTipoCuenta").val(),
								 "pstrNroCuenta", $("#cmbNroCuenta").val(),
								 "pstrMotivoAnulacionExtorno", strMotivoAnulacion ];

    parent.fn_blockUI();

    fn_util_AjaxSyncWM("frmPagoCuotasRegistro.aspx/AnularPagoCuotas",
						 arrParametros,
						 fn_retornoAnulacionPagoCuotas,
						function(resultado) {
						    parent.fn_unBlockUI();
						    var error = eval("(" + resultado.responseText + ")");
						    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al Anular Pago");
						}
			);
}

function fn_anularPagoCuotas() {

    if ($("#hddEstadoRecuperacion").val() == 'A') {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("El pago ya se encuentra Anulado.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    if ($("#hddEstadoRecuperacion").val() == 'H') {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("No es posible anular este pago porque ya ha sido Ejecutado. Utilice la opción de Extornos.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    if ($("#hddEstadoRecuperacion").val() != 'I' && $("#hddEstadoRecuperacion").val() != 'C') {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("El pago debe estar en estado Ingresado o Enviado a Host para poder ser anulado.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    strOperacionModal = "Anular";
    parent.fn_util_AbreModal("Pago de Cuotas :: Anulación", "Pagos/frmConfirmarOperacion.aspx", 600, 300, function() { });
}

function fn_extornar(strMotivoExtorno) {
    var strTipoRecuperacion = "R";
    var arrParametros = ["pstrCodOperacionActiva", $("#hddCodSolicitudCredito").val(),
                                 "pstrTipoRecuperacion", strTipoRecuperacion,
		                         "pstrNumSecRecuperacion", $("#hddNumSecRecuperacion").val(),
		                         "pstrCodAutorizacionRecuperacion", $("#txtCodAutorizacionRecuperacion").val(),
		                         "pstrEstadoRecuperacion", $("#hddEstadoRecuperacion").val(),
								 "pstrFechaRecuperacion", $("#txtFechaRecuperacion").val(),
								 "pstrTipoViaCobranza", $("#hddTipoViaCobranza").val(),
								 "pstrFlagCuentaPropia", $("#hddFlagCuentaPropia").val(),
								 "pstrCodUnicoClienteCargo", $("#txtCUClienteCargo").val(),
								 "pstrCodMonedaCargo", $("#cmbCodMonedaCargo").val(),
								 "pstrTipoCuenta", $("#cmbTipoCuenta").val(),
								 "pstrNroCuenta", $("#cmbNroCuenta").val(),
								 "pstrMotivoAnulacionExtorno", strMotivoExtorno ];

    parent.fn_blockUI();

    fn_util_AjaxSyncWM("frmPagoCuotasRegistro.aspx/ExtornarPagoCuotas",
						 arrParametros,
						 fn_retornoExtornoPagoCuotas,
						function(resultado) {
						    parent.fn_unBlockUI();
						    var error = eval("(" + resultado.responseText + ")");
						    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al Anular Pago");
						}
			);
}

function fn_extornarPagoCuotas() {

    if ($("#hddCodSolicitudCredito").val() == '') {
        parent.fn_mdl_mensajeIco("Debe seleccionar un Nro. de Crédito para extornar operaciones.", "util/images/error.gif", "Validación de Datos");
        return;
    }
    
    if ($("#hddEstadoRecuperacion").val() == 'E') {
        parent.fn_mdl_mensajeIco("El pago ya se encuentra Extornado.", "util/images/error.gif", "Validación de Datos");
        return;
    }
    
    /*
    if ($("#hddEstadoRecuperacion").val() == 'H') {
    parent.fn_mdl_mensajeIco("No es posible anular este pago porque ya ha sido Ejecutado. Utilice la opción de Extornos.", "util/images/error.gif", "Validación de Datos");
    return;
    }

    if ($("#hddEstadoRecuperacion").val() != 'I' && $("#hddEstadoRecuperacion").val() != 'C') {
    parent.fn_mdl_mensajeIco("El pago debe estar en estado Ingresado o Enviado a Host para poder ser anulado.", "util/images/error.gif", "Validación de Datos");
    return;
    }
    */

    strOperacionModal = "Extornar";
    parent.fn_util_AbreModal("Pago de Cuotas :: Extorno", "Pagos/frmConfirmarOperacion.aspx", 600, 300, function() { });

}

//****************************************************************
// Funcion		:: 	fn_retornoEjecucionPagoCuotas
// Descripción	::	Recibe respuesta de la ejecucion de Pago de Cuotas (NumSecRecuperacion, Cod Autorizacion, etc..)
// Log			:: 	IBK - RPR - 07/01/2013
//****************************************************************
function fn_retornoEjecucionPagoCuotas(response) {

    var objECreditoRecuperacion = response;

    if (objECreditoRecuperacion.CodError != 0) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objECreditoRecuperacion.MsgError, "util/images/error.gif", "Error en Ejecución de Pago");
    } else {

        parent.fn_unBlockUI();

        //Log
        parent.fn_util_MuestraLogPage("Ejecución correcta. N° Contrato: " + objECreditoRecuperacion.CodOperacionActiva + ". Se muestran los datos.", "I");

        fn_util_globalRedirect("/Pagos/frmPagoCuotasRegistro.aspx?hddCodSolicitudCredito=" + objECreditoRecuperacion.CodOperacionActiva + "&hddNumSecRecuperacion=" + objECreditoRecuperacion.NumSecRecuperacion);
    }

    fn_doResize();
}

//****************************************************************
// Funcion		:: 	fn_retornoAnulacionPagoCuotas
// Descripción	::	Recibe respuesta de la anulacion de Pago de Cuotas
// Log			:: 	IBK - RPR - 25/01/2013
//****************************************************************
function fn_retornoAnulacionPagoCuotas(response) {

    var objECreditoRecuperacion = response;

    if (objECreditoRecuperacion.CodError != 0) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objECreditoRecuperacion.MsgError, "util/images/error.gif", "Validación de Datos");
    } else {
        parent.fn_unBlockUI();

        //Log
        parent.fn_util_MuestraLogPage("Anulación correcta. Se muestran los datos actualizados.", "I");

        fn_util_globalRedirect("/Pagos/frmPagoCuotasRegistro.aspx?hddCodSolicitudCredito=" + objECreditoRecuperacion.CodOperacionActiva + "&hddNumSecRecuperacion=" + objECreditoRecuperacion.NumSecRecuperacion);
    }

    fn_doResize();
}

function fn_retornoExtornoPagoCuotas(response) {

    var objECreditoRecuperacion = response;

    if (objECreditoRecuperacion.CodError != 0) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objECreditoRecuperacion.MsgError, "util/images/error.gif", "Validación de Datos");
    } else {

        parent.fn_unBlockUI();

        //Log
        parent.fn_util_MuestraLogPage("Extorno correcto. Se muestran los datos actualizados.", "I");

        fn_util_globalRedirect("/Pagos/frmPagoCuotasRegistro.aspx?hddCodSolicitudCredito=" + objECreditoRecuperacion.CodOperacionActiva + "&hddNumSecRecuperacion=" + objECreditoRecuperacion.NumSecRecuperacion);
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
