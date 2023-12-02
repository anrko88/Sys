//****************************************************************
// Variables Globales
//****************************************************************
var C_TX_NUEVO = "NUEVO";
var C_TX_EDITAR = "EDITAR";
var strFlagRegistro = '0';
var C_GESTIONBIEN_OTROSCONCEPTOS = "007";
var C_CONCEPTOS_OpcionCompra = "O00";

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	WCR - 20/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();

    //On load Page (siempre al final)
    fn_onLoadPage();
});

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_inicializaCampos() {
    $('#txtNroContrato').validText({ type: 'number', length: 8 });
    $("#txtImporte").validNumber({ value: '' });
    $("#txtComision").validNumber({ value: '' });
    $("#txtIGVComision").validNumber({ value: '' });
    $("#txtTotal").validNumber({ value: '' });
    $("#txtObservaciones").validText({ type: 'comment', length: 150 }); ;
    $("#txtObservaciones").maxLength(150);
    $('#txtRegistro').validText({ type: 'number', length: 8 });

    fn_SetearFlagRegistro('0');
    fn_util_SeteaCalendario($('input[id*=txtFechaCobro]'));

    $('#cboConcepto').change(function() {
        fn_CalculoComision();
    });

    $('#cboMoneda').change(function() {
        fn_CalculoComision();
    });

    $('#imgPrevious').click(function() {
        fn_guardar(false, $('#hidPaginadoAnterior').val());
        //fn_SetearParametro($('#hidPaginadoAnterior').val());
    });

    $('#imgNext').click(function() {
        fn_guardar(false, $('#hidPaginadoSiguiente').val());
        //fn_SetearParametro($('#hidPaginadoSiguiente').val());
    });

    $('#txtRegistro').keydown(function(event) {
        if (event.which || event.keyCode) {
            if ((event.which == 13) || (event.keyCode == 13)) {
                fn_guardar(false, 'x');
                //fn_BuscarItem();
                return false;
            }
        }
        else {
            return true
        }
    });

    //    $('#txtRegistro').blur(function(event) {
    //        fn_BuscarItem();
    //    });

    fn_util_SeteaObligatorio($("#txtComision"), "input");
    fn_util_SeteaObligatorio($("#txtFechaCobro"), "calendar");

    if ($('#hidOpcion').val() == C_TX_NUEVO) {
        fn_SetearNuevo();
    }
    else {

        fn_ObtenerCobro();
        $('#tbPagina').show();
        if ($('#hidEditarCons').val() == '1') { $('#tbPagina').hide(); }

        $("#imgBuscarContrato").hide();
        $('#txtNroContrato').removeClass('css_input');
        $('#txtNroContrato').addClass('css_input_inactivo');
        $('#txtNroContrato').attr('readonly', true);
        $('#cboConcepto').attr('disabled', 'disabled');

    }

}

//****************************************************************
// Funcion		:: 	fn_IncializaEditar
// Descripción	::	
// Log			:: 	WCR - 04/12/2012
//****************************************************************
function fn_IncializaEditar() {
    fn_util_SeteaCalendario($('input[id*=txtFechaCobro]'));
    $('#txtNroContrato').validText({ type: 'number', length: 8 });
    //    $("#txtImporte").validNumber({ value: '' });
    //    $("#txtComision").validNumber({ value: '' });
    //    $("#txtIGVComision").validNumber({ value: '' });
    //$("#txtTotal").validNumber({ value: '' });
    $("#txtObservaciones").validText({ type: 'comment', length: 150 }); ;
    $("#txtObservaciones").maxLength(150);
    $('#txtRegistro').validText({ type: 'number', length: 8 });

    fn_util_SeteaCalendario($('input[id*=txtFechaCobro]'));
    fn_util_SeteaObligatorio($("#txtComision"), "input");
    fn_util_SeteaObligatorio($("#txtFechaCobro"), "input");

    $('#tbPagina').show();
    if ($('#hidEditarCons').val() == '1') { $('#tbPagina').hide(); }
    $("#imgBuscarContrato").hide();
    $('#txtNroContrato').removeClass('css_input');
    $('#txtNroContrato').addClass('css_input_inactivo');
    $('#txtNroContrato').attr('readonly', true);
    $('#cboConcepto').attr('disabled', 'disabled');
    if ($('#hidFlagIndividual').val() == '1') { fn_util_SeteaObligatorio($("#cboMoneda"), "select"); }
}

