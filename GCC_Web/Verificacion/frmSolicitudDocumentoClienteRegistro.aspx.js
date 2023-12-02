//Variables Globales
var strCodigoModuloCliente = "001";

//*******************************************************************
// Funcion		:: 	JQUERY -Solicitud de Documentos Cliente Registro
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//******************************************************************
$(document).ready(function() {

    //Inicializa Campos
    fn_inicializaCampos();

    //Carga Grilla
    fn_cargaGrilla();
    //fn_ListarDocumentoCondiciones();

    //Valida Bloqueo
    fn_ValidaBloqueo();
    
    // On load Page (siempre al final)
    fn_onLoadPage();

});


//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	LISTADO DE REGISTRO DE DOCUEMNTOS
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() {
        $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            fn_ListarDocumentoCondiciones();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",             // Número de página actual.
            total: "PageCount",              // Número total de páginas.
            records: "RecordCount",          // Total de registros a mostrar.
            repeatitems: false,
            id: "CodigoContratoDocumento"    // Índice de la columna con la clave primaria.
        },
        colNames: ['', '', 'Carta', 'Tipo Documento', 'Nombre', '', ''],
        colModel: [
                { name: 'CodigoContratoDocumento', index: 'CodigoContratoDocumento', hidden: true },
                { name: '', index: '', width: 15, align: "center", formatter: check },
                { name: 'FlagCartaEnvio', index: 'FlagCartaEnvio', width: 15, align: "center", formatter: envioCarta },
                { name: 'TipoDocCond', index: 'TipoDocCond', width: 30, align: "center", sorttype: "string" },
                { name: 'Descripcion', index: 'Descripcion', align: "left", sorttype: "string" },
                { name: 'CodigoDocumento', index: 'CodigoDocumento', width: 15, align: "center", sortable: false, formatter: eliminar },
                //Inicio IBK - AAE - 03/10/2012 - Agrego nueva columna con descripción larga
                {name: 'DescripcionLarga', index: 'DescripcionLarga', hidden: true },
                //Fin IBK - AAE
            ],
        height: '60%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname:    'CodigoDocumento', // Columna a ordenar por defecto.
        sortorder:   'desc',            // Criterio de ordenación por defecto.
        viewrecords: true,              // Muestra la cantidad de registros.
        gridview:    true,
        autowidth:   true,
        altRows:     true,
        loadonce:    false,
        altclass:    'gridAltClass'
        });
        jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
        $("#search_jqGrid_lista_A").hide();

    function check(cellvalue, options, rowObject) {
        var arrmarcados = $("#hddPagChecked").val();
        // Inicio IBK - AAE - 03/10/2012 - Marco los documentos para envíó de correo al cliente
        if (arrmarcados != "") {
            var sResultadoarrmarcados = arrmarcados.split("|");
            for (var i = 0; i < sResultadoarrmarcados.length; i++) {
                if (rowObject.CodigoContratoDocumento == sResultadoarrmarcados[i]) {
                    // AAE - comento código original
                    //return "<input id='chkTipoDocCond' name='chkTerminoRecepcion' type='checkbox' checked runat='server' onclick=\"javascript:fn_seleccionaRegistro(this, " + rowObject.CodigoContratoDocumento + ",'" + rowObject.Descripcion + "');\" />";                    
                    return "<input id='chkTipoDocCond' name='chkTerminoRecepcion' type='checkbox' checked runat='server' onclick=\"javascript:fn_seleccionaRegistro(this, " + rowObject.CodigoContratoDocumento + ",'" + rowObject.DescripcionLarga + "');\" />";
                }
            }
        }
        //AAE - comento código original
        //return "<input id='chkTipoDocCond' name='chkTerminoRecepcion' type='checkbox' runat='server' onclick=\"javascript:fn_seleccionaRegistro(this, " + rowObject.CodigoContratoDocumento + ",'" + rowObject.Descripcion + "');\" />";
        return "<input id='chkTipoDocCond' name='chkTerminoRecepcion' type='checkbox' runat='server' onclick=\"javascript:fn_seleccionaRegistro(this, " + rowObject.CodigoContratoDocumento + ",'" + rowObject.DescripcionLarga + "');\" />";
        // Fin IBK - AAE

    }
    
    function envioCarta(cellvalue, options, rowObject) {
        if (rowObject.FlagCartaEnvio == "True") {
            return "<img src='../Util/images/ico_carta_enviado.gif' alt='" + cellvalue + "' title='Se envió la carta' width='20px' />";
        } else {
            return "<img src='../Util/images/ico_carta_porenviar.gif' alt='" + cellvalue + "' title='No se envió la carta' width='20px' />";
        }
    }

    function eliminar(cellvalue, options, rowObject) {
        if ((cellvalue == null) || (cellvalue == '') && strCodigoModuloCliente == fn_util_trim(rowObject.codigoOrigenCondicion)) {
            var sScript = "javascript:fn_EliminarNuevos(" + rowObject.CodigoContratoDocumento + ");";
            return "<img src='../Util/images/ico_acc_eliminar.gif' alt='" + cellvalue + "' title='Eliminar' width='20px' onclick='" + sScript + "' style='cursor: pointer;cursor: hand;' />";
        } else {
            return ".";
        }
    };
}

