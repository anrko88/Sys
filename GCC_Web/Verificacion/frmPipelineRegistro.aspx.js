var StrEstadoEnEvaluacion = "001";
var StrEstadoEnRiesgos ="002";
var StrEstadoPorDesembolsar = "003";

//***********************************************************************
// Funcion		:: 	JQUERY - Verificacion Registro Pipeline
// Descripción	::	
//					
// Log			:: 	
//***********************************************************************

var bFirstClick;


$(document).ready(function() {
    //Setea Calendario
    

    //Carga Grilla

	fn_OcultarMostrarPorcentajeDesembolso($("#cmbEstado").val());
    //Inicializa Campos
    fn_inicializaCampos();

    
    // On load Page (siempre al final)
    fn_onLoadPage();

//	$('#cmbEstado').change(function() {
//		var strValor = $(this).val();
//	if((fn_util_trim(strValor))==StrEstadoPorDesembolsar) {
//	$("#dv_desembolso").show();
//	}else {
//	$("#dv_desembolso").hide();
//	}
//	});
	
	
});



//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	
//****************************************************************
function fn_inicializaCampos() {
	
	//$("#dv_desembolso").hide();
	
    //Valida Tipo de Datos
	//fn_util_SeteaObligatorio($("#cmbEstado"),"select");
	
    $("#txtCuCliente").validText({ type: 'number', length: 10});
    $('#txtRazonsocial').validText({ type: 'comment', length: 100 });
	$('#txtAnterior').validNumber({value:'',decimals:2,length:6});
	$("#txtActual").validNumber({value:'',decimals:2,length:6});
    $("#txtsiguienteMeses").validNumber({value:'',decimals:2,length:6});
    $("#txtsiguienteAnios").validNumber({ value: '', decimals: 2, length: 6 });
    //Inicio IBK - AAE - Iniciliza campos
    $("#TxtMontoLeasing").validNumber({ value: $("#TxtMontoLeasing").val(), decimals: 2, length: 18 });
    $("#TxtMontoDesembolsado").validNumber({ value: $("#TxtMontoDesembolsado").val(), decimals: 2, length: 18 });
    $("#txtRiesgoNeto").validNumber({ value: $("#txtRiesgoNeto").val(), decimals: 2, length: 18 });
    $("#txtPrecioVenta").validNumber({ value: $("#txtPrecioVenta").val(), decimals: 2, length: 18 });      
    //Fin IBK
    
}


//****************************************************************
// Funcion		:: 	fn_cancelar
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_cancelar() {

    parent.fn_mdl_confirma('¿Está seguro de volver?',
            function() {
                fn_util_redirect('frmPipelineListado.aspx');
            },
             "Util/images/question.gif",
            function() { },
            'Pipeline - Listado'
         );
}


