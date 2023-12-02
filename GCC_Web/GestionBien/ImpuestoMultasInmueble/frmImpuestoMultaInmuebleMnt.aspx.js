//****************************************************************
// Variables Globales
//****************************************************************
var C_TX_NUEVO = "NUEVO"
var C_TX_EDITAR = "EDITAR"

var C_GESTIONBIEN_IMPMUNICIPAL = "003";
var strMunicipalidadHidden = $("#hddMunicipalidad").val();
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	??? - 02/11/2012
//****************************************************************
$(document).ready(function() {
    //Valida Campos
    fn_inicializaCampos();



    //-------------------------------------------
    //CheckPagoCliente
    //-------------------------------------------
    $('#chkPagoCliente').click(function() {

        if ($('#chkPagoCliente').is(':checked') == true) {
            $("#hddPagoCliente").val("1");
            $("#txtEstadoPago").val("003");
            $("#txtEstadoCobro").val("003");

            $('#txtFechaPago').removeAttr('disabled');
            $('#txtFechaPago').attr('enabled', true);
            $('#txtFechaPago').attr('enabled', 'enabled');
            fn_util_SeteaCalendario($('#txtFechaPago'));

        } else {
            $('#txtFechaPago').attr('disabled', 'disabled');
            $('#txtFechaPago').val(' ');

            $("#hddPagoCliente").val("0")
            $("#txtEstadoPago").val("001");
            $("#txtEstadoCobro").val("001");
        }
    });


    //-------------------------------------------
    //Autovaluo
    //-------------------------------------------
    $("#txtAutovaluo").focusout(function() {
        fn_sumaImpuestoPredial();
        fn_sumaTotal();
    });

    //-------------------------------------------
    //ImpuestoPredial
    //-------------------------------------------
    $("#txtImpuestoPredial").focusout(function() {
        fn_sumaTotal();
    });

    //-------------------------------------------
    //Arbitrio
    //-------------------------------------------
    $("#txtArbitrio").focusout(function() {
        fn_sumaTotal();
    });

    //-------------------------------------------
    //Fiscalizacion
    //-------------------------------------------
    //    $("#txtFiscalizacion").focusout(function() {
    //        fn_sumaTotal();
    //    });

    //-------------------------------------------
    //Multa
    //-------------------------------------------
    $("#txtMulta").focusout(function() {
        fn_sumaTotal();
    });

    //-------------------------------------------
    //Total Autovaluo
    //-------------------------------------------
    $("#txtTotalAutovaluo").focusout(function() {
        fn_sumaImpuestoPredial();
        fn_sumaTotal();
    });

    //-------------------------------------------
    //Total Predial
    //-------------------------------------------
    $("#txtTotalPredial").focusout(function() {
        fn_sumaImpuestoPredial();
        fn_sumaTotal();
    });


    //Valida Modo Ver
    var strEstadoPago = $("#hddEstadoPago").val();
    var strFechaTransferencia = $("#hddFechaTransferencia").val();
    if (fn_util_trim(strEstadoPago) == "002" || fn_util_trim(strFechaTransferencia) != "" || fn_util_trim(strFechaTransferencia) == "01/01/1900") {
        fn_util_bloquearFormulario();
        $("#txtTotalAutovaluo").addClass("ui-edit-align-right");
        $("#txtTotalPredial").addClass("ui-edit-align-right");
        $("#txtImpuestoPredial").addClass("ui-edit-align-right");
        $("#txtAutovaluo").addClass("ui-edit-align-right");
        $("#txtArbitrio").addClass("ui-edit-align-right");
        $("#txtMulta").addClass("ui-edit-align-right");
        $("#txtFiscalizacion").addClass("ui-edit-align-right");
        $("#txtTotal").addClass("ui-edit-align-right");
        $("#dv_botonGuardar").hide();
        $("#dv_separador").hide();
        $("#dv_btnDocumentos").hide();
    }

    fn_ValidaImpuestoPredial();
    //On load Page (siempre al final)
    fn_onLoadPage();

});



