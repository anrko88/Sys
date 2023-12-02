//****************************************************************
// Variables Globales
//****************************************************************
var strComboVacio = "<option value='0'>[-Seleccione-]</option>"

var C_TX_NUEVO = "NUEVO"
var C_TX_EDITAR = "EDITAR"

var blnPrimeraBusqueda = false;
var intPaginaActual = 1;

var strSecImpuesto = '';
//Inicio IBK
var strMunicipalidad = '';
var strTotalAutovaluo = '';
var strTotalPredial = '';
var strPeriodo = '';
//Ins Lote
var MuniLote = '';
var TotalAutovaluoLote = '';
var TotalPredialLote = '';
var PeriodoLote = '';
var Perfil = '';
var strNroLote = '';
var strMontoDescuento = '';
//Fin IBK
var strTieneImpuesto = 'N';
var strCodigosImpuestos = '';

var C_GESTIONBIEN_IMPMUNICIPAL = "003";

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
    //Inicio IBK - AAE
    $('#txtMontoCheque').val(Fn_util_ReturnValidDecimal2($('#txtMontoCheque').val()));
    $('#txtTotalLote').val(Fn_util_ReturnValidDecimal2($('#txtTotalLote').val()));
    $('#txtDevuelto').val(Fn_util_ReturnValidDecimal2($('#txtDevuelto').val()));
    $('#txtReembolsar').val(Fn_util_ReturnValidDecimal2($('#txtReembolsar').val()));
    //Fin IBK - AAE
    $("#txtCodMunicipalidad").focusout(function() {
        var strValor = $(this).val();
        $("#txtMunicipalidadDesc").val("");
    });

    $("#txtMunicipalidadDesc").focusout(function() {
        var strValor = $(this).val();
        $('#txtCodMunicipalidad').val("");
    });
    //CargaGrillas
    fn_configuraGrillaBienes();

    //Busca con Enter
    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscarListarBienes(true);
        }
    });
    //JJM IBK
    Perfil = $("#hddPerfil").val();
    //FIN
    //Oculta ver 	
    //$("#dv_ver").hide(); 	

    //Abre AutoEditar
    var strAbreEditarAuto = $("#hddAbreEditarAuto").val();
    strNroLote = $("#txtNroLoteCarga").val(); //IBK JJM
    if (fn_util_trim(strAbreEditarAuto) == "S") {
        //Inicio IBK JJM
        if (strNroLote != "" && strNroLote != "0") {
            strTieneImpuesto = "X";

            $("#dv_busquedaBienes").show();
            $("#dv_separador").show();
            $("#dv_btnBuscar").show();
            $("#dv_btnGeneraLote").show();
            $("#dv_AgregarImpuesto").show();
            $("#txtCodMunicipalidad").val($("#hddMunicipalidadBien").val());
            $("#txtPeriodo").val($("#hddPeriodoEdita").val());
            $("#dv_ver").hide();

            fn_buscarImpuestosLote();
            fn_CargaDatosLote();
        }
        else {
            strTieneImpuesto = "X";
            fn_editarImpuestoAuto();
            fn_buscarListaBieneEditar();
            //fn_buscarImpuestos();
            $("#dv_busquedaBienes").hide();
            $("#dv_separador").hide();
            $("#dv_btnBuscar").hide();
            $("#dv_btnGeneraLote").hide();
            //Incio IBK
            //$("#dv_AgregarImpuesto").hide();
            $("#dv_AgregarImpuesto").show();
            $("#dv_ver").hide();
            //Fin IBK
        }
        //Fin IBK
    } else {
        $("#hddCodContrato").val("");
        $("#hddCodBien").val("");
        $("#hddCodImpuesto").val("");
        $("#dv_ver").hide();
    }
    //JJM IBK
    $('#imgBsqMunicipalidad').click(function() {
        if ($('#txtCodMunicipalidad').val() == '' && $('#txtMunicipalidadDesc').val() == '') {
            parent.fn_unBlockUI();
            parent.fn_util_AbreModal("Municipalidad", "Comun/frmMunicipalidadesConsulta.aspx?Codigo= " + $('#txtCodMunicipalidad').val() + '&Descripcion= ' + $('#txtMunicipalidadDesc').val(), 800, 600, function() { });
        }
        else {
            var paramArray = ["pCodMunicipalidad", $('#txtCodMunicipalidad').val(),
                          "pMunicipalidad", $('#txtMunicipalidadDesc').val()
                         ];
            fn_util_AjaxWM("frmImpuestoMultaInmuebleRegistro.aspx/ConsultaMunicipalidad",
                       paramArray,
                       /*function(jsondata) {
                           $('#txtCodMunicipalidad').val('');
                           $('#txtMunicipalidadDesc').val('');
                           $('#txtCodMunicipalidad').val(fn_util_trim(jsondata.Items[0].CLAVE1));
                           $('#txtMunicipalidadDesc').val(fn_util_trim(jsondata.Items[0].VALOR1));
                       },*/
                       function(resultado) {
                           var arrResultado = resultado.split("|")
                           if (arrResultado[0] == "0") {
                               parent.fn_unBlockUI();
                               $('#txtCodMunicipalidad').val(fn_util_trim(arrResultado[1]));
                               $('#txtMunicipalidad').val(fn_util_trim(arrResultado[2]));
                           } else {
                               parent.fn_unBlockUI();
                               parent.fn_util_AbreModal("Municipalidad", "Comun/frmMunicipalidadesConsulta.aspx?Codigo= " + $('#txtCodMunicipalidad').val() + '&Descripcion= ' + $('#txtMunicipalidadDesc').val(), 800, 600, function() { });
                           }
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

    //Fin IBK
    //Carga Grilla Impuestos
    fn_configuraGrillaImpuesto();
    //JJM IBK

    //    $("#cmbMunicipalidad").change(function() {
    //        var Option = $('#cmbMunicipalidad option:selected').text();        
    //        if (Option != $("#txtMunicipalidad").val()) {
    //            $("#jqGrid_lista_A").GridUnload();
    //            fn_configuraGrillaBienes();
    //        }
    //    });
    //JJM IBK
    //On load Page (siempre al final)
    fn_onLoadPage();

});


//****************************************************************
// Función		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {


    fn_util_inactivaInput("txtMunicipalidad", "I");
    fn_util_inactivaInput("txtNroLoteCarga", "I");
    $('#txtPeriodo').validText({ type: 'number', length: 4 });
    $('#txtUbicacion').validText({ type: 'comment', length: 100 });
    
    if (fn_util_trim($("#hddTipoTx").val()) == C_TX_NUEVO) {
        $("#txtTotalAutovaluo").validNumber({ value: '0' });
        $("#txtTotalPredial").validNumber({ value: '0' });
    } else {
        $("#txtTotalAutovaluo").validNumber({ value: $("#txtTotalAutovaluo").val() });
        $("#txtTotalPredial").validNumber({ value: $("#txtTotalPredial").val() });        
    }
    $("#hddRowIdImpuesto").val("");
    $("#hddCodigosImpuestos").val("");
    $("#hddTotalAutovaluo").val("");
    $("#hddTotalPredial").val("");
    $("#hddPeriodo").val("");

    //Inicio IBK - AAE
    $("#txtMontoCheque").attr('disabled', 'disabled');
    $("#txtTotalLote").attr('disabled', 'disabled');
    $("#txtDevuelto").attr('disabled', 'disabled');
    $("#txtReembolsar").attr('disabled', 'disabled');
    $("#txtEstadoLote").attr('disabled', 'disabled');
    //Fin IBK - AAE
}


//****************************************************************
// Función		:: 	fn_configuraGrilla
// Descripción	::	Inicializa Grilla Bienes
// Log			:: 	JRC - 25/11/2012
//****************************************************************
function fn_configuraGrillaBienes() {

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_realizaBusquedaBienes();
        },
        jsonReader: {                 // Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage",      // Número de página actual.
            total: "PageCount",       // Número total de páginas.
            records: "RecordCount",   // Total de registros a mostrar.
            repeatitems: false,
            id: "id" // Índice de la columna con la clave primaria.
        },
        //Inicio IBK       
        colNames: ['', '', 'Nº Contrato', 'CU Cliente', 'Tipo Documento', 'Nº Documento', 'Razón Social o Nombre', 'Departamento', 'Provincia', 'Distrito', 'Dirección', 'Municipalidad', 'Estado Bien', 'Estado Contrato', '', ''],
        colModel: [
			{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
			{ name: 'FechaTransferencia', index: 'FechaTransferencia', hidden: true },
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', sortable: true, width: 25, sorttype: "string", align: "center" },
		    { name: 'CodUnico', index: 'CodUnico', sortable: true, width: 25, sorttype: "string", align: "center" },
		    { name: 'DesCodigoTipoDocumento', index: 'DesCodigoTipoDocumento', hidden: true },
		    { name: 'NumeroDocumento', index: 'NumeroDocumento', hidden: true },
		    { name: 'ClienteRazonSocial', index: 'ClienteRazonSocial', sortable: true, sorttype: "left", width: 50, align: "center" },
		    { name: 'DesDepartamento', index: 'DesDepartamento', hidden: true },
		    { name: 'DesProvincia', index: 'DesProvincia', hidden: true },
		    { name: 'DesDistrito', index: 'DesDistrito', hidden: true },
		    { name: 'Ubicacion', index: 'Ubicacion', sortable: false, width: 25, align: "center", sorttype: "string" },
		    { name: 'DesMunicipalidad', index: 'DesMunicipalidad', sortable: false, width: 25, align: "center", sorttype: "string" },
		    { name: 'DesEstadoBien', index: 'DesEstadoBien', sortable: false, width: 25, align: "center", sorttype: "string" },
		    { name: 'DesEstadoContrato', index: 'DesEstadoContrato', sortable: false, width: 25, align: "center", sorttype: "string" },
		    { name: 'CodMunicipalidad', index: 'CodMunicipalidad', hidden: true },
		    { name: '', index: '', width: 5, formatter: fn_fechaTransferencia }
	    ],
        //Fin IBK
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 5,                      // Tamaño de la página
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
            $("#hddCodContrato").val(rowData.CodSolicitudCredito);
            $("#hddCodBien").val(rowData.SecFinanciamiento);
            $("#hddCodUnico").val(rowData.CodUnico);
            $("#hddCodMunicipalidad").val(rowData.CodMunicipalidad);
        }
    });
    $("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 85);

    //**************************************************
    // fn_fechaTransferencia
    //**************************************************
    function fn_fechaTransferencia(cellvalue, options, rowObject) {
        $("#hddFechaTransferencia").val(rowObject.FechaTransferencia);
        return "&nbsp;";
    };

}


