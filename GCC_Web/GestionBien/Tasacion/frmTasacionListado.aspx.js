//****************************************************************
// Variables Globales
//****************************************************************
var bFirstClick;
var CrLf = 1;
var intPaginaActual = 1;

var strTipoDocumentoIdentificacion = new Object();
strTipoDocumentoIdentificacion.Dni = "1";
strTipoDocumentoIdentificacion.Ruc = "2";
strTipoDocumentoIdentificacion.CarnetExt = "3";
strTipoDocumentoIdentificacion.Pasaporte = "5";
strTipoDocumentoIdentificacion.Otros = "6";

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	??? - 02/11/2012
//****************************************************************
$(document).ready(function() {
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));
    //Valida Campos
    fn_inicializaCampos();
    fn_configuraGrilla();

    //On load Page (siempre al final)
    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscarContratos(true);
        }
    });

    //************************************************************
    // Función		:: Tipo Documento
    // Descripcion 	:: 	
    // Log			::
    //************************************************************

    $('#cmbTipoDocumento').change(function() {
        var strValor = $(this).val();

        $("#txtnumerodocumento").val("");
        $('#txtnumerodocumento').unbind('keypress');
        if (fn_util_trim(strValor) == strTipoDocumentoIdentificacion.Dni) {
            $('#txtnumerodocumento').validText({ type: 'number', length: 8 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoIdentificacion.Ruc) {
            $('#txtnumerodocumento').validText({ type: 'number', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoIdentificacion.CarnetExt) {
            $('#txtnumerodocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoIdentificacion.Pasaporte) {
            $('#txtnumerodocumento').validText({ type: 'alphanumeric', length: 11 });
        } else {
            $('#txtnumerodocumento').validText({ type: 'alphanumeric', length: 11 });
        }
    });


    fn_onLoadPage();

});


//****************************************************************
// Función		:: 	fn_buscarContratos
// Descripción	::	Ejecuta la búsqueda de los contratos, cuando el usuario hace click en
//                  el botón 'Buscar'.
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_buscarContratos(bSearch) {
    bFirstClick = bSearch;
    intPaginaActual = 1;
    fn_buscar();

}

function fn_limpiar() {

    $("#txtcontrato").val("");
    $("#txtcucliente").val("");
    $("#txtrazonsocial").val("");
    $("#cmbTipoDocumento").val(0);
    $("#txtnumerodocumento").val("");
    $("#cbmEstadoTasacionContrato").val(0);
    $("#cmbClasificacionBien").val(0);
    $("#cmbbanca").val(0);
    $("#TxtEjecutivoBanca").val("");
    $("#txtPeriodo").val("");
    $("#txtFechaDesde").val("");
    $("#txtFechaHasta").val("");
    $("#txtEjecutivoBanca").val("");
    $("#cmbEstadoTasacion").val("0");


}
//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {

    $("#txtcontrato").validText({ type: 'number', length: 8 });
    $("#txtcucliente").validText({ type: 'number', length: 10 });
    $('#txtrazonsocial').validText({ type: 'comment', length: 100 });
    $("#txtnumerodocumento").validText({ type: 'number', length: 11 });
    $("#txtPeriodo").validText({ type: 'number', length: 4 });
    $('#txtEjecutivoBanca').validText({ type: 'comment', length: 100 });

}


function fn_configuraGrilla() {
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            fn_ListadoAsignacionTasador();
        },
        jsonReader: {                 // Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage",      // Número de página actual.
            total: "PageCount",       // Número total de páginas.
            records: "RecordCount",   // Total de registros a mostrar.
            repeatitems: false,
            id: "CodSolicitudCredito" // Índice de la columna con la clave primaria.
        },
        colNames: ['Nº Contrato', 'Razón Social o Nombre', 'Clasificación del Bien', 'Banca', 'Ejecutivo Banca', 'Ejecutivo Leasing', 'Fecha Activación', 'Última Tasación', 'Estado del Contrato', 'Nombre del Tasador', 'Estado Tasación del contrato', '', ''],
        colModel: [
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', sortable: true, width: 25, sorttype: "string", align: "center" },
		    { name: 'ClienteRazonSocial', index: 'ClienteRazonSocial', sortable: true, width: 40, sorttype: "string", align: "left" },
		    { name: 'ClasificacionBien', index: 'ClasificacionBien', sortable: true, sorttype: "string", width: 40, align: "left" },
		    { name: 'Banca', index: 'Banca', sortable: true, sorttype: "string", width: 25, align: "left" },
		    { name: 'DesEjecutivoBanca', index: 'DesEjecutivoBanca', sortable: true, sorttype: "string", width: 30, align: "left" },
		    { name: 'EjecutivoLeasing', index: 'EjecutivoLeasing', sortable: true, width: 25, sorttype: "string", align: "left" },
            { name: 'FechaActivacion', index: 'FechaActivacion', width: 15, align: "center", sorttype: "string" },
		    { name: 'FechaUltimaTasacion', index: 'FechaUltimaTasacion', sortable: false, width: 15, align: "center", sorttype: "string" },
		    { name: 'EstadoContrato', index: 'EstadoContrato', width: 25 },
            { name: 'Tasador', index: 'Tasador', width: 30 },
            { name: 'EstadoTasacion', index: 'EstadoTasacion', width: 25 },
            { name: '', index: '', width: 2 },
            { name: 'FechaTransferencia', index: 'FechaTransferencia', hidden: true }
  	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,                      // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito', // Columna a ordenar por defecto.
        sortorder: 'desc',               // Criterio de ordenación por defecto.
        viewrecords: true,               // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hidCodigoContrato").val(rowData.CodSolicitudCredito);


        },
        ondblClickRow: function(id) {

            fn_editar();
        }
    });
    function fnEl(cellvalue, options, rowObject) {

    };

    $("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 85);

}



