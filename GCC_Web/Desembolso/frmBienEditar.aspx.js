//VARIABLES CONTANTES
var DestinoCredito_Inmueble = ["002"];
var DestinoCredito_Maquinaria = ["003", "004", "005", "006", "011"];
var DestinoCredito_Otros = ["007", "008"];


//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    //Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    //var Codclasibien = getParameter("Codclasibien");
    fn_inicializaCampos();
	fn_SeteaUbigeo();
	fn_SeteaUbigeoBien();
	fn_SeteaUbigeoOtro();
    $("#dv_datos_inmueble").hide();
    $("#dv_datos_vehiculo").hide();
    $("#dv_datos_otros").hide();

    //$("#dvContrato").val("Nº Contrato" + $("#dvContrato").val()) ;


    //$("#hddClasificacionBien").val();
    //alert($("#hddCodclasibien").val());
    //hddCodContrato
    var destinoCredito = $('#hddCodclasibien').val();

    if (DestinoCredito_Inmueble.indexOf(destinoCredito) != -1) {
        $("#dv_datos_inmueble").show();
        //ConfigurarGrillaInmueble();
    }
    // Maquinaria
    else if (DestinoCredito_Maquinaria.indexOf(destinoCredito) != -1) {
        $("#dv_datos_vehiculo").show();
        //ConfigurarGrillaMaquinaria();
    }
    // Otros
    else if (DestinoCredito_Otros.indexOf(destinoCredito) != -1) {
        $("#dv_datos_otros").show();
        //ConfigurarGrillaOtrosBienes();
    }
    fWidthCombo(0);

    //    switch ($("#hddCodclasibien").val()) {
    //        case "006":
    //            //$("#dv_datos_inmueble").show();
    //            $("#dv_datos_vehiculo").show();
    //            break;
    //        case "002":
    //            //$("#dv_datos_vehiculo").show();
    //            $("#dv_datos_inmueble").show();
    //            break;
    //        case "008":
    //            $("#dv_datos_otros").show();
    //            break;
    //    }
}
);


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa Campos
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_inicializaCampos() {

    //var strCodSolicitudCredito = $("#hddCodSolicitudCredito").val(); 
    //var strCodigoBien = $("#hddClasificacionBien").val();
    //alert(strCodigoBien);
    //fn_cargaTipoBien($("#hddCodclasibien").val());
	
//	// Maquinaria, vehiculos
	$("#txtClasificacionBien1").attr('disabled', 'disabled');
	$('#txtEstadoBien1').attr('disabled', 'disabled');
    $('#txtEstadoBien1').validText({ type: 'comment'});
	$('#txtCantidad1').attr('disabled', 'disabled');
    $('#txtCantidad1').validText({ type: 'number'});
	$('#txtDescripcionBien1').attr('disabled', 'disabled');
    $('#txtDescripcionBien1').validText({ type: 'comment'});
	$('#txtUbicacion1').attr('disabled', 'disabled');
    $('#txtUbicacion1').validText({ type: 'comment'});
	$('#txtUso1').attr('disabled', 'disabled');
    $('#txtUso1').validText({ type: 'comment'});
	$('#ddlDepartamento1').attr('disabled', 'disabled');
    $('#ddlProvincia1').attr('disabled', 'disabled');
	$('#txtRazonSocial').validText({ type: 'comment', length: 100 });
	$('#txtMonedaBien1').attr('disabled', 'disabled');
    $('#txtMonedaBien1').validText({ type: 'comment'});
	$('#txtValorBien1').validNumber({value:'',length:12,decimals:2});
	$('#txtPlacaActual').validText({ type: 'alphanumeric',length:10});
	//$('#txtPlacaAnterior').attr('disabled', 'disabled');
    $('#txtPlacaAnterior').validText({ type: 'alphanumeric',length:10});
	$('#txtFechaTransferencia1').validText({ type: 'date',length:10});
	$('#txtAnio').validText({ type: 'number',length:4});
	$('#txtNroSerie').attr('disabled', 'disabled');
    $('#txtNroSerie').validText({ type: 'alphanumeric'});
	$('#txtNrMotor').validText({ type: 'alphanumeric',length:20});
	$('#txtMarca').attr('disabled', 'disabled');
    $('#txtMarca').validText({ type: 'comment',length:20});
	$('#txtModelo').attr('disabled', 'disabled');
    $('#txtModelo').validText({ type: 'comment',length:20});
	$('#txtColor1').validText({ type: 'comment',length:50});
	$('#txtCarroceria').validText({ type: 'comment',length:20});
	$('#txtMedidas').validText({ type: 'comment',length:100});
	$('#txtObservaciones1').validText({ type: 'comment',length:500});
	$("#ddlMonedaBien1").attr('disabled', 'disabled');
	
////======================================================================================	
	// Bien
	$("#txtClasificacionBien").attr('disabled', 'disabled');
	$('#txtDescripcionDemanda').attr('disabled', 'disabled');
    $('#txtDescripcionDemanda').validText({ type: 'comment'});
	$('#txtUbicacion').attr('disabled', 'disabled');
    $('#txtUbicacion').validText({ type: 'comment'});
	$('#txtDescripcionBien').attr('disabled', 'disabled');
    $('#txtDescripcionBien').validText({ type: 'comment'});
	$('#txtUso').attr('disabled', 'disabled');
    $('#txtUso').validText({ type: 'comment'});
	$('#txtEstadoBien').attr('disabled', 'disabled');
    $('#txtEstadoBien').validText({ type: 'comment'});
	$('#txtCantidad').attr('disabled', 'disabled');
    $('#txtCantidad').validText({ type: 'number'});
	$('#ddlDepartamento').attr('disabled', 'disabled');
    $('#ddlProvincia').attr('disabled', 'disabled');
	$('#txtMonedaBien').attr('disabled', 'disabled');
    $('#txtMonedaBien').validText({ type: 'comment'});
	$('#txtValorBien').validNumber({value:'',length:12,decimals:2});
	$('#txtFechaTransferencia').validText({ type: 'date',length:10});
	$('#txtColor').validText({ type: 'comment',length:50});
	$('#txtPartidaRegistral').validText({ type: 'number',length:10});
	$('#txtOficinaRegistral').validText({ type: 'number',length:50});
	$('#txtObservaciones').validText({ type: 'comment',length:500});
	$("#ddlMonedaBien").attr('disabled', 'disabled');
	
// ======================otros==========================================	
	$("#txtClasificacionBien0").attr('disabled', 'disabled');
	$('#txtCantidad2').attr('disabled', 'disabled');
    $('#txtCantidad2').validText({ type: 'number'});
	$('#txtEstadoBien2').attr('disabled', 'disabled');
    $('#txtEstadoBien2').validText({ type: 'comment'});
	$('#txtDescripcionBien2').attr('disabled', 'disabled');
    $('#txtDescripcionBien2').validText({ type: 'comment'});
	$('#txtUbicacion2').attr('disabled', 'disabled');
    $('#txtUbicacion2').validText({ type: 'comment'});
	$('#txtUso2').attr('disabled', 'disabled');
    $('#txtUso2').validText({ type: 'comment'});
	$('#ddlDepartamento2').attr('disabled', 'disabled');
    $('#ddlProvincia2').attr('disabled', 'disabled');
	$('#txtMonedaBien2').attr('disabled', 'disabled');
    $('#txtMonedaBien2').validText({ type: 'comment'});
	$('#txtValorBien2').validNumber({value:'',length:12,decimals:2});
	$('#txtFechaTransferencia2').validText({ type: 'date',length:10});
	$('#txtMarca2').attr('disabled', 'disabled');
    $('#txtMarca2').validText({ type: 'comment',length:20});
	$('#txtModelo2').attr('disabled', 'disabled');
    $('#txtModelo2').validText({ type: 'comment',length:20});
	$('#txtColor2').validText({ type: 'comment',length:50});
	$('#txtPartidaRegistral2').validText({ type: 'number',length:10});
	$('#txtOficinaRegistral2').validText({ type: 'number',length:50});
	$('#txtObservaciones2').validText({ type: 'comment',lenght:500});
	$("#ddlMonedaBien2").attr('disabled', 'disabled');

}
//****************************************************************
// Funcion		:: 	fWidthCombo
// Descripción	::	Aumenta o disminuye tamaño combo
// Log			:: 	WCR - 27/07/2012
//****************************************************************
function fWidthCombo(pInd) {
    var destinoCredito = $('#hddCodclasibien').val();

    $('#cmbTipoBien').hide();
    $('#cmbTipoBien1').hide();
    $('#cmbTipoBien2').hide();
    if (DestinoCredito_Inmueble.indexOf(destinoCredito) != -1) {
        $('#cmbTipoBien').show();
        if (pInd == 0) { $('#cmbTipoBien').width(100); }
        else { $('#cmbTipoBien').width(250); }
    }
    // Maquinaria
    else if (DestinoCredito_Maquinaria.indexOf(destinoCredito) != -1) {
        $('#cmbTipoBien1').show();
        if (pInd == 0) { $('#cmbTipoBien1').width(100); }
        else { $('#cmbTipoBien1').width(250); }
    }
    // Otros
    else if (DestinoCredito_Otros.indexOf(destinoCredito) != -1) {
        $('#cmbTipoBien2').show();
        if (pInd == 0) { $('#cmbTipoBien2').width(100); }
        else { $('#cmbTipoBien2').width(250); }
    }




}

