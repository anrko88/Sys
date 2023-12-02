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
    
    //Valida Campos
    fn_inicializaCampos();
    
    //Carga Grilla
    fn_cargaGrilla();
    
    //On load Page (siempre al final)
    fn_onLoadPage();
    
});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 20/03/2013
//****************************************************************
function fn_inicializaCampos() {
	fn_util_SeteaObligatorio($("#txtCodigo"), "input");
	fn_util_SeteaObligatorio($("#txtNombre"), "input");

	$('#txtCodigo').validText({ type: 'alphanumeric', length: 10 });
    $('#txtNombre').validText({ type: 'alphanumeric', length: 100 });    
}


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
		colNames: ['','','','Cod. Ejecutivo Negocio', 'Nombre Ejecutivo Negocio'],
		colModel: [
			{ name: 'ID_Tabla',					index: 'ID_Tabla',					hidden: true },
			{ name: 'ID_Registro',				index: 'ID_Registro',				hidden: true },
			{ name: 'Codigo',					index: 'Codigo',					hidden: true },
			{ name: 'CodigoEjecutivo',			index: 'CodigoEjecutivo',			width: 20,	sorttype: "string", align: "center",sortable: false },
			{ name: 'NombreEjecutivo',			index: 'NomENegocio',				width: 100, sorttype: "string", align: "left" }			
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
			
			$("#hddClave1").val(rowData.Codigo);
			$("#hddCodigo").val(rowData.CodigoEjecutivo);
			$("#hddNombre").val(rowData.NombreEjecutivo);
			       
		},
   		ondblClickRow: function(id) {
			parent.fn_blockUI();
			$("#hddCodEjecutivo").val(rowData.Codigo);
			$("#hddClave1").val(rowData.Codigo);
			$("#hddCodigo").val(rowData.CodigoEjecutivo);
			$("#hddNombre").val(rowData.NombreEjecutivo);
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
    blnPrimeraBusqueda = true;
	intPaginaActual = 1;
    fn_buscar();
}
function fn_buscar() {
	blnPrimeraBusqueda = true;
	if(!blnPrimeraBusqueda) 
	{
		return;
		
	} else {
		parent.fn_blockUI();
		var txtCodEjecutivo = $('#txtCodigo').val() == undefined ? "" : $('#txtCodigo').val();
		var txtNomEjecutivo = $('#txtNombre').val() == undefined ? "" : $('#txtNombre').val();
		
		var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página.
			"pCurrentPage", intPaginaActual, // Página actual.
			"pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
			"pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
			"pTipo", "",
			"pCodigo", txtCodEjecutivo,
			"pNombre", txtNomEjecutivo
		];
	
		fn_util_AjaxWM("frmELeasingRegistro.aspx/BuscarEjecutivo",
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
// Función		:: 	fn_LimpiarCampos
// Descripcion 	:: 	Método para limpiar los campos de la busqueda
// Log			:: 	JRC - 19/03/2013
//************************************************************
function fn_LimpiarCampos() {
	$("#hddClave1").val('');
	$("#hddCodigo").val('');
	$("#hddNombre").val('');
	$("#txtCodigo").val('');
	$("#txtNombre").val('');		
}




//****************************************************************
// Funcion		:: 	fn_eliminar 
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
                
                fn_util_AjaxWM("frmELeasingRegistro.aspx/EliminarEjecutivo",
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


//****************************************************************
// Funcion		:: 	fn_grabar 
// Descripción	::	Guardar 
// Log			:: 	JRC - 20/03/2013
//****************************************************************
function fn_grabar() {

    var blnGraba = true;
    var strError = "";

    if (fn_util_trim($("#txtCodigo").val()) == "") { 
        strError = strError +"- Debe ingresar un Código <br/>"
    }

	if (fn_util_trim($("#txtNombre").val()) == "") { 
        strError = strError +"- Debe ingresar un Nombre <br/>"
    }    
    
    if (fn_util_trim(strError) != "") {
        parent.fn_mdl_mensajeError(strError, function() { }, "VALIDACIÓN");
    } else {    
        parent.fn_blockUI();
        $("#btnGrabar").click();    
    }
    
}

//****************************************************************
// Funcion		:: 	fn_grabadoOK 
// Descripción	::	Guardar 
// Log			:: 	JRC - 20/03/2013
//****************************************************************
function fn_grabadoOK(strMensaje){
	parent.fn_mdl_alert(strMensaje, function() { fn_buscarEjecutivo(true); fn_LimpiarCampos(); });
}



//****************************************************************
// Funcion		:: 	fn_editar 
// Descripción	::	Guardar 
// Log			:: 	JRC - 20/03/2013
//****************************************************************
function fn_editar(){
	
	if ($('#hddClave1').val() != '0' && $('#hddClave1').val() != '') {   
	
		$("#txtCodigo").val($("#hddCodigo").val());
		$("#txtNombre").val($("#hddNombre").val());
		
		$("#dv_cancelar").show();
		$("#dv_grabar").show();
		$("#dv_eliminar").hide();	
		$("#dv_editar").hide();
		$("#dv_agregar").hide();
		
		$("#hddTipoTransaccion").val("EDITAR");

    } else {
        parent.fn_mdl_mensajeIco("&nbsp;&nbsp;- Debe seleccionar un Ejecutivo de la lista", "Util/images/warning.gif", "ADVERTENCIA");
    }
	
}


//****************************************************************
// Funcion		:: 	fn_cancelar 
// Descripción	::  Cancelar 
// Log			:: 	JRC - 20/03/2013
//****************************************************************
function fn_cancelar(){
	
	fn_LimpiarCampos();
	
	$("#dv_cancelar").hide();
	$("#dv_grabar").hide();
	$("#dv_eliminar").show();	
	$("#dv_editar").show();
	$("#dv_agregar").show();
	
	$("#hddTipoTransaccion").val("NUEVO");
	
}
