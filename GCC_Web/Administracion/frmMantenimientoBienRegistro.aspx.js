var DestinoCredito_Inmueble = ["002"];
var DestinoCredito_Maquinaria = ["003", "004", "005", "006", "011"];
var DestinoCredito_Otros = ["007", "008"];

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    fn_configurar_PanelesBienes();
    fn_cargarTipoBien();
    fn_cargaGrilla();
    fn_consultar();
	fn_SeteaUbigeo();
	fn_SeteaUbigeoBien();
	fn_SeteaUbigeoOtro();
	fn_InicializarCampos();
});

//************************************************************
// Función		:: 	fn_configurar_PanelesBienes
// Descripcion 	:: 	Configura las distintas ventanas de mantenimiento de los bienes
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_configurar_PanelesBienes() {

    $("#dv_datos_inmueble").hide();
    $("#dv_datos_vehiculo").hide();
    $("#dv_datos_otros").hide();

    // Inmueble
    if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_datos_inmueble").show(); }
    // Maquinaria
    else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_datos_vehiculo").show(); }
    // Otros
    else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_datos_otros").show(); }
}

//************************************************************
// Función		:: 	fn_cargarTipoBien
// Descripcion 	:: 	Carga el combo de tipo de bien
// Log			:: 	WCR - 15/05/2012
//************************************************************
function fn_cargarTipoBien() {
    var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL104", "pstrCodigoGenerico", $("#hidCodClasificacion").val()];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            // Inmueble
            if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) { $('#cmbTipoBien').html(arrResultado[1]); $('#cmbTipoBien').val($('#hidCodTipoBien').val()); }
            // Maquinaria
            else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) { $('#cmbTipoBien1').html(arrResultado[1]); $('#cmbTipoBien1').val($('#hidCodTipoBien').val()); }
            // Otros
            else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) { $('#cmbTipoBien2').html(arrResultado[1]); $('#cmbTipoBien2').val($('#hidCodTipoBien').val()); }
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
}



//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Inicializa Grilla
// Log			:: 	JRC - 15/06/2012
//****************************************************************
function fn_cargaGrilla() {

    $("#jqGrid_lista_F").jqGrid({
        datatype: function() {
            fn_buscarBienProveedor();
        },
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
        root: "Items",
        page: "CurrentPage", // Número de página actual.
        total: "PageCount", // Número total de páginas.
        records: "RecordCount", // Total de registros a mostrar.
        repeatitems: false,
        id: "NumeroDocumento" // Índice de la columna con la clave primaria.
    },
    colNames: ['Razón Social o Nombre', 'Tipo Documento', 'Número Documento'],
    colModel: [
                { name: 'NombreInstitucion', index: 'NombreInstitucion', width: 200, align: "left" },
	            { name: 'TipoDocumento', index: 'TipoDocumento', width: 100, align: "center" },
	            { name: 'NumeroDocumento', index: 'NumeroDocumento', width: 100, align: "left" }
	         ],
    height: '100%',
    pager: '#jqGrid_Pager_F',
    rowNum: 5,
    rowList: [10, 20, 30],
    sortname: 'invid',
    sortorder: 'desc',
    viewrecords: true,
    gridview: true,
    autowidth: true,
    altRows: true,
    altclass: 'gridAltClass',
    multiselect: false
});
}


