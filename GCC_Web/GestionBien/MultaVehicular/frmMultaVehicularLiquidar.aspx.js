//****************************************************************
// Variables Globales
//****************************************************************

var blnPrimeraBusqueda;
var intPaginaActual = 1;

var idsAll = [];
var idsAllId = [];
var idsAllNoId = [];
var idsAllMontos = [];
var idsAllNoMontos = [];
var boolBus = false;
var boolAll = false;;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	??? - 02/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    fn_inicializaCampos();
	
	      $(document).keypress(function(e) {
	      if (e.which == 13) {
	          $("#hddCondicionMensaje").val('1');
            fn_Buscar(true);
        }
    });
	
	fn_cargaGrilla();
    //On load Page (siempre al final)
    fn_onLoadPage();
    
});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() {
$('#txtNroLote').validText({ type: 'number', length: 8 });
fn_util_SeteaObligatorio($('#txtNroLote'), "text");
//Inicio IBK - AAE - 14/02/2013 - Inicializo campos
$("#txtDescEstadoLote").attr('disabled', 'disabled');
$("#txtTotal").attr('disabled', 'disabled');
$("#txtFechaCobro").attr('disabled', 'disabled');
$("#txtDescConcepto").attr('disabled', 'disabled');
    //Inicio JJM IBK
    $("#txtMontoDevuelto").attr('disabled', 'disabled');
    $("#txtMontoReembolsar").attr('disabled', 'disabled');
    $("#txtMontoCheque").attr('disabled', 'disabled');
    $("#txtNroCheque").attr('disabled', 'disabled');
    $("#txtFechaPago").attr('disabled', 'disabled');
    //Fin JJM IBK
    $("#dv_img_botonCobraLote").hide();    
//Fin IBK	
}
//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 08/11/2012
//****************************************************************

	var dblTotal = 0.0;
	var strImporte = '';
	var idsOfSelectedRows = [];
