//****************************************************************
// Variables Globales
//****************************************************************
var C_TX_NUEVO = "NUEVO"
var C_TX_EDITAR = "EDITAR"
var C_APLICFONDO_ABONOCUENTA = "003";

var C_GESTIONBIEN_SINIESTRO = "001";        

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	??? - 02/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();
    
    //Fechas
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));
    
    //Desactiva Fecha    
    $('#txtFechaSituacion').removeClass('css_calendario');
    $("#txtFechaSituacion").attr('disabled', 'disabled');
    $('#txtFechaSituacion').addClass('css_input_inactivo');
    
        	
    //***********************************    	
    //Valida Modo Ver
    //***********************************
    var strVer = $("#hddVer").val();   
    if( fn_util_trim(strVer) == "1" ){
		fn_util_bloquearFormulario();
		$("#sp_accion").html("Ver");
		$("#dv_guardar").hide();
    }
    
    //***********************************
    //Valida Aplica Fondo    
    //***********************************
    $('#cmbAplicaFondo').change(function() {
        var strValor = $(this).val();
        
        $("#txtNroCuenta").val("");
		$("#cmbTipoCuenta").val("0");
		$("#cmbMonedaCuenta").val("0");
	
        fn_validaAplicFondo(strValor);        
    });	
        	
    //On load Page (siempre al final)
    fn_onLoadPage();
    
});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 13/11/2012
//****************************************************************
function fn_inicializaCampos() {

	fn_util_inactivaInput("txtNroSiniestro", "I");	
	if( fn_util_trim($("#hddTipoTx").val()) == C_TX_NUEVO ) {
		fn_util_inactivaInput("txtNroSiniestro", "I");	
		$("#tr_cuenta").hide();
		$("#txtMontoIndem").validNumber({ value: '0' });
		$("#dv_documentos").hide();
		$("#dv_separador").hide();
	}else{
		fn_validaAplicFondo($('#cmbAplicaFondo').val());
		$("#txtMontoIndem").validNumber({ value: $("#txtMontoIndem").val() });
	}

	$('#txtChequeAseg').validText({ type: 'comment', length: 20 });
	$('#txtNroPoliza').validText({ type: 'comment', length: 20 });	
	$('#txtNroCuenta').validText({ type: 'number', length: 13 });
	
}


