//****************************************************************
// Variables Globales
//****************************************************************
var strConcepto = new Object();
strConcepto.ImpuestoMunicipal = "O01";
strConcepto.ImpuestoVehicular = "O02";
strConcepto.InfraccionTransito = "O17";
strConcepto.MultasInscripcion = "O31";

var rowDataOld;
var bFirstClick;
var lastRowEdit;
var intActGrid = 0;
var C_GESTIONBIEN_OTROSCONCEPTOS = "007";
var C_TX_NUEVO = "NUEVO";
var C_TX_EDITAR = "EDITAR";
var strValidFecha = '';
var intValidFecha = 0;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	WCR - 26/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();

    $(document).keypress(function(event) {
        if (event.which || event.keyCode) {
            if ((event.which == 13) || (event.keyCode == 13)) {
                fn_buscar(true);
            }
        }
    });

    fn_cargaGrilla();

    if ($('#hidOpcion').val() == C_TX_EDITAR) { bFirstClick = true; fn_buscarListarEditar(true); }
    else {
        $('#dv_Franccionar').hide();
        $('#dv_eliminar').hide();
        $('#dv_editar').hide();
    }

    //On load Page (siempre al final)
    fn_onLoadPage();
    $('#dv_Franccionar').hide();
});

//****************************************************************
// Funcion		:: 	fn_CheckFirstVisit
// Descripción	::	
// Log			:: 	WCR - 10/12/2012
//****************************************************************
function fn_CheckFirstVisit() {
    if (document.cookie.indexOf('coCobro') == -1) {
        document.cookie = 'coCobro=1';
    	$('#hidRefresh').val('0');
    }
    else { $('#hidRefresh').val('1'); }
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	WCR - 27/11/2012
//****************************************************************
function fn_inicializaCampos() {

}


//****************************************************************
// Funcion		:: 	fn_cargaGrillaConceptos
// Descripción	::	Inicializa Grilla Conceptos
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_cargaGrilla() {

    try {

        $("#jqGrid_lista_A").jqGrid({
            datatype: function() {
                if ($('#hidOpcion').val() == C_TX_EDITAR) {
                    fn_buscarListarEditar(false);
                } else {
                    fn_buscarListar();
                }

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
            colNames: ['Item', 'Concepto', 'N° Contrato', 'CU Cliente', 'Razón Social o Nombre', 'Moneda', 'Importe', 'Comisión', 'IGV', 'Total', 'Estado Contrato', 'Fecha Pago', 'Estado Pago', 'Fecha Cobro', 'Estado Cobro', 'CantidadFraccion', 'Obs.', '', '', '', '', '', '', '', '', '', '', '', 'Docs.','','','','','', '','','',''],
            colModel: [
            //                    { name: '', index: '', width: 15, align: "center", formatter: fn_Check },
                    {name: 'Id', index: 'Id', width: 15, align: "center" },
					{ name: 'Concepto', index: 'Concepto', width: 75, align: "left" },
					{ name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 35, align: "center" },
					{ name: 'CodUnico', index: 'CodUnico', width: 35, align: "left" },
            //					{ name: 'TipoDocumento', index: 'TipoDocumento', width: 55, align: "left" },
            //					{ name: 'NumeroDocumento', index: 'NumeroDocumento', width: 55, align: "center" },
					{name: 'ClienteRazonSocial', index: 'ClienteRazonSocial', width: 75, align: "left" },
					{ name: 'NombreMoneda', index: 'NombreMoneda', width: 45, align: "center" },
					{ name: 'MontoReembolso', index: 'MontoReembolso', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
					{ name: 'MontoComision', index: 'MontoComision', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
					{ name: 'MontoIGV', index: 'MontoIGV', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
					{ name: 'Total', index: 'Total', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
					{ name: 'EstadoContrato', index: 'EstadoContrato', width: 30, align: "center" },
		            { name: 'FecPago', index: 'FecPago', hidden: true, sortable: true, width: 30, align: "center" },
		            { name: 'DesEstadoPago', index: 'DesEstadoPago', hidden: true, sortable: true, sorttype: "string", width: 50, align: "center" },
		            { name: 'FechaRecuperacion', index: 'FechaRecuperacion', sortable: true, width: 30, align: "center" },
		            { name: 'EstadoCobro', index: 'EstadoCobro', sortable: true, sorttype: "string", width: 30, align: "center" },
		            { name: 'CantidadFraccion', index: 'CantidadFraccion', hidden: true },
					{ name: 'Observacion', index: 'Observacion', width: 20, align: "center", sortable: false, formatter: fn_Observacion },
		            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
		            { name: 'TipoRubroFinanciamiento', index: 'TipoRubroFinanciamiento', hidden: true },
		            { name: 'CodIfi', index: 'CodIfi', hidden: true },
		            { name: 'TipoRecuperacion', index: 'TipoRecuperacion', hidden: true },
		            { name: 'NumSecRecuperacion', index: 'NumSecRecuperacion', hidden: true },
		            { name: 'NumSecRecupComi', index: 'NumSecRecupComi', hidden: true },
		            { name: 'CodComisionTipo', index: 'CodComisionTipo', hidden: true },
		            { name: 'EstadoRecuperacion', index: 'EstadoRecuperacion', hidden: true },
		            { name: 'FlagIndividual', index: 'FlagIndividual', hidden: true },
		            { name: 'Observaciones', index: 'Observaciones', hidden: true },
		            { name: 'CantidadFraccionPendiente', index: 'CantidadFraccionPendiente', hidden: true },
					{ name: 'Doc', index: 'Doc', align: "center", sortable: false, formatter: fn_abreDocumentos, width: 20 },
            	    { name: 'ClienteDomicilioLegal', index: 'ClienteDomicilioLegal',  hidden: true },
            	    { name: 'SimMoneda', index: 'SimMoneda',  hidden: true },
            	    { name: 'Departamento', index: 'Departamento',  hidden: true },
            	    { name: 'Provincia', index: 'Provincia',  hidden: true },
            	    { name: 'Distrito', index: 'Distrito',  hidden: true },
            	    { name: 'TipoCambio', index: 'TipoCambio', formatter: Fn_util_ReturnValidDecimalx , hidden: true },
            	    { name: 'NroLote', index: 'NroLote',  hidden: true },
            	    { name: 'CorreoCliente', index: 'CorreoCliente',  hidden: true },
					{ name: '', index: '', width: 3 }

			],
            width: glb_intWidthPantalla - 80,
            height: '100%',
            pager: '#jqGrid_pager_A',
            loadtext: 'Cargando datos...',
            emptyrecords: 'No hay resultados',
            rowNum: 10,
            rowList: [10, 20, 30],
            sortname: 'FechaRegistro',
            sortorder: 'desc',
            viewrecords: true,
            //            gridview: true,
            //autowidth: true,
            altRows: true,
            loadonce: false,
            multiselect: true,
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
                $("#hidFlagIndividual").val(rowData.FlagIndividual);
                $("#hidItem").val(rowData.Id);


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
                $("#hidFlagIndividual").val(rowData.FlagIndividual);
                $("#hidItem").val(rowData.Id);

                fn_EditarConcepto('0');
            },
            afterInsertRow: function(id) {
                var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);

                if (parseInt(rowData.CantidadFraccion) == 0) {
                    $("#" + id + " td.sgcollapsed", $("#jqGrid_lista_A")[0]).unbind('click').html('').removeProp('class');
                }
            },
            subGrid: true,
            subGridOptions: {
                "openicon": "ui-icon-arrowreturn-1-e"
            },
            subGridRowExpanded: function(subgrid_id, row_id) {
                var rowDataPadre = $("#jqGrid_lista_A").jqGrid('getRowData', row_id);
                $("#hidCodSolicitudCredito").val(rowDataPadre.CodSolicitudCredito);
                $("#hidTipoRubroFinanciamiento").val(rowDataPadre.TipoRubroFinanciamiento);
                $("#hidCodIfi").val(rowDataPadre.CodIfi);
                $("#hidTipoRecuperacion").val(rowDataPadre.TipoRecuperacion);
                $("#hidNumSecRecuperacion").val(rowDataPadre.NumSecRecuperacion);
                $("#hidNumSecRecupComi").val(rowDataPadre.NumSecRecupComi);
                $("#hidCodComisionTipo").val(rowDataPadre.CodComisionTipo);
                $("#hidEstadoRecuperacion").val(rowDataPadre.EstadoRecuperacion);
                $("#hidFlagIndividual").val(rowDataPadre.FlagIndividual);
                $("#hidItem").val(rowDataPadre.Id);

                var subgrid_table_id, pager_id;
                subgrid_table_id = subgrid_id + "_t";
                pager_id = "p_" + subgrid_table_id;

                //var strTitImporte = pSubgrid_table_id + 'Importe';

                $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pager_id + "' class='scroll'></div>");
                jQuery("#" + subgrid_table_id).jqGrid({
                    datatype: function() {
                        fn_CargaAgrupacion(subgrid_table_id);
                    },
                    //datatype: "local",
                    jsonReader:
					{
					    root: "Items",
					    page: "CurrentPage",
					    total: "PageCount",
					    records: "RecordCount",
					    repeatitems: false,
					    id: "IdF"
					},
                    colNames: ['', 'N° Cuota', 'Fecha Cobro', 'Importe', 'Comisión', 'IGV', 'Interés', 'Total a Pagar', '', 'Estado Cobro', '', '', '', '', '', '', '', '', '', '', '', '', '', '','','','','','','',''],
                    colModel: [
				            { name: 'IdF', index: 'IdF', hidden: true, editable: true },
				            { name: 'NroCuota', index: 'NroCuota', sortable: false, width: 30, align: "right" },
				            { name: 'FechaCobro', index: 'FechaCobro', sortable: false, width: 40, sorttype: "date", align: "center", editable: true },
				            { name: 'Importe', index: 'Importe', sortable: false, width: 40, align: "right", editable: true, formatter: Fn_util_ReturnValidDecimalx },
				            { name: 'Comision', index: 'Comision', sortable: false, width: 40, align: "right", editable: true, formatter: Fn_util_ReturnValidDecimalx },
				            { name: 'IGVComision', index: 'IGVComision', sortable: false, width: 30, align: "right", formatter: Fn_util_ReturnValidDecimalx },
				            { name: 'Interes', index: 'Interes', sortable: false, width: 30, align: "right", formatter: Fn_util_ReturnValidDecimalx },
				            { name: 'TotalPagar', index: 'TotalPagar', sortable: false, width: 40, align: "right", formatter: Fn_util_ReturnValidDecimalx },
				            { name: 'EstadoCobro', index: 'EstadoCobro', hidden: true },
				            { name: 'DesEstadoCobro', index: 'DesEstadoCobro', sortable: false, width: 45, align: "center" },
				            { name: '', index: '', width: 1 },
				            { name: 'Dias', index: 'Dias', hidden: true },
				            { name: 'FechaVencimiento', index: 'FechaVencimiento', hidden: true },
				            { name: 'FechaRecuperacion', index: 'FechaRecuperacion', hidden: true },
				            { name: 'MontoReembolso', index: 'MontoReembolso', hidden: true },
				            { name: 'PorcentajeTEA', index: 'PorcentajeTEA', hidden: true },
				            { name: 'MaximaCuota', index: 'MaximaCuota', hidden: true },
				            { name: 'CodOperacionActiva', index: 'CodOperacionActiva', hidden: true },
				            { name: 'TipoRubroFinanciamiento', index: 'TipoRubroFinanciamiento', hidden: true },
				            { name: 'CodIfi', index: 'CodIfi', hidden: true },
				            { name: 'TipoRecuperacion', index: 'TipoRecuperacion', hidden: true },
				            { name: 'NumSecRecuperacion', index: 'NumSecRecuperacion', hidden: true },
				            { name: 'CodComisionTipo', index: 'CodComisionTipo', hidden: true },
				            { name: 'NumSecRecupComi', index: 'NumSecRecupComi', hidden: true },
            	            { name: 'ClienteDomicilioLegal', index: 'ClienteDomicilioLegal',  hidden: true },
            	            { name: 'SimMoneda', index: 'SimMoneda',  hidden: true },
            	            { name: 'Departamento', index: 'Departamento',  hidden: true },
            	            { name: 'Provincia', index: 'Provincia',  hidden: true },
            	            { name: 'Distrito', index: 'Distrito',  hidden: true },
            	            { name: 'NroLote', index: 'NroLote',  hidden: true },
            	            { name: 'CorreoCliente', index: 'CorreoCliente',  hidden: true },
            	            { name: 'TipoCambio', index: 'TipoCambio', formatter: Fn_util_ReturnValidDecimalx , hidden: true }
					],
                    width: glb_intWidthPantalla - 435,
                    height: '100%',
                    editurl: 'clientArray',
                    loadtext: 'Cargando datos...',
                    emptyrecords: 'No hay resultados',
                    rowNum: 999,
                    rowList: [10, 20, 30],
                    sortname: 'Id',
                    sortorder: 'desc',
                    viewrecords: true,
                    gridview: true,
                    //autowidth: true,
                    altRows: true,
                    altclass: 'gridAltClass',
                    multiselect: false,
                    gridComplete: function(id) {
                        fn_doResize();
                    },
                    ondblClickRow: function(rowid) {
                        //var row_id = $("#grid").getGridParam('selrow');
                        var rowData = $("#" + subgrid_table_id).jqGrid('getRowData', rowid);
                        if (fn_util_trim(rowData.EstadoCobro) == 'C') {
                            lastRowEdit = rowid;
                            intActGrid = 1;
                            var cm = jQuery('#' + subgrid_table_id).jqGrid('getColProp', 'Interes');
                            jQuery('#' + subgrid_table_id).editRow(rowid, true);
                            cm.editable = true;
                            //                            fn_util_SeteaCalendario(jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A"));
                            //                            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").blur(function() {
                            //                                fn_ActualizarFecha(this, rowid, subgrid_table_id);
                            //                            });
                            //                                jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").keydown(function(event) {
                            //                                    if (event.which || event.keyCode) {
                            //                                        if ((event.which == 13) || (event.keyCode == 13)) {
                            //                                            fn_ActualizarFecha(this,rowid);
                            //                                            return false;
                            //                                        }
                            //                                    }
                            //                                    else {
                            //                                        return true
                            //                                    }
                            //                                });
                            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").datepicker({ selectOtherMonths: true, changeYear: true, changeMonth: true,
                                onSelect: function(dateText, inst) { fn_ActualizarFecha(this, rowid, subgrid_table_id); }
                            });

                            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").trigger('onSelect');
                            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").addClass('css_calendario');
                            //fn_util_SeteaCalendario(jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A"));
                            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").blur(function() {
                                fn_ActualizarFecha(this, rowid, subgrid_table_id);
                            });
                            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").keydown(function(event) {
                                if (event.which || event.keyCode) {
                                    if ((event.which == 13) || (event.keyCode == 13)) {
                                        fn_ActualizarFecha(this, rowid, subgrid_table_id);
                                        return false;
                                    }
                                }
                                else {
                                    return true
                                }
                            });
                            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").keyup(function() {
                                $(this).val(fn_util_ValidaFecha(this));
                            });

                            jQuery("#" + rowid + "_Importe", "#jqGrid_lista_A").validNumber({ value: '', decimals: 2, length: 15 });
                            jQuery("#" + rowid + "_Importe", "#jqGrid_lista_A").blur(function() {
                                fn_ActualizarImporte(this, rowid, subgrid_table_id);
                            });
                            jQuery("#" + rowid + "_Importe", "#jqGrid_lista_A").keydown(function(event) {
                                if (event.which || event.keyCode) {
                                    if ((event.which == 13) || (event.keyCode == 13)) {
                                        fn_ActualizarImporte(this, rowid, subgrid_table_id);
                                        return false;
                                    }
                                }
                                else {
                                	return true;
                                }
                            });

                            jQuery("#" + rowid + "_Comision", "#jqGrid_lista_A").validNumber({ value: '', decimals: 2, length: 15 });
                            jQuery("#" + rowid + "_Comision", "#jqGrid_lista_A").blur(function() {
                                fn_ActualizarComision(this, rowid, subgrid_table_id);
                            });
                            jQuery("#" + rowid + "_Comision", "#jqGrid_lista_A").keydown(function(event) {
                                if (event.which || event.keyCode) {
                                    if ((event.which == 13) || (event.keyCode == 13)) {
                                        fn_ActualizarComision(this, rowid, subgrid_table_id);
                                        return false;
                                    }
                                }
                                else {
                                	return true;
                                }
                            });
                        }
                        //jQuery('#' + subgrid_table_id).jqGrid('editGridRow', rowid);
                    },
                    onSelectRow: function(rowid) {

                        // jQuery('#' + subgrid_table_id).editRow(rowid, false);
                        var ids = jQuery("#" + subgrid_table_id).jqGrid('getDataIDs');
                        for (var i = 0; i < ids.length; i++) {
                            //jQuery('#' + subgrid_table_id).editRow(ids[i], false, null, null, 'clientArray');
                            //if (parseFloat(ids[i]) != parseFloat(rowid)) {
                            jQuery('#' + subgrid_table_id).jqGrid('saveRow', ids[i]);
                            var cm = jQuery('#' + subgrid_table_id).jqGrid('getColProp', 'Interes');
                            cm.editable = false;
                            //}
                        }
                        if (intActGrid == 1) {
                            fn_guardarFraccionamiento(lastRowEdit, subgrid_table_id);
                        }
                        intActGrid = 0;
                        //                        lastRowEdit = null;
                        //lastRowEdit
                        //                        var rowData = jQuery('#' + subgrid_table_id).jqGrid('getRowData', id);
                        //                        if (intActGrid == 1) {
                        //                            jQuery(list).setColProp('FechaCobro', { editable: false });
                        //                            jQuery(list).setColProp('Importe', { editable: false });
                        //                            jQuery(list).setColProp('Comision', { editable: false });
                        //                            parent.fn_mdl_mensajeIco("- Termine editar el fraccionamiento", "util/images/warning.gif", "ELIMINAR COBRO");
                        //                        }
                        //                        else {
                        //                            jQuery('#' + subgrid_table_id).setColProp('FechaCobro', { editable: true });
                        //                            jQuery('#' + subgrid_table_id).setColProp('Importe', { editable: true });
                        //                            jQuery('#' + subgrid_table_id).setColProp('Comision', { editable: true });
                        //                        }
                    }

                });
            }


        });

        jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_lista_A', { edit: false, add: false, del: false });
        $("#search_jqGrid_lista_A").hide();


    } catch (e) {
        alert(e.message);
    }

    function fn_Observacion(cellvalue, options, rowObject) {
        var param = "'" + rowObject.CodSolicitudCredito + "','" + rowObject.TipoRubroFinanciamiento + "','" + rowObject.CodIfi + "','" + rowObject.TipoRecuperacion + "'," + rowObject.NumSecRecuperacion + "," + rowObject.NumSecRecupComi + ",'" + rowObject.CodComisionTipo + "'";
        var obj = '';
        if (fn_util_trim(rowObject.Observaciones) == '') {
            obj = "<img src='../../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observación' width='20px' onclick=\"javascript:fn_AgregarObservacion(" + param + ");\" style='cursor: pointer;cursor: hand;' />";
        }
        else {
            obj = "<img src='../../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observación' width='20px' onclick=\"javascript:fn_AgregarObservacion(" + param + ");\" style='cursor: pointer;cursor: hand;' />";
        }
        return obj;
    };

    //**************************************************
    // Documentos
    //**************************************************
    function fn_abreDocumentos(cellvalue, options, rowObject) {
        //return ".";
        return "<img src='../../Util/images/ico_docs.gif' alt='Ver Documentos' title='Ver Documentos' width='18px' onclick=\"javascript:fn_GBAbreDocumentos(\'" + rowObject.CodSolicitudCredito + "\',\'0\',\'" + rowObject.NumSecRecuperacion + "\',\'" + C_GESTIONBIEN_OTROSCONCEPTOS + "\',\'" + rowObject.EstadoRecuperacion + "\');\" style='cursor:pointer;' />";
    };

}

