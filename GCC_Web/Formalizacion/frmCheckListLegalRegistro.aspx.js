//****************************************************************
//Variables Globales
  var C_ORIGENDOC_CHECKLIST_LEGAL = "003";
  var C_intFlagFiltroDocumento = 1;
  var C_intpFlagEnvioCarta = 1;
  var C_intpFlagFiltroCondicionAdicional = 2;
  var C_strpUbigeoLima = "150100";
  var C_strpTipoRepresentante = "001";
  var c_intFlagEnvioLegal = 0;
  var C_MOSTRAR_DOCUMENTOS_ADICIONALES = "1";
  var C_CODIGO_TIPO_REPRESENTANTE = "001";
  var intTotalRegistros;

// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene metodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
//****************************************************************
  $(document).ready(function() {

      //Setea Calendario
      fn_util_SeteaCalendario($('input[id*=txtFecha]'));

      //Valida Campos
      fn_inicializaCampos();

      //Valida Tabs
      $("div#divTabs").tabs({
          show: function(event, ui) {
              fn_doResize();
          }
      });

      fn_cargaGrilla();
      $("#jqGrid_lista_C").setGridWidth($(window).width() - 800);
      $("#jqGrid_lista_F").setGridWidth($(window).width() - 300);

      //Valida Bloqueo
      fn_ValidaBloqueo();
      
      //Inicio IBK - AAE - 12/02/2013
      if (($("#hidTipoCronograma").val() == "ANU") || ($("#hidTipoCronograma").val() == "BIM") || ($("#hidTipoCronograma").val() == "MEN") || ($("#hidTipoCronograma").val() == "SEM") || ($("#hidTipoCronograma").val() == "TRI")) {
          $("#txtFechaMaxActivacion").datepicker("destroy");
          $('#txtFechaMaxActivacion').attr('class', 'css_input_inactivo');
          $('#txtFechaMaxActivacion').prop('readonly', true);
      }
      //Fin IBK

      fn_onLoadPage();

  });

//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistrito
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_LimpiaComboDistrito() {
    $('#cmbDistrito').empty();
    $('#cmbDistrito').html('<option value="0">[- Seleccionar -]</option>');
}

//****************************************************************
// Funcion		:: 	fn_cargaComboProvincia
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_cargaComboProvincia(valor) {

        var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
        var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

        if (arrResultado.length > 0) {
            if (arrResultado[0] == "0") {
                $('#cmbProvincia').html(arrResultado[1]);
            } else {
                var strError = arrResultado[1];
                fn_mdl_alert(strError.toString(), function() { });
            }
        }
        fn_ListagrillaRepresentantes();
        fn_LimpiaComboDistrito();
    }


//****************************************************************
// Funcion		:: 	fn_cargaComboDistrito
// Descripción	::	F
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_cargaComboDistrito(strCodigoDepartamento, strCodigoProvincia) {
    
    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbDistrito').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_ListagrillaRepresentantes();

}