//****************************************************************
// Funcion		:: 	fn_cancelar
// Descripción	::	LISTADO DE REGISTRO DE DOCUEMNTOS
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cancelar() {
   
    parent.fn_mdl_confirma('¿Está seguro de volver?',
            function() {
                fn_util_redirect('frmSolicitudDocumentoClienteListado.aspx');
            },
             "Util/images/question.gif",
            function() { },
            'Solicitud de Documentos - Cliente'
         );
}

//****************************************************************
// Funcion		:: 	fn_grabar
// Descripción	::	Guardar
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_grabar() {
    parent.fn_blockUI();
    var arrParametros = [
                         "strFechaTerminoRecepCli", Fn_util_DateToString($("#txtFechaTerminoRecepcion").val()),
                         "strFlagFechaTerminoRecepCli", $("#chkTerminoRecepcion").is(':checked') ? '1' : '0',
                         "strCodigoContacto", $("#hidCodContacto").val(),
                         "strCodigocotizacion", $("#txtNroCotizacion").val(),
                         "strNumeroContrato", $("#txtNumeroContrato").val(),
                         "strNombre", $("#txtContacto").val(),
                         "strCorreo", $("#txtCorreo").val(),
                         "strTelefono", $("#txtTelefonos").val(),
                         "strAnexo", $("#txtAnexo").val(),
                         "strCargo", $("#cmbCargo option:selected").val()
                        ];
    fn_util_AjaxWM("frmSolicitudDocumentoClienteRegistro.aspx/Guardar",
                    arrParametros,
                    fn_MensajeYRedireccionar,
                    function(resultado) {
                        var error = eval("(" + resultado.responseText + ")");
                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR Al GUARDAR");
                     }
    );
    fn_seteaCamposObligatorios();
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
    fn_seteaCamposObligatorios();
    
	$('#txtnombredocumento').validText({ type: 'comment', length: 50 });
    $('#txtContacto').validText({ type: 'comment', length: 100 });
    $('#txtCorreo').validText({ type: 'comment', length: 50 });
    $('#txtTelefonos').validText({ type: 'number', length: 10 });
    $("#txtAnexo").validText({ type: 'number', length: 5 });
    $("#chkTerminoRecepcion").hide();
    $("#txtFechaTerminoRecepcion").hide();
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_seteaCamposObligatorios() {
    fn_util_SeteaObligatorio($("#txtContacto"), "input");
    fn_util_SeteaObligatorio($("#txtCorreo"), "input");
    fn_util_SeteaObligatorio($("#txtTelefonos"), "input");
    fn_util_SeteaObligatorio($("#cmbCondicionesAdicionales"), "select");
    fn_util_SeteaObligatorio($("#txtnombredocumento"), "input");
    fn_util_SeteaObligatorio($("#cmbCargo"), "select");
}

//****************************************************************
// Funcion		:: 	fn_validaTipoDocumento
// Descripción	::	Guardar
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_validaTipoDocumento(pstrTipo) {
    if (pstrTipo == "D") {
        $("#tr_documento").show();
        $("#tr_condicion").hide();
    } else {
        $("#tr_condicion").show();
        $("#tr_documento").hide();
    }
    $("#cmbCondicionesAdicionales option").eq("0").attr("selected", "selected");
    $("#txtnombredocumento").val("");

}

/****************************************************************
Funcion		:: 	fn_MensajeYRedireccionar
Descripción	::	Mensaje
Log			:: 	KCC - 16/05/2012
**************************************************************** */
var fn_MensajeYRedireccionar = function() {
    parent.fn_unBlockUI();
    parent.fn_mdl_mensajeIco("Los datos fueron registrados correctamente.", "util/images/ok.gif", "ACTUALIZACION CORRECTA");
};

/****************************************************************
Funcion		:: 	fn_ListarDocumentoCondiciones
Descripción	::	Listar
Log			:: 	KCC - 16/05/2012
**************************************************************** */
function fn_ListarDocumentoCondiciones() {
    var arrParametros = [
                         "pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"),
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"),
                         "pCodContrato", $("#txtNumeroContrato").val()
                        ];

    fn_util_AjaxWM("frmSolicitudDocumentoClienteRegistro.aspx/ListaDocumentosCondiciones",
                   arrParametros,
                   function(jsondata) {
                       jqGrid_lista_A.addJSONData(jsondata);
                       fn_doResize();
                   },
                   function(resultado) {
                       var error = eval("(" + resultado.responseText + ")");
                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
                   }
    );
}

