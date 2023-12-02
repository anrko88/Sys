var CrLf = 1;

var Departamento = new Object();
Departamento.Lima = '15';

// Longitud predefinida de los tipos de documento.
var strTipoDocumentoDNI = "1";
var strTipoDocumentoRUC = "2";
var strTipoDocumentoCarnetEx = "3";
var strTipoDocumentoPasaporte = "5";
var strTipoDocumentoOtroDoc = "6";


//****************************************************************
// Función		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	EBL - 10/02/2012
//****************************************************************
$(document).ready(function() {

    // Establece la configuración inicial de los controles, incluyendo las grillas.
    fn_InicializarControles();
    
    // Valida Campos.
    fn_inicializarCampos();

    // Asocia los eventos a los controles.
    fn_InicializarEventos();
    
    // On load Page (siempre al final)
    fn_onLoadPage();
});

//****************************************************************
// Función		:: 	fn_InicializarControles
// Descripción	::	Establece la configuración inicial de los controles, incluyendo las grillas.
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_InicializarControles() {
    $("#hddCambiosSinGuardar").val('0');

    $("#cmbTipoDocumento option[value='2']").remove();
    $('#txtNroDocumento').attr('disabled', 'disabled');
    
    fn_ConfigurarGrillaRepresentantes();
}

