//****************************************************************
// Variables Globales
//****************************************************************

var strComboVacio = "<option value='0'>[-Seleccione-]</option>";

var numCuentasAsociadas = 0;
var tipoCuentaAsociada = new Array();
var monedaCuentaAsociada = new Array();
var numeroCuentaAsociada = new Array();

var strOperacionModal;

var editandoConcepto = false;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 19/09/2012
//****************************************************************
$(document).ready(function() {
    fn_doResize();

    //Setea Calendario
    fn_util_SeteaCalendario($("#txtFechaRecuperacion"));
    fn_util_SeteaCalendario($("#txtFechaProcesoPago"));
    fn_util_SeteaCalendario($("#txtFechaValor"));

    $('#txtNroContrato').validText({ type: 'number', length: 8 });

    $("#dv_botonGrabar").hide();

    if ($('#hddTipoTransaccion').val() == "EDITAR") {
        $("#dv_botonEjecutar").hide();
        fn_inicializaCampos("Consulta");

        if ($("#hddFlagCuentaPropia").val() == "N") {
            fn_consultaRM();
        }

        if ($('#cmbEstadoRecuperacion').val() == "E") {
            $("#dv_botonEditar").hide();
            $("#dv_botonAnular").hide();
            $("#dv_botonExtornar").hide();
        }
        else if ($('#cmbEstadoRecuperacion').val() == "A") {
            $("#dv_botonEditar").hide();
            $("#dv_botonAnular").hide();
        }
    }
    else {
        $("#dv_botonEditar").hide();
        $("#dv_botonAnular").hide();
        $("#dv_botonExtornar").hide();
        fn_inicializaCampos("Nuevo");
    }

    $('#txtMontoPago').val(Fn_util_ReturnValidDecimal2($('#txtMontoPago').val()));

    $('#txtMontoReembolso').val(Fn_util_ReturnValidDecimal2($('#txtMontoReembolso').val()));
    $('#txtMontoIGVReembolso').val(Fn_util_ReturnValidDecimal2($('#txtMontoIGVReembolso').val()));
    $('#txtMontoComision').val(Fn_util_ReturnValidDecimal2($('#txtMontoComision').val()));
    $('#txtMontoIGV').val(Fn_util_ReturnValidDecimal2($('#txtMontoIGV').val()));

    $('#txtReajusteReembolso').val(Fn_util_ReturnValidDecimal2($('#txtReajusteReembolso').val()));
    $('#txtReajusteIGVReembolso').val(Fn_util_ReturnValidDecimal2($('#txtReajusteIGVReembolso').val()));
    $('#txtReajusteComision').val(Fn_util_ReturnValidDecimal2($('#txtReajusteComision').val()));
    $('#txtReajusteIGV').val(Fn_util_ReturnValidDecimal2($('#txtReajusteIGV').val()));

    $('#txtMontoReembolso').validNumber({ value: '', decimals: 2, length: 15 });
    $('#txtMontoIGVReembolso').validNumber({ value: '', decimals: 2, length: 15 });
    $('#txtMontoComision').validNumber({ value: '', decimals: 2, length: 15 });
    $('#txtMontoIGV').validNumber({ value: '', decimals: 2, length: 15 });

    $('#txtReajusteReembolso').validNumber({ value: '', decimals: 2, length: 15 });
    $('#txtReajusteIGVReembolso').validNumber({ value: '', decimals: 2, length: 15 });
    $('#txtReajusteComision').validNumber({ value: '', decimals: 2, length: 15 });
    $('#txtReajusteIGV').validNumber({ value: '', decimals: 2, length: 15 });

    $('#txtMontoPago').validNumber({ value: '', decimals: 2, length: 15 });

    fn_calculaTotales();

    $('#imgBsqContrato').click(function() {

        var strCodigo = $("#txtNroContrato").val();

        if (strCodigo == "") {
            VentanaCreditos();
        } else {
            fn_obtenerCredito(strCodigo)
        }

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

    $('#txtMontoReembolso').change(function() {
        fn_calculaTotales();
    });
    $('#txtMontoIGVReembolso').change(function() {
        fn_calculaTotales();
    });
    $('#txtMontoComision').change(function() {
        fn_calculaTotales();
    });
    $('#txtMontoIGV').change(function() {
        fn_calculaTotales();
    });

    if ($('#cmbEstadoRecuperacion').val() != "E" && $('#cmbEstadoRecuperacion').val() != "A" && $('#txtMotivo').val() == "") {
        $('#rw_MotivoAnulacionExtorno').hide();
    }

    if ($('#hddPerfilUsuario').val() != "1" && $('#hddPerfilUsuario').val() != "6" && $('#hddPerfilUsuario').val() != "10" && $('#hddPerfilUsuario').val() != "11") {
        $('#dv_botonAnular').hide();
        $('#dv_botonExtornar').hide();
        $('#dv_botonEditar').hide();
    }

    //On load Page (siempre al final)
    fn_onLoadPage();

});

function fn_cancelar() {

    if (editandoConcepto == false) {

        parent.fn_mdl_confirma(
                    "¿Está seguro de Volver?",
                    function() {
                        fn_util_globalRedirect("/Pagos/frmPagoConceptoListado.aspx");
                    },
                    "Util/images/question.gif",
                    function() { },
                    'Confirmación'
                   );
    }
    else {

        parent.fn_mdl_confirma(
                    "¿Desea cancelar los cambios?",
                    function() {
                        fn_util_globalRedirect("/Pagos/frmPagoConceptoRegistro.aspx?hddCodSolicitudCredito=" + $('#hddCodSolicitudCredito').val() + "&hddNumSecRecuperacion=" + $('#hddNumSecRecuperacion').val());
                    },
                    "Util/images/question.gif",
                    function() { },
                    'Confirmación'
                   );
    }
      
}
//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 19/09/2012
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
    fn_util_inactivaInput("txtEstadoContrato", "I");

    $('#txtNroContrato').validText({ type: 'number', length: 8 });
    $('#txtCuCliente').validText({ type: 'number', length: 10 });
    $('#txtCUClienteCargo').validText({ type: 'number', length: 10 });

    //Datos del Pago
    fn_util_inactivaInput("txtNumSecRecuperacion", "I");
    fn_util_inactivaInput("txtCodAutorizacionRecuperacion", "I");
    fn_util_inactivaInput("txtMontoPago", "I");

    fn_util_inactivaInput("txtReajusteReembolso", "I");
    fn_util_inactivaInput("txtReajusteIGVReembolso", "I");
    fn_util_inactivaInput("txtReajusteComision", "I");
    fn_util_inactivaInput("txtReajusteIGV", "I");

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

    //Vias de cobranza no soportadas
    fn_util_inactivaInput("rdbViaCondonacion", "S");

    if (strOperacion == "Nuevo") {

        fn_util_SeteaObligatorio($("#txtNroContrato"), "input");

        fn_util_SeteaObligatorio($('#txtCUClienteCargo'), "input");
        fn_util_SeteaObligatorio($("#cmbCodMonedaCargo"), "input");
        fn_util_SeteaObligatorio($("#cmbNroCuenta"), "input");
        fn_util_SeteaObligatorio($("#cmbTipoCuenta"), "input");

        fn_util_SeteaObligatorio($("#txtFechaValor"), "input");

        fn_util_SeteaObligatorio($("#txtMontoReembolso"), "input");
        fn_util_SeteaObligatorio($("#txtMontoIGVReembolso"), "input");
        fn_util_SeteaObligatorio($("#txtMontoComision"), "input");
        fn_util_SeteaObligatorio($("#txtMontoIGV"), "input");

        fn_util_SeteaObligatorio($("#cmbConcepto"), "input");
        fn_util_SeteaObligatorio($("#cmbCodMoneda"), "input");
        fn_util_SeteaObligatorio($("#cmbEstadoOpe"), "input");

        fn_setFlagCuentaPropia("S", "Nuevo");


        $("#dv_img_botonEditar").hide();
        $("#Div2").show();
        $("#rdbCuenta").attr('checked', true);

    }
    else if (strOperacion == "Consulta") {
        fn_setFlagCuentaPropia($("#hddFlagCuentaPropia").val(), "Consulta");

        fn_util_inactivaInput("txtNroContrato", "I");
        $('#imgBsqContrato').hide();
        $('#txtFechaValor').addClass("css_input_inactivo");
        $('#txtFechaValor').datepicker().datepicker('disable');

        fn_util_inactivaInput("cmbConcepto", "S");

        fn_util_inactivaInput("txtMontoReembolso", "I");
        fn_util_inactivaInput("txtMontoIGVReembolso", "I");
        fn_util_inactivaInput("txtMontoComision", "I");
        fn_util_inactivaInput("txtMontoIGV", "I");

        fn_util_inactivaInput("cmbCodMoneda", "S");

        //Via de Cobranza
        fn_setTipoViaCobranza($("#hddTipoViaCobranza").val());
        fn_util_inactivaInput("rdbViaCuenta", "S");
        fn_util_inactivaInput("rdbViaVentanilla", "S");
        fn_util_inactivaInput("rdbViaAdministrativo", "S");
        fn_util_inactivaInput("rdbViaCondonacion", "S");
        
        fn_util_inactivaInput("rdbCuentaPropia", "S");
        fn_util_inactivaInput("rdbOtraCuenta", "S");

        fn_util_inactivaInput("txtCUClienteCargo", "I");
        fn_util_inactivaInput("txtNombreClienteCargo", "I");
        $('#imgBsqClienteRM').hide();
        fn_util_inactivaInput("cmbCodMonedaCargo", "S");
        fn_util_inactivaInput("cmbTipoCuenta", "S");
        fn_util_inactivaInput("cmbNroCuenta", "S");

    }

    ////

    if ($("#hddTipoViaCobranza").val() == "Cuenta") {
        $('input[id=rdbCuenta]').attr('checked', true);
    }
    else if ($("#hddTipoViaCobranza").val() == "Ventanilla") {
        $('input[id=rdbVentanilla]').attr('checked', true);
    }
    else {
        $('input[id=rdbCuenta]').attr('checked', false);
        $('input[id=rdbVentanilla]').attr('checked', false);
    }

    $('#rdbVentanilla').attr('disabled', 'disabled');
    $('#rdbCuenta').attr('disabled', 'disabled');
    $('#txtfechavalor').attr('disabled', 'disabled');
    $('#txtfechaproceso').attr('disabled', 'disabled');


    $("#dv_img_botonEditar").show();
    $("#Div2").hide();
}

function fn_inicializaEditar() {

    fn_util_SeteaObligatorio($("#txtfechavalor"), "input");
    fn_util_SeteaObligatorio($("#cmbMoneda1"), "input");
    fn_util_SeteaObligatorio($("#cmbmoneda"), "input");
    fn_util_SeteaObligatorio($("#cmbEstadoOpe"), "input");

    fn_util_inactivaInput("txtNroContrato", "I");
    fn_util_inactivaInput("txtCuCliente", "I");
    fn_util_inactivaInput("txtRazonSocial", "I");
    fn_util_inactivaInput("txtTipoPersona", "I");
    fn_util_inactivaInput("txtTipoDocumento", "I");
    fn_util_inactivaInput("txtNumeroDocumento", "I");
    fn_util_inactivaInput("txtTipoContrato", "I");
    fn_util_inactivaInput("txtMoneda", "I");
    fn_util_inactivaInput("txtEjecutivoLeasing", "I");
    //

    $('#txtNumeroCuenta1').removeAttr('disabled');
    $('#txtNumeroCuenta1').attr('enabled', true);
    $('#txtNumeroCuenta1').attr('enabled', 'enabled');
    //
    $('#cmbTipoCuenta1').removeAttr('disabled');
    $('#cmbTipoCuenta1').attr('enabled', true);
    $('#cmbTipoCuenta1').attr('enabled', 'enabled');

    $('#rdbVentanilla').removeAttr('disabled');
    $('#rdbVentanilla').attr('enabled', true);
    $('#rdbVentanilla').attr('enabled', 'enabled');

    $('#rdbCuenta').removeAttr('disabled');
    $('#rdbCuenta').attr('enabled', true);
    $('#rdbCuenta').attr('enabled', 'enabled');

    $('#cmbMoneda1').removeAttr('disabled');
    $('#cmbMoneda1').attr('enabled', true);
    $('#cmbMoneda1').attr('enabled', 'enabled');

    $('#cmbEstadoOpe').removeAttr('disabled');
    $('#cmbEstadoOpe').attr('enabled', true);
    $('#cmbEstadoOpe').attr('enabled', 'enabled');

    $('#cmbmoneda').removeAttr('disabled');
    $('#cmbmoneda').attr('enabled', true);
    $('#cmbmoneda').attr('enabled', 'enabled');

    $('#txtfechavalor').removeAttr('disabled');
    $('#txtfechavalor').attr('enabled', true);
    $('#txtfechavalor').attr('enabled', 'enabled');

    $("#dv_img_botonEditar").hide();
    $("#Div2").show();


    fn_util_SeteaObligatorio($("#cmbconcepto"), "input");

    fn_util_SeteaObligatorio($("#txtMontoReembolso"), "input");
    fn_util_SeteaObligatorio($("#txtMontoIGVReembolso"), "input");
    fn_util_SeteaObligatorio($("#txtMontoComision"), "input");
    fn_util_SeteaObligatorio($("#txtMontoIGV"), "input");

}

function fn_realizaBusqueda() {

    try {
        parent.fn_blockUI();
        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
		                     "pstrNroContrato", $("#txtNroContrato").val(),
		                     "pstrSecfinanciamiento", $("#hddNumSecRecuperacion").val()
                            ];
        fn_util_AjaxWM("frmPagoConceptoRegistro.aspx/ListadoPagoConceptoxNumeroSecuencia",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);
                    parent.fn_unBlockUI();
                    fn_doResize();
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

    } catch (ex) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }
}
function fn_Editar() {
    fn_inicializaEditar();
}
function fn_ActivaControles() {
    if ($("#rdbVentanilla").attr('checked')) {
        $('#txtNumeroCuenta1').attr('disabled', 'disabled');
        $('#cmbTipoCuenta1').attr('disabled', 'disabled');
        $('#cmbMoneda1').attr('disabled', 'disabled');
    }
    else {
        $('#txtNumeroCuenta1').removeAttr('disabled');
        $('#txtNumeroCuenta1').attr('enabled', true);
        $('#txtNumeroCuenta1').attr('enabled', 'enabled');

        $('#cmbTipoCuenta1').removeAttr('disabled');
        $('#cmbTipoCuenta1').attr('enabled', true);
        $('#cmbTipoCuenta1').attr('enabled', 'enabled');

        $('#cmbMoneda1').removeAttr('disabled');
        $('#cmbMoneda1').attr('enabled', true);
        $('#cmbMoneda1').attr('enabled', 'enabled');

        fn_util_SeteaObligatorio($("#cmbTipoCuenta1"), "input");
        fn_util_SeteaObligatorio($("#txtNumeroCuenta1"), "input");
    }
}
function fn_AgregarConceptos() {

    var strError = new StringBuilderEx();
    strError = fn_Valida(strError);
    if ($("#txtNroContrato").val() == '') {
        strError = ('<br/> El campo Nro Credito es obligatorio. ');
    }

    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }
    else {
        //var rows = jQuery("#jqGrid_lista_A").jqGrid('getRowData');
        if ($("#jqGrid_lista_A").getGridParam("reccount") == 1) {
            parent.fn_mdl_mensajeIco("No se puede agregar más de un concepto.", "util/images/warning.gif", "Pago Conceptos.");
        } else {
            //RegistraPagoConceptoDetalle
            try {
                parent.fn_blockUI();
                var arrParametros = [
                             "pstrNroContrato", $("#txtNroContrato").val(),
                             "pstrCodConcepto", $("#cmbconcepto").val(),
                             "pstrDescConcepto", $("#cmbconcepto").val(),
                             "pdecMonto", $("#txtMontoReg").val(),
                             "pdecMontoIGV", $("#txtMontoIGV").val(),
                             "pstrNumSecuencia", $("#txtnumsecrecuperacion").val() == '' ? '0' : $("#txtnumsecrecuperacion").val(),
                             "pstrNumSecuenciaAutorizacion", $("#txtcodautorizacionrecuperacion").val() == '' ? '0' : $("#txtcodautorizacionrecuperacion").val()
                            ];

                fn_util_AjaxSyncWM("frmPagoConceptoRegistro.aspx/GrabaConceptoDetalleTemporal",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);
                    fn_realizaBusquedaTemporal();
                    parent.fn_unBlockUI();
                    fn_doResize();
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

            } catch (ex) {
                parent.fn_unBlockUI();
                fn_util_alert(ex.message);
            }
        }
    }
}


