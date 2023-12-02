//****************************************************************
// Variables Globales
//****************************************************************
var blnPrimeraBusqueda;
var intPaginaActual = 1;
var strCmbCuota = "";

var C_GESTIONBIEN_IMPVEHICULAR = "005";

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	AEP - 08/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();


    if ($("#cbPagoCliente").attr('checked')) {
        $("#ddlEstadoCobro").val('003');
        $("#ddlEstadoPago").val('003');
        $("#hddCheck").val('1');
    } else {
        $("#hddCheck").val('2');
    }


//    $("#cbPagoCliente").change(function() {
//        if ($(this).attr('checked')) {

//            $("#ddlEstadoCobro").val('003');
//            $("#ddlEstadoPago").val('003');
//            $("#hddCheck").val('1');
//        } else {
//            if ($("#hddOrigen.Value").val('1')) {
//                $("#ddlEstadoCobro").val('001');
//                $("#ddlEstadoPago").val('001');
//            } else {
//                $("#ddlEstadoCobro").val($("#hddEstadoCobro").val());
//                $("#ddlEstadoPago").val($("#hddEstadoPago").val());
//            }
//            $("#hddCheck").val('2');
//        }

//    });

    //strCmbCuota = $('#ddlCuota').html();	

    //		$("#txtPeriodo").focusout(function() {
    //    		if ($("#txtPeriodo").val()!="") {
    //    			$('#ddlCuota').html(strCmbCuota);
    //    			fn_ValidarCuotas();
    //    		}else {
    //    		 $('#ddlCuota').html(strCmbCuota);
    //    		}
    //		});


    //On load Page (siempre al final)
    fn_onLoadPage();


});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	AEP - 14/11/2012
//****************************************************************
function fn_inicializaCampos() {
    if ($("#hddTipoTransaccion").val() == "NUEVO") {
        $('#dv_documentos').css('display', 'none');
        $('#dv_separador').css('display', 'none');
        $("#ddlMoneda").val('001');
    }


    $('#txtImporte').validNumber({ value: '', decimals: 2, length: 15 });

    if ($("#hddFechaTransferencia").val() != "" || $("#hddOrigen").val() == "3" || $("#hddCheque").val() == "002") {
        fn_util_SeteaCalendario($('#txtFechaDeclaracion'));
        fn_util_SeteaCalendario($('#txtFechaPago'));
        fn_util_SeteaCalendario($('#txtFechaCobro'));
        $('#hddVer').val('1');
        $('#txtFechaDeclaracion').attr('disabled', 'disabled');
        $('#txtPeriodo').attr('disabled', 'disabled');
        $('#txtImporte').attr('disabled', 'disabled');
        $('#ddlCuota').attr('disabled', 'disabled');
        $('#ddlMoneda').attr('disabled', 'disabled');
        $('#cbPagoCliente').attr('disabled', 'disabled');
        $('#txtFechaPago').attr('disabled', 'disabled');
        $('#txtFechaCobro').attr('disabled', 'disabled');
        $('#ddlEstadoPago').attr('disabled', 'disabled');
        $('#ddlEstadoCobro').attr('disabled', 'disabled');
        $('#txtObservaciones').attr('disabled', 'disabled');
        $('#dv_guardar').css('display', 'none');
        $('#dv_documentos').css('display', 'none');
        $('#dv_separador').css('display', 'none');
        //	$('#txtImporte').validNumber({value:'',decimals:2,length:15});
    } else {
        $('#txtPeriodo').validText({ type: 'number', length: 4 });
        // $('#txtImporte').validNumber({value:'',decimals:2,length:15});

        fn_util_SeteaCalendario($('#txtFechaDeclaracion'));
        fn_util_SeteaCalendario($('#txtFechaPago'));
        fn_util_SeteaCalendario($('#txtFechaCobro'));
        $('#txtFechaPago').attr('disabled', 'disabled');
        $('#txtFechaCobro').attr('disabled', 'disabled');
        $('#ddlEstadoPago').attr('disabled', 'disabled');
        $('#ddlEstadoCobro').attr('disabled', 'disabled');
        $('#ddlEstadoCobro').val('001');
        $('#ddlEstadoPago').val('001');
        fn_SetearCamposObligatorios();
    }




}

function fn_SetearCamposObligatorios() {
    fn_util_SeteaObligatorio($("#txtFechaDeclaracion"), "text");
    fn_util_SeteaObligatorio($("#txtPeriodo"), "text");
    fn_util_SeteaObligatorio($("#txtImporte"), "text");
    fn_util_SeteaObligatorio($("#ddlCuota"), "select");
    fn_util_SeteaObligatorio($("#ddlMoneda"), "select");
    fn_util_SeteaCalendario($('#txtFechaDeclaracion'));
}



