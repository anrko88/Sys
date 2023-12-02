var blnPrimeraBusqueda;
var intPaginaActual = 1;
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
            fn_buscarBien(true);
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

});

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	WCR - 14/06/2012
//****************************************************************
function fn_cargaGrilla() {

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");	 
            fn_buscar();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Nº Contrato', 'CU Cliente','Razón Social o Nombre','Clasificación del Bien','Tipo de Bien','Estado del Bien', 'Cantidad','Fecha de Transferencia','CodSolicitudCredito','SecFinaciamiento'],
        colModel: [
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 50, sorttype: "string", align: "center" },
            { name: 'CodUnico', index: 'CodUnico', width: 50, sorttype: "string", align: "center" },
		    { name: 'RazonSocial', index: 'RazonSocial', width: 150, sorttype: "string", align: "left" },
		    { name: 'ClasificacionBien', index: 'ClasificacionBien', width: 150, align: "center", sorttype: "string" },
		    { name: 'TipoBien', index: 'TipoBien', width: 60, align: "center" },
		    { name: 'EstadoBien', index: 'EstadoBien', width: 60, align: "center", sorttype: "string" },
		    { name: 'CantidadProducto', index: 'CantidadProducto', width: 50, align: "right", sorttype: "string" },
		    { name: 'FechaTransferencia', index: 'FechaTransferencia', width: 80, align: "center", sorttype: "string" },
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
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
            $("#hidSecFinanciamiento").val(rowData.SecFinanciamiento);
            $("#hidCodigoSolicitudCredito").val(rowData.CodSolicitudCredito);
        },
        ondblClickRow: function() {
            parent.fn_blockUI();
            fn_util_redirect('frmMantenimientoBienRegistro.aspx?co=1&csf=' + $('#hidSecFinanciamiento').val() + '&csc=' + $("#hidCodigoSolicitudCredito").val());
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    
}


//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	
// Log			:: 	WCR - 14/06/2012
//****************************************************************
function fn_buscarBien(pblnBusqueda) {
    blnPrimeraBusqueda = pblnBusqueda;
	intPaginaActual = 1;
    fn_buscar();
}
	
function fn_buscar() {
	if(!blnPrimeraBusqueda) {
		return;
		
	} else {
	    var vNumeroContrato = $('#txtContrato').val() == undefined ? "" : $('#txtContrato').val();
        var vCUCliente = $('#txtCUCliente').val() == undefined ? "" : $('#txtCUCliente').val();
        var vRazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
        var vClasificacionbien = $('#ddlClasificacionbien').val() == undefined ? "" : $('#ddlClasificacionbien').val();
        var vTipobien = $('#ddlTipobien').val() == undefined ? "" : $('#ddlTipobien').val();
        var vDepartamento = $('#ddlDepartamento').val() == undefined ? "" : $('#ddlDepartamento').val();
        var vEstado = $('#ddlEstado').val() == undefined ? "" : $('#ddlEstado').val();
        var vFechaTransferencia = $('#txtFechaTransferencia').val() == undefined ? "" : $('#txtFechaTransferencia').val();
	
    	 var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", intPaginaActual,    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
                             "pNumeroContraro", vNumeroContrato,
                             "pCUCliente", vCUCliente,
                             "pRazonSocial", vRazonSocial,
                             "pCodClasificacionBien", vClasificacionbien,
                             "pCodTipoBien", vTipobien,
                             "pCodEstadoBien", vEstado,
                             "pCodDepartamento", vDepartamento,
                             "pFechaTransferencia", Fn_util_DateToString(vFechaTransferencia)
                            ];
		

    fn_util_AjaxWM("frmMantenimientoBienLista.aspx/BuscarBien",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_A.addJSONData(jsondata);
                    	fn_doResize();
                    },
                    function(request) {
                        fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                    }
                   );
	}
	
}

//****************************************************************
// Funcion		:: 	fn_Editar
// Descripción	::	
// Log			:: 	WCR - 15/06/2012
//****************************************************************
function fn_Editar() {
    if (($('#hidSecFinanciamiento').val() != '0') && ($("#hidCodigoSolicitudCredito").val() != '')) {
        fn_util_redirect('frmMantenimientoBienRegistro.aspx?co=1&csf=' + $('#hidSecFinanciamiento').val() + '&csc=' + $("#hidCodigoSolicitudCredito").val());
    } else {
        parent.fn_mdl_mensajeIco("&nbsp;&nbsp;- Debe seleccionar un registro de la lista", "Util/images/warning.gif", "ADVERTENCIA AL EDITAR");
    }

}

//****************************************************************
// Funcion		:: 	fn_Editar
// Descripción	::	
// Log			:: 	WCR - 15/06/2012
//****************************************************************
function fn_Consultar() {
    if (($('#hidSecFinanciamiento').val() != '0') && ($("#hidCodigoSolicitudCredito").val() != '')) {
        fn_util_redirect('frmMantenimientoBienRegistro.aspx?co=2&csf=' + $('#hidSecFinanciamiento').val() + '&csc=' + $("#hidCodigoSolicitudCredito").val());
    } else {
        parent.fn_mdl_mensajeIco("&nbsp;&nbsp;- Debe seleccionar un registro de la lista", "Util/images/warning.gif", "ADVERTENCIA AL CONSULTAR");
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
	$("#ddlTipobien").val(0);
	$("#txtFechaTransferencia").val('');
	$("#ddlDepartamento").val(0);
	$("#ddlEstado").val(0);
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
}