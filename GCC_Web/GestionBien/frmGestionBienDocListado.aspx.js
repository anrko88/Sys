//****************************************************************
// Variables Globales
//****************************************************************


//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 07/11/2012
//****************************************************************
$(document).ready(function() {
    
    //Carga Grilla Documentos
    fn_GBCargaGrilla();
            	
    //***********************************    	
    //Valida Modo Ver
    //***********************************
    var strVer = $("#hddVer").val();   
    if( fn_util_trim(strVer) == "1" ){
		$("#dv_separador").hide();
		$("#dv_GBEliminar").hide();
		$("#dv_GBEditar").hide();
		$("#dv_GBAgregar").hide();
    }
            	
    //On load Page (siempre al final)
    fn_onLoadPage();
    
});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 05/12/2012
//****************************************************************
function fn_GBInicializaCampos() {

}


//****************************************************************
// Funcion		:: 	fn_GBCargaGrilla 
// Descripción	::	Inicializa Grilla Documento
// Log			:: 	JRC - 05/12/2012
//****************************************************************
function fn_GBCargaGrilla(){
    
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
			fn_GBListarDocumentos();	
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
        colNames: ['Codigo','Nombre Archivo', 'Adjunto', 'Comentario'],
        colModel: [
                { name: 'CodDocumento', index: 'CodDocumento', hidden:true },
		        { name: 'NombreArchivo', index: 'NombreArchivo', width: 200, align: "left", sorttype: "string", defaultValue: "" },
		        { name: 'RutaArchivo', index: 'RutaArchivo', width: 100, align: "Center", sortable: false, formatter: fn_icoDownload },
		        { name: 'Comentario', index: 'Comentario', width: 550, align: "left", sorttype: "string", defaultValue: "" }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 5,
        rowList: [10, 20, 30],
        sortname: 'CodDocumento',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodDocumento").val(rowData.CodDocumento);
        },
        ondblClickRow: function(id) {
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    
    //Abrir Archivo
    function fn_icoDownload(cellvalue, options, rowObject) {
        var strNombreArchivo = rowObject.RutaArchivo.split('\\').pop();
        strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
        if (fn_util_trim(rowObject.RutaArchivo) != "") {
            return "<img src='../Util/images/ico_download.gif' alt='" + strNombreArchivo + "' title='" + strNombreArchivo + "' onclick=\"javascript:fn_GBAbreArchivo('" + encodeURIComponent(rowObject.RutaArchivo) + "');\" style='cursor:pointer;'/>";
        } else {
            return ".";
        }
    };
    
}



//****************************************************************
// Funcion		:: 	fn_GBListarDocumentos 
// Descripción	::	Abre Modal de Motivo de Rechazo
// Log			:: 	JRC - 05/12/2012
//****************************************************************
function fn_GBListarDocumentos() {

    try {        

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"), // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
                             "pstrCodContrato", $("#hddCodContrato").val(),
                             "pstrCodBien", $("#hddCodBien").val(),
                             "pstrCodRelacionado", $("#hddCodRelacionado").val(),
                             "pstrCodTipo", $("#hddCodTipo").val()     
                            ];

        fn_util_AjaxWM("frmGestionBienDocListado.aspx/ListadoDocumentos",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);                    
                    fn_doResize();
                },
                function(request) {                    
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

    } catch (ex) {        
        fn_util_alert(ex.message);
    }

}


//****************************************************************
// Funcion		:: 	fn_abreArchivo 
// Descripción	::	Abre Archivo
// Log			:: 	JRC - 05/12/2012
//****************************************************************
function fn_GBAbreArchivo(pstrRuta) {
    window.open("../frmDownload.aspx?nombreArchivo=" + pstrRuta);
    return false;
}


//****************************************************************
// Funcion		:: 	fn_GBAbreNuevo
// Descripción	::	Abre Nuevo Documento Comentario
// Log			:: 	JRC - 05/12/2012
//****************************************************************
function fn_GBAbreNuevo() {
	var hddCodContrato = fn_util_trim($("#hddCodContrato").val());
	var hddCodBien = fn_util_trim($("#hddCodBien").val());
	var hddCodRelacionado = fn_util_trim($("#hddCodRelacionado").val());
	var hddCodTipo = fn_util_trim($("#hddCodTipo").val());
	var strParametros = "?hddCodContrato="+hddCodContrato+"&hddCodBien="+hddCodBien+"&hddCodRelacionado="+hddCodRelacionado+"&hddCodTipo="+hddCodTipo
	
	fn_util_globalRedirect("/GestionBien/frmGestionBienDocRegistro.aspx"+strParametros);
}


//****************************************************************
// Funcion		:: 	fn_GBAbreEditar
// Descripción	::	Abre Editar Documento Comentario
// Log			:: 	JRC - 05/12/2012
//****************************************************************
function fn_GBAbreEditar() {
	var strCodDocumento = fn_util_trim($("#hddCodDocumento").val());
	if(strCodDocumento == ""){
		parent.fn_mdl_mensajeError("Debe seleccionar un Documento", function() { }, "VALIDACIÓN");
	}else{
		var hddCodContrato = fn_util_trim($("#hddCodContrato").val());
		var hddCodBien = fn_util_trim($("#hddCodBien").val());
		var hddCodRelacionado = fn_util_trim($("#hddCodRelacionado").val());
		var hddCodTipo = fn_util_trim($("#hddCodTipo").val());
		var strParametros = "?hddCodContrato="+hddCodContrato+"&hddCodBien="+hddCodBien+"&hddCodRelacionado="+hddCodRelacionado+"&hddCodTipo="+hddCodTipo+"&hddCodDocumento="+strCodDocumento;
		
		fn_util_globalRedirect("/GestionBien/frmGestionBienDocRegistro.aspx"+strParametros);
	}	
}



//****************************************************************
// Funcion		:: 	fn_GBEliminar
// Descripción	::	Eliminar Documento Comentario
// Log			:: 	JRC - 05/12/2012
//****************************************************************
function fn_GBEliminar() {

	var strCodDocumento = fn_util_trim($("#hddCodDocumento").val());
	
	if(strCodDocumento == ""){
		parent.fn_mdl_mensajeError("Debe seleccionar un Documento", function() { }, "VALIDACIÓN");
	}else{
		
		//Variables
		var hddCodContrato = fn_util_trim($("#hddCodContrato").val());
		var hddCodBien = fn_util_trim($("#hddCodBien").val());
		var hddCodRelacionado = fn_util_trim($("#hddCodRelacionado").val());
		var hddCodTipo = fn_util_trim($("#hddCodTipo").val());
		
		var paramArray = ["pstrCodContrato", hddCodContrato, 
							"pstrCodBien", hddCodBien, 
							"pstrCodRelacionado", hddCodRelacionado, 
							"pstrCodDocumento", strCodDocumento, 
							"pstrCodTipo", hddCodTipo
						 ];
	    
		parent.fn_mdl_confirma(
            "¿Está seguro que desea eliminar el Documento/Comentario seleccionado?  ",
            function() {
                parent.fn_blockUI();
                
                fn_util_AjaxWM("frmGestionBienDocListado.aspx/EliminarGestionBienDoc",
                                   paramArray,
                                   function(resultado) {
                                       fn_GBListarDocumentos();
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

