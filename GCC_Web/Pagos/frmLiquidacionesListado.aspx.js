//****************************************************************
// Variables Globales
//****************************************************************
var blnPrimeraBusqueda;
var intPaginaActual = 1;


//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	IBK - RPR - 24/12/2012
//****************************************************************
$(document).ready(function() {

    //Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    //Carga Grilla
    fn_cargaGrilla();

    //Valida Campos
    fn_inicializaCampos();

    //Busca con Enter    
    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscarPagoCuotas(true);
        }
    });

    //On load Page (siempre al final)
    fn_onLoadPage();

});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	IBK - RPR - 24/12/2012
//****************************************************************
function fn_inicializaCampos() {

    blnPrimeraBusqueda = false;

    $('#txtNroContrato').validText({ type: 'number', length: 8 });
    $('#txtCodigoLiquidacion').validText({ type: 'number', length: 8 });
    $('#txtCuCliente').validText({ type: 'number', length: 10 });
    $('#txtRazonSocial').validText({ type: 'comment', length: 100 });    

}

//****************************************************************
// Funcion		:: 	fn_buscarPagoCuotas
// Descripción	::	Busca listado de pago de cuotas
// Log			:: 	IBK - RPR - 24/12/2012
//****************************************************************
function fn_buscarPagoCuotas(pblnBusqueda) {
    blnPrimeraBusqueda = pblnBusqueda;
    intPaginaActual = 1;
    fn_realizaBusquedaPagoCuotas();
}
function fn_realizaBusquedaPagoCuotas() {
    if (!blnPrimeraBusqueda) {
        return;
    } else {

        try {
            parent.fn_blockUI();

            var txtNroContrato = $('#txtNroContrato').val() == undefined ? "" : $('#txtNroContrato').val();
            var txtCodigoLiquidacion = $('#txtCodigoLiquidacion').val() == undefined ? "" : $('#txtCodigoLiquidacion').val();
            var txtCuCliente = $('#txtCuCliente').val() == undefined ? "" : $('#txtCuCliente').val();
            var txtRazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
            var txtFechaValorInicio = $('#txtFechaValorInicio').val() == undefined ? "" : $('#txtFechaValorInicio').val();
            var txtFechaValorFin = $('#txtFechaValorFin').val() == undefined ? "" : $('#txtFechaValorFin').val();
            var cmbTipoLiquidacion = $('#cmbTipoLiquidacion').val() == undefined ? "" : $('#cmbTipoLiquidacion').val();
            var cmbEstado = $('#cmbEstado').val() == undefined ? "" : $('#cmbEstado').val();
            var cmbMoneda = $('#cmbMoneda').val() == undefined ? "" : $('#cmbMoneda').val();
            var cmbFlagAdenda = $('#cmbFlagAdenda').val() == undefined ? "" : $('#cmbFlagAdenda').val();
            
            var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
                             "pstrNroContrato", txtNroContrato,
                             "pstrCodigoLiquidacion", txtCodigoLiquidacion,
                             "pstrCuCliente", txtCuCliente,
                             "pstrRazonSocial", txtRazonSocial,
                             "pstrFechaValorInicio", txtFechaValorInicio,
                             "pstrFechaValorFin", txtFechaValorFin,
                             "pstrTipoLiquidacion", cmbTipoLiquidacion,
                             "pstrEstado", cmbEstado,
                             "pstrMoneda", cmbMoneda,
                             "pstrFlagAdenda", cmbFlagAdenda];

            fn_util_AjaxWM("frmLiquidacionesListado.aspx/ListaLiquidaciones",
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


//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla Listado de Pagos
// Log			:: 	INK - RPR - 24/12/2012
//****************************************************************
function fn_cargaGrilla() {

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_realizaBusquedaPagoCuotas();
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
        colNames: ['Nº Liquidación', 'N° Contrato', 'CU Cliente', 'Razon Social o Nombre', 'Estado', 'Fecha Valor', 'Moneda', 'Valor Neto', 'IGV', 'Total'],
        colModel: [
                { name: 'CodigoLiquidacion', index: 'CodigoLiquidacion', sortable: true, sorttype: "int", width: 40, align: "center", defaultValue: "" },
                { name: 'CodOperacionActiva', index: 'CodOperacionActiva', sortable: true, sorttype: "int", width: 40, align: "center", defaultValue: "" },
                { name: 'CodUnico', index: 'CodUnico', sortable: true, sorttype: "string", width: 40, align: "center", defaultValue: "" },
                { name: 'NombreSubprestatario', index: 'NombreSubprestatario', sortable: true, sorttype: "string", width: 80, align: "left", defaultValue: "" },
                { name: 'NombreEstadoLiquidacion', index: 'NombreEstadoLiquidacion', sortable: true, width: 40, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'FechaValorLiquidacion', index: 'FechaValorLiquidacion', sortable: true, width: 25, sorttype: "string", align: "center", defaultValue: "", formatter: fn_util_ValidaStringFecha },
                { name: 'NombreMoneda', index: 'NombreMoneda', sortable: true, width: 40, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'ValorNeto', index: 'ValorNeto', sortable: true, sorttype: "float", width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'MontoIGV', index: 'MontoIGV', sortable: true, sorttype: "float", width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'MontoTotal', index: 'MontoTotal', sortable: true, sorttype: "float", width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 }
        ],
        width: glb_intWidthPantalla - 70,
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'CodigoLiquidacion',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //shrinkToFit: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodSolicitudCredito").val(rowData.CodOperacionActiva);
            $("#hddCodigoLiquidacion").val(rowData.CodigoLiquidacion);
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            window.location = "frmLiquidacionesRegistro.aspx?hddCodSolicitudCredito=" + rowData.CodOperacionActiva + "&hddCodigoLiquidacion=" + rowData.CodigoLiquidacion;
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

}


//****************************************************************
// Funcion		:: 	fn_abreEditar
// Descripción	::	Abre Detalle de Pago de Cuotas
// Log			:: 	INK - RPR - 24/12/2012
//****************************************************************
function fn_abreEditar() {
    var strCodSolicitudCredito = $("#hddCodSolicitudCredito").val();
    var strCodigoLiquidacion = $("#hddCodigoLiquidacion").val();

    if (strCodSolicitudCredito == "" || strCodSolicitudCredito == null) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro.", "util/images/warning.gif", "ERROR EN SELECCION");
    } else {
        window.location = "frmLiquidacionesRegistro.aspx?hddCodSolicitudCredito=" + strCodSolicitudCredito + "&hddCodigoLiquidacion=" + strCodigoLiquidacion;
    }
}

//****************************************************************
// Funcion		:: 	fn_agregar
// Descripción	::	Nuevo Pago de Cuotas
// Log			:: 	IBK - RPR - 24/12/2012
//****************************************************************
function fn_agregar() {
    if ($("#hddTipoTransaccion").val() == "E") {
        fn_util_redirect('frmLiquidacionesRegistro.aspx?op=E');
    }
    else {
        fn_util_redirect('frmLiquidacionesRegistro.aspx');
    }
}

//****************************************************************
// Funcion		:: 	fn_limpiarForm 
// Descripción	::	Limpiar Datos
// Log			:: 	IBK - RPR - 24/12/2012
//****************************************************************
function fn_limpiarForm() {
    blnPrimeraBusqueda = false;

    $('#txtNroContrato').val("");
    $('#txtCodigoLiquidacion').val("");
    $('#txtCuCliente').val("");
    $('#txtRazonSocial').val("");
    $('#txtFechaValorInicio').val("");
    $('#txtFechaValorFin').val("");
    
    $('#cmbEstado').val("0");
    $('#cmbMoneda').val("0");
    $('#cmbTipoLiquidacion').val("0");
    
    $("#jqGrid_lista_A").GridUnload();
    fn_cargaGrilla();

}