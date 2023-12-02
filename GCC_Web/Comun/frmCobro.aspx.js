//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    // Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));

	// Valida Campos
	fn_inicializaCampos();
	
    // Carga Grilla
    fn_cargaGrilla();

    // On load Page (siempre al final)
//    fn_onLoadPage();

});

//****************************************************************
// Funcion		:: 	fn_eliminar
// Descripción	::	Confirmación de eliminación
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_eliminar() {
    parent.fn_mdl_confirma(
		"Est&aacute; seguro de dar de baja el Impuesto seleccionado?"
		, function() {

		}
		, null
		, function() { }
		, null
	);
}

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() {
    // Inicializa Grilla
    $("#jqGrid_lista_B").jqGrid({
        datatype: "local",
        colNames: ['Nº Contrato', 'CU Cliente', 'Razón Social', 'Clasific. Del Bien', 'Periodo', 'Moneda', 'Importe Total', 'Comisión Total', 'Bienes', ''],
        colModel: [
            { name: 'contrato', index: 'contrato', width: 25, align: "center" },
            { name: 'CUCliente', index: 'CUCliente', width: 22, align: "center" },
            { name: 'RazonSocial', index: 'RazonSocial', width: 42, align: "left" },
            { name: 'ClasificDelBien', index: 'ClasificDelBien', width: 30, align: "center" },
            { name: 'Periodo', index: 'Periodo', width: 20, align: "center" },
            { name: 'Moneda', index: 'Moneda', width: 25, align: "center" },
            { name: 'importe', index: 'importe', width: 25, align: "right", sortable: "double" },
            { name: 'Comision', index: 'Comision', width: 26, align: "right", sortable: "double" },
            { name: 'Bienes', index: 'Bienes', width: 20, align: "center", sortable: "date", formatter: lupa },
            { name: 'check', index: 'check', align: 'center', width: 10, edittype: "checkbox", editoptions: { value: '1:0' }, formatter: "checkbox", formatoptions: { disabled: false }, sortable: false }
        ],
        height: '100%',
        pager: '#jqGrid_pager_B',
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
            $("#hddCodigoContrato").val(id);
        }
    });
    function lupa(cellvalue) {
        return "<img src='../Util/images/ico_acc_buscar.gif' alt='" + cellvalue + "' title='Ver Lista de Bienes' width='20px' onclick='javascript:verListaBienes();' style='cursor: pointer;cursor: hand;' />";
    };	
    // Inicializa Data
    var mydata1 = [
            {contrato: "0001", CUCliente: "03215", RazonSocial: "NC CONSULTORES", ClasificDelBien: "Bienes Inmuebles", Periodo: "2011", Moneda: "Nuevos Soles", importe: "1,057.80", Comision: "46.00", chek: ""},
            {contrato: "0002", CUCliente: "98754", RazonSocial: "SCANIA DEL PERU S.A.", ClasificDelBien: "Maquinaria y Equipo Industrial", Periodo: "2011", Moneda: "Dólares Americanos", importe: "1,350.39", Comision: "23.00", chek: ""}    
        ]; 
    // Carga Data
    for (var i = 0; i <= mydata1.length; i++) {
        jQuery("#jqGrid_lista_B").jqGrid('addRowData', i + 1, mydata1[i]);
    }
    
    $("#jqGrid_lista_B").setGridWidth($(window).width() - 50);
}

