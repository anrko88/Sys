//****************************************************************
// Variables Globales
//****************************************************************
var C_TX_NUEVO = "NUEVO";
var C_TX_EDITAR = "EDITAR";
var C_GESTIONBIEN_OPCIONCOMPRA = "009";
var intPaginaActual = 1;
var C_ESTADOCONTRATO_Cerrado = '14';
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	WCR - 04/01/2013
//****************************************************************
$(document).ready(function() {

    //Valida Tabs
    $("div#divTabs").tabs({
        show: function(event, ui) {
            fn_doResize();
        }
    });

    //Valida Campos
    fn_inicializaCampos();

    fn_CargarEnvios();
    fn_cargaGrillaBienes();
    fn_CargarCheckList();
    //    if ($('#hidCodEstadoContrato').val() == C_ESTADOCONTRATO_Cerrado) {
    //        $('#dv_guardar').hide();
    //        // $('#dv_Documentos').hide();               
    //    }
    //On load Page (siempre al final)
    fn_onLoadPage();
});

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	WCR - 20/11/2012
//****************************************************************
function fn_inicializaCampos() {

    $('#txtFechaTransferencia').attr('readonly', 'readonly');
    $('#txtFechaTransferenciaRRPP').attr('readonly', 'readonly');
    $('#ddlPago').attr('disabled', 'disabled');
    $('#txtFechaPagoOC').attr('readonly', 'readonly');
    $('#txtFechaTransferencia').removeClass();
    $('#txtFechaTransferencia').addClass('css_input_inactivo');
    $('#txtFechaTransferenciaRRPP').removeClass();
    $('#txtFechaTransferenciaRRPP').addClass('css_input_inactivo');
    $('#txtFechaPagoOC').removeClass();
    $('#txtFechaPagoOC').addClass('css_input_inactivo');

    $('#tbDatos').hide();
    $('#divDatos').hide();

    //    if ($('#hidCodOpcionCompra').val() != '0') { $('#dv_Documentos').show(); } else { $('#dv_Documentos').hide(); }
}

//************************************************************
// Función		:: 	fn_CheckAceptacion
// Descripcion 	::
// Log			:: WCR - 08/01/2013
//************************************************************
function fn_CheckAceptacion() {
    $('#txtFechaTransferencia').removeAttr('readonly');
    $('#txtFechaTransferenciaRRPP').removeAttr('readonly');
    $('#txtFechaPagoOC').removeAttr('readonly');
    $('#txtFechaTransferencia').removeClass();
    $('#txtFechaTransferencia').addClass('css_input');
    $('#txtFechaTransferenciaRRPP').removeClass();
    $('#txtFechaTransferenciaRRPP').addClass('css_input');
    $('#txtFechaPagoOC').removeClass();
    $('#txtFechaPagoOC').addClass('css_input');
    fn_util_SeteaCalendario($('input[id*=txtFechaTransferencia]'));
    fn_util_SeteaCalendario($('input[id*=txtFechaTransferenciaRRPP]'));
    fn_util_SeteaCalendario($('input[id*=txtFechaPagoOC]'));
    $('#ddlPago').removeAttr('disabled');
    $('#txtFechaAceptacion').val($('#hddFechaActual').val())
}

//************************************************************
// Función		:: 	fn_CheckNoAceptacion
// Descripcion 	::
// Log			:: WCR - 08/01/2013
//************************************************************
function fn_CheckNoAceptacion() {
    $("#txtFechaTransferencia").datepicker("destroy");
    $("#txtFechaTransferenciaRRPP").datepicker("destroy");
    $("#txtFechaPagoOC").datepicker("destroy");
    $('#txtFechaTransferencia').attr('readonly', 'readonly');
    $('#txtFechaTransferenciaRRPP').attr('readonly', 'readonly');
    $('#ddlPago').attr('disabled', 'disabled');
    $('#txtFechaPagoOC').attr('readonly', 'readonly');
    $('#txtFechaTransferencia').removeClass();
    $('#txtFechaTransferencia').addClass('css_input_inactivo');
    $('#txtFechaTransferenciaRRPP').removeClass();
    $('#txtFechaTransferenciaRRPP').addClass('css_input_inactivo');
    $('#txtFechaPagoOC').removeClass();
    $('#txtFechaPagoOC').addClass('css_input_inactivo');
    $("#txtFechaTransferencia").val("");
    $("#txtFechaTransferenciaRRPP").val("");
    $("#txtFechaPagoOC").val("");
    $('#txtFechaAceptacion').val("");
    $("#ddlPago").val("0");
}

