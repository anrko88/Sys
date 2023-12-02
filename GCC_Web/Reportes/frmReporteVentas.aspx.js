//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JJM - 30/11/2012
//****************************************************************
$(document).ready(function() {

    fn_util_SeteaCalendario($('input[id*=txtFechaInicial]'));
    fn_util_SeteaCalendario($('input[id*=txtFechaFinal]'));
});

function fn_venta() {
    var strFechaInicial = $("#txtFechaInicial").val();
    var strFechaFinal = $("#txtFechaFinal").val();


    if (fn_util_trim(strFechaInicial) == "") {
        parent.fn_mdl_mensajeIco("Debe ingresar la Fecha Inicial", "util/images/error.gif", "ADVERTENCIA");
    } else if (fn_util_trim(strFechaFinal) == "") {
        parent.fn_mdl_mensajeIco("Debe ingresar la Fecha Final", "util/images/error.gif", "ADVERTENCIA");
    } else if (fn_util_ComparaFecha(strFechaFinal, strFechaInicial)) {
        parent.fn_mdl_mensajeIco("La fecha final no puede ser menor a la fecha inicial", "util/images/error.gif", "ADVERTENCIA");
    } else {
        $("#btnGenerar").click();
    }

}