/****************************************************************
Funcion		:: 	fn_EliminarNuevos
Descripción	::	Agregar
Log			:: 	KCC - 16/05/2012
**************************************************************** */
function fn_EliminarNuevos(pstrCodContratoDoc) {
    parent.fn_mdl_confirma("¿Est&aacute; seguro de eliminar Documento/Condici&oacute;n?",
		    function() {
		    var arrParametros = ["strCodContratoDoc", pstrCodContratoDoc,
                                 "pstrCodContrato", $("#txtNumeroContrato").val()
                                 ];

		    fn_util_AjaxWM("frmSolicitudDocumentoClienteRegistro.aspx/EliminarDocCond",
                           arrParametros,
                           function() {
                               fn_ListarDocumentoCondiciones();
                               fn_doResize();
                           },
                           function(resultado) {
                               var error = eval("(" + resultado.responseText + ")");
                               parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL ELIMINAR");
                           }
            );
		},
		 "Util/images/question.gif",
		 function() { },
		 "ELIMINAR DOCUMENTO/CONDICI&Oacute;N - CLIENTE"
	);
}

/****************************************************************
Funcion		:: 	fn_agregar
Descripción	::	Agregar
Log			:: 	KCC - 16/05/2012
**************************************************************** */
function fn_agregar() {
    var radio = $("#opcioncodumento");
    
    for (var i = 0; i < radio.length; i++) {
        if (radio[i].checked) {
            var strTipo = radio[i].value;
            break;
        }
    }
    if (strTipo == "D") {
        if ($("#txtnombredocumento").val() == '') {
            fn_mdl_alert("Debe Ingresar el Documento.", function() { }, "AGREGAR DOCUMENTO/CONDICI&Oacute;N - CLIENTE");
            return;
        }
    } else {
    if ($("#cmbCondicionesAdicionales").val() == '0') {
        fn_mdl_alert("Debe seleccionar una Condición.", function() { });
        return;
    } else {
            //Validar la Duplicidad de Condiciones
     	     var arrPara = ["pstrNumeroContrato", $("#txtNumeroContrato").val(),"pstrCodigoTipoCondicion",$("#cmbCondicionesAdicionales").val(),"pintFlagCartaEnvio",0];
     	     var arrResultado = fn_util_ejecutarASHX(arrPara, 'whValidaCondicionDocumento', '../');
            
             if (arrResultado.length > 0) {
                if (arrResultado[0] == '1') {
                    if (parseFloat(arrResultado[1]) > 0) {
                        fn_mdl_alert("Ya se ingresó la condición seleccionada", function() { });
                        return;
                    }
                }
             }
        }
    }

    var arrParametros = [
                         "pstrCodContrato", $("#txtNumeroContrato").val(),
                         "pstrDocumento",   $("#txtnombredocumento").val(),
                         "pstrCondicion",   $("#cmbCondicionesAdicionales").val()
                         ];

    fn_util_AjaxWM("frmSolicitudDocumentoClienteRegistro.aspx/AgregarDocCond",
                   arrParametros,
                   function(result) {
                       fn_ListarDocumentoCondiciones();

                       if (result == "") {
                           $("#hddPagChecked").val(result);
                       } else {
                           $("#hddPagChecked").val($("#hddPagChecked").val() + '|' + (result));
                       }
                       
                       parent.fn_mdl_mensajeIco("Datos Guardados", "util/images/ok.gif", "DOCUMENTOS CLIENTE");
                   },
                   function(result) {
                       var error = eval("(" + result.responseText + ")");
                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL AGREGAR");
                   }
                   );

   $("#cmbCondicionesAdicionales").val(0);
   $("#txtnombredocumento").val("");
            
}

