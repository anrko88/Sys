
var strTipoDocumentoIdentificacion = new Object();
strTipoDocumentoIdentificacion.Dni = "1";
strTipoDocumentoIdentificacion.Ruc = "2";
strTipoDocumentoIdentificacion.CarnetExt = "3";
strTipoDocumentoIdentificacion.Pasaporte = "5";
strTipoDocumentoIdentificacion.Otros = "6";


var strModalidadTC = new Object();
strModalidadTC.Sunat = 'SBS';
strModalidadTC.Dia = 'PRF';

var strComprobante = new Object();
strComprobante.Factura = '01';
strComprobante.CodigoDua = '04';
strComprobante.CodigoNoDomiciliado = '15';
strComprobante.CodigoReciboHonorarioNoDomiciliado = '30';
strComprobante.CodigoNotaDebito = '08';
strComprobante.CodigoNotaCredito = '09';
strComprobante.CodigoNotaDebidoNoDomiciliado = '98';
strComprobante.CodigoNotaCreditoNoDomiciliado = '97';
strComprobante.CodigoNotaCreditoEspecial = '87';
strComprobante.CodigoNotaDebidoEspecial = '88';
strComprobante.ReciboHonorario = '11';

var strCodigoEstadoDocumento = new Object();
strCodigoEstadoDocumento.Formalizado = '2';
strCodigoEstadoDocumento.Desembolsado = '3';
strCodigoEstadoDocumento.Anulado = '4';

var strCodigoEstadoContrato = new Object();
strCodigoEstadoContrato.Formalizado = '07';
strCodigoEstadoContrato.EnProceso = '09';

var strCodigoEstadoID = new Object();
strCodigoEstadoID.EnElaboracion = '001';
strCodigoEstadoID.Wio = '002';
strCodigoEstadoID.PenEjecucion = '003';
strCodigoEstadoID.Devuelta = '004';
strCodigoEstadoID.Anulada = '005';
strCodigoEstadoID.Aprobada = '006';

var strIdTabla = new Object();
strIdTabla.Detraccion = 'TBL180';
strIdTabla.TipoDocumento = 'TBL107';
strIdTabla.ConceptoRetencion = 'TBL185';
strIdTabla.Aduanas = 'TBL184';


var strSubTipoContrato = new Object();
strSubTipoContrato.Total = '001';
strSubTipoContrato.Parcial = '002';


var strRucSunat = '20131312955';   // RUC SUNAT
var strMonedaSoles = '001';
var strMonedaDolares = '002';
var intIgv = '18';                 //VALOR IGV
var int4ta = '10';                 //VALOR 4TA
var intDocAnulados = 0;
var intDocBloqueados = 0;

var strCodigoRetencion = '001';   //Retención 6%
//IBK JJM
var strRetencionProveedor = '0'; //Indica si el Proveedor tiene retención
//IBK JJM
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    //Setea Calendario
    //fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    //Inicio AAE
    //---------------------------------
    //Valida Tabs
    //---------------------------------
    $("div#divTabs").tabs({
        show: function(event, ui) {
            fn_doResize();
        }
    });
    //Cargo la info del tab2 siempre nuevamente
    $('div#divTabs').bind('tabsselect', function(event, ui) {
        fn_cargarAsociaciones(event, ui);
    })

    fn_cargaGrillaBienes();
    fn_cargaGrillaDocumentos();
    fn_cargaGrillaRelaciones();
    $("#hidLazyLoadTab").val("1");
    $("#hidIdBien").val("0");
    //Fin AAE

    //Inicializar
    fn_inicializaCampos();

    // Tipo Comprobante 1
    $('#imgNumeroTipo').click(function() {
        AbrirListaGenerico(strIdTabla.TipoDocumento, 'Tipo de Comprobante', 'NumeroTipo');
        //fn_activaRetencionAutomatica2();
    });

    // Tipo Comprobante 2
    $('#imgNumeroTipo2').click(function() {
        AbrirListaGenerico(strIdTabla.TipoDocumento, 'Tipo de Comprobante', 'NumeroTipo2');
        //fn_activaRetencionAutomatica2();
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

    // Aduana
    $('#imgAduana').click(function() {
        AbrirListaGenerico(strIdTabla.Aduanas, 'Aduana', 'Aduana');
    });

    //Carga Grilla
    fn_cargaGrilla();

    //Inicializa Si Tiene REtencion
    $('#hidAgenteRetencion').val('1');

    //************************************************************
    // Función		:: 	Evento Change de Opcion detraccion
    // Descripcion 	:: 	
    // Log			:: 	
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
    // Log			:: 	
    //************************************************************
    $('#ChkRetencion').change(function() {
        if ($(this).is(':checked') == true) {
            fn_validaCheckRetencion();
        }
        //fn_activaRetencionAutomatica2();
    });


    //************************************************************
    // Función		:: 	Evento Change de Opcion Ninguno
    // Descripcion 	:: 	
    // Log			:: 	
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
        //fn_activaRetencionAutomatica2();
    });


    //************************************************************
    // Función		:: 	IGV
    // Descripcion 	:: 	
    // Log			:: 	
    //************************************************************
    $("#txtPorcIGV").focusout(function() {

        if ($("#txtPorcIGV").val() > fn_util_ValidaDecimal(intIgv)) {
            $("#txtPorcIGV").val(fn_util_ValidaMonto(intIgv, 2));
            parent.fn_util_MuestraLogPage('El IGV no puede ser mayor a' + intIgv + "% ", "I");
        }

        fn_MontoIGV();
        fn_ValorTotal();
        fn_ValorDetraccion();
        fn_ValorRetencion();

        //IBK - JJM Inicio
        //fn_activaRetencionAutomatica();
        //fn_activaRetencionAutomatica2();
        //IBK JJM Fin
    });

    //************************************************************
    // Función		:: Numero IGV
    // Descripcion 	:: 	
    // Log			:: 	
    //************************************************************
    $("#txtNumeroIGV").focusout(function() {

        //if ($('#txtNumeroTipo').val() == strComprobante.CodigoDua) {
        fn_ValorTotal();
        fn_ValorDetraccion();
        fn_ValorRetencion();
        //fn_ValorPorcIgv();

        //IBK - JJM Inicio
        //fn_activaRetencionAutomatica();
        //fn_activaRetencionAutomatica2();
        //IBK JJM Fin
        //}
    });


    //************************************************************
    // Función		:: Numero IGV
    // Descripcion 	:: 	
    // Log			:: 	
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
        //fn_activaRetencionAutomatica();
        //IBK - JJM Inicio
        //fn_activaRetencionAutomatica();
        //fn_activaRetencionAutomatica2();
        //IBK JJM Fin
    });


    //************************************************************
    // Función		:: Valor No Grabado
    // Descripcion 	:: 	
    // Log			:: 	
    //************************************************************
    $("#txtValorNoGravado").focusout(function() {

        if ($('#txtNumeroTipo').val() == strComprobante.ReciboHonorario) {
            fn_calculaRenta4ta();
        }

        if (fn_util_ValidaMonto($("#txtNumeroIGV").val()) == fn_util_ValidaMonto(0, 2)) {
            //alert("ole")
            $('#chkNinguno').attr('checked', true);
            fnHabiltarDetranReten(false);
        }

        fn_ValorTotal();
        fn_ValorDetraccion();
        fn_ValorRetencion();
        //fn_activaRetencionAutomatica2();
    });


    //************************************************************
    // Función		:: Valor Grabado
    // Descripcion 	:: 	
    // Log			:: 	
    //************************************************************
    $("#txtGravado").focusout(function() {
        fn_MontoIGV();
        fn_ValorTotal();
        fn_ValorDetraccion();
        fn_ValorRetencion();

        //fn_activaRetencionAutomatica();       
        //fn_activaRetencionAutomatica2();
    });

    //************************************************************
    // Función		:: Porc 4ta
    // Descripcion 	:: 	
    // Log			:: 	
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
    fn_util_SeteaCalendario($('input[id*=txtFechaVenc1]'));



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

    //Inicio IBK - AAE - Activación de Leasing Parcial
    if ($('#hidCodSubContrato').val() == strSubTipoContrato.Total) {
        $('#ChkActivacionLeasing').attr('disabled', 'disabled');
        $('input[name=ChkActivacionLeasing]').attr('checked', true);
    }
    //Fin IBK 


    //Valida Bloqueo
    fn_ValidaBloqueo();

    //On load Page (siempre al final)
    fn_onLoadPage();

});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa Campos
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_inicializaCampos() {
    fn_util_SeteaObligatorio($("#cmbMoneda"), "select");
    fn_util_SeteaObligatorio($("#cmbProcedencia"), "select");
    fn_util_SeteaObligatorio($("#cmdTipoDoc"), "select");
    fn_util_SeteaObligatorio($("#txtNroDocProveed"), "input");

    //Valida Tipo de Datos
    $('#txtNumeroTipo').validText({ type: 'number', length: 3 });
    $('#txtNumeroTipo2').validText({ type: 'number', length: 3 });
    $('#txtAduana').validText({ type: 'number', length: 3 });
    $('#txtTipoBien').validText({ type: 'number', length: 3 });
    $("#txtPorcIGV").validNumber({ value: '', decimals: 2, length: 5 });
    $('#txtSerieDoc1').validText({ type: 'alphanumeric', length: 4 });
    $('#txtNumeroDoc1').validText({ type: 'number', length: 16 });
    $('#txtAnnoAduana').validText({ type: 'number', length: 4 });
    $('#txtNumeroComprobante').validText({ type: 'number', length: 20 });
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
    $("#txtSerieDoc2").attr('disabled', 'disabled');
    $("#txtNroDoc2").attr('disabled', 'disabled');
    $('#imgNroDoc2').attr('disabled', 'disabled');
    $("#txtFechaAdm").attr('disabled', 'disabled');
    $('#txtTipoBien').attr('disabled', 'disabled');
    $('#imgTipoBien').attr('disabled', 'disabled');
    $('#txtNroConstancia').attr('disabled', 'disabled');
    $('#txtFechaCompro').attr('disabled', 'disabled');
    $('#txtMontoSoles').attr('disabled', 'disabled');
    $('#txtMontoDolar').attr('disabled', 'disabled');
    $('#txtFechaAdm').removeClass();
    $('#txtFechaAdm').addClass('css_input_inactivo');
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
    $('#lblFecVenc').css("display", "none");
    //$('#txtFechaVenc1').css("display", "none");
    $("#spa_FecPago").hide();
    $("#chkNinguno").attr("checked", true);


    $("#dv_GenerarWIO").hide();
    if ($("#hidCodEstadoContrato").val() == 07 || $("#hidCodEstadoContrato").val() == 09) {
        $("#dv_RecalculaIGV").show();
    } else {
        $("#dv_RecalculaIGV").hide();
    }

    $("#dv_contenedor").addClass("css_scrollPane");
    $('#cmdTipoDoc').val(strTipoDocumentoIdentificacion.Ruc);
    $('#txtNroDocProveed').validText({ type: 'number', length: 11 });

    $("#txtPorcIGV").val(fn_util_ValidaMonto(intIgv, 2));
    fn_TipoCambioDia(strModalidadTC.Sunat);

    $("#spn_4taLabel").hide();
    $("#spn_4taInput").hide();
    $("#txtPorc4ta").validNumber({ value: '', decimals: 2, length: 5 });
    $("#txtPorc4ta").val(fn_util_ValidaMonto(int4ta, 2));


    //Valida estado Contrato
    if (fn_util_trim($("#hidCodEstadoContrato").val()) == strCodigoEstadoContrato.Formalizado || fn_util_trim($("#hidCodEstadoContrato").val()) == strCodigoEstadoContrato.EnProceso) {
        $('#ChkActivacionLeasing').removeAttr('disabled');
        $('#txtFechaAct').removeAttr('disabled');
        $('#txtFechaAct').attr('class', 'css_input_inactivo');
    } else {
        $('#txtFechaAct').attr('class', 'css_input_inactivo');
        $('#ChkActivacionLeasing').attr('disabled', 'disabled');
        $('#txtFechaAct').attr('disabled', 'disabled');
    }

    //Valida Check
    if ($('#ChkActivacionLeasing').is(':checked') == true) {
        $('#ChkActivacionLeasing').attr('disabled', 'disabled');
    }
    //Inicio IBK - AAE
    if ($('#ChkActivacionLeasing').is(':checked') == false) {
        var pSubContrato = $("#hidCodSubContrato").val();
        if (pSubContrato == strSubTipoContrato.Total) { //TOTAL
            $('#ChkActivacionLeasing').attr('checked', true);
        }
    }
    //Fin IBK

}

//****************************************************************
// Funcion		:: 	fn_EliminarDetalle
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_EliminarDetalle() {
    if (($("#hidCodProveedor").val() == '') && ($("#hidTipoDocumento").val() == '') &&
        ($("#hidNumeroDocumento").val() == '') && ($("#hidFecEmision").val() == '')) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro en la grilla.", "util/images/warning.gif", "ERROR AL ANULAR");
    } else if ($("#hidCodigoEstadoDoc").val() == strCodigoEstadoDocumento.Anulado) { //strEstadoAnulado
        parent.fn_mdl_mensajeIco("No puede anular el documento ya se encuentra anulado.", "util/images/warning.gif", "ERROR AL ANULAR");
    } else if ($("#hidCodigoEstadoDoc").val() == strCodigoEstadoDocumento.Desembolsado) {  //strEstadoDesembolsado
        parent.fn_mdl_mensajeIco("No puede anular el documento se encuentra desembolsar.", "util/images/warning.gif", "ERROR AL ANULAR");
    }
    else {
        fn_mdl_confirma("Est&aacute; seguro de dar de baja el Desembolso seleccionado?"
    		      , function() {
    		          fn_EliminarLogica();
    		      },
		            '../util/images/question.gif',
                  function() { },
                  'Eliminar Desembolso'
	    );
    }
}

//****************************************************************
// Funcion		:: 	fn_replicar
// Descripción	::	Obtiene datos de la grilla para editarlos 
// Log			:: 	KCC - 05/06/2012
//****************************************************************
function fn_replicar() {
    if (($("#hidCodProveedor").val() == '') && ($("#hidTipoDocumento").val() == '') && ($("#hidNumeroDocumento").val() == '') && ($("#hidFecEmision").val() == '')) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro en la grilla.", "util/images/warning.gif", "ERROR AL REPLICAR");
    } else {
        var arrParametros = ["pstrCodContrato", $("#txtNumeroContrato").val(),
                             "pstrCodProveedor", $("#hidCodProveedor").val(),
                             "pstrNumeroTipo", $("#hidTipoDocumento").val(),
                             "pstrNumeroDoc1", $("#hidNumeroDocumento").val(),
                             "pstrFecEmision", Fn_util_DateToString($("#hidFecEmision").val())
                             ];

        fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/ObtenerDocumentoEspecifico",
                            arrParametros,
                            function(resultado) {
                                $("#hddActionBotonGrid").val('R');
                                fn_DatosObtenidos(resultado);
                                $("#txtSerieDoc1").val('');
                                $("#txtNumeroDoc1").val('');
                                $("#dv_agregar").show();
                                $("#dv_Modificar").hide();
                                $("#dv_EditarReplicar").hide();

                            },
                        function(resultado) {
                            parent.fn_unBlockUI();
                            var error = eval("(" + resultado.responseText + ")");
                            parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR Al CARGAR REPLICAR");
                        }
            );
    }
}

//****************************************************************
// Funcion		:: 	fn_EditarDetalle
// Descripción	::	Obtiene datos de la grilla para editarlos 
// Log			:: 	KCC - 05/06/2012
//****************************************************************
function fn_EditarDetalle() {

    if (($("#hidCodProveedor").val() == '') && ($("#hidTipoDocumento").val() == '') &&
        ($("#hidNumeroDocumento").val() == '') && ($("#hidFecEmision").val() == '')) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro en la grilla.", "util/images/warning.gif", "ERROR AL EDITAR");
    }

    else if ($("#hidCodigoEstadoDoc").val() == strCodigoEstadoDocumento.Anulado) {
        parent.fn_mdl_mensajeIco("No puede editar el documento se encuentra anulado.", "util/images/warning.gif", "ERROR AL EDITAR");
    }

    else if (fn_util_trim($("#hidEstadoID").val()) == strCodigoEstadoID.PenEjecucion ||
				fn_util_trim($("#hidEstadoID").val()) == strCodigoEstadoID.Wio ||
				fn_util_trim($("#hidEstadoID").val()) == strCodigoEstadoID.Anulada ||
				fn_util_trim($("#hidEstadoID").val()) == strCodigoEstadoID.Aprobada) {
        parent.fn_mdl_mensajeIco("No puede editar el doucumento debido al estado de su Instrucción de Desembolso asociada.", "util/images/warning.gif", "ERROR AL EDITAR");
    }

    else {
        var arrParametros = ["pstrCodContrato", $("#txtNumeroContrato").val(),
                                 "pstrCodProveedor", $("#hidCodProveedor").val(),
                                 "pstrNumeroTipo", $("#hidTipoDocumento").val(),
                                 "pstrNumeroDoc1", fn_util_trim($("#hidNumeroDocumento").val()),
                                 "pstrFecEmision", Fn_util_DateToString($("#hidFecEmision").val())
                                 ];
        fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/ObtenerDocumentoEspecifico",
            arrParametros,
                        function(resultado) {
                            $("#hddActionBotonGrid").val('E');
                            fn_DatosObtenidos(resultado);

                        },
                        function(resultado) {
                            parent.fn_unBlockUI();
                            var error = eval("(" + resultado.responseText + ")");
                            parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR Al CARGAR EDITAR");
                        }
            );
    }
}