function fn_Modificar() {

    //Graba PagoConcepto
    var strError = new StringBuilderEx();

    strError = fn_Valida(strError);
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
        $('#DivCancelar').hide();
        $('#DivModifica').hide();
        $('#DivEditar').show();
        $('#DivEliminar').show();
        $('#DivAgregar').show();

        fn_util_SeteaObligatorio($("#txtMontoReg"), "input");
        fn_util_SeteaObligatorio($("#txtMontoIGV"), "input");
        fn_util_SeteaObligatorio($("#cmbconcepto"), "input");

    }
    else {
        //Modifica Detalle Concepto
        try {
            parent.fn_blockUI();
            var arrParametros = [
                             "pstrNroContrato", $("#txtNroContrato").val(),
                             "pstrCodConcepto", $("#cmbconcepto").val(),
                             "pstrDescConcepto", $("#cmbconcepto").val(),
                             "pdecMonto", $("#txtMontoReg").val(),
                             "pdecMontoIGV", $("#txtMontoIGV").val(),
                             "pstrNumSecuencia", $("#txtnumsecrecuperacion").val(),
                             "pstrNumSecuenciaAutorizacion", $("#txtcodautorizacionrecuperacion").val()
                            ];

            fn_util_AjaxSyncWM("frmPagoConceptoRegistro.aspx/ActualizaConceptoDetalleTemporal",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);
                    fn_realizaBusquedaTemporal();
                    parent.fn_unBlockUI();
                    fn_doResize();
                },
                function(request) {
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
// Funcion		:: 	fn_TipoCambioDia
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_TipoCambioDia(pstrModalidad) {
    var arrParametros = ["pstrCodMoneda", $("#hidCodMoneda").val(),
                         "pstrFecha", obtiene_fecha(),
                         "pstrModalidad", pstrModalidad
                        ];
    fn_util_AjaxSyncWM("frmPagoConceptoRegistro.aspx/ConsultarTipoCambio",
                            arrParametros,
                            function(resultado) {
                                fnObtenerTipoCambioDia(resultado);
                            },
                            function(resultado) {
                                parent.fn_unBlockUI();
                                var error = eval("(" + resultado.responseText + ")");
                                parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL PAGO CONCEPTO");
                            }
     );
}


//****************************************************************
// Funcion		:: 	fnObtenerTipoCambioDia
// Descripción	::	
// Log			:: 	
//****************************************************************
function fnObtenerTipoCambioDia(resultado) {

    var varresult = resultado.split("|");
    var varTipoCambio;
    if (varresult[0] == "0") {
        varTipoCambio = varresult[1].split("$");
        if ($("#hidCodMoneda").val() == strMonedaSoles && $("#cmbMoneda").val() == strMonedaDolares) {
            $("#hidTipoCambioDia").val(varTipoCambio[0]);
        } else if ($("#hidCodMoneda").val() == strMonedaDolares && $("#cmbMoneda").val() == strMonedaSoles) {
            $("#hidTipoCambioDia").val(varTipoCambio[1]);
        }
        else {
            $("#hidTipoCambioDia").val(varTipoCambio[0]);
        }
        $("#txtTipoCambioSunat").val($("#hidTipoCambioDia").val());
    } else {
        parent.fn_mdl_mensajeIco(varresult[1], "util/images/error.gif", "ERROR OBTENER TIPO CAMBIO");
    }
}

//****************************************************************
// Funcion		:: 	obtiene_fecha
// Descripción	::	
// Log			:: 	
//****************************************************************
function obtiene_fecha() {
    var fecha_actual = new Date();

    var dia = fecha_actual.getDate();
    var mes = fecha_actual.getMonth() + 1;
    var anio = fecha_actual.getFullYear();

    if (mes < 10)
        mes = '0' + mes;
    if (dia < 10)
        dia = '0' + dia;
    //return (anio + mes + dia);
    return (anio + "" + mes + "" + dia);

}

function fn_realizaBusquedaTemporal() {

    var numsecuencia = $("#txtnumsecrecuperacion").val() == '' ? 0 : $("#txtnumsecrecuperacion").val();
    var autorizacion = $("#txtcodautorizacionrecuperacion").val() == '' ? '0' : $("#txtcodautorizacionrecuperacion").val()
    try {
        parent.fn_blockUI();
        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
		                     "pstrNroContrato", $("#txtNroContrato").val(),
		                     "pstrNumSecuencia", numsecuencia,
		                     "pstrNumSecuenciaAutorizacion", autorizacion
                            ];
        fn_util_AjaxWM("frmPagoConceptoRegistro.aspx/ListadoPagoConceptoxNumeroSecuenciaTemporal",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);
                    parent.fn_unBlockUI();
                    fn_doResize();
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

    } catch (ex) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }
}

