var DestinoCredito_Inmueble = ["002"];
var DestinoCredito_Maquinaria = ["003", "004", "005"];
var DestinoCredito_Otros = ["007", "008"];
var DestinoCredito_Vehiculo = ["006"];
var Municipalidad = '';

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	AEP - 24/09/2012
//****************************************************************
$(document).ready(function() {
  
    Municipalidad = $("#hidCodMunicipalidadInmueble").val();
    $("div#divTabs").tabs({
        show: function() {
            fn_doResize();
        }
    });

    $("div#divTabsV").tabs({
        show: function() {
            fn_doResize();
        }
    });

    $("div#divTabsS").tabs({
        show: function() {
            fn_doResize();
        }
    });
    $("div#divTabsM").tabs({
        show: function() {
            fn_doResize();
        }
    });

    $("#txtClaseVehiculo").css('display', 'none');
    $("#ddlClaseVehivulo").change(function() {
        var strValor = $(this).val();
        if (fn_util_trim(strValor) == '006') {
            $("#txtClaseVehiculo").css('display', 'block');
        } else {
            $("#txtClaseVehiculo").css('display', 'none');
        }
    });


    if ($("#cbInafectacion").attr('checked')) {
        $('input[name=cbPagoImpuestos]').attr('checked', true);
        $('input[name=cbPagoImpuestos]').attr('disabled', 'disabled');
    }


    if ($("#cbInafectacion").attr('checked')) {
        $('input[name=cbPagoImpuestos]').attr('checked', true);
        $('input[name=cbPagoImpuestos]').attr('disabled', 'disabled');
        $('#hddPagoInafectacion').val('1');
        $('#hddPagoImpuesto').val('1');
    }

    if ($("#cbPagoImpuestos").attr('checked')) {
        $('#hddPagoImpuesto').val('1');
    }


    $("#cbPagoImpuestos").change(function() {
        if ($(this).attr('checked')) {
            $('#hddPagoImpuesto').val('1');
        } else {
            $('#hddPagoImpuesto').val('0');
        }
    });

    $("#cbInafectacion").change(function() {
        if ($(this).attr('checked')) {

            $('#cbPagoImpuestos').attr('checked', true);
            $('#cbPagoImpuestos').attr('disabled', 'disabled');
            $('#btn_botones_inafectacion').css('display', 'block');
            //$('#dv_grilla_inafectacion').css('display', 'block');
            $('#hddPagoInafectacion').val('1');
            $('#hddPagoImpuesto').val('1');

        } else {
            //$('input[name=cbPagoImpuestos]').attr('disabled', false);
            $('#cbPagoImpuestos').attr('disabled', false);
            $('#btn_botones_inafectacion').css('display', 'none');
            //$('#dv_grilla_inafectacion').css('display', 'none');
            $('#hddPagoInafectacion').val('0');

        }

    });

    $("#txtPlacaActualVehivulo").focusout(function() {
        if ($("#txtPlacaActualVehivulo").val() != "") {
            if ($("#ddlEstadoMunicipalVehiculo").val() == '001') {
                $('#txtFechaInscripcionMunicipalVehivulo').attr('disabled', false);
            } else {
                $('#txtFechaInscripcionMunicipalVehivulo').attr('disabled', 'disabled');
            }
            $('#txtFechaPropiedadVehivulo').attr('disabled', false);
        }
        else {
            $('#txtFechaInscripcionMunicipalVehivulo').attr('disabled', 'disabled');
            $('#txtFechaPropiedadVehivulo').attr('disabled', 'disabled');
        }
    });
    $("#txtNroMotorVehivulo").focusout(function() {
        if ($("#txtNroMotorVehivulo").val() != "") {
            if ($("#ddlEstadoMunicipalVehiculo").val() == '001') {
                $('#txtFechaInscripcionMunicipalVehivulo').attr('disabled', false);
            } else {
                $('#txtFechaInscripcionMunicipalVehivulo').attr('disabled', 'disabled');
            }
            $('#txtFechaPropiedadVehivulo').attr('disabled', false);
        } else {
            $('#txtFechaInscripcionMunicipalVehivulo').attr('disabled', 'disabled');
            $('#txtFechaPropiedadVehivulo').attr('disabled', 'disabled');
        }
    });
    $("#txtNroSerieVehivulo").focusout(function() {
        if ($("#txtNroSerieVehivulo").val() != "") {
            if ($("#ddlEstadoMunicipalVehiculo").val() == '001') {
                $('#txtFechaInscripcionMunicipalVehivulo').attr('disabled', false);
            } else {
                $('#txtFechaInscripcionMunicipalVehivulo').attr('disabled', 'disabled');
            }
            $('#txtFechaPropiedadVehivulo').attr('disabled', false);
        } else {
            $('#txtFechaInscripcionMunicipalVehivulo').attr('disabled', 'disabled');
            $('#txtFechaPropiedadVehivulo').attr('disabled', 'disabled');
        }
    });

    $("#ddlEstadoMunicipalInmueble").change(function() {
        if ($(this).val() == '001') {
            $("#txtFechaInscripcionMunicipalInmueble").attr('disabled', false);
        } else {
            $("#txtFechaInscripcionMunicipalInmueble").attr('disabled', 'disabled');
            $("#txtFechaInscripcionMunicipalInmueble").val('');
            $("#txtFechaInscripcionMunicipalInmueble").addClass('css_calendario').removeClass('css_input_error');

        }
    });

    $("#ddlEstadoInscripcionRRPPInmueble").change(function() {
        if ($(this).val() == '001') {
            $("#txtFechaInscripcionRegistralInmueble").attr('disabled', false);
        } else {
            $("#txtFechaInscripcionRegistralInmueble").attr('disabled', 'disabled');
            $("#txtFechaInscripcionRegistralInmueble").val('');
            $("#txtFechaInscripcionRegistralInmueble").addClass('css_calendario').removeClass('css_input_error');
        }
    });

    $("#ddlEstadoMunicipalVehiculo").change(function() {
        if (($(this).val() == '001') && (($("#txtPlacaActualVehivulo").val() != '') || ($("#txtNroMotorVehivulo").val() != '') || ($("#txtNroSerieVehivulo").val() != ''))) {
            $("#txtFechaInscripcionMunicipalVehivulo").attr('disabled', false);
        } else {
            $("#txtFechaInscripcionMunicipalVehivulo").attr('disabled', 'disabled');
            $("#txtFechaInscripcionMunicipalVehivulo").val('');
            $("#txtFechaInscripcionMunicipalVehivulo").addClass('css_calendario').removeClass('css_input_error');

        }
    });

    $("#ddlEstadoInscripcionRRPPVehiculo").change(function() {
        if (($(this).val() == '001') && (($("#txtPlacaActualVehivulo").val() != '') || ($("#txtNroMotorVehivulo").val() != '') || ($("#txtNroSerieVehivulo").val() != ''))) {
            $("#txtFechaInscripcionRegistralVehivulo").attr('disabled', false);
        } else {
            $("#txtFechaInscripcionRegistralVehivulo").attr('disabled', 'disabled');
            $("#txtFechaInscripcionRegistralVehivulo").val('');
            $("#txtFechaInscripcionRegistralVehivulo").addClass('css_input').removeClass('css_input_error');
        }
    });


    fn_InicializarCampos();
    fn_configurar_PanelesBienes();
    fn_cargarTipoBien();
    fn_SeteaUbigeoInmueble();
    fn_SeteaUbigeoMaquinaria();
    fn_SeteaUbigeoVehiculo();
    fn_SeteaUbigeoOtros();
    fn_CargarGrillaDetalleInscripcion();
    fn_CargarGrillaInafectacion();
    fn_CargarGrillaDocuementosIM();



    //IBK - RPH Cuando se inicia al editar si encuentra valor ingresado
    if ($("#txtValorVehivulo").val() != "" || $("#txtValorVehivulo").val() > 0) {
        //si tuviera un valor le resto respecto al total del grid    
        var sTotalupd = fn_util_ValidaDecimal($("#hidTotal").val()) - fn_util_ValidaDecimal($("#txtValorVehivulo").val());
        fn_util_ValidaDecimal($("#hidTotal").val(sTotalupd));
    }

    if ($("#txtValorBienOtros").val() != "" || $("#txtValorBienOtros").val() > 0) {
        var sTotalupd = fn_util_ValidaDecimal($("#hidTotal").val()) - fn_util_ValidaDecimal($("#txtValorBienOtros").val());
        fn_util_ValidaDecimal($("#hidTotal").val(sTotalupd));
    }

    if ($("#txtValorBienMaquinaria").val() != "" || $("#txtValorBienMaquinaria").val() > 0) {
        var sTotalupd = fn_util_ValidaDecimal($("#hidTotal").val()) - fn_util_ValidaDecimal($("#txtValorBienMaquinaria").val());
        fn_util_ValidaDecimal($("#hidTotal").val(sTotalupd));
    }

    if ($("#txtValorInmueble").val() != "" || $("#txtValorInmueble").val() > 0) {
        var sTotalupd = fn_util_ValidaDecimal($("#hidTotal").val()) - fn_util_ValidaDecimal($("#txtValorInmueble").val());
        fn_util_ValidaDecimal($("#hidTotal").val(sTotalupd));
    }

    //Fin


    // al final
    fn_onLoadPage();


});


//****************************************************************
// Funcion		:: 	fn_SeteaUbigeoInmueble
// Descripción	::	Setear Ubigeo del tipo de bien "Inmueble"
// Log			:: 	AEP - 28/09/2012
//****************************************************************
function fn_SeteaUbigeoInmueble() {
    
    //Carga Departamento
    var strDepartamento = fn_util_trim($("#hidCodDepartamentoInmueble").val());
    $("#ddlDepartamentoInmueble").val(strDepartamento);
    //Inicio IBK
    $("#ddlMunicipalidadInmueble").val(fn_util_trim(Municipalidad));
    //Fin IBK
    //Carga Provincia
    fn_cargaComboProvinciaInmueble(strDepartamento);
    strProvincia = fn_util_trim($("#hidCodProvinciaInmueble").val());
    $("#ddlProvinciaInmueble").val(strProvincia);

    //Carga Distrito
    fn_cargaComboDistritoInmueble(strDepartamento, strProvincia);
    $("#ddlDistritoInmueble").val(fn_util_trim($("#hidCodDistritoInmueble").val()));
}

//****************************************************************
// Funcion		:: 	fn_SeteaUbigeoInmueble
// Descripción	::	Setear Ubigeo del tipo de bien "Inmueble"
// Log			:: 	AEP - 28/09/2012
//****************************************************************
function fn_SeteaUbigeoMaquinaria() {
    //Carga Departamento
    var strDepartamentoM = fn_util_trim($("#hidDepartamentoMaquinaria").val());
    $("#ddlDepartamentoMaquinaria").val(strDepartamentoM);

    //Carga Provincia
    fn_cargaComboProvinciaMaquinaria(strDepartamentoM);
    var strProvinciaM = fn_util_trim($("#hidProvinciaMaquinaria").val());
    $("#ddlProvinciaMaquinaria").val(strProvinciaM);

    //Carga Distrito
    fn_cargaComboDistritoMaquinaria(strDepartamentoM, strProvinciaM);
    $("#ddlDistritoMaquinaria").val(fn_util_trim($("#hidDistritoMaquinaria").val()));
}

//****************************************************************
// Funcion		:: 	fn_SeteaUbigeoVehiculo
// Descripción	::	Setear Ubigeo del tipo de bien "Vehiculo"
// Log			:: 	AEP - 15/10/2012
//****************************************************************
function fn_SeteaUbigeoVehiculo() {
    //Carga Departamento
    var strDepartamentoV = fn_util_trim($("#hidDepartamentoVehiculo").val());
    $("#ddlDepartamentoVehiculo").val(strDepartamentoV);

    //Carga Provincia
    fn_cargaComboProvinciaVehiculo(strDepartamentoV);
    var strProvinciaV = fn_util_trim($("#hidProvinciaVehiculo").val());
    $("#ddlProvinciaVehiculo").val(strProvinciaV);

    //Carga Distrito
    fn_cargaComboDistritoVehiculo(strDepartamentoV, strProvinciaV);
    $("#ddlDistritoVehiculo").val(fn_util_trim($("#hidDistritoVehiculo").val()));
}
//****************************************************************
// Funcion		:: 	fn_SeteaUbigeoVehiculo
// Descripción	::	Setear Ubigeo del tipo de bien "Vehiculo"
// Log			:: 	AEP - 15/10/2012
//****************************************************************
function fn_SeteaUbigeoOtros() {
    //Carga Departamento
    var strDepartamentoO = fn_util_trim($("#hidCodDepartamentoOtros").val());
    $("#ddlDepartamentoOtros").val(strDepartamentoO);

    //Carga Provincia
    fn_cargaComboProvinciaOtros(strDepartamentoO);
    var strProvinciaO = fn_util_trim($("#hidCodProvinciaOtros").val());
    $("#ddlProvinciaOtros").val(strProvinciaO);

    //Carga Distrito
    fn_cargaComboDistritoOtros(strDepartamentoO, strProvinciaO);
    $("#ddlDistritoOtros").val(fn_util_trim($("#hidCodDistritoOtros").val()));
}

//************************************************************
// Función		:: 	fn_cargarTipoBien
// Descripcion 	:: 	Carga el combo de tipo de bien
// Log			:: 	AEP - 28/09/2012
//************************************************************
function fn_cargarTipoBien() {
    var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL104", "pstrCodigoGenerico", $("#hidCodClasificacion").val()];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            // Inmueble
            if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) {
                $('#ddlTipoBien').html(arrResultado[1]);
                $('#ddlTipoBien').val($('#hidCodTipoBien').val());
            }
            // Maquinaria
            else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) {
                $('#ddlTipoBienMaquinaria').html(arrResultado[1]);
                $('#ddlTipoBienMaquinaria').val($('#hidTipoBienMaquinaria').val());
            }
            // Vehivulo
            else if (DestinoCredito_Vehiculo.indexOf($("#hidCodClasificacion").val()) != -1) {
                $('#ddlTipoBienVehiculo').html(arrResultado[1]);
                $('#ddlTipoBienVehiculo').val($('#hidTipoBienVehiculo').val());
            }
            // Otros
            else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) {
                $('#ddlTipoBienOtros').html(arrResultado[1]);
                $('#ddlTipoBienOtros').val($('#hidTipoBienOtros').val());
            }


        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
}

function fn_SetearCamposObligatorios() {

    fn_util_SeteaObligatorio($("#ddlTipoBien"), "select");
    fn_util_SeteaObligatorio($("#ddlEstadoBien"), "select");
    fn_util_SeteaObligatorio($("#ddlDepartamentoInmueble"), "select");
    fn_util_SeteaObligatorio($("#ddlProvinciaInmueble"), "select");
    fn_util_SeteaObligatorio($("#txtCantidadInmueble"), "text");
    fn_util_SeteaObligatorio($("#txtDescripcionInmueble"), "text");
    fn_util_SeteaObligatorio($("#txtUbicacionInmueble"), "text");
    fn_util_SeteaObligatorio($("#txtUsoInmueble"), "text");
    fn_util_SeteaObligatorio($("#ddlMonedaBien"), "select");

    fn_util_SeteaObligatorio($("#ddlTipoBienVehiculo"), "select");
    fn_util_SeteaObligatorio($("#ddlEstadoBienVehiculo"), "select");
    fn_util_SeteaObligatorio($("#ddlDepartamentoVehiculo"), "select");
    fn_util_SeteaObligatorio($("#ddlProvinciaVehiculo"), "select");
    fn_util_SeteaObligatorio($("#txtCantidadVehivulo"), "text");
    fn_util_SeteaObligatorio($("#txtMarcaVehivulo"), "text");
    fn_util_SeteaObligatorio($("#txtDescripcionVehivulo"), "text");
    fn_util_SeteaObligatorio($("#txtUsoVehiculo"), "text");
    fn_util_SeteaObligatorio($("#txtDireccionVehiculo"), "text");
    fn_util_SeteaObligatorio($("#ddlMonedaVehiculo"), "select");

    fn_util_SeteaObligatorio($("#ddlBienOtros"), "select");
    fn_util_SeteaObligatorio($("#txtCantidadOtros"), "text");
    fn_util_SeteaObligatorio($("#txtDescripcionOtros"), "text");
    fn_util_SeteaObligatorio($("#ddlTipoBienOtros"), "select");
    fn_util_SeteaObligatorio($("#txtUbicacionOtros"), "text");
    fn_util_SeteaObligatorio($("#txtUsoOtros"), "text");
    fn_util_SeteaObligatorio($("#ddlDepartamentoOtros"), "select");
    fn_util_SeteaObligatorio($("#ddlProvinciaOtros"), "select");
    fn_util_SeteaObligatorio($("#ddlMonedaOtros"), "select");
    fn_util_SeteaObligatorio($("#txtMarcaOtros"), "text");


    fn_util_SeteaObligatorio($("#ddlDepartamentoMaquinaria"), "select");
    fn_util_SeteaObligatorio($("#ddlProvinciaMaquinaria"), "select");
    fn_util_SeteaObligatorio($("#ddlEstadoBienMaquinaria"), "select");
    fn_util_SeteaObligatorio($("#ddlMonedaMaquinaria"), "select");
    fn_util_SeteaObligatorio($("#txtDireccionMaquinaria"), "text");
    fn_util_SeteaObligatorio($("#txtDireccionMaquinaria"), "text");
    fn_util_SeteaObligatorio($("#txtUsoMaquinaria"), "text");
    fn_util_SeteaObligatorio($("#txtDescripcionMaquinaria"), "text");
    fn_util_SeteaObligatorio($("#txtCantidadMaquinaria"), "text");
    fn_util_SeteaObligatorio($("#ddlTipoBienMaquinaria"), "select");
    //fn_util_SeteaObligatorio($("#txtSerieMaquinaria"), "text");
    fn_util_SeteaObligatorio($("#txtMarcaMaquinaria"), "text");
    //fn_util_SeteaObligatorio($("#txtModeloMaquinaria"), "text");

}

