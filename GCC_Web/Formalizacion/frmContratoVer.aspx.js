var CrLf = 1;

var TipoDatoNotarial = new Object();
TipoDatoNotarial.DatoNotarial = '001';
TipoDatoNotarial.Adenda = '002';

var DestinoCredito_Inmueble = ["002"];
var DestinoCredito_Maquinaria = ["003", "004", "005", "006", "011"];
var DestinoCredito_Otros = ["008", "007"];

var CodigoTipoRepresentante = new Object();
CodigoTipoRepresentante.Banco = '001';
CodigoTipoRepresentante.Cliente = '002';

var CodigoEstadoContrato = new Object();
CodigoEstadoContrato.EnElaboracion = '03';
CodigoEstadoContrato.PendienteDeCarta = '04';
CodigoEstadoContrato.EnviadoAlCliente = '05';
CodigoEstadoContrato.PendienteDeFirma = '06';
CodigoEstadoContrato.Formalizado = '07';
CodigoEstadoContrato.Vigente = '08';
CodigoEstadoContrato.Desembolsado = '09';
CodigoEstadoContrato.Anulado = '10';
CodigoEstadoContrato.PendienteDeEnvio = '15';

var CodigoTipoPersona = new Object();
CodigoTipoPersona.Natural = '1';
CodigoTipoPersona.Juridica = '2';

var CodigoEstadoCivil = new Object();
CodigoEstadoCivil.Soltero = '001';
CodigoEstadoCivil.Casado = '002';
CodigoEstadoCivil.Viudo = '003';
CodigoEstadoCivil.Divorciado = '004';

var Departamento = new Object();
Departamento.Lima = '15';

var C_intFlagFiltroDocumento = 1;
var C_intpFlagEnvioCarta = 1;

// Documentos de condiciones adicionales que tienen texto predefinido.
var TipoTextoPredefinido = [
                            // Fianza Solidaria
                            "006",
                            // Cesión de Posición Contractual
                            "010"
                           ];

//****************************************************************
// Funcion		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	EBL - 10/02/2012
//****************************************************************
$(document).ready(function() {

    // Establece la configuración inicial de los controles, incluyendo las grillas.
    fn_InicializarControles();

    //Valida Campos.
    fn_inicializaCampos();

    // Asocia los eventos a los controles.
    fn_InicializarEventos();

    // Lee los datos de los controles ocultos y los usa para seleccionar los controles html select.
    fn_LeerDatos();

    // Muestra u oculta los formularios de los bienes, de acuerdo a la clasificación del contrato.
    fn_configurar_PanelesBienes();

    fn_util_bloquearFormulario();

    //On load Page (siempre al final).
    fn_onLoadPage();
});

//************************************************************
// Función		:: 	fn_ActualizarArchivoAdjunto (OtroConcepto - correo).
// Descripcion 	:: 	Le permite adjuntar el archivo de correo anexo.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_ActualizarArchivoAdjunto() {
    var strNombreArchivo = $("#hddAdjuntarArchivoOtroConcepto").val().split('\\').pop();
    strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
    var img = "<img src='../Util/images/ico_download.gif' alt='Descargar archivo de correo' title='Descargar archivo de correo' style='cursor:pointer;cursor: hand;' />";
    var lnk = "<a href='#'>" + strNombreArchivo + "</a>";

    $("#hddFlagModificado").val("1");
    $("#hddGeneraContrato_Adjunto").val("0");

    $("#dv_ArchivoOtroConcepto").show();
    $("#dv_ArchivoOtroConcepto").html(img + "&nbsp;&nbsp;" + lnk);

    parent.fn_unBlockUI();
}

//************************************************************
// Función		:: 	fn_configurar_PanelesBienes
// Descripcion 	:: 	Configura las distintas ventanas de mantenimiento de los bienes
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_configurar_PanelesBienes() {
    var destinoCredito = $('#hddTipoRubroFinanciamiento').val();

    $("#dvDatosBien").hide();
    $("#dvDatosMaquinaria").hide();
    $("#dvDatosOtros").hide();

    // Inmueble
    if (DestinoCredito_Inmueble.indexOf(destinoCredito) != -1) {
        $("#dvDatosBien").show();
        ConfigurarGrillaInmueble();
    }
    // Maquinaria
    else if (DestinoCredito_Maquinaria.indexOf(destinoCredito) != -1) {
        $("#dvDatosMaquinaria").show();
        ConfigurarGrillaMaquinaria();
    }
    // Otros
    else {
        $("#dvDatosOtros").show();
        ConfigurarGrillaOtrosBienes();
    }
}

