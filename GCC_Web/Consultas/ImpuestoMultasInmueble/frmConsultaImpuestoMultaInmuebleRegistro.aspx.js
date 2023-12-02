//****************************************************************
// Variables Globales
//****************************************************************
var strComboVacio = "<option value='0'>[-Seleccione-]</option>";

var C_TX_NUEVO = "NUEVO";
var C_TX_EDITAR = "EDITAR";

var blnPrimeraBusqueda = false;
var intPaginaActual = 1;

var strSecImpuesto = '';
var strTieneImpuesto = 'N';

var C_GESTIONBIEN_IMPMUNICIPAL = "003";     

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	??? - 02/11/2012
//****************************************************************
$(document).ready(function() {
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    //Valida Campos
    fn_inicializaCampos();

	//CargaGrillas
	fn_configuraGrillaBienes();		
	
	//Busca con Enter
    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscarListarBienes(true);
        }
    });
    	
   //Oculta ver 	
   //$("#dv_ver").hide(); 	
    	
	//Abre AutoEditar
	var strAbreEditarAuto = $("#hddAbreEditarAuto").val();
	if( fn_util_trim(strAbreEditarAuto) == "S" ){
		strTieneImpuesto = "X";		
		fn_editarImpuestoAuto();
		fn_buscarListaBieneEditar();
		//fn_buscarImpuestos();
		$("#dv_busquedaBienes").hide();
		$("#dv_separador").hide();
		$("#dv_btnBuscar").hide();
		$("#dv_btnGeneraLote").hide();
		$("#dv_AgregarImpuesto").hide();		
	}else{
		$("#hddCodContrato").val("");
		$("#hddCodBien").val("");
		$("#hddCodImpuesto").val("");
		$("#dv_ver").hide();
	}
	
	//Carga Grilla Impuestos
	fn_configuraGrillaImpuesto();
	
    //On load Page (siempre al final)
    fn_onLoadPage();
    
});


//****************************************************************
// Función		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
	
	$('#txtPeriodo').validText({ type: 'number', length: 4 });	
	$('#txtUbicacion').validText({ type: 'comment', length: 100 });	
	
	if( fn_util_trim($("#hddTipoTx").val()) == C_TX_NUEVO ) {
		$("#txtTotalAutovaluo").validNumber({ value: '0' });
		$("#txtTotalPredial").validNumber({ value: '0' });
	}else{
		$("#txtTotalAutovaluo").validNumber({ value: $("#txtTotalAutovaluo").val() });
		$("#txtTotalPredial").validNumber({ value: $("#txtTotalAutovaluo").val() });
	}
	
}


//****************************************************************
// Función		:: 	fn_configuraGrilla
// Descripción	::	Inicializa Grilla Bienes
// Log			:: 	JRC - 25/11/2012
//****************************************************************
function fn_configuraGrillaBienes() {

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_realizaBusquedaBienes();
        },
        jsonReader: {                 // Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage",      // Número de página actual.
            total: "PageCount",       // Número total de páginas.
            records: "RecordCount",   // Total de registros a mostrar.
            repeatitems: false,
            id: "id" // Índice de la columna con la clave primaria.
        },
        colNames: ['','','Nº Contrato', 'CU Cliente', 'Tipo Documento', 'Nº Documento', 'Razón Social o Nombre', 'Departamento', 'Provincia', 'Distrito', 'Ubicación',''],
        colModel: [
			{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
			{ name: 'FechaTransferencia', index: 'FechaTransferencia', hidden: true },			
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', sortable: true, width: 25, sorttype: "string", align: "center" },
		    { name: 'CodUnico', index: 'CodUnico', sortable: true, width: 25, sorttype: "string", align: "center" },
		    { name: 'DesCodigoTipoDocumento', index: 'DesCodigoTipoDocumento', sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'NumeroDocumento', index: 'NumeroDocumento', sortable: true, sorttype: "string", width: 25, align: "left" },
		    { name: 'ClienteRazonSocial', index: 'ClienteRazonSocial', sortable: true, sorttype: "left", width: 25, align: "center" },
		    { name: 'DesDepartamento', index: 'DesDepartamento', sortable: true, width: 25, sorttype: "string", align: "center" },
		    { name: 'DesProvincia', index: 'DesProvincia', sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'DesDistrito', index: 'DesDistrito', sortable: false, width: 25, align: "center", sorttype: "string" },
		    { name: 'Ubicacion', index: 'Ubicacion', sortable: false, width: 25, align: "center", sorttype: "string" },
		    { name: '', index: '', width: 5, formatter: fn_fechaTransferencia }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 5,                      // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito', // Columna a ordenar por defecto.
        sortorder: 'desc',               // Criterio de ordenación por defecto.
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
            $("#hddCodUnico").val(rowData.CodUnico);
        }
    });    
    $("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    
    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 85);
    
    //**************************************************
    // fn_fechaTransferencia
    //**************************************************
    function fn_fechaTransferencia(cellvalue, options, rowObject) {  
		$("#hddFechaTransferencia").val(rowObject.FechaTransferencia);		
		return "&nbsp;";
    };
    
}


