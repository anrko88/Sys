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
            fn_ListarDocumento();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodProveedor,TipoDocumento,NroDocumento,FechaEmision" // Índice de la columna con la clave primaria.
        },
        colNames: ['','', '', 'Nro. Documento', 'Fec. Emisión', 'Tipo Doc.', 'Nº Documento', 'Proveedor', 'Tipo Comprobante', 'Moneda', 'Total', 'Por Desembolsar',''],
        colModel: [
                { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
                { name: 'TipoDocumento', index: 'TipoDocumento', hidden: true },
                { name: 'NumeroSerieDocumento', index: 'NumeroSerieDocumento', hidden: true },
                { name: 'NroDocumento', index: 'NroDocumento', width: 100, align: "center" },
                { name: 'FechaEmision', index: 'FechaEmision', width: 60, align: "center", formatter: Fn_util_cortaDateServer },
                { name: 'TipoDocumentoProveedor', index: 'TipoDocumentoProveedor', hidden: true },
                { name: 'NumeroDocumentoProveedor', index: 'NumeroDocumentoProveedor', hidden: true },
                { name: 'NombreProveedor', index: 'NombreProveedor', width: 150, align: "left" },
                { name: 'NombreTipoDocumento', index: 'NombreTipoDocumento', width: 120, sorttype: "string", align: "left" },
                { name: 'NombreMoneda', index: 'NombreMoneda', width: 80, align: "center" },
                { name: 'Total', index: 'Total', width: 80, align: "right", formatter: Fn_util_ReturnValidDecimal2 },                
                { name: 'MontoPendienteProveedor', index: 'MontoPendienteProveedor', width: 80, align: "right", sortable: "double", formatter: Fn_util_ReturnValidDecimal2 },
                { name: '', index: '', width: 2 }
	    ],
        height: '100%',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        sortname: 'MontoPendienteProveedor',
        sortorder: 'asc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        altclass: 'gridAltClass',
        ondblClickRow: function(id) {
            fn_selectFila(id);
        }
    }).navGrid('#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 40);
}

function fn_ListarDocumento() {
   
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página.
                          "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),    // Página actual.
                          "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
                          "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"),
                         
                          "pstrCodigoContrato", $("#hidCodigoContrato").val(),
                          "pstrTipoDocumento",  $("#hidTipoDocumento").val(),
                          "pstrNroDocumento",   $("#hidNroDocumento").val(),
                          "pstrFechaEmision",   $("#hidFechaEmision").val(),
                          "pstrCodProveedor",   $("#hidCodProveedor").val(),
                          "pstrCodunico",       $("#hidCodunico").val()
                        ];
    
    fn_util_AjaxWM("frmListaDocumentoDesembolso.aspx/ListarDocumento",
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
    pag.fn_obtenerDatosDocumento(rowData.TipoDocumento, rowData.NumeroSerieDocumento, rowData.NroDocumento, rowData.FechaEmision, rowData.CodSolicitudCredito);
    parent.fn_util_CierraModal();
}