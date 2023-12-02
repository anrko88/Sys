//Variables Globales
var strMontoTemp = "0";
var strEstadoCotizacionIngresado = "001";
var strEstadoCotizacionPendCarta = "002"
var strEstadoCotizacion_EvaSuperv = "003";
var strEstadoCotizacion_EvaCliente = "004";
var strEstadoCotizacion_PendF10 = "007";
var strTipoContratoLeasing = "LD";
var strTipoContratoLeaseback = "LB";
var strSubTipoContrato = "001";
var strTipoCronogramaCuotaConst = "003";
var strPlazoGracia = "0";
var strClasifacionBienInmueble = "002";
var strProcedenciaLocal = "002";
var strProcedenciaImportacion = "001";
var strSeguroBienTipoInterno = "001";
var strSeguroBienTipoExterno = "002";
var strSeguroDegravamenTipoInterno = "001";
var strSeguroDegravamenTipoExterno = "002";
var strTipoPersonaNatural = "1";
var strTipoPersonaJuridica = "2";
var strEstadoBienNuevo = "001";
var strEstadoBienUsado = "002";
var strTipoDocumentoRuc = "2";
var strPorcIGVInmueble = "9";
var strPeriodicidadMensual = "MEN";
var strSubTipoContratoTotal = "001";
var strSubTipoContratoParcial = "002";
var strTipoDocumentoDNI = "1";
var strTipoDocumentoRUC = "2";
var strTipoDocumentoCarnetEx = "3";
var strTipoDocumentoPasaporte = "5";
var strTipoDocumentoOtroDoc = "6";
var strTipoMonedaDolares = "002";
var strTipoBienEmbarcacion = "019";

var strFrecPagoAnual_fija = "ANU"
var strFrecPagoAnual_variable = "AND"
var strFrecPagoBimestral_fija = "BIM"
var strFrecPagoBimestral_variable = "BID"
var strFrecPagoMensual_fija = "MEN"
var strFrecPagoMensual_variable = "MED"
var strFrecPagoSemestral_fija = "SEM"
var strFrecPagoSemestral_variable = "SED"
var strFrecPagoTrimestral_fija = "TRI"
var strFrecPagoTrimestral_variable = "TRD"

var blnDatosCambiados = false;
var strCmbTipoDocumento = "";
var strComboVacio = "<option value='0'>[-Seleccione-]</option>"

