var DestinoCredito_Inmueble = ["002"];
var DestinoCredito_Maquinaria = ["003", "004", "005"];
var DestinoCredito_Otros = ["007", "008"];
var DestinoCredito_Vehiculo = ["006"];

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	AEP - 24/09/2012
//****************************************************************
$(document).ready(function() {

    $("div#divTabs").tabs({
        show: function() {
            fn_doResize();
        }
    });

    $("div#divTabsV").tabs({
        show: function() {
            fn_doResize();
        }
    });

    $("div#divTabsS").tabs({
        show: function() {
            fn_doResize();
        }
    });
    $("div#divTabsM").tabs({
        show: function() {
            fn_doResize();
        }
    });

	fn_InicializarCampos();

    // al final
    fn_onLoadPage();


});


function fn_SetearCamposObligatorios() {
	

	}

function fn_InicializarCampos()
{
	 fn_configurar_PanelesBienes();
	fn_CargarGrillaDetalleInscripcion();
	fn_CargarGrillaInafectacion();
	fn_CargarGrillaDocuementosIM();
}

//************************************************************
// Función		:: 	fn_configurar_PanelesBienes
// Descripcion 	:: 	Configura las distintas ventanas de mantenimiento de los bienes
// Log			:: 	AEP - 26/09/2012
//************************************************************
function fn_configurar_PanelesBienes() {

    $("#dv_datos_otros").hide();
    $("#dv_datos_vehiculo").hide();
    $("#dv_datos_inmueble").hide();
    $("#dv_datos_maquinaria").hide();
    //IBK JJM
   //("#dv_img_botonNxt").hide();
    //"#dv_img_botonBck").hide();
    //Fin
    // Inmueble
    if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_datos_inmueble").show(); }
    // Maquinaria
    else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_datos_maquinaria").show(); }
    // Vehivulo
    else if (DestinoCredito_Vehiculo.indexOf($("#hidCodClasificacion").val()) != -1) {
        $("#dv_datos_vehiculo").show();
        //IBK JJM
       //("#dv_img_botonNxt").show();
        //"#dv_img_botonBck").show();
    }
    //Fin

    // Otros
    else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_datos_otros").show(); }
}


function fn_CargarGrillaDetalleInscripcion() {
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el detalle de inscripcion municipal
// Log			:: 	AEP - 24/09/2012
//****************************************************************

    $("#jqGrid_lista_A").jqGrid({
 
    datatype: function() {
        fn_ListagrillaInscripcion();
    },
    jsonReader: //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            {
            root: "Items",
            page: "CurrentPage",            // Número de página actual.
            total: "PageCount",             // Número total de páginas.
            records: "RecordCount",         // Total de registros a mostrar.
            repeatitems: false,
            id: "codInscripcionMunicipalDetalle"   // Índice de la columna con la clave primaria.
        },
    colNames: ['Codigo', 'Partida Registral', 'Asiento Registral', 'Acto Registral','',''],
    colModel: [
                { name: 'codInscripcionMunicipalDetalle', index: 'codInscripcionMunicipalDetalle', hidden: true },
		        { name: 'PartidaRegistral', index: 'PartidaRegistral', width: 100, align: "Center", sorttype: "string"},
		        { name: 'AsientoRegistral', index: 'AsientoRegistral', width: 100, align: "Center", sortable: false },
		        { name: 'Acto', index: 'Acto', width: 100, align: "Center", sorttype: "string" },
    	        { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
    	        { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true }
	    ],
    //width: 100%,
    height: '100%',
    pager: '#jqGrid_pager_A',
    loadtext: 'Cargando datos...',
    emptyrecords: 'No hay resultados',
    rowNum: 10,                             // Tamaño de la página
    rowList: [10, 20, 30],
    sortname: 'codInscripcionMunicipalDetalle', // Columna a ordenar por defecto.
    sortorder: 'asc',                     // Criterio de ordenación por defecto.
    viewrecords: true,                      // Muestra la cantidad de registros.
    gridview: true,
    autowidth: false,
    altRows: true,
    loadonce: false,
    altclass: 'gridAltClass',
    onSelectRow: function(id) {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
        $("#hidRowInscripcionMunicipal").val(id);   
    }
});

jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });

// Le asigna el ancho a usar para la grilla.
$("#jqGrid_lista_A").setGridWidth($(window).width() - 120);
$("#search_jqGrid_lista_A").hide();

}

