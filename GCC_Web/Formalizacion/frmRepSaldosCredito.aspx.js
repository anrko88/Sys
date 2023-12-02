//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	VMA - 29/01/2013
//****************************************************************
$(document).ready(function() {

    fn_util_SeteaCalendario($('input[id*=txtFechaInicial]'));
});
function fn_VerReporte() {
    var strFechaInicial = $("#txtFechaInicial").val();

    if (fn_util_trim(strFechaInicial) == "") {
        parent.fn_mdl_mensajeIco("Debe ingresar la fecha de saldos de crédito inicial", "util/images/error.gif", "ADVERTENCIA");
    } 
    else {
        $("#btnGenerar").click();
    }
}

//****************************************************************
// Funcion		:: 	fn_Limpiar
// Descripción	::	
// Log			:: 	VMA - 29/01/2013
//****************************************************************
function fn_Limpiar() {
    $("#txtFechaInicial").val('');
}