//****************************************************************
// Funcion		:: 	fn_buscarListarBienes
// Descripción	::	Busca listado de Bienes
// Log			:: 	JRC - 25/11/2012
//****************************************************************
function fn_buscarListarBienes(pblnBusqueda) {
    $("#hddCodContrato").val("");
    $("#hddCodBien").val("");
    $("#hddCodImpuesto").val("");
    strTieneImpuesto = "N";

    blnPrimeraBusqueda = pblnBusqueda;
    intPaginaActual = 1;
    fn_realizaBusquedaBienes();
}
function fn_realizaBusquedaBienes() {
    if (!blnPrimeraBusqueda) {
        return;
    }
    if ($('#txtCodMunicipalidad').val() != '') {
        try {

            //$('#txtMunicipalidad').val($('#cmbMunicipalidad option:selected').text());

            parent.fn_blockUI();

            var cmbDepartamento = $('#cmbDepartamento').val() == undefined ? "" : $('#cmbDepartamento').val();
            var cmbProvincia = $('#cmbProvincia').val() == undefined ? "" : $('#cmbProvincia').val();
            var cmbDistrito = $('#cmbDistrito').val() == undefined ? "" : $('#cmbDistrito').val();
            var txtUbicacion = $('#txtUbicacion').val() == undefined ? "" : $('#txtUbicacion').val();
            var cmbMunicipalidad = $('#txtCodMunicipalidad').val() == undefined ? "0" : $('#txtCodMunicipalidad').val(); //IBK JJM
            var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación                             
                             "pstrDepartamento", cmbDepartamento,
                             "pstrProvincia", cmbProvincia,
                             "pstrDistrito", cmbDistrito,
                             "pstrUbicacion", txtUbicacion,
                             "pstrMunicipalidad", cmbMunicipalidad
                            ];

            fn_util_AjaxSyncWM("frmImpuestoMultaInmuebleRegistro.aspx/ListaImpuestoMunicipalBienes",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);

                    //LImpia data
                    $("#hddCodContrato").val("");
                    $("#hddCodBien").val("");
                    $("#hddCodImpuesto").val("");
                    $("#hddAbreEditarAuto").val("N");

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
    else { alert("- El filtro Municipalidad es obligatorio. "); }
}






//****************************************************************
// Función		:: 	fn_configuraGrillaImpuesto
// Descripción	::	Inicializa Grilla Impuestos
// Log			:: 	JRC - 25/11/2012
//****************************************************************
var idsOfSelectedRows = [];
function fn_configuraGrillaImpuesto() {
    //var strNroLote = $("#hddNroLote").val(); IBK JJM
    strSecImpuesto = '';
    strMunicipalidad = '';
    strPeriodo = '';
    strTotalAutovaluo = '';
    strTotalPredial = '';

    $("#hddRowIdImpuesto").val("");
    $("#hddCodigosImpuestos").val("");
    $("#hddTotalAutovaluo").val("");
    $("#hddTotalPredial").val("");
    $("#hddPeriodo").val("");

    var updateIdsOfSelectedRows = function(id, isSelected) {

        var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);

        /*
        var strEstadoPago = rowData.EstadoPago;
        //alert(strEstadoPago);
        if( fn_util_trim(strEstadoPago) == "002" ){
        $("#jqGrid_lista_B").jqGrid('setSelection', id, false);
        }else{}
        */
        if (isSelected) {
            strSecImpuesto = strSecImpuesto + rowData.SecImpuesto + ",";
            //Agrega Municipalidades - JJM IBK
            strMunicipalidad = strMunicipalidad + rowData.CodMunicipalidadBien + ",";
            strPeriodo = strPeriodo + rowData.Periodo + ",";
            strTotalAutovaluo = strTotalAutovaluo + rowData.TotalAutovaluo + ",";
            strTotalPredial = strTotalPredial + rowData.TotalPredial + ",";
        } else {
            var lblChekedResult3 = strSecImpuesto.split(",");
            var pstrSecImpuesto = "";
            for (var i = 0; i < lblChekedResult3.length; i++) {
                if (rowData.SecImpuesto != lblChekedResult3[i]) {
                    if (lblChekedResult3[i] != "") {
                        pstrSecImpuesto += lblChekedResult3[i] + ",";
                    }
                }
            }
            strSecImpuesto = pstrSecImpuesto;
        }

        $("#hddRowIdImpuesto").val(id);
        $("#hddCodigosImpuestos").val(strSecImpuesto);
        $("#hddMunicipalidad").val(strMunicipalidad);
        $("#hddTotalAutovaluo").val(strTotalAutovaluo);
        $("#hddTotalPredial").val(strTotalPredial);
        $("#hddPeriodo").val(strPeriodo);


        //Para Paginación (NO TOCAR)
        var index = $.inArray(id, idsOfSelectedRows);
        if (!isSelected && index >= 0) {
            idsOfSelectedRows.splice(index, 1);
        } else if (index < 0) {
            idsOfSelectedRows.push(rowData.SecImpuesto);
        }








    };


    $("#jqGrid_lista_B").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_B", "page");
            if (strNroLote != '' && strNroLote != "0") {
                fn_buscarImpuestosLote();
            }
            else {
                fn_buscarImpuestos();
            }
        },
        jsonReader: {                 // Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage",      // Número de página actual.
            total: "PageCount",       // Número total de páginas.
            records: "RecordCount",   // Total de registros a mostrar.
            repeatitems: false,
            id: "SecImpuesto" // Índice de la columna con la clave primaria.
        },
        colNames: ['', '', '', 'Nro Contrato', 'Dirección', 'Razón Social o Nombre', 'Periodo', 'Cod. Predio', 'Autovaluo', 'Impuesto Predial', 'Arbitrio', 'Multa', 'Fiscalización', 'Importe Total', 'F. Pago', 'Estado Pago', 'F. Cobro', 'Estado Cobro', 'Lote', 'Cheque', 'Municipalidad', 'Docs.', '',
                    '', '', '', ''],
        colModel: [
			{ name: 'SecImpuesto', index: 'SecImpuesto', hidden: true },
			{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
			{ name: 'EstadoPago', index: 'EstadoPago', hidden: true },
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'Ubicacion', index: 'Ubicacion', sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'ClienteRazonSocial', index: 'ClienteRazonSocial', sortable: true, sorttype: "string", width: 25, align: "left" },
		    { name: 'Periodo', index: 'Periodo', sortable: true, sorttype: "string", width: 25, align: "left" },
		    { name: 'CodPredio', index: 'CodPredio', sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'Autovaluo', index: 'Autovaluo', sortable: true, sorttype: "string", width: 25, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'ImpuestoPredial', index: 'ImpuestoPredial', sortable: true, sorttype: "string", width: 25, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'Arbitrio', index: 'Arbitrio', sortable: true, sorttype: "right", width: 25, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'Multa', index: 'Multa', sortable: true, sorttype: "string", width: 25, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'Fiscalizacion', index: 'Fiscalizacion', sortable: true, sorttype: "string", width: 25, align: "right" },
		    { name: 'Total', index: 'Total', sortable: true, sorttype: "string", width: 25, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'FecPago', index: 'FecPago', sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'DesEstadoPago', index: 'DesEstadoPago', sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'FechaCobro', index: 'FechaCobro', sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'DesEstadoCobro', index: 'DesEstadoCobro', sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'NroLote', index: 'NroLote', sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'NroCheque', index: 'NroCheque', sortable: true, sorttype: "string", width: 25, align: "center" },
		    { name: 'DesMunicipalidadBien', index: 'DesMunicipalidadBien', sortable: true, sorttype: "string", width: 25, align: "center" },
            { name: 'Doc', index: 'Doc', align: "center", sortable: false, formatter: fn_abreDocumentos, width: 20 },
            { name: 'TotalAutovaluo', index: 'TotalAutovaluo', hidden: true },
            { name: 'TotalPredial', index: 'TotalPredial', hidden: true },
            { name: 'CodMunicipalidadBien', index: 'CodMunicipalidadBien', hidden: true },
            { name: 'EstadoCobro', index: 'EstadoCobro', hidden: true },
            { name: '', index: '', width: 10 }
	    ],
        height: '100%',
        //pager: '#jqGrid_pager_B',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        //rowNum: 5,
        //rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito',
        sortorder: 'desc',
        viewrecords: true,
        //gridview: true,
        autowidth: true,
        altRows: true,
        multiselect: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: updateIdsOfSelectedRows,
        onSelectAll: function(aRowids, isSelected) {
            var i, count, id;
            for (i = 0, count = aRowids.length; i < count; i++) {
                id = aRowids[i];
                updateIdsOfSelectedRows(id, isSelected);
            }
        },
        //Inicio IBK - AAE
        ondblClickRow: function(id) {            
            parent.fn_blockUI();
           
            var Resultado = '';
            var vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow');
            //            var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', vElementosAEditar);
            var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
            var strEstadoPago = rowData.EstadoPago;
            var strMunicipalidad = rowData.CodMunicipalidadBien;
            //Inicio IBK - AAE
            var Pago = rowData.DesEstadoPago;
            var estCobro = rowData.EstadoCobro;
            var cobrado = "0";
            var pagado = "0";
            var supervisor = "0";
            if (fn_util_trim(Pago) == "Pagado") {
                pagado = "1"
            }
            if ((Perfil == '6') || (Perfil == '11') || (Perfil == '1')) {//Perfil Supervisor Leasginif()
                supervisor = "1";
                if ((fn_util_trim(estCobro) == "C") || (fn_util_trim(estCobro) == "I") || (fn_util_trim(estCobro) == "H")) {
                    cobrado = "1"
                }
            }
            else {//Otros Perfiles
                if ((fn_util_trim(estCobro) == "C") || (fn_util_trim(estCobro) == "I") || (fn_util_trim(estCobro) == "H")) {
                    cobrado = "1";
                }
            }
            if ((supervisor == "0") && ((cobrado == "1") || (pagado == "1"))) {

                var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
                var strCodContrato = rowData.CodSolicitudCredito;
                var strCodBien = rowData.SecFinanciamiento;
                var strCodImpuesto = rowData.SecImpuesto;
                parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Editar", "GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleMnt.aspx?hddCodContrato=" + strCodContrato + "&hddCodBien=" + strCodBien + "&hddCodImpuesto=" + strCodImpuesto + "&strMunicipalidad=" + strMunicipalidad + "&codNroLote=" + strNroLote + "&ReadOnly=Y", 950, 550, function() { });
            } else {//if ((supervisor == "0") && ((cobrado == "1")||(pagado == "1") ) )
            if (supervisor == "1") {
                    parent.fn_unBlockUI();
                    parent.fn_mdl_confirma(Resultado == '' ? "¿Está seguro que desea Editar los Impuestos seleccionados?" : Resultado,
       	        		    function() {
       	        		        var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
       	        		        var strCodContrato = rowData.CodSolicitudCredito;
       	        		        var strCodBien = rowData.SecFinanciamiento;
       	        		        var strCodImpuesto = rowData.SecImpuesto;
       	        		        parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Editar", "GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleMnt.aspx?hddCodContrato=" + strCodContrato + "&hddCodBien=" + strCodBien + "&hddCodImpuesto=" + strCodImpuesto + "&strMunicipalidad=" + strMunicipalidad + "&codNroLote="+ strNroLote + "&ReadOnly=N", 950, 550, function() { });
       	        		    },
       	        		    "Util/images/question.gif",
       	        		    function() {
       	        		        var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
       	        		        var strCodContrato = rowData.CodSolicitudCredito;
       	        		        var strCodBien = rowData.SecFinanciamiento;
       	        		        var strCodImpuesto = rowData.SecImpuesto;
       	        		        parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Editar", "GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleMnt.aspx?hddCodContrato=" + strCodContrato + "&hddCodBien=" + strCodBien + "&hddCodImpuesto=" + strCodImpuesto + "&strMunicipalidad=" + strMunicipalidad + "&codNroLote=" + strNroLote + "&ReadOnly=Y", 950, 550, function() { });
       	        		    },
       	        		    'EDITAR IMPUESTO'
       	        	    );
                } else { //cierra if (supervisor == "1") 
                    var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
                    var strCodContrato = rowData.CodSolicitudCredito;
                    var strCodBien = rowData.SecFinanciamiento;
                    var strCodImpuesto = rowData.SecImpuesto;
                    parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Editar", "GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleMnt.aspx?hddCodContrato=" + strCodContrato + "&hddCodBien=" + strCodBien + "&hddCodImpuesto=" + strCodImpuesto + "&strMunicipalidad=" + strMunicipalidad + "&codNroLote=" + strNroLote + "&ReadOnly=N", 950, 550, function() { });
                } //cierra ELSE (supervisor == "1") 
            }  //ELSE ((supervisor == "0") && ((cobrado == "1")||(pagado == "1") ) )                                       
        }
        //Fin IBK

        /*,afterInsertRow : function(id) { 	
        var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
        var strEstadoPago = rowData.EstadoPago;
        if( fn_util_trim(strEstadoPago) == "002" ){
        //alert(id+"|"+$("#jqGrid_lista_B")[0]);
        //$("#"+id+" td.jqGrid_lista_B_cb ",$("#jqGrid_lista_B")[0]).html('aaa').removeProp('class');				
        $("#jqg_jqGrid_lista_B_"+id).attr('disabled', 'disabled');
        }							    			
        }*/
    });
    $("#jqGrid_lista_B").jqGrid('navGrid', '#jqGrid_pager_B', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_B").hide();

    $("#jqGrid_lista_B").setGridWidth($(window).width() - 85);

    //**************************************************
    // Documentos
    //**************************************************
    function fn_abreDocumentos(cellvalue, options, rowObject) {
        //return ".";
        return "<img src='../../Util/images/ico_docs.gif' alt='Ver Documentos' title='Ver Documentos' width='18px' onclick=\"javascript:fn_GBAbreDocumentos(\'" + rowObject.CodSolicitudCredito + "\',\'" + rowObject.SecFinanciamiento + "\',\'" + rowObject.SecImpuesto + "\',\'" + C_GESTIONBIEN_IMPMUNICIPAL + "\');\" style='cursor:pointer;' />";
    };

}