//****************************************************************
// Funcion		:: 	fn_ListagrillaDocumentos
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_ListagrillaDocumentos() {
    var arrParametros = ["pPageSize",      fn_util_getJQGridParam("jqGrid_lista_H", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage",   fn_util_getJQGridParam("jqGrid_lista_H", "page"), // Página actual
                         "pSortColumn",    fn_util_getJQGridParam("jqGrid_lista_H", "sortname"), // Columna a ordenar
                         "pSortOrder",     fn_util_getJQGridParam("jqGrid_lista_H", "sortorder"), // Criterio de ordenación
                         "pCodigo",        $("#txtNumeroContrato").val(),
                         "pFlagFiltro",     C_intFlagFiltroDocumento,
                         "pFlagEnvioCarta", C_intpFlagEnvioCarta
                        ];
    
    fn_util_AjaxSyncWM("frmCheckListLegalRegistro.aspx/ListaCondicionesAdicionales",
    arrParametros,
    function(jsondata) {
        jqGrid_lista_H.addJSONData(jsondata);
        parent.fn_unBlockUI();
    },
    function(request) {
        fn_util_alert(jQuery.parseJSON(request.responseText).Message);
        parent.fn_unBlockUI();
    }
   );

    //Hiden para limpiar la variable de Aprobacion de 
    $("#hddFlagVerificaAdjunto").val(""); 
}
//****************************************************************
// Funcion		:: 	fn_ListagrillaCondicionAdicional
// Descripción	::	
//                  de resultados.
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_ListagrillaCondicionAdicional() {
        var arrParametros = ["pPageSize",     fn_util_getJQGridParam("jqGrid_lista_E", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage",  fn_util_getJQGridParam("jqGrid_lista_E", "page"), // Página actual
                             "pSortColumn",   fn_util_getJQGridParam("jqGrid_lista_E", "sortname"), // Columna a ordenar
                             "pSortOrder",    fn_util_getJQGridParam("jqGrid_lista_E", "sortorder"), // Criterio de ordenación
                             "pCodigo",        $("#txtNumeroContrato").val(),
                             "pFlagFiltro",     C_intpFlagFiltroCondicionAdicional,
                             "pFlagEnvioCarta", C_intpFlagEnvioCarta
                             ];
        fn_util_AjaxSyncWM("frmCheckListLegalRegistro.aspx/ListaCondicionesAdicionales",
        arrParametros,
        function(jsondata) {
           jqGrid_lista_E.addJSONData(jsondata);
           parent.fn_unBlockUI();
        },
        function(request) {
           fn_util_alert(jQuery.parseJSON(request.responseText).Message);
           parent.fn_unBlockUI();
        }
       );
    }
    //****************************************************************
    // Funcion		:: 	fn_ListagrillaRepresentantes
    // Descripción	::	Carga Grilla
    // Log			:: 	IJM - 25/02/2012
    //****************************************************************

    function fn_ListagrillaRepresentantes() {

        var strDepartamento = $("#cmbDepartamento").val();
        if (strDepartamento == 0 || strDepartamento == null) {
            strDepartamento = "00";
        }

        var strProvincia = $("#cmbProvincia").val();
        if (strProvincia == 0 || strProvincia == null) {
            strProvincia = "";
        }


        var strDistrito = $("#cmbDistrito").val();
        if (strDistrito == 0 || strDistrito == null) {
            strDistrito = "";
        }

        var strubigeo = (strDepartamento + '' + strProvincia + '' + strDistrito);
        
       var arrParametros =  ["pPageSize",           fn_util_getJQGridParam("jqGrid_lista_C", "rowNum"),    // Cantidad de elementos de la página
                             "pCurrentPage",        fn_util_getJQGridParam("jqGrid_lista_C", "page"),      // Página actual
                             "pSortColumn",         fn_util_getJQGridParam("jqGrid_lista_C", "sortname"),  // Columna a ordenar
                             "pSortOrder",          fn_util_getJQGridParam("jqGrid_lista_C", "sortorder"), // Criterio de ordenación
                             "pfirma",              $("#cmbcontratofirma option:selected").val(),          // Se Firma en lima o Provincia
                             "pUbigeo",             strubigeo,                                             //Ubigeo Lima
                             "pTipoRepresentante",  C_strpTipoRepresentante,                               // 001 Interbank 002 Cliente
                             "pNumeroContrato",     $("#txtNumeroContrato").val()
                            ];

        fn_util_AjaxSyncWM("frmCheckListLegalRegistro.aspx/Listarepresentantes",
        arrParametros,
        function(jsondata) {
                jqGrid_lista_C.addJSONData(jsondata);
                fn_doResize();
        },
        function(request) {
            fn_util_alert(jQuery.parseJSON(request.responseText).Message);
        }
   );
    }

    //****************************************************************
    // Funcion		:: 	fn_ListagrillaRepresentantesContrato
    // Descripción	::	Carga Grilla
    // Log			:: 	JRC - 25/02/2012
    //****************************************************************
    function fn_ListagrillaRepresentantesContrato() {
        var arrParametros = ["pPageSize",           fn_util_getJQGridParam("jqGrid_lista_B", "rowNum"),    // Cantidad de elementos de la página
                             "pCurrentPage",        fn_util_getJQGridParam("jqGrid_lista_B", "page"),      // Página actual
                             "pSortColumn",         fn_util_getJQGridParam("jqGrid_lista_B", "sortname"),  // Columna a ordenar
                             "pSortOrder",          fn_util_getJQGridParam("jqGrid_lista_B", "sortorder"), // Criterio de ordenación
                             "pNumeroContrato",          $("#txtNumeroContrato").val(),                    // Codigo de Contrato
                             "pCodigoTipoRepresentante", C_strpTipoRepresentante,                          // Tipo Represennatante 001 Interbank 002 Cliente
                             "pfirma",                   $("#hddFirmaen").val()                            // Se firma en lima O provincia
                             ];

        fn_util_AjaxSyncWM("frmCheckListLegalRegistro.aspx/ListaRepresentantesContrato",
        arrParametros,
        function(jsondata) {
            jqGrid_lista_B.addJSONData(jsondata);
            fn_doResize();
        },
        function(request) {
            fn_util_alert(jQuery.parseJSON(request.responseText).Message);
        }
        );
                    }
    
      //****************************************************************
    // Funcion		:: 	fn_ListagrillaRepresentantesContratoTotal
    // Descripción	::	Carga Grilla
    // Log			:: 	AEP - 20/08/2012
    //****************************************************************
    function fn_ListagrillaRepresentantesContratoTotal() {
        var arrParametros = ["pPageSize",           fn_util_getJQGridParam("jqGrid_lista_B", "rowNum"),    // Cantidad de elementos de la página
                             "pCurrentPage",        1,      // Página actual
                             "pSortColumn",         fn_util_getJQGridParam("jqGrid_lista_B", "sortname"),  // Columna a ordenar
                             "pSortOrder",          fn_util_getJQGridParam("jqGrid_lista_B", "sortorder"), // Criterio de ordenación
                             "pNumeroContrato",          $("#txtNumeroContrato").val(),                    // Codigo de Contrato
                             "pCodigoTipoRepresentante", C_strpTipoRepresentante,                          // Tipo Represennatante 001 Interbank 002 Cliente
                             "pfirma",                   $("#hddFirmaen").val()                            // Se firma en lima O provincia
                             ];

        fn_util_AjaxSyncWM("frmCheckListLegalRegistro.aspx/ListaRepresentantesContratoTotal",
        arrParametros,
        function(request) {
        	intTotalRegistros = request; 
        },
        function(request) {
            fn_util_alert(jQuery.parseJSON(request.responseText).Message);
        }
        );
                    }

    //****************************************************************
    // Funcion		:: 	fn_AgregarRepresentante
    // Descripción	::	
    // Log			:: 	IJM - 25/02/2012
    //****************************************************************
    function fn_AgregarRepresentante() {

            if ($("#hddCodigoRepresentante").val() != '') {
                    var arrParametros = ["pCodigoRepresentante", $("#hddCodigoRepresentante").val(),
                                         "pNumeroContrato", $("#txtNumeroContrato").val(),
                                         "pCodigoTipoRepresentante", C_strpTipoRepresentante   // Si es Representante de interbank o del cliente
                                        ];
                   
                    fn_util_AjaxSyncWM("frmCheckListLegalRegistro.aspx/InsertaRepresentanteContrato",
                    arrParametros,
                    function(jsondata) {
                        fn_ListagrillaRepresentantesContrato();
                        fn_ListagrillaRepresentantes();
                    	fn_ListagrillaRepresentantesContratoTotal();
                    	$("#hddCodigoRepresentante").val('');
                                                    },
                    function(request) {
                        fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                    }
                    );
                }
            else {
                parent.fn_mdl_mensajeIco("Seleccione un Representante", "util/images/warning.gif", "ALERTA");
            }
        }

        //****************************************************************
        // Funcion		:: 	fn_EliminarRepresentante
        // Descripción	::	
        // Log			:: 	IJM - 25/02/2012
        //****************************************************************
        function fn_EliminarRepresentante() {
            if ($("#hddCodigoRepresentanteElimina").val() != '') {
                    var arrParametros = ["pCodigoRepresentante",     $("#hddCodigoRepresentanteElimina").val(),
                                         "pNumeroContrato",          $("#txtNumeroContrato").val(),
                                         "pCodigoTipoRepresentante", C_strpTipoRepresentante                                  //$("#cmbcontratofirma option:selected").val()
                                        ];
                    fn_util_AjaxSyncWM("frmCheckListLegalRegistro.aspx/EliminaRepresentanteContratoItem",
                    arrParametros,
                    function(jsondata) {
                            fn_ListagrillaRepresentantesContrato();
                            fn_ListagrillaRepresentantes();
                    	    fn_ListagrillaRepresentantesContratoTotal();
                    	    $("#hddCodigoRepresentanteElimina").val('');
                    },
                    function(request) {
                            fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                    }
                    );
            } else {
                parent.fn_mdl_mensajeIco("Seleccione un Representante", "util/images/warning.gif", "ALERTA");
            }

        }
//****************************************************************
// Funcion		:: 	fn_Ubigeo
// Descripción	::	Devuelve el ubigeo, a partir de los nombres de la departamento, provincia y distrito de la fila correspondiente.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_Ubigeo(cellvalue, options, rowObject) {
    return fn_util_trim(rowObject.Departamento) + " / " + fn_util_trim(rowObject.Provincia) + " / " + fn_util_trim(rowObject.Distrito);
}


//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrilla() {
    
            /*********************************************************
            GRILLA TAB DOCUMENTOS ADICIONALES 
            **********************************************************/
            $("#jqGrid_lista_E").jqGrid({
            datatype: function() {
                fn_ListagrillaCondicionAdicional();
            },
            jsonReader: //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            {
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodigoContratoDocumento" // Índice de la columna con la clave primaria.
            },

            colNames: ['Legal', '', 'Condiciones Adicionales', 'Ver adjunto', 'Adjuntar', 'Obs. Comercial', 'Obs. Legal', 'Eliminar', '', ''],
            colModel: [
                        { name: '', index: '', width: 15, align: "center", formatter: Check },
                        { name: 'CodigoContratoDocumento', index: 'CodigoContratoDocumento', hidden: true },
                        { name: 'Descripcion', index: 'Descripcion', width: 100, align: "left", sorttype: "string" },
                        { name: 'adjunto', index: 'adjunto', width: 20, align: "center", sorttype: "string", formatter: VerAdjunto1 },
	                    { name: 'SubirArchivo', index: 'SubirArchivo', width: 20, align: "center", sortable: false, formatter: SubirArchivo2 },
                        { name: 'lupa', index: 'lupa', width: 20, align: "center", sortable: false, formatter: Lupa5 },
                        { name: 'lupa', index: 'lupa', width: 20, align: "center", sortable: false, formatter: Lupa4 },
                        { name: 'codigoOrigenCondicion', index: 'codigoOrigenCondicion', width: 20, align: "center", sortable: false, formatter: EliminaSeleccion },
                        { name: 'RecordCount', index: 'RecordCount', hidden: true, formatter: RecordCountDocadd },
                        { name: 'SumFlagAprobacionLegal', index: 'SumFlagAprobacionLegal', hidden: true, formatter: SumTotalLegalDocadd }
                      ],
            height: '100%',
            pager: '#jqGrid_pager_E',
            loadtext: 'Cargando datos...',
            emptyrecords: 'No hay resultados',
            rowNum: 10,                 // Tamaño de la página
            rowList: [10, 20, 30],
            sortname:    'CodigoContratoDocumento', // Columna a ordenar por defecto.
            sortorder:   'asc',   // Criterio de ordenación por defecto.
            viewrecords: true,    // Muestra la cantidad de registros.
            gridview:    true,
            autowidth:   true,
            altRows:     true,
            loadonce:    false,
            altclass:    'gridAltClass',
            onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_E").jqGrid('getRowData', id);
                $("#hddCodigo").val(rowData.CodigoContratoDocumento);
            }
            });
    
            jQuery("#jqGrid_lista_E").jqGrid('navGrid', '#jqGrid_pager_E', { edit: false, add: false, del: false });
            // Le asigna el ancho a usar para la grilla.
            $("#jqGrid_lista_E").setGridWidth($(window).width() - 150);
            $("#search_jqGrid_lista_E").hide();
            
            /*************************  **************************
            AGREGA ICONO Y FUNCIONALIDAD A LA GRILLA DOCUMENTOS   
            **************************  **************************/
            function SubirArchivo2(cellvalue, options, rowObject) {
                var sScript2 = "javascript:AdjuntarArchivoDocumento(" + rowObject.CodigoContratoDocumento + ");";
                return "<img src='../Util/images/ico_acc_adjuntarMini.gif' alt='" + cellvalue + "' title='Subir Archivo' width='20px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
            };
            function Lupa4(cellvalue, options, rowObject) {
                if (rowObject.FlagObservacionLegal == 0) {
                    var sScript2 = "javascript:VerObservacionesLegal(" + rowObject.CodigoContratoDocumento + ",2);";
                    return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
                } else {
                    sScript2 = "javascript:VerObservacionesLegal(" + rowObject.CodigoContratoDocumento + ",2);";
                    return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
                }
            };

            function EliminaSeleccion(cellvalue, options, rowObject) {
                if ( fn_util_trim(rowObject.codigoOrigenCondicion) == C_ORIGENDOC_CHECKLIST_LEGAL) 
                    {
                    var sScript2 = "javascript:fn_EliminarCondicionAdicional(" + rowObject.CodigoContratoDocumento + ");";
                    return "<img src='../Util/images/ico_acc_eliminar.gif' alt='" + cellvalue + "' title=' Eliminar ' width='20px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";                    
                } else {
                    return ".";
                }
           };

           function VerAdjunto1(cellvalue, options, rowObject) {
               if (rowObject.adjunto != "") {
                   var strNombreArchivo = rowObject.adjunto.split('\\').pop();
                   strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
                   return "<img src='../Util/images/ico_download.gif' alt='Descargar/Mostrar Archivo' title='" + strNombreArchivo + "' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.adjunto) + "');\" style='cursor:pointer;'/>";
               } else {
                   return ".";    
               }
           };
    
           function Check(cellvalue, options, rowObject) {

               if (trim(rowObject.FlagAprobacionLegal) == 0) {
                   var sScript3 = "javascript:fn_AprobarLegal(0," + rowObject.CodigoContratoDocumento + ",'" + rowObject.numeroContrato + "');";
                   return "<input type='checkbox'  value='' onclick=\"" + sScript3 + "\" />";
               } else {
                   sScript3 = "javascript:fn_AprobarLegal(1," + rowObject.CodigoContratoDocumento + ",'" + rowObject.numeroContrato + "');";
                   return "<input type='checkbox'  checked='checked' onclick=\"" + sScript3 + "\" />";
               }
           };
           function Lupa5(cellvalue, options, rowObject) {
               if (rowObject.Flagobservacion == 0) {
                  return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='' style='cursor: pointer;cursor: hand;' />";

               } else {
                   var sScript2 = "javascript:VerObservacionesEjeLeasing(" + rowObject.CodigoContratoDocumento + ",1);";
                   return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
               }
           };
           function RecordCountDocadd(cellvalue, options, rowObject) {
               return $("#hddTotalRegistrosDocAdd").val(rowObject.RecordCount);
           };
           function SumTotalLegalDocadd(cellvalue, options, rowObject) {
               return $("#hddTotalValidadadosDocAdd").val(rowObject.SumFlagAprobacionLegal);
           };

           /*********************************************************
           GRILLA TAB REPRESENTANTES A FIRMAR  MANTENIMIENTO LIMA
           **********************************************************/
            $("#jqGrid_lista_C").jqGrid({
            datatype: function() {
                    fn_ListagrillaRepresentantes();
            },
            jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
                root: "Items",
                page: "CurrentPage",        // Número de página actual.
                total: "PageCount",         // Número total de páginas.
                records: "RecordCount",     // Total de registros a mostrar.
                repeatitems: false,
                id: "CodigoRepresentante"   // Índice de la columna con la clave primaria.
             },
               colNames: ['CodigoRepresentante', 'DNI', 'Representantes a Elegir', 'Ubigeo', '', '', '', ''],
               colModel: [
                            { name: 'CodigoRepresentante', index: 'CodigoRepresentante', hidden: true },
                            { name: 'NroDocumento', index: 'NroDocumento', width: 50, sorttype: "string", align: "center" },
                            { name: 'NombreRepresentante', index: 'NombreRepresentante', width: 150, align: "left" },
                            { name: 'UbigeoNombre', index: 'UbigeoNombre', width: 200, sorttype: "string", align: "left", formatter: fn_Ubigeo },
                            { name: 'check', index: 'check', align: 'center', width: 50, edittype: "checkbox", editoptions: { value: '1:0' }, formatter: "checkbox", formatoptions: { disabled: false }, sortable: false, hidden: true },
                            { name: 'Departamento', index: 'Departamento', hidden: true },
                            { name: 'Provincia', index: 'Provincia', hidden: true },
                            { name: 'Distrito', index: 'Distrito', hidden: true }
                         ],
               height: '100%',
               pager: '#jqGrid_pager_C',
               loadtext: 'Cargando datos...',
               emptyrecords: 'No hay resultados',
               rowNum: 10,                      // Tamaño de la página
               rowList: [10, 20, 30],
               sortname:   'CodigoRepresentante', // Columna a ordenar por defecto.
               sortorder:  'asc',                // Criterio de ordenación por defecto.
               viewrecords: false,              // Muestra la cantidad de registros.
               gridview:    true,
               autowidth:   true,
               altRows:     true,
               loadonce:    false,
               altclass:    'gridAltClass',
               onSelectRow: function(id) {
                   var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id);
                   $("#hddCodigoRepresentante").val(rowData.CodigoRepresentante);
               },
               ondblClickRow: function(id) {
                   var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id);
                   $("#hddCodigoRepresentante").val(rowData.CodigoRepresentante);
                   fn_AgregarRepresentante(rowData.CodigoRepresentante, $("#txtNumeroContrato").val(), C_CODIGO_TIPO_REPRESENTANTE);
               }
               });
               jQuery("#jqGrid_lista_C").jqGrid('navGrid', '#jqGrid_pager_C', { edit: false, add: false, del: false });
               $("#search_jqGrid_lista_C").hide();


           /*******************************
            GRILLA TAB REPRESENTANTES A FIRMAR REPRESNETANTES LIMA SELECIONADOS
           ********************************/
            $("#jqGrid_lista_B").jqGrid({
                datatype: function() {
                fn_ListagrillaRepresentantesContrato();
                fn_ListagrillaRepresentantesContratoTotal();	
            },
                jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
                root: "Items",
                page: "CurrentPage", // Número de página actual.
                total: "PageCount", // Número total de páginas.
                records: "RecordCount", // Total de registros a mostrar.
                repeatitems: false,
                id: "CodigoRepresentante" // Índice de la columna con la clave primaria.
            },
               colNames: ['CodigoRepresentante', 'DNI', 'Firmantes', 'Ubigeo', '', '', '', ''],
               colModel: [
                            { name: 'CodigoRepresentante', index: 'CodigoRepresentante', hidden: true },
                            { name: 'NroDocumento', index: 'NroDocumento', width: 50, sorttype: "string", align: "center" },
                            { name: 'NombreRepresentante', index: 'NombreRepresentante', width: 150, align: "left" },
                            { name: 'UbigeoNombre', index: 'UbigeoNombre', width: 200, sorttype: "string", align: "left", formatter: fn_Ubigeo },
                            { name: 'check', index: 'check', align: 'center', width: 50, edittype: "checkbox", editoptions: { value: '1:0' }, formatter: "checkbox", formatoptions: { disabled: false }, sortable: false, hidden: true },
                            { name: 'Departamento', index: 'Departamento', hidden: true },
                            { name: 'Provincia', index: 'Provincia', hidden: true },
                            { name: 'Distrito', index: 'Distrito', hidden: true }
                         ],
               height: '100%',
               pager: '#jqGrid_pager_B',
               loadtext: 'Cargando datos...',
               emptyrecords: 'No hay resultados',
               rowNum: 10, // Tamaño de la página
               rowList: [10, 20, 30],
               sortname: 'CodigoRepresentante', // Columna a ordenar por defecto.
               sortorder: 'asc', // Criterio de ordenación por defecto.
               viewrecords: false, // Muestra la cantidad de registros.
               gridview:    true,
               autowidth:   true,
               altRows:     true,
               loadonce:    false,
               altclass:    'gridAltClass',
               onSelectRow: function(id) {
               	   var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
                  $("#hddCodigoRepresentanteElimina").val(rowData.CodigoRepresentante);
               },
               ondblClickRow: function(id) {
                   var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
                   $("#hddCodigoRepresentanteElimina").val(rowData.CodigoRepresentante);
                   fn_EliminarRepresentante(rowData.CodigoRepresentante, $("#txtNumeroContrato").val(), C_CODIGO_TIPO_REPRESENTANTE);
               }
               });
               jQuery("#jqGrid_lista_B").jqGrid('navGrid', '#jqGrid_pager_B', { edit: false, add: false, del: false });
               $("#search_jqGrid_lista_B").hide();

            /**********************************************************
            GRILLA TAB  DOCUMENTOS 
            ***********************************************************/
            $("#jqGrid_lista_H").jqGrid({
            datatype: function() {
                fn_ListagrillaDocumentos();
            },
            jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
                root: "Items",
                page: "CurrentPage",            // Número de página actual.
                total: "PageCount",             // Número total de páginas.
                records: "RecordCount",         // Total de registros a mostrar.
                repeatitems: false,
                id: "CodigoContratoDocumento"   // Índice de la columna con la clave primaria.
            },
            colNames: ['Legal', 'CodigoContratoDocumento', 'Documentos', 'Archivo', 'Adjuntar', 'Obs. Comercial', 'Obs. Legal ', 'Eliminar', '', ''],
            colModel: [
                     { name: 'AprobarLegal', index: 'AprobarLegal', align: "center", width: 50, sortable: false, formatter: ConfigCheckBox0 },
                     { name: 'CodigoContratoDocumento', index: 'CodigoContratoDocumento', hidden: true },
	                 { name: 'Descripcion', index: 'Descripcion', width: 400, align: "left", sorttype: "string" },
	                 { name: 'adjunto', index: 'adjunto', width: 80, align: "center", sorttype: "string", formatter: VerAdjunto4 },
	                 { name: 'SubirArchivo', index: 'SubirArchivo', width: 80, align: "center", sortable: false, formatter: SubirArchivo3 },
                     { name: 'lupa3', index: 'lupa3', width: 80, align: "center", sortable: false, formatter: Lupa3 },
	                 { name: 'lupa', index: 'lupa', width: 80, align: "center", sortable: false, formatter: Lupa },
                     { name: 'CodigoOrigenCondicion', index: 'CodigoOrigenCondicion', width: 80, align: "center", sortable: false, formatter: EliminaSeleccionDocumento },
                     { name: 'RecordCount', index: 'RecordCount', hidden: true, formatter: RecordCount },
                     { name: 'SumFlagAprobacionLegal', index: 'SumFlagAprobacionLegal', hidden: true, formatter: SumTotalLegal }
                     ],
            height: '100%',
            pager: '#jqGrid_pager_H',
            loadtext: 'Cargando datos...',
            emptyrecords: 'No hay resultados',
            rowNum: 10,                          // Tamaño de la página
            rowList:     [10, 20, 30],
            sortname:    'CodigoContratoDocumento', // Columna a ordenar por defecto.
            sortorder:   'asc',                    // Criterio de ordenación por defecto.
            viewrecords: true,                   // Muestra la cantidad de registros.
            gridview:    true,
            autowidth:   true,
            altRows:     true,
            loadonce:    false,
            altclass:    'gridAltClass',
            onSelectRow: function(id) {
                var rowData = $("#jqGrid_lista_H").jqGrid('getRowData', id);
                $("#hddCodigo").val(rowData.CodigoContratoDocumento);
            }
            });
            jQuery("#jqGrid_lista_H").jqGrid('navGrid', '#jqGrid_pager_H', { edit: false, add: false, del: false });
            $("#jqGrid_lista_H").setGridWidth($(window).width() - 150);
            $("#search_jqGrid_lista_H").hide();
            /***************************************************
            AGREGA ICONO Y FUNCIONALIDAD A LA GRILLA DOCUMENTOS   
            ****************************************************/
            function ConfigCheckBox0(cellvalue, options, rowObject) {
                 $("#hddTotalAprobacionDocumentos").val(rowObject.SumFlagAprobacionLegal);
                 if (trim(rowObject.FlagAprobacionLegal)==0) {
                     var sScript3 = "javascript:fn_AprobarLegal(0," + rowObject.CodigoContratoDocumento + ",'" + rowObject.numeroContrato + "');";
                     return "<input type='checkbox'  value='' onclick=\"" + sScript3 + "\" />";
                 } else {
                     sScript3 = "javascript:fn_AprobarLegal(1," + rowObject.CodigoContratoDocumento + ",'" + rowObject.numeroContrato + "');";
                     return "<input type='checkbox'  checked='checked' onclick=\"" + sScript3 + "\" />";
                }
             };
            function SubirArchivo3(cellvalue, options, rowObject) {
                    var sScript2 = "javascript:AdjuntarArchivoDocumento(" + rowObject.CodigoContratoDocumento + ");";
                    return "<img src='../Util/images/ico_acc_adjuntarMini.gif' alt='" + cellvalue + "' title='Subir Archivo' width='20px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
             };
            function Lupa(cellvalue, options, rowObject) {
                   if (rowObject.FlagObservacionLegal==0 ) {
                     var sScript2 = "javascript:VerObservacionesLegal(" + rowObject.CodigoContratoDocumento + ",2);";
                     return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
                  } else {

                     sScript2 = "javascript:VerObservacionesLegal(" + rowObject.CodigoContratoDocumento + ",2);";
                     return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
                  }
            };
    
            function Lupa3(cellvalue, options, rowObject) {
                if (rowObject.Flagobservacion == 0) {
                    return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='' style='cursor: pointer;cursor: hand;' />";
                } else {
                    var  sScript2 = "javascript:VerObservacionesEjeLeasing(" + rowObject.CodigoContratoDocumento + ",1);";
                    return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
                }
 
            };
            function EliminaSeleccionDocumento(cellvalue, options, rowObject) {
                    if (fn_util_trim(rowObject.codigoOrigenCondicion) == C_ORIGENDOC_CHECKLIST_LEGAL) {
                        var sScript2 = "javascript:fn_eliminarDocumentos(" + rowObject.CodigoContratoDocumento + ");";
                        return "<img src='../Util/images/ico_acc_eliminar.gif' alt='" + cellvalue + "' title=' Eliminar ' width='20px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
                    } else {
                        return ".";
                    }
            }
            function VerAdjunto4(cellvalue, options, rowObject) {

                if (rowObject.adjunto != "") {
                    var strNombreArchivo = rowObject.adjunto.split('\\').pop();
                    strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);    
                    return "<img src='../Util/images/ico_download.gif' alt='Descargar/Mostrar Archivo' title='" + strNombreArchivo + "' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.adjunto) + "');\" style='cursor:pointer;'/>";
                } else {
                    $("#hddFlagVerificaAdjunto").val("1");
                    return ".";
                }
            }

            function RecordCount(cellvalue, options, rowObject) {
                 return $("#hddTotalRegistros").val(rowObject.RecordCount);
            }

            function SumTotalLegal(cellvalue, options, rowObject) {
                return $("#hddTotalValidadados").val(rowObject.SumFlagAprobacionLegal);
            }
   }

   //****************************************************************
   // Funcion		:: 	fn_AprobarLegal
   // Descripción	::	Registar Observaciones
   // Log			:: 	IJM - 25/02/2012
   //****************************************************************
   function fn_AprobarLegal(intFlagAprobacionLegal, intCodigoContratoDocumento, strNumeroContrato) {

       if (intFlagAprobacionLegal == 1) {
           var vintFlagAprobacionLegal = "0";
       } else {
           vintFlagAprobacionLegal = "1";
       }

       var arrParametros3 = ["pFlagAprobacionLegal", vintFlagAprobacionLegal,
                             "pNumeroContrato",      strNumeroContrato,
                             "pCodigoContratoDocumento", intCodigoContratoDocumento
                             ];
       fn_util_AjaxSyncWM("frmCheckListLegalRegistro.aspx/ActualizaFlagAprobacionLegal",
                         arrParametros3,
                         function() {
                             fn_ListagrillaDocumentos();
                             fn_ListagrillaCondicionAdicional();
                         },
                         function(result) {
                             parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                             result(false);
                         });
   }
  
 //****************************************************************
 // Funcion		:: 	VerObservaciones
 // Descripción	::	Registar Observaciones
 // Log			:: 	JRC - 25/02/2012
 //****************************************************************
 function fn_abreArchivo(pstrRuta) {
       window.open("../frmDownload.aspx?nombreArchivo=" + pstrRuta);
       return false;
   }

 function AdjuntarArchivoDocumento(CodigoContratoDocumento) {
     var strNumeroContrato = $("#txtNumeroContrato").val();
     var sTitulo = "Formlaizacion";
     var sSubTitulo = "Formalizacion::Checklist Legal";
     parent.fn_util_AbreModal(sSubTitulo, "Comun/frmSubirArchivo.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodConDoc=" + CodigoContratoDocumento + "&hddCodContrato=" + strNumeroContrato + "&Add=False", 550, 150, function() { });

 };

 function VerObservacionesLegal(strCodDocContrato, el) {
     var sTitulo = "Formlaizacion";
     var sSubTitulo = "Formalizacion::Checklist Legal";
     parent.fn_util_AbreModal(sSubTitulo, "Formalizacion/frmObservacion.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodConDoc=" + strCodDocContrato + "&hddCodContrato=" + $("#txtNumeroContrato").val() + "&sflagtipoobs=" + el + "&Add=False", 550, 200, function() { });
 }

 function VerObservacionesEjeLeasing(strCodDocContrato, el) {
     var sTitulo = "Formlaizacion";
     var sSubTitulo = "Formalizacion::Checklist Legal";
     parent.fn_util_AbreModal(sSubTitulo, "Comun/frmCheckListObservacion.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodConDoc=" + strCodDocContrato + "&hddCodContrato=" + $("#txtNumeroContrato").val() + "&sflagtipoobs=" + el + "&Add=false", 650, 300, function() { });

 }

 //****************************************************************
 // Función		:: 	fn_AgregarRepresentantes
 // Descripción	::	Muestra la ventana modal de representantes del cliente disponibles, permitiendo seleccionar al usuario cuales agregar al contrato.
 //                  La ventana permite realizar el mantenimiento de los representantes.
 // Log			:: 	ijm - 25/02/2012
 //****************************************************************
 function fn_AgregarRepresentantes() {
     var sTitulo = "Verificación";
     var sSubTitulo = "Verificación :: Checklist Comercial ";
     
     var sFirmaen = fn_util_trim($("#cmbcontratofirma").val());
     
     var strDepartamento = $("#cmbDepartamento").val();
     if (strDepartamento == 0 || strDepartamento == null) {
         strDepartamento = "00";
     }

     var strProvincia = $("#cmbProvincia").val();
     if (strProvincia == 0 || strProvincia == null) {
         strProvincia = "00";
     }

     var strDistrito = $("#cmbDistrito").val();
     if (strDistrito == 0 || strDistrito == null) {
         strDistrito = "00";
     }
     var strubigeo = (strDepartamento + '' + strProvincia + '' + strDistrito);
     parent.fn_util_AbreModal(sSubTitulo, "Comun/frmRepresentanteRegistroIbk.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hFirmaen=" + sFirmaen + "&hubigeo=" + strubigeo, 650, 250, function() { });
 }

