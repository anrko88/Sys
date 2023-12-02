var DestinoCredito_Inmueble = ["002"];
var DestinoCredito_Maquinaria = ["003", "004", "005"];
var DestinoCredito_Otros = ["007", "008"];
var DestinoCredito_Vehiculo = ["006"];
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	AEP - 03/10/2012
//****************************************************************
$(document).ready(function() {
fn_doResize();
    
    fn_configurar_PanelesBienes();
	//fn_cargarTipoBien();
	fn_SeteaUbigeoInmueble();
	fn_SeteaUbigeoMaquinaria();
	fn_SeteaUbigeoSistemasOtros();
	fn_SeteaUbigeoVehiculo();
	fn_InicializarCampos();
	
	
      //On load Page (siempre al final)
    fn_onLoadPage();
});

//****************************************************************
// Funcion		:: 	fn_SeteaUbigeoInmueble
// Descripción	::	Setear Ubigeo del tipo de bien "Inmueble"
// Log			:: 	AEP - 03/10/2012
//****************************************************************
function fn_SeteaUbigeoInmueble() {
    //Carga Departamento
    var strDepartamento =fn_util_trim($("#hidCodDepartamentoInmueble").val());
    $("#ddlDepartamentoInmueble").val(strDepartamento);

    //Carga Provincia
    fn_cargaComboProvinciaInmueble(strDepartamento);
    strProvincia = fn_util_trim($("#hidCodProvinciaInmueble").val());
    $("#ddlProvinciaInmueble").val(strProvincia);

    //Carga Distrito
    fn_cargaComboDistritoInmueble(strDepartamento, strProvincia);
    $("#ddlDistritoInmueble").val(fn_util_trim($("#hidCodDistritoInmueble").val()));
}
//****************************************************************
// Funcion		:: 	fn_SeteaUbigeoInmueble
// Descripción	::	Setear Ubigeo del tipo de bien "Inmueble"
// Log			:: 	AEP - 28/09/2012
//****************************************************************
function fn_SeteaUbigeoMaquinaria() {
    //Carga Departamento
    var strDepartamentoM =fn_util_trim($("#hidDepartamentoMaquinaria").val());
    $("#cmbDepartamentoMaquinaria").val(strDepartamentoM);

    //Carga Provincia
    fn_cargaComboProvinciaMaquinaria(strDepartamentoM);
   var strProvinciaM = fn_util_trim($("#hidProvinciaMaquinaria").val());
    $("#cmbProvinciaMaquinaria").val(strProvinciaM);

    //Carga Distrito
    fn_cargaComboDistritoMaquinaria(strDepartamentoM, strProvinciaM);
    $("#cmbDistritoMaquinaria").val(fn_util_trim($("#hidDistritoMaquinaria").val()));
}

//****************************************************************
// Funcion		:: 	fn_SeteaUbigeoVehiculo
// Descripción	::	Setear Ubigeo del tipo de bien "Vehiculo"
// Log			:: 	AEP - 04/10/2012
//****************************************************************
function fn_SeteaUbigeoVehiculo() {
    //Carga Departamento
    var strDepartamentoV =fn_util_trim($("#hidDepartamentoVehiculo").val());
    $("#ddlDepartamentoVehiculo").val(strDepartamentoV);

    //Carga Provincia
    fn_cargaComboProvinciaVehiculo(strDepartamentoV);
   var strProvinciaV = fn_util_trim($("#hidProvinciaVehiculo").val());
    $("#ddlProvinciaVehiculo").val(strProvinciaV);

    //Carga Distrito
    fn_cargaComboDistritoVehiculo(strDepartamentoV, strProvinciaV);
    $("#ddlDistritoVehiculo").val(fn_util_trim($("#hidDistritoVehiculo").val()));
}

//****************************************************************
// Funcion		:: 	fn_SeteaUbigeoSistemasOtros
// Descripción	::	Setear Ubigeo del tipo de bien "Sistemas y Otros"
// Log			:: 	AEP - 04/10/2012
//****************************************************************
function fn_SeteaUbigeoSistemasOtros() {
    //Carga Departamento
    var strDepartamentoO =fn_util_trim($("#hidDepartamentoOtros").val());
    $("#ddlDepartamentoDatosOtros").val(strDepartamentoO);

    //Carga Provincia
    fn_cargaComboProvinciaOtros(strDepartamentoO);
   var strProvinciaO = fn_util_trim($("#hidProvinciaOtros").val());
    $("#ddlProvinciaDatosOtros").val(strProvinciaO);

    //Carga Distrito
    fn_cargaComboDistritoOtros(strDepartamentoO, strProvinciaO);
    $("#ddlDistritoDatosOtros").val(fn_util_trim($("#hidDistritoOtros").val()));
}