//****************************************************************
// Funcion		:: 	fn_buscarImpuestoMuni
// Descripción	::	Busca listado de cotizacion por parametros
// Log			:: 	JRC - 07/11/2012
//****************************************************************
function fn_buscarImpuestos() {

    //Limpia datos	
    //$("#hddCodigosImpuestos").val("");		
    //$("#hddRowIdImpuesto").val("");	
    //strSecImpuesto = "";
    $("#jqGrid_lista_B").jqGrid('resetSelection');
    $("#jqGrid_lista_A").jqGrid('resetSelection');

    var strAbreEditarAuto = $("#hddAbreEditarAuto").val();
    if (fn_util_trim(strAbreEditarAuto) != "S") {
        $("#hddCodContrato").val("");
        $("#hddCodBien").val("");
        $("#hddCodImpuesto").val("");
    }


    try {
        parent.fn_blockUI();
        //Inicio IBK - AAE
        strLote = $("#hddNroLote").val();

        if ($("#hidTengoLote").val() != "N") {
            //Cargo info de header de lote
            var arrParametros2 = ["pNroLote", strLote];
            fn_util_AjaxWM("frmImpuestoMultaInmuebleRegistro.aspx/ObtenerInfoLote",
                           arrParametros2,
                           function(jsondata) {
                               $("#txtEstadoLote").val(jsondata.Items[0].DescCodEstadoLote);
                               $("#hidCodEstadoLote").val(jsondata.Items[0].CodEstadoLote);
                               $("#txtTotalLote").val(jsondata.Items[0].TotalLote);
                               $("#txtDevuelto").val(jsondata.Items[0].MontoDevuelto);
                               $("#txtReembolsar").val(jsondata.Items[0].MontoReembolso);
                               $("#txtMontoCheque").val(jsondata.Items[0].MontoCheque);
                               //Formato
                               $('#txtMontoCheque').val(Fn_util_ReturnValidDecimal2($('#txtMontoCheque').val()));
                               $('#txtTotalLote').val(Fn_util_ReturnValidDecimal2($('#txtTotalLote').val()));
                               $('#txtDevuelto').val(Fn_util_ReturnValidDecimal2($('#txtDevuelto').val()));
                               $('#txtReembolsar').val(Fn_util_ReturnValidDecimal2($('#txtReembolsar').val()));
                           },
                           function(resultado) {
                               var error = eval("(" + resultado.responseText + ")");
                               parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
                           }
            );
        }

        //Fin IBK - AAE

        var hddCodContrato = $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();
        var hddCodBien = $('#hddCodBien').val() == undefined ? "" : $('#hddCodBien').val();
        var hddTieneLote = strTieneImpuesto;
        var strMunicipalidad = $('#txtCodMunicipalidad').val() == undefined ? "" : $('#txtCodMunicipalidad').val();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_B", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_B", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_B", "sortorder"), // Criterio de ordenación                             
                             "pstrCodContrato", hddCodContrato,
                             "pstrCodBien", hddCodBien,
                             "pstrTieneLote", fn_util_trim(hddTieneLote) == undefined ? 'N' : hddTieneLote,
                             "pstrMunicipalidad", strMunicipalidad
                            ];

        //alert(arrParametros);                   
        fn_util_AjaxSyncWM("frmImpuestoMultaInmuebleRegistro.aspx/ListaImpuestoMunicipal",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_B.addJSONData(jsondata);
                    for (var i = 0, count = idsOfSelectedRows.length; i < count; i++) {
                        $("#jqGrid_lista_B").jqGrid('setSelection', idsOfSelectedRows[i], false);
                    }
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
// Funcion		:: 	fn_LimpiaComboDistrito
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_LimpiaComboDistrito() {
    $('#cmbDistrito').empty();
    $('#cmbDistrito').html('<option value="0">[- Seleccionar -]</option>');
}

//****************************************************************
// Funcion		:: 	fn_cargaComboProvincia
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_cargaComboProvincia(valor) {

    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbProvincia').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistrito();
}


//****************************************************************
// Funcion		:: 	fn_cargaComboDistrito
// Descripción	::	F
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_cargaComboDistrito(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbDistrito').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}


//****************************************************************
// Funcion		:: 	fn_agregarImpuesto  
// Descripción	::	Agrega Impuesto
// Log			:: 	JRC - 05/11/2012
//****************************************************************
function fn_agregarImpuesto() {

    var hddCodContrato = $("#hddCodContrato").val();
    var hddCodBien = $("#hddCodBien").val();
    var hddCodUnico = $("#hddCodUnico").val();
    var txtTotalAutovaluo = $("#txtTotalAutovaluo").val();
    var txtTotalPredial = $("#txtTotalPredial").val();
    var txtPeriodo = $("#txtPeriodo").val();
    var hddCodMunicipalidad = $("#hddCodMunicipalidad").val();

    //JJM IBK
    //    var Resultado = '';
    //    var Estado = 0;
    //    var EstadoSup = 0;
    //    var rowData1 = $("#jqGrid_lista_B").jqGrid('getRowData');
    //    for (var i = 0; i < rowData1.length; i++) {
    //        var row = rowData1[i];
    //        for (var j = 0; j < vElementosAEditar.length; j++) {
    //            if (row.SecImpuesto == vElementosAEditar[j]) {
    //                if (Perfil == '6') {//Perfil Supervisor Leasginif()
    //                    if (row.EstadoPago == "002" && row.EstadoCobro == "002") {
    //                        Estado = Estado + 1;
    //                        break;
    //                    }
    //                    if (row.EstadoPago == "002" && row.EstadoCobro == "001") {
    //                        EstadoSup = EstadoSup + 1;
    //                        break;
    //                    }
    //                }
    //                else {//Otros Perfiles
    //                    if (row.EstadoPago == "002" || row.EstadoCobro == "002") {
    //                        Estado = Estado + 1;
    //                        break;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //Fin JJM IBK
    //debugger;
    var strError = "";
    var rowData = $("#jqGrid_lista_B").jqGrid('getRowData');
    for (var i = 0; i < rowData.length; i++) {
        var row = rowData[i];
        if (fn_util_trim(row.DesMunicipalidadBien) != fn_util_trim($("#txtMunicipalidadDesc").val())) {
            strError = strError + "&nbsp;&nbsp;- No puede ingresar un nuevo impuesto con otra municipalidad.<br/>";
            break;
        }
    }



    if (fn_util_trim(hddCodBien) == "") {
        parent.fn_mdl_mensajeError("Debe seleccionar un Bien", function() { }, "VALIDACIÓN");
    } else {

        if (fn_util_trim(txtPeriodo) == "") {
            strError = strError + "&nbsp;&nbsp;- Debe ingresar el Periodo <br/>"
        }
        if (fn_util_trim(txtTotalAutovaluo) == "" || fn_util_trim(txtTotalAutovaluo) == "0.00") {
            strError = strError + "&nbsp;&nbsp;- Debe ingresar el Total de Autovaluo <br/>"
        }
        if (fn_util_trim(txtTotalPredial) == "" || fn_util_trim(txtTotalPredial) == "0.00") {
            strError = strError + "&nbsp;&nbsp;- Debe ingresar el Total Predial <br/>"
        }

        if (fn_util_trim(strError) != "") {
            parent.fn_mdl_mensajeError(strError, function() { }, "VALIDACIÓN");
        } else {
            //Inicio IBK - AAE
            var lote = $("#hidTengoLote").val();

            //parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Agregar", "GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleMnt.aspx?hddCodContrato=" + hddCodContrato + "&hddCodBien=" + hddCodBien + "&hddCodUnico=" + hddCodUnico + "&txtTotalAutovaluo=" + txtTotalAutovaluo + "&txtTotalPredial=" + txtTotalPredial + "&txtPeriodo=" + txtPeriodo + "&strMunicipalidad=" + $("#txtCodMunicipalidad").val() + "&strNroLote=" + strNroLote, 950, 550, function() { });
            parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Agregar", "GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleMnt.aspx?hddCodContrato=" + hddCodContrato + "&hddCodBien=" + hddCodBien + "&hddCodUnico=" + hddCodUnico + "&txtTotalAutovaluo=" + txtTotalAutovaluo + "&txtTotalPredial=" + txtTotalPredial + "&txtPeriodo=" + txtPeriodo + "&strMunicipalidad=" + hddCodMunicipalidad + "&strNroLote=" + strNroLote + '&codNroLote=' + lote, 950, 550, function() { });
            //Fin IBK
        }

    }

}

//****************************************************************
// Funcion		:: 	fn_editarImpuesto  
// Descripción	::	Edita Impuesto
// Log			:: 	JRC - 05/11/2012
//****************************************************************
function fn_editarImpuesto() {
    
    var Resultado = '';
    var id = $("#hddRowIdImpuesto").val();

    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "EDITAR IMPUESTO");
    } else {
        var vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow');
        if (vElementosAEditar.length > 1) {
            parent.fn_mdl_mensajeIco("Seleccione un sólo registro.", "util/images/warning.gif", "EDITAR IMPUESTO");
        } else {
            if (vElementosAEditar != "") {

                var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', vElementosAEditar);
                var strEstadoPago = rowData.EstadoPago;
                var strMunicipalidad = rowData.CodMunicipalidadBien;
                //Inicio IBK - AAE
                var Pago = rowData.DesEstadoPago;
                var estCobro = rowData.DesEstadoCobro;
                var cobrado = "0";
                var pagado = "0";
                var supervisor = "0";
                /*
                if (fn_util_trim(strEstadoPago) == "002") {
                parent.fn_mdl_mensajeIco("El impuesto no puede ser editado por su estado de pago", "util/images/warning.gif", "EDITAR IMPUESTO");
                } else {
                var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', vElementosAEditar);
                var strCodContrato = rowData.CodSolicitudCredito;
                var strCodBien = rowData.SecFinanciamiento;
                var strCodImpuesto = rowData.SecImpuesto;
                parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Editar", "GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleMnt.aspx?hddCodContrato=" + strCodContrato + "&hddCodBien=" + strCodBien + "&hddCodImpuesto=" + strCodImpuesto + "&strMunicipalidad=" + strMunicipalidad, 950, 550, function() { });
                }*/
                if (fn_util_trim(Pago) == "Pagado") {
                    pagado = "1"
                }
                if ((Perfil == '6') || (Perfil == '11') || (Perfil == '1')) {//Perfil Supervisor Leasginif()
                    supervisor = "1";
                    if ((fn_util_trim(estCobro) == "C") || (fn_util_trim(estCobro) == "I") || (fn_util_trim(estCobro) == "H")) {
                        cobrado = "1"
                    }
                }
                else {//Otros Perfiles
                    if ((fn_util_trim(estCobro) == "C") || (fn_util_trim(estCobro) == "I") || (fn_util_trim(estCobro) == "H")) {
                        cobrado = "1";
                    }
                }
                if ((supervisor == "0") && ((cobrado == "1") || (pagado == "1"))) {
                    var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', vElementosAEditar);
                    var strCodContrato = rowData.CodSolicitudCredito;
                    var strCodBien = rowData.SecFinanciamiento;
                    var strCodImpuesto = rowData.SecImpuesto;
                    parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Editar", "GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleMnt.aspx?hddCodContrato=" + strCodContrato + "&hddCodBien=" + strCodBien + "&hddCodImpuesto=" + strCodImpuesto + "&strMunicipalidad=" + strMunicipalidad + "&codNroLote=" + strNroLote + "&ReadOnly=Y", 950, 550, function() { });
                } else {//if ((supervisor == "0") && ((cobrado == "1")||(pagado == "1") ) )
                    if (supervisor == "1") {
                        parent.fn_mdl_confirma(Resultado == '' ? "¿Está seguro que desea Editar los Impuestos seleccionados?" : Resultado,
           	        		    function() {
           	        		        var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', vElementosAEditar);
           	        		        var strCodContrato = rowData.CodSolicitudCredito;
           	        		        var strCodBien = rowData.SecFinanciamiento;
           	        		        var strCodImpuesto = rowData.SecImpuesto;
           	        		        parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Editar", "GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleMnt.aspx?hddCodContrato=" + strCodContrato + "&hddCodBien=" + strCodBien + "&hddCodImpuesto=" + strCodImpuesto + "&strMunicipalidad=" + strMunicipalidad + "&codNroLote=" + strNroLote + "&ReadOnly=N", 950, 550, function() { });
           	        		    },
           	        		    "Util/images/question.gif",
           	        		    function() {
           	        		        var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', vElementosAEditar);
           	        		        var strCodContrato = rowData.CodSolicitudCredito;
           	        		        var strCodBien = rowData.SecFinanciamiento;
           	        		        var strCodImpuesto = rowData.SecImpuesto;
           	        		        parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Editar", "GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleMnt.aspx?hddCodContrato=" + strCodContrato + "&hddCodBien=" + strCodBien + "&hddCodImpuesto=" + strCodImpuesto + "&strMunicipalidad=" + strMunicipalidad + "&codNroLote=" + strNroLote + "&ReadOnly=Y", 950, 550, function() { });
           	        		    },
           	        		    'EDITAR IMPUESTO'
           	        	    );
                    } else { //cierra if (supervisor == "1") 
                        var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', vElementosAEditar);
                        var strCodContrato = rowData.CodSolicitudCredito;
                        var strCodBien = rowData.SecFinanciamiento;
                        var strCodImpuesto = rowData.SecImpuesto;
                        parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Editar", "GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleMnt.aspx?hddCodContrato=" + strCodContrato + "&hddCodBien=" + strCodBien + "&hddCodImpuesto=" + strCodImpuesto + "&strMunicipalidad=" + strMunicipalidad + "&codNroLote=" + strNroLote + "&ReadOnly=N", 950, 550, function() { });
                    } //cierra ELSE (supervisor == "1") 
                }  //ELSE ((supervisor == "0") && ((cobrado == "1")||(pagado == "1") ) )                 
                //Fin IBK 

            } else {
                parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "EDITAR IMPUESTO");
            }
        }
    }

}

