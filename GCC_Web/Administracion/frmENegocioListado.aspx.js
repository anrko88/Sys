//VARIABLES GLOBALES
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
			id: "CodEjecutivo" // Índice de la columna con la clave primaria.
		},
		colNames: ['','','','Cod. Ejecutivo Negocio', 'Nombre Ejecutivo Negocio', 'Cod. Ejecutivo Leasing', 'Nombre Ejecutivo Negocio'],
		colModel: [
			{ name: 'ID_Tabla',					index: 'ID_Tabla',					hidden: true },
			{ name: 'ID_Registro',				index: 'ID_Registro',				hidden: true },
			{ name: 'Codigo',					index: 'Codigo',					hidden: true },
			{ name: 'CodigoEjecutivo',			index: 'CodigoEjecutivo',			width: 50,	sorttype: "string", align: "center",sortable: false },
			{ name: 'NombreEjecutivo',			index: 'NomENegocio',				width: 100, sorttype: "string", align: "left" },
			{ name: 'CodigoEjecutivoLeasing',	index: 'CodigoEjecutivoLeasing',	width: 50,	sorttype: "string", align: "center" },
			{ name: 'NombreEjecutivoLeasing',	index: 'NombreEjecutivoLeasing',	width: 100, align: "left", sorttype: "string" }			
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
			$("#hddCodEjecutivo").val(rowData.Codigo);	        
		},
   		ondblClickRow: function(id) {
			parent.fn_blockUI();
			$("#hddCodEjecutivo").val(rowData.Codigo);
			fn_abreEditar();	    
		}     
	}).navGrid('#jqGrid_pager_A', { edit: false, add: false, del: false });
	$("#search_jqGrid_lista_A").hide();

}

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	
// Log			:: 	WCR - 14/05/2012
//****************************************************************
function fn_buscarEjecutivo(pblnBusqueda) {
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
		var cmbTipo = $('#cmbTipo').val() == undefined ? "" : $('#cmbTipo').val();
		var txtCodEjecutivo = $('#txtCodEjecutivo').val() == undefined ? "" : $('#txtCodEjecutivo').val();
		var txtNomEjecutivo = $('#txtNomEjecutivo').val() == undefined ? "" : $('#txtNomEjecutivo').val();
		
		var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página.
			"pCurrentPage", intPaginaActual, // Página actual.
			"pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
			"pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
			"pTipo", cmbTipo,
			"pCodigo", txtCodEjecutivo,
			"pNombre", txtNomEjecutivo
		];
	
		fn_util_AjaxWM("frmENegocioListado.aspx/BuscarEjecutivo",
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


//************************************************************
// Función		:: 	fn_abreEditar
// Descripcion 	:: 	Método para limpiar los campos de la busqueda
// Log			:: 	JRC - 19/03/2013
//************************************************************
function fn_abreEditar() {
    if ($('#hddCodEjecutivo').val() != '0') {        
        parent.fn_util_AbreModal("Mantenimiento :: Ejecutivo Negocio", "Administracion/frmENegocioRegistro.aspx?co="+$('#hddCodEjecutivo').val(), 600, 220, function() { });
    } else {
        parent.fn_mdl_mensajeIco("&nbsp;&nbsp;- Debe seleccionar un Ejecutivo de la lista", "Util/images/warning.gif", "ADVERTENCIA");
    }
}


//************************************************************
// Función		:: 	fn_abreNuevo
// Descripcion 	:: 	Método para limpiar los campos de la busqueda
// Log			:: 	JRC - 19/03/2013
//************************************************************
function fn_abreNuevo() {    
	parent.fn_util_AbreModal("Mantenimiento :: Ejecutivo Negocio", "Administracion/frmENegocioRegistro.aspx?co=0", 600, 220, function() { });    
}


//************************************************************
// Función		:: 	fn_LimpiarCampos
// Descripcion 	:: 	Método para limpiar los campos de la busqueda
// Log			:: 	JRC - 19/03/2013
//************************************************************
function fn_LimpiarCampos() {
	blnPrimeraBusqueda=false;
	
	$("#txtCodEjecutivo").val('');
	$("#txtNomEjecutivo").val('');
	$("#cmbTipo").val(0);
	
	$("#jqGrid_lista_A").GridUnload();
	fn_cargaGrilla();
	
}




//****************************************************************
// Funcion		:: 	fn_grabadoOK 
// Descripción	::	Guardar Rechazo
// Log			:: 	JRC - 21/05/2012
//****************************************************************
function fn_eliminar(){

    if ($('#hddCodEjecutivo').val() != '0') {        

		//Variables
		var hddCodEjecutivo = fn_util_trim($("#hddCodEjecutivo").val());
		
		var paramArray = ["pstrCodEjecutivo", hddCodEjecutivo];
	    
		parent.fn_mdl_confirma(
            "¿Está seguro que desea eliminar el Ejecutivo seleccionado?  ",
            function() {
                parent.fn_blockUI();
                
                fn_util_AjaxWM("frmENegocioListado.aspx/EliminarEjecutivo",
                                   paramArray,
                                   function(resultado) {
                                       fn_buscarEjecutivo(true);
                                       parent.fn_unBlockUI();
                                   },
                                   function(resultado) {
                                       var error = eval("(" + resultado.responseText + ")");
                                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA ELIMINACIÓN");
                                   }
                    );
            },
            null,
            function() { },
            'VALIDACIÓN'
		);        
        
    } else {
        parent.fn_mdl_mensajeIco("&nbsp;&nbsp;- Debe seleccionar un Ejecutivo de la lista", "Util/images/warning.gif", "ADVERTENCIA");
    }
    
}


//************************************************************
// Función		:: 	fn_abreELeasing
// Descripcion 	:: 	Abre Leasing
// Log			:: 	JRC - 19/03/2013
//************************************************************
function fn_abreELeasing() {    
	parent.fn_util_AbreModal("Mantenimiento :: Ejecutivo Leasing", "Administracion/frmELeasingRegistro.aspx?co=0", 700, 600, function() { });    
}