//****************************************************************
// Funcion		:: 	fn_buscarBienProveedor
// Descripción	::	Buscar Proveedor del Bien
// Log			:: 	WCR - 18/06/2012
//****************************************************************
function fn_buscarBienProveedor() {

    var hidNumeroContrato = $('#hidNumeroContrato').val() == undefined ? "" : $('#hidNumeroContrato').val();
    var hidSecFinanciamiento = $('#hidSecFinanciamiento').val() == undefined ? "" : $('#hidSecFinanciamiento').val();
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_F", "rowNum"),     // Cantidad de elementos de la página.
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_F", "page"),    // Página actual.
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_F", "sortname"), // Columna a ordenar.
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_F", "sortorder"), // Criterio de ordenación.
                         "pNumeroContrato", hidNumeroContrato,
                         "pSecFinanciamiento", hidSecFinanciamiento
                         ];

    fn_util_AjaxWM("frmMantenimientoBienRegistro.aspx/ListarBienProveedor",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_F.addJSONData(jsondata);
                        parent.fn_unBlockUI();
                        fn_doResize();

                        if ($("#hddFlagMensajeTotal").val() > 1) {
                            if (fn_util_ValidaDecimal($("#hddtotal").val()) > fn_util_ValidaDecimal($("#txtPrecioVenta").val())) {
                                parent.fn_mdl_mensajeIco("La Suma de Facturas es Mayor al Precio de Venta", "util/images/error.gif", "ERROR EN EL REGISTRO");
                            }
                        } else {
                            $("#hddFlagMensajeTotal").val(2);
                        }
                        
                    },
                    function(request) {
                        fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                    }
        );
}

//****************************************************************
// Funcion		:: 	fn_cancelar
// Descripción	::	cancelar
// Log			:: 	WCR - 18/06/2012
//****************************************************************
function fn_cancelar() {
	fn_mdl_confirma('¿Está seguro de Volver?',
		function() {
			parent.fn_blockUI();
			fn_util_redirect('frmMantenimientoBienLista.aspx');
		},
		"../util/images/question.gif",
		function() {
		},
		'Mantenimiento Bien'
	);
}

//****************************************************************
// Funcion		:: 	fn_Validacion
// Descripción	::	Valida Registro
// Log			:: 	WCR - 18/06/2012
//****************************************************************
function fn_Validacion(pError) {
    
	 var cmbTipoBien = $('select[id=cmbTipoBien]');
     var cmbTipoBien1 = $('select[id=cmbTipoBien1]');
	 var cmbTipoBien2 = $('select[id=cmbTipoBien2]');

	 var txtPartida = $('input[id=txtPartidaRegistral]:text');
	 var txtOficina = $('input[id=txtOficinaRegistral]:text');
	 var txtPartida1 = $('input[id=txtPartidaRegistral2]:text');
	 var txtOficina1 = $('input[id=txtOficinaRegistral2]:text');

	// Inmueble
    if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) {
      pError.append(fn_util_ValidateControl(cmbTipoBien[0], 'un Tipo de Bien', 1, ''));
      pError.append(fn_util_ValidateControl(txtPartida[0], 'Partida Registral', 1, ''));
      pError.append(fn_util_ValidateControl(txtOficina[0], 'Oficina Registral', 1, ''));
    }
    // Maquinaria
    else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) {
        pError.append(fn_util_ValidateControl(cmbTipoBien1[0], 'un Tipo de Bien', 1, ''));
    }
    // Otros
    else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) {
      pError.append(fn_util_ValidateControl(cmbTipoBien2[0], 'un Tipo de Bien', 1, ''));
      pError.append(fn_util_ValidateControl(txtPartida1[0], 'Partida Registral', 1, ''));
      pError.append(fn_util_ValidateControl(txtOficina1[0], 'Oficina Registral', 1, ''));
    }
   
    return pError.toString();
}

//****************************************************************
// Funcion		:: 	fn_consultar
// Descripción	::	Consultar
// Log			:: 	WCR - 18/06/2012
//****************************************************************
function fn_consultar() {
    if ($('#hidOp').val() == '2') {
        $('#hGrabar').hide();

        // Inmueble
        if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) {
            $('#cmbTipoBien').attr('disabled', 'disabled');
            $('#txtFechaTransferencia').addClass('css_input_inactivo');
            $('#txtFechaTransferencia').prop('readonly', true);
            $('#txtObservaciones').addClass('css_input_inactivo');
            $('#txtObservaciones').prop('readonly', true);
        }
        // Maquinaria
        else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) {
            $('#cmbTipoBien1').attr('disabled', 'disabled');
            $('#txtFechaTransferencia1').addClass('css_input_inactivo');
            $('#txtFechaTransferencia1').prop('readonly', true);
            $('#txtColor').addClass('css_input_inactivo');
            $('#txtColor').prop('readonly', true);
            $('#txtObservaciones1').addClass('css_input_inactivo');
            $('#txtObservaciones1').prop('readonly', true);

        }
        // Otros
        else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) {
            $('#cmbTipoBien2').attr('disabled', 'disabled');
            $('#txtFechaTransferencia2').addClass('css_input_inactivo');
            $('#txtFechaTransferencia2').prop('readonly', true);
            $('#txtObservaciones2').addClass('css_input_inactivo');
            $('#txtObservaciones2').prop('readonly', true);
        }
    } else {
        //Setea Calendario
        fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    }

}

