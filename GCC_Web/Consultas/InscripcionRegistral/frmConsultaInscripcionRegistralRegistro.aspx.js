$(document).ready(function() {

 

	fn_CargarGrillaDetalleInscripcion();
	
	//On load Page (siempre al final)
    fn_onLoadPage();
	
});

function fn_CargarGrillaDetalleInscripcion() {
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el detalle de inscripcion municipal
// Log			:: 	AEP - 24/09/2012
//****************************************************************

    $("#jqGrid_lista_A").jqGrid({
 
    datatype: function() {
        fn_ListagrillaInscripcion();
    },
    jsonReader: //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            {
            root: "Items",
            page: "CurrentPage",            // Número de página actual.
            total: "PageCount",             // Número total de páginas.
            records: "RecordCount",         // Total de registros a mostrar.
            repeatitems: false,
            id: "codInscripcionMunicipalDetalle"   // Índice de la columna con la clave primaria.
        },
    colNames: ['Codigo', 'Partida Registral', 'Asiento Registral', 'Acto Registral','',''],
    colModel: [
                { name: 'codInscripcionMunicipalDetalle', index: 'codInscripcionMunicipalDetalle', hidden: true },
		        { name: 'PartidaRegistral', index: 'PartidaRegistral', width: 100, align: "Center", sorttype: "string"},
		        { name: 'AsientoRegistral', index: 'AsientoRegistral', width: 100, align: "Center", sortable: false },
		        { name: 'Acto', index: 'Acto', width: 100, align: "Center", sorttype: "string" },
    	        { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
    	        { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true }
	    ],
    //width: 100%,
    height: '100%',
    pager: '#jqGrid_pager_A',
    loadtext: 'Cargando datos...',
    emptyrecords: 'No hay resultados',
    rowNum: 10,                             // Tamaño de la página
    rowList: [10, 20, 30],
    sortname: 'codInscripcionMunicipalDetalle', // Columna a ordenar por defecto.
    sortorder: 'asc',                     // Criterio de ordenación por defecto.
    viewrecords: true,                      // Muestra la cantidad de registros.
    gridview: true,
    autowidth: false,
    altRows: true,
    loadonce: false,
    altclass: 'gridAltClass',
    onSelectRow: function(id) {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
        $("#hidRowInscripcionMunicipal").val(id);   
    }
});

jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });

// Le asigna el ancho a usar para la grilla.
$("#jqGrid_lista_A").setGridWidth($(window).width() - 120);
$("#search_jqGrid_lista_A").hide();

}

//****************************************************************
// Funcion		:: 	fn_ListagrillaInscripcion
// Descripción	::	
// Log			:: 	AEP - 19/10/2012
//****************************************************************
function fn_ListagrillaInscripcion() {
	var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
		"pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"), // Página actual
		"pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
		"pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
		"pCodigoContrato", $("#hidCodigoContrato").val(),
		"pCodigoBien", $("#hidCodigoBien").val()                         
	];

	fn_util_AjaxSyncWM("frmConsultaInscripcionRegistralRegistro.aspx/ListaDocumentosInscripcion",
		arrParametros,
		function(jsondata) {
			jqGrid_lista_A.addJSONData(jsondata);
			//parent.fn_unBlockUI();
			//$("#hidRowInscripcionMunicipal").val('');
		},
		function(request) {
			fn_util_alert(jQuery.parseJSON(request.responseText).Message);
			parent.fn_unBlockUI();
		}
	);

	//Hiden para limpiar la variable de Aprobacion de 
	$("#hddFlagVerificaAdjunto").val("");
}

//****************************************************************
// Funcion		:: 	fn_Volver
// Descripción	::	fn_Volver
// Log			:: 	AEP - 29/01/2013
//****************************************************************
function fn_Volver() {
	fn_mdl_confirma('¿Está seguro de volver?',
		function() {
			parent.fn_blockUI();
			fn_util_redirect('frmConsultaInscripcionRegistral.aspx');
		},
		"../../util/images/question.gif",
		function() {
		},
		'Consulta - Inscripción Registral'
	);
}
