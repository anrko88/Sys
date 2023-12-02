var blnPrimeraBusqueda;
var intPaginaActual = 1;

var objTipoEnvio = { A60DiasxVencer: '001', A30DiasxVencer: '002' };
var objFiltros = { NumeroContrato: '', CUCliente: '', RazonSocial: '', Clasificacionbien: '', Tipobien: '', strTipoEnvio: '', FechaFiltro: '', Demanda: '', PlacaActual: '', NroSerie: '' };
var C_ESTADOOC_Total = '003';

var idsOfSelectedRows = [];
var idsOfNoSelectedRows = [];
var idsAllContratos = [];
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	WCR - 04/01/2013
//****************************************************************

$(document).ready(function() {
    //Carga Grilla
    fn_cargaGrilla();
    //$("#jqGrid_listado").setGridWidth($(window).width() - 65);

    //On load Page (siempre al final)
    fn_onLoadPage();
    fn_InicializarCampos();

    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscar(true);
        }
    });

    //-------------------------------------------
    //Valida Change del ClasificacionBien
    //-------------------------------------------
    $('#ddlClasificacionbien').change(function() {
        var strValor = $(this).val();

        var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL104", "pstrCodigoGenerico", strValor];
        var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

        if (arrResultado.length > 0) {
            if (arrResultado[0] == "0") {
                $('#ddlTipobien').html(arrResultado[1]);
            } else {
                var strError = arrResultado[1];
                fn_mdl_alert(strError.toString(), function() { });
            }
        }
    });

    $("#txtFechaFiltro").datepicker("destroy");
    $('#txtFechaFiltro').removeClass();
    $('#txtFechaFiltro').addClass('css_input_inactivo');
    $('#txtFechaFiltro').attr('readonly', 'readonly');

    $('#ddlCartaEnviar').change(function() {
        var strTipoEnvio = $(this).val();
        arrTipoEnvio = strTipoEnvio.split('|');
        if ((arrTipoEnvio[0] == objTipoEnvio.A60DiasxVencer) || (arrTipoEnvio[0] == objTipoEnvio.A30DiasxVencer)) {
            $('#txtFechaFiltro').removeAttr('readonly');
            $('#txtFechaFiltro').removeClass();
            $('#txtFechaFiltro').addClass('css_input');
            fn_util_SeteaCalendario($('input[id*=txtFechaFiltro]'));
            fn_util_SeteaObligatorio($("#txtFechaFiltro"), "calendar");
            $('#txtFechaFiltro').val($('#hidFechaActual').val());
        }
        else {
            $("#txtFechaFiltro").datepicker("destroy");
            $('#txtFechaFiltro').removeClass();
            $('#txtFechaFiltro').addClass('css_input_inactivo');
            $('#txtFechaFiltro').attr('readonly', 'readonly');
            $('#txtFechaFiltro').val('');
        }
    });
});

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 26/09/2012
//****************************************************************
function fn_cargaGrilla() {

    var updateIdsOfSelectedRows = function(id, isSelected) {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
        $("#hidCodigoSolicitudCredito").val(rowData.CodSolicitudCredito);
        $("#hidCodOpcionCompra").val(rowData.CodOpcionCompra);

        if ($('#jqg_jqGrid_lista_A_' + id).is(':disabled')) {
            $('#jqg_jqGrid_lista_A_' + id).attr("checked", false);
            if ($('#jqg_jqGrid_lista_A_' + id).is(':checked')) {
                jQuery("#jqGrid_lista_A").jqGrid().setSelection(id, false);
            }
        }

        var index = $.inArray(id, idsOfSelectedRows);
        if (!isSelected && index >= 0) {
            idsOfSelectedRows.splice(index, 1);
        } else if (index < 0) {
            idsOfSelectedRows.push(rowData.CodSolicitudCredito);
        }

        var indexU = $.inArray(id, idsOfNoSelectedRows);
        if (isSelected && indexU >= 0) {
            idsOfNoSelectedRows.splice(indexU, 1);
        } else if ((indexU < 0) && (!isSelected)) {
            idsOfNoSelectedRows.push(rowData.CodSolicitudCredito);
        }
    };

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_buscarContrato();
        },
        //        datatype: "local",
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "CodSolicitudCredito"
        },
        colNames: ['N° Contrato', 'CU Cliente', 'Razón Social o Nombre', 'Tipo de Bien', 'Ubigeo', 'Fecha Vencimiento', 'Estado OC', 'Última Carta OC', 'Demanda', 'Comisión', '', '', '', '',''],
        colModel: [
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 25, sorttype: "string", align: "center" },
            { name: 'CodUnico', index: 'CodUnico', width: 25, sorttype: "string", align: "center" },
		    { name: 'ClienteRazonSocial', index: 'ClienteRazonSocial', width: 80, sorttype: "string", align: "left" },
        	{ name: 'NombreTipoBien', index: 'NombreTipoBien', width: 80, align: "left", sorttype: "string" },
		    { name: 'Ubigeo', index: 'Ubigeo', width: 80, align: "left", sorttype: "string" },
		    { name: 'FechaVencimiento', index: 'FechaVencimiento', width: 30, align: "center", sorttype: "string" },
		    { name: 'EstadoOC', index: 'EstadoOC', width: 25, align: "center" },
		    { name: 'UltimoEnvio', index: 'UltimoEnvio', width: 60, align: "center", sorttype: "string" },
        	{ name: 'Demanda', index: 'Demanda', width: 20, align: "center", sortable: false, sorttype: "string", formatter: fn_Demanda },
		    { name: 'Editar', index: 'Editar', width: 28, align: "center", sortable: false, sorttype: "string", formatter: fn_Editar },
		    { name: 'CantidadDemanda', index: 'CantidadDemanda', hidden: true },
		    { name: 'CodEstadoOC', index: 'CodEstadoOC', hidden: true },
		    { name: 'CodOpcionCompra', index: 'CodOpcionCompra', hidden: true },
		    { name: 'EnvioRegistrados', index: 'EnvioRegistrados', hidden: true },
		    { name: 'ClienteDomicilioLegal', index: 'ClienteDomicilioLegal', hidden: true }
	    ],
        height: '100%',
        width: glb_intWidthPantalla - 70,
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
        loadonce: false,
        altclass: 'gridAltClass',
        multiselect: true,
        onSelectRow: updateIdsOfSelectedRows,
        onSelectAll: function(aRowids, isSelected) {
            var i, count, id;
            for (i = 0, count = aRowids.length; i < count; i++) {
                id = aRowids[i];
                updateIdsOfSelectedRows(id, isSelected);
            }
        },
        ondblClickRow: function(id) {
            fn_OpcionCompra(id);
        }
    });

    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

    function fn_Demanda(cellvalue, options, rowObject) {
        var obj = '';
        if (parseInt(rowObject.CantidadDemanda, 10) > 0) {
            obj = "<img src='../../Util/images/ok.gif' alt='" + cellvalue + "' title='Demanda' width='20px' onclick=\"javascript:fn_AbrirDemanda('" + rowObject.CodSolicitudCredito + "');\" style='cursor: pointer;cursor: hand;' />";
        }
        return obj;
    };

    function fn_Editar(cellvalue, options, rowObject) {
        var obj = '';
        if (rowObject.CodEstadoOC != C_ESTADOOC_Total) {
            obj = "<img src='../../Util/images/ico_acc_editar.gif' alt='" + cellvalue + "' title='Editar' width='20px' onclick=\"javascript:fn_ComisionOC('" + rowObject.CodSolicitudCredito + "'," + rowObject.CodOpcionCompra + ",1);\" style='cursor: pointer;cursor: hand;' />";
        } else {
        obj = "<img src='../../Util/images/ico_acc_ver.gif' alt='" + cellvalue + "' title='Editar' width='20px' onclick=\"javascript:fn_ComisionOC('" + rowObject.CodSolicitudCredito + "'," + rowObject.CodOpcionCompra + ",0);\" style='cursor: pointer;cursor: hand;' />";
        }
        return obj;
    };

}