function VentanaCreditos() {
    parent.fn_util_AbreModal("Pagos :: Pagos otros Conceptos", "Pagos/frmCreditoConsulta.aspx", 850, 600, function() { });
}

function fn_ejecutarPagoConcepto(flagNuevo) {

    if ($("#hddCodSolicitudCredito").val() == "") {
        parent.fn_mdl_mensajeIco("Por favor ingrese un número de contrato.", "util/images/error.gif", "Validación de Datos");
        return;
    }
    if ($("#cmbConcepto").val() == "0") {
        parent.fn_mdl_mensajeIco("Seleccione un concepto de pago.", "util/images/error.gif", "Validación de Datos");
        return;
    }
    if ($("#cmbCodMoneda").val() == "0") {
        parent.fn_mdl_mensajeIco("Seleccione la moneda del concepto.", "util/images/error.gif", "Validación de Datos");
        return;
    }
    if ($("#txtFechaValor").val() == "") {
        parent.fn_mdl_mensajeIco("Por favor ingrese la fecha valor de pago.", "util/images/error.gif", "Validación de Datos");
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

    if ($("#txtMontoReembolso").val() < 0 || $("#txtMontoIGVReembolso").val() < 0 || $("#txtMontoComision").val() < 0 || $("#txtMontoIGV").val() < 0) {
        parent.fn_mdl_mensajeIco("Los montos no pueden ser negativos.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    if ($("#txtMontoPago").val() <= 0) {
        parent.fn_mdl_mensajeIco("El monto total no puede ser menor o igual a cero.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    var strMensajeConfirmacion;
    
    if (flagNuevo == true) {
        strMensajeConfirmacion = "¿Está seguro que desea registrar el Pago de otros Conceptos?"
    }
    else {
        strMensajeConfirmacion = "¿Está seguro que desea modificar el Pago de otros Conceptos?"
    }
    
    parent.fn_mdl_confirma(
    strMensajeConfirmacion,
	function() {
	    parent.fn_blockUI();

	    var arrParametros = ["pstrCodOperacionActiva", $("#hddCodSolicitudCredito").val(),
	                         "pstrNumSecRecuperacion", $("#hddNumSecRecuperacion").val(),
	                         "pstrCodAutorizacion", $("#txtCodAutorizacionRecuperacion").val(),
							 "pstrFechaValorRecuperacion", $("#txtFechaValor").val(),
							 "pstrCodMoneda", $("#cmbCodMoneda").val(),
							 "pstrCodComisionTipo", $("#cmbConcepto").val(),
							 "pstrMontoReembolso", fn_util_ValidaDecimal($("#txtMontoReembolso").val()),
							 "pstrMontoIGVReembolso", fn_util_ValidaDecimal($("#txtMontoIGVReembolso").val()),
							 "pstrMontoComision", fn_util_ValidaDecimal($("#txtMontoComision").val()),
							 "pstrMontoIGV", fn_util_ValidaDecimal($("#txtMontoIGV").val()),
							 "pstrTipoViaCobranza", $("#hddTipoViaCobranza").val(),
							 "pstrFlagCuentaPropia", $("#hddFlagCuentaPropia").val(),
							 "pstrCodUnicoClienteCargo", $("#txtCUClienteCargo").val(),
							 "pstrCodMonedaCargo", $("#cmbCodMonedaCargo").val(),
							 "pstrTipoCuenta", $("#cmbTipoCuenta").val(),
							 "pstrNroCuenta", $("#cmbNroCuenta").val(),
							 "pstrFlagNuevo", flagNuevo];


	    fn_util_AjaxSyncWM("frmPagoConceptoRegistro.aspx/EjecutaPagoConcepto",
					 arrParametros,
					 fn_retornoEjecucionPagoConcepto,
					function(resultado) {
					    parent.fn_unBlockUI();
					    var error = eval("(" + resultado.responseText + ")");
					    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al Ejecutar Pago");
					}
		);

	},
    "Util/images/question.gif",
    function() { },
    'Ejecutar Pago de otros Conceptos');

}

function fn_calculaTotales() {
    var totalMontos = fn_util_ValidaDecimal($('#txtMontoComision').val()) + fn_util_ValidaDecimal($('#txtMontoIGV').val()) + fn_util_ValidaDecimal($('#txtMontoReembolso').val()) + fn_util_ValidaDecimal($('#txtMontoIGVReembolso').val());
    var totalReajuste = fn_util_ValidaDecimal($('#txtReajusteComision').val()) + fn_util_ValidaDecimal($('#txtReajusteIGV').val()) + fn_util_ValidaDecimal($('#txtReajusteReembolso').val()) + fn_util_ValidaDecimal($('#txtReajusteIGVReembolso').val());
    $('#txtMontoPago').val(Fn_util_ReturnValidDecimal2((totalMontos + totalReajuste).toString()));
}

function fn_retornoEjecucionPagoConcepto(response) {

    var objECreditoRecuperacion = response;

    if (objECreditoRecuperacion.CodError != 0) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objECreditoRecuperacion.MsgError, "util/images/error.gif", "Error en Ejecución de Pago");
    } else {

        parent.fn_unBlockUI();

        //Log
        parent.fn_util_MuestraLogPage("Ejecución correcta. N° Contrato: " + objECreditoRecuperacion.CodOperacionActiva + ". Se muestran los datos.", "I");

        fn_util_globalRedirect("/Pagos/frmPagoConceptoRegistro.aspx?hddCodSolicitudCredito=" + objECreditoRecuperacion.CodOperacionActiva + "&hddNumSecRecuperacion=" + objECreditoRecuperacion.NumSecRecuperacion);
    }

    fn_doResize();
}

//****************************************************************
// Funcion		:: 	fn_anularPagoConcepto
// Descripción	::	Anular Pago de Cuotas
// Log			:: 	IBK - RPR - 24/01/2013
//****************************************************************
function fn_anular(strMotivoAnulacion) {

    var strTipoRecuperacion = "C";
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
								 "pstrMotivoAnulacionExtorno", strMotivoAnulacion];

    parent.fn_blockUI();

    fn_util_AjaxSyncWM("frmPagoCuotasRegistro.aspx/AnularPagoCuotas",
						 arrParametros,
						 fn_retornoAnulacionPagoConcepto,
						function(resultado) {
						    parent.fn_unBlockUI();
						    var error = eval("(" + resultado.responseText + ")");
						    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al Anular Pago");
						}
			);
}

function fn_anularPagoConcepto() {

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
    parent.fn_util_AbreModal("Pago de Otros Conceptos :: Anulación", "Pagos/frmConfirmarOperacion.aspx", 600, 300, function() { });
}

function fn_extornar(strMotivoExtorno) {

    var strTipoRecuperacion = "C";
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
						 fn_retornoExtornoPagoConcepto,
						function(resultado) {
						    parent.fn_unBlockUI();
						    var error = eval("(" + resultado.responseText + ")");
						    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al Anular Pago");
						}
			);

}

