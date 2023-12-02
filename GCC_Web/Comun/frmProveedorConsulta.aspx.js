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
    //Carga Grilla
    fn_cargaGrilla();
    $("#jqGrid_listado").setGridWidth($(window).width() - 50);

    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscarProveedor(true);
        }
    });
    
	$("#txtRazonSocial").validText({ type: 'comment', length: 100 });	
	$('#txtNroDocumento').attr('disabled','disabled');
	
	// Valida el ingreso de datos en tipo documento
	  $('#cmbTipoDocumento').change(function() {
        var strValor = $(this).val();
        $("#txtNroDocumento").val("");
        $('#txtNroDocumento').unbind('keypress');
        if (fn_util_trim(strValor) == strTipoDocumentoDNI) {
        	$('#txtNroDocumento').attr('disabled',false);
            $('#txtNroDocumento').validText({ type: 'number', length: 8 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoRUC) {
        	$('#txtNroDocumento').attr('disabled',false);
            $('#txtNroDocumento').validText({ type: 'number', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoCarnetEx) {
        	$('#txtNroDocumento').attr('disabled',false);
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoPasaporte) {
        	$('#txtNroDocumento').attr('disabled',false);
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if(fn_util_trim(strValor)==strTipoDocumentoOtroDoc){
        	$('#txtNroDocumento').attr('disabled',false);
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else {
        	$('#txtNroDocumento').attr('disabled','disabled');
        }
    });

    //On load Page (siempre al final)
    fn_onLoadPage();
	
});

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
	
	if(!blnPrimeraBusqueda) {
		return;
		
	} else {
	try {
	    parent.fn_blockUI();
        
	    var codigoTipoDocumento = $('#cmbTipoDocumento').val() == undefined ? "" : $('#cmbTipoDocumento').val();
	    var nroDocumento = $('#txtNroDocumento').val() == undefined ? "" : $('#txtNroDocumento').val();
	    var razonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();

	    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página.
		    "pCurrentPage", intPaginaActual, // Página actual.
		    "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
		    "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
		    "pCodigoTipoDocumento", codigoTipoDocumento,
		    "pNumeroDocumento", nroDocumento,
		    "pRazonSocial", razonSocial
	    ];

	    fn_util_AjaxWM("frmProveedorConsulta.aspx/BuscarProveedor",
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
            id: "CodProveedor" // Índice de la columna con la clave primaria.
        },
        colNames: ['Item', 'Tipo Documento', 'Nro. Documento', 'Razón Social o Nombre', 'Tipo de Persona', 'Procedencia', 'CuentaD', 'CuentaS', 'CodigoTipoDocumento', 'CodProveedor','Tipo Nacionalidad'],
        colModel: [
            { name: 'Id', index: 'Id', width: 40, sorttype: "string", align: "right",sortable: false  },
            { name: 'TipoDocumento', index: 'TipoDocumento', width: 100, sorttype: "string", align: "center" },
	        { name: 'NumeroDocumento', index: 'NumeroDocumento', width: 85, sorttype: "string", align: "left" },
		    { name: 'RazonSocial', index: 'RazonSocial', width: 180, align: "left", sorttype: "string" },
		    { name: 'TipoPersona', index: 'TipoPersona', width: 100, align: "center", sorttype: "string" },
		    { name: 'Procedencia', index: 'Procedencia', width: 100, align: "center", sorttype: "string" },
		    { name: 'NumCuentaBcrME', index: 'NumCuentaBcrME', hidden: true },
		    { name: 'NumCuentaBcrMN', index: 'NumCuentaBcrMN', hidden: true },
		    { name: 'CodigoTipoDocumento', index: 'CodigoTipoDocumento', hidden: true },
		    { name: 'CodProveedor', index: 'CodProveedor', hidden: true },
            { name: 'TipoNacionalidad', index: 'TipoNacionalidad',hidden: true }
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
            //$("#hddCodigoProveedor").val(id);
            //$('#hddCodigoProveedor').val('');
            //alert(rowData.TipoNacionalidad);
            //, rowData.Ruc, rowData.RazonSocial, rowData.NumCuentaBcrME, rowData.NumCuentaBcrMN);
        },
        ondblClickRow: function(id) {

            //var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            //$("#hddCodigoProveedor").val(rowObject.CodProveedor);
            //fn_selectProveedor(rowObject);
            //$("#hddCodigoProveedor").val(id)
            //        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            //        //alert(rowData);
            //        //        alert(rowData.NumeroDocumento);
            //        //        alert(rowData.TipoNacionalidad);
            fn_selectProveedor(id);
                //fn_selectProveedor(id);
            //cellvalue, options, rowObject
            //cellvalue, options, rowObject
        }
    }).navGrid('#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
	
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 70);
}

//****************************************************************
// Funcion		:: 	fn_selectProveedor
// Descripción	::	Devuelve el registro seleccionado en la lista
// Log			:: 	WCR - 15/05/2012
//****************************************************************
function fn_selectProveedor(id) {
    //alert(intCodigoProveedor);
    //var RowData = $("#jqGrid_lista_A").jqGrid('getRowData', $("#hddCodigoProveedor").val());

    //alert(rowObject.TipoNacionalidad);
    //var id = $("#hddCodigoProveedor").val();
    
    var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
    
    var pag = window.parent.frames[0];
    pag.fn_obtenerProveedor(rowData.CodProveedor, rowData.CodigoTipoDocumento, rowData.NumeroDocumento, rowData.RazonSocial, rowData.TipoNacionalidad);
    parent.fn_util_CierraModal();
}

//****************************************************************
// Funcion		:: 	fn_limpiar
// Descripción	::	Devuelve el registro seleccionado en la lista
// Log			:: 	WCR - 15/05/2012
//****************************************************************
function fn_limpiar() {
	
	blnPrimeraBusqueda=false;
	
    $('#txtNroDocumento').val('');
    $('#txtRazonSocial').val('');
	$("#cmbTipoDocumento").val(0);
	
	$("#jqGrid_lista_A").GridUnload();
	fn_cargaGrilla();
}