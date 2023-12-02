//****************************************************************
// Variables Globales
//****************************************************************
var bFirstClick;

var strTipoDocumento = new Object();
strTipoDocumento.Dni = "1";
strTipoDocumento.Ruc = "2";
strTipoDocumento.CarnetExt = "3";
strTipoDocumento.Pasaporte = "5";
strTipoDocumento.Otros = "6";

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	WCR - 27/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();

    $('#cmbTipoDocumento').change(function() {
        var strValor = $(this).val();
        $("#txtNroDocumento").val("");
        $('#txtNroDocumento').unbind('keypress');
        if (fn_util_trim(strValor) == strTipoDocumento.Dni) {
            $('#txtNroDocumento').validText({ type: 'number', length: 8 });
        } else if (fn_util_trim(strValor) == strTipoDocumento.Ruc) {
            $('#txtNroDocumento').validText({ type: 'number', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumento.CarnetExt) {
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumento.Pasaporte) {
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumento.Otros) {
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else {
            $('#txtNroDocumento').attr('disabled', 'disabled');
        }

    });

    $(document).keypress(function(event) {
        if (event.which || event.keyCode) {
            if ((event.which == 13) || (event.keyCode == 13)) {
                fn_buscar(true);
            }
        }
    });

    fn_configuraGrilla();
    //On load Page (siempre al final)
    fn_onLoadPage();

});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	WCR - 27/11/2012
//****************************************************************
function fn_inicializaCampos() {
    $('#txtNroContrato').validText({ type: 'number', length: 8 });
    $('#txtNroDocumento').validText({ type: 'number', length: 11 });
    $('#txtNroLote').validText({ type: 'number', length: 10 });
    $('#txtRazonSocial').validText({ type: 'comment', length: 150 });
}
//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	WCR - 16/11/2012
//****************************************************************

function fn_configuraGrilla() {

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            fn_buscarListar();
        },
        jsonReader: {//Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodSolicitudCredito,TipoRubroFinanciamiento,CodIfi,TipoRecuperacion,NumSecRecuperacion,NumSecRecupComi,CodComisionTipo" // Índice de la columna con la clave primaria.
        },
        colNames: ['N° Contrato', 'Razón Social o Nombre', 'N° Lote', 'Concepto', 'Importe Total', 'Banca en Atención', 'Ejecutivo de la Banca', 'Fecha Pago', 'Fecha Cobro', '', '', '', '', '', '', '', '', '',''],
        colModel: [
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', sortable: true, width: 50, sorttype: "string", align: "center" },
		    { name: 'ClienteRazonSocial', index: 'ClienteRazonSocial', sortable: true, sorttype: "string", width: 75, align: "left" },
		    { name: 'NroLote', index: 'NroLote', sortable: true, width: 50, sorttype: "string", align: "center" },
		    { name: 'Concepto', index: 'Concepto', sortable: true, sorttype: "string", width: 75, align: "left" },
		    { name: 'Total', index: 'Total', sortable: true, width: 50, align: "right", sorttype: "float", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'NombreBanca', index: 'NombreBanca', sortable: true, width: 50, sorttype: "string" },
		    { name: 'DesEjecutivoBanca', index: 'DesEjecutivoBanca', sortable: true, width: 50, sorttype: "string" },
		    { name: 'FecPago', index: 'FecPago', sortable: true, width: 30, align: "center" },
		    { name: 'FechaRecuperacion', index: 'FechaRecuperacion', sortable: true, width: 30, align: "center" },
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
		    { name: 'TipoRubroFinanciamiento', index: 'TipoRubroFinanciamiento', hidden: true },
		    { name: 'CodIfi', index: 'CodIfi', hidden: true },
		    { name: 'TipoRecuperacion', index: 'TipoRecuperacion', hidden: true },
		    { name: 'NumSecRecuperacion', index: 'NumSecRecuperacion', hidden: true },
		    { name: 'NumSecRecupComi', index: 'NumSecRecupComi', hidden: true },
		    { name: 'CodComisionTipo', index: 'CodComisionTipo', hidden: true },
		    { name: 'EstadoRecuperacion', index: 'EstadoRecuperacion', hidden: true },
		    { name: 'CantidadFraccion', index: 'CantidadFraccion', hidden: true },
		    { name: '', index: '', width: 1 }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10, // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito', // Columna a ordenar por defecto.
        sortorder: 'desc', // Criterio de ordenación por defecto.
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        multiselect: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hidCodSolicitudCredito").val(rowData.CodSolicitudCredito);
            $("#hidTipoRubroFinanciamiento").val(rowData.TipoRubroFinanciamiento);
            $("#hidCodIfi").val(rowData.CodIfi);
            $("#hidTipoRecuperacion").val(rowData.TipoRecuperacion);
            $("#hidNumSecRecuperacion").val(rowData.NumSecRecuperacion);
            $("#hidNumSecRecupComi").val(rowData.NumSecRecupComi);
            $("#hidCodComisionTipo").val(rowData.CodComisionTipo);
            $("#hidEstadoRecuperacion").val(rowData.EstadoRecuperacion);

        },
        ondblClickRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hidCodSolicitudCredito").val(rowData.CodSolicitudCredito);
            $("#hidTipoRubroFinanciamiento").val(rowData.TipoRubroFinanciamiento);
            $("#hidCodIfi").val(rowData.CodIfi);
            $("#hidTipoRecuperacion").val(rowData.TipoRecuperacion);
            $("#hidNumSecRecuperacion").val(rowData.NumSecRecuperacion);
            $("#hidNumSecRecupComi").val(rowData.NumSecRecupComi);
            $("#hidCodComisionTipo").val(rowData.CodComisionTipo);
            $("#hidEstadoRecuperacion").val(rowData.EstadoRecuperacion);

            fn_abreEditar();
        }
    });

    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 85);
}

