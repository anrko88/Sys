//****************************************************************
// Variables Globales
//****************************************************************


//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	??? - 02/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();
	fn_cargaGrillaImpuestos();    	
    //On load Page (siempre al final)
    fn_onLoadPage();
    
});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {
$('#txtPeriodo').validText({ type: 'number', length: 4 });
$('#txtNroLote').validText({ type: 'number', length: 10 });
}

//****************************************************************
// Funcion		:: 	fn_cargaGrillaImpuestos
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 08/11/2012
//****************************************************************

var idsOfSelectedRows = [];
function fn_cargaGrillaImpuestos() {
    
    var mydata2 = 
          [
		    { CodSolicitudCredito: "10000001",Placa: "xt15444",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "170.00",NroCuota: "1",FechaPago: "16/11/2012",FechaCobro: "20/11/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"},
		    { CodSolicitudCredito: "10000002",Placa: "xt15445",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "80.00",NroCuota: "2",FechaPago: "16/12/2012",FechaCobro: "20/12/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"},
          	{ CodSolicitudCredito: "10000003",Placa: "xt15488",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "170.00",NroCuota: "1",FechaPago: "16/11/2012",FechaCobro: "20/11/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"},
          	{ CodSolicitudCredito: "10000001",Placa: "xt15444",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "170.00",NroCuota: "1",FechaPago: "16/11/2012",FechaCobro: "20/11/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"},
		    { CodSolicitudCredito: "10000002",Placa: "xt15445",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "80.00",NroCuota: "2",FechaPago: "16/12/2012",FechaCobro: "20/12/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"},
          	{ CodSolicitudCredito: "10000003",Placa: "xt15488",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "170.00",NroCuota: "1",FechaPago: "16/11/2012",FechaCobro: "20/11/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"},
          	{ CodSolicitudCredito: "10000001",Placa: "xt15444",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "170.00",NroCuota: "1",FechaPago: "16/11/2012",FechaCobro: "20/11/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"},
		    { CodSolicitudCredito: "10000002",Placa: "xt15445",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "80.00",NroCuota: "2",FechaPago: "16/12/2012",FechaCobro: "20/12/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"},
          	{ CodSolicitudCredito: "10000003",Placa: "xt15488",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "170.00",NroCuota: "1",FechaPago: "16/11/2012",FechaCobro: "20/11/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"},
		    { CodSolicitudCredito: "10000004",Placa: "xt15400",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "80.00",NroCuota: "2",FechaPago: "16/12/2012",FechaCobro: "20/12/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"}
		  ];
	
    var updateIdsOfSelectedRows = function (id, isSelected) {
    $("#hddRowId").val(id);
    var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
    var index = $.inArray(id, idsOfSelectedRows);
    	if (!isSelected && index >= 0) {            
    	idsOfSelectedRows.splice(index, 1);       
    	} else if (index < 0) {
       idsOfSelectedRows.push(rowData.CodSolicitudCredito);
    	}
    };

    $("#jqGrid_lista_B") .jqGrid({
//        datatype: function() {
//            fn_ListarPipeline();
//        },
    	datatype: "local",
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "CodSolicitudCredito"
        },
        colNames: ['Estado','Nro. Contrato','Razón Social o Nombre','Placa','Nº Motor','Marca','Modelo','Clase','Año Fabricación','Fecha Adquisición','Fecha Inscripción','Fecha Declaración','Periodo','Nº Cuota','Moneda','Importe','Observación','Bien','','',''],
        colModel: [
		    { name: 'Estado', index: 'Estado', width: 50, align: "center", sorttype: "string" },
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 50, sorttype: "string", align: "center" },
            { name: 'RazonSocial', index: 'RazonSocial', width: 150, sorttype: "string", align: "center" },
            { name: 'Placa', index: 'Placa', width: 50, sorttype: "string", align: "left" },
            { name: 'NroMotor', index: 'NroMotor', width: 50, sorttype: "string", align: "center" },
        	{ name: 'Marca', index: 'Marca', width: 80, align: "center", sorttype: "string" },
		    { name: 'Modelo', index: 'Modelo', width: 50, align: "center", sorttype: "string" },
		    { name: 'Clase', index: 'Clase', width: 50, align: "center", sorttype: "string",editable:true },
		    { name: 'AnioFabricacion', index: 'AnioFabricacion', width: 60, align: "center" },
        	{ name: 'FechaAdquisicion', index: 'FechaAdquisicion', width: 60, align: "center" },
        	{ name: 'FechaDeclaracion', index: 'FechaDeclaracion', width: 60, align: "center" },
		    { name: 'FechaInscripcion', index: 'FechaInscripcion', width: 50, align: "center", sorttype: "string" },
        	 { name: 'Periodo', index: 'Periodo', width: 50, align: "center", sorttype: "string" },
        	{ name: 'NroCuota', index: 'NroCuota', width: 50, align: "center", sorttype: "string" },
        	 { name: 'Moneda', index: 'Moneda', width: 50, align: "center", sorttype: "string" },
        	{ name: 'Importe', index: 'Importe', width: 50, align: "center", sorttype: "string" },
        	{ name: 'Observacion', index: 'Observacion', width: 50, align: "center", sorttype: "string",formatter:Lupa2  },
        	{ name: 'Verificar', index: 'Verificar', width: 50, align: "center", sorttype: "string",formatter:asociar  },
        	{ name: '', index: '', width: 1 },
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
            { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true }   
	                ],
        height: '100%',
        pager: '#jqGrid_pager_B',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        multiselect:true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: updateIdsOfSelectedRows,
        onSelectAll: function (aRowids, isSelected) {
        	var i, count, id;         
        	  for (i = 0, count = aRowids.length; i < count; i++) {
        	  	  id = aRowids[i];             
        	  	updateIdsOfSelectedRows(id, isSelected);
        	  }
        },     
        ondblClickRow: function(id) {
            parent.fn_blockUI();
//            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
//            window.location = "frmPipelineRegistro.aspx?CodCotizacion=" + rowData.CodigoCotizacion + "&CodEstado=" + rowData.CodigoEstadoCotizacion;
        }
    });
	
	for (var i = 0; i <= mydata2.length; i++) {
	     jQuery("#jqGrid_lista_B").jqGrid('addRowData', i + 1, mydata2[i]);
	 }
	
	
    jQuery("#jqGrid_lista_B").jqGrid('navGrid', '#jqGrid_pager_B', { edit: false, add: false, del: false });

   // $("#jqGrid_lista_B").setGridWidth($(window).width() - 70);
    $("#search_jqGrid_lista_B").hide();
	
     function asociar(cellvalue, options, rowObject) {
        return "<img src='../../Util/images/ico_mdl_demanda.gif' alt='Verificar bienes' title='Mantenimiento del Bien' width='20px' height='20px' onclick='javascript:VerDatosBien(" + rowObject.SecFinanciamiento + "," + rowObject.CodSolicitudCredito + " )" + ";' style='cursor: pointer;cursor: hand;' />";
    };
	  function Lupa2(cellvalue, options, rowObject) {
           
       if (rowObject.ComentarioBaja == "") {
           return "<img src='../../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
       } else {
          var sScript2 = "javascript:VerObservaciones(" + rowObject.CodSolicitudCredito + "," + rowObject.SecFinanciamiento + ");";
       	  return "<img src='../../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
       }
    	
    };

}

function VerObservaciones(strcodContrato,strsecfinanciamiento) {
    var sTitulo = "Gestión del Bien";
    var sSubTitulo = "Mant. Bien :: Observación de Baja  ";
    parent.fn_util_AbreModal(sSubTitulo, "GestionBien/ImpuestoVehicular/frmImpuestoVehicularObservacion.aspx?ccf=" + strcodContrato + "&csf=" + strsecfinanciamiento + "&Add=true", 500, 200, function() { });
}

function VerDatosBien(strCodigoBien,strCodigoContrato) {
   // fn_util_AbreModal( "Lista :: BIENES", "Administracion/frmMantRegistroBienesContrato.aspx?csc=" + strCodigoContrato + "&csf=" + strCodigoBien, 950, 375, function(){} );
	fn_util_AbreModal( "Gestión del Bien  :: Impuesto Vehicular - Verificar Bienes", "frmImpuestoVehicularVerificarBienes.aspx",950,375, function(){} );
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
        fn_util_redirect('frmImpuestoVehicularListado.aspx');
		},
		"../../util/images/question.gif",
		function() {
		},
		'Gestión del Bien : Impuesto Vehicular'
	);
	
    
}
