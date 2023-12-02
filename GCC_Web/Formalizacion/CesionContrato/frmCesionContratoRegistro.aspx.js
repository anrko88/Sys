//****************************************************************
// Variables Globales
//****************************************************************
var strComboVacio = "<option value='0'>[-Seleccione-]</option>"

var C_FORMALIZACION_CESIONCONTRATO = "010";

var C_EstadoContrato_Cancelado = "13";
var C_EstadoContrato_Cerrado = "14";
var C_EstadoContrato_Descargado = "16";

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
    fn_cargaGrillaRepresentantes();
    
    //Valores
    strEstadoContrato = fn_util_trim($("#hddCodEstadoContrato").val());
    if(strEstadoContrato == C_EstadoContrato_Cancelado || strEstadoContrato == C_EstadoContrato_Cerrado || strEstadoContrato == C_EstadoContrato_Descargado){		
		$("#hddVer").val("1");
		$("#dv_RealizarCesion").hide();
    }else{
		$("#hddVer").val("0");
    }
        
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
            fn_listaCesionarios();
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
        colNames: ['','','','','','','','_','Item','Cod. Unico','Denominación o Razón Social','Tipo Doc.','Nº Doc.','Departamento','Provincia','Distrito','Cant. Representantes'],
        colModel: [		
				{ name: 'CodCesionario',		index: 'CodCesionario',			hidden: true},		
				{ name: 'CodigoTipoDocumento',	index: 'CodigoTipoDocumento',	hidden: true},
				{ name: 'CodDepartamento',		index: 'CodDepartamento',		hidden: true},
				{ name: 'CodProvincia',			index: 'CodProvincia',			hidden: true},
				{ name: 'CodDistrito',			index: 'CodDistrito',			hidden: true},
				{ name: 'Direccion',			index: 'Direccion',				hidden: true},
				{ name: 'CodSolicitudCredito',	index: 'CodSolicitudCredito',	hidden: true},				
                { name: '_',					index: '_',						width:25,		 align: "center", formatter: fn_checkCesionario },
                { name: 'Id',					index: 'Id',					sortable: true,  sorttype: "int",	 align: "center", defaultValue: "", width:35, },                
                { name: 'CodUnico',				index: 'CodUnico',				sortable: true,  sorttype: "string", align: "center",   defaultValue: "" },
                { name: 'RazonSocial',			index: 'RazonSocial',			sortable: true,  sorttype: "int",	 align: "left", defaultValue: "" },
                { name: 'NombreTipoDocumento',	index: 'NombreTipoDocumento',	sortable: true,  sorttype: "string", align: "center",   defaultValue: "" },
                { name: 'NroDocumento',			index: 'NroDocumento',			sortable: true,  sorttype: "string", align: "left",   defaultValue: "" },
                { name: 'NomDepartamento',		index: 'NomDepartamento',		sortable: true,  sorttype: "string", align: "center", defaultValue: ""},
                { name: 'NomProvincia',			index: 'NomProvincia',			sortable: true,  sorttype: "string", align: "center", defaultValue: "" },
                { name: 'NomDistrito',			index: 'NomDistrito',			sortable: true,  sorttype: "string", align: "center",   defaultValue: ""},                
                { name: 'CantRepresentantes',	index: 'CantRepresentantes',	sortable: false, sorttype: "string", align: "center"}
        ],
        width: glb_intWidthPantalla-100,
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 5,
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
            $("#hddCodCesionario").val(rowData.CodCesionario);         
            $("#hddNomCesionario").val(rowData.RazonSocial);    
            $("#hddCodUnicoCesionario").val(rowData.CodUnico);                                    
            fn_validaCheckCesionario(fn_util_trim(rowData.CodCesionario),fn_util_trim(rowData.RazonSocial));            
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

    
    //**************************************************
    // fn_checkCesionario
    //**************************************************
    function fn_checkCesionario(cellvalue, options, rowObject) {   
		var strCodCesionario = fn_util_trim(rowObject.CodCesionario)             
		var strRazonSocial = fn_util_trim(rowObject.RazonSocial)  
		var strCodUnico = fn_util_trim(rowObject.CodUnico)  
        return "<input id='chkCesionario"+strCodCesionario+"' name='chkCesionario' type='radio' runat='server' onclick='javascript:fn_validaCheckCesionario("+strCodCesionario+",\""+strRazonSocial+"\",\""+strCodUnico+"\");'/>";        
    };
    

}

function fn_validaCheckCesionario(strCodCesionario,strRazonSocial, strCodUnico){
	$("input:radio").attr("checked", false);	
	$("#chkCesionario"+strCodCesionario).attr("checked", true);
	$("#hddCodCesionario").val(strCodCesionario);	  
	$("#hddNomCesionario").val(strRazonSocial);   
	$("#hddCodUnicoCesionario").val(strCodUnico);                      
	fn_listadoRepresentantes();
}