function fn_Check(cellvalue, options, rowObject) {

    //      var arrmarcados = $("#hddPagChecked").val();

    //    if (rowObject.FlagEnvioCarta=="True") {
    //        $("#hddTotalDocumentoProveedor").val(rowObject.TotalImporte);
    //    }

    //        if (arrmarcados != "") {
    //            var sResultadoarrmarcados = arrmarcados.split("|");
    //            for (var i = 0; i < sResultadoarrmarcados.length; i++) {
    //                if (rowObject.CodigoContratoProveedor == sResultadoarrmarcados[i]) {
    //                	$("#hddTotalDocumentoProveedor").val(fn_util_ValidaDecimal($("#hddTotalDocumentoProveedor").val()) + fn_util_ValidaDecimal(rowObject.TotalImporte));
    //                	return "<input id='chkenvioCarta' name='chkenvioCarta' type='checkbox' checked runat='server' onclick='javascript:fn_seleccionaRegistro(this, " + rowObject.CodigoContratoProveedor + ")'/>";
    //                }
    //            }

    //        }
    //    
    return "<input id='chkenvio' name='chkenvio' type='checkbox' runat='server' />";
    //    }   

}


//****************************************************************
// Funcion		:: 	fn_EliminarConcepto
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_EliminarConcepto() {

    var vElementosAEditar = $("#jqGrid_lista_A").getGridParam('selarrrow');
    var strItem = $("#hidItem").val();
    if (vElementosAEditar.length == 0) { parent.fn_mdl_mensajeIco("Seleccione un registro para poder eliminar.", "util/images/warning.gif", "ELIMINAR COBRO"); }
    else {
        if (vElementosAEditar.length == 1) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', vElementosAEditar);
            var strCodSolicitudCredito = rowData.CodSolicitudCredito;
            var strEstadoRecuperacion = rowData.EstadoRecuperacion;
            var strFlagIndividual = rowData.FlagIndividual;
            var strConcepto = rowData.Concepto;
            if (strFlagIndividual == '1') {
                if (strEstadoRecuperacion == 'C') {
                    fn_mdl_confirma("Est&aacute; seguro de eliminar el cobro?"
    		          , function() {
    		              $("#hidCodSolicitudCredito").val(rowData.CodSolicitudCredito);
    		              $("#hidTipoRubroFinanciamiento").val(rowData.TipoRubroFinanciamiento);
    		              $("#hidCodIfi").val(rowData.CodIfi);
    		              $("#hidTipoRecuperacion").val(rowData.TipoRecuperacion);
    		              $("#hidNumSecRecuperacion").val(rowData.NumSecRecuperacion);
    		              $("#hidNumSecRecupComi").val(rowData.NumSecRecupComi);
    		              $("#hidCodComisionTipo").val(rowData.CodComisionTipo);
    		              $("#hidEstadoRecuperacion").val(rowData.EstadoRecuperacion);
    		              $("#hidFlagIndividual").val(rowData.FlagIndividual);
    		              $("#hidItem").val(rowData.Id);
    		              fn_EliminarRegistro();
    		          },
		                '../../util/images/question.gif',
                      function() { },
                      'Eliminar Cobro'
	                  );
                }
                else { parent.fn_mdl_mensajeIco("Solo se puede eliminar cobro en estado pendiente", "util/images/warning.gif", "ELIMINAR COBRO"); }
            }
            else { parent.fn_mdl_mensajeIco("No se puede eliminar el cobro " + strConcepto.toLowerCase(), "util/images/warning.gif", "ELIMINAR COBRO"); }
        }
        else { parent.fn_mdl_mensajeIco("Solo se puede eliminar un cobro a la vez.", "util/images/warning.gif", "ELIMINAR COBRO"); }
    }
}