//****************************************************************
// Funcion		:: 	fn_editarImpuestoAuto
// Descripción	::	Edita Impuesto
// Log			:: 	JRC - 05/11/2012
//****************************************************************
function fn_editarImpuestoAuto() {

    var strCodContrato = $("#hddCodContrato").val();
    var strCodBien = $("#hddCodBien").val();
    var strCodImpuesto = $("#hddCodImpuesto").val();

    if (fn_util_trim(strCodImpuesto) == "") {
        parent.fn_mdl_mensajeError("Impuesto no encontrado", function() { }, "VALIDACIÓN");
    } else {
        parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Editar", "GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleMnt.aspx?hddCodContrato=" + strCodContrato + "&hddCodBien=" + strCodBien + "&hddCodImpuesto=" + strCodImpuesto, 950, 550, function() { });
    }

}


//****************************************************************
// Funcion		:: 	fn_cancelar  
// Descripción	::	Cancela las Operaciones de Cotizacion
// Log			:: 	JRC - 05/11/2012
//****************************************************************
function fn_cancelar() {

    parent.fn_mdl_confirma(
                    "¿Está seguro de Volver?",
                    function() {
                        //fn_EliminaLote();
                        //Inicio IBK - AAE
                        //fn_util_globalRedirect("/GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleListado.aspx");
                        fn_util_globalRedirect("/GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleListado.aspx?NroLote=" + $("#hddNroLote").val());
                        //Fin IBK
                    },
                    "Util/images/question.gif",
                    function() { },
                    'CONFIRMACION'
                   );

}




