var blnPrimeraBusqueda;
var intPaginaActual = 1;

var strPeruana = 'P';
var strExtranjero = 'E';
var strPaisPeru = '504';
var strTipoPersonaJuridica = '2';
var strTipoPersonaNatural = '1';
var strTipoDocumentoRuc = '2';
var strTipoDocumentoOtros = '6';
var strTipoCuentaCorriente = '01';
var strMonedaSoles = '001';
var strTipoBancoIBK = '001';
var strTipoBancoBN = '002';



var strTipoDocumentoDNI = "1";
var strTipoDocumentoRUC = "2";
var strTipoDocumentoCarnetEx = "3";
var strTipoDocumentoPasaporte = "5";
var strTipoDocumentoOtroDoc = "6";

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    //Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    //Carga Grilla
    fn_cargaGrilla();
    $("#jqGrid_listado").setGridWidth($(window).width() - 65);

    //On load Page (siempre al final)
    fn_onLoadPage();
	fn_InicializarCampos();

   $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscar(true);
        }
    });
	
    //-------------------------------------------
    //Valida Change del ClasificacionBien
    //-------------------------------------------
    $('#ddlClasificacionbien').change(function() {
        var strValor = $(this).val();

        var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL104", "pstrCodigoGenerico", strValor];
        var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

        if (arrResultado.length > 0) {
            if (arrResultado[0] == "0") {
                $('#ddlTipobien').html(arrResultado[1]);
            } else {
                var strError = arrResultado[1];
                fn_mdl_alert(strError.toString(), function() { });
            }
        }
    });
	$('#txtNumeroDocumento').attr('disabled',true);
	$('#ddlTipoDocumento').change(function() {
        var strValor = $(this).val();
        $("#txtNumeroDocumento").val("");
        $('#txtNumeroDocumento').unbind('keypress');
	  	//$('#cmbTipoPersona').val("0");
        //$('#txtRazonSocial').val("");
        if (fn_util_trim(strValor) == strTipoDocumentoDNI) {
        	$('#txtNumeroDocumento').attr('disabled',false);
            $('#txtNumeroDocumento').validText({ type: 'number', length: 8 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoRUC) {
        	$('#txtNumeroDocumento').attr('disabled',false);
            $('#txtNumeroDocumento').validText({ type: 'number', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoCarnetEx) {
        	$('#txtNumeroDocumento').attr('disabled',false);
            $('#txtNumeroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoPasaporte) {
        	$('#txtNumeroDocumento').attr('disabled',false);
            $('#txtNumeroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if(fn_util_trim(strValor)==strTipoDocumentoOtroDoc){
        	$('#txtNumeroDocumento').attr('disabled',false);
            $('#txtNumeroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else {
            $('#txtNumeroDocumento').attr('disabled','disabled');
        }

	  });
	
	 if($("#hidEstadoVerificacion").val() != "")
	    {
		parent.fn_util_MuestraLogPage($("#hidMensajeVerificacion").val(), "I");
	    }
});

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 26/09/2012
//****************************************************************
function fn_cargaGrilla() {

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");	 
            fn_buscarContrato();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Nro. Contrato', 'CU Cliente','Razón Social o Nombre','Tipo de Documento','Nro. Documento','Clasificación del Bien','Tipo de Bien','Estado del Contrato','Opción de Compra','','',''],
        colModel: [
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 50, sorttype: "string", align: "center" },
            { name: 'CodUnico', index: 'CodUnico', width: 50, sorttype: "string", align: "center" },
		    { name: 'NombreCliente', index: 'NombreCliente', width: 150, sorttype: "string", align: "left" },
        	{ name: 'TipoDocumento', index: 'FechaTransferencia', width: 80, align: "center", sorttype: "string" },
		    { name: 'NumeroDocumento', index: 'CantidadProducto', width: 50, align: "right", sorttype: "string" },
		    { name: 'ClasificacionBien', index: 'ClasificacionBien', width: 150, align: "center", sorttype: "string",editable:true },
		    { name: 'TipoBien', index: 'TipoBien', width: 60, align: "center" },
		    { name: 'EstadoContrato', index: 'EstadoContrato', width: 60, align: "center", sorttype: "string" },		    
		    { name: 'OpcionCompra', index: 'codOpcionCompra', width: 80, align: "center", sorttype: "string" },
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
            { name: 'CodigoEstadoContrato', index: 'CodigoEstadoContrato', hidden: true },
        	{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true } 
	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
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
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            //$("#hidSecFinanciamiento").val(rowData.SecFinanciamiento);
            $("#hidCodigoSolicitudCredito").val(rowData.CodSolicitudCredito);
        },
        ondblClickRow: function() {
 //           parent.fn_blockUI();
//            fn_util_redirect('frmMantenimientoBienContrato.aspx?co=1&csc=' + $("#hidCodigoSolicitudCredito").val());
        	}
    });
	
//	for (var i = 0; i <= mydata.length; i++) {
//	     jQuery("#jqGrid_lista_A").jqGrid('addRowData', i + 1, mydata[i]);
//	 }
//	
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    
}


//************************************************************
// Función		:: 	fn_cargarTipoBien
// Descripcion 	:: 	Carga el combo de tipo de bien
// Log			:: 	AEP - 10/10/2012
//************************************************************
function fn_cargarTipoBien() {
    var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL104", "pstrCodigoGenerico", $("#ddlClasificacionbien").val()];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
          $('#ddlTipoBien').html(arrResultado[1]); 
          //$('#ddlTipoBien').val($('#hidCodTipoBien').val());
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
}


//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	
// Log			:: 	WCR - 14/06/2012
//****************************************************************
function fn_buscar(pblnBusqueda) {
    blnPrimeraBusqueda = pblnBusqueda;
	intPaginaActual = 1;
    fn_buscarContrato();
}
	
function fn_buscarContrato() {
	if(!blnPrimeraBusqueda) {
		return;
		
	} else {
		
		parent.fn_blockUI();

		$("#hidEstadoVerificacion").val('');
	    var vNumeroContrato = $('#txtContrato').val() == undefined ? "" : $('#txtContrato').val();
        var vCUCliente = $('#txtCUCliente').val() == undefined ? "" : $('#txtCUCliente').val();
        var vRazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
        var vClasificacionbien = $('#ddlClasificacionbien').val() == undefined ? "" : $('#ddlClasificacionbien').val();
        var vTipobien = $('#ddlTipobien').val() == undefined ? "" : $('#ddlTipobien').val();
		var vEstadoContrato = $('#ddlEstadoContrato').val() == undefined ? "" : $('#ddlEstadoContrato').val();
        var vTipoDocumento = $('#ddlTipoDocumento').val() == undefined ? "" : $('#ddlTipoDocumento').val();
		var vNumeroDocumento = $('#txtNumeroDocumento').val() == undefined ? "" : $('#txtNumeroDocumento').val();
        var vKardex= $('#txtKardex').val() == undefined ? "" : $('#txtKardex').val();
	
    	 var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", intPaginaActual,    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
                             "pNumeroContraro", vNumeroContrato,
                             "pCUCliente", vCUCliente,
                             "pRazonSocial", vRazonSocial,
                             "pCodClasificacionBien", vClasificacionbien,
                             "pCodTipoBien", vTipobien,
    	 	                 "pEstadoContrato", vEstadoContrato,
    	 	                 "pTipoDocumento", vTipoDocumento,
                             "pNumeroDocumento", vNumeroDocumento,
                             "pKardex", vKardex
                            ];


    	 fn_util_AjaxWM("frmBuscarContrato.aspx/BuscarContrato",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_A.addJSONData(jsondata);
                    	parent.fn_unBlockUI();
                    	fn_doResize();
                    },
                    function(request) {
                        fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                    }
                   );
	}
	
}

