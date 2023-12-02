//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    fn_cargaGrilla();

    fn_inicializaCampos();
});

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() {
     $("#jqGrid_lista_E").jqGrid({
        url: 'subgrid.php?q=1&id=0',
        datatype: "local",
        colNames: ['Nº Bien', 'Tipo de Bien', 'Descripción Bien', 'Importe'],
        colModel: [
                    { name: 'nro', index: 'nro', width: 40, align: "right" },
                    { name: 'tipobien', index: 'tipobien', width: 60, align: "center" },
                    { name: 'DescripcionBien', index: 'DescripcionBien', width: 100, align:"left" },
                    { name: 'Importe', index: 'Importe', width: 35, align: "right", sortable: false }
                  ],
        height: '100%',
        pager: '#jqGrid_pager_E',
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
	var mydatabien = [
		        {nro: "78976", tipobien: "Almacén", DescripcionBien: "Almacén en zona industrial", Importe: "500.00"},
		        {nro: "16546", tipobien: "Automóvil", DescripcionBien: "Camioneta 4x4", Importe: "1,200.00"},
		        {nro: "54465", tipobien: "Grupo Electrógeno", DescripcionBien: "Grupo Electrógeno", Importe: "450.00"},
		        {nro: "54466", tipobien: "Otros", DescripcionBien: "Grupo Electrógeno", Importe: "1,850.00"},
		        {nro: "54467", tipobien: "Automóvil", DescripcionBien: "Automovil azul marino", Importe: "750.00"},
		        {nro: "54468", tipobien: "Otros", DescripcionBien: "Grúa", Importe: "1,500.00"}
		        ];
    for (var i = 0; i <= mydatabien.length; i++) {
        $("#jqGrid_lista_E").jqGrid('addRowData', i + 1, mydatabien[i]);
    }
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {

}