//****************************************************************
// Funcion		:: 	fn_InicializarCampos
// Descripción	::	Realiza la acción que contiene este método al 
//              ::  cargar la pantalla
// Log			:: 	AEP - 04/10/2012
//****************************************************************

function fn_InicializarCampos()
{
	var strEstadoBien =fn_util_trim($("#hidEstadoBien").val());
    $("#ddlEstadoBien").val(strEstadoBien);
		var strEstadoBienM =fn_util_trim($("#hidEstadoBienMaquinaria").val());
    $("#cmbEstadobienMaquina").val(strEstadoBienM);
	var strEstadoBienV =fn_util_trim($("#hidEstadoBienVehiculo").val());
    $("#ddlEstadoBienVehiculo").val(strEstadoBienV);
	var strEstadoBienO =fn_util_trim($("#hidEstadoBienOtros").val());
    $("#ddlEstadoDatosOtros").val(strEstadoBienO);
	
	//Inmuebles
	$('#txtUbicacionInmueble').validText({ type: 'comment',length:100});
    $('#txtDescripcionInmueble').validText({ type: 'comment',length:100});
	$('#txtDescripcionInmueble').maxLength(100);
    $('#ddlEstadoBien').validText({ type: 'comment'});
	$('#txtPartidaRegistralInmueble').validText({ type: 'number',length:10});
    $('#txtOficinaRegistralInmueble').validText({ type: 'comment',length:50});
	$('#txtOficinaRegistralInmueble').maxLength(50);
    $('#txtCantidadInmueble').validText({ type: 'number'});
    $('#txtCodigoPredioInmueble').validText({ type: 'comment',length:40});
	//Maquinarias
	
	    $('#txtSerieMotorMaquina').validText({ type: 'alphanumeric', length: 20 });
        $('#txtNumeroMotorMaquina').validText({ type: 'alphanumeric', length: 20 });
        $('#txtFabricacionMaquina').validText({ type: 'number', length: 4 });
        $('#txtMarcaMaquina').maxLength(20);
        $('#txtModeloMaquina').maxLength(20);
        $('#txtTipoCarroceriaMaquina').maxLength(20);
        $('#txtDescripcionAutoMaquina').maxLength(100);
        $('#txtValorMaquina').maxLength(250);
        $('#txtPlacaMaquina').validText({ type: 'alphanumeric', length: 10 });
        $('#txtMedidasMaquina').maxLength(100);
        $('#txtCantidadMaquina').validText({ type: 'number', length: 3 });
        $('#txtUsoBienMaquina').maxLength(100);
        $('#txtUbicacionBienMaquina').maxLength(100);

    // Otros
	
	$('#txtUsoDatosOtros').maxLength(100);
    $('#txtUbicacionDatosOtros').maxLength(100);
    $('#txtDescripcionDatosOtros').maxLength(100);
    $('#txtMarcaDatosOtros').maxLength(20);
    $('#txtModeloDatosOtros').maxLength(20);
    $('#txtCantidadDatosOtros').validText({ type: 'number', length: 3 });
    $('#txtPartidaRegistralDatosOtros').validText({ type: 'number', length: 10 });
    $('#txtPartidaRegistralDatosOtros').maxLength(10);
    $('#txtOficinaRegistralDatosOtros').maxLength(50);
	
	//Vehiculos
	
	$('#txtSerieVehiculo').validText({ type: 'alphanumeric', length: 20 });
    $('#txtMotorVehiculo').validText({ type: 'alphanumeric', length: 20 });
    $('#txtAnioVehiculo').validText({ type: 'number', length: 4 });
    $('#txtMarcaVehiculo').maxLength(20);
    $('#txtModeloVehiculo').maxLength(20);
    $('#txtCarroceriaVehiculo').maxLength(20);
    $('#txtDescripcionVehiculo').maxLength(100);
    $('#txtPlacaVehiculo').validText({ type: 'alphanumeric', length: 10 });
    $('#txtMedidasVehiculo').maxLength(100);
    $('#txtCantidadVehiculo').validText({ type: 'number', length: 3 });
    $('#txtUsoVehiculo').maxLength(100);
    $('#txtUbicacionVehiculo').maxLength(100);
	
	fn_SeteaCamposObligatorios();
	
	
}