function fn_validaCuenta() {
     //  CUENTA Nº 1
     if ($("#cmbTipoCuenta1").val() == "01") {
         var strargFCDTIPOCUENTA = "IM";
     } else {
         strargFCDTIPOCUENTA = "ST";
     }
     if ($("#cmbMoneda1").val() == "001") {
         var strargFCDCODMONEDA = "001";
     } else {
         strargFCDCODMONEDA = "010";
     }
     if ($("#cmbTipoCuenta1").val() == "01") {
         var strCoCategoria = "001";
     } else {
         strCoCategoria = "002";
     }
     var NumeroCuenta = fn_util_trim($("#txtNumeroCuenta1").val());

     //  CUENTA Nº 2
     if ($("#cmbTipoCuenta2").val() == "01") {
         var strargFCDTIPOCUENTA2 = "IM";
     } else {
         strargFCDTIPOCUENTA2 = "ST";
     }

     if ($("#cmbMoneda2").val() == "001") {
         var strargFCDCODMONEDA2 = "001";
     } else {
         strargFCDCODMONEDA2 = "010";
     }

     if ($("#cmbTipoCuenta2").val() == "01") {
         var strCoCategoria2 = "001";
     } else {
         strCoCategoria2 = "002";
     }


     var NumeroCuenta2 = fn_util_trim($("#txtNumeroCuenta2").val());
     var validacta2;
    
     if ((NumeroCuenta.length) == 13) {
     	var srtMonedaContrato = $("#txtMonedaContrato").val();

     	// RF1_1 - AEP - 03/09/2012	
     	// Motivo :: Se quitó la validación "La moneda de la primera cuenta no es igual al del contrato".

//        if (srtMonedaContrato == $("#cmbMoneda2").val() || srtMonedaContrato == $("#cmbMoneda1").val()) {
     	validacta2 = "true";
     	if ($("#cmbTipoCuenta2").val() != 0 || $("#cmbMoneda2").val() != 0) {
     		if ((NumeroCuenta2.length) == 13) {
     			validacta2 = "true";
     			if ((NumeroCuenta2) == (NumeroCuenta)) {
     				validacta2 = "false";
     				$("#hddValidaCuenta").val("1" + "|" + "Los números de cuenta no pueden ser iguales");
     			} else {
     				validacta2 = "true";
     			}
     		} else {
     			validacta2 = "false";
     			$("#hddValidaCuenta").val("1" + "|" + "La segunda cuenta debe tener 13 dígitos");
     		}
     	} else {
     		validacta2 = "true";
     	}
     } else {
     	    validacta2 = "false"; 
            $("#hddValidaCuenta").val("1" + "|" + "La primera cuenta debe tener 13 digitos");
     }
            

                    if (validacta2=="true"){
                           var arrParametros3 = ["argFCDTIPOCUENTA",    strargFCDTIPOCUENTA,
                                                 "argFCDCODMONEDA",     strargFCDCODMONEDA,
                                                 "argFCDCODTIENDA",     NumeroCuenta.substr(0, 3),
                                                 "argFCDCODCATEGORIA",  strCoCategoria,
                                                 "argFCDNUMCUENTA",     NumeroCuenta.substr(3, 12),
                                                 //CUENTA 2
                                                 "argFCDTIPOCUENTA2",   strargFCDTIPOCUENTA2,
                                                 "argFCDCODMONEDA2",    strargFCDCODMONEDA2,
                                                 "argFCDCODTIENDA2",    NumeroCuenta2.substr(0, 3),
                                                 "argFCDCODCATEGORIA2", strCoCategoria2,
                                                 "argFCDNUMCUENTA2",    NumeroCuenta2.substr(3, 12),
                           	                     "pCodUnico", $("#txtCuCliente").val()
                                                ];
                         fn_util_AjaxSyncWM("frmCheckListLegalRegistro.aspx/ValidaCuentaST",
                         arrParametros3,
                         function(result) {
                                $("#hddValidaCuenta").val(result);
                         },
                         function(result) {
                             $("#hddValidaCuenta").val("1|" + "ERROR " + result.status + ' ' + result.statusText);
                             parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                             result(false);
                         });
           
    } else {
     	return; 
    }

 }