function fn_CargarGrillaInafectacion() {
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Lista los datos de inafectacion
// Log			:: 	AEP - 24/09/2012
//****************************************************************

  $("#jqGrid_lista_B").jqGrid({
    
    datatype: function() {
        fn_ListagrillaInafectacion();
    },
    jsonReader: //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            {
            root: "Items",
            page: "CurrentPage",            // Número de página actual.
            total: "PageCount",             // Número total de páginas.
            records: "RecordCount",         // Total de registros a mostrar.
            repeatitems: false,
            id: "CodigoContratoDocumento"   // Índice de la columna con la clave primaria.
        },
    colNames: ['Codigo', 'Periodo', 'Fecha Envío de Carta', 'Fecha Recepción Documento', 'Fecha Presentación SAT', 'Fecha Notificación', 'Nro. Resolución', 'Estado', '', '', '', '','Archivo' , 'Observaciones'],
        colModel: [
                { name: 'codInmatriculacionDetalle', index: 'codInmatriculacionDetalle', hidden: true },
		        { name: 'Periodo', index: 'Periodo', width: 10, align: "left", sorttype: "string" },
    	        { name: 'FechaEnvioCarta', index: 'FechaEnvioCarta', width: 20, align: "left", sorttype: "string" },
		        { name: 'FechaRecepcionDocumentos', index: 'FechaRecepcionDocumentos', width: 30, align: "Center", sortable: false },
		        { name: 'FechaPresentacionSAT', index: 'FechaPresentacionSAT', width: 20, align: "left", sorttype: "string" },
    	        { name: 'FechaNotificacion', index: 'FechaNotificacion', width: 20, align: "left", sorttype: "string" },
		        { name: 'NroResolucion', index: 'NroResolucion', width: 20, align: "left", sorttype: "string" },
		        { name: 'EstadoResolucion', index: 'EstadoResolucion', width: 10, align: "Center", sortable: false },
    	        { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
    	        { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
    	        { name: 'Estado', index: 'Estado', hidden: true },
  	            { name: 'CodEstadoResolucion', index: 'CodEstadoResolucion', hidden: true },
 	            { name: 'adjunto', index: 'adjunto', width: 10, align: "center", sorttype: "string", formatter: VerAdjunto1 },
//                { name: 'SubirArchivo', index: 'SubirArchivo', width: 10, align: "center", sortable: false, formatter: SubirArchivo5 },
                { name: 'lupa', index: 'lupa', width: 20, align: "center", sortable: false, formatter: Lupa }

	    ],
    //width: 300,
    height: '100%',
    pager: '#jqGrid_pager_B',
    loadtext: 'Cargando datos...',
    emptyrecords: 'No hay resultados',
    rowNum: 10,                             // Tamaño de la página
    rowList: [10, 20, 30],
    sortname: 'CodigoContratoDocumento', // Columna a ordenar por defecto.
    sortorder: 'asc',                     // Criterio de ordenación por defecto.
    viewrecords: true,                      // Muestra la cantidad de registros.
    gridview: true,
    autowidth: false,
    altRows: true,
    loadonce: false,
    altclass: 'gridAltClass',
    onSelectRow: function(id) {
        var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
        $("#hidRowInafectacion").val(id);
    }
});

jQuery("#jqGrid_lista_B").jqGrid('navGrid', '#jqGrid_pager_B', { edit: false, add: false, del: false });

// Le asigna el ancho a usar para la grilla.
$("#jqGrid_lista_B").setGridWidth($(window).width() - 120);
$("#search_jqGrid_lista_B").hide();

 //INICIO - JJM
    function VerAdjunto1(cellvalue, options, rowObject) {
        if (fn_util_trim(rowObject.adjunto) != "") {
            var strNombreArchivo = rowObject.adjunto.split('\\').pop();
            strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
            return "<img src='../../Util/images/ico_download.gif' alt='Descargar/Mostrar Archivo' title='" + strNombreArchivo + "'Descargar/Mostrar Archivo' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.adjunto) + "');\" style='cursor:pointer;'/>";
        } else {
            return ".";
        }
    };
    function SubirArchivo5(cellvalue, options, rowObject) {

        var sScript2 = "javascript:AdjuntarArchivoDocumento(" + rowObject.codInmatriculacionDetalle + ",2);";
        return "<img src='../../Util/images/ico_acc_adjuntarMini.gif' alt='" + cellvalue + "' title='Subir Archivo' width='20px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
    };

    function Lupa(cellvalue, options, rowObject) {
        if (rowObject.Flagobservacion == 0) {
            var sScript2 = "javascript:VerObservaciones(" + rowObject.codInmatriculacionDetalle + ",1);";
            return "<img src='../../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
        } else {
            sScript2 = "javascript:VerObservaciones(" + rowObject.codInmatriculacionDetalle + ",1);";
            return "<img src='../../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
        }
    };
}
function AdjuntarArchivoDocumento(CodInmatriculacionDetalle) {
   
    var sTitulo = "Mantenimiento Bien";
    var sSubTitulo = "Mant. Bien:: Inafectación";
    parent.fn_util_AbreModal(sSubTitulo, "Comun/frmSubirArchivo.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodConDoc=" + CodInmatriculacionDetalle + "&hddCodContrato=&Add=False", 550, 150, function() { });

};
function fn_abreArchivo(pstrRuta) {
    //alert(pstrRuta);
    window.open("../../frmDownload.aspx?nombreArchivo=" + pstrRuta);
    return false;
}
function VerObservaciones(CodInmatriculacionDetalle, el) {
    var sTitulo = "Mantenimiento Bien";
    var sSubTitulo = "Bien Detalle:: Inafectación  ";
    parent.fn_util_AbreModal(sSubTitulo, "Consultas/DetalleBien/frmConsultaInafectacionObservacion.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodConDoc=" + CodInmatriculacionDetalle + "&hddCodContrato=" + $("#txtNumeroContrato").html() + "&sflagtipoobs=" + el + "&Add=Inafectacion", 700, 300, function() { });
}
//FIN - JJM
//****************************************************************
// Funcion		:: 	fn_ListagrillaInafectacion
// Descripción	::	
// Log			:: 	AEP - 18/10/2012
//****************************************************************
function fn_ListagrillaInafectacion() {
	var arrParametros = ["pCodigoContrato", $("#txtNumeroContrato").html(),
		                "pCodigoBien", $("#hidSecFinanciamiento").val()                         
	];

	fn_util_AjaxSyncWM("frmConsultasBienDetalle.aspx/ListaInafectacion",
		arrParametros,
		function(jsondata) {
			jqGrid_lista_B.addJSONData(jsondata);
			parent.fn_unBlockUI();
		},
		function(request) {
			fn_util_alert(jQuery.parseJSON(request.responseText).Message);
			parent.fn_unBlockUI();
		}
	);

	//Hiden para limpiar la variable de Aprobacion de 
	$("#hddFlagVerificaAdjunto").val("");
}

function fn_CargarGrillaDocuementosIM() {
/*******************************
CARGA GRILLA TAB  DOCUMENTOS 
********************************/
 
		  
    $("#jqGrid_lista_C").jqGrid({
    datatype: function() {
        fn_ListagrillaDocumentos();
    },
    jsonReader: //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            {
            root: "Items",
            page: "CurrentPage",            // Número de página actual.
            total: "PageCount",             // Número total de páginas.
            records: "RecordCount",         // Total de registros a mostrar.
            repeatitems: false,
            id: "CodigoContratoDocumento"   // Índice de la columna con la clave primaria.
        },
    colNames: ['Codigo','','', 'Nombre Archivo', 'Adjunto', 'Comentario','',''],
    colModel: [
                { name: 'CODIGOBIENDOCUMENTO', index: 'CODIGOBIENDOCUMENTO', hidden: true },
    	        { name: 'NUMEROCONTRATO', index: 'NUMEROCONTRATO', hidden: true },
    	        { name: 'SECFINANCIAMIENTO', index: 'SECFINANCIAMIENTO', hidden: true },
		        { name: 'NOMBREARCHIVO', index: 'NOMBREARCHIVO', width: 200, align: "left", sorttype: "string", defaultValue: "" },
		        { name: 'ADJUNTO', index: 'ADJUNTO', width: 100, align: "Center", sortable: false, formatter: fn_icoDownload },
		        //{ name: 'OBSERVACIONES', index: 'OBSERVACIONES', width: 200, align: "center", sorttype: "string" ,sortable:false,formatter:VerObservacion},
    	        { name: 'OBSERVACIONES', index: 'OBSERVACIONES',align: "left", sorttype: "string"},
    	        { name: 'ESTADODOCCONTRATO', index: 'ESTADODOCCONTRATO', hidden: true },
    	        { name: 'ESTADODOCBIEN', index: 'ESTADODOCBIEN', hidden: true }
	    ],
    height: '100%',
    pager: '#jqGrid_pager_C',
    loadtext: 'Cargando datos...',
    emptyrecords: 'No hay resultados',
    rowNum: 10,                             // Tamaño de la página
    rowList: [10, 20, 30],
    sortname: 'CODIGOBIENDOCUMENTO', // Columna a ordenar por defecto.
    sortorder: 'asc',                     // Criterio de ordenación por defecto.
    viewrecords: true,                      // Muestra la cantidad de registros.
    gridview: true,
    autowidth: true,
    altRows: true,
    loadonce: false,
    altclass: 'gridAltClass',
    onSelectRow: function(id) {
        var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id);
        $("#hidRowDocumento").val(id);
    }
});