//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {

    //Cabecera
    fn_util_inactivaInput("txtNroContrato", "I");
    fn_util_inactivaInput("txtCUCliente", "I");
    fn_util_inactivaInput("txtTipoDocumento", "I");
    fn_util_inactivaInput("txtNroDocumento", "I");
    fn_util_inactivaInput("txtRazonSocial", "I");
    fn_util_inactivaInput("txtDepartamento", "I");
    fn_util_inactivaInput("txtProvincia", "I");
    fn_util_inactivaInput("txtDistrito", "I");
    fn_util_inactivaInput("txtUbicacion", "I");
    fn_util_inactivaInput("txtLote", "I");

    //Cuerpo
    //fn_util_inactivaInput("txtTotalAutovaluo", "I");
    //fn_util_inactivaInput("txtTotalPredial", "I");
    fn_util_inactivaInput("txtTotal", "I");
    fn_util_inactivaInput("txtFechaPago", "I");
    fn_util_inactivaInput("txtFecCobro", "I");
    fn_util_inactivaInput("txtEstadoCobro", "S");
    fn_util_inactivaInput("txtEstadoPago", "S");
    //fn_util_inactivaInput("txtImpuestoPredial", "I");

    $('#txtPeriodo').validText({ type: 'number', length: 4 });
    $('#txtCodPredio').validText({ type: 'comment', length: 20 });
    $('#txtObservacion').validText({ type: 'comment', length: 150 });
    $("#txtObservacion").maxLength(150);

    //Alinea Montos
    $("#txtTotalAutovaluo").addClass("ui-edit-align-right");
    $("#txtTotalPredial").addClass("ui-edit-align-right");
    $("#txtImpuestoPredial").addClass("ui-edit-align-right");
    $("#txtTotal").addClass("ui-edit-align-right");

    //Campos dependiendo de la Tx
    if (fn_util_trim($("#hddTipoTx").val()) == C_TX_NUEVO) {
        $("#txtAutovaluo").validNumber({ value: '0' });
        $("#txtArbitrio").validNumber({ value: '0' });
        $("#txtMulta").validNumber({ value: '0' });
        $("#txtFiscalizacion").validNumber({ value: '0' });

        $("#txtTotalAutovaluo").validNumber({ value: fn_util_ValidaDecimal($("#txtTotalAutovaluo").val()) });
        $("#txtTotalPredial").validNumber({ value: fn_util_ValidaDecimal($("#txtTotalPredial").val()) });

        $("#id_loteLabel").hide();
        $("#id_loteInput").hide();
        $("#txtEstadoPago").val("001");
        $("#txtEstadoCobro").val("001");
        $("#dv_separador").hide();
        $("#dv_btnDocumentos").hide();
    } else {
        $("#txtAutovaluo").validNumber({ value: $("#txtAutovaluo").val() });
        $("#txtImpuestoPredial").validNumber({ value: $("#txtImpuestoPredial").val() });
        $("#txtArbitrio").validNumber({ value: $("#txtArbitrio").val() });
        $("#txtMulta").validNumber({ value: $("#txtMulta").val() });
        $("#txtFiscalizacion").validNumber({ value: $("#txtFiscalizacion").val() });

        $("#txtTotalAutovaluo").validNumber({ value: $("#txtTotalAutovaluo").val() });
        $("#txtTotalPredial").validNumber({ value: $("#txtTotalPredial").val() });
        $("#txtTotal").validNumber({ value: $("#txtTotal").val() });

        //        $("#txtTotalAutovaluo").val(fn_util_ValidaMonto($("#txtTotalAutovaluo").val()));
        //        $("#txtTotalPredial").val(fn_util_ValidaMonto($("#txtTotalPredial").val()));

    }

    if (($('#hddPerfil').val() != '6') && ($('#hddPerfil').val() != '11') && ($('#hddPerfil').val() != '1')) {
        $('#cbNoComision').attr('disabled', 'disabled');
    }
    // Fin IBK

    //Inicio JJM IBK
    if ($("#txtTotalAutovaluo").val() == '0.00') {
        $("#txtTotalAutovaluo").attr('enabled', true);
        $("#txtTotalAutovaluo").attr('enabled', 'enabled');
    }
    else { fn_util_inactivaInput("txtTotalAutovaluo", "I"); }

    if ($("#txtTotalPredial").val() == '0.00') {
        $("#txtTotalPredial").attr('enabled', 'enabled');
    }
    else { fn_util_inactivaInput("txtTotalPredial", "I"); }

    if ($("#txtTotalPredial").val() == '0.00') {
        $("#txtTotalPredial").attr('enabled', 'enabled');
    }
    else { fn_util_inactivaInput("txtTotalPredial", "I"); }
    //txtPeriodo
    if ($("#txtPeriodo").val() == '0.00') {
        $("#txtPeriodo").attr('enabled', 'enabled');
    }
    else { fn_util_inactivaInput("txtPeriodo", "I"); }
    fn_SetearCamposObligatorios();
    //Fin JJM IBK
    
    //INICIO IBK - AAE
    if ($("#hidReadOnly").val() == "Y") {
        $('#cbCobroAdelantado').attr('disabled', 'disabled');
        $('#cbNoComision').attr('disabled', 'disabled');
                
        $('#txtPeriodo').attr('disabled', 'disabled');
        $('#txtCodPredio').attr('disabled', 'disabled');
        $('#txtAutovaluo').attr('disabled', 'disabled');
        $('#txtImpuestoPredial').attr('disabled', 'disabled');
        $('#txtArbitrio').attr('disabled', 'disabled');
        $('#txtMulta').attr('disabled', 'disabled');
        $('#chkFizcalizacion').attr('disabled', 'disabled');
        $('#chkPagoCliente').attr('disabled', 'disabled');
        $('#txtFechaPago').attr('disabled', 'disabled');
        $('#txtObservaciones').attr('disabled', 'disabled');
        
        $('#dv_botonGuardar').css('display', 'none');
        $('#dv_btnDocumentos').css('display', 'none');
        $('#dv_separador').css('display', 'none');
    }   
    //FIN IBK
}

