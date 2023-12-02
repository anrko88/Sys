var blnPrimeraBusqueda;

//****************************************************************
// Funcion		:: 	JQUERY - Listado Check List
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {
    //Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    //Carga Grilla
    fn_cargaGrilla();

    //Inicializa Campos
    fn_inicializaCampos();

    //Busca con Enter
    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscarContratoCotizacion(true);
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
    blnPrimeraBusqueda = false;
    
    $("#txtNroCotizacion").val('');
    $("#txtCuCliente").val('');
    $('#txtRazonsocial').val('');
    $("#txtContrato").val('');
    $("#txtFechaIni").val('');
    $("#txtFechaFin").val('');
    $("#cmbEjecutivo option").eq("0").attr("selected", "selected");
    $("#cmbZonalCombo option").eq("0").attr("selected", "selected");
    $("#cmbClasificacionbien option").eq("0").attr("selected", "selected");

    $("#jqGrid_lista_A").GridUnload();
    fn_cargaGrilla();
}

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() {
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            fn_ListarContratoCotizacion();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "CODIGOCOTIZACION"
        },
        colNames: ['Nº Cotización', 'Nº Contrato', 'CU Cliente', 'Razón Social o Nombre', 'Clasificación de Bien', 'Moneda', '', 'Estado Cotización', 'Estado Contrato', 'F. Cotización', ''],
        colModel: [
		            { name: 'CODIGOCOTIZACION', index: 'CODIGOCOTIZACION', width: 30, align: "center" },
		            { name: 'CODSOLICITUDCREDITO', index: 'CODSOLICITUDCREDITO', width: 30, align: "center" },
		            { name: 'CODUNICO', index: 'CODUNICO', width: 30, align: "center" },
		            { name: 'NOMBRECLIENTE', index: 'NOMBRECLIENTE', width: 80, align: "left" },
		            { name: 'NOMBRECLASIFICACIONBIEN', index: 'NOMBRECLASIFICACIONBIEN', width: 50, align: "center" },
		            { name: 'NOMBREMONEDA', index: 'NOMBREMONEDA', width: 30, align: "center" },
		            { name: 'CODIGOESTADOCOTIZACION', index: 'CODIGOESTADOCOTIZACION', hidden: true },
		            { name: 'NOMBREESTADOCOTIZACION', index: 'NOMBREESTADOCOTIZACION', width: 30, align: "center" },
		            { name: 'ESTADOCONTRATO', index: 'ESTADOCONTRATO', width: 30, align: "center" },
		            { name: 'FECHAINGRESO', index: 'FECHAINGRESO', width: 30, align: "center", sorttype: "date", formatter: fn_util_ValidaStringFecha },
                    { name: 'VERSIONCOTIZACION', index: 'VERSIONCOTIZACION', hidden: true }
	              ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'CODIGOCOTIZACION',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hidCodigoContrato").val(rowData.CODSOLICITUDCREDITO);

            $("#hddId").val(id);
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
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
    $("#txtNroCotizacion").validText({ type: 'number', length: 8 });
    $("#txtCuCliente").validText({ type: 'number', length: 10 });
    $('#txtRazonsocial').validText({ type: 'comment', length: 100 });
    $("#txtContrato").validText({ type: 'number', length: 8 });
}

/****************************************************************
Funcion		:: 	fn_ListarContratoCotizacion
Descripción	::	Listar
Log			:: 	KCC - 17/05/2012
**************************************************************** */
function fn_buscarContratoCotizacion(pblnBusqueda) {
    blnPrimeraBusqueda = pblnBusqueda;
    
    fn_ListarContratoCotizacion();
}

function fn_ListarContratoCotizacion() {
    if (!blnPrimeraBusqueda) {
        return;

    } else {
        var ejecutivo = $("#cmbEjecutivo option:selected").val() == "0" ? "" : $("#cmbEjecutivo option:selected").val();
        var clasificacionbien = $("#cmbClasificacionbien option:selected").val() == "0" ? "" : $("#cmbClasificacionbien option:selected").val();
        var zonalCombo = $("#cmbZonalCombo option:selected").val() == "0" ? "" : $("#cmbZonalCombo option:selected").val();
        
        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),
			"pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),
			"pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"),
			"pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"),
			"pNroCotizacion", $("#txtNroCotizacion").val(),
			"pNroContrato", $("#txtContrato").val(),
			"pCuCliente", $("#txtCuCliente").val(),
			"pRazonSocialCli", $("#txtRazonsocial").val(),
			"pCodEjecutivo", ejecutivo,
			"pClasifBien", clasificacionbien,
			"pZonal", zonalCombo,
			"pFechaInicio", $("#txtFechaIni").val(),
			"pFechaFin", $("#txtFechaFin").val()
		];

        fn_util_AjaxWM("frmBandejaListado.aspx/ListadoContratoCotizacionRep",
			            arrParametros,
			            function(jsondata) {
			                jqGrid_lista_A.addJSONData(jsondata);
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
}

/*****************************************************************
Funcion		:: 	fn_Buscar
Descripción	::	Listado
Log			:: 	KCC - 16/05/2012
***************************************************************** */
function fn_Buscar() {
    fn_ListarContratoCotizacion();
}

/*****************************************************************
Funcion		:: 	fn_Editar
Descripción	::	Abrir para Editar
Log			:: 	IJM - 17/05/2012
***************************************************************** */
function fn_Editar() {
    var strId = fn_util_trim($("#hidCodigoContrato").val());
    
    if (strId == "" || strId == null || strId == undefined) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro.", "util/images/warning.gif", "ERROR EN SELECCION");
    } else {
        parent.fn_blockUI();
        window.location = "frmCheckListComercialRegistro.aspx?cc=" + strId;
    }
}

