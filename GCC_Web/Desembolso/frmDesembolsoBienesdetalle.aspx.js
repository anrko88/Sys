//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    //Setea Calendario
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));
    
    //Inicializa Tabs
    //$("div#divTabs").tabs({
    //   show: function(event, ui) {
    //      fn_doResize();
    //   }
    //});

    //On load Page (siempre al final)
    HabilitaTipodeBien($("#htipobien").val());

    fn_onLoadPage();

});


function HabilitaTipodeBien(sTipoBien) {
    if (sTipoBien == "Bien Inmueble") {
        $("#dv_datos_bienes").show();  
        $("#dv_datos_vehiculo").hide();
        $("#dv_datos_otros").hide();
    }
    if (sTipoBien == "Automovil")
    {
        $("#dv_datos_vehiculo").show();
        $("#dv_datos_bienes").hide();
        $("#dv_datos_otros").hide();
    }

    if (sTipoBien == "Otros") {
        $("#dv_datos_vehiculo").hide();
        $("#dv_datos_bienes").hide();
        $("#dv_datos_otros").show();
    }   

}


function fn_grabar(){

        fn_mdl_alert(
                     "Los datos fueron grabados correctamente.",
                     function(){
                        parent.fn_util_CierraModal();
                     }, 
                     "CONFIRMACIÓN"
                     ); 

}