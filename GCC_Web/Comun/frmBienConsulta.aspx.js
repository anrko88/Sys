//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
//****************************************************************
$(document).ready(function () {
	
	//On load Page (siempre al final)
    fn_onLoadPage();

    //Carga Grilla
    fn_cargaGrilla();
});

function fn_cargaGrilla() {
    $("#jqGrid_lista_C").jqGrid({
        url: 'subgrid.php?q=1&id=0',
        datatype: "local",
        colNames: ['N° Bien', '', 'Tipo de Bien', 'Descripción del Bien', ''],
        colModel: [
                    { name: 'nro', index: 'nro', width: 40, align: "right" },
                    { name: 'Codclasibien', index: 'Codclasibien', hidden: true },
                    { name: 'tipobien', index: 'tipobien', width: 60, align:"left" },
                    { name: 'DescripcionBien', index: 'DescripcionBien', width: 100, align:"left" },
                    { name: 'marca', index: 'marca', width: 20, align: "center", sortable: false, formatter: generacarta }
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
        //multiselect: true
    });
    function generacarta(cellvalue, options, rowObject) {
        return "<img src='../Util/images/ok.gif' alt='" + cellvalue + "' title='Seleccionar' style='width:20px' />";
    };    
    var mydata4 = [
    		    {nro: "78976", Codclasibien: "1", tipobien: "Almacén", DescripcionBien: "Almacén en zona industrial", marca: "marca"},
		        {nro: "16546", Codclasibien: "2", tipobien: "Automóvil", DescripcionBien: "Camioneta 4x4", marca: "marca"},
		        {nro: "54464", Codclasibien: "3", tipobien: "Grupo Electrógeno", DescripcionBien: "Grupo Electrógeno a gas natural", marca: "marca"},
		        {nro: "54464", Codclasibien: "3", tipobien: "Otros", DescripcionBien: "Grupo Electrógeno", marca: "marca"},
		        {nro: "54464", Codclasibien: "3", tipobien: "Otros", DescripcionBien: "Línea de frío", marca: "marca"},
		        {nro: "54464", Codclasibien: "3", tipobien: "Otros", DescripcionBien: "Grúa", marca: "marca"}
		        ];
		        
    for (var i = 0; i <= mydata4.length; i++) {
        $("#jqGrid_lista_C").jqGrid('addRowData', i + 1, mydata4[i]);
    }
    
    $("#jqGrid_lista_C").setGridWidth($(window).width() - 50);	
}