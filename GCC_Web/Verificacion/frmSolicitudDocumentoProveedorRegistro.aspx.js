// VARIABLES GLOBALES			:: 	IJM - 25/02/2012
//****************************************************************
var strTipoCambioDefault = '1';
var strCompra =     'C';
var strVenta =      'V';
var strSoles =      '001';
var strDolares = '002';

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {
   
    fn_cargaGrilla();    
    fn_inicializaCampos();

    //Valida Bloqueo
    fn_ValidaBloqueo();
    
    // On load Page (siempre al final)
    fn_onLoadPage();
    
});


//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Inicializa Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() {

    $("#jqGrid_lista_F").jqGrid({
        datatype: function() {
            fn_buscarContratoProveedor(0);
        },
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
        
        root: "Items",
        page: "CurrentPage",    // Número de página actual.
        total: "PageCount",     // Número total de páginas.
        records: "RecordCount", // Total de registros a mostrar.
        repeatitems: false,
        id: "CodigoContratoProveedor" // Índice de la columna con la clave primaria.
    },
    colNames: ['', 'Carta', 'Nro de Documento', 'Razon Social o Nombre', 'Correo', 'Moneda', 'Importe', 'Total','', 'CodigoContratoProveedor', 'CodProveedor', 'CodigoContacto', 'CodigoMoneda', 'CodigoTipoProveedor', 'DescripcionBien', '', ''],
    colModel: [
                { name: '', index: '', width: 15, align: "center", formatter: Check },
                { name: 'FlagEnvioCarta', index: 'FlagEnvioCarta', width: 15, align: "center", formatter: EnvioCarta },
	            { name: 'RUC', index: 'RUC', width: 50, align: "left" },
	            { name: 'NombreInstitucion', index: 'NombreInstitucion', width: 200, align: "left" },
	            { name: 'Correo', index: 'Correo', width: 100, align: "left" },
	            { name: 'NombreMoneda', index: 'NombreMoneda', width: 50, align: "center" },
	            { name: 'Importe', index: 'Importe', width: 50, align: "right",sorttype: "float", formatter: Fn_util_ReturnValidDecimal2 },
	            { name: 'TotalImporte', index: 'TotalImporte', width: 50, align: "right" , sorttype: "float", formatter: Fn_util_ReturnValidDecimal2 },
    	        { name: '', index: '',width: 1 } ,
	            { name: 'CodigoContratoProveedor', index: 'CodigoContratoProveedor', hidden: true },
	            { name: 'CodProveedor', index: 'CodProveedor', hidden: true },
	            { name: 'CodigoContacto', index: 'CodigoContacto', hidden: true },
	            { name: 'CodigoMoneda', index: 'CodigoMoneda', hidden: true },
	            { name: 'CodigoTipoProveedor', index: 'CodigoTipoProveedor', hidden: true },
	            { name: 'DescripcionBien', index: 'DescripcionBien', hidden: true },
                { name: 'Nombre', index: 'Nombre', hidden: true },
                { name: 'CodigoTipoProveedor', index: 'CodigoTipoProveedor', hidden: true }

	         ],
    height: '100%',
    pager: '#jqGrid_Pager_F',
    rowNum: 100,
    rowList: [10, 20, 30],
    sortname: 'invid',
    sortorder: 'desc',
    viewrecords: true,
    gridview: true,
    autowidth: true,
    altRows: true,
    altclass: 'gridAltClass',
    multiselect: false,
    onSelectRow: function(id) {
        var rowData = $("#jqGrid_lista_F").jqGrid('getRowData', id);
        $("#hidCodigoContratoProveedor").val(rowData.CodigoContratoProveedor);
    }
});
$("#search_jqGrid_lista_F").hide();
    
function EnvioCarta(cellvalue, options, rowObject) {
    if (rowObject.FlagEnvioCarta == "True") {
        return "<img src='../Util/images/ico_carta_enviado.gif' alt='" + cellvalue + "' title='Se Envió Carta' width='20px' />";
    } else {
        return "<img src='../Util/images/ico_carta_porenviar.gif' alt='" + cellvalue + "' title='No Envió Carta' width='20px' />";
    }
}

function Check(cellvalue, options, rowObject) {
    
      var arrmarcados = $("#hddPagChecked").val();

    if (rowObject.FlagEnvioCarta=="True") {
        $("#hddTotalDocumentoProveedor").val(rowObject.TotalImporte);
    }

        if (arrmarcados != "") {
            var sResultadoarrmarcados = arrmarcados.split("|");
            for (var i = 0; i < sResultadoarrmarcados.length; i++) {
                if (rowObject.CodigoContratoProveedor == sResultadoarrmarcados[i]) {
                	$("#hddTotalDocumentoProveedor").val(fn_util_ValidaDecimal($("#hddTotalDocumentoProveedor").val()) + fn_util_ValidaDecimal(rowObject.TotalImporte));
                	return "<input id='chkenvioCarta' name='chkenvioCarta' type='checkbox' checked runat='server' onclick='javascript:fn_seleccionaRegistro(this, " + rowObject.CodigoContratoProveedor + ")'/>";
                }
            }

        }
    
        return "<input id='chkenvioCarta" + rowObject.CodigoContratoProveedor + "' name='chkenvioCarta' type='checkbox' runat='server' value='"+ rowObject.FlagEnvioCarta + "' onclick='javascript:fn_seleccionaRegistro(this, " + rowObject.CodigoContratoProveedor + " )' />";
    }   

}


