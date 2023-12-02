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
// Log			:: 	IJM - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {

   $("#TxaObs").validText({ type: 'comment', length: 300 }); ;
	$("#TxaObs").maxLength(300);
	$("#cmbTipoObservacion option[value='005']").remove();
   switch ($("#hflagtipoObs").val())
        {
          case '1': $("#dvtipoObservaciones").hide();
                    break;
          case '2': $("#dvtipoObservaciones").show();
                    break;
          case '3': $("#dv_guardar").hide();
                    $("#cmbTipoObservacion").attr('disabled', 'disabled');
                    $("#TxaObs").attr('disabled', 'disabled');
                    break;
        }

    }

function fn_guardar() {

    parent.fn_blockUI();
    //Valida
    var strError = new StringBuilderEx();

   
    var objTxaObs = $('textarea[id=TxaObs]');
    var objcmbTipoObservacion = $('select[id=cmbTipoObservacion]');

    strError.append(fn_util_ValidateControl(objcmbTipoObservacion[0], 'Tipo de Observacion', 1, ''));
 
    strError.append(fn_util_ValidateControl(objTxaObs[0], 'Observacion', 1, ''));
    //Valida si hay Error
     if (strError.toString() != '') {
         parent.fn_unBlockUI();
        
         parent.fn_mdl_alert(strError.toString(), function() { });
         strError = null;
     }
     else {

         var arrParametros = [
                             "strCodigoContratoDocumento",  $("#hddCodConDoc").val(),
                             "strnumeroContrato",           $("#hddCodContrato").val(),
                             "strObservacion",              $("#TxaObs").val(),
                             "CodigoTipoObservacion",       $("#cmbTipoObservacion").val(),
                             "audUsuarioRegistro",          $("#hddusuariosesion").val(),
                             "intFlagOrigenObservacion", 2
                            ];
         //alert(arrParametros);
         fn_util_AjaxWM("frmObservacion.aspx/InsertaObservacionDocumento",
                         arrParametros,
                         function() {
                             fn_ListaDocumentos();
                             //parent.fn_util_CierraModal();

                         },
                         function(result) {
                             parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                         }
                      );
     }

}

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
};


function fn_ListaDocumentos() {
    var ctrlBtn = window.parent.frames[0].document.getElementById('cmdListarDocumentos');
    ctrlBtn.click();

    parent.fn_util_CierraModal();
}