function fn_cargaGrilla() {
	
	 var updateIdsOfSelectedRows = function (id, isSelected,pIndicador) {
    $("#hddRowId").val(id);
    var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
    	
    
   if ($('#jqg_jqGrid_lista_A_' + id).is(':disabled')) {
            $('#jqg_jqGrid_lista_A_' + id).attr("checked", false);
            if ($('#jqg_jqGrid_lista_A_' + id).is(':checked')) {
                jQuery("#jqGrid_lista_A").jqGrid().setSelection(id, false);
            }
        }
    	
    	if (fn_util_trim(rowData.EstadoPago) != '003') {
            if (!isSelected) {

                var index = $.inArray(id, idsAllNoId);
                if (index < 0) {
                    idsAllNoId.push(id);
                    idsAllNoMontos.push(fn_util_ValidaDecimal(rowData.Importe));
                }

                var indexU = $.inArray(id, idsAllId);
                if (indexU >= 0) {
                    idsAllId.splice(indexU, 1);
                    idsAllMontos.splice(indexU, 1);
                }
            }
            else if (isSelected) {
                var index = $.inArray(id, idsAllNoId);
                if (index >= 0) {
                    idsAllNoId.splice(index, 1);
                    idsAllNoMontos.splice(index, 1);
                }
                var indexU = $.inArray(id, idsAllId);
                if (indexU < 0) {
                    idsAllId.push(rowData.SecMulta);
                    idsAllMontos.push(fn_util_ValidaDecimal(rowData.Importe));
                }

            }
        }
        if (pIndicador == 0) { fn_CalculoTotal(); }

    };
	
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");	 
            fn_ListarMultaVehicular();
        },
   	   jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "SecMulta"
        },
        colNames: ['Nº Contrato','CU Cliente','Razón Social o Nombre','Nº Infracción','Tipo de Bien','Distrito/ Municipalidad','Placa','Marca','Modelo','Importe','Imp c/Dto','Estado de Pago','Estado de Cobro','','',''],
        colModel: [
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 50, sorttype: "string", align: "center" },
            { name: 'CodUnico', index: 'CodUnico', width: 70, sorttype: "string", align: "left" },
            { name: 'RazonSocial', index: 'RazonSocial', width: 120, sorttype: "string", align: "left" },
        	{ name: 'NroInfraccion', index: 'NroInfraccion', width: 80, sorttype: "string", align: "left" },
        	{ name: 'TipoBien', index: 'TipoBien', width: 80, align: "center", sorttype: "string" },
        	{ name: 'DistritoNombre', index: 'DistritoNombre', width: 80, align: "center", sorttype: "string"},
		    { name: 'Placa', index: 'Placa', width: 50, align: "center", sorttype: "string" },
		    { name: 'Marca', index: 'Marca', width: 50, align: "center", sorttype: "string" },
        	{ name: 'Modelo', index: 'Modelo', width: 50, align: "center", sorttype: "string" },
        	{ name: 'Importe', index: 'Importe', width: 50, align: "center", sorttype: "string" },
        	{ name: 'Descuento', index: 'Descuento', width: 50, align: "center", sorttype: "string" },
        	{ name: 'EstPago', index: 'EstPago', width: 50, align: "center", sorttype: "string" },
        	{ name: 'EstCobro', index: 'EstCobro', width: 50, align: "center", sorttype: "string" },
        	{ name: '', index: '', width: 1 },
        	{ name: 'EstadoPago', index: 'EstadoPago', width: 50, align: "center", sorttype: "string", hidden:true },
        	{ name: 'SecMulta', index: 'SecMulta', width: 50, align: "center", sorttype: "string" , hidden:true}
	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 5,
        rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        multiselect:true,
        altclass: 'gridAltClass',
         onSelectRow: function(id, isSelected) {
         	updateIdsOfSelectedRows(id, isSelected, 0);
        },
                onSelectAll: function(aRowids, isSelected) {

            if (isSelected) {
                for (var j = 0; j < idsAllNoId.length; j++) {
                    var indexU = $.inArray(idsAllNoId[j], idsAllId);
                    if (indexU < 0) {
                        if (idsAllNoMontos[j] != null) {
                            idsAllId.push(idsAllNoId[j]);
                            idsAllMontos.push(idsAllNoMontos[j]);
                        }
                    }
                }
                idsAllNoId = [];
                idsAllNoMontos = [];
            }
            else {
                for (var j = 0; j < idsAllId.length; j++) {
                    var indexU = $.inArray(idsAllId[j], idsAllNoId);
                    if (indexU < 0) {
                        if (idsAllMontos[j] != null) {
                            idsAllNoId.push(idsAllId[j]);
                            idsAllNoMontos.push(idsAllMontos[j]);
                        }
                    }
                }
                idsAllId = [];
                idsAllMontos = [];
            }
            boolAll = isSelected;
            var i, count, id;
            for (i = 0, count = aRowids.length; i < count; i++) {
                id = aRowids[i];
                updateIdsOfSelectedRows(id, isSelected, 1);
            }
            fn_CalculoTotal();
        }
    });
	
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    
}

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	AEP - 12/11/2012
//****************************************************************

function fn_Buscar(bSearch) {

    idsAll = [];
    idsOfSelectedRows = [];
    idsAllId = [];
    idsAllMontos = [];
    idsAllNoId = [];
    idsAllNoMontos = [];
	blnPrimeraBusqueda = bSearch;

	fn_Valida();
     boolBus = true;


	//fn_ListarImpuestoTodo();

}

function fn_CalculoTotal() {
    var decTotal = 0;
    for (var i = 0; i < idsAllMontos.length; i++) {
        decTotal = decTotal + parseFloat(idsAllMontos[i]);
    }
    $("#txtTotal").val(fn_util_AddCommas(fn_util_RedondearDecimales(decTotal, 2)));
    $("#hidTotal").val($("#txtTotal").val());
}

