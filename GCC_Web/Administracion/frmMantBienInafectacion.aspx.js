
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	AEP - 24/09/2012
//****************************************************************
$(document).ready(function() {
fn_doResize();
       
	fn_InicializarCampos();
    fn_onLoadPage();
});

function fn_InicializarCampos() {
   fn_util_SeteaCalendario($("#txtFecEnvioCarta"));	
   fn_util_SeteaCalendario($("#txtFecRecepDocumentos"));
   fn_util_SeteaCalendario($("#txtFecPresentacionSat"));
   fn_util_SeteaObligatorio($("#txtPeriodo"),"text");
	$("#txtNroResolucion").attr('disabled', 'disabled');
	$("#ddlEstado").attr('disabled', 'disabled');	
	$('#txtPeriodo').validText({ type: 'number',length:4});
	$('#txtNroResolucion').validText({ type: 'comment',length:20});
   fn_util_SeteaCalendarioFunction($("#txtFecNotificacion"),function() { fn_ValidarFechaNotificacion();});
}

//****************************************************************
// Funcion		:: 	fn_ValidarFechaNotificacion
// Descripción	::	Valdacion
// Log			:: 	AEP - 22/10/2012
//****************************************************************

function fn_ValidarFechaNotificacion() {
	if($("#txtFecNotificacion").val()!='') {
	$("#txtNroResolucion").attr('disabled', false);
	$("#ddlEstado").attr('disabled', false);	
	}else {
	$("#txtNroResolucion").attr('disabled', 'disabled');
	$("#ddlEstado").attr('disabled', 'disabled');
		$("#txtNroResolucion").val('');
		$("#ddlEstado").val(0);
	}
}


//****************************************************************
// Funcion		:: 	fn_grabar
// Descripción	::	Grabar
// Log			:: 	AEP - 18/10/2012
//****************************************************************

