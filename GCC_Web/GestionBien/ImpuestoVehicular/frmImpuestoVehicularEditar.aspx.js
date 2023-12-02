//****************************************************************
// Variables Globales
//****************************************************************


//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	??? - 02/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();
        	
    //On load Page (siempre al final)
    fn_onLoadPage();
    
});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {
$('#txtPeriodo').validText({ type: 'number', length: 4 });
	$('#txtImporte').validNumber({value:'',decimals:2,length:15});
	
	fn_util_SeteaCalendario($('#txtFechaDeclaracion'));
	fn_util_SeteaCalendario($('#txtFechaPago'));
	fn_util_SeteaCalendario($('#txtFechaCobro'));
}

//****************************************************************
// Funcion		:: 	fn_Volver
// Descripción	::	Regresa a la pagina anterior
// Log			:: 	AEP - 24/09/2012
//****************************************************************
function fn_Volver() {
 
 	fn_mdl_confirma('¿Está seguro de volver?',
		function() {
			parent.fn_blockUI();
        fn_util_redirect('frmImpuestoVehicularListado.aspx');
		},
		"../../util/images/question.gif",
		function() {
		},
		'Gestión del Bien : Impuesto Vehicular'
	);
	
    
}