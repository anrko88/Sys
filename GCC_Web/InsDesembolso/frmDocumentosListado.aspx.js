var strComprobante = new Object();
    strComprobante.Factura									='01';
    strComprobante.CodigoDua								='04';
    strComprobante.CodigoNoDomiciliado						='15';
    strComprobante.CodigoReciboHonorarioNoDomiciliado		='30';    
    strComprobante.CodigoNotaDebito							='08';
    strComprobante.CodigoNotaCredito						='09';
    strComprobante.CodigoNotaDebidoNoDomiciliado			='98';
    strComprobante.CodigoNotaCreditoNoDomiciliado			='97';
    strComprobante.CodigoNotaCreditoEspecial				='87';
    strComprobante.CodigoNotaDebidoEspecial					='88';
    strComprobante.ReciboHonorario							='11';
    
var strCodigoEstadoDocumento = new Object();
    strCodigoEstadoDocumento.Formalizado =  '2';
    strCodigoEstadoDocumento.Desembolsado = '3';
    strCodigoEstadoDocumento.Anulado =      '4';
    
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 03/10/2012
//****************************************************************
$(document).ready(function() {
    
    //Inicializa Grillas
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
    $("#jqGrid_lista_C").jqGrid({
        datatype: function() {
            fn_ListaDocumentos();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage" , // Número de página actual.
            total: "PageCount",     // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodProveedor,TipoDocumento,NroDocumento,FechaEmision" // Índice de la columna con la clave primaria.
        },
        colNames: ['', '', '', '', 'Tipo Doc.', 'Nº Documento', 'Proveedor', 'Tipo Comprobante', 'Moneda', 'Gravado', 'IGV', 'No Gravado', 'Total', 'Total Convertido', 'Desembolso', 'Estado', 'CodEstado', 'CodMoneda'],
        colModel: [
                { name: 'CodProveedor', index: 'CodProveedor', width: 0, hidden: true, sorttype: "string" },
                { name: 'TipoDocumento', index: 'TipoDocumento', width: 0, hidden: true, sorttype: "string" },
                { name: 'NroDocumento', index: 'NroDocumento', width: 0, hidden: true, sorttype: "string" },
                { name: 'FechaEmision', index: 'FechaEmision', width: 0, hidden: true },
                { name: 'TipoDocumentoProveedor', index: 'TipoDocumentoProveedor', width: 80 },
                { name: 'NumeroDocumentoProveedor', index: 'NumeroDocumentoProveedor', width: 120, align: "left" },
                { name: 'NombreProveedor', index: 'NombreProveedor', width: 250, align: "left" },
                { name: 'NombreTipoDocumento', index: 'NombreTipoDocumento', width: 130, sorttype: "string" },
                { name: 'NombreMoneda', index: 'NombreMoneda', width: 150, align: "left" },
                { name: 'MontoGravado', index: 'MontoGravado', width: 150, align: "right", hidden: true, formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'MontoIGV', index: 'MontoIGV', width: 150, align: "right", hidden: true, formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'MontoNoGravado', index: 'MontoNoGravado', width: 150, align: "right", hidden: true, formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'Total', index: 'Total', width: 150, align: "right", formatter: Fn_util_ReturnValidDecimal2 },                
                { name: 'TotalConvertido', index: 'TotalConvertido', width: 100, align: "right", formatter: Fn_util_ReturnValidDecimal2 },                
                { name: 'Desembolso', index: 'Desembolso', width: 90, align: "center", sortable: false, formatter: fnDesembolso },
                { name: 'NombreEstadoDocumento', index: 'NombreEstadoDocumento', width: 150, align: "center" },
                { name: 'EstadoDocumento', index: 'EstadoDocumento', hidden: true },
                { name: 'MonedaOriginal', index: 'MonedaOriginal', hidden: true, sorttype: "string" }
              ],
        height: '60%',
        pager: '#jqGrid_pager_C',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 30,
        rowList: [10, 20, 30],
        sortname: 'MontoPendienteProveedor', // Columna a ordenar por defecto.
        sortorder: 'desc', // Criterio de ordenación por defecto.
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
        var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id); 
            
            $("#hidCodigoEstadoDoc").val(rowData.EstadoDocumento);
            $("#hidCodProveedor").val(rowData.CodProveedor);
            $("#hidTipoDocumento").val(rowData.TipoDocumento);
            $("#hidNumeroDocumento").val(rowData.NroDocumento);
            $("#hidFecEmision").val(rowData.FechaEmision);

        }
    });
    jQuery("#jqGrid_lista_C").jqGrid('navGrid', '#jqGrid_pager_C', { edit: false, add: false, del: false });
    $("#jqGrid_lista_C").setGridWidth($(window).width());
    $("#search_jqGrid_lista_C").hide();

    function fnDesembolso(cellvalue, options, rowObject) {
        var param = "'" + rowObject.CodProveedor + "','" + rowObject.TipoDocumento + "','" + rowObject.NroDocumento + "','" + Fn_util_DateToString(rowObject.FechaEmision) + "','" + rowObject.MonedaOriginal + "','" + rowObject.TotalConvertido + "','" + rowObject.ContadorBienes + "'";
		var strCodID = $("#hddCodInsDesembolso").val();
		
		//alert(strCodID + " => |" + rowObject.CodInstruccionDesembolso + "| - |" + rowObject.CodAgrupacion + "|");

		if(fn_util_trim(rowObject.CodInstruccionDesembolso) == ""){
			return "<input id='chkDesembolso' name='chkDesembolso' type='checkbox' runat='server' onclick=\"javascript:fn_seleccionaRegistro(this, " + param + " )\" />";
		}
		else if(strCodID == rowObject.CodInstruccionDesembolso){
			 fn_seleccionaRegistroAuto(rowObject.CodProveedor, rowObject.TipoDocumento, rowObject.NroDocumento, Fn_util_DateToString(rowObject.FechaEmision));
			return "<input id='chkDesembolso' name='chkDesembolso' type='checkbox' runat='server' checked='checked' onclick=\"javascript:fn_seleccionaRegistro(this, " + param + " )\" />";
		}
		else if(strCodID != rowObject.CodInstruccionDesembolso){
			return "<input id='chkDesembolso' name='chkDesembolso' type='checkbox' runat='server' disabled='true' />";
		}
		else{
			return "<input id='chkDesembolso' name='chkDesembolso' type='checkbox' runat='server' onclick=\"javascript:fn_seleccionaRegistro(this, " + param + " )\" />";
		}
        
    };
    
}