//****************************************************************
// Funcion		:: 	fn_guardar
// Descripción	::	
// Log			:: 	WCR - 29/11/2012
//****************************************************************
function fn_guardar(pModo, pPaginado) {
    var sbError = new StringBuilderEx();

    fn_ValidarRegistro(sbError);
    if (sbError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(sbError.toString(), function() { });
        sbError = null;
    } else {

        var strOpcion = $('#hidOpcion').val() == undefined ? "" : $('#hidOpcion').val();
        var strNumeroContrato = $('#hidCodSolicitudCredito').val() == undefined ? "" : $('#hidCodSolicitudCredito').val();
        var strTipoRubroFinanciamiento = $('#hidTipoRubroFinanciamiento').val() == undefined ? "" : $('#hidTipoRubroFinanciamiento').val();
        var strCodIfi = $('#hidCodIfi').val() == undefined ? "" : $('#hidCodIfi').val();
        var strTipoRecuperacion = $('#hidTipoRecuperacion').val() == undefined ? "" : $('#hidTipoRecuperacion').val();
        var strNumSecRecuperacion = $('#hidNumSecRecuperacion').val() == undefined ? "" : $('#hidNumSecRecuperacion').val();
        var strNumSecRecupComi = $('#hidNumSecRecupComi').val() == undefined ? "" : $('#hidNumSecRecupComi').val();
        var strImporte = $('#txtImporte').val() == undefined ? "" : $('#txtImporte').val();
        var strImporteIGV = $('#txtReembolsoIGV').val() == undefined ? "" : $('#txtReembolsoIGV').val(); //JJM IBK
        var strComision = $('#txtComision').val() == undefined ? "" : $('#txtComision').val();
        var strComisionIGV = $('#txtIGVComision').val() == undefined ? "" : $('#txtIGVComision').val();
        var strFechaCobro = $('#txtFechaCobro').val() == undefined ? "" : $('#txtFechaCobro').val();
        var strObservaciones = $('#txtObservaciones').val() == undefined ? "" : $('#txtObservaciones').val();
        var strFlagIndividual = $('#hidFlagIndividual').val() == undefined ? "" : $('#hidFlagIndividual').val();
        var strInstancia = $('#hidInstancia').val() == undefined ? "" : $('#hidInstancia').val();

        var strCodComisionTipo = $("#cboConcepto option:selected").val() == "0" ? "" : $("#cboConcepto option:selected").val();
        var strCodigoMoneda = $("#cboMoneda option:selected").val() == "0" ? "" : $("#cboMoneda option:selected").val();
        var strEstadoCobro = $('#hidEstadoCobro').val() == undefined ? "" : $('#hidEstadoCobro').val();
        var strNumeroSecuencia = $('#hidNumeroSecuencia').val() == undefined ? "" : $('#hidNumeroSecuencia').val();
       
        var arrParametros = ["pstrOpcion", strOpcion,
                                     "pstrNumeroContrato", strNumeroContrato,
                                     "pstrTipoRubroFinanciamiento", strTipoRubroFinanciamiento,
                                     "pstrCodIfi", strCodIfi,
                                     "pstrTipoRecuperacion", strTipoRecuperacion,
                                     "pstrNumSecRecuperacion", strNumSecRecuperacion,
            	 		             "pstrNumSecRecupComi", strNumSecRecupComi,
            	 		             "pstrCodComisionTipo", strCodComisionTipo,
            		                 "pstrCodigoMoneda", strCodigoMoneda,
            		                 "pstrImporte", fn_util_ValidaDecimal(strImporte),
            		                 "pstrComision", fn_util_ValidaDecimal(strComision),
            		                 "pstrComisionIGV", fn_util_ValidaDecimal(strComisionIGV),
            		                 "pstrFechaCobro", Fn_util_DateToString(strFechaCobro),
            		                 "pstrObservaciones", strObservaciones,
            		                 "pstrFlagIndividual", strFlagIndividual,
            		                 "pstrInstancia", strInstancia,
            		                 "pstrNumeroSecuencia", strNumeroSecuencia,
            		                 "pstrImporteIGV", strImporteIGV
                                    ];

        if (pModo) {

            //            fn_mdl_confirma('¿Esta seguro de grabar?',
            //            function() {
            //                parent.fn_blockUI();
            fn_util_AjaxWM("frmCobroRegistro.aspx/GrabaCobro",
                    arrParametros,
                    function(result) {
                        if (fn_util_trim(result) == "0") {
                            parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "GRABAR COBRO");
                        } else {
                            fn_SetearFlagRegistro('1');
                            if ($("#hidOpcion").val() == C_TX_NUEVO) {
                                fn_RedireccionNuevo();
                                fn_SetearNuevo();
                                parent.fn_unBlockUI();
                                fn_util_MuestraLogPage("El cobro se grabó correctamente.", "I");
                                //parent.fn_mdl_mensajeOk("El cobro se grabó correctamente.", function() { fn_RedireccionGrabar(); }, "GRABADO CORRECTO");
                            } else {
                                parent.fn_unBlockUI();
                                fn_util_MuestraLogPage("El cobro se actualizó correctamente.", "I");
                                //parent.fn_mdl_mensajeOk("El cobro se actualizó correctamente", function() { fn_IncializaEditar(); }, "GRABADO CORRECTO");
                            }

                        }
                    },
                     function(resultado) {
                         parent.fn_unBlockUI();
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "GRABAR COBRO");
                     }

                );
            //            },
            //            "../../util/images/question.gif",
            //            function() { },
            //            'Otros Conceptos :: Cobros'
            //         );
        }
        else {

            fn_IncializaEditar();
            if ((pPaginado != 'x') && (pPaginado != '')) {
                if (strEstadoCobro == 'C') {
                    fn_util_AjaxWM("frmCobroRegistro.aspx/GrabaCobro",
                        arrParametros,
                        function(result) {
                            if (fn_util_trim(result) == "0") {
                                parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "GRABAR COBRO");
                            }
                            else {
                                fn_SetearFlagRegistro('1');
                                fn_SetearParametro(pPaginado);
                            }
                        },
                         function(resultado) {
                             var error = eval("(" + resultado.responseText + ")");
                             parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "GRABAR COBRO");
                         }

                    );
                } else { fn_SetearParametro(pPaginado); }

            }
            else if (pPaginado == 'x') {
                if (strEstadoCobro == 'C') {
                    fn_util_AjaxWM("frmCobroRegistro.aspx/GrabaCobro",
                        arrParametros,
                        function(result) {
                            if (fn_util_trim(result) == "0") {
                                parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "GRABAR COBRO");
                            }
                            else {
                                fn_SetearFlagRegistro('1');
                                fn_BuscarItem();
                            }
                        },
                         function(resultado) {
                             var error = eval("(" + resultado.responseText + ")");
                             parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "GRABAR COBRO");
                         }

                    );
                } else { fn_SetearParametro(pPaginado); }
            }
        }
    }
}

