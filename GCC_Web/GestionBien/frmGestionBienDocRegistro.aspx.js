//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
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
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
    $('#txtNombre').validText({ type: 'alphanumeric', length: 100 });
    $('#txtComentario').validText({ type: 'alphanumeric', length: 250 });
    $("#txtComentario").maxLength(250);
}




//****************************************************************
// Funcion		:: 	fn_guardar 
// Descripción	::	Guardar Rechazo
// Log			:: 	JRC - 21/05/2012
//****************************************************************
function fn_guardar() {

    var blnGraba = true;
    var strError = "";

    if (fn_util_trim($("#txtNombre").val()) == "") { 
        strError = strError +"- Debe ingresar un Nombre <br/>"
    }

    if (fn_util_trim($("#txtAdjunto").val()) == "" && fn_util_trim($("#hddAdjunto").val()) == "" && fn_util_trim($("#txtComentario").val()) == "") {		
        strError = strError + "- Debe de adjuntar un documento o ingresar un comentario";        
    }

    if (fn_util_trim(strError) != "") {
        parent.fn_mdl_mensajeError(strError, function() { }, "VALIDACIÓN");
    } else {
        parent.fn_blockUI();
        $("#btnGrabar").click();    
    }
    
}



//****************************************************************
// Funcion		:: 	fn_cargaListadoDoc 
// Descripción	::	Lista Documentos
// Log			:: 	JRC - 21/05/2012
//****************************************************************
function fn_cargaListadoDoc(pstrMensaje) {
	
	var hddCodContrato = fn_util_trim($("#hddCodContrato").val());
	var hddCodBien = fn_util_trim($("#hddCodBien").val());
	var hddCodRelacionado = fn_util_trim($("#hddCodRelacionado").val());
	var hddCodTipo = fn_util_trim($("#hddCodTipo").val());
	var strParametros = "?hddCodContrato="+hddCodContrato+"&hddCodBien="+hddCodBien+"&hddCodRelacionado="+hddCodRelacionado+"&hddCodTipo="+hddCodTipo

	parent.fn_mdl_mensajeOk(pstrMensaje, function() { fn_util_globalRedirect("/GestionBien/frmGestionBienDocListado.aspx"+strParametros); }, "GRABADO CORRECTO");
}

 
//****************************************************************
// Funcion		:: 	fn_volverListado
// Descripción	::	Volver a Listado de Documentos
// Log			:: 	JRC - 21/05/2012
//****************************************************************
function fn_volverListado() {
	
	var hddCodContrato = fn_util_trim($("#hddCodContrato").val());
	var hddCodBien = fn_util_trim($("#hddCodBien").val());
	var hddCodRelacionado = fn_util_trim($("#hddCodRelacionado").val());
	var hddCodTipo = fn_util_trim($("#hddCodTipo").val());
	var strParametros = "?hddCodContrato="+hddCodContrato+"&hddCodBien="+hddCodBien+"&hddCodRelacionado="+hddCodRelacionado+"&hddCodTipo="+hddCodTipo
	
	fn_util_globalRedirect("/GestionBien/frmGestionBienDocListado.aspx"+strParametros);

}