function fn_InicializarCampos() {
    //$("#txtFechaInscripcionMunicipalVehivulo").attr('disabled', 'disabled');
    if (($("#txtNroSerieVehivulo").val() != "") || ($("#txtNroMotorVehivulo").val() != "") || ($("#txtPlacaActualVehivulo").val() != "")) {
        $("#txtFechaInscripcionMunicipalVehivulo").attr('disabled', false);
        $("#txtFechaPropiedadVehivulo").attr('disabled', false);
    } else {
        $("#txtFechaInscripcionMunicipalVehivulo").attr('disabled', 'disabled');
        $("#txtFechaPropiedadVehivulo").attr('disabled', 'disabled');
    }

    if ($("#ddlEstadoInscripcionRRPPInmueble").val() != '001') {
        $("#txtFechaInscripcionRegistralInmueble").attr('disabled', 'disabled');
        $("#txtFechaInscripcionRegistralInmueble").val('');
    }
    if ($("#ddlEstadoMunicipalInmueble").val() != '001') {
        $("#txtFechaInscripcionMunicipalInmueble").attr('disabled', 'disabled');
        $("#txtFechaInscripcionMunicipalInmueble").val('');
    }

    if ($("#ddlEstadoInscripcionRRPPVehiculo").val() != '001') {
        $("#txtFechaInscripcionRegistralVehivulo").attr('disabled', 'disabled');
        $("#txtFechaInscripcionRegistralVehivulo").val('');
    }
    if ($("#ddlEstadoMunicipalVehiculo").val() != '001') {
        $("#txtFechaInscripcionMunicipalVehivulo").attr('disabled', 'disabled');
        $("#txtFechaInscripcionMunicipalVehivulo").val('');
    }

    fn_util_SeteaCalendario($("#txtFechaTransferenciaInmueble"));
    fn_util_SeteaCalendario($("#txtFechaAdquisicionInmueble"));
    fn_util_SeteaCalendario($("#txtFechaBajaInmueble"));
    fn_util_SeteaCalendario($("#txtFechaBajaMaquinaria"));
    fn_util_SeteaCalendario($("#txtFechaProbableObra"));
    fn_util_SeteaCalendario($("#txtFechaRealObra"));
    fn_util_SeteaCalendario($("#txtFechaPropiedad"));
    fn_util_SeteaCalendario($("#txtFechaEnvioNotaria"));
    fn_util_SeteaCalendario($("#txtFechaTransferenciaMaquinaria"));
    fn_util_SeteaCalendario($("#txtFechaAdquisicionMaquinaria"));
    fn_util_SeteaCalendario($("#txtFechaTransferenciaVehivulo"));
    fn_util_SeteaCalendario($("#txtFechaAdquisionVehiculo"));
    fn_util_SeteaCalendario($("#txtFechaEnvioSATVehivulo"));
    fn_util_SeteaCalendario($("#txtFechaInscripcionMunicipalVehivulo"));
    fn_util_SeteaCalendario($("#txtFechaInscripcionMunicipalInmueble"));
    fn_util_SeteaCalendario($("#txtFechaEnvioNotariaVehivulo"));
    fn_util_SeteaCalendario($("#txtFechaEnvioRRPPVehivulo"));
    fn_util_SeteaCalendario($("#txtFechaPropiedadVehivulo"));
    fn_util_SeteaCalendario($("#txtFechaInscripcionRegistralInmueble"));
    fn_util_SeteaCalendario($("#txtFechaInscripcionRegistralVehivulo"));
    fn_util_SeteaCalendario($("#txtFechaEmisionTarjetaVehiculo"));
    fn_util_SeteaCalendario($("#txtFechaTransferenciaOtros"));
    fn_util_SeteaCalendario($("#txtFechaAdquisicionOtros"));
    fn_util_SeteaCalendario($("#txtFechaBajaOtros"));
    fn_util_SeteaCalendario($("#txtFechaBajaVehiculo"));

    var strEstadoBien = fn_util_trim($("#hidEstadoBien").val());
    $("#ddlEstadoBien").val(strEstadoBien);
    var strTipoMoneda = fn_util_trim($("#hidMonedaBien").val());
    $("#ddlMonedaBien").val(strTipoMoneda);
    var strEstadoBienMaquinaria = fn_util_trim($("#hidEstadoBienMaquinaria").val());
    $("#ddlEstadoBienMaquinaria").val(strEstadoBienMaquinaria);
    var strEstadoBienVehiculo = fn_util_trim($("#hidEstadoBienVehiculo").val());
    $("#ddlEstadoBienVehiculo").val(strEstadoBienVehiculo);
    var strTipoMonedaVehiculo = fn_util_trim($("#hidMonedaVehiculo").val());
    $("#ddlMonedaVehiculo").val(strTipoMonedaVehiculo);
    var strTipoMonedaMaquinaria = fn_util_trim($("#hidMonedaMaquinaria").val());
    $("#ddlMonedaMaquinaria").val(strTipoMonedaMaquinaria);

    $('#ddlEstaRegistroBienInmueble').attr('disabled', 'disabled');
    $('#ddlEstadoRegistroBienVehiculo').attr('disabled', 'disabled');
    $('#ddlEstadoRegistroBienMaquinaria').attr('disabled', 'disabled');
    $('#ddlEstadoRegistroBienOtros').attr('disabled', 'disabled');

    //////////////////////////////////////////////////////////////////////////////
    ///VALIDACIONES DE TAMAÑO Y TIPO DE DATO

    //INMUEBLES
    fn_SetearCamposObligatorios();

    $('#txtUbicacionInmueble').validText({ type: 'comment', length: 100 });
    $('#txtUbicacionInmueble').maxLength(100);
    $('#txtDescripcionInmueble').validText({ type: 'comment', length: 100 });
    $('#txtDescripcionInmueble').maxLength(100);
    $('#txtCodigoPredio').validText({ type: 'comment', length: 20 });
    $('#txtCodigoPredio').maxLength(20);
    $('#txtOficinaRegistralInmueble').validText({ type: 'comment', length: 50 });
    $('#txtOficinaRegistralInmueble').maxLength(50);
    $('#ddlEstadoBien').validText({ type: 'comment' });
    $('#txtCantidadInmueble').validText({ type: 'number', length: 3 });
    $('#ddlMonedaBien').validText({ type: 'comment' });
    $('#txtValorInmueble').validNumber({ value: '', decimals: 2, length: 15 });
    $('#txtFechaTransferenciaInmueble').validText({ type: 'date', length: 10 });
    $('#txtFechaAdquisicionInmueble').validText({ type: 'date', length: 10 });
    $('#txtFechaBajaInmueble').validText({ type: 'date', length: 10 });
    $('#txtObservacionesInmueble').validText({ type: 'comment', length: 300 });
    $('#txtObservacionesInmueble').maxLength(100);

    //VEHÍCULOS	

    $('#txtCantidadVehivulo').validText({ type: 'number', length: 3 });
    $('#txtDescripcionVehivulo').validText({ type: 'comment', length: 100 });
    $('#txtDescripcionVehivulo').maxLength(100);
    $('#txtDireccionVehiculo').validText({ type: 'comment', length: 100 });
    $('#txtDireccionVehiculo').maxLength(100);
    $('#txtUsoVehiculo').validText({ type: 'comment', length: 100 });
    $('#txtUsoVehiculo').maxLength(100);
    $('#txtRazonSocial').validText({ type: 'comment', length: 100 });
    $('#txtRazonSocial').maxLength(100);
    $('#txtValorVehivulo').validNumber({ value: '', decimals: 2, length: 15 });
    $('#txtCilindrosVehivulo').validNumber({ value: '', decimals: 2, length: 5 });
    $('#txtCilindrajeVehivulo').validNumber({ value: '', decimals: 2, length: 8 });
    $('#txtLongitudVehivulo').validNumber({ value: '', decimals: 2, length: 8 });
    $('#txtPesoNetoVehivulo').validText({ type: 'comment', length: 10 });
    $('#txtPesoNetoVehivulo').maxLength(10);
    $('#txtCombustibleVehiculo').validText({ type: 'comment', length: 8 });
    $('#txtCombustibleVehiculo').maxLength(8);
    $('#txtPesoBrutoVehivulo').validText({ type: 'comment', length: 10 });
    $('#txtPesoBrutoVehivulo').maxLength(10);
    $('#txtCargaUtilVehivulo').validText({ type: 'comment', length: 10 });
    $('#txtCargaUtilVehivulo').maxLength(10);
    $('#txtAnchoVehivulo').validText({ type: 'comment', length: 20 });
    $('#txtAnchoVehivulo').maxLength(20);
    $('#txtAltoVehivulo').validText({ type: 'comment', length: 20 });
    $('#txtAltoVehivulo').maxLength(20);
    $('#txtClaseVehiculo').validText({ type: 'comment', length: 20 });
    $('#txtClaseVehiculo').maxLength(20);
    $('#txtFormulaRodanteVehivulo').validText({ type: 'comment', length: 3 });
    $('#txtFormulaRodanteVehivulo').maxLength(3);
    $('#txtAsientosVehivulo').validText({ type: 'number', length: 2 });
    $('#txtPasajerosVehivulo').validText({ type: 'number', length: 2 });
    $('#txtEjesVehivulo').validText({ type: 'number', length: 2 });
    $('#txtRuedasVehivulo').validText({ type: 'number', length: 2 });
    $('#txtPuertasVehivulo').validText({ type: 'number', length: 2 });
    $('#txtPlacaActualVehivulo').validText({ type: 'alphanumeric', length: 10 });
    $('#txtPlacaAnteriorVehivulo').validText({ type: 'alphanumeric', length: 10 });
    $('#txtFechaTransferenciaVehivulo').validText({ type: 'date', length: 10 });
    $('#txtAnioVehivulo').validText({ type: 'number', length: 4 });
    $('#txtNroSerieVehivulo').validText({ type: 'alphanumeric', length: 20 });
    $('#txtNroMotorVehivulo').validText({ type: 'alphanumeric', length: 20 });
    $('#txtMarcaVehivulo').validText({ type: 'comment', length: 20 });
    $('#txtModeloVehivulo').validText({ type: 'comment', length: 20 });
    $('#txtColorVehivulo').validText({ type: 'comment', length: 50 });
    $('#txtCarroceriaVehiculo').validText({ type: 'comment', length: 20 });
    $('#txtMedidasVehivulo').validText({ type: 'comment', length: 100 });
    $('#txtMedidasVehivulo').maxLength(100);
    $('#txtObservacionesVehivulo').validText({ type: 'comment', length: 300 });
    $('#txtObservacionesVehivulo').maxLength(300);

    // MAQUINARIAS

    $('#txtDescripcionMaquinaria').validText({ type: 'comment', length: 100 });
    $('#txtDescripcionMaquinaria').maxLength(100);
    $('#txtDireccionMaquinaria').validText({ type: 'comment', length: 100 });
    $('#txtDireccionMaquinaria').maxLength(100);
    $('#txtColorMaquinaria').validText({ type: 'comment', length: 50 });
    $('#txtColorMaquinaria').maxLength(50);
    $('#txtMarcaMaquinaria').validText({ type: 'comment', length: 20 });
    $('#txtMarcaMaquinaria').maxLength(20);
    $('#txtModeloMaquinaria').validText({ type: 'comment', length: 20 });
    $('#txtModeloMaquinaria').maxLength(20);
    $('#txtCarroceriaMaquinaria').validText({ type: 'comment', length: 20 });
    $('#txtCarroceriaMaquinaria').maxLength(20);
    $('#txtMedidasMaquinaria').validText({ type: 'comment', length: 100 });
    $('#txtMedidasMaquinaria').maxLength(100);
    $('#txtCantidadMaquinaria').validText({ type: 'number', length: 3 });
    $('#txtDescripcionMaquinaria').validText({ type: 'comment', length: 100 });
    $('#txtDescripcionMaquinaria').maxLength(100);
    $('#txtObservacionesMaquinaria').validText({ type: 'comment', length: 300 });
    $('#txtObservacionesMaquinaria').maxLength(100);
    $('#txtPlacaActualMaquinaria').validText({ type: 'alphanumeric', length: 10 });
    $('#txtPlacaAnteriorMaquinaria').validText({ type: 'alphanumeric', length: 10 });
    $('#txtAnioTransferenciaMaquinaria').validText({ type: 'number', length: 4 });
    $('#txtSerieMaquinaria').validText({ type: 'alphanumeric', length: 20 });
    $('#txtMotorMaquinaria').validText({ type: 'alphanumeric', length: 20 });
    $('#txtValorBienMaquinaria').validNumber({ value: '', decimals: 2, length: 15 });
    //SISTEMAS Y OTROS	

    $('#txtCantidadOtros').validText({ type: 'number', length: 3 });
    $('#txtDescripcionOtros').validText({ type: 'comment', length: 100 });
    $('#txtDescripcionOtros').maxLength(100);
    $('#txtUbicacionOtros').validText({ type: 'comment', length: 100 });
    $('#txtValorBienOtros').validNumber({ value: '', decimals: 2, length: 15 });
    $('#txtFechaTransferenciaOtros').validText({ type: 'date', length: 10 });
    $('#txtSerieOtros').validText({ type: 'alphanumeric', length: 20 });
    $('#txtMotorOtros').validText({ type: 'alphanumeric', length: 20 });
    $('#txtMarcaOtros').validText({ type: 'comment', length: 20 });
    $('#txtModeloOtros').validText({ type: 'comment', length: 20 });
    $('#txtColorOtros').validText({ type: 'comment', length: 50 });
    $('#txtPartidaRegistralOtros').validText({ type: 'number', length: 8 });
    $('#txtOficinaRegistralOtros').validText({ type: 'comment', length: 50 });
    $('#txtObservacionesOtros').validText({ type: 'comment', length: 300 });





    if ($("#HidCodEstado").val() == '002') {

        // cuando el bien esta en estado desactivado
        //fn_SetearCamposObligatorios();	
        // Inmuebles

        $('#txtUbicacionInmueble').attr('disabled', 'disabled');
        $('#txtUsoInmueble').attr('disabled', 'disabled');
        $('#txtDescripcionBien').attr('disabled', 'disabled');
        $('#ddlEstadoBien').attr('disabled', 'disabled');
        $('#txtCantidadInmueble').attr('disabled', 'disabled');
        $('#txtDescripcionInmueble').attr('disabled', 'disabled');
        $('#ddlDepartamentoInmueble').attr('disabled', 'disabled');
        $('#ddlProvinciaInmueble').attr('disabled', 'disabled');
        $('#ddlDistritoInmueble').attr('disabled', 'disabled');
        $('#ddlMonedaBien').attr('disabled', 'disabled');
        $('#ddlTipoBien').attr('disabled', 'disabled');
        $('#txtValorInmueble').attr('disabled', 'disabled');
        $('#txtFechaTransferenciaInmueble').attr('disabled', 'disabled');
        $('#txtFechaAdquisicionInmueble').attr('disabled', 'disabled');
        $('#txtCodigoPredio').attr('disabled', 'disabled');
        $('#txtFechaBajaInmueble').validText({ type: 'date', length: 10 });
        $('#txtObservacionesInmueble').validText({ type: 'comment', length: 500 });

        //RRPP

        $('#txtFechaProbableObra').attr('disabled', 'disabled');
        $('#txtFechaRealObra').attr('disabled', 'disabled');
        $('#txtFechaInscripcionMunicipalInmueble').attr('disabled', 'disabled');
        $('#txtFechaInscripcionRegistralInmueble').attr('disabled', 'disabled');
        $('#txtOficinaRegistralInmueble').attr('disabled', 'disabled');
        $('#ddlNotariaInmueble').attr('disabled', 'disabled');
        $('#txtFechaEnvioNotaria').attr('disabled', 'disabled');
        $('#txtFechaPropiedad').attr('disabled', 'disabled');
        $('#ddlEstadoMunicipalInmueble').attr('disabled', 'disabled');
        $('#ddlEstadoInscripcionRRPPInmueble').attr('disabled', 'disabled');
        $('#ddlPropiedadInmueble').attr('disabled', 'disabled');


        //Vehiculos

        $('#ddlDepartamentoVehiculo').attr('disabled', 'disabled');
        $('#ddlProvinciaVehiculo').attr('disabled', 'disabled');
        $('#ddlDistritoVehiculo').attr('disabled', 'disabled');
        $('#txtDireccionVehiculo').attr('disabled', 'disabled');
        $('#txtUsoVehiculo').attr('disabled', 'disabled');
        $('#txtNroSerieVehivulo').attr('disabled', 'disabled');
        $('#txtNroMotorVehivulo').attr('disabled', 'disabled');
        $('#txtAnioVehivulo').attr('disabled', 'disabled');
        $('#txtMarcaVehivulo').attr('disabled', 'disabled');
        $('#txtModeloVehivulo').attr('disabled', 'disabled');
        $('#txtCarroceriaVehiculo').attr('disabled', 'disabled');
        $('#txtDescripcionVehivulo').attr('disabled', 'disabled');
        $('#ddlEstadoBienVehiculo').attr('disabled', 'disabled');
        $('#txtCantidadVehivulo').attr('disabled', 'disabled');
        $('#txtMedidasVehivulo').attr('disabled', 'disabled');
        $('#txtPlacaActualVehivulo').attr('disabled', 'disabled');
        $('#txtPlacaAnteriorVehivulo').attr('disabled', 'disabled');
        $('#txtColorVehivulo').attr('disabled', 'disabled');
        $('#ddlClaseVehivulo').attr('disabled', 'disabled');
        $('#txtClaseVehiculo').attr('disabled', 'disabled');
        $('#ddlTipoBienVehiculo').attr('disabled', 'disabled');
        $('#ddlMonedaVehiculo').attr('disabled', 'disabled');
        $('#txtValorVehivulo').attr('disabled', 'disabled');
        $('#txtFechaTransferenciaVehivulo').attr('disabled', 'disabled');
        $('#txtFechaAdquisionVehiculo').attr('disabled', 'disabled');
        $('#ddlTransmisionVehivulo').attr('disabled', 'disabled');
        $('#ddlTraccionVehivulo').attr('disabled', 'disabled');
        $('#ddlTipoMotorVehivulo').attr('disabled', 'disabled');
        $('#txtPotenciaMotorVehivulo').attr('disabled', 'disabled');
        $('#ddlCombustibleVehivulo').attr('disabled', 'disabled');
        $('#txtLongitudVehivulo').attr('disabled', 'disabled');
        $('#txtAnchoVehivulo').attr('disabled', 'disabled');
        $('#txtAltoVehivulo').attr('disabled', 'disabled');
        $('#txtPuertasVehivulo').attr('disabled', 'disabled');
        $('#txtAsientosVehivulo').attr('disabled', 'disabled');
        $('#txtPasajerosVehivulo').attr('disabled', 'disabled');
        $('#txtRuedasVehivulo').attr('disabled', 'disabled');
        $('#txtEjesVehivulo').attr('disabled', 'disabled');
        $('#txtFormulaRodanteVehivulo').attr('disabled', 'disabled');
        $('#txtPesoBrutoVehivulo').attr('disabled', 'disabled');
        $('#txtPesoNetoVehivulo').attr('disabled', 'disabled');
        $('#txtCargaUtilVehivulo').attr('disabled', 'disabled');
        $('#txtCilindrosVehivulo').attr('disabled', 'disabled');
        $('#txtCilindrajeVehivulo').attr('disabled', 'disabled');


        //RRPP - Vehiculos

        $('#txtFechaEnvioSATVehivulo').attr('disabled', 'disabled');
        $('#txtFechaInscripcionMunicipalVehivulo').attr('disabled', 'disabled');
        $('#ddlEstadoMunicipalVehiculo').attr('disabled', 'disabled');
        $('#txtFechaEmisionTarjetaVehiculo').attr('disabled', 'disabled');
        $('#txtFechaPropiedadVehivulo').attr('disabled', 'disabled');
        $('#txtFechaEnvioNotariaVehivulo').attr('disabled', 'disabled');
        $('#txtFechaInscripcionRegistralVehivulo').attr('disabled', 'disabled');
        $('#ddlEstadoInscripcionRRPPVehiculo').attr('disabled', 'disabled');

        $('#cbInafectacion').attr('disabled', 'disabled');
        $('#cbPagoImpuestos').attr('disabled', 'disabled');
        $('#btn_botones_inafectacion').css('display', 'none');

        // Maquinaria

        $('#ddlDepartamentoMaquinaria').attr('disabled', 'disabled');
        $('#ddlProvinciaMaquinaria').attr('disabled', 'disabled');
        $('#ddlDistritoMaquinaria').attr('disabled', 'disabled');
        $('#txtDireccionMaquinaria').attr('disabled', 'disabled');
        $('#txtUsoMaquinaria').attr('disabled', 'disabled');
        $('#txtSerieMaquinaria').attr('disabled', 'disabled');
        $('#txtMotorMaquinaria').attr('disabled', 'disabled');
        $('#txtMarcaMaquinaria').attr('disabled', 'disabled');
        $('#txtModeloMaquinaria').attr('disabled', 'disabled');
        $('#txtDescripcionMaquinaria').attr('disabled', 'disabled');
        $('#ddlEstadoBienMaquinaria').attr('disabled', 'disabled');
        $('#txtCantidadMaquinaria').attr('disabled', 'disabled');
        $('#txtColorMaquinaria').attr('disabled', 'disabled');
        $('#ddlTipoBienMaquinaria').attr('disabled', 'disabled');
        $('#ddlMonedaMaquinaria').attr('disabled', 'disabled');
        $('#txtValorBienMaquinaria').attr('disabled', 'disabled');
        $('#txtAnioTransferenciaMaquinaria').attr('disabled', 'disabled');
        $('#txtCarroceriaMaquinaria').attr('disabled', 'disabled');
        $('#txtMedidasMaquinaria').attr('disabled', 'disabled');
        $('#txtPlacaActualMaquinaria').attr('disabled', 'disabled');
        $('#txtPlacaAnteriorMaquinaria').attr('disabled', 'disabled');
        $('#txtFechaAdquisicionMaquinaria').attr('disabled', 'disabled');
        $('#txtFechaTransferenciaMaquinaria').attr('disabled', 'disabled');
        $('#txtFechaAdquisicionMaquinaria').attr('disabled', 'disabled');


        //Otros

        $('#ddlDepartamentoOtros').attr('disabled', 'disabled');
        $('#ddlProvinciaOtros').attr('disabled', 'disabled');
        $('#ddlDistritoOtros').attr('disabled', 'disabled');
        $('#txtUbicacionOtros').attr('disabled', 'disabled');
        $('#txtUsoOtros').attr('disabled', 'disabled');
        $('#txtSerieOtros').attr('disabled', 'disabled');
        $('#txtMotorOtros').attr('disabled', 'disabled');
        $('#txtMarcaOtros').attr('disabled', 'disabled');
        $('#txtModeloOtros').attr('disabled', 'disabled');
        $('#txtDescripcionOtros').attr('disabled', 'disabled');
        $('#ddlBienOtros').attr('disabled', 'disabled');
        $('#txtCantidadOtros').attr('disabled', 'disabled');
        $('#txtColorOtros').attr('disabled', 'disabled');
        $('#ddlTipoBienOtros').attr('disabled', 'disabled');
        $('#ddlMonedaOtros').attr('disabled', 'disabled');
        $('#txtValorBienOtros').attr('disabled', 'disabled');
        $('#txtPartidaRegistralOtros').attr('disabled', 'disabled');
        $('#txtOficinaRegistralOtros').attr('disabled', 'disabled');
        $('#txtFechaAdquisicionOtros').attr('disabled', 'disabled');
        $('#txtFechaTransferenciaOtros').attr('disabled', 'disabled');
        $('#txtFechaAdquisicionOtros').attr('disabled', 'disabled');


    } else {
        //Inicio IBK - AAE - Cambio el flag origen
        //if ($("#hidFlagOrigen").val() == '2') {
        if (($("#hidFlagOrigen").val() == '2') || ($("#hidFlagOrigen").val() == '1')) {
        //Fin IBK - AAE
            // cuando el bien se creo desde bienes por contrato




        } else {

            // cuando el bien se creo desde el contrato

            //Inmuebles	
            fn_SetearCamposObligatorios();

            fn_util_SeteaObligatorio($("#ddlTipoBien"), "select");

            $('#txtUbicacionInmueble').attr('disabled', 'disabled');
            $('#txtUsoInmueble').attr('disabled', 'disabled');
            $('#txtDescripcionInmueble').attr('disabled', 'disabled');
            $('#ddlEstadoBien').attr('disabled', 'disabled');
            $('#txtCantidadInmueble').attr('disabled', 'disabled');
            $('#ddlDepartamentoInmueble').attr('disabled', 'disabled');
            $('#ddlProvinciaInmueble').attr('disabled', 'disabled');
            $('#ddlMonedaBien').attr('disabled', 'disabled');

            //Vehiculo

            $('#ddlEstadoBienVehiculo').attr('disabled', 'disabled');
            $('#txtCantidadVehivulo').attr('disabled', 'disabled');
            $('#txtDescripcionVehivulo').attr('disabled', 'disabled');
            $('#txtDireccionVehiculo').attr('disabled', 'disabled');
            $('#txtUsoVehiculo').attr('disabled', 'disabled');
            $('#ddlDepartamentoVehiculo').attr('disabled', 'disabled');
            $('#ddlProvinciaVehiculo').attr('disabled', 'disabled');
            $('#ddlMonedaVehiculo').attr('disabled', 'disabled');
            $('#txtNroSerieVehivulo').attr('disabled', 'disabled');
            $('#txtMarcaVehivulo').attr('disabled', 'disabled');
            $('#txtModeloVehivulo').attr('disabled', 'disabled');

            //Maquinarias

            $('#ddlDepartamentoMaquinaria').attr('disabled', 'disabled');
            $('#ddlProvinciaMaquinaria').attr('disabled', 'disabled');
            $('#txtDireccionMaquinaria').attr('disabled', 'disabled');
            $('#txtUsoMaquinaria').attr('disabled', 'disabled');
            $('#txtDescripcionMaquinaria').attr('disabled', 'disabled');
            $('#txtUsoMaquinaria').attr('disabled', 'disabled');
            $('#ddlEstadoBienMaquinaria').attr('disabled', 'disabled');
            $('#txtCantidadMaquinaria').attr('disabled', 'disabled');
            $('#ddlMonedaMaquinaria').attr('disabled', 'disabled');
            $('#txtSerieMaquinaria').attr('disabled', 'disabled');
            $('#txtMarcaMaquinaria').attr('disabled', 'disabled');
            $('#txtModeloMaquinaria').attr('disabled', 'disabled');

            //Otros	
            $('#txtCantidadOtros').attr('disabled', 'disabled');
            $('#ddlBienOtros').attr('disabled', 'disabled');
            $('#txtDescripcionOtros').attr('disabled', 'disabled');
            $('#txtUbicacionOtros').attr('disabled', 'disabled');
            $('#txtUsoOtros').attr('disabled', 'disabled');
            $('#ddlDepartamentoOtros').attr('disabled', 'disabled');
            $('#ddlProvinciaOtros').attr('disabled', 'disabled');
            $('#ddlMonedaOtros').attr('disabled', 'disabled');
            $('#txtMarcaOtros').attr('disabled', 'disabled');
            $('#txtModeloOtros').attr('disabled', 'disabled');

        }

    }



}