//****************************************************************
// Funcion		:: 	fn_LeerDatos
// Descripción	::	Lee los datos de los controles ocultos y los usa para seleccionar los controles html y
//                  configurar y visualizar los controles de manipulación de grillas.
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_LeerDatos() {
    var nombreArchivo;

    // Registros públicos, sólo es visible si clasificación del bien es inmuebles
    var destinoCredito = $("#hddTipoRubroFinanciamiento").val();

    $("#txtFechaAdenda").val($("#hddFechaActual").val());
    $("#txtFechaContratoNotarial").val($("#hddFechaActual").val());

    // Sólo es editable en el primer estado del contrato.
    if ($("#hddCodigoEstadoContrato").val() == CodigoEstadoContrato.EnElaboracion) {
        if ($("#hddCodigoClasificacionContrato").val() != "") {
            $("#cmbClasificacionContrato").val($("#hddCodigoClasificacionContrato").val());
        } else { // Si no existe dato, se elige el predeterminado
            $("#cmbClasificacionContrato").val($("#hddClasifContratoSeleccion").val());
        }
    } else {
        // Si existe un dato, se lo selecciona.
        if ($("#hddCodigoClasificacionContrato").val() != "") {
            $("#cmbClasificacionContrato").val($("#hddCodigoClasificacionContrato").val());
        }
        $("#cmbClasificacionContrato").attr('disabled', 'disabled');
    }

    if (DestinoCredito_Inmueble.indexOf(destinoCredito) == -1) {
        $("#dv_lblRegistrosPublicos").removeClass();
        $("#dv_RegistrosPublicos").hide();
        $("#dv_FechaRegistroPublico").hide();
    }

    // Estado civil
    if ($("#hddCodigoEstadoCivil").val() != "0") {
        $("#cmbEstadoCivil").val($("#hddCodigoEstadoCivil").val());
    }
    if ($("#hddTipoDocumento").val() != "0") {
        $("#cmbTipoDocumento").val($("#hddTipoDocumento").val());
    }
    if ($("#hddTipoDocumentoConyuge").val() != "0") {
        $("#cmbTipoDocumentoConyuge").val($("#hddTipoDocumentoConyuge").val());
    }

    // Archivo contrato, los anexos
    var adjuntarArchivoContrato;
    // Caso persona juridica
    if ($("#hddCodigoTipoPersona").val() == CodigoTipoPersona.Juridica) {
        nombreArchivo = $("#hddAdjuntarArchivo").val().split('\\').pop();
        nombreArchivo = nombreArchivo.substr(28, nombreArchivo.length);

        adjuntarArchivoContrato = "<a href=\"#\">";
        adjuntarArchivoContrato = adjuntarArchivoContrato + "\n<img style=\"cursor: pointer;cursor: hand;width: 35px;height: 35px;border: 0;\" id=\"imgArchivoContratoAdjunto\" title=\"Adjuntar correo\" alt=\"\" src=\"../Util/images/ico_acc_adjuntarMini.gif\" />";
        adjuntarArchivoContrato = adjuntarArchivoContrato + "\n<br />Adjuntar Contrato";
        adjuntarArchivoContrato = adjuntarArchivoContrato + "\n</a>";
        $("#dv_AdjuntarArchivoContrato").html(adjuntarArchivoContrato);

        if ($("#hddAdjuntarArchivo").val() != "") {
            $("#dv_DescargarArchivoContrato").html("<img src='../Util/images/ico_download.gif' alt='Descargar archivo de anexo' title='Descargar archivo de anexo' style='cursor:pointer;cursor: hand;border: 0;' />&nbsp;&nbsp;<a href='#'>" + nombreArchivo + "</a>");
        }

    } else {
        // Caso persona natural
        // Las persona naturales pueden adjuntar archivos sin haber generado previamente el archivo de contrato.
        adjuntarArchivoContrato = "<a href=\"#\">";
        adjuntarArchivoContrato = adjuntarArchivoContrato + "\n<img style=\"cursor: pointer;cursor: hand;width: 35px;height: 35px;border: 0;\" id=\"imgArchivoContratoAdjunto\" title=\"Adjuntar correo\" alt=\"\" src=\"../Util/images/ico_acc_adjuntarMini.gif\" />";
        adjuntarArchivoContrato = adjuntarArchivoContrato + "\n<br />Adjuntar Contrato";
        adjuntarArchivoContrato = adjuntarArchivoContrato + "\n</a>";
        $("#dv_AdjuntarArchivoContrato").html(adjuntarArchivoContrato);

        if ($("#hddAdjuntarArchivo").val() != "") {
            nombreArchivo = $("#hddAdjuntarArchivo").val().split('\\').pop();
            nombreArchivo = nombreArchivo.substr(28, nombreArchivo.length);

            $("#dv_DescargarArchivoContrato").html("<img src='../Util/images/ico_download.gif' alt='Descargar archivo de anexo' title='Descargar archivo de anexo' style='cursor:pointer;cursor: hand;' />&nbsp;&nbsp;<a href='#'>" + nombreArchivo + "</a>");
        } else {
            $("#dv_DescargarArchivoContrato").html("");
        }
    }

    // Correo de otros conceptos.
    if ($("#hddAdjuntarArchivoOtroConcepto").val() != "") {
        nombreArchivo = $("#hddAdjuntarArchivoOtroConcepto").val().split('\\').pop();
        nombreArchivo = nombreArchivo.substr(28, nombreArchivo.length);

        $("#dv_ArchivoOtroConcepto").show();
        $("#dv_ArchivoOtroConcepto").html("<img style=\"cursor: pointer;cursor: hand;\" id=\"imgArchivoOtroConcepto\" src=\"../Util/images/ico_download.gif\" alt=\"Descargar correo adjunto\" alt=\"\" title=\"Descargar correo adjunto\" />&nbsp;&nbsp;<a href='#'>" + nombreArchivo + "</a>");
    } else {
        $("#dv_ArchivoOtroConcepto").hide();
        $("#dv_ArchivoOtroConcepto").html("");
    }

    // Documento de separación
    if ($("#hddAdjuntarArchivoDocumentoSeparacion").val() != "") {
        nombreArchivo = $("#hddAdjuntarArchivoDocumentoSeparacion").val().split('\\').pop();
        nombreArchivo = nombreArchivo.substr(28, nombreArchivo.length);
        $("#dv_AdjuntarArchivoDocumentoSeparacion").html("<img style=\"cursor: pointer;cursor: hand;\" id=\"imgArchivoOtroConcepto\" src=\"../Util/images/ico_download.gif\" alt=\"Descargar correo adjunto\" alt=\"\" title=\"Descargar correo adjunto\" />&nbsp;&nbsp;<a href='#'>" + nombreArchivo + "</a>");
    }

    // Ocultar datos del conyugue si no es persona natural y estado civil casado
    if ($("#hddTipoDocumento").val() == CodigoTipoPersona.Natural
        && $("#hddCodigoEstadoCivil").val() == CodigoEstadoCivil.Casado) {
        $("#fs_DatosConyugue").show();
    } else {
        $("#fs_DatosConyugue").hide();
    }

    // Ocultar estado civil si es persona juridica
    if ($("#hddCodigoTipoPersona").val() == CodigoTipoPersona.Natural) {
        $("#cmbEstadoCivil").show();
        $("#td_EstadoCivil").show();
    } else {
        $("#cmbEstadoCivil").hide();
        $("#td_EstadoCivil").hide();
    }

    // Botón "Aprobar", sólo es visible cuando el estado es enviado al cliente, para pasar al estado pendiente de firma.
    if ($("#hddCodigoEstadoContrato").val() != CodigoEstadoContrato.EnviadoAlCliente) {
        $("#dv_aprobarContrato").hide();
    }
    // Configurar fecha firma en notaria
    if ($("#hddCodigoEstadoContrato").val() != CodigoEstadoContrato.PendienteDeFirma) {
        $("#chkFirmaNotaria").attr('disabled', 'disabled');
    }
    // Configura la fecha de registros públicos
    if ($("#hddCodigoEstadoContrato").val() != CodigoEstadoContrato.Formalizado) {
        $("#chkRegistroPublico").attr('disabled', 'disabled');
    }
    if (!($("#hddCodigoEstadoContrato").val() == CodigoEstadoContrato.Formalizado ||
          $("#hddCodigoEstadoContrato").val() == CodigoEstadoContrato.Vigente ||
          $("#hddCodigoEstadoContrato").val() == CodigoEstadoContrato.Desembolsado)) {
        $("#tab-5").hide();
        $("#li_adenda").hide();
    }
    // Seleccionar el estado del bien de acuerdo al dato en la cotización
    if ($("#hddCodigoEstadoBien").val() != "") {
        $("#cmbEstadoBienInmueble").val($("#hddCodigoEstadoBien").val());
        $("#cmbEstadobienMaquina").val($("#hddCodigoEstadoBien").val());
        $("#cmbEstadoDatosOtros").val($("#hddCodigoEstadoBien").val());
    }

    // Otros conceptos
    if ($("#hddCodigoEstadoContrato").val() === CodigoEstadoContrato.Formalizado) {
        $("#txtImporteAtrasoPorc").addClass("css_input_inactivo");
        $("#txtImporteAtrasoPorc").attr("readonly", true);
        $("#txtdiasVencimiento").addClass("css_input_inactivo");
        $("#txtdiasVencimiento").attr("readonly", true);
        $("#txtPorcentajeCuota").addClass("css_input_inactivo");
        $("#txtPorcentajeCuota").attr("readonly", true);
        $("#txtaOtrasPenalidades").addClass("css_input_inactivo");
        $("#txtaOtrasPenalidades").attr("readonly", true);
    }
}

