//****************************************************************
// Variables Globales
//****************************************************************
var gstrGrid = '';
var gstrCodPadre = 0;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	??? - 02/11/2012
//****************************************************************
$(document).ready(function() {
    //fn_util_SeteaCalendario($('input[id*=txtFecha]'));
    //Valida Campos
    fn_inicializaCampos();
    fn_configuraGrilla();
    //On load Page (siempre al final)
    fn_onLoadPage();

});

function fn_configuraGrilla() {
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            fn_ListadoContratoBienTasador();
        },
        jsonReader: {                 // Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage",      // Número de página actual.
            total: "PageCount",       // Número total de páginas.
            records: "RecordCount",   // Total de registros a mostrar.
            repeatitems: false,
            id: "CodContratoTasacion" // Índice de la columna con la clave primaria.
        },
        colNames: ['Fecha Asignación', 'Última Tasación', 'Nombre Tasador', 'Estado Tasación del Contrato', 'Cantidad de Bienes', '', '', ''],
        colModel: [
		            { name: 'FechaAsignacion', index: 'FechaAsignacion', sortable: true, width: 10, sorttype: "string", align: "center" },
		            { name: 'FechaUltimaTasacion', index: 'FechaUltimaTasacion', sortable: true, width: 10, sorttype: "string", align: "center" },
		            { name: 'DesTasador', index: 'DesTasador', sortable: true, sorttype: "string", width: 25, align: "left" },
		            { name: 'EstadoTasacion', index: 'EstadoTasacion', sortable: true, sorttype: "string", width: 25, align: "center" },
		            { name: 'CantidadProducto', index: 'CantidadProducto', sortable: true, sorttype: "string", width: 20, align: "center" },
	                { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
		            { name: 'CodContratoTasacion', index: 'CodContratoTasacion', hidden: true },
		            { name: 'CodContratoTasacionUltimo', index: 'CodContratoTasacionUltimo', hidden: true }
                   ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,                      // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CodContratoTasacion', // Columna a ordenar por defecto.
        sortorder: 'desc',               // Criterio de ordenación por defecto.
        viewrecords: true,               // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        afterInsertRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);

            if (parseInt(rowData.CantidadProducto) == 0) {
                $("#" + id + " td.sgcollapsed", $("#jqGrid_lista_A")[0]).unbind('click').html('').removeProp('class');
            }
        },
        subGrid: true,
        subGridOptions: {
            "openicon": "ui-icon-arrowreturn-1-e"
        },
        subGridRowExpanded: function(subgrid_id, row_id) {
            var rowDataPadre = $("#jqGrid_lista_A").jqGrid('getRowData', row_id);

            var subgrid_table_id, pager_id;
            subgrid_table_id = subgrid_id + "_t";
            pager_id = "p_" + subgrid_table_id;

            //var strTitImporte = pSubgrid_table_id + 'Importe';

            $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pager_id + "'></div>");
            if (rowDataPadre.CodContratoTasacion == rowDataPadre.CodContratoTasacionUltimo) {
                gstrGrid = subgrid_table_id;
                gstrCodPadre = rowDataPadre.CodContratoTasacionUltimo;
                jQuery("#" + subgrid_table_id).jqGrid({
                    datatype: function() {
                        fn_ListadoHistoricoContratoBienTasador(subgrid_table_id, rowDataPadre.CodContratoTasacion);
                    },
                    //datatype: "local",
                    jsonReader:
					{
					    root: "Items",
					    page: "CurrentPage",
					    total: "PageCount",
					    records: "RecordCount",
					    repeatitems: false,
					    id: "SolSec"
					},
					colNames: ['Tipo Bien', 'Descripción Bien', 'Valor del Bien', 'Tasador Asignado', 'Fecha Prox Tasación', 'Motivo No Tasación', 'Estado Tasación del bien', 'Cantidad de Bienes', 'Asignar Tasador', '', '', '', '', 'Fecha de Transferencia', 'Fecha Asignación por Bien','', ''],
                    colModel: [
		            { name: 'TipoBien', index: 'TipoBien', sortable: true, width: 40, sorttype: "string", align: "left" },
		            { name: 'Comentario', index: 'Comentario', sortable: true, width: 25, sorttype: "string", align: "left" },
		            { name: 'ValorBien', index: 'ValorBien', sortable: true, sorttype: "string", width: 20, align: "right", formatter: fn_ValorBien },
		            { name: 'DesTasador', index: 'DesTasador', sortable: true, sorttype: "string", width: 35, align: "left" },
		            { name: 'FechaProxTasacion', index: 'FechaProxTasacion', sortable: true, sorttype: "string", width: 20, align: "center" },
	                { name: 'MotivoNoTasacion', index: 'MotivoNoTasacion', sortable: true, sorttype: "string", width: 25, align: "left" },
		            { name: 'DescEstadoTasacionBien', index: 'DescEstadoTasacionBien', sortable: false, width: 25, align: "left", sorttype: "string" },
                    { name: 'CantidadProducto', index: 'CantidadProducto', sortable: false, width: 20, align: "center", sorttype: "string" },
                    { name: 'Editar', index: 'Editar', width: 15, align: "center", sortable: false, formatter: EditarIndividual },
                    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 20, align: "center", sortable: false, hidden: true },
                    { name: 'SecFinanciamiento', index: 'SecFinanciamiento', width: 20, align: "center", sortable: false, hidden: true },
                    { name: 'CodTasador', index: 'CodTasador', width: 20, align: "center", sortable: false, hidden: true },
                    { name: 'CodTasacion', index: 'CodTasacion', align: "center", sortable: false, hidden: true },
                    { name: 'FechaTransferencia', index: 'FechaTransferencia', width: 20, align: "center", sortable: false },
                    { name: 'FechaEncargo', index: 'FechaEncargo', width: 27, align: "center" },
                    { name: '', index: '', width: 2, align: "center" }, 
                    { name: 'CodMotivoNoTasacion', index: 'CodMotivoNoTasacion',align: "center", sortable: true, hidden: true }
                   ],
                    width: glb_intWidthPantalla - 105,
                    height: '100%',
                    editurl: 'clientArray',
                    pager: pager_id,
                    editurl: 'clientArray',
                    loadtext: 'Cargando datos...',
                    emptyrecords: 'No hay resultados',
                    rowNum: 10,
                    rowList: [10, 20, 30],
                    sortname: 'SecFinanciamiento',
                    sortorder: 'desc',
                    viewrecords: true,
                    gridview: true,
                    //autowidth: true,
                    altRows: true,
                    altclass: 'gridAltClass',
                    multiselect: false,
                    gridComplete: function(id) {
                        fn_doResize();
                    }
                });

                function EditarIndividual(cellvalue, options, rowObject) {
                    var obj = '';
                    if (rowObject.FechaTransferencia == '') {
                        var param = "'" + rowObject.CodSolicitudCredito + "','" + rowObject.SecFinanciamiento + "','" + rowObject.CodTasador + "' ,'" + rowObject.CodTasacion + "','" + rowObject.FechaProxTasacion + "','" + rowObject.CodMotivoNoTasacion + "','" + subgrid_table_id + "','" + rowObject.SolSec + "'";
                        //alert(param);
                        var obj = "<img src='../../Util/images/ico_acc_editar.gif' alt='" + cellvalue + "' title='Editar' width='17px' style='cursor: pointer;cursor: hand;' onclick=\"javascript:fn_editarTasador(this," + param + ");\" />";

                    }
                    return obj;
                };
            }
            else {
                jQuery("#" + subgrid_table_id).jqGrid({
                    datatype: function() {
                        fn_ListadoHistoricoContratoBienTasador(subgrid_table_id, rowDataPadre.CodContratoTasacion);
                    },
                    //datatype: "local",
                    jsonReader:
					{
					    root: "Items",
					    page: "CurrentPage",
					    total: "PageCount",
					    records: "RecordCount",
					    repeatitems: false,
					    id: "SolSec"
					},
                    colNames: ['Tipo Bien', 'Descripción Bien', 'Valor del Bien', 'Tasador Asignado', 'Fecha Prox Tasación', 'Motivo No Tasación', 'Estado Tasación del bien', 'Cantidad de Bienes', '', '', '', '', 'Fecha de Transferencia','Fecha Asignación por Bien','', ''],
                    colModel: [
		            { name: 'TipoBien', index: 'TipoBien', sortable: true, width: 40, sorttype: "string", align: "left" },
		            { name: 'Comentario', index: 'Comentario', sortable: true, width: 25, sorttype: "string", align: "left" },
		            { name: 'ValorBien', index: 'ValorBien', sortable: true, sorttype: "string", width: 20, align: "right", formatter: fn_ValorBien },
		            { name: 'DesTasador', index: 'DesTasador', sortable: true, sorttype: "string", width: 35, align: "left" },
		            { name: 'FechaProxTasacion', index: 'FechaProxTasacion', sortable: true, sorttype: "string", width: 20, align: "center" },
	                { name: 'MotivoNoTasacion', index: 'MotivoNoTasacion', sortable: true, sorttype: "string", width: 25, align: "left" },
		            { name: 'DescEstadoTasacionBien', index: 'DescEstadoTasacionBien', sortable: false, width: 25, align: "left", sorttype: "string" },
                    { name: 'CantidadProducto', index: 'CantidadProducto', sortable: false, width: 20, align: "center", sorttype: "string" },                    
                    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 20, align: "center", sortable: false, hidden: true },
                    { name: 'SecFinanciamiento', index: 'SecFinanciamiento', width: 20, align: "center", sortable: false, hidden: true },
                    { name: 'CodTasador', index: 'CodTasador', width: 20, align: "center", sortable: false, hidden: true },
                    { name: 'CodTasacion', index: 'CodTasacion', align: "center", sortable: false, hidden: true },
                    { name: 'FechaTransferencia', index: 'FechaTransferencia', width: 20, align: "center", sortable: false },
                    { name: 'FechaEncargo', index: 'FechaEncargo', width: 25, align: "center" },
                    { name: '', index: '', width: 2, align: "center" }, 
                    { name: 'CodMotivoNoTasacion', index: 'CodMotivoNoTasacion', align: "center", sortable: true, hidden: true }

                   ],
                    width: glb_intWidthPantalla - 105,
                    height: '100%',
                    editurl: 'clientArray',
                    pager: pager_id,
                    editurl: 'clientArray',
                    loadtext: 'Cargando datos...',
                    emptyrecords: 'No hay resultados',
                    rowNum: 10,
                    rowList: [10, 20, 30],
                    sortname: 'SecFinanciamiento',
                    sortorder: 'desc',
                    viewrecords: true,
                    gridview: true,
                    //autowidth: true,
                    altRows: true,
                    altclass: 'gridAltClass',
                    multiselect: false,
                    gridComplete: function(id) {
                        fn_doResize();
                    }
                });

            }

            jQuery("#" + subgrid_table_id).jqGrid('navGrid', "#" + pager_id, { edit: false, add: false, del: false, search: false })

            //**************************************************
            // ValorBien
            //**************************************************
            function fn_ValorBien(cellvalue, options, rowObject) {
                var strValorBien = fn_util_ValidaDecimal(rowObject.ValorBien);
                return "" + fn_util_ValidaMonto(strValorBien, 2);
            };
        }
    });

    $("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false, search: false });
    //$("#search_jqGrid_lista_A").hide();
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 85);
}

