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


	$("#dv_grabar_e").css('display', 'none');
	$("#dv_Cancelar").css('display', 'none');
	 fn_cargaGrilla();
   
	 //On load Page (siempre al final)
    fn_onLoadPage();
	  
});

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() {
	
//	  var mydata = 
//              [
//    		    { nombre: "Jeisson Espinoza P", correo: " aaas6@ssss.ss", telefono: "65545444", codigo: "DASO SA"},
//                { nombre: "Jeisson Espinoza P", correo: " jjjj@aa.ss", telefono: "4544564", codigo: "DASO SA"},
//              	{ nombre: "Jeisson Espinoza P", correo: " okioik@dddd.ss", telefono: "4454545", codigo: "DASO SA"},
//              	{ nombre: "Jeisson Espinoza P", correo: " jj@ssuyss.ss", telefono: "02121485", codigo: "DASO SA"},
//              	{ nombre: "Jeisson Espinoza P", correo: " ll@ssiiss.ss", telefono: "644545445", codigo: "DASO SA"}
//    		  ];

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");	    
            fn_buscar();
        },
//    	datatype: "local",
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
        root: "Items",
        page: "CurrentPage", // Número de página actual.
        total: "PageCount", // Número total de páginas.
        records: "RecordCount", // Total de registros a mostrar.
        repeatitems: false,
        id: "CodigoContacto" // Índice de la columna con la clave primaria.
    },
    colNames: ['Nombre Contacto', 'Correo', 'Teléfono', '',''],
    colModel: [
        { name: 'Nombre', index: 'Nombre', width: 100, sorttype: "string", align: "right" },
        { name: 'Correo', index: 'Correo', width: 100, sorttype: "string", align: "center" },
		{ name: 'Telefono', index: 'Telefono', width: 80, sorttype: "string", align: "left" },
		{ name: 'CodigoContacto', index: 'CodigoContacto',  hidden: true},
        { name: 'a', index: 'a',  hidden: true,width:2 }
	],
    height: '100%',
    pager: '#jqGrid_pager_A',
    loadtext: 'Cargando datos...',
    emptyrecords: 'No hay resultados',
    rowNum: 5, // Tamaño de la página
    rowList: [10, 20, 30],
    sortname: 'CodigoContacto',
    sortorder: 'desc',
    viewrecords: true,
    gridview: true,
    autowidth: true,
    altRows: true,
    altclass: 'gridAltClass',
    onSelectRow: function(id) {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
        $("#hidCodID").val(id);
        
    }  
   	    });
   	
//    	for (var i = 0; i <= mydata.length; i++) {
//    	     jQuery("#jqGrid_lista_A").jqGrid('addRowData', i + 1, mydata[i]);
//    	 }
   	
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    
//    	  function Lupa(cellvalue, options, rowObject) {
//           
//       if (rowObject.ComentarioBaja == "") {
//           return "<img src='../Util/images/ico_aprobar_inact.gif' alt='" + cellvalue + "' title='Asiganar Usuario' width='17px' style='cursor: pointer;cursor: hand;' />";
//       } else {
//          var sScript2 = "javascript:fn_asignar();";
//       	  return "<img src='../Util/images/ico_aprobar_act.gif' alt='" + cellvalue + "' title='Asiganar Usuario' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
//       }
//    	
//    };
}

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	
// Log			:: 	AEP - 21/03/2013
//****************************************************************

function fn_buscar() {
	fn_cargaGrilla();

		parent.fn_blockUI();
		var CodigoSuprestatario= $('#hidCodigoSup').val() == undefined ? "" : $('#hidCodigoSup').val();
		
		var NombreC = $('#txtNombreContactoP').val() == undefined ? "" : $('#txtNombreContactoP').val();
		var CorreoC = $('#txtCorreoContactoP').val() == undefined ? "" : $('#txtCorreoContactoP').val();
		var TelefonoC = $('#txtTelefonoContactoP').val() == undefined ? "" : $('#txtTelefonoContactoP').val();
		
		
		var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"), // Cantidad de elementos de la página.
			"pCurrentPage", intPaginaActual, // Página actual.
			"pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
			"pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
			"pCodigoSuprestatario", CodigoSuprestatario,
			"pNombreC", NombreC,
			"pCorreoC", CorreoC,
			"pTelefonoC", TelefonoC
		];
	
		fn_util_AjaxWM("frmMntClienteAgregarAsignar.aspx/BuscarContacto",
			arrParametros,
			function(jsondata) {
				jqGrid_lista_A.addJSONData(jsondata);
				parent.fn_unBlockUI();
				 fn_doResize();
			},
			function(request) {
				parent.fn_unBlockUI();
				fn_util_alert(jQuery.parseJSON(request.responseText).Message);
			}
		);

}




//************************************************************
// Función		:: 	fn_LimpiarCampos
// Descripcion 	:: 	Método para limpiar los campos de la busqueda
// Log			:: 	AEP - 10/07/2012
//************************************************************