//****************************************************************
// Funcion		:: 	fn_grabarInpuestoVehicular
// Descripción	::	Grabar
// Log			:: 	AEP - 14/11/2012
//****************************************************************
function fn_grabarInpuestoVehicular() {


    var strError = new StringBuilderEx();

    var objtxtFechaDeclaracion = $('input[id=txtFechaDeclaracion]:text');
    var objtxtPeriodo = $('input[id=txtPeriodo]:text');
    var objtxtImporte = $('input[id=txtImporte]:text');
    var objddlCuota = $('select[id=ddlCuota]');
    var objddlMoneda = $('select[id=ddlMoneda]');
    if (objtxtFechaDeclaracion[0]) {
        strError.append(fn_util_ValidateControl(objtxtFechaDeclaracion[0], 'fecha declaración', 1, ''));
    }
    strError.append(fn_util_ValidateControl(objtxtPeriodo[0], 'un periodo', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtImporte[0], 'un importe', 1, ''));
    strError.append(fn_util_ValidateControl(objddlCuota[0], 'una cuota', 1, ''));
    strError.append(fn_util_ValidateControl(objddlMoneda[0], 'una moneda', 1, ''));

    if (strError.toString() != '') {

        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });

        strError = null;
        fn_SetearCamposObligatorios();
        return;
    }

    if ($("#hddTipoTransaccion").val() == "NUEVO") {


        fn_mdl_confirma('¿Está seguro de Guardar?',
            function() {
                parent.fn_blockUI();

                var vCodSolicitudCredito = $("#txtNroContrato").val();
                var vCodigoBien = $("#hddSecFinanciamiento").val();
                var vCodigoUnico = $("#hddCodigoUnico").val();
                var vFecDeclaracion = '';
                var vPeriodo = '';
                var vImporte = '';
                var vCuota = '';
                var vPagoCliente = '';
                var vFechaPago = '';
                var vEstadoPago = '';
                var vFechaCobro = '';
                var vEstadoCobro = '';
                var vMoneda = '';
                var vObservaciones = '';

                vFecDeclaracion = $('#txtFechaDeclaracion').val() == undefined ? "" : $('#txtFechaDeclaracion').val();
                vPeriodo = $('#txtPeriodo').val() == undefined ? "" : $('#txtPeriodo').val();
                vImporte = $('#txtImporte').val() == undefined ? "" : $('#txtImporte').val();
                vCuota = $('#ddlCuota').val() == undefined ? "" : $('#ddlCuota').val();
                vPagoCliente = $("#hddCheck").val() == undefined ? "" : $("#hddCheck").val();
                vFechaPago = $('#txtFechaPago').val() == undefined ? "" : $('#txtFechaPago').val();
                vEstadoPago = $('#ddlEstadoPago').val() == undefined ? "" : $('#ddlEstadoPago').val();
                vFechaCobro = $('#txtFechaCobro').val() == undefined ? "" : $('#txtFechaCobro').val();
                vEstadoCobro = $('#ddlEstadoCobro').val() == undefined ? "" : $('#ddlEstadoCobro').val();
                vMoneda = $('#ddlMoneda').val() == undefined ? "" : $('#ddlMoneda').val();
                vObservaciones = $('#txtObservaciones').val() == undefined ? "" : $('#txtObservaciones').val();

                var arrParametros = ["pNumeroContrato", vCodSolicitudCredito,
                                         "pCodigoBien", vCodigoBien,
                                         "pCodUnico", vCodigoUnico,
                                         "pFechaDeclaracion", Fn_util_DateToString(vFecDeclaracion),
                                         "pPeriodo", vPeriodo,
                                         "pImporte", fn_util_ValidaDecimal(vImporte),
                	 		             "pCuota", vCuota,
                                         "pPagoCliente", vPagoCliente,
                	 		             "pFechaPago", Fn_util_DateToString(vFechaPago),
                	 		             "pEstadoPago", vEstadoPago,
                	 		             "pFechaCobro", Fn_util_DateToString(vFechaCobro),
                	 		             "pEstadoCobro", vEstadoCobro,
                	 		             "pMoneda", vMoneda,
                	 		             "pObservaciones", vObservaciones
                                         ];

                fn_util_AjaxSyncWM("frmImpuestoVehicularRegistroAgregar.aspx/GuardarImpuestoVehicular",
            		arrParametros,
            		 function() {
            		     fn_cargaListadoImpuestos();
            		 },
            		function(resultado) {
            		    var error = eval("(" + resultado.responseText + ")");
            		    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
            		});
                fn_unBlockUI();
                fn_doResize();
            },
            "../../util/images/question.gif",
            function() { fn_SetearCamposObligatorios(); },
            'Impuesto Vehicular'
         );

    } else {

        fn_mdl_confirma('¿Está seguro de Modificar?',
				function() {
				    parent.fn_blockUI();

				    var vCodSolicitudCredito = $("#txtNroContrato").val();
				    var vCodigoBien = $("#hddSecFinanciamiento").val();
				    var vCodigoUnico = $("#hddCodigoUnico").val();
				    var vCodigoImpuesto = $("#hddSecImpuesto").val();
				    var vFecDeclaracion = '';
				    var vPeriodo = '';
				    var vImporte = '';
				    var vCuota = '';
				    var vPagoCliente = '';
				    var vFechaPago = '';
				    var vEstadoPago = '';
				    var vFechaCobro = '';
				    var vEstadoCobro = '';
				    var vEstado = '';
				    var vMoneda = '';
				    var vObservaciones = '';

				    vFecDeclaracion = $('#txtFechaDeclaracion').val() == undefined ? "" : $('#txtFechaDeclaracion').val();
				    vPeriodo = $('#txtPeriodo').val() == undefined ? "" : $('#txtPeriodo').val();
				    vImporte = $('#txtImporte').val() == undefined ? "" : $('#txtImporte').val();
				    vCuota = $('#ddlCuota').val() == undefined ? "" : $('#ddlCuota').val();
				    vPagoCliente = $("#hddCheck").val() == undefined ? "" : $("#hddCheck").val();
				    vFechaPago = $('#txtFechaPago').val() == undefined ? "" : $('#txtFechaPago').val();
				    vEstadoPago = $('#ddlEstadoPago').val() == undefined ? "" : $('#ddlEstadoPago').val();
				    vFechaCobro = $('#txtFechaCobro').val() == undefined ? "" : $('#txtFechaCobro').val();
				    vEstadoCobro = $('#ddlEstadoCobro').val() == undefined ? "" : $('#ddlEstadoCobro').val();
				    vMoneda = $('#ddlMoneda').val() == undefined ? "" : $('#ddlMoneda').val();
				    vObservaciones = $('#txtObservaciones').val() == undefined ? "" : $('#txtObservaciones').val();

				    var arrParametros2 = ["pNumeroContrato", vCodSolicitudCredito,
                                         "pCodigoBien", vCodigoBien,
                	 		             "pCodImpuesto", vCodigoImpuesto,
                                         "pCodUnico", vCodigoUnico,
                                         "pFechaDeclaracion", Fn_util_DateToString(vFecDeclaracion),
                                         "pPeriodo", vPeriodo,
                                         "pImporte", fn_util_ValidaDecimal(vImporte),
                	 		             "pCuota", vCuota,
                                         "pPagoCliente", vPagoCliente,
                	 		             "pFechaPago", Fn_util_DateToString(vFechaPago),
                	 		             "pEstadoPago", vEstadoPago,
                	 		             "pFechaCobro", Fn_util_DateToString(vFechaCobro),
                	 		             "pEstadoCobro", vEstadoCobro,
                	 		             "pMoneda", vMoneda,
                	 		             "pEstado", "1",
                	 		             "pObservaciones", vObservaciones
                                         ];

				    fn_util_AjaxSyncWM("frmImpuestoVehicularRegistroAgregar.aspx/ModificarImpuestoVehicular",
            		arrParametros2,
            		 function() {
            		     fn_cargaListadoImpuestos();
            		 },
            		function(resultado) {
            		    var error = eval("(" + resultado.responseText + ")");
            		    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN MODIFICAR");
            		});
				    fn_unBlockUI();
				    fn_doResize();
				},
				"../../util/images/question.gif",
				function() {
				},
				'Impuesto Vehicular'
			);
    }
}