//****************************************************************
// Función		:: 	fn_abreSeguimiento
// Descripción	::	Muestra la ventana modal de representantes del cliente disponibles, permitiendo seleccionar al usuario 
//                  cuales agregar al contrato.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_abreSeguimientoContrato() {
    var strId = $("#hddId").val();

    if (strId == "" || strId == null || strId == undefined) {
        parent.fn_mdl_mensajeIco("Seleccione un contrato para ver el seguimiento.", "util/images/warning.gif", "ERROR EN SELECCIÓN");
        return;
    }
    
    var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', $("#hddId").val());
    if (parseInt(rowData.CODIGOESTADOCOTIZACION, 10) > 4) {
        var sTitulo = "Contrato";
        var sSubTitulo = "Reporte :: Contrato";
        var strCodDocContrato = rowData.CODSOLICITUDCREDITO;

        parent.fn_util_AbreModal(sSubTitulo, "Comun/frmContratoSeguimientoListado.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodigoContrato=" + strCodDocContrato + "&Add=False", 600, 500, function() { });
    } else {
        parent.fn_mdl_mensajeIco("La cotización aún no tiene estados del contrato.", "util/images/warning.gif", "ERROR");
    }
}

//****************************************************************
// Función		:: 	fn_abreSeguimientoCotizacion
// Descripción	::	Abre Seguimiento de Cotización
// Log			:: 	EBL - 22/05/2012
//****************************************************************
function fn_abreSeguimientoCotizacion() {
    var strId = $("#hddId").val();

    if (strId == "" || strId == null || strId == undefined) {
        parent.fn_mdl_mensajeIco("Seleccione una cotización para ver el seguimiento.", "util/images/warning.gif", "ERROR EN SELECCION");
        return;
    }

    var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', $("#hddId").val());

    parent.fn_util_AbreModal("Cotización :: Seguimiento", "Cotizacion/frmCotizacionSeguimiento.aspx?hddCodigoCotizacion=" + rowData.CODIGOCOTIZACION, 700, 400, function() { });
}

//****************************************************************
// Función		:: 	fn_VerContrato
// Descripción	::	Visualiza el contrato en estado sólo lectura.
// Log			:: 	EBL - 22/05/2012
//****************************************************************
function fn_VerContrato() {
    var strId = $("#hddId").val();

    if (strId == "" || strId == null || strId == undefined) {
        parent.fn_mdl_mensajeIco("Seleccione una fila para ver el contrato.", "util/images/warning.gif", "ERROR EN SELECCIÓN");
    } else {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', $("#hddId").val());
        
        if (parseInt(rowData.CODIGOESTADOCOTIZACION, 10) > 4) {
            var title = "Contrato :: Seguimiento";

            parent.fn_util_AbreModal(title, "Formalizacion/frmContratoVer.aspx?hddCodigo=" + rowData.CODSOLICITUDCREDITO, 1100, 550, function() { });
        } else {
            parent.fn_mdl_mensajeIco("La cotización aún no tiene un contrato para hacer seguimiento.", "util/images/warning.gif", "ERROR");
        }
    }
}

//****************************************************************
// Función		:: 	fn_VerCotizacion
// Descripción	::	Visualiza la cotización en estado sólo lectura.
// Log			:: 	EBL - 22/05/2012
//****************************************************************
function fn_VerCotizacion() {
    var strId = $("#hddId").val();

    if (strId == "" || strId == null || strId == undefined) {
        parent.fn_mdl_mensajeIco("Seleccione una fila para ver la cotización.", "util/images/warning.gif", "ERROR EN SELECCIÓN");
    } else {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', $("#hddId").val());
        var strCodigoCotizacion = rowData.CODIGOCOTIZACION;
        var strVersionCotizacion = rowData.VERSIONCOTIZACION;
        
        if (fn_util_trim(strCodigoCotizacion) != "" && fn_util_trim(strCodigoCotizacion) != "") {
            var title = "Cotización :: Seguimiento";

            parent.fn_util_AbreModal(title, "Cotizacion/frmCotizacionVer.aspx?cc=" + strCodigoCotizacion + "&cv=" + strVersionCotizacion, 980, 550, function() { });
        } else {
            parent.fn_mdl_mensajeIco("Debe seleccionar un registro", "util/images/error.gif", "FALLO EN SELECCIÓN");
        }
    }
}



//****************************************************************
// Función		:: 	fn_abreSeguimientoGlobal
// Descripción	::	Muestra la ventana modal de representantes del cliente disponibles, permitiendo seleccionar al usuario 
//                  cuales agregar al contrato.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_abreSeguimientoGlobal() {
    var strId = $("#hddId").val();

    if (strId == "" || strId == null || strId == undefined) {
        parent.fn_mdl_mensajeIco("Seleccione un contrato para ver el seguimiento.", "util/images/warning.gif", "ERROR EN SELECCIÓN");
        return;
    }
    
    var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', $("#hddId").val());
    var sTitulo = "Seguimiento";
    var sSubTitulo = "Reporte :: Seguimiento Global";
    var strCodDocContrato = rowData.CODSOLICITUDCREDITO;
    var strCodDocCotizacion = rowData.CODIGOCOTIZACION;
     
    parent.fn_util_AbreModal(sSubTitulo, "Reportes/frmSeguimiento.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodigoContrato=" + strCodDocContrato + "&hddCodigoCotizacion=" + strCodDocCotizacion + "&Add=False", 950, 600, function() { });
    
}