function fn_Valida() {

var strError = new StringBuilderEx();
    
	var objtxtLote = $('input[id=txtNroLote]:text');
	
	//Valida
	strError.append(fn_util_ValidateControl(objtxtLote[0], 'un Lote válido', 1, ''));
	//Valida error existente
		
		
	if (strError.toString() != '') {
        parent.fn_unBlockUI();
		if ($("#hddCondicionMensaje").val()=='1') {
	
	    fn_mdl_alert(strError.toString(), function() { });
	
		}else {
		parent.fn_mdl_alert(strError.toString(), function() { });
		}
        strError = null;
		$("#hddCondicionMensaje").val('');
		
       fn_util_SeteaObligatorio($('#txtNroLote'),"text");
		blnPrimeraBusqueda = false;
		$("#jqGrid_lista_A").GridUnload();
	    fn_cargaGrilla();
	} else {
	    //Inicio IBK - AAE - Obtengo el nro de lote correcto
	    var arrParametros = ["pNroLote", $("#txtNroLote").val()];

	    fn_util_AjaxWM("frmMultaVehicularLiquidar.aspx/CheckLote",
                           arrParametros,
                           function(resultado) {
                               //parent.fn_unBlockUI();
                               if (resultado == "-1") {
                                   parent.fn_mdl_mensajeError("No existe el lote", function() { }, "VALIDACIÓN");
                               } else {
                                   $("#txtNroLote").val(resultado);
                                   $("#hidNroLote").val(resultado);
                                   fn_ListarMultaVehicular();
                               }
                           },
                           function(resultado) {
                               parent.fn_unBlockUI();
                               parent.fn_mdl_mensajeError("No se pudo obtener el lote " + resultado.responseText, function() { }, "VALIDACIÓN");
                           }
            );

                           //fn_ListarMultaVehicular();
	//Fin IBK
		//fn_ListarMultaVehicular();
		
	}
}

//****************************************************************
// Funcion		:: 	fn_ListarMultaVehicularTodo
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	AEP - 01/02/2013
//****************************************************************
function fn_ListarMultaVehicularTodo() {
    //
    //Inicio IBK - AAE - 14/02/2013 - Obtener info del header del lote
    var arrParametros = ["pNroLote", $("#txtNroLote").val()];
    //Inicio IBK JJM
    fn_util_AjaxWM("frmMultaVehicularLiquidar.aspx/ObtenerHeaderLote",
			arrParametros,
			function(jsondata) {
			    $("#txtNroLote").val(jsondata.Items[0].CodNroLote);
			    $("#txtDescEstadoLote").val(jsondata.Items[0].DescCodEstadoLote);
			    $("#hidCodEstadoLote").val(jsondata.Items[0].CodEstadoLote);
			    $("#txtTotal").val(fn_util_AddCommas(fn_util_RedondearDecimales(jsondata.Items[0].TotalLote, 2)));

			    $("#txtFechaCobro").val(jsondata.Items[0].FechaCobro);
			    $("#txtDescConcepto").val(jsondata.Items[0].DescConcepto);
			    $("#hidConcepto").val(jsondata.Items[0].CodigoConcepto);

			    $("#txtMontoDevuelto").val(fn_util_AddCommas(fn_util_RedondearDecimales(jsondata.Items[0].DevueltoLote, 2)));
			    $("#txtMontoReembolsar").val(fn_util_AddCommas(fn_util_RedondearDecimales(jsondata.Items[0].ReembolsoLote, 2)));
			    $("#txtMontoCheque").val(fn_util_AddCommas(fn_util_RedondearDecimales(jsondata.Items[0].MontoCheque, 2)));
			    $("#txtNroCheque").val(jsondata.Items[0].NroCheque);
			    $("#txtFechaPago").val(jsondata.Items[0].FechaPago);

			    //Fin IBK JJM
			    //mouestro oculto el botón
			    // Si el estado del lote es diferente a Pagado, no puedo liquidarlo
			    if ($("#hidCodEstadoLote").val() == '02') {
			        $("#dv_img_botonCobraLote").show();
			    } else {
			        $("#dv_img_botonCobraLote").hide();
			    };

			},
			function(resultado) {
			    parent.fn_unBlockUI();
			    var error = eval("(" + resultado.responseText + ")");
			    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
			}
		);


    //Fin IBK
    $("#hidNroLote").val($("#txtNroLote").val());
    var arrParametros = ["pNroLote", $("#txtNroLote").val()];

    fn_util_AjaxWM("frmMultaVehicularLiquidar.aspx/ListaMultaVehicularLiquidarTodo",
			arrParametros,
			function(jsondata) {
			    for (var i = 0; i < jsondata.Items.length; i++) {
			        if (fn_util_trim(jsondata.Items[i].EstadoPago) != '003') {
			            idsAllId.push(jsondata.Items[i].SecMulta);
			            idsAllMontos.push(jsondata.Items[i].Importe);
			        }
			    }
			    fn_DeshabilitarCheck();
				   if (idsAllNoId.length == 0) {
			        $("#cb_jqGrid_lista_A").attr('checked', true);
			    }
			    fn_CalculoTotal();
			    boolBus = false;
			},
			function(resultado) {
			    parent.fn_unBlockUI();
			    var error = eval("(" + resultado.responseText + ")");
			    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
			}
		);
}