//****************************************************************
// Funcion		:: 	fn_cargaTipoBien
// Descripción	::	Carga Combo TipoBien
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaTipoBien(strValor) {

    var destinoCredito = $('#hddCodclasibien').val();
    var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL104", "pstrCodigoGenerico", strValor];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {

            //if (DestinoCredito_Inmueble.indexOf(destinoCredito) != -1) {
            $('#cmbTipoBien1').html(arrResultado[1]);
            //}  else if (DestinoCredito_Maquinaria.indexOf(destinoCredito) != -1) {
            $('#cmbTipoBien').html(arrResultado[1]);
            //} else if (DestinoCredito_Otros.indexOf(destinoCredito) != -1) {
            $('#cmbTipoBien2').html(arrResultado[1]);
            //}


        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}


//****************************************************************
// Funcion		:: 	fn_grabar
// Descripción	::	Grabar
// Log			:: 	WCR - 18/06/2012
//****************************************************************
function fn_grabar() {

    var strError = new StringBuilderEx();

    //fn_Validacion(strError);
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;

    }
    else {

        fn_mdl_confirma('¿Esta seguro de actualizar este registro?',
            function() {
                parent.fn_blockUI();

                var strFlag = '0';
                var vTipoBien = '';
                var vFechaTransferencia = '';
                var vColor = '';
                var vObservaciones = '';
            	var vCodDistrito = '';
            	var vValorBien = '';
            	var vPartidaRegistral = '';
            	var vOficinaRegistral = '';
            	var vPlacaActual = '';
            	var vPlacaAnterior = '';
            	var vAnioFabricacion = '';
            	var vNroMotor = '';
            	var vTipoCarroceria = '';
            	var vMedidas = '';
            	var vCodMoneda = '';

                // Inmueble
                if (DestinoCredito_Inmueble.indexOf($("#hddCodclasibien").val()) != -1) {
                    vTipoBien = $('#cmbTipoBien').val() == undefined ? "" : $('#cmbTipoBien').val();
                    vFechaTransferencia = $('#txtFechaTransferencia').val() == undefined ? "" : $('#txtFechaTransferencia').val();
                    vObservaciones = $('#txtObservaciones').val() == undefined ? "" : $('#txtObservaciones').val();
                	vColor = $('#txtColor').val() == undefined ? "" : $('#txtColor').val();
                	vCodDistrito = $('#ddlDistrito').val() == undefined ? "" : $('#ddlDistrito').val();
                	vValorBien = $('#txtValorBien').val() == undefined ? "" : $('#txtValorBien').val();
                	vPartidaRegistral = $('#txtPartidaRegistral').val() == undefined ? "" : $('#txtPartidaRegistral').val();
                	vOficinaRegistral = $('#txtOficinaRegistral').val() == undefined ? "" : $('#txtOficinaRegistral').val();
                	vFechaTransferencia = $('#txtFechaTransferencia').val() == undefined ? "" : $('#txtFechaTransferencia').val();
                	vCodMoneda = $('#ddlMonedaBien').val() == undefined ? "" : $('#ddlMonedaBien').val();
                }
                // Maquinaria
                else if (DestinoCredito_Maquinaria.indexOf($("#hddCodclasibien").val()) != -1) {
                    vTipoBien = $('#cmbTipoBien1').val() == undefined ? "" : fn_util_trim($('#cmbTipoBien1').val());
                    vFechaTransferencia = $('#txtFechaTransferencia1').val() == undefined ? "" : $('#txtFechaTransferencia1').val();
                    vObservaciones = $('#txtObservaciones1').val() == undefined ? "" : $('#txtObservaciones1').val();
                    vColor = $('#txtColor1').val() == undefined ? "" : $('#txtColor1').val();
                	vCodDistrito = $('#ddlDistrito1').val() == undefined ? "" : $('#ddlDistrito1').val();
                	vValorBien = $('#txtValorBien1').val() == undefined ? "" : $('#txtValorBien1').val();
                	vPlacaActual = $('#txtPlacaActual').val() == undefined ? "" : $('#txtPlacaActual').val();
                	vPlacaAnterior = $('#txtPlacaAnterior').val() == undefined ? "" : $('#txtPlacaAnterior').val();
                	vAnioFabricacion=$('#txtAnio').val() == "" ? 0 : $('#txtAnio').val();
                	vNroMotor=$('#txtNrMotor').val() == undefined ? "" : $('#txtNrMotor').val();
                	vTipoCarroceria=$('#txtCarroceria').val() == undefined ? "" : $('#txtCarroceria').val();
                	vMedidas=$('#txtMedidas').val() == undefined ? "" : $('#txtMedidas').val();
                	vCodMoneda = $('#ddlMonedaBien1').val() == undefined ? "" : $('#ddlMonedaBien1').val();
                    strFlag = '1';
                }
                // Otros
                else if (DestinoCredito_Otros.indexOf($("#hddCodclasibien").val()) != -1) {
                    vTipoBien = $('#cmbTipoBien2').val() == undefined ? "" : $('#cmbTipoBien2').val();
                    vFechaTransferencia = $('#txtFechaTransferencia2').val() == undefined ? "" : $('#txtFechaTransferencia2').val();
                    vObservaciones = $('#txtObservaciones2').val() == undefined ? "" : $('#txtObservaciones2').val();
                	vColor = $('#txtColor2').val() == undefined ? "" : $('#txtColor2').val();
                	vCodDistrito = $('#ddlDistrito2').val() == undefined ? "" : $('#ddlDistrito2').val();
                	vValorBien = $('#txtValorBien2').val() == undefined ? "" : $('#txtValorBien2').val();
                	vPartidaRegistral = $('#txtPartidaRegistral2').val() == undefined ? "" : $('#txtPartidaRegistral2').val();
                	vOficinaRegistral = $('#txtOficinaRegistral2').val() == undefined ? "" : $('#txtOficinaRegistral2').val();
                    vCodMoneda = $('#ddlMonedaBien2').val() == undefined ? "" : $('#ddlMonedaBien2').val();

                }
                if (vFechaTransferencia == '') { vFechaTransferencia = '19000101'; }
                else { vFechaTransferencia = Fn_util_DateToString(vFechaTransferencia); }

                var vNumeroContrato = $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();
                var vSecFinanciamiento = $('#hddSecFinanciamiento').val() == undefined ? "" : $('#hddSecFinanciamiento').val();

                if (DestinoCredito_Inmueble.indexOf($("#hddCodclasibien").val()) != -1) {
                var arrParametros = ["pNumeroContrato", vNumeroContrato,
                                         "pSecFinanciamiento", vSecFinanciamiento,
                                         "pCodigoTipoBien", vTipoBien,
                                         "pFechaTransferencia", vFechaTransferencia,
                                         "pObservaciones", vObservaciones,
                                         "pColor", vColor,
                	 		             "pCodDistrito",vCodDistrito,
                	 		             "pValorBien",fn_util_ValidaDecimal(vValorBien),
                	 		             "pPartidaRegistral",vPartidaRegistral,
                	 		             "pOficinaRegistral",vOficinaRegistral,
                	 		             "pCodMoneda",vCodMoneda,
                	 		             "pFlag", strFlag
                                    ];

                fn_util_AjaxWM("frmBienEditar.aspx/GuardarBien",
                     arrParametros,
                     fn_MensajeYRedireccionar,
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
                
                }else if (DestinoCredito_Maquinaria.indexOf($("#hddCodclasibien").val()) != -1)  {
                var arrParametros2 = ["pNumeroContrato", vNumeroContrato,
                                         "pSecFinanciamiento", vSecFinanciamiento,
                                         "pCodigoTipoBien", vTipoBien,
                                         "pFechaTransferencia", vFechaTransferencia,
                                         "pObservaciones", vObservaciones,
                                         "pColor", vColor,
                	 		             "pCodDistrito",vCodDistrito,
                	 		             "pValorBien",fn_util_ValidaDecimal(vValorBien),
                		                 "pPlacaActual",vPlacaActual,
                	                     "pPlacaAnterior",vPlacaAnterior,
                		                 "pAnioFabricacion",vAnioFabricacion,
                		                 "pNroMotor",vNroMotor,
                		                 "pCarroceria",vTipoCarroceria,
                		                 "pMedidas",vMedidas,
                		                 "pCodMoneda",vCodMoneda,
                	 		             "pFlag", strFlag
                                    ];

                fn_util_AjaxWM("frmBienEditar.aspx/GuardarMaquinaria",
                     arrParametros2,
                     fn_MensajeYRedireccionar,
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
                
                }else if (DestinoCredito_Otros.indexOf($("#hddCodclasibien").val()) != -1){
                
                var arrParametros3 = ["pNumeroContrato", vNumeroContrato,
                                         "pSecFinanciamiento", vSecFinanciamiento,
                                         "pCodigoTipoBien", vTipoBien,
                                         "pFechaTransferencia", vFechaTransferencia,
                                         "pObservaciones", vObservaciones,
                                         "pColor", vColor,
                	 		             "pCodDistrito",vCodDistrito,
                	 		             "pValorBien",fn_util_ValidaDecimal(vValorBien),
                	 		             "pPartidaRegistral",vPartidaRegistral,
                	 		             "pOficinaRegistral",vOficinaRegistral,
                	 		             "pCodMoneda",vCodMoneda,
                	 		             "pFlag", strFlag
                                    ];

                fn_util_AjaxWM("frmBienEditar.aspx/GuardarBien",
                     arrParametros3,
                     fn_MensajeYRedireccionar,
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
                }
           },
            "../util/images/question.gif",
            function() { },
            'Mantenimiento Bien'
         );
    }
}



//****************************************************************
// Función		:: 	fn_MensajeYRedireccionarSolicitud
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
//                  Luego lo redirecciona al formulario de búsquedas ("frmMantenimientoBienListado.aspx").
// Log			:: 	WCR - 18/06/2012
//****************************************************************
var fn_MensajeYRedireccionar = function() {	
    parent.fn_unBlockUI();   
    parent.fn_mdl_alert('Los datos se grabaron satisfactoriamente', function() {		
        fn_ActualizaListaBienes();
    });
};

function fn_ActualizaListaBienes() {
	var ctrlBtn = window.parent.frames[1].document.getElementById('btnCargaBienes');
	ctrlBtn.click();
	parent.fn_util_CierraModal2();
}



//****************************************************************
// Funcion		:: 	fn_SeteaUbigeo
// Descripción	::	Setear Ubigeo
// Log			:: 	AEP - 17/07/2012
//****************************************************************
function fn_SeteaUbigeo() {
    //Carga Departamento
    var strDepartamento1 = $("#hidCodDepartamento1").val();
    $("#ddlDepartamento1").val(strDepartamento1);

    //Carga Provincia
    fn_cargaComboProvincia(strDepartamento1);
     strProvincia1 = $("#hidCodProvincia1").val();
    $("#ddlProvincia1").val(strProvincia1);

    //Carga Distrito
    fn_cargaComboDistrito(strDepartamento1, strProvincia1);
    $("#ddlDistrito1").val($("#hidCodDistrito1").val());
}


//****************************************************************
// Funcion		:: 	fn_SeteaUbigeoBien
// Descripción	::	Setear Ubigeo del tipo de bien "Bien"
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_SeteaUbigeoBien() {
    //Carga Departamento
    var strDepartamento = $("#hidCodDepartamento").val();
    $("#ddlDepartamento").val(strDepartamento);

    //Carga Provincia
    fn_cargaComboProvinciaBien(strDepartamento);
    strProvincia = $("#hidCodProvincia").val();
    $("#ddlProvincia").val(strProvincia);

    //Carga Distrito
    fn_cargaComboDistritoBien(strDepartamento, strProvincia);
    $("#ddlDistrito").val($("#hidCodDistrito").val());
}

//****************************************************************
// Funcion		:: 	fn_SeteaUbigeoOtro
// Descripción	::	Setear Ubigeo del tipo de bien "otros"
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_SeteaUbigeoOtro() {
    //Carga Departamento
    var strDepartamento2 = $("#hidCodDepartamento2").val();
    $("#ddlDepartamento2").val(strDepartamento2);

    //Carga Provincia
    fn_cargaComboProvinciaOtro(strDepartamento2);
    strProvincia2 = $("#hidCodProvincia2").val();
    $("#ddlProvincia2").val(strProvincia2);

    //Carga Distrito
    fn_cargaComboDistritoOtro(strDepartamento2, strProvincia2);
    $("#ddlDistrito2").val($("#hidCodDistrito2").val());
}
//****************************************************************
// Funcion		:: 	fn_cargaComboDistrito
// Descripción	::	
// Log			:: 	AEP - 17/07/2012
//****************************************************************
function fn_cargaComboDistrito(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistrito1').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}

//****************************************************************
// Funcion		:: 	fn_cargaComboDistritoBien
// Descripción	::	
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_cargaComboDistritoBien(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistrito').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}
//****************************************************************
// Funcion		:: 	fn_cargaComboDistritoOtro
// Descripción	::	
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_cargaComboDistritoOtro(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistrito2').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistrito
// Descripción	::	
// Log			:: 	AEP - 17/07/2012
//****************************************************************
function fn_LimpiaComboDistrito() {
    $('#ddlDistrito1').empty();
    $("#ddlDistrito1").append('<option value="0">[-Seleccione-]</option>');
}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistritoBien
// Descripción	::	
// Log			:: 	AEP - 18/07/2012

//****************************************************************

function fn_LimpiaComboDistritoBien() {
    $('#ddlDistrito').empty();
    $("#ddlDistrito").append('<option value="0">[-Seleccione-]</option>');
}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistritoOtro
// Descripción	::	
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_LimpiaComboDistritoOtro() {
    $('#ddlDistrito2').empty();
    $("#ddlDistrito2").append('<option value="0">[-Seleccione-]</option>');

}
//****************************************************************
// Funcion		:: 	fn_cargaComboProvincia
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 17/07/2012
//****************************************************************
function fn_cargaComboProvincia(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvincia1').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistrito();

}

//****************************************************************
// Funcion		:: 	fn_cargaComboProvinciaBien
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_cargaComboProvinciaBien(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvincia').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoBien();

}

//****************************************************************
// Funcion		:: 	fn_cargaComboProvinciaOtro
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_cargaComboProvinciaOtro(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvincia2').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoOtro();

}