//****************************************************************
// Funcion		:: 	fn_CargarEnvios
// Descripción	::	
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_CargarEnvios() {
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            fn_buscarEnvio();
        },
        //datatype: "local",
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "TipoEnvio"
        },
        colNames: ['Tipo de Envio', 'Fecha de Envio','Doc.'],
        colModel: [
            { name: 'TipoEnvio', index: 'TipoEnvio', width: 80, sorttype: "string", sortable: false, align: "left" },
            { name: 'FechaEnvio', index: 'FechaEnvio', width: 50, sorttype: "string", sortable: false, align: "center" },            
            { name: 'Archivo', index: 'Archivo', width: 20, align: "Center", sortable: false, formatter: fn_icoDownload }
	    ],
        height: '100%',
        width: glb_intWidthPantalla - 400,
        //pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'TipoEnvio',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        altclass: 'gridAltClass',
        gridComplete: function(id) {
            fn_doResize();
        }
    });
    //$('tr.ui-jqgrid-labels').hide();
    
    //Abrir Archivo
    function fn_icoDownload(cellvalue, options, rowObject) {
        var strNombreArchivo = rowObject.Archivo.split('\\').pop();
        strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
        if (fn_util_trim(rowObject.Archivo) != "") {
            return "<img src='../../Util/images/ico_download.gif' alt='" + strNombreArchivo + "' title='" + strNombreArchivo + "' onclick=\"javascript:fn_OCAbreArchivo('" + encodeURIComponent(rowObject.Archivo) + "');\" style='cursor:pointer;'/>";
        } else {
            return ".";
        }
    };
    
}

//****************************************************************
// Funcion		:: 	fn_abreArchivo 
// Descripción	::	Abre Archivo
// Log			:: 	JRC - 20/02/2013
//****************************************************************
function fn_OCAbreArchivo(pstrRuta) {
	//alert(pstrRuta);
    window.open("../../frmDownload.aspx?nombreArchivo=" + pstrRuta);
    return false;
}


//****************************************************************
// Funcion		:: 	fn_guardar
// Descripción	::	
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_guardar() {
    var sbError = new StringBuilderEx();

    fn_ValidarRegistro(sbError);
    if (sbError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(sbError.toString(), function() { });
        sbError = null;
    }
}


//****************************************************************
// Función		:: 	fn_RedireccionGrabar
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
//                  Luego lo redirecciona al formulario de resgistro de opcion de compra.
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_RedireccionGrabar() {
    parent.fn_util_CierraModal();
}



//****************************************************************
// Función		:: 	fn_ValidarRegistro
// Descripción	::	
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_ValidarRegistro(sbError) {

    return sbError.toString();
}


//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	WCR - 04/01/2013
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos() {
    var pstrCodContrato = $("#hidNumeroContrato").val();
    var pstrCodBien = "0";
    var pstrCodRelacionado = $("#hidCodOpcionCompra").val();
    var pstrCodTipo = C_GESTIONBIEN_OPCIONCOMPRA;
    var strVer = '0';
    //    strEstado = $('#hidEstadoCobro').val();
    //    if (strEstado != 'C') { strVer = 1 }
    parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=0" + "&hddCodTipo=" + pstrCodTipo + '&hddVer=' + strVer, 800, 350, function() { });
}

