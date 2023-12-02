var blnPrimeraBusqueda;
var intPaginaActual = 1;
var C_GESTIONBIEN_BIEN = '008';
var C_GESTIONBIEN_DEMANDA = '002';
var idsOfSelectedRows = [];

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	WCR - 04/01/2013
//****************************************************************
$(document).ready(function() {
    //Carga Grilla
    fn_cargaGrilla();
    fn_cargaGrillaDemanda()
    //$("#jqGrid_listado").setGridWidth($(window).width() - 65);

    //On load Page (siempre al final)
    fn_onLoadPage();

    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscar(true);
        }
    });

});

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 26/09/2012
//****************************************************************
function fn_cargaGrilla() {
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_buscarBien();
        },
        //        datatype: "local",
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Tipo de Bien', 'Descripción del Bien', 'Placa Actual', 'Marca', 'Modelo', 'Demanda', 'Aprobación', 'Docs', '', '', '', ''],
        colModel: [
            { name: 'TipoBien', index: 'TipoBien', width: 80, sorttype: "string", align: "left" },
            { name: 'DescripcionBien', index: 'DescripcionBien', width: 80, sorttype: "string", align: "left" },
		    { name: 'PlacaAnterior', index: 'PlacaAnterior', width: 25, sorttype: "string", align: "center" },
		    { name: 'Marca', index: 'Marca', width: 30, align: "left", sorttype: "string" },
        	{ name: 'Modelo', index: 'Modelo', width: 30, align: "left", sorttype: "string" },
		    { name: 'Demanda', index: 'Demanda', width: 20, align: "center", sortable: false, sorttype: "string", formatter: fn_Ver },
		    { name: 'Aprobacion', index: 'Aprobacion', width: 25, sortable: false, align: "center", formatter: fn_Aprobacion },
		    { name: 'Doc', index: 'Doc', align: "center", sortable: false, formatter: fn_abreDocumentos, width: 20 },
		    { name: 'CantidadDemanda', index: 'CantidadDemanda', hidden: true },
		    { name: 'CodSolicitudCredito', index: 'CantidadDemanda', hidden: true },
		    { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
		    { name: 'FlagAprobacion', index: 'FlagAprobacion', hidden: true }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 5,
        rowList: [10, 20, 30],
        sortname: 'SecFinanciamiento',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#jqGrid_lista_B").GridUnload();
            fn_cargaGrillaDemanda();
            if (parseInt(rowData.CantidadDemanda, 10) > 0) { fn_BuscaDemanda(rowData.SecFinanciamiento); }
        },
        ondblClickRow: function() {
            //           parent.fn_blockUI();
            //            fn_util_redirect('frmMantenimientoBienContrato.aspx?co=1&csc=' + $("#hidCodigoSolicitudCredito").val());
        }
    });


    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

    function fn_Ver(cellvalue, options, rowObject) {
        var obj = '';
        if (parseInt(rowObject.CantidadDemanda, 10) > 0) {
            obj = "<img src='../../Util/images/ok.gif' alt='" + cellvalue + "' title='Ver' width='20px' style='cursor: pointer;cursor: hand;' />";
        }
        return obj;
    };

    function fn_Aprobacion(cellvalue, options, rowObject) {
        var obj = '';
        if (parseInt(rowObject.CantidadDemanda, 10) > 0) {
            var strCheck = '';
            if (rowObject.FlagAprobacion == '1') { strCheck = "checked='checked'"; }
            obj = "<input type='Checkbox' id='chkAprobacion' onchange='javascript:fn_CheckAprobacion(this," + rowObject.SecFinanciamiento + ");' " + strCheck + " />";
        }
        else {
            obj = "<input type='Checkbox' id='chkAprobacion' checked='checked' disabled='disabled' />";
        }

        return obj;
    };

    //**************************************************
    // Documentos
    //**************************************************
    function fn_abreDocumentos(cellvalue, options, rowObject) {
        return "<img src='../../Util/images/ico_docs.gif' alt='Ver Documentos' title='Ver Documentos' width='18px' onclick=\"javascript:fn_GBAbreDocumentos(\'" + rowObject.CodSolicitudCredito + "\',\'" + rowObject.SecFinanciamiento + "',\'0\',\'" + C_GESTIONBIEN_BIEN + "\');\" style='cursor:pointer;' />";
    };
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_CheckAprobacion
// Descripción	::	Check Aprobación
// Log			:: 	WCR - 16/01/2013
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_CheckAprobacion(pControl, pSecFinan) {
    var index = $.inArray(pSecFinan, idsOfSelectedRows);
    if (pControl.checked) {
        if (index < 0) { idsOfSelectedRows.push(pSecFinan); }
    }
    else {
        if (index >= 0) { idsOfSelectedRows.splice(index, 1); }
    }

}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos(pstrCodContrato, pstrCodBien, pstrCodRelacionado, pstrCodTipo) {
    parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + "&hddVer=0", 800, 350, function() { });
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_guardar
// Descripción	::	Grabar Aprobación
// Log			:: 	WCR - 16/01/2013
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_guardar() {
    parent.fn_blockUI();
    var strAprobacionDemanda = '';
    for (var i = 0; i < idsOfSelectedRows.length; i++) {
        strAprobacionDemanda = strAprobacionDemanda + idsOfSelectedRows[i] + '*';
    }
    var strNumeroContrato = $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();

    var arrParametros = ["pstrNumeroContrato", strNumeroContrato,
                         "pstrBienes", strAprobacionDemanda
                        ];

    fn_util_AjaxWM("frmListadoBienes.aspx/GrabaAprobacion",
                    arrParametros,
                    function(result) {
                        parent.fn_unBlockUI();
                        if (fn_util_trim(result) == "0") {
                            parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "GRABAR ENVIO");
                        } else {
                            fn_util_MuestraLogPage('Los datos se grabaron satisfactoriamente', "I");
                            intPaginaActual = 1;
                            fn_buscarBien();
                            parent.fn_unBlockUI();
                        }
                    },
                     function(resultado) {
                         parent.fn_unBlockUI();
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "GRABAR ENVIO");
                     }

                );
}