jQuery("#jqGrid_lista_C").jqGrid('navGrid', '#jqGrid_pager_C', { edit: false, add: false, del: false });

// Le asigna el ancho a usar para la grilla.
$("#jqGrid_lista_C").setGridWidth($(window).width() - 120);
$("#search_jqGrid_lista_C").hide();

function fn_icoDownload(cellvalue, options, rowObject) {
    var strNombreArchivo = rowObject.ADJUNTO.split('\\').pop();
    strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
	
	//if(fn_util_trim(rowObject.ESTADODOCBIEN) == "1") {
		if (fn_util_trim(rowObject.ADJUNTO) != "") {
        return "<img src='../../Util/images/ico_download.gif' alt='" + strNombreArchivo + "' title='" + strNombreArchivo + "' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.ADJUNTO) + "');\" style='cursor:pointer;'/>";
    } else {
        return ".";
    }
//	}else {
//		return "";
//	}
	
};

function VerObservacion(cellvalue, options, rowObject) {
           
       if (rowObject.OBSERVACIONES == "") {
           return "<img src='../../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
       } else {
          var sScript2 = "javascript:VerObservacionesDocumento('" + rowObject.NUMEROCONTRATO + "','" + rowObject.SECFINANCIAMIENTO + "','" + rowObject.OBSERVACIONES + "','" + rowObject.NOMBREARCHIVO + "');";
       	   return "<img src='../../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick=\"" + sScript2 + "\" style='cursor: pointer;cursor: hand;' />";
       }
    	
    };
	
	

}

	 function fn_abreArchivo(pstrRuta) {
       window.open("../../frmDownload.aspx?nombreArchivo=" + pstrRuta);
       return false;
   }
//****************************************************************
// Funcion		:: 	fn_ListagrillaDocumentos
// Descripción	::	
// Log			:: 	AEP - 12/10/2012
//****************************************************************
function fn_ListagrillaDocumentos() {
	var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_C", "rowNum"), // Cantidad de elementos de la página
		"pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_C", "page"), // Página actual
		"pSortColumn", fn_util_getJQGridParam("jqGrid_lista_C", "sortname"), // Columna a ordenar
		"pSortOrder", fn_util_getJQGridParam("jqGrid_lista_C", "sortorder"), // Criterio de ordenación
		"pCodigoContrato", $("#txtNumeroContrato").html(),
		"pCodigoBien", $("#hidSecFinanciamiento").val()                         
	];

	fn_util_AjaxSyncWM("frmConsultasBienDetalle.aspx/ListaDocumentos",
		arrParametros,
		function(jsondata) {
			jqGrid_lista_C.addJSONData(jsondata);
			parent.fn_unBlockUI();
		},
		function(request) {
			fn_util_alert(jQuery.parseJSON(request.responseText).Message);
			parent.fn_unBlockUI();
		}
	);

	//Hiden para limpiar la variable de Aprobacion de 
	$("#hddFlagVerificaAdjunto").val("");
}

//****************************************************************
// Funcion		:: 	fn_ListagrillaInscripcion
// Descripción	::	
// Log			:: 	AEP - 19/10/2012
//****************************************************************
function fn_ListagrillaInscripcion() {
	var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
		"pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"), // Página actual
		"pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
		"pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
		"pCodigoContrato", $("#txtNumeroContrato").html(),
		"pCodigoBien", $("#hidSecFinanciamiento").val()                         
	];

	fn_util_AjaxSyncWM("frmConsultasBienDetalle.aspx/ListaDocumentosInscripcion",
		arrParametros,
		function(jsondata) {
			jqGrid_lista_A.addJSONData(jsondata);
			parent.fn_unBlockUI();
			$("#hidRowInscripcionMunicipal").val('');
		},
		function(request) {
			fn_util_alert(jQuery.parseJSON(request.responseText).Message);
			parent.fn_unBlockUI();
		}
	);

	//Hiden para limpiar la variable de Aprobacion de 
	$("#hddFlagVerificaAdjunto").val("");
}


//****************************************************************
// Funcion		:: 	fn_EditarInscripcionMunicipal
// Descripción	::	Abre Modal para agregar la Inscripcion Municipal
// Log			:: 	AEP - 19/10/2012
//****************************************************************
function fn_EditarInscripcionMunicipal() {
   
        var strCodigoContrato = $("#hidNumeroContrato").val();
	    var strCodigoBien=$("#hidSecFinanciamiento").val();
	    var id = $("#hidRowInscripcionMunicipal").val();
	    if(IsNullOrEmpty(id)) 
	    {
	    parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	    }else 
	    {
		var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
		var codigoInscripcion = rowData.codInscripcionMunicipalDetalle;
		var strPartida = rowData.PartidaRegistral;
	    var strAsiento=	rowData.AsientoRegistral;
		var strActo = rowData.Acto;
	    var estado = rowData.Estado;
	
        parent.fn_util_AbreModal("", "Consultas/DetalleBien/frmConsultaInscripcionMunicipalDetalle.aspx?NumContrato=" + strCodigoContrato + "&CodigoBien=" + strCodigoBien + "&codigo=" + 
	                            codigoInscripcion + "&partida=" + strPartida + "&asiento=" + strAsiento + 
		                        "&acto=" + strActo + "&estado=" + estado , 550, 200, function() { });
	}    
}