//****************************************************************
// Funcion		:: 	fn_grabar
// Descripción	::	Grabar
// Log			:: 	WCR - 18/06/2012
//****************************************************************
function fn_grabar() {
    var strError = new StringBuilderEx();
    fn_Validacion(strError);
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;

    }
    else {

        fn_mdl_confirma('¿Esta seguro de actualizar este registro?',
            function() {
                parent.fn_blockUI();
                var vMonedaContrato = $("#hidMonedaContrato").val();
                var vTipoCompra = $("#hidTipoCompra").val();
                var vTipoVenta = $("#hidTipoVenta").val();
                var strFlag = '0';
                var vTipoBien = '';
                var vFechaTransferencia = '';
                var vColor = '';
                var vObservaciones = '';
            	var vCodDistrito = '';
            	var vValorBien = '';
            	var vPartidaRegistral = '';
            	var vOficinaRegistral = '';
            	var vPlacaActual = '';
            	var vPlacaAnterior = '';
            	var vAnioFabricacion = '';
            	var vNroMotor = '';
            	var vTipoCarroceria = '';
            	var vMedidas = '';
            	var vCodMoneda = '';
                // Inmueble
                if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) {
                    vTipoBien = $('#cmbTipoBien').val() == undefined ? "" : $('#cmbTipoBien').val();
                    vFechaTransferencia = $('#txtFechaTransferencia').val() == undefined ? "" : $('#txtFechaTransferencia').val();
                    vObservaciones = $('#txtObservaciones').val() == undefined ? "" : $('#txtObservaciones').val();
                	//vColor = $('#txtColor1').val() == undefined ? "" : $('#txtColor1').val();
                	vCodDistrito = $('#ddlDistrito').val() == undefined ? "" : $('#ddlDistrito').val();
                	vValorBien = $('#txtValorBien').val() == undefined ? "" : $('#txtValorBien').val();
                	vPartidaRegistral = $('#txtPartidaRegistral').val() == undefined ? "" : $('#txtPartidaRegistral').val();
                	vOficinaRegistral = $('#txtOficinaRegistral').val() == undefined ? "" : $('#txtOficinaRegistral').val();
                	vFechaTransferencia = $('#txtFechaTransferencia').val() == undefined ? "" : $('#txtFechaTransferencia').val();
                	vCodMoneda = $('#ddlMonedaBien').val() == undefined ? "" : $('#ddlMonedaBien').val();
                }
                // Maquinaria
                else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) {
                    vTipoBien = $('#cmbTipoBien1').val() == undefined ? "" : fn_util_trim($('#cmbTipoBien1').val());
                    vFechaTransferencia = $('#txtFechaTransferencia1').val() == undefined ? "" : $('#txtFechaTransferencia1').val();
                    vObservaciones = $('#txtObservaciones1').val() == undefined ? "" : $('#txtObservaciones1').val();
                    vColor = $('#txtColor').val() == undefined ? "" : $('#txtColor').val();
                	vCodDistrito = $('#ddlDistrito1').val() == undefined ? "" : $('#ddlDistrito1').val();
                	vValorBien = $('#txtValorBien1').val() == undefined ? "" : $('#txtValorBien1').val();
                	vPlacaActual = $('#txtPlacaActual').val() == undefined ? "" : $('#txtPlacaActual').val();
                	vPlacaAnterior = $('#txtPlacaAnterior').val() == undefined ? "" : $('#txtPlacaAnterior').val();
                	vAnioFabricacion=$('#txtAnio').val() == "" ? 0 : $('#txtAnio').val();
                	vNroMotor=$('#txtNrMotor').val() == undefined ? "" : $('#txtNrMotor').val();
                	vTipoCarroceria=$('#txtCarroceria').val() == undefined ? "" : $('#txtCarroceria').val();
                	vMedidas=$('#txtMedidas').val() == undefined ? "" : $('#txtMedidas').val();
                	vCodMoneda = $('#ddlMonedaBien1').val() == undefined ? "" : $('#ddlMonedaBien1').val();
                    strFlag = '1';
                }
                // Otros
                else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) {
                    vTipoBien = $('#cmbTipoBien2').val() == undefined ? "" : $('#cmbTipoBien2').val();
                    vFechaTransferencia = $('#txtFechaTransferencia2').val() == undefined ? "" : $('#txtFechaTransferencia2').val();
                    vObservaciones = $('#txtObservaciones2').val() == undefined ? "" : $('#txtObservaciones2').val();
                	vColor = $('#txtColor2').val() == undefined ? "" : $('#txtColor2').val();
                	vCodDistrito = $('#ddlDistrito2').val() == undefined ? "" : $('#ddlDistrito2').val();
                	vValorBien = $('#txtValorBien2').val() == undefined ? "" : $('#txtValorBien2').val();
                	vPartidaRegistral = $('#txtPartidaRegistral2').val() == undefined ? "" : $('#txtPartidaRegistral2').val();
                	vOficinaRegistral = $('#txtOficinaRegistral2').val() == undefined ? "" : $('#txtOficinaRegistral2').val();
                    vCodMoneda = $('#ddlMonedaBien2').val() == undefined ? "" : $('#ddlMonedaBien2').val();
                }
                if (vFechaTransferencia == '') { vFechaTransferencia = '19000101'; }
                else { vFechaTransferencia = Fn_util_DateToString(vFechaTransferencia); }
                var vNumeroContrato = $('#hidNumeroContrato').val() == undefined ? "" : $('#hidNumeroContrato').val();
                var vSecFinanciamiento = $('#hidSecFinanciamiento').val() == undefined ? "" : $('#hidSecFinanciamiento').val();

                if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) {
                	 	var arrParametros = ["pNumeroContrato", vNumeroContrato,
                                         "pSecFinanciamiento", vSecFinanciamiento,
                                         "pCodigoTipoBien", vTipoBien,
                                         "pFechaTransferencia", vFechaTransferencia,
                                         "pObservaciones", vObservaciones,
                                         "pColor", "",
                	 		             "pCodDistrito",vCodDistrito,
                	 		             "pValorBien",fn_util_ValidaDecimal(vValorBien),
                	 		             "pPartidaRegistral",vPartidaRegistral,
                	 		             "pOficinaRegistral",vOficinaRegistral,
                	 		             "pCodMoneda",vCodMoneda,
                	 		             "pFlag", strFlag
                                    ];

                fn_util_AjaxWM("frmMantenimientoBienRegistro.aspx/GuardarBien",
                     arrParametros,
                     fn_MensajeYRedireccionar,
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
                } else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) {
                	var arrParametros2 = ["pNumeroContrato", vNumeroContrato,
                                         "pSecFinanciamiento", vSecFinanciamiento,
                                         "pCodigoTipoBien", vTipoBien,
                                         "pFechaTransferencia", vFechaTransferencia,
                                         "pObservaciones", vObservaciones,
                                         "pColor", vColor,
                	 		             "pCodDistrito",vCodDistrito,
                	 		             "pValorBien",fn_util_ValidaDecimal(vValorBien),
                		                 "pPlacaActual",vPlacaActual,
                		                 "pPlacaAnterior",vPlacaAnterior,
                		                 "pAnioFabricacion",vAnioFabricacion,
                		                 "pNroMotor",vNroMotor,
                		                 "pCarroceria",vTipoCarroceria,
                		                 "pMedidas",vMedidas,
                		                 "pCodMoneda",vCodMoneda,
                	 		             "pFlag", strFlag
                                    ];

                fn_util_AjaxWM("frmMantenimientoBienRegistro.aspx/GuardarMaquinaria",
                     arrParametros2,
                     fn_MensajeYRedireccionar,
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
                } else  if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) {
                	var arrParametros3 = ["pNumeroContrato", vNumeroContrato,
                                         "pSecFinanciamiento", vSecFinanciamiento,
                                         "pCodigoTipoBien", vTipoBien,
                                         "pFechaTransferencia", vFechaTransferencia,
                                         "pObservaciones", vObservaciones,
                                         "pColor", vColor,
                	 		             "pCodDistrito",vCodDistrito,
                	 		             "pValorBien",fn_util_ValidaDecimal(vValorBien),
                	 		             "pPartidaRegistral",vPartidaRegistral,
                	 		             "pOficinaRegistral",vOficinaRegistral,
                	 		             "pCodMoneda",vCodMoneda,
                	 		             "pFlag", strFlag
                                    ];

                fn_util_AjaxWM("frmMantenimientoBienRegistro.aspx/GuardarBien",
                     arrParametros3,
                     fn_MensajeYRedireccionar,
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
                }

            },
            "../util/images/question.gif",
            function() { },
            'Mantenimiento Bien'
         );
    }
}



