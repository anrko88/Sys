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


    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscarContratos(true);
        }
    });


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

    //On load Page (siempre al final)
    fn_onLoadPage();

});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	
//****************************************************************
function fn_inicializaCampos() {

    $("#txtcontrato").validText({ type: 'number', length: 8 });
    $("#txtcucliente").validText({ type: 'number', length: 10 });
    $('#txtrazonsocial').validText({ type: 'comment', length: 100 });
    $("#txtnumerodocumento").validText({ type: 'number', length: 11 });
    $("#txtPeriodo").validText({ type: 'number', length: 4 });
    $('#TxtEjecutivoBanca').validText({ type: 'comment', length: 100 });

    $("#dv_AsignarActivo").hide();
    $("#dv_AsignarDesactivo").show();

}

//************************************************************
// Función		:: fn_seteaCamposObligatorios
// Descripcion 	:: 	
// Log			::
//************************************************************
function fn_seteaCamposObligatorios() {
    fn_util_SeteaObligatorio($("#txtPeriodo"), "input");
    fn_util_SeteaObligatorio($("#txtFechaDesde"), "calendar");
    fn_util_SeteaObligatorio($("#txtFechaHasta"), "calendar");
}


//****************************************************************
// Función		:: 	fn_buscarContratos
// Descripción	::	Ejecuta la búsqueda de los contratos, cuando el usuario hace click en
//                  el botón 'Buscar'.
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_buscarContratos(bSearch) {
    bFirstClick = bSearch;
    intPaginaActual = 1;
    fn_ListadoAsignacionTasador();

}


//************************************************************
// Función		:: fn_configuraGrilla
// Descripcion 	:: 	
// Log			::
//************************************************************
var idsOfSelectedRows = [];
var idsOfSelectedRowsBanca = [];
function fn_configuraGrilla() {

    var updateIdsOfSelectedRows = function(id, isSelected) {
        $("#hddRowId").val(id);
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
        var index = $.inArray(id, idsOfSelectedRows);
        //var index2 = $.inArray(id, idsOfSelectedRowsBanca);

        if (!isSelected && index >= 0) {
            idsOfSelectedRows.splice(index, 1);
            idsOfSelectedRowsBanca.splice(index, 1);
        } else if (index < 0) {
            idsOfSelectedRows.push(rowData.CodSolicitudCredito);
            idsOfSelectedRowsBanca.push(rowData.DesEjecutivoBanca);
            $("#hddTasador").val(rowData.CodTasador);

        }
    };


    //jQuery("#jqGrid_lista_A").jqGrid('hideCol', "Placa");

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            fn_ListadoAsignacionTasador();
        },
        jsonReader: {                         // Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage",        // Número de página actual.
            total: "PageCount",          // Número total de páginas.
            records: "RecordCount",        // Total de registros a mostrar.
            repeatitems: false,
            id: "CodSolicitudCredito" // Índice de la columna con la clave primaria.
        },
        colNames: ['Nº Contrato', 'Razón Social o Nombre', 'Clasificación del Bien', 'Banca', 'Ejecutivo Banca', 'Ejecutivo Leasing', 'Fecha Activación', 'Fecha Asignación', 'Última Tasación', 'Estado del Contrato', 'Nombre Tasador', 'Estado Tasación del Contrato','', ''],
        colModel: [
        //{ name: 'check', index: 'check', align: 'center', width: 20, edittype: "checkbox", editoptions: { value: '1:0' }, formatter: Check },
		    {name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', sortable: true, width: 25, sorttype: "string", align: "center" },
		    { name: 'ClienteRazonSocial', index: 'ClienteRazonSocial', sortable: true, width: 50, sorttype: "string", align: "left" },
		    { name: 'ClasificacionBien', index: 'ClasificacionBien', sortable: true, sorttype: "string", width: 40, align: "left" },
		    { name: 'Banca', index: 'Banca', sortable: true, sorttype: "string", width: 25, align: "left" },
		    { name: 'DesEjecutivoBanca', index: 'DesEjecutivoBanca', sortable: true, sorttype: "string", width: 40, align: "left" },
		    { name: 'EjecutivoLeasing', index: 'EjecutivoLeasing', sortable: true, width: 10, sorttype: "string", align: "left", hidden: true },
            { name: 'FechaActivacion', index: 'FechaActivacion', width: 20, align: "center", sorttype: "string" },
            { name: 'FechaAsignacion', index: 'FechaAsignacion', width: 20, align: "center", sorttype: "string" },
		    { name: 'FechaUltimaTasacion', index: 'FechaUltimaTasacion', sortable: false, width: 20, align: "center", sorttype: "string" },
		    { name: 'EstadoContrato', index: 'EstadoContrato', hidden: false, width: 25 },
            { name: 'Tasador', index: 'Tasador', hidden: false, width: 30 },
            { name: 'EstadoTasacion', index: 'EstadoTasacion', align: "left", hidden: false, width: 29 },
            { name: '', index: '', width: 1 },
            { name: 'CodTasador', index: 'CodTasador', hidden: true, width: 25 }

	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,                            // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito', // Columna a ordenar por defecto.
        sortorder: 'desc',                // Criterio de ordenación por defecto.
        viewrecords: true,                  // Muestra la cantidad de registros.
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

        }
    });

    $("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 85);
}

