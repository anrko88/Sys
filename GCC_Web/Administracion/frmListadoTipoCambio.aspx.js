var blnPrimeraBusqueda;
var intPaginaActual = 1;


//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
//****************************************************************
$(document).ready(function() {

    //Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));


    //Carga Grilla
    fn_cargaGrilla();
    fn_buscarTipoCambio(true);
    //Valida Campos
    //fn_inicializaCampos();

    //Busca con Enter    
    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscar();
        }
    });
    fn_buscar();
    //On load Page (siempre al final)
    fn_onLoadPage();

});

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
        colNames: ['Modalidad','Nombre Moneda', 'Fecha Inicio Vigencia',
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
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hidCodTipoCambio").val(rowData.TipoModalidadCambio);
            $("#hidCodMoneda").val(rowData.NombreMoneda);
            $("#hidTipoModalidadCambio").val(rowData.NombreTipoModalidadCambio);
            $("#hidFechaInicio").val(rowData.FechaInicioVigencia);
            $("#hidFechaFin").val(rowData.FechaFinalVigencia);
            $("#hidValorCompra").val(rowData.MontoValorCompra);
            $("#hidValorVenta").val(rowData.MontoValorVenta);
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            window.location = "frmRegistroTipoCambio.aspx?co=1&CodTipoCambio=" + rowData.TipoModalidadCambio +
                                "&CodMoneda=" + rowData.NombreMoneda +                                
                                "&TipoModalidadCambio=" + rowData.NombreTipoModalidadCambio +
                                "&FechaInicio=" + rowData.FechaInicioVigencia +
                                "&FechaFin=" + rowData.FechaFinalVigencia +
                                "&ValorCompra=" + rowData.MontoValorCompra +
                                "&ValorVenta=" + rowData.MontoValorVenta;
        }
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

            fn_util_AjaxWM("frmListadoTipoCambio.aspx/BuscarTipoCambio",
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
function fn_abreEditar() {
    var strCodTipoCambio = $("#hidCodTipoCambio").val();
    var strCodMoneda = $("#hidCodMoneda").val();
    var strTipoModalidad = $("#hidTipoModalidadCambio").val();
    var strFechaInicio = $("#hidFechaInicio").val();
    var strFechaFin = $("#hidFechaFin").val();
    var strValorCompra = $("#hidValorCompra").val();
    var strValorVenta = $("#hidValorVenta").val();

    if (strCodTipoCambio == "" || strCodMoneda == '' || strTipoModalidad == '' || strFechaInicio == '' || strFechaFin == '' || strValorCompra == '' || strValorVenta == '') {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro.", "util/images/warning.gif", "ERROR EN SELECCION");
    } else {
    window.location = "frmRegistroTipoCambio.aspx?co=1&CodTipoCambio=" + strCodTipoCambio +
                                "&CodMoneda=" + strCodMoneda +
                                "&TipoModalidadCambio=" + strTipoModalidad +
                                "&FechaInicio=" + strFechaInicio +
                                "&FechaFin=" + strFechaFin +
                                "&ValorCompra=" + strValorCompra +
                                "&ValorVenta=" + strValorVenta;
        
    }
}

function fn_Agregar() {

    fn_util_redirect('frmRegistroTipoCambio.aspx?co=2');
}
function fn_LimpiarCampos() {
    $("#txtFechaIni").val('');
    $("#txtFechaFin").val('');
    $("#cmbTipoModalidad option").eq(0).attr("selected", "selected");   
}