function fn_seleccionaRegistro(pCheck, pRegistro) {

    var pRegistros = $("#hddPagChecked").val();
    var pRegistrosNew="";

    if (pCheck.checked == true) {
        
        if (pRegistros.length==0) {
            pRegistros = pRegistro;    
        } else {
            pRegistros = pRegistros + "|" + pRegistro;
        }
       
    } else {

    var lblCheckedResult = pRegistros.split("|");

        for (var i = 0; i < lblCheckedResult.length; i++) {
            if (pRegistro != lblCheckedResult[i]) {
                if (pRegistrosNew.length == 0) {
                    pRegistrosNew = lblCheckedResult[i];
                } else {
                pRegistrosNew = pRegistrosNew + "|" + lblCheckedResult[i];
                }
                
            }
        }
        pRegistros = pRegistrosNew;
    }

    $("#hddPagChecked").val(pRegistros);
    var rowData = $("#jqGrid_lista_F").jqGrid('getRowData', parseInt(pRegistro));
   
    if (pCheck.checked == true) {
        if ($("#chkenvioCarta" + rowData.CodigoContratoProveedor).val() != "True") {
            $("#hddTotalDocumentoProveedor").val(fn_util_ValidaDecimal($("#hddTotalDocumentoProveedor").val()) + fn_util_ValidaDecimal(rowData.TotalImporte));
            }
            else {
            }
    } else {

    if ($("#chkenvioCarta" + rowData.CodigoContratoProveedor).val() != "True") {
            $("#hddTotalDocumentoProveedor").val(fn_util_ValidaDecimal($("#hddTotalDocumentoProveedor").val()) - fn_util_ValidaDecimal(rowData.TotalImporte));
            }else {
        }
    }

}