//************************************************************
// Función		:: 	fn_configurar_PanelesBienes
// Descripcion 	:: 	Configura las distintas ventanas de mantenimiento de los bienes
// Log			:: 	AEP - 26/09/2012
//************************************************************
function fn_configurar_PanelesBienes() {

    $("#dv_datos_otros").hide();
    $("#dv_datos_vehiculo").hide();
    $("#dv_datos_inmueble").hide();
    $("#dv_datos_maquinaria").hide();
    //IBK JJM
    //("#dv_img_botonNxt").hide();
    //"#dv_img_botonBck").hide();
    //Fin
    // Inmueble
    if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_datos_inmueble").show(); }
    // Maquinaria
    else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_datos_maquinaria").show(); }
    // Vehivulo
    else if (DestinoCredito_Vehiculo.indexOf($("#hidCodClasificacion").val()) != -1) {
        $("#dv_datos_vehiculo").show();
        //IBK JJM
        //("#dv_img_botonNxt").show();
        //"#dv_img_botonBck").show();
    }
    //Fin

    // Otros
    else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_datos_otros").show(); }
}


function fn_CargarGrillaDetalleInscripcion() {
    //****************************************************************
    // Funcion		:: 	JQUERY - Documento listo
    // Descripción	::	Contiene métodos a ejecutarse una vez cargado 
    //					el detalle de inscripcion municipal
    // Log			:: 	AEP - 24/09/2012
    //****************************************************************

    $("#jqGrid_lista_A").jqGrid({

        datatype: function() {
            fn_ListagrillaInscripcion();
        },
        jsonReader: //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            {
            root: "Items",
            page: "CurrentPage",            // Número de página actual.
            total: "PageCount",             // Número total de páginas.
            records: "RecordCount",         // Total de registros a mostrar.
            repeatitems: false,
            id: "codInscripcionMunicipalDetalle"   // Índice de la columna con la clave primaria.
        },
        colNames: ['Codigo', 'Partida Registral', 'Asiento Registral', 'Acto Registral', '', ''],
        colModel: [
                { name: 'codInscripcionMunicipalDetalle', index: 'codInscripcionMunicipalDetalle', hidden: true },
		        { name: 'PartidaRegistral', index: 'PartidaRegistral', width: 100, align: "Center", sorttype: "string" },
		        { name: 'AsientoRegistral', index: 'AsientoRegistral', width: 100, align: "Center", sortable: false },
		        { name: 'Acto', index: 'Acto', width: 100, align: "Center", sorttype: "string" },
    	        { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
    	        { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true }
	    ],
        //width: 100%,
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,                             // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'codInscripcionMunicipalDetalle', // Columna a ordenar por defecto.
        sortorder: 'asc',                     // Criterio de ordenación por defecto.
        viewrecords: true,                      // Muestra la cantidad de registros.
        gridview: true,
        autowidth: false,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hidRowInscripcionMunicipal").val(id);
        }
    });

    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });

    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 120);
    $("#search_jqGrid_lista_A").hide();

}

