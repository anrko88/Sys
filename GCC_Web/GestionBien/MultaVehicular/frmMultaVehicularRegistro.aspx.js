//****************************************************************
// Variables Globales
//****************************************************************
var blnPrimeraBusqueda;
var intPaginaActual = 1;

var strDepartamento = "15";
var strProvincia = "01";
//var srtSecImpuesto = "";
//Inicio IBK
var strMunicipalidad = '';

var MuniLote = '';
var TotalAutovaluoLote = '';
var TotalPredialLote = '';
var PeriodoLote = '';
var strCodigosImpuestos = '';
//Fin IBK
var C_GESTIONBIEN_MULTAVEHICULAR = "004";
var strRegistros = 0;
var strLote = '';
var Perfil = '';
//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	AEP - 08/11/2012
//****************************************************************
$(document).ready(function() {

    //Valida Campos
    strLote = $("#txtNroLoteCarga").val();
    //strMunicipalidad = $("#hddCodMunicipalidad").val();
    // fn_SeteaMunicipalidad();
    //$("#ddlMunicipalidad").val(strMunicipalidad);
    fn_inicializaCampos();
    //Inicio IBK - AAE
    $('#txtMontoCheque').val(Fn_util_ReturnValidDecimal2($('#txtMontoCheque').val()));
    $('#txtTotalLote').val(Fn_util_ReturnValidDecimal2($('#txtTotalLote').val()));
    $('#txtDevuelto').val(Fn_util_ReturnValidDecimal2($('#txtDevuelto').val()));
    $('#txtReembolsar').val(Fn_util_ReturnValidDecimal2($('#txtReembolsar').val()));
    //Fin IBK - AAE
    Perfil = $("#hddPerfil").val();
    //Inicio IBK JJM

    //    if ($("#txtCodMunicipalidad").val() != "") {
    //        var strValor = $(this).val();
    //        fn_ListarMultasLote('');
    //    };

    //Fin IBK JJM

    $(document).keypress(function(e) {
        if (e.which == 13) {
            fn_buscar(true);
        }
    });




    //Carga Grilla
    if (strLote == '') {
        if ($("#hddFecTransferencia").val() != '') {
            blnPrimeraBusqueda = true;
            fn_cargaGrilla();
            $("#txtPlaca").attr('disabled', 'disabled');
            $("#imgBsqMunicipalidad").attr('disabled', 'disabled');
            $("#txtCodMunicipalidad").val(strMunicipalidad);

            $("#dv_img_lote").css('display', 'none');
            $("#dv_separador").css('display', 'none');
            $("#dv_img_buscar").css('display', 'none');
            $("#dv_img_limpiar").css('display', 'none');
            $("#dv_editar").css('display', 'none');
            $("#dv_img_agregar").css('display', 'none');
            $("#dv_eliminar_Multa").css('display', 'none');

            parent.fn_util_AbreModal("Gestión del Bien :: Editar", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?codImp=" + $("#hddCodMulta").val() + "&placa=" + $("#txtPlaca").val() + "&origen=2" + "&codMuni=" + $("#txtCodMunicipalidad").val() + "&fechaT=" + $("#hddFecTransferencia").val(), 900, 450, function() {
            });

        } else {
            if ($("#hddTipo").val() == '1') {
                blnPrimeraBusqueda = true;
                fn_cargaGrilla();
                $("#txtPlaca").attr('disabled', 'disabled');
                $("#imgBsqMunicipalidad").attr('disabled', 'disabled');
                $("#txtCodMunicipalidad").val(strMunicipalidad);
                $("#dv_img_lote").css('display', 'none');
                $("#dv_separador").css('display', 'none');
                $("#dv_img_buscar").css('display', 'none');
                $("#dv_img_agregar").css('display', 'none');
                $("#dv_img_limpiar").css('display', 'none');
                $("#dv_agregarMulta").css('display', 'none');
                //$("#dv_eliminar_Multa").css('display', 'none');
                //if (fn_util_trim($("#hddEstadoPago").val()) != '002') {
                parent.fn_util_AbreModal("Gestión del Bien :: Editar", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?codImp=" +
		  			                        $("#hddCodMulta").val() + "&placa=" + $("#txtPlaca").val() + "&origen=2" + "&codMuni=" + $("#txtCodMunicipalidad").val() +
			  			                    "&fechaT=" + $("#hddFecTransferencia").val() + "&EstPago=" + $("#hddEstadoPago").val(), 900, 450, function() {
			  			                    });
                // }

            } else {
                fn_cargaGrilla();
                $("#dv_ver").css('display', 'none');
            }
        }
    }
    else {
        fn_EditarLote();
    }
    $("#txtCodMunicipalidad").focusout(function() {
        var strValor = $(this).val();
        $("#txtMunicipalidad").val("");
    });

    $("#txtMunicipalidad").focusout(function() {
        var strValor = $(this).val();
        $('#txtCodMunicipalidad').val("");
    });
    $('#imgBsqMunicipalidad').click(function() {
        if ($('#txtCodMunicipalidad').val() == '' && $('#txtMunicipalidad').val() == '') {
            parent.fn_unBlockUI();
            parent.fn_util_AbreModal("Municipalidad", "Comun/frmMunicipalidadesConsulta.aspx?Codigo= " + $('#txtCodMunicipalidad').val() + '&Descripcion= ' + $('#txtMunicipalidadDesc').val(), 800, 600, function() { });
        }
        else {
            var paramArray = ["pCodMunicipalidad", $('#txtCodMunicipalidad').val(),
                          "pMunicipalidad", $('#txtMunicipalidad').val()
                         ];
            fn_util_AjaxWM("frmMultaVehicularRegistro.aspx/ConsultaMunicipalidad",
                       paramArray,
                        /*function(jsondata) {
                        $('#txtCodMunicipalidad').val('');
                        $('#txtMunicipalidad').val('');
                        $('#txtCodMunicipalidad').val(fn_util_trim(jsondata.Items[0].CLAVE1));
                        $('#txtMunicipalidad').val(fn_util_trim(jsondata.Items[0].VALOR1));
                        }*/
                       function(resultado) {                           
                           var arrResultado = resultado.split("|")
                           if (arrResultado[0] == "0") {
                               parent.fn_unBlockUI();   
                               $('#txtCodMunicipalidad').val(fn_util_trim(arrResultado[1]));
                               $('#txtMunicipalidad').val(fn_util_trim(arrResultado[2]));
                           } else {
                                parent.fn_unBlockUI();
                                parent.fn_util_AbreModal("Municipalidad", "Comun/frmMunicipalidadesConsulta.aspx?Codigo= " + $('#txtCodMunicipalidad').val() + '&Descripcion= ' + $('#txtMunicipalidadDesc').val(), 800, 600, function() { });
                           }
                       },
                       function(resultado) {
                           parent.fn_unBlockUI();
                           parent.fn_mdl_mensajeIco("Se produjo un error al obtener los datos del contrato", "util/images/error.gif", "ERROR EN CONSULTA");
                       }
    );

            //Resize Pantalla    
            fn_doResize();
        }
    });
    fn_cargaGrillaMultas();



    //On load Page (siempre al final)
    fn_onLoadPage();


});
function fn_EditarLote() {
    //strLote = $("#hddNroLote").val();
    $("#dv_ver").css('display', 'none');
    $("#txtNroLoteCarga").attr('disabled', 'disabled');
    if (strLote == '' || strLote == '0') {
        if ($("#hddFecTransferencia").val() != '') {
            blnPrimeraBusqueda = true;
            fn_cargaGrilla();
            $("#txtPlaca").attr('disabled', 'disabled');
            $("#imgBsqMunicipalidad").attr('disabled', 'disabled');
            $("#txtCodMunicipalidad").val(strMunicipalidad);

            $("#dv_img_lote").css('display', 'none');
            $("#dv_separador").css('display', 'none');
            $("#dv_img_buscar").css('display', 'none');
            $("#dv_img_limpiar").css('display', 'none');
            $("#dv_editar").css('display', 'none');
            $("#dv_img_agregar").css('display', 'none');
            $("#dv_eliminar_Multa").css('display', 'none');

            parent.fn_util_AbreModal("Gestión del Bien :: Editar", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?codImp=" + $("#hddCodMulta").val() + "&placa=" + $("#txtPlaca").val() + "&origen=2" + "&codMuni=" + $("#txtCodMunicipalidad").val() + "&fechaT=" + $("#hddFecTransferencia").val(), 900, 450, function() {
            });

        } else {
            if ($("#hddTipo").val() == '1') {
                blnPrimeraBusqueda = true;
                fn_cargaGrilla();
                $("#txtPlaca").attr('disabled', 'disabled');
                $("#imgBsqMunicipalidad").attr('disabled', 'disabled');
                $("#txtCodMunicipalidad").val(strMunicipalidad);
                $("#dv_img_lote").css('display', 'none');
                $("#dv_separador").css('display', 'none');
                $("#dv_img_buscar").css('display', 'none');
                $("#dv_img_agregar").css('display', 'none');
                $("#dv_img_limpiar").css('display', 'none');
                $("#dv_agregarMulta").css('display', 'none');
                //$("#dv_eliminar_Multa").css('display', 'none');
                //if (fn_util_trim($("#hddEstadoPago").val()) != '002') {
                parent.fn_util_AbreModal("Gestión del Bien :: Editar", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?codImp=" +
		  			                        $("#hddCodMulta").val() + "&placa=" + $("#txtPlaca").val() + "&origen=2" + "&codMuni=" + $("#txtCodMunicipalidad").val() +
			  			                    "&fechaT=" + $("#hddFecTransferencia").val() + "&EstPago=" + $("#hddEstadoPago").val(), 900, 450, function() {
			  			                    });
                // }

            } else {
                fn_cargaGrilla();
                $("#dv_ver").css('display', 'none');
            }
        }
    }
    else {
        if ($("#hddFecTransferencia").val() != '') {
            blnPrimeraBusqueda = true;
            fn_cargaGrilla();
            //Inicio IBK - AAE - Cargo grilla impuestos
            //fn_ListarMultasLote('');
            //Fin IBK
            //            $("#txtPlaca").attr('disabled', 'disabled');
            $("#imgBsqMunicipalidad").attr('disabled', 'disabled');
            //Inicio IBK - Bloque Municipalidad
            $("#txtCodMunicipalidad").attr('disabled', 'disabled');
            $("#txtMunicipalidad").attr('disabled', 'disabled');
            //Fin IBK
            //$("#txtCodMunicipalidad").val(strMunicipalidad);

            //            $("#dv_img_lote").css('display', 'none');
            //            $("#dv_separador").css('display', 'none');
            //            $("#dv_img_buscar").css('display', 'none');
            //            $("#dv_img_limpiar").css('display', 'none');
            //            $("#dv_editar").css('display', 'none');
            //            $("#dv_img_agregar").css('display', 'none');
            //            $("#dv_eliminar_Multa").css('display', 'none');

            //            parent.fn_util_AbreModal("Gestión del Bien :: Editar", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?codImp=" + $("#hddCodMulta").val() + "&placa=" + $("#txtPlaca").val() + "&origen=2" + "&codMuni=" + $("#ddlMunicipalidad").val() + "&fechaT=" + $("#hddFecTransferencia").val(), 900, 450, function() {
            //            });

        } else {
            if ($("#hddTipo").val() == '1') {
                blnPrimeraBusqueda = true;
                fn_cargaGrilla();
                //Inicio IBK - AAE - Cargo grilla impuestos
                //fn_ListarMultasLote('');
                //Fin IBK
                //                                $("#txtPlaca").attr('disabled', 'disabled');
                $("#imgBsqMunicipalidad").attr('disabled', 'disabled');
                //Inicio IBK - Bloque Municipalidad
                $("#txtCodMunicipalidad").attr('disabled', 'disabled');
                $("#txtMunicipalidad").attr('disabled', 'disabled');
                //Fin IBK
                //$("#txtCodMunicipalidad").val(strMunicipalidad);
                //                $("#dv_img_lote").css('display', 'none');
                //                $("#dv_separador").css('display', 'none');
                //                $("#dv_img_buscar").css('display', 'none');
                //                $("#dv_img_agregar").css('display', 'none');
                //                $("#dv_img_limpiar").css('display', 'none');
                //                $("#dv_agregarMulta").css('display', 'none');
                //$("#dv_eliminar_Multa").css('display', 'none');
                //if (fn_util_trim($("#hddEstadoPago").val()) != '002') {
                //                parent.fn_util_AbreModal("Gestión del Bien :: Editar", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?codImp=" +
                //		  			                        $("#hddCodMulta").val() + "&placa=" + $("#txtPlaca").val() + "&origen=2" + "&codMuni=" + $("#ddlMunicipalidad").val() +
                //			  			                    "&fechaT=" + $("#hddFecTransferencia").val() + "&EstPago=" + $("#hddEstadoPago").val(), 900, 450, function() {
                //			  			                    });
                // }

            } else {
                fn_cargaGrilla();
                //Inicio IBK - AAE - Cargo grilla impuestos
                //fn_ListarMultasLote('');
                //Fin IBK
                $("#dv_ver").css('display', 'none');
            }
        }
    }
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_inicializaCampos() { 
    $('#txtPlaca').validText({ type: 'alphanumeric', length: 10 });
    if ($("#hddTipo").val() == "1") {
        //$("#ddlMunicipalidad").val(fn_util_trim($("#hddCodMunicipalidad").val()));
    }
    //Inicio IBK - AAE
    $("#txtMontoCheque").attr('disabled', 'disabled');
    $("#txtTotalLote").attr('disabled', 'disabled');
    $("#txtDevuelto").attr('disabled', 'disabled');
    $("#txtReembolsar").attr('disabled', 'disabled');
    $("#txtEstadoLote").attr('disabled', 'disabled');
    //Fin IBK - AAE

}

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	AEP - 08/11/2012
//****************************************************************
function fn_cargaGrilla() {

    //var mydata = 
    //          [
    //		    { CodSolicitudCredito: "10000001", CodUnico: " 0000000987", RazonSocial: "RIPLEY", Municipalidad: "Lima",Placa: "xt15444",NroMotor: "11144dd", Marca: "Toyota",Modelo: "Yaris",Clase: "Automovil", AnioFabricacion: "2002",SecFinanciamiento: "1"},
    //            { CodSolicitudCredito: "10000002", CodUnico: " 0000000981", RazonSocial: "RIPLEY", Municipalidad: "Lima",Placa: "xt15gg4",NroMotor: "11147dd", Marca: "Toyota",Modelo: "Yaris",Clase: "Automovil", AnioFabricacion: "2002",SecFinanciamiento: "1"},
    //          	{ CodSolicitudCredito: "10000003", CodUnico: " 0000000982", RazonSocial: "RIPLEY", Municipalidad: "Lima",Placa: "xt15664",NroMotor: "11149dd", Marca: "Toyota",Modelo: "Corolla",Clase: "Automovil", AnioFabricacion: "2002",SecFinanciamiento: "1"},
    //          	{ CodSolicitudCredito: "10000004", CodUnico: " 0000000983", RazonSocial: "RIPLEY", Municipalidad: "Lima",Placa: "xt15774",NroMotor: "1114jdd", Marca: "Toyota",Modelo: "Corolla",Clase: "Automovil", AnioFabricacion: "2002",SecFinanciamiento: "1"},
    //          	{ CodSolicitudCredito: "10000005", CodUnico: " 0000000984", RazonSocial: "RIPLEY", Municipalidad: "Lima",Placa: "xt15yy4",NroMotor: "1114sdd", Marca: "Toyota",Modelo: "Corolla",Clase: "Automovil", AnioFabricacion: "2002",SecFinanciamiento: "1"}
    //		  ];

    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_ListarBienes();
        },
        //   	    datatype: "local",
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['Nro. Contrato', 'CU Cliente', 'Razón Social o Nombre', 'Placa Actual', 'Municipalidad', 'Marca', 'Modelo', 'Nº Motor', 'Estado Bien', 'Estado Contrato', '', '', '', '', ''],
        colModel: [
            { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', width: 50, sorttype: "string", align: "center" },
            { name: 'CodUnico', index: 'CodUnico', width: 50, sorttype: "string", align: "left" },
            { name: 'RazonSocial', index: 'RazonSocial', width: 150, sorttype: "string", align: "left" },
        	{ name: 'Placa', index: 'Placa', width: 50, align: "center", sorttype: "string" },
		    { name: 'Municipalidad', index: 'Municipalidad', hidden: true },
		    { name: 'Marca', index: 'Marca', width: 50, align: "center", sorttype: "string" },
        	{ name: 'Modelo', index: 'Modelo', width: 50, align: "center", sorttype: "string" },
        	{ name: 'NroMotor', index: 'NroMotor', width: 50, align: "center", sorttype: "string", editable: true },
        	{ name: 'DesEstadoBien', index: 'DesEstadoBien', width: 50, align: "center", sorttype: "string" },
        	{ name: 'DesEstadoContrato', index: 'DesEstadoContrato', width: 50, align: "center", sorttype: "string" },
        	{ name: '', index: '', width: 1 },
		    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
            { name: 'CodigoEstadoContrato', index: 'CodigoEstadoContrato', hidden: true },
        	{ name: 'CodMunicipalidad', index: 'CodMunicipalidad', hidden: true },
        	{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true }
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
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddRowId").val(id);
        },
        ondblClickRow: function() {
            //parent.fn_blockUI();
            //fn_util_redirect('frmMantenimientoBienContrato.aspx?co=1&csc=' + $("#hidCodigoSolicitudCredito").val());
        }
    });
    //	
    //	for (var i = 0; i <= mydata.length; i++) {
    //	     jQuery("#jqGrid_lista_A").jqGrid('addRowData', i + 1, mydata[i]);
    //	 }
    //	
    jQuery("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();

}

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_buscar(bSearch) {
    blnPrimeraBusqueda = bSearch;
    fn_ListarBienes();
}

