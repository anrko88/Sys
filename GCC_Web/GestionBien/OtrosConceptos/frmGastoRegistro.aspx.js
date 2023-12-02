//****************************************************************
// Variables Globales
//****************************************************************
var strIdTabla = new Object();
strIdTabla.Detraccion = 'TBL180';
strIdTabla.TipoDocumento = 'TBL107';
strIdTabla.ConceptoRetencion = 'TBL185';
strIdTabla.Aduanas = 'TBL184';

var strTipoDocumentoIdentificacion = new Object();
strTipoDocumentoIdentificacion.Dni = '1';
strTipoDocumentoIdentificacion.Ruc = '2';
strTipoDocumentoIdentificacion.CarnetExt = '3';
strTipoDocumentoIdentificacion.Pasaporte = '5';
strTipoDocumentoIdentificacion.Otros = '6';

var strComprobante = new Object();
strComprobante.Factura = '01';
strComprobante.ReciboHonorario = '11';

var strModalidadTC = new Object();
strModalidadTC.Sunat = 'SBS';
strModalidadTC.Dia = 'PRF';

var strMonedaSoles = '001';
var strMonedaDolares = '002';
var intIgv = '18';                 //VALOR IGV
var int4ta = '10';                 //VALOR 4TA

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	WCR - 19/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();

    // Tipo Comprobante 1
    $('#imgNumeroTipo').click(function() {
        AbrirListaGenerico(strIdTabla.TipoDocumento, 'Tipo de Comprobante', 'NumeroTipo');
    });

    // Tipo Bien
    $('#imgTipoBien').click(function() {
        if ($('#chkDetraccion').is(':checked') == true) {
            AbrirListaGenerico(strIdTabla.Detraccion, 'Tipo Bien Detracción', 'Detraccion');
        }
        else if ($('#ChkRetencion').is(':checked') == true) {
            AbrirListaGenerico(strIdTabla.ConceptoRetencion, 'Tipo Bien Retención', 'Retencion');
        }
    });

    //Inicializa Si Tiene REtencion
    $('#hidAgenteRetencion').val('1');

    //************************************************************
    // Función		:: 	Evento Change de Opcion detraccion
    // Descripcion 	:: 	
    // Log			:: 	WCR - 19/11/2012
    //************************************************************
    $('#chkDetraccion').change(function() {
        if ($(this).is(':checked') == true) {
            fnHabiltarDetranReten(true);
            fn_TipoCambioSunat(strModalidadTC.Sunat);
            $("#txtMontoSoles").val(0.00);
            $("#txtMontoDolar").val(0.00);
            $("#txtNroConstancia").val('');
            $("#txtTipoBien").val('');
            $("#txtFechaCompro").val('');
            $('#lblTipoBien').html('');
        }
    });

    //************************************************************
    // Función		:: 	Evento Change de Opcion Retencion
    // Descripcion 	:: 	
    // Log			:: 	WCR - 19/11/2012
    //************************************************************
    $('#ChkRetencion').change(function() {
        if ($(this).is(':checked') == true) {
            fn_validaCheckRetencion();
        }
    });

    //************************************************************
    // Función		:: 	Evento Change de Opcion Ninguno
    // Descripcion 	:: 	
    // Log			:: 	WCR - 19/11/2012
    //************************************************************
    $('#chkNinguno').change(function() {
        if ($(this).is(':checked') == true) {
            fnHabiltarDetranReten(false);
            fn_TipoCambioSunat(strModalidadTC.Sunat);
        }
    });

    //---------------------------------
    //Moneda
    //---------------------------------
    $('#cmbMoneda').change(function() {
        var strValor = $(this).val();
        fn_calculaRenta4ta();
    });

    //************************************************************
    // Función		:: 	IGV
    // Descripcion 	:: 	
    // Log			:: 	WCR - 19/11/2012
    //************************************************************
    $("#txtPorcIGV").focusout(function() {

        if ($("#txtPorcIGV").val() > fn_util_ValidaDecimal(intIgv)) {
            $("#txtPorcIGV").val(fn_util_ValidaMonto(intIgv, 2));
            parent.fn_util_MuestraLogPage('El IGV no puede ser mayor a' + intIgv + "% ", "I");
        }

        fn_MontoIGV();
        fn_ValorDetraccion();
        fn_ValorRetencion();
        fn_ValorTotal();
        fn_activaRetencionAutomatica();
    });

    //************************************************************
    // Función		:: Numero IGV
    // Descripcion 	:: 	
    // Log			:: WCR - 19/11/2012	
    //************************************************************
    $("#txtNumeroIGV").focusout(function() {
        //if ($('#txtNumeroTipo').val() == strComprobante.CodigoDua) {            
        fn_ValorDetraccion();
        fn_ValorRetencion();
        fn_ValorPorcIgv();
        fn_ValorTotal();
        fn_activaRetencionAutomatica();
        //}
    });

    //************************************************************
    // Función		:: Numero IGV
    // Descripcion 	:: 	
    // Log			:: WCR - 19/11/2012	
    //************************************************************
    $("#txttcdia").focusout(function() {
        var decTCDiaCompra = fn_util_ValidaDecimal($("#hddtcdiaCompra").val());
        var decTCDiaVenta = fn_util_ValidaDecimal($("#hddtcdiaVenta").val());
        var decTCDia = fn_util_ValidaDecimal($("#txttcdia").val());

        if (decTCDia < decTCDiaCompra) {
            parent.fn_util_MuestraLogPage("El T.C. del día no puede ser menor al T.C. de Compra(" + $("#hddtcdiaCompra").val() + ")", "E");
            $("#txttcdia").val(fn_util_ValidaMonto($("#hddtxttcdia").val(), 6));
        }
        if (decTCDia > decTCDiaVenta) {
            parent.fn_util_MuestraLogPage("El T.C. del día no puede ser mayor al T.C. de Venta(" + $("#hddtcdiaVenta").val() + ")", "E");
            $("#txttcdia").val(fn_util_ValidaMonto($("#hddtxttcdia").val(), 6));
        }
        fn_activaRetencionAutomatica();
    });

    //************************************************************
    // Función		:: Valor No Grabado
    // Descripcion 	:: 	
    // Log			:: WCR - 19/11/2012	
    //************************************************************
    $("#txtValorNoGravado").focusout(function() {
        fn_ValorDetraccion();
        fn_ValorRetencion();

        if ($('#txtNumeroTipo').val() == strComprobante.ReciboHonorario) {
            fn_calculaRenta4ta();
        }

        if (fn_util_ValidaMonto($("#txtNumeroIGV").val()) == fn_util_ValidaMonto(0, 2)) {
            //alert("ole")
            $('#chkNinguno').attr('checked', true);
            fnHabiltarDetranReten(false);
        }

        fn_ValorTotal();
    });


    //************************************************************
    // Función		:: Valor Grabado
    // Descripcion 	:: 	
    // Log			:: WCR - 19/11/2012	
    //************************************************************
    $("#txtGravado").focusout(function() {
        fn_MontoIGV();
        fn_ValorDetraccion();
        fn_ValorRetencion();
        fn_ValorTotal();
        fn_activaRetencionAutomatica();
    });

    //************************************************************
    // Función		:: Porc 4ta
    // Descripcion 	:: 	
    // Log			:: WCR - 19/11/2012	
    //************************************************************
    $("#txtPorc4ta").focusout(function() {
        fn_calculaRenta4ta();
    });

    fn_util_SeteaCalendarioFunction($("#txtFechaEmision"),
									function() {
									    fn_TipoCambioSunat(strModalidadTC.Sunat);
									    fn_ValorDetraccion();
									    fn_ValorRetencion();
									});

    //************************************************************
    // Función		:: Tipo Documento
    // Descripcion 	:: 	
    // Log			::
    //************************************************************

    $('#cmdTipoDoc').change(function() {
        var strValor = $(this).val();
        $("#txtNroDocProveed").val("");
        $('#txtNroDocProveed').unbind('keypress');
        if (fn_util_trim(strValor) == strTipoDocumentoIdentificacion.Dni) {
            $('#txtNroDocProveed').validText({ type: 'number', length: 8 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoIdentificacion.Ruc) {
            $('#txtNroDocProveed').validText({ type: 'number', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoIdentificacion.CarnetExt) {
            $('#txtNroDocProveed').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoIdentificacion.Pasaporte) {
            $('#txtNroDocProveed').validText({ type: 'alphanumeric', length: 11 });
        } else {
            $('#txtNroDocProveed').validText({ type: 'alphanumeric', length: 11 });
        }
    });

    //fn_cargaGrillaDocumentos
    fn_cargaGrillaDocumentos()

    //On load Page (siempre al final)
    fn_onLoadPage();

});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	WCR - 19/11/2012
//****************************************************************
function fn_inicializaCampos() {
    fn_util_SeteaObligatorio($("#cmbMoneda"), "select");
    fn_util_SeteaObligatorio($("#cmdTipoDoc"), "select");
    fn_util_SeteaObligatorio($("#txtNroDocProveed"), "input");

    //Valida Tipo de Datos
    $('#txtNumeroTipo').validText({ type: 'number', length: 3 });
    $('#txtTipoBien').validText({ type: 'number', length: 3 });
    $("#txtPorcIGV").validNumber({ value: '', decimals: 2, length: 5 });
    $('#txtSerieDoc1').validText({ type: 'alphanumeric', length: 4 });
    $('#txtNumeroDoc1').validText({ type: 'number', length: 16 });
    $('#txtNroConstancia').validText({ type: 'number', length: 20 });
    $("#txtGravado").validNumber({ value: '' });
    $("#txtPorcIGV").validNumber({ value: '' });
    $("#txtNumeroIGV").validNumber({ value: '' });
    $('#txttcdia').validNumber({ value: '', decimals: 6, length: 9 });
    $("#txttcespecial").validNumber({ value: '', decimals: 6, length: 9 });
    $("#txtValorNoGravado").validNumber({ value: '' });
    $('#txttcdia').val($("#hddtxttcdia").val());
    $('#txtTipoCambioSunat').val($("#hidTipoCambioSunat").val());
    $("#txttcespecial").attr('disabled', 'disabled');
    $("#txtTipoCambioSunat").attr('disabled', 'disabled');
    $("#chkSunat").attr('disabled', 'disabled');
    $('#txtTipoBien').attr('disabled', 'disabled');
    $('#imgTipoBien').attr('disabled', 'disabled');
    $('#txtNroConstancia').attr('disabled', 'disabled');
    $('#txtFechaCompro').attr('disabled', 'disabled');
    $('#txtMontoSoles').attr('disabled', 'disabled');
    $('#txtMontoDolar').attr('disabled', 'disabled');
    $('#txtTipoBien').removeClass();
    $('#txtTipoBien').addClass('css_input_inactivo');
    $('#txtNroConstancia').removeClass('css_input');
    $('#txtNroConstancia').addClass('css_input_inactivo');
    $('#txtFechaCompro').removeClass('css_input');
    $('#txtFechaCompro').addClass('css_input_inactivo');
    $('#txtMontoSoles').removeClass('css_input');
    $('#txtMontoSoles').addClass('css_input_inactivo');
    $('#txtMontoDolar').removeClass('css_input');
    $('#txtMontoDolar').addClass('css_input_inactivo');
    $('#dv_cancelar').css("display", "none");
    $('#dv_Modificar').css("display", "none");
    $("#chkNinguno").attr("checked", true);


    $("#dv_contenedor").addClass("css_scrollPane");
    $('#cmdTipoDoc').val(strTipoDocumentoIdentificacion.Ruc);
    $('#txtNroDocProveed').validText({ type: 'number', length: 11 });

    $("#txtPorcIGV").val(fn_util_ValidaMonto(intIgv, 2));
    fn_TipoCambioDia(strModalidadTC.Sunat);

    $("#spn_4taLabel").hide();
    $("#spn_4taInput").hide();
    $("#txtPorc4ta").validNumber({ value: '', decimals: 2, length: 5 });
    $("#txtPorc4ta").val(fn_util_ValidaMonto(int4ta, 2));

}

function VentanaProveedores() {
    parent.fn_util_AbreModal("Formalización :: Búsqueda de Proveedor", "Comun/frmProveedorConsulta.aspx", 850, 600, function() { });
}


//*****************************************************************
// Funcion		:: 	fn_obtenerProveedor
// Descripción	::	Obtiene el registro seleccionado de la busqueda
//                  de proveedores
// Log			:: 	WCR - 15/05/2012
//*****************************************************************
function fn_obtenerProveedor(pCodProveedor, pCodTipoDoc, pRuc, pRazonSocial, pNacionalidad) {
    $('#hidCodProveedor').val(pCodProveedor);
    $('#txtRazonSocialProveedor').val(pRazonSocial);
    $('#txtNroDocProveed').val(pRuc);
    $("#cmdTipoDoc").val(pCodTipoDoc);
    fn_AgenteRetencion(pRuc);
    fn_activaRetencionAutomatica();
}

//****************************************************************
// Funcion		:: 	Agente Retencion
// Descripción	::	Busca Agente Retenedor
// Log			:: 	WCR - 19/11/2012
//****************************************************************
function fn_AgenteRetencion(pRuc) {
    if (pRuc != "") {
        var arrParametros = ["pNroDocumento", fn_util_trim(pRuc)];

        fn_util_AjaxSyncWM("frmGastoRegistro.aspx/AgenteRetencion",
                        arrParametros,
                        function(resultado) {
                            $('#hidAgenteRetencion').val(resultado);
                        },
                        function(resultado) {

                            $('#hidAgenteRetencion').val('0');
                            $('#ChkRetencion').attr('disabled', 'disabled');

                            var error = eval("(" + resultado.responseText + ")");
                            parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR Al GUARDAR");
                        }
            );
    } else {
        //parent.fn_mdl_mensajeIco("Debe seleccionar un Proveedor.", "util/images/warning.gif", "ERROR AL ANULAR");

    }
}


//****************************************************************
// Funcion		:: 	TCEspecial
// Descripción	::	Tipo de Cambio Especial
// Log			:: 	WCR - 19/11/2012
//****************************************************************
function TCEspecial() {
    $('#txttcespecial').attr('value', '');

    if ($('#chktcespecial').is(':checked') == true) {
        $('#txttcdia').attr('disabled', false);
        $('#txttcdia').removeClass();
        $('#txttcdia').addClass('css_input_inactivo');
        $('#txttcespecial').removeAttr('disabled');
        $('#txttcespecial').removeClass();
        $('#txttcespecial').addClass('css_input');
    }
    else {

        $('#txttcdia').removeClass();
        $('#txttcdia').removeAttr('disabled');
        $('#txttcdia').addClass('css_input');
        $('#txttcdia').attr('value', $("#hddtxttcdia").val());

        $('#txttcespecial').attr('disabled', true);
        $('#txttcespecial').removeClass();
        $('#txttcespecial').addClass('css_input_inactivo');
    }
}

//****************************************************************
// Funcion		:: 	fn_DecripcionValorGenerico
// Descripción	::	Descripción de Valor Gnerica
// Log			:: 	WCR - 19/11/2012
//****************************************************************
function fn_DecripcionValorGenerico(strDominio, strParametro, strControlPrinc, strNombControl, strPorcentajeBien, strValorCompara, strTipo) {
    var arrParametros = ["pstrOp", "7", "pstrDominio", strDominio, "pstrParametro", strParametro];

    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');
    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            if (arrResultado[1] == "") {
                strNombControl.html('');
                strControlPrinc.val('');
                strError = "No Existe el Valor Buscado.";
                fn_mdl_alert(strError.toString(), function() { strControlPrinc.focus(); });
            } else {

                //alert(strTipo+"-"+arrResultado[0]+"-"+arrResultado[1]+"-"+arrResultado[2]+"-"+arrResultado[3]);					
                strNombControl.html(arrResultado[3] + ' - ' + arrResultado[1]);
                if (strDominio == strIdTabla.Detraccion) {
                    strNombControl.html(arrResultado[3] + ' - ' + arrResultado[1] + ' ' + arrResultado[4] + '%');
                }
                if (strTipo == 'D') {
                    if (strPorcentajeBien != null) {
                        strPorcentajeBien.val(arrResultado[4]);
                    }
                    if (strValorCompara != null) {
                        strValorCompara.val(arrResultado[2]);
                    }
                }
                if (strTipo == 'R') {
                    if (strPorcentajeBien != null) {
                        strPorcentajeBien.val(arrResultado[2]);
                    }
                    if (strValorCompara != null) {
                        strValorCompara.val(arrResultado[3]);
                    }
                    if (fn_util_trim(strParametro) != "001") {
                        strNombControl.html(arrResultado[3] + ' - ' + arrResultado[1] + ' ' + arrResultado[2] + '%');
                    }
                }
            }
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
}

//****************************************************************
// Funcion		:: 	fnHabiltarDetranReten
// Descripción	::	
// Log			:: 	WCR - 19/11/2012
//****************************************************************
function fnHabiltarDetranReten(boolValor) {

    if (boolValor) {
        $('#txtTipoBien').removeAttr('disabled');
        $('#imgTipoBien').removeAttr('disabled');
        $('#txtNroConstancia').removeAttr('disabled');
        $('#txtFechaCompro').removeAttr('disabled');
        $('#txtTipoBien').removeClass();
        $('#txtTipoBien').addClass('css_input');
        $('#txtNroConstancia').removeClass();
        $('#txtNroConstancia').addClass('css_input');
        $('#txtFechaCompro').removeClass();
        fn_util_SeteaCalendario($('input[id*=txtFechaCompro]'));
        //}

    } else {
        $('#txtTipoBien').val('');
        $('#lblTipoBien').html('');
        $('#txtNroConstancia').val('');
        $('#txtFechaCompro').val('');
        $('#txtMontoSoles').val('');
        $('#txtMontoDolar').val('');

        $('#txtTipoBien').attr('disabled', 'disabled');
        $('#imgTipoBien').attr('disabled', 'disabled');
        $('#txtNroConstancia').attr('disabled', 'disabled');
        $('#txtFechaCompro').attr('disabled', 'disabled');
        $('#txtMontoSoles').attr('disabled', 'disabled');
        $('#txtMontoDolar').attr('disabled', 'disabled');

        $('#txtTipoBien').removeClass();
        $('#txtTipoBien').addClass('css_input_inactivo');
        $('#txtNroConstancia').removeClass();
        $('#txtNroConstancia').addClass('css_input_inactivo');
        $('#txtFechaCompro').removeClass();
        $('#txtFechaCompro').addClass('css_input_inactivo');
        $('#txtMontoSoles').removeClass();
        $('#txtMontoSoles').addClass('css_input_inactivo');
        $('#txtMontoDolar').removeClass();
        $('#txtMontoDolar').addClass('css_input_inactivo');
    }
}


//****************************************************************
// Funcion		:: 	fn_MontoIGV
// Descripción	::	
// Log			:: 	WCR - 19/11/2012
//****************************************************************
function fn_MontoIGV() {
    if ($('#txtNumeroTipo').val() != strComprobante.CodigoDua) {
        var decValorGravado = fn_util_ValidaDecimal($('#txtGravado').val());
        var decValorIGV = fn_util_ValidaDecimal($('#txtPorcIGV').val());
        var decMonto = 0;

        decMonto = decValorGravado * (decValorIGV / 100);
        $('#txtNumeroIGV').val(fn_util_ValidaMonto(decMonto, 2));
        //Inicio IBK
        //RPH asignado al hidden el valor del igv para luego evaluarlo
        $('#hidNontoIGV').val(fn_util_ValidaMonto(decMonto, 2));
        //var igv = $('#hidNontoIGV').val();
        //fin IBK
    }
}

//****************************************************************
// Funcion		:: 	fn_ValorDetraccion
// Descripción	::	
// Log			::  WCR - 19/11/2012	
//****************************************************************
function fn_ValorDetraccion() {
    var decValorTotal = fn_util_ValidaDecimal($('#txtTotal').val());
    var decCompara = fn_util_ValidaDecimal($("#hidValorCompara").val());
    var decTipoCambio3 = fn_util_ValidaDecimal($("#txtTipoCambioSunat").val());
    var decTipoCambioCp = 0;

    //alert(decValorTotal+">"+decCompara+" && "+ $('#chkDetraccion').is(':checked') );

    if ((decValorTotal > decCompara) && (decCompara > 0) && ($('#chkDetraccion').is(':checked') == true)) {
        var decPorc = fn_util_ValidaDecimal($("#hidtxtTipoBien").val());
        var decMonto = 0;
        decMonto = (decValorTotal * (decPorc / 100));
        decTipoCambioCp = decTipoCambio3;

        if ($("#cmbMoneda").val() == strMonedaSoles) {
            $('#txtMontoSoles').val(fn_util_ValidaMonto(decMonto, 2));
            $('#txtMontoDolar').val(fn_util_ValidaMonto((decMonto / decTipoCambioCp), 2));
        }
        else {
            $('#txtMontoDolar').val(fn_util_ValidaMonto(decMonto, 2));
            $('#txtMontoSoles').val(fn_util_ValidaMonto((decMonto * decTipoCambioCp), 2));
        }
    } else {
        $('#txtMontoDolar').val(fn_util_ValidaMonto('0', 2));
        $('#txtMontoSoles').val(fn_util_ValidaMonto('0', 2));
    }
}

//****************************************************************
// Funcion		:: 	fn_ValorRetencion
// Descripción	::	
// Log			::  WCR - 19/11/2012	
//****************************************************************
function fn_ValorRetencion() {
    var decValorTotal = fn_util_ValidaDecimal($('#txtTotal').val());
    var decTipoCambio3 = fn_util_ValidaDecimal($("#txtTipoCambioSunat").val());
    var decTipoCambioCp = 0;
    if ($('#ChkRetencion').is(':checked') == true) {
        var decPorc = fn_util_ValidaDecimal($("#hidtxtTipoBien").val());
        var decMonto = 0;
        decMonto = (decValorTotal * (decPorc / 100));
        decTipoCambioCp = decTipoCambio3;
        if ($("#cmbMoneda").val() == strMonedaSoles) {
            $('#txtMontoSoles').val(fn_util_ValidaMonto(decMonto, 2));
            $('#txtMontoDolar').val(fn_util_ValidaMonto((decMonto / decTipoCambioCp), 2));
        }
        else {
            $('#txtMontoDolar').val(fn_util_ValidaMonto(decMonto, 2));
            $('#txtMontoSoles').val(fn_util_ValidaMonto((decMonto * decTipoCambioCp), 2));
        }
    }
}


//****************************************************************
// Funcion		:: 	fn_buscarProveedor
// Descripción	::	
// Log			:: 	WCR - 19/11/2012
//****************************************************************
function fn_buscarProveedor() {
    if ($('#txtNroDocProveed').attr('readonly') != 'readonly') {
        var strRuc = $('#txtNroDocProveed').val();
        var strtipo = $('#cmdTipoDoc').val();
        if (strRuc != '') {
            var arrParametros = ["pstrRuc", strRuc, "pstrTipoDocumento", strtipo];
            var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whProveedor', '../../');
            if (arrResultado.length > 0) {
                if (arrResultado[0] == "0") {
                    $('#hidCodProveedor').val(arrResultado[1]);
                    $('#txtRazonSocialProveedor').val(arrResultado[2]);
                    $("#cmdTipoDoc").val(arrResultado[3]);
                    fn_activaRetencionAutomatica();
                }
                else {
                    var strError = arrResultado[1];
                    fn_mdl_alert(strError.toString(), function() {
                        $('#txtNroDocProveed').val('');
                        $('#txtRazonSocialProveedor').val('');
                        $('#txtNroDocProveed').focus();
                    });
                }
            }
        }
        else {
            $('#hidCodProveedor').val('');
            $('#txtRazonSocialProveedor').val('');
            $("#cmdTipoDoc option").eq(0).attr("selected", "selected");
        }
        $('#hidAgenteRetencion').val("0");
        fn_AgenteRetencion($('#txtNroDocProveed').val());
    }

}

//****************************************************************
// Funcion		:: 	fn_NumeroTipo1
// Descripción	::	
// Log			::  WCR - 19/11/2012	
//****************************************************************
function fn_NumeroTipo1() {

    if ($('#txtNumeroTipo').val() != "") {

        fn_DecripcionValorGenerico(strIdTabla.TipoDocumento, $('#txtNumeroTipo').val(), $('#txtNumeroTipo'), $('#lblNumeroTipo'), '');

        //FechaEmision
        $("#txtFechaEmision").addClass('css_calendario');
        $("#txtFechaEmision").datepicker("enable");

        //Ini Checks
        fn_inicializaChecks();

        //4ta
        $("#spn_4taLabel").hide();
        $("#spn_4taInput").hide();
        $("#txtPorc4ta").val(fn_util_ValidaMonto(10, 2));
        $("#txtMonto4taSoles").val(fn_util_ValidaMonto(0, 2));
        $("#txtMonto4taDolares").val(fn_util_ValidaMonto(0, 2));


        if ($('#hidTipoComprobante').val() != $('#txtNumeroTipo').val()) {
            fn_MontoIGV();
            fn_ValorDetraccion();
            fn_ValorRetencion();
            fn_ValorTotal();
        }

        else if ($('#txtNumeroTipo').val() == strComprobante.ReciboHonorario) {

            $('#txtPorc4ta').val(fn_util_ValidaMonto(10, 2));
            $('#txtMonto4taSoles').val(fn_util_ValidaMonto(0, 2));
            $('#txtMonto4taDolares').val(fn_util_ValidaMonto(0, 2));

            $("#spn_4taLabel").show();
            $("#spn_4taInput").show();
            fn_calculaRenta4ta();
            $('#ChkRetencion').attr('checked', false);
            $('#chkDetraccion').attr('checked', false);
            $('#chkNinguno').attr('checked', true);
            fnHabiltarDetranReten(false);
            $('#chkDetraccion').attr('disabled', true);
            $('#ChkRetencion').attr('disabled', true);

        }
        else if ($('#txtNumeroTipo').val() == strComprobante.Factura) {

            $('#ChkRetencion').attr('checked', false);
            $('#chkDetraccion').attr('checked', false);
            $('#chkNinguno').attr('checked', true);

            fnHabiltarDetranReten(false);
            fn_activaRetencionAutomatica();

        }
        else {
            fn_AgenteRetencion($("#txtNroDocProveed").val());

            $('#chkDetraccion').attr('disabled', true);
            $('#ChkRetencion').attr('disabled', true);

        }

    }
}

//****************************************************************
// Funcion		:: 	fn_inicializaChecks
// Descripción	::	Inicializa Checks
// Log			:: 	WCR - 16/11/2012
//****************************************************************
function fn_inicializaChecks() {

    $('#chkDetraccion').removeAttr('disabled');
    $('#ChkRetencion').removeAttr('disabled');

    $('#ChkRetencion').attr('checked', false);
    $('#chkDetraccion').attr('checked', false);
    $('#chkNinguno').attr('checked', true);

    fnHabiltarDetranReten(false);
    fn_TipoCambioSunat(strModalidadTC.Sunat);

}

//****************************************************************
// Funcion		:: 	fn_activaRetencionAutomatica
// Descripción	::	Inicializa Retencion Automatica
// Log			:: 	WCR - 16/11/2012
//****************************************************************
function fn_activaRetencionAutomatica() {

    strRetencion = $("#hidAgenteRetencion").val();
    decMontoIGV = fn_util_ValidaDecimal($("#txtNumeroIGV").val());
    decTotal = fn_util_ValidaDecimal($("#txtTotal").val());
    strMoneda = $("#cmbMoneda").val();
    decTC = fn_util_ValidaDecimal($("#txttcdia").val());

    //Valida si Moneda es Dólares
    if (fn_util_trim(strMoneda) == "002") {
        decTotal = decTotal * decTC;
    }

    //alert(strRetencion +"-"+ decMontoIGV +"-"+ decTotal + "-"+ $("#ChkRetencion").prop("disabled"));
    if ($("#ChkRetencion").prop("disabled") == false) {
        if (strRetencion != "1" && decMontoIGV > 0 && decTotal > 700) {
            $('#ChkRetencion').attr('checked', true);
            $('#chkDetraccion').attr('checked', false);
            $('#chkNinguno').attr('checked', false);
            fn_validaCheckRetencion();
        } else {
            $('#ChkRetencion').attr('checked', false);
            $('#chkDetraccion').attr('checked', false);
            $('#chkNinguno').attr('checked', true);
            fnHabiltarDetranReten(false);
        }
    }
}

//****************************************************************
// Funcion		:: 	fn_TipoCambioSunat
// Descripción	::	
// Log			:: 	WCR - 19/11/2012
//****************************************************************
function fn_TipoCambioSunat(pstrModalidad) {

    if ($('#chkDetraccion').is(':checked') == true) {
        if ($("#txtFechaEmision").val() != "") {
            arrParametros = ["pstrCodMoneda", $("#hidCodMoneda").val(),
                              "pstrFecha", Fn_util_DateToString($("#txtFechaEmision").val()),
                              "pstrModalidad", pstrModalidad];

            fn_util_AjaxSyncWM("frmGastoRegistro.aspx/ConsultarTipoCambio",
                            arrParametros,
                            function(resultado) {
                                fnObtenerTipoCambioSunat(resultado);
                            },
                            function(resultado) {
                                parent.fn_unBlockUI();
                                var error = eval("(" + resultado.responseText + ")");
                                parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL DESEMBOLSAR");
                            }
            );
        } else {
            $("#hidTipoCambioSunat").val('0');
            $("#txtTipoCambioSunat").val('0');
        }
    } else {

        var arrParametros = ["pstrCodMoneda", $("#hidCodMoneda").val(),
                             "pstrFecha", Fn_util_DateToString($("#hddFechaActual").val()),
                             "pstrModalidad", pstrModalidad];

        fn_util_AjaxSyncWM("frmGastoRegistro.aspx/ConsultarTipoCambio",
        arrParametros,
                    function(resultado) {
                        fnObtenerTipoCambioSunat(resultado);
                    },
                    function(resultado) {
                        parent.fn_unBlockUI();
                        var error = eval("(" + resultado.responseText + ")");
                        parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL DESEMBOLSAR");
                    }
         );
    }
}

//****************************************************************
// Funcion		:: 	fn_validaCheckRetencion
// Descripción	::	Valida el check en Retencion
// Log			:: 	WCR - 16/11/2012
//****************************************************************
function fn_validaCheckRetencion() {

    if ($('#txtNumeroTipo').val() == strComprobante.Factura) {
        fnHabiltarDetranReten(true);
        $("#txtTipoBien").val(strCodigoRetencion); //Retencion 6%
        fn_TipoBien();
        $("#txtTipoBien").attr("disabled", true);
        $('#imgTipoBien').attr('disabled', 'disabled');
    }
    else {
        fnHabiltarDetranReten(false);
    }

}

//****************************************************************
// Funcion		:: 	fn_TipoBien()
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_TipoBien() {
    if ($('#txtTipoBien').val() != "") {
        if ($('#chkDetraccion').is(':checked') == true) {
            fn_DecripcionValorGenerico(strIdTabla.Detraccion, $('#txtTipoBien').val(), $('#txtTipoBien'), $('#lblTipoBien'), $("#hidtxtTipoBien"), $("#hidValorCompara"), 'D');
            fn_ValorDetraccion();
        }
        else if ($('#ChkRetencion').is(':checked') == true) {
            fn_TipoCambioSunat(strModalidadTC.Sunat);
            fn_DecripcionValorGenerico(strIdTabla.ConceptoRetencion, $('#txtTipoBien').val(), $('#txtTipoBien'), $('#lblTipoBien'), $("#hidtxtTipoBien"), $("#hidValorCompara"), 'R');
            fn_ValorRetencion();
        }
    } else {
        $('#lblTipoBien').html('');
    }
}

//****************************************************************
// Funcion		:: 	AbrirListaGenerico
// Descripción	::	
// Log			:: 	WCR - 19/11/2012
//****************************************************************
function AbrirListaGenerico(pintDominio, pstrTitulo, pstrProvino) {
    var strpagina = "Comun/frmListaValorGenerico.aspx?ncd=" + pintDominio + "&nt=" + pstrTitulo + "&np=" + pstrProvino;
    parent.fn_util_AbreModal("Listado :: Búsqueda Valor Genérico", strpagina, 700, 500, function() { });
}

//****************************************************************
// Funcion		:: 	fnObtenerTipoCambioSunat
// Descripción	::	
// Log			:: 	WCR - 19/11/2012
//****************************************************************
function fnObtenerTipoCambioSunat(resultado) {
    var varresult = resultado.split("|");
    var varTipoCambio;

    if (varresult[0] == "0") {
        varTipoCambio = varresult[1].split("$");
        $("#hidTipoCambioSunat").val(varTipoCambio[0]);
        $("#txtTipoCambioSunat").val($("#hidTipoCambioSunat").val());
    } else {
        parent.fn_mdl_mensajeIco(varresult[1], "util/images/error.gif", "ERROR OBTENER TIPO CAMBIO");
    }
}

//****************************************************************
// Funcion		:: 	fn_calculaRenta4ta
// Descripción	::	Renta 4ta
// Log			:: 	WCR - 19/11/2012
//****************************************************************
function fn_calculaRenta4ta() {
    var decMontoNOGrabado = fn_util_ValidaDecimal($("#txtValorNoGravado").val());
    var decMontoNOGrabadoValida = fn_util_ValidaDecimal($("#txtValorNoGravado").val());

    //Valida si Moneda es Dólares
    var strMoneda = $("#cmbMoneda").val();
    var decTC = fn_util_ValidaDecimal($("#txttcdia").val());
    if (fn_util_trim(strMoneda) == "002") {
        decMontoNOGrabadoValida = decMontoNOGrabado * decTC;
    }

    if (decMontoNOGrabadoValida > 1500) {

        var strMoneda = $("#cmbMoneda").val();
        var decTCSunat = fn_util_ValidaDecimal($("#txtTipoCambioSunat").val());

        var decPorc4ta = fn_util_ValidaDecimal($("#txtPorc4ta").val());
        var decMonto4ta = decMontoNOGrabado * (decPorc4ta / 100);
        var decMonto4taConvertido = 0;

        if (fn_util_trim(strMoneda) == "001") {
            $("#txtMonto4taSoles").val(fn_util_ValidaMonto(decMonto4ta, 2));
            decMonto4taConvertido = decMonto4ta / decTCSunat;
            $("#txtMonto4taDolares").val(fn_util_ValidaMonto(decMonto4taConvertido, 2));
        } else {
            $("#txtMonto4taDolares").val(fn_util_ValidaMonto(decMonto4ta, 2));
            decMonto4taConvertido = decMonto4ta * decTCSunat;
            $("#txtMonto4taSoles").val(fn_util_ValidaMonto(decMonto4taConvertido, 2));
        }

    } else {
        $("#txtMonto4taSoles").val(fn_util_ValidaMonto("0", 2))
        $("#txtMonto4taDolares").val(fn_util_ValidaMonto("0", 2))
    }
}

//****************************************************************
// Funcion		:: 	fn_ValorTotal
// Descripción	::	
// Log			:: 	WCR - 19/11/2012
//****************************************************************
function fn_ValorTotal() {
    var decValorGravado = fn_util_ValidaDecimal($('#txtGravado').val());
    var decValorMontoIGV = fn_util_ValidaDecimal($('#txtNumeroIGV').val());
    var decValorNoGravado = fn_util_ValidaDecimal($('#txtValorNoGravado').val());
    //alert(decValorGravado+decValorMontoIGV+decValorNoGravado);

    //Valida 4ta segun moneda
    var strMoneda = $("#cmbMoneda").val();
    var decRenta4ta = 0;
    if (fn_util_trim($('#txtNumeroTipo').val()) == strComprobante.ReciboHonorario) {
        if (fn_util_trim(strMoneda) == "001") {
            decRenta4ta = fn_util_ValidaDecimal($('#txtMonto4taSoles').val());
        } else {
            decRenta4ta = fn_util_ValidaDecimal($('#txtMonto4taDolares').val());
        }
    }

    var decMonto = 0;
    decMonto = decValorGravado + decValorMontoIGV + decValorNoGravado - decRenta4ta;
    $('#txtTotal').val(fn_util_ValidaMonto(decMonto, 2));
}

//****************************************************************
// Funcion		:: 	fn_ValorRetencion
// Descripción	::	
// Log			:: 	WCR - 19/11/2012
//****************************************************************
function fn_ValorPorcIgv() {
    var strGravado = $('#txtGravado').val();
    if (fn_util_trim(strGravado) == "") strGravado = "0"

    var decGravado = fn_util_ValidaDecimal(strGravado);
    var decNumeroIGV = fn_util_ValidaDecimal($('#txtNumeroIGV').val());

    var decNuevoIGV = 0;
    if (decGravado == 0) {
        decNuevoIGV = 0;
    } else {
        decNuevoIGV = (decNumeroIGV / decGravado) * 100;
    }
    //alert(decNumeroIGV+"/"+decGravado+"="+decNuevoIGV);		

    if (fn_util_trim($("#txtNumeroTipo").val()) != strComprobante.CodigoDua) {
        //Valida % no mayor a 18%
        if (decNuevoIGV > 18) {
            $('#txtPorcIGV').val(fn_util_ValidaMonto("18", 2));
            var decNuevoGrabado = (decGravado * 0.18);
            $('#txtNumeroIGV').val(fn_util_ValidaMonto(decNuevoGrabado, 2));
        } else {
            $('#txtPorcIGV').val(fn_util_ValidaMonto(decNuevoIGV, 2));
        }
    }

}


//****************************************************************
// Funcion		:: 	fn_SetearTipoComprobante()
// Descripción	::
// Log			::  WCR - 19/11/2012	
//****************************************************************
function fn_SetearTipoComprobante() {
    $('#hidTipoComprobante').val($('#txtNumeroTipo').val());
}

//****************************************************************
// Funcion		:: 	fn_TipoCambioDia
// Descripción	::
// Log			::  WCR - 19/11/2012	
//****************************************************************
function fn_TipoCambioDia(pstrModalidad) {
    var arrParametros = ["pstrCodMoneda", $("#hidCodMoneda").val(),
                         "pstrFecha", obtiene_fecha(),
                         "pstrModalidad", pstrModalidad
                        ];
    fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/ConsultarTipoCambio",
                            arrParametros,
                            function(resultado) {
                                fnObtenerTipoCambioDia(resultado);
                            },
                            function(resultado) {
                                parent.fn_unBlockUI();
                                var error = eval("(" + resultado.responseText + ")");
                                parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL DESEMBOLSAR");
                            }
     );
}

//****************************************************************
// Funcion		:: 	fnObtenerTipoCambioDia
// Descripción	::
// Log			:: 	WCR - 19/11/2012
//****************************************************************
function fnObtenerTipoCambioDia(resultado) {

    var varresult = resultado.split("|");
    var varTipoCambio;
    if (varresult[0] == "0") {
        varTipoCambio = varresult[1].split("$");
        if ($("#hidCodMoneda").val() == strMonedaSoles && $("#cmbMoneda").val() == strMonedaDolares) {
            $("#hidTipoCambioDia").val(varTipoCambio[0]);
        } else if ($("#hidCodMoneda").val() == strMonedaDolares && $("#cmbMoneda").val() == strMonedaSoles) {
            $("#hidTipoCambioDia").val(varTipoCambio[1]);
        }
        else {
            $("#hidTipoCambioDia").val(varTipoCambio[0]);
        }
        $("#txtTipoCambioSunat").val($("#hidTipoCambioDia").val());
    } else {
        parent.fn_mdl_mensajeIco(varresult[1], "util/images/error.gif", "ERROR OBTENER TIPO CAMBIO");
    }
}


//****************************************************************
// Funcion		:: 	obtiene_fecha
// Descripción	::
// Log			:: 	WCR - 19/11/2012
//****************************************************************
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
    return (anio + "" + mes + "" + dia);

}