//****************************************************************
// Función		:: 	fn_CalcularCapital
// Descripción	::	
// Log			:: 	WCR - 11/12/2012
//****************************************************************
function fn_DeshabilitarCheck() {

    $("#jqGrid_lista_A").jqGrid('resetSelection');

    var strTipoEnvio = $('#ddlCartaEnviar').val() == "0" ? "" : $('#ddlCartaEnviar').val();
    if (fn_util_trim(strTipoEnvio) != '') {
        var ids = $("#jqGrid_lista_A").jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', ids[i]);
            if (rowData.CodEstadoOC != C_ESTADOOC_Total) {
                var strEnvioRegistrados = rowData.EnvioRegistrados;
                if (strEnvioRegistrados != '') {
                    var arrTipoEnvio = strTipoEnvio.split('|');
                    if (strEnvioRegistrados.indexOf(arrTipoEnvio[0]) > -1) {
                        $('#jqg_jqGrid_lista_A_' + ids[i]).attr("disabled", true);
                        idsOfNoSelectedRows.push(ids[i]);
                    }
                    else {
                        var arrEnvioRegistrados = strEnvioRegistrados.split('*');
                        var arrDatos = arrEnvioRegistrados[0].split(';');
                        if (parseFloat(arrDatos[1]) > parseFloat(arrTipoEnvio[1])) {
                            $('#jqg_jqGrid_lista_A_' + ids[i]).attr("disabled", true);
                            idsOfNoSelectedRows.push(ids[i]);
                        }
                        else {
                            if (parseFloat(rowData.CantidadDemanda) == 0) {
                                var index = $.inArray(ids[i], idsOfNoSelectedRows);
                                if (index < 0) {
                                    jQuery("#jqGrid_lista_A").setSelection(ids[i], true);
                                    //idsOfSelectedRows.push(rowData.CodSolicitudCredito);
                                }
                            }
                            else { idsOfNoSelectedRows.push(ids[i]); }
                        }
                    }
                }
                else {
                    if (parseFloat(rowData.CantidadDemanda) == 0) {
                        var index = $.inArray(ids[i], idsOfNoSelectedRows);
                        if (index < 0) {
                            jQuery("#jqGrid_lista_A").setSelection(ids[i], true);
                            //idsOfSelectedRows.push(rowData.CodSolicitudCredito);
                        }
                    }
                    else { idsOfNoSelectedRows.push(ids[i]); }
                }
            }
            else {
                $('#jqg_jqGrid_lista_A_' + ids[i]).attr("disabled", true);
                idsOfNoSelectedRows.push(ids[i]);
            }
        }
    }
    else {
        for (var i = 0; i < idsOfSelectedRows.length; i++) {
            jQuery("#jqGrid_lista_A").jqGrid().setSelection(idsOfSelectedRows[i], true);
        }
    }
}


