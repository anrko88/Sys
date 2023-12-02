//****************************************************************
// Variables Globales
//****************************************************************
var C_GESTIONBIEN_TASACION = "006";

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
    $("#txtvalorrealizacion").validNumber({ value: '', decimals: 2, length: 15 });
    $("#txtValorComercial").validNumber({ value: '', decimals: 2, length: 15 });
    
    
}
function fn_configuraGrilla() {

}

function fn_guardar() {

    
        var strError = new StringBuilderEx();
        var objcmbTasador = $('select[id=cmbTasador]');
        var objFechaasignacion = $('input[id=txtFechaasignacion]:text');
        var objtxtFechatasacion = $('input[id=txtFechatasacion]:text');
        var objtxtvalorrealizacion = $('input[id=txtvalorrealizacion]:text');
        var objtxtValorComercial = $('input[id=txtValorComercial]:text');


        strError.append(fn_util_ValidateControl(objFechaasignacion[0], 'Fecha Asignación', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtFechatasacion[0], 'Fecha Tasación', 1, ''));

        strError.append(fn_util_ValidateControl(objtxtvalorrealizacion[0], 'valor realización', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtValorComercial[0], 'valor Comercial', 1, ''));

        strError.append(fn_util_ValidateControl(objcmbTasador[0], 'Tasador', 1, ''));

        if (!fn_util_ComparaFecha($("#txtFechaasignacion").val(),$("#txtFechatasacion").val())) {            
           strError.append('La Fecha de Tasación no debe ser menor a la fecha de asignación'); 
       }

        if (strError.toString() != '') {
            parent.fn_unBlockUI();
            parent.fn_mdl_alert(strError.toString(), function() { });

            strError = null;
        } else {

			var arrParametros = ["pCodSolicitudcredito", $("#hddCodContrato").val() ,
                             "pCodTasacion",         $("#hddCodtasacion").val(),
                             "pvalorejecucion",      $("#txtvalorrealizacion").val(),
                             "pvalorComercial",      $("#txtValorComercial").val(),
                             "pfechaencargo",        $("#txtFechaasignacion").val(),
                             "pfechatasacion",       $("#txtFechatasacion").val(),
                             "pCodTasador",          $("#cmbTasador").val(),
                             "pSecfinanciamiento",   $("#hddSecfinanciamiento ").val()
                            ];
                                                        
			fn_util_AjaxSyncWM("frmTasacionRegistroDetalle.aspx/ActualizaTasacion",
						arrParametros,
						function(request) {
	                        
						},
						function(request) {
							parent.fn_util_alert(jQuery.parseJSON(request.responseText).Message);
							parent.fn_unBlockUI();
						}
			);
	                
	                 
	                 
			fn_ListaDocumentos();
			parent.fn_unBlockUI();

        }
       
        fn_util_SeteaCalendario($('input[id*=txtFecha]'));
       

    }

    function fn_ListaDocumentos() {
        var ctrlBtn = window.parent.frames[0].document.getElementById('cmdListarDocumentos');
        ctrlBtn.click();
        parent.fn_util_CierraModal();
    }



//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos() {	
	var pstrCodContrato = fn_util_trim($("#hddCodContrato").val());
	var pstrCodBien = fn_util_trim($("#hddSecfinanciamiento").val());
	var pstrCodRelacionado = fn_util_trim($("#hddCodtasacion").val());
	var pstrCodTipo = C_GESTIONBIEN_TASACION;
	parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo, 800, 350, function() { });	
}


