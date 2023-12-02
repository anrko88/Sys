//****************************************************************
// Variables Globales
//****************************************************************


var strTipoDocumentoDNI = "1";
var strTipoDocumentoRUC = "2";
var strTipoDocumentoCarnetEx = "3";
var strTipoDocumentoPasaporte = "5";
var strTipoDocumentoOtroDoc = "6";
var strUTT = "006";
var strDepartamento = "15";
var strProvincia = "01";
var blnPrimeraBusqueda;
var intPaginaActual = 1;


//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	??? - 02/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();
    fn_SeteaMunicipalidad();
    $('#txtNroDocumento').attr('disabled', true);
    $('#ddlTipoDocumento').change(function() {
        var strValor = $(this).val();
        $("#txtNroDocumento").val("");
        $('#txtNroDocumento').unbind('keypress');
        //$('#cmbTipoPersona').val("0");
        //$('#txtRazonSocial').val("");
        if (fn_util_trim(strValor) == strTipoDocumentoDNI) {
            $('#txtNroDocumento').attr('disabled', false);
            $('#txtNroDocumento').validText({ type: 'number', length: 8 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoRUC) {
            $('#txtNroDocumento').attr('disabled', false);
            $('#txtNroDocumento').validText({ type: 'number', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoCarnetEx) {
            $('#txtNroDocumento').attr('disabled', false);
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoPasaporte) {
            $('#txtNroDocumento').attr('disabled', false);
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoOtroDoc) {
            $('#txtNroDocumento').attr('disabled', false);
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else {
            $('#txtNroDocumento').attr('disabled', 'disabled');
        }

    });

    fn_cargarTipoBien();
    //Carga Grilla
    fn_cargaGrilla();


    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscar(true);
        }
    });

    //On load Page (siempre al final)
    fn_onLoadPage();


});


//************************************************************
// Función		:: 	fn_cargarTipoBien
// Descripcion 	:: 	Carga el combo de tipo de bien
// Log			:: 	AEP - 23/11/2012
//************************************************************
function fn_cargarTipoBien() {
    var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL104", "pstrCodigoGenerico", strUTT];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {

            $('#ddlTipoBien').html(arrResultado[1]); $('#ddlTipoBien').val(strUTT);

        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
}
//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {

    fn_util_SeteaCalendario($("#txtFechaInscripcionDesde"));
    fn_util_SeteaCalendario($("#txtFechaInscripcionHasta"));

    $('#txtNroContrato').validText({ type: 'number', length: 8 });
    $('#txtcuCliente').validText({ type: 'number', length: 10 });
    $('#txtRazonSocialNombre').validText({ type: 'comment', length: 100 });
    $('#txtPlaca').validText({ type: 'alphanumeric', length: 10 });
    $('#txtNroLote').validText({ type: 'number', length: 8 });
    $('#txtAnioFabricacion').validText({ type: 'number', length: 4 });
    $('#txtPeriodo').validText({ type: 'number', length: 4 });
    $('#txtCodigoInfraccion').validText({ type: 'number', length: 3 });
}