//****************************************************************
// Función		:: 	fn_InicializarEventos
// Descripción	::	Asocia los eventos a los controles respectivos
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_InicializarEventos() {
    // Detectar algún cambio
    $('#txtNroDocumento, #txtNombreRepresentante, #txtPartidaRegistral, #txtOficinaRegistral, #cmbDepartamento, #cmbProvincia, #cmbDistrito').change(function() {
        $("#hddCambiosSinGuardar").val("1");
    });

    // Valida el ingreso de datos en tipo documento.
    $('#cmbTipoDocumento').change(function() {
        var strValor = $(this).val();
        $("#txtNroDocumento").val("");
        $('#txtNroDocumento').unbind('keypress');

        // Establece la longitud válida del número de documento de acuerdo al tipo seleccionado.
        if (fn_util_trim(strValor) == strTipoDocumentoDNI) {
            $('#txtNroDocumento').attr('disabled', false);
            $('#txtNroDocumento').validText({ type: 'number', length: 8 });
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
}

//****************************************************************
// Función		:: 	fn_HabilitarControlesRepresentantes
// Descripción	::	Inicializa los controles para poder agregar o editar un nuevo representante.
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_HabilitarControlesRepresentantes() {
    $("#cmbDepartamento option:first").attr('selected', 'selected');
    $("#cmbProvincia option:first").attr('selected', 'selected');
    fn_LimpiaComboDistritOProvincia("#cmbProvincia");
    $("#cmbDistrito option:first").attr('selected', 'selected');
    fn_LimpiaComboDistritOProvincia("#cmbDistrito");

    $("#cmbTipoDocumento option:first").attr('selected', 'selected');
    
    $("#txtNroDocumento").val("");
    $("#txtNombreRepresentante").val("");
    $("#txtPartidaRegistral").val("");
    $("#txtOficinaRegistral").val("");

    fn_seteaCamposObligatorios();
}

//****************************************************************
// Función		:: 	fn_seteaCamposObligatorios
// Descripción	::	Estrablece que controles son obligatorios al agregar o editar un nuevo representante.
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_seteaCamposObligatorios() {
    fn_util_SeteaObligatorio($("#txtNroDocumento"), "input");
    fn_util_SeteaObligatorio($("#txtNombreRepresentante"), "input");
    fn_util_SeteaObligatorio($("#txtPartidaRegistral"), "input");
    fn_util_SeteaObligatorio($("#txtOficinaRegistral"), "input");

    fn_util_SeteaObligatorio($("#cmbDepartamento"), "select");
    fn_util_SeteaObligatorio($("#cmbTipoDocumento"), "select");
}
    
//************************************************************
// Función		:: 	fn_agregaRepresentante
// Descripción 	::  Habilita los controles para que el usuario pueda agregar un nuevo bien inmueble, 
//                  asociado al proveedor seleccionado.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_agregaRepresentante() {
    fn_HabilitarControlesRepresentantes(true);
    
    $("#hdnCodigoRepresentante").val("");
    $("#jqGrid_lista_AA").jqGrid('resetSelection');

    $("#dv_AccionesRepresentante").hide();
    $("#dv_ProcesoRepresentante").show();
}

//************************************************************
// Función		:: 	fn_LeerRepresentante
// Descripcion 	::  Le permite leer los datos del represente.
// Log			:: 	EBL - 10/02/2012
//************************************************************  
function fn_LeerRepresentante(id) {
    var rowData = $("#jqGrid_lista_AA").jqGrid('getRowData', id);

    $("#hdnCodigoRepresentante").val(rowData.CodigoRepresentante);
    $("#cmbTipoDocumento").val(fn_util_trim(rowData.CodigoTipoDocumento));
    $('#cmbTipoDocumento').change();
    $("#txtNroDocumento").val(rowData.NroDocumento);
    $("#txtNombreRepresentante").val(rowData.NombreRepresentante);
    $("#txtPartidaRegistral").val(rowData.PartidaRegistral);
    $("#txtOficinaRegistral").val(rowData.OficinaRegistral);

    $("#cmbDepartamento").val(rowData.CodUbigeo.substring(0,2));
    fn_cargaComboProvincia("#cmbProvincia", "#cmbDistrito", rowData.CodUbigeo.substring(0, 2));
    $("#cmbProvincia").val(rowData.CodUbigeo.substring(2, 4));
    fn_cargaComboDistrito("#cmbDistrito", rowData.CodUbigeo.substring(0, 2), rowData.CodUbigeo.substring(2, 4));
    $("#cmbDistrito").val(rowData.CodUbigeo.substring(4, 6));
}

//************************************************************
// Función		:: 	fn_EditarRepresentante
// Descripción 	::  Le permite actualizar los datos del represente.
// Log			:: 	EBL - 10/02/2012
//************************************************************  
function fn_EditarRepresentante() {
    var vElementosAEditar = $("#jqGrid_lista_AA").getGridParam('selarrrow');

    if (vElementosAEditar.length === 0) {
        parent.fn_mdl_mensajeIco("Seleccione un representante para poder modificarlo.", "util/images/warning.gif", "EDITAR REPRESENTANTE");
    } else {
        
        if (vElementosAEditar.length > 1) {
            parent.fn_mdl_mensajeIco("Seleccione un sólo representante.", "util/images/warning.gif", "EDITAR REPRESENTANTE");
        } else {
            var id = $("#hdnCodigoRepresentante").val();
            
            fn_HabilitarControlesRepresentantes(true);

            fn_LeerRepresentante(id);

            $("#dv_AccionesRepresentante").hide();
            $("#dv_ProcesoRepresentante").show();
        }
    }
}

//************************************************************
// Función		:: 	fn_eliminarRepresentante
// Descripción 	::  Le permite eliminar uno o más representantes, solicitandole una confirmación del usuario.
// Log			:: 	EBL - 10/02/2012
//************************************************************      
function fn_eliminarRepresentante() {
    if (fn_EsEliminacionValida()) {
        parent.fn_mdl_confirma(
		    "¿Está seguro de eliminar el/los representantes seleccionado(s)?"
		    , fn_Representante_eliminarWM
		    , "util/images/warning.gif"
		    , function() { }
		    , "ELIMINACIÓN DE REPRESENTANTE"
	    );
    }
}

//************************************************************
// Función		:: 	fn_EsEliminacionValida
// Descripción 	::  Verifica si la operación de eliminación es válida.
// Log			:: 	EBL - 06/08/2012
//************************************************************
function fn_EsEliminacionValida() {
    var vElementosAEliminar;

    vElementosAEliminar = $("#jqGrid_lista_AA").getGridParam('selarrrow');
    if (vElementosAEliminar.length == 0) {
        parent.fn_mdl_mensajeIco("Seleccione al menos un representante para eliminar.", "util/images/warning.gif", "ELIMINAR REPRESENTANTE");
        return false;
    } else {
        return fn_Representante_AsociadoAOtroContrato($("#hdnCodigoContrato").val());
    }
}

//************************************************************
// Función		:: 	fn_Representante_AsociadoAOtroContrato
// Descripción 	::  Verifica si la operación de eliminación es válida.
// Log			:: 	EBL - 06/08/2012
//************************************************************
function fn_Representante_AsociadoAOtroContrato(codigoContrato) {
    var bResult;
    var id = $("#hdnCodigoRepresentante").val();
    var rowData = $("#jqGrid_lista_AA").jqGrid('getRowData', id);

    $("#hdnCodigoRepresentante").val(rowData.CodigoRepresentante);
    
    var paramArray = [
                         "pCodigoContrato", codigoContrato,
                         "pCodigoTipoRepresentante", $("#hddCodigoTipoRepresentante").val(),
                         "pCodigoRepresentante", rowData.CodigoRepresentante
                     ];

    fn_util_AjaxSyncWM("frmRepresentanteRegistro.aspx/RepresentanteAsociadoAOtroContrato",
                       paramArray,
                       function(result) {
                           bResult = result;
                       },
                       function(result) {
                           parent.fn_mdl_mensajeIco(result.responseText, "util/images/warning.gif", "AGREGAR REPRESENTANTE DEL CLIENTE");
                           bResult = false;
                       });

     return bResult;
}

//****************************************************************
// Función		:: 	fn_Representante_eliminarWM
// Descripción	::	Elimina un registro por web method, si obtiene respuesta llama al método ResultadoEliminar.
//                  Si ocurre una excepción, muestra el mensaje describiendo la excepción.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function fn_Representante_eliminarWM() {
    parent.fn_blockUI();
    
    var vElementosAEliminar;
    var sResult = "";

    vElementosAEliminar = $("#jqGrid_lista_AA").getGridParam('selarrrow');

    for (var i = 0; i < vElementosAEliminar.length; i++) {
        sResult = sResult + vElementosAEliminar[i] + "|";
    }
    // Eliminar el último palito
    sResult = sResult.slice(0, -1);

    var paramArray = [
                        "pRepresentantesEliminar", sResult
                     ];
    fn_util_AjaxWM("frmRepresentanteRegistro.aspx/RepresentantesEliminar",
                   paramArray,
                   ResultadoEliminarRepresentantes,
                   function(result) {
                       parent.fn_mdl_mensajeIco(result.responseText, "util/images/warning.gif", "ELIMINACIÓN DE REPRESENTANTE");
                   });

   parent.fn_unBlockUI();
}

//****************************************************************
// Función		:: 	ResultadoEliminarRepresentantes
// Descripción	::	Le muestra un mensaje de éxito en la eliminación, caso contrario le describe el error.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function ResultadoEliminarRepresentantes(result) {
    if (result == "0") {
        parent.fn_mdl_mensajeIco("Se eliminó con éxito el/los registro(s).", "util/images/ok.gif", "ELIMINACIÓN DE REPRESENTANTE");

        fn_ConfigurarGrillaRepresentantes();
        fn_ListaRepresentantes();
    } else {
        parent.fn_mdl_mensajeIco("Ocurrió un error al eliminar el/los registro(s).", "util/images/warning.gif", "ELIMINACIÓN DE REPRESENTANTE");
    }
}

