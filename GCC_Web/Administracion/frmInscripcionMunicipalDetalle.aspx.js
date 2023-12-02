
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	AEP - 24/09/2012
//****************************************************************
$(document).ready(function() {
fn_doResize();

	fn_SetearCamposObligatorios();    
	
	   fn_onLoadPage();
	
	$('#txtPartidaRegistral').validText({ type: 'number',length:8});
});

function fn_SetearCamposObligatorios() {
	fn_util_SeteaObligatorio($("#txtPartidaRegistral"), "text");
	fn_util_SeteaObligatorio($("#txtAsientoRegitral"), "text");
	fn_util_SeteaObligatorio($("#txtActo"), "text");
}

//****************************************************************
// Funcion		:: 	fn_grabar
// Descripción	::	Grabar
// Log			:: 	AEP - 19/10/2012
//****************************************************************
function fn_grabarInscripcionMunicipal() {

	
	var strError = new StringBuilderEx();
            	
            	var objtxtPartida = $('input[id=txtPartidaRegistral]:text');
	            var objtxtAsiento = $('input[id=txtAsientoRegitral]:text');
	            var objtxtActo = $('input[id=txtActo]:text');
            	strError.append(fn_util_ValidateControl(objtxtPartida[0], 'partida', 1, ''));
            	strError.append(fn_util_ValidateControl(objtxtAsiento[0], 'asiento', 1, ''));
	            strError.append(fn_util_ValidateControl(objtxtActo[0], 'acto', 1, ''));
	
            	    if (strError.toString() != '') {
                    parent.fn_unBlockUI();
                    parent.fn_mdl_alert(strError.toString(), function() { });
            	    
                    strError = null;
                	return;
                    }
	
	
	        
	
	
	
	if($("#hddTipoTransaccion").val()=="NUEVO") {
		
		var arrParametroValidacion = ["strNroContrato", $("#hddCodigoContrato").val(),
		                              "strPartida", $("#txtPartidaRegistral").val(),
		                              "strAsiento", $("#txtAsientoRegitral").val(),
		                              "strActo", $("#txtActo").val(),
		                              "strTipo", "1",
	                                  "intCodigoInscripcion",$("#hddCodigoInafectacion").val()];
		
        	fn_util_AjaxSyncWM("frmInscripcionMunicipalDetalle.aspx/ValidarDatosPartida",
                       arrParametroValidacion,
                       function(result2) {
                       	$("#hidResultado").val(result2);                    	
                       },
                       function(result) {
                           parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                       	fn_SetearCamposObligatorios();
                       });
	if 	($("#hidResultado").val()!="") {
	
	parent.fn_mdl_mensajeIco("No se puede registrar porque el dato " +  $("#hidResultado").val() + " se repite", "util/images/warning.gif", "GUARDAR");
	
	}else {	
	fn_mdl_confirma('¿Esta seguro de insertar?',
            function() {
                parent.fn_blockUI();
            	
                var vCodSolicitudCredito = $("#hddCodigoContrato").val();
                var vCodigoBien= $("#hddCodigoBien").val();
                var vPartida= '';
                var vAsiento = '';
                var vActo = '';
            	
            	
            	
                    vPartida = $('#txtPartidaRegistral').val() == undefined ? "" : $('#txtPartidaRegistral').val();
                    vAsiento = $('#txtAsientoRegitral').val() == undefined ? "" : $('#txtAsientoRegitral').val();
                	vActo = $('#txtActo').val() == undefined ? "" : $('#txtActo').val();
                	
                	
                	 	var arrParametros = ["pNumeroContrato", vCodSolicitudCredito,
                                         "pCodigoBien", vCodigoBien,
                                         "pPartida", vPartida,
                                         "pAsiento",vAsiento,
                                         "pActo", vActo,
                                         "pEstado","1"];

            	fn_util_AjaxSyncWM("frmInscripcionMunicipalDetalle.aspx/GuardarInscripcionMunicipal",
            		arrParametros,
            		 function() {
            		 	fn_cargaListadoInscripcion();
            		 },
            		function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
            	fn_unBlockUI();
            	fn_doResize();
            },
            "../util/images/question.gif",
            function() { fn_SetearCamposObligatorios();},
            'Mantenimiento Bien'
         );
	}
	}else {

		var arrParametroValidacionM = ["strNroContrato", $("#hddCodigoContrato").val(),
			"strPartida", $("#txtPartidaRegistral").val(),
			"strAsiento", $("#txtAsientoRegitral").val(),
			"strActo", $("#txtActo").val(),
			"strTipo", "2",
		    "intCodigoInscripcion",$("#hddCodigoInafectacion").val()];
		
		fn_util_AjaxSyncWM("frmInscripcionMunicipalDetalle.aspx/ValidarDatosPartida",
			arrParametroValidacionM,
			function(result2) {
				$("#hidResultado").val(result2);
			},
			function(result) {
				parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
				fn_SetearCamposObligatorios();
			});
		if ($("#hidResultado").val() != "") {

			parent.fn_mdl_mensajeIco("No se puede registrar porque el dato " + $("#hidResultado").val() + " se repite", "util/images/warning.gif", "GUARDAR");

		} else {
			fn_mdl_confirma('¿Esta seguro de modificar?',
				function() {
					parent.fn_blockUI();
					
					var vCodSolicitudCredito = $("#hddCodigoContrato").val();
					var vCodigoBien = $("#hddCodigoBien").val();
					var vCodigoInscripcion = $("#hddCodigoInafectacion").val();
					var vPartida = '';
					var vAsiento = '';
					var vActo = '';            	
            	
            	
					vPartida = $('#txtPartidaRegistral').val() == undefined ? "" : $('#txtPartidaRegistral').val();
					vAsiento = $('#txtAsientoRegitral').val() == undefined ? "" : $('#txtAsientoRegitral').val();
					vActo = $('#txtActo').val() == undefined ? "" : $('#txtActo').val();


					var arrParametros = ["pCodigoInscripcion", vCodigoInscripcion,
						"pNumeroContrato", vCodSolicitudCredito,
						"pCodigoBien", vCodigoBien,
						"pPartida", vPartida,
						"pAsiento", vAsiento,
						"pActo", vActo,
						"pEstado", "1"];

					fn_util_AjaxWM("frmInscripcionMunicipalDetalle.aspx/ModificarInscripcionMunicipal",
						arrParametros,
						function() {
							fn_cargaListadoInscripcion();
						},
						function(resultado) {
							var error = eval("(" + resultado.responseText + ")");
							parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
						});
					fn_unBlockUI();
					fn_doResize();
				},
				"../util/images/question.gif",
				function() {
				},
				'Mantenimiento Bien'
			);
		}
	}
}
    

function fn_cargaListadoInscripcion() {
    var ctrlBtn = window.parent.frames[0].document.getElementById('btnCargarInscripcion');
    ctrlBtn.click();
	$("#hidResultado").val('');
	parent.fn_unBlockUI();
    parent.fn_util_CierraModal();
}

