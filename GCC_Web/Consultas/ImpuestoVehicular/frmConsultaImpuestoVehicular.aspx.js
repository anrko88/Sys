//****************************************************************
// Variables Globales
//****************************************************************


var strTipoDocumentoDNI = "1";
var strTipoDocumentoRUC = "2";
var strTipoDocumentoCarnetEx = "3";
var strTipoDocumentoPasaporte = "5";
var strTipoDocumentoOtroDoc = "6";

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

    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscar(true);
        }
    });
    //Carga Grilla
    fn_cargaGrilla();




    //On load Page (siempre al final)
    fn_onLoadPage();


});


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
}


//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 08/11/2012
//****************************************************************
function fn_cargaGrilla() {

    //var mydata = 
    //          [
    //		    { CodSolicitudCredito: "10000001", RazonSocial: " RIPLEY", NroLote: "0000000001", Placa: "xt454s", NroMotor: "xds444", Marca: "Toyota", Modelo: "Yaris",anioFabricacion: "2002",FechaInscripcion: "10/11/2012",Periodo: "2012",Moneda: "NUEVOS SOLES",Importe: "170.00",FechaPago: "16/11/2012",FechaCobro: "17/11/2012",NroCheque: "1000000000000001",SecFinanciamiento: "1"},
    //            { CodSolicitudCredito: "10000002", RazonSocial: " RIPLEY", NroLote: "0000000001", Placa: "xt454s", NroMotor: "xds644", Marca: "Toyota", Modelo: "Yaris",anioFabricacion: "2004",FechaInscripcion: "10/11/2012",Periodo: "2012",Moneda: "NUEVOS SOLES",Importe: "350.00",FechaPago: "16/11/2012",FechaCobro: "17/11/2012",NroCheque: "1000000000000001",SecFinanciamiento: "1"},
    //          	{ CodSolicitudCredito: "10000003", RazonSocial: " RIPLEY", NroLote: "0000000002", Placa: "xt454s", NroMotor: "eds544", Marca: "Toyota", Modelo: "Corolla",anioFabricacion: "2010",FechaInscripcion: "10/11/2012",Periodo: "2012",Moneda: "NUEVOS SOLES",Importe: "170.00",FechaPago: "16/11/2012",FechaCobro: "17/11/2012",NroCheque: "1000000000000001",SecFinanciamiento: "2"},
    //          	{ CodSolicitudCredito: "10000004", RazonSocial: " RIPLEY", NroLote: "0000000002", Placa: "tt454s", NroMotor: "rvds44", Marca: "Nissan", Modelo: "Sunny",anioFabricacion: "2010",FechaInscripcion: "10/11/2012",Periodo: "2012",Moneda: "NUEVOS SOLES",Importe: "180.00",FechaPago: "16/11/2012",FechaCobro: "17/11/2012",NroCheque: "1000000000000001",SecFinanciamiento: "3"},
    //          	{ CodSolicitudCredito: "10000005", RazonSocial: " RIPLEY", NroLote: "0000000003", Placa: "dt454s", NroMotor: "x4s444", Marca: "Nissan", Modelo: "Sunny",anioFabricacion: "2011",FechaInscripcion: "10/11/2012",Periodo: "2012",Moneda: "NUEVOS SOLES",Importe: "80.00",FechaPago: "16/11/2012",FechaCobro: "17/11/2012",NroCheque: "1000000000000001",SecFinanciamiento: "4"},
    //          	{ CodSolicitudCredito: "10000006", RazonSocial: " RIPLEY", NroLote: "0000000003", Placa: "pt454s", NroMotor: "x6s444", Marca: "Nissan", Modelo: "Primera",anioFabricacion: "2003",FechaInscripcion: "10/11/2012",Periodo: "2012",Moneda: "NUEVOS SOLES",Importe: "80.00",FechaPago: "16/11/2012",FechaCobro: "17/11/2012",NroCheque: "1000000000000001",SecFinanciamiento: "4"}

    //		  ];

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_ListarImpuesto();
        },
        // datatype: "local",
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Nro. Contrato', 'Razón Social o Nombre', 'Nº Lote', 'Placa Actual', 'Nº Motor', 'Marca', 'Modelo', 'Año Fabricación', 'F. Inscripción Registral', 'Periodo', 'Nº Cuota', 'Moneda', 'Importe', 'Estado Pago', 'Estado Cobro', 'Nº Cheque', '', '', '', '', '', '', ''],
        colModel: [
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 50, sorttype: "string", align: "center" },
            { name: 'RazonSocial', index: 'RazonSocial', width: 150, sorttype: "string", align: "left" },
        	{ name: 'NroLote', index: 'NroLote', width: 80, align: "center", sorttype: "string" },
		    { name: 'Placa', index: 'Placa', width: 50, align: "center", sorttype: "string" },
		    { name: 'NroMotor', index: 'NroMotor', width: 50, align: "center", sorttype: "string", editable: true },
		    { name: 'Marca', index: 'Marca', width: 60, align: "center" },
        	{ name: 'Modelo', index: 'Modelo', width: 60, align: "center" },
		    { name: 'anioFabricacion', index: 'anioFabricacion', width: 60, align: "center", sorttype: "string" },
		    { name: 'FechaInscripcion', index: 'FechaInscripcion', width: 80, align: "center", sorttype: "string", formatter: fn_util_ValidaStringFecha },
		    { name: 'Periodo', index: 'Periodo', width: 50, align: "center", sorttype: "string" },
        	{ name: 'NroCuota', index: 'NroCuota', width: 50, align: "center", sorttype: "string" },
        	{ name: 'Moneda', index: 'Moneda', width: 50, align: "center", sorttype: "string" },
        	{ name: 'Importe', index: 'Importe', width: 50, align: "right", sorttype: "string", formatter: Fn_util_ReturnValidDecimal2 },
        	{ name: 'EstPago', index: 'EstPago', width: 50, align: "center", sorttype: "string" },
        	{ name: 'EstCobro', index: 'EstCobro', width: 50, align: "center", sorttype: "string" },
        	{ name: 'NroCheque', index: 'NroCheque', width: 50, align: "center", sorttype: "string" },
        	{ name: '', index: '', width: 6 },
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
            { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
            { name: 'SecImpuesto', index: 'SecImpuesto', hidden: true },
        	{ name: 'EstadoPago', index: 'EstadoPago', width: 50, align: "right", sorttype: "string", hidden: true },
        	{ name: 'EstadoCobro', index: 'EstadoCobro', width: 50, align: "right", sorttype: "string", hidden: true },
        	{ name: 'FechaTransferencia', index: 'FechaTransferencia', width: 50, align: "right", sorttype: "string", hidden: true }

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
            //$("#hidSecFinanciamiento").val(rowData.SecFinanciamiento);
            $("#hidCodigoSolicitudCredito").val(rowData.CodSolicitudCredito);
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            var strPlaca = rowData.Placa;
            var strCodImpuesto = rowData.SecImpuesto;
            var strFechaTransferencia = rowData.FechaTransferencia;
            var strCheque = rowData.EstadoPago;
            fn_util_redirect('frmConsultaImpuestoVehicularRegistro.aspx?placa=' + strPlaca + '&tipo=1' + '&codImpuesto=' + strCodImpuesto + '&fecTransferencia=' + strFechaTransferencia + '&cheque=' + strCheque);

        }
    });

    //	for (var i = 0; i <= mydata.length; i++) {
    //	     jQuery("#jqGrid_lista_A").jqGrid('addRowData', i + 1, mydata[i]);
    //	 }

    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    $("#hddRowId").val('');
}

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	AEP - 12/11/2012
//****************************************************************

