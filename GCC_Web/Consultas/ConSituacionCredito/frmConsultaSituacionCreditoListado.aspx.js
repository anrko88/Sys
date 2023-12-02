var bFirstClick;

var CodigoEstadoContrato = new Object();
CodigoEstadoContrato.PendienteDeCarta = "04";
CodigoEstadoContrato.EnviadoCliente = "05";

var CrLf = 1;
var intPaginaActual = 1;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    // Inicializa el valor de campos.
    fn_inicializaCampos();

    // Configura la estructura de la grilla.
    fn_configuraGrilla();

    //Busca con Enter
    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscarContratos(true);
        }
    });
    
    // On load Page (siempre al final).
    fn_onLoadPage();
});


//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_configuraGrilla() {
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");	
            fn_buscarListarContratos();
        },
        jsonReader: {                 // Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage",      // Número de página actual.
            total: "PageCount",       // Número total de páginas.
            records: "RecordCount",   // Total de registros a mostrar.
            repeatitems: false,
            id: "CodSolicitudCredito" // Índice de la columna con la clave primaria.
        },
        colNames: ['Nº Contrato', 'Nº Cotización', 'CU Cliente', 'Razón Social o Nombre', 'Clasificación Bien', 'Estado', 'Fecha', '', '', '', ''],
        colModel: [
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', sortable: true, width: 32, sorttype: "string", align: "center" },
		    { name: 'CodigoCotizacion', index: 'CodigoCotizacion', sortable: true, width: 32, sorttype: "string", align: "center" },
		    { name: 'CodUnico', index: 'CodUnico', sortable: true, sorttype: "string", width: 32, align: "center" },
		    { name: 'NombreSubprestatario', index: 'NombreSubprestatario', sortable: true, sorttype: "string", width: 55, align: "left" },
		    { name: 'ClasificacionBien', index: 'ClasificacionBien', sortable: true, sorttype: "string", width: 50, align: "center" },
		    { name: 'NombreEstadoOperacionActiva', index: 'NombreEstadoOperacionActiva', sortable: true, width: 50, sorttype: "string", align: "center" },
		    { name: 'FechaSolicitudCredito', index: 'FechaSolicitudCredito', sortable: true, sorttype: "string", width: 30, align: "center", formatter: fn_util_ValidaStringFecha },
		    //{ name: 'ArchivoContratoAdjunto', index: 'ArchivoContratoAdjunto', width: 25, align: "center", sortable: false, sorttype: "string", formatter: fnEl },
		    {name: 'EstadoOperacionActiva', index: 'EstadoOperacionActiva', hidden: true },
		    { name: 'CorreoContacto', index: 'CorreoContacto', hidden: true },
		    { name: 'ArchivoContratoAdjunto', index: 'ArchivoContratoAdjunto', hidden: true },
		    { name: 'FlagModificado', index: 'FlagModificado', hidden: true }
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
        	$("#hddId").val(id);
            $("#hddCodigoContrato").val(rowData.CodSolicitudCredito);
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            window.location = "frmConsultaSituacionCreditoRegistro.aspx?pTipoTransaccion=EDITAR&hddCodigo=" + rowData.CodSolicitudCredito;
        }
    });
    function fnEl(cellvalue, options, rowObject) {
        if (fn_util_trim(rowObject.EstadoOperacionActiva) == CodigoEstadoContrato.PendienteDeCarta) {
            return "<img src=\"../../Util/images/ico_acc_msgEnviarMini.gif\" alt=\"" + cellvalue + "\" title=\"Generar Carta\" width=\"20px\" height=\"20px\" style=\"cursor: pointer;cursor: hand;\" onclick=\"javascript:fn_EnviarCarta('" + options.rowId + "');\" />";
        } else {
            return "<img src='../../Util/images/ico_acc_msgEnviarMini_noenviar.gif' alt='' width='20px' height='20px' />";
        }
    };

    $("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 85);
}

