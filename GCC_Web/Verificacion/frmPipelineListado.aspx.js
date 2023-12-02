//***********************************************************************
// Funcion		:: 	JQUERY - Verificacion Listado Pipeline
// Descripción	::	
//					
// Log			:: 	
//***********************************************************************

var bFirstClick;


$(document).ready(function() {
    //Setea Calendario
   

    //Carga Grilla
    fn_cargaGrilla();

    //Inicializa Campos
    fn_inicializaCampos();
    //Busca con Enter
    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_Buscar(true);
        }
    });

    // On load Page (siempre al final)
    fn_onLoadPage();
});

//****************************************************************
// Funcion		:: 	fn_Limpiar
// Descripción	::	Limpiar
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_Limpiar() { 
	bFirstClick=false;
    $("#txtCuCliente").val('');
    $('#txtRazonsocial').val('');  
    $("#cmbEjecutivo option").eq("0").attr("selected", "selected");  

    $("#jqGrid_lista_A").GridUnload();
    fn_cargaGrilla();

}

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************

var idsOfSelectedRows = [];
function fn_cargaGrilla() {
    
    var updateIdsOfSelectedRows = function (id, isSelected) {
    $("#hddRowId").val(id);
    var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
    var index = $.inArray(id, idsOfSelectedRows);
    	if (!isSelected && index >= 0) {            
    	idsOfSelectedRows.splice(index, 1);       
    	} else if (index < 0) {
       idsOfSelectedRows.push(rowData.CodigoCotizacion);
    	}
    };
    
    // Inicio IBK - AAE - 19/10/2012 - Se agrega moneda
    $("#jqGrid_lista_A") .jqGrid({
        datatype: function() {
            fn_ListarPipeline();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "CodigoCotizacion"
        },
         //comento ´código original
        /*colNames: ['Nº Cotización', 'CU Cliente', 'Razón Social o Nombre', 'Importe', 'Spread', 'Estado', 'Ejecutivo Banca', 'Ejecutivo Leasing',''],
        colModel: [
		            { name: 'CodigoCotizacion', index: 'CodigoCotizacion', width: 30, align: "center" },
		            { name: 'codUnico', index: 'codUnico', width: 30, align: "center" },
		            { name: 'RazonSocialNombre', index: 'RazonSocialNombre', width: 80, align: "left" },
		            { name: 'Importe', index: 'Importe', width: 30, align: "right" , formatter: Fn_util_ReturnValidDecimal2},
		            { name: 'Spread', index: 'Spread', width: 30, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		            { name: 'Estado', index: 'Estado', width: 30, align: "center" },
		            { name: 'EjecutivoBanca', index: 'EjecutivoBanca', width: 50, align: "left" },
		            { name: 'EjecutivoLeasing', index: 'EjecutivoLeasing', width: 30, align: "left" },
                    { name: 'CodigoEstadoCotizacion', index: 'CodigoEstadoCotizacion', width: 30, align: "left",hidden:true }   
	                ],*/
	      colNames: ['Nº Cotización', 'CU Cliente', 'Razón Social o Nombre', 'Moneda','Importe', 'Spread', 'Estado', 'Banca', 'Ejecutivo Banca', 'Ejecutivo Leasing',''],
        colModel: [
		            { name: 'CodigoCotizacion', index: 'CodigoCotizacion', width: 30, align: "center" },
		            { name: 'codUnico', index: 'codUnico', width: 30, align: "center" },
		            { name: 'RazonSocialNombre', index: 'RazonSocialNombre', width: 55, align: "left" },
		            { name: 'codMoneda', index: 'codMoneda', width: 30, align: "center" },
		            { name: 'RiesgoNeto', index: 'RiesgoNeto', width: 25, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		            { name: 'Spread', index: 'Spread', width: 15, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		            { name: 'Estado', index: 'Estado', width: 20, align: "center" },
		            { name: 'Banca', index: 'Banca', width: 30, align: "center" },
		            { name: 'EjecutivoBanca', index: 'EjecutivoBanca', width: 45, align: "left" },
		            { name: 'EjecutivoLeasing', index: 'EjecutivoLeasing', width: 30, align: "left" },
                    { name: 'CodigoEstadoCotizacion', index: 'CodigoEstadoCotizacion', width: 30, align: "left",hidden:true }   
	                ],	          
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'CodigoCotizacion',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        multiselect:true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: updateIdsOfSelectedRows,
        onSelectAll: function (aRowids, isSelected) {
        	var i, count, id;         
        	  for (i = 0, count = aRowids.length; i < count; i++) {
        	  	  id = aRowids[i];             
        	  	updateIdsOfSelectedRows(id, isSelected);
        	  }
        },     
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            window.location = "frmPipelineRegistro.aspx?CodCotizacion=" + rowData.CodigoCotizacion + "&CodEstado=" + rowData.CodigoEstadoCotizacion;
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    // Fin IBK
    	
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 70);
    $("#search_jqGrid_lista_A").hide();

}


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
    //Valida Tipo de Datos
    $("#txtCuCliente").validText({ type: 'number', length: 10 });
    $('#txtRazonsocial').validText({ type: 'comment', length: 100 });
}

/****************************************************************
Funcion		:: 	fn_ListarPipeline
Descripción	::	Listar
Log			:: 	AEP- 27/08/2012
**************************************************************** */
function fn_ListarPipeline() {
    
    if (!bFirstClick) {
        return;
    }

    parent.fn_blockUI();
    //Inicio IBK - AAE - Agrego parámetros de búsqueda
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"),
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"),
                         "pCUCliente", $("#txtCuCliente").val(),
                         "pRazonSocialCli", $("#txtRazonsocial").val(),
                         "pCodEjecutivo", $("#cmbEjecutivo").val(),
                         "pCodBanca", $("#cmbBanca").val(),
                         "pCodEstado", $("#cmbEstado").val()
                         ];
    //Fin IBK - AAE
    
    fn_util_AjaxWM("frmPipelineListado.aspx/ListarPipeline",
                   arrParametros,
                   function(jsondata) {
                       jqGrid_lista_A.addJSONData(jsondata);
                   	    for (var i = 0, count = idsOfSelectedRows.length; i < count; i++) {
        		$("#jqGrid_lista_A").jqGrid('setSelection', idsOfSelectedRows[i], false);
        		}
                       parent.fn_unBlockUI();
                       fn_doResize();
                   },
                   function(resultado) {
                       parent.fn_unBlockUI();
                       var error = eval("(" + resultado.responseText + ")");
                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
                   }
    );
}


/*****************************************************************
Funcion		:: 	fn_Buscar
Descripción	::	Listado
Log			:: 	KCC - 16/05/2012
***************************************************************** */
function fn_Buscar(bSearch) {
  
    bFirstClick = bSearch;
    fn_ListarPipeline();
}

/*****************************************************************
Funcion		:: 	fn_Editar
Descripción	::	Abrir para Editar
Log			:: 	AEP - 29/08/2012
***************************************************************** */
function fn_Editar() {
       var id = $("#hddRowId").val();
	
	if(IsNullOrEmpty(id)) 
	{
	parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "EDITAR PIPELINE");	
	}else 
	{
		var vElementosAEditar = $("#jqGrid_lista_A").getGridParam('selarrrow');
		if (vElementosAEditar.length > 1) {
            parent.fn_mdl_mensajeIco("Seleccione un sólo registro.", "util/images/warning.gif", "EDITAR PIPELINE");
        } else {
           if(vElementosAEditar != "")
			{
				var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', vElementosAEditar[0]);
        	    window.location = "frmPipelineRegistro.aspx?CodCotizacion=" + id + "&CodEstado=" + rowData.CodigoEstadoCotizacion;
			}else{
			parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "EDITAR PIPELINE");
            }
        }
	}
}

