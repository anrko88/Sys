//VARIABLES GLOBALES

var strTipoDocumentoDNI = "1";
var strTipoDocumentoRUC = "2";
var strTipoDocumentoCarnetEx = "3";
var strTipoDocumentoPasaporte = "5";
var strTipoDocumentoOtroDoc = "6";
var blnPrimeraBusqueda;
var intPaginaActual = 1;


//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
//****************************************************************
$(document).ready(function() {
   
    $("#jqGrid_listado").setGridWidth($(window).width() - 50);
     $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscarProveedor(true);
        }
    });
    
	 fn_cargaGrilla();
    //On load Page (siempre al final)
    fn_onLoadPage();
	//fn_validarCampos($("#cmbTipoDocumento").val());
	$("#txtRazonSocial").validText({ type: 'comment', length: 100 });	
	$('#txtNroDocumento').attr('disabled','disabled');
	// Valida el ingreso de datos en tipo documento
	  $('#cmbTipoDocumento').change(function() {
        var strValor = $(this).val();
        $("#txtNroDocumento").val("");
        $('#txtNroDocumento').unbind('keypress');
        if (fn_util_trim(strValor) == strTipoDocumentoDNI) {
        	$('#txtNroDocumento').attr('disabled', false);
            $('#txtNroDocumento').validText({ type: 'number', length: 8 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoRUC) {
        	$('#txtNroDocumento').attr('disabled', false);
            $('#txtNroDocumento').validText({ type: 'number', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoCarnetEx) {
        	$('#txtNroDocumento').attr('disabled', false);
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoPasaporte) {
        	$('#txtNroDocumento').attr('disabled', false);
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if(fn_util_trim(strValor)==strTipoDocumentoOtroDoc){
        	$('#txtNroDocumento').attr('disabled', false);
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else {
        	$('#txtNroDocumento').attr('disabled','disabled');
        }
	  });
	  
});

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
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
        id: "CodProveedor" // Índice de la columna con la clave primaria.
    },
    colNames: ['Item', 'Tipo Documento', 'Nro Documento', 'Razón Social o Nombre', 'Tipo de Persona', 'Procedencia', ''],
    colModel: [
        { name: 'Id', index: 'Id', width: 30, sorttype: "string", align: "right",sortable: false },
        { name: 'TipoDocumento', index: 'Tipo Documento', width: 100, sorttype: "string", align: "center" },
		{ name: 'NumeroDocumento', index: 'Nro. Documento', width: 75, sorttype: "string", align: "left" },
		{ name: 'RazonSocial', index: 'RazonSocial', width: 180, align: "left", sorttype: "string" },
		{ name: 'TipoPersona', index: 'TipoPersona', width: 100, align: "left", sorttype: "string" },
		{ name: 'Procedencia', index: 'Procedencia', width: 100, align: "center", sorttype: "string" },
		{ name: 'CodProveedor', index: 'CodProveedor',  hidden: true }
	],
    height: '100%',
    pager: '#jqGrid_pager_A',
    loadtext: 'Cargando datos...',
    emptyrecords: 'No hay resultados',
    rowNum: 10, // Tamaño de la página
    rowList: [10, 20, 30],
    sortname: 'Id',
    sortorder: 'asc',
    viewrecords: true,
    gridview: true,
    autowidth: true,
    altRows: true,
    altclass: 'gridAltClass',
    onSelectRow: function(id) {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
        $("#hidCodProveedor").val(rowData.CodProveedor);
        
    },
   	ondblClickRow: function(id) {
	    parent.fn_blockUI();
		var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
	    fn_util_redirect('frmProveedorMant.aspx?co=1&cp=' + $('#hidCodProveedor').val());
    
	}     
}).navGrid('#jqGrid_pager_A', { edit: false, add: false, del: false });
$("#search_jqGrid_lista_A").hide();

}

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	
// Log			:: 	WCR - 14/05/2012
//****************************************************************
function fn_buscarProveedor(pblnBusqueda) {
    blnPrimeraBusqueda = pblnBusqueda;
	intPaginaActual = 1;
    fn_buscar();
}
function fn_buscar() {
	
	if(!blnPrimeraBusqueda) 
	{
		return;
		
	} else {
		parent.fn_blockUI();
		var CodigoTipoDocumento = $('#cmbTipoDocumento').val() == undefined ? "" : $('#cmbTipoDocumento').val();
		var NroDocumento = $('#txtNroDocumento').val() == undefined ? "" : $('#txtNroDocumento').val();
		var RazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
		
		var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página.
			"pCurrentPage", intPaginaActual, // Página actual.
			"pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
			"pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
			"pCodigoTipoDocumento", CodigoTipoDocumento,
			"pNumeroDocumento", NroDocumento,
			"pRazonSocial", RazonSocial
		];
	
		fn_util_AjaxWM("frmProveedorListado.aspx/BuscarProveedor",
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
	}
}

function fn_abreDetalle() {
    if ($('#hidCodProveedor').val() != '0') {
        fn_util_redirect('frmProveedorMant.aspx?co=1&cp=' + $('#hidCodProveedor').val());
    } else {
        parent.fn_mdl_mensajeIco("&nbsp;&nbsp;- Debe seleccionar un registro de la lista", "Util/images/warning.gif", "ADVERTENCIA AL EDITAR");
    }
}


////************************************************************
//// Función		:: 	fn_validarCampos
//// Descripcion 	:: 	Método para validar campos por el tipo de documento
//// Log			:: 	AEP - 10/07/2012
////************************************************************
//function fn_validarCampos(pvalue) {
//	$("#txtNroDocumento").val('');
//	if (pvalue == '0') {
//		$("#txtNroDocumento").attr('disabled', 'disabled');;
//    }
//	if (pvalue == '1') {
//    	$("#txtNroDocumento").attr('disabled', false);;
//		$("#txtNroDocumento").validText({ type: 'number', length: 8 });
//    }
//	if(pvalue == '2') {
//		$("#txtNroDocumento").attr('disabled', false);;
//	    $("#txtNroDocumento").validText({ type: 'number', length: 11 });
//	}
//	if(pvalue=='3') {
//		$("#txtNroDocumento").attr('disabled', false);;
//		$("#txtNroDocumento").validText({ type: 'alphanumeric', length: 11 });
//	}
//	if (pvalue=='5') {
//		$("#txtNroDocumento").attr('disabled', false);;
//	    $("#txtNroDocumento").validText({ type: 'alphanumeric', length: 11 });	
//	}
//	if (pvalue=='6') {
//		$("#txtNroDocumento").attr('disabled', false);;
//	    $("#txtNroDocumento").validText({ type: 'alphanumeric', length: 11 });		
//	}
//}

//************************************************************
// Función		:: 	fn_LimpiarCampos
// Descripcion 	:: 	Método para limpiar los campos de la busqueda
// Log			:: 	AEP - 10/07/2012
//************************************************************

function fn_LimpiarCampos() {
	blnPrimeraBusqueda=false;
	$("#txtNroDocumento").val('');
	$("#txtRazonSocial").val('');
	$("#cmbTipoDocumento").val(0);
	$("#jqGrid_lista_A").GridUnload();
	fn_cargaGrilla();
}
