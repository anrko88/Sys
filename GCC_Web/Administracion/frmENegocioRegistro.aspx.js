//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 20/03/2013
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();
    
    //On load Page (siempre al final)
    fn_onLoadPage();
    
});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 20/03/2013
//****************************************************************
function fn_inicializaCampos() {
	fn_util_SeteaObligatorio($("#txtCodigo"), "input");
	fn_util_SeteaObligatorio($("#txtNombre"), "input");
	fn_util_SeteaObligatorio($("#cmbELeasing"), "select");

	$('#txtCodigo').validText({ type: 'alphanumeric', length: 10 });
    $('#txtNombre').validText({ type: 'alphanumeric', length: 100 });    
}




//****************************************************************
// Funcion		:: 	fn_guardar 
// Descripción	::	Guardar 
// Log			:: 	JRC - 20/03/2013
//****************************************************************
function fn_guardar() {

    var blnGraba = true;
    var strError = "";

    if (fn_util_trim($("#txtCodigo").val()) == "") { 
        strError = strError +"- Debe ingresar un Código <br/>"
    }

	if (fn_util_trim($("#txtNombre").val()) == "") { 
        strError = strError +"- Debe ingresar un Nombre <br/>"
    }    

	if (fn_util_trim($("#cmbELeasing").val()) == "0") { 
        strError = strError +"- Debe seleccionar un Ejecutivo de Leasing <br/>"
    }
    
    if (fn_util_trim(strError) != "") {
        parent.fn_mdl_mensajeError(strError, function() { }, "VALIDACIÓN");
    } else {
        parent.fn_blockUI();
        $("#btnGrabar").click();    
    }
    
}


//****************************************************************
// Funcion		:: 	fn_grabadoOK 
// Descripción	::	Guardar 
// Log			:: 	JRC - 20/03/2013
//****************************************************************
function fn_grabadoOK(strMensaje){
	parent.fn_mdl_alert(strMensaje, function() { parent.fn_util_CierraModal(); fn_cargaListado(); });
}


//****************************************************************
// Funcion		:: 	fn_cargaListado 
// Descripción	::	Carga Listado 
// Log			:: 	JRC - 20/03/2013
//****************************************************************
function fn_cargaListado() {

    var ctrlBtn = window.parent.frames[0].document.getElementById('btnBuscar');
    ctrlBtn.click();
    parent.fn_util_CierraModal();

}