function fn_LimpiarCampos() {
	blnPrimeraBusqueda=false;
	$("#txtNombreContactoP").val('');
	$("#txtCorreoContactoP").val('');
	$("#txtTelefonoContactoP").val('');
	$("#jqGrid_lista_A").GridUnload();
	fn_cargaGrilla();
}

//****************************************************************
// Funcion		:: 	fn_grabar
// Descripción	::	Grabar
// Log			:: 	AEP - 21/03/2013
//****************************************************************
function fn_grabar() {
	
	
	
	var strError = new StringBuilderEx();
	            parent.fn_blockUI();

	            var vCodSuprestatario = ''; 
                var vNombre = '';
                var vCorreo= '';
                var vTelefono = '';
                
            	
              
                    vCodSuprestatario = $('#hidCodigoSup').val() == undefined ? "" : $('#hidCodigoSup').val();
                    vNombre = $('#txtNombreContactoP').val() == undefined ? "" : $('#txtNombreContactoP').val();
                	vCorreo = $('#txtCorreoContactoP').val() == undefined ? "" : $('#txtCorreoContactoP').val();
                	vTelefono = $('#txtTelefonoContactoP').val() == undefined ? "" : $('#txtTelefonoContactoP').val();
                	
                
                	 	var arrParametros = ["pCodSuprestatario", vCodSuprestatario,
                                             "pNombre", vNombre,
                                             "pCorreo", vCorreo,
                                             "pTelefono",vTelefono
                                    ];

            	fn_util_AjaxWM("frmMntClienteAgregarAsignar.aspx/GuardarContactoSuprestatario",
            		arrParametros,
            		function (resultado2) {
            			parent.fn_unBlockUI();
            			fn_mdl_mensajeIco("Se grabó correctamente los datos del contacto", "../util/images/ok.gif", "Grabar contacto suprestatario");
            			fn_LimpiarCampos();
            		},
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
	

}

//****************************************************************
// Funcion		:: 	fn_Editar
// Descripción	::	Grabar
// Log			:: 	AEP - 21/03/2013
//****************************************************************

function fn_Editar() {
	
	if ($("#hidCodID").val()!="") {

	var id = $("#hidCodID").val();
	var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
	
	$("#hidCodSupContacto").val(rowData.CodigoContacto);	
	$("#txtNombreContactoP").val(rowData.Nombre);
	$("#txtCorreoContactoP").val(rowData.Correo);
	$("#txtTelefonoContactoP").val(rowData.Telefono);
	 
	 $("#dv_grabar_e").css('display', 'block');
	 $("#dv_Cancelar").css('display', 'block');
	
	 $("#divAgregar").css('display', 'none');
	 $("#divEliminar").css('display', 'none');
	 $("#divEditar").css('display', 'none');
	
	}else {
	            			fn_mdl_mensajeIco("Seleccione un registro para poder modificar", "../util/images/warning.gif", "Grabar contacto suprestatario");

	}

}


	
	
//****************************************************************
// Funcion		:: 	fn_modificar
// Descripción	::	Grabar
// Log			:: 	AEP - 21/03/2013
//****************************************************************
function fn_modificar() {

    var strError = new StringBuilderEx();
	            parent.fn_blockUI();


	            var vCodSuprestatario = ''; 
                var vNombre = '';
                var vCorreo= '';
                var vTelefono = '';
	            var vCodSuprestatarioContacto = '';
                
            	    vCodSuprestatario = $('#hidCodigoSup').val() == undefined ? "" : $('#hidCodigoSup').val();
	                vCodSuprestatarioContacto = $("#hidCodSupContacto").val() == undefined ? "" : $("#hidCodSupContacto").val();
                    vNombre = $('#txtNombreContactoP').val() == undefined ? "" : $('#txtNombreContactoP').val();
                	vCorreo = $('#txtCorreoContactoP').val() == undefined ? "" : $('#txtCorreoContactoP').val();
                	vTelefono = $('#txtTelefonoContactoP').val() == undefined ? "" : $('#txtTelefonoContactoP').val();
	                
                	
                
                	 	var arrParametros = ["pCodSupContacto",vCodSuprestatarioContacto,
                	 		                 "pCodSuprestatario", vCodSuprestatario,
                                             "pNombre", vNombre,
                                             "pCorreo", vCorreo,
                                             "pTelefono",vTelefono,
	                                         "pEstado","2"
                                    ];

            	fn_util_AjaxWM("frmMntClienteAgregarAsignar.aspx/ModificarContactoSuprestatario",
            		arrParametros,
            		function (resultado2) {
            			parent.fn_unBlockUI();
            			fn_mdl_mensajeIco("Se modificó correctamente los datos del contacto", "../util/images/ok.gif", "Modificar contacto suprestatario");
            			fn_LimpiarCampos();
            			 $("#dv_grabar_e").css('display', 'none');
	                     $("#dv_Cancelar").css('display', 'none');
	                     $("#divAgregar").css('display', 'block');
	                     $("#divEliminar").css('display', 'block');
	                     $("#divEditar").css('display', 'block');
            		},
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
	
            }

function fn_cancelar() {
 fn_LimpiarCampos();	
 $("#dv_grabar_e").css('display', 'none');
 $("#dv_Cancelar").css('display', 'none');
 $("#divAgregar").css('display', 'block');
 $("#divEliminar").css('display', 'block');
 $("#divEditar").css('display', 'block');
}

//****************************************************************
// Funcion		:: 	fn_Eliminar
// Descripción	::	Eliminar
// Log			:: 	AEP - 21/03/2013
//****************************************************************

function fn_Eliminar() {
	
	if ($("#hidCodID").val()!="") {

             fn_mdl_confirma('¿Esta seguro de eliminar este registro?',
            function() {
            	
                var id = $("#hidCodID").val();
	            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
	            $("#hidCodSupContacto").val(rowData.CodigoContacto);	

	            var vCodSuprestatario = ''; 
                var vNombre = '';
                var vCorreo= '';
                var vTelefono = '';
	            var vCodSuprestatarioContacto = '';
                
            	
                    vCodSuprestatario = $('#hidCodigoSup').val() == undefined ? "" : $('#hidCodigoSup').val();
	                vCodSuprestatarioContacto = $("#hidCodSupContacto").val() == undefined ? "" : $("#hidCodSupContacto").val();
                    vNombre = $('#txtNombreContactoP').val() == undefined ? "" : $('#txtNombreContactoP').val();
                	vCorreo = $('#txtCorreoContactoP').val() == undefined ? "" : $('#txtCorreoContactoP').val();
                	vTelefono = $('#txtTelefonoContactoP').val() == undefined ? "" : $('#txtTelefonoContactoP').val();
	                
                	
                
                	 	var arrParametros = ["pCodSupContacto",vCodSuprestatarioContacto,
                	 		                 "pCodSuprestatario", vCodSuprestatario,
                                             "pNombre", vNombre,
                                             "pCorreo", vCorreo,
                                             "pTelefono",vTelefono,
                	 		                 "pEstado","2"
                                    ];

            	fn_util_AjaxWM("frmMntClienteAgregarAsignar.aspx/ModificarContactoSuprestatario",
            		arrParametros,
            		function (resultado2) {
            			parent.fn_unBlockUI();
            			fn_mdl_mensajeIco("Se eliminó correctamente los datos del contacto", "../util/images/ok.gif", "Eliminar contacto suprestatario");
            			fn_LimpiarCampos();
            			
            		},
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
	 },
            "../util/images/question.gif",
            function() {
            	
            },
            'Mantenimiento Contacto'
         );
	 
	
	}else {
	
	fn_mdl_mensajeIco("Seleccione un registro para poder eliminar", "../util/images/warning.gif", "Eliminar contacto suprestatario");

	}

}


function fn_asignar() {
	
		var id = $('#hidCodID').val(); 
	if (id!="") {
		        
		        parent.fn_blockUI();
		
		        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
		
		        var vCodSuprestatario = ''; 
                var vCodSuprestatarioContacto = rowData.CodigoContacto;
		        var vNombre = rowData.Nombre; 
                var vCorreo = rowData.Correo;
                var vTelefono = rowData.Telefono;
	
		
		var ctrlIframe = parent.document.getElementById('ifrModal');
		
		var ctrlTxtNombre = ctrlIframe.contentWindow.document.getElementById("txtNombreContactoP");
		var ctrlTxtCorreo = ctrlIframe.contentWindow.document.getElementById("txtCorreoContactoP");
		var ctrlTxtTelefono = ctrlIframe.contentWindow.document.getElementById("txtTelefonoContactoP");
		var ctrlCodContacto = ctrlIframe.contentWindow.document.getElementById("HidCodigoContacto");
		
		ctrlTxtNombre.value = vNombre;
		ctrlTxtCorreo.value = vCorreo;
		ctrlTxtTelefono.value = vTelefono;
		ctrlCodContacto.value = vCodSuprestatarioContacto;
		parent.fn_unBlockUI();
	    parent.fn_util_CierraModal2();
		
		
    } else {
        parent.fn_mdl_mensajeIco("&nbsp;&nbsp;- Debe seleccionar un registro de la lista", "Util/images/warning.gif", "ADVERTENCIA AL ASIGNAR");
    }
  
  


}

function fn_listaContactoPreferente() {

	var ctrlIframe = parent.document.getElementById('ifrModal');
	var ctrlBtn = ctrlIframe.contentWindow.document.getElementById("btnCargarContacto");
		ctrlBtn.click();
		parent.fn_util_CierraModal2();
}