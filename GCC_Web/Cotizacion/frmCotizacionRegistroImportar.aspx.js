//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	IJM - 07/03/2012
//****************************************************************
$(document).ready(function() {
    //Setea Calendario
    //fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    //Valida Tabs

    //$("div#divTabs").tabs({
    //    show: function(event, ui) {
    //        fn_doResize();
    //    }
    //});

    //Carga Grilla
    //fn_cargaGrilla();
    //$("#jqGrid_lista_C").setGridWidth($(window).width() - 120);

    //Valida Campos
    //fn_inicializaCampos();


    //On load Page (siempre al final)
    //fn_onLoadPage();

});


//************************************************************
// Función		:: 	fn_grabar
// Descripcion 	:: 	Método que graba
// Log			:: 	JRC - 10/02/2012
//************************************************************
function fn_grabar() {

}

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() {


}

function fn_procesa_Archivo_cronograma() {
    if ($("#txtArchivoDocumentos").val() == "") {
        parent.fn_mdl_mensajeIco('Debe de seleccionar un archivo.', "util/images/warning.gif", "Cronograma");
    }
    else {
        parent.fn_mdl_mensajeIco("Se completo la Importación ", "util/images/warning.gif", "Cronograma");
        var ifrm = window.top.frames("ifrm_contenedor");
        ifrm.fn_activaTabs(3);
        parent.fn_util_CierraModal();
    }
}