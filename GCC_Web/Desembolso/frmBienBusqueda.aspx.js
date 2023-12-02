//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 25/02/2012
//****************************************************************
$(document).ready(function() {
    fn_inicializaCampos();
    fn_cargaGrilla();
});

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa Campos
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_inicializaCampos() {

    //var strCodSolicitudCredito = $("#hddCodSolicitudCredito").val(); 
    //var strCodigoBien = $("#hddClasificacionBien").val();
    //alert(strCodigoBien);

    fn_cargaTipoBien($("#hddClasificacionBien").val());

    var arrParametros = ["pstrOp", "7", "pstrDominio", "TBL014", "pstrParametro", $("#hddClasificacionBien").val()];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#txtClasificacionBien').val(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }    
    
}


//****************************************************************
// Funcion		:: 	fn_cargaTipoBien
// Descripción	::	Carga Combo TipoBien
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaTipoBien(strValor) {

    var arrParametros = ["pstrOp", "4", "pstrTablaGenerica", "TBL104", "pstrCodigoGenerico", strValor];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbTipoBien').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    
}


//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() {
    $("#jqGrid_lista_E").jqGrid({
        datatype: function() {
            fn_ListaBienesContrato();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "SecFinanciamiento"
        },
        colNames: ['','TipoRubroFinanciamiento','Nº Bien', 'Descripción Bien', 'Clasif. Bien', 'Tipo Bien', 'Valor Bien', 'Editar', 'Asociar'],
        colModel: [
                    { name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true},
                    { name: 'TipoRubroFinanciamiento', index: 'TipoRubroFinanciamiento', hidden: true },
                    { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
                    { name: 'Comentario', index: 'Comentario', width: 90, align: "left" },
                    { name: 'NombreRubroFinanciamiento', index: 'NombreRubroFinanciamiento', width: 100, align: "left" },
                    { name: 'NombreTipoBien', index: 'NombreTipoBien', width: 60, align: "left" }, //NombreTipoBien
                    { name: 'ValorBien', index: 'ValorBien', width: 50, align: "right", hidden: true },
                    { name: 'Editar', index: 'Editar', width: 20, align: "center", sortable: false, formatter: fn_EditarBien },
                    { name: '', index: '', width: 30, align: "center",formatter: fn_Check }                    
                  ],
        height: '100%',
        pager: '#jqGrid_pager_E',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,
        rowList: [10, 20, 30],
        sortname: 'SecFinanciamiento',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass'
    });
    function fn_Check(cellvalue, options, rowObject) {
 
        if ($("#hddEstadoDocumento").val() == "4" || $("#hddEstadoDocumento").val() == "3") {
            
            if ($("#hddTipoSubcontrato").val() == "002") {
                if (fn_util_trim(rowObject.cCheck) == 0) {
                    return "<input id='chkAsociarBien' name='chkAsociarBien' type='checkbox' runat='server' onclick='javascript:fn_AsociaBien(this, " + rowObject.SecFinanciamiento + "," + fn_util_ValidaDecimal(rowObject.ValorBien) + " )' />";
                } else {
                    return "<input id='chkAsociarBien' name='chkAsociarBien' type='checkbox' runat='server' onclick='javascript:fn_AsociaBien(this, " + rowObject.SecFinanciamiento + "," + fn_util_ValidaDecimal(rowObject.ValorBien) + " )' checked = 'checked' />";
                }
                
            }else {
                if (fn_util_trim(rowObject.cCheck) == 0) {
                     return "<input id='chkAsociarBien' name='chkAsociarBien' type='checkbox' runat='server' onclick='' disabled='true'  />";
                } else {
                     return "<input id='chkAsociarBien' name='chkAsociarBien' type='checkbox' runat='server' onclick='' disabled='true' checked='checked' />";
                }
            }
        }else {
      
                if (fn_util_trim(rowObject.cCheck) == 0) {  
                	
                return "<input id='chkAsociarBien' name='chkAsociarBien' type='checkbox' runat='server' onclick='javascript:fn_AsociaBien(this," + rowObject.SecFinanciamiento + "," + fn_util_ValidaDecimal(rowObject.ValorBien) + ");' />";
            } else {   
                return "<input id='chkAsociarBien' name='chkAsociarBien' type='checkbox' runat='server' onclick='javascript:fn_AsociaBien(this, " + rowObject.SecFinanciamiento + "," + fn_util_ValidaDecimal(rowObject.ValorBien) + "  )' checked='checked' />";        
            }
        }
   };
    
    function fn_EditarBien(cellvalue, options, rowObject) {
        
        return "<img src='../Util/images/ico_mdl_demanda.gif' alt='Asociar bienes' title='Editar' width='20px' height='20px' onclick='javascript:fn_abreEditarBien(" + rowObject.SecFinanciamiento + ");' style='cursor: pointer;cursor: hand;' />";
    };
}


