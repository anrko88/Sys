var blnPrimeraBusqueda;
//var list;
//debugger;
////****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	RPH - 12/12/2012
//****************************************************************
$(document).ready(function() {


    //   ebugger;
    //Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    //Carga Grilla
    //    debugger;
    fn_cargaGrilla();

    fn_inicializaCampos();


    //Busca con Enter
    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscarSeguros(true);
        }
    });

    
    //On load Page (siempre al final)
    fn_onLoadPage();
});





//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {

    //Valida Tipo de Datos
    blnPrimeraBusqueda = false;
    $('#txtContrato').validText({ type: 'number', length: 8 });
    $('#txtNroPoliza').validText({ type: 'number', length: 10 });

    $('#txtCiaSeguros').validText({ type: 'comment', length: 100 });
}

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla Listado de Cotizaciones
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() 
{
   // debugger;
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_realizaBusquedaSeguros();
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
        colNames: ['','Contrato', 'Tipo Documento', 'Nro. Documento', 'Razón Social','Valor Prenda', 'Valor Endosado', 'Prima Neta', 'Tipo Valor', 'Tipo Bien', 'Tipo Declaración', 'Tipo Seguro', 'Nro. Poliza', 'Seguro', 'Ramo', '',''],
        colModel: [
                { name: 'NroPrendaRehder', index: 'NroPrendaRehder', hidden: true },
                { name: 'CodGarantiaBanco', index: 'CodGarantiaBanco', sortable: true, sorttype: "string", width: 20, align: "center" },
                { name: 'TipoDocSubprestatario', index: 'TipoDocSubprestatario', sortable: true, sorttype: "string", width: 20, align: "left", defaultValue: "" },
                { name: 'NroDocSubprestatario', index: 'NroDocSubprestatario', sortable: true, sorttype: "string", width: 20, align: "left", defaultValue: "" },
                { name: 'NombreCliente', index: 'NombreCliente', sortable: true, sorttype: "string", width: 40, align: "left", defaultValue: "" },
                { name: 'ValorPrenda', index: 'ValorPrenda', sortable: true, width: 20, sorttype: "float", align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'ValorEndosado', index: 'ValorEndosado', sortable: true, width: 20, sorttype: "float", align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'PrimaNeta', index: 'PrimaNeta', sortable: true, width: 20, sorttype: "float", align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'TipoValor', index: 'TipoValor', sortable: true, sorttype: "string", width: 15, align: "center", defaultValue: "" },
                { name: 'TipoBien', index: 'TipoBien', sortable: true, sorttype: "string", width: 30, align: "center", defaultValue: "" },
                { name: 'TipoDeclaracion', index: 'TipoDeclaracion', sortable: true, sorttype: "string", width: 20, align: "center", defaultValue: "" },
                { name: 'TipoSeguro', index: 'TipoSeguro', sortable: true, sorttype: "string", width: 30, align: "center", defaultValue: "" },
                { name: 'NroPoliza', index: 'NroPoliza', sortable: true, sorttype: "string", width: 20, align: "center", defaultValue: "" },
                { name: 'CiaSeguros', index: 'CiaSeguros', sortable: true, sorttype: "string", width: 35, align: "center", defaultValue: "" },
                { name: 'TipoRamo', index: 'TipoRamo', sortable: true, sorttype: "string", width: 45, align: "center", defaultValue: "" },
                { name: 'FIniPoliza', index: 'FIniPoliza', hidden: true },
                { name: 'FFinPoliza', index: 'FFinPoliza', hidden: true }
                
                
        ],
        width: glb_intWidthPantalla - 70,
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'CodGarantiaBanco',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hdNroContrato").val(rowData.CodGarantiaBanco);
        },

        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hdNroContrato").val(rowData.CodGarantiaBanco);
            $("#hdNroPoliza").val(rowData.NroPoliza);
            $("#hdTipoSeguro").val(rowData.TipoSeguro);
            $("#hdCiaSeguro").val(rowData.CiaSeguros);
            $("#hdNroPrenda").val(rowData.NroPrendaRehder);
            $("#hdCliente").val(rowData.NombreCliente);
            $("#hdFini").val(rowData.FIniPoliza);
            $("#hdFfin").val(rowData.FFinPoliza);
            verDetalle();
            //window.location = "frmCotizacionRegistro.aspx?hdNroContrato=" + rowData.CodGarantiaBanco;
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
}