//****************************************************************
// Funcion		:: 	fn_EliminarRegistro
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_EliminarRegistro() {
    var strCodSolicitudCredito = $("#hidCodSolicitudCredito").val();
	
    //var strTipoRubroFinanciamiento = $("#hidTipoRubroFinanciamiento").val();
    //var strCodIfi = $("#hidCodIfi").val();
    //var strTipoRecuperacion = $("#hidTipoRecuperacion").val();
    //var strNumSecRecuperacion = $("#hidNumSecRecuperacion").val();
    //var strNumSecRecupComi = $("#hidNumSecRecupComi").val();
    //var strCodComisionTipo = $("#hidCodComisionTipo").val();
	
    var strEstadoRecuperacion = $("#hidEstadoRecuperacion").val();
    var strFlagIndividual = $("#hidFlagIndividual").val();

    var strNumeroContrato = $('#hidCodSolicitudCredito').val() == undefined ? "" : $('#hidCodSolicitudCredito').val();
    var strTipoRubroFinanciamiento = $('#hidTipoRubroFinanciamiento').val() == undefined ? "" : $('#hidTipoRubroFinanciamiento').val();
    var strCodIfi = $('#hidCodIfi').val() == undefined ? "" : $('#hidCodIfi').val();
    var strTipoRecuperacion = $('#hidTipoRecuperacion').val() == undefined ? "" : $('#hidTipoRecuperacion').val();
    var strNumSecRecuperacion = $('#hidNumSecRecuperacion').val() == undefined ? "" : $('#hidNumSecRecuperacion').val();
    var strNumSecRecupComi = $('#hidNumSecRecupComi').val() == undefined ? "" : $('#hidNumSecRecupComi').val();
    var strCodComisionTipo = $('#hidCodComisionTipo').val() == undefined ? "" : $('#hidCodComisionTipo').val();

    var arrParametros = ["pstrNumeroContrato", strNumeroContrato,
                         "pstrTipoRubroFinanciamiento", strTipoRubroFinanciamiento,
                         "pstrCodIfi", strCodIfi,
                         "pstrTipoRecuperacion", strTipoRecuperacion,
                         "pstrNumSecRecuperacion", strNumSecRecuperacion,
	 		             "pstrNumSecRecupComi", strNumSecRecupComi,
	 		             "pstrCodComisionTipo", strCodComisionTipo
                        ];

    fn_util_AjaxWM("frmCobroMasivoRegistro.aspx/EliminarCobro",
                    arrParametros,
                    function(result) {
                        parent.fn_unBlockUI();
                        if (fn_util_trim(result) == "0") {
                            parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ELIMINAR COBRO");
                        } else {
                            fn_util_MuestraLogPage("El cobro se eliminó correctamente.", "I");
                            $('#hidFlagRegistro').val('1');
                            fn_ActualizarListado();
                            $('#hidFlagRegistro').val('0');
                            fn_LimpiarHidden();
                        }
                    },
                     function(resultado) {
                         parent.fn_unBlockUI();
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ELIMINAR COBRO");
                     }

                );

}
//****************************************************************
// Funcion		:: 	fn_EditarConcepto
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_EditarConcepto(pIndicador) {
    var sTitulo = "Otros Conceptos";
    var sSubTitulo = "Otros Conceptos:: Editar Cobro";
    var intValid = 0;
    var intEntrar = 1;
    var vElementosAEditar = $("#jqGrid_lista_A").getGridParam('selarrrow');
    var intCantidad = 0;
    if (pIndicador == '0') {
        if (vElementosAEditar.length == 0) {
            intEntrar = 0;
            parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "EDITAR COBRO");
        } else {

            if (vElementosAEditar.length > 1) { intValid = 1; }
            else {
                var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', vElementosAEditar);
                $("#hidCodSolicitudCredito").val(rowData.CodSolicitudCredito);
                $("#hidTipoRubroFinanciamiento").val(rowData.TipoRubroFinanciamiento);
                $("#hidCodIfi").val(rowData.CodIfi);
                $("#hidTipoRecuperacion").val(rowData.TipoRecuperacion);
                $("#hidNumSecRecuperacion").val(rowData.NumSecRecuperacion);
                $("#hidNumSecRecupComi").val(rowData.NumSecRecupComi);
                $("#hidCodComisionTipo").val(rowData.CodComisionTipo);
                $("#hidEstadoRecuperacion").val(rowData.EstadoRecuperacion);
                $("#hidFlagIndividual").val(rowData.FlagIndividual);
                $("#hidItem").val(rowData.Id);
                intCantidad = parseInt(rowData.CantidadFraccion, 10);
            }

        }
    }
    if (intEntrar == 1) {
        if (intValid == 0) {

            var strCodSolicitudCredito = $("#hidCodSolicitudCredito").val();
            var strTipoRubroFinanciamiento = $("#hidTipoRubroFinanciamiento").val();
            var strCodIfi = $("#hidCodIfi").val();
            var strTipoRecuperacion = $("#hidTipoRecuperacion").val();
            var strNumSecRecuperacion = $("#hidNumSecRecuperacion").val();
            var strNumSecRecupComi = $("#hidNumSecRecupComi").val();
            var strCodComisionTipo = $("#hidCodComisionTipo").val();
            var strEstadoRecuperacion = $("#hidEstadoRecuperacion").val();
            var strFlagIndividual = $("#hidFlagIndividual").val();
            var strItem = $("#hidItem").val();

            var strNroLote = '';  //$('#txtNroLote').val() == undefined ? "" : $('#txtNroLote').val();
            var strRazonSocial = ''; // $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
            var strNroContrato = strCodSolicitudCredito;  //$('#txtNroContrato').val() == undefined ? "" : $('#txtNroContrato').val();
            var strCUCliente = '';  //$('#txtCUCliente').val() == undefined ? "" : $('#txtCUCliente').val();

            var strConcepto = '';  //$("#cmbConcepto option:selected").val() == "0" ? "" : $("#cmbConcepto option:selected").val();
            var strEstadoCobro = '';  //$("#cmbEstadoCobro option:selected").val() == "0" ? "" : $("#cmbEstadoCobro option:selected").val();
            var strInstancia = $('#hidInstancia').val() == undefined ? "" : $('#hidInstancia').val();
            if (strInstancia == '') { strInstancia = '0'; }

            //            if (intCantidad == 0) {
            //            if (strEstadoRecuperacion != 'H') {
            var strSortName = fn_util_getJQGridParam("jqGrid_lista_A", "sortname");
            var strSortOrder = fn_util_getJQGridParam("jqGrid_lista_A", "sortorder");

            var strParam = 'Titulo=' + sTitulo + '&SubTitulo=' + sSubTitulo + '&co=EDITAR&csc=' + strCodSolicitudCredito;
            strParam = strParam + '&trf=' + strTipoRubroFinanciamiento + '&ci=' + strCodIfi + '&tre=' + strTipoRecuperacion;
            strParam = strParam + '&nsr=' + strNumSecRecuperacion + '&nsrc=' + strNumSecRecupComi + '&cct=' + strCodComisionTipo;
            strParam = strParam + '&fic=' + strItem + '&fnl=' + strNroLote + '&frs=' + strRazonSocial + '&fnc=' + strNroContrato;
            strParam = strParam + '&fcu=' + strCUCliente + '&fco=' + strConcepto + '&fec=' + strEstadoCobro + '&fsn=' + strSortName;
            strParam = strParam + '&fso' + strSortOrder + '&eco=' + pIndicador + '&inr=' + strInstancia;

            parent.fn_util_AbreModal(sSubTitulo, 'GestionBien/OtrosConceptos/frmCobroRegistro.aspx?' + strParam, 800, 460, function() { fn_ActualizarListado(); });
            //            } else {
            //                parent.fn_mdl_mensajeIco("Solo se puede editar en estado pendiente.", "util/images/warning.gif", "EDITAR COBRO");
            //            }
            //            } else { parent.fn_mdl_mensajeIco("No se puede editar el cobro fraccionado.", "util/images/warning.gif", "EDITAR COBRO");  }
        } else {
            parent.fn_mdl_mensajeIco("Solo se puede editar un cobro a la vez.", "util/images/warning.gif", "ELIMINAR COBRO");
        }
    }
}

