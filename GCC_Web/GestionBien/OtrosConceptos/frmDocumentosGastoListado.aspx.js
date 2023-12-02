
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 03/10/2012
//****************************************************************
$(document).ready(function() {

    //Inicializa Grillas
    fn_cargaGrilla();

    //On load Page (siempre al final)
    fn_onLoadPage();

});


//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() {
    $("#jqGrid_lista_C").jqGrid({
        //        datatype: function() {
        //            fn_ListaDocumentos();
        //        },
        datatype: "local",
        jsonReader: {
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount",     // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "Id" // Índice de la columna con la clave primaria.
        },
        colNames: ['Razón Social o Nombre', 'Tipo Comprobante', 'N° Comprobante', 'Moneda', 'Importe Total', 'Estado', ''],
        colModel: [
                { name: 'RazonSocial', index: 'RazonSocial', width: 250, align: "left" },
                { name: 'TipoComprobante', index: 'TipoComprobante', width: 130, sorttype: "string" },
                { name: 'NroComprobante', index: 'NroComprobante', width: 120, align: "left" },
                { name: 'NombreMoneda', index: 'NombreMoneda', width: 150, align: "left" },
                { name: 'ImporteTotal', index: 'ImporteTotal', width: 150, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'EstadoDocumento', index: 'EstadoDocumento', align: "center" },
                { name: 'Desembolso', index: 'Desembolso', width: 90, align: "center", sortable: false, formatter: fn_Seleccionar }
              ],
        height: '60%',
        pager: '#jqGrid_pager_C',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 30,
        rowList: [10, 20, 30],
        sortname: 'Id', // Columna a ordenar por defecto.
        sortorder: 'desc', // Criterio de ordenación por defecto.
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass'
    });
    jQuery("#jqGrid_lista_C").jqGrid('navGrid', '#jqGrid_pager_C', { edit: false, add: false, del: false });
    $("#jqGrid_lista_C").setGridWidth($(window).width());
    $("#search_jqGrid_lista_C").hide();

    var arrData = [
					{ RazonSocial: "TX DEVELOPERS S.A.C.", TipoComprobante: "FACTURA", NroComprobante: "1015478944", NombreMoneda: "DOLARES AMERICANOS", ImporteTotal: "1500.00", EstadoDocumento: "" },
					{ RazonSocial: "MANPER S.A.C.", TipoComprobante: "BOLETA", NroComprobante: "1665142514", NombreMoneda: "NUEVOS SOLES", ImporteTotal: "850.00", EstadoDocumento: "" }
					];
    for (var i = 0; i <= arrData.length; i++) {
        jQuery("#jqGrid_lista_C").jqGrid('addRowData', i + 1, arrData[i]);
    }

    function fn_Seleccionar(cellvalue, options, rowObject) {
        return "<input id='chkSeleccionar' name='chkSeleccionar' type='checkbox' runat='server' onclick=\"javascript:fn_seleccionaRegistro();\" />";
    };

}

//****************************************************************
// Funcion		:: 	fn_seleccionaRegistro
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_seleccionaRegistro() {

}