//************************************************************
// Función		:: 	fn_GuardarRepresentante
// Descripción 	:: 	Guarda los datos del representante, previa validación de los datos
// Log			:: 	EBL - 09/05/2012
//************************************************************
function fn_GuardarRepresentante() {
    parent.fn_blockUI();

    if (EsValidoRepresentante()) {
        // Si es una operación de agregar (no tiene número secuencial)
        if ($("#hdnCodigoRepresentante").val() != "") {
            // Si es una operación de edición
            fn_GuardarRepresentanteEditar();
            $("#dv_AccionesRepresentante").show();
            $("#dv_ProcesoRepresentante").hide();

            fn_inicializarCampos();
            $("#jqGrid_lista_AA").trigger("reloadGrid");
        }
    }

    parent.fn_unBlockUI();
}

//****************************************************************
// Función		:: 	fn_GuardarRepresentanteEditar
// Descripción	::	Guarda los datos ingresados de un representante existente (operación editar).
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function fn_GuardarRepresentanteEditar() {
    parent.fn_blockUI();

    if (EsValidoRepresentante() && !fn_ExisteDniOtroRep()) {
        var arrParametros = [
                             "strCodigoRepresentante", $("#hdnCodigoRepresentante").val(),
                             "strCodigoTipoRepresentante", $("#hddCodigoTipoRepresentante").val(),
                             "strNroDocumento", $("#txtNroDocumento").val(),
                             "strNombreRepresentante", $("#txtNombreRepresentante").val(),
                             "strPartidaRegistral", $("#txtPartidaRegistral").val(),
                             "strOficinaRegistra", $("#txtOficinaRegistral").val(),
                             "strDepartamento", $("#cmbDepartamento").val(),
                             "strProvincia", $("#cmbProvincia").val(),
                             "strDistrito", $("#cmbDistrito").val(),
                             "strCodigoTipoDeDocumento", $("#cmbTipoDocumento").val()
                            ];

        fn_util_AjaxWM("frmRepresentanteRegistro.aspx/GuardarRepresentanteEditar",
                       arrParametros,
                       function() {
                           fn_ListaRepresentantes();
                           fn_CancelarRepresentante();
                       },
                       function(result) {
                           fn_ListaRepresentantes();
                           fn_CancelarRepresentante();
                           parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                       });
   }
}

