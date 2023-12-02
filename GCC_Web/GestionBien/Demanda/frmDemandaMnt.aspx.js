//****************************************************************
// Variables Globales
//****************************************************************
var C_TX_NUEVO = "NUEVO"
var C_TX_EDITAR = "EDITAR"
var C_APLICFONDO_ABONOCUENTA = "003";
var C_TIPO_SINIESTRO_SINIESTRO = "001"
var C_TIPO_SINIESTRO_DEMANDA = "002"

var strTipoDocumentoDNI = "1";
var strTipoDocumentoRUC = "2";
var strTipoDocumentoCarnetEx = "3";
var strTipoDocumentoPasaporte = "5";
var strTipoDocumentoOtroDoc = "6";

var C_GESTIONBIEN_DEMANDA = "002";

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	??? - 02/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();
    
    //Valida Tabs
    $("div#divTabs").tabs({
        show: function(event, ui) {
            fn_doResize();
        }
    });
           
    //Grilla Implicados
    fn_cargaGrilla()
    
    //Fechas
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));
    
    //Desactiva Fecha    
    $('#txtFechaSituacion').removeClass('css_calendario');
    $("#txtFechaSituacion").attr('disabled', 'disabled');
    $('#txtFechaSituacion').addClass('css_input_inactivo');
    
    
    //***********************************    	
    //Valida Modo Ver
    //***********************************
    var strVer = $("#hddVer").val();   
    if( fn_util_trim(strVer) == "1" ){
		fn_util_bloquearFormulario();
		$("#sp_accion").html("Ver");
		$("#dv_guardar").hide();
		$("#tb_btnImplicados").hide();		
    }
    
    
    //***********************************
    //Valida Aplica Fondo    
    //***********************************
    $('#cmbAplicaFondo').change(function() {
        var strValor = $(this).val();
        
        $("#txtNroCuenta").val("");
		$("#cmbTipoCuenta").val("0");
		$("#cmbMonedaCuenta").val("0");
	
        fn_validaAplicFondo(strValor);        
    });	    
    
    
    //Valida Tipo Siniestro segun Tx
    if( fn_util_trim($("#hddTipoTx").val()) == C_TX_NUEVO ) {
		$("#hddTipoSiniestro").val(C_TIPO_SINIESTRO_DEMANDA);
		$("#tab2-tab").css("display", "none");
    }else{
		if( fn_util_trim($("#hddTipoSiniestro").val()) == "001" ){
			fn_inactivaCamposSiniestro();
		}else{
			$("#txtNroSiniestro").val("");
		}		
		$("#tab2-tab").css("display", "");		
    }
        
    //Oculta botones Implicados
    $("#dv_cancelar").hide();
	$("#dv_modificar").hide();
        
        
    //---------------------------------
    //Valida Tipo Documento
    //---------------------------------
    $('#cmbTipoDocImplicado').change(function() {
        var strValor = $(this).val();
        $("#txtNroDocImplicado").val("");
        $('#txtNroDocImplicado').unbind('keypress');
        if (fn_util_trim(strValor) == strTipoDocumentoDNI) {
            $('#txtNroDocImplicado').validText({ type: 'number', length: 8 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoRUC) {
            $('#txtNroDocImplicado').validText({ type: 'number', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoCarnetEx) {
            $('#txtNroDocImplicado').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoPasaporte) {
            $('#txtNroDocImplicado').validText({ type: 'alphanumeric', length: 11 });
        } else {
            $('#txtNroDocImplicado').validText({ type: 'alphanumeric', length: 11 });
        }
    });     
    
        
    //On load Page (siempre al final)
    fn_onLoadPage();
       
});

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {
	
	//***********************************
    //Inicializa Siniestro
    //***********************************
	fn_util_inactivaInput("txtNroSiniestro", "I");	
	if( fn_util_trim($("#hddTipoTx").val()) == C_TX_NUEVO ) {
		fn_util_inactivaInput("txtNroSiniestro", "I");	
		$("#tr_cuenta").hide();
		$("#txtMontoIndem").validNumber({ value: '0' });
		$("#dv_documentos").hide();
		$("#dv_separador").hide();
	}else{
		fn_validaAplicFondo($('#cmbAplicaFondo').val());
		$("#txtMontoIndem").validNumber({ value: $("#txtMontoIndem").val() });
	}

	$('#txtChequeAseg').validText({ type: 'comment', length: 20 });
	$('#txtNroPoliza').validText({ type: 'comment', length: 20 });	
	$('#txtNroCuenta').validText({ type: 'number', length: 13 });	


	//***********************************
    //Inicializa Demanda
    //***********************************
    fn_util_inactivaInput("txtNroDemanda", "I");	        
    $('#txtJuzgado').validText({ type: 'comment', length: 50 });    
    
    if( fn_util_trim($("#hddTipoTx").val()) == C_TX_NUEVO ) {
		$("#txtMontoDemanda").validNumber({ value: '0' });
	}else{
		$("#txtMontoDemanda").validNumber({ value: $("#txtMontoDemanda").val() });
	}
	
    //***********************************
    //Inicializa Implicado
    //***********************************
    $('#txtNombreImplicado').validText({ type: 'comment', length: 50 });	
	$('#txtNroDocImplicado').validText({ type: 'number', length: 11 });	
    
    
}



//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla Listado de Cotizaciones
// Log			:: 	JRC - 07/11/2012
//****************************************************************
function fn_cargaGrilla() {

     $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");	
            fn_buscaImplicados();
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
        colNames: ['','','','Tipo','Nombre Completo','Tipo Documento','Nro. Documento'],
        colModel: [
				{ name: 'CodImplicado',			index: 'CodImplicado',			hidden: true },
				{ name: 'CodTipoDocumento',		index: 'CodTipoDocumento',		hidden: true },
				{ name: 'CodTipoImplicado',		index: 'CodTipoImplicado',		hidden: true },
                { name: 'DesTipoImplicado',		index: 'DesTipoImplicado',		sortable: true, sorttype: "string",	align: "center", defaultValue: "" },
                { name: 'NombreImplicado',		index: 'NombreImplicado',		sortable: true, sorttype: "string", align: "center", defaultValue: "" },                         
                { name: 'NombreTipoDocumento',	index: 'NombreTipoDocumento',	sortable: true, sorttype: "string", align: "center", defaultValue: "" },                         
                { name: 'NroDocumento',			index: 'NroDocumento',			sortable: true, sorttype: "string", align: "center", defaultValue: "" }
        ],
        width: glb_intWidthPantalla-90,
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'NombreImplicado',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodImplicado").val(rowData.CodImplicado);
            $("#hddNroDocImplicado").val(rowData.NroDocumento);
            $("#hddTipoDocImplicado").val(rowData.CodTipoDocumento);
            $("#hddNombreImplicado").val(rowData.NombreImplicado);
            $("#hddTipoImplicado").val(rowData.CodTipoImplicado);            
        },
        ondblClickRow: function(id) {            
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddCodImplicado").val(rowData.CodImplicado);
            $("#hddNroDocImplicado").val(rowData.NroDocumento);
            $("#hddTipoDocImplicado").val(rowData.CodTipoDocumento);
            $("#hddNombreImplicado").val(rowData.NombreImplicado);
            $("#hddTipoImplicado").val(rowData.CodTipoImplicado);             
        }
    });
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
   
}


