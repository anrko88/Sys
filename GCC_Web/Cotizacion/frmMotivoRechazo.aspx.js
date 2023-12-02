//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    //On load Page (siempre al final)
    fn_onLoadPage();

});


//****************************************************************
// Funcion		:: 	fn_guardar 
// Descripción	::	Guardar Rechazo
// Log			:: 	JRC - 21/05/2012
//****************************************************************
function fn_guardar() {

    var blnGraba = true;
    
    //Valida Documento
    if ($("#cmbTipoMotivo").val() == "0") {
        blnGraba = false;
        parent.fn_mdl_alert("Debe seleccionar el Tipo Motivo", function() { });
    }
    else if ($("#txtAdjunto").val() == "" && $("#txtComentario").val() == "") {
        blnGraba = false;
        parent.fn_mdl_alert("Debe de ingresar un comentario o adjuntar un mail de sustento", function() { });
    }
    else {
        blnGraba = true;
    }
    
    //Graba
    if (blnGraba) {

        parent.fn_mdl_confirma('¿Esta seguro que desea Rechazar la Cotización?',
            function() {
                parent.fn_blockUI();
                $("#btnGrabar").click();
            }
		    , "Util/images/question.gif"
		    , function() { }
		    , null
	    );

    }
        
}



//****************************************************************
// Funcion		:: 	fn_cargaListado 
// Descripción	::	Guardar Rechazo
// Log			:: 	JRC - 21/05/2012
//****************************************************************
function fn_cargaListado() {

    var ctrlBtn = window.parent.frames[0].document.getElementById('btnBuscar');
    ctrlBtn.click();
    parent.fn_util_CierraModal();

}