//****************************************************************
// Funcion		:: 	fn_InicializarControles
// Descripción	::	Establece la configuración inicial de los controles, incluyendo las grillas.
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_InicializarControles() {
    fn_ConfigurarGrillaContratoProveedor();
    fn_ConfigurarGrillaBienes();

    fn_ConfigurarGrillaDatosNotariales();
    fn_ConfigurarGrillaAdenda();
    fn_ConfigurarGrillaDocumentos();
    fn_ConfigurarCondicionesAdicionales();

    fn_ConfigurarGrillaRepresentantesInterbank();
    fn_ConfigurarGrillaRepresentantesCliente();

    //Valida Tabs
    $("div#divTabs").tabs({
        show: function() {
            fn_doResize();
        }
    });
}

//****************************************************************
// Función		:: 	fn_ConfigurarGrillaBienes
// Descripción	::	Carga Grilla de los bienes del proveedor seleccionado
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ConfigurarGrillaBienes() {
    var destinoCredito = $('#hddTipoRubroFinanciamiento').val();

    if (DestinoCredito_Inmueble.indexOf(destinoCredito) != -1) {
        // Inmueble
        ConfigurarGrillaInmueble();
    } else if (DestinoCredito_Maquinaria.indexOf(destinoCredito) != -1) {
        // Maquinaria
        ConfigurarGrillaMaquinaria();
    } else {
        // Otros
        ConfigurarGrillaOtrosBienes();
    }
}

//************************************************************
// Función		:: 	fn_ListaDatosBienes
// Descripción 	:: 	Lista los bienes del proveedor seleccionado, cargando la grilla seleccionada.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_ListaDatosBienes() {
    var destinoCredito = $('#hddTipoRubroFinanciamiento').val();
    var arrParametros;

    // Inmueble
    if (DestinoCredito_Inmueble.indexOf(destinoCredito) != -1) {
        arrParametros = [
                         "pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_A", "page"),    // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación
                         "pContrato", $('#hddCodigoContrato').val(),
                         "pFields", "CantidadProducto|SecFinanciamiento|Comentario|EstadoBien|Ubicacion|DepartamentoNombre|ProvinciaNombre"
                        ];
    }
    // Maquinaria
    else if (DestinoCredito_Maquinaria.indexOf(destinoCredito) != -1) {
        arrParametros = [
                     "pPageSize", fn_util_getJQGridParam("jqGrid_lista_B", "rowNum"),     // Cantidad de elementos de la página
                     "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_B", "page"),    // Página actual
                     "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_B", "sortname"), // Columna a ordenar
                     "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_B", "sortorder"), // Criterio de ordenación
                     "pContrato", $('#hddCodigoContrato').val(),
                     "pFields", "CantidadProducto|SecFinanciamiento|Comentario|EstadoBien|Ubicacion|NroSerie|Marca"
                     ];
    }
    // Otros
    else {
        arrParametros = [
                     "pPageSize", fn_util_getJQGridParam("jqGrid_lista_J", "rowNum"),     // Cantidad de elementos de la página
                     "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_J", "page"),    // Página actual
                     "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_J", "sortname"), // Columna a ordenar
                     "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_J", "sortorder"), // Criterio de ordenación
                     "pContrato", $('#hddCodigoContrato').val(),
                     "pFields", "CantidadProducto|SecFinanciamiento|Comentario|EstadoBien|Ubicacion|DepartamentoNombre|ProvinciaNombre|Marca"
                    ];
    }

    fn_util_AjaxWM("frmContratoVer.aspx/ListarBienes",
                    arrParametros,
                    function(jsondata) {
                        // Inmueble
                        if (DestinoCredito_Inmueble.indexOf(destinoCredito) != -1) {
                            jqGrid_lista_A.addJSONData(jsondata);
                        }
                        // Maquinaria
                        else if (DestinoCredito_Maquinaria.indexOf(destinoCredito) != -1) {
                            jqGrid_lista_B.addJSONData(jsondata);
                        }
                        // Otros
                        else {
                            jqGrid_lista_J.addJSONData(jsondata);
                        }
                    },
                    function(request) {
                        parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR AL LEER BIENES");
                    }
                   );
}

//****************************************************************
// Funcion		:: 	ConfigurarGrillaInmueble
// Descripción	::	DATOS DEL BIEN INMOVILIARIOS.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function ConfigurarGrillaInmueble() {
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            fn_ListaDatosBienes();
        },
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "SecFinanciamiento" // Índice de la columna con la clave primaria.
        },
        colNames: ['Cantidad', '', 'Descripción', 'Estado Bien', 'Ubicación', 'Departamento', 'Provincia'],
        colModel: [
	                { name: 'CantidadProducto', index: 'CantidadProducto', width: 50, align: "right", sorttype: "string" },
	                { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
                    { name: 'Comentario', index: 'Comentario', width: 150, align: "left", sorttype: "string" },
                    { name: 'EstadoBien', index: 'EstadoBien', width: 150, align: "center", sorttype: "string" },
	                { name: 'Ubicacion', index: 'Ubicacion', align: "left", sorttype: "string" },

	                { name: 'DepartamentoNombre', index: 'DepartamentoNombre', align: "left", sorttype: "string" },
                    { name: 'ProvinciaNombre', index: 'ProvinciaNombre', align: "left", sorttype: "string" }
                ],
        height: '100%',
        pager: '#jqGrid_pager_A',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10, // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'SecFinanciamiento',
        sortorder: 'desc',
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        multiselect: true
    });
    $("#jqGrid_lista_A").jqGrid('navGrid', '#jqGrid_pager_A', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_A").hide();
    $("#jqGrid_lista_A").setGridWidth($(window).width() - 150);
}