/* Iniioc IBK - AAE - 08/02/2013 - Se agregar validación de tipo doc e igv*/
//****************************************************************
// Funcion		:: 	fn_ModificarDetalle2
// Descripción	::	Obtiene datos de la grilla para editarlos 
// Log			:: 	KCC - 05/06/2012
//****************************************************************
function fn_ModificarDetalle() {
    var tipoDoc = $('input[id=txtNumeroTipo]:text');
    var nbr_igv = fn_util_ValidaMonto($("#txtNumeroIGV").val(), 2);
    if ((tipoDoc[0].value != "01") && (tipoDoc[0].value != "04") && (tipoDoc[0].value != "08") && (tipoDoc[0].value != "09")) {
        if (nbr_igv > 0) {
            fn_mdl_confirma("El tipo de documento seleccionado no lleva IGV. Desea agregarlo con IGV?"
    		      , function() {
    		          fn_ModificarDetalle2();
    		      },
		            '../util/images/question.gif',
                  function() { },
                  'Agregar detalle.'
	    );
        } else {
            fn_ModificarDetalle2();
        }
    } else {
        fn_ModificarDetalle2();
    }

}

//****************************************************************
// Funcion		:: 	fn_AgregarDetalle
// Descripción	::	Agrega datos a la grilla
// Log			:: 	AAE - 08/02/2013
//****************************************************************
function fn_AgregarDetalle() {
    var tipoDoc = $('input[id=txtNumeroTipo]:text');
    var nbr_igv = fn_util_ValidaMonto($("#txtNumeroIGV").val(), 2);
    if ((tipoDoc[0].value != "01") && (tipoDoc[0].value != "04") && (tipoDoc[0].value != "08") && (tipoDoc[0].value != "09")) {
        if (nbr_igv > 0) {
            fn_mdl_confirma("El tipo de documento seleccionado no lleva IGV. Desea agregarlo con IGV?"
    		      , function() {
    		          fn_AgregarDetalle2();
    		      },
		            '../util/images/question.gif',
                  function() { },
                  'Agregar detalle.'
	    );
        } else {
            fn_AgregarDetalle2();
        }
    } else {
        fn_AgregarDetalle2();
    }
}

//****************************************************************
// Funcion		:: 	fn_AgregarDetalle
// Descripción	::	Agrega datos a la grilla
// Log			:: 	KCC - 04/06/2012
//****************************************************************
function fn_AgregarDetalle2() {
    parent.fn_blockUI();
    $("#hidCodigoEstadoDoc").val('');

    //String Validación
    var strError = new StringBuilderEx();
    //Instancia Objetos
    var objhidCodProveedor = $('#hidCodProveedor');
    var objtxtNumeroTipo = $('input[id=txtNumeroTipo]:text');
    var objtxtNumeroDoc1 = $('input[id=txtNumeroDoc1]:text');
    var objtxtFechaEmision = $('input[id=txtFechaEmision]:text');
    var objtxtPorcIGV = $('input[id=txtPorcIGV]:text');
    var objtxtNumeroIGV = $('input[id=txtNumeroIGV]:text');
    var objtxtTotal = $('input[id=txtTotal]:text');
    var objcmbMoneda = $('select[id=cmbMoneda]');
    var objtxtNroDocProveed = $('input[id=txtNroDocProveed]:text');
    var objcmdTipoDoc = $('select[id=cmdTipoDoc]');
    var objtxtFechaVenc1 = $('input[id=txtFechaVenc1]:text');

    if (objtxtNumeroTipo[0].value == strComprobante.CodigoDua) {

        var objtxtAduana = $('input[id=txtAduana]:text');
        var objtxtAnnoAduana = $('input[id=txtAnnoAduana]:text');
        var objtxtNumeroComprobante = $('input[id=txtNumeroComprobante]:text');
        strError.append(fn_util_ValidateControl(objtxtAduana[0], 'una oficina de aduana válida', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtAnnoAduana[0], 'un año', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtNumeroComprobante[0], 'un número de comprobante', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtFechaVenc1[0], 'Fecha de Pago', 1, ''));
        fn_util_SeteaCalendario($('input[id*=txtFechaVenc1]'));

        var dtmFechaVence = $("#txtFechaVenc1").val();
        var dtmFechaEmision = $("#txtFechaEmision").val();

        if (fn_util_ComparaFecha(dtmFechaVence, dtmFechaEmision)) {
            strError.append("La fecha de pago debe ser mayor a la fecha de Emisión");
        }
    } else {
        strError.append(fn_util_ValidateControl(objtxtNumeroDoc1[0], 'un número de Comprobante', 1, ''));
    }

    if ($('#chkNinguno').is(':checked') == false) {
        var objtxtCodTipoBien = $('#txtTipoBien');
        strError.append(fn_util_ValidateControl(objtxtCodTipoBien[0], 'un tipo válido', 1, ''));
    }

    //Valida
    strError.append(fn_util_ValidateControl(objcmdTipoDoc[0], 'un Tipo Documento válido', 1, ''));
    strError.append(fn_util_ValidateControl(objhidCodProveedor[0], 'un proveedor válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtNumeroTipo[0], 'un tipo de comprobante', 1, ''));
    strError.append(fn_util_ValidateControl(objcmbMoneda[0], 'una moneda', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtFechaEmision[0], 'una fecha emisión', 1, ''));

    //fn_util_SeteaCalendario($('input[id*=txtFechaEmision]'));
    fn_util_SeteaCalendarioFunction($("#txtFechaEmision"), function() { fn_TipoCambioSunat(strModalidadTC.Sunat); });

    strError.append(fn_util_ValidateControl(objtxtNroDocProveed[0], 'Número de Documento', 1, ''));


    // si es diferente a la dua valida importe gravado
    if (objtxtNumeroTipo[0].value != strComprobante.CodigoDua) {
        if ($('#txtGravado').prop('class').indexOf('css_input_inactivo') == -1) {
            ////strError.append(fn_util_ValidateControl(objtxtGravado[0], 'valor gravado', 1, '')); 
        }
    }
    if ($('#txtNumeroTipo').val() != strComprobante.CodigoNoDomiciliado) {
        if ($('#txtPorcIGV').prop('class').indexOf('css_input_inactivo') == -1) {
            //strError.append(fn_util_ValidateControl(objtxtPorcIGV[0], 'porcentaje IGV', 1, ''));
        }
    } else {
        strError.append(fn_util_ValidateControl(objtxtFechaVenc1[0], 'Fecha Pago', 1, ''));
        fn_util_SeteaCalendario($('input[id*=txtFechaVenc1]'));
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

    if ($("#hddSubtipoContrato").val() == strSubTipoContrato.Total) {
        if ($("#hddFlagRegDesembolso").val() == "1") {
            //strError.append("No se Puede Agregar por que ya se realizo un desembolso");
        }
    }

    if (fn_util_ValidaMonto($("#txtTotal").val(), 2) == fn_util_ValidaMonto(0, 2)) {

        strError.append("El total debe ser mayor a cero");
    }
    //Inicio IBK
    //RPH Valido los montos de IGV
    var dblhidigv = fn_util_ValidaDecimal($('#hidNontoIGV').val());
    var dbligv = fn_util_ValidaDecimal($('#txtNumeroIGV').val());
    if (objtxtNumeroTipo[0].value != strComprobante.CodigoDua) {
        if (dblhidigv != dbligv) {
            var dif = 0;

            if (dbligv > dblhidigv) {
                dif = dbligv - dblhidigv;
            }
            else {
                dif = dblhidigv - dbligv;
            }

            //Inicio IBK - AAE - modifico validación
            //if (fn_util_ValidaMonto(dif, 2) > 0.5) {
            if (dif > 0.5) {
                //Fin IBK
                strError.append("IGV Ingresado fuera del rango de tolerancia");
            }
        }
    }
    //Fin IBK
    fn_ValidaDuplicidad(strError);

    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    } else {

        var strNumComNDNC = '';
        if ($("#txtNroDoc2").val() != '') {
            var strNumSerNDNC = $("#txtSerieDoc2").val();
            strNumComNDNC = $("#txtNroDoc2").val();

            if (strNumSerNDNC == null) { strNumSerNDNC = ''; }

            if ($('#hidTipoComNDNC').val() != strComprobante.CodigoDua) {
                var strPad = '0000';
                strNumComNDNC = strPad.substring(0, strPad.length - strNumSerNDNC.length) + strNumSerNDNC + strNumComNDNC;
            }
        }

        var arrParametros = ["pstrCodContrato", $("#txtNumeroContrato").val(),
                             "pstrCodProveedor", $("#hidCodProveedor").val(),
                             "pstrNumeroTipo", $("#txtNumeroTipo").val(),
                             "pstrSerieDoc1", $("#txtSerieDoc1").val(),
                             "pstrNumeroDoc1", $("#txtNumeroDoc1").val(),
                             "pstrFecVenc", Fn_util_DateToString($("#txtFechaVenc1").val()),
                             "pstrAduana", $("#txtAduana").val(),
                             "pstrAnioAduana", $("#txtAnnoAduana").val(),
                             "pstrNumAduana", $("#txtNumeroComprobante").val(),
                             "pstrMoneda", $("#cmbMoneda").val(),
                             "pstrProcedencia", $("#cmbProcedencia").val(),
                             "pstrFecEmsion", Fn_util_DateToString($("#txtFechaEmision").val()),
                             "pstrGravado", $("#txtGravado").val(),
                             "pstrPorcIGV", $("#txtPorcIGV").val(),
                             "pstrIgv", $("#txtNumeroIGV").val(),
                             "pstrNoGravado", $("#txtValorNoGravado").val(),
                             "pstrTotal", $("#txtTotal").val(),
                             "pstrchkAdelantoProveedor", $("#chkAdelantoProveedor").is(':checked') ? '1' : '0',
                             "pstrAdelantoProveedor", $("#txtAdelantoProveedor").val(),
                             "pstrPorDesembolsar", $("#txtDesembolsar").val(),
                             "pstrTipoCambioDia", $("#txttcdia").val(),
                             "pstrchkTipoCambioEspecial", $("#chktcespecial").is(':checked') ? '1' : '0',
                             "pstrTipoCambioEspecial", $("#txttcespecial").val(),
                             "pstrchkTipoCambioSunat", $("#chkSunat").is(':checked') ? '1' : '0',
                             "pstrTipoCambioSunat", $("#txtTipoCambioSunat").val(),
                             "pstrchkDetraccion", $("#chkDetraccion").is(':checked') ? '1' : '0',
                             "pstrchkRetencion", $("#ChkRetencion").is(':checked') ? '1' : '0',
                             "pstrCodigoTipoBien", $("#txtTipoBien").val(),
                             "pstrPorcServicio", $("#hidtxtTipoBien").val(),
                             "pstrMontoServicioSoles", $("#txtMontoSoles").val(),
                             "pstrMontoServicioDolar", $("#txtMontoDolar").val(),
                             "pstrNumeroConstancia", $("#txtNroConstancia").val(),
                             "pstrFecEmisionConst", Fn_util_DateToString($("#txtFechaCompro").val()),
                             "pstrNumeroTipo2", $("#txtNumeroTipo2").val(),
                             "pstrSerieDoc2", $("#txtSerieDoc2").val(),
                             "pstrNroDoc2", strNumComNDNC,
                             "pstrFecVenc2", Fn_util_DateToString($("#txtFechaAdm").val()),
                             "pstrCodEstadoContrato", $("#hidCodEstadoContrato").val(),
                             "pstrOpcion", "N",
                             "pstrKeyTipoComprobante", "",
                             "pstrKeyNumeroComprobante", "",
                             "pstrKeyFechaEmision", "",
                             "pstrKeyCodProveedor", "",
                             "pstrCodEstadoDoc", $("#hidCodigoEstadoDoc").val(),
                             "pstrCodSolicitudCreditoAdd", $("#hddCodSolicitudCredito").val(),
                             "pstrPorc4ta", $("#txtPorc4ta").val(),
                             "pstrMonto4taSoles", $("#txtMonto4taSoles").val(),
                             "pstrMonto4taDolares", $("#txtMonto4taDolares").val()
                             ];


        fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/GrabarDesembolso",
                        arrParametros,
                        function(resultado) {
                            parent.fn_unBlockUI();
                            fn_LimpiarDetalle();
                            fn_ListarContratoEstructuraDoc();
                        },
                        function(resultado) {
                            parent.fn_unBlockUI();
                            var error = eval("(" + resultado.responseText + ")");
                            parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR Al GUARDAR");
                        }
            );
    }
}

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() {
    $("#jqGrid_lista_C").jqGrid({
        datatype: function() {
            fn_ListarContratoEstructuraDoc();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount",     // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodProveedor,TipoDocumento,NroDocumento,FechaEmision" // Índice de la columna con la clave primaria.
        },
        colNames: ['', '', '', '', 'Tipo Doc.', 'Nº Documento', 'Proveedor', 'Tipo Comprobante', 'Moneda', 'Gravado', 'IGV', 'No Gravado', 'Total', 'Total Convertido', 'Asociar', 'Desembolso', 'Estado', 'CodEstado', 'CodMoneda', 'CodEstadoInstruccion'],
        colModel: [
                { name: 'CodProveedor', index: 'CodProveedor', width: 0, hidden: true, sorttype: "string" },
                { name: 'TipoDocumento', index: 'TipoDocumento', width: 0, hidden: true, sorttype: "string" },
                { name: 'NroDocumento', index: 'NroDocumento', width: 0, hidden: true, sorttype: "string" },
                { name: 'FechaEmision', index: 'FechaEmision', width: 0, hidden: true },
                { name: 'TipoDocumentoProveedor', index: 'TipoDocumentoProveedor', width: 80 },
                { name: 'NumeroDocumentoProveedor', index: 'NumeroDocumentoProveedor', width: 120, align: "left" },
                { name: 'NombreProveedor', index: 'NombreProveedor', width: 250, align: "left" },
                { name: 'NombreTipoDocumento', index: 'NombreTipoDocumento', width: 130, sorttype: "string" },
                { name: 'NombreMoneda', index: 'NombreMoneda', width: 150, align: "left" },
                { name: 'MontoGravado', index: 'MontoGravado', width: 150, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'MontoIGV', index: 'MontoIGV', width: 150, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'MontoNoGravado', index: 'MontoNoGravado', width: 150, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'Total', index: 'Total', width: 150, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'TotalConvertido', index: 'TotalConvertido', width: 100, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'Asociar', index: 'Asociar', width: 80, align: "center", sortable: false, formatter: Asociar },
                { name: 'Desembolso', index: 'Desembolso', width: 90, align: "center", sortable: false, formatter: fnDesembolso },
                { name: 'NombreEstadoDocumento', index: 'NombreEstadoDocumento', width: 150, align: "center" },
                { name: 'EstadoDocumento', index: 'EstadoDocumento', hidden: true },
                { name: 'MonedaOriginal', index: 'MonedaOriginal', hidden: true, sorttype: "string" },
                { name: 'CodEstadoInstruccion', index: 'CodEstadoInstruccion', hidden: true, sorttype: "string" }
              ],
        height: '60%',
        pager: '#jqGrid_pager_C',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 5000,
        rowList: [10, 20, 30],
        sortname: 'Fecharegistro', // Columna a ordenar por defecto.
        sortorder: 'asc', // Criterio de ordenación por defecto.
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: true,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id);

            $("#hidCodigoEstadoDoc").val(rowData.EstadoDocumento);
            $("#hidCodProveedor").val(rowData.CodProveedor);
            $("#hidTipoDocumento").val(rowData.TipoDocumento);
            $("#hidNumeroDocumento").val(rowData.NroDocumento);
            $("#hidFecEmision").val(rowData.FechaEmision);
            $("#hidEstadoID").val(rowData.CodEstadoInstruccion);

        }
    });
    jQuery("#jqGrid_lista_C").jqGrid('navGrid', '#jqGrid_pager_C', { edit: false, add: false, del: false });

    $("#jqGrid_lista_C").setGridWidth($(window).width() - 110);
    $("#search_jqGrid_lista_C").hide();

    function Asociar(cellvalue, options, rowObject) {

        if (rowObject.EstadoDocumento == strCodigoEstadoDocumento.Anulado) {  // Anulado=4
            return "<img src='../Util/images/ico_acc_detalle.gif' alt='" + cellvalue + "' title='Asociar' width='20px' onclick=''  style='cursor: pointer;cursor: hand;' />";
        } else {
            $("#hidCodigoEstadoDoc").val(rowObject.EstadoDocumento);
            var param = "'" + rowObject.CodProveedor + "','" + rowObject.TipoDocumento + "','" + rowObject.NroDocumento + "','" + Fn_util_DateToString(rowObject.FechaEmision) + "','" + rowObject.EstadoDocumento + "'";
            var obj = "<img src='../Util/images/ico_acc_detalle.gif' alt='" + cellvalue + "' title='Asociar' width='20px' onclick=\"javascript:AsociarBienDesemb(" + param + ");\" style='cursor: pointer;cursor: hand;' />";
            return obj;
        }
    };

    function fnDesembolso(cellvalue, options, rowObject) {
        //debugger;
        var param = "'" + rowObject.CodProveedor + "','" + rowObject.TipoDocumento + "','" + rowObject.NroDocumento + "','" + Fn_util_DateToString(rowObject.FechaEmision) + "','" + rowObject.MonedaOriginal + "','" + rowObject.TotalConvertido + "','" + rowObject.ContadorBienes + "'";

        if (rowObject.EstadoDocumento == strCodigoEstadoDocumento.Formalizado) {  // Formalizado=2
            return "<input id='chkDesembolso' name='chkDesembolso' type='checkbox' runat='server' onclick=\"javascript:fn_seleccionaRegistro(this, " + param + " )\" />";
        } else if (rowObject.EstadoDocumento == strCodigoEstadoDocumento.Anulado) {  //Anulado Anulado
            intDocAnulados = intDocAnulados + 1;
            intDocBloqueados = intDocBloqueados + 1;
            return "<input id='chkDesembolso' name='chkDesembolso' type='checkbox' runat='server' disabled='true' />";
            //} else if (rowObject.EstadoDocumento == strCodigoEstadoDocumento.Desembolsado) {
            //IBK - JJM 
            //Estado = 5 Desembolsado, Estado = 4 Por desembolsar
        } else if (rowObject.EstadoDocumento == strCodigoEstadoDocumento.Desembolsado || rowObject.EstadoDocumento == 5) {
            $("#hddFlagRegDesembolso").val(1);
            intDocBloqueados = intDocBloqueados + 1;
            return "<input id='chkDesembolso' name='chkDesembolso' type='checkbox' runat='server' disabled='true' />";
        } else {
            return "<input id='chkDesembolso' name='chkDesembolso' type='checkbox' runat='server' onclick=\"javascript:fn_seleccionaRegistro(this, " + param + " )\" />";
        }
    };

}


