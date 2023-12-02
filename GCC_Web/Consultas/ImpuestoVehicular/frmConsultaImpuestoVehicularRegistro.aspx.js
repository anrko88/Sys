//****************************************************************
// Variables Globales
//****************************************************************
var blnPrimeraBusqueda;
var intPaginaActual = 1;
var blnPrimeraBusquedaI;
var intPaginaActualI = 1;
var strCodigoBien = '';
var strPeriodos = '';

var C_GESTIONBIEN_IMPVEHICULAR = "005";



//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	AEP - 08/11/2012
//****************************************************************
$(document).ready(function() {
    //Valida Campos
    //debugger;
    fn_inicializaCampos();
    //    $(document).keypress(function(e) {
    //        if (e.which == 13) {
    //            fn_buscar(true);
    //        }
    //    });
    //Carga Grilla
    //debugger;
    if ($("#hddFechaTransferencia").val() != '') {
        blnPrimeraBusqueda = true;
        fn_cargaGrilla();
        $("#txtPlaca").attr('disabled', 'disabled');
        $("#dv_img_lote").css('display', 'none');
        $("#dv_separador").css('display', 'none');
        $("#dv_img_buscar").css('display', 'none');
        $("#dv_img_limpiar").css('display', 'none');
        $("#dv_editar").css('display', 'none');
        $("#dv_agregarImpuesto").css('display', 'none');
        $("#dv_eliminar_impuesto").css('display', 'none');

        
        parent.fn_util_AbreModal("Impuesto Vehicular :: Ver", "Consultas/ImpuestoVehicular/frmConsultaImpuestoVehicularRegistroAgregar.aspx?codImp=" +
        $("#hddCodImpuesto").val() + "&placa=" + $("#txtPlaca").val() + "&origen=2" + "&fechaT=" + $("#hddFechaTransferencia").val(), 900, 280, function() { });
        
    } else {
        if ($("#hddTipo").val() == '1') {
            blnPrimeraBusqueda = true;
            fn_cargaGrilla();
            $("#txtPlaca").attr('disabled', 'disabled');
            $("#dv_img_lote").css('display', 'none');
            $("#dv_separador").css('display', 'none');
            $("#dv_img_buscar").css('display', 'none');
            $("#dv_img_limpiar").css('display', 'none');
            $("#dv_agregarImpuesto").css('display', 'none');
            if ($("#hddCheque").val() != "") {
                // $("#dv_eliminar_impuesto").css('display', 'none');
            }
            
            parent.fn_util_AbreModal("Impuesto Vehicular :: Ver", "Consultas/ImpuestoVehicular/frmConsultaImpuestoVehicularRegistroAgregar.aspx?codImp=" +
            $("#hddCodImpuesto").val() + "&placa=" + $("#txtPlaca").val() + "&origen=2" + "&fechaT=" + $("#hddFechaTransferencia").val() + "&cheque=" + $("#hddCheque").val(), 900, 280, function() { });
	        	                    
            
        } else {
            fn_cargaGrilla();
            $("#dv_ver").css('display', 'none');
        }
    }
    fn_cargaGrillaImpuestos();

    //On load Page (siempre al final)
    fn_onLoadPage();


});


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	AEP - 12/11/2012
//****************************************************************
function fn_inicializaCampos() {

    $('#txtPlaca').validText({ type: 'alphanumeric', length: 6 });

}

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 08/11/2012
//****************************************************************
function fn_cargaGrilla() {

    //var mydata = 
    //          [
    //		    { CodSolicitudCredito: "10000001", CodUnico: " 0000000987", RazonSocial: "RIPLEY", TipoBien: "Automóvil",Placa: "xt15444",NroMotor: "11144dd", FechaAdquisicion: "15/10/2002",FechaInscripcion: "20/10/2010",Marca: "Toyota",Modelo: "Yaris",Clase: "Automovil", AnioFabricacion: "2002",SecFinanciamiento: "1"},
    //            { CodSolicitudCredito: "10000002", CodUnico: " 0000000981", RazonSocial: "RIPLEY", TipoBien: "Automóvil",Placa: "xt15gg4",NroMotor: "11147dd", FechaAdquisicion: "15/10/2002",FechaInscripcion: "20/10/2010",Marca: "Toyota",Modelo: "Yaris",Clase: "Automovil", AnioFabricacion: "2002",SecFinanciamiento: "1"},
    //          	{ CodSolicitudCredito: "10000003", CodUnico: " 0000000982", RazonSocial: "RIPLEY", TipoBien: "Automóvil",Placa: "xt15664",NroMotor: "11149dd", FechaAdquisicion: "15/10/2002",FechaInscripcion: "20/10/2010",Marca: "Toyota",Modelo: "Corolla",Clase: "Automovil", AnioFabricacion: "2002",SecFinanciamiento: "1"},
    //          	{ CodSolicitudCredito: "10000004", CodUnico: " 0000000983", RazonSocial: "RIPLEY", TipoBien: "Automóvil",Placa: "xt15774",NroMotor: "1114jdd", FechaAdquisicion: "15/10/2002",FechaInscripcion: "20/10/2010",Marca: "Toyota",Modelo: "Corolla",Clase: "Automovil", AnioFabricacion: "2002",SecFinanciamiento: "1"},
    //          	{ CodSolicitudCredito: "10000005", CodUnico: " 0000000984", RazonSocial: "RIPLEY", TipoBien: "Automóvil",Placa: "xt15yy4",NroMotor: "1114sdd", FechaAdquisicion: "15/10/2002",FechaInscripcion: "20/10/2010",Marca: "Toyota",Modelo: "Corolla",Clase: "Automovil", AnioFabricacion: "2002",SecFinanciamiento: "1"}
    //		  ];

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_ListarBienes();
        },
        // datatype: "local",
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Nro. Contrato', 'CU Cliente', 'Razón Social o Nombre', 'Tipo de Bien', 'Placa Actual', 'Nº Motor', 'F. Adquisición', 'F. Inscripción Registral', 'Marca', 'Modelo', 'Clase', 'Año Fabricación', '', '', '', '', ''],
        colModel: [
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 50, sorttype: "string", align: "center" },
            { name: 'CodUnico', index: 'CodUnico', width: 50, sorttype: "string", align: "left" },
            { name: 'RazonSocial', index: 'RazonSocial', width: 150, sorttype: "string", align: "left" },
        	{ name: 'TipoBien', index: 'TipoBien', width: 80, align: "center", sorttype: "string" },
		    { name: 'Placa', index: 'Placa', width: 50, align: "center", sorttype: "string" },
		    { name: 'NroMotor', index: 'NroMotor', width: 50, align: "center", sorttype: "string", editable: true },
		    { name: 'FechaAdquisicion', index: 'FechaAdquisicion', width: 60, align: "center" },
        	{ name: 'FechaInscripcionRegistral', index: 'FechaInscripcionRegistral', width: 60, align: "center" },
		    { name: 'Marca', index: 'Marca', width: 50, align: "center", sorttype: "string" },
        	{ name: 'Modelo', index: 'Modelo', width: 50, align: "center", sorttype: "string" },
        	{ name: 'Clase', index: 'Clase', width: 50, align: "center", sorttype: "string" },
        	{ name: 'Anio', index: 'Anio', width: 50, align: "center", sorttype: "string" },
        	{ name: '', index: '', width: 1 },
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
            { name: 'CodigoEstadoContrato', index: 'CodigoEstadoContrato', hidden: true },
        	{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
             { name: 'Descripcion', index: 'Descripcion', hidden: true }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddRowId").val(id);
            //$("#hidCodigoSolicitudCredito").val(rowData.CodSolicitudCredito);
        }
        //        ondblClickRow: function() {
        ////            parent.fn_blockUI();
        ////            fn_util_redirect('frmMantenimientoBienContrato.aspx?co=1&csc=' + $("#hidCodigoSolicitudCredito").val());
        //        	}
    });

    //	for (var i = 0; i <= mydata.length; i++) {
    //	     jQuery("#jqGrid_lista_A").jqGrid('addRowData', i + 1, mydata[i]);
    //	 }

    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

}

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	AEP - 14/11/2012
//****************************************************************
//function fn_buscar(bSearch) {
//    blnPrimeraBusqueda = bSearch;
//    fn_ListarBienes();
//}