function fn_SetearCamposObligatorios() {
    fn_util_SeteaObligatorio($("#txtPeriodo"), "text");
    //    fn_util_SeteaObligatorio($("#txtCodPredio"), "text");
    //    fn_util_SeteaObligatorio($("#txtTotalAutovaluo"), "text");
    //    fn_util_SeteaObligatorio($("#txtTotalPredial"), "text");
    //    fn_util_SeteaObligatorio($("#txtAutovaluo"), "text");      
}


//****************************************************************
// Funcion		:: 	fn_guardar
// Descripción	::	Guarda
// Log			:: 	JRC - 13/11/2012
//****************************************************************
function fn_guardar() {


    //String Validación
    var strError = new StringBuilderEx();
    var pError = new StringBuilderEx();
    //Instancia Validaciones    		
    var objtxtTotalAutovaluo = $('input[id=txtTotalAutovaluo]:text');
    var objtxtTotalPredial = $('input[id=txtTotalPredial]:text');
    var objtxtPeriodo = $('input[id=txtPeriodo]:text');
    var objtxtCodPredio = $('input[id=txtCodPredio]:text');
    var objtxtAutovaluo = $('input[id=txtAutovaluo]:text');
    //var objtxtImpuestoPredial = $('input[id=txtImpuestoPredial]:text');
    var objtxtArbitrio = $('input[id=txtArbitrio]:text');
    var objtxtMulta = $('input[id=txtMulta]:text');
    //var objtxtFiscalizacion = $('input[id=txtFiscalizacion]:text');
    //var objtxtTotal = $('input[id=txtTotal]:text');
    //var objchkPagoCliente = $('input[id=chkPagoCliente]:text');
    //var objtxtFechaPago = $('input[id=txtFechaPago]:text');
    //var objtxtEstadoPago = $('input[id=txtEstadoPago]:text');
    //var objtxtFecCobro = $('input[id=txtFecCobro]:text');
    //var objtxtEstadoCobro = $('input[id=txtEstadoCobro]:text');
    //var objtxtObservacion = $('input[id=txtObservacion]:text');
    //Inicio IBK - AAE Agrego variables
    var vlote = 'N';
    var vCobroAdelantado = 'N';
    var vNoComision = '0';
    //Fin IBK
    //Inicio IBK - AAE - Agrego nuevos parametros
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
    //debugger;
    //Valida
    //    strError.append(fn_util_ValidateControl(objtxtTotalAutovaluo[0], 'un Total Autovaluo válido', 1, ''));
    //    strError.append(fn_util_ValidateControl(objtxtTotalPredial[0], 'un Total Predial válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtPeriodo[0], 'un Periodo válido', 1, ''));
    //	strError.append(fn_util_ValidateControl(objtxtCodPredio[0], 'un Código de Predio válido', 1, ''));
    //	strError.append(fn_util_ValidateControl(objtxtAutovaluo[0], 'un Autovaluo válido', 1, ''));
    //strError.append(fn_util_ValidateControl(objtxtArbitrio[0], 'una Arbitrio válido', 1, ''));
    //strError.append(fn_util_ValidateControl(objtxtMulta[0], 'una Multa válida', 1, ''));
    //strError.append(fn_util_ValidateControl(objtxtFiscalizacion[0], 'una Fiscalización válida', 1, ''));
    if ($('#txtTotal').val() <= 0) {
        strError.append("- El importe total no puede ser igual a 0. ");
    }
    if ($("#txtmuni").val() == '') {
        strError.append("Debe seleccionar Municipalidad");
    }
    if ($("#hddTotalAutovaluo").val() != $("#txtTotalAutovaluo").val() || $("#hddTotalPredial").val() != $("#txtTotalPredial").val()) {
        pError.append('El total de autovalúo y/o predial es diferente al de los registros ingresados para el periodo y municipalidad seleccionados, ¿Desea continuar y actualizar los registros? ');
    }
    else {
        pError.append('¿Está seguro que desea grabar el impuesto o multa?');
    }
    //Valida error existente
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
        fn_SetearCamposObligatorios();
    } else {
    parent.fn_mdl_confirma(pError, //Mensaje - Obligatorio
                            function() {
                                var Fiscalizacion;
                                if ($("#chkFizcalizacion").attr('checked')) {
                                    Fiscalizacion = '1';
                                } else { Fiscalizacion = '0'; }

                                var arrParametros = [
							"pstrTipoTx", $("#hddTipoTx").val(),
							"pstrCodContrato", $("#hddCodContrato").val(),
							"pstrCodBien", $("#hddCodBien").val(),
							"pstrCodImpuesto", $("#hddCodImpuesto").val(),
							"pstrCodUnico", $("#hddCodUnico").val(),
							"pstrTotalAutovaluo", $("#txtTotalAutovaluo").val(),
							"pstrTotalPredial", $("#txtTotalPredial").val(),
							"pstrPeriodo", $("#txtPeriodo").val(),
							"pstrCodPredio", $("#txtCodPredio").val(),
							"pstrAutovaluo", $("#txtAutovaluo").val(),
							"pstrImpuestoPredial", $("#txtImpuestoPredial").val(),
							"pstrArbitrio", $("#txtArbitrio").val(),
							"pstrMulta", $("#txtMulta").val(),
							"pstrFiscalizacion", Fiscalizacion,
							"pstrTotal", $("#txtTotal").val(),
							"pstrPagoCliente", $("#hddPagoCliente").val(),
							"pstrFechaPago", $("#txtFechaPago").val(),
							"pstrEstadoPago", $("#txtEstadoPago").val(),
							"pstrFecCobro", $("#txtFecCobro").val(),
							"pstrEstadoCobro", $("#txtEstadoCobro").val(),
							"pstrObservacion", $("#txtObservacion").val(),
							"pstrMunicipalidad", $("#txtmuni").val(),
							"pstrNroLote", $("#hddNroLote").val(),
							"pCobroAdelantado", vCobroAdelantado,
 		                     "pTengoLote", vLote,
 		                     "pNoComision", vNoComision
							];
                                parent.fn_blockUI();
                                fn_util_AjaxWM("frmImpuestoMultaInmuebleMnt.aspx/GrabaImpuesto",
                 arrParametros,
                                //Inicio IBK - AAE - agrego lógica de error
                                /*function(result) {
                                parent.fn_unBlockUI();
                                if (fn_util_trim(result) == "0") {
                                parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL GUARDAR");
                                } else {
                                if ($("#hddTipoTx").val() == C_TX_NUEVO) {
                                parent.fn_mdl_alert("Se grabó el Impuesto correctamente.", function() { fn_RedireccionGrabar() }, "GRABADO CORRECTO");
                                } else {
                                parent.fn_mdl_alert("Se actualizó correctamente los datos del Impuesto", function() { fn_RedireccionGrabar() }, "GRABADO CORRECTO");
                                }
                                }
                                },*/
                 function(resultado) {
                     var arrResultado = resultado.split("|")
                     if (arrResultado[0] == "0") {
                         var tengolot = window.parent.frames[0].document.getElementById('hidTengoLote');
                         var lotehid = window.parent.frames[0].document.getElementById('hddNroLote');
                         var loteNRo = window.parent.frames[0].document.getElementById('txtNroLoteCarga');
                         tengolot.value = arrResultado[1];
                         lotehid.value = arrResultado[1];
                         loteNRo.value = arrResultado[1];
                         $("#hddNroLote").val(arrResultado[1]);
                         if ($("#hddTipoTx").val() == C_TX_NUEVO) {
                             parent.fn_unBlockUI();
                             parent.fn_mdl_alert("Se grabó el Impuesto correctamente.", function() { fn_RedireccionGrabar() }, "GRABADO CORRECTO");
                         } else {
                             parent.fn_unBlockUI();   
                             parent.fn_mdl_alert("Se actualizó correctamente los datos del Impuesto", function() { fn_RedireccionGrabar() }, "GRABADO CORRECTO");
                         }
                     } else {
                         parent.fn_unBlockUI();
                         parent.fn_mdl_mensajeIco(arrResultado[1], "util/images/error.gif", "ERROR EN GRABAR");
                     }
                 },
                 function(resultado) {
                     parent.fn_unBlockUI();
                     var error = eval("(" + resultado.responseText + ")");
                     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABADO");
                 });

                            },
        "Util/images/question.gif",
        function() { },
        'Grabar');
    }
    $("#hddTotalAutovaluo").val('');
    $("#hddTotalPredial").val('');
}
function fn_RedireccionGrabar() {    
        var ctrlBtn = window.parent.frames[0].document.getElementById('btnBuscarImpuestosxLote');
        ctrlBtn.click();
        parent.fn_util_CierraModal(); 
}


