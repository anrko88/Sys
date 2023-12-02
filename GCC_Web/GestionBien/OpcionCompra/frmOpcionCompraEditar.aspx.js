//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	WCR - 04/01/2013
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();

	
	$("#txtComisionOC").focusout(function() {		
		if (!$(this).is('[readonly]')){ 	
			var decImporteOC = fn_util_ValidaDecimal($('#txtImporteOC').val());
			var decComisionOC = fn_util_ValidaDecimal($('#txtComisionOC').val());
			if(decComisionOC > 100){
				$('#txtComisionOC').val(fn_util_ValidaMonto("0", 2));
				parent.fn_util_MuestraLogPage("Porcentaje no Válido.", "E");
			}else{
				var decCalculo = decImporteOC*(decComisionOC/100);
				$('#txtComisionOCMonto').val(fn_util_ValidaMonto(decCalculo, 2));
			}	
		}
	});
		
	$("#txtPorcentajeCGT").focusout(function() {
		if (!$(this).is('[readonly]')){ 	
			var decImporteOC = fn_util_ValidaDecimal($('#txtImporteOC').val());
			var decPorcentajeCGT = fn_util_ValidaDecimal($('#txtPorcentajeCGT').val());
			if(decPorcentajeCGT > 100){
				$('#txtPorcentajeCGT').val(fn_util_ValidaMonto("0", 2));
				parent.fn_util_MuestraLogPage("Porcentaje no Válido.", "E");
			}else{
				var decCalculo = decImporteOC*(decPorcentajeCGT/100);
				$('#txtPorcentajeCGTMonto').val(fn_util_ValidaMonto(decCalculo, 2));
			}	
		}
	});
	
	
	
	$("#txtComisionOCMonto").focusout(function() {
		if (!$(this).is('[readonly]')){ 	
			var decImporteOC = fn_util_ValidaDecimal($('#txtImporteOC').val());
			var decComisionOCMonto = fn_util_ValidaDecimal($('#txtComisionOCMonto').val());
			if(decComisionOCMonto > decImporteOC){
				$('#txtComisionOCMonto').val(fn_util_ValidaMonto("0", 2));
				parent.fn_util_MuestraLogPage("Monto no Válido.", "E");
			}else{
				var decCalculo = (decComisionOCMonto / decImporteOC) * 100;    
				$('#txtComisionOC').val(fn_util_ValidaMonto(decCalculo, 2));
			}	
		}
	});
		
	$("#txtPorcentajeCGTMonto").focusout(function() {
		if (!$(this).is('[readonly]')){ 	
			var decImporteOC = fn_util_ValidaDecimal($('#txtImporteOC').val());
			var decPorcentajeCGTMonto = fn_util_ValidaDecimal($('#txtPorcentajeCGTMonto').val());
			if(decPorcentajeCGTMonto > decImporteOC){
				$('#txtPorcentajeCGTMonto').val(fn_util_ValidaMonto("0", 2));
				parent.fn_util_MuestraLogPage("Monto no Válido.", "E");
			}else{
				var decCalculo = (decPorcentajeCGTMonto / decImporteOC) * 100;            
				$('#txtPorcentajeCGT').val(fn_util_ValidaMonto(decCalculo, 2));
			}	
		}
	});
	
	//Valida Check
	fn_validaCheck();

    //On load Page (siempre al final)
    fn_onLoadPage();
});



//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_inicializaCampos() {
    
    $('#txtComisionOC').validNumber({ value: '', decimals: 2, length: 5 });
    $('#txtPorcentajeCGT').validNumber({ value: '', decimals: 2, length: 5 });
    
    $('#txtComisionOCMonto').validNumber({ value: '', decimals: 2, length: 10 });
    $('#txtPorcentajeCGTMonto').validNumber({ value: '', decimals: 2, length: 10 });
    
    if ($('#hidOpcion').val() == '0') {
        $('#txtComisionOC').attr('readonly', 'readonly');
        $('#txtComisionOC').removeClass();
        $('#txtComisionOC').addClass('css_input_inactivo');
        $('#txtPorcentajeCGT').attr('readonly', 'readonly');
        $('#txtPorcentajeCGT').removeClass();
        $('#txtPorcentajeCGT').addClass('css_input_inactivo');
        $('#dv_guardar').hide();
        $('#dvTitulo').html('Opción de Compra: Consulta');
        dvTitulo
    }
    
