//****************************************************************
// Funcion		:: 	JQUERY - Subir Archivo
// Descripción	::	
//					el documento
// Log			:: 	IJM - 10/02/2012
//****************************************************************
$(document).ready(function() {
    
    //On load Page (siempre al final)
    fn_onLoadPage();
});

//
function fn_Guardar() {
    
    parent.fn_blockUI();
    var strError = new StringBuilderEx();
    var objtxtRutaArchivo = $('input[id=txtArchivoDocumentos]');
    
    
    strError.append(fn_util_ValidateControl(objtxtRutaArchivo[0], 'un Archivo', 1, ''));

    if (strError.toString() != '') {
       
        parent.fn_unBlockUI();
        fn_seteaCamposObligatorios();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;

    }else {
//    if ($("#txtArchivoDocumentos").val() == "") {
//        parent.fn_mdl_alert("Seleccione un Documento", function() { });

//    } else {
        $("#cmdguardar").click();
        //fn_util_CierraModal();
        //parent.fn_mdl_alert("Se Registro Correctamente", function() { });
    }
       
//    }

}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_seteaCamposObligatorios() {
    fn_util_SeteaObligatorio($("#txtArchivoDocumentos"), "input");
}

//****************************************************************
// Funcion              ::      fn_cargaListado
// Descripción          ::      Guardar Rechazo
// Log                  ::      JRC - 21/05/2012
//****************************************************************
function fn_cargaListado() {

    var ctrlBtn = window.parent.frames[0].document.getElementById('btnBuscar');
    ctrlBtn.click();
    parent.fn_util_CierraModal();
////JJM - IBK
//    if (ctrlBtn != undefined) {
//        ctrlBtn.click();
//        parent.fn_util_CierraModal();
//    }
    
    //JJM - IBK
}