//****************************************************************
// Funcion		:: 	fn_buscarContratoProveedor
// Descripción	::	Buscar Contrato Proveedor
// Log			:: 	JRC - 24/05/2012
//****************************************************************
function fn_buscarContratoProveedor(intOrigen) {
        //debugger;
        var hidNumeroContrato = $('#hidNumeroContrato').val() == undefined ? "" : $('#hidNumeroContrato').val();
    
        var arrParametros = ["pPageSize",       fn_util_getJQGridParam("jqGrid_lista_F", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage",    fn_util_getJQGridParam("jqGrid_lista_F", "page"),    // Página actual.
                             "pSortColumn",     fn_util_getJQGridParam("jqGrid_lista_F", "sortname"), // Columna a ordenar.
                             "pSortOrder",      fn_util_getJQGridParam("jqGrid_lista_F", "sortorder"), // Criterio de ordenación.                     
                             "pNumeroContrato", hidNumeroContrato
                             ];

        fn_util_AjaxWM("frmSolicitudDocumentoProveedorRegistro.aspx/ListarContratoProveedor",
                        arrParametros,
                        function(jsondata) {
                            jqGrid_lista_F.addJSONData(jsondata);
                            fn_valida_monto(intOrigen);
                            parent.fn_unBlockUI();
                            fn_doResize();
                       },
                       function(request) {
                            fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                        });
}

//****************************************************************
// Funcion		:: 	fn_cancelar
// Descripción	::	cancelar
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cancelar() {
    parent.fn_mdl_confirma('¿Está seguro de volver?',
    function() {
            fn_util_redirect('frmSolicitudDocumentoProveedorListado.aspx');
    },
     "Util/images/question.gif",
    function() { },
    'Solicitud de Documentos - Proveedor'
 );
}

//****************************************************************
// Funcion		:: 	fn_grabar
// Descripción	::	Grabar
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_grabar() {
    fn_mdl_confirma('¿Esta seguro de actualizar este registro?',
            function() {
                parent.fn_blockUI();

                var hidNumeroContrato = $('#hidNumeroContrato').val() == undefined ? "" : $('#hidNumeroContrato').val();
                var arrParametros = ["pstrNumeroContrato", hidNumeroContrato,
                                    "pstrFechaTerminoRecepDocProv", Fn_util_DateToString($("#txtFechaTerminoRecepcion").val()),
                                    "pstrFlagFechaTerminoRecepDocProv", $("#chkTerminoRecepcion").is(':checked') ? '1' : '0',
                                    "pstrDescripcionBien", $("#txadescripcionbien").val()
                                    ];

                     fn_util_AjaxWM("frmSolicitudDocumentoProveedorRegistro.aspx/GuardarSolicitud",
                     arrParametros,
                     fn_MensajeYRedireccionarSolicitud,
                     function(resultado) {
                         var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                     });
           
            },
            "../util/images/question.gif",
            function() { },
            'Solicitud de Documentos - Proveedor'
         );
	$("#jqGrid_lista_F").jqGrid('resetSelection');

}

//****************************************************************
// Funcion		:: 	fn_enviarCarta
// Descripción	::	Enviar Carta
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_enviarCarta() {

    var vElementosAEnviar = $("#hddPagChecked").val();
    if (vElementosAEnviar.length == 0) {
        parent.fn_mdl_mensajeIco("Debe seleccionar al menos un Proveedor", "util/images/error.gif", "REGISTROS NECESARIOS");
    } else {

            if (fn_util_ValidaDecimal($("#hddTotalDocumentoProveedor").val()) <= fn_util_ValidaDecimal($("#txtPrecioVenta").val())) {
                var param = new StringBuilderEx();
                param.append("p1=" + $("#txtNumContrato").val());
                param.append("&p2=" + $("#hddPagChecked").val());
            	param.append("&p3=" + $("#txtRazonSocial").val());
            	
                parent.fn_util_AbreModal("Solicitud Proveedores :: Envio Carta", "Verificacion/frmEnviarCartaProv.aspx?" + param.toString(), 300, 120, function() { });
            }else{
                parent.fn_mdl_mensajeIco("El Total de Facturas es mayor al Precio de Venta", "util/images/error.gif", "ADVERTENCIA");    
            }
    }

}

//****************************************************************
// Función		:: 	fn_ResultadoGenerarAnexos
// Descripción	::	Abre anexos
// Log			:: 	WCR - 27/06/2012
//****************************************************************
function fn_ResultadoGenerarAnexos(result) {
    var vResult = result.split('|');    
    if (vResult[0] == "0") {

        if (vResult[1].length > 0) {            
            var vCarta = vResult[1].split(';');
            for (var i = 0; i < vCarta.length; i++) {
                if (vCarta[i] != '') {
                    var arrDatos = vCarta[i].split('*');                    
                    var strRutaArchivo = encodeURIComponent(arrDatos[0]);                                        
                    fn_abreArchivo(strRutaArchivo);
                    fn_util_MailTo(arrDatos[1], "IBK :: Sistema Leasing :: Contrato Nro." + $("#txtNumContrato").val(), $("#hidMensajeCorreo").val());
                }
            }
        }
        $("#hddPagChecked").val("");
        parent.fn_unBlockUI();
        fn_buscarContratoProveedor(0);
        window.fn_util_redirect('frmSolicitudDocumentoProveedorListado.aspx');
   }else {
        parent.fn_mdl_mensajeIco("Ocurrió un error al actualizar el contrato y generar los anexos.", "util/images/warning.gif", "ACTUALIZACIÓN DE CONTRATO");
  }
}

//****************************************************************
// Funcion		:: 	fn_abreArchivo 
// Descripción	::	Abre Archivo
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_abreArchivo(pstrRuta) {
    window.open("../frmDownload.aspx?nombreArchivo=" + pstrRuta);
    return false;
}

//****************************************************************
// Funcion		:: 	fn_agregarProveedor
// Descripción	::	Enviar Carta
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_agregarProveedor() {
    fn_GuardarNuevo();
}


//****************************************************************
// Funcion		:: 	fn_eliminarProveedor
// Descripción	::	Enviar Carta
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_eliminarProveedor() {
    fn_GuardarEliminar();
}


//****************************************************************
// Funcion		:: 	fn_editarProveedor
// Descripción	::	Enviar Carta
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_editarProveedor() {
    fn_GuardarEditar();
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {

	fn_seteaCamposObligatorios();
    $("#hddTotalDocumentoProveedor").val(0);
    $("#dv_botonesAgregar").show();
    $("#dv_botonesEditar").hide();
    $("#chkTerminoRecepcion").hide();
    $("#txtFechaTerminoRecepcion").hide();
	$("#txtCorreoProveedor").validText({ type: 'comment', length: 50 });
	$("#txadescripcionbien").validText({ type: 'comment', length: 500 });
	$("#txadescripcionbien").maxLength(500);	
    $("#txtRucProveedor").validText({ type: 'number', length: 11 });
    $('#txtcorreoProveedor').validText({ type: 'comment', length: 100 });
    $('#txtdescripcionbien').validText({ type: 'name', length: 100 });
    $('#txtcontacto').validText({ type: 'name', length: 100 });
    $('#tbCombo').hide();
    $("#txtNroCotizacion").validNumber({ value: '' });
    $("#txtCuCliente").validNumber({ value: '' });
    $('#txtRazonsocial').validText({ type: 'comment', length: 100 });
    $("#txtContrato").validNumber({ value: '' });
    $("#txtImporte").validNumber({ value: '0' });
    fn_doResize();

}


//****************************************************************
// Funcion		:: 	fn_LimpiarFecha
// Descripción	::	Limpiar Fecha
// Log			:: 	JRC - 24/05/2012
//****************************************************************
function fn_LimpiarFecha() {
    $("#chkTerminoRecepcion").is(':checked') ? $("#txtFechaTerminoRecepcion").val() : $("#txtFechaTerminoRecepcion").val('');
}


//****************************************************************
// Funcion		:: 	fn_seteaCamposObligatorios
// Descripción	::	Setea campos obligatorios
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_seteaCamposObligatorios() {

    fn_util_SeteaObligatorio($("#txtRucProveedor"), "input");
    fn_util_SeteaObligatorio($("#txtCorreoProveedor"), "input");
	 fn_util_SeteaObligatorio($("#txadescripcionbien"), "input");
	fn_util_SeteaObligatorio($("#txtContacto"), "input");
	fn_util_SeteaObligatorio($("#txtImporte"), "input");
	fn_util_SeteaObligatorio($("#cmbMoneda"), "select");
	fn_util_SeteaObligatorio($("#cmbContacto"), "select");
	
}


//****************************************************************
// Funcion		:: 	fn_abreBusquedaProveedor
// Descripción	::	Abre la busqueda de Proveedores
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_abreBusquedaProveedor() {
    parent.fn_util_AbreModal("Formalización :: Búsqueda de Proveedor", "Comun/frmProveedorConsulta.aspx", 950, 600, function() { });
}

//*****************************************************************
// Funcion		:: 	fn_obtenerProveedor
// Descripción	::	Obtiene el registro seleccionado de la busqueda
//                  de proveedores
// Log			:: 	WCR - 15/05/2012
//*****************************************************************
function fn_obtenerProveedor(pCodProveedor, pCodigoTipoDocumento, pRuc, pRazonSocial, tiponacionalidad) {

    $('#hddCodProveedor').val(pCodProveedor);
    $('#txtRucProveedor').val(pRuc);
    $('#cmbTipoProveedor').val(tiponacionalidad);
    $('#txtRazonSocialProveedor').val(pRazonSocial);
    fn_listaContacto(pCodProveedor);

}

//*****************************************************************
// Funcion		:: 	fn_buscarProveedor
// Descripción	::	Obtiene el registro del proveedor ingresando el 
//                  Ruc
// Log			:: 	WCR - 15/05/2012
//*****************************************************************
function fn_buscarProveedor() {
    var strRuc = $('#txtRucProveedor').val();
    if (strRuc != '') {
        var arrParametros = ["pstrRuc", strRuc, "pstrTipoDocumento", ""];
        var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whProveedor', '../');
        if (arrResultado.length > 0) {
            if (arrResultado[0] == "0") {
                $('#hddCodProveedor').val(arrResultado[1]);
                $('#txtRazonSocialProveedor').val(arrResultado[2]);
                fn_listaContacto(arrResultado[1]);

            }
            else {

                var strError = arrResultado[1];
                fn_mdl_alert(strError.toString(), function() {
                    $('#txtRucProveedor').val('');
                    $('#txtRucProveedor').focus();
                    $('#txtRazonSocialProveedor').val('');
                    $('#cmbTipoProveedor').val(0);

                });
            }
        }
    }
    else {
        $('#hddCodProveedor').val('');
        $('#txtRazonSocialProveedor').val('');
        $('#hidCodContacto').val('0');
        $('#txtContacto').val('');
        $('#txtCorreoProveedor').val('');
        $('#tbCombo').hide();
        $('#tbTexto').show();
    }
}

//*****************************************************************
// Funcion		:: 	fn_listaContacto
// Descripción	::	Obtiene el listado de los contactos del 
//                  proveedor
// Log			:: 	WCR - 15/05/2012
//*****************************************************************
function fn_listaContacto(pCodProveedor) {
    var arrParametros = ["pstrCodProveedor", pCodProveedor];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whContacto', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbContacto').html(arrResultado[1]);
            $('#hidValueContacto').val(arrResultado[2]);

            if (arrResultado[1].length > 41) {
                $('#tbCombo').show();
                $('#tbTexto').hide();
            }
            else {
                $('#tbCombo').hide();
                $('#tbTexto').show();
            }
        }
        else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
}

