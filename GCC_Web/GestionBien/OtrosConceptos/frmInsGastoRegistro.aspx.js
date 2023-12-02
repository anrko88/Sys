//****************************************************************
// Variables Globales
//****************************************************************


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

    //Documentos
    //	fn_iniGrillaDocumento();

    //Inicializa Grillas
    fn_cargaGrillaDocumentos();
    fn_cargaGrillaProveedores();
    fn_cargaGrillaSunat();

    //On load Page (siempre al final)
    fn_onLoadPage();

});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_inicializaCampos() {

}


//****************************************************************
// Funcion		:: 	fn_cargaGrillaDocumentos
// Descripción	::	Inicializa Grilla Documentos
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_cargaGrillaDocumentos() {

    try {

        $("#jqGrid_lista_A").jqGrid({
            //			datatype: function() {
            //				fn_cargaAgrupacion();
            //			},
            datatype: "local",
            jsonReader:
			{
			    root: "Items",
			    page: "CurrentPage",
			    total: "PageCount",
			    records: "RecordCount",
			    repeatitems: false,
			    id: "Id"
			},
            colNames: ['Tipo Documento', 'Nro. Documento', 'Razón Social o Nombre', 'Cant. Documentos', 'Moneda', 'Importe Total', 'Desc. Sunat', 'Importe Abonar'],
            colModel: [
					{ name: 'TipoDocumento', index: 'NombreProveedor', width: 40, align: "center" },
					{ name: 'NumeroDocumento', index: 'NumeroDocumento', width: 40, align: "center" },
					{ name: 'RazonSocial', index: 'RazonSocial', align: "left" },
					{ name: 'CantidadDocumentos', index: 'CantidadDocumentos', width: 40, align: "center" },
					{ name: 'NombreMoneda', index: 'NombreMoneda', width: 55, align: "center" },
					{ name: 'Importe', index: 'Importe', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
					{ name: 'MontoDescSunat', index: 'MontoDescSunat', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
					{ name: 'MontoTotal', index: 'MontoTotal', width: 50, align: "right", formatter: Fn_util_ReturnValidDecimal2 }
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
                //				$("#hddCodigoAgrupacion").val(rowDataPadre.CodCorrelativo);			

                var subgrid_table_id, pager_id;
                subgrid_table_id = subgrid_id + "_t";
                pager_id = "p_" + subgrid_table_id;
                $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pager_id + "' class='scroll'></div>");
                jQuery("#" + subgrid_table_id).jqGrid({
                    //					datatype: function() {					
                    //						fn_cargaAgrupacionDocumento(subgrid_table_id);
                    //					},
                    datatype: "local",
                    jsonReader:
					{
					    root: "Items",
					    page: "CurrentPage",
					    total: "PageCount",
					    records: "RecordCount",
					    repeatitems: false,
					    id: "Id"
					},
                    colNames: ['Tipo Comprobante', 'Nro.Comprobante', 'Fecha Emisión', 'TC IBK', 'TC SUNAT', 'Importe', 'Desc. SUNAT', 'Importe Total'],
                    colModel: [
						{ name: 'TipoComprobante', index: 'TipoComprobante', align: "left" },
						{ name: 'NroComprobante', index: 'NroComprobante', width: 70, align: "center" },
						{ name: 'FechaEmision', FechaEmision: 'FechaEmision', width: 50, align: "center" },
						{ name: 'TCDia', index: 'TCDia', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
						{ name: 'TCSunat', index: 'TCSunat', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
						{ name: 'Importe', index: 'Importe', width: 50, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
						{ name: 'DctoSunat', index: 'DctoSunat', width: 60, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
						{ name: 'Total', index: 'Total', width: 60, align: "right", formatter: Fn_util_ReturnValidDecimal2 }
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


                if (subgrid_table_id == "jqGrid_lista_A_1_t") {
                    var arrDocumentos1 = [
							{ TipoComprobante: "FACTURA", NroComprobante: "1000012414", FechaEmision: '20/11/2012', TCDia: "2.61", TCSunat: "2.58", Importe: "800.00", DctoSunat: "0.00", Total: "800.00" },
							{ TipoComprobante: "FACTURA", NroComprobante: "1000000261", FechaEmision: '19/11/2012', TCDia: "2.62", TCSunat: "2.55", Importe: "700.00", DctoSunat: "200.00", Total: "500.00" }
							];
                    for (var i = 0; i <= arrDocumentos1.length; i++) {
                        jQuery("#jqGrid_lista_A_1_t").jqGrid('addRowData', i + 1, arrDocumentos1[i]);
                    }
                }
                if (subgrid_table_id == "jqGrid_lista_A_2_t") {
                    var arrDocumentos2 = [
							{ TipoComprobante: "BOLETA", NroComprobante: "1000012414", FechaEmision: '21/11/2012', TCDia: "2.65", TCSunat: "2.52", Importe: "850.00", DctoSunat: "50.00", Total: "800.00" }
							];
                    for (var i = 0; i <= arrDocumentos2.length; i++) {
                        jQuery("#jqGrid_lista_A_2_t").jqGrid('addRowData', i + 1, arrDocumentos2[i]);
                    }
                }
            }

        });
        jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_lista_A', { edit: false, add: false, del: false });
        $("#search_jqGrid_lista_A").hide();

        var arrAgrupado = [
					{ TipoDocumento: "RUC", NroDocumento: "1015478944", RazonSocial: "TX DEVELOPERS S.A.C.", CantidadDocumentos: "2", NombreMoneda: "DOLARES AMERICANOS", Importe: "1500.00", MontoDescSunat: "200.00", MontoTotal: "1300.00" },
					{ TipoDocumento: "RUC", NroDocumento: "1015478944", RazonSocial: "TX DEVELOPERS S.A.C.(Detracción) FACTURA N° 1000012414", CantidadDocumentos: "0", NombreMoneda: "DOLARES AMERICANOS", Importe: "200.00", MontoDescSunat: "0.00", MontoTotal: "200.00" },
					{ TipoDocumento: "RUC", NroDocumento: "1665142514", RazonSocial: "MANPER S.A.C.", CantidadDocumentos: "1", NombreMoneda: "NUEVOS SOLES", Importe: "850.00", MontoDescSunat: "50.00", MontoTotal: "800.00" }
					];
        for (var i = 0; i <= arrAgrupado.length; i++) {
            jQuery("#jqGrid_lista_A").jqGrid('addRowData', i + 1, arrAgrupado[i]);
        }

    } catch (e) {
        alert(e.message);
    }

}


//****************************************************************
// Funcion		:: 	fn_cargaGrillaProveedores
// Descripción	::	Inicializa Grilla 
// Log			:: 	WCR - 21/11/2012
//****************************************************************
function fn_cargaGrillaProveedores() {

    $("#jqGrid_lista_B").jqGrid({
        //        datatype: function() {
        //            fn_listaCargoAbono("B");
        //        },
        datatype: "local",
        jsonReader:
        {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Razón Social', 'Cant. Doc', 'Moneda', 'Total Importe', 'Desc. Sunat', 'Importe Total', 'Medio Pago', '_'],
        colModel: [
		        { name: 'RazonSocial', index: 'RazonSocial', align: "left" },
		        { name: 'CantidadDocumentos', index: 'CantidadDocumentos', width: 40, align: "center" },
		        { name: 'NombreMoneda', index: 'NombreMoneda', width: 60, align: "center" },
		        { name: 'Importe', index: 'Importe', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MontoDescSunat', index: 'MontoDescSunat', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'Total', index: 'Total', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MedioAbono', index: 'MedioAbono', width: 40, align: "center" },
		        { name: '', index: '', width: 35, align: "center", formatter: fn_medioPago }
        ],
        width: glb_intWidthPantalla - 170,
        //width: 950,
        height: '100%',
        //pager: '#jqGrid_pager_B',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 50,
        //rowList: [10, 20, 30],
        sortname: 'Id',
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

    var arrData = [
				{ RazonSocial: "TX DEVELOPERS S.A.C.", CantidadDocumentos: "2", NombreMoneda: "DOLARES AMERICANOS", Importe: "1500.00", MontoDescSunat: "200.00", Total: "1300.00", MedioAbono: '' },
				{ RazonSocial: "MANPER S.A.C.", CantidadDocumentos: "1", NombreMoneda: "NUEVOS SOLES", Importe: "850.00", MontoDescSunat: "50.00", Total: "800.00", MedioAbono: '' }
					];
    for (var i = 0; i <= arrData.length; i++) {
        jQuery("#jqGrid_lista_B").jqGrid('addRowData', i + 1, arrData[i]);
    }

    //**************************************************
    // MedioPago
    //**************************************************
    function fn_medioPago(cellvalue, options, rowObject) {
        return "<img src='../../Util/images/ico_mdl_checklist.gif' alt='" + cellvalue + "' title='Medio Pago' width='18px' onclick=\"javascript:fn_abreMedioPago();\" style='cursor:pointer;'/>";
    };

}



//****************************************************************
// Funcion		:: 	fn_cargaGrillaSunat
// Descripción	::	Inicializa Grilla 
// Log			:: 	JRC - 20/09/2012
//****************************************************************
function fn_cargaGrillaSunat() {

    $("#jqGrid_lista_C").jqGrid({
        //        datatype: function() {
        //            fn_listaCargoAbono("C");
        //        },
        datatype: "local",
        jsonReader:
        {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Tipo', 'Razón Social', 'N° Comprobante', 'Moneda', 'Importe', 'Importe S/.', 'Tc Sunat', 'Medio Pago', '_'],
        colModel: [
		        { name: 'TipoAgrupacion', index: 'TipoAgrupacion', align: "left", width: 80 },
		        { name: 'RazonSocial', index: 'RazonSocial', align: "left" },
		        { name: 'NroDocumento', index: 'NroDocumentoDUA', width: 60, align: "center" },
		        { name: 'NombreMoneda', index: 'NombreMoneda', width: 60, align: "center" },
		        { name: 'MontoTotalPago', index: 'MontoTotalPago', width: 40, align: "center", formatter: Fn_util_ReturnValidDecimal2 },
		        { name: 'MontoSunat', index: 'MontoSunat', width: 40, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'TCSunat', index: 'TCSunat', width: 40, align: "center" },
		        { name: 'MedioAbono', index: 'MedioAbono', width: 40, align: "center" },
		        { name: '', index: '', width: 15, align: "center", formatter: fn_medioPagoS }
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
    jQuery("#jqGrid_lista_C").jqGrid('navGrid', '#jqGrid_lista_C', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_C").hide();

    var arrData = [
				{ TipoAgrupacion: 'Detracción', RazonSocial: "TX DEVELOPERS S.A.C.", NroDocumento: "1000012414", NombreMoneda: "DOLARES AMERICANOS", MontoTotalPago: '200.00', MontoSunat: '512.00', TCSunat: '2.5600', MedioAbono: '' },
					];
    for (var i = 0; i <= arrData.length; i++) {
        jQuery("#jqGrid_lista_C").jqGrid('addRowData', i + 1, arrData[i]);
    }

    //**************************************************
    // MedioPago
    //**************************************************
    function fn_medioPagoS(cellvalue, options, rowObject) {
        return "<img src='../../Util/images/ico_mdl_checklist.gif' alt='" + cellvalue + "' title='Medio Pago' width='18px' onclick=\"javascript:fn_abreMedioPago();\" style='cursor:pointer;'/>";
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
                        fn_util_globalRedirect("GestionBien/OtrosConceptos/frmInsGastoListado.aspx");
                    },
                    "Util/images/question.gif",
                    function() { },
                    'CONFIRMACION'
                   );
}


function fn_ocultaBloques(pstrBloque) {
    //	var intCantidad = $("#jqGrid_lista_"+pstrBloque).getGridParam("reccount");
    //	
    //	if(parseInt(intCantidad) == 0){
    //		if(fn_util_trim(pstrBloque)=="C"){
    //			$("#tr_clientes").hide();
    //			$("#grd_clientes").hide();
    //		}		
    //		if(fn_util_trim(pstrBloque)=="D"){
    //			$("#tr_sunat").hide();
    //			$("#grd_sunat").hide();
    //		}	
    //		if(fn_util_trim(pstrBloque)=="E"){
    //			$("#tr_difCambio").hide();
    //			$("#grd_difCambio").hide();
    //		}	
    //	}else{
    //		if(fn_util_trim(pstrBloque)=="C"){
    //			$("#tr_clientes").show();
    //			//$("#grd_clientes").show();
    //		}		
    //		if(fn_util_trim(pstrBloque)=="D"){
    //			$("#tr_sunat").show();
    //			//$("#grd_sunat").show();
    //		}	
    //		if(fn_util_trim(pstrBloque)=="E"){
    //			$("#tr_difCambio").show();
    //			//$("#grd_difCambio").show();
    //		}
    //		if(fn_util_trim(pstrBloque)=="F"){
    //			$("#tr_cargos").show();
    //			//$("#grd_cargos").show();
    //		}
    //	}
    //	
}


//****************************************************************
// Funcion		:: 	fn_cargaAgrupacion  
// Descripción	::	Carga Lista 
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_abreListadoDocumentos() {
	parent.fn_blockUI();
	parent.fn_util_AbreModal("Instrucción Gasto :: Listado Documentos", "GestionBien/OtrosConceptos/frmDocumentosGastoListado.aspx", 950, 500, function() { });
}


//****************************************************************
// Funcion		:: 	fn_abreMedioPago 
// Descripción	::	Carga Medio de Pago
// Log			:: 	JRC - 03/10/2012
//****************************************************************
function fn_abreMedioPago(pstrCodSolicitudCredito, pstrCodInstruccionDesembolso, pstrcodagrupacion, pstrCodProveedor, pstrCodMonedaPago, pstrCodGrupoHtml, pstrCodunico, pstModover) {
    //	parent.fn_blockUI();
    //	parent.fn_util_AbreModal("Instrucción Desembolso :: Medio Pago", "InsDesembolso/frmMediosPagoRegistro.aspx?cc=" + pstrCodSolicitudCredito + "&cid=" + pstrCodInstruccionDesembolso + "&ca=" + pstrcodagrupacion + "&cah=" + pstrCodGrupoHtml + "&cma=" + pstrCodMonedaPago + "&pro=" + pstrCodProveedor + "&cu=" + pstrCodunico + "&acc=" + pstModover, 950, 270, function() { });
}


//****************************************************************
// Funcion		:: 	fn_ActualizarGrupos  
// Descripción	::	Carga Listas 
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_ActualizarGrupos() {
    //	var strGrupoHtml = $("#hddCodAgrupacionModal").val();
    //	fn_listaCargoAbono(strGrupoHtml);
    //	fn_cargaDatosID();
}

//****************************************************************
// Funcion		:: 	fn_actualizaListaCargo  
// Descripción	::	Carga Listas 
// Log			:: 	JRC - 19/09/2012
//****************************************************************
function fn_actualizaListaCargo(strGrupoHtml) {
    //	fn_listaCargoAbono(strGrupoHtml);
    //	fn_cargaDatosID();
}


//****************************************************************
// Funcion		:: 	fn_volverDocumento
// Descripción	::	Volver a desembolso de documentos
// Log			:: 	
//****************************************************************
function fn_volverDocumento() {

}

//****************************************************************
// Funcion		:: 	fn_iniGrillaDocumento 
// Descripción	::	Inicializa Grilla Documento
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_iniGrillaDocumento() {
    //    
    //    $("#jqGrid_lista_D").jqGrid({
    //        datatype: function() {
    //			fn_cargaGrillaDocumento();	
    //        },
    //        jsonReader:
    //        {
    //            root: "Items",
    //            page: "CurrentPage",
    //            total: "PageCount",
    //            records: "RecordCount",
    //            repeatitems: false,
    //            id: "Id"
    //        },
    //        colNames: ['Codigo','Nombre Archivo', 'Adjunto', 'Comentario'],
    //        colModel: [
    //                { name: 'CodigoDocumento', index: 'CodigoDocumento', hidden:true },
    //		        { name: 'NombreArchivo', index: 'NombreArchivo', width: 200, align: "left", sorttype: "string", defaultValue: "" },
    //		        { name: 'RutaArchivo', index: 'RutaArchivo', width: 100, align: "Center", sortable: false, formatter: fn_icoDownload },
    //		        { name: 'Comentario', index: 'Comentario', width: 550, align: "left", sorttype: "string", defaultValue: "" }
    //	    ],
    //        height: '100%',
    //        pager: '#jqGrid_pager_D',
    //        loadtext: 'Cargando datos...',
    //        emptyrecords: 'No hay resultados',
    //        rowNum: 10,
    //        rowList: [10, 20, 30],
    //        sortname: 'CodigoDocumento',
    //        sortorder: 'desc',
    //        viewrecords: true,
    //        gridview: true,
    //        autowidth: true,
    //        altRows: true,
    //        loadonce: false,
    //        altclass: 'gridAltClass',
    //        onSelectRow: function(id) {
    //            var rowData = $("#jqGrid_lista_D").jqGrid('getRowData', id);
    //            $("#hddCodigoDocumento").val(rowData.CodigoDocumento);
    //        },
    //        ondblClickRow: function(id) {
    //        }
    //    });
    //    jQuery("#jqGrid_lista_D").jqGrid('navGrid', '#jqGrid_pager_D', { edit: false, add: false, del: false });
    //    $("#search_jqGrid_lista_D").hide();
    //    
    //    //Abrir Archivo
    //    function fn_icoDownload(cellvalue, options, rowObject) {
    //        var strNombreArchivo = rowObject.RutaArchivo.split('\\').pop();
    //        strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
    //        if (fn_util_trim(rowObject.RutaArchivo) != "") {
    //            return "<img src='../Util/images/ico_download.gif' alt='" + strNombreArchivo + "' title='" + strNombreArchivo + "' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.RutaArchivo) + "');\" style='cursor:pointer;'/>";
    //        } else {
    //            return ".";
    //        }
    //    };
    //    
}

//****************************************************************
// Funcion		:: 	fn_abreMedioPago 
// Descripción	::	Carga Medio de Pago
// Log			:: 	WCR - 21/11/2012
//****************************************************************
function fn_abreMedioPago() {
    parent.fn_blockUI();
    parent.fn_util_AbreModal("Instrucción Gasto :: Medio Pago", "GestionBien/OtrosConceptos/frmMedioPagoRegistro.aspx", 900, 270, function() { });
}