//****************************************************************
// Funcion		:: 	fn_obtenerValorGenerico
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_obtenerValorGenerico(pstrProvino, pstrCodigo, pstrDescrip, pstrValor2, pstrValor3, pstrValor4) {

    if (pstrProvino == 'NumeroTipo') {
        $('#txtNumeroTipo').val(pstrCodigo);
        $('#lblNumeroTipo').html(pstrDescrip);
        fn_NumeroTipo1();
    }
    else if (pstrProvino == 'Detraccion') {
        $('#txtTipoBien').val(pstrCodigo);
        $('#lblTipoBien').html(pstrDescrip + ' ' + pstrValor2 + '%');
        $("#hidtxtTipoBien").val(pstrValor2);
        $("#hidValorCompara").val(pstrValor3);
        fn_ValorDetraccion();
    }
    else if (pstrProvino == 'Retencion') {
        $('#txtTipoBien').val(pstrCodigo);

        if (pstrCodigo == "001") $('#lblTipoBien').html(pstrDescrip);
        else $('#lblTipoBien').html(pstrDescrip + ' ' + pstrValor3 + '%');

        $("#hidtxtTipoBien").val(pstrValor3);
        fn_ValorRetencion();
    }
}




//****************************************************************
// Funcion		:: 	fn_cargaGrillaDocumentos
// Descripción	::	Inicializa Grilla Documentos
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_cargaGrillaDocumentos() {

    try {

        $("#jqGrid_lista_A").jqGrid({
            //            datatype: function() {
            //                fn_cargaAgrupacion();
            //            },
            datatype: "local",
            jsonReader:
			{
			    root: "Items",
			    page: "CurrentPage",
			    total: "PageCount",
			    records: "RecordCount",
			    repeatitems: false,
			    id: "Id"
			},
            colNames: ['Proveedor', 'Tipo Comprobante', 'N° Comprobante', 'Moneda', 'Total', 'Fecha Pago', 'Estado Pago', 'Estado Cobro', 'CantidadContrato', 'Contrato'],
            colModel: [
					{ name: 'NombreProveedor', index: 'NombreProveedor', width: 75, align: "left" },
					{ name: 'TipoComprobante', index: 'TipoComprobante', width: 55, align: "left" },
					{ name: 'NroComprobante', index: 'NroComprobante', width: 55, align: "center" },
					{ name: 'Moneda', index: 'Moneda', width: 55, align: "center" },
					{ name: 'Importe', index: 'Importe', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		            { name: 'FechaPago', index: 'FechaPago', align: "center", sortable: true, width: 35, sorttype: "string" },
		            { name: 'EstadoPago', index: 'EstadoPago', sortable: true, sorttype: "string", width: 35, align: "center" },
		            { name: 'EstadoCobro', index: 'EstadoCobro', sortable: true, sorttype: "string", width: 50, align: "center" },
		            { name: 'CantidadContrato', index: 'CantidadContrato', hidden: true, width: 80, align: "center", sortable: false },
					{ name: 'Contrato', index: 'Contrato', width: 25, align: "center", sortable: false, formatter: fn_Contrato }
			],
            width: glb_intWidthPantalla - 100,
            height: '100%',
            //pager: '#jqGrid_pager_A',
            loadtext: 'Cargando datos...',
            emptyrecords: 'No hay resultados',
            rowNum: 50,
            //rowList: [10, 20, 30],
            sortname: 'CodigoComprobante',
            sortorder: 'desc',
            viewrecords: true,
            //gridview: true,
            //autowidth: true,
            altRows: true,
            loadonce: false,
            altclass: 'gridAltClass',
            ondblClickRow: function(id) { },
            afterInsertRow: function(id) {
                var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
                if (parseInt(rowData.CantidadContrato) == 0) {
                    $("#" + id + " td.sgcollapsed", $("#jqGrid_lista_A")[0]).unbind('click').html('').removeProp('class');
                }
            },
            subGrid: true,
            subGridOptions: {
                "openicon": "ui-icon-arrowreturn-1-e"
            },
            subGridRowExpanded: function(subgrid_id, row_id) {
                var rowDataPadre = $("#jqGrid_lista_A").jqGrid('getRowData', row_id);
                //$("#hddCodigoAgrupacion").val(rowDataPadre.CodCorrelativo);

                var subgrid_table_id, pager_id;
                subgrid_table_id = subgrid_id + "_t";
                pager_id = "p_" + subgrid_table_id;
                $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pager_id + "' class='scroll'></div>");
                jQuery("#" + subgrid_table_id).jqGrid({
                    //                    datatype: function() {
                    //                        fn_cargaAgrupacionDocumento(subgrid_table_id);
                    //                    },
                    datatype: "local",
                    jsonReader:
					{
					    root: "Items",
					    page: "CurrentPage",
					    total: "PageCount",
					    records: "RecordCount",
					    repeatitems: false,
					    id: "Id"
					},
                    colNames: ['N° Contrato', 'Razón Social o Nombre', 'Concepto', 'Importe', 'Asumido Por', 'Cobrable', 'Fecha Cobro', 'Estado Cobro', 'Editar', 'Eliminar'],
                    colModel: [
						{ name: 'NroContrato', index: 'NroContrato', width: 30, align: "left" },
						{ name: 'RazonSocial', index: 'RazonSocial', width: 70, align: "left" },
						{ name: 'Concepto', index: 'Concepto', width: 70, align: "left" },
						{ name: 'Importe', index: 'Importe', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
						{ name: 'AsumidoPor', index: 'AsumidoPor', width: 40, align: "center" },
						{ name: 'Cobrable', index: 'Cobrable', width: 40, align: "center" },
						{ name: 'FechaCobro', index: 'FechaCobro', width: 40, align: "center" },
						{ name: 'EstadoCobro', index: 'EstadoCobro', width: 40, align: "center" },
						{ name: 'Editar', index: 'Editar', width: 20, align: "center", sortable: false, formatter: Editar },
						{ name: 'Eliminar', index: 'Eliminar', width: 20, align: "center", sortable: false, formatter: Eliminar }
					],
                    width: glb_intWidthPantalla - 135,
                    height: '100%',
                    loadtext: 'Cargando datos...',
                    emptyrecords: 'No hay resultados',
                    rowNum: 999,
                    rowList: [10, 20, 30],
                    sortname: 'CodigoContrato',
                    sortorder: 'desc',
                    viewrecords: true,
                    gridview: true,
                    //autowidth: true,
                    altRows: true,
                    altclass: 'gridAltClass',
                    multiselect: false,
                    gridComplete: function(id) {
                        fn_doResize();
                    }
                });

                function Editar(cellvalue, options, rowObject) {
                    var sScript = "javascript:fn_EditarContrato(" + rowObject.CodigoContrato + ");";
                    return "<img src='../../Util/images/ico_acc_editar.gif' alt='" + cellvalue + "' title=' Eliminar ' width='20px' onclick='" + sScript + "' style='cursor: pointer;cursor: hand;' />";
                };

                function Eliminar(cellvalue, options, rowObject) {
                    var sScript = "javascript:fn_EliminarContrato(" + rowObject.CodigoContrato + ");";
                    return "<img src='../../Util/images/ico_acc_eliminar.gif' alt='" + cellvalue + "' title=' Eliminar ' width='20px' onclick='" + sScript + "' style='cursor: pointer;cursor: hand;' />";
                };

                if (subgrid_table_id == "jqGrid_lista_A_1_t") {
                    var mydata2 =
                      [
		                { CodigoContrato: '1', NroContrato: '111111', RazonSocial: "TEAMSOFT", Concepto: "ASESORIA LEGAL EXTERNA", Importe: "200.25", AsumidoPor: "BANCO", Cobrable: "NO", FechaCobro: '21/10/2012', EstadoPago: "PAGADO", EstadoCobro: "COBRO PARCIAL", Editar: 'Editar', Eliminar: 'Eliminar' },
                        { CodigoContrato: '2', NroContrato: '222222', RazonSocial: "COSAPISOFT", Concepto: "GASTOS REGISTRALES", Importe: "400.00", AsumidoPor: "CLIENTE", Cobrable: "SI", FechaCobro: '20/10/2012', EstadoPago: "PAGADO", EstadoCobro: "COBRO TOTAL", Editar: 'Editar', Eliminar: 'Eliminar' }
		              ];
                    for (var i = 0; i <= mydata2.length; i++) {
                        jQuery("#" + subgrid_table_id).jqGrid('addRowData', i + 1, mydata2[i]);
                    }
                }
                if (subgrid_table_id == "jqGrid_lista_A_2_t") {
                    var mydata2 =
                      [
		                { CodigoContrato: '1', NroContrato: '333333', RazonSocial: "DELAWARE", Concepto: "GASTOS NOTARIALES", Importe: "3580.50", AsumidoPor: "BANCO", Cobrable: "NO", FechaCobro: '21/10/2012', EstadoPago: "PAGADO", EstadoCobro: "COBRO PARCIAL", Editar: 'Editar', Eliminar: 'Eliminar' },
		              ];
                    for (var i = 0; i <= mydata2.length; i++) {
                        jQuery("#" + subgrid_table_id).jqGrid('addRowData', i + 1, mydata2[i]);
                    }
                }
            }


        });

        jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_lista_A', { edit: false, add: false, del: false });
        $("#search_jqGrid_lista_A").hide();



        var mydata =
          [
		    { Id: 1, NombreProveedor: "TX DEVELOPERS", TipoComprobante: "FACTURA", NroComprobante: '045045771014', Moneda: "NUEVOS SOLES", Importe: "6000.25", FechaPago: "19/10/2012", EstadoPago: "PAGADO", EstadoCobro: "COBRO PARCIAL", CantidadContrato: '2', Contrato: "Contrato" },
            { Id: 2, NombreProveedor: "ESTUDIO NOTARIALES", TipoComprobante: "RECIBO POR HONORARIO", NroComprobante: '04777771088', Moneda: "DOLARES AMERICANOS", Importe: "3580.50", FechaPago: "20/10/2012", EstadoPago: "POR PAGAR", EstadoCobro: "COBRO PARCIAL", CantidadContrato: '1', Contrato: "Contrato" },
          	{ Id: 3, NombreProveedor: "IB-LIMA-ESTUDIO AZA", TipoComprobante: "IRPES", NroComprobante: '11698881777', Moneda: "DOLARES AMERICANOS", Importe: "1500.00", FechaPago: "21/10/2012", EstadoPago: "PENDIENTE", EstadoCobro: "COBRO TOTAL", CantidadContrato: '0', Contrato: "Contrato" }
		  ];

        for (var i = 0; i <= mydata.length; i++) {
            jQuery("#jqGrid_lista_A").jqGrid('addRowData', i + 1, mydata[i]);
        }

    } catch (e) {
        alert(e.message);
    }

    function fn_Contrato(cellvalue, options, rowObject) {
        //var param = "'" + rowObject.CodProveedor + "','" + rowObject.TipoDocumento + "','" + rowObject.NroDocumento + "','" + Fn_util_DateToString(rowObject.FechaEmision) + "','" + rowObject.EstadoDocumento + "'";
        //var obj = "<img src='../../Util/images/ico_acc_detalle.gif' alt='" + cellvalue + "' title='Contrato' width='20px' onclick=\"javascript:AgregarContrato(" + param + ");\" style='cursor: pointer;cursor: hand;' />";
        var obj = "<img src='../../Util/images/ico_acc_agregar.gif' alt='" + cellvalue + "' title='Contrato' width='20px' onclick=\"javascript:fn_AgregarContrato();\" style='cursor: pointer;cursor: hand;' />";
        return obj;
    };
}


//****************************************************************
// Funcion		:: 	fn_valFecha
// Descripción	::	Se agrego funcion para darle formato a la fecha
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_valFecha(tipo) {
    if (tipo == '1') {
        //alert('Aqui');
        var strFecha = $('#txtFechaEmision').val();
        if ($("#txtFechaEmision").val() != "") {
            if (strFecha.length == 8) {
                var dia = strFecha.substring(0, 2);
                var mes = strFecha.substring(3, 5);
                var anio = strFecha.substring(6, 8);


                var sfecha = dia + '/' + mes + '/' + '20' + anio;
                $('#txtFechaEmision').val(sfecha);
            }
        }
    }
    if (tipo == '2') {
        var strFecha = $('#txtFechaVenc1').val();
        if ($("#txtFechaVenc1").val() != "") {
            if (strFecha.length == 8) {
                var dia = strFecha.substring(0, 2);
                var mes = strFecha.substring(3, 5);
                var anio = strFecha.substring(6, 8);


                var sfecha = dia + '/' + mes + '/' + '20' + anio;
                $('#txtFechaVenc1').val(sfecha);
            }

        }
    }
    if (tipo == '3') {
        var strFecha = $('#txtFechaCompro').val();
        if ($("#txtFechaCompro").val() != "") {
            if (strFecha.length == 8) {
                var dia = strFecha.substring(0, 2);
                var mes = strFecha.substring(3, 5);
                var anio = strFecha.substring(6, 8);


                var sfecha = dia + '/' + mes + '/' + '20' + anio;
                $('#txtFechaCompro').val(sfecha);
            }

        }
    }
}

//****************************************************************
// Funcion		:: 	fn_CancelarDocumento
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_CancelarDocumento() {
    fn_util_SeteaObligatorio($("#cmbMoneda"), "select");
    fn_util_SeteaObligatorio($("#cmdTipoDoc"), "select");
    fn_util_SeteaObligatorio($("#txtNroDocProveed"), "input");
    $("#hidCodProveedor").val('');
    $("#cmdTipoDoc option").eq(0).attr("selected", "selected");
    $("#txtNroDocProveed").val('');
    $("#txtRazonSocialProveedor").val('');
    $("#txtNumeroTipo").val('');
    $("#lblNumeroTipo").html('');
    $("#hidCodProveedor").val('');
    $("#hidTipoDocumento").val('');
    $("#hidNumeroDocumento").val('');
    $("#hidFecEmision").val('');
    $("#cmbMoneda").val($('#hidCodMoneda').val());
    $('#cmbMoneda').removeAttr('disabled');
    $('#txtGravado').removeClass();
    $('#txtGravado').addClass('css_input');
    $('#txtGravado').removeAttr('disabled');
    $('#txtPorcIGV').removeClass();
    $('#txtPorcIGV').addClass('css_input');
    $('#txtPorcIGV').removeAttr('disabled');
    $("#txtPorcIGV").val(fn_util_ValidaMonto("18", 2));

    $('#txtNumeroIGV').removeClass();
    $('#txtNumeroIGV').addClass('css_input');
    //$('#txtNumeroIGV').attr('disabled', 'disabled');    
    $('#txtNumeroIGV').val(fn_util_ValidaMonto("0", 2));
    $('#txtNumeroIGV').removeAttr('disabled');

    $("#cmbProcedencia option").eq(0).attr("selected", "selected");

    $('#txtFechaEmision').removeAttr('disabled');
    $("#txtFechaEmision").val('');
    $("#txtFechaEmision").datepicker("enable");

    $("#txtGravado").val('');
    $("#txtValorNoGravado").val('');
    $("#txtTotal").val('');

    if (fn_util_trim($("#hidCodMoneda").val()) == strMonedaSoles) {
        $("#txttcdia").val($("#hddtcdiaVenta").val());
    } else {
        $("#txttcdia").val($("#hddtcdiaCompra").val());
    }

    $("#chktcespecial").attr("checked", false);
    $("#txttcespecial").val('');
    $("#chkSunat").attr("checked", false);
    $("#chkDetraccion").attr("checked", false);
    $("#ChkRetencion").attr("checked", false);
    $("#txtTipoBien").val('');
    $("#lblTipoBien").html('');
    $("#hidtxtTipoBien").val('');
    $("#txtMontoSoles").val('');
    $("#txtMontoDolar").val('');
    $("#txtNroConstancia").val('');
    $("#txtFechaCompro").val('');
    $('#dv_cancelar').css("display", "none");
    $('#dv_Modificar').css("display", "none");
    $('#dv_agregar').css("display", "block");
    $('#dv_editar').css("display", "block");
    $('#dv_eliminar').css("display", "block");
    $('#dv_EditarReplicar').css("display", "block");
    $("#chkNinguno").attr("checked", true);
    $('#txtTotal').addClass('css_input_inactivo');

    fn_util_SeteaCalendarioFunction($("#txtFechaEmision"), function() { fn_TipoCambioSunat(strModalidadTC.Sunat); });

    $('#hidAgenteRetencion').val('0');
    $('#trDUA').css("display", "none");
    $('#cmdTipoDoc').val(strTipoDocumentoIdentificacion.Ruc);

    fn_DatosProveedorSunat(false);
    fnHabiltarDetranReten(false);
    $('#imgBuscarProveedor').removeAttr('disabled');
    $('#imgTipoBien').removeAttr('disabled', 'disabled');
    $('#imgNumeroTipo').removeAttr('disabled', 'disabled');
    $('#chkNinguno').removeAttr('disabled', 'disabled');
    $('#chkDetraccion').removeAttr('disabled', 'disabled');
    $('#ChkRetencion').removeAttr('disabled', 'disabled');
    $('#txtNumeroTipo').removeAttr('disabled', 'disabled');

    $('#txtNroDocProveed').removeAttr('disabled');
    $('#txtNroDocProveed').prop('readonly', false);

    $("#txtPorc4ta").val('');
    $("#txtMonto4taSoles").val('');
    $("#txtMonto4taDolares").val('');
    $("#spn_4taLabel").hide();
    $("#spn_4taInput").hide();

}

//****************************************************************
// Funcion		:: 	fn_ModificarDocumento
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_ModificarDocumento() {
    parent.fn_blockUI();
    //String Validación
    var strError = new StringBuilderEx();

    //Instancia Objetos
    var objhidCodProveedor = $('#hidCodProveedor');
    var objtxtNumeroTipo = $('input[id=txtNumeroTipo]:text');
    var objtxtNumeroDoc1 = $('input[id=txtNumeroDoc1]:text');
    var objtxtFechaEmision = $('input[id=txtFechaEmision]:text');
    var objtxtNumeroIGV = $('input[id=txtNumeroIGV]:text');
    var objtxtTotal = $('input[id=txtTotal]:text');
    var objcmbMoneda = $('select[id=cmbMoneda]');

    //Valida
    strError.append(fn_util_ValidateControl(objhidCodProveedor[0], 'un proveedor válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtNumeroTipo[0], 'un tipo de comprobante', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtFechaEmision[0], 'una fecha emsisión', 1, ''));

    if ($('#txtTotal').prop('class').indexOf('css_input_inactivo') == -1) { strError.append(fn_util_ValidateControl(objtxtTotal[0], 'un total', 1, '')); }

    strError.append(fn_util_ValidateControl(objcmbMoneda[0], 'una moneda', 1, ''));

    if ($('#chkNinguno').is(':checked') == false) {
        var objtxtCodTipoBien = $('#txtTipoBien');
        strError.append(fn_util_ValidateControl(objtxtCodTipoBien[0], 'un tipo válido', 1, ''));
    }

    if (fn_util_ValidaMonto($("#txtTotal").val(), 2) == fn_util_ValidaMonto(0, 2)) {
        strError.append("El total debe ser mayor a cero");
    }
    var dblhidigv = fn_util_ValidaDecimal($('#hidNontoIGV').val());
    var dbligv = fn_util_ValidaDecimal($('#txtNumeroIGV').val());


    if (dblhidigv != dbligv) {
        var dif = 0;

        if (dbligv > dblhidigv) {
            dif = dbligv - dblhidigv;
        }
        else {
            dif = dblhidigv - dbligv;
        }

        if (fn_util_ValidaMonto(dif, 2) > 0.5) {
            strError.append("IGV Ingresado fuera del rango de tolerancia");
        }
    }

    fn_ValidaDuplicidad(strError);

    //Valida si hay Error
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }
    else {
        //Editar Documento
        parent.fn_unBlockUI();
    }
}

//****************************************************************
// Funcion		:: 	fn_EliminarDocumento
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_EliminarDocumento() {
    if (($("#hidCodProveedor").val() == '') && ($("#hidTipoDocumento").val() == '') &&
        ($("#hidNumeroDocumento").val() == '') && ($("#hidFecEmision").val() == '')) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro en la grilla.", "util/images/warning.gif", "ERROR AL ANULAR");
    }
    //    else if ($("#hidCodigoEstadoDoc").val() == strCodigoEstadoDocumento.Anulado) { //strEstadoAnulado
    //        parent.fn_mdl_mensajeIco("No puede anular el documento ya se encuentra anulado.", "util/images/warning.gif", "ERROR AL ANULAR");
    //    } else if ($("#hidCodigoEstadoDoc").val() == strCodigoEstadoDocumento.Desembolsado) {  //strEstadoDesembolsado
    //        parent.fn_mdl_mensajeIco("No puede anular el documento se encuentra desembolsar.", "util/images/warning.gif", "ERROR AL ANULAR");
    //    }
    else {
        fn_mdl_confirma("Est&aacute; seguro de anular documento?"
    		      , function() {
    		          fn_EliminarLogica();
    		      },
		            '../../util/images/question.gif',
                  function() { },
                  'Eliminar Documento'
	    );
    }
}

//****************************************************************
// Funcion		:: 	fn_EditarDocumento
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_EditarDocumento() {

    if (($("#hidCodProveedor").val() == '') && ($("#hidTipoDocumento").val() == '') &&
        ($("#hidNumeroDocumento").val() == '') && ($("#hidFecEmision").val() == '')) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro en la grilla.", "util/images/warning.gif", "ERROR AL EDITAR");
    }

    //    else if ($("#hidCodigoEstadoDoc").val() == strCodigoEstadoDocumento.Anulado) {
    //        parent.fn_mdl_mensajeIco("No puede editar el documento se encuentra anulado.", "util/images/warning.gif", "ERROR AL EDITAR");
    //    }

    //    else if (fn_util_trim($("#hidEstadoID").val()) == strCodigoEstadoID.PenEjecucion ||
    //				fn_util_trim($("#hidEstadoID").val()) == strCodigoEstadoID.Wio ||
    //				fn_util_trim($("#hidEstadoID").val()) == strCodigoEstadoID.Anulada ||
    //				fn_util_trim($("#hidEstadoID").val()) == strCodigoEstadoID.Aprobada) {
    //        parent.fn_mdl_mensajeIco("No puede editar el doucumento debido al estado de su Instrucción de Desembolso asociada.", "util/images/warning.gif", "ERROR AL EDITAR");
    //    }

    else {
        //Obtener Documento
    }
}