//****************************************************************
// Función		:: 	fn_MensajeYRedireccionarSolicitud
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
//                  Luego lo redirecciona al formulario de búsquedas ("frmMantenimientoBienListado.aspx").
// Log			:: 	WCR - 18/06/2012
//****************************************************************
var fn_MensajeYRedireccionar = function() {
    parent.fn_unBlockUI();
    parent.fn_mdl_alert('Los datos se grabaron satisfactoriamente', function() { fn_util_redirect("frmMantenimientoBienLista.aspx"); });
};


function getParameter(paramName) {
    var searchString = window.location.search.substring(1), i, val, params = searchString.split("&");

    for (var i = 0; i < params.length; i++) {
        val = params[i].split("=");
        if (val[0] == paramName) {
            return unescape(val[1]);
        }
    }

    return null;
}

//****************************************************************
// Funcion		:: 	fn_InicializarCampos
// Descripción	::	
// Log			:: 	AEP - 17/07/2012
//****************************************************************
function fn_InicializarCampos() {
// Maquinaria, vehiculos
	$('#txtEstadoBien1').attr('disabled', 'disabled');
    $('#txtEstadoBien1').validText({ type: 'comment'});
	$('#txtCantidad1').attr('disabled', 'disabled');
    $('#txtCantidad1').validText({ type: 'number'});
	$('#txtDescripcionBien1').attr('disabled', 'disabled');
    $('#txtDescripcionBien1').validText({ type: 'comment'});
	$('#txtUbicacion1').attr('disabled', 'disabled');
    $('#txtUbicacion1').validText({ type: 'comment'});
	$('#txtUso1').attr('disabled', 'disabled');
    $('#txtUso1').validText({ type: 'comment'});
	$('#ddlDepartamento1').attr('disabled', 'disabled');
    $('#ddlProvincia1').attr('disabled', 'disabled');
	$('#txtRazonSocial').validText({ type: 'comment', length: 100 });
	$('#txtMonedaBien1').attr('disabled', 'disabled');
    $('#txtMonedaBien1').validText({ type: 'comment'});
	$('#txtValorBien1').validNumber({value:'',decimals:2,length:15});
	$('#txtPlacaActual').validText({ type: 'alphanumeric',length:10});
	//$('#txtPlacaAnterior').attr('disabled', 'disabled');
    $('#txtPlacaAnterior').validText({ type: 'alphanumeric',length:10});
	$('#txtFechaTransferencia1').validText({ type: 'date',length:10});
	$('#txtAnio').validText({ type: 'number',length:4});
	$('#txtNroSerie').attr('disabled', 'disabled');
    $('#txtNroSerie').validText({ type: 'alphanumeric'});
	$('#txtNrMotor').validText({ type: 'alphanumeric',length:20});
	$('#txtMarca').attr('disabled', 'disabled');
    $('#txtMarca').validText({ type: 'comment',length:20});
	$('#txtModelo').attr('disabled', 'disabled');
    $('#txtModelo').validText({ type: 'comment',length:20});
	$('#txtColor').validText({ type: 'comment',length:50});
	$('#txtCarroceria').validText({ type: 'comment',length:20});
	$('#txtMedidas').validText({ type: 'comment',length:100});
	$('#txtObservaciones1').validText({ type: 'comment',length:500});
//======================================================================================	
	// Bien
	$('#txtDescripcionDemanda').attr('disabled', 'disabled');
    $('#txtDescripcionDemanda').validText({ type: 'comment'});
	$('#txtUbicacion').attr('disabled', 'disabled');
    $('#txtUbicacion').validText({ type: 'comment'});
	$('#txtDescripcionBien').attr('disabled', 'disabled');
    $('#txtDescripcionBien').validText({ type: 'comment'});
	$('#txtUso').attr('disabled', 'disabled');
    $('#txtUso').validText({ type: 'comment'});
	$('#txtEstadoBien').attr('disabled', 'disabled');
    $('#txtEstadoBien').validText({ type: 'comment'});
	$('#txtCantidad').attr('disabled', 'disabled');
    $('#txtCantidad').validText({ type: 'number'});
	$('#ddlDepartamento').attr('disabled', 'disabled');
    $('#ddlProvincia').attr('disabled', 'disabled');
	$('#txtMonedaBien').attr('disabled', 'disabled');
    $('#txtMonedaBien').validText({ type: 'comment'});
	$('#txtValorBien').validNumber({value:'',decimals:2,length:15});
	$('#txtFechaTransferencia').validText({ type: 'date',length:10});
	//$('#txtColor1').validText({ type: 'comment',length:50});
	$('#txtPartidaRegistral').validText({ type: 'number',length:10});
	$('#txtOficinaRegistral').validText({ type: 'comment',length:50});
	$('#txtObservaciones').validText({ type: 'comment',length:500});
	
// ======================otros==========================================	
	$('#txtCantidad2').attr('disabled', 'disabled');
    $('#txtCantidad2').validText({ type: 'number'});
	$('#txtEstadoBien2').attr('disabled', 'disabled');
    $('#txtEstadoBien2').validText({ type: 'comment'});
	$('#txtDescripcionBien2').attr('disabled', 'disabled');
    $('#txtDescripcionBien2').validText({ type: 'comment'});
	$('#txtUbicacion2').attr('disabled', 'disabled');
    $('#txtUbicacion2').validText({ type: 'comment'});
	$('#txtUso2').attr('disabled', 'disabled');
    $('#txtUso2').validText({ type: 'comment'});
	$('#ddlDepartamento2').attr('disabled', 'disabled');
    $('#ddlProvincia2').attr('disabled', 'disabled');
	$('#txtMonedaBien2').attr('disabled', 'disabled');
    $('#txtMonedaBien2').validText({ type: 'comment'});
	$('#txtValorBien2').validNumber({value:'',decimals:2,length:15});
	$('#txtFechaTransferencia2').validText({ type: 'date',length:10});
	$('#txtMarca2').attr('disabled', 'disabled');
    $('#txtMarca2').validText({ type: 'comment',length:20});
	$('#txtModelo2').attr('disabled', 'disabled');
    $('#txtModelo2').validText({ type: 'comment',length:20});
	$('#txtColor2').validText({ type: 'comment',length:50});
	$('#txtPartidaRegistral2').validText({ type: 'number',length:10});
	$('#txtOficinaRegistral2').validText({ type: 'number',length:50});
	$('#txtObservaciones2').validText({ type: 'comment',length:500});
}

