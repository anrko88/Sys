//****************************************************************
// Variables Globales
//****************************************************************
var C_TX_NUEVO = "NUEVO"
var C_TX_EDITAR = "EDITAR"
var C_APLICFONDO_ABONOCUENTA = "003";

var C_GESTIONBIEN_SINIESTRO = "001";        

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
// Log			:: 	JRC - 13/11/2012
//****************************************************************
function fn_inicializaCampos() {	
	if( fn_util_trim($("#hddTipoTx").val()) != C_TX_NUEVO ) {
	    fn_validaAplicFondo($('#hddAplicaFondo').val());		
	}	
}

//****************************************************************
// Funcion		:: 	fn_validaAplicFondo
// Descripción	::	valida Aplicacion Fondos
// Log			:: 	JRC - 13/11/2012
//****************************************************************
function fn_validaAplicFondo(strValor) {	
	if( fn_util_trim(strValor) == C_APLICFONDO_ABONOCUENTA ){
		$("#tr_cuenta").show();
    }else{
		$("#tr_cuenta").hide();
    }	
}


//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos() {	
	var pstrCodContrato = fn_util_trim($("#hddCodContrato").val());
	var pstrCodBien = fn_util_trim($("#hddCodBien").val());
	var pstrCodRelacionado = fn_util_trim($("#hddCodSiniestro").val());
	var pstrCodTipo = C_GESTIONBIEN_SINIESTRO;	
	parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + "&hddVer=1", 800, 350, function() { });	
}


