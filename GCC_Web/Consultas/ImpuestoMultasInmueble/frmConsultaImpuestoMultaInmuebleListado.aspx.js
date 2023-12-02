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
// Log			:: 	??? - 02/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();

    //Busca con Enter
    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscarImpuestoMuni(true);
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
    

    //On load Page (siempre al final)
    fn_onLoadPage();
    fn_configuraGrilla();
});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {

	//cmbDepartamento
	//cmbProvincia
	//cmbDistrito
	//cmdTipoDoc
	//cmdEstadoPago
	//cmdEstadoCobro

	$('#txtNroContrato').validText({ type: 'number', length: 8 });
	$('#txtRazonSocial').validText({ type: 'comment', length: 100 });
	$('#txtNroDocumento').validText({ type: 'number', length: 11 });
	$('#txtPeriodo').validText({ type: 'number', length: 4 });
	$('#txtLote').validText({ type: 'number', length: 8 });
	
}


//****************************************************************
// Funcion		:: 	fn_configuraGrilla
// Descripción	::	Inicializa grilla
// Log			:: 	JRC - 02/11/2012
//****************************************************************
function fn_configuraGrilla() {
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_realizaBusquedaImpuestoMuni();            
        },
        jsonReader: {                 // Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage",      // Número de página actual.
            total: "PageCount",       // Número total de páginas.
            records: "RecordCount",   // Total de registros a mostrar.
            repeatitems: false,
            id: "SecImpuesto" // Índice de la columna con la clave primaria.
        },
        colNames: ['', '', 'Nº Contrato', 'Razón Social o Nombre', 'Tipo Documento', 'Nº Documento', 'Departamento', 'Provincia', 'Distrito', 'Ubicación', 'Periodo', 'Importe Total', 'Estado Pago', 'Estado Cobro', 'N° Lote', 'N° Cheque', ''],
        colModel: [
			{ name: 'SecImpuesto',			index: 'SecImpuesto',			hidden: true },
			{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
			{ name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', sortable: true, sorttype: "string", width: 25, align: "center" },
			{ name: 'ClienteRazonSocial', index: 'ClienteRazonSocial', sortable: true, sorttype: "string", width: 25, align: "left" },
		    { name: 'NombreTipoDocumento', index: 'NombreTipoDocumento', sortable: true, width: 25, sorttype: "string", align: "center" },
		    { name: 'NumeroDocumento', index: 'NumeroDocumento', sortable: true, width: 25, sorttype: "string", align: "center" },			
		    { name: 'DesDepartamento',		index: 'DesDepartamento',		sortable: true, width: 25, sorttype: "string", align: "center" },
		    { name: 'DesProvincia',			index: 'DesProvincia',			sortable: true, width: 25, sorttype: "string", align: "center" },
		    { name: 'DesDistrito',			index: 'DesDistrito',			sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'Ubicacion',			index: 'Ubicacion',				sortable: true, sorttype: "string", width: 25, align: "left" }, 		  		    
		    { name: 'Periodo',				index: 'Periodo',				sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'Total',				index: 'Total',					sortable: false, width: 25, align: "right" , sorttype: "string", formatter: Fn_util_ReturnValidDecimal2},
		    { name: 'DesEstadoPago',		index: 'DesEstadoPago',			sortable: false, width: 25, align: "center" , sorttype: "string"},
		    { name: 'DesEstadoCobro',		index: 'DesEstadoCobro',		sortable: false, width: 25, align: "center" , sorttype: "string"},
		    { name: 'NroLote',				index: 'NroLote',				sortable: false, width: 25, align: "center" , sorttype: "string"},
		    { name: 'NroCheque',			index: 'NroCheque',				sortable: false, width: 25, align: "center" , sorttype: "string"},
		    { name: '',						index: '',						width: 3 }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,                      // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito', // Columna a ordenar por defecto.
        sortorder: 'asc',               // Criterio de ordenación por defecto.
        viewrecords: true,               // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodContrato").val(rowData.CodSolicitudCredito);
            $("#hddCodBien").val(rowData.SecFinanciamiento);
            $("#hddCodImpuesto").val(rowData.SecImpuesto);
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodContrato").val(rowData.CodSolicitudCredito);
            $("#hddCodBien").val(rowData.SecFinanciamiento);
            $("#hddCodImpuesto").val(rowData.SecImpuesto);
            fn_abreEditar();
        }
    });

    $("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    
    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 55);
}


