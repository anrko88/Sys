// Función		:: 	JQUERY - Enviar carta cliente
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    // On load Page (siempre al final)
    fn_onLoadPage();

});

function fnRetornar(strCodContrato) {
    parent.fn_util_CierraModal();
    parent.fn_mdl_mensajeOk(

                "Se envió el correo correctamente del Contrato Nro. <span  style='font-size:11px;font-weight:bold;'>" + strCodContrato + "</span>.",
                function() {
                    try {
                        parent.fn_util_menuLink("Verificacion/frmSolicitudDocumentoClienteListado.aspx");
                        parent.fn_util_CierraModal();
                    } catch (ex) {
                        alert(ex);
                    }
                },
                "GENERACIÓN CORRECTA",
                "../template/css/img/ico_modalOk.png"
            );
}