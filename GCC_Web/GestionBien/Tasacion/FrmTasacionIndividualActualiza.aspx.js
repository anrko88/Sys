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
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));
    //Valida Campos
    fn_inicializaCampos();

    $('#cmbTasador').change(function() {
        //alert($("#cmbTasador").val());
        if ($("#cmbTasador").val() == "0") {
            $('#txtFechaProxTasacion').removeAttr('disabled');
            $('#cmbMotivonoTasacion').removeAttr('disabled');
            $('#txtFechaProxTasacion').removeAttr('class', 'css_input_inactivo');
            $('#cmbMotivonoTasacion').removeAttr('class', 'css_input_inactivo');
        } else {
            $("#cmbMotivonoTasacion").val(0);
            $("#txtFechaProxTasacion").val('');
            $("#txtFechaProxTasacion").attr('disabled', 'disabled');
            $("#cmbMotivonoTasacion").attr('disabled', 'disabled');
            $('#txtFechaProxTasacion').attr('class', 'css_input_inactivo');
            $('#cmbMotivonoTasacion').attr('class', 'css_input_inactivo');
        }
    });

    //On load Page (siempre al final)
    fn_onLoadPage();



});


//****************************************************************
// Función		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
    $("#cmbTasador").val($("#hddCodTasador").val());

    if ($("#cmbTasador").val() == "0") {
        $('#txtFechaProxTasacion').removeAttr('disabled');
        $('#cmbMotivonoTasacion').removeAttr('disabled');
        $('#txtFechaProxTasacion').removeAttr('class', 'css_input_inactivo');
        $('#cmbMotivonoTasacion').removeAttr('class', 'css_input_inactivo');

    } else {
        $("#cmbMotivonoTasacion").val(0);
        $("#txtFechaProxTasacion").val('');
        $("#txtFechaProxTasacion").attr('disabled', 'disabled');
        $("#cmbMotivonoTasacion").attr('disabled', 'disabled');
        $('#txtFechaProxTasacion').attr('class', 'css_input_inactivo');
        $('#cmbMotivonoTasacion').attr('class', 'css_input_inactivo');
    }


}

function fn_guardar() {

    var strError = new StringBuilderEx();
    if ($("#cmbTasador").val() == 0) {
        var objcmbMotivonoTasacion = $('select[id=cmbMotivonoTasacion]');
        var objtxtFechaProxTasacion = $('input[id=txtFechaProxTasacion]:text');

        strError.append(fn_util_ValidateControl(objcmbMotivonoTasacion[0], 'Motivo no tasación', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtFechaProxTasacion[0], 'fecha de tasación', 1, ''));
    } else {

    }


    if (strError.toString() != '') {

        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
        return;
    } else {
        var arrParametros = ["pCodSolicitudcredito", $("#hddCodSolicitudcredito").val(),
                         "pSecFinanciamiento", $("#hddSecfinanciamiento").val(),
                         "pCodtasacion", $("#hddCodTasacion").val(),
                         "pCodTasador", $("#cmbTasador").val(),
                         "pFechaProxTasacion", $("#txtFechaProxTasacion").val(),
                         "pMotivoNoTasacion", $("#cmbMotivonoTasacion").val()

                         ];

        fn_util_AjaxSyncWM("FrmTasacionIndividualActualiza.aspx/AsignaTasadorIndividual",
                    arrParametros,
                    function(request) {
                        fn_ListaDocumentos();
                        parent.fn_unBlockUI();
                        fn_doResize();
                    },
                    function(request) {

                        parent.fn_unBlockUI();
                    }
                );
        fn_util_SeteaCalendario($('input[id*=txtFecha]'));
    }
}

function fn_ListaDocumentos() {

    var objRetorno = new Object();
    objRetorno.Grid = $('#hidGrid').val();
    objRetorno.ID = $('#hidID').val();
//    objRetorno.CodTasador = $('#cmbTasador').val();
//    if ($('#cmbTasador').val() == '0') { objRetorno.Tasador = '&nbsp;'; }
//    else { objRetorno.Tasador = $('select[name=cmbTasador] option:selected').text(); }
//    if ($('#txtFechaProxTasacion').val() == '') { objRetorno.FecProxTasacion = '&nbsp;' }
//    else { objRetorno.FecProxTasacion = $('#txtFechaProxTasacion').val(); }
//    if ($('#cmbMotivonoTasacion').val() == '0') {
//        objRetorno.CodMotivo = '&nbsp;';
//        objRetorno.Motivo = '&nbsp;';
//    }
//    else {
//        objRetorno.CodMotivo = $('#cmbMotivonoTasacion').val();
//        objRetorno.Motivo = $('select[name=cmbMotivonoTasacion] option:selected').text();
//    }
   
    var winPag = window.parent.frames[0];
    winPag.fn_ShowMensaje('La asignación se registro correctamente', objRetorno);
    parent.fn_util_CierraModal();
}


