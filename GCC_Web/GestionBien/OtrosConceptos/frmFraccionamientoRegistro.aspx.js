//****************************************************************
// Variables Globales
//****************************************************************
var bFirstClick;
var rowDataOld;
var intActGrid = 0;
var intCantError = 0;
var C_TX_NUEVO = "NUEVO";
var C_TX_EDITAR = "EDITAR";
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	WCR - 20/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();

    //fn_configuraGrilla();

    jQuery("#txtNroCuotas").keydown(function(event) {
        if (event.which || event.keyCode) {
            if ((event.which == 13) || (event.keyCode == 13)) {
                fn_Generar();
                return false;
                
            }
        }
        else {
            return true
        }
    });
    if ($('#hidOpcion').val() != C_TX_NUEVO) {
        fn_configuraGrillaEditar();
        fn_CargaFraccionamiento();
        $('#txtNroCuotas').removeClass('css_input');
        $('#txtNroCuotas').addClass('css_input_inactivo');
        $('#txtNroCuotas').attr('readonly', true);
        $('#divGenerar').hide();
    } else {
        fn_configuraGrillaNuevo();
    }



    //On load Page (siempre al final)
    fn_onLoadPage();
});

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_inicializaCampos() {
    $("#txtNroCuotas").validText({ type: 'number', length: 2 });
    //$("#txtNroCuotas").val('4');
    //$("#txtImporte").val('1,040.00');
}

