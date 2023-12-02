//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    //Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    fn_cargaGrilla();
    fn_ListarCotizacionVersiones();

    //On load Page (siempre al final)
    fn_onLoadPage();

});

function fn_ListarCotizacionVersiones() {
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"), // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder") // Criterio de ordenación
                         ];
                         
    fn_util_AjaxWM("frmCotizacionVersion.aspx/ListarCotizacionVersion",
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


//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla Listado de Cotizaciones
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() {

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() { },
        jsonReader:
        {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['ID', 'Ver.', 'CU Cliente', 'Raz. Social o Nombre', 'Clasificación del  Bien', 'Moneda', 'T.E.A.', 'Precio Venta', 'Nº Cuotas', 'Tipo Seguro', 'Estado', 'Fecha'],
        colModel: [
            { name: 'CODIGOCOTIZACION', index: 'CODIGOCOTIZACION', hidden: true, width: 0, align: "cnter", sorttype: "int" },
            { name: 'VERSIONCOTIZACION', index: 'VERSIONCOTIZACION', width: 10, align: "center", sorttype: "string" },
            { name: 'CODIGOUNICO', index: 'CODIGOUNICO', sorttype: "string", width: 30, align: "center", defaultValue: "" },
            { name: 'NOMBRECLIENTE', index: 'NOMBRECLIENTE', sorttype: "string", width: 80, align: "left", defaultValue: "" },
            { name: 'NOMBRECLASIFICACIONBIEN', index: 'NOMBRECLASIFICACIONBIEN', sorttype: "string", width: 80, align: "center", defaultValue: "" },
            { name: 'NOMBREMONEDA', index: 'NOMBREMONEDA', sorttype: "string", width: 25, align: "center", defaultValue: "" },
            { name: 'TEAPORC', index: 'TEAPORC', hidden: true, formatter: Fn_util_ReturnValidDecimal2 },
            { name: 'PRECIOVENTA', index: 'PRECIOVENTA', sorttype: "float", width: 30, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
            { name: 'NUMEROCUOTAS', index: 'NUMEROCUOTAS', width: 30, align: "center", defaultValue: "" },
            { name: 'NOMBREBIENTIPOSEGURO', index: 'NOMBREBIENTIPOSEGURO', hidden: true },
            { name: 'NOMBREESTADOCOTIZACION', index: 'NOMBREESTADOCOTIZACION', width: 35, align: "center", defaultValue: "" },
            { name: 'AUDFECHAREGISTRO', index: 'AUDFECHAREGISTRO', width: 30, align: "center", formatter: fn_util_ValidaStringFechaHora }
        ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'CODIGOCOTIZACION',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodigoCotizacion").val(rowData.CODIGOCOTIZACION);
            $("#hddVersionCotizacion").val(rowData.VERSIONCOTIZACION);            
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            window.location = "frmCotizacionVer.aspx?cc=" + rowData.CODIGOCOTIZACION + "&cv=" + rowData.VERSIONCOTIZACION;        
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
}


//****************************************************************
// Funcion		:: 	fn_abrirVer
// Descripción	::	Visualizar Version Cotizacion
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_abrirVer() {

    if (fn_util_trim($("#hddCodigoCotizacion").val()) == "" || fn_util_trim($("#hddVersionCotizacion").val()) == "") {
        parent.fn_mdl_mensajeError("Debe seleccionar una versión para visualizar el su detalle", function() { }, "VALIDACIÓN");
    } else {
        var strCodigoCotizacion = $("#hddCodigoCotizacion").val();
        var strVersionCotizacion = $("#hddVersionCotizacion").val();
        if (fn_util_trim(strCodigoCotizacion) != "" && fn_util_trim(strCodigoCotizacion) != "") {
            window.location = "frmCotizacionVer.aspx?cc=" + strCodigoCotizacion + "&cv=" + strVersionCotizacion; 
        } else {
            parent.fn_mdl_mensajeIco("Debe seleccionar un registro", "util/images/error.gif", "FALLO EN SELECCION");
        }
    }
    
}
