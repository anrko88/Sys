//****************************************************************
// Variables Globales
//****************************************************************
var bFirstClick;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	WCR - 16/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();
    fn_configuraGrilla();
    //    $(document).keypress(function(e) {
    //        if (e.which == 13) {
    //            fn_buscar(true);
    //        }
    //    });


    //On load Page (siempre al final)
    fn_onLoadPage();

});

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	WCR - 16/11/2012
//****************************************************************

function fn_configuraGrilla() {

    var mydata =
          [
		    { NroInstruccion: '00000110000001', TipoComprobante: "FACTURA", NroComprobante: '045045771014', NombreProveedor: "TX DEVELOPERS", Moneda: "NUEVOS SOLES", Importe: "6000.25", FechaRegistro: "15/10/2012", FechaPago: "19/10/2012", EstadoPago: "PAGADO", EstadoCobro: "COBRO PARCIAL" },
            { NroInstruccion: '00000110000001', TipoComprobante: "RECIBO POR HONORARIO", NroComprobante: '04777771088', NombreProveedor: "ESTUDIO NOTARIALES", Moneda: "DOLARES AMERICANOS", Importe: "3580.50", FechaRegistro: "10/10/2012", FechaPago: "20/10/2012", EstadoPago: "POR PAGAR", EstadoCobro: "COBRO PARCIAL" },
          	{ NroInstruccion: '00000110000002', TipoComprobante: "IRPES", NroComprobante: '11698881777', NombreProveedor: "IB-LIMA-ESTUDIO AZA", Moneda: "DOLARES AMERICANOS", Importe: "1500.00", FechaRegistro: "14/10/2012", FechaPago: "21/10/2012", EstadoPago: "PENDIENTE", EstadoCobro: "COBRO TOTAL" }
		  ];

    $("#jqGrid_lista_A").jqGrid({
        //        datatype: function() {
        //            fn_buscarListar();
        //        },
        datatype: "local",
        jsonReader: {//Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "Codigo" // Índice de la columna con la clave primaria.
        },
        colNames: ['N° Instrucción', 'Tipo Comprobante', 'Nº Comprobante', 'Razón Social o Nombre', 'Moneda', 'Importe', 'Fecha Registro', 'Fecha Pago', 'Estado Pago', 'Estado Cobro'],
        colModel: [
            { name: 'NroInstruccion', index: 'NroInstruccion', sortable: true, width: 50, sorttype: "string", align: "center" },
		    { name: 'TipoComprobante', index: 'TipoComprobante', sortable: true, width: 50, sorttype: "string", align: "left" },
		    { name: 'NroComprobante', index: 'NroComprobante', sortable: true, width: 40, sorttype: "string", align: "center" },
		    { name: 'NombreProveedor', index: 'NombreProveedor', sortable: true, sorttype: "string", width: 75, align: "left" },
		    { name: 'Moneda', index: 'Moneda', sortable: true, sorttype: "string", width: 75, align: "center" },
		    { name: 'Importe', index: 'EstadoContrato', sortable: true, width: 50, align: "right", sorttype: "float", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'FechaRegistro', index: 'FechaRegistro', sortable: true, width: 35, sorttype: "string" },
		    { name: 'FechaPago', index: 'FechaPago', sortable: true, width: 35, sorttype: "string" },
		    { name: 'EstadoPago', index: 'EstadoPago', sortable: true, sorttype: "string", width: 35, align: "center" },
		    { name: 'EstadoCobro', index: 'EstadoCobro', sortable: true, sorttype: "string", width: 50, align: "center" }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10, // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'Codigo', // Columna a ordenar por defecto.
        sortorder: 'desc', // Criterio de ordenación por defecto.
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        multiselect: true,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            //            $("#hddCodigodesembolso").val(rowData.CodSolicitudCredito);
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();

            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            //            window.location = "frmGastoRegistro.aspx?hcontrato=" + rowData.CodSolicitudCredito;

            parent.fn_unBlockUI();
        }
    });

    for (var i = 0; i <= mydata.length; i++) {
        jQuery("#jqGrid_lista_A").jqGrid('addRowData', i + 1, mydata[i]);
    }

    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 85);
}


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	WCR - 16/11/2012
//****************************************************************
function fn_inicializaCampos() {
    bFirstClick = false;

    fn_util_SeteaCalendario($('input[id*=txtFechaIni]'));
    fn_util_SeteaCalendario($('input[id*=txtFechaFin]'));
    $('#txtNroDocumento').validText({ type: 'number', length: 11 });
    $('#txtNroComprobante').validText({ type: 'number', length: 11 });
    $('#txtRazonSocial').validText({ type: 'comment', length: 50 });
}