//****************************************************************
// Funcion		:: 	fn_SeteaCamposObligatorios
// Descripción	::	Indica que campos son obligatorios al momento de registrar 
// Log			:: 	AEP - 11/10/2012
//****************************************************************
function fn_SeteaCamposObligatorios() {
	
	//Inmuebles
    fn_util_SeteaObligatorio($("#ddlTipoBien"), "select");	
	fn_util_SeteaObligatorio($("#ddlEstadoBien"), "select");	
	fn_util_SeteaObligatorio($("#ddlDepartamentoInmueble"), "select");	
	fn_util_SeteaObligatorio($("#ddlProvinciaInmueble"), "select");	
	fn_util_SeteaObligatorio($("#txtCantidadInmueble"), "input");
	fn_util_SeteaObligatorio($("#txtDescripcionInmueble"), "input");
	fn_util_SeteaObligatorio($("#txtUbicacionInmueble"), "input");
	fn_util_SeteaObligatorio($("#txtUsoInmueble"), "input");
	fn_util_SeteaObligatorio($("#ddlMonedaBien"), "select");
	//Maquinarias
	fn_util_SeteaObligatorio($("#txtMarcaMaquina"), "input");
    fn_util_SeteaObligatorio($("#txtDescripcionAutoMaquina"), "input");
    fn_util_SeteaObligatorio($("#cmbEstadobienMaquina"), "select");
    fn_util_SeteaObligatorio($("#txtUsoMaquina"), "input");
    fn_util_SeteaObligatorio($("#txtUbicacionBienMaquina"), "input");
    fn_util_SeteaObligatorio($("#txtCantidadMaquina"), "input");
    fn_util_SeteaObligatorio($("#cmbDepartamentoMaquinaria"), "select");
    fn_util_SeteaObligatorio($("#cmbProvinciaMaquinaria"), "select");
	//Sistemas y/u Otros
	fn_util_SeteaObligatorio($("#txtUsoDatosOtros"), "input");
    fn_util_SeteaObligatorio($("#txtUbicacionDatosOtros"), "input");
    fn_util_SeteaObligatorio($("#txtDescripcionDatosOtros"), "input");
    fn_util_SeteaObligatorio($("#txtMarcaDatosOtros"), "input");
    fn_util_SeteaObligatorio($("#txtCantidadDatosOtros"), "input");
    fn_util_SeteaObligatorio($("#ddlEstadoDatosOtros"), "select");
    fn_util_SeteaObligatorio($("#ddlDepartamentoDatosOtros"), "select");
    fn_util_SeteaObligatorio($("#ddlProvinciaDatosOtros"), "select");
	//Vehiculo
	fn_util_SeteaObligatorio($("#txtMarcaVehiculo"), "input");
    fn_util_SeteaObligatorio($("#txtDescripcionVehiculo"), "input");
    fn_util_SeteaObligatorio($("#ddlEstadoBienVehiculo"), "select");
    fn_util_SeteaObligatorio($("#txtUsoVehiculo"), "input");
    fn_util_SeteaObligatorio($("#txtUbicacionVehiculo"), "input");
    fn_util_SeteaObligatorio($("#txtCantidadVehiculo"), "input");
    fn_util_SeteaObligatorio($("#ddlDepartamentoVehiculo"), "select");
    fn_util_SeteaObligatorio($("#ddlProvinciaVehiculo"), "select");
}


//************************************************************
// Función		:: 	fn_cargarTipoBien
// Descripcion 	:: 	Carga el combo de tipo de bien
// Log			:: 	AEP - 28/09/2012
//************************************************************
function fn_cargarTipoBien() {
    var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL104", "pstrCodigoGenerico", $("#hidCodClasificacion").val()];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            // Inmueble
        	if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) { $('#ddlTipoBien').html(arrResultado[1]); $('#ddlTipoBien').val($('#hidCodTipoBien').val()); }
            // Maquinaria
            else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) { $('#cmbtipob').html(arrResultado[1]); $('ddlTipoBienMaquinaria').val($('#hidTipoBienMaquinaria').val()); }
	        // Vehivulo
	        else if (DestinoCredito_Vehiculo.indexOf($("#hidCodClasificacion").val()) != -1) { $('#ddlTipoBienVehiculo').html(arrResultado[1]); $('').val($('#ddlTipoBienVehiculo').val('#hidTipoBienVehiculo').val()); }
            // Otros
            else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) { $('#cmbTipoBien').html(arrResultado[1]); $('').val($('#hidCodTipoBien').val()); }
     	
        	
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
}

//************************************************************
// Función		:: 	fn_configurar_PanelesBienes
// Descripcion 	:: 	Configura las distintas ventanas de mantenimiento de los bienes
// Log			:: 	AEP - 03/10/2012
//************************************************************
function fn_configurar_PanelesBienes() {

    $("#dv_datos_otros").hide();
	$("#dv_datos_vehiculo").hide();
	$("#dv_datos_inmueble").hide();
	$("#dv_datos_maquinaria").hide();
	$("#dv_contenedor_Maquinaria").hide();
	$("#dv_contenedor_Vehiculo").hide();

    // Inmueble
    if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_datos_inmueble").show();  }
    // Maquinaria
    else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_datos_maquinaria").show(); $("#dv_contenedor_Maquinaria").show();}
	// Vehivulo
	else if (DestinoCredito_Vehiculo.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_datos_vehiculo").show(); $("#dv_contenedor_Vehiculo").show(); }
    // Otros
    else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) { $("#dv_datos_otros").show(); }
	
	fn_doResize();
}

