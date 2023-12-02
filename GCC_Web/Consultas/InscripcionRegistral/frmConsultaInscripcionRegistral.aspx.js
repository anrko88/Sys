//****************************************************************
// Variables Globales
//****************************************************************


var strTipoDocumentoDNI = "1";
var strTipoDocumentoRUC = "2";
var strTipoDocumentoCarnetEx = "3";
var strTipoDocumentoPasaporte = "5";
var strTipoDocumentoOtroDoc = "6";

var blnPrimeraBusqueda;
var intPaginaActual = 1;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	??? - 02/11/2012
//****************************************************************
$(document).ready(function() {
    
    //Valida Campos
    fn_inicializaCampos();

        $('#txtNroDocumento').attr('disabled', true);
        $('#ddlTipoDocumento').change(function() {
            var strValor = $(this).val();
            $("#txtNroDocumento").val("");
            $('#txtNroDocumento').unbind('keypress');
            //$('#cmbTipoPersona').val("0");
            //$('#txtRazonSocial').val("");
            if (fn_util_trim(strValor) == strTipoDocumentoDNI) {
                $('#txtNroDocumento').attr('disabled', false);
                $('#txtNroDocumento').validText({ type: 'number', length: 8 });
            } else if (fn_util_trim(strValor) == strTipoDocumentoRUC) {
                $('#txtNroDocumento').attr('disabled', false);
                $('#txtNroDocumento').validText({ type: 'number', length: 11 });
            } else if (fn_util_trim(strValor) == strTipoDocumentoCarnetEx) {
                $('#txtNroDocumento').attr('disabled', false);
                $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
            } else if (fn_util_trim(strValor) == strTipoDocumentoPasaporte) {
                $('#txtNroDocumento').attr('disabled', false);
                $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
            } else if (fn_util_trim(strValor) == strTipoDocumentoOtroDoc) {
                $('#txtNroDocumento').attr('disabled', false);
                $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
            } else {
                $('#txtNroDocumento').attr('disabled', 'disabled');
            }

        });

    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscar(true);
        }
    });
    //Carga Grilla
    fn_cargaGrilla();




    //On load Page (siempre al final)
    fn_onLoadPage();
   

});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {

    fn_util_SeteaCalendario($("#txtFechaprobablefinObra"));
    fn_util_SeteaCalendario($("#txtFecharealfinObra"));
	fn_util_SeteaCalendario($("#txtFechaIbscripcionMunicipal"));
    fn_util_SeteaCalendario($("#txtFechaEnvioNotaria"));
	fn_util_SeteaCalendario($("#txtFechaInscripcionRegistral"));
    fn_util_SeteaCalendario($("#txtFechaPropiedad"));
    $('#txtNroContrato').validText({ type: 'number', length: 8 });
    $('#txtcuCliente').validText({ type: 'number', length: 10 });
    $('#txtRazonSocialNombre').validText({ type: 'comment', length: 100 });
    $('#txtPlaca').validText({ type: 'alphanumeric', length: 10 });
    $('#txtNroLote').validText({ type: 'number', length: 8 });
	$('#txtKardex').validText({ type: 'number', length: 10 });
	$('#txtOficinaRegistral').validText({ type: 'comment',length:50});
	$('#txtOficinaRegistral').maxLength(50);
    
}


//************************************************************
// Función		:: 	fn_cargarTipoBien
// Descripcion 	:: 	Carga el combo de tipo de bien
// Log			:: 	AEP - 10/10/2012
//************************************************************
function fn_cargarTipoBien() {
    var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL104", "pstrCodigoGenerico", $("#cmbClasificacionBien").val()];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
          $('#cmbTipoBien').html(arrResultado[1]); 
          //$('#ddlTipoBien').val($('#hidCodTipoBien').val());
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
}