//****************************************************************
// Funcion		:: 	fn_ListarBienes
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	AEP - 26/11/2012
//****************************************************************

function fn_ListarBienes() {
    if (!blnPrimeraBusqueda) {
        return;
    } else {
        // parent.fn_blockUI();

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"),
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"),
    	                 "pCodMunicipalidad", "0",
    	                 "pPlaca", $("#txtPlaca").val(),
    	                 "pTipo", $("#hddTipo").val(),
    	                 "pNroMotor", $("#txtMotor").val(),
    	                 "pCUCliente", $("#txtCU").val(),
    	                 "pCodContrato", $("#txtContrato").val()
                         ];

        fn_util_AjaxWM("frmMultaVehicularRegistro.aspx/ListaBienesVehicular",
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
function fn_cargaGrillaMultas() {
    srtSecImpuesto = '';
    //    var mydata2 = 
    //          [
    //		    { CodSolicitudCredito: "10000001",Placa: "xt15444",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "170.00",NroCuota: "1",FechaPago: "16/11/2012",FechaCobro: "20/11/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"},
    //		    { CodSolicitudCredito: "10000002",Placa: "xt15445",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "80.00",NroCuota: "2",FechaPago: "16/12/2012",FechaCobro: "20/12/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"},
    //          	{ CodSolicitudCredito: "10000003",Placa: "xt15488",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "170.00",NroCuota: "1",FechaPago: "16/11/2012",FechaCobro: "20/11/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"},
    //		    { CodSolicitudCredito: "10000004",Placa: "xt15400",FechaDeclaracion: "15/10/2002", Periodo: "2012",Importe: "80.00",NroCuota: "2",FechaPago: "16/12/2012",FechaCobro: "20/12/2012", EstadoCobro: "Pendiente",Observacion:"Observacion",SecFinanciamiento: "1"}
    //		  ];

    var updateIdsOfSelectedRows = function(id, isSelected) {
        $("#hddRowIdMulta").val(id);
        var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);

        if (isSelected) {
            //Agrega Municipalidades - JJM IBK
            strMunicipalidad = strMunicipalidad + rowData.Municipalidad + ",";
            srtSecImpuesto = srtSecImpuesto + id + ",";
        } else {


            var lblChekedResult3 = srtSecImpuesto.split(",");
            var pstrSecImpuesto = "";
            for (var i = 0; i < lblChekedResult3.length; i++) {
                if (rowData.SecMulta != lblChekedResult3[i]) {
                    if (lblChekedResult3[i] != "") {
                        pstrSecImpuesto += lblChekedResult3[i] + ",";
                    }
                }

            }

            srtSecImpuesto = pstrSecImpuesto;


        }
        $("#hddRowIdMulta").val(srtSecImpuesto);
        $("#hddMunicipalidad").val(strMunicipalidad);  //IBK JJM

        var index = $.inArray(id, idsOfSelectedRows);
        if (!isSelected && index >= 0) {
            idsOfSelectedRows.splice(index, 1);
        } else if (index < 0) {
            idsOfSelectedRows.push(rowData.SecMulta);
        }
    };

    $("#jqGrid_lista_B").jqGrid({
        datatype: function() {
            fn_ListarMultasLote('');
        },
        //    	datatype: "local",
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "SecMulta"
        },
        colNames: ['Placa Actual', 'Nº Infracción', 'F. Infracción', 'Concepto', 'Código Infracción', 'F. de Registro',
        	       'F. Recepcion del Banco', 'Importe', 'Importe Descuento', 'Municipalidad', 'F. de Pago', 'Estado de Pago',
        	       'Observación', '', '', '', 'Docs.', '', '', '', ''],
        colModel: [

            { name: 'Placa', index: 'Placa', width: 50, sorttype: "string", align: "left" },
            { name: 'NroInfraccion', index: 'NroInfraccion', width: 50, sorttype: "string", align: "left" },
        	{ name: 'FecInfraccion', index: 'FecInfraccion', width: 50, sorttype: "string", align: "left" },
        	{ name: 'TipoInfraccion', index: 'TipoInfraccion', width: 50, sorttype: "string", align: "left" },
            { name: 'CodigoInfraccion', index: 'CodigoInfraccion', width: 50, sorttype: "string", align: "left" },
            { name: 'FecIngreso', index: 'FecIngreso', width: 50, align: "center", sorttype: "string" },
            { name: 'FecRecepcionBanco', index: 'FecRecepcionBanco', width: 60, align: "center" },
            { name: 'Importe', index: 'Importe', width: 50, align: "center", sorttype: "string", formatter: Fn_util_ReturnValidDecimal2 },
        	{ name: 'ImporteDescuento', index: 'ImporteDescuento', width: 50, align: "center", sorttype: "string", formatter: Fn_util_ReturnValidDecimal2 },
		    { name: 'Municipalidad', index: 'Municipalidad', width: 50, align: "center", sorttype: "string", editable: true },
        	{ name: 'FechaPago', index: 'FechaPago', width: 60, align: "center", sorttype: "string" },
		    { name: 'EstadoPago', index: 'EstadoPago', width: 50, align: "center", sorttype: "string" },
        	{ name: 'Observaciones', index: 'Observaciones', hidden: true }, //,formatter:Lupa2  },
            {name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
            { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
        	{ name: 'SecMulta', index: 'SecMulta', hidden: true },
            { name: 'Doc', index: 'Doc', align: "center", sortable: false, formatter: fn_abreDocumentos, width: 45 },
            { name: '', index: '', width: 2 },
        	{ name: 'CodEstadoPago', index: 'CodEstadoPago', hidden: true },
        	{ name: 'CodEstadoCobro', index: 'CodEstadoCobro', hidden: true },
        	{ name: 'EstadoCobro', index: 'EstadoCobro', hidden: true}],
        height: '100%',
        //pager: '#jqGrid_pager_B',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        //rowNum: 5,
        //rowList: [10, 20, 30],
        sortname: 'CodSolicitudCredito',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        multiselect: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: updateIdsOfSelectedRows,
        onSelectAll: function(aRowids, isSelected) {
            var i, count, id;
            for (i = 0, count = aRowids.length; i < count; i++) {
                id = aRowids[i];
                updateIdsOfSelectedRows(id, isSelected);
            }
        },
        //Inicio IBK - AAE
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var Resultado = '';
            var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
            var strPlaca = rowData.Placa;
            var strSecMulta = rowData.SecMulta;
            var strCodMulta = rowData.CodEstadoPago;
            var CodImpuesto = rowData.SecMulta;
            var strCodMun = $("#txtCodMunicipalidad").val();
            var Placa = rowData.Placa;
            var Pago = rowData.EstadoPago;
            //var estCobro = rowData.EstadoCobro;
            var estCobro = rowData.CodEstadoCobro;
            var cobrado = "0";
            var pagado = "0";
            var supervisor = "0";
            if (fn_util_trim(Pago) == "Pagado") {
                pagado = "1"
            }
            if ((Perfil == '6') || (Perfil == '11') || (Perfil == '1')) {//Perfil Supervisor Leasginif()
                supervisor = "1";
            }
            if ((fn_util_trim(estCobro) == "C") || (fn_util_trim(estCobro) == "I") || (fn_util_trim(estCobro) == "H")) {
                cobrado = "1"
            }
            if ((supervisor == "0") && ((cobrado == "1") || (pagado == "1"))) {
                parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?codImp=" + strSecMulta + "&placa=" + strPlaca + "&origen=2" + "&fechaT=" + $("#hddFecTransferencia").val() + "&codMuni="+strCodMun+"&strNroLote=" + strLote + "&ReadOnly=Y", 900, 550, function() { });
            } else {//if ((supervisor == "0") && ((cobrado == "1")||(pagado == "1") ) )
                if (supervisor == "1") {
                    parent.fn_unBlockUI();
                    
                    parent.fn_mdl_confirma(Resultado == '' ? "¿Está seguro que desea Editar la Multa seleccionada?" : Resultado,
       	        		    function() {
       	        		        parent.fn_blockUI();
       	        		        parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?codImp=" + strSecMulta + "&placa=" + strPlaca + "&origen=2" + "&fechaT=" + $("#hddFecTransferencia").val() + "&codMuni="+strCodMun+"&strNroLote=" + strLote + "&ReadOnly=N", 900, 550, function() { });
       	        		    },
       	        		    "Util/images/question.gif",
       	        		    function() {
       	        		        parent.fn_blockUI();
       	        		        parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?codImp=" + strSecMulta + "&placa=" + strPlaca + "&origen=2" + "&fechaT=" + $("#hddFecTransferencia").val() + "&codMuni="+strCodMun+"&strNroLote=" + strLote + "&ReadOnly=Y", 900, 550, function() { });
       	        		    },
       	        		    'ELIMINAR IMPUESTO'
       	        	    );
                } else { //cierra if (supervisor == "1") 
                    parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?codImp=" + strSecMulta + "&placa=" + strPlaca + "&origen=2" + "&fechaT=" + $("#hddFecTransferencia").val() + "&codMuni="+strCodMun+"&strNroLote=" + strLote + "&ReadOnly=N", 900, 550, function() { });
                } //cierra ELSE (supervisor == "1") 
            }
        }
        //Fin IBK
    });

    //	for (var i = 0; i <= mydata2.length; i++) {
    //	     jQuery("#jqGrid_lista_B").jqGrid('addRowData', i + 1, mydata2[i]);
    //	 }


    jQuery("#jqGrid_lista_B").jqGrid('navGrid', '#jqGrid_pager_B', { edit: false, add: false, del: false });

    $("#jqGrid_lista_B").setGridWidth($(window).width() - 70);
    $("#search_jqGrid_lista_B").hide();

    //	  function Lupa2(cellvalue, options, rowObject) {
    //           
    //       if (rowObject.Observaciones == "") {
    //           return "<img src='../../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
    //       } else {
    //          var sScript2 = "javascript:VerObservaciones(" + rowObject.CodSolicitudCredito + "," + rowObject.SecFinanciamiento + ");";
    //       	  return "<img src='../../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
    //       }
    //    	
    //    };


    //**************************************************
    // Documentos
    //**************************************************
    function fn_abreDocumentos(cellvalue, options, rowObject) {
        //return ".";
        return "<img src='../../Util/images/ico_docs.gif' alt='Ver Documentos' title='Ver Documentos' width='18px' onclick=\"javascript:fn_GBAbreDocumentos(\'" + rowObject.CodSolicitudCredito + "\',\'" + rowObject.SecFinanciamiento + "\',\'" + rowObject.SecMulta + "\',\'" + C_GESTIONBIEN_MULTAVEHICULAR + "\');\" style='cursor:pointer;' />";
    };

}

function VerObservaciones(strcodContrato, strsecfinanciamiento) {
    var sTitulo = "Gestión del Bien";
    var sSubTitulo = "Mant. Bien :: Observación de Baja  ";
    parent.fn_util_AbreModal(sSubTitulo, "GestionBien/MultaVehicular/frmMultaVehicularObservacion.aspx?ccf=" + strcodContrato + "&csf=" + strsecfinanciamiento + "&Add=true", 500, 200, function() { });
}


//****************************************************************
// Funcion		:: 	fn_ListarMultasLote
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	AEP - 29/11/2012
//****************************************************************
function fn_ListarMultasLote(strRegistroMulta) {
    parent.fn_blockUI();
    //Inicio IBK - AAE
    strLote = $("#hddNroLote").val();

    if ($("#hidTengoLote").val() != "N") {
        //Cargo info de header de lote
        var arrParametros2 = ["pNroLote", strLote];
        fn_util_AjaxWM("frmMultaVehicularRegistro.aspx/ObtenerInfoLote",
                       arrParametros2,
                       function(jsondata) {
                           $("#txtEstadoLote").val(jsondata.Items[0].DescCodEstadoLote);
                           $("#hidCodEstadoLote").val(jsondata.Items[0].CodEstadoLote);
                           $("#txtTotalLote").val(jsondata.Items[0].TotalLote);
                           $("#txtDevuelto").val(jsondata.Items[0].DevueltoLote);
                           $("#txtReembolsar").val(jsondata.Items[0].ReembolsoLote);
                           $("#txtMontoCheque").val(jsondata.Items[0].MontoCheque);
                           //Formato
                           $('#txtMontoCheque').val(Fn_util_ReturnValidDecimal2($('#txtMontoCheque').val()));
                           $('#txtTotalLote').val(Fn_util_ReturnValidDecimal2($('#txtTotalLote').val()));
                           $('#txtDevuelto').val(Fn_util_ReturnValidDecimal2($('#txtDevuelto').val()));
                           $('#txtReembolsar').val(Fn_util_ReturnValidDecimal2($('#txtReembolsar').val()));
                       },
                       function(resultado) {
                           parent.fn_unBlockUI();
                           var error = eval("(" + resultado.responseText + ")");
                           parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN LA BÚSQUEDA");
                       }
        );
    }

    //Fin IBK - AAE

    $("#hddRowIdMulta").val('');
    $("#jqGrid_lista_B").jqGrid('resetSelection');
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_B", "rowNum"),
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_B", "page"),
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_B", "sortname"),
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_B", "sortorder"),
    	                 "pPlaca", $("#txtPlaca").val(),
    	                 "pCodMunicipalidad", $("#txtCodMunicipalidad").val(),
    	                 "pTipo", $("#hddTipo").val() == '' ? '0' : $("#hddTipo").val(),
    	                 "pNroLote", strLote
                         ];

    fn_util_AjaxWM("frmMultaVehicularRegistro.aspx/ListarLoteMultaVehicular",
                   arrParametros,
                   function(jsondata) {
                       jqGrid_lista_B.addJSONData(jsondata);
                       strRegistros = strRegistros + 1;
                       for (var i = 0, count = idsOfSelectedRows.length; i < count; i++) {
                           $("#jqGrid_lista_B").jqGrid('setSelection', idsOfSelectedRows[i], false);

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
    fn_OcultaMunicipalidad();
}

//****************************************************************
// Funcion		:: 	fn_SeteaMunicipalidad
// Descripción	::	Setear la municipalidad
// Log			:: 	AEP - 26/11/2012
//****************************************************************
function fn_SeteaMunicipalidad() {

    //Carga Distrito
    //fn_cargaComboMunicipalidad(strDepartamento, strProvincia);
    //$("#txtCodMunicipalidad").val(fn_util_trim($("#hidCodDistrito").val()));
}
//********************************************************  ********
// Funcion		:: 	fn_cargaComboMunicipalidad
// Descripción	::	
// Log			:: 	AEP - 26/11/2012
//****************************************************************
function fn_cargaComboMunicipalidad(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#ddlMunicipalidad').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    //fn_doResize();
}

//****************************************************************
// Funcion		:: 	fn_buscar
// Descripción	::	Realiza Busqueda (Listado)
// Log			:: 	??? - 02/11/2012
//****************************************************************
function fn_limpiar() {
    blnPrimeraBusqueda = false;
    $("#txtPlaca").val('');
    $("#txtCodMunicipalidad").val('');
    $("#jqGrid_lista_A").GridUnload();
    fn_cargaGrilla();
}


//****************************************************************
// Funcion		:: 	fn_abreEliminarMulta
// Descripción	::	Eliminar todos los registro seleccionados
// Log			:: 	AEP - 09/11/2012
//****************************************************************
function fn_abreEliminarMulta() {
    var Resultado = '';
    var id = $("#hddRowIdMulta").val();
    //JJM IBK
    var vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow');
    var count = vElementosAEditar.length;
    var Resultado = '';
    var Estado = 0;
    var EstadoSup = 0;
    //Inicio IBK - AAE
    var pagados = 0;
    var cobrados = 0;
    //Fin IBK
    var rowData1 = $("#jqGrid_lista_B").jqGrid('getRowData');
    for (var i = 0; i < rowData1.length; i++) {
        var row = rowData1[i];
        for (var j = 0; j < vElementosAEditar.length; j++) {
            if (row.SecMulta == vElementosAEditar[j]) {
                //Inicio IBK - AAE
                /*
                if (Perfil == '6') {//Perfil Supervisor Leasginif()
                    if (fn_util_trim(row.CodEstadoPago) == "002") {//&& row.EstadoCobro == "Pagado") {
                        Estado = Estado + 1;
                        break;
                    }
                    if (fn_util_trim(row.CodEstadoPago) == "002") {//&& row.EstadoCobro == "Pendiente") {
                        EstadoSup = EstadoSup + 1;
                        break;
                    }
                }
                else {//Otros Perfiles
                    if (fn_util_trim(row.CodEstadoPago) == "002") {//|| row.EstadoCobro == "Pendiente") {
                        Estado = Estado + 1;
                        break;
                    }
                }*/
                if (fn_util_trim(row.EstadoPago) == "Pagado") {
                    pagados = pagados + 1;

                }
                if ((fn_util_trim(row.CodEstadoCobro) == "H") || (fn_util_trim(row.CodEstadoCobro) == "C") || (fn_util_trim(row.CodEstadoCobro) == "I")) {
                    cobrados = cobrados + 1;
                }
            }
        } //For
    } //For
    //Fin JJM IBK
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder eliminar.", "util/images/warning.gif", "ELIMINAR MULTA");
    } else { //cierra if (IsNullOrEmpty(id))
        var vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow');

        if (vElementosAEditar != "") {
            var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', vElementosAEditar[0]);
            //Inicio IBK - AAE
            // Si no tengo perfil de supervisor y hay alguno pagado/cobrado
            if ( ((Perfil != '6') && (Perfil != '1') && (Perfil != '11')) && ((pagados > 0) || (cobrados > 0))) {
                parent.fn_mdl_mensajeIco("Uno de los impuestos no puede ser eliminado por su estado pagado/cobrado.", "util/images/warning.gif", "EDITAR IMPUESTO");
            } else { //cierra if ( ((Perfil != '6') && (Perfil != '1')...
                //Si alguno de los impuestos se encuentra cobrado
                if (cobrados > 0) {
                    parent.fn_mdl_mensajeIco("Uno de los impuestos no puede ser eliminado por su estado cobrado, por favor extorne el cobro antes de eliminarlo.", "util/images/warning.gif", "EDITAR IMPUESTO");
                } else {//cierra if (cobrados > 0) 
                    parent.fn_mdl_confirma(Resultado == '' ? "¿Está seguro que desea eliminar los Impuestos seleccionados?" : Resultado,
	        			function() {

	        			    parent.fn_blockUI();

	        			    var strCodigoMulta = $("#hddRowIdMulta").val().substring(0, $("#hddRowIdMulta").val().length - 1);
	        			    var strLoteE = $("#txtNroLoteCarga").val();
	        			    
	        			    var arrParametros = ["pstrCodigosImpuestos", strCodigoMulta,
	        			                         "pstrNroLote", strLoteE];
	        			    fn_util_AjaxWM("frmMultaVehicularRegistro.aspx/EliminarMultaVehicular",
	        					arrParametros,
	        					function(resultado) {
	        					    fn_ListarMultasLote('');
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
	        			'ELIMINAR MULTA'
	        		);//Parent.fn_mdl_confirma
                }; //cierra else (cobrados > 0) 
            }; //cierra else ( ((Perfil != '6') && (Perfil != '1')... 
            /*if (Estado > 0) {
                parent.fn_mdl_mensajeIco("Uno de los impuestos no puede ser eliminado por su estado pagado.", "util/images/warning.gif", "EDITAR IMPUESTO");
            } else {
                if (EstadoSup > 0) {
                    Resultado = "Uno de los impuestos no puede ser eliminado por su estado pagado.¿Está seguro que eliminar los items seleccionados?";
                } else {
                    parent.fn_mdl_confirma(Resultado == '' ? "¿Está seguro que desea eliminar los Impuestos seleccionados?" : Resultado,
           		        			function() {

           		        			    parent.fn_blockUI();

           		        			    var strCodigoMulta = $("#hddRowIdMulta").val().substring(0, $("#hddRowIdMulta").val().length - 1);

           		        			    var arrParametros = ["pstrCodigosImpuestos", strCodigoMulta];
           		        			    fn_util_AjaxWM("frmMultaVehicularRegistro.aspx/EliminarMultaVehicular",
           		        					arrParametros,
           		        					function(resultado) {
           		        					    fn_ListarMultasLote('');
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
           		        			'ELIMINAR MULTA'
           		        		);
                }
            }*/
            //Fin IBK
        } else {// cierra if (vElementosAEditar != "") 
            parent.fn_mdl_mensajeIco("Seleccione un registro para poder eliminar.", "util/images/warning.gif", "ELIMINAR MULTA");
        } //cierra else (vElementosAEditar != "")
    } //cierra else (IsNullOrEmpty(id))
    fn_OcultaMunicipalidad();
}


//****************************************************************
// Funcion		:: 	fn_abreEditarMulta
// Descripción	::	Abre Editar Registro
// Log			:: 	AEP - 09/11/2012
//****************************************************************
function fn_abreEditarMulta() {
    var Resultado = '';
    var id = $("#hddRowIdMulta").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "EDITAR MULTA");
    } else {
        var vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow');
        if (vElementosAEditar.length > 1) {
            parent.fn_mdl_mensajeIco("Seleccione un sólo registro.", "util/images/warning.gif", "EDITAR MULTA");
        } else {
            if (vElementosAEditar != "") {
                var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', vElementosAEditar[0]);
                var strPlaca = rowData.Placa;
                var strSecMulta = rowData.SecMulta;
                var strCodMulta = rowData.CodEstadoPago;
                var strCodMun = $("#txtCodMunicipalidad").val();
                //Inicio IBK - AAE
                var Pago = rowData.EstadoPago;
                var estCobro = rowData.CodEstadoCobro;
                var cobrado = "0";
                var pagado = "0";
                var supervisor = "0";
                /*
                if (fn_util_trim(strCodMulta) == "002") {
                    parent.fn_mdl_mensajeIco("No se puede editar por estar en el estado pagado", "util/images/warning.gif", "EDITAR MULTA");
                } else {
                    parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?codImp=" + strSecMulta + "&placa=" + strPlaca + "&origen=2" + "&fechaT=" + $("#hddFecTransferencia").val() + "&strNroLote=" + strLote, 900, 450, function() { });
                }*/
                if (fn_util_trim(Pago) == "Pagado") {
                    pagado = "1" 
                }
                if ((Perfil == '6')||(Perfil == '11')||(Perfil == '1')) {//Perfil Supervisor Leasginif()
                    supervisor = "1";
                    if ((fn_util_trim(estCobro) == "H") || (fn_util_trim(estCobro) == "I") || (fn_util_trim(estCobro) == "C")) {
                        cobrado = "1"                      
                    }
                }
                else {//Otros Perfiles
                    if ((fn_util_trim(estCobro) == "H") || (fn_util_trim(estCobro) == "I") || (fn_util_trim(estCobro) == "C")) {
                        cobrado = "1";                        
                    }
                }
                if ((supervisor == "0") && ((cobrado == "1")||(pagado == "1") ) ) {
                    parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?codImp=" + strSecMulta + "&placa=" + strPlaca + "&origen=2" + "&fechaT=" + $("#hddFecTransferencia").val() + "&codMuni="+strCodMun+"&strNroLote=" + strLote+ "&ReadOnly=Y", 900, 550, function() { });
                } else {//if ((supervisor == "0") && ((cobrado == "1")||(pagado == "1") ) )
                    if (supervisor == "1") {
                        parent.fn_mdl_confirma(Resultado == '' ? "¿Está seguro que desea Editar los Impuestos seleccionados?" : Resultado,
           	        		    function() {
                                    parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?codImp=" + strSecMulta + "&placa=" + strPlaca + "&origen=2" + "&fechaT=" + $("#hddFecTransferencia").val() + "&codMuni="+strCodMun+"&strNroLote=" + strLote+ "&ReadOnly=N", 900, 550, function() { });
	        	                },
           	        		    "Util/images/question.gif",
           	        		    function() {
	        	                parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?codImp=" + strSecMulta + "&placa=" + strPlaca + "&origen=2" + "&fechaT=" + $("#hddFecTransferencia").val() + "&codMuni=" + strCodMun + "&strNroLote=" + strLote + "&ReadOnly=Y", 900, 550, function() { });
           	        		    },
           	        		    'ELIMINAR IMPUESTO'
           	        	    ); 
           	        } else { //cierra if (supervisor == "1")
           	        parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?codImp=" + strSecMulta + "&placa=" + strPlaca + "&origen=2" + "&fechaT=" + $("#hddFecTransferencia").val() + "&codMuni=" + strCodMun + "&strNroLote=" + strLote + "&ReadOnly=N", 900, 550, function() { });
           	        } //cierra ELSE (supervisor == "1") 
                }  //ELSE ((supervisor == "0") && ((cobrado == "1")||(pagado == "1") ) )                 
                //Fin IBK        
            } else {
                parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "EDITAR MULTA");
            }
        }
    }
}


//****************************************************************
// Funcion		:: 	fn_abreNuevo
// Descripción	::	Abre Nuevo Registro
// Log			:: 	AEP - 08/11/2012
//****************************************************************
function fn_abreNuevoMulta() {
    var id = $("#hddRowId").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder editar.", "util/images/warning.gif", "AGREGAR IMPUESTO");
    } else {
        if ($("#txtCodMunicipalidad").val() == '') {
            parent.fn_mdl_mensajeIco("Seleccione una municipalidad para poder agregar una multa.", "util/images/warning.gif", "AGREGAR IMPUESTO");
        } else {

            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            var CodigoSolicitudCredito = rowData.CodSolicitudCredito;
            var Placa = rowData.Placa;
            //var CodMunicipalidad = rowData.CodMunicipalidad;
            //var Descripcion = rowData.Descripcion;
            //var CodBien = rowData.SecFinanciamiento;
            //var CodUnico = rowData.CodUnico;
            //Inicio IBK - AAE  
            var lote =$("#hidTengoLote").val() 
            //parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?csc=" + CodigoSolicitudCredito + "&placa=" + Placa + "&origen=1" + "&CodMuni=" + $("#txtCodMunicipalidad").val() + "&strNroLote=" + strLote, 900, 450, function() { });
            parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?csc=" + CodigoSolicitudCredito + "&placa=" + Placa + "&origen=1" + "&CodMuni=" + $("#txtCodMunicipalidad").val() + "&strNroLote=" + strLote+ '&codNroLote=' + lote, 900, 550, function() { });
            //Fin IBK
        }

    }
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
		    //Inicio IBK - AAE
		    //fn_EliminaLote();

		    //fn_util_redirect('frmMultaVehicularListado.aspx');
		    fn_util_redirect('frmMultaVehicularListado.aspx?NroLote=' + strLote);
		    //Fin IBK
		},
		"../../util/images/question.gif",
		function() {
		},
		'Gestión del Bien : Impuesto Vehicular'
	);


}