//****************************************************************
// Funcion		:: 	fn_cargaComboProvinciaInmueble
// Descripción	::	Cargar el combo provincia 
// Log			:: 	AEP - 28/09/2012
//****************************************************************
function fn_cargaComboProvinciaInmueble(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvinciaInmueble').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoInmueble();
	//fn_doResize();

}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistritoInmueble
// Descripción	::	
// Log			:: 	AEP - 28/09/2012

//****************************************************************

function fn_LimpiaComboDistritoInmueble() {
    $('#ddlDistritoInmueble').empty();
    $("#ddlDistritoInmueble").append('<option value="0">[-Seleccione-]</option>');
}

//****************************************************************
// Funcion		:: 	fn_cargaComboDistritoInmueble
// Descripción	::	
// Log			:: 	AEP - 18/07/2012
//****************************************************************
function fn_cargaComboDistritoInmueble(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistritoInmueble').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
//fn_doResize();
}

//****************************************************************
// Funcion		:: 	fn_cargaComboProvinciaMaquinaria
// Descripción	::	Cargar el combo provincia 
// Log			:: 	AEP - 02/10/2012
//****************************************************************
function fn_cargaComboProvinciaMaquinaria(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbProvinciaMaquinaria').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoMaquinaria();
	//fn_doResize();

}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistritoMaquinaria
// Descripción	::	
// Log			:: 	AEP - 02/10/2012

//****************************************************************

function fn_LimpiaComboDistritoMaquinaria() {
    $('#cmbDistritoMaquinaria').empty();
    $("#cmbDistritoMaquinaria").append('<option value="0">[-Seleccione-]</option>');
}

//****************************************************************
// Funcion		:: 	fn_cargaComboDistritoMaquinaria
// Descripción	::	
// Log			:: 	AEP - 02/10/2012
//****************************************************************
function fn_cargaComboDistritoMaquinaria(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbDistritoMaquinaria').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
//fn_doResize();
}



//****************************************************************
// Funcion		:: 	fn_cargaComboProvinciaVehiculo
// Descripción	::	Cargar el combo provincia 
// Log			:: 	AEP - 04/10/2012
//****************************************************************
function fn_cargaComboProvinciaVehiculo(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvinciaVehiculo').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoVehiculo();
	//fn_doResize();

}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistritoVehiculo
// Descripción	::	
// Log			:: 	AEP - 04/10/2012

//****************************************************************

function fn_LimpiaComboDistritoVehiculo() {
    $('#ddlDistritoMaquinaria').empty();
    $("#ddlDistritoMaquinaria").append('<option value="0">[-Seleccione-]</option>');
}

//****************************************************************
// Funcion		:: 	fn_cargaComboDistritoVehiculo
// Descripción	::	
// Log			:: 	AEP - 04/10/2012
//****************************************************************
function fn_cargaComboDistritoVehiculo(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistritoVehiculo').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
//fn_doResize();
}





//****************************************************************
// Funcion		:: 	fn_cargaComboProvinciaOtros
// Descripción	::	Cargar el combo provincia 
// Log			:: 	AEP - 04/10/2012
//****************************************************************
function fn_cargaComboProvinciaOtros(valor) {
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlProvinciaDatosOtros').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoOtros();
	//fn_doResize();

}
//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistritoOtros
// Descripción	::	
// Log			:: 	AEP - 04/10/2012

//****************************************************************

function fn_LimpiaComboDistritoOtros() {
    $('#ddlDistritoDatosOtros').empty();
    $("#ddlDistritoDatosOtros").append('<option value="0">[-Seleccione-]</option>');
}