//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 08/11/2012
//****************************************************************
function fn_cargaGrilla() {

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_ListarImpuesto();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Nº Contrato','Departamento', 'Provincia', 'Distrito','Ubicación' ,'Código del Predio', 'Estado Inscripción Municipal', 'Estado Inscripción Registral', 'Propiedad', 'Estado del Registro del Bien', 'Fecha de Baja',''],
        colModel: [
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 60, sorttype: "string", align: "center" },
            { name: 'DepartamentoNombre', index: 'DepartamentoNombre', width: 60, sorttype: "string", align: "left" },
        	{ name: 'ProvinciaNombre', index: 'ProvinciaNombre', width: 60, align: "left", sorttype: "string" },
		    { name: 'DistritoNombre', index: 'DistritoNombre', width: 60, align: "left", sorttype: "string" },
		    { name: 'Ubicacion', index: 'Ubicacion', width: 100, align: "left", sorttype: "string", editable: true },
            { name: 'Codigopredio', index: 'Codigopredio', width: 60, align: "center", sorttype: "string", editable: true },
		    { name: 'EstadoMunicipal', index: 'EstadoMunicipal', width: 60, align: "left" },
        	{ name: 'EstadoInscripcionRRPP', index: 'EstadoInscripcionRRPP', width: 60, align: "center" },
            { name: 'prioridad', index: 'prioridad', width: 60, align: "center" },
		    { name: 'EstadoBienLogico', index: 'EstadoBienLogico', width: 60, align: "center", sorttype: "string" },
		    { name: 'FechaBaja', index: 'FechaBaja', width: 60, align: "center", sorttype: "string"},
            { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden:true }
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
                  $("#hddRowId").val(id);
                //  $("#hidSecFinanciamiento").val(rowData.SecFinanciamiento);
                //  $("#hidCodigoSolicitudCredito").val(rowData.CodSolicitudCredito);
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            var strCodigoContrato= rowData.CodSolicitudCredito;
        	var strCodigoBien = rowData.SecFinanciamiento;
            fn_util_redirect('frmConsultaInscripcionRegistralRegistro.aspx?codContrato=' + strCodigoContrato + '&codbien=' + strCodigoBien);

        }
    });

 
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    $("#hddRowId").val('');
}

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	AEP - 12/11/2012
//****************************************************************

function fn_buscar(bSearch) {
    blnPrimeraBusqueda = bSearch;
    fn_ListarImpuesto();
}

//****************************************************************
// Funcion		:: 	fn_ListarImpuesto
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	AEP - 12/11/2012
//****************************************************************
function fn_ListarImpuesto() {
    if (!blnPrimeraBusqueda) {
        return;
    } else {


        parent.fn_blockUI();

        var arrParametros = ["pPageSize",               fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage",            intPaginaActual,    // Página actual.
                             "pSortColumn",             fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
                             "pSortOrder",              fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
                             //Agregar mas campos de Filtro
                             "pCodContrato",            $("#txtNroContrato").val(),
                             "pCuCliente",              $("#txtcuCliente").val(),
                             "pRazonSocial",            $("#txtRazonSocialNombre").val(),
                             "pCodTipoDocumento",       $("#ddlTipoDocumento").val(),
                             "pCodNroDocumento",        $("#txtNroDocumento").val(),
                             "pCodClasificacionBien",   $("#cmbClasificacionBien").val(),
                             "pCodTipoBien",            $("#cmbTipoBien").val(),
                             "pCodEstaContrato",        $("#cmbEstadoContrato").val(),
                             "pKardex",                 $("#txtKardex").val(),
                             "pFechaProbableFinObra",   Fn_util_DateToString($("#txtFechaprobablefinObra").val()),
                             "pFechaRealFinObra",       Fn_util_DateToString($("#txtFecharealfinObra").val()),
                             "pCodEstadoInscMunicipal", $("#cmbEstadoInscripcionMunicipal").val(),
                             "pFechaInsMunicipal",      Fn_util_DateToString($("#txtFechaIbscripcionMunicipal").val()),
                             "pCodNoptaria",            $("#cmbNotaria").val(),
                             "pFechaEnvioNotaria",      Fn_util_DateToString($("#txtFechaEnvioNotaria").val()),
                             "pCodEstadoInscRegistral", $("#cmbEstadoInscripcionRegistral").val(),
                             "pFechaInscrRegistral",    Fn_util_DateToString($("#txtFechaInscripcionRegistral").val()),
                             "pOficinaRegistral",       $("#txtOficinaRegistral").val(),
        	                 "pFechaPropiedad",         Fn_util_DateToString($("#txtFechaPropiedad").val())

                             ];

        fn_util_AjaxWM("frmConsultaInscripcionRegistral.aspx/ListaBienContratoInscripcionResgistral",
                   arrParametros,
                   function(jsondata) {
                       jqGrid_lista_A.addJSONData(jsondata);
                       parent.fn_unBlockUI();
                       fn_doResize();
                   },
                   function(resultado) {
                       parent.fn_unBlockUI();
                       var error = eval("(" + resultado.responseText + ")");
                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
                   }
    );
    }
};