// ****************************************************************
// Funcion		:: 	fn_EnviarCarta
// Descripción	::	Envía la carta del cliente que se encuentra en la fila
//                  seleccionada de la grilla.
// Log			:: 	EBL - 06/03/2012
//*****************************************************************
function fn_EnviarCarta(id) {

    parent.fn_blockUI();
    
    if (EsValidoEnviarCarta(id)) {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
        parent.fn_util_AbreModal("Contrato :: Generar Carta", "Formalizacion/frmGenerarCartaContrato.aspx?cc=" + rowData.CodSolicitudCredito + "&mail=" + rowData.CorreoContacto + "&rutacarta=" + rowData.ArchivoContratoAdjunto + "&nombreCliente=" + rowData.NombreSubprestatario, 300, 120, function() { });

    }
    parent.fn_unBlockUI();
}

//****************************************************************
// Función		:: 	EsValidoEnviarCarta
// Descripción	::	Verifica si los datos del contrato no han sido modificados y por lo tanto es válido enviar
//                  la carta.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function EsValidoEnviarCarta(id) {

    var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);

    if (rowData.FlagModificado == "True") {
        parent.fn_mdl_mensajeIco("Genere nuevamente los anexos para enviar el contrato.", "util/images/warning.gif", "GENERAR CARTA");
        return false;
    } else {
        return true;     
    }
}

//****************************************************************
// Función		:: 	fn_contrato_GeneraCartaWM
// Descripción	::	Envía la carta al cliente.
//                  Si ocurre una excepción, muestra el mensaje describiendo la excepción.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function fn_contrato_GeneraCartaWM() {
    try {
        var paramArray = [
                             "pCodigoContrato", $("#hddId").val()
                         ];

        fn_util_AjaxSyncWM("frmContratoListado.aspx/EnviarCarta",
                           paramArray,
                           ResultadoEnviar,
                           function(result) {
                               parent.fn_mdl_mensajeIco(result.responseText, "util/images/warning.gif", "ENVIAR CARTA");
                           });

       fn_doResize();
   } catch (ex) {
       parent.fn_mdl_mensajeIco(ex.message, "util/images/warning.gif", "ENVIAR CARTA");
   } finally {
       parent.fn_unBlockUI();
   }
}

//****************************************************************
// Función		:: 	ResultadoEnviar
// Descripción	::	Le muestra un mensaje de éxito en el envío de la carta, caso contrario le describe el error.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function ResultadoEnviar(result) {
    if (result == "0") {
        try {
            // Validar los datos requeridos del antes de enviar la carta.
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', $("#hddId").val());
            if (IsNullOrEmpty(rowData.CorreoContacto)) {
                parent.fn_mdl_mensajeIco("No se estableció un correo electrónico.", "util/images/ok.gif", "ENVIAR CARTA");
                return;
            }
            if (IsNullOrEmpty(rowData.NombreSubprestatario)) {
                parent.fn_mdl_mensajeIco("No se estableció el nombre del cliente.", "util/images/ok.gif", "ENVIAR CARTA");
                return;
            }
            if (IsNullOrEmpty(rowData.ArchivoContratoAdjunto)) {
                parent.fn_mdl_mensajeIco("No existe la carta.", "util/images/ok.gif", "ENVIAR CARTA");
                return;
            }
      
            var contenido = fn_Mail_Contenido(rowData.CodSolicitudCredito);
            var vContenido = contenido.split("|");
            if (vContenido[0] == "1") {
                var pstrCorreo = rowData.CorreoContacto;

                var pstrTitulo = vContenido[1];
                var pstrBody = vContenido[2];
                fn_util_MailTo(pstrCorreo, pstrTitulo, pstrBody);

                fn_abreArchivo(rowData.ArchivoContratoAdjunto);

                parent.fn_mdl_mensajeIco("Se envió con éxito la carta.", "util/images/ok.gif", "ENVIAR CARTA");
            } else {
                parent.fn_mdl_mensajeIco("Ocurrió un error al generar la carta.", "util/images/warning.gif", "ERROR AL GENERAR CARTA");
            }
        } catch(ex) {
            parent.fn_mdl_mensajeIco(ex.toString(), "util/images/warning.gif", "ENVIAR CARTA");
        } finally {
            fn_buscarListarContratos();
        }
    } else {
        parent.fn_mdl_mensajeIco("Ocurrió un error al enviar la carta.", "util/images/warning.gif", "ENVIAR CARTA");
    }
}