//****************************************************************
// Funcion		:: 	ConfigurarGrillaMaquinaria
// Descripción	::	DATOS DE LAS MAQUINARIAS
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function ConfigurarGrillaMaquinaria() {
    $("#jqGrid_lista_B").jqGrid({
        datatype: function() {
            fn_ListaDatosBienes();
        },
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "SecFinanciamiento" // Índice de la columna con la clave primaria.
        },
        colNames: ['Cantidad', '', 'Descripción', 'Estado', 'Ubicación', 'Serie', 'Marca'],
        colModel: [
                    { name: 'CantidadProducto', index: 'CantidadProducto', width: 40, align: "right", sorttype: "string" },
			        { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
                    { name: 'Comentario', index: 'Comentario', width: 150, align: "left", sorttype: "string" },
                    { name: 'EstadoBien', index: 'EstadoBien', width: 80, align: "center", sorttype: "string" },

                    { name: 'Ubicacion', index: 'Ubicacion', width: 80, align: "left", sorttype: "string" },
			        { name: 'NroSerie', index: 'NroSerie', width: 90, sorttype: "string", align: "left" },
                    { name: 'Marca', index: 'Marca', width: 90, align: "left" }
		        ],
        height: '100%',
        pager: '#jqGrid_pager_B',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10, // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'SecFinanciamiento',
        sortorder: 'desc', // Muestra la cantidad de registros.
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        multiselect: true
    });
    $("#jqGrid_lista_B").jqGrid('navGrid', '#jqGrid_pager_B', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_B").hide();
    $("#jqGrid_lista_B").setGridWidth($(window).width() - 150);
}

//****************************************************************
// Funcion		:: 	ConfigurarGrillaOtrosBienes
// Descripción	::	DATOS DE OTROS BIENES
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function ConfigurarGrillaOtrosBienes() {
    $("#jqGrid_lista_J").jqGrid({
        datatype: function() {
            fn_ListaDatosBienes();
        },
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "SecFinanciamiento" // Índice de la columna con la clave primaria.
        },
        colNames: ['Cantidad', '', 'Descripción', '', 'Ubicación', 'Departamento', 'Provincia', 'Marca'],
        colModel: [
		            { name: 'CantidadProducto', index: 'CantidadProducto', width: 50, sorttype: "string", align: "right" },
                    { name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
                    { name: 'Comentario', index: 'Comentario', width: 150, align: "left", sorttype: "string" },
                    { name: 'EstadoBien', index: 'EstadoBien', align: "center", sorttype: "string" },
                    { name: 'Ubicacion', index: 'Ubicacion', align: "left", sorttype: "string" },
                    { name: 'DepartamentoNombre', index: 'DepartamentoNombre', align: "left", sorttype: "string" },
                    { name: 'ProvinciaNombre', index: 'ProvinciaNombre', align: "left", sorttype: "string" },
                    { name: 'Marca', index: 'Marca', width: 70, align: "left", sorttype: "string" }
                  ],
        height: '100%',
        pager: '#jqGrid_pager_J',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10, // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'SecFinanciamiento',
        sortorder: 'desc',
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        altclass: 'gridAltClass',
        multiselect: true
    });
    $("#jqGrid_lista_J").jqGrid('navGrid', '#jqGrid_pager_J', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_J").hide();
    $("#jqGrid_lista_J").setGridWidth($(window).width() - 150);
}

//****************************************************************
// Funcion		:: 	fn_ListagrillaDocumentos
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_ListagrillaDocumentos() {
    var arrParametros = [
                         "pPageSize", fn_util_getJQGridParam("jqGrid_lista_K", "rowNum"), // Cantidad de elementos de la página.
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_K", "page"), // Página actual.
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_K", "sortname"), // Columna a ordenar.
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_K", "sortorder"), // Criterio de ordenación.
                         "pCodigo", $("#txtNroContrato").val(),
                         "pFlagFiltro", C_intFlagFiltroDocumento,
                         "pFlagEnvioCarta", C_intpFlagEnvioCarta,
                         "pFields", "CodigoContratoDocumento|Descripcion|adjunto|FlagObservacionLegal|Flagobservacion"
                        ];

    fn_util_AjaxWM("frmContratoVer.aspx/ListaDocumentosContrato",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_K.addJSONData(jsondata);
                        parent.fn_unBlockUI();
                    },
                    function(request) {
                        parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR AL LEER DOCUMENTOS");
                        parent.fn_unBlockUI();
                    }
                   );
}

//****************************************************************
// Funcion		:: 	fn_ConfigurarGrillaDocumentos
// Descripción	::	DOCUMENTOS
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ConfigurarGrillaDocumentos() {
    $("#jqGrid_lista_K").jqGrid({
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
        colNames: ['CodigoContratoDocumento', 'Documentos', 'Archivo', 'Obs. Comercial', 'Obs. Legal'],
        colModel: [
                     { name: 'CodigoContratoDocumento', index: 'CodigoContratoDocumento', hidden: true },
                     { name: 'Descripcion', index: 'Descripcion', width: 400, align: "left", sorttype: "string" },
                     { name: 'adjunto', index: 'adjunto', width: 80, align: "center", sorttype: "string", formatter: verAdjunto4 },
                     { name: 'lupa3', index: 'lupa3', width: 80, align: "center", sortable: false, formatter: lupa3 },
                     { name: 'lupa', index: 'lupa', width: 80, align: "center", sortable: false, formatter: lupa }
                 ],
        height: '100%',
        pager: '#jqGrid_pager_K',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10,                          // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CodigoContratoDocumento', // Columna a ordenar por defecto.
        sortorder: 'asc',                    // Criterio de ordenación por defecto.
        viewrecords: true,                   // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass'
    });
    $("#jqGrid_lista_K").jqGrid('navGrid', '#jqGrid_pager_K', { edit: false, add: false, del: false });
    $("#jqGrid_lista_K").setGridWidth($(window).width() - 150);
    $("#search_jqGrid_lista_K").hide();
    
    /***************************************************
    AGREGA ICONO Y FUNCIONALIDAD A LA GRILLA DOCUMENTOS   
    ****************************************************/
    function lupa(cellvalue, options, rowObject) {
        if (rowObject.FlagObservacionLegal == 0) {
            return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
        } else {
            return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
        }
    };
    function lupa3(cellvalue, options, rowObject) {
        if (rowObject.Flagobservacion == 0) {
            return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='' style='cursor: pointer;cursor: hand;' />";
        } else {
            return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
        }
    };
    function verAdjunto4(cellvalue, options, rowObject) {
        if (rowObject.adjunto != "") {
            var strNombreArchivo = rowObject.adjunto.split('\\').pop();

            strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
            return "<img src='../Util/images/ico_download.gif' alt='Descargar/Mostrar Archivo' title='" + strNombreArchivo + "' style='cursor:pointer;'/>";
        } else {
            return ".";
        }
    }
}

//****************************************************************
// Funcion		:: 	fn_ConfigurarGrillaContratoProveedor
// Descripción	::	Carga Grilla datos de los proveedores del contrato
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ConfigurarGrillaContratoProveedor() {
    $("#jqGrid_lista_F").jqGrid({
        datatype: function() {
            fn_ListaDatosContratoProveedor();
        },
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodigoContratoProveedor" // Índice de la columna con la clave primaria.
        },
        colNames: ['', 'Tipo de Documento', 'Nro. Documento', 'Razón social o Nombre', 'Nacionalidad'],
        colModel: [
                     { name: 'CodigoContratoProveedor', index: 'CodigoContratoProveedor', hidden: true },
                     { name: 'TipoDocumento', index: 'TipoDocumento', width: 75, sorttype: "string", align: "center" },
                     { name: 'RUC', index: 'RUC', width: 50, sorttype: "string", align: "left" },
                     { name: 'NombreInstitucion', index: 'NombreInstitucion', width: 200, sorttype: "string", align: "left" },
                     { name: 'Nacionalidad', index: 'Nacionalidad', width: 75, sorttype: "string", align: "left" }
                  ],
        height: '100%',
        pager: '#jqGrid_pager_F',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10, // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CodigoContratoProveedor',
        sortorder: 'desc',
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        multiselect: false
    });
    $("#jqGrid_lista_F").jqGrid('navGrid', '#jqGrid_pager_F', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_F").hide();
    $("#jqGrid_lista_F").setGridWidth($(window).width() - 150);
}

//****************************************************************
// Funcion		:: 	fn_ListaDatosContratoProveedor
// Descripción	::	Devuelve los proveedores del contrato.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ListaDatosContratoProveedor() {

    var arrParametros = [
                         "pPageSize", fn_util_getJQGridParam("jqGrid_lista_F", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_F", "page"), // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_F", "sortname"), // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_F", "sortorder"), // Criterio de ordenación
                         "pContrato", $('#hddCodigoContrato').val(),
                         "pFields", "CodigoContratoProveedor|TipoDocumento|RUC|NombreInstitucion|Nacionalidad"
                        ];

    fn_util_AjaxWM("frmContratoVer.aspx/ListarContratoProveedores",
                   arrParametros,
                   function(jsondata) {
                       jqGrid_lista_F.addJSONData(jsondata);
                   },
                   function(request) {
                       parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR AL LEER PROVEEDORES");
                   }
                   );
}