//****************************************************************
// Funcion		:: 	fn_EditarInafectacion
// Descripción	::	Abre Modal para agregar la inafectación
// Log			:: 	AEP - 18/10/2012
//****************************************************************
function fn_EditarInafectacion() {
   
        var strCodigoContrato = $("#hidNumeroContrato").val();
        var strCodigoBien = $("#hidSecFinanciamiento").val();
        var strAnioFabricacion = $("#txtAnioVehivulo").val();
	    var id = $("#hidRowInafectacion").val();
	    if(IsNullOrEmpty(id)) 
	    {
	    parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	    }else 
	    {
		var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
		var codigoInafectacion = rowData.codInmatriculacionDetalle;
		var strPeriodo = rowData.Periodo;
	    var strFechaEnvioCarta=	rowData.FechaEnvioCarta;
		var strFechaRecepcionDocumentos = rowData.FechaRecepcionDocumentos;
	    var strFechaPresentacionSat = rowData.FechaPresentacionSAT;
	    var strFechaNotificacion = rowData.FechaNotificacion;	
	    var nroResolucion = rowData.NroResolucion;
	    var codEstadoResolucion = rowData.EstadoResolucion;
	    var estado = rowData.Estado;
	
        parent.fn_util_AbreModal("", "Consultas/DetalleBien/frmConsultaInafectacion.aspx?NumContrato=" + strCodigoContrato + "&CodigoBien=" + strCodigoBien + "&codigo=" + 
	                            codigoInafectacion + "&periodo=" + strPeriodo + "&fecEnvio=" + strFechaEnvioCarta + 
		                        "&fecRec=" + strFechaRecepcionDocumentos + "&fecPre=" +strFechaPresentacionSat + "&fecNot=" + strFechaNotificacion + 
		                        "&nrores=" + nroResolucion + "&codestadores=" +  codEstadoResolucion + "&estado=" + estado + "&AnioFabricacion=" + strAnioFabricacion , 600, 240, function() { });
	}    
}






//****************************************************************
// Funcion		:: 	fn_Volver
// Descripción	::	Regresa a la pagina anterior
// Log			:: 	AEP - 24/09/2012
//****************************************************************
function fn_Volver() {
 
 	fn_mdl_confirma('¿Está seguro de volver?',
		function() {
			parent.fn_blockUI();
        fn_util_redirect('frmConsultasBienContrato.aspx?co=1&csc=' + $("#txtNumeroContrato").html());
		},
		"../../util/images/question.gif",
		function() {
		},
		'Consultas-Detalle Bien'
	);
	
    
}


//****************************************************************
// Funcion		:: 	fn_abreNuevoDocumentoComentario 
// Descripción	::	Abre Modal nuevo documento o comentario
// Log			:: 	AEP - 17/10/2012
//****************************************************************
function fn_abreNuevoDocumentoComentario() {
   
        var strCodigoContrato = $("#txtNumeroContrato").html();
	    var strSecFinanciamiento= $("#hidSecFinanciamiento").val();
        parent.fn_util_AbreModal("Mant. Bien :: Documentos y Comentarios", "Administracion/frmDocumentoComentarioBien.aspx?codcontrato=" + strCodigoContrato + "&scf=" + strSecFinanciamiento, 650, 320, function() { });
    
}

//****************************************************************
// Funcion		:: 	fn_abreEditarDocumentoComentario 
// Descripción	::	Abre Modal nuevo documento o comentario
// Log			:: 	AEP - 17/10/2012
//****************************************************************
function fn_abreEditarDocumentoComentario() {
   
        var strCodigoContrato = $("#hidNumeroContrato").val();
	    var id = $("#hidRowDocumento").val();
	    if(IsNullOrEmpty(id)) 
	    {
	    parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	    }else 
	    {
		var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id);
		var codigo = rowData.CODIGOBIENDOCUMENTO;
		var strobservacion = rowData.OBSERVACIONES;
	    var strEstadoBien=	rowData.ESTADODOCBIEN;
		var strnombrearchivo = rowData.NOMBREARCHIVO;
	    var strSecFinanciamiento = rowData.SECFINANCIAMIENTO;
	
        parent.fn_util_AbreModal("Consultas :: Documentos y Comentarios", "Consultas/DetalleBien/frmConsultasDocumentoComentarioBien.aspx?codcontrato=" + strCodigoContrato + "&codigo=" + codigo + "&obs=" + strobservacion + "&nomArchivo=" + strnombrearchivo + "&det=" + strEstadoBien + "&scf=" +strSecFinanciamiento , 650, 320, function() { });
	}
   
}



//****************************************************************
// Funcion		:: 	fn_cargaComboProvinciaInmueble
// Descripción	::	Cargar el combo provincia 
// Log			:: 	AEP - 28/09/2012
//****************************************************************
function fn_cargaComboProvinciaInmueble(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvinciaInmueble').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoInmueble();
	//fn_doResize();

}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistritoInmueble
// Descripción	::	
// Log			:: 	AEP - 28/09/2012

//****************************************************************

function fn_LimpiaComboDistritoInmueble() {
    $('#ddlDistritoInmueble').empty();
    $("#ddlDistritoInmueble").append('<option value="0">[-Seleccione-]</option>');
}

//****************************************************************
// Funcion		:: 	fn_cargaComboDistritoInmueble
// Descripción	::	
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_cargaComboDistritoInmueble(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistritoInmueble').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
//fn_doResize();
}


