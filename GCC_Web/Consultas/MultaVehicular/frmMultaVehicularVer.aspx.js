//****************************************************************
// Variables Globales
//****************************************************************
var blnPrimeraBusqueda;
var intPaginaActual = 1;
var strDepartamento = "15";
var strProvincia = "01";

var C_GESTIONBIEN_MULTAVEHICULAR = "004";

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	AEP - 08/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();
	
    fn_SeteaMunicipalidad();
        	
    //On load Page (siempre al final)
    fn_onLoadPage();
    
    
});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {

}

//****************************************************************
// Funcion		:: 	fn_SeteaMunicipalidad
// Descripción	::	Setear la municipalidad
// Log			:: 	AEP - 26/11/2012
//****************************************************************
function fn_SeteaMunicipalidad() {
    
    //Carga Distrito
    fn_cargaComboMunicipalidad(strDepartamento, strProvincia);
    $("#ddlMunicipalidad").val(fn_util_trim($("#hddCodMunicipalidad").val()));
    $("#tdMunicipalidadMulta").html($('select[name=ddlMunicipalidad] option:selected').text())
}
//********************************************************  ********
// Funcion		:: 	fn_cargaComboMunicipalidad
// Descripción	::	
// Log			:: 	AEP - 26/11/2012
//****************************************************************
function fn_cargaComboMunicipalidad(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlMunicipalidad').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
//fn_doResize();
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos() {	
	var pstrCodContrato = fn_util_trim($("#hddNumeroContrato").val());
	var pstrCodBien = fn_util_trim($("#hddSecFinanciamiento").val());
	var pstrCodRelacionado = fn_util_trim($("#hddSecImpuesto").val());
	var pstrCodTipo = C_GESTIONBIEN_MULTAVEHICULAR;
	parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + "&hddVer=1", 800, 350, function() { });	
}