//****************************************************************
// Variables Globales
//****************************************************************
var strModalidadTC = new Object();
strModalidadTC.Sunat = 'SBS';
strModalidadTC.Dia = 'PRF';

var bFirstClick;
var CrLf = 1;
var intPaginaActual = 1;
var C_GESTIONBIEN_TASACION = "006";

var lastRowEdit = null;
var intActGrid = 0;

var fn_CargarCombo = '';
var lRowData = null;
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

    fn_CargarCombo = $("#hddComboTasador").val();
    fn_configuraGrilla();
    //Busca con Enter




    //On load Page (siempre al final)
    fn_onLoadPage();

});

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {

    $('#txtcontrato').prop('readonly', true);
    $('#txtcontrato').attr('class', 'css_input_inactivo');

    $('#txtcucliente').prop('readonly', true);
    $("#txtcucliente").validText({ type: 'number', length: 8 });
    $('#txtcucliente').attr('class', 'css_input_inactivo');

    $('#txtrazonsocial').prop('readonly', true);
    $('#txtrazonsocial').attr('class', 'css_input_inactivo');

    $('#cmbbanca').prop('readonly', true);
    $('#cmbbanca').attr('class', 'css_input_inactivo');
    $("#cmbbanca").attr('disabled', 'disabled');

    $('#cbmEstadoContrato').prop('readonly', true);
    $('#cbmEstadoContrato').attr('class', 'css_input_inactivo');
    $("#cbmEstadoContrato").attr('disabled', 'disabled');

    $('#cmbEstadoContrato').attr('class', 'css_input_inactivo');
    $("#cbmEstadoContrato").attr('disabled', 'disabled');
    $('#cmbClasificacionBien').attr('class', 'css_input_inactivo');
    $("#cmbClasificacionBien").attr('disabled', 'disabled');

    $('#cmbEjecutivoLeasing').prop('readonly', true);
    $('#cmbEjecutivoLeasing').attr('class', 'css_input_inactivo');
    $("#cmbEjecutivoLeasing").attr('disabled', 'disabled');

    $('#txttipocambio').prop('readonly', true);
    $('#txttipocambio').attr('class', 'css_input_inactivo');

    $('#txtFechaActivacion').attr('class', 'css_input_inactivo');
    $('#txtFechaActivacion').prop('readonly', true);

    $('#txtcapitalfinanciadosoles').prop('readonly', true);
    $('#txtcapitalfinanciadosoles').attr('class', 'css_input_inactivo');

    $('#txtcapitalfinanciadodolar').prop('readonly', true);
    $('#txtcapitalfinanciadodolar').attr('class', 'css_input_inactivo');

    $('#txtsaldocapitalsoles').prop('readonly', true);
    $('#txtsaldocapitalsoles').attr('class', 'css_input_inactivo');


    $('#txtsaldocapitaldolares').prop('readonly', true);
    $('#txtsaldocapitaldolares').attr('class', 'css_input_inactivo');


    $('#txtdesejecutivobanca').prop('readonly', true);
    $('#txtdesejecutivobanca').attr('class', 'css_input_inactivo');

    $('#cmbMoneda').prop('readonly', true);
    $('#cmbMoneda').attr('class', 'css_input_inactivo');
    $("#cmbMoneda").attr('disabled', 'disabled');
    fnObtenerTipoCambioSunat($("#hddtiopcambiosunat").val());


    if ($('#cmbMoneda').val() == "001") {
        $('#txtcapitalfinanciadodolar').val(fn_util_ValidaDecimal($('#txtcapitalfinanciadosoles').val()) * fn_util_ValidaDecimal($('#txttipocambio').val()));
        $('#txtsaldocapitaldolares').val(fn_util_ValidaDecimal($('#txtsaldocapitalsoles').val()) * fn_util_ValidaDecimal($('#txttipocambio').val()));
    }


}