//****************************************************************
// Funcion		:: 	fn_Limpiar
// Descripción	::	
// Log			:: 	AEP - 17/07/2012
//****************************************************************
function fn_Limpiar() {
	blnPrimeraBusqueda=false;
	$("#txtContrato").val('');
	$("#txtCUCliente").val('');
	$("#txtRazonSocial").val('');
	$("#ddlClasificacionbien").val(0);
	$("#ddlTipoDocumento").val(0);
	$("#ddlTipobien").val(0);
	$("#txtNumeroDocumento").val('');
	$("#ddlEstadoContrato").val(0);
	$("#txtKardex").val('');
	$("#jqGrid_lista_A").GridUnload();
	fn_cargaGrilla();

}

//****************************************************************
// Funcion		:: 	fn_InicializarCampos
// Descripción	::	
// Log			:: 	AEP - 17/07/2012
//****************************************************************
function fn_InicializarCampos() {
	blnPrimeraBusqueda=false;
	$('#txtContrato').validText({ type: 'number', length: 8 });
    $('#txtCUCliente').validText({ type: 'number', length: 10 });
	$('#txtRazonSocial').validText({ type: 'comment', length: 100 });
	$('#txtKardex').validText({ type: 'number', length: 10 });
	
	$("#ddlEstadoContrato option[value='10']").remove();
	$("#ddlEstadoContrato option[value='14']").remove();
	$("#ddlEstadoContrato option[value='01']").remove();
	$("#ddlEstadoContrato option[value='12']").remove();
	$("#ddlEstadoContrato option[value='02']").remove();
}