function fn_guardar() {

}

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_abreDetalleCobrar() {
    var strId = $("#hddCodigoContrato").val();
    var strGestion = $("#hddGestion").val();
    
    if (strId === "" || strId === null) {
        parent.fn_mdl_mensajeIco("Debe seleccionar un registro.", "util/images/warning.gif", "ERROR EN SELECCION");
    } else {
        var sTitulo;

        switch(strGestion) {
            case "Demanda":
                sTitulo = "DEMANDA :: COBRO";
                break;
            case "ImpuestoMunicipal":
                sTitulo = "IMPUESTO MUNICIPAL :: COBRO";
                break;    
            case "ImpuestoVehicular":
                sTitulo = "IMPUESTO VEHICULAR :: COBRO";
                break;
            case "Inmatriculacion":
                sTitulo = "INMATRICULACION :: COBRO";
                break;        
            case "Multa":
                sTitulo = "MULTA :: COBRO";
                break;
            case "OtrosConceptos":
                sTitulo = "OTROS CONCEPTOS :: COBRO";
                break;
           case "Siniestro":
                sTitulo = "SINIESTRO :: COBRO";
                break;  
           case "Tasacion":
                sTitulo = "TASACIÓN :: COBRO";
                break;     
           default:
           	    sTitulo = "";
        }
        
	    fn_util_AbreModal(sTitulo, "frmCobroCobrar.aspx?strGestion=" + strGestion, 700, 275, function(){} );
    }
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
    var strGestion = $("#hddGestion").val();
    	  
    if (!(strGestion === "" || strGestion === null)) {
        var sTitulo;
        var sTituloContenido;
        
        switch(strGestion) {
            case "Demanda":
                sTitulo = "DEMANDA :: COBRO";
                sTituloContenido = "Listado de demandas seleccionadas";
                break;
            case "ImpuestoMunicipal":
                sTitulo = "IMPUESTO MUNICIPAL :: COBRO";
                sTituloContenido = "Listado de impuestos seleccionados";
                break;    
            case "ImpuestoVehicular":
                sTitulo = "IMPUESTO VEHICULAR :: COBRO";
                sTituloContenido = "Listado de impuestos seleccionados";
                break;
            case "Inmatriculacion":
                sTitulo = "INMATRICULACION :: COBRO";
                sTituloContenido = "Listado de inmatriculaciones seleccionadas";
                break;        
            case "Multa":
                sTitulo = "MULTA :: COBRO";
                sTituloContenido = "Listado de multas seleccionadas";
                break;
            case "OtrosConceptos":
                sTitulo = "OTROS CONCEPTOS :: COBRO";
                sTituloContenido = "Listado de otros conceptos seleccionadas";
                break;
           case "Siniestro":
                sTitulo = "SINIESTRO :: COBRO";
                sTituloContenido = "Listado de siniestros seleccionadas";
                break;  
           case "Tasacion":
                sTitulo = "TASACIÓN :: COBRO";
                sTituloContenido = "Listado de tasaciones seleccionadas";
                break;   
           	default:
           		sTitulo = "";
           		sTituloContenido = "";
        }
        
	    $("#dvTitulo").html(sTitulo);  
	    $("#lbl_tituloContenido").html(sTituloContenido);  
	}  
}

function verListaBienes() {
    var sTitulo;
    var sTipo;
    var sTituloContenido;
	
    sTipo = $("#hddGestion").val();

    switch(sTipo) {
	     case "Demanda":
            sTitulo = "DEMANDA :: LISTA DE BIENES";
            sTituloContenido = "Listado de demandas seleccionadas";
            break;
        case "ImpuestoMunicipal":
            sTitulo = "IMPUESTO MUNICIPAL :: LISTA DE BIENES";
            sTituloContenido = "Listado de impuestos seleccionados";
            break;    
        case "ImpuestoVehicular":
            sTitulo = "IMPUESTO VEHICULAR :: LISTA DE BIENES";
            sTituloContenido = "Listado de impuestos seleccionados";
            break;
        case "Inmatriculacion":
            sTitulo = "INMATRICULACION :: LISTA DE BIENES";
            sTituloContenido = "Listado de inmatriculaciones seleccionadas";
            break;        
        case "Multa":
            sTitulo = "MULTA :: LISTA DE BIENES";
            sTituloContenido = "Listado de multas seleccionadas";
            break;
        case "OtrosConceptos":
            sTitulo = "OTROS CONCEPTOS :: LISTA DE BIENES";
            sTituloContenido = "Listado de otros conceptos seleccionadas";
            break;
       case "Siniestro":
            sTitulo = "SINIESTRO :: LISTA DE BIENES";
            sTituloContenido = "Listado de siniestros seleccionadas";
            break;  
       case "Tasacion":
            sTitulo = "TASACIÓN :: LISTA DE BIENES";
            sTituloContenido = "Listado de tasaciones seleccionadas";
            break;   
       	default:
       		sTitulo = "";
       		sTituloContenido = "";
	}

    fn_util_AbreModal(sTitulo, "frmBienListado.aspx?Tipo=" + sTituloContenido + "&Titulo=" + sTitulo, 850, 475, function(){} );
}