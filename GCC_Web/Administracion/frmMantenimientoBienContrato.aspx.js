var DestinoCredito_Inmueble = ["002"];
var DestinoCredito_Maquinaria = ["003", "004", "005"];
var DestinoCredito_Otros = ["007", "008"];
var DestinoCredito_Vehiculo = ["006"];

var blnPrimeraBusqueda;
var intPaginaActual = 1;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	AEP - 20/09/2012
//****************************************************************
$(document).ready(function() {
 
     // Valida Tabs
    $("div#divTabs").tabs({
        show: function() {
            fn_doResize();
        }
    });
	
	   $("div#divTabsM").tabs({
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

	// Remover Select de estados del bien
	
	$("#ddlEstadoBienMaquinariaOtros option[value='0']").remove();
	$("#ddlEstadoBienUnidadTransporte option[value='0']").remove();
    $("#ddlEstadoBienInmueble option[value='0']").remove();
	$("#ddlEstadoBienSistemas option[value='0']").remove();
	
	 fn_InicializarCampos();
     fn_configurar_PanelesBienes();
    //    fn_cargarTipoBien();
    fn_cargaGrillaInmuebles();
	fn_cargaGrillaDOCUMENTOS();
	//JJM
    fn_cargaGrillaDocumentosMaquina();
    fn_cargaGrillaDocumentosTransporte();
    fn_cargaGrillaDocumentosSistema();
    //JJM
	fn_cargaGrillaMaquinarias();
	fn_cargaGrillaUnidadTransporte();
	fn_cargaGrillaSistemasOtros();
	ValidarEstadosBien($("#ddlEstadoBienInmueble").val());
	ValidarEstadosMaquinaria($("#ddlEstadoBienMaquinaria").val());
    //    fn_consultar();
    //	fn_SeteaUbigeo();
    //	fn_SeteaUbigeoBien();
    //	fn_SeteaUbigeoOtro();
    fn_InicializarCampos();

	$("#ddlEstadoInscripcionMunicipal").change(function() {
	if ($(this).val()=='001') {
	 $("#txtFechaInscripcionMunicipal").attr('disabled', false);
	}else {
	 $("#txtFechaInscripcionMunicipal").attr('disabled', 'disabled');
		$("#txtFechaInscripcionMunicipal").val('');	
	$("#txtFechaInscripcionMunicipal").addClass('css_calendario').removeClass('css_input_error');
            
	}
	});
	
	$("#ddlEstadoInscripcionRRPP").change(function() {
	if ($(this).val()=='001') {
	 $("#txtFechaInscripcionRegistral").attr('disabled', false);  
	}else {
	 $("#txtFechaInscripcionRegistral").attr('disabled', 'disabled');
		$("#txtFechaInscripcionRegistral").val('');	
		$("#txtFechaInscripcionRegistral").addClass('css_calendario').removeClass('css_input_error');		
	}
	});
	
	  $("#txtObservacionContrato").focusout(function() {
	    	fn_util_ReplaceAll($("#txtObservacionContrato").val(), "'", "");
	        fn_util_ReplaceAll($("#txtObservacionContrato").val(), "\"", "");
	  	//alert(($("#txtObservacionContrato").val()));
	    });
	


    //On load Page (siempre al final)
    fn_onLoadPage();
});

//************************************************************
// Función		:: 	fn_InicializarCampos
// Descripcion 	:: 	Inicializa los campos
// Log			:: 	AEP - 27/09/2012
//************************************************************

//function fn_InicializarCampos() {
//	
//	
//}

//************************************************************
// Función		:: 	fn_ValidarInscripcionMunicipal
// Descripcion 	:: 	Valida que se ingrese la fecha para poder activar el estado
// Log			:: 	AEP - 21/10/2012
//************************************************************
function fn_ValidarInscripcionMunicipal() {
	
	     	if($("#txtFechaInscripcionMunicipal").val()=='') {
		$("#ddlEstadoInscripcionMunicipal").attr('disabled', 'disabled');
	}else {
	     $("#ddlEstadoInscripcionMunicipal").attr('disabled', false);	
	     	}
}

//function fn_LimpiarCaracteresEspeciales(val) {
//	alert("joven iguishh!!!");
//	alert(val);
//	fn_util_ReplaceAll(val, "'", "");
//	fn_util_ReplaceAll(val, "\"", "");
//}

//************************************************************
// Función		:: 	fn_ValidarInscripcionRegistral
// Descripcion 	:: 	Valida que se ingrese la fecha para poder activar el estado
// Log			:: 	AEP - 21/10/2012
//************************************************************
function fn_ValidarInscripcionRegistral() {
	
	     	if($("#txtFechaInscripcionRegistral").val()=='') {
		$("#ddlEstadoInscripcionRRPP").attr('disabled', 'disabled');
	}else {
	     $("#ddlEstadoInscripcionRRPP").attr('disabled', false);	
	     	}
}

//************************************************************
// Función		:: 	fn_configurar_PanelesBienes
// Descripcion 	:: 	Configura las distintas ventanas de mantenimiento de los bienes
// Log			:: 	AEP - 26/09/2012
//************************************************************
function fn_configurar_PanelesBienes() {

    $("#dv_bienesinmuebles").hide();
    $("#dv_Maquinarias").hide();
    $("#dv_UnidadTransporte").hide();
	$("#dv_SistemasOTros").hide();


    // Inmueble
    if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_bienesinmuebles").show(); }
    // Maquinaria
    else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_Maquinarias").show(); }
	// Vehivulo
	else if (DestinoCredito_Vehiculo.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_UnidadTransporte").show(); }
    // Otros
    else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_SistemasOTros").show(); }
}

//************************************************************
// Función		:: 	fn_cargarTipoBien
// Descripcion 	:: 	Carga el combo de tipo de bien
// Log			:: 	WCR - 15/05/2012
//************************************************************
function fn_cargarTipoBien() {
    var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL104", "pstrCodigoGenerico", $("#hidCodClasificacion").val()];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            // Inmueble
            if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) { $('#cmbTipoBien').html(arrResultado[1]); $('#cmbTipoBien').val($('#hidCodTipoBien').val()); }
            // Maquinaria
            else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) { $('#cmbTipoBien1').html(arrResultado[1]); $('#cmbTipoBien1').val($('#hidCodTipoBien').val()); }
            // Otros
            else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) { $('#cmbTipoBien2').html(arrResultado[1]); $('#cmbTipoBien2').val($('#hidCodTipoBien').val()); }
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
}



//****************************************************************
// Funcion		:: 	fn_cargaGrillaInmuebles
// Descripción	::	Inicializa Grilla de Bienes e Inmuebles
// Log			:: 	AEP - 20/09/2012
//****************************************************************
function fn_cargaGrillaInmuebles() {
//IBK - RPH agrego la columna Id para obtenerlo en el paginado cuando no se seleccione


    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_buscarInmuebles();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['CodigoContrato', 'Departamento', 'Provincia', 'Distrito', 'Ubicación', 'Código del Predio', 'Estado Inscripción Municipal', 'Estado Inscripción Registral', 'Propiedad', 'Estado del Registro del Bien', 'Fecha de Baja', 'Comentario', '', '', '', '', '', '', '', '', '', ''],
        colModel: [
			{ name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
			{ name: 'DepartamentoNombre', index: 'DepartamentoNombre', width: 100, align: "center" },
			{ name: 'ProvinciaNombre', index: 'ProvinciaNombre', width: 100, align: "center" },
			{ name: 'DistritoNombre', index: 'DistritoNombre', width: 100, align: "center" },
			{ name: 'Ubicacion', index: 'Ubicacion', width: 100, align: "left" },
			{ name: 'codigopredio', index: 'codigopredio', width: 100, align: "left" },
			{ name: 'EstadoMunicipal', index: 'EstadoMunicipal', width: 100, align: "left" },
			{ name: 'EstadoInscripcionRRPP', index: 'EstadoInscripcionRRPP', width: 100, align: "left" },
			{ name: 'EstadoTransferencia', index: 'EstadoTransferencia', width: 100, align: "left" },
			{ name: 'EstadoBienLogico', index: 'EstadoBienLogico', width: 100, align: "left" },
			{ name: 'FechaBaja', index: 'FechaBaja', width: 100, align: "left" },
			{ name: 'ComentarioBaja', index: 'ComentarioBaja', width: 150, align: "center", sortable: false, formatter: Lupa2 },
			{ name: 'CodEstadoBien', index: 'CodEstadoBien', hidden: true },
			{ name: 'Departamento', index: 'Departamento', hidden: true },
			{ name: 'Provincia', index: 'Provincia', hidden: true },
			{ name: 'Distrito', index: 'Distrito', hidden: true },
			{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
			{ name: 'CodEstadoInscripcionRRPP', index: 'CodEstadoInscripcionRRPP', hidden: true },
			{ name: 'CodEstadoMunicipal', index: 'CodEstadoMunicipal', hidden: true },
			{ name: 'CodEstadoTransferencia', index: 'CodEstadoTransferencia', hidden: true },
			{ name: 'FlagOrigen', index: 'FlagOrigen', hidden: true },
			{ name: 'Id', index: 'Id', hidden: true }
		],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        altclass: 'gridAltClass',
        //multiselect: true,
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddRowId").val(id);
            $("#hidNumeroContrato").val(rowData.CodSolicitudCredito);
            $("#hidSecFinanciamiento").val(rowData.SecFinanciamiento);
            $("#hidFlagOrigen").val(rowData.FlagOrigen);
            $("#hidCodEstado").val(rowData.CodEstadoBien);
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);


            //fn_util_redirect('frmMantenimientoBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + $("#hidSecFinanciamiento").val() + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#hidCodEstado").val());
            //IBK - RPH precio venta leasing
            
            fn_util_redirect('frmMantenimientoBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + $("#hidSecFinanciamiento").val() + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#hidCodEstado").val() + "&precioventa=" + $("#hidPrecioVenta").val());
            //fin
        }
    }).navGrid('#jqGrid_pager_A', { edit: false, add: false, del: false });
       $("#search_jqGrid_lista_A").hide();
	
	
    function Lupa2(cellvalue, options, rowObject) {
           
       if (rowObject.ComentarioBaja == "") {
           return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
       } else {
          var sScript2 = "javascript:VerObservaciones(" + rowObject.CodSolicitudCredito + "," + rowObject.SecFinanciamiento + ");";
       	  return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
       }
    	
    };

	
}
	
