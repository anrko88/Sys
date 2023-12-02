//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	WCR - 20/11/2012
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
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_inicializaCampos() {
    $("#txtComisionAct").validNumber({ value: '' });
    $("#txtSaldoComisionAct").validNumber({ value: '' });
    $("#txtImporte").validNumber({ value: '' });
    $("#txtComision").validNumber({ value: '' });
    $("#txtIGVComision").validNumber({ value: '' });
    $("#txtTotal").validNumber({ value: '' });
    fn_util_SeteaCalendario($('input[id*=txtFechaCobro]'));
}

function fn_guardar() {

}

//****************************************************************
// Función		:: 	fn_MensajeYRedireccionar
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
//                  Luego lo redirecciona al formulario de búsquedas ("frmTemporalListado.aspx").
// Log			:: 	WCR - 20/11/2012
//****************************************************************
var fn_MensajeYRedireccionar = function(pCodigo) {
    if (pCodigo == "0") {
        fn_mdl_alert("Se grabó con éxito la Observaciones : " + pCodigo.toString(), function() { });
    } else {
        fn_mdl_alert("Se actualizó con éxito Observacion ", function() { });
    }
};

//****************************************************************
// Función		:: 	fn_ListaContrato
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_ListaContrato() {
    var ctrlBtn = window.parent.frames[0].document.getElementById('cmdListarContrato');
    ctrlBtn.click();

    parent.fn_util_CierraModal();
}

//****************************************************************
// Función		:: 	fn_buscarContrato
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_buscarContrato() {

}

//****************************************************************
// Función		:: 	fn_VentanaContrato
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_VentanaContrato() {
    var sTitulo = "Otros Conceptos";
    var sSubTitulo = "Gastos:: Búsqueda por Contrato";
    parent.fn_util_AbreModal2(sSubTitulo, "GestionBien/OtrosConceptos/frmBuscarContrato.aspx?Titulo=" + sTitulo + "&SubTitulo=" + sSubTitulo + "&hddCodConDoc=0", 1100, 520, function() { });

}