function fn_CargarGrillaInafectacion() {
    //****************************************************************
    // Funcion		:: 	JQUERY - Documento listo
    // Descripción	::	Lista los datos de inafectacion
    // Log			:: 	AEP - 24/09/2012
    //****************************************************************

    $("#jqGrid_lista_B").jqGrid({

        datatype: function() {
            fn_ListagrillaInafectacion();
        },
        jsonReader: //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            {
            root: "Items",
            page: "CurrentPage",            // Número de página actual.
            total: "PageCount",             // Número total de páginas.
            records: "RecordCount",         // Total de registros a mostrar.
            repeatitems: false,
            id: "CodigoContratoDocumento"   // Índice de la columna con la clave primaria.
        },
        colNames: ['Codigo', 'Periodo', 'Fecha Envío de Carta', 'Fecha Recepción Documento', 'Fecha Presentación SAT', 'Fecha Notificación', 'Nro. Resolución', 'Estado', '', '', '', '', 'Archivo', 'Adjuntar', 'Observaciones'],
        colModel: [
                { name: 'codInmatriculacionDetalle', index: 'codInmatriculacionDetalle', hidden: true },
		        { name: 'Periodo', index: 'Periodo', width: 10, align: "left", sorttype: "string" },
    	        { name: 'FechaEnvioCarta', index: 'FechaEnvioCarta', width: 20, align: "left", sorttype: "string" },
		        { name: 'FechaRecepcionDocumentos', index: 'FechaRecepcionDocumentos', width: 30, align: "Center", sortable: false },
		        { name: 'FechaPresentacionSAT', index: 'FechaPresentacionSAT', width: 20, align: "left", sorttype: "string" },
    	        { name: 'FechaNotificacion', index: 'FechaNotificacion', width: 20, align: "left", sorttype: "string" },
		        { name: 'NroResolucion', index: 'NroResolucion', width: 20, align: "left", sorttype: "string" },
		        { name: 'EstadoResolucion', index: 'EstadoResolucion', width: 10, align: "Center", sortable: false },
    	        { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
    	        { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
    	        { name: 'Estado', index: 'Estado', hidden: true },
  	            { name: 'CodEstadoResolucion', index: 'CodEstadoResolucion', hidden: true },
  	            { name: 'adjunto', index: 'adjunto', width: 10, align: "center", sorttype: "string", formatter: VerAdjunto1 },
                { name: 'SubirArchivo', index: 'SubirArchivo', width: 10, align: "center", sortable: false, formatter: SubirArchivo5 },
                { name: 'lupa', index: 'lupa', width: 20, align: "center", sortable: false, formatter: Lupa }

	    ],
        //width: 300,
        height: '100%',
        pager: '#jqGrid_pager_B',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,                             // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CodigoContratoDocumento', // Columna a ordenar por defecto.
        sortorder: 'asc',                     // Criterio de ordenación por defecto.
        viewrecords: true,                      // Muestra la cantidad de registros.
        gridview: true,
        autowidth: false,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
            $("#hidRowInafectacion").val(id);
        }
    });

    jQuery("#jqGrid_lista_B").jqGrid('navGrid', '#jqGrid_pager_B', { edit: false, add: false, del: false });

    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_B").setGridWidth($(window).width() - 120);
    $("#search_jqGrid_lista_B").hide();

    //INICIO - JJM
    function VerAdjunto1(cellvalue, options, rowObject) {
        if (fn_util_trim(rowObject.adjunto) != "") {
            var strNombreArchivo = rowObject.adjunto.split('\\').pop();
            strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
            return "<img src='../Util/images/ico_download.gif' alt='Descargar/Mostrar Archivo' title='" + strNombreArchivo + "'Descargar/Mostrar Archivo' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.adjunto) + "');\" style='cursor:pointer;'/>";
        } else {
            return ".";
        }
    };
    function SubirArchivo5(cellvalue, options, rowObject) {

        var sScript2 = "javascript:AdjuntarArchivoDocumento(" + rowObject.codInmatriculacionDetalle + ",2);";
        return "<img src='../Util/images/ico_acc_adjuntarMini.gif' alt='" + cellvalue + "' title='Subir Archivo' width='20px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
    };

    function Lupa(cellvalue, options, rowObject) {
        if (rowObject.Flagobservacion == 0) {
            var sScript2 = "javascript:VerObservaciones(" + rowObject.codInmatriculacionDetalle + ",1);";
            return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
        } else {
            sScript2 = "javascript:VerObservaciones(" + rowObject.codInmatriculacionDetalle + ",1);";
            return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
        }
    };
}
function AdjuntarArchivoDocumento(CodInmatriculacionDetalle) {

    var sTitulo = "Mantenimiento Bien";
    var sSubTitulo = "Mant. Bien:: Inafectación";
    parent.fn_util_AbreModal(sSubTitulo, "Comun/frmSubirArchivo.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodConDoc=" + CodInmatriculacionDetalle + "&hddCodContrato=&Add=False", 550, 150, function() { });

};
function fn_abreArchivo(pstrRuta) {
    //alert(pstrRuta);
    window.open("../frmDownload.aspx?nombreArchivo=" + pstrRuta);
    return false;
}
function VerObservaciones(CodInmatriculacionDetalle, el) {
    var sTitulo = "Mantenimiento Bien";
    var sSubTitulo = "Bien Detalle:: Inafectación  ";
    parent.fn_util_AbreModal(sSubTitulo, "Comun/frmCheckListObservacion.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodConDoc=" + CodInmatriculacionDetalle + "&hddCodContrato=" + $("#txtNumeroContrato").val() + "&sflagtipoobs=" + el + "&Add=Inafectacion", 700, 300, function() { });
}
//FIN - JJM
//****************************************************************
// Funcion		:: 	fn_ListagrillaInafectacion
// Descripción	::	
// Log			:: 	AEP - 18/10/2012
//****************************************************************
function fn_ListagrillaInafectacion() {
    var arrParametros = ["pCodigoContrato", $("#txtNumeroContrato").val(),
		                "pCodigoBien", $("#hidSecFinanciamiento").val()
	];

    fn_util_AjaxSyncWM("frmMantenimientoBienDetalle.aspx/ListaInafectacion",
		arrParametros,
		function(jsondata) {
		    jqGrid_lista_B.addJSONData(jsondata);
		    parent.fn_unBlockUI();
		},
		function(request) {
		    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
		    parent.fn_unBlockUI();
		}
	);

    //Hiden para limpiar la variable de Aprobacion de 
    $("#hddFlagVerificaAdjunto").val("");
}

function fn_CargarGrillaDocuementosIM() {
    /*******************************
    CARGA GRILLA TAB  DOCUMENTOS 
    ********************************/


    $("#jqGrid_lista_C").jqGrid({
        datatype: function() {
            fn_ListagrillaDocumentos();
        },
        jsonReader: //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            {
            root: "Items",
            page: "CurrentPage",            // Número de página actual.
            total: "PageCount",             // Número total de páginas.
            records: "RecordCount",         // Total de registros a mostrar.
            repeatitems: false,
            id: "CodigoContratoDocumento"   // Índice de la columna con la clave primaria.
        },
        colNames: ['Codigo', '', '', 'Nombre Archivo', 'Adjunto', 'Comentario', '', ''],
        colModel: [
                { name: 'CODIGOBIENDOCUMENTO', index: 'CODIGOBIENDOCUMENTO', hidden: true },
    	        { name: 'NUMEROCONTRATO', index: 'NUMEROCONTRATO', hidden: true },
    	        { name: 'SECFINANCIAMIENTO', index: 'SECFINANCIAMIENTO', hidden: true },
		        { name: 'NOMBREARCHIVO', index: 'NOMBREARCHIVO', width: 200, align: "left", sorttype: "string", defaultValue: "" },
		        { name: 'ADJUNTO', index: 'ADJUNTO', width: 100, align: "Center", sortable: false, formatter: fn_icoDownload },
        //{ name: 'OBSERVACIONES', index: 'OBSERVACIONES', width: 200, align: "center", sorttype: "string" ,sortable:false,formatter:VerObservacion},
    	        {name: 'OBSERVACIONES', index: 'OBSERVACIONES', align: "left", sorttype: "string" },
    	        { name: 'ESTADODOCCONTRATO', index: 'ESTADODOCCONTRATO', hidden: true },
    	        { name: 'ESTADODOCBIEN', index: 'ESTADODOCBIEN', hidden: true }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_C',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,                             // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CODIGOBIENDOCUMENTO', // Columna a ordenar por defecto.
        sortorder: 'asc',                     // Criterio de ordenación por defecto.
        viewrecords: true,                      // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id);
            $("#hidRowDocumento").val(id);
        }
    });


    jQuery("#jqGrid_lista_C").jqGrid('navGrid', '#jqGrid_pager_C', { edit: false, add: false, del: false });

    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_C").setGridWidth($(window).width() - 120);
    $("#search_jqGrid_lista_C").hide();

    function fn_icoDownload(cellvalue, options, rowObject) {
        var strNombreArchivo = rowObject.ADJUNTO.split('\\').pop();
        strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);

        //if(fn_util_trim(rowObject.ESTADODOCBIEN) == "1") {
        if (fn_util_trim(rowObject.ADJUNTO) != "") {
            return "<img src='../Util/images/ico_download.gif' alt='" + strNombreArchivo + "' title='" + strNombreArchivo + "' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.ADJUNTO) + "');\" style='cursor:pointer;'/>";
        } else {
            return ".";
        }
        //	}else {
        //		return "";
        //	}

    };

    function VerObservacion(cellvalue, options, rowObject) {

        if (rowObject.OBSERVACIONES == "") {
            return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
        } else {
            var sScript2 = "javascript:VerObservacionesDocumento('" + rowObject.NUMEROCONTRATO + "','" + rowObject.SECFINANCIAMIENTO + "','" + rowObject.OBSERVACIONES + "','" + rowObject.NOMBREARCHIVO + "');";
            return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick=\"" + sScript2 + "\" style='cursor: pointer;cursor: hand;' />";
        }

    };



}

function fn_abreArchivo(pstrRuta) {
    window.open("../frmDownload.aspx?nombreArchivo=" + pstrRuta);
    return false;
}
//****************************************************************
// Funcion		:: 	fn_ListagrillaDocumentos
// Descripción	::	
// Log			:: 	AEP - 12/10/2012
//****************************************************************
function fn_ListagrillaDocumentos() {
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_C", "rowNum"), // Cantidad de elementos de la página
		"pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_C", "page"), // Página actual
		"pSortColumn", fn_util_getJQGridParam("jqGrid_lista_C", "sortname"), // Columna a ordenar
		"pSortOrder", fn_util_getJQGridParam("jqGrid_lista_C", "sortorder"), // Criterio de ordenación
		"pCodigoContrato", $("#txtNumeroContrato").val(),
		"pCodigoBien", $("#hidSecFinanciamiento").val()
	];

    fn_util_AjaxSyncWM("frmMantenimientoBienDetalle.aspx/ListaDocumentos",
		arrParametros,
		function(jsondata) {
		    jqGrid_lista_C.addJSONData(jsondata);
		    parent.fn_unBlockUI();
		},
		function(request) {
		    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
		    parent.fn_unBlockUI();
		}
	);

    //Hiden para limpiar la variable de Aprobacion de 
    $("#hddFlagVerificaAdjunto").val("");
}

//****************************************************************
// Funcion		:: 	fn_ListagrillaInscripcion
// Descripción	::	
// Log			:: 	AEP - 19/10/2012
//****************************************************************
function fn_ListagrillaInscripcion() {
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
		"pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"), // Página actual
		"pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
		"pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
		"pCodigoContrato", $("#txtNumeroContrato").val(),
		"pCodigoBien", $("#hidSecFinanciamiento").val()
	];

    fn_util_AjaxSyncWM("frmMantenimientoBienDetalle.aspx/ListaDocumentosInscripcion",
		arrParametros,
		function(jsondata) {
		    jqGrid_lista_A.addJSONData(jsondata);
		    parent.fn_unBlockUI();
		    $("#hidRowInscripcionMunicipal").val('');
		},
		function(request) {
		    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
		    parent.fn_unBlockUI();
		}
	);

    //Hiden para limpiar la variable de Aprobacion de 
    $("#hddFlagVerificaAdjunto").val("");
}
//****************************************************************
// Funcion		:: 	fn_AgregarDetalleInscripcionMunicipal
// Descripción	::	Abre Modal de Detalle Inscripcion Municipal
// Log			:: 	AEP - 24/09/2012
//****************************************************************
function fn_AgregarDetalleInscripcionMunicipal() {

    var strNumeroContrato = $("#txtNumeroContrato").val();
    var strCodigoBien = $("#hidSecFinanciamiento").val();

    parent.fn_util_AbreModal("", "Administracion/frmInscripcionMunicipalDetalle.aspx?NumContrato=" + strNumeroContrato + "&CodigoBien=" + strCodigoBien, 520, 180, function() { });

}

//****************************************************************
// Funcion		:: 	fn_EditarInscripcionMunicipal
// Descripción	::	Abre Modal para agregar la Inscripcion Municipal
// Log			:: 	AEP - 19/10/2012
//****************************************************************
function fn_EditarInscripcionMunicipal() {

    var strCodigoContrato = $("#hidNumeroContrato").val();
    var strCodigoBien = $("#hidSecFinanciamiento").val();
    var id = $("#hidRowInscripcionMunicipal").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");
    } else {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
        var codigoInscripcion = rowData.codInscripcionMunicipalDetalle;
        var strPartida = rowData.PartidaRegistral;
        var strAsiento = rowData.AsientoRegistral;
        var strActo = rowData.Acto;
        var estado = rowData.Estado;

        parent.fn_util_AbreModal("", "Administracion/frmInscripcionMunicipalDetalle.aspx?NumContrato=" + strCodigoContrato + "&CodigoBien=" + strCodigoBien + "&codigo=" +
	                            codigoInscripcion + "&partida=" + strPartida + "&asiento=" + strAsiento +
		                        "&acto=" + strActo + "&estado=" + estado, 650, 320, function() { });
    }
}

//****************************************************************
// Funcion		:: 	fn_AgregarInafectacion
// Descripción	::	Abre Modal para agregar la inafectación
// Log			:: 	AEP - 18/10/2012
//****************************************************************
function fn_AgregarInafectacion() {


    var strNumeroContrato = $("#txtNumeroContrato").val();
    var strCodigoBien = $("#hidSecFinanciamiento").val();
    var strAnioFabricacion = $("#txtAnioVehivulo").val();

    var rows = jQuery("#jqGrid_lista_B").jqGrid('getRowData');
    if (rows.length >= "3") {
        parent.fn_mdl_mensajeIco("Imposible registrar, el bien tiene 3 inafectaciones.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");
    } else {
        parent.fn_util_AbreModal("", "Administracion/frmMantBienInafectacion.aspx?NumContrato=" + strNumeroContrato + "&CodigoBien=" + strCodigoBien + "&AnioFabricacion=" + strAnioFabricacion, 620, 230, function() { });
    }

}
//****************************************************************
// Funcion		:: 	fn_EditarInafectacion
// Descripción	::	Abre Modal para agregar la inafectación
// Log			:: 	AEP - 18/10/2012
//****************************************************************
function fn_EditarInafectacion() {

    var strCodigoContrato = $("#hidNumeroContrato").val();
    var strCodigoBien = $("#hidSecFinanciamiento").val();
    var strAnioFabricacion = $("#txtAnioVehivulo").val();
    var id = $("#hidRowInafectacion").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");
    } else {
        var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
        var codigoInafectacion = rowData.codInmatriculacionDetalle;
        var strPeriodo = rowData.Periodo;
        var strFechaEnvioCarta = rowData.FechaEnvioCarta;
        var strFechaRecepcionDocumentos = rowData.FechaRecepcionDocumentos;
        var strFechaPresentacionSat = rowData.FechaPresentacionSAT;
        var strFechaNotificacion = rowData.FechaNotificacion;
        var nroResolucion = rowData.NroResolucion;
        var codEstadoResolucion = rowData.CodEstadoResolucion;
        var estado = rowData.Estado;

        parent.fn_util_AbreModal("", "Administracion/frmMantBienInafectacion.aspx?NumContrato=" + strCodigoContrato + "&CodigoBien=" + strCodigoBien + "&codigo=" +
	                            codigoInafectacion + "&periodo=" + strPeriodo + "&fecEnvio=" + strFechaEnvioCarta +
		                        "&fecRec=" + strFechaRecepcionDocumentos + "&fecPre=" + strFechaPresentacionSat + "&fecNot=" + strFechaNotificacion +
		                        "&nrores=" + nroResolucion + "&codestadores=" + codEstadoResolucion + "&estado=" + estado + "&AnioFabricacion=" + strAnioFabricacion, 650, 320, function() { });
    }
}

//****************************************************************
// Funcion		:: 	fn_eliminarInscripcion
// Descripción	::	Eliminar 
// Log			:: 	AEP - 19/10/2012
//****************************************************************
function fn_eliminarInscripcion() {

    //Variables

    var id = $("#hidRowInscripcionMunicipal").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder eliminar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");



    } else {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
        var strCodigoContrato = $("#hidNumeroContrato").val();
        var strCodigoBien = $("#hidSecFinanciamiento").val();
        var codigoInscripcion = rowData.codInscripcionMunicipalDetalle;

        var paramArray = ["pstrCodigoContrato", strCodigoContrato, "pstrCodigoInscripcion", codigoInscripcion, "pstrCodigoBien", strCodigoBien];
        //Confirmacion de Eliminacion
        parent.fn_mdl_confirma(
                            "¿Está seguro que desea eliminar el registro de inscripción municipal?  ", //Mensaje - Obligatorio
                            function() {
                                parent.fn_blockUI();
                                fn_util_AjaxWM("frmMantenimientoBienDetalle.aspx/EliminaInscripcion",
                                                   paramArray,
                                                   function(resultado) {
                                                       fn_ListagrillaInscripcion();
                                                       parent.fn_unBlockUI();
                                                       $("#hidRowInscripcionMunicipal").val('');
                                                   },
                                                   function(resultado) {
                                                       var error = eval("(" + resultado.responseText + ")");
                                                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA ELIMINACIÓN");
                                                   }
                                    );

                            },
                            "Util/images/question.gif",
                            function() { },
                            'ELIMINAR INSCRIPCIÓN MUNICIPAL'
            );
    }
}