//************************************************************
// Función		:: fn_asignar
// Descripcion 	:: 	
// Log			::
//************************************************************
function fn_asignar() {

    var strId = "";
    var sTitulo = "Gestion del Bien";
    var sSubTitulo = "Tasacion::Asignar";

    var strError = new StringBuilderEx();
    var objtxtPeriodo = $('input[id=txtPeriodo]:text');
    var objtxtFechaDesde = $('input[id=txtFechaDesde]:text');
    var objtxtFechaHasta = $('input[id=txtFechaHasta]:text');

    $('#txtFechaCobro').removeClass('css_input_error');
    $('#txtFechaCobro').addClass('css_calendario_error');

    strError.append(fn_util_ValidateControl(objtxtPeriodo[0], 'Periodo', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtFechaDesde[0], 'Fecha Desde', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtFechaHasta[0], 'Fecha Hasta', 1, ''));

    if (objtxtFechaDesde[0].value == '') {
        $('#txtFechaDesde').removeClass('css_input_error');
        $('#txtFechaDesde').addClass('css_calendario_error');
    } else {
        $('#txtFechaDesde').removeClass('css_calendario_error');
        $('#txtFechaDesde').addClass('css_calendario');
    }
    if (objtxtFechaHasta[0].value == '') {
        $('#txtFechaHasta').removeClass('css_input_error');
        $('#txtFechaHasta').addClass('css_calendario_error');
    } else {
        $('#txtFechaHasta').removeClass('css_calendario_error');
        $('#txtFechaHasta').addClass('css_calendario');
    }

    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });

        strError = null;
    } else {
        var intCambio = 0;
        if ($("#hddperiodo").val() != $("#txtPeriodo").val()) { intCambio = 1; }
        if ($("#hddfdesde").val() != $("#txtFechaDesde").val()) { intCambio = 1; }
        if ($("#hddfhasta").val() != $("#txtFechaHasta").val()) { intCambio = 1; }

        if (intCambio == 0) {
            if ($("#jqGrid_lista_A").getGridParam("reccount") > 0) {

                //Genera cadena para enviar los codigos
                //debugger; 
                $("#hddPagChecked").val("");
                for (var i = 0; i < idsOfSelectedRows.length; i++) {
                    if (i == 0) {
                        $("#hddPagChecked").val(idsOfSelectedRows[i]);
                    } else {
                        $("#hddPagChecked").val($("#hddPagChecked").val() + "|" + idsOfSelectedRows[i]);
                    }
                }

                if ($("#hddPagChecked").val() != "") {
                    if (validaEBanca() == "true") {
                        parent.fn_util_AbreModal(sSubTitulo, "GestionBien/Tasacion/frmTasacionAsignacion.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&sListaContratos=" + $("#hddPagChecked").val() + "&eb=" + $("#hddPrimerEB").val() + "&Per=" + $("#txtPeriodo").val() + "&fd=" + $("#txtFechaDesde").val() + "&fh=" + $("#txtFechaHasta").val() + "&ut=" + $("#txtPeriodo").val() + "&stasador=" + $("#hddTasador").val() + "&Add=false", 550, 250, function() {
                        });
                    } else {
                        parent.fn_mdl_mensajeIco("La Asignación masiva es para un solo ejecutivo de banca.", "util/images/warning.gif", "ERROR");
                        //fn_ListadoAsignacionTasador();
                    }
                } else {
                    parent.fn_mdl_mensajeIco("Debe seleccionar un registro en la grilla.", "util/images/warning.gif", "ERROR");
                }
            } else {
                parent.fn_mdl_mensajeIco("No hay registros en la lista", "util/images/warning.gif", "ERROR");
            }
        } else {
            parent.fn_mdl_mensajeIco("existen cambios en los filtros obligatorios por favor volver a realizar la busqueda ", "util/images/warning.gif", "ERROR");
        }
    }
}


