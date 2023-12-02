//****************************************************************
// Variables Globales
//****************************************************************
var blnPrimeraBusqueda;
var intPaginaActual = 1;
var strDepartamento = "15";
var strProvincia = "01";
//Inicio IBK
var strMunicipalidad = '';
var strLote = '';
//Fin IBK
var C_GESTIONBIEN_MULTAVEHICULAR = "004";

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	AEP - 08/11/2012
//****************************************************************
$(document).ready(function() {

    strLote = $("#hddNroLote").val();

    //Valida Campos
    fn_inicializaCampos();

    if ($("#cbPagoCliente").attr('checked')) {
        $("#ddlEstadoCobro").val('003');
        $("#ddlEstadoPago").val('003');
        $("#hddCheck").val('1');
    } else {
        $("#hddCheck").val('2');
    }


    $("#cbPagoCliente").change(function() {
        if ($(this).attr('checked')) {

            $("#ddlEstadoCobro").val('003');
            $("#ddlEstadoPago").val('003');
            // Inicio JJM IBK
            $('#txtFechaPago').removeAttr('disabled');
            $('#txtFechaPago').attr('enabled', true);
            $('#txtFechaPago').attr('enabled', 'enabled');
            fn_util_SeteaCalendario($('#txtFechaPago'));
            //Fin IBK
            $("#hddCheck").val('1');
        } else {
            $('#txtFechaPago').attr('disabled', 'disabled');
            $('#txtFechaPago').val(' ');
            if ($("#hddOrigen.Value").val('1')) {
                $("#ddlEstadoCobro").val('001');
                $("#ddlEstadoPago").val('001');
            } else {
                $("#ddlEstadoCobro").val($("#hddEstadoCobro").val());
                $("#ddlEstadoPago").val($("#hddEstadoPago").val());
            }
            $("#hddCheck").val('2');
        }

    });


    $("#ddlCodInfraccion").change(function() {
        if ($("#txtCodigoInfraccion").val() != "") {
            fn_ValidarInfraccionTotal();
        } else {
            $("#txtCodigoInfraccion").val('');
            $("#txtImporte").val('');
        }
        $("#txtFechaRecBanco").val('');
        $("#txtImporteDescuento").val('');
    });



    $("#txtCodigoInfraccion").focusout(function() {
        if ($("#txtCodigoInfraccion").val() != "") {
            fn_ValidarInfraccionTotal();
        }
        else {
            $("#txtCodigoInfraccion").val('');
            $("#txtImporte").val('');
        }
        $("#txtFechaRecBanco").val('');
        $("#txtImporteDescuento").val('');
    });




    fn_SeteaMunicipalidad();

    //On load Page (siempre al final)
    fn_onLoadPage();


});


function fn_ValidarInfraccionTotal() {
    var arrParametro = ["strCodigoInfraccion", $("#ddlCodInfraccion").val(),
        		                "strInfraccion", $("#txtCodigoInfraccion").val()];

    fn_util_AjaxSyncWM("frmMultaVehicularRegistroAgregar.aspx/ValidarInfraccionTotal",
        		arrParametro,
        		function(result2) {
        		    $("#txtImporte").val(result2);
        		    return;
        		},
        		function(result) {
        		    parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
        		});
}

