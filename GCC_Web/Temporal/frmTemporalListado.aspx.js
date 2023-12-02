var bFirstClick;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    bFirstClick = false;
    
    // Valida Campos
    fn_inicializaCampos();

    // Carga Grilla
    fn_configuraGrilla();

    // On load Page (siempre al final)
    fn_onLoadPage();
});

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
    //Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));
}

//****************************************************************
// Funcion		:: 	fn_configuraGrilla
// Descripción	::	Carga la grilla a través de llamadas asincronas con web methods
//                  a nivel de servidor.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_configuraGrilla() {

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            fn_BuscarTemporales();
        },
        jsonReader: //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
        {
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "Codigo" // Índice de la columna con la clave primaria.
        },
        colNames: ['Código', 'Texto', 'Comentario', 'Fecha Original', 'Fecha dd/MM/yyyy', 'Número', 'Decimal Original', 'Decimal 2dígitos', 'Decimal 6dígitos', 'Flag', 'Equivalente', 'Id'],
        colModel: [
                    { name: 'Codigo', index: 'Codigo', sortable: true, sorttype: "string", width: 25, align: "center", defaultValue: "" },
                    { name: 'Texto', index: 'Texto', sortable: true, sorttype: "string", width: 80, align: "left", defaultValue: "" },
                    { name: 'Comentario', index: 'Comentario', sortable: true, sorttype: "string", width: 80, align: "left", defaultValue: "" },
                    { name: 'Fecha', index: 'Fecha', sortable: true, sorttype: "string", width: 35, align: "center" },
                    { name: 'Fecha', index: 'Fecha', sortable: true, sorttype: "string", width: 35, align: "center", formatter: Fn_util_ReturnValidDate },
                    { name: 'Numero', index: 'Numero', sortable: true, sorttype: "string", width: 50, align: "left", defaultValue: "" },
                    { name: 'Decimales', index: 'Decimales', sortable: true, sorttype: "string", width: 50, align: "left" },
                    { name: 'Decimales', index: 'Decimales', sortable: true, sorttype: "string", width: 50, align: "left", formatter: Fn_util_ReturnValidDecimal2 },
                    { name: 'Decimales', index: 'Decimales', sortable: true, sorttype: "string", width: 50, align: "left", formatter: Fn_util_ReturnValidDecimal6 },
                    { name: 'Flag', index: 'Flag', sortable: true, sorttype: "string", width: 25, align: "left", defaultValue: "" },
                    { name: 'Flag', index: 'Flag', sortable: true, sorttype: "string", width: 25, align: "left", formatter: fn_Equivalente },
                    { name: 'Id', index: 'Id', sortable: true, sorttype: "string", width: 30, align: "left", hidden: true }
                ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10, // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'Codigo', // Columna a ordenar por defecto.
        sortorder: 'asc', // Criterio de ordenación por defecto.
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
                var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
                $("#hddCodigo").val(rowData.Codigo);
        },
        ondblClickRow: function(id) {
                var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
                window.location = "frmTemporalRegistro.aspx?hddCodigo=" + rowData.Codigo;
        },
        onPaging: function(pgButton) {
				var nextPg = $('#clientList').getGridParam('page');
		}
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    
    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 70);
    
}

//****************************************************************
// Funcion		:: 	fn_Equivalente
// Descripción	::	Convierte en valor ingresado en uno equivalente para ser visuzalizado
//                  por el usuario.
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_Equivalente(cellvalue) {
    switch (cellvalue) {
        case '0':
            return "F";
        case '1':
            return "V";
        default:
            return "";
    }
}

//****************************************************************
// Funcion		:: 	fn_BuscarTemporales
// Descripción	::	Listado de Temporales, que utilizan el método de paginación para la presentación
//                  de resultados.
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_BuscarTemporales() {
    if (!bFirstClick) {
        return;
    } 
    
    var txtCodigo = $('#txtCodigo').val() == undefined ? "" : $('#txtCodigo').val();
    var txtFecha = $('#txtFecha').val() == undefined ? "" : $('#txtFecha').val();
    var txtNumero = $('#txtNumero').val() == undefined ? "" : $('#txtNumero').val();
    var txtTexto = $('#txtTexto').val() == undefined ? "" : $('#txtTexto').val();
    var txtDecimales = $('#txtCodigo').val() == undefined ? "" : $('#txtDecimales').val();
    var txtComentario = $('#txtCodigo').val() == undefined ? "" : $('#txtComentario').val();
    
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"), // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
                         "pCodigo", txtCodigo,
                         "pFecha", txtFecha,
                         "pNumero", txtNumero,
                         "pDecimales", txtDecimales,
                         "pComentario", txtComentario,
                         "pTexto", txtTexto,
                         "pFlag", $("#chkFlag").is(':checked') ? '1' : '0'
                        ];

    fn_util_AjaxWM("frmTemporalListado.aspx/ListaTemporal",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_A.addJSONData(jsondata);
                    },
                    function(request) {
                    alert(JSON.stringify(jsondata));    
                        fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                    }
                   );
}

