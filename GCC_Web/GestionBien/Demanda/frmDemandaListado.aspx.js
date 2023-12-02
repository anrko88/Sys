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
// Log			:: 	JRC - 07/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();
        	
    //Carga Grilla
    fn_cargaGrilla();
            
    //Busca con Enter
    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscarDemanda(true);
        }
    });
    
    //-------------------------------------------
    //Valida Change del ClasificacionBien
    //-------------------------------------------
    $('#cmdClasificacion').change(function() {
        var strValor = $(this).val();
        fn_oc_clasificacionBien(strValor);        
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
    $("#cmdEstadoContrato option[value='01']").remove();
    $("#cmdEstadoContrato option[value='02']").remove();
    $("#cmdEstadoContrato option[value='03']").remove();
    $("#cmdEstadoContrato option[value='04']").remove();
    $("#cmdEstadoContrato option[value='05']").remove();
    $("#cmdEstadoContrato option[value='06']").remove();
    $("#cmdEstadoContrato option[value='07']").remove();
    $("#cmdEstadoContrato option[value='09']").remove();
    $("#cmdEstadoContrato option[value='15']").remove();   
        	
    //On load Page (siempre al final)
    fn_onLoadPage();
    
});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 07/11/2012
//****************************************************************
function fn_inicializaCampos() {
	
	//cmdEstadoContrato
	//cmdTipoDoc
	//cmdClasificacion
	//cmdTipoBien
	$('#txtNroContrato').validText({ type: 'number', length: 8 });
	$('#txtCUCliente').validText({ type: 'number', length: 10 });
	$('#txtNroDocumento').validText({ type: 'number', length: 11 });
	$('#txtRazonSocial').validText({ type: 'comment', length: 100 });
	$('#txtPlaca').validText({ type: 'alphanumeric', length: 10 });
	$('#txtMotor').validText({ type: 'alphanumeric', length: 20 });
	$('#txtUbicacion').validText({ type: 'comment', length: 100 });

}




//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla Listado de Cotizaciones
// Log			:: 	JRC - 07/11/2012
//****************************************************************
function fn_cargaGrilla() {

     $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");	
            fn_realizaBusquedaDemanda();
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
        colNames: ['','Nº Contrato','Razón Social o Nombre','Clasificación del Bien','Tipo de Bien','Ejecutivo Banca','Ejecutivo Leasing','Placa Actual','Ubicación','Moneda','Valor del Bien','F. Transferencia por OC','Descripción del Bien','Estado del Contrato','Demanda'],
        colModel: [
                { name: 'SecFinanciamiento',	index: 'SecFinanciamiento', hidden: true },
                { name: 'CodSolicitudCredito',	index: 'CodSolicitudCredito',			sortable: true, sorttype: "int",	align: "center", defaultValue: "" },
                { name: 'ClienteRazonSocial',	index: 'ClienteRazonSocial',			sortable: true, sorttype: "string", align: "left", defaultValue: "" },
                { name: 'ClasificacionBien',	index: 'ClasificacionBien',		sortable: true, sorttype: "string", align: "center", defaultValue: "", hidden: true },
                { name: 'TipoBien',				index: 'TipoBien',				sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'DesEjecutivoBanca',	index: 'DesEjecutivoBanca',		sortable: true, sorttype: "string", align: "left", defaultValue: "", hidden: true },
                { name: 'NombreEjecutivoleasing', index: 'NombreEjecutivoleasing',		sortable: true, sorttype: "string", align: "left", defaultValue: "", hidden: true },
                { name: 'Placa',				index: 'Placa',					sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'Ubicacion',			index: 'Ubicacion',				sortable: true, sorttype: "string", align: "left", defaultValue: "", hidden: true },
                { name: 'NombreMoneda',			index: 'NombreMoneda',				sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'ValorBien',			index: 'ValorBien',				sortable: true, sorttype: "string", align: "right", defaultValue: "0.00", formatter: fn_ValorBien },                
                { name: 'FechaTransferencia',	index: 'FechaTransferencia',	sortable: true, sorttype: "string", align: "center"},
                { name: 'Comentario',			index: 'Comentario',			sortable: true, sorttype: "string", align: "left", hidden: true },
                { name: 'EstadoContrato',		index: 'EstadoContrato',		sortable: true, sorttype: "string", align: "center" },
                { name: 'TieneDemanda',			index: 'TieneDemanda',		align: "center", sortable: false, formatter: fn_tieneDemanda }
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
            $("#hddCodBien").val(rowData.SecFinanciamiento);
        },

        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            window.location = "frmDemandaRegistro.aspx?hddCodContrato=" + rowData.CodSolicitudCredito + "&hddCodBien=" + rowData.SecFinanciamiento;
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

	//**************************************************
    // TieneDemanda
    //**************************************************
    function fn_tieneDemanda(cellvalue, options, rowObject) {
        if (fn_util_trim(rowObject.TieneDemanda) != "0") {
            return "<img src='../../Util/images/ok.gif' alt='" + cellvalue + "' title='El Bien tiene "+rowObject.TieneDemanda+" demandas registradas' width='18px' />";
        } else {
            return ".";//<img src='../../Util/images/ok_des.gif' alt='" + cellvalue + "' title='No tiene demandas registradas' width='18px'/>";
        }  
    };
    
    //**************************************************
    // ValorBien
    //**************************************************
    function fn_ValorBien(cellvalue, options, rowObject) {  
		strValorBien = fn_util_ValidaDecimal(rowObject.ValorBien);             
		return ""+fn_util_ValidaMonto(strValorBien, 2);       
    };
        
}




