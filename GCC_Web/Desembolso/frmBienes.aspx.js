//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    fn_cargaGrilla();

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
        colNames: ['Nº Bien', 'Codclasibien', 'Tipo de Bien', 'Descripción Bien', 'Editar', 'Asociar'],
        colModel: [
                { name: 'nro', index: 'nro', width: 40, align: "right" },
                { name: 'Codclasibien', index: 'Codclasibien', hidden: true },
                { name: 'tipobien', index: 'tipobien', width: 60, align:"left" },
                { name: 'DescripcionBien', index: 'DescripcionBien', width: 100,align:"left" },
                { name: 'Editar', index: 'Editar', width: 35, align: "center", sortable: false, formatter: asociar },
                { name: 'chek', index: 'chek', width: 35, align: "center", edittype: "checkbox", editoptions: { value: '1:0' }, formatter: "checkbox", formatoptions: { disabled: false }, sortable: false }
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
    function asociar(cellvalue, options, rowObject) {
        return "<img src='../Util/images/ico_mdl_demanda.gif' alt='Asociar bienes' title='Editar' width='20px' height='20px' onclick='javascript:VerDatosBien(" + rowObject.Codclasibien + ");' style='cursor: pointer;cursor: hand;' />";
    };
	var mydatabien = [
		        {nro: "78976", Codclasibien: "1", tipobien:"Almacén", DescripcionBien: "Almacén en zona industrial", Editar: "Editar", chek: ""},
		        {nro: "16546", Codclasibien: "2", tipobien: "Automovil", DescripcionBien: "Camioneta 4x4", Editar: "Editar", chek: ""},
		        {nro: "54464", Codclasibien: "3", tipobien: "Grupo Electrógeno", DescripcionBien: "Grupo Electrógeno", Editar:"Editar", chek: ""},
		        {nro: "54464", Codclasibien: "3", tipobien: "Otros", DescripcionBien: "Grupo Electrógeno", Editar:"Editar", chek: ""},
		        {nro: "54464", Codclasibien: "3", tipobien: "Otros", DescripcionBien: "SANG YONG REXTON", Editar:"Editar", chek: ""},
		        {nro: "54464", Codclasibien: "3", tipobien: "Otros", DescripcionBien: "Grua", Editar:"Editar", chek: ""}
		        ];
    for (var i = 0; i <= mydatabien.length; i++) {
        $("#jqGrid_lista_E").jqGrid('addRowData', i + 1, mydatabien[i]);
    }
}

function DatosBien(strId, rowid, clasibien) {
    if(strId == "") {    
        strId = "555";
    }    
    
    var oRow = $("#jqGrid_lista_E").getRowData(rowid);    
    fn_util_AbreModal( "Lista :: BIENES", "Desembolso/frmDatosBien.aspx", 950, 375, function(){} );
}

function VerDatosBien(Codclasibien) {
    fn_util_AbreModal( "Lista :: BIENES", "frmDatosBien.aspx?Codclasibien=" + Codclasibien, 950, 375, function(){} );
}