//****************************************************************
// Funcion		:: 	fn_grabar
// Descripción	::	Guardar
// Log			:: 	AEP - 28/08/2012
//****************************************************************
function fn_grabar() {
    
    var strError = new StringBuilderEx();
	//fn_validarRegistro(strError);
	
	    var cmbEstado= $('select[id=cmbEstado]');
	    strError.append(fn_util_ValidateControl(cmbEstado[0], 'un estado', '1', ''));

    if (strError.toString() != '') {
    	parent.fn_unBlockUI();
    	//fn_seteaCamposObligatorios();
    	parent.fn_mdl_alert(strError.toString(), function() {
    	});
    	strError = null;
    } else {
   
//                    var decPorcentajeAnterior = $("#txtAnterior").val();
//    				var decPorcentajeActual = $("#txtActual").val();
//    				var decPorcentajeMSiguiente = $("#txtsiguienteMeses").val();
//    				var decPorcentajeASiguiente = $("#txtsiguienteAnios").val();
//    				var decTotalPorcentaje;

//    				decTotalPorcentaje = fn_util_ValidaDecimal(decPorcentajeAnterior) + fn_util_ValidaDecimal(decPorcentajeActual) + fn_util_ValidaDecimal(decPorcentajeMSiguiente) + fn_util_ValidaDecimal(decPorcentajeASiguiente);
    				
//    				if (decTotalPorcentaje > 100) {
//    				parent.fn_mdl_mensajeIco("La suma de los porcentajes de desembolso no puede superar el 100%", "util/images/warning.gif", "PIPELINE");
//    					parent.fn_unBlockUI();
//    					return;
//    				}
    	
    	fn_mdl_confirma('¿Está seguro de guardar?',
    			function() {

    				parent.fn_blockUI();

    				
    				var strMotivo = $("#cmbMotivoDemora").val();
    				if (strMotivo == 0 || strMotivo == null) {
    					strMotivo = "00";
    				}

    				var strEstado = $("#cmbEstado").val();
    				if (strEstado == 0 || strEstado == null) {
    					strEstado = "00";
    				}

    				

						// Inicio IBK - AAE - 22/10/2012 - Se agrega el comentario al momento de salvar
            // comento código original
    				/*
    				var arrParametros = ["strCodigoCotizacion", $("#txtNroCotizacion").val(),
    					"strCodigoContrato", $('#txtNumeroContrato').val(),
    					"strCodigoMotivo", strMotivo,
    					"strCodigoEstado", strEstado,
    					"strPorcentajeAnterior", $("#txtAnterior").val(),
    					"strPorcentajeMesActual", $("#txtActual").val(),
    					"strPorcentajeMesSiguiente", $("#txtsiguienteMeses").val(),
    					"strPorcentajeAnioSiguiente", $("#txtsiguienteAnios").val()
    					
    				];*/
    				var arrParametros = ["strCodigoCotizacion", $("#txtNroCotizacion").val(),
    					"strCodigoContrato", $('#txtNumeroContrato').val(),
    					"strCodigoMotivo", strMotivo,
    					"strCodigoEstado", strEstado,
    					"strPorcentajeAnterior", $("#txtAnterior").val(),
    					"strPorcentajeMesActual", $("#txtActual").val(),
    					"strPorcentajeMesSiguiente", $("#txtsiguienteMeses").val(),
    					"strPorcentajeAnioSiguiente", $("#txtsiguienteAnios").val(),
    					"strComentario", $("#TxtComentario").val()

    				];
    				//Fin IBK - AAE

    				fn_util_AjaxWM("frmPipelineRegistro.aspx/GuardarPipeline",
    					arrParametros,
    					fn_MensajeYRedireccionar,
    					function(resultado) {
    						var error = eval("(" + resultado.responseText + ")");
    						parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN EL REGISTRO");
    					});
    			},
    			"../util/images/question.gif",
    			function() {
    			},
    			'Pipeline'
    		);
    }
}


/****************************************************************
Funcion		:: 	fn_MensajeYRedireccionar
Descripción	::	Mensaje
Log			:: 	AEP - 28/08/2012
**************************************************************** */
var fn_MensajeYRedireccionar = function() {
    parent.fn_unBlockUI();
    parent.fn_mdl_alert('Los datos se grabaron satisfactoriamente', function() { fn_util_redirect("frmPipelineListado.aspx"); });
};


/****************************************************************
Funcion		:: 	fn_OcultarMostrarPorcentajeDesembolso
Descripción	::	Mensaje
Log			:: 	AEP - 29/08/2012
**************************************************************** */

function fn_OcultarMostrarPorcentajeDesembolso(valor) {
		
	if((fn_util_trim(valor))==StrEstadoPorDesembolsar) {
	$("#dv_desembolso").show();
	}else {
	$("#dv_desembolso").hide();
		$("#txtAnterior").val('');
		$("#txtActual").val('');
		$("#txtsiguienteMeses").val('');
		$("#txtsiguienteAnios").val('');
	}
	
}