/*****************************************************************
Funcion		:: 	fn_Actualizar
Descripción	::	Regresa los valores a su version inicial
Log			:: 	AEP - 29/08/2012
***************************************************************** */
function fn_Actualizar() {
    
    var id = $("#hddRowId").val();
	
	if(IsNullOrEmpty(id)) 
	{
	parent.fn_mdl_mensajeIco("Seleccione un registro para poder actualizar.", "util/images/warning.gif", "ACTUALIZAR PIPELINE");	
	}else 
	{
		var vElementosAEditar = $("#jqGrid_lista_A").getGridParam('selarrrow');
		if (vElementosAEditar.length > 1) {
            parent.fn_mdl_mensajeIco("Seleccione un sólo registro.", "util/images/warning.gif", "ACTUALIZAR PIPELINE");
        } else {
            
            if(vElementosAEditar != "")
			{
				var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', vElementosAEditar[0]);
            	
            	fn_mdl_confirma('¿Está seguro de actualizar?',
    			function() {

    				parent.fn_blockUI();

    				
    				var arrParametros = ["strCodigoCotizacion", id];

    				fn_util_AjaxWM("frmPipelineListado.aspx/EliminarPipeline",
    					arrParametros,
    					fn_ListarPipeline,
    					function(resultado) {
    						var error = eval("(" + resultado.responseText + ")");
    						parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR ACTUALIZAR");
    					});
    			},
    			"../util/images/question.gif",
    			function() {
    			},
    			'Pipeline'
    		);	
			}else{
			parent.fn_mdl_mensajeIco("Seleccione un registro para poder actualizar.", "util/images/warning.gif", "ACTUALIZAR PIPELINE");
            }
        }
	}
}

/*****************************************************************
Funcion		:: 	fn_Reporte
Descripción	::	Genera Reporte
Log			:: 	AEP - 29/08/2012
***************************************************************** */

function fn_Reporte() {
	var vElementosAEditar = idsOfSelectedRows;
	var count = vElementosAEditar.length;
	if(IsNullOrEmpty(count)) 
	{
	parent.fn_mdl_mensajeIco("Seleccione al menos un registro para poder generar el reporte.", "util/images/warning.gif", "ACTUALIZAR PIPELINE");	
	}else 
	{
	    
		var strCodigo = '';
		 // Inicio IBK - AAE
            /*if(vElementosAEditar != "")
			{*/
			   
			    $("#HddSelected").val("");
			    var strSelected = "";
			    for (var j = 0; j < vElementosAEditar.length; j++) {
			        strSelected = strSelected + vElementosAEditar[j] + '|';
			    }
			    $("#HddSelected").val(strSelected);
			    $("#btnGenerar").click();
            	/*for (var j = 0; j < vElementosAEditar.length; j++) {
                    strCodigo = strCodigo + vElementosAEditar[j] + '|';
            	}
	
            	window.location = "../Reportes/frmRepPipeline.aspx?strcodigo=" + strCodigo;*/
  	
			/*}else{
			parent.fn_mdl_mensajeIco("Seleccione un registro para poder actualizar.", "util/images/warning.gif", "ACTUALIZAR PIPELINE");
            }*/
            // Fin IBK
	}
}