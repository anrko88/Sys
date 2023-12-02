//VARIABLES GLOBALES

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
   
    $("#jqGrid_listado").setGridWidth($(window).width() - 50);
    
     $(document).keypress(function(e) {
        if (e.which == 13) {
          //  fn_buscarProveedor(true);
        }
    });
    
   
    $("#txtRazonSocial").validText({ type: 'comment', length: 100 });	
	$('#txtNroDocumento').attr('disabled','disabled');
	
	$('#imgBuscarContactoP').click(function() {

    //parent.fn_util_AbreModal2("Mant. Cliente", "Administracion/frmMntClienteAgregarAsignar.aspx?ccf=" + strcodContrato + "&csf=" + strsecfinanciamiento + "&Add=true", 650, 300, function() { });
    parent.fn_util_AbreModal2("Mant. Cliente", "Administracion/frmMntClienteAgregarAsignar.aspx?codSup=" + $("#hidCodSup").val(), 850, 450, function() { });
	});
    //On load Page (siempre al final)
    fn_onLoadPage();
	//fn_validarCampos($("#cmbTipoDocumento").val());
	


	  
});

//************************************************************
// Función		:: 	fn_LimpiarCampos
// Descripcion 	:: 	Método para limpiar los campos de la busqueda
// Log			:: 	AEP - 10/07/2012
//************************************************************

function fn_LimpiarCampos() {
	blnPrimeraBusqueda=false;
	$("#txtNroDocumento").val('');
	$("#txtRazonSocial").val('');
	$("#cmbTipoDocumento").val(0);
	$("#jqGrid_lista_A").GridUnload();
	fn_cargaGrilla();
}

function fn_grabar() {
	
	            var vCodSuprestatario = ''; 
                var vDireccion = '';
                
                
            	      vCodSuprestatario = $('#hidCodSup').val() == undefined ? "" : $('#hidCodSup').val();
                      vDireccion = $('#txtDireccion').val() == undefined ? "" : $('#txtDireccion').val();
                	  var vCodSuprestatarioContacto = $('#HidCodigoContacto').val() == undefined ? "" : $('#HidCodigoContacto').val(); 
		              var vNombre =  $('#txtNombreContactoP').val() == undefined ? "" : $('#txtNombreContactoP').val(); 
                      var vCorreo =  $('#txtCorreoContactoP').val() == undefined ? "" : $('#txtCorreoContactoP').val();
                      var vTelefono = $('#txtTelefonoContactoP').val() == undefined ? "" : $('#txtTelefonoContactoP').val();
                	
                
                	 	var arrParametros = ["pCodSuprestatario", vCodSuprestatario,
                                             "pDireccion", vDireccion,
                	 		                 "pCodSupContacto",vCodSuprestatarioContacto,
                	 		                 "pNombre", vNombre,
                	 		                 "pCorreo", vCorreo,
                	 		                 "pTelefono", vTelefono
                                    ];

            	fn_util_AjaxWM("frmMntClienteModificacion.aspx/ModificarSuprestatario",
            		arrParametros,
            		function (resultado2) {
            			parent.fn_unBlockUI();
            			fn_mdl_mensajeIco("Se modificó correctamente la dirección del cliente", "../util/images/ok.gif", "Modificar suprestatario");
            			fn_listaCliente();
            		},
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
	
	           
//                	 	var arrParametros2 = [
//                	 		                 "pCodSuprestatario", vCodSuprestatario,
//                                             "pNombre", vNombre,
//                	 		                 "pCorreo", vCorreo,
//                	 		                 "pTelefono", vTelefono
//                                    ];

//            	fn_util_AjaxWM("frmMntClienteModificacion.aspx/AsignarContactoPreferente",
//            		arrParametros2,
//            		function (resultado2) {
//            			parent.fn_unBlockUI();
//            	     },
//                     function(resultado) {
//                         var error = eval("(" + resultado.responseText + ")");
//                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
//                     });
}

function fn_listaCliente() {
  
    var btnCargarContacto = window.parent.frames[0].document.getElementById('btnCargar');
	btnCargarContacto.click();
    parent.fn_util_CierraModal();
}