//*****************************************************************
// Funcion		:: 	fn_agregarContacto
// Descripción	::	Habilita control texto para ingresar un nuevo 
//                  contacto
// Log			:: 	WCR - 15/05/2012
//*****************************************************************
function fn_agregarContacto() {
    $('#txtContacto').val('');
    $('#txtCorreoProveedor').val('');
    $('#hidCodContacto').val('0');
    $('#tbCombo').hide();
    $('#tbTexto').show();
    return false;
}


//*****************************************************************
// Funcion		:: 	fn_agregarContacto
// Descripción	::	cancelar el control texto para el ingreso de un
//                  contacto
// Log			:: 	WCR - 15/05/2012
//*****************************************************************
function fn_cancelarContacto() {
    var vcmbContacto = document.getElementById("cmbContacto");
    if (vcmbContacto.options.length > 1) {
        vcmbContacto.selectedIndex = 0;
        $('#hidCodContacto').val('0');
        $('#tbCombo').show();
        $('#tbTexto').hide();
    }
    return false;
	$("#jqGrid_lista_F").jqGrid('resetSelection');
}


//*****************************************************************
// Funcion		:: 	fn_selectContacto
// Descripción	::	Obtiene el correo del contacto seleccionado
// Log			:: 	WCR - 15/05/2012
//*****************************************************************
function fn_selectContacto() {
    var arrDatoAdicional = $('#hidValueContacto').val().split(';');
    var vcmbContacto = document.getElementById("cmbContacto");
    if (vcmbContacto.selectedIndex > 0) {
        for (var i = 0; i < arrDatoAdicional.length - 1; i++) {
            if (arrDatoAdicional[i] != '') {
                var arrDato = arrDatoAdicional[i].split('*');
                if (arrDato[0] == vcmbContacto.value) {
                    $('#hidCodContacto').val(arrDato[0]);
                    $('#txtCorreoProveedor').val(arrDato[1]);
                    break;
                }

            }
        }
    }
    else {
        $('#txtCorreoProveedor').val('');
    }
}


//****************************************************************
// Función		:: 	fn_GuardarEditar
// Descripción	::	Guarda los datos ingresados para un Contrato 
//                  Proveedor existente (operación editar).
// Log			:: 	WCR - 17/05/2012
//****************************************************************
function fn_GuardarEditar() {

    var id = $("#hidCodigoContratoProveedor").val();
   
    if (id != 0) {
            $("#dv_botonesAgregar").hide();
            $("#dv_botonesEditar").show();
            var rowData = $("#jqGrid_lista_F").jqGrid('getRowData', id);
            $("#txtRucProveedor").val(rowData.RUC);
            $("#txtRazonSocialProveedor").val(rowData.NombreInstitucion);
            $("#txtContacto").val(rowData.Nombre);
            $("#txtCorreoProveedor").val(rowData.Correo);
            $("#txtImporte").val(rowData.Importe);
            $("#cmbMoneda").val(rowData.CodigoMoneda);
            $("#cmbTipoProveedor").val(fn_util_trim(rowData.CodigoTipoProveedor));
            $("#txadescripcionbien").val(rowData.DescripcionBien);
    }else {
            parent.fn_mdl_mensajeIco("Seleccione un Registro", "util/images/error.gif", "REGISTROS NECESARIOS");
        
    }

}