//****************************************************************
// Funcion		:: 	fn_limpiar
// Descripción	::	Limpia las grillas 
// Log			:: 	WCR - 16/02/2012
//****************************************************************
function fn_limpiar() {
    try {
        parent.fn_blockUI();

        bFirstClick = false;

        $('#txtNroDocumento').val("");
        $('#txtNroComprobante').val("");
        $('#txtRazonSocial').val("");
        $('#txtFechaIni').val("");
        $('#txtFechaFin').val("");
        $("#cmbTipoDocumento option:first").attr('selected', 'selected');
        $("#cmbTipoComprobante option:first").attr('selected', 'selected');
        $("#cmbEstadoPago option:first").attr('selected', 'selected');
        $("#cmbEstadoCobro option:first").attr('selected', 'selected');

        $("#jqGrid_lista_A").GridUnload();
        fn_configuraGrilla();

        parent.fn_unBlockUI();
        fn_doResize();
    } catch (e) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }
}

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	Ejecuta la búsqueda de los documentos, cuando el usuario hace click en
//                  el botón 'Buscar'.
// Log			:: 	WCR - 15/02/2012
//****************************************************************
function fn_buscar(bSearch) {
    bFirstClick = bSearch;

    fn_buscarListar();
}

//****************************************************************
// Funcion		:: 	fn_buscarListarDocumentos
// Descripción	::	
// Log			:: 	WCR - 15/02/2012
//****************************************************************
function fn_buscarListar() {

    if (!bFirstClick) {
        return;
    }

    try {
        parent.fn_blockUI();

        var strNroDocumento = $('#txtNroDocumento').val() == undefined ? "" : $('#txtNroDocumento').val();
        var strNroComprobante = $('#txtNroComprobante').val() == undefined ? "" : $('#txtNroComprobante').val();
        var strRazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
        var strFechaIni = $('#txtFechaIni').val() == undefined ? "" : $('#txtFechaIni').val();
        var strFechaFin = $('#txtFechaFin').val() == undefined ? "" : $('#txtFechaFin').val();

        var strTipoDocumento = $("#cmbTipoDocumento option:selected").val() == "0" ? "" : $("#cmbTipoDocumento option:selected").val();
        var strTipoComprobante = $("#cmbTipoComprobante option:selected").val() == "0" ? "" : $("#cmbTipoComprobante option:selected").val();
        var strEstadoPago = $("#cmbEstadoPago option:selected").val() == "0" ? "" : $("#cmbEstadoPago option:selected").val();
        var strEstadoCobro = $("#cmbEstadoCobro option:selected").val() == "0" ? "" : $("#cmbEstadoCobro option:selected").val();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
                             "pTipoDocumento", strTipoDocumento,
                             "pNroDocumento", strNroDocumento,
                             "pRazonSocial", strRazonSocial,
                             "pTipoComprobante", strTipoComprobante,
                             "pNroComprobante", strNroComprobante,
                             "pEstadoPago", strEstadoPago,
                             "pFechaInicio", strFechaIni,
                             "pFechaFin", strFechaFin,
                             "pEstadoCobro", strEstadoCobro
                            ];

        //        fn_util_AjaxWM("frmGastoListado.aspx/BuscarDocumentos",
        //                       arrParametros,
        //                       function(jsondata) {
        //                           jqGrid_lista_A.addJSONData(jsondata);
        //                           parent.fn_unBlockUI();
        //                           fn_doResize();
        //                       },
        //                       function(request) {
        //                           parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR EN LA BÚSQUEDA");
        //                       });


        parent.fn_unBlockUI();
        fn_doResize();
    } catch (ex) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }
}

//****************************************************************
// Funcion		:: 	fn_abreEditar
// Descripción	::	Abre Editar Registro
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_abreEditar() {
    var id = $("#hddRowId").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "../../util/images/warning.gif", "EDITAR IMPUESTO");
    } else {
        fn_util_redirect('frmGastoRegistro.aspx');
    }
}


//****************************************************************
// Funcion		:: 	fn_abreNuevo
// Descripción	::	Abre Nuevo Registro
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_abreNuevo() {
    parent.fn_blockUI();
    fn_util_redirect("frmGastoRegistro.aspx");
    //parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/ImpuestoVehicular/frmImpuestoVehicularNuevo.aspx", 500, 300, function() { });
}

//****************************************************************
// Funcion		:: 	fn_ValidaGenerarWIO
// Descripción	::	
// Log			:: 	WCR - 21/11/2012
//****************************************************************
function fn_ValidaGenerarWIO() {
    //    var id = $("#hddRowId").val();
    //    if (IsNullOrEmpty(id)) {
    //        parent.fn_mdl_mensajeIco("Seleccione un registro para poder generar.", "../../util/images/warning.gif", "GENERAR WIO");
    //    } else {
    fn_util_redirect('frmInsGastoRegistro.aspx');
    //    }
}
