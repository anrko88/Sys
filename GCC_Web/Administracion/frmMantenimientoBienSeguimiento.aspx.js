//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    fn_cargaGrilla();
    
    //On load Page (siempre al final)
    fn_onLoadPage();

});

function fn_ListarSeguimientoCotizacion() {
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"), // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder") // Criterio de ordenación
                         ];

    fn_util_AjaxWM("frmCotizacionSeguimiento.aspx/ListarSeguimientoCotizacion",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);
                    parent.fn_unBlockUI();
                    fn_doResize();
                },
                function(request) {
                    parent.fn_unBlockUI();
                    var error = eval("(" + request.responseText + ")");
                    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR LISTAR");
                }
        );
}

/****************************************************************
 Funcion		:: 	fn_cargaGrilla
 Descripción	::	Carga Grilla Listado Seguimiento
 Log			:: 	KCC - 25/05/2012
****************************************************************/
function fn_cargaGrilla() {

        var mydata = 
          [
		    { CODIGOCONTRATO: "001", USUARIOREGISTRO: " B11197", FECHAREGISTRO: "20/09/2012", ESTADOBIEN: "Activo", COMENTARIO: "prueba" },
		    { CODIGOCONTRATO: "001", USUARIOREGISTRO: " B11197", FECHAREGISTRO: "20/09/2012", ESTADOBIEN: "Inactivo", COMENTARIO: "prueba"},
          	{ CODIGOCONTRATO: "001", USUARIOREGISTRO: " B11197", FECHAREGISTRO: "20/09/2012", ESTADOBIEN: "Activo", COMENTARIO: "prueba"},
          	{ CODIGOCONTRATO: "001", USUARIOREGISTRO: " B11197", FECHAREGISTRO: "20/09/2012", ESTADOBIEN: "Inactivo", COMENTARIO: "prueba" }
		  ];
    $("#jqGrid_lista_A").jqGrid({
//        datatype: function() {
//            fn_ListarSeguimientoCotizacion()
//        },
    	datatype:"local",
//        jsonReader:
//        {
//            root: "Items",
//            page: "CurrentPage",
//            total: "PageCount",
//            records: "RecordCount",
//            repeatitems: false,
//            id: "Id"
//        },
        colNames: ['ID', 'Usuario', 'Fecha Registro', 'Estado', 'Comentario/ Motivo Rechazo'],
        colModel: [
            { name: 'CODIGOCONTRATO', index: 'CODIGOCONTRATO', hidden: true, width: 0, align: "left", sorttype: "string" },
            { name: 'USUARIOREGISTRO', index: 'USUARIOREGISTRO', sorttype: "string", width: 30, align: "center", defaultValue: "" },
            { name: 'FECHAREGISTRO', index: 'FECHAREGISTRO', sorttype: "string", width: 50, align: "center", formatter: fn_util_ValidaStringFechaHora },
            { name: 'ESTADOBIEN', index: 'ESTADOBIEN', sorttype: "string", width: 50, align: "center", defaultValue: "" },
            { name: 'COMENTARIO', index: 'COMENTARIO', sorttype: "string", width: 80, align: "left", defaultValue: "" }
        ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'CODIGOCONTRATO',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass'
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });

    for (var i = 0; i <= mydata.length; i++) {
	     jQuery("#jqGrid_lista_A").jqGrid('addRowData', i + 1, mydata[i]);
	 }

}