//****************************************************************
// Función		:: 	fn_SetearFlagRegistro
// Descripción	::	
// Log			:: 	WCR - 13/12/2012
//****************************************************************
function fn_SetearFlagRegistro(pFlag) {
    var winPag = window.parent.frames[0].document;
    var hidRegistro = winPag.getElementById("hidFlagRegistro");
    if (hidRegistro != null) { hidRegistro.value = pFlag; }

}

//****************************************************************
// Función		:: 	fn_RedireccionGrabar
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
//                  Luego lo redirecciona al formulario de resgistro de cobro.
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_RedireccionGrabar() {
    var winPag = window.parent.frames[0];
    winPag.fn_ActualizarListado();
    parent.fn_util_CierraModal();
}

//****************************************************************
// Función		:: 	fn_RedireccionNuevo
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
//                  Luego lo redirecciona al formulario de resgistro de cobro.
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_RedireccionNuevo() {
    var winPag = window.parent.frames[0];
    winPag.fn_ActualizarListado();
    //parent.fn_util_CierraModal();
}

//****************************************************************
// Función		:: 	fn_ListaContrato
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_ListaContrato() {
    var ctrlBtn = window.parent.frames[0].document.getElementById('cmdListarContrato');
    ctrlBtn.click();

    parent.fn_util_CierraModal();
}

//****************************************************************
// Función		:: 	fn_buscarContrato
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_buscarContrato() {
    if (fn_util_trim($('#txtNroContrato').val()) != '') {
        var arrParametros = ["pstrNumeroContrato", $('#txtNroContrato').val()];
        var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whObtenerContrato', '../../');
        if (arrResultado.length > 0) {
            if (arrResultado[0] == "0") {

                $('#hidCodSolicitudCredito').val(arrResultado[1]);
                $('#txtNroContrato').val(arrResultado[1]);
                $('#txtTipoDocumento').val(arrResultado[3]);
                $('#txtNroDocumento').val(arrResultado[4]);
                $("#txtRazonSocialProveedor").val(arrResultado[5]);
                $('#txtEstadoContrato').val(arrResultado[11]);
                $('#cboMoneda').val(arrResultado[7]);
                $('#hidFechaActivacion').val(arrResultado[12]);
                $('#hidFechaVencmiento').val(arrResultado[13]);
                $('#hidPorcentajeComisionSC').val(arrResultado[14]);
            }
            else {
                var strError = arrResultado[1];
                fn_mdl_alert(strError.toString(), function() {
                    $('#hidCodSolicitudCredito').val('');
                    $('#txtNroContrato').val('');
                    $('#txtTipoDocumento').val('');
                    $('#txtNroDocumento').val('');
                    $("#txtRazonSocialProveedor").val('');
                    $("#cboMoneda option:first").attr('selected', 'selected');
                    $('#hidFechaActivacion').val('');
                    $('#hidFechaVencmiento').val('');
                    $('#hidPorcentajeComisionSC').val('0');
                    $('#txtNroContrato').focus();
                });
            }
        }
    }
    else { fn_mdl_alert('Ingrese un número de contrato', function() { }) }
}

//****************************************************************
// Función		:: 	fn_VentanaContrato
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_VentanaContrato() {
    //    var sTitulo = "Otros Conceptos";
    //    var sSubTitulo = "Cobros:: Búsqueda por Contrato";
    //    parent.fn_util_AbreModal2(sSubTitulo, "GestionBien/OtrosConceptos/frmBuscarContrato.aspx?Titulo=" + sTitulo + "&SubTitulo=" + sSubTitulo + "&hddCodConDoc=0", 1100, 520, function() { });
    fn_buscarContrato();
}

//****************************************************************
// Función		:: 	fn_CalculoComision
// Descripción	::	
// Log			:: 	WCR - 29/11/2012
//****************************************************************
function fn_CalculoComision() {

    var strConcepto = $("#cboConcepto option:selected").val() == "0" ? "" : $("#cboConcepto option:selected").val();
    var strImporte = $('#txtImporte').val() == undefined ? "" : $('#txtImporte').val();
    var strMoneda = $("#cboMoneda option:selected").val() == "0" ? "" : $("#cboMoneda option:selected").val();
    if (strImporte == "") { strImporte = "0"; }
    var decImporte = fn_util_ValidaDecimal(strImporte);
    var decComisionSC = fn_util_ValidaDecimal($('#hidPorcentajeComisionSC').val());
    
    if ((strConcepto != '') && (strMoneda != '')) {

        var arrParametros = ["pstrCodigoConcepto", strConcepto,
                             "pstrImporte", strImporte,
                             "pstrCodMoneda", strMoneda,
                            ];
        var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whCalculoComision', '../../');
        if (arrResultado.length > 0) {
            if (arrResultado[0] == "0") {
                if (fn_util_ValidaDecimal($("#hidIGV").val()) == 0) { $("#hidIGV").val(arrResultado[3]); }
                var decPorcentajeIGV = fn_util_ValidaDecimal($("#hidIGV").val());
                var decComision = 0;
                if ((decComisionSC > -1) && (strConcepto == C_CONCEPTOS_OpcionCompra)) { decComision = decImporte * (decComisionSC / 100); }
                else { decComision = parseFloat(arrResultado[1]); }
                var decIGV = decComision * decPorcentajeIGV;
                var decTotal = decImporte + decComision + decIGV;
                $('#hidMontoMinimo').val(arrResultado[4]);
                $('#hidMontoMaximo').val(arrResultado[5]);
                $("#txtComision").val(fn_util_AddCommas(fn_util_RedondearDecimales(decComision, 2)));
                $("#txtIGVComision").val(fn_util_AddCommas(fn_util_RedondearDecimales(decIGV, 2)));
                $("#txtTotal").val(fn_util_AddCommas(fn_util_RedondearDecimales(decTotal, 2)));

                //if (arrResultado[2] != '') { fn_mdl_alert(arrResultado[2], function() { }) }
            }
            else {
                var strError = arrResultado[1];
                fn_mdl_alert(strError.toString(), function() {
                    $("#txtComision").val('0.00');
                    $("#txtIGVComision").val('0.00');
                    $("#txtTotal").val('0.00');
                    $('#hidMontoMinimo').val('0.00');
                    $('#hidMontoMaximo').val('0.00');
                });
            }
        }
    }
    else {
        $("#txtComision").val('0.00');
        $("#txtIGVComision").val('0.00');
        $("#txtTotal").val('0.00');
    }
}