//************************************************************
// Función		:: 	fn_CancelarRepresentante
// Descripción 	:: 	Cancela la operación de agregar o editar y configura
//                  los controles a su estado original
// Log			:: 	EBL - 09/05/2012
//************************************************************
function fn_CancelarRepresentante() {
    fn_HabilitarControlesRepresentantes(false);

    $("#dv_AccionesRepresentante").show();
    $("#dv_ProcesoRepresentante").hide();
    
    $("#hddCambiosSinGuardar").val("0");

    fn_seteaCamposObligatorios();

    $("#jqGrid_lista_AA").jqGrid('resetSelection');
}

//****************************************************************
// Función		:: 	fn_GuardarRepresentanteNuevo
// Descripción	::	Guarda los datos ingresados de un nuevo representante (operación nuevo).
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function fn_GuardarRepresentanteNuevo() {
    parent.fn_blockUI();
    
    if (EsValidoRepresentante() && !fn_ExisteDni()) {
    
        var arrParametros = [
                             "pCodigoContrato",             $("#hdnCodigoContrato").val(),
                             "strCodigoTipoRepresentante",  $("#hddCodigoTipoRepresentante").val(),
                             "strNroDocumento",             $("#txtNroDocumento").val(),
                             "strNombreRepresentante",      $("#txtNombreRepresentante").val(),
                             "strPartidaRegistral",         $("#txtPartidaRegistral").val(),
                             "strOficinaRegistral",         $("#txtOficinaRegistral").val(),
                             "strDepartamento",             $("#cmbDepartamento").val(),
                             "strProvincia",                $("#cmbProvincia").val(),
                             "strDistrito",                 $("#cmbDistrito").val(),
                             "strCodUnico",                 $("#hddCodUnico").val(),
                             "strCodigoTipoDeDocumento",    $("#cmbTipoDocumento").val()
                             ];

        fn_util_AjaxWM("frmRepresentanteRegistro.aspx/GuardarRepresentanteNuevo",
                       arrParametros,
                       function() {
                           fn_CancelarRepresentante();
                           $("#jqGrid_lista_AA").trigger("reloadGrid");
                           var ctrlBtn = window.parent.frames[0].document.getElementById('cmdGuardarRepresentante');
                           ctrlBtn.click();
                           
                           parent.fn_mdl_mensajeIco("Se agregó el representante al contrato y a la lista de representantes del cliente", "util/images/ok.gif", "AGREGAR REPRESENTANTE");
                           
                       },
                       function(result) {
                           parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                       });
   }
    
   parent.fn_unBlockUI();
}

//************************************************************
// Función		:: 	EsValidoRepresentante
// Descripción 	:: 	Verifica si los datos ingresados son válidos, para una operación de editar o agregar.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function EsValidoRepresentante() {
    // Cadena para el mensaje de error, en caso hubiese alguno.
    var strError = new StringBuilderEx();

    /****** VALIDACIONES DE TEXTO */
    var cmbDepartamento = $('select[id=cmbDepartamento]');
    var cmbTipoDocumento = $('select[id=cmbTipoDocumento]');
    var txtNombreRepresentante = $('input[id=txtNombreRepresentante]:text');
    var txtPartidaRegistral = $('input[id=txtPartidaRegistral]:text');
    var txtOficinaRegistral = $('input[id=txtOficinaRegistral]:text');

    strError.append(fn_util_ValidateControl(cmbDepartamento[0], 'el departamento.', CrLf, ''));
    strError.append(fn_util_ValidateControl(cmbTipoDocumento[0], 'el tipo de documento.', CrLf, ''));
    strError.append(fn_util_ValidarNroDocumento());
    strError.append(fn_util_ValidateControl(txtNombreRepresentante[0], 'el nombre.', CrLf, ''));
    strError.append(fn_util_ValidateControl(txtPartidaRegistral[0], 'la partida registral.', CrLf, ''));
    strError.append(fn_util_ValidateControl(txtOficinaRegistral[0], 'la oficina registral.', CrLf, ''));

    if (strError.toString() != '') {
        fn_seteaCamposObligatorios();
        parent.fn_mdl_mensajeIco(strError.toString(), "util/images/warning.gif", "VALIDAR DATOS REPRESENTANTE");

        return false;
    } else {
        return true;
    }
}

