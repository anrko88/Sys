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
		    { NroInstruccion: '00000110000001', EstadoInstruccion: "EN ELABORACION", CantidadDocumentos: '5', TotalAbonoSoles: "30250.50", TotalAbonoDolares: "12320.12" },
            { NroInstruccion: '00000110000001', EstadoInstruccion: "PENDIENTE DE EJECUCION", CantidadDocumentos: '2', TotalAbonoSoles: "98014.25", TotalAbonoDolares: "6478.50" },
          	{ NroInstruccion: '00000110000002', EstadoInstruccion: "APROBADA", CantidadDocumentos: '3', TotalAbonoSoles: "10470.00", TotalAbonoDolares: "2680.00" }
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
        colNames: ['N° Instrucción', 'Estado Instruccion', 'Cant. Documentos', 'Total Abono S/.', 'Total Abono US$'],
        colModel: [
            { name: 'NroInstruccion', index: 'NroInstruccion', sortable: true, width: 40, sorttype: "string", align: "center" },
		    { name: 'EstadoInstruccion', index: 'EstadoInstruccion', sortable: true, width: 70, sorttype: "string", align: "left" },
		    { name: 'CantidadDocumentos', index: 'CantidadDocumentos', sortable: true, width: 25, sorttype: "string", align: "center" },
		    { name: 'TotalAbonoSoles', index: 'TotalAbonoSoles', sortable: true, sorttype: "string", width: 25, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'TotalAbonoDolares', index: 'TotalAbonoDolares', sortable: true, sorttype: "string", width: 25, align: "right", formatter: Fn_util_ReturnValidDecimal2 }
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
// Log			:: 	WCR - 21/11/2012
//****************************************************************
function fn_limpiar() {
    try {
        parent.fn_blockUI();

        bFirstClick = false;

        $('#txtNroInstruccion').val("");
        $("#cmbEstadoInstruccion option:first").attr('selected', 'selected');

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

        var strNroInstruccion = $('#txtNroInstruccion').val() == undefined ? "" : $('#txtNroInstruccion').val();
        var strEstadoInstruccion = $("#cmbEstadoInstruccion option:selected").val() == "0" ? "" : $("#cmbEstadoInstruccion option:selected").val();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
                             "pNroInstruccion", strNroInstruccion,
                             "pEstadoInstruccion", strEstadoInstruccion
                            ];

        //        fn_util_AjaxWM("frmInsGastoListado.aspx/BuscarInstruccion",
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
//    var id = $("#hddRowId").val();
//    if (IsNullOrEmpty(id)) {
//        parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "../../util/images/warning.gif", "EDITAR IMPUESTO");
//    } else {
        fn_util_redirect('frmInsGastoRegistro.aspx');
//    }
}
