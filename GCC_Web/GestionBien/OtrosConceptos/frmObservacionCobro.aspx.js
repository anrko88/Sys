//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	WCR - 20/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();
    fn_SetearFlagRegistro('0');
    //On load Page (siempre al final)
    fn_onLoadPage();
});

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_inicializaCampos() {

    $("#txtObservaciones").validText({ type: 'comment', length: 150 }); ;
    $("#txtObservaciones").maxLength(150);
}

//****************************************************************
// Funcion		:: 	fn_guardar
// Descripción	::	
// Log			:: 	WCR - 29/11/2012
//****************************************************************
function fn_guardar() {

    //        fn_mdl_confirma('¿Esta seguro de grabar?',
    //            function() {
    //                parent.fn_blockUI();

    var strNumeroContrato = $('#hidCodSolicitudCredito').val() == undefined ? "" : $('#hidCodSolicitudCredito').val();
    var strTipoRubroFinanciamiento = $('#hidTipoRubroFinanciamiento').val() == undefined ? "" : $('#hidTipoRubroFinanciamiento').val();
    var strCodIfi = $('#hidCodIfi').val() == undefined ? "" : $('#hidCodIfi').val();
    var strTipoRecuperacion = $('#hidTipoRecuperacion').val() == undefined ? "" : $('#hidTipoRecuperacion').val();
    var strNumSecRecuperacion = $('#hidNumSecRecuperacion').val() == undefined ? "" : $('#hidNumSecRecuperacion').val();
    var strNumSecRecupComi = $('#hidNumSecRecupComi').val() == undefined ? "" : $('#hidNumSecRecupComi').val();
    var strCodComisionTipo = $("#hidCodComisionTipo").val() == "0" ? "" : $("#hidCodComisionTipo").val();
    var strObservaciones = $('#txtObservaciones').val() == undefined ? "" : $('#txtObservaciones').val();
    var arrParametros = ["pstrNumeroContrato", strNumeroContrato,
                                     "pstrTipoRubroFinanciamiento", strTipoRubroFinanciamiento,
                                     "pstrCodIfi", strCodIfi,
                                     "pstrTipoRecuperacion", strTipoRecuperacion,
                                     "pstrNumSecRecuperacion", strNumSecRecuperacion,
            	 		             "pstrNumSecRecupComi", strNumSecRecupComi,
            	 		             "pstrCodComisionTipo", strCodComisionTipo,
            	 		             "pstrObservaciones", strObservaciones
                                    ];

    fn_util_AjaxWM("frmObservacionCobro.aspx/GrabaCobro",
                    arrParametros,
                    function(result) {
                        parent.fn_unBlockUI();
                        if (fn_util_trim(result) == "0") {
                            parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "GRABAR OBSERVACION");
                        } else {
                            fn_SetearFlagRegistro('1');
                            fn_RedireccionGrabar();
                            //parent.fn_mdl_mensajeOk("El cobro se actualizó correctamente", function() { fn_RedireccionGrabar() }, "GRABAR OBSERVACION");
                        }
                    },
                     function(resultado) {
                         parent.fn_unBlockUI();
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "GRABAR OBSERVACION");
                     }

                );
    //            },
    //            "../../util/images/question.gif",
    //            function() { },
    //            'Otros Conceptos :: Cobros'
    //         );

}

//****************************************************************
// Función		:: 	fn_RedireccionGrabar
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
//                  Luego lo redirecciona al formulario de resgistro de cobro.
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_RedireccionGrabar() {
    var winPag = window.parent.frames[0];
    winPag.fn_ActualizarListado();
    winPag.fn_util_MuestraLogPage("La observación se actualizó correctamente.", "I");
    parent.fn_util_CierraModal();
}

function fn_SetearFlagRegistro(pFlag) {
    var winPag = window.parent.frames[0].document;
    var hidRegistro = winPag.getElementById("hidFlagRegistro");
    if (hidRegistro != null) { hidRegistro.value = pFlag; }

}