//****************************************************************
// Funcion		:: 	fn_buscarListarBienes
// Descripción	::	Busca listado de Bienes
// Log			:: 	JRC - 25/11/2012
//****************************************************************
function fn_buscarListarBienes(pblnBusqueda) {
	$("#hddCodContrato").val("");
	$("#hddCodBien").val("");
	$("#hddCodImpuesto").val("");
	strTieneImpuesto = "N";
	
    blnPrimeraBusqueda = pblnBusqueda;
	intPaginaActual = 1;
    fn_realizaBusquedaBienes();
}
function fn_realizaBusquedaBienes() {

    if (!blnPrimeraBusqueda) {
        return;
    }

    try {
        parent.fn_blockUI();

        var cmbDepartamento = $('#cmbDepartamento').val() == undefined ? "" : $('#cmbDepartamento').val();
        var cmbProvincia = $('#cmbProvincia').val() == undefined ? "" : $('#cmbProvincia').val();
        var cmbDistrito = $('#cmbDistrito').val() == undefined ? "" : $('#cmbDistrito').val();
        var txtUbicacion = $('#txtUbicacion').val() == undefined ? "" : $('#txtUbicacion').val();
        
        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación                             
                             "pstrDepartamento", cmbDepartamento,
                             "pstrProvincia", cmbProvincia,
                             "pstrDistrito", cmbDistrito,
                             "pstrUbicacion", txtUbicacion
                            ];
                            
        fn_util_AjaxSyncWM("frmConsultaImpuestoMultaInmuebleRegistro.aspx/ListaImpuestoMunicipalBienes",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);
                    
                    //LImpia data
                    $("#hddCodContrato").val("");
					$("#hddCodBien").val("");
					$("#hddCodImpuesto").val("");
					$("#hddAbreEditarAuto").val("N");
															
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
// Función		:: 	fn_configuraGrillaImpuesto
// Descripción	::	Inicializa Grilla Impuestos
// Log			:: 	JRC - 25/11/2012
//****************************************************************
var idsOfSelectedRows = [];
function fn_configuraGrillaImpuesto() {
	strSecImpuesto = '';
	$("#hddRowIdImpuesto").val("");
	$("#hddCodigosImpuestos").val("");
	
	var updateIdsOfSelectedRows = function (id, isSelected) {
	//debugger;		
    	
    	var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id); 
		
    		if(isSelected) {    	    		
    			strSecImpuesto = strSecImpuesto + rowData.SecImpuesto + ",";	
    		}else {    		
    			var lblChekedResult3 = strSecImpuesto.split(",");            
    			var pstrSecImpuesto = "";    		
				for (var i = 0; i < lblChekedResult3.length; i++) {
					if (rowData.SecImpuesto != lblChekedResult3[i]) {
						if (lblChekedResult3[i] != "") {                        
                    		pstrSecImpuesto += lblChekedResult3[i] + ",";
						}
					}            	
				}    		
    			strSecImpuesto = pstrSecImpuesto;
			}

			$("#hddRowIdImpuesto").val(id);
    		$("#hddCodigosImpuestos").val(strSecImpuesto);
	    	
    		//Para Paginación (NO TOCAR) hddCodImpuesto
			var index = $.inArray(id, idsOfSelectedRows);
			if (!isSelected && index >= 0) {            
				idsOfSelectedRows.splice(index, 1);       
			} else if (index < 0) {
				idsOfSelectedRows.push(rowData.SecImpuesto);
			}
    	
    };
	

    $("#jqGrid_lista_B").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_B", "page");
            fn_buscarImpuestos();
        },
        jsonReader: {                 // Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage",      // Número de página actual.
            total: "PageCount",       // Número total de páginas.
            records: "RecordCount",   // Total de registros a mostrar.
            repeatitems: false,
            id: "SecImpuesto" // Índice de la columna con la clave primaria.
        },
        colNames: ['','','','Nro Contrato','Ubicación','Razón Social o Nombre','Periodo','Cod. Predio','Autovaluo','Impuesto Predial','Arbitrio','Multa','Fiscalización','Importe Total','F. Pago','Estado Pago','F. Cobro','Estado Cobro','Lote','Cheque','Observación','Docs.',''],
        colModel: [
			{ name: 'SecImpuesto',			index: 'SecImpuesto',			hidden: true },
			{ name: 'SecFinanciamiento',	index: 'SecFinanciamiento',		hidden: true },
			{ name: 'EstadoPago',			index: 'EstadoPago',			hidden: true },
		    { name: 'CodSolicitudCredito',	index: 'CodSolicitudCredito',	sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'Ubicacion',			index: 'Ubicacion',				sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'ClienteRazonSocial',	index: 'ClienteRazonSocial',	sortable: true, sorttype: "string", width: 25, align: "left" },
		    { name: 'Periodo',				index: 'Periodo',				sortable: true, sorttype: "string", width: 25, align: "left" },
		    { name: 'CodPredio',			index: 'CodPredio',				sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'Autovaluo',			index: 'Autovaluo',				sortable: true, sorttype: "string", width: 25, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'ImpuestoPredial',		index: 'ImpuestoPredial',		sortable: true, sorttype: "string", width: 25, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'Arbitrio',				index: 'Arbitrio',				sortable: true,	sorttype: "right",	width: 25, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'Multa',				index: 'Multa',					sortable: true, sorttype: "string", width: 25, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'Fiscalizacion',		index: 'Fiscalizacion',			sortable: true, sorttype: "string", width: 25, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'Total',				index: 'Total',					sortable: true, sorttype: "string", width: 25, align: "right", formatter: Fn_util_ReturnValidDecimal2 },		    
		    { name: 'FecPago',				index: 'FecPago',				sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'DesEstadoPago',		index: 'DesEstadoPago',			sortable: true, sorttype: "string", width: 25, align: "center" },		    
		    { name: 'FechaCobro',			index: 'FechaCobro',			sortable: true,	sorttype: "string", width: 25, align: "center" },		     
		    { name: 'DesEstadoCobro',		index: 'DesEstadoCobro',		sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'NroLote',				index: 'NroLote',				sortable: true,	sorttype: "string",	width: 25, align: "center" },
		    { name: 'NroCheque',			index: 'NroCheque',				sortable: true,	sorttype: "string",	width: 25, align: "center" },
		    { name: 'Observaciones',		index: 'Observaciones',			sortable: true,	sorttype: "string",	width: 25, align: "left" },
            { name: 'Doc',					index: 'Doc',					align: "center", sortable: false, formatter: fn_abreDocumentos, width: 20 },
            { name: '',						index: '', width: 10}
	    ],
        height: '100%',
        pager: '#jqGrid_pager_B',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 5,
        rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito',
        sortorder: 'desc',
        viewrecords: true,
        //gridview: true,
        autowidth: true,
        altRows: true,
        multiselect:false,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: updateIdsOfSelectedRows,
        onSelectAll: function (aRowids, isSelected) {
        	var i, count, id;        	
        	for (i = 0, count = aRowids.length; i < count; i++) {
        		id = aRowids[i];             
        	  	updateIdsOfSelectedRows(id, isSelected);
        	}
        }
        /*,afterInsertRow : function(id) { 	
			var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
    		var strEstadoPago = rowData.EstadoPago;
			if( fn_util_trim(strEstadoPago) == "002" ){
				//alert(id+"|"+$("#jqGrid_lista_B")[0]);
				//$("#"+id+" td.jqGrid_lista_B_cb ",$("#jqGrid_lista_B")[0]).html('aaa').removeProp('class');				
				$("#jqg_jqGrid_lista_B_"+id).attr('disabled', 'disabled');
			}							    			
		}*/
    });
    $("#jqGrid_lista_B").jqGrid('navGrid', '#jqGrid_pager_B', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_B").hide();

    $("#jqGrid_lista_B").setGridWidth($(window).width() - 85);
    
    //**************************************************
    // Documentos
    //**************************************************
    function fn_abreDocumentos(cellvalue, options, rowObject) {
		//return ".";
        return "<img src='../../Util/images/ico_docs.gif' alt='Ver Documentos' title='Ver Documentos' width='18px' onclick=\"javascript:fn_GBAbreDocumentos(\'"+rowObject.CodSolicitudCredito+"\',\'"+rowObject.SecFinanciamiento+"\',\'"+rowObject.SecImpuesto+"\',\'"+C_GESTIONBIEN_IMPMUNICIPAL+"\');\" style='cursor:pointer;' />";
    };
    
}