function verDetalle()
{
    var sNroContrato = $("#hdNroContrato").val();
    var sNroPoliza = $("#hdNroPoliza").val();
    var sTipoSeguro = $("#hdTipoSeguro").val();
    var sCiaSeguro = $("#hdCiaSeguro").val();
    var sNomCliente = $("#hdCliente").val();
    var NroPrenda = $("#hdNroPrenda").val();
    var fini = $("#hdFini").val();
    var ffin = $("#hdFfin").val();
    parent.fn_util_AbreModal("", "GestionBien/frmSegurosDetalle.aspx?CodigoContrato=" + sNroContrato + "&NroPoliza=" + sNroPoliza + "&TipoSeguro=" + sTipoSeguro + "&CiaSeguro=" + sCiaSeguro + "&NroPrenda=" + NroPrenda + "&Cliente=" + sNomCliente+ "&fini=" +fini+ "&ffin=" + ffin, 600, 594, function() { });
}



////Busqueda de Seguros
function fn_buscarSeguros(pblnBusqueda) {
    blnPrimeraBusqueda = pblnBusqueda;
	intPaginaActual = 1;
    fn_realizaBusquedaSeguros();
}

function fn_realizaBusquedaSeguros() {
//debugger;
    if (!blnPrimeraBusqueda) {
        return;
    }

    try {
        parent.fn_blockUI();

        var txtContrato = $('#txtContrato').val() == undefined ? "" : $('#txtContrato').val();
        var txtNroPoliza = $('#txtNroPoliza').val() == undefined ? "" : $('#txtNroPoliza').val();
        var txtCiaSeguros = $('#txtCiaSeguros').val() == undefined ? "" : $('#txtCiaSeguros').val();
        var cmbTipoValor = $('#cmbTipoValor').val() == undefined ? "" : $('#cmbTipoValor').val();
        var txtFechaIngresoIni = $('#txtFechaIni').val() == undefined ? "" : $('#txtFechaIni').val();
        var txtFechaIngresoFin = $('#txtFechaFin').val() == undefined ? "" : $('#txtFechaFin').val();
        var txtTipoBien = $('#txtTipoBien').val() == undefined ? "" : $('#txtTipoBien').val();
        var cmbTipoSeguro = $('#cmbTipoSeguro').val() == undefined ? "" : $('#cmbTipoSeguro').val();
        

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
                             "pstrNroContrato", txtContrato,
                             "pstrNroPoliza", txtNroPoliza,
                             "pstrCiaSeguros", txtCiaSeguros,
                             "pstrTipoValor", cmbTipoValor,
                             "pstrFechaIngresoIni", txtFechaIngresoIni,
                             "pstrFechaIngresoFin", txtFechaIngresoFin,
                             "pstrTipobien", txtTipoBien,
                             "pstrTipoSeguro", cmbTipoSeguro
                            ];

        fn_util_AjaxWM("frmSegurosListado.aspx/ListarSeguros",
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
// Descripción	::	Limpiar Datos
// Log			:: 	RPH - 25/02/2012
//****************************************************************
function fn_limpiar() {
    blnPrimeraBusqueda = false;

    $('#txtContrato').val("");
    $('#txtNroPoliza').val("");
    $('#txtCiaSeguros').val("");
    $('#txtFechaIni').val("");
    $('#txtFechaFin').val("");
    $('#txtTipoBien').val("");

    $('#cmbTipoValor').val("0");
    $('#cmbTipoSeguro').val("0");
    
    $("#jqGrid_lista_A").GridUnload();
    fn_cargaGrilla();

}