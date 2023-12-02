//****************************************************************
// Variables Globales
//****************************************************************
var strComboVacio = "<option value='0'>[-Seleccione-]</option>"

var blnPrimeraBusqueda = false;
var intPaginaActual = 1;

var strTipoDocumentoDNI = "1";
var strTipoDocumentoRUC = "2";
var strTipoDocumentoCarnetEx = "3";
var strTipoDocumentoPasaporte = "5";
var strTipoDocumentoOtroDoc = "6";
var strNroLote = '';

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	??? - 02/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();

    //Busca con Enter
    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscarImpuestoMuni(true);
        }
    });

    $("#txtCodMunicipalidad").focusout(function() {
        var strValor = $(this).val();
        $("#txtMunicipalidad").val("");
    });

    $("#txtMunicipalidad").focusout(function() {
        var strValor = $(this).val();
        $('#txtCodMunicipalidad').val("");
    });
    //---------------------------------
    //Valida Tipo Documento
    //---------------------------------
    $('#cmdTipoDoc').change(function() {
        var strValor = $(this).val();
        $("#txtNroDocumento").val("");
        $('#txtNroDocumento').unbind('keypress');
        if (fn_util_trim(strValor) == strTipoDocumentoDNI) {
            $('#txtNroDocumento').validText({ type: 'number', length: 8 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoRUC) {
            $('#txtNroDocumento').validText({ type: 'number', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoCarnetEx) {
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoPasaporte) {
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        } else {
            $('#txtNroDocumento').validText({ type: 'alphanumeric', length: 11 });
        }
    });


    $('#imgBsqMunicipalidad').click(function() {
        if ($('#txtCodMunicipalidad').val() == '' && $('#txtMunicipalidad').val() == '') {
            parent.fn_unBlockUI();
            parent.fn_util_AbreModal("Municipalidad", "Comun/frmMunicipalidadesConsulta.aspx?Codigo= " + $('#txtCodMunicipalidad').val() + '&Descripcion= ' + $('#txtMunicipalidad').val(), 800, 600, function() { });
        }
        else {
            var paramArray = ["pCodMunicipalidad", $('#txtCodMunicipalidad').val(),
                          "pMunicipalidad", $('#txtMunicipalidad').val()
                         ];
            fn_util_AjaxWM("frmImpuestoMultaInmuebleListado.aspx/ConsultaMunicipalidad",
                       paramArray,
                       function(jsondata) {
                           $('#txtCodMunicipalidad').val("");
                           $('#txtMunicipalidad').val("");

                           $('#txtCodMunicipalidad').val(fn_util_trim(jsondata.Items[0].CLAVE1));
                           $('#txtMunicipalidad').val(fn_util_trim(jsondata.Items[0].VALOR1));
                       },
                       function(resultado) {
                           parent.fn_unBlockUI();
                           parent.fn_mdl_mensajeIco("Se produjo un error al obtener los datos del contrato", "util/images/error.gif", "ERROR EN CONSULTA");
                       }
    );

            //Resize Pantalla    
            fn_doResize();
        }
    });
    fn_configuraGrilla();
    //Inicio IBK - AAE - Si tengo lote busco
    if ($('#txtLote').val() != '') {
        fn_buscarImpuestoMuni(true);
    }
    
    //Fin IBK
    //On load Page (siempre al final)
    fn_onLoadPage();
    
});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {

    //cmbDepartamento
    //cmbProvincia
    //cmbDistrito
    //cmdTipoDoc
    //cmdEstadoPago
    //cmdEstadoCobro

    $('#txtNroContrato').validText({ type: 'number', length: 8 });
    $('#txtRazonSocial').validText({ type: 'comment', length: 100 });
    $('#txtNroDocumento').validText({ type: 'number', length: 11 });
    $('#txtPeriodo').validText({ type: 'number', length: 4 });
    $('#txtLote').validText({ type: 'number', length: 8 });

}


//****************************************************************
// Funcion		:: 	fn_configuraGrilla
// Descripción	::	Inicializa grilla
// Log			:: 	JRC - 02/11/2012
// Modificacion ::  Se agregó la columna Municipalidad
// Autor        ::  JJM IBK
//****************************************************************
function fn_configuraGrilla() {
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            //intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_realizaBusquedaImpuestoMuni();
        },
        jsonReader: {                 // Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage",      // Número de página actual.
            total: "PageCount",       // Número total de páginas.
            records: "RecordCount",   // Total de registros a mostrar.
            repeatitems: false,
            id: "SecImpuesto" // Índice de la columna con la clave primaria.
        },
        colNames: ['', '', 'Nº Contrato', 'CU Cliente', 'Razón Social o Nombre', 'N° Lote', '', '', 'Departamento', 'Provincia', 'Distrito', 'Municipalidad', '', 'Dirección', 'Periodo', 'Importe Total', 'Estado Pago', 'Estado Cobro', 'N° Cheque', ''],
        colModel: [
			{ name: 'SecImpuesto', index: 'SecImpuesto', hidden: true },
			{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
			{ name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', sortable: true, sorttype: "string", width: 25, align: "center" },
			{ name: 'CodUnico', index: 'CodUnico', width: 40, sorttype: "string", align: "center" },
			{ name: 'ClienteRazonSocial', index: 'ClienteRazonSocial', sortable: true, sorttype: "string", width: 80, align: "left" },
			{ name: 'NroLote', index: 'NroLote', sortable: true, width: 20, align: "center", sorttype: "string" },
			{ name: 'NombreTipoDocumento', index: 'NombreTipoDocumento', hidden: true },
		    { name: 'NumeroDocumento', index: 'NumeroDocumento', hidden: true },
		    { name: 'DesDepartamento', index: 'DesDepartamento', sortable: true, width: 40, sorttype: "string", align: "center" },
		    { name: 'DesProvincia', index: 'DesProvincia', sortable: true,  sorttype: "string", align: "center", hidden: true },
		    { name: 'DesDistrito', index: 'DesDistrito', sortable: true, sorttype: "string", width: 40, align: "center" },
		    { name: 'DesMunicipalidadBien', index: 'DesMunicipalidadBien', sortable: true, width: 80, sorttype: "string", align: "center" },
		    { name: 'CodMunicipalidadBien', index: 'CodMunicipalidadBien', hidden: true },
		    { name: 'Ubicacion', index: 'Ubicacion', sortable: true, sorttype: "string", width: 40, align: "left" },
		    { name: 'Periodo', index: 'Periodo', sortable: true, sorttype: "string", width: 20, align: "center" },
		    { name: 'Total', index: 'Total', sortable: false, width: 25, align: "right", sorttype: "string", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'DesEstadoPago', index: 'DesEstadoPago', sortable: false, width: 25, align: "center", sorttype: "string" },
		    { name: 'DesEstadoCobro', index: 'DesEstadoCobro', sortable: false, width: 25, align: "center", sorttype: "string" },		    
		    { name: 'NroCheque', index: 'NroCheque', sortable: false, width: 30, align: "center", sorttype: "string" },
		    { name: '', index: '', width: 3, hidden: true }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,                      // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'NroLote', // Columna a ordenar por defecto.
        sortorder: 'desc',               // Criterio de ordenación por defecto.
        viewrecords: true,               // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodContrato").val(rowData.CodSolicitudCredito);
            $("#hddCodBien").val(rowData.SecFinanciamiento);
            $("#hddCodImpuesto").val(rowData.SecImpuesto);
            $("#hddPeriodo").val(rowData.Periodo);
            $("#hddMunicipalidad").val(rowData.CodMunicipalidadBien);
            $("#hddNroCheque").val(rowData.NroCheque);
            strNroLote = rowData.NroLote;
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodContrato").val(rowData.CodSolicitudCredito);
            $("#hddCodBien").val(rowData.SecFinanciamiento);
            $("#hddCodImpuesto").val(rowData.SecImpuesto);
            $("#hddPeriodo").val(rowData.Periodo);
            $("#hddMunicipalidad").val(rowData.CodMunicipalidadBien);
            $("#hddNroCheque").val(rowData.NroCheque);
            strNroLote = rowData.NroLote;
            fn_abreEditar();
        }
    });

    $("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

    // Le asigna el ancho a usar para la grilla.
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 55);
}


//****************************************************************
// Funcion		:: 	fn_buscarImpuestoMuni
// Descripción	::	Busca listado de cotizacion por parametros
// Log			:: 	JRC - 07/11/2012
// Modificacion ::  Se agregó el parametro strMunicipalidad
// Autor        ::  JJM IBK
//****************************************************************
function fn_buscarImpuestoMuni(pblnBusqueda) {
    blnPrimeraBusqueda = pblnBusqueda;
    intPaginaActual = 1;
    fn_realizaBusquedaImpuestoMuni();
}
function fn_realizaBusquedaImpuestoMuni() {

    if (!blnPrimeraBusqueda) {
        return;
    }

    try {
        parent.fn_blockUI();

        var cmbDepartamento = $('#cmbDepartamento').val() == undefined ? "" : $('#cmbDepartamento').val();
        var cmbProvincia = $('#cmbProvincia').val() == undefined ? "" : $('#cmbProvincia').val();
        var cmbDistrito = $('#cmbDistrito').val() == undefined ? "" : $('#cmbDistrito').val();
        var txtNroContrato = $('#txtNroContrato').val() == undefined ? "" : $('#txtNroContrato').val();
        var txtRazonSocial = $('#txtRazonSocial').val() == undefined ? "" : $('#txtRazonSocial').val();
        var cmdTipoDoc = $('#cmdTipoDoc').val() == undefined ? "" : $('#cmdTipoDoc').val();
        var NroDocumento = $('#NroDocumento').val() == undefined ? "" : $('#NroDocumento').val();
        var txtPeriodo = $('#txtPeriodo').val() == undefined ? "" : $('#txtPeriodo').val();
        var txtLote = $('#txtLote').val() == undefined ? "" : $('#txtLote').val();
        var cmdEstadoPago = $('#cmdEstadoPago').val() == undefined ? "" : $('#cmdEstadoPago').val();
        var cmdEstadoCobro = $('#cmdEstadoCobro').val() == undefined ? "" : $('#cmdEstadoCobro').val();

        //        var cmdMunicipalidad = $('#cmbMunicipalidad').val() == undefined ? "" : $('#cmbMunicipalidad').val();
        var cmdMunicipalidad = $('#txtCodMunicipalidad').val() == "" ? "0" : $('#txtCodMunicipalidad').val(); //JJM IBK

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación                             
                             "pstrDepartamento", cmbDepartamento,
                             "pstrProvincia", cmbProvincia,
                             "pstrDistrito", cmbDistrito,
                             "pstrNroContrato", txtNroContrato,
                             "pstrRazonSocial", txtRazonSocial,
                             "pstrTipoDoc", cmdTipoDoc,
                             "pstrDocumento", NroDocumento,
                             "pstrPeriodo", txtPeriodo,
                             "txtLote", txtLote,
                             "pstrEstadoPago", cmdEstadoPago,
                             "pstrEstadoCobro", cmdEstadoCobro,
                             "pstrMunicipalidad", cmdMunicipalidad //IBK JJM                             
                            ];

        fn_util_AjaxWM("frmImpuestoMultaInmuebleListado.aspx/ListaImpuestoMunicipal",
                arrParametros,
                function(jsondata) {                    
                    jqGrid_lista_A.addJSONData(jsondata);
                    parent.fn_unBlockUI();
                    fn_doResize();
                    
                },
                function(resultado) {
                    parent.fn_unBlockUI();
                    //alert(resultado.responseText);
                    //fn_util_alert(jQuery.parseJSON(resultado.responseText).Message);
                    var error = eval("(" + resultado.responseText + ")");
                    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
                }
        );

    } catch (ex) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }

}





