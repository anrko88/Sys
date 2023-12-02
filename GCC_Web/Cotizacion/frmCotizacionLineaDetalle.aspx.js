//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	IJM - 07/03/2012
//****************************************************************
$(document).ready(function() {
    //Setea Calendario
    //fn_util_SeteaCalendario($('input[id*=txtFecha]'));

    //Valida Campos
    fn_inicializaCampos();


    //On load Page (siempre al final)
    fn_onLoadPage();

});


//************************************************************
// Función		:: 	fn_grabar
// Descripcion 	:: 	Método que graba
// Log			:: 	JRC - 10/02/2012
//************************************************************
function fn_guardar() {


}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
    //Valida campos obligatorio
    fn_seteaCamposObligatorios();

    $("#txtMontoDisponible").addClass("ui-edit-align-right");
    $("#txtMontoAprobado").addClass("ui-edit-align-right");
    $("#txtMontoUtilizado").addClass("ui-edit-align-right");

    $("#txtMontoDisponible").val(fn_util_ValidaMonto($("#txtMontoDisponible").val(), 2));
    $("#txtMontoAprobado").val(fn_util_ValidaMonto($("#txtMontoAprobado").val(), 2));
    $("#txtMontoUtilizado").val(fn_util_ValidaMonto($("#txtMontoUtilizado").val(), 2));
    
    $('#txtMontoDisponible').prop('readonly', true);
    $('#txtMontoAprobado').prop('readonly', true);
    $('#txtMontoUtilizado').prop('readonly', true);
    $('#txtEstado').prop('readonly', true);
    $('#txtFechaVence').prop('readonly', true);
        
}


//****************************************************************
// Funcion		:: 	fn_seteaCamposObligatorios
// Descripción	::	Validacion Campos Obligatorios
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_seteaCamposObligatorios() {


}