//************************************************************
// Función		:: 	fn_ComisionOC
// Descripcion 	:: 	Editar datos de Opción de Compra
// Log			:: 	WCR - 04/01/2013
//************************************************************
function fn_ComisionOC(pNumeroContrato, pCodOpcionCompra, pAccion) {
    var strTitulo = 'Opción de Compra:: Editar';
    if (pAccion == '0') { strTitulo = 'Opción de Compra:: Consulta'; }

    parent.fn_util_AbreModal(strTitulo, 'GestionBien/OpcionCompra/frmOpcionCompraEditar.aspx?csc=' + pNumeroContrato + '&coc=' + pCodOpcionCompra + '&op=' + pAccion, 500, 300, function() { });
}

//************************************************************
// Función		:: 	fn_AbrirDemanda
// Descripcion 	:: 	Abrir listado de Demanda
// Log			:: 	WCR - 04/01/2013
//************************************************************
function fn_AbrirDemanda(pNumContrato) {
    parent.fn_util_AbreModal('Opción de Compra:: Bienes', 'GestionBien/OpcionCompra/frmListadoBienes.aspx?csc=' + pNumContrato, 950, 550, function() { });
}

//************************************************************
// Función		:: 	fn_AbrirDemanda
// Descripcion 	:: 	
// Log			:: 	WCR - 04/01/2013
//************************************************************