function fn_guardar() {
    var sbError = new StringBuilderEx();
    var intHeigt = 25;
    intCantError = 0;
    fn_ValidarRegistro(sbError);
    if (sbError.toString() != '') {
        intHeigt = intHeigt * intCantError;
        if (intCantError == 1) { intHeigt = 30; }
        fn_util_MuestraMensaje(sbError.toString(), "E", intHeigt);
        sbError = null;
        //        parent.fn_unBlockUI();
        //        parent.fn_mdl_alert(sbError.toString(), function() { });
        //        sbError = null;
    } else {
        var strRegistro = '';
        var ids = jQuery("#jqGrid_lista_A").jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', ids[i]);
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
    	 		             "pstrCodComisionTipo", strCodComisionTipo,
    		                 "pstrRegistro", strRegistro
                            ];

        fn_util_AjaxWM("frmFraccionamientoRegistro.aspx/GrabaFraccionamiento",
                    arrParametros,
                    function(result) {
                        if (fn_util_trim(result) == "0") {
                            parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "GRABAR FRACCIONAMIENTO");
                        } else {
                            fn_SetearFlagRegistro('1');
                            window.parent.frames[0].fn_util_MuestraLogPage("El fraccionamiento se registro correctamente.", "I");
                            fn_RedireccionGrabar();
                            parent.fn_unBlockUI();
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
// Funcion		:: 	fn_configuraGrilla
// Descripción	::	Carga Grilla
// Log			:: 	WCR - 20/11/2012
//****************************************************************

function fn_configuraGrillaNuevo() {

    $("#jqGrid_lista_A").jqGrid({
        //        datatype: function() {
        //            fn_buscarListar();
        //        },
        datatype: "local",
        //        jsonReader: {//Set the jsonReader to the JQGridJSonResponse squema to bind the data.
        //            root: "Items",
        //            page: "CurrentPage", // Número de página actual.
        //            total: "PageCount", // Número total de páginas.
        //            records: "RecordCount", // Total de registros a mostrar.
        //            repeatitems: false,
        //            id: "Id" // Índice de la columna con la clave primaria.
        //        },
        colNames: ['', 'N° Cuota', 'Fecha Cobro', 'Importe', 'Comisión', 'IGV', 'Interés', 'Total a Pagar', '', 'Estado Cobro', ''],
        colModel: [
				    { name: 'Id', index: 'Id', hidden: true, editable: true },
				    { name: 'NroCuota', index: 'NroCuota', sortable: false, width: 30, align: "right" },
				    { name: 'FechaCobro', index: 'FechaCobro', sortable: false, width: 40, sorttype: "date", align: "center", editable: true },
				    { name: 'Importe', index: 'Importe', sortable: false, width: 40, align: "right", editable: true },
				    { name: 'Comision', index: 'Comision', sortable: false, width: 40, align: "right", editable: true },
				    { name: 'IGVComision', index: 'IGVComision', sortable: false, width: 30, align: "right" },
				    { name: 'Interes', index: 'Interes', sortable: false, width: 30, align: "right" },
				    { name: 'TotalPagar', index: 'TotalPagar', sortable: false, width: 40, align: "right" },
				    { name: 'EstadoCobro', index: 'EstadoCobro', hidden: true },
				    { name: 'DesEstadoCobro', index: 'DesEstadoCobro', sortable: false, width: 45, align: "center" },
				    { name: 'Dias', index: 'Dias', hidden: true }
				],
        height: '100%',
        editurl: 'clientArray',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        multiselect: false,
        altclass: 'gridAltClass',
        ondblClickRow: function(rowid) {

            lastRowEdit = rowid;
            intActGrid = 1;
            var cm = jQuery('#jqGrid_lista_A').jqGrid('getColProp', 'Interes');
            jQuery('#jqGrid_lista_A').editRow(rowid, true);
            cm.editable = true;
            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").datepicker({ selectOtherMonths: true, changeYear: true, changeMonth: true,
                onSelect: function(dateText, inst) { fn_ActualizarFecha(this, rowid); }
            });

            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").trigger('onSelect');
            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").addClass('css_calendario');
            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").blur(function() {
                fn_ActualizarFecha(this, rowid);
            });
            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").keydown(function(event) {
                if (event.which || event.keyCode) {
                    if ((event.which == 13) || (event.keyCode == 13)) {
                        fn_ActualizarFecha(this, rowid);
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
                fn_ActualizarImporte(this, rowid);
            });
            jQuery("#" + rowid + "_Importe", "#jqGrid_lista_A").keydown(function(event) {
                if (event.which || event.keyCode) {
                    if ((event.which == 13) || (event.keyCode == 13)) {
                        fn_ActualizarImporte(this, rowid);
                        return false;
                    }
                }
                else {
                    return true
                }
            });

            jQuery("#" + rowid + "_Comision", "#jqGrid_lista_A").validNumber({ value: '', decimals: 2, length: 15 });
            jQuery("#" + rowid + "_Comision", "#jqGrid_lista_A").blur(function() {
                fn_ActualizarComision(this, rowid);
            });
            jQuery("#" + rowid + "_Comision", "#jqGrid_lista_A").keydown(function(event) {
                if (event.which || event.keyCode) {
                    if ((event.which == 13) || (event.keyCode == 13)) {
                        fn_ActualizarComision(this, rowid);
                        return false;
                    }
                }
                else {
                    return true
                }
            });
        },
        onSelectRow: function(rowid) {
            var ids = jQuery("#jqGrid_lista_A").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                jQuery("#jqGrid_lista_A").jqGrid('saveRow', ids[i]);
                var cm = jQuery("#jqGrid_lista_A").jqGrid('getColProp', 'Interes');
                cm.editable = false;
            }
            intActGrid = 0;

        }
    });

    $("#jqGrid_lista_A").setGridWidth($(window).width() - 50);
}

//****************************************************************
// Funcion		:: 	fn_configuraGrilla
// Descripción	::	Carga Grilla
// Log			:: 	WCR - 20/11/2012
//****************************************************************

