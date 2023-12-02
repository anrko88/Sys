var V_ESTADOCOTIZACION_INGRESADO = "001"
var V_ESTADOCOTIZACION_PENDCARTA = "002"
var V_ESTADOCOTIZACION_EVASUPERV = "003"
var V_ESTADOCOTIZACION_EVACLIE = "004"
var V_ESTADOCOTIZACION_APROBADO = "005"
var V_ESTADOCOTIZACION_DESAPROBADO = "006"
var blnPrimeraBusqueda;
var intPaginaActual = 1;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    //Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    //Carga Grilla
    fn_cargaGrilla();

    //Valida Campos
    fn_inicializaCampos();

    //Busca con Enter
    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscarCotizacion(true);
        }
    });

    //On load Page (siempre al final)
    fn_onLoadPage();
    //IBK - RPH
    var sNroCotizacion = $('#hddNroCotizacion').val();
    if (sNroCotizacion != '') {
        $('#txtNroCotizacion').val(sNroCotizacion);
        fn_buscarCotizacion(true);
    }
    //FIN
});


//****************************************************************
// Funcion		:: 	fn_buscarCotizacion
// Descripción	::	Busca listado de cotizacion por parametros
// Log			:: 	JRM - 14/05/2012
//****************************************************************
function fn_buscarCotizacion(pblnBusqueda) {
    blnPrimeraBusqueda = pblnBusqueda;
	intPaginaActual = 1;
    fn_realizaBusquedaCotizacion();
}
function fn_realizaBusquedaCotizacion() {
    if (!blnPrimeraBusqueda) {
        return;
    }

    try {
        parent.fn_blockUI();

        var txtNroCotizacion = $('#txtNroCotizacion').val() == undefined ? "" : $('#txtNroCotizacion').val();
        var txtCuCliente = $('#txtCuCliente').val() == undefined ? "" : $('#txtCuCliente').val();
        var txtRazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
        var cmbEjecutivo = $('#cmbEjecutivo').val() == undefined ? "" : $('#cmbEjecutivo').val();
        var txtFechaIngresoIni = $('#txtFechaIngresoIni').val() == undefined ? "" : $('#txtFechaIngresoIni').val();
        var txtFechaIngresoFin = $('#txtFechaIngresoFin').val() == undefined ? "" : $('#txtFechaIngresoFin').val();
        var cmbClasificacionbien = $('#cmbClasificacionbien').val() == undefined ? "" : $('#cmbClasificacionbien').val();
        var cmbEstado = $('#cmbEstado').val() == undefined ? "" : $('#cmbEstado').val();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
                             "pstrNroCotizacion", txtNroCotizacion,
                             "pstrCuCliente", txtCuCliente,
                             "pstrRazonSocial", txtRazonSocial,
                             "pstrEjecutivo", cmbEjecutivo,
                             "pstrFechaIngresoIni", txtFechaIngresoIni,
                             "pstrFechaIngresoFin", txtFechaIngresoFin,
                             "pstrClasificacionbien", cmbClasificacionbien,
                             "pstrEstado", cmbEstado
                            ];

        fn_util_AjaxWM("frmCotizacionListado.aspx/ListaCotizacion",
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
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla Listado de Cotizaciones
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() {

     $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");	
            fn_realizaBusquedaCotizacion();
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
        colNames: ['Nº Cotización', 'CU Cliente', 'Raz. Social o Nombre', 'Clasificación del  Bien', 'Moneda', 'T.E.A.', 'Precio Venta', 'Nº Cuotas', 'Tipo Seguro', 'Estado', 'Fecha', 'Carta', 'Aprobar', 'Rechazar'],
        colModel: [
                { name: 'Codigocotizacion', index: 'Codigocotizacion', sortable: true, sorttype: "int", width: 40, align: "center", defaultValue: "" },
                { name: 'CodUnico', index: 'CodUnico', sortable: true, sorttype: "string", width: 40, align: "center", defaultValue: "" },
                { name: 'NombreCliente', index: 'NombreCliente', sortable: true, sorttype: "string", align: "left", defaultValue: "" },
                { name: 'NombreClasificacionbien', index: 'NombreClasificacionbien', sortable: true, width: 70, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'NombreMoneda', index: 'NombreMoneda', sortable: true, sorttype: "string", width: 40, align: "center", defaultValue: "" },
                { name: 'Teaporc', index: 'Teaporc', sortable: true, sorttype: "float", width: 25, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'Precioventa', index: 'Precioventa', sortable: true, sorttype: "float", width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'Numerocuotas', index: 'Numerocuotas', sortable: true, sorttype: "int", width: 25, align: "center", defaultValue: "" },
                { name: 'NombreBientiposeguro', index: 'NombreBientiposeguro', sortable: true, sorttype: "string", width: 35, align: "center", defaultValue: "" },
                { name: 'Nombreestadocotizacion', index: 'Nombreestadocotizacion', sortable: true, sorttype: "string", width: 35, align: "center", defaultValue: "" },
                { name: 'AudFechaRegistro', index: 'AudFechaRegistro', sortable: true, width: 30, align: "center", formatter: fn_util_ValidaStringFecha },
                { name: 'cCarta', index: 'cCarta', width: 18, align: "center", sortable: false, formatter: generacarta },
                { name: 'cAprobar', index: 'cAprobar', width: 25, align: "center", sortable: false, formatter: aprobar },
                { name: 'cRechazar', index: 'cRechazar', width: 40, align: "center", sortable: false, formatter: rechazar }
        ],
        width: glb_intWidthPantalla-70,
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'Codigocotizacion',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodigoCotizacion").val(rowData.Codigocotizacion);
        },

        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            window.location = "frmCotizacionRegistro.aspx?hddCodigoCotizacion=" + rowData.Codigocotizacion;
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();


    //**************************************************
    // Rechazar
    //**************************************************
    function rechazar(cellvalue, options, rowObject) {
        if (V_ESTADOCOTIZACION_EVACLIE == fn_util_trim(rowObject.Codigoestadocotizacion)) {
            return "<img src='../Util/images/ico_rechazar_act.gif' alt='" + cellvalue + "' title='Rechazar' width='18px' onclick=\"javascript:fn_cotizacionRechazar('" + rowObject.Codigocotizacion + "');\" style='cursor:pointer;'/>";
        } else {
            return "<img src='../Util/images/ico_rechazar_inact.gif' alt='" + cellvalue + "' title='Rechazar' width='18px'/>";
        }
    };

    //**************************************************
    // Aprobar
    //**************************************************
    function aprobar(cellvalue, options, rowObject) {
        if (V_ESTADOCOTIZACION_EVACLIE == fn_util_trim(rowObject.Codigoestadocotizacion)) {
            return "<img src='../Util/images/ico_aprobar_act.gif' alt='" + cellvalue + "' title='Aprobar' width='18px' onclick=\"javascript:fn_cotizacionAprobar('" + rowObject.Codigocotizacion + "');\" style='cursor:pointer;'/>";
        } else {
            return "<img src='../Util/images/ico_aprobar_inact.gif' alt='" + cellvalue + "' title='Aprobar' width='18px'/>";
        }
    };

    //**************************************************
    // Generar Carta
    //**************************************************
    function generacarta(cellvalue, options, rowObject) {
        if ("True" == fn_util_trim(rowObject.Generarcarta)) {
            return "<img src='../Util/images/ico_acc_msgEnviarMini.gif' alt='" + cellvalue + "' title='Generar Carta' width='20px' onclick=\"javascript:fn_mensajecarta('" + rowObject.Codigocotizacion + "', '" + rowObject.Correocontacto + "');\" style='cursor:pointer;'/>";
        } else {
            return "<img src='../Util/images/ico_acc_msgEnviarMini_noenviar.gif' alt='" + cellvalue + "' title='Generar Carta' width='20px'/>";
            //return "<img src='../Util/images/ico_acc_msgEnviarMini.gif' alt='" + cellvalue + "' title='Generar Carta' width='20px' onclick=\"javascript:fn_mensajecarta('" + rowObject.Codigocotizacion + "', '" + rowObject.Correocontacto + "');\" style='cursor:pointer;'/>";
        }
    };

}



//****************************************************************
// Funcion		:: 	fn_cotizacionRechazar 
// Descripción	::	Abre Modal de Motivo de Rechazo
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cotizacionRechazar(strCodigoCotizacion) {
    parent.fn_util_AbreModal("Cotización :: Motivo de rechazo", "Cotizacion/frmMotivoRechazo.aspx?hddCodigoCotizacion=" + strCodigoCotizacion, 635, 315, function() { });
}


//****************************************************************
// Funcion		:: 	fn_mensajecarta 
// Descripción	::	Generar Carta
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_mensajecarta(pstrCodigoCotizacion, pstrCorreo) {
    parent.fn_util_AbreModal("Cotización :: Generar Carta", "Cotizacion/frmGenerarCartaCot.aspx?cc=" + pstrCodigoCotizacion + "&mail=" + pstrCorreo, 300, 120, function() { });
}


//****************************************************************
// Funcion		:: 	fn_mensajeaprobar 
// Descripción	::	Aprobar Cotización
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_cotizacionAprobar(pstrCodigoCotizacion) {
    parent.fn_mdl_confirma('¿Esta Seguro de Aprobar la Cotización?',
        function() {

            parent.fn_blockUI();
            var arrParametros = ["pstrCodigoCotizacion", pstrCodigoCotizacion];
            fn_util_AjaxWM("frmCotizacionListado.aspx/AprobarCotizacion",
                    arrParametros,
                    function(jsondata) {
                        fn_buscarCotizacion(true);
                    },
                    function(resultado) {
                        parent.fn_unBlockUI();
                        var error = eval("(" + resultado.responseText + ")");
                        parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ENVIAR CARTA ERROR");
                    }
            );

        }
		, "Util/images/question.gif"
		, function() { }
		, null
	);
}


//****************************************************************
// Funcion		:: 	fn_opcionimportarcronograma 
// Descripción	::	Importar Cronograma
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_ImportarCronograma(strCodigoCotizacion) {
    parent.fn_util_AbreModal("Cotización:: Importar Cronograma", "Cotizacion/frmImportarCronograma.aspx?hddCodigoCotizacion=" + strCodigoCotizacion, 650, 250, function() { });
}


//****************************************************************
// Funcion		:: 	fn_limpiar 
// Descripción	::	Limpiar Datos
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_limpiar() {
    blnPrimeraBusqueda = false;

    $('#txtNroCotizacion').val("");
    $('#txtCuCliente').val("");
    $('#txtRazonSocial').val("");
    $('#txtFechaIngresoIni').val("");
    $('#txtFechaIngresoFin').val("");

    $('#cmbEjecutivo').val("0");
    $('#cmbClasificacionbien').val("0");
    $('#cmbEstado').val("0");
    //$("#cmbEjecutivo option").eq("0").attr("selected", "selected");

    $("#jqGrid_lista_A").GridUnload();
    fn_cargaGrilla();

}


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {

    //Valida Tipo de Datos
    blnPrimeraBusqueda = false;
    $('#txtNroCotizacion').validText({ type: 'number', length: 8 });
    $('#txtCuCliente').validText({ type: 'number', length: 10 });

    $('#txtRazonSocial').validText({ type: 'comment', length: 100 });

}


//****************************************************************
// Funcion		:: 	fn_abreDetalle para Editar
// Descripción	::	Abre Detalle
// Log			:: 	IJM - 06/03/2012
//****************************************************************
function fn_abreDetalle() {
    var strId = $("#hddCodigoCotizacion").val();
    if (strId == "" || strId == null) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro.", "util/images/warning.gif", "ERROR EN SELECCION");
    } else {
        window.location = "frmCotizacionRegistro.aspx?hddCodigoCotizacion=" + strId;
    }
}


//****************************************************************
// Funcion		:: 	fn_agregar
// Descripción	::	Agregar
// Log			:: 	IJM - 06/03/2012
//****************************************************************
function fn_agregar() {
    fn_util_redirect('frmCotizacionRegistro.aspx');
}


//****************************************************************
// Funcion		:: 	fn_listaEjecutivoLeasing
// Descripción	::	Lista Ejecutivos
// Log			:: 	IJM - 06/03/2012
//****************************************************************
function fn_listaEjecutivoLeasing() {

    //Consulta Ultimus
    var arrParametros = ["Dato", ""];
    fn_util_AjaxWM("frmCotizacionListado.aspx/ConsultaEjecutivoLeasing",
             arrParametros,
             function(result) {
                 parent.fn_unBlockUI();
                 $('#cmbEjecutivo').html(result);                 
             },
             function(resultado) {
                 parent.fn_unBlockUI();
                 var error = eval("(" + resultado.responseText + ")");
                 parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN ULTIMUS");
             }
    );
    
}