//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistrito
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_LimpiaComboDistrito() {
    $('#cmbDistrito').empty();
    $('#cmbDistrito').html('<option value="0">[- Seleccionar -]</option>');
}

//****************************************************************
// Funcion		:: 	fn_cargaComboProvincia
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_cargaComboProvincia(valor) {

    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbProvincia').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistrito();
}


//****************************************************************
// Funcion		:: 	fn_cargaComboDistrito
// Descripción	::	F
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_cargaComboDistrito(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbDistrito').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}


//****************************************************************
// Funcion		:: 	fn_abreNuevo
// Descripción	::	Abre Nuevo Registro
// Log			:: 	JRC - 02/11/2012
//****************************************************************
function fn_abreNuevo() {
    fn_util_globalRedirect("/GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleRegistro.aspx");
}


//****************************************************************
// Funcion		:: 	fn_abreEditar
// Descripción	::	Edita Impuesto
// Log			:: 	JRC - 27/11/2012
//****************************************************************
function fn_abreEditar() {

    var strCodContrato = $("#hddCodContrato").val();
    var strCodBien = $("#hddCodBien").val();
    var strCodImpuesto = $("#hddCodImpuesto").val();
    var Periodo = $("#hddPeriodo").val();
    var Muni = $("#hddMunicipalidad").val();
    var Cheque = $("#hddNroCheque").val();


    if (fn_util_trim(strCodImpuesto) == "") {
        //Inicio IBK - AAE - Cargo lote si agrege un valor en nro de lote
        //parent.fn_mdl_mensajeError("Debe seleccionar un Impuesto", function() { }, "VALIDACIÓN");
        fn_editarLoteParam()
        //Fin IBK
    }
    else if (strNroLote != '') {
        fn_util_globalRedirect("/GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleRegistro.aspx?hddCodContrato=" + strCodContrato + "&hddCodBien=" + strCodBien + "&hddCodImpuesto=" + strCodImpuesto + "&NroLote=" + strNroLote + "&Municipalidad=" + Muni + "&Periodo=" + Periodo); //IBK JJM        
    }

}




//****************************************************************
// Funcion		:: 	fn_abreAsignarCheque
// Descripción	::	Abre Modal para asignar Cheque
// Log			:: 	JRC - 02/11/2012
//****************************************************************
function fn_abreAsignarCheque() {
    //Inicio IBK - AAE
    //parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Asignar Cheque", "GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleListadoAsigCheque.aspx", 550, 220, function() { });
    var strLote = $("#txtLote").val();
    var id = $("#hddRowId").val();
    if (IsNullOrEmpty(id)) {
        strLote = $("#txtLote").val();
    } else {
        strLote = strNroLote;
    };
    parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Asignar Cheque", "GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleListadoAsigCheque.aspx?NroLote=" + strLote, 550, 220, function() { });
}   


//****************************************************************
// Funcion		:: 	fn_abreLiquidar
// Descripción	::	Abre Modal para Liquidar
// Log			:: 	JRC - 02/11/2012
//****************************************************************
function fn_abreLiquidar() {
    parent.fn_util_AbreModal("Impuesto y Multa Municipal :: Liquidar", "GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleLiquidar.aspx", 1200, 550, function() { });
}


//****************************************************************
// Funcion		:: 	fn_limpiar
// Descripción	::	Limpiar
// Log			:: 	JRC - 24/11/2012
//****************************************************************
function fn_limpiar() {

    $("#cmbDepartamento").val("0");
    $("#cmbProvincia").html(strComboVacio);
    $("#cmbDistrito").html(strComboVacio);
    $("#txtNroContrato").val("");
    $("#txtRazonSocial").val("");
    $("#cmdTipoDoc").val("0");
    $("#txtNroDocumento").val("");
    $("#txtPeriodo").val("");
    $("#txtLote").val("");
    $("#cmdEstadoPago").val("0");
    $("#cmdEstadoCobro").val("0");

}
//Inicio IBK - AAE
//****************************************************************
// Funcion		:: 	fn_editarLoteParam
// Descripción	::	Envio a editar lote
// Log			:: 	AAE - 13/02/2012
//****************************************************************
function fn_editarLoteParam() {

    parent.fn_blockUI();

    var arrParametros = ["pNroLote", $("#txtLote").val()];
   
    fn_util_AjaxWM("frmImpuestoMultaInmuebleListado.aspx/CheckLote",
                   arrParametros,
                   function(resultado) {
                       parent.fn_unBlockUI();
                       if (resultado == "-1") {
                           parent.fn_mdl_mensajeError("Debe seleccionar un Impuesto", function() { }, "VALIDACIÓN");
                       } else {
                           strNroLote = resultado;
                           fn_util_globalRedirect("/GestionBien/ImpuestoMultasInmueble/frmImpuestoMultaInmuebleRegistro.aspx?NroLote=" + strNroLote);
                       }
                   },
                   function(resultado) {
                       parent.fn_unBlockUI();
                       parent.fn_mdl_mensajeError("Debe seleccionar un Impuesto", function() { }, "VALIDACIÓN");
                   }
    );
}
//Inicio JJM IBK
function fn_obtenerMuncipalidad(pClaveId, pValor1) {
    $('#txtCodMunicipalidad').val(fn_util_trim(pClaveId));
    $('#txtMunicipalidad').val(fn_util_trim(pValor1));
    //Resize Pantalla
    fn_doResize();
}
//Fin JJM IBK