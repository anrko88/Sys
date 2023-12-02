var strTipoPersonaNatural = "1";
var strTipoPersonaJuridica = "2";
var strSeguroDegravamenTipoInterno = "001";
var strSeguroDegravamenTipoExterno = "002";

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {
    //---------------------------------
    //Valida Tabs
    //---------------------------------
    $("div#divTabs").tabs({
        show: function(event, ui) {
            fn_doResize();
        }
    });

    //Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    fn_cargaGrilla_A();
    fn_ListarSeguimientoCotizacion();

    fn_cargaGrilla_C();
    //fn_ListarCronogramaCotizacion();
    //fn_validaColumnasCronograma();

    //Inicializa    
    if (fn_util_trim($('#hddTipoPersona').val()) == strTipoPersonaNatural) {
        $('#fld_SeguroDegravamen').show();
    } else {
        $('#fld_SeguroDegravamen').hide();
    }

    //Check Opciones    
    fn_validaMostrarComision($("#hddMostrarComision").val());
    fn_validaMostrarTea($("#hddMostrarTea").val());

    $('#txaOtrasComisiones').prop('readonly', true);
    $('#txtProveedores').prop('readonly', true);

    //Redimencionar    
    fn_doResize();

    //On load Page (siempre al final)
    fn_onLoadPage();

});

/****************************************************************
Funcion		:: 	fn_cargaGrilla
Descripción	::	Carga Grilla A
Log			:: 	KCC - 24/05/2012
****************************************************************/
function fn_cargaGrilla_A() {
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() { },
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
		        { name: 'Comentario', index: 'Comentario', width: 450, align: "left", sorttype: "string", defaultValue: "" }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'Codigocotizacion',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass'
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

    //Abrir Archivo
    function fn_icoDownload(cellvalue, options, rowObject) {
        if (fn_util_trim(rowObject.RutaArchivo) != "") {
            return "<img src='../Util/images/ico_download.gif' alt='Descargar/Mostrar Archivo' title='Descargar/Mostrar Archivo' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.RutaArchivo) + "');\" style='cursor:pointer;'/>";
        } else {
            return ".";
        }
    };
}

function fn_ListarSeguimientoCotizacion() {
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"), // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
                         "pstrCodCotizacion", $("#hddCodigoCotizacion").val()
                         ];

    fn_util_AjaxWM("frmCotizacionVer.aspx/ListadoCotizacionDocumento",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);
                    //parent.fn_unBlockUI();
                    fn_doResize();
                },
                function(request) {
                    //parent.fn_unBlockUI();
                    var error = eval("(" + request.responseText + ")");
                    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR LISTAR");
                }
        );
}

/****************************************************************
Funcion		:: 	fn_cargaGrilla
Descripción	::	Carga Grilla C
Log			:: 	KCC - 24/05/2012
****************************************************************/
function fn_cargaGrilla_C() {

    $("#jqGrid_lista_C").jqGrid({
        datatype: function() {
            fn_ListarCronogramaCotizacion();
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
		        { name: 'Cantdiascuota', index: 'Cantdiascuota', align: "center", width: 80 },
		        { name: 'SMontosaldoadeudado', index: 'Montosaldoadeudado', align: "right", width: 120, formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontointeresbien', index: 'Montointeresbien', align: "right", width: 120, formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontoprincipalbien', index: 'Montoprincipalbien', align: "right", width: 120, formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontototalcuota', index: 'Montototalcuota', align: "right", width: 120, formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontocuotasegurobien', index: 'Montocuotasegurobien', align: "right", width: 120, formatter: Fn_util_ValidaMontoNull },
		        { name: 'SCuotaSeguroDes', index: 'CuotaSeguroDes', align: "right", width: 120, formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontototalcuotaigv', index: 'Montototalcuotaigv', align: "right", width: 100, formatter: Fn_util_ValidaMontoNull },
		        { name: 'STotalapagar', index: 'Totalapagar', width: 120, align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'AA', index: 'AA', width: 10, align: "right" }
        ], 
        width: 900,
        height: '100%',
        pager: '#jqGrid_pager_C',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 50,
        //rowList: [10, 20, 30],
        sortname: 'Codigocotizacion',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: false,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        gridComplete: function(id) {
            fn_validaColumnasCronograma();
        }
    });
    jQuery("#jqGrid_lista_C").jqGrid('navGrid', '#jqGrid_lista_C', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_C").hide();
    
}


/****************************************************************
Funcion		:: 	fn_ListarCronogramaCotizacion
Descripción	::	Listar Cronograma
Log			:: 	KCC - 24/05/2012
****************************************************************/
function fn_ListarCronogramaCotizacion() {
    var arrParametros = ["pstrNroCotizacion", $("#hddCodigoCotizacion").val(),
                         "pstrVersionCotizacion", $("#hddVersionCotizacion").val(),
                         "pstrPaginaActual", fn_util_getJQGridParam("jqGrid_lista_C", "page")
                         ];

    fn_util_AjaxWM("frmCotizacionVer.aspx/ListadoCotizacionCronograma",
            arrParametros,
            function(jsondata) {
                jqGrid_lista_C.addJSONData(jsondata);                
                fn_doResize();
            },
            function(request) {
                var error = eval("(" + request.responseText + ")");
                parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR LISTAR");
            }
    );
    
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



//************************************************************
// Función		:: 	fn_validaMostrarComision
// Descripcion 	:: 	Valida RadioButton MostrarComision
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_validaMostrarComision(strValor) {    
    if (strValor == "1") {
        $('#hddMostrarComision').val("1");
        $('input[id=rdbMostrarComisionSI]').attr('checked', true);        
    } else {
        $('#hddMostrarComision').val("0");        
        $('input[id=rdbMostrarComisionNO]').attr('checked', true);
    }
}


//************************************************************
// Función		:: 	fn_validaMostrarTea
// Descripcion 	:: 	Valida RadioButton TEA
// Log			:: 	JRC - 17/05/2012
//************************************************************
function fn_validaMostrarTea(strValor) { 
    if (strValor == "1") {
        $('#hddMostrarTea').val("1");
        $('input[id=rdbMostrarTeaSI]').attr('checked', true);
    } else {
        $('#hddMostrarTea').val("0");        
        $('input[id=rdbMostrarTeaNO]').attr('checked', true);
    }
}



//****************************************************************
// Funcion		:: 	fn_validaColumnasCronograma 
// Descripción	::	Valida Columnas Cronograma
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_validaColumnasCronograma() {
    
    var gridCronograma = $('#jqGrid_lista_C');
    if (fn_util_trim($("#hddTipoPersona").val()) == strTipoPersonaJuridica) {
        gridCronograma.jqGrid('hideCol', gridCronograma.getGridParam("colModel")[8].name);
    } else {
        if ($("#hddTipoPersona").val() != strSeguroDegravamenTipoInterno) {
            gridCronograma.jqGrid('hideCol', gridCronograma.getGridParam("colModel")[8].name);
        }
    }
    if (!fn_util_ValidaDecimal($('#lblImportePrimaSeguroBien').html()) > 0) {
        gridCronograma.jqGrid('hideCol', gridCronograma.getGridParam("colModel")[7].name);
    } else {
        gridCronograma.jqGrid('showCol', gridCronograma.getGridParam("colModel")[7].name);
    }

    if (!fn_util_ValidaDecimal($('#lblImportePrimaDesgravamen').html()) > 0) {
        gridCronograma.jqGrid('hideCol', gridCronograma.getGridParam("colModel")[8].name);
    } else {
        gridCronograma.jqGrid('showCol', gridCronograma.getGridParam("colModel")[8].name);
    }

}