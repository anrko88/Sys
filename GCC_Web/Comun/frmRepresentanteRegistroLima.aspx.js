//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    // fn_inicializaCampos();
    fn_cargaGrilla();
    //On load Page (siempre al final)
    fn_onLoadPage();
});

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
//function fn_inicializaCampos() {
//    var hdnTitulo = document.getElementById("hdnTitulo").value;
//    if (hdnTitulo == "cot") {
//        $("#TextArea1").removeAttr("disabled");
//        $("#TextArea1").removeAttr("readonly");
//    }

//    if ($("#hflagtipoObs").val() == 2) {
//        $("#dvtipoObservaciones").hide();

//        //document.getElementById("dvtipoObservaciones").style.display = 'none';
//    } else {
//        $("#dvtipoObservaciones").show();
//        //document.getElementById("hdnTitulo").value
//        //document.getElementById("dvtipoObservaciones").style.display = 'block';

//    }

//}

function fn_cargaGrilla() {
    $("#jqGrid_lista_A").jqGrid({
        datatype: "local",
        colNames: ['DNI', 'Representantes', ''],
        colModel: [
		            { name: 'dni', index: 'dni', width: 200, sorttype: "string", align: "left" },
		            { name: 'representantes', index: 'representantes', width: 300, align: "left" },
		            { name: 'check', index: 'check', align: 'center', width: 100, edittype: "checkbox", editoptions: { value: '1:0' }, formatter: "checkbox", formatoptions: { disabled: false }, sortable: false }
	            ],
        height: '100%',
        pager: '#pager1',
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

    mydata1 = [
		        { dni: "40910626", representantes: "Representante 01" },
		        { dni: "41118330", representantes: "Representante 02" },
		        { dni: "79787675", representantes: "Representante 03" }
            ];

    for (var i = 0; i <= mydata1.length; i++)
        jQuery("#jqGrid_lista_A").jqGrid('addRowData', i + 1, mydata1[i]);

}