function VerObservaciones(strcodContrato,strsecfinanciamiento) {
    var sTitulo = "Gestión del Bien";
    var sSubTitulo = "Mant. Bien :: Observación de Baja  ";
    parent.fn_util_AbreModal(sSubTitulo, "Administracion/frmComentarioEliminacion.aspx?ccf=" + strcodContrato + "&csf=" + strsecfinanciamiento + "&Add=true", 500, 250, function() { });
}

//****************************************************************
// Funcion		:: 	fn_buscarInmuebles
// Descripción	::	
// Log			:: 	AEP - 27/09/2012
//****************************************************************
	
function fn_buscarInmuebles() {
	    $("#hddRowId").val('');
	    var vNumeroContrato = $('#txtNumeroContrato').val() == undefined ? "" : $('#txtNumeroContrato').val();
		var vEstadoLogico= $('#ddlEstadoBienInmueble').val() == undefined ? "" : $('#ddlEstadoBienInmueble').val();
	
    	 var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", intPaginaActual,    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
                             "pNumeroContraro", vNumeroContrato,
                             "pCodEstadoLogico", vEstadoLogico
                            ];
		

    fn_util_AjaxWM("frmMantenimientoBienContrato.aspx/ListaBienContratoInmuebles",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_A.addJSONData(jsondata);
                    	fn_doResize();
                    },
                    function(request) {
                        fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                    }
                   );
	}

//****************************************************************
// Funcion		:: 	fn_cargaGrillaMaquinarias
// Descripción	::	Inicializa Grilla de Maquinarias
// Log			:: 	AEP - 24/09/2012
//****************************************************************
function fn_cargaGrillaMaquinarias() {

	$("#jqGrid_lista_B").jqGrid({
		datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_B", "page");	 
            fn_buscarMaquinarias();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['CodigoContrato','Departamento','Provincia','Distrito', 'Nro. Serie','Nro. Motor','Marca','Modelo','Placa Actual','Año', 'Estado del Registro del Bien', 'Fecha de Baja','Comentario','','','','','','',''],
		colModel: [
			{ name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
			{ name: 'DepartamentoNombre', index: 'DepartamentoNombre', width: 100, align: "center" },
			{ name: 'ProvinciaNombre', index: 'ProvinciaNombre', width: 100, align: "center" },
			{ name: 'DistritoNombre', index: 'DistritoNombre', width: 100, align: "center" },
			{ name: 'NroSerie', index: 'NroSerie', width: 100, align: "left" },
			{ name: 'NroMotor', index: 'NroMotor', width: 100, align: "left" },
			{ name: 'Marca', index: 'Marca', width: 100, align: "left" },
			{ name: 'Modelo', index: 'Modelo', width: 100, align: "left" },
			{ name: 'Placa', index: 'Placa', width: 100, align: "left" },
			{ name: 'Anio', index: 'Anio', width: 100, align: "left" },
			{ name: 'EstadoBienLogico', index: 'EstadoBienLogico', width: 100, align: "left" },
			//{ name: 'FechaBaja', index: 'FechaBaja', width: 100, align: "left",formatter: fn_util_ValidaStringFecha},
			{ name: 'FechaBaja', index: 'FechaBaja', width: 100, align: "left"},
			{ name: 'ComentarioBaja', index: 'ComentarioBaja', width: 100, align: "center",sortable:false, formatter:Lupa2},
//			{ name: 'EstadoBienLogico', index: 'EstadoBienLogico', width: 100, align: "left" },
			{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true},
			{ name: 'CodEstadoBien', index: 'CodEstadoBien', hidden: true},
		    { name: 'Departamento', index: 'Departamento', hidden: true },
			{ name: 'Provincia', index: 'Provincia', hidden: true},
			{ name: 'Distrito', index: 'Distrito', hidden: true},
			{ name: 'FlagOrigen', index: 'FlagOrigen', hidden: true },
			{ name: 'Id', index: 'Id', hidden: true}
		],
		height: '100%',
		pager: '#jqGrid_pager_B',
		loadtext: 'Cargando datos...',
		emptyrecords: 'No hay resultados',
		rowNum: 10,
		rowList: [10, 20, 30],
		sortname: 'CodSolicitudCredito',
		sortorder: 'desc',
		viewrecords: true,
		gridview: true,
		autowidth: true,
		altRows: true,
		altclass: 'gridAltClass',
		//multiselect: true,
    onSelectRow: function(id) {
        var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
    	$("#hddRowId").val(id);
    	$("#hidNumeroContrato").val(rowData.CodSolicitudCredito);
        $("#hidSecFinanciamiento").val(rowData.SecFinanciamiento);
    	$("#hidFlagOrigen").val(rowData.FlagOrigen);
    	$("#hidCodEstado").val(rowData.CodEstadoBien);

    },
   	ondblClickRow: function(id) {
   	parent.fn_blockUI();

   	//fn_util_redirect('frmMantenimientoBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + $("#hidSecFinanciamiento").val() + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#hidCodEstado").val());
   	//IBK - RPH precio venta leasing
   	fn_util_redirect('frmMantenimientoBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + $("#hidSecFinanciamiento").val() + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#hidCodEstado").val() + "&precioventa=" + $("#hidPrecioVenta").val());
   	//fin
	}     
    
	}).navGrid('#jqGrid_pager_B', { edit: false, add: false, del: false });
      $("#search_jqGrid_lista_B").hide();
	
	    function Lupa2(cellvalue, options, rowObject) {
           
       if (rowObject.ComentarioBaja == "") {
           return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
       } else {
          var sScript2 = "javascript:VerObservaciones(" + rowObject.CodSolicitudCredito + "," + rowObject.SecFinanciamiento + ");";
       	  return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
       }
    	
    };
	
}

//****************************************************************
// Funcion		:: 	fn_buscarMaquinarias
// Descripción	::	
// Log			:: 	AEP - 27/09/2012
//****************************************************************
	
function fn_buscarMaquinarias() {
	
	    var vNumeroContrato = $('#txtNumeroContrato').val() == undefined ? "" : $('#txtNumeroContrato').val();
		var vEstadoLogico= $('#ddlEstadoBienMaquinariaOtros').val() == undefined ? "" : $('#ddlEstadoBienMaquinariaOtros').val();
	
    	 var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_B", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", intPaginaActual,    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_B", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_B", "sortorder"), // Criterio de ordenación.
                             "pNumeroContraro", vNumeroContrato,
                             "pCodEstadoLogico", vEstadoLogico
                            ];
		

    fn_util_AjaxWM("frmMantenimientoBienContrato.aspx/ListadoBienContratoMaquinaria",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_B.addJSONData(jsondata);
                    	fn_doResize();
                    },
                    function(request) {
                        fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                    }
                   );
	}

//****************************************************************
// Funcion		:: 	fn_cargaGrillaUnidadTransporte
// Descripción	::	Inicializa Grilla de Unidad y Transporte
// Log			:: 	AEP - 24/09/2012
//****************************************************************
function fn_cargaGrillaUnidadTransporte() {


	$("#jqGrid_lista_C").jqGrid({
		datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_C", "page");	 
            ListaBienContratoVehiculos();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
	    colNames: ['CodigoContrato', 'Nro. Serie','Motor','Marca', 'Placa Actual','Año','Descripción', 'Estado del Registro del Bien', 'Fecha de Baja','Comentario','','','','',''],
		colModel: [
			{ name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
			{ name: 'NroSerie', index: 'NroSerie', width: 100, align: "center" },
			{ name: 'NroMotor', index: 'NroMotor', width: 100, align: "center" },
			{ name: 'Marca', index: 'Marca', width: 100, align: "center" },
			{ name: 'Placa', index: 'Placa', width: 100, align: "left" },
			{ name: 'Anio', index: 'Anio', width: 100, align: "left" },
			{ name: 'Comentario', index: 'Comentario', width: 100, align: "left" },
			{ name: 'EstadoBienLogico', index: 'EstadoBienLogico', width: 100, align: "left" },
			{ name: 'FechaBaja', index: 'FechaBaja', width: 100, align: "left" },
			{ name: 'ComentarioBaja', index: 'ComentarioBaja', width: 150, align: "center",sortable:false,formatter:Lupa2},
			{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true},
			{ name: 'CodEstadoBien', index: 'CodEstadoBien', hidden: true},
			{ name: 'FlagOrigen', index: 'FlagOrigen', hidden: true },
			{ name: 'ValorBien', index: 'ValorBien', hidden: true },
			{ name: 'Id', index: 'Id', hidden: true }
			
		],
		height: '100%',
		loadtext: 'Cargando datos...',
		pager: '#jqGrid_pager_C',
		emptyrecords: 'No hay resultados',
		rowNum: 10,
		rowList: [10, 20, 30],
		sortname: 'CodSolicitudCredito',
		sortorder: 'desc',
		viewrecords: true,
		gridview: true,
		autowidth: true,
		altRows: true,
		altclass: 'gridAltClass',
		//multiselect: true,
    onSelectRow: function(id) {
        var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id);
        $("#hddRowId").val(id);
    	$("#hidNumeroContrato").val(rowData.CodSolicitudCredito);
        $("#hidSecFinanciamiento").val(rowData.SecFinanciamiento);
    	$("#hidFlagOrigen").val(rowData.FlagOrigen);
    	$("#hidCodEstado").val(rowData.CodEstadoBien);
    },
   	ondblClickRow: function(id) {
	    parent.fn_blockUI();
		var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id);
		//fn_util_redirect('frmMantenimientoBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + $("#hidSecFinanciamiento").val() + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#hidCodEstado").val());
		//IBK - RPH precio venta leasing
		fn_util_redirect('frmMantenimientoBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + $("#hidSecFinanciamiento").val() + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#hidCodEstado").val() + "&precioventa=" + $("#hidPrecioVenta").val());
		//fin
	}     
	}).navGrid('#jqGrid_pager_C', { edit: false, add: false, del: false });
      $("#search_jqGrid_lista_C").hide();
	
     function Lupa2(cellvalue, options, rowObject) {
           
       if (rowObject.ComentarioBaja == "") {
           return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
       } else {
          var sScript2 = "javascript:VerObservaciones(" + rowObject.CodSolicitudCredito + "," + rowObject.SecFinanciamiento + ");";
       	  return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
       }
    	
    };
	
}


//****************************************************************
// Funcion		:: 	ListaBienContratoVehiculos
// Descripción	::	
// Log			:: 	AEP - 27/09/2012
//****************************************************************
	
