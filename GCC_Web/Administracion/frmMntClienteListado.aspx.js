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
            fn_buscarCliente(true);
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
	
//	  var mydata = 
//              [
//    		    { Codigo: "00000001", CuCliente: " 0007052877", NumeroDocumento: "2545852465", NombreCliente: "DASO SA", Direccion: "xds444", NombreContacto: "Toyota", CorreoContacto: "Yaris",TelefonoContacto: "2002"},
//                { Codigo: "00000002", CuCliente: " 0007052877", NumeroDocumento: "2545852465", NombreCliente: "DASO SA", Direccion: "xds644", NombreContacto: "Toyota", CorreoContacto: "Yaris",TelefonoContacto: "2004"},
//              	{ Codigo: "00000003", CuCliente: " 0007052877", NumeroDocumento: "2545852465", NombreCliente: "DASO SA", Direccion: "eds544", NombreContacto: "Toyota", CorreoContacto: "Corolla",TelefonoContacto: "2010"},
//              	{ Codigo: "00000004", CuCliente: " 0007052877", NumeroDocumento: "2545852465", NombreCliente: "DASO SA", Direccion: "rvds44", NombreContacto: "Nissan", CorreoContacto: "Sunny",TelefonoContacto: "2010"},
//             	{ Codigo: "00000005", CuCliente: " 0007052877", NumeroDocumento: "2545852465", NombreCliente: "DASO SA", Direccion: "x4s444", NombreContacto: "Nissan", CorreoContacto: "Sunny",TelefonoContacto: "2011"},
//              	{ Codigo: "00000006", CuCliente: " 0007052877", NumeroDocumento: "2545852465", NombreCliente: "DASO SA", Direccion: "x6s444", NombreContacto: "Nissan", CorreoContacto: "Primera",TelefonoContacto: "2003"}

//    		  ];

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");	    
            fn_buscar();
        },
    	//datatype: "local",
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
        root: "Items",
        page: "CurrentPage", // Número de página actual.
        total: "PageCount", // Número total de páginas.
        records: "RecordCount", // Total de registros a mostrar.
        repeatitems: false,
        id: "CodSubprestatario" // Índice de la columna con la clave primaria.
    },
    colNames: ['Código', 'CU Cliente', 'Nro Documento', 'Nombre Cliente', 'Dirección', 'Nombre Contacto Preferente','Correo Contacto Preferente','Teléfono Contacto Preferente', ''],
    colModel: [
        { name: 'CodSubprestatario', index: 'CodSubprestatario', width: 80, sorttype: "string", align: "center" },
        { name: 'CodUnico', index: 'CodUnico', width: 100, sorttype: "string", align: "center" },
		{ name: 'NumeroDocumento', index: 'Nro. NumeroDocumento', width: 75, sorttype: "string", align: "left" },
		{ name: 'NombreSubprestatario', index: 'NombreSubprestatario', width: 180, align: "left", sorttype: "string" },
		{ name: 'Direccion', index: 'Direccion', width: 100, align: "left", sorttype: "string" },
		{ name: 'NOMBRE', index: 'NOMBRE', width: 100, align: "center", sorttype: "string" },
    	{ name: 'CORREO', index: 'CORREO', width: 100, align: "center", sorttype: "string" },
    	{ name: 'TELEFONO', index: 'TELEFONO', width: 100, align: "center", sorttype: "string" },
    	//{ name: 'e', index: 'e', width: 50, align: "center", sortable: false, formatter: Lupa },
		{ name: 'a', index: 'a',  hidden: true,width:2 }
	],
    height: '100%',
    pager: '#jqGrid_pager_A',
    loadtext: 'Cargando datos...',
    emptyrecords: 'No hay resultados',
    rowNum: 10, // Tamaño de la página
    rowList: [10, 20, 30],
    sortname: 'CodSubprestatario',
    sortorder: 'asc',
    viewrecords: true,
    gridview: true,
    autowidth: true,
    altRows: true,
    altclass: 'gridAltClass',
    onSelectRow: function(id) {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
    	$("#hidRowID").val(id);
        $("#hidCodigoSup").val(rowData.CodSubprestatario);
    	$("#hidCodUnico").val(rowData.CodUnico);
    	$("#hidDireccion").val(rowData.Direccion);
    }  
   	    });
   	