//****************************************************************
// Funcion		:: 	JQUERY - COTIZACION REGISTRO
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//   				El documento
// Log			:: 	JRC - 16/04/2012
//****************************************************************
$(document).ready(function() {

    //---------------------------------
    //Setea Calendario
    //---------------------------------    
    fn_util_SeteaCalendario($("#txtFechavence"));
    fn_util_SeteaCalendario($("#txtFechaOfertaValida"));
    fn_util_SeteaCalendarioFunction($("#txtFechaMaxActivacion"), function() { fn_validaFechaActivacion($("#txtFechaMaxActivacion").val()); });

    //Bloqueo de Fechas
    $("#txtFechaIngreso").datepicker("destroy");
    $('#txtFechaIngreso').attr('class', 'css_input_inactivo');
    $('#txtFechaIngreso').prop('readonly', true);

    //---------------------------------
    //Valida Tabs
    //---------------------------------
    $("div#divTabs").tabs({
        show: function(event, ui) {
            fn_doResize();
        }
    });

    //---------------------------------
    //Validacion Inicial
    //---------------------------------+
    fn_inicializaCampos();
    fn_iniGrillaDocumento();
    fn_cargaGrillaCronograma();

    //---------------------------------
    //Validacion Transaccion
    //---------------------------------
    $('#cmbEstado').attr('class', 'css_select_inactivo');
    $('#cmbEstado').attr('disabled', 'disabled');
    $('#txtNumeroCotizacion').prop('readonly', true);
    if ($("#hddCodigoCotizacion").val() == "") {
        $("#dv_OpcMenu").hide();
        fn_inicializaNuevo();
    } else {

        fn_inicializaEditar();
        $("#dv_OpcMenu").show();

        //Documentos        
        fn_cargaGrillaDocumento();
        $("#tab2-tab").css("display", "");

        //Cronograma      
        fn_llenaGrillaCronograma();
        $("#tab3-tab").css("display", "block");
        $("div#divTabs").tabs("enable", [3]);
        //Inicio IBK - AAE
        if ($("#hidPerfil").val() == "1" || $("#hidPerfil").val() == "6") {
            //Valida Botones        
            if (fn_util_trim($('#cmbEstado').val()) == strEstadoCotizacion_EvaSuperv) {
                $("#dvGstSupervisor").show();
            } else {
                $("#dvGstSupervisor").hide();
            }
        } else {
            $("#dvGstSupervisor").hide();
        }
        //Fin IBK

        //Oculta Columnas Cronograma
        //fn_validaColumnasCronograma();

        //Valida datos para Cronograma
        $("#hddIniTipoCronograma").val($("#cmbTipoCronograma").val());
        $("#hddIniNroCuotas").val($("#txtNroCuotas").val());
        $("#hddIniPeriodicidad").val($("#cmbPeriodicidad").val());
        $("#hddIniFrecuenciaPago").val($("#cmbFrecuenciaPago").val());
        $("#hddIniTEA").val($("#txtTEA").val());
        $("#hddIniTipoBienSeguro").val($("#cmbTipoBienSeguro").val());
        $("#hddIniPrecioVenta").val($("#txtPrecioVenta").val());
        $("#hddIniCuotaIni").val($("#txtCuotaInicial").val());
        $("#hddIniCuotaIniPorc").val($("#txtCuotaInicialPorc").val());
        $("#hddIniIGVPorc").val($("#txtPorcIGV").val());
        $("#hddIniTipoGracia").val($("#cmbTipoGracia").val());
        $("#hddIniFecVenc").val($("#txtFechavence").val());
        $("#hddIniFecMaxAct").val($("#txtFechaMaxActivacion").val());
        $("#hddIniImportePrimaSeguroBien").val($("#txtImportePrimaSeguroBien").val());
        $("#hddIniNumCuotasfinanciadas").val($("#txtNumCuotasfinanciadas").val());

        //Valida Supervisor
        $("#hddIniSupTEA").val($("#txtTEA").val());
        $("#hddIniSupPreCuotaPorc").val($("#txtPrecuota").val());
        $("#hddIniSupSpread").val($("#txtSpread").val());
        $("#hddIniComisionActivacionMonto").val($("#txtComisionActivacionMonto").val());

        //Focus
        $("#txtContacto").focus();
        document.getElementById("txtContacto").focus();

        //Oculta Botones
        if (fn_util_trim($("#cmbEstado").val()) == strEstadoCotizacionIngresado) {
            $("#dv_botonEnviar").show();
        }
        else if (fn_util_trim($("#cmbEstado").val()) == strEstadoCotizacionPendCarta) {
            $("#dv_botonGrabar").hide();
        }
        else if (fn_util_trim($("#cmbEstado").val()) == strEstadoCotizacion_EvaCliente) {
            $("#dv_botonGrabar").hide();
            $("#dv_botonEnviar").show();
        }
        else if (fn_util_trim($("#cmbEstado").val()) == strEstadoCotizacion_PendF10) {
            $("#dv_botonGrabar").hide();
            $("#dv_botonEnviar").show();
        }
        else {
            $("#dv_botonEnviar").hide();
            $("#dv_botonGrabar").hide();
            $("#dv_botonGenerar").hide();
            fn_util_bloquearFormulario();
            $("#dv_SeparadorGuardar").hide();
            $("#tb_botonesDocumentos").hide();
        }

    }


    //---------------------------------
    //Valida Retorno
    //---------------------------------
    //Inicio IBK - AAE - Siempre dejo modificar la cotización
    /*
    if ($("#hddFlagRetorno").val() == '1') {

        $('#cmbTipoContrato').attr('class', 'css_select_inactivo');
        $('#cmbTipoContrato').attr('disabled', 'disabled');

        $('#cmbSubTipoContrato').attr('class', 'css_select_inactivo');
        $('#cmbSubTipoContrato').attr('disabled', 'disabled');

        $('#cmbMoneda').attr('class', 'css_select_inactivo');
        $('#cmbMoneda').attr('disabled', 'disabled');

        $('#cmbprocedencia').attr('class', 'css_select_inactivo');
        $('#cmbprocedencia').attr('disabled', 'disabled');

        $('#cmbClasificacionBien').attr('class', 'css_select_inactivo');
        $('#cmbClasificacionBien').attr('disabled', 'disabled');

        $('#cmbTipoBien').attr('class', 'css_select_inactivo');
        $('#cmbTipoBien').attr('disabled', 'disabled');

        $('#cmbEstadoBien').attr('class', 'css_select_inactivo');
        $('#cmbEstadoBien').attr('disabled', 'disabled');

    }*/
    //Fin IBK





    //---------------------------------
    //Valida Bloqueo
    //---------------------------------
    fn_ValidaBloqueo();


    //---------------------------------
    //Valida Tipo Documento
    //---------------------------------
    $('#cmbTipoDocumento').change(function() {
        var strValor = $(this).val();
        $("#txtNumeroDocumento").val("");
        $('#txtNumeroDocumento').unbind('keypress');
        if (fn_util_trim(strValor) == strTipoDocumentoDNI) {
            $('#txtNumeroDocumento').validText({ type: 'number', length: 8 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoRUC) {
            $('#txtNumeroDocumento').validText({ type: 'number', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoCarnetEx) {
            $('#txtNumeroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoPasaporte) {
            $('#txtNumeroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else {
            $('#txtNumeroDocumento').validText({ type: 'alphanumeric', length: 11 });
        }
    });


    //---------------------------------
    //Valida Check Linea
    //---------------------------------
    $('#chkLinea').change(function() {
        fn_seteaCheckLinea($(this).attr('checked'));
    });

    //---------------------------------
    //Consulta Datos RM
    //---------------------------------
    $('#imgBsqClienteRM').click(function() {
        //Inicio IBK
        var strtipobusquedad = "1";
        parent.fn_blockUI();
        //if ($('#txtCUCliente').val() == "") {
        if ($('#txtCUCliente').val() == "" && $('#txtNumeroDocumento').val() == "") {
            parent.fn_unBlockUI();
            //parent.fn_mdl_mensajeIco("Para realizar la búsqueda del Cliente debe ingresar el Código Unico", "util/images/error.gif", "ADVERTENCIA");
            parent.fn_mdl_mensajeIco("Para realizar la búsqueda del Cliente debe ingresar el Código Unico ó Número de Documento", "util/images/error.gif", "ADVERTENCIA");
        } else {

            //Lmpia RM
            $('#txtNombreCliente').val("");
            $('#cmbTipoPersona').val("0");
            $('#cmbTipoDocumento').val("0");
            //Inicio IBK
            //$('#txtNumeroDocumento').val("");
            $('#cmbEjecutivoBanca').val("0");
            $('#cmbEjecutivoBanca').change();
            $('#cmbBancaAtencion').val("0");
            $('#hddCodSuprestatario').val("");
            $('#cmbLinea').val("0");
            $('#cmbLinea').html("<option value='0'>- Seleccionar -</option>");

            $('#txtEjecutivoBanca').val("");
            $('#txtZonal').val("");

            //Inicio IBK
            //Valores
            //var strCodigo = $("#txtCUCliente").val();
            //var paramArray = ["pstrCodUnico", strCodigo];
            var strCodigo = $("#txtCUCliente").val(); //probar
            if (strCodigo == "") {
                //tomara el valor de Numero de Documento
                var strCodigo = $('#txtNumeroDocumento').val();
                strtipobusquedad = "2";
            }
            else {
                if ($('#chkValidaCliente').is(':checked') == false) {
                    var strCodigo = $('#txtNumeroDocumento').val();
                    strtipobusquedad = "2";
                    fn_validaTipoPersonaEditar("2");
                    fn_validaActivacionClienteNuevo(false);
                }

            }
            var paramArray = ["pstrCodUnico", strCodigo, "pstrTipoBusqueda", strtipobusquedad];
            //Fin IBK


            fn_util_AjaxWM("frmCotizacionRegistro.aspx/ConsultaClienteRM",
                   paramArray,
                   fn_PoneDatosClienteRM,
                   function(resultado) {
                       parent.fn_unBlockUI();
                       parent.fn_mdl_mensajeIco("Se produjo un error al cargar los datos de RM", "util/images/error.gif", "ERROR EN CONSULTA RM");
                   }
            );

            //Resize Pantalla
            fn_doResize();
        }

    });

    //---------------------------------
    //Valida Check del CLiente (si existe o no)
    //---------------------------------
    $('#chkValidaCliente').change(function() {
        $("#hddCodUnico").val("");
        $('#fld_SeguroDegravamen').hide();
        //$("#cmbTipoBienSeguro").val(strSeguroDegravamenTipoExterno);
        if ($(this).attr('checked')) {
            //$('#cmbEjecutivoLeasing').html("<option value='0'>[-Seleccione-]</option>");
            $("#hddValidaCliente").val("1");
            fn_validaActivacionClienteNuevo(false);
            $('#cmbTipoDocumento').html(strCmbTipoDocumento);
            $("#hddCodSuprestatario").val("");
            parent.fn_util_MuestraLogPage("El sistema habilitó la búsqueda del Cliente, digite el Código Unico", "I");
        } else {
            //fn_listaEjecutivoLeasing();
            $("#hddValidaCliente").val("0");
            $("#txtCUCliente").val("00000000");
            $("#hddCodUnico").val("00000000");
            $("#hddCodSuprestatario").val("");
            fn_validaActivacionClienteNuevo(true);
            $('#cmbTipoDocumento').html(strComboVacio);
            parent.fn_util_MuestraLogPage("El sistema deshabilitó la búsqueda del Cliente, ingrese los datos del Cliente", "I");
        }
    });

    //-------------------------------------------
    //Valida Change del Tipo Persona
    //-------------------------------------------
    $('#cmbTipoPersona').change(function() {
        var strValor = $(this).val();
        fn_validaTipoPersona(strValor);
    });

    //-------------------------------------------
    //Valida Change Banca Atencion
    //-------------------------------------------
    $('#cmbBancaAtencion').change(function() {
        var strValor = $(this).val();
        fn_oc_bancaAtencion(strValor);
    });

    //-------------------------------------------
    //Valida Change del EjecutivoBanca
    //-------------------------------------------
    $('#cmbEjecutivoBanca').change(function() {
        var strValor = $(this).val();

        //Ejecutivo Leasing
        /*
        var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL146", "pstrCodigoGenerico", strValor];
        var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');
        if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
        $('#cmbEjecutivoLeasing').html(arrResultado[1]);
        } else {
        var strError = arrResultado[1];
        fn_mdl_alert(strError.toString(), function() { });
        }
        }
        

        //Zonal
        var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL311", "pstrCodigoGenerico", strValor];
        var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');
        if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
        $('#cmbZonal').html(arrResultado[1]);
        } else {
        var strError = arrResultado[1];
        fn_mdl_alert(strError.toString(), function() { });
        }
        }
        */

    });




    //---------------------------------
    //Valida Change del TipoContrato
    //---------------------------------
    $('#cmbTipoContrato').change(function() {
        var strValor = $(this).val();
        if (strValor == strTipoContratoLeaseback) {
            /*if ($("#chkLinea").attr('checked')) {
            $('input[name=chkLinea]').attr('checked', false);
            parent.fn_util_MuestraLogPage("No se puede tener linea si el Tipo de Contrato es Leasing Back", "I");
            }*/
            $("#cmbprocedencia").val(strProcedenciaLocal)
        }
        if (strValor == strTipoContratoLeasing) {
            if ($("#cmbSubTipoContrato").val() == strSubTipoContratoTotal) {
                $("#cmbprocedencia").val(strProcedenciaLocal)
            }
        }
    });

    //---------------------------------
    //Valida Change del SubTipoContrato
    //---------------------------------
    $('#cmbSubTipoContrato').change(function() {
        var strValor = $(this).val();
        fn_validaSubTipoContrato(strValor);
    });

    //---------------------------------
    //Valida Change del SubTipoContrato
    //---------------------------------
    $('#cmbMoneda').change(function() {
        var strValor = $(this).val();
        $('#hddComisionActivacion').val("0");

        //Comision Activacion
        if (strValor != "0") {
            fn_validaComisionMoneda(strValor)
        }

        fn_of_PrecioVenta();

    });

    //--------------------------------------
    //Valida Change del Procedencia
    //--------------------------------------
    $('#cmbprocedencia').change(function() {
        var strValor = $(this).val();
        if (strValor == strProcedenciaImportacion) {
            $('#txtPorcIGV').val(fn_util_ValidaMonto($('#hddIGV').val(), 2));
            $('#txtPorcIGV').addClass('css_input').removeClass('css_input_inactivo');
            $('#txtPorcIGV').prop('readonly', false);

            if ($("#cmbTipoContrato").val() == strTipoContratoLeasing) {
                if ($("#cmbSubTipoContrato").val() == strSubTipoContratoTotal && $("#cmbTipoBien").val() != strTipoBienEmbarcacion) {
                    //$("#cmbprocedencia").val("0");
                    $("#cmbprocedencia").val(strProcedenciaLocal);
                    parent.fn_util_MuestraLogPage("La Procedencia no puede ser Importación debido a que es un Leasing Total", "I");
                }
            } else {
                $("#cmbprocedencia").val("0");
                parent.fn_util_MuestraLogPage("La Procedencia no puede ser Importación debido a que es un LeaseBack", "I");
            }

        } else {
            $('#txtPorcIGV').val(fn_util_ValidaMonto($('#hddIGV').val(), 2));
            $('#txtPorcIGV').addClass('css_input_inactivo').removeClass('css_input');
            $('#txtPorcIGV').prop('readonly', true);
        }

        //Valida Clasificacion BienInmueble
        var strClasificacionBien = $("#cmbClasificacionBien").val();
        var strTipoBien = $("#cmbTipoBien").val();
        if (strClasificacionBien == strClasifacionBienInmueble) {
            if (strTipoBienEmbarcacion != strTipoBien) {
                $("#cmbprocedencia").val(strProcedenciaLocal)
            }
        }

        //Recalcula
        fn_of_PrecioVenta();
        fn_of_CuotaInicialPorc();
        fn_of_calculaNeto();
    });

    //-------------------------------------------
    //Valida Change del ClasificacionBien
    //-------------------------------------------
    $('#cmbClasificacionBien').change(function() {
        var strValor = $(this).val();
        fn_oc_clasificacionBien(strValor);
        //Recalcula
        fn_of_PrecioVenta();
        fn_of_CuotaInicialPorc();
        fn_of_calculaNeto();
    });

    //-------------------------------------------
    //Valida Change del Estado Bien
    //-------------------------------------------
    $('#cmbEstadoBien').change(function() {
        var strValor = $(this).val();
        var strClasificacionBien = $('#cmbClasificacionBien').val();
        if (strValor == strEstadoBienNuevo) {
            if (strClasifacionBienInmueble == strClasificacionBien) {
                $('#txtPorcIGV').prop('readonly', false);
                $('#txtPorcIGV').addClass('css_input').removeClass('css_input_inactivo');
                $('#txtPorcIGV').val(fn_util_ValidaMonto(strPorcIGVInmueble));
            } else {
                $('#txtPorcIGV').addClass('css_input_inactivo').removeClass('css_input');
                $('#txtPorcIGV').prop('readonly', true);
                $('#txtPorcIGV').val(fn_util_ValidaMonto($("#hddIGV").val()));
            }
        } else {
            if (strClasifacionBienInmueble == strClasificacionBien) {
                $('#txtPorcIGV').addClass('css_input_inactivo').removeClass('css_input');
                $('#txtPorcIGV').prop('readonly', true);
                $('#txtPorcIGV').val(fn_util_ValidaMonto("0"));
            } else {
                $('#txtPorcIGV').addClass('css_input_inactivo').removeClass('css_input');

                $('#txtPorcIGV').prop('readonly', true);
                $('#txtPorcIGV').val(fn_util_ValidaMonto($("#hddIGV").val()));
            }
        }
        //Recalcula
        fn_of_PrecioVenta();
        fn_of_CuotaInicialPorc();
        fn_of_calculaNeto();
    });



    //--------------------------------------
    //Valida Change del Tipo Bien
    //--------------------------------------
    $('#cmbTipoBien').change(function() {
        var strValor = $(this).val();
        var strClasificacionBien = $("#cmbClasificacionBien").val();

        if (strClasificacionBien == strClasifacionBienInmueble) {
            if (strTipoBienEmbarcacion != strValor) {
                $("#cmbprocedencia").val(strProcedenciaLocal)
            }
        }

    });


    //-------------------------------------------
    //Calcula % IGV
    //-------------------------------------------
    //Inicio IBK - AAE
    $("#txtPorcIGV").focus(function() {
        //alert($("#txtPrecioVenta").val());
        $("#txtPorcIGV").data("value", $("#txtPorcIGV").val())
        var valor = $("#txtPorcIGV").val();
        if (valor > 0) {
            $("#txtPorcIGV").css("color", "blue");
        }
    });
    //Fin IBK
    $("#txtPorcIGV").focusout(function() {
        //Inicio IBK - AAE
        if (fn_util_ValidaDecimal($("#txtPorcIGV").data("value")) != fn_util_ValidaDecimal($("#txtPorcIGV").val())) {
            $("#txtPorcIGV").data("value", $("#txtPorcIGV").val())
            //Fin IBK
            var decIGV = fn_util_ValidaDecimal($("#txtPorcIGV").val());
            if (decIGV > 18) {
                $("#txtPorcIGV").val(fn_util_ValidaMonto(strPorcIGVInmueble, 2));
                parent.fn_util_MuestraLogPage("El IGV no puede ser mayor a 18.00%", "E");
            } else {
                fn_of_PrecioVenta();
                if ($("input[id=rdbTipoCuotaMonto]").attr('checked')) {
                    fn_of_CuotaInicial();
                } else {
                    fn_of_CuotaInicialPorc();
                }
                fn_of_calculaNeto();
            }
        }
        $("#txtPorcIGV").css("color", "#464646");
    });

    //-------------------------------------------
    //Calcula Valor venta
    //-------------------------------------------
    //Inicio IBK - AAE
    $("#txtPrecioVenta").focus(function() {
        //alert($("#txtPrecioVenta").val());
        $("#txtPrecioVenta").data("value", $("#txtPrecioVenta").val())
        var valor = $("#txtPrecioVenta").val();
        //alert(valor.length);
        if (valor > 0) {
            $("#txtPrecioVenta").css("color", "blue");
        }
    });
    //Fin IBK
    $("#txtPrecioVenta").focusout(function() {
        //Inicio IBK - AAE
        if (fn_util_ValidaDecimal($("#txtPrecioVenta").data("value")) != fn_util_ValidaDecimal($("#txtPrecioVenta").val())) {
            $("#txtPrecioVenta").data("value", $("#txtPrecioVenta").val())
            //Fin IBK
            fn_of_PrecioVenta();
            if ($("input[id=rdbTipoCuotaMonto]").attr('checked')) {
                fn_of_CuotaInicial();
            } else {
                fn_of_CuotaInicialPorc();
            }
            fn_of_calculaNeto();
            $('#hddPorcentajeComision').val($('#txtComisionActivacionProc').val());
        }
        //IBK - RPH
        $("#txtPrecioVenta").css("color", "#464646");
    });

    //-------------------------------------------
    //Calcula Valor venta
    //-------------------------------------------
    //Inicio IBK - AAE
    $("#txtCuotaInicial").focus(function() {
        //alert($("#txtPrecioVenta").val());
        $("#txtCuotaInicial").data("value", $("#txtCuotaInicial").val())
        var valor = $("#txtCuotaInicial").val();
        if (valor > 0) {
            $("#txtCuotaInicial").css("color", "blue");
        }


    });
    //Fin IBK
    $("#txtCuotaInicial").focusout(function() {
        //Inicio IBK - AAE
        if (fn_util_ValidaDecimal($("#txtCuotaInicial").data("value")) != fn_util_ValidaDecimal($("#txtPrecioVenta").val())) {
            $("#txtCuotaInicial").data("value", $("#txtCuotaInicial").val())
            // Fin IBK
            fn_of_CuotaInicial();
            fn_of_calculaNeto();
        }
        //IBK - RPH
        $("#txtCuotaInicial").css("color", "#464646");
    });

    //-------------------------------------------
    //Calcula Valor venta
    //-------------------------------------------
    $("#txtCuotaInicialPorc").focusout(function() {
        fn_of_CuotaInicialPorc();
        fn_of_calculaNeto();
        //IBK - RPH
        $("#txtCuotaInicialPorc").css("color", "#464646");
    });

    //Inicio IBK - RPH Si edita lo pone en color Azul
    $("#txtCuotaInicialPorc").focus(function() {
        var valor = $("#txtCuotaInicialPorc").val();
        if (valor > 0) {
            $("#txtCuotaInicialPorc").css("color", "blue");
        }
    });
    //Fin




    //-------------------------------------------
    //Calcula Valor venta
    //-------------------------------------------
    $("#txtFechaMaxActivacion").focusout(function() {
        fn_validaFechaActivacion($(this).val());
    });

    //-------------------------------------------
    //Valida Change del Periodicidad
    //-------------------------------------------
    $('#cmbPeriodicidad').change(function() {
        var strValor = $(this).val();
        fn_oc_periodicidad(strValor);
    });

    //-------------------------------------------
    //Calcula NroCuotas
    //-------------------------------------------
    $("#txtNroCuotas").focusout(function() {
        var strNroCuotas = $("#txtNroCuotas").val();
        if (fn_util_trim(strNroCuotas) == "") strNroCuotas = "0";
        var intNroCuotas = parseInt(strNroCuotas);
        //IBK - RPH se aunmento a 240 cuotas
        if (intNroCuotas <= 0 || intNroCuotas > 240) {
            //if (intNroCuotas <= 0 || intNroCuotas > 120) {
            $("#txtNroCuotas").val("0");
            parent.fn_util_MuestraLogPage("El Nro. de Cuotas ingresado no es correcto", "E");
        } else {
            fn_of_CuotasSeguro();
            fn_of_CuotasSeguroDes();
            fn_of_PlazoGracia();
        }
        //IBK - RPH
        $("#txtNroCuotas").css("color", "#464646");

    });

    //Inicio IBK - RPH Si edita lo pone en color Azul
    $("#txtNroCuotas").focus(function() {
        var valor = $("#txtNroCuotas").val();
        if (valor > 0) {
            $("#txtNroCuotas").css("color", "blue");
        }
    });
    //Fin

    //-------------------------------------------
    //Calcula PlazoGracia
    //-------------------------------------------
    $("#txtPlazoGracia").focusout(function() {
        fn_of_PlazoGracia();
        //IBK - RPH
        $("#txtPlazoGracia").css("color", "#464646");
    });

    //Inicio IBK - RPH Si edita lo pone en color Azul
    $("#txtPlazoGracia").focus(function() {
        var valor = $("#txtPlazoGracia").val();
        if (valor > 0) {
            $("#txtPlazoGracia").css("color", "blue");
        }
    });
    //Fin

    
    //-------------------------------------------
    //Calcula PlazoGraciaPrecuota
    //-------------------------------------------
    $("#txtPlazoGraciaPrecuota").focusout(function() {
        var intNroCuotas = parseInt($("#txtNroCuotas").val());
        var intPlazoGraciaPrecuota = parseInt($("#txtPlazoGraciaPrecuota").val());
        if (intPlazoGraciaPrecuota < 0) {
            $("#txtPlazoGraciaPrecuota").val("0");
            parent.fn_util_MuestraLogPage("El Plazo de Gracia Precuota ingresado no es correcto", "E");
        }
        if (intNroCuotas <= intPlazoGraciaPrecuota) {
            $("#txtPlazoGraciaPrecuota").val("0");
            parent.fn_util_MuestraLogPage("El Plazo de Gracia Precuota ingresado no es correcto", "E");
        }
    });



    //-------------------------------------------
    //Valida % de TEA
    //-------------------------------------------
    $("#txtTEA").focusout(function() {
        var decTEA = fn_util_ValidaDecimal($('#txtTEA').val());
        if (decTEA >= 100) {
            $('#txtTEA').val(fn_util_ValidaMonto("0", 2));
            parent.fn_util_MuestraLogPage("El Porcentaje de la T.E.A. ingresado no es correcto", "E");
        } else {
            var decCostoFondos = fn_util_ValidaDecimal($('#txtCostoFondos').val());
            if (decCostoFondos > decTEA) {
                $('#txtCostoFondos').val(fn_util_ValidaMonto("0", 2));
                parent.fn_util_MuestraLogPage("El Porcentaje de la T.E.A. no puede ser menor a los Costos de Fondos", "E");
            } else {
                $("#txtPrecuota").val($("#txtTEA").val());
                fn_of_calculaSpread();
            }
        }
        //IBK - RPH
        $("#txtTEA").css("color", "#464646");
    });

    //Inicio IBK - RPH Si edita lo pone en color Azul
    $("#txtTEA").focus(function() {
        var valor = $("#txtTEA").val();
        if (valor > 0) {
            $("#txtTEA").css("color", "blue");
        }
    });
    //Fin

    //-------------------------------------------
    //Valida % de Costos Fondos
    //-------------------------------------------
    $("#txtCostoFondos").focusout(function() {
        var decCostoFondos = fn_util_ValidaDecimal($('#txtCostoFondos').val());
        if (decCostoFondos >= 100) {
            $('#txtCostoFondos').val(fn_util_ValidaMonto("0", 2));
            parent.fn_util_MuestraLogPage("El Porcentaje de los Costos Fondos ingresado no es correcto", "E");
        } else {
            var decTEA = fn_util_ValidaDecimal($('#txtTEA').val());
            if (decCostoFondos > decTEA) {
                $('#txtCostoFondos').val(fn_util_ValidaMonto("0", 2));
                parent.fn_util_MuestraLogPage("El Porcentaje de los Costos Fondos no puede ser mayor a la TEA", "E");
            } else {
                fn_of_calculaSpread();
            }
        }
        //IBK - RPH
        $("#txtCostoFondos").css("color", "#464646");
    });

    //Inicio IBK - RPH Si edita lo pone en color Azul
    $("#txtCostoFondos").focus(function() {
        var valor = $("#txtCostoFondos").val();
        if (valor > 0) {
            $("#txtCostoFondos").css("color", "blue");
        }
    });
    //Fin

    //-------------------------------------------
    //Valida % de PreCuota
    //-------------------------------------------
    $("#txtPrecuota").focusout(function() {
        var decPrecuota = fn_util_ValidaDecimal($('#txtPrecuota').val());
        if (decPrecuota >= 100) {
            $("#txtPrecuota").validNumber({ value: "0" });
            parent.fn_util_MuestraLogPage("El Porcentaje de la PreCuota ingresado no es correcto", "E");
        }
    });





    //-------------------------------------------
    //Valida OpcionCompra Porcentaje
    //-------------------------------------------
    $("#txtOpcionCompraPorc").focusout(function() {
        fn_of_OpcionCompraPorc();
        //IBK - RPH
        $("#txtOpcionCompraPorc").css("color", "#464646");
    });

    //Inicio IBK - RPH Si edita lo pone en color Azul
    $("#txtOpcionCompraPorc").focus(function() {
        var valor = $("#txtOpcionCompraPorc").val();
        if (valor > 0) {
            $("#txtOpcionCompraPorc").css("color", "blue");
        }
    });
    //Fin
    
    //-------------------------------------------
    //Valida OpcionCompra Monto
    //-------------------------------------------
    $("#txtOpcionCompraMonto").focusout(function() {
        var decPrecioVenta = fn_util_ValidaDecimal($('#txtPrecioVenta').val());
        var decOpcionCompraMonto = fn_util_ValidaDecimal($('#txtOpcionCompraMonto').val());
        if (decOpcionCompraMonto >= decPrecioVenta) {
            $('#txtOpcionCompraMonto').val(fn_util_ValidaMonto("0", 2));
            parent.fn_util_MuestraLogPage("El monto de la  Opción de Compra ingresada no puede ser mayor al Precio de Venta", "E");
        } else {
            var decCalculo = (decOpcionCompraMonto / decPrecioVenta) * 100;
            $('#txtOpcionCompraPorc').val(fn_util_ValidaMonto(decCalculo, 2));
        }
        //IBK - RPH
        $("#txtOpcionCompraMonto").css("color", "#464646");
    });

    //Inicio IBK - RPH Si edita lo pone en color Azul
    $("#txtOpcionCompraMonto").focus(function() {
        var valor = $("#txtOpcionCompraMonto").val();
        if (valor > 0) {
            $("#txtOpcionCompraMonto").css("color", "blue");
        }
    });
    //Fin
    
    //-------------------------------------------
    //Valida Comision Activacion Porcentaje
    //-------------------------------------------
    // Inicio IBK - AAE
    $("#txtComisionActivacionProc").focus(function() {
        //alert($("#txtPrecioVenta").val());
        $("#txtComisionActivacionProc").data("value", $("#txtComisionActivacionProc").val())
        var valor = $("#txtComisionActivacionProc").val();
        if (valor > 0) {
            $("#txtComisionActivacionProc").css("color", "blue");
        }
    });
    //Fin IBK
    
    $("#txtComisionActivacionProc").focusout(function() {
        //Inicio IBK - AAE
        if (fn_util_ValidaDecimal($("#txtComisionActivacionProc").data("value")) != fn_util_ValidaDecimal($("#txtComisionActivacionProc").val())) {
            $("#txtComisionActivacionProc").data("value", $("#txtComisionActivacionProc").val())
            //Fin IBK
            var decPrecioVenta = fn_util_ValidaDecimal($('#txtPrecioVenta').val());
            var decComisionActivacionProc = fn_util_ValidaDecimal($('#txtComisionActivacionProc').val());
            if (decComisionActivacionProc >= 100) {
                $('#txtComisionActivacionProc').val(fn_util_ValidaMonto("0", 2));
                parent.fn_util_MuestraLogPage("El Porcentaje de la Comisión de Activación ingresado es incorrecto", "E");
            } else {
                var decCalculo = decPrecioVenta * (decComisionActivacionProc / 100);
                $('#txtComisionActivacionMonto').val(fn_util_ValidaMonto(decCalculo, 2));
                //JJM IBK
                /*if ($("#hddPorcentajeComision").val() != '') {
                if ($('#txtComisionActivacionProc').val() != $("#hddPorcentajeComision").val()) {
                $('#txtComisionActivacionMonto').val(fn_util_ValidaMonto(decCalculo, 2));
                $('#hddPorcentajeComision').val($('#txtComisionActivacionProc').val());
                }
                }
                else {

                    $('#txtComisionActivacionMonto').val(fn_util_ValidaMonto(decCalculo, 2));
                }
                //$('#hddComisionActivacion').val(fn_util_ValidaMonto(decCalculo, 2));*/
            }

        }
        //IBK - RPH
        $("#txtComisionActivacionProc").css("color", "#464646");
    });
    //-------------------------------------------
    //Valida Comision Activacion Monto
    //-------------------------------------------
    $("#txtComisionActivacionMonto").focusout(function() {
        fn_of_ComisionActivacionMonto();
        //IBK - RPH
        $("#txtComisionActivacionMonto").css("color", "#464646");
    });

    //Inicio IBK - RPH Si edita lo pone en color Azul
    $("#txtComisionActivacionMonto").focus(function() {
        var valor = $("#txtComisionActivacionMonto").val();
        if (valor > 0) {
            $("#txtComisionActivacionMonto").css("color", "blue");
        }
    });

    //-------------------------------------------
    //Valida OpcionCompra Porcentaje
    //-------------------------------------------
    $("#txtComisionEstructuracionPorc").focusout(function() {
        fn_of_ComisionEstructuracionPorc();
        //IBK - RPH
        $("#txtComisionEstructuracionPorc").css("color", "#464646");
    });

    //Inicio IBK - RPH Si edita lo pone en color Azul
    $("#txtComisionEstructuracionPorc").focus(function() {
        var valor = $("#txtComisionEstructuracionPorc").val();
        if (valor > 0) {
            $("#txtComisionEstructuracionPorc").css("color", "blue");
        }
    });
    //Fin
    
    //-------------------------------------------
    //Valida OpcionCompra Monto
    //-------------------------------------------
    $("#txtComisionEstructuracionMonto").focusout(function() {
        var decPrecioVenta = fn_util_ValidaDecimal($('#txtPrecioVenta').val());
        var decComisionEstructuracionMont = fn_util_ValidaDecimal($('#txtComisionEstructuracionMonto').val());
        if (decComisionEstructuracionMont >= decPrecioVenta) {
            $('#txtComisionEstructuracionMonto').val(fn_util_ValidaMonto("0", 2));
            parent.fn_util_MuestraLogPage("El monto de la Comisión de Estructuración ingresada no puede ser mayor al Precio de Venta", "E");
        } else {
            var decCalculo = (decComisionEstructuracionMont / decPrecioVenta) * 100;
            $('#txtComisionEstructuracionPorc').val(fn_util_ValidaMonto(decCalculo, 2));
        }
        //IBK - RPH
        $("#txtComisionEstructuracionMonto").css("color", "#464646");
    });

    //Inicio IBK - RPH Si edita lo pone en color Azul
    $("#txtComisionEstructuracionMonto").focus(function() {
        var valor = $("#txtComisionEstructuracionMonto").val();
        if (valor > 0) {
            $("#txtComisionEstructuracionMonto").css("color", "blue");
        }
    });
    //Fin




    //--------------------------------------
    //Valida Change del Tipo Seguro Bien
    //--------------------------------------
    $('#cmbTipoBienSeguro').change(function() {
        var strValor = $(this).val();
        fn_validaTipoSeguroBien(strValor, $('#txtNroCuotas').val());
    });

    //-------------------------------------------
    //Valida Change del Tipo Seguro Degravamen
    //-------------------------------------------
    $('#cmbTipoSeguro').change(function() {
        var strValor = $(this).val();
        fn_validaTipoSeguroDegravamen(strValor, $('#txtNroCuotas').val());
    });


    //-------------------------------------------
    //Valida Cuotas Seguro
    //-------------------------------------------
    $("#txtNumCuotasfinanciadas").focusout(function() {
        fn_of_CuotasSeguro();
    });

    //-------------------------------------------
    //Valida Cuotas Seguro Desgravamen
    //-------------------------------------------
    $("#txtNumCuotaFinanciar").focusout(function() {
        fn_of_CuotasSeguroDes();
        //IBK - RPH
        $("#txtNumCuotaFinanciar").css("color", "#464646");
    });

    //Inicio IBK - RPH Si edita lo pone en color Azul
    $("#txtNumCuotaFinanciar").focus(function() {
        var valor = $("#txtNumCuotaFinanciar").val();

        if (valor > 0) {
            $("#txtNumCuotaFinanciar").css("color", "blue");
        }
    });
    //Fin

    //Inicio IBK - RPH
    $("#txtImportePrimaDesgravamen").focusout(function() {
        $("#txtImportePrimaDesgravamen").css("color", "#464646");
    });

    $("#txtImportePrimaDesgravamen").focus(function() {
        var valor = $("#txtImportePrimaDesgravamen").val();
        if (valor > 0) {
            $("#txtImportePrimaDesgravamen").css("color", "blue");
        }
    });
    //Fin



    //-------------------------------------------
    //Periodo Disponibilidad
    /*-------------------------------------------
    $("#txtPeriodoDisponibilidad").focusout(function() {

        var strDias = $('#txtPeriodoDisponibilidad').val();
    if (fn_util_trim(strDias) == "") {
    strDias = "0";
    $('#txtPeriodoDisponibilidad').val(strDias);
    }

        var intDias = parseInt(strDias);
    var strFecha = $('#txtFechaOfertaValida').val();
    if (fn_util_trim(strFecha) == "") {
    parent.fn_util_MuestraLogPage("Debe seleccionar una Fecha de Oferta Válida para realizar el cálculo", "E");
    $('#txtPeriodoDisponibilidad').val("0");
    } else {

            //AumentaDias
    var arrParametros = ["pstrOp", "6", "pstrFecha", strFecha, "pstrDias", intDias];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');
    if (arrResultado.length > 0) {
    if (arrResultado[0] == "0") {
    $('#txtFechaMaxActivacion').val(arrResultado[1]);
    } else {
    var strError = arrResultado[1];
    fn_mdl_alert(strError.toString(), function() { });
    }
    }

        }

    });
    */
    //IBK - RPH iniciando
    fn_validaOpCompras($("#hdOpCompra").val());
    fn_validaComisionActivacion($("#hdComiAct").val());
    fn_validaComisionEstructuracion($("#hdComEstruc").val());

    if ($("#hddAdjuntarArchivo").val() != "") {
        rutaArchivo = $("#hddAdjuntarArchivo").val();
        nombreArchivo = $("#hddAdjuntarArchivo").val().split('\\').pop();
        nombreArchivo = nombreArchivo.substr(28, nombreArchivo.length);

        //alert(rutaArchivo);
        $("#dv_DescargarArchivoCronograma").html("<a href='#'  onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rutaArchivo) + "');return false;\">" + nombreArchivo + "</a>");
    } else {
        $("#dv_DescargarArchivoCronograma").html("");
    }
    $("#hddAdjuntarArchivo").val("");
    //FIN

    //On load Page (siempre al final)
    fn_onLoadPage();

});

//IBK - RPH

function fn_abreBusquedaCliente() {
    parent.fn_util_AbreModal("Cotizacion :: Búsqueda de Cliente", "Comun/frmClienteConsulta.aspx", 950, 600, function() { });
}

function fn_obtenerCliente(pNumeroDocumento, pCodigoTipoDocumento) {

    $('#txtNumeroDocumento').val(pNumeroDocumento);
    Consultar();

}

function Consultar() {
    var strtipobusquedad = "1";

    parent.fn_blockUI();
    if ($('#txtCUCliente').val() == "" && $('#txtNumeroDocumento').val() == "") {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("Para realizar la búsqueda del Cliente debe ingresar el Código Unico ó Número de Documento", "util/images/error.gif", "ADVERTENCIA");
    } else {

        //Lmpia RM
        $('#txtNombreCliente').val("");
        $('#cmbTipoPersona').val("0");
        $('#cmbTipoDocumento').val("0");

        //$('#txtNumeroDocumento').val("");

        $('#cmbEjecutivoBanca').val("0");
        $('#cmbEjecutivoBanca').change();
        $('#cmbBancaAtencion').val("0");
        $('#hddCodSuprestatario').val("");
        $('#cmbLinea').val("0");
        $('#cmbLinea').html("<option value='0'>- Seleccionar -</option>");

        $('#txtEjecutivoBanca').val("");
        $('#txtZonal').val("");

        //Valores
        //var strCodigo = $("#txtCUCliente").val(); //probar
        //debugger;
        //        if (strCodigo == "") {
        //            //tomara el valor de Numero de Documento
        //            var strCodigo = $('#txtNumeroDocumento').val();
        //            strtipobusquedad = "2";
        //           // $('#cmbLinea').val("1");
        //            //$('input[id=cmbLinea]').attr('checked', true);
        //        }

        //debugger;
        var strCodigo = $("#txtCUCliente").val();

        if (strCodigo == "") {

            //ingreso el DNI con el chek activado   
            if ($('#chkValidaCliente').is(':checked') == true && $('#txtNumeroDocumento').val() != "") {
                //tomara el valor de Numero de Documento
                //var
                strCodigo = $('#txtNumeroDocumento').val();
                strtipobusquedad = "2";

            }
        }
        else {
            if ($('#chkValidaCliente').is(':checked') == false) {
                strCodigo = $('#txtNumeroDocumento').val();
                strtipobusquedad = "2";
                fn_validaTipoPersonaEditar("2");
                fn_validaActivacionClienteNuevo(false);

                //$('#cmbTipoDocumento').append('<option value="2" selected="selected">RUC</option>');
            }

        }
        var paramArray = ["pstrCodUnico", strCodigo, "pstrTipoBusqueda", strtipobusquedad];

        fn_util_AjaxWM("frmCotizacionRegistro.aspx/ConsultaClienteRM",
                   paramArray,
                   fn_PoneDatosClienteRM,
                   function(resultado) {
                       parent.fn_unBlockUI();
                       parent.fn_mdl_mensajeIco("Se produjo un error al cargar los datos de RM", "util/images/error.gif", "ERROR EN CONSULTA RM");
                   }
            );

        //Resize Pantalla
        fn_doResize();
    }

}
//Fin
//****************************************************************
// Función		:: 	fn_abreArchivo 
// Descripción	::	Abre Archivo
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_abreArchivo(pstrRuta) {
    window.open("../frmDownload.aspx?nombreArchivo=" + pstrRuta);

    return false;
}


//Inicio IBK - RPH
function fn_validaOpCompras(valor) {
    if (valor == '1') {
        $('input[id=rdbOpCompraProc]').attr('checked', true);
        $('input[id=rdbOpCompraMonto]').attr('checked', false);
        $('#txtOpcionCompraPorc').addClass('css_input').removeClass('css_input_inactivo');
        $('#txtOpcionCompraPorc').removeAttr('disabled');
        $('#txtOpcionCompraMonto').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtOpcionCompraMonto').attr('disabled', 'disabled');

        $("#hdOpCompra").val(valor); //1

    }
    else { //Monto
        $('input[id=rdbOpCompraProc]').attr('checked', false);
        $('input[id=rdbOpCompraMonto]').attr('checked', true);
        $('#txtOpcionCompraMonto').addClass('css_input').removeClass('css_input_inactivo');
        $('#txtOpcionCompraMonto').removeAttr('disabled');
        $('#txtOpcionCompraPorc').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtOpcionCompraPorc').attr('disabled', 'disabled');

        $("#hdOpCompra").val(valor); //0
    }

}

function fn_validaComisionActivacion(valor) {
    if (valor == '1') {
        $('input[id=rdbComiActPorc]').attr('checked', true);
        $('input[id=rdbComiActMonto]').attr('checked', false);
        $('#txtComisionActivacionProc').addClass('css_input').removeClass('css_input_inactivo');
        $('#txtComisionActivacionProc').removeAttr('disabled');
        $('#txtComisionActivacionMonto').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtComisionActivacionMonto').attr('disabled', 'disabled');

        $("#hdComiAct").val(valor);   //1 
    }
    else { //Monto
        $('input[id=rdbComiActPorc]').attr('checked', false);
        $('input[id=rdbComiActMonto]').attr('checked', true);
        $('#txtComisionActivacionMonto').addClass('css_input').removeClass('css_input_inactivo');
        $('#txtComisionActivacionMonto').removeAttr('disabled');
        $('#txtComisionActivacionProc').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtComisionActivacionProc').attr('disabled', 'disabled');

        $("#hdComiAct").val(valor); //0
    }

    // alert(valor);
}

function fn_validaComisionEstructuracion(valor) {
    if (valor == '1') {

        $('input[id=rdbComEstrucPorc]').attr('checked', true);
        $('input[id=rdbComEstrucMonto]').attr('checked', false);
        $('#txtComisionEstructuracionPorc').addClass('css_input').removeClass('css_input_inactivo');
        $('#txtComisionEstructuracionPorc').removeAttr('disabled');
        $('#txtComisionEstructuracionMonto').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtComisionEstructuracionMonto').attr('disabled', 'disabled');

        $("#hdComEstruc").val(valor); //1
    }
    else { //Monto
        $('input[id=rdbComEstrucPorc]').attr('checked', false);
        $('input[id=rdbComEstrucMonto]').attr('checked', true);
        $('#txtComisionEstructuracionMonto').addClass('css_input').removeClass('css_input_inactivo');
        $('#txtComisionEstructuracionMonto').removeAttr('disabled');
        $('#txtComisionEstructuracionPorc').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtComisionEstructuracionPorc').attr('disabled', 'disabled');

        $("#hdComEstruc").val(valor); //0
    }
}
//Fin



//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
    //Valida campos obligatorio

    fn_seteaCamposObligatorios();
    //Valida Tipo de Datos

    $('#txtCUCliente').validText({ type: 'number', length: 10 });
    $('#txtNumeroDocumento').validText({ type: 'number', length: 11 });
    $('#txtNombreCliente').validText({ type: 'comment', length: 100 });

    $('#txtEjecutivoLeasing').validText({ type: 'name', length: 100 });
    $('#txtZonal').validText({ type: 'alphanumeric', length: 100 });
    $('#txtEjecutivoBanca').validText({ type: 'name', length: 100 });
    $("#txtPrecioVenta").validNumber({ value: '' });
    $("#txtMontoIGV").validNumber({ value: '' });
    $("#txtCuotaInicialPorc").validNumber({ value: '' });
    $('#txtFechaOfertaValida').validText({ type: 'date', length: 10 });
    $("#txtPrimaSeguro").validNumber({ value: '' });
    $('#txtPlazoGracia').validText({ type: 'number', length: 3 });

    $('#txtNroCuotas').validText({ type: 'number', length: 3 });
    $("#txtCuotaInicial").validNumber({ value: '' });
    $('#txtFechaPrimerVencimiento').validText({ type: 'date', length: 10 });
    $('#txtFechaMaxActivacion').validText({ type: 'date', length: 10 });
    $("#txtValorVenta").validNumber({ value: '' });
    $("#txtRiesgoNeto").validNumber({ value: '' });
    $("#txtPorcIGV").validNumber({ value: '', decimals: 2, length: 9 });

    $("#txtTEA").validNumber({ value: '', decimals: 2, length: 9 });
    $("#txtCostoFondos").validNumber({ value: '', decimals: 2, length: 9 });
    $("#txtSpread").validNumber({ value: '', decimals: 2, length: 9 });
    $("#txtPrecuota").validNumber({ value: '', decimals: 2, length: 9 });
    $("#txtPlazoGraciaPrecuota").validText({ type: 'number', length: 3 });

    $("#txtOpcionCompraPorc").validNumber({ value: '', decimals: 2, length: 9 });
    $("#txtOpcionCompraMonto").validNumber({ value: '' });
    $("#txtComisionActivacionProc").validNumber({ value: '', decimals: 2, length: 9 });
    $("#txtComisionActivacionMonto").validNumber({ value: '' });
    $("#txtComisionEstructuracionPorc").validNumber({ value: '', decimals: 2, length: 9 });
    $("#txtComisionEstructuracionMonto").validNumber({ value: '' });
    $('#txtFechaIngreso').validText({ type: 'date', length: 10 });
    $('#txtPeriodoDisponibilidad').validText({ type: 'number', length: 2 });
    $('#txaOtrasComisiones').validText({ type: 'comment', length: 250 });
    $("#txaOtrasComisiones").maxLength(250);
    $('#txtProveedores').validText({ type: 'comment', length: 250 });
    $("#txtProveedores").maxLength(250);


    $('#txtObservacionDocumentos').validText({ type: 'comment', length: 500 });
    $('#txtObservacionComentarios').validText({ type: 'comment', length: 500 });

    $("#txtImportePrimaSeguroBien").validNumber({ value: '', decimals: 2 });
    $('#txtNumCuotasfinanciadas').validText({ type: 'number', length: 3 });

    $("#txtImportePrimaDesgravamen").validNumber({ value: '', decimals: 2 });
    $('#txtNumCuotaFinanciar').validText({ type: 'number', length: 3 });
    //$('#txtSpread').validText({ type: 'number', length: 3 }); // Spred = TEA %- COSTO DE FONDO %

    $('#txtFechaIngreso').prop('readonly', true);
    //$('#txtFechaMaxActivacion').prop('readonly', true);

    strCmbTipoDocumento = $('#cmbTipoDocumento').html();
    //$('#cmbTipoDocumento').html(strComboVacio);


}


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_seteaCamposObligatorios() {

    fn_util_SeteaObligatorio($("#txtContacto"), "input");
    fn_util_SeteaObligatorio($("#txtCorreo"), "input");

    //fn_util_SeteaObligatorio($("#cmbBancaAtencion"), "select");
    //fn_util_SeteaObligatorio($("#cmbZonal"), "select");
    fn_util_SeteaObligatorio($("#txtContacto"), "input");
    fn_util_SeteaObligatorio($("#txtCorreo"), "input");
    fn_util_SeteaObligatorio($("#cmbEjecutivoLeasing"), "select");

    fn_util_SeteaObligatorio($("#txtImportePrimaSeguroBien"), "input");
    fn_util_SeteaObligatorio($("#txtNumCuotasfinanciadas"), "input");
    fn_util_SeteaObligatorio($("#txtImportePrimaDesgravamen"), "input");
    fn_util_SeteaObligatorio($("#txtNumCuotaFinanciar"), "input");

    $('#txtRiesgoNeto').attr('class', 'css_input_inactivo');
    $('#txtMontoIGV').attr('class', 'css_input_inactivo');
    $("#txtPorcIGV").attr('class', 'css_input_inactivo');
    $('#txtValorVenta').attr('class', 'css_input_inactivo');
    $('#txtRiesgoNeto').prop('readonly', true);
    $('#txtMontoIGV').prop('readonly', true);
    $('#txtValorVenta').prop('readonly', true);

    $('#txtSpread').attr('class', 'css_input_inactivo');
    $('#txtSpread').prop('readonly', true);

    $('#txtEjecutivoBanca').prop('readonly', true);
    $('#txtZonal').prop('readonly', true);



}

//Inicio IBK - RPH
function fn_EstadoBien(valor, tipo) {
    if (tipo == '0') { //Tipo de Contrato
        if (valor == 'LB') {
            $("#cmbEstadoBien").val(strEstadoBienUsado);
        }
        else { $("#cmbEstadoBien").val(strEstadoBienNuevo); }
    }
    else { //Tipo de Bien
        if (valor == '024') //Bien usado
        { $("#cmbEstadoBien").val(strEstadoBienUsado); }
        else
        { $("#cmbEstadoBien").val(strEstadoBienNuevo); }
    }
    //alert(valor);
}
//Fin

//****************************************************************
// Funcion		:: 	fn_inicializaNuevo
// Descripción	::	Inicia parametros y campos cuando es NUEVO
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaNuevo() {

    //Cotizacion
    $('#txtNumeroCotizacion').val("[Autogenerado]");

    //IGV
    $('#txtPorcIGV').val(fn_util_ValidaMonto($('#hddIGV').val(), 2));

    //Estado
    $('#cmbEstado').val(strEstadoCotizacionIngresado);

    //Cliente
    $('input[name=chkValidaCliente]').attr('checked', true);
    $("#hddValidaCliente").val("1");
    fn_validaActivacionClienteNuevo(false);

    //CuotaInicial
    $('input[id=rdbTipoCuotaMonto]').attr('checked', true);
    $("#hddTipoCuota").val("1");
    fn_validaTipoCuota("1");

    //Carta
    $("#hddGeneraCarta").val("0");
    $('#chkGeneraCarta').attr('disabled', 'disabled');

    //Banca - ELeasing
    $('#cmbBancaAtencion').attr('disabled', 'disabled');
    //$('#cmbEjecutivoLeasing').attr('disabled', 'disabled');
    $('#cmbBancaAtencion').attr('class', 'css_select_inactivo');
    //$('#cmbEjecutivoLeasing').attr('class', 'css_select_inactivo');

    //Linea
    $('#cmbLinea').attr('class', 'css_select_inactivo');
    $('#cmbLinea').attr('disabled', 'disabled');
    $("#imgBusqLinea").hide();

    //$('#cmbEstado').attr('disabled','disabled');
    $('#txtMontoIGV').attr('disabled', 'disabled');
    $('#txtRiesgoTotal').attr('disabled', 'disabled');
    $('#txtNumeroCotizacion').attr('disabled', 'disabled');

    //Procedencia
    $("#cmbprocedencia").val(strProcedenciaLocal);

    //Moneda
    $("#cmbMoneda").val(strTipoMonedaDolares);

    //EstadoBien
    $("#cmbEstadoBien").val(strEstadoBienNuevo);

    //Periodicidad
    fn_cargaComboPeriocidad();
    $("#cmbPeriodicidad").val(strPeriodicidadMensual);

    //Seguro Degravamen
    $("#fld_SeguroDegravamen").hide();

    //Valida Fecha
    fn_validaFechaActivacion("");

    //Fechas
    $("#txtFechaIngreso").val($("#hddFechaActual").val());
    //$("#txtFechaOfertaValida").val($("#hddFechaActual").val());

    //Opcion de Compra porc
    $("#txtOpcionCompraPorc").val(fn_util_ValidaMonto("1", 2));
    fn_of_OpcionCompraPorc();

    //Opciones Options
    fn_validaMostrarTea(1);
    fn_validaMostrarComision(1);

    //Precuota
    $('#txtPrecuota').addClass('css_input_inactivo').removeClass('css_input');
    $('#txtPrecuota').prop('readonly', true);
    $('#txtPlazoGraciaPrecuota').val("0");
    $('#txtPlazoGraciaPrecuota').addClass('css_input_inactivo').removeClass('css_input');
    $('#txtPlazoGraciaPrecuota').prop('readonly', true);

    //Valida COmision
    fn_validaComisionMoneda($("#cmbMoneda").val());

    //Seguros
    $("#cmbTipoBienSeguro").val(strSeguroBienTipoInterno);
    fn_validaTipoSeguroBien(strSeguroBienTipoInterno, $('#txtNroCuotas').val());

    //Inicio IBK - AAE Inicializo radios
    $('input[name=rdbComiActMonto]').attr('checked', true);
    $('input[name=rdbComiActPorc]').attr('checked', false);
    $('#rdbComiActMonto').attr('checked', 'checked');
    fn_validaComisionActivacion('0')

    $('input[name=rdbComEstrucMonto]').attr('checked', true);
    $('input[name=rdbComEstrucPorc]').attr('checked', false);
    $('#rdbComEstrucMonto').attr('checked', 'checked');
    fn_validaComisionEstructuracion('0')

    // Fin IBK

}


//****************************************************************
// Funcion		:: 	fn_PoneDatosClienteRM
// Descripción	::	Pone Datos del Cliente RM
// Log			:: 	JRC - 15/05/2012
//****************************************************************
var fn_PoneDatosClienteRM = function(response) {

    var objEClienteRM = response;

    if (objEClienteRM.CodError == 1) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objEClienteRM.MsgError, "util/images/error.gif", "ERROR EN CONSULTA RM");
    } else {



        $('#hddCodUnico').val(objEClienteRM.Codigounico);
        $('#txtCUCliente').val(objEClienteRM.Codigounico);
        $('#txtNombreCliente').val(objEClienteRM.Razonsocialcliente);
        $('#cmbTipoDocumento').val(objEClienteRM.Codigotipodocumento);
        $('#txtNumeroDocumento').val(objEClienteRM.Numerodocumento);
        $('#cmbEjecutivoBanca').val(objEClienteRM.Codigoejecutivo);
        $('#cmbEjecutivoBanca').change();

        $('#txtEjecutivoBanca').val(objEClienteRM.Nombreejecutivo);
        $('#hddCodSuprestatario').val(objEClienteRM.CodClienteLocal);

        $('#hddDireccionCliente').val(objEClienteRM.Direccion);
        //Inicio iBK
        //Una vez encontrado bloqueo el nro de documento
        $('#txtNumeroDocumento').attr('class', 'css_input_inactivo');
        $('#txtNumeroDocumento').prop('readonly', true);
        //Fin IBK
        //Tipo Documento
        var strNumeroDocumento = objEClienteRM.Numerodocumento
        if (objEClienteRM.Codigotipodocumento == strTipoDocumentoRuc) {
            if (strNumeroDocumento.substring(0, 1) == "2") {
                $('#cmbTipoPersona').val(strTipoPersonaJuridica);
            } else {
                $('#cmbTipoPersona').val(strTipoPersonaNatural);
            }
        } else {
            $('#cmbTipoPersona').val(strTipoPersonaNatural);
        }

        //Tipo Persona
        if ($('#cmbTipoPersona').val() == strTipoPersonaNatural) {
            $('#fld_SeguroDegravamen').show();
        } else {
            $('#fld_SeguroDegravamen').hide();
        }

        //Log
        parent.fn_util_MuestraLogPage("El sistema ubicó el Código Unico: " + objEClienteRM.Codigounico + " en RM. Se muestran los datos.", "I");

        //Consulta Ultimus
        var arrParametros = ["pstrCodEjecutivoBanca", objEClienteRM.Codigoejecutivo];
        fn_util_AjaxSyncWM("frmCotizacionRegistro.aspx/ConsultaUltimus",
                 arrParametros,
                 function(result) {
                     parent.fn_unBlockUI();
                     //alert(fn_util_trim(result));
                     var pstrResultado = fn_util_trim(result);
                     var arrResultado = pstrResultado.split("|")
                     $('#txtZonal').val(arrResultado[0]);
                     $('#cmbBancaAtencion').val(arrResultado[1]);
                     //$('#cmbEjecutivoLeasing').html(arrResultado[3]);
                     //Consulta Spread Banca
                     fn_oc_bancaAtencion(arrResultado[1]);
                 },
                 function(resultado) {
                     parent.fn_unBlockUI();
                     var error = eval("(" + resultado.responseText + ")");
                     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN ULTIMUS");
                 }
        );
        //debugger;

        //Consulta Lineas de Cliente    
        var strCodUnico = $("#txtCUCliente").val();
        var strCodProducto = "";
        var arrParametros = ["pstrCodUnico", strCodUnico, "pstrCodProducto", strCodProducto];
        fn_util_AjaxSyncWM("frmCotizacionRegistro.aspx/ConsultaLineas",
                 arrParametros,
                 function(result) {
                     parent.fn_unBlockUI();
                     if (fn_util_trim(result) != "") {
                         $('#cmbLinea').html(fn_util_trim(result));
                     }
                     if ($('#cmbLinea option').size() <= 1) {
                         $('#chkLinea').attr('disabled', 'disabled');
                         $('input[name=chkLinea]').attr('checked', false);
                         $("#hddValidaLinea").val("0");
                         $('#cmbLinea').attr('class', 'css_select_inactivo');
                         $('#cmbLinea').attr('disabled', 'disabled');
                         $("#imgBusqLinea").hide();
                         parent.fn_util_MuestraLogPage("No se encontraron Lineas para el Cliente seleccionado", "I");
                     } else {
                         $('#chkLinea').removeAttr('disabled');
                         $('input[name=chkLinea]').attr('checked', false);
                         $("#hddValidaLinea").val("0");
                     }
                 },
                 function(resultado) {
                     parent.fn_unBlockUI();
                     var error = eval("(" + resultado.responseText + ")");
                     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LINEAS");
                 }
        );

    }

    fn_doResize();

};


//****************************************************************
// Funcion		:: 	fn_validaActivacionClienteNuevo
// Descripción	::	Valida si el cliente viene de RM o es EDITAR
// Log			:: 	JRC - 08/05/2012
//****************************************************************
function fn_validaActivacionClienteNuevo(pblnActiva) {
    $('#txtCUCliente').val("");
    $('#hddCodUnico').val("");
    $('#txtNumeroDocumento').val("");
    $('#txtNombreCliente').val("");
    $('#cmbTipoPersona').val("0");
    $('#cmbEjecutivoBanca').val("0");
    $('#cmbTipoDocumento').val("0");

    if (pblnActiva) {

        $('input[name=chkLinea]').attr('checked', false);
        $('#cmbLinea').attr('class', 'css_select_inactivo');
        $('#cmbLinea').attr('disabled', 'disabled');
        $('#cmbLinea').val("0");
        $("#imgBusqLinea").hide();
        $("#hddTasaLinea").val("");
        $('#chkLinea').attr('disabled', 'disabled');

        $('#txtCUCliente').removeClass('css_input css_input_obligatorio').addClass('css_input_inactivo');
        $('#txtCUCliente').prop('readonly', true);
        $('#txtCUCliente').val("00000000");
        $('#hddCodUnico').val("00000000");

        $('#txtNombreCliente').removeClass('css_input_inactivo').addClass('css_input_obligatorio');
        $('#txtNombreCliente').prop('readonly', false);

        $('#txtNumeroDocumento').removeClass('css_input_inactivo').addClass('css_input_obligatorio');
        $('#txtNumeroDocumento').prop('readonly', false);

        $('#cmbTipoPersona').attr('class', 'css_select css_select_obligatorio');
        $('#cmbTipoPersona').removeAttr('disabled');
        $('#cmbTipoDocumento').attr('class', 'css_select css_select_obligatorio');
        $('#cmbTipoDocumento').removeAttr('disabled');
        $('#cmbEjecutivoBanca').attr('class', 'css_select css_select_obligatorio');
        $('#cmbEjecutivoBanca').removeAttr('disabled');

        $('#imgBsqClienteRM').hide();

        $('#cmbBancaAtencion').attr('class', 'css_select css_select_obligatorio');
        $('#cmbBancaAtencion').removeAttr('disabled');

    } else {

        $("#hddTasaLinea").val("");
        $('#cmbLinea').val("0");
        $('#chkLinea').removeAttr('disabled');

        $('#txtCUCliente').addClass('css_input css_input_obligatorio').removeClass('css_input_inactivo');
        $('#txtCUCliente').prop('readonly', false);
        $('#txtCUCliente').val("");

        $('#txtNombreCliente').attr('class', 'css_input_inactivo');
        $('#txtNombreCliente').prop('readonly', true);

        //IBK - RPH
        //$('#txtNumeroDocumento').attr('class', 'css_input_inactivo');
        //$('#txtNumeroDocumento').prop('readonly', true);

        $('#txtNumeroDocumento').addClass('css_input css_input_obligatorio').removeClass('css_input_inactivo');
        $('#txtNumeroDocumento').prop('readonly', false);
        $('#txtNumeroDocumento').val("");
        //Fin
        
        $('#cmbTipoPersona').attr('class', 'css_select_inactivo');
        $('#cmbTipoPersona').attr('disabled', 'disabled');
        $('#cmbTipoDocumento').attr('class', 'css_select_inactivo');
        $('#cmbTipoDocumento').attr('disabled', 'disabled');
        $('#cmbEjecutivoBanca').attr('class', 'css_select_inactivo');
        $('#cmbEjecutivoBanca').attr('disabled', 'disabled');

        $('#imgBsqClienteRM').show();

        $('#cmbBancaAtencion').attr('class', 'css_select_inactivo');
        $('#cmbBancaAtencion').attr('disabled', 'disabled');

    }

    //Ultimus
    $('#txtEjecutivoBanca').val("");
    $('#cmbBancaAtencion').val("0");
    $('#txtZonal').val("");

    //Valores por Defecto
    $('#cmbTipoContrato').val(strTipoContratoLeasing);
    $('#cmbTipoCronograma').val(strTipoCronogramaCuotaConst);
    $('#txtPlazoGracia').val(strPlazoGracia);
    $('#cmbSubTipoContrato').val(strSubTipoContrato);

}

//****************************************************************
// Funcion		:: 	fn_validaSubTipoContrato
// Descripción	::	Valida el Sub Tipo Contrato
// Log			:: 	JRC - 08/05/2012
//****************************************************************
function fn_validaSubTipoContrato(strValor) {
    if (strValor == strSubTipoContratoParcial) {
        $('#txtPrecuota').addClass('css_input').removeClass('css_input_inactivo');
        $('#txtPrecuota').prop('readonly', false);
        $('#txtPlazoGraciaPrecuota').val("0");
        $('#txtPlazoGraciaPrecuota').addClass('css_input').removeClass('css_input_inactivo');
        $('#txtPlazoGraciaPrecuota').prop('readonly', false);
    } else {
        $('#txtPrecuota').val($('#txtTEA').val());
        $('#txtPrecuota').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtPrecuota').prop('readonly', true);
        $('#txtPlazoGraciaPrecuota').val("0");
        $('#txtPlazoGraciaPrecuota').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtPlazoGraciaPrecuota').prop('readonly', true);
        if ($('#cmbTipoContrato').val() == strTipoContratoLeasing) {
            $("#cmbprocedencia").val(strProcedenciaLocal)
        }
    }
}
//****************************************************************
// Funcion		:: 	fn_validaSubTipoContratoEditar
// Descripción	::	Valida el Sub Tipo Contrato
// Log			:: 	JRC - 08/05/2012
//****************************************************************
function fn_validaSubTipoContratoEditar(strValor) {
    if (strValor == strSubTipoContratoParcial) {
        $('#txtPrecuota').addClass('css_input').removeClass('css_input_inactivo');
        $('#txtPrecuota').prop('readonly', false);
        $('#txtPlazoGraciaPrecuota').addClass('css_input').removeClass('css_input_inactivo');
        $('#txtPlazoGraciaPrecuota').prop('readonly', false);
    } else {
        $('#txtPrecuota').val($('#txtTEA').val());
        $('#txtPrecuota').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtPrecuota').prop('readonly', true);
        $('#txtPlazoGraciaPrecuota').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtPlazoGraciaPrecuota').prop('readonly', true);
        if ($('#cmbTipoContrato').val() == strTipoContratoLeasing) {
            $("#cmbprocedencia").val(strProcedenciaLocal)
        }
    }
}

//****************************************************************
// Funcion		:: 	fn_validaTipoCuota
// Descripción	::	Valida el Tipo de Cuota
// Log			:: 	JRC - 08/05/2012
//****************************************************************
function fn_validaTipoCuota(pstrTipo) {
    $("#hddTipoCuota").val(pstrTipo);
    $("#txtCuotaInicial").val(fn_util_ValidaMonto("0", 2));
    $("#txtCuotaInicialPorc").val(fn_util_ValidaMonto("0", 2));
    if (pstrTipo == "1") {
        $('#txtCuotaInicial').addClass('css_input').removeClass('css_input_inactivo');
        $('#txtCuotaInicial').removeAttr('disabled');
        $('#txtCuotaInicialPorc').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtCuotaInicialPorc').attr('disabled', 'disabled');
        //$('#txtCuotaInicial').focus();    
    } else {
        $('#txtCuotaInicial').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtCuotaInicial').attr('disabled', 'disabled');
        $('#txtCuotaInicialPorc').addClass('css_input').removeClass('css_input_inactivo');
        $('#txtCuotaInicialPorc').removeAttr('disabled');
        //$('#txtCuotaInicialPorc').focus();
    }
    fn_of_calculaNeto();
}

//****************************************************************
// Funcion		:: 	fn_validaTipoCuotaEditar
// Descripción	::	Valida el Tipo de Cuota
// Log			:: 	JRC - 08/05/2012
//****************************************************************
function fn_validaTipoCuotaEditar(pstrTipo) {
    $("#hddTipoCuota").val(pstrTipo);
    if (pstrTipo == "1") {
        $('input[id=rdbTipoCuotaMonto]').attr('checked', true);
        $('#txtCuotaInicial').addClass('css_input').removeClass('css_input_inactivo');
        $('#txtCuotaInicial').removeAttr('disabled');
        $('#txtCuotaInicialPorc').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtCuotaInicialPorc').attr('disabled', 'disabled');
        //$('#txtCuotaInicial').focus();
    } else {
        $('input[id=rdbTipoCuotaPorc]').attr('checked', true);
        $('#txtCuotaInicial').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtCuotaInicial').attr('disabled', 'disabled');
        $('#txtCuotaInicialPorc').addClass('css_input').removeClass('css_input_inactivo');
        $('#txtCuotaInicialPorc').removeAttr('disabled');
        //$('#txtCuotaInicialPorc').focus();
    }
}


//****************************************************************
// Funcion		:: 	fn_cargaComboPeriocidad
// Descripción	::	CargaComboPeriodicidad
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_cargaComboPeriocidad() {

    var arrParametros = ["pstrOp", "1", "pstrTablaGenerica", "TBL019"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbPeriodicidad ').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}


//****************************************************************
// Funcion		:: 	fn_devolvercotizacion  
// Descripción	::	Devolver Cotizacion
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_devolvercotizacion(scodcontrato) {

    fn_mdl_confirma(
                    "Está seguro que desea devolver Cotizacion ??  ", //Mensaje - Obligatorio
                    function() {
                        parent.fn_util_AbreModal("Cotización:: Motivo Rechazo", "Cotizacion/frmMotivoRechazo.aspx?hddCodigo=" + scodcontrato, 650, 310, function() { });
                    }, // ACCION SI - Obligatorio
                    null, //Imagen - puede ser nulo
                    function() { }, // ACCION SI - Obligatorio 
                    'COTIZACION' //Titulo - Puede ir vacio o no ponerlo
                   );
}


//****************************************************************
// Funcion		:: 	fn_aprobarcotizacion  
// Descripción	::	Aprobar Cotizacion
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_aprobarcotizacion(scodcontrato) {
    fn_mdl_confirma(
                    "Está seguro de Aprobar la Cotizacion  ?? ", //Mensaje - Obligatorio
                    function() {
                        fn_util_redirect('frmCotizacionListado.aspx');
                    }, // ACCION SI - Obligatorio
                    null, //Imagen - puede ser nulo
                    function() { }, // ACCION SI - Obligatorio 
                    'COTIZACION' //Titulo - Puede ir vacio o no ponerlo
                   );
}


//****************************************************************
// Funcion		:: 	fn_DetalleLineas  
// Descripción	::	Aprobar Cotizacion
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_DetalleLineas() {
    var strLinea = $("#cmbLinea").val();
    parent.fn_util_AbreModal("COTIZACION :: DETALLE DE LINEAS", "Cotizacion/frmCotizacionLineaDetalle.aspx?hddLinea=" + strLinea, 650, 250, function() { });
}


//****************************************************************
// Funcion		:: 	fn_agregarDocumento
// Descripción	::	Grabar Documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_agregarDocumento() {
}


//****************************************************************
// Funcion		:: 	fn_eliminarDocumento
// Descripción	::	Eliminar Comentario
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_eliminarDocumento() {
}


//****************************************************************
// Funcion		:: 	fn_cancelar  
// Descripción	::	Cancela las Operaciones de Cotizacion
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cancelar() {

    parent.fn_mdl_confirma(
                    "¿Está seguro de Volver?",
                    function() {
                        fn_util_redirect('frmCotizacionListado.aspx');
                    },
                    "Util/images/question.gif",
                    function() { },
                    'CONFIRMACION'
                   );
}


//************************************************************
// Función		:: 	fn_grabar
// Descripcion 	:: 	Método que graba
// Log			:: 	JRC - 09/05/2012
//************************************************************
function fn_grabar(strEnviar, strValidaEnviar) {
    parent.fn_blockUI();

    //String Validación
    var strError = new StringBuilderEx();

    //Instancia Validaciones    
    var objtxtCUCliente = $('input[id=txtCUCliente]:text');
    var objtxtNombreCliente = $('input[id=txtNombreCliente]:text');
    var objcmbTipoPersona = $('select[id=cmbTipoPersona]');
    var objcmbTipoDocumento = $('select[id=cmbTipoDocumento]');
    var objtxtNumeroDocumento = $('input[id=txtNumeroDocumento]:text');
    //var objcmbEjecutivoBanca = $('select[id=cmbEjecutivoBanca]');
    var objcmbBancaAtencion = $('select[id=cmbBancaAtencion]');
    //var objcmbZonal = $('select[id=cmbZonal]');
    var objtxtContacto = $('input[id=txtContacto]:text');
    var objtxtCorreo = $('input[id=txtCorreo]:text');
    var objcmbEjecutivoLeasing = $('select[id=cmbEjecutivoLeasing]');

    var objcmbLinea = $('select[id=cmbLinea]');


    //----------------------------------
    //General Cabecera
    //----------------------------------
    if (fn_util_trim($("#hddCodUnico").val()) != fn_util_trim($("#txtCUCliente").val())) {
        strError.append('&nbsp;&nbsp;- El CU del Cliente ha sido cambiado, vuelva hacer la búsqueda.<br />');
    }


    //strError.append(fn_util_ValidateControl(objcmbBancaAtencion[0], 'una Banca de Atención válida', 1, ''));
    //strError.append(fn_util_ValidateControl(objcmbZonal[0], 'una Zona válida', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtContacto[0], 'un Contacto válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtCorreo[0], 'un Correo válido', 1, ''));
    strError.append(fn_util_ValidateControl(objcmbEjecutivoLeasing[0], 'un Ejecutivo Leasing válido', 1, ''));

    //----------------------------------
    // VALIDA CLIENTE
    //----------------------------------
    if ($("#chkValidaCliente").attr('checked')) {
        strError.append(fn_util_ValidateControl(objtxtCUCliente[0], 'un Código Unico válido', 1, ''));
    } else {
        strError.append(fn_util_ValidateControl(objtxtNombreCliente[0], 'un Nombre Cliente válido', 1, ''));
        strError.append(fn_util_ValidateControl(objcmbTipoPersona[0], 'un Tipo Persona válido', 1, ''));
        strError.append(fn_util_ValidateControl(objcmbTipoDocumento[0], 'un Tipo Documento válido', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtNumeroDocumento[0], 'un Número Documento válido', 1, ''));
        //strError.append(fn_util_ValidateControl(objcmbEjecutivoBanca[0], 'un Ejecutivo Banca válido', 1, ''));

        var strTipoDocumento = $("#cmbTipoDocumento").val();
        var strNroDocumento = $("#txtNumeroDocumento").val();
        var intNroDocumento = strNroDocumento.length;
        if (fn_util_trim(strTipoDocumento) == strTipoDocumentoDNI) {
            if (intNroDocumento < 8) strError.append('&nbsp;&nbsp;- Número de Documento Inválido <br />');
        } else if (fn_util_trim(strTipoDocumento) == strTipoDocumentoRUC) {
            if (intNroDocumento < 11) strError.append('&nbsp;&nbsp;- Número de Documento Inválido <br />');
        } else if (fn_util_trim(strTipoDocumento) == strTipoDocumentoCarnetEx) {
            if (intNroDocumento < 4) strError.append('&nbsp;&nbsp;- Número de Documento Inválido <br />');
        } else if (fn_util_trim(strTipoDocumento) == strTipoDocumentoPasaporte) {
            if (intNroDocumento < 7) strError.append('&nbsp;&nbsp;- Número de Documento Inválido <br />');
        } else {
            if (intNroDocumento < 4) strError.append('&nbsp;&nbsp;- Número de Documento Inválido <br />');
        }

    }

    //----------------------------------
    // VALIDA LINEA
    //----------------------------------
    if ($(this).attr('checked')) {
        strError.append(fn_util_ValidateControl(objcmbLinea[0], 'una Linea válido', 1, ''));
    }

    //----------------------------------
    //VALIDA CORREO
    //----------------------------------
    if (fn_util_trim(objtxtCorreo[0].value) != "") {
        if (!fn_util_ValidateEmailPuntoComas(objtxtCorreo[0].value)) {
            strError.append('&nbsp;&nbsp;- Ingrese un Correo válido.<br />');
        }
    }


    //----------------------------------
    // VALIDA ENVIAR
    //----------------------------------
    if (fn_util_trim(strValidaEnviar) == "1") {

        //Declara        
        var objcmbTipoContrato = $('select[id=cmbTipoContrato]');
        var objcmbSubTipoContrato = $('select[id=cmbSubTipoContrato]');
        var objcmbMoneda = $('select[id=cmbMoneda]');
        var objcmbprocedencia = $('select[id=cmbprocedencia]');
        var objcmbClasificacionBien = $('select[id=cmbClasificacionBien]');
        var objcmbTipoBien = $('select[id=cmbTipoBien]');
        var objcmbEstadoBien = $('select[id=cmbEstadoBien]');

        var objtxtPrecioVenta = $('input[id=txtPrecioVenta]:text');
        var objtxtPorcIGV = $('input[id=txtPorcIGV]:text');
        var objtxtMontoIGV = $('input[id=txtMontoIGV]:text');
        var objtxtValorVenta = $('input[id=txtValorVenta]:text');
        var objtxtCuotaInicial = $('input[id=txtCuotaInicial]:text');
        var objtxtCuotaInicialPorc = $('input[id=txtCuotaInicialPorc]:text');
        var objtxtRiesgoNeto = $('input[id=txtRiesgoNeto]:text');

        strError.append(fn_util_ValidateControl(objcmbTipoContrato[0], 'un Tipo de Contrato válido', 1, ''));
        strError.append(fn_util_ValidateControl(objcmbSubTipoContrato[0], 'un Sub Tipo Contrato válido', 1, ''));
        strError.append(fn_util_ValidateControl(objcmbMoneda[0], 'una Moneda válida', 1, ''));
        strError.append(fn_util_ValidateControl(objcmbprocedencia[0], 'una Procedencia válida', 1, ''));
        strError.append(fn_util_ValidateControl(objcmbClasificacionBien[0], 'una Clasificacion de Bien válida', 1, ''));
        strError.append(fn_util_ValidateControl(objcmbTipoBien[0], 'un Tipo Bien válido', 1, ''));
        strError.append(fn_util_ValidateControl(objcmbEstadoBien[0], 'un Estado Bien válido', 1, ''));

        strError.append(fn_util_ValidateControl(objtxtPrecioVenta[0], 'un Precio Venta válido', 1, ''));
        //strError.append(fn_util_ValidateControl(objtxtPorcIGV[0], 'un Porcentaje IGV válido', 1, ''));
        //strError.append(fn_util_ValidateControl(objtxtMontoIGV[0], 'un Monto IGV válido', 1, ''));
        //strError.append(fn_util_ValidateControl(objtxtValorVenta[0], 'un Valor Venta válido', 1, ''));
        //strError.append(fn_util_ValidateControl(objtxtCuotaInicial[0], 'un Cuota Inicial válido', 1, ''));
        //strError.append(fn_util_ValidateControl(objtxtCuotaInicialPorc[0], 'un Porcentaje Cuota Inicial válido', 1, ''));
        //strError.append(fn_util_ValidateControl(objtxtRiesgoNeto[0], 'un Riesgo Neto válido', 1, ''));

        //Valida Cronograma
        var intCantCronograma = $("#jqGrid_lista_C").getGridParam("reccount");
        if (intCantCronograma == null || intCantCronograma == "" || intCantCronograma == undefined || intCantCronograma == "undefined") intCantCronograma = 0;
        if (intCantCronograma == 0) {
            strError.append("&nbsp;&nbsp;- Debe generar el Cronograma para hacer el envio.<br />");
        }

        //Valida Fecha Activacion
        var strMensajeFecActivacion = fn_validaFechaActivacion();
        if (fn_util_trim(strMensajeFecActivacion) != "") {
            strError.append(strMensajeFecActivacion);
        }



        //Valida Seguro
        if ($("#cmbTipoBienSeguro").val() == strSeguroBienTipoInterno) {
            var objtxtImportePrimaSeguroBien = $('input[id=txtImportePrimaSeguroBien]:text');
            var objtxtNumCuotasfinanciadas = $('input[id=txtNumCuotasfinanciadas]:text');

            var decImporteSeguro = fn_util_ValidaDecimal($("#txtImportePrimaSeguroBien").val());
            var strCuotasSeguro = $("#txtNumCuotasfinanciadas").val();
            if (strCuotasSeguro == "") strCuotasSeguro = "0";

            if (decImporteSeguro <= 0) {
                //strError.append('- El Importe Prima del Seguro debe ser mayor a cero<br />');
                strError.append(fn_util_ValidateControl(objtxtImportePrimaSeguroBien[0], 'un valor mayor a cero en el Importe Prima', 1, ''));
            }
            if (parseInt(strCuotasSeguro) <= 0) {
                //strError.append('- Las Cuotas a Financiar del Seguro debe ser mayor a cero<br / >');
                strError.append(fn_util_ValidateControl(objtxtNumCuotasfinanciadas[0], 'un valor mayor a cero en las Cuotas a Financiar', 1, ''));
            }
        }

        //Valida SeguroDegravamen
        if ($('#cmbTipoPersona').val() == strTipoPersonaNatural) {
            if ($("#cmbTipoSeguro").val() == strSeguroDegravamenTipoInterno) {

                var objtxtImportePrimaDesgravamen = $('input[id=txtImportePrimaDesgravamen]:text');
                var objtxtNumCuotaFinanciar = $('input[id=txtNumCuotaFinanciar]:text');

                var decImporteSeguroDes = fn_util_ValidaDecimal($("#txtImportePrimaDesgravamen").val());
                var strCuotasSeguroDes = $("#txtNumCuotaFinanciar").val();
                if (strCuotasSeguroDes == "") strCuotasSeguroDes = "0";

                if (decImporteSeguroDes <= 0) {
                    //strError.append('- El Importe Prima del Seguro de Desgravamen debe ser mayor a cero<br />');
                    strError.append(fn_util_ValidateControl(objtxtImportePrimaDesgravamen[0], 'un valor mayor a cero en el Importe Prima del Seguro de Desgravamen', 1, ''));
                }
                if (parseInt(strCuotasSeguroDes) <= 0) {
                    //strError.append('- Las Cuotas a Financiar del Seguro de Desgravamen debe ser mayor a cero<br />');
                    strError.append(fn_util_ValidateControl(objtxtNumCuotaFinanciar[0], 'un valor mayor a cero en las Cuotas a Financiar del Seguro de Desgravamen', 1, ''));
                }
            }
        }


        //Valida Posicion Numero
        $("#txtPrecioVenta").addClass("ui-edit-align-right");
        $("#txtPorcIGV").addClass("ui-edit-align-right");
        $("#txtMontoIGV").addClass("ui-edit-align-right");
        $("#txtValorVenta").addClass("ui-edit-align-right");
        $("#txtCuotaInicial").addClass("ui-edit-align-right");
        $("#txtCuotaInicialPorc").addClass("ui-edit-align-right");
        $("#txtRiesgoNeto").addClass("ui-edit-align-right");

        $("#txtImportePrimaSeguroBien").addClass("ui-edit-align-right");
        $("#txtNumCuotasfinanciadas").addClass("ui-edit-align-right");
        $("#txtImportePrimaDesgravamen").addClass("ui-edit-align-right");
        $("#txtNumCuotaFinanciar").addClass("ui-edit-align-right");

    }




    //Valida Fecha Valida Hasta
    var dtmFechaValida = $("#txtFechaOfertaValida").val();
    var dtmFechaIngreso = $("#txtFechaIngreso").val();
    if (fn_util_ComparaFecha(dtmFechaValida, dtmFechaIngreso)) {
        strError.append("- La fecha de Oferta Válida Hasta debe ser mayor a la Fecha de Ingreso<br />");
    }


    //Valida Cambios en Cronograma    
    if (strError.toString() == '') {
        if ($("#hddTipoTransaccion").val() != "NUEVO") {
            var intCantCronograma = $("#jqGrid_lista_C").getGridParam("reccount");
            if (intCantCronograma > 0) {
                if (!fn_validaCambioDataCronograma()) {
                    strError.append("- Los datos para la generación del Cronograma han cambiado. Vuelva a generar el Cronograma<br />");
                }
            }
        } else {
            if (fn_util_trim(strValidaEnviar) == "1") {
                var intCantCronograma = $("#jqGrid_lista_C").getGridParam("reccount");
                if (intCantCronograma > 0) {
                    if (!fn_validaCambioDataCronograma()) {
                        strError.append("- Los datos para la generación del Cronograma han cambiado. Vuelva a generar el Cronograma<br />");
                    }
                }
            }
        }
    }


    //Valida si hay Error
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        //fn_seteaCamposObligatorios();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }
    else {

        //Valida Tipo Enviar
        var strEnviarSupervisor = fn_validaEnviarCotizacion(strEnviar);
        if (strEnviar == "1") { //Boton Guardar y Enviar
            if (strEnviarSupervisor == "1") { //Si va a enviar a Supervisor
                if (fn_validaReenvioSupervisor()) { //Datos SI han cambiado                    
                    $("#hddGeneraCarta").val("0");
                    $('input[name=chkGeneraCarta]').attr('checked', false);
                    strEnviarSupervisor = fn_validaEnviarCotizacion(strEnviar);
                } else {//Datos NO han cambiado                    
                    if (strEstadoCotizacionIngresado == $("#cmbEstado").val()) {
                        strEnviarSupervisor = fn_validaEnviarCotizacion(strEnviar);
                    } else {
                        strEnviarSupervisor = "0";
                    }
                }
            }
        }

        //Valida caso Evaluacion Cliente 9
        if (strEstadoCotizacion_EvaCliente == $("#cmbEstado").val()) {
            //alert(blnDatosCambiados);
            if (blnDatosCambiados) {
                strEnviarSupervisor = "0";
            } else {
                strEnviarSupervisor = "9";
            }
        }

        //Valida caso Pendiente F10
        if (strEstadoCotizacion_PendF10 == $("#cmbEstado").val()) {
            //alert(blnDatosCambiados);
            if (blnDatosCambiados) {
                strEnviarSupervisor = "0";
            } else {
                strEnviarSupervisor = "9";
            }
        }



        //Valida Tipo Transaccion
        if ($("#hddTipoTransaccion").val() == "NUEVO") {

            fn_ejecutaGrabar(1, strEnviarSupervisor, strEnviar);

        } else {

            parent.fn_unBlockUI();

            if (strValidaEnviar == "1") {

                var intCantCronograma = $("#jqGrid_lista_C").getGridParam("reccount");
                if (parseInt(intCantCronograma) > 0) {
                    //Inicio IBK - RPH Obtengo el valor para generar Carta
                    $('#hddValidaEnviar').val(strValidaEnviar);
                    //Fin

                    parent.fn_mdl_confirma(
                        "¿Desea grabar los cambios en la Cotización?"
                        , function() {


                            parent.fn_mdl_confirma(
                                "¿Desea generar una Nueva Versión de la Cotización que está actualizando?"
                                , function() { fn_ejecutaGrabar(1, strEnviarSupervisor, strEnviar); }
                                , "Util/images/question.gif"
                                , function() { fn_ejecutaGrabar(0, strEnviarSupervisor, strEnviar); }
                                , "VERSION COTIZACION"
                            );


                        }
                        , "Util/images/question.gif"
                        , function() { }
                        , "CONFIRMACIÓN"
                    );


                } else {
                    parent.fn_unBlockUI();
                    parent.fn_mdl_mensajeError("La Cotización no tiene Cronograma. Genere el Cronograma antes de enviar.", function() { }, "CRONOGRAMA PENDIENTE");
                }

            } else if (strValidaEnviar == "2") {
                parent.fn_mdl_confirma(
                    "¿Está seguro que desea Devolver la Cotización?"
                    , function() {
                        fn_ejecutaGrabar(0, strEnviarSupervisor, strEnviar);
                        /*
                        parent.fn_mdl_confirma(
                        "¿Desea generar una Nueva Versión de la Cotización que está actualizando?"
                        , function() { fn_ejecutaGrabar(1, strEnviarSupervisor, strEnviar); }
                        , "Util/images/question.gif"
                        , function() { fn_ejecutaGrabar(0, strEnviarSupervisor, strEnviar); }
                        , "VERSION COTIZACION"
                        );  
                        */
                    }
                    , "Util/images/question.gif"
                    , function() { parent.fn_unBlockUI(); }
                    , "CONFIRMACION"
                );

            } else if (strValidaEnviar == "3") {

                parent.fn_mdl_confirma(
                    "¿Está seguro que desea Aprobar la Cotización?"
                    , function() {
                        fn_ejecutaGrabar(0, strEnviarSupervisor, strEnviar);
                        /*
                        parent.fn_mdl_confirma(
                        "¿Desea generar una Nueva Versión de la Cotización que está actualizando?"
                        , function() { fn_ejecutaGrabar(1, strEnviarSupervisor, strEnviar); }
                        , "Util/images/question.gif"
                        , function() { fn_ejecutaGrabar(0, strEnviarSupervisor, strEnviar); }
                        , "VERSION COTIZACION"
                        );
                        */
                    }
                    , "Util/images/question.gif"
                    , function() { parent.fn_unBlockUI(); }
                    , "CONFIRMACION"
                );

            } else {

                parent.fn_mdl_confirma(
                    "¿Desea grabar los cambios en la Cotización?"
                    , function() {


                        parent.fn_mdl_confirma(
                            "¿Desea generar una Nueva Versión de la Cotización que está actualizando?"
                            , function() { fn_ejecutaGrabar(1, strEnviarSupervisor, strEnviar); }
                            , "Util/images/question.gif"
                            , function() { fn_ejecutaGrabar(0, strEnviarSupervisor, strEnviar); }
                            , "VERSION COTIZACION"
                        );


                    }
                    , "Util/images/question.gif"
                    , function() { }
                    , "CONFIRMACIÓN"
                );



            }

        }

    }

}


//************************************************************
// Función		:: 	fn_ejecutaGrabar
// Descripcion 	:: 	Método que graba
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_ejecutaGrabar(pstrGeneraVersion, pstrEnviarSupervisor, pstrEnviar) {

    var arrParametros = [
    //Cabecera
            "pstrTipoTransaccion", $("#hddTipoTransaccion").val(),
            "pstrNumeroCotizacion", $("#txtNumeroCotizacion").val(),
            "pstrEstado", $("#cmbEstado").val(),
            "pstrGeneraCarta", $("#hddGeneraCarta").val(),
            "pstrValidaCliente", $("#hddValidaCliente").val(),
            "pstrCUCliente", $("#txtCUCliente").val(),
            "pstrNombreCliente", $("#txtNombreCliente").val(),
            "pstrTipoPersona", $("#cmbTipoPersona").val(),
            "pstrTipoDocumento", $("#cmbTipoDocumento").val(),
            "pstrNumeroDocumento", $("#txtNumeroDocumento").val(),
            "pstrValidaLinea", $("#hddValidaLinea").val(),
            "pstrTasaLinea", $("#hddTasaLinea").val(),
            "pstrLinea", $("#cmbLinea").val(),
            "pstrEjecutivoBanca", $("#cmbEjecutivoBanca").val(),
            "pstrBancaAtencion", $("#cmbBancaAtencion").val(),
            "pstrZonal", $("#cmbZonal").val(),
            "pstrContacto", $("#txtContacto").val(),
            "pstrCorreo", $("#txtCorreo").val(),
            "pstrEjecutivoLeasing", $("#cmbEjecutivoLeasing").val(),
    //DatosGenerales :: Cotizacion
            "pstrProdFinanActivo", $("#hddProdFinanActivo").val(),
            "pstrProdFinanPasivo", $("#hddProdFinanPasivo").val(),
            "pstrTipoContrato", $("#cmbTipoContrato").val(),
            "pstrSubTipoContrato", $("#cmbSubTipoContrato").val(),
            "pstrMoneda", $("#cmbMoneda").val(),
            "pstrprocedencia", $("#cmbprocedencia").val(),
            "pstrClasificacionBien", $("#cmbClasificacionBien").val(),
            "pstrTipoBien", $("#cmbTipoBien").val(),
            "pstrTipoInmueble", "", //OJOOOOOOOOO!!!!!!!!!!!!!!!!!!!!
            "pstrEstadoBien", $("#cmbEstadoBien").val(),
            "pstrPrecioVenta", $("#txtPrecioVenta").val(),
            "pstrMontoIGV", $("#txtMontoIGV").val(),
            "pstrValorVenta", $("#txtValorVenta").val(),
            "pstrCuotaInicial", $("#txtCuotaInicial").val(),
            "pstrCuotaInicialPorc", $("#txtCuotaInicialPorc").val(),
            "pstrRiesgoNeto", $("#txtRiesgoNeto").val(),
    //DatosGenerales :: Cronograma
            "pstrTipoCronograma", $("#cmbTipoCronograma").val(),
            "pstrNroCuotas", $("#txtNroCuotas").val(),
            "pstrPeriodicidad", $("#cmbPeriodicidad").val(),
            "pstrFrecuenciaPago", $("#cmbFrecuenciaPago").val(),
            "pstrPlazoGracia", $("#txtPlazoGracia").val(),
            "pstrTipoGracia", $("#cmbTipoGracia").val(),
            "pstrFechavence", Fn_util_DateToString($("#txtFechavence").val()),
    //DatosGenerales :: Tasas
            "pstrTea", $("#txtTEA").val(),
            "pstrCostoFondos", $("#txtCostoFondos").val(),
            "pstrSpread", $("#txtSpread").val(),
            "pstrPrecuota", $("#txtPrecuota").val(),
            "pstrPlazoGraciaPrecuota", $("#txtPlazoGraciaPrecuota").val(),
    //DatosGenerales :: Comsiones
            "pstrOpcionCompraPorc", $("#txtOpcionCompraPorc").val(),
            "pstrOpcionCompraMonto", $("#txtOpcionCompraMonto").val(),
            "pstrComisionActivacionProc", $("#txtComisionActivacionProc").val(),            
            "pstrComisionActivacionMonto", $("#txtComisionActivacionMonto").val(),
            "pstrComisionEstructuracionPorc", $("#txtComisionEstructuracionPorc").val(),
            "pstrComisionEstructuracionMonto", $("#txtComisionEstructuracionMonto").val(),
    //DatosGenerales :: SeguroBien
            "pstrTipoBienSeguro", $("#cmbTipoBienSeguro").val(),
            "pstrImportePrimaSeguroBien", $("#txtImportePrimaSeguroBien").val(),
            "pstrNumCuotasfinanciadas", $("#txtNumCuotasfinanciadas").val(),
    //DatosGenerales :: SeguroDegravamen
            "pstrTipoSeguro", $("#cmbTipoSeguro").val(),
            "pstrImportePrimaDesgravamen", $("#txtImportePrimaDesgravamen").val(),
            "pstrNumCuotaFinanciar", $("#txtNumCuotaFinanciar").val(),
    //Opciones
            "pstrMostrarTea", $("#hddMostrarTea").val(),
            "pstrMostrarComision", $("#hddMostrarComision").val(),
            "pstrFechaIngreso", Fn_util_DateToString($("#txtFechaIngreso").val()),
            "pstrFechaOfertaValida", Fn_util_DateToString($("#txtFechaOfertaValida").val()),
            "pstrPeriodoDisponibilidad", $("#txtPeriodoDisponibilidad").val(),
            "pstrFechaMaxActivacion", Fn_util_DateToString($("#txtFechaMaxActivacion").val()),
            "pstrOtrasComisiones", $("#txaOtrasComisiones").val(),
            "pstrProveedores", $("#txtProveedores").val(),
    //Nuevos
            "pstrCodSuprestatario", $("#hddCodSuprestatario").val(),
            "pstrCodigoContacto", $("#hddCodigoContacto").val(),
            "pstrAplicaVersion", pstrGeneraVersion,
            "pstrEnviar", pstrEnviarSupervisor,
            "pstrVersion", $("#hddVersionCotizacion").val(),
    //Mas
            "pstrDesEjecutivoBanca", $("#txtEjecutivoBanca").val(),
            "pstrDesZonal", $("#txtZonal").val(),
            "pstrPorcIGV", $("#txtPorcIGV").val(),
            "pstrTipoCuota", $("#hddTipoCuota").val(),
            "pstrDireccionCliente", $("#hddDireccionCliente").val(),

    //inicio IBK - RPH
            "pstrOpcionCompra", $("#hdOpCompra").val(),
            "pstrComisionAct", $("#hdComiAct").val(),
            "pstrComisionEstr", $("#hdComEstruc").val()
        ];


    fn_util_AjaxWM("frmCotizacionRegistro.aspx/GuardarCotizacion",
                 arrParametros,
                 function(result) {
                     parent.fn_unBlockUI();
                     if (fn_util_trim(result) == "0") {
                         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL GUARDAR");
                     } else {
                         if ($("#hddTipoTransaccion").val() == "NUEVO") {
                             parent.fn_mdl_mensajeOk("Se grabó correctamente los datos. Se generó la Cotización Nº" + fn_util_trim(result), function() { fn_RedireccionGrabar(result, pstrEnviar) }, "GRABADO CORRECTO");
                             //IBK - RPH 
                             var intCantCronograma = $("#jqGrid_lista_C").getGridParam("reccount");
                             if (intCantCronograma > 0) {
                                 fn_GenerarArchivoCronograma(fn_util_trim(result)); //Crea archivo cronograma
                             }
                             //Fin
                         } else {
                             parent.fn_mdl_mensajeOk("Se actualizó correctamente los datos de la Cotización Nº" + fn_util_trim(result), function() { fn_RedireccionGrabar(result, pstrEnviar) }, "GRABADO CORRECTO");
                             //IBK - RPH
                             var intCantCronograma = $("#jqGrid_lista_C").getGridParam("reccount");
                             if (intCantCronograma > 0) {
                                 if ($("#hddGeneraCronograma").val() == "1") {
                                     fn_GenerarArchivoCronograma(fn_util_trim(result)); //Crea archivo cronograma
                                 }
                             }
                             //Fin       
                         }
                     }
                 },
                 function(resultado) {
                     parent.fn_unBlockUI();
                     var error = eval("(" + resultado.responseText + ")");
                     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABADO");
                 }
        );


}
function fn_RedireccionGrabar(result, pstrEnviar) {
    parent.fn_blockUI();
    //Inicio IBK
    /*if (parseInt(pstrEnviar)>0) {
        window.location = "frmCotizacionListado.aspx";
    }else{*/
    if (parseInt(pstrEnviar) > 0) {
        if ($('#hddValidaEnviar').val() == '1' && $("#hddGeneraCarta").val() == "1") {
            window.location = "frmCotizacionListado.aspx?cod=" + fn_util_trim(result);
        }
        else {
            //alert('Ronlad3333333');
            //IBK - RPH
            if (($("#cmbEstado").val() == strEstadoCotizacionIngresado) || ($("#cmbEstado").val() == strEstadoCotizacionPendCarta)) {
                $("#hddGeneraCronograma").val("0");
                window.location = "frmCotizacionRegistro.aspx?hddCodigoCotizacion=" + fn_util_trim(result);
            }
            else {

                $("#hddGeneraCronograma").val("0");
                window.location = "frmCotizacionListado.aspx";
            }
        }

    } else {
        //Inicio IBK - RPH
        $("#hddGeneraCronograma").val("0");
        //Fin IBK
        window.location = "frmCotizacionRegistro.aspx?hddCodigoCotizacion=" + fn_util_trim(result);
    }
}

//Inicio IBK - RPH Archivo Cronograma
function fn_GenerarArchivoCronograma(pstrCodigoCotizacion) {
    //debugger;
    var sRuta;
    var arrParametros = ["pstrCodigoCotizacion", pstrCodigoCotizacion];
    fn_util_AjaxWM("frmCotizacionRegistro.aspx/GenerarArchivoCronograma",
                    arrParametros,
                    fn_ResultadoGenerarCotizacion,
                    function(result) {
                    });
}

function fn_ResultadoGenerarCotizacion(result) {
    var vResult = result.split('|');

    if (vResult[0] == "0") {
        var strRutaArchivo = vResult[1];
        var strNombreArchivo = vResult[1].split('\\').pop();
        strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
        $("#hddAdjuntarArchivo").val(strRutaArchivo);

        $("#dv_DescargarArchivoCronograma").show();
        $("#dv_DescargarArchivoCronograma").html("<a href='#'  onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(strRutaArchivo) + "');return false;\">" + strNombreArchivo + "</a>");
    }
}

//Fin

//****************************************************************
// Funcion		:: 	fn_validaActivacionClienteEditar
// Descripción	::	Valida si el cliente viene de RM o es EDITAR
// Log			:: 	JRC - 08/05/2012
//****************************************************************
function fn_validaActivacionClienteEditar(pblnActiva) {

    if (pblnActiva) {

        //$("#chkValidaCliente").hide();

        $('input[name=chkLinea]').attr('checked', false);
        $('#cmbLinea').attr('class', 'css_select_inactivo');
        $('#cmbLinea').attr('disabled', 'disabled');
        $("#imgBusqLinea").hide();
        $('#chkLinea').attr('disabled', 'disabled');

        $('#txtCUCliente').removeClass('css_input css_input_obligatorio').addClass('css_input_inactivo');
        $('#txtCUCliente').prop('readonly', true);

        $('#txtNombreCliente').removeClass('css_input_inactivo').addClass('css_input_obligatorio');
        $('#txtNombreCliente').prop('readonly', false);

        $('#txtNumeroDocumento').removeClass('css_input_inactivo').addClass('css_input_obligatorio');
        $('#txtNumeroDocumento').prop('readonly', false);

        $('#cmbTipoPersona').attr('class', 'css_select css_select_obligatorio');
        $('#cmbTipoPersona').removeAttr('disabled');
        $('#cmbTipoDocumento').attr('class', 'css_select css_select_obligatorio');
        $('#cmbTipoDocumento').removeAttr('disabled');
        $('#cmbEjecutivoBanca').attr('class', 'css_select css_select_obligatorio');
        $('#cmbEjecutivoBanca').removeAttr('disabled');

        $('#imgBsqClienteRM').hide();

        $('#cmbBancaAtencion').attr('class', 'css_select css_select_obligatorio');
        $('#cmbBancaAtencion').removeAttr('disabled');

        $('#cmbEjecutivoLeasing').attr('class', 'css_select css_select_obligatorio');
        $('#cmbEjecutivoLeasing').removeAttr('disabled');

    } else {

        $("#chkValidaCliente").hide();

        if (fn_util_trim($("#cmbEstado").val()) == strEstadoCotizacion_PendF10) {
            if (fn_util_trim($('#txtCUCliente').val()) == '00000000' || fn_util_trim($('#txtCUCliente').val()) == '') {
                $("#cmbTipoDocumento").html(strCmbTipoDocumento);
                $('#imgBsqClienteRM').show();
                $('#txtCUCliente').addClass('css_input css_input_obligatorio').removeClass('css_input_inactivo');
                $('input[name=chkValidaCliente]').attr('checked', true);
            } else {
                $('#imgBsqClienteRM').hide();
                $('#txtCUCliente').addClass('css_input_inactivo').removeClass('css_input css_input_obligatorio');
                $('#txtCUCliente').prop('readonly', true);
            }
        }
        else {
            $('#imgBsqClienteRM').hide();
            $('#txtCUCliente').addClass('css_input_inactivo').removeClass('css_input css_input_obligatorio');
            $('#txtCUCliente').prop('readonly', true);
        }

        $('#txtNombreCliente').attr('class', 'css_input_inactivo');
        $('#txtNombreCliente').prop('readonly', true);

        $('#txtNumeroDocumento').attr('class', 'css_input_inactivo');
        $('#txtNumeroDocumento').prop('readonly', true);

        $('#cmbTipoPersona').attr('class', 'css_select_inactivo');
        $('#cmbTipoPersona').attr('disabled', 'disabled');
        $('#cmbTipoDocumento').attr('class', 'css_select_inactivo');
        $('#cmbTipoDocumento').attr('disabled', 'disabled');
        $('#cmbEjecutivoBanca').attr('class', 'css_select_inactivo');
        $('#cmbEjecutivoBanca').attr('disabled', 'disabled');

        $('#cmbBancaAtencion').attr('class', 'css_select_inactivo');
        $('#cmbBancaAtencion').attr('disabled', 'disabled');

        //$('#cmbEjecutivoLeasing').attr('class', 'css_select_inactivo');
        //$('#cmbEjecutivoLeasing').attr('disabled', 'disabled');

    }

}


//****************************************************************
// Funcion		:: 	fn_seteaCheckLinea
// Descripción	::	fn_seteaCheckLinea
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_seteaCheckLinea(booCheck) {

    $("#hddTasaLinea").val("");
    if (booCheck) {
        $("#hddValidaLinea").val("1");
        $('#cmbLinea').attr('class', 'css_select css_select_obligatorio');
        $('#cmbLinea').removeAttr('disabled');
        $("#imgBusqLinea").show();
        /*if ($("#cmbTipoContrato").val() == strTipoContratoLeaseback) {
        $("#cmbTipoContrato").val("0");
        parent.fn_util_MuestraLogPage("El Tipo de Contrato no puede ser LEASING BACK porque se eligió una linea", "I");
        }*/
    }
    else {
        $('#cmbLinea').val("0");
        $("#hddValidaLinea").val("0");
        $('#cmbLinea').attr('class', 'css_select_inactivo');
        $('#cmbLinea').attr('disabled', 'disabled');
        $("#imgBusqLinea").hide();
    }

}


//************************************************************
// Función		:: 	fn_oc_clasificacionBien
// Descripcion 	:: 	Método Clasificacion
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_oc_clasificacionBien(strValor) {

    var strEstadoBien = $("#cmbEstadoBien").val();

    if (strValor == strClasifacionBienInmueble) {
        if (strEstadoBien == strEstadoBienNuevo) {
            $('#txtPorcIGV').val(fn_util_ValidaMonto(strPorcIGVInmueble, 2));
            $('#txtPorcIGV').addClass('css_input').removeClass('css_input_inactivo');
            $('#txtPorcIGV').prop('readonly', false);
        } else {
            $('#txtPorcIGV').val(fn_util_ValidaMonto("0", 2));
            $('#txtPorcIGV').addClass('css_input_inactivo').removeClass('css_input');
            $('#txtPorcIGV').prop('readonly', true);
        }
        $('#cmbprocedencia').val(strProcedenciaLocal);
    } else {
        $('#txtPorcIGV').val(fn_util_ValidaMonto($('#hddIGV').val(), 2));
        $('#txtPorcIGV').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtPorcIGV').prop('readonly', true);
        //$("#cmbprocedencia").val(strProcedenciaLocal);
    }

    var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL104", "pstrCodigoGenerico", strValor];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbTipoBien').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }





}


//************************************************************
// Función		:: 	fn_oc_periodicidad
// Descripcion 	:: 	Método Clasificacion
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_oc_periodicidad(strValor) {

    //var arrValor = strValor.split("*");

    var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL033", "pstrCodigoGenerico", strValor];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbFrecuenciaPago').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}


//************************************************************
// Función		:: 	fn_oc_bancaAtencion
// Descripcion 	:: 	Método Clasificacion
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_oc_bancaAtencion(strValor) {

    var arrParametros = ["pstrOp", "7", "pstrDominio", "TBL168", "pstrParametro", strValor];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $("#hddSpreadBanca").val(arrResultado[2]);
            $("#imgSpreadBanca").show();
            $("#imgSpreadBanca").attr("alt", "Spread mínimo de la Banca :" + arrResultado[2]);
            $("#imgSpreadBanca").attr("title", "Spread mínimo de la Banca :" + arrResultado[2]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}


//************************************************************
// Función		:: 	fn_of_PrecioVenta
// Descripcion 	:: 	Metodo onFocus Precio Venta
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_of_PrecioVenta() {

    var decPrecioVenta = fn_util_ValidaDecimal($('#txtPrecioVenta').val());
    var decIGV = fn_util_ValidaDecimal($('#txtPorcIGV').val());
    var decMontoIGV = (decPrecioVenta / (1 + (decIGV / 100))) * (decIGV / 100);

    var decValorVenta = decPrecioVenta - decMontoIGV;
    $('#txtPrecioVenta').val(fn_util_ValidaMonto(decPrecioVenta, 2));
    $('#txtMontoIGV').val(fn_util_ValidaMonto(decMontoIGV, 2));
    $('#txtValorVenta').val(fn_util_ValidaMonto(decValorVenta, 2));

    $("#txtComisionActivacionMonto").val(fn_util_ValidaMonto($('#hddComisionActivacion').val(), 2));
    $("#txtComisionActivacionProc").val(fn_util_ValidaMonto("0", 2));    


    fn_of_ComisionActivacionMonto();
    fn_of_OpcionCompraPorc();
    fn_of_ComisionEstructuracionPorc();
}


//************************************************************
// Función		:: 	fn_of_CuotaInicial
// Descripcion 	:: 	Metodo Calculo Cuota y % inicial
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_of_CuotaInicial() {
    var decValorVenta = fn_util_ValidaDecimal($('#txtValorVenta').val());
    var decMontoInicial = fn_util_ValidaDecimal($('#txtCuotaInicial').val());
    var decMontoPorc = 0;

    if (decMontoInicial >= decValorVenta) {
        $('#txtCuotaInicialPorc').val(fn_util_ValidaMonto("0", 2));
        $('#txtCuotaInicial').val(fn_util_ValidaMonto("0", 2));
        parent.fn_util_MuestraLogPage("La cuota Inicial no puede ser Mayor o Igual al Valor de Venta", "E");
    } else {
        if (decValorVenta <= 0) decValorVenta = 1;
        decMontoPorc = (decMontoInicial / decValorVenta) * 100;
        $('#txtCuotaInicial').val(fn_util_ValidaMonto(decMontoInicial, 2));
        $('#txtCuotaInicialPorc').val(fn_util_ValidaMonto(decMontoPorc, 2));
    }
}


//************************************************************
// Función		:: 	fn_of_CuotaInicialPorc
// Descripcion 	:: 	Metodo Calculo Cuota y % inicial
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_of_CuotaInicialPorc() {
    var decValorVenta = fn_util_ValidaDecimal($('#txtValorVenta').val());
    var decPorcInicial = fn_util_ValidaDecimal($('#txtCuotaInicialPorc').val());
    var decMonto = 0;

    if (decPorcInicial >= 100) {
        $('#txtCuotaInicial').val(fn_util_ValidaMonto("0", 2));
        $('#txtCuotaInicialPorc').val(fn_util_ValidaMonto("0", 2));
        parent.fn_util_MuestraLogPage("El Porcentaje de la Cuota Inicial ingresado no es correcto", "E");
    } else {
        decMonto = decValorVenta * (decPorcInicial / 100);
        $('#txtCuotaInicialPorc').val(fn_util_ValidaMonto(decPorcInicial, 2));
        $('#txtCuotaInicial').val(fn_util_ValidaMonto(decMonto, 2));
    }
}


//************************************************************
// Función		:: 	fn_of_calculaNeto
// Descripcion 	:: 	Metodo CalculaNeto
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_of_calculaNeto() {
    var decValorVenta = fn_util_ValidaDecimal($('#txtValorVenta').val());
    var decMontoInicial = fn_util_ValidaDecimal($('#txtCuotaInicial').val());
    var decNeto = decValorVenta - decMontoInicial;
    $('#txtRiesgoNeto').val(fn_util_ValidaMonto(decNeto, 2));
}


//************************************************************
// Función		:: 	fn_of_calculaSpread
// Descripcion 	:: 	Metodo para calcular el Spread
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_of_calculaSpread() {
    var decTEA = fn_util_ValidaDecimal($("#txtTEA").val());
    var decCostoFondos = fn_util_ValidaDecimal($("#txtCostoFondos").val());
    var decSpread = decTEA - decCostoFondos;
    decSpread = fn_util_RedondearDecimales(decSpread, 2);

    $('#txtSpread').val(fn_util_ValidaMonto(decSpread, 2));
    //alert(decTEA + "-" + decCostoFondos + "=" + decSpread+"("+$('#txtSpread').val()+")");

    //Valida Spread
    var decSpreadBanca = fn_util_ValidaDecimal($("#hddSpreadBanca").val());

    //alert(decSpread + "<" + decSpreadBanca);
    if (decSpread < decSpreadBanca) {
        parent.fn_util_MuestraLogPage("El Spread calculado es menor al Spread de Banca. Se enviará al Supervisor.", "I");
    }
}



//************************************************************
// Función		:: 	fn_of_OpcionCompraPorc
// Descripcion 	:: 	Metodo Opcion Compra Porc
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_of_OpcionCompraPorc() {
    var decPrecioVenta = fn_util_ValidaDecimal($('#txtPrecioVenta').val());
    var decOpcionCompraPorc = fn_util_ValidaDecimal($('#txtOpcionCompraPorc').val());
    if (decOpcionCompraPorc >= 100) {
        $('#txtOpcionCompraPorc').val(fn_util_ValidaMonto("0", 2));
        parent.fn_util_MuestraLogPage("El Porcentaje de Opción de Compra ingresado no incorrecto", "E");
    } else {
        var decCalculo = decPrecioVenta * (decOpcionCompraPorc / 100);
        $('#txtOpcionCompraMonto').val(fn_util_ValidaMonto(decCalculo, 2));
    }
}


//************************************************************
// Función		:: 	fn_of_ComisionActivacionMonto
// Descripcion 	:: 	Metodo Comision Activacion Monto
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_of_ComisionActivacionMonto() {

    var decPrecioVenta = fn_util_ValidaDecimal($('#txtPrecioVenta').val());
    var decComisionActivacionMonto = fn_util_ValidaDecimal($('#txtComisionActivacionMonto').val());
    if (decComisionActivacionMonto >= decPrecioVenta) {
        $('#txtComisionActivacionMonto').val(fn_util_ValidaMonto("0", 2));
        //parent.fn_util_MuestraLogPage("El monto de la Comisión de Activación ingresada no puede ser mayor al Precio de Venta", "E");
    } else {
        var decCalculo = (decComisionActivacionMonto / decPrecioVenta) * 100;
        $('#txtComisionActivacionProc').val(fn_util_ValidaMonto(decCalculo, 2));
    }
}


//************************************************************
// Función		:: 	fn_of_ComisionEstructuracionPorc
// Descripcion 	:: 	Metodo Comision Estructuracion Porc
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_of_ComisionEstructuracionPorc() {
    var decPrecioVenta = fn_util_ValidaDecimal($('#txtPrecioVenta').val());
    var decComisionEstructuracionPorc = fn_util_ValidaDecimal($('#txtComisionEstructuracionPorc').val());
    if (decComisionEstructuracionPorc >= 100) {
        $('#txtComisionEstructuracionPorc').val(fn_util_ValidaMonto("0", 2));
        parent.fn_util_MuestraLogPage("El Porcentaje de la Comisión de Estructuración ingresado es incorrecto", "E");
    } else {
        var decCalculo = decPrecioVenta * (decComisionEstructuracionPorc / 100);
        $('#txtComisionEstructuracionMonto').val(fn_util_ValidaMonto(decCalculo, 2));
    }
}


//************************************************************
// Función		:: 	fn_validaTipoPersona
// Descripcion 	:: 	Valida el tipo de Persona
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_validaTipoPersona(strValor) {

    if (strValor == strTipoPersonaNatural) {
        $('#fld_SeguroDegravamen').show();
        $("#cmbTipoSeguro").val(strSeguroDegravamenTipoInterno);
        $("#cmbTipoSeguro").select();
        fn_validaTipoSeguroBien(strSeguroDegravamenTipoInterno, $('#txtNroCuotas').val());

        $('#cmbTipoDocumento').html(strCmbTipoDocumento);
        $("#cmbTipoDocumento option[value='2']").remove();
        $("#cmbTipoDocumento option[value='4']").remove();

    }
    else if (strValor == strTipoPersonaJuridica) {
        $('#fld_SeguroDegravamen').hide();
        $("#cmbTipoSeguro").val("0");
        $("#cmbTipoSeguro").select();
        $("#txtImportePrimaDesgravamen").val("");
        $("#txtNumCuotaFinanciar").val("");

        $('#cmbTipoDocumento').html(strCmbTipoDocumento);
        $("#cmbTipoDocumento option[value='1']").remove();
        $("#cmbTipoDocumento option[value='3']").remove();
        $("#cmbTipoDocumento option[value='4']").remove();
        $("#cmbTipoDocumento option[value='5']").remove();

    }
    else {
        $('#fld_SeguroDegravamen').show();
        $("#cmbTipoSeguro").val("0");
    }
    fn_doResize();

}


//************************************************************
// Función		:: 	fn_validaTipoPersonaEditar
// Descripcion 	:: 	Valida el tipo de Persona
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_validaTipoPersonaEditar(strValor) {

    if (strValor == strTipoPersonaNatural) {
        $('#fld_SeguroDegravamen').show();
        //$("#cmbTipoSeguro").val(strSeguroDegravamenTipoInterno);
        //fn_validaTipoSeguroBien(strSeguroDegravamenTipoInterno, $('#txtNroCuotas').val());

        $('#cmbTipoDocumento').html(strCmbTipoDocumento);
        $("#cmbTipoDocumento option[value='2']").remove();
        $("#cmbTipoDocumento option[value='4']").remove();

    }
    else if (strValor == strTipoPersonaJuridica) {
        $('#fld_SeguroDegravamen').hide();
        //$("#cmbTipoSeguro").val("0");
        //$("#txtImportePrimaDesgravamen").val("");
        //$("#txtNumCuotaFinanciar").val("");

        $('#cmbTipoDocumento').html(strCmbTipoDocumento);
        $("#cmbTipoDocumento option[value='1']").remove();
        $("#cmbTipoDocumento option[value='3']").remove();
        $("#cmbTipoDocumento option[value='4']").remove();
        $("#cmbTipoDocumento option[value='5']").remove();

    }
    else {
        $('#fld_SeguroDegravamen').show();
        $("#cmbTipoSeguro").val("0");
    }
    fn_doResize();

}


//************************************************************
// Función		:: 	fn_validaTipoSeguroBien
// Descripcion 	:: 	Valida el tipo de Seguro Bien
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_validaTipoSeguroBien(strValor, intNroCuotas) {
    if (strValor == strSeguroBienTipoInterno) {
        $('#txtNumCuotasfinanciadas').val(intNroCuotas);
        $('#txtNumCuotasfinanciadas').removeClass('css_input_inactivo').addClass('css_input_obligatorio');
        $('#txtNumCuotasfinanciadas').prop('readonly', false);
        $('#txtImportePrimaSeguroBien').removeClass('css_input_inactivo').addClass('css_input_obligatorio ui-edit-align-right');
        $('#txtImportePrimaSeguroBien').prop('readonly', false);
    } else {
        $('#txtNumCuotasfinanciadas').val("");
        $('#txtNumCuotasfinanciadas').attr('class', 'css_input_inactivo');
        $('#txtNumCuotasfinanciadas').prop('readonly', true);
        $('#txtImportePrimaSeguroBien').attr('class', 'css_input_inactivo ui-edit-align-right');
        $('#txtImportePrimaSeguroBien').prop('readonly', true);
        $("#hddMontoTemporal").validNumber({ value: "0" });
        $('#txtImportePrimaSeguroBien').val($("#hddMontoTemporal").val());
    }
}


//************************************************************
// Función		:: 	fn_validaTipoSeguroDegravamen
// Descripcion 	:: 	Valida el tipo de Seguro Degravamen
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_validaTipoSeguroDegravamen(strValor, intNroCuotas) {
    if (strValor == strSeguroDegravamenTipoInterno) {
        $('#txtNumCuotaFinanciar').val(intNroCuotas);
        $('#txtNumCuotaFinanciar').removeClass('css_input_inactivo').addClass('css_input_obligatorio');
        $('#txtNumCuotaFinanciar').prop('readonly', false);
        $('#txtImportePrimaDesgravamen').removeClass('css_input_inactivo').addClass('css_input_obligatorio ui-edit-align-right');
        $('#txtImportePrimaDesgravamen').prop('readonly', false);
    } else {
        $('#txtNumCuotaFinanciar').val("");
        $('#txtNumCuotaFinanciar').attr('class', 'css_input_inactivo');
        $('#txtNumCuotaFinanciar').prop('readonly', true);
        $('#txtImportePrimaDesgravamen').attr('class', 'css_input_inactivo ui-edit-align-right');
        $('#txtImportePrimaDesgravamen').prop('readonly', true);
        $("#hddMontoTemporal").validNumber({ value: "0" });
        $('#txtImportePrimaDesgravamen').val($("#hddMontoTemporal").val());
    }
}


//************************************************************
// Función		:: 	fn_validaMostrarComision
// Descripcion 	:: 	Valida RadioButton MostrarComision
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_validaMostrarComision(strValor) {
    if (strValor == "1") {
        $('#hddMostrarComision').val("1");
        $('input[id=rdbMostrarComisionSI]').attr('checked', true);
    } else {
        $('#hddMostrarComision').val("0");
        $('input[id=rdbMostrarComisionNO]').attr('checked', true);
    }
}


//************************************************************
// Función		:: 	fn_validaMostrarTea
// Descripcion 	:: 	Valida RadioButton TEA
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_validaMostrarTea(strValor) {
    if (strValor == "1") {
        $('#hddMostrarTea').val("1");
        $('input[id=rdbMostrarTeaSI]').attr('checked', true);
    } else {
        $('#hddMostrarTea').val("0");
        $('input[id=rdbMostrarTeaNO]').attr('checked', true);
    }
}


//************************************************************
// Función		:: 	fn_validaEnviarCotizacion
// Descripcion 	:: 	Valida El estado de Envia Cotizacion
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_validaEnviarCotizacion(strEnviar) {
    var strEnviarEstado = "0";
    if (strEnviar == "1") {

        //Valida Spread
        var decSpreadBanca = fn_util_ValidaDecimal($('#hddSpreadBanca').val());
        var decSpread = fn_util_ValidaDecimal($('#txtSpread').val());
        //alert(decSpread + "<" + decSpreadBanca);  
        if (decSpread < decSpreadBanca) {
            strEnviarEstado = "1";
        }

        //Valida %Precuota
        var decTEA = fn_util_ValidaDecimal($('#txtTEA').val());
        var decPrecuota = fn_util_ValidaDecimal($('#txtPrecuota').val());
        //alert(decPrecuota + "<" + decTEA);
        if (decPrecuota < decTEA) {
            strEnviarEstado = "1";
        }

        //Valida Comision Activacion
        var decComActivacion = fn_util_ValidaDecimal($('#hddComisionActivacion').val());
        var decMontoComActivacion = fn_util_ValidaDecimal($('#txtComisionActivacionMonto').val());
        //alert(decMontoComActivacion + "<" + decComActivacion);
        if (decMontoComActivacion < decComActivacion) {
            strEnviarEstado = "1";
        }
        //alert(strEnviarEstado);
        return strEnviarEstado;

    }
    else if (strEnviar == "2") {
        return "2";
    }
    else if (strEnviar == "3") {
        return "3";
    }
    else {
        return "9";
    }

}


//****************************************************************
// Funcion		:: 	fn_cargaGrillaDocumento 
// Descripción	::	Abre Modal de Motivo de Rechazo
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrillaDocumento() {

    try {

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"), // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
                             "pstrNroCotizacion", $("#hddCodigoCotizacion").val()
                            ];

        fn_util_AjaxWM("frmCotizacionRegistro.aspx/ListadoCotizacionDocumento",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);
                    fn_doResize();
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

    } catch (ex) {
        fn_util_alert(ex.message);
    }

}


//****************************************************************
// Funcion		:: 	fn_iniGrillaDocumento 
// Descripción	::	Inicializa Grilla Documento
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_iniGrillaDocumento() {

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() { },
        jsonReader:
        {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Codigo', 'Nombre Archivo', 'Adjunto', 'Comentario'],
        colModel: [
                { name: 'CodigoDocumento', index: 'CodigoDocumento', hidden: true },
		        { name: 'NombreArchivo', index: 'NombreArchivo', width: 200, align: "left", sorttype: "string", defaultValue: "" },
		        { name: 'RutaArchivo', index: 'RutaArchivo', width: 100, align: "Center", sortable: false, formatter: fn_icoDownload },
		        { name: 'Comentario', index: 'Comentario', width: 550, align: "left", sorttype: "string", defaultValue: "" }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'Codigocotizacion',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodigoDocumento").val(rowData.CodigoDocumento);
        },
        ondblClickRow: function(id) {
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

    //Abrir Archivo
    function fn_icoDownload(cellvalue, options, rowObject) {
        var strNombreArchivo = rowObject.RutaArchivo.split('\\').pop();
        strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
        if (fn_util_trim(rowObject.RutaArchivo) != "") {
            return "<img src='../Util/images/ico_download.gif' alt='" + strNombreArchivo + "' title='" + strNombreArchivo + "' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.RutaArchivo) + "');\" style='cursor:pointer;'/>";
        } else {
            return ".";
        }
    };

}





//****************************************************************
// Funcion		:: 	fn_generaCronograma
// Descripción	::	Activa los Tabs de la página
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_generaCronograma() {

    parent.fn_blockUI();

    //Limpia Grilla
    //$("#jqGrid_lista_C").GridUnload();
    //$("#jqGrid_lista_C").clearGridData(true);
    //$("#tbCronograma").hide();
    $("#jqGrid_lista_C").jqGrid("clearGridData", true);
    //$("#jqGrid_lista_C").trigger("reloadGrid");

    //String Validación
    var strError = new StringBuilderEx();

    //Declara        
    var objcmbTipoCronograma = $('select[id=cmbTipoCronograma]');
    var objtxtNroCuotas = $('input[id=txtNroCuotas]:text');
    var objcmbPeriodicidad = $('select[id=cmbPeriodicidad]');
    var objcmbFrecuenciaPago = $('select[id=cmbFrecuenciaPago]');
    var objtxtTEA = $('input[id=txtTEA]:text');
    var objcmbTipoBienSeguro = $('select[id=cmbTipoBienSeguro]');
    var objtxtPrecioVenta = $('input[id=txtPrecioVenta]:text');

    strError.append(fn_util_ValidateControl(objcmbTipoCronograma[0], 'un Tipo Cronograma válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtNroCuotas[0], 'un Nro. Cuotas válido', 1, ''));
    strError.append(fn_util_ValidateControl(objcmbPeriodicidad[0], 'un Periodicidad Neto válido', 1, ''));
    strError.append(fn_util_ValidateControl(objcmbFrecuenciaPago[0], 'una Frecuencia de Pago Neto válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtTEA[0], 'una TEA válida', 1, ''));
    strError.append(fn_util_ValidateControl(objcmbTipoBienSeguro[0], 'un Seguro de Bien válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtPrecioVenta[0], 'un Precio Venta válido', 1, ''));

    $("#txtNroCuotas").addClass("ui-edit-align-right");
    $("#txtTEA").addClass("ui-edit-align-right");
    $("#txtPrecioVenta").addClass("ui-edit-align-right");

    //Valida Seguro
    if ($("#cmbTipoBienSeguro").val() == strSeguroBienTipoInterno) {
        var decImporteSeguro = fn_util_ValidaDecimal($("#txtImportePrimaSeguroBien").val());
        var strCuotasSeguro = $("#txtNumCuotasfinanciadas").val();
        if (strCuotasSeguro == "") strCuotasSeguro = "0";

        if (decImporteSeguro <= 0) {
            strError.append('&nbsp;&nbsp;- El Importe Prima del Seguro debe ser mayor a cero<br />');
        }
        if (parseInt(strCuotasSeguro) <= 0) {
            strError.append('&nbsp;&nbsp;- Las Cuotas a Financiar del Seguro debe ser mayor a cero<br />');
        }
    }

    //Valida SeguroDegravamen
    if ($('#cmbTipoPersona').val() == strTipoPersonaNatural) {
        if ($("#cmbTipoSeguro").val() == strSeguroDegravamenTipoInterno) {
            var decImporteSeguroDes = fn_util_ValidaDecimal($("#txtImportePrimaDesgravamen").val());
            var strCuotasSegurodes = $("#txtNumCuotaFinanciar").val();
            if (strCuotasSegurodes == "") strCuotasSegurodes = "0";

            if (decImporteSeguroDes <= 0) {
                strError.append('&nbsp;&nbsp;- El Importe Prima del Seguro de Desgravamen debe ser mayor a cero<br />');
            }
            if (parseInt(strCuotasSegurodes) <= 0) {
                strError.append('&nbsp;&nbsp;- Las Cuotas a Financiar del Seguro de Desgravamen debe ser mayor a cero<br />');
            }
        }
    }

    //Valida Fecha Activacion
    var strMensajeFecActivacion = fn_validaFechaActivacion();
    if (fn_util_trim(strMensajeFecActivacion) != "") {
        strError.append(strMensajeFecActivacion);
    }

    //Valida si hay Error
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }
    else {

        if (!fn_validaCambioDataCronograma()) {//Datos han cambiado
            //alert("true");//Kina
            blnDatosCambiados = true;
        }

        //Inicializa Grilla
        fn_cargaGrillaCronograma();

        var arrParametros = [
        //Cabecera
            "pstrTipoTransaccion", $("#hddTipoTransaccion").val(),
            "pstrNumeroCotizacion", $("#txtNumeroCotizacion").val(),
            "pstrEstado", $("#cmbEstado").val(),
            "pstrGeneraCarta", $("#hddGeneraCarta").val(),
            "pstrValidaCliente", $("#hddValidaCliente").val(),
            "pstrCUCliente", $("#txtCUCliente").val(),
            "pstrNombreCliente", $("#txtNombreCliente").val(),
            "pstrTipoPersona", $("#cmbTipoPersona").val(),
            "pstrTipoDocumento", $("#cmbTipoDocumento").val(),
            "pstrNumeroDocumento", $("#txtNumeroDocumento").val(),
            "pstrValidaLinea", $("#hddValidaLinea").val(),
            "pstrTasaLinea", $("#hddTasaLinea").val(),
            "pstrLinea", $("#cmbLinea").val(),
            "pstrEjecutivoBanca", $("#cmbEjecutivoBanca").val(),
            "pstrBancaAtencion", $("#cmbBancaAtencion").val(),
            "pstrZonal", $("#cmbZonal").val(),
            "pstrContacto", $("#txtContacto").val(),
            "pstrCorreo", $("#txtCorreo").val(),
            "pstrEjecutivoLeasing", $("#cmbEjecutivoLeasing").val(),
        //DatosGenerales :: Cotizacion
            "pstrProdFinanActivo", $("#hddProdFinanActivo").val(),
            "pstrProdFinanPasivo", $("#hddProdFinanPasivo").val(),
            "pstrTipoContrato", $("#cmbTipoContrato").val(),
            "pstrSubTipoContrato", $("#cmbSubTipoContrato").val(),
            "pstrMoneda", $("#cmbMoneda").val(),
            "pstrprocedencia", $("#cmbprocedencia").val(),
            "pstrClasificacionBien", $("#cmbClasificacionBien").val(),
            "pstrTipoBien", $("#cmbTipoBien").val(),
            "pstrTipoInmueble", "", // => OJOOOOOOOOOOOOOOOOOOOOOOOOOOO!!!!!
            "pstrEstadoBien", $("#cmbEstadoBien").val(),
            "pstrPrecioVenta", $("#txtPrecioVenta").val(),
            "pstrMontoIGV", $("#txtMontoIGV").val(),
            "pstrValorVenta", $("#txtValorVenta").val(),
            "pstrCuotaInicial", $("#txtCuotaInicial").val(),
            "pstrCuotaInicialPorc", $("#txtCuotaInicialPorc").val(),
            "pstrRiesgoNeto", $("#txtRiesgoNeto").val(),
        //DatosGenerales :: Cronograma
            "pstrTipoCronograma", $("#cmbTipoCronograma").val(),
            "pstrNroCuotas", $("#txtNroCuotas").val(),
            "pstrPeriodicidad", $("#cmbPeriodicidad").val(),
            "pstrFrecuenciaPago", $("#cmbFrecuenciaPago").val(),
            "pstrPlazoGracia", $("#txtPlazoGracia").val(),
            "pstrTipoGracia", $("#cmbTipoGracia").val(),
            "pstrFechavence", Fn_util_DateToString($("#txtFechavence").val()),
        //DatosGenerales :: Tasas
            "pstrTea", $("#txtTEA").val(),
            "pstrCostoFondos", $("#txtCostoFondos").val(),
            "pstrSpread", $("#txtSpread").val(),
            "pstrPrecuota", $("#txtPrecuota").val(),
            "pstrPlazoGraciaPrecuota", $("#txtPlazoGraciaPrecuota").val(),
        //DatosGenerales :: Comsiones
            "pstrOpcionCompraPorc", $("#txtOpcionCompraPorc").val(),
            "pstrOpcionCompraMonto", $("#txtOpcionCompraMonto").val(),
            "pstrComisionActivacionProc", $("#txtComisionActivacionProc").val(),
            "pstrComisionActivacionMonto", $("#txtComisionActivacionMonto").val(),
            "pstrComisionEstructuracionPorc", $("#txtComisionEstructuracionPorc").val(),
            "pstrComisionEstructuracionMonto", $("#txtComisionEstructuracionMonto").val(),
        //DatosGenerales :: SeguroBien
            "pstrTipoBienSeguro", $("#cmbTipoBienSeguro").val(),
            "pstrImportePrimaSeguroBien", $("#txtImportePrimaSeguroBien").val(),
            "pstrNumCuotasfinanciadas", $("#txtNumCuotasfinanciadas").val(),
        //DatosGenerales :: SeguroDegravamen
            "pstrTipoSeguro", $("#cmbTipoSeguro").val(),
            "pstrImportePrimaDesgravamen", $("#txtImportePrimaDesgravamen").val(),
            "pstrNumCuotaFinanciar", $("#txtNumCuotaFinanciar").val(),
        //Opciones
            "pstrMostrarTea", $("#hddMostrarTea").val(),
            "pstrMostrarComision", $("#hddMostrarComision").val(),
            "pstrFechaIngreso", Fn_util_DateToString($("#txtFechaIngreso").val()),
            "pstrFechaOfertaValida", Fn_util_DateToString($("#txtFechaOfertaValida").val()),
            "pstrPeriodoDisponibilidad", $("#txtPeriodoDisponibilidad").val(),
            "pstrFechaMaxActivacion", Fn_util_DateToString($("#txtFechaMaxActivacion").val()),
            "pstrOtrasComisiones", $("#txaOtrasComisiones").val(),
            "pstrProveedores", $("#txtProveedores").val(),
        //Nuevos
            "pstrCodSuprestatario", $("#hddCodSuprestatario").val(),
            "pstrCodigoContacto", $("#hddCodigoContacto").val(),
            "pstrAplicaVersion", "0",
            "pstrEnviar", "9"
        ];


        fn_util_AjaxWM("frmCotizacionRegistro.aspx/GeneraCronograma",
                 arrParametros,
                 function(jsondata) {
                     parent.fn_unBlockUI();
                     if (jsondata.FlagError == 1) {
                         parent.fn_mdl_mensajeIco(jsondata.MsgError, "util/images/error.gif", "ERROR EN CRONOGRAMA");
                     } else {
                         $("#tbCronograma").show();
                         jqGrid_lista_C.addJSONData(jsondata);

                         $("#hddIniTipoCronograma").val($("#cmbTipoCronograma").val());
                         $("#hddIniNroCuotas").val($("#txtNroCuotas").val());
                         $("#hddIniPeriodicidad").val($("#cmbPeriodicidad").val());
                         $("#hddIniFrecuenciaPago").val($("#cmbFrecuenciaPago").val());
                         $("#hddIniTEA").val($("#txtTEA").val());
                         $("#hddIniTipoBienSeguro").val($("#cmbTipoBienSeguro").val());
                         $("#hddIniPrecioVenta").val($("#txtPrecioVenta").val());

                         $("#hddIniCuotaIni").val($("#txtCuotaInicial").val());
                         $("#hddIniCuotaIniPorc").val($("#txtCuotaInicialPorc").val());
                         $("#hddIniIGVPorc").val($("#txtPorcIGV").val());
                         $("#hddIniTipoGracia").val($("#cmbTipoGracia").val());
                         $("#hddIniFecVenc").val($("#txtFechavence").val());
                         $("#hddIniFecMaxAct").val($("#txtFechaMaxActivacion").val());

                         $("#hddIniImportePrimaSeguroBien").val($("#txtImportePrimaSeguroBien").val());
                         $("#hddIniNumCuotasfinanciadas").val($("#txtNumCuotasfinanciadas").val());

                     }
                     fn_doResize();
                 },
                 function(resultado) {
                     var error = eval("(" + resultado.responseText + ")");
                     parent.fn_mdl_mensajeIco("No se pudo cargar el Cronograma.", "util/images/error.gif", "ERROR EN CRONOGRAMA");
                 }
        );

        //Activa Tab
        $("#tab3-tab").css("display", "block");
        $("div#divTabs").tabs("enable", [3]);
        $("div#divTabs").tabs("select", [3]);

        //IBK - RPH Limpio para el nombre del archivo generado
        $("#dv_DescargarArchivoCronograma").html("");
        $("#hddGeneraCronograma").val("1");

        /*
        var gridCronograma = $('#jqGrid_lista_C');
        gridCronograma.jqGrid('showCol', gridCronograma.getGridParam("colModel")[7].name);
        gridCronograma.jqGrid('showCol', gridCronograma.getGridParam("colModel")[8].name);
        */
        //fn_validaColumnasCronograma();

    }



}



//****************************************************************
// Funcion		:: 	fn_abreSeguimiento 
// Descripción	::	Abre Siguimiento de Cotizacion 
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_abreSeguimiento() {
    var strCodigoCotizacion = $("#hddCodigoCotizacion").val();
    parent.fn_util_AbreModal("Cotización :: Seguimiento", "Cotizacion/frmCotizacionSeguimiento.aspx?hddCodigoCotizacion=" + strCodigoCotizacion, 700, 400, function() { });
}


//****************************************************************
// Funcion		:: 	fn_abreVersiones 
// Descripción	::	Abre Versiones de Cotizacion 
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_abreVersiones() {
    var strCodigoCotizacion = $("#hddCodigoCotizacion").val();
    parent.fn_util_AbreModal("Cotización :: Versiones", "Cotizacion/frmCotizacionVersion.aspx?hddCodigoCotizacion=" + strCodigoCotizacion, 980, 550, function() { });
}





//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrillaCronograma() {

    $("#jqGrid_lista_C").jqGrid({
        datatype: function() {
            fn_paginaCronograma();
        },
        jsonReader:
        {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['#', 'Fec.Venc.', 'Días', 'Saldo', 'Interés', 'Principal', 'Monto Cuota', 'Monto Cuota Seguro', 'Monto Cuota Seguro Desgrav.', 'IGV', 'Total a Pagar', ''],
        colModel: [
		        { name: 'Numerocuota', index: 'Numerocuota', width: 40, align: "center" },
		        { name: 'SFechavencimiento', index: 'Fechavencimiento', align: "center", formatter: Fn_util_ValidaFechaVacia },
		        { name: 'Cantdiascuota', index: 'Cantdiascuota', align: "center" },
		        { name: 'SMontosaldoadeudado', index: 'Montosaldoadeudado', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontointeresbien', index: 'Montointeresbien', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontoprincipalbien', index: 'Montoprincipalbien', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontototalcuota', index: 'Montototalcuota', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontocuotasegurobien', index: 'Montocuotasegurobien', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'SCuotaSeguroDes', index: 'CuotaSeguroDes', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontototalcuotaigv', index: 'Montototalcuotaigv', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'STotalapagar', index: 'Totalapagar', width: 120, align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'AA', index: 'AA', width: 7, align: "right" }
        ],
        width: glb_intWidthPantalla - 120,
        height: '100%',
        pager: '#jqGrid_pager_C',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 50,
        //rowList: [10, 20, 30],
        sortname: 'Codigocotizacion',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) { },
        ondblClickRow: function(id) { },
        gridComplete: function(id) {
            fn_validaColumnasCronograma();
        }
    });
    jQuery("#jqGrid_lista_C").jqGrid('navGrid', '#jqGrid_lista_C', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_C").hide();

}




//****************************************************************
// Funcion		:: 	fn_cargaGrillaCronograma
// Descripción	::	Abre Modal de Motivo de Rechazo
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_llenaGrillaCronograma() {

    try {

        var arrParametros = ["pstrNroCotizacion", $("#hddCodigoCotizacion").val(),
                             "pstrVersionCotizacion", $("#hddVersionCotizacion").val()
                            ];

        fn_util_AjaxWM("frmCotizacionRegistro.aspx/ListadoCotizacionCronograma",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_C.addJSONData(jsondata);
                    fn_doResize();
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

    } catch (ex) {
        fn_util_alert(ex.message);
    }

}




//****************************************************************
// Funcion		:: 	fn_paginaCronograma
// Descripción	::	Pagina Cronograma
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_paginaCronograma() {

    try {

        var arrParametros = ["pstrPaginaActual", fn_util_getJQGridParam("jqGrid_lista_C", "page")];

        fn_util_AjaxWM("frmCotizacionRegistro.aspx/PaginaCronograma",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_C.addJSONData(jsondata);
                    fn_doResize();
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

    } catch (ex) {
        fn_util_alert(ex.message);
    }

}





//****************************************************************
// Funcion		:: 	fn_inicializaEditar
// Descripción	::	Setea Edicion
// Log			:: 	JRC - 01/06/2012
//****************************************************************
function fn_inicializaEditar() {

    //Tipo Persona
    fn_validaTipoPersonaEditar($('#cmbTipoPersona').val());

    //Check Cliente
    var booValidaCliente = false;
    if ($('#hddValidaCliente').val() == "1") booValidaLinea = false;
    fn_validaActivacionClienteEditar(booValidaCliente);

    //Obtiene SpreadBanca
    fn_oc_bancaAtencion($('#cmbBancaAtencion').val());

    //Valida CheckLinea    
    if (fn_util_trim($("#txtCUCliente").val()) == "00000000" || fn_util_trim($("#txtCUCliente").val()) == "") {
        $('#chkLinea').attr('disabled', 'disabled');
    }

    //Valida Fecha
    fn_validaFechaActivacion($("#txtFechaMaxActivacion").val());

    //Clasificacion Bien
    fn_oc_clasificacionBienEditar($('#cmbClasificacionBien').val());

    //Check Linea
    var booValidaLinea = $('#chkLinea').attr('checked');
    fn_seteaCheckLinea(booValidaLinea);
    if ($('#cmbLinea option').size() == 1) {
        $('#chkLinea').attr('disabled', 'disabled');
    }


    //Estado Cotizacion
    if ($("#cmbEstado").val() != strEstadoCotizacionPendCarta) {
        fn_util_SeteaDisabledInput('chkGeneraCarta');
    }

    //Valida COmision
    fn_validaComisionMoneda($("#cmbMoneda").val());


    //Clasificacion Bien
    fn_validaTipoSeguroBien($('#cmbTipoBienSeguro').val(), $('#txtNumCuotasfinanciadas').val());

    //Seguro Degravamen
    fn_validaTipoSeguroDegravamen($('#cmbTipoSeguro').val(), $('#txtNumCuotaFinanciar').val());

    //Check Opciones
    fn_validaMostrarComision($("#hddMostrarComision").val());
    fn_validaMostrarTea($("#hddMostrarTea").val());

    //Cuota Inicial
    fn_validaTipoCuotaEditar($("#hddTipoCuota").val());

    //Valida Precuota
    fn_validaSubTipoContratoEditar($("#cmbSubTipoContrato").val());

    //Setea Campos
    $("#txtPrecioVenta").val(fn_util_ValidaMonto($("#txtPrecioVenta").val(), 2));
    $("#txtPorcIGV").val(fn_util_ValidaMonto($("#txtPorcIGV").val(), 2));
    $("#txtMontoIGV").val(fn_util_ValidaMonto($("#txtMontoIGV").val(), 2));
    $("#txtValorVenta").val(fn_util_ValidaMonto($("#txtValorVenta").val(), 2));
    $("#txtCuotaInicial").val(fn_util_ValidaMonto($("#txtCuotaInicial").val(), 2));
    $("#txtCuotaInicialPorc").val(fn_util_ValidaMonto($("#txtCuotaInicialPorc").val(), 2));
    $("#txtRiesgoNeto").val(fn_util_ValidaMonto($("#txtRiesgoNeto").val(), 2));

    $("#txtTEA").val(fn_util_ValidaMonto($("#txtTEA").val(), 2));
    $("#txtCostoFondos").val(fn_util_ValidaMonto($("#txtCostoFondos").val(), 2));
    $("#txtSpread").val(fn_util_ValidaMonto($("#txtSpread").val(), 2));
    $("#txtPrecuota").val(fn_util_ValidaMonto($("#txtPrecuota").val(), 2));

    $("#txtOpcionCompraPorc").val(fn_util_ValidaMonto($("#txtOpcionCompraPorc").val(), 2));
    $("#txtOpcionCompraMonto").val(fn_util_ValidaMonto($("#txtOpcionCompraMonto").val(), 2));
    $("#txtComisionActivacionProc").val(fn_util_ValidaMonto($("#txtComisionActivacionProc").val(), 2));
    $("#txtComisionActivacionMonto").val(fn_util_ValidaMonto($("#txtComisionActivacionMonto").val(), 2));
    $("#txtComisionEstructuracionPorc").val(fn_util_ValidaMonto($("#txtComisionEstructuracionPorc").val(), 2));
    $("#txtComisionEstructuracionMonto").val(fn_util_ValidaMonto($("#txtComisionEstructuracionMonto").val(), 2));

    $("#txtImportePrimaSeguroBien").val(fn_util_ValidaMonto($("#txtImportePrimaSeguroBien").val(), 2));
    $("#txtImportePrimaDesgravamen").val(fn_util_ValidaMonto($("#txtImportePrimaDesgravamen").val(), 2));

}




//************************************************************
// Función		:: 	fn_oc_clasificacionBienEditar
// Descripcion 	:: 	Método Clasificacion
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_oc_clasificacionBienEditar(strValor) {
    var strEstadoBien = $("#cmbEstadoBien").val();
    if (strValor == strClasifacionBienInmueble) {
        if (strEstadoBien == strEstadoBienNuevo) {
            //$('#txtPorcIGV').val(fn_util_ValidaMonto(strPorcIGVInmueble, 2));
            $('#txtPorcIGV').addClass('css_input').removeClass('css_input_inactivo');
            $('#txtPorcIGV').prop('readonly', false);
        } else {
            //$('#txtPorcIGV').val(fn_util_ValidaMonto("0", 2));
            $('#txtPorcIGV').addClass('css_input_inactivo').removeClass('css_input');
            $('#txtPorcIGV').prop('readonly', true);
        }
        //$('#cmbprocedencia').val(strProcedenciaLocal);
    } else {
        //$('#txtPorcIGV').val(fn_util_ValidaMonto($('#hddIGV').val(), 2));
        $('#txtPorcIGV').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtPorcIGV').prop('readonly', true);
    }
}





//****************************************************************
// Funcion		:: 	fn_cotizacionRechazar 
// Descripción	::	Abre Modal de Motivo de Rechazo
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_abreNuevoDocumentoComentario() {
    if ($("#hddTipoTransaccion").val() != "NUEVO") {
        var strCodigoCotizacion = $("#hddCodigoCotizacion").val();
        parent.fn_util_AbreModal("Cotización :: Documentos y Comentarios", "Cotizacion/frmDocumentoComentario.aspx?hddCodigoCotizacion=" + strCodigoCotizacion, 650, 320, function() { });
    } else {
        parent.fn_mdl_alert("Debe grabar la Cotización para ingresar un Documento/Comentario.", function() { });
    }
}

//****************************************************************
// Funcion		:: 	fn_editarComentario 
// Descripción	::	Editar 
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_editarDocumentoComentario() {
    if ($("#hddTipoTransaccion").val() != "NUEVO") {
        var strCodigoCotizacion = $("#hddCodigoCotizacion").val();
        var strCodigoDocumento = $("#hddCodigoDocumento").val();
        if (fn_util_trim(strCodigoDocumento) == "") {
            parent.fn_mdl_alert("Debe seleccionar un documento.", function() { });
        } else {
            parent.fn_util_AbreModal("Cotización :: Documentos y Comentarios", "Cotizacion/frmDocumentoComentario.aspx?hddCodigoCotizacion=" + strCodigoCotizacion + "&hddCodigoDocumento=" + strCodigoDocumento, 650, 320, function() { });
        }
    } else {
        parent.fn_mdl_alert("Debe grabar la Cotización para ingresar un Documento/Comentario.", function() { });
    }
}

//****************************************************************
// Funcion		:: 	fn_eliminarDocumentoComentario 
// Descripción	::	Editar 
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_eliminarDocumentoComentario() {

    if ($("#hddTipoTransaccion").val() != "NUEVO") {

        //Variables
        var strCodigoCotizacion = $("#hddCodigoCotizacion").val();
        var strCodigoDocumento = $("#hddCodigoDocumento").val();
        var paramArray = ["pstrCodigoCotizacion", strCodigoCotizacion, "pstrCodigoDocumento", strCodigoDocumento];

        if (fn_util_trim(strCodigoDocumento) == "") {
            parent.fn_mdl_alert("Debe seleccionar un documento.", function() { });
        }
        else {
            //Confirmacion de Eliminacion
            parent.fn_mdl_confirma(
                            "¿Está seguro que desea eliminar el Documento/Comentario?  ", //Mensaje - Obligatorio
                            function() {
                                parent.fn_blockUI();
                                fn_util_AjaxWM("frmCotizacionRegistro.aspx/EliminaDocumentoComentario",
                                                   paramArray,
                                                   function(resultado) {
                                                       fn_cargaGrillaDocumento();
                                                       parent.fn_unBlockUI();
                                                   },
                                                   function(resultado) {
                                                       var error = eval("(" + resultado.responseText + ")");
                                                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
                                                   }
                                    );

                            },
                            "Util/images/question.gif",
                            function() { },
                            'ELIMINAR DOCUMENTO/COMENTARIO'
            );

        }

    } else {
        parent.fn_mdl_alert("Debe grabar la Cotización para ingresar un Documento/Comentario.", function() { });
    }

}


//****************************************************************
// Funcion		:: 	fn_abreArchivo 
// Descripción	::	Abre Archivo
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_abreArchivo(pstrRuta) {
    window.open("../frmDownload.aspx?nombreArchivo=" + pstrRuta);
    return false;
}


//****************************************************************
// Funcion		:: 	fn_validaCambioDataCronograma 
// Descripción	::	Valida Cronograma
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_validaCambioDataCronograma() {

    //Campos
    var strCmbTipoCronograma = $("#cmbTipoCronograma").val();
    var strTxtNroCuotas = $("#txtNroCuotas").val();
    var strCmbPeriodicidad = $("#cmbPeriodicidad").val();
    var strCmbFrecuenciaPago = $("#cmbFrecuenciaPago").val();
    var strTxtTEA = $("#txtTEA").val();
    var strCmbTipoBienSeguro = $("#cmbTipoBienSeguro").val();
    var strTxtPrecioVenta = $("#txtPrecioVenta").val();

    var strTxtPorcIGV = $("#txtPorcIGV").val();
    var strTxtCuotaInicial = $("#txtCuotaInicial").val();
    var strTxtCuotaInicialPorc = $("#txtCuotaInicialPorc").val();
    var strCmbTipoGracia = $("#cmbTipoGracia").val();
    var strTxtFechavence = $("#txtFechavence").val();
    var strTxtFechaMaxActivacion = $("#txtFechaMaxActivacion").val();

    var strTxtImportePrimaSeguroBien = $("#txtImportePrimaSeguroBien").val();
    var strTxtNumCuotasfinanciadas = $("#txtNumCuotasfinanciadas").val();


    //Iniciales
    var strHddIniTipoCronograma = $("#hddIniTipoCronograma").val();
    var strHddIniNroCuotas = $("#hddIniNroCuotas").val();
    var strHddIniPeriodicidad = $("#hddIniPeriodicidad").val();
    var strHddIniFrecuenciaPago = $("#hddIniFrecuenciaPago").val();
    var strHddIniTEA = $("#hddIniTEA").val();
    var strHddIniTipoBienSeguro = $("#hddIniTipoBienSeguro").val();
    var strHddIniPrecioVenta = $("#hddIniPrecioVenta").val();

    var strHddIniCuotaIni = $("#hddIniCuotaIni").val();
    var strHddIniCuotaIniPorc = $("#hddIniCuotaIniPorc").val();
    var strHddIniIGVPorc = $("#hddIniIGVPorc").val();
    var strHddIniTipoGracia = $("#hddIniTipoGracia").val();
    var strHddIniFecVenc = $("#hddIniFecVenc").val();
    var strHddIniFecMaxAct = $("#hddIniFecMaxAct").val();

    var strHddIniImportePrimaSeguroBien = $("#hddIniImportePrimaSeguroBien").val();
    var strHddIniNumCuotasfinanciadas = $("#hddIniNumCuotasfinanciadas").val();



    var blnOK = true;

    if (fn_util_trim(strCmbTipoCronograma) != fn_util_trim(strHddIniTipoCronograma)) {
        blnOK = false;
    }

    if (fn_util_trim(strTxtNroCuotas) != fn_util_trim(strHddIniNroCuotas)) {
        blnOK = false;
    }

    if (fn_util_trim(strCmbPeriodicidad) != fn_util_trim(strHddIniPeriodicidad)) {
        blnOK = false;
    }

    if (fn_util_trim(strCmbFrecuenciaPago) != fn_util_trim(strHddIniFrecuenciaPago)) {
        blnOK = false;
    }

    if (fn_util_trim(strTxtTEA) != fn_util_trim(strHddIniTEA)) {
        blnOK = false;
    }

    if (fn_util_trim(strCmbTipoBienSeguro) != fn_util_trim(strHddIniTipoBienSeguro)) {
        blnOK = false;
    }

    if (fn_util_trim(strCmbTipoGracia) != fn_util_trim(strHddIniTipoGracia)) {
        blnOK = false;
    }

    if (fn_util_ValidaDecimal(strTxtPrecioVenta) != fn_util_ValidaDecimal(strHddIniPrecioVenta)) {
        blnOK = false;
    }

    if (fn_util_trim(strTxtFechavence) != fn_util_trim(strHddIniFecVenc)) {
        blnOK = false;
    }

    if (fn_util_trim(strHddIniFecMaxAct) != fn_util_trim(strHddIniFecMaxAct)) {
        blnOK = false;
    }

    if (fn_util_ValidaDecimal(strTxtCuotaInicial) != fn_util_ValidaDecimal(strHddIniCuotaIni)) {
        blnOK = false;
    }

    if (fn_util_ValidaDecimal(strTxtCuotaInicialPorc) != fn_util_ValidaDecimal(strHddIniCuotaIniPorc)) {
        blnOK = false;
    }

    if (fn_util_ValidaDecimal(strTxtPorcIGV) != fn_util_ValidaDecimal(strHddIniIGVPorc)) {
        blnOK = false;
    }

    if (fn_util_ValidaDecimal(strTxtPorcIGV) != fn_util_ValidaDecimal(strHddIniIGVPorc)) {
        blnOK = false;
    }

    if (fn_util_ValidaDecimal(strTxtPorcIGV) != fn_util_ValidaDecimal(strHddIniIGVPorc)) {
        blnOK = false;
    }

    if (fn_util_ValidaDecimal(strTxtNumCuotasfinanciadas) != fn_util_ValidaDecimal(strHddIniNumCuotasfinanciadas)) {
        blnOK = false;
    }

    if (fn_util_trim(strTxtImportePrimaSeguroBien) != fn_util_trim(strHddIniImportePrimaSeguroBien)) {
        blnOK = false;
    }

    return blnOK;

}


//****************************************************************
// Funcion		:: 	fn_validaColumnasCronograma 
// Descripción	::	Valida Columnas Cronograma
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_validaColumnasCronograma() {

    var gridCronograma = $('#jqGrid_lista_C');
    if ($("#hddTipoPersona").val() == strTipoPersonaJuridica) {
        gridCronograma.jqGrid('hideCol', gridCronograma.getGridParam("colModel")[8].name);
    } else {
        if ($("#hddTipoPersona").val() != strSeguroDegravamenTipoInterno) {
            gridCronograma.jqGrid('hideCol', gridCronograma.getGridParam("colModel")[8].name);
        }
    }
    if (!fn_util_ValidaDecimal($('#txtImportePrimaSeguroBien').val()) > 0) {
        gridCronograma.jqGrid('hideCol', gridCronograma.getGridParam("colModel")[7].name);
    } else {
        gridCronograma.jqGrid('showCol', gridCronograma.getGridParam("colModel")[7].name);
    }

    if (!fn_util_ValidaDecimal($('#txtImportePrimaDesgravamen').val()) > 0) {
        gridCronograma.jqGrid('hideCol', gridCronograma.getGridParam("colModel")[8].name);
    } else {
        gridCronograma.jqGrid('showCol', gridCronograma.getGridParam("colModel")[8].name);
    }

}



//****************************************************************
// Funcion		:: 	fn_validaReenvioSupervisor 
// Descripción	::	Valida datos para supervisor
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_validaReenvioSupervisor() {

    var blnOK = false;

    var strHddIniSupTEA = $("#hddIniSupTEA").val();
    var strHddIniSupPreCuotaPorc = $("#hddIniSupPreCuotaPorc").val();
    var strHddIniSupSpread = $("#hddIniSupSpread").val();
    var strHddIniComisionActivacionMonto = $("#hddIniComisionActivacionMonto").val();

    var strTxtTEA = $("#txtTEA").val();
    var strTxtPrecuota = $("#txtPrecuota").val();
    var strTxtSpread = $("#txtSpread").val();
    var strTxtComisionActivacionMonto = $("#txtComisionActivacionMonto").val();

    if (fn_util_ValidaDecimal(strHddIniSupTEA) != fn_util_ValidaDecimal(strTxtTEA)) {
        blnOK = true;
    }
    if (fn_util_ValidaDecimal(strHddIniSupPreCuotaPorc) != fn_util_ValidaDecimal(strTxtPrecuota)) {
        blnOK = true;
    }
    if (fn_util_ValidaDecimal(strHddIniSupSpread) != fn_util_ValidaDecimal(strTxtSpread)) {
        blnOK = true;
    }
    if (fn_util_ValidaDecimal(strHddIniComisionActivacionMonto) != fn_util_ValidaDecimal(strTxtComisionActivacionMonto)) {
        blnOK = true;
    }

    return blnOK;
}



//****************************************************************
// Funcion		:: 	fn_validaFechaActivacion 
// Descripción	::	Valida datos para supervisor
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_validaFechaActivacion(pstrValor) {
    if (fn_util_trim(pstrValor) != "") {
        $('#txtFechavence').addClass('css_input').removeClass('css_input_inactivo');
        $('#txtFechavence').prop('readonly', false);
        fn_util_SeteaCalendario($("#txtFechavence"));
    } else {
        $("#txtFechavence").datepicker("destroy");
        $('#txtFechavence').addClass('css_input_inactivo').removeClass('css_input');
        $('#txtFechavence').prop('readonly', true);
    }
}



//****************************************************************
// Funcion		:: 	fn_of_CuotasSeguro 
// Descripción	::	Valida Cuotas Seguro
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_of_CuotasSeguro() {
    var strCuotas = $("#txtNroCuotas").val();
    var strCuotasSeg = $("#txtNumCuotasfinanciadas").val();

    if (fn_util_trim(strCuotas) == "") strCuotas = "0";
    if (fn_util_trim(strCuotasSeg) == "") strCuotasSeg = "0";

    if (parseInt(strCuotasSeg) > parseInt(strCuotas)) {
        $("#txtNumCuotasfinanciadas").val(strCuotas);
        //parent.fn_util_MuestraLogPage("El Nro. de Cuotas del Seguro ingresado no valido", "E");
    }

    //if (parseInt(strCuotasSeg) == 0) {
    //$("#txtNumCuotasfinanciadas").val(1)
    //parent.fn_util_MuestraLogPage("El Nro. de Cuotas del Seguro ingresado no valido", "E");
    //}

}


//****************************************************************
// Funcion		:: 	fn_of_PlazoGracia 
// Descripción	::	Valida PlazoGracia
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_of_PlazoGracia() {
    var txtPlazoGracia = $("#txtPlazoGracia").val();
    if (fn_util_trim(txtPlazoGracia) == "") {
        txtPlazoGracia = "0";
    }

    var strNroCuotas = $("#txtNroCuotas").val();
    if (fn_util_trim(strNroCuotas) == "") strNroCuotas = "0";
    var intNroCuotas = parseInt(strNroCuotas);
    var intPlazoGracia = parseInt(txtPlazoGracia);
    if (intPlazoGracia < 0 || intNroCuotas <= intPlazoGracia) {
        $("#txtPlazoGracia").val("0");
        parent.fn_util_MuestraLogPage("El Plazo de Gracia ingresado no es correcto", "E");
    }
    if (intPlazoGracia == 0) {
        $("#txtPlazoGracia").val("0");
    }
}


//****************************************************************
// Funcion		:: 	fn_of_CuotasSeguroDes
// Descripción	::	Valida Cuotas Seguro
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_of_CuotasSeguroDes() {
    var strCuotas = $("#txtNroCuotas").val();
    var strCuotasSeg = $("#txtNumCuotaFinanciar").val();

    if (fn_util_trim(strCuotas) == "") strCuotas = "0";
    if (fn_util_trim(strCuotasSeg) == "") strCuotasSeg = "0";

    if (parseInt(strCuotasSeg) > parseInt(strCuotas)) {
        $("#txtNumCuotaFinanciar").val(strCuotas)
        //parent.fn_util_MuestraLogPage("El Nro. de Cuotas del Seguro ingresado no valido", "E");
    }

    //if (parseInt(strCuotasSeg) == 0) {
    //$("#txtNumCuotaFinanciar").val(1)
    //parent.fn_util_MuestraLogPage("El Nro. de Cuotas del Seguro ingresado no valido", "E");
    //}

}


//****************************************************************
// Funcion		:: 	fn_validaComisionMoneda
// Descripción	::	Comision Moneda
// Log			:: 	JRC - 06/07/2012
//****************************************************************
function fn_validaComisionMoneda(strValor) {

    //Comision Activacion
    if (strValor != "0") {
        var arrParametros = ["pstrOp", "9", "pstrCodMoneda", strValor];
        var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');
        if (arrResultado.length > 0) {
            if (arrResultado[0] == "0") {
                $('#hddComisionActivacion').val(arrResultado[1]);
            } else {
                var strError = arrResultado[1];
                fn_mdl_alert(strError.toString(), function() { });
            }
        }
    }

}



//****************************************************************
// Funcion		:: 	fn_validaFechaActivacion
// Descripción	::	Valida Fecha Activacion
// Log			:: 	JRC - 10/07/2012
//****************************************************************
function fn_validaFechaActivacion() {
    var strMensaje = "";
    var strFrecPago = $("#cmbFrecuenciaPago").val();
    if (fn_util_trim(strFrecPago) == strFrecPagoAnual_fija || fn_util_trim(strFrecPago) == strFrecPagoBimestral_fija ||
            fn_util_trim(strFrecPago) == strFrecPagoMensual_fija || fn_util_trim(strFrecPago) == strFrecPagoSemestral_fija ||
            fn_util_trim(strFrecPago) == strFrecPagoTrimestral_fija) {

        var strFecAtivacion = $("#txtFechaMaxActivacion").val();
        if (fn_util_trim(strFecAtivacion) == "") {
            strMensaje = "- Debe ingresar la fecha de Activación<br />";
        } else {
            var strFecHoy = $("#hddFechaActual").val();
            if (fn_util_ComparaFecha(strFecAtivacion, strFecHoy)) {
                strMensaje = "- La fecha de activación debe ser mayor a la fecha de hoy.<br />";
            }
        }

    }
    return strMensaje;

}


//****************************************************************
// Funcion		:: 	fn_listaEjecutivoLeasing
// Descripción	::	Lista Ejecutivos
// Log			:: 	IJM - 06/03/2012
//****************************************************************
function fn_listaEjecutivoLeasing() {

    //Consulta Ultimus
    var arrParametros = ["Dato", ""];
    fn_util_AjaxWM("frmCotizacionRegistro.aspx/ConsultaEjecutivoLeasing",
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
// Funcion		:: 	fn_ValidaBloqueo
// Descripción	::	Valida Bloqueo
// Log			:: 	JRC - 06/08/2012
//****************************************************************
function fn_ValidaBloqueo() {

    var strBloqueoExistente = $("#hddBloqueoExistente").val();
    var strBloqueoCodigo = $("#hddBloqueoCodigo").val();
    var strBloqueoCodUsuario = $("#hddBloqueoCodUsuario").val();
    var strBloqueoNomUsuario = $("#hddBloqueoNomUsuario").val();
    var strBloqueoFecha = $("#hddBloqueoFecha").val();

    if (fn_util_trim(strBloqueoExistente) == "1") {

        parent.fn_mdl_confirmaBloqueo(
                        "La Cotización está siendo modificada por el usuario (" + strBloqueoCodUsuario + ") " + strBloqueoNomUsuario + " desde la fecha " + strBloqueoFecha + " ¿Desea continuar con la modificación?"
                        , function() { fn_ActualizaBloqueo(strBloqueoCodigo) }
                        , "Util/images/img_bloqueo.gif"
                        , function() { fn_util_redirect('frmCotizacionListado.aspx'); }
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
    fn_util_AjaxWM("frmCotizacionRegistro.aspx/ActualizaBloqueo",
             arrParametros,
             function(result) {
                 parent.fn_unBlockUI();
             },
             function(resultado) {
                 parent.fn_unBlockUI();
                 var error = eval("(" + resultado.responseText + ")");
                 parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN BLOQUEO");
             }
    );


}