function fn_ValidarInfraccion() {

    if ($("#txtFechaRecBanco").val() != "") {

        var arrParametro = ["strCodigoInfraccion", $("#ddlCodInfraccion").val(),
        		                "strInfraccion", $("#txtCodigoInfraccion").val(),
	                     "dtmFechaRecepcion", $("#txtFechaRecBanco").val()];

        fn_util_AjaxSyncWM("frmMultaVehicularRegistroAgregar.aspx/ValidarInfraccion",
        		arrParametro,
        		function(result2) {
        		    $("#txtImporteDescuento").val(result2);
        		    return;
        		},
        		function(result) {
        		    parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
        		});
    }
    else {
        $("#txtFechaRecBanco").val('');
        $("#txtImporteDescuento").val('');
    }


}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {
    strLote = $("#hddNroLote").val();
    if (strLote != '') {
        $('#ddlMunicipalidad').attr('disabled', 'disabled');
    }
    if ($("#hddTipoTransaccion").val() == "NUEVO") {
        $('#dv_documentos').css('display', 'none');
        $('#dv_separador').css('display', 'none');
        $('#ddlEstadoCobro').val('001');
        $('#ddlEstadoPago').val('001');
    }
    if ($("#hddFechaTransferencia").val() != "" || $("#hddOrigen").val() == "3" || $("#hddEstadoPago").val() == "002") {

        $('#txtFechaPago').attr('disabled', 'disabled');
        $('#txtFechaCobro').attr('disabled', 'disabled');
        $('#ddlEstadoPago').attr('disabled', 'disabled');
        $('#ddlEstadoCobro').attr('disabled', 'disabled');
        $('#ddlConcepto').attr('disabled', 'disabled');
        $('#ddlCodInfraccion').attr('disabled', 'disabled');
        $('#ddlMunicipalidad').attr('disabled', 'disabled');
        $('#txtNroInfraccion').attr('disabled', 'disabled');
        $('#txtPeriodo').attr('disabled', 'disabled');
        $('#txtImporte').attr('disabled', 'disabled');
        $('#txtImporteDescuento').attr('disabled', 'disabled');
        $('#txtTipoInfraccion').attr('disabled', 'disabled');
        $('#txtCodigoInfraccion').attr('disabled', 'disabled');
        $('#txtObservaciones').attr('disabled', 'disabled');
        $('#txtFechaRecBanco').attr('disabled', 'disabled');
        $('#txtFechaInfraccion').attr('disabled', 'disabled');
        $('#txtFechaPago').attr('disabled', 'disabled');
        $('#txtFechaCobro').attr('disabled', 'disabled');
        $('#txtFechaRegistro').attr('disabled', 'disabled');
        $('#cbPagoCliente').attr('disabled', 'disabled');
        //Inicio IBK - AAE
        $('#cbCobroAdelantado').attr('disabled', 'disabled');
        $('#cbNoComision').attr('disabled', 'disabled');
        $('#txtFechaNotLeasing').attr('disabled', 'disabled');
        $('#txtFecVenc').attr('disabled', 'disabled');
        fn_util_SeteaCalendario($('#txtFechaNotLeasing'));
        //Fin IBK

        fn_util_SeteaCalendario($('#txtFechaInfraccion'));
        fn_util_SeteaCalendario($('#txtFechaPago'));
        fn_util_SeteaCalendario($('#txtFechaCobro'));
        fn_util_SeteaCalendario($('#txtFechaRegistro'));
        fn_util_SeteaCalendarioFunction($('#txtFechaRecBanco'), fn_ValidarInfraccion);

        $('#dv_documentos').css('display', 'none');
        $('#dv_separador').css('display', 'none');
        $('#dv_guardar').css('display', 'none');

    } else {
        $('#txtNroInfraccion').validText({ type: 'alphanumeric', length: 12 });
        $('#txtPeriodo').validText({ type: 'number', length: 4 });
        $('#txtImporte').validNumber({ value: '', decimals: 2, length: 15 });
        $('#txtImporteDescuento').validNumber({ value: '', decimals: 2, length: 15 });
        $('#txtTipoInfraccion').validText({ type: 'comment', length: 20 });
        $('#txtTipoInfraccion').maxLength(20);
        $('#txtCodigoInfraccion').validText({ type: 'number', length: 3 });
        $('#txtObservaciones').validText({ type: 'comment', length: 150 });
        $('#txtObservaciones').maxLength(150);
        $('#txtFechaPago').attr('disabled', 'disabled');
        $('#txtFechaCobro').attr('disabled', 'disabled');
        $('#ddlEstadoPago').attr('disabled', 'disabled');
        $('#ddlEstadoCobro').attr('disabled', 'disabled');
        //Inicio IBK - AAE - Dejo que tome el valor del estado de pago/cobro , bloque municipalidad
        $('#ddlMunicipalidad').attr('disabled', 'disabled');        
        if (($('#hddPerfil').val() != '6') && ($('#hddPerfil').val() != '11') && ($('#hddPerfil').val() != '1')) {
            $('#cbNoComision').attr('disabled', 'disabled');
        }
        $('#txtFecVenc').attr('disabled', 'disabled');
        //$('#ddlEstadoCobro').val('001');
        //$('#ddlEstadoPago').val('001');
        fn_util_SeteaCalendario($('#txtFechaNotLeasing'));
        // Fin IBK
        fn_util_SeteaCalendario($('#txtFechaInfraccion'));
        fn_util_SeteaCalendario($('#txtFechaPago'));
        fn_util_SeteaCalendario($('#txtFechaCobro'));
        fn_util_SeteaCalendario($('#txtFechaRegistro'));        
        fn_util_SeteaCalendarioFunction($('#txtFechaRecBanco'), fn_ValidarInfraccion);

        fn_SetearCamposObligatorios();
    }
    
     //INICIO IBK - AAE
    if ($("#hidReadOnly").val() == "Y") {
        $('#txtFechaPago').attr('disabled', 'disabled');
        $('#txtFechaCobro').attr('disabled', 'disabled');
        $('#ddlEstadoPago').attr('disabled', 'disabled');
        $('#ddlEstadoCobro').attr('disabled', 'disabled');
        $('#ddlConcepto').attr('disabled', 'disabled');
        $('#ddlCodInfraccion').attr('disabled', 'disabled');
        $('#ddlMunicipalidad').attr('disabled', 'disabled');
        $('#txtNroInfraccion').attr('disabled', 'disabled');
        $('#txtPeriodo').attr('disabled', 'disabled');
        $('#txtImporte').attr('disabled', 'disabled');
        $('#txtImporteDescuento').attr('disabled', 'disabled');
        $('#txtTipoInfraccion').attr('disabled', 'disabled');
        $('#txtCodigoInfraccion').attr('disabled', 'disabled');
        $('#txtObservaciones').attr('disabled', 'disabled');
        $('#txtFechaRecBanco').attr('disabled', 'disabled');
        $('#txtFechaInfraccion').attr('disabled', 'disabled');
        $('#txtFechaPago').attr('disabled', 'disabled');
        $('#txtFechaCobro').attr('disabled', 'disabled');
        $('#txtFechaRegistro').attr('disabled', 'disabled');
        $('#cbPagoCliente').attr('disabled', 'disabled');        
        $('#cbCobroAdelantado').attr('disabled', 'disabled');
        $('#cbNoComision').attr('disabled', 'disabled');
        $('#txtFecVenc').attr('disabled', 'disabled');
        fn_util_SeteaCalendario($('#txtFechaNotLeasing'));
        fn_util_SeteaCalendario($('#txtFechaInfraccion'));
        fn_util_SeteaCalendario($('#txtFechaPago'));
        fn_util_SeteaCalendario($('#txtFechaCobro'));
        fn_util_SeteaCalendario($('#txtFechaRegistro'));
        fn_util_SeteaCalendarioFunction($('#txtFechaRecBanco'), fn_ValidarInfraccion);

        $('#dv_documentos').css('display', 'none');
        $('#dv_separador').css('display', 'none');
        $('#dv_guardar').css('display', 'none');
        $('#txtFechaNotLeasing').attr('disabled', 'disabled');
    }
    //FIN IBK
}