//****************************************************************
// Funcion		:: 	fn_listaCesionarios
// Descripción	::	Busca listado de Cesiones por parametros
// Log			:: 	JRC - 04/01/2013
//****************************************************************
function fn_listaCesionarios() {

    try {
        parent.fn_blockUI();

        var hddCodContrato = $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación                             
                             "pstrNroContrato", hddCodContrato
                            ];
			
        fn_util_AjaxWM("frmCesionarioRegistro.aspx/ListaCesionarios",
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
}


//****************************************************************
// Funcion		:: 	fn_abreEditar
// Descripción	::	Abre Editar Registro
// Log			:: 	JRC - 04/01/2013
//****************************************************************
function fn_abreEditar() {
	/*
	var codContrato = $("#hddCodContrato").val();
	if(fn_util_trim(codContrato) == ""){
		parent.fn_mdl_mensajeError("Debe seleccionar un Contrato", function() { }, "VALIDACIÓN");
	}else{
		fn_util_globalRedirect("/Formalizacion/CesionContrato/frmCesionContratoRegistro.aspx?hddCodContrato=" + $("#hddCodContrato").val());	 
	}
	*/	
	fn_util_globalRedirect("/Formalizacion/CesionContrato/frmCesionContratoRegistro.aspx?hddCodContrato=" + $("#hddCodContrato").val());	 
}



//****************************************************************
// Funcion		:: 	fn_abreCesionarios
// Descripción	::	Abre Editar Registro
// Log			:: 	JRC - 02/11/2013
//****************************************************************
function fn_abreCesionarios() {	
	var pstrCodContrato = fn_util_trim($("#hddCodContrato").val());	
	parent.fn_util_AbreModal("Cesión de Contrato :: Registro", "Formalizacion/CesionContrato/frmCesionarioRegistro.aspx?hddCodContrato=" + pstrCodContrato, 900, 560, function() { });	
}




//****************************************************************
// Funcion		:: 	fn_cancelar  
// Descripción	::	Cancela las Operaciones
// Log			:: 	JRC - 02/11/2013
//****************************************************************
function fn_cancelar() {

    parent.fn_mdl_confirma(
                    "¿Está seguro de Volver?",
                    function() {                        
                        fn_util_globalRedirect("/Formalizacion/CesionContrato/frmCesionContratoListado.aspx");
                    },
                    "Util/images/question.gif",
                    function() { },
                    'CONFIRMACION'
                   );
}




//****************************************************************
// Funcion		:: 	fn_abreVerificaDatos
// Descripción	::	Abre Editar Registro
// Log			:: 	JRC - 02/11/2013
//****************************************************************
function fn_abreVerificaDatos() {	
	var pstrCodContrato = fn_util_trim($("#hddCodContrato").val());	
	parent.fn_util_AbreModal("Cesión de Contrato :: Verifica Datos", "Formalizacion/CesionContrato/frmVerificaDatos.aspx?hddCodContrato=" + pstrCodContrato, 850, 350, function() { });	
}





//****************************************************************
// Funcion		:: 	fn_cargaGrillaRepresentantes
// Descripción	::	Carga Grilla Listado de Representantes
// Log			:: 	JRC - 04/01/2013
//****************************************************************
function fn_cargaGrillaRepresentantes() {

     $("#jqGrid_lista_B").jqGrid({
        datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_B", "page");	
            //fn_listadoRepresentantes();
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
        colNames: ['','','','','Nombre Completo','Tipo Doc.','Nº Doc.','Nº Partida','Of. Registral'],
        colModel: [								
				{ name: 'CodCesionario',		index: 'CodCesionario',			hidden: true},		
				{ name: 'CodRepresentante',		index: 'CodRepresentante',		hidden: true},		
				{ name: 'CodigoTipoDocumento',	index: 'CodigoTipoDocumento',	hidden: true},	
				{ name: 'CodSolicitudCredito',	index: 'CodSolicitudCredito',	hidden: true},	
                { name: 'NomRepresentante',		index: 'NomRepresentante',		sortable: true, sorttype: "string",	align: "left", defaultValue: "" },
                { name: 'NombreTipoDocumento',	index: 'NombreTipoDocumento',	sortable: true, sorttype: "string",	align: "left", defaultValue: "" },
                { name: 'NroDocumento',			index: 'NroDocumento',			sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'PartidaRegistral',		index: 'PartidaRegistral',		sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'OficinaRegistral',		index: 'OficinaRegistral',		sortable: true, sorttype: "string", align: "center", defaultValue: ""}
        ],
        width: glb_intWidthPantalla-100,
        height: '100%',
        pager: '#jqGrid_pager_B',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 5,
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
            var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
            $("#hddCodContrato").val(rowData.CodSolicitudCredito);
            $("#hddCodCesionario").val(rowData.CodCesionario);
            $("#hddCodRepresentante").val(rowData.CodRepresentante);            
        }
    });
    jQuery("#jqGrid_lista_B").jqGrid('navGrid', '#jqGrid_pager_B', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_B").hide();

}




//****************************************************************
// Funcion		:: 	fn_listadoRepresentantes
// Descripción	::	Busca listado de Representantes
// Log			:: 	JRC - 04/01/2013
//****************************************************************
function fn_listadoRepresentantes() {
    try {
        parent.fn_blockUI();

        var hddCodContrato = $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();
        var hddCodCesionario = $('#hddCodCesionario').val() == undefined ? "" : $('#hddCodCesionario').val();
        
        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_B", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_B", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_B", "sortorder"), // Criterio de ordenación                             
                             "pstrCodContrato", hddCodContrato,
                             "pstrCodCesionario", hddCodCesionario
                            ];

        fn_util_AjaxWM("frmRepresentanteRegistro.aspx/ListaRepresentates",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_B.addJSONData(jsondata);
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



//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_realizaCesion
// Descripción	::	Realiza Cesión
// Log			:: 	JRC - 18/01/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_realizaCesion() {	
		
	var hddCodContrato = $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();
	var hddCodCesionario = $('#hddCodCesionario').val() == undefined ? "" : $('#hddCodCesionario').val();
	var hddCodCesionarioPri = $('#hddCodCesionarioPri').val() == undefined ? "" : $('#hddCodCesionarioPri').val();
	var hddCodUnicoCesionario = $('#hddCodUnicoCesionario').val() == undefined ? "" : $('#hddCodUnicoCesionario').val();
	
	if(fn_util_trim(hddCodCesionario)==""){		
		parent.fn_mdl_mensajeError("Debe seleccionar un Cesionario", function() { }, "VALIDACIÓN");		
	}
	
	else if(fn_util_trim(hddCodCesionarioPri) == fn_util_trim(hddCodCesionario)) {
		parent.fn_mdl_mensajeError("Debe elegir un Cesionario distinto", function() { }, "VALIDACIÓN");		
	}
	
	else if(fn_util_trim(hddCodUnicoCesionario) == fn_util_trim(hddCodUnicoCesionario)) {
		parent.fn_mdl_mensajeError("El Cesionario elegido no cuenta con Cod. Unico", function() { }, "VALIDACIÓN");		
	}
		
	else{			        
				        
	    parent.fn_mdl_confirma(
                    "¿Está seguro que desea asignar el Cesionario seleccionado al Contrato?",
                    function() {                        
                        parent.fn_blockUI();
                        
                        
						var arrParametros = ["hddCodContrato", hddCodContrato,
											 "hddCodCesionario", hddCodCesionario
											];
				                            
						fn_util_AjaxWM("frmCesionContratoRegistro.aspx/RealizaCesion",
								arrParametros,
								function(resultado) {
								
									parent.fn_unBlockUI();
									
									var pstrResultado = fn_util_trim(resultado);
									var arrResultado = pstrResultado.split("|")
									
									if(fn_util_trim(arrResultado[0]) == "1"){
										//alert("Ya ta!!!"+$("#txtCUCliente").val()+$("#txtRazonSocial").val()+" a "+$("#hddNomCesionario").val());
										parent.fn_mdl_mensajeOk("Se realizo el cambio de cliente '"+$("#txtRazonSocial").val()+"' a '"+$("#hddNomCesionario").val()+"'", function() { fn_util_globalRedirect("/Formalizacion/CesionContrato/frmCesionContratoListado.aspx"); }, "CESIÓN REALIZADA");
									}else{
										parent.fn_mdl_mensajeIco(arrResultado[1], "util/images/error.gif", "ERROR AL REALIZAR CESIÓN");	
									}
									
								},
								function(request) {
									parent.fn_unBlockUI();
									fn_util_alert(jQuery.parseJSON(request.responseText).Message);
								}
						);                        
                        
                        
                    },
                    "Util/images/question.gif",
                    function() { },
                    'CONFIRMACION'
                   );
	
	}
	
	
	
}



//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos() {	
	var pstrCodContrato = fn_util_trim($("#hddCodContrato").val());
	var pstrCodBien = "0";
	var pstrCodRelacionado = "0";
	var pstrCodTipo = C_FORMALIZACION_CESIONCONTRATO;
	var pstrVer = fn_util_trim($("#hddVer").val());
	parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + "&hddVer=" + pstrVer, 800, 350, function() { });	
}



