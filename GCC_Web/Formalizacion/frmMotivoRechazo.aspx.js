//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    //Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    //On load Page (siempre al final)
    fn_onLoadPage();

});


function fn_guardar() {
    var strUrl = "CotizacionNegociacion/frmCotizacionListado.aspx";
    parent.fn_util_cargaLinkModal(strUrl);
}