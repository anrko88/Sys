//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	EBL - 10/02/2012
//****************************************************************
$(document).ready(function() {


    if ($("#hddEdita").val() == "no") {
        $("#dv_img_boton_grabar").hide();
    } else {
        $("#dv_img_boton_grabar").show();
    }


    //On load Page (siempre al final)
    fn_onLoadPage();
});

//****************************************************************
// Función		:: 	fn_grabar
// Descripción	::	Le consulta al usuario si desea actualizar el contenido del texto
//                  predefinido.
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function fn_grabar() {
    var mensaje;
    var titulo;
    if ($("#hddNuevo").val() == "S") {
        mensaje = "¿Desea agregar el texto predefinido?";
        titulo = "AGREGAR TEXTO PREDEFINIDO";
    } else {
        mensaje = "¿Desea modificar el texto predefinido?";
        titulo = "ACTUALIZACIÓN DE TEXTO PREDEFINIDO";
    }
    
    parent.fn_mdl_confirma(
		    mensaje
		    , fn_TextoPredefinido_ActualizarWM
		    , "util/images/question.gif"
		    , function() { }
		    , titulo
	    );
}

//****************************************************************
// Función		:: 	fn_TextoPredefinido_ActualizarWM
// Descripción	::	Guarda los datos ingresados por el usuario a traves de un web method,
//                  evaluando si es una operación de editar o eliminar.
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function fn_TextoPredefinido_ActualizarWM() {
    parent.fn_blockUI();

    if (fn_util_trim($("#txaTextoPredefinido").val()) != "") {
        var arrParametros = [
                             "strNroContrato", $("#hddCodigoContrato").val(),
                             "intCodigoContratoDocumento", $("#hddCodigoContratoDocumento").val(),
                             "strTextoPredefinido", $("#txaTextoPredefinido").val()
                            ];

        fn_util_AjaxWM("frmTextoPredefinido.aspx/GuardarTextoPredefinido",
                       arrParametros,
                       fn_CierraModal,
                       function(result) {
                           parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                       });
    } else {
        parent.fn_mdl_mensajeIco("No ingreso un texto.", "util/images/warning.gif", "ADVERTENCIA");
    }

    parent.fn_unBlockUI();
}

//****************************************************************
// Función		:: 	fn_CierraModal
// Descripción	::	Le muestra un mensaje que confirma la realización de la operación y cierra la actual ventana modal.
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function fn_CierraModal() {
    var mensaje;
    var titulo;
    
    if ($("#hddNuevo").val() == "S") {
        mensaje = "Se agregó con éxito el texto predefinido.";
        titulo = "AGREGAR TEXTO PREDEFINIDO";
    } else {
        mensaje = "Se actualizó con éxito el texto predefinido.";
        titulo = "ACTUALIZAR TEXTO PREDEFINIDO";
    }
    var ctrlBtn = window.parent.frames[0].document.getElementById("btnTextoPredefinido");

    ctrlBtn.click();
    parent.fn_mdl_mensajeIco(mensaje, "util/images/ok.gif", titulo);
    
    parent.fn_util_CierraModal();
}