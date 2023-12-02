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

    //Inicializa Fecha
    fn_util_SeteaCalendario($('input[id*=txtFechaPago]'));

    //Valida Campos
    fn_inicializaCampos();

    //Inicio IBK JJM
    $("#txtLote").focusout(function() {
        fn_CargaData();
    });
    $("#txtNroCheque").focusout(function() {
        fn_Validacion();
    });
    //Fin IBK JJM//inicio IBK - AAE
    if ($("#txtLote").val() != "") {
        fn_CargaData();
    }
    //Fin IBK - AAE
    
    //On load Page (siempre al final)
    fn_onLoadPage();

});
function fn_CargaData() {
    if ($("#txtLote").val() != "") {
        var Lote = $("#txtLote").val();
        var arrParametros = ["pstrLote", Lote];
        fn_util_AjaxSyncWM("frmImpuestoMultaInmuebleListadoAsigCheque.aspx/ObtieneDatosLote",
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
    $('#txtLote').validText({ type: 'number', length: 8 });
    $('#txtNroCheque').validText({ type: 'alphanumeric', length: 20 });
    fn_util_inactivaInput("txtMunicipalidad", "I");
    fn_util_inactivaInput("txtImporteTotal", "I");
    fn_util_inactivaInput("txtFechaGeneracion", "I");
    fn_SetearCamposObligatorios();

}

function fn_SetearCamposObligatorios() {
    fn_util_SeteaObligatorio($("#txtLote"), "text");
    fn_util_SeteaObligatorio($("#txtNroCheque"), "text");
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

    var objtxtLote = $('input[id=txtLote]:text');
    var objtxtNroCheque = $('input[id=txtNroCheque]:text');
    var objtxtFechaPago = $('input[id=txtFechaPago]:text');

    
    //Valida
    strError.append(fn_util_ValidateControl(objtxtLote[0], 'un Lote válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtNroCheque[0], 'un Nro. de Cheque válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtFechaPago[0], 'una Fecha de Pago válida', 1, ''));

    //Valida error existente
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
        fn_SetearCamposObligatorios();
    } else {
        if (strValidacion == '2') {
            strValidacion = "El Lote ingresado no existe.";
        }
        //        else {
        if (strValidacion == '3') {
            strValidacion = "El Lote ingresado ya cuenta con un cheque asignado,¿Desea reemplazarlo?";
        }
        //            else
        if (strValidacion == '0') {
            strValidacion = '¿Está seguro de asignar el cheque?.';
        }
        //            else {
        if (strValidacion == '-1') {
            strValidacion = "Error al consultar lote.";
            
        }
        //else {
        parent.fn_mdl_confirma(strValidacion, //Mensaje - Obligatorio
                            function() {
                                var arrParametros = [
							"pstrLote", $("#txtLote").val(),
							"pstrNroCheque", $("#txtNroCheque").val(),
							"pstrFechaPago", Fn_util_DateToString($("#txtFechaPago").val())
							];
                                parent.fn_blockUI();
                                fn_util_AjaxWM("frmImpuestoMultaInmuebleListadoAsigCheque.aspx/AsignarCheque",
                 arrParametros,
                 function(result) {
                     parent.fn_unBlockUI();
                     if (fn_util_trim(result) == "0") {
                         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL GUARDAR");
                     } else {
                         parent.fn_mdl_mensajeIco("Se asignó el cheque correctamente.", "util/images/error.gif", "ERROR AL ASIGNAR");

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
    //}
}
//}
function fn_RedireccionGrabar() {
    var ctrlBtn = window.parent.frames[0].document.getElementById('btnBuscarImpuestos');
    ctrlBtn.click();
    parent.fn_util_CierraModal();
}
//Inicion IBK JJM
function fn_Validacion() {
    
    if ($("#txtNroCheque") != '') {
        var arrParametros = ["pstrLote", $("#txtLote").val(),
							"pstrNroCheque", $("#txtNroCheque").val(),
							"pstrFechaPago", Fn_util_DateToString($("#txtFechaPago").val())
							];

        fn_util_AjaxWM("frmImpuestoMultaInmuebleListadoAsigCheque.aspx/ValidaLotesCheque",
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
 function fn_DatosObtenidos (response) {
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