//****************************************************************
// Funcion		:: 	fn_generaLote
// Descripción	::	Genera Lote
// Log			:: 	JRC - 25/11/2012
//****************************************************************
/*Inicio IBK - AAE - Cambio la función, ahora solo calculo totales del lote*/
function fn_generaLote() {
    parent.fn_blockUI();
    var arrParametros = ["strNroLote", strLote];

    fn_util_AjaxWM("frmMultaVehicularRegistro.aspx/ReGenerarLote",
    					arrParametros,
    					function(resultado) {
    					    parent.fn_unBlockUI();
    					    parent.fn_mdl_mensajeOk("Se actualizó el Lote Nº" + fn_util_trim(resultado), function() { fn_Redireccion(); }, "GRABADO CORRECTO");
    					},
    					function(resultado) {
    					    var error = eval("(" + resultado.responseText + ")");
    					    parent.fn_mdl_mensajeIco(error.Message, "../../util/images/error.gif", "ERROR GENERAR LOTE");
    					});

}

function fn_generaLote_old() {
    fn_ObtieneImpuestos();
    //
    var vElementosAEditar = $("#jqGrid_lista_B").jqGrid('getRowData');
    var count = vElementosAEditar.length;

    if (IsNullOrEmpty(count)) {
        parent.fn_mdl_mensajeIco("Seleccione al menos un registro para poder generar el lote.", "util/images/warning.gif", "GENERAR LOTE");
    } else {
        if (vElementosAEditar.length < 1) {
            parent.fn_mdl_mensajeIco("Seleccione al menos un registro para poder generar el lote.", "util/images/warning.gif", "GENERAR LOTE");
        } else {

            parent.fn_mdl_confirma(
		"¿Está seguro que desea generar el Lote con las multas/infracciones.?",
		function() {
		    //var strCodigoMulta = $("#hddRowIdMulta").val().substring(0, $("#hddRowIdMulta").val().length - 1);
		    var arrParametros = ["strCodigoImpuesto", strCodigosImpuestos, "strNroLote", strLote, "strMunicipalidad", $("#txtCodMunicipalidad").val()];
		    fn_util_AjaxSyncWM("frmMultaVehicularRegistro.aspx/GenerarLote",
							 arrParametros,
							 function(result) {
							     parent.fn_unBlockUI();
							     if (fn_util_trim(result) == "0") {
							         parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL GENERAR LOTE");
							     } else {
							         parent.fn_mdl_mensajeOk("El Lote se generó correctamente. Se generó el Lote Nº" + fn_util_trim(result), function() { fn_util_globalRedirect("/GestionBien/MultaVehicular/frmMultaVehicularListado.aspx"); }, "GRABADO CORRECTO");
							     }
							 },
							 function(resultado) {
							     parent.fn_unBlockUI();
							     var error = eval("(" + resultado.responseText + ")");
							     parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABADO");
							 }
						);

		},
		"Util/images/question.gif",
		function() { },
		'CONFIRMACION'
	);
        }
    }
    //    }
    //	parent.fn_mdl_confirma(
    //		"¿Está seguro que desea generar el Lote con las multas/infracciones seleccionados?",
    //		function() {                        
    //			
    //			
    //				var vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow'); 
    //				var count = vElementosAEditar.length;
    //				
    //				if(IsNullOrEmpty(count)) {
    //					parent.fn_mdl_mensajeIco("Seleccione al menos un registro para poder generar el lote.", "util/images/warning.gif", "GENERAR LOTE");		
    //				}else{
    //					if (vElementosAEditar.length < 1) {
    //						parent.fn_mdl_mensajeIco("Seleccione al menos un registro para poder generar el lote.", "util/images/warning.gif", "GENERAR LOTE");		
    //					}else{
    //						
    //							var strCodigoMulta = $("#hddRowIdMulta").val().substring(0,$("#hddRowIdMulta").val().length-1);
    //						//alert(strCodigosImpuestos);		
    //						
    //						var arrParametros = ["strCodigoImpuesto", strCodigoMulta,"strNroLote",''];                
    //						fn_util_AjaxSyncWM("frmMultaVehicularRegistro.aspx/GenerarLote",
    //							 arrParametros,
    //							 function(result) {
    //								 parent.fn_unBlockUI();
    //								 if (fn_util_trim(result) == "0") {
    //									 parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/error.gif", "ERROR AL GENERAR LOTE");
    //								 } else {                                                  
    //									 parent.fn_mdl_mensajeOk("El Lote se generó correctamente. Se generó el Lote Nº"+fn_util_trim(result), function() { fn_util_globalRedirect("/GestionBien/MultaVehicular/frmMultaVehicularListado.aspx"); }, "GRABADO CORRECTO");
    //								 }
    //							 },
    //							 function(resultado) {
    //								 parent.fn_unBlockUI();
    //								 var error = eval("(" + resultado.responseText + ")");
    //								 parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN GRABADO");
    //							 }
    //						);	
    //						
    //			        	
    //					}		
    //				}

    //		},
    //		"Util/images/question.gif",
    //		function() { },
    //		'CONFIRMACION'
    //	);

}