function fn_extornarPagoConcepto() {

    if ($("#hddEstadoRecuperacion").val() == 'E') {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("El pago ya se encuentra Extornado.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    strOperacionModal = "Extornar";
    parent.fn_util_AbreModal("Pago de Otros Conceptos :: Extorno", "Pagos/frmConfirmarOperacion.aspx", 600, 300, function() { });

}

function fn_editarPagoConcepto() {

    if ($("#hddEstadoRecuperacion").val() != 'I' && $("#hddEstadoRecuperacion").val() != 'C') {
        parent.fn_mdl_mensajeIco("Solo es posible editar conceptos en estado Ingresado o Enviado a Host.", "util/images/error.gif", "Validación de Datos");
        return;
    }

    editandoConcepto = true;
    
    $('#dv_botonAnular').hide();
    $('#dv_botonExtornar').hide();

    $('#dv_botonEditar').hide();
    $('#dv_botonGrabar').show();
    
    fn_util_activaInput("txtFechaValor", "I");
    fn_util_SeteaObligatorio($("#txtFechaValor"), "input");
    fn_util_SeteaCalendario($("#txtFechaValor"));
    $('#txtFechaValor').datepicker().datepicker('enable');
    
    fn_util_activaInput("txtMontoComision", "I");
    fn_util_SeteaObligatorio($("#txtMontoComision"), "input");

    fn_util_activaInput("txtMontoIGV", "I");
    fn_util_SeteaObligatorio($("#txtMontoIGV"), "input");
    
    fn_util_activaInput("txtMontoReembolso", "I");
    fn_util_SeteaObligatorio($("#txtMontoReembolso"), "input");

    fn_util_activaInput("txtMontoIGVReembolso", "I");
    fn_util_SeteaObligatorio($("#txtMontoIGVReembolso"), "input");

    $('#cmbCodMonedaCargo').removeAttr('disabled');
    fn_util_SeteaObligatorio($("#cmbCodMonedaCargo"), "input");
    $('#cmbTipoCuenta').removeAttr('disabled');
    fn_util_SeteaObligatorio($("#cmbTipoCuenta"), "input");
    $('#cmbNroCuenta').removeAttr('disabled');
    fn_util_SeteaObligatorio($("#cmbNroCuenta"), "input");

    fn_util_activaInput("rdbViaVentanilla", "S");
    fn_util_activaInput("rdbViaCuenta", "S");
    fn_util_activaInput("rdbViaAdministrativo", "S");
    
    fn_util_activaInput("rdbCuentaPropia", "S");
    fn_util_activaInput("rdbOtraCuenta", "S");

    fn_util_activaInput("txtCUClienteCargo", "I");
    fn_util_SeteaObligatorio($("#txtCUClienteCargo"), "input");

    fn_util_activaInput("txtNombreClienteCargo", "I");
    fn_util_SeteaObligatorio($("#txtNombreClienteCargo"), "input");

    $('#imgBsqClienteRM').show();

    $('#txtReajusteComision').val("0.00");
    $('#txtReajusteIGV').val("0.00");
    $('#txtReajusteReembolso').val("0.00");
    $('#txtReajusteIGVReembolso').val("0.00");
}

//****************************************************************
// Funcion		:: 	fn_retornoAnulacionPagoConcepto
// Descripción	::	Recibe respuesta de la anulacion de Pago de Concepto
// Log			:: 	IBK - RPR - 25/01/2013
//****************************************************************
function fn_retornoAnulacionPagoConcepto(response) {

    var objECreditoRecuperacion = response;

    if (objECreditoRecuperacion.CodError != 0) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objECreditoRecuperacion.MsgError, "util/images/error.gif", "Validación de Datos");
    } else {

        parent.fn_unBlockUI();

        //Log
        parent.fn_util_MuestraLogPage("Anulación correcta. Se muestran los datos actualizados.", "I");

        fn_util_globalRedirect("/Pagos/frmPagoConceptoRegistro.aspx?hddCodSolicitudCredito=" + objECreditoRecuperacion.CodOperacionActiva + "&hddNumSecRecuperacion=" + objECreditoRecuperacion.NumSecRecuperacion);
    }

    fn_doResize();
}