//****************************************************************
// Función		:: 	fn_GuardarNuevo
// Descripción	::	Guarda los datos ingresados de un Contrato
//                  Proveedor
// Log			:: 	WCR - 17/05/2012
//****************************************************************
function fn_GuardarNuevo() {
    //debugger;
	fn_seteaCamposObligatorios();
    parent.fn_blockUI();

    var strError = new StringBuilderEx();

    var objtxadescripcionbien =      $('textarea[id=txadescripcionbien]');
    var objtxtRucProveedor =         $('input[id=txtRucProveedor]:text');
    var objtxtRazonSocialProveedor = $('input[id=txtRazonSocialProveedor]:text');
    var objcmbMoneda =               $('select[id=cmbMoneda]');
    var objcmbTipoProveedor =        $('select[id=cmbTipoProveedor]');
    var objtxtImporte =              $('input[id=txtImporte]:text');
    var objtxtCorreo =               $('input[id=txtCorreoProveedor]:text');
   
    if ($('#tbCombo').css('display') == 'none') {
        var objtxtContacto = $('input[id=txtContacto]:text');
        strError.append(fn_util_ValidateControl(objtxtContacto[0], 'Contacto', 1, ''));
    } else {
        var objcmbContacto = $('select[id=cmbContacto]');
        strError.append(fn_util_ValidateControl(objcmbContacto[0], 'Contacto', 1, ''));
    }
	
	if ((objcmbTipoProveedor.val()=="0") || (objtxtRazonSocialProveedor.val()=="")) {
	    strError.append(fn_util_ValidateControl(objtxtRucProveedor[0], 'Proveedor', 1, ''));
	}

	// strError.append(fn_util_ValidateControl(objtxtRucProveedor[0], 'Ruc', 1, ''));
   // strError.append(fn_util_ValidateControl(objtxtRazonSocialProveedor[0], 'Proveedor', 1, ''));
    strError.append(fn_util_ValidateControl(objcmbMoneda[0], 'Moneda', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtCorreo[0], 'Correo', 1, ''));
    //strError.append(fn_util_ValidateControl(objcmbTipoProveedor[0], 'Tipo Proveedor', 1, ''));
    strError.append(fn_util_ValidateControl(objtxadescripcionbien[0], 'Descripcion', 1, ''));
  
  
    if (fn_util_ValidaMonto($("#txtImporte").val(), 2) <= 0) {
            strError.append(fn_util_ValidateControl(objtxtImporte[0], 'Importe', 1, ''));
    } else {
            strError.append(fn_util_ValidateControl(objtxtImporte[0], 'Importe', 1, ''));
    }

    // VALIDA CORREO
    //----------------------------------
        if (fn_util_trim(objtxtCorreo[0].value) != "") {
            //strError.append(fn_util_ValidateControl(objtxtImporte[0], 'Importe', 1, ''));
            if (!fn_util_ValidateEmailPuntoComas(objtxtCorreo[0].value)) {
                strError.append('- Ingrese un Correo válido.<br />');
                
                
            }
    }
	var ImporteTotal;
   
    //Valida si hay Error
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    }
    else {
            // Contrato Soles
            if ($("#hidTipoVenta").val() == 0) {
            	
             if ($("#hidMonedaContrato").val() != $("#cmbMoneda").val()) {
             	parent.fn_unBlockUI();
                parent.fn_mdl_mensajeIco("No existe T.C. para la fecha   " + fn_util_ValidaStringFecha($("#hddFechaSolicitudCredito").val()) + " ", "util/images/error.gif", "REGISTROS NECESARIOS");
             	return; 
            	} else {
             	var vTipoCompra = 1;
             	ImporteTotal = fn_util_ValidaDecimal($("#txtImporte").val());
             	var arrParametros =     ["pCodigoContratoProveedor", $("#hidCodigoContratoProveedor").val(),
                                     "pCodProveedor", $("#hddCodProveedor").val(),
                                     "pCodigoContacto", $("#hidCodContacto").val(),
                                     "pCodigoTipoProveedor", $("#cmbTipoProveedor").val(),
                                     "pNumeroContrato", $("#hidNumeroContrato").val(),
                                     "pCodigoMoneda", $("#cmbMoneda").val(),
                                     "pImporte", fn_util_ValidaDecimal($("#txtImporte").val()),
                                     "pTipoCambio", "V",
                                     "pMontoTipoCambio", $("#hidTipoVenta").val(),
                                     "pTotalImporte", ImporteTotal,
                                     "pNombreContacto", $("#txtContacto").val(),
                                     "pDescripcionBien", $("#txadescripcionbien").val(),
                                     "pCorreo", $("#txtCorreoProveedor").val()
                                    ];
            //alert(arrParametros);
            fn_util_AjaxSyncWM("frmSolicitudDocumentoProveedorRegistro.aspx/GuardarNuevo",
                    arrParametros,
                    function(resultado) {
                        $("#hddtotal").val(0);
                    	fn_buscarContratoProveedor(1);
                    	
                       if (resultado == "") {
                           $("#hddPagChecked").val(resultado);
                       } else {
                           $("#hddPagChecked").val($("#hddPagChecked").val() + '|' + (resultado));
                       }
                       
                        //fn_limpiar();
                        fn_MensajeYRedireccionar('grabaron');
                        $("#hddTotalDocumentoProveedor").val(0);
                        fn_Limpiar();
                    	fn_seteaCamposObligatorios();

                    },
                    function(resultado) {
                        parent.fn_unBlockUI();
                        var error = eval("(" + resultado.responseText + ")");
                        parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN EL REGISTRO");
                    }
                );
             }

            }else {
      
    	    if ($("#hidMonedaContrato").val() == strSoles) {
                    if ($("#cmbMoneda").val() == $("#hidMonedaContrato").val()) {
                        ImporteTotal = fn_util_ValidaDecimal($("#txtImporte").val());
                    } else {
                        ImporteTotal = fn_util_ValidaDecimal($("#txtImporte").val()) * fn_util_ValidaDecimal($("#hidTipoVenta").val());
                    }
            }

            //Contrato Dolares
            if ($("#hidMonedaContrato").val() == strDolares) {
                if ($("#cmbMoneda").val() == $("#hidMonedaContrato").val()) {
                    ImporteTotal = fn_util_ValidaDecimal($("#txtImporte").val());
                } else {
                    ImporteTotal = fn_util_ValidaDecimal($("#txtImporte").val()) / fn_util_ValidaDecimal($("#hidTipoCompra").val());
                }
            }

            var arrParametros =     ["pCodigoContratoProveedor", $("#hidCodigoContratoProveedor").val(),
                                     "pCodProveedor", $("#hddCodProveedor").val(),
                                     "pCodigoContacto", $("#hidCodContacto").val(),
                                     "pCodigoTipoProveedor", $("#cmbTipoProveedor").val(),
                                     "pNumeroContrato", $("#hidNumeroContrato").val(),
                                     "pCodigoMoneda", $("#cmbMoneda").val(),
                                     "pImporte", fn_util_ValidaDecimal($("#txtImporte").val()),
                                     "pTipoCambio", "V",
                                     "pMontoTipoCambio", $("#hidTipoVenta").val(),
                                     "pTotalImporte", ImporteTotal,
                                     "pNombreContacto", $("#txtContacto").val(),
                                     "pDescripcionBien", $("#txadescripcionbien").val(),
                                     "pCorreo", $("#txtCorreoProveedor").val()
                                    ];
            //alert(arrParametros);
            fn_util_AjaxSyncWM("frmSolicitudDocumentoProveedorRegistro.aspx/GuardarNuevo",
                    arrParametros,
                    function(resultado) {
                        $("#hddtotal").val(0);
                        fn_buscarContratoProveedor(1);
                    	  if (resultado == "") {
                           $("#hddPagChecked").val(resultado);
                       } else {
                           $("#hddPagChecked").val($("#hddPagChecked").val() + '|' + (resultado));
                       }
                        //fn_limpiar();
                        fn_MensajeYRedireccionar('grabaron');
                        $("#hddTotalDocumentoProveedor").val(0);
                        fn_Limpiar();
                    	fn_seteaCamposObligatorios();

                    },
                    function(resultado) {
                        parent.fn_unBlockUI();
                        var error = eval("(" + resultado.responseText + ")");
                        parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN EL REGISTRO");
                    }
                );
        }      
    }
}