//****************************************************************
// Funcion		:: 	fn_buscarImpuestoMuni
// Descripción	::	Busca listado de cotizacion por parametros
// Log			:: 	JRC - 07/11/2012
//****************************************************************
function fn_buscarImpuestos() {

	//Limpia datos	
	//$("#hddCodigosImpuestos").val("");		
	//$("#hddRowIdImpuesto").val("");	
	//strSecImpuesto = "";
	$("#jqGrid_lista_B").jqGrid('resetSelection');
	$("#jqGrid_lista_A").jqGrid('resetSelection');
	
	var strAbreEditarAuto = $("#hddAbreEditarAuto").val();
	if( fn_util_trim(strAbreEditarAuto) != "S" ){		
		$("#hddCodContrato").val("");
		$("#hddCodBien").val("");
		$("#hddCodImpuesto").val("");		
	}
	
					
    try {
        parent.fn_blockUI();

        var hddCodContrato	= $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();
        var hddCodBien		= $('#hddCodBien').val() == undefined ? "" : $('#hddCodBien').val();
        var hddTieneLote	= strTieneImpuesto;

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_B", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_B", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_B", "sortorder"), // Criterio de ordenación                             
                             "pstrCodContrato", hddCodContrato,
                             "pstrCodBien", hddCodBien,
                             "pstrTieneLote", hddTieneLote
                            ];
        
        //alert(arrParametros);                   
        fn_util_AjaxSyncWM("frmConsultaImpuestoMultaInmuebleRegistro.aspx/ListaImpuestoMunicipal",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_B.addJSONData(jsondata);    
                    for (var i = 0, count = idsOfSelectedRows.length; i < count; i++) {
        				$("#jqGrid_lista_B").jqGrid('setSelection', idsOfSelectedRows[i], false);
        			}               								
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
    fn_LimpiaComboDistrito();
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
// Funcion		:: 	fn_agregarImpuesto  
// Descripción	::	Agrega Impuesto
// Log			:: 	JRC - 05/11/2012
//****************************************************************
function fn_agregarImpuesto() {

    var hddCodContrato = $("#hddCodContrato").val();
    var hddCodBien = $("#hddCodBien").val();
    var hddCodUnico = $("#hddCodUnico").val();
    var txtTotalAutovaluo = $("#txtTotalAutovaluo").val();
    var txtTotalPredial = $("#txtTotalPredial").val();
    var txtPeriodo = $("#txtPeriodo").val();

    var strError = "";

    if (fn_util_trim(hddCodBien) == "") {
        parent.fn_mdl_mensajeError("Debe seleccionar un Bien", function() { }, "VALIDACIÓN");
    } else {

        //		if( fn_util_trim(txtPeriodo) == "" ){
        //			strError = strError + "&nbsp;&nbsp;- Debe ingresar el Periodo <br/>"
        //		}
        //		if( fn_util_trim(txtTotalAutovaluo) == "" || fn_util_trim(txtTotalAutovaluo) == "0.00" ){
        //			strError = strError + "&nbsp;&nbsp;- Debe ingresar el Total de Autovaluo <br/>"			
        //		}
        //		if( fn_util_trim(txtTotalPredial) == "" || fn_util_trim(txtTotalPredial) == "0.00" ){
        //			strError = strError + "&nbsp;&nbsp;- Debe ingresar el Total Predial <br/>"			
        //		}	
        //		
        //		
        //		if( fn_util_trim(strError) != "" ){
        //			parent.fn_mdl_mensajeError(strError, function() { }, "VALIDACIÓN");										
        //		}else{
        parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Agregar", "Consultas/ImpuestoMultasInmueble/frmConsultaImpuestoMultaInmuebleMnt.aspx?hddCodContrato=" + hddCodContrato + "&hddCodBien=" + hddCodBien + "&hddCodUnico=" + hddCodUnico + "&txtTotalAutovaluo=" + txtTotalAutovaluo + "&txtTotalPredial=" + txtTotalPredial + "&txtPeriodo=" + txtPeriodo, 950, 550, function() { });
        //		}		

    }

}

//****************************************************************
// Funcion		:: 	fn_editarImpuesto  
// Descripción	::	Edita Impuesto
// Log			:: 	JRC - 05/11/2012
//****************************************************************
function fn_editarImpuesto() {

    var id = $("#hddRowIdImpuesto").val();

    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "EDITAR IMPUESTO");
    } else {
        var vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow');
        if (vElementosAEditar.length > 1) {
            parent.fn_mdl_mensajeIco("Seleccione un sólo registro.", "util/images/warning.gif", "EDITAR IMPUESTO");
        } else {
            if (vElementosAEditar != "") {

                var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', vElementosAEditar);
                var strEstadoPago = rowData.EstadoPago;

                if (fn_util_trim(strEstadoPago) == "002") {
                    parent.fn_mdl_mensajeIco("El impuesto no puede ser editado por su estado de pago", "util/images/warning.gif", "EDITAR IMPUESTO");
                } else {
                    var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', vElementosAEditar);
                    var strCodContrato = rowData.CodSolicitudCredito;
                    var strCodBien = rowData.SecFinanciamiento;
                    var strCodImpuesto = rowData.SecImpuesto;
                    parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Editar", "Consultas/ImpuestoMultasInmueble/frmConsultaImpuestoMultaInmuebleMnt.aspx?hddCodContrato=" + strCodContrato + "&hddCodBien=" + strCodBien + "&hddCodImpuesto=" + strCodImpuesto, 950, 550, function() { });
                }

            } else {
                parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "EDITAR IMPUESTO");
            }
        }
    }

}

