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
var NroLote = '';
var Municipalidad = '';

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
    $("#txtCodMunicipalidad").focusout(function() {
        var strValor = $(this).val();
        $("#txtMunicipalidad").val("");
    });

    $("#txtMunicipalidad").focusout(function() {
        var strValor = $(this).val();
        $('#txtCodMunicipalidad').val("");
    });
    fn_cargarTipoBien();
    //Carga Grilla
    fn_cargaGrilla();


    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscar(true);
        }
    });
    //Inicio JJM IBK
    $('#imgBsqMunicipalidad').click(function() {
        if ($('#txtCodMunicipalidad').val() == '' && $('#txtMunicipalidad').val() == '') {
            parent.fn_unBlockUI();
            parent.fn_util_AbreModal("Municipalidad", "Comun/frmMunicipalidadesConsulta.aspx?Codigo= " + $('#txtCodMunicipalidad').val() + '&Descripcion= ' + $('#txtMunicipalidadDesc').val(), 800, 600, function() { });
        }
        else {
            var paramArray = ["pCodMunicipalidad", $('#txtCodMunicipalidad').val(),
                          "pMunicipalidad", $('#txtMunicipalidad').val()
                         ];
            fn_util_AjaxWM("frmMultaVehicularListado.aspx/ConsultaMunicipalidad",
                       paramArray,
                       function(jsondata) {
                           $('#txtCodMunicipalidad').val('');
                           $('#txtMunicipalidad').val('');
                           $('#txtCodMunicipalidad').val(fn_util_trim(jsondata.Items[0].CLAVE1));
                           $('#txtMunicipalidad').val(fn_util_trim(jsondata.Items[0].VALOR1));
                       },
                       function(resultado) {
                           parent.fn_unBlockUI();
                           parent.fn_mdl_mensajeIco("Se produjo un error al obtener los datos del contrato", "util/images/error.gif", "ERROR EN CONSULTA");
                       }
    );

            //Resize Pantalla    
            fn_doResize();
        }
    });
    //Fin JJM IBK
    //Inicio IBK - AAE - Si tengo lote busco
    if ($('#txtNroLote').val() != '') {
        fn_buscar(true);
    }
    //Fin IBK
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
    //    fn_cargaComboMunicipalidad(strDepartamento, strProvincia);
    //    $("#ddlMunicipalidad").val(fn_util_trim($("#hidCodDistrito").val()));
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

    //var mydata = 
    //          [
    //		    { CodSolicitudCredito: "10000001",CUCliente:"585444445", RazonSocial: " RIPLEY", Municipalidad: "Lima",TipoBien:"Automovil", Placa: "xt454s", Marca: "Toyota", Modelo: "Yaris",Importe: "170.00",FechaPago: "16/11/2012",FechaCobro: "17/11/2012",NroCheque: "1000000000000001",SecFinanciamiento: "1"},
    //            { CodSolicitudCredito: "10000002",CUCliente:"585444445", RazonSocial: " RIPLEY", Municipalidad: "Lima", TipoBien:"Automovil",Placa: "xt454s", Marca: "Toyota", Modelo: "Yaris",Importe: "350.00",FechaPago: "16/11/2012",FechaCobro: "17/11/2012",NroCheque: "1000000000000001",SecFinanciamiento: "1"},
    //          	{ CodSolicitudCredito: "10000003",CUCliente:"585444445", RazonSocial: " RIPLEY", Municipalidad: "Lima", TipoBien:"Automovil",Placa: "xt454s", Marca: "Toyota", Modelo: "Corolla",Importe: "170.00",FechaPago: "16/11/2012",FechaCobro: "17/11/2012",NroCheque: "1000000000000001",SecFinanciamiento: "2"},
    //          	{ CodSolicitudCredito: "10000004",CUCliente:"585444445", RazonSocial: " RIPLEY", Municipalidad: "Lima", TipoBien:"Automovil",Placa: "tt454s", Marca: "Nissan", Modelo: "Sunny",Importe: "180.00",FechaPago: "16/11/2012",FechaCobro: "17/11/2012",NroCheque: "1000000000000001",SecFinanciamiento: "3"},
    //          	{ CodSolicitudCredito: "10000005",CUCliente:"585444445", RazonSocial: " RIPLEY", Municipalidad: "Lima", TipoBien:"Automovil",Placa: "dt454s", Marca: "Nissan", Modelo: "Sunny",Importe: "80.00",FechaPago: "16/11/2012",FechaCobro: "17/11/2012",NroCheque: "1000000000000001",SecFinanciamiento: "4"},
    //          	{ CodSolicitudCredito: "10000006",CUCliente:"585444445", RazonSocial: " RIPLEY", Municipalidad: "Lima", TipoBien:"Automovil",Placa: "pt454s", Marca: "Nissan", Modelo: "Primera",Importe: "80.00",FechaPago: "16/11/2012",FechaCobro: "17/11/2012",NroCheque: "1000000000000001",SecFinanciamiento: "4"}

    //		  ];

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_ListarMulta();
        },
        //datatype: "local",
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Nro. Contrato', 'CU Cliente', 'Razón Social o Nombre', 'Nro. Lote', 'Municipalidad', '', 'Placa Actual', 'Marca', 'Modelo', 'Año Fabricación', 'F. Inscripción Registral', 'Importe', '', '', '', '', 'Estado Pago', 'Estado Cobro', 'Nº Cheque', '', '', '', '', '', '', ''],
        colModel: [
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 50, sorttype: "string", align: "center" },
            { name: 'CodUnico', index: 'CodUnico', width: 50, sorttype: "string", align: "center" },
        	{ name: 'RazonSocial', index: 'RazonSocial', width: 110, align: "left", sorttype: "string" },
        	{ name: 'CodNroLote', index: 'CodNroLote', width: 40, align: "center", sorttype: "string" },
		    { name: 'Municipalidad', index: 'Municipalidad', width: 100, align: "center", sorttype: "string" },
        	{ name: 'TipoBien', index: 'TipoBien', width: 50, align: "center", sorttype: "string", hidden: true },
        	{ name: 'Placa', index: 'Placa', width: 40, align: "center", sorttype: "string" },
		    { name: 'Marca', index: 'Marca', width: 50, align: "center" },
        	{ name: 'Modelo', index: 'Modelo', width: 60, align: "center", hidden: true },
        	{ name: 'Anio', index: 'Anio', width: 40, align: "center", sorttype: "string" },
		    { name: 'FechaInscripcion', index: 'FechaInscripcion', width: 50, align: "center", sorttype: "string", formatter: fn_util_ValidaStringFecha },
		    { name: 'Importe', index: 'Importe', width: 40, align: "center", sorttype: "string" },
		    { name: 'FechaPago', index: 'FechaPago', width: 50, align: "center", sorttype: "string", hidden: true },
        	{ name: 'FechaCobro', index: 'FechaCobro', width: 50, align: "center", sorttype: "string", hidden: true },
        	{ name: 'EstadoPago', index: 'EstadoPago', width: 50, align: "center", sorttype: "string", hidden: true },
        	{ name: 'EstadoCobro', index: 'EstadoCobro', width: 50, align: "center", sorttype: "string", hidden: true },
        	{ name: 'EstPago', index: 'EstPago', width: 40, align: "center", sorttype: "string" },
        	{ name: 'EstCobro', index: 'EstCobro', width: 40, align: "center", sorttype: "string" },
        	{ name: 'NroCheque', index: 'NroCheque', width: 40, align: "center", sorttype: "string" },
        	{ name: '', index: '', width: 3 },
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
            { name: 'CodigoEstadoContrato', index: 'CodigoEstadoContrato', hidden: true },
        	{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
        	{ name: 'CodMunicipalidad', index: 'CodMunicipalidad', hidden: true },
        	{ name: 'SecMulta', index: 'SecMulta', hidden: true },        	
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
            //Inicio IBK - AAE - Cargo el nro de Lote
            NroLote = rowData.CodNroLote;
            //Fin IBK   
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            //Inicio IBK - AAE
            var strPlaca = "";//rowData.Placa;
            //Fin IBK
            var strCodMulta = rowData.SecMulta;
            var strCheque = rowData.NroCheque;
            var strCodMunicipalidad = rowData.CodMunicipalidad;
            var strEstadoPago = rowData.EstadoPago;
            var strFecTransferencia = rowData.FechaTransferencia;
            NroLote = rowData.CodNroLote; // $("#txtNroLote").val();

            if (NroLote != '') {
                fn_util_redirect('frmMultaVehicularRegistro.aspx?placa=' + strPlaca + '&tipo=1' + '&codMulta=' + strCodMulta + '&codMunicipalidad=' + strCodMunicipalidad + '&cheque=' + strCheque + '&EstPago=' + strEstadoPago + '&FecTrans=' + strFecTransferencia + '&strNroLote=' + NroLote);
            }
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
    	                 "pCodMunicipalidad", $("#txtCodMunicipalidad").val(),
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
    $("#txtCodMunicipalidad").val("");  //JJM IBK
    $("#txtMunicipalidad").val("");  //JJM IBK 
    fn_cargaGrilla();
}