//****************************************************************
// Funcion		:: 	fn_buscarBien
// Descripción	::	
// Log			:: 	WCR - 08/01/2013
//****************************************************************
function fn_buscarBien() {

    var strNumeroContrato = $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();

    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", intPaginaActual,    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
                             "pNumeroContraro", strNumeroContrato
                            ];

    fn_util_AjaxWM("frmOpcionCompraRegistro.aspx/BuscarBien",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_A.addJSONData(jsondata);
                        parent.fn_unBlockUI();
                        fn_doResize();
                    },
                    function(request) {
                        parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "BUSCAR BIEN");
                        parent.fn_unBlockUI();
                        fn_doResize();
                    }
                   );


}

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla Listado de Cotizaciones
// Log			:: 	JRC - 07/11/2012
//****************************************************************
function fn_cargaGrillaDemanda() {

    $("#jqGrid_lista_B").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_B", "page");
            //            fn_BuscaDemanda();
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
        colNames: ['', '', '', 'Nº Demanda', 'Fecha Demanda', 'Nº Siniestro', 'Estado Demanda', 'Seguro', 'Estado del Bien', 'Nº Poliza', 'Aplicación del Fondo', 'Juzgado', 'F.Última Actualización', 'Ver', 'Docs.', ''],
        colModel: [
				{ name: 'CodDemanda', index: 'CodDemanda', hidden: true },
				{ name: 'CodSiniestro', index: 'CodSiniestro', hidden: true },
				{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
                { name: 'NroDemanda', index: 'NroDemanda', sortable: true, sorttype: "int", align: "center", defaultValue: "", width: 100 },
                { name: 'FechaDemanda', index: 'FechaDemanda', sortable: true, sorttype: "string", align: "center", defaultValue: "", width: 90 },
                { name: 'NroSiniestro', index: 'NroSiniestro', sortable: true, sorttype: "string", align: "left", defaultValue: "", width: 100 },
                { name: 'DesEstadoDemanda', index: 'DesEstadoDemanda', sortable: true, sorttype: "string", align: "left", defaultValue: "" },
                { name: 'DesSeguro', index: 'DesSeguro', sortable: true, sorttype: "string", align: "left", defaultValue: "" },
                { name: 'DesEstadoBien', index: 'DesEstadoBien', sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'NroPoliza', index: 'NroPoliza', sortable: true, sorttype: "string", align: "center", defaultValue: "", width: 90 },
                { name: 'DesAplicaFondo', index: 'DesAplicaFondo', sortable: true, sorttype: "string", align: "center", defaultValue: "", hidden: true },
                { name: 'Juzgado', index: 'Juzgado', sortable: true, sorttype: "string", align: "center", defaultValue: "" },
                { name: 'FecSituacion', index: 'FecSituacion', sortable: true, sorttype: "string", align: "center", defaultValue: "", width: 120 },
                { name: 'Ver', index: 'Ver', align: "center", sortable: false, formatter: fn_VerD, width: 45 },
                { name: 'Doc', index: 'Doc', align: "center", sortable: false, formatter: fn_abreDocumentosD, width: 45 },
                { name: '', index: '', width: 5 }
        ],
        width: glb_intWidthPantalla - 70,
        height: '100%',
        pager: '#jqGrid_pager_B',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 5,
        rowList: [10, 20, 30],
        sortname: 'NroDemanda',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
            $("#hddCodDemanda").val(rowData.CodDemanda);
            $("#hddCodSiniestro").val(rowData.CodSiniestro);
        }
    });
    jQuery("#jqGrid_lista_B").jqGrid('navGrid', '#jqGrid_pager_B', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_B").hide();



}