function fn_CalculoTotal() {

    var strImporte = $('#txtImporte').val() == undefined ? "" : $('#txtImporte').val();
    var strComision = $('#txtComision').val() == undefined ? "" : $('#txtComision').val();
    if (strImporte == "") { strImporte = "0"; }
    var decImporte = fn_util_ValidaDecimal(strImporte);
    if (strComision == "") { strComision = "0"; }
    var decComision = fn_util_ValidaDecimal(strComision);

    var decIGV = decComision * 0.18; ;
    var decTotal = decImporte + decComision + decIGV;

    $("#txtIGVComision").val(fn_util_AddCommas(fn_util_RedondearDecimales(decIGV, 2)));
    $("#txtTotal").val(fn_util_AddCommas(fn_util_RedondearDecimales(decTotal, 2)));
}

//****************************************************************
// Función		:: 	fn_ValidarRegistro
// Descripción	::	
// Log			:: 	WCR - 29/11/2012
//****************************************************************
function fn_ValidarRegistro(sbError) {
    var txtNroContrato = $('input[id=txtNroContrato]:text');
    var cboConcepto = $('select[id=cboConcepto]');
    var cboMoneda = $('select[id=cboMoneda]');
    var txtComision = $('input[id=txtComision]:text');
    var txtFechaCobro = $('input[id=txtFechaCobro]:text');
    var strEstadoCobro = $('#hidEstadoCobro').val() == undefined ? "" : $('#hidEstadoCobro').val();

    if (strEstadoCobro == 'C') {
        sbError.append(fn_util_ValidateControl(txtNroContrato[0], 'un número de contrato', 1, ''));
        sbError.append(fn_util_ValidateControl(cboConcepto[0], 'un concepto', 1, ''));
        sbError.append(fn_util_ValidateControl(cboMoneda[0], 'una moneda', 1, ''));
        sbError.append(fn_util_ValidateControl(txtComision[0], 'una comisión', 1, ''));
        //sbError.append(fn_util_ValidateControl(txtFechaCobro[0], 'una fecha de cobro', 1, ''));

        var strNroContrato = txtNroContrato[0].value == undefined ? "" : txtNroContrato[0].value;
        var strConcepto = cboConcepto[0].value == "0" ? "" : cboConcepto[0].value;
        var strMoneda = cboMoneda[0].value == "0" ? "" : cboMoneda[0].value;
        var strFechaCobro = txtFechaCobro[0].value == undefined ? "" : txtFechaCobro[0].value;
        var strComision = txtComision[0].value == undefined ? "" : txtComision[0].value;

        if ($("#hidOpcion").val() == C_TX_NUEVO) {
            if (strNroContrato != '') {
                if ($("#imgBuscarContrato").css("display") != 'none') {
                    fn_util_SeteaObligatorio($("#txtNroContrato"), "input");
                }
            }
            if (strConcepto != '') { fn_util_SeteaObligatorio($("#cboConcepto"), "select"); }
            if (strMoneda != '') { fn_util_SeteaObligatorio($("#cboMoneda"), "select"); }
        }
        else {
            if (($('#hidFlagIndividual').val() == '1') && (strMoneda != '')) { fn_util_SeteaObligatorio($("#cboMoneda"), "select"); }
        }
        if (strComision == '') { strComision = '0'; }
        if (parseFloat(strComision) > 0) {
            fn_util_SeteaObligatorio($("#txtComision"), "input");

            var strMontoMinimo = $('#hidMontoMinimo').val() == undefined ? "" : $('#hidMontoMinimo').val();
            if (strMontoMinimo == '') { strMontoMinimo = '0'; }
            var strMontoMaximo = $('#hidMontoMaximo').val() == undefined ? "" : $('#hidMontoMaximo').val();
            if (strMontoMaximo == '') { strMontoMaximo = '0'; }

            var decMontoComision = fn_util_ValidaDecimal(strComision);
            var decMontoMinimo = fn_util_ValidaDecimal(strMontoMinimo);
            var decMontoMaximo = fn_util_ValidaDecimal(strMontoMaximo);

            if (decMontoMinimo > 0) {
                if (decMontoComision < decMontoMinimo) {
                    sbError.append('&nbsp;&nbsp;- El monto mínimo de la comisión es ' + fn_util_AddCommas(fn_util_RedondearDecimales(decMontoMinimo, 2)) + '<br/>&nbsp;&nbsp;&nbsp;&nbsp;' + $("#cboMoneda option:selected").text() + '<br/>');
                }
            }
            if (decMontoMaximo > 0) {
                if (decMontoComision > decMontoMaximo) {
                    sbError.append('&nbsp;&nbsp;- El monto máximo de la comisión es ' + fn_util_AddCommas(fn_util_RedondearDecimales(decMontoMaximo, 2)) + '<br/>&nbsp;&nbsp;&nbsp;&nbsp;' + $("#cboMoneda option:selected").text() + '<br/>');
                }
            }

        }
        //    var strFechaActual = $('#hidFechaActual').val() == undefined ? "" : $('#hidFechaActual').val();
        if (strFechaCobro != '') {

            fn_util_SeteaObligatorio($("#txtFechaCobro"), "calendar");

            //        if ((strFechaActual != '') && ($('#hidOpcion').val() == C_TX_NUEVO)) {
            //            if (strFechaCobro != strFechaActual) {
            //                var boolRpta = fn_util_ComparaFecha(strFechaActual, strFechaCobro)
            //                if (!boolRpta) { sbError.append('&nbsp;&nbsp;- La fecha de cobro no puede ser menor a la fecha<br/>&nbsp;&nbsp;&nbsp;&nbsp;actual'); }
            //            }
            //        }

            var arrFecha = strFechaCobro.split('/');
            var strFecha = arrFecha[2] + '-' + arrFecha[1] + '-' + arrFecha[0];

            var arrParametros = ["pstrFlag", "1",
                                 "pstrFecha", strFecha
                            ];
            var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whValidarFeriado', '../../');
            if (arrResultado.length > 0) {
                if (arrResultado[0] == "0") {
                    if (arrResultado[1] != '') {
                        var arrDato = arrResultado[1].split('*');
                        if (arrDato[1] != '') { sbError.append('&nbsp;&nbsp;- ' + arrDato[1].substring(0, 55) + '<br/>&nbsp;&nbsp;&nbsp;&nbsp;' + arrDato[1].substring(56, arrDato[1].length) + '<br/>'); }
                    }
                }
                else {
                    var strError = arrResultado[1];
                    sbError.append('&nbsp;&nbsp;- ' + arrDato[1] + '<br/>');
                }
            }

            if ($('#hidFechaPago').val() != '') {
                var boolRpta = fn_util_ComparaFecha($('#hidFechaPago').val(), strFechaCobro);
                if (!boolRpta) { sbError.append('&nbsp;&nbsp;- La fecha de cobro debe ser como mínimo la fecha de<br/>&nbsp;&nbsp;&nbsp;&nbsp; pago(' + $('#hidFechaPago').val() + ')<br/>'); }
            } else if ($('#hidFechaActivacion').val() != '') {
                var boolRpta = fn_util_ComparaFecha($('#hidFechaActivacion').val(), strFechaCobro);
                if (!boolRpta) { sbError.append('&nbsp;&nbsp;- La fecha de cobro debe ser como mínimo la fecha de<br/>&nbsp;&nbsp;&nbsp;&nbsp; activación del contrato(' + $('#hidFechaActivacion').val() + ')<br/>'); }
            }
            if ($('#hidFechaVencmiento').val() != '') {
                if ($('#hidFechaVencmiento').val() != strFechaCobro) {
                    var boolRpta = fn_util_ComparaFecha($('#hidFechaVencmiento').val(), strFechaCobro);
                    if (boolRpta) { sbError.append('&nbsp;&nbsp;- La fecha de cobro debe ser como máximo la fecha de<br/>&nbsp;&nbsp;&nbsp;&nbsp; vencimiento del contrato(' + $('#hidFechaVencmiento').val() + ')<br/>'); }
                }
            }

        } else {
            sbError.append(fn_util_ValidateControl(txtFechaCobro[0], 'una fecha de cobro', 1, ''));
            $('#txtFechaCobro').removeClass('css_input_error');
            $('#txtFechaCobro').addClass('css_calendario_error');
        }
    }
    return sbError.toString();
}