function ListaBienContratoVehiculos() {
	
	    var vNumeroContrato = $('#txtNumeroContrato').val() == undefined ? "" : $('#txtNumeroContrato').val();
		var vEstadoLogico= $('#ddlEstadoBienUnidadTransporte').val() == undefined ? "" : $('#ddlEstadoBienUnidadTransporte').val();
	
    	 var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_C", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", intPaginaActual,    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_C", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_C", "sortorder"), // Criterio de ordenación.
                             "pNumeroContraro", vNumeroContrato,
                             "pCodEstadoLogico", vEstadoLogico
                            ];
		

    fn_util_AjaxWM("frmMantenimientoBienContrato.aspx/ListaBienContratoVehiculos",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_C.addJSONData(jsondata);
                    	fn_doResize();
                    },
                    function(request) {
                        fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                    }
                   );
	}



//****************************************************************
// Funcion		:: 	fn_cargaGrillaSistemasOtros
// Descripción	::	Inicializa Grilla de Sistemas, otros
// Log			:: 	AEP - 03/10/2012
//****************************************************************
function fn_cargaGrillaSistemasOtros() {

	$("#jqGrid_lista_D").jqGrid({
		datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_D", "page");	 
            fn_buscarSistemasOtros();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['CodigoContrato','Departamento','Provincia','Distrito', 'Nro. Serie','Marca','Modelo', 'Estado del Registro del Bien', 'Fecha de Baja','Comentario','','','','','','',''],
		colModel: [
			{ name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
			{ name: 'DepartamentoNombre', index: 'DepartamentoNombre', width: 100, align: "center" },
			{ name: 'ProvinciaNombre', index: 'ProvinciaNombre', width: 100, align: "center" },
			{ name: 'DistritoNombre', index: 'DistritoNombre', width: 100, align: "center" },
			{ name: 'NroSerie', index: 'NroSerie', width: 100, align: "left" },
			{ name: 'Marca', index: 'Marca', width: 100, align: "left" },
			{ name: 'Modelo', index: 'Modelo', width: 100, align: "left" },
			{ name: 'EstadoBienLogico', index: 'EstadoBienLogico', width: 100, align: "left" },
			{ name: 'FechaBaja', index: 'FechaBaja', width: 100, align: "left" },
			{ name: 'ComentarioBaja', index: 'ComentarioBaja', width: 100, align: "center",sortable:false,formatter:Lupa2},
//			{ name: 'EstadoBienLogico', index: 'EstadoBienLogico', width: 100, align: "left" },
			{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true},
			{ name: 'CodEstadoBien', index: 'CodEstadoBien', hidden: true},
		    { name: 'Departamento', index: 'Departamento', hidden: true },
			{ name: 'Provincia', index: 'Provincia', hidden: true},
			{ name: 'Distrito', index: 'Distrito', hidden: true},
			{ name: 'FlagOrigen', index: 'FlagOrigen', hidden: true },
			{ name: 'Id', index: 'Id', hidden: true}
		],
		height: '100%',
		pager: '#jqGrid_pager_D',
		loadtext: 'Cargando datos...',
		emptyrecords: 'No hay resultados',
		rowNum: 10,
		rowList: [10, 20, 30],
		sortname: 'CodSolicitudCredito',
		sortorder: 'desc',
		viewrecords: true,
		gridview: true,
		autowidth: true,
		altRows: true,
		altclass: 'gridAltClass',
		//multiselect: true,
    onSelectRow: function(id) {
        var rowData = $("#jqGrid_lista_D").jqGrid('getRowData', id);
    	$("#hddRowId").val(id);
    	$("#hidNumeroContrato").val(rowData.CodSolicitudCredito);
     	$("#hidFlagOrigen").val(rowData.FlagOrigen);
    	$("#hidSecFinanciamiento").val(rowData.SecFinanciamiento);
    	$("#hidCodEstado").val(rowData.CodEstadoBien);
    },
   	ondblClickRow: function(id) {
	    parent.fn_blockUI();
		var rowData = $("#jqGrid_lista_D").jqGrid('getRowData', id);
		//fn_util_redirect('frmMantenimientoBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + $("#hidSecFinanciamiento").val() + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#hidCodEstado").val());
		//IBK - RPH precio venta leasing
		fn_util_redirect('frmMantenimientoBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + $("#hidSecFinanciamiento").val() + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#hidCodEstado").val() + "&precioventa=" + $("#hidPrecioVenta").val());
		//fin
	}     
	}).navGrid('#jqGrid_pager_D', { edit: false, add: false, del: false });
      $("#search_jqGrid_lista_D").hide();
	
    function Lupa2(cellvalue, options, rowObject) {
           
       if (rowObject.ComentarioBaja == "") {
           return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
       } else {
          var sScript2 = "javascript:VerObservaciones(" + rowObject.CodSolicitudCredito + "," + rowObject.SecFinanciamiento + ");";
       	  return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
       }
    	
    };
	
}

//****************************************************************
// Funcion		:: 	fn_buscarMaquinarias
// Descripción	::	
// Log			:: 	AEP - 27/09/2012
//****************************************************************
	
function fn_buscarSistemasOtros() {
	
	    var vNumeroContrato = $('#txtNumeroContrato').val() == undefined ? "" : $('#txtNumeroContrato').val();
		var vEstadoLogico= $('#ddlEstadoBienSistemas').val() == undefined ? "" : $('#ddlEstadoBienSistemas').val();
	
    	 var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_D", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", intPaginaActual,    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_D", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_D", "sortorder"), // Criterio de ordenación.
                             "pNumeroContraro", vNumeroContrato,
                             "pCodEstadoLogico", vEstadoLogico
                            ];
		

    fn_util_AjaxWM("frmMantenimientoBienContrato.aspx/ListadoBienContratoSistemas",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_D.addJSONData(jsondata);
                    	fn_doResize();
                    },
                    function(request) {
                        fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                    }
                   );
	}


function fn_cargaGrillaDOCUMENTOS() {
/*******************************
CARGA GRILLA TAB  DOCUMENTOS 
********************************/

//    var mydata2 =
//          [
//		    { CodigoDocumento: "001", NombreArchivo: " PU", RutaArchivo: "", Comentario: "" },
//		    { CodigoDocumento: "001", NombreArchivo: " HLR", RutaArchivo: "", Comentario: "" },
//		    { CodigoDocumento: "001", NombreArchivo: " Arrentadamiento Financiero", RutaArchivo: "", Comentario: "" },
//		    { CodigoDocumento: "001", NombreArchivo: " Minuta de Compra y Venta", RutaArchivo: "", Comentario: "" }

//		  ];
//		  
		  
    $("#jqGrid_lista_I").jqGrid({

   // datatype: "local",
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
    colNames: ['Codigo','','', 'Nombre Archivo', 'Adjunto', 'Comentario',''],
    colModel: [
                { name: 'CODIGOBIENDOCUMENTO', index: 'CODIGOBIENDOCUMENTO', hidden: true },
    	        { name: 'NUMEROCONTRATO', index: 'NUMEROCONTRATO', hidden: true },
    	        { name: 'SECFINANCIAMIENTO', index: 'SECFINANCIAMIENTO', hidden: true },
		        { name: 'NOMBREARCHIVO', index: 'NOMBREARCHIVO', width: 200, align: "left", sorttype: "string", defaultValue: "" },
		        { name: 'ADJUNTO', index: 'ADJUNTO', width: 100, align: "Center", sortable: false, formatter: fn_icoDownload },
		        //{ name: 'OBSERVACIONES', index: 'OBSERVACIONES', width: 200, align: "center", sorttype: "string" ,sortable:false,formatter:VerObservacion},
    	        { name: 'OBSERVACIONES', index: 'OBSERVACIONES',align: "left", sorttype: "string"},
    	        { name: 'ESTADODOCCONTRATO', index: 'ESTADODOCCONTRATO', hidden: true }
	    ],
    height: '100%',
    pager: '#jqGrid_pager_I',
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
        var rowData = $("#jqGrid_lista_I").jqGrid('getRowData', id);
        $("#hidRowDocumento").val(id);
    }
});
//for (var i = 0; i <= mydata2.length; i++) {
//    jQuery("#jqGrid_lista_I").jqGrid('addRowData', i + 1, mydata2[i]);
//}

jQuery("#jqGrid_lista_I").jqGrid('navGrid', '#jqGrid_pager_I', { edit: false, add: false, del: false });

// Le asigna el ancho a usar para la grilla.
$("#jqGrid_lista_I").setGridWidth($(window).width() - 100);
$("#search_jqGrid_lista_I").hide();

    function fn_icoDownload(cellvalue, options, rowObject) {
        var strNombreArchivo = rowObject.ADJUNTO.split('\\').pop();
        strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
        if (fn_util_trim(rowObject.ADJUNTO) != "") {
            return "<img src='../Util/images/ico_download.gif' alt='" + strNombreArchivo + "' title='" + strNombreArchivo + "' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.ADJUNTO) + "');\" style='cursor:pointer;'/>";
        } else {
            return ".";
        }
    };
    function VerObservacion(cellvalue, options, rowObject) {

        if (rowObject.OBSERVACIONES == "") {
            return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
        } else {
            var sScript2 = "javascript:VerObservacionesDocumento('" + rowObject.NUMEROCONTRATO + "','" + rowObject.SECFINANCIAMIENTO + "','" + rowObject.OBSERVACIONES + "','" + rowObject.NOMBREARCHIVO + "');";
            return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick=\"" + sScript2 + "\" style='cursor: pointer;cursor: hand;' />";
        }

    };
}