//****************************************************************
// Funcion		:: 	fn_cargaComboDistritoOtros
// Descripción	::	
// Log			:: 	AEP - 02/10/2012
//****************************************************************
function fn_cargaComboDistritoOtros(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlDistritoDatosOtros').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
//fn_doResize();
}
//****************************************************************
// Función		:: 	fn_GuardarBienes
// Descripción	::	Guarda los datos ingresados de un nuevo bien (operación nuevo).
// Log			:: 	AEP - 04/10/2012
//****************************************************************
function fn_GuardarBienes() {
   fn_blockUI();
    
    var strError = new StringBuilderEx();
    fn_Validacion(strError);
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    	fn_SeteaCamposObligatorios();
    	  }
    else {
        if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) {
  
     
  
	   var arrParametros = [
                             "strNroContrato",              $("#hidNumeroContrato").val(),
                             "strTipoRubroFinanciamiento",  $("#hidCodClasificacion").val(),
                             "strUsoInmueble",              $('#txtUsoInmueble').val(),
                             "strUbicacionInmueble",        $('#txtUbicacionInmueble').val(),
                             "strDescripcionInmueble",      $("#txtDescripcionInmueble").val(),
                             "strEstadoBienInmueble",       $("#ddlEstadoBien").val(),
                             "intCantidadInmueble",         $("#txtCantidadInmueble").val(),
                             "strDepartamentoInmueble",     $("#ddlDepartamentoInmueble").val(),
                             "strProvinciaInmueble",        $("#ddlProvinciaInmueble").val(),
                             "strDistritoInmueble",         $("#ddlDistritoInmueble").val(),
                             "strPartidaRegistralInmueble", $("#txtPartidaRegistralInmueble").val(),
                             "strOficinaRegistralInmueble", $("#txtOficinaRegistralInmueble").val(),
        	                 "strCodigoPredioInmueble",     $("#txtCodigoPredioInmueble").val(),
        	                 "intFlagOrigen", "2",
        	                 "strCodTipoBien", $("#hidCodTipoBien").val() //IBK - RPH
                             ];

        fn_util_AjaxWM("../Formalizacion/frmContratoRegistro.aspx/GuardarBienInmuebleNuevo",
                       arrParametros,
                       function() {
                        fn_ListaInmuebles();
                       	//parent.fn_mdl_alert('Los datos se registraron satisfactoriamente', function() { });
                       	fn_util_MuestraLogPage("Se registró correctamente el bien", "I");
                       },
                       function(result) {
                           parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                       });
   }
        
    // Maquinaria
    else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) {
        	// if (EsValidoMaquina()) {

        	var arrParametroValidacionM = ["strSerie", $("#txtSerieMotorMaquina").val(),
        		                           "strNumContrato", $("#hidNumeroContrato").val()];
        	
        	fn_util_AjaxSyncWM("frmMantRegistroBienesContrato.aspx/ValidarDatosMaquinaria",
        		arrParametroValidacionM,
        		function(result2) {
        			$("#hidNroSerie").val(result2);
//                       	alert(result2);
        		},
        		function(result) {
        			parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
        		});

        	if ($("#hidNroSerie").val() != "") {

        		parent.fn_mdl_mensajeIco("No se puede registrar porque se repite el Nro. de Serie", "util/images/warning.gif", "GUARDAR");

        	} else {

        		var arrParametrosM = [
        			"strNroContrato", $("#hidNumeroContrato").val(),
        			"strCodProveedor", "",
        			"strTipoRubroFinanciamiento", $("#hidCodClasificacion").val(),
        			"strSerieMotorMaquina", $("#txtSerieMotorMaquina").val(),
        			"strNumeroMotorMaquina", $("#txtNumeroMotorMaquina").val(),
        			"strFabricacionMaquina", $('#txtFabricacionMaquina').val(),
        			"strMarcaMaquina", $("#txtMarcaMaquina").val(),
        			"strModeloMaquina", $("#txtModeloMaquina").val(),
        			"strTipoCarroceriaMaquina", fn_util_trim($("#txtTipoCarroceriaMaquina").val()),
        			"strDescripcionAutoMaquina", $("#txtDescripcionAutoMaquina").val(),
        			"strEstadobienMaquina", $("#cmbEstadobienMaquina").val(),
        			"strPlacaMaquina", $("#txtPlacaMaquina").val(),
        			"strMedidasMaquina", $("#txtMedidasMaquina").val(),
        			"intCantidadMaquina", $("#txtCantidadMaquina").val(),
        			"strUsoBienMaquina", $("#txtUsoMaquina").val(),
        			"strUbicacionBienMaquina", $("#txtUbicacionBienMaquina").val(),
        			"strDepartamentoMaquinaria", $("#cmbDepartamentoMaquinaria").val(),
        			"strProvinciaMaquinaria", $("#cmbProvinciaMaquinaria").val(),
        			"strDistritoMaquinaria", $("#cmbDistritoMaquinaria").val(),
        			"intFlagOrigen", "2",
        			"strColor", "", //IBK - RPH
        			"strCodTipoBien", $("#hidCodTipoBien").val() //IBK - RPH
        		];
        		fn_util_AjaxSyncWM("../Formalizacion/frmContratoRegistro.aspx/GuardarMaquinaNuevo",
        			arrParametrosM,
        			function() {
        				fn_ListaMaquinas();
        				//parent.fn_mdl_alert('Los datos se registraron satisfactoriamente', function() { });
        				fn_util_MuestraLogPage("Se registró correctamente el bien", "I");
        			},
        			function(result) {
        				parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
        			});
        		//}
        	}
        }
    	// Vehivulo
	else if (DestinoCredito_Vehiculo.indexOf($("#hidCodClasificacion").val()) != -1) {
        	       	// if (EsValidoMaquina()) {

        	var arrParametroValidacion = ["strSerie",$("#txtSerieVehiculo").val(),
        	                              "strMotor",$("#txtMotorVehiculo").val(),
        		                          "strPlaca",$("#txtPlacaVehiculo").val(),
        	                              "strPlacaAntigua",""];	
        	fn_util_AjaxSyncWM("frmMantRegistroBienesContrato.aspx/ValidarDatosVehiculo",
                       arrParametroValidacion,
                       function(result2) {
                       	$("#hidMensaje").val(result2);
//                       	alert(result2);
                       },
                       function(result) {
                           parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                       });
        	
        	        if ($("#hidMensaje").val()!="") {
        	         
        	         parent.fn_mdl_mensajeIco("No se puede registrar porque se repiten los siguientes datos " +  $("#hidMensaje").val(), "util/images/warning.gif", "GUARDAR");

        	        }else {
        	        
        	        var arrParametrosV = [
                             "strNroContrato",               $("#hidNumeroContrato").val(),
                             "strCodProveedor",              $("#txtUsoVehiculo").val(),
                             "strTipoRubroFinanciamiento",   $("#hidCodClasificacion").val(),
                             "strSerieMotorMaquina",         $("#txtSerieVehiculo").val(),
                             "strNumeroMotorMaquina",        $("#txtMotorVehiculo").val(),
                             "strFabricacionMaquina",        $('#txtAnioVehiculo').val(),
                             "strMarcaMaquina",              $("#txtMarcaVehiculo").val(),
                             "strModeloMaquina",             $("#txtModeloVehiculo").val(),
                             "strTipoCarroceriaMaquina",     fn_util_trim($("#txtCarroceriaVehiculo").val()),
                             "strDescripcionAutoMaquina",    $("#txtDescripcionVehiculo").val(),
                             "strEstadobienMaquina",         $("#ddlEstadoBienVehiculo").val(),
                             "strPlacaMaquina",              $("#txtPlacaVehiculo").val(),
                             "strMedidasMaquina",            $("#txtMedidasVehiculo").val(),
                             "intCantidadMaquina",           $("#txtCantidadVehiculo").val(),
                             "strUsoBienMaquina",            $("#txtUsoVehiculo").val(),
                             "strUbicacionBienMaquina",      $("#txtUbicacionVehiculo").val(),
                             "strDepartamentoMaquinaria",    $("#ddlDepartamentoVehiculo").val(),
                             "strProvinciaMaquinaria",       $("#ddlProvinciaVehiculo").val(),
                             "strDistritoMaquinaria",        $("#ddlDistritoVehiculo").val(),
        	                 "intFlagOrigen", "2",
        	                 "strColor", $("#txtColorVehiculo").val(), //IBK - RPH
        	                 "strCodTipoBien", $("#hidCodTipoBien").val() //IBK - RPH
                            ];
        fn_util_AjaxSyncWM("../Formalizacion/frmContratoRegistro.aspx/GuardarMaquinaNuevo",
                       arrParametrosV,
                       function() {
                       	fn_ListaVehiculo();
                       //	parent.fn_mdl_alert('Los datos se registraron satisfactoriamente', function() { });
                       	fn_util_MuestraLogPage("Se registró correctamente el bien", "I");
                       },
                       function(result) {
                           parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                       });
        	        }

        	
       //}
        }
    // Otros
    else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) {
        	
       // 	if (EsValidoDatosOtros()) {
        var arrParametrosO = ["strNroContrato",               $("#hidNumeroContrato").val(),
                            "strTipoRubroFinanciamiento",    $("#hidCodClasificacion").val(),
                            "strUsoDatosOtros",              $("#txtUsoDatosOtros").val(),
                            "strUbicacionDatosOtros",        $("#txtUbicacionDatosOtros").val(),
                            "strDescripcionDatosOtros",      $("#txtDescripcionDatosOtros").val(),
                            "strMarcaDatosOtros",            $("#txtMarcaDatosOtros").val(),
                            "strModeloDatosOtros",           $("#txtModeloDatosOtros").val(),
                            "intCantidadDatosOtros",         $("#txtCantidadDatosOtros").val(),
                            "strPartidaRegistralDatosOtros", $("#txtPartidaRegistralDatosOtros").val(),
                            "strOficinaRegistralDatosOtros", $("#txtOficinaRegistralDatosOtros").val(),
                            "strDepartamentoDatosOtros",     $("#ddlDepartamentoDatosOtros").val(),
                            "strProvinciaDatosOtros",        $("#ddlProvinciaDatosOtros").val(),
                            "strDistritoDatosOtros",         $("#ddlDistritoDatosOtros").val(),
                            "strEstadoBienInmueble",         $("#ddlEstadoDatosOtros").val(),
        	                "intFlagOrigen", "2",
        	                "strCodTipoBien", $("#hidCodTipoBien").val() //IBK - RPH
                            ];

        fn_util_AjaxWM("../Formalizacion/frmContratoRegistro.aspx/GuardarDatosOtrosNuevo",
            arrParametrosO,
            function() {
            	fn_ListaDatosOtros();
                //parent.fn_mdl_alert('Los datos se registraron satisfactoriamente', function() { });
            	fn_util_MuestraLogPage("Se registró correctamente el bien", "I");
            	
            },
            function(result) {
                parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
            });

            }
    }
    
 fn_unBlockUI();