//************************************************************
// Función		:: 	fn_Mail_Contenido
// Descripcion 	::  Genera la carta y envía la envía, devolviendo el contenido de la misms.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_Mail_Contenido(codSolicitudCredito) {
    var mail;
    var arrParametros = [
                            "pContrato", codSolicitudCredito
                        ];
    fn_util_AjaxSyncWM("frmContratoListado.aspx/ContenidoMail",
                        arrParametros,
                        function(jsondata) {
                            mail = jsondata;
                        },
                        function() {
                            parent.fn_mdl_mensajeIco("Ocurrió un error al generar la carta.", "util/images/warning.gif", "ERROR AL GENERAR CARTA");
                            mail = "0";
                        }
                      );

    return mail;
}

//************************************************************
// Función		:: 	fn_ListadoBienesCarta
// Descripcion 	::  Genera una lista de bienes, listados por su descripción y marca.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_ListadoBienesCarta() {
    var listaBienes = "";
    var arrParametros = [
                            "pContrato", $('#hddId').val()
                        ];

    fn_util_AjaxSyncWM("frmSituacionCreditoRegistro.aspx/ListarBienesDescripcion",
                        arrParametros,
                        function(jsondata) {
                            var vBienes = jsondata.split("|");

                            for (var i = 0; i < vBienes.length; i++) {
                                listaBienes += " - " + vBienes[i] + ".%0A";
                            }
                        },
                        function() { }
                       );

    return listaBienes;
}

//****************************************************************
// Funcion		:: 	fn_abreArchivo 
// Descripción	::	Abre Archivo
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_abreArchivo(pstrRuta) {
    window.open("../../frmDownload.aspx?nombreArchivo=" + pstrRuta);

    return false;
}

// ****************************************************************
// Función		:: 	fn_abreDetalle
// Descripción	::	Abre Detalle.
// Log			:: 	IJM - 06/03/2012
//*****************************************************************
function fn_abreDetalle() {
	
    try {
        var strId = fn_util_trim($("#hddCodigoContrato").val());

        if (strId == "" || strId == null || strId == undefined) {
        	parent.fn_mdl_mensajeError("Debe seleccionar un Contrato", function() { }, "VALIDACIÓN");
        } else {
        	parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', $("#hddId").val());
            window.location = "frmConsultaSituacionCreditoRegistro.aspx?pTipoTransaccion=EDITAR&hddCodigo=" + rowData.CodSolicitudCredito;
        }
    } catch (ex) {
        parent.fn_mdl_mensajeIco(ex.message, "util/images/warning.gif", "ERROR EN SELECCION");
    }
}

//*****************************************************************
// Función		:: 	fn_inicializaCampos
// Descripción	::	Inicializa las variables globales y los controles.
// Log			:: 	EBL - 10/02/2012
//*****************************************************************
function fn_inicializaCampos() {
    bFirstClick = false;
    
    fn_InicializarControles();
}

//*****************************************************************
// Función		:: 	fn_InicializarControles
// Descripción	::	Inicializa los controles
// Log			:: 	EBL - 10/02/2012
//*****************************************************************
function fn_InicializarControles() {
    // Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    $('#txtContrato').validText({ type: 'number', length: 8 });
    $('#txtCotizacion').validText({ type: 'number', length: 8 });
    $('#txtCuCliente').validText({ type: 'number', length: 10 });
    $('#txtKardex').validText({ type: 'number', length: 10 });
    $('#txtKardex').maxLength(10);
    $('#txtRazonSocial').validText({ type: 'comment', length: 100 });
	$('#txtZonal').validText({ type: 'comment', length: 50 });
}