function fn_EnviarCarta() {

    var strNumeroContrato = $('#txtContrato').val() == undefined ? "" : $('#txtContrato').val();
    var strCUCliente = $('#txtCUCliente').val() == undefined ? "" : $('#txtCUCliente').val();
    var strRazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
    var strClasificacionbien = $('#ddlClasificacionbien').val() == "0" ? "" : $('#ddlClasificacionbien').val();
    var strTipobien = $('#ddlTipobien').val() == "0" ? "" : $('#ddlTipobien').val();
    var strTipoEnvio = $('#ddlCartaEnviar').val() == "0" ? "" : $('#ddlCartaEnviar').val();
    var strDemanda = '';
    if ($('#rdSi').is(':checked')) { strDemanda = '1'; }
    if ($('#rdNo').is(':checked')) { strDemanda = '0'; }
    var strPlacaActual = $('#txtPlacaActual').val() == undefined ? "" : $('#txtPlacaActual').val();
    var strNroSerie = $('#txtNroSerie').val() == undefined ? "" : $('#txtNroSerie').val();
    var strFechaFiltro = $('#txtFechaFiltro').val() == undefined ? "" : $('#txtFechaFiltro').val();
    var intFiltroDiferente = 0;

    if (objFiltros.NumeroContrato != strNumeroContrato) { intFiltroDiferente = 1; }
    if (objFiltros.CUCliente != strCUCliente) { intFiltroDiferente = 1; }
    if (objFiltros.RazonSocial != strRazonSocial) { intFiltroDiferente = 1; }
    if (objFiltros.Clasificacionbien != strClasificacionbien) { intFiltroDiferente = 1; }
    if (objFiltros.Tipobien != strTipobien) { intFiltroDiferente = 1; }
    if (objFiltros.strTipoEnvio != strTipoEnvio) { intFiltroDiferente = 1; }
    if (objFiltros.Demanda != strDemanda) { intFiltroDiferente = 1; }
    if (objFiltros.PlacaActual != strPlacaActual) { intFiltroDiferente = 1; }
    if (objFiltros.NroSerie != strNroSerie) { intFiltroDiferente = 1; }
    if (objFiltros.FechaFiltro != strFechaFiltro) { intFiltroDiferente = 1; }

    for (var x = 0; x < idsOfNoSelectedRows.length; x++) {
        var index = $.inArray(idsOfNoSelectedRows[x], idsAllContratos);
        if (index >= 0) { idsAllContratos.splice(index, 1); }
    }

    if (intFiltroDiferente == 1) {
        parent.fn_mdl_mensajeIco("Los filtros han cambiado, volver a realizar la búsqueda.", "util/images/warning.gif", "Opción de Compra");
    }
    else if (strTipoEnvio == '') { parent.fn_mdl_mensajeIco("Debe seleccionar la carta a enviar.", "util/images/warning.gif", "Opción de Compra"); }
    else if (idsAllContratos.length == 0) { parent.fn_mdl_mensajeIco("Seleccione por lo menos un registro para realizar el envio de la carta.", "util/images/warning.gif", "Opción de Compra"); }
    else {
        parent.fn_blockUI();
        var strEnvioCarta = '';
        for (var i = 0; i < idsAllContratos.length; i++) {
            strEnvioCarta = strEnvioCarta + idsAllContratos[i] + '*';
        }
                
        var arrTipoEnvio = strTipoEnvio.split('|');
        var arrParametros = ["pstrCodTipoEnvio", arrTipoEnvio[0],
                             "pstrOrdenEnvio", arrTipoEnvio[1],
                             "pstrContratos", strEnvioCarta
                            ];

                    fn_util_AjaxWM("frmOpcionCompraListado.aspx/GrabaEnvioCarta",
                    arrParametros,
                    function(result) {
                        parent.fn_unBlockUI();
                        if (fn_util_trim(result) == "0") {
                            parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "GRABAR ENVIO");
                        } else {
    	                    //fn_fn_enviarCarta();
                            fn_ShowMensaje('El envio de carta se registro correctamente');
                            parent.fn_unBlockUI();
                        }
                    },
                     function(resultado) {
                         parent.fn_unBlockUI();
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "GRABAR ENVIO");
                     }
                );
    }



}

