//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	VMA - 15/01/2013
//****************************************************************
$(document).ready(function() {

    fn_util_SeteaCalendario($('input[id*=txtFechaInicial]'));
    fn_util_SeteaCalendario($('input[id*=txtFechaFinal]'));
});
function fn_VerReporte() {
    var strFechaInicial = $("#txtFechaInicial").val();
    var strFechaFinal = $("#txtFechaFinal").val();


    if (fn_util_trim(strFechaInicial) == "") {
        parent.fn_mdl_mensajeIco("Debe ingresar la fecha de opción de compra inicial", "util/images/error.gif", "ADVERTENCIA");
    } else if (fn_util_trim(strFechaFinal) == "") {
        parent.fn_mdl_mensajeIco("Debe ingresar la fecha de opción de compra final", "util/images/error.gif", "ADVERTENCIA");
    } else if (fn_util_ComparaFecha(strFechaFinal, strFechaInicial)) {
    parent.fn_mdl_mensajeIco("La fecha de opción de compra final no puede ser menor a la fecha de opción de compra inicial", "util/images/error.gif", "ADVERTENCIA");
    } else {
        $("#btnGenerar").click();
    }
}

//****************************************************************
// Funcion		:: 	fn_Limpiar
// Descripción	::	
// Log			:: 	VMA - 24/01/2013
//****************************************************************
function fn_Limpiar() {
    $("#txtFechaInicial").val('');
    $("#txtFechaFinal").val('');
}