//****************************************************************
// Funcion		:: 	fn_util_ValidarNroDocumento
// Descripción	::	Verifica si el número de documento existe, y si la longitud del mismo es válido
//                  de acuerdo al tipo seleccionado.
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_util_ValidarNroDocumento() {    
    var sError;
    var txtNroDocumento = $('input[id=txtNroDocumento]:text');
    
    sError = fn_util_ValidateControl(txtNroDocumento[0], 'el número de documento.', CrLf, '');

    if (sError.length === 0) {
        var strTipoDocumento = $("#cmbTipoDocumento").val();
        var strNroDocumento = $("#txtNroDocumento").val();
        var intNroDocumento = strNroDocumento.length;
        
        if (fn_util_trim(strTipoDocumento) == strTipoDocumentoDNI) {
            if (intNroDocumento < 8) sError = '  - Número de documento inválido <br />';
        } else if (fn_util_trim(strTipoDocumento) == strTipoDocumentoRUC) {
        if (intNroDocumento < 11) sError = '  - Número de documento inválido <br />';
        } else if (fn_util_trim(strTipoDocumento) == strTipoDocumentoCarnetEx) {
        if (intNroDocumento < 4) sError = '  - Número de documento inválido <br />';
        } else if (fn_util_trim(strTipoDocumento) == strTipoDocumentoPasaporte) {
        if (intNroDocumento < 7) sError = '  - Número de documento inválido <br />';
        } else {
            if (intNroDocumento < 4) sError = '  - Número de documento inválido <br />';
        }
    }

    return sError;
}

//****************************************************************
// Funcion		:: 	fn_inicializarCampos
// Descripción	::	Inicializa el formato de los campos.
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_inicializarCampos() {
    $('#txtNroDocumento').validText({ type: 'number', length: 8 });
    $('#txtNombreRepresentante').validText({ type: 'comment', length: 100 });

    $('#txtPartidaRegistral').validText({ type: 'number', length: 10 });
    $('#txtPartidaRegistral').maxLength(10);
    $('#txtOficinaRegistral').validText({ type: 'comment', length: 50 });
    
    $("#dv_ProcesoRepresentante").hide();

    fn_seteaCamposObligatorios();
}

//****************************************************************
// Función		:: 	fn_ListaRepresentantes
// Descripción	::	Devuelve los representantes disponibles para le tipo de representante requerido.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ListaRepresentantes() {
    var arrParametros = [
                         "pPageSize",       fn_util_getJQGridParam("jqGrid_lista_AA", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage",    fn_util_getJQGridParam("jqGrid_lista_AA", "page"), // Página actual
                         "pSortColumn",     fn_util_getJQGridParam("jqGrid_lista_AA", "sortname"), // Columna a ordenar
                         "pSortOrder",      fn_util_getJQGridParam("jqGrid_lista_AA", "sortorder"), // Criterio de ordenación
                         "pCodigoContrato", $("#hdnCodigoContrato").val(),
                         "pCodUnico",       $("#hddCodUnico").val()
                        ];

    fn_util_AjaxWM("frmRepresentanteRegistro.aspx/ListarRepresentantes",
                   arrParametros,
                   function(jsondata) {
                       jqGrid_lista_AA.addJSONData(jsondata);
                   },
                   function(request) {
                       parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "REPRESENTANTES DEL CLIENTE");
                   }
                   );
}