function fn_editarTasador(pthis, pCodSolicitudcredito, pSecfinanciamiento, pCodTasador, pCodTasacion, pFProxtasacion, pcodMotivonoTasacion, pGrid, pID) {

    var sTitulo = "Gestión del Bien";
    var sSubTitulo = "Tasación::Individual";
    var strParam = '?Titulo=' + sTitulo;
    strParam = strParam + '&spCodSolicitudcredito=' + pCodSolicitudcredito;
    strParam = strParam + '&spSecfinanciamiento=' + pCodSolicitudcredito;
    strParam = strParam + '&spCodTasador=' + pCodTasador;
    strParam = strParam + '&spCodTasacion=' + pCodTasacion;
    strParam = strParam + '&spFProxtasacion=' + pFProxtasacion;
    strParam = strParam + '&spcodMotivonoTasacion=' + pcodMotivonoTasacion;
    strParam = strParam + '&Add=False';
    strParam = strParam + '&spGrid=' + pGrid;
    strParam = strParam + '&spID=' + pID;

    parent.fn_util_AbreModal(sSubTitulo, "GestionBien/Tasacion/FrmTasacionIndividualActualiza.aspx" + strParam, 550, 220, function() { });
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {

    $("#txtContrato").validText({ type: 'number', length: 8 });
    $("#txtcucliente").validText({ type: 'number', length: 8 });
    $('#txtrasonSocial').validText({ type: 'comment', length: 50 });


    $('#txtContrato').prop('readonly', true);
    $('#txtcucliente').prop('readonly', true);
    $('#txtrasonSocial').prop('readonly', true);
    $('#cmbEstadoContrato').prop('readonly', true);
    $('#cmbClasificacionBien').prop('readonly', true);
    $('#txtFechaActivacion').prop('readonly', true);
    $('#cmbBanca').prop('readonly', true);
    $('#txtdesejecutivobanca').prop('readonly', true);
    $('#cmbMoneda').prop('readonly', true);


    $('#txtContrato').attr('class', 'css_input_inactivo');
    $('#txtcucliente').attr('class', 'css_input_inactivo');
    $('#txtrasonSocial').attr('class', 'css_input_inactivo');
    $('#cmbEstadoContrato').attr('class', 'css_input_inactivo');
    $('#cmbClasificacionBien').attr('class', 'css_input_inactivo');
    $('#txtFechaActivacion').attr('class', 'css_input_inactivo');
    $('#cmbBanca').attr('class', 'css_input_inactivo');
    $('#txtdesejecutivobanca').attr('class', 'css_input_inactivo');
    $('#cmbMoneda').attr('class', 'css_input_inactivo');

    $("#cmbEstadoContrato").attr('disabled', 'disabled');
    $("#cmbClasificacionBien").attr('disabled', 'disabled');
    $("#cmbMoneda").attr('disabled', 'disabled');
    $("#cmbBanca").attr('disabled', 'disabled');
}


function fn_enviarcarta() {

    var sMensaje = "¿Desea enviar las cartas?";
    var sTitulo = "Tasacion individual";
    parent.fn_mdl_confirma(
        sMensaje, //Mensaje - Obligatorio
        function() {
            parent.fn_blockUI();
            var arrParametros = ["pCodSolicitudcredito", $("#txtContrato").val()
                                    ];
            fn_util_AjaxSyncWM("FrmTasacionIndividual.aspx/enviarcarta",
                arrParametros,
                function(jsondata) {
                    fn_ShowMensaje('El envio de carta se realizó satisfactoriamente', null);
                    parent.fn_unBlockUI();
                    fn_ListadoHistoricoContratoBienTasador(gstrGrid, gstrCodPadre);
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                    parent.fn_unBlockUI();

                }
                );

        }, // ACCION SI - Obligatorio
        "Util/images/question.gif", // Imagen - puede ser nulo
        function() { }, // ACCION SI - Obligatorio
        sTitulo
        );
}

function fn_volver() {
    window.location = "frmTasacionAsignacionListado.aspx";
}

//****************************************************************
// Funcion		:: 	fn_ListadoContratoBienTasador
// Descripción	::	
// Log			:: 	WCR - 17/01/2013
//****************************************************************
function fn_ListadoContratoBienTasador() {
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"),
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"),
                         "pCodSolicitudcredito", $("#txtContrato").val()
                        ];

    fn_util_AjaxSyncWM("FrmTasacionIndividual.aspx/ListaContratoBienTasacion",
        arrParametros,
        function(jsondata) {

            jqGrid_lista_A.addJSONData(jsondata);
            fn_doResize();
            parent.fn_unBlockUI();


        },
        function(request) {
            parent.fn_util_alert(jQuery.parseJSON(request.responseText).Message);
            parent.fn_unBlockUI();

        }
    );
}

