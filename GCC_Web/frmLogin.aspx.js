//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
//****************************************************************
$(document).ready(function() {

    fn_seteaCamposObligatorios();

    //Ubicacion Pantalla
    window.moveTo(0, 0);
    window.resizeTo(screen.availWidth, screen.availHeight);

    //Boton Ingresar
    $("#btnIngresar").click(function() {

        fn_blockUI();

        //Coge los Tipos
        var objtxtUsuario = $('input[id$=txtUsuario]:text');
        var objddlPerfil = $('select[id$=ddlPerfil]');

        //Arma validación
        var strError = new StringBuilderEx();
        strError.append(fn_util_ValidateControl(objtxtUsuario[0], 'un Usuario válido', 1, ''));
        strError.append(fn_util_ValidateControl(objddlPerfil[0], 'un Perfil válido', 1, ''));

        //Valida si hay Error
        if (strError.toString() != '') {
            fn_unBlockUI();
            fn_seteaCamposObligatorios();
            fn_mdl_alert(strError.toString(), function() { });
            strError = null;
        }
        else {
            strError = null;
            $("#btnIngresarHide").click();
        }

    });

    //Focus
    $("#btnIngresar").focus();
    //$("#btnIngresar").click();

});
	
		
//****************************************************************
// Funcion		:: 	fn_abrePopUp
// Descripción	::	Abre PopUp de la aplicacion
// Log			:: 	JRC - 10/02/2012
//****************************************************************
//function fn_abrePopUp(){
//	window.open("frmPrincipal.aspx", "GCCWeb", "titlebar=0,toolbar=0, scrollbars=0,resizable=1");
//}
	
	
//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_seteaCamposObligatorios() {
	fn_util_SeteaObligatorio($("#ddlPerfil"), "select");
	fn_util_SeteaObligatorio($("#txtClave"), "input");
	fn_util_SeteaObligatorio($("#txtUsuario"), "input");	
}
