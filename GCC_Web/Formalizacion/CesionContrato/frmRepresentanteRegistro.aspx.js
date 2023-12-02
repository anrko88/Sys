//****************************************************************
// Variables Globales
//****************************************************************
var strComboVacio = "<option value='0'>[-Seleccione-]</option>"

var strTipoDocumentoDNI = "1";
var strTipoDocumentoRUC = "2";
var strTipoDocumentoCarnetEx = "3";
var strTipoDocumentoPasaporte = "5";
var strTipoDocumentoOtroDoc = "6";

var C_TX_NUEVO = "NUEVO"
var C_TX_EDITAR = "EDITAR"

var strNombreCompleto = "";
var strTipoDoc = "";
var strNroDoc = "";
var strNroPartida = "";
var strOfRegistral = "";

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
    
    //Botones
    $("#sp_editar").hide();
    
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
    
});




//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 04/01/2013
//****************************************************************
function fn_inicializaCampos() {		
	$('#txtNroDocumento').validText({ type: 'number', length: 11 });
	$('#txtNombreCompleto').validText({ type: 'comment', length: 100 });	
	$('#txtNroPartida').validText({ type: 'number', length: 8 });	
	$('#txtOfRegistral').validText({ type: 'comment', length: 50 });	
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
            fn_listadoRepresentantes();
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
        width: glb_intWidthPantalla-40,
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 5,
        rowList: [5, 10, 20, 30],
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
            $("#hddCodCesionario").val(rowData.CodCesionario);
            $("#hddCodRepresentante").val(rowData.CodRepresentante);
            
            strNombreCompleto = fn_util_trim(rowData.NomRepresentante);
			strTipoDoc = fn_util_trim(rowData.CodigoTipoDocumento);
			strNroDoc = fn_util_trim(rowData.NroDocumento);
			strNroPartida = fn_util_trim(rowData.PartidaRegistral);
			strOfRegistral = fn_util_trim(rowData.OficinaRegistral);
            
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

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
        
        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación                             
                             "pstrCodContrato", hddCodContrato,
                             "pstrCodCesionario", hddCodCesionario
                            ];

        fn_util_AjaxWM("frmRepresentanteRegistro.aspx/ListaRepresentates",
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
// Funcion		:: 	fn_agregarRepresentante
// Descripción	::	Abre Editar Registro
// Log			:: 	JRC - 02/11/2013
//****************************************************************
function fn_agregarRepresentante() {	
	parent.fn_blockUI();

    //String Validación
    var strError = new StringBuilderEx();

    //Instancia Validaciones    		
    //hddCodCesionario
    //hddCodRepresentante
	var objtxtNombreCompleto = $('input[id=txtNombreCompleto]:text');
	var objcmdTipoDoc		 = $('select[id=cmdTipoDoc]');
	var objtxtNroDocumento   = $('input[id=txtNroDocumento]:text');
	var objtxtNroPartida	 = $('input[id=txtNroPartida]:text');		
	var objtxtOfRegistral	 = $('input[id=txtOfRegistral]:text');
	
    //Valida
	strError.append(fn_util_ValidateControl(objtxtNombreCompleto[0], 'un Nombre válido', 1, ''));
	strError.append(fn_util_ValidateControl(objcmdTipoDoc[0], 'un Tipo de documento válido', 1, ''));
	strError.append(fn_util_ValidateControl(objtxtNroDocumento[0], 'un Nro. de documento válido', 1, ''));
	strError.append(fn_util_ValidateControl(objtxtNroPartida[0], 'una Nro. Partida válida', 1, ''));	
	strError.append(fn_util_ValidateControl(objtxtOfRegistral[0], 'una Of. Registral válida', 1, ''));
		
	var strTipoDocumento = $("#cmdTipoDoc").val();
    var strNroDocumento = $("#txtNroDocumento").val();
	var intNroDocumento = strNroDocumento.length;
	if (fn_util_trim(strTipoDocumento) == strTipoDocumentoDNI) {
		if (intNroDocumento < 8) strError.append('&nbsp;&nbsp;- Número de Documento Inválido <br />');
	} else if (fn_util_trim(strTipoDocumento) == strTipoDocumentoRUC) {
		if (intNroDocumento < 11) strError.append('&nbsp;&nbsp;- Número de Documento Inválido <br />');
	} else if (fn_util_trim(strTipoDocumento) == strTipoDocumentoCarnetEx) {
		if (intNroDocumento < 4) strError.append('&nbsp;&nbsp;- Número de Documento Inválido <br />');
	} else if (fn_util_trim(strTipoDocumento) == strTipoDocumentoPasaporte) {
		if (intNroDocumento < 7) strError.append('&nbsp;&nbsp;- Número de Documento Inválido <br />');
	} else {
		if (intNroDocumento < 4) strError.append('&nbsp;&nbsp;- Número de Documento Inválido <br />');
	}	//Valida error existente

		
	//Valida error existente
	if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }else{
    


		var arrParametros = [     
							"pstrTipoTx",			$("#hddTipoTx").val(),
							"pstrCodContrato",		$("#hddCodContrato").val(),
							"pstrCodCesionario",	$("#hddCodCesionario").val(),
							"pstrCodRepresentante",	$("#hddCodRepresentante").val(),			
							"pstrNombreCompleto",	$("#txtNombreCompleto").val(),														
							"pstrTipoDoc",			$("#cmdTipoDoc").val(),							
							"pstrNroDocumento",		$("#txtNroDocumento").val(),
							"pstrNroPartida",		$("#txtNroPartida").val(),							
							"pstrOfRegistral",		$("#txtOfRegistral").val()							
							];

            
        fn_util_AjaxWM("frmRepresentanteRegistro.aspx/GrabaRepresentante",
                 arrParametros,
                 function(result) {
                     parent.fn_unBlockUI();
                     if (fn_util_trim(result) == "0") {
                         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL GUARDAR");
                     } else {                         
                         if ($("#hddTipoTx").val() == C_TX_NUEVO) {
                             parent.fn_mdl_mensajeOk("Se grabó el Representante correctamente.", function() { fn_RedireccionGrabar() }, "GRABADO CORRECTO");
                         } else {
                             parent.fn_mdl_mensajeOk("Se actualizó correctamente los datos del Representante", function() { fn_RedireccionGrabar() }, "GRABADO CORRECTO");
                         }
                         
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
function fn_RedireccionGrabar(){
	fn_listadoRepresentantes();
	fn_limpiar();
	fn_cancelaRepresentante();
	
	var ctrlIframe = parent.document.getElementById('ifrModal');	
	var ctrlBtn = ctrlIframe.contentWindow.document.getElementById("btnBuscar");		
	ctrlBtn.click();
	//parent.fn_util_CierraModal2();
	
}





//****************************************************************
// Funcion		:: 	fn_limpiaRepresentante
// Descripción	::	Limpiar Representante
// Log			:: 	JRC - 09/11/2013
//****************************************************************
function fn_limpiar() {	
	
	$("#hddCodRepresentante").val("");
	$("#txtNombreCompleto").val("");
	$("#cmdTipoDoc").val(0);
	$("#txtNroDocumento").val("");
	$("#txtNroPartida").val("");	
	$("#txtOfRegistral").val("");
	
	strNombreCompleto = "";
	strTipoDoc = "";
	strNroDoc = "";
	strNroPartida = "";
	strOfRegistral = "";
	
	$("#txtNombreCompleto").attr('class', 'css_input');
	$("#cmdTipoDoc").attr('class', 'css_select');
	$("#txtNroDocumento").attr('class', 'css_input');
	$("#txtNroPartida").attr('class', 'css_input');
	$("#txtOfRegistral").attr('class', 'css_input');
			
}



//****************************************************************
// Funcion		:: 	fn_cancelaRepresentante
// Descripción	::	Editar Representante
// Log			:: 	JRC - 09/11/2013
//****************************************************************
function fn_cancelaRepresentante() {	
	fn_limpiar();
	$("#hddTipoTx").val(C_TX_NUEVO);
	
	//Botones
    $("#sp_editar").hide();
    $("#sp_nuevo").show();
}


//****************************************************************
// Funcion		:: 	fn_editarRepresentante
// Descripción	::	Editar Representante
// Log			:: 	JRC - 09/11/2013
//****************************************************************
function fn_editarRepresentante() {	

	var strCodCesionario = $("#hddCodCesionario").val();
	
	if(fn_util_trim(strCodCesionario) == ""){
		parent.fn_mdl_mensajeIco("Debe seleccionar un Cesionario", "util/images/warning.gif", "VALIDACION");
	}else{
		
		//Quita Obligatorios
		$("#txtNombreCompleto").attr('class', 'css_input');
		$("#cmdTipoDoc").attr('class', 'css_select');
		$("#txtNroDocumento").attr('class', 'css_input');
		$("#txtNroPartida").attr('class', 'css_input');
		$("#txtOfRegistral").attr('class', 'css_input');
		
		//Pone Datos		
		$("#txtNombreCompleto").val(strNombreCompleto);
		$("#cmdTipoDoc").val(strTipoDoc);
		$("#txtNroDocumento").val(strNroDoc);
		$("#txtNroPartida").val(strNroPartida);
		$("#txtOfRegistral").val(strOfRegistral);
		
		$("#hddTipoTx").val(C_TX_EDITAR);
	
		$("#sp_editar").show();
		$("#sp_nuevo").hide();
    
	}

}


//****************************************************************
// Funcion		:: 	fn_eliminarRepresentante
// Descripción	::	Editar Representante
// Log			:: 	JRC - 09/11/2013
//****************************************************************
function fn_eliminarRepresentante() {	

	var strCodContrato = $("#hddCodContrato").val();	
	var strCodCesionario = $("#hddCodCesionario").val();
	var strCodRepresentante = $("#hddCodRepresentante").val();
				
	if(fn_util_trim(strCodRepresentante) == ""){
		parent.fn_mdl_mensajeIco("Debe seleccionar un Representante", "util/images/warning.gif", "VALIDACION");
	}else{
		
		var paramArray = ["hddCodContrato", strCodContrato, "hddCodCesionario", strCodCesionario, "hddCodRepresentante", strCodRepresentante];
    
		parent.fn_mdl_confirma(
            "¿Está seguro que desea eliminar el Representante seleccionado?  ",
            function() {
                parent.fn_blockUI();
                
                fn_util_AjaxWM("frmRepresentanteRegistro.aspx/EliminaRepresentate",
                                   paramArray,
                                   function(resultado) {
                                       //fn_listadoRepresentantes();
                                       fn_RedireccionGrabar()
                                       $("#hddCodRepresentante").val("");
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