//****************************************************************
// Funcion		:: 	fn_ListarImpuesto
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	AEP - 12/11/2012
//****************************************************************
function fn_ListarMultaVehicular() {
 if (!blnPrimeraBusqueda) {
        return;
    }else {

 	var strError = new StringBuilderEx();

 	var objtxtLote = $('input[id=txtNroLote]:text');

 	//Valida
 	strError.append(fn_util_ValidateControl(objtxtLote[0], 'un Lote válido', 1, ''));
 	//Valida error existente
 	if (strError.toString() != '') {
 		parent.fn_unBlockUI();
 		parent.fn_mdl_alert(strError.toString(), function() {
 		});
 		strError = null;
 		fn_util_SeteaObligatorio($('#txtNroLote'), "text");
 	} else {
 		parent.fn_blockUI();
        $("#hidNroLote").val($("#txtNroLote").val());
 		var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),
 			"pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),
 			"pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"),
 			"pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"),
 			"pNroLote", $("#txtNroLote").val()
 		];

 		fn_util_AjaxWM("frmMultaVehicularLiquidar.aspx/ListaMultaVehicularLiquidar",
 			arrParametros,
 			function(jsondata) {
 				jqGrid_lista_A.addJSONData(jsondata);
 				if (boolBus == true) {
			        fn_ListarMultaVehicularTodo();
			    }
			    else {
			        fn_DeshabilitarCheck();
 					   if (idsAllNoId.length == 0) {
			        $("#cb_jqGrid_lista_A").attr('checked', true);
			    }
			    }
			    parent.fn_unBlockUI();
			    fn_doResize();
 			},
 			function(resultado) {
 				parent.fn_unBlockUI();
 				var error = eval("(" + resultado.responseText + ")");
 				parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
 			}
 		);
 	}
 }
}



/*****************************************************************
Funcion		:: 	fn_Deshabilitar Chek
Descripción	::	Genera Reporte
Log			:: 	AEP - 17/01/2013
***************************************************************** */

