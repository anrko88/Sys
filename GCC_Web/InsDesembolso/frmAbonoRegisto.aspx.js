//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 03/10/2012
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
// Log			:: 	JRC - 04/10/2012
//****************************************************************
function fn_inicializaCampos() {
	$("#txtMonto").validNumber({ value: $("#txtMonto").val() });		
	
	$('#cmbProveedor').attr('disabled', 'disabled');
	$('#cmbProveedor').val($("#hddCodProveedor").val());
	$('#cmbMoneda').val($("#hddCodMoneda").val());

	$("#txtMonto").focus();
	$('#cmbMoneda').attr('disabled', 'disabled');
	  
}



//****************************************************************
// Funcion		:: 	fn_guardar 
// Descripción	::	Guardar Adelanto
// Log			:: 	JRC - 03/10/2012
//****************************************************************
function fn_guardar() {
	parent.fn_blockUI();

    var blnGraba = true;
    var strError = new StringBuilderEx();

	var objcmbProveedor = $('select[id=cmbProveedor]');
	var objcmbMoneda = $('select[id=cmbMoneda]');
	var objtxtMonto = $('input[id=txtMonto]:text');
	
	strError.append(fn_util_ValidateControl(objcmbProveedor[0], 'un Proveedor válido', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbMoneda[0], 'una Moneda válida', 1, ''));
	strError.append(fn_util_ValidateControl(objtxtMonto[0], 'un Monto válido', 1, ''));

    //	if (fn_util_ValidaDecimal($("#txtMonto").val()) > fn_util_ValidaDecimal($("#hddimporteProveedor").val())) {

    //	    parent.fn_mdl_alert("El importe ingresado es mayor al importe Total de pago", function() { });
    //	    parent.fn_unBlockUI();
    //	    return;
	//	}

	var decMonto = fn_util_ValidaDecimal($('#txtMonto').val());
	var decImporteAgrupa = fn_util_ValidaDecimal($('#hddImporteAgrupa').val());

	if (decMonto > decImporteAgrupa) {
	    strError.append("El Adelanto es mayor al importe Total de pago");
	}	
		
    if (fn_util_trim(strError) != "") {
		parent.fn_unBlockUI();
        parent.fn_mdl_mensajeError(strError, function() { }, "VALIDACIÓN");
        strError = null;

        
    } else {
        
        var arrParametros = ["pstrCodContrato",      $("#hddCodigoContrato").val(),
							 "pstrCodInsDesembolso", $("#hddCodigoInsDesembolso").val(),
							 "pstrCodProveedor",     $("#cmbProveedor").val(),        //$("#cmbProveedor").val(),
							 "pstrCodMoneda",        $("#cmbMoneda").val(),
							 "pstrCodMonto",         $("#txtMonto").val()
                            ];


        fn_util_AjaxWM("frmAbonoRegisto.aspx/GrabaAbono",
				 arrParametros,
				 function(result) {
				     parent.fn_unBlockUI();
				     if (fn_util_trim(result) == "1") {
				         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL GUARDAR");
				     } else {
				         parent.fn_mdl_mensajeOk("El Adelanto fué grabado correctamente", function() { fn_RedireccionGrabar(); }, "GRABADO CORRECTO");
				     }
				 },
				 function(resultado) {
				     parent.fn_unBlockUI();
				     var error = eval("(" + resultado.responseText + ")");
				     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABADO");
				 }
		);    
        
    }
    
}
function fn_RedireccionGrabar() {
	var ctrlBtn = window.parent.frames[0].document.getElementById('btnListaAdelantos');
    ctrlBtn.click();
    parent.fn_util_CierraModal();
}


//****************************************************************
// Funcion		:: 	fn_cargaDatosProveedor 
// Descripción	::	Onchange del Combo Proveedor
// Log			:: 	IJM - 09/10/2012
//****************************************************************
function fn_cargaDatosProveedor(strDatos) {
    var resultado = strDatos;
    var result = resultado.split('|');
    
    $("#hddCodigoProveedor").val(result[0]);
    $("#hddimporteProveedor").val(result[1]);

    //$("#txtMonto").val(fn_util_ValidaDecimal(result[1]));    
    //alert($("#hddimporteProveedor").val());
    
}
