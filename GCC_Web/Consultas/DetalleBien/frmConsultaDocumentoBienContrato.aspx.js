//****************************************************************
// Función		:: 	
// Descripción	::	Subir Archivo
// Log			:: 	IJM - 10/02/2012
//****************************************************************
$(document).ready(function() {

    if((($("#hidDetalle").val()!="1")) && ($("#hddTipoTransaccion").val()!="NUEVO")){	
//    	$("#tr_nombre").css('display', 'none');
//    	$("#tr_adjuntar").css('display', 'none');
        $("#dv_grabar").css('display', 'none');
    	$("#dv_separacion").css('display', 'none');
    	$("#txtNombreArchivo").attr('disabled', 'disabled');
    	$("#txtArchivoDocumentos").attr('disabled', 'disabled');
    	$("#txtComentario").attr('disabled', 'disabled');
    }    
   
	//On load Page (siempre al final)
    fn_onLoadPage();
	
	
});

//****************************************************************
// Función       ::      fn_Guardar
// Descripción   ::      Activa el botón que ejecuta la tarea siguiente al proceso de adjuntar el archivo seleccionado.
// Log           ::      EBL - 21/05/2012
//****************************************************************
function fn_Guardar() {
	//parent.fn_blockUI();
	//if ($("#hddCodigoDocumento").val() == '') {
 if($("#hddTipoTransaccion").val()!="NUEVO"){	
	 	 if ($("#txtNombreArchivo").val() === "") {
			parent.fn_mdl_mensajeIco("Ingrese un nombre de archivo.",
				"util/images/warning.gif",
				"ADJUNTAR ARCHIVO");

		} else {
			parent.fn_blockUI();
			$("#btnGrabar").click();
		}
	 }else {
	 
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

//	}else {
//	parent.fn_blockUI();
//			$("#btnGrabar").click();
//	}
}

//****************************************************************
// Función        ::      fn_cargaListado
// Descripción    ::      Activa el botón que ejecuta la tarea siguiente al proceso de adjuntar el archivo seleccionado.
// Log            ::      EBL - 21/05/2012
//****************************************************************
function fn_cargaListado() {
//    var control = $("#hddControl").val();
//    var file = $("#hddFile").val();

//    window.parent.frames[0].document.getElementById($("#btnCargarAdjuntos").val()).value = file;
//    
    var ctrlBtn = window.parent.frames[0].document.getElementById('btnCargarAdjuntos');
    ctrlBtn.click();
	parent.fn_unBlockUI();
    parent.fn_util_CierraModal();
}

//****************************************************************
// Función        ::      fn_mensaje
// Descripción    ::      Muestra un mensaje de advertencia con el error ocurrido en el servidor durante el proceso de adjuntar
//                        un archivo.
// Log            ::      EBL - 21/05/2012
//****************************************************************
function fn_mensaje(mensaje) {
    parent.fn_mdl_mensajeIco(mensaje,
                             "util/images/warning.gif",
                             "ERROR AL ADJUNTAR ARCHIVO");
}