function fn_configuraGrillaEditar() {

    $("#jqGrid_lista_A").jqGrid({
        //        datatype: function() {
        //            fn_buscarListar();
        //        },
        //datatype: "local",
        jsonReader: {//Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "Id" // Índice de la columna con la clave primaria.
        },
        colNames: ['', 'N° Cuota', 'Fecha Cobro', 'Importe', 'Comisión', 'IGV', 'Interés', 'Total a Pagar', '', 'Estado Cobro', ''],
        colModel: [
				    { name: 'Id', index: 'Id', hidden: true, editable: true },
				    { name: 'NroCuota', index: 'NroCuota', sortable: false, width: 30, align: "right" },
				    { name: 'FechaCobro', index: 'FechaCobro', sortable: false, width: 40, sorttype: "date", align: "center", editable: true },
				    { name: 'Importe', index: 'Importe', sortable: false, width: 40, align: "right", editable: true },
				    { name: 'Comision', index: 'Comision', sortable: false, width: 40, align: "right", editable: true },
				    { name: 'IGVComision', index: 'IGVComision', sortable: false, width: 30, align: "right" },
				    { name: 'Interes', index: 'Interes', sortable: false, width: 30, align: "right" },
				    { name: 'TotalPagar', index: 'TotalPagar', sortable: false, width: 40, align: "right" },
				    { name: 'EstadoCobro', index: 'EstadoCobro', hidden: true },
				    { name: 'DesEstadoCobro', index: 'DesEstadoCobro', sortable: false, width: 45, align: "center" },
				    { name: 'Dias', index: 'Dias', hidden: true} //,
        //{ name: '', index: '', width: 1 }

				],
        height: '100%',
        editurl: 'clientArray',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        //        rowNum: 10, // Tamaño de la página
        //        rowList: [10, 20, 30],
        //        sortname: 'NroCuota', // Columna a ordenar por defecto.
        //        sortorder: 'asc', // Criterio de ordenación por defecto.
        //        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        multiselect: false,
        altclass: 'gridAltClass',
        ondblClickRow: function(rowid) {
            //var row_id = $("#grid").getGridParam('selrow');
            lastRowEdit = rowid;
            intActGrid = 1;
            var cm = jQuery('#jqGrid_lista_A').jqGrid('getColProp', 'Interes');
            jQuery('#jqGrid_lista_A').editRow(rowid, true);
            cm.editable = true;
            //jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").focus();
            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").datepicker({ selectOtherMonths: true, changeYear: true, changeMonth: true,
                onSelect: function(dateText, inst) { fn_ActualizarFecha(this, rowid); }
            });

            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").trigger('onSelect');
            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").addClass('css_calendario');
            // fn_util_SeteaCalendario(jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A"));
            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").blur(function() {
                fn_ActualizarFecha(this, rowid);
            });
            jQuery("#" + rowid + "_FechaCobro", "#jqGrid_lista_A").keydown(function(event) {
                if (event.which || event.keyCode) {
                    if ((event.which == 13) || (event.keyCode == 13)) {
                        fn_ActualizarFecha(this, rowid);
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
                fn_ActualizarImporte(this, rowid);
            });
            jQuery("#" + rowid + "_Importe", "#jqGrid_lista_A").keydown(function(event) {
                if (event.which || event.keyCode) {
                    if ((event.which == 13) || (event.keyCode == 13)) {
                        fn_ActualizarImporte(this, rowid);
                        return false;
                    }
                }
                else {
                    return true
                }
            });

            jQuery("#" + rowid + "_Comision", "#jqGrid_lista_A").validNumber({ value: '', decimals: 2, length: 15 });
            jQuery("#" + rowid + "_Comision", "#jqGrid_lista_A").blur(function() {
                fn_ActualizarComision(this, rowid);
            });
            jQuery("#" + rowid + "_Comision", "#jqGrid_lista_A").keydown(function(event) {
                if (event.which || event.keyCode) {
                    if ((event.which == 13) || (event.keyCode == 13)) {
                        fn_ActualizarComision(this, rowid);
                        return false;
                    }
                }
                else {
                    return true
                }
            });

            //jQuery('#' + subgrid_table_id).jqGrid('editGridRow', rowid);
        },
        onSelectRow: function(rowid) {

            // jQuery('#' + subgrid_table_id).editRow(rowid, false);
            var ids = jQuery("#jqGrid_lista_A").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                //jQuery('#' + subgrid_table_id).editRow(ids[i], false, null, null, 'clientArray');
                //                if (parseFloat(ids[i]) != parseFloat(rowid)) {
                jQuery("#jqGrid_lista_A").jqGrid('saveRow', ids[i]);
                var cm = jQuery("#jqGrid_lista_A").jqGrid('getColProp', 'Interes');
                cm.editable = false;
                //                }
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

    //    for (var i = 0; i <= mydata.length; i++) {
    //        jQuery("#jqGrid_lista_A").jqGrid('addRowData', i + 1, mydata[i]);
    //    }

    //    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    //    $("#search_jqGrid_lista_A").hide();

    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 50);
}