//****************************************************************
// Funcion		:: 	fn_AgregarConcepto
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_AgregarConcepto() {

    var strCodSolicitudCredito = '';
    if ($("#hidOpcion").val() != C_TX_NUEVO) { strCodSolicitudCredito = $("#hidCodSolicitudCredito").val(); }

    var sTitulo = "Otros Conceptos";
    var sSubTitulo = "Otros Conceptos:: Agregar Cobro";
    var strParam = 'Titulo=' + sTitulo + '&SubTitulo=' + sSubTitulo + '&co=NUEVO&csc=&trf=&ci=&tre=&nsr=&nsrc=&cct=&inr=' + $('#hidInstancia').val() + '&fnc=' + strCodSolicitudCredito;

    parent.fn_util_AbreModal(sSubTitulo, 'GestionBien/OtrosConceptos/frmCobroRegistro.aspx?' + strParam, 800, 430, function() { });
}

//****************************************************************
// Funcion		:: 	fn_FraccionarConcepto
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_FraccionarConcepto() {
    var vElementosAEditar = $("#jqGrid_lista_A").getGridParam('selarrrow');
    if (vElementosAEditar.length == 0) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder fraccionar.", "util/images/warning.gif", "FRACCIONAR COBRO");
    } else {

        if (vElementosAEditar.length == 1) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', vElementosAEditar);
            var strCodSolicitudCredito = rowData.CodSolicitudCredito;
            var strTipoRubroFinanciamiento = rowData.TipoRubroFinanciamiento;
            var strCodIfi = rowData.CodIfi;
            var strTipoRecuperacion = rowData.TipoRecuperacion;
            var strNumSecRecuperacion = rowData.NumSecRecuperacion;
            var strNumSecRecupComi = rowData.NumSecRecupComi;
            var strCodComisionTipo = rowData.CodComisionTipo;
            var strEstadoRecuperacion = rowData.EstadoRecuperacion;
            var intCantidad = parseInt(rowData.CantidadFraccion, 10);
            var intCantidadPendiente = parseInt(rowData.CantidadFraccionPendiente, 10);
            if ((intCantidad > 0) && (intCantidadPendiente != intCantidad)) { parent.fn_mdl_mensajeIco('Existe cuota(s) cobrada(s), no se puede editar el fraccionamiento.', "util/images/warning.gif", "FRACCIONAR COBRO"); }
            else {
                if (intCantidad == 0) {
                    if ((strCodComisionTipo == strConcepto.ImpuestoMunicipal) || (strCodComisionTipo == strConcepto.ImpuestoVehicular)) {
                        if (strEstadoRecuperacion == 'C') {
                            var sTitulo = "Otros Conceptos";
                            var sSubTitulo = "Otros Conceptos:: Fraccionamiento";

                            var strParam = 'Titulo=' + sTitulo + '&SubTitulo=' + sSubTitulo + '&co=NUEVO&csc=' + strCodSolicitudCredito;
                            strParam = strParam + '&trf=' + strTipoRubroFinanciamiento + '&ci=' + strCodIfi + '&tre=' + strTipoRecuperacion;
                            strParam = strParam + '&nsr=' + strNumSecRecuperacion + '&nsrc=' + strNumSecRecupComi + '&cct=' + strCodComisionTipo;
                            parent.fn_util_AbreModal(sSubTitulo, "GestionBien/OtrosConceptos/frmFraccionamientoRegistro.aspx?" + strParam, 800, 420, function() { });

                        }
                        else { parent.fn_mdl_mensajeIco('Solo se puede fraccionar en estado pendiente.', "util/images/warning.gif", "FRACCIONAR COBRO"); }
                    } else { parent.fn_mdl_mensajeIco('Este concepto no se puede fraccionar.', "util/images/warning.gif", "FRACCIONAR COBRO"); }
                } else {
                    //                    var sTitulo = "Otros Conceptos";
                    //                    var sSubTitulo = "Otros Conceptos:: Fraccionamiento";

                    //                    var strParam = 'Titulo=' + sTitulo + '&SubTitulo=' + sSubTitulo + '&co=EDITAR&csc=' + strCodSolicitudCredito;
                    //                    strParam = strParam + '&trf=' + strTipoRubroFinanciamiento + '&ci=' + strCodIfi + '&tre=' + strTipoRecuperacion;
                    //                    strParam = strParam + '&nsr=' + strNumSecRecuperacion + '&nsrc=' + strNumSecRecupComi + '&cct=' + strCodComisionTipo + '&cfp=' + intCantidadPendiente.toString();
                    //                    parent.fn_util_AbreModal(sSubTitulo, "GestionBien/OtrosConceptos/frmFraccionamientoRegistro.aspx?" + strParam, 800, 420, function() { });

                    parent.fn_mdl_mensajeIco('Este concepto ya esta fraccionado.', "util/images/warning.gif", "FRACCIONAR COBRO");
                }
            }
        } else { parent.fn_mdl_mensajeIco('Solo se puede fraccionar un cobro a la vez.', "util/images/warning.gif", "FRACCIONAR COBRO"); }
    }
}