//****************************************************************
// Funcion		:: 	fn_limpiar
// Descripción	::	Realiza la limpieza de campos (Listado)
// Log			:: 	AEP - 12/11/2012
//****************************************************************
function fn_limpiar() {
    blnPrimeraBusqueda = false;
    $("#txtNroContrato").val('');
    $("#txtcuCliente").val('');
    $("#txtRazonSocialNombre").val('');
    $("#ddlTipoDocumento").val('0');
    $("#ddlEstadoCobro").val('0');
    $("#ddlEstadoPago").val('0');
    $("#txtNroDocumento").val('');
    $("#txtNroLote").val('');
    $("#txtPlaca").val('');
    $("#txtFechaInscripcionDesde").val(''),
    $("#txtFechaInscripcionHasta").val(''),
    $("#ddlEstadoCobro").val(''),
    $("#ddlEstadoPago").val(''),
    $("#txtAnioFabricacion").val(''),
    $("#txtPeriodo").val('');
    $("#jqGrid_lista_A").GridUnload();
    fn_cargaGrilla();
}


//****************************************************************
// Funcion		:: 	fn_abreEditar
// Descripción	::	Abre Editar Registro
// Log			:: 	AEP - 15/11/2012
//****************************************************************
function fn_abreEditar() {
    var id = $("#hddRowId").val();
    if (IsNullOrEmpty(id)) {
    	parent.fn_mdl_mensajeError("Debe seleccionar un Impuesto Registral", function() { }, "VALIDACIÓN");
    } else {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            var strCodigoContrato= rowData.CodSolicitudCredito;
        	var strCodigoBien = rowData.SecFinanciamiento;
            fn_util_redirect('frmConsultaInscripcionRegistralRegistro.aspx?codContrato=' + strCodigoContrato + '&codbien=' + strCodigoBien);    }
}


//****************************************************************
// Funcion		:: 	fn_abreNuevo
// Descripción	::	Abre Nuevo Registro
// Log			:: 	??? - 02/11/2012
//****************************************************************
//function fn_abreNuevo() {
//    parent.fn_blockUI();
//    fn_util_redirect("frmImpuestoVehicularRegistro.aspx");
//    //parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/ImpuestoVehicular/frmImpuestoVehicularNuevo.aspx", 500, 300, function() { });
//}


//****************************************************************
// Funcion		:: 	fn_abreCargarArchivo
// Descripción	::	Abre Nuevo Registro
// Log			:: 	AEP - 08/11/2012
//****************************************************************
//function fn_abreCargarArchivo() {
//    //parent.fn_util_redirect("frmImpuestoVehicularMasivoResumenCarga.aspx");
//    parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/ImpuestoVehicular/frmImpuestoVehicularCargarMasivo.aspx", 550, 150, function() { });
//}



//****************************************************************
// Funcion		:: 	fn_abreLiquidacion
// Descripción	::	Abre Nuevo Registro
// Log			:: 	AEP - 08/11/2012
//****************************************************************
//function fn_abreLiquidacion() {
//    //parent.fn_util_redirect("frmImpuestoVehicularMasivoResumenCarga.aspx");
//    parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/ImpuestoVehicular/frmImpuestoVehicularLiquidar.aspx", 950, 530, function() { });
//}

//****************************************************************
// Funcion		:: 	fn_abreAsignarCheque
// Descripción	::	Abre una ventana para asignar cheque
// Log			:: 	AEP - 08/11/2012
//****************************************************************
//function fn_abreAsignarCheque() {
//    //parent.fn_util_redirect("frmImpuestoVehicularMasivoResumenCarga.aspx");
//    parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/ImpuestoVehicular/frmImpuestoVehicularAsigCheque.aspx", 550, 220, function() { });
//}

//****************************************************************
// Funcion		:: 	fn_AbrirResumenCarga
// Descripción	::	Redirecciona a la pagina de Resumen de Carga
// Log			:: 	AEP - 13/11/2012
//****************************************************************
function fn_AbrirResumenCarga() {
    fn_util_redirect("frmImpuestoVehicularMasivoResumenCarga.aspx");
}


/*****************************************************************
Funcion		:: 	fn_Reporte
Descripción	::	Genera Reporte
Log			:: 	AEP - 18/01/2013
***************************************************************** */

function fn_Reporte() {
 $("#btnGenerar").click();
}