//****************************************************************
// Funcion		:: 	fn_abreEditar
// Descripción	::	Abre Editar Registro
// Log			:: 	aep - 23/11/2012
//****************************************************************
function fn_abreEditar() {

    var id = $("#hddRowId").val();
    if (IsNullOrEmpty(id)) {
        //Inicio IBK - AAE - Cargo lote si agrege un valor en nro de lote
        //parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "EDITAR MULTA");
        fn_editarLoteParam()
        //Fin IBK
    } else {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
        //Inicio IBK - AAE
        var strPlaca = ""; //rowData.Placa;
        //Fin IBK
        var strCodMulta = rowData.SecMulta;
        var strCheque = rowData.NroCheque;
        var strCodMunicipalidad = rowData.CodMunicipalidad;
        var strEstadoPago = rowData.EstadoPago;
        var strFecTransferencia = rowData.FechaTransferencia;
        //Inicio IBK - AAE - Se agrega código para edición de lote
        if (NroLote != '') {
            fn_util_redirect('frmMultaVehicularRegistro.aspx?placa=' + strPlaca + '&tipo=1' + '&codMulta=' + strCodMulta + '&codMunicipalidad=' + strCodMunicipalidad + '&cheque=' + strCheque + '&EstPago=' + strEstadoPago + '&FecTrans=' + strFecTransferencia + '&strNroLote=' + NroLote);
        } else {
            parent.fn_mdl_mensajeIco("Seleccione un registro o indique nro de Lote  para poder editar.", "util/images/warning.gif", "EDITAR MULTA");
        }
        // Fin IBK
    }
}