//****************************************************************
// Funcion		:: 	fn_AgregarObservacion
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_AgregarObservacion(pCodSolicitudCredito, pTipoRubroFinanciamiento, pCodIfi, pTipoRecuperacion, pNumSecRecuperacion, pNumSecRecupComi, pCodComisionTipo) {
    var sTitulo = "Otros Conceptos";
    var sSubTitulo = "Cobros:: Observaciones";
    var strParam = 'Titulo=' + sTitulo + '&SubTitulo=' + sSubTitulo + '&csc=' + pCodSolicitudCredito + '&trf=' + pTipoRubroFinanciamiento + '&ci=' + pCodIfi;
    strParam = strParam + '&tre=' + pTipoRecuperacion + '&nsr=' + pNumSecRecuperacion + '&nsrc=' + pNumSecRecupComi + '&cct=' + pCodComisionTipo;

    parent.fn_util_AbreModal(sSubTitulo, "GestionBien/OtrosConceptos/frmObservacionCobro.aspx?" + strParam, 500, 220, function() { fn_ActualizarListado(); });

}

//****************************************************************
// Funcion		:: 	fn_cancelar
// Descripción	::	cancelar
// Log			:: 	WCR - 21/11/2012
//****************************************************************
function fn_cancelar() {
    parent.fn_mdl_confirma('¿Está seguro de Volver?',
		function() {
		    parent.fn_blockUI();
		    fn_util_redirect('frmCobroListado.aspx');
		    //fn_util_redirect('GestionBien/OtrosConceptos/frmCobroListado.aspx');
		},
         "util/images/question.gif",

		function() {
		},
		'Otros Conceptos :: Cobros'
	);
}

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	Ejecuta la búsqueda de los documentos, cuando el usuario hace click en
//                  el botón 'Buscar'.
// Log			:: 	WCR - 15/02/2012
//****************************************************************
function fn_buscar(bSearch) {
    bFirstClick = bSearch;
    fn_LimpiarHidden();
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

    parent.fn_blockUI();

    var strNroLote = '';  //$('#txtNroLote').val() == undefined ? "" : $('#txtNroLote').val();
    var strRazonSocial = '';  // $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
    var strNroContrato = '';  //$('#txtNroContrato').val() == undefined ? "" : $('#txtNroContrato').val();
    var strCUCliente = '';  //$('#txtCUCliente').val() == undefined ? "" : $('#txtCUCliente').val();
    var strFlagIndividual = $('#hidFlagIndividual').val() == undefined ? "" : $('#hidFlagIndividual').val();
    var strFlagRegistro = '';  //$('#hidFlagRegistro').val() == undefined ? "" : $('#hidFlagRegistro').val();
    var strInstancia = $('#hidInstancia').val() == undefined ? "" : $('#hidInstancia').val();
    if (strInstancia == '') { strInstancia = '0'; }
	var strConcepto = ''; //$("#cmbConcepto option:selected").val() == "0" ? "" : $("#cmbConcepto option:selected").val();
	var strEstadoCobro = ''; //$("#cmbEstadoCobro option:selected").val() == "0" ? "" : $("#cmbEstadoCobro option:selected").val();

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
                             "pstrFlagRegistro", strFlagRegistro,
                             "pstrInstancia", strInstancia
                            ];

    fn_util_AjaxWM("frmCobroMasivoRegistro.aspx/BuscarCobros",
                       arrParametros,
                       function(jsondata) {
                           jqGrid_lista_A.addJSONData(jsondata);
                           parent.fn_unBlockUI();
                           if (parseInt($("#jqGrid_lista_A").getGridParam("records"), 10) > 0) {
                               $('#dv_eliminar').show();
                               $('#dv_editar').show();
                           }
                           else {
                               $('#dv_eliminar').hide();
                               $('#dv_editar').hide();
                           }
                           fn_doResize();
                       },
                       function(request) {
                           parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR EN LA BÚSQUEDA");
                           parent.fn_unBlockUI();
                           fn_doResize();
                       });

}

//****************************************************************
// Funcion		:: 	fn_buscarListar
// Descripción	::	
// Log			:: 	WCR - 27/11/2012
//****************************************************************
function fn_buscarListarEditar(pEntrar) {

    if (!bFirstClick) {
        return;
    }
    parent.fn_blockUI();

    var strNumSecRecuperacion = $('#hidNumSecRecuperacion').val() == undefined ? "" : $('#hidNumSecRecuperacion').val();
    var strNumSecRecupComi = $('#hidNumSecRecupComi').val() == undefined ? "" : $('#hidNumSecRecupComi').val();
    var strNroContrato = $('#hidCodSolicitudCredito').val() == undefined ? "" : $('#hidCodSolicitudCredito').val();
	var strConcepto = '';//$('#hidCodComisionTipo').val() == undefined ? "" : $('#hidCodComisionTipo').val();


	var strConcepto = ''; //$("#cmbConcepto option:selected").val() == "0" ? "" : $("#cmbConcepto option:selected").val();
	var strEstadoCobro = ''; // $("#cmbEstadoCobro option:selected").val() == "0" ? "" : $("#cmbEstadoCobro option:selected").val();

    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
                             "pstrNroContrato", strNroContrato,
                             "pstrCodigoConcepto", strConcepto,
                             "pstrNumSecRecuperacion", strNumSecRecuperacion,
                             "pstrNumSecRecupComi", strNumSecRecupComi
                            ];

    fn_util_AjaxWM("frmCobroMasivoRegistro.aspx/BuscarCobroEditar",
                       arrParametros,
                       function(jsondata) {
                           jqGrid_lista_A.addJSONData(jsondata);
                           parent.fn_unBlockUI();
                           if (pEntrar) {
                               $("#hidFlagIndividual").val('0');
                               $("#hidItem").val('1');
                               if ($('#hidRefresh').val() == '0') {
                                   fn_EditarConcepto('1');
                               }

                           }
                           if (parseInt($("#jqGrid_lista_A").getGridParam("records"), 10) > 0) {
                               $('#dv_eliminar').show();
                               $('#dv_editar').show();
                           }
                           else {
                               $('#dv_eliminar').hide();
                               $('#dv_editar').hide();
                           }
                           fn_doResize();
                       },
                       function(request) {
                           parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "BUSCAR COBRO");
                           parent.fn_unBlockUI();
                           fn_doResize();
                       });
}


//****************************************************************
// Funcion		:: 	fn_CargaAgrupacion  
// Descripción	::	Carga Lista 
// Log			:: 	WCR - 13/12/2012
//****************************************************************
function fn_CargaAgrupacion(pSubgrid_table_id) {

    try {
        var strNumeroContrato = $('#hidCodSolicitudCredito').val() == undefined ? "" : $('#hidCodSolicitudCredito').val();
        var strTipoRubroFinanciamiento = $('#hidTipoRubroFinanciamiento').val() == undefined ? "" : $('#hidTipoRubroFinanciamiento').val();
        var strCodIfi = $('#hidCodIfi').val() == undefined ? "" : $('#hidCodIfi').val();
        var strTipoRecuperacion = $('#hidTipoRecuperacion').val() == undefined ? "" : $('#hidTipoRecuperacion').val();
        var strNumSecRecuperacion = $('#hidNumSecRecuperacion').val() == undefined ? "" : $('#hidNumSecRecuperacion').val();
        var strNumSecRecupComi = $('#hidNumSecRecupComi').val() == undefined ? "" : $('#hidNumSecRecupComi').val();
        var strCodComisionTipo = $('#hidCodComisionTipo').val() == undefined ? "" : $('#hidCodComisionTipo').val();

        var arrParametros = ["pstrNumeroContrato", strNumeroContrato,
                         "pstrTipoRubroFinanciamiento", strTipoRubroFinanciamiento,
                         "pstrCodIfi", strCodIfi,
                         "pstrTipoRecuperacion", strTipoRecuperacion,
                         "pstrNumSecRecuperacion", strNumSecRecuperacion,
	 		             "pstrNumSecRecupComi", strNumSecRecupComi,
	 		             "pstrCodComisionTipo", strCodComisionTipo
                        ];

        fn_util_AjaxSyncWM("frmCobroMasivoRegistro.aspx/ListadoFraccionamiento",
                arrParametros,
                function(jsondata) {
                    var subgrid = jQuery("#" + pSubgrid_table_id)[0];
                    subgrid.addJSONData(jsondata);
                    parent.fn_unBlockUI();
                    fn_doResize();
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

    } catch (ex) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }

}

//****************************************************************
// Funcion		:: 	fn_ListadoModal
// Descripción	::	
// Log			:: 	WCR - 27/11/2012
//****************************************************************
function fn_ActualizarListado() {
    if ($('#hidFlagRegistro').val() == '1') {
        if ($('#hidOpcion').val() == C_TX_EDITAR) { fn_LimpiarHidden(); fn_buscarListarEditar(false); }
        else { fn_buscar(true); }
    }
    else { fn_LimpiarHidden(); }
}

function fn_LimpiarHidden() {
    //$("#hidCodSolicitudCredito").val('');
    $("#hidTipoRubroFinanciamiento").val('');
    $("#hidCodIfi").val('');
    $("#hidTipoRecuperacion").val('');
    $("#hidNumSecRecuperacion").val('0');
    $("#hidNumSecRecupComi").val('0');
    $("#hidCodComisionTipo").val('');
    $("#hidEstadoRecuperacion").val('');
    $("#hidFlagIndividual").val('');
    $("#hidItem").val('');
    jQuery("#jqGrid_lista_A").jqGrid('resetSelection');

}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos(pstrCodContrato, pstrCodBien, pstrCodRelacionado, pstrCodTipo, pstrEstado) {
    var strVer = '0';
    if (pstrEstado != 'C') { strVer = 1 }

    parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + "&hddVer=" + strVer, 800, 350, function() { });
}