//****************************************************************
// Funcion		:: 	fn_SeteaUbigeo
// Descripción	::	Setear Ubigeo
// Log			:: 	AEP - 17/07/2012
//****************************************************************
function fn_SeteaUbigeo() {
    //Carga Departamento
    var strDepartamento1 = $("#hidCodDepartamento1").val();
    $("#ddlDepartamento1").val(strDepartamento1);

    //Carga Provincia
    fn_cargaComboProvincia(strDepartamento1);
     strProvincia1 = $("#hidCodProvincia1").val();
    $("#ddlProvincia1").val(strProvincia1);

    //Carga Distrito
    fn_cargaComboDistrito(strDepartamento1, strProvincia1);
    $("#ddlDistrito1").val($("#hidCodDistrito1").val());
}


//****************************************************************
// Funcion		:: 	fn_SeteaUbigeoBien
// Descripción	::	Setear Ubigeo del tipo de bien "Bien"
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_SeteaUbigeoBien() {
    //Carga Departamento
    var strDepartamento = $("#hidCodDepartamento").val();
    $("#ddlDepartamento").val(strDepartamento);

    //Carga Provincia
    fn_cargaComboProvinciaBien(strDepartamento);
    strProvincia = $("#hidCodProvincia").val();
    $("#ddlProvincia").val(strProvincia);

    //Carga Distrito
    fn_cargaComboDistritoBien(strDepartamento, strProvincia);
    $("#ddlDistrito").val($("#hidCodDistrito").val());
}