//****************************************************************
// Función		:: 	fn_ListaRepresentantes
// Descripción	::	Devuelve los representantes disponibles para le tipo de representante requerido.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ConfigurarGrillaRepresentantes() {
    $("#jqGrid_lista_AA").jqGrid({
        datatype: function() {
            fn_ListaRepresentantes();
        },
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodigoRepresentante" // Índice de la columna con la clave primaria.
        },
        colNames: ['', 'Nombre', '', 'Tipo de Documento', 'Nro. Documento', '', '', ''],
        colModel: [
		            { name: 'CodigoRepresentante', index: 'CodigoRepresentante', hidden: true },
		            { name: 'NombreRepresentante', index: 'NombreRepresentante', width: 40, align: "left" },
		            { name: 'CodigoTipoDocumento', index: 'CodigoTipoDocumento', hidden: true },
		            { name: 'TipoDocumento', index: 'CodigoTipoDocumento', width: 20, sorttype: "string", align: "center" },
		            { name: 'NroDocumento', index: 'TipoDocumento', width: 20, sorttype: "string", align: "center" },
		            { name: 'PartidaRegistral', index: 'PartidaRegistral', hidden: true },
		            { name: 'OficinaRegistral', index: 'OficinaRegistral', hidden: true },
		            { name: 'CodUbigeo', index: 'CodUbigeo', hidden: true }
	              ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 5, // Tamaño de la página
        rowList: [5],
        sortname: 'CodigoRepresentante',
        sortorder: 'desc',
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        altclass: 'gridAltClass',
        multiselect: true,
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_AA").jqGrid('getRowData', id);
            $("#hdnCodigoRepresentante").val(rowData.CodigoRepresentante);
        }
    });
    $("#jqGrid_lista_AA").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_AA").hide();
    $("#jqGrid_lista_AA").setGridWidth($(window).width() - 20);
    $("#tb_formulario").width($(window).width() - 15);
    $("#tb_formulario1").width($(window).width() - 15);
}

//****************************************************************
// Función		:: 	fn_SeleccionarRepresentantes
// Descripción	::	Selecciona los representantes y los agrega a la grilla de representantes del cliente.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_SeleccionarRepresentantes() {
    var vRepresentantesAAgregar;

    vRepresentantesAAgregar = $("#jqGrid_lista_AA").getGridParam('selarrrow');
    if (vRepresentantesAAgregar.length == 0) {
        parent.fn_mdl_mensajeIco("Seleccione el/los representantes para agregarlos.", "util/images/warning.gif", "SELECCIONAR REPRESENTANTE(S)");
    } else {
        fn_Representante_AgregarWM(vRepresentantesAAgregar);

        fn_ListaRepresentantesCerrar();
    }
}

//****************************************************************
// Función		:: 	fn_Representante_AgregarWM
// Descripción	::	Selecciona los representantes y los agrega a la grilla de representantes del cliente.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function fn_Representante_AgregarWM(vRepresentantesAAgregar) {
    var sResult = "";

    for (var i = 0; i < vRepresentantesAAgregar.length; i++) {
        sResult = sResult + vRepresentantesAAgregar[i] + "|";
    }
    // Eliminar el último palito
    sResult = sResult.slice(0, -1);

    var paramArray = [
                        "pCodigoContrato", $("#hdnCodigoContrato").val(),
                        "pCodigoTipoRepresentante", $("#hddCodigoTipoRepresentante").val(),
                        "pRepresentantesAAgregar", sResult
                     ];
    fn_util_AjaxWM("frmRepresentanteRegistro.aspx/RepresentantesClienteAgregarAContrato",
                   paramArray,
                   function() { },
                   function(result) {
                       parent.fn_mdl_mensajeIco(result.responseText, "util/images/warning.gif", "AGREGAR REPRESENTANTE AL CONTRATO");
                   });
}

//****************************************************************
// Función      ::      fn_ListaRepresentantesCerrar
// Descripción  ::      Cierra la actual ventana y actualiza la grilla de representantes de la ventana padre.
// Log          ::      EBL - 21/05/2012
//****************************************************************
function fn_ListaRepresentantesCerrar() {
    var ctrlBtn = window.parent.frames[0].document.getElementById('cmdGuardarRepresentante');
    ctrlBtn.click();

    parent.fn_util_CierraModal();
}

