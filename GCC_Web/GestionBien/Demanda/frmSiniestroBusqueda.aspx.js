//****************************************************************
// Variables Globales
//****************************************************************
var blnPrimeraBusqueda = false;
var intPaginaActual = 1;


//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 07/11/2012
//****************************************************************
$(document).ready(function() {
        	
    //Carga Grilla
    fn_cargaGrilla();
        
    //On load Page (siempre al final)
    fn_onLoadPage();
    
});




//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla Listado de Cotizaciones
// Log			:: 	JRC - 07/11/2012
//****************************************************************
function fn_cargaGrilla() {

     $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");	        	
            fn_buscaSiniestro();
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
        colNames: ['','','Nº Siniestro','Fecha Siniestro','Tipo','Contrato','Seguro','Estado del Bien','Nº Poliza','Aplicación del Fondo'],
        colModel: [
				{ name: 'SecFinanciamiento',index: 'SecFinanciamiento',	hidden: true },
				{ name: 'SecSiniestro',		index: 'SecSiniestro', hidden: true},
                { name: 'NroSiniestro',		index: 'NroSiniestro',	sortable: true, sorttype: "string",	align: "center", defaultValue: "" },
                { name: 'FecSiniestro',		index: 'FecSiniestro',	sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'DesTipo',			index: 'DesTipo',		sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'DesContrato',		index: 'DesContrato',	sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'DesSeguro',		index: 'DesSeguro',		sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'DesEstadoBien',	index: 'DesEstadoBien',	sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'NroPoliza',		index: 'NroPoliza',		sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'DesAplicacionFondo', index: 'DesAplicacionFondo', sortable: true, sorttype: "string", align: "center", defaultValue: "",	hidden: true }
        ],
        width: glb_intWidthPantalla-70,
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'SecSiniestro',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodSiniestro").val(rowData.SecSiniestro);
        },
        ondblClickRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodSiniestro").val(rowData.SecSiniestro);
            fn_seleccionar();
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    
}



//****************************************************************
// Funcion		:: 	fn_buscaSiniestro
// Descripción	::	Busca listado por parametros
// Log			:: 	JRC - 07/11/2012
//****************************************************************
function fn_buscaSiniestro() {

    try {
        parent.fn_blockUI();

        var hddCodContrato = $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();
        var hddCodBien = $('#hddCodBien').val() == undefined ? "" : $('#hddCodBien').val();
        var txtNroSiniestro = $('#txtNroSiniestro').val() == undefined ? "" : $('#txtNroSiniestro').val();
       
        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación                             
                             "pstrCodContrato", hddCodContrato,
                             "pstrCodBien", hddCodBien,
                             "pstrNroSiniestro", txtNroSiniestro
                            ];
		
        fn_util_AjaxWM("frmSiniestroBusqueda.aspx/ListaSiniestro",
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
// Funcion		:: 	fn_seleccionar
// Descripción	::	Seleccionar Siniestro
// Log			:: 	JRC - 07/11/2012
//****************************************************************
function fn_seleccionar() {
	var codSiniestro = $("#hddCodSiniestro").val();
	if(fn_util_trim(codSiniestro) == ""){
		parent.fn_mdl_mensajeError("Debe seleccionar un Sinestro", function() { }, "VALIDACIÓN");
	}else{
			
		var ctrlIframe = parent.document.getElementById('ifrModal');
		var ctrlHddCodSiniestro = ctrlIframe.contentWindow.document.getElementById("hddCodSiniestroBsq");
		var ctrlBtn = ctrlIframe.contentWindow.document.getElementById("btnCargaSiniestro");
		
		ctrlHddCodSiniestro.value = codSiniestro;
		ctrlBtn.click();
		parent.fn_util_CierraModal2();
				
	}	
}