//****************************************************************
// Funcion		:: 	fn_grabar
// Descripción	::	Grabar
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_grabar() {
    fn_util_redirect('frmDesembolsoListado.aspx');
}


//****************************************************************
// Funcion		:: 	AsociarBienDesemb
// Descripción	::	Asociar Bien Desembolso
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function AsociarBienDesemb(pCodProveedor, pTipoDocumento, pCodigoDocumento, pFechaEmision, pEstado) {
    var param = "csc=" + $("#txtNumeroContrato").val() + "&cp=" + pCodProveedor + "&ctd=" + pTipoDocumento + "&cnd=" + fn_util_trim(pCodigoDocumento) + "&cfe=" + pFechaEmision + "&clb=" + fn_util_trim($("#hidCodClasificacion").val()) + "&CEstado=" + pEstado + "&ctipoSubcontrato=" + fn_util_trim($("#hddSubtipoContrato").val()) + "";
    parent.fn_util_AbreModal("Lista :: BIENES", "Desembolso/frmBienBusqueda.aspx?" + param, 1000, 600, function() { });
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
function fn_obtenerProveedor(pCodProveedor, pCodTipoDoc, pRuc, pRazonSocial) {
    $('#hidCodProveedor').val(pCodProveedor);
    $('#txtRazonSocialProveedor').val(pRazonSocial);
    $('#txtNroDocProveed').val(pRuc);
    $("#cmdTipoDoc").val(pCodTipoDoc);
    fn_AgenteRetencion(pRuc);
    //fn_activaRetencionAutomatica();
}


//****************************************************************
// Funcion		:: 	Agente Retencion
// Descripción	::	Busca Agente Retenedor
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_AgenteRetencion(pRuc) {

    if (pRuc != "") {
        var arrParametros = ["pNroDocumento", fn_util_trim(pRuc)];

        fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/AgenteRetencion",
                        arrParametros,
                        function(resultado) {
                            $('#hidAgenteRetencion').val(resultado);

                            //Inicio IBK - AAE
                            //strRetencionProveedor = $('#hidAgenteRetencion').val(resultado);
                            strRetencionProveedor = $('#hidAgenteRetencion').val();
                            //Fin IBK
                            var strNumeroTipoAR = $('#txtNumeroTipo').val();
                            if ($('#hidAgenteRetencion').val() == 1) {
                                if (strNumeroTipoAR == strComprobante.CodigoDua) {
                                    $('#ChkRetencion').attr('disabled', 'disabled');
                                    $('#chkNinguno').attr('checked', true);
                                    fnHabiltarDetranReten(false);
                                }
                                else if (strNumeroTipoAR == strComprobante.CodigoNoDomiciliado || strNumeroTipoAR == strComprobante.CodigoReciboHonorarioNoDomiciliado) {
                                    $('#chkDetraccion').attr('disabled', true);
                                    $('#chkNinguno').attr('checked', true);
                                    fnHabiltarDetranReten(false);
                                }
                                else {
                                    //RF1_1 - IJM - 03/09/2012
                                    //Motivo :: Cuando el check esta en niguno limpiar la texto Tipo Bien y de desabilita                                    
                                    $('#chkNinguno').attr('checked', true);
                                    fnHabiltarDetranReten(false);
                                    $('#txtTipoBien').val('');
                                    fn_TipoBien();
                                }
                            } else {
                                if ($('#txtNumeroTipo').val() == strComprobante.CodigoDua) {
                                    $('#ChkRetencion').attr('disabled', 'disabled');
                                    $('#chkNinguno').attr('checked', true);
                                    fnHabiltarDetranReten(false);
                                } else if ($('#txtNumeroTipo').val() == strComprobante.CodigoNoDomiciliado) {
                                    $('#ChkRetencion').attr('disabled', 'disabled');
                                    $('#chkNinguno').attr('checked', true);
                                    fnHabiltarDetranReten(false);
                                } else {
                                    $('#ChkRetencion').attr('checked', true);
                                    fnHabiltarDetranReten(true);
                                    $('#txtTipoBien').val(strCodigoRetencion);
                                    fn_TipoBien();
                                }
                            }


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
// Log			:: 	JRC - 25/02/2012
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
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_DecripcionValorGenerico(strDominio, strParametro, strControlPrinc, strNombControl, strPorcentajeBien, strValorCompara, strTipo) {
    var arrParametros = ["pstrOp", "7", "pstrDominio", strDominio, "pstrParametro", strParametro];

    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');
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
// Funcion		:: 	fn_ListarContratoEstructuraDoc
// Descripción	::	ListaContratoEstructDocumentos
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_ListarContratoEstructuraDoc() {
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_C", "rowNum"),
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_C", "page"),
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_C", "sortname"),
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_C", "sortorder"),
                         "pCodContrato", $("#txtNumeroContrato").val()
                         ];
    fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/ListaContratoEstructDocumentos",
                   arrParametros,
                   function(jsondata) {
                       jqGrid_lista_C.addJSONData(jsondata);
                       fnVisualizarTablaTotal();
                       fn_doResize();
                   },
                   function(resultado) {
                       var error = eval("(" + resultado.responseText + ")");
                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL LISTAR");
                   }
    );
}

//****************************************************************
// Funcion		:: 	fnVisualizarTablaTotal
// Descripción	::	
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fnVisualizarTablaTotal() {
    var strHtml;
    strHtml = "<table><tr><th>CANTIDAD DOCS. :</th>";
    strHtml = strHtml + "<td>" + $("#jqGrid_lista_C").getGridParam("reccount") + "</td><td>&nbsp;&nbsp;&nbsp;&nbsp;</td><th>MONTO TOTAL :</th>";
    var decTotal = 0;
    var rows = jQuery("#jqGrid_lista_C").jqGrid('getRowData');
    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        var totalRegistro = fn_util_ValidaDecimal(row.TotalConvertido);
        var tipoDocumento = row.TipoDocumento;

        if (tipoDocumento == strComprobante.CodigoNotaCredito || tipoDocumento == strComprobante.CodigoNotaCreditoNoDomiciliado || tipoDocumento == strComprobante.CodigoNotaCreditoEspecial) {//tipoDocumento == strComprobante.CodigoNotaDebito || 
            totalRegistro = totalRegistro * -1;
        }

        decTotal = decTotal + totalRegistro;
    }

    strHtml = strHtml + "<td>" + fn_util_RedondearDecimalesComas(decTotal, 2) + "</td></tr></table>";

    $('#divTotal').html(strHtml);
}


//****************************************************************
// Funcion		:: 	fnHabiltarDetranReten
// Descripción	::	
// Log			:: 	JRC - 25/02/2012
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
// Funcion		:: 	fn_DatosObtenidos
// Descripción	::	
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_DatosObtenidos(response) {

    //BOTONES 
    $('#dv_cancelar').css("display", "block");
    $('#dv_Modificar').css("display", "block");
    $('#dv_agregar').css("display", "none");
    $('#dv_editar').css("display", "none");
    $('#dv_eliminar').css("display", "none");
    $('#dv_EditarReplicar').css("display", "none");

    var objEContratoEstructDoc = response;

    $("#hidTipoComprobante").val(objEContratoEstructDoc.Tipodocumento);
    $("#txtNumeroTipo").val(objEContratoEstructDoc.Tipodocumento);
    fn_DecripcionValorGenerico(strIdTabla.TipoDocumento, $('#txtNumeroTipo').val(), $('#txtNumeroTipo'), $('#lblNumeroTipo'), '');
    fn_NumeroTipo1();

    $("#hidCodigoEstadoDoc").val(objEContratoEstructDoc.Estadodocumento);
    $("#hidCodProveedor").val(objEContratoEstructDoc.CodProveedor);
    $("#cmdTipoDoc").val(objEContratoEstructDoc.CodigoTipoDocumentoProveedor);
    $("#txtNroDocProveed").val(objEContratoEstructDoc.NumeroDocumentoProveedor);
    $("#txtRazonSocialProveedor").val(objEContratoEstructDoc.NombreProveedor);

    //$("#lblNumeroTipo").html(objEContratoEstructDoc.NombreTipoComprobDoc1);
    $("#txtSerieDoc1").val(objEContratoEstructDoc.Numeroseriedocumento);
    var strNumeroDoc = objEContratoEstructDoc.Nrodocumento;
    if (strNumeroDoc == null) {
        strNumeroDoc = '';
    }
    $("#txtNumeroDoc1").val(strNumeroDoc.substring(4, strNumeroDoc.length));
    $("#txtFechaVenc1").val(objEContratoEstructDoc.StringFechaVencimiento);
    $("#txtAduana").val(objEContratoEstructDoc.CodigoTipoAduana);
    $("#lblAduana").html(objEContratoEstructDoc.NombreTipoAduana);
    $("#txtAnnoAduana").val(objEContratoEstructDoc.AnioDUA);

    $("#txtNumeroComprobante").val(objEContratoEstructDoc.NroComprobanteDUA);
    $("#cmbMoneda option").eq(objEContratoEstructDoc.Monedaoriginal).attr("selected", "selected");
    $("#cmbProcedencia option").eq(objEContratoEstructDoc.Codigoprocedencia).attr("selected", "selected");
    $("#txtFechaEmision").val(objEContratoEstructDoc.StringFechaEmision);
    $("#txtGravado").val(fn_util_AddCommas(fn_util_RedondearDecimales(objEContratoEstructDoc.MontoGravado, 2)));
    $("#txtPorcIGV").val(fn_util_AddCommas(fn_util_RedondearDecimales(objEContratoEstructDoc.Igvoriginal, 2)));

    var decMontoGravado = parseFloat(objEContratoEstructDoc.MontoGravado);
    var decIGV = parseFloat(objEContratoEstructDoc.Igvoriginal);
    var decMontoIGV = decMontoGravado * (decIGV / 100);

    //Inicio IBK
    //RPH Obtengo el Monto del IGV en el hidden si es que es actualizado
    $('#hidNontoIGV').val(fn_util_ValidaMonto(decMontoIGV, 2));
    var igv = $('#hidNontoIGV').val();    
    //Fin IBK

    $("#txtNumeroIGV").val(fn_util_AddCommas(fn_util_RedondearDecimales(objEContratoEstructDoc.Montoigv, 2)));
    $("#txtValorNoGravado").val(fn_util_AddCommas(fn_util_RedondearDecimales(objEContratoEstructDoc.MontoNoGravado, 2)));
    $("#txtTotal").val(fn_util_AddCommas(fn_util_RedondearDecimales(objEContratoEstructDoc.Total, 2)));

    if (objEContratoEstructDoc.FlagTipoCambioEspecial == 1) {
        $("#chktcespecial").attr("checked", true);
        //Inicio IBK - redondeo a 6 decimales
        $("#txttcespecial").val(fn_util_AddCommas(fn_util_RedondearDecimales(objEContratoEstructDoc.Tipocambioespecial, 6)));
        // Finb IBK
    } else {

        //NO DEBERIA TRAER EL TIPO DE CAMBIO DEL CAMPO , SI NO OBTENER EL TIPO DE CAMBIO DEL DIA
        //$("#txttcdia").val($("#hddtxttcdia").val());
        $("#txttcdia").val(fn_util_AddCommas(fn_util_RedondearDecimales(objEContratoEstructDoc.Tcutilizado, 6)));
    }

    $("#txtTipoCambioSunat").val(fn_util_ValidaMonto(objEContratoEstructDoc.Tcsbs, 6));
    //alert(objEContratoEstructDoc.Indicedetraccion);
    //alert(objEContratoEstructDoc.Indiceretencion);
    if (objEContratoEstructDoc.Indicedetraccion == 1) {
        $("#chkDetraccion").attr("checked", true);
        fnHabiltarDetranReten(true);
    }
    if (objEContratoEstructDoc.Indiceretencion == 1) {
        $('#ChkRetencion').attr('checked', true);
        $('#chkDetraccion').attr('checked', false);
        $('#chkNinguno').attr('checked', false);
        //IBK - JJM
        //Se agregó : $('#ChkRetencion').attr('checked', 'checked');
        $('#ChkRetencion').attr('checked', 'checked');
        fnHabiltarDetranReten(true);
        //fn_AgenteRetencion($("#txtNroDocProveed").val());
    }
    //Inicio IBK - AAE
    if ((objEContratoEstructDoc.Indiceretencion == 0) && (objEContratoEstructDoc.Indicedetraccion == 0)) {
        $('#ChkRetencion').attr('checked', false);
        $('#chkDetraccion').attr('checked', false);
        $('#chkNinguno').attr('checked', true);
        //IBK - JJM
        //Se agregó : $('#ChkRetencion').attr('checked', 'checked');
        $('#chkNinguno').attr('checked', 'checked');
        fnHabiltarDetranReten(false);
    }
    //FIn IBK

    $("#txtTipoBien").val(objEContratoEstructDoc.CodigoTipoServicio);
    $("#lblTipoBien").html(objEContratoEstructDoc.NombreTipoServicio);
    $("#hidtxtTipoBien").val(objEContratoEstructDoc.ServicioPorc);
    fn_TipoBien();
    if ($("#ChkRetencion").is(':checked') == true) {
        fn_validaCheckRetencion();
    }


    $("#txtMontoSoles").val(fn_util_AddCommas(fn_util_RedondearDecimales(objEContratoEstructDoc.MontoServicioSoles, 2)));
    $("#txtMontoDolar").val(fn_util_AddCommas(fn_util_RedondearDecimales(objEContratoEstructDoc.MontoServicioDolar, 2)));
    $("#txtNroConstancia").val(objEContratoEstructDoc.NroConstancia);
    $("#txtFechaCompro").val(objEContratoEstructDoc.StringFechaEmisionServicio);
    $("#txtNumeroTipo2").val(objEContratoEstructDoc.CodigoTipoComprobante);
    $("#lblNumeroTipo2").html(objEContratoEstructDoc.NombreTipoComprobDoc2);
    $("#txtSerieDoc2").val(objEContratoEstructDoc.NumeroSerieDocumentoAdd);

    var strNroComNDNC = objEContratoEstructDoc.NroDocumentoAdd;
    if (strNroComNDNC == null) {
        strNroComNDNC = '';
    }
    if (strNroComNDNC != '') {
        $("#txtNroDoc2").val(strNroComNDNC.substring(4, strNroComNDNC.length));
    }
    $("#txtFechaAdm").val(objEContratoEstructDoc.StringFechaEmisionAdd);
    $("#hidKeyTipoComprobante").val(objEContratoEstructDoc.Tipodocumento);
    $("#hidKeyFechaEmision").val(objEContratoEstructDoc.StringFechaEmision);
    $("#hidKeyCodProveedor").val(objEContratoEstructDoc.CodProveedor);

    if ($('#txtNumeroTipo').val() == strComprobante.CodigoDua) {
        $("#hidKeyNumeroComprobante").val(objEContratoEstructDoc.NroComprobanteDUA);
    } else {
        $("#hidKeyNumeroComprobante").val(objEContratoEstructDoc.Nrodocumento);
    }

    if ($('#txtNumeroTipo').val() == strComprobante.ReciboHonorario) {
        $("#spn_4taLabel").show();
        $("#spn_4taInput").show();
        $("#txtPorc4ta").val(fn_util_AddCommas(fn_util_RedondearDecimales(objEContratoEstructDoc.Porc4ta, 2)));
        $("#txtMonto4taSoles").val(fn_util_AddCommas(fn_util_RedondearDecimales(objEContratoEstructDoc.Monto4taSoles, 2)));
        $("#txtMonto4taDolares").val(fn_util_AddCommas(fn_util_RedondearDecimales(objEContratoEstructDoc.Monto4taDolares, 2)));
    }

    if ($("#hddActionBotonGrid").val() == 'R') {
        $('#ChkRetencion').attr('checked', false);
        $('#chkDetraccion').attr('checked', false);
        $('#chkNinguno').attr('checked', true);
        fnHabiltarDetranReten(false);
    }

    if ($('#txtNumeroTipo').val() == strComprobante.CodigoNoDomiciliado) {
        //$("#txtFechaVenc1").show();
        $("#spa_FecPago").show();
    }

    fn_ActivaCamposTipoComprobante();
    parent.fn_unBlockUI();
};

//****************************************************************
// Funcion		:: 	fn_ActivaCamposTipoComprobante
// Descripción	::
// Log			::
//****************************************************************
function fn_ActivaCamposTipoComprobante() {

    if ($('#txtNumeroTipo').val() == strComprobante.CodigoDua) {
        $('#trDUA').css("display", "block");
        $('#cmbMoneda').attr('disabled', 'disabled');
        $('#cmdTipoDoc').attr('disabled', 'disabled');
        $('#txtNroDocProveed').attr('readonly', 'readonly');
        $('#txtNroDocProveed').addClass('css_input_inactivo');
        $('#imgBuscarProveedor').css('visibility', 'hidden');
        $('#txtSerieDoc1').attr('disabled', 'disabled');
        $('#txtSerieDoc1').addClass('css_input_inactivo');
        $('#txtNumeroDoc1').attr('disabled', 'disabled');
        $('#txtNumeroDoc1').addClass('css_input_inactivo');
        if ($("#hddActionBotonGrid").val() == 'R') {
            $('#txtNumeroComprobante').val('');
        }
        $('#txtPorcIGV').attr('disabled', 'disabled');
        $('#txtPorcIGV').addClass('css_input_inactivo');
        $('#txtPorcIGV').val('0.00');

        $('#txtNumeroIGV').removeClass();
        $('#txtNumeroIGV').addClass('css_input');
        $('#txtNumeroIGV').removeAttr('disabled');

        $('#chkDetraccion').attr('disabled', 'disabled');
        $('#ChkRetencion').attr('disabled', 'disabled');
        $('#lblFecVenc').css("display", "block");
        //$('#txtFechaVenc1').css("display", "block");
        $("#spa_FecPago").show();

    } else {

        $('#trDUA').css("display", "none");
        $('#lblFecVenc').css("display", "none");
        //$('#txtFechaVenc1').css("display", "none");
        $("#spa_FecPago").hide();
    }


    if ($('#txtNumeroTipo').val() == strComprobante.CodigoReciboHonorarioNoDomiciliado || $('#txtNumeroTipo').val() == strComprobante.CodigoNoDomiciliado) {
        $('#lblFecVenc').css("display", "");
        //$('#txtFechaVenc1').css("display", "none");
        $("#spa_FecPago").show();
    }



    if ($('#hidCodigoEstadoDoc').val() == strCodigoEstadoDocumento.Desembolsado) {
        $('#txtNroConstancia').removeClass('css_input_inactivo');
        $('#txtNroConstancia').addClass('css_input');
        $('#txtNroConstancia').removeAttr('disabled');
        $('#txtFechaCompro').removeClass('css_input_inactivo');
        $('#txtFechaCompro').addClass('css_input');
        $('#txtFechaCompro').removeAttr('disabled');
        fn_util_SeteaCalendario($('input[id*=txtFechaCompro]'));
        $('#cmdTipoDoc').attr('disabled', 'disabled');
        $('#txtNroDocProveed').attr('disabled', 'disabled');
        $('#txtNumeroTipo').attr('disabled', 'disabled');
        $('#txtSerieDoc1').attr('disabled', 'disabled');
        $('#txtNumeroDoc1').attr('disabled', 'disabled');
        $('#cmbMoneda').attr('disabled', 'disabled');
        $('#txtFechaEmision').attr('disabled', 'disabled');
        $('#txtGravado').attr('disabled', 'disabled');
        $('#txtPorcIGV').attr('disabled', 'disabled');
        $('#chktcespecial').attr('disabled', 'disabled');
        $('#txtValorNoGravado').attr('disabled', 'disabled');
        $('#chkNinguno').attr('disabled', 'disabled');
        $('#chkDetraccion').attr('disabled', 'disabled');
        $('#ChkRetencion').attr('disabled', 'disabled');
        $('#imgBuscarProveedor').attr('disabled', 'disabled');
        $('#imgTipoBien').attr('disabled', 'disabled');
        $('#imgNumeroTipo').attr('disabled', 'disabled');
        $('#txtNumeroIGV').attr('disabled', 'disabled');
        $('#imgAduana').attr('disabled', 'disabled');
        $('#txtAduana').attr('disabled', 'disabled');
        $('#txtAnnoAduana').attr('disabled', 'disabled');
        $('#txtNumeroComprobante').attr('disabled', 'disabled');
        $('#txtFechaVenc1').attr('disabled', 'disabled');

        $('#txtPorc4ta').attr('disabled', 'disabled');


    }


    if ($('#txtNumeroTipo').val() == strComprobante.CodigoNotaDebito || $('#txtNumeroTipo').val() == strComprobante.CodigoNotaCredito) {
        $('#trComprobante2').css("display", "block");
        $('#imgNroDoc2').removeAttr('disabled', 'disabled');
    }

}



//****************************************************************
// Funcion		:: 	fn_LimpiarDetalle
// Descripción	::
// Log			::
//****************************************************************
function fn_LimpiarDetalle() {

    fn_util_SeteaObligatorio($("#cmbMoneda"), "select");
    fn_util_SeteaObligatorio($("#cmbProcedencia"), "select");
    fn_util_SeteaObligatorio($("#cmdTipoDoc"), "select");
    fn_util_SeteaObligatorio($("#txtNroDocProveed"), "input");
    $("#hidCodigoEstadoDoc").val('');
    $("#hidCodProveedor").val('');
    $("#cmdTipoDoc option").eq(0).attr("selected", "selected");
    $("#txtNroDocProveed").val('');
    $("#txtRazonSocialProveedor").val('');
    $("#txtNumeroTipo").val('');
    $("#lblNumeroTipo").html('');
    $("#txtSerieDoc1").val('');
    $("#txtNumeroDoc1").val('');
    $("#txtFechaVenc1").val('');
    $("#txtAduana").val('');
    $("#lblAduana").html('');
    $("#txtAnnoAduana").val('');
    $("#txtNumeroComprobante").val('');
    $("#hidCodProveedor").val('');
    $("#hidTipoDocumento").val('');
    $("#hidNumeroDocumento").val('');
    $("#hidFecEmision").val('');
    $('#txtSerieDoc1').removeAttr('disabled');
    $('#txtNumeroDoc1').removeAttr('disabled');
    $('#txtSerieDoc1').removeClass('css_input_inactivo');
    $('#txtNumeroDoc1').removeClass('css_input_inactivo');
    $('#txtSerieDoc1').addClass('css_input');
    $('#txtNumeroDoc1').addClass('css_input');
    $("#cmbMoneda").val($('#hidCodMoneda').val());
    $('#cmbMoneda').removeAttr('disabled');
    $('#txtGravado').removeClass();
    $('#txtGravado').addClass('css_input');
    $('#txtGravado').removeAttr('disabled');
    $('#txtPorcIGV').removeClass();
    $('#txtPorcIGV').addClass('css_input');
    $('#txtPorcIGV').removeAttr('disabled');
    $("#txtPorcIGV").val(fn_util_ValidaMonto("18", 2));
    $('#txtValorNoGravado').removeClass();
    $('#txtValorNoGravado').addClass('css_input');
    $('#txtValorNoGravado').removeAttr('disabled');
    

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

    if (fn_util_trim($("#hidCodMoneda").val()) == "001") {
        $("#txttcdia").val($("#hddtcdiaVenta").val());
    } else {
        $("#txttcdia").val($("#hddtcdiaCompra").val());
    }

    $("#chktcespecial").attr("checked", false);
    //$('#chktcespecial').attr('disabled', 'none');
    $("#txttcespecial").val('');
    $("#chkSunat").attr("checked", false);
    //$("#txtTipoCambioSunat").val('');
    $("#chkDetraccion").attr("checked", false);
    $("#ChkRetencion").attr("checked", false);
    $("#txtTipoBien").val('');
    $("#lblTipoBien").html('');
    $("#hidtxtTipoBien").val('');
    $("#txtMontoSoles").val('');
    $("#txtMontoDolar").val('');
    $("#txtNroConstancia").val('');
    $("#txtFechaCompro").val('');
    $("#txtNumeroTipo2").val('');
    $("#lblNumeroTipo2").html('');
    $("#txtSerieDoc2").val('');
    $("#txtNroDoc2").val('');
    $("#txtFechaAdm").val('');
    $('#dv_cancelar').css("display", "none");
    $('#dv_Modificar').css("display", "none");
    $('#dv_agregar').css("display", "block");
    $('#dv_editar').css("display", "block");
    $('#dv_eliminar').css("display", "block");
    $('#dv_EditarReplicar').css("display", "block");
    $("#chkNinguno").attr("checked", true);
    $('#txtTotal').addClass('css_input_inactivo');
    //fn_util_SeteaCalendario($('input[id*=txtFechaEmision]'));
    fn_util_SeteaCalendarioFunction($("#txtFechaEmision"), function() { fn_TipoCambioSunat(strModalidadTC.Sunat); });

    //$('#txtNumeroIGV').addClass('css_input_inactivo');

    $('#trComprobante2').css("display", "none");
    $('#hidAgenteRetencion').val('0');
    $('#trDUA').css("display", "none");
    $('#cmdTipoDoc').val(strTipoDocumentoIdentificacion.Ruc);
    //fn_TipoCambioDia(strModalidadTC.Sunat);
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

    $('#lblFecVenc').css("display", "none");
    //$('#txtFechaVenc1').css("display", "none");
    $("#spa_FecPago").hide();
}


//****************************************************************
// Funcion		:: 	fn_ModificarDetalle
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_ModificarDetalle2() {
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

    if (objtxtNumeroTipo[0].value == strComprobante.CodigoDua) {
        var objtxtAduana = $('input[id=txtAduana]:text');
        var objtxtAnnoAduana = $('input[id=txtAnnoAduana]:text');
        var objtxtNumeroComprobante = $('input[id=txtNumeroComprobante]:text');
        strError.append(fn_util_ValidateControl(objtxtAduana[0], 'una oficina de aduana válida', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtAnnoAduana[0], 'un año', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtNumeroComprobante[0], 'un número de comprobante', 1, ''));

        var dtmFechaVence = $("#txtFechaVenc1").val();
        var dtmFechaEmision = $("#txtFechaEmision").val();

        if (fn_util_ComparaFecha(dtmFechaVence, dtmFechaEmision)) {
            strError.append("La fecha de pago debe ser mayor a la fecha de Emisión");
        }
    }
    else {
        strError.append(fn_util_ValidateControl(objtxtNumeroDoc1[0], 'un número de documento', 1, ''));
    }
    //Valida
    strError.append(fn_util_ValidateControl(objhidCodProveedor[0], 'un proveedor válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtNumeroTipo[0], 'un tipo de comprobante', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtFechaEmision[0], 'una fecha emsisión', 1, ''));

    //if ($('#txtNumeroIGV').prop('class').indexOf('css_input_inactivo') == -1) { strError.append(fn_util_ValidateControl(objtxtNumeroIGV[0], 'número IGV', 1, '')); }
    if ($('#txtTotal').prop('class').indexOf('css_input_inactivo') == -1) { strError.append(fn_util_ValidateControl(objtxtTotal[0], 'un total', 1, '')); }

    strError.append(fn_util_ValidateControl(objcmbMoneda[0], 'una moneda', 1, ''));

    if ($('#chkNinguno').is(':checked') == false) {
        var objtxtCodTipoBien = $('#txtTipoBien');
        strError.append(fn_util_ValidateControl(objtxtCodTipoBien[0], 'un tipo válido', 1, ''));
    }
    var strNumComNDNC = '';
    if ($("#txtNroDoc2").val() != '') {
        var strNumSerNDNC = $("#txtSerieDoc2").val();
        strNumComNDNC = $("#txtNroDoc2").val();
        if ($('#hidTipoComNDNC').val() != strComprobante.CodigoDua) {
            var strPad = '0000';
            strNumComNDNC = strPad.substring(0, strPad.length - strNumSerNDNC.length) + strNumSerNDNC + strNumComNDNC;
        }
    }

    if (fn_util_ValidaMonto($("#txtTotal").val(), 2) == fn_util_ValidaMonto(0, 2)) {
        strError.append("El total debe ser mayor a cero");
    }
    //Inicio IBK
    //RPH Valido los montos de IGV
    var dblhidigv = fn_util_ValidaDecimal($('#hidNontoIGV').val());
    var dbligv = fn_util_ValidaDecimal($('#txtNumeroIGV').val());
    if (objtxtNumeroTipo[0].value != strComprobante.CodigoDua) {
        if (dblhidigv != dbligv) {
            var dif = 0;

            if (dbligv > dblhidigv) {
                dif = dbligv - dblhidigv;
            }
            else {
                dif = dblhidigv - dbligv;
            }
            //Inicio IBK - AAE - modifico validación
            //if (fn_util_ValidaMonto(dif, 2) > 0.5) {
            if (dif > 0.5) {
                //Fin IBK
                strError.append("IGV Ingresado fuera del rango de tolerancia");
            }
        };
    };
    //Fin IBK
    fn_ValidaDuplicidad(strError);
    //Valida si hay Error
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }
    else {
        //alert($("#chkDetraccion").is(':checked') ? '1' : '0');
        //alert($("#ChkRetencion").is(':checked') ? '1' : '0');
        var arrParametros = ["pstrCodContrato", $("#txtNumeroContrato").val(),
                             "pstrCodProveedor", $("#hidCodProveedor").val(),
                             "pstrNumeroTipo", $("#txtNumeroTipo").val(),
                             "pstrSerieDoc1", $("#txtSerieDoc1").val(),
                             "pstrNumeroDoc1", $("#txtNumeroDoc1").val(),
                             "pstrFecVenc", Fn_util_DateToString($("#txtFechaVenc1").val()),
                             "pstrAduana", $("#txtAduana").val(),
                             "pstrAnioAduana", $("#txtAnnoAduana").val(),
                             "pstrNumAduana", $("#txtNumeroComprobante").val(),
                             "pstrMoneda", $("#cmbMoneda").val(),
                             "pstrProcedencia", $("#cmbProcedencia").val(),
                             "pstrFecEmsion", Fn_util_DateToString($("#txtFechaEmision").val()),
                             "pstrGravado", $("#txtGravado").val(),
                             "pstrPorcIGV", $("#txtPorcIGV").val(),
                             "pstrIgv", $("#txtNumeroIGV").val(),
                             "pstrNoGravado", $("#txtValorNoGravado").val(),
                             "pstrTotal", $("#txtTotal").val(),
                             "pstrchkAdelantoProveedor", $("#chkAdelantoProveedor").is(':checked') ? '1' : '0',
                             "pstrAdelantoProveedor", $("#txtAdelantoProveedor").val(),
                             "pstrPorDesembolsar", $("#txtDesembolsar").val(),
                             "pstrTipoCambioDia", $("#txttcdia").val(),
                             "pstrchkTipoCambioEspecial", $("#chktcespecial").is(':checked') ? '1' : '0',
                             "pstrTipoCambioEspecial", $("#txttcespecial").val(),
                             "pstrchkTipoCambioSunat", $("#chkSunat").is(':checked') ? '1' : '0',
                             "pstrTipoCambioSunat", $("#txtTipoCambioSunat").val(),
                             "pstrchkDetraccion", $("#chkDetraccion").is(':checked') ? '1' : '0',
                             "pstrchkRetencion", $("#ChkRetencion").is(':checked') ? '1' : '0',
                             "pstrCodigoTipoBien", $("#txtTipoBien").val(),
                             "pstrPorcServicio", $("#hidtxtTipoBien").val(),
                             "pstrMontoServicioSoles", $("#txtMontoSoles").val(),
                             "pstrMontoServicioDolar", $("#txtMontoDolar").val(),
                             "pstrNumeroConstancia", $("#txtNroConstancia").val(),
                             "pstrFecEmisionConst", Fn_util_DateToString($("#txtFechaCompro").val()),
                             "pstrNumeroTipo2", $("#txtNumeroTipo2").val(),
                             "pstrSerieDoc2", $("#txtSerieDoc2").val(),
                             "pstrNroDoc2", strNumComNDNC,
                             "pstrFecVenc2", Fn_util_DateToString($("#txtFechaAdm").val()),
                             "pstrCodEstadoContrato", $("#hidCodEstadoContrato").val(),
                             "pstrOpcion", "M",
                             "pstrKeyTipoComprobante", $("#hidKeyTipoComprobante").val(),
                             "pstrKeyNumeroComprobante", $("#hidKeyNumeroComprobante").val(),
                             "pstrKeyFechaEmision", Fn_util_DateToString($("#hidKeyFechaEmision").val()),
                             "pstrKeyCodProveedor", $("#hidKeyCodProveedor").val(),
                             "pstrCodEstadoDoc", $("#hidCodigoEstadoDoc").val(),
                             "pstrCodSolicitudCreditoAdd", $("#hddCodSolicitudCredito").val(),
                             "pstrPorc4ta", $("#txtPorc4ta").val(),
                             "pstrMonto4taSoles", $("#txtMonto4taSoles").val(),
                             "pstrMonto4taDolares", $("#txtMonto4taDolares").val()
                             ];

        fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/GrabarDesembolso",
                        arrParametros,
                        function(resultado) {
                            parent.fn_unBlockUI();
                            fn_LimpiarDetalle();
                            fn_ListarContratoEstructuraDoc();
                        },
                        function(resultado) {
                            parent.fn_unBlockUI();
                            var error = eval("(" + resultado.responseText + ")");
                            parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR Al GUARDAR");
                        }
            );
    }
}


//****************************************************************
// Funcion		:: 	fn_ValorAduana
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_ValorAduana(boolValor) {
    $("#txtAduana").val('');
    $("#lblAduana").html('');
    $("#txtAnnoAduana").val('');
    $("#txtNumeroComprobante").val('');
    $('#txtFechaVenc1').val('');
    //Inicio IBK
    //RPH se agrego para limpiar los valores si se cambia de documentos
    $('#txtGravado').val('');
    $('#txtValorNoGravado').val('');

    // RPH Inicio Aqui vuelve y lo limpia
    $("#txtSerieDoc1").val('');
    $("#txtNumeroDoc1").val('');
    //Fin IBK       

    if (boolValor) {
        $('#trDUA').css("display", "block");

        $('#txtSerieDoc1').attr('disabled', 'disabled');
        $('#txtSerieDoc1').removeClass();
        $('#txtSerieDoc1').addClass('css_input_inactivo');
        $('#txtNumeroDoc1').attr('disabled', 'disabled');
        $('#txtNumeroDoc1').removeClass();
        $('#txtNumeroDoc1').addClass('css_input_inactivo');
        $("#cmbMoneda").val(strMonedaDolares);
        $('#cmbMoneda').attr('disabled', 'disabled');

        $("#chkSunat").removeAttr('disabled');
        $('#txtGravado').addClass('css_input');
        $('#txtPorcIGV').attr('disabled', 'disabled');
        $('#txtPorcIGV').addClass('css_input_inactivo');
        $("#txtPorcIGV").val(fn_util_ValidaMonto(intIgv, 2));

        $('#txtNumeroIGV').removeClass();
        $('#txtNumeroIGV').addClass('css_input');
        $('#txtNumeroIGV').removeAttr('disabled');

        fn_DatosProveedorSunat(true);

        $('#lblFecVenc').css("display", "block");
        //$('#txtFechaVenc1').css("display", "block");        
        $("#spa_FecPago").show();
        $("#txtFechaVenc1").datepicker("enable");

        $('#txtFechaEmision').removeClass();
        $('#txtFechaEmision').addClass('css_input');
        fn_util_SeteaCalendarioFunction($("#txtFechaEmision"), function() { fn_TipoCambioSunat(strModalidadTC.Sunat); });

    } else {

        $('#trDUA').css("display", "none");
        $("#chkSunat").attr('disabled', 'disabled');
        $('#txtSerieDoc1').removeAttr('disabled');
        $('#txtSerieDoc1').removeClass();
        $('#txtSerieDoc1').addClass('css_input');
        $("#cmbMoneda").val($('#hidCodMoneda').val());
        //Inicio IBK - AAE solo habilito si estaba deshabilitado
        if ($("#cmbMoneda").attr('disabled') == 'disabled') {
            $('#cmbMoneda').removeAttr('disabled');
        }
        //$('#cmbMoneda').removeAttr('disabled');
        //Fin IBK
        $('#txtNumeroDoc1').removeAttr('disabled');
        $('#txtNumeroDoc1').removeClass();
        $('#txtNumeroDoc1').addClass('css_input');
        $('#txtGravado').removeClass();
        $('#txtGravado').addClass('css_input');
        $('#txtPorcIGV').removeClass();
        $('#txtPorcIGV').addClass('css_input');
        $('#txtPorcIGV').removeAttr('disabled');
        $("#txtPorcIGV").val(fn_util_ValidaMonto(intIgv, 2));

        //$('#txtNumeroIGV').removeClass();
        //$('#txtNumeroIGV').addClass('css_input_inactivo');
        //$('#txtNumeroIGV').attr('disabled', 'disabled');
        //$('#txtNumeroIGV').val('');

        $('#lblFecVenc').css("display", "none");
        //$('#txtFechaVenc1').css("display", "none");    		
        $("#spa_FecPago").hide();
        $("#txtFechaVenc1").datepicker("disable");

        $('#chkDetraccion').removeAttr('disabled');
        $('#ChkRetencion').removeAttr('disabled');
        $('#txtFechaEmision').removeClass();
        $('#txtFechaEmision').addClass('css_input');

        //fn_util_SeteaCalendario($('input[id*=txtFechaEmision]'));
        //fn_util_SeteaCalendarioFunction($("#txtFechaEmision"), function() { fn_TipoCambioSunat(strModalidadTC.Sunat); });
        fn_DatosProveedorSunat(false);
    }
}


//****************************************************************
// Funcion		:: 	fn_DatosProveedorSunat
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_DatosProveedorSunat(pbool) {
    if (pbool) {
        $("#txtNroDocProveed").val(strRucSunat);
        $("#cmdTipoDoc").val(strTipoDocumentoIdentificacion.Ruc);
        fn_buscarProveedor();
        $("#imgBuscarProveedor").css('visibility', 'hidden');
        //Inicio IBK  
        //--RPH Inicio habilito el campo para poder ingresar nuevamente al proveedor  
        //$('#txtNroDocProveed').attr('readonly', 'readonly');
        $('#txtNroDocProveed').attr('disabled', 'disabled');
        $('#txtNroDocProveed').removeClass();
        $('#txtNroDocProveed').addClass('css_input_inactivo');
        //--Fin IBK
        $('#cmdTipoDoc').attr('disabled', 'disabled');
        $('#cmdTipoDoc').removeClass();
        $('#cmdTipoDoc').addClass('css_select');

    } else {
        //Inicio IBK    
        //$('#txtNroDocProveed').removeAttr('readonly');
        $('#txtNroDocProveed').removeAttr('disabled');
        //$('#txtNroDocProveed').prop('readonly', false);
        $('#txtNroDocProveed').removeClass();
        //Fin IBK
        $('#txtNroDocProveed').addClass('css_input');
        $('#cmdTipoDoc').removeAttr('disabled');
        if ((($('#hidTipoComprobante').val() != strComprobante.CodigoDua) || ($('#txtNumeroTipo').val() != strComprobante.CodigoNoDomiciliado)) && ($('#txtNroDocProveed').val() == strRucSunat)) {
            $("#txtNroDocProveed").val('');
            $("#cmdTipoDoc").val(strTipoDocumentoIdentificacion.Ruc);
            $("#txtRazonSocialProveedor").val('');
            $('#hidCodProveedor').val('0');
        }
        $("#imgBuscarProveedor").css('visibility', 'visible');
        $('#cmdTipoDoc').removeClass();
        $('#cmdTipoDoc').addClass('css_select_obligatorio');
    }
}


//****************************************************************
// Funcion		:: 	fn_EliminarLogica
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_EliminarLogica() {
    var arrParametros = ["pstrCodContrato", $("#txtNumeroContrato").val(),
                         "pstrCodProveedor", $("#hidCodProveedor").val(),
                         "pstrNumeroTipo", $("#hidTipoDocumento").val(),
                         "pstrNumeroDoc1", $("#hidNumeroDocumento").val(),
                         "pstrFecEmision", Fn_util_DateToString($("#hidFecEmision").val())
                         ];
    fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/EliminarDesembolso",
    arrParametros,
                    function(resultado) {
                        parent.fn_unBlockUI();
                        $("#hidCodProveedor").val('');
                        $("#cmdTipoDoc").val('');
                        $("#txtNumeroTipo").val('');
                        $("#txtNumeroDoc1").val('');
                        $("#txtFechaEmision").val('');
                        fn_ListarContratoEstructuraDoc();
                    },
                    function(resultado) {
                        parent.fn_unBlockUI();
                        var error = eval("(" + resultado.responseText + ")");
                        parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL ELIMINAR");
                    }
        );
}


//****************************************************************
// Funcion		:: 	fn_ValidaDuplicidad
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_ValidaDuplicidad(pError) {

    var strNroDocumento = $('#txtNumeroDoc1').val();
    if ($("#txtNumeroTipo").val() == strComprobante.CodigoDua) { strNroDocumento = $('#txtNumeroComprobante').val(); }

    var arrParametros = ["CodigoContrato", $("#txtNumeroContrato").val(),
                         "CodProveedor", $("#hidCodProveedor").val(),
                         "TipoDocumento", $("#txtNumeroTipo").val(),
                         "NroDocumento", strNroDocumento,
                         "FechaEmision", Fn_util_DateToString($("#txtFechaEmision").val()),
                         "KeyCodProveedor", $("#hidKeyCodProveedor").val(),
                         "KeyTipoComprobante", $("#hidKeyTipoComprobante").val(),
                         "KeyNumeroComprobante", $("#hidKeyNumeroComprobante").val(),
                         "KeyFechaEmision", Fn_util_DateToString($("#hidKeyFechaEmision").val()),
                         "EstadoDocumento", "1"  //1?
                         ];

    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whValidaDocumentoDesembolso', '../');
    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            if (parseFloat(arrResultado[1]) > 0) { pError.append('&nbsp;&nbsp;- El documento ingresado ya se encuentra registrado<br />'); }

        }
    }
}


