//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
//****************************************************************
$(document).ready(function () {		
    //Carga Grilla
	fn_cargaGrilla();
	$("#jqGrid_listado").setGridWidth($(window).width() - 50);
	
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
	colNames:['RUC', 'Razón Social', 'Tipo de Persona', 'Procedencia', 'Contacto', 'Correo', ''],
	colModel:[ 
		{name: 'ruc', index: 'ruc', width: 75, sorttype: "string", align: "left"},
		{name: 'razonsocial', index: 'razonsocial', width: 180, align: "left", sorttype: "string"},
		{name: 'TipoDePersona', index: 'TipoDePersona', width: 100, align: "center", sorttype: "string"},
		{name: 'Procedencia', index: 'Procedencia', width: 100, align: "left", sorttype: "string"},
		{name: 'Contacto', index: 'Contacto', width: 200, align: "left", sorttype: "string"},
		{name: 'Correo', index: 'Correo', width: 180, align:"left", sorttype: "string"},
		{name: 'chek', index: 'chek', width: 30, align: "center", edittype: "checkbox", editoptions: { value: '1:0' }, formatter: "checkbox", formatoptions: { disabled: false }, sortable: false}
		
	],	
	height: '100%',
	pager: '#jqGrid_pager_A',
	rowNum:10,
	rowList: [10,20,30],
	sortname: 'invid',
	sortorder: 'desc',
	viewrecords: true,
	gridview: true,
	autowidth: true,
	altRows: true,
	altclass:'gridAltClass',	
	onSelectRow: function(rowObject){ 
	    var rowData = jQuery(this).getRowData(rowObject.ruc);
	    
	    parent.fn_util_CierraModal();
   }
  });   
  function SeleccionarProveedor(cellvalue, options, rowObject) {
        return "<img src='../Util/images/ok.gif' alt='" + cellvalue + "' title='Seleccionar proveedor' width='20px' />";
  };
    
  var mydata = [ 
		{ruc: "10101245121", razonsocial: "RODAMIENTOS Y REPUESTOS X", TipoDePersona: "Persona Jurídica", Procedencia: "Local", Contacto: "PEREZ ASSEO LUIS MIGUEL", Correo: "legal@ryrx.com.pe", chek: ""}, 
		{ruc: "65401654154", razonsocial: "CONKER", TipoDePersona: "Persona Jurídica", Procedencia: "Extranjero", Contacto: "MENDOZA GARCIA JESUS MARTIN", Correo: "proveedores@conker.com", chek: ""}, 
		{ruc: "30132016580", razonsocial: "GRAPHIC GROUP", TipoDePersona: "Persona Jurídica", Procedencia: "Extranjero", Contacto:"CARRILLO INGUNZA FABIOLA MARIA", Correo:"cingunza@graphicgroup.com", chek: ""}, 
		{ruc: "80415041801", razonsocial: "TAI LOY", TipoDePersona: "Persona Jurídica", Procedencia: "Local", Contacto: "CARLOS GUILLERMO CONROY LANATTA", Correo:"gerencia@tailoy.com", chek: ""}, 
		{ruc: "68249682468", razonsocial: "ELECTRO HOGAR", TipoDePersona: "Persona Jurídica", Procedencia: "Local", Contacto: "ANA MARITZA JAZMIN LINO CALDERON",Correo:"analino@electrohogar.com.pe", chek: ""}, 
		{ruc: "32468426810", razonsocial: "PLANINSA", TipoDePersona: "Persona Jurídica", Procedencia: "Local", Contacto: "YOLANDA BENEDICTA CHAVEZ MOGROVEJO", Correo:"contabilidad@planinsa.com.pe", chek: ""}, 
		{ruc: "30416840654", razonsocial: "CONSORCIO UNIPETRO", TipoDePersona: "Persona Jurídica", Procedencia:"Local",Contacto:"ANTONIO ESPINOZA SOTO", Correo:"aespinoza@unipetro.com.pe", chek: ""}, 
		{ruc: "54016546580", razonsocial: "FERNANDEZ PAJUELO GUSTAVO EDUARDO", TipoDePersona: "Persona Natural",Procedencia:"Local",Contacto:"FERNANDEZ PAJUELO GUSTAVO EDUARDO", Correo:"goñi2001@hotmail.com", chek: ""}
  ]; 
  for(var i = 0; i <= mydata.length; i++) 
	jQuery("#jqGrid_lista_A").jqGrid('addRowData', i + 1, mydata[i]);
}