function fn_retornoExtornoPagoConcepto(response) {

    var objECreditoRecuperacion = response;

    if (objECreditoRecuperacion.CodError != 0) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objECreditoRecuperacion.MsgError, "util/images/error.gif", "Validación de Datos");
    } else {

        parent.fn_unBlockUI();

        //Log
        parent.fn_util_MuestraLogPage("Extorno correcto. Se muestran los datos actualizados.", "I");

        fn_util_globalRedirect("/Pagos/frmPagoConceptoRegistro.aspx?hddCodSolicitudCredito=" + objECreditoRecuperacion.CodOperacionActiva + "&hddNumSecRecuperacion=" + objECreditoRecuperacion.NumSecRecuperacion);
    }

    fn_doResize();
}
//********************************************************************************
//********************************************************************************
//*************** FUNCIONES REPLICADAS DE PAGO CUOTAS ****************************
//***************** ACTUALIZAR EN AMBOS FORMULARIOS ******************************
//********************************************************************************
//********************************************************************************

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
    $('#txtEstadoContrato').val("");

    //Valores
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
        $('#txtEstadoContrato').val(objEContrato.Estadosolicitudcredito);

        $('#hddCodMonedaContrato').val(objEContrato.Codmoneda);

        if ($('#hddTipoViaCobranza').val() == 'V') {
            $('#cmbCodMoneda').val(objEContrato.Codmoneda);
        }

        $('#txtNroContrato').val(objEContrato.Codsolicitudcredito);
        parent.fn_unBlockUI();

        //Log
        parent.fn_util_MuestraLogPage("El sistema ubicó el N° Contrato: " + objEContrato.Codsolicitudcredito + ". Se muestran los datos.", "I");
    }

    fn_doResize();
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
        if ($('#hddTipoTransaccion').val() == "NUEVO") {
            $('#cmbCodMoneda').removeAttr('disabled');
        }
    }
    else if (pstrTipoViaCobranza == "V") {
        $('input[id=rdbViaVentanilla]').attr('checked', true);
        fn_setCobranzaCuenta(false);
        //$('#cmbCodMoneda').val($('#hddCodMonedaContrato').val());
        //$('#cmbCodMoneda').attr('disabled', 'disabled');
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