//****************************************************************
// Funcion		:: 	fn_MontoIGV
// Descripción	::	
// Log			:: 	
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
// Funcion		:: 	fn_ValorTotal
// Descripción	::	
// Log			:: 	
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
// Funcion		:: 	fn_ValorDetraccion
// Descripción	::	
// Log			:: 	
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
// Log			:: 	
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
// Log			:: 	
//****************************************************************
function fn_buscarProveedor() {
    //debugger;
    if ($('#txtNroDocProveed').attr('readonly') != 'readonly') {
        var strRuc = $('#txtNroDocProveed').val();
        var strtipo = $('#cmdTipoDoc').val();
        if (strRuc != '') {
            var arrParametros = ["pstrRuc", strRuc, "pstrTipoDocumento", strtipo];
            var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whProveedor', '../');
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
// Log			:: 	
//****************************************************************
function fn_NumeroTipo1() {
    //debugger;
    if ($('#txtNumeroTipo').val() != "") {

        //Comprobante Oculto
        fn_desactivaComprobanteAdd();

        fn_DecripcionValorGenerico(strIdTabla.TipoDocumento, $('#txtNumeroTipo').val(), $('#txtNumeroTipo'), $('#lblNumeroTipo'), '');
        fn_ValorAduana(false);

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
            $('#txtSerieDoc1').val('');
            $('#txtNumeroDoc1').val('');
            fn_MontoIGV();
            fn_ValorDetraccion();
            fn_ValorRetencion();
            fn_ValorTotal();
        }

        if ($('#txtNumeroTipo').val() == strComprobante.CodigoDua) {
            fn_ValorAduana(true);
            $('#chkDetraccion').attr('disabled', 'disabled');
            $('#ChkRetencion').attr('disabled', 'disabled');
            $('#ChkRetencion').attr('checked', false);
            $('#chkDetraccion').attr('checked', false);
            $('#chkNinguno').attr('checked', true);
            $('#txtPorcIGV').val(fn_util_ValidaMonto(0, 2));
            fnHabiltarDetranReten(false);
            $("#hidAgenteRetencion").val("0");
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
        else if ($('#txtNumeroTipo').val() == strComprobante.CodigoNoDomiciliado) {

            $('#lblFecVenc').css("display", "block");
            $('#chkDetraccion').attr('disabled', 'disabled');
            $('#ChkRetencion').attr('enable', 'enable');
            $('#txtPorcIGV').val('0.00');
            $('#lblFecVenc').css("display", "block");
            //$('#txtFechaVenc1').css("display", "block");
            $("#txtFechaVenc1").datepicker("enable");
            $("#spa_FecPago").show();
            $('#txtNumeroIGV').val('0.00');

            $('#ChkRetencion').attr('checked', false);
            $('#chkDetraccion').attr('checked', false);
            $('#chkNinguno').attr('checked', true);
            $('#txtPorcIGV').val(fn_util_ValidaMonto(0.00));

            $('#trComprobante2').css("display", "none");
            $('#imgNroDoc2').attr('disabled', true);
            //fnHabiltarDetranReten(false);
            fnHabiltarDetranReten(true);

            //fn_activaRetencionAutomatica();
            //fn_activaRetencionAutomatica2();


        }
        //else if ($('#txtNumeroTipo').val() == strComprobante.CodigoNoDomiciliado || $('#txtNumeroTipo').val() == strComprobante.CodigoReciboHonorarioNoDomiciliado) {
        else if ($('#txtNumeroTipo').val() == strComprobante.CodigoReciboHonorarioNoDomiciliado) {

            $('#lblFecVenc').css("display", "block");
            $('#chkDetraccion').attr('disabled', 'disabled');
            //$('#ChkRetencion').attr('disabled', 'disabled');
            $('#txtPorcIGV').val('0.00');
            $('#lblFecVenc').css("display", "block");
            //$('#txtFechaVenc1').css("display", "block");
            $("#txtFechaVenc1").datepicker("enable");
            $("#spa_FecPago").show();
            $('#txtNumeroIGV').val('0.00');

            $('#ChkRetencion').attr('checked', false);
            $('#chkDetraccion').attr('checked', false);
            $('#chkNinguno').attr('checked', true);
            $('#txtPorcIGV').val(fn_util_ValidaMonto(0.00));

            $('#trComprobante2').css("display", "none");
            $('#imgNroDoc2').attr('disabled', true);
            //fnHabiltarDetranReten(false);
            fnHabiltarDetranReten(true);

            //fn_activaRetencionAutomatica();
            fn_activaRetencionAutomatica2();


        }
        else if ($('#txtNumeroTipo').val() == strComprobante.Factura) {

            $('#trComprobante2').css("display", "none");
            $('#imgNroDoc2').attr('disabled', true);

            //$('#chkDetraccion').attr('disabled', 'disabled');
            //$('#ChkRetencion').attr('disabled', 'disabled');
            $('#ChkRetencion').attr('checked', false);
            $('#chkDetraccion').attr('checked', false);
            $('#chkNinguno').attr('checked', true);

            fnHabiltarDetranReten(false);
            fn_activaRetencionAutomatica2();
            //fn_activaRetencionAutomatica();

        }

        else if ($('#txtNumeroTipo').val() == strComprobante.CodigoNotaCreditoEspecial || $('#txtNumeroTipo').val() == strComprobante.CodigoNotaDebidoEspecial) {

            $('#trComprobante2').css("display", "block");
            $('#imgNroDoc2').removeAttr('disabled');

            $('#txtNumeroTipo2').val(strComprobante.Factura);
            fn_NumeroTipo2();

            $('#chkDetraccion').attr('disabled', 'disabled');
            $('#ChkRetencion').attr('disabled', 'disabled');
            $('#ChkRetencion').attr('checked', false);
            $('#chkDetraccion').attr('checked', false);
            $('#chkNinguno').attr('checked', true);

            fnHabiltarDetranReten(false);
            //fn_activaRetencionAutomatica();         

        }

        else if ($('#txtNumeroTipo').val() == strComprobante.CodigoNotaDebidoNoDomiciliado || $('#txtNumeroTipo').val() == strComprobante.CodigoNotaCreditoNoDomiciliado) {

            $('#trComprobante2').css("display", "block");
            $('#imgNroDoc2').removeAttr('disabled');

            $('#txtNumeroTipo2').val(strComprobante.CodigoNoDomiciliado);
            fn_NumeroTipo2();

            $('#chkDetraccion').attr('disabled', 'disabled');
            $('#ChkRetencion').attr('enable', 'enable');
            $('#ChkRetencion').attr('checked', false);
            $('#chkDetraccion').attr('checked', false);
            $('#chkNinguno').attr('checked', true);

            fnHabiltarDetranReten(true);
            //fnHabiltarDetranReten(false);

            //Deshabilita Busqueda de tipo Doc
            //            $('#txtNumeroTipo2').attr('disabled', 'disabled');
            //            $('#imgNumeroTipo2').attr('disabled', 'disabled');

        }


        else if ($('#txtNumeroTipo').val() == strComprobante.CodigoNotaDebito || $('#txtNumeroTipo').val() == strComprobante.CodigoNotaCredito) {

            $('#trComprobante2').css("display", "block");
            $('#imgNroDoc2').removeAttr('disabled');

            $('#txtNumeroTipo2').val(strComprobante.Factura);
            fn_NumeroTipo2();

            //$('#chkDetraccion').attr('disabled', 'disabled');
            //$('#ChkRetencion').attr('disabled', 'disabled');
            $('#ChkRetencion').attr('checked', false);
            $('#chkDetraccion').attr('checked', false);
            $('#chkNinguno').attr('checked', true);

            fnHabiltarDetranReten(false);
            //fn_activaRetencionAutomatica();                        

            //Deshabilita Busqueda de tipo Doc
            $('#txtNumeroTipo2').attr('disabled', 'disabled');
            $('#imgNumeroTipo2').attr('disabled', 'disabled');
            fn_activaRetencionAutomatica2();
        }

        else {

            $('#lblFecVenc').css("display", "none");
            //$('#txtFechaVenc1').css("display", "none");
            $("#spa_FecPago").hide();
            fn_AgenteRetencion($("#txtNroDocProveed").val());

            $('#trComprobante2').css("display", "none");
            $('#imgNroDoc2').attr('disabled', 'disabled');
            $("#txtNumeroTipo2").val('');
            $("#lblNumeroTipo2").html('');
            $("#txtSerieDoc2").val('');
            $("#txtNroDoc2").val('');
            $("#txtFechaAdm").val('');

            $('#chkDetraccion').attr('disabled', true);
            $('#ChkRetencion').attr('disabled', true);
            fn_activaRetencionAutomatica2();

        }

    } else {
        fn_ValorAduana(false);
        $('#lblNumeroTipo').html('');
        $('#txtSerieDoc1').val('');
        $('#txtNumeroDoc1').val('');
    }
}


//****************************************************************
// Funcion		:: 	fn_AduanaDUA
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_AduanaDUA() {
    if ($('#txtAduana').val() != "") {
        fn_DecripcionValorGenerico(strIdTabla.Aduanas, $('#txtAduana').val(), $('#txtAduana'), $('#lblAduana'), '');
    }
    else {
        $('#lblAduana').html('');
    }
}


//****************************************************************
// Funcion		:: 	fn_SetearTipoComprobante()
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_SetearTipoComprobante() {
    $('#hidTipoComprobante').val($('#txtNumeroTipo').val());
}

//****************************************************************
// Funcion		:: 	fn_NumeroTipo2()
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_NumeroTipo2() {
    if ($('#txtNumeroTipo2').val() != "") {
        fn_DecripcionValorGenerico(strIdTabla.TipoDocumento, $('#txtNumeroTipo2').val(), $('#txtNumeroTipo2'), $('#lblNumeroTipo2'), '');
    } else {
        $('#lblNumeroTipo2').html('');
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
// Log			:: 	
//****************************************************************
function AbrirListaGenerico(pintDominio, pstrTitulo, pstrProvino) {
    var strpagina = "Comun/frmListaValorGenerico.aspx?ncd=" + pintDominio + "&nt=" + pstrTitulo + "&np=" + pstrProvino;
    parent.fn_util_AbreModal("Listado :: Búsqueda Valor Genérico", strpagina, 700, 500, function() { });
}


//****************************************************************
// Funcion		:: 	fn_obtenerValorGenerico
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_obtenerValorGenerico(pstrProvino, pstrCodigo, pstrDescrip, pstrValor2, pstrValor3, pstrValor4) {

    //alert(pstrProvino+"-"+pstrCodigo+"-"+pstrDescrip+"-"+pstrValor2+"-"+pstrValor3+"-"+pstrValor4);

    if (pstrProvino == 'NumeroTipo') {
        $('#txtNumeroTipo').val(pstrCodigo);
        $('#lblNumeroTipo').html(pstrDescrip);

        fn_NumeroTipo1();

        /*
        if ($('#txtNumeroTipo').val() == strComprobante.CodigoDua) {
        fn_ValorAduana(true);
        $('#chkNinguno').attr('checked', true);
        } else {
        fn_ValorAduana(false);
        }

        if ($('#txtNumeroTipo').val() == strComprobante.CodigoNoDomiciliado) {
        $('#lblFecVenc').css("display", "block");
        //$('#txtFechaVenc1').css("display", "block");
        $("#spa_FecPago").show();

            $('#txtPorcIGV').val(fn_util_ValidaMonto(0,2));
        $('#chkNinguno').attr('checked', true);
        }


        if ($('#txtNumeroTipo').val() == strComprobante.CodigoNotaDebito || $('#txtNumeroTipo').val() == strComprobante.CodigoNotaCredito) {
        $('#trComprobante2').css("display", "block");
        $('#imgNroDoc2').removeAttr('disabled');

            $('#txtNumeroTipo2').val(strComprobante.Factura);
        fn_NumeroTipo2();
        // $('#txtNumeroTipo2').val();

        } else if ($('#txtNumeroTipo').val() == strComprobante.CodigoNotaDebidoNoDomiciliado || $('#txtNumeroTipo').val() == strComprobante.CodigoNotaCreditoNoDomiciliado) {
        $('#trComprobante2').css("display", "block");
        $('#imgNroDoc2').removeAttr('disabled');
        $('#txtNumeroTipo2').val(strComprobante.CodigoNoDomiciliado);
        fn_NumeroTipo2();
        }else {
        $('#trComprobante2').css("display", "none");
        $("#txtNumeroTipo2").val('');
        $("#lblNumeroTipo2").html('');
        $("#txtSerieDoc2").val('');
        $("#txtNroDoc2").val('');
        $("#txtFechaAdm").val('');
        }
        */

    }
    else if (pstrProvino == 'NumeroTipo2') {
        $('#txtNumeroTipo2').val(pstrCodigo);
        $('#lblNumeroTipo2').html(pstrDescrip);
    }
    else if (pstrProvino == 'Aduana') {
        $('#txtAduana').val(pstrCodigo);
        $('#lblAduana').html(pstrDescrip);
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
    //fn_activaRetencionAutomatica2();
}


//****************************************************************
// Funcion		:: 	fn_seleccionaRegistro
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_seleccionaRegistro(pCheck, pCodProveedor, pTipoDocumento, pCodigoDocumento, pFechaEmision, pMoneda, pMonto, pContador, pEstadoDoc) {
    //debugger;
    var pChekeados = $("#hidDesembolso").val();
    var pChekeados1 = $("#hidDesembolso1").val();

    //var pParametro = pCodProveedor + "$" + pTipoDocumento + "$" + fn_util_trim(pCodigoDocumento) + "$" + pFechaEmision + "$" + pMoneda + "$" + pMonto;
    var pParametro = "'" + $("#txtNumeroContrato").val() + "" + pFechaEmision + "" + pTipoDocumento + "" + fn_util_trim(pCodigoDocumento) + "" + pCodProveedor + "'";
    var pParametro1 = pCodProveedor + "$" + pTipoDocumento + "$" + fn_util_trim(pCodigoDocumento) + "$" + pFechaEmision + "$" + pMoneda + "$" + pMonto;
    var pChkComplete = '';
    var pChkComplete1 = '';

    if (pCheck.checked == true) {
        if (pParametro.length == 0) {
            pChekeados = pParametro;
            pChekeados1 = pParametro1;
        } else {
            pChekeados += pParametro + ",";
            pChekeados1 += pParametro1 + "|";
        }

    } else {
        var lblCheckedResult = pChekeados.split(",");
        var lblCheckedResult1 = pChekeados1.split("|");
        for (var i = 0; i < lblCheckedResult.length; i++) {
            if (pParametro != lblCheckedResult[i]) {
                if (lblCheckedResult[i] != "") {
                    pChkComplete += lblCheckedResult[i] + ",";
                    pChkComplete1 += lblCheckedResult1[i] + "|";
                }
            }
        }
        pChekeados = pChkComplete;
        pChekeados1 = pChkComplete1;
    }

    $("#hidDesembolso").val(pChekeados);
    $("#hidDesembolso1").val(pChekeados1);
}



//Inicio IBK - AAE - Controlo que para un leasing total el valor venta sea igual al precio venta
//****************************************************************
// Funcion		:: 	fn_generarID
// Descripción	::	Chequeo si es un leasing total que el total de documentos estén marcados
// Log			:: 	
//****************************************************************
function fn_generarID() {
    
    var nbrMontoDoc = 0;
    var totalCredito = fn_util_ValidaDecimal($("#hidMontoFinanciado").val());
    //si es un leasing total
    if ($("#hddSubtipoContrato").val() == "001") {
        //chequeo que el monto a desembolsar sea igual al monto de los documentos seleccionados
        var docs = $("#hidDesembolso1").val();
        var documentos = docs.split("|");
        //alert(documentos.length);
        for (var i = 0; i < documentos.length; i++) {
            var doc = documentos[i].split("$");
            if (doc.length > 1) {
                var monto = doc[5];
                nbrMontoDoc = nbrMontoDoc + fn_util_ValidaDecimal(monto);
            }
        }
        if (nbrMontoDoc != totalCredito) {
            parent.fn_mdl_confirma("¿El leasing es Total pero el monto de los documentos NO coincide con el Valor Venta, desea continuar?"
    				  , function() { fn_generarID2(); }
					  , "util/images/question.gif"
					  , function() { }
					  , "Activación de Instrucción"
					  );
        } else {
            fn_generarID2();
        }
    } else {
        fn_generarID2();
    }
}
//Fin IBK - AAE


//****************************************************************
// Funcion		:: 	fn_generarID2
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_generarID2() {
    //debugger;
    var booEjecuta = false;
    var booActivacion = false;
    var strMensaje = "";
    //var strMensajePrueba = fn_ValidarCheckTipoCambioEspecial();
    //if (fn_TodoChekeadosActivos() && $('#ChkActivacionLeasing').is(':checked') == true) {
    if ((fn_TotalChekeadosActivos() == false) && $('#ChkActivacionLeasing').is(':checked') == true) {
        fn_ejecutaGeneraID('1');
    } else {

        if (fn_TodoChekeadosActivos()) {
            strMensaje = "No existen documentos para seleccionar. Sólo puede generar la Instrucción con ACTIVACIÓN.";
        } else if ($("#hidDesembolso").val() == "") {
            strMensaje = "No ha seleccionado nigún documento a desembolsar. No se puede Generar la Instrucción de Desembolso.";
        } else {

            if ($('#ChkActivacionLeasing').is(':checked') == true) {

                if (fn_TotalChekeadosActivos() == true) {
                    strMensaje = "La Activación ha sido marcada, debe seleccionar todos los documentos restantes.";
                } else {
                    booEjecuta = true;
                    booActivacion = true;
                }

            } else {
                booEjecuta = true;
            }

        }

        if (booEjecuta) {
            if (booActivacion) {
                parent.fn_mdl_confirma("¿Está seguro que sea generar la Instrucción de Desembolso con ACTIVACIÓN?"
    				  , function() { fn_ejecutaGeneraID('1'); }
					  , "util/images/question.gif"
					  , function() { }
					  , "Activación de Instrucción"
				);
            } else {
                parent.fn_mdl_confirma("¿Está seguro que sea generar la Instrucción de Desembolso?"
    				  , function() { fn_ejecutaGeneraID('0'); }
					  , "util/images/question.gif"
					  , function() { }
					  , "Generar Instrucción Desembolso"
				);
            }
        } else {
            parent.fn_mdl_mensajeIco(strMensaje, "util/images/warning.gif", "ALERTA");
        }

    }

}
function fn_ejecutaGeneraID(pstrCheckActivacion) {
    //Inicio IBK - AAE
    var strActivacion = pstrCheckActivacion;
    if ($("#hidCodSubContrato").val() == strSubTipoContrato.Total) {
        strActivacion = "1";
    }
    //Fin IBK
    parent.fn_blockUI();
    var arrParametros = ["pstrCodContrato", $("#txtNumeroContrato").val(),
							  "pstrChekeados", $("#hidDesembolso").val(),
							  "pstrChekeados1", $("#hidDesembolso1").val(),
							  "pstrActivacion", strActivacion,
							  "pstrTipoLeasing", $("#hddSubtipoContrato").val()];

    fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/RegistrarID",
		arrParametros,
		function(resultado) {
		    parent.fn_unBlockUI();
		    var varresult = resultado.split("|");
		    if (varresult[0] == "0") {
		        parent.fn_mdl_mensajeOk("Se grabó correctamente los datos. Se generó la ID Nº" + fn_util_trim(varresult[1]), function() { fn_redireccionarID(fn_util_trim(varresult[1])) }, "GRABADO CORRECTO");
		    } else {
		        parent.fn_mdl_mensajeIco(varresult[1], "util/images/error.gif", "ERROR EN GENERACIÓN");
		    }
		},
		function(resultado) {
		    parent.fn_unBlockUI();
		    var error = eval("(" + resultado.responseText + ")");
		    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GENERACIÓN");
		}
	);


}
function fn_redireccionarID(pstrInstruccion) {
    fn_util_redirect("../InsDesembolso/frmInsDesembolsoRegistro.aspx?hddCodigoContrato=" + $("#txtNumeroContrato").val() + "&hddCodigoInsDesembolso=" + pstrInstruccion);
}

//****************************************************************
// Funcion		:: 	fn_ValidarCheckTipoCambioEspecial
// Descripción	::	Valida los checks Tipo Cambio Especial
// Autor        ::  JJM IBK
//****************************************************************
//function fn_ValidarCheckTipoCambioEspecial() {
//    debugger;
//    var total;
//    var rows = jQuery("#jqGrid_lista_C").jqGrid('getRowData');
//    var lblCheckedResult = $("#hidDesembolso1").val().split("|");
//    for (var i = 0; i < rows.length; i++) {
//        var row = rows[i];        
//        var sDesembolso = row.Desembolso;
//        if (sDesembolso.checked) {
//            //if ($('#chkDesembolso').is(':checked')) {
//            total = total + 1;
//        }
//    }
//}


//****************************************************************
// Funcion		:: 	fn_TotalChekeados
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_TotalChekeados() {
    //debugger;
    var pSubContrato = $("#hidCodSubContrato").val();
    if (pSubContrato == strSubTipoContrato.Total) { //TOTAL
        var CantTotalGrilla = $("#jqGrid_lista_C").getGridParam("reccount");
        var lblCheckedResult = $("#hidDesembolso").val().split(",");
        if ((CantTotalGrilla - intDocAnulados) == (lblCheckedResult.length - 1)) {
            return false;
        } else {
            return true;
        }
    } else {
        return false;
    }
}


//****************************************************************
// Funcion		:: 	fn_TotalChekeadosActivos
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_TotalChekeadosActivos() {
    //debugger;
    var CantTotalGrilla = $("#jqGrid_lista_C").getGridParam("reccount");
    var lblCheckedResult = $("#hidDesembolso").val().split(",");
    if ((CantTotalGrilla - intDocBloqueados) == (lblCheckedResult.length - 1)) {
        return false;
    } else {
        return true;
    }
}


//****************************************************************
// Funcion		:: 	fn_TodoChekeadosActivos
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_TodoChekeadosActivos() {
    //debugger;
    var CantTotalGrilla = $("#jqGrid_lista_C").getGridParam("reccount");
    var lblCheckedResult = $("#hidDesembolso").val().split(",");
    if (CantTotalGrilla == intDocBloqueados) {
        return true;
    } else {
        return false;
    }
}



//****************************************************************
// Funcion		:: 	fn_RedireccionGrabar
//****************************************************************
function fn_RedireccionGrabar() {
    fn_util_redirect('frmDesembolsoListado.aspx');
}


//****************************************************************
// Funcion		:: 	fn_TipoCambioSunat
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_TipoCambioSunat(pstrModalidad) {

    if ($('#chkDetraccion').is(':checked') == true) {
        if ($("#txtFechaEmision").val() != "") {
            arrParametros = ["pstrCodMoneda", $("#hidCodMoneda").val(),
                              "pstrFecha", Fn_util_DateToString($("#txtFechaEmision").val()),
                              "pstrModalidad", pstrModalidad];

            fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/ConsultarTipoCambio",
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

        fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/ConsultarTipoCambio",
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
// Funcion		:: 	fnObtenerTipoCambioSunat
// Descripción	::	
// Log			:: 	
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
// Funcion		:: 	fn_TipoCambioDia
// Descripción	::	
// Log			:: 	
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
// Log			:: 	
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
// Log			:: 	
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
// Funcion		:: 	fn_listaEjecutivoLeasing
// Descripción	::	Lista Ejecutivos
// Log			:: 	JRC - 18/07/2012
//****************************************************************
function fn_ValidaBloqueo() {

    var strBloqueoExistente = $("#hddBloqueoExistente").val();
    var strBloqueoCodigo = $("#hddBloqueoCodigo").val();
    var strBloqueoCodUsuario = $("#hddBloqueoCodUsuario").val();
    var strBloqueoNomUsuario = $("#hddBloqueoNomUsuario").val();
    var strBloqueoFecha = $("#hddBloqueoFecha").val();

    if (fn_util_trim(strBloqueoExistente) == "1") {

        parent.fn_mdl_confirmaBloqueo(
                        "El Desembolso está siendo modificado por el usuario (" + strBloqueoCodUsuario + ") " + strBloqueoNomUsuario + " desde la fecha " + strBloqueoFecha + " ¿Desea continuar con la modificación?"
                        , function() { fn_ActualizaBloqueo(strBloqueoCodigo) }
                        , "Util/images/img_bloqueo.gif"
                        , function() { fn_util_redirect('frmDesembolsoListado.aspx'); }
                        , null
        );

    }

}

//****************************************************************
// Funcion		:: 	fn_ActualizaBloqueo
// Descripción	::	Actualiza Bloqueo
// Log			:: 	JRC - 18/07/2012
//****************************************************************
function fn_ActualizaBloqueo(pstrBloqueoCodigo) {

    //Consulta Ultimus
    var arrParametros = ["pstrCodBloqueo", pstrBloqueoCodigo];
    fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/ActualizaBloqueo",
             arrParametros,
             function(result) {
                 parent.fn_unBlockUI();
                 $('#cmbEjecutivoLeasing').html(result);
             },
             function(resultado) {
                 parent.fn_unBlockUI();
                 var error = eval("(" + resultado.responseText + ")");
                 parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN ULTIMUS");
             }
    );

}

//****************************************************************
// Funcion		:: 	fn_cancelar
// Descripción	::	cancelar
// Log			:: 	WCR - 18/06/2012
//****************************************************************
function fn_cancelar() {
    parent.fn_mdl_confirma('¿Está seguro de Volver?',
		function() {
		    parent.fn_blockUI();
		    fn_util_redirect('frmDesembolsoListado.aspx');
		},
         "Util/images/question.gif",

		function() {
		},
		'Mantenimiento Bien'
	);
}
//****************************************************************
// Funcion		:: 	fn_ventanaDocumento
// Descripción	::	abre ventana para asociar un documento
// Log			:: 	WCR - 10/08/2012
//****************************************************************
function fn_ventanaDocumento() {

    if ($("#txtNroDocProveed").val() != "") {
        var strURL = 'Desembolso/frmListaDocumentoDesembolso.aspx?';
        strURL = strURL + 'cc=' + $("#txtNumeroContrato").val();
        strURL = strURL + '&td=' + $("#txtNumeroTipo2").val();

        if ($("#txtNumeroTipo").val() == strComprobante.CodigoDua) {
            strURL = strURL + '&nd=' + $("#txtNumeroComprobante").val();
        } else {
            strURL = strURL + '&nd=' + $("#txtNroDoc2").val();
        }
        strURL = strURL + '&fe=' + Fn_util_DateToString($("#txtFechaEmision").val());
        strURL = strURL + '&cp=' + $("#hidCodProveedor").val();
        strURL = strURL + '&cu=' + $("#txtcu").val();


        parent.fn_util_AbreModal("Desembolso :: Búsqueda de Documentos", strURL, 950, 380, function() { });
    } else {
        parent.fn_mdl_mensajeIco("Debe ingresar Nº Documento", "util/images/warning.gif", "DESEMBOLSO");
    }

}

//****************************************************************
// Funcion		:: 	fn_obtenerDatosDocumento
// Descripción	::	obtiene los datos de un documento para asociar
// Log			:: 	WCR - 10/08/2012
//****************************************************************
function fn_obtenerDatosDocumento(pTipoDoc, pNroSerie, pNroDoc, pFecEmi, pCodSolicitudCredito) {
    $('#txtSerieDoc2').val(pNroSerie);
    $('#hidTipoComNDNC').val(pTipoDoc);
    if (pNroDoc == null) { pNroDoc = ''; }
    $('#txtNroDoc2').val(pNroDoc.substring(4, pNroDoc.length));
    var arrFecha = pFecEmi.split(' ');
    $('#txtFechaAdm').val(arrFecha[0]);
    $('#hddCodSolicitudCredito').val(pCodSolicitudCredito);

}


//****************************************************************
// Funcion		:: 	fn_recalculaIGV
// Descripción	::	Recalcular IGV de los documentos
// Log			:: 	JRC - 18/09/2012
//****************************************************************
function fn_recalculaIGV() {

    parent.fn_blockUI();

    var arrParametros = ["pstrNumeroContrato", $("#txtNumeroContrato").val()];
    fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/RecalculaIGVDocumentos",
             arrParametros,
             function(result) {
                 parent.fn_unBlockUI();
                 if (fn_util_trim(result) != "0") {
                     parent.fn_mdl_mensajeIco("No se pudo actualizar los IGV de los Documentos.", "util/images/error.gif", "ERROR");
                 } else {
                     intDocBloqueados = 0;
                     fn_ListarContratoEstructuraDoc();
                     $('#dv_GenerarWIO').show();
                     $("#hidDesembolso").val("");
                     $("#hidDesembolso1").val("");
                 }
             },
             function(resultado) {
                 parent.fn_unBlockUI();
                 var error = eval("(" + resultado.responseText + ")");
                 parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR");
             }
    );

}


//****************************************************************
// Funcion		:: 	fn_validaCheckRetencion
// Descripción	::	Valida el check en Retencion
// Log			:: 	JRC - 18/09/2012
//****************************************************************
function fn_validaCheckRetencion() {

    if ($('#txtNumeroTipo').val() == strComprobante.CodigoNotaDebito || $('#txtNumeroTipo').val() == strComprobante.CodigoNotaCredito || $('#txtNumeroTipo').val() == strComprobante.Factura) {
        fnHabiltarDetranReten(true);
        $("#txtTipoBien").val(strCodigoRetencion); //Retencion 6%
        fn_TipoBien();
        $("#txtTipoBien").attr("disabled", true);
        $('#imgTipoBien').attr('disabled', 'disabled');
    }
    else if ($('#txtNumeroTipo').val() == strComprobante.CodigoNoDomiciliado || $('#txtNumeroTipo').val() == strComprobante.CodigoReciboHonorarioNoDomiciliado) {
        //debugger;
        fnHabiltarDetranReten(true);

        $("#txtTipoBien").attr("enable", true);
        $("#txtTipoBien").attr("enable", "enable");
        $('#imgTipoBien').attr('enable', true);
        $('#imgTipoBien').attr('enable', 'enable');
    }
    //IBK-JJM
    else if ($('#txtNumeroTipo').val() == strComprobante.CodigoNotaCreditoNoDomiciliado || $('#txtNumeroTipo').val() == strComprobante.CodigoNotaDebidoNoDomiciliado) {
        //debugger;
        fnHabiltarDetranReten(true);

        $("#txtTipoBien").attr("enable", true);
        $("#txtTipoBien").attr("enable", "enable");
        $('#imgTipoBien').attr('enable', true);
        $('#imgTipoBien').attr('enable', 'enable');

    }
    else {
        fnHabiltarDetranReten(false);
    }

}


//****************************************************************
// Funcion		:: 	fn_inicializaChecks
// Descripción	::	Inicializa Checks
// Log			:: 	JRC - 18/09/2012
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
// Log			:: 	JRC - 18/09/2012
//****************************************************************
function fn_activaRetencionAutomatica() {
    //debugger;
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
        if (strRetencion != "1" && decMontoIGV > 0) {
            //if (strRetencion != "1" && decMontoIGV > 0 && decTotal > 700) {
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
// Funcion		:: 	fn_activaRetencionAutomatica2
// Descripción	::	Inicializa Retencion Automatica
// Log			:: 	IBK JJM - 22/11/2012
//****************************************************************
function fn_activaRetencionAutomatica2() {
    //debugger;
    decMontoIGV = fn_util_ValidaDecimal($("#txtNumeroIGV").val());
    decTotal = fn_util_ValidaDecimal($("#txtTotal").val());
    strMoneda = $("#cmbMoneda").val();
    decTC = fn_util_ValidaDecimal($("#txttcdia").val());
    //    $('#ChkRetencion').attr('checked', 'checked');
    //    $('#chkDetraccion').attr('checked', 'checked');
    //    $('#chkNinguno').attr('checked', 'checked');
    //Valida si Moneda es Dólares
    if (fn_util_trim(strMoneda) == "002") {
        decTotal = decTotal * decTC;
    }
    //alert(strRetencionProveedor);
    if ($("#chkDetraccion").is(':checked') == false) {
        if ($("#ChkRetencion").prop("disabled") == false) {
            //Inicio IBK - AAE
            //if (strRetencionProveedor == "1" && decMontoIGV > 0) {
            if (strRetencionProveedor == "0" && decMontoIGV > 0) {
            // Fin IBK
                //if (strRetencionProveedor = "1" && decMontoIGV > 0 && decTotal > 700) {
                $('#ChkRetencion').attr('checked', true);
                $('#chkDetraccion').attr('checked', false);
                $('#chkNinguno').attr('checked', false);
                $('#ChkRetencion').attr('checked', 'checked');
                fn_validaCheckRetencion();
            } else {
                $('#ChkRetencion').attr('checked', false);
                $('#chkDetraccion').attr('checked', false);
                $('#chkNinguno').attr('checked', true);
                $("#ChkRetencion").prop("enabled", false);

                $('#chkNinguno').attr('checked', 'checked');
                fnHabiltarDetranReten(false);
            }
            //Inicio IBK - AAE - Se ocmenta if porque asigna retenció na TODOS las facturas, sin chequear            
            //if ($('#txtNumeroTipo').val() == strComprobante.Factura || $('#txtNumeroTipo').val() == strComprobante.CodigoNotaCredito) {
            if (($('#txtNumeroTipo').val() == strComprobante.Factura || $('#txtNumeroTipo').val() == strComprobante.CodigoNotaCredito) && (strRetencionProveedor == "0")) {
                //Fin IBK -	
                $('#ChkRetencion').attr('checked', true);
                $('#chkDetraccion').attr('checked', false);
                $('#chkNinguno').attr('checked', false);
                $('#ChkRetencion').attr('checked', 'checked');
                fn_validaCheckRetencion();
            }
            //            if ($('#txtNumeroTipo').val() == strComprobante.CodigoNoDomiciliado || $('#txtNumeroTipo').val() == strComprobante.CodigoNotaCredito) {
            //                $('#ChkRetencion').attr('checked', true);
            //                $('#chkDetraccion').attr('checked', false);
            //                $('#chkNinguno').attr('checked', false);
            //                $('#ChkRetencion').attr('checked', 'checked');
            //                fn_validaCheckRetencion();
            //            }
        }
        else {
            $('#ChkRetencion').attr('checked', false);
            $('#chkDetraccion').attr('checked', false);
            $('#chkNinguno').attr('checked', true);
            $("#ChkRetencion").prop("enabled", false);

            $('#chkNinguno').attr('checked', 'checked');
            fnHabiltarDetranReten(false);
        }
    }
}
//****************************************************************
// Funcion		:: 	fn_calculaRenta4ta
// Descripción	::	Renta 4ta
// Log			:: 	JRC - 18/09/2012
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
// Funcion		:: 	fn_ValorRetencion
// Descripción	::	
// Log			:: 	
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
// Funcion		:: 	fn_desactivaComprobanteAdd
// Descripción	::	
//****************************************************************
function fn_desactivaComprobanteAdd() {
    $('#trComprobante2').css("display", "none");

    $('#txtNumeroTipo2').val("");
    $('#txtSerieDoc2').val("");
    $('#txtNroDoc2').val("");
    $('#txtFechaAdm').val("");

}

// Inicio IBK
//****************************************************************
// Funcion		:: 	fn_cargarAsociaciones
// Descripción	::	Cargo la info de la página
// Log			:: 	AAE - 24/09/2012
//****************************************************************
function fn_cargarAsociaciones(event, ui) {
    //veo quien me llamó
    //si es asociación cargo la info
    if (ui.index == 1) {
        //logica de lazyload, si nunca lo cargué lo
        if ($("#hidLazyLoadTab").val() != "1") {

            /*var d = new Date();
            var t = d.getTime();
            */
            //$("#txtTest").val("Hola mundo!!");
            // $("#jqGrid_lista_A").GridUnload();
            // $("#jqGrid_lista_B").GridUnload();
            // $("#jqGrid_lista_D").GridUnload();
            fn_cargaGrillaBienes();

            /*var dd = new Date();
            var t1 = dd.getTime();*/

            fn_cargaGrillaDocumentos();

            /*var ddd = new Date();
            var t2 = ddd.getTime();*/
            fn_cargaGrillaRelaciones();

            /*var dddd = new Date();
            var t3 = dddd.getTime();

            alert(t + " " + t1 + " " + t2 + " " + t3);*/
            $("#hidLazyLoadTab").val("1");
        };
    };
}

//****************************************************************
// Funcion		:: 	fn_cargaGrillaBienes
// Descripción	::	Carga Grilla Bienes
// Log			:: 	AAE - 24/09/2012
//****************************************************************
function fn_cargaGrillaBienes() {
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            fn_ListagrillaBienes();
        },
        jsonReader:                      //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            {
            root: "Items",
            page: "CurrentPage",         // Número de página actual.
            total: "PageCount",          // Número total de páginas.
            records: "RecordCount",      // Total de registros a mostrar.
            repeatitems: false,
            id: "CodSolicitudCredito,SecFinanciamiento"    // Índice de la columna con la clave primaria.
        },
        colNames: ['Contrato', 'Id Bien', 'Cantidad', 'Descripción', 'Uso', 'Ubicación', 'Descripción Bien', 'Departamento', 'Provincia', 'Distrito', 'Marca', 'Modelo', 'Año', 'Valor', 'Fecha'],
        colModel: [
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
            { name: 'SecFinanciamiento', index: 'SecFinanciamiento', width: 40, sorttype: "string", align: "left" },
            { name: 'Cantidad', index: 'Cantidad', hidden: true },
            { name: 'Descripcion', index: 'Descripcion', hidden: true },
            { name: 'Uso', index: 'Uso', hidden: true },
            { name: 'Ubicacion', index: 'Ubicacion', hidden: true },
            { name: 'DescBien', index: 'DescBien', align: 'left', width: 300, sorttype: "string" },
            { name: 'Departamento', index: 'Departamento', hidden: true },
            { name: 'Provincia', index: 'Provincia', hidden: true },
            { name: 'Distrito', index: 'Distrito', hidden: true },
            { name: 'Marca', index: 'Marca', hidden: true },
            { name: 'Modelo', index: 'Modelo', hidden: true },
            { name: 'Anio', index: 'Anio', hidden: true },
            { name: 'ValorBien', index: 'ValorBien', align: 'center', width: 50, formatter: Fn_util_ReturnValidDecimal2 },
            { name: 'FechaRegistro', index: 'FechaRegistro', hidden: true }
        ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,                         // Tamaño de la página
        rowList: [10, 20, 30, 50, 100, 200, 500, 1000],
        sortname: 'SecFinanciamiento',  // Columna a ordenar por defecto.
        sortorder: 'asc',                  // Criterio de ordenación por defecto.
        viewrecords: false,                 // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hidIdBien").val(rowData.SecFinanciamiento);
            fn_CargarRelaciones();
            /*var lista = jQuery("#jqGrid_lista_B").getDataIDs();
            for (i = 0; i < lista.length; i++) {
            $("#jqGrid_lista_B").setCell(lista[i], "ChkSelect", "1", "");
            };*/
        }
    });

    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

};


//****************************************************************
// Funcion		:: 	fn_cargaGrillaDocumentos
// Descripción	::	Carga Grilla Documentos
// Log			:: 	AAE - 24/09/2012
//****************************************************************
function fn_cargaGrillaDocumentos() {
    $("#jqGrid_lista_B").jqGrid({
        datatype: function() {
            fn_ListagrillaDocumentos();
        },
        jsonReader:                      //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            {
            root: "Items",
            page: "CurrentPage",         // Número de página actual.
            total: "PageCount",          // Número total de páginas.
            records: "RecordCount",      // Total de registros a mostrar.
            repeatitems: false,
            id: "CodSolicitudCredito,TipoDocumento,NroDocumento, CodProveedor, FechaEmision"    // Índice de la columna con la clave primaria.
        },
        colNames: ['Seleccionar', 'Contrato', 'TipoDoc', 'NroDocumento', 'CodProveedor', 'FechaEmision', 'Tipo Doc', 'Número Doc', 'Proveedor', 'Fecha Emisión', 'Moneda', 'Monto Original', 'T/C', 'Monto Contrato'],
        colModel: [
            { name: 'ChkSelect', index: 'ChkSelect', align: "center", width: 70, editable: true, formatter: 'checkbox', edittype: 'checkbox', stype: 'select', editoptions: { value: "1:0" }, formatoptions: { disabled: false} },
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
            { name: 'TipoDocumento', index: 'TipoDocumento', hidden: true },
            { name: 'NroDocumento', index: 'NroDocumento', hidden: true },
            { name: 'CodProveedor', index: 'CodProveedor', hidden: true },
            { name: 'FechaEmision', index: 'FechaEmision', hidden: true },
            { name: 'TipoDoc', index: 'TipoDoc', align: 'center', width: 50, sorttype: "string" },
            { name: 'NroDoc', index: 'NroDoc', align: 'center', width: 80, sorttype: "string" },
            { name: 'Proveedor', index: 'Proveedor', align: 'left', width: 180, sorttype: "string" },
            { name: 'FEmision', index: 'FEmision', align: 'center', width: 60, sorttype: "string" },
            { name: 'Moneda', index: 'Moneda', align: 'center', width: 50, sorttype: "string" },
            { name: 'MontoOrig', index: 'MontoOrig', align: 'right', width: 60, formatter: Fn_util_ReturnValidDecimal2 },
            { name: 'TC', index: 'TC', align: 'right', width: 50 },
            { name: 'MontoCont', index: 'MontoCont', align: 'right', width: 70, formatter: Fn_util_ReturnValidDecimal2 }

        ],
        height: '100%',
        pager: '#jqGrid_pager_B',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,                         // Tamaño de la página
        rowList: [10, 20, 30, 50, 100, 200, 500, 1000],
        sortname: 'FechaEmision',  // Columna a ordenar por defecto.
        sortorder: 'desc',                  // Criterio de ordenación por defecto.
        viewrecords: false,                 // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass'
    });

    jQuery("#jqGrid_lista_B").jqGrid('navGrid', '#jqGrid_pager_B', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_B").hide();



};

//****************************************************************
// Funcion		:: 	fn_cargaGrillaRelaciones
// Descripción	::	Carga Grilla Relaciones
// Log			:: 	AAE - 24/09/2012
//****************************************************************
function fn_cargaGrillaRelaciones() {
    $("#jqGrid_lista_D").jqGrid({
        datatype: function() {
            fn_ListagrillaRelaciones();
        },
        jsonReader:                      //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            {
            root: "Items",
            page: "CurrentPage",         // Número de página actual.
            total: "PageCount",          // Número total de páginas.
            records: "RecordCount",      // Total de registros a mostrar.
            repeatitems: false,
            id: "CodSolicitudCredito, SecFinanciamiento, TipoDocumento, NroDocumento, CodProveedor, FechaEmision"    // Índice de la columna con la clave primaria.
        },
        colNames: ['Contrato', 'Id Bien', 'TipoDoc', 'NroDocumento', 'CodProveedor', 'FechaEmision', 'Descripción Bien', 'Descripción Documento', 'Monto Doc.', 'Valor Bien'],
        colModel: [
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
            { name: 'SecFinanciamiento', index: 'SecFinanciamiento', width: 50 },
            { name: 'TipoDocumento', index: 'TipoDocumento', hidden: true },
            { name: 'NroDocumento', index: 'NroDocumento', hidden: true },
            { name: 'CodProveedor', index: 'CodProveedor', hidden: true },
            { name: 'FechaEmision', index: 'FechaEmision', hidden: true },
            { name: 'DescBien', index: 'DescBien', width: 400, sorttype: "string", align: "left" },
            { name: 'DescDoc', index: 'DescDoc', width: 400, sorttype: "string", align: "left" },
            { name: 'MontoDoc', index: 'MontoDoc', align: 'right', width: 120, formatter: Fn_util_ReturnValidDecimal2 },
            { name: 'ValorBien', index: 'ValorBien', align: 'right', width: 100, formatter: Fn_util_ReturnValidDecimal2 }

        ],
        height: '100%',
        pager: '#jqGrid_pager_D',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,                         // Tamaño de la página
        rowList: [10, 20, 30, 50, 100, 200, 500, 1000],
        sortname: 'SecFinanciamiento',  // Columna a ordenar por defecto.
        sortorder: 'asc',                  // Criterio de ordenación por defecto.
        viewrecords: false,                 // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
        },
        ondblClickRow: function(id) {
        }
    });

    jQuery("#jqGrid_lista_D").jqGrid('navGrid', '#jqGrid_pager_D', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_D").hide();

};


//****************************************************************
// Funcion		:: 	fn_ListagrillaBienes
// Descripción	::	Lista la informacíón de bienes
// Log			:: 	AAE - 24/09/2012
//****************************************************************
function fn_ListagrillaBienes() {
    var strContrato = $("#txtNumeroContrato").val();
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),      // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),        // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"),    // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"),   // Criterio de ordenación
                         "pNroContrato", strContrato
                        ];
    fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/ListaBienesContrato",
                            arrParametros,
                            function(jsondata) {
                                jqGrid_lista_A.addJSONData(jsondata);
                                fn_doResize();
                            },
                            function(request) {
                                fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                            }
                           );

};