//    fn_util_SeteaObligatorio($("#txtComisionOC"), "input");
//    fn_util_SeteaObligatorio($("#txtPorcentajeCGT"), "input");
}

//****************************************************************
// Funcion		:: 	fn_guardar
// Descripción	::	
// Log			:: 	WCR - 08/01/2013
//****************************************************************
function fn_guardar() {
    var sbError = new StringBuilderEx();

    fn_ValidarRegistro(sbError);
    if (sbError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(sbError.toString(), function() { });
        sbError = null;
    } else {

        var strNumeroContrato = $('#hidNumeroContrato').val() == undefined ? "" : $('#hidNumeroContrato').val();
        var strCodOpcionCompra = $('#hidCodOpcionCompra').val() == undefined ? "" : $('#hidCodOpcionCompra').val();
        var strComisionOC = $('#txtComisionOC').val() == undefined ? "0" : $('#txtComisionOC').val();
        var strPorcentajeCGT = $('#txtPorcentajeCGT').val() == undefined ? "0" : $('#txtPorcentajeCGT').val();
        
        var hddComisionOC = $('#hddComisionOC').val() == undefined ? "0" : $('#hddComisionOC').val();
        var hddGastosTransCGT = $('#hddGastosTransCGT').val() == undefined ? "0" : $('#hddGastosTransCGT').val();
        var txtComisionOCMonto = $('#txtComisionOCMonto').val() == undefined ? "0" : $('#txtComisionOCMonto').val();
        var txtPorcentajeCGTMonto = $('#txtPorcentajeCGTMonto').val() == undefined ? "0" : $('#txtPorcentajeCGTMonto').val();

        var arrParametros = ["pstrNumeroContrato", strNumeroContrato,
                             "pstrCodOpcionCompra", strCodOpcionCompra,
                             "pstrComisionOC", strComisionOC,
                             "pstrPorcentajeCGT", strPorcentajeCGT,
                             "pstrHddComisionOC", hddComisionOC,
                             "pstrHddGastosTransCGT", hddGastosTransCGT,
                             "pstrComisionOCMonto", txtComisionOCMonto,
                             "pstrPorcentajeCGTMonto", txtPorcentajeCGTMonto
                            ];

        fn_util_AjaxWM("frmOpcionCompraEditar.aspx/GrabaOpcionCompra",
                    arrParametros,
                    function(result) {
                        parent.fn_unBlockUI();
                        if (fn_util_trim(result) == "0") {
                            parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "GRABAR OPCION DE COMPRA");
                        } else {
                            fn_RedireccionGrabar();
                        }
                    },
                     function(resultado) {
                         parent.fn_unBlockUI();
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "GRABAR OPCION DE COMPRA");
                     }

                );
    }
}

//****************************************************************
// Función		:: 	fn_ValidarRegistro
// Descripción	::	
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_ValidarRegistro(sbError) {

//    var txtComisionOC = $('input[id=txtComisionOC]:text');
//    var txtPorcentajeCGT = $('input[id=txtPorcentajeCGT]:text');

//    if (!isNaN($('#txtComisionOC').val())) {
//        if (parseFloat($('#txtComisionOC').val()) == 0) { sbError.append('- Ingrese un porcentaje de comisión de opción de<br/>&nbsp;&nbsp;compra.<br/>'); }
//    }
//    if (!isNaN($('#txtPorcentajeCGT').val())) {
//        if (parseFloat($('#txtPorcentajeCGT').val()) == 0) { sbError.append('- Ingrese un porcentaje de comisión de gastos de <br/>&nbsp;&nbsp;transferencia.'); }
//    }
            
//    sbError.append(fn_util_ValidateControl(txtComisionOC[0], 'un Porcentaje de Comisión Opción de Compra ', 1, ''));
//    sbError.append(fn_util_ValidateControl(txtPorcentajeCGT[0], 'un Porcentaje de Comisión de Gastos de Transferencia ', 1, ''));
    return sbError.toString();
}