//****************************************************************
// Funcion		:: 	fn_ListarImpuesto
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	AEP - 14/11/2012
//****************************************************************
function fn_ListarBienes() {
    if (!blnPrimeraBusqueda) {
        return;
    } else {


        parent.fn_blockUI();

        // GCCTS_AEP_20120212 - se agregó parametros al método.
        var arrParametros = ["pPageSize",    fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),
                             "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),
                             "pSortColumn",  fn_util_getJQGridParam("jqGrid_lista_A", "sortname"),
                             "pSortOrder",   fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"),
    	                     "pPlaca",       $("#txtPlaca").val(),
    	                     "pTipo",        $("#hddTipo").val(),
        	                 "pNroMotor", "",
    	                     "pCUCliente", "",
    	                     "pCodContrato", ""
                            ];

        fn_util_AjaxWM("frmConsultaImpuestoVehicularRegistro.aspx/ListaBienesVehicular",
                   arrParametros,
                   function(jsondata) {
                       jqGrid_lista_A.addJSONData(jsondata);
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
//****************************************************************
// Funcion		:: 	fn_cargaGrillaImpuestos
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 08/11/2012
//****************************************************************

var idsOfSelectedRows = [];
function fn_cargaGrillaImpuestos() {
    $("#hddSecFinanciamiento").val('');
    $("#hddPeriodos").val('');
    strCodigoBien = '';
    strPeriodos = '';
    strSecImpuesto = '';
    //    var mydata2 = 
    //          [
    //		    { CodSolicitudCredito: "10000001",Placa: "xt15444",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "170.00",NroCuota: "1",FechaPago: "16/11/2012",FechaCobro: "20/11/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"},
    //		    { CodSolicitudCredito: "10000002",Placa: "xt15445",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "80.00",NroCuota: "2",FechaPago: "16/12/2012",FechaCobro: "20/12/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"},
    //          	{ CodSolicitudCredito: "10000003",Placa: "xt15488",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "170.00",NroCuota: "1",FechaPago: "16/11/2012",FechaCobro: "20/11/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"},
    //		    { CodSolicitudCredito: "10000004",Placa: "xt15400",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "80.00",NroCuota: "2",FechaPago: "16/12/2012",FechaCobro: "20/12/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"}
    //		  ];

    var updateIdsOfSelectedRows = function(id, isSelected) {

        var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id);

        if (isSelected) {

            strCodigoBien = strCodigoBien + rowData.SecFinanciamiento + ",";
            strPeriodos = strPeriodos + rowData.Periodo + ",";
            strSecImpuesto = strSecImpuesto + id + ",";
        } else {

            var lblCheckedResult = strCodigoBien.split(","); //strCodigoBien.substring(0,strCodigoBien.length-1).split(",");
            var lblCheckedResult2 = strPeriodos.split(","); //strPeriodos.substring(0,strPeriodos.length-1).split(",");
            var lblChekedResult3 = strSecImpuesto.split(",");
            var pstrCodigoBien = "";
            var pstrSecImpuesto = "";
            var pstrPeriodos = "";
            for (var i = 0; i < lblChekedResult3.length; i++) {
                if (rowData.SecImpuesto != lblChekedResult3[i]) {
                    if (lblChekedResult3[i] != "") {
                        pstrCodigoBien += lblCheckedResult[i] + ",";
                        pstrPeriodos += lblCheckedResult2[i] + ",";
                        pstrSecImpuesto += lblChekedResult3[i] + ",";
                    }
                }

            }

            strCodigoBien = pstrCodigoBien;
            strPeriodos = pstrPeriodos;
            strSecImpuesto = pstrSecImpuesto;
        }

        $("#hddRowIdImpuesto").val(id);
        $("#hddSecFinanciamiento").val(strCodigoBien);
        $("#hddPeriodos").val(strPeriodos);
        $("#hddCodigosImpuesto").val(strSecImpuesto);

        var index = $.inArray(id, idsOfSelectedRows);
        if (!isSelected && index >= 0) {
            idsOfSelectedRows.splice(index, 1);
        } else if (index < 0) {
            idsOfSelectedRows.push(rowData.SecImpuesto);
        }

    };

    $("#jqGrid_lista_C").jqGrid({
        datatype: function() {
            fn_ListarImpuestosLote();
        },
        //datatype: "local",
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "SecImpuesto"
        },
        colNames: ['Nro. Contrato', 'Nro. Lote', 'Placa Actual', 'F. Declaración', 'Periodo', 'Importe', 'Nº Cuota', 'F. Pago', 'F. Cobro', 'Estado Pago', 'Estado Cobro', 'Observación', '', '', '', 'Docs.', ''],
        colModel: [
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 50, sorttype: "string", align: "center" },
            { name: 'NroLote', index: 'NroLote', width: 50, sorttype: "string", align: "center" },
            { name: 'Placa', index: 'Placa', width: 50, sorttype: "string", align: "center" },
            { name: 'FecDeclaracion', index: 'FecDeclaracion', width: 50, sorttype: "string", align: "center" },
        	{ name: 'Periodo', index: 'Periodo', width: 80, align: "center", sorttype: "string" },
		    { name: 'Importe', index: 'Importe', width: 50, align: "center", sorttype: "string", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'Cuota', index: 'Cuota', width: 50, align: "center", sorttype: "string", editable: true },
		    { name: 'FechaPago', index: 'FechaPago', width: 60, align: "center" },
        	{ name: 'FechaCobro', index: 'FechaCobro', width: 60, align: "center" },
        	{ name: 'EstadoPago', index: 'EstadoPago', width: 50, align: "center", sorttype: "string" },
		    { name: 'EstadoCobro', index: 'EstadoCobro', width: 50, align: "center", sorttype: "string" },
        	{ name: 'Observaciones', index: 'Observacion', width: 50, align: "center", sorttype: "string", formatter: observacion, alt: "fff" },
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
            { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
        	{ name: 'SecImpuesto', index: 'SecImpuesto', hidden: true },
            { name: 'Doc', index: 'Doc', align: "center", sortable: false, formatter: fn_abreDocumentos, width: 25 },
            { name: '', index: '', width: 2 }
	                ],
        height: '100%',
        pager: '#jqGrid_pager_C',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        multiselect: false,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: updateIdsOfSelectedRows,
        beforeSelectRow: function(id) {
          
            $("#hddRowIdImpuesto").val(id);
        },
        onSelectAll: function(aRowids, isSelected) {
            //            var i, count, id;

            //            for (i = 0, count = aRowids.length; i < count; i++) {
            //                id = aRowids[i];
            //                updateIdsOfSelectedRows(id, isSelected);
            //            }
        }

    });

    //	for (var i = 0; i <= mydata2.length; i++) {
    //	     jQuery("#jqGrid_lista_C").jqGrid('addRowData', i + 1, mydata2[i]);
    //	 }
    //	

    jQuery("#jqGrid_lista_C").jqGrid('navGrid', '#jqGrid_pager_C', { edit: false, add: false, del: false });

    $("#jqGrid_lista_C").setGridWidth($(window).width() - 70);
    $("#search_jqGrid_lista_C").hide();


    function observacion(cellvalue, options, rowObject) {

        if (rowObject.Observaciones == "") {
            return "";
        } else {
            var mensaje;
            mensaje = rowObject.Observaciones.toString().substring(0, 10) + " ...";
            return "<a href='#' class='css_lbl_comentario_grilla' alt='" + rowObject.Observaciones + "' title='" + rowObject.Observaciones + "'>" + mensaje + "</a>";
        }
    };


    //**************************************************
    // Documentos
    //**************************************************
    function fn_abreDocumentos(cellvalue, options, rowObject) {
        //return ".";
        return "<img src='../../Util/images/ico_docs.gif' alt='Ver Documentos' title='Ver Documentos' width='18px' onclick=\"javascript:fn_GBAbreDocumentos(\'" + rowObject.CodSolicitudCredito + "\',\'" + rowObject.SecFinanciamiento + "\',\'" + rowObject.SecImpuesto + "\',\'" + C_GESTIONBIEN_IMPVEHICULAR + "\');\" style='cursor:pointer;' />";
    };

}

//****************************************************************
// Funcion		:: 	fn_ListarImpuesto
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	AEP - 14/11/2012
//****************************************************************
function fn_ListarImpuestosLote() {

    parent.fn_blockUI();
    $("#hddRowIdImpuesto").val('');
    $("#hddSecFinanciamiento").val('');
    $("#hddPeriodos").val('');
    strCodigoBien = '';
    strPeriodos = '';
    strSecImpuesto = '';
    $("#jqGrid_lista_C").jqGrid('resetSelection');
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_C", "rowNum"),
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_C", "page"),
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_C", "sortname"),
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_C", "sortorder"),
    	                 "pPlaca", $("#txtPlaca").val(),
    	                 "pTipo", $("#hddTipo").val()
                         ];

    fn_util_AjaxWM("frmConsultaImpuestoVehicularRegistro.aspx/ListarLoteImpuestoVehicular",
                   arrParametros,
                   function(jsondata) {
                       jqGrid_lista_C.addJSONData(jsondata);
                       for (var i = 0, count = idsOfSelectedRows.length; i < count; i++) {
                           $("#jqGrid_lista_C").jqGrid('setSelection', idsOfSelectedRows[i], false);
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

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_limpiar() {

    blnPrimeraBusqueda = false;
    $("#txtPlaca").val('');
    $("#jqGrid_lista_A").GridUnload();
    fn_cargaGrilla();
}


//****************************************************************
// Funcion		:: 	fn_abreEliminarImpuesto
// Descripción	::	Abre Editar Registro
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_abreEliminarImpuesto() {
    var id = $("#hddRowIdImpuesto").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder eliminar.", "util/images/warning.gif", "ELIMINAR IMPUESTO");
    } else {
        var vElementosAEditar = $("#jqGrid_lista_C").getGridParam('selarrrow');

        if (vElementosAEditar != "") {
            var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', vElementosAEditar[0]);


            if (vElementosAEditar.length > 1) {
                parent.fn_mdl_mensajeIco("Seleccione un sólo registro.", "util/images/warning.gif", "ELIMINAR IMPUESTO");
            } else {

                if (rowData.EstadoPago == "Pagado") {

                    parent.fn_mdl_mensajeIco("No se puede eliminar el impuesto porque ya se encuentra pagado", "util/images/warning.gif", "ELIMINAR IMPUESTO");

                } else {

                    parent.fn_mdl_confirma(
           	        		"¿Está seguro que desea eliminar el impuesto?  ", //Mensaje - Obligatorio
           	        		function() {

           	        		    parent.fn_blockUI();

           	        		    var strCodigoImpuesto = $("#hddCodigosImpuesto").val().substring(0, $("#hddCodigosImpuesto").val().length - 1);

           	        		    var arrParametros = ["pstrCodigosImpuestos", strCodigoImpuesto];
           	        		    fn_util_AjaxWM("frmImpuestoVehicularRegistro.aspx/EliminarImpuestoVehicular",
           	        				arrParametros,
           	        				function(resultado) {
           	        				    fn_ListarImpuestosLote();
           	        				    parent.fn_unBlockUI();
           	        				},
           	        				function(resultado) {
           	        				    var error = eval("(" + resultado.responseText + ")");
           	        				    parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA ELIMINACIÓN");
           	        				}
           	        			);

           	        		},
           	        		"Util/images/question.gif",
           	        		function() {
           	        		},
           	        		'ELIMINAR IMPUESTO'
           	        	);

                }

            }
        } else {
            parent.fn_mdl_mensajeIco("Seleccione un registro para poder eliminar.", "util/images/warning.gif", "ELIMINAR IMPUESTO");
        }
    }
}


//****************************************************************
// Funcion		:: 	fn_abreEditarImpuesto
// Descripción	::	Abre Editar Registro
// Log			:: 	AEP - 08/11/2012
//****************************************************************
function fn_abreEditarImpuesto() {
    var id = $("#hddRowIdImpuesto").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "EDITAR IMPUESTO");
    } else {
        var vElementosAEditar = $("#jqGrid_lista_C").getGridParam('selarrrow');
        if (vElementosAEditar.length > 1) {
            parent.fn_mdl_mensajeIco("Seleccione un sólo registro.", "util/images/warning.gif", "EDITAR IMPUESTO");
        } else {
            if (vElementosAEditar != "") {
                var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', vElementosAEditar);
                var CodImpuesto = rowData.SecImpuesto;
                var Placa = rowData.Placa;
                var Pago = rowData.EstadoPago;
                //var CodBien = rowData.SecFinanciamiento;
                //var CodUnico = rowData.CodUnico;
                if (Pago == "Pagado") {

                    parent.fn_mdl_mensajeIco("No se puede editar el impuesto porque ya se encuentra pagado", "util/images/warning.gif", "EDITAR IMPUESTO");

                } else {
                    parent.fn_util_AbreModal("Gestión del Bien :: Editar", "Consultas/ImpuestoVehicular/frmConsultaImpuestoVehicularRegistroAgregar.aspx?codImp=" +
	        	         CodImpuesto + "&placa=" + Placa + "&origen=2" + "&fechaT=" + $("#hddFechaTransferencia").val() + "&cheque=" + $("#hddCheque").val(), 900, 280, function() { });


                }
            } else {
                parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "EDITAR IMPUESTO");
            }
        }
    }
}


