
$(document).ready(function() {

    //Carga Grilla
    fn_cargaGrilla();

    //On load Page (siempre al final)
    fn_onLoadPage();
});

function fn_cargaGrilla() {
    // debugger;
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_ListarDetalle();
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
        colNames: ['NroPrenda','Item', 'Descripción Movimiento', 'Nro. Carta', 'Fecha Carta'],
        colModel: [
                { name: 'NroPrendaRehder', index: 'NroPrendaRehder', sortable: true, sorttype: "string", width: 20, align: "left", defaultValue: "" },
                { name: 'seqNum', index: 'seqNum', sortable: true, sorttype: "string", width: 20, align: "left", defaultValue: "" },
                { name: 'DescripcionMovimiento', index: 'DescripcionMovimiento', sortable: true, sorttype: "string", width: 40, align: "left", defaultValue: "" },
                { name: 'NroCarta', index: 'NroCarta', sortable: true, sorttype: "string", width: 20, align: "left", defaultValue: "" },
                { name: 'FechaCarta', index: 'FechaCarta', sortable: true, sorttype: "string", width: 20, align: "left", defaultValue: "" }
        ],
        width: glb_intWidthPantalla - 70,
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'NroPrendaRehder, seqNum',
        sortorder: 'asc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass'
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
}

function fn_ListarDetalle() {

    var arrParametros = [
                         "pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"), // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
                         "pCodigoContrato", $("#hdnCodigoContrato").val()
                        ];

    fn_util_AjaxWM("frmSegurosDetalle.aspx/ListarDetalleSeguros",
                   arrParametros,
                   function(jsondata) {
                        jqGrid_lista_A.addJSONData(jsondata);
                   },
                   function(request) {
                       parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "REPRESENTANTES DEL CLIENTE");
                   }
                   );

}