//****************************************************************
// Función		:: 	fn_RedireccionGrabar
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
//                  Luego lo redirecciona al formulario de resgistro de cobro.
// Log			:: 	WCR - 08/01/2013
//****************************************************************
function fn_RedireccionGrabar() {
    var winPag = window.parent.frames[0];
    //    winPag.fn_ActualizarListado();
    winPag.fn_ShowMensaje("Los datos se actualizaron correctamente.");
    parent.fn_util_CierraModal();
}




//****************************************************************
// Función		:: 	fn_validaRadio
// Log			:: 	JRC - 16/02/2013
//****************************************************************
function fn_validaRadio(pValor, pHidden, pDisabled, pTipo) {
    $("#"+pHidden).val(pValor);
    
    if(pTipo == 1){
		$('#txtComisionOC').prop('readonly', false);
		$('#txtComisionOC').addClass('css_input').removeClass('css_input_inactivo');    
	    
		$('#txtComisionOCMonto').prop('readonly', false);
		$('#txtComisionOCMonto').addClass('css_input').removeClass('css_input_inactivo');    
		
		$('#txtComisionOC').val("0.00");
		$('#txtComisionOCMonto').val("0.00");
		
    }else{
		$('#txtPorcentajeCGT').prop('readonly', false);
		$('#txtPorcentajeCGT').addClass('css_input').removeClass('css_input_inactivo');    
	    
		$('#txtPorcentajeCGTMonto').prop('readonly', false);
		$('#txtPorcentajeCGTMonto').addClass('css_input').removeClass('css_input_inactivo');        
		
		$('#txtPorcentajeCGT').val("0.00");
		$('#txtPorcentajeCGTMonto').val("0.00");	
    }
         
    $('#'+pDisabled).addClass('css_input_inactivo').removeClass('css_input');    
    $('#'+pDisabled).prop('readonly', true);
        
}


//****************************************************************
// Función		:: 	fn_validaRadio
// Log			:: 	JRC - 16/02/2013
//****************************************************************
function fn_validaCheck(){

	var hddComisionOC = $('#hddComisionOC').val() == undefined ? "0" : $('#hddComisionOC').val();
    var hddGastosTransCGT = $('#hddGastosTransCGT').val() == undefined ? "0" : $('#hddGastosTransCGT').val();

	if( hddComisionOC == "1"){		
		$('input[id=rdbComisionOCPorc]').attr('checked', true);
		$('input[id=rdbComisionOCMonto]').attr('checked', false);
		
		$('#txtComisionOCMonto').addClass('css_input_inactivo').removeClass('css_input');    
		$('#txtComisionOCMonto').prop('readonly', true);    
	}else{
		$('input[id=rdbComisionOCPorc]').attr('checked', false);
		$('input[id=rdbComisionOCMonto]').attr('checked', true);
		
		$('#txtComisionOC').addClass('css_input_inactivo').removeClass('css_input');    
		$('#txtComisionOC').prop('readonly', true);
	}

	if( hddGastosTransCGT == "1"){		
		$('input[id=rdbGastosTransCGTPorc]').attr('checked', true);
		$('input[id=rdbGastosTransCGTMonto]').attr('checked', false);
		
		$('#txtPorcentajeCGTMonto').addClass('css_input_inactivo').removeClass('css_input');    
		$('#txtPorcentajeCGTMonto').prop('readonly', true);
	}else{
		$('input[id=rdbGastosTransCGTPorc]').attr('checked', false);
		$('input[id=rdbGastosTransCGTMonto]').attr('checked', true);
		
		$('#txtPorcentajeCGT').addClass('css_input_inactivo').removeClass('css_input');    
		$('#txtPorcentajeCGT').prop('readonly', true);
	}
	
}