//*****************************************************************
// Funcion		:: 	fn_Search
// Descripción	::	Ejecuta la búsqueda de los temporales, cuando el usuario hace click en
//                  el botón 'Buscar'.
// Log			:: 	EBL - 04/05/2012
//*****************************************************************
function fn_Search(bSearch) {
    bFirstClick = bSearch;

    fn_BuscarTemporales();
}

//*****************************************************************
// Funcion		:: 	fn_ListaTemporal
// Descripción	::	Limpia la grilla y los controles de la búsqueda.
// Log			:: 	EBL - 04/05/2012
//*****************************************************************
function fn_LimpiarListaTemporal() {
    bFirstClick = false;
    
    $("#jqGrid_lista_A").GridUnload();
    fn_configuraGrilla();
    
    $('#txtCodigo').val("");
    $('#txtFecha').val("");
    $('#txtNumero').val("");
    $('#txtTexto').val("");
    $('#chkFlag').attr('checked', false);
    $('#txtDecimales').val("");
    $('#txtComentario').val("");
}

//****************************************************************
// Funcion		:: 	fn_abreDetalle para Editar
// Descripción	::	Abre Detalle
// Log			:: 	IJM - 06/03/2012
//****************************************************************
function fn_abreDetalle() {

    var strId = fn_util_trim($("#hddCodigo").val());

    if (strId == "" || strId == null || strId == undefined) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro.", "util/images/warning.gif", "ERROR EN SELECCION");
    }
    else {
        window.location = "frmTemporalRegistro.aspx?hddCodigo=" + strId;
    }
}

//****************************************************************
// Función		:: 	fn_confirmaEliminar
// Descripción	::	Le permite eliminar el temporal seleccionado de la grilla, solicitandole
//                  primero la confirmación.
//                  En caso de eliminarlo, le muestra un mensaje, caso contrario le muestra
//                  un mensaje de error.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function fn_confirmaEliminar() {
    var strCodigo = fn_util_trim($("#hddCodigo").val());

    if (strCodigo == "" || strCodigo == null || strCodigo == undefined) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro.", "util/images/warning.gif", "ERROR EN SELECCION");
    }
    else {
        parent.fn_mdl_confirma (
		    "¿Está seguro de eliminar el registro seleccionado?"
		    , fn_Temporal_eliminarWM
		    , "util/images/warning.gif"
		    , function() { }
		    , "ELIMINACIÓN DE REGISTRO"
	    );
    }
}

//****************************************************************
// Función		:: 	fn_Temporal_eliminarWM
// Descripción	::	Elimina un registro por web method, si obtiene respuesta llama al método ResultadoEliminar.
//                  Si ocurre una excepción, muestra el mensaje describiendo la excepción.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function fn_Temporal_eliminarWM() {
    var strCodigo = $("#hddCodigo").val();
    var paramArray = ["pCodigo", strCodigo];
    
    fn_util_AjaxWM("frmTemporalListado.aspx/TemporalEliminar",
                   paramArray,
                   ResultadoEliminar,
                   function(result) {
                       parent.fn_mdl_mensajeIco(result.responseText, "util/images/warning.gif", "ELIMINACIÓN DE REGISTRO");
                   });
}

//****************************************************************
// Función		:: 	ResultadoEliminar
// Descripción	::	Le muestra un mensaje de éxito en la eliminación, caso contrario le describe el error.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function ResultadoEliminar(result) {
    if (result == "0") {
        parent.fn_mdl_mensajeIco("Se eliminó con éxito el registro.", "util/images/ok.gif", "ELIMINACIÓN DE REGISTRO");

        fn_ListaTemporal();
    }
    else {
        parent.fn_mdl_mensajeIco("Ocurrió un error al eliminar el registro.", "util/images/warning.gif", "ELIMINACIÓN DE REGISTRO");
    }
}

//****************************************************************
// Función		:: 	fn_MensajeYRedireccionar
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
//                  Luego lo redirecciona al formulario de búsquedas ("frmTemporalListado.aspx").
// Log			:: 	EBL - 07/05/2012
//****************************************************************
var fn_MensajeYRedireccionar = function(pCodigo) {

    if (pCodigo == "0") {
        fn_mdl_alert("Se grabó con éxito Check List Comercial: " + pCodigo.toString(), function() { });
    } else {
        fn_mdl_alert("Se actualizó con éxito el Temporal.", function() { });
    }

    fn_util_redirect("frmTemporalListado.aspx");
};

function fn_AbrirReporte() {
    //var x = 0, y = 0;
    //var w = screen.availWidth - 10;
    //var h = screen.availHeight - 50;
    //window.open(strRuta, '', "top=" + x + ",left=" + y + ",width=" + w + ",height=" + h + ",toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,copyhistory=no, resizable=yes");
    window.open ('frmReporte.aspx');
}


function VentanaAsociarBien() {
    parent.fn_util_AbreModal("Lista :: BIENES", "Desembolso/frmBienBusqueda.aspx?csc=00000001&cdc=0&clb=002", 1000, 600, function() { });
}


// Se lo pone primero para que pueda acceder a las configuraciones correspondientes.
//$("#txtAdjuntarAdenda").replaceWith("<input type='file' id='txtAdjuntarAdenda' name='txtAdjuntarAdenda' size='60' runat='server' style='width: 400px;' />");
