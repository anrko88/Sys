//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	IJM - 07/03/2012
//****************************************************************
$(document).ready(function() {
    //Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));

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


    //Valida Tipo de Datos

    $("#txtMontoDisponible").validNumber({ value: '' });
    $("#txtMontoAprobado").validNumber({ value: '' });
    $("#txtMontoUtilizado").validNumber({ value: '' });

}


////****************************************************************
//// Funcion		:: 	fn_seteaCamposObligatorios
//// Descripción	::	Validacion Campos Obligatorios
//// Log			:: 	JRC - 10/02/2012
////****************************************************************
//function fn_seteaCamposObligatorios() {


//}