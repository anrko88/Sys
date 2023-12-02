//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	WCR - 21/01/2013
//****************************************************************
$(document).ready(function() {

    fn_util_SeteaCalendario($('input[id*=txtFechaActIni]'));
    fn_util_SeteaCalendario($('input[id*=txtFechaActFin]'));

});

//****************************************************************
// Funcion		:: 	fn_Reporte
// Descripción	::	
// Log			:: 	WCR - 21/01/2013
//****************************************************************
function fn_Reporte() {
    var strFechaActIni = fn_util_trim($("#txtFechaActIni").val());
    var strFechaActFin = fn_util_trim($("#txtFechaActFin").val());
    var strMensaje = '';


    if ((strFechaActIni == '') && (strFechaActFin != '')) { strMensaje = '- Ingrese fecha de activación desde<br/>'; }
    if ((strFechaActIni != '') && (strFechaActFin == '')) { strMensaje = '- Ingrese fecha de activación hasta<br/>'; }
    
    if ((strFechaActIni != '') && (strFechaActFin != '')) {
        if (fn_util_ComparaFecha(strFechaActFin, strFechaActIni)) {
            strMensaje = strMensaje + '- La fecha de activación de contrato final no puede ser menor a la fecha inicial<br/>';
        }
    }
    if ((strFechaActIni == '') && (strFechaActFin == '')) { strMensaje = '- Debe de ingresar fecha de activación<br/>'; }

    if (strMensaje != '') { parent.fn_mdl_mensajeIco(strMensaje, "util/images/warning.gif", "ADVERTENCIA"); }
    else {
        //        var strFAI = fn_FormatoFecha(strFechaActIni);
        //        var strFAF = fn_FormatoFecha(strFechaActFin);
        $("#btnGenerar").click();
        //        window.location = '../Reportes/frmRepDetalleBien.aspx?pfai=' + strFAI + '&pfaf=' + strFAF;
    }

}

//****************************************************************
// Funcion		:: 	fn_Limpiar
// Descripción	::	
// Log			:: 	WCR - 21/01/2013
//****************************************************************
function fn_Limpiar() {
    $("#txtFechaActIni").val('');
    $("#txtFechaActFin").val('');
}

//****************************************************************
// Funcion		:: 	fn_FormatoFecha
// Descripción	::	
// Log			:: 	WCR - 09/01/2013
//****************************************************************
function fn_FormatoFecha(pFecha) {
    var strFecha = '1900-01-01';
    if (fn_util_trim(pFecha) != '') {
        var arrFecha = pFecha.split('/');
        strFecha = arrFecha[2] + '-' + arrFecha[1] + '-' + arrFecha[0];
    }
    return strFecha;
}