function fnObtenerTipoCambioSunat(resultado) {

    var varresult = resultado.split("$");
    var varTipoCambio;
    $("#txttipocambio").val(varresult[0]);

}



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
            id: "Id" // Índice de la columna con la clave primaria.
        },
        colNames: ['', 'Tipo Bien', 'Descripción Bien', 'Capital Financiado', 'Valor Comercial', 'Valor Realización', 'F. Asignación', 'F. Tasación', 'Tasador Asignado', '', '', '', 'Docs.'],
        colModel: [
        //{ name: 'DesTipoRubroFinanciamiento', index: 'DesTipoRubroFinanciamiento', sortable: true, width: 25, sorttype: "string", align: "center" },
                    {name: 'FechaTransferencia', index: 'FechaTransferencia', hidden: true, width: 25, editable: true },
                    { name: 'TipoBien', index: 'TipoBien', sortable: true, width: 25, sorttype: "string", align: "center" },
		            { name: 'Comentario', index: 'Comentario', sortable: true, width: 25, sorttype: "string", align: "center" },
		            { name: 'ValorBien', index: 'ValorBien', sortable: true, sorttype: "string", width: 25, align: "right", formatter: fn_ValorBien },
		            { name: 'ValorComercial', index: 'ValorComercial', sortable: true, sorttype: "string", width: 25, align: "right", formatter: Fn_util_ReturnValidDecimalx, editable: true },
		            { name: 'ValorEjecucion', index: 'ValorEjecucion', sortable: true, sorttype: "string", width: 25, align: "right", formatter: Fn_util_ReturnValidDecimalx, editable: true },
		            { name: 'FechaEncargo', index: 'FechaEncargo', sortable: true, width: 17, sorttype: "string", align: "center", editable: true },
		            { name: 'FechaTasacion', index: 'FechaTasacion', sortable: true, sorttype: "string", width: 17, align: "center", editable: true },
                    { name: 'DesTasador', index: 'DesTasador', sortable: false, width: 45, align: "left", sorttype: "string", editable: true, edittype: "select", editoptions: { value: fn_CargarCombo} },
        //{name: 'DesTasador', index: 'DesTasador', sortable: false, width: 45, align: "center", sorttype: "string" },
		            {name: 'SecFinanciamiento', index: 'SecFinanciamiento', width: 20, align: "center", sortable: false, hidden: true },
                    { name: 'CodTasador', index: 'CodTasador', width: 20, align: "center", sortable: false, hidden: true },
                    { name: 'CodTasacion', index: 'CodTasacion', width: 20, align: "center", sortable: false, hidden: true },
        //                    { name: 'Editar', index: 'Editar', width: 20, align: "center", formatter: Editar, sortable: false },
					{name: 'doc', index: 'doc', align: "center", sortable: false, formatter: fn_abreDocumentos, width: 10 }

        //{ name: 'ots', index: 'ots', width: 1 }
        	      ],
        height: '100%',
        editurl: 'clientArray',
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
            if (lastRowEdit != null) {
                if (intActGrid == 1) {
                    fn_ActualizarTasador(lastRowEdit);
                }
            }

        },
        ondblClickRow: function(id) {
            if (lastRowEdit == null) {
                var lRowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
                if (lRowData.FechaTransferencia == "") {
                    fn_GridEditable(id, lRowData);
                }
            }
        }
    });

    $("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 85);

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Funcion		:: 	fn_GridEditable
    // Descripción	::	Convierte la grilla editable
    // Log			:: 	WCR - 15/01/2013
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    function fn_GridEditable(pRow, pRowData) {
        lastRowEdit = pRow;
        intActGrid = 1;
        var cm = jQuery('#jqGrid_lista_A').jqGrid('getColProp', 'FechaTransferencia');
        jQuery('#jqGrid_lista_A').editRow(pRow, true);
        cm.editable = true;

        jQuery("#" + pRow + "_FechaEncargo", "#jqGrid_lista_A").datepicker({ selectOtherMonths: true, changeYear: true, changeMonth: true });
        jQuery("#" + pRow + "_FechaTasacion", "#jqGrid_lista_A").datepicker({ selectOtherMonths: true, changeYear: true, changeMonth: true });
        jQuery("#" + pRow + "_FechaEncargo", "#jqGrid_lista_A").addClass('css_calendario');
        jQuery("#" + pRow + "_FechaTasacion", "#jqGrid_lista_A").addClass('css_calendario');

        jQuery("#" + pRow + "_ValorComercial", "#jqGrid_lista_A").validNumber({ value: '', decimals: 2, length: 15 });
        jQuery("#" + pRow + "_ValorEjecucion", "#jqGrid_lista_A").validNumber({ value: '', decimals: 2, length: 15 });

        jQuery("#" + pRow + "_FechaEncargo", "#jqGrid_lista_A").keydown(function(event) {
            return fn_EventoControl(event);
        });
        jQuery("#" + pRow + "_FechaTasacion", "#jqGrid_lista_A").keydown(function(event) {
            return fn_EventoControl(event);
        });
        jQuery("#" + pRow + "_ValorComercial", "#jqGrid_lista_A").keydown(function(event) {
            return fn_EventoControl(event);
        });
        jQuery("#" + pRow + "_ValorEjecucion", "#jqGrid_lista_A").keydown(function(event) {
            return fn_EventoControl(event);
        });

        jQuery("#" + pRow + "_DesTasador", "#jqGrid_lista_A").val(pRowData.CodTasador);
        jQuery("#" + pRow + "_DesTasador", "#jqGrid_lista_A").change(function() {
            $("#jqGrid_lista_A").jqGrid('setCell', pRow, 'CodTasador', $(this).val());
        });

        fn_util_SeteaObligatorio(jQuery("#" + pRow + "_DesTasador", "#jqGrid_lista_A"), "select");
        fn_util_SeteaObligatorio(jQuery("#" + pRow + "_FechaEncargo", "#jqGrid_lista_A"), "calendar");
        fn_util_SeteaObligatorio(jQuery("#" + pRow + "_FechaTasacion", "#jqGrid_lista_A"), "calendar");
        fn_util_SeteaObligatorio(jQuery("#" + pRow + "_ValorComercial", "#jqGrid_lista_A"), "input");
        fn_util_SeteaObligatorio(jQuery("#" + pRow + "_ValorEjecucion", "#jqGrid_lista_A"), "input");

        jQuery("#" + pRow + "_FechaEncargo", "#jqGrid_lista_A").width(80);
        jQuery("#" + pRow + "_FechaTasacion", "#jqGrid_lista_A").width(80);
        jQuery("#" + pRow + "_ValorComercial", "#jqGrid_lista_A").width(100);
        jQuery("#" + pRow + "_ValorEjecucion", "#jqGrid_lista_A").width(100);

        jQuery("#" + pRow + "_ValorComercial", "#jqGrid_lista_A").css("text-align", "right");
        jQuery("#" + pRow + "_ValorEjecucion", "#jqGrid_lista_A").css("text-align", "right");

    }

    //**************************************************
    // Documentos
    //**************************************************
    function fn_abreDocumentos(cellvalue, options, rowObject) {

        var hddVer = "1";
        if (rowObject.FechaTransferencia == "") {
            hddVer = "0";
        }


        return "<img src='../../Util/images/ico_docs.gif' alt='Ver Documentos' title='Ver Documentos' width='18px' onclick=\"javascript:fn_GBAbreDocumentos(\'" + fn_util_trim($("#txtcontrato").val()) + "\',\'" + rowObject.SecFinanciamiento + "\',\'" + rowObject.CodTasacion + "\',\'" + C_GESTIONBIEN_TASACION + "\',\'" + hddVer + "\' );\" style='cursor:pointer;' />";
    };

    //**************************************************
    // ValorBien
    //**************************************************
    function fn_ValorBien(cellvalue, options, rowObject) {
        var strValorBien = fn_util_ValidaDecimal(rowObject.ValorBien);
        return "" + fn_util_ValidaMonto(strValorBien, 2);
    };


}