//****************************************************************
// Función		:: 	fn_limpiarContrato
// Descripción	::	Limpia las grillas.
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_limpiarContrato() {
    try {
        parent.fn_blockUI();
        
        bFirstClick = false;

        $('#txtContrato').val("");
        $('#txtCuCliente').val("");
        $('#txtRazonSocial').val("");
        $('#txtCotizacion').val("");
        $('#txtFechaIni').val("");
        $('#txtFechaFin').val("");
        $("#cmbEjecutivo option:first").attr('selected', 'selected');
        $("#cmbEstado option:first").attr('selected', 'selected');
        $('#txtZonal').val("");
        $("#cmbClasificacion option:first").attr('selected', 'selected');
        $("#cmbClasificacionContrato option:first").attr('selected', 'selected');
        $("#cmbBanca option:first").attr('selected', 'selected');
        $("#cmbTipoPersona option:first").attr('selected', 'selected');
        $("#cmbNotaria option:first").attr('selected', 'selected');
        $("#txtKardex").val("");

        $("#jqGrid_lista_A").GridUnload();
        fn_configuraGrilla();

        fn_doResize();
    } catch (ex) {
        parent.fn_mdl_mensajeIco(ex.message, "util/images/warning.gif", "BÚSQUEDA CONTRATO");
    } finally {
        parent.fn_unBlockUI();
    }
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
    
    fn_buscarListarContratos();
}

//****************************************************************
// Función		:: 	fn_buscarListarContratos
// Descripción	::	Devuelve los contratos coincidentes con los criterios de búsqueda.
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_buscarListarContratos() {
    if (!bFirstClick) {
        return;
    }

    try {
        parent.fn_blockUI();
        
        if (fn_ValidarDatos()) {
            var contrato =          $('#txtContrato').val() == undefined ? "" : $('#txtContrato').val();
            var cuCliente =         $('#txtCuCliente').val() == undefined ? "" : $('#txtCuCliente').val();
            var razonSocial =       $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
            var cotizacion =        $('#txtCotizacion').val() == undefined ? "" : $('#txtCotizacion').val();
            var fechaIni =          $('#txtFechaIni').val() == undefined ? "" : $('#txtFechaIni').val();
            var fechaFin =          $('#txtFechaFin').val() == undefined ? "" : $('#txtFechaFin').val();
            var ejecutivo =         $("#cmbEjecutivo option:selected").val() == "0" ? "" : $("#cmbEjecutivo option:selected").val();
            var estado =            $("#cmbEstado option:selected").val() == "0" ? "" : $("#cmbEstado option:selected").val();
            var zonal =             $('#txtZonal').val() == undefined ? "" : $('#txtZonal').val();
            var clasificacion =     $("#cmbClasificacion option:selected").val() == "0" ? "" : $("#cmbClasificacion option:selected").val();
            var clasificacionContrato = $("#cmbClasificacionContrato option:selected").val() == "0" ? "" : $("#cmbClasificacionContrato option:selected").val();
            var banca =             $("#cmbBanca option:selected").val() == "0" ? "" : $("#cmbBanca option:selected").val();
            var tipoPersona =       $("#cmbTipoPersona option:selected").val() == "0" ? "" : $("#cmbTipoPersona option:selected").val();
            var notaria =           $("#cmbNotaria option:selected").val() == "0" ? "" : $("#cmbNotaria option:selected").val();
            var kardex =            $('#txtKardex').val() == undefined ? "" : $('#txtKardex').val();

            var arrParametros = [
                                 "pPageSize",       fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página.
                                 "pCurrentPage",    intPaginaActual,                                     // Página actual.
                                 "pSortColumn",     fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
                                 "pSortOrder",      fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
                                 "pContrato",       contrato,
                                 "pCuCliente",      cuCliente,
                                 "pRazonSocial",    razonSocial,
                                 "pCotizacion",     cotizacion,
                                 "pFechaIni",       fechaIni,
                                 "pFechaFin",       fechaFin,
                                 "pEjecutivo",      ejecutivo,
                                 "pEstado",         '',
                                 "pZonal",          zonal,
                                 "pClasificacion",  clasificacion,
                                 "pClasificacionContrato", clasificacionContrato,
                                 "pCodigoBanca",    banca,
                                 "pTipoPersona",    tipoPersona,
                                 "pNotaria",        notaria,
                                 "pKardex", kardex,
                                 "pFields", "CodSolicitudCredito|CodigoCotizacion|CodUnico|NombreSubprestatario|ClasificacionBien|NombreEstadoOperacionActiva|FechaSolicitudCredito|ArchivoContratoAdjunto|EstadoOperacionActiva|CorreoContacto|FlagModificado",
                                 "pEstadoOperacionActiva", estado
                                ];

            fn_util_AjaxWM("frmConsultaSituacionCreditoListado.aspx/BuscarContratos",
                           arrParametros,
                           function(jsondata) {
                               jqGrid_lista_A.addJSONData(jsondata);
                               parent.fn_unBlockUI();
                               fn_doResize();
                           },
                           function(request) {
                               parent.fn_unBlockUI();
                               parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR EN LA BÚSQUEDA");
                           });
         }
     } catch (ex) {
        // parent.fn_unBlockUI();
         parent.fn_mdl_mensajeIco(ex.message, "util/images/warning.gif", "ERROR EN LA BÚSQUEDA");
    } 
}