//****************************************************************
// Funcion		:: 	fn_ListadoHistoricoContratoBienTasador
// Descripción	::	
// Log			:: 	WCR - 17/01/2013
//****************************************************************
function fn_ListadoHistoricoContratoBienTasador(pGrid, pCodContratoTasacion) {
    var arrParametros = ["pPageSize", fn_util_getJQGridParam(pGrid, "rowNum"),
                         "pCurrentPage", fn_util_getJQGridParam(pGrid, "page"),
                         "pSortColumn", fn_util_getJQGridParam(pGrid, "sortname"),
                         "pSortOrder", fn_util_getJQGridParam(pGrid, "sortorder"),
                         "pCodSolicitudcredito", $("#txtContrato").val(),
                         "pCodContratoTasacion", pCodContratoTasacion,
                        ];

    fn_util_AjaxSyncWM("FrmTasacionIndividual.aspx/ListaHistoricoContratoBienTasacion",
        arrParametros,
        function(jsondata) {
            var subgrid = jQuery("#" + pGrid)[0];
            subgrid.addJSONData(jsondata);
            fn_doResize();
            parent.fn_unBlockUI();


        },
        function(request) {
            parent.fn_util_alert(jQuery.parseJSON(request.responseText).Message);
            parent.fn_unBlockUI();

        }
    );
}

//****************************************************************
// Funcion		:: 	fn_ShowMensaje
// Descripción	::	
// Log			:: 	WCR - 18/01/2013
//****************************************************************
function fn_ShowMensaje(pMensaje, pobjRetorno) {
    if (pobjRetorno != null) {
        var objRetorno = new Object();
        objRetorno = pobjRetorno;
        fn_ListadoHistoricoContratoBienTasador(gstrGrid, gstrCodPadre);
//        $("#" + objRetorno.Grid).jqGrid('setCell', objRetorno.ID, 'CodTasador', objRetorno.CodTasador);
//        $("#" + objRetorno.Grid).jqGrid('setCell', objRetorno.ID, 'DesTasador', objRetorno.Tasador);
//        $("#" + objRetorno.Grid).jqGrid('setCell', objRetorno.ID, 'FechaProxTasacion', objRetorno.FecProxTasacion);
//        $("#" + objRetorno.Grid).jqGrid('setCell', objRetorno.ID, 'CodMotivoNoTasacion', objRetorno.CodMotivo);
//        $("#" + objRetorno.Grid).jqGrid('setCell', objRetorno.ID, 'MotivoNoTasacion', objRetorno.Motivo);
    }
    fn_util_MuestraLogPage(pMensaje, "I");
}
