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
        colNames: ['Nº Bien', 'Razón Social', 'Clasificación Bien', 'Descripción Bien', ''],
        colModel: [
                { name: 'nro', index: 'nro', width: 50, align: "right" },
                { name: 'rsocial', index: 'rsocial', width: 100, align: "left" },
                { name: 'clasibien', index: 'clasibien', width: 80, align: "center" },
                { name: 'DescripcionBien', index: 'DescripcionBien', width: 50,align:"left" },
                { name: 'chek', index: 'chek', width: 90, align: "center", edittype: "checkbox", editoptions: { value: '1:0' }, formatter: "checkbox", formatoptions: { disabled: false }, sortable: false }                
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
		        { nro:"78976", rsocial:"RIPLEY", clasibien:"Bienes Inmuebles", DescripcionBien:"Almacén en zona industrial", chek:"chek"},
		        { nro:"16546", rsocial:"SERVICIOS LOGISTICOS INTEGRALES", clasibien:"Unidades de Transporte Terrestre", DescripcionBien:"Camioneta 4x4", chek:"chek"},
		        { nro:"54464", rsocial:"PROFUTURO", clasibien:"Maquinaria y Equipos Industrial", DescripcionBien:"Almacén", chek:"chek"},
		        { nro:"54464", rsocial:"RIPLEY", clasibien:"Maquinaria y Equipos Industrial", DescripcionBien:"Grupo electrógeno", chek:"chek"},
		        ];
    for (var i = 0; i <= mydatabien.length; i++) {
        $("#jqGrid_lista_E").jqGrid('addRowData', i + 1, mydatabien[i]);
    }
}