//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function () {

    //Setea Calendario
	fn_util_SeteaCalendario($('input[id*=txtFecha]'));
	 
	//Carga Grilla
	fn_cargaGrilla();
    $("#jqGrid_lista_B").setGridWidth($(window).width() - 47);
	
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
		"Est&aacute; seguro de dar de baja la Tasación seleccionada?"
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
function fn_cargaGrilla(){

$("#jqGrid_lista_B").jqGrid({
        url: 'subgrid.php?q=1&id=0',
        datatype: "local",
        colNames: ['', 'Tipo Doc.', 'Nº Documento', 'Valor Venta', 'IGV', 'Valor Total'],
        colModel: [
                { name: 'chek', index: 'chek', width: 20, align: "center", edittype: "checkbox", editoptions: { value: '1:0' }, formatter: "checkbox", formatoptions: { disabled: false} },
                { name: 'tipodoc', index: 'tipodoc', width: 150, align: "left" },
                { name: 'nrodocumento', index: 'nrodocumento', width: 150, align: "left" },
                { name: 'venta', index: 'venta', width: 120, align: "right", sortable: "double" },
                { name: 'igv', index: 'igv', width: 120, align: "right", sortable: "double" },
                { name: 'total', index: 'total', width: 120, align: "right", sortable: "double" }
              ],
        height: '100%',
        pager: '#jqGrid_pager_B',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'invid',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true  ,
        altRows: true,
        altclass: 'gridAltClass'
    }).navGrid('#jqGrid_lista_B', { add: false, edit: false, del: false });

    var mydata3 = [
		        { chek: "001", tipodoc: "Factura", nrodocumento: "001-00022365", venta: "8,403.36", igv: "1,596.64", total: "10,000.00" },
		        { chek: "001", tipodoc: "Factura", nrodocumento: "002-00095632", venta: "12,605.04", igv: "2,394.96", total: "15,000.00" },
		        { chek: "001", tipodoc: "Factura", nrodocumento: "003-00002145", venta: "15,126.05", igv: "2,873.95", total: "18,000.00" }
		      ];
    
    for (var i = 0; i <= mydata3.length; i++) {
        $("#jqGrid_lista_B").jqGrid('addRowData', i + 1, mydata3[i]);
    }
}

//****************************************************************
// Funcion		:: 	fn_abreEditarModal
// Descripción	::	Abre Detalle Demanda
// Log			:: 	JRC - 05/03/2012
//****************************************************************
function fn_abreEditarModal(strId){
    if(strId == "") strId = $("#hddCodigoContrato").val();  
	
    if(strId == "") {
        parent.fn_mdl_alert("Debe seleccionar un registro.");
    }
    else{    
        var strUrl = "GestionBien/frmTasacionRegistro.aspx?hddCodigo="+strId;        
    }
}

function fn_guardar() {

    fn_mdl_confirma(
                "¿Está seguro que desea guardar?", //Mensaje - Obligatorio
                function(){
                    parent.fn_util_CierraModal();            
                }, 
                null, 
                function(){}, 
                'CONFIRMACIÓN'
               );
    
}