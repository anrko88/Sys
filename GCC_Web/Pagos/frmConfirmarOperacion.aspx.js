var strOperacion;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
//****************************************************************
$(document).ready(function() {

    fn_util_SeteaObligatorio($("#txtMotivo"), "input");

    var pag = window.parent.frames[0];
    strOperacion = pag.strOperacionModal;

    $("#lblOperacion").html(strOperacion);
    
    //On load Page (siempre al final)
    fn_onLoadPage();
});

//****************************************************************
// Funcion		:: 	fn_selectCredito
// Descripción	::	Devuelve el registro seleccionado en la lista
// Log			:: 	WCR - 15/05/2012
//****************************************************************
function fn_ejecutarOperacion() {

    var strMotivo = jQuery.trim($("#txtMotivo").val());

    if (strMotivo == "") {

        var strAux;
        if (strOperacion == "Extornar") strAux = "el extorno.";
        else if (strOperacion == "Anular") strAux = "la anulación.";
        else strAux = "la operación.";
        
        parent.fn_mdl_mensajeIco("Debe ingresar el motivo de " + strAux, "util/images/error.gif", "Validación de Datos");
        return;
    }
     
    var pag = window.parent.frames[0];

    if (strOperacion == "Anular") {
        pag.fn_anular(strMotivo);
    }
    else if (strOperacion == "Extornar") {
        pag.fn_extornar(strMotivo);
    }
    else if (strOperacion == "Devolver") {
        pag.fn_devolver(strMotivo);
    }
    
    parent.fn_util_CierraModal();
}

//****************************************************************
// Funcion		:: 	fn_limpiar
// Descripción	::	Devuelve el registro seleccionado en la lista
// Log			:: 	WCR - 15/05/2012
//****************************************************************
function fn_limpiar() {
    $('#txtMotivo').val('');
}