//****************************************************************
// Funcion		:: 	fn_Cancelar
// Descripción	::	cancelar
// Log			:: 	WCR - 21/11/2012
//****************************************************************
function fn_Cancelar() {
    parent.fn_mdl_confirma('¿Está seguro de Volver?',
		function() {
		    parent.fn_blockUI();
		    fn_util_globalRedirect('/GestionBien/OpcionCompra/frmOpcionCompraListado.aspx');
		},
         "util/images/question.gif",

		function() {
		},
		'Gestión del Bien :: Opción de Compra'
	);
}

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_cargaGrillaBienes() {
    $("#jqGrid_lista_B").jqGrid({
        datatype: function() {
            //intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_B", "page");
            fn_buscarBien();
        },
        //        datatype: "local",
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "CodSolicitudCredito,SecFinanciamiento"
        },
        colNames: ['Descripción del Bien', 'Tipo de Bien', 'Ubicación', 'Placa Actual', 'Moneda', 'Valor del Bien', 'Editar', '', '', '', '', ''],
        colModel: [
            { name: 'DescripcionBien', index: 'DescripcionBien', width: 80, sorttype: "string", align: "left" },
            { name: 'TipoBien', index: 'TipoBien', width: 80, sorttype: "string", align: "left" },
            { name: 'Ubicacion', index: 'Ubicacion', width: 80, align: "left", sorttype: "string" },
		    { name: 'PlacaAnterior', index: 'PlacaAnterior', width: 25, sorttype: "string", align: "center" },
		    { name: 'MonedaBien', index: 'MonedaBien', width: 30, align: "center", sorttype: "string" },
		    { name: 'ValorBien', index: 'ValorBien', width: 25, align: "right", formatter: Fn_util_ReturnValidDecimalx },
		    { name: 'Editar', index: 'Editar', align: "center", sortable: false, formatter: fn_Editar, width: 20 },
		    { name: 'CantidadDemanda', index: 'CantidadDemanda', hidden: true },
		    { name: 'CodSolicitudCredito', index: 'CantidadDemanda', hidden: true },
		    { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
		    { name: 'CantidadDemanda', index: 'CantidadDemanda', hidden: true },
		    { name: 'FlagAprobacion', index: 'FlagAprobacion', hidden: true }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_B',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'SecFinanciamiento',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hidSecFinanciamiento").val(rowData.SecFinanciamiento);
            //$("#hidCodigoSolicitudCredito").val(rowData.CodSolicitudCredito);
        },
        ondblClickRow: function() {
            //           parent.fn_blockUI();
            //            fn_util_redirect('frmMantenimientoBienContrato.aspx?co=1&csc=' + $("#hidCodigoSolicitudCredito").val());
        }
    });

    jQuery("#jqGrid_lista_B").jqGrid('navGrid', '#jqGrid_pager_B', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_B").hide();
    $("#jqGrid_lista_B").setGridWidth($(window).width() - 125);

    function fn_Editar(cellvalue, options, rowObject) {
        var obj = '';

        if ($('#hidCodEstadoContrato').val() == C_ESTADOCONTRATO_Cerrado) {
            obj = "<img src='../../Util/images/ico_acc_ver.gif' alt='" + cellvalue + "' title='Ver' width='20px' onclick=\"javascript:fn_EditarBien(" + rowObject.SecFinanciamiento + "," + rowObject.CantidadDemanda + ",'" + rowObject.FlagAprobacion + "');\" style='cursor: pointer;cursor: hand;' />";
        }
        else {
            obj = "<img src='../../Util/images/ico_acc_editar.gif' alt='" + cellvalue + "' title='Ver' width='20px' onclick=\"javascript:fn_EditarBien(" + rowObject.SecFinanciamiento + "," + rowObject.CantidadDemanda + ",'" + rowObject.FlagAprobacion + "');\" style='cursor: pointer;cursor: hand;' />";
        }
        return obj;
    };

    if ($('#hidCodEstadoContrato').val() == C_ESTADOCONTRATO_Cerrado) {
        jQuery("#jqGrid_lista_B").jqGrid('setLabel', 'Editar', 'Ver');
    }

}


//****************************************************************
// Funcion		:: 	fn_buscarBien
// Descripción	::	
// Log			:: 	WCR - 08/01/2013
//****************************************************************
function fn_buscarBien() {

    var strNumeroContrato = $('#hidNumeroContrato').val() == undefined ? "" : $('#hidNumeroContrato').val();

    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_B", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_B", "page"),    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_B", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_B", "sortorder"), // Criterio de ordenación.
                             "pNumeroContraro", strNumeroContrato
                            ];

    fn_util_AjaxWM("frmOpcionCompraRegistro.aspx/BuscarBien",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_B.addJSONData(jsondata);
                        parent.fn_unBlockUI();
                        fn_doResize();
                    },
                    function(request) {
                        parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "BUSCAR BIEN");
                        parent.fn_unBlockUI();
                        fn_doResize();
                    }
                   );


}