//****************************************************************
// Función		:: 	fn_GuardarEliminar
// Descripción	::	Elimina logicamente el registro de Contrato
//                  Proveedor
// Log			:: 	WCR - 22/05/2012
//****************************************************************
function fn_GuardarEliminar() {

    var strError = new StringBuilderEx();
    if ($("#hidCodigoContratoProveedor").val() == '0') {
        parent.fn_mdl_mensajeIco("&nbsp;&nbsp;- Debe seleccionar un registro de la lista para eliminar", "Util/images/warning.gif", "ERROR EN ELIMINACIÓN");
    }
    else {
        parent.fn_mdl_confirma('¿Esta seguro de eliminar este registro?',
            function() {
                parent.fn_blockUI();

                var arrParametros = ["pCodigoContratoProveedor", $("#hidCodigoContratoProveedor").val()];

                fn_util_AjaxWM("frmSolicitudDocumentoProveedorRegistro.aspx/EliminarRegistro",
                            arrParametros,
                            function(resultado) {
                                $("#hddtotal").val(0);
                                fn_buscarContratoProveedor(0);
                                fn_MensajeYRedireccionar('eliminaron');
                                $("#hddTotalDocumentoProveedor").val(0);

                            },
                             function(resultado) {
                                 parent.fn_unBlockUI();
                                 var error = eval("(" + resultado.responseText + ")");
                                 parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA ELIMINACIÓN");
                             });
            },
             "Util/images/question.gif",

            function() { },
            'Solicitud de Documentos - Proveedor'
         );
        
        
    }
}

//****************************************************************
// Función		:: 	fn_MensajeYRedireccionar
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
// Log			:: 	EBL - 07/05/2012
//****************************************************************
var fn_MensajeYRedireccionar = function(pMensaje) {
    parent.fn_mdl_mensajeIco("Los datos se " + pMensaje + " satisfactoriamente", "util/images/ok.gif", "CONFIRMACION");
};

//****************************************************************
// Función		:: 	fn_MensajeYRedireccionarSolicitud
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
//                  Luego lo redirecciona al formulario de búsquedas ("frmSolicitudDocumentoProveedorListado.aspx").
// Log			:: 	EBL - 07/05/2012
//****************************************************************
var fn_MensajeYRedireccionarSolicitud = function() {
    fn_seteaCamposObligatorios();
    parent.fn_unBlockUI();
    parent.fn_mdl_alert('Los datos se grabaron satisfactoriamente', function() { fn_util_redirect("Verificacion/frmSolicitudDocumentoProveedorRegistro.aspx"); });
    
};

//****************************************************************
// Función		:: 	fn_selectContratoProveedor
// Descripción	::	Muestra los datos de la fila seleccionada en 
//                  grilla de contrato proveedor.
// Log			:: 	WCR - 22/05/2012
//****************************************************************
function fn_selectContratoProveedor(pData) {
    $('#hddCodProveedor').val(pData.CodProveedor);
    fn_listaContacto(pData.CodProveedor);
    $('#txtRucProveedor').val(pData.RUC);
    $('#hidCodigoContratoProveedor').val(pData.CodigoContratoProveedor);
    $('#txtRazonSocialProveedor').val(pData.NombreInstitucion);
    $('#hidCodContacto').val(pData.CodigoContacto);
    $('#cmbContacto').val(pData.CodigoContacto);
    $('#txtCorreoProveedor').val(pData.Correo);
    $('#cmbMoneda').val(pData.CodigoMoneda);
    $('#txtImporte').val(pData.Importe);
    $('#cmbTipoProveedor').val(fn_util_trim(pData.CodigoTipoProveedor));
    $('#txadescripcionbien').val(pData.DescripcionBien);
    $('#tbCombo').show();
    $('#tbTexto').hide();
    $('#divEditar').show();
    $('#divCancelar').show();
    $('#divAgregar').hide();

}

//****************************************************************
// Función		:: 	fn_Limpiar
// Descripción	::	Limpia los input para el registro o edición 
// Log			:: 	WCR - 22/05/2012
//****************************************************************
function fn_Limpiar() {
    $('#hddCodProveedor').val('0');
    $('#txtRucProveedor').val('');
    $('#hidCodigoContratoProveedor').val('0');
    $('#txtRazonSocialProveedor').val('');
    
    $('#hidCodContacto').val('0');
    $('#cmbContacto').val('0');
    $('#txtCorreoProveedor').val('');
    $('#cmbMoneda').val('0');
    $('#cmbMoneda').addClass('css_select').removeClass('css_select_error');
    
    $('#txtImporte').val('0.00');
    $('#txtImporte').addClass('css_input').removeClass('css_input_error');
    
    $('#cmbTipoProveedor').val('0');
    
    $('#txtContacto').val('');
    $('#cmbContacto')[0].options.length = 0;
    
    $('#tbCombo').hide();
    $('#tbTexto').show();
    $('#txadescripcionbien').val('');
    $('#txadescripcionbien').addClass('css_input').removeClass('css_input_error');

}

function fn_cancelarProveedor() {

    $("#dv_botonesAgregar").show();
    $("#dv_botonesEditar").hide();
    fn_Limpiar();
	$("#jqGrid_lista_F").jqGrid('resetSelection');

}