//****************************************************************
// Funcion		:: 	fn_cargaComboProvinciaMaquinaria
// Descripción	::	Cargar el combo provincia 
// Log			:: 	AEP - 02/10/2012
//****************************************************************
function fn_cargaComboProvinciaMaquinaria(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvinciaMaquinaria').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoMaquinaria();
	//fn_doResize();

}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistritoMaquinaria
// Descripción	::	
// Log			:: 	AEP - 02/10/2012

//****************************************************************

function fn_LimpiaComboDistritoMaquinaria() {
    $('#ddlDistritoMaquinaria').empty();
    $("#ddlDistritoMaquinaria").append('<option value="0">[-Seleccione-]</option>');
}

//****************************************************************
// Funcion		:: 	fn_cargaComboDistritoMaquinaria
// Descripción	::	
// Log			:: 	AEP - 02/10/2012
//****************************************************************
function fn_cargaComboDistritoMaquinaria(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistritoMaquinaria').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
//fn_doResize();
}


//****************************************************************
// Funcion		:: 	fn_cargaComboProvinciaVehiculo
// Descripción	::	Cargar el combo provincia 
// Log			:: 	AEP - 15/10/2012
//****************************************************************
function fn_cargaComboProvinciaVehiculo(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvinciaVehiculo').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoVehiculo();
	//fn_doResize();

}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistritoVehiculo
// Descripción	::	
// Log			:: 	AEP - 15/10/2012

//****************************************************************

function fn_LimpiaComboDistritoVehiculo() {
    $('#ddlDistritoVehiculo').empty();
    $("#ddlDistritoVehiculo").append('<option value="0">[-Seleccione-]</option>');
}

//****************************************************************
// Funcion		:: 	fn_cargaComboDistritoVehiculo
// Descripción	::	
// Log			:: 	AEP - 15/10/2012
//****************************************************************
function fn_cargaComboDistritoVehiculo(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistritoVehiculo').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
//fn_doResize();
}


//****************************************************************
// Funcion		:: 	fn_cargaComboProvinciaOtros
// Descripción	::	Cargar el combo provincia 
// Log			:: 	AEP - 17/10/2012
//****************************************************************
function fn_cargaComboProvinciaOtros(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvinciaOtros').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoOtros();
	//fn_doResize();

}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistritoOtros
// Descripción	::	
// Log			:: 	AEP - 17/10/2012

//****************************************************************

function fn_LimpiaComboDistritoOtros() {
    $('#ddlDistritoOtros').empty();
    $("#ddlDistritoOtros").append('<option value="0">[-Seleccione-]</option>');
}

//****************************************************************
// Funcion		:: 	fn_cargaComboDistritoOtros
// Descripción	::	
// Log			:: 	AEP - 15/10/2012
//****************************************************************
function fn_cargaComboDistritoOtros(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistritoOtros').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
//fn_doResize();
}

