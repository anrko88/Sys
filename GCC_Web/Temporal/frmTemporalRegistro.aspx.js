//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    // Asocia los eventos a los controles.
    fn_inicializaEventos();

    //Valida Campos.
    fn_inicializaCampos();

    // Verifica si la operacion es editar.
    if (fn_esEditar()) {
        fn_LeerDatosTemporal();
    }

    //On load Page (siempre al final)
    fn_onLoadPage();
});

//****************************************************************
// Funcion		:: 	fn_esEditar
// Descripción	::	Verifica si la operación es editar un registro o agregar uno nuevo. 
// Log			:: 	EBL - 05/04/2012
//****************************************************************
function fn_esEditar() {
    var strCodigo = $("#hddCodigo").val();

    if (strCodigo == "" || strCodigo == null || strCodigo == undefined) {
        return false;
    } else {
        return true;
    }
}

//****************************************************************
// Función		:: 	fn_esEditar
// Descripción	::	Lee los datos del objeto registro por medio de un web method y agrega los datos
//                  del objeto a los controles. 
// Log			:: 	EBL - 05/04/2012
//****************************************************************
function fn_LeerDatosTemporal() {
    fn_buscarDatosWM();
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Asocia los eventos a los controles.
// Log			:: 	EBL - 05/04/2012
//****************************************************************
function fn_inicializaEventos() {
    
    $('#ddlCombo').change(function() {
        var arrParametros = ["pstrTipo", "1", "pstrCombo", $("#ddlCombo").val()];
        var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whTemporal', '../');

        if (arrResultado.length > 0) {
            if (arrResultado[0] == "0") {
                $('#ddlCombo1').html(arrResultado[1]);
            } 
            else {
                var strError = arrResultado[1];
                fn_mdl_alert(strError.toString(), function() { });
            }
        }
    });
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
    // Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));
    $('#txtFecha').validText({ type: 'date', length: 10 });

    $('#txtNumero').validText({ type: 'number', length: 10 });

    $("#txtDecimales").validNumber({ value: '' });

    $('#txtTexto').validText({ type: 'alphanumeric', length: 50 });   
    
    $('#txaComentario').validText({ type: 'alphanumeric', length: 3 });
    $("#txaComentario").maxLength(3);
}

//****************************************************************
// Funcion		:: 	fn_buscarDatosASHX
// Descripción	::	Busca Datos por ASHX
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_buscarDatosASHX() {

    var strCodigo = $("#hddCodigo").val();
    var arrParametros = ["pstrTipo", "2", "pstrCombo", strCodigo];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whTemporal', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            
            $("#txtFecha").val(arrResultado[1]);
            $("#txtNumero").val(arrResultado[2]);
            $("#txtDecimales").val(arrResultado[3]);
            $("#txaComentario").val(arrResultado[4]);
            $("#txtTexto").val(arrResultado[5]);

            if (fn_util_trim(arrResultado[6]) == "1") {
                $("#chkFlag").attr("checked", "true");
            }
        } 
        else {
            var strError = arrResultado[1];
            
            fn_mdl_alert(strError.toString(), function() { });
        }
    }

}


//****************************************************************
// Funcion		:: 	fn_buscarDatosWM
// Descripción	::	Busca Datos por web method, el web method puede estar en la misma página o en otra.
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_buscarDatosWM() {
    var strCodigo = $("#hddCodigo").val();
    var paramArray = ["pCodigo", strCodigo];

    fn_util_AjaxWM("frmTemporalRegistro.aspx/BuscaDatosWM",
                   paramArray,
                   fn_PoneDatos,
                   function(result) {
                       parent.fn_mdl_mensajeIco(result.responseText, "util/images/ok.gif", "ERROR EN LA BÚSQUEDA");
                   });
}

//****************************************************************
// Función		:: 	fn_grabar
// Descripción	::	Guarda los datos ingresados por el usuario a traves de un web method,
//                  evaluando si es una operaciòn de editar o eliminar.
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function  fn_grabar() {
    if (fn_EsPaginaValida()) {
        if (fn_esEditar()) {
            fn_GuardarEditar();
        } else {
            fn_GuardarNuevo();
        }
    }
}

//****************************************************************
// Función		:: 	fn_GuardarEditar
// Descripción	::	Guarda los datos ingresados para un temporal existente (operación editar).
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function fn_GuardarEditar() {

    var arrParametros = ["strCodigo", $("#hddCodigo").val(),
                         "strFecha", Fn_util_DateToString($("#txtFecha").val()),
                         "strNumero", $("#txtNumero").val(),
                         "strCombo", $("#ddlCombo").val(),
                         "strCombo1", $("#ddlCombo1").val(),
                         "strTexto", $("#txtTexto").val(),
                         "strFlag", $("#chkFlag").val(),
                         "strDecimales", $("#txtDecimales").val(),
                         "strComentario", $("#txaComentario").val()
                         ];

    fn_util_AjaxWM("frmTemporalRegistro.aspx/GuardarEditar",
                     arrParametros,
                     fn_MensajeYRedireccionar,
                     function(result) {
                         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                     });
    
}