function fn_cargaListadoImpuestos() {
    var ctrlBtn = window.parent.frames[0].document.getElementById('btnCargarImpuesto');
    ctrlBtn.click();
    parent.fn_unBlockUI();
    parent.fn_util_CierraModal();
}

function fn_ValidarCuotas() {


    var arrParametro = ["strCodigoBien", $("#hddSecFinanciamiento").val(),
        		    "strPeriodo", $("#txtPeriodo").val(),
                    "strCodigoContrato", $("#txtNroContrato").val()];

    fn_util_AjaxSyncWM("frmImpuestoVehicularRegistroAgregar.aspx/ValidarCuotaPeriodo",
        		arrParametro,
        		function(result2) {
        		    $("#hddNroCuotas").val(result2);
        		},
        		function(result) {
        		    parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL VALIDAR");
        		});
    var StrNroCuota = $("#hddNroCuotas").val();

    if (StrNroCuota != "") {
        var ArrayCuota = StrNroCuota.split(',');

        for (var i = 0; i < ArrayCuota.length; i++) {

            $("#ddlCuota option[value='" + ArrayCuota[i] + "']").remove();
        }
    }
    else {
        $('#ddlCuota').html(strCmbCuota);
    }

}



//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos() {
    var pstrCodContrato = fn_util_trim($("#txtNroContrato").val());
    var pstrCodBien = fn_util_trim($("#hddSecFinanciamiento").val());
    var pstrCodRelacionado = fn_util_trim($("#hddSecImpuesto").val());
    var pstrCodTipo = C_GESTIONBIEN_IMPVEHICULAR;
    parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + "&hddVer=" + $('#hddVer').val(), 800, 350, function() { });
}