//****************************************************************
// Funcion		:: 	fn_Validacion
// Descripción	::	Valida Registro
// Log			:: 	AEP - 09/10/2012
//****************************************************************
function fn_Validacion(pError) {
    
	 //inmuebles
	 var cmbTipoBien = $('select[id=ddlTipoBien]');
	 var txtcantidadinmueble = $('input[id=txtCantidadInmueble]:text');
	 var txtDescripcionInmueble =    $('textarea[id=txtDescripcionInmueble]');
	 var txtUbicacion=    $('input[id=txtUbicacionInmueble]:text');
	 var txtUso=    $('input[id=txtUsoInmueble]:text');
	 var cmbEstadoBien = $('select[id=ddlEstadoBien]');
	 var cmbDepartamento = $('select[id=ddlDepartamentoInmueble]');
	 var cmbProvincia = $('select[id=ddlProvinciaInmueble]');
	 var cmbMoneda = $('select[id=ddlMonedaBien]');
	 
	//vehiculos
	 var cmbTipoBienVehiculo = $('select[id=ddlTipoBienVehiculo]');
	 var txtcantidadVehiculo = $('input[id=txtCantidadVehivulo]:text');
	 var txtDescripcionVehiculo =    $('textarea[id=txtDescripcionVehivulo]');
	 var txtUsoVehiculo =    $('input[id=txtUsoVehiculo]:text');
	 var txtUbicacionVehiculo=    $('input[id=txtDireccionVehiculo]:text');
	 var txtMarcaVehiculo=    $('input[id=txtMarcaVehivulo]:text');
	 var txtPlacaActualVehiculo=    $('input[id=txtPlacaActualVehivulo]:text');
	 var txtPlacaAnteriorVehiculo=    $('input[id=txtPlacaAnteriorVehivulo]:text');
	 var cmbEstadoBienVehiculo = $('select[id=ddlEstadoBienVehiculo]');
	 var cmbDepartamentoVehiculo = $('select[id=ddlDepartamentoVehiculo]');
	 var cmbProvinciaVehiculo = $('select[id=ddlProvinciaVehiculo]');
	 var cmbMonedaVehiculo = $('select[id=ddlMonedaVehiculo]');
	 var txtFechaBajaVehiculo =$('input[id=txtFechaBajaVehiculo]:text');	
	 
	// Maquinaria
	
	var cmbDepartamentoMaquinaria = $('select[id=ddlDepartamentoMaquinaria]');
	var cmbProvinciaMaquinaria = $('select[id=ddlProvinciaMaquinaria]');
	var cmbEstadoBienMaquinaria = $('select[id=ddlEstadoBienMaquinaria]');
	var cmbMonedaMaquinaria = $('select[id=ddlMonedaMaquinaria]');
	var txtDescripcionMaquinaria =    $('textarea[id=txtDescripcionMaquinaria]');
	var txtDireccionMaquinaria=    $('input[id=txtDireccionMaquinaria]:text');
	var txtUsoMaquinaria=    $('input[id=txtUsoMaquinaria]:text');
	var txtCantidadMaquinaria=    $('input[id=txtCantidadMaquinaria]:text');
	var txtSerieMaquinaria=    $('input[id=txtSerieMaquinaria]:text');
	var txtMarcaMaquinaria=    $('input[id=txtMarcaMaquinaria]:text');
	var txtModeloMaquinaria=    $('input[id=txtModeloMaquinaria]:text');
	var cmbTipoBienMaquinaria = $('select[id=ddlTipoBienMaquinaria]');
	
	
	// Otros
	var cmbBienOtros= $('select[id=ddlBienOtros]');
	var txtcantidadOtros= $('input[id=txtCantidadOtros]:text');
	var txtDescripcionOtros =    $('textarea[id=txtDescripcionOtros]');
	var txtUbicacionOtros=    $('input[id=txtUbicacionOtros]:text');
	var txtValorOtros=    $('input[id=txtValorBienOtros]:text');
	var txtUsoOtros=    $('input[id=txtUsoOtros]:text');
	var cmbTipoBienOtros = $('select[id=ddlTipoBienOtros]');
	var cmbDeparatamentoOtros = $('select[id=ddlDepartamentoOtros]');
	var cmbProvinciaOtros = $('select[id=ddlProvinciaOtros]');
	var cmbMonedaOtros = $('select[id=ddlMonedaOtros]');
	var txtFechaOtros=    $('input[id=txtFechaTransferenciaOtros]:text');
    var txtColorOtros=    $('input[id=txtColorOtros]:text');
	 var txtMarcaOtros=    $('input[id=txtMarcaOtros]:text');

	
//	 var txtOficina = $('input[id=txtOficinaRegistral]:text');
//	 var txtPartida1 = $('input[id=txtPartidaRegistral2]:text');
//	 var txtOficina1 = $('input[id=txtOficinaRegistral2]:text');

	// Inmueble
    if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) {
      pError.append(fn_util_ValidateControl(txtcantidadinmueble[0], 'una cantidad', 1, ''));
      pError.append(fn_util_ValidateControl(txtDescripcionInmueble[0], 'una descripción', 1, ''));
      pError.append(fn_util_ValidateControl(txtUbicacion[0], 'una ubicacion', 1, ''));
      pError.append(fn_util_ValidateControl(txtUso[0], 'uso', 1, ''));	
      pError.append(fn_util_ValidateControl(cmbEstadoBien[0], 'un estado del bien', 1, ''));	
      pError.append(fn_util_ValidateControl(cmbDepartamento[0], 'un departamento', 1, ''));
      pError.append(fn_util_ValidateControl(cmbProvincia[0], 'una provincia', 1, ''));
      pError.append(fn_util_ValidateControl(cmbTipoBien[0], 'un tipo de bien', 1, ''));
      pError.append(fn_util_ValidateControl(cmbMoneda[0], 'una moneda', 1, ''));
       $("div#divTabs").tabs("select", [0]);	
    }
    // Maquinaria
    else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) {
    pError.append(fn_util_ValidateControl(cmbDepartamentoMaquinaria[0], 'un departamento', 1, ''));
    pError.append(fn_util_ValidateControl(cmbProvinciaMaquinaria[0], 'una provincia', 1, ''));
    pError.append(fn_util_ValidateControl(cmbEstadoBienMaquinaria[0], 'un estado del bien', 1, ''));
    pError.append(fn_util_ValidateControl(cmbMonedaMaquinaria[0], 'una moneda', 1, ''));
    pError.append(fn_util_ValidateControl(cmbTipoBienMaquinaria[0], 'una Tipo de Bien', 1, ''));
    pError.append(fn_util_ValidateControl(txtUsoMaquinaria[0], 'uso', 1, ''));	
    pError.append(fn_util_ValidateControl(txtDescripcionMaquinaria[0], 'una descripción', 1, ''));
    pError.append(fn_util_ValidateControl(txtDireccionMaquinaria[0], 'una ubicacion', 1, ''));
    pError.append(fn_util_ValidateControl(txtCantidadMaquinaria[0], 'una cantidad', 1, ''));
    //pError.append(fn_util_ValidateControl(txtSerieMaquinaria[0], 'una serie', 1, ''));
    pError.append(fn_util_ValidateControl(txtMarcaMaquinaria[0], 'una marca', 1, ''));
    //pError.append(fn_util_ValidateControl(txtModeloMaquinaria[0], 'un modelo', 1, ''));
     $("div#divTabsM").tabs("select", [0]);	
	  	
    }else if(DestinoCredito_Vehiculo.indexOf($("#hidCodClasificacion").val())!=-1) {
      pError.append(fn_util_ValidateControl(txtcantidadVehiculo[0], 'una cantidad', 1, ''));
      pError.append(fn_util_ValidateControl(txtDescripcionVehiculo[0], 'una descripción', 1, ''));
      pError.append(fn_util_ValidateControl(txtMarcaVehiculo[0], 'una marca', 1, ''));	
      pError.append(fn_util_ValidateControl(txtUbicacionVehiculo[0], 'una ubicacion', 1, ''));
      pError.append(fn_util_ValidateControl(cmbEstadoBienVehiculo[0], 'un estado del bien', 1, ''));	
      pError.append(fn_util_ValidateControl(cmbDepartamentoVehiculo[0], 'un departamento', 1, ''));
      pError.append(fn_util_ValidateControl(cmbProvinciaVehiculo[0], 'una provincia', 1, ''));
      pError.append(fn_util_ValidateControl(cmbTipoBienVehiculo[0], 'un tipo de bien', 1, ''));
      pError.append(fn_util_ValidateControl(cmbMonedaVehiculo[0], 'una moneda', 1, ''));
      pError.append(fn_util_ValidateControl(txtUsoVehiculo[0], 'uso', 1, ''));	
    	if (($("#txtPlacaAnteriorVehivulo").val()!="") && ($("#txtPlacaActualVehivulo").val()=="")) {
    	pError.append(fn_util_ValidateControl(txtPlacaActualVehiculo[0], 'placa actual', 1, ''));
    	}else {
    	$("#txtPlacaActualVehivulo").addClass('css_input').removeClass('css_input_error');
 }
 $("div#divTabsV").tabs("select", [0]);
    	
    	// Otros
    }else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) {
    	 $("div#divTabsS").tabs("select", [0]);
      pError.append(fn_util_ValidateControl(cmbBienOtros[0], 'un estado de bien', 1, ''));
      pError.append(fn_util_ValidateControl(txtcantidadOtros[0], 'una cantidad', 1, ''));
      pError.append(fn_util_ValidateControl(txtDescripcionOtros[0], 'una descripción', 1, ''));
      pError.append(fn_util_ValidateControl(txtUbicacionOtros[0], 'una ubicación', 1, ''));
      pError.append(fn_util_ValidateControl(txtMarcaOtros[0], 'una marca', 1, ''));
      pError.append(fn_util_ValidateControl(cmbDeparatamentoOtros[0], 'un departamento', 1, ''));
      pError.append(fn_util_ValidateControl(cmbTipoBienOtros[0], 'un tipo de bien', 1, ''));
      pError.append(fn_util_ValidateControl(cmbProvinciaOtros[0], 'una provincia', 1, ''));
      pError.append(fn_util_ValidateControl(cmbMonedaOtros[0], 'una moneda', 1, ''));
      pError.append(fn_util_ValidateControl(cmbMonedaOtros[0], 'una marca', 1, ''));
	pError.append(fn_util_ValidateControl(txtUsoOtros[0], 'uso', 1, ''));	
  	
    }
  
	
                var objtxtFechaRegistral = $('input[id=txtFechaInscripcionRegistralInmueble]:text');
	            var objtxtFechaMunicipal = $('input[id=txtFechaInscripcionMunicipalInmueble]:text');
	            
	            var objtxtFechaRegistralV = $('input[id=txtFechaInscripcionRegistralVehivulo]:text');
	            var objtxtFechaMunicipalV = $('input[id=txtFechaInscripcionMunicipalVehivulo]:text');
	            
	            if(($("#ddlEstadoInscripcionRRPPInmueble").val()=='001') &&  ($("#txtFechaInscripcionRegistralInmueble").val()=='')) {
	            pError.append(fn_util_ValidateControl(objtxtFechaRegistral[0], 'la fecha de inscripción registral', 1, ''));
	            	$("div#divTabs").tabs("select", [1]);
	            }
	            if(($("#ddlEstadoMunicipalInmueble").val()=='001') && ($("#txtFechaInscripcionMunicipalInmueble").val()=='')) {
	             pError.append(fn_util_ValidateControl(objtxtFechaMunicipal[0], 'la fecha de inscripción municipal ', 1, ''));
	            	$("div#divTabs").tabs("select", [1]);
	             }
	             
	               if(($("#ddlEstadoInscripcionRRPPVehiculo").val()=='001') &&  ($("#txtFechaInscripcionRegistralVehivulo").val()=='')) {
	            pError.append(fn_util_ValidateControl(objtxtFechaRegistralV[0], 'la fecha de inscripción registral', 1, ''));
	            	$("div#divTabsV").tabs("select", [1]);
	            }
	            if(($("#ddlEstadoMunicipalVehiculo").val()=='001') && ($("#txtFechaInscripcionMunicipalVehivulo").val()=='')) {
	             pError.append(fn_util_ValidateControl(objtxtFechaMunicipalV[0], 'la fecha de inscripción municipal ', 1, ''));
	            	$("div#divTabsV").tabs("select", [1]);
	             }
	//IBK Inicio JJM
    if (DestinoCredito_Vehiculo.indexOf($("#hidCodClasificacion").val()) != -1) {

        var sFechaBaja = new Date($("#txtFechaBajaVehiculo").val());
        var sFechaInsMunicipal = new Date($("#txtFechaInscripcionMunicipalVehivulo").val());
        var iTotal = 0; //Ahora

        var sFechaPropiedad = new Date($("#txtFechaPropiedadVehivulo").val());
        var sFechaTransferencia = new Date($("#txtFechaTransferenciaVehivulo").val());
        //Ahora
        iTotal = fn_ValidaPeriodo();
        
//        var sPrecioVenta = $("#hidPrecioVenta").val(); //Valor Bien Leasing
//        var sTotal = $("#hidTotal").val(); //Total Valor bien Grid
//        var sTotalImporte = "";


//        sTotalImporte = (fn_util_ValidaDecimal(sTotal) + fn_util_ValidaDecimal($("#txtValorVehivulo").val()));


        //if (sTotalImporte > sPrecioVenta) {
         //   pError.append('<br/> El Valor del Bien No debe ser Mayor al Total Valor Leasing.  ');
        //    $("div#divTabsV").tabs("select", [1]);
       // }
        
        if (sFechaBaja < sFechaInsMunicipal) {
            pError.append("<br/> La Fecha de Baja No Puede ser Menor a la Fecha de Inscripción Municipal.  ");
            $("div#divTabsV").tabs("select", [1]);

        }

        if (sFechaPropiedad < sFechaTransferencia) {
            pError.append("<br/> La Fecha de Propiedad No Puede ser Menor a la Fecha de Transferencia.  ");
            $("div#divTabsV").tabs("select", [1]);
        }
        
        //Ahora
        if (iTotal.toString() != '0') 
        {
            pError.append("<br/> El período ingresado es incorrecto, el correcto debe ser mayor o igual al año de fabricación y no mayor a 3 años de este. ");
            $("div#divTabsV").tabs("select", [2]);
        }
    }
   
    //IBK Fin JJM

    //debugger;
    //Inicio IBK -RPH
    var sPrecioVenta = "";
    var sTotal = "";
    var sTotalImporte = "";

    if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) {
        sPrecioVenta = $("#hidPrecioVenta").val(); //Valor Bien Leasing
        sTotal = $("#hidTotal").val(); //Total Valor bien Grid

        sTotalImporte = (fn_util_ValidaDecimal(sTotal) + fn_util_ValidaDecimal($("#txtValorInmueble").val()));
        
            //importe
        if (sTotalImporte > sPrecioVenta) {
            pError.append('<br/> El Valor del Bien No debe exceder al Total Valor Leasing. ');
        }
    }
    else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) {
        sPrecioVenta = $("#hidPrecioVenta").val(); //Valor Bien Leasing
        sTotal = $("#hidTotal").val(); //Total Valor bien Grid

        sTotalImporte = (fn_util_ValidaDecimal(sTotal) + fn_util_ValidaDecimal($("#txtValorBienMaquinaria").val()));

        if (sTotalImporte > sPrecioVenta) {
            pError.append('<br/> El Valor del Bien No debe exceder al Total Valor Leasing. ');
        }
    }
    else if (DestinoCredito_Vehiculo.indexOf($("#hidCodClasificacion").val()) != -1) {
        sPrecioVenta = $("#hidPrecioVenta").val(); //Valor Bien Leasing
        sTotal = $("#hidTotal").val(); //Total Valor bien Grid

        sTotalImporte = (fn_util_ValidaDecimal(sTotal) + fn_util_ValidaDecimal($("#txtValorVehivulo").val()));

        if (sTotalImporte > sPrecioVenta) {
            pError.append('<br/> El Valor del Bien No debe exceder al Total Valor Leasing. ');
        }
    }
    else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) {
        var sPrecioVenta = $("#hidPrecioVenta").val(); //Valor Bien Leasing
        var sTotal = $("#hidTotal").val(); //Total Valor bien Grid

        sTotalImporte = (fn_util_ValidaDecimal(sTotal) + fn_util_ValidaDecimal($("#txtValorBienOtros").val()));

        if (sTotalImporte > sPrecioVenta) {
            pError.append('<br/> El Valor del Bien No debe exceder al Total Valor Leasing. ');
        }
    }
    //Fin
   
    return pError.toString();
}