//****************************************************************
// Funcion		:: 	fn_SeteaMunicipalidad
// Descripción	::	Setear la municipalidad
// Log			:: 	AEP - 23/11/2012
//****************************************************************
function fn_SeteaMunicipalidad() {

    //Carga Distrito
    fn_cargaComboMunicipalidad(strDepartamento, strProvincia);
    //$("#ddlMunicipalidad").val(fn_util_trim($("#hidCodDistrito").val()));
}
//********************************************************  ********
// Funcion		:: 	fn_cargaComboDistritoInmueble
// Descripción	::	
// Log			:: 	AEP - 23/11/2012
//****************************************************************
function fn_cargaComboMunicipalidad(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlMunicipalidad').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    //fn_doResize();
}

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 08/11/2012
//****************************************************************
function fn_cargaGrilla() {

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_ListarMulta();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Nro. Contrato', 'CU Cliente', 'Razón Social o Nombre', 'Nro. Lote', 'Municipalidad', 'Tipo de Bien', 'Placa Actual', 'Marca', 'Modelo', 'Importe', 'Fecha Pago', 'Fecha Cobro', 'Nº Cheque', '', '', '', '', '', '', '', ''],
        colModel: [
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 50, sorttype: "string", align: "center" },
            { name: 'CodUnico', index: 'CodUnico', width: 40, sorttype: "string", align: "center" },
        	{ name: 'RazonSocial', index: 'RazonSocial', width: 110, align: "left", sorttype: "string" },
        	{ name: 'CodNroLote', index: 'CodNroLote', width: 50, align: "center", sorttype: "string" },
		    { name: 'Municipalidad', index: 'Municipalidad', width: 50, align: "center", sorttype: "string" },
        	{ name: 'TipoBien', index: 'TipoBien', width: 50, align: "center", sorttype: "string" },
        	{ name: 'Placa', index: 'Placa', width: 50, align: "center", sorttype: "string" },
		    { name: 'Marca', index: 'Marca', width: 60, align: "center" },
        	{ name: 'Modelo', index: 'Modelo', width: 60, align: "center" },
		    { name: 'Importe', index: 'Importe', width: 50, align: "center", sorttype: "string" },
		    { name: 'FechaPago', index: 'FechaPago', width: 50, align: "center", sorttype: "string" },
        	{ name: 'FechaCobro', index: 'FechaCobro', width: 50, align: "center", sorttype: "string" },
        	{ name: 'NroCheque', index: 'NroCheque', width: 50, align: "center", sorttype: "string" },
        	{ name: '', index: '', width: 3 },
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
            { name: 'CodigoEstadoContrato', index: 'CodigoEstadoContrato', hidden: true },
        	{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
        	{ name: 'CodMunicipalidad', index: 'CodMunicipalidad', hidden: true },
        	{ name: 'SecMulta', index: 'SecMulta', hidden: true },
        	{ name: 'EstadoPago', index: 'EstadoPago', hidden: true },
        	{ name: 'FechaTransferencia', index: 'FechaTransferencia', hidden: true }
	    ],
        height: '100%',
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
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddRowId").val(id);
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            var strPlaca = rowData.Placa;
            var strCodMulta = rowData.SecMulta;
            var strCheque = rowData.NroCheque;
            var strCodMunicipalidad = rowData.CodMunicipalidad;
            var strEstadoPago = rowData.EstadoPago;
            var strFecTransferencia = rowData.FechaTransferencia;
            
            alert('/Consultas/MultaVehicular/frmMultaVehicularConsulta.aspx?placa=' + strPlaca + '&tipo=1' + '&codMulta=' + strCodMulta + '&codMunicipalidad=' + strCodMunicipalidad + '&cheque=' + strCheque + '&EstPago=' + strEstadoPago + '&FecTrans=' + strFecTransferencia);
            fn_util_globalRedirect('/Consultas/MultaVehicular/frmMultaVehicularConsulta.aspx?placa=' + strPlaca + '&tipo=1' + '&codMulta=' + strCodMulta + '&codMunicipalidad=' + strCodMunicipalidad + '&cheque=' + strCheque + '&EstPago=' + strEstadoPago + '&FecTrans=' + strFecTransferencia);
        }
    });

    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    $("#hddRowId").val('');
}

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_buscar(bSearch) {

    blnPrimeraBusqueda = bSearch;
    fn_ListarMulta();
}

//****************************************************************
// Funcion		:: 	fn_ListarMulta
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	AEP - 12/11/2012
//****************************************************************
function fn_ListarMulta() {
    if (!blnPrimeraBusqueda) {
        return;
    } else {


        parent.fn_blockUI();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"),
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"),
    	                 "pNumeroContraro", $("#txtNroContrato").val(),
	                     "pCUCliente", $("#txtcuCliente").val(),
	                     "pRazonSocial", $("#txtRazonSocialNombre").val(),
	                     "pTipoDocumento", $("#ddlTipoDocumento").val(),
    	                 "pTipoBien", $("#ddlTipoBien").val(),
	                     "pNumeroDocumento", $("#txtNroDocumento").val(),
	                     "pNroLote", $("#txtNroLote").val(),
    	                 "pConcepto", $("#ddlConcepto").val(),
	                     "pPlaca", $("#txtPlaca").val(),
	                     "pCodInfraccion", $("#ddlCodInfraccion").val(),
    	                 "pInfraccion", $("#txtCodigoInfraccion").val(),
    	                 "pCodMunicipalidad", $("#ddlMunicipalidad").val(),
                         "pEstadoCobro", $("#ddlEstadoCobro").val(),
    	                 "pEstadoPago", $("#ddlEstadoPago").val()
                         ];

        fn_util_AjaxWM("frmMultaVehicularListado.aspx/ListaMultaVehicular",
                   arrParametros,
                   function(jsondata) {
                       jqGrid_lista_A.addJSONData(jsondata);
                       parent.fn_unBlockUI();
                       fn_doResize();
                   },
                   function(resultado) {
                       parent.fn_unBlockUI();
                       var error = eval("(" + resultado.responseText + ")");
                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
                   }
    );
    }
}