//****************************************************************
// Funcion		:: 	fn_ListaDocumentos
// Descripción	::	Lista Documentos
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_ListaDocumentos() {
    var arrParametros = ["pPageSize",    fn_util_getJQGridParam("jqGrid_lista_C", "rowNum"),
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_C", "page"),
                         "pSortColumn",  fn_util_getJQGridParam("jqGrid_lista_C", "sortname"),
                         "pSortOrder",   fn_util_getJQGridParam("jqGrid_lista_C", "sortorder"),
                         "pCodContrato", $("#hddCodContrato").val()
                         ];
    fn_util_AjaxSyncWM("frmDocumentosListado.aspx/ListaDocumentos",
                   arrParametros,
                   function(jsondata) {
					   jqGrid_lista_C.addJSONData(jsondata);
                       fn_VisualizarTablaTotal();
                       fn_doResize();
                   },
                   function(resultado) {
                       var error = eval("(" + resultado.responseText + ")");
                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL LISTAR");
                   }
    );
}

//****************************************************************
// Funcion		:: 	fnVisualizarTablaTotal
// Descripción	::	
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_VisualizarTablaTotal() {
    var strHtml;
    strHtml = "<table><tr><th>CANTIDAD DOCS. :</th>";
    strHtml = strHtml + "<td>" + $("#jqGrid_lista_C").getGridParam("reccount") + "</td><td>&nbsp;&nbsp;&nbsp;&nbsp;</td><th>MONTO TOTAL :</th>";
    var decTotal = 0;
    var rows = jQuery("#jqGrid_lista_C").jqGrid('getRowData');
    for (var i = 0; i < rows.length; i++) {		
        var row = rows[i];
        var totalRegistro = fn_util_ValidaDecimal(row.TotalConvertido);
        var tipoDocumento = row.TipoDocumento;
        
        if(tipoDocumento == strComprobante.CodigoNotaDebito || tipoDocumento == strComprobante.CodigoNotaCredito){
			totalRegistro = totalRegistro*-1;			
        }
        
        decTotal = decTotal + totalRegistro;
    }
    
    strHtml = strHtml + "<td>" + fn_util_RedondearDecimalesComas(decTotal, 2) + "</td></tr></table>";

    $('#divTotal').html(strHtml);
}


