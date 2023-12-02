//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
//****************************************************************
$(document).ready(function () {		
    //Carga Grilla
	fn_cargaGrilla();
	$("#jqGrid_listado").setGridWidth($(window).width()-50);
	
	//On load Page (siempre al final)
	fn_onLoadPage();
});

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla(){	
  $("#jqGrid_lista_A").jqGrid({
	datatype: "local",
	colNames:['RUC', 'Razón Social', 'Seleccionar'],
	colModel:[ 
		{name:'ruc', index: 'ruc', width:50, sorttype:"string", align: "center"}, 		
		{name:'razonsocial', index: 'razonsocial', width:180, align:"left", sorttype:"string"}, 		
		{name:'Seleccionar', index: 'Seleccionar', width:40, sortable:false, align:"center", formatter: SeleccionarProveedor}		
	],	
	height: '100%',
	pager: '#jqGrid_pager_A',
	rowNum:10,
	rowList:[10,20,30],
	sortname: 'invid',
	sortorder: 'desc',
	viewrecords: true,
	gridview: true,
	autowidth: true,
	altRows: true,
	altclass:'gridAltClass'
  }); 
  
  function SeleccionarProveedor(cellvalue, options, rowObject) {
        return "<img src='../Util/images/ok.gif' alt='" + cellvalue + "' title='Seleccionar proveedor' width='20px' />";
  };
    
  var mydata = [ 
		{ruc:"10101245121", razonsocial:"RODAMIENTOS Y REPUESTOS X", Seleccionar:"Seleccionar"}, 
		{ruc:"65401654154", razonsocial:"CONKER", Seleccionar:"Seleccionar"}, 
		{ruc:"30132016580", razonsocial:"GRAPHIC GROUP", Seleccionar:"Seleccionar"}, 
		{ruc:"80415041801", razonsocial:"TAI LOY", Seleccionar:"Seleccionar"}, 
		{ruc:"68249682468", razonsocial:"ELECTRO HOGAR", Seleccionar:"Seleccionar"}, 
		{ruc:"32468426810", razonsocial:"PLANINSA", Seleccionar:"Seleccionar"}, 
		{ruc:"30416840654", razonsocial:"CONSORCIO UNIPETRO", Seleccionar:"Seleccionar"}, 
		{ruc:"54016546580", razonsocial:"FERNANDEZ PAJUELO GUSTAVO EDUARDO", Seleccionar:"Seleccionar"}, 
		{ruc:"98746541541", razonsocial:"ZAMORA FLORES ALCALA", Seleccionar:"Seleccionar"}, 
		{ruc:"98765463111", razonsocial:"COMPAÑIAS UNIDAS VITARTE VICTORIA INCA", Seleccionar:"Seleccionar"}, 
		{ruc:"98765432117", razonsocial:"NISSAN MAQUINARIAS", Seleccionar:"Seleccionar"}
  ];
  
  for(var i = 0; i<= mydata.length; i++) 
	jQuery("#jqGrid_lista_A").jqGrid('addRowData', i + 1,mydata[i]);
}