//****************************************************************
// Funcion		:: 	fn_ListaGrillaDocumento
// Descripción	::	
// Log			:: 	WCR - 29/01/2013
//****************************************************************
function fn_ListaGrillaDocumento() {

    var strNumeroContrato = $('#hidNumeroContrato').val() == undefined ? "" : $('#hidNumeroContrato').val();

    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_B", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_B", "page"),    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_B", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_B", "sortorder"), // Criterio de ordenación.
                             "pNumeroContraro", strNumeroContrato
                            ];

    fn_util_AjaxWM("frmOpcionCompraRegistro.aspx/ListadoDocumento",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_C.addJSONData(jsondata);
                        parent.fn_unBlockUI();
                        fn_doResize();
                    },
                    function(request) {
                        parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "BUSCAR DOCUMENTOS");
                        parent.fn_unBlockUI();
                        fn_doResize();
                    }
                   );


}

//****************************************************************
// Funcion		:: 	fn_buscarEnvio
// Descripción	::	
// Log			:: 	WCR - 11/01/2013
//****************************************************************
function fn_buscarEnvio() {

    var strNumeroContrato = $('#hidNumeroContrato').val() == undefined ? "" : $('#hidNumeroContrato').val();

    var arrParametros = ["pNumeroContraro", strNumeroContrato];

    fn_util_AjaxWM("frmOpcionCompraRegistro.aspx/BuscarOpcionCompraEnvio",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_A.addJSONData(jsondata);
                        //parent.fn_unBlockUI();
                        fn_doResize();
                    },
                    function(request) {
                        parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "BUSCAR ENVIOS");
                        //parent.fn_unBlockUI();
                        fn_doResize();
                    }
                   );


}


//****************************************************************
// Funcion		:: 	fn_EditarBien
// Descripción	::	Carga Grilla
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_EditarBien(pCodBien, pCantidadDemanda, pFlagAprobacion) {
    $('#tbDatos').show();
    $('#divDatos').show();
    fn_ObtenerBien(pCodBien, pCantidadDemanda, pFlagAprobacion);

    if ($('#hidCodEstadoContrato').val() == C_ESTADOCONTRATO_Cerrado) { $('#dv_Modificar').hide(); }
    else { $('#dv_Modificar').show(); }
    fn_doResize();
}

//****************************************************************
// Funcion		:: 	fn_LimpiarBien
// Descripción	::	Carga Grilla
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_LimpiarBien() {
    $('#hidSecFinanciamiento').val('0');
    $('#hidTotalBien').val('0');
    $("#txtTipoBien").val("");
    $("#txtDescripcion").val("");
    $("#txtUbicacion").val("");
    $("#txtDeparatamento").val("");
    $("#txtProvincia").val("");
    $("#txtDistrito").val("");
    $("#txtPlacaActual").val("");
    $("#txtNroMotor").val("");
    $("#txtMarca").val("");
    $("#txtFechaTransferencia").val("");
    $("#txtFechaTransferenciaRRPP").val("");
    $("#txtFechaPagoOC").val("");
    $("#ddlPago").val("0");
    $("#chkAceptacion").attr("checked", false);
    $('#tbDatos').hide();
    $('#divDatos').hide();
    fn_doResize();
}