//    	for (var i = 0; i <= mydata.length; i++) {
//    	     jQuery("#jqGrid_lista_A").jqGrid('addRowData', i + 1, mydata[i]);
//    	 }
//   	
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
	
//	  function Lupa(cellvalue, options, rowObject) {

//	  	var strDireccion = '\"'+ rowObject.Direccion + '\"';
//	  	alert(strDireccion);
//          var sScript2 = "javascript:VerDetalle(" + rowObject.CodSubprestatario + "," + rowObject.CodUnico + "," + strDireccion + ");";
//       	  return "<img src='../Util/images/ico_acc_editar.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
//    
//    	
//    };

}

//function VerDetalle(strcodSup,strCodUni,strDir) {
//    var sTitulo = "Gestión del Bien";
//    var sSubTitulo = "Mantenimiento - Cliente :: Registro  ";
//    parent.fn_util_AbreModal(sSubTitulo, "Administracion/frmMntClienteModificacion.aspx?csup=" + strcodSup + "&cuni=" + strCodUni + "&dir=" + strDir + "&Add=true", 650, 300, function() { });
//}


function VerDetalle() {
	var id = $('#hidRowID').val(); 
	if (id!="") {
        
        var sSubTitulo = "Mantenimiento - Cliente :: Registro  ";
        parent.fn_util_AbreModal(sSubTitulo, "Administracion/frmMntClienteModificacion.aspx?csup=" + $("#hidCodigoSup").val() + "&cuni=" + $("#hidCodUnico").val() + "&dir=" + $("#hidDireccion").val() + "&Add=true", 650, 300, function() { });

    } else {
        parent.fn_mdl_mensajeIco("&nbsp;&nbsp;- Debe seleccionar un registro de la lista", "Util/images/warning.gif", "ADVERTENCIA AL EDITAR");
    }
	
	}

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	
// Log			:: 	WCR - 14/05/2012
//****************************************************************
function fn_buscarCliente(pblnBusqueda) {
    blnPrimeraBusqueda = pblnBusqueda;
	intPaginaActual = 1;
    fn_buscar();
}
function fn_buscar() {
	fn_cargaGrilla();
	if(!blnPrimeraBusqueda) 
	{
		return;
		
	} else {
		parent.fn_blockUI();
		var Codigo = $('#txtCodigo').val() == undefined ? "" : $('#txtCodigo').val();
		var CuCliente= $('#txtCUCliente').val() == undefined ? "" : $('#txtCUCliente').val();
		var Nombre = $('#txtNombreSuprestatario').val() == undefined ? "" : $('#txtNombreSuprestatario').val();
		var Direccion = $('#txtDireccion').val() == undefined ? "" : $('#txtDireccion').val();
		var TipoDocumento= $('#ddlTipoDocumento').val() == undefined ? "" : $('#ddlTipoDocumento').val();
		var Documento = $('#txtDocumento').val() == undefined ? "" : $('#txtDocumento').val();
		
		var NombreC = $('#txtNombreContacto').val() == undefined ? "" : $('#txtNombreContacto').val();
		var CorreoC = $('#txtCorreoContacto').val() == undefined ? "" : $('#txtCorreoContacto').val();
		var TelefonoC = $('#txtTelefonoContacto').val() == undefined ? "" : $('#txtTelefonoContacto').val();
		
		
		
		var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página.
			"pCurrentPage", intPaginaActual, // Página actual.
			"pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
			"pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
			"pCodigo", Codigo,
			"pCuCliente", CuCliente,
			"pNombre", Nombre,
			"pDireccion", Direccion,
			"pTipoDocumento", TipoDocumento,
			"pDocumento", Documento,
			"pNombreC", NombreC,
			"pCorreoC", CorreoC,
			"pTelefonoC", TelefonoC
		];
	
		fn_util_AjaxWM("frmMntClienteListado.aspx/BuscarSuprestatarios",
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
