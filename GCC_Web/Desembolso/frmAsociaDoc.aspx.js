//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function () {
    //Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));
    
    //Inicializa Tabs
    $("div#divTabs").tabs();
    
	//Carga Grilla
	fn_cargaGrilla();
	$("#jqGrid_lista_A").setGridWidth($(window).width() - 100);
		
    $("#txtTotal").attr('readOnly', true);
	$("#txtTotal").addClass("css_input_inactivo");	
	
	//On load Page (siempre al final)
	fn_onLoadPage();
	
});

//****************************************************************
// Funcion		:: 	fn_eliminar
// Descripción	::	Confirmación de eliminación
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_eliminar(){
 	parent.fn_mdl_confirma(
		"Est&aacute; seguro de dar de baja el Impuesto seleccionado?"
		, function () {
			var obtnEliminar = $('input[id$=btnEliminar]:submit');
			if (obtnEliminar.length > 0) {
				obtnEliminar.click();
			}
    	}
		, null
		, function () {}
		, null
	);
}


//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() {
    $("#jqGrid_lista_A").jqGrid({
            url: 'server.php?q=2', datatype: "local",
            colNames: ['Clasificación del Bien', 'Tipo del Bien', 'Cantidad', 'Descripción', 'Valor Total'],
            colModel: [
            { name: 'clasificacion', index: 'clasificacion', width: 120, align: "center" },
            { name: 'tipo', index: 'tipo', width: 80, align: "left" },
            { name: 'cantidad', index: 'cantidad', width: 50, align: "right" },
            { name: 'descripcion', index: 'descripcion', width: 120, align: "left" },
            { name: 'valor', index: 'valor', width: 80, align: "right" }//,
           // { name: 'doc', index: 'doc', width:80, edittype:"checkbox", editoptions:{value:'1:0'}, formatter:"checkbox", formatoptions:{disabled:false}}
            ],

           height: '100%',
            pager: '#jqGrid_pager_A',
            rowNum: 10,
            rowList: [10, 20, 30],
            sortname: 'invid',
            sortorder: 'desc',
            viewrecords: true,
            gridview: true,
            autowidth: true  ,
            altRows: true,
            altclass: 'gridAltClass',

             ondblClickRow: function(id) {
            fn_abreDetalle(id);
            }
     });

    $("#jqGrid_lista_C").jqGrid({
        height: 100,
        url: 'subgrid.php?q=1&id=0',
        datatype: "local",
        colNames: ['Tipo Doc.', 'Nº Documento', 'Valor Venta', 'IGV', 'Valor Total'],
        colModel: [
                //{ name: 'chek', index: 'chek', width: 2, edittype: "checkbox", editoptions: { value: '1:0' }, formatter: "checkbox", formatoptions: { disabled: false }, sortable: false },
                { name: 'tipodoc', index: 'tipodoc', width: 100, align: "left" },
                { name: 'nrodocumento', index: 'nrodocumento', width: 100, align: "left" },
                { name: 'venta', index: 'venta', width: 100, align: "right", sortable: "double" },
                { name: 'igv', index: 'igv', width: 100, align: "right", sortable: "double" },
                { name: 'total', index: 'total', width: 100, align: "right", sortable: "double" }
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
        altclass: 'gridAltClass'
    });

    var mydata = [
		            { clasificacion: "Unidad de Transporte terrestre", tipo: "Automovil", cantidad: "1", descripcion: "Hyundai", valor: "12,550.00" },
		            { clasificacion: "Unidad de Transporte terrestre", tipo: "Automovil", cantidad: "1", descripcion: "Mazda", valor: "21,660.00" },
		            { clasificacion: "Unidad de Transporte terrestre", tipo: "Automovil", cantidad: "1", descripcion: "Totoya Hilux", valor: "18,480.00" },
               ];

    for (var i = 0; i <= mydata.length; i++)
        jQuery("#jqGrid_lista_A").jqGrid('addRowData', i + 1, mydata[i]);


    var mydata4 = [
		        {  tipodoc: "Factura", nrodocumento: "001-00022365", venta: "8,403.36", igv: "1,596.64", total: "10,000.00" },
		        {  tipodoc: "Factura", nrodocumento: "002-00095632", venta: "12,605.04", igv: "2,394.96", total: "15,000.00" },
		        {  tipodoc: "Factura", nrodocumento: "003-00002145", venta: "15,126.05", igv: "2,873.95", total: "18,000.00" }
		        ];
		        for (var i = 0; i <= mydata4.length; i++)
		            $("#jqGrid_lista_C").jqGrid('addRowData', i + 1, mydata4[i]);
}


//****************************************************************
// Funcion		:: 	fn_abreDetalle
// Descripción	::	Abre Detalle
// Log			:: 	JRC - 05/03/2012
//****************************************************************
function fn_abreDetalle(strId){
    if (strId == "") 
        strId = "555";
    
    if(strId == "" || strId == null){
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro.", "util/images/warning.gif", "ERROR EN SELECCION");
    } else{    
        parent.fn_util_AbreModal( "ASOCIACIÓN :: DOCUMENTOS", "Desembolso/frmAsociaDocDetalle.aspx?hddCodigo="+strId, 950, 500, function(){} );
    }
}		