function fn_grabarInafectacion() {

        parent.fn_unBlockUI();
	
	    var strError = new StringBuilderEx();
            	
        var objtxtPeriodo = $('input[id=txtPeriodo]:text');
        strError.append(fn_util_ValidateControl(objtxtPeriodo[0], 'periodo', 1, ''));

        if (strError.toString() != '') 
        {
            parent.fn_unBlockUI();
            parent.fn_mdl_alert(strError.toString(), function() { });
            strError = null;
           	return;
        }


        //Inicio IBK - RPH
        //debugger;
        var actual= new Date();
        var anio = actual.getUTCFullYear();
        var sPerido = $("#txtPeriodo").val();
		var sError = '';

        var sFabricacion = $("#hddAnioFabricacion").val();
        
        var aniFab = sPerido - sFabricacion;

        if (sPerido < anio)
        {
            //parent.fn_unBlockUI();
			//$("#txtPeriodo").focus();
			sError = 'Periodo No debe ser menor al año actual.';
			//parent.fn_mdl_alert("Periodo No debe ser menor al año actual", function() { });
			//return;
        }

        if (sPerido > anio) 
        {
            if (aniFab > 3) {
                //parent.fn_unBlockUI();
                //$("#txtPeriodo").focus();
                sError = 'Periodo No debe superar los 3 años de la fecha de fabricación.  ';
                //return;
            }
        }
	    //Fin
	
 if (sError.toString() != '') {
        if ($("#hddTipoTransaccion").val() == "NUEVO") {
            fn_mdl_confirma(sError.toString() + '<br/> ¿Esta seguro de insertar?',
                function() {
                    parent.fn_blockUI();
                    var vCodSolicitudCredito = $("#hddCodigoContrato").val();
                    var vCodigoBien = $("#hddCodigoBien").val();
                    var vPeriodo = '';
                    var vFechaEnvioCarta = '';
                    var vFechaRecepcionDocumentos = '';
                    var vFechaPresentacionSat = '';
                    var vFechaNotificacion = '';
                    var vNroResolucion = '';
                    var vEstadoResolucion = '';
                    var vEstado = '';


                    vPeriodo = $('#txtPeriodo').val() == undefined ? "" : $('#txtPeriodo').val();
                    vFechaEnvioCarta = $('#txtFecEnvioCarta').val() == undefined ? "" : $('#txtFecEnvioCarta').val();
                    vFechaRecepcionDocumentos = $('#txtFecRecepDocumentos').val() == undefined ? "" : $('#txtFecRecepDocumentos').val();
                    vFechaPresentacionSat = $('#txtFecPresentacionSat').val() == undefined ? "" : $('#txtFecPresentacionSat').val();
                    vFechaNotificacion = $('#txtFecNotificacion').val() == undefined ? "" : $('#txtFecNotificacion').val();
                    vNroResolucion = $('#txtNroResolucion').val() == undefined ? "" : $('#txtNroResolucion').val();
                    vEstadoResolucion = $('#ddlEstado').val() == undefined ? "" : $('#ddlEstado').val();

                    var arrParametros = ["pNumeroContrato", vCodSolicitudCredito,
                    "pCodigoBien", vCodigoBien,
                    "pPeriodo", vPeriodo,
                    "pFechaEnvioCarta", Fn_util_DateToString(vFechaEnvioCarta),
                    "pFechaRecepcionDocumentos", Fn_util_DateToString(vFechaRecepcionDocumentos),
                    "pFechaPresentacionSat", Fn_util_DateToString(vFechaPresentacionSat),
                    "pFechaNotificacion", Fn_util_DateToString(vFechaNotificacion),
                    "pNroResolucion", vNroResolucion,
                    "pEstadoResolucion", vEstadoResolucion,
                    "pEstado", "1"
                    ];

                    fn_util_AjaxWM("frmMantBienInafectacion.aspx/GuardarInafectacion",
                    arrParametros,
                    function() {
                        fn_cargaListadoInafectacion();
                    },
                  function(resultado) {
                      var error = eval("(" + resultado.responseText + ")");
                      parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                  });
                    parent.fn_unBlockUI();
                },
            "../util/images/question.gif",
            function() { },
            'Mantenimiento Bien'
            );

        } else {
            fn_mdl_confirma(sError.toString() + '<br/> ¿Esta seguro de modificar?',
            function() {
                parent.fn_blockUI();
                var vCodSolicitudCredito = $("#hddCodigoContrato").val();
                var vCodigoInafectacion = $("#hddCodigoInafectacion").val();
                var vCodigoBien = $("#hddCodigoBien").val();
                var vPeriodo = '';
                var vFechaEnvioCarta = '';
                var vFechaRecepcionDocumentos = '';
                var vFechaPresentacionSat = '';
                var vFechaNotificacion = '';
                var vNroResolucion = '';
                var vEstadoResolucion = '';
                var vEstado = '';


                vPeriodo = $('#txtPeriodo').val() == undefined ? "" : $('#txtPeriodo').val();
                vFechaEnvioCarta = $('#txtFecEnvioCarta').val() == undefined ? "" : $('#txtFecEnvioCarta').val();
                vFechaRecepcionDocumentos = $('#txtFecRecepDocumentos').val() == undefined ? "" : $('#txtFecRecepDocumentos').val();
                vFechaPresentacionSat = $('#txtFecPresentacionSat').val() == undefined ? "" : $('#txtFecPresentacionSat').val();
                vFechaNotificacion = $('#txtFecNotificacion').val() == undefined ? "" : $('#txtFecNotificacion').val();
                vNroResolucion = $('#txtNroResolucion').val() == undefined ? "" : $('#txtNroResolucion').val();
                vEstadoResolucion = $('#ddlEstado').val() == undefined ? "" : $('#ddlEstado').val();

                var arrParametros = ["pCodigoInafectacion", vCodigoInafectacion,
                "pNumeroContrato", vCodSolicitudCredito,
                "pCodigoBien", vCodigoBien,
                "pPeriodo", vPeriodo,
                "pFechaEnvioCarta", Fn_util_DateToString(vFechaEnvioCarta),
                "pFechaRecepcionDocumentos", Fn_util_DateToString(vFechaRecepcionDocumentos),
                "pFechaPresentacionSat", Fn_util_DateToString(vFechaPresentacionSat),
                "pFechaNotificacion", Fn_util_DateToString(vFechaNotificacion),
                "pNroResolucion", vNroResolucion,
                "pEstadoResolucion", vEstadoResolucion,
                "pEstado", "1"
                ];

                fn_util_AjaxWM("frmMantBienInafectacion.aspx/ModificarInafectacion",
                arrParametros,
                function() {
                    fn_cargaListadoInafectacion();
                },
                function(resultado) {
                    var error = eval("(" + resultado.responseText + ")");
                    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                });
                parent.fn_unBlockUI();
            },
                "../util/images/question.gif",
                function() { },
                'Mantenimiento Bien'
            );
        }
    }
    else {        
        if ($("#hddTipoTransaccion").val() == "NUEVO") {           
                parent.fn_blockUI();
                var vCodSolicitudCredito = $("#hddCodigoContrato").val();
                var vCodigoBien = $("#hddCodigoBien").val();
                var vPeriodo = '';
                var vFechaEnvioCarta = '';
                var vFechaRecepcionDocumentos = '';
                var vFechaPresentacionSat = '';
                var vFechaNotificacion = '';
                var vNroResolucion = '';
                var vEstadoResolucion = '';
                var vEstado = '';

                vPeriodo = $('#txtPeriodo').val() == undefined ? "" : $('#txtPeriodo').val();
                vFechaEnvioCarta = $('#txtFecEnvioCarta').val() == undefined ? "" : $('#txtFecEnvioCarta').val();
                vFechaRecepcionDocumentos = $('#txtFecRecepDocumentos').val() == undefined ? "" : $('#txtFecRecepDocumentos').val();
                vFechaPresentacionSat = $('#txtFecPresentacionSat').val() == undefined ? "" : $('#txtFecPresentacionSat').val();
                vFechaNotificacion = $('#txtFecNotificacion').val() == undefined ? "" : $('#txtFecNotificacion').val();
                vNroResolucion = $('#txtNroResolucion').val() == undefined ? "" : $('#txtNroResolucion').val();
                vEstadoResolucion = $('#ddlEstado').val() == undefined ? "" : $('#ddlEstado').val();

                var arrParametros = ["pNumeroContrato", vCodSolicitudCredito,
                "pCodigoBien", vCodigoBien,
                "pPeriodo", vPeriodo,
                "pFechaEnvioCarta", Fn_util_DateToString(vFechaEnvioCarta),
                "pFechaRecepcionDocumentos", Fn_util_DateToString(vFechaRecepcionDocumentos),
                "pFechaPresentacionSat", Fn_util_DateToString(vFechaPresentacionSat),
                "pFechaNotificacion", Fn_util_DateToString(vFechaNotificacion),
                "pNroResolucion", vNroResolucion,
                "pEstadoResolucion", vEstadoResolucion,
                "pEstado", "1"
                ];

                fn_util_AjaxWM("frmMantBienInafectacion.aspx/GuardarInafectacion",
                    arrParametros,
                    function() {
                        fn_cargaListadoInafectacion();
                    },
                    function(resultado) {
                        var error = eval("(" + resultado.responseText + ")");
                        parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                    });
                parent.fn_unBlockUI();            
        }
        else {
            parent.fn_blockUI();
            var vCodSolicitudCredito = $("#hddCodigoContrato").val();
            var vCodigoInafectacion = $("#hddCodigoInafectacion").val();
            var vCodigoBien = $("#hddCodigoBien").val();
            var vPeriodo = '';
            var vFechaEnvioCarta = '';
            var vFechaRecepcionDocumentos = '';
            var vFechaPresentacionSat = '';
            var vFechaNotificacion = '';
            var vNroResolucion = '';
            var vEstadoResolucion = '';
            var vEstado = '';


            vPeriodo = $('#txtPeriodo').val() == undefined ? "" : $('#txtPeriodo').val();
            vFechaEnvioCarta = $('#txtFecEnvioCarta').val() == undefined ? "" : $('#txtFecEnvioCarta').val();
            vFechaRecepcionDocumentos = $('#txtFecRecepDocumentos').val() == undefined ? "" : $('#txtFecRecepDocumentos').val();
            vFechaPresentacionSat = $('#txtFecPresentacionSat').val() == undefined ? "" : $('#txtFecPresentacionSat').val();
            vFechaNotificacion = $('#txtFecNotificacion').val() == undefined ? "" : $('#txtFecNotificacion').val();
            vNroResolucion = $('#txtNroResolucion').val() == undefined ? "" : $('#txtNroResolucion').val();
            vEstadoResolucion = $('#ddlEstado').val() == undefined ? "" : $('#ddlEstado').val();

            var arrParametros = ["pCodigoInafectacion", vCodigoInafectacion,
                                    "pNumeroContrato", vCodSolicitudCredito,
                                    "pCodigoBien", vCodigoBien,
                                    "pPeriodo", vPeriodo,
                                    "pFechaEnvioCarta", Fn_util_DateToString(vFechaEnvioCarta),
                                    "pFechaRecepcionDocumentos", Fn_util_DateToString(vFechaRecepcionDocumentos),
                                    "pFechaPresentacionSat", Fn_util_DateToString(vFechaPresentacionSat),
                                    "pFechaNotificacion", Fn_util_DateToString(vFechaNotificacion),
                                    "pNroResolucion", vNroResolucion,
                                    "pEstadoResolucion", vEstadoResolucion,
                                    "pEstado", "1"
                                    ];

            fn_util_AjaxWM("frmMantBienInafectacion.aspx/ModificarInafectacion",
                            arrParametros,
                            function() {
                                fn_cargaListadoInafectacion();
                            },
                            function(resultado) {
                                var error = eval("(" + resultado.responseText + ")");
                                parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                            });
            parent.fn_unBlockUI();
        }
        
    }
}
    /* if ($("#hddTipoTransaccion").val() == "NUEVO") {
    fn_mdl_confirma(sError.toString() + '<br/> ¿Esta seguro de insertar?',

    function() {
    parent.fn_blockUI();
    var vCodSolicitudCredito = $("#hddCodigoContrato").val();
    var vCodigoBien= $("#hddCodigoBien").val();
    var vPeriodo= '';
    var vFechaEnvioCarta = '';
    var vFechaRecepcionDocumentos = '';
    var vFechaPresentacionSat = '';
    var vFechaNotificacion = '';
    var vNroResolucion = '';
    var vEstadoResolucion = '';
    var vEstado = '';
            	
            	
    vPeriodo = $('#txtPeriodo').val() == undefined ? "" : $('#txtPeriodo').val();
    vFechaEnvioCarta = $('#txtFecEnvioCarta').val() == undefined ? "" : $('#txtFecEnvioCarta').val();
    vFechaRecepcionDocumentos = $('#txtFecRecepDocumentos').val() == undefined ? "" : $('#txtFecRecepDocumentos').val();
    vFechaPresentacionSat = $('#txtFecPresentacionSat').val() == undefined ? "" : $('#txtFecPresentacionSat').val();
    vFechaNotificacion = $('#txtFecNotificacion').val() == undefined ? "" : $('#txtFecNotificacion').val();
    vNroResolucion = $('#txtNroResolucion').val() == undefined ? "" : $('#txtNroResolucion').val();
    vEstadoResolucion = $('#ddlEstado').val() == undefined ? "" : $('#ddlEstado').val();
                	
    var arrParametros = ["pNumeroContrato", vCodSolicitudCredito,
    "pCodigoBien", vCodigoBien,
    "pPeriodo", vPeriodo,
    "pFechaEnvioCarta",Fn_util_DateToString(vFechaEnvioCarta),
    "pFechaRecepcionDocumentos", Fn_util_DateToString(vFechaRecepcionDocumentos),
    "pFechaPresentacionSat", Fn_util_DateToString(vFechaPresentacionSat),
    "pFechaNotificacion", Fn_util_DateToString(vFechaNotificacion),
    "pNroResolucion",vNroResolucion,
    "pEstadoResolucion",vEstadoResolucion,
    "pEstado","1"
    ];

fn_util_AjaxWM("frmMantBienInafectacion.aspx/GuardarInafectacion",
    arrParametros,
    function() {
    fn_cargaListadoInafectacion();
    },
    function(resultado) {
    var error = eval("(" + resultado.responseText + ")");
    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
    });
    parent.fn_unBlockUI();
    },
    "../util/images/question.gif",
    function() { },
    'Mantenimiento Bien'
    );
	
}else {

fn_mdl_confirma(sError.toString() + '<br/> ¿Esta seguro de modificar?',
    function() {
    parent.fn_blockUI();
    var vCodSolicitudCredito = $("#hddCodigoContrato").val();
    var vCodigoInafectacion=$("#hddCodigoInafectacion").val();
    var vCodigoBien= $("#hddCodigoBien").val();
    var vPeriodo= '';
    var vFechaEnvioCarta = '';
    var vFechaRecepcionDocumentos = '';
    var vFechaPresentacionSat = '';
    var vFechaNotificacion = '';
    var vNroResolucion = '';
    var vEstadoResolucion = '';
    var vEstado = '';
            	
            	
    vPeriodo = $('#txtPeriodo').val() == undefined ? "" : $('#txtPeriodo').val();
    vFechaEnvioCarta = $('#txtFecEnvioCarta').val() == undefined ? "" : $('#txtFecEnvioCarta').val();
    vFechaRecepcionDocumentos = $('#txtFecRecepDocumentos').val() == undefined ? "" : $('#txtFecRecepDocumentos').val();
    vFechaPresentacionSat = $('#txtFecPresentacionSat').val() == undefined ? "" : $('#txtFecPresentacionSat').val();
    vFechaNotificacion = $('#txtFecNotificacion').val() == undefined ? "" : $('#txtFecNotificacion').val();
    vNroResolucion = $('#txtNroResolucion').val() == undefined ? "" : $('#txtNroResolucion').val();
    vEstadoResolucion = $('#ddlEstado').val() == undefined ? "" : $('#ddlEstado').val();
                	
    var arrParametros = ["pCodigoInafectacion",vCodigoInafectacion,
    "pNumeroContrato", vCodSolicitudCredito,
    "pCodigoBien", vCodigoBien,
    "pPeriodo", vPeriodo,
    "pFechaEnvioCarta",Fn_util_DateToString(vFechaEnvioCarta),
    "pFechaRecepcionDocumentos", Fn_util_DateToString(vFechaRecepcionDocumentos),
    "pFechaPresentacionSat", Fn_util_DateToString(vFechaPresentacionSat),
    "pFechaNotificacion", Fn_util_DateToString(vFechaNotificacion),
    "pNroResolucion",vNroResolucion,
    "pEstadoResolucion",vEstadoResolucion,
    "pEstado","1"
    ];

fn_util_AjaxWM("frmMantBienInafectacion.aspx/ModificarInafectacion",
    arrParametros,
    function() {
    fn_cargaListadoInafectacion();
    },
    function(resultado) {
    var error = eval("(" + resultado.responseText + ")");
    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
    });
    parent.fn_unBlockUI();
    },
    "../util/images/question.gif",
    function() { },
    'Mantenimiento Bien'
    );
    }
    */
//IBK - FIN JJM
	
    //}

function fn_cargaListadoInafectacion() {
    var ctrlBtn = window.parent.frames[0].document.getElementById('btnCargarInafectacion');
    ctrlBtn.click();
	parent.fn_unBlockUI();
    parent.fn_util_CierraModal();
}