//****************************************************************
// Funcion		:: 	fn_eliminarInafectacion
// Descripción	::	Eliminar 
// Log			:: 	AEP - 18/10/2012
//****************************************************************
function fn_eliminarInafectacion() {

    //Variables

    var id = $("#hidRowInafectacion").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder eliminar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");



    } else {
        var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
        var strCodigoContrato = $("#hidNumeroContrato").val();
        var strCodigoBien = $("#hidSecFinanciamiento").val();
        var codigoInafectacion = rowData.codInmatriculacionDetalle;

        var paramArray = ["pstrCodigoContrato", strCodigoContrato, "pstrCodigoInafectacion", codigoInafectacion, "pstrCodigoBien", strCodigoBien];
        //Confirmacion de Eliminacion
        parent.fn_mdl_confirma(
                            "¿Está seguro que desea eliminar la inafectación?", //Mensaje - Obligatorio
                            function() {
                                parent.fn_blockUI();
                                fn_util_AjaxWM("frmMantenimientoBienDetalle.aspx/EliminaInafectacion",
                                                   paramArray,
                                                   function(resultado) {
                                                       fn_ListagrillaInafectacion();
                                                       parent.fn_unBlockUI();
                                                       $("#hidRowInafectacion").val('');
                                                   },
                                                   function(resultado) {
                                                       var error = eval("(" + resultado.responseText + ")");
                                                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
                                                   }
                                    );

                            },
                            "Util/images/question.gif",
                            function() { },
                            'ELIMINAR INAFECTACIÓN'
            );
    }
}

//****************************************************************
// Funcion		:: 	fn_Volver
// Descripción	::	Regresa a la pagina anterior
// Log			:: 	AEP - 24/09/2012
//****************************************************************
function fn_Volver() {

    fn_mdl_confirma('¿Está seguro de volver?',
		function() {
		    parent.fn_blockUI();
		    fn_util_redirect('frmMantenimientoBienContrato.aspx?co=1&csc=' + $("#txtNumeroContrato").val());
		},
		"../util/images/question.gif",
		function() {
		},
		'Mantenimiento Bien'
	);


}


//****************************************************************
// Funcion		:: 	fn_abreNuevoDocumentoComentario 
// Descripción	::	Abre Modal nuevo documento o comentario
// Log			:: 	AEP - 17/10/2012
//****************************************************************
function fn_abreNuevoDocumentoComentario() {

    var strCodigoContrato = $("#txtNumeroContrato").val();
    var strSecFinanciamiento = $("#hidSecFinanciamiento").val();
    parent.fn_util_AbreModal("Mant. Bien :: Documentos y Comentarios", "Administracion/frmDocumentoComentarioBien.aspx?codcontrato=" + strCodigoContrato + "&scf=" + strSecFinanciamiento, 650, 320, function() { });

}

//****************************************************************
// Funcion		:: 	fn_abreEditarDocumentoComentario 
// Descripción	::	Abre Modal nuevo documento o comentario
// Log			:: 	AEP - 17/10/2012
//****************************************************************
function fn_abreEditarDocumentoComentario() {

    var strCodigoContrato = $("#hidNumeroContrato").val();
    var id = $("#hidRowDocumento").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");
    } else {
        var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id);
        var codigo = rowData.CODIGOBIENDOCUMENTO;
        var strobservacion = rowData.OBSERVACIONES;
        var strEstadoBien = rowData.ESTADODOCBIEN;
        var strnombrearchivo = rowData.NOMBREARCHIVO;
        var strSecFinanciamiento = rowData.SECFINANCIAMIENTO;

        parent.fn_util_AbreModal("Mant. Bien :: Documentos y Comentarios", "Administracion/frmDocumentoComentarioBien.aspx?codcontrato=" + strCodigoContrato + "&codigo=" + codigo + "&obs=" + strobservacion + "&nomArchivo=" + strnombrearchivo + "&det=" + strEstadoBien + "&scf=" + strSecFinanciamiento, 650, 320, function() { });
    }

}

