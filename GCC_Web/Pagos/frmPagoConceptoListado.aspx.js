//****************************************************************
// Variables Globales
//****************************************************************
var blnPrimeraBusqueda;
var intPaginaActual = 1;
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 19/09/2012
//****************************************************************
$(document).ready(function() {

    //Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFechaPagoIni]'));
    fn_util_SeteaCalendario($('input[id*=txtFechaPagoFin]'));

    //Carga Grilla
    fn_cargaGrilla();

    //Valida Campos
    fn_inicializaCampos();

    //Busca con Enter    
    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscar(true);
        }
    });

    //On load Page (siempre al final)
    fn_onLoadPage();

});

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JJC - 10/02/2012
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
// Funcion		:: 	fn_buscarCotizacion
// Descripción	::	Busca listado
// Log			:: 	JJM - 19/09/2012
//****************************************************************
function fn_buscar(pblnBusqueda) {
    //debugger;
    blnPrimeraBusqueda = pblnBusqueda;
    intPaginaActual = 1;
    fn_realizaBusqueda();
}

function fn_realizaBusqueda() {

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
                             "pstrEstado", cmbEstado == '0' ? "" : cmbEstado,
                             "pstrMoneda", cmbMoneda == '0' ? "" : cmbMoneda
                            ];

            fn_util_AjaxWM("frmPagoConceptoListado.aspx/ListaPagoConcepto",
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
// Descripción	::	Carga Grilla Listado
// Log			:: 	JJM - 19/09/2012
//****************************************************************
function fn_cargaGrilla() {

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_realizaBusqueda();

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
        colNames: ['N° Contrato', 'Sec', 'Razón Social', 'Tipo', 'Fecha Pago', 'Fecha Valor', 'Comision', 'IGVComision', 'Reembolso', 'IGV Reembolso', 'Total', 'Estado', 'N° Autorización'],
        colModel: [
                { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', sortable: true, sorttype: "string", width: 10, align: "left", defaultValue: "" },
                { name: 'NumSecRecuperacion', index: 'NumSecRecuperacion', sortable: true, sorttype: "int", width: 5, align: "center", defaultValue: "" },
                { name: 'NombreSubprestatario', index: 'NombreSubprestatario', sortable: true, sorttype: "string", width: 30, align: "Left", defaultValue: "" },
                { name: 'TipDes', index: 'TipDes', sortable: true, sorttype: "string", width: 10, align: "left", defaultValue: "" },
                { name: 'FechaRecuperacion', index: 'FechaRecuperacion', sortable: true, width: 10, sorttype: "string", align: "center", defaultValue: "", formatter: fn_util_ValidaStringFecha },
                { name: 'FechaValorRecuperacion', index: 'FechaValorRecuperacion', sortable: true, width: 10, sorttype: "string", align: "center", defaultValue: "", formatter: fn_util_ValidaStringFecha },
                { name: 'MontoComision', index: 'MontoComision', sortable: true, width: 10, sorttype: "float", align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'MontoIGV', index: 'MontoIGV', sortable: true, width: 10, sorttype: "float", align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'MontoReembolso', index: 'MontoReembolso', sortable: true, width: 10, sorttype: "float", align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'MontoIGVReembolso', index: 'MontoIGVReembolso', sortable: true, width: 10, sorttype: "float", align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'MontoRecuperacion', index: 'MontoRecuperacion', sortable: true, width: 10, sorttype: "float", align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'Estado', index: 'Estado', sortable: true, width: 15, sorttype: "string", align: "left", defaultValue: "" },
                { name: 'CodAutorizacionRecuperacion', index: 'CodAutorizacionRecuperacion', sortable: true, width: 15, sorttype: "string", align: "center", defaultValue: "" }
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
            window.location = "frmPagoConceptoRegistro.aspx?hddCodSolicitudCredito=" + rowData.CodSolicitudCredito + "&hddNumSecRecuperacion=" + rowData.NumSecRecuperacion;
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

}
//****************************************************************
// Funcion		:: 	fn_abreEditar
// Descripción	::	Abre Detalle de Pago
// Log			:: 	JJM - 24/12/2012
//****************************************************************
function fn_abreEditar() {

    var strCodSolicitudCredito = $("#hddCodSolicitudCredito").val();
    var strSecRecuperacion = $("#hddNumSecRecuperacion").val();

    if (strCodSolicitudCredito == "" || strCodSolicitudCredito == null || strSecRecuperacion == '0') {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro.", "util/images/warning.gif", "ERROR EN SELECCION");
    } else {
        window.location = "frmPagoConceptoRegistro.aspx?hddCodSolicitudCredito=" + strCodSolicitudCredito + "&hddNumSecRecuperacion=" + strSecRecuperacion;
    }
}

function fn_AgregarConcepto() {
    fn_util_redirect('frmPagoConceptoRegistro.aspx');
}

//****************************************************************
// Funcion		:: 	fn_abreCartas
// Descripción	::	Abre una ventana para asignar cheque
// Log			:: 	AEP - 07/03/2013
//****************************************************************
function fn_abreCartas() {
    //parent.fn_util_redirect("frmImpuestoVehicularMasivoResumenCarga.aspx");
    parent.fn_util_AbreModal("Generación de Cartas", "Pagos/frmGenerarCarta.aspx",650, 240, function() { });
}

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