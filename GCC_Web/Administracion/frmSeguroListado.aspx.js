//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
//****************************************************************
$(document).ready(function () {	
	//Setea Calendario
	fn_util_SeteaCalendario($('input[id*=txtFecha]'));
		
    //Carga Grilla
	fn_cargaGrilla();
	$("#jqGrid_lista_B").setGridWidth($(window).width()-75);
	
	//On load Page (siempre al final)
	fn_onLoadPage();
});

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla(){
  $("#jqGrid_lista_B").jqGrid({
	datatype: "local",
	colNames:['N° Contrato', 'CU Cliente', 'Razón Social o Nombre', 'Nº Póliza', 'Tipo de Bien', 'Tipo de Seguro', 'Fecha Inicio P.', 'Fecha Fin P.'],
	colModel:[ 	    
		{ name:'NoContrato',index:'NoContrato', width:30, sorttype:"string", align:"center" },
		{ name: 'CUCliente', index: 'CUCliente', width: 30, sorttype: "string", align: "center" }, 
		{ name: 'RazonSocial',index:'RazonSocial', width:100, sorttype:"string", align:"left" }, 
		{ name: 'NoPoliza',index:'NoPoliza', width:30, sorttype:"string", align:"left" }, 
		{ name: 'TipoBien',index:'TipoBien', width:50, sorttype:"string", align:"left" },
		{ name: 'TipoSeguro', index: 'TipoSeguro', width: 30, sorttype: "string", align: "left" },
		{ name: 'FechaInicio',index:'FechaInicio', width:50, sorttype:"string", align:"center" },
		{ name: 'FechaFin', index: 'FechaFin', width: 30, sorttype: "string", align: "center" }
	],	
	height: '100%',
	pager: '#pager3',
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
  var mydata2 = [ 
		{NoContrato: "10000160", CUCliente: "0000000795", RazonSocial: "ABAD CHUNGA RAFAEL EDUARDO", NoPoliza: "00001", TipoBien: "Terreno", TipoSeguro: "Cotización",FechaInicio:"17/06/2011",FechaFin:"17/06/2013"},
		{ NoContrato: "10000254", CUCliente: "0050002959", RazonSocial: "FRAMELVA", NoPoliza: "00011", TipoBien: "Vehiculos", TipoSeguro: "Cotización", FechaInicio: "23/09/2011", FechaFin: "23/09/2015" },
		{ NoContrato: "10000306", CUCliente: "0050019993", RazonSocial: "E & C PROYECTOS OBRAS Y CONSTRUCCIONES", NoPoliza: "00002", TipoBien: "Edificios", TipoSeguro: "Cotización", FechaInicio: "10/09/2011", FechaFin: "10/09/2015" },
		{ NoContrato: "10000287", CUCliente: "0050026779", RazonSocial: "APC CORPORACION SA", NoPoliza: "00003", TipoBien: "Terreno", TipoSeguro: "Cotización", FechaInicio: "30/01/2010", FechaFin: "30/01/2013" },
		{ NoContrato: "10000259", CUCliente: "0050026888", RazonSocial: "DIBU DIBU DUD", NoPoliza: "00004", TipoBien: "Vehiculos", TipoSeguro: "Cotización", FechaInicio: "28/05/2010", FechaFin: "08/05/2014" },
		{ NoContrato: "10000251", CUCliente: "5000000130", RazonSocial: "SAN FERNANDO", NoPoliza: "00005", TipoBien: "Terreno", TipoSeguro: "Cotización", FechaInicio: "16/09/2011", FechaFin: "16/09/2016" },
		{ NoContrato: "10000236", CUCliente: "5000295555", RazonSocial: "ODEBRECHT PERU INGENIERIA", NoPoliza: "00006", TipoBien: "Equipo Electrico", TipoSeguro: "Cotización", FechaInicio: "13/07/2010", FechaFin: "13/07/2016" },
		{ NoContrato: "10000226", CUCliente: "0050026748", RazonSocial: "SAGA FALABELLA", NoPoliza: "00007", TipoBien: "Equipo Electrico", TipoSeguro: "Cotización", FechaInicio: "22/09/2010", FechaFin: "22/09/2013" },
		{ NoContrato: "10000197", CUCliente: "0050020533", RazonSocial: "RIPLEY", NoPoliza: "00008", TipoBien: "Vehiculos", TipoSeguro: "Cotización", FechaInicio: "23/09/2011", FechaFin: "23/09/2016" }
  ]; 
  for(var i=0;i<=mydata2.length;i++) 
	jQuery("#jqGrid_lista_B").jqGrid('addRowData', i+1, mydata2[i]);
}