function fn_GuardaProveedor() {

  

    var strError = new StringBuilderEx();
  
    var objtxadescripcionbien = $('textarea[id=txadescripcionbien]');
    var objtxtRucProveedor = $('input[id=txtRucProveedor]:text');
    var objtxtRazonSocialProveedor = $('input[id=txtRazonSocialProveedor]:text');
    var objcmbMoneda = $('select[id=cmbMoneda]');
    var objcmbTipoProveedor = $('select[id=cmbTipoProveedor]');
    var objtxtImporte = $('input[id=txtImporte]:text');


    if ($('#tbCombo').css('display') == 'none') {
        var objtxtContacto = $('input[id=txtContacto]:text');
        strError.append(fn_util_ValidateControl(objtxtContacto[0], 'Contacto', 1, ''));
    } else {
        var objcmbContacto = $('select[id=cmbContacto]');
        strError.append(fn_util_ValidateControl(objcmbContacto[0], 'Contacto', 1, ''));
    }

    var objtxtCorreoProveedor = $('input[id=txtCorreoProveedor]:text');

    strError.append(fn_util_ValidateControl(objtxtRucProveedor[0], 'Ruc', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtRazonSocialProveedor[0], 'Proveedor', 1, ''));

    strError.append(fn_util_ValidateControl(objcmbMoneda[0], 'Moneda', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtCorreoProveedor[0], 'Correo', 1, ''));
    strError.append(fn_util_ValidateControl(objcmbTipoProveedor[0], 'Tipo Proveedor', 1, ''));
    strError.append(fn_util_ValidateControl(objtxadescripcionbien[0], 'Descripcion', 1, ''));

    if (fn_util_ValidaMonto($("#txtImporte").val(), 2) <= 0) {
        strError.append(fn_util_ValidateControl(objtxtImporte[0], 'Importe', 1, ''));
    }else {
        strError.append(fn_util_ValidateControl(objtxtImporte[0], 'Importe', 1, ''));
    }

    // VALIDA CORREO
    //----------------------------------
    if (fn_util_trim(objtxtCorreoProveedor[0].value) != "") {
        if (!fn_util_ValidateEmailPuntoComas(objtxtCorreoProveedor[0].value)) {
            strError.append('- Ingrese un Correo válido.<br />');
        }
    }
  


    //Valida si hay Error
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;

    }
    else {

         if ($("#hidTipoVenta").val() == 0) {
         	var vTipoCompra = 1;
             if ($("#hidMonedaContrato").val() != $("#cmbMoneda").val())
            	{
             	parent.fn_unBlockUI();
                parent.fn_mdl_mensajeIco("No existe T.C. para la fecha   " + fn_util_ValidaStringFecha($("#hddFechaSolicitudCredito").val()) + " ", "util/images/error.gif", "REGISTROS NECESARIOS");
             	return; 
            	} else {
             	ImporteTotal = fn_util_ValidaDecimal($("#txtImporte").val());
             	
             	fn_mdl_confirma('¿Está seguro de actualizar los datos?',
    			function() {
    				  parent.fn_blockUI();
    	//..............................................
                var strcodcontprov = $("#hidCodigoContratoProveedor").val();
                var arrParametros = ["pCodigoContratoProveedor", $("#hidCodigoContratoProveedor").val(),
                                    "pCodProveedor", $("#hddCodProveedor").val(),           // rowData.CodProveedor,
                                    "pCodigoContacto", $("#hidCodContacto").val(),          //rowData.CodigoContacto,
                                    "pCodigoTipoProveedor", $("#cmbTipoProveedor").val(),   // rowData.CodigoTipoProveedor,
                                    "pNumeroContrato", $("#txtNumContrato").val(),
                                    "pCodigoMoneda", $("#cmbMoneda").val(),
                                    "pImporte", $("#txtImporte").val(),
                                    "pTipoCambio", "V",
                                    "pMontoTipoCambio", vTipoCompra,
                                    "pTotalImporte", ImporteTotal,
                                    "pNombreContacto", $("#txtContacto").val(),
                                    "pDescripcionBien", $("#txadescripcionbien").val(),
                                    "pCorreo", $("#txtCorreoProveedor").val()
                                    ];

                fn_util_AjaxWM("frmSolicitudDocumentoProveedorRegistro.aspx/GuardarEditar",
                arrParametros,
                function(resultado) {
                    // debugger;
                    $("#hddtotal").val(0);
                    fn_buscarContratoProveedor(1);

                    var rowData = $("#jqGrid_lista_F").jqGrid('getRowData', strcodcontprov);

                    $("#hddTotalDocumentoProveedor").val(0);
                },
                function(resultado) {
                    var error = eval("(" + resultado.responseText + ")");
                    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA EDICIÓN");
                });
    	
    	        $("#dv_botonesAgregar").show();
                $("#dv_botonesEditar").hide();

                fn_Limpiar();
                $("#jqGrid_lista_F").jqGrid('resetSelection');
    				parent.fn_mdl_mensajeIco("Se actualizó la información correctamente","util/images/ok.gif","SOLICITUD DE DOCUMENTOS - PROVEEDOR ");
    				
    				
    		},
    			"../util/images/question.gif",
    			function() {
    			},
    			'Solicitud de Documentos - Proveedor'
    		);
             }

            }else {       


        var vTipoCompra = $("#hidTipoCompra").val();
        //Moneda Soles
        if ($("#hidMonedaContrato").val() == strSoles) {
            if ($("#cmbMoneda").val() == $("#hidMonedaContrato").val()) {
                var ImporteTotal = $("#txtImporte").val();
            } else {
            ImporteTotal = fn_util_ValidaDecimal($("#txtImporte").val()) * fn_util_ValidaDecimal($("#hidTipoVenta").val());
            }
        }
        //Dolares
        if ($("#hidMonedaContrato").val() == strDolares) {
            if ($("#cmbMoneda").val() == $("#hidMonedaContrato").val()) {
                ImporteTotal = $("#txtImporte").val();
            } else {
            ImporteTotal = fn_util_ValidaDecimal($("#txtImporte").val()) / fn_util_ValidaDecimal($("#hidTipoCompra").val());
            }

        }
         	
    	fn_mdl_confirma('¿Está seguro de actualizar los datos?',
    			function() {
    				  parent.fn_blockUI();
    	//..............................................
                var strcodcontprov = $("#hidCodigoContratoProveedor").val();
                var arrParametros = ["pCodigoContratoProveedor", $("#hidCodigoContratoProveedor").val(),
                                    "pCodProveedor", $("#hddCodProveedor").val(),           // rowData.CodProveedor,
                                    "pCodigoContacto", $("#hidCodContacto").val(),          //rowData.CodigoContacto,
                                    "pCodigoTipoProveedor", $("#cmbTipoProveedor").val(),   // rowData.CodigoTipoProveedor,
                                    "pNumeroContrato", $("#txtNumContrato").val(),
                                    "pCodigoMoneda", $("#cmbMoneda").val(),
                                    "pImporte", $("#txtImporte").val(),
                                    "pTipoCambio", "V",
                                    "pMontoTipoCambio", vTipoCompra,
                                    "pTotalImporte", ImporteTotal,
                                    "pNombreContacto", $("#txtContacto").val(),
                                    "pDescripcionBien", $("#txadescripcionbien").val(),
                                    "pCorreo", $("#txtCorreoProveedor").val()
                                    ];

                fn_util_AjaxWM("frmSolicitudDocumentoProveedorRegistro.aspx/GuardarEditar",
                arrParametros,
                function(resultado) {
                    // debugger;
                    $("#hddtotal").val(0);
                    fn_buscarContratoProveedor(1);

                    var rowData = $("#jqGrid_lista_F").jqGrid('getRowData', strcodcontprov);

                    $("#hddTotalDocumentoProveedor").val(0);
                },
                function(resultado) {
                    var error = eval("(" + resultado.responseText + ")");
                    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA EDICIÓN");
                });
    	
    	        $("#dv_botonesAgregar").show();
                $("#dv_botonesEditar").hide();

                fn_Limpiar();
                $("#jqGrid_lista_F").jqGrid('resetSelection');
    				parent.fn_mdl_mensajeIco("Se actualizó la información correctamente","util/images/ok.gif","SOLICITUD DE DOCUMENTOS - PROVEEDOR ");
    				
    				
    		},
    			"../util/images/question.gif",
    			function() {
    			},
    			'Solicitud de Documentos - Proveedor'
    		)
    		;
    	//.......................................................
    	
    	
      


    }
}
}