//****************************************************************
// Funcion		:: 	fn_ObtenerPaginado
// Descripción	::	
// Log			:: 	WCR - 03/12/2012
//****************************************************************
function fn_ObtenerPaginado() {
    try {
        //parent.fn_blockUI();

        var strNroLote = $('#hidFilNroLote').val() == undefined ? "" : $('#hidFilNroLote').val();
        var strRazonSocial = $('#hidFilRazonSocial').val() == undefined ? "" : $('#hidFilRazonSocial').val();
        var strNroContrato = $('#hidFilNroContrato').val() == undefined ? "" : $('#hidFilNroContrato').val();
        var strCUCliente = $('#hidFilCUCliente').val() == undefined ? "" : $('#hidFilCUCliente').val();
        var strSortName = $('#hidFilSortName').val() == undefined ? "" : $('#hidFilSortName').val();
        var strSortOrder = $('#hidFilSortOrder').val() == undefined ? "" : $('#hidFilSortOrder').val();
        var strItem = $('#hidFilItem').val() == undefined ? "" : $('#hidFilItem').val();

        var strInstancia = $('#hidInstancia').val() == undefined ? "" : $('#hidInstancia').val();
        if (strInstancia == '') { strInstancia = '0'; }
        if (parseFloat(strInstancia) > 0) { strNroContrato = ''; }



        var strNumSecRecuperacion = '0';
        var strNumSecRecupComi = '0';

        if ($('#hidEditarCons').val() == '1') {
            strNumSecRecuperacion = $('#hidNumSecRecuperacion').val() == undefined ? "" : $('#hidNumSecRecuperacion').val();
            strNumSecRecupComi = $('#hidNumSecRecupComi').val() == undefined ? "" : $('#hidNumSecRecupComi').val();
        }

        var strConcepto = $("#hidFilConcepto").val() == "0" ? "" : $("#hidFilConcepto").val();
        var strEstadoCobro = $("#hidFilEstadoCobro").val() == "0" ? "" : $("#hidFilEstadoCobro").val();

        var arrParametros = ["pSortColumn", strSortName, // Columna a ordenar.
                             "pSortOrder", strSortOrder, // Criterio de ordenación.
                             "pstrCUCliente", strCUCliente,
                             "pstrRazonSocial", strRazonSocial,
                             "pstrNroContrato", strNroContrato,
                             "pstrNroLote", strNroLote,
                             "pstrCodigoConcepto", strConcepto,
                             "pstrEstadoCobro", strEstadoCobro,
                             "pstrItem", strItem,
                             "pstrNumSecRecuperacion", strNumSecRecuperacion,
                             "pstrNumSecRecupComi", strNumSecRecupComi,
                             "pstrInstancia", strInstancia
                            ];


        fn_util_AjaxWM("frmCobroRegistro.aspx/PaginarRegistro",
                       arrParametros,
                       function(objResultado) {
                           fn_SetearPaginado(objResultado);
                           parent.fn_unBlockUI();
                       },
                       function(request) {

                           var error = eval("(" + request.responseText + ")");
                           parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "OBTENER PAGINADO");
                           parent.fn_unBlockUI();
                       });



    } catch (ex) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }
}

