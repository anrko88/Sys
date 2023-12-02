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
}

