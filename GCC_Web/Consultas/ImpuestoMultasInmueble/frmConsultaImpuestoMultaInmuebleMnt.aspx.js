//****************************************************************
// Variables Globales
//****************************************************************
var C_TX_NUEVO = "NUEVO";
var C_TX_EDITAR = "EDITAR";
var C_GESTIONBIEN_IMPMUNICIPAL = "003";  

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	??? - 02/11/2012
//****************************************************************
$(document).ready(function() {

	//-------------------------------------------
    //CheckPagoCliente
    //-------------------------------------------
	$('#chkPagoCliente').click(function() {
        if ($('#chkPagoCliente').is(':checked') == true) {
            $("#hddPagoCliente").val("1");
            $("#txtEstadoPago").val("003");
			$("#txtEstadoCobro").val("003");
        }else{
        	$("#hddPagoCliente").val("0");
			$("#txtEstadoPago").val("001");
			$("#txtEstadoCobro").val("001");
        }        
    });
	
    //On load Page (siempre al final)
    fn_onLoadPage();
});

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos() {	
	
	var pstrCodContrato = fn_util_trim($("#hddCodContrato").val());
	var pstrCodBien = fn_util_trim($("#hddCodBien").val());
	var pstrCodRelacionado = fn_util_trim($("#hddCodImpuesto").val());
	var pstrCodTipo = C_GESTIONBIEN_IMPMUNICIPAL;
	parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "Consultas/frmConsultaGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo, 800, 350, function() { });	
}



	