//****************************************************************
// Variables Globales
//****************************************************************
var strComboVacio = "<option value='0'>[-Seleccione-]</option>"

var strTipoDocumentoDNI = "1";
var strTipoDocumentoRUC = "2";
var strTipoDocumentoCarnetEx = "3";
var strTipoDocumentoPasaporte = "5";
var strTipoDocumentoOtroDoc = "6";

var C_TX_NUEVO = "NUEVO"
var C_TX_EDITAR = "EDITAR"

var strRazSocial = "";
var strTipoDoc = "";
var strNroDoc = "";
var strDireccion = "";
var strDepartamento = "";
var strProvincia = "";
var strDistrito = "";

var strCodUnico = "";
var strCodigoTipoCuenta = "";
var strCodigoMoneda = "";
var strCuenta = "";

var strErrorCuenta = "";

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 04/01/2013
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();
        	    
	//Carga Grilla
    fn_cargaGrilla();
    
    //Botones
    $("#sp_editar").hide();
        
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
        
    
    //---------------------------------
    //Consulta Datos RM
    //---------------------------------
    $('#imgBsqClienteRM').click(function() {
		ConsultaRM();
    });
        
        
    //On load Page (siempre al final)
    fn_onLoadPage();
    
});




//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 04/01/2013
//****************************************************************
function fn_inicializaCampos() {		
	$('#txtNroDocumento').validText({ type: 'number', length: 11 });
	$('#txtRazSocial').validText({ type: 'comment', length: 100 });	
	$('#txtDireccion').validText({ type: 'comment', length: 100 });	
	
	$('#txtNumeroCuenta').validText({ type: 'number', length: 13 });
}