//****************************************************************
// Funcion		:: 	fn_limpiar
// Descripción	::	Realiza el limipado de campos
// Log			:: 	AEP - 23/11/2012
//****************************************************************
function fn_limpiar() {
    blnPrimeraBusqueda = false;
    $("#txtNroContrato").val('');
    $("#txtcuCliente").val('');
    $("#txtRazonSocialNombre").val('');
    $("#ddlTipoDocumento").val('0');
    $("#ddlEstadoCobro").val('0');
    $("#ddlEstadoPago").val('0');
    $("#txtNroDocumento").val('');
    $("#txtNroLote").val('');
    $("#txtPlaca").val('');
    $("#txtCodigoInfraccion").val('');
    $("#ddlEstadoCobro").val('0');
    $("#ddlCodInfraccion").val('0');
    $("#ddlEstadoPago").val('');
    $("#ddlTipoBien").val('0');
    $("#ddlConcepto").val('0');
    $("#ddlMunicipalidad").val('0');
    $("#jqGrid_lista_A").GridUnload();
    fn_cargaGrilla();
}


//****************************************************************
// Funcion		:: 	fn_abreEditar
// Descripción	::	Abre Editar Registro
// Log			:: 	aep - 23/11/2012
//****************************************************************
function fn_abreConsulta() {
    var id = $("#hddRowId").val();    
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeError("Debe seleccionar una multa.", function() { }, "VALIDACIÓN");
    } else {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
        var strPlaca = rowData.Placa;
        var strCodMulta = rowData.SecMulta;
        var strCheque = rowData.NroCheque;
        var strCodMunicipalidad = rowData.CodMunicipalidad;
        var strEstadoPago = rowData.EstadoPago;
        var strFecTransferencia = rowData.FechaTransferencia;

        fn_util_globalRedirect('/Consultas/MultaVehicular/frmMultaVehicularConsulta.aspx?placa=' + strPlaca + '&tipo=1' + '&codMulta=' + strCodMulta + '&codMunicipalidad=' + strCodMunicipalidad + '&cheque=' + strCheque + '&EstPago=' + strEstadoPago + '&FecTrans=' + strFecTransferencia);
    }
}

//****************************************************************
// Funcion		:: 	fn_Reporte
// Descripción	::	Abre Reporte
// Log			:: 	WCR - 21/01/2013
//****************************************************************
function fn_Reporte() {

    //    var txtNroContrato = $('#txtNroContrato').val() == undefined ? "" : $('#txtNroContrato').val();
    //    var txtCUCliente = $('#txtcuCliente').val() == undefined ? "" : $('#txtcuCliente').val();
    //    var txtRazonSocial = $('#txtRazonSocialNombre').val() == undefined ? "" : $('#txtRazonSocialNombre').val();    
    //    var cmdTipoDocumento = $('#ddlTipoDocumento').val() == "0" ? "" : $('#ddlTipoDocumento').val();
    //    var cmdTipoBien = $('#ddlTipoBien').val() == "0" ? "" : $('#ddlTipoBien').val();
    //    var txtNroDocumento = $('#txtNroDocumento').val() == undefined ? "" : $('#txtNroDocumento').val();    
    //    var txtNroLote = $('#txtNroLote').val() == undefined ? "" : $('#txtNroLote').val();    
    //    var cmdConcepto = $('#ddlConcepto').val() == "0" ? "" : $('#ddlConcepto').val();
    //    var txtPlaca = $('#txtPlaca').val() == undefined ? "" : $('#txtPlaca').val();
    //    var cmdCodInfraccion = $('#ddlCodInfraccion').val() == "0" ? "" : $('#ddlCodInfraccion').val();
    //    var cmdCodigoInfraccion = $('#txtCodigoInfraccion').val() == undefined ? "" : $('#txtCodigoInfraccion').val();
    //    var cmdMunicipalidad = $('#ddlMunicipalidad').val() == "0" ? "" : $('#ddlMunicipalidad').val();    
    //    var cmdEstadoCobro = $('#ddlEstadoCobro').val() == "0" ? "" : $('#ddlEstadoCobro').val();
    //    var cmdEstadoPago = $('#ddlEstadoPago').val() == "0" ? "" : $('#ddlEstadoPago').val();

    //    var strParam = '?filtro=' + txtNroContrato + '|' + txtCUCliente + '|' + txtRazonSocial + '|' + cmdTipoDocumento;
    //    strParam = strParam + '|' + cmdTipoBien + '|' + txtNroDocumento + '|' + txtNroLote + '|' + cmdConcepto + '|' + txtPlaca;
    //    strParam = strParam + '|' + cmdCodInfraccion + '|' + cmdCodigoInfraccion + '|' + cmdMunicipalidad + '|' + cmdEstadoCobro + '|' + cmdEstadoPago;

    //    window.location = "../../Reportes/frmRepMultaVehicular.aspx" + strParam;
    $("#btnGenerar").click();
}

function fn_SetearTipo(pValue, pOpt) {
    if (pOpt == 1) {
        $('#hidMunicipalidad').val(pValue);
    }
    else {
        $('#hidTipoBien').val(pValue);

    }

}