//****************************************************************
// Funcion		:: 	fn_SeteaMunicipalidad
// Descripción	::	Setear la municipalidad
// Log			:: 	AEP - 26/11/2012
//****************************************************************
function fn_SeteaMunicipalidad() {

    //    //Carga Distrito
    //    fn_cargaComboMunicipalidad(strDepartamento, strProvincia);
    //    $("#ddlMunicipalidad").val(fn_util_trim($("#hddCodMunicipalidad").val()));
}
//********************************************************  ********
// Funcion		:: 	fn_cargaComboMunicipalidad
// Descripción	::	
// Log			:: 	AEP - 26/11/2012
//****************************************************************
function fn_cargaComboMunicipalidad(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlMunicipalidad').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    //fn_doResize();
}
//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_limpiar() {

}


//****************************************************************
// Funcion		:: 	fn_abreEliminarImpuesto
// Descripción	::	Abre Editar Registro
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_abreEliminarImpuesto() {

}


//****************************************************************
// Funcion		:: 	fn_abreEditarImpuesto
// Descripción	::	Abre Editar Registro
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_abreEditarImpuesto() {

}


//****************************************************************
// Funcion		:: 	fn_abreNuevo
// Descripción	::	Abre Nuevo Registro
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_abreNuevoImpuesto() {
    parent.fn_blockUI();
    fn_util_redirect("frmImpuestoVehicularRegistro.aspx");
    //parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/ImpuestoVehicular/frmImpuestoVehicularNuevo.aspx", 500, 300, function() { });
}

