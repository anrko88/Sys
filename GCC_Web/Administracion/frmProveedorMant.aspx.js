//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
//****************************************************************

var strPeruana = 'P';
var strExtranjero = 'E';
var strPaisPeru = '504';
var strTipoPersonaJuridica = '2';
var strTipoPersonaNatural = '1';
var strTipoDocumentoRuc = '2';
var strTipoDocumentoOtros = '6';
var strTipoCuentaCorriente = '01';
var strMonedaSoles = '001';
var strTipoBancoIBK = '001';
var strTipoBancoBN = '002';



var strTipoDocumentoDNI = "1";
var strTipoDocumentoRUC = "2";
var strTipoDocumentoCarnetEx = "3";
var strTipoDocumentoPasaporte = "5";
var strTipoDocumentoOtroDoc = "6";

var intPerfil = "";

$(document).ready(function() {
    intPerfil = $('#HidPerfil').val();
    //Carga Grilla
    fn_cargaGrilla();
    $("#jqGrid_lista_B").setGridWidth($(window).width() - 75);

    //On load Page (siempre al final)
    fn_onLoadPage();

    //Recupera datos de RM

    $("#txtNroDocumento").focusout(function() {
        $('#imgBsqClienteRM').click();
    });




    fn_inicializaCampos();
    //$("#divEditar").hide();
    $("#divCancelar").hide();
    fn_oculta_controles($("#ddlProcedencia").val());
    //fn_validarCampos($("#cmbTipoDocumento").val());
    fn_ValidarCampo_TipoPersona($("#cmbTipoPersona").val());
    //$("#cmbDepartamento option[value='504']").remove();

    //VALIDAR EL TAMAÑO DE CARACTERES DE TIPO DOCUMENTO
    $("#txtRazonSocial").validText({ type: 'comment', length: 100 });
    $('#txtNroDocumento').attr('disabled', 'disabled');
    // Valida el ingreso de datos en tipo documento
    $('#cmbTipoDocumento').change(function() {
        var strValor = $(this).val();
        //$("#txtNroDocumento").val("");
        //$('#txtNroDocumento').unbind('keypress');
        //$('#cmbTipoPersona').val("0");
        //$('#txtRazonSocial').val("");
        //$('#txtDireccion').val("");
        if (fn_util_trim(strValor) == strTipoDocumentoDNI) {
            $('#txtNroDocumento').attr('disabled', false);
            $('#txtNroDocumento').validText({ type: 'number', length: 8 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoRUC) {
            $('#txtNroDocumento').attr('disabled', false);
            $('#txtNroDocumento').validText({ type: 'number', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoCarnetEx) {
            $('#txtNroDocumento').attr('disabled', false);
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoPasaporte) {
            $('#txtNroDocumento').attr('disabled', false);
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoOtroDoc) {
            $('#txtNroDocumento').attr('disabled', false);
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else {
            $('#txtNroDocumento').attr('disabled', 'disabled');
        }

    });

    // CONSULTAR DATOS RM POR TIPO DE PERSONA Y NUMERO DE DOCUMENTO

    $('#imgBsqClienteRM').click(function() {

        parent.fn_blockUI();
        if ($('#cmbTipoDocumento').val() == "0") {
            parent.fn_unBlockUI();
            parent.fn_mdl_mensajeIco("Para realizar la búsqueda del proveedor debe ingresar el tipo de persona", "util/images/error.gif", "ADVERTENCIA");
        } else {

            //Lmpia RM   

            // $('#cmbTipoPersona').val("0");
            //$('#txtRazonSocial').val("");
            //$('#txtDireccion').val("");

            //Valores            
            var strTipoDocumento = $("#cmbTipoDocumento").val();
            var strNumeroDocumento = $("#txtNroDocumento").val();

            var paramArray = ["pstrNroRuc", strNumeroDocumento, "pstrTipoDoc", strTipoDocumento];

            fn_util_AjaxWM("frmProveedorMant.aspx/ConsultaClienteRM",
                   paramArray,
                   fn_PoneDatosClienteRM,
                   function(resultado) {
                       parent.fn_unBlockUI();
                       parent.fn_mdl_mensajeIco("Se produjo un error al cargar los datos de RM", "util/images/error.gif", "ERROR EN CONSULTA RM");
                   }
            );

            //Resize Pantalla
            fn_doResize();
            parent.fn_unBlockUI();
        }

    });

});

//****************************************************************
// Funcion		:: 	fn_consultarRM
// Descripción	::	El método consulta RM, para obtener el código único.
//              ::  El método es invocado desde grabar, una vez que valide si no existe un código único para le proveedor.
// Log			:: 	AEP - 14/08/2012
// Modificacion ::  

//****************************************************************
function fn_consultarRM() {
    var strTipoDocumento = $("#cmbTipoDocumento").val();
    var strNumeroDocumento = $("#txtNroDocumento").val();

    var paramArray = ["pstrNroRuc", strNumeroDocumento, "pstrTipoDoc", strTipoDocumento];

    fn_util_AjaxWM("frmProveedorMant.aspx/ConsultaClienteRM",
                   paramArray,
                   fn_RetornarCodigoClienteRM,
                   function(resultado) {
                       parent.fn_mdl_mensajeIco("Se produjo un error al cargar los datos de RM", "util/images/error.gif", "ERROR EN CONSULTA RM");

                   }
            );
}




//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	En este método se realiza validacion de campos como tamaño, tipo de dato, etc.
//              ::  El método es invocado al inicio de la carga de la página.
// Log			:: 	JRC - 05/06/2012
// Modificacion ::  AEP - 12/07/2012
// Se realiza la validación para desactivar los controles cuando este en modo de edición
// cuando el hidCodProveedor=0 eso quiere decir que la operación a realizar es un nuevo registro
// en caso contrario se esta realizando una edición, y procedemos a desactivar los controles.	
//****************************************************************

function fn_inicializaCampos() {

    var CodProveedor = $('#hidCodProveedor').val() == undefined ? "" : $('#hidCodProveedor').val();

    //    if (CodProveedor != "0") {
    //        $("#ddlProcedencia").attr('disabled', 'disabled');
    //        $("#cmbTipoDocumento").attr('disabled', 'disabled');
    //        $("#cmbTipoPersona").attr('disabled', 'disabled');
    //        $("#cmbTipoDocumento").attr('disabled', 'disabled');
    //        $("#ddlPais").attr('disabled', 'disabled');
    //        $("#ddlDepartamento").attr('disabled', 'disabled');
    //        $("#cmbProvincia").attr('disabled', 'disabled');
    //        $("#cmbDistrito").attr('disabled', 'disabled');
    //        $("#txtRazonSocial").attr('disabled', 'disabled');
    //        $("#txtDireccion").attr('disabled', 'disabled');
    //        $("#txtNroDocumento").attr('disabled', 'disabled');
    //        $("#imgBsqClienteRM").attr('disabled', 'disabled');
    //        $("#tb_tabla_BN").hide();
    //        $("#dv_datosBN").hide();
    //        $("#td_TipoCuenta_BN").hide();
    //    }

    //Valida campos obligatorio
    //    fn_seteaCamposObligatorios();

    //Valida Tipo de Datos


    $("#txtNroDocumento").validText({ type: 'number', length: 15 });
    $('#txtDireccion').validText({ type: 'comment', length: 100 });
    $('#txtNombreContacto').validText({ type: 'comment', length: 100 });
    $('#txtCorreo').validText({ type: 'comment', length: 50 });
    $("#txtTelefono").validText({ type: 'number', length: 10 });
    $("#txtNumCuenta1").validText({ type: 'number', length: 13 });
    $("#txtNumCuenta2").validText({ type: 'number', length: 13 });
    $("#txtNumCuenta3").validText({ type: 'number', length: 11 });
}


//****************************************************************
// Funcion		:: 	fn_SeteaUbigeo
// Descripción	::	Vulve a cargar los valores iniciales del ubigeo.
// Log			:: 	JRC - 05/06/2012
//****************************************************************
function fn_SeteaUbigeo() {
    //Carga Departamento
    var strDepartamento = $("#hidCodDepartamento").val();
    $("#ddlDepartamento").val(strDepartamento);

    //Carga Provincia
    fn_cargaComboProvincia(strDepartamento);
    strProvincia = $("#hidCodProvincia").val();
    $("#cmbProvincia").val(strProvincia);

    //Carga Distrito
    fn_cargaComboDistrito(strDepartamento, strProvincia);
    $("#cmbDistrito").val($("#hidCodDistrito").val());
}

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Se configura la grilla con los campos que se desee mnostrar, 
//                  también se invoca al metodo fn_buscarContacto() donde se realiza la carga de datos desde BD
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() {
    $("#jqGrid_lista_B").jqGrid({
        datatype: function() {
            fn_buscarContacto();
        },
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodigoContacto" // Índice de la columna con la clave primaria.
        },
        colNames: ['Nombre Contacto', 'Cargo', 'Correo', 'Teléfono', 'CodigoContacto', 'CodCargo'],
        colModel: [
			{ name: 'Nombre', index: 'Nombre', width: 220, align: "left" },
			{ name: 'Cargo', index: 'Cargo', width: 100, align: "left" },
			{ name: 'Correo', index: 'Correo', width: 150, align: "left" },
			{ name: 'Telefono', index: 'Telefono', width: 100, align: "left" },
			{ name: 'CodigoContacto', index: 'CodigoContacto', hidden: true },
			{ name: 'CodCargo', index: 'CodCargo', hidden: true }
		],
        height: '100%',
        pager: '#jqGrid_pager_B',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'Nombre',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        multiselect: true,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            $("#hddRowId").val(id);
        },
        ondblClickRow: function(id) {
            var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
            fn_selectContacto(rowData);
            $("#dv_BotonesNormales").hide();
            $("#dv_BotonesEdicion").show();
        }
    });
    $("#jqGrid_lista_B").jqGrid('navGrid', '#jqGrid_pager_B', { edit: false, add: false, del: false });
    $("#jqGrid_lista_B").setGridWidth($(window).width() - 150);
    $("#search_jqGrid_lista_B").hide();
}

//****************************************************************
// Funcion		:: 	fn_buscarContacto
// Descripción	::	Carga los datos del Contacto desde la base de datos, invocando a un metodo del 
//              ::  code behind "ListadoContacto". 
// Log			:: 	WCR - 01/06/2012
//****************************************************************
function fn_buscarContacto() {
    var codProveedor = $('#hidCodProveedor').val() == undefined ? "" : $('#hidCodProveedor').val();

    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_B", "rowNum"),     // Cantidad de elementos de la página.
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_B", "page"),    // Página actual.
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_B", "sortname"), // Columna a ordenar.
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_B", "sortorder"), // Criterio de ordenación.
                         "pCodProveedor", codProveedor
                            ];

    fn_util_AjaxWM("frmProveedorMant.aspx/ListadoContacto",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_B.addJSONData(jsondata);
                        parent.fn_unBlockUI();
                        fn_doResize();
                    },
                    function(request) {
                        fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                    }
                   );
}

//************************************************************
// Función		:: 	fn_CargarComboPais
// Descripcion 	:: 	Método que vuelve a cargar los datos inciales del combo pais.
// Log			:: 	WCR - 01/06/2012
//************************************************************
function fn_CargarComboPais() {

    var arrParametros = ["pstrOp", "10"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlPais').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
}

//************************************************************
// Función		:: 	fn_oculta_controles
// Descripcion 	:: 	Método donde se realiza distintas operaciones de ocultar y mostrar los controles
//                  de acuerdo a ciertas validaciones por procedencia. 
// Log			:: 	WCR - 01/06/2012
// Modificado   ::  AEP - 10/07/2012 
//************************************************************
function fn_oculta_controles(pValue) {
    $("#divCuentas").hide();
    $("#tdlDepartamento").hide();
    $("#tdcDepartamento").hide();
    $("#tdlProvincia").hide();
    $("#tdcProvincia").hide();
    $("#tdlDistrito").hide();
    $("#tdcDistrito").hide();

    if ($("#hidOpcion").val() == '0') {
        $("#ddlPais").prop("selectedIndex", 0);
        $("#cmbTipoPersona").prop("selectedIndex", 0);
        $("#cmbTipoDocumento").prop("selectedIndex", 0);
        $("#cmbTipoCuenta1").prop("selectedIndex", 0);
        $("#cmbTipoCuenta2").prop("selectedIndex", 0);
        //$("#cmbTipoCuenta3").prop("selectedIndex", 0);
        $("#cmbMoneda1").prop("selectedIndex", 0);
        $("#cmbMoneda2").prop("selectedIndex", 0);
        $("#cmbMoneda3").prop("selectedIndex", 0);
    }
    //$("#ddlPais").removeAttr('disabled');
    $("#cmbMoneda3").val(strMonedaSoles);
    $("#cmbMoneda3").attr('disabled', 'disabled');
    $("#divCuentas").show();
    if (pValue == strExtranjero) { // si la procedencia es estranjera
        $("#cmbTipoDocumento option[value='1']").remove();
        $("#cmbTipoDocumento option[value='2']").remove();
        $("#tb_tabla_BN").hide();
        $("#dv_datosBN").hide();
        $("#td_TipoCuenta_BN").hide();
        fn_util_SeteaObligatorio($("#ddlPais"), "select");
        $("#txtDireccion").addClass('css_input').removeClass('css_input_obligatorio');

        if (($("#hidOpcion").val() == '0') && ($("#cmbTipoPersona").val() == '0')) { $("#cmbTipoPersona").val(0); }
        if (($("#hidOpcion").val() == '0') && ($("#cmbTipoDocumento").val() == '0')) { $("#cmbTipoDocumento").val(0); }
        if (($("#hidOpcion").val() == '0') && ($("#ddlPais").val() == '0')) { $("#ddlPais").prop("selectedIndex", 0); }
        jQuery('#ddlPais').children('option[value="' + strPaisPeru + '"]').css('display', 'none');

        $("#ddlPais option[value='504']").remove();
        //$("#cmbTipoCuenta3").attr('disabled', 'disabled');
        $("#txtNumCuenta3").attr('disabled', 'disabled');
        //Inicio JJM IBK
        //        if ($("#hidCodProveedor").val() != "0") {
        //            $("#ddlPais").attr('disabled', 'disabled');
        //        } else {
        //            $("#ddlPais").attr('disabled', false);
        //        }
        //      Fin IBK


    } else if (pValue == strPeruana) { // si la procedencia es peruana
        $("#cmbTipoDocumento option[value='6']").remove();
        $("#tb_tabla_BN").show();
        $("#dv_datosBN").show();
        if ($("#hidCodProveedor").val() == "0") {
            fn_util_SeteaObligatorio($("#txtDireccion"), "text");
        }

        $("#ddlPais").addClass('css_input').removeClass('css_input_obligatorio');
        fn_CargarComboPais();
        jQuery('#ddlPais').children('option[value="' + strPaisPeru + '"]').css('display', 'block');
        if (($("#hidOpcion").val() == '0') && ($("#cmbTipoPersona").val() == '0')) { $("#cmbTipoPersona").val(0); }
        $("#ddlPais").val(strPaisPeru);
        $("#ddlPais").attr('disabled', 'disabled');
        $("#tdlDepartamento").show();
        $("#tdcDepartamento").show();
        $("#tdlProvincia").show();
        $("#tdcProvincia").show();
        $("#tdlDistrito").show();
        $("#tdcDistrito").show();
    } else {
        //$("#cmbTipoCuenta3").attr('disabled', 'disabled');
        $("#txtNumCuenta3").attr('disabled', 'disabled');

        $("#cmbTipoDocumento option[value='1']").remove();
        $("#cmbTipoDocumento option[value='2']").remove();
        $("#cmbTipoDocumento option[value='3']").remove();
        $("#cmbTipoDocumento option[value='4']").remove();
        $("#cmbTipoDocumento option[value='5']").remove();
        $("#cmbTipoDocumento option[value='6']").remove();

        $("#tb_tabla_BN").hide();
        $("#dv_datosBN").hide();
        $("#td_TipoCuenta_BN").hide();
    }
    //Inicio JJM IBK
    if ((intPerfil == '6') || (intPerfil == '11') || (intPerfil == '1')) {
        $("#dv_img_boton3").show();
    }
    else { $("#dv_img_boton3").hide(); }
    //Fin
    fn_doResize();
}

function fn_seteaCamposObligatorios() {

    if ($("#hidCodProveedor").val() == "0") {
        fn_util_SeteaObligatorio($("#ddlProcedencia"), "select");
        fn_util_SeteaObligatorio($("#ddlDepartamento"), "select");
        fn_util_SeteaObligatorio($("#cmbProvincia"), "select");
        fn_util_SeteaObligatorio($("#cmbTipoDocumento"), "select");
        fn_util_SeteaObligatorio($("#cmbTipoPersona"), "select");
        fn_util_SeteaObligatorio($("#txtRazonSocial"), "text");
        fn_util_SeteaObligatorio($("#txtNroDocumento"), "text");
        fn_util_SeteaObligatorio($("#txtDireccion"), "text");
    }

}



//************************************************************
// Función		:: 	fn_grabar
// Descripcion 	:: 	Método que graba los valores del proveedor,contactos y cuentas.
// Log			:: 	JRC - 10/02/2012
// Modificado   ::  AEP - 19/07/2012
//************************************************************
function fn_grabar() {
    var strError = new StringBuilderEx();
    fn_validarRegistro(strError);
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        //fn_seteaCamposObligatorios();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }
    else {
        if (($("#cmbTipoCuenta1").val() != "0") || ($("#cmbTipoCuenta2").val() != "0")) {  // || ($("#cmbTipoCuenta3").val()!="0")) {
            fn_validaCuenta();
            var resultado = $("#hddValidaCuenta").val();
            var result = resultado.split('|');
            if (result[0] != "0") {
                parent.fn_mdl_mensajeIco(result[1], "util/images/warning.gif", "ADVERTENCIA");
                return;
            } else {
                if (result[0] == "0" && result[1] == $("#HidCodigoUnico").val()) {

                } else {
                    parent.fn_mdl_mensajeIco("La Primera cuenta está errada", "util/images/warning.gif", "ADVERTENCIA");
                    return;
                }
            }
        }

        if ($("#HidCodigoUnico").val() == '') {
            fn_consultarRM();
        }

        if ($("#HidCodigoUnico").val() != '') {

            fn_mdl_confirma('¿Está seguro de guardar al proveedor?',
    			function() {

    			    parent.fn_blockUI();

    			    var strCuenta = '';
    			    var strContactos = '';


    			    var rows = jQuery("#jqGrid_lista_B").jqGrid('getRowData'); //Trae todas las filas que contiene la grilla
    			    for (var i = 0; i < rows.length; i++) {
    			        var oData = rows[i];
    			        if (parseFloat(oData.CodigoContacto) <= 0) {
    			            strContactos = strContactos + '0;' + oData.Nombre + ';' + oData.Correo + ';' + oData.Telefono + ';' + oData.CodCargo + '|';
    			        } else {
    			            strContactos = strContactos + oData.CodigoContacto + ';' + oData.Nombre + ';' + oData.Correo + ';' + oData.Telefono + ';' + oData.CodCargo + '|';
    			        }
    			    }

    			    var strDepartamento = $("#ddlDepartamento").val();
    			    if (strDepartamento == 0 || strDepartamento == null) {
    			        strDepartamento = "00";
    			    }

    			    var strProvincia = $("#cmbProvincia").val();
    			    if (strProvincia == 0 || strProvincia == null) {
    			        strProvincia = "";
    			    }

    			    var strDistrito = $("#cmbDistrito").val();
    			    if (strDistrito == 0 || strDistrito == null) {
    			        strDistrito = "";
    			    }

    			    var strCuentasEliminadas = '';
    			    //if ($('#ddlProcedencia').val() == strPeruana) {
    			    if ($('#cmbTipoCuenta1').val() != '0') {
    			        strCuenta = strCuenta + $("#hidCodProveedorCuenta1").val() + ';' + strTipoBancoIBK + ';' + $('#cmbTipoCuenta1').val() + ';' + $('#cmbMoneda1').val() + ';' + $('#txtNumCuenta1').val() + '|';
    			    }
    			    if ($('#cmbTipoCuenta2').val() != '0') {
    			        strCuenta = strCuenta + $("#hidCodProveedorCuenta2").val() + ';' + strTipoBancoIBK + ';' + $('#cmbTipoCuenta2').val() + ';' + $('#cmbMoneda2').val() + ';' + $('#txtNumCuenta2').val() + '|';
    			    }
    			    if ($('#txtNumCuenta3').val() != '') {
    			        strCuenta = strCuenta + $("#hidCodProveedorCuenta3").val() + ';' + strTipoBancoBN + ';' + '001' + ';' + $('#cmbMoneda3').val() + ';' + $('#txtNumCuenta3').val() + '|';
    			    }


    			    if (($('#cmbTipoCuenta1').val() == '0') && ($('#cmbMoneda1').val() == '0') && ($('#txtNumCuenta1').val() == '') && ($("#hidCodProveedorCuenta1").val() != '0')) {
    			        strCuentasEliminadas = strCuentasEliminadas + $("#hidCodProveedorCuenta1").val() + '|';
    			    }
    			    if (($('#cmbTipoCuenta2').val() == '0') && ($('#cmbMoneda2').val() == '0') && ($('#txtNumCuenta2').val() == '') && ($("#hidCodProveedorCuenta2").val() != '0')) {
    			        strCuentasEliminadas = strCuentasEliminadas + $("#hidCodProveedorCuenta2").val() + '|';
    			    }
    			    //if (($('#cmbTipoCuenta3').val() == '0') && 
    			    if (($('#cmbMoneda3').val() == '0') && ($('#txtNumCuenta3').val() == '') && ($("#hidCodProveedorCuenta3").val() != '0')) {
    			        strCuentasEliminadas = strCuentasEliminadas + $("#hidCodProveedorCuenta3").val() + '|';
    			    }

    			    //} else {
    			    //    if ($("#hidCodProveedorCuenta1").val() != '0') { strCuentasEliminadas = strCuentasEliminadas + $("#hidCodProveedorCuenta1").val() + '|'; }
    			    //  if ($("#hidCodProveedorCuenta2").val() != '0') { strCuentasEliminadas = strCuentasEliminadas + $("#hidCodProveedorCuenta2").val() + '|'; }
    			    //     if ($("#hidCodProveedorCuenta3").val() != '0') { strCuentasEliminadas = strCuentasEliminadas + $("#hidCodProveedorCuenta3").val() + '|'; }
    			    //}



    			    var arrParametros = ["pCodProveedor", $("#hidCodProveedor").val(),
    					"pCodProcedencia", $('#ddlProcedencia').val(),
    					"pCodTipoPersona", $("#cmbTipoPersona").val(),
    					"pCodTipoDocumento", $("#cmbTipoDocumento").val(),
    					"pNumeroDocumento", $("#txtNroDocumento").val(),
    					"pRazonSocial", $("#txtRazonSocial").val(),
    					"pDireccion", $("#txtDireccion").val(),
    					"pCodPais", $("#ddlPais").val(),
    					"pCodDepartamento", strDepartamento,
    					"pCodProvincia", strProvincia,
    					"pCodDistrito", strDistrito,
    					"parrContactos", strContactos,
    					"parrContactosEliminados", $("#hidEliminarContacto").val(),
    					"parrCuentas", strCuenta,
    					"parrCuentasEliminadas", strCuentasEliminadas,
    					"pOpcion", $("#hidOpcion").val(),
    					"pCodUnico", $("#HidCodigoUnico").val()
    				];

    			    fn_util_AjaxWM("frmProveedorMant.aspx/GuardarRegistro",
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
    			'Proveedor'
    		);
        }
        //    	} else {
        //    		parent.fn_mdl_mensajeIco("La cuenta no pertenece al proveedor", "util/images/error.gif", "ERROR EN EL REGISTRO");
        //    	}
    }
}

//****************************************************************
// Función		:: 	fn_MensajeYRedireccionar
// Descripción	::	Le muestra un mensaje con el resultado de la 
//                  llamada al respectivo web method.
// Log			:: 	EBL - 07/05/2012
//****************************************************************
var fn_MensajeYRedireccionar = function() {
    parent.fn_unBlockUI();
    parent.fn_mdl_alert('Los datos se registraron satisfactoriamente', function() { fn_util_redirect("frmProveedorListado.aspx"); });
};


//****************************************************************
// Funcion		:: 	fn_validarRegistro
// Descripción	::	Esté metodo se ejecuta antes de grabar el registro, realizando unas validaciones de 
//                  campos obligatorios y validaciones de negocio.
// Log			:: 	WCR - 04/06/2012
// Modificado   ::  AEP - 19/07/2012
//****************************************************************
function fn_validarRegistro(pError) {
    var txtNroDocumento = $('input[id=txtNroDocumento]:text');
    var txtRazonSocial = $('input[id=txtRazonSocial]:text');
    var txtNumCuenta1 = $('input[id=txtNumCuenta1]:text');
    var txtNumCuenta2 = $('input[id=txtNumCuenta2]:text');
    var txtNumCuenta3 = $('input[id=txtNumCuenta3]:text');
    var ddlProcedencia = $('select[id=ddlProcedencia]');
    var cmbTipoPersona = $('select[id=cmbTipoPersona]');
    var cmbTipoDocumento = $('select[id=cmbTipoDocumento]');
    var cmbTipoCuenta1 = $('select[id=cmbTipoCuenta1]');
    var cmbTipoCuenta2 = $('select[id=cmbTipoCuenta2]');
    var cmbTipoCuenta3 = $('select[id=cmbTipoCuenta3]');
    var cmbMoneda1 = $('select[id=cmbMoneda1]');
    var cmbMoneda2 = $('select[id=cmbMoneda2]');
    var cmbMoneda3 = $('select[id=cmbMoneda3]');
    var strtxtDireccion = $('input[id=txtDireccion]:text');
    var strddlPais = $('select[id=ddlPais]');
    var strddlDepartamento = $('select[id=ddlDepartamento]');
    var strddlProvincia = $('select[id=cmbProvincia]');
    var strddlDistrito = $('select[id=cmbDistrito]');

    pError.append(fn_util_ValidateControl(ddlProcedencia[0], 'una Procedencia', 1, ''));
    pError.append(fn_util_ValidateControl(cmbTipoPersona[0], 'un Tipo de Persona', 1, ''));
    pError.append(fn_util_ValidateControl(cmbTipoDocumento[0], 'un Tipo de Documento', 1, ''));
    pError.append(fn_util_ValidateControl(txtNroDocumento[0], 'un Número de Documento', 1, ''));
    pError.append(fn_util_ValidateControl(txtRazonSocial[0], 'una Razón Social o Nombre', 1, ''));

    pError.append(fn_util_ValidateControl(strddlPais[0], 'Pais', 1, ''));


    if ($("#hidOpcion").val() == '0') {
        var arrParametros = ["pstrRuc", $('#txtNroDocumento').val(), "pstrTipoDocumento", $('#cmbTipoDocumento').val()];

        //verifica el si ya se Ingreso
        var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whProveedor', '../');
        if (arrResultado.length > 0) {
            if (arrResultado[0] == "0") {
                pError.append('&nbsp;&nbsp;- Ya se ha registrado un proveedor con el mismo Tipo<br />&nbsp;&nbsp;&nbsp;&nbsp; y Número de Documento<br />');
            }
        }
    }
    if (($('#ddlProcedencia').val() == strPeruana)) {

        if ($("#hidCodProveedor").val() == "0") {
            fn_util_SeteaObligatorio($("#txtDireccion"), "text");
        }

        if (($('#ddlPais').val() != '0') && ($('#ddlDepartamento').val() == '0')) {
            pError.append(fn_util_ValidateControl(strddlDepartamento[0], 'un departamento', 1, ''));
        }
        if (($('#ddlDepartamento').val() != '0') && ($('#cmbProvincia').val() == '0')) {
            pError.append(fn_util_ValidateControl(strddlProvincia[0], 'una provincia', 1, ''));
        }

        pError.append(fn_util_ValidateControl(strtxtDireccion[0], 'Direccion', 1, ''));

        if (($('#cmbTipoCuenta1').val() != '0') && ($('#cmbMoneda1').val() == '0') && ($('#txtNumCuenta1').val() == '')) {
            pError.append(fn_util_ValidateControl(cmbMoneda1[0], 'una Moneda', 1, ''));
            pError.append(fn_util_ValidateControl(txtNumCuenta1[0], 'un Número de Cuenta', 1, ''));

        } else if (($('#cmbTipoCuenta1').val() == '0') && ($('#cmbMoneda1').val() != '0') && ($('#txtNumCuenta1').val() == '')) {
            pError.append(fn_util_ValidateControl(cmbTipoCuenta1[0], 'un Tipo de Cuenta', 1, ''));
            pError.append(fn_util_ValidateControl(txtNumCuenta1[0], 'un Número de Cuenta', 1, ''));
        } else if (($('#cmbTipoCuenta1').val() == '0') && ($('#cmbMoneda1').val() == '0') && ($('#txtNumCuenta1').val() != '')) {
            pError.append(fn_util_ValidateControl(cmbTipoCuenta1[0], 'un Tipo de Cuenta', 1, ''));
            pError.append(fn_util_ValidateControl(cmbMoneda1[0], 'una Moneda', 1, ''));
        } else if (($('#cmbTipoCuenta1').val() != '0') && ($('#cmbMoneda1').val() != '0') && ($('#txtNumCuenta1').val() == '')) {
            pError.append(fn_util_ValidateControl(txtNumCuenta1[0], 'un Número de Cuenta', 1, ''));
        } else if (($('#cmbTipoCuenta1').val() != '0') && ($('#cmbMoneda1').val() == '0') && ($('#txtNumCuenta1').val() != '')) {
            pError.append(fn_util_ValidateControl(cmbMoneda1[0], 'una Moneda', 1, ''));
        }
        if (($('#cmbTipoCuenta2').val() != '0') && ($('#cmbMoneda2').val() == '0') && ($('#txtNumCuenta2').val() == '')) {
            pError.append(fn_util_ValidateControl(cmbMoneda2[0], 'una Moneda', 1, ''));
            pError.append(fn_util_ValidateControl(txtNumCuenta2[0], 'un Número de Cuenta', 1, ''));
        } else if (($('#cmbTipoCuenta2').val() == '0') && ($('#cmbMoneda2').val() != '0') && ($('#txtNumCuenta2').val() == '')) {
            pError.append(fn_util_ValidateControl(cmbTipoCuenta2[0], 'un Tipo de Cuenta', 1, ''));
            pError.append(fn_util_ValidateControl(txtNumCuenta2[0], 'un Número de Cuenta', 1, ''));
        } else if (($('#cmbTipoCuenta2').val() == '0') && ($('#cmbMoneda2').val() == '0') && ($('#txtNumCuenta2').val() != '')) {
            pError.append(fn_util_ValidateControl(cmbTipoCuenta2[0], 'un Tipo de Cuenta', 1, ''));
            pError.append(fn_util_ValidateControl(cmbMoneda2[0], 'una Moneda', 1, ''));
        } else if (($('#cmbTipoCuenta2').val() != '0') && ($('#cmbMoneda2').val() != '0') && ($('#txtNumCuenta2').val() == '')) {
            pError.append(fn_util_ValidateControl(txtNumCuenta2[0], 'un Número de Cuenta', 1, ''));
        } else if (($('#cmbTipoCuenta2').val() != '0') && ($('#cmbMoneda2').val() == '0') && ($('#txtNumCuenta2').val() != '')) {
            pError.append(fn_util_ValidateControl(cmbMoneda2[0], 'una Moneda', 1, ''));
        } else if (($('#cmbTipoCuenta2').val() == '0') && ($('#cmbMoneda2').val() != '0') && ($('#txtNumCuenta2').val() != '')) {
            pError.append(fn_util_ValidateControl(cmbTipoCuenta2[0], 'un Tipo de Cuenta', 1, ''));
        } else if (($('#cmbTipoCuenta1').val() != '0') && ($('#cmbTipoCuenta1').val() == $('#cmbTipoCuenta2').val()) && ($('#cmbMoneda1').val() == $('#cmbMoneda2').val()) && ($('#txtNumCuenta1').val() == $('#txtNumCuenta2').val())) {
            pError.append('&nbsp;&nbsp;- Las cuentas se estan duplicando<br />');
        }
        //if (($('#cmbTipoCuenta3').val() == '0') && 
        //	    	if(($('#cmbMoneda3').val() != '0') && ($('#txtNumCuenta3').val() != '')) {
        //            pError.append(fn_util_ValidateControl(cmbTipoCuenta3[0], 'un Tipo de Cuenta', 1, ''));
        //        } else 
        //	    		if (($('#cmbTipoCuenta3').val() != '0') && ($('#cmbMoneda3').val() != '0') && ($('#txtNumCuenta3').val() == '')) {
        //            pError.append(fn_util_ValidateControl(txtNumCuenta3[0], 'un Número de Cuenta', 1, ''));
        //      }
    } else if (($('#ddlProcedencia').val() == strExtranjero)) {
        if (($('#cmbTipoCuenta1').val() != '0') && ($('#cmbMoneda1').val() == '0') && ($('#txtNumCuenta1').val() == '')) {
            pError.append(fn_util_ValidateControl(cmbMoneda1[0], 'una Moneda', 1, ''));
            pError.append(fn_util_ValidateControl(txtNumCuenta1[0], 'un Número de Cuenta', 1, ''));

        } else if (($('#cmbTipoCuenta1').val() == '0') && ($('#cmbMoneda1').val() != '0') && ($('#txtNumCuenta1').val() == '')) {
            pError.append(fn_util_ValidateControl(cmbTipoCuenta1[0], 'un Tipo de Cuenta', 1, ''));
            pError.append(fn_util_ValidateControl(txtNumCuenta1[0], 'un Número de Cuenta', 1, ''));
        } else if (($('#cmbTipoCuenta1').val() == '0') && ($('#cmbMoneda1').val() == '0') && ($('#txtNumCuenta1').val() != '')) {
            pError.append(fn_util_ValidateControl(cmbTipoCuenta1[0], 'un Tipo de Cuenta', 1, ''));
            pError.append(fn_util_ValidateControl(cmbMoneda1[0], 'una Moneda', 1, ''));
        } else if (($('#cmbTipoCuenta1').val() != '0') && ($('#cmbMoneda1').val() != '0') && ($('#txtNumCuenta1').val() == '')) {
            pError.append(fn_util_ValidateControl(txtNumCuenta1[0], 'un Número de Cuenta', 1, ''));
        } else if (($('#cmbTipoCuenta1').val() != '0') && ($('#cmbMoneda1').val() == '0') && ($('#txtNumCuenta1').val() != '')) {
            pError.append(fn_util_ValidateControl(cmbMoneda1[0], 'una Moneda', 1, ''));
        }
        if (($('#cmbTipoCuenta2').val() != '0') && ($('#cmbMoneda2').val() == '0') && ($('#txtNumCuenta2').val() == '')) {
            pError.append(fn_util_ValidateControl(cmbMoneda2[0], 'una Moneda', 1, ''));
            pError.append(fn_util_ValidateControl(txtNumCuenta2[0], 'un Número de Cuenta', 1, ''));
        } else if (($('#cmbTipoCuenta2').val() == '0') && ($('#cmbMoneda2').val() != '0') && ($('#txtNumCuenta2').val() == '')) {
            pError.append(fn_util_ValidateControl(cmbTipoCuenta2[0], 'un Tipo de Cuenta', 1, ''));
            pError.append(fn_util_ValidateControl(txtNumCuenta2[0], 'un Número de Cuenta', 1, ''));
        } else if (($('#cmbTipoCuenta2').val() == '0') && ($('#cmbMoneda2').val() == '0') && ($('#txtNumCuenta2').val() != '')) {
            pError.append(fn_util_ValidateControl(cmbTipoCuenta2[0], 'un Tipo de Cuenta', 1, ''));
            pError.append(fn_util_ValidateControl(cmbMoneda2[0], 'una Moneda', 1, ''));
        } else if (($('#cmbTipoCuenta2').val() != '0') && ($('#cmbMoneda2').val() != '0') && ($('#txtNumCuenta2').val() == '')) {
            pError.append(fn_util_ValidateControl(txtNumCuenta2[0], 'un Número de Cuenta', 1, ''));
        } else if (($('#cmbTipoCuenta2').val() != '0') && ($('#cmbMoneda2').val() == '0') && ($('#txtNumCuenta2').val() != '')) {
            pError.append(fn_util_ValidateControl(cmbMoneda2[0], 'una Moneda', 1, ''));
        } else if (($('#cmbTipoCuenta2').val() == '0') && ($('#cmbMoneda2').val() != '0') && ($('#txtNumCuenta2').val() != '')) {
            pError.append(fn_util_ValidateControl(cmbTipoCuenta2[0], 'un Tipo de Cuenta', 1, ''));
        } else if (($('#cmbTipoCuenta1').val() != '0') && ($('#cmbTipoCuenta1').val() == $('#cmbTipoCuenta2').val()) && ($('#cmbMoneda1').val() == $('#cmbMoneda2').val()) && ($('#txtNumCuenta1').val() == $('#txtNumCuenta2').val())) {
            pError.append('&nbsp;&nbsp;- Las cuentas se estan duplicando<br />');
        }

        //    	if (($('#cmbTipoCuenta3').val() == '0') && ($('#cmbMoneda3').val() != '0') && ($('#txtNumCuenta3').val() != '')) {
        //            pError.append(fn_util_ValidateControl(cmbTipoCuenta3[0], 'un Tipo de Cuenta', 1, ''));

        //        } else if (($('#cmbTipoCuenta3').val() != '0') && ($('#cmbMoneda3').val() != '0') && ($('#txtNumCuenta3').val() == '')) {
        //            pError.append(fn_util_ValidateControl(txtNumCuenta3[0], 'un Número de Cuenta', 1, ''));

        //      }
    }
}

//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistrito
// Descripción	::	La selección del combo ditrito vuelve a seleccione
// Log			:: 	WCR - 01/06/2012
//****************************************************************
function fn_LimpiaComboDistrito() {
    $('#cmbDistrito').empty();
    $("#cmbDistrito").append('<option value="0">[-Seleccione-]</option>');

}
//****************************************************************
// Funcion		:: 	fn_cargaComboProvincia
// Descripción	::	se carga los valores del combo provincia filtrado por su departamento
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_cargaComboProvincia(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbProvincia').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistrito();

}


//****************************************************************
// Funcion		:: 	fn_cargaComboDistrito
// Descripción	::	se carga los valores del combo distrito filtrado por el departamento y provincia
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_cargaComboDistrito(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbDistrito').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}


//****************************************************************
// Funcion		:: 	fn_cargaComboTipoDocumento
// Descripción	::	se carga los valores del combo Tipo Documento 
// Log			:: 	AEP - 02/08/2012
//****************************************************************
function fn_cargaComboTipoDocumento() {

    var arrParametros = ["pstrTablaGenerica", "TBL041", "pstrOp", "1"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbTipoDocumento').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}

//****************************************************************
// Funcion		:: 	fn_agregarContacto
// Descripción	::	se agrega en forma temporal a la grilla, para despues ser grabado a la base de datos.
// Log			:: 	WCR - 04/06/2012
// Modificado   ::  AEP - 19/07/2012
//****************************************************************
function fn_agregarContacto() {

    var txtNombreContacto = $('input[id=txtNombreContacto]:text');
    var txtCorreo = $('input[id=txtCorreo]:text');
    var txtTelefono = $('input[id=txtTelefono]:text');
    var txtCargo = $("#ddlCargo").find("option:selected").text();
    var strError = new StringBuilderEx();
    strError.append(fn_util_ValidateControl(txtNombreContacto[0], 'un Nombre de Contacto', 1, ''));
    strError.append(fn_util_ValidateControl(txtCorreo[0], 'un Correo', 1, ''));
    if ((!fn_util_ValidateEmail($('#txtCorreo').val())) && ($('#txtCorreo').val() != '')) { strError.append('&nbsp;&nbsp;- El Correo ingresado no es valido'); }
    fn_validarDuplicidad(strError, '0');

    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        //fn_seteaCamposObligatorios();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }
    else {
        var codCargo;
        var cargo;
        if ($('#ddlCargo').val() == "0") {
            cargo = "";

        } else {
            cargo = txtCargo;
        }

        if ($('#ddlCargo').val() == '0') {
            codCargo = "";

        } else {
            codCargo = $('#ddlCargo').val();
        }
        var intRows = (jQuery("#jqGrid_lista_B").getDataIDs().length + 1) * -1;
        var mydata = [{ Nombre: $('#txtNombreContacto').val(), Cargo: cargo, CodCargo: codCargo, Correo: $('#txtCorreo').val(), Telefono: $('#txtTelefono').val(), CodigoContacto: intRows}];
        var su = jQuery("#jqGrid_lista_B").jqGrid('addRowData', intRows, mydata[0]);

        if (su) {
            $('#txtNombreContacto').val('');
            $('#ddlCargo').val(0);
            $('#txtCorreo').val('');
            $('#txtTelefono').val('');
            $('#hidCodigoContacto').val('0');
            parent.fn_mdl_mensajeIco("Los datos se agregaron satisfactoriamente", "util/images/ok.gif", "CONFIRMACION");
            fn_doResize();
        } else {
            parent.fn_mdl_mensajeIco("Se produjo un error al agregar el registro", "util/images/error.gif", "ERROR");
        }
    }
}

//****************************************************************
// Funcion		:: 	fn_validarDuplicidad
// Descripción	::	Valida la duplicidad de contactos al momento de agregar a la grilla.
// Log			:: 	WCR - 04/06/2012
// Modificado   ::  AEP - 19/07/2012
//****************************************************************
function fn_validarDuplicidad(pError, pCodigoContacto) {

    jQuery("#jqGrid_lista_B > tbody > tr").each(function() {

        var vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow');

        if (vElementosAEditar != "") {

            //        var pData = jQuery("#jqGrid_lista_B").getRowData(this.id);
            var pData = $("#jqGrid_lista_B").jqGrid('getRowData', vElementosAEditar[0]);

            if ((pData.CodigoContacto != undefined) && (pCodigoContacto == '0')) {
                if (fn_util_trim($('#txtNombreContacto').val()).toUpperCase() == pData.Nombre.toUpperCase()) {
                    pError.append('&nbsp;&nbsp;- El Contacto ya se encuentra registrado<br />');
                    return pError;
                }
            } else if ((pData.CodigoContacto != undefined) && (pCodigoContacto != pData.CodigoContacto)) {
                if (fn_util_trim($('#txtNombreContacto').val()).toUpperCase() == pData.Nombre.toUpperCase()) {
                    pError.append('&nbsp;&nbsp;- El Contacto ya se encuentra registrado<br />');
                    return pError;
                }
            }

        }
    });
}

//****************************************************************
// Funcion		:: 	fn_eliminarContacto
// Descripción	::  Se elimina en forma temporal de la grilla de contactos.
// Log			:: 	WCR - 04/06/2012
// Modificado   ::  AEP - 16/07/2012
//****************************************************************
function fn_eliminarContacto() {

    var vElementosAEliminar;

    vElementosAEliminar = $("#jqGrid_lista_B").getGridParam('selarrrow');
    if (vElementosAEliminar.length == 0) {
        parent.fn_mdl_mensajeIco("Seleccione el/los contactos para eliminarlos.", "util/images/warning.gif", "ELIMINACIÓN DE CONTACTOS");
    } else {
        parent.fn_mdl_confirma(
		    "¿Está seguro de eliminar el/los contactos seleccionado(s)?"
    	    , fn_Contacto_Eliminar_XML
		    , "util/images/warning.gif"
		    , function() { }
		    , "ELIMINACIÓN DE REGISTRO"
	    );
    }

}
//****************************************************************
// Funcion		:: 	fn_Contacto_Eliminar_XML
// Descripción	::	Todo lo que se elimina de la grilla se agrega al hiden de eliminados
// Log			:: 	Aep - 16/07/2012
//****************************************************************

function fn_Contacto_Eliminar_XML() {

    var vElementosAEliminar;
    //var vEliminado;
    var sResult = "";

    vElementosAEliminar = $("#jqGrid_lista_B").getGridParam('selarrrow');
    for (var i = 0; i < vElementosAEliminar.length; i++) {
        if (vElementosAEliminar[i] > 0) {
            $('#hidEliminarContacto').val($('#hidEliminarContacto').val() + vElementosAEliminar[i] + '|');
            var su = jQuery("#jqGrid_lista_B").jqGrid('delRowData', vElementosAEliminar[i]);
            if (su) {

                $('#txtNombreContacto').val('');
                $('#txtCorreo').val('');
                $('#txtTelefono').val('');
                $('#hidCodigoContacto').val('0');
                parent.fn_mdl_mensajeIco("Los datos se eliminaron satisfactoriamente", "util/images/ok.gif", "CONFIRMACION");

            } else {
                parent.fn_mdl_mensajeIco("Se produjo un error al eliminar el registro", "util/images/error.gif", "ERROR");
            }

        } else {

            var su2 = jQuery("#jqGrid_lista_B").jqGrid('delRowData', vElementosAEliminar[i]);
            if (su2) {

                $('#txtNombreContacto').val('');
                $('#txtCorreo').val('');
                $('#txtTelefono').val('');
                $('#hidCodigoContacto').val('0');
                parent.fn_mdl_mensajeIco("Los datos se eliminaron satisfactoriamente", "util/images/ok.gif", "CONFIRMACION");

            }


        }
        i = i - 1;

    }

    $("#jqGrid_lista_B").jqGrid('resetSelection');
}


//****************************************************************
// Funcion		:: 	fn_editarProveedor
// Descripción	::	Realiza una edicion de los datos del proveedor.
// Log			:: 	WCR - 04/06/2012
//****************************************************************
function fn_editarContacto() {
    if ($('#hidCodigoContacto').val() != '0') {

        var txtNombreContacto = $('input[id=txtNombreContacto]:text');
        var txtCorreo = $('input[id=txtCorreo]:text');
        var txtTelefono = $('input[id=txtTelefono]:text');
        var txtCargo = $("#ddlCargo").find("option:selected").text();
        var strError = new StringBuilderEx();
        strError.append(fn_util_ValidateControl(txtNombreContacto[0], 'un Nombre de Contacto', 1, ''));
        strError.append(fn_util_ValidateControl(txtCorreo[0], 'un Correo', 1, ''));
        if ((!fn_util_ValidateEmail($('#txtCorreo').val())) && ($('#txtCorreo').val() != '')) { strError.append('&nbsp;&nbsp;- El Correo ingresado no es valido'); }
        fn_validarDuplicidad(strError, $('#hidCodigoContacto').val());

        if (strError.toString() != '') {
            parent.fn_unBlockUI();
            parent.fn_mdl_alert(strError.toString(), function() { });
            strError = null;
        }
        else {

            var cargo;
            var codCargo;
            if ($('#ddlCargo').val() == "0") {
                cargo = "";

            } else {
                cargo = txtCargo;
            }

            if ($('#ddlCargo').val() == '0') {
                codCargo = "";

            } else {
                codCargo = $('#ddlCargo').val();
            }

            var su = jQuery("#jqGrid_lista_B").jqGrid('setRowData', $('#hidCodigoContacto').val(), { Nombre: $('#txtNombreContacto').val(), Cargo: cargo, Correo: $('#txtCorreo').val(), Telefono: $('#txtTelefono').val(), CodCargo: codCargo, CodigoContacto: $('#hidCodigoContacto').val() });
            if (su) {
                $('#txtNombreContacto').val('');
                $('#txtCorreo').val('');
                $('#txtTelefono').val('');
                $('#ddlCargo').val(0);
                $('#hidCodigoContacto').val('0');
                // $("#divEditar").hide();
                $("#divAgregar").show();
                $("#divCancelar").hide();

                parent.fn_mdl_mensajeIco("Los datos se actualizaron satisfactoriamente", "util/images/ok.gif", "CONFIRMACION");
                $("#dv_BotonesNormales").show();
                $("#dv_BotonesEdicion").hide();
            } else {
                parent.fn_mdl_mensajeIco("Se produjo un error al actualizar el registro", "util/images/error.gif", "ERROR");
            }

        }
    }
    else {
        parent.fn_mdl_mensajeIco("&nbsp;&nbsp;- Debe seleccionar un registro de la lista para editar", "Util/images/warning.gif", "ADVERTENCIA EN EDITAR");
    }
}

//****************************************************************
// Funcion		:: 	fn_cancelarContacto
// Descripción	::	Cancela la operación de edición,volviendo a sus estado original.
// Log			:: 	WCR - 04/06/2012
//****************************************************************
function fn_cancelarContacto() {

    fn_mdl_confirma('¿Esta seguro de cancelar?',
            function() {
                $('#txtNombreContacto').val('');
                $('#ddlCargo').val(0);
                $('#txtCorreo').val('');
                $('#txtTelefono').val('');
                $('#hidCodigoContacto').val('0');
                $("#divEditar").hide();
                $("#divCancelar").hide();
                $("#divAgregar").show();
                $("#txtNombreContacto").attr('class', "inputform");
                $("#txtCorreo").attr('class', "inputformnormal");
            },
            '../util/images/question.gif',
            function() { },
            'Proveedor');

}
//****************************************************************
// Funcion		:: 	fn_selectContacto
// Descripción	::	
// Log			:: 	WCR - 04/06/2012
//****************************************************************
function fn_selectContacto(pData) {


    $('#txtNombreContacto').val(pData.Nombre);
    $('#ddlCargo').val(fn_util_trim(pData.CodCargo));
    //$("#ddlCargo").find("option:selected").text(pData.Cargo);
    $('#txtCorreo').val(pData.Correo);
    $('#txtTelefono').val(pData.Telefono);
    $('#hidCodigoContacto').val(pData.CodigoContacto);
    $("#txtNombreContacto").attr('class', "inputform");
    $("#txtCorreo").attr('class', "inputformnormal");
    $("#divEditar").show();
    $("#divAgregar").hide();
    $("#divCancelar").show();
}

//************************************************************
// Función		:: 	fn_validarCampos
// Descripcion 	:: 	Método para validar campos por el tipo de documento
// Log			:: 	AEP - 11/07/2012
//************************************************************

//debugger;
function fn_validarCampos(pvalue) {
    //$("#txtNroDocumento").val('');

    if ($("#hidCodProveedor").val == "0") {

        $("#txtNroDocumento").attr('disabled', 'disabled');

        if (pvalue == '0') {
            $("#txtNroDocumento").attr('disabled', 'disabled');

        }
        if (pvalue == '1') { //DNI
            $("#txtNroDocumento").attr('disabled', false);

            $("#txtNroDocumento").validText({ type: 'number', length: 8 });
        }
        if (pvalue == '2') { // RUC
            $("#txtNroDocumento").attr('disabled', false);

            $("#txtNroDocumento").validText({ type: 'number', length: 11 });
        }
        if (pvalue == '3') {
            $("#txtNroDocumento").attr('disabled', false);

            $("#txtNroDocumento").validText({ type: 'alphanumeric', length: 11 });
        }
        if (pvalue == '5') {
            $("#txtNroDocumento").attr('disabled', false);

            $("#txtNroDocumento").validText({ type: 'alphanumeric', length: 11 });
        }
        if (pvalue == '6') {
            $("#txtNroDocumento").attr('disabled', false);

            $("#txtNroDocumento").validText({ type: 'alphanumeric', length: 11 });
        }



        if ((pvalue == strTipoDocumentoRuc) && ($('#cmbTipoPersona').val() == strTipoPersonaJuridica)) {
            //$("#cmbTipoCuenta3").attr('disabled', false);
            $("#txtNumCuenta3").attr('disabled', false);
        } else {
            //$("#cmbTipoCuenta3").attr('disabled', 'disabled');
            $("#txtNumCuenta3").attr('disabled', 'disabled');
        }
    } else {
        if ((pvalue == strTipoDocumentoRuc) && ($('#cmbTipoPersona').val() == strTipoPersonaJuridica)) {
            //$("#cmbTipoCuenta3").attr('disabled', false);
            $("#txtNumCuenta3").attr('disabled', false);
        } else {
            //$("#cmbTipoCuenta3").attr('disabled', 'disabled');
            $("#txtNumCuenta3").attr('disabled', 'disabled');
        }
    }
}

//************************************************************
// Función		:: 	fn_EditarContacto
// Descripcion 	::  Le permite actualizar los datos del contacto.
// Log			:: 	AEP - 11/07/2012
//************************************************************  
function fn_EditarContacto() {
    var id = $("#hddRowId").val();

    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un contacto para poder modificarlo.", "util/images/warning.gif", "EDITAR CONTACTO");
    } else {
        var vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow');
        if (vElementosAEditar.length > 1) {
            parent.fn_mdl_mensajeIco("Seleccione un sólo contacto.", "util/images/warning.gif", "EDITAR CONTACTO");
        } else {
            //fn_HabilitarControlesRepresentantes(true);

            if (vElementosAEditar != "") {
                var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', vElementosAEditar[0]);
                fn_selectContacto(rowData);
                //fn_LeerRepresentante(id);

                $("#dv_BotonesNormales").hide();
                $("#dv_BotonesEdicion").show();
            } else {
                parent.fn_mdl_mensajeIco("Seleccione un sólo contacto.", "util/images/warning.gif", "EDITAR CONTACTO");
            }
        }
    }
}

//************************************************************
// Función		:: 	fn_CancelarContacto
// Descripción 	:: 	Cancela la operación de agregar o editar y configura
//                  los controles a su estado original
// Log			:: 	AEP - 11/07/2012
//************************************************************
function fn_CancelarContacto() {
    $("#dv_BotonesNormales").show();
    $("#dv_BotonesEdicion").hide();
    $('#txtNombreContacto').val('');
    $('#txtCorreo').val('');
    $('#txtTelefono').val('');
    $('#hidCodigoContacto').val('0');
    $('#ddlCargo').val(0);
    $("#divCancelar").hide();
    $("#divAgregar").show();
    $("#txtNombreContacto").attr('class', "inputform");
    $("#txtCorreo").attr('class', "inputformnormal");
    //$("#hddCambiosSinGuardar").val("0");
    $("#hddRowId").val("");
    $("#jqGrid_lista_B").jqGrid('resetSelection');
    //fn_seteaCamposObligatorios();
}
//************************************************************
// Función		:: 	fn_ValidarCampo_TipoPersona
// Descripción 	:: 	valida la activacion de la cuenta del banco de la nacion
//                  los controles a su estado original
// Log			:: 	AEP - 12/07/2012
//*
function fn_ValidarCampo_TipoPersona(pvalue) {
    if ($("#hidCodProveedor").val() == "0") {
        fn_cargaComboTipoDocumento();
        if ($("#ddlProcedencia").val() == strPeruana && (pvalue == strTipoPersonaJuridica)) {
            $("#cmbTipoDocumento option[value='1']").remove();
            $("#cmbTipoDocumento option[value='3']").remove();
            $("#cmbTipoDocumento option[value='4']").remove();
            $("#cmbTipoDocumento option[value='5']").remove();
            $("#cmbTipoDocumento option[value='6']").remove();
        } else if ($("#ddlProcedencia").val() == strPeruana && (pvalue == strTipoPersonaNatural)) {
            $("#cmbTipoDocumento option[value='2']").remove();
            $("#cmbTipoDocumento option[value='3']").remove();
            $("#cmbTipoDocumento option[value='4']").remove();
            $("#cmbTipoDocumento option[value='5']").remove();
            $("#cmbTipoDocumento option[value='6']").remove();
        } else if ($("#ddlProcedencia").val() == strExtranjero && (pvalue == strTipoPersonaNatural)) {
            $("#cmbTipoDocumento option[value='1']").remove();
            $("#cmbTipoDocumento option[value='2']").remove();
            $("#cmbTipoDocumento option[value='3']").remove();
            $("#cmbTipoDocumento option[value='4']").remove();
            $("#cmbTipoDocumento option[value='5']").remove();
        } else if ($("#ddlProcedencia").val() == strExtranjero && (pvalue == strTipoPersonaJuridica)) {
            $("#cmbTipoDocumento option[value='1']").remove();
            $("#cmbTipoDocumento option[value='2']").remove();
            $("#cmbTipoDocumento option[value='3']").remove();
            $("#cmbTipoDocumento option[value='4']").remove();
            $("#cmbTipoDocumento option[value='5']").remove();
        } else {
            $("#cmbTipoDocumento option[value='1']").remove();
            $("#cmbTipoDocumento option[value='2']").remove();
            $("#cmbTipoDocumento option[value='3']").remove();
            $("#cmbTipoDocumento option[value='4']").remove();
            $("#cmbTipoDocumento option[value='5']").remove();
            $("#cmbTipoDocumento option[value='6']").remove();
        }

        if ($("#ddlProcedencia").val() == strPeruana || $("#ddlProcedencia").val() == strExtranjero) {
            if (($('#cmbTipoDocumento').val() == strTipoDocumentoRuc) && (pvalue == strTipoPersonaJuridica)) {
                //$("#cmbTipoCuenta3").attr('disabled',false);
                $("#txtNumCuenta3").attr('disabled', false);
            } else {
                //$("#cmbTipoCuenta3").attr('disabled','disabled');
                $("#txtNumCuenta3").attr('disabled', 'disabled');
            }
        } else {
            //$("#cmbTipoCuenta3").attr('disabled','disabled');
            $("#txtNumCuenta3").attr('disabled', 'disabled');
        }
    }
}

//****************************************************************
// Funcion		:: 	fn_cancelar
// Descripción	::	Regresa a la busqueda de proveedores.
// Log			:: 	AEP - 20/07/2012
//****************************************************************
function fn_cancelar() {
    fn_mdl_confirma('¿Está seguro de volver?',
		function() {
		    parent.fn_blockUI();
		    fn_util_redirect('frmProveedorListado.aspx');
		},
		"../util/images/question.gif",
		function() {
		},
		'Mantenimiento Proveedor'
	);
}

//****************************************************************
// Funcion		:: 	fn_PoneDatosClienteRM
// Descripción	::	Pone Datos del Cliente RM
// Log			:: 	AEP - 30/07/2012
//****************************************************************
var fn_PoneDatosClienteRM = function(response) {

    var objEClienteRM = response;

    if (objEClienteRM.CodError == 1) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objEClienteRM.MsgError, "util/images/error.gif", "ERROR EN CONSULTA RM");
    } else {
        $('#txtRazonSocial').val("");
        $('#txtDireccion').val("");
        
        $('#txtRazonSocial').val(objEClienteRM.Razonsocialcliente);
        $('#txtDireccion').val(objEClienteRM.Direccion);
        $('#HidCodigoUnico').val(objEClienteRM.Codigounico);
        var strNumeroDocumento = objEClienteRM.Numerodocumento;
    }
};

//****************************************************************
// Funcion		:: 	fn_RetornarCodigoClienteRM
// Descripción	::	Retorna el código úinico del Cliente RM
// Log			:: 	AEP - 14/08/2012
//****************************************************************
var fn_RetornarCodigoClienteRM = function(response) {

    var objEClienteRM = response;

    if (objEClienteRM.CodError == 1) {
        $('#HidCodigoUnico').val('0');
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objEClienteRM.MsgError, "util/images/error.gif", "ERROR EN CONSULTA RM");
    } else {

        $('#HidCodigoUnico').val(objEClienteRM.Codigounico);

    }
};

function fn_validaCuenta(result) {
    //  CUENTA Nº 1
    if ($("#cmbTipoCuenta1").val() == "01") {
        var strargFCDTIPOCUENTA = "IM";
    } else {
        strargFCDTIPOCUENTA = "ST";
    }

    if ($("#cmbMoneda1").val() == "001") {
        var strargFCDCODMONEDA = "001";
    } else {
        strargFCDCODMONEDA = "010";
    }

    if ($("#cmbTipoCuenta1").val() == "01") {
        var strCoCategoria = "001";
    } else {
        strCoCategoria = "002";
    }
    var NumeroCuenta = fn_util_trim($("#txtNumCuenta1").val());

    //  CUENTA Nº 2
    if ($("#cmbTipoCuenta2").val() == "01") {
        var strargFCDTIPOCUENTA2 = "IM";
    } else {
        strargFCDTIPOCUENTA2 = "ST";
    }

    if ($("#cmbMoneda2").val() == "001") {
        var strargFCDCODMONEDA2 = "001";
    } else {
        strargFCDCODMONEDA2 = "010";
    }

    if ($("#cmbTipoCuenta2").val() == "01") {
        var strCoCategoria2 = "001";
    } else {
        strCoCategoria2 = "002";
    }

    var NumeroCuenta2 = fn_util_trim($("#txtNumCuenta2").val());

    var validacta2;
    if ((NumeroCuenta.length) == 13) {

        if ($("#cmbTipoCuenta2").val() != 0 || $("#cmbMoneda2").val() != 0) {
            if ((NumeroCuenta2.length) == 13) {
                validacta2 = "true";
                if (fn_util_trim(NumeroCuenta2) == fn_util_trim(NumeroCuenta)) {
                    validacta2 = "false";
                    $("#hddValidaCuenta").val("1" + "|" + "Los Numeros de Cuenta no Pueden ser Iguales");
                } else {
                    validacta2 = "true";
                }
            } else {
                validacta2 = "false";
                $("#hddValidaCuenta").val("1" + "|" + "La Segunda Cuenta debe tener 13 Digitos");
            }
        } else {
            validacta2 = "true";
        }


        if (validacta2 == "true") {
            var arrParametros3 = ["argFCDTIPOCUENTA", strargFCDTIPOCUENTA,
                                          "argFCDCODMONEDA", strargFCDCODMONEDA,
                                          "argFCDCODTIENDA", NumeroCuenta.substr(0, 3),
                                          "argFCDCODCATEGORIA", strCoCategoria,
                                          "argFCDNUMCUENTA", NumeroCuenta.substr(3, 12),
            //CUENTA 2
                                          "argFCDTIPOCUENTA2", strargFCD1 | TIPOCUENTA2,
                                          "argFCDCODMONEDA2", strargFCDCODMONEDA2,
                                          "argFCDCODTIENDA2", NumeroCuenta2.substr(0, 3),
                                          "argFCDCODCATEGORIA2", strCoCategoria2,
                                          "argFCDNUMCUENTA2", NumeroCuenta2.substr(3, 12),
                    	                  "pCodUnico", $('#HidCodigoUnico').val()
                                         ];
            fn_util_AjaxSyncWM("../Verificacion/frmCheckListComercialRegistro.aspx/ValidaCuentaST",
                     arrParametros3,
                     function(result) {
                         $("#hddValidaCuenta").val(result);
                     },
                     function(result) {
                         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                         result(false);
                     });
        }

    } else {
        validacta2 = "false";
        $("#hddValidaCuenta").val("1" + "|" + "La Primera Cuenta debe tener 13 Digitos");
    }

}

//Inicio JJM IBK
function fn_eliminar() {
    var Mensaje = '';
    var arrParametros = ["pCodProveedor", $("#hidCodProveedor").val()];
    fn_util_AjaxSyncWM("frmProveedorMant.aspx/ListaProveedorDocumento",
                     arrParametros,
                     function(result) {
                         Mensaje = result;
                     },
                     function(result) {
                         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                         result(false);
                     });

    if (Mensaje == '0') {
        //Elimina
        fn_mdl_confirma('¿Está seguro de Eliminar al proveedor?',
    			function() {
    			    parent.fn_blockUI();
    			    var arrParametros = ["pCodProveedor", $("#hidCodProveedor").val(),
    					"pCodEstadoLogico", '0'
    				];

    			    fn_util_AjaxWM("frmProveedorMant.aspx/EliminarRegistro",
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
    			'Proveedor'
    		);

    }
    else {
        //Avisa y elimina
        fn_mdl_confirma('El proveedor tiene documentos generados. ¿Está seguro de Eliminar al proveedor?',
    			function() {
    			    parent.fn_blockUI();
    			    var arrParametros = ["pCodProveedor", $("#hidCodProveedor").val(),
    					"pCodEstadoLogico", '0'
    				];
    			    fn_util_AjaxWM("frmProveedorMant.aspx/EliminarRegistro",
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
    			'Proveedor'
    		);

    }
}
//Fin IBK