//****************************************************************
// Funcion		:: 	fn_sumaTotal
// Descripción	::	Suma Totales
// Log			:: 	JRC - 26/11/2012
//****************************************************************
function fn_sumaTotal() {
    var decImpuestoPredial = fn_util_ValidaDecimal($("#txtImpuestoPredial").val());
    var decArbitrio = fn_util_ValidaDecimal($("#txtArbitrio").val());
    var decMulta = fn_util_ValidaDecimal($("#txtMulta").val());
    //var decFiscalizacion = fn_util_ValidaDecimal($("#txtFiscalizacion").val());

    var decTotal = decImpuestoPredial + decArbitrio + decMulta; //+ decFiscalizacion;
    $("#txtTotal").val(fn_util_ValidaMonto(decTotal, 2));
}


//****************************************************************
// Funcion		:: 	fn_sumaImpuestoPredial
// Descripción	::	Suma ImpPredial
// Log			:: 	JRC - 26/11/2012
//****************************************************************
function fn_sumaImpuestoPredial() {
    var decAutoValuo = fn_util_ValidaDecimal($("#txtAutovaluo").val());
    var decTotalAutovaluo = fn_util_ValidaDecimal($("#txtTotalAutovaluo").val());
    var decTotalPredial = fn_util_ValidaDecimal($("#txtTotalPredial").val());

    var decImpPredial = decAutoValuo * decTotalPredial / decTotalAutovaluo
    $("#txtImpuestoPredial").val(fn_util_ValidaMonto(decImpPredial, 2));
}