//****************************************************************
// Funcion		:: 	fn_editarImpuestoAuto
// Descripción	::	Edita Impuesto
// Log			:: 	JRC - 05/11/2012
//****************************************************************
function fn_editarImpuestoAuto() {
	
	var strCodContrato	= $("#hddCodContrato").val();
	var strCodBien		= $("#hddCodBien").val();
	var strCodImpuesto	= $("#hddCodImpuesto").val();

	if( fn_util_trim(strCodImpuesto) == ""){
		parent.fn_mdl_mensajeError("Impuesto no encontrado", function() { }, "VALIDACIÓN");
	}else{	
		parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Ver", "Consultas/ImpuestoMultasInmueble/frmConsultaImpuestoMultaInmuebleMnt.aspx?hddCodContrato="+strCodContrato+"&hddCodBien="+strCodBien+"&hddCodImpuesto="+strCodImpuesto, 950, 550, function() { });
	}
	
}

//****************************************************************
// Funcion		:: 	fn_cancelar  
// Descripción	::	Cancela las Operaciones de Cotizacion
// Log			:: 	JRC - 05/11/2012
//****************************************************************
function fn_cancelar() {

    parent.fn_mdl_confirma(
                    "¿Está seguro de Volver?",
                    function() {                        
                        fn_util_globalRedirect("/Consultas/ImpuestoMultasInmueble/frmConsultaImpuestoMultaInmuebleListado.aspx");
                    },
                    "Util/images/question.gif",
                    function() { },
                    'CONFIRMACION'
                   );
                   
}