function fn_SetearCamposObligatorios() {
    fn_util_SeteaObligatorio($("#txtNroInfraccion"), "text");
    fn_util_SeteaObligatorio($("#txtFechaInfraccion"), "text");
    fn_util_SeteaObligatorio($("#ddlConcepto"), "select");
    fn_util_SeteaObligatorio($("#ddlCodInfraccion"), "select");
    fn_util_SeteaObligatorio($('#txtCodigoInfraccion'), "text");
    fn_util_SeteaObligatorio($('#txtFechaRecBanco'), "text");
    fn_util_SeteaObligatorio($('#txtImporte'), "text");
    //fn_util_SeteaObligatorio($('#txtImporteDescuento'),"text");
    fn_util_SeteaObligatorio($("#ddlMunicipalidad"), "select");
    /* Inicio IBK - AAE - Seteo obligatorio fecha notif leasing*/
    fn_util_SeteaObligatorio($('#txtFechaNotLeasing'), "text");
    fn_util_SeteaCalendario($('#txtFechaNotLeasing'));
    /*Fin IBK*/
    fn_util_SeteaCalendario($('#txtFechaRecBanco'));
    fn_util_SeteaCalendario($('#txtFechaInfraccion'));
    
}

//****************************************************************
// Funcion		:: 	fn_grabarInpuestoVehicular
// Descripción	::	Grabar
// Log			:: 	AEP - 14/11/2012
//****************************************************************
function fn_grabarMultaVehicular() {

    var strError = new StringBuilderEx();

    var objtxtNroInfraccion = $('input[id=txtNroInfraccion]:text');
    var objtxtFechaInfraccion = $('input[id=txtFechaInfraccion]:text');
    var objddlTipoInfraccion = $('select[id=ddlConcepto]');
    var objddlCodInfraccion = $('select[id=ddlCodInfraccion]');
    var objtxtCodInfraccion = $('input[id=txtCodigoInfraccion]:text');
    var objtxtImporte = $('input[id=txtImporte]:text');
    var objtxtFechaRecBanco = $('input[id=txtFechaRecBanco]:text');
    //var objtxtImporteDcto = $('input[id=txtImporteDescuento]:text');
    var objddlMunicipal = $('select[id=ddlMunicipalidad]');
    //Inicio IBK - AAE - Cehque fecha notificación leasin
    var objtxtFechaNotifLeasing = $('input[id=txtFechaNotLeasing]:text');
    strError.append(fn_util_ValidateControl(objtxtFechaNotifLeasing[0], 'fecha de recepción del área de Leasing', 1, ''));
    //Fin IBK

    strError.append(fn_util_ValidateControl(objtxtNroInfraccion[0], 'un número de infracción', 1, ''));
    if (objtxtFechaInfraccion[0]) {
        strError.append(fn_util_ValidateControl(objtxtFechaInfraccion[0], 'fecha de infracción', 1, ''));
    }
    strError.append(fn_util_ValidateControl(objddlTipoInfraccion[0], 'un tipo de infracción', 1, ''));
    //    strError.append(fn_util_ValidateControl(objddlCodInfraccion[0], 'el código de infracción', 1, ''));
    //    strError.append(fn_util_ValidateControl(objtxtCodInfraccion[0], 'el código de infracción', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtImporte[0], 'un importe', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtFechaRecBanco[0], 'fecha de recepción de banco', 1, ''));
    //strError.append(fn_util_ValidateControl(objtxtImporteDcto[0], 'un importe de descuento', 1, ''));
    strError.append(fn_util_ValidateControl(objddlMunicipal[0], 'una municipalidad', 1, ''));
    //IBK JJM
    var strFechaActual = obtiene_fecha();

    if ($("#ddlConcepto").val() != '001') {
        strError.append(fn_util_ValidateControl(objddlCodInfraccion[0], 'el código de infracción', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtCodInfraccion[0], 'el código de infracción', 1, ''));
    }
    if (fn_util_ComparaFecha(strFechaActual, $("#txtFechaRecBanco").val())) {
        strError.append("<br/>   - La fecha de recepción banco no puede ser diferente a la fecha actual.");
    }
    if (fn_util_ComparaFecha(strFechaActual, $("#txtFechaRegistro").val())) {
        strError.append("<br/>   - La fecha de registro no puede ser diferente a la fecha actual.");
    }
    //IBK JJM
    if (strError.toString() != '') {

        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });

        strError = null;
        fn_SetearCamposObligatorios();
        return;
    }



    if ($("#hddTipoTransaccion").val() == "NUEVO") {


        fn_mdl_confirma('¿Está seguro de guardar?',
            function() {
                parent.fn_blockUI();

                var vCodSolicitudCredito = $("#txtNroContrato").val();
                var vCodigoBien = $("#hddSecFinanciamiento").val();
                var vNroInfraccion = '';
                var vFecInfraccion = '';
                var vCodConcepto = '';
                var vCodInfraccion = '';
                var vInfraccion = '';
                var vFecIngreso = '';
                var vFecRecepcionBanco = '';
                var vImporte = '';
                var vImporteDcto = '';
                var vCodMunicipalidad = '';
                var vPagoCliente = '';
                var vFechaPago = '';
                var vEstadoPago = '';
                var vFechaCobro = '';
                var vEstadoCobro = '';
                var vObservaciones = '';
                //Inicio IBK - AAE Agrego variables
                var vlote = 'N';
                var vCobroAdelantado = 'N';
                var vNoComision = '0';
                var vFechaNotifLeasing = '';
                //Fin IBK

                vNroInfraccion = $('#txtNroInfraccion').val() == undefined ? "" : $('#txtNroInfraccion').val();
                vFecInfraccion = $('#txtFechaInfraccion').val() == undefined ? "" : $('#txtFechaInfraccion').val();
                vCodConcepto = $('#ddlConcepto').val() == undefined ? "" : $('#ddlConcepto').val();
                vCodInfraccion = $('#ddlCodInfraccion').val() == undefined ? "" : $('#ddlCodInfraccion').val();
                vInfraccion = $('#txtCodigoInfraccion').val() == undefined ? "" : $('#txtCodigoInfraccion').val();
                vFecIngreso = $('#txtFechaRegistro').val() == undefined ? "" : $('#txtFechaRegistro').val();
                vFecRecepcionBanco = $('#txtFechaRecBanco').val() == undefined ? "" : $('#txtFechaRecBanco').val();
                vImporte = $('#txtImporte').val() == undefined ? "" : $('#txtImporte').val();
                vImporteDcto = $('#txtImporteDescuento').val() == undefined ? "" : $('#txtImporteDescuento').val();
                vCodMunicipalidad = $('#ddlMunicipalidad').val() == undefined ? "" : $('#ddlMunicipalidad').val();
                vPagoCliente = $("#hddCheck").val() == undefined ? "" : $("#hddCheck").val();
                vFechaPago = $('#txtFechaPago').val() == undefined ? "" : $('#txtFechaPago').val();
                vEstadoPago = $('#ddlEstadoPago').val() == undefined ? "" : $('#ddlEstadoPago').val();
                vFechaCobro = $('#txtFechaCobro').val() == undefined ? "" : $('#txtFechaCobro').val();
                vEstadoCobro = $('#ddlEstadoCobro').val() == undefined ? "" : $('#ddlEstadoCobro').val();
                vObservaciones = $('#txtObservaciones').val() == undefined ? "" : $('#txtObservaciones').val();
                //Inicio IBK - AAE - Agrego nuevos parametros
                vFechaNotifLeasing = $('#txtFechaNotLeasing').val() == undefined ? "" : Fn_util_DateToString($('#txtFechaNotLeasing').val());
                if ($("#cbCobroAdelantado").attr('checked')) {
                    vCobroAdelantado = 'S';
                } else {
                    vCobroAdelantado = 'N';
                }
                if ($("#cbNoComision").attr('checked')) {
                    vNoComision = '1';
                } else {
                    vNoComision = '0';
                }
                vLote = $('#hidTengoLote').val() == undefined ? "N" : $('#hidTengoLote').val();
                //Fin IBK

                var arrParametros = ["pNumeroContrato", vCodSolicitudCredito,
                                         "pCodigoBien", vCodigoBien,
                                         "pNroInfraccion", vNroInfraccion,
                                         "pFechaInfraccion", Fn_util_DateToString(vFecInfraccion),
                                         "pCodConcepto", vCodConcepto,
                	 		             "pCodInfraccion", vCodInfraccion,
                	 		             "pInfraccion", vInfraccion,
                	 		             "pFecIngreso", Fn_util_DateToString(vFecIngreso),
                	 		             "pFecRecepcionBanco", Fn_util_DateToString(vFecRecepcionBanco),
                                         "pImporte", fn_util_ValidaDecimal(vImporte),
                	 		             "pImporteDcto", fn_util_ValidaDecimal(vImporteDcto),
                	 		             "pCodMunicipalidad", vCodMunicipalidad,
                                         "pPagoCliente", vPagoCliente,
                	 		             "pFechaPago", Fn_util_DateToString(vFechaPago),
                	 		             "pEstadoPago", vEstadoPago,
                	 		             "pFechaCobro", Fn_util_DateToString(vFechaCobro),
                	 		             "pEstadoCobro", vEstadoCobro,
                	 		             "pObservaciones", vObservaciones,
                	 		             "pLote", strLote,
                	 		             "pCobroAdelantado", vCobroAdelantado,
                	 		             "pTengoLote", vLote,
                	 		             "pNoComision", vNoComision,
                	 		             "pFechaNotLeasing", vFechaNotifLeasing
                                         ];

                fn_util_AjaxSyncWM("frmMultaVehicularRegistroAgregar.aspx/GuardarMultaVehicular",
            		arrParametros,
                //Inicio IBK - AAE - agrego lógica de error
                /*function() {
                parent.fn_mdl_mensajeOk("Se grabó la multa correctamente.", function() { fn_cargaListadoMultas(); }, "GRABADO CORRECTO");
                },*/
            		 function(resultado) {            		     
            		     var arrResultado = resultado.split("|")
            		     if (arrResultado[0] == "0") {
            		         var tengolot = window.parent.frames[0].document.getElementById('hidTengoLote');
            		         var lotehid = window.parent.frames[0].document.getElementById('hddNroLote');
            		         var loteNRo = window.parent.frames[0].document.getElementById('txtNroLoteCarga');
            		         tengolot.value = arrResultado[1]
            		         lotehid.value = arrResultado[1]
            		         loteNRo.value = arrResultado[1]
            		         fn_cargaListadoMultas();
            		     } else {
            		         parent.fn_unBlockUI();
            		         parent.fn_mdl_mensajeIco(arrResultado[1], "util/images/error.gif", "ERROR EN GRABAR");
            		     }
            		 },
                //Fin IBK
            		function(resultado) {            		    
            		    var error = eval("(" + resultado.responseText + ")");
            		    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
            		});
                parent.fn_unBlockUI();
                fn_doResize();
            },
            "../../util/images/question.gif",
            function() { fn_SetearCamposObligatorios(); },
            'Impuesto Vehicular'
         );

    } else {

    fn_mdl_confirma('¿Está seguro de modificar?',
				function() {
				    parent.fn_blockUI();

				    var vCodSolicitudCredito = $("#txtNroContrato").val();
				    var vCodigoBien = $("#hddSecFinanciamiento").val();
				    var vCodigoMulta = $("#hddSecImpuesto").val();
				    var vNroInfraccion = '';
				    var vFecInfraccion = '';
				    var vCodConcepto = '';
				    var vCodInfraccion = '';
				    var vInfraccion = '';
				    var vFecIngreso = '';
				    var vFecRecepcionBanco = '';
				    var vImporte = '';
				    var vImporteDcto = '';
				    var vCodMunicipalidad = '';
				    var vPagoCliente = '';
				    var vFechaPago = '';
				    var vEstadoPago = '';
				    var vFechaCobro = '';
				    var vEstadoCobro = '';
				    var vObservaciones = '';
				    //Inicio IBK - AAE Agrego variables
				    var vCobroAdelantado = 'N';
				    var vNoComision = '0';
				    var vFechaNotifLeasing = '';
				    //Fin IBK

				    vNroInfraccion = $('#txtNroInfraccion').val() == undefined ? "" : $('#txtNroInfraccion').val();
				    vFecInfraccion = $('#txtFechaInfraccion').val() == undefined ? "" : $('#txtFechaInfraccion').val();
				    vCodConcepto = $('#ddlConcepto').val() == undefined ? "" : $('#ddlConcepto').val();
				    vCodInfraccion = $('#ddlCodInfraccion').val() == undefined ? "" : $('#ddlCodInfraccion').val();
				    vInfraccion = $('#txtCodigoInfraccion').val() == undefined ? "" : $('#txtCodigoInfraccion').val();
				    vFecIngreso = $('#txtFechaRegistro').val() == undefined ? "" : $('#txtFechaRegistro').val();
				    vFecRecepcionBanco = $('#txtFechaRecBanco').val() == undefined ? "" : $('#txtFechaRecBanco').val();
				    vImporte = $('#txtImporte').val() == undefined ? "" : $('#txtImporte').val();
				    vImporteDcto = $('#txtImporteDescuento').val() == undefined ? "" : $('#txtImporteDescuento').val();
				    vCodMunicipalidad = $('#ddlMunicipalidad').val() == undefined ? "" : $('#ddlMunicipalidad').val();
				    vPagoCliente = $("#hddCheck").val() == undefined ? "" : $("#hddCheck").val();
				    vFechaPago = $('#txtFechaPago').val() == undefined ? "" : $('#txtFechaPago').val();
				    vEstadoPago = $('#ddlEstadoPago').val() == undefined ? "" : $('#ddlEstadoPago').val();
				    vFechaCobro = $('#txtFechaCobro').val() == undefined ? "" : $('#txtFechaCobro').val();
				    vEstadoCobro = $('#ddlEstadoCobro').val() == undefined ? "" : $('#ddlEstadoCobro').val();
				    vObservaciones = $('#txtObservaciones').val() == undefined ? "" : $('#txtObservaciones').val();
				    //Inicio IBK - AAE - Agrego nuevos parametros
				    vFechaNotifLeasing = $('#txtFechaNotLeasing').val() == undefined ? "" : Fn_util_DateToString($('#txtFechaNotLeasing').val());
				    if ($("#cbCobroAdelantado").attr('checked')) {
				        vCobroAdelantado = 'S';
				    } else {
				        vCobroAdelantado = 'N';
				    }
				    if ($("#cbNoComision").attr('checked')) {
				        vNoComision = '1';
				    } else {
				        vNoComision = '0';
				    }
				    // Fin IBK
				    var arrParametros2 = ["pNumeroContrato", vCodSolicitudCredito,
                                         "pCodigoBien", vCodigoBien,
                	 	                 "pSecMulta", vCodigoMulta,
                                         "pNroInfraccion", vNroInfraccion,
                                         "pFechaInfraccion", Fn_util_DateToString(vFecInfraccion),
                                         "pCodConcepto", vCodConcepto,
                	 		             "pCodInfraccion", vCodInfraccion,
                	 		             "pInfraccion", vInfraccion,
                	 		             "pFecIngreso", Fn_util_DateToString(vFecIngreso),
                	 		             "pFecRecepcionBanco", Fn_util_DateToString(vFecRecepcionBanco),
                                         "pImporte", fn_util_ValidaDecimal(vImporte),
                	 		             "pImporteDcto", fn_util_ValidaDecimal(vImporteDcto),
                	 		             "pCodMunicipalidad", vCodMunicipalidad,
                                         "pPagoCliente", vPagoCliente,
                	 		             "pFechaPago", Fn_util_DateToString(vFechaPago),
                	 		             "pEstadoPago", vEstadoPago,
                	 		             "pFechaCobro", Fn_util_DateToString(vFechaCobro),
                	 		             "pEstadoCobro", vEstadoCobro,
                	 		             "pObservaciones", vObservaciones,
                	 		             "pNroLote", strLote,
                	 		             "pCobroAdelantado", vCobroAdelantado,
                	 		             "pNoComision", vNoComision,
                	 		             "pFechaNotLeasing", vFechaNotifLeasing
                                         ];

				    fn_util_AjaxSyncWM("frmMultaVehicularRegistroAgregar.aspx/ModificarMultaVehicular",
            		arrParametros2,
				    /*function() {
				    parent.fn_mdl_mensajeOk("Se modificó la multa correctamente.", function() { fn_cargaListadoMultas(); }, "GRABADO CORRECTO");

            		 },*/
				    //Inicio IBK - AAE - Agrego lógica de error
            		 function(resultado) {
            		     //fn_cargaListadoImpuestos();
            		     var arrResultado = resultado.split("|")
            		     if (arrResultado[0] == "0") {
            		         fn_cargaListadoMultas();
            		     } else {
            		         parent.fn_unBlockUI();
            		         parent.fn_mdl_mensajeIco(arrResultado[1], "util/images/error.gif", "ERROR EN MODIFICAR");
            		     }
            		 },
            		function(resultado) {
            		    var error = eval("(" + resultado.responseText + ")");
            		    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN MODIFICAR");
            		});
				    parent.fn_unBlockUI();
				    fn_doResize();
				},
				"../../util/images/question.gif",
				function() {
				},
				'Impuesto Vehicular'
			);
    }
}

function fn_cargaListadoMultas() {
    var ctrlBtn = window.parent.frames[0].document.getElementById('btnCargarMulta');
    ctrlBtn.click();
    parent.fn_unBlockUI();
    parent.fn_util_CierraModal();
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
    var pstrCodTipo = C_GESTIONBIEN_MULTAVEHICULAR;
    parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo, 800, 350, function() { });
}

function obtiene_fecha() {
    var fecha_actual = new Date();

    var dia = fecha_actual.getDate();
    var mes = fecha_actual.getMonth() + 1;
    var anio = fecha_actual.getFullYear();

    if (mes < 10)
        mes = '0' + mes;
    if (dia < 10)
        dia = '0' + dia;
    //return (anio + mes + dia);
    return (dia + "/" + mes + "/" + anio);

}