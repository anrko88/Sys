var bFirstClick;
var EnviadoCliente;
var CrLf = 1;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    // Inicializa el valor de campos.
    fn_inicializaCampos();

    // Configura la estructura de la grilla.
    fn_configuraGrilla();

    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscar(true);
        }
    });

    // On load Page (siempre al final).
    fn_onLoadPage();
});

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_configuraGrilla() {
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            fn_buscarListar();
        },
        jsonReader: {//Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodSolicitudCredito" // Índice de la columna con la clave primaria.
        },
        colNames: ['Nº Contrato', 'Nº Cotización', 'CU Cliente', 'Razón Social o Nombre', 'Clasificación Bien', 'Estado', 'Fecha', 'Tipo Contrato', 'CodigoEstadoContrato'],
        colModel: [
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', sortable: true, width: 35, sorttype: "string", align: "center" },
		    { name: 'CodigoCotizacion', index: 'CodigoCotizacion', sortable: true, width: 35, sorttype: "string", align: "center" },
		    { name: 'CodUnico', index: 'CodUnico', sortable: true, sorttype: "string", width: 35, align: "center" },
		    { name: 'NombreSubprestatario', index: 'NombreSubprestatario', sortable: true, sorttype: "string", width: 75, align: "left" },
		    { name: 'ClasificacionBien', index: 'ClasificacionBien', sortable: true, sorttype: "string", width: 75, align: "center" },
		    { name: 'EstadoContrato', index: 'EstadoContrato', sortable: true, width: 50, sorttype: "string", align: "center" },
		    { name: 'FechaSolicitudCredito', index: 'FechaSolicitudCredito', hidden: true, formatter: fn_util_ValidaStringFecha },
		    { name: 'SubTipoContrato', index: 'SubTipoContrato', sortable: true, sorttype: "string", width: 50, align: "center" },
		    { name: 'CodigoEstadoContrato', index: 'CodigoEstadoContrato', hidden: true }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10, // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito', // Columna a ordenar por defecto.
        sortorder: 'desc', // Criterio de ordenación por defecto.
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodigodesembolso").val(rowData.CodSolicitudCredito);
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();

            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            window.location = "frmDesembolsoRegistro.aspx?hcontrato=" + rowData.CodSolicitudCredito;

            parent.fn_unBlockUI();
        }
    });

    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 85);
}

// ****************************************************************
// Funcion		:: 	fn_abreDetalle
// Descripción	::	Abre Detalle
// Log			:: 	IJM - 06/03/2012
//****************************************************************
function fn_abreDetalle() {
    try {
        parent.fn_blockUI();
        var strId = fn_util_trim($("#hddCodigodesembolso").val());

        if (strId == "" || strId == null || strId == undefined) {
            parent.fn_mdl_mensajeIco("Debe seleccionar un registro.", "util/images/warning.gif", "ERROR EN SELECCION");
        } else {
            fn_util_redirect("frmContratoRegistro.aspx?pTipoTransaccion=EDITAR&hddCodigo=" + strId);
        }

        parent.fn_unBlockUI();
        fn_doResize();
    } catch (e) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa las variables globales y los controles.
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
    bFirstClick = false;
    EnviadoCliente = "04";

    fn_InicializarControles();
}

//****************************************************************
// Funcion		:: 	fn_InicializarControles
// Descripción	::	Inicializa los controles
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_InicializarControles() {
    // Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    $('#txtContrato').validText({ type: 'number', length: 8 });
    $('#txtCotizacion').validText({ type: 'number', length: 8 });
    $('#txtCuCliente').validText({ type: 'number', length: 10 });
    $('#txtRazonSocial').validText({ type: 'comment', length: 100 });
}

//****************************************************************
// Funcion		:: 	fn_limpiar
// Descripción	::	Limpia las grillas 
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_limpiar() {
    try {
        parent.fn_blockUI();

        bFirstClick = false;

        $('#txtContrato').val("");
        $('#txtCuCliente').val("");
        $('#txtRazonSocial').val("");
        $('#txtCotizacion').val("");
        $('#txtFechaIni').val("");
        $('#txtFechaFin').val("");
        $("#cmbEjecutivo option:first").attr('selected', 'selected');
        $("#cmbMoneda option:first").attr('selected', 'selected');
        $("#cmbEstado option:first").attr('selected', 'selected');
        $("#cmbZonal option:first").attr('selected', 'selected');
        $("#cmbClasificacion option:first").attr('selected', 'selected');
        $("#cmbClasificacionContrato option:first").attr('selected', 'selected');
        $("#cmbBanca option:first").attr('selected', 'selected');
        $("#cmbTipoPersona option:first").attr('selected', 'selected');
        $("#cmbNotaria option:first").attr('selected', 'selected');
        $("#txtKardex").val("");

        $("#jqGrid_lista_A").GridUnload();
        fn_configuraGrilla();

        parent.fn_unBlockUI();
        fn_doResize();
    } catch (e) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }
}

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	Ejecuta la búsqueda de los documentos, cuando el usuario hace click en
//                  el botón 'Buscar'.
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_buscar(bSearch) {
    bFirstClick = bSearch;

    fn_buscarListar();
}

