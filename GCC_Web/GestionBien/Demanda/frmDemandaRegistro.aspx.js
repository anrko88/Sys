//****************************************************************
// Variables Globales
//****************************************************************
var C_GESTIONBIEN_DEMANDA = "002";

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	??? - 02/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();
        	
    //Carga Grilla
    fn_cargaGrilla();
    
    //Valida FechaTransferencia
    var strFechaTrasferencia = $("#txtFecTransferencia").val();
    if( fn_util_trim(strFechaTrasferencia) == "" ){
		$("#dv_ver").hide();
		$("#hddVer").val("0");
    }else{		
		$("#dv_eliminar").hide();
		$("#dv_editar").hide();
		$("#dv_agregar").hide();
		$("#hddVer").val("1");
    }
        	
    //On load Page (siempre al final)
    fn_onLoadPage();
    
});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {

	//Cabecera
	fn_util_inactivaInput("txtNroContrato", "I");
	fn_util_inactivaInput("txtCUCliente", "I");
	fn_util_inactivaInput("txtRazonSocial", "I");
	fn_util_inactivaInput("txtEstadoContrato", "I");
	fn_util_inactivaInput("txtClasificacionBien", "I");
	fn_util_inactivaInput("txtTipoBien", "I");
	fn_util_inactivaInput("txtMoneda", "I");	
	fn_util_inactivaInput("txtValorBien", "I");
	fn_util_inactivaInput("txtFecTransferencia", "I");
	
	fn_util_inactivaInput("txtDescripcion", "I");
	fn_util_inactivaInput("txtPlaca", "I");
	
	fn_util_inactivaInput("txtUbicacionCab", "I");
	fn_util_inactivaInput("txtEjecutivoBanca", "I");
	fn_util_inactivaInput("txtEjecutivoLeasing", "I");
		
	$("#txtDescripcion").val(fn_util_trim($("#txtDescripcion").val()));
	 
}


//****************************************************************
// Funcion		:: 	fn_cancelar  
// Descripción	::	Cancela las Operaciones de Cotizacion
// Log			:: 	??? - 05/11/2012
//****************************************************************
function fn_cancelar() {

    parent.fn_mdl_confirma(
                    "¿Está seguro de Volver?",
                    function() {                        
                        fn_util_globalRedirect("/GestionBien/Demanda/frmDemandaListado.aspx");
                    },
                    "Util/images/question.gif",
                    function() { },
                    'CONFIRMACION'
                   );
}


//****************************************************************
// Funcion		:: 	fn_grabar
// Descripción	::	Abre Editar Registro
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_grabar() {

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
            fn_buscaDemanda();
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
        colNames: ['','','Nº Demanda','Fecha Demanda','Nº Siniestro','Estado Demanda','Seguro','Estado del Bien','Nº Poliza','Aplicación del Fondo','Juzgado','F.última actualización','Docs.',''],
        colModel: [
				{ name: 'CodDemanda',			index: 'CodDemanda',			hidden: true },
				{ name: 'CodSiniestro',			index: 'CodSiniestro',			hidden: true },
                { name: 'NroDemanda',			index: 'NroDemanda',			sortable: true, sorttype: "int",	align: "center", defaultValue: "" },
                { name: 'FechaDemanda',			index: 'FechaDemanda',			sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'NroSiniestro',			index: 'NroSiniestro',			sortable: true, sorttype: "string", align: "left", defaultValue: "" },
                { name: 'DesEstadoDemanda',		index: 'DesEstadoDemanda',		sortable: true, sorttype: "string", align: "left", defaultValue: "" },
                { name: 'DesSeguro',			index: 'DesSeguro',				sortable: true, sorttype: "string", align: "left", defaultValue: "" },
                { name: 'DesEstadoBien',		index: 'DesEstadoBien',			sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'NroPoliza',			index: 'NroPoliza',				sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'DesAplicaFondo',		index: 'DesAplicaFondo',		sortable: true, sorttype: "string", align: "center", defaultValue: "", hidden: true  },                
                { name: 'Juzgado',				index: 'Juzgado',				sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'FecSituacion',			index: 'FecSituacion',			sortable: true, sorttype: "string", align: "center",		defaultValue: "" },                                
                { name: 'Doc',					index: 'Doc',					align: "center", sortable: false, formatter: fn_abreDocumentos, width: 45 },
                { name: '',						index: '', width: 10}
        ],
        width: glb_intWidthPantalla-70,
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'NroDemanda',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodDemanda").val(rowData.CodDemanda);
            $("#hddCodSiniestro").val(rowData.CodSiniestro);
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
   
    //**************************************************
    // Documentos
    //**************************************************
    function fn_abreDocumentos(cellvalue, options, rowObject) {
		//return ".";
        return "<img src='../../Util/images/ico_docs.gif' alt='Ver Documentos' title='Ver Documentos' width='18px' onclick=\"javascript:fn_GBAbreDocumentos(\'"+rowObject.CodSolCredito+"\',\'"+rowObject.SecFinanciamiento+"\',\'"+rowObject.CodDemanda+"\',\'"+C_GESTIONBIEN_DEMANDA+"\');\" style='cursor:pointer;' />";
    };
        
}