//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
    
    //INICIA OCULTANDO REPRESENTANTES DE PROVINCIA
    document.getElementById("trMonedaContrato").style.display = 'none';

    //Inicio IBK - AAE - 11/02/2013 - Inicializo nuevos campos
    $('#txtFechaMaxActivacion').validText({ type: 'date', length: 10 });
    $('#txtFechaDisponibilidad').validText({ type: 'date', length: 10 });
    $("#txtOpcionCompraMonto").validNumber({ value: '' });
    $("#txtComisionActivacionMonto").validNumber({ value: '' });
    $("#txtComisionEstructuracionMonto").validNumber({ value: '' });

    $("#hidOpcionCompra").validNumber({ value: '' });
    $("#hidComActivacion").validNumber({ value: '' });
    $("#hidComestructuracion").validNumber({ value: '' });

    $("#txtOpcionCompraMonto").val(fn_util_AddCommas(fn_util_RedondearDecimales($("#txtOpcionCompraMonto").val(), 2)));
    $("#txtComisionActivacionMonto").val(fn_util_AddCommas(fn_util_RedondearDecimales($("#txtComisionActivacionMonto").val(), 2)));
    $("#txtComisionEstructuracionMonto").val(fn_util_AddCommas(fn_util_RedondearDecimales($("#txtComisionEstructuracionMonto").val(), 2)));

    $("#hidOpcionCompra").val(fn_util_AddCommas(fn_util_RedondearDecimales($("#hidOpcionCompra").val(), 2)));
    $("#hidComActivacion").val(fn_util_AddCommas(fn_util_RedondearDecimales($("#hidComActivacion").val(), 2)));
    $("#hidComestructuracion").val(fn_util_AddCommas(fn_util_RedondearDecimales($("#hidComestructuracion").val(), 2)));

    //Fin IBK
    
    if ($("#cmbcontratofirma").val() == '001') {
        document.getElementById("dv_representanteslima").style.visibility = 'visible';
        document.getElementById("dv_representantesprovincia").style.visibility = 'hidden';
        document.getElementById("dv_representanteslima").style.display = 'block';
        document.getElementById("dv_representantesprovincia").style.display = 'none';
        document.getElementById("td_lblDepartamento").style.display = 'none';
        document.getElementById("td_InpDepartamento").style.display = 'none';
        document.getElementById("tr_ProvDist").style.display = 'none';
    } else {
    	document.getElementById("dv_representantesprovincia").style.visibility = 'hidden';
    	document.getElementById("dv_representantesprovincia").style.display = 'none';
        document.getElementById("td_lblDepartamento").style.display = 'block';
        document.getElementById("td_InpDepartamento").style.display = 'block';
        document.getElementById("tr_ProvDist").style.display = 'block';
    }

    $('#txtUso').validText({ type: 'comment', length: 100 });
    $('#txtUbicacion').validText({ type: 'comment', length: 100 });
    $("#txtUbicacion").MarcaAgua("<Ingresar dirección completa>");


    var strUbigeoUbicacion = $("#hddUbigeoUbicacion").val();
    var strDepartamento = strUbigeoUbicacion.substring(0, 2);
    var strProvincia = strUbigeoUbicacion.substring(2, 4);
    var strDistrito = strUbigeoUbicacion.substring(4, 6);

    $("#cmbDepartamentoUbicacion").val(strDepartamento);
    fn_cargaComboProvinciaUbicacion(strDepartamento);
    $("#cmbProvinciaUbicacion").val(strProvincia);
    fn_cargaComboDistritoUbicacion(strDepartamento, strProvincia);
    $("#cmbDistritoUbicacion").val(strDistrito);
    
    

    //TAB DATOS DEL BIEN
    $('#txtubicacion').validText({ type: 'alphanumeric', length: 100 });
    $('#txtuso').validText({ type: 'alphanumeric', length: 100 });
    //TAB CONDIONES ADICIONALES
    $('#txtaObservaciones').validText({ type: 'alphanumeric', length: 100 });
    //TAB CUENTAS DEL CLIENTE
    $('#txtNumeroCuenta1').validText({ type: 'number', length: 13 });
    $('#txtNumeroCuenta2').validText({ type: 'number', length: 13 });
    //TAB DOCUMENTOS
    $('#txtNombreDocumento').validText({ type: 'alphanumeric', length: 100 });
    fn_seteaCamposObligatorios();

}


