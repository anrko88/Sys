var blnPrimeraBusqueda;
var intPaginaActual = 1;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
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

    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_BuscarContratoCotizacion(true);
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
	blnPrimeraBusqueda=false;
    $("#txtNroCotizacion").val('');
    $("#txtCuCliente").val('');
    $('#txtRazonsocial').val('');
    $("#txtContrato").val('');
    $("#txtFechaIni").val('');
    $("#txtFechaFin").val('');
    $("#cmbEjecutivo option").eq("0").attr("selected", "selected");
    $("#cmbZonalCombo option").eq("0").attr("selected", "selected");
    $("#cmbClasificacionContrato option").eq("0").attr("selected", "selected");

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
        intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_ListarContratoCotizacion();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Nº Contrato', 'Nº Cotización', 'CU Cliente', 'Razón Social o Nombre', 'Clasificación de Bien', 'Moneda', 'Precio Venta', 'Estado', 'Fecha'],
        colModel: [
		            { name: 'CODSOLICITUDCREDITO', index: 'CODSOLICITUDCREDITO', width: 30, align: "center" },
		            { name: 'CODIGOCOTIZACION', index: 'CODIGOCOTIZACION', width: 30, align: "center" },
		            { name: 'CODUNICO', index: 'CODUNICO', width: 30, align: "center" },
		            { name: 'NOMBRECLIENTE', index: 'NOMBRECLIENTE', width: 80, align: "left" },
		            { name: 'NOMBRECLASIFICACIONBIEN', index: 'NOMBRECLASIFICACIONBIEN', width: 50, align: "center" },
		            { name: 'NOMBREMONEDA', index: 'NOMBREMONEDA', width: 30, align: "center" },
		            { name: 'PRECIOVENTA', index: 'PRECIOVENTA', width: 30, align: "right", sorttype: "float", formatter: Fn_util_ReturnValidDecimal2 },
		            { name: 'ESTADOCONTRATO', index: 'ESTADOCONTRATO', width: 30, align: "center" },
		            { name: 'FECHACONTRATO', index: 'FECHACONTRATO', width: 30, align: "center", sorttype: "date", formatter: fn_util_ValidaStringFecha }
	              ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'CODSOLICITUDCREDITO',
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
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            window.location = "frmCheckListLegalRegistro.aspx?cc=" + rowData.CODSOLICITUDCREDITO;
        }
    });
    $("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });

    $("#jqGrid_lista_A").setGridWidth($(window).width() - 70);
    $("#search_jqGrid_lista_A").hide();
}


//****************************************************************
// Función		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
    // Valida Tipo de Datos
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
function fn_BuscarContratoCotizacion(pblnBusqueda) {
    blnPrimeraBusqueda = pblnBusqueda;
    intPaginaActual = 1;
    
    fn_ListarContratoCotizacion();
}

function fn_ListarContratoCotizacion() {

	if (!blnPrimeraBusqueda) {
		return;

	} else {

		//parent.fn_blockUI();

		var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),
			"pCurrentPage", intPaginaActual,
			"pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"),
			"pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"),
			"pNroCotizacion", $("#txtNroCotizacion").val(),
			"pNroContrato", $("#txtContrato").val(),
			"pCUCliente", $("#txtCuCliente").val(),
			"pRazonSocialCli", $("#txtRazonSocial").val(),
			"pCodEjecutivo", $("#cmbEjecutivo").val(),
			"pClasifBien", $("#cmbClasificacionContrato").val(),
			"pZonal", $("#cmbZonal").val(),
			"pFechaInicio", $("#txtFechaIni").val(),
			"pFechaFin", $("#txtFechaFin").val()
		];

		fn_util_AjaxWM("frmCheckListLegalListado.aspx/ListadoContratoCotizacion",
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
Log			:: 	KCC - 17/05/2012
***************************************************************** */
function fn_Editar() {
    var strId = fn_util_trim($("#hidCodigoContrato").val());
    if (strId == "" || strId == null || strId == undefined) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro.", "util/images/warning.gif", "ERROR EN SELECCION");
    } else {
        parent.fn_blockUI();
        window.location = "frmCheckListLegalRegistro.aspx?cc=" + strId;
    }
}