//****************************************************************
// Funcion		:: 	fn_ListagrillaDocumentos
// Descripción	::	Lista la informacíón de bienes
// Log			:: 	AAE - 24/09/2012
//****************************************************************
function fn_ListagrillaDocumentos() {
    var strContrato = $("#txtNumeroContrato").val();
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_B", "rowNum"),      // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_B", "page"),        // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_B", "sortname"),    // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_B", "sortorder"),   // Criterio de ordenación
                         "pNroContrato", strContrato
                        ];
    fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/ListaDocumentosContrato",
                            arrParametros,
                            function(jsondata) {
                                jqGrid_lista_B.addJSONData(jsondata);
                                fn_doResize();
                            },
                            function(request) {
                                fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                            }
                           );

};
//****************************************************************
// Funcion		:: 	fn_ListagrillaDocumentos
// Descripción	::	Lista la informacíón de bienes
// Log			:: 	AAE - 24/09/2012
//****************************************************************
function fn_ListagrillaRelaciones() {
    var strContrato = $("#txtNumeroContrato").val();
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_D", "rowNum"),      // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_D", "page"),        // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_D", "sortname"),    // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_D", "sortorder"),   // Criterio de ordenación
                         "pNroContrato", strContrato
                        ];
    fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/ListaRelacionesContrato",
                            arrParametros,
                            function(jsondata) {
                                jqGrid_lista_D.addJSONData(jsondata);
                                fn_doResize();
                            },
                            function(request) {
                                fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                            }
                           );

};