//****************************************************************
// Funcion		:: 	fn_limpiar
// Descripción	::	Limpia las grillas 
// Log			:: 	WCR - 16/02/2012
//****************************************************************
function fn_limpiar() {
    try {
        // parent.fn_blockUI();

        bFirstClick = false;

        $('#txtNroContrato').val("");
        $('#txtCUCliente').val("");
        $('#txtRazonSocial').val("");
        $('#txtNroLote').val("");
        $("#cmbConcepto option:first").attr('selected', 'selected');
        $("#cmbEstadoCobro option:first").attr('selected', 'selected');

        $("#jqGrid_lista_A").GridUnload();
        fn_configuraGrilla();


        // parent.fn_unBlockUI();
        // fn_doResize();
    } catch (e) {
        //parent.fn_unBlockUI();
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
// Funcion		:: 	fn_buscarListar
// Descripción	::	
// Log			:: 	WCR - 27/11/2012
//****************************************************************
function fn_buscarListar() {

    if (!bFirstClick) {
        return;
    }

    try {
        parent.fn_blockUI();

        var strNroLote = $('#txtNroLote').val() == undefined ? "" : $('#txtNroLote').val();
        var strRazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
        var strNroContrato = $('#txtNroContrato').val() == undefined ? "" : $('#txtNroContrato').val();
        var strCUCliente = $('#txtCUCliente').val() == undefined ? "" : $('#txtCUCliente').val();
        var strFlagIndividual = $('#hidFlagIndividual').val() == undefined ? "" : $('#hidFlagIndividual').val();
        var strFlagRegistro = $('#hidFlagRegistro').val() == undefined ? "" : $('#hidFlagRegistro').val();

        var strConcepto = $("#cmbConcepto option:selected").val() == "0" ? "" : $("#cmbConcepto option:selected").val();
        var strEstadoCobro = $("#cmbEstadoCobro option:selected").val() == "0" ? "" : $("#cmbEstadoCobro option:selected").val();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
                             "pstrCUCliente", strCUCliente,
                             "pstrRazonSocial", strRazonSocial,
                             "pstrNroContrato", strNroContrato,
                             "pstrNroLote", strNroLote,
                             "pstrCodigoConcepto", strConcepto,
                             "pstrEstadoCobro", strEstadoCobro,
                             "pstrFlagIndividual", strFlagIndividual,
                             "pstrFlagRegistro", strFlagRegistro
                            ];


        fn_util_AjaxWM("frmCobroListado.aspx/BuscarCobros",
                       arrParametros,
                       function(jsondata) {
                           jqGrid_lista_A.addJSONData(jsondata);
                           parent.fn_unBlockUI();
                           fn_doResize();
                       },
                       function(request) {
                           parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "BUSCAR COBRO");
                           parent.fn_unBlockUI();
                           fn_doResize();
                       });




    } catch (ex) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }
}

//****************************************************************
// Funcion		:: 	fn_abreEditar
// Descripción	::	Abre Editar Registro
// Log			:: 	WCR - 27/11/2012
//****************************************************************
function fn_abreEditar() {
    var strCodSolicitudCredito = $("#hidCodSolicitudCredito").val();
    var strTipoRubroFinanciamiento = $("#hidTipoRubroFinanciamiento").val();
    var strCodIfi = $("#hidCodIfi").val();
    var strTipoRecuperacion = $("#hidTipoRecuperacion").val();
    var strNumSecRecuperacion = $("#hidNumSecRecuperacion").val();
    var strNumSecRecupComi = $("#hidNumSecRecupComi").val();
    var strCodComisionTipo = $("#hidCodComisionTipo").val();
    var strEstadoRecuperacion = $("#hidEstadoRecuperacion").val();
    if (IsNullOrEmpty(strCodSolicitudCredito)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "EDITAR COBRO");
    } else {
        if ($("#hidEstadoRecuperacion").val() != 'H') {
            var strParam = 'co=EDITAR&csc=' + strCodSolicitudCredito + '&trf=' + strTipoRubroFinanciamiento;
            strParam = strParam + '&ci=' + strCodIfi + '&tre=' + strTipoRecuperacion + '&nsr=' + strNumSecRecuperacion;
            strParam = strParam + '&nsrc=' + strNumSecRecupComi + '&cct=' + strCodComisionTipo + '&ere=' + strEstadoRecuperacion;            
            if (document.cookie.indexOf('coCobro') > -1) {

                var dtNow = new Date();
                var dtExpirationDate = new Date();
                dtExpirationDate.setDate(dtNow.getDate() - 7);
                document.cookie = 'coCobro=1; Expires = ' + dtExpirationDate.toUTCString(); //((Sun, 01-Jan-70 00:00:01 GMT;';                
            }
            fn_util_redirect('frmCobroMasivoRegistro.aspx?' + strParam);
        }
        else {
            parent.fn_mdl_mensajeIco("Solo se puede editar en estado pendiente.", "util/images/warning.gif", "EDITAR COBRO");
        }
    }

}

//****************************************************************
// Funcion		:: 	fn_abreNuevo
// Descripción	::	Abre Nuevo Registro
// Log			:: 	WCR - 27/11/2012
//****************************************************************
function fn_abreNuevo() {
    parent.fn_blockUI();
    fn_util_redirect("frmCobroMasivoRegistro.aspx?co=NUEVO&csc=&trf=&ci=&tre=&nsr=0&nsrc=0&cct=");
}
