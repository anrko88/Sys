﻿//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    fn_inicializaCampos();
    
    //On load Page (siempre al final)
    fn_onLoadPage();


});

function fn_ListaInmuebles() {
     var ctrlBtn = window.parent.frames[0].document.getElementById('btnCargarInmuebles');
     ctrlBtn.click();
     parent.fn_util_CierraModal();
 }
  function fn_ListaMaquinas() {
     var ctrlBtn = window.parent.frames[0].document.getElementById('btnCargarMaquinaria');
     ctrlBtn.click();
     parent.fn_util_CierraModal();
 }
 
   function fn_ListaVehiculo() {
     var ctrlBtn = window.parent.frames[0].document.getElementById('btnCargarVehiculo');
     ctrlBtn.click();
     parent.fn_util_CierraModal();
 }
 
  
   function fn_ListaDatosOtros() {
     var ctrlBtn = window.parent.frames[0].document.getElementById('btnCargarDatosOtros');
     ctrlBtn.click();
     parent.fn_util_CierraModal();
 }
 
//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {

    if ($("#hidOpcion").val() != "")
	{
		$("#div_guardar").css('display', 'none');
    	$("#div_Separador").css('display', 'none');
    	$('#txtComentario').attr('disabled', 'disabled');
    	
	} else {
    	$("#div_guardar").css('display', 'block');
    	$("#div_Separador").css('display', 'block');
    	fn_seteaCamposObligatorios();
    }
    $('#txtComentario').validText({ type: 'comment', length: 250 });
	$('#txtComentario').maxLength(250);
  
    
    $('#txtFecha').attr('disabled', 'disabled');
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_seteaCamposObligatorios() {
    
    fn_util_SeteaObligatorio($("#txtComentario"), "input");
    fn_util_SeteaObligatorio($("#txtFecha"), "input");
    fn_util_SeteaObligatorio($("#txtArchivoDocumentos"), "input");
}

//****************************************************************
// Funcion		:: 	fn_cargaListado 
// Descripción	::	Guardar Rechazo
// Log			:: 	JRC - 21/05/2012
//****************************************************************
function fn_cargaListado() {
    
    var ctrlBtn = window.parent.frames[0].document.getElementById('btnCancelar2');
    ctrlBtn.click();
    parent.fn_util_CierraModal();
   

}