//****************************************************************
// Funcion		:: 	fn_SetearPaginado
// Descripción	::	
// Log			:: 	WCR - 03/12/2012
//****************************************************************
function fn_SetearPaginado(pResultado) {
    var objECreditoRecuperacionComision = pResultado;
    $('#tdTotal').html(objECreditoRecuperacionComision.TotalRegistros);
    $('#hidTotalRegistros').val(objECreditoRecuperacionComision.TotalRegistros);
    $('#hidPaginadoAnterior').val(objECreditoRecuperacionComision.Anterior);
    $('#hidPaginadoSiguiente').val(objECreditoRecuperacionComision.Siguiente);
    $('#hidPaginadoActual').val(objECreditoRecuperacionComision.Actual);

    var strActual = $('#hidPaginadoActual').val();
    var arrActual = strActual.split('|');
    $('#txtRegistro').val(arrActual[0]);

    if ($('#hidPressNumero').val() == '1') { fn_SetearParametro(strActual); }
}

//****************************************************************
// Funcion		:: 	fn_ObtenerCobro
// Descripción	::	
// Log			:: 	WCR - 03/12/2012
//****************************************************************
function fn_ObtenerCobro() {
    try {
        //parent.fn_blockUI();

        var strNroContrato = $('#hidCodSolicitudCredito').val() == undefined ? "" : $('#hidCodSolicitudCredito').val();
        var strTipoRubroFinanciamiento = $('#hidTipoRubroFinanciamiento').val() == undefined ? "" : $('#hidTipoRubroFinanciamiento').val();
        var strCodIfi = $('#hidCodIfi').val() == undefined ? "" : $('#hidCodIfi').val();
        var strTipoRecuperacion = $('#hidTipoRecuperacion').val() == undefined ? "" : $('#hidTipoRecuperacion').val();
        var strNumSecRecuperacion = $('#hidNumSecRecuperacion').val() == undefined ? "" : $('#hidNumSecRecuperacion').val();
        var strNumSecRecupComi = $('#hidNumSecRecupComi').val() == undefined ? "" : $('#hidNumSecRecupComi').val();
        var strCodComisionTipo = $('#hidCodComisionTipo').val() == undefined ? "" : $('#hidCodComisionTipo').val();


        var arrParametros = ["pstrNroContrato", strNroContrato,
                             "pstrTipoRubroFinanciamiento", strTipoRubroFinanciamiento,
                             "pstrCodIfi", strCodIfi,
                             "pstrTipoRecuperacion", strTipoRecuperacion,
                             "pstrNumSecRecuperacion", strNumSecRecuperacion,
                             "pstrNumSecRecupComi", strNumSecRecupComi,
                             "pstrCodComisionTipo", strCodComisionTipo
                             ];


        fn_util_AjaxWM("frmCobroRegistro.aspx/ObtenerCobro",
                       arrParametros,
                       function(objResultado) {
                           fn_SetearDatosCobros(objResultado);
                           parent.fn_unBlockUI();
                       },
                       function(request) {
                           var error = eval("(" + request.responseText + ")");
                           parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "OBTENER COBRO");
                           parent.fn_unBlockUI();
                       });



    } catch (ex) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }
}