function fn_buscar(bSearch) {
    blnPrimeraBusqueda = bSearch;
    fn_ListarImpuesto();
}

//****************************************************************
// Funcion		:: 	fn_ListarImpuesto
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	AEP - 12/11/2012
//****************************************************************
function fn_ListarImpuesto() {
    if (!blnPrimeraBusqueda) {
        return;
    } else {


        parent.fn_blockUI();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", intPaginaActual,    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
                             "pNumeroContraro", $("#txtNroContrato").val(),
	                         "pCUCliente", $("#txtcuCliente").val(),
	                         "pRazonSocial", $("#txtRazonSocialNombre").val(),
	                         "pTipoDocumento", $("#ddlTipoDocumento").val(),
	                         "pNumeroDocumento", $("#txtNroDocumento").val(),
	                         "pPlaca", $("#txtPlaca").val(),
	                         "pFechaInscripcionIni", Fn_util_DateToString($("#txtFechaInscripcionDesde").val()),
                             "pFechaInscripcionFin", Fn_util_DateToString($("#txtFechaInscripcionHasta").val()),
                             "pEstadoCobro", $("#ddlEstadoCobro").val(),
    	                     "pEstadoPago", $("#ddlEstadoPago").val(),
                             "pAnioFabricacion", $("#txtAnioFabricacion").val(),
                             "pPeriodo", $("#txtPeriodo").val(),
    	                     "pNroLote", $("#txtNroLote").val()
                             ];

        fn_util_AjaxWM("frmConsultaImpuestoVehicular.aspx/ListaImpuestoVehicular",
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
// Descripción	::	Realiza la limpieza de campos (Listado)
// Log			:: 	AEP - 12/11/2012
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
    $("#txtFechaInscripcionDesde").val(''),
 $("#txtFechaInscripcionHasta").val(''),
 $("#ddlEstadoCobro").val(''),
 $("#ddlEstadoPago").val(''),
 $("#txtAnioFabricacion").val(''),
 $("#txtPeriodo").val('');
    $("#jqGrid_lista_A").GridUnload();
    fn_cargaGrilla();
}


//****************************************************************
// Funcion		:: 	fn_abreConsulta
// Descripción	::	Abre Consulta Registro
// Log			:: 	WCR - 31/01/2013
//****************************************************************
function fn_abreConsulta() {
    var id = $("#hddRowId").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeError("Debe seleccionar un impuesto.", function() { }, "VALIDACIÓN");
    } else {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
        var strPlaca = rowData.Placa;
        var strCodImpuesto = rowData.SecImpuesto;
        var strFechaTransferencia = rowData.FechaTransferencia;
        var strCheque = rowData.EstadoPago;

        fn_util_redirect('frmConsultaImpuestoVehicularRegistro.aspx?placa=' + strPlaca + '&tipo=1' + '&codImpuesto=' + strCodImpuesto + '&fecTransferencia=' + strFechaTransferencia + '&cheque=' + strCheque);
    }
}