//****************************************************************
// Funcion		:: 	fn_eliminarDocumentoComentario 
// Descripción	::	Eliminar 
// Log			:: 	AEP - 17/10/2012
//****************************************************************
function fn_eliminarDocumentoComentario() {

    //Variables

    var id = $("#hidRowDocumento").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder eliminar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");



    } else {
        var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id);
        var strCodigoContrato = $("#txtNumeroContrato").val();
        var strCodigoDocumento = rowData.CODIGOBIENDOCUMENTO;
        var strCodigoBien = rowData.SECFINANCIAMIENTO;
        var strEstadoBien = rowData.ESTADODOCBIEN;

        if (strEstadoBien == '0') {

            parent.fn_mdl_mensajeIco("No se puede eliminar por pertenecer a un contrato", "util/images/error.gif", "ADVERTENCIA");


        } else {

            var paramArray = ["pstrCodigoContrato", strCodigoContrato, "pstrCodigoDocumento", strCodigoDocumento, "pstrSecFinanciamiento", strCodigoBien];
            //Confirmacion de Eliminacion
            parent.fn_mdl_confirma(
                            "¿Está seguro que desea eliminar el Documento/Comentario?  ", //Mensaje - Obligatorio
                            function() {
                                parent.fn_blockUI();
                                fn_util_AjaxWM("frmMantenimientoBienDetalle.aspx/EliminaDocumentoComentario",
                                                   paramArray,
                                                   function(resultado) {
                                                       fn_ListagrillaDocumentos();
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
// Funcion		:: 	fn_cargaComboProvinciaInmueble
// Descripción	::	Cargar el combo provincia 
// Log			:: 	AEP - 28/09/2012
//****************************************************************
function fn_cargaComboProvinciaInmueble(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvinciaInmueble').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoInmueble();
    //fn_doResize();

}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistritoInmueble
// Descripción	::	
// Log			:: 	AEP - 28/09/2012

//****************************************************************

function fn_LimpiaComboDistritoInmueble() {
    $('#ddlDistritoInmueble').empty();
    $("#ddlDistritoInmueble").append('<option value="0">[-Seleccione-]</option>');
}

//****************************************************************
// Funcion		:: 	fn_cargaComboDistritoInmueble
// Descripción	::	
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_cargaComboDistritoInmueble(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistritoInmueble').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    //fn_doResize();
}


//****************************************************************
// Funcion		:: 	fn_cargaComboProvinciaMaquinaria
// Descripción	::	Cargar el combo provincia 
// Log			:: 	AEP - 02/10/2012
//****************************************************************
function fn_cargaComboProvinciaMaquinaria(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvinciaMaquinaria').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoMaquinaria();
    //fn_doResize();

}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistritoMaquinaria
// Descripción	::	
// Log			:: 	AEP - 02/10/2012

//****************************************************************

function fn_LimpiaComboDistritoMaquinaria() {
    $('#ddlDistritoMaquinaria').empty();
    $("#ddlDistritoMaquinaria").append('<option value="0">[-Seleccione-]</option>');
}

//****************************************************************
// Funcion		:: 	fn_cargaComboDistritoMaquinaria
// Descripción	::	
// Log			:: 	AEP - 02/10/2012
//****************************************************************
function fn_cargaComboDistritoMaquinaria(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistritoMaquinaria').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    //fn_doResize();
}


//****************************************************************
// Funcion		:: 	fn_cargaComboProvinciaVehiculo
// Descripción	::	Cargar el combo provincia 
// Log			:: 	AEP - 15/10/2012
//****************************************************************
function fn_cargaComboProvinciaVehiculo(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvinciaVehiculo').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoVehiculo();
    //fn_doResize();

}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistritoVehiculo
// Descripción	::	
// Log			:: 	AEP - 15/10/2012

//****************************************************************

function fn_LimpiaComboDistritoVehiculo() {
    $('#ddlDistritoVehiculo').empty();
    $("#ddlDistritoVehiculo").append('<option value="0">[-Seleccione-]</option>');
}

//****************************************************************
// Funcion		:: 	fn_cargaComboDistritoVehiculo
// Descripción	::	
// Log			:: 	AEP - 15/10/2012
//****************************************************************
function fn_cargaComboDistritoVehiculo(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistritoVehiculo').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    //fn_doResize();
}


//****************************************************************
// Funcion		:: 	fn_cargaComboProvinciaOtros
// Descripción	::	Cargar el combo provincia 
// Log			:: 	AEP - 17/10/2012
//****************************************************************
function fn_cargaComboProvinciaOtros(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvinciaOtros').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoOtros();
    //fn_doResize();

}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistritoOtros
// Descripción	::	
// Log			:: 	AEP - 17/10/2012

//****************************************************************

function fn_LimpiaComboDistritoOtros() {
    $('#ddlDistritoOtros').empty();
    $("#ddlDistritoOtros").append('<option value="0">[-Seleccione-]</option>');
}

//****************************************************************
// Funcion		:: 	fn_cargaComboDistritoOtros
// Descripción	::	
// Log			:: 	AEP - 15/10/2012
//****************************************************************
function fn_cargaComboDistritoOtros(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistritoOtros').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    //fn_doResize();
}
//****************************************************************
// Funcion		:: 	fn_grabar
// Descripción	::	Grabar
// Log			:: 	AEP - 09/10/2012
//****************************************************************
function fn_grabar() {



    var strError = new StringBuilderEx();
    fn_Validacion(strError);
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
        fn_SetearCamposObligatorios();

    }
    else {
        fn_mdl_confirma('¿Esta seguro de actualizar este registro?',
            function() {
                parent.fn_blockUI();
                var vMonedaContrato = $("#hidMonedaContrato").val();
                var vTipoCompra = $("#hidTipoCompra").val();
                var vTipoVenta = $("#hidTipoVenta").val();
                var strFlag = '0';
                var vTipoBien = '';
                var vFechaTransferencia = '';
                var vColor = '';
                var vObservaciones = '';
                var vCodDepartamento = '';
                var vCodProvincia = '';
                var vCodDistrito = '';
                var vValorBien = '';
                var vPartidaRegistral = '';
                var vOficinaRegistral = '';
                var vPlacaActual = '';
                var vPlacaAnterior = '';
                var vAnioFabricacion = '';
                var vNroMotor = '';
                var vTipoCarroceria = '';
                var vMedidas = '';
                var vCodMoneda = '';
                var vUbicacion = '';
                var vCantidad = '';
                var vDescripcion = '';
                var vCodEstadoBien = '';
                var vFechaAdquisicion = '';
                var vFechaBaja = '';
                var vCodPredio = '';
                var vFlagOrigen = '';
                var vCodEstado = '';
                var vTransmision = '';
                var vTraccion = '';
                var vTipoMotor = '';
                var vPotenciaMotor = '';
                var vCombustible = '';
                var vCilindros = '';
                var vLongitud = '';
                var vPasajeros = '';
                var vPesoNeto = '';
                var vCargaUtil = '';
                var vPesoBruto = '';
                var vAsientos = '';
                var vEjes = '';
                var vAncho = '';
                var vPuertas = '';
                var vAlto = '';
                var vRuedas = '';
                var vFormulaRodante = '';
                var vNroSerie = '';
                var vMarca = '';
                var vModelo = '';
                var vCarroceria = '';
                var vCodClase = '';
                var vClase = '';
                var vCilindraje = '';
                var vFechaEnvioSat = '';
                var vFechaInscripcionMunicipal = '';
                var vFechaEnvioNotaria = '';
                var vFechaPropiedad = '';
                var vFechaInscripcionRegistral = '';
                var vEstadoInscripcionMunicipal = '';
                var vEstadoInscripcionRrpp = '';

                var vFechaProbableFinObra = '';
                var vFechaRealFinObra = '';
                var vFechaEmisionTarjeta = '';
                var vCodigoNotaria = '';
                var vCodEstadoInscripcionRrpp = '';
                var vCodEstadoMunicipal = '';
                var vUso = '';
                var vCodInafectacion = '';
                var vCodImpuesto = '';

                var vMunicipalidad = ''; //IBK JJM
                // Inmueble
                if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) {
                    vCodDepartamento = $('#ddlDepartamentoInmueble').val() == undefined ? "" : $('#ddlDepartamentoInmueble').val();
                    vCodProvincia = $('#ddlProvinciaInmueble').val() == undefined ? "" : $('#ddlProvinciaInmueble').val();
                    vCodDistrito = $('#ddlDistritoInmueble').val() == undefined ? "" : $('#ddlDistritoInmueble').val();
                    vUbicacion = $('#txtUbicacionInmueble').val() == undefined ? "" : $('#txtUbicacionInmueble').val();
                    vUso = $('#txtUsoInmueble').val() == undefined ? "" : $('#txtUsoInmueble').val();
                    vCantidad = $('#txtCantidadInmueble').val() == undefined ? "" : $('#txtCantidadInmueble').val();
                    vDescripcion = $('#txtDescripcionInmueble').val() == undefined ? "" : $('#txtDescripcionInmueble').val();
                    vCodEstadoBien = $('#ddlEstadoBien').val() == undefined ? "" : $('#ddlEstadoBien').val();
                    vTipoBien = $('#ddlTipoBien').val() == undefined ? "" : $('#ddlTipoBien').val();
                    vCodMoneda = $('#ddlMonedaBien').val() == undefined ? "" : $('#ddlMonedaBien').val();
                    vValorBien = $('#txtValorInmueble').val() == undefined ? "" : $('#txtValorInmueble').val();
                    vFechaTransferencia = $('#txtFechaTransferenciaInmueble').val() == undefined ? "" : $('#txtFechaTransferenciaInmueble').val();
                    vFechaAdquisicion = $('#txtFechaAdquisicionInmueble').val() == undefined ? "" : $('#txtFechaAdquisicionInmueble').val();
                    vFechaBaja = $('#txtFechaBajaInmueble').val() == undefined ? "" : $('#txtFechaBajaInmueble').val();
                    vCodPredio = $('#txtCodigoPredio').val() == undefined ? "" : $('#txtCodigoPredio').val();
                    vFlagOrigen = $('#hidFlagOrigen').val() == undefined ? "" : $('#hidFlagOrigen').val();
                    vCodEstado = $('#HidCodEstado').val() == undefined ? "" : $('#HidCodEstado').val();
                    vObservaciones = $('#txtObservacionesInmueble').val() == undefined ? "" : $('#txtObservacionesInmueble').val();

                    vFechaProbableFinObra = $('#txtFechaProbableObra').val() == undefined ? "" : $('#txtFechaProbableObra').val();
                    vFechaRealFinObra = $('#txtFechaRealObra').val() == undefined ? "" : $('#txtFechaRealObra').val();
                    vFechaInscripcionMunicipal = $('#txtFechaInscripcionMunicipalInmueble').val() == undefined ? "" : $('#txtFechaInscripcionMunicipalInmueble').val();
                    vCodEstadoMunicipal = $('#ddlEstadoMunicipalInmueble').val() == undefined ? "" : $('#ddlEstadoMunicipalInmueble').val();
                    vCodigoNotaria = $('#ddlNotariaInmueble').val() == undefined ? "" : $('#ddlNotariaInmueble').val();
                    vFechaEnvioNotaria = $('#txtFechaEnvioNotaria').val() == undefined ? "" : $('#txtFechaEnvioNotaria').val();

                    vFechaInscripcionRegistral = $('#txtFechaInscripcionRegistralInmueble').val() == undefined ? "" : $('#txtFechaInscripcionRegistralInmueble').val();
                    vCodEstadoInscripcionRrpp = $('#ddlEstadoInscripcionRRPPInmueble').val() == undefined ? "" : $('#ddlEstadoInscripcionRRPPInmueble').val();
                    vFechaPropiedad = $('#txtFechaPropiedad').val() == undefined ? "" : $('#txtFechaPropiedad').val();
                    vOficinaRegistral = $('#txtOficinaRegistralInmueble').val() == undefined ? "" : $('#txtOficinaRegistralInmueble').val();

                    vMunicipalidad = $('#ddlMunicipalidadInmueble').val() == undefined ? "" : $('#ddlMunicipalidadInmueble').val(); // IBK JJM

                }
                // Maquinaria
                else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) {
                    vCodDepartamento = $('#ddlDepartamentoMaquinaria').val() == undefined ? "" : $('#ddlDepartamentoMaquinaria').val();
                    vCodProvincia = $('#ddlProvinciaMaquinaria').val() == undefined ? "" : $('#ddlProvinciaMaquinaria').val();
                    vCodDistrito = $('#ddlDistritoMaquinaria').val() == undefined ? "" : $('#ddlDistritoMaquinaria').val();
                    vUbicacion = $('#txtDireccionMaquinaria').val() == undefined ? "" : $('#txtDireccionMaquinaria').val();
                    vUso = $('#txtUsoMaquinaria').val() == undefined ? "" : $('#txtUsoMaquinaria').val();
                    vPlacaActual = $('#txtPlacaActualMaquinaria').val() == undefined ? "" : $('#txtPlacaActualMaquinaria').val();
                    vPlacaAnterior = $('#txtPlacaAnteriorMaquinaria').val() == undefined ? "" : $('#txtPlacaAnteriorMaquinaria').val();

                    vAnioFabricacion = $('#txtAnioTransferenciaMaquinaria').val() == undefined ? "" : $('#txtAnioTransferenciaMaquinaria').val();

                    vNroSerie = $('#txtSerieMaquinaria').val() == undefined ? "" : $('#txtSerieMaquinaria').val();
                    vNroMotor = $('#txtMotorMaquinaria').val() == undefined ? "" : $('#txtMotorMaquinaria').val();
                    vMedidas = $('#txtMedidasMaquinaria').val() == undefined ? "" : $('#txtMedidasMaquinaria').val();
                    vMarca = $('#txtMarcaMaquinaria').val() == undefined ? "" : $('#txtMarcaMaquinaria').val();
                    vFechaBaja = $('#txtFechaBajaMaquinaria').val() == undefined ? "" : $('#txtFechaBajaMaquinaria').val();
                    vModelo = $('#txtModeloMaquinaria').val() == undefined ? "" : $('#txtModeloMaquinaria').val();
                    vColor = $('#txtColorMaquinaria').val() == undefined ? "" : $('#txtColorMaquinaria').val();
                    vCarroceria = $('#txtCarroceriaMaquinaria').val() == undefined ? "" : $('#txtCarroceriaMaquinaria').val();
                    vCantidad = $('#txtCantidadMaquinaria').val() == undefined ? "" : $('#txtCantidadMaquinaria').val();
                    vDescripcion = $('#txtDescripcionMaquinaria').val() == undefined ? "" : $('#txtDescripcionMaquinaria').val();
                    vCodEstadoBien = $('#ddlEstadoBienMaquinaria').val() == undefined ? "" : $('#ddlEstadoBienMaquinaria').val();
                    vTipoBien = $('#ddlTipoBienMaquinaria').val() == undefined ? "" : $('#ddlTipoBienMaquinaria').val();
                    vCodMoneda = $('#ddlMonedaMaquinaria').val() == undefined ? "" : $('#ddlMonedaMaquinaria').val();
                    vValorBien = $('#txtValorBienMaquinaria').val() == undefined ? "" : $('#txtValorBienMaquinaria').val();
                    vFechaTransferencia = $('#txtFechaTransferenciaMaquinaria').val() == undefined ? "" : $('#txtFechaTransferenciaMaquinaria').val();
                    vFechaAdquisicion = $('#txtFechaAdquisicionMaquinaria').val() == undefined ? "" : $('#txtFechaAdquisicionMaquinaria').val();
                    vObservaciones = $('#txtObservacionesMaquinaria').val() == undefined ? "" : $('#txtObservacionesMaquinaria').val();
                }
                // Vehiculos
                else if (DestinoCredito_Vehiculo.indexOf($("#hidCodClasificacion").val()) != -1) {
                    vCodDepartamento = $('#ddlDepartamentoVehiculo').val() == undefined ? "" : $('#ddlDepartamentoVehiculo').val();
                    vCodProvincia = $('#ddlProvinciaVehiculo').val() == undefined ? "" : $('#ddlProvinciaVehiculo').val();
                    vCodDistrito = $('#ddlDistritoVehiculo').val() == undefined ? "" : $('#ddlDistritoVehiculo').val();
                    vUbicacion = $('#txtDireccionVehiculo').val() == undefined ? "" : $('#txtDireccionVehiculo').val();
                    vUso = $('#txtUsoVehiculo').val() == undefined ? "" : $('#txtUsoVehiculo').val();
                    vCantidad = $('#txtCantidadVehivulo').val() == undefined ? "" : $('#txtCantidadVehivulo').val();
                    vDescripcion = $('#txtDescripcionVehivulo').val() == undefined ? "" : $('#txtDescripcionVehivulo').val();
                    vCodEstadoBien = $('#ddlEstadoBienVehiculo').val() == undefined ? "" : $('#ddlEstadoBienVehiculo').val();
                    vTipoBien = $('#ddlTipoBienVehiculo').val() == undefined ? "" : $('#ddlTipoBienVehiculo').val();
                    vCodMoneda = $('#ddlMonedaVehiculo').val() == undefined ? "" : $('#ddlMonedaVehiculo').val();
                    vValorBien = $('#txtValorVehivulo').val() == undefined ? "" : $('#txtValorVehivulo').val();
                    vFechaTransferencia = $('#txtFechaTransferenciaVehivulo').val() == undefined ? "" : $('#txtFechaTransferenciaVehivulo').val();
                    vFechaAdquisicion = $('#txtFechaAdquisionVehiculo').val() == undefined ? "" : $('#txtFechaAdquisionVehiculo').val();
                    vObservaciones = $('#txtObservacionesVehivulo').val() == undefined ? "" : $('#txtObservacionesVehivulo').val();
                    vTransmision = $('#ddlTransmisionVehivulo').val() == undefined ? "" : $('#ddlTransmisionVehivulo').val();
                    vTraccion = $('#ddlTraccionVehivulo').val() == undefined ? "" : $('#ddlTraccionVehivulo').val();
                    vTipoMotor = $('#ddlTipoMotorVehivulo').val() == undefined ? "" : $('#ddlTipoMotorVehivulo').val();
                    vPotenciaMotor = $('#txtPotenciaMotorVehivulo').val() == undefined ? "" : $('#txtPotenciaMotorVehivulo').val();
                    vCombustible = $('#txtCombustibleVehiculo').val() == undefined ? "" : $('#txtCombustibleVehiculo').val();
                    vCilindros = $('#txtCilindrosVehivulo').val() == undefined ? "" : $('#txtCilindrosVehivulo').val();
                    vLongitud = $('#txtLongitudVehivulo').val() == undefined ? "" : $('#txtLongitudVehivulo').val();
                    vPasajeros = $('#txtPasajerosVehivulo').val() == undefined ? "" : $('#txtPasajerosVehivulo').val();
                    vPesoNeto = $('#txtPesoNetoVehivulo').val() == undefined ? "" : $('#txtPesoNetoVehivulo').val();
                    vCargaUtil = $('#txtCargaUtilVehivulo').val() == undefined ? "" : $('#txtCargaUtilVehivulo').val();
                    vPesoBruto = $('#txtPesoBrutoVehivulo').val() == undefined ? "" : $('#txtPesoBrutoVehivulo').val();
                    vAsientos = $('#txtAsientosVehivulo').val() == undefined ? "" : $('#txtAsientosVehivulo').val();
                    vEjes = $('#txtEjesVehivulo').val() == undefined ? "" : $('#txtEjesVehivulo').val();
                    vAncho = $('#txtAnchoVehivulo').val() == undefined ? "" : $('#txtAnchoVehivulo').val();
                    vPuertas = $('#txtPuertasVehivulo').val() == undefined ? "" : $('#txtPuertasVehivulo').val();
                    vAlto = $('#txtAltoVehivulo').val() == undefined ? "" : $('#txtAltoVehivulo').val();
                    vRuedas = $('#txtRuedasVehivulo').val() == undefined ? "" : $('#txtRuedasVehivulo').val();
                    vFormulaRodante = $('#txtFormulaRodanteVehivulo').val() == undefined ? "" : $('#txtFormulaRodanteVehivulo').val();
                    vColor = $('#txtColorVehivulo').val() == undefined ? "" : $('#txtColorVehivulo').val();
                    vPlacaActual = $('#txtPlacaActualVehivulo').val() == undefined ? "" : $('#txtPlacaActualVehivulo').val();
                    vPlacaAnterior = $('#txtPlacaAnteriorVehivulo').val() == undefined ? "" : $('#txtPlacaAnteriorVehivulo').val();
                    vAnioFabricacion = $('#txtAnioVehivulo').val() == undefined ? "" : $('#txtAnioVehivulo').val();
                    vNroSerie = $('#txtNroSerieVehivulo').val() == undefined ? "" : $('#txtNroSerieVehivulo').val();
                    vNroMotor = $('#txtNroMotorVehivulo').val() == undefined ? "" : $('#txtNroMotorVehivulo').val();
                    vMedidas = $('#txtMedidasVehivulo').val() == undefined ? "" : $('#txtMedidasVehivulo').val();
                    vMarca = $('#txtMarcaVehivulo').val() == undefined ? "" : $('#txtMarcaVehivulo').val();
                    vModelo = $('#txtModeloVehivulo').val() == undefined ? "" : $('#txtModeloVehivulo').val();
                    vCarroceria = $('#txtCarroceriaVehiculo').val() == undefined ? "" : $('#txtCarroceriaVehiculo').val();
                    vClase = $('#txtClaseVehiculo').val() == undefined ? "" : $('#txtClaseVehiculo').val();
                    vCodClase = $('#ddlClaseVehivulo').val() == undefined ? "" : $('#ddlClaseVehivulo').val();
                    vCilindraje = $('#txtCilindrajeVehivulo').val() == undefined ? "" : $('#txtCilindrajeVehivulo').val();
                    vFechaBaja = $('#txtFechaBajaVehiculo').val() == undefined ? "" : $('#txtFechaBajaVehiculo').val();
                    vFechaEnvioSat = $('#txtFechaEnvioSATVehivulo').val() == undefined ? "" : $('#txtFechaEnvioSATVehivulo').val();
                    vFechaInscripcionMunicipal = $('#txtFechaInscripcionMunicipalVehivulo').val() == undefined ? "" : $('#txtFechaInscripcionMunicipalVehivulo').val();
                    vFechaEnvioNotaria = $('#txtFechaEnvioNotariaVehivulo').val() == undefined ? "" : $('#txtFechaEnvioNotariaVehivulo').val();
                    vEstadoInscripcionMunicipal = $('#ddlEstadoMunicipalVehiculo').val() == undefined ? "" : $('#ddlEstadoMunicipalVehiculo').val();
                    vEstadoInscripcionRrpp = $('#ddlEstadoInscripcionRRPPVehiculo').val() == undefined ? "" : $('#ddlEstadoInscripcionRRPPVehiculo').val();
                    vFechaPropiedad = $('#txtFechaPropiedadVehivulo').val() == undefined ? "" : $('#txtFechaPropiedadVehivulo').val();
                    vFechaEmisionTarjeta = $('#txtFechaEmisionTarjetaVehiculo').val() == undefined ? "" : $('#txtFechaEmisionTarjetaVehiculo').val();
                    vFechaInscripcionRegistral = $('#txtFechaInscripcionRegistralVehivulo').val() == undefined ? "" : $('#txtFechaInscripcionRegistralVehivulo').val();
                    vCodImpuesto = $('#hddPagoImpuesto').val() == undefined ? "" : $('#hddPagoImpuesto').val();
                    vCodInafectacion = $('#hddPagoInafectacion').val() == undefined ? "" : $('#hddPagoInafectacion').val();
                }
                // Otros
                else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) {
                    vCodDistrito = $('#ddlDistritoOtros').val() == undefined ? "" : $('#ddlDistritoOtros').val();
                    vCodDepartamento = $('#ddlDepartamentoOtros').val() == undefined ? "" : $('#ddlDepartamentoOtros').val();
                    vCodProvincia = $('#ddlProvinciaOtros').val() == undefined ? "" : $('#ddlProvinciaOtros').val();
                    vNroSerie = $('#txtSerieOtros').val() == undefined ? "" : $('#txtSerieOtros').val();
                    vNroMotor = $('#txtMotorOtros').val() == undefined ? "" : $('#txtMotorOtros').val();
                    vColor = $('#txtColorOtros').val() == undefined ? "" : $('#txtColorOtros').val();
                    vUso = $('#txtUsoOtros').val() == undefined ? "" : $('#txtUsoOtros').val();
                    vUbicacion = $('#txtUbicacionOtros').val() == undefined ? "" : $('#txtUbicacionOtros').val();
                    vMarca = $('#txtMarcaOtros').val() == undefined ? "" : $('#txtMarcaOtros').val();
                    vModelo = $('#txtModeloOtros').val() == undefined ? "" : $('#txtModeloOtros').val();
                    vPartidaRegistral = $('#txtPartidaRegistralOtros').val() == undefined ? "" : $('#txtPartidaRegistralOtros').val();
                    vOficinaRegistral = $('#txtOficinaRegistralOtros').val() == undefined ? "" : $('#txtOficinaRegistralOtros').val();
                    vCantidad = $('#txtCantidadOtros').val() == undefined ? "" : $('#txtCantidadOtros').val();
                    vDescripcion = $('#txtDescripcionOtros').val() == undefined ? "" : $('#txtDescripcionOtros').val();
                    vCodEstadoBien = $('#ddlBienOtros').val() == undefined ? "" : $('#ddlBienOtros').val();
                    vTipoBien = $('#ddlTipoBienOtros').val() == undefined ? "" : $('#ddlTipoBienOtros').val();
                    vCodMoneda = $('#ddlMonedaOtros').val() == undefined ? "" : $('#ddlMonedaOtros').val();
                    vValorBien = $('#txtValorBienOtros').val() == undefined ? "" : $('#txtValorBienOtros').val();
                    vFechaBaja = $('#txtFechaBajaOtros').val() == undefined ? "" : $('#txtFechaBajaOtros').val();
                    vFechaTransferencia = $('#txtFechaTransferenciaOtros').val() == undefined ? "" : $('#txtFechaTransferenciaOtros').val();
                    vFechaAdquisicion = $('#txtFechaAdquisicionOtros').val() == undefined ? "" : $('#txtFechaAdquisicionOtros').val();
                    vObservaciones = $('#txtObservacionesOtros').val() == undefined ? "" : $('#txtObservacionesOtros').val();
                }


                var vNumeroContrato = $('#hidNumeroContrato').val() == undefined ? "" : $('#hidNumeroContrato').val();
                var vSecFinanciamiento = $('#hidSecFinanciamiento').val() == undefined ? "" : $('#hidSecFinanciamiento').val();

                if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) {
                    var arrParametros = ["pNumeroContrato", vNumeroContrato,
                                         "pSecFinanciamiento", vSecFinanciamiento,
                                         "pCodDepartamento", vCodDepartamento,
                                         "pCodProvincia", vCodProvincia,
                	 		             "pCodDistrito", vCodDistrito,
                	 		             "pUbicacion", vUbicacion,
                	 		             "pUso", vUso,
                	 		             "pCantidad", vCantidad,
                	 		             "pDescripcion", vDescripcion,
                	 		             "pCodEstadoBien", vCodEstadoBien,
                	 		             "pCodigoTipoBien", vTipoBien,
                	 		             "pCodMoneda", vCodMoneda,
                	 		             "pValorBien", fn_util_ValidaDecimal(vValorBien),
                	 		             "pFechaTransferencia", Fn_util_DateToString(vFechaTransferencia),
                	 		             "pFechaAdquisicion", Fn_util_DateToString(vFechaAdquisicion),
                	 		             "pCodigoPredio", vCodPredio,
                	 		             "pFlagOrigen", vFlagOrigen,
                	 		             "pCodEstado", vCodEstado,
                                         "pObservaciones", vObservaciones,
                	 		             "pFechaProbableFinObra", Fn_util_DateToString(vFechaProbableFinObra),
                                         "pFechaRealFinObra", Fn_util_DateToString(vFechaRealFinObra),
                                         "pFechaInscripcionMunicipal", Fn_util_DateToString(vFechaInscripcionMunicipal),
                                         "pFechaEnvioNotaria", Fn_util_DateToString(vFechaEnvioNotaria),
                                         "pFechaPropiedad", Fn_util_DateToString(vFechaPropiedad),
                	 		             "pFechaBaja", Fn_util_DateToString(vFechaBaja),
                	 		             "pFechaInscripcionRegistral", Fn_util_DateToString(vFechaInscripcionRegistral),
                	 		             "pOficinaRegistral", vOficinaRegistral,
                	 		             "pCodigoNotaria", vCodigoNotaria,
                	 		             "pCodEstadoInscripcionRrpp", vCodEstadoInscripcionRrpp,
                	 		             "pCodEstadoMunicipal", vCodEstadoMunicipal,
                	 		             "pMunicipalidad", vMunicipalidad //IBK JJM
                                         ];

                    fn_util_AjaxWM("frmMantenimientoBienDetalle.aspx/GuardarBienDetalle",
                     arrParametros,
                     fn_MensajeYRedireccionar,
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
                } else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) {
                    var arrParametros2 = ["pNumeroContrato", vNumeroContrato,
                                         "pSecFinanciamiento", vSecFinanciamiento,
                		                 "pCodDepartamento", vCodDepartamento,
                                         "pCodProvincia", vCodProvincia,
                	 		             "pCodDistrito", vCodDistrito,
                		                 "pUbicacion", vUbicacion,
                		                 "pUso", vUso,
                		                 "pPlacaActual", vPlacaActual,
                		                 "pPlacaAnterior", vPlacaAnterior,
                		                 "pAnioFabricacion", vAnioFabricacion = $('#txtAnioTransferenciaMaquinaria').val() == "" ? "0" : $('#txtAnioTransferenciaMaquinaria').val(),
                    //vAnioFabricacion,
                		                 "pNroSerie", vNroSerie,
                		                 "pNroMotor", vNroMotor,
                		                 "pMedidas", vMedidas,
                		                 "pMarca", vMarca,
                                         "pModelo", vModelo,
                		                 "pColor", vColor,
                		                 "pCarroceria", vCarroceria,
                		                 "pFechaBaja", Fn_util_DateToString(vFechaBaja),
                		                 "pCantidad", vCantidad,
                                         "pDescripcion", vDescripcion,
                                         "pCodEstadoBien", vCodEstadoBien,
                		                 "pCodigoTipoBien", vTipoBien,
                		                 "pCodMoneda", vCodMoneda,
                		                 "pValorBien", fn_util_ValidaDecimal(vValorBien),
                                         "pFechaTransferencia", Fn_util_DateToString(vFechaTransferencia),
                		                 "pFechaAdquisicion", Fn_util_DateToString(vFechaAdquisicion),
                                         "pObservaciones", vObservaciones,
                		                 "pFlagOrigen", $("#hidFlagOrigen").val(),
                                         "pCodEstado", $("#HidCodEstado").val()
                                    ];

                    fn_util_AjaxWM("frmMantenimientoBienDetalle.aspx/GuardarDetalleMaquinaria",
                     arrParametros2,
                     fn_MensajeYRedireccionar,
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
                } else if (DestinoCredito_Vehiculo.indexOf($("#hidCodClasificacion").val()) != -1) {


                    //IBK -RPH
                    //                var sFechaBaja = new Date($("#txtFechaBajaVehiculo").val());
                    //                var sFechaInsMunicipal = new Date($("#txtFechaInscripcionMunicipalVehivulo").val());

                    //					if (sFechaBaja < sFechaInsMunicipal) 
                    //					{
                    //						parent.fn_unBlockUI();
                    //						parent.fn_mdl_alert("La Fecha de Baja No Puede ser Menor a la Fecha de Inscripción. ", function() { });
                    //					}   
                    //					else
                    //					{
                    if (vFechaTransferencia == '') { vFechaTransferencia = '19000101'; }
                    else { vFechaTransferencia = Fn_util_DateToString(vFechaTransferencia); }

                    if (vFechaAdquisicion == '') { vFechaAdquisicion = '19000101'; }
                    else { vFechaAdquisicion = Fn_util_DateToString(vFechaAdquisicion); }

                    if (vFechaEnvioSat == '') { vFechaEnvioSat = '19000101'; }
                    else { vFechaEnvioSat = Fn_util_DateToString(vFechaEnvioSat); }

                    if (vFechaInscripcionMunicipal == '') { vFechaInscripcionMunicipal = '19000101'; }
                    else { vFechaInscripcionMunicipal = Fn_util_DateToString(vFechaInscripcionMunicipal); }

                    if (vFechaEnvioNotaria == '') { vFechaEnvioNotaria = '19000101'; }
                    else { vFechaEnvioNotaria = Fn_util_DateToString(vFechaEnvioNotaria); }

                    if (vFechaPropiedad == '') { vFechaPropiedad = '19000101'; }
                    else { vFechaPropiedad = Fn_util_DateToString(vFechaPropiedad); }

                    if (vFechaInscripcionRegistral == '') { vFechaInscripcionRegistral = '19000101'; }
                    else { vFechaInscripcionRegistral = Fn_util_DateToString(vFechaInscripcionRegistral); }

                    if (vFechaEmisionTarjeta == '') { vFechaEmisionTarjeta = '19000101'; }
                    else { vFechaEmisionTarjeta = Fn_util_DateToString(vFechaEmisionTarjeta); }

                    if (vFechaBaja == '') { vFechaBaja = '19000101'; }
                    else { vFechaBaja = Fn_util_DateToString(vFechaBaja); }

                    if (vPasajeros == '') { vPasajeros = 0; }
                    else { vPasajeros = vPasajeros; }

                    if (vRuedas == '') { vRuedas = 0; }
                    else { vRuedas = vRuedas; }

                    if (vPuertas == '') { vPuertas = 0; }
                    else { vPuertas = vPuertas; }

                    if (vEjes == '') { vEjes = 0; }
                    else { vEjes = vEjes; }

                    if (vAsientos == '') { vAsientos = 0; }
                    else { vAsientos = vAsientos; }


                    var arrParametros3 = ["pNumeroContrato", vNumeroContrato,
												 "pSecFinanciamiento", vSecFinanciamiento,
												 "pTipoBien", vTipoBien,
												"pFechaTransferencia", vFechaTransferencia,
												"pColor", vColor,
												"pObservaciones", vObservaciones,
												"pCodDepartamento", vCodDepartamento,
												"pCodProvincia", vCodProvincia,
												"pCodDistrito", vCodDistrito,
												"pValorBien", vValorBien,
												"pPlacaActual", vPlacaActual,
												"pPlacaAnterior", vPlacaAnterior,
												"pAnioFabricacion", vAnioFabricacion,
												"pNroMotor", vNroMotor,
												"pTipoCarroceria", vTipoCarroceria,
												"pMedidas", vMedidas,
												"pCodMoneda", vCodMoneda,
												"pUbicacion", vUbicacion,
												"pUso", vUso,
												"pCantidad", vCantidad,
												"pDescripcion", vDescripcion,
												"pCodEstadoBien", vCodEstadoBien,
												"pFechaAdquisicion", vFechaAdquisicion,
												"pFechaBaja", vFechaBaja,
												"pCodPredio", vCodPredio,
												"pFlagOrigen", $("#hidFlagOrigen").val(),
												"pCodEstado", $("#ddlEstadoRegistroBienVehiculo").val(),
												"pTransmision", vTransmision,
												"pTraccion", vTraccion,
												"pTipoMotor", vTipoMotor,
												"pPotenciaMotor", vPotenciaMotor,
												"pCombustible", vCombustible,
												"pCilindros", vCilindros,
												"pLongitud", vLongitud,
												"pPasajeros", vPasajeros,
												"pPesoNeto", vPesoNeto,
												"pCargaUtil", vCargaUtil,
												"pPesoBruto", vPesoBruto,
												"pAsientos", vAsientos,
												"pEjes", vEjes,
												"pAncho", vAncho,
												"pPuertas", vPuertas,
												"pAlto", vAlto,
												"pRuedas", vRuedas,
												"pFormulaRodante", vFormulaRodante,
												"pNroSerie", vNroSerie,
												"pMarca", vMarca,
												"pModelo", vModelo,
												"pCarroceria", vCarroceria,
												"pCodClase", vCodClase,
												"pClase", vClase,
												"pCilindraje", vCilindraje,
												"pFechaEnvioSat", vFechaEnvioSat,
												"pFechaBaja", vFechaBaja,
												"pFechaInscripcionMunicipal", vFechaInscripcionMunicipal,
												"pFechaEnvioNotaria", vFechaEnvioNotaria,
												"pEstadoInscripcionMunicipal", vEstadoInscripcionMunicipal,
												"pEstadoInscripcionRrpp", vEstadoInscripcionRrpp,
												"pFechaPropiedad", vFechaPropiedad,
												"pFechaInscripcionRegistral", vFechaInscripcionRegistral,
												"pEstadoInscripcionMunicipal", vEstadoInscripcionMunicipal,
												"pFechaEmisionTarjeta", vFechaEmisionTarjeta,
												"pEstadoInscripcionRrpp", vEstadoInscripcionRrpp,
                	                             "pCodImpuesto", vCodImpuesto,
                		                         "pCodInafectacion", vCodInafectacion
												];

                    fn_util_AjaxWM("frmMantenimientoBienDetalle.aspx/GuardarVehiculoDetalle",
							 arrParametros3,
							 fn_MensajeYRedireccionar,
							 function(resultado) {
							     var error = eval("(" + resultado.responseText + ")");
							     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
							 });
                    //					}

                } else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) {
                    var arrParametros4 = ["pNumeroContrato", vNumeroContrato,
                                         "pSecFinanciamiento", vSecFinanciamiento,
                                         "pCodDistrito", vCodDistrito,
                                        "pCodDepartamento", vCodDepartamento,
                                        "pCodProvincia", vCodProvincia,
                		                "pUso", vUso,
                                        "pNroSerie", vNroSerie,
                                        "pNroMotor", vNroMotor,
                                        "pColor", vColor,
                                        "pUbicacion", vUbicacion,
                                        "pMarca", vMarca,
                                        "pModelo", vModelo,
                                        "pPartidaRegistral", vPartidaRegistral,
                                        "pOficinaRegistral", vOficinaRegistral,
                                        "pCantidad", vCantidad,
                                        "pDescripcion", vDescripcion,
                                        "pCodEstadoBien", vCodEstadoBien,
                                        "pTipoBien", vTipoBien,
                                        "pCodMoneda", vCodMoneda,
                                        "pValorBien", vValorBien,
                		                "pFechaBaja", Fn_util_DateToString(vFechaBaja),
                                        "pFechaTransferencia", Fn_util_DateToString(vFechaTransferencia),
                                        "pFechaAdquisicion", Fn_util_DateToString(vFechaAdquisicion),
                                        "pObservaciones", vObservaciones,
                		                "pFlagOrigen", $("#hidFlagOrigen").val(),
                                        "pCodEstado", $("#HidCodEstado").val()
                                    ];

                    fn_util_AjaxWM("frmMantenimientoBienDetalle.aspx/GuardarOtrosDetalle",
                     arrParametros4,
                     fn_MensajeYRedireccionar,
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
                }

            },
            "../util/images/question.gif",
            function() {
                fn_SetearCamposObligatorios();
            },
            'Mantenimiento Bien'
         );
    }
}

//****************************************************************
// Función		:: 	fn_MensajeYRedireccionarSolicitud
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
//                  Luego lo redirecciona al formulario de búsquedas ("frmMantenimientoBienListado.aspx").
// Log			:: 	AEP - 09/10/2012
//****************************************************************
var fn_MensajeYRedireccionar = function() {
    parent.fn_unBlockUI();
    fn_SetearCamposObligatorios();
    parent.fn_mdl_alert('Los datos se grabaron satisfactoriamente', function() { });
};

//****************************************************************
// Funcion		:: 	fn_Validacion
// Descripción	::	Valida Registro
// Log			:: 	AEP - 09/10/2012
//****************************************************************
function fn_Validacion(pError) {

    //inmuebles
    var cmbTipoBien = $('select[id=ddlTipoBien]');
    var txtcantidadinmueble = $('input[id=txtCantidadInmueble]:text');
    var txtDescripcionInmueble = $('textarea[id=txtDescripcionInmueble]');
    var txtUbicacion = $('input[id=txtUbicacionInmueble]:text');
    var txtUso = $('input[id=txtUsoInmueble]:text');
    var cmbEstadoBien = $('select[id=ddlEstadoBien]');
    var cmbDepartamento = $('select[id=ddlDepartamentoInmueble]');
    var cmbProvincia = $('select[id=ddlProvinciaInmueble]');
    var cmbMoneda = $('select[id=ddlMonedaBien]');

    //vehiculos
    var cmbTipoBienVehiculo = $('select[id=ddlTipoBienVehiculo]');
    var txtcantidadVehiculo = $('input[id=txtCantidadVehivulo]:text');
    var txtDescripcionVehiculo = $('textarea[id=txtDescripcionVehivulo]');
    var txtUsoVehiculo = $('input[id=txtUsoVehiculo]:text');
    var txtUbicacionVehiculo = $('input[id=txtDireccionVehiculo]:text');
    var txtMarcaVehiculo = $('input[id=txtMarcaVehivulo]:text');
    var txtPlacaActualVehiculo = $('input[id=txtPlacaActualVehivulo]:text');
    var txtPlacaAnteriorVehiculo = $('input[id=txtPlacaAnteriorVehivulo]:text');
    var cmbEstadoBienVehiculo = $('select[id=ddlEstadoBienVehiculo]');
    var cmbDepartamentoVehiculo = $('select[id=ddlDepartamentoVehiculo]');
    var cmbProvinciaVehiculo = $('select[id=ddlProvinciaVehiculo]');
    var cmbMonedaVehiculo = $('select[id=ddlMonedaVehiculo]');
    var txtFechaBajaVehiculo = $('input[id=txtFechaBajaVehiculo]:text');

    // Maquinaria

    var cmbDepartamentoMaquinaria = $('select[id=ddlDepartamentoMaquinaria]');
    var cmbProvinciaMaquinaria = $('select[id=ddlProvinciaMaquinaria]');
    var cmbEstadoBienMaquinaria = $('select[id=ddlEstadoBienMaquinaria]');
    var cmbMonedaMaquinaria = $('select[id=ddlMonedaMaquinaria]');
    var txtDescripcionMaquinaria = $('textarea[id=txtDescripcionMaquinaria]');
    var txtDireccionMaquinaria = $('input[id=txtDireccionMaquinaria]:text');
    var txtUsoMaquinaria = $('input[id=txtUsoMaquinaria]:text');
    var txtCantidadMaquinaria = $('input[id=txtCantidadMaquinaria]:text');
    var txtSerieMaquinaria = $('input[id=txtSerieMaquinaria]:text');
    var txtMarcaMaquinaria = $('input[id=txtMarcaMaquinaria]:text');
    var txtModeloMaquinaria = $('input[id=txtModeloMaquinaria]:text');
    var cmbTipoBienMaquinaria = $('select[id=ddlTipoBienMaquinaria]');


    // Otros
    var cmbBienOtros = $('select[id=ddlBienOtros]');
    var txtcantidadOtros = $('input[id=txtCantidadOtros]:text');
    var txtDescripcionOtros = $('textarea[id=txtDescripcionOtros]');
    var txtUbicacionOtros = $('input[id=txtUbicacionOtros]:text');
    var txtValorOtros = $('input[id=txtValorBienOtros]:text');
    var txtUsoOtros = $('input[id=txtUsoOtros]:text');
    var cmbTipoBienOtros = $('select[id=ddlTipoBienOtros]');
    var cmbDeparatamentoOtros = $('select[id=ddlDepartamentoOtros]');
    var cmbProvinciaOtros = $('select[id=ddlProvinciaOtros]');
    var cmbMonedaOtros = $('select[id=ddlMonedaOtros]');
    var txtFechaOtros = $('input[id=txtFechaTransferenciaOtros]:text');
    var txtColorOtros = $('input[id=txtColorOtros]:text');
    var txtMarcaOtros = $('input[id=txtMarcaOtros]:text');


    //	 var txtOficina = $('input[id=txtOficinaRegistral]:text');
    //	 var txtPartida1 = $('input[id=txtPartidaRegistral2]:text');
    //	 var txtOficina1 = $('input[id=txtOficinaRegistral2]:text');
    
    // Inmueble
    if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) {
        pError.append(fn_util_ValidateControl(txtcantidadinmueble[0], 'una cantidad', 1, ''));
        pError.append(fn_util_ValidateControl(txtDescripcionInmueble[0], 'una descripción', 1, ''));
        pError.append(fn_util_ValidateControl(txtUbicacion[0], 'una ubicacion', 1, ''));
        pError.append(fn_util_ValidateControl(txtUso[0], 'uso', 1, ''));
        pError.append(fn_util_ValidateControl(cmbEstadoBien[0], 'un estado del bien', 1, ''));
        pError.append(fn_util_ValidateControl(cmbDepartamento[0], 'un departamento', 1, ''));
        pError.append(fn_util_ValidateControl(cmbProvincia[0], 'una provincia', 1, ''));
        pError.append(fn_util_ValidateControl(cmbTipoBien[0], 'un tipo de bien', 1, ''));
        pError.append(fn_util_ValidateControl(cmbMoneda[0], 'una moneda', 1, ''));
        if ($("#ddlMunicipalidadInmueble").val() == '' || $("#ddlMunicipalidadInmueble").val() == undefined || $("#ddlMunicipalidadInmueble").val() == '0') {
            pError.append('<br/> El campo municipalidad es obligatorio. ');
        }
        $("div#divTabs").tabs("select", [0]);
    }
    // Maquinaria
    else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) {
        pError.append(fn_util_ValidateControl(cmbDepartamentoMaquinaria[0], 'un departamento', 1, ''));
        pError.append(fn_util_ValidateControl(cmbProvinciaMaquinaria[0], 'una provincia', 1, ''));
        pError.append(fn_util_ValidateControl(cmbEstadoBienMaquinaria[0], 'un estado del bien', 1, ''));
        pError.append(fn_util_ValidateControl(cmbMonedaMaquinaria[0], 'una moneda', 1, ''));
        pError.append(fn_util_ValidateControl(cmbTipoBienMaquinaria[0], 'una Tipo de Bien', 1, ''));
        pError.append(fn_util_ValidateControl(txtUsoMaquinaria[0], 'uso', 1, ''));
        pError.append(fn_util_ValidateControl(txtDescripcionMaquinaria[0], 'una descripción', 1, ''));
        pError.append(fn_util_ValidateControl(txtDireccionMaquinaria[0], 'una ubicacion', 1, ''));
        pError.append(fn_util_ValidateControl(txtCantidadMaquinaria[0], 'una cantidad', 1, ''));
        //pError.append(fn_util_ValidateControl(txtSerieMaquinaria[0], 'una serie', 1, ''));
        pError.append(fn_util_ValidateControl(txtMarcaMaquinaria[0], 'una marca', 1, ''));
        //pError.append(fn_util_ValidateControl(txtModeloMaquinaria[0], 'un modelo', 1, ''));
        $("div#divTabsM").tabs("select", [0]);

    } else if (DestinoCredito_Vehiculo.indexOf($("#hidCodClasificacion").val()) != -1) {
        pError.append(fn_util_ValidateControl(txtcantidadVehiculo[0], 'una cantidad', 1, ''));
        pError.append(fn_util_ValidateControl(txtDescripcionVehiculo[0], 'una descripción', 1, ''));
        pError.append(fn_util_ValidateControl(txtMarcaVehiculo[0], 'una marca', 1, ''));
        pError.append(fn_util_ValidateControl(txtUbicacionVehiculo[0], 'una ubicacion', 1, ''));
        pError.append(fn_util_ValidateControl(cmbEstadoBienVehiculo[0], 'un estado del bien', 1, ''));
        pError.append(fn_util_ValidateControl(cmbDepartamentoVehiculo[0], 'un departamento', 1, ''));
        pError.append(fn_util_ValidateControl(cmbProvinciaVehiculo[0], 'una provincia', 1, ''));
        pError.append(fn_util_ValidateControl(cmbTipoBienVehiculo[0], 'un tipo de bien', 1, ''));
        pError.append(fn_util_ValidateControl(cmbMonedaVehiculo[0], 'una moneda', 1, ''));
        pError.append(fn_util_ValidateControl(txtUsoVehiculo[0], 'uso', 1, ''));
        if (($("#txtPlacaAnteriorVehivulo").val() != "") && ($("#txtPlacaActualVehivulo").val() == "")) {
            pError.append(fn_util_ValidateControl(txtPlacaActualVehiculo[0], 'placa actual', 1, ''));
        } else {
            $("#txtPlacaActualVehivulo").addClass('css_input').removeClass('css_input_error');
        }
        $("div#divTabsV").tabs("select", [0]);

        // Otros
    } else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) {
        $("div#divTabsS").tabs("select", [0]);
        pError.append(fn_util_ValidateControl(cmbBienOtros[0], 'un estado de bien', 1, ''));
        pError.append(fn_util_ValidateControl(txtcantidadOtros[0], 'una cantidad', 1, ''));
        pError.append(fn_util_ValidateControl(txtDescripcionOtros[0], 'una descripción', 1, ''));
        pError.append(fn_util_ValidateControl(txtUbicacionOtros[0], 'una ubicación', 1, ''));
        pError.append(fn_util_ValidateControl(txtMarcaOtros[0], 'una marca', 1, ''));
        pError.append(fn_util_ValidateControl(cmbDeparatamentoOtros[0], 'un departamento', 1, ''));
        pError.append(fn_util_ValidateControl(cmbTipoBienOtros[0], 'un tipo de bien', 1, ''));
        pError.append(fn_util_ValidateControl(cmbProvinciaOtros[0], 'una provincia', 1, ''));
        pError.append(fn_util_ValidateControl(cmbMonedaOtros[0], 'una moneda', 1, ''));
        pError.append(fn_util_ValidateControl(cmbMonedaOtros[0], 'una marca', 1, ''));
        pError.append(fn_util_ValidateControl(txtUsoOtros[0], 'uso', 1, ''));

    }


    var objtxtFechaRegistral = $('input[id=txtFechaInscripcionRegistralInmueble]:text');
    var objtxtFechaMunicipal = $('input[id=txtFechaInscripcionMunicipalInmueble]:text');

    var objtxtFechaRegistralV = $('input[id=txtFechaInscripcionRegistralVehivulo]:text');
    var objtxtFechaMunicipalV = $('input[id=txtFechaInscripcionMunicipalVehivulo]:text');

    if (($("#ddlEstadoInscripcionRRPPInmueble").val() == '001') && ($("#txtFechaInscripcionRegistralInmueble").val() == '')) {
        pError.append(fn_util_ValidateControl(objtxtFechaRegistral[0], 'la fecha de inscripción registral', 1, ''));
        $("div#divTabs").tabs("select", [1]);
    }
    if (($("#ddlEstadoMunicipalInmueble").val() == '001') && ($("#txtFechaInscripcionMunicipalInmueble").val() == '')) {
        pError.append(fn_util_ValidateControl(objtxtFechaMunicipal[0], 'la fecha de inscripción municipal ', 1, ''));
        $("div#divTabs").tabs("select", [1]);
    }

    if (($("#ddlEstadoInscripcionRRPPVehiculo").val() == '001') && ($("#txtFechaInscripcionRegistralVehivulo").val() == '')) {
        pError.append(fn_util_ValidateControl(objtxtFechaRegistralV[0], 'la fecha de inscripción registral', 1, ''));
        $("div#divTabsV").tabs("select", [1]);
    }
    if (($("#ddlEstadoMunicipalVehiculo").val() == '001') && ($("#txtFechaInscripcionMunicipalVehivulo").val() == '')) {
        pError.append(fn_util_ValidateControl(objtxtFechaMunicipalV[0], 'la fecha de inscripción municipal ', 1, ''));
        $("div#divTabsV").tabs("select", [1]);
    }
    //IBK Inicio JJM
    if (DestinoCredito_Vehiculo.indexOf($("#hidCodClasificacion").val()) != -1) {

        var sFechaBaja = new Date($("#txtFechaBajaVehiculo").val());
        var sFechaInsMunicipal = new Date($("#txtFechaInscripcionMunicipalVehivulo").val());
        var iTotal = 0; //Ahora

        var sFechaPropiedad = new Date($("#txtFechaPropiedadVehivulo").val());
        var sFechaTransferencia = new Date($("#txtFechaTransferenciaVehivulo").val());
        //Ahora
        iTotal = fn_ValidaPeriodo();

        //        var sPrecioVenta = $("#hidPrecioVenta").val(); //Valor Bien Leasing
        //        var sTotal = $("#hidTotal").val(); //Total Valor bien Grid
        //        var sTotalImporte = "";


        //        sTotalImporte = (fn_util_ValidaDecimal(sTotal) + fn_util_ValidaDecimal($("#txtValorVehivulo").val()));


        //if (sTotalImporte > sPrecioVenta) {
        //   pError.append('<br/> El Valor del Bien No debe ser Mayor al Total Valor Leasing.  ');
        //    $("div#divTabsV").tabs("select", [1]);
        // }

        if (sFechaBaja < sFechaInsMunicipal) {
            pError.append("<br/> La Fecha de Baja No Puede ser Menor a la Fecha de Inscripción Municipal.  ");
            $("div#divTabsV").tabs("select", [1]);

        }

        if (sFechaPropiedad < sFechaTransferencia) {
            pError.append("<br/> La Fecha de Propiedad No Puede ser Menor a la Fecha de Transferencia.  ");
            $("div#divTabsV").tabs("select", [1]);
        }

        //Ahora
        if (iTotal.toString() != '0') {
            pError.append("<br/> El período ingresado es incorrecto, el correcto debe ser mayor o igual al año de fabricación y no mayor a 3 años de este. ");
            $("div#divTabsV").tabs("select", [2]);
        }
    }

    //IBK Fin JJM

    //debugger;
    //Inicio IBK -RPH
    var sPrecioVenta = "";
    var sTotal = "";
    var sTotalImporte = "";

    if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) {
        sPrecioVenta = $("#hidPrecioVenta").val(); //Valor Bien Leasing
        sTotal = $("#hidTotal").val(); //Total Valor bien Grid

        sTotalImporte = (fn_util_ValidaDecimal(sTotal) + fn_util_ValidaDecimal($("#txtValorInmueble").val()));

        //importe
        if (sTotalImporte > sPrecioVenta) {
            pError.append('<br/> El Valor del Bien No debe exceder al Total Valor Leasing. ');
        }
    }
    else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) {
        sPrecioVenta = $("#hidPrecioVenta").val(); //Valor Bien Leasing
        sTotal = $("#hidTotal").val(); //Total Valor bien Grid

        sTotalImporte = (fn_util_ValidaDecimal(sTotal) + fn_util_ValidaDecimal($("#txtValorBienMaquinaria").val()));

        if (sTotalImporte > sPrecioVenta) {
            pError.append('<br/> El Valor del Bien No debe exceder al Total Valor Leasing. ');
        }
    }
    else if (DestinoCredito_Vehiculo.indexOf($("#hidCodClasificacion").val()) != -1) {
        sPrecioVenta = $("#hidPrecioVenta").val(); //Valor Bien Leasing
        sTotal = $("#hidTotal").val(); //Total Valor bien Grid

        sTotalImporte = (fn_util_ValidaDecimal(sTotal) + fn_util_ValidaDecimal($("#txtValorVehivulo").val()));

        if (sTotalImporte > sPrecioVenta) {
            pError.append('<br/> El Valor del Bien No debe exceder al Total Valor Leasing. ');
        }
    }
    else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) {
        var sPrecioVenta = $("#hidPrecioVenta").val(); //Valor Bien Leasing
        var sTotal = $("#hidTotal").val(); //Total Valor bien Grid

        sTotalImporte = (fn_util_ValidaDecimal(sTotal) + fn_util_ValidaDecimal($("#txtValorBienOtros").val()));

        if (sTotalImporte > sPrecioVenta) {
            pError.append('<br/> El Valor del Bien No debe exceder al Total Valor Leasing. ');
        }
    }
    //Fin
   
    return pError.toString();
}