function fn_limpiar() {
    bFirstClick = false;
    fn_LimpiarHidden();
    $("#jqGrid_lista_A").GridUnload();
    fn_cargaGrilla();
}


//---Editar Fraccionamiento------------------------------------------------------------///

//****************************************************************
// Función		:: 	fn_ActualizarFecha
// Descripción	::	
// Log			:: 	WCR - 11/12/2012
//****************************************************************
function fn_ActualizarFecha(pControl, pRow, pGrid) {
    var strFechaCobro = pControl.value;
    var objFechaCobro = new Object();
    var intHeight = 25;
    var intCantError = 0;

    if (strFechaCobro != '') {
        rowDataPrin = $("#" + pGrid).jqGrid('getRowData', pRow);

        var sbError = new StringBuilderEx();
        var strFechaAnterior = '';
        var strFechaSiguiente = '';
        var rowData;
        var strMsnAnt = 'de cobro';
        var strMsnSig = 'de vencimiento del contrato';
        var intCuota = parseInt(fn_util_Right(pRow, 2), 10);
        var intAntC = intCuota - 1;
        var intSigC = intCuota + 1;
        var intAnt = parseFloat(pRow) - 1;
        var intSig = parseFloat(pRow) + 1;

        var decNroCuotas = parseInt(rowDataPrin.MaximaCuota);
        if (intAntC <= 0) { strFechaAnterior = rowDataPrin.FechaRecuperacion; }
        else {
            rowData = $("#" + pGrid).jqGrid('getRowData', intAnt);
            strFechaAnterior = rowData.FechaCobro;
            strMsnAnt = 'anterior'
        }

        if (intSigC > decNroCuotas) { strFechaSiguiente = rowDataPrin.FechaVencimiento; }
        else {
            rowData = $("#" + pGrid).jqGrid('getRowData', intSig);
            strFechaSiguiente = rowData.FechaCobro;
            strMsnSig = 'siguiente';
        }
        //        alert(strFechaAnterior);
        //        alert(strFechaSiguiente);
        if (strFechaAnterior != '') {
            var boolRpta = fn_util_ComparaFecha(strFechaAnterior, strFechaCobro);
            if (!boolRpta) { sbError.append('&nbsp;&nbsp;- La fecha ingresada no puede ser menor a la fecha<br/>&nbsp;&nbsp;&nbsp;&nbsp;' + strMsnAnt + '(' + strFechaAnterior + ')<br/>'); intCantError++; intCantError++; }
        }
        if (strFechaSiguiente != '') {
            if (strFechaSiguiente != strFechaCobro) {
                var boolRpta = fn_util_ComparaFecha(strFechaSiguiente, strFechaCobro);
                if (boolRpta) { sbError.append('&nbsp;&nbsp;- La fecha ingresada no puede ser mayor a la fecha <br/>&nbsp;&nbsp;&nbsp;&nbsp;' + strMsnSig + '(' + strFechaSiguiente + ')<br/>'); intCantError++; intCantError++; }
            }
        }
        objFechaCobro = fn_CalcularDias(sbError, strFechaAnterior, strFechaCobro);
        var intDifFechaError = parseInt(objFechaCobro.CantError, 10);
        intCantError = intCantError + intDifFechaError;

        //alert(sbError.toString());
        if (sbError.toString() != '') {
            intHeight = intHeight * intCantError;
            intValidFecha = intCantError;
            strValidFecha = sbError.toString();
            fn_util_MuestraMensaje(sbError.toString(), "E", intHeight);
            sbError = null;
            pControl.focus();
        }
        else {
            strValidFecha = '';
            intValidFecha = 0;
            var objFraccionar = new Object();
            var decCapital = fn_CalcularCapital(pRow, rowDataPrin.MontoReembolso, pGrid);
            rowDataOld = rowDataPrin;
            objFraccionar = fn_CalculaComision($('#' + pRow + '_Importe').val(), objFechaCobro.Dias, decCapital, rowDataPrin.PorcentajeTEA, 0);

            $('#' + pRow + '_Comision').val(objFraccionar.Comision);
            $("#" + pGrid).jqGrid('setCell', pRow, 'IGVComision', objFraccionar.IGVComision);
            $("#" + pGrid).jqGrid('setCell', pRow, 'Interes', objFraccionar.Interes);
            $("#" + pGrid).jqGrid('setCell', pRow, 'TotalPagar', objFraccionar.Total);
            $("#" + pGrid).jqGrid('setCell', pRow, 'Dias', objFechaCobro.Dias);
            if (parseInt(rowDataPrin.NroCuota) < decNroCuotas) {
                var pRowSig = parseFloat(pRow) + 1;
                rowDataSig = $("#" + pGrid).jqGrid('getRowData', pRowSig);
                strFechaAnterior = strFechaCobro;
                strFechaCobro = rowDataSig.FechaCobro;

                decCapital = fn_CalcularCapital(pRowSig, rowDataSig.MontoReembolso, pGrid);
                objFechaCobro = fn_CalcularDias(sbError, strFechaAnterior, strFechaCobro);
                objFraccionar = fn_CalculaComision(rowDataSig.Importe, objFechaCobro.Dias, decCapital, rowDataSig.PorcentajeTEA, rowDataSig.Comision);

                $("#" + pGrid).jqGrid('setCell', pRowSig, 'IGVComision', objFraccionar.IGVComision);
                $("#" + pGrid).jqGrid('setCell', pRowSig, 'Interes', objFraccionar.Interes);
                $("#" + pGrid).jqGrid('setCell', pRowSig, 'TotalPagar', objFraccionar.Total);
                $("#" + pGrid).jqGrid('setCell', pRowSig, 'Dias', objFechaCobro.Dias);
            }

        }
    }
    //    else {
    //        parent.fn_mdl_alert('&nbsp;&nbsp;- Ingrese Fecha', function() {
    //            pControl.focus();
    //        });
    //    }

}

//****************************************************************
// Función		:: 	fn_CalcularDias
// Descripción	::	
// Log			:: 	WCR - 17/12/2012
//****************************************************************
function fn_CalcularDias(sbError, pFechaAnterior, pFechaCobro) {
    var objFechaCobro = new Object();
    var arrFecha = pFechaCobro.split('/');
    var strFechaCobro = arrFecha[2] + '-' + arrFecha[1] + '-' + arrFecha[0];
    var arrFechaFin = pFechaAnterior.split('/');
    var strFechaAnterior = arrFechaFin[2] + '-' + arrFechaFin[1] + '-' + arrFechaFin[0];

    var intCantError = 0;
    objFechaCobro.FechaCobro = pFechaCobro;
    var arrParametros = ["pstrFlag", "3",
                             "pstrFecha", strFechaAnterior,
                             "pstrFechaFin", strFechaCobro,
                             "pstrAddMonth", "0"
                            ];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whValidarFeriado', '../../');
    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            if (arrResultado[1] != '') {
                var arrDato = arrResultado[1].split('*');
                //                if (arrDato[1] != '') {
                //                    //sbError.append('&nbsp;&nbsp;- ' + arrDato[1].substring(0, 55) + '<br/>&nbsp;&nbsp;&nbsp;&nbsp;' + arrDato[1].substring(56, arrDato[1].length) + '<br/>'); intCantError++; intCantError++; 
                //                }
                //                else {
                //                    if (arrDato[0] == '') {
                //                        var arrFechaCobro = arrDato[3].split('-');
                //                        objFechaCobro.FechaCobro = arrFechaCobro[2] + '/' + arrFechaCobro[1] + '/' + arrFechaCobro[0];
                //                    } else {
                //                        var arrFechaCobro = arrDato[0].split('-');
                //                        objFechaCobro.FechaCobro = arrFechaCobro[2] + '/' + arrFechaCobro[1] + '/' + arrFechaCobro[0];
                //                    }
                objFechaCobro.Dias = parseFloat(arrDato[2]);
                //                }
            }
        }
        else {
            var strError = arrResultado[1];
            sbError.append('&nbsp;&nbsp;- ' + arrDato[1] + '<br/>'); intCantError++;
        }
    }
    objFechaCobro.CantError = intCantError;
    return objFechaCobro;
}

//****************************************************************
// Función		:: 	fn_ActualizarImporte
// Descripción	::	
// Log			:: 	WCR - 11/12/2012
//****************************************************************
function fn_ActualizarImporte(pControl, pRow, pGrid) {
    var objFraccionar = new Object();

    var rowData = $("#" + pGrid).jqGrid('getRowData', pRow);
    var decCapital = fn_CalcularCapital(pRow, rowData.MontoReembolso, pGrid);

    rowDataOld = rowData;
    objFraccionar = fn_CalculaComision(pControl.value, rowData.Dias, decCapital, rowData.PorcentajeTEA, 0);

    $('#' + pRow + '_Comision').val(objFraccionar.Comision);
    //$("#jqGrid_lista_A").jqGrid('setCell', pRow, 'Comision', objFraccionar.Comision);
    // $("#" + pGrid).jqGrid('setCell', pRow, 'Importe', pControl.value);
    //alert(objFraccionar.Interes);
    $("#" + pGrid).jqGrid('setCell', pRow, 'IGVComision', objFraccionar.IGVComision);
    $("#" + pGrid).jqGrid('setCell', pRow, 'Interes', objFraccionar.Interes);
    $("#" + pGrid).jqGrid('setCell', pRow, 'TotalPagar', objFraccionar.Total);

    fn_ActualizarInteres(pRow, rowData.MontoReembolso, pGrid);
}