//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Funcion		:: 	fn_GBAbreDocumentos
// Descripción	::	Abre Documentos
// Log			:: 	??? - 02/11/2012
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
function fn_GBAbreDocumentos(pstrCodContrato, pstrCodBien, pstrCodRelacionado, pstrCodTipo) {

    if ($("#hddFecTransferencia").val() != '') {
        parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo + "&hddVer=1", 800, 350, function() { });
    } else {
        parent.fn_util_AbreModal2("Documentos y Comentarios :: Listado", "GestionBien/frmGestionBienDocListado.aspx?hddCodContrato=" + pstrCodContrato + "&hddCodBien=" + pstrCodBien + "&hddCodRelacionado=" + pstrCodRelacionado + "&hddCodTipo=" + pstrCodTipo, 800, 350, function() { });
    }
}


//****************************************************************
// Funcion		:: 	fn_verImpuesto
// Descripción	::	Ver Impuesto
// Log			:: 	JRC - 03/12/2012
//****************************************************************
function fn_verImpuesto() {

    var id = $("#hddRowIdMulta").val();
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un registro para poder visualizar.", "util/images/warning.gif", "VER IMPUESTO");
    } else {
        var vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow');
        if (vElementosAEditar.length > 1) {
            parent.fn_mdl_mensajeIco("Seleccione un sólo registro.", "util/images/warning.gif", "VER IMPUESTO");
        } else {
            if (vElementosAEditar != "") {
                var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', vElementosAEditar[0]);
                var strPlaca = rowData.Placa;
                var strSecMulta = rowData.SecMulta;
                var strCodMulta = rowData.CodEstadoPago;
                parent.fn_util_AbreModal("Gestión del Bien :: Nuevo", "GestionBien/MultaVehicular/frmMultaVehicularRegistroAgregar.aspx?codImp=" + strSecMulta + "&placa=" + strPlaca + "&origen=3", 900, 450, function() { });
            } else {
                parent.fn_mdl_mensajeIco("Seleccione un registro para poder visualizar.", "util/images/warning.gif", "VER IMPUESTO");
            }
        }
    }

}
//Inicio IBK JJM
function fn_validaMunicipalidad() {
    var Municipalidad = $("#hddMunicipalidad").val();
    var lblCheckedResult = Municipalidad.split(",");
    var pstrMunicipalidad = 0;
    for (var i = 1; i < lblCheckedResult.length; i++) {
        if (lblCheckedResult[i - 1] != lblCheckedResult[i]) {
            if (lblCheckedResult[i] != "") {
                pstrMunicipalidad = pstrMunicipalidad + 1;
            }
        }
        if (pstrMunicipalidad > 0) { break; }
    }
    $("#hddMunicipalidad").val(pstrMunicipalidad);
}

