//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	KCC - 12/06/2012
//****************************************************************
$(document).ready(function() {
    //Carga Grilla
    fn_cargaGrilla();
    
    //On load Page (siempre al final)
    fn_onLoadPage();
});

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	KCC - 12/06/2012
//****************************************************************
function fn_cargaGrilla() {
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            fn_ListarValorGenerico();
        },
        jsonReader: {
            root:    "Items",
            page:    "CurrentPage", // Número de página actual.
            total:   "PageCount",   // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CODIGO"       // Índice de la columna con la clave primaria.
        },
        colNames: ['Código Interno', 'Código Sunat - Descripción', 'Valor2','Valor3','Valor4'],
        colModel: [
            { name: 'CODIGO', index: 'CODIGO', width: 50, sorttype: "string", align: "right" },
            { name: 'DESCRIPCION', index: 'DESCRIPCION', width: 250, sorttype: "string", align: "left" },
	        { name: 'VALOR2', index: 'VALOR2', width: 0, hidden: true, sorttype: "string", align: "left" },
	        { name: 'VALOR3', index: 'VALOR3', width: 0, hidden: true, sorttype: "string", align: "left" },
	        { name: 'VALOR4', index: 'VALOR4', width: 0, hidden: true, sorttype: "string", align: "left" }
	        ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum:200, // Tamaño de la página
        //rowList: [10, 20, 30],
        sortname: 'CODIGO',
        sortorder: 'asc',
        //viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {            
        },
        ondblClickRow: function(id) {
            fn_selectFila(id);        
        }
    }).navGrid('#jqGrid_pager_A', { edit: false, add: false, del: false  });
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 50);
    $("#search_jqGrid_lista_A").hide();
	$("#jqGrid_pager_A_center").hide();
}


function fn_ListarValorGenerico() {
    var arrParametros = ["pPageSize",          fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página.
                          "pCurrentPage",      fn_util_getJQGridParam("jqGrid_lista_A", "page"),    // Página actual.
                          "pSortColumn",       fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
                          "pSortOrder",        fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"),
                          "pstrCodigoDominio", $("#hidDominio").val()
                        ];
    
    fn_util_AjaxWM("frmListaValorGenerico.aspx/ListarParametros",
                    arrParametros,
                    function(resultado) {
                        jqGrid_lista_A.addJSONData(resultado);
                        fn_doResize();                        
                    },
                    function(resultado) {
                        parent.fn_unBlockUI();
                        var error = eval("(" + resultado.responseText + ")");
                        parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL LISTAR");
                    }
        );
 }
 
 function fn_selectFila(id) {

    var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
    var pag = window.parent.frames[0];
     
    pag.fn_obtenerValorGenerico($("#hidProvino").val(), rowData.CODIGO, rowData.DESCRIPCION, rowData.VALOR2, rowData.VALOR3, rowData.VALOR4);
    parent.fn_util_CierraModal();
}
 