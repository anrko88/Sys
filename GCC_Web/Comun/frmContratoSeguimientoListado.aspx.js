//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    fn_cargaGrilla();

    fn_InicializarControles();

    //On load Page (siempre al final)
    fn_onLoadPage();
    $("#hddFlagVerificaAdjunto").val("");

});

//****************************************************************
// Función		:: 	fn_InicializarControles
// Descripción	::	Lee el dato en los controles ocultos y los asigna a los controles de usuario correspondientes.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_InicializarControles() {
    $("#dv_subTitulo").html($("#hddSubTitulo").val());
}

function fn_ListarSeguimientoContrato() {
    var arrParametros = [
                         "pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"), // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
                         "pCodSolicitudCredito", $("#hddCodigoContrato").val() // Criterio de ordenación   
                         ];

    fn_util_AjaxWM("frmContratoSeguimientoListado.aspx/ListarSeguimientoContrato",
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
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            fn_ListarSeguimientoContrato();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "codigoseguimiento"
        },
        colNames: ['ID', 'Usuario', 'Fecha Cambio', 'Estado Contrato', 'Observación', 'Motivo Rechazo', 'Adjunto'],
        colModel: [
            { name: 'codigoseguimiento', index: 'codigoseguimiento', hidden: true },
            { name: 'UsuarioRegistro',   index: 'UsuarioRegistro', sorttype: "string", width: 80, align: "center" },
            { name: 'FechaCambioEstado', index: 'FechaCambioEstado', sorttype: "string", width: 80, align: "center", formatter: Fn_util_ReturnValidDateTime },
            { name: 'EstadoContrato', index: 'EstadoContrato', sorttype: "string", width: 80, align: "center" },
            { name: 'Observacion', index: 'Observacion', sorttype: "string", width: 150, align: "left" },
            { name: 'MotivoRechazo', index: 'MotivoRechazo', sorttype: "string", width: 80, align: "center" },
            { name: 'Adjunto', index: 'Adjunto', width: 100, align: "Center", sortable: "string", formatter: VerAdjunto }
        ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'codigoseguimiento',
        sortorder: 'desc',
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodigoSeguimiento").val(rowData.codigoseguimiento);

        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    
    $("#jqGrid_lista_A").setGridWidth($(window).width() -40);
}

//Inicio - IBK
    //JJM
    //Abrir Archivo
    //****************************************************************
    // Funcion		:: 	fn_abreArchivo 
    // Descripción	::	Abre Archivo
    // Log			:: 	JRC - 22/05/2012
    //****************************************************************

    function VerAdjunto(cellvalue, options, rowObject) {
        if (fn_util_trim(rowObject.Adjunto) != "") {
            var strNombreArchivo = rowObject.Adjunto.split('\\').pop();          
            strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);            
            return "<img src='../Util/images/ico_download.gif' alt='Descargar/Mostrar Archivo' title='" + strNombreArchivo + "'Descargar/Mostrar Archivo' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.Adjunto) + "');\" style='cursor:pointer;'/>";            

        } else {
            $("#hddFlagVerificaAdjunto").val("1");
            return ".";
        }

    };   
//}

function fn_abreArchivo(pstrRuta) {   
    window.open("../frmDownload.aspx?nombreArchivo=" + pstrRuta);
    return false;
};
//Fin - IBK