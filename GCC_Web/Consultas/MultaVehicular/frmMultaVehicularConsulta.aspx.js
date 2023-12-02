//****************************************************************
// Variables Globales
//****************************************************************
var blnPrimeraBusqueda;
var intPaginaActual = 1;

var strDepartamento = "15";
var strProvincia = "01";
//var srtSecImpuesto = "";

var C_GESTIONBIEN_MULTAVEHICULAR = "004";

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	AEP - 08/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos

    //fn_SeteaMunicipalidad();

    fn_inicializaCampos();

    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscar(true);
        }
    });

    //Carga Grilla

    if ($("#hddFecTransferencia").val() != '') {
        blnPrimeraBusqueda = true;
        fn_cargaGrilla();
        $("#ddlMunicipalidad").val($("#hddCodMunicipalidad").val());
        //$("#tdMunicipalidad").html($('select[name=ddlMunicipalidad] option:selected').text());
        parent.fn_util_AbreModal("Gestión del Bien :: Consulta", "Consultas/MultaVehicular/frmMultaVehicularVer.aspx?codImp=" + $("#hddCodMulta").val() + "&placa=" + $("#hddPlaca").val() + "&origen=2" + "&codMuni=" + $("#ddlMunicipalidad").val() + "&fechaT=" + $("#hddFecTransferencia").val(), 900, 450, function() {
        });

    } else {
        if ($("#hddTipo").val() == '1') {
            blnPrimeraBusqueda = true;
            fn_cargaGrilla();
            $("#ddlMunicipalidad").val($("#hddCodMunicipalidad").val());
            //$("#tdMunicipalidad").html($('select[name=ddlMunicipalidad] option:selected').text());
            parent.fn_util_AbreModal("Gestión del Bien :: Consulta", "Consultas/MultaVehicular/frmMultaVehicularVer.aspx?codImp=" +
            		  			     $("#hddCodMulta").val() + "&placa=" + $("#hddPlaca").val() + "&origen=2" + "&codMuni=" + $("#ddlMunicipalidad").val() +
			  			             "&fechaT=" + $("#hddFecTransferencia").val() + "&EstPago=" + $("#hddEstadoPago").val(), 900, 450, function() {
			  			             });
        } else {
            fn_cargaGrilla();
        }
    }

    fn_cargaGrillaMultas();



    //On load Page (siempre al final)
    fn_onLoadPage();


});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {
    $("#ddlMunicipalidad").attr('disabled', 'disabled');
//    if ($("#hddTipo").val() == "1") {

//    	//alert(fn_util_trim($("#hddCodMunicipalidad").val()));
//        //$("#ddlMunicipalidad").val(fn_util_trim($("#hddCodMunicipalidad").val()));
//    	//$("#ddlMunicipalidad").val("010");
//    	//$("#tdMunicipalidad").html($('select[name=ddlMunicipalidad] option:selected').text());
//    }
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
            fn_ListarBienes();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Nro. Contrato', 'CU Cliente', 'Razón Social o Nombre', 'Placa Actual', 'Municipalidad', 'Marca', 'Modelo', 'Nº Motor', '', '', '', '', ''],
        colModel: [
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 50, sorttype: "string", align: "center" },
            { name: 'CodUnico', index: 'CodUnico', width: 50, sorttype: "string", align: "left" },
            { name: 'RazonSocial', index: 'RazonSocial', width: 150, sorttype: "string", align: "left" },
        	{ name: 'Placa', index: 'Placa', width: 50, align: "center", sorttype: "string" },
		    { name: 'Municipalidad', index: 'Municipalidad', width: 50, align: "center", sorttype: "string" },
		    { name: 'Marca', index: 'Marca', width: 50, align: "center", sorttype: "string" },
        	{ name: 'Modelo', index: 'Modelo', width: 50, align: "center", sorttype: "string" },
        	{ name: 'NroMotor', index: 'NroMotor', width: 50, align: "center", sorttype: "string", editable: true },
        	{ name: '', index: '', width: 1 },
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
            { name: 'CodigoEstadoContrato', index: 'CodigoEstadoContrato', hidden: true },
        	{ name: 'CodMunicipalidad', index: 'CodMunicipalidad', hidden: true },
        	{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 5,
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
        ondblClickRow: function() {

        }
    });

    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

}

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_buscar(bSearch) {
    blnPrimeraBusqueda = bSearch;
    fn_ListarBienes();
}

