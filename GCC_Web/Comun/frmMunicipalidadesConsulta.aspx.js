var blnPrimeraBusqueda;
var intPaginaActual = 1;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
//****************************************************************
$(document).ready(function() {
    //Carga Grilla
    fn_cargaGrilla();
    $("#jqGrid_listado").setGridWidth($(window).width() - 50);

    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscarMunicipalidad(true);
        }
    });

    $('#txtNroCodigo').validText({ type: 'number', length: 3 });
    $('#txtMunicipalidad').validText({ type: 'comment', length: 80 });


    //On load Page (siempre al final)
    fn_onLoadPage();

});

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	
// Log			:: 	WCR - 14/05/2012
//****************************************************************
function fn_buscarMunicipalidad(pblnBusqueda) {
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

            var nroCodigo = $('#txtNroCodigo').val() == undefined ? "" : $('#txtNroCodigo').val();
            var Municipaliad = $('#txtMunicipalidad').val() == undefined ? "" : $('#txtMunicipalidad').val();

            var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página.
		    "pCurrentPage", intPaginaActual, // Página actual.
		    "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
		    "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
		    "pCodMunicipalidad", nroCodigo,
		    "pMunicipalidad", Municipaliad
	    ];

            fn_util_AjaxWM("frmMunicipalidadesConsulta.aspx/ListadoMunicipalidadPaginado",
		    arrParametros,
		    function(jsondata) {
		        jqGrid_lista_A.addJSONData(jsondata);
		        parent.fn_unBlockUI();
		        fn_doResize();
		    },
		    function(request) {
		        parent.fn_unBlockUI();
		        parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR EN LA BÚSQUEDA");
		    }
	    );
        } catch (ex) {
            parent.fn_unBlockUI();
            parent.fn_mdl_mensajeIco(ex.message, "util/images/warning.gif", "ERROR EN LA BÚSQUEDA");
        }
    }
}

//****************************************************************
// Función		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	WCR - 14/05/2012
//****************************************************************
function fn_cargaGrilla() {
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_buscar();
        },
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodSolicitudCredito" // Índice de la columna con la clave primaria.
        },
        colNames: ['Codigo', 'Descripción'],
        colModel: [
            { name: 'CLAVE1', index: 'CLAVE1', width: 10, sorttype: "string", align: "center" },
	        { name: 'VALOR1', index: 'VALOR1', width: 100, sorttype: "string", align: "left" },
	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10, // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CLAVE1',
        sortorder: 'asc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
        },
        ondblClickRow: function(id) {
            fn_selectMunicipalidad(id);
        }
    }).navGrid('#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

    $("#jqGrid_lista_A").setGridWidth($(window).width() - 70);
}

//****************************************************************
// Funcion		:: 	fn_selectCredito
// Descripción	::	Devuelve el registro seleccionado en la lista
// Log			:: 	WCR - 15/05/2012
//****************************************************************
function fn_selectMunicipalidad(id) {
    var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id); 
    var pag = window.parent.frames[0];
    pag.fn_obtenerMuncipalidad(rowData.CLAVE1, rowData.VALOR1);
    parent.fn_util_CierraModal();
}

//****************************************************************
// Funcion		:: 	fn_limpiar
// Descripción	::	Devuelve el registro seleccionado en la lista
// Log			:: 	WCR - 15/05/2012
//****************************************************************
function fn_limpiar() {

    blnPrimeraBusqueda = false;

    $('#txtNroCodigo').val('');
    $('#txtMunicipalidad').val('');
    $("#jqGrid_lista_A").GridUnload();
    fn_cargaGrilla();
}