//****************************************************************
// Funcion		:: 	fn_buscarListaBieneEditar
// Descripción	::	Busca listado de Bienes
// Log			:: 	JRC - 25/11/2012
//****************************************************************
function fn_buscarListaBieneEditar() {
    try {
        parent.fn_blockUI();

        var hddCodBien = $('#hddCodBien').val() == undefined ? "" : $('#hddCodBien').val();
        var hddCodContrato = $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación                             
                             "pstrCodContrato", hddCodContrato,
                             "pstrCodBien", hddCodBien
                            ];

        fn_util_AjaxSyncWM("frmImpuestoMultaInmuebleRegistro.aspx/ListaImpuestoMunicipalBienEditar",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);
                    parent.fn_unBlockUI();

                    //$("#dv_ver").hide();
                    var strFechaTrasferencia = $("#hddFechaTransferencia").val();
                    if (fn_util_trim(strFechaTrasferencia) != "") {
                        $("#dv_eliminar").hide();
                        $("#dv_editar").hide();
                        //$("#dv_ver").show();
                    }

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
// Funcion		:: 	fn_generaLote
// Descripción	::	Genera Lote
// Log			:: 	JRC - 25/11/2012
//****************************************************************
/*Inicio IBK - AAE - Cambio la función, ahora solo calculo totales del lote*/
function fn_generaLote() {

    parent.fn_blockUI();
    var vElementosAEditar = $("#jqGrid_lista_B").jqGrid('getRowData');
    var count = vElementosAEditar.length;
    if (IsNullOrEmpty(count) || count == '0') {
        parent.fn_mdl_mensajeIco("Ingrese al menos un registro para poder generar el lote.", "util/images/warning.gif", "GENERAR LOTE");
    } else {
        var arrParametros = ["strNroLote", strNroLote];

        fn_util_AjaxWM("frmImpuestoMultaInmuebleRegistro.aspx/ReGenerarLote",
    					arrParametros,
    					function(resultado) {
    					    parent.fn_unBlockUI();
    					    parent.fn_mdl_mensajeOk("Se actualizó el Lote Nº" + fn_util_trim(resultado), function() { fn_Redireccion(); }, "GRABADO CORRECTO");
    					},
    					function(resultado) {
    					    var error = eval("(" + resultado.responseText + ")");
    					    parent.fn_mdl_mensajeIco(error.Message, "../../util/images/error.gif", "ERROR GENERAR LOTE");
    					});
    }
}

function fn_generaLote_old() {

    fn_ObtieneImpuestos();
    var vElementosAEditar = $("#jqGrid_lista_B").jqGrid('getRowData');
    var count = vElementosAEditar.length;
    if (IsNullOrEmpty(count)) {
        parent.fn_mdl_mensajeIco("Seleccione al menos un registro para poder generar el lote.", "util/images/warning.gif", "GENERAR LOTE");
    } else {
        parent.fn_mdl_confirma(
		"¿Está seguro que desea generar el Lote?",
		function() {
		    //var strCodigosImpuestos = $("#hddCodigosImpuestos").val() + "0";
		    var strRegeneraLote = "1"; //$("#hddNroLote").val(); == undefined ? "0" : "1";
		    // var strNroLote = $("#hddNroLote").val();
		    var arrParametros = ["pstrCodigosImpuestos", strCodigosImpuestos,
		                         "pRegeneraLote", strRegeneraLote == ' ' ? '0' : strRegeneraLote,
		                         "pNroLote", strNroLote,
		                         "pMunicipalidad", MuniLote,
		                         "pTotalAutovaluo", TotalAutovaluoLote,
		                         "pTotalPredial", TotalPredialLote,
		                         "pPeriodo", PeriodoLote];
		    fn_util_AjaxSyncWM("frmImpuestoMultaInmuebleRegistro.aspx/GeneraLote",
							 arrParametros,
							 function(result) {
							     parent.fn_unBlockUI();
							     if (fn_util_trim(result) == "0") {
							         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL GENERAR LOTE");
							     } else {
							         parent.fn_mdl_mensajeOk("El Lote se generó correctamente. Se generó el Lote Nº" + fn_util_trim(result), function() { fn_util_globalRedirect("/GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleListado.aspx"); }, "GRABADO CORRECTO");
							     }
							 },
							 function(resultado) {
							     parent.fn_unBlockUI();
							     var error = eval("(" + resultado.responseText + ")");
							     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABADO");
							 }
						);
		},
		"Util/images/question.gif",
		function() {
		    $("#hddTotalAutovaluo").val("");
		    $("#hddTotalPredial").val("");
		    $("#hddPeriodo").val("");
		},
		'CONFIRMACION'
	);

    }
}


//    var rows = jQuery("#jqGrid_lista_B").jqGrid('getRowData');

//    var vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow');
//    var count = vElementosAEditar.length;

//    fn_validaPeriodo();
//    fn_validaTotalPredial();
//    fn_validaTotalAutovaluo();
//    if (IsNullOrEmpty(count)) {
//        parent.fn_mdl_mensajeIco("Seleccione al menos un registro para poder generar el lote.", "util/images/warning.gif", "GENERAR LOTE");
//    } else {
//        if (vElementosAEditar.length < 1) {
//            parent.fn_mdl_mensajeIco("Seleccione al menos un registro para poder generar el lote.", "util/images/warning.gif", "GENERAR LOTE");
//        } else {
//            if ($("#hddMunicipalidad").val() > 0) {
//                parent.fn_mdl_mensajeIco("Los items seleccionados deben pertenecer a una misma Municipalidad.", "util/images/warning.gif", "GENERAR LOTE");
//                $("#jqGrid_lista_B").jqGrid('resetSelection');
//                $("#jqGrid_lista_A").jqGrid('resetSelection');
//                $("#hddMunicipalidad").val("");
//                strMunicipalidad = "";
//            } else {
//                if ($("#hddTotalPredial").val() > 0) {
//                    parent.fn_mdl_mensajeIco("Los items seleccionados deben tener el mismo Total Predial.", "util/images/warning.gif", "GENERAR LOTE");

//                    $("#jqGrid_lista_B").jqGrid('resetSelection');
//                    $("#jqGrid_lista_A").jqGrid('resetSelection');
//                    $("#hddTotalPredial").val("");
//                    strTotalPredial = "";
//                }
//                else {
//                    if ($("#hddTotalAutovaluo").val() > 0) {
//                        parent.fn_mdl_mensajeIco("Los items seleccionados deben tener el mismo Total Autovaluo.", "util/images/warning.gif", "GENERAR LOTE");

//                        $("#jqGrid_lista_B").jqGrid('resetSelection');
//                        $("#jqGrid_lista_A").jqGrid('resetSelection');
//                        $("#hddTotalAutovaluo").val("");
//                        strTotalAutovaluo = "";
//                    }
//                    else {
//                        if ($("#hddPeriodo").val() > 0) {
//                            parent.fn_mdl_mensajeIco("Los items seleccionados deben tener el mismo Periodo.", "util/images/warning.gif", "GENERAR LOTE");

//                            $("#jqGrid_lista_B").jqGrid('resetSelection');
//                            $("#jqGrid_lista_A").jqGrid('resetSelection');
//                            $("#hddPeriodo").val("");
//                            strPeriodo = "";
//                        }
//                        else {
//                            $("#hddTotalAutovaluo").val("");
//                            $("#hddTotalPredial").val("");
//                            $("#hddPeriodo").val("");



//    $("#hddTotalAutovaluo").val("");
//    $("#hddTotalPredial").val("");
//    $("#hddPeriodo").val("");
//    strPeriodo = "";
//    strTotalAutovaluo = "";
//    strMunicipalidad = "";
//    strTotalPredial = "";
//}

//****************************************************************
// Funcion		:: 	fn_eliminarImpuesto
// Descripción	::	Busca listado de Bienes
// Log			:: 	JRC - 25/11/2012
//****************************************************************
function fn_eliminarImpuesto() {
    var vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow');
    var count = vElementosAEditar.length;
    //JJM IBK
    var Resultado = '';
    var Estado = 0;
    var EstadoSup = 0;
    //Inicio IBK - AAE
    var pagados = 0;
    var cobrados = 0;
    //Fin IBK
    var rowData1 = $("#jqGrid_lista_B").jqGrid('getRowData');
    for (var i = 0; i < rowData1.length; i++) {
        var row = rowData1[i];
        for (var j = 0; j < vElementosAEditar.length; j++) {
            if (row.SecImpuesto == vElementosAEditar[j]) {
                //Inicio IBK - AAE
                /*if (Perfil == '6') {//Perfil Supervisor Leasginif()
                if (row.EstadoPago == "002" && row.EstadoCobro == "002") {
                Estado = Estado + 1;
                break;
                }
                if (row.EstadoPago == "002" && row.EstadoCobro == "001") {
                EstadoSup = EstadoSup + 1;
                break;
                }
                }
                else {//Otros Perfiles
                if (row.EstadoPago == "002" || row.EstadoCobro == "002") {
                Estado = Estado + 1;
                break;
                }
                }
                }*/
                if (fn_util_trim(row.EstadoPago) == "Pagado") {
                    pagados = pagados + 1;

                }
                if ((fn_util_trim(row.EstadoCobro) == "C") || (fn_util_trim(row.EstadoCobro) == "I") || (fn_util_trim(row.EstadoCobro) == "H")) {
                    cobrados = cobrados + 1;
                }
            }
        } //For
    } //For
    //Fin JJM IBK
    if (IsNullOrEmpty(count)) {
        parent.fn_mdl_mensajeIco("Seleccione al menos un registro para eliminar", "util/images/warning.gif", "ELIMINAR IMPUESTO");
    } else { //cierra if (IsNullOrEmpty(id))
        if (vElementosAEditar.length < 1) {
            parent.fn_mdl_mensajeIco("Seleccione al menos un registro para eliminar.", "util/images/warning.gif", "ELIMINAR IMPUESTO");
        } else { // cierra if (vElementosAEditar.length < 1)
            //Inicio IBK - AAE
            // Si no tengo perfil de supervisor y hay alguno pagado/cobrado
            if (((Perfil != '6') && (Perfil != '1') && (Perfil != '11')) && ((pagados > 0) || (cobrados > 0))) {
                parent.fn_mdl_mensajeIco("Uno de los impuestos no puede ser eliminado por su estado pagado/cobrado.", "util/images/warning.gif", "EDITAR IMPUESTO");
            } else { //cierra if ( ((Perfil != '6') && (Perfil != '1')...
                //Si alguno de los impuestos se encuentra cobrado
                if (cobrados > 0) {
                    parent.fn_mdl_mensajeIco("Uno de los impuestos no puede ser eliminado por su estado cobrado, por favor extorne el cobro antes de eliminarlo.", "util/images/warning.gif", "EDITAR IMPUESTO");
                } else {//cierra if (cobrados > 0)   
                    parent.fn_mdl_confirma(Resultado == '' ? "¿Está seguro que desea eliminar los Impuestos seleccionados?" : Resultado,
						function() {

						    var strCodigosImpuestos = $("#hddCodigosImpuestos").val() + "0";
						    var strNroLoteCarga = $("#txtNroLoteCarga").val()
						    
                            
						    var arrParametros = ["pstrCodigosImpuestos", strCodigosImpuestos,
						                         "pstrNroLote", strNroLoteCarga
						                         ];
						    fn_util_AjaxSyncWM("frmImpuestoMultaInmuebleRegistro.aspx/EliminarImpuestoMunicipal",
								 arrParametros,
								 function(result) {
								     parent.fn_unBlockUI();
								     if (fn_util_trim(result) == "0") {
								         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL ELIMINAR");
								     } else {
								     //parent.fn_mdl_mensajeOk("Los registros seleccionados se eliminaron correctamente.", function() { fn_buscarImpuestos(); }, "ELIMINADO CORRECTO");
								     parent.fn_mdl_mensajeOk("Los registros seleccionados se eliminaron correctamente.", function() { fn_buscarImpuestosLote(); }, "ELIMINADO CORRECTO");    
								     }
								 },
								 function(resultado) {
								     parent.fn_unBlockUI();
								     var error = eval("(" + resultado.responseText + ")");
								     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN ELIMINAR");
								 }
							);



						},
						"Util/images/question.gif",
						function() { },
						'CONFIRMACION'
					); //Parent.fn_mdl_confirma
                }; //cierra else (cobrados > 0) 
            }; //cierra else ( ((Perfil != '6') && (Perfil != '1')... 
            /*
            if (Estado > 0) {
            parent.fn_mdl_mensajeIco("Uno de los impuestos no puede ser eliminado por su estado pagado.", "util/images/warning.gif", "EDITAR IMPUESTO");
            } else {
            if (EstadoSup > 0) {
            Resultado = "Uno de los impuestos no puede ser eliminado por su estado pagado.¿Está seguro que eliminar los items seleccionados?";
            } else {
            parent.fn_mdl_confirma(Resultado == '' ? "¿Está seguro que desea eliminar los Impuestos seleccionados?" : Resultado,
            function() {

						    var strCodigosImpuestos = $("#hddCodigosImpuestos").val() + "0";
            //alert(strCodigosImpuestos);		

						    var arrParametros = ["pstrCodigosImpuestos", strCodigosImpuestos];
            fn_util_AjaxSyncWM("frmImpuestoMultaInmuebleRegistro.aspx/EliminarImpuestoMunicipal",
            arrParametros,
            function(result) {
            parent.fn_unBlockUI();
            if (fn_util_trim(result) == "0") {
            parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL ELIMINAR");
            } else {
            parent.fn_mdl_mensajeOk("Los registros seleccionados se eliminaron correctamente.", function() { fn_buscarImpuestos(); }, "ELIMINADO CORRECTO");
            }
            },
            function(resultado) {
            parent.fn_unBlockUI();
            var error = eval("(" + resultado.responseText + ")");
            parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN ELIMINAR");
            }
            );



						},
            "Util/images/question.gif",
            function() { },
            'CONFIRMACION'
            );

                }*/
            //Fin IBK

        } // cierra else (vElementosAEditar.length < 1)
    } //cierra else (IsNullOrEmpty(id))    
}


//****************************************************************
// Funcion		:: 	fn_listaBusquedaBien
// Descripción	::	Busca listado de Bienes
// Log			:: 	JRC - 25/11/2012
//****************************************************************
function fn_listaBusquedaBien() {
    //var strNroLote = $("#hddNroLote").val(); IBK JJM
    fn_buscarListarBienes(true);
    if (strNroLote != '' && strNroLote != "0") {
        fn_buscarImpuestosLote();
    }
    else { fn_buscarImpuestos(); }
}


//****************************************************************
// Funcion		:: 	fn_verImpuesto
// Descripción	::	Ver Impuesto
// Log			:: 	JRC - 03/12/2012
//****************************************************************
function fn_verImpuesto() {

    var id = $("#hddRowIdImpuesto").val();

    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para visualizar.", "util/images/warning.gif", "EDITAR IMPUESTO");
    } else {
        var vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow');
        if (vElementosAEditar.length > 1) {
            parent.fn_mdl_mensajeIco("Seleccione un sólo registro.", "util/images/warning.gif", "EDITAR IMPUESTO");
        } else {
            if (vElementosAEditar != "") {
                var CodImpuesto = $("#hddCodImpuesto").val();
                if (fn_util_trim(CodImpuesto) == "") {
                    parent.fn_mdl_mensajeError("Debe seleccionar un Impuesto", function() { }, "VALIDACIÓN");
                } else {
                    parent.fn_util_AbreModal("Demanda :: Ver", "GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleMnt.aspx?hddCodContrato=" + $("#hddCodContrato").val() + "&hddCodBien=" + $("#hddCodBien").val() + "&hddCodSiniestro=" + $("#hddCodSiniestro").val() + "&hddCodImpuesto=" + $("#hddCodImpuesto").val() + "&hddVer=1", 950, 550, function() { });
                }
            } else {
                parent.fn_mdl_mensajeIco("Seleccione un registro para visualizar.", "util/images/warning.gif", "EDITAR IMPUESTO");
            }
        }
    }

}