function fn_CargarCheckList() {
    /*********************************************************
    GRILLA TAB DOCUMENTOS ADICIONALES 
    **********************************************************/
    $("#jqGrid_lista_C").jqGrid({
        datatype: function() {
            fn_ListaGrillaDocumento();
        },
        //datatype: 'local',
        jsonReader:                              //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            {
            root: "Items",
            page: "CurrentPage",            // Número de página actual.
            total: "PageCount",             // Número total de páginas.
            records: "RecordCount",         // Total de registros a mostrar.
            repeatitems: false,
            id: "CodCheckList"   // Índice de la columna con la clave primaria.
        },

        colNames: ['ID', '', '', 'Documentos', 'Fecha', 'Ver Adjunto', 'Adjuntar', 'Ver Observaciones'],
        colModel: [
                { name: 'CodOpcionCompraDocumento', index: 'CodOpcionCompraDocumento', hidden: true, width: 0, align: "left", sorttype: "string" },
                { name: 'Ajunto', index: 'Ajunto', hidden: true, width: 0, align: "left", sorttype: "string" },
                { name: 'CodCheckList', index: 'CodCheckList', hidden: true, width: 0, align: "left", sorttype: "string" },
                { name: 'DesCheckList', index: 'DesCheckList', width: 150, align: "left", sorttype: "string" },
                { name: 'CheckFecha', index: 'CheckFecha', width: 30, align: "left", sorttype: "string", sortable: false, formatter: fn_CheckFecha },
                { name: 'VerAdjunto', index: 'VerAdjunto', width: 20, align: "center", sorttype: "string", formatter: fn_VerAdjunto },
                { name: 'SubirArchivo', index: 'SubirArchivo', width: 20, align: "center", sortable: false, formatter: fn_SubirArchivo },
                { name: 'Lupa', index: 'Lupa', width: 20, align: "center", sortable: false, formatter: fn_Lupa }
             ],
        height: '100%',
        //pager: '#jqGrid_pager_C',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,                             // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CodCheckList',    // Columna a ordenar por defecto.
        sortorder: 'asc',                       // Criterio de ordenación por defecto.
        viewrecords: true,                      // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            //            var rowData = $("#jqGrid_lista_E").jqGrid('getRowData', id);
            //            $("#hddCodigo").val(rowData.CodigoContratoDocumento);
        }
    });
    jQuery("#jqGrid_lista_C").jqGrid('navGrid', '#jqGrid_pager_C', { edit: false, add: false, del: false });

    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_C").setGridWidth($(window).width() - 125);
    $("#search_jqGrid_lista_C").hide();

    function fn_SubirArchivo(cellvalue, options, rowObject) {        
        return "<img src='../../Util/images/ico_acc_adjuntarMini.gif' alt='" + cellvalue + "' title='Subir Archivo' width='20px' onclick=\"javascript:AdjuntarArchivoDocumento(" + rowObject.CodOpcionCompraDocumento + ",'" + rowObject.CodCheckList + "');\" style='cursor: pointer;cursor: hand;' />";
    };

    function fn_Lupa(cellvalue, options, rowObject) {        
        if (rowObject.CantidadObservacion == 0) {
            return "<img src='../../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick=\"javascript:VerObservaciones(" + rowObject.CodOpcionCompraDocumento + ",'" + rowObject.CodCheckList + "');\" style='cursor: pointer;cursor: hand;' />";
        } else {
        return "<img src='../../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick=\"javascript:VerObservaciones(" + rowObject.CodOpcionCompraDocumento + ",'" + rowObject.CodCheckList + "');\" style='cursor: pointer;cursor: hand;' />";
        }
    };

    function fn_VerAdjunto(cellvalue, options, rowObject) {
        var strAdjunto = rowObject.Adjunto;
        var strNombreArchivo = strAdjunto.split('\\').pop();
        strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
        if (rowObject.Adjunto != "") {
            return "<img src='../../Util/images/ico_download.gif' alt='Descargar/Mostrar Archivo' title='" + strNombreArchivo + "' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.Adjunto) + "');\" style='cursor:pointer;'/>";
        } else {
            return ".";
        }
    };

    function fn_CheckFecha(cellvalue, options, rowObject) {
        var obj = '';
        var strChecked = '';

        if (rowObject.FlagCheckList == '1') { strChecked = "checked='checked'" }
        obj = "<table border='0' cellpadding='0' cellspacing='0'><tr><td style='width:20px'><input type='checkbox' " + strChecked + " id='chkFecha" + rowObject.CodCheckList + "' onclick=\"javascript:fn_SetearFecha(this," + rowObject.CodOpcionCompraDocumento + ",'" + rowObject.CodCheckList + "');\" /></td><td><input type='text' id='txtFecha" + rowObject.CodCheckList + "' class='css_input_inactivo' readonly='true' size='10' value='" + rowObject.FechaCheckList + "' /></td></tr></table>";
        return obj;
    };

}
//****************************************************************
// Funcion		:: 	AdjuntarArchivoDocumento
// Descripción	::	
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function AdjuntarArchivoDocumento(pCodOpcionCompraDocumento, pCodCheckList) {

    var strNumeroContrato = $("#hidNumeroContrato").val();
    var sTitulo = "Gestión del Bien";
    var sSubTitulo = "Opción de Compra:: Checklist Documentos ";
    parent.fn_util_AbreModal(sSubTitulo, "GestionBien/OpcionCompra/frmSubirArchivoOC.aspx?hddCodOpcComDoc=" + pCodOpcionCompraDocumento + "&hddCodContrato=" + strNumeroContrato + "&hddCodCheck=" + pCodCheckList, 550, 150, function() { });

}