//****************************************************************
// Función		:: 	fn_GuardarNuevo
// Descripción	::	Guarda los datos ingresados de un nuevo temporal (operación nuevo).
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function fn_GuardarNuevo() {

    var arrParametros = ["strFecha", Fn_util_DateToString($("#txtFecha").val()),
                         "strNumero", $("#txtNumero").val(),
                         "strCombo", $("#ddlCombo").val(),
                         "strCombo1", $("#ddlCombo1").val(),
                         "strTexto", $("#txtTexto").val(),
                         "strFlag", $("#chkFlag").val(),
                         "strDecimales", $("#txtDecimales").val(),
                         "strComentario", $("#txaComentario").val()
                         ];
    
    fn_util_AjaxWM("frmTemporalRegistro.aspx/GuardarNuevo",
                     arrParametros,
                     fn_MensajeYRedireccionar,
                     function(result) {
                         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                     });
}

//****************************************************************
// Función		:: 	fn_EsPaginaValida
// Descripción	::	Verifica si los datos ingresados son válidos.
// Output       ::  
//                  true  : Datos válidos.
//                  false : Datos inválidos, mostrando un mensaje describiendo el error.
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function fn_EsPaginaValida() {
    if (true) {
        return true;
    } else {
        var strListErrors = "";
        parent.fn_mdl_mensajeIco(strListErrors, "util/images/warning.gif", "ERROR EN LA VALIDACIÓN");
        
        return false;
    }
}

//****************************************************************
// Funcion		:: 	fn_PoneDatos
// Descripción	::	Pone Datos de la consulta fn_buscarDatosWM()
// Log			:: 	JRC - 23/04/2012
//****************************************************************
var fn_PoneDatos = function(response) {
    var objETemporal = response;

    $("#txtFecha").val(fn_LeerFechaServer(objETemporal.Fecha));
    $("#txtNumero").val(objETemporal.Numero);
    $("#txtDecimales").val(objETemporal.Decimales);
    $("#txaComentario").val(objETemporal.Comentario);
    $("#txtTexto").val(objETemporal.Texto);
    $("#ddlCombo").val(objETemporal.Flag);
    // Selecciona el radio button con el valor coincidente
    $("[name=radio]").filter("[value=" + objETemporal.Flag + "]").prop("checked", true);

    if (fn_util_trim(objETemporal.Flag) == "1") {
        $("#chkFlag").attr("checked", "true");
    }
};

//****************************************************************
// Función		:: 	fn_LeerParametros
// Descripción	::	Lee los datos ingresados por el usuario para las funciones fn_GuardarEditar()
//                  y fn_GuardarNuevo().
// Log			:: 	EBL - 07/05/2012
//****************************************************************
//function fn_LeerParametros() {
var fn_LeerParametros = function() {
    var arrParametros;

    if (fn_esEditar()) {
        arrParametros = ["strCodigo", $("#hddCodigo").val(),
                         "strFecha", Fn_util_DateToString($("#txtFecha").val()),
                         "strNumero", $("#txtNumero").val(),
                         "strCombo", $("#ddlCombo").val(),
                         "strCombo1", $("#ddlCombo1").val(),
                         "strTexto", $("#txtTexto").val(),
                         "strFlag", $("#chkFlag").val(),
                         "strDecimales", $("#txtDecimales").val(),
                         "strComentario", $("#txaComentario").val()
                         ];
    } else {
        arrParametros = ["strFecha", Fn_util_DateToString($("#txtFecha").val()),
                         "strNumero", $("#txtNumero").val(),
                         "strCombo", $("#ddlCombo").val(),
                         "strCombo1", $("#ddlCombo1").val(),
                         "strTexto", $("#txtTexto").val(),
                         "strFlag", $("#chkFlag").val(),
                         "strDecimales", $("#txtDecimales").val(),
                         "strComentario", $("#txaComentario").val()
                         ];
    }

    return arrParametros;
//}
};

//****************************************************************
// Función		:: 	fn_MensajeYRedireccionar
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
//                  Luego lo redirecciona al formulario de búsquedas ("frmTemporalListado.aspx").
// Log			:: 	EBL - 07/05/2012
//****************************************************************
var fn_MensajeYRedireccionar = function(pCodigo) {

    if (pCodigo == "0") {
        fn_mdl_alert("Se grabó con éxito el Temporal. Código del Temporal: " + pCodigo.toString(), function() { });
    } else {
        fn_mdl_alert("Se actualizó con éxito el Temporal.", function() { });
    }

    fn_util_redirect("frmTemporalListado.aspx");
};