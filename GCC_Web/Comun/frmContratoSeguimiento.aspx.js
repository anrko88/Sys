//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    fn_inicializaCampos();
    
    //On load Page (siempre al final)
    fn_onLoadPage();
    $("#hddFlagVerificaAdjunto").val("");

});


//****************************************************************
// Funcion		:: 	fn_guardar 
// Descripción	::	Guardar Rechazo
// Log			:: 	JRC - 21/05/2012
//****************************************************************
function fn_guardar() {
 
            var strError = new StringBuilderEx();
            var objcmbTipoMotivo = $('select[id=cmbTipoMotivo]');
            var objtxtComentario = $('textarea[id=txtComentario]');
            var objtxtFecha = $('input[id=txtFecha]:text');
            //var objtxtArchivoDocumentos = $('input[id=txtArchivoDocumentos]:text');

            //var strcoment = trim($('#txtComentario').val());
            //alert(strcoment.length);
            //if (strcoment.lenght==0) {
            //    strError.append("");
            //}
            if (fn_util_trim($('#txtComentario').val()) == "" && fn_util_trim($('#txtArchivoDocumentos').val()) == "") 
            {
                strError.append("Ingrese Comentario o Adjunto");
            } 
            // else {
            //strError.append("Ingrese Comentario");
            //}

            strError.append(fn_util_ValidateControl(objcmbTipoMotivo[0], 'Motivo', 1, ''));
    
            //strError.append(fn_util_ValidateControl(objtxtComentario[0], 'Comentario', 1, ''));
            strError.append(fn_util_ValidateControl($("#HidFecha").val(), 'Fecha', 1, ''));

            //strError.append(fn_util_ValidateControl(objtxtArchivoDocumentos[0], 'Adjunto', 1, ''));
    
    
                if (strError.toString() != '') {
                    //parent.fn_unBlockUI();
                    parent.fn_mdl_alert(strError.toString(), function() { });
                    strError = null;
                    fn_seteaCamposObligatorios();
                }
                else {
                    parent.fn_mdl_confirma('¿Desea Devolver el Contrato?',
                function() {
                parent.fn_blockUI();
                    $("#btnGrabar").click();
                }
                , "Util/images/question.gif"
                , function() { }
                , null
                );
        }
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {

    //  $('#txtComentario').validText({ type: 'comment', length: 250 });
    //	$('#txtComentario').maxLength(250);
    $('#txtComentario').validText({ type: 'comment', length: 511 });
    $('#txtComentario').maxLength(511);
    
    $("#cmbTipoMotivo option[value='002']").remove();
    $("#cmbTipoMotivo option[value='003']").remove();
    
    fn_seteaCamposObligatorios();
    $('#txtFecha').attr('disabled', 'disabled');
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_seteaCamposObligatorios() {
    
    fn_util_SeteaObligatorio($("#cmbTipoMotivo"), "select");
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