//****************************************************************
// Funcion		:: 	fn_buscarCotizacion
// Descripción	::	Busca listado de cotizacion por parametros
// Log			:: 	JRC - 07/11/2012
//****************************************************************
function fn_buscarDemanda(pblnBusqueda) {
    blnPrimeraBusqueda = pblnBusqueda;
	intPaginaActual = 1;
    fn_realizaBusquedaDemanda();
}
function fn_realizaBusquedaDemanda() {
    if (!blnPrimeraBusqueda) {
        return;
    }

    try {
        parent.fn_blockUI();

        var txtNroContrato = $('#txtNroContrato').val() == undefined ? "" : $('#txtNroContrato').val();
        var cmdEstadoContrato = $('#cmdEstadoContrato').val() == undefined ? "" : $('#cmdEstadoContrato').val();
        var txtCUCliente = $('#txtCUCliente').val() == undefined ? "" : $('#txtCUCliente').val();
        var cmdTipoDoc = $('#cmdTipoDoc').val() == undefined ? "" : $('#cmdTipoDoc').val();
        var txtNroDocumento = $('#txtNroDocumento').val() == undefined ? "" : $('#txtNroDocumento').val();
        var txtRazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
        var cmdClasificacion = $('#cmdClasificacion').val() == undefined ? "" : $('#cmdClasificacion').val();
        var txtPlaca = $('#txtPlaca').val() == undefined ? "" : $('#txtPlaca').val();
        var txtMotor = $('#txtMotor').val() == undefined ? "" : $('#txtMotor').val();
        var cmdTipoBien = $('#cmdTipoBien').val() == undefined ? "" : $('#cmdTipoBien').val();
        var txtUbicacion = $('#txtUbicacion').val() == undefined ? "" : $('#txtUbicacion').val();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación                             
                             "pstrNroContrato", txtNroContrato,
                             "pstrEstadoContrato", cmdEstadoContrato,
                             "pstrCUCliente", txtCUCliente,
                             "pstrTipoDoc", cmdTipoDoc,
                             "pstrNroDocumento", txtNroDocumento,
                             "pstrRazonSocial", txtRazonSocial,
                             "pstrClasificacion", cmdClasificacion,
                             "pstrPlaca", txtPlaca,
                             "pstrMotor", txtMotor,
                             "pstrTipoBien", cmdTipoBien,
                             "pstrUbicacion", txtUbicacion
                            ];

        fn_util_AjaxWM("frmDemandaListado.aspx/ListaDemanda",
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




//************************************************************
// Función		:: 	fn_oc_clasificacionBien
// Descripcion 	:: 	Método Clasificacion
// Log			:: 	JRC - 07/11/2012
//************************************************************
function fn_oc_clasificacionBien(strValor) {

    var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL104", "pstrCodigoGenerico", strValor];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmdTipoBien').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}



//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_limpiar() {
	
	$("#txtNroContrato").val("");
	$("#cmbEstadoContrato").val("0");
	$("#txtCUCliente").val("");
	$("#cmdEstadoContrato").val("0");
	$("#txtNroDocumento").val("");
	$("#txtRazonSocial").val("");
	$("#cmdTipoDoc").val("0");
	$("#txtPlaca").val("");
	$("#txtMotor").val("");
	$("#cmdClasificacion").val("0");
	$("#cmdTipoBien").val("0");
	$("#txtUbicacion").val("");
	
	$('#cmdTipoBien').html(strComboVacio);

}

//****************************************************************
// Funcion		:: 	fn_abreEditar
// Descripción	::	Abre Editar Registro
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_abreEditar() {
	var codBien = $("#hddCodBien").val();
	if(fn_util_trim(codBien) == ""){
		parent.fn_mdl_mensajeError("Debe seleccionar un Bien", function() { }, "VALIDACIÓN");
	}else{
		fn_util_globalRedirect("/GestionBien/Demanda/frmDemandaRegistro.aspx?hddCodContrato=" + $("#hddCodContrato").val() + "&hddCodBien=" + $("#hddCodBien").val());	 
	}	
}


//****************************************************************
// Funcion		:: 	fn_abreNuevo
// Descripción	::	Abre Nuevo Registro
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_abreNuevo() {
	fn_util_globalRedirect("/GestionBien/Demanda/frmDemandaRegistro.aspx");
}