//****************************************************************
// Funcion		:: 	fn_buscarListarContratos
// Descripción	::	
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_buscarListar() {

    if (!bFirstClick) {
        return;
    }

    try {
        parent.fn_blockUI();

        var contrato = $('#txtContrato').val() == undefined ? "" : $('#txtContrato').val();
        var cuCliente = $('#txtCuCliente').val() == undefined ? "" : $('#txtCuCliente').val();
        var razonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();

        var clasificacion = $("#cmbClasificacion option:selected").val() == "0" ? "" : $("#cmbClasificacion option:selected").val();
        var moneda = $("#cmbMoneda option:selected").val() == "0" ? "" : $("#cmbMoneda option:selected").val();
        var clasificacionContrato = $("#cmbClasificacionContrato option:selected").val() == "0" ? "" : $("#cmbClasificacionContrato option:selected").val();

        var estado = $("#cmbEstado option:selected").val() == "0" ? "" : $("#cmbEstado option:selected").val();
        var ejecutivo = $("#cmbEjecutivo option:selected").val() == "0" ? "" : $("#cmbEjecutivo option:selected").val();
        var banca = $("#cmbBanca option:selected").val() == "0" ? "" : $("#cmbBanca option:selected").val();


        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.                                 
                             "pContrato", contrato,
                             "pCuCliente", cuCliente,
                             "pRazonSocial", razonSocial,
                             "pClasificacion", clasificacion,
                             "pMoneda", moneda,
                             "pCodigoSubTipoContrato", clasificacionContrato,
                             "pEstado", estado,
                             "pEjecutivo", ejecutivo,
                             "pBanca", banca
                            ];

        fn_util_AjaxWM("frmDesembolsoListado.aspx/BuscarDocumentos",
                       arrParametros,
                       function(jsondata) {
                           jqGrid_lista_A.addJSONData(jsondata);
                           parent.fn_unBlockUI();
                       	   fn_doResize();
                       },
                       function(request) {
                           parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR EN LA BÚSQUEDA");
                       });


        parent.fn_unBlockUI();
        fn_doResize();
    } catch (ex) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }
}

//****************************************************************
// Funcion		:: 	fn_editar
// Descripción	::	Abre Detalle
// Log			:: 	IJM - 06/03/2012
//****************************************************************
function fn_editar() {
    var strId = $("#hddCodigodesembolso").val();

    if (strId == "" || strId == null) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro.", "util/images/warning.gif", "ERROR EN SELECCION");
    } else {
        window.location = "frmDesembolsoRegistro.aspx?hcontrato=" + $("#hddCodigodesembolso").val();
    }
}

function fn_registrocompras() {
    //window.open("../Reportes/frmRepRegistroCompra.aspx")
    parent.fn_util_AbreModal("Reporte Registro de Compras", "Reportes/frmConsultaRegistroCompra.aspx", 600, 220, function() { });
}

function fn_retenciones() {
    //window.open("../Reportes/frmRepRegistroCompra.aspx")
    parent.fn_util_AbreModal("Reporte Registro de Retenciones", "Reportes/frmConsultaRetenciones.aspx", 600, 220, function() { });
}
function fn_retenciones4ta() {
    //window.open("../Reportes/frmRepRegistroCompra.aspx")
    parent.fn_util_AbreModal("Reporte Retenciones 4ta Categoria", "Reportes/frmRepRetencionCuarta.aspx", 600, 220, function() { });
}
function fn_regventa() {
    //window.open("../Reportes/frmRepRegistroCompra.aspx")
    parent.fn_util_AbreModal("Reporte Registro de Ventas", "Reportes/frmRepRegVenta.aspx", 600, 220, function() { });
}
function fn_ventas() {
    //window.open("../Reportes/frmRepRegistroCompra.aspx")
    parent.fn_util_AbreModal("Reporte Ventas", "Reportes/frmReporteVentas.aspx", 600, 220, function() { });
}
//Inicio IBK - AAE - Funcion para abrir modal de notas de abono
function fn_notasAbono() {
    parent.fn_util_AbreModal("Notas de Abono", "Reportes/frmNotasAbono.aspx", 600, 220, function() { });
}