// IBK JJM
function fn_Back() {
    var objinthidSecFinanciamiento = $("#hidSecFinanciamiento").val();
    //alert(objinthidSecFinanciamiento);
    objinthidSecFinanciamiento = parseInt(objinthidSecFinanciamiento) - 1;
    //alert(objinthidSecFinanciamiento);
    if ($("#hidSecFinanciamiento").val() == '2') {
        //Inhabilitar el boton cuando no haya mas registros para retroceder
        //alert('-1');
        $('#hBack').attr('disabled', true);
        $('#hBack').attr('disabled', 'disabled');
    }
    else {
        //alert('frmConsultasBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + $("#hidSecFinanciamiento").val() + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#HidCodEstado").val());
        $('#hBack').attr('enabled', true);
        $('#hBack').attr('enabled', 'enabled');

        //alert($("#hidPrecioVenta").val());
        fn_util_redirect('frmConsultasBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + objinthidSecFinanciamiento + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#HidCodEstado").val() + "&precioventa=" + $("#hidPrecioVenta").val());
    }
}
function fn_Next() {
    var objinthidSecFinanciamiento = $("#hidSecFinanciamiento").val();
    //alert(objinthidSecFinanciamiento);
    objinthidSecFinanciamiento = parseInt(objinthidSecFinanciamiento) + 1;
    //alert(objinthidSecFinanciamiento);
    var objinthidMaxSecFinanciamiento = $("#hidMaxSecFinanciamiento").val();
    //alert(objinthidMaxSecFinanciamiento);
    if (objinthidSecFinanciamiento > objinthidMaxSecFinanciamiento) {
        //Inhabilitar el boton cuando no haya mas registros para avanzar
        //alert('+1');

        $('#hNext').attr('disabled', true);
        $('#hNext').attr('disabled', 'disabled');
    }
    else {
        //alert(objinthidSecFinanciamiento[0]);
        $('#hNext').attr('enabled', true);
        $('#hNext').attr('enabled', 'enabled');

        //alert($("#hidPrecioVenta").val());
        fn_util_redirect('frmConsultasBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + objinthidSecFinanciamiento + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#HidCodEstado").val() + "&precioventa=" + $("#hidPrecioVenta").val());        
    }
}


// function fn_ValidaPeriodofn_ValidaPeriodo() {
function fn_ValidaPeriodo() {






    //debugger;
    var rows = jQuery("#jqGrid_lista_B").jqGrid('getRowData');
    var actual = new Date();
    var anio = actual.getUTCFullYear();
    var sFabricacion = $("#txtAnioVehivulo").val();
    var total = 0;
    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        var sPerido = row.Periodo;
        var aniFab = sPerido - sFabricacion;
        if (sPerido < anio) {
            total = total + 1;










        }
        if (sPerido > anio) {
            if (aniFab > 3) {
                total = total + 1;




            }

 }



    }
    return total.toString();
}
//Fin IBK JJM
//Fin IBK JJM

//Fin IBK JJM

/*
// IBK JJM
function fn_Back() {
    var objinthidSecFinanciamiento = $("#hidSecFinanciamiento").val();
    //alert(objinthidSecFinanciamiento);
    //objinthidSecFinanciamiento = parseInt(objinthidSecFinanciamiento[0]) - 1;

    //alert("Hola");

    //debugger;
    objinthidSecFinanciamiento = objinthidSecFinanciamiento - 1;
    
    //alert(objinthidSecFinanciamiento);
    if ($("#hidSecFinanciamiento").val() == '2') {
        //Inhabilitar el boton cuando no haya mas registros para retroceder
        //alert('1');
        //$('#hBack').attr('disabled', true);  dv_img_botonBck
        //$('input[name=hBack]').attr('disabled', 'disabled');
        $('#hNext').attr('disabled', true);
        $('#hNext').attr('disabled', 'disabled');
    }
    else {
        //alert('frmConsultasBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + $("#hidSecFinanciamiento").val() + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#HidCodEstado").val());
        fn_util_redirect('frmConsultasBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + objinthidSecFinanciamiento + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#HidCodEstado").val());
    }
}
function fn_Next() {


    //alert("CHAO");
    var objinthidSecFinanciamiento = $("#hidSecFinanciamiento").val();
    //alert(objinthidSecFinanciamiento);
    
    //objinthidSecFinanciamiento = parseInt(objinthidSecFinanciamiento[0]) + 1;
    objinthidSecFinanciamiento = objinthidSecFinanciamiento + 1;
    
    //alert(objinthidSecFinanciamiento);
    var objinthidMaxSecFinanciamiento = $("#hidMaxSecFinanciamiento").val();
    alert(objinthidMaxSecFinanciamiento);
    if (objinthidSecFinanciamiento > objinthidMaxSecFinanciamiento) {
        //Inhabilitar el boton cuando no haya mas registros para avanzar
        //alert('1');

        $('#hBack').attr('disabled', true);

        $('#hBack').attr('disabled', 'disabled');
    }
    else {
        //alert(objinthidSecFinanciamiento[0]);
        fn_util_redirect('frmConsultasBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + objinthidSecFinanciamiento + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#HidCodEstado").val());
    }
}
//Fin IBK JJM
*/