//****************************************************************
// Funcion		:: 	VerObservaciones
// Descripción	::	
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function VerObservaciones(pCodOpcionCompraDocumento, pCodCheckList) {
    var strNumeroContrato = $("#hidNumeroContrato").val();
    var sTitulo = "Gestión del Bien";
    var sSubTitulo = "Opción de Compra:: Check List Documentos  ";
    parent.fn_util_AbreModal(sSubTitulo, "GestionBien/OpcionCompra/frmObservacionOC.aspx?hddCodOpcComDoc=" + pCodOpcionCompraDocumento + "&hddCodContrato=" + strNumeroContrato + "&hddCodCheck=" + pCodCheckList, 700, 300, function() { });
}

//****************************************************************
// Funcion		:: 	fn_abreArchivo 
// Descripción	::	Abre Archivo
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_abreArchivo(pstrRuta) {
    //alert(pstrRuta);
    window.open("../../frmDownload.aspx?nombreArchivo=" + pstrRuta);
    return false;
}

//****************************************************************
// Funcion		:: 	fn_SetearFecha 
// Descripción	::	
// Log			:: 	WCR - 04/01/2013
//****************************************************************
function fn_SetearFecha(pControl, pCodOpcionCompraDocumento, pCodCheckList) {
    var strFlag = '0';
    if (pControl.checked) { strFlag = '1'; }

    var strNumeroContrato = $('#hidNumeroContrato').val() == undefined ? "" : $('#hidNumeroContrato').val();

    var arrParametros = ["pstrNumeroContrato", strNumeroContrato,
                         "pstrCodOpcionCompraDocumento", pCodOpcionCompraDocumento,
                         "pstrCodCheckList", pCodCheckList,
                         "pstrFlagCheckList", strFlag
                        ];

    fn_util_AjaxWM("frmOpcionCompraRegistro.aspx/GrabaDocumento",
                    arrParametros,
                    function(result) {
                        parent.fn_unBlockUI();
                        if (fn_util_trim(result) == "0") {
                            parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "GRABAR DOCUMENTO");
                        } else {
                            //fn_util_MuestraLogPage('Los datos del bien se actualizaron correctamente', "I");
                            //fn_LimpiarBien();
                            fn_ListaGrillaDocumento();
                            parent.fn_unBlockUI();
                        }
                    },
                     function(resultado) {
                         parent.fn_unBlockUI();
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "GRABAR DOCUMENTO");
                     }

                );


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


