//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

	//Setea Calendario
	fn_util_SeteaCalendario($('input[id*=txtFecha]'));

     var Codclasibien = getParameter("Codclasibien");
     
     $("#dv_datos_inmueble").hide();
     $("#dv_datos_vehiculo").hide();
     $("#dv_datos_otros").hide();
     
     switch(Codclasibien) {
        case "1":
            $("#dv_datos_inmueble").show();
            break;
        case "2":
            $("#dv_datos_vehiculo").show();
            break;
        case "3":
            $("#dv_datos_otros").show();
            break;
     }
});


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