//****************************************************************
// Funcion		:: 	fn_buscaImplicados
// Descripción	::	Busca listado por parametros
// Log			:: 	JRC - 07/11/2012
//****************************************************************
function fn_buscaImplicados() {
    try {
        parent.fn_blockUI();

        var hddCodContrato = $('#hddCodContrato').val() == undefined ? "" : $('#hddCodContrato').val();
        var hddCodBien = $('#hddCodBien').val() == undefined ? "" : $('#hddCodBien').val();
        var hddCodDemanda = $('#hddCodDemanda').val() == undefined ? "" : $('#hddCodDemanda').val();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", intPaginaActual, // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación                             
                             "pstrCodContrato", hddCodContrato,
                             "pstrCodBien", hddCodBien,
                             "pstrCodDemanda", hddCodDemanda
                            ];

		//alert(arrParametros);
        fn_util_AjaxWM("frmDemandaMnt.aspx/ListaImplicados",
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
// Funcion		:: 	fn_buscaSiniestro
// Descripción	::	Abre buscar Siniestro
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_buscaSiniestro() {
	parent.fn_util_AbreModal2("Demanda :: Nuevo", "GestionBien/Demanda/frmSiniestroBusqueda.aspx?hddCodContrato="+ $("#hddCodContrato").val() + "&hddCodBien=" + $("#hddCodBien").val(), 950, 550, function() { });
}




//****************************************************************
// Funcion		:: 	fn_guardar
// Descripción	::	Guarda
// Log			:: 	JRC - 13/11/2012
//****************************************************************
function fn_guardar() {
	parent.fn_blockUI();

    //String Validación
    var strError = new StringBuilderEx();

    //Instancia Validaciones    		
    //txtNroSiniestro
	var objtxtFechaConoBanco = $('input[id=txtFechaConoBanco]:text');
	var objtxtFechaConoLeasing = $('input[id=txtFechaConoLeasing]:text');
	var objtxtFechaSiniestro = $('input[id=txtFechaSiniestro]:text');
	var objcmbTipo = $('select[id=cmbTipo]');
	var objtxtFechaSituacion = $('input[id=txtFechaSituacion]:text');
	var objcmbSituacion = $('select[id=cmbSituacion]');
	var objcmbContrato = $('select[id=cmbContrato]');
	var objtxtFechaAplicacion = $('input[id=txtFechaAplicacion]:text');
	var objcmbAplicacion = $('select[id=cmbAplicacion]');
	//txtFechaDescargoMunicipal
	//txtFechaTransparencia
	//cmbTransferencia
	//cmbOrigenCono
	var objcmbSeguro = $('select[id=cmbSeguro]');
	//txtChequeAseg
	var objcmbEstadoBien = $('select[id=cmbEstadoBien]');
	var objtxtNroPoliza = $('input[id=txtNroPoliza]:text');
	var objcmbTipoPoliza = $('select[id=cmbTipoPoliza]');
	//txtFechaIndem
	//cmbMonedaIndem
	//txtMontoIndem
	//cmbBancoEmite
	var objcmbAplicaFondo = $('select[id=cmbAplicaFondo]');
	var objtxtNroCuenta = $('input[id=txtNroCuenta]:text');
	var objcmbTipoCuenta = $('select[id=cmbTipoCuenta]');
	var objcmbMonedaCuenta = $('select[id=cmbMonedaCuenta]');
	
	var objtxtNroDemanda = $('input[id=txtNroDemanda]:text');
	var objtxtFechaDemanda = $('input[id=txtFechaDemanda]:text');
	var objcmbEstadoDemanda = $('select[id=cmbEstadoDemanda]');
	//txtJuzgado
	var objcmbMonedaDemanda = $('select[id=cmbMonedaDemanda]');
	var objtxtMontoDemanda = $('input[id=txtMontoDemanda]:text');
	
	var objcmbTransferencia = $('select[id=cmbTransferencia]');
	var objcmbOrigenCono = $('select[id=cmbOrigenCono]');
	

    //Valida
	strError.append(fn_util_ValidateControl(objtxtFechaConoBanco[0], 'una Fecha Conocimiento Banco válida', 1, ''));
	strError.append(fn_util_ValidateControl(objtxtFechaConoLeasing[0], 'una Fecha Conocimiento Leasing válida', 1, ''));
	//strError.append(fn_util_ValidateControl(objtxtFechaSiniestro[0], 'una Fecha de Siniestro válida', 1, ''));
	//strError.append(fn_util_ValidateControl(objcmbTipo[0], 'un Tipo válido', 1, ''));
	strError.append(fn_util_ValidateControl(objtxtFechaSituacion[0], 'una Fecha Situación válida', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbSituacion[0], 'un Tipo Situación válido', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbContrato[0], 'un Contrato válido', 1, ''));
	//strError.append(fn_util_ValidateControl(objtxtFechaAplicacion[0], 'una Fecha de Aplicación válida', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbAplicacion[0], 'una Aplicación válida', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbSeguro[0], 'un Seguro válido', 1, ''));
	//strError.append(fn_util_ValidateControl(objcmbEstadoBien[0], 'un Estado de Bien válido', 1, ''));
	strError.append(fn_util_ValidateControl(objtxtNroPoliza[0], 'un Nro. Póliza válido', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbTipoPoliza[0], 'un Tipo de Poliza válido', 1, ''));
	//strError.append(fn_util_ValidateControl(objcmbAplicaFondo[0], 'una Aplicación de Fondo válida', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbTransferencia[0], 'una Transferencia del Bien válida', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbOrigenCono[0], 'un Origen Conocimiento válido', 1, ''));
	
	
	if( fn_util_trim($('#cmbAplicaFondo').val()) == C_APLICFONDO_ABONOCUENTA ){    	
		strError.append(fn_util_ValidateControl(objtxtNroCuenta[0], 'un Nro. Cuenta válido', 1, ''));
		strError.append(fn_util_ValidateControl(objcmbTipoCuenta[0], 'un Tipo de Cuenta válido', 1, ''));
		strError.append(fn_util_ValidateControl(objcmbMonedaCuenta[0], 'una Moneda de Cuenta válida', 1, ''));
	}
	
	//strError.append(fn_util_ValidateControl(objtxtNroDemanda[0], 'un Nro. de Demanda válido', 1, ''));
	strError.append(fn_util_ValidateControl(objtxtFechaDemanda[0], 'una Fecha de Demanda válida', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbEstadoDemanda[0], 'un Estado de Demanda válido', 1, ''));
	strError.append(fn_util_ValidateControl(objcmbMonedaDemanda[0], 'una Moneda de Demanda válida', 1, ''));
	strError.append(fn_util_ValidateControl(objtxtMontoDemanda[0], 'un Monto válido válido', 1, ''));

	//Setea Icono Calendario en caso se quiten
	if( fn_util_trim($("#hddTipoSiniestro").val()) == "001" ){
		fn_inactivaCamposSiniestro();
	}else{
		fn_util_SeteaCalendario($('input[id*=txtFecha]'));
	}	
	
	if( fn_util_trim($("#hddTipoTx").val()) != C_TX_NUEVO ) {
		var intCantImplicados = $("#jqGrid_lista_A").getGridParam("reccount");
		if (intCantImplicados == null || intCantImplicados == "" || intCantImplicados == undefined || intCantImplicados == "undefined") intCantImplicados = 0;  
        if (intCantImplicados == 0) {
            strError.append("&nbsp;&nbsp;- Debe agregar al menos un Implicado.<br />");         
            $("div#divTabs").tabs("select", [2]);  
        }
	}
	
	//Valida error existente
	if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }else{
    


			var arrParametros = [     
								"pstrTipoTx", $("#hddTipoTx").val(),    
								"pstrCodContrato", $("#hddCodContrato").val(), 
								"pstrCodBien", $("#hddCodBien").val(),    
								"pstrCodSiniestro", $("#hddCodSiniestro").val(),    								
								"pstrNroSiniestro", $("#hddNroSiniestro").val(),
								"pstrFechaConoBanco", Fn_util_DateToString($("#txtFechaConoBanco").val()),
								"pstrFechaConoLeasing", Fn_util_DateToString($("#txtFechaConoLeasing").val()),
								"pstrFechaSiniestro", Fn_util_DateToString($("#txtFechaSiniestro").val()),
								"pstrTipo", $("#cmbTipo").val(),
								"pstrFechaSituacion", Fn_util_DateToString($("#txtFechaSituacion").val()),
								"pstrSituacion", $("#cmbSituacion").val(),
								"pstrContrato", $("#cmbContrato").val(),
								"pstrFechaAplicacion", Fn_util_DateToString($("#txtFechaAplicacion").val()),
								"pstrAplicacion", $("#cmbAplicacion").val(),
								"pstrFechaDescargoMunicipal", Fn_util_DateToString($("#txtFechaDescargoMunicipal").val()),
								"pstrFechaTransparencia", Fn_util_DateToString($("#txtFechaTransparencia").val()),
								"pstrTransferencia", $("#cmbTransferencia").val(),
								"pstrOrigenCono", $("#cmbOrigenCono").val(),
								"pstrSeguro", $("#cmbSeguro").val(),
								"pstrChequeAseg", $("#txtChequeAseg").val(),
								"pstrEstadoBien", $("#cmbEstadoBien").val(),
								"pstrNroPoliza", $("#txtNroPoliza").val(),
								"pstrTipoPoliza", $("#cmbTipoPoliza").val(),
								"pstrFechaIndem", Fn_util_DateToString($("#txtFechaIndem").val()),
								"pstrMonedaIndem", $("#cmbMonedaIndem").val(),
								"pstrMontoIndem", $("#txtMontoIndem").val(),
								"pstrBancoEmite", $("#cmbBancoEmite").val(),
								"pstrAplicaFondo", $("#cmbAplicaFondo").val(),
								"pstrNroCuenta", $("#txtNroCuenta").val(),
								"pstrTipoCuenta", $("#cmbTipoCuenta").val(),
								"pstrMonedaCuenta", $("#cmbMonedaCuenta").val(),
								"pstrCodUnico", $("#hddCodUnico").val(),
								
								"pstrTipoSiniestro", $("#hddTipoSiniestro").val(),
								"pstrCodDemanda", $("#hddCodDemanda").val(),								
								"pstrNroDemanda", $("#txtNroDemanda").val(),
								"pstrFechaDemanda", Fn_util_DateToString($("#txtFechaDemanda").val()),
								"pstrEstadoDemanda", $("#cmbEstadoDemanda").val(),
								"pstrJuzgado", $("#txtJuzgado").val(),
								"pstrMonedaDemanda", $("#cmbMonedaDemanda").val(),
								"pstrMontoDemanda", $("#txtMontoDemanda").val()
																    
								];
                
        fn_util_AjaxWM("frmDemandaMnt.aspx/GrabaDemanda",
                 arrParametros,
                 function(result) {
                     parent.fn_unBlockUI();
                     if (fn_util_trim(result) == "0") {
                         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL GUARDAR");
                     } else {                         
                         if ($("#hddTipoTx").val() == C_TX_NUEVO) {
                         
							 var strCodigos = fn_util_trim(result);
							 var arrCodigos = strCodigos.split("|");
							 $("#hddCodDemanda").val(fn_util_trim(arrCodigos[0]));
							 $("#hddCodSiniestro").val(fn_util_trim(arrCodigos[1]));
							 //alert($("#hddCodDemanda").val()+" - "+$("#hddCodSiniestro").val());
							 
                             parent.fn_mdl_mensajeOk("Se grabó la Demanda correctamente.", function() { fn_RedireccionGrabar() }, "GRABADO CORRECTO");
                         } else {
                             parent.fn_mdl_mensajeOk("Se actualizó correctamente los datos de la Demanda", function() { fn_RedireccionGrabar() }, "GRABADO CORRECTO");
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
function fn_RedireccionGrabar() {    
	var ctrlBtn = window.parent.frames[0].document.getElementById('btnBuscar');
    ctrlBtn.click();
    $("#tab2-tab").css("display", "block");
    $("div#divTabs").tabs("enable", [2]);
    $("div#divTabs").tabs("select", [2]);
    
    $("#hddTipoTx").val(C_TX_EDITAR);
    
    fn_buscaImplicados();
    
    //parent.fn_util_CierraModal();
}


//****************************************************************
// Funcion		:: 	fn_validaAplicFondo
// Descripción	::	valida Aplicacion Fondos
// Log			:: 	JRC - 13/11/2012
//****************************************************************
function fn_validaAplicFondo(strValor) {
	if( fn_util_trim(strValor) == C_APLICFONDO_ABONOCUENTA ){
		$("#tr_cuenta").show();
    }else{
		$("#tr_cuenta").hide();
    }	
}


//****************************************************************
// Funcion		:: 	fn_aplicaBusquedaSiniestro
// Descripción	::	valida Aplicacion Fondos
// Log			:: 	JRC - 13/11/2012
//****************************************************************
function fn_aplicaBusquedaSiniestro() {
	
	parent.fn_blockUI();
     
    try{
                
    var strCodContrato = $("#hddCodContrato").val();
    var strCodBien = $("#hddCodBien").val();
    var strCodSiniestro = $("#hddCodSiniestroBsq").val();
    var paramArray = ["hddCodContrato", strCodContrato, "hddCodBien", strCodBien, "hddCodSiniestro", strCodSiniestro];
        
	fn_util_AjaxWM("frmDemandaMnt.aspx/CargaSiniestro",
				   paramArray,
				   fn_PoneDatosSiniestro,
				   function(resultado) {
					   var error = eval("(" + resultado.responseText + ")");
					   parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA ELIMINACIÓN");
				   }
	);
	
	}catch(e){
		alert(e.message);
	}
	
}


//****************************************************************
// Funcion		:: 	fn_PoneDatosSiniestro
// Descripción	::	Pone los Datos Siniestro
// Log			:: 	JRC - 13/11/2012
//****************************************************************
var fn_PoneDatosSiniestro = function(response) {
	
	var objSiniestro = response;
	
	$('#hddTipoSiniestro').val("001");
	$('#hddCodSiniestro').val(objSiniestro.Secsiniestro);
		
	$('#txtNroSiniestro').val(objSiniestro.NroSiniestro);
	$('#txtFechaConoBanco').val(objSiniestro.FecConocimientoBancoStr);
	$('#txtFechaConoLeasing').val(objSiniestro.FecConocimientoStr);
	$('#txtFechaSiniestro').val(objSiniestro.FecSiniestroStr);
	$('#cmbTipo').val(objSiniestro.Tipo);
	$('#txtFechaSituacion').val(objSiniestro.FecSituacionStr);
	$('#cmbSituacion').val(objSiniestro.Situacion);
	$('#cmbContrato').val(objSiniestro.Contrato);
	$('#txtFechaAplicacion').val(objSiniestro.FecAplicacionStr);
	$('#cmbAplicacion').val(objSiniestro.Aplicacion);
	$('#txtFechaDescargoMunicipal').val(objSiniestro.FecDescargoMunicipalStr);
	$('#txtFechaTransparencia').val(objSiniestro.FecTransferenciaStr);
	$('#cmbTransferencia').val(objSiniestro.Transferencia);
	$('#cmbOrigenCono').val(objSiniestro.Origen);
	$('#cmbSeguro').val(objSiniestro.Seguro);
	$('#txtChequeAseg').val(objSiniestro.NroChequeAseguradora);
	$('#cmbEstadoBien').val(objSiniestro.CodEstadoBien);
	$('#txtNroPoliza').val(objSiniestro.NroPoliza);
	$('#cmbTipoPoliza').val(objSiniestro.CodTipoPoliza);
	$('#txtFechaIndem').val(objSiniestro.FecRecIndemnizacionStr);
	$('#cmbMonedaIndem').val(objSiniestro.Moneda);
	$('#txtMontoIndem').val(fn_util_ValidaMonto(objSiniestro.Montoindemnizacion, 2));
	$('#cmbBancoEmite').val(objSiniestro.CodBancoEmiteCheque);
	$('#cmbAplicaFondo').val(objSiniestro.CodAplicaFondo);
	$('#txtNroCuenta').val(objSiniestro.NroCuenta);
	$('#cmbTipoCuenta').val(objSiniestro.CodTipoCuenta);
	$('#cmbMonedaCuenta').val(objSiniestro.CodMonedaCuenta);

	//Valida AbonoCuenta
	if( fn_util_trim($("#cmbAplicaFondo").val()) == C_APLICFONDO_ABONOCUENTA ){
		$("#tr_cuenta").show();
    }else{
		$("#tr_cuenta").hide();
    }	

	//Bloquea Campos
	fn_inactivaCamposSiniestro();
				
	//Desbloquear Pantalla
	parent.fn_unBlockUI();

}


//****************************************************************
// Funcion		:: 	fn_inactivaCamposSiniestro
// Descripción	::	Inactiva Campos Siniestro
// Log			:: 	JRC - 13/11/2012
//****************************************************************
function fn_inactivaCamposSiniestro(){

	//fn_util_inactivaInput("txtNroSiniestro", "I");	
	fn_util_inactivaInputDis("txtFechaConoBanco", "I");	
	fn_util_inactivaInputDis("txtFechaConoLeasing", "I");	
	fn_util_inactivaInputDis("cmbTipo", "S");		
	fn_util_inactivaInputDis("txtFechaSiniestro", "I");	
	fn_util_inactivaInputDis("txtFechaSituacion", "I");	
	fn_util_inactivaInputDis("cmbSituacion", "S");	
	fn_util_inactivaInputDis("cmbContrato", "S");	
	fn_util_inactivaInputDis("txtFechaAplicacion", "I");	
	fn_util_inactivaInputDis("cmbAplicacion", "S");	
	fn_util_inactivaInputDis("txtFechaDescargoMunicipal", "I");	
	fn_util_inactivaInputDis("txtFechaTransparencia", "I");	
	fn_util_inactivaInputDis("cmbTransferencia", "S");	
	fn_util_inactivaInputDis("cmbOrigenCono", "S");	
	fn_util_inactivaInputDis("cmbSeguro", "S");	
	fn_util_inactivaInputDis("txtChequeAseg", "I");	
	fn_util_inactivaInputDis("cmbEstadoBien", "S");	
	fn_util_inactivaInputDis("txtNroPoliza", "I");	
	fn_util_inactivaInputDis("cmbTipoPoliza", "S");	
	fn_util_inactivaInputDis("txtFechaIndem", "I");
	fn_util_inactivaInputDis("cmbMonedaIndem", "S");	
	fn_util_inactivaInputDis("txtMontoIndem", "I");	
	fn_util_inactivaInputDis("cmbBancoEmite", "S");	
	fn_util_inactivaInputDis("cmbAplicaFondo", "S");	
	fn_util_inactivaInputDis("txtNroCuenta", "I");	
	fn_util_inactivaInputDis("cmbTipoCuenta", "S");		
	fn_util_inactivaInputDis("cmbMonedaCuenta", "S");	
	
}



//****************************************************************
// Funcion		:: 	fn_activaCamposSiniestro
// Descripción	::	Activa Campos Siniestro
// Log			:: 	JRC - 13/11/2012
//****************************************************************
function fn_activaCamposSiniestro(){
	
	//fn_util_activaInput("txtNroSiniestro", "I");	
	fn_util_activaInputDis("txtFechaConoBanco", "I");	
	fn_util_activaInputDis("txtFechaConoLeasing", "I");	
	fn_util_activaInputDis("cmbTipo", "S");	
	fn_util_activaInputDis("txtFechaSiniestro", "I");	
	fn_util_activaInputDis("txtFechaSituacion", "I");	
	fn_util_activaInputDis("cmbSituacion", "S");	
	fn_util_activaInputDis("cmbContrato", "S");	
	fn_util_activaInputDis("txtFechaAplicacion", "I");	
	fn_util_activaInputDis("cmbAplicacion", "S");	
	fn_util_activaInputDis("txtFechaDescargoMunicipal", "I");	
	fn_util_activaInputDis("txtFechaTransparencia", "I");	
	fn_util_activaInputDis("cmbTransferencia", "S");	
	fn_util_activaInputDis("cmbOrigenCono", "S");	
	fn_util_activaInputDis("cmbSeguro", "S");	
	fn_util_activaInputDis("txtChequeAseg", "I");	
	fn_util_activaInputDis("cmbEstadoBien", "S");	
	fn_util_activaInputDis("txtNroPoliza", "I");	
	fn_util_activaInputDis("cmbTipoPoliza", "S");	
	fn_util_activaInputDis("txtFechaIndem", "I");
	fn_util_activaInputDis("cmbMonedaIndem", "S");	
	fn_util_activaInputDis("txtMontoIndem", "I");	
	fn_util_activaInputDis("cmbBancoEmite", "S");	
	fn_util_activaInputDis("cmbAplicaFondo", "S");	
	fn_util_activaInputDis("txtNroCuenta", "I");	
	fn_util_activaInputDis("cmbTipoCuenta", "S");		
	fn_util_activaInputDis("cmbMonedaCuenta", "S");	
	
	//Setea Icono Calendario en caso se quiten
	fn_util_SeteaCalendario($('input[id*=txtFecha]'));
}

//****************************************************************
// Funcion		:: 	fn_limpiaCamposSiniestro
// Descripción	::	Activa Campos Siniestro
// Log			:: 	JRC - 13/11/2012
//****************************************************************
function fn_limpiaCamposSiniestro(){

	$('#hddTipoSiniestro').val("002");
	$('#hddCodSiniestro').val("");
	$('#hddCodSiniestroBsq').val("");
	
	$("#txtNroSiniestro").val("");
	$("#txtFechaConoBanco").val("");
	$("#txtFechaConoLeasing").val("");
	$("#txtFechaSiniestro").val("");
	$("#cmbTipo").val("0");
	$("#txtFechaSituacion").val("");
	$("#cmbSituacion").val("0");
	$("#cmbContrato").val("0");
	$("#txtFechaAplicacion").val("");
	$("#cmbAplicacion").val("0");
	$("#txtFechaDescargoMunicipal").val("");
	$("#txtFechaTransparencia").val("");
	$("#cmbTransferencia").val("0");
	$("#cmbOrigenCono").val("0");
	$("#cmbSeguro").val("0");
	$("#txtChequeAseg").val("");
	$("#cmbEstadoBien").val("0");
	$("#txtNroPoliza").val("");
	$("#cmbTipoPoliza").val("0");
	$("#txtFechaIndem").val("");
	$("#cmbMonedaIndem").val("0");
	$("#txtMontoIndem").val("");
	$("#cmbBancoEmite").val("0");
	$("#cmbAplicaFondo").val("0");
	$("#txtNroCuenta").val("");
	$("#cmbTipoCuenta").val("0");
	$("#cmbMonedaCuenta").val("0");
	
}




//****************************************************************
// Funcion		:: 	fn_guardarImplicado
// Descripción	::	Guarda Implicado
// Log			:: 	JRC - 13/11/2012
//****************************************************************
function fn_guardarImplicado() {
	parent.fn_blockUI();

    //String Validación
    var strError = new StringBuilderEx();

    //Instancia Validaciones    		
    //hddCodImplicado
    var cmbTipoImplicado = $('select[id=cmbTipoImplicado]');
    var txtNombreImplicado = $('input[id=txtNombreImplicado]:text');
	var cmbTipoDocImplicado = $('select[id=cmbTipoDocImplicado]');
	var txtNroDocImplicado = $('input[id=txtNroDocImplicado]:text');
		

    //Valida
	strError.append(fn_util_ValidateControl(cmbTipoImplicado[0], 'un Tipo de Implicado válido', 1, ''));
	strError.append(fn_util_ValidateControl(txtNombreImplicado[0], 'un Nombre válido', 1, ''));
	strError.append(fn_util_ValidateControl(cmbTipoDocImplicado[0], 'un Tipo Documento válido', 1, ''));
	strError.append(fn_util_ValidateControl(txtNroDocImplicado[0], 'un Nro. Documento válido', 1, ''));
	
	var strTipoDocumento = $("#cmbTipoDocImplicado").val();
    var strNroDocumento = $("#txtNroDocImplicado").val();
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
    }
	

	//Valida error existente
	if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }else{

			var arrParametros = [     
								"pstrTipoTxImplicado", $("#hddTipoTxImplicado").val(),    
								"pstrCodContrato", $("#hddCodContrato").val(), 
								"pstrCodBien", $("#hddCodBien").val(),    
								"pstrCodDemanda", $("#hddCodDemanda").val(),    								
								"pstrCodImplicado", $("#hddCodImplicado").val(),
								
								"pstrTipoImplicado", $("#cmbTipoImplicado").val(),
								"pstrNombreImplicado", $("#txtNombreImplicado").val(),								
								"pstrTipoDocImplicado", $("#cmbTipoDocImplicado").val(),								
								"pstrNroDocImplicado", $("#txtNroDocImplicado").val()																    
								];
        
            
        fn_util_AjaxWM("frmDemandaMnt.aspx/GrabaImplicado",
                 arrParametros,
                 function(result) {
                     parent.fn_unBlockUI();
                     if (fn_util_trim(result) == "0") {
                         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL GUARDAR");
                     } else {                         
                         if ($("#hddTipoTxImplicado").val() == C_TX_NUEVO) {
                             parent.fn_mdl_mensajeOk("Se grabó el Implicado correctamente.", function() { fn_buscaImplicados();fn_cancelaImplicado(); }, "GRABADO CORRECTO");
                         } else {
                             parent.fn_mdl_mensajeOk("Se actualizó correctamente los datos del Implicado", function() { fn_buscaImplicados();fn_cancelaImplicado(); }, "GRABADO CORRECTO");
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




//****************************************************************
// Funcion		:: 	fn_editarImplicado
// Descripción	::	Carga Editar Implicado
// Log			:: 	JRC - 13/11/2012
//****************************************************************
function fn_editarImplicado(){
	
	var codTipoImplicado = $("#hddTipoImplicado").val();
	if(fn_util_trim(codTipoImplicado) == ""){
		parent.fn_mdl_mensajeError("Debe seleccionar un Implicado", function() { }, "VALIDACIÓN");
	}else{		
		$("#hddTipoTxImplicado").val(C_TX_EDITAR);
		$("#cmbTipoImplicado").val(fn_util_trim($("#hddTipoImplicado").val()));
		
		$("#txtNombreImplicado").val($("#hddNombreImplicado").val());
		$("#cmbTipoDocImplicado").val(fn_util_trim($("#hddTipoDocImplicado").val()));		
		var strValor = fn_util_trim($("#hddTipoDocImplicado").val());	
		$("#txtNroDocImplicado").val("");
		$('#txtNroDocImplicado').unbind('keypress');
		if (strValor == strTipoDocumentoDNI) {
            $('#txtNroDocImplicado').validText({ type: 'number', length: 8 });
        } else if (strValor == strTipoDocumentoRUC) {
            $('#txtNroDocImplicado').validText({ type: 'number', length: 11 });
        } else if (strValor == strTipoDocumentoCarnetEx) {
            $('#txtNroDocImplicado').validText({ type: 'alphanumeric', length: 11 });
        } else if (strValor == strTipoDocumentoPasaporte) {
            $('#txtNroDocImplicado').validText({ type: 'alphanumeric', length: 11 });
        } else {
            $('#txtNroDocImplicado').validText({ type: 'alphanumeric', length: 11 });
        }		
        $("#txtNroDocImplicado").val($("#hddNroDocImplicado").val());
		
		$("#dv_cancelar").show();
		$("#dv_modificar").show();
		$("#dv_eliminar").hide();
		$("#dv_Modificar").hide();	
		$("#dv_agregarImp").hide();	
		
	}
			
}

//****************************************************************
// Funcion		:: 	fn_cancelaImplicado
// Descripción	::	Cancela Editar Implicado
// Log			:: 	JRC - 21/11/2012
//****************************************************************
function fn_cancelaImplicado(){
	
	$("#hddTipoTxImplicado").val(C_TX_NUEVO);
	$("#cmbTipoImplicado").val("0");
	$("#txtNombreImplicado").val("");
	$("#cmbTipoDocImplicado").val("0");
	$("#txtNroDocImplicado").val("");

	$("#dv_cancelar").hide();
	$("#dv_modificar").hide();
	$("#dv_eliminar").show();
	$("#dv_Modificar").show();	
	$("#dv_agregarImp").show();
}





//****************************************************************
// Funcion		:: 	fn_eliminarImplicado
// Descripción	::	Eliminar Siniestro
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_eliminarImplicado() {
		
	//Variables
    var strCodContrato = $("#hddCodContrato").val();
    var strCodBien = $("#hddCodBien").val();
    var strCodDemanda = $("#hddCodDemanda").val();
    var strCodImplicado = $("#hddCodImplicado").val();
        
    var paramArray = ["hddCodContrato", strCodContrato, "hddCodBien", strCodBien, "hddCodDemanda", strCodDemanda, "hddCodImplicado", strCodImplicado];

    if (fn_util_trim(strCodDemanda) == "") {
        parent.fn_mdl_mensajeError("Debe seleccionar una Demanda", function() { }, "VALIDACIÓN");        
    }
    else{
    
		parent.fn_mdl_confirma(
            "¿Está seguro que desea eliminar el Implicado seleccionado?  ",
            function() {
                parent.fn_blockUI();
                
                fn_util_AjaxWM("frmDemandaMnt.aspx/EliminaImplicado",
                                   paramArray,
                                   function(resultado) {
                                       fn_buscaImplicados();
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
// Funcion		:: 	fn_cerrar
// Descripción	::	Cerrar Modal
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_cerrar(){	
	parent.fn_util_CierraModal();
}


//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos() {	
	var pstrCodContrato = fn_util_trim($("#hddCodContrato").val());
	var pstrCodBien = fn_util_trim($("#hddCodBien").val());
	var pstrCodRelacionado = fn_util_trim($("#hddCodDemanda").val());
	var pstrCodTipo = C_GESTIONBIEN_DEMANDA;
	var pstrVer = fn_util_trim($("#hddVer").val());
	parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + "&hddVer=" + pstrVer, 800, 350, function() { });	
}