//****************************************************************
// Funcion		:: 	fn_SeleccionarTodo
// Descripción	::	Marca todos los checks de la selección de documentos
// Log			:: 	AAE - 24/09/2012
//****************************************************************
function fn_SeleccionarTodo() {
    var lista = jQuery("#jqGrid_lista_B").getDataIDs();
    for (i = 0; i < lista.length; i++) {
        $("#jqGrid_lista_B").setCell(lista[i], "ChkSelect", "1", "");
    };
};

//****************************************************************
// Funcion		:: 	fn_DeSeleccionarTodo
// Descripción	::	Desmarca todos los checks de la selección de documentos
// Log			:: 	AAE - 24/09/2012
//****************************************************************
function fn_DeSeleccionarTodo() {
    var lista = jQuery("#jqGrid_lista_B").getDataIDs();
    for (i = 0; i < lista.length; i++) {
        $("#jqGrid_lista_B").setCell(lista[i], "ChkSelect", "0", "");
    };
};

//****************************************************************
// Funcion		:: 	fn_CargarRelaciones
// Descripción	::	Carga las relaciones en la grilla de documentos
// Log			:: 	AAE - 24/09/2012
//****************************************************************
function fn_CargarRelaciones() {

    //Obtengo las filas de ambas grillas
    var listaDocs = jQuery("#jqGrid_lista_B").getDataIDs();
    var listaRels = jQuery("#jqGrid_lista_D").getDataIDs();
    var rel, tipoDoc, nroDoc, codProv, fEmision;
    var tipoDoc2, nroDoc2, codProv2, fEmision2;

    //borro las marcas
    for (j = 0; j < listaDocs.length; j++) {
        $("#jqGrid_lista_B").setCell(listaDocs[j], "ChkSelect", "0", "");
    };

    //Recorro las relaciones
    for (i = 0; i < listaRels.length; i++) {

        rel = jQuery('#jqGrid_lista_D').jqGrid('getCell', listaRels[i], 'SecFinanciamiento');
        tipoDoc = jQuery('#jqGrid_lista_D').jqGrid('getCell', listaRels[i], 'TipoDocumento');
        nroDoc = jQuery('#jqGrid_lista_D').jqGrid('getCell', listaRels[i], 'NroDocumento');
        codProv = jQuery('#jqGrid_lista_D').jqGrid('getCell', listaRels[i], 'CodProveedor');
        fEmision = jQuery('#jqGrid_lista_D').jqGrid('getCell', listaRels[i], 'FechaEmision');
        //para cada relación igual al bien
        if ($("#hidIdBien").val() == rel) {
            //recorro los documentos buscando el que debo activar
            for (j = 0; j < listaDocs.length; j++) {
                tipoDoc2 = jQuery('#jqGrid_lista_B').jqGrid('getCell', listaDocs[j], 'TipoDocumento');
                nroDoc2 = jQuery('#jqGrid_lista_B').jqGrid('getCell', listaDocs[j], 'NroDocumento');
                codProv2 = jQuery('#jqGrid_lista_B').jqGrid('getCell', listaDocs[j], 'CodProveedor');
                fEmision2 = jQuery('#jqGrid_lista_B').jqGrid('getCell', listaDocs[j], 'FechaEmision');
                //alert(    tipoDoc +":" + tipoDoc2 + " - "+ nroDoc +":" + nroDoc2 + " - "+ codProv+":" + codProv2 + "-" + fEmision+":" + fEmision2);
                //cuando lo encuentro lo activo y salgo del loop
                if (($.trim(tipoDoc) == $.trim(tipoDoc2)) && ($.trim(nroDoc2) == $.trim(nroDoc)) && ($.trim(codProv2) == $.trim(codProv)) && ($.trim(fEmision2) == $.trim(fEmision))) {
                    //alert("Entro!");
                    $("#jqGrid_lista_B").setCell(listaDocs[j], "ChkSelect", "1", "");
                    break;
                };
            };
        };
    };
};