//****************************************************************
// Funcion		:: 	fn_cargaComboProvincia
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_cargaComboProvincia(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrDepartamento", strCodigoDepartamento, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbProvincia').html(arrResultado[1]);
            $('#cmbProvincia').val(strCodigoProvincia);            
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
function fn_cargaComboDistrito(strCodigoDepartamento, strCodigoProvincia, strCodigoDistrito) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbDistrito').html(arrResultado[1]);
            $('#cmbDistrito').val(strCodigoDistrito);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
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
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla Listado de Cotizaciones
// Log			:: 	JRC - 04/01/2013
//****************************************************************
function fn_cargaGrilla() {

     $("#jqGrid_lista_A").jqGrid({
        datatype: function() {    
			intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");	      	
            fn_listaCesionarios();
        },
        jsonReader:
        {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['','','','','','','','','','','','Item','Denominación o razón social','Tipo Doc.','Nº Doc.','Departamento','Provincia','Distrito','Cant. Representantes'],
        colModel: [		
				{ name: 'CodCesionario',		index: 'CodCesionario',			hidden: true},		
				{ name: 'CodigoTipoDocumento',	index: 'CodigoTipoDocumento',	hidden: true},
				{ name: 'CodDepartamento',		index: 'CodDepartamento',		hidden: true},
				{ name: 'CodProvincia',			index: 'CodProvincia',			hidden: true},
				{ name: 'CodDistrito',			index: 'CodDistrito',			hidden: true},
				{ name: 'Direccion',			index: 'Direccion',				hidden: true},
				{ name: 'CodSolicitudCredito',	index: 'CodSolicitudCredito',	hidden: true},
				{ name: 'CodUnico',				index: 'CodUnico',				hidden: true},
				{ name: 'CodigoTipoCuenta',		index: 'CodigoTipoCuenta',		hidden: true},
				{ name: 'CodigoMoneda',			index: 'CodigoMoneda',			hidden: true},
				{ name: 'Cuenta',				index: 'Cuenta',				hidden: true},
                { name: 'Id',					index: 'Id',					sortable: true,  sorttype: "string",	 align: "center", defaultValue: "" },				                
                { name: 'RazonSocial',			index: 'RazonSocial',			sortable: true,  sorttype: "string",	 align: "left", defaultValue: "" },
                { name: 'NombreTipoDocumento',	index: 'NombreTipoDocumento',	sortable: true,  sorttype: "string", align: "center",   defaultValue: "" },
                { name: 'NroDocumento',			index: 'NroDocumento',			sortable: true,  sorttype: "string", align: "left",   defaultValue: "" },
                { name: 'NomDepartamento',		index: 'NomDepartamento',		sortable: true,  sorttype: "string", align: "center", defaultValue: ""},
                { name: 'NomProvincia',			index: 'NomProvincia',			sortable: true,  sorttype: "string", align: "center", defaultValue: "" },
                { name: 'NomDistrito',			index: 'NomDistrito',			sortable: true,  sorttype: "string", align: "center",   defaultValue: ""},                
                { name: 'CantRepresentantes',	index: 'CantRepresentantes',	sortable: false, sorttype: "string", align: "center"}
        ],
        width: glb_intWidthPantalla-40,
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 5,
        rowList: [5, 10, 20, 30],
        sortname: 'CodSolicitudCredito',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodCesionario").val(rowData.CodCesionario);
 
			strRazSocial = fn_util_trim(rowData.RazonSocial);
			strTipoDoc = fn_util_trim(rowData.CodigoTipoDocumento);
			strNroDoc = fn_util_trim(rowData.NroDocumento);
			strDireccion = fn_util_trim(rowData.Direccion);
			strDepartamento = fn_util_trim(rowData.CodDepartamento);
			strProvincia = fn_util_trim(rowData.CodProvincia);
			strDistrito = fn_util_trim(rowData.CodDistrito);
			
			strCodUnico = fn_util_trim(rowData.CodUnico);
			strCodigoTipoCuenta = fn_util_trim(rowData.CodigoTipoCuenta);
			strCodigoMoneda = fn_util_trim(rowData.CodigoMoneda);
			strCuenta = fn_util_trim(rowData.Cuenta);
			
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

}




//****************************************************************
// Funcion		:: 	fn_listaCesionarios
// Descripción	::	Busca listado de Cesiones por parametros
// Log			:: 	JRC - 04/01/2013
//****************************************************************
function fn_listaCesionarios() {

    try {
        parent.fn_blockUI();

        var hddCodContrato = $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación                             
                             "pstrNroContrato", hddCodContrato
                            ];
				
        fn_util_AjaxWM("frmCesionarioRegistro.aspx/ListaCesionarios",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_A.addJSONData(jsondata);
                    parent.fn_unBlockUI();
                    fn_doResize();
                },
                function(request) {
                    parent.fn_unBlockUI();
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

    } catch (ex) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }

}



//****************************************************************
// Funcion		:: 	fn_agregarCesionario
// Descripción	::	Abre Editar Registro
// Log			:: 	JRC - 02/11/2013
//****************************************************************
function fn_agregarCesionario() {	
	parent.fn_blockUI();

    //String Validación
    var strError = new StringBuilderEx();

    //Instancia Validaciones    		
    //hddCodCesionario
	var objtxtRazSocial = $('input[id=txtRazSocial]:text');
	var objtxtNroDocumento = $('input[id=txtNroDocumento]:text');
	var objtxtDireccion = $('input[id=txtDireccion]:text');	
	var objcmdTipoDoc = $('select[id=cmdTipoDoc]');
	var objcmbDepartamento = $('select[id=cmbDepartamento]');
	var objcmbProvincia = $('select[id=cmbProvincia]');
	var objcmbDistrito = $('select[id=cmbDistrito]');
	
    //Valida
	strError.append(fn_util_ValidateControl(objtxtRazSocial[0], 'una Razón Social válida', 1, ''));
	strError.append(fn_util_ValidateControl(objcmdTipoDoc[0], 'un Tipo de documento válido', 1, ''));
	strError.append(fn_util_ValidateControl(objtxtNroDocumento[0], 'un Nro. de documento válido', 1, ''));
	strError.append(fn_util_ValidateControl(objtxtDireccion[0], 'una Dirección válida', 1, ''));	
	strError.append(fn_util_ValidateControl(objcmbDepartamento[0], 'un Departamento válido', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbProvincia[0], 'una Provincia válida', 1, ''));
	//strError.append(fn_util_ValidateControl(objcmbDistrito[0], 'un Distrito válido', 1, ''));
		
	var strTipoDocumento = $("#cmdTipoDoc").val();
    var strNroDocumento = $("#txtNroDocumento").val();
	var intNroDocumento = strNroDocumento.length;
	if (fn_util_trim(strTipoDocumento) == strTipoDocumentoDNI) {
		if (intNroDocumento < 8) strError.append('&nbsp;&nbsp;- Número de Documento Inválido <br />');
	} else if (fn_util_trim(strTipoDocumento) == strTipoDocumentoRUC) {
		if (intNroDocumento < 11) strError.append('&nbsp;&nbsp;- Número de Documento Inválido <br />');
	} else if (fn_util_trim(strTipoDocumento) == strTipoDocumentoCarnetEx) {
		if (intNroDocumento < 4) strError.append('&nbsp;&nbsp;- Número de Documento Inválido <br />');
	} else if (fn_util_trim(strTipoDocumento) == strTipoDocumentoPasaporte) {
		if (intNroDocumento < 7) strError.append('&nbsp;&nbsp;- Número de Documento Inválido <br />');
	} else {
		if (intNroDocumento < 4) strError.append('&nbsp;&nbsp;- Número de Documento Inválido <br />');
	}	//Valida error existente

	
	//Valida Cuenta
	var strValidaCuenta = fn_validaCuenta();
	if(strValidaCuenta!=""){
		strError.append(strValidaCuenta);
	}	

	if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }else{
    


		var arrParametros = [     
							"pstrTipoTx",		$("#hddTipoTx").val(),
							"pstrCodContrato",	$("#hddCodContrato").val(),
							"pstrCodCesionario",$("#hddCodCesionario").val(),
							"pstrRazSocial",	$("#txtRazSocial").val(),
							"pstrNroDocumento", $("#txtNroDocumento").val(),
							"pstrDireccion",	$("#txtDireccion").val(),
							"pstrTipoDoc",		$("#cmdTipoDoc").val(),
							"pstrDepartamento", $("#cmbDepartamento").val(),
							"pstrProvincia",	$("#cmbProvincia").val(),
							"pstrDistrito",		$("#cmbDistrito").val(),
							"txtCodUnico",		$("#txtCodUnico").val(),
							"cmbTipoCuenta",	$("#cmbTipoCuenta").val(),
							"cmbMoneda",		$("#cmbMoneda").val(),
							"txtNumeroCuenta",	$("#txtNumeroCuenta").val()
							];
          
        fn_util_AjaxWM("frmCesionarioRegistro.aspx/GrabaCesionario",
                 arrParametros,
                 function(result) {
                     parent.fn_unBlockUI();
                     
                     var strCodigos = fn_util_trim(result);
					 var arrCodigos = strCodigos.split("|");
                     
                     if (fn_util_trim(arrCodigos[0]) == "0") {
                         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL GUARDAR");
                     } else {                         
                         if ($("#hddTipoTx").val() == C_TX_NUEVO) {
							 $("#hddCodCesionario").val(fn_util_trim(arrCodigos[1]));
							 fn_RedireccionGrabar("I",fn_util_trim(arrCodigos[1]));
                             //parent.fn_mdl_mensajeOk("Se grabó el Cesionario correctamente.", function() { fn_RedireccionGrabar("I",fn_util_trim(arrCodigos[1])) }, "GRABADO CORRECTO");
                         } else {
							fn_RedireccionGrabar("U",0);
                             //parent.fn_mdl_mensajeOk("Se actualizó correctamente los datos del Cesionario", function() { fn_RedireccionGrabar("U",0) }, "GRABADO CORRECTO");
                         }
                         
                     }
                 },
                 function(resultado) {
                     parent.fn_unBlockUI();
                     var error = eval("(" + resultado.responseText + ")");
                     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABADO");
                 }
        );		
		
    
    }

	
}
function fn_RedireccionGrabar(strTipo,strCodCesionario){	
	fn_listaCesionarios();
	fn_limpiar();
	fn_cancelaCesionario()
	if(fn_util_trim(strTipo)=="I"){
		parent.fn_util_AbreModal2("Cesión de Contrato :: Registro Representante", "Formalizacion/CesionContrato/frmRepresentanteRegistro.aspx?hddCodContrato=" + $("#hddCodContrato").val() + "&hddCodCesionario=" + strCodCesionario, 850, 550, function() { });	
	}
}




//****************************************************************
// Funcion		:: 	fn_limpiaCesionario
// Descripción	::	Limpiar Cesionario
// Log			:: 	JRC - 09/11/2013
//****************************************************************
function fn_limpiar() {	
	
	$("#hddCodCesionario").val("");
	$("#txtRazSocial").val("");
	$("#txtNroDocumento").val("");
	$("#txtDireccion").val("");
	$("#cmdTipoDoc").val(0);
	$("#cmbDepartamento").val(0);
	
	$("#cmbTipoCuenta").val(0);
	$("#cmbMoneda").val(0);
	$("#txtCodUnico").val("");
	$("#txtNumeroCuenta").val("");
	
	$("#cmbProvincia").html(strComboVacio);
	$("#cmbDistrito").html(strComboVacio);
	
	strRazSocial = "";
	strTipoDoc = "";
	strNroDoc = "";
	strDireccion = "";
	strDepartamento = "";
	strProvincia = "";
	strDistrito = "";
	
	$("#txtRazSocial").attr('class', 'css_input');
	$("#txtNroDocumento").attr('class', 'css_input');
	$("#txtDireccion").attr('class', 'css_input');
	$("#cmdTipoDoc").attr('class', 'css_select');
	$("#cmbDepartamento").attr('class', 'css_select');
	$("#cmbProvincia").attr('class', 'css_select');
	$("#cmbDistrito").attr('class', 'css_select');
		
}



//****************************************************************
// Funcion		:: 	fn_cancelaCesionario
// Descripción	::	Editar Cesionario
// Log			:: 	JRC - 09/11/2013
//****************************************************************
function fn_cancelaCesionario() {	
	fn_limpiar();
	$("#hddTipoTx").val(C_TX_NUEVO);
	
	//Botones
    $("#sp_editar").hide();
    $("#sp_nuevo").show();
}


//****************************************************************
// Funcion		:: 	fn_editarCesionario
// Descripción	::	Editar Cesionario
// Log			:: 	JRC - 09/11/2013
//****************************************************************
function fn_editarCesionario() {	

	var strCodCesionario = $("#hddCodCesionario").val();
	
	if(fn_util_trim(strCodCesionario) == ""){
		parent.fn_mdl_mensajeIco("Debe seleccionar un Cesionario", "util/images/warning.gif", "VALIDACION");
	}else{
		
		//Quita Obligatorios
		$("#txtRazSocial").attr('class', 'css_input');
		$("#txtNroDocumento").attr('class', 'css_input');
		$("#txtDireccion").attr('class', 'css_input');
		$("#cmdTipoDoc").attr('class', 'css_select');
		$("#cmbDepartamento").attr('class', 'css_select');
		$("#cmbProvincia").attr('class', 'css_select');
		$("#cmbDistrito").attr('class', 'css_select');
		
		//Pone Datos
		$("#txtRazSocial").val(strRazSocial);
		$("#cmdTipoDoc").val(strTipoDoc);
		$("#txtNroDocumento").val(strNroDoc);
		$("#txtDireccion").val(strDireccion);
		
		$("#cmbDepartamento").val(strDepartamento);		
		fn_cargaComboProvincia(strDepartamento,strProvincia) 		
		fn_cargaComboDistrito(strDepartamento, strProvincia, strDistrito)
						
		//$("#cmbProvincia").val(strProvincia);
		//$("#cmbDistrito").val(strDistrito);
		
		$("#txtCodUnico").val(strCodUnico);
		$("#cmbTipoCuenta").val(strCodigoTipoCuenta);
		$("#cmbMoneda").val(strCodigoMoneda);
		$("#txtNumeroCuenta").val(strCuenta);
				
		$("#hddTipoTx").val(C_TX_EDITAR);
	
		$("#sp_editar").show();
		$("#sp_nuevo").hide();
    
	}

}


//****************************************************************
// Funcion		:: 	fn_eliminarCesionario
// Descripción	::	Editar Cesionario
// Log			:: 	JRC - 09/11/2013
//****************************************************************
function fn_eliminarCesionario() {	

	var strCodCesionario = $("#hddCodCesionario").val();
	
	if(fn_util_trim(strCodCesionario) == ""){
		parent.fn_mdl_mensajeIco("Debe seleccionar un Cesionario", "util/images/warning.gif", "VALIDACION");
	}else{
	
		var strCodContrato = $("#hddCodContrato").val();
		var strCodCesionario = $("#hddCodCesionario").val();		
		var paramArray = ["hddCodContrato", strCodContrato, "hddCodCesionario", strCodCesionario];
    
		parent.fn_mdl_confirma(
            "¿Está seguro que desea eliminar el Cesionario seleccionado?  ",
            function() {
                parent.fn_blockUI();
                
                fn_util_AjaxWM("frmCesionarioRegistro.aspx/EliminaCesionario",
                                   paramArray,
                                   function(resultado) {
                                       fn_listaCesionarios();
                                       $("#hddCodCesionario").val("");
                                       parent.fn_unBlockUI();
                                   },
                                   function(resultado) {
                                       var error = eval("(" + resultado.responseText + ")");
                                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA ELIMINACIÓN");
                                   }
                    );
            },
            null,
            function() { },
            'VALIDACIÓN'
		);
		
	}

}


//****************************************************************
// Funcion		:: 	fn_cargaRepresentantes
// Descripción	::	Editar Cesionario
// Log			:: 	JRC - 09/11/2013
//****************************************************************
function fn_cargaRepresentantes() {	

	var strCodCesionario = $("#hddCodCesionario").val();
	
	if(fn_util_trim(strCodCesionario) == ""){
		parent.fn_mdl_mensajeIco("Debe seleccionar un Cesionario", "util/images/warning.gif", "VALIDACION");
	}else{
		parent.fn_util_AbreModal2("Cesión de Contrato :: Registro Representante", "Formalizacion/CesionContrato/frmRepresentanteRegistro.aspx?hddCodContrato=" + $("#hddCodContrato").val() + "&hddCodCesionario=" + strCodCesionario, 850, 550, function() { });		
	}

}



//****************************************************************
// Funcion		:: 	fn_cargaRepresentantes
// Descripción	::	Editar Cesionario
// Log			:: 	JRC - 15/02/2013
//****************************************************************
function ConsultaRM(){
	parent.fn_blockUI();
	
	var strTipoDocumento = $("#cmdTipoDoc").val();
	var strNroDocumento = $("#txtNroDocumento").val();

	parent.fn_blockUI();
    if (fn_util_trim(strTipoDocumento) == "" || fn_util_trim(strNroDocumento) == "") {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco("Para verificar los datos con RM debe ingresar el Tipo y Número de Documento", "util/images/error.gif", "ADVERTENCIA");
    } else {

        //Lmpia RM
        //$('#txtRazSocial').val("");
        //$('#txtDireccion').val("");

        //Valores            
        var paramArray = ["pstrTipoDoc", strTipoDocumento, "pstrNroDoc", strNroDocumento];

        fn_util_AjaxWM("frmCesionarioRegistro.aspx/ConsultaRM",
               paramArray,
               fn_PoneDatosClienteRM,
               function(resultado) {
                   parent.fn_unBlockUI();
                   parent.fn_mdl_mensajeIco("Se produjo un error al cargar los datos de RM", "util/images/error.gif", "ERROR EN CONSULTA RM");
               }
        );

    }

}



//****************************************************************
// Funcion		:: 	fn_PoneDatosClienteRM
// Descripción	::	Pone Datos del Cliente RM
// Log			:: 	JRC - 15/02/2013
//****************************************************************
var fn_PoneDatosClienteRM = function(response) {

    var objEClienteRM = response;

    if (objEClienteRM.CodError == 1) {
        parent.fn_unBlockUI();
        parent.fn_mdl_mensajeIco(objEClienteRM.MsgError, "util/images/error.gif", "ERROR EN CONSULTA RM");
    } else {

        $('#txtCodUnico').val(objEClienteRM.Codigounico);        
        $('#txtRazSocial').val(objEClienteRM.Razonsocialcliente);
        //$('#cmbTipoDocumento').val(objEClienteRM.Codigotipodocumento);
        //$('#txtNumeroDocumento').val(objEClienteRM.Numerodocumento);
        $('#txtDireccion').val(objEClienteRM.Direccion);
        
        parent.fn_unBlockUI();
        
	}

};



//****************************************************************
// Funcion		:: 	fn_PoneDatosClienteRM
// Descripción	::	Pone Datos del Cliente RM
// Log			:: 	JRC - 15/02/2013
//****************************************************************
function fn_validaCuenta(){
	
	var strMensajeError = "";
	
     if ($("#cmbTipoCuenta").val() == "01") {
         var strargFCDTIPOCUENTA = "IM";
     } else {
         strargFCDTIPOCUENTA = "ST";
     }
     if ($("#cmbMoneda").val() == "001") {
         var strargFCDCODMONEDA = "001";
     } else {
         strargFCDCODMONEDA = "010";
     }
     if ($("#cmbTipoCuenta").val() == "01") {
         var strCoCategoria = "001";
     } else {
         strCoCategoria = "002";
     }
     var NumeroCuenta = fn_util_trim($("#txtNumeroCuenta").val());
     
     
     
     
	if( NumeroCuenta != "" || $("#cmbTipoCuenta").val() != 0 || $("#cmbMoneda").val() != 0){
				
		if( fn_util_trim($("#txtCodUnico").val()) != "" ){
				
			 if ((NumeroCuenta.length) != 13) {
				strMensajeError = strMensajeError + "&nbsp;&nbsp;- La primera cuenta debe tener 13 digitos <br/> ";				
			 }
			 if ($("#cmbTipoCuenta").val() == 0) {
				strMensajeError = strMensajeError + "&nbsp;&nbsp;- Debe seleccionar un Tipo de Cuenta <br/> ";				
			 }
			 if ($("#cmbMoneda").val() == 0) {
				strMensajeError = strMensajeError + "&nbsp;&nbsp;- Debe seleccionar una Moneda <br/> ";				
			 }
			
		}else{
			strMensajeError = strMensajeError + "&nbsp;&nbsp;- No se puede validar la Cuenta. El Cesionario no cuenta con Cod. Unico <br/> ";				
		}
	     
	 }else{     
		return "";     
	 }
     
     
     
     
     
     
     
     if(fn_util_trim(strMensajeError) != ""){
				
		return strMensajeError;
		
     }else{
     
		var arrParametros3 =	["argFCDTIPOCUENTA",    strargFCDTIPOCUENTA,
								 "argFCDCODMONEDA",     strargFCDCODMONEDA,
								 "argFCDCODTIENDA",     NumeroCuenta.substr(0, 3),
								 "argFCDCODCATEGORIA",  strCoCategoria,
								 "argFCDNUMCUENTA",     NumeroCuenta.substr(3, 12),							
   								 "pCodUnico",			$("#txtCodUnico").val()
								];
								
		 fn_util_AjaxSyncWM("frmCesionarioRegistro.aspx/ValidaCuentaST",
		 arrParametros3,
		 function(result) {
			
			var resultado = fn_util_trim(result);
			var arrResult = resultado.split('|');            
            
            if (arrResult[0] != "0") {		
            	strMensajeError = arrResult[1]; 
            }else{
				strMensajeError = "";
            }             
                          			
		 },
		 function(result) {
			strMensajeError = result.status + ' ' + result.statusText;			
		 });
     
     }
     
     
     return strMensajeError;

}
