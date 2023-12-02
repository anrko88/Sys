//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    fn_inicializaTitulos();

    fn_cargaGrilla();
	$("#jqGrid_lista_C").setGridWidth($(window).width() - 25);	
	    
	//On load Page (siempre al final)
    fn_onLoadPage();
    
});

function fn_inicializaTitulos() {
    var strTitulo = $("#hddCodigoContrato").val();
    
    if (!(strTitulo === undefined || strTitulo === "")) {
  
    }
}

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla(){
    $("#jqGrid_lista_C").jqGrid({
        datatype: "local",
        colNames: ['Nº Contrato', 'CU Cliente', 'Razón Social', 'Clasif. del Bien', 'Estado', 'Ejecutivo', 'Moneda', ''],
        colModel: [
            { name: 'contrato', index: 'contrato', width: 30, align: "center" },
            { name: 'CUCliente', index: 'CUCliente', width: 28, align: "center" },
            { name: 'RazonSocial', index: 'RazonSocial', width: 50, align: "left" },
            { name: 'ClasifContrato', index: 'ClasifContrato', width: 28, align: "left" },
            { name: 'Estado', index: 'Estado', width: 30, align: "center" },
            {name: 'Ejecutivo', index: 'Ejecutivo', width: 28, align: "left"},
            {name: 'Moneda', index: 'Moneda', width: 28, align: "center"},
            {name: 'marca', index: 'marca', width: 10, align: "left", sortable: false, formatter: generacarta}
        ],
        height: '100%',
        pager: '#jqGrid_pager_C',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'invid',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
           var rowData = jQuery(this).getRowData(id);
            
            parent.fn_util_CierraModal();
        }
    });
    function generacarta(cellvalue) {
        return "<img src='../Util/images/ok.gif' alt='" + cellvalue + "' title='Seleccionar' style='width:20px' />";
    };    
    var mydata = [
        {contrato: "100789", CUCliente: "9875", RazonSocial: "BALIQ JOYAS S.A.C.", ClasifContrato: "Inmuebles", Estado: "Formalizado", Ejecutivo: "Bruno de la Cruz", Moneda: "Nuevos Soles", marca: "marca"},
        {contrato: "100719", CUCliente: "6532", RazonSocial: "NEPTUNIA S.A.", ClasifContrato: "Mueble", Estado: "Pendiente de Firma", Ejecutivo: "Javier Choy Ortiz", Moneda: "Nuevos Soles", marca: "marca"},
        {contrato: "100698", CUCliente: "9887", RazonSocial: "COSMOS AGENCIA MARITIMA SAC", ClasifContrato: "Vehículo", Estado: "Pendiente de Firma", Ejecutivo: "Jorge Tuesta Lujan", Moneda: "Dólares Americanos", marca: "marca"},
        {contrato: "100987", CUCliente: "5195", RazonSocial: "AMANCO DEL PERU S.A.", ClasifContrato: "Embarcación pesquera", Estado: "Formalizado", Ejecutivo: "Gino Purín Subiría", Moneda: "Dólares Americanos", marca: "marca"}
    ];
    
    for (var i = 0; i <= mydata.length; i++) {
        jQuery("#jqGrid_lista_C").jqGrid('addRowData', i + 1, mydata[i]);
    }
}