//****************************************************************
// Funcion		:: 	fn_ListarBienes
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	AEP - 26/11/2012
//****************************************************************
function fn_ListarBienes() {
    if (!blnPrimeraBusqueda) {
        return;
    } else {


        parent.fn_blockUI();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"),
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"),
    	                 "pCodMunicipalidad", fn_util_trim($("#ddlMunicipalidad").val()),
    	                 "pPlaca", $("#hddPlaca").val(),
    	                 "pTipo", $("#hddTipo").val()
                         ];

        fn_util_AjaxWM("frmMultaVehicularConsulta.aspx/ListaBienesVehicular",
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
// Funcion		:: 	fn_cargaGrillaImpuestos
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 08/11/2012
//****************************************************************

var idsOfSelectedRows = [];
function fn_cargaGrillaMultas() {
    srtSecImpuesto = '';

    $("#jqGrid_lista_B").jqGrid({
        datatype: function() {
            fn_ListarMultasLote();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "SecMulta"
        },
        colNames: ['Placa Actual', 'Nº Infracción', 'F. Infracción', 'Concepto', 'Código Infracción', 'F. de Registro',
        	       'F. Recepcion del Banco', 'Importe', 'Importe Descuento', 'Municipalidad', 'F. de Pago', 'Estado de Pago',
        	       'Observación', '', '', '', 'Docs.', '', ''],
        colModel: [
            { name: 'Placa', index: 'Placa', width: 50, sorttype: "string", align: "left" },
            { name: 'NroInfraccion', index: 'NroInfraccion', width: 50, sorttype: "string", align: "left" },
        	{ name: 'FecInfraccion', index: 'FecInfraccion', width: 50, sorttype: "string", align: "left" },
        	{ name: 'TipoInfraccion', index: 'TipoInfraccion', width: 50, sorttype: "string", align: "left" },
            { name: 'CodigoInfraccion', index: 'CodigoInfraccion', width: 50, sorttype: "string", align: "left" },
            { name: 'FecIngreso', index: 'FecIngreso', width: 50, align: "center", sorttype: "string" },
            { name: 'FecRecepcionBanco', index: 'FecRecepcionBanco', width: 60, align: "center" },
            { name: 'Importe', index: 'Importe', width: 50, align: "center", sorttype: "string", formatter: Fn_util_ReturnValidDecimal2 },
        	{ name: 'ImporteDescuento', index: 'ImporteDescuento', width: 50, align: "center", sorttype: "string", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'Municipalidad', index: 'Municipalidad', width: 50, align: "center", sorttype: "string", editable: true },
        	{ name: 'FechaPago', index: 'FechaPago', width: 60, align: "center", sorttype: "string" },
		    { name: 'EstadoPago', index: 'EstadoPago', width: 50, align: "center", sorttype: "string" },
        	{ name: 'Observaciones', index: 'Observaciones', width: 50, align: "center", sorttype: "string" }, //,formatter:Lupa2  },
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
            { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
        	{ name: 'SecMulta', index: 'SecMulta', hidden: true },
            { name: 'Doc', index: 'Doc', align: "center", sortable: false, formatter: fn_abreDocumentos, width: 45 },
            { name: '', index: '', width: 2 },
        	{ name: 'CodEstadoPago', index: 'CodEstadoPago', hidden: true }
	                ],
        height: '100%',
        pager: '#jqGrid_pager_B',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 5,
        rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        multiselect: false,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            //$("#hddRowIdMulta").val(id);
            var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
            $("#hddRowIdMulta").val(id);
            //$("#hddRowIdMulta").val(srtSecImpuesto);
        },
        ondblClickRow: function(id) {
        }
    });

    jQuery("#jqGrid_lista_B").jqGrid('navGrid', '#jqGrid_pager_B', { edit: false, add: false, del: false });

    $("#jqGrid_lista_B").setGridWidth($(window).width() - 70);
    $("#search_jqGrid_lista_B").hide();


    //**************************************************
    // Documentos
    //**************************************************
    function fn_abreDocumentos(cellvalue, options, rowObject) {
        //return ".";
        return "<img src='../../Util/images/ico_docs.gif' alt='Ver Documentos' title='Ver Documentos' width='18px' onclick=\"javascript:fn_GBAbreDocumentos(\'" + rowObject.CodSolicitudCredito + "\',\'" + rowObject.SecFinanciamiento + "\',\'" + rowObject.SecMulta + "\',\'" + C_GESTIONBIEN_MULTAVEHICULAR + "\');\" style='cursor:pointer;' />";
    };

}

//****************************************************************
// Funcion		:: 	fn_ListarMultasLote
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	AEP - 29/11/2012
//****************************************************************
function fn_ListarMultasLote() {

    parent.fn_blockUI();
    $("#hddRowIdMulta").val('');
    $("#jqGrid_lista_B").jqGrid('resetSelection');
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_B", "rowNum"),
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_B", "page"),
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_B", "sortname"),
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_B", "sortorder"),
    	                 "pPlaca", $("#hddPlaca").val(),
    	                 "pCodMunicipalidad", $("#ddlMunicipalidad").val(),
    	                 "pTipo", $("#hddTipo").val()
                         ];

    fn_util_AjaxWM("frmMultaVehicularConsulta.aspx/ListarLoteMultaVehicular",
                   arrParametros,
                   function(jsondata) {
                       jqGrid_lista_B.addJSONData(jsondata);
                       for (var i = 0, count = idsOfSelectedRows.length; i < count; i++) {
                           $("#jqGrid_lista_B").jqGrid('setSelection', idsOfSelectedRows[i], false);
                       }
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

//****************************************************************
// Funcion		:: 	fn_SeteaMunicipalidad
// Descripción	::	Setear la municipalidad
// Log			:: 	AEP - 26/11/2012
//****************************************************************
function fn_SeteaMunicipalidad() {

    //Carga Distrito
    fn_cargaComboMunicipalidad(strDepartamento, strProvincia);
    //$("#ddlMunicipalidad").val(fn_util_trim($("#hidCodDistrito").val()));
}
//********************************************************  ********
// Funcion		:: 	fn_cargaComboMunicipalidad
// Descripción	::	
// Log			:: 	AEP - 26/11/2012
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
// Funcion		:: 	fn_Volver
// Descripción	::	Regresa a la pagina anterior
// Log			:: 	AEP - 24/09/2012
//****************************************************************
function fn_Volver() {

    fn_mdl_confirma('¿Está seguro de volver?',
		function() {
		    parent.fn_blockUI();
		    fn_util_globalRedirect('/Consultas/MultaVehicular/frmMultaVehicularListado.aspx');
		},
		"../../util/images/question.gif",
		function() {
		},
		'Gestión del Bien : Impuesto Vehicular'
	);


}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos(pstrCodContrato, pstrCodBien, pstrCodRelacionado, pstrCodTipo) {
        parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + "&hddVer=1", 800, 350, function() { });
}


//****************************************************************
// Funcion		:: 	fn_verImpuesto
// Descripción	::	Ver Impuesto
// Log			:: 	JRC - 03/12/2012
//****************************************************************
function fn_verImpuesto() {

    var id = $("#hddRowIdMulta").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeError("Debe seleccionar una multa.", function() { }, "VALIDACIÓN");
    } else {
        var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
        var strPlaca = rowData.Placa;
        var strSecMulta = rowData.SecMulta;
        var strCodMulta = rowData.CodEstadoPago;
        parent.fn_util_AbreModal("Consultas :: Multa Vehicular", "Consultas/MultaVehicular/frmMultaVehicularVer.aspx?codImp=" + strSecMulta + "&placa=" + strPlaca + "&origen=3", 900, 450, function() { });
    }

}