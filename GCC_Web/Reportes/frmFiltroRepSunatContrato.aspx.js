//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	WCR - 21/01/2013
//****************************************************************
$(document).ready(function() {

    fn_util_SeteaCalendario($('input[id*=txtFechaCelConIni]'));
    fn_util_SeteaCalendario($('input[id*=txtFechaCelConFin]'));
    fn_util_SeteaCalendario($('input[id*=txtFechaActIni]'));
    fn_util_SeteaCalendario($('input[id*=txtFechaActFin]'));

});

//****************************************************************
// Funcion		:: 	fn_Reporte
// Descripción	::	
// Log			:: 	WCR - 21/01/2013
//****************************************************************
function fn_Reporte() {
    var strFechaCelConIni = fn_util_trim($("#txtFechaCelConIni").val());
    var strFechaCelConFin = fn_util_trim($("#txtFechaCelConFin").val());
    var strFechaActIni = fn_util_trim($("#txtFechaActIni").val());
    var strFechaActFin = fn_util_trim($("#txtFechaActFin").val());
    var strMensaje = '';


    if ((strFechaCelConIni == '') && (strFechaCelConFin != '')) { strMensaje = '- Ingrese fecha de celebración contrato desde<br/>'; }
    if ((strFechaCelConIni != '') && (strFechaCelConFin == '')) { strMensaje = '- Ingrese fecha de celebración contrato hasta<br/>'; }
    if ((strFechaCelConIni != '') && (strFechaCelConFin != '')) {
        if (fn_util_ComparaFecha(strFechaCelConFin, strFechaCelConIni)) {
            strMensaje = strMensaje + '- La fecha de celebración de contrato final no puede ser menor a la fecha inicial<br/>';
        }
    }

    if ((strFechaActIni == '') && (strFechaActFin != '')) { strMensaje = '- Ingrese fecha de activación desde<br/>'; }
    if ((strFechaActIni != '') && (strFechaActFin == '')) { strMensaje = '- Ingrese fecha de activación hasta<br/>'; }
    if ((strFechaActIni != '') && (strFechaActFin != '')) {
        if (fn_util_ComparaFecha(strFechaActFin, strFechaActIni)) {
            strMensaje = strMensaje + '- La fecha de activación de contrato final no puede ser menor a la fecha inicial<br/>';
        }
    }
    if ((strFechaCelConIni == '') && (strFechaCelConFin == '') && (strFechaActIni == '') && (strFechaActFin == '')) { strMensaje = '- Debe de ingresar fecha de celebración de contrato o fecha de activación<br/>'; }

    if (strMensaje != '') { parent.fn_mdl_mensajeIco(strMensaje, "util/images/warning.gif", "ADVERTENCIA"); }
    else {
//        var strFCCI = fn_FormatoFecha(strFechaCelConIni);
//        var strFCCF = fn_FormatoFecha(strFechaCelConFin);
//        var strFAI = fn_FormatoFecha(strFechaActIni);
//        var strFAF = fn_FormatoFecha(strFechaActFin);
        $("#btnGenerar").click();
//        window.location = '../Reportes/frmRepSunatContrato.aspx?pfcci=' + strFCCI + '&pfccf=' + strFCCF + '&pfai=' + strFAI + '&pfaf=' + strFAF;
    }

}

//****************************************************************
// Funcion		:: 	fn_Limpiar
// Descripción	::	
// Log			:: 	WCR - 21/01/2013
//****************************************************************
function fn_Limpiar() {
    $("#txtFechaCelConIni").val('');
    $("#txtFechaCelConFin").val('');
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