//****************************************************************
// Funcion		:: 	fn_cargaComboProvinciaUbicacion
// Descripción	::	
// Log			:: 	IJM - 04/09/2012
//****************************************************************
function fn_cargaComboProvinciaUbicacion(valor) {

    var arrParametros = ["pstrDepartamento", valor, "pstrOp", "2"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbProvinciaUbicacion').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    fn_LimpiaComboDistritoUbicacion();
}

//****************************************************************
// Funcion		:: 	fn_cargaComboDistrito
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_cargaComboDistritoUbicacion(strCodigoDepartamento, strCodigoProvincia) {

    var arrParametros = ["pstrProvincia", strCodigoProvincia, "pstrDepartamento", strCodigoDepartamento, "pstrOp", "3"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#cmbDistritoUbicacion').html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            fn_mdl_alert(strError.toString(), function() { });
        }
    }
    //fn_ListagrillaRepresentantes();

}


//****************************************************************
// Funcion		:: 	fn_LimpiaComboDistrito
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_LimpiaComboDistritoUbicacion() {
    $('#cmbDistritoUbicacion').empty();
    $('#cmbDistritoUbicacion').html('<option value="0">- Seleccionar -</option>');
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_seteaCamposObligatorios() {
    fn_util_SeteaObligatorio($("#cmbCombo"), "select");
    fn_util_SeteaObligatorio($("#txtInput"), "input");
    fn_util_SeteaObligatorio($("#txtFecha"), "calendar");

    fn_util_SeteaObligatorio($("#cmbDepartamentoUbicacion"), "select");
    fn_util_SeteaObligatorio($("#cmbProvinciaUbicacion"), "select");
    fn_util_SeteaObligatorio($("#cmbDistritoUbicacion"), "select");

    //Inicio IBK - AAE - 11/02/2013 -Seteo obigatorios las fechas
    fn_util_SeteaObligatorio($("#txtFechaMaxActivacion"), "input");
    fn_util_SeteaObligatorio($("#txtFechaDisponibilidad"), "input");
    //Fin IBK
}

//****************************************************************
// Funcion		:: 	fn_oculta_controles
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_oculta_controles(value) {
    if (value == 2) {
        document.getElementById("dvTipoDeInmueble").style.visibility = 'visible';
        document.getElementById("dvSelectTipoDeInmueble").style.visibility = 'visible';
    } else {
        document.getElementById("dvTipoDeInmueble").style.visibility = 'hidden';
        document.getElementById("dvSelectTipoDeInmueble").style.visibility = 'hidden';
    }
}

//****************************************************************
// Funcion		:: 	trim
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function trim(stringToTrim) {
    return stringToTrim.replace(/^\s+|\s+$/g, "");
}

//****************************************************************
// Funcion		:: 	SubirArchivo
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_SubirArchivo() {
    var sTitulo = "Formlaizacion";
    var sSubTitulo = "Formalizacion::Checklist Legal";
    parent.fn_util_AbreModal(sSubTitulo, "Comun/frmCheckListSubirArchivo.aspx?Titulo=Subir Archivo&sSubTitulo=" + sSubTitulo, 650, 200, function() { });
}

//****************************************************************
// Funcion		:: 	VerObservaciones
// Descripción	::	
// Log			:: 	IJM - 10/02/2012
//****************************************************************
function VerObservaciones(strCodDocContrato, el) {
    var sTitulo = "Formlaizacion";
    var sSubTitulo = "Formalizacion::Checklist Legal";
    parent.fn_util_AbreModal(sSubTitulo, "Comun/frmCheckListObservacion.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodConDoc=" + strCodDocContrato + "&hddCodContrato=" + $("#txtNumeroContrato").val() + "&sflagtipoobs=" + el + "&Add=true", 550, 200, function() { });
}

//****************************************************************
// Funcion		:: 	GeneraUbigeoUbicacion
// Descripción	::	
// Log			:: 	IJM - 04/09/2012
//****************************************************************
function GeneraUbigeoUbicacion() {

    var strDepartamentoubi = $("#cmbDepartamentoUbicacion").val();
    if (strDepartamentoubi == 0 || strDepartamentoubi == null) {
        strDepartamentoubi = "00";
    }

    var strProvinciaUbi = $("#cmbProvinciaUbicacion").val();
    if (strProvinciaUbi == 0 || strProvinciaUbi == null) {
        strProvinciaUbi = "00";
    }

    var strDistritoUbi = $("#cmbDistritoUbicacion").val();
    if (strDistritoUbi == 0 || strDistritoUbi == null) {
        strDistritoUbi = "00";
    }

    var strubigeoUbicacion = (strDepartamentoubi + '' + strProvinciaUbi + '' + strDistritoUbi);

    return strubigeoUbicacion;
}


//****************************************************************
// Funcion		:: 	fn_aprobar
// Descripción	::	
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_aprobar() {

    var strError = new StringBuilderEx();

    var objcmbcontratofirma = $('select[id=cmbcontratofirma]');
    var objcmbMoneda1 =       $('select[id=cmbMoneda1]');
    var objtxtNumeroCuenta1 = $('input[id=txtNumeroCuenta1]:text');
    var objcmbTipoCuenta1 =   $('select[id=cmbTipoCuenta1]');
    var objtxtUso =           $('input[id=txtUso]:text');
    var objcmbMoneda2 =       $('select[id=cmbMoneda2]');
    var objtxtNumeroCuenta2 = $('input[id=txtNumeroCuenta2]:text');
    var objcmbTipoCuenta2 =   $('select[id=cmbTipoCuenta2]');

    var objcmbDepartamentoUbicacion = $('select[id=cmbDepartamentoUbicacion]');
    var objcmbProvinciaUbicacion = $('select[id=cmbProvinciaUbicacion]');
    var objcmbDistritoUbicacion = $('select[id=cmbDistritoUbicacion]');
    /* AAE - 11/09/2012 - Obtengo el perfil*/
    var strPerfil = $("#hddPerfil").val();
    var strEsVacio = '1' //0 si es vacio, 1 si no
    /* FIN AAE */

    //Inicio IBK - AAE - Controlo que tenga una comisión de activación válida
    var objFechaActivacion = $('input[id=txtFechaMaxActivacion]:text');
    var objFechaDisponibilidad = $('input[id=txtFechaDisponibilidad]:text');

    var decMontoOriginal = fn_util_ValidaDecimal($("#hidComActivacion").val())
    var decMontoNuevo = fn_util_ValidaDecimal($("#txtComisionActivacionMonto").val())
    if (decMontoOriginal >= 500) {
        decMontoOriginal = 500;
    };
    if (decMontoNuevo < decMontoOriginal) {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert("El monto de la comisión de activación no puede ser menor a " + decMontoOriginal.toString(), function() { });
        strError = null;
        return;
    };

    strError.append(fn_util_ValidateControl(objFechaActivacion[0], 'Fecha de Activación', 1, ''));
    strError.append(fn_util_ValidateControl(objFechaDisponibilidad[0], 'Fecha de disponibilidad', 1, ''));
    //Fin IBK
	
	if($("#txtUbicacion").val()=="<Ingresar dirección completa>") {
		parent.fn_mdl_mensajeIco("Ingrese una ubicación", "util/images/warning.gif", "CHECKLIST LEGAL");
		 $("div#divTabs").tabs("select", [0]);
		return;
	}
	
    strError.append(fn_util_ValidateControl(objcmbcontratofirma[0], 'contrato Firmarse En', 1, ''));
    if (strError.toString() != '') {
    	$("div#divTabs").tabs("select", [2]); 
    	//alert(strError.toString());
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
    	return; 
    }

    strError.append(fn_util_ValidateControl(objtxtUso[0], 'uso', 1, ''));

    strError.append(fn_util_ValidateControl(objcmbDepartamentoUbicacion[0], 'Departamento', 1, ''));
    strError.append(fn_util_ValidateControl(objcmbProvinciaUbicacion[0], 'Ubicacion', 1, ''));
    strError.append(fn_util_ValidateControl(objcmbDistritoUbicacion[0], 'Distrito', 1, ''));  
    
    if (strError.toString() != '') {
    	$("div#divTabs").tabs("select", [0]); 
    	//alert(strError.toString());
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
        strError = null;
        fn_seteaCamposObligatorios();
    	return; 
    }
	/*AAE - 11/09/2012 - Si el perfil es 1- Administrador, 6 - Supervisor Leasing, 8 - SUpervisor Legar no controlo cuenta */
    if ($("#cmbMoneda1").val() == 0 && $("#cmbTipoCuenta1").val() == 0 && fn_util_trim($("#txtNumeroCuenta1").val()) == "") {
        strEsVacio = '0';
    }
    if ((strPerfil != '1' && strPerfil != '6' && strPerfil != '8') || strEsVacio != '0') {
        strError.append(fn_util_ValidateControl(objcmbMoneda1[0], 'la moneda', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtNumeroCuenta1[0], 'el número de Cuenta', 1, ''));
        strError.append(fn_util_ValidateControl(objcmbTipoCuenta1[0], 'el tipo de Cuenta', 1, ''));
    }
    /*Fin AAE*/
    
    if ($("#cmbMoneda2").val() != 0 || $("#cmbTipoCuenta2").val() != 0 || fn_util_trim($("#txtNumeroCuenta2").val()) !="") {
        strError.append(fn_util_ValidateControl(objcmbMoneda2[0], 'la moneda', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtNumeroCuenta2[0], 'el número de cuenta', 1, ''));
        strError.append(fn_util_ValidateControl(objcmbTipoCuenta2[0], 'el tipo de cuenta', 1, ''));
    }

    if (strError.toString() != '') {
        parent.fn_unBlockUI();
        parent.fn_mdl_alert(strError.toString(), function() { });
    	$("div#divTabs").tabs("select", [3]);
        strError = null;
    } else {

        if ($("#hddTotalValidadadosDocAdd").val() == $("#hddTotalRegistrosDocAdd").val()) { // VALIDA CHEK MARCADOS EN CONDICIONES ADICIONALES 
            if ($("#hddTotalValidadados").val() == $("#hddTotalRegistros").val()) {         // VALIDA CHEK MARCADOS EN DOCUMENTOS
                 if ($("#jqGrid_lista_H").getGridParam("reccount") > 0) {                   // VALIDA QUE SE HAYA AGREGADO UN DOCUMENTO
                   // if ($("#jqGrid_lista_B").getGridParam("reccount") == 2) {               // VALIDA QUE SE HAYA AGREGADO UN REPRESENTANTE
                   if (intTotalRegistros == 2) {
                     /*AAE - 11/09/2012 - Si el perfil es 1- Administrador, 6 - Supervisor Leasing, 8 - SUpervisor Legar no controlo cuenta */
                     if ((strPerfil != '1' && strPerfil != '6' && strPerfil != '8') || strEsVacio != '0') {	
                        fn_validaCuenta();
                    	
                         //RF1_1 - AEP - 03/09/2012	
                        //Motivo :: Se mejoró la validacion de cuentas, si el resultado es erroneo mostrará el mensaje establecido desde el método fn_validaCuenta().

                            fn_validaCuenta();
                         	var resultado = $("#hddValidaCuenta").val();
                            var result = resultado.split('|');
                           
                            if (result[0] != "0") {
                            parent.fn_mdl_mensajeIco(result[1], "util/images/warning.gif", "ERROR AL ENVIAR");
                            $("div#divTabs").tabs("select", [3]);
                            parent.fn_unBlockUI();	
                            	return; 
                            }
                     }
                     /* FIN AAE */
                                var sMensaje = "¿ Desea Aprobar el Contrato ?";
                                var sTitulo = "Aprobar Check List Legal";
                                parent.fn_mdl_confirma(
                                sMensaje, //Mensaje - Obligatorio
                                function() {
                                    //Guarda                
                                    var arrParametros = ["strCodigo",           $("#txtNumeroContrato").val(),
                                                         "strUso",              $("#txtUso").val(),
                                                         "strUbicacion",        $("#txtUbicacion").val(),
                                                         "strNumeroCuenta1",    $("#txtNumeroCuenta1").val(),
                                                         "strCodigoTipoCuenta1",$("#cmbTipoCuenta1").val(),
                                                         "strCodigoMoneda1",    $("#cmbMoneda1").val(),
                                                         "strNumeroCuenta2",    $("#txtNumeroCuenta2").val(),
                                                         "strCodigoTipoCuenta2",$("#cmbTipoCuenta2").val(),
                                                         "strCodigoMoneda2",    $("#cmbMoneda2").val(),
                                                         "strcontratofirma",    $("#cmbcontratofirma").val(),
                                                         "strUbigeofirma",      "",
                                                         "strUbigeoUbicacionBien", GeneraUbigeoUbicacion(),
                                                         "strRazonSocial", $("#txtrazonSocial").val(),
                                                         "strDireccionCliente", $("#txtDirCliente").val(),
                                                         "strNroLinea", $("#cmbLinea").val(),
                                                         "strFechaActivacion", $("#txtFechaMaxActivacion").val(),
                                                         "strFechaDisponibilidad", $("#txtFechaDisponibilidad").val(),
                                                         "strOpcionCompra", $("#txtOpcionCompraMonto").val(),
                                                         "strComActivacion", $("#txtComisionActivacionMonto").val(),
                                                         "strComEstructuracion", $("#txtComisionEstructuracionMonto").val(),
                                                         "hidOpcionCompra", $("#hidOpcionCompra").val(),
                                                         "hidComActivacion", $("#hidComActivacion").val(),
                                                         "hidComEstructuracion", $("#hidComestructuracion").val()  
                                                                 ];
                                 
                                    fn_util_AjaxSyncWM("frmCheckListLegalRegistro.aspx/ActualizarSolicitudCredito",
                                                                arrParametros,
                                                                function() { },
                                                                function(result) {
                                                                    parent.fn_unBlockUI();
                                                                    parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                                                                }
                                                               );
                                    // Actualiza Estado 
                                    parent.fn_blockUI();
                                    arrParametros = ["strCodSolicitudCredito", fn_util_trim($("#txtNumeroContrato").val()),
                                                             "intFlagEnvioLegal", c_intFlagEnvioLegal
                                                            ];
                                    fn_util_AjaxWM("frmCheckListLegalRegistro.aspx/ActualizarGestionComercialAprobar",
                                            arrParametros,
                                            function() {
                                                parent.fn_unBlockUI();
                                                window.fn_util_redirect('frmCheckListLegalListado.aspx');
                                            },
                                            function(result) {
                                                parent.fn_unBlockUI();
                                                parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                                            });

                                }, // ACCION SI - Obligatorio
                                "Util/images/question.gif", // Imagen - puede ser nulo
                                function() { }, // ACCION SI - Obligatorio
                                sTitulo
                            );
                          
            
                    } else {
                        parent.fn_mdl_mensajeIco("Solo debe ingresar 2 representantes a firmar", "util/images/warning.gif", "ERROR AL ENVIAR");
                        $("div#divTabs").tabs("select", [2]);
                    }
                } else {
                    parent.fn_mdl_mensajeIco("No se puede enviar, faltan documentos ", "util/images/warning.gif", "ERROR AL ENVIAR");
                    $("div#divTabs").tabs("select", [4]);
                }
                
         } else {
             parent.fn_mdl_mensajeIco("No se puede Aprobar. Falta seleccionar documentos", "util/images/warning.gif", "ERROR AL ENVIAR");
             $("div#divTabs").tabs("select", [4]);
         }
      } else {
          parent.fn_mdl_mensajeIco("No se puede Aprobar. Falta seleccionar condiciones adicionales", "util/images/warning.gif", "ERROR AL ENVIAR");
          $("div#divTabs").tabs("select", [1]);
      }    
            
    }
}

//****************************************************************
// Funcion		:: 	fn_grabar
// Descripción	::	
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_grabar() {

    var strError = new StringBuilderEx();
    
    var objcmbcontratofirma = $('select[id=cmbcontratofirma]');
    var objcmbMoneda1 = $('select[id=cmbMoneda1]');
    var objtxtNumeroCuenta1 = $('input[id=txtNumeroCuenta1]:text');
    var objcmbTipoCuenta1 = $('select[id=cmbTipoCuenta1]');

    var objtxtUbicacion = $('input[id=txtUbicacion]:text');
    var objtxtUso = $('input[id=txtUso]:text');
    
	if($("#txtUbicacion").val()=="<Ingresar dirección completa>") {
		parent.fn_mdl_mensajeIco("Ingrese una ubicación", "util/images/warning.gif", "CHECKLIST LEGAL");
		$("div#divTabs").tabs("select", [0]);
		return;
	}
	
        strError.append(fn_util_ValidateControl(objcmbcontratofirma[0], 'Seleccione contrato Firmarse En', 1, ''));        
        strError.append(fn_util_ValidateControl(objcmbMoneda1[0], 'Seleccione Moneda', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtNumeroCuenta1[0], 'Ingrese Numero de Cuenta', 1, ''));
        strError.append(fn_util_ValidateControl(objcmbTipoCuenta1[0], 'Seleccione Tipo de Cuenta', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtUbicacion[0], 'Ingrese Ubicacion', 1, ''));
        strError.append(fn_util_ValidateControl(objtxtUso[0], 'Ingrese Uso', 1, ''));

        //Inicio IBK - AAE - Controlo monto de comisión de activación
        var decMontoOriginal = fn_util_ValidaDecimal($("#hidComActivacion").val())
        var decMontoNuevo = fn_util_ValidaDecimal($("#txtComisionActivacionMonto").val())
        if (decMontoOriginal >= 500) {
            decMontoOriginal = 500;
        };
        if (decMontoNuevo < decMontoOriginal) {
            parent.fn_unBlockUI();
            parent.fn_mdl_alert("El monto de la comisión de activación no puede ser menor a " + decMontoOriginal.toString(), function() { });
            return;
        };
        
        if (strError.toString() != '') {
            parent.fn_unBlockUI();
            parent.fn_mdl_alert(strError.toString(), function() { });
        	
            strError = null;
        } else {
            parent.fn_blockUI();
            var arrParametros = ["strCodigo",            $("#txtNumeroContrato").val(),
                                 "strUso",               $("#txtUso").val(),
                                 "strUbicacion",         $("#txtUbicacion").val(),
                                 "strNumeroCuenta1",     $("#txtNumeroCuenta1").val(),
                                 "strCodigoTipoCuenta1", $("#cmbTipoCuenta1").val(),
                                 "strCodigoMoneda1",     $("#cmbMoneda1").val(),
                                 "strNumeroCuenta2",     $("#txtNumeroCuenta2").val(),
                                 "strCodigoTipoCuenta2", $("#cmbTipoCuenta2").val(),
                                 "strCodigoMoneda2",     $("#cmbMoneda2").val(),
                                 "strcontratofirma",     $("#cmbcontratofirma").val(),
                                 "strUbigeofirma",          "",
                                 "strUbigeoUbicacionBien", GeneraUbigeoUbicacion(),
                                 "strRazonSocial", $("#txtrazonSocial").val(),
                                 "strDireccionCliente", $("#txtDirCliente").val(),
                                 "strNroLinea", $("#cmbLinea").val(),
                                 "strFechaActivacion", $("#txtFechaMaxActivacion").val(),
                                 "strFechaDisponibilidad", $("#txtFechaDisponibilidad").val(),
                                 "strOpcionCompra", $("#txtOpcionCompraMonto").val(),
                                 "strComActivacion", $("#txtComisionActivacionMonto").val(),
                                 "strComEstructuracion", $("#txtComisionEstructuracionMonto").val(),
                                 "hidOpcionCompra", $("#hidOpcionCompra").val(),
                                 "hidComActivacion", $("#hidComActivacion").val(),
                                 "hidComEstructuracion", $("#hidComestructuracion").val()                                   
            ];
            fn_util_AjaxSyncWM("frmCheckListLegalRegistro.aspx/ActualizarSolicitudCredito",
                            arrParametros,
                            fn_MensajeYRedireccionar,
                            function(result) {
                                parent.fn_unBlockUI();
                                parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                            }
                        );
        }
    }

//****************************************************************
// Funcion		:: 	fn_Cancelar
// Descripción	::	Regresa a la busqueda.
// Log			:: 	AEP - 16/07/2012
//****************************************************************
function fn_Cancelar() {
    
    parent.fn_mdl_confirma('¿Está seguro de volver?',
            function() {
                fn_util_redirect('frmCheckListLegalListado.aspx');
            },
             "Util/images/question.gif",
            function() { },
            'Checklist - Legal'
         );
}

function fn_Cancelar2() {

    fn_util_redirect('frmCheckListLegalListado.aspx');
      
}
//****************************************************************
// Funcion		:: 	fn_grabar
// Descripción	::	
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_devolver() {

    var strCodigoContrato = $("#txtNumeroContrato").val();
    var strhddCodigoEstadoContrato = $("#hddCodigoEstadoContrato").val();
    parent.fn_util_AbreModal("Checklist Legal :: Seguimiento", "comun/frmContratoSeguimiento.aspx?hddCodigoContrato=" + strCodigoContrato + "&hddCodigoEstadoContrato=" + strhddCodigoEstadoContrato, 500, 300, function() { });
 
}

//****************************************************************
// Funcion		:: 	fn_Representantes
// Descripción	::	Abre Mnt. Reprensentante
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_Representantes(sFlag) {
    var sTitulo = "Verificacion";
    var sSubTitulo = "Resgistro de Representantes";
    parent.fn_util_AbreModal(sSubTitulo, "Comun/FrmRepresentanteRegistroProv.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&sflagtipoobs=" + sFlag + "&Add=False", 750, 500, function() { });
}

//****************************************************************
// Funcion		:: 	fn_ubicacionfirmar
// Descripción	::	VALIDA SI EL CONTRATO SE FIRMARA EN LIMA O PROVINCIA
// Log			:: 	JRC - 25/02/2012 
//****************************************************************
function fn_ubicacionfirmar(value) {

    if (value == '001') {
        document.getElementById("dv_representanteslima").style.visibility = 'visible';
        document.getElementById("dv_representantesprovincia").style.visibility = 'hidden';
        document.getElementById("dv_representanteslima").style.display = 'block';
        document.getElementById("dv_representantesprovincia").style.display = 'none';
        document.getElementById("td_lblDepartamento").style.display = 'none';
        document.getElementById("td_InpDepartamento").style.display = 'none';
        document.getElementById("tr_ProvDist").style.display = 'none';
        document.getElementById("tragregarrepresentante").style.display = 'block';

    } else if (value == '002') {

        document.getElementById("td_lblDepartamento").style.display = 'block';
        document.getElementById("td_InpDepartamento").style.display = 'block';
        document.getElementById("tr_ProvDist").style.display = 'block';

        document.getElementById("tragregarrepresentante").style.display = 'block';
        
        //IBK - RPH
        //$("#cmbDepartamento option[value='15']").remove();

    } else if (value == '0') {
        document.getElementById("td_lblDepartamento").style.display = 'none';
        document.getElementById("td_InpDepartamento").style.display = 'none';
        document.getElementById("tr_ProvDist").style.display = 'none';
        document.getElementById("tragregarrepresentante").style.display = 'none';

    }
    
    fn_EliminarTodosRepresentante();
    fn_ListagrillaRepresentantes();
    fn_ListagrillaRepresentantesContrato();

}

//****************************************************************
// Funcion		:: 	fn_EliminarTodosRepresentante
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_EliminarTodosRepresentante() {
    var arrParametros = ["pNumeroContrato", $("#txtNumeroContrato").val(),
                         "pCodigoTipoRepresentante", C_CODIGO_TIPO_REPRESENTANTE   //Codigo Tipo Representante 001 Interbak 002 Cliente
                        ];
    fn_util_AjaxSyncWM("frmCheckListLegalRegistro.aspx/EliminaRepresentanteContrato",
            arrParametros,
            function() {
                fn_ListagrillaRepresentantes();
                fn_ListagrillaRepresentantesContrato();
            },
            function(request) {
                fn_util_alert(jQuery.parseJSON(request.responseText).Message);
            }
        );
}

//****************************************************************
// Funcion		:: 	fn_AgregarCondicionesAdicionales
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
    
function fn_AgregarCondicionesAdicionales() {
    var strCodDocContrato = $("#hddCodigo").val();
    var strNumeroContrato = $("#txtNumeroContrato").val();
    var strOrigenCondicion = $("#hddconsOrigenCondicion").val();

    var sTitulo = "Verificación";
    var sSubTitulo = "Verificación :: Checklist Legal ";
    var el = "1";
    parent.fn_util_AbreModal(sSubTitulo, "Comun/frmCheckListSubirArchivo.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodConDoc=" + strCodDocContrato + "&hddCodContrato=" + strNumeroContrato + "&hdOrigenCondicion=" + strOrigenCondicion + "&sflaCondAdicional=" + el + "&Add=False", 550, 200, function() { });

}

//****************************************************************
// Funcion		:: 	fn_AgregarDocumentos
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_AgregarDocumentos() {

    var strCodDocContrato = $("#hddCodigo").val();
    var strNumeroContrato = $("#txtNumeroContrato").val();
    var strOrigenCondicion = $("#hddconsOrigenCondicion").val();

    var sTitulo = "Verificación";
    var sSubTitulo = "Verificación :: Checklist Legal";
    var el = "2";
    
    parent.fn_util_AbreModal(sSubTitulo, "Comun/frmCheckListSubirArchivo.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodConDoc=" + strCodDocContrato + "&hddCodContrato=" + strNumeroContrato + "&hdOrigenCondicion=" + strOrigenCondicion + "&sflaCondAdicional=" + el + "&Add=False", 550, 200, function() { });

}

//****************************************************************
// Funcion		:: 	fn_eliminarDocumentos
// Descripción	::	Inicializa campos
// Log			:: 	IJM - 10/02/2012
//****************************************************************
function fn_eliminarDocumentos(codContratoDocumento) {
   
    var sMensaje = "¿Confirma que desea eliminar el documento?";
    var sTitulo = "Eliminar Documento";

    parent.fn_mdl_confirma(sMensaje, //Mensaje - Obligatorio
                           function() {
                                parent.fn_blockUI();
                                var arrParametros = ["intContratoDocumento", codContratoDocumento,        
                                                     "strnumeroContrato", $("#txtNumeroContrato").val()
                                ];
                                fn_util_AjaxWM("frmCheckListLegalRegistro.aspx/EliminaContratoDocumento",
                                                arrParametros,
                                                function() {
                                                    fn_ListagrillaDocumentos();
                                                    fn_doResize();
                                                },
                                                function(result) {
                                                    parent.fn_unBlockUI();
                                                    parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                                                }
                            );
                            }, // ACCION SI - Obligatorio
                             "Util/images/question.gif", // Imagen - puede ser nulo
                            function() { }, // ACCION SI - Obligatorio
                            sTitulo
                          );
}

//****************************************************************
// Funcion		:: 	fn_EliminarCondicionAdicional
// Descripción	::	
// Log			:: 	IJM - 10/02/2012
//****************************************************************
function fn_EliminarCondicionAdicional(codContratoDocumento) {

    if (codContratoDocumento != "") {

        var sMensaje = "¿Confirma que desea Eliminar la Condición Adicional?";
        var sTitulo = "Eliminar Condición Adicional";
            parent.fn_mdl_confirma(
                            sMensaje, //Mensaje - Obligatorio
                            function() {
                                parent.fn_blockUI();
                                var arrParametros = ["intContratoDocumento",  codContratoDocumento , 
                                                     "strnumeroContrato",    $("#txtNumeroContrato").val()
                                                    ];
                                fn_util_AjaxWM("frmCheckListLegalRegistro.aspx/EliminaContratoDocumento",
                                                arrParametros,
                                                function() {
                                                    fn_ListagrillaCondicionAdicional();
                                                },
                                                function(result) {
                                                    parent.fn_unBlockUI();
                                                    parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                                                }
                            );
                            }, // ACCION SI - Obligatorio
                            "Util/images/question.gif", // Imagen - puede ser nulo
                            function() { }, // ACCION SI - Obligatorio
                            sTitulo
                          );

    } else {
        parent.fn_mdl_mensajeIco("Seleccionar Un registro Verifique !!", "util/images/warning.gif", "ALERTA");
    }
}

//****************************************************************
// Función		:: 	fn_cargaDocumentos
// Descripción	::	
// Log			:: 	IJM - 07/05/2012
//****************************************************************
function fn_cargaDocumentos() {
    fn_ListagrillaCondicionAdicional();
    fn_ListagrillaDocumentos();
}

//****************************************************************
// Función		:: 	fn_MensajeYRedireccionar
// Descripción	::	Le muestra un mensaje con el resultado de la llamada al respectivo web method.
//                  Luego lo redirecciona al formulario de búsquedas ("frmTemporalListado.aspx").
//   			:: 	EBL - 07/05/2012
//****************************************************************
var fn_MensajeYRedireccionar = function(pCodigo) {
    parent.fn_unBlockUI();
    if (pCodigo == "0") {
        parent.fn_mdl_alert("Se grabó con éxito Check List Legal: " + pCodigo.toString(), function() { });
    } else {
        parent.fn_mdl_alert("Se actualizó con éxito Check List Legal", function() { });
    }
    parent.fn_util_CierraModal();
};

//****************************************************************
// Función		:: 	fn_cargaDocumentos
// Descripción	::	Refresca las grillas despues de haber adicionado de un formulario emergente
//                  
// Log			:: 	
//****************************************************************
function fn_cargaDocumentos() {
    fn_ListagrillaCondicionAdicional();
    
    fn_ListagrillaDocumentos();
    fn_ListagrillaRepresentantes();
}
//****************************************************************
// Función		:: 	fn_AgregarRepresentantes
// Descripción	::	Muestra la ventana modal de representantes del cliente disponibles, permitiendo seleccionar al usuario cuales agregar al contrato.
//                  La ventana permite realizar el mantenimiento de los representantes.
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_abreSeguimiento() {

    var sTitulo = "Verificacion";
    var sSubTitulo = "Verificacion:: Checklist Legal";
    var strCodDocContrato = $("#txtNumeroContrato").val();
    parent.fn_util_AbreModal(sSubTitulo, "Comun/frmContratoSeguimientoListado.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodigoContrato=" + strCodDocContrato + "&Add=False", 550, 500, function() { });
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
                        "El Checklist Legal está siendo modificado por el usuario (" + strBloqueoCodUsuario + ") " + strBloqueoNomUsuario + " desde la fecha " + strBloqueoFecha + " ¿Desea continuar con la modificación?"
                        , function() { fn_ActualizaBloqueo(strBloqueoCodigo); }
                        , "Util/images/img_bloqueo.gif"
                        , function() { fn_util_redirect('frmCheckListLegalListado.aspx'); }
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
    fn_util_AjaxWM("frmCheckListLegalRegistro.aspx/ActualizaBloqueo",
             arrParametros,
             function(result) {
                 parent.fn_unBlockUI();
                 $('#cmbEjecutivoLeasing').html(result);
             },
             function(resultado) {
                 parent.fn_unBlockUI();
                 var error = eval("(" + resultado.responseText + ")");
                 parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR EN ULTIMUS");
             }
    );

}