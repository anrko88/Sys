var strConceptoCheque = "001";
var strConceptoCuenta = "002";
var strConceptoInterbancario = "003";
var strConceptoCartaCrédito = "004";
var strConceptoTransferenciaExterior = "005";
var strConceptoPagoAgenteAduana = "006";
var strConceptoContable = "007";
var strConceptoComisiones = "008";
var strConceptoCuotaInicial = "009";
var strConceptoPrecuotas = "010";
var strConceptoExtornoCuotaInicial = "011";

var strCmbMedio = "";
var strComboVacio = "<option value='0'>[-Seleccione-]</option>";

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 03/10/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();


    if ($("#hddAccion").val() == 1) {
        $("#dv_img_boton_g").hide();
        fn_util_bloquearFormulario();
    }

    //Inicio IBK - AAE - Valido el bloqueo según el estado de ejecución del pago
    // Si esta ejecutado o Administrativo no puedo modificar
    if (($("#hddCodEstadoEjecucion").val() == "02") || ($("#hddCodEstadoEjecucion").val() == "04")) {
        $("#dv_img_boton_g").hide();
        fn_util_bloquearFormulario();
    }

    //Fin IBK 

    //Oculta Grupos
    fn_ocultaGrupos();


    //Valida Editar
    var strMedioPago = $("#hddCodigoMedioPago").val();
    if (fn_util_trim(strMedioPago) != "") {
        fn_validaMedioPago(strMedioPago);
    }


    //---------------------------------
    //Valida Tipo Medio
    //---------------------------------
    $('#cmbMedio').change(function() {
        var strValor = $(this).val();
        var strMonCont = $("#hddCodMonedaContrato").val();
        fn_validaMedioPago(strValor);
        $("#hddCodigoMedioPago").val(strValor);
        // Inicio IBK - AAE
        if (strValor == strConceptoContable) {
            $("#cmbMonedaPend").val(strMonCont);
            $("#cmbMonedaPend").attr('disabled', 'disabled');
        } else {        		
            $("#cmbMonedaPend").removeAttr('disabled');
        }
        if (fn_util_trim(strValor) == strConceptoCartaCrédito || fn_util_trim(strValor) == strConceptoTransferenciaExterior || fn_util_trim(strValor) == strConceptoInterbancario) {
            cargarValoresMedioPago(fn_util_trim(strValor))
        }
        
        // Fin IBK

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
	
	//Inicializa Campos
	$("#txtMonto").validNumber({ value: $("#txtMonto").val() });
	
	//Cuenta corriente
	$('#txtNroCuenta').validText({ type: 'number', length: 13 });
    
    //Carta de credito
	$('#txtPendiente').validText({ type: 'comment', length: 14 });
	$('#txtEmisora').validText({ type: 'number', length: 3 });
	$('#txtReceptora').validText({ type: 'number', length: 3 });
	$('#txtNota').validText({ type: 'comment', length: 8 });
	//Inicio IBK - AAE
	//$('#txtMontoComision').validNumber({ type: 'number', length: 10 });
	$('#txtMontoComision').validNumber({ value: $("#txtMontoComision").val() });


	$('#txtCuentaBancaria').validText({ type: 'number', length: 20 });
	$('#txtNroDocumento').validText({ type: 'comment', length: 11 });
	$('#txtRazonSocial').validText({ type: 'comment', length: 150 });

	//Inicio IBK - AAE - campos no domiciliado
	$('#txtCargoNoDom').validNumber({ value: $("#txtCargoNoDom").val() });
	$('#txtAbonoNoDom').validNumber({ value: $("#txtAbonoNoDom").val() });
	$('#txtCtaCargoNoDom').validText({ type: 'number', length: 13 });
	$('#txtCtaAbonoNoDom').validText({ type: 'number', length: 13 });
	// Fin IBK


	fn_seteaCamposObligatorios();
    

	//Inicializa Combo Medios
	strCmbMedio = $('#cmbMedio').html();		
	var strTipoGrupoHtml = $("#hddCodigoGrupoHtml").val();
	
	//Inicio IBK - AAE
	if ($('#cmbMedio').val() == strConceptoContable) {
		$("#cmbMonedaPend").attr('disabled', 'disabled');
    }
	// Fin IBK
	
	//Proveedor
	if(fn_util_trim(strTipoGrupoHtml) == "B"){
		$('#cmbMedio').html(strCmbMedio);
	}
	
	//Cliente
	if(fn_util_trim(strTipoGrupoHtml) == "C"){
		$('#cmbMedio').html(strCmbMedio);
		$("#cmbMedio option[value='004']").remove();
		$("#cmbMedio option[value='005']").remove();
		$("#cmbMedio option[value='006']").remove();
	}
	
	//Sunat
	if(fn_util_trim(strTipoGrupoHtml) == "D"){
		$('#cmbMedio').html(strCmbMedio);
		$("#cmbMedio option[value='001']").remove();
		$("#cmbMedio option[value='002']").remove();
		$("#cmbMedio option[value='003']").remove();
		$("#cmbMedio option[value='004']").remove();
		$("#cmbMedio option[value='005']").remove();
		$("#cmbMedio option[value='006']").remove();
	}
	
	//Diferencia Cambio
	if(fn_util_trim(strTipoGrupoHtml) == "E"){
		$('#cmbMedio').html(strCmbMedio);
		$("#cmbMedio option[value='001']").remove();
		$("#cmbMedio option[value='002']").remove();
		$("#cmbMedio option[value='003']").remove();
		$("#cmbMedio option[value='004']").remove();
		$("#cmbMedio option[value='005']").remove();
		$("#cmbMedio option[value='006']").remove();
	}
    
    //Cargos    
	if(fn_util_trim(strTipoGrupoHtml) == "F"){
		$('#cmbMedio').html(strCmbMedio);			
		$("#cmbMedio option[value='001']").remove();	
		$("#cmbMedio option[value='003']").remove();
		$("#cmbMedio option[value='004']").remove();
		$("#cmbMedio option[value='005']").remove();
		$("#cmbMedio option[value='006']").remove();
	}
	
	
}

function fn_seteaCamposObligatorios() {


    fn_util_SeteaObligatorio($("#txtMonto"), "input");
    fn_util_SeteaObligatorio($("#txtNroCuenta"), "input");
    fn_util_SeteaObligatorio($("#txtPendiente"), "input");
    fn_util_SeteaObligatorio($("#txtEmisora"), "input");
    fn_util_SeteaObligatorio($("#txtReceptora"), "input");
    fn_util_SeteaObligatorio($("#txtNota"), "input");
    fn_util_SeteaObligatorio($("#txtMontoComision"), "input");
    fn_util_SeteaObligatorio($("#txtCuentaBancaria"), "input");
    fn_util_SeteaObligatorio($("#txtNroDocumento"), "input");
    fn_util_SeteaObligatorio($("#txtRazonSocial"), "input");

    fn_util_SeteaObligatorio($("#cmbMonedaPend"), "select");
    fn_util_SeteaObligatorio($("#cmbPagoComision"), "select");
    fn_util_SeteaObligatorio($("#cmbMonedaCuenta"), "select");
    fn_util_SeteaObligatorio($("#cmbTipoCuenta"), "select");
    fn_util_SeteaObligatorio($("#cmbTipoDocumento"), "select");
    fn_util_SeteaObligatorio($("#cmbTipoDocumento"), "select");
    
   
}
//****************************************************************
// Funcion		:: 	fn_ocultaGrupos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 04/10/2012
//****************************************************************
function fn_ocultaGrupos() {
	$("#tr_grupo1").hide();  
	$("#tr_grupo2").hide();  
	$("#tr_grupo3_1").hide();  
	$("#tr_grupo3_2").hide();  
	$("#tr_grupo4_1").hide();  
	$("#tr_grupo4_2").hide();
	$("#tr_grupo5").hide();
	//Inicio IBK - AAE - Oculto campos No Dom
	$("#tr_grupo6_1").hide();
	$("#tr_grupo6_2").hide();
	//Fin IBK 
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

	var cmbMedio = $('select[id=cmbMedio]');
	var objcmbPagoComision = $('select[id=cmbPagoComision]');
	var objcmbMonedaPend = $('select[id=cmbMonedaPend]');
	var objcmbMonedaCuenta = $('select[id=cmbMonedaCuenta]');
	var objcmbTipoCuenta = $('select[id=cmbTipoCuenta]');
	var objtxtNroCuenta = $('input[id=txtNroCuenta]:text');
	
	var objtxtPendiente = $('input[id=txtPendiente]:text');
	var objtxtNota = $('input[id=txtNota]:text');
	var objtxtEmisora = $('input[id=txtEmisora]:text');
	var objtxtReceptora = $('input[id=txtReceptora]:text');
	var objtxtMontoComision = $('input[id=txtMontoComision]:text');
	var objtxtCuentaBancaria = $('input[id=txtCuentaBancaria]:text');
	var objtxtNroDocumento = $('input[id=txtNroDocumento]:text');
	var objcmbTipoDocumento = $('select[id=cmbTipoDocumento]');
	var objtxtRazonSocial = $('input[id=txtRazonSocial]:text');

	
	if ($("#cmbMedio").val()=="004") {

	    // strError.append(fn_util_ValidateControl(cmbMedio[0], 'un Medio de Pago válido', 1, ''));
	    strError.append(fn_util_ValidateControl(objcmbPagoComision[0], 'Pago comision', 1, ''));
	    strError.append(fn_util_ValidateControl(objcmbMonedaPend[0], 'Moneda', 1, ''));
	    strError.append(fn_util_ValidateControl(objtxtPendiente[0], 'Pendiente', 1, ''));
	    strError.append(fn_util_ValidateControl(objtxtNota[0], 'Nota', 1, ''));
	    strError.append(fn_util_ValidateControl(objtxtEmisora[0], 'Emisora', 1, ''));
	    strError.append(fn_util_ValidateControl(objtxtReceptora[0], 'Receptora', 1, ''));
	    strError.append(fn_util_ValidateControl(objtxtMontoComision[0], 'Monto Comision', 1, ''));
	}
	if ($("#cmbMedio").val() == "007") {
	    //Inicio IBK - AAE Si es diferencia en cambio no controlo que tengan valor
	    if ((fn_util_trim($("#hddCodigoAgrupacion").val()) != "05") && (fn_util_trim($("#hddCodigoAgrupacion").val()) != "13")) {
	        strError.append(fn_util_ValidateControl(objtxtPendiente[0], 'Pendiente', 1, ''));
	        strError.append(fn_util_ValidateControl(objcmbMonedaPend[0], 'Moneda', 1, ''));
	        strError.append(fn_util_ValidateControl(objtxtNota[0], 'Nota', 1, ''));
	        strError.append(fn_util_ValidateControl(objtxtReceptora[0], 'Receptora', 1, ''));
	        strError.append(fn_util_ValidateControl(objtxtEmisora[0], 'Emisora', 1, ''));
	    }
	    //Fin IBK

	}

	if ($("#cmbMedio").val() == "002") {
	    strError.append(fn_util_ValidateControl(objcmbMonedaCuenta[0], 'Moneda', 1, ''));
	    strError.append(fn_util_ValidateControl(objcmbTipoCuenta[0], 'Tipo Cuenta', 1, ''));
	    strError.append(fn_util_ValidateControl(objtxtNroCuenta[0], 'Numero de Cuenta', 1, ''));


	}
	if ($("#cmbMedio").val() == "003") {
	    strError.append(fn_util_ValidateControl(objtxtCuentaBancaria[0], 'Cuenta Bancaria', 1, ''));
	}

	if ($("#cmbMedio").val() == "006") {
	    strError.append(fn_util_ValidateControl(objcmbMonedaCuenta[0], 'Moneda Cuenta', 1, ''));
	    strError.append(fn_util_ValidateControl(objcmbTipoCuenta[0], 'Moneda Cuenta', 1, ''));
	    strError.append(fn_util_ValidateControl(objtxtNroDocumento[0], 'Numero Documento', 1, ''));
	    strError.append(fn_util_ValidateControl(objcmbTipoDocumento[0], 'Tipo Documento', 1, ''));
	    strError.append(fn_util_ValidateControl(objtxtRazonSocial[0], 'Razon Social', 1, ''));
	    strError.append(fn_util_ValidateControl(objtxtNroCuenta[0], 'Numero de Cuenta', 1, ''));
	    
	}


	if ($("#cmbMedio").val() == "005") {

	    strError.append(fn_util_ValidateControl(objtxtPendiente[0], 'Pendiente', 1, ''));
	    strError.append(fn_util_ValidateControl(objcmbMonedaPend[0], 'Moneda', 1, ''));
	    strError.append(fn_util_ValidateControl(objtxtNota[0], 'Nota', 1, ''));
	    strError.append(fn_util_ValidateControl(objtxtEmisora[0], 'Emisora', 1, ''));
	    strError.append(fn_util_ValidateControl(objtxtReceptora[0], 'Receptora', 1, ''));
	    strError.append(fn_util_ValidateControl(objcmbPagoComision[0], 'Pago Comision', 1, ''));
	
	}

    //strError.append(fn_util_ValidateControl(cmbMedio[0], 'un Medio de Pago válido', 1, ''));
    //strError.append(fn_util_ValidateControl(objcmbMoneda[0], 'una Moneda válida', 1, ''));
    //strError.append(fn_util_ValidateControl(objtxtMonto[0], 'un Monto válido', 1, ''));
		
			
    if (fn_util_trim(strError) != "") {
		parent.fn_unBlockUI();
        parent.fn_mdl_mensajeError(strError, function() { }, "VALIDACIÓN");
        strError = null;
    } else {
       
       	//Inicio IBK - AAE - Agrego validación para pago de agente de aduana
        if (($("#hddCodigoMedioPago").val()== strConceptoCuenta)||($("#hddCodigoMedioPago").val()== strConceptoPagoAgenteAduana)) {
        // Fin IBK

            fn_validaCuenta();
            
            var resultado = $("#hddValidaCuenta").val();
            if (resultado != '') {
                var result = resultado.split('|');
                if (result[0] != "0") {
                    parent.fn_mdl_mensajeIco(result[1], "util/images/warning.gif", "ERROR AL ENVIAR");
                    parent.fn_unBlockUI();
                    return;
                }
            } else {
                parent.fn_unBlockUI();
                return; 
            }

        }
        //Inicio IBK - AAE - Agrego códigoejecución de pago
        var arrParametros = ["pstrCodContrato",     $("#hddCodigoContrato").val(),
							"pstrCodInsDesembolso", $("#hddCodigoInsDesembolso").val(),							 
							"pstrCodAgrupacion",    $("#hddCodigoAgrupacion").val(),
							"pstrCodMoneda",        $("#hddCodigoMonedaAgrupacion").val(),
							"pstrMedio",            $("#cmbMedio").val(),
							"pstrMonedaCuenta",     $("#cmbMonedaCuenta").val(),
							"pstrTipoCuenta",       $("#cmbTipoCuenta").val(),
							"pstrNroCuenta",        $("#txtNroCuenta").val(),
							"pstrCuentaBancaria",   $("#txtCuentaBancaria").val(),
							"pstrPendiente",        $("#txtPendiente").val(),
							"pstrMonedaPend",       $("#cmbMonedaPend").val(),
							"pstrNota",             $("#txtNota").val(),
							"pstrEmisora",          $("#txtEmisora").val(),
							"pstrReceptora",        $("#txtReceptora").val(),
							"pstrNroDocumento",     $("#txtNroDocumento").val(),
							"pstrTipoDocumento",    $("#cmbTipoDocumento").val(),
							"pstrRazonSocial",      $("#txtRazonSocial").val(),
							"pstrPagoComision",     $("#cmbPagoComision").val(),
							"pstrMontoComision",    $("#txtMontoComision").val(),
							"pstrCodProveedor",     $("#hddCodProveedor").val(),
							"pstrCodEjecucionPago", $("#hddCodEstadoEjecucion").val(),
							"pstrCargoNoDom",       $("#txtCargoNoDom").val(),
							"pstrCtaCargoNoDom",    $("#txtCtaCargoNoDom").val(),
							"pstrAbonoNoDom",       $("#txtAbonoNoDom").val(),
							"pstrCtaAbonoNoDom",    $("#txtCtaAbonoNoDom").val()
							];

        fn_util_AjaxWM("frmMediosPagoRegistro.aspx/GrabaMedioPago",
				 arrParametros,
				 function(result) {
				     parent.fn_unBlockUI();
				     if (fn_util_trim(result) == "1") {
				         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL GUARDAR");
				     } else {
				         parent.fn_mdl_mensajeOk("El Cargo fué grabado correctamente", function() { fn_RedireccionGrabar(); }, "GRABADO CORRECTO");
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


function fn_validaCuenta() {

    //  CUENTA Nº 1
    if ($("#cmbTipoCuenta").val() == "01") {
        var strargFCDTIPOCUENTA = "IM";
    } else {
        strargFCDTIPOCUENTA = "ST";
    }
    if ($("#cmbMonedaCuenta").val() == "001") {
        var strargFCDCODMONEDA = "001";
    } else {
        strargFCDCODMONEDA = "010";
    }
    if ($("#cmbTipoCuenta").val() == "01") {
        var strCoCategoria = "001";
    } else {
        strCoCategoria = "002";
    }
    var NumeroCuenta = fn_util_trim($("#txtNroCuenta").val());

    if ((NumeroCuenta.length) == 13) {
        //inicio IBK - AAE - quito la validación del CU la valida el vb
        //if ($("#hddcu").val() != '') {
            /* Inicio IBK - AAE - Se corrige que si el proveedor obtenga el CU de host y si es cliente obtenga el CU de la solicitud de credito */
            /*var arrParametros3 = ["argFCDTIPOCUENTA",  strargFCDTIPOCUENTA,
                                  "argFCDCODMONEDA",   strargFCDCODMONEDA,
                                  "argFCDCODTIENDA",   NumeroCuenta.substr(0, 3),
                                  "argFCDCODCATEGORIA",strCoCategoria,
                                  "argFCDNUMCUENTA",   NumeroCuenta.substr(3, 12),
                                  "pCodUnico",         $("#hddcu").val()
                                  ];*/
            var arrParametros3 = ["argFCDTIPOCUENTA", strargFCDTIPOCUENTA,
                                  "argFCDCODMONEDA", strargFCDCODMONEDA,
                                  "argFCDCODTIENDA", NumeroCuenta.substr(0, 3),
                                  "argFCDCODCATEGORIA", strCoCategoria,
                                  "argFCDNUMCUENTA", NumeroCuenta.substr(3, 12),
                                  "pCodUnico", $("#hddcu").val(),
                                  "pstrCodAgrupacion", $("#hddCodigoAgrupacion").val(),
                                  "pstrCodProveedor", $("#hddCodProveedor").val(),
                                  "pstrCodSolicitudCredito", $("#hddCodigoContrato").val(),
                                  "pstrCodMedioPago", $("#cmbMedio").val(),
                                  "pstrTipoDocAgAd", $("#cmbTipoDocumento").val(),
                                  "pstrNroDocAgAd", $("#txtNroDocumento").val()                                  
                                  ];
                         //Fin IBK
                         fn_util_AjaxSyncWM("frmMediosPagoRegistro.aspx/ValidaCuentaST",
                         arrParametros3,
                         function(result) {
                             $("#hddValidaCuenta").val(result);
                         },
                         function(result) {
                             $("#hddValidaCuenta").val("1|" + "ERROR " + result.status + ' ' + result.statusText);
                             parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                             result(false);
                         });
       /* }else {
        //parent.fn_mdl_alert("Se grabó con éxito Check List Legal: " + pCodigo.toString(), function() { });
        parent.fn_mdl_alert("El Proveedor no tiene Codigo Unico " , function() { });
        }*/
        //fin IBK
        
    } else {
        parent.fn_mdl_alert("Ingrese una cuenta de 13 digitos", function() { });
    }

}


function fn_RedireccionGrabar() {


    var hddModal = window.parent.frames[0].document.getElementById('hddCodAgrupacionModal');
   
	hddModal.value = $("#hddCodigoGrupoHtml").val();
	var ctrlBtn = window.parent.frames[0].document.getElementById('btnActualizaGrupos');
    ctrlBtn.click();
    parent.fn_util_CierraModal();
}


//****************************************************************
// Funcion		:: 	fn_validaMedioPago
// Descripción	::	Valida Medio Pago
// Log			:: 	JRC - 04/10/2012
//****************************************************************
function fn_validaMedioPago(strValor) {

	fn_ocultaGrupos();                
    if (fn_util_trim(strValor) == strConceptoCuenta || fn_util_trim(strValor) == strConceptoPagoAgenteAduana) {
        $("#tr_grupo1").show();  
    } 
    if (fn_util_trim(strValor) == strConceptoInterbancario) {
        $("#tr_grupo2").show();
        //Inicio IBK - AAE - Muestro grupos
        $("#tr_grupo3_1").show();
        $("#tr_grupo3_2").show();
        //cargarValoresMedioPago(fn_util_trim(strValor))
        //Fin IBK
    }
    if (fn_util_trim(strValor) == strConceptoCartaCrédito || fn_util_trim(strValor) == strConceptoTransferenciaExterior || fn_util_trim(strValor) == strConceptoContable) {
        $("#tr_grupo3_1").show();
        $("#tr_grupo3_2").show();
        //Inicio IBK
        //cargarValoresMedioPago(fn_util_trim(strValor))
        //Fin IBK
    } 
    if (fn_util_trim(strValor) == strConceptoPagoAgenteAduana) {
        $("#tr_grupo4_1").show();  
        $("#tr_grupo4_2").show();  
    } 
    if (fn_util_trim(strValor) == strConceptoCartaCrédito || fn_util_trim(strValor) == strConceptoTransferenciaExterior) {
        $("#tr_grupo5").show();  
    }
    //Inicio IBK - AAE - Valido que si es diferencia en cambio grise la info de pendiente
    if ((fn_util_trim($("#hddCodigoAgrupacion").val()) == "05") || (fn_util_trim($("#hddCodigoAgrupacion").val()) == "13")) {
        $("#txtPendiente").attr('disabled', 'disabled');
        $("#txtEmisora").attr('disabled', 'disabled');
         $("#txtReceptora").attr('disabled', 'disabled');
         $('#txtNota').attr('disabled', 'disabled');
     }
     if (fn_util_trim($("#hddCodigoAgrupacion").val()) == "15") {
         $("#tr_grupo6_1").show();
         $("#tr_grupo6_2").show();
     }
     
     
    //fin IBK        
}

//****************************************************************
// Funcion		:: 	fn_validaMedioPago
// Descripción	::	Valida Medio Pago
// Log			:: 	JRC - 04/10/2012
//****************************************************************
function cargarValoresMedioPago(strValor) {
    //if ($("#txtPendiente").val() == "") {
        if (fn_util_trim(strValor) == strConceptoCartaCrédito) {
            $("#cmbMonedaPend").val($("#hddCodMonedaContrato").val());
            $("#txtPendiente").val($("#hddPendiente05").val());
            $("#txtEmisora").val($("#hddEmisora05").val());
            $("#txtReceptora").val($("#hddReceptora05").val());
        }
        if (fn_util_trim(strValor) == strConceptoTransferenciaExterior) {
            $("#cmbMonedaPend").val($("#hddCodMonedaContrato").val());
            $("#txtPendiente").val($("#hddPendiente04").val());
            $("#txtEmisora").val($("#hddEmisora04").val());
            $("#txtReceptora").val($("#hddReceptora04").val());
        }
        if (fn_util_trim(strValor) == strConceptoInterbancario) {
            $("#cmbMonedaPend").val($("#hddCodMonedaContrato").val());
            $("#txtPendiente").val($("#hddPendiente03").val());
            $("#txtEmisora").val($("#hddEmisora03").val());
            $("#txtReceptora").val($("#hddReceptora03").val());
        }
   // } 
}