//****************************************************************
// Funcion		:: 	fn_buscarImpuestoMuni
// Descripción	::	Busca listado de cotizacion por parametros
// Log			:: 	JRC - 07/11/2012
//****************************************************************
function fn_buscarImpuestoMuni(pblnBusqueda) {
    blnPrimeraBusqueda = pblnBusqueda;
	intPaginaActual = 1;
    fn_realizaBusquedaImpuestoMuni();
}
function fn_realizaBusquedaImpuestoMuni() {
    if (!blnPrimeraBusqueda) {
        return;
    }

    try {
        parent.fn_blockUI();

        var cmbDepartamento = $('#cmbDepartamento').val() == undefined ? "" : $('#cmbDepartamento').val();
        var cmbProvincia = $('#cmbProvincia').val() == undefined ? "" : $('#cmbProvincia').val();
        var cmbDistrito = $('#cmbDistrito').val() == undefined ? "" : $('#cmbDistrito').val();
        var txtNroContrato = $('#txtNroContrato').val() == undefined ? "" : $('#txtNroContrato').val();
        var txtRazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
        var cmdTipoDoc = $('#cmdTipoDoc').val() == undefined ? "" : $('#cmdTipoDoc').val();
        var NroDocumento = $('#NroDocumento').val() == undefined ? "" : $('#NroDocumento').val();
        var txtPeriodo = $('#txtPeriodo').val() == undefined ? "" : $('#txtPeriodo').val();
        var txtLote = $('#txtLote').val() == undefined ? "" : $('#txtLote').val();
        var cmdEstadoPago = $('#cmdEstadoPago').val() == undefined ? "" : $('#cmdEstadoPago').val();
        var cmdEstadoCobro = $('#cmdEstadoCobro').val() == undefined ? "" : $('#cmdEstadoCobro').val();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación                             
                             "pstrDepartamento", cmbDepartamento,
                             "pstrProvincia", cmbProvincia,
                             "pstrDistrito", cmbDistrito,
                             "pstrNroContrato", txtNroContrato,
                             "pstrRazonSocial", txtRazonSocial,                             
                             "pstrTipoDoc", cmdTipoDoc,
                             "pstrDocumento", NroDocumento,
                             "pstrPeriodo", txtPeriodo,
                             "txtLote", txtLote,
                             "pstrEstadoPago", cmdEstadoPago,
                             "pstrEstadoCobro", cmdEstadoCobro
                            ];
                            
        fn_util_AjaxWM("frmConsultaImpuestoMultaInmuebleListado.aspx/ListaImpuestoMunicipal",
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
// Funcion		:: 	fn_LimpiaComboDistrito
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_LimpiaComboDistrito() {
    $('#cmbDistrito').empty();
    $('#cmbDistrito').html('<option value="0">[- Seleccionar -]</option>');
}

//****************************************************************
// Funcion		:: 	fn_cargaComboProvincia
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_cargaComboProvincia(valor) {

    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbProvincia').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
   fn_LimpiaComboDistrito ();
}


//****************************************************************
// Funcion		:: 	fn_cargaComboDistrito
// Descripción	::	F
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_cargaComboDistrito(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbDistrito').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}



//****************************************************************
// Funcion		:: 	fn_abreEditar
// Descripción	::	Edita Impuesto
// Log			:: 	JRC - 27/11/2012
//****************************************************************
function fn_abreEditar() {

	var strCodContrato	= $("#hddCodContrato").val();
	var strCodBien	 = $("#hddCodBien").val(); 
	var strCodImpuesto = $("#hddCodImpuesto").val();
	
	if( fn_util_trim(strCodImpuesto) == ""){
		parent.fn_mdl_mensajeError("Debe seleccionar un impuesto.", function() { }, "VALIDACIÓN");
	}else{	
		fn_util_globalRedirect("/Consultas/ImpuestoMultasInmueble/frmConsultaImpuestoMultaInmuebleRegistro.aspx?hddCodContrato="+strCodContrato+"&hddCodBien="+strCodBien+"&hddCodImpuesto="+strCodImpuesto);
	}
    
}

//****************************************************************
// Funcion		:: 	fn_limpiar
// Descripción	::	Limpiar
// Log			:: 	JRC - 24/11/2012
//****************************************************************
function fn_limpiar() {
	
	$("#cmbDepartamento").val("0");
	$("#cmbProvincia").html(strComboVacio);
	$("#cmbDistrito").html(strComboVacio);
	$("#txtNroContrato").val("");
	$("#txtRazonSocial").val("");
	$("#cmdTipoDoc").val("0");
	$("#txtNroDocumento").val("");
	$("#txtPeriodo").val("");
	$("#txtLote").val("");
	$("#cmdEstadoPago").val("0");
	$("#cmdEstadoCobro").val("0");
	
}


/*****************************************************************
Funcion		:: 	fn_Reporte
Descripción	::	Genera Reporte
Log			:: 	SCA - 25/01/2013
***************************************************************** */

function fn_Reporte() {
	$("#btnGenerar").click();
//	try {
//        parent.fn_blockUI();
//        var cmbDepartamento = $('#cmbDepartamento').val() == undefined ? "" : $('#cmbDepartamento').val();
//        var cmbProvincia = $('#cmbProvincia').val() == undefined ? "" : $('#cmbProvincia').val();
//        var cmbDistrito = $('#cmbDistrito').val() == undefined ? "" : $('#cmbDistrito').val();
//        var txtNroContrato = $('#txtNroContrato').val() == undefined ? "" : $('#txtNroContrato').val();
//        var txtRazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
//        var cmdTipoDoc = $('#cmdTipoDoc').val() == undefined ? "" : $('#cmdTipoDoc').val();
//        var txtNroDocumento = $('#NroDocumento').val() == undefined ? "" : $('#NroDocumento').val();
//        var txtPeriodo = $('#txtPeriodo').val() == undefined ? "" : $('#txtPeriodo').val();
//        var txtLote = $('#txtLote').val() == undefined ? "" : $('#txtLote').val();
//        var cmdEstadoPago = $('#cmdEstadoPago').val() == undefined ? "" : $('#cmdEstadoPago').val();
//        var cmdEstadoCobro = $('#cmdEstadoCobro').val() == undefined ? "" : $('#cmdEstadoCobro').val();

//        var arrParametros = ["pstrDepartamento", cmbDepartamento,
//                             "pstrProvincia", cmbProvincia,
//                             "pstrDistrito", cmbDistrito,
//                             "pstrNroContrato", txtNroContrato,
//                             "pstrRazonSocial", txtRazonSocial,                             
//                             "pstrTipoDoc", cmdTipoDoc,
//                             "pstrDocumento", txtNroDocumento,
//                             "pstrPeriodo", txtPeriodo,
//                             "txtLote", txtLote,
//                             "pstrEstadoPago", cmdEstadoPago,
//                             "pstrEstadoCobro", cmdEstadoCobro
//                            ];
//                            
//        fn_util_AjaxWM("frmConsultaImpuestoMultaInmuebleListado.aspx/ExportarExcel",
//                arrParametros,
//                function(jsondata) {
//                    //jqGrid_lista_A.addJSONData(jsondata);
//                    parent.fn_unBlockUI();
//                    //fn_doResize();
//                },
//                function(request) {
//                    parent.fn_unBlockUI();
//                    //fn_util_alert(jQuery.parseJSON(request.responseText).Message);
//                }
//        );

//    } catch (ex) {
//        parent.fn_unBlockUI();
//        //fn_util_alert(ex.message);
//    }
}