//****************************************************************
// Funcion		:: 	fn_SetearDatosCobros
// Descripción	::	
// Log			:: 	WCR - 03/12/2012
//****************************************************************
function fn_SetearDatosCobros(pResultado) {
    var objECreditoRecuperacionComision = pResultado;

    $('#txtNroContrato').val(objECreditoRecuperacionComision.CodOperacionActiva);
    $('#txtTipoDocumento').val(fn_util_trim(objECreditoRecuperacionComision.TipoDocumento));
    $('#txtNroDocumento').val(fn_util_trim(objECreditoRecuperacionComision.NumeroDocumento));
    $('#txtRazonSocialProveedor').val(fn_util_trim(objECreditoRecuperacionComision.RazonSocial));
    $('#txtEstadoContrato').val(fn_util_trim(objECreditoRecuperacionComision.EstadoContrato));
    $('#cboConcepto').val(objECreditoRecuperacionComision.CodComisionTipo);
    $('#cboMoneda').val(objECreditoRecuperacionComision.CodMoneda);
    $('#txtImporte').val(fn_util_AddCommas(fn_util_RedondearDecimales(objECreditoRecuperacionComision.MontoReembolso, 2)));
    $('#txtComision').val(fn_util_AddCommas(fn_util_RedondearDecimales(objECreditoRecuperacionComision.MontoComision, 2)));
    $('#txtConsComision').val($('#txtComision').val());
    $('#txtIGVComision').val(fn_util_AddCommas(fn_util_RedondearDecimales(objECreditoRecuperacionComision.MontoIGV, 2)));
    $('#txtTotal').val(fn_util_AddCommas(fn_util_RedondearDecimales(objECreditoRecuperacionComision.Total, 2)));
    $('#txtFechaCobro').val(objECreditoRecuperacionComision.StringFechaCobro);
    $('#txtConsFechaCobro').val(objECreditoRecuperacionComision.StringFechaCobro);
    $('#txtEstadoCobro').val(fn_util_trim(objECreditoRecuperacionComision.EstadoCobro));
    $('#txtObservaciones').val(objECreditoRecuperacionComision.Observaciones);
    $('#hidEstadoCobro').val(objECreditoRecuperacionComision.EstadoRecuperacion);
    $('#hidFlagIndividual').val(objECreditoRecuperacionComision.FlagIndividual);
    $('#hidFechaPago').val(objECreditoRecuperacionComision.StringFechaPago);
    $('#hidFechaActivacion').val(objECreditoRecuperacionComision.StringFechaActivacion);
    $('#hidFechaVencmiento').val(objECreditoRecuperacionComision.StringFechaVencimientoOperacion);
    $('#hidPorcentajeComisionSC').val(objECreditoRecuperacionComision.PorcenComision);
    $('#hidNumeroSecuencia').val(objECreditoRecuperacionComision.NumeroSecuencia);

    $('#txtImporte').addClass('css_input');
    $('#txtImporte').removeClass('css_input_inactivo');
    $('#txtImporte').removeAttr('readonly');

    if ($('#hidFlagIndividual').val() == '1') {

        $('txtImporte').attr('onkeydown', '').unbind('onkeydown');
        $('txtImporte').attr('onblur', '').unbind('onblur');

        $('#txtImporte').keydown(function(event) {
            if (event.which || event.keyCode) {
                if ((event.which == 13) || (event.keyCode == 13)) {
                    fn_CalculoComision();
                    return false;
                }
            }
            else {
                return true
            }
        });
        $('#txtImporte').blur(function() {
            fn_CalculoComision();
        });
        fn_util_SeteaObligatorio($("#cboMoneda"), "select");
    } else {
        $('txtImporte').attr('onkeydown', '').unbind('onkeydown');
        $('txtImporte').attr('onblur', '').unbind('onblur');
        $('#txtImporte').removeClass('css_input');
        $('#txtImporte').addClass('css_input_inactivo');
        $('#txtImporte').attr('readonly', true);
        $('#cboMoneda').attr('disabled', 'disabled');

    }
    if ($('#hidPressNumero').val() == '0') { fn_ObtenerPaginado(); } else { $('#hidPressNumero').val('0'); }
    if (objECreditoRecuperacionComision.EstadoRecuperacion != 'C') { fn_ModoConsulta(); }
    else if (parseInt(objECreditoRecuperacionComision.CantidadFraccionar, 10) > 0) { fn_ModoConsulta(); }
    else {
        $('#dv_guardar').show();
        $('#txtConsFechaCobro').hide()
        $('#txtFechaCobro').show();
        $('#txtObservaciones').removeClass('css_input_inactivo');
        $('#txtObservaciones').removeAttr('readonly');
        $('#txtConsComision').hide();
        $('#txtComision').show();
    }
    fn_ObtenerMinimoMaximo();

}

//****************************************************************
// Funcion		:: 	fn_ModoConsulta
// Descripción	::	
// Log			:: 	WCR - 03/12/2012
//****************************************************************
function fn_ModoConsulta() {
    $('txtImporte').attr('onkeydown', '').unbind('onkeydown');
    $('txtImporte').attr('onblur', '').unbind('onblur');
    $('txtComision').attr('onkeydown', '').unbind('onkeydown');
    $('txtComision').attr('onblur', '').unbind('onblur');
    $('#cboConcepto').attr('disabled', 'disabled');
    $('#cboMoneda').attr('disabled', 'disabled');
    $('#txtImporte').removeClass('css_input');
    $('#txtImporte').addClass('css_input_inactivo');
    $('#txtImporte').attr('readonly', true);
    $('#txtConsComision').show();
    $('#txtComision').hide();
    $('#txtObservaciones').addClass('css_input_inactivo');
    $('#txtObservaciones').attr('readonly', true);
    $('#txtConsFechaCobro').show();
    $('#txtFechaCobro').hide();
    $('#dv_guardar').hide()
}