//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos(pstrCodContrato, pstrCodBien, pstrCodRelacionado, pstrCodTipo) {
    var strFechaTransferencia = $("#hddFechaTransferencia").val();
    var strVer = "0";
    if (fn_util_trim(strFechaTransferencia) != "") {
        strVer = "1";
    }

    parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + "&hddVer=" + strVer, 800, 350, function() { });
}
//Inicio IBK JJM
function fn_validaMunicipalidad() {

    var Municipalidad = $("#hddMunicipalidad").val();
    var lblCheckedResult = Municipalidad.split(",");
    var pstrMunicipalidad = 0;
    for (var i = 1; i < lblCheckedResult.length; i++) {
        if (lblCheckedResult[i - 1] != lblCheckedResult[i]) {
            if (lblCheckedResult[i] != "") {
                pstrMunicipalidad = pstrMunicipalidad + 1;
            }
        }
        if (pstrMunicipalidad > 0) { break; }
        else { MuniLote = lblCheckedResult[i - 1]; }
    }
    $("#hddMunicipalidad").val(pstrMunicipalidad);
}
function fn_validaPeriodo() {

    var Periodo = $("#hddPeriodo").val();
    var lblCheckedResult0 = Periodo.split(",");
    var pstrPeriodos = 0;
    for (var i = 1; i < lblCheckedResult0.length; i++) {
        if (lblCheckedResult0[i - 1] != lblCheckedResult0[i]) {
            if (lblCheckedResult0[i] != "") {
                pstrPeriodos = pstrPeriodos + 1;
            }
        }
        if (pstrPeriodos > 0) { break; }
        else { PeriodoLote = lblCheckedResult0[i - 1]; }
    }
    $("#hddPeriodo").val(pstrPeriodos);
}
function fn_validaTotalAutovaluo() {

    var TotalAutovaluo = $("#hddTotalAutovaluo").val();
    var lblCheckedResult1 = TotalAutovaluo.split(",");
    var pstrTotalAutovaluos = 0;
    for (var i = 1; i < lblCheckedResult1.length; i++) {
        if (lblCheckedResult1[i - 1] != lblCheckedResult1[i]) {
            if (lblCheckedResult1[i] != "") {
                pstrTotalAutovaluos = pstrTotalAutovaluos + 1;
            }
        }
        if (pstrTotalAutovaluos > 0) { break; }
        else { TotalAutovaluoLote = lblCheckedResult1[i - 1]; }
    }
    $("#hddTotalAutovaluo").val(pstrTotalAutovaluos);
}
function fn_validaTotalPredial() {

    var TotalPredial = $("#hddTotalPredial").val();
    var lblCheckedResult2 = TotalPredial.split(",");
    var pstrTotalPrediales = 0;
    for (var i = 1; i < lblCheckedResult2.length; i++) {
        if (lblCheckedResult2[i - 1] != lblCheckedResult2[i]) {
            if (lblCheckedResult2[i] != "") {
                pstrTotalPrediales = pstrTotalPrediales + 1;
            }
        }
        if (pstrTotalPrediales > 0) { break; }
        else { TotalPredialLote = lblCheckedResult2[i - 1] }
    }
    $("#hddTotalPredial").val(pstrTotalPrediales);
}
function fn_buscarImpuestosLote() {

    //    $("#jqGrid_lista_B").jqGrid('resetSelection');
    //    $("#jqGrid_lista_A").jqGrid('resetSelection');

    var strAbreEditarAuto = $("#hddAbreEditarAuto").val();
    if (fn_util_trim(strAbreEditarAuto) == "S") {
        $("#hddCodContrato").val("");
        $("#hddCodBien").val("");
        $("#hddCodImpuesto").val("");
    }


    try {
        parent.fn_blockUI();
        //Inicio IBK - AAE
        strLote = $("#txtNroLoteCarga").val();

        if ($("#hidTengoLote").val() != "N") {
            //Cargo info de header de lote
            var arrParametros2 = ["pNroLote", strLote];
            fn_util_AjaxWM("frmImpuestoMultaInmuebleRegistro.aspx/ObtenerInfoLote",
                       arrParametros2,
                       function(jsondata) {
                           $("#txtEstadoLote").val(jsondata.Items[0].DescCodEstadoLote);
                           $("#hidCodEstadoLote").val(jsondata.Items[0].CodEstadoLote);
                           $("#txtTotalLote").val(jsondata.Items[0].TotalLote);
                           $("#txtDevuelto").val(jsondata.Items[0].DevueltoLote);
                           $("#txtReembolsar").val(jsondata.Items[0].ReembolsoLote);
                           $("#txtMontoCheque").val(jsondata.Items[0].MontoCheque);
                           //Formato
                           $('#txtMontoCheque').val(Fn_util_ReturnValidDecimal2($('#txtMontoCheque').val()));
                           $('#txtTotalLote').val(Fn_util_ReturnValidDecimal2($('#txtTotalLote').val()));
                           $('#txtDevuelto').val(Fn_util_ReturnValidDecimal2($('#txtDevuelto').val()));
                           $('#txtReembolsar').val(Fn_util_ReturnValidDecimal2($('#txtReembolsar').val()));
                       },
                       function(resultado) {
                           parent.fn_unBlockUI();
                           var error = eval("(" + resultado.responseText + ")");
                           parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
                       }
        );
        }

        //Fin IBK - AAE
        

        var hddCodContrato = $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();
        var hddCodBien = $('#hddCodBien').val() == undefined ? "" : $('#hddCodBien').val();
        var hddTieneLote = strTieneImpuesto;
        // var strNroLote = $('#hddNroLote').val() == undefined ? "" : $('#hddNroLote').val();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_B", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_B", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_B", "sortorder"), // Criterio de ordenación                             
                             "pstrCodContrato", hddCodContrato,
                             "pstrCodBien", hddCodBien,
                             "pstrTieneLote", 'X',
                             "pstrNroLote", strNroLote
                            ];

        //alert(arrParametros);
        fn_util_AjaxSyncWM("frmImpuestoMultaInmuebleRegistro.aspx/ListaImpuestoMunicipalxLotes",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_B.addJSONData(jsondata);
//                    for (var i = 0, count = idsOfSelectedRows.length; i < count; i++) {
//                        $("#jqGrid_lista_B").jqGrid('setSelection', idsOfSelectedRows[i], false);
//                    }
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
function fn_CargaDatosLote() {

    try {
        parent.fn_blockUI();

        var Periodo = $('#hddPeriodoEdita').val() == undefined ? "" : $('#hddPeriodoEdita').val();
        var Municipalidad = $('#hddMunicipalidadBien').val() == undefined ? "" : $('#hddMunicipalidadBien').val();

        var arrParametros = ["pstrPeriodo", Periodo, "pstrMunicipalidad", Municipalidad];

        fn_util_AjaxSyncWM("frmImpuestoMultaInmuebleRegistro.aspx/GetImpuestoTotalesInmueble",
                arrParametros,
                fn_DatosObtenidos,
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
fn_DatosObtenidos = function(response) {

    var objEImpuesto = response;
    //$("#txtTotalAutovaluo").val(Fn_util_ReturnValidDecimal2(objEImpuesto.TotalAutovaluo));
    $("#txtTotalAutovaluo").val(objEImpuesto.TotalAutovaluo);
    $("#txtTotalPredial").val(objEImpuesto.TotalPredial);
    parent.fn_unBlockUI();
};
//Fin IBK JJM

function fn_ObtieneImpuestos() {
    //
    var rowData = $("#jqGrid_lista_B").jqGrid('getRowData');
    for (var i = 0; i < rowData.length; i++) {
        var row = rowData[i];
        strCodigosImpuestos = strCodigosImpuestos + row.SecImpuesto + ',';
        PeriodoLote = row.Periodo;
        TotalPredialLote = row.TotalPredial;
        TotalAutovaluoLote = row.TotalAutovaluo;
        MuniLote = row.CodMunicipalidad;
    }

    strCodigosImpuestos = strCodigosImpuestos.substring(0, strCodigosImpuestos.length - 1);
}

function fn_EliminaLote() {
    var Lote = $("#txtNroLoteCarga").val();
    var arrParametros = ["pLote", Lote];
    fn_util_AjaxSyncWM("frmImpuestoMultaInmuebleRegistro.aspx/EliminarLote",
            arrParametros,
                        function(resultado) {
                            parent.fn_unBlockUI();
                            var error = eval("(" + resultado.responseText + ")");
                            parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR Al CARGAR");
                        }
            );
}
//Inicio JJM IBK
function fn_obtenerMuncipalidad(pClaveId, pValor1) {
    $('#txtCodMunicipalidad').val(fn_util_trim(pClaveId));
    $('#txtMunicipalidadDesc').val(fn_util_trim(pValor1));
    //Resize Pantalla
    fn_doResize();
}
function fn_EjecutaDescuento() {
    var DataRow = $("#jqGrid_lista_B").jqGrid('getRowData');
    var pagados = 0;
    var cobrados = 0;
    for (var i = 0; i < DataRow.length; i++) {
        var row = DataRow[i];
        for (var j = 0; j < DataRow.length; j++) {
            //if (row.SecImpuesto == vElementosAEditar[j]) {
                if (fn_util_trim(row.EstadoPago) == "Pagado") {
                    pagados = pagados + 1;
                }
                /*if ((fn_util_trim(row.EstadoCobro) == "Cobrado") || (fn_util_trim(row.EstadoCobro) == "Pagado")) {*/
                if ((fn_util_trim(row.EstadoCobro) == "C") || (fn_util_trim(row.EstadoCobro) == "I") || (fn_util_trim(row.EstadoCobro) == "H")) {
                    cobrados = cobrados + 1;
                }
            //}
        } //For
    } //For
    if (DataRow.length == '0') {
        parent.fn_mdl_mensajeIco("No existen Impuestos o Multas para aplicar el descuento.", "util/images/warning.gif", "Aplicar Descuento");
    } else {
        if (((Perfil != '6') && (Perfil != '1') && (Perfil != '11')) && ((pagados > 0) || (cobrados > 0))) {//Perfil Supervisor Leasginif()
            parent.fn_mdl_mensajeIco("No se puede aplicar descuento a uno de los impuestos estado pagado/cobrado.", "util/images/warning.gif", "EDITAR IMPUESTO");

        } else { //cierra 
            //Si alguno de los impuestos se encuentra cobrado
            //            if (cobrados > 0) {
            //                parent.fn_mdl_mensajeIco("No se puede aplicar descuento a uno de los impuestos por su estado cobrado.", "util/images/warning.gif", "EDITAR IMPUESTO");
            //            }
            //            else {
            if ($("#txtDscto").val() > 100 || $("#txtDscto").val() == '') {
                parent.fn_mdl_mensajeIco("El porcentaje de descuento no es válido.", "util/images/warning.gif", "Aplicar Descuento");
            }
            else {
                parent.fn_mdl_confirma(
        		"El descuento se aplicará a todos los impuestos, ¿Está seguro que desea aplicar el descuento?",
        		function() {
        		    parent.fn_blockUI();
        		    var strPorcDescuento = $("#txtDscto").val();
        		    var arrParametros = ["pNroLote", $("#txtNroLoteCarga").val(),
        		                         "pDescuento", strPorcDescuento
        		                         ];
        		    fn_util_AjaxSyncWM("frmImpuestoMultaInmuebleRegistro.aspx/DescuentoLoteImpuestoMunicipal",
        							 arrParametros,
        							 function(result) {
        							     parent.fn_unBlockUI();
        							     if (result == "1") {
        							         parent.fn_mdl_mensajeOk("Se ejecutó el descuento correctamente.", function() { fn_buscarImpuestosLote(); }, "GRABADO CORRECTO");
        							     } else {
        							         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR");
        							     }
        							 },
        							 function(resultado) {
        							     var error = eval("(" + resultado.responseText + ")");
        							     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABADO");
        							 }
        						);
        		},
        		"Util/images/question.gif",
        		function() {
        		},
        		'CONFIRMACION'
        	);
            }
        }
    }
}
//Fin IBK JJM

//Inicio IBK - AAE - Agrego función para anular el lote
function fn_AnularLote() {


    fn_mdl_confirma('¿Está seguro de Anular el lote?',
		function() {

		    parent.fn_blockUI();


		    var Lote = $("#txtNroLoteCarga").val();
		    var arrParametros = ["pLote", Lote];
		    fn_util_AjaxSyncWM("frmImpuestoMultaInmuebleRegistro.aspx/AnularLote",
            arrParametros,
                function(resultado) {
                    parent.fn_unBlockUI();
                    var arrResultado = resultado.split("|");
                    if (arrResultado[0] == '0') {
                        parent.fn_mdl_mensajeOk("Se anuló el lote correctamente", function() { fn_Redireccion(); }, "GRABADO CORRECTO");
                    } else {
                        parent.fn_mdl_mensajeIco(fn_util_trim(arrResultado[1]), "../../util/images/error.gif", "Error Anular Lote");
                    }
                },
				function(resultado) {
				    var error = eval("(" + resultado.responseText + ")");
				    parent.fn_mdl_mensajeIco(error.Message, "../../util/images/error.gif", "ERROR ANULAR LOTE");
				});
		},
		"../../util/images/question.gif",
		function() {
		},
		'IMPUESTO MUNICIPAL'
	);

}


function fn_Redireccion() {
    window.location = "frmImpuestoMultaInmuebleListado.aspx";
}
function fn_obtenerMuni(pCodigo, pDescripcion) {

    //Valores
    var paramArray = ["strCodSolicitudCredito", pCredito];

    fn_util_AjaxWM("frmPagoCuotasRegistro.aspx/ConsultaContrato",
                       paramArray,
                       function(response) {
                           if (response.length == '0') {
                               fn_PonerDatosContrato(response)
                           } else {
                               parent.fn_blockUI();
                               parent.fn_util_AbreModal("Municipalidad", "Comun/frmMunicipalidadesConsulta.aspx&Codigo= " + $('#txtCodMunicipalidad').val() + '&Descripcion= ' + $('#txtMunicipalidadDesc').val(), 800, 600, function() { });
                           };
                       },
                       function(resultado) {
                           parent.fn_unBlockUI();
                           parent.fn_mdl_mensajeIco("Se produjo un error al obtener los datos del contrato", "util/images/error.gif", "ERROR EN CONSULTA");
                       }
    );

    //Resize Pantalla
    fn_doResize();
}