//****************************************************************
// Funcion		:: 	fn_ObtenerBien
// Descripción	::	
// Log			:: 	WCR - 08/01/2013
//****************************************************************
function fn_ObtenerBien(pCodBien, pCantidadDemanda, pFlagAprobacion) {
    try {
        parent.fn_blockUI();

        var strNroContrato = $('#hidNumeroContrato').val() == undefined ? "" : $('#hidNumeroContrato').val();

        var arrParametros = ["pstrNroContrato", strNroContrato,
                             "pstrSecFinanciamiento", pCodBien
                             ];


        fn_util_AjaxWM("frmOpcionCompraRegistro.aspx/ObtenerBien",
                       arrParametros,
                       function(objResultado) {
                           fn_SetearDatosBien(objResultado, pCantidadDemanda, pFlagAprobacion);
                           parent.fn_unBlockUI();
                       },
                       function(request) {
                           var error = eval("(" + request.responseText + ")");
                           parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "OBTENER BIEN");
                           parent.fn_unBlockUI();
                       });



    } catch (ex) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }
}

//****************************************************************
// Funcion		:: 	fn_SetearDatosBien
// Descripción	::	
// Log			:: 	WCR - 08/01/2013
//****************************************************************
function fn_SetearDatosBien(pResultado, pCantidadDemanda, pFlagAprobacion) {
    var objEOpcionCompra = pResultado;
    $('#hidCodOpcionCompra').val(objEOpcionCompra.CodOpcionCompra);
    $('#hidSecFinanciamiento').val(objEOpcionCompra.SecFinanciamiento);
    $('#txtTipoBien').val(objEOpcionCompra.TipoBien);
    $('#txtDescripcion').val(fn_util_trim(objEOpcionCompra.DescripcionBien));
    $('#txtUbicacion').val(fn_util_trim(objEOpcionCompra.Ubicacion));
    $('#txtDepartamento').val(fn_util_trim(objEOpcionCompra.DepartamentoNombre));
    $('#txtProvincia').val(fn_util_trim(objEOpcionCompra.ProvinciaNombre));
    $('#txtDistrito').val(objEOpcionCompra.DistritoNombre);
    $('#txtPlacaActual').val(objEOpcionCompra.PlacaActual);
    $('#txtNroMotor').val(objEOpcionCompra.NroMotor);
    $('#txtMarca').val(objEOpcionCompra.Marca);
    $('#hidTotalBien').val(objEOpcionCompra.Item);

    if (objEOpcionCompra.FlagAceptacionCliente == '1') {

        $('input[name=chkAceptacion]').attr('checked', true);
        if ($('#hidCodEstadoContrato').val() == C_ESTADOCONTRATO_Cerrado) { $('input[name=chkAceptacion]').attr('disabled', true); }
        else { fn_CheckAceptacion(); }
    }
    else {
        $('input[name=chkAceptacion]').attr('checked', false);
        if ($('#hidCodEstadoContrato').val() == C_ESTADOCONTRATO_Cerrado) { $('input[name=chkAceptacion]').attr('disabled', true); }
        // fn_CheckNoAceptacion();
    }
    //$("#chkAceptacion").attr("onchange", "");
    $("#chkAceptacion").unbind("change");
    $('#chkAceptacion').change(function() {
        if ($(this).is(':checked') == true) {
            if ((parseFloat(pCantidadDemanda) > 0) && (pFlagAprobacion == '0')) {
                $('#chkAceptacion').removeAttr('checked');
                parent.fn_mdl_mensajeIco("El bien seleccionado presenta demanda(s) registrada(s), es necesario la aprobación de un supervisor.", "util/images/warning.gif", "Opción de Compra");
            }
            else { fn_CheckAceptacion(); }

        }
        else { fn_CheckNoAceptacion(); }
    });

    $('#txtFechaAceptacion').val(objEOpcionCompra.StringFechaAceptacionCliente);
    $('#txtFechaTransferencia').val(fn_util_trim(objEOpcionCompra.StringFechaTransferencia));
    $('#txtFechaTransferenciaRRPP').val(objEOpcionCompra.StringFechaTransferenciaRRPP);
    $('#ddlPago').val(objEOpcionCompra.CodTipoPagoOC);
    $('#txtFechaPagoOC').val(objEOpcionCompra.StringFechaPagoOC);

    //    if ($('#hidCodOpcionCompra').val() != '0') { $('#dv_Documentos').show(); } else { $('#dv_Documentos').hide(); }
}

