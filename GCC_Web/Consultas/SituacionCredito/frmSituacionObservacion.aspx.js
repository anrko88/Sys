//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
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


    if ($("#hddAdd").val() == "true") {
        //Si viene de Check Lis Comercial Se Habilita el Texto
        $("#TxaObs").validText({ type: 'comment', length: 300 }); ;
        $("#TxaObs").maxLength(300);
        $("#TxaObs").prop('readonly', false);
        $("#dv_img_botonGuardar").show();
        $("#dv_img_botonGuardar2").hide();

    } else if ($("#hddAdd").val() == "Inafectacion") {
        $("#TxaObs").validText({ type: 'comment', length: 300 }); ;
        $("#TxaObs").maxLength(300);
        $("#TxaObs").prop('readonly', false);
        $("#dv_img_botonGuardar2").show();
        $("#dv_img_botonGuardar").hide();
    }
    else {
        // Si viene de Check List Legal se Bloquea el Texto
        $("#TxaObs").prop('readonly', true);
        $("#dv_img_botonGuardar").hide();
        $("#dv_img_botonGuardar2").hide();
    }

    if ($("#hflagtipoObs").val() == 1) {
        $("#dvtipoObservaciones").hide();
    } else {
        $("#dvtipoObservaciones").show();
    }

}
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
        var arrParametros = ["strCodigoContratoDocumento", $("#hddCodConDoc").val(),
                             "strnumeroContrato", $("#hddCodContrato").val(),
                             "strObservacion", $("#TxaObs").val(),
                             "CodigoTipoObservacion", $("#cmbTipoObservacion").val(),
                             "audUsuarioRegistro", "XT0000",
                             "intFlagOrigenObservacion", 1
        ];

        fn_util_AjaxWM("frmCheckListObservacion.aspx/InsertaObservacionDocumento",
            arrParametros,
            function(result) {
                fn_ListaDocumentos();

            },

            function(result) {
                parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
            }
        );
    }

}
//INICIO-jjm
function fn_guardar2() {

    var strError = new StringBuilderEx();
    var objTxaObs = $('textarea[id=TxaObs]');

    strError.append(fn_util_ValidateControl(objTxaObs[0], 'Observacion', 1, ''));

    if (strError.toString() != '') {
        parent.fn_unBlockUI();

        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }
    else {
        var arrParametros = ["strCodigoContratoDocumento", $("#hddCodConDoc").val(),
                             "strnumeroContrato", $("#hddCodContrato").val(),
                             "strObservacion", $("#TxaObs").val(),
                             "CodigoTipoObservacion", $("#cmbTipoObservacion").val(),
                             "audUsuarioRegistro", "XT0000",
                             "intFlagOrigenObservacion", 1
        ];

        fn_util_AjaxWM("frmCheckListObservacion.aspx/InsertaObservacionDocumentoInafectacion",
            arrParametros,
            function(result) {
                fn_ListaDocumentos();

            },

            function(result) {
                parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
            }
        );
    }

}

//FIN-jjm
//****************************************************************
// Función		:: 	fn_MensajeYRedireccionar
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
//                  Luego lo redirecciona al formulario de búsquedas ("frmTemporalListado.aspx").
// Log			:: 	EBL - 07/05/2012
//****************************************************************
var fn_MensajeYRedireccionar = function(pCodigo) {

    if (pCodigo == "0") {
        fn_mdl_alert("Se grabó con éxito la Observaciones : " + pCodigo.toString(), function() { });

    } else {
        fn_mdl_alert("Se actualizó con éxito Observacion ", function() { });
    }
    parent.fn_util_CierraModal();
};


//****************************************************************
// Función      ::      fn_ListaRepresentantesCerrar
// Descripción  ::      Cierra la actual ventana y actualiza la grilla de representantes de la ventana padre.
// Log          ::      EBL - 21/05/2012
//****************************************************************
function fn_ListaDocumentos() {
    var ctrlBtn = window.parent.frames[0].document.getElementById('cmdListarDocumentos');
    ctrlBtn.click();
    parent.fn_util_CierraModal();
}