//****************************************************************
// Función      ::      fn_CierraModal
// Descripción  ::      Cierra la actual ventana, previa confirmación por parte del usuario.
// Log          ::      EBL - 21/05/2012
//****************************************************************
function fn_CierraModal() {
    var sMensaje;
    
    if ($("#hddCambiosSinGuardar").val() == "0") {
        sMensaje = "¿Está seguro de cerrar Representantes?";
    } else {
        sMensaje = "¿Está seguro de cerrar Representantes?<br />No se guardará la información ingresada.";
    }

    parent.fn_mdl_confirma(sMensaje
		                    , fn_Cerrar
		                    , "util/images/question.gif"
		                    , function() { }
		                    , "REPRESENTANTES"
	                       );
}

//****************************************************************
// Función      ::      fn_Cerrar
// Descripción  ::      Cierra la actual ventana.
// Log          ::      EBL - 21/05/2012
//****************************************************************
function fn_Cerrar() {
    parent.fn_util_CierraModal();
}

//****************************************************************
// Función      ::      fn_ExisteDni
// Descripción  ::      Verifica si el Dni ya existe asignado para algún representante del cliente, cuando se agrega uno nuevo.
// Log          ::      EBL - 21/05/2012
//****************************************************************
function fn_ExisteDni() {
    var bResult;
    var paramArray = [
                         "pCodigoContrato", $("#hdnCodigoContrato").val(),
                         "pCodUnico", $("#hddCodUnico").val(),
                         "pCodigoTipoDocumento", $("#cmbTipoDocumento").val(),
                         "NroDocumento", $("#txtNroDocumento").val()
                     ];

    fn_util_AjaxSyncWM("frmRepresentanteRegistro.aspx/ExisteDni",
                       paramArray,
                       function(result) {
                            if (result == "0") {
                                parent.fn_mdl_mensajeIco("Ya existe un represente del cliente con el mismo documento de identidad.<br />Por favor ingrese otro.", "util/images/warning.gif", "AGREGAR REPRESENTANTE AL CONTRATO");
                                bResult = true;
                            } else {
                                bResult = false;
                            }
                       },
                       function(result) {
                           parent.fn_mdl_mensajeIco(result.responseText, "util/images/warning.gif", "AGREGAR REPRESENTANTE AL CONTRATO");
                           bResult = true;
                       });

   return bResult;
}

//****************************************************************
// Función      ::      fn_ExisteDniOtroRep
// Descripción  ::      Verifica si el Dni ya existe asignado para algún representante del cliente, 
//                      cuando se edita el representante.
// Log          ::      EBL - 21/05/2012
//****************************************************************
function fn_ExisteDniOtroRep() {
    var bResult;
    var paramArray = [
                       "pCodigoRepresentante", $("#hdnCodigoRepresentante").val(),
                       "pCodigoContrato", $("#hdnCodigoContrato").val(),
                       "pCodUnico", $("#hddCodUnico").val(),
                       "pCodigoTipoDocumento", $("#cmbTipoDocumento").val(),
                       "pNroDocumento", $("#txtNroDocumento").val()
                     ];

    fn_util_AjaxSyncWM("frmRepresentanteRegistro.aspx/ExisteDniOtroRep",
                       paramArray,
                       function(result) {
                           if (result == "0") {
                               parent.fn_mdl_mensajeIco("Ya existe un represente del cliente con el mismo documento de identidad.<br />Por favor ingrese otro.", "util/images/warning.gif", "EDITAR REPRESENTANTE DEL CLIENTE");
                               bResult = true;
                           } else {
                               bResult = false;
                           }
                       },
                       function(result) {
                           parent.fn_mdl_mensajeIco(result.responseText, "util/images/warning.gif", "EDITAR REPRESENTANTE DEL CLIENTE");
                           bResult = false;
                       });

    return bResult;
}

//****************************************************************
// Función      ::      fn_AsignarOficinaRegistral
// Descripción  ::      Lee el departamento y asigna el nombre como oficina registral predeterminada.
// Log          ::      EBL - 21/05/2012
//****************************************************************
function fn_AsignarOficinaRegistral() {
    if ($("#cmbDepartamento").val() != "0") {
        if ($("#cmbDepartamento").val() != Departamento.Lima) {
            $("#txtOficinaRegistral").val($("#cmbDepartamento").find("option:selected").text());
        } else {
            $("#txtOficinaRegistral").val($("#cmbDepartamento").find("option:selected").text()+" Y CALLAO");
        }
    } else {
        $("#txtOficinaRegistral").val("");
    }
}