//****************************************************************
// Función		:: 	fn_MensajeYRedireccionar
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
//                  Luego lo redirecciona al formulario de búsquedas ("frmTemporalListado.aspx").
// Log			:: 	WCR - 20/11/2012
//****************************************************************
var fn_MensajeYRedireccionar = function(pCodigo) {
    if (pCodigo == "0") {
        fn_mdl_alert("Se grabó con éxito la Observaciones : " + pCodigo.toString(), function() { });
    } else {
        fn_mdl_alert("Se actualizó con éxito Observacion ", function() { });
    }
};

//****************************************************************
// Función		:: 	fn_ListaFraccionamiento
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_ListaFraccionamiento() {
    var ctrlBtn = window.parent.frames[0].document.getElementById('cmdListarFraccionamiento');
    ctrlBtn.click();

    parent.fn_util_CierraModal();
}

//****************************************************************
// Función		:: 	fn_Generar
// Descripción	::	
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_Generar() {

    var decNroCuotas = fn_util_ValidaDecimal($('#txtNroCuotas').val());
    var decNroCuotasDefault = fn_util_ValidaDecimal($('#hidNroCuotas').val());
    var decCapital = 0;
    if (decNroCuotas > decNroCuotasDefault) {
        var strMensaje = 'Solo se puede generar hasta ' + $('#hidNroCuotas').val() + ' cuotas.';
        parent.fn_mdl_mensajeIco(strMensaje, "util/images/warning.gif", "ERROR FRACCIONAMIENTO");
    }
    else {

        $("#jqGrid_lista_A").jqGrid("clearGridData", true);
        var objFechaCobro = new Object();
        var objFraccionar = new Object();
        //var ids = jQuery("#jqGrid_lista_A").jqGrid('getDataIDs');
        var strImporte = $('#txtImporte').val() == undefined ? "0" : $('#txtImporte').val();
        var decImporte = fn_util_ValidaDecimal(strImporte);
        decCapital = decImporte;
        var decImporteFracc = parseFloat(fn_util_RedondearDecimales(decImporte / decNroCuotas, 2));
        var strImporteFracc = fn_util_AddCommas(fn_util_RedondearDecimales(decImporteFracc, 2));
        var strFechaCobro = $('#hidFechaCobro').val();
        var strEstadoCobro = $('#hidEstadoDefecto').val();
        var arrEstado = strEstadoCobro.split('|');

        for (var i = 1; i <= decNroCuotas; i++) {

            var decMontoReembolso = 0;

            if (i == decNroCuotas) { decMontoReembolso = decImporte - decImporteFracc * (decNroCuotas - 1); }
            else { decMontoReembolso = decImporteFracc; }

            strImporteFracc = fn_util_AddCommas(fn_util_RedondearDecimales(decMontoReembolso, 2));

            objFechaCobro = fn_CalculaFechaCobro(strFechaCobro);
            objFraccionar = fn_CalculaComision(strImporteFracc, objFechaCobro.Dias, decCapital, 0);

            var mydata = [{ Id: 0,
                NroCuota: i,
                FechaCobro: objFechaCobro.FechaCobro,
                Importe: strImporteFracc,
                Comision: objFraccionar.Comision,
                IGVComision: objFraccionar.IGVComision,
                Interes: objFraccionar.Interes,
                TotalPagar: objFraccionar.Total,
                EstadoCobro: arrEstado[0],
                DesEstadoCobro: arrEstado[1],
                Dias: objFechaCobro.Dias}];

                strFechaCobro = objFechaCobro.FechaCobro;
                decCapital = decCapital - decImporteFracc;

                jQuery("#jqGrid_lista_A").jqGrid('addRowData', i, mydata[0]);
            }

        }

    }

    //****************************************************************
    // Función		:: 	fn_CalculaFechaCobro
    // Descripción	::	
    // Log			:: 	WCR - 07/12/2012
    //****************************************************************
    function fn_CalculaFechaCobro(pstrFecha) {
        var objFechaCobro = new Object();
        var arrFecha = pstrFecha.split('/');
        var strFecha = arrFecha[2] + '-' + arrFecha[1] + '-' + arrFecha[0];
        var strFechaCobro = '';
        var arrParametros = ["pstrFlag", "2",
                             "pstrFecha", strFecha,
                             "pstrAddMonth", "30"
                        ];
        var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whValidarFeriado', '../../');
        if (arrResultado.length > 0) {
            if (arrResultado[0] == "0") {
                if (arrResultado[1] != '') {
                    var arrDato = arrResultado[1].split('*');
                    //                    if (arrDato[0] == '') {
                    var arrFechaCobro = arrDato[3].split('-');
                    objFechaCobro.FechaCobro = arrFechaCobro[2] + '/' + arrFechaCobro[1] + '/' + arrFechaCobro[0];
                    //                    } else {
                    //                        var arrFechaCobro = arrDato[0].split('-');
                    //                        objFechaCobro.FechaCobro = arrFechaCobro[2] + '/' + arrFechaCobro[1] + '/' + arrFechaCobro[0];
                    //                    }
                    objFechaCobro.Dias = parseFloat(arrDato[2]);
                }
            }
            else {
                var strError = arrResultado[1];
                objFechaCobro.FechaCobro = '';
                objFechaCobro.Dias = 0;
                //sbError.append('&nbsp;&nbsp;- ' + arrDato[1]);
            }
        }
        return objFechaCobro;
    }

    //****************************************************************
    // Función		:: 	fn_CalculaComision
    // Descripción	::	
    // Log			:: 	WCR - 07/12/2012
    //****************************************************************
    function fn_CalculaComision(pstrImporte, pstrDias, pdecCapital, pstrComision) {
        var objFraccionar = new Object();
        var strConcepto = $("#hidCodComisionTipo").val();
        var strMoneda = $("#hidCodMoneda").val();
        if (pstrImporte == "") { pstrImporte = "0"; }
        var decImporte = fn_util_ValidaDecimal(pstrImporte);
        var intDias = parseFloat(pstrDias);
        var decPorcentajeIGV = fn_util_ValidaDecimal($('#hidIGV').val());
        var decComision = fn_util_ValidaDecimal(pstrComision);
        if (decComision == 0) { decComision = decImporte * (fn_util_ValidaDecimal($('#hidTasaDefecto').val()) / 100); }
        //var decComision = decImporte * (fn_util_ValidaDecimal($('#hidTasaDefecto').val()) / 100);
        var decIGV = decComision * decPorcentajeIGV;

        var decInteres = fn_CalculoInteres(pdecCapital, pstrDias);
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
    function fn_CalculoInteres(pdecCapital, pintDias) {
        var decInteres = 0;
        var strTea = $("#hidTea").val();
        var strDiaAnio = parseFloat($("#hidDiaAnio").val());
        var decTea = (1 + (parseFloat(strTea) / 100));
        var intDias = pintDias / strDiaAnio;
        decInteres = (Math.pow(decTea, intDias) - 1) * pdecCapital;
        return decInteres;
    }

    //****************************************************************
    // Función		:: 	fn_ActualizarFecha
    // Descripción	::	
    // Log			:: 	WCR - 11/12/2012
    //****************************************************************
    function fn_ActualizarFecha(pControl, pRow) {
        var strFechaCobro = pControl.value;
        var objFechaCobro = new Object();

        if (strFechaCobro != '') {
            var intHeigt = 25;
            var intCantError = 0;
            var sbError = new StringBuilderEx();
            var strFechaAnterior = '';
            var strFechaSiguiente = '';
            var rowData;
            var strMsnAnt = 'de cobro';
            var strMsnSig = 'de vencimiento del contrato';
            var intAnt = parseInt(pRow, 10) - 1;
            var intSig = parseInt(pRow, 10) + 1;
            var decNroCuotas = parseInt($('#txtNroCuotas').val(), 10);
            if (intAnt <= 0) { strFechaAnterior = $('#hidFechaCobro').val(); }
            else {
                rowData = $("#jqGrid_lista_A").jqGrid('getRowData', intAnt);
                strFechaAnterior = rowData.FechaCobro;
                strMsnAnt = 'anterior'
            }

            if (intSig > decNroCuotas) { strFechaSiguiente = $('#hidFechaVencmiento').val(); }
            else {
                rowData = $("#jqGrid_lista_A").jqGrid('getRowData', intSig);
                strFechaSiguiente = rowData.FechaCobro;
                strMsnSig = 'siguiente';
            }
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

            if (sbError.toString() != '') {
                intHeigt = intHeigt * intCantError
                fn_util_MuestraMensaje(sbError.toString(), "E", intHeigt);
                sbError = null;
                //pControl.focus();                
                //                parent.fn_mdl_alert(sbError.toString(), function() {
                //                    sbError = null;
                //                    pControl.focus();
                //                });
            }
            else {
                var objFraccionar = new Object();
                var decCapital = fn_CalcularCapital(pRow);
                var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', pRow);
                rowDataOld = rowData;
                objFraccionar = fn_CalculaComision($('#' + pRow + '_Importe').val(), objFechaCobro.Dias, decCapital, 0);

                $('#' + pRow + '_Comision').val(objFraccionar.Comision);
                //$("#jqGrid_lista_A").jqGrid('setCell', pRow, 'Comision', objFraccionar.Comision);
                $("#jqGrid_lista_A").jqGrid('setCell', pRow, 'IGVComision', objFraccionar.IGVComision);
                $("#jqGrid_lista_A").jqGrid('setCell', pRow, 'Interes', objFraccionar.Interes);
                $("#jqGrid_lista_A").jqGrid('setCell', pRow, 'TotalPagar', objFraccionar.Total);
                $("#jqGrid_lista_A").jqGrid('setCell', pRow, 'Dias', objFechaCobro.Dias);

                if (parseInt(rowData.NroCuota, 10) < decNroCuotas) {
                    var pRowSig = parseFloat(pRow) + 1;
                    rowDataSig = $("#jqGrid_lista_A").jqGrid('getRowData', pRowSig);
                    strFechaAnterior = strFechaCobro;
                    strFechaCobro = rowDataSig.FechaCobro;

                    decCapital = fn_CalcularCapital(pRowSig);
                    objFechaCobro = fn_CalcularDias(sbError, strFechaAnterior, strFechaCobro);
                    objFraccionar = fn_CalculaComision(rowDataSig.Importe, objFechaCobro.Dias, decCapital, rowDataSig.Comision);

                    $("#jqGrid_lista_A").jqGrid('setCell', pRowSig, 'IGVComision', objFraccionar.IGVComision);
                    $("#jqGrid_lista_A").jqGrid('setCell', pRowSig, 'Interes', objFraccionar.Interes);
                    $("#jqGrid_lista_A").jqGrid('setCell', pRowSig, 'TotalPagar', objFraccionar.Total);
                    $("#jqGrid_lista_A").jqGrid('setCell', pRowSig, 'Dias', objFechaCobro.Dias);
                }
            }
        }
        //        else {
        //            parent.fn_mdl_alert('&nbsp;&nbsp;- Ingrese Fecha', function() {
        //                pControl.focus();
        //            });
        //        }

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
                    //                    if (arrDato[1] != '') {
                    //                        //sbError.append('&nbsp;&nbsp;- ' + arrDato[1].substring(0, 55) + '<br/>&nbsp;&nbsp;&nbsp;&nbsp;' + arrDato[1].substring(56, arrDato[1].length) + '<br/>'); intCantError++; intCantError++; 
                    //                    }
                    //                    else {
                    //                    if (arrDato[0] == '') {
                    //                        var arrFechaCobro = arrDato[3].split('-');
                    //                        objFechaCobro.FechaCobro = arrFechaCobro[2] + '/' + arrFechaCobro[1] + '/' + arrFechaCobro[0];
                    //                    } else {
                    //                        var arrFechaCobro = arrDato[0].split('-');
                    //                        objFechaCobro.FechaCobro = arrFechaCobro[2] + '/' + arrFechaCobro[1] + '/' + arrFechaCobro[0];
                    //                    }                        
                    objFechaCobro.Dias = parseFloat(arrDato[2]);
                    //                    }
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
    function fn_ActualizarImporte(pControl, pRow) {
        var objFraccionar = new Object();
        var decCapital = fn_CalcularCapital(pRow);
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', pRow);
        rowDataOld = rowData;
        objFraccionar = fn_CalculaComision(pControl.value, rowData.Dias, decCapital, 0);

        $('#' + pRow + '_Comision').val(objFraccionar.Comision);
        //$("#jqGrid_lista_A").jqGrid('setCell', pRow, 'Comision', objFraccionar.Comision);
        $("#jqGrid_lista_A").jqGrid('setCell', pRow, 'IGVComision', objFraccionar.IGVComision);
        $("#jqGrid_lista_A").jqGrid('setCell', pRow, 'Interes', objFraccionar.Interes);
        $("#jqGrid_lista_A").jqGrid('setCell', pRow, 'TotalPagar', objFraccionar.Total);
        fn_ActualizarInteres(pRow);
    }

    //****************************************************************
    // Función		:: 	fn_ActualizarComision
    // Descripción	::	
    // Log			:: 	WCR - 11/12/2012
    //****************************************************************
    function fn_ActualizarComision(pControl, pRow) {
        var objComision = new Object();
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', pRow);
        rowDataOld = rowData;

        var decPorcentajeIGV = fn_util_ValidaDecimal($('#hidIGV').val());
        var decImporte = fn_util_ValidaDecimal($('#' + pRow + '_Importe').val());
        var decComision = fn_util_ValidaDecimal(pControl.value);
        var decIGV = decComision * decPorcentajeIGV;
        var decInteres = fn_util_ValidaDecimal(rowData.Interes);
        var decTotal = decImporte + decComision + decIGV + decInteres;

        $("#jqGrid_lista_A").jqGrid('setCell', pRow, 'IGVComision', fn_util_AddCommas(fn_util_RedondearDecimales(decIGV, 2)));
        //$("#jqGrid_lista_A").jqGrid('setCell', pRow, 'Interes', objFraccionar.Interes);
        $("#jqGrid_lista_A").jqGrid('setCell', pRow, 'TotalPagar', fn_util_AddCommas(fn_util_RedondearDecimales(decTotal, 2)));
    }

    //****************************************************************
    // Función		:: 	fn_CalcularCapital
    // Descripción	::	
    // Log			:: 	WCR - 11/12/2012
    //****************************************************************
    function fn_CalcularCapital(pRow) {
        var decImporte = fn_util_ValidaDecimal($('#txtImporte').val());
        var decCapital = decImporte;

        var ids = jQuery("#jqGrid_lista_A").jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            if (parseFloat(ids[i]) < parseFloat(pRow)) {

                var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', ids[i]);
                var decMonto = fn_util_ValidaDecimal(rowData.Importe);
                if (isNaN(decMonto)) {
                    decMonto = fn_util_ValidaDecimal($('#' + ids[i] + '_Importe').val());
                }
                decCapital = decCapital - decMonto;
            }
        }
        return decCapital;
    }

    //****************************************************************
    // Función		:: 	fn_CalcularCapital
    // Descripción	::	
    // Log			:: 	WCR - 11/12/2012
    //****************************************************************
    function fn_ActualizarInteres(pRow) {
        var ids = jQuery("#jqGrid_lista_A").jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            if (parseFloat(ids[i]) > parseFloat(pRow)) {

                var objFraccionar = new Object();
                var decCapital = fn_CalcularCapital(ids[i]);
                var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', ids[i]);
                objFraccionar = fn_CalculaComision(rowData.Importe, rowData.Dias, decCapital, 0);

                //$('#' + pRow + '_Comision').val(objFraccionar.Comision);
                //$("#jqGrid_lista_A").jqGrid('setCell', pRow, 'Comision', objFraccionar.Comision);
                $("#jqGrid_lista_A").jqGrid('setCell', ids[i], 'IGVComision', objFraccionar.IGVComision);
                $("#jqGrid_lista_A").jqGrid('setCell', ids[i], 'Interes', objFraccionar.Interes);
                $("#jqGrid_lista_A").jqGrid('setCell', ids[i], 'TotalPagar', objFraccionar.Total);

            }
        }
    }

    //****************************************************************
    // Función		:: 	fn_ObtenerMinimoMaximo
    // Descripción	::	
    // Log			:: 	WCR - 12/12/2012
    //****************************************************************
    function fn_ObtenerMinimoMaximo(pstrImporte) {
        var objComision = new Object();
        var strConcepto = $("#hidCodComisionTipo").val();
        var strMoneda = $("#hidCodMoneda").val();
        var decImporte = fn_util_ValidaDecimal(pstrImporte);

        if ((strConcepto != '') && (strMoneda != '')) {
            var arrParametros = ["pstrCodigoConcepto", strConcepto,
                                 "pstrImporte", strImporte,
                                 "pstrCodMoneda", strMoneda,
                                ];
            var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whCalculoComision', '../../');
            if (arrResultado.length > 0) {
                if (arrResultado[0] == "0") {
                    objComision.IGV = arrResultado[3];
                    objComision.MontoMinimo = arrResultado[4];
                    objComision.MontoMaximo = arrResultado[5];
                }
                else {
                    objComision.IGV = '0.00';
                    objComision.MontoMinimo = '0.00';
                    objComision.MontoMaximo = '0.00';
                }
            }
        }
        else {
            objComision.IGV = '0.00';
            objComision.MontoMinimo = '0.00';
            objComision.MontoMaximo = '0.00';
        }
    }

    //****************************************************************
    // Función		:: 	fn_ValidarRegistro
    // Descripción	::	
    // Log			:: 	WCR - 13/12/2012
    //****************************************************************
    function fn_ValidarRegistro(sbError) {

        if (intActGrid == 1) { sbError.append('&nbsp;&nbsp;- Termine de editar el registro<br/>'); intCantError++; }
        else {

            var ids = jQuery("#jqGrid_lista_A").jqGrid('getDataIDs');
            if (ids.length == 0) { sbError.append('&nbsp;&nbsp;- No hay datos para grabar<br/>'); intCantError++; }
            else {
                var decImporte = 0;
                var decImporteCobro = fn_util_ValidaDecimal($('#txtImporte').val());
                var intNroCuotas = parseInt($('#txtNroCuotas').val(), 10);
                var strFechaCobroFin = '';

                for (var i = 0; i < ids.length; i++) {
                    var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', ids[i]);
                    decImporte = decImporte + fn_util_ValidaDecimal(rowData.Importe);
                    if ((i + 1) == intNroCuotas) { strFechaCobroFin = rowData.FechaCobro; }
                }

                if (decImporteCobro != decImporte) { sbError.append('&nbsp;&nbsp;- La suma de los importes del fraccionamiento<br/>&nbsp;&nbsp;&nbsp;&nbsp;no coincide con el importe del cobro<br/>'); intCantError++; intCantError++; }
                if (($('#hidFechaVencmiento').val() != '') && (strFechaCobroFin != '')) {
                    if ($('#hidFechaVencmiento').val() != strFechaCobroFin) {
                        var boolRpta = fn_util_ComparaFecha($('#hidFechaVencmiento').val(), strFechaCobroFin);
                        if (boolRpta) { sbError.append('&nbsp;&nbsp;- La fecha del último cobro no puede ser mayor a la<br/>&nbsp;&nbsp;&nbsp;&nbsp;fecha de vencimiento del contrato(' + $('#hidFechaVencmiento').val() + ')<br/>'); intCantError++; intCantError++; }
                    }
                }
            }
        }
        return sbError.toString();
    }

    //****************************************************************
    // Función		:: 	fn_SetearFlagRegistro
    // Descripción	::	
    // Log			:: 	WCR - 13/12/2012
    //****************************************************************
    function fn_SetearFlagRegistro(pFlag) {
        var winPag = window.parent.frames[0].document;
        var hidRegistro = winPag.getElementById("hidFlagRegistro");
        if (hidRegistro != null) { hidRegistro.value = pFlag; }

    }

    //****************************************************************
    // Función		:: 	fn_RedireccionGrabar
    // Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
    //                  Luego lo redirecciona al formulario de resgistro de cobro.
    // Log			:: 	WCR - 20/11/2012
    //****************************************************************
    function fn_RedireccionGrabar() {
        var winPag = window.parent.frames[0];
        winPag.fn_ActualizarListado();
        parent.fn_util_CierraModal();
    }

    //****************************************************************
    // Funcion		:: 	fn_CargaFraccionamiento  
    // Descripción	::	Carga Lista 
    // Log			:: 	WCR - 19/12/2012
    //****************************************************************
    function fn_CargaFraccionamiento() {

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
            alert(arrParametros);
            fn_util_AjaxSyncWM("frmFraccionamientoRegistro.aspx/ListadoFraccionamiento",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);
                    // fn_doResize();
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