// IBK JJM
function fn_Back() {
    var objinthidSecFinanciamiento = $("#hidSecFinanciamiento").val();
    //alert(objinthidSecFinanciamiento);
    objinthidSecFinanciamiento = parseInt(objinthidSecFinanciamiento) - 1;
    //alert(objinthidSecFinanciamiento);
    if ($("#hidSecFinanciamiento").val() == '2') {
        //Inhabilitar el boton cuando no haya mas registros para retroceder
        //alert('-1');
        $('#hBack').attr('disabled', true);
        $('#hBack').attr('disabled', 'disabled');
    }
    else {
        //alert('frmMantenimientoBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + $("#hidSecFinanciamiento").val() + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#HidCodEstado").val());
        $('#hBack').attr('enabled', true);
        $('#hBack').attr('enabled', 'enabled');

        //alert($("#hidPrecioVenta").val());
        fn_util_redirect('frmMantenimientoBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + objinthidSecFinanciamiento + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#HidCodEstado").val() + "&precioventa=" + $("#hidPrecioVenta").val());
    }
}
function fn_Next() {
    var objinthidSecFinanciamiento = $("#hidSecFinanciamiento").val();
    //alert(objinthidSecFinanciamiento);
    objinthidSecFinanciamiento = parseInt(objinthidSecFinanciamiento) + 1;
    //alert(objinthidSecFinanciamiento);
    var objinthidMaxSecFinanciamiento = $("#hidMaxSecFinanciamiento").val();
    //alert(objinthidMaxSecFinanciamiento);
    if (objinthidSecFinanciamiento > objinthidMaxSecFinanciamiento) {
        //Inhabilitar el boton cuando no haya mas registros para avanzar
        //alert('+1');

        $('#hNext').attr('disabled', true);
        $('#hNext').attr('disabled', 'disabled');
    }
    else {
        //alert(objinthidSecFinanciamiento[0]);
        $('#hNext').attr('enabled', true);
        $('#hNext').attr('enabled', 'enabled');

        //alert($("#hidPrecioVenta").val());
        fn_util_redirect('frmMantenimientoBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + objinthidSecFinanciamiento + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#HidCodEstado").val() + "&precioventa=" + $("#hidPrecioVenta").val());
    }
}


// function fn_ValidaPeriodofn_ValidaPeriodo() {
function fn_ValidaPeriodo() {






    //debugger;
    var rows = jQuery("#jqGrid_lista_B").jqGrid('getRowData');
    var actual = new Date();
    var anio = actual.getUTCFullYear();
    var sFabricacion = $("#txtAnioVehivulo").val();
    var total = 0;
    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        var sPerido = row.Periodo;
        var aniFab = sPerido - sFabricacion;
        if (sPerido < anio) {
            total = total + 1;










        }
        if (sPerido > anio) {
            if (aniFab > 3) {
                total = total + 1;




            }

        }



    }
    return total.toString();
}
//Fin IBK JJM
//Fin IBK JJM

//Fin IBK JJM

/*
// IBK JJM
function fn_Back() {
var objinthidSecFinanciamiento = $("#hidSecFinanciamiento").val();
//alert(objinthidSecFinanciamiento);
//objinthidSecFinanciamiento = parseInt(objinthidSecFinanciamiento[0]) - 1;

//alert("Hola");

//debugger;
objinthidSecFinanciamiento = objinthidSecFinanciamiento - 1;
    
//alert(objinthidSecFinanciamiento);
if ($("#hidSecFinanciamiento").val() == '2') {
//Inhabilitar el boton cuando no haya mas registros para retroceder
//alert('1');
//$('#hBack').attr('disabled', true);  dv_img_botonBck
//$('input[name=hBack]').attr('disabled', 'disabled');
$('#hNext').attr('disabled', true);
$('#hNext').attr('disabled', 'disabled');
}
else {
//alert('frmMantenimientoBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + $("#hidSecFinanciamiento").val() + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#HidCodEstado").val());
fn_util_redirect('frmMantenimientoBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + objinthidSecFinanciamiento + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#HidCodEstado").val());
}
}
function fn_Next() {


//alert("CHAO");
var objinthidSecFinanciamiento = $("#hidSecFinanciamiento").val();
//alert(objinthidSecFinanciamiento);
    
//objinthidSecFinanciamiento = parseInt(objinthidSecFinanciamiento[0]) + 1;
objinthidSecFinanciamiento = objinthidSecFinanciamiento + 1;
    
//alert(objinthidSecFinanciamiento);
var objinthidMaxSecFinanciamiento = $("#hidMaxSecFinanciamiento").val();
alert(objinthidMaxSecFinanciamiento);
if (objinthidSecFinanciamiento > objinthidMaxSecFinanciamiento) {
//Inhabilitar el boton cuando no haya mas registros para avanzar
//alert('1');

$('#hBack').attr('disabled', true);

$('#hBack').attr('disabled', 'disabled');
}
else {
//alert(objinthidSecFinanciamiento[0]);
fn_util_redirect('frmMantenimientoBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + objinthidSecFinanciamiento + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#HidCodEstado").val());
}
}
//Fin IBK JJM
*/