function fn_editar() {
    var strId = fn_util_trim($("#hidCodigoContrato").val());
    if (strId == "" || strId == null || strId == undefined) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro.", "util/images/warning.gif", "ERROR EN SELECCION");
    } else {
        parent.fn_blockUI();
        window.location = "frmTasacionRegistro.aspx?cc=" + strId;
    }

}



function VerObservacionesLegal(strCodDocContrato, el) {
    var sTitulo = "Gestion del Bien";
    var sSubTitulo = "Tasacion::Individual";
    var strId = "00000001";
    window.location = "frmTasacionRegistro.aspx?cc=" + strId;

}


function fn_ListadoAsignacionTasador() {

    if (!bFirstClick) {
        return;
    }

    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),
                          "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),
                          "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"),
                          "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"),
                          "pCodSolicitudcredito", $("#txtcontrato").val(),
                          "pCuCliente", $("#txtcucliente").val(),
                          "pRazonsolcial", $("#txtrazonsocial").val(),
                          "pTipoDocumento", $("#cmbTipoDocumento").val(),
                          "pNumerodocumento", $("#txtnumerodocumento").val(),
                          "pEstadoTasacion", $("#cmbEstadoTasacion").val(),
                          "pClasificacionBien", $("#cmbClasificacionBien").val(),
                          "pBanca", $("#cmbbanca").val(),
                          "pEjecutivoBanca", $("#txtEjecutivoBanca").val(),
                          "pPeriodo", $("#txtPeriodo").val(),
                          "pFechadesde", Fn_util_DateToString($("#txtFechaDesde").val()),
                          "pFechaHasta", Fn_util_DateToString($("#txtFechaHasta").val()),
                          "pEstadoTasacionContrato", $("#cbmEstadoTasacionContrato").val()
                        ];

    fn_util_AjaxSyncWM("frmTasacionListado.aspx/ListaContratosTasaciones",
                         arrParametros,
                         function(jsondata) {
                             jqGrid_lista_A.addJSONData(jsondata);
                             fn_doResize();
                             parent.fn_unBlockUI();

                         },
                         function(request) {
                             fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                             parent.fn_unBlockUI();

                         }
                       );
}

function fn_buscar() {

    fn_ListadoAsignacionTasador();

}