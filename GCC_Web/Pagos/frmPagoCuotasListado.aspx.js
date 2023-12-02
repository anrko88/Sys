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
    $('#txtCuCliente').validText({ type: 'number', length: 10 });
    $('#txtRazonSocial').validText({ type: 'comment', length: 100 });
    $('#txtNroAutorizacion').validText({ type: 'number', length: 8 });
    //$('#cmbEstado').html(strComboVacio);        

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
            var txtCuCliente = $('#txtCuCliente').val() == undefined ? "" : $('#txtCuCliente').val();
            var txtRazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
            var txtNroInstruccion = $('#txtNroAutorizacion').val() == undefined ? "" : $('#txtNroAutorizacion').val();
            var txtFechaPagoIni = $('#txtFechaPagoIni').val() == undefined ? "" : $('#txtFechaPagoIni').val();
            var txtFechaPagoFin = $('#txtFechaPagoFin').val() == undefined ? "" : $('#txtFechaPagoFin').val();
            var cmbTipoContrato = $('#cmbTipoContrato').val() == undefined ? "" : $('#cmbTipoContrato').val();
            var cmbEstado = $('#cmbEstado').val() == undefined ? "" : $('#cmbEstado').val();
            var cmbMoneda = $('#cmbMoneda').val() == undefined ? "" : $('#cmbMoneda').val();

            var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
                             "pstrNroContrato", txtNroContrato,
                             "pstrCuCliente", txtCuCliente,
                             "pstrRazonSocial", txtRazonSocial,
                             "pstrNroAutorizacion", txtNroInstruccion,
                             "pstrFechaPagoIni", txtFechaPagoIni,
                             "pstrFechaPagoFin", txtFechaPagoFin,
                             "pstrTipoContrato", cmbTipoContrato,
                             "pstrEstado", cmbEstado,
                             "pstrMoneda", cmbMoneda
                            ];

            fn_util_AjaxWM("frmPagoCuotasListado.aspx/ListaPagoCuotas",
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
        colNames: ['Nº Contrato', 'Sec', 'CU Cliente', 'Razon Social o Nombre', 'Fecha Pago', 'Fecha Valor', 'Fecha Vcto', 'Nº Autorizacion', 'Nº Cuota', 'Total Pagado', 'Estado', 'Via Cobranza'],
        colModel: [
                { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', sortable: true, sorttype: "int", width: 40, align: "center", defaultValue: "" },
                { name: 'NumSecRecuperacion', index: 'NumSecRecuperacion', sortable: true, sorttype: "int", width: 15, align: "center", defaultValue: "" },
                { name: 'CodUnico', index: 'CodUnico', sortable: true, sorttype: "string", width: 40, align: "center", defaultValue: "" },
                { name: 'NombreSubprestatario', index: 'NombreSubprestatario', sortable: true, sorttype: "string", width: 80, align: "left", defaultValue: "" },
                { name: 'FechaRecuperacion', index: 'FechaRecuperacion', sortable: true, width: 25, sorttype: "string", align: "center", defaultValue: "", formatter: fn_util_ValidaStringFecha },
                { name: 'FechaValorRecuperacion', index: 'FechaValorRecuperacion', sortable: true, width: 25, sorttype: "string", align: "center", defaultValue: "", formatter: fn_util_ValidaStringFecha },
                { name: 'FechaVencimientoCuota', index: 'FechaVencimientoCuota', sortable: true, width: 25, sorttype: "string", align: "center", defaultValue: "", formatter: fn_util_ValidaStringFecha },
                { name: 'CodAutorizacionRecuperacion', index: 'CodAutorizacionRecuperacion', sortable: true, width: 40, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'NumCuotaCalendario', index: 'NumCuotaCalendario', sortable: true, width: 20, sorttype: "string", align: "center", defaultValue: "" },

                { name: 'MontoRecuperacionNeto', index: 'MontoRecuperacionNeto', sortable: true, sorttype: "float", width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },

                { name: 'NombreEstadoRecuperacion', index: 'NombreEstadoRecuperacion', sortable: true, width: 40, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'ViaCobranza', index: 'ViaCobranza', sortable: true, width: 40, sorttype: "string", align: "center", defaultValue: "" },
        ],
        width: glb_intWidthPantalla - 70,
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito',
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
            $("#hddCodSolicitudCredito").val(rowData.CodSolicitudCredito);
            $("#hddNumSecRecuperacion").val(rowData.NumSecRecuperacion);
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodSolicitudCredito").val(rowData.CodSolicitudCredito);
            $("#hddNumSecRecuperacion").val(rowData.NumSecRecuperacion);
            fn_abreEditar();
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
    var strNumSecRecuperacion = $("#hddNumSecRecuperacion").val();

    if (strCodSolicitudCredito == "" || strCodSolicitudCredito == null) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro.", "util/images/warning.gif", "ERROR EN SELECCION");
    } else if ($("#hddTipoTransaccion").val() == "E") {
        window.location = "frmPagoCuotasRegistro.aspx?hddCodSolicitudCredito=" + strCodSolicitudCredito + "&hddNumSecRecuperacion=" + strNumSecRecuperacion + "&op=E";
    }
    else {
        window.location = "frmPagoCuotasRegistro.aspx?hddCodSolicitudCredito=" + strCodSolicitudCredito + "&hddNumSecRecuperacion=" + strNumSecRecuperacion;
    }
}

//****************************************************************
// Funcion		:: 	fn_agregar
// Descripción	::	Nuevo Pago de Cuotas
// Log			:: 	IBK - RPR - 24/12/2012
//****************************************************************
function fn_agregar() {
    if ($("#hddTipoTransaccion").val() == "E") {
        fn_util_redirect('frmPagoCuotasRegistro.aspx?op=E');
    }
    else {
        fn_util_redirect('frmPagoCuotasRegistro.aspx');
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
    $('#txtCuCliente').val("");
    $('#txtRazonSocial').val("");
    $('#txtFechaPagoIni').val("");
    $('#txtFechaPagoFin').val("");

    $('#txtNroAutorizacion').val("");
    $('#cmbEstado').val("0");
    $('#cmbMoneda').val("0");
    $('#cmbTipoContrato').val("0");
    $("#jqGrid_lista_A").GridUnload();
    fn_cargaGrilla();

}