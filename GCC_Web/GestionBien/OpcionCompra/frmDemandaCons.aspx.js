//****************************************************************
// Variables Globales
//****************************************************************


//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	WCR - 04/01/2013
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();

    //Valida Tabs
    $("div#divTabs").tabs({
        show: function(event, ui) {
            fn_doResize();
        }
    });

    //Grilla Implicados
    fn_cargaGrilla()


    //On load Page (siempre al final)
    fn_onLoadPage();

});

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_inicializaCampos() {
//    $("#txtMontoIndem").validNumber({ value: $("#txtMontoIndem").val() });
//    $("#txtMontoDemanda").validNumber({ value: $("#txtMontoDemanda").val() });
}



//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla Listado de Cotizaciones
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_cargaGrilla() {

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_buscaImplicados();
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
        colNames: ['', '', '', 'Tipo', 'Nombre Completo', 'Tipo Documento', 'Nro. Documento'],
        colModel: [
				{ name: 'CodImplicado', index: 'CodImplicado', hidden: true },
				{ name: 'CodTipoDocumento', index: 'CodTipoDocumento', hidden: true },
				{ name: 'CodTipoImplicado', index: 'CodTipoImplicado', hidden: true },
                { name: 'DesTipoImplicado', index: 'DesTipoImplicado', sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'NombreImplicado', index: 'NombreImplicado', sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'NombreTipoDocumento', index: 'NombreTipoDocumento', sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'NroDocumento', index: 'NroDocumento', sortable: true, sorttype: "string", align: "center", defaultValue: "" }
        ],
        width: glb_intWidthPantalla - 90,
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'NombreImplicado',
        sortorder: 'desc',
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


//****************************************************************
// Funcion		:: 	fn_buscaImplicados
// Descripción	::	Busca listado por parametros
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_buscaImplicados() {
    try {
        parent.fn_blockUI();

        var hddCodContrato = $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();
        var hddCodBien = $('#hddCodBien').val() == undefined ? "" : $('#hddCodBien').val();
        var hddCodDemanda = $('#hddCodDemanda').val() == undefined ? "" : $('#hddCodDemanda').val();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación                             
                             "pstrCodContrato", hddCodContrato,
                             "pstrCodBien", hddCodBien,
                             "pstrCodDemanda", hddCodDemanda
                            ];

        //alert(arrParametros);
        fn_util_AjaxWM("frmDemandaCons.aspx/ListaImplicados",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);
                    parent.fn_unBlockUI();
                    fn_doResize();
                },
                function(request) {
                    parent.fn_unBlockUI();
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

    } catch (ex) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }

}

//****************************************************************
// Funcion		:: 	fn_cerrar
// Descripción	::	Cerrar Modal
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_cerrar() {
    parent.fn_util_CierraModal2();
}