function fn_ObtieneImpuestos() {
    //
    var rowData = $("#jqGrid_lista_B").jqGrid('getRowData');
    for (var i = 0; i < rowData.length; i++) {
        var row = rowData[i];
        strCodigosImpuestos = strCodigosImpuestos + row.SecMulta + ',';
    }

    strCodigosImpuestos = strCodigosImpuestos.substring(0, strCodigosImpuestos.length - 1);


}
function fn_OcultaMunicipalidad() {
    if (strRegistros == 2) {
        $("#imgBsqMunicipalidad").attr('disabled', 'disabled');
        strRegistros = 0;
    } else { $("#imgBsqMunicipalidad").attr('enabled', 'enabled'); }
}
function fn_EliminaLote() {
    var Lote = $("#txtNroLoteCarga").val();
    var arrParametros = ["pLote", Lote];
    fn_util_AjaxSyncWM("frmMultaVehicularRegistro.aspx/EliminarLote",
            arrParametros,
                        function(resultado) {
                            parent.fn_unBlockUI();
                            var error = eval("(" + resultado.responseText + ")");
                            parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR Al CARGAR");
                        }
            );
}
function fn_obtenerMuncipalidad(pClaveId, pValor1) {    
    $('#txtCodMunicipalidad').val(fn_util_trim(pClaveId));
    $('#txtMunicipalidad').val(fn_util_trim(pValor1));

    if ($("#txtCodMunicipalidad").val() != "") {
        //Inicio IBK - AAE - Comento cargar e lote por municipalidad
        //fn_ListarMultasLote('');
        // Fin IBK
    }
    //Resize Pantalla
    fn_doResize();
}
//Fin JJM IBK
//Inicio IBK - AAE - Agrego función para anular el lote
function fn_AnularLote() {


    fn_mdl_confirma('¿Está seguro de Anular el lote?',
		function() {

		    parent.fn_blockUI();


		    var Lote = $("#txtNroLoteCarga").val();
		    var arrParametros = ["pLote", Lote];
		    fn_util_AjaxSyncWM("frmMultaVehicularRegistro.aspx/AnularLote",
            arrParametros,
                function(resultado) {
                    parent.fn_unBlockUI();
                    var arrResultado = resultado.split("|");
                    if (arrResultado[0] == '0') {
                        parent.fn_mdl_mensajeOk("Se anuló el lote correctamente", function() { fn_Redireccion(); }, "GRABADO CORRECTO");
                    } else {
                        parent.fn_mdl_mensajeIco(fn_util_trim(arrResultado[1]), "../../util/images/error.gif", "Error Anular Lote");
                    }
                },
				function(resultado) {
				    var error = eval("(" + resultado.responseText + ")");
				    parent.fn_mdl_mensajeIco(error.Message, "../../util/images/error.gif", "ERROR ANULAR LOTE");
				});
		},
		"../../util/images/question.gif",
		function() {
		},
		'IMPUESTO VEHICULAR'
	);
            
}


function fn_Redireccion() {
    window.location = "frmMultaVehicularListado.aspx";
}

//Fin IBK