//************************************************************
// Función		:: validaEBanca
// Descripcion 	:: 	
// Log			::
//************************************************************
function validaEBanca() {

    var retorna = "";
    //var lblResultado = $("#hddEbanca").val();
    //var ValidaEbanca = lblResultado.split("|");
    var primerregistro = "";

    for (var i = 0; i < idsOfSelectedRowsBanca.length; i++) {
        if (i == 0) {
            // primerregistro = ValidaEbanca[i];
            primerregistro = idsOfSelectedRowsBanca[i];

            $("#hddPrimerEB").val(primerregistro);
        }

        if (primerregistro == idsOfSelectedRowsBanca[i]) {
            retorna = "true";

        } else {
            retorna = "false";
            return retorna;
            break;
        }
    }

    return retorna;
}

//************************************************************
// Función		:: Check
// Descripcion 	:: 	
// Log			::
//************************************************************
function Check(cellvalue, options, rowObject) {
    var param = "'" + rowObject.CodSolicitudCredito + "','" + rowObject.DesEjecutivoBanca + "','" + rowObject.CodTasador + "'";
    var obj = "<input id='chkAsignar' name='chkAsignar' type='checkbox' runat='server' value='' onclick=\"javascript:fn_seleccionaRegistro(this," + param + ");\" />";
    return obj;
}

//************************************************************
// Función		:: fn_seleccionaRegistro
// Descripcion 	:: 	
// Log			::
//************************************************************
function fn_seleccionaRegistro(pCheck, pRegistro, pBanca, pTasador) {

    //    var pRegistros = $("#hddPagChecked").val();
    //    var pRegistrosNew = "";
    //    var pEbancas = $("#hddEbanca").val();
    //    var pRegistrosNewBanca = "";

    //    if (pCheck.checked == true) {

    //        if (pRegistros.length == 0) {
    //            pRegistros = pRegistro;
    //            pEbancas = pBanca;
    //        } else {
    //            pRegistros = pRegistros + "|" + pRegistro;
    //            pEbancas = pEbancas + "|" + pBanca;
    //        }
    //    } else {
    //        var lblCheckedResult = pRegistros.split("|");
    //        for (var i = 0; i < lblCheckedResult.length; i++) {
    //            if (pRegistro != lblCheckedResult[i]) {
    //                if (pRegistrosNew.length == 0) {
    //                    pRegistrosNew = lblCheckedResult[i];
    //                } else {
    //                    pRegistrosNew = pRegistrosNew + "|" + lblCheckedResult[i];
    //                }

    //            }
    //        }
    //        pRegistros = pRegistrosNew;
    //        
    //        var lblCheckedResultBanca = pEbancas.split("|");
    //        for (i = 0; i < lblCheckedResultBanca.length; i++) {
    //            if (pBanca != lblCheckedResultBanca[i]) {
    //                if (pRegistrosNewBanca.length == 0) {
    //                    pRegistrosNewBanca = lblCheckedResultBanca[i];
    //                } else {
    //                    pRegistrosNewBanca = pRegistrosNewBanca + "|" + lblCheckedResultBanca[i];
    //                }

    //            }
    //        }
    //        pEbancas = pRegistrosNewBanca;
    //    }
    //    $("#hddPagChecked").val(pRegistros);
    //    $("#hddEbanca").val(pEbancas);
    //    $("#hddTasador").val(pTasador);

}