//****************************************************************
// Funcion		:: 	fn_InicializarEventos
// Descripción	::	Asocia los eventos a los controles respectivos
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_InicializarEventos() {
    $('#chkRegistroPublico').click(function() {
        if ($("#chkRegistroPublico").is(':checked')) {
            $("#txtFechaRegistroPublico").removeClass("css_input_inactivo");
            $("#txtFechaRegistroPublico").addClass("css_input");
            $('#txtFechaRegistroPublico').attr('readonly', false);
            $('#txtFechaRegistroPublico').datepicker('enable');
        } else {
            $("#txtFechaRegistroPublico").removeClass("css_input");
            $("#txtFechaRegistroPublico").addClass("css_input_inactivo");
            $('#txtFechaRegistroPublico').attr('readonly', true);
            $('#txtFechaRegistroPublico').datepicker().datepicker('disable');
        }
    });

    $('#chkFirmaNotaria').click(function() {
        if ($("#chkFirmaNotaria").is(':checked')) {
            $("#txtFechaFirmaNotaria").removeClass("css_input_inactivo");
            $("#txtFechaFirmaNotaria").addClass("css_input");
            $('#txtFechaFirmaNotaria').attr('readonly', false);
            $('#txtFechaFirmaNotaria').datepicker('enable');
        } else {
            $("#txtFechaFirmaNotaria").removeClass("css_input");
            $("#txtFechaFirmaNotaria").addClass("css_input_inactivo");
            $('#txtFechaFirmaNotaria').attr('readonly', true);
            $('#txtFechaFirmaNotaria').datepicker().datepicker('disable');
        }

        fn_util_SeteaCalendario($('input[id*=txtFecha]'));
    });

    $('#cmbEstadoCivil').change(function() {
        if ($("#hddTipoDocumento").val() == CodigoTipoPersona.Natural
            && $("#cmbEstadoCivil").val() == CodigoEstadoCivil.Casado) {
            $("#fs_DatosConyugue").show();
        } else {
            $("#fs_DatosConyugue").hide();
        }
    });
}

//****************************************************************
// Funcion		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
    // Setea Calendarios
    fn_util_SeteaCalendario($('input[id*=txtFecha]'));
    $('#txtFechaContrato').datepicker().datepicker('disable');
    $('#txtFechamaxdisp').datepicker().datepicker('disable');
    $('#txtFechaActivacion').datepicker().datepicker('disable');
    $('#txtFechaFirmaNotaria').datepicker().datepicker('disable');
    $('#txtFechaRegistroPublico').datepicker().datepicker('disable');

    // Valida campos obligatorio
    fn_seteaCamposObligatorios();

    // Barra botones de las grillas
    $("#dv_ProcesoDatosNotariales").hide();
    $("#dv_ProcesoAdenda").hide();
    $("#dv_ProcesoRepresentantes").hide();

    //TAB :: DATOS DEL BIEN	
    $("#dv_ProcesoBien").hide();
    $("#dv_ProcesoMaquina").hide();
    $("#dv_ProcesoDatosOtros").hide();

    //TAB :: CONDICIONES ADICIONALES
    $('#txtObservaciones').maxLength(100);

    // TAB :: OTROS CONCEPTOS
    $('#txtPartidaRegistralDatosOtros').validText({ type: 'alphanumeric', length: 8 });
    $('#txtOficinaRegistralDatosOtros').maxLength(50);

    $("#txtImporteAtrasoPorc").validNumber({ value: '', decimals: 2, length: 5 });
    $('#txtdiasatraso').validText({ type: 'number', length: 10 });
    $('#txtaOtrasPenalidades').maxLength(500);
    $('#txtdiasVencimiento').validText({ type: 'number', length: 2 });
    $('#txtPorcentajeCuota').validNumber({ value: '', decimals: 2, length: 5 });

    $('#txtKardex').validText({ type: 'number', length: 8 });
    $('#txtObservacionesNotariales').maxLength(300);

    // TAB :: ADENDAS
    if ($("#hddCodigoEstadoContrato").val() == CodigoEstadoContrato.Formalizado) {
        $('#txtFechaAdenda').datepicker().datepicker('disable');
        $('#txtaMotivo').maxLength(100);
        $('#txtComisionEstructuracion').validText({ type: 'number', length: 10 });
        $('#txtKadexAdenda').validText({ type: 'number', length: 8 });
    }
    
    // Datos conyugue
    $('#txtNombreConyuge').validText({ type: 'alphanumeric', length: 200 });
    $('#txtnumerodocumento').validText({ type: 'number', length: 8 });
}