//****************************************************************
// Funcion		:: 	fn_AgregarDocumento
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_AgregarDocumento() {
    parent.fn_blockUI();
    $("#hidCodigoEstadoDoc").val('');

    //String Validación
    var strError = new StringBuilderEx();
    //Instancia Objetos
    var objhidCodProveedor = $('#hidCodProveedor');
    var objtxtFechaEmision = $('input[id=txtFechaEmision]:text');
    var objtxtPorcIGV = $('input[id=txtPorcIGV]:text');
    var objtxtNumeroIGV = $('input[id=txtNumeroIGV]:text');
    var objtxtTotal = $('input[id=txtTotal]:text');
    var objcmbMoneda = $('select[id=cmbMoneda]');
    var objtxtNroDocProveed = $('input[id=txtNroDocProveed]:text');
    var objcmdTipoDoc = $('select[id=cmdTipoDoc]');

    if ($('#chkNinguno').is(':checked') == false) {
        var objtxtCodTipoBien = $('#txtTipoBien');
        strError.append(fn_util_ValidateControl(objtxtCodTipoBien[0], 'un tipo válido', 1, ''));
    }

    //Valida
    strError.append(fn_util_ValidateControl(objcmdTipoDoc[0], 'un Tipo Documento válido', 1, ''));
    strError.append(fn_util_ValidateControl(objhidCodProveedor[0], 'un proveedor válido', 1, ''));
    strError.append(fn_util_ValidateControl(objcmbMoneda[0], 'una moneda', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtFechaEmision[0], 'una fecha emisión', 1, ''));

    //fn_util_SeteaCalendario($('input[id*=txtFechaEmision]'));
    fn_util_SeteaCalendarioFunction($("#txtFechaEmision"), function() { fn_TipoCambioSunat(strModalidadTC.Sunat); });

    strError.append(fn_util_ValidateControl(objtxtNroDocProveed[0], 'Número de Documento', 1, ''));



    if ($('#txtGravado').prop('class').indexOf('css_input_inactivo') == -1) {
        ////strError.append(fn_util_ValidateControl(objtxtGravado[0], 'valor gravado', 1, '')); 
    }

    if ($('#txtPorcIGV').prop('class').indexOf('css_input_inactivo') == -1) {
        //strError.append(fn_util_ValidateControl(objtxtPorcIGV[0], 'porcentaje IGV', 1, ''));
    }

    //if ($('#txtNumeroIGV').prop('class').indexOf('css_input_inactivo') == -1) { strError.append(fn_util_ValidateControl(objtxtNumeroIGV[0], 'Importe IGV', 1, '')); }
    if ($('#txtTotal').prop('class').indexOf('css_input_inactivo') == -1) { strError.append(fn_util_ValidateControl(objtxtTotal[0], 'un total', 1, '')); }

    if ($('#cmbMoneda').val() != $('#hidCodMoneda').val()) {
        if (fn_util_ValidaMonto($('#txttcdia').val(), 6) == '0.000000') {
            strError.append("No se encuentra registrado un tipo de cambio del día");
        }
    }

    if (fn_util_ValidaMonto($('#txtTipoCambioSunat').val(), 6) == '0.000000') {
        strError.append("No se encuentra registrado un Tipo de cambio sunat");
    }

    if (fn_util_ValidaMonto($("#txtTotal").val(), 2) == fn_util_ValidaMonto(0, 2)) {

        strError.append("El total debe ser mayor a cero");
    }

    var dblhidigv = fn_util_ValidaDecimal($('#hidNontoIGV').val());
    var dbligv = fn_util_ValidaDecimal($('#txtNumeroIGV').val());

    if (dblhidigv != dbligv) {
        var dif = 0;

        if (dbligv > dblhidigv) {
            dif = dbligv - dblhidigv;
        }
        else {
            dif = dblhidigv - dbligv;
        }

        if (fn_util_ValidaMonto(dif, 2) > 0.5) {
            strError.append("IGV Ingresado fuera del rango de tolerancia");
        }
    }

    fn_ValidaDuplicidad(strError);

    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    } else {
        parent.fn_unBlockUI();
        //Insertar Documento
    }
}