//****************************************************************
// Funcion		:: 	fn_buscarListaBieneEditar
// Descripción	::	Busca listado de Bienes
// Log			:: 	JRC - 25/11/2012
//****************************************************************
function fn_buscarListaBieneEditar() {
    try {
        parent.fn_blockUI();

        var hddCodBien = $('#hddCodBien').val() == undefined ? "" : $('#hddCodBien').val();
        var hddCodContrato = $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();
        
        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación                             
                             "pstrCodContrato", hddCodContrato,
                             "pstrCodBien", hddCodBien
                            ];
                            
        fn_util_AjaxSyncWM("frmConsultaImpuestoMultaInmuebleRegistro.aspx/ListaImpuestoMunicipalBienEditar",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);                    
                    parent.fn_unBlockUI();
                    
                    //$("#dv_ver").hide();
                    var strFechaTrasferencia = $("#hddFechaTransferencia").val();
                    if(fn_util_trim(strFechaTrasferencia)!= ""){
						$("#dv_eliminar").hide();
						$("#dv_editar").hide();
						//$("#dv_ver").show();
                    }
                    
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
// Funcion		:: 	fn_generaLote
// Descripción	::	Genera Lote
// Log			:: 	JRC - 25/11/2012
//****************************************************************
function fn_generaLote() {

    parent.fn_mdl_confirma(
		"¿Está seguro que desea generar el Lote con los Impuestos seleccionados?",
		function() {


		    var vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow');
		    var count = vElementosAEditar.length;

		    if (IsNullOrEmpty(count)) {
		        parent.fn_mdl_mensajeIco("Seleccione al menos un registro para poder generar el lote.", "util/images/warning.gif", "GENERAR LOTE");
		    } else {
		        if (vElementosAEditar.length < 1) {
		            parent.fn_mdl_mensajeIco("Seleccione al menos un registro para poder generar el lote.", "util/images/warning.gif", "GENERAR LOTE");
		        } else {

		            var strCodigosImpuestos = $("#hddCodigosImpuestos").val() + "0";
		            //alert(strCodigosImpuestos);		

		            var arrParametros = ["pstrCodigosImpuestos", strCodigosImpuestos];
		            fn_util_AjaxSyncWM("frmConsultaImpuestoMultaInmuebleRegistro.aspx/GeneraLote",
							 arrParametros,
							 function(result) {
							     parent.fn_unBlockUI();
							     if (fn_util_trim(result) == "0") {
							         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL GENERAR LOTE");
							     } else {
							         parent.fn_mdl_mensajeOk("El Lote se generó correctamente. Se generó el Lote Nº" + fn_util_trim(result), function() { fn_util_globalRedirect("/GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleListado.aspx"); }, "GRABADO CORRECTO");
							     }
							 },
							 function(resultado) {
							     parent.fn_unBlockUI();
							     var error = eval("(" + resultado.responseText + ")");
							     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABADO");
							 }
						);


		        }
		    }

		},
		"Util/images/question.gif",
		function() { },
		'CONFIRMACION'
	);

}



//****************************************************************
// Funcion		:: 	fn_eliminarImpuesto
// Descripción	::	Busca listado de Bienes
// Log			:: 	JRC - 25/11/2012
//****************************************************************
function fn_eliminarImpuesto() {

    var vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow');
    var count = vElementosAEditar.length;

    if (IsNullOrEmpty(count)) {
        parent.fn_mdl_mensajeIco("Seleccione al menos un registro para eliminar", "util/images/warning.gif", "ELIMINAR IMPUESTO");
    } else {
        if (vElementosAEditar.length < 1) {
            parent.fn_mdl_mensajeIco("Seleccione al menos un registro para eliminar.", "util/images/warning.gif", "ELIMINAR IMPUESTO");
        }
        else if (vElementosAEditar.length > 1) {
            parent.fn_mdl_mensajeIco("Seleccione un sólo registro.", "util/images/warning.gif", "EDITAR IMPUESTO");
        }
        else {

            var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', vElementosAEditar);
            var strEstadoPago = rowData.EstadoPago;

            if (fn_util_trim(strEstadoPago) == "002") {
                parent.fn_mdl_mensajeIco("El impuesto no puede ser eliminado por su estado de pago", "util/images/warning.gif", "EDITAR IMPUESTO");
            } else {


                parent.fn_mdl_confirma(
						"¿Está seguro que desea eliminar los Impuestos seleccionados?",
						function() {


						    var strCodigosImpuestos = $("#hddCodigosImpuestos").val() + "0";
						    //alert(strCodigosImpuestos);		

						    var arrParametros = ["pstrCodigosImpuestos", strCodigosImpuestos];
						    fn_util_AjaxSyncWM("frmConsultaImpuestoMultaInmuebleRegistro.aspx/EliminarImpuestoMunicipal",
								 arrParametros,
								 function(result) {
								     parent.fn_unBlockUI();
								     if (fn_util_trim(result) == "0") {
								         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL ELIMINAR");
								     } else {
								         parent.fn_mdl_mensajeOk("Los registros seleccionados se eliminaron correctamente.", function() { fn_buscarImpuestos(); }, "ELIMINADO CORRECTO");
								     }
								 },
								 function(resultado) {
								     parent.fn_unBlockUI();
								     var error = eval("(" + resultado.responseText + ")");
								     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN ELIMINAR");
								 }
							);



						},
						"Util/images/question.gif",
						function() { },
						'CONFIRMACION'
					);


            }





        }
    }

}


//****************************************************************
// Funcion		:: 	fn_listaBusquedaBien
// Descripción	::	Busca listado de Bienes
// Log			:: 	JRC - 25/11/2012
//****************************************************************
function fn_listaBusquedaBien(){
	fn_buscarListarBienes(true);
	fn_buscarImpuestos();
}


//****************************************************************
// Funcion		:: 	fn_verImpuesto
// Descripción	::	Ver Impuesto
// Log			:: 	JRC - 03/12/2012
//****************************************************************
function fn_verImpuesto() {
	//debugger;
	var id = $("#hddRowIdImpuesto").val();	 
	
	if(IsNullOrEmpty(id)) {
	    parent.fn_mdl_mensajeError("Debe seleccionar un impuesto.", function() { }, "VALIDACIÓN");
	}else{
	parent.fn_util_AbreModal("Impuestos y Multas Inmuebles :: Ver", "Consultas/ImpuestoMultasInmueble/frmConsultaImpuestoMultaInmuebleMnt.aspx?hddCodContrato=" + $("#hddCodContrato").val() + "&hddCodBien=" + $("#hddCodBien").val() + "&hddCodSiniestro=" + $("#hddCodSiniestro").val() + "&hddCodImpuesto=" + $("#hddRowIdImpuesto").val() + "&hddVer=1", 950, 550, function() { });
		
	}
	
}



//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos(pstrCodContrato, pstrCodBien, pstrCodRelacionado, pstrCodTipo) {	
	//debugger
	var strFechaTransferencia = $("#hddFechaTransferencia").val();	
	var strVer = "0";
	if( fn_util_trim(strFechaTransferencia) != "" ){
		strVer = "1";
	}

	parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "Consultas/frmConsultaGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + "&hddVer=" + strVer, 800, 350, function() { });	
}




