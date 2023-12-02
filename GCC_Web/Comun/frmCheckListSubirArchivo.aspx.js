//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
//****************************************************************
$(document).ready(function() {
    //On load Page (siempre al final)

    //alert($("#hddflaCondAdicional").val());
    $('#txtDocumentos').validText({ type: 'comment',length:100});
    if ($("#hddflaCondAdicional").val() == "1") {
        $("#trNombreDocumento").hide();
        $("#trCondAicional").show();
        
    } else {
        
        $("#trNombreDocumento").show();
        $("#trCondAicional").hide();
    }

    fn_onLoadPage();
});

//
function fn_GuardarDetalleDocumento() {

    parent.fn_blockUI();
    //Valida
    var strError = new StringBuilderEx();

    var objtxtDocumento = $('input[id=txtDocumentos]');
    var objcmbCondicionAdicional = $('select[id=cmbCondicionesAdicionales]');
    var objtxtArchivoDocumentos = $('input[id=txtArchivoDocumentos]');
    
    
    //
    if ($("#hddflaCondAdicional").val() == 1) {
        strError.append(fn_util_ValidateControl(objcmbCondicionAdicional[0], 'Condicion Adicional', 1, ''));
        //strError.append(fn_util_ValidateControl(objtxtArchivoDocumentos[0], 'Archivo', 1, ''));
         
    } else {
        strError.append(fn_util_ValidateControl(objtxtDocumento[0], 'Nombre del Documento', 1, ''));
    }

	 var arrPara = ["pstrNumeroContrato", $("#hddCodContrato").val(),"pstrCodigoTipoCondicion",$("#cmbCondicionesAdicionales").val(),"pintFlagCartaEnvio",1];
     
        var arrResultado = fn_util_ejecutarASHX(arrPara, 'whValidaCondicionDocumento', '../');
     
        if (arrResultado.length > 0) {
            if (arrResultado[0] == '1') {
                if (parseFloat(arrResultado[1]) > 0) {
                    fn_mdl_alert("Ya se ingresó la condición seleccionada", function() { });
                    parent.fn_unBlockUI();
                    return;
                	 
                }
            }
        }
    
    //Valida si hay Error
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        fn_seteaCamposObligatorios();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }
    else {
        //FALTA IMPLEMENTAR
        $("#cmdguardar").click();
        fn_util_CierraModal();
    }
    
    
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_seteaCamposObligatorios() {
    
    
    fn_util_SeteaObligatorio($("#cmbCondicionesAdicionales"), "select");
    fn_util_SeteaObligatorio($("#txtArchivoDocumentos"), "input");


}

//****************************************************************
// Funcion              ::      fn_cargaListado
// Descripción  ::      Guardar Rechazo
// Log                  ::      JRC - 21/05/2012
//****************************************************************
function fn_cargaListado() {
  
    var ctrlBtn = window.parent.frames[0].document.getElementById('btnBuscar');
    ctrlBtn.click();
    parent.fn_util_CierraModal();

}