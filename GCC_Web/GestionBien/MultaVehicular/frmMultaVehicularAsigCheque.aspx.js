//****************************************************************
// Variables Globales
//****************************************************************
var strValidacion = '';

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	??? - 02/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();
    $("#txtCheque").focusout(function() {
        //debugger;
        fn_Validacion();
    });
    //Inicio IBK JJM
    $("#txtNroLote").focusout(function() {
        fn_CargaData();
    });
    //inicio IBK - AAE
    if ($("#txtNroLote").val() != "") {
        fn_CargaData();
    }
    //Fin IBK - AAE
    
    //On load Page (siempre al final)
    fn_onLoadPage();

});

function fn_CargaData() {
    if ($("#txtNroLote").val() != "") {
        var Lote = $("#txtNroLote").val();
        var arrParametros = ["pstrLote", Lote];
        fn_util_AjaxSyncWM("frmMultaVehicularAsigCheque.aspx/ObtieneDatosLote",
            arrParametros,
                fn_PonerDatos,
                        function(resultado) {
                            parent.fn_unBlockUI();
                            var error = eval("(" + resultado.responseText + ")");
                            parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR Al CARGAR");
                        }
            );
    }
}
fn_PonerDatos = function(response) {
    var objEImpuestoMunicipal = response;
    var Muni = objEImpuestoMunicipal.Municipalidad == undefined ? "0" : objEImpuestoMunicipal.Municipalidad;
    var Total = objEImpuestoMunicipal.Total == undefined ? "0" : objEImpuestoMunicipal.Total;
    var Fechacobro = objEImpuestoMunicipal.FechacobroStr == undefined ? "0" : objEImpuestoMunicipal.FechacobroStr;
    $("#txtMunicipalidad").val(Muni);
    $("#txtImporteTotal").val(Total);
    $("#txtFechaGeneracion").val(Fechacobro);
};

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {
    $('#txtNroLote').validText({ type: 'number', length: 8 });
    $('#txtCheque').validText({ type: 'alphanumeric', length: 20 });
    fn_util_SeteaCalendario($("#txtFechaPago"));
    $('#txtImporteTotal').validNumber({ value: '', decimals: 2, length: 20 });
    fn_SetearCamposObligatorios();
}

function fn_SetearCamposObligatorios() {
    fn_util_SeteaObligatorio($("#txtNroLote"), "text");
    fn_util_SeteaObligatorio($("#txtCheque"), "text");
    fn_util_SeteaObligatorio($("#txtFechaPago"), "text");
    fn_util_SeteaCalendario($("#txtFechaPago"));
}

//****************************************************************
// Funcion		:: 	fn_asignaCheque
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_asignaCheque() {
    //parent.fn_blockUI();

    //String Validación
    var strError = new StringBuilderEx();

    var objtxtLote = $('input[id=txtNroLote]:text');
    var objtxtNroCheque = $('input[id=txtCheque]:text');
    var objtxtFechaPago = $('input[id=txtFechaPago]:text');

    //Valida
    strError.append(fn_util_ValidateControl(objtxtLote[0], 'un Lote válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtNroCheque[0], 'un Nro. de Cheque válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtFechaPago[0], 'un fecha de pago', 1, ''));

    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
        fn_SetearCamposObligatorios();
    } else {
        if (strValidacion == '2') {
            strError = "El Lote ingresado no existe.";

        }
        //        else {
        if (strValidacion == '3') {
            strError = "-El Lote ingresado ya cuenta con un cheque asignado."
            strError = strError + "El proceso modificará solo el Nro de Cheque y la Fecha Pago."
            strError = strError + "No volverá a cobrar al cliente,¿Desea reemplazarlo?";
        }
        //            else
        if (strValidacion == '0') {
            strError = '¿Está seguro de asignar el cheque?.';
        }
        //            else {
        if (strValidacion == '-1') {
            strError = "Error al consultar lote.";

        }
        if (strValidacion != '2' && strValidacion != '-1') {
            parent.fn_mdl_confirma(strError, //Mensaje - Obligatorio
                            function() {
                                var arrParametros = [
							"pstrLote", $("#txtNroLote").val(),
							"pstrNroCheque", $("#txtCheque").val(),
							"pstrFechaPago", Fn_util_DateToString($("#txtFechaPago").val())
							];
                                parent.fn_blockUI();
                                fn_util_AjaxWM("frmMultaVehicularAsigCheque.aspx/AsignarCheque",
                 arrParametros,
                 function(result) {
                     parent.fn_unBlockUI();
                     if (fn_util_trim(result) == "0") {
                         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL GUARDAR");
                     } else {
                         parent.fn_mdl_mensajeIco("Se asignó el cheque correctamente.", "util/images/error.gif", "ASIGNAR");

                     }
                 },
                 function(resultado) {
                     parent.fn_unBlockUI();
                     var error = eval("(" + resultado.responseText + ")");
                     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABADO");
                 });

                            },
        "Util/images/question.gif",
        function() { },
        'Grabar');
        }
        else {
            //parent.fn_blockUI();
            if (strValidacion == '-1') {
                parent.fn_mdl_mensajeIco(strError, "util/images/error.gif", "ERROR AL ASIGNAR");
            }
            if (strValidacion == '2') {
                parent.fn_mdl_mensajeIco(strError, "util/images/error.gif", "ERROR AL ASIGNAR");
            }
        }
    }
}
//}
function fn_RedireccionGrabar() {
    var ctrlBtn = window.parent.frames[0].document.getElementById('btnCargarMulta');
    ctrlBtn.click();
    parent.fn_util_CierraModal();
}
function fn_Validacion() {

    if ($("#txtCheque") != '') {
        var arrParametros = ["pstrLote", $("#txtNroLote").val()];

        fn_util_AjaxWM("frmMultaVehicularAsigCheque.aspx/ValidaLotesCheque",
                 arrParametros,
                 fn_DatosObtenidos,
                 function(resultado) {
                     parent.fn_unBlockUI();
                     var error = eval("(" + resultado.responseText + ")");
                     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABADO");
                 }
        );
    }
}
function fn_DatosObtenidos(response) {
    if (fn_util_trim(response) == "0") {
        strValidacion = "0";
        //parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL ASIGNAR");
    }
    else if (fn_util_trim(response) == "2") {
        strValidacion = "2";
        //parent.fn_mdl_mensajeIco("El Lote ingresado no existe.", "util/images/error.gif", "ERROR AL ASIGNAR");
    }
    else if (fn_util_trim(response) == "3") {
        strValidacion = "3";
        //parent.fn_mdl_mensajeIco("El Lote ingresado ya cuenta con un cheque asignado", "util/images/error.gif", "ERROR AL ASIGNAR");
    }
    else {
        strValidacion = "-1";
    }

    // parent.fn_unBlockUI();
}
//Fin IBK JJM 