//****************************************************************
// Funcion		:: 	fn_abreNuevo
// Descripción	::	Abre Nuevo Registro
// Log			:: 	AEP - 08/11/2012
//****************************************************************
function fn_abreNuevoImpuesto() {

    var id = $("#hddRowId").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "AGREGAR IMPUESTO");
    } else {
        //var vElementosANuevo = $("#jqGrid_lista_A").getGridParam('selarrrow');
        //		if (vElementosANuevo.length > 1) {
        //            parent.fn_mdl_mensajeIco("Seleccione un sólo registro.", "util/images/warning.gif", "AGREGAR IMPUESTO");
        //        } else {
        //           if(vElementosANuevo != "")
        //			{
        var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
        var CodigoSolicitudCredito = rowData.CodSolicitudCredito;
        var Placa = rowData.Placa;
        var Descripcion = rowData.Descripcion;
        var CodBien = rowData.SecFinanciamiento;
        var CodUnico = rowData.CodUnico;
        parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "Consultas/ImpuestoVehicular/frmConsultaImpuestoVehicularRegistroAgregar.aspx?csc=" + CodigoSolicitudCredito + "&placa=" + Placa + "&origen=1" + "&desc=" + Descripcion + "&codBien=" + CodBien + "&CodUnico=" + CodUnico, 900, 280, function() { });
        //			}else{
        //			parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "AGREGAR IMPUESTO");
        //            }
    }
    //	}
}