//****************************************************************
// Funcion		:: 	fn_enviarCarta
// Descripción	::	Enviar Carta
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_enviarCarta() {
   // debugger;
    parent.fn_blockUI();

    //String Validación
    var strError = new StringBuilderEx();

    //Instancia Objetos
    var objtxtContacto = $('input[id=txtContacto]:text');
    var objtxtCorreo = $('input[id=txtCorreo]:text');
    var objtxtTelefonos = $('input[id=txtTelefonos]:text');
    var objcmbcontratofirma = $('select[id=cmbCargo]');

    strError.append(fn_util_ValidateControl(objcmbcontratofirma[0], 'cargo', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtContacto[0], 'un contacto válido', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtCorreo[0], 'un correo', 1, ''));
    strError.append(fn_util_ValidateControl(objtxtTelefonos[0], 'un teléfono válido', 1, ''));

    //Validaciones Adicionales
    if (fn_util_trim(objtxtCorreo[0].value) != "") {
        if (!fn_util_ValidateEmailPuntoComas(objtxtCorreo[0].value)) {
            strError.append('- Ingrese un correo válido');
        }
    }

    //Valida si hay Error
    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        fn_seteaCamposObligatorios();
    } else {

        if ($("#hddPagChecked").val() == "" || $("#hddPagChecked").val() == "0|") {
            parent.fn_mdl_mensajeIco("Debe seleccionar al menos un documento o condición", "util/images/error.gif", "REGISTROS NECESARIOS");
            fn_seteaCamposObligatorios();
            parent.fn_unBlockUI();
        } else {
            var check = $("#chkTerminoRecepcion").is(':checked') ? '1' : '0';
            var param = new StringBuilderEx();
            param.append("p1=" + Fn_util_DateToString($("#txtFechaTerminoRecepcion").val()));  
            param.append("&p2=" + check);
            param.append("&p3=" + $("#hidCodContacto").val());
            param.append("&p4=" + $("#txtNroCotizacion").val()) ;
            param.append("&p5=" + $("#txtNumeroContrato").val());
            param.append("&p6=" + $("#txtContacto").val()) ;
            param.append("&p7=" + $("#txtCorreo").val());
            param.append("&p8=" + $("#txtTelefonos").val());
            param.append("&p9=" + $("#txtAnexo").val()) ;
            param.append("&p10=" + $("#cmbCargo option:selected").val());
           param.append("&p11=" + $("#txtNombreCliente").val());
            param.append("&p12=" + $("#hddPagChecked").val());

            parent.fn_util_AbreModal("Solicitud Cliente :: Envío Correo", "Verificacion/frmEnviarCartaCli.aspx?" + param.toString(), 300, 120, function() { });
        }
    }
}

function fn_seleccionaRegistro(pCheck, pRegistro, pNombre) {    
    var pRegistros = $("#hddPagChecked").val();
    var pListaRegistros = $("#hidListaDocumento").val();
    var pRegistrosNew = "";
    var pListaRegistrosNew = "";

    if (pCheck.checked) {
       if (pRegistros.length == 0) {
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

    //****************************************************************
    // Funcion		::
    // Descripción	::	Paginacion Acumula los Check Marcados
    //              ::  
    // Log			:: 	IJM - 17/07/2012
    //****************************************************************
    if (pCheck.checked == true) {
        pListaRegistros = pListaRegistros + "- " + pNombre + "|";
    } else {
        var lblCheckedRes = pListaRegistros.split("|");
        for (var j = 0; j < lblCheckedRes.length; j++) {
            if (fn_util_trim(lblCheckedRes[j]) != '') {
                if (fn_util_trim("- " + pNombre) != fn_util_trim(lblCheckedRes[j])) {                                
                    pListaRegistrosNew = pListaRegistrosNew + lblCheckedRes[j] + '|';
                }
            } 
        }
        pListaRegistros = pListaRegistrosNew;
    }    

    $("#hddPagChecked").val(pRegistros);
    $("#hidListaDocumento").val(pListaRegistros);    
}

//****************************************************************
// Funcion		:: 	fn_listaEjecutivoLeasing
// Descripción	::	Lista Ejecutivos
// Log			:: 	JRC - 18/07/2012
//****************************************************************
function fn_ValidaBloqueo() {

    var strBloqueoExistente = $("#hddBloqueoExistente").val();
    var strBloqueoCodigo = $("#hddBloqueoCodigo").val();
    var strBloqueoCodUsuario = $("#hddBloqueoCodUsuario").val();
    var strBloqueoNomUsuario = $("#hddBloqueoNomUsuario").val();
    var strBloqueoFecha = $("#hddBloqueoFecha").val();

    if (fn_util_trim(strBloqueoExistente) == "1") {

        parent.fn_mdl_confirmaBloqueo(
                        "La Solicitud de Documento Cliente está siendo modificada por el usuario (" + strBloqueoCodUsuario + ") " + strBloqueoNomUsuario + " desde la fecha " + strBloqueoFecha + " ¿Desea continuar con la modificación?"
                        , function() { fn_ActualizaBloqueo(strBloqueoCodigo); }
                        , "Util/images/img_bloqueo.gif"
                        , function() { fn_util_redirect('frmSolicitudDocumentoClienteListado.aspx'); }
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
    
    fn_util_AjaxWM("frmSolicitudDocumentoClienteRegistro.aspx/ActualizaBloqueo",
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