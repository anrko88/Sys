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
// Función		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
    $('#txtEjecutivoBanca').prop('readonly', true);
    $('#txtEjecutivoBanca').attr('class', 'css_input_inactivo');

    $('#txtFechadesde').prop('readonly', true);
    $('#txtFechadesde').attr('class', 'css_input_inactivo');

    $('#txtFechahasta').prop('readonly', true);
    $('#txtFechahasta').attr('class', 'css_input_inactivo');


    $('#txtPeriodo').prop('readonly', true);
    $('#txtPeriodo').attr('class', 'css_input_inactivo');

}

//****************************************************************
// Funcion		::  fn_guardar
// Descripción	::	guardar
// Log			:: 	
//****************************************************************
function fn_guardar() {

    var strError = new StringBuilderEx();
    var objcmbTasador = $('select[id=cmbTasador]');

    strError.append(fn_util_ValidateControl(objcmbTasador[0], 'Tasador', 1, ''));
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    } else {
        parent.fn_blockUI();

        var arrmarcados = fn_util_trim($("#hddListaContratos").val());
        var sResultadoarrmarcados = arrmarcados.split("|");
        for (var i = 0; i < sResultadoarrmarcados.length; i++) {
            var arrParametros = ["pCodSolicitudcredito", sResultadoarrmarcados[i],
                                     "pCodTasador", $("#cmbTasador").val()
                                    ];

            fn_util_AjaxSyncWM("frmTasacionAsignacion.aspx/AsignaTasador",
                    arrParametros,
                    function(request) {
                        if (request == 0) {
                            parent.fn_mdl_mensajeIco("Mensaje Enviado", "util/images/question.gif", "TASADOR");
                            fn_ListaDocumentos('Los datos se registraron correctamente');
                        } else {
                            // parent.fn_mdl_mensajeIco("Mensaje Errado", "util/images/warning.gif", "TASADOR");
                            parent.fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                        }

                    },
                    function(request) {
                        parent.fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                        parent.fn_unBlockUI();
                    }
                );
        }

        parent.fn_unBlockUI();

    }
}

//****************************************************************
// Funcion		::  fn_ListaDocumentos
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_ListaDocumentos(pMensaje) {
    var win = window.parent.frames[0]; //.document.getElementById('cmdListarDocumentos');
    win.fn_ShowMensaje(pMensaje);
    parent.fn_util_CierraModal();
}