//****************************************************************
// Funcion		:: 	fn_Volver
// Descripción	::	Regresa a la pagina anterior
// Log			:: 	AEP - 24/09/2012
//****************************************************************
function fn_Volver() {

    fn_mdl_confirma('¿Está seguro de volver?',
		function() {
		    parent.fn_blockUI();
		    fn_util_redirect('frmConsultaImpuestoVehicular.aspx');
		},
		"../../util/images/question.gif",
		function() {
		},
		'Gestión del Bien : Impuesto Vehicular'
	);


}


/*****************************************************************
Funcion		:: 	fn_AsignarLote
Descripción	::	Genera Nro de Lote con tos los impuestos registrados
Log			:: 	AEP - 16/11/2012
***************************************************************** */

function fn_AsignarLote() {
    var strCodigoBien = '';
    var strPeriodos = '';
    var vElementosAEditar = idsOfSelectedRows;
    var count = vElementosAEditar.length;
    if (IsNullOrEmpty(count)) {
        parent.fn_mdl_mensajeIco("Seleccione al menos un registro para poder generar el lote.", "util/images/warning.gif", "GENERAR LOTE");
    } else {

        var strCodigo = '';
        var strCodigoBien = '';
        var strCodigoPeriodo = '';
        if (vElementosAEditar != "") {
            for (var j = 0; j < vElementosAEditar.length; j++) {
                strCodigo = strCodigo + vElementosAEditar[j] + ',';
            }
            strCodigoBien = $("#hddSecFinanciamiento").val();
            strCodigoPeriodo = $("#hddPeriodos").val();

            strCodigo = strCodigo.substring(0, strCodigo.length - 1);
            strCodigoBien = strCodigoBien.substring(0, strCodigoBien.length - 1);
            strCodigoPeriodo = strCodigoPeriodo.substring(0, strCodigoPeriodo.length - 1);

            //VALIDACION POR FALTA DE CUOTAS


            var arrParametro = ["strCodigoImpuesto", strCodigo, "strCodigoBien", strCodigoBien, "strPeriodos", strCodigoPeriodo];

            fn_util_AjaxSyncWM("frmImpuestoVehicularRegistro.aspx/ValidarCuotas",
        		arrParametro,
        		function(result2) {
        		    $("#hddNroCuotas").val(result2);
        		},
        		function(result) {
        		    parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL VALIDAR");
        		});

            if ($("#hddNroCuotas").val() != "") {

                parent.fn_mdl_mensajeIco($("#hddNroCuotas").val(), "util/images/warning.gif", "GENERAR LOTE");

            } else {

                fn_mdl_confirma('¿Está seguro de generar lote?',
    			function() {

    			    parent.fn_blockUI();


    			    var arrParametros = ["strCodigoImpuesto", strCodigo, "strNroLote", ''];

    			    fn_util_AjaxWM("frmImpuestoVehicularRegistro.aspx/GenerarLote",
    					arrParametros,
    					function(resultado) {
    					    parent.fn_unBlockUI();
    					    parent.fn_mdl_mensajeOk("Se generó el Lote Nº" + fn_util_trim(resultado), function() { fn_Redireccion(); }, "GRABADO CORRECTO");
    					},
    					function(resultado) {
    					    var error = eval("(" + resultado.responseText + ")");
    					    parent.fn_mdl_mensajeIco(error.Message, "../../util/images/error.gif", "ERROR GENERAR LOTE");
    					});
    			},
    			"../../util/images/question.gif",
    			function() {
    			},
    			'IMPUESTO VEHICULAR'
    		);
            }

        } else {
            parent.fn_mdl_mensajeIco("Seleccione un registro para poder generar el lote.", "util/images/warning.gif", "GENERAR LOTE IMPUESTO VEHICULAR");
        }
    }
}