//****************************************************************
// Funcion		:: 	fn_seteaCamposObligatorios
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_seteaCamposObligatorios() {
    // Obligatorio para "Guardar y Enviar"
    if ($("#hddCodigoEstadoContrato").val() != CodigoEstadoContrato.Formalizado) {
        fn_util_SeteaObligatorio($("#cmbClasificacionContrato"), "select");
        fn_util_SeteaObligatorio($("#txtImporteAtrasoPorc"), "input");
        fn_util_SeteaObligatorio($("#txtdiasVencimiento"), "input");
        fn_util_SeteaObligatorio($("#txtPorcentajeCuota"), "input");
    }
    // Obligatorio si es persona natural
    if ($("#hddTipoDocumento").val()) {
        fn_util_SeteaObligatorio($("#cmbEstadoCivil"), "select");
    }
    // Obligatorio si es persona natural y casado
    if ($("#hddTipoDocumento").val() == CodigoTipoPersona.Natural
        && $("#hddCodigoEstadoCivil").val() == CodigoEstadoCivil.Casado) {
        fn_util_SeteaObligatorio($("#txtNombreConyuge"), "input");
        fn_util_SeteaObligatorio($("#cmbTipoDocumentoConyuge"), "select");
        fn_util_SeteaObligatorio($("#txtnumerodocumento"), "input");
    }

    // Controles datos notariales
    fn_util_SeteaObligatorio($("#cmbDepartamento"), "select");
    fn_util_SeteaObligatorio($("#cmbNotariaDatoNotarial"), "select");
    fn_util_SeteaObligatorio($("#cmbTipoContrato"), "select");

    // Controles Adenda
    if ($("#hddCodigoEstadoContrato").val() == CodigoEstadoContrato.Formalizado) {
        fn_util_SeteaObligatorio($("#txtFechaEscrituraPub"), "input");
        fn_util_SeteaObligatorio($("#txtaMotivo"), "input");
        fn_util_SeteaObligatorio($("#cmbDepartamentoAdenda"), "select");
        fn_util_SeteaObligatorio($("#cmbNotariaAdenda"), "select");
        fn_util_SeteaObligatorio($("#txtKardexAdenda"), "input");
    }
}

//****************************************************************
// Funcion		:: 	fn_ConfigurarGrillaAdenda
// Descripción	::	Carga Grilla de las adendas
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ConfigurarGrillaAdenda() {
    $("#jqGrid_lista_I").jqGrid({
        datatype: function() {
            fn_ListaDatosNotariales(TipoDatoNotarial.Adenda);
        },
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodigoNotarial" // Índice de la columna con la clave primaria.
        },
        colNames: ['', 'RutaArchivo', 'Archivo', 'Fecha', 'Motivo', 'Por cuenta de', 'Kardex'],
        colModel: [
            { name: 'CodigoNotarial', index: 'CodigoNotarial', hidden: true },
		    { name: 'NombreArchivo', index: 'NombreArchivo', hidden: true },
		    { name: 'Archivo', index: 'Archivo', width: 45, sorttype: "string", align: "center", formatter: fn_NombreArchivoAdenda },
		    { name: 'Fecha', index: 'Fecha', width: 22, sorttype: "string", align: "center", formatter: fn_util_ValidaStringFecha },
		    { name: 'Motivo', index: 'Motivo', width: 50, sorttype: "string", align: "left" },
		    { name: 'PorCuentaDe', index: 'PorCuentaDe', width: 45, align: "left", sorttype: "string" },
		    { name: 'Kardex', index: 'Kardex', width: 35, sorttype: "string", align: "left" }
	    ],
        height: '100%',
        pager: '#jqGrid_pager_I',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10, // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CodigoNotarial',
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        altclass: 'gridAltClass',
        multiselect: true
    });
    $("#jqGrid_lista_I").jqGrid('navGrid', '#jqGrid_pager_I', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_I").hide();
    $("#jqGrid_lista_I").setGridWidth($(window).width() - 250);
}

//****************************************************************
// Función		:: 	fn_NombreArchivoAdenda
// Descripción	::	Lee el nombre del archivo, excluyendo la ruta relativa del archivo.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_NombreArchivoAdenda(cellvalue, options, rowObject) {
    var strNombreArchivo = rowObject.NombreArchivo.split('\\').pop();
    
    strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);

    return strNombreArchivo;
}

//****************************************************************
// Funcion		:: 	fn_ConfigurarGrillaDatosNotariales
// Descripción	::	Carga Grilla datos notariales
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ConfigurarGrillaDatosNotariales() {
    $("#jqGrid_lista_G").jqGrid({
        datatype: function() {
            fn_ListaDatosNotariales(TipoDatoNotarial.DatoNotarial);
        },
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodigoNotarial" // Índice de la columna con la clave primaria.
        },
        colNames: ['', 'Contrato', 'Kardex', 'Fecha', 'Notaria', 'Departamento', 'Provincia', 'Observaciones'],
        colModel: [
            { name: 'CodigoNotarial', index: 'CodigoNotarial', hidden: true },
            { name: 'Minuta', index: 'Minuta', width: 50, sorttype: "string", align: "left" },
            { name: 'Kardex', index: 'Kardex', width: 50, sorttype: "string", align: "left" },
            { name: 'Fecha', index: 'Fecha', width: 30, formatter: fn_util_ValidaStringFecha, align: "center" },
            { name: 'Notaria', index: 'Notaria', width: 50, sorttype: "string", align: "left" },
            { name: 'Departamento', index: 'Departamento', width: 50, sorttype: "string", align: "left" },
            { name: 'Provincia', index: 'Provincia', width: 50, sorttype: "string", align: "left" },
            { name: 'Observacion', index: 'Observacion', width: 100, sorttype: "string", align: "left" }
        ],
        height: '100%',
        pager: '#jqGrid_pager_G',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10, // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CodigoNotarial',
        sortorder: 'desc',
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        multiselect: true
    });
    $("#jqGrid_lista_G").jqGrid('navGrid', '#jqGrid_pager_G', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_G").hide();
    $("#jqGrid_lista_G").setGridWidth($(window).width() - 250);
}