fn_SeteaCamposObligatorios();	
}


//****************************************************************
// Funcion		:: 	fn_Validacion
// Descripción	::	Valida Registro
// Log			:: 	AEP - 09/10/2012
//****************************************************************
function fn_Validacion(pError) {
    
	 // Inmueble
	
    if (DestinoCredito_Inmueble.indexOf($("#hidCodClasificacion").val()) != -1) {
    	
     var txtcantidadinmueble = $('input[id=txtCantidadInmueble]:text');
	 var txtcodigopredio = $('input[id=txtCodigoPredioInmueble]:text');
	 var txtDescripcionInmueble =    $('textarea[id=txtDescripcionInmueble]');
	 var txtUbicacion=    $('input[id=txtUbicacionInmueble]:text');
     var txtUsoInmueble=    $('input[id=txtUsoInmueble]:text');	
	 var cmbEstadoBien = $('select[id=ddlEstadoBien]');
	 var cmbDepartamento = $('select[id=ddlDepartamentoInmueble]');
	 var cmbProvincia = $('select[id=ddlProvinciaInmueble]');
	 var txtOficinaInmueble = $('input[id=txtOficinaRegistralInmueble]:text');
     var txtPartidaInmueble = $('input[id=txtPartidaRegistralInmueble]:text');
      
      pError.append(fn_util_ValidateControl(txtUsoInmueble[0], 'uso', 1, ''));	
      pError.append(fn_util_ValidateControl(txtcantidadinmueble[0], 'una cantidad', 1, ''));
      pError.append(fn_util_ValidateControl(txtDescripcionInmueble[0], 'una descripción', 1, ''));
      pError.append(fn_util_ValidateControl(txtUbicacion[0], 'una ubicacion', 1, ''));
      pError.append(fn_util_ValidateControl(cmbEstadoBien[0], 'un estado del bien', 1, ''));	
      pError.append(fn_util_ValidateControl(cmbDepartamento[0], 'un departamento', 1, ''));
      pError.append(fn_util_ValidateControl(cmbProvincia[0], 'una provincia', 1, ''));	
    
    }
    // Maquinaria
    else if (DestinoCredito_Maquinaria.indexOf($("#hidCodClasificacion").val()) != -1) {

     var txtDescripcionMaquina = $('textarea[id=txtDescripcionAutoMaquina]');
     var cmbEstadoMaquina= $('select[id=cmbEstadobienMaquina]');
	 var txtUbicacionMaquina = $('input[id=txtUbicacionBienMaquina]:text');
     var txtUsoMaquina = $('input[id=txtUsoMaquina]:text');	
     var txtMarcaMaquina = $('input[id=txtMarcaMaquina]:text');
	 var txtCantidadMaquina =    $('input[id=txtCantidadMaquina]:text');
     var cmbDepartamentoMaquina= $('select[id=cmbDepartamentoMaquinaria]');
     var cmbProvinciaMaquina= $('select[id=cmbProvinciaMaquinaria]');
    
	  pError.append(fn_util_ValidateControl(txtDescripcionMaquina[0], 'una descripción', 1, ''));
      pError.append(fn_util_ValidateControl(txtCantidadMaquina[0], 'una cantidad', 1, ''));
      pError.append(fn_util_ValidateControl(txtMarcaMaquina[0], 'una marca', 1, ''));
    	 pError.append(fn_util_ValidateControl(txtUsoMaquina[0], 'uso', 1, ''));
      pError.append(fn_util_ValidateControl(txtUbicacionMaquina[0], 'una dirección', 1, ''));
      pError.append(fn_util_ValidateControl(cmbEstadoMaquina[0], 'un estado del bien', 1, ''));	
      pError.append(fn_util_ValidateControl(cmbDepartamentoMaquina[0], 'un departamento', 1, ''));
      pError.append(fn_util_ValidateControl(cmbProvinciaMaquina[0], 'una provincia', 1, ''));	
    }
    // Otros
    else if (DestinoCredito_Otros.indexOf($("#hidCodClasificacion").val()) != -1) {
           
     var txtDescripcionOtros = $('textarea[id=txtDescripcionDatosOtros]');
     var cmbEstadoOtros= $('select[id=ddlEstadoDatosOtros]');
	 var txtUbicacionOtros = $('input[id=txtUbicacionDatosOtros]:text');
     var txtUsoOtros = $('input[id=txtUsoDatosOtros]:text');	
     var txtMarcaOtros = $('input[id=txtMarcaDatosOtros]:text');
	 var txtCantidadOtros =    $('input[id=txtCantidadDatosOtros]:text');
     var cmbDepartamentoOtros= $('select[id=ddlDepartamentoDatosOtros]');
     var cmbProvinciaOtros= $('select[id=ddlProvinciaDatosOtros]');
    
	  pError.append(fn_util_ValidateControl(txtDescripcionOtros[0], 'una descripción', 1, ''));
      pError.append(fn_util_ValidateControl(txtCantidadOtros[0], 'una cantidad', 1, ''));
      pError.append(fn_util_ValidateControl(txtMarcaOtros[0], 'una marca', 1, ''));
      pError.append(fn_util_ValidateControl(txtUbicacionOtros[0], 'una dirección', 1, ''));
      pError.append(fn_util_ValidateControl(txtUsoOtros[0], 'uso', 1, ''));	
      pError.append(fn_util_ValidateControl(cmbEstadoOtros[0], 'un estado del bien', 1, ''));	
      pError.append(fn_util_ValidateControl(cmbDepartamentoOtros[0], 'un departamento', 1, ''));
      pError.append(fn_util_ValidateControl(cmbProvinciaOtros[0], 'una provincia', 1, ''));	
    }
   else if (DestinoCredito_Vehiculo.indexOf($("#hidCodClasificacion").val()) != -1) {
           
      var txtDescripcionVehiculo = $('textarea[id=txtDescripcionVehiculo ]');
     var cmbEstadoVehiculo = $('select[id=ddlEstadoBienVehiculo]');
	 var txtUbicacionVehiculo  = $('input[id=txtUbicacionVehiculo]:text');
     var txtMarcaVehiculo  = $('input[id=txtMarcaVehiculo]:text');
	 var txtCantidadVehiculo  =    $('input[id=txtCantidadVehiculo]:text');
      var txtUsoVehiculo  =    $('input[id=txtUsoVehiculo]:text');	
     var cmbDepartamentoVehiculo = $('select[id=ddlDepartamentoVehiculo]');
     var cmbProvinciaVehiculo = $('select[id=ddlProvinciaVehiculo]');
    
	  pError.append(fn_util_ValidateControl(txtDescripcionVehiculo[0], 'una descripción', 1, ''));
      pError.append(fn_util_ValidateControl(txtCantidadVehiculo[0], 'una cantidad', 1, ''));
      pError.append(fn_util_ValidateControl(txtMarcaVehiculo[0], 'una marca', 1, ''));
      pError.append(fn_util_ValidateControl(txtUsoVehiculo[0], 'uso', 1, ''));	
      pError.append(fn_util_ValidateControl(txtUbicacionVehiculo[0], 'una dirección', 1, ''));
      pError.append(fn_util_ValidateControl(cmbEstadoVehiculo[0], 'un estado del bien', 1, ''));	
      pError.append(fn_util_ValidateControl(cmbDepartamentoVehiculo[0], 'un departamento', 1, ''));
      pError.append(fn_util_ValidateControl(cmbProvinciaVehiculo[0], 'una provincia', 1, ''));	
    	
    }
	
    return pError.toString();
}

 function fn_ListaInmuebles() {
     var ctrlBtn = window.parent.frames[0].document.getElementById('btnCargarInmuebles');
     ctrlBtn.click();
     //parent.fn_util_CierraModal();
 }
  function fn_ListaMaquinas() {
     var ctrlBtn = window.parent.frames[0].document.getElementById('btnCargarMaquinaria');
     ctrlBtn.click();
     //parent.fn_util_CierraModal();
 }
 
   function fn_ListaVehiculo() {
     var ctrlBtn = window.parent.frames[0].document.getElementById('btnCargarVehiculo');
     ctrlBtn.click();
     //parent.fn_util_CierraModal();
 }
 
  
   function fn_ListaDatosOtros() {
     var ctrlBtn = window.parent.frames[0].document.getElementById('btnCargarDatosOtros');
     ctrlBtn.click();
     //parent.fn_util_CierraModal();
 }