//****************************************************************
// Función		:: 	fn_CalcularCapital
// Descripción	::	
// Log			:: 	WCR - 11/12/2012
//****************************************************************
function fn_ActualizarInteres(pRow, pMontoReembolso, pGrid) {
    var ids = jQuery("#" + pGrid).jqGrid('getDataIDs');
    for (var i = 0; i < ids.length; i++) {
        if (parseFloat(ids[i]) > parseFloat(pRow)) {

            var objFraccionar = new Object();
            var decCapital = fn_CalcularCapital(ids[i], pMontoReembolso, pGrid);
            var rowData = $("#" + pGrid).jqGrid('getRowData', ids[i]);
            objFraccionar = fn_CalculaComision(rowData.Importe, rowData.Dias, decCapital, rowData.PorcentajeTEA, 0);

            //$('#' + pRow + '_Comision').val(objFraccionar.Comision);
            //$("#jqGrid_lista_A").jqGrid('setCell', pRow, 'Comision', objFraccionar.Comision);
            $("#" + pGrid).jqGrid('setCell', ids[i], 'IGVComision', objFraccionar.IGVComision);
            $("#" + pGrid).jqGrid('setCell', ids[i], 'Interes', objFraccionar.Interes);
            $("#" + pGrid).jqGrid('setCell', ids[i], 'TotalPagar', objFraccionar.Total);

        }
    }
}

//****************************************************************
// Función		:: 	fn_ActualizarComision
// Descripción	::	
// Log			:: 	WCR - 11/12/2012
//****************************************************************
function fn_ActualizarComision(pControl, pRow, pGrid) {
    var objComision = new Object();
    var rowData = $("#" + pGrid).jqGrid('getRowData', pRow);
    rowDataOld = rowData;

    var decPorcentajeIGV = fn_util_ValidaDecimal($('#hidIGV').val());
    var decImporte = fn_util_ValidaDecimal($('#' + pRow + '_Importe').val());
    var decComision = fn_util_ValidaDecimal(pControl.value);
    var decIGV = decComision * decPorcentajeIGV;
    var decInteres = fn_util_ValidaDecimal(rowData.Interes);
    var decTotal = decImporte + decComision + decIGV + decInteres;

    $("#" + pGrid).jqGrid('setCell', pRow, 'IGVComision', fn_util_AddCommas(fn_util_RedondearDecimales(decIGV, 2)));
    //$("#jqGrid_lista_A").jqGrid('setCell', pRow, 'Interes', objFraccionar.Interes);
    $("#" + pGrid).jqGrid('setCell', pRow, 'TotalPagar', fn_util_AddCommas(fn_util_RedondearDecimales(decTotal, 2)));
}


//****************************************************************
// Función		:: 	fn_CalculaComision
// Descripción	::	
// Log			:: 	WCR - 07/12/2012
//****************************************************************
function fn_CalculaComision(pstrImporte, pstrDias, pdecCapital, pstrTea, pstrComision) {
    var objFraccionar = new Object();
    if (pstrImporte == "") { pstrImporte = "0"; }
    var decImporte = fn_util_ValidaDecimal(pstrImporte);

    var intDias = parseFloat(pstrDias);
    var decPorcentajeIGV = fn_util_ValidaDecimal($('#hidIGV').val());
    var decComision = fn_util_ValidaDecimal(pstrComision);
    if (decComision == 0) { decComision = decImporte * (fn_util_ValidaDecimal($('#hidTasaDefecto').val()) / 100); }
    var decIGV = decComision * decPorcentajeIGV;
    var decInteres = fn_CalculoInteres(pdecCapital, pstrDias, pstrTea);
    var decTotal = decImporte + decComision + decIGV + decInteres;

    objFraccionar.Comision = fn_util_AddCommas(fn_util_RedondearDecimales(decComision, 2));
    objFraccionar.IGVComision = fn_util_AddCommas(fn_util_RedondearDecimales(decIGV, 2));
    objFraccionar.Interes = fn_util_AddCommas(fn_util_RedondearDecimales(decInteres, 2));
    objFraccionar.Total = fn_util_AddCommas(fn_util_RedondearDecimales(decTotal, 2));
    return objFraccionar;
}

//****************************************************************
// Función		:: 	fn_CalculoInteres
// Descripción	::	
// Log			:: 	WCR - 07/12/2012
//****************************************************************
function fn_CalculoInteres(pdecCapital, pintDias, pstrTea) {
    var decInteres = 0;
    var strDiaAnio = parseFloat($("#hidDiaAnio").val());
    var decTea = (1 + (parseFloat(pstrTea) / 100));
    var intDias = pintDias / strDiaAnio;
    decInteres = (Math.pow(decTea, intDias) - 1) * pdecCapital;
    if (decInteres <= 0) { decInteres = 0; }
    return decInteres;
}

//****************************************************************
// Función		:: 	fn_CalcularCapital
// Descripción	::	
// Log			:: 	WCR - 11/12/2012
//****************************************************************
function fn_CalcularCapital(pRow, pMontoReembolso, pGrid) {
    var decImporte = fn_util_ValidaDecimal(pMontoReembolso);
    var decCapital = decImporte;
    var ids = $("#" + pGrid).jqGrid('getDataIDs');
    for (var i = 0; i < ids.length; i++) {
        if (parseFloat(ids[i]) < parseFloat(pRow)) {
            var rowData = $("#" + pGrid).jqGrid('getRowData', ids[i]);
            var decMonto = fn_util_ValidaDecimal(rowData.Importe);
            if (isNaN(decMonto)) {
                decMonto = fn_util_ValidaDecimal($('#' + ids[i] + '_Importe').val());
            }
            decCapital = decCapital - decMonto;
        }
    }
    return decCapital;
}


function fn_guardarFraccionamiento(pRow, pGrid) {
    var sbError = new StringBuilderEx();

    var rowData = $("#" + pGrid).jqGrid('getRowData', pRow);
    var intHeigt = 25;
    var intCantError = 0;

    intCantError = fn_ValidarRegistroFraccionamiento(sbError, pGrid, rowData);
    if (sbError.toString() != '') {
        parent.fn_unBlockUI();
        intHeigt = intHeigt * intCantError;
        if (intCantError == 1) { intHeigt = 30; }
        fn_util_MuestraMensaje(sbError.toString(), "E", intHeigt);
        //        fn_util_MuestraLogPage(sbError.toString(), "E");
        //parent.fn_mdl_alert(sbError.toString(), function() { });
        sbError = null;
    } else {
        strValidFecha = '';
        intValidFecha = 0;
        var strRegistro = '';

        var ids = jQuery("#" + pGrid).jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var rowData = $("#" + pGrid).jqGrid('getRowData', ids[i]);
            var strFecha = rowData.FechaCobro;
            var arrFecha = strFecha.split('/');
            strFecha = arrFecha[2] + '-' + arrFecha[1] + '-' + arrFecha[0];

            strRegistro = strRegistro + rowData.NroCuota + '*';
            strRegistro = strRegistro + strFecha + '*';
            strRegistro = strRegistro + fn_util_ValidaDecimal(rowData.Importe) + '*';
            strRegistro = strRegistro + fn_util_ValidaDecimal(rowData.Comision) + '*';
            strRegistro = strRegistro + fn_util_ValidaDecimal(rowData.IGVComision) + '*';
            strRegistro = strRegistro + fn_util_ValidaDecimal(rowData.Interes) + '*';
            strRegistro = strRegistro + fn_util_ValidaDecimal(rowData.TotalPagar) + '*';
            strRegistro = strRegistro + rowData.EstadoCobro + '*';
            strRegistro = strRegistro + rowData.Dias + '|';
        }

        var arrParametros = ["pstrNumeroContrato", rowData.CodOperacionActiva,
                             "pstrTipoRubroFinanciamiento", rowData.TipoRubroFinanciamiento,
                             "pstrCodIfi", rowData.CodIfi,
                             "pstrTipoRecuperacion", rowData.TipoRecuperacion,
                             "pstrNumSecRecuperacion", rowData.NumSecRecuperacion,
    	 		             "pstrNumSecRecupComi", rowData.NumSecRecupComi,
    	 		             "pstrCodComisionTipo", rowData.CodComisionTipo,
    		                 "pstrRegistro", strRegistro
                            ];

        fn_util_AjaxWM("frmCobroMasivoRegistro.aspx/GrabaFraccionamiento",
                    arrParametros,
                    function(result) {
                        if (fn_util_trim(result) == "0") {
                            parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "GRABAR FRACCIONAMIENTO");
                        } else {
                            fn_util_MuestraLogPage("El fraccionamiento se actualizó correctamente.", "I");
                            //fn_SetearFlagRegistro('1');
                            //fn_RedireccionGrabar();
                            //parent.fn_unBlockUI();
                            //parent.fn_mdl_mensajeOk("El fraccionamiento del cobro se grabó correctamente.", function() { fn_RedireccionGrabar(); }, "GRABAR FRACCIONAMIENTO");

                        }
                    },
                     function(resultado) {
                         parent.fn_unBlockUI();
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "GRABAR FRACCIONAMIENTO");
                     }

                );
    }
}

