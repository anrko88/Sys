//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	WCR 29/01/2013
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
// Log			:: 	WCR 29/01/2013
//****************************************************************
function fn_inicializaCampos() {
    $('#TxaObs').maxLength(300);
}

//****************************************************************
// Funcion		:: 	fn_guardar
// Descripción	::	
// Log			:: 	WCR 29/01/2013
//****************************************************************
function fn_guardar() {

    var strError = new StringBuilderEx();
    var objTxaObs = $('textarea[id=TxaObs]');

    strError.append(fn_util_ValidateControl(objTxaObs[0], 'Observacion', 1, ''));

    if (strError.toString() != '') {
        parent.fn_unBlockUI();

        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }
    else {
        var arrParametros = ["pstrNumeroContrato", $("#hddCodContrato").val(),
                             "pstrCodOpcionCompraDocumento", $("#hddCodOpcComDoc").val(),
                             "pstrCodCheckList", $("#hddCodCheck").val(),
                             "pstrObservacion", $("#TxaObs").val()
        ];

        fn_util_AjaxWM("frmObservacionOC.aspx/InsertaObservacionDocumento",
            arrParametros,
            function(result) {
                fn_cargaListado();
            },

            function(result) {
                parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
            }
        );
    }

}


//****************************************************************
// Funcion              ::      fn_cargaListado
// Descripción          ::      
// Log                  ::      WCR 29/01/2013
//****************************************************************
function fn_cargaListado() {
    var winPag = window.parent.frames[0];
    winPag.fn_ListaGrillaDocumento();
    parent.fn_util_CierraModal();
}