//************************************************************
// Función		:: 	fn_OpcionCompra
// Descripcion 	:: 	
// Log			:: 	WCR - 04/01/2013
//************************************************************
function fn_OpcionCompra(pRow) {
    if (pRow == null) {
        var vElementos = $("#jqGrid_lista_A").getGridParam('selarrrow');
        if (vElementos.length == 0) { parent.fn_mdl_mensajeIco("Seleccione un registro para realizar la opción de compra.", "util/images/warning.gif", "Opción de Compra"); }
        else if (vElementos.length == 1) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', vElementos);
            fn_util_globalRedirect('/GestionBien/OpcionCompra/frmOpcionCompraRegistro.aspx?csc=' + rowData.CodSolicitudCredito + '&coc=' + rowData.CodOpcionCompra);
        }
        else { parent.fn_mdl_mensajeIco("Solo se puede realizar  una opción de compra a la vez.", "util/images/warning.gif", "Opción de Compra"); }
    }
    else {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', pRow);
        fn_util_globalRedirect('/GestionBien/OpcionCompra/frmOpcionCompraRegistro.aspx?csc=' + rowData.CodSolicitudCredito + '&coc=' + rowData.CodOpcionCompra);
    }
}

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	
// Log			:: 	WCR - 14/06/2012
//****************************************************************
function fn_buscar(pblnBusqueda) {
    blnPrimeraBusqueda = pblnBusqueda;
    intPaginaActual = 1;
    idsOfSelectedRows = [];
    idsOfNoSelectedRows = [];
    idsAllContratos = [];
    fn_buscarContrato();
    fn_buscarContratoTodo();
}

function fn_buscarContrato() {
    if (!blnPrimeraBusqueda) {
        return;

    } else {

        parent.fn_blockUI();

        var strNumeroContrato = $('#txtContrato').val() == undefined ? "" : $('#txtContrato').val();
        var strCUCliente = $('#txtCUCliente').val() == undefined ? "" : $('#txtCUCliente').val();
        var strRazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
        var strClasificacionbien = $('#ddlClasificacionbien').val() == "0" ? "" : $('#ddlClasificacionbien').val();
        var strTipobien = $('#ddlTipobien').val() == "0" ? "" : $('#ddlTipobien').val();
        var strTipoEnvio = $('#ddlCartaEnviar').val() == "0" ? "" : $('#ddlCartaEnviar').val();
        var strDemanda = '';
        if ($('#rdSi').is(':checked')) { strDemanda = '1'; }
        if ($('#rdNo').is(':checked')) { strDemanda = '0'; }
        var strPlacaActual = $('#txtPlacaActual').val() == undefined ? "" : $('#txtPlacaActual').val();
        var strNroSerie = $('#txtNroSerie').val() == undefined ? "" : $('#txtNroSerie').val();
        var strFechaFiltro = $('#txtFechaFiltro').val() == undefined ? "" : $('#txtFechaFiltro').val();

        objFiltros.NumeroContrato = strNumeroContrato;
        objFiltros.CUCliente = strCUCliente;
        objFiltros.RazonSocial = strRazonSocial;
        objFiltros.Clasificacionbien = strClasificacionbien;
        objFiltros.Tipobien = strTipobien;
        objFiltros.strTipoEnvio = strTipoEnvio;
        objFiltros.Demanda = strDemanda;
        objFiltros.PlacaActual = strPlacaActual;
        objFiltros.NroSerie = strNroSerie;
        objFiltros.FechaFiltro = strFechaFiltro;

        var intValid = 0;
        if ((strTipoEnvio == objTipoEnvio.A60DiasxVencer) || (strTipoEnvio == objTipoEnvio.A30DiasxVencer)) {
            if (strFechaFiltro == '') {
                parent.fn_mdl_mensajeIco("Debe ingresar un fecha.", "util/images/warning.gif", "Opción de Compra");
                parent.fn_unBlockUI();
                intValid = 1;
                fn_doResize();
            }
        }
        if (intValid == 0) {
            var arrTipoEnvio = strTipoEnvio.split('|');
            strFechaFiltro = fn_FormatoFecha(strFechaFiltro);
            var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", intPaginaActual,    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
                             "pNumeroContraro", strNumeroContrato,
                             "pCUCliente", strCUCliente,
                             "pRazonSocial", strRazonSocial,
                             "pCodClasificacionBien", strClasificacionbien,
                             "pCodTipoBien", strTipobien,
    	 	                 "pCodTipoEnvio", arrTipoEnvio[0],
    	 	                 "pDemanda", strDemanda,
                             "pPlacaActual", strPlacaActual,
                             "pNroSerie", strNroSerie,
                             "pFechaFiltro", strFechaFiltro
                            ];

            fn_util_AjaxWM("frmOpcionCompraListado.aspx/BuscarOpcionCompra",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_A.addJSONData(jsondata);
                        fn_DeshabilitarCheck();
                        parent.fn_unBlockUI();
                        fn_doResize();
                    },
                    function(request) {
                        parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "BUSCAR OPCION COMPRA");
                        parent.fn_unBlockUI();
                        fn_doResize();
                    }
                   );
        }
    }

}

