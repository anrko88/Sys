var blnPrimeraBusqueda;
var intPaginaActual = 1;
var Perfil = '';
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
//****************************************************************
$(document).ready(function() {
    fn_doResize();
    Perfil = $("#hddPerfil").val();
    fn_cargaGrilla();
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));
    $("#cmbTipoModalidad").change(function() {
        fn_buscarTipoCambio(true);
    });
    //    $('#txtValorCompra').validText({ type: 'number', length: 18 });
    //    $('#txtValorVenta').validText({ type: 'number', length: 18 });
    //$('#txtValorCompra').val(Fn_util_ReturnValidDecimal2($('#txtValorCompra').val()));
    //    $('#txtValorVenta').val(Fn_util_ReturnValidDecimal2($('#txtValorVenta').val()));
    //debugger;
    $('#txtValorCompra').validNumber({ value: '', decimals: 3, length: 10 });
    $('#txtValorVenta').validNumber({ value: '', decimals: 3, length: 15 });

    if ($("#hidOpcion").val() == '2') {

        $("#txtFechaIni").datepicker({
            dateFormat: "dd/mm/yy"
        }).datepicker("setDate", new Date());

        $("#txtFechaFin").datepicker({
            dateFormat: "dd/mm/yy"
        }).datepicker("setDate", new Date());

        fn_util_SeteaObligatorio($("#txtFechaIni"), "input");
        fn_util_SeteaObligatorio($("#txtFechaFin"), "input");
        fn_util_SeteaObligatorio($("#cmbTipoModalidad"), "input");
        fn_util_SeteaObligatorio($("#txtValorCompra"), "input");
        fn_util_SeteaObligatorio($("#txtValorVenta"), "input");



        $('#txtFechaIni').removeAttr('disabled');
        $('#txtFechaIni').attr('enabled', true);
        $('#txtFechaIni').attr('enabled', 'enabled');

        $('#txtFechaFin').removeAttr('disabled');
        $('#txtFechaFin').attr('enabled', true);
        $('#txtFechaFin').attr('enabled', 'enabled');

        $('#txtValorCompra').removeAttr('disabled');
        $('#txtValorCompra').attr('enabled', true);
        $('#txtValorCompra').attr('enabled', 'enabled');

        $('#txtValorVenta').removeAttr('disabled');
        $('#txtValorVenta').attr('enabled', true);
        $('#txtValorVenta').attr('enabled', 'enabled');


        $("#txtNombreMoneda").val('DOLARES USA');
        $("#txtNombreMoneda").attr('disabled', 'disabled');

        $("#dv_img_boton2").show();
        $("#dv_img_boton3").hide();
        $("#dv_img_boton4").hide();
    }
    if ($("#hidOpcion").val() == '1') {
        fn_inicializaCampos();
        $("#dv_img_boton3").show();
        $("#dv_img_boton2").hide();
        $("#dv_img_boton4").show();
    }
    fn_buscar();
    fn_onLoadPage();

});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	En este método se realiza validacion de campos como tamaño, tipo de dato, etc.
//              ::  El método es invocado al inicio de la carga de la página.
// Log			:: 	JRC - 05/06/2012
// Modificacion ::  AEP - 12/07/2012
// Se realiza la validación para desactivar los controles cuando este en modo de edición
// cuando el hidCodTipoCambio=0 eso quiere decir que la operación a realizar es un nuevo registro
// en caso contrario se esta realizando una edición, y procedemos a desactivar los controles.	
//****************************************************************

function fn_inicializaCampos() {

    var CodTipoCambio = $('#hidCodTipoCambio').val() == undefined ? "" : $('#hidCodTipoCambio').val();

    if (CodTipoCambio != "0") {
        $("#cmbTipoModalidad").attr('disabled', 'disabled');
        $("#txtFechaIni").attr('disabled', 'disabled');
        $("#txtFechaFin").attr('disabled', 'disabled');
        $("#txtNombreMoneda").attr('disabled', 'disabled');
    } else {
        $("#txtNombreMoneda").val('DOLARES USA');
        $("#txtNombreMoneda").attr('disabled', 'disabled');
    }

    //Valida Tipo de Datos
    //$("#txtValorCompra").validNumber({ value: '', decimals: 5, length: 10 });
    //$("#txtValorVenta").validNumber({ value: '', decimals: 5, length: 10 });
}