//****************************************************************
// Funcion		:: 	fn_buscaDemanda
// Descripción	::	Busca listado por parametros
// Log			:: 	JRC - 07/11/2012
//****************************************************************
function fn_buscaDemanda() {
    try {
        parent.fn_blockUI();

        var hddCodContrato = $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();
        var hddCodBien = $('#hddCodBien').val() == undefined ? "" : $('#hddCodBien').val();
       
        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación                             
                             "pstrCodContrato", hddCodContrato,
                             "pstrCodBien", hddCodBien
                            ];
		
        fn_util_AjaxWM("frmDemandaRegistro.aspx/ListaDemanda",
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
// Funcion		:: 	fn_eliminarSiniestro
// Descripción	::	Eliminar Siniestro
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_eliminarDemanda() {
		
	//Variables
    var strCodContrato = $("#hddCodContrato").val();
    var strCodBien = $("#hddCodBien").val();
    var strCodDemanda = $("#hddCodDemanda").val();
    var paramArray = ["hddCodContrato", strCodContrato, "hddCodBien", strCodBien, "hddCodDemanda", strCodDemanda];

    if (fn_util_trim(strCodDemanda) == "") {
        parent.fn_mdl_mensajeError("Debe seleccionar una Demanda", function() { }, "VALIDACIÓN");        
    }
    else{
    
		parent.fn_mdl_confirma(
            "¿Está seguro que desea eliminar la Demanda seleccionada?  ",
            function() {
                parent.fn_blockUI();
                
                fn_util_AjaxWM("frmDemandaRegistro.aspx/EliminaDemanda",
                                   paramArray,
                                   function(resultado) {
                                       fn_buscaDemanda();
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
		
	}

}


//****************************************************************
// Funcion		:: 	fn_editarDemanda
// Descripción	::	Editar Registro
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_editarDemanda() {
	var codDemanda = $("#hddCodDemanda").val();
	if(fn_util_trim(codDemanda) == ""){
		parent.fn_mdl_mensajeError("Debe seleccionar una Demanda", function() { }, "VALIDACIÓN");
	}else{
		parent.fn_util_AbreModal("Siniestro :: Editar", "GestionBien/Demanda/frmDemandaMnt.aspx?hddCodContrato="+ $("#hddCodContrato").val() +"&hddCodBien="+ $("#hddCodBien").val() + "&hddCodSiniestro=" + $("#hddCodSiniestro").val() + "&hddCodDemanda="+ $("#hddCodDemanda").val() + "&hddVer=0", 950, 550, function() { });
	}
}


//****************************************************************
// Funcion		:: 	fn_agregarDemanda
// Descripción	::	Abre Agregar Registro
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_agregarDemanda() {
	parent.fn_util_AbreModal("Demanda :: Nuevo", "GestionBien/Demanda/frmDemandaMnt.aspx?hddCodContrato="+ $("#hddCodContrato").val() +"&hddCodBien="+ $("#hddCodBien").val() +"&hddCodUnico="+ $("#hddCodUnico").val(), 950, 550, function() { });
}


//****************************************************************
// Funcion		:: 	fn_verDemanda
// Descripción	::	Ver Demanda
// Log			:: 	JRC - 03/12/2012
//****************************************************************
function fn_verDemanda() {
	var codDemanda = $("#hddCodDemanda").val();
	if(fn_util_trim(codDemanda) == ""){
		parent.fn_mdl_mensajeError("Debe seleccionar una Demanda", function() { }, "VALIDACIÓN");
	}else{
		parent.fn_util_AbreModal("Demanda :: Ver", "GestionBien/Demanda/frmDemandaMnt.aspx?hddCodContrato="+ $("#hddCodContrato").val() +"&hddCodBien="+ $("#hddCodBien").val() +"&hddCodSiniestro="+ $("#hddCodSiniestro").val() + "&hddCodDemanda="+ $("#hddCodDemanda").val() + "&hddVer=1", 950, 500, function() { });
	}
}


//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos(pstrCodContrato, pstrCodBien, pstrCodRelacionado, pstrCodTipo) {	
	parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + "&hddver= " + $("#hddVer").val(), 800, 350, function() { });	
}