//****************************************************************
// Funcion		:: 	fn_SeteaUbigeoOtro
// Descripción	::	Setear Ubigeo del tipo de bien "otros"
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_SeteaUbigeoOtro() {
    //Carga Departamento
    var strDepartamento2 = $("#hidCodDepartamento2").val();
    $("#ddlDepartamento2").val(strDepartamento2);

    //Carga Provincia
    fn_cargaComboProvinciaOtro(strDepartamento2);
    strProvincia2 = $("#hidCodProvincia2").val();
    $("#ddlProvincia2").val(strProvincia2);

    //Carga Distrito
    fn_cargaComboDistritoOtro(strDepartamento2, strProvincia2);
    $("#ddlDistrito2").val($("#hidCodDistrito2").val());
}
//****************************************************************
// Funcion		:: 	fn_cargaComboDistrito
// Descripción	::	
// Log			:: 	AEP - 17/07/2012
//****************************************************************
function fn_cargaComboDistrito(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistrito1').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}

//****************************************************************
// Funcion		:: 	fn_cargaComboDistritoBien
// Descripción	::	
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_cargaComboDistritoBien(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistrito').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}
//****************************************************************
// Funcion		:: 	fn_cargaComboDistritoOtro
// Descripción	::	
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_cargaComboDistritoOtro(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistrito2').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistrito
// Descripción	::	
// Log			:: 	AEP - 17/07/2012
//****************************************************************
function fn_LimpiaComboDistrito() {
    $('#ddlDistrito1').empty();
    $("#ddlDistrito1").append('<option value="0">[-Seleccione-]</option>');
}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistritoBien
// Descripción	::	
// Log			:: 	AEP - 18/07/2012

//****************************************************************

function fn_LimpiaComboDistritoBien() {
    $('#ddlDistrito').empty();
    $("#ddlDistrito").append('<option value="0">[-Seleccione-]</option>');
}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistritoOtro
// Descripción	::	
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_LimpiaComboDistritoOtro() {
    $('#ddlDistrito2').empty();
    $("#ddlDistrito2").append('<option value="0">[-Seleccione-]</option>');

}
//****************************************************************
// Funcion		:: 	fn_cargaComboProvincia
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 17/07/2012
//****************************************************************
function fn_cargaComboProvincia(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvincia1').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistrito();

}

//****************************************************************
// Funcion		:: 	fn_cargaComboProvinciaBien
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_cargaComboProvinciaBien(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvincia').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoBien();

}

//****************************************************************
// Funcion		:: 	fn_cargaComboProvinciaOtro
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_cargaComboProvinciaOtro(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvincia2').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoOtro();

}