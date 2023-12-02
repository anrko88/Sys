//****************************************************************
// Funcion		:: 	JQUERY - Subir Archivo
// Descripción	::	
//					el documento
// Log			:: 	WCR 29/01/2013
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
        $("#cmdguardar").click();

    }
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	WCR 29/01/2013
//****************************************************************
function fn_seteaCamposObligatorios() {
    fn_util_SeteaObligatorio($("#txtArchivoDocumentos"), "input");
}

//****************************************************************
// Funcion              ::      fn_cargaListado
// Descripción          ::      
// Log                  ::      WCR 29/01/2013
//****************************************************************
function fn_cargaListado() {
    var winPag = window.parent.frames[0];
    winPag.fn_ListaGrillaDocumento();
    parent.fn_util_CierraModal();
}