//****************************************************************
// Funcion		:: 	fn_AgregarRelacion
// Descripción	::	Agrega las relaciones
// Log			:: 	AAE - 24/09/2012
//****************************************************************
function fn_AgregarRelacion() {
    //controlo que tenga seleccionado un bien y al menos un documento
    //String Validación
    var strError = new StringBuilderEx();
    var strEncontre = "0";
    var listaDocs = jQuery("#jqGrid_lista_B").getDataIDs();
    var i = 0;
    var strDocs = "";
    var tipoDoc, nroDoc, codProv, fEmision;
    if ($("#hidIdBien").val() == "0" || $("#hidIdBien").val() == "") {
        strError.append("Debe seleccionar un bien. ");
        strError.append("\r");
    };

    //chequeo que tenga seleccionado al menos un doc, siempre debe tener al menos una relación, así que nunca pueden eliminar TODAS
    while (i < listaDocs.length && strEncontre == "0") {
        if (jQuery('#jqGrid_lista_B').jqGrid('getCell', listaDocs[i], 'ChkSelect') == "1") {
            strEncontre = "1";
        };
        i++;
    };
    if (strEncontre == "0") {
        strError.append("Debe seleccionar al menos un documento.");
    };
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    } else {
        //obtengo los documentos seleccionados
        for (i = 0; i < listaDocs.length; i++) {
            if (jQuery('#jqGrid_lista_B').jqGrid('getCell', listaDocs[i], 'ChkSelect') == "1") {
                tipoDoc = jQuery('#jqGrid_lista_B').jqGrid('getCell', listaDocs[i], 'TipoDocumento');
                nroDoc = jQuery('#jqGrid_lista_B').jqGrid('getCell', listaDocs[i], 'NroDocumento');
                codProv = jQuery('#jqGrid_lista_B').jqGrid('getCell', listaDocs[i], 'CodProveedor');
                fEmision = jQuery('#jqGrid_lista_B').jqGrid('getCell', listaDocs[i], 'FechaEmision');
                if (strDocs == "") {
                    strDocs = tipoDoc + ";" + nroDoc + ";" + codProv + ";" + fEmision;
                } else {
                    strDocs = strDocs + "|" + tipoDoc + ";" + nroDoc + ";" + codProv + ";" + fEmision;
                }; //if (strDocs == "")
            }; //if (jQuery('#jqGrid_lista_B')
        }; //for      
        parent.fn_blockUI();
        var arrParametros = ["strNroContrato", $("#txtNumeroContrato").val(),
                             "strSecBien", $("#hidIdBien").val(),
                             "strArrayDocs", strDocs
        ];
        fn_util_AjaxSyncWM("frmDesembolsoRegistro.aspx/AgregarRelacion",
                            arrParametros,
                            function(resultado) {
                                parent.fn_unBlockUI();
                                $("#jqGrid_lista_D").GridUnload();
                                fn_cargaGrillaRelaciones();
                            },
                        function(resultado) {
                            parent.fn_unBlockUI();
                            var error = eval("(" + resultado.responseText + ")");
                            parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR Al GUARDAR");
                        }
            );
        $("#hidIdBien").val("0");
    };
}

//****************************************************************
// Funcion		:: 	fn_valFecha
// Descripción	::	Se agrego funcion para darle formato a la fecha
// Log			:: 	RPH - 25/09/2012
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

// FIN IBK