//************************************************************
// Función		:: 	fn_grabar
// Descripcion 	:: 	Método que graba los valores del proveedor,contactos y cuentas.
// Log			:: 	JRC - 10/02/2012
// Modificado   ::  AEP - 19/07/2012
//************************************************************
function fn_grabar() {
    var rows = jQuery("#jqGrid_lista_A").jqGrid('getRowData');
    var strError = new StringBuilderEx();
    var strMensaje;
    strMensaje = '¿Está seguro de guardar el Tipo de Cambio?'
    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        var sTipoModalidadCambio = row.TipoModalidadCambio;
        var sFechaInicio = row.FechaInicioVigencia;
        var sFechaFin = row.FechaFinalVigencia;
        if (sFechaInicio == $('#txtFechaIni').val() && sFechaFin == $('#txtFechaFin').val()) {
            if ((Perfil == '6') || (Perfil == '11') || (Perfil == '1')) {
                if (sTipoModalidadCambio == $('#cmbTipoModalidad').val()) {
                    strMensaje = 'El tipo de cambio ya existe. ¿Desea modificarlo?';
                    break;
                }
            }
            else {
                parent.fn_mdl_mensajeIco("El Tipo de Cambio ya existe. Solo un Administrador puede actualizar un Tipo de Cambio", "util/images/warning.gif", "ADVERTENCIA");
                strMensaje = '';
                break;
            }
        }
    }
    fn_validarRegistro(strError);
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }
    else {

        if (($("#cmbTipoModalidad").val() == "0") || ($("#cmbTipoModalidad").val() == "")) {
            parent.fn_mdl_mensajeIco("Seleccione una de Modaldiad para Tipo de Cambio", "util/images/warning.gif", "ADVERTENCIA");
            return;
        }
        if (strMensaje == '¿Está seguro de guardar el Tipo de Cambio?') {
            fn_mdl_confirma(strMensaje,
        			function() {

        			    parent.fn_blockUI();
        			    var arrParametros = ["pCodTipoModalidad", $('#cmbTipoModalidad').val(),
        					"ptxtFechaIni", $("#txtFechaIni").val(),
        					"ptxtFechaFin", $("#txtFechaFin").val(),
        					"ptxtValorCompra", $("#txtValorCompra").val(),
        					"ptxtValorVenta", $("#txtValorVenta").val()
        				];

        			    fn_util_AjaxWM("frmRegistroTipoCambio.aspx/GuardarRegistro",
        					arrParametros,
        					        fn_MensajeYRedireccionar,
    			            function(request) {
    			                parent.fn_unBlockUI();
    			                var error = eval("(" + resultado.responseText + ")");
    			                parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al Tipo Cambio");
    			            });
        			},
                    "../util/images/question.gif",
        			function() {
        			},
        			'Tipo de Cambio'
        		);
        }
        else if (strMensaje != '') {
            fn_mdl_confirma(strMensaje,
        			function() {

        			    parent.fn_blockUI();
        			    var arrParametros = ["pCodTipoModalidad", $('#cmbTipoModalidad').val(),
        					"ptxtFechaIni", $("#txtFechaIni").val(),
        					"ptxtFechaFin", $("#txtFechaFin").val(),
        					"ptxtValorCompra", $("#txtValorCompra").val(),
        					"ptxtValorVenta", $("#txtValorVenta").val()
        				];

        			    fn_util_AjaxWM("frmRegistroTipoCambio.aspx/ActualizaRegistro",
        					arrParametros,
        					fn_MensajeYRedireccionar,
    			            function(request) {
    			                parent.fn_unBlockUI();
    			                var error = eval("(" + resultado.responseText + ")");
    			                parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al Tipo Cambio");
    			            });
        			},
                    "../util/images/question.gif",
        			function() {
        			},
        			'Tipo de Cambio'
        		);
        }

    }
}
function fn_actualizar() {

    var strError = new StringBuilderEx();


    if ((Perfil == '6') || (Perfil == '11') || (Perfil == '1')) {
        fn_validarRegistro(strError);
        if (strError.toString() != '') {
            parent.fn_unBlockUI();
            parent.fn_mdl_alert(strError.toString(), function() { });
            strError = null;
        }
        else {

            if (($("#cmbTipoModalidad").val() == "0") || ($("#cmbTipoModalidad").val() == "")) {
                parent.fn_mdl_mensajeIco("Seleccione una de Modaldiad para Tipo de Cambio", "util/images/warning.gif", "ADVERTENCIA");
                return;
            }

            //        
            fn_mdl_confirma('¿Está seguro de actualizar el Tipo de Cambio?',
        			function() {

        			    parent.fn_blockUI();
        			    var arrParametros = ["pCodTipoModalidad", $('#cmbTipoModalidad').val(),
        					"ptxtFechaIni", $("#txtFechaIni").val(),
        					"ptxtFechaFin", $("#txtFechaFin").val(),
        					"ptxtValorCompra", $("#txtValorCompra").val(),
        					"ptxtValorVenta", $("#txtValorVenta").val()
        				];

        			    fn_util_AjaxWM("frmRegistroTipoCambio.aspx/ActualizaRegistro",
        					arrParametros,
        					fn_MensajeYRedireccionar,
    			            function(request) {
    			                parent.fn_unBlockUI();
    			                var error = eval("(" + resultado.responseText + ")");
    			                parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al Tipo Cambio");
    			            });
        			},
                    "../util/images/question.gif",
        			function() {
        			},
        			'Tipo de Cambio'
        		);
        }
    }
    else { parent.fn_mdl_mensajeIco("Solo un Administrador puede actualizar un Tipo de Cambio", "util/images/warning.gif", "ADVERTENCIA"); }
}
function fn_eliminar() {

    var strError = new StringBuilderEx();
    if ((Perfil == '6') || (Perfil == '11') || (Perfil == '1')) {
        fn_validarRegistro(strError);
        if (strError.toString() != '') {
            parent.fn_unBlockUI();
            parent.fn_mdl_alert(strError.toString(), function() { });
            strError = null;
        }
        else {

            if (($("#cmbTipoModalidad").val() == "0") || ($("#cmbTipoModalidad").val() == "")) {
                parent.fn_mdl_mensajeIco("Seleccione una de Modaldiad para Tipo de Cambio", "util/images/warning.gif", "ADVERTENCIA");
                return;
            }

            //
            fn_mdl_confirma('¿Está seguro de Eliminar el Tipo de Cambio?',
        			function() {

        			    parent.fn_blockUI();
        			    var arrParametros = ["pCodTipoModalidad", $('#cmbTipoModalidad').val(),
        					"ptxtFechaIni", $("#txtFechaIni").val(),
        					"ptxtFechaFin", $("#txtFechaFin").val()
        				];

        			    fn_util_AjaxWM("frmRegistroTipoCambio.aspx/EliminaRegistro",
        					arrParametros,
        					function(resultado) {
        			            parent.fn_unBlockUI();
        					    fn_util_globalRedirect("/Administracion/frmListadoTipoCambio.aspx")
        					},
    			            function(request) {
    			                parent.fn_unBlockUI();
    			                var error = eval("(" + resultado.responseText + ")");
    			                parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "Error al Tipo Cambio");
    			            });
        			},
                    "../util/images/question.gif",
        			function() {
        			},
        			'Tipo de Cambio'
        		);
        }
    }
    else { parent.fn_mdl_mensajeIco("Solo un Administrador puede eliminar un Tipo de Cambio", "util/images/warning.gif", "ADVERTENCIA"); }
}
//****************************************************************
// Función		:: 	fn_MensajeYRedireccionar
// Descripción	::	Le muestra un mensaje con el resultado de la 
//                  llamada al respectivo web method.
// Log			:: 	EBL - 07/05/2012
//****************************************************************
var fn_MensajeYRedireccionar = function(response) {
    if (response == '0' && $("#hidOpcion").val() == '2') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert("El tipo de cambio ya está registrado.", function() { fn_util_globalRedirect("/Administracion/frmRegistroTipoCambio.aspx"); });
        fn_doResize();
    }
    else {
        parent.fn_unBlockUI();
        parent.fn_util_MuestraLogPage("Los datos se registraron satisfactoriamente.", "I");
        //fn_util_globalRedirect("/Administracion/frmRegistroTipoCambio.aspx");
        fn_buscarTipoCambio(true);
        fn_doResize();
    }
};


