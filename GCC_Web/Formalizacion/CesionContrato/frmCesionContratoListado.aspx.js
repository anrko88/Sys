//****************************************************************
// Variables Globales
//****************************************************************
var strComboVacio = "<option value='0'>[-Seleccione-]</option>"

var blnPrimeraBusqueda = false;
var intPaginaActual = 1;

var strTipoDocumentoDNI = "1";
var strTipoDocumentoRUC = "2";
var strTipoDocumentoCarnetEx = "3";
var strTipoDocumentoPasaporte = "5";
var strTipoDocumentoOtroDoc = "6";


//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 04/01/2013
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();
        	
    //Carga Grilla
    fn_cargaGrilla();
    
    //Busca con Enter
    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscarCesion(true);
        }
    });
        
    //---------------------------------
    //Valida Tipo Documento
    //---------------------------------
    $('#cmdTipoDoc').change(function() {
        var strValor = $(this).val();
        $("#txtNroDocumento").val("");
        $('#txtNroDocumento').unbind('keypress');
        if (fn_util_trim(strValor) == strTipoDocumentoDNI) {
            $('#txtNroDocumento').validText({ type: 'number', length: 8 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoRUC) {
            $('#txtNroDocumento').validText({ type: 'number', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoCarnetEx) {
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoPasaporte) {
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else {
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        }
    });
        	
     
    //-------------------------------------------
    //Valida Estados de Contrato
    //-------------------------------------------
    $("#cmbEstadoContrato option[value='01']").remove();
    $("#cmbEstadoContrato option[value='02']").remove();
    $("#cmbEstadoContrato option[value='03']").remove();
    $("#cmbEstadoContrato option[value='04']").remove();
    $("#cmbEstadoContrato option[value='05']").remove();
    $("#cmbEstadoContrato option[value='06']").remove();
    $("#cmbEstadoContrato option[value='07']").remove();
    $("#cmbEstadoContrato option[value='09']").remove();
    $("#cmbEstadoContrato option[value='15']").remove();
    
            	
    //On load Page (siempre al final)
    fn_onLoadPage();
    
});




//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 04/01/2013
//****************************************************************
function fn_inicializaCampos() {	
	$('#txtNroContrato').validText({ type: 'number', length: 8 });
	$('#txtCUCliente').validText({ type: 'number', length: 10 });
	$('#txtNroDocumento').validText({ type: 'number', length: 11 });
	$('#txtRazonSocial').validText({ type: 'comment', length: 100 });	
}




//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla Listado de Cotizaciones
// Log			:: 	JRC - 04/01/2013
//****************************************************************
function fn_cargaGrilla() {

     $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");	
            fn_realizaBusquedaCesion();
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
        colNames: ['','','','Nº Contrato','CU Cliente','Razón Social o Nombre','Clasificación del Bien','Ubigeo','F. Vencimiento','Estado Contrato','Cesión de Posición'],
        colModel: [				
				{ name: 'DesDepartamento',		index: 'DesDepartamento',		hidden: true},	
				{ name: 'DesProvincia',			index: 'DesProvincia',			hidden: true},	
				{ name: 'DesDistrito',			index: 'DesDistrito',			hidden: true},	
                { name: 'CodSolicitudCredito',	index: 'CodSolicitudCredito',	sortable: true, width: 25,	sorttype: "string",	align: "center", defaultValue: "" },
                { name: 'CodUnico',				index: 'CodUnico',				sortable: true, width: 30,	sorttype: "string", align: "center", defaultValue: "" },
                { name: 'ClienteRazonSocial',	index: 'ClienteRazonSocial',	sortable: true,				sorttype: "string", align: "left", defaultValue: "" },
                { name: 'ClasificacionBien',	index: 'ClasificacionBien',		sortable: true, width: 55,	sorttype: "string", align: "center", defaultValue: ""},
                { name: 'Ubigeo',				index: 'Ubigeo',				sortable: true, width: 55,	sorttype: "string", align: "left", defaultValue: "", formatter:fn_Ubigeo },
                { name: 'FechaVencimiento',		index: 'FechaVencimiento',		sortable: true, width: 25,	sorttype: "string", align: "center", defaultValue: ""},                
                { name: 'DesEstadoContrato',	index: 'DesEstadoContrato',		sortable: true, width: 25,	sorttype: "string", align: "center", defaultValue: ""},                                
                { name: 'CesionPosicion',		index: 'CesionPosicion',		width: 25,		align: "center", sortable: false, formatter: fn_tienePosision }
        ],
        width: glb_intWidthPantalla-70,
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodContrato").val(rowData.CodSolicitudCredito);
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            window.location = "frmCesionContratoRegistro.aspx?hddCodContrato=" + rowData.CodSolicitudCredito;
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

	//**************************************************
    // Ubigeo
    //**************************************************
    function fn_Ubigeo(cellvalue, options, rowObject) {      
		if(fn_util_trim(rowObject.DesDepartamento) == ""){
			return ".";
		}else{
			return fn_util_trim(rowObject.DesDepartamento)+" / "+fn_util_trim(rowObject.DesProvincia)+" / "+fn_util_trim(rowObject.DesDistrito);
		}
        
    };
    
	//**************************************************
    // TieneSiniestro
    //**************************************************
    function fn_tienePosision(cellvalue, options, rowObject) {        
        if (fn_util_trim(rowObject.CesionPosicion) != "0") {
            return "<img src='../../Util/images/ok.gif' alt='" + cellvalue + "' title='El Contrato tiene Cesión de Posición' width='18px' />";
        } else {
            return ".";//<img src='../../Util/images/ok_des.gif' alt='" + cellvalue + "' title='El Contrato No tiene Cesión de Posición' width='18px'/>";
        }        
    };

}




//****************************************************************
// Funcion		:: 	fn_buscarCesion
// Descripción	::	Busca listado de Cesiones por parametros
// Log			:: 	JRC - 04/01/2013
//****************************************************************
function fn_buscarCesion(pblnBusqueda) {
    blnPrimeraBusqueda = pblnBusqueda;
	intPaginaActual = 1;
    fn_realizaBusquedaCesion();
}
function fn_realizaBusquedaCesion() {
    if (!blnPrimeraBusqueda) {
        return;
    }

    try {
        parent.fn_blockUI();

        var txtNroContrato = $('#txtNroContrato').val() == undefined ? "" : $('#txtNroContrato').val();
        var txtCUCliente = $('#txtCUCliente').val() == undefined ? "" : $('#txtCUCliente').val();
        var txtRazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
        var cmdTipoDoc = $('#cmdTipoDoc').val() == undefined ? "" : $('#cmdTipoDoc').val();
        var txtNroDocumento = $('#txtNroDocumento').val() == undefined ? "" : $('#txtNroDocumento').val();
        var cmdClasificacion = $('#cmdClasificacion').val() == undefined ? "" : $('#cmdClasificacion').val();
        var cmbEstadoContrato = $('#cmbEstadoContrato').val() == undefined ? "" : $('#cmbEstadoContrato').val();
        var hddCesionPosicion = $('#hddCesionPosicion').val() == undefined ? "" : $('#hddCesionPosicion').val();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación                             
                             "pstrNroContrato", txtNroContrato,
                             "pstrCUCliente", txtCUCliente,
                             "pstrRazonSocial",txtRazonSocial,
                             "pstrTipoDoc", cmdTipoDoc,
                             "pstrNroDocumento", txtNroDocumento,                             
                             "pstrClasificacion", cmdClasificacion,
                             "pstrEstadoContrato", cmbEstadoContrato,
                             "pstrCesionPosicion", hddCesionPosicion
                            ];

		//alert(arrParametros);
        fn_util_AjaxWM("frmCesionContratoListado.aspx/ListaCesionContrato",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);
                    parent.fn_unBlockUI();
                    $("#hddCodContrato").val("");
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
// Funcion		:: 	fn_limpiar
// Descripción	::	Realiza limpieza de Busqueda
// Log			:: 	JRC - 04/01/2013
//****************************************************************
function fn_limpiar() {	
	$("#txtNroContrato").val("");
	$("#cmbEstadoContrato").val("0");
	$("#txtCUCliente").val("");	
	$("#txtNroDocumento").val("");
	$("#txtRazonSocial").val("");
	$("#cmdTipoDoc").val("0");
	$("#cmdClasificacion").val("0");		
	
	$("input:radio").attr("checked", false);	
	$("#hddCesionPosicion").val("");	
	
	blnPrimeraBusqueda = false;
	$("#jqGrid_lista_A").GridUnload();
    fn_cargaGrilla();
	
}


//****************************************************************
// Funcion		:: 	fn_abreEditar
// Descripción	::	Abre Editar Registro
// Log			:: 	JRC - 04/01/2013
//****************************************************************
function fn_abreEditar() {	
	var codContrato = $("#hddCodContrato").val();
	if(fn_util_trim(codContrato) == ""){
		parent.fn_mdl_mensajeError("Debe seleccionar un Contrato", function() { }, "VALIDACIÓN");
	}else{
		fn_util_globalRedirect("/Formalizacion/CesionContrato/frmCesionContratoRegistro.aspx?hddCodContrato=" + codContrato);	 
	}	
}



//****************************************************************
// Funcion		:: 	fn_abreCesionarios
// Descripción	::	Abre Editar Registro
// Log			:: 	JRC - 02/11/2013
//****************************************************************
function fn_abreCesionarios() {	
	var codContrato = $("#hddCodContrato").val();
	if(fn_util_trim(codContrato) == ""){
		parent.fn_mdl_mensajeError("Debe seleccionar un Contrato", function() { }, "VALIDACIÓN");
	}else{
		parent.fn_util_AbreModal("Cesión de Contrato :: Registro", "Formalizacion/CesionContrato/frmCesionarioRegistro.aspx?hddCodContrato=" + codContrato, 950, 560, function() { });	
	}		
}


//****************************************************************
// Funcion		:: 	fn_seteaValorRadio
// Descripción	::	Setea Valor Radio
// Log			:: 	JRC - 02/11/2013
//****************************************************************
function fn_seteaValorRadio(strValor) {	
	$("#hddCesionPosicion").val(strValor);	
}

