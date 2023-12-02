
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	AEP - 24/09/2012
//****************************************************************
$(document).ready(function() {
fn_doResize();

	fn_SetearCamposObligatorios();    
	
	   fn_onLoadPage();
	
	$('#txtPartidaRegistral').validText({ type: 'number',length:8});
});

function fn_SetearCamposObligatorios() {

}