function fn_buscarContratoTodo() {

    var strTipoEnvio = $('#ddlCartaEnviar').val() == "0" ? "" : $('#ddlCartaEnviar').val();
    if (strTipoEnvio != '') {
        var strNumeroContrato = $('#txtContrato').val() == undefined ? "" : $('#txtContrato').val();
        var strCUCliente = $('#txtCUCliente').val() == undefined ? "" : $('#txtCUCliente').val();
        var strRazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
        var strClasificacionbien = $('#ddlClasificacionbien').val() == "0" ? "" : $('#ddlClasificacionbien').val();
        var strTipobien = $('#ddlTipobien').val() == "0" ? "" : $('#ddlTipobien').val();

        var strDemanda = '';
        if ($('#rdSi').is(':checked')) { strDemanda = '1'; }
        if ($('#rdNo').is(':checked')) { strDemanda = '0'; }
        var strPlacaActual = $('#txtPlacaActual').val() == undefined ? "" : $('#txtPlacaActual').val();
        var strNroSerie = $('#txtNroSerie').val() == undefined ? "" : $('#txtNroSerie').val();
        var strFechaFiltro = $('#txtFechaFiltro').val() == undefined ? "" : $('#txtFechaFiltro').val();

        var intValid = 0;
        if ((strTipoEnvio == objTipoEnvio.A60DiasxVencer) || (strTipoEnvio == objTipoEnvio.A30DiasxVencer)) {
            if (strFechaFiltro == '') {
                parent.fn_mdl_mensajeIco("Debe ingresar un fecha.", "util/images/warning.gif", "Opción de Compra");
                intValid = 1;
                fn_doResize();
            }
        }
        if (intValid == 0) {
            var arrTipoEnvio = strTipoEnvio.split('|');
            strFechaFiltro = fn_FormatoFecha(strFechaFiltro);
            var arrParametros = ["pNumeroContraro", strNumeroContrato,
                                 "pCUCliente", strCUCliente,
                                 "pRazonSocial", strRazonSocial,
                                 "pCodClasificacionBien", strClasificacionbien,
                                 "pCodTipoBien", strTipobien,
    	 	                     "pCodTipoEnvio", arrTipoEnvio[0],
    	 	                     "pDemanda", strDemanda,
                                 "pPlacaActual", strPlacaActual,
                                 "pNroSerie", strNroSerie,
                                 "pFechaFiltro", strFechaFiltro
                            ];

            fn_util_AjaxWM("frmOpcionCompraListado.aspx/BuscarOpcionCompraTodo",
                    arrParametros,
                    function(jsondata) {
                        for (var i = 0; i < jsondata.Items.length; i++) {							
                            idsAllContratos.push(jsondata.Items[i].CodSolicitudCredito);
                        }
                    },
                    function(request) {
                        parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "BUSCAR OPCION COMPRA");
                        parent.fn_unBlockUI();
                        fn_doResize();
                    }
                   );
        }
    }
}