//************************************************************
// Función		:: fn_ListadoAsignacionTasador
// Descripcion 	:: 	
// Log			::
//************************************************************
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
                          "pEstadoTasacion", $("#cbmEstadoTasacionContrato").val(),
                          "pClasificacionBien", $("#cmbClasificacionBien").val(),
                          "pBanca", $("#cmbbanca").val(),
                          "pEjecutivoBanca", $("#TxtEjecutivoBanca").val(),
                          "pPeriodo", $("#txtPeriodo").val(),
                          "pFechadesde", Fn_util_DateToString($("#txtFechaDesde").val()),
                          "pFechaHasta", Fn_util_DateToString($("#txtFechaHasta").val()),
                          "pEstadoTasacionContrato", $("#cmbEstadocontrato").val()
                        ];

    fn_util_AjaxSyncWM("frmTasacionAsignacionListado.aspx/ListaCondicionesAdicionales",
                         arrParametros,
                         function(jsondata) {

                             jqGrid_lista_A.addJSONData(jsondata);
                             idsOfSelectedRowsBanca = [];
                             idsOfSelectedRows = [];
                             fn_doResize();
                             parent.fn_unBlockUI();

                         },
                         function(request) {
                             fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                             parent.fn_unBlockUI();

                         }
                       );

    $("#hddPagChecked").val("");
    $("#hddTasador").val("");
}
//************************************************************
// Función		:: fn_buscar
// Descripcion 	:: 	
// Log			::
//************************************************************
function fn_buscar(bSearch) {
    bFirstClick = bSearch;
    intPaginaActual = 1;
    fn_ListadoAsignacionTasador();
    $("#dv_AsignarDesactivo").hide();
    $("#dv_AsignarActivo").show();
    $("#hddperiodo").val($("#txtPeriodo").val());
    $("#hddfdesde").val($("#txtFechaDesde").val());
    $("#hddfhasta").val($("#txtFechaHasta").val());
    $("#hddPagChecked").val("");
    //fn_util_SeteaCalendario($('input[id*=txtFecha]'));
}


//************************************************************
// Función		:: fn_asignarindividual
// Descripcion 	:: 	
// Log			::
//************************************************************
function fn_asignarindividual() {
    pRegistros = "";

    var pRegistros;
    for (var i = 0; i < idsOfSelectedRows.length; i++) {
        if (i == 0) {
            pRegistros = idsOfSelectedRows[i];
        } else {
            pRegistros = pRegistros + "|" + idsOfSelectedRows[i];
        }
    }

    var lblCheckedResult = pRegistros.split("|");

    if (lblCheckedResult != "") {
        if (lblCheckedResult.length == 1) {
            var sid = lblCheckedResult[0];
            window.location = "FrmTasacionIndividual.aspx?sid=" + sid + "";
        } else {
            parent.fn_mdl_mensajeIco("Debe seleccionar un contrato", "util/images/warning.gif", "ERROR");

        }
    } else {
        parent.fn_mdl_mensajeIco("Debe seleccionar un contrato", "util/images/warning.gif", "ERROR");
    }
}


//************************************************************
// Función		:: fn_limpiar
// Descripcion 	:: 	
// Log			::
//************************************************************
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

}

//************************************************************
// Función		:: fn_asignar2
// Descripcion 	:: 	
// Log			::
//************************************************************
function fn_asignar2() {


}

//****************************************************************
// Funcion		:: 	fn_ShowMensaje
// Descripción	::	
// Log			:: 	WCR - 09/01/2013
//****************************************************************
function fn_ShowMensaje(pMensaje) {
    fn_ListadoAsignacionTasador();
    fn_util_MuestraLogPage(pMensaje, "I");
}