//****************************************************************
// Funcion		:: 	fn_SetearParametro
// Descripción	::	
// Log			:: 	WCR - 03/12/2012
//****************************************************************
function fn_SetearParametro(pDatos) {
    if (pDatos != '') {
        var arrDatos = pDatos.split('|');
        $('#hidCodSolicitudCredito').val(arrDatos[1]);
        $('#hidTipoRubroFinanciamiento').val(arrDatos[2]);
        $('#hidCodIfi').val(arrDatos[3]);
        $('#hidTipoRecuperacion').val(arrDatos[4]);
        $('#hidNumSecRecuperacion').val(arrDatos[5]);
        $('#hidNumSecRecupComi').val(arrDatos[6]);
        $('#hidCodComisionTipo').val(arrDatos[7]);
        $('#hidFilItem').val(arrDatos[0]);

        fn_ObtenerCobro();
    }
}

//****************************************************************
// Funcion		:: 	fn_BuscarItem
// Descripción	::	
// Log			:: 	WCR - 03/12/2012
//****************************************************************
function fn_BuscarItem() {
    if (parseFloat($('#tdTotal').html()) < parseFloat($('#txtRegistro').val())) { $('#txtRegistro').val($('#tdTotal').html()); }
    if (parseFloat($('#txtRegistro').val()) == 0) { $('#txtRegistro').val('1'); }
    $('#hidFilItem').val($('#txtRegistro').val());
    $('#hidPressNumero').val('1');
    fn_ObtenerPaginado();
}

//****************************************************************
// Funcion		:: 	fn_CerrarCobro
// Descripción	::	
// Log			:: 	WCR - 03/12/2012
//****************************************************************
function fn_CerrarCobro() {
    fn_RedireccionGrabar();
}




//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos() {
    var pstrCodContrato = fn_util_trim($("#hidCodSolicitudCredito").val());
    var pstrCodBien = "0";
    var pstrCodRelacionado = fn_util_trim($("#hidNumSecRecuperacion").val());
    var pstrCodTipo = C_GESTIONBIEN_OTROSCONCEPTOS;
    var strVer = '0';
    strEstado = $('#hidEstadoCobro').val();
    if (strEstado != 'C') { strVer = 1 }
    parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + '&hddVer=' + strVer, 800, 350, function() { });
}

//****************************************************************
// Función		:: 	fn_ObtenerMinimoMaximo
// Descripción	::	
// Log			:: 	WCR - 12/12/2012
//****************************************************************
function fn_ObtenerMinimoMaximo() {

    var strConcepto = $("#cboConcepto option:selected").val() == "0" ? "" : $("#cboConcepto option:selected").val();
    var strImporte = $('#txtImporte').val() == undefined ? "" : $('#txtImporte').val();
    var strMoneda = $("#cboMoneda option:selected").val() == "0" ? "" : $("#cboMoneda option:selected").val();
    if (strImporte == "") { strImporte = "0"; }
    var decImporte = fn_util_ValidaDecimal(strImporte);

    if ((strConcepto != '') && (strMoneda != '')) {
        var arrParametros = ["pstrCodigoConcepto", strConcepto,
                             "pstrImporte", strImporte,
                             "pstrCodMoneda", strMoneda,
                            ];
        var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whCalculoComision', '../../');
        if (arrResultado.length > 0) {
            if (arrResultado[0] == "0") {
                if (fn_util_ValidaDecimal($("#hidIGV").val()) == 0) { $("#hidIGV").val(arrResultado[3]); }
                $('#hidMontoMinimo').val(arrResultado[4]);
                $('#hidMontoMaximo').val(arrResultado[5]);
            }
            else {
                $('#hidMontoMinimo').val('0.00');
                $('#hidMontoMaximo').val('0.00');
            }
        }
    }
    else {
        $('#hidMontoMinimo').val('0.00');
        $('#hidMontoMaximo').val('0.00');
    }
}

//****************************************************************
// Función		:: 	fn_SetearNuevo
// Descripción	::	
// Log			:: 	WCR - 12/12/2012
//****************************************************************
function fn_SetearNuevo() {
    $('#dv_Documentos').hide();
    $('#tbPagina').hide();

    $("#cboMoneda option:first").attr('selected', 'selected');
    $("#cboConcepto option:first").attr('selected', 'selected');
    $("#txtImporte").val('');
    $("#txtComision").val('');
    $("#txtIGVComision").val('');
    $("#txtTotal").val('');
    $("#txtFechaCobro").val('');
    $("#txtObservaciones").val('');



    fn_util_SeteaObligatorio($("#cboMoneda"), "select");
    fn_util_SeteaObligatorio($("#cboConcepto"), "select");

    $('#txtImporte').bind('keydown', function(event) {
        if (event.which || event.keyCode) {
            if ((event.which == 13) || (event.keyCode == 13)) {
                fn_CalculoComision();
                return false;
            }
        }
        else {
            return true
        }
    });

    $('txtImporte').bind('onblur', function() { fn_CalculoComision(); });

    var strInstancia = $('#hidInstancia').val() == undefined ? "" : $('#hidInstancia').val();
    if (strInstancia == '') { strInstancia = '0'; }
    var strNroContrato = $('#hidFilNroContrato').val() == undefined ? "" : $('#hidFilNroContrato').val();

    if (strNroContrato != '') {
        $('#txtNroContrato').val(strNroContrato);
        fn_buscarContrato();
        $("#imgBuscarContrato").hide();
        $('#txtNroContrato').removeClass('css_input');
        $('#txtNroContrato').addClass('css_input_inactivo');
        $('#txtNroContrato').attr('readonly', true);
    }
    else {
        $("#txtNroContrato").val('');
        $("#txtTipoDocumento").val('');
        $("#txtNroDocumento").val('');
        $("#txtRazonSocialProveedor").val('');
        $("#txtEstadoContrato").val('');
        fn_util_SeteaObligatorio($("#txtNroContrato"), "input");
    }

}