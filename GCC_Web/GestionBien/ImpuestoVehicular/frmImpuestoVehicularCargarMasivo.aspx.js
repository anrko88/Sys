//****************************************************************
// Variables Globales
//****************************************************************


//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	??? - 02/11/2012
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
// Log			:: 	AEP - 08/11/2012
//****************************************************************
function fn_inicializaCampos() {
$('#txtPeriodo').validText({ type: 'number', length: 4 });
}

//****************************************************************
// Funcion		:: 	fn_GrabarMasivoCarga
// Descripción	::	fn_GrabarMasivoCarga
// Log			:: 	AEP - 08/11/2012
//****************************************************************
function fn_GrabarMasivoCarga() {
	
	if ($("#txtArchivoDocumentos").val() === "") {
			parent.fn_mdl_mensajeIco("Seleccione un documento para adjuntarlo.",
				"util/images/warning.gif",
				"ADJUNTAR ARCHIVO");
		} else if ($("#txtNombreArchivo").val() === "") {
			parent.fn_mdl_mensajeIco("Ingrese un nombre de archivo.",
				"util/images/warning.gif",
				"ADJUNTAR ARCHIVO");

		} else {
			parent.fn_blockUI();
			$("#btnGrabar").click();
		}
	
	
	
    
}

function fn_redireccionarResumen () {
	
	parent.fn_util_CierraModal();
	 var ctrlBtn = window.parent.frames[0].document.getElementById('btnResumenCarga');
     ctrlBtn.click();
}