//****************************************************************
// Funcion		:: 	fn_Limpiar
// Descripción	::	
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_limpiar() {
    blnPrimeraBusqueda = false;
    $("#txtContrato").val('');
    $("#txtCUCliente").val('');
    $("#txtRazonSocial").val('');
    $("#ddlClasificacionbien").val(0);
    $('#ddlTipobien')
        .find('option')
        .remove()
        .end()
        .append('<option value="0">[-Seleccione-]</option>')
        .val('0')
    ;
    //$("#ddlTipobien").val(0);
    $("#ddlCartaEnviar").val(0);
    $("#rdSi").attr("checked", false);
    $("#rdNo").attr("checked", false);
    $("#txtPlacaActual").val('');
    $("#txtNroSerie").val('');
    $("#jqGrid_lista_A").GridUnload();
    fn_cargaGrilla();

    objFiltros.NumeroContrato = '';
    objFiltros.CUCliente = '';
    objFiltros.RazonSocial = '';
    objFiltros.Clasificacionbien = '';
    objFiltros.Tipobien = '';
    objFiltros.strTipoEnvio = '';
    objFiltros.Demanda = '';
    objFiltros.PlacaActual = '';
    objFiltros.NroSerie = '';
    objFiltros.FechaFiltro = '';

    $("#txtFechaFiltro").datepicker("destroy");
    $('#txtFechaFiltro').removeClass();
    $('#txtFechaFiltro').addClass('css_input_inactivo');
    $('#txtFechaFiltro').attr('readonly', 'readonly');
    $('#txtFechaFiltro').val('');

}

//****************************************************************
// Funcion		:: 	fn_InicializarCampos
// Descripción	::	
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_InicializarCampos() {
    blnPrimeraBusqueda = false;
    $('#txtContrato').validText({ type: 'number', length: 10 });
    $('#txtCUCliente').validText({ type: 'number', length: 10 });
    $('#txtRazonSocial').validText({ type: 'comment', length: 50 });
    $('#txtPlacaActual').validText({ type: 'alphanumeric', length: 10 });
    $('#txtNroSerie').validText({ type: 'alphanumeric', length: 20 });
}

//****************************************************************
// Funcion		:: 	fn_ShowMensaje
// Descripción	::	
// Log			:: 	WCR - 09/01/2013
//****************************************************************
function fn_ShowMensaje(pMensaje) {
    fn_buscarContrato();
    $("#hidCodigoSolicitudCredito").val('');
    $("#hidCodOpcionCompra").val('0');
    fn_util_MuestraLogPage(pMensaje, "I");
}

//****************************************************************
// Funcion		:: 	fn_FormatoFecha
// Descripción	::	
// Log			:: 	WCR - 10/01/2013
//****************************************************************
function fn_FormatoFecha(pFecha) {
    var strFecha = '1900-01-01';
    if (fn_util_trim(pFecha) != '') {
        var arrFecha = pFecha.split('/');
        strFecha = arrFecha[2] + '-' + arrFecha[1] + '-' + arrFecha[0];
    }
    return strFecha;
}

//****************************************************************
// Funcion		:: 	fn_fn_enviarCarta
// Descripción	::	
// Log			:: 	SCA - 16/01/2013
//****************************************************************
//debugger;
function fn_fn_enviarCarta() {
	var strTipo = $('#ddlCartaEnviar').val();
    var arrTipoEnvio = strTipo.split('|');
	
	var vEC = jQuery("#jqGrid_lista_A").getGridParam('selarrrow');
	var count = vEC.length;
	
	    for (var j = 0; j < count; j++) {
	    	var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', vEC[j]);
	    	
	    	fn_enviarWord(
	    		arrTipoEnvio[0],
	    		rowData.CodSolicitudCredito,
	    		rowData.ClienteRazonSocial,
	    		rowData.ClienteDomicilioLegal,
	    		rowData.Ubigeo);
	    }
	   //parent.fn_mdl_mensajeOk("La Carta se envio correctamente.", function() {  }, "ENVIAR CARTA CORRECTO");                        
	   //parent.fn_mdl_mensajeIco(strSelected, "util/images/warning.gif", "ENVIAR ");	fn_RedireccionGrabar();
}

function fn_enviarWord(pstrTipo,pstrCodSolicitudCredito,pstrRazonSocial,pstrDireccion,pstrUbigeo){
	
	
		var arrParametros = [     
			                "pstrTipo",pstrTipo,
	                        "pstrCodSolicitudCredito",pstrCodSolicitudCredito,
	                        "pstrRazonSocial",pstrRazonSocial,
	                        "pstrDireccion",pstrDireccion,
                            "pstrUbigeo",pstrUbigeo
							];
                
        fn_util_AjaxWM("frmOpcionCompraListado.aspx/EnviarCarta",
                 arrParametros,
                 function(result) { },
                 function(resultado) {}
        );

}