//****************************************************************
// Funcion		:: 	fn_ReplicarDocumento
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_ReplicarDocumento() {
    if (($("#hidCodProveedor").val() == '') && ($("#hidTipoDocumento").val() == '') && ($("#hidNumeroDocumento").val() == '') && ($("#hidFecEmision").val() == '')) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro en la grilla.", "util/images/warning.gif", "ERROR AL REPLICAR");
    } else {
        //Replicar Documento
    }
}

//****************************************************************
// Funcion		:: 	fn_EditarContrato
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_EditarContrato(pCodigoContrato) {
    var sTitulo = "Otros Conceptos";
    var sSubTitulo = "Gastos:: Editar Contrato";
    parent.fn_util_AbreModal(sSubTitulo, "GestionBien/OtrosConceptos/frmGastoContratoRegistro.aspx?Titulo=" + sTitulo + "&SubTitulo=" + sSubTitulo + "&hddCodConDoc=" + pCodigoContrato, 650, 480, function() { });
}

//****************************************************************
// Funcion		:: 	fn_EliminarContrato
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_EliminarContrato(pCodigoContrato) {
    fn_mdl_confirma("Est&aacute; seguro de eliminar contrato?"
    		      , function() {

    		      },
		            '../../util/images/question.gif',
                  function() { },
                  'Eliminar Contrato'
	    );
}