//****************************************************************
// Funcion		:: 	fn_seleccionaRegistro
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_seleccionaRegistro(pCheck, pCodProveedor, pTipoDocumento, pCodigoDocumento, pFechaEmision, pMoneda, pMonto,pContador,pEstadoDoc) {
    
    var pChekeados = $("#hidDesembolso").val();
    var pParametro = "'" + $("#hddCodContrato").val() + "" + pFechaEmision + "" + pTipoDocumento + "" + fn_util_trim(pCodigoDocumento) + "" + pCodProveedor + "'";
    var pChkComplete = '';
    
    if (pCheck.checked == true) {
        if (pParametro.length == 0) {
            pChekeados = pParametro;
        } else {
            pChekeados += pParametro + ",";
        }

    } else {
        var lblCheckedResult = pChekeados.split(",");
        for (var i = 0; i < lblCheckedResult.length; i++) {
            if (pParametro != lblCheckedResult[i]) {
                if (lblCheckedResult[i] != "") {
                    pChkComplete += lblCheckedResult[i] + ",";
                }
            }
        }
        pChekeados = pChkComplete;
    }

    $("#hidDesembolso").val(pChekeados);
}



//****************************************************************
// Funcion		:: 	fn_seleccionaRegistroAuto
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_seleccionaRegistroAuto(pCodProveedor, pTipoDocumento, pCodigoDocumento, pFechaEmision) {
    
    var pChekeados = $("#hidDesembolso").val();
    var pParametro = "'" + $("#hddCodContrato").val() + "" + pFechaEmision + "" + pTipoDocumento + "" + fn_util_trim(pCodigoDocumento) + "" + pCodProveedor + "'";
    
    if (pParametro.length == 0) {
        pChekeados = pParametro;
    } else {
        pChekeados += pParametro + ",";
    }

    $("#hidDesembolso").val(pChekeados);
}




//****************************************************************
// Funcion		:: 	fn_actualizarInsDesembolso
// Descripción	::	
// Log			:: 	
//****************************************************************
function fn_actualizarInsDesembolso() {
    if ($("#hidDesembolso").val() == "") {
        parent.fn_mdl_mensajeIco("No ha seleccionado nigún documento a desembolsar. No se puede actualizar la Instrucción de Desembolso.", "util/images/warning.gif", "DESEMBOLSAR");
    } else {
    fn_mdl_confirma(
                "Está seguro que desea actualizar la Instrucción de Desembolso??  ",
                function() {

                    parent.fn_blockUI();
                    var arrParametros = [
												"pstrCodContrato", $("#hddCodContrato").val(),
												"pstrCodInsDesembolso", $("#hddCodInsDesembolso").val(),
												"pstrChekeados", $("#hidDesembolso").val()
												];


                    fn_util_AjaxSyncWM("frmDocumentosListado.aspx/ActualizarID",
							arrParametros,
							function(resultado) {
							    parent.fn_unBlockUI();
							    var varresult = resultado.split("|");
							    if (varresult[0] == "0") {
							        parent.fn_mdl_mensajeOk("Los datos de la Instrucción de Desembolso fueron actualizados correctamente.",
								        function() {
								            fn_RedireccionActualizarID($("#hddCodContrato").val(), $("#hddCodInsDesembolso").val());

								        }, "ACTUALIZACIÓN CORRECTA");
							    } else {
							        parent.fn_mdl_mensajeIco(varresult[1], "util/images/error.gif", "ERROR EN GENERACIÓN");
							    }
							},
							function(resultado) {
							    parent.fn_unBlockUI();
							    var error = eval("(" + resultado.responseText + ")");
							    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GENERACIÓN");
							}
						);



                },
                "../Util/images/question.gif",
                function() { },
                'CONFIRMACIÓN'
               );

              
       }
}
function fn_RedireccionActualizarID(pstrCodContrato, pstrCodInsDesembolso) {
    
	var ctrlBtn = window.parent.frames[0].document.getElementById('btnListaAgrupacion');
    ctrlBtn.click();
    parent.fn_util_CierraModal();
}