//****************************************************************
// Funcion		:: 	fn_abreNuevo
// Descripción	::	Abre Nuevo Registro
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_abreNuevo() {
    parent.fn_blockUI();
    fn_util_redirect("frmMultaVehicularRegistro.aspx");
    //parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/ImpuestoVehicular/frmImpuestoVehicularNuevo.aspx", 500, 300, function() { });
}


//****************************************************************
// Funcion		:: 	fn_abreCargarArchivo
// Descripción	::	Abre Nuevo Registro
// Log			:: 	AEP - 08/11/2012
//****************************************************************
function fn_abreCargarArchivo() {
    //parent.fn_util_redirect("frmImpuestoVehicularMasivoResumenCarga.aspx");
    parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/MultaVehicular/frmImpuestoVehicularCargarMasivo.aspx", 550, 150, function() { });
}



//****************************************************************
// Funcion		:: 	fn_abreLiquidacion
// Descripción	::	Abre Nuevo Registro
// Log			:: 	AEP - 08/11/2012
//****************************************************************
function fn_abreLiquidacion() {
    //parent.fn_util_redirect("frmMultaVehicularMasivoResumenCarga.aspx");
    parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/MultaVehicular/frmMultaVehicularLiquidar.aspx", 1200, 530, function() { });
}

//****************************************************************
// Funcion		:: 	fn_abreAsignarCheque
// Descripción	::	Abre una ventana para asignar cheque
// Log			:: 	AEP - 08/11/2012
//****************************************************************
function fn_abreAsignarCheque() {
    //parent.fn_util_redirect("frmImpuestoVehicularMasivoResumenCarga.aspx");
    //Inicio IBK - AAE
    //parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/MultaVehicular/frmMultaVehicularAsigCheque.aspx", 550, 210, function() { });
    var strNroLote = $("#txtNroLote").val();
    var id = $("#hddRowId").val();
    if (IsNullOrEmpty(id)) {
        strNroLote = $("#txtNroLote").val();
    } else {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
        strNroLote = rowData.NroLote;
    };
    parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/MultaVehicular/frmMultaVehicularAsigCheque.aspx?NroLote=" + strNroLote, 550, 210, function() { });
    //Fin IBK
}

//Inicio IBK - AAE
//****************************************************************
// Funcion		:: 	fn_editarLoteParam
// Descripción	::	Envio a editar lote
// Log			:: 	AAE - 13/02/2012
//****************************************************************
function fn_editarLoteParam() {

    parent.fn_blockUI();

    var arrParametros = ["pNroLote", $("#txtNroLote").val()];

    fn_util_AjaxWM("frmMultaVehicularListado.aspx/CheckLote",
                   arrParametros,
                   function(resultado) {
                       parent.fn_unBlockUI();
                       if (resultado == "-1") {
                           parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "EDITAR MULTA");
                       } else {
                           fn_util_redirect('frmMultaVehicularRegistro.aspx?tipo=1' + '&strNroLote=' + resultado);
                       }
                   },
                   function(resultado) {
                       parent.fn_unBlockUI();
                       parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "EDITAR MULTA");
                   }
    );
}

//Inicio JJM IBK
function fn_obtenerMuncipalidad(pClaveId, pValor1) {
    $('#txtCodMunicipalidad').val(fn_util_trim(pClaveId));
    $('#txtMunicipalidad').val(fn_util_trim(pValor1));
    //Resize Pantalla
    fn_doResize();
}
//Fin JJM IBK