//****************************************************************
// Funcion		:: 	fn_AsociaBien
// Descripción	::	AsociaBien
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_AsociaBien(objCheck, pstrCodBien, pstrValorBien) {

    parent.fn_blockUI();
    var arrParametros = ["strNumContrato", $("#hddCodSolicitudCredito").val(),    
                         "pstrCodProveedor", $("#hidCodProveedor").val(),
                         "pstrNumeroTipo", $("#hidTipoDocumento").val(),
                         "pstrNumeroDoc1", $("#hidNumeroDocumento").val(),
                         "pstrFecEmision", $("#hidFecEmision").val(),
                         "strCodigoBien", pstrCodBien
                        ];
                            
    if (objCheck.checked) {
		
		var decValorBien = parseFloat(pstrValorBien);
		if( decValorBien > 0 ){

			fn_util_AjaxWM("frmBienBusqueda.aspx/AsociaBien",
						arrParametros,
						function() {
							fn_ListaBienesContrato(); 
							fn_doResize();
							parent.fn_mdl_mensajeOk("Se asoció correctamente el Bien al Documento.", function() { }, "ASOCIACION CORRECTA");
						},
						function(resultado) {
							var error = eval("(" + resultado.responseText + ")");
							parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR Al GUARDAR");
						}
			);  
			
        }else{
			objCheck.checked = false;
			parent.fn_unBlockUI();
			parent.fn_mdl_mensajeIco("El bien no puede ser asociado debido que no cuenta con un Valor.", "util/images/error.gif", "ADVERTENCIA");
			
        }
        
    } else {

        fn_util_AjaxWM("frmBienBusqueda.aspx/DesasociaBien",
                    arrParametros,
                    function() {
                        fn_ListaBienesContrato();
                        fn_doResize();
                        parent.fn_mdl_mensajeOk("Se desasoció correctamente el Bien al Documento.", function() { }, "ASOCIACION CORRECTA");
                    },
                    function(resultado) {
                        var error = eval("(" + resultado.responseText + ")");
                        parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR Al GUARDAR");
                    }
        );  
        
    }

}



//****************************************************************
// Funcion		:: 	fn_abreEditarBien
// Descripción	::	Abre Editar Bienes Contrato
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_abreEditarBien(srtSecFinanciamiento) {
    //alert(Codclasibien);
    parent.fn_util_AbreModal2("MANTENIMIENTO :: BIENES", "Desembolso/frmBienEditar.aspx?Codclasibien=" + $("#hddClasificacionBien").val() + "&CodContrato=" + $("#hddCodSolicitudCredito").val() + "&SecFinanciamiento=" + srtSecFinanciamiento , 950, 500, function() { });
}

//****************************************************************
// Funcion		:: 	fn_ListaBienesContrato
// Descripción	::	Lista Bienes Contrato
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_ListaBienesContrato() {    
    try {
        parent.fn_blockUI();       
        var TipoBien = $("#cmbTipoBien option:selected").val() == "0" || $("#cmbTipoBien option:selected").val() == undefined ? "" : $("#cmbTipoBien option:selected").val();
        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_E", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_E", "page"), // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_E", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_E", "sortorder"), // Criterio de ordenación
                             "pstrNroContrato", $('#hddCodSolicitudCredito').val(),
                             "pstrCodProveedor", $('#hidCodProveedor').val(),
                             "pstrNumeroTipo", $('#hidTipoDocumento').val(),
                             "pstrNumeroDoc1", $('#hidNumeroDocumento').val(),
                             "pstrFecEmision", $('#hidFecEmision').val(),
                             "pstrCodigoTipoBien", TipoBien
                            ];

        fn_util_AjaxWM("frmBienBusqueda.aspx/ListaBienesContrato",
                arrParametros,
                function(jsondata) {                    
                    jqGrid_lista_E.addJSONData(jsondata);
                    parent.fn_unBlockUI();
                    fn_doResize();
                },
                function(request) {
                    parent.fn_unBlockUI();
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

    } catch (ex) {
        parent.fn_unBlockUI();
        fn_util_alert(ex.message);
    }

}


function fn_Limpiar() {
    $('#cmbTipoBien').val('0');
    
}