//****************************************************************
// Funcion		:: 	fn_ListaDatosNotariales
// Descripción	::	Devuelve los datos notariales, para el tipo de origen.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ListaDatosNotariales(tipoDatoNotarial) {
    try {
        parent.fn_blockUI();
        var arrParametros;

        if (tipoDatoNotarial == TipoDatoNotarial.DatoNotarial) {
            arrParametros = [
                                "pPageSize", fn_util_getJQGridParam("jqGrid_lista_G", "rowNum"), // Cantidad de elementos de la página
                                "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_G", "page"), // Página actual
                                "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_G", "sortname"), // Columna a ordenar
                                "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_G", "sortorder"), // Criterio de ordenación
                                "pContrato", $('#hddCodigoContrato').val(),
                                "pTipoDatoNotarial", tipoDatoNotarial,
                                "pFields", "CodigoNotarial|Minuta|Kardex|Fecha|Notaria|Departamento|Provincia|Observacion"
                            ];
        } else {
            arrParametros = [
                                "pPageSize", fn_util_getJQGridParam("jqGrid_lista_I", "rowNum"), // Cantidad de elementos de la página
                                "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_I", "page"), // Página actual
                                "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_I", "sortname"), // Columna a ordenar
                                "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_I", "sortorder"), // Criterio de ordenación
                                "pContrato", $('#hddCodigoContrato').val(),
                                "pTipoDatoNotarial", tipoDatoNotarial,
                                "pFields", "CodigoNotarial|NombreArchivo|Fecha|Motivo|CodigoPorCuenta|PorCuentaDe|Kardex"
                            ];
        }

        fn_util_AjaxSyncWM("frmContratoRegistro.aspx/ListadoContratoNotarialPaginado",
                            arrParametros,
                            function(jsondata) {
                                if (tipoDatoNotarial == TipoDatoNotarial.DatoNotarial) {
                                    jqGrid_lista_G.addJSONData(jsondata);
                                } else {
                                    jqGrid_lista_I.addJSONData(jsondata);
                                }
                                parent.fn_unBlockUI();
                                fn_doResize();
                            },
                            function(request) {
                                if (tipoDatoNotarial == TipoDatoNotarial.DatoNotarial) {
                                    parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR AL LISTAR DATOS NOTARIALES");
                                } else {
                                    parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR AL LISTAR ADENDAS");
                                }
                            }
                           );
    } catch (ex) {
        parent.fn_unBlockUI();
        if (tipoDatoNotarial == TipoDatoNotarial.DatoNotarial) {
            parent.fn_mdl_mensajeIco(ex.message, "util/images/warning.gif", "ERROR AL LISTAR DATOS NOTARIALES");
        } else {
            parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR AL LISTAR ADENDAS");
        }
    }
}

//****************************************************************
// Funcion		:: 	fn_ListaCondicionesAdicionales
// Descripción	::	Devuelve una lista de objetos GCC_ContratoDocumento para la grilla jqGrid_lista_E.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ListaCondicionesAdicionales() {
    var arrParametros = [
                         "pPageSize", fn_util_getJQGridParam("jqGrid_lista_E", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_E", "page"), // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_E", "sortname"), // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_E", "sortorder"), // Criterio de ordenación
                         "pCodigo", $('#hddCodigoContrato').val(),
                         "pFields", "CodigoContratoDocumento|Descripcion|adjunto|FlagObservacionLegal|Flagobservacion"
                         ];

    fn_util_AjaxWM("frmContratoVer.aspx/ListaCondicionesAdicionales",
                   arrParametros,
                   function(jsondata) {
                       jqGrid_lista_E.addJSONData(jsondata);
                   },
                   function(request) {
                       parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR AL LISTAR CONDICIONES ADICIONALES");
                   }
                   );
}

//****************************************************************
// Funcion		:: 	fn_ListaCondicionesAdicionales
// Descripción	::	Devuelve una lista de representantes del cliente para la grilla jqGrid_lista_H.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ListaRepresentantesCliente() {
    var arrParametros = [
                             "pPageSize", fn_util_getJQGridParam("jqGrid_lista_H", "rowNum"), // Cantidad de elementos de la página
                             "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_H", "page"), // Página actual
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_H", "sortname"), // Columna a ordenar
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_H", "sortorder"), // Criterio de ordenación
                             "pNumeroContrato", $('#hddCodigoContrato').val(),
                             "pCodigoTipoRepresentante", CodigoTipoRepresentante.Cliente,
                             "pFields", "CodigoRepresentante|TipoDocumento|NroDocumento|NombreRepresentante|PartidaRegistral|OficinaRegistral|Departamento|Provincia|Distrito"
                         ];

    fn_util_AjaxWM("frmContratoVer.aspx/ListaRepresentantes",
                   arrParametros,
                   function(jsondata) {
                       jqGrid_lista_H.addJSONData(jsondata);
                       fn_doResize();
                   },
                   function(request) {
                       parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR AL LEER");
                   }
                   );
}

//****************************************************************
// Funcion		:: 	fn_ConfigurarGrillaRepresentantesCliente
// Descripción	::	Configura la estructura de la grilla jqGrid_lista_H, para los representantes del cliente.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ConfigurarGrillaRepresentantesCliente() {
    $("#jqGrid_lista_H").jqGrid({
        datatype: function() {
            fn_ListaRepresentantesCliente();
        },
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodigoRepresentante" // Índice de la columna con la clave primaria.
        },
        colNames: ['', 'Tipo  de Documento', 'Nro. Documento', 'Representante', 'Partida Registral', 'Oficina Registral', 'Departamento / Provincia / Distrito', '', '', ''],
        colModel: [
                      { name: 'CodigoRepresentante', index: 'CodigoRepresentante', hidden: true },
                      { name: 'TipoDocumento', index: 'TipoDocumento', width: 70, sorttype: "string", align: "left" },
		              { name: 'NroDocumento', index: 'NroDocumento', width: 45, sorttype: "string", align: "left" },
		              { name: 'NombreRepresentante', index: 'NombreRepresentante', width: 125, align: "left" },
		              { name: 'PartidaRegistral', index: 'PartidaRegistral', width: 100, align: "left" },
		              { name: 'OficinaRegistral', index: 'OficinaRegistral', width: 100, align: "left" },
	                  { name: 'UbigeoNombre', index: 'UbigeoNombre', width: 200, sorttype: "string", align: "left", formatter: fn_Ubigeo, sortable: false },
                      { name: 'Departamento', index: 'Departamento', hidden: true },
                      { name: 'Provincia', index: 'Provincia', hidden: true },
                      { name: 'Distrito', index: 'Distrito', hidden: true }
	              ],
        height: '100%',
        pager: '#jqGrid_pager_H',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10, // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CodigoRepresentante', // Columna a ordenar por defecto.
        sortorder: 'desc',
        viewrecords: true,
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        multiselect: true
    });
    $("#jqGrid_lista_H").jqGrid('navGrid', '#jqGrid_pager_H', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_H").hide();
    $("#jqGrid_lista_H").setGridWidth($(window).width() - 150);
}