//****************************************************************
// Funcion		:: 	fn_abreNuevo
// Descripción	::	Abre Nuevo Registro
// Log			:: 	??? - 02/11/2012
//****************************************************************
//function fn_abreNuevo() {
//    parent.fn_blockUI();
//    fn_util_redirect("frmImpuestoVehicularRegistro.aspx");
//    //parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/ImpuestoVehicular/frmImpuestoVehicularNuevo.aspx", 500, 300, function() { });
//}


//****************************************************************
// Funcion		:: 	fn_abreCargarArchivo
// Descripción	::	Abre Nuevo Registro
// Log			:: 	AEP - 08/11/2012
//****************************************************************
//function fn_abreCargarArchivo() {
//    //parent.fn_util_redirect("frmImpuestoVehicularMasivoResumenCarga.aspx");
//    parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/ImpuestoVehicular/frmImpuestoVehicularCargarMasivo.aspx", 550, 150, function() { });
//}



//****************************************************************
// Funcion		:: 	fn_abreLiquidacion
// Descripción	::	Abre Nuevo Registro
// Log			:: 	AEP - 08/11/2012
//****************************************************************
//function fn_abreLiquidacion() {
//    //parent.fn_util_redirect("frmImpuestoVehicularMasivoResumenCarga.aspx");
//    parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/ImpuestoVehicular/frmImpuestoVehicularLiquidar.aspx", 950, 530, function() { });
//}

//****************************************************************
// Funcion		:: 	fn_abreAsignarCheque
// Descripción	::	Abre una ventana para asignar cheque
// Log			:: 	AEP - 08/11/2012
//****************************************************************
//function fn_abreAsignarCheque() {
//    //parent.fn_util_redirect("frmImpuestoVehicularMasivoResumenCarga.aspx");
//    parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/ImpuestoVehicular/frmImpuestoVehicularAsigCheque.aspx", 550, 220, function() { });
//}

//****************************************************************
// Funcion		:: 	fn_AbrirResumenCarga
// Descripción	::	Redirecciona a la pagina de Resumen de Carga
// Log			:: 	AEP - 13/11/2012
//****************************************************************
function fn_AbrirResumenCarga() {
    fn_util_redirect("frmImpuestoVehicularMasivoResumenCarga.aspx");
}


/*****************************************************************
Funcion		:: 	fn_Reporte
Descripción	::	Genera Reporte
Log			:: 	AEP - 18/01/2013
***************************************************************** */

function fn_Reporte() {

    
    $("#btnGenerar").click();

}