//****************************************************************
// Funcion		:: 	fn_cancelar
// Descripción	::	Regresa a la busqueda de proveedores.
// Log			:: 	AEP - 20/07/2012
//****************************************************************
function fn_cancelar() {
    fn_mdl_confirma('¿Está seguro de volver?',
    		function() {
    		    parent.fn_blockUI();
    		    fn_util_redirect('frmListadoTipoCambio.aspx');
    		},
    		"../util/images/question.gif",
    		function() {
    		},
    		'Mantenimiento Tipo Cambio'
    	);
}

//****************************************************************
// Funcion		:: 	fn_validarRegistro
// Descripción	::	Esté metodo se ejecuta antes de grabar el registro, realizando unas validaciones de 
//                  campos obligatorios y validaciones de negocio.
// Log			:: 	WCR - 04/06/2012
// Modificado   ::  AEP - 19/07/2012
//****************************************************************
function fn_validarRegistro(pError) {


    var strFechaInicial = $("#txtFechaIni").val();
    var strFechaFinal = $("#txtFechaFin").val();

    if (fn_util_trim(strFechaInicial) == "") {
        pError.append("<br/> Debe ingresar la Fecha Inicial. ");
    }
    if (fn_util_trim(strFechaFinal) == "") {
        pError.append("<br/> Debe ingresar la Fecha Final. ");
    }
    if (strFechaInicial != strFechaFinal) {
        if (fn_util_ComparaFecha(strFechaFinal, strFechaInicial)) {
            pError.append("<br/> La fecha final no puede ser menor a la fecha inicial. ");
        }
    }
    if ($("#txtValorVenta").val() == "") {
        pError.append("<br/> El campo valor venta es obligatorio. ");
    }
    if ($("#txtValorCompra").val() == "") {
        pError.append("<br/> El campo valor compra es obligatorio. ");
    }


}
//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() {

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_buscar();

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
        colNames: ['Modalidad', 'Nombre Moneda', 'Fecha Inicio Vigencia',
                   'Fecha Final Vigencia', 'Valor de Compra', 'Valor de Venta', '', ''],
        colModel: [
                { name: 'NombreTipoModalidadCambio', index: 'NombreTipoModalidadCambio', sortable: true, sorttype: "string", width: 10, align: "center", defaultValue: "" },
                { name: 'NombreMoneda', index: 'NombreMoneda', sortable: true, sorttype: "string", width: 10, align: "center", defaultValue: "" },
                { name: 'FechaInicioVigencia', index: 'FechaInicioVigencia', sortable: true, sorttype: "string", width: 10, align: "center", defaultValue: "", formatter: fn_util_ValidaStringFecha },
                { name: 'FechaFinalVigencia', index: 'FechaFinalVigencia', sortable: true, sorttype: "string", width: 10, align: "center", defaultValue: "", formatter: fn_util_ValidaStringFecha },
                { name: 'MontoValorCompra', index: 'MontoValorCompra', sortable: true, sorttype: "float", width: 10, sorttype: "float", align: "center" },
                { name: 'MontoValorVenta', index: 'MontoValorVenta', sortable: true, sorttype: "float", width: 10, sorttype: "float", align: "center" },
                { name: 'CodMoneda', index: 'CodMoneda', hidden: true },
                { name: 'TipoModalidadCambio', index: 'TipoModalidadCambio', hidden: true }
        ],
        width: glb_intWidthPantalla - 70,
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 15,
        rowList: [10, 20, 30],
        sortname: 'FechaInicioVigencia',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //shrinkToFit: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass'
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

}
//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	
// Log			:: 	WCR - 14/05/2012
//****************************************************************
function fn_buscarTipoCambio(pblnBusqueda) {
    blnPrimeraBusqueda = pblnBusqueda;
    intPaginaActual = 1;
    fn_buscar();
}
function fn_buscar() {
    if (!blnPrimeraBusqueda) {
        return;

    } else {

        try {
            parent.fn_blockUI();

            var CodigoTipoModalidad = $('#cmbTipoModalidad').val() == undefined ? "" : $('#cmbTipoModalidad').val();
            var FechaInicioVigencia = $('#txtFechaIni').val() == undefined ? "" : $('#txtFechaIni').val();
            var FechaFinalVigencia = $('#txtFechaFin').val() == undefined ? "" : $('#txtFechaFin').val();

            var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
			                 "pCodigoTipoModalidad", CodigoTipoModalidad == '0' ? "" : CodigoTipoModalidad,
			                 "pFechaInicioVigencia", FechaInicioVigencia,
			                 "pFechaFinalVigencia", FechaFinalVigencia
		                    ];

            fn_util_AjaxWM("frmRegistroTipoCambio.aspx/BuscarTipoCambio",
			 arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);
                    parent.fn_unBlockUI();
                    fn_doResize();
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );
        } catch (ex) {
            parent.fn_unBlockUI();
            fn_util_alert(ex.message);
        }
    }
}
function DateAdd(objDate, strInterval, intIncrement) {
    if (typeof (objDate) == "string") {
        objDate = new Date(objDate);

        if (isNaN(objDate)) {
            throw ("DateAdd: Date is not a valid date");
        }
    }
    else if (typeof (objDate) != "object" || objDate.constructor.toString().indexOf("Date()") == -1) {
        throw ("DateAdd: First parameter must be a date object");
    }

    if (
        strInterval != "M"
        && strInterval != "D"
        && strInterval != "Y"
        && strInterval != "h"
        && strInterval != "m"
        && strInterval != "uM"
        && strInterval != "uD"
        && strInterval != "uY"
        && strInterval != "uh"
        && strInterval != "um"
        && strInterval != "us"
        ) {
        throw ("DateAdd: Second parameter must be M, D, Y, h, m, uM, uD, uY, uh, um or us");
    }

    if (typeof (intIncrement) != "number") {
        throw ("DateAdd: Third parameter must be a number");
    }

    switch (strInterval) {
        case "M":
            objDate.setMonth(parseInt(objDate.getMonth()) + parseInt(intIncrement));
            break;

        case "D":
            objDate.setDate(parseInt(objDate.getDate()) + parseInt(intIncrement));
            break;

        case "Y":
            objDate.setYear(parseInt(objDate.getYear()) + parseInt(intIncrement));
            break;

        case "h":
            objDate.setHours(parseInt(objDate.getHours()) + parseInt(intIncrement));
            break;

        case "m":
            objDate.setMinutes(parseInt(objDate.getMinutes()) + parseInt(intIncrement));
            break;

        case "s":
            objDate.setSeconds(parseInt(objDate.getSeconds()) + parseInt(intIncrement));
            break;

        case "uM":
            objDate.setUTCMonth(parseInt(objDate.getUTCMonth()) + parseInt(intIncrement));
            break;

        case "uD":
            objDate.setUTCDate(parseInt(objDate.getUTCDate()) + parseInt(intIncrement));
            break;

        case "uY":
            objDate.setUTCFullYear(parseInt(objDate.getUTCFullYear()) + parseInt(intIncrement));
            break;

        case "uh":
            objDate.setUTCHours(parseInt(objDate.getUTCHours()) + parseInt(intIncrement));
            break;

        case "um":
            objDate.setUTCMinutes(parseInt(objDate.getUTCMinutes()) + parseInt(intIncrement));
            break;

        case "us":
            objDate.setUTCSeconds(parseInt(objDate.getUTCSeconds()) + parseInt(intIncrement));
            break;
    }
    return objDate;
}