function fn_Redireccion() {
    window.location = "frmImpuestoVehicularListado.aspx";
}



//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos(pstrCodContrato, pstrCodBien, pstrCodRelacionado, pstrCodTipo) {
   // debugger; 
   // if ($("#hddFechaTransferencia").val() != '') {
        parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + "&hddVer=1", 800, 350, function() { });
    //} else {
      //  parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo, 800, 350, function() { });
    //}

}



//****************************************************************
// Funcion		:: 	fn_verImpuesto
// Descripción	::	Ver Impuesto
// Log			:: 	JRC - 03/12/2012
//****************************************************************
function fn_verImpuesto() {
  
    var id = $("#hddRowIdImpuesto").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeError("Debe seleccionar un impuesto.", function() { }, "VALIDACIÓN");
    } else {
        var vElementosAEditar = $("#jqGrid_lista_C").getGridParam('selarrrow');
        if (vElementosAEditar.length > 1) {
            parent.fn_mdl_mensajeIco("Seleccione un sólo registro.", "util/images/warning.gif", "VER IMPUESTO");
        } else {
//            if (vElementosAEditar != "") {
                var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id);
                var CodImpuesto = rowData.SecImpuesto;
                var Placa = rowData.Placa;
                //var CodBien = rowData.SecFinanciamiento;
                //var CodUnico = rowData.CodUnico;
                parent.fn_util_AbreModal("Gestión del Bien :: Editar", "Consultas/ImpuestoVehicular/frmConsultaImpuestoVehicularRegistroAgregar.aspx?codImp=" +
	        	                    CodImpuesto + "&placa=" + Placa + "&origen=3", 900, 280, function() { });

//            } else {
//                parent.fn_mdl_mensajeIco("Seleccione un registro para poder visualizar.", "util/images/warning.gif", "VER IMPUESTO");
//            }
        }
    }

}