//****************************************************************
// Función		:: 	fn_valida_monto
// Descripción	::	Valida si el Total de Facturas es mayor al Precio de Venta
// Log			:: 	IJM - 07/07/2012
//****************************************************************
function fn_valida_monto(intOrigen) {
    
        var rows = jQuery("#jqGrid_lista_F").jqGrid('getRowData');
        $("#hddtotal").val(0);
        for (var i = 0; i < rows.length; i++) {
            var row = rows[i];
            $("#hddtotal").val(fn_util_ValidaDecimal($("#hddtotal").val()) + fn_util_ValidaDecimal(row.TotalImporte));
        }
        $('#divTotal').html('<strong>TOTAL:</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + Fn_util_ReturnValidDecimal2($("#hddtotal").val()) + '&nbsp;&nbsp;');
        if ($("#jqGrid_lista_F").getGridParam("reccount") == 0) {
            $('#divTotal').html('<strong>TOTAL:</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + Fn_util_ReturnValidDecimal2($("#hddtotal").val(0)) + '&nbsp;&nbsp;');
        }

        if (intOrigen==1) {
            if (fn_util_ValidaDecimal($("#hddtotal").val()) > (fn_util_ValidaDecimal($("#txtPrecioVenta").val()))) {
                parent.fn_mdl_mensajeIco("El Total de Facturas es mayor al Precio de Venta", "util/images/error.gif", "ADVERTENCIA");
            }
        }
}


//****************************************************************
// Funcion		:: 	fn_listaEjecutivoLeasing
// Descripción	::	Lista Ejecutivos
// Log			:: 	IJM - 06/03/2012
//****************************************************************
function fn_ValidaBloqueo() {

    var strBloqueoExistente = $("#hddBloqueoExistente").val();
    var strBloqueoCodigo = $("#hddBloqueoCodigo").val();
    var strBloqueoCodUsuario = $("#hddBloqueoCodUsuario").val();
    var strBloqueoNomUsuario = $("#hddBloqueoNomUsuario").val();
    var strBloqueoFecha = $("#hddBloqueoFecha").val();

    if (fn_util_trim(strBloqueoExistente) == "1") {

        parent.fn_mdl_confirmaBloqueo(
                        "La Solicitud de Documento Proveedor está siendo modificada por el usuario (" + strBloqueoCodUsuario + ") " + strBloqueoNomUsuario + " desde la fecha " + strBloqueoFecha + " ¿Desea continuar con la modificación?"
                        , function() { fn_ActualizaBloqueo(strBloqueoCodigo) }
                        , "Util/images/img_bloqueo.gif"
                        , function() { fn_util_redirect('frmSolicitudDocumentoProveedorListado.aspx'); }
                        , null
        );

    }

}



//****************************************************************
// Funcion		:: 	fn_ActualizaBloqueo
// Descripción	::	Actualiza Bloqueo
// Log			:: 	JRC - 18/07/2012
//****************************************************************
function fn_ActualizaBloqueo(pstrBloqueoCodigo) {

    //Consulta Ultimus
    var arrParametros = ["pstrCodBloqueo", pstrBloqueoCodigo];
    fn_util_AjaxWM("frmSolicitudDocumentoProveedorRegistro.aspx/ActualizaBloqueo",
             arrParametros,
             function(result) {
                 parent.fn_unBlockUI();
                 $('#cmbEjecutivoLeasing').html(result);
             },
             function(resultado) {
                 parent.fn_unBlockUI();
                 var error = eval("(" + resultado.responseText + ")");
                 parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN BLOQUEO");
             }
    );

}