//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos() {
    var pstrCodContrato = fn_util_trim($("#hddCodContrato").val());
    var pstrCodBien = fn_util_trim($("#hddCodBien").val());
    var pstrCodRelacionado = fn_util_trim($("#hddCodImpuesto").val());
    var pstrCodTipo = C_GESTIONBIEN_IMPMUNICIPAL;
    parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo, 800, 350, function() { });
}


function fn_ValidaImpuestoPredial() {

    try {
        parent.fn_blockUI();

        var Periodo = $('#txtPeriodo').val() == undefined ? "" : $('#txtPeriodo').val();
        var Municipalidad = $('#txtmuni').val() == undefined ? "" : $('#txtmuni').val();

        var arrParametros = ["pstrPeriodo", Periodo, "pstrMunicipalidad", Municipalidad];

        fn_util_AjaxSyncWM("frmImpuestoMultaInmuebleMnt.aspx/GetImpuestoTotalesInmueble",
                arrParametros,
                fn_DatosObtenidos,
                function(request) {
                    parent.fn_unBlockUI();
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

    } catch (ex) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }


}
fn_DatosObtenidos = function(response) {

    var objEImpuesto = response;
    $("#hddTotalAutovaluo").val(objEImpuesto.TotalAutovaluo);
    $("#hddTotalPredial").val(objEImpuesto.TotalPredial);

    $("#hddTotalAutovaluo").validNumber({ value: fn_util_ValidaDecimal($("#hddTotalAutovaluo").val()) });
    $("#hddTotalPredial").validNumber({ value: fn_util_ValidaDecimal($("#hddTotalPredial").val()) });

    if ($("#hddTotalAutovaluo").val() == '') {
        $("#hddTotalAutovaluo").val($("#txtTotalAutovaluo").val());
    }
    if ($("#hddTotalPredial").val() == '') {
        $("#hddTotalPredial").val($("#txtTotalPredial").val());
    }
    parent.fn_unBlockUI();
};