//****************************************************************
// Funcion		:: 	fn_AgregarContrato
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_AgregarContrato() {
    var sTitulo = "Otros Conceptos";
    var sSubTitulo = "Gastos:: Agregar Contrato";
    parent.fn_util_AbreModal(sSubTitulo, "GestionBien/OtrosConceptos/frmGastoContratoRegistro.aspx?Titulo=" + sTitulo + "&SubTitulo=" + sSubTitulo + "&hddCodConDoc=0", 650, 480, function() { });

}

//****************************************************************
// Funcion		:: 	fn_EliminarLogica
// Descripción	::
// Log			::  WCR - 20/11/2012
//****************************************************************
function fn_EliminarLogica() {
    //Eliminar Documento
}

//****************************************************************
// Funcion		:: 	fn_ValidaDuplicidad
// Descripción	::
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_ValidaDuplicidad(pError) {
    //Validar Documento Existente
}

//****************************************************************
// Funcion		:: 	fn_cancelar
// Descripción	::	cancelar
// Log			:: 	WCR - 21/11/2012
//****************************************************************
function fn_cancelar() {
    parent.fn_mdl_confirma('¿Está seguro de Volver?',
		function() {
		    parent.fn_blockUI();
		    fn_util_redirect('GestionBien/OtrosConceptos/frmGastoListado.aspx');
		},
         "Util/images/question.gif",

		function() {
		},
		'Otros Conceptos :: Gastos'
	);
}