//****************************************************************
// Funcion		:: 	fn_ModificarBien
// Descripción	::	
// Log			:: 	WCR - 09/01/2013
//****************************************************************
function fn_ModificarBien() {
    parent.fn_blockUI();
    var strNumeroContrato = $('#hidNumeroContrato').val() == undefined ? "" : $('#hidNumeroContrato').val();
    var strSecFinanciamiento = $('#hidSecFinanciamiento').val() == undefined ? "" : $('#hidSecFinanciamiento').val();
    var strTotalBienes = $('#hidTotalBien').val() == undefined ? "0" : $('#hidTotalBien').val();
    var strCodOpcionCompra = $('#hidCodOpcionCompra').val() == undefined ? "0" : $('#hidCodOpcionCompra').val();

    var strFlagAceptacion = '0';

    if ($('#chkAceptacion').is(':checked')) { strFlagAceptacion = '1'; }

    var strFechaAceptacion = fn_FormatoFecha($('#txtFechaAceptacion').val());
    var strFechaTransferencia = fn_FormatoFecha($('#txtFechaTransferencia').val());
    var strFechaTransferenciaRRPP = fn_FormatoFecha($('#txtFechaTransferenciaRRPP').val());
    var strFechaPagoOC = fn_FormatoFecha($('#txtFechaPagoOC').val());

    var strTipoPago = $("#ddlPago option:selected").val();

    var arrParametros = ["pstrNumeroContrato", strNumeroContrato,
                         "pstrCodOpcionCompra", strCodOpcionCompra,
                         "pstrSecFinanciamiento", strSecFinanciamiento,
                         "pstrFlagAceptacion", strFlagAceptacion,
                         "pstrFechaAceptacion", strFechaAceptacion,
                         "pstrFechaTransferencia", strFechaTransferencia,
	 		             "pstrFechaTransferenciaRRPP", strFechaTransferenciaRRPP,
	 		             "pstrFechaPagoOC", strFechaPagoOC,
	 		             "pstrTipoPago", strTipoPago,
	 		             "pstrTotalBienes", strTotalBienes
                        ];

    fn_util_AjaxWM("frmOpcionCompraRegistro.aspx/GrabaBien",
                    arrParametros,
                    function(result) {
                        parent.fn_unBlockUI();
                        if (fn_util_trim(result) == "0") {
                            parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "GRABAR BIEN");
                        } else {
                            fn_util_MuestraLogPage('Los datos del bien se actualizaron correctamente', "I");
                            fn_LimpiarBien();
                            parent.fn_unBlockUI();
                        }
                    },
                     function(resultado) {
                         parent.fn_unBlockUI();
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "GRABAR BIEN");
                     }

                );

}

//****************************************************************
// Funcion		:: 	fn_FormatoFecha
// Descripción	::	
// Log			:: 	WCR - 09/01/2013
//****************************************************************
function fn_FormatoFecha(pFecha) {
    var strFecha = '1900-01-01';
    if (fn_util_trim(pFecha) != '') {
        var arrFecha = pFecha.split('/');
        strFecha = arrFecha[2] + '-' + arrFecha[1] + '-' + arrFecha[0];
    }
    return strFecha;
}