var jovenIguish = "";
var lastSel;
var reloadGrid;

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {

    //$("#hidCargarComboClasificacion").val("FE:JOVEN;IN:IGUISH;TN:TRAGA;AR:LECHE");
    jovenIguish = $("#hidCargarComboClasificacion").val();

    fn_cargaGrilla();

    //On load Page (siempre al final)
    fn_onLoadPage();

});

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 26/09/2012
//****************************************************************
function fn_cargaGrilla() {

    var mydata =
		[
			{ CodSolicitudCredito: "10000001", CodUnico: " 0000000987", RazonSocial: "RIPLEY", ClasificacionBien: "Unidades de transporte terrestre", TipoBien: "Automóvil", EstadoContrato: "Formalizado", CantidadProducto: "5", TipoDocumento: "RUC", NumeroDocumento: "20451254121", codOpcionCompra: "10/20/2012", SecFinanciamiento: "1",CodClasificacionBien:"006" },
			{ CodSolicitudCredito: "10000002", CodUnico: " 0000000986", RazonSocial: "RIPLEY", ClasificacionBien: "Unidades de Transporte terrestre", TipoBien: "Camión", EstadoContrato: "Formalizado", CantidadProducto: "10", TipoDocumento: "RUC", NumeroDocumento: "20451254121", codOpcionCompra: "10/20/2012", SecFinanciamiento: "1", CodClasificacionBien: "006" },
			{ CodSolicitudCredito: "10000003", CodUnico: " 0000000985", RazonSocial: "RIPLEY", ClasificacionBien: "Maquinaria y Equipos Industrial", TipoBien: "Maquinaria Textil", EstadoContrato: "Formalizado", CantidadProducto: "1", TipoDocumento: "RUC", NumeroDocumento: "20451254121", codOpcionCompra: "10/20/2012", SecFinanciamiento: "2", CodClasificacionBien: "003" },
			{ CodSolicitudCredito: "10000004", CodUnico: " 0000000984", RazonSocial: "RIPLEY", ClasificacionBien: "Sistema de procesamiento electrónico de datos", TipoBien: "Computadoras", EstadoContrato: "Formalizado", CantidadProducto: "3", TipoDocumento: "RUC", NumeroDocumento: "20451254121", codOpcionCompra: "10/20/2012", SecFinanciamiento: "3", CodClasificacionBien: "007" },
			{ CodSolicitudCredito: "10000005", CodUnico: " 0000000983", RazonSocial: "RIPLEY", ClasificacionBien: "Bienes Inmuebles", TipoBien: "Embarcaciones", EstadoContrato: "Formalizado", CantidadProducto: "15", TipoDocumento: "RUC", NumeroDocumento: "20451254121", codOpcionCompra: "10/20/2012", SecFinanciamiento: "4", CodClasificacionBien: "002" },
			{ CodSolicitudCredito: "10000006", CodUnico: " 0000000981", RazonSocial: "RIPLEY", ClasificacionBien: "Bienes Inmuebles", TipoBien: "Embarcaciones", EstadoContrato: "Formalizado", CantidadProducto: "8", TipoDocumento: "RUC", NumeroDocumento: "20451254121", codOpcionCompra: "10/20/2012", SecFinanciamiento: "4", CodClasificacionBien: "002" }
		];

    $("#jqGrid_lista_A").jqGrid({
        //        datatype: function() {
        //        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");	 
        //            fn_buscarContrato();
        //        },
        datatype: "local",
        //        jsonReader: {
        //            root: "Items",
        //            page: "CurrentPage",
        //            total: "PageCount",
        //            records: "RecordCount",
        //            repeatitems: false,
        //            id: "Id"
        //        },

        colNames: ['', 'Nro. Contrato', 'CU Cliente', 'Monto', 'Tipo de Documento', 'Nro. Documento', 'Clasificación del Bien', 'Tipo de Bien', 'Estado del Contrato', 'Opción de Compra', '', '', '', '',''],
        colModel: [
        { name: 'myac', width: 80, fixed: true, sortable: false, resize: false, formatter: 'actions',
            formatoptions: {
                keys: true,
                // delbutton:false,// we want use [Enter] key to save the row and [Esc] to cancel editing.
                onEdit: function(rowid) {
                    fn_util_SeteaCalendario(jQuery("#" + rowid + "_codOpcionCompra", "#jqGrid_lista_A")); //.datepicker({dateFormat:"dd/mm/yyyy"});
                    //                    jQuery("#" + rowid + "_codOpcionCompra", "#jqGrid_lista_A").blur(function() {
                    ////                    var ids = jQuery("#jqGrid_lista_A").jqGrid('getDataIDs');
                    ////                    for (var i = 0; i < ids.length; i++) {
                    ////                        
                    ////                            if (parseFloat(ids[i]) > parseFloat(rowid)) {
                    ////                                alert(ids[i]);
                    ////                            }
                    ////                        }
                    //                    });
                    jQuery("#" + rowid + "_Monto", "#jqGrid_lista_A").validNumber({ value: '', decimals: 2, length: 15 });
                    jQuery("#" + rowid + "_Monto", "#jqGrid_lista_A").blur(function() {
                        alert(jQuery("#" + rowid + "_Monto", "#jqGrid_lista_A").val());
                    });
                    jQuery("#" + rowid + "_Monto", "#jqGrid_lista_A").keydown(function(event) {
                        if (event.which || event.keyCode) {
                            if ((event.which == 13) || (event.keyCode == 13)) {
                                alert('keydown');
                                return false;
                            }
                        }
                        else {
                            return true
                        }
                    });
                    var rd = $("#jqGrid_lista_A").jqGrid('getRowData', rowid);
                    jQuery("#" + rowid + "_ClasificacionBien", "#jqGrid_lista_A").val(rd.CodClasificacionBien);
                },
                afterRestore: function(rowid) {
                    // cuando se cancela
                    alert("in afterRestore (Cancel): rowid=" + rowid + "\nWe don't need return anything");
                },
                delOptions: {
                    onclickSubmit: function(rowid) {
                        alert("The row with rowid=" + rowid + " will be deleted");
                    }
                }
                         ,

                afterSave: function(rowid) {
                    var rd = $("#jqGrid_lista_A").jqGrid('getRowData', rowid);

                    alert(rd.CodSolicitudCredito);
                    //alert(jQuery("#" + rowid + "_Monto", "#jqGrid_lista_A").val());
                    alert('despues de grabar');
                    //                        var arrParametros = ["pstrNumeroContrato", jQuery("#"+rowid+"_CodSolicitudCredito","#jqGrid_lista_A").val(),
                    //                                    "pstrFechaTerminoRecepDocProv",jQuery("#"+rowid+"_NombreCliente","#jqGrid_lista_A").val() ,
                    //                                    "pstrFlagFechaTerminoRecepDocProv", jQuery("#"+rowid+"_TipoDocumento","#jqGrid_lista_A").val(),
                    //                                    "pstrDescripcionBien", jQuery("#"+rowid+"_NumeroDocumento","#jqGrid_lista_A").val()
                    //                                    ];

                    //                     fn_util_AjaxWM("frmTemporalEditarGrilla.aspx/GuardarRegistro",
                    //                     arrParametros,
                    //                     function(resultado) {
                    //                         var error = eval("(" + resultado.responseText + ")");
                    //                         parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABAR");
                    //                     });

                }
            }

        },
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 50, sorttype: "string", align: "center" },
            { name: 'CodUnico', index: 'CodUnico', width: 50, sorttype: "string", align: "center", editable: true },
		    { name: 'Monto', index: 'Monto', width: 50, sorttype: "decimal", align: "left", editable: true },
        	{ name: 'TipoDocumento', index: 'FechaTransferencia', width: 80, align: "center", sorttype: "string", editable: true },
		    { name: 'NumeroDocumento', index: 'CantidadProducto', width: 50, align: "right", sorttype: "string", editable: true },
		   { name: 'ClasificacionBien', index: 'ClasificacionBien', width: 155, align: "center", sorttype: "string", editable: true, edittype: "select", editoptions: { value: jovenIguish } },
        //{ name: 'ClasificacionBien', index: 'ClasificacionBien', width: 150, align: "center", sorttype: "string",editable:true,edittype:"select",editoptions:{value:"FE:FedEx;IN:InTime;TN:TNT;AR:ARAMEX"}},
		    {name: 'TipoBien', index: 'TipoBien', width: 60, align: "center", editable: true },
		    { name: 'EstadoContrato', index: 'EstadoContrato', width: 50, align: "center", sorttype: "string", editable: true },
        //{ name: 'cantidad', index: 'cantidad', width: 50, align: "right", sorttype: "string" },
		    {name: 'codOpcionCompra', index: 'codOpcionCompra', width: 50, align: "center", sorttype: "date", editable: true },
        	{ name: '', index: '', width: 2 },
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
            { name: 'CodigoEstadoContrato', index: 'CodigoEstadoContrato', hidden: true },
            { name: 'CodClasificacionBien', index: 'CodClasificacionBien', hidden: true },
        	{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true }

	    ],

        height: '100%',
        editurl: 'clientArray',
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
        deleteurl: 'hotmail.com',
        subgrid: true

    });


    for (var i = 0; i <= mydata.length; i++) {
        jQuery("#jqGrid_lista_A").jqGrid('addRowData', i + 1, mydata[i]);
    }

    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();



    //agregar();   
}


//var DataBlancoJoven = [{ CodSolicitudCredito: "", CodUnico: " ", RazonSocial: "", ClasificacionBien: "", TipoBien: "", EstadoContrato: "", CantidadProducto: "", TipoDocumento: "", NumeroDocumento: "", codOpcionCompra: "", SecFinanciamiento: "4" }];

//function agregar() {

//	jQuery("#jqGrid_lista_A").jqGrid('addRowData',0,DataBlancoJoven[0],lastSel);
//	var cantidad = jQuery("#jqGrid_lista_A").jqGrid('getRowData').length;
//	jQuery("#jqGrid_lista_A").jqGrid('editRow',cantidad,true);
//	//jQuery("#jqGrid_lista_A").jqGrid('addRowData', 0, DataBlancoJoven[0]);
//}