function fn_DeshabilitarCheck() {
	
	$("#jqGrid_lista_A").jqGrid('resetSelection');
    var ids = $("#jqGrid_lista_A").jqGrid('getDataIDs');
    for (var i = 0; i < ids.length; i++) {
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', ids[i]);
        if (fn_util_trim(rowData.EstadoPago) == '003') {
            $('#jqg_jqGrid_lista_A_' + ids[i]).attr("disabled", true);
            //            idsAllNoId.push(ids[i]);
            //            idsAllNoMontos.push(fn_util_ValidaDecimal(rowData.Importe));
        }
        else {
            var index = $.inArray(ids[i], idsAllId);
            if (index >= 0) {
                jQuery("#jqGrid_lista_A").jqGrid().setSelection(ids[i], true);
            }
        }
    }
    $("#cb_jqGrid_lista_A").attr('checked', boolAll);
}


/*****************************************************************
Funcion		:: 	fn_Reporte
Descripción	::	Genera Reporte
Log			:: 	AEP - 07/01/2013
***************************************************************** */

function fn_Reporte() {
	
	// GCCTS_AEP_20130212 - Se cambio la validación para exportar al excel.
	  if (idsAllId.length == 0) {
	parent.fn_mdl_mensajeIco("Seleccione al menos un registro para poder generar el reporte.", "util/images/warning.gif", "LIQUIDIDAR MULTA VEHICULAR");	
    } else {

        var strCodigo = '';

        for (var j = 0; j < idsAllId.length; j++) {
            strCodigo = strCodigo + idsAllId[j] + ',';
        }
	  	strCodigo = strCodigo.toString().substring(0, strCodigo.length - 1);
        $("#hddCodigos").val(strCodigo);
        $("#btnGenerar").click();

    }
}


//Inicio IBK - AAE - 14/02/2013
/*****************************************************************
Funcion		:: 	fn_CobrarLote
Descripción	::	Liquida Lote
Log			:: 	AAE - 14/02/2013
***************************************************************** */
function fn_CobrarLote() {
    var strFechaActual = obtiene_fecha();
    var strFechaCobro = $('#txtFechaCobro').val() == "" ? '01/01/1999' : $('#txtFechaCobro').val();
    if (fn_util_ComparaFecha(strFechaCobro, strFechaActual)) {
        parent.fn_mdl_mensajeIco("La fecha de cobro no puede ser menor a la fecha actual.", "util/images/warning.gif", "LIQUIDIDAR IMPUESTO VEHICULAR");
    }
    else {
        fn_mdl_confirma('¿Está seguro de liquidar el lote?',
    			function() {

    			    parent.fn_blockUI();

    			    var arrParametros = ["strNroLote", $("#hidNroLote").val()];

    			    fn_util_AjaxWM("frmMultaVehicularLiquidar.aspx/LiquidarLote",
    					arrParametros,
    					function(resultado) {
    					    parent.fn_unBlockUI();
    					    var arrResultado = resultado.split("|");
    					    if (arrResultado[1] == '0') {
    					        parent.fn_mdl_mensajeOk("Se liquidó correctamente el Lote.", function() { fn_ListarMultaVehicular() }, "Guardar Liquidar Lote");
    					    }
    					    if (arrResultado[1] == '-1') {
    					        parent.fn_mdl_mensajeIco(fn_util_trim(arrResultado[0]), "../../util/images/error.gif", "Error Liquidar Lote");
    					    }

    					},
    					function(resultado) {
    					    var error = eval("(" + resultado[0].responseText + ")");
    					    parent.fn_mdl_mensajeIco(error.Message, "../../util/images/error.gif", "ERROR LIQUIDAR LOTE");
    					});
    			},
    			"../../util/images/question.gif",
    			function() {
    			},
    			'LIQUIDAR IMPUESTO VEHICULAR'
    		);
    }
}
function obtiene_fecha() {
    var fecha_actual = new Date();

    var dia = fecha_actual.getDate();
    var mes = fecha_actual.getMonth() + 1;
    var anio = fecha_actual.getFullYear();

    if (mes < 10)
        mes = '0' + mes;
    if (dia < 10)
        dia = '0' + dia;
    //return (anio + mes + dia);
    return (dia + "/" + mes + "/" + anio);

}
//Fin IBK