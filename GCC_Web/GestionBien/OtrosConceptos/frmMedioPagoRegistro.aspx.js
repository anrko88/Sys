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

    //Oculta Grupos
    fn_ocultaGrupos();

    //---------------------------------
    //Valida Tipo Medio
    //---------------------------------
    $('#cmbMedio').change(function() {
        var strValor = $(this).val();
        fn_validaMedioPago(strValor);
        $("#hddCodigoMedioPago").val(strValor);

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
	
	//Cuenta corriente
	$('#txtNroCuenta').validText({ type: 'number', length: 13 });
    
    //Carta de credito
	$('#txtPendiente').validText({ type: 'comment', length: 14 });
	$('#txtEmisora').validText({ type: 'number', length: 3 });
	$('#txtReceptora').validText({ type: 'number', length: 3 });
	$('#txtNota').validText({ type: 'comment', length: 8 });
	$('#txtMontoComision').validNumber({ type: 'number', length: 10 });
	
}

//****************************************************************
// Funcion		:: 	fn_ocultaGrupos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 04/10/2012
//****************************************************************
function fn_ocultaGrupos() {
	$("#tr_grupo1").hide();  
	$("#tr_grupo2").hide();  
	$("#tr_grupo3").hide();  
}


//****************************************************************
// Funcion		:: 	fn_guardar 
// Descripción	::	Guardar Adelanto
// Log			:: 	JRC - 03/10/2012
//****************************************************************
function fn_guardar() {

}


function fn_validaCuenta() {

//    //  CUENTA Nº 1
//    if ($("#cmbTipoCuenta").val() == "01") {
//        var strargFCDTIPOCUENTA = "IM";
//    } else {
//        strargFCDTIPOCUENTA = "ST";
//    }
//    if ($("#cmbMonedaCuenta").val() == "001") {
//        var strargFCDCODMONEDA = "001";
//    } else {
//        strargFCDCODMONEDA = "010";
//    }
//    if ($("#cmbTipoCuenta").val() == "01") {
//        var strCoCategoria = "001";
//    } else {
//        strCoCategoria = "002";
//    }
//    var NumeroCuenta = fn_util_trim($("#txtNroCuenta").val());

//    if ((NumeroCuenta.length) == 13) {

//        if ($("#hddcu").val() != '') {
//            
//            var arrParametros3 = ["argFCDTIPOCUENTA",  strargFCDTIPOCUENTA,
//                                  "argFCDCODMONEDA",   strargFCDCODMONEDA,
//                                  "argFCDCODTIENDA",   NumeroCuenta.substr(0, 3),
//                                  "argFCDCODCATEGORIA",strCoCategoria,
//                                  "argFCDNUMCUENTA",   NumeroCuenta.substr(3, 12),
//                                  "pCodUnico",         $("#hddcu").val()
//                                  ];
//            
//                         fn_util_AjaxSyncWM("frmMediosPagoRegistro.aspx/ValidaCuentaST",
//                         arrParametros3,
//                         function(result) {
//                             $("#hddValidaCuenta").val(result);
//                         },
//                         function(result) {
//                             $("#hddValidaCuenta").val("1|" + "ERROR " + result.status + ' ' + result.statusText);
//                             parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
//                             result(false);
//                         });
//        }else {
//        //parent.fn_mdl_alert("Se grabó con éxito Check List Legal: " + pCodigo.toString(), function() { });
//        parent.fn_mdl_alert("El Proveedor no tiene Codigo Unico " , function() { });
//        }
//        
//    } else {
//        parent.fn_mdl_alert("Ingrese una cuenta de 13 digitos", function() { });
//    }

}


function fn_RedireccionGrabar() {

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
    if (fn_util_trim(strValor) == strConceptoContable) {
        $("#tr_grupo2").show();  
        $("#tr_grupo3").show();  
    }       
}
