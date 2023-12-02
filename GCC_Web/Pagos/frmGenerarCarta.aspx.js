var strTipoDocumentoDNI = "1";
var strTipoDocumentoRUC = "2";
var strTipoDocumentoCarnetEx = "3";
var strTipoDocumentoPasaporte = "5";
var strTipoDocumentoOtroDoc = "6";
var blnPrimeraBusqueda;
var intPaginaActual = 1;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
//****************************************************************
$(document).ready(function() {
  
    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_CargaData();
        }
    });

    $("#txtNroLote").validText({ type: 'number', length: 8 });
      fn_util_SeteaObligatorio($("#txtNroLote"), "text");
    //On load Page (siempre al final)
    fn_onLoadPage();

});

//****************************************************************
// Funcion		:: 	fn_CargaData
// Descripción	::	
// Log			:: 	AEP - 08/03/2013
//****************************************************************
function fn_CargaData() {
    if ($("#txtNroLote").val() != "") {
        var Lote = $("#txtNroLote").val();
        var arrParametros = ["pstrLote", Lote];
        fn_util_AjaxSyncWM("frmGenerarCarta.aspx/ObtieneDatosLote",
            arrParametros,
                function(jsondata) {
                	$("#hddCartas").val(jsondata.Items.length);
                	
                	if (jsondata.Items.length>0) {
                	for (var i = 0; i < jsondata.Items.length; i++) {							
                        $("#txtMunicipalidad").val(jsondata.Items[i].Municipalidad);
                        $("#txtImporteTotal").val(jsondata.Items[i].ImporteTotal);
                        $("#txtGeneracion").val(jsondata.Items[i].FechaGeneracion);
	                    $("#txtConcepto").val(jsondata.Items[i].Concepto);
                        }
                	}else {
                	
                	parent.fn_mdl_mensajeIco("No se encontó información para este Lote", "util/images/warning.gif", "Generar Cartas");

                	}

                },
                        function(resultado) {
                            parent.fn_unBlockUI();
                            var error = eval("(" + resultado.responseText + ")");
                            parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR Al CARGAR");
                        }
            );
    }else {
		parent.fn_mdl_mensajeIco("Debe ingresar un número de lote", "util/images/warning.gif", "Generar Cartas");
    
    }
}
fn_PonerDatos = function(response) {
    var objEImpuestoMunicipal = response;
    var Muni = objEImpuestoMunicipal.Municipalidad == undefined ? "0" : objEImpuestoMunicipal.Municipalidad;
    var Total = objEImpuestoMunicipal.Total == undefined ? "0" : objEImpuestoMunicipal.Total;
    var Fechacobro = objEImpuestoMunicipal.FechacobroStr == undefined ? "0" : objEImpuestoMunicipal.FechacobroStr;
	var Concepto = objEImpuestoMunicipal.Concepto == undefined ? "0" : objEImpuestoMunicipal.Concepto;
    $("#txtMunicipalidad").val(Muni);
    $("#txtImporteTotal").val(Total);
    $("#txtGeneracion").val(Fechacobro);
	$("#txtConcepto").val(Concepto);
};

function fn_VerReporte() {
	
	if ($("#hddCartas").val()=='')
	{
		parent.fn_mdl_mensajeIco("Debe ingresar un lote para mostrar la información", "util/images/warning.gif", "Generar Cartas");
	}else {
		$("#btnGenerar").click();
	}

}