//****************************************************************
// Función		:: 	fn_ValidarDatos
// Descripción	::	Verifica que los datos de las consultas sean válidos, 
//                  antes de realizar la búsqueda.
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_ValidarDatos() {
    var strError = new StringBuilderEx();

    if ($('#txtFechaIni').val() != "") {
        var txtFechaIni = $('input[id=txtFechaIni]:text');
        strError.append(fn_util_ValidateControl(txtFechaIni[0], 'una fecha inicial válida', CrLf, ""));
    }
    if ($('#txtFechaFin').val() != "") {
        var txtFechaFin = $('input[id=txtFechaFin]:text');
        strError.append(fn_util_ValidateControl(txtFechaFin[0], 'una fecha final válida', CrLf, ""));
    }

    if (strError.toString() != "") {
        parent.fn_mdl_mensajeIco(strError.toString(), "util/images/warning.gif", "ERROR EN DATOS DE BÚSQUEDA");
        return false;
    } else {
        fn_InicializarControles();
        return true;
    }
}


/*****************************************************************
Funcion		:: 	fn_Reporte
Descripción	::	Genera Reporte
Log			:: 	AEP - 18/01/2013
***************************************************************** */

function fn_Reporte() {
	
	
//	var vElementosAEditar = idsOfSelectedRows;
//	var count = vElementosAEditar.length;
//	if(IsNullOrEmpty(count)) 
//	{
//	parent.fn_mdl_mensajeIco("Seleccione al menos un registro para poder generar el reporte.", "util/images/warning.gif", "LIQUIDIDAR IMPUESTO VEHICULAR");	
//	}else 
//	{
//	    
//		var strCodigo = '';

//            if(vElementosAEditar != "") {

//            	for (var j = 0; j < vElementosAEditar.length; j++) {
//            		strCodigo = strCodigo + vElementosAEditar[j] + ',';
//            	}
             $("#btnGenerar").click();
            	//window.location = "../../Reportes/frmRepSituacionCredito.aspx";

//            } else{
//			parent.fn_mdl_mensajeIco("Seleccione un registro para poder generar el reporte.", "util/images/warning.gif", "LIQUIDIDAR IMPUESTO VEHICULAR");
//            }
//       
//	}
			
}