//****************************************************************
// Función		:: 	fn_ValidarRegistroFraccionamiento
// Descripción	::	
// Log			:: 	WCR - 13/12/2012
//****************************************************************
function fn_ValidarRegistroFraccionamiento(sbError, pGrid, pRowData) {
    //rowData.MontoReembolso, rowData.MaximaCuota, rowData.FechaVencimiento
    //    if (intActGrid == 1) { sbError.append('&nbsp;&nbsp;- Termine de editar el registro<br/>'); }
    //    else {
    var intCantError = 0;
    if (strValidFecha != '') { sbError.append(strValidFecha); intCantError = intCantError + intValidFecha; }
    if (fn_util_trim(pRowData.FechaCobro) == '') { sbError.append('&nbsp;&nbsp;- Ingrese una fecha de cobro en la cuota N° ' + pRowData.NroCuota + '<br/>'); intCantError++; }
    if (fn_util_ValidaDecimal(pRowData.Importe) == 0) { sbError.append('&nbsp;&nbsp;- Ingrese un importe en la cuota N° ' + pRowData.NroCuota + '<br/>'); intCantError++; }
    if (fn_util_ValidaDecimal(pRowData.Comision) == 0) { sbError.append('&nbsp;&nbsp;- Ingrese una comisión en la cuota N° ' + pRowData.NroCuota + '<br/>'); intCantError++; }


    var ids = jQuery("#" + pGrid).jqGrid('getDataIDs');
    if (ids.length == 0) { sbError.append('&nbsp;&nbsp;- No hay datos para grabar<br/>'); intCantError++; }
    else {
        var decImporte = 0;
        var decImporteCobro = fn_util_ValidaDecimal(pRowData.MontoReembolso);
        var intNroCuotas = parseInt(pRowData.MaximaCuota, 10);
        var strFechaCobroFin = '';

        for (var i = 0; i < ids.length; i++) {
            var rowData = $("#" + pGrid).jqGrid('getRowData', ids[i]);
            decImporte = decImporte + fn_util_ValidaDecimal(rowData.Importe);
            if ((i + 1) == intNroCuotas) { strFechaCobroFin = rowData.FechaCobro; }
        }

        if (decImporteCobro != decImporte) { sbError.append('&nbsp;&nbsp;- La suma de los importes del fraccionamiento<br/>&nbsp;&nbsp;&nbsp;&nbsp;no coincide con el importe del cobro<br/>'); intCantError++; intCantError++; }
        if ((pRowData.FechaVencimiento != '') && (strFechaCobroFin != '')) {
            if (pRowData.FechaVencimiento != strFechaCobroFin) {
                //                    alert(pFechaVencimiento);
                //                    alert(strFechaCobroFin);
                var boolRpta = fn_util_ComparaFecha(pRowData.FechaVencimiento, strFechaCobroFin);
                if (boolRpta) { sbError.append('&nbsp;&nbsp;- La fecha del último cobro no puede ser mayor a la<br/>&nbsp;&nbsp;&nbsp;&nbsp;fecha de vencimiento del contrato(' + pRowData.FechaVencimiento + ')<br/>'); intCantError++; intCantError++; }
            }
        }
    }
    //    }
    return intCantError;
}

//****************************************************************
// Función		:: 	Fn_util_ReturnValidDecimalx
// Descripción	::	
// Log			:: 	WCR - 13/12/2012
//****************************************************************
function Fn_util_ReturnValidDecimalx(numberString) {
    // Prefijo si se va a devolver el símbolo de moneda.
    var prefix = "";
    // Valor a retornar si el dato de la celda es null.
    var nullValue = "";
    numberString = fn_util_ReplaceAll(numberString, ",", "");
    // Evalúa los posibles datos null
    if (isNaN(numberString) || numberString == "" || numberString == null || numberString == undefined) {
        return nullValue;
    }
    else {
        // Se utiliza una función de jquery que permite usar una formato para el número
        // (valor a ser evaluado, máscara a usar para el formato, tipo de formato local devuelto)        
        var amt = fn_util_ValidaDecimal(numberString);
        var validDecimal = amt.toFixed(2);
        validDecimal = fn_util_ValidaMonto(validDecimal, 2);

        // Concatena el prefijo y el valor numérico con formato.
        return prefix + validDecimal;
    }
}


function fn_DateFormatterx(cellvalue, options, rowObject) {
    //object[property]
    var miliseconds = cellvalue["time"];
    var myDate = new Date(miliseconds);
    var str = myDate.format("dd/mm/yyyy");

    return str;
}


//****************************************************************
// Función		:: 	Fn_enviarCarta
// Descripción	::	
// Log			:: 	SCA - 14/01/2013
//****************************************************************
function fn_enviarCarta() {
	var vEC = jQuery("#jqGrid_lista_A").getGridParam('selarrrow');
	var count = vEC.length;
	if(count == 0) 
	{
	   parent.fn_mdl_mensajeIco("Seleccione al menos un registro para poder Enviar Carta.", "util/images/warning.gif", "ENVIAR ");	
	}else{
	    var strSelected = "";
	    for (var j = 0; j < count; j++) {
	        strSelected = strSelected + vEC[j] + '|';
	    	var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', vEC[j]);
	    	
	    	
	    	fn_enviarPdf(rowData.ClienteRazonSocial,
	                      rowData.ClienteDomicilioLegal, //"pstrDireccion",
	                      rowData.Departamento, //"pstrDepartamento",
	                      rowData.Provincia, //"pstrProvincia",
	                      rowData.Distrito, //"pstrDistrito",
	                      rowData.Concepto,// "pstrConcepto",
	                      rowData.CodSolicitudCredito,//"pstrNumContrato",
	                      rowData.FecPago,//"pstrFechaPago",
	                      rowData.SimMoneda,//"pstrSimMoneda",
	                      rowData.Total,//"pstrImporte",
	                      rowData.TipoCambio,//"pstrTipoCambio",
	                      rowData.MontoReembolso,//"pstrMonto",
	                      rowData.MontoComision,//"pstrComision",
	                      rowData.MontoIGV,//"pstrIgv",
	                      rowData.FechaRecuperacion,//"pstrFechaCobro"
	                      rowData.CodComisionTipo,
	                      rowData.NroLote,
	    		          rowData.CorreoCliente
	    	               );
	    }
	   parent.fn_mdl_mensajeOk("La Carta se envio correctamente.", function() {  }, "ENVIAR CARTA CORRECTO");                        
	   //parent.fn_mdl_mensajeIco(strSelected, "util/images/warning.gif", "ENVIAR ");	fn_RedireccionGrabar();
	}
}

function fn_enviarPdf(pstrRazonSocial,
	                  pstrDireccion,
	                  pstrDepartamento,
	                  pstrProvincia,
	                  pstrDistrito,
	                  pstrConcepto,
	                  pstrNumContrato,
	                  pstrFechaPago,
	                  pstrSimMoneda,
	                  pstrImporte,
	                  pstrTipoCambio,
	                  pstrMonto,
	                  pstrComision,
	                  pstrIgv,
	                  pstrFechaCobro,
	                  pstrCodComisionTipo,
	                  pstrNroLote,
	                  pstrCorreoCliente) {
	
		var arrParametros = [     
	                        "pstrRazonSocial",pstrRazonSocial,
	                        "pstrDireccion",pstrDireccion,
	                        "pstrDepartamento",pstrDepartamento,
	                        "pstrProvincia",pstrProvincia,
                            "pstrDistrito",pstrDistrito,
                            "pstrConcepto",pstrConcepto,
                            "pstrNumContrato",pstrNumContrato,
                            "pstrFechaPago",pstrFechaPago,
                            "pstrSimMoneda",pstrSimMoneda,
                            "pstrImporte",pstrImporte,
                            "pstrTipoCambio",pstrTipoCambio,
                            "pstrMonto",pstrMonto,
                            "pstrComision",pstrComision,
                            "pstrIgv",pstrIgv,
                            "pstrFechaCobro",pstrFechaCobro,
                            "pstrCodComisionTipo",pstrCodComisionTipo,
                            "pstrNroLote",pstrNroLote,
			                "pstrCorreoCliente",pstrCorreoCliente
							];
                
        fn_util_AjaxWM("frmCobroMasivoRegistro.aspx/EnviarCarta",
                 arrParametros,
                 function(result) {
                     parent.fn_unBlockUI();
//                     if (fn_util_trim(result) == "0") {
//                         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL ASIGNAR");
//                     } 
//                     else if(fn_util_trim(result) == "2"){
//						parent.fn_mdl_mensajeIco("El Lote ingresado no existe.", "util/images/error.gif", "ERROR AL ASIGNAR");
//                     }
//                     else if(fn_util_trim(result) == "3"){
//						parent.fn_mdl_mensajeIco("El Lote ingresado ya cuenta con un cheque asignado", "util/images/error.gif", "ERROR AL ASIGNAR");
//                     }
//                     else {                         
//                        parent.fn_mdl_mensajeOk("El cheque se asignó correctamente.", function() { fn_RedireccionGrabar(); }, "ASIGNACION CORRECTO");                        
//                     }
                 },
                 function(resultado) {
                     parent.fn_unBlockUI();
                     var error = eval("(" + resultado.responseText + ")");
                     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABADO");
                 }
        );
	
}