//**************************************************
// Documentos
//**************************************************
function fn_abreDocumentosD(cellvalue, options, rowObject) {
    //return ".";
    return "<img src='../../Util/images/ico_docs.gif' alt='Ver Documentos' title='Ver Documentos' width='18px' onclick=\"javascript:fn_GBAbreDocumentosD(\'" + rowObject.CodSolCredito + "\',\'" + rowObject.SecFinanciamiento + "\',\'" + rowObject.CodDemanda + "\',\'" + C_GESTIONBIEN_DEMANDA + "\');\" style='cursor:pointer;' />";
};

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentosD(pstrCodContrato, pstrCodBien, pstrCodRelacionado, pstrCodTipo) {
    var strVer = '1';
    parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + "&hddVer=" + strVer, 800, 350, function() { });
}

//**************************************************
// Ver Demanda
//**************************************************
function fn_VerD(cellvalue, options, rowObject) {
    //return ".";
    return "<img src='../../Util/images/ico_acc_ver.gif' alt='Ver Demanda' title='Ver Demanda' width='18px' onclick=\"javascript:fn_VerDemanda(\'" + rowObject.CodDemanda + "\',\'" + rowObject.CodSiniestro + "\','" + rowObject.SecFinanciamiento + "');\" style='cursor:pointer;' />";
};


function fn_VerDemanda(pCodDemanda, pCodSiniestro, pCodBien) {
    parent.fn_util_AbreModal2("Demanda :: Consulta", "GestionBien/OpcionCompra/frmDemandaCons.aspx?hddCodContrato=" + $('#hddCodContrato').val() + "&hddCodBien=" + pCodBien + "&hddCodSiniestro=" + pCodSiniestro + "&hddCodDemanda=" + pCodDemanda + "&hddVer=0", 950, 550, function() { });
}

//****************************************************************
// Funcion		:: 	fn_buscaDemanda
// Descripción	::	Busca listado por parametros
// Log			:: 	JRC - 07/11/2012
//****************************************************************
function fn_BuscaDemanda(pCodBien) {
    try {
        parent.fn_blockUI();

        var strNumeroContrato = $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();

        $('#hddCodBien').val(pCodBien);

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_B", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_B", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_B", "sortorder"), // Criterio de ordenación
                             "pstrCodContrato", strNumeroContrato,
                             "pstrCodBien", pCodBien
                            ];

        fn_util_AjaxWM("frmListadoBienes.aspx/ListaDemanda",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_B.addJSONData(jsondata);
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
        parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "BUSCAR BIEN");
    }

}