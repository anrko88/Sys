var strConcepto_ComisionActivacion = "08";
var strConcepto_ComisionEstructuracion = "09";
var strConcepto_CuotaInicial = "10";
var strConcepto_ExtornoCuota = "11";
var strConcepto_Precuota = "12";

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 03/10/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();

    //Bloquea Moneda
    $("#cmbMoneda").val($("#hddCodMonedaContrato").val());

    $("#cmbMoneda").prop({ disabled: true });

    /* Inicio IBK - AAE */
    $("#txtPorc").focusout(function() {
        fn_CalculaMontoCarg();
    });

    $("#txtMonto").focusout(function() {
        fn_CalculaPorc();
    }); 
    /* Fin */

    //Oculta Campo Porcentaje
    fn_ocultaCampos();

    //---------------------------------
    //Valida Tipo Medio
    //---------------------------------
    $('#cmbConcepto').change(function() {
        var strValor = $(this).val();
        fn_validaConcepto(strValor);
    });

    //On load Page (siempre al final)
    fn_onLoadPage();

});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 04/10/2012
//****************************************************************
function fn_inicializaCampos() {
	
	$("#cmbConcepto option[value='01']").remove();
	$("#cmbConcepto option[value='02']").remove();
	$("#cmbConcepto option[value='03']").remove();
	$("#cmbConcepto option[value='04']").remove();
	$("#cmbConcepto option[value='05']").remove();
	$("#cmbConcepto option[value='06']").remove();
	$("#cmbConcepto option[value='07']").remove();
	$("#cmbConcepto option[value='13']").remove();
	$("#txtMonto").validNumber({ value: $("#txtMonto").val() });		  
	$("#txtPorc").validNumber({ value: $("#txtPorc").val() });
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

	var objcmbConcepto = $('select[id=cmbConcepto]');
	var objcmbMoneda = $('select[id=cmbMoneda]');
	var objtxtMonto = $('input[id=txtMonto]:text');
	
	strError.append(fn_util_ValidateControl(objcmbConcepto[0], 'un Concepto válido', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbMoneda[0], 'una Moneda válida', 1, ''));
	strError.append(fn_util_ValidateControl(objtxtMonto[0], 'un Monto válido', 1, ''));
		
			
    if (fn_util_trim(strError) != "") {
		parent.fn_unBlockUI();
        parent.fn_mdl_mensajeError(strError, function() { }, "VALIDACIÓN");
        strError = null;
    } else {
        
        var arrParametros = ["pstrCodContrato", $("#hddCodigoContrato").val(),
							 "pstrCodInsDesembolso", $("#hddCodigoInsDesembolso").val(),
							 "pstrCodConceptoCargo", $("#cmbConcepto").val(),
							 "pstrCodMoneda", $("#cmbMoneda").val(),
							 "pstrCodMonto", $("#txtMonto").val(),
							 "pstrPorcMonto", $("#txtPorc").val()];
														
        fn_util_AjaxWM("frmCargoRegistro.aspx/GrabaCargo",
				 arrParametros,
				 function(result) {
					 parent.fn_unBlockUI();
					 if (fn_util_trim(result) == "1") {
						 parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL GUARDAR");
					 } else {                         
						parent.fn_mdl_mensajeOk("El Cargo fué grabado correctamente", function() { fn_RedireccionGrabar() }, "GRABADO CORRECTO");
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
	var ctrlBtn = window.parent.frames[0].document.getElementById('btnListaCargos');
    ctrlBtn.click();
    parent.fn_util_CierraModal();
}


//****************************************************************
// Funcion		:: 	fn_ocultaGrupos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 04/10/2012
//****************************************************************
function fn_ocultaCampos() {
	$("#tr_Porc").hide();    
}


//****************************************************************
// Funcion		:: 	fn_validaConcepto
// Descripción	::	Valida Conceptos 
// Log			:: 	JRC - 09/10/2012
//****************************************************************
function fn_validaConcepto(strValor) {
    
	fn_ocultaCampos();                 
    if (fn_util_trim(strValor) == strConcepto_ComisionActivacion) {
		$("#tr_Porc").show();		
		//Inicio IBK - AAE
		if ($("#hddRdComActivacion").val() == "1") {
		    $("#txtPorc").
		    $('#txtMonto').prop('readonly', true);
		    $('#txtMonto').attr('class', 'css_input_inactivo');
		    fn_calculaMonto();
		} else {
		    $('#txtMonto').validNumber({ value: fn_util_ValidaMonto($('#hddMontoComActivacion').val(), 2) });
		    fn_CalculaPorc();
		}
		//Fin IBK
        
        
    } 
    if (fn_util_trim(strValor) == strConcepto_ComisionEstructuracion) {
        $("#tr_Porc").show();
        if ($("#hddRdComEstructuracion").val() == "1") {
            $("#txtPorc").validNumber({ value: fn_util_ValidaMonto($('#hddPorcComEstructuracion').val(), 2) });
            $('#txtMonto').prop('readonly', true);
            $('#txtMonto').attr('class', 'css_input_inactivo');
            fn_calculaMonto();
        } else {
            $('#txtMonto').validNumber({ value: fn_util_ValidaMonto($('#hddMontoComEstructuracion').val(), 2) });
            fn_CalculaPorc();
        }            
    } 
    if (fn_util_trim(strValor) == strConcepto_CuotaInicial) {
		$("#tr_Porc").show();  
        $("#txtPorc").validNumber({ value: fn_util_ValidaMonto($('#hddPorcCuotaInicial').val(), 2) });
        $('#txtMonto').prop('readonly', true);
        $('#txtMonto').attr('class', 'css_input_inactivo');
        fn_calculaMonto();
    } 
    if (fn_util_trim(strValor) == strConcepto_ExtornoCuota || fn_util_trim(strValor) == strConcepto_Precuota) {	
        $("#txtPorc").validNumber({ value: fn_util_ValidaMonto("0", 2) });
        $('#txtMonto').prop('readonly', false);
        $('#txtMonto').attr('class', 'css_input');        
        $("#txtMonto").val(fn_util_ValidaMonto("0", 2));
    }
    /* Inicio IBK - AAE - Se elimina el grisado de campos*/
    $('#txtMonto').prop('readonly', false);
    $('#txtMonto').attr('class', 'css_input');        
    /* Fin IBK*/
	$("#txtMonto").addClass("ui-edit-align-right");
}


//****************************************************************
// Funcion		:: 	fn_validaConcepto
// Descripción	::	Valida Conceptos 
// Log			:: 	JRC - 09/10/2012
//****************************************************************
function fn_calculaMonto() {
	var strPorc = $('#txtPorc').val();
	var strTotal = $('#hddMontoTotal').val();
	if(fn_util_trim(strPorc) == "")strPorc="0"
	if(fn_util_trim(strTotal) == "")strTotal="0"
	
    var decPorc = fn_util_ValidaDecimal(strPorc);
    var decTotal = fn_util_ValidaDecimal(strTotal);
    var strValor = $('#cmbConcepto').val()
    //Se comento el calculo del IGV  ---  JJM IBK
    //Inicio IBK - AAE - Saco el IGV
    if (fn_util_trim(strValor) == strConcepto_CuotaInicial) {    
        decTotal = (decTotal/1.18)
    }
    //Fin IBK
    
    var decMonto = ((decPorc/100)*decTotal);
    //alert(decPorc+"/"+decTotal+"="+decMonto);
    
    $("#txtMonto").addClass("ui-edit-align-right");
    $('#txtMonto').val(fn_util_ValidaMonto(decMonto, 2));
}

/* Inicio IBK - AAE - Agrego funciones para actualizar procentajes */
//****************************************************************
// Funcion		:: 	fn_CalculaMontoCarg
// Descripción	::	Calcula montos en base al porcentaje 
// Log			:: 	AAE
//****************************************************************
function fn_CalculaMontoCarg() {
    var strPorc = $('#txtPorc').val();
    var strTotal = $('#hddMontoTotal').val();
    if (fn_util_trim(strPorc) == "") strPorc = "0"
    if (fn_util_trim(strTotal) == "") strTotal = "0"

    var decPorc = fn_util_ValidaDecimal(strPorc);
    var decTotal = fn_util_ValidaDecimal(strTotal);

    var decMonto = ((decPorc / 100) * decTotal);
    //alert(decPorc+"/"+decTotal+"="+decMonto);

    $("#txtMonto").addClass("ui-edit-align-right");
    $('#txtMonto').val(fn_util_ValidaMonto(decMonto, 2));
}

//****************************************************************
// Funcion		:: 	fn_CalculaPorc
// Descripción	::	Calcula montos en base al porcentaje  
// Log			:: 	AAE
//****************************************************************
function fn_CalculaPorc() {
    var strMonto = $('#txtMonto').val();
    var strTotal = $('#hddMontoTotal').val();
    if (fn_util_trim(strTotal) == "") txtMonto = "0"
    if (fn_util_trim(strTotal) == "") strTotal = "0"

    var decMonto = fn_util_ValidaDecimal(strMonto);
    var decTotal = fn_util_ValidaDecimal(strTotal);

    var decPorc = ((decMonto / decTotal) * 100);  
    
    if(isNaN(decPorc) || (isFinite(decPorc) == false )) {  
     decPorc = 100;
    }
    //alert(decPorc+"/"+decTotal+"="+decMonto);

    $("#txtPorc").addClass("ui-edit-align-right");
    $('#txtPorc').val(fn_util_ValidaMonto(decPorc, 2));
}
/* Fin IBK */