//****************************************************************
// Función		:: 	fn_ConfigurarGrillaRepresentantesInterbank
// Descripción	::	Configura la estructura de la grilla jqGrid_lista_C, para los representantes del banco.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ConfigurarGrillaRepresentantesInterbank() {
    $("#jqGrid_lista_C").jqGrid({
        datatype: function() {
            fn_ListaRepresentantesBanco();
        },
        jsonReader: {//Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodigoRepresentante" // Índice de la columna con la clave primaria.
        },
        colNames: ['', 'DNI', 'Representante', 'Departamento / Provincia / Distrito', '', '', ''],
        colModel: [
                      { name: 'CodigoRepresentante', index: 'CodigoRepresentante', hidden: true },
                      { name: 'NroDocumento', index: 'NroDocumento', width: 50, sorttype: "string", align: "left" },
		              { name: 'NombreRepresentante', index: 'NombreRepresentante', width: 300, sorttype: "string", align: "left" },
		              { name: 'UbigeoNombre', index: 'UbigeoNombre', width: 200, sorttype: "string", align: "left", formatter: fn_Ubigeo, sortable: false },
                      { name: 'Departamento', index: 'Departamento', hidden: true },
                      { name: 'Provincia', index: 'Provincia', hidden: true },
                      { name: 'Distrito', index: 'Distrito', hidden: true }
                  ],
        height: '100%',
        pager: '#jqGrid_pager_C',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10, // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CodigoRepresentante', // Columna a ordenar por defecto.
        sortorder: 'desc', // Criterio de ordenación por defecto.
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        multiselect: false
    });
    $("#jqGrid_lista_C").jqGrid('navGrid', '#jqGrid_pager_C', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_C").hide();
    $("#jqGrid_lista_C").setGridWidth($(window).width() - 150);
}

//****************************************************************
// Función		:: 	fn_Ubigeo
// Descripción	::	Devuelve el ubigeo, a partir de los nombres de la departamento, provincia y distrito de la fila correspondiente.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_Ubigeo(cellvalue, options, rowObject) {
    var ubigeo = "";

    if (!(IsNullOrEmpty(rowObject.Departamento) || IsNullOrEmpty(rowObject.Provincia) || IsNullOrEmpty(rowObject.Distrito))) {
        ubigeo = rowObject.Departamento + " / " + rowObject.Provincia + " / " + rowObject.Distrito;
    } else if (IsNullOrEmpty(rowObject.Departamento)) {
        ubigeo = "";
    } else if (IsNullOrEmpty(rowObject.Provincia)) {
        ubigeo = rowObject.Departamento;
    } else if (IsNullOrEmpty(rowObject.Distrito)) {
        ubigeo = rowObject.Departamento + " / " + rowObject.Provincia;
    }

    return ubigeo;
}

//****************************************************************
// Función		:: 	fn_ListaCondicionesAdicionales
// Descripción	::	Devuelve una lista de representantes del banco para la grilla jqGrid_lista_E.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ListaRepresentantesBanco() {
    var arrParametros = [
                         "pPageSize", fn_util_getJQGridParam("jqGrid_lista_C", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_C", "page"), // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_C", "sortname"), // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_C", "sortorder"), // Criterio de ordenación
                         "pNumeroContrato", $('#hddCodigoContrato').val(),
                         "pCodigoTipoRepresentante", CodigoTipoRepresentante.Banco,
                         "pFields", "CodigoRepresentante|NroDocumento|NombreRepresentante|Departamento|Provincia|Distrito"
                         ];

    fn_util_AjaxWM("frmContratoVer.aspx/ListaRepresentantes",
                   arrParametros,
                   function(jsondata) {
                       jqGrid_lista_C.addJSONData(jsondata);
                   },
                   function(request) {
                       fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                   }
                   );
}

//****************************************************************
// Función		:: 	fn_ConfigurarCondicionesAdicionales
// Descripción	::	Configura la para la grilla jqGrid_lista_E, para las condiciones adicionales.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ConfigurarCondicionesAdicionales() {
    $("#jqGrid_lista_E").jqGrid({
        datatype: function() {
            fn_ListaCondicionesAdicionales();
        },
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodigoContratoDocumento" // Índice de la columna con la clave primaria.
        },
        colNames: ['', 'Condiciones Adicionales', 'Adjunto', 'Obs. Comercial', 'Obs. Legal', 'Texto Predef.'],
        colModel: [
                   { name: 'CodigoContratoDocumento', index: 'CodigoContratoDocumento', hidden: true },
                   { name: 'Descripcion', index: 'Descripcion', width: 250, align: "left", sorttype: "string" },
                   { name: 'VerAdjunto', index: 'VerAdjunto', width: 50, align: "center", sortable: false, formatter: fnVerAdjunto },
                   { name: 'lupa', index: 'lupa', width: 50, align: "center", sortable: false, formatter: lupa5 },
                   { name: 'lupa', index: 'lupa', width: 50, align: "center", sortable: false, formatter: lupa4 },
                   { name: 'TextoPredef', index: 'TextoPredef', width: 50, align: "center", sortable: false, formatter: fn_textoPredef }
                  ],
        height: '100%',
        pager: '#jqGrid_pager_E',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10, // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'CodigoContratoDocumento',
        sortorder: 'desc',
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        altclass: 'gridAltClass'
    });
    function fnVerAdjunto(cellvalue, options, rowObject) {
        if (rowObject.adjunto != "") {
            var strNombreArchivo = rowObject.adjunto.split('\\').pop();
            
            strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
            return "<img src='../Util/images/ico_download.gif' alt='Descargar/Mostrar Archivo' title='" + strNombreArchivo + "' style='cursor:pointer;'/>";
        } else {
            return ".";
        }
    };
    function fn_textoPredef(cellvalue, options, rowObject) {
        if (TipoTextoPredefinido.indexOf(rowObject.CodigoTipoCondicion) != -1) {
            return "<img src='../Util/images/ico_acc_editar.gif' alt='" + cellvalue + "' title='Modificar Texto Predefinido' width='20px' style='cursor: pointer;cursor: hand;' />";
        } else {
            return "";
        }
    };
    function lupa4(cellvalue, options, rowObject) {
        if (rowObject.FlagObservacionLegal == 0) {
            return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='' style='cursor: pointer;cursor: hand;' />";
        } else {
            return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
        }
    };
    function lupa5(cellvalue, options, rowObject) {
        if (rowObject.Flagobservacion == 0) {
            return "<img src='../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='' style='cursor: pointer;cursor: hand;' />";
        } else {
            return "<img src='../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' style='cursor: pointer;cursor: hand;' />";
        }
    };
    $("#jqGrid_lista_E").jqGrid('navGrid', '#jqGrid_pager_E', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_E").hide();
    $("#jqGrid_lista_E").setGridWidth($(window).width() - 150);
}