//Inicio-JJM
function fn_cargaGrillaDocumentosMaquina() {
    /*******************************
    CARGA GRILLA TAB  DOCUMENTOS 
    ********************************/

    //    var mydata2 =
    //          [
    //		    { CodigoDocumento: "001", NombreArchivo: " PU", RutaArchivo: "", Comentario: "" },
    //		    { CodigoDocumento: "001", NombreArchivo: " HLR", RutaArchivo: "", Comentario: "" },
    //		    { CodigoDocumento: "001", NombreArchivo: " Arrentadamiento Financiero", RutaArchivo: "", Comentario: "" },
    //		    { CodigoDocumento: "001", NombreArchivo: " Minuta de Compra y Venta", RutaArchivo: "", Comentario: "" }

    //		  ];
    //

    $("#jqGrid_lista_K").jqGrid({

        // datatype: "local",
        datatype: function() {
            fn_ListagrillaDocumentosMaquinaria();
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
        colNames: ['Codigo', '', '', 'Nombre Archivo', 'Adjunto', 'Comentario', ''],
        colModel: [
                { name: 'CODIGOBIENDOCUMENTO', index: 'CODIGOBIENDOCUMENTO', hidden: true },
    	        { name: 'NUMEROCONTRATO', index: 'NUMEROCONTRATO', hidden: true },
    	        { name: 'SECFINANCIAMIENTO', index: 'SECFINANCIAMIENTO', hidden: true },
		        { name: 'NOMBREARCHIVO', index: 'NOMBREARCHIVO', width: 200, align: "left", sorttype: "string", defaultValue: "" },
		        { name: 'ADJUNTO', index: 'ADJUNTO', width: 100, align: "Center", sortable: false, formatter: fn_icoDownload2 },
        //{ name: 'OBSERVACIONES', index: 'OBSERVACIONES', width: 200, align: "center", sorttype: "string" ,sortable:false,formatter:VerObservacion},
    	        { name: 'OBSERVACIONES', index: 'OBSERVACIONES', align: "left", sorttype: "string" },
    	        { name: 'ESTADODOCCONTRATO', index: 'ESTADODOCCONTRATO', hidden: true }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_K',
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
            var rowData = $("#jqGrid_lista_K").jqGrid('getRowData', id);
            $("#hidRowDocumento").val(id);
        }
    });
    jQuery("#jqGrid_lista_K").jqGrid('navGrid', '#jqGrid_pager_K', { edit: false, add: false, del: false });

    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_K").setGridWidth($(window).width() - 100);
    $("#search_jqGrid_lista_K").hide();

    function fn_icoDownload2(cellvalue, options, rowObject) {
        var strNombreArchivo = rowObject.ADJUNTO.split('\\').pop();
        strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
        if (fn_util_trim(rowObject.ADJUNTO) != "") {
            return "<img src='../Util/images/ico_download.gif' alt='" + strNombreArchivo + "' title='" + strNombreArchivo + "' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.ADJUNTO) + "');\" style='cursor:pointer;'/>";
        } else {
            return ".";
        }
    };
    function VerObservacion(cellvalue, options, rowObject) {

        if (rowObject.OBSERVACIONES == "") {
            return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
        } else {
            var sScript2 = "javascript:VerObservacionesDocumento('" + rowObject.NUMEROCONTRATO + "','" + rowObject.SECFINANCIAMIENTO + "','" + rowObject.OBSERVACIONES + "','" + rowObject.NOMBREARCHIVO + "');";
            return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick=\"" + sScript2 + "\" style='cursor: pointer;cursor: hand;' />";
        }

    };
}
//Transportes - Documentos
function fn_cargaGrillaDocumentosTransporte() {
    /*******************************
    CARGA GRILLA TAB  DOCUMENTOS 
    ********************************/

    //    var mydata2 =
    //          [
    //		    { CodigoDocumento: "001", NombreArchivo: " PU", RutaArchivo: "", Comentario: "" },
    //		    { CodigoDocumento: "001", NombreArchivo: " HLR", RutaArchivo: "", Comentario: "" },
    //		    { CodigoDocumento: "001", NombreArchivo: " Arrentadamiento Financiero", RutaArchivo: "", Comentario: "" },
    //		    { CodigoDocumento: "001", NombreArchivo: " Minuta de Compra y Venta", RutaArchivo: "", Comentario: "" }

    //		  ];
    //

    $("#jqGrid_lista_J").jqGrid({

        // datatype: "local",
        datatype: function() {
            fn_ListagrillaDocumentosTransporte();
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
        colNames: ['Codigo', '', '', 'Nombre Archivo', 'Adjunto', 'Comentario', ''],
        colModel: [
                { name: 'CODIGOBIENDOCUMENTO', index: 'CODIGOBIENDOCUMENTO', hidden: true },
    	        { name: 'NUMEROCONTRATO', index: 'NUMEROCONTRATO', hidden: true },
    	        { name: 'SECFINANCIAMIENTO', index: 'SECFINANCIAMIENTO', hidden: true },
		        { name: 'NOMBREARCHIVO', index: 'NOMBREARCHIVO', width: 200, align: "left", sorttype: "string", defaultValue: "" },
		        { name: 'ADJUNTO', index: 'ADJUNTO', width: 100, align: "Center", sortable: false, formatter: fn_icoDownload },
        //{ name: 'OBSERVACIONES', index: 'OBSERVACIONES', width: 200, align: "center", sorttype: "string" ,sortable:false,formatter:VerObservacion},
    	        { name: 'OBSERVACIONES', index: 'OBSERVACIONES', align: "left", sorttype: "string" },
    	        { name: 'ESTADODOCCONTRATO', index: 'ESTADODOCCONTRATO', hidden: true }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_J',
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
            var rowData = $("#jqGrid_lista_J").jqGrid('getRowData', id);
            $("#hidRowDocumento").val(id);
        }
    });
    jQuery("#jqGrid_lista_J").jqGrid('navGrid', '#jqGrid_pager_J', { edit: false, add: false, del: false });

    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_J").setGridWidth($(window).width() - 100);
    $("#search_jqGrid_lista_J").hide();

    function fn_icoDownload(cellvalue, options, rowObject) {
        var strNombreArchivo = rowObject.ADJUNTO.split('\\').pop();
        strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
        if (fn_util_trim(rowObject.ADJUNTO) != "") {
            return "<img src='../Util/images/ico_download.gif' alt='" + strNombreArchivo + "' title='" + strNombreArchivo + "' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.ADJUNTO) + "');\" style='cursor:pointer;'/>";
        } else {
            return ".";
        }
    };
    function VerObservacion(cellvalue, options, rowObject) {

        if (rowObject.OBSERVACIONES == "") {
            return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
        } else {
            var sScript2 = "javascript:VerObservacionesDocumento('" + rowObject.NUMEROCONTRATO + "','" + rowObject.SECFINANCIAMIENTO + "','" + rowObject.OBSERVACIONES + "','" + rowObject.NOMBREARCHIVO + "');";
            return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick=\"" + sScript2 + "\" style='cursor: pointer;cursor: hand;' />";
        }

    };
}
//Sistemas-Documentos
function fn_cargaGrillaDocumentosSistema() {
    /*******************************
    CARGA GRILLA TAB  DOCUMENTOS 
    ********************************/

    //    var mydata2 =
    //          [
    //		    { CodigoDocumento: "001", NombreArchivo: " PU", RutaArchivo: "", Comentario: "" },
    //		    { CodigoDocumento: "001", NombreArchivo: " HLR", RutaArchivo: "", Comentario: "" },
    //		    { CodigoDocumento: "001", NombreArchivo: " Arrentadamiento Financiero", RutaArchivo: "", Comentario: "" },
    //		    { CodigoDocumento: "001", NombreArchivo: " Minuta de Compra y Venta", RutaArchivo: "", Comentario: "" }

    //		  ];
    //

    $("#jqGrid_lista_F").jqGrid({

        // datatype: "local",
        datatype: function() {
            fn_ListagrillaDocumentosSistema();
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
        colNames: ['Codigo', '', '', 'Nombre Archivo', 'Adjunto', 'Comentario', ''],
        colModel: [
                { name: 'CODIGOBIENDOCUMENTO', index: 'CODIGOBIENDOCUMENTO', hidden: true },
    	        { name: 'NUMEROCONTRATO', index: 'NUMEROCONTRATO', hidden: true },
    	        { name: 'SECFINANCIAMIENTO', index: 'SECFINANCIAMIENTO', hidden: true },
		        { name: 'NOMBREARCHIVO', index: 'NOMBREARCHIVO', width: 200, align: "left", sorttype: "string", defaultValue: "" },
		        { name: 'ADJUNTO', index: 'ADJUNTO', width: 100, align: "Center", sortable: false, formatter: fn_icoDownload },
        //{ name: 'OBSERVACIONES', index: 'OBSERVACIONES', width: 200, align: "center", sorttype: "string" ,sortable:false,formatter:VerObservacion},
    	        { name: 'OBSERVACIONES', index: 'OBSERVACIONES', align: "left", sorttype: "string" },
    	        { name: 'ESTADODOCCONTRATO', index: 'ESTADODOCCONTRATO', hidden: true }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_F',
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
            var rowData = $("#jqGrid_lista_F").jqGrid('getRowData', id);
            $("#hidRowDocumento").val(id);
        }
    });
    jQuery("#jqGrid_lista_F").jqGrid('navGrid', '#jqGrid_pager_F', { edit: false, add: false, del: false });

    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_F").setGridWidth($(window).width() - 100);
    $("#search_jqGrid_lista_F").hide();

    function fn_icoDownload(cellvalue, options, rowObject) {
        var strNombreArchivo = rowObject.ADJUNTO.split('\\').pop();
        strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
        if (fn_util_trim(rowObject.ADJUNTO) != "") {
            return "<img src='../Util/images/ico_download.gif' alt='" + strNombreArchivo + "' title='" + strNombreArchivo + "' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.ADJUNTO) + "');\" style='cursor:pointer;'/>";
        } else {
            return ".";
        }
    };
    function VerObservacion(cellvalue, options, rowObject) {

        if (rowObject.OBSERVACIONES == "") {
            return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
        } else {
            var sScript2 = "javascript:VerObservacionesDocumento('" + rowObject.NUMEROCONTRATO + "','" + rowObject.SECFINANCIAMIENTO + "','" + rowObject.OBSERVACIONES + "','" + rowObject.NOMBREARCHIVO + "');";
            return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick=\"" + sScript2 + "\" style='cursor: pointer;cursor: hand;' />";
        }

    };
}


//Fin-JJM

function VerObservacionesDocumento(strcodContrato,strsecfinanciamiento,strobservacion,strnombrearchivo) {
    var sTitulo = "Gestión del Bien";
    var sSubTitulo = "Mant. Bien :: Observación de Documento";
    parent.fn_util_AbreModal(sSubTitulo, "Administracion/frmDocumentoComentarioBien.aspx?ccf=" + strcodContrato + "&csf=" + strsecfinanciamiento + "&Add=true" + "&obs=" + strobservacion + "&nomArchivo=" + strnombrearchivo , 650, 320, function() { });
}

 function fn_abreArchivo(pstrRuta) {
       window.open("../frmDownload.aspx?nombreArchivo=" + pstrRuta);
       return false;
   }
 
//****************************************************************
// Funcion		:: 	fn_ListagrillaDocumentos
// Descripción	::	
// Log			:: 	AEP - 12/10/2012
//****************************************************************
function fn_ListagrillaDocumentos() {
    var arrParametros = ["pPageSize",      fn_util_getJQGridParam("jqGrid_lista_I", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage",   fn_util_getJQGridParam("jqGrid_lista_I", "page"), // Página actual
                         "pSortColumn",    fn_util_getJQGridParam("jqGrid_lista_I", "sortname"), // Columna a ordenar
                         "pSortOrder",     fn_util_getJQGridParam("jqGrid_lista_I", "sortorder"), // Criterio de ordenación
                         "pCodigoContrato",        $("#hidNumeroContrato").val(),
                         "pCodigoBien",     0
                         
                        ];
	
    fn_util_AjaxSyncWM("frmMantenimientoBienContrato.aspx/ListaDocumentos",
    arrParametros,
    function(jsondata) {
        jqGrid_lista_I.addJSONData(jsondata);
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
//IBK JJM
function fn_ListagrillaDocumentosMaquinaria() {
   
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_K", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_K", "page"), // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_K", "sortname"), // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_K", "sortorder"), // Criterio de ordenación
                         "pCodigoContrato", $("#hidNumeroContrato").val(),
                         "pCodigoBien", 0

                        ];

    fn_util_AjaxSyncWM("frmMantenimientoBienContrato.aspx/ListaDocumentos",
    arrParametros,
    function(jsondata) {
        jqGrid_lista_K.addJSONData(jsondata);
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
//Transporte-Documentos

function fn_ListagrillaDocumentosTransporte() {
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_J", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_J", "page"), // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_J", "sortname"), // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_J", "sortorder"), // Criterio de ordenación
                         "pCodigoContrato", $("#hidNumeroContrato").val(),
                         "pCodigoBien", 0

                        ];

    fn_util_AjaxSyncWM("frmMantenimientoBienContrato.aspx/ListaDocumentos",
    arrParametros,
    function(jsondata) {
        jqGrid_lista_J.addJSONData(jsondata);
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

//Sistemas-Documento
function fn_ListagrillaDocumentosSistema() {
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_F", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_F", "page"), // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_F", "sortname"), // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_F", "sortorder"), // Criterio de ordenación
                         "pCodigoContrato", $("#hidNumeroContrato").val(),
                         "pCodigoBien", 0

                        ];

    fn_util_AjaxSyncWM("frmMantenimientoBienContrato.aspx/ListaDocumentos",
    arrParametros,
    function(jsondata) {
        jqGrid_lista_F.addJSONData(jsondata);
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

//FIN JJM
//****************************************************************
// Funcion		:: 	fn_cancelar
// Descripción	::	cancelar
// Log			:: 	WCR - 18/06/2012
//****************************************************************
function fn_cancelar() {
	fn_mdl_confirma('¿Está seguro de volver?',
		function() {
			parent.fn_blockUI();
			fn_util_redirect('frmMantenimientoListaContrato.aspx');
		},
		"../util/images/question.gif",
		function() {
		},
		'Mantenimiento Bien'
	);
}

//****************************************************************
// Funcion		:: 	fn_Validacion
// Descripción	::	Valida Registro
// Log			:: 	WCR - 18/06/2012
//****************************************************************
function fn_Validacion(pError) {
    
	 var cmbTipoBien = $('select[id=cmbTipoBien]');
     var cmbTipoBien1 = $('select[id=cmbTipoBien1]');
	 var cmbTipoBien2 = $('select[id=cmbTipoBien2]');

	 var txtPartida = $('input[id=txtPartidaRegistral]:text');
	 var txtOficina = $('input[id=txtOficinaRegistral]:text');
	 var txtPartida1 = $('input[id=txtPartidaRegistral2]:text');
	 var txtOficina1 = $('input[id=txtOficinaRegistral2]:text');

	// Inmueble
    if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) {
      pError.append(fn_util_ValidateControl(cmbTipoBien[0], 'un Tipo de Bien', 1, ''));
      pError.append(fn_util_ValidateControl(txtPartida[0], 'Partida Registral', 1, ''));
      pError.append(fn_util_ValidateControl(txtOficina[0], 'Oficina Registral', 1, ''));
    }
    // Maquinaria
    else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) {
        pError.append(fn_util_ValidateControl(cmbTipoBien1[0], 'un Tipo de Bien', 1, ''));
    }
    // Otros
    else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) {
      pError.append(fn_util_ValidateControl(cmbTipoBien2[0], 'un Tipo de Bien', 1, ''));
      pError.append(fn_util_ValidateControl(txtPartida1[0], 'Partida Registral', 1, ''));
      pError.append(fn_util_ValidateControl(txtOficina1[0], 'Oficina Registral', 1, ''));
    }
   
    return pError.toString();
}


//****************************************************************
// Funcion		:: 	fn_grabar
// Descripción	::	Grabar
// Log			:: 	AEP - 04/10/2012
//****************************************************************
function fn_grabar() {
    var strError = new StringBuilderEx();
//    fn_Validacion(strError);
//    if (strError.toString() != '') {
        parent.fn_unBlockUI();
//        parent.fn_mdl_alert(strError.toString(), function() { });
//        strError = null;

//    }
//    else {
            
                var objtxtFechaRegistral = $('input[id=txtFechaInscripcionRegistral]:text');
	            var objtxtFechaMunicipal = $('input[id=txtFechaInscripcionMunicipal]:text');
	            
	            if(($("#ddlEstadoInscripcionRRPP").val()=='001') &&  ($("#txtFechaInscripcionRegistral").val()=='')) {
	            strError.append(fn_util_ValidateControl(objtxtFechaRegistral[0], 'la fecha de inscripción registral', 1, ''));
	            }
	            if(($("#ddlEstadoInscripcionMunicipal").val()=='001') && ($("#txtFechaInscripcionMunicipal").val()=='')) {
	             strError.append(fn_util_ValidateControl(objtxtFechaMunicipal[0], 'la fecha de inscripción municipal ', 1, ''));
	             }
	
            	    if (strError.toString() != '') {
                    parent.fn_unBlockUI();
                    parent.fn_mdl_alert(strError.toString(), function() { });
            	    
                    strError = null;
                	return;
                    }
            

        fn_mdl_confirma('¿Esta seguro de actualizar este registro?',
            function() {
                parent.fn_blockUI();
            	
                var vCodSolicitudCredito = $("#txtNumeroContrato").val();
                var vFechaProbableFinObra = '';
                var vFechaRealFinObra= '';
                var vFechaInscripcionMunicipal = '';
                var vFechaEnvioNotaria = '';
                var vFechaPropiedad = '';
                var vFechaInscripcionRegistral = '';
                var vOficinaRegistral = '';
            	var vCodigoNotaria= '';
            	var vCodEstadoInscripcionRrpp = '';
            	var vCodEstadoMunicipal = '';
            	var vCodEstadoTransferencia = '';
            	var vObservacionContrato = '';
            	
              
                    vFechaProbableFinObra = $('#txtFechaFinObra').val() == undefined ? "" : $('#txtFechaFinObra').val();
                    vFechaRealFinObra = $('#txtFechaRealFinObra').val() == undefined ? "" : $('#txtFechaRealFinObra').val();
                	vFechaInscripcionMunicipal = $('#txtFechaInscripcionMunicipal').val() == undefined ? "" : $('#txtFechaInscripcionMunicipal').val();
                	vFechaEnvioNotaria = $('#txtFechaEnvioNotaria').val() == undefined ? "" : $('#txtFechaEnvioNotaria').val();
                	vFechaPropiedad = $('#txtFechaPropiedad').val() == undefined ? "" : $('#txtFechaPropiedad').val();
                	vFechaInscripcionRegistral = $('#txtFechaInscripcionRegistral').val() == undefined ? "" : $('#txtFechaInscripcionRegistral').val();
                	vOficinaRegistral = $('#txtOficinaRegistral').val() == undefined ? "" : $('#txtOficinaRegistral').val();
                	vCodigoNotaria = $('#ddlNotaria').val() == undefined ? "" : $('#ddlNotaria').val();
                	vCodEstadoInscripcionRrpp = $('#ddlEstadoInscripcionRRPP').val() == undefined ? "" : $('#ddlEstadoInscripcionRRPP').val();
                	vCodEstadoMunicipal = $('#ddlEstadoInscripcionMunicipal').val() == undefined ? "" : $('#ddlEstadoInscripcionMunicipal').val();
                	vCodEstadoTransferencia = $('#ddlPropiedad').val() == undefined ? "" : $('#ddlPropiedad').val();
                	vObservacionContrato = $('#txtObservacionContrato').val() == undefined ? "" : $('#txtObservacionContrato').val();
                
                	 	var arrParametros = ["pNumeroContrato", vCodSolicitudCredito,
                                         "pFechaProbableFinObra", Fn_util_DateToString(vFechaProbableFinObra),
                                         "pFechaRealFinObra", Fn_util_DateToString(vFechaRealFinObra),
                                         "pFechaInscripcionMunicipal",Fn_util_DateToString(vFechaInscripcionMunicipal),
                                         "pFechaEnvioNotaria", Fn_util_DateToString(vFechaEnvioNotaria),
                                         "pFechaPropiedad", Fn_util_DateToString(vFechaPropiedad),
                	 		             "pFechaInscripcionRegistral",Fn_util_DateToString(vFechaInscripcionRegistral),
                	 		             "pOficinaRegistral",vOficinaRegistral,
                	 		             "pCodigoNotaria",vCodigoNotaria,
                	 		             "pCodEstadoInscripcionRrpp",vCodEstadoInscripcionRrpp,
                	 		             "pCodEstadoMunicipal",vCodEstadoMunicipal,
                	 		             "pCodEstadoTransferencia", vCodEstadoTransferencia,
                	 		             "pObservacionContrato", vObservacionContrato
                                    ];

            	fn_util_AjaxWM("frmMantenimientoBienContrato.aspx/GuardarRrppBienContrato",
            		arrParametros,
            		function (resultado2) {
            			parent.fn_unBlockUI();
            			fn_mdl_mensajeIco("Se actualizó correctamente los datos del bien", "../util/images/ok.gif", "Grabar Bienes por Contrato");
            			fn_buscarInmuebles();
            		},
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
            },
            "../util/images/question.gif",
            function() {
            	$("#txtFechaInscripcionMunicipal").addClass('css_calendario').removeClass('css_input_error');
            	$("#txtFechaInscripcionRegistral").addClass('css_calendario').removeClass('css_input_error');
            },
            'Mantenimiento Bien'
         );
    }
//}

////****************************************************************
//// Función		:: 	fn_MensajeYRedireccionarSolicitud
//// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
////                  Luego lo redirecciona al formulario de búsquedas ("frmMantenimientoBienListado.aspx").
//// Log			:: 	AEP - 09/10/2012
////****************************************************************
//var fn_MensajeYRedireccionar = function() {
//    parent.fn_unBlockUI();
//    parent.fn_mdl_alert('Los datos se grabaron satisfactoriamente', function() { fn_util_redirect("frmMantenimientoListaContrato.aspx"); });
//};


function getParameter(paramName) {
    var searchString = window.location.search.substring(1), i, val, params = searchString.split("&");

    for (var i = 0; i < params.length; i++) {
        val = params[i].split("=");
        if (val[0] == paramName) {
            return unescape(val[1]);
        }
    }

    return null;
}

//****************************************************************
// Funcion		:: 	fn_InicializarCampos
// Descripción	::	
// Log			:: 	AEP - 17/07/2012
//****************************************************************
function fn_InicializarCampos() {
//---------------------------------
	
	// Validaciones de RRPP
	
//	if($("#txtFechaInscripcionMunicipal").val()=='') {
//		$("#ddlEstadoInscripcionMunicipal").attr('disabled', 'disabled');
//	}
//	
//	if($("#txtFechaInscripcionRegistral").val()=='') {
//		$("#ddlEstadoInscripcionRRPP").attr('disabled', 'disabled');
//	}
	
	if ($("#ddlEstadoInscripcionRRPP").val()!='001') {
	    $("#txtFechaInscripcionRegistral").attr('disabled', 'disabled');
		$("#txtFechaInscripcionRegistral").val('');
	}
	if ($("#ddlEstadoInscripcionMunicipal").val()!='001') {
	    $("#txtFechaInscripcionMunicipal").attr('disabled', 'disabled');
		$("#txtFechaInscripcionMunicipal").val('');
	}
	
	//Setea Calendario
    //---------------------------------    
    fn_util_SeteaCalendario($("#txtFechaFinObra"));
    fn_util_SeteaCalendario($("#txtFechaRealFinObra"));
	fn_util_SeteaCalendario($("#txtFechaInscripcionRegistral"));
	fn_util_SeteaCalendario($("#txtFechaInscripcionMunicipal"));
	//fn_util_SeteaCalendarioFunction($("#txtFechaInscripcionRegistral"),function() { fn_ValidarInscripcionRegistral();});
	//fn_util_SeteaCalendarioFunction($("#txtFechaInscripcionMunicipal"),function() {fn_ValidarInscripcionMunicipal();});
	fn_util_SeteaCalendario($("#txtFechaEnvioNotaria"));
	fn_util_SeteaCalendario($("#txtFechaPropiedad"));
    

    $('#txtObservacionContrato').validText({ type: 'comment',length:500});
	$('#txtObservacionContrato').maxLength(500);

	
}



//****************************************************************
// Funcion		:: 	fn_abreNuevoDocumentoComentario 
// Descripción	::	Abre Modal nuevo documento o comentario
// Log			:: 	AEP - 21/09/2012
//****************************************************************
function fn_abreNuevoDocumentoComentario() {
   
        var strCodigoContrato = $("#hidNumeroContrato").val();
        parent.fn_util_AbreModal("Mant. Bien :: Documentos y Comentarios", "Administracion/frmDocumentoComentarioContratoBien.aspx?codcontrato=" + strCodigoContrato, 650, 320, function() { });
    
}
//InicioJJM
function fn_abreEditarDocumentoComentarioMaquina() {

    var strCodigoContrato = $("#hidNumeroContrato").val();
    var id = $("#hidRowDocumento").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Maquinarias");
    } else {
        var rowData = $("#jqGrid_lista_K").jqGrid('getRowData', id);
        var codigo = rowData.CODIGOBIENDOCUMENTO;
        var strobservacion = rowData.OBSERVACIONES;
        var strnombrearchivo = rowData.NOMBREARCHIVO;
        var strEstadoBien = rowData.ESTADODOCCONTRATO;

        parent.fn_util_AbreModal("Mant. Bien :: Documentos y Comentarios", "Administracion/frmDocumentoComentarioContratoBien.aspx?codcontrato=" + strCodigoContrato + "&codigo=" + codigo + "&obs=" + strobservacion + "&nomArchivo=" + strnombrearchivo + "&det=" + strEstadoBien, 650, 320, function() { });
    }

}

function fn_EliminarDocumentoComentarioMaquina() {

    //Variables

    var id = $("#hidRowDocumento").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder eliminar.", "util/images/warning.gif", "Mant. Maquinarias");
    } else {
        var rowData = $("#jqGrid_lista_K").jqGrid('getRowData', id);
        var strCodigoContrato = $("#hidNumeroContrato").val();
        var strCodigoDocumento = rowData.CODIGOBIENDOCUMENTO;


        var paramArray = ["pstrCodigoContrato", strCodigoContrato, "pstrCodigoDocumento", strCodigoDocumento];
        //Confirmacion de Eliminacion
        parent.fn_mdl_confirma(
                            "¿Está seguro que desea eliminar el Documento/Comentario?  ", //Mensaje - Obligatorio
                            function() {
                                parent.fn_blockUI();
                                fn_util_AjaxWM("frmMantenimientoBienContrato.aspx/EliminaDocumentoComentario",
                                                   paramArray,
                                                   function(resultado) {
                                                       fn_ListagrillaDocumentosMaquinaria();
                                                       parent.fn_unBlockUI();
                                                   },
                                                   function(resultado) {
                                                       var error = eval("(" + resultado.responseText + ")");
                                                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
                                                   }
                                    );

                            },
                            "Util/images/question.gif",
                            function() { },
                            'ELIMINAR DOCUMENTO/COMENTARIO'
            );
    }

}
//Transporte
function fn_abreEditarDocumentoComentarioTransporte() {

    var strCodigoContrato = $("#hidNumeroContrato").val();
    var id = $("#hidRowDocumento").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Transportes");
    } else {
        var rowData = $("#jqGrid_lista_J").jqGrid('getRowData', id);
        var codigo = rowData.CODIGOBIENDOCUMENTO;
        var strobservacion = rowData.OBSERVACIONES;
        var strnombrearchivo = rowData.NOMBREARCHIVO;
        var strEstadoBien = rowData.ESTADODOCCONTRATO;

        parent.fn_util_AbreModal("Mant. Bien :: Documentos y Comentarios", "Administracion/frmDocumentoComentarioContratoBien.aspx?codcontrato=" + strCodigoContrato + "&codigo=" + codigo + "&obs=" + strobservacion + "&nomArchivo=" + strnombrearchivo + "&det=" + strEstadoBien, 650, 320, function() { });
    }

}

function fn_EliminarDocumentoComentarioTransporte() {

    //Variables

    var id = $("#hidRowDocumento").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder eliminar.", "util/images/warning.gif", "Mant. Transportes");
    } else {
        var rowData = $("#jqGrid_lista_J").jqGrid('getRowData', id);
        var strCodigoContrato = $("#hidNumeroContrato").val();
        var strCodigoDocumento = rowData.CODIGOBIENDOCUMENTO;


        var paramArray = ["pstrCodigoContrato", strCodigoContrato, "pstrCodigoDocumento", strCodigoDocumento];
        //Confirmacion de Eliminacion
        parent.fn_mdl_confirma(
                            "¿Está seguro que desea eliminar el Documento/Comentario?  ", //Mensaje - Obligatorio
                            function() {
                                parent.fn_blockUI();
                                fn_util_AjaxWM("frmMantenimientoBienContrato.aspx/EliminaDocumentoComentario",
                                                   paramArray,
                                                   function(resultado) {
                                                       fn_ListagrillaDocumentosTransporte();
                                                       parent.fn_unBlockUI();
                                                   },
                                                   function(resultado) {
                                                       var error = eval("(" + resultado.responseText + ")");
                                                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
                                                   }
                                    );

                            },
                            "Util/images/question.gif",
                            function() { },
                            'ELIMINAR DOCUMENTO/COMENTARIO'
            );
    }

}
//Sistemas

function fn_abreEditarDocumentoComentarioSistema() {

    var strCodigoContrato = $("#hidNumeroContrato").val();
    var id = $("#hidRowDocumento").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Transportes");
    } else {
        var rowData = $("#jqGrid_lista_F").jqGrid('getRowData', id);
        var codigo = rowData.CODIGOBIENDOCUMENTO;
        var strobservacion = rowData.OBSERVACIONES;
        var strnombrearchivo = rowData.NOMBREARCHIVO;
        var strEstadoBien = rowData.ESTADODOCCONTRATO;

        parent.fn_util_AbreModal("Mant. Bien :: Documentos y Comentarios", "Administracion/frmDocumentoComentarioContratoBien.aspx?codcontrato=" + strCodigoContrato + "&codigo=" + codigo + "&obs=" + strobservacion + "&nomArchivo=" + strnombrearchivo + "&det=" + strEstadoBien, 650, 320, function() { });
    }

}

function fn_eliminarDocumentoComentariosistema() {

    //Variables

    var id = $("#hidRowDocumento").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder eliminar.", "util/images/warning.gif", "Mant. Transportes");
    } else {
        var rowData = $("#jqGrid_lista_F").jqGrid('getRowData', id);
        var strCodigoContrato = $("#hidNumeroContrato").val();
        var strCodigoDocumento = rowData.CODIGOBIENDOCUMENTO;


        var paramArray = ["pstrCodigoContrato", strCodigoContrato, "pstrCodigoDocumento", strCodigoDocumento];
        //Confirmacion de Eliminacion
        parent.fn_mdl_confirma(
                            "¿Está seguro que desea eliminar el Documento/Comentario?  ", //Mensaje - Obligatorio
                            function() {
                                parent.fn_blockUI();
                                fn_util_AjaxWM("frmMantenimientoBienContrato.aspx/EliminaDocumentoComentario",
                                                   paramArray,
                                                   function(resultado) {
                                                       fn_ListagrillaDocumentosSistema();
                                                       parent.fn_unBlockUI();
                                                   },
                                                   function(resultado) {
                                                       var error = eval("(" + resultado.responseText + ")");
                                                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
                                                   }
                                    );

                            },
                            "Util/images/question.gif",
                            function() { },
                            'ELIMINAR DOCUMENTO/COMENTARIO'
            );
    }

}

//FinJJM
//****************************************************************
// Funcion		:: 	fn_abreNuevoDocumentoComentario 
// Descripción	::	Abre Modal nuevo documento o comentario
// Log			:: 	AEP - 21/09/2012
//****************************************************************
function fn_abreEditarDocumentoComentario() {
   
        var strCodigoContrato = $("#hidNumeroContrato").val();
	    var id = $("#hidRowDocumento").val();
	    if(IsNullOrEmpty(id)) 
	    {
	    parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	    }else 
	    {
		var rowData = $("#jqGrid_lista_I").jqGrid('getRowData', id);
		var codigo = rowData.CODIGOBIENDOCUMENTO;
		var strobservacion = rowData.OBSERVACIONES;
		var strnombrearchivo = rowData.NOMBREARCHIVO;
	    var strEstadoBien = rowData.ESTADODOCCONTRATO;	
	
        parent.fn_util_AbreModal("Mant. Bien :: Documentos y Comentarios", "Administracion/frmDocumentoComentarioContratoBien.aspx?codcontrato=" + strCodigoContrato + "&codigo=" + codigo + "&obs=" + strobservacion + "&nomArchivo=" + strnombrearchivo + "&det=" + strEstadoBien, 650, 320, function() { });
	}
   
}

//****************************************************************
// Funcion		:: 	fn_eliminarDocumentoComentario 
// Descripción	::	Eliminar 
// Log			:: 	AEP - 17/10/2012
//****************************************************************
function fn_eliminarDocumentoComentario() {
        
        //Variables
   
	    var id = $("#hidRowDocumento").val();
	    if(IsNullOrEmpty(id)) 
	    {
	    parent.fn_mdl_mensajeIco("Seleccione un registro para poder eliminar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	    }else 
	    {
	    	var rowData = $("#jqGrid_lista_I").jqGrid('getRowData', id);
	    	var strCodigoContrato = $("#hidNumeroContrato").val();
	    	var strCodigoDocumento = rowData.CODIGOBIENDOCUMENTO;
	    	
	    	
            var paramArray = ["pstrCodigoContrato", strCodigoContrato, "pstrCodigoDocumento", strCodigoDocumento];
	    	 //Confirmacion de Eliminacion
            parent.fn_mdl_confirma(
                            "¿Está seguro que desea eliminar el Documento/Comentario?  ", //Mensaje - Obligatorio
                            function() {
                                parent.fn_blockUI();
                                fn_util_AjaxWM("frmMantenimientoBienContrato.aspx/EliminaDocumentoComentario",
                                                   paramArray,
                                                   function(resultado) {
                                                       fn_ListagrillaDocumentos();
                                                       parent.fn_unBlockUI();
                                                   },
                                                   function(resultado) {
                                                       var error = eval("(" + resultado.responseText + ")");
                                                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
                                                   }
                                    );

                            },
                            "Util/images/question.gif",
                            function() { },
                            'ELIMINAR DOCUMENTO/COMENTARIO'
            );
        }       

}

//****************************************************************
// Funcion		:: 	fn_AbreRegistroInmuebles 
// Descripción	::	Abre Modal de registro de inmuebles hijos
// Log			:: 	AEP - 03/10/2012
//****************************************************************
function fn_AbreRegistroInmuebles() {
   
        var strCodigoContrato = $("#hidNumeroContrato").val();
	    var strCodigoBien= $("#hidSecFinanciamiento").val();
	
     var id = $("#hddRowId").val();
	
	//	if(IsNullOrEmpty(id)) 
//	{

//	parent.fn_mdl_mensajeIco("Seleccione un registro para poder agregar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
//	}else 
//	{

//	parent.fn_util_AbreModal("", "Administracion/frmMantRegistroBienesContrato.aspx?csc=" + strCodigoContrato + "&csf=" + strCodigoBien, 950, 310, function() { });


     //        }
     //IBK JJM Inicio
     parent.fn_util_AbreModal("", "Administracion/frmMantRegistroBienesContrato.aspx?csc=" + strCodigoContrato + "&csf=" + strCodigoBien, 950, 310, function() { });
     //IBK JJM Fin

        //}
	}
//****************************************************************
// Funcion		:: 	fn_AbreRegistroMaquinaria
// Descripción	::	Abre Modal de registro de inmuebles
// Log			:: 	AEP - 03/10/2012
//****************************************************************
function fn_AbreRegistroMaquinaria() {
   
        var strCodigoContrato = $("#hidNumeroContrato").val();
	    var strCodigoBien= $("#hidSecFinanciamiento").val();
	
     var id = $("#hddRowId").val();
	
	//	if(IsNullOrEmpty(id)) 
//	{
//	parent.fn_mdl_mensajeIco("Seleccione un registro para poder agregar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
//	}else 
//	{

//	parent.fn_util_AbreModal("", "Administracion/frmMantRegistroBienesContrato.aspx?csc=" + strCodigoContrato + "&csf=" + strCodigoBien, 950, 310, function() { });






     //	}
     //IBK JJM Inicio
     parent.fn_util_AbreModal("", "Administracion/frmMantRegistroBienesContrato.aspx?csc=" + strCodigoContrato + "&csf=" + strCodigoBien, 950, 310, function() { }); 
     //IBK JJM Fin
}

//****************************************************************
// Funcion		:: 	fn_AbreRegistroVehiculos
// Descripción	::	Abre Modal de registro de vehiculos
// Log			:: 	AEP - 04/10/2012
//****************************************************************

function fn_AbreRegistroVehiculos() {
   
  var strCodigoContrato = $("#hidNumeroContrato").val();
  var strCodigoBien = $("#hidSecFinanciamiento").val();
        
  
  var id = $("#hddRowId").val();


     //alert(strCodigoContrato, strCodigoBien, id);
//	if(IsNullOrEmpty(id)) 
//	{
//	parent.fn_mdl_mensajeIco("Seleccione un registro para poder agregar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
//	}else 
//	{
//		var vElementosAEditar = $("#jqGrid_lista_C").getGridParam('selarrrow');
//		if (vElementosAEditar.length > 1) {
//            parent.fn_mdl_mensajeIco("Seleccione un sólo registro.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");
//        } else {
//           if(vElementosAEditar != "")
     //			{
   
     parent.fn_util_AbreModal("", "Administracion/frmMantRegistroBienesContrato.aspx?csc=" + strCodigoContrato + "&csf=" + strCodigoBien, 950, 310, function() { });
				
//			}else{
//			parent.fn_mdl_mensajeIco("Seleccione un registro para poder agregar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");
//            }
//        }
	//}
}

//****************************************************************
// Funcion		:: 	fn_AbreRegistroSistemasOtros
// Descripción	::	Abre Modal de registro de Sistemas y otros
// Log			:: 	AEP - 04/10/2012
//****************************************************************
function fn_AbreRegistroSistemasOtros() {
   
        var strCodigoContrato = $("#hidNumeroContrato").val();
	    var strCodigoBien= $("#hidSecFinanciamiento").val();
	
     var id = $("#hddRowId").val();
	
//	if(IsNullOrEmpty(id)) 
//	{
//	parent.fn_mdl_mensajeIco("Seleccione un registro para poder agregar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
//	}else 
//	{

//    parent.fn_util_AbreModal("", "Administracion/frmMantRegistroBienesContrato.aspx?csc=" + strCodigoContrato + "&csf=" + strCodigoBien, 950, 310, function() { });






     //	}
     //IBK - JJM Inicio
     parent.fn_util_AbreModal("", "Administracion/frmMantRegistroBienesContrato.aspx?csc=" + strCodigoContrato + "&csf=" + strCodigoBien, 950, 310, function() { }); 
     //IBK - JJM Fin
}

//****************************************************************
// Funcion		:: 	fn_EditarInmueble
// Descripción	::	Reedirecciona a la pantalla del detalle del bien
// Log			:: 	AEP - 25/09/2012
//****************************************************************
function fn_EditarInmueble() {

    var id = $("#hddRowId").val();


    //IBK - RPH Si no selecciona ninguno toma por defecto el primero
    //debugger;
    if (id == "" && $("#jqGrid_lista_A").getGridParam("reccount") > 0) {
        //recorre para obtener el 1er Id cuando esta en otra pagina
        var rows = jQuery("#jqGrid_lista_A").jqGrid('getRowData');
        for (var i = 0; i < rows.length; i++) {
            id = rows[i]["Id"];
            break;
        }
    }
    //Fin
    
	//if(IsNullOrEmpty(id))
    if ($("#jqGrid_lista_A").getGridParam("reccount") == 0) 
	{
	    parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	}else 
	{
//		var vElementosAEditar = $("#jqGrid_lista_A").getGridParam('selarrrow');
//		if (vElementosAEditar.length > 1) {
//            parent.fn_mdl_mensajeIco("Seleccione un sólo registro.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");
//        } else {
//           if(vElementosAEditar != "")
//			{
	    //var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', vElementosAEditar[0]);


	    //IBK - RPH
	    //debugger;
	    var sPrecioVenta = fn_util_ValidaDecimal($("#hidPrecioVenta").val());	
	    	
		var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
		//window.location = "frmMantenimientoBienDetalle.aspx?co=1&csc=" + rowData.CodSolicitudCredito + "&csf=" + rowData.SecFinanciamiento + "&flag=" + rowData.FlagOrigen  + "&codestado=" + rowData.CodEstadoBien;
		window.location = "frmMantenimientoBienDetalle.aspx?co=1&csc=" + rowData.CodSolicitudCredito + "&csf=" + rowData.SecFinanciamiento + "&flag=" + rowData.FlagOrigen + "&codestado=" + rowData.CodEstadoBien + "&precioventa=" + sPrecioVenta;
//			}else{
//			parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");
//            }
        }
	}
//****************************************************************
// Funcion		:: 	fn_EditarMaquinarias
// Descripción	::	Reedirecciona a la pantalla del detalle del bien
// Log			:: 	AEP - 25/09/2012
//****************************************************************
function fn_EditarMaquinarias() {

    var id = $("#hddRowId").val();

    //IBK - RPH Si no selecciona ninguno toma por defecto el primero
    //debugger;
    if (id == "" && $("#jqGrid_lista_B").getGridParam("reccount") > 0) {
        //recorre para obtener el 1er Id cuando esta en otra pagina
        var rows = jQuery("#jqGrid_lista_B").jqGrid('getRowData');
        for (var i = 0; i < rows.length; i++) {
            id = rows[i]["Id"];
            break;
        }
    }
    //Fin 
     
	
	//if(IsNullOrEmpty(id))
    if ($("#jqGrid_lista_B").getGridParam("reccount") == 0) 
	{
	    parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	}else 
	{
		//var vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow');
//		if (vElementosAEditar.length > 1) {
//            parent.fn_mdl_mensajeIco("Seleccione un sólo registro.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");
//        } else {
//           if(vElementosAEditar != "")
	    //			{
            
            //IBK - RPH
	        var sPrecioVenta = fn_util_ValidaDecimal($("#hidPrecioVenta").val()); 
	    		
			var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
			//window.location = "frmMantenimientoBienDetalle.aspx?co=1&csc=" + rowData.CodSolicitudCredito + "&csf=" + rowData.SecFinanciamiento + "&flag=" + rowData.FlagOrigen  + "&codestado=" + rowData.CodEstadoBien;
			window.location = "frmMantenimientoBienDetalle.aspx?co=1&csc=" + rowData.CodSolicitudCredito + "&csf=" + rowData.SecFinanciamiento + "&flag=" + rowData.FlagOrigen + "&codestado=" + rowData.CodEstadoBien + "&precioventa=" + sPrecioVenta;

			//Fin
            
//			}else{
//			parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");
//            }
//        }
	}
}

//****************************************************************
// Funcion		:: 	fn_EditarVehiculos
// Descripción	::	Reedirecciona a la pantalla del detalle del bien
// Log			:: 	AEP - 25/09/2012
//****************************************************************
function fn_EditarVehiculos() {

     var id = $("#hddRowId").val();

     //IBK - RPH Si no selecciona ninguno toma por defecto el primero
     //debugger;
     if (id == "" && $("#jqGrid_lista_C").getGridParam("reccount") > 0) 
     {
         //recorre para obtener el 1er Id
         var rows = jQuery("#jqGrid_lista_C").jqGrid('getRowData');
         for (var i = 0; i < rows.length; i++) 
         {
             id = rows[i]["Id"];
             break;
         }   
     }
	 //Fin
	
	//if(IsNullOrEmpty(id))
    if ($("#jqGrid_lista_C").getGridParam("reccount") == 0) 
	{
	    parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	}else {

        //Aqui tiene que ir el valor del hidden Leasing
	    var sPrecioVenta = fn_util_ValidaDecimal($("#hidPrecioVenta").val());   //"120000"
	    //var total = fn_util_ValidaDecimal($("#hidTotal").val());   //el total del grid BD
	    
	    /*
	    var rows = jQuery("#jqGrid_lista_C").jqGrid('getRowData');
	    var total = 0;
	    for (var i = 0; i < rows.length; i++) {
	        var row = rows[i];
	        //$("#hddtotal").val(fn_util_ValidaDecimal($("#hddtotal").val()) + fn_util_ValidaDecimal(row.ValorBien));
	        total = parseFloat(total) + fn_util_ValidaDecimal(row.ValorBien);
	    }
	    */

	    var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id);
	    window.location = "frmMantenimientoBienDetalle.aspx?co=1&csc=" + rowData.CodSolicitudCredito + "&csf=" + rowData.SecFinanciamiento + "&flag=" + rowData.FlagOrigen + "&codestado=" + rowData.CodEstadoBien + "&precioventa=" + sPrecioVenta;
	    //window.location = "frmMantenimientoBienDetalle.aspx?co=1&csc=" + rowData.CodSolicitudCredito + "&csf=" + rowData.SecFinanciamiento + "&flag=" + rowData.FlagOrigen + "&codestado=" + rowData.CodEstadoBien + "&precioventa=" + sPrecioVenta + "&total=" + total;

	}
}

//****************************************************************
// Funcion		:: 	fn_EditarSistemasOtros
// Descripción	::	Reedirecciona a la pantalla del detalle del bien
// Log			:: 	AEP - 25/09/2012
//****************************************************************
function fn_EditarSistemasOtros() {

    var id = $("#hddRowId").val();

    //IBK - RPH Si no selecciona ninguno toma por defecto el primero
    //debugger;
    if (id == "" && $("#jqGrid_lista_D").getGridParam("reccount") > 0) {
        //recorre para obtener el 1er Id
        var rows = jQuery("#jqGrid_lista_D").jqGrid('getRowData');
        for (var i = 0; i < rows.length; i++) {
            id = rows[i]["Id"];
            break;
        }
    }
    //Fin 
     
	
	//if(IsNullOrEmpty(id))
    if ($("#jqGrid_lista_D").getGridParam("reccount") == 0) 
	{
	    parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	}else 
	{
//		var vElementosAEditar = $("#jqGrid_lista_D").getGridParam('selarrrow');
//		if (vElementosAEditar.length > 1) {
//            parent.fn_mdl_mensajeIco("Seleccione un sólo registro.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");
//        } else {
//           if(vElementosAEditar != "")
	    //			{

	    //IBK - RPH
	    var sPrecioVenta = fn_util_ValidaDecimal($("#hidPrecioVenta").val());
        //Fin

				var rowData = $("#jqGrid_lista_D").jqGrid('getRowData', id);
				//window.location = "frmMantenimientoBienDetalle.aspx?co=1&csc=" + rowData.CodSolicitudCredito + "&csf=" + rowData.SecFinanciamiento + "&flag=" + rowData.FlagOrigen  + "&codestado=" + rowData.CodEstadoBien;
				window.location = "frmMantenimientoBienDetalle.aspx?co=1&csc=" + rowData.CodSolicitudCredito + "&csf=" + rowData.SecFinanciamiento + "&flag=" + rowData.FlagOrigen + "&codestado=" + rowData.CodEstadoBien + "&precioventa=" + sPrecioVenta;
//			}else{
//			parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");
//            }
//        }
	}
}


//****************************************************************
// Funcion		:: 	fn_EliminarBien
// Descripción	::	Deshabilitar bienes
// Log			:: 	AEP - 08/10/2012
//****************************************************************
function fn_EliminarBien() {


    var id = $("#hddRowId").val();
	
	if(IsNullOrEmpty(id)) 
	{
	parent.fn_mdl_mensajeIco("Seleccione un registro para poder eliminar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	}else 
	{

	var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
	        	   
	if(fn_util_trim(rowData.CodEstadoBien)=='002') {
	parent.fn_mdl_mensajeIco("El bien ya se encuentra desactivado", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	}else if(fn_util_trim(rowData.CodEstadoBien)=='003') {
		parent.fn_mdl_mensajeIco("El bien se encuentra transferido", "util/images/warning.gif", "Mant. Bienes e Inmuebles");
	}else{

	parent.fn_util_AbreModal("", "Administracion/frmComentarioEliminacion.aspx?ccf=" + rowData.CodSolicitudCredito + "&csf=" + rowData.SecFinanciamiento, 500, 250, function() { }); 

	}
        }

}

//****************************************************************
// Funcion		:: 	fn_EliminarMaquinaria
// Descripción	::	Deshabilitar bienes
// Log			:: 	AEP - 08/10/2012
//****************************************************************
function fn_EliminarMaquinaria() {


    var id = $("#hddRowId").val();
	
	if(IsNullOrEmpty(id)) 
	{
	parent.fn_mdl_mensajeIco("Seleccione un registro para poder eliminar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	}else 
	{

	var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
	        	   
	
	if(fn_util_trim(rowData.CodEstadoBien)=='002') {
	parent.fn_mdl_mensajeIco("El bien ya se encuentra desactivado", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	}else if(fn_util_trim(rowData.CodEstadoBien)=='003') {
		parent.fn_mdl_mensajeIco("El bien se encuentra transferido", "util/images/warning.gif", "Mant. Bienes e Inmuebles");
	}else{

	parent.fn_util_AbreModal("", "Administracion/frmComentarioEliminacion.aspx?ccf=" + rowData.CodSolicitudCredito + "&csf=" + rowData.SecFinanciamiento, 500, 250, function() { }); 

	}
        }

}

//****************************************************************
// Funcion		:: 	fn_EliminarVehiculo
// Descripción	::	Deshabilitar bienes
// Log			:: 	AEP - 08/10/2012
//****************************************************************
function fn_EliminarVehiculo() {


    var id = $("#hddRowId").val();
	
	if(IsNullOrEmpty(id)) 
	{
	parent.fn_mdl_mensajeIco("Seleccione un registro para poder eliminar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	}else 
	{

	var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id);
	        	   
	
	if(fn_util_trim(rowData.CodEstadoBien)=='002') {
	parent.fn_mdl_mensajeIco("El bien ya se encuentra desactivado", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	}else if(fn_util_trim(rowData.CodEstadoBien)=='003') {
		parent.fn_mdl_mensajeIco("El bien se encuentra transferido", "util/images/warning.gif", "Mant. Bienes e Inmuebles");
	}else{

	parent.fn_util_AbreModal("", "Administracion/frmComentarioEliminacion.aspx?ccf=" + rowData.CodSolicitudCredito + "&csf=" + rowData.SecFinanciamiento, 500, 250, function() { }); 

	}
        }

}

//****************************************************************
// Funcion		:: 	fn_EliminarSistemasOtros
// Descripción	::	Deshabilitar bienes
// Log			:: 	AEP - 08/10/2012
//****************************************************************
function fn_EliminarSistemasOtros() {


    var id = $("#hddRowId").val();
	
	if(IsNullOrEmpty(id)) 
	{
	parent.fn_mdl_mensajeIco("Seleccione un registro para poder eliminar.", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	}else 
	{

	var rowData = $("#jqGrid_lista_D").jqGrid('getRowData', id);
	        	   
	
if(fn_util_trim(rowData.CodEstadoBien)=='002') {
	parent.fn_mdl_mensajeIco("El bien ya se encuentra desactivado", "util/images/warning.gif", "Mant. Bienes e Inmuebles");	
	}else if(fn_util_trim(rowData.CodEstadoBien)=='003') {
		parent.fn_mdl_mensajeIco("El bien se encuentra transferido", "util/images/warning.gif", "Mant. Bienes e Inmuebles");
	}else{

	parent.fn_util_AbreModal("", "Administracion/frmComentarioEliminacion.aspx?ccf=" + rowData.CodSolicitudCredito + "&csf=" + rowData.SecFinanciamiento, 500, 250, function() { }); 

	}
        }

}


function  ValidarEstadosBien(val) {
	fn_buscarInmuebles();
	if (val=="002" || val=="003")
	{
    $("#divEliminarInmueble").css('display', 'none');
	}else {
	$("#divEliminarInmueble").css('display', 'block');
	}
}

function  ValidarEstadosMaquinaria(val) {
	fn_buscarMaquinarias();
	if (val=="002" || val=="003")
	{
    $("#div_eliminar_Maquinaria").css('display', 'none');
	}else {
	$("#div_eliminar_Maquinaria").css('display', 'block');
	}
}

function  ValidarEstadosVehiculos(val) {
	ListaBienContratoVehiculos();
	if (val=="002" || val=="003")
	{
    $("#divEliminarVehiculo").css('display', 'none');
	}else {
	$("#divEliminarVehiculo").css('display', 'block');
	}
}

function  ValidarEstadosSistemas(val) {
	fn_buscarSistemasOtros();
	if (val=="002" || val=="003")
	{
    $("#divEliminarSistemas").css('display', 'none');
	}else {
	$("#divEliminarSistemas").css('display', 'block');
	}
}
//Inicio-JJM
function fn_ReloadGrillas() {   
    fn_ListagrillaDocumentosMaquinaria();
    fn_ListagrillaDocumentosSistema();
    fn_ListagrillaDocumentosTransporte();
    fn_ListagrillaDocumentos();
}
//Fin-JJM