function fn_EventoControl(event) {
    if (event.which || event.keyCode) {
        if ((event.which == 27) || (event.keyCode == 27)) {
            lastRowEdit = null;
            return true;
        } else if ((event.which == 13) || (event.keyCode == 13)) {
            return false;
        }
    }
    else {
        return true
    }

}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_ActualizarTasador
// Descripción	::	Actualiza Tasador
// Log			:: 	WCR - 15/01/2013
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_ActualizarTasador(pRow) {
    var sbError = new StringBuilderEx();
    var intError = fn_ValidarRegistro(sbError, pRow);
    var intHeight = 22;
    if (sbError.toString() != '') {
        intHeight = intHeight * intError;
        if (intHeight == 22) { intHeight = 30; }
        //parent.fn_mdl_alert(sbError.toString(), function() { });
        fn_util_MuestraMensaje(sbError.toString(), "E", intHeight);
        sbError = null;
        fn_util_SeteaObligatorio(jQuery("#" + pRow + "_DesTasador", "#jqGrid_lista_A"), "select");
        fn_util_SeteaObligatorio(jQuery("#" + pRow + "_FechaEncargo", "#jqGrid_lista_A"), "calendar");
        fn_util_SeteaObligatorio(jQuery("#" + pRow + "_FechaTasacion", "#jqGrid_lista_A"), "calendar");
        fn_util_SeteaObligatorio(jQuery("#" + pRow + "_ValorComercial", "#jqGrid_lista_A"), "input");
        fn_util_SeteaObligatorio(jQuery("#" + pRow + "_ValorEjecucion", "#jqGrid_lista_A"), "input");
    } else {

        jQuery("#jqGrid_lista_A").jqGrid('saveRow', pRow);
        var cm = jQuery("#jqGrid_lista_A").jqGrid('getColProp', 'CodTasacion');
        cm.editable = false;

        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', pRow);
        var intCodTasacion = parseInt(rowData.CodTasacion, 10);

        var arrParametros = ["pCodSolicitudcredito", $('#hddCodigoContratos').val(),
                             "pCodTasacion", intCodTasacion.toString(),
                             "pvalorejecucion", rowData.ValorEjecucion,
                             "pvalorComercial", rowData.ValorComercial,
                             "pfechaencargo", rowData.FechaEncargo,
                             "pfechatasacion", rowData.FechaTasacion,
                             "pCodTasador", rowData.CodTasador,
                             "pSecfinanciamiento", rowData.SecFinanciamiento
                            ];

        fn_util_AjaxSyncWM("frmTasacionRegistro.aspx/ActualizaTasacion",
						arrParametros,
						function(request) {

						    $("#jqGrid_lista_A").jqGrid('setCell', pRow, 'CodTasacion', request);
						    if (fn_util_trim(rowData.CodTasador) == '0') { $("#jqGrid_lista_A").jqGrid('setCell', pRow, 'DesTasador', '&nbsp;'); }

						    fn_util_MuestraLogPage("Los datos se grabaron satisfactoriamente.", "I");
						    lastRowEdit = null;
						    intActGrid = 0;
						    if (intCodTasacion == 0) { fn_ListadoContratoBienTasador(); }
						    totaliza();
						},
						function(request) {
						    parent.fn_util_alert(jQuery.parseJSON(request.responseText).Message);
						    parent.fn_unBlockUI();
						}
			);

    }
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_ValidarRegistro
// Descripción	::	Valida registro
// Log			:: 	WCR - 15/01/2013
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_ValidarRegistro(sb, pRow) {
    var objcmbTasador = $('select[id=' + pRow + '_DesTasador]');
    var objFechaasignacion = $('input[id=' + pRow + '_FechaEncargo]:text');
    var objtxtFechatasacion = $('input[id=' + pRow + '_FechaTasacion]:text');
    var objtxtvalorrealizacion = $('input[id=' + pRow + '_ValorEjecucion]:text');
    var objtxtValorComercial = $('input[id=' + pRow + '_ValorComercial]:text');
    var intError = 0;


    //    if (fn_util_ValidaDecimal($('#' + pRow + '_ValorComercial').val()) == 0) {
    //        intError++;
    //        sb.append(fn_util_ValidateControl(objtxtValorComercial[0], 'valor Comercial', 1, ''));
    //    }
    //    if (fn_util_ValidaDecimal($('#' + pRow + '_ValorEjecucion').val()) == 0) {
    //        intError++;
    //        sb.append(fn_util_ValidateControl(objtxtvalorrealizacion[0], 'valor realización', 1, ''));
    //    }
    //    if ($('#' + pRow + '_FechaEncargo').val() == '') {
    //        intError++;
    //        sb.append(fn_util_ValidateControl(objFechaasignacion[0], 'Fecha Asignación', 1, ''));
    //        $('#' + pRow + '_FechaEncargo').removeClass('css_input_error');
    //        $('#' + pRow + '_FechaEncargo').addClass('css_calendario_error');
    //    }
    //    if ($('#' + pRow + '_FechaTasacion').val() == '') {
    //        intError++;
    //        sb.append(fn_util_ValidateControl(objtxtFechatasacion[0], 'Fecha Tasación', 1, ''));
    //        $('#' + pRow + '_FechaTasacion').removeClass('css_input_error');
    //        $('#' + pRow + '_FechaTasacion').addClass('css_calendario_error');
    //    }
    //    if ($('#' + pRow + '_DesTasador').val() == '0') {
    //        intError++;
    //        sb.append(fn_util_ValidateControl(objcmbTasador[0], 'Tasador', 1, ''));
    //    }
    var strFechaEncargo = $('#' + pRow + '_FechaEncargo').val();
    var strFechaTasacion = $('#' + pRow + '_FechaTasacion').val();

    if ((strFechaEncargo != '') && (strFechaTasacion != '') && (strFechaEncargo != undefined) && (strFechaTasacion != undefined)) {
        if ($('#' + pRow + '_FechaEncargo').val() != $('#' + pRow + '_FechaTasacion').val()) {
            if (!fn_util_ComparaFecha($('#' + pRow + '_FechaEncargo').val(), $('#' + pRow + '_FechaTasacion').val())) {
                intError++;
                intError++;
                sb.append('La Fecha de Tasación no debe ser menor a la fecha de asignación');
            }
        }
    }
    return intError;
}

function VerObservacionesLegal(strCodContrato, strCodtasacion, strCodTasador, strSecfinanciamiento, strValorBien, strvalorComercial, strfechaencargo, strfechatasacion) {
    var sTitulo = "Gestión del Bien";
    var sSubTitulo = "Tasación::Detalle";
    parent.fn_util_AbreModal(sSubTitulo, "GestionBien/Tasacion/frmTasacionRegistroDetalle.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodContrato=" + strCodContrato + "&hddCodtasacion=" + strCodtasacion + "&hddCodTasador=" + strCodTasador + "&hddSecfinanciamiento=" + strSecfinanciamiento + "&hhdValorBien=" + strValorBien + "&hhvalorComercial=" + strvalorComercial + "&hddfechaencargo=" + strfechaencargo + "&hddfechatasacion=" + strfechatasacion + "&Add=False", 550, 250, function() { });
}

function fn_ListadoContratoBienTasador() {
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"),
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"),
                         "pCodSolicitudcredito", $("#txtcontrato").val()
                        ];

    fn_util_AjaxSyncWM("frmTasacionRegistro.aspx/ListaContratoBienTasacion",
        arrParametros,
        function(jsondata) {
            jqGrid_lista_A.addJSONData(jsondata);
            totaliza();
            fn_doResize();
            parent.fn_unBlockUI();
        },
        function(request) {
            fn_util_alert(jQuery.parseJSON(request.responseText).Message);
            parent.fn_unBlockUI();

        }
    );
    //calculadiferenciaRealizacion();
    //calculadiferenciaComercial();
}