//****************************************************************
// Funcion		:: 	fn_guardar
// Descripción	::	Guarda
// Log			:: 	JRC - 13/11/2012
//****************************************************************
function fn_guardar() {
	parent.fn_blockUI();

    //String Validación
    var strError = new StringBuilderEx();

    //Instancia Validaciones    		
    //txtNroSiniestro
	var objtxtFechaConoBanco = $('input[id=txtFechaConoBanco]:text');
	var objtxtFechaConoLeasing = $('input[id=txtFechaConoLeasing]:text');
	var objtxtFechaSiniestro = $('input[id=txtFechaSiniestro]:text');
	var objcmbTipo = $('select[id=cmbTipo]');
	var objtxtFechaSituacion = $('input[id=txtFechaSituacion]:text');
	var objcmbSituacion = $('select[id=cmbSituacion]');
	var objcmbContrato = $('select[id=cmbContrato]');
	var objtxtFechaAplicacion = $('input[id=txtFechaAplicacion]:text');
	var objcmbAplicacion = $('select[id=cmbAplicacion]');
	//txtFechaDescargoMunicipal
	//txtFechaTransparencia
	//cmbTransferencia
	//cmbOrigenCono
	var objcmbSeguro = $('select[id=cmbSeguro]');
	//txtChequeAseg
	var objcmbEstadoBien = $('select[id=cmbEstadoBien]');
	var objtxtNroPoliza = $('input[id=txtNroPoliza]:text');
	var objcmbTipoPoliza = $('select[id=cmbTipoPoliza]');
	//txtFechaIndem
	//cmbMonedaIndem
	//txtMontoIndem
	//cmbBancoEmite
	//var objcmbAplicaFondo = $('select[id=cmbAplicaFondo]');
	var objtxtNroCuenta = $('input[id=txtNroCuenta]:text');
	var objcmbTipoCuenta = $('select[id=cmbTipoCuenta]');
	var objcmbMonedaCuenta = $('select[id=cmbMonedaCuenta]');
	
	var objcmbTransferencia = $('select[id=cmbTransferencia]');
	var objcmbOrigenCono = $('select[id=cmbOrigenCono]');
	

    //Valida
	strError.append(fn_util_ValidateControl(objtxtFechaConoBanco[0], 'una Fecha Conocimiento Banco válida', 1, ''));
	strError.append(fn_util_ValidateControl(objtxtFechaConoLeasing[0], 'una Fecha Conocimiento Leasing válida', 1, ''));
	//strError.append(fn_util_ValidateControl(objtxtFechaSiniestro[0], 'una Fecha de Siniestro válida', 1, ''));
	//strError.append(fn_util_ValidateControl(objcmbTipo[0], 'un Tipo válido', 1, ''));
	strError.append(fn_util_ValidateControl(objtxtFechaSituacion[0], 'una Fecha Situación válida', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbSituacion[0], 'un Tipo Situación válido', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbContrato[0], 'un Contrato válido', 1, ''));
	//strError.append(fn_util_ValidateControl(objtxtFechaAplicacion[0], 'una Fecha de Aplicación válida', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbAplicacion[0], 'una Aplicación válida', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbSeguro[0], 'un Seguro válido', 1, ''));
	//strError.append(fn_util_ValidateControl(objcmbEstadoBien[0], 'un Estado de Bien válido', 1, ''));
	strError.append(fn_util_ValidateControl(objtxtNroPoliza[0], 'un Nro. Póliza válido', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbTipoPoliza[0], 'un Tipo de Poliza válido', 1, ''));
	//strError.append(fn_util_ValidateControl(objcmbAplicaFondo[0], 'una Aplicación de Fondo válida', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbTransferencia[0], 'una Transferencia del Bien válida', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbOrigenCono[0], 'un Origen Conocimiento válido', 1, ''));
	
	
	
	if( fn_util_trim($('#cmbAplicaFondo').val()) == C_APLICFONDO_ABONOCUENTA ){  
		strError.append(fn_util_ValidateControl(objtxtNroCuenta[0], 'un Nro. Cuenta válido', 1, ''));
		strError.append(fn_util_ValidateControl(objcmbTipoCuenta[0], 'un Tipo de Cuenta válido', 1, ''));
		strError.append(fn_util_ValidateControl(objcmbMonedaCuenta[0], 'una Moneda de Cuenta válida', 1, ''));
	}

	//Setea Icono Calendario en caso se quiten
	fn_util_SeteaCalendario($('input[id*=txtFecha]'));
	
	//Valida error existente
	if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }else{
    


			var arrParametros = [     
								"pstrTipoTx", $("#hddTipoTx").val(),    
								"pstrCodContrato", $("#hddCodContrato").val(), 
								"pstrCodBien", $("#hddCodBien").val(),    
								"pstrCodSiniestro", $("#hddCodSiniestro").val(),    								
								"pstrNroSiniestro", $("#txtNroSiniestro").val(),
								"pstrFechaConoBanco", Fn_util_DateToString($("#txtFechaConoBanco").val()),
								"pstrFechaConoLeasing", Fn_util_DateToString($("#txtFechaConoLeasing").val()),
								"pstrFechaSiniestro", Fn_util_DateToString($("#txtFechaSiniestro").val()),
								"pstrTipo", $("#cmbTipo").val(),
								"pstrFechaSituacion", Fn_util_DateToString($("#txtFechaSituacion").val()),
								"pstrSituacion", $("#cmbSituacion").val(),
								"pstrContrato", $("#cmbContrato").val(),
								"pstrFechaAplicacion", Fn_util_DateToString($("#txtFechaAplicacion").val()),
								"pstrAplicacion", $("#cmbAplicacion").val(),
								"pstrFechaDescargoMunicipal", Fn_util_DateToString($("#txtFechaDescargoMunicipal").val()),
								"pstrFechaTransparencia", Fn_util_DateToString($("#txtFechaTransparencia").val()),
								"pstrTransferencia", $("#cmbTransferencia").val(),
								"pstrOrigenCono", $("#cmbOrigenCono").val(),
								"pstrSeguro", $("#cmbSeguro").val(),
								"pstrChequeAseg", $("#txtChequeAseg").val(),
								"pstrEstadoBien", $("#cmbEstadoBien").val(),
								"pstrNroPoliza", $("#txtNroPoliza").val(),
								"pstrTipoPoliza", $("#cmbTipoPoliza").val(),
								"pstrFechaIndem", Fn_util_DateToString($("#txtFechaIndem").val()),
								"pstrMonedaIndem", $("#cmbMonedaIndem").val(),
								"pstrMontoIndem", $("#txtMontoIndem").val(),
								"pstrBancoEmite", $("#cmbBancoEmite").val(),
								"pstrAplicaFondo", $("#cmbAplicaFondo").val(),
								"pstrNroCuenta", $("#txtNroCuenta").val(),
								"pstrTipoCuenta", $("#cmbTipoCuenta").val(),
								"pstrMonedaCuenta", $("#cmbMonedaCuenta").val(),
								"pstrCodUnico", $("#hddCodUnico").val()								    
								];
        
            
        fn_util_AjaxWM("frmSiniestroMnt.aspx/GrabaSiniestro",
                 arrParametros,
                 function(result) {
                     parent.fn_unBlockUI();
                     if (fn_util_trim(result) == "0") {
                         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL GUARDAR");
                     } else {                         
                         if ($("#hddTipoTx").val() == C_TX_NUEVO) {
                             parent.fn_mdl_mensajeOk("Se grabó el Siniestro correctamente.", function() { fn_RedireccionGrabar() }, "GRABADO CORRECTO");
                         } else {
                             parent.fn_mdl_mensajeOk("Se actualizó correctamente los datos del Siniestro", function() { fn_RedireccionGrabar() }, "GRABADO CORRECTO");
                         }
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
	var ctrlBtn = window.parent.frames[0].document.getElementById('btnBuscar');
    ctrlBtn.click();
    parent.fn_util_CierraModal();
}
 


//****************************************************************
// Funcion		:: 	fn_validaAplicFondo
// Descripción	::	valida Aplicacion Fondos
// Log			:: 	JRC - 13/11/2012
//****************************************************************
function fn_validaAplicFondo(strValor) {	
	if( fn_util_trim(strValor) == C_APLICFONDO_ABONOCUENTA ){
		$("#tr_cuenta").show();
    }else{
		$("#tr_cuenta").hide();
    }	
}


//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos() {	
	var pstrCodContrato = fn_util_trim($("#hddCodContrato").val());
	var pstrCodBien = fn_util_trim($("#hddCodBien").val());
	var pstrCodRelacionado = fn_util_trim($("#hddCodSiniestro").val());
	var pstrCodTipo = C_GESTIONBIEN_SINIESTRO;
	var pstrVer = fn_util_trim($("#hddVer").val());
	parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + "&hddVer=" + pstrVer, 800, 350, function() { });	
}


