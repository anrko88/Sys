//****************************************************************
// Funcion		:: 	JQUERY - Subir Archivo
// Descripción	::	
//					el documento
// Log			:: 	IJM - 10/02/2012
//****************************************************************
$(document).ready(function() {

    fn_inicializaCampos();

    var strDepartamento = $("#hubigeo").val();
    var strProvincia = $("#hubigeo").val();
    var strDistrito = $("#hubigeo").val();


    $('#cmbcontratofirma').attr('disabled', 'disabled');

    if ($("#hFirmaen").val() == '001') {
        $("#cmbDepartamento").val("15");
        fn_cargaComboProvincia($("#cmbDepartamento").val());
    	fn_cargaComboDistrito($("#cmbDepartamento").val(), $("#cmbProvincia").val());
        $('#cmbDepartamento').attr('disabled', 'disabled');

    } else {
        //Carga Departamento
    	 $("#cmbDepartamento option[value='15']").remove();
        strDepartamento = strDepartamento.substring(0, 2);
        $("#cmbDepartamento").val(strDepartamento);

        if (strDepartamento == "00") {
            $('#cmbDepartamento').removeAttr('disabled');
        } else {
            $('#cmbDepartamento').attr('disabled', 'disabled');
        }

        //Carga Provincia
        fn_cargaComboProvincia($("#cmbDepartamento").val());
        strProvincia = strProvincia.substring(2, 4);
        $("#cmbProvincia").val(strProvincia);
        if (strProvincia != "00") {
            $('#cmbProvincia').attr('disabled', 'disabled');
        } else {
            $('#cmbProvincia').removeAttr('disabled');
        }

        //Carga Distrito
        fn_cargaComboDistrito($("#cmbDepartamento").val(), $("#cmbProvincia").val());
        strDistrito = strDistrito.substring(4, 6);
        $("#cmbDistrito").val(strDistrito);
        if (strDistrito != "00") {
            $('#cmbDistrito').attr('disabled', 'disabled');
        } else {
            $('#cmbDistrito').removeAttr('disabled');
        }

    }

    fn_onLoadPage();
});



//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	IJM - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
    //Valida campos obligatorio

    fn_seteaCamposObligatorios();
    //Valida Tipo de Datos
    $('#txtNroDocumento').validText({ type: 'number', length: 8 });
    $('#txtNombreRepresentante').validText({ type: 'comment', length: 100 });
    $('#txtPartidaRegistral').validText({ type: 'number', length: 10 });
    $('#txtOficinaRegistral').validText({ type: 'comment', length: 50 });
    
}


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	IJM - 10/02/2012
//****************************************************************
function fn_seteaCamposObligatorios() {

    fn_util_SeteaObligatorio($("#txtNroDocumento"), "input");
    fn_util_SeteaObligatorio($("#txtNombreRepresentante"), "input");
    fn_util_SeteaObligatorio($("#cmbDepartamento"), "select");
    fn_util_SeteaObligatorio($("#cmbProvincia"), "select");
    fn_util_SeteaObligatorio($("#cmbDistrito"), "select");
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_seteaCamposObligatorios() {
    fn_util_SeteaObligatorio($("#txtArchivoDocumentos"), "input");
}

//****************************************************************
// Funcion              ::      fn_cargaListado
// Descripción  ::      Guardar Rechazo
// Log                  ::      JRC - 21/05/2012
//****************************************************************
function fn_cargaListado() {

    var ctrlBtn = window.parent.frames[0].document.getElementById('btnBuscar');
    ctrlBtn.click();
    parent.fn_util_CierraModal();

}


//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistrito
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_LimpiaComboDistrito() {
    $('#cmbDistrito').empty();
}
//****************************************************************
// Funcion		:: 	fn_cargaComboProvincia
// Descripción	::	Carga Grilla
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_cargaComboProvincia(valor) {
    
    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbProvincia').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistrito();
	   if ($("#cmbDepartamento").val() != "0") {
        $("#txtOficinaRegistral").val($("#cmbDepartamento").find("option:selected").text());
    } else {
        $("#txtOficinaRegistral").val("");
    }
}


//****************************************************************
// Funcion		:: 	fn_cargaComboDistrito
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_cargaComboDistrito(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbDistrito').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}


function fn_GuardarRepresentanteNuevo() {

        var strError = new StringBuilderEx();
        var objcmbDepartamento = $('select[id=cmbDepartamento]');
        var objcmbProvincia = $('select[id=cmbProvincia]');
        //var objcmbDistrito = $('select[id=cmbDistrito]');
    
        var objtxtNroDocumento = $('input[id=txtNroDocumento]:text');
        var objtxtNombreRepresentante = $('input[id=txtNombreRepresentante]:text');
        var objtxtPartidaRegistral = $('input[id=txtPartidaRegistral]:text');
        var objtxtOficinaRegistral = $('input[id=txtOficinaRegistral]:text');

        strError.append(fn_util_ValidateControl(objcmbDepartamento[0], 'Departamento', 1, ''));
        strError.append(fn_util_ValidateControl(objcmbProvincia[0], 'Provincia', 1, ''));

        strError.append(fn_util_ValidateControl(objtxtNroDocumento[0], 'Numero de DNI', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtNombreRepresentante[0], 'Nombre Representante', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtPartidaRegistral[0], 'Partida Registral', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtOficinaRegistral[0], 'Oficina Registral', 1, ''));

        if ($("#txtNroDocumento").val().length != 8) {
            $("#txtNroDocumento").val('');
            strError.append(fn_util_ValidateControl(objtxtNroDocumento[0], 'DNI 8 Caracteres', 1, ''));
        }

        if (strError.toString() != '') {
            parent.fn_unBlockUI();
            parent.fn_mdl_alert(strError.toString(), function() { });
            strError = null;
        }
        else {
                var strDepartamento = $("#cmbDepartamento").val();
                if (strDepartamento == 0 || strDepartamento == null) {
                    strDepartamento = "00";
                }

                var strProvincia = $("#cmbProvincia").val();
                if (strProvincia == 0 || strProvincia == null) {
                    strProvincia = "00";
                }
            
                var strDistrito = $("#cmbDistrito").val();
                if (strDistrito == 0 || strDistrito == null) {
                    strDistrito = "00";
                }
                $("#hddCadenaUbigeo").val(strDepartamento + '' + strProvincia + '' + strDistrito);

                $("#cmdguardar").click();
                fn_util_CierraModal();
        }
}

//****************************************************************
// Funcion              ::      fn_cargaListado
// Descripción  ::      Guardar Rechazo
// Log                  ::      JRC - 21/05/2012
//****************************************************************
function fn_cargaListado() {
    var ctrlBtn = window.parent.frames[0].document.getElementById('btnBuscar');
    ctrlBtn.click();
    parent.fn_util_CierraModal();
}

function fn_MensajeValidaDni() {
    //var mensaje = "Ya existe un represente del cliente con el mismo documento de identidad. Por favor ingrese otro";

    //hubigeo.Value
    
    parent.fn_mdl_mensajeIco("Ya existe un represente del Banco con el mismo documento de identidad", "util/images/warning.gif", "ERROR AL ENVIAR");
}