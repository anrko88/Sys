//****************************************************************
// Variables Globales
//****************************************************************
var strComboVacio = "<option value='0'>[-Seleccione-]</option>"
var strEstadoIns_EnElaboracion = "001";
var strEstadoIns_Wio = "002";
var strEstadoIns_PendEjecucion = "003";
var strEstadoIns_Devuelta = "004";
var strEstadoIns_Anulada = "005";
var strEstadoIns_Aprobada = "006";
//Inicio IBK - AAE - Se agregan estados nuevos
var strEstadoIns_Error = "007";
var strEstadoIns_PagAdmin = "008";
var strEstadoIns_EnEjecucion = "009"
//Fin IBK


//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 19/09/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();

    //Valida Tabs
    $("div#divTabs").tabs({
        show: function(event, ui) {
            fn_doResize();
        }
    });

    //Valida Estado
    fn_validaEstado();   
    //Documentos
    fn_iniGrillaDocumento();

    //Inicializa Grillas
    fn_cargaGrillaDocumentos();
    fn_cargaGrillaProveedores();
    fn_cargaGrillaClientes();
    fn_cargaGrillaSunat();
    fn_cargaGrillaDifCambio();
    fn_cargaGrillaCargos();

    /* Inicio IBK - AAE - Actiavación Parcial*/

    fn_util_SeteaCalendario($("#txtFechavence"));
    fn_util_SeteaCalendario($("#txtFechaMaxActivacion"));

    fn_cargaGrillaDesembolsos();
    fn_cargaGrillaCronograma();

    fn_llenaGrillaCronograma();

    //-------------------------------------------
    //Valida Change del Periodicidad
    //-------------------------------------------
    $('#cmbPeriodicidad').change(function() {
        var strValor = $(this).val();
        fn_oc_periodicidad(strValor);
    });

    //--------------------------------------
    //Valida Change del Tipo Seguro Bien
    //--------------------------------------
    $('#cmbTipoBienSeguro').change(function() {
        var strValor = $(this).val();
        fn_validaTipoSeguroBien(strValor, $('#txtNroCuotas').val());
    });


    //-------------------------------------------
    //Calcula NroCuotas
    //-------------------------------------------
    $("#txtNroCuotas").focusout(function() {
        var strNroCuotas = $("#txtNroCuotas").val();
        if (fn_util_trim(strNroCuotas) == "") strNroCuotas = "0";
        var intNroCuotas = parseInt(strNroCuotas);
        //IBK - RPH se aunmento a 240 cuotas
        if (intNroCuotas <= 0 || intNroCuotas > 240) {
            $("#txtNroCuotas").val("0");
            parent.fn_util_MuestraLogPage("El Nro. de Cuotas ingresado no es correcto", "E");
        } else {
            fn_of_CuotasSeguro();
            fn_of_CuotasSeguroDes();
            fn_of_PlazoGracia();
        }


    });

    //-------------------------------------------
    //Calcula PlazoGracia
    //-------------------------------------------
    $("#txtPlazoGracia").focusout(function() {
        fn_of_PlazoGracia();

    });

    //-------------------------------------------
    //Valida % de TEA
    //-------------------------------------------
    $("#txtTEA").focusout(function() {
        var decTEA = fn_util_ValidaDecimal($('#txtTEA').val());
        if (decTEA >= 100) {
            $('#txtTEA').val(fn_util_ValidaMonto("0", 2));
            parent.fn_util_MuestraLogPage("El Porcentaje de la T.E.A. ingresado no es correcto", "E");
        }
    });



    //-------------------------------------------
    //Valida Change del Tipo Seguro Degravamen
    //-------------------------------------------
    $('#cmbTipoSeguro').change(function() {
        var strValor = $(this).val();
        fn_validaTipoSeguroDegravamen(strValor, $('#txtNroCuotas').val());
    });

    //-------------------------------------------
    //Valida Cuotas Seguro
    //-------------------------------------------
    $("#txtNumCuotasfinanciadas").focusout(function() {
        fn_of_CuotasSeguro();
    });

    //-------------------------------------------
    //Valida Cuotas Seguro Desgravamen
    //-------------------------------------------
    $("#txtNumCuotaFinanciar").focusout(function() {
        fn_of_CuotasSeguroDes();
    });

    $('#chkActivacion').click(function() {
        if ($("#chkActivacion").is(':checked')) {
            if ($("#hddCodigoSubtipoContrato").val() == "001") {
                $("#tab-3").hide();
                $("#tabAct").hide();
                $("#tab-4").show();
                $("#tabCro").show();
            } else {
                $("#tab-3").show();
                $("#tabAct").show();
                $("#tab-4").show();
                $("#tabCro").show();
            }
        } else {
            $("#tab-3").hide();
            $("#tabAct").hide();
            $("#tab-4").hide();
            $("#tabCro").hide();
        }
    });

    /* fin Ibk*/

    //On load Page (siempre al final)
    fn_onLoadPage();

});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_inicializaCampos() {

    fn_util_inactivaInput("txtNroContrato", "I");
    fn_util_inactivaInput("txtCuCliente", "I");
    fn_util_inactivaInput("txtRazonSocial", "I");
    fn_util_inactivaInput("txtNroInstruccion", "I");
    fn_util_inactivaInput("txtTipoContrato", "I");
    fn_util_inactivaInput("txtEstado", "I");
    fn_util_inactivaInput("txtMoneda", "I");

    fn_util_inactivaInput("txtValorVenta", "I");
    fn_util_inactivaInput("txtIgv", "I");
    fn_util_inactivaInput("txtPrecioventa", "I");

    fn_util_inactivaInput("txtTotalDesembolsado", "I");
    fn_util_inactivaInput("txtTotalPagos", "I");
    fn_util_inactivaInput("txtTotalCargos", "I");

    $("#txtTotalDesembolsado").validNumber({ value: $("#txtTotalDesembolsado").val() });
    $("#txtTotalPagos").validNumber({ value: $("#txtTotalPagos").val() });
    $("#txtTotalCargos").validNumber({ value: $("#txtTotalCargos").val() });

    $("#txtTcDia").validNumber({ value: $("#txtTcDia").val() });
    $("#txtTcTicket").validNumber({ value: $("#txtTcTicket").val() });
    $('#txtNroTicket').validText({ type: 'number', length: 5 });

    $("#txtValorVenta").validNumber({ value: $("#txtValorVenta").val() });
    $("#txtIgv").validNumber({ value: $("#txtIgv").val() });
    $("#txtPrecioventa").validNumber({ value: $("#txtPrecioventa").val() });

    //Inicio IBK - AAE  - Cheque e estado del contrato y activación para mostrar check
    var totDesembolso = $("#txtTotalDesembolsado").val();
    if (($("#hddCodigoSubtipoContrato").val() == "001") || (($("#hddCodigoSubtipoContrato").val() == "002") && (parseInt(totDesembolso) == 0))) {
        $('#chkActivacion').attr('disabled', 'disabled');
    }
    //Fin IBK
    if (fn_util_trim($("#hddActivacion").val()) == "1") {
        $('input[name=chkActivacion]').attr('checked', true);
        //Leasing total
        if ($("#hddCodigoSubtipoContrato").val() == "001") {
            $("#tab-3").hide();
            $("#tabAct").hide();
        }
    } else {
        $("#tab-3").hide();
        $("#tabAct").hide();
        $("#tab-4").hide();
        $("#tabCro").hide();
    }


    //Inicio IBK - AAE -  Seteo estado Campos
    /*fn_util_inactivaInput("#txtTotProv", "I");
    fn_util_inactivaInput("#txtTotDtoSUNAT", "I");
    fn_util_inactivaInput("#txtTotAdelantosProv", "I");
    fn_util_inactivaInput("#txtTotAdelantos", "I");
    fn_util_inactivaInput("#txtTotDUAS", "I");
    fn_util_inactivaInput("#txtTotDif", "I");
    fn_util_inactivaInput("#txtTotSUNAT", "I"); */

    $("#txtTotProv").validNumber({ value: $("#txtTotProv").val() });
    $("#txtTotDtoSUNAT").validNumber({ value: $("#txtTotDtoSUNAT").val() });
    $("#txtTotAdelantosProv").validNumber({ value: $("#txtTotAdelantosProv").val() });
    $("#txtTotAdelantos").validNumber({ value: $("#txtTotAdelantos").val() });
    $("#txtTotDUAS").validNumber({ value: $("#txtTotDUAS").val() });
    $("#txtTotDif").validNumber({ value: $("#txtTotDif").val() });
    $("#txtTotSUNAT").validNumber({ value: $("#txtTotSUNAT").val() });

    /* Inicialización Activacion Leasing Parcial */

    fn_util_inactivaInput("txtValorVentaCro", "I");
    fn_util_inactivaInput("txtRiesgoNetoCro", "I");
    fn_util_inactivaInput("txtCuotaInicialCro", "I");
    fn_util_inactivaInput("txtPrecioVentaCro", "I");
    fn_util_inactivaInput("txtMontoIGVCro", "I");
    fn_util_inactivaInput("txtCuotaIniContrato", "I");

    //Setea Campos
    $("#txtPrecioVentaCro").validNumber({ value: '' });
    $("#txtMontoIGVCro").validNumber({ value: '' });
    $("#txtValorVentaCro").validNumber({ value: '' });
    $("#txtCuotaInicialCro").validNumber({ value: '' });
    $("#txtRiesgoNetoCro").validNumber({ value: '' });
    $("#txtCuotaIniContrato").validNumber({ value: '' });
    $("#txtTEA").validNumber({ value: '', decimals: 2, length: 9 });


    $('#txtNroCuotas').validText({ type: 'number', length: 3 });

    $('#txtFechavence').validText({ type: 'date', length: 10 });
    $('#txtFechaMaxActivacion').validText({ type: 'date', length: 10 });

    $("#txtImportePrimaSeguroBien").validNumber({ value: '', decimals: 2 });
    $('#txtNumCuotasfinanciadas').validText({ type: 'number', length: 3 });

    $("#txtImportePrimaDesgravamen").validNumber({ value: '', decimals: 2 });
    $('#txtNumCuotaFinanciar').validText({ type: 'number', length: 3 });

    $("#txtPrecioVentaCro").val(fn_util_ValidaMonto($("#txtPrecioVentaCro").val(), 2));
    $("#txtMontoIGVCro").val(fn_util_ValidaMonto($("#txtMontoIGVCro").val(), 2));
    $("#txtValorVentaCro").val(fn_util_ValidaMonto($("#txtValorVentaCro").val(), 2));
    $("#txtCuotaInicialCro").val(fn_util_ValidaMonto($("#txtCuotaInicialCro").val(), 2));
    $("#txtRiesgoNetoCro").val(fn_util_ValidaMonto($("#txtRiesgoNetoCro").val(), 2));
    $("#txtTEA").val(fn_util_ValidaMonto($("#txtTEA").val(), 2));
    $("#txtImportePrimaSeguroBien").val(fn_util_ValidaMonto($("#txtImportePrimaSeguroBien").val(), 2));
    $("#txtImportePrimaDesgravamen").val(fn_util_ValidaMonto($("#txtImportePrimaDesgravamen").val(), 2));
    $("#txtCuotaIniContrato").val(fn_util_ValidaMonto($("#txtCuotaIniContrato").val(), 2));
    //Clasificacion Bien
    fn_validaTipoSeguroBien($('#cmbTipoBienSeguro').val(), $('#txtNumCuotasfinanciadas').val());

    //Seguro Degravamen
    fn_validaTipoSeguroDegravamen($('#cmbTipoSeguro').val(), $('#txtNumCuotaFinanciar').val());

    //Tipo Persona
    if ($("#hddTipoPersona").val() == "1") {
        $('#fld_SeguroDegravamen').show();
    } else {
        $('#fld_SeguroDegravamen').hide();
    }


    //Fin IBK

}


//****************************************************************
// Funcion		:: 	fn_cargaGrillaDocumentos
// Descripción	::	Inicializa Grilla Documentos
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_cargaGrillaDocumentos() {

    try {

        $("#jqGrid_lista_A").jqGrid({
            datatype: function() {
                fn_cargaAgrupacion();
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
            colNames: ['', 'Tipo Documento', 'Nro. Documento', 'Razón Social o Nombre', 'Cant. Documentos', 'Moneda', 'Importe Total', 'Desc. Sunat', 'Importe Abonar', ''],
            colModel: [
					{ name: 'CodCorrelativo', index: 'CodCorrelativo', width: 3, align: "right", hidden: true },
					{ name: 'TipoDocumento', index: 'NombreProveedor', width: 40, align: "center" },
					{ name: 'NumeroDocumento', index: 'NumeroDocumento', width: 40, align: "center" },
					{ name: 'RazonSocial', index: 'RazonSocial', align: "left" },
					{ name: 'CantidadDocumentos', index: 'CantidadDocumentos', width: 40, align: "center" },
					{ name: 'NombreMoneda', index: 'NombreMoneda', width: 55, align: "center" },
					{ name: 'ImporteAgrupacion', index: 'ImporteAgrupacion', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
					{ name: 'MontoDescSunat', index: 'MontoDescSunat', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
					{ name: 'MontoTotalPago', index: 'MontoTotalPago', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
					{ name: 'AA', index: 'AA', width: 3, align: "right" }
			],
            width: glb_intWidthPantalla - 138,
            height: '100%',
            //pager: '#jqGrid_pager_A',
            loadtext: 'Cargando datos...',
            emptyrecords: 'No hay resultados',
            rowNum: 50,
            //rowList: [10, 20, 30],
            sortname: 'Codigocotizacion',
            sortorder: 'desc',
            viewrecords: true,
            //gridview: true,
            //autowidth: true,
            altRows: true,
            loadonce: false,
            altclass: 'gridAltClass',
            ondblClickRow: function(id) { },
            afterInsertRow: function(id) {
                var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
                if (parseInt(rowData.CantidadDocumentos) == 0) {
                    $("#" + id + " td.sgcollapsed", $("#jqGrid_lista_A")[0]).unbind('click').html('').removeProp('class');
                }
            },
            subGrid: true,
            subGridOptions: {
                "openicon": "ui-icon-arrowreturn-1-e"
            },
            subGridRowExpanded: function(subgrid_id, row_id) {
                var rowDataPadre = $("#jqGrid_lista_A").jqGrid('getRowData', row_id);
                $("#hddCodigoAgrupacion").val(rowDataPadre.CodCorrelativo);

                var subgrid_table_id, pager_id;
                subgrid_table_id = subgrid_id + "_t";
                pager_id = "p_" + subgrid_table_id;
                $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pager_id + "' class='scroll'></div>");
                jQuery("#" + subgrid_table_id).jqGrid({
                    datatype: function() {
                        fn_cargaAgrupacionDocumento(subgrid_table_id);
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
                    //Inicio IBK - AAE - Agrego neto a pagar en moneda contrato
                    //colNames: ['Tipo Comprobante', 'Nro.Comprobante', 'Fecha Emisión', 'TC IBK', 'TC Sunat', 'Importe', 'Descuentos', 'Importe Total'],
                    colNames: ['Tipo Comprobante', 'Nro.Comprobante', 'Fecha Emisión', 'TC IBK', 'TC Sunat', 'Importe', 'Imp Mon Contrato', 'Descuentos', 'Importe Total'],
                    colModel: [
            { name: 'NombreTipoDocumento', index: 'NombreTipoDocumento', align: "left" },
						{ name: 'NroDocumento', index: 'NroDocumento', width: 50, align: "center" },
						{ name: 'FechaEmision', FechaEmision: 'TCDia', width: 35, align: "center" },
						{ name: 'TipoCambioIBK', index: 'TipoCambioIBK', width: 35, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
						{ name: 'TCSBS', index: 'TCSBS', width: 35, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
						{ name: 'Total', index: 'Total', width: 50, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
						{ name: 'TotalMonCont', index: 'TotalMonCont', width: 55, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
						{ name: 'DctoSunat', index: 'DctoSunat', width: 50, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
						{ name: 'TotalDesembolsar', index: 'TotalDesembolsar', width: 60, align: "right", formatter: Fn_util_ReturnValidDecimal2 }
                    /*{ name: 'NombreTipoDocumento', index: 'NombreTipoDocumento', align: "left" },
                    { name: 'NroDocumento', index: 'NroDocumento', width: 70, align: "center" },
                    { name: 'FechaEmision', FechaEmision: 'TCDia', width: 50, align: "center" },
                    { name: 'TipoCambioIBK', index: 'TipoCambioIBK', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                    { name: 'TCSBS', index: 'TCSBS', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                    { name: 'Total', index: 'Total', width: 50, align: "right", formatter: Fn_util_ReturnValidDecimal2 },						
                    { name: 'DctoSunat', index: 'DctoSunat', width: 60, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                    { name: 'TotalDesembolsar', index: 'TotalDesembolsar', width: 60, align: "right", formatter: Fn_util_ReturnValidDecimal2 }*/
					],
                    width: glb_intWidthPantalla - 175,
                    height: '100%',
                    loadtext: 'Cargando datos...',
                    emptyrecords: 'No hay resultados',
                    rowNum: 999,
                    rowList: [10, 20, 30],
                    sortname: 'CodigoContrato',
                    sortorder: 'desc',
                    viewrecords: true,
                    gridview: true,
                    //autowidth: true,
                    altRows: true,
                    altclass: 'gridAltClass',
                    multiselect: false,
                    gridComplete: function(id) {
                        fn_doResize();
                    }
                });

            }

        });
        jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_lista_A', { edit: false, add: false, del: false });
        $("#search_jqGrid_lista_A").hide();

    } catch (e) {
        alert(e.message);
    }

}


//****************************************************************
// Funcion		:: 	fn_cargaGrillaProveedores
// Descripción	::	Inicializa Grilla 
// Log			:: 	JRC - 20/09/2012
//****************************************************************
function fn_cargaGrillaProveedores() {

    $("#jqGrid_lista_B").jqGrid({
        datatype: function() {
            fn_listaCargoAbono("B");
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
        //Inicio IBK - AAE - Agrego Columna Estado Ejecución del pago
        //colNames: ['Razón Social','Cant. Doc','Moneda','Total Importe','Desc. Sunat','Adelanto','Importe Total','Medio Pago','_','','','',''],
        colNames: ['Razón Social', 'Cant. Doc', 'Moneda', 'Total Importe', 'Desc. Sunat', 'Adelanto', 'Neto a Pagar', 'Neto Moneda Contrato', 'Medio Pago', '_', '', '', '', '', 'Estado Pago', 'CodCorrelativo', '_'],
        colModel: [
		        { name: 'RazonSocial', index: 'RazonSocial', align: "left" },
		        { name: 'CantidadDocumentos', index: 'CantidadDocumentos', width: 40, align: "center" },
		        { name: 'NombreMoneda', index: 'NombreMoneda', width: 60, align: "center" },
		        { name: 'ImporteAgrupacion', index: 'ImporteAgrupacion', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MontoDescSunat', index: 'MontoDescSunat', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MontoAdelanto', index: 'MontoAdelanto', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'MontoTotalPago2', index: 'MontoTotalPago2', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'MontoMonContrato', index: 'MontoMonContrato', width: 40, align: "right", weightfont: "bold", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MedioAbono', index: 'MedioAbono', width: 40, align: "center" },
		        { name: '', index: '', width: 35, align: "center", formatter: fn_medioPagoB },
		        { name: 'codagrupacion', index: 'codagrupacion', hidden: true },
		        { name: 'CodProveedor', index: 'CodProveedor', hidden: true },
		        { name: 'CodMonedaPago', index: 'CodMonedaPago', hidden: true },
            { name: 'MontoTotalPago', index: 'MontoTotalPago', hidden: true },
            { name: 'EstadoEjecucionPago', index: 'EstadoEjecucionPago', width: 60, align: "center" },
            { name: 'CodCorrelativo', index: 'CodCorrelativo', hidden: true },
            { name: '', index: '', width: 35, align: "center", formatter: fn_Voucher }
        //fin IBK

        ],
        //width: glb_intWidthPantalla-170,
        width: 950,
        height: '100%',
        //pager: '#jqGrid_pager_B',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 50,
        //rowList: [10, 20, 30],
        sortname: 'Codigocotizacion',
        sortorder: 'desc',
        viewrecords: true,
        //gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) { },
        ondblClickRow: function(id) { }
    });
    jQuery("#jqGrid_lista_B").jqGrid('navGrid', '#jqGrid_lista_B', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_B").hide();


    //**************************************************
    // MedioPago
    //**************************************************
    function fn_medioPagoB(cellvalue, options, rowObject) {
        var strModoVer = $("#hddModoVer").val();
        var strParam = "'" + rowObject.CodSolicitudCredito + "','" + rowObject.CodInstruccionDesembolso + "','" + rowObject.codagrupacion + "','" + rowObject.CodProveedor + "','" + rowObject.CodMonedaPago + "','B','" + rowObject.codunico + "','" + strModoVer + "','" + rowObject.ImporteAgrupacion + "'";
        //
        if (strModoVer == "1") {
            if (rowObject.MedioAbono == '') {
                return ".";
            } else {
                return "<img src='../Util/images/ico_mdl_checklist.gif' alt='" + cellvalue + "' title='Medio Pago' width='18px' onclick=\"javascript:fn_abreMedioPago(" + strParam + ");\" style='cursor:pointer;'/>";

            }

        } else {
            return "<img src='../Util/images/ico_acc_agregar.gif' alt='" + cellvalue + "' title='Agregar Adelanto' width='16px' onclick=\"javascript:fn_abreNuevoAdelanto(" + strParam + ");\" style='cursor:pointer;'/>&nbsp;&nbsp;<img src='../Util/images/ico_mdl_checklist.gif' alt='" + cellvalue + "' title='Medio Pago' width='18px' onclick=\"javascript:fn_abreMedioPago(" + strParam + ");\" style='cursor:pointer;'/>";
        }

    };

    //**************************************************
    // Voucher
    //**************************************************
    function fn_Voucher(cellvalue, options, rowObject) {
        var strParam = "'" + rowObject.CodSolicitudCredito + "','" + rowObject.CodInstruccionDesembolso + "','" + rowObject.CodCorrelativo + "'";
        //debugger;
        $("#hddCorrelativo").val(rowObject.CodCorrelativo);
        return "<img src='../Util/images/ico_mdl_checklist.gif' title='Voucher' width='18px' onclick=\"javascript:fn_abreVoucher(" + strParam + ");\"/>";
    };


}

//****************************************************************
// Funcion		:: 	fn_cargaGrillaClientes
// Descripción	::	Inicializa Grilla 
// Log			:: 	JRC - 20/09/2012
//****************************************************************
function fn_cargaGrillaClientes() {

    $("#jqGrid_lista_C").jqGrid({
        datatype: function() {
            fn_listaCargoAbono("C");
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
        //Inicio IBK - AAE - Agrego Estado pago
        //colNames: ['Moneda','Importe a abonar','Medio Pago','_'],
        colNames: ['Moneda', 'Importe a abonar', 'Neto Moneda Contrato', 'Medio Pago', '_', 'Estado Pago'],
        colModel: [
		        { name: 'NombreMoneda', index: 'NombreMoneda', width: 60, align: "center" },
		        { name: 'MontoTotalPago', index: 'MontoTotalPago', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MontoMonContrato', index: 'MontoMonContrato', width: 40, align: "right", weightfont: "bold", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MedioAbono', index: 'MedioAbono', width: 40, align: "center" },
		        { name: '', index: '', width: 25, align: "center", formatter: fn_medioPagoC },
		        { name: 'EstadoEjecucionPago', index: 'EstadoEjecucionPago', width: 60, align: "center" }
        //Fin IBK        
        ],
        //width: glb_intWidthPantalla-170,
        width: 500,
        height: '100%',
        //pager: '#jqGrid_pager_C',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 50,
        //rowList: [10, 20, 30],
        sortname: 'Codigocotizacion',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) { },
        ondblClickRow: function(id) { }
    });
    jQuery("#jqGrid_lista_C").jqGrid('navGrid', '#jqGrid_lista_C', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_C").hide();

    //**************************************************
    // MedioPago
    //**************************************************
    function fn_medioPagoC(cellvalue, options, rowObject) {
        var strModoVer = $("#hddModoVer").val();
        var strParam = "'" + rowObject.CodSolicitudCredito + "','" + rowObject.CodInstruccionDesembolso + "','" + rowObject.codagrupacion + "','" + rowObject.CodProveedor + "','" + rowObject.CodMonedaPago + "','C','" + rowObject.codunico + "','" + strModoVer + "'";

        if (strModoVer == "1") {

            //return ".";
            if (rowObject.MedioAbono == '') {
                return ".";
            } else {
                return "<img src='../Util/images/ico_mdl_checklist.gif' alt='" + cellvalue + "' title='Medio Pago' width='18px' onclick=\"javascript:fn_abreMedioPago(" + strParam + ");\" style='cursor:pointer;'/>";
            }

        } else {
            return "<img src='../Util/images/ico_mdl_checklist.gif' alt='" + cellvalue + "' title='Medio Pago' width='18px' onclick=\"javascript:fn_abreMedioPago(" + strParam + ");\" style='cursor:pointer;'/>&nbsp;<img src='../Util/images/ico_acc_eliminar.gif' alt='" + cellvalue + "' title='Eliminar Adelanto' width='18px' onclick=\"javascript:fn_eliminaAbono(" + strParam + ");\" style='cursor:pointer;'/>";
        }

    };
}


//****************************************************************
// Funcion		:: 	fn_cargaGrillaSunat
// Descripción	::	Inicializa Grilla 
// Log			:: 	JRC - 20/09/2012
//****************************************************************
function fn_cargaGrillaSunat() {

    $("#jqGrid_lista_D").jqGrid({
        datatype: function() {
            fn_listaCargoAbono("D");
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
        //Inicio IBK - AAE - Agrego columna estado pago
        //colNames: ['Tipo','Razón Social','Número','Moneda','Importe','Importe S/.','Tc Sunat','Medio Pago','_'],
        colNames: ['Tipo', 'Razón Social', 'Número', 'Moneda', 'Importe', 'Importe S/.', 'Neto Moneda Contrato', 'Tc Sunat', 'Medio Pago', '_', 'Estado Pago'],
        colModel: [
		        { name: 'TipoAgrupacion', index: 'TipoAgrupacion', align: "left", width: 80 },
		        { name: 'RazonSocialSunat', index: 'RazonSocialSunat', align: "left" },
		        { name: 'NroDocumentoDUA', index: 'NroDocumentoDUA', width: 60, align: "center" },
		        { name: 'NombreMoneda', index: 'NombreMoneda', width: 60, align: "center" },
		        { name: 'MontoTotalPago', index: 'MontoTotalPago', width: 40, align: "center", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MontoSunat', index: 'MontoSunat', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MontoMonContrato', index: 'MontoMonContrato', width: 40, align: "right", weightfont: "bold", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'tcsunat', index: 'tcsunat', width: 40, align: "center" },
		        { name: 'MedioAbono', index: 'MedioAbono', width: 40, align: "center" },
		        { name: '', index: '', width: 15, align: "center", formatter: fn_medioPagoD },
		        { name: 'EstadoEjecucionPago', index: 'EstadoEjecucionPago', width: 60, align: "center" }
        //Fin IBK
        ],
        //width: glb_intWidthPantalla-170,
        width: 950,
        height: '100%',
        //pager: '#jqGrid_pager_D',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 50,
        //rowList: [10, 20, 30],
        sortname: 'Codigocotizacion',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) { },
        ondblClickRow: function(id) { }
    });
    jQuery("#jqGrid_lista_D").jqGrid('navGrid', '#jqGrid_lista_D', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_D").hide();

    //**************************************************
    // MedioPago
    //**************************************************
    function fn_medioPagoD(cellvalue, options, rowObject) {
        var strModoVer = $("#hddModoVer").val();
        var strParam = "'" + rowObject.CodSolicitudCredito + "','" + rowObject.CodInstruccionDesembolso + "','" + rowObject.codagrupacion + "','" + rowObject.CodProveedor + "','" + rowObject.CodMonedaPago + "','D','" + rowObject.codunico + "','" + strModoVer + "'";

        if (strModoVer == "1") {
            //return ".";
            if (rowObject.MedioAbono == '') {
                return ".";
            } else {
                return "<img src='../Util/images/ico_mdl_checklist.gif' alt='" + cellvalue + "' title='Medio Pago' width='18px' onclick=\"javascript:fn_abreMedioPago(" + strParam + ");\" style='cursor:pointer;'/>";

            }

        } else {
            return "<img src='../Util/images/ico_mdl_checklist.gif' alt='" + cellvalue + "' title='Medio Pago' width='18px' onclick=\"javascript:fn_abreMedioPago(" + strParam + ");\" style='cursor:pointer;'/>";
        }

    };

}


//****************************************************************
// Funcion		:: 	fn_cargaGrillaDifCambio
// Descripción	::	Inicializa Grilla 
// Log			:: 	JRC - 20/09/2012
//****************************************************************
function fn_cargaGrillaDifCambio() {

    $("#jqGrid_lista_E").jqGrid({
        datatype: function() {
            fn_listaCargoAbono("E");
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
        //Inicio IBK - AAE - Agrego columna estado pago
        //colNames: ['Razón Social','Moneda','Importe a abonar','Medio Pago','_'],
        colNames: ['Razón Social', 'Moneda', 'Importe a abonar', 'Neto Moneda Contrato', 'Medio Pago', '_', 'Estado Pago'],
        colModel: [
		        { name: 'RazonSocial', index: 'RazonSocial', align: "left" },
		        { name: 'NombreMoneda', index: 'NombreMoneda', width: 40, align: "center" },
		        { name: 'MontoTotalPago', index: 'MontoTotalPago', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MontoMonContrato', index: 'MontoMonContrato', width: 40, align: "right", weightfont: "bold", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MedioAbono', index: 'MedioAbono', width: 40, align: "center" },
		        { name: '', index: '', width: 15, align: "center", formatter: fn_medioPagoE },
		        { name: 'EstadoEjecucionPago', index: 'EstadoEjecucionPago', width: 60, align: "center" }
        // Fin IBK
        ],
        //width: glb_intWidthPantalla-170,
        width: 600,
        height: '100%',
        //pager: '#jqGrid_pager_E',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 50,
        //rowList: [10, 20, 30],
        sortname: 'Codigocotizacion',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) { },
        ondblClickRow: function(id) { }
    });
    jQuery("#jqGrid_lista_E").jqGrid('navGrid', '#jqGrid_lista_E', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_E").hide();

    //**************************************************
    // MedioPago
    //**************************************************
    function fn_medioPagoE(cellvalue, options, rowObject) {
        var strModoVer = $("#hddModoVer").val();
        var strParam = "'" + rowObject.CodSolicitudCredito + "','" + rowObject.CodInstruccionDesembolso + "','" + rowObject.codagrupacion + "','" + rowObject.CodProveedor + "','" + rowObject.CodMonedaPago + "','E','" + rowObject.codunico + "','" + strModoVer + "'";

        if (strModoVer == "1") {
            //return ".";
            if (rowObject.MedioAbono == '') {
                return ".";
            } else {
                return "<img src='../Util/images/ico_mdl_checklist.gif' alt='" + cellvalue + "' title='Medio Pago' width='18px' onclick=\"javascript:fn_abreMedioPago(" + strParam + ");\" style='cursor:pointer;'/>";

            }

        } else {
            return "<img src='../Util/images/ico_mdl_checklist.gif' alt='" + cellvalue + "' title='Medio Pago' width='18px' onclick=\"javascript:fn_abreMedioPago(" + strParam + ");\" style='cursor:pointer;'/>";
        }

    };

}


//****************************************************************
// Funcion		:: 	fn_cargaGrillaCargos
// Descripción	::	Inicializa Grilla 
// Log			:: 	JRC - 20/09/2012
//****************************************************************
function fn_cargaGrillaCargos() {

    $("#jqGrid_lista_F").jqGrid({
        datatype: function() {
            fn_listaCargoAbono("F");
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
        //Inicio IBK - AAE - Agrego columna estado pago
        //colNames: ['Concepto','Moneda','Porc.(%)','Importe a abonar','Medio Pago','_'],
        colNames: ['Concepto', 'Moneda', 'Porc.(%)', 'Importe a abonar', 'Neto Moneda Contrato', 'Medio Pago', '_', 'Estado Pago'],
        colModel: [
		        { name: 'NombreConceptoCargo', index: 'NombreConceptoCargo', align: "left" },
		        { name: 'NombreMoneda', index: 'NombreMoneda', width: 60, align: "center" },
		        { name: 'PorcCalculo', index: 'PorcCalculo', width: 40, align: "center", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MontoTotalPago', index: 'MontoTotalPago', width: 60, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MontoMonContrato', index: 'MontoMonContrato', width: 40, align: "right", weightfont: "bold", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MedioAbono', index: 'MedioAbono', width: 40, align: "center" },
		        { name: '', index: '', width: 35, align: "center", formatter: fn_medioPagoF },
		        { name: 'EstadoEjecucionPago', index: 'EstadoEjecucionPago', width: 60, align: "center" }
        // Fin IBK
        ],
        //width: glb_intWidthPantalla-170,
        width: 700,
        height: '100%',
        //pager: '#jqGrid_pager_F',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 50,
        //rowList: [10, 20, 30],
        sortname: 'Codigocotizacion',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) { },
        ondblClickRow: function(id) { }
    });
    jQuery("#jqGrid_lista_F").jqGrid('navGrid', '#jqGrid_lista_F', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_F").hide();

    //**************************************************
    // MedioPago
    //**************************************************
    function fn_medioPagoF(cellvalue, options, rowObject) {
        var strModoVer = $("#hddModoVer").val();
        var strParam = "'" + rowObject.CodSolicitudCredito + "','" + rowObject.CodInstruccionDesembolso + "','" + rowObject.codagrupacion + "','" + rowObject.CodProveedor + "','" + rowObject.CodMonedaPago + "','F','" + rowObject.codunico + "','" + strModoVer + "'";

        if (strModoVer == "1") {
            //return ".";

            if (rowObject.MedioAbono == '') {
                return ".";
            } else {
                return "<img src='../Util/images/ico_mdl_checklist.gif' alt='" + cellvalue + "' title='Medio Pago' width='18px' onclick=\"javascript:fn_abreMedioPago(" + strParam + ");\" style='cursor:pointer;'/>";
            }


        } else {
            return "<img src='../Util/images/ico_mdl_checklist.gif' alt='" + cellvalue + "' title='Medio Pago' width='18px' onclick=\"javascript:fn_abreMedioPago(" + strParam + ");\" style='cursor:pointer;'/>&nbsp;<img src='../Util/images/ico_acc_eliminar.gif' alt='" + cellvalue + "' title='Eliminar Cargo' width='18px' onclick=\"javascript:fn_eliminaCargo(" + strParam + ");\" style='cursor:pointer;'/>";
        }

    };

}


//****************************************************************
// Funcion		:: 	fn_cancelar  
// Descripción	::	Cancela las Operaciones de Cotizacion
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_cancelar() {

    parent.fn_mdl_confirma(
                    "¿Está seguro de Volver?",
                    function() {
                        //IBK JJM
                        //Se quitó InsDesembolso de la ruta
                        //fn_util_globalRedirect("/frmInsDesembolsoListado.aspx");
                        fn_util_globalRedirect("/InsDesembolso/frmInsDesembolsoListado.aspx");
                    },
                    "Util/images/question.gif",
                    function() { },
                    'CONFIRMACION'
                   );
}


//****************************************************************
// Funcion		:: 	fn_cargaAgrupacion  
// Descripción	::	Carga Lista 
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_cargaAgrupacion() {

    try {
        parent.fn_blockUI();

        var arrParametros = [
                             "pstrNroContrato", $("#txtNroContrato").val(),
                             "pstrNroInstruccion", $("#hddCodigoInsDesembolso").val()
                            ];

        fn_util_AjaxSyncWM("frmInsDesembolsoRegistro.aspx/ListaAgrupacion",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);
                    fn_cargaDatosID();
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


//****************************************************************
// Funcion		:: 	fn_cargaAgrupacion  
// Descripción	::	Carga Lista 
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_cargaAgrupacionDocumento(pSubgrid_table_id) {

    try {
        //parent.fn_blockUI();		

        var arrParametros = [
                             "pstrNroContrato", $("#txtNroContrato").val(),
                             "pstrNroInstruccion", $("#hddCodigoInsDesembolso").val(),
                             "pstrCodAgrupacion", $("#hddCodigoAgrupacion").val()
                            ];

        fn_util_AjaxSyncWM("frmInsDesembolsoRegistro.aspx/ListaAgrupacionDocumento",
                arrParametros,
                function(jsondata) {
                    var subgrid = jQuery("#" + pSubgrid_table_id)[0];
                    subgrid.addJSONData(jsondata);
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

//****************************************************************
// Funcion		:: 	fn_listaCargoAbono  
// Descripción	::	Carga Lista 
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_listaCargoAbono(pstrTipo) {
    try {
        parent.fn_blockUI();

        var arrParametros = [
                             "pstrNroContrato", $("#txtNroContrato").val(),
                             "pstrNroInstruccion", $("#hddCodigoInsDesembolso").val(),
                             "pstrTipoGrupo", pstrTipo
                            ];

        fn_util_AjaxSyncWM("frmInsDesembolsoRegistro.aspx/ListaCargoAbono",
                arrParametros,
                function(jsondata) {
                    var grdListado = jQuery("#jqGrid_lista_" + pstrTipo)[0];
                    grdListado.addJSONData(jsondata);


                    fn_ocultaBloques(pstrTipo);
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
function fn_ocultaBloques(pstrBloque) {
    var intCantidad = $("#jqGrid_lista_" + pstrBloque).getGridParam("reccount");
    //Inicio IBK - AAE - Agrego lógica de ocultar mostrar
    if (parseInt(intCantidad) == 0) {
        if (fn_util_trim(pstrBloque) == "C") {
            $("#tr_clientes").hide();
            $("#grd_clientes").hide();
            $("#tot_clientes").hide();
        }
        if (fn_util_trim(pstrBloque) == "D") {
            $("#tr_sunat").hide();
            $("#grd_sunat").hide();
            $("#tot_sunat").hide();
        }
        if (fn_util_trim(pstrBloque) == "E") {
            $("#tr_difCambio").hide();
            $("#grd_difCambio").hide();
            $("#tot_diferencia").hide();
        }
    } else {
        if (fn_util_trim(pstrBloque) == "C") {
            $("#tr_clientes").show();
            //$("#grd_clientes").show();
            //$("#tot_clientes").show();
        }
        if (fn_util_trim(pstrBloque) == "D") {
            $("#tr_sunat").show();
            //$("#grd_sunat").show();
            //$("#tot_sunat").show();
        }
        if (fn_util_trim(pstrBloque) == "E") {
            $("#tr_difCambio").show();
            //$("#grd_difCambio").show();
            //$("#tot_diferencia").show();
        }
        if (fn_util_trim(pstrBloque) == "F") {
            $("#tr_cargos").show();
            //$("#grd_cargos").show();
        }
    }

}


//****************************************************************
// Funcion		:: 	fn_cargaAgrupacion  
// Descripción	::	Carga Lista 
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_abreListadoDocumentos() {
    parent.fn_blockUI();
    parent.fn_util_AbreModal("Instrucción Desembolso :: Listado Documentos", "InsDesembolso/frmDocumentosListado.aspx?cc=" + $("#txtNroContrato").val() + "&cid=" + $("#txtNroInstruccion").val(), 950, 500, function() { });
}


//****************************************************************
// Funcion		:: 	fn_abreNuevoAdelanto  
// Descripción	::	Carga Nuevo Adelanto 
// Log			:: 	JRC - 03/10/2012
//****************************************************************
function fn_abreNuevoAdelanto(pstrCodSolicitudCredito, pstrCodInstruccionDesembolso, pstrcodagrupacion, pstrCodProveedor, pstrCodMonedaPago, pstrCodGrupoHtml, pstrCodunico, pstModover, strImporteAgrupa) {

    parent.fn_blockUI();
    parent.fn_util_AbreModal("Instrucción Desembolso :: Adelanto", "InsDesembolso/frmAbonoRegisto.aspx?cc=" + $("#txtNroContrato").val() + "&cid=" + $("#txtNroInstruccion").val() + "&cpr=" + pstrCodProveedor + "&cmo=" + pstrCodMonedaPago + "&ImporteAgrupa=" + strImporteAgrupa, 500, 230, function() { });
}


//****************************************************************
// Funcion		:: 	fn_abreNuevoCargo  
// Descripción	::	Carga Nuevo Cargo 
// Log			:: 	JRC - 03/10/2012
//****************************************************************
function fn_abreNuevoCargo() {
    parent.fn_blockUI();
    parent.fn_util_AbreModal("Instrucción Desembolso :: Adelanto", "InsDesembolso/frmCargoRegistro.aspx?cc=" + $("#txtNroContrato").val() + "&cid=" + $("#txtNroInstruccion").val() + "&mco=" + $("#hddCodMonedaContrato").val() + "&mto=" + $("#txtTotalDesembolsado").val() + "&cct=" + $("#hddCodCotizacion").val(), 500, 270, function() { fn_recargaID(); });
    //Inicio IBK - AAE - Recargo la página

    //Fin IBK
}


//****************************************************************
// Funcion		:: 	fn_abreMedioPago 
// Descripción	::	Carga Medio de Pago
// Log			:: 	JRC - 03/10/2012
//****************************************************************
function fn_abreMedioPago(pstrCodSolicitudCredito, pstrCodInstruccionDesembolso, pstrcodagrupacion, pstrCodProveedor, pstrCodMonedaPago, pstrCodGrupoHtml, pstrCodunico, pstModover) {
    //Inicio IBK - AAE Paso como CU el código único del cliente
    var strCU = $("#txtCuCliente").val();  //pstrCodunico //("#txtCuCliente").val()
    var strMonCont = $("#hddCodMonedaContrato").val();
    if ((fn_util_trim(pstrcodagrupacion) == "07") || (fn_util_trim(pstrcodagrupacion) == "07") || (fn_util_trim(pstrcodagrupacion) == "08") || (fn_util_trim(pstrcodagrupacion) == "09") || (fn_util_trim(pstrcodagrupacion) == "10") || (fn_util_trim(pstrcodagrupacion) == "11") || (fn_util_trim(pstrcodagrupacion) == "12") || (fn_util_trim(pstrcodagrupacion) == "14")) {

        strCU = $("#txtCuCliente").val();
    } else {

        strCU = pstrCodunico;
    }
    parent.fn_blockUI();
    //parent.fn_util_AbreModal("Instrucción Desembolso :: Medio Pago", "InsDesembolso/frmMediosPagoRegistro.aspx?cc=" + pstrCodSolicitudCredito + "&cid=" + pstrCodInstruccionDesembolso + "&ca=" + pstrcodagrupacion + "&cah=" + pstrCodGrupoHtml + "&cma=" + pstrCodMonedaPago + "&pro=" + pstrCodProveedor + "&cu=" + pstrCodunico + "&acc=" + pstModover, 950, 270, function() { });
    //parent.fn_util_AbreModal("Instrucción Desembolso :: Medio Pago", "InsDesembolso/frmMediosPagoRegistro.aspx?cc=" + pstrCodSolicitudCredito + "&cid=" + pstrCodInstruccionDesembolso + "&ca=" + pstrcodagrupacion + "&cah=" + pstrCodGrupoHtml + "&cma=" + pstrCodMonedaPago + "&pro=" + pstrCodProveedor + "&cu=" + strCU + "&acc=" + pstModover, 950, 270, function() { });
    parent.fn_util_AbreModal("Instrucción Desembolso :: Medio Pago", "InsDesembolso/frmMediosPagoRegistro.aspx?cc=" + pstrCodSolicitudCredito + "&cid=" + pstrCodInstruccionDesembolso + "&ca=" + pstrcodagrupacion + "&cah=" + pstrCodGrupoHtml + "&cma=" + pstrCodMonedaPago + "&cmc=" + strMonCont + "&pro=" + pstrCodProveedor + "&cu=" + strCU + "&acc=" + pstModover, 950, 270, function() { });
    //Fin IBK
}

//****************************************************************
// Funcion		:: 	fn_ActualizarGrupos  
// Descripción	::	Carga Listas 
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_ActualizarGrupos() {
    var strGrupoHtml = $("#hddCodAgrupacionModal").val();
    fn_listaCargoAbono(strGrupoHtml);
    fn_cargaDatosID();
}

//****************************************************************
// Funcion		:: 	fn_actualizaListaCargo  
// Descripción	::	Carga Listas 
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_actualizaListaCargo(strGrupoHtml) {
    fn_listaCargoAbono(strGrupoHtml);
    fn_cargaDatosID();
}



//****************************************************************
// Funcion		:: 	fn_cargaDatosID  
// Descripción	::	Carga Datos ID 
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_cargaDatosID() {
    try {
        var arrParametros = [
                             "pstrNroContrato", $("#txtNroContrato").val(),
                             "pstrNroInstruccion", $("#hddCodigoInsDesembolso").val()
                            ];

        fn_util_AjaxWM("frmInsDesembolsoRegistro.aspx/CargaDatosID",
                arrParametros,
                function(result) {

                    var pstrResultado = fn_util_trim(result);
                    var arrResultado = pstrResultado.split("|")
                    $('#txtTotalDesembolsado').val(fn_util_ValidaMonto(arrResultado[0], 2));
                    $('#txtTotalPagos').val(fn_util_ValidaMonto(arrResultado[1], 2));
                    $('#txtTotalCargos').val(fn_util_ValidaMonto(arrResultado[2], 2));

                    parent.fn_unBlockUI();
                    fn_doResize();
                    //Inicio IBK - AAE - Activación leasing parcial
                    $("div#divTabs").tabs("enable", [1]);
                    $("div#divTabs").tabs("select", [1]);
                    //Fin IBK
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


//****************************************************************
// Funcion		:: 	fn_eliminaAbono
// Descripción	::	Inicializa Grilla 
// Log			:: 	JRC - 20/09/2012
//****************************************************************
function fn_eliminaAbono(strcodsolicitudcredito, strinstrucciondesembolso, strCodAgrupacion, strCodProveedor, strCodMoneda) {

    //Confirmacion de Eliminacion
    parent.fn_mdl_confirma(
            "¿Está seguro que desea eliminar el Adelanto?", //Mensaje - Obligatorio
            function() {
                parent.fn_blockUI();

                var arrParametros = ["pCodSolicitudCredito", strcodsolicitudcredito,
									 "pCodInstruccionDesembolso", strinstrucciondesembolso,
									 "pCodTipoOperacion", '001', //Abonos
									 "pCodAgrupacion", strCodAgrupacion,
									 "pCodProveedor", strCodProveedor,
									 "pCodMonedaPago", strCodMoneda];

                fn_util_AjaxSyncWM("frmInsDesembolsoRegistro.aspx/EliminaInstrucionDesembolsoGrupo",
					arrParametros,
					function(result) {
					    if (fn_util_trim(result) == "1") {
					        fn_cargaDatosID();
					        parent.fn_mdl_mensajeIco("El Adelanto se eliminó correctamente.", "util/images/ok.gif", "ELIMINACIÓN CORRECTA");
					        fn_listaCargoAbono("C");
					    }
					    parent.fn_unBlockUI();
					    fn_doResize();
					},
					function(request) {
					    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
					}
				);

            },
            "Util/images/question.gif",
            function() { },
            'ELIMINACIÓN'
    );

}


//****************************************************************
// Funcion		:: 	fn_eliminaCargo
// Descripción	::	Eliminar Cargo
// Log			:: 	JRC - 20/09/2012
//****************************************************************
function fn_eliminaCargo(strcodsolicitudcredito, strinstrucciondesembolso, strCodAgrupacion, strCodProveedor, strCodMoneda) {

    //Confirmacion de Eliminacion
    parent.fn_mdl_confirma(
            "¿Está seguro que desea eliminar el Cargo?", //Mensaje - Obligatorio
            function() {
                parent.fn_blockUI();

                var arrParametros = ["pCodSolicitudCredito", strcodsolicitudcredito,
									 "pCodInstruccionDesembolso", strinstrucciondesembolso,
									 "pCodTipoOperacion", '002', //Cargos
									 "pCodAgrupacion", strCodAgrupacion,
									 "pCodProveedor", strCodProveedor,
									 "pCodMonedaPago", strCodMoneda];

                fn_util_AjaxSyncWM("frmInsDesembolsoRegistro.aspx/EliminaInstrucionDesembolsoGrupo",
					arrParametros,
					function(result) {
					    if (fn_util_trim(result) == "1") {
					        fn_cargaDatosID();
					        parent.fn_mdl_mensajeIco("El Cargo se eliminó correctamente.", "util/images/ok.gif", "ELIMINACIÓN CORRECTA");
					        fn_listaCargoAbono("F");
					    }
					},
					function(request) {
					    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
					}
				);

            },
            "Util/images/question.gif",
            function() { },
            'ELIMINACIÓN'
    );

}

//****************************************************************
// Funcion		:: 	fn_generarWIO
// Descripción	::	Genera Wio
// Log			:: 	
//****************************************************************

function fn_ValidaGenerarWIO() {

    //Confirmacion de Eliminacion
    parent.fn_mdl_confirma(
            "¿Está seguro que desea generar el WIO?",
            function() {
                parent.fn_blockUI();

                var arrParametros = ["pCodigoSolicitudCredito", $("#txtNroContrato").val(),
									 "pCodigoInstruccionDesembolso", $("#txtNroInstruccion").val()];

                fn_util_AjaxSyncWM("frmInsDesembolsoRegistro.aspx/ValidaEjecucion",
									arrParametros,
									function(resultado) {
									    parent.fn_unBlockUI();
									    var varresult = resultado.split("|");
									    if (varresult[0] == "0") {
									        //if (fn_util_trim(resultado) == "0") {
									        //Inicio IBK - AAE - Activación LeasingParcial
									        //fn_generarWIO();
									        fn_ValidaGenWIOActParcial()
									    } else {
									        //parent.fn_mdl_mensajeIco("No se puede generar WIO. Falta completar Medios de pago.", "util/images/error.gif", "ERROR EN GENERACION WIO");
									        parent.fn_mdl_mensajeIco(varresult[1], "util/images/error.gif", "ERROR EN GENERACION WIO");
									    }
									},
									function(resultado) {
									    parent.fn_unBlockUI();
									    var error = eval("(" + resultado.responseText + ")");
									    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL DESEMBOLSAR");
									}
				);

            },
			"Util/images/question.gif",
			function() { },
			'CONFIRMACIÓN'
    );

}

//****************************************************************
// Funcion		:: 	fn_generarWIO
// Descripción	::	Genera Wio
// Log			:: 	
//****************************************************************
function fn_generarWIO() {

    parent.fn_blockUI();
    var strActivacion = "0";
    if ($('#chkActivacion').is(':checked')) {
        strActivacion = "1";
    }
    var arrParametros = ["pstrCodContrato", $("#txtNroContrato").val(),
						 "pstrCodInstruccion", $("#txtNroInstruccion").val(),
						 "pstrTotDifTC", $("#txtTotDif").val(),
						 "pstrActivacion", strActivacion];

    fn_util_AjaxSyncWM("frmInsDesembolsoRegistro.aspx/EnviaWIO",
				 arrParametros,
				 function(resultado) {
				     parent.fn_unBlockUI();
				     var varresult = resultado.split("|");
				     if (varresult[0] == "0") {
				         //Inicio IBK - AAE - Se comenta código por error de js
				         //parent.fn_mdl_mensajeOk("Se grabó correctamente los datos. Se generó la IO Nº" + fn_util_trim(varresult[1]), function() { fn_RedireccionGrabar() }, "GRABADO CORRECTO");
				         parent.fn_mdl_mensajeOk("Se grabó correctamente los datos. Se generó la IO Nº" + fn_util_trim(varresult[1]), function() { fn_RedireccionEjecucionID() }, "GRABADO CORRECTO");
				         //fin IBK 
				     } else {
				         parent.fn_mdl_mensajeIco(varresult[1], "util/images/error.gif", "ERROR AL DESEMBOLSAR");
				     }
				 },
				function(resultado) {
				    parent.fn_unBlockUI();
				    var error = eval("(" + resultado.responseText + ")");
				    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL DESEMBOLSAR");
				}
	);


}



//****************************************************************
// Funcion		:: 	fn_recalcular
// Descripción	::	Recalcular Montos con TC
// Log			:: 	
//****************************************************************
function fn_recalcular() {

    parent.fn_blockUI();
    var decTC = $("#txtTcDia").val()
    if (fn_isNumber(decTC) == false) {
        decTC = 0
    }
    var arrParametros = ["pstrcodInstruccionDesembolso", $("#txtNroInstruccion").val(),
						 "pstrcodsolicitudcredito", $("#txtNroContrato").val(),
						 "pstrtcdia", decTC,
						 "pstrnroticket", $("#txtNroTicket").val()
						];

    fn_util_AjaxWM("frmInsDesembolsoRegistro.aspx/Recalcular",
				 arrParametros,
				 function(result) {
				     parent.fn_unBlockUI();
				     //Inicio IBK - AAE
				     var varresult = result.split("|");
				     if (varresult[0] == "0") {
				         /*parent.fn_mdl_mensajeOk("Grabado correctamente", function() {
				         // fn_RedireccionGrabar();
				         }, "GRABADO CORRECTO");*/
				         $("#dv_botonWIO").show();
				     } else {
				         parent.fn_mdl_mensajeIco(varresult[1], "util/images/error.gif", "ERROR AL ACTUALIZAR TC");
				     }
				     //Fin IBK
				 },
				 function(resultado) {
				     parent.fn_unBlockUI();
				     var error = eval("(" + resultado.responseText + ")");
				     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABADO");
				 }
		);

}



//****************************************************************
// Funcion		:: 	fn_ejecutarID
// Descripción	::	Ejecutar Instruccion Desembolso
// Log			:: 	
//****************************************************************
function fn_ejecutarID() {

    //Confirmacion de Eliminacion
    parent.fn_mdl_confirma(
        "¿Está seguro que desea ejecutar la Instrucción de Desembolso?",
		function() {

		    parent.fn_blockUI();
		    /* Inicio IBK - AAE - Cambio a cantidad de parámetros*/
		    /*var arrParametros = ["pstrNroContrato", $("#txtNroContrato").val(),
		    "pstrNroInstruccion", $("#txtNroInstruccion").val()
		    ]*/
		    var arrParametros = ["pstrNroContrato", $("#txtNroContrato").val(),
								 "pstrNroInstruccion", $("#txtNroInstruccion").val(),
								 "pstrEnvioLPC", $("#hddFlagEnvioLPC").val(),
								 "pstrReintentar", "0"
								 ];

		    fn_util_AjaxSyncWM("frmInsDesembolsoRegistro.aspx/EjecutaInsDesembolso",
						 arrParametros,
						 function(resultado) {
						     parent.fn_unBlockUI();
						     var varresult = resultado.split("|");
						     if (varresult[0] == "0") {
						         parent.fn_mdl_mensajeOk(varresult[1], function() { fn_RedireccionEjecucionID() }, "EJECUCIÓN CORRECTA");
						     } else {
						         parent.fn_mdl_mensajeIco(varresult[1], "util/images/error.gif", "ERROR AL EJECUTAR");
						         //Inicio IBK - AAE
						         //fn_recargaID();
						         //Fin IBK - AAE
						     }
						 },
						function(resultado) {
						    parent.fn_unBlockUI();
						    var error = eval("(" + resultado.responseText + ")");
						    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL EJECUTAR");
						}
			);

		},
        "Util/images/question.gif",
        function() { },
        'EJECUCION ID'
    );





}
function fn_RedireccionEjecucionID() {
    fn_util_globalRedirect("/InsDesembolso/frmInsDesembolsoListado.aspx");
    //IBK JJM 
    //Se quitó InsDesembolso de la ruta
    //fn_util_globalRedirect("/frmInsDesembolsoListado.aspx");
}



//****************************************************************
// Funcion		:: 	fn_recargaID  
// Descripción	::	Carga Lista 
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_recargaID() {
    fn_util_globalRedirect("/InsDesembolso/frmInsDesembolsoRegistro.aspx?hddCodigoContrato=" + $("#txtNroContrato").val() + "&hddCodigoInsDesembolso=" + $("#txtNroInstruccion").val());
    //IBK JJM 
    //Se quitó InsDesembolso de la ruta
    //fn_util_globalRedirect("/frmInsDesembolsoRegistro.aspx?hddCodigoContrato=" + $("#txtNroContrato").val() + "&hddCodigoInsDesembolso=" + $("#txtNroInstruccion").val());
}


//****************************************************************
// Funcion		:: 	fn_validaEstado  
// Descripción	::	Valida estado de ID
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_validaEstado() {
    
    $("#hddModoVer").val("0");
    $("#dv_botonWIO").hide();
    $("#dv_botonEjecutar").hide();
    $("#dv_botonRecalcular").hide();
    $("#dv_botonDevolver").hide();
    $("#dv_botonEnviar").hide();
    //Inicio IBK - AAE - Oculto botones
    $("#dv_botonReintentar").hide();
    $("#dv_botonPagoAdministrativo").hide();
    $("#dv_botonAnular").hide();
    //Fin IBK

    var strEstadoIns = $("#hddCodEstadoInstruccion").val();

    if (fn_util_trim(strEstadoIns) == strEstadoIns_EnElaboracion) {
        //$("#dv_botonWIO").show();
        $("#dv_botonRecalcular").show();
        $("#dv_botonAnular").show();
    }
    if (fn_util_trim(strEstadoIns) == strEstadoIns_Wio) {
        fn_activaVerID();
    }
    if (fn_util_trim(strEstadoIns) == strEstadoIns_PendEjecucion) {
        fn_activaVerID();
        $("#dv_botonEjecutar").show();
        $("#dv_botonDevolver").show();
        //Inicio IBK - AAE - Muestro botones
        $("#dv_botonPagoAdministrativo").show();
        // Fin IBK
    }
    if (fn_util_trim(strEstadoIns) == strEstadoIns_Devuelta) {
        $("#dv_botonEnviar").show();
        $("#dv_botonRecalcular").show();
        $("#dv_botonAnular").show();
    }
    //Inicio IBK - AAE - Si el estado es Aprobada o Administrativa
    //if(fn_util_trim(strEstadoIns) == strEstadoIns_Anulada || fn_util_trim(strEstadoIns) == strEstadoIns_Aprobada){
    if (fn_util_trim(strEstadoIns) == strEstadoIns_Anulada || fn_util_trim(strEstadoIns) == strEstadoIns_Aprobada || fn_util_trim(strEstadoIns) == strEstadoIns_PagAdmin) {
        fn_activaVerID();
    }
    //Inicio IBK - AAE - Codifico estados nuevos
    if (fn_util_trim(strEstadoIns) == strEstadoIns_PagAdmin) {
        fn_activaVerID();
    }
    if (fn_util_trim(strEstadoIns) == strEstadoIns_Error) {
        fn_activaVerID();
        $("#dv_botonReintentar").show();
        $("#dv_botonPagoAdministrativo").show();
        //$("#dv_botonDevolver").show();	
    }
    if (fn_util_trim(strEstadoIns) == strEstadoIns_EnEjecucion) {
        fn_activaVerID();
        $("#dv_botonReintentar").show();
        $("#dv_botonPagoAdministrativo").show();
        //$("#dv_botonDevolver").show();	
    }

    //Fin IBK


}


//****************************************************************
// Funcion		:: 	fn_activaVerID  
// Descripción	::	Activa Modo ver en la ID
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_activaVerID() {
    fn_util_bloquearFormulario();
    $("#hddModoVer").val("1");
    $("#tb_linkAbreListadoDoc").hide();
    $("#af_linkAbreAdelanto").hide();
    $("#af_linkAbreCargo").hide();
    $("#tb_botonesDocumentos").hide();

    $("#txtTotalDesembolsado").addClass("ui-edit-align-right");
    $("#txtTotalPagos").addClass("ui-edit-align-right");
    $("#txtTotalCargos").addClass("ui-edit-align-right");
    $("#txtTcDia").addClass("ui-edit-align-right");
    $("#txtTcTicket").addClass("ui-edit-align-right");

    //Inicio IBK - AAE - Activacion Leasing Parcial
    $("#dv_botonCronograma").hide();
    //Fin IBK
}



//****************************************************************
// Funcion		:: 	fn_actualizarEstadoID
// Descripción	::	Actualiza estado Instruccion Desembolso
// Log			:: 	
//****************************************************************
function fn_actualizarEstadoID(pstrCodEstado) {

    parent.fn_blockUI();
    //Inicio IBK - AAE - Agrego parámetro de ejecución en LPC
    var strFlagLPC = "0"
    var arrParametros = ["pCodSolicitudCredito", $("#txtNroContrato").val(),
						 "pCodInstruccionDesembolso", $("#txtNroInstruccion").val(),
						 "pCodEstado", pstrCodEstado,
						 "pflagLPC", strFlagLPC];

    fn_util_AjaxSyncWM("frmInsDesembolsoRegistro.aspx/ActualizaInsDesembolsoEstado",
				 arrParametros,
				 function(resultado) {
				     parent.fn_unBlockUI();
				     var varresult = resultado.split("|");
				     if (varresult[0] == "1") {
				         parent.fn_mdl_mensajeOk(varresult[1], function() { fn_RedireccionEjecucionID() }, "GRABADO CORRECTO");
				     } else {
				         parent.fn_mdl_mensajeIco(varresult[1], "util/images/error.gif", "ERROR AL DESEMBOLSAR");
				     }
				 },
				function(resultado) {
				    parent.fn_unBlockUI();
				    var error = eval("(" + resultado.responseText + ")");
				    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL DESEMBOLSAR");
				}
	);

}


//****************************************************************
// Funcion		:: 	fn_volverDocumento
// Descripción	::	Volver a desembolso de documentos
// Log			:: 	
//****************************************************************
function fn_volverDocumento() {
    //IBK JJM
    //Se quitó InsDesembolso de la ruta
    //fn_util_globalRedirect("/frmDesembolsoRegistro.aspx?hcontrato=" + $("#txtNroContrato").val());
    fn_util_globalRedirect("/Desembolso/frmDesembolsoRegistro.aspx?hcontrato=" + $("#txtNroContrato").val());
}


//****************************************************************
// Funcion		:: 	fn_iniGrillaDocumento 
// Descripción	::	Inicializa Grilla Documento
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_iniGrillaDocumento() {

    $("#jqGrid_lista_G").jqGrid({
        datatype: function() {
            fn_cargaGrillaDocumento();
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
        colNames: ['Codigo', 'Nombre Archivo', 'Adjunto', 'Comentario'],
        colModel: [
                { name: 'CodigoDocumento', index: 'CodigoDocumento', hidden: true },
		        { name: 'NombreArchivo', index: 'NombreArchivo', width: 200, align: "left", sorttype: "string", defaultValue: "" },
		        { name: 'RutaArchivo', index: 'RutaArchivo', width: 100, align: "Center", sortable: false, formatter: fn_icoDownload },
		        { name: 'Comentario', index: 'Comentario', width: 550, align: "left", sorttype: "string", defaultValue: "" }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_G',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'CodigoDocumento',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_G").jqGrid('getRowData', id);
            $("#hddCodigoDocumento").val(rowData.CodigoDocumento);
        },
        ondblClickRow: function(id) {
        }
    });
    jQuery("#jqGrid_lista_G").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_G").hide();

    //Abrir Archivo
    function fn_icoDownload(cellvalue, options, rowObject) {
        var strNombreArchivo = rowObject.RutaArchivo.split('\\').pop();
        strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
        if (fn_util_trim(rowObject.RutaArchivo) != "") {
            return "<img src='../Util/images/ico_download.gif' alt='" + strNombreArchivo + "' title='" + strNombreArchivo + "' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.RutaArchivo) + "');\" style='cursor:pointer;'/>";
        } else {
            return ".";
        }
    };

}



//****************************************************************
// Funcion		:: 	fn_cargaGrillaDocumento 
// Descripción	::	Abre Modal de Motivo de Rechazo
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrillaDocumento() {

    try {

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_G", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_G", "page"), // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_G", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_G", "sortorder"), // Criterio de ordenación
                             "pstrCodInstruccion", $("#txtNroInstruccion").val(),
                             "pstrCodContrato", $("#txtNroContrato").val()
                            ];

        fn_util_AjaxWM("frmInsDesembolsoRegistro.aspx/ListadoInsDesembolsoDocumento",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_G.addJSONData(jsondata);
                    fn_doResize();
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

    } catch (ex) {
        fn_util_alert(ex.message);
    }

}


//****************************************************************
// Funcion		:: 	fn_eliminarDocumentoComentario 
// Descripción	::	Editar 
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_eliminarDocumentoComentario() {

    if ($("#hddTipoTransaccion").val() != "NUEVO") {

        //Variables                             
        var strNroInstruccion = $("#txtNroInstruccion").val();
        var strNroContrato = $("#txtNroContrato").val();

        var strCodigoDocumento = $("#hddCodigoDocumento").val();
        var paramArray = ["pstrCodInstruccion", strNroInstruccion, "pstrCodContrato", strNroContrato, "pstrCodigoDocumento", strCodigoDocumento];

        if (fn_util_trim(strCodigoDocumento) == "") {
            parent.fn_mdl_alert("Debe seleccionar un documento.", function() { });
        }
        else {
            //Confirmacion de Eliminacion
            parent.fn_mdl_confirma(
                            "¿Está seguro que desea eliminar el Documento/Comentario?  ", //Mensaje - Obligatorio
                            function() {
                                parent.fn_blockUI();
                                fn_util_AjaxWM("frmInsDesembolsoRegistro.aspx/EliminaDocumentoComentario",
                                                   paramArray,
                                                   function(resultado) {
                                                       fn_cargaGrillaDocumento();
                                                       parent.fn_unBlockUI();
                                                   },
                                                   function(resultado) {
                                                       var error = eval("(" + resultado.responseText + ")");
                                                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
                                                   }
                                    );

                            },
                            "Util/images/question.gif",
                            function() { },
                            'ELIMINAR DOCUMENTO/COMENTARIO'
            );

        }

    } else {
        parent.fn_mdl_alert("Debe grabar la Cotización para ingresar un Documento/Comentario.", function() { });
    }

}



//****************************************************************
// Funcion		:: 	fn_cotizacionRechazar 
// Descripción	::	Abre Modal de Motivo de Rechazo
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_abreNuevoDocumentoComentario() {
    var strNroInstruccion = $("#txtNroInstruccion").val();
    var strNroContrato = $("#txtNroContrato").val();
    parent.fn_util_AbreModal("Ins. Desembolso :: Documentos y Comentarios", "InsDesembolso/frmDocumentoComentario.aspx?hddCodigoInstruccion=" + strNroInstruccion + "&hddCodigoContrato=" + strNroContrato, 650, 320, function() { });
}

//****************************************************************
// Funcion		:: 	fn_editarComentario 
// Descripción	::	Editar 
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_editarDocumentoComentario() {
    var strNroInstruccion = $("#txtNroInstruccion").val();
    var strNroContrato = $("#txtNroContrato").val();
    var strCodigoDocumento = $("#hddCodigoDocumento").val();

    if (fn_util_trim(strCodigoDocumento) == "") {
        parent.fn_mdl_alert("Debe seleccionar un documento.", function() { });
    } else {
        parent.fn_util_AbreModal("Ins. Desembolso :: Documentos y Comentarios", "InsDesembolso/frmDocumentoComentario.aspx?hddCodigoInstruccion=" + strNroInstruccion + "&hddCodigoContrato=" + strNroContrato + "&hddCodigoDocumento=" + strCodigoDocumento, 650, 320, function() { });
    }
}

//****************************************************************
// Funcion		:: 	fn_abreArchivo 
// Descripción	::	Abre Archivo
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_abreArchivo(pstrRuta) {
    window.open("../frmDownload.aspx?nombreArchivo=" + pstrRuta);
    return false;
}

/* Inicio IBK - AAE - Agrego nuevos métodos */

//****************************************************************
// Funcion		:: 	fn_ReintentarPago
// Descripción	::	Reintenta Ejecutar Instruccion Desembolso
// Log			:: 	
//****************************************************************
function fn_ReintentarPago() {

    //Confirmacion de Eliminacion
    parent.fn_mdl_confirma(
        "¿Está seguro que desea ejecutar la Instrucción de Desembolso?",
		function() {

		    parent.fn_blockUI();
		    var arrParametros = ["pstrNroContrato", $("#txtNroContrato").val(),
								 "pstrNroInstruccion", $("#txtNroInstruccion").val(),
								 "pstrEnvioLPC", $("#hddFlagEnvioLPC").val()
								 ];

		    fn_util_AjaxSyncWM("frmInsDesembolsoRegistro.aspx/ReintentoEjecutaInsDesembolso",
						 arrParametros,
						 function(resultado) {
						     parent.fn_unBlockUI();
						     var varresult = resultado.split("|");
						     if (varresult[0] == "0") {
						         parent.fn_mdl_mensajeOk(varresult[1], function() { fn_RedireccionEjecucionID() }, "EJECUCIÓN CORRECTA");
						     } else {
						         parent.fn_mdl_mensajeIco(varresult[1], "util/images/error.gif", "ERROR AL EJECUTAR");
						     }
						 },
						function(resultado) {
						    parent.fn_unBlockUI();
						    var error = eval("(" + resultado.responseText + ")");
						    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL EJECUTAR");
						}
			);

		},
        "Util/images/question.gif",
        function() { },
        'EJECUCION ID'
    );
}

//****************************************************************
// Funcion		:: 	fn_Administrativo
// Descripción	::	Marca la instrucción de desembolso como Administrativo
// Log			:: 	
//****************************************************************
function fn_Administrativo() {

    //Confirmacion de Eliminacion
    parent.fn_mdl_confirma(
        "¿Está seguro que desea ejecutar la Instrucción de Desembolso como Administrativo?",
		function() {

		    parent.fn_blockUI();
		    var arrParametros = ["pstrNroContrato", $("#txtNroContrato").val(),
								 "pstrNroInstruccion", $("#txtNroInstruccion").val(),
								 "pstrEnvioLPC", $("#hddFlagEnvioLPC").val()];

		    fn_util_AjaxSyncWM("frmInsDesembolsoRegistro.aspx/EjecutaAdministrativo",
						 arrParametros,
						 function(resultado) {
						     parent.fn_unBlockUI();
						     var varresult = resultado.split("|");
						     if (varresult[0] == "0") {
						         parent.fn_mdl_mensajeOk(varresult[1], function() { fn_RedireccionEjecucionID() }, "EJECUCIÓN CORRECTA");
						     } else {
						         parent.fn_mdl_mensajeIco(varresult[1], "util/images/error.gif", "ERROR AL EJECUTAR");
						     }
						 },
						function(resultado) {
						    parent.fn_unBlockUI();
						    var error = eval("(" + resultado.responseText + ")");
						    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL EJECUTAR");
						}
			);

		},
        "Util/images/question.gif",
        function() { },
        'EJECUCION ID'
    );
}

//****************************************************************
// Funcion		:: 	fn_Anular
// Descripción	::	Anula la instrucción
// Log			:: 	
//****************************************************************
function fn_Anular() {

    //Confirmacion de Eliminacion
    parent.fn_mdl_confirma(
        "¿Está seguro que desea Anular la Instrucción de Desembolso?",
		function() {

		    parent.fn_blockUI();
		    var arrParametros = ["pstrNroContrato", $("#txtNroContrato").val(),
								 "pstrNroInstruccion", $("#txtNroInstruccion").val()];

		    fn_util_AjaxSyncWM("frmInsDesembolsoRegistro.aspx/Anular",
						 arrParametros,
						 function(resultado) {
						     parent.fn_unBlockUI();
						     var varresult = resultado.split("|");
						     if (varresult[0] == "0") {
						         parent.fn_mdl_mensajeOk(varresult[1], function() { fn_RedireccionEjecucionID() }, "Anulación Correcta");
						     } else {
						         parent.fn_mdl_mensajeIco(varresult[1], "util/images/error.gif", "ERROR AL ANULAR");
						     }
						 },
						function(resultado) {
						    parent.fn_unBlockUI();
						    var error = eval("(" + resultado.responseText + ")");
						    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL ANULAR");
						}
			);

		},
        "Util/images/question.gif",
        function() { },
        'EJECUCION ID'
    );
}

function fn_isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}


//************************************************************
// Función		:: 	fn_validaTipoSeguroBien
// Descripcion 	:: 	Valida el tipo de Seguro Bien
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_validaTipoSeguroBien(strValor, intNroCuotas) {
    if (strValor == "001") {
        $('#txtNumCuotasfinanciadas').val(intNroCuotas);
        $('#txtNumCuotasfinanciadas').removeClass('css_input_inactivo').addClass('css_input_obligatorio');
        $('#txtNumCuotasfinanciadas').prop('readonly', false);
        $('#txtImportePrimaSeguroBien').removeClass('css_input_inactivo').addClass('css_input_obligatorio ui-edit-align-right');
        $('#txtImportePrimaSeguroBien').prop('readonly', false);
    } else {
        $('#txtNumCuotasfinanciadas').val("");
        $('#txtNumCuotasfinanciadas').attr('class', 'css_input_inactivo');
        $('#txtNumCuotasfinanciadas').prop('readonly', true);
        $('#txtImportePrimaSeguroBien').attr('class', 'css_input_inactivo ui-edit-align-right');
        $('#txtImportePrimaSeguroBien').prop('readonly', true);
        $("#hddMontoTemporal").validNumber({ value: "0" });
        $('#txtImportePrimaSeguroBien').val($("#hddMontoTemporal").val());
    }
}


//************************************************************
// Función		:: 	fn_validaTipoSeguroDegravamen
// Descripcion 	:: 	Valida el tipo de Seguro Degravamen
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_validaTipoSeguroDegravamen(strValor, intNroCuotas) {
    if (strValor == "001") {
        $('#txtNumCuotaFinanciar').val(intNroCuotas);
        $('#txtNumCuotaFinanciar').removeClass('css_input_inactivo').addClass('css_input_obligatorio');
        $('#txtNumCuotaFinanciar').prop('readonly', false);
        $('#txtImportePrimaDesgravamen').removeClass('css_input_inactivo').addClass('css_input_obligatorio ui-edit-align-right');
        $('#txtImportePrimaDesgravamen').prop('readonly', false);
    } else {
        $('#txtNumCuotaFinanciar').val("");
        $('#txtNumCuotaFinanciar').attr('class', 'css_input_inactivo');
        $('#txtNumCuotaFinanciar').prop('readonly', true);
        $('#txtImportePrimaDesgravamen').attr('class', 'css_input_inactivo ui-edit-align-right');
        $('#txtImportePrimaDesgravamen').prop('readonly', true);
        $("#hddMontoTemporal").validNumber({ value: "0" });
        $('#txtImportePrimaDesgravamen').val($("#hddMontoTemporal").val());
    }
}

//****************************************************************
// Funcion		:: 	fn_of_CuotasSeguro 
// Descripción	::	Valida Cuotas Seguro
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_of_CuotasSeguro() {
    var strCuotas = $("#txtNroCuotas").val();
    var strCuotasSeg = $("#txtNumCuotasfinanciadas").val();

    if (fn_util_trim(strCuotas) == "") strCuotas = "0";
    if (fn_util_trim(strCuotasSeg) == "") strCuotasSeg = "0";

    if (parseInt(strCuotasSeg) > parseInt(strCuotas)) {
        $("#txtNumCuotasfinanciadas").val(strCuotas);
        //parent.fn_util_MuestraLogPage("El Nro. de Cuotas del Seguro ingresado no valido", "E");
    }

    //if (parseInt(strCuotasSeg) == 0) {
    //$("#txtNumCuotasfinanciadas").val(1)
    //parent.fn_util_MuestraLogPage("El Nro. de Cuotas del Seguro ingresado no valido", "E");
    //}

}

//****************************************************************
// Funcion		:: 	fn_of_CuotasSeguroDes
// Descripción	::	Valida Cuotas Seguro
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_of_CuotasSeguroDes() {
    var strCuotas = $("#txtNroCuotas").val();
    var strCuotasSeg = $("#txtNumCuotaFinanciar").val();

    if (fn_util_trim(strCuotas) == "") strCuotas = "0";
    if (fn_util_trim(strCuotasSeg) == "") strCuotasSeg = "0";

    if (parseInt(strCuotasSeg) > parseInt(strCuotas)) {
        $("#txtNumCuotaFinanciar").val(strCuotas)
        //parent.fn_util_MuestraLogPage("El Nro. de Cuotas del Seguro ingresado no valido", "E");
    }

    //if (parseInt(strCuotasSeg) == 0) {
    //$("#txtNumCuotaFinanciar").val(1)
    //parent.fn_util_MuestraLogPage("El Nro. de Cuotas del Seguro ingresado no valido", "E");
    //}

}

//****************************************************************
// Funcion		:: 	fn_cargaGrillaDesembolsos
// Descripción	::	Inicializa Grilla 
// Log			:: 	JRC - 20/09/2012
//****************************************************************
function fn_cargaGrillaDesembolsos() {

    $("#jqGrid_lista_H").jqGrid({
        datatype: function() {
            fn_listaDesembolsos();
        },
        jsonReader:
        {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "CodSolicitudCredito,NroDesembolso"
        },

        colNames: ['Contrato', 'Id', 'TipoDocumento', 'NroDocumento', '', 'Proveedor', 'Fecha emisión', 'Moneda', 'Tipo Cambio', 'T/C SBS', 'Importe Original', 'IGV Original', 'Importe Mon Contrato', 'IGV Mon Contrato', 'Total', 'Fecha Desembolso'],
        colModel: [
                { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
		        { name: 'NroDesembolso', index: 'NroDesembolso', width: 25, align: "center" },
		        { name: 'tipoDoc', index: 'tipoDoc', width: 40, align: "center" },
		        { name: 'nroDoc', index: 'nroDoc', width: 50, align: "center" },
		        { name: 'RUC', index: 'RUC', hidden: true },
		        { name: 'Proveedor', index: 'Proveedor', width: 100, align: "center" },
		        { name: 'fechaEmision', index: 'fechaEmision', width: 50, align: "center" },
		        { name: 'Moneda', index: 'Moneda', width: 40, align: "center" },
		        { name: 'tipoCambio', index: 'tipoCambio', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'tipoCambioSBS', index: 'tipoCambioSBS', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'importeOriginal', index: 'importeOriginal', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'igvOriginal', index: 'igvOriginal', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'importeMonContrato', index: 'importeMonContrato', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'igvMonContrato', index: 'igvMonContrato', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'totalMonContrato', index: 'totalMonContrato', width: 40, align: "right", weightfont: "bold", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'fechaDesembolso', index: 'fechaDesembolso', width: 50, align: "center" }
        ],
        //width: glb_intWidthPantalla-170,
        width: glb_intWidthPantalla - 120,
        height: '100%',
        pager: '#jqGrid_pager_H',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30, 50, 100, 200],
        sortname: 'fechaDesembolso',
        sortorder: 'asc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) { },
        ondblClickRow: function(id) { }
    });
    jQuery("#jqGrid_lista_H").jqGrid('navGrid', '#jqGrid_lista_H', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_H").hide();

}

//****************************************************************
// Funcion		:: 	fn_listaDesembolsos  
// Descripción	::	Carga Lista 
// Log			:: 	AAE - 19/09/2012
//****************************************************************
function fn_listaDesembolsos() {
    try {
        parent.fn_blockUI();

        var arrParametros = [
                            "pPageSize", fn_util_getJQGridParam("jqGrid_lista_H", "rowNum"),      // Cantidad de elementos de la página
                            "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_H", "page"),        // Página actual
                            "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_H", "sortname"),    // Columna a ordenar
                            "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_H", "sortorder"),   // Criterio de ordenación
                            "pstrNroContrato", $("#txtNroContrato").val(),
                            "pstrCodInstDesembolso", $("#txtNroInstruccion").val()
                            ];

        fn_util_AjaxSyncWM("frmInsDesembolsoRegistro.aspx/ListaDesembolsos",
                arrParametros,
                function(jsondata) {
                    var grdListado = jQuery("#jqGrid_lista_H")[0];
                    grdListado.addJSONData(jsondata);
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

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrillaCronograma() {

    $("#jqGrid_lista_I").jqGrid({
        datatype: function() {
            fn_paginaCronograma();
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
        colNames: ['#', 'Fec.Venc.', 'Días', 'Saldo', 'Interés', 'Principal', 'Monto Cuota', 'Monto Cuota Seguro', 'Monto Cuota Seguro Desgrav.', 'IGV', 'Total a Pagar', ''],
        colModel: [
		        { name: 'Numerocuota', index: 'Numerocuota', width: 40, align: "center" },
		        { name: 'SFechavencimiento', index: 'Fechavencimiento', align: "center", formatter: Fn_util_ValidaFechaVacia },
		        { name: 'Cantdiascuota', index: 'Cantdiascuota', align: "center" },
		        { name: 'SMontosaldoadeudado', index: 'Montosaldoadeudado', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontointeresbien', index: 'Montointeresbien', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontoprincipalbien', index: 'Montoprincipalbien', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontototalcuota', index: 'Montototalcuota', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontocuotasegurobien', index: 'Montocuotasegurobien', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'SCuotaSeguroDes', index: 'CuotaSeguroDes', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontototalcuotaigv', index: 'Montototalcuotaigv', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'STotalapagar', index: 'Totalapagar', width: 120, align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'AA', index: 'AA', width: 7, align: "right" }
        ],
        width: glb_intWidthPantalla - 120,
        height: '100%',
        pager: '#jqGrid_pager_I',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 50,
        //rowList: [10, 20, 30],
        sortname: 'Numerocuota',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) { },
        ondblClickRow: function(id) { },
        gridComplete: function(id) {
            fn_validaColumnasCronograma();
        }
    });
    jQuery("#jqGrid_lista_I").jqGrid('navGrid', '#jqGrid_lista_I', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_I").hide();

}

//****************************************************************
// Funcion		:: 	fn_paginaCronograma
// Descripción	::	Pagina Cronograma
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_paginaCronograma() {

    try {
        //debugger;
        var arrParametros = ["pstrPaginaActual", fn_util_getJQGridParam("jqGrid_lista_I", "page")];

        fn_util_AjaxWM("frmInsDesembolsoRegistro.aspx/PaginaCronograma",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_I.addJSONData(jsondata);
                    fn_doResize();
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

    } catch (ex) {
        fn_util_alert(ex.message);
    }
}

//****************************************************************
// Funcion		:: 	fn_validaColumnasCronograma 
// Descripción	::	Valida Columnas Cronograma
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_validaColumnasCronograma() {

    var gridCronograma = $('#jqGrid_lista_I');
    if ($("#hddTipoPersona").val() == "2") {
        gridCronograma.jqGrid('hideCol', gridCronograma.getGridParam("colModel")[8].name);
    } else {
        if ($("#cmbTipoSeguro").val() != "001") {//strSeguroDegravamenTipoInterno) {
            gridCronograma.jqGrid('hideCol', gridCronograma.getGridParam("colModel")[8].name);
        }
    }
    if (!fn_util_ValidaDecimal($('#txtImportePrimaSeguroBien').val()) > 0) {
        gridCronograma.jqGrid('hideCol', gridCronograma.getGridParam("colModel")[7].name);
    } else {
        gridCronograma.jqGrid('showCol', gridCronograma.getGridParam("colModel")[7].name);
    }

    if (!fn_util_ValidaDecimal($('#txtImportePrimaDesgravamen').val()) > 0) {
        gridCronograma.jqGrid('hideCol', gridCronograma.getGridParam("colModel")[8].name);
    } else {
        gridCronograma.jqGrid('showCol', gridCronograma.getGridParam("colModel")[8].name);
    }

}

//****************************************************************
// Funcion		:: 	fn_cargaGrillaCronograma
// Descripción	::	Abre Modal de Motivo de Rechazo
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_llenaGrillaCronograma() {

    try {

        var arrParametros = ["pstrNroContrato", $("#txtNroContrato").val(),
                             "pstrInstruccionDesembolso", $("#txtNroInstruccion").val(),
                             "pstrCuotaInicial", $("#txtCuotaInicialCro").val()
                            ];

        fn_util_AjaxWM("frmInsDesembolsoRegistro.aspx/ListadoCronogramaActivacion",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_I.addJSONData(jsondata);
                    fn_doResize();
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

    } catch (ex) {
        fn_util_alert(ex.message);
    }

}

//****************************************************************
// Funcion		:: 	fn_padStr
// Descripción	::	Auxiliar para fechas
// Log			:: 	AAE
//****************************************************************
function fn_padStr(i) {
    return (i < 10) ? "0" + i : "" + i;
}

//****************************************************************
// Funcion		:: 	fn_generaCronograma
// Descripción	::	Activa los Tabs de la página
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_generaCronograma() {

    //parent.fn_blockUI();

    //Limpia Grilla
    //$("#jqGrid_lista_C").GridUnload();
    //$("#jqGrid_lista_C").clearGridData(true);
    //$("#tbCronograma").hide();
    $("#jqGrid_lista_I").jqGrid("clearGridData", true);
    //$("#jqGrid_lista_C").trigger("reloadGrid");

    //String Validación
    var strError = new StringBuilderEx();

    //Declara        
    var objcmbTipoCronograma = $('select[id=cmbTipoCronograma]');
    var objtxtNroCuotas = $('input[id=txtNroCuotas]:text');
    var objcmbPeriodicidad = $('select[id=cmbPeriodicidad]');
    var objcmbFrecuenciaPago = $('select[id=cmbFrecuenciaPago]');
    var objtxtTEA = $('input[id=txtTEA]:text');
    var objcmbTipoBienSeguro = $('select[id=cmbTipoBienSeguro]');
    var objtxtPrecioVenta = $('input[id=txtPrecioVentaCro]:text');

    strError.append(fn_util_ValidateControl(objcmbTipoCronograma[0], 'un Tipo Cronograma válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtNroCuotas[0], 'un Nro. Cuotas válido', 1, ''));
    strError.append(fn_util_ValidateControl(objcmbPeriodicidad[0], 'un Periodicidad Neto válido', 1, ''));
    strError.append(fn_util_ValidateControl(objcmbFrecuenciaPago[0], 'una Frecuencia de Pago Neto válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtTEA[0], 'una TEA válida', 1, ''));
    strError.append(fn_util_ValidateControl(objcmbTipoBienSeguro[0], 'un Seguro de Bien válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtPrecioVenta[0], 'un Precio Venta válido', 1, ''));

    $("#txtNroCuotas").addClass("ui-edit-align-right");
    $("#txtTEA").addClass("ui-edit-align-right");
    $("#txtPrecioVentaCro").addClass("ui-edit-align-right");

    //Valida Seguro
    if ($("#cmbTipoBienSeguro").val() == "001") {
        var decImporteSeguro = fn_util_ValidaDecimal($("#txtImportePrimaSeguroBien").val());
        var strCuotasSeguro = $("#txtNumCuotasfinanciadas").val();
        if (strCuotasSeguro == "") strCuotasSeguro = "0";

        if (decImporteSeguro <= 0) {
            strError.append('&nbsp;&nbsp;- El Importe Prima del Seguro debe ser mayor a cero<br />');
        }
        if (parseInt(strCuotasSeguro) <= 0) {
            strError.append('&nbsp;&nbsp;- Las Cuotas a Financiar del Seguro debe ser mayor a cero<br />');
        }
    }

    //Valida SeguroDegravamen
    if ($('#cmbTipoPersona').val() == "1") {
        if ($("#cmbTipoSeguro").val() == "001") {
            var decImporteSeguroDes = fn_util_ValidaDecimal($("#txtImportePrimaDesgravamen").val());
            var strCuotasSegurodes = $("#txtNumCuotaFinanciar").val();
            if (strCuotasSegurodes == "") strCuotasSegurodes = "0";

            if (decImporteSeguroDes <= 0) {
                strError.append('&nbsp;&nbsp;- El Importe Prima del Seguro de Desgravamen debe ser mayor a cero<br />');
            }
            if (parseInt(strCuotasSegurodes) <= 0) {
                strError.append('&nbsp;&nbsp;- Las Cuotas a Financiar del Seguro de Desgravamen debe ser mayor a cero<br />');
            }
        }
    }

    //Valida Fecha Activacion
    var strFrecuencia = $("#cmbFrecuenciaPago").val()
    var strFechaAct = $("#txtFechaMaxActivacion").val()
    var dateParts = strFechaAct.split("/");
    //alert(strFrecuencia);
    //alert(dateParts[2] + "/" + (dateParts[1] - 1) + "/" + dateParts[0]);
    var dtFechaActivacion = new Date(dateParts[2], (dateParts[1] - 1), dateParts[0]);
    var dtNuFechaVence = new Date(dtFechaActivacion);
    if (strFrecuencia == "AND") {
        dtNuFechaVence.setDate(dtNuFechaVence.getDate() + 360);
        $("#txtFechavence").val(fn_padStr(dtNuFechaVence.getDate()) + "/" + fn_padStr(1 + dtNuFechaVence.getMonth()) + "/" + fn_padStr(dtNuFechaVence.getFullYear()));
    }
    if (strFrecuencia == "BID") {
        dtNuFechaVence.setDate(dtNuFechaVence.getDate() + 60);
        $("#txtFechavence").val(fn_padStr(dtNuFechaVence.getDate()) + "/" + fn_padStr(1 + dtNuFechaVence.getMonth()) + "/" + fn_padStr(dtNuFechaVence.getFullYear()));
    }
    if (strFrecuencia == "SED") {
        dtNuFechaVence.setDate(dtNuFechaVence.getDate() + 180);
        $("#txtFechavence").val(fn_padStr(dtNuFechaVence.getDate()) + "/" + fn_padStr(1 + dtNuFechaVence.getMonth()) + "/" + fn_padStr(dtNuFechaVence.getFullYear()));
    }
    if (strFrecuencia == "MED") {
        dtNuFechaVence.setDate(dtNuFechaVence.getDate() + 30);
        $("#txtFechavence").val(fn_padStr(dtNuFechaVence.getDate()) + "/" + fn_padStr(1 + dtNuFechaVence.getMonth()) + "/" + fn_padStr(dtNuFechaVence.getFullYear()));
    }
    if (strFrecuencia == "TRD") {
        dtNuFechaVence.setDate(dtNuFechaVence.getDate() + 90);
        $("#txtFechavence").val(fn_padStr(dtNuFechaVence.getDate()) + "/" + fn_padStr(1 + dtNuFechaVence.getMonth()) + "/" + fn_padStr(dtNuFechaVence.getFullYear()));
    }

    /*var strMensajeFecActivacion = fn_validaFechaActivacion();
    if (fn_util_trim(strMensajeFecActivacion) != "") {
    strError.append(strMensajeFecActivacion);
    }*/


    //Valida si hay Error
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }
    else {

        /*if (!fn_validaCambioDataCronograma()) {//Datos han cambiado
        //alert("true");//Kina
        blnDatosCambiados = true;
        }*/
        //Inicializa Grilla
        fn_cargaGrillaCronograma();

        var arrParametros = [
        //Cabecera
            "pstrNroContrato", $("#txtNroContrato").val(),
            "pstrCodInstrDesembolso", $("#txtNroInstruccion").val(),
            "pstrTipoPersona", $("#hddTipoPersona").val(),
            "pstrPrecioVenta", $("#txtPrecioVentaCro").val(),
            "pstrMontoIGV", $("#txtMontoIGVCro").val(),
            "pstrValorVenta", $("#txtValorVentaCro").val(),
            "pstrCuotaInicial", $("#txtCuotaInicialCro").val(),
            "pstrRiesgoNeto", $("#txtRiesgoNetoCro").val(),
        //DatosGenerales :: Cronograma
            "pstrTipoCronograma", $("#cmbTipoCronograma").val(),
            "pstrNroCuotas", $("#txtNroCuotas").val(),
            "pstrPeriodicidad", $("#cmbPeriodicidad").val(),
            "pstrFrecuenciaPago", $("#cmbFrecuenciaPago").val(),
            "pstrPlazoGracia", $("#txtPlazoGracia").val(),
            "pstrTipoGracia", $("#cmbTipoGracia").val(),
            "pstrFechaActivacion", Fn_util_DateToString($("#txtFechaMaxActivacion").val()),
            "pstrFechavence", Fn_util_DateToString($("#txtFechavence").val()),
        //DatosGenerales :: Tasas
            "pstrTea", $("#txtTEA").val(),
        //DatosGenerales :: SeguroBien
            "pstrTipoBienSeguro", $("#cmbTipoBienSeguro").val(),
            "pstrImportePrimaSeguroBien", $("#txtImportePrimaSeguroBien").val(),
            "pstrNumCuotasfinanciadas", $("#txtNumCuotasfinanciadas").val(),
        //DatosGenerales :: SeguroDegravamen
            "pstrTipoSeguro", $("#cmbTipoSeguro").val(),
            "pstrImportePrimaDesgravamen", $("#txtImportePrimaDesgravamen").val(),
            "pstrNumCuotaFinanciar", $("#txtNumCuotaFinanciar").val(),
        ];


        fn_util_AjaxWM("frmInsDesembolsoRegistro.aspx/GeneraCronograma",
                 arrParametros,
                 function(jsondata) {
                     parent.fn_unBlockUI();
                     if (jsondata.FlagError == 1) {
                         parent.fn_mdl_mensajeIco(jsondata.MsgError, "util/images/error.gif", "ERROR EN CRONOGRAMA");
                     } else {
                         $("#tbCronograma").show();
                         jqGrid_lista_I.addJSONData(jsondata);

                         /* $("#hddIniTipoCronograma").val($("#cmbTipoCronograma").val());
                         $("#hddIniNroCuotas").val($("#txtNroCuotas").val());
                         $("#hddIniPeriodicidad").val($("#cmbPeriodicidad").val());
                         $("#hddIniFrecuenciaPago").val($("#cmbFrecuenciaPago").val());
                         $("#hddIniTEA").val($("#txtTEA").val());
                         $("#hddIniTipoBienSeguro").val($("#cmbTipoBienSeguro").val());
                         $("#hddIniPrecioVenta").val($("#txtPrecioVenta").val());

                         $("#hddIniCuotaIni").val($("#txtCuotaInicial").val());
                         $("#hddIniCuotaIniPorc").val($("#txtCuotaInicialPorc").val());
                         $("#hddIniIGVPorc").val($("#txtPorcIGV").val());
                         $("#hddIniTipoGracia").val($("#cmbTipoGracia").val());
                         $("#hddIniFecVenc").val($("#txtFechavence").val());
                         $("#hddIniFecMaxAct").val($("#txtFechaMaxActivacion").val());

                         $("#hddIniImportePrimaSeguroBien").val($("#txtImportePrimaSeguroBien").val());
                         $("#hddIniNumCuotasfinanciadas").val($("#txtNumCuotasfinanciadas").val());*/

                     }
                     fn_doResize();
                 },
                 function(resultado) {
                     var error = eval("(" + resultado.responseText + ")");
                     parent.fn_mdl_mensajeIco("No se pudo cargar el Cronograma.", "util/images/error.gif", "ERROR EN CRONOGRAMA");
                 }
        );

        //Activa Tab
        $("#tab4-tab").css("display", "block");
        $("div#divTabs").tabs("enable", [4]);
        $("div#divTabs").tabs("select", [4]);

    }
}

//************************************************************
// Función		:: 	fn_oc_periodicidad
// Descripcion 	:: 	Método Clasificacion
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_oc_periodicidad(strValor) {

    //var arrValor = strValor.split("*");

    var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL033", "pstrCodigoGenerico", strValor];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbFrecuenciaPago').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}

//****************************************************************
// Funcion		:: 	fn_of_PlazoGracia 
// Descripción	::	Valida PlazoGracia
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_of_PlazoGracia() {
    var txtPlazoGracia = $("#txtPlazoGracia").val();
    if (fn_util_trim(txtPlazoGracia) == "") {
        txtPlazoGracia = "0";
    }

    var strNroCuotas = $("#txtNroCuotas").val();
    if (fn_util_trim(strNroCuotas) == "") strNroCuotas = "0";
    var intNroCuotas = parseInt(strNroCuotas);
    var intPlazoGracia = parseInt(txtPlazoGracia);
    if (intPlazoGracia < 0 || intNroCuotas <= intPlazoGracia) {
        $("#txtPlazoGracia").val("0");
        parent.fn_util_MuestraLogPage("El Plazo de Gracia ingresado no es correcto", "E");
    }
    if (intPlazoGracia == 0) {
        $("#txtPlazoGracia").val("0");
    }
}

//****************************************************************
// Funcion		:: 	fn_ValidaGenWIOActParcial
// Descripción	::	Genera Wio
// Log			:: 	
//****************************************************************

function fn_ValidaGenWIOActParcial() {

    if (($("#chkActivacion").is(':checked')) && ($("#hddCodigoSubtipoContrato").val() == "002")) {
        var txtCuotaIni = $("#txtCuotaInicialCro").val()
        var txtCuotaIniContrato = $("#txtCuotaIniContrato").val()
        var txtMontoDesembolsoContrato = $("#txtValorVenta").val()
        var txtMontoDesembolsoRealizado = $("#txtValorVentaCro").val()
        var strWarning = new StringBuilderEx();
        if (parseFloat(txtCuotaIni) != parseFloat(txtCuotaIniContrato)) {
            strWarning.append("&nbsp;&nbsp; - La Cuota Inicial de la cotización es diferente a la Cuota Inicial adelantada. <br /> ");
        }
        if (parseFloat(txtMontoDesembolsoContrato) != parseFloat(txtMontoDesembolsoRealizado)) {
            strWarning.append("&nbsp;&nbsp; - El Valor venta desembolsado es diferente al firmado en el contrato. <br /> ");
        }
        if (strWarning.toString() != '') {
            strWarning.append("&nbsp;&nbsp;Debe generar Adenda previo a Activar. ¿Desea continuar?<br /> ");
            //Confirmacion de Eliminacion
            parent.fn_mdl_confirma(
            //"La cuota Inicial de la cotización es diferente a la Cuota Inicial adelantada. ¿Está seguro que desea generar el WIO?",
                strWarning.toString(),
                function() {
                    parent.fn_blockUI();

                    var arrParametros = ["pCodigoSolicitudCredito", $("#txtNroContrato").val(),
							             "pCodigoInstruccionDesembolso", $("#txtNroInstruccion").val()];

                    fn_util_AjaxSyncWM("frmInsDesembolsoRegistro.aspx/ValidaEjecucionActParcial",
							            arrParametros,
							            function(resultado) {
							                parent.fn_unBlockUI();
							                var varresult = resultado.split("|");
							                if (varresult[0] == "0") {
							                    //if (fn_util_trim(resultado) == "0") {							            
							                    fn_generarWIO();

							                } else {
							                    //parent.fn_mdl_mensajeIco("No se puede generar WIO. Falta completar Medios de pago.", "util/images/error.gif", "ERROR EN GENERACION WIO");
							                    parent.fn_mdl_mensajeIco(varresult[1], "util/images/error.gif", "ERROR EN GENERACION WIO");
							                }
							            },
							            function(resultado) {
							                parent.fn_unBlockUI();
							                var error = eval("(" + resultado.responseText + ")");
							                parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL DESEMBOLSAR");
							            }
		            );
                },
			    "Util/images/question.gif",
			    function() { },
			    'CONFIRMACIÓN'
            );
        } else {
            parent.fn_blockUI();

            var arrParametros = ["pCodigoSolicitudCredito", $("#txtNroContrato").val(),
							        "pCodigoInstruccionDesembolso", $("#txtNroInstruccion").val()];

            fn_util_AjaxSyncWM("frmInsDesembolsoRegistro.aspx/ValidaEjecucionActParcial",
						            arrParametros,
						            function(resultado) {
						                parent.fn_unBlockUI();
						                var varresult = resultado.split("|");
						                if (varresult[0] == "0") {
						                    //if (fn_util_trim(resultado) == "0") {							            
						                    fn_generarWIO();

						                } else {
						                    //parent.fn_mdl_mensajeIco("No se puede generar WIO. Falta completar Medios de pago.", "util/images/error.gif", "ERROR EN GENERACION WIO");
						                    parent.fn_mdl_mensajeIco(varresult[1], "util/images/error.gif", "ERROR EN GENERACION WIO");
						                }
						            },
						            function(resultado) {
						                parent.fn_unBlockUI();
						                var error = eval("(" + resultado.responseText + ")");
						                parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL DESEMBOLSAR");
						            }
		        );
        }
    } else {
        fn_generarWIO();
    }


}

//****************************************************************
// Funcion		:: 	fn_ExportarExcel
// Descripción	::	Exporta grilla a excel
// Log			:: 	
//****************************************************************

function fn_ExportarExcel() {
    $("#btnGenerar").click()
}
/* FIN IBK */


//****************************************************************
// Funcion		:: 	fn_abreVoucher 
// Descripción	::	ExportaVoucher
// Log			:: 	JJM IBK -  23/04/2013
//****************************************************************
function fn_abreVoucher(pstrCodSolicitudCredito, pstrCodInstruccionDesembolso, pstrCodCorrelativo) {
    //function fn_abreVoucher() {    
    window.open("frmDownloadInstr.aspx?CodSolicitudCredito=" + pstrCodSolicitudCredito + "&CodInstruccionDesembolso=" + pstrCodInstruccionDesembolso + "&CodCorrelativo=" + pstrCodCorrelativo);
    return false;

}