function totaliza() {
    var arrParametros = ["pCodSolicitudcredito", $("#txtcontrato").val()];

    fn_util_AjaxSyncWM("frmTasacionRegistro.aspx/calculatotales",
            arrParametros,
            function(request) {

                var resultadototales = fn_util_trim(request);
                var strtotales = resultadototales.split("|");

                //for (var i = 0; i < strtotales.length; i++) {
                $("#hddtotalRealizacion").val(strtotales[0]);
                $("#hddtotalcomercial").val(strtotales[1]);

                $('#divTotalRealizacion').html(fn_util_ValidaMonto($("#hddtotalRealizacion").val(), 2));
                $('#divTotalComercial').html(fn_util_ValidaMonto($("#hddtotalcomercial").val(), 2));


                var storealizacion = fn_util_ValidaDecimal($("#txtsaldocapitaldolares").val()) - fn_util_ValidaDecimal(strtotales[0]);
                $('#divDiferenciaRealizacion').html(fn_util_ValidaMonto(storealizacion, 2));


                var stocomercializacion = fn_util_ValidaDecimal($("#txtsaldocapitaldolares").val()) - fn_util_ValidaDecimal(strtotales[1]);
                $('#divDiferenciaComercial').html(fn_util_ValidaMonto(stocomercializacion, 2));

                //Habilita Enviar 
                if (storealizacion < 0 || stocomercializacion < 0) {
                    $("#Div2").hide();
                    $("#Div1").show();
                } else {
                    $("#Div1").hide();
                    $("#Div2").show();
                }

            },
            function(request) {
                parent.fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                parent.fn_unBlockUI();
            });

}





//function calculadiferenciaRealizacion() {
//       
//    var rows = jQuery("#jqGrid_lista_A").jqGrid('getRowData');
//    $("#hddDiferenciaRealizacion").val(0);
//    var stot = 0;
//    for (var i = 0; i < rows.length; i++) {
//        
//        var row = rows[i];
//        if (row.FechaTransferencia == "") {
//     
//            $("#hddDiferenciaRealizacion").val(fn_util_ValidaDecimal($("#hddDiferenciaRealizacion").val()) + fn_util_ValidaDecimal(row.valorEjecucion));
//        }else {
//            $("#hddDiferenciaRealizacion").val(0);
//        }
//    }
//    stot = fn_util_ValidaDecimal($("#txtsaldocapitaldolares").val()) - fn_util_ValidaDecimal($("#hddDiferenciaRealizacion").val());

//    $('#divDiferenciaRealizacion').html(fn_util_ValidaMonto(stot, 2));
//    
//}

//function calculadiferenciaComercial() {
//   
//    var rows = jQuery("#jqGrid_lista_A").jqGrid('getRowData');
//    $("#hddDiferenciaComercial").val(0);
//    var stotal2 = 0;
//    for (var i = 0; i < rows.length; i++) {
//        var row = rows[i];
//        if (row.FechaTransferencia =="") {
//            $("#hddDiferenciaComercial").val(fn_util_ValidaDecimal($("#hddDiferenciaComercial").val()) + fn_util_ValidaDecimal(row.valorComercial));
//    } else {
//            $("#hddDiferenciaComercial").val(0);
//    }
//    }
//    stotal2 = fn_util_ValidaDecimal($("#txtsaldocapitaldolares").val()) - fn_util_ValidaDecimal($("#hddDiferenciaComercial").val());
//     $('#divDiferenciaComercial').html(fn_util_ValidaMonto(stotal2,2));
//}
//

function fn_regresar() {
    window.location = "frmTasacionListado.aspx";
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos(pstrCodContrato, pstrCodBien, pstrCodRelacionado, pstrCodTipo, psthddVer) {
    parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + "&hddVer=" + psthddVer, 800, 350, function() { });
}

function fn_enviarcorreo() {
    parent.fn_blockUI();

    //String Validación
    var strError = new StringBuilderEx();

    var objtxtcontrato = $('input[id=txtcontrato]:text');
    var objtxtFechaActivacion = $('input[id=txtFechaActivacion]:text');

    //Valida
    strError.append(fn_util_ValidateControl(objtxtcontrato[0], 'un Contrato válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtFechaActivacion[0], 'un Fecha Activación válido', 1, ''));

    //Valida error existente
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
        fn_SetearCamposObligatorios();
    } else {

        var arrParametros = [
							"pstrContrato", $("#txtcontrato").val(),
							"pstrFechaActivacion", $("#txtFechaActivacion").val()
							];

        fn_util_AjaxWM("frmTasacionRegistro.aspx/EnviarCartaII",
                 arrParametros,
                 function(result) {
                     parent.fn_unBlockUI();
                     if (fn_util_trim(result) == "0") {
                     } else {
                         parent.fn_mdl_mensajeOk("El correo se envio correctamente.", function() { fn_RedireccionGrabar(); }, "ENVIAR CORRECTO");
                     }
                 },
                 function(resultado) {
                     parent.fn_unBlockUI();
                     var error = eval("(" + resultado.responseText + ")");
                     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN ENVIAR");
                 }
        );

    }
}

function fn_RedireccionGrabar() {
    //var ctrlBtn = window.parent.frames[0].document.getElementById('btnCargarMulta');
    //ctrlBtn.click();
    parent.fn_util_CierraModal();
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
