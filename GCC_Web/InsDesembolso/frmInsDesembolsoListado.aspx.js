//****************************************************************
// Variables Globales
//****************************************************************
var blnPrimeraBusqueda;
var intPaginaActual = 1;


//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 19/09/2012
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
            fn_buscarInsDesembolso(true);
        }
    });
    
    //On load Page (siempre al final)
    fn_onLoadPage();
    
});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {

    blnPrimeraBusqueda = false;
    
    $('#txtNroContrato').validText({ type: 'number', length: 8 });
    $('#txtCuCliente').validText({ type: 'number', length: 10 });
    $('#txtRazonSocial').validText({ type: 'comment', length: 100 });
    $('#txtNroInstruccion').validText({ type: 'number', length: 8 });    
    //$('#cmbEstado').html(strComboVacio);        
    
}



//****************************************************************
// Funcion		:: 	fn_buscarCotizacion
// Descripción	::	Busca listado de cotizacion por parametros
// Log			:: 	JRM - 19/09/2012
//****************************************************************
function fn_buscarInsDesembolso(pblnBusqueda) {
    blnPrimeraBusqueda = pblnBusqueda;
	intPaginaActual = 1;
    fn_realizaBusquedaInsDesembolso();
}
function fn_realizaBusquedaInsDesembolso() {
    if (!blnPrimeraBusqueda) {
        return;
    }else {
   
	try {
        parent.fn_blockUI();

		var txtNroContrato = $('#txtNroContrato').val() == undefined ? "" : $('#txtNroContrato').val();
        var txtCuCliente = $('#txtCuCliente').val() == undefined ? "" : $('#txtCuCliente').val();
        var txtRazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
        var txtNroInstruccion = $('#txtNroInstruccion').val() == undefined ? "" : $('#txtNroInstruccion').val();
        var txtFechaIngresoIni = $('#txtFechaIngresoIni').val() == undefined ? "" : $('#txtFechaIngresoIni').val();
        var txtFechaIngresoFin = $('#txtFechaIngresoFin').val() == undefined ? "" : $('#txtFechaIngresoFin').val();
        var cmbTipoContrato = $('#cmbTipoContrato').val() == undefined ? "" : $('#cmbTipoContrato').val();
        var cmbEstado = $('#cmbEstado').val() == undefined ? "" : $('#cmbEstado').val();
        var cmbMoneda = $('#cmbMoneda').val() == undefined ? "" : $('#cmbMoneda').val();
        //Inicio IBK - AAE - Agrego Nro de WIO
        var txtNroWIO = $('#txtNroWIO').val() == undefined ? "" : $('#txtNroWIO').val();
        
        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
                             "pstrNroContrato", txtNroContrato,
                             "pstrCuCliente", txtCuCliente,
                             "pstrRazonSocial", txtRazonSocial,
                             "pstrNroInstruccion", txtNroInstruccion,
                             "pstrFechaIngresoIni", txtFechaIngresoIni,
                             "pstrFechaIngresoFin", txtFechaIngresoFin,
                             "pstrTipoContrato", cmbTipoContrato,
                             "pstrEstado", cmbEstado,
                             //"pstrMoneda", cmbMoneda
                             "pstrMoneda", cmbMoneda,
                             "pstrNroWIO", txtNroWIO,
                            ];
		//Fin IBK
        fn_util_AjaxWM("frmInsDesembolsoListado.aspx/ListaInsDesembolso",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);
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
}


//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla Listado de Cotizaciones
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_cargaGrilla() {

     $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");	
            fn_realizaBusquedaInsDesembolso();
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
        //Inicio IBK - AAE - Se agrega Nro de WIO
        //colNames: ['Nº Contrato', 'Nº Cotización', 'CU Cliente', 'Razon Social o nombre', 'Clasificacion del Bien', 'Nº Instrucción', 'Moneda', 'Monto Desembolsado', 'Tipo de Contrato', 'Estado de Instrucción', ''],
        colNames: ['Nº Contrato','Nº Cotización','N° Inst. Operativa','CU Cliente','Razon Social o nombre','Clasificacion del Bien','Nº Instrucción','Moneda','Monto Desembolsado','Tipo de Contrato','Estado de Instrucción',''],
        colModel: [
                { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', sortable: true, sorttype: "int", width: 40, align: "center", defaultValue: "" },
                { name: 'CodigoCotizacion', index: 'CodigoCotizacion', sortable: true, sorttype: "int", width: 40, align: "center", defaultValue: "" },
                { name: 'NroWIO', index: 'NroWIO', sortable: true, sorttype: "string", width: 60, align: "center", defaultValue: "" },
                { name: 'CodUnico', index: 'CodUnico', sortable: true, sorttype: "string", width: 40, align: "center", defaultValue: "" },
                //{ name: 'ClienteRazonSocial', index: 'ClienteRazonSocial', sortable: true, sorttype: "string", align: "left", defaultValue: "" },
                {name: 'ClienteRazonSocial', index: 'ClienteRazonSocial', sortable: true, sorttype: "string", align: "left", defaultValue: "" },
                { name: 'ClasificacionBien', index: 'ClasificacionBien', sortable: true, width: 70, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'CodInstruccionDesembolso', index: 'CodInstruccionDesembolso', sortable: true, width: 40, sorttype: "string", align: "center", defaultValue: "" },                
                { name: 'NombreMoneda', index: 'NombreMoneda', sortable: true, sorttype: "string", width: 45, align: "center", defaultValue: "" },
                { name: 'Totaldesembolsado', index: 'Totaldesembolsado', sortable: true, sorttype: "float", width: 45, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'TipoContrato', index: 'TipoContrato', sortable: true, sorttype: "string", width: 40, align: "center", defaultValue: "" },
                { name: 'NombreEstado', index: 'NombreEstado', sortable: true, sorttype: "string", width: 55, align: "center", defaultValue: "" },
                { name: 'CodEstadoInstruccion', index: 'CodEstadoInstruccion', hidden: true}
        ],
        //Fin IBK
        width: glb_intWidthPantalla-70,
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'CodInstruccionDesembolso',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodigoInsDesembolso").val(rowData.CodInstruccionDesembolso);
            $("#hddCodigoContrato").val(rowData.CodSolicitudCredito);        
            $("#hddCodigoEstadoID").val(rowData.CodEstadoInstruccion);                        
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            window.location = "frmInsDesembolsoRegistro.aspx?hddCodigoInsDesembolso=" + rowData.CodInstruccionDesembolso+"&hddCodigoContrato="+rowData.CodSolicitudCredito+"&hddCodigoEstadoID="+rowData.CodEstadoInstruccion;
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

}


//****************************************************************
// Funcion		:: 	fn_abreEditar
// Descripción	::	Abre Detalle
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_abreEditar() {
    var strId = $("#hddCodigoInsDesembolso").val();
    var strEstado = $("#hddCodigoEstadoID").val();
    
    if (strId == "" || strId == null) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro.", "util/images/warning.gif", "ERROR EN SELECCION");
    } else {
        window.location = "frmInsDesembolsoRegistro.aspx?hddCodigoInsDesembolso=" + strId + "&hddCodigoEstadoID=" + strEstado;
    }   
}


//****************************************************************
// Funcion		:: 	fn_limpiarForm 
// Descripción	::	Limpiar Datos
// Log			:: 	JRC - 18/09/2012
//****************************************************************
function fn_limpiarForm() {
    blnPrimeraBusqueda = false;

    $('#txtNroContrato').val("");
    $('#txtCuCliente').val("");
    $('#txtRazonSocial').val("");
    $('#txtFechaIngresoIni').val("");
    $('#txtFechaIngresoFin').val("");

    $('#txtNroInstruccion').val("");
    $('#cmbClasificacionbien').val("0");
    $('#cmbEstado').val("0");
    $('#cmbMoneda').val("0");
    $('#cmbTipoContrato').val("0");
    $("#jqGrid_lista_A").GridUnload();
    fn_cargaGrilla();

}