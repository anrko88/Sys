var CrLf = 1;
var bFirstClick;

var DatosIniciales = new Array();

var TipoDatoNotarial = new Object();
TipoDatoNotarial.DatoNotarial = '001';
TipoDatoNotarial.Adenda = '002';

var DestinoCredito_Inmueble =   ["002"];
var DestinoCredito_Maquinaria = ["003", "004", "005", "006"];
var DestinoCredito_UTT        = ["006"];
var DestinoCredito_Otros =      ["008", "007"];
var DestinoCredito = new Object();
DestinoCredito.Inmueble = "Inmueble";
DestinoCredito.Utt = "UTT";
DestinoCredito.Maquinaria = "Maquinaria";
DestinoCredito.Otros = "Otros";
var destinoCredito;

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
var TipoTextoPredefinido = ["006", "010"];  // Fianza Solidaria  // Cesión de Posición Contractual
var strTipoDocumentoDNI  = "1";
var strTipoDocumentoRUC  = "2";
var strTipoDocumentoCarnetEx  = "3";
var strTipoDocumentoPasaporte = "5";
var strTipoDocumentoOtroDoc   = "6";

var intPaginaActual = 1;

var sTrace = new StringBuilderEx();
var inicio = new Date();
var fin;
//****************************************************************
// Función		:: 	JQUERY - Documento listo
// Descripción	::	Contiene métodos a ejecutarse una vez cargado 
//					el documento
// Log			:: 	JRC - 10/02/2012
//****************************************************************
$(document).ready(function() {    
    sTrace.append("document.ready" + $.format.date(inicio.toString(), "hh:mm:ss") + ":" + inicio.getMilliseconds() + "\n");

    bFirstClick = false;
    
    //Valida Campos.
    fn_inicializaCampos();
    
    // Establece la configuración inicial de los controles, incluyendo las grillas.
    fn_InicializarControles();

    // Asocia los eventos a los controles.
    fn_InicializarEventos();

    // Lee los datos de los controles ocultos y los usa para seleccionar los controles html select.
    fn_LeerDatos();

    // Muestra u oculta los formularios de los bienes, de acuerdo a la clasificación del contrato.
    fn_configurar_PanelesBienes();

    fn_configurar_BarraHerramientas();

    fn_LeerDatosIniciales();

    // Valida Bloqueo
    fn_ValidaBloqueo();
	//Valida Retorno
    var strCodigoEstadoContrato = $("#hddCodigoEstadoContrato").val();    
    if( strCodigoEstadoContrato != CodigoEstadoContrato.EnElaboracion && strCodigoEstadoContrato != CodigoEstadoContrato.PendienteDeCarta && strCodigoEstadoContrato != CodigoEstadoContrato.EnviadoAlCliente && strCodigoEstadoContrato != CodigoEstadoContrato.PendienteDeFirma ){
		$("#dv_btnRetornaFlujo").hide();
    }
    
    // On load Page (siempre al final).
    fn_onLoadPage();
});

$(window).load(function() {
    fn_ConfigurarGrillaRepresentantesCliente();
    
    fn_ConfigurarGrillaBienes();
    fn_ConfigurarGrillaContratoProveedor();

    fn_ConfigurarGrillaDocumentos();
    fn_cargaGrillaDocumentos();

    fn_ConfigurarGrillaRepresentantesInterbank();
    
    fn_ConfigurarGrillaGastos();
	//GCCTS_AEP_20130212 - Se agregó a la condicion para que sea igual a vigente o formalizado
   // if (($("#hddCodigoEstadoContrato").val() == CodigoEstadoContrato.Formalizado) ||(($("#hddCodigoEstadoContrato").val() == CodigoEstadoContrato.Vigente) )) {
        fn_ConfigurarGrillaAdenda();
    //}
   
    fn_cargaGrillaCronograma();
    
    //Cronograma      
           // fn_llenaGrillaCronograma();
        //$("#tab7-tab").css("display", "block");
        //$("div#divTabs").tabs("enable", [6]);
    fin = new Date();
});

//****************************************************************
// Funcion		:: 	fn_cargaGrilla
// Descripción	::	Carga Grilla
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_cargaGrillaCronograma() {

    $("#jqGrid_lista_L").jqGrid({
        datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_L", "page");
           // fn_paginaCronograma();
        	fn_llenaGrillaCronograma();
        },
        jsonReader:
        {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['#', 'Fec.Venc.', 'Días', 'Saldo Capital', 'Interés', 'Principal', 'Saldo Capital Seguro', 'Principal Seguro', 'Intereses Seguro', 'IGV', 'Total a Pagar','Estado','Fecha de Pago',''],
        colModel: [
		        { name: 'Numerocuota', index: 'Numerocuota', width: 40, align: "center" },
		        { name: 'SFechavencimiento', index: 'Fechavencimiento', align: "center", formatter: Fn_util_ValidaFechaVacia },
		        { name: 'Cantdiascuota', index: 'Cantdiascuota', align: "center" },
		        { name: 'SMontosaldoadeudado', index: 'Montosaldoadeudado', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontointeresbien', index: 'Montointeresbien', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'SMontoprincipalbien', index: 'Montoprincipalbien', align: "right", formatter: Fn_util_ValidaMontoNull },
		        
		        { name: 'SSaldoseguro', index: 'SSaldoseguro', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'SPrincipalsegurobien', index: 'SPrincipalsegurobien', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'SInteressegurobien', index: 'SInteressegurobien', align: "right", formatter: Fn_util_ValidaMontoNull },
		        
		        { name: 'SMontototalcuotaigv', index: 'Montototalcuotaigv', align: "right", formatter: Fn_util_ValidaMontoNull },
		        { name: 'STotalapagar', index: 'Totalapagar', width: 120, align: "right", formatter: Fn_util_ValidaMontoNull },
        	    { name: 'EstadoCuotaCalendario', index: 'EstadoCuotaCalendario',  align: "right"},
                { name: 'FechaCancelacionCuota', index: 'FechaCancelacionCuota',  align: "right"},
		        { name: 'AA', index: 'AA', width: 9, align: "right"}
        ],
        width: glb_intWidthPantalla-120,
        height: '100%',
        pager: '#jqGrid_pager_C',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 50,
        //rowList: [10, 20, 30],
        sortname: 'Codigocotizacion',
        sortorder: 'asc',
        viewrecords: true,
        gridview: true,
        //autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        onSelectRow: function(id) { }
      //  ondblClickRow: function(id) { },
//        gridComplete: function(id) {
//            fn_validaColumnasCronograma();
//        }
    });
    jQuery("#jqGrid_lista_L").jqGrid('navGrid', '#jqGrid_lista_L', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_L").hide();
    
}

//****************************************************************
// Funcion		:: 	fn_cargaGrillaCronograma
// Descripción	::	Abre Modal de Motivo de Rechazo
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_llenaGrillaCronograma() {

    try {

        var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_L", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", intPaginaActual,    // Página actual.
                             "pSortColumn", "Numerocuota", // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_L", "sortorder"), // Criterio de ordenación.
        	                 "pstrNroCotizacion", $("#hddCodigoCotizacion").val(),
                             "pstrVersionCotizacion", $("#hddVersionCotizacion").val(),
    	                     "pstrNumeroContrato", $('#hddCodigoContrato').val()
                            ];

        fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/ListadoCotizacionCronograma",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_L.addJSONData(jsondata);                    
                    fn_doResize();
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

    } catch (ex) {
        fn_util_alert(ex.message);
    }

}


//****************************************************************
// Funcion		:: 	fn_paginaCronograma
// Descripción	::	Pagina Cronograma
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_paginaCronograma() {

    try {

        var arrParametros = ["pstrPaginaActual", fn_util_getJQGridParam("jqGrid_lista_L", "page")];

        fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/PaginaCronograma",
                arrParametros,
                function(jsondata) {
                    jqGrid_lista_L.addJSONData(jsondata);                    
                    fn_doResize();
                },
                function(request) {
                    fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                }
        );

    } catch (ex) {
        fn_util_alert(ex.message);
    }

}

//************************************************************
// Función		:: 	fn_LeerDatosIniciales
// Descripción 	:: 	Lee los datos iniciales de los elementos del contrato que pueden establecer su estado a modificación.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_LeerDatosIniciales() {
    trace("fn_LeerDatosIniciales-start");
    DatosIniciales[0] =  $("#txtNroContrato").val();
    DatosIniciales[1] =  $("#cmbClasificacionContrato").val();
    DatosIniciales[2] =  $("#hddCodigoEstadoContrato").val();
    DatosIniciales[3] =  $("#txtFechaRegistroPublico").val();
    DatosIniciales[5] =  $("#cmbEstadoCivil").val();
    DatosIniciales[6] =  $("#txtNombreConyuge").val();
    DatosIniciales[7] =  $("#cmbTipoDocumentoConyuge").val();
    DatosIniciales[8] =  $("#txtnumerodocumento").val();
    DatosIniciales[9] =  $("#txtImporteAtrasoPorc").val();
    DatosIniciales[10] = $("#txtaOtrasPenalidades").val();
    DatosIniciales[11] = $("#txtdiasVencimiento").val();
    DatosIniciales[12] = $("#txtPorcentajeCuota").val();
    trace("fn_LeerDatosIniciales-end");
}

//************************************************************
// Función		:: 	fn_huboCambios
// Descripción 	:: 	Evalua los valores actuales en los controles con los valores originales para verificar si hubo o no cambios en los datos.
// Log			:: 	    - 10/02/2012
//************************************************************
function fn_huboCambios() {
   
    if (DatosIniciales[0] !=  $("#txtNroContrato").val() ||
        DatosIniciales[1] !=  $("#cmbClasificacionContrato").val() ||
        DatosIniciales[2] !=  $("#hddCodigoEstadoContrato").val() ||
        DatosIniciales[3] !=  $("#txtFechaRegistroPublico").val() ||
        DatosIniciales[5] !=  $("#cmbEstadoCivil").val() ||
        DatosIniciales[6] !=  $("#txtNombreConyuge").val() ||
        DatosIniciales[7] !=  $("#cmbTipoDocumentoConyuge").val() ||
        DatosIniciales[8] !=  $("#txtnumerodocumento").val() ||
        DatosIniciales[9] !=  $("#txtImporteAtrasoPorc").val() ||
        DatosIniciales[10] != $("#txtaOtrasPenalidades").val() ||
        DatosIniciales[11] != $("#txtdiasVencimiento").val() ||
        DatosIniciales[12] != $("#txtPorcentajeCuota").val()) {
         return true;
     } else {
         return false;
     }
}

//************************************************************
// Función		:: 	fn_configurar_PanelesBienes
// Descripción 	:: 	Muestra u oculta los botones de la barra de herramientas, según el estado del contrato.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_configurar_BarraHerramientas() {
    trace("fn_configurar_BarraHerramientas-start");
    switch (fn_util_trim($("#hddCodigoEstadoContrato").val())) {
        case CodigoEstadoContrato.EnElaboracion:
            $("#dv_aprobarContrato").hide();
            $("#dv_FirmarContrato").hide();
            $("#dv_img_EnviarNotaria").hide();
            break;
        case CodigoEstadoContrato.PendienteDeCarta:
            $("#dv_aprobarContrato").hide();
            $("#dv_img_GuardarEnviar").hide();
            $("#dv_FirmarContrato").hide();
            $("#dv_img_GuardarEnviar").hide();
            $("#dv_img_EnviarNotaria").hide();
            break;
        case CodigoEstadoContrato.EnviadoAlCliente:
            $("#dv_FirmarContrato").hide();
            $("#dv_img_GuardarEnviar").hide();
            $("#dv_img_EnviarNotaria").hide();
            break;
        case CodigoEstadoContrato.PendienteDeFirma:
            $("#dv_aprobarContrato").hide();
            $("#dv_img_GuardarEnviar").hide();
            $("#dv_img_EnviarNotaria").hide();
            $("#dv_FirmarContrato").show();
            break;
        case CodigoEstadoContrato.Formalizado:
            $("#dv_aprobarContrato").hide();
            $("#dv_FirmarContrato").hide();
            $("#dv_img_GenerarContrato").hide();
            $("#dv_AdjuntarArchivoContrato").hide();
            $("#dv_img_GuardarEnviar").hide();
            $("#dv_img_EnviarNotaria").hide();
            break;
        case CodigoEstadoContrato.PendienteDeEnvio:
            $("#dv_aprobarContrato").hide();
            $("#dv_FirmarContrato").hide();
            $("#dv_img_GenerarContrato").show();
            $("#dv_AdjuntarArchivoContrato").show();
            $("#dv_img_GuardarEnviar").hide();
            $("#dv_img_EnviarNotaria").show();
            break;
    }
    trace("fn_configurar_BarraHerramientas-end");
}

//************************************************************
// Función		:: 	fn_AdjuntarAdenda
// Descripción 	:: 	Le permite adjuntar el archivo de adenda, evaluando si es una operación edición o agregar un nuevo
//                  registro.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_AdjuntarAdenda() {
    if ($("#hdnCodigoAdenda").val() == "") {
        fn_AdjuntarArchivoDocumento('ArchivoNotarialNuevo');
    } else {
        fn_AdjuntarArchivoDocumento('ArchivoNotarialEditar');
    }
}

//************************************************************
// Función		:: 	fn_AdjuntarArchivoDocumento
// Descripción 	:: 	Le permite adjuntar un archivo.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_AdjuntarArchivoDocumento(boton) {

    var strNumeroContrato = $("#txtNroContrato").val();
    var controlArchivo;
    var sTitulo = "Formalización";
    var sSubTitulo = "";

    switch (boton) {
        // Contrato (Anexos)
        case "ArchivoContratoAdjunto":
            boton = "btnAdjuntarArchivo";
            controlArchivo = "hddAdjuntarArchivo";
            sSubTitulo = "Contrato :: Adjuntar Contrato";
            break;
        // Datos del conyugue
        case "ArchivoDocumentoSeparacion":
            boton = "btnAdjuntarArchivoDocumentoSeparacion";
            controlArchivo = "hddAdjuntarArchivoDocumentoSeparacion";
            sSubTitulo = "Contrato :: Adjuntar Documento de Separación";
            break;
        // Otros conceptos 
        case "CorreoAdjunto":
            boton = "btnAdjuntarArchivoOtroConcepto";
            controlArchivo = "hddAdjuntarArchivoOtroConcepto";
            sSubTitulo = "Contrato :: Adjuntar Correo";
            break;
        // Adenda - nuevo
        case "ArchivoNotarialNuevo":
            boton = "btnAdjuntarArchivoNotarialNuevo";
            controlArchivo = "hddAdjuntarArchivoNotarialNuevo";
            sSubTitulo = "Contrato :: Archivo Notarial - Nuevo";
            break;
        // Adenda - editar
        case "ArchivoNotarialEditar":
            boton = "btnAdjuntarArchivoNotarialEditar";
            controlArchivo = "hddAdjuntarArchivoNotarialEditar";
            sSubTitulo = "Contrato :: Archivo Notarial - Editar";
            break;
        default:
            boton = "";
            controlArchivo = "";
            break;
    }
   
    parent.fn_util_AbreModal(sSubTitulo, "Formalizacion/frmSubirArchivo.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodContrato=" + strNumeroContrato + "&Add=False&hddControl=" + boton + "&hddOp=" + controlArchivo, 550, 185, function() { });
}

//************************************************************
// Función		:: 	fn_ActualizarTextoPredefinido.
// Descripción 	:: 	Actualiza el estado del contrato cuando se agrega o modifica el texto predefinido.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_ActualizarTextoPredefinido() {
    $("#hddFlagModificado").val("1");
    $("#hddGeneraContrato_Adjunto").val("0");
    
    parent.fn_unBlockUI();
}

//************************************************************
// Función		:: 	fn_ActualizarArchivoNotarialEditar.
// Descripción 	:: 	Le permite subir un archivo de adenda a un documento notarial existente.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_ActualizarArchivoNotarialEditar() {
    
    var strRutaArchivo = $("#hddAdjuntarArchivoNotarialEditar").val();
    var strNombreArchivo = $("#hddAdjuntarArchivoNotarialEditar").val().split('\\').pop();
    strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
    var img = "<img src='../../Util/images/ico_download.gif' alt='Descargar archivo de adenda' title='Descargar archivo de adenda' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(strRutaArchivo) + "');\" style='cursor:pointer;cursor: hand;' />";
    var lnk = "<a href='#' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(strRutaArchivo) + "');\">" + strNombreArchivo + "</a>";
    
    $("#dv_DescargarArchivoAdenda").show();
    $("#dv_DescargarArchivoAdenda").html(img + "&nbsp;&nbsp;" + lnk);
    parent.fn_unBlockUI();
}

//************************************************************
// Función		:: 	fn_ActualizarArchivoNotarialNuevo.
// Descripción 	:: 	Le permite subir un archivo de adenda a un nuevo documento notarial.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_ActualizarArchivoNotarialNuevo() {
    
    var strRutaArchivo = $("#hddAdjuntarArchivoNotarialNuevo").val();
    var strNombreArchivo = $("#hddAdjuntarArchivoNotarialNuevo").val().split('\\').pop();
    strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
    var img = "<img src='../../Util/images/ico_download.gif' alt='Descargar archivo de adenda' title='Descargar archivo de adenda' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(strRutaArchivo) + "');\" style='cursor:pointer;cursor: hand;' />";
    var lnk = "<a href='#' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(strRutaArchivo) + "');\">" + strNombreArchivo + "</a>";
   
    $("#dv_DescargarArchivoAdenda").show();
    $("#dv_DescargarArchivoAdenda").html(img + "&nbsp;&nbsp;" + lnk);
    $("#imgArchivoAdenda").removeClass("css_input_error");
    
    parent.fn_unBlockUI();
}

//************************************************************
// Función		:: 	fn_ActualizarAnexo.
// Descripción 	:: 	Le permite acceder al archivo de contrato.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_ActualizarAnexo() {

    var strRutaArchivo = $("#hddAdjuntarArchivo").val();
    var strNombreArchivo = $("#hddAdjuntarArchivo").val().split('\\').pop();
    strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
    var img = "<img src='../../Util/images/ico_download.gif' alt='Descargar archivo de correo' title='Descargar archivo de anexos' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(strRutaArchivo) + "');\" style='cursor:pointer;cursor: hand;' />";
    var lnk = "<a href='#' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(strRutaArchivo) + "');\">" + strNombreArchivo + "</a>";
  
    $("#dv_DescargarArchivoContrato").show();
    $("#dv_DescargarArchivoContrato").html(img + "&nbsp;&nbsp;" + lnk);
    $("#hddGeneraContrato_Adjunto").val("1");
    parent.fn_unBlockUI();
}

//************************************************************
// Función		:: 	fn_ActualizarArchivoAdjunto (OtroConcepto - correo).
// Descripción 	:: 	Le permite adjuntar el archivo de correo anexo.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_ActualizarArchivoAdjunto() {
    var strRutaArchivo = $("#hddAdjuntarArchivoOtroConcepto").val();
    var strNombreArchivo = $("#hddAdjuntarArchivoOtroConcepto").val().split('\\').pop();
    strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
    var img = "<img src='../../Util/images/ico_download.gif' alt='Descargar archivo de correo' title='Descargar archivo de correo' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(strRutaArchivo) + "');\" style='cursor:pointer;cursor: hand;' />";
    var lnk = "<a href='#' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(strRutaArchivo) + "');\">" + strNombreArchivo + "</a>";

    $("#hddFlagModificado").val("1");
    $("#hddGeneraContrato_Adjunto").val("0");
    $("#dv_ArchivoOtroConcepto").show();
    $("#dv_ArchivoOtroConcepto").html(img + "&nbsp;&nbsp;" + lnk);
    parent.fn_unBlockUI();
}

//************************************************************
// Función		:: 	fn_ActualizarDocumentoSeparacion
// Descripción 	:: 	Le permite adjuntar un archivo
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_ActualizarDocumentoSeparacion() {
    var strRutaArchivo = $("#hddAdjuntarArchivoDocumentoSeparacion").val();
    var strNombreArchivo = $("#hddAdjuntarArchivoDocumentoSeparacion").val().split('\\').pop();
    strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);

    var img = "<img src='../../Util/images/ico_download.gif' alt='Descargar documento de separación.' title='Descargar documento de separación.' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(strRutaArchivo) + "');\" style='cursor:pointer;cursor: hand;' />";
    var lnk = "<a href='#' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(strRutaArchivo) + "');\">" + strNombreArchivo + "</a>";
 
    $("#hddFlagModificado").val("1");
    $("#hddGeneraContrato_Adjunto").val("0");
    $("#dv_AdjuntarArchivoDocumentoSeparacion").html(img + "&nbsp;&nbsp;" + lnk);
    
    parent.fn_unBlockUI();
}

//************************************************************
// Función		:: 	fn_configurar_PanelesBienes
// Descripción 	:: 	Configura las distintas ventanas de mantenimiento de los bienes.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_configurar_PanelesBienes() {
  
    
    switch(destinoCredito) {    
        // Controles Bien Inmueble
        case DestinoCredito.Inmueble:
            $("#dvDatosMaquinaria").hide();
            $("#dvDatosOtros").hide();
        	 $("#dvDatosUTT").hide();
            break;
        // Controles obligatorios maquinaria
        case DestinoCredito.Maquinaria:
            $("#dvDatosBien").hide();
            $("#dvDatosOtros").hide();
        	 $("#dvDatosUTT").hide();
            break;
        case DestinoCredito.Utt:
            $("#dvDatosBien").hide();
            $("#dvDatosOtros").hide();
        	$("#dvDatosMaquinaria").hide();
            break;
    // Datos Otros 
    default:
        $("#dvDatosBien").hide();
        $("#dvDatosMaquinaria").hide();
    	$("#dvDatosUTT").hide();
    }
   
}

//************************************************************
// Función		:: 	fn_HabilitarControlesBienInmueble
// Descripción 	:: 	Habilita los controles que del matenimeinto de bienes inmuebles.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_HabilitarControlesBienInmueble() {

    //$("#cmbDepartamentoInmueble option:first").attr('selected', 'selected');
    //$("#cmbProvinciaInmueble option:first").attr('selected', 'selected');
    //fn_LimpiaComboDistritOProvincia("#cmbProvinciaInmueble");
    //$("#cmbDistritoInmueble option:first").attr('selected', 'selected');
    //fn_LimpiaComboDistritOProvincia("#cmbDistritoInmueble");
    $("#txtDescripcionInmueble").html("");
    $("#txtCantidadInmueble").html("");
    $("#txtPartidaRegistralInmueble").html("");
    $("#txtOficinaRegistralInmueble").html("");
    $("#cmbDepartamentoInmueble").html(""); 
    $("#cmbProvinciaInmueble").html(""); 
    $("#cmbDistritoInmueble").html("");   
    $("#cmbEstadoBienInmueble").html(""); 
	
    $("#txtUsoInmueble").html($("#hddUso").val());
    $("#txtUbicacionInmueble").html($("#hddUbicacion").val());
    
    //fn_seteaCamposObligatorios();
}

//****************************************************************
// Función		:: 	fn_LeerDatos
// Descripción	::	Lee los datos de los controles ocultos y los usa para seleccionar los controles html y
//                  configurar y visualizar los controles de manipulación de grillas.
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_LeerDatos() {
    trace("fn_LeerDatos-start");
    var rutaArchivo;
    var nombreArchivo;

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

    var numerodocumento = $("#txtnumerodocumento").val();
    //$('#cmbTipoDocumentoConyuge').change();
    $("#txtnumerodocumento").val(numerodocumento);

    // Archivo contrato, los anexos
    var adjuntarArchivoContrato;
    // Caso persona jurídica
    if ($("#hddCodigoTipoPersona").val() == CodigoTipoPersona.Juridica) {
        rutaArchivo = $("#hddAdjuntarArchivo").val();
        nombreArchivo = $("#hddAdjuntarArchivo").val().split('\\').pop();
        nombreArchivo = nombreArchivo.substr(28, nombreArchivo.length);

        adjuntarArchivoContrato = "<a href=\"javascript:fn_AdjuntarArchivoDocumento('ArchivoContratoAdjunto');\">";
        adjuntarArchivoContrato = adjuntarArchivoContrato + "\n<img style=\"cursor: pointer;cursor: hand;width: 35px;height: 35px;border: 0;\" id=\"imgArchivoContratoAdjunto\" title=\"Adjuntar correo\" alt=\"\" src=\"../../Util/images/ico_acc_adjuntarMini.gif\" />";
        adjuntarArchivoContrato = adjuntarArchivoContrato + "\n<br />Adjuntar Contrato";
        adjuntarArchivoContrato = adjuntarArchivoContrato + "\n</a>";
        $("#dv_AdjuntarArchivoContrato").html(adjuntarArchivoContrato);

        if ($("#hddAdjuntarArchivo").val() != "") {
            $("#dv_DescargarArchivoContrato").html("<img src='../../Util/images/ico_download.gif' alt='Descargar archivo de anexo' title='Descargar archivo de anexo' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rutaArchivo) + "');\" style='cursor:pointer;cursor: hand;border: 0;' />&nbsp;&nbsp;<a href='#'  onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rutaArchivo) + "');return false;\">" + nombreArchivo + "</a>");
        }

    } else {   
        // Caso persona natural
        // Las persona naturales pueden adjuntar archivos sin haber generado previamente el archivo de contrato.
        adjuntarArchivoContrato = "<a href=\"javascript:fn_AdjuntarArchivoDocumento('ArchivoContratoAdjunto');\">";
        adjuntarArchivoContrato = adjuntarArchivoContrato + "\n<img style=\"cursor: pointer;cursor: hand;width: 35px;height: 35px;border: 0;\" id=\"imgArchivoContratoAdjunto\" title=\"Adjuntar correo\" alt=\"\" src=\"../../Util/images/ico_acc_adjuntarMini.gif\" />";
        adjuntarArchivoContrato = adjuntarArchivoContrato + "\n<br />Adjuntar Contrato";
        adjuntarArchivoContrato = adjuntarArchivoContrato + "\n</a>";
        $("#dv_AdjuntarArchivoContrato").html(adjuntarArchivoContrato);
        
        if ($("#hddAdjuntarArchivo").val() != "") {
            rutaArchivo = $("#hddAdjuntarArchivo").val();
            nombreArchivo = $("#hddAdjuntarArchivo").val().split('\\').pop();
            nombreArchivo = nombreArchivo.substr(28, nombreArchivo.length);

            $("#dv_DescargarArchivoContrato").html("<img src='../../Util/images/ico_download.gif' alt='Descargar archivo de anexo' title='Descargar archivo de anexo' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rutaArchivo) + "');\" style='cursor:pointer;cursor: hand;' />&nbsp;&nbsp;<a href='#'  onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rutaArchivo) + "');return false;\">" + nombreArchivo + "</a>");
        } else {
            $("#dv_DescargarArchivoContrato").html("");
        }
    }
    
    // Correo de otros conceptos.
    if ($("#hddAdjuntarArchivoOtroConcepto").val() != "") {
        rutaArchivo = $("#hddAdjuntarArchivoOtroConcepto").val();
        nombreArchivo = $("#hddAdjuntarArchivoOtroConcepto").val().split('\\').pop();
        nombreArchivo = nombreArchivo.substr(28, nombreArchivo.length);

        $("#dv_ArchivoOtroConcepto").show();
        $("#dv_ArchivoOtroConcepto").html("<img style=\"cursor: pointer;cursor: hand;\" id=\"imgArchivoOtroConcepto\" src=\"../../Util/images/ico_download.gif\" alt=\"Descargar correo adjunto\" alt=\"\" title=\"Descargar correo adjunto\" onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rutaArchivo) + "')\" />&nbsp;&nbsp;<a href='#'  onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rutaArchivo) + "');return false;\">" + nombreArchivo + "</a>");
    } else {
        $("#dv_ArchivoOtroConcepto").hide();
        $("#dv_ArchivoOtroConcepto").html("");
    }
    
    // Documento de separación
    if ($("#hddAdjuntarArchivoDocumentoSeparacion").val() != "") {
        rutaArchivo = $("#hddAdjuntarArchivoDocumentoSeparacion").val();
        nombreArchivo = $("#hddAdjuntarArchivoDocumentoSeparacion").val().split('\\').pop();
        nombreArchivo = nombreArchivo.substr(28, nombreArchivo.length);
        $("#dv_AdjuntarArchivoDocumentoSeparacion").html("<img style=\"cursor: pointer;cursor: hand;\" id=\"imgArchivoOtroConcepto\" src=\"../../Util/images/ico_download.gif\" alt=\"Descargar correo adjunto\" alt=\"\" title=\"Descargar correo adjunto\" onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rutaArchivo) + "')\" />&nbsp;&nbsp;<a href='#'  onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rutaArchivo) + "');return false;\">" + nombreArchivo + "</a>");
    }

    // Ocultar datos del conyugue si no es persona natural y estado civil casado.
    if ($("#hddTipoDocumento").val() == CodigoTipoPersona.Natural 
        && $("#hddCodigoEstadoCivil").val() == CodigoEstadoCivil.Casado) {
        $("#fs_DatosConyugue").show();
    } else {
        $("#fs_DatosConyugue").hide();
    }

    // Ocultar estado civil si es persona jurídica
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
    // Configurar fecha firma en notaria.
    if ($("#hddCodigoEstadoContrato").val() != CodigoEstadoContrato.PendienteDeFirma) {
        $("#chkFirmaNotaria").attr('disabled', 'disabled');
    }
    // Configura la fecha de registros públicos.
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

    // Departamento notaria
    $("#cmbDepartamento").val(Departamento.Lima);
  //  fn_cargaComboProvincia('#cmbProvincia2', '#cmbDistrito2', $("#cmbDepartamento").val());
    
    //fn_cargaComboNotaria($("#cmbDepartamento").val(), '#cmbNotariaDatoNotarial');
   // fn_cargaComboNotaria($("#cmbDepartamento").val(), 0, '#cmbNotariaDatoNotarial');


    if ($("#hddCodigoEstadoContrato").val() === CodigoEstadoContrato.Formalizado) {
        $("#dv_AccionesRepresentantes").hide();
        // Máquina
        //$("#dv_ProcesoMaquina").hide();
        $("#dv_AccionesMaquina").hide();
        // Datos Inmueble
        //$("#dv_ProcesoBien").hide();
        $("#dv_AccionesBien").hide();
        // Otros bienes
        //$("#dv_AccionesDatosOtros").hide();
        $("#dv_ProcesoDatosOtros").hide();

        // Otros conceptos
        $("#txtImporteAtrasoPorc").addClass("css_input_inactivo");
        $("#txtImporteAtrasoPorc").attr("readonly", true);
        $("#txtdiasVencimiento").addClass("css_input_inactivo");
        $("#txtdiasVencimiento").attr("readonly", true);
        $("#txtPorcentajeCuota").addClass("css_input_inactivo");
        $("#txtPorcentajeCuota").attr("readonly", true);
        $("#txtaOtrasPenalidades").addClass("css_input_inactivo");
        $("#txtaOtrasPenalidades").attr("readonly", true);
        
        // Departamento adenda
        $("#cmbDepartamentoAdenda").val(Departamento.Lima);
        //fn_cargaComboProvincia('#cmbProvienciaAdenda', '#cmbDistritoAdenda', $("#cmbDepartamentoAdenda").val());
        //IBK - RPH
        //fn_cargaComboNotaria($("#cmbDepartamentoAdenda").val(), 0, '#cmbNotariaAdenda');
        //fn_cargaComboNotaria($("#cmbDepartamentoAdenda").val(), '#cmbNotariaAdenda');
    }
    
    trace("fn_LeerDatos-end");
}

//****************************************************************
// Función		:: 	fn_InicializarControles
// Descripción	::	Establece la configuración inicial de los controles, incluyendo las grillas.
// Log			:: 	EBL - 10/02/2012
//****************************************************************
function fn_InicializarControles() {
    trace("fn_InicializarControles-start");

    $("#hddCambiosSinGuardar").val("0");
    
    // Valida Datos Modificados
    $("#hddValidaModificacion").val("0");

    $("#hddGeneraContrato_Adjunto").val("0");

    $('#cmbDistrito2').hide();
    $('#lblcmbdistrito').hide();

    $("#tb_DatosInmueble").width($("#dvDatosBien").width() - 25); // inmueble
    $("#tb_DatosMaquinaria").width($("#dvDatosMaquinaria").width() - 25);
    $("#tb_DatosOtros").width($("#dvDatosOtros").width() - 25);
    
    // Valida Tabs
    $("div#divTabs").tabs({
        show: function() {
            fn_doResize();
        }
    });
    trace("fn_InicializarControles-end");
}

//****************************************************************
// Función		:: 	fn_ConfigurarGrillaBienes
// Descripción	::	Carga Grilla de los bienes del proveedor seleccionado.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ConfigurarGrillaBienes() {
    trace("fn_ConfigurarGrillaBienes-start");

    switch(destinoCredito) {    
        // Bien Inmueble
        case DestinoCredito.Inmueble:
            ConfigurarGrillaInmueble();
            break;
        // Maquinaria
        case DestinoCredito.Maquinaria:
            ConfigurarGrillaMaquinaria();
            break;
        case DestinoCredito.Utt:
            fn_cargaGrillaUnidadTransporte();
            break;	
        // Datos Otros 
        default:
        ConfigurarGrillaOtrosBienes();
    }
    trace("fn_ConfigurarGrillaBienes-end");
}

//************************************************************
// Función		:: 	fn_RefreshDatosBienes
// Descripción 	:: 	Actualiza la lista de los bienes.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_RefreshDatosBienes() {
     switch(destinoCredito) {    
             // Bien Inmueble
        case DestinoCredito.Inmueble:
            $("#jqGrid_lista_A").trigger("reloadGrid");
            break;
             // Maquinaria
        case DestinoCredito.Maquinaria:
            $("#jqGrid_lista_B").trigger("reloadGrid");
        break;
            // Datos Otros  
        default:
            $("#jqGrid_lista_J").trigger("reloadGrid");
    }
}

//************************************************************
// Función		:: 	fn_ListaDatosBienes
// Descripción 	:: 	Lista los bienes del proveedor seleccionado, cargando la grilla seleccionada.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_ListaDatosBienes() {
    trace("fn_ListaDatosBienes-start");
    switch (destinoCredito) {
        // Bien Inmueble 
        case DestinoCredito.Inmueble:
            fn_ListaBienesDatosInmueble();
            break;
        // Maquinaria 
        case DestinoCredito.Maquinaria:
            fn_ListaBienesDatosMaquina();
            break;
        // Datos Otros   
        default:
            fn_ListaBienesDatosOtros();
    }
    trace("fn_ListaDatosBienes-end");
}

function fn_ListaBienesDatosInmueble() {
    var arrParametros = [
                         "pPageSize", fn_util_getJQGridParam("jqGrid_lista_E", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_E", "page"), // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_E", "sortname"), // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_E", "sortorder"), // Criterio de ordenación
                         "pContrato", $('#hddCodigoContrato').val(),
                         "pFields", "CantidadProducto|SecFinanciamiento|Comentario|EstadoBien|Uso|Ubicacion|DepartamentoNombre|ProvinciaNombre|DistritoNombre|CodigoEstadoBien|Departamento|Provincia|Distrito|PartidaRegistral|OficinaRegistral"
                        ];
    
    fn_util_AjaxSyncWM("frmConsultaSituacionCreditoRegistro.aspx/ListarBienes",
                        arrParametros,
                        function(jsondata) {
                            jqGrid_lista_E.addJSONData(jsondata);
                        },
                        function(request) {
                            parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR AL LISTAR LOS BIENES");
                        }
                       );
}

//****************************************************************
// Funcion		:: 	fn_buscarMaquinarias
// Descripción	::	
// Log			:: 	AEP - 27/09/2012
//****************************************************************
	
function fn_buscarMaquinarias() {
	
	    var vNumeroContrato = $('#txtNroContrato').html() == undefined ? "" : $('#txtNroContrato').html();
		//var vEstadoLogico= $('#ddlEstadoBienMaquinariaOtros').val() == undefined ? "" : $('#ddlEstadoBienMaquinariaOtros').val();
	
    	 var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_B", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", intPaginaActual,    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_B", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_B", "sortorder"), // Criterio de ordenación.
                             "pNumeroContraro", vNumeroContrato,
                             "pCodEstadoLogico", '001'
                            ];
		

    fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/ListadoBienContratoMaquinaria",
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


function fn_ListaBienesDatosOtros() {
    var arrParametros = [
                         "pPageSize", fn_util_getJQGridParam("jqGrid_lista_J", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_J", "page"), // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_J", "sortname"), // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_J", "sortorder"), // Criterio de ordenación
                         "pContrato", $('#hddCodigoContrato').val(),
                         "pFields", "CantidadProducto|SecFinanciamiento|Comentario|CodigoEstadoBien|EstadoBien|Ubicacion|Uso|DepartamentoNombre|ProvinciaNombre|Marca|Modelo|Departamento|Provincia|Distrito|PartidaRegistral|OficinaRegistral|DistritoNombre"
                        ];

    fn_util_AjaxSyncWM("frmConsultaSituacionCreditoRegistro.aspx/ListarBienes",
                        arrParametros,
                        function(jsondata) {
                            jqGrid_lista_J.addJSONData(jsondata);
                        },
                        function(request) {
                            parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR AL LISTAR LOS BIENES");
                        }
                       );
}

//****************************************************************
// Función		:: 	ConfigurarGrillaInmueble
// Descripción	::	DATOS DE BIENES INMOVILIARIOS.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function ConfigurarGrillaInmueble() {
    $("#jqGrid_lista_A").jqGrid({
        datatype: function() {
            intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_A", "page");
            fn_buscarInmuebles();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['CodigoContrato','Tipo de Bien' ,'Departamento', 'Provincia', 'Distrito', 'Ubicación', 'Descripción','Valor Venta', 'Estado del Registro del Bien', 'Fecha de Baja', '', '', '', '', '', '', '', '', '', ''],
        colModel: [
			{ name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
			{ name: 'TipoBien', index: 'TipoBien', width: 100, align: "center" },
			{ name: 'DepartamentoNombre', index: 'DepartamentoNombre', width: 100, align: "center" },
			{ name: 'ProvinciaNombre', index: 'ProvinciaNombre', width: 100, align: "center" },
			{ name: 'DistritoNombre', index: 'DistritoNombre', width: 100, align: "center" },
			{ name: 'Ubicacion', index: 'Ubicacion', width: 100, align: "left" },
			{ name: 'Comentario', index: 'Comentario', width: 100, align: "left" },
        	{ name: 'ValorBien', index: 'ValorBien', width: 100, align: "left" },
			{ name: 'EstadoBienLogico', index: 'EstadoBienLogico', width: 100, align: "left" },
			{ name: 'FechaBaja', index: 'FechaBaja', width: 100, align: "left" },
			{ name: 'CodEstadoBien', index: 'CodEstadoBien', hidden: true },
			{ name: 'Departamento', index: 'Departamento', hidden: true },
			{ name: 'Provincia', index: 'Provincia', hidden: true },
			{ name: 'Distrito', index: 'Distrito', hidden: true },
			{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true },
			{ name: 'CodEstadoInscripcionRRPP', index: 'CodEstadoInscripcionRRPP', hidden: true },
			{ name: 'CodEstadoMunicipal', index: 'CodEstadoMunicipal', hidden: true },
			{ name: 'CodEstadoTransferencia', index: 'CodEstadoTransferencia', hidden: true },
			{ name: 'FlagOrigen', index: 'FlagOrigen', hidden: true },
			{ name: 'Id', index: 'Id', hidden: true }
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
        //multiselect: true,
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
            $("#hddRowId").val(id);
        
        },
        ondblClickRow: function(id) {
            parent.fn_blockUI();
            var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);


            //fn_util_redirect('frmConsultasBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + $("#hidSecFinanciamiento").val() + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#hidCodEstado").val());
            //IBK - RPH precio venta leasing
            
            fn_util_redirect('frmConsultasSituacionCreditoBienDetalle.aspx?co=1&csc=' + rowData.CodSolicitudCredito + '&csf=' + rowData.SecFinanciamiento + '&flag=' + rowData.FlagOrigen + '&codestado=' + rowData.CodEstadoBien + "&precioventa=" + $("#hidPrecioVenta").val());
            //fin
        }
    }).navGrid('#jqGrid_pager_A', { edit: false, add: false, del: false });
       $("#search_jqGrid_lista_A").hide();
	

}

//****************************************************************
// Funcion		:: 	fn_buscarInmuebles
// Descripción	::	
// Log			:: 	AEP - 27/09/2012
//****************************************************************
	
function fn_buscarInmuebles() {
	    $("#hddRowId").val('');
	    var vNumeroContrato = $('#txtNroContrato').html() == undefined ? "" : $('#txtNroContrato').html();
		
	
    	 var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_A", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", intPaginaActual,    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_A", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_A", "sortorder"), // Criterio de ordenación.
                             "pNumeroContraro", vNumeroContrato,
                             "pCodEstadoLogico", "001"
                            ];
		

    fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/ListaBienContratoInmuebles",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_A.addJSONData(jsondata);
                    	fn_doResize();
                    },
                    function(request) {
                        fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                    }
                   );
	}

//****************************************************************
// Función		:: 	fn_GuardarMaquinaNuevo
// Descripción	::	Guarda los datos ingresados de un nuevo bien (operación nuevo).
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function fn_GuardarMaquinaNuevo() {
    parent.fn_blockUI();
    
    if (EsValidoMaquina()) {
        var arrParametros = [
                             "strNroContrato",               $("#txtNroContrato").val(),
                             "strCodProveedor",              $("#hddCodProveedor").val(),
                             "strTipoRubroFinanciamiento",   $("#hddTipoRubroFinanciamiento").val(),
                             "strSerieMotorMaquina",         $("#txtSerieMotorMaquina").val(),
                             "strNumeroMotorMaquina",        $("#txtNumeroMotorMaquina").val(),
                             "strFabricacionMaquina",        $('#txtFabricacionMaquina').val(),
                             "strMarcaMaquina",              $("#txtMarcaMaquina").val(),
                             "strModeloMaquina",             $("#txtModeloMaquina").val(),
                             "strTipoCarroceriaMaquina",     fn_util_trim($("#txtTipoCarroceriaMaquina").val()),
                             "strDescripcionAutoMaquina",    $("#txtDescripcionAutoMaquina").val(),
                             "strEstadobienMaquina",         $("#cmbEstadobienMaquina").val(),
                             "strPlacaMaquina",              $("#txtPlacaMaquina").val(),
                             "strMedidasMaquina",            $("#txtMedidasMaquina").val(),
                             "intCantidadMaquina",           $("#txtCantidadMaquina").val(),
                             "strUsoBienMaquina",            $("#txtUsoBienMaquina").val(),
                             "strUbicacionBienMaquina",      $("#txtUbicacionBienMaquina").val(),
                             "strDepartamentoMaquinaria",    $("#cmbDepartamentoMaquinaria").val(),
                             "strProvinciaMaquinaria",       $("#cmbProvinciaMaquinaria").val(),
                             "strDistritoMaquinaria",        $("#cmbDistritoMaquinaria").val(),
        	                 "intFlagOrigen", "1",
        	                 "strColor", "", //IBK RPH
        	                 "strCodTipoBien", $("#hddCodigoTipoBien").val() // IBK - RPH
                            ];
        fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/GuardarMaquinaNuevo",
                       arrParametros,
                       function() {
                            $("#hddFlagModificado").val("1");
                            // Valida si hay algún cambio en contrato
                            $("#hddGeneraContrato_Adjunto").val("0");
                            $("#jqGrid_lista_B").trigger("reloadGrid");
                            fn_CancelarMaquina();
                       },
                       function(result) {
                           parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                       });
        $("#dv_AccionesMaquina").show();
        $("#dv_ProcesoMaquina").hide();

        fn_inicializaCampos();
        ConfigurarGrillaMaquinaria();
    }

    parent.fn_unBlockUI();
}

//************************************************************
// Función		:: 	fn_GuardarMaquina
// Descripción 	:: 	Guarda los datos del bien maquinaria, previa validación de los datos
// Log			:: 	EBL - 09/05/2012
//************************************************************
function fn_GuardarMaquina() {
    parent.fn_blockUI();

    if (EsValidoMaquina()) {
        // Si es una operación de agregar (no tiene número secuencial)
        if ($("#hddSecFinanciamiento").val() != "") {
             parent.fn_mdl_confirma(
		    "¿Está seguro de modificar los datos del bien?"
		    , function () {
                    fn_GuardarMaquinaEditar();
                    $("#dv_AccionesMaquina").show();
                    $("#dv_ProcesoMaquina").hide();
                    fn_inicializaCampos();
                    ConfigurarGrillaMaquinaria();
            }
		    , "util/images/question.gif"
		    , function() { }
		    , "EDITAR DE REGISTRO"
	        );
        }
    }

    parent.fn_unBlockUI();
}

//****************************************************************
// Función		:: 	ConfigurarGrillaInmueble
// Descripción	::	DATOS DEL BIEN AUTOMOVIL
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function ConfigurarGrillaMaquinaria() {
   $("#jqGrid_lista_B").jqGrid({
		datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_B", "page");	 
            fn_buscarMaquinarias();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['CodigoContrato','Tipo de Bien','Cantidad','Marca','Modelo','Nro. Serie','Descripción','Valor Venta', 'Estado del Registro del Bien', 'Fecha de Baja','','','','','','',''],
		colModel: [
			{ name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
			{ name: 'TipoBien', index: 'TipoBien', width: 100, align: "center" },
			{ name: 'Cantidad', index: 'Cantidad', width: 100, align: "center" },
			{ name: 'Marca', index: 'Marca', width: 100, align: "left" },
			{ name: 'Modelo', index: 'Modelo', width: 100, align: "left" },
			{ name: 'NroSerie', index: 'NroSerie', width: 100, align: "left" },
			{ name: 'Descripcion', index: 'Descripcion', width: 100, align: "left" },
			{ name: 'ValorBien', index: 'ValorBien', width: 100, align: "left" },
			{ name: 'EstadoBienLogico', index: 'EstadoBienLogico', width: 100, align: "left" },
			//{ name: 'FechaBaja', index: 'FechaBaja', width: 100, align: "left",formatter: fn_util_ValidaStringFecha},
			{ name: 'FechaBaja', index: 'FechaBaja', width: 100, align: "left"},
			//{ name: 'ComentarioBaja', index: 'ComentarioBaja', width: 100, align: "center",sortable:false},
//			{ name: 'EstadoBienLogico', index: 'EstadoBienLogico', width: 100, align: "left" },
			{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true},
			{ name: 'CodEstadoBien', index: 'CodEstadoBien', hidden: true},
		    { name: 'Departamento', index: 'Departamento', hidden: true },
			{ name: 'Provincia', index: 'Provincia', hidden: true},
			{ name: 'Distrito', index: 'Distrito', hidden: true},
			{ name: 'FlagOrigen', index: 'FlagOrigen', hidden: true },
			{ name: 'Id', index: 'Id', hidden: true}
		],
		height: '100%',
		pager: '#jqGrid_pager_B',
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
		//multiselect: true,
    onSelectRow: function(id) {
        var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
    	$("#hddRowId").val(id);
    	$("#hidNumeroContrato").val(rowData.CodSolicitudCredito);
        $("#hidSecFinanciamiento").val(rowData.SecFinanciamiento);
    	$("#hidFlagOrigen").val(rowData.FlagOrigen);
    	$("#hidCodEstado").val(rowData.CodEstadoBien);

    },
   	ondblClickRow: function(id) {
   	parent.fn_blockUI();
        var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);
    	$("#hddRowId").val(id);
  
   	//IBK - RPH precio venta leasing
   	fn_util_redirect('frmConsultasSituacionCreditoBienDetalle.aspx?co=1&csc=' + rowData.CodSolicitudCredito + '&csf=' + rowData.SecFinanciamiento + '&flag=' + rowData.FlagOrigen + '&codestado=' + rowData.CodEstadoBien + "&precioventa=" + $("#hidPrecioVenta").val());
   	//fin
	}     
    
	}).navGrid('#jqGrid_pager_B', { edit: false, add: false, del: false });
      $("#search_jqGrid_lista_B").hide();
	
}

//****************************************************************
// Funcion		:: 	fn_cargaGrillaUnidadTransporte
// Descripción	::	Inicializa Grilla de Unidad y Transporte
// Log			:: 	AEP - 24/09/2012
//****************************************************************
function fn_cargaGrillaUnidadTransporte() {
	$("#jqGrid_lista_C").jqGrid({
		datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_C", "page");	 
            ListaBienContratoVehiculos();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
	    colNames: ['CodigoContrato','Tipo de Bien','Cantidad','Marca','Modelo','Nº Serie','Nº Motor', 'Placa','Descripción','Valor Venta','Estado del Registro del Bien', 'Fecha de Baja','','','','',''],
		colModel: [
			{ name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
			{ name: 'TipoBien', index: 'TipoBien', width: 100, align: "center" },
			{ name: 'Cantidad', index: 'Cantidad', width: 100, align: "center" },
			{ name: 'Marca', index: 'Marca', width: 100, align: "center" },
			{ name: 'Modelo', index: 'Modelo', width: 100, align: "center" },
			{ name: 'NroSerie', index: 'NroSerie', width: 100, align: "center" },
			{ name: 'NroMotor', index: 'NroMotor', width: 100, align: "center" },
			{ name: 'Placa', index: 'Placa', width: 100, align: "left" },
			{ name: 'Comentario', index: 'Comentario', width: 100, align: "left" },
			{ name: 'ValorBien', index: 'ValorBien', width: 100, align: "left" },
			{ name: 'EstadoBienLogico', index: 'EstadoBienLogico', width: 100, align: "left" },
			{ name: 'FechaBaja', index: 'FechaBaja', width: 100, align: "left" },
			{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true},
			{ name: 'CodEstadoBien', index: 'CodEstadoBien', hidden: true},
			{ name: 'FlagOrigen', index: 'FlagOrigen', hidden: true },
			{ name: 'ValorBien', index: 'ValorBien', hidden: true },
			{ name: 'Id', index: 'Id', hidden: true }
			
		],
		height: '100%',
		loadtext: 'Cargando datos...',
		pager: '#jqGrid_pager_C',
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
		//multiselect: true,
    onSelectRow: function(id) {
        var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id);
        $("#hddRowId").val(id);
    	$("#hidNumeroContrato").val(rowData.CodSolicitudCredito);
        $("#hidSecFinanciamiento").val(rowData.SecFinanciamiento);
    	$("#hidFlagOrigen").val(rowData.FlagOrigen);
    	$("#hidCodEstado").val(rowData.CodEstadoBien);
    },
   	ondblClickRow: function(id) {
	    parent.fn_blockUI();
		var rowData = $("#jqGrid_lista_C").jqGrid('getRowData', id);
		//fn_util_redirect('frmConsultasBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + $("#hidSecFinanciamiento").val() + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#hidCodEstado").val());
		//IBK - RPH precio venta leasing
   	fn_util_redirect('frmConsultasSituacionCreditoBienDetalle.aspx?co=1&csc=' + rowData.CodSolicitudCredito + '&csf=' + rowData.SecFinanciamiento + '&flag=' + rowData.FlagOrigen + '&codestado=' + rowData.CodEstadoBien + "&precioventa=" + $("#hidPrecioVenta").val());
		//fin
	}     
	}).navGrid('#jqGrid_pager_C', { edit: false, add: false, del: false });
      $("#search_jqGrid_lista_C").hide();
	
 
	
}




//****************************************************************
// Funcion		:: 	ListaBienContratoVehiculos
// Descripción	::	
// Log			:: 	AEP - 27/09/2012
//****************************************************************
	
function ListaBienContratoVehiculos() {
	
	    var vNumeroContrato = $('#txtNroContrato').html() == undefined ? "" : $('#txtNroContrato').html();
		
	
    	 var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_C", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", intPaginaActual,    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_C", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_C", "sortorder"), // Criterio de ordenación.
                             "pNumeroContraro", vNumeroContrato,
                             "pCodEstadoLogico", "001"
                            ];
		

    fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx.aspx/ListaBienContratoVehiculos",
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
// Función		:: 	ConfigurarGrillaOtrosBienes
// Descripción	::	DATOS DE OTROS BIENES
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function ConfigurarGrillaOtrosBienes() {
    $("#jqGrid_lista_J").jqGrid({
		datatype: function() {
        	intPaginaActual = fn_util_getJQGridParam("jqGrid_lista_J", "page");	 
            fn_buscarSistemasOtros();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage",
            total: "PageCount",
            records: "RecordCount",
            repeatitems: false,
            id: "Id"
        },
        colNames: ['CodigoContrato','Tipo de Bien','Cantidad','Marca','Modelo','Nro. Serie','Descripción','Valor Venta','Estado del Registro del Bien', 'Fecha de Baja','','','','','','',''],
		colModel: [
			{ name: 'CodSolicitudCredito', index: 'CodSolicitudCredito', hidden: true },
			{ name: 'TipoBien', index: 'TipoBien', width: 100, align: "center" },
			{ name: 'Cantidad', index: 'Cantidad', width: 100, align: "center" },
			{ name: 'Marca', index: 'Marca', width: 100, align: "left" },
			{ name: 'Modelo', index: 'Modelo', width: 100, align: "left" },
			{ name: 'NroSerie', index: 'NroSerie', width: 100, align: "left" },
			{ name: 'Descripcion', index: 'Descripcion', width: 100, align: "left" },
			{ name: 'ValorBien', index: 'ValorBien', width: 100, align: "left" },
			{ name: 'EstadoBienLogico', index: 'EstadoBienLogico', width: 100, align: "left" },
			{ name: 'FechaBaja', index: 'FechaBaja', width: 100, align: "left" },
//			{ name: 'EstadoBienLogico', index: 'EstadoBienLogico', width: 100, align: "left" },
			{ name: 'SecFinanciamiento', index: 'SecFinanciamiento', hidden: true},
			{ name: 'CodEstadoBien', index: 'CodEstadoBien', hidden: true},
		    { name: 'Departamento', index: 'Departamento', hidden: true },
			{ name: 'Provincia', index: 'Provincia', hidden: true},
			{ name: 'Distrito', index: 'Distrito', hidden: true},
			{ name: 'FlagOrigen', index: 'FlagOrigen', hidden: true },
			{ name: 'Id', index: 'Id', hidden: true}
		],
		height: '100%',
		pager: '#jqGrid_pager_J',
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
		//multiselect: true,
    onSelectRow: function(id) {
        var rowData = $("#jqGrid_lista_J").jqGrid('getRowData', id);
    	$("#hddRowId").val(id);
    	$("#hidNumeroContrato").val(rowData.CodSolicitudCredito);
     	$("#hidFlagOrigen").val(rowData.FlagOrigen);
    	$("#hidSecFinanciamiento").val(rowData.SecFinanciamiento);
    	$("#hidCodEstado").val(rowData.CodEstadoBien);
    },
   	ondblClickRow: function(id) {
	    parent.fn_blockUI();
		var rowData = $("#jqGrid_lista_J").jqGrid('getRowData', id);
		//fn_util_redirect('frmConsultasBienDetalle.aspx?co=1&csc=' + $('#hidNumeroContrato').val() + '&csf=' + $("#hidSecFinanciamiento").val() + '&flag=' + $("#hidFlagOrigen").val() + '&codestado=' + $("#hidCodEstado").val());
		//IBK - RPH precio venta leasing
   	fn_util_redirect('frmConsultasSituacionCreditoBienDetalle.aspx?co=1&csc=' + rowData.CodSolicitudCredito + '&csf=' + rowData.SecFinanciamiento + '&flag=' + rowData.FlagOrigen + '&codestado=' + rowData.CodEstadoBien + "&precioventa=" + $("#hidPrecioVenta").val());
		//fin
	}     
	}).navGrid('#jqGrid_pager_J', { edit: false, add: false, del: false });
      $("#search_jqGrid_lista_J").hide();

}


//****************************************************************
// Funcion		:: 	fn_buscarMaquinarias
// Descripción	::	
// Log			:: 	AEP - 27/09/2012
//****************************************************************
	
function fn_buscarSistemasOtros() {
	
	    var vNumeroContrato = $('#txtNroContrato').html() == undefined ? "" : $('#txtNroContrato').html();
		
    	 var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_J", "rowNum"),     // Cantidad de elementos de la página.
                             "pCurrentPage", intPaginaActual,    // Página actual.
                             "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_J", "sortname"), // Columna a ordenar.
                             "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_J", "sortorder"), // Criterio de ordenación.
                             "pNumeroContraro", vNumeroContrato,
                             "pCodEstadoLogico", "001"
                            ];
		

    fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/ListadoBienContratoSistemas",
                    arrParametros,
                    function(jsondata) {
                        jqGrid_lista_J.addJSONData(jsondata);
                    	fn_doResize();
                    },
                    function(request) {
                        fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                    }
                   );
	}

//****************************************************************
// Función		:: 	fn_ListagrillaDocumentos
// Descripción	::	
// Log			:: 	IJM - 25/02/2012
//****************************************************************
function fn_ListagrillaDocumentos() {
    trace("fn_ListagrillaDocumentos-start");
    var arrParametros = [
                         "pPageSize", fn_util_getJQGridParam("jqGrid_lista_K", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_K", "page"), // Página actual
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_K", "sortname"), // Columna a ordenar
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_K", "sortorder"), // Criterio de ordenación
                         "pCodigo", $("#txtNroContrato").html(),
                         "pFlagFiltro", C_intFlagFiltroDocumento,
                         "pFlagEnvioCarta", C_intpFlagEnvioCarta,
                         "pFields", "CodigoContratoDocumento|Descripcion|adjunto|FlagObservacionLegal|Flagobservacion"
                        ];

    fn_util_AjaxSyncWM("frmConsultaSituacionCreditoRegistro.aspx/ListaDocumentosContrato",
                        arrParametros,
                        function(jsondata) {
                            jqGrid_lista_K.addJSONData(jsondata);
                            parent.fn_unBlockUI();
                        },
                        function(request) {
                            parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "ERROR AL LISTAR LOS DOCUMENTOS");
                            parent.fn_unBlockUI();
                        }
                       );
    trace("fn_ListagrillaDocumentos-end");
}

//****************************************************************
// Función		:: 	fn_ConfigurarGrillaDocumentos
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
        colNames: ['CodigoContratoDocumento', 'Documentos', 'Archivo',  'Obs. Comercial', 'Obs. Legal'],
        colModel: [
                     { name: 'CodigoContratoDocumento', index: 'CodigoContratoDocumento', hidden: true, width: 0, align: "left", sorttype: "string" },
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
    jQuery("#jqGrid_lista_K").jqGrid('navGrid', '#jqGrid_pager_K', { edit: false, add: false, del: false });
    $("#jqGrid_lista_K").setGridWidth($(window).width() - 150);
    $("#search_jqGrid_lista_K").hide();
    
    /***************************************************
    AGREGA ICONO Y FUNCIONALIDAD A LA GRILLA DOCUMENTOS   
    ****************************************************/
    function lupa(cellvalue, options, rowObject) {
       if (rowObject.FlagObservacionLegal == 0 ) {
            return "<img src='../../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='' style='cursor: pointer;cursor: hand;' />";
       } else {
            var sScript2 = "javascript:VerObservacionesLegal(" + rowObject.CodigoContratoDocumento + ",3);";
            return "<img src='../../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
       }
    };
    function lupa3(cellvalue, options, rowObject) {
        if (rowObject.Flagobservacion == 0) {
            return "<img src='../../Util/images/ico_obs_inact.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='' style='cursor: pointer;cursor: hand;' />";
        } else {
            var  sScript2 = "javascript:VerObservacionesEjeLeasing(" + rowObject.CodigoContratoDocumento + ",1);";
            return "<img src='../../Util/images/ico_obs_act.gif' alt='" + cellvalue + "' title='Observaciones' width='17px' onclick='" + sScript2 + "' style='cursor: pointer;cursor: hand;' />";
        }
    };
    function verAdjunto4(cellvalue, options, rowObject) {
        if (rowObject.adjunto != "") {
            var strNombreArchivo = rowObject.adjunto.split('\\').pop();
            strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);    
            return "<img src='../../Util/images/ico_download.gif' alt='Descargar/Mostrar Archivo' title='" + strNombreArchivo + "' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.adjunto) + "');\" style='cursor:pointer;'/>";
        } else {
            return ".";
        }
    }
}

function VerObservacionesLegal(strCodDocContrato, el) {
    var sTitulo = "Consultas";
    var sSubTitulo = "Situación del Crédito :: Editar";
    parent.fn_util_AbreModal(sSubTitulo, "Consultas/SituacionCredito/frmSituacionObservacion.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodConDoc=" + strCodDocContrato + "&hddCodContrato=" + $("#txtNroContrato").html() + "&sflagtipoobs=" + el + "&Add=False", 550, 200, function() { });
}

function VerObservacionesEjeLeasing(strCodDocContrato, el) {
    var sTitulo = "Consultas";
    var sSubTitulo = "Situación del Crédito :: Editar";
    parent.fn_util_AbreModal(sSubTitulo, "Consultas/SituacionCredito/frmSituacionObservacion.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodConDoc=" + strCodDocContrato + "&hddCodContrato=" + $("#txtNroContrato").html() + "&sflagtipoobs=" + el + "&Add=false", 650, 300, function() { });
}

//****************************************************************
// Función		:: 	fn_ConfigurarGrillaContratoProveedor
// Descripción	::	Carga Grilla datos de los proveedores del contrato
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ConfigurarGrillaContratoProveedor() {
    trace("fn_ConfigurarGrillaContratoProveedor-start");
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
            id: "NumeroContrato" // Índice de la columna con la clave primaria.
        },
        colNames: ['', 'Tipo de Documento', 'Nro. Documento', 'Razón social o Nombre', 'Nacionalidad'],
        colModel: [
                     { name: 'NumeroContrato', index: 'NumeroContrato', hidden: true },
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
        sortname: 'NumeroContrato',
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
    trace("fn_ConfigurarGrillaContratoProveedor-end");
}

//****************************************************************
// Función		:: 	fn_ListaDatosContratoProveedor
// Descripción	::	Devuelve los proveedores del contrato.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ListaDatosContratoProveedor() {
    trace("fn_ListaDatosContratoProveedor-start");
    var arrParametros = [
                         "pPageSize",    fn_util_getJQGridParam("jqGrid_lista_F", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_F", "page"), // Página actual
                         "pSortColumn",  fn_util_getJQGridParam("jqGrid_lista_F", "sortname"), // Columna a ordenar
                         "pSortOrder",   fn_util_getJQGridParam("jqGrid_lista_F", "sortorder"), // Criterio de ordenación
                         "pContrato", $('#hddCodigoContrato').val(),
                         "pFields", "NumeroContrato|TipoDocumento|RUC|NombreInstitucion|Nacionalidad"
                        ];

    fn_util_AjaxSyncWM("frmConsultaSituacionCreditoRegistro.aspx/ListarContratoProveedores",
                       arrParametros,
                       function(jsondata) {
                           jqGrid_lista_F.addJSONData(jsondata);
                       },
                       function(request) {
                           parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR AL LEER PROVEEDORES");
                       }
                       );
   trace("fn_ListaDatosContratoProveedor-end");
}

//****************************************************************
// Función		:: 	fn_InicializarEventos
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
        	$('#txtFechaRegistroPublico').val('');
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
        	$('#txtFechaFirmaNotaria').val('');
        }

        //fn_util_SeteaCalendario($('input[id*=txtFecha]'));
    });

    $('#cmbEstadoCivil').change(function() {
        if ($("#hddTipoDocumento").val() == CodigoTipoPersona.Natural
            && $("#cmbEstadoCivil").val() == CodigoEstadoCivil.Casado) {
            $("#fs_DatosConyugue").show();
        } else {
            $("#fs_DatosConyugue").hide();
        }
        $("#hddCodigoEstadoCivil").val($("#cmbEstadoCivil").val());
        fn_doResize();
    });
    
    // Valida el ingreso de datos en tipo documento, en caso el cliente sea persona natural y
    // el estado civil casado.
    $('#cmbTipoDocumentoConyuge').change(function() {
        var strValor = $(this).val();
        $("#txtnumerodocumento").val("");
        $('#txtnumerodocumento').unbind('keypress');

        if (fn_util_trim(strValor) == strTipoDocumentoDNI) {
            $('#txtnumerodocumento').attr('disabled', false);
            $('#txtnumerodocumento').validText({ type: 'number', length: 8 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoCarnetEx) {
            $('#txtnumerodocumento').attr('disabled', false);
            $('#txtnumerodocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoPasaporte) {
            $('#txtnumerodocumento').attr('disabled', false);
            $('#txtnumerodocumento').validText({ type: 'alphanumeric', length: 11 });
        } else if (fn_util_trim(strValor) == strTipoDocumentoOtroDoc) {
            $('#txtnumerodocumento').attr('disabled', false);
            $('#txtnumerodocumento').validText({ type: 'alphanumeric', length: 11 });
        } else {
            $('#txtnumerodocumento').attr('disabled', 'disabled');
        }
    }); 
    
    // Detectar algún cambio
    $('#txtImporteAtrasoPorc, #txtdiasVencimiento, #txtPorcentajeCuota, #txtaOtrasPenalidades, #cmbClasificacionContrato').change(function() {
        $("#hddCambiosSinGuardar").val("1");
    });
}

//****************************************************************
// Función		:: 	fn_inicializaCampos
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
function fn_inicializaCampos() {
    trace("fn_inicializaCampos-start");

	$('#btnCancelarDatosNotariales').css('display','none');
	$('#btn_CancelarAdenda').css('display','none');
	$('#btnEditarDatosNotariales').css('display','block');
	
    var tipoRubroFinanciamiento = $('#hddTipoRubroFinanciamiento').val();
    if (DestinoCredito_Inmueble.indexOf(tipoRubroFinanciamiento) != -1) {
        destinoCredito = DestinoCredito.Inmueble;
       
    } else if (DestinoCredito_Maquinaria.indexOf(tipoRubroFinanciamiento) != -1) {
        destinoCredito = DestinoCredito.Maquinaria;
    } else if (DestinoCredito_Maquinaria.indexOf(tipoRubroFinanciamiento) != -1) {
        destinoCredito = DestinoCredito.Utt;
    }else {
     destinoCredito = DestinoCredito.Otros;
    }

	//TAB :: CONDICIONES ADICIONALES
    $('#txtObservaciones').maxLength(100);

    // TAB :: OTROS CONCEPTOS
    $("#txtImporteAtrasoPorc").validNumber({ value: '', decimals: 3, length: 6 });
    $('#txtdiasatraso').validText({ type: 'number', length: 10 });
    $('#txtaOtrasPenalidades').maxLength(500);
    $('#txtdiasVencimiento').validText({ type: 'number', length: 2 });
    $('#txtPorcentajeCuota').validNumber({ value: '', decimals: 2, length: 5 });

    // TAB :: DATOS NOTARIALES
    $('#txtKardex').validText({ type: 'number', length: 10 });
    $('#txtKardex').maxLength(10);

    //IBK - RPH
    $("#txtContactoNotario").val('');
    $("#txtCorreoNotaria").val('');
    
    // TAB :: ADENDAS
    if ($("#hddCodigoEstadoContrato").val() == CodigoEstadoContrato.Formalizado) {
        $('#txtaMotivo').maxLength(500);
        $('#txtComisionEstructuracion').validText({ type: 'number', length: 10 });

        $('#txtKardexAdenda').validText({ type: 'number', length: 10 });
        $('#txtKardexAdenda').maxLength(10);
    }

    // Datos conyugue
    $('#txtNombreConyuge').maxLength(100);
    $("#txtnumerodocumento").attr('disabled', 'disabled');

    // Valida campos obligatorio
    //fn_seteaCamposObligatorios();
    //Inicio IBK - AAE
    $('#txtNumeroCuenta1').validText({ type: 'number', length: 13 });
    $('#txtNumeroCuenta2').validText({ type: 'number', length: 13 });
    $('#txtNumeroCuenta3').validText({ type: 'number', length: 13 });
    fn_util_inactivaInput("txtNumeroCuenta1", "I");
    fn_util_inactivaInput("txtNumeroCuenta2", "I");
    fn_util_inactivaInput("txtNumeroCuenta3", "I");
    $('#cmbTipoCuenta1').attr('class', 'css_select_inactivo');
    $('#cmbTipoCuenta1').attr('disabled', 'disabled');
    $('#cmbTipoCuenta2').attr('class', 'css_select_inactivo');
    $('#cmbTipoCuenta2').attr('disabled', 'disabled');
    $('#cmbMoneda2').attr('class', 'css_select_inactivo');
    $('#cmbMoneda2').attr('disabled', 'disabled');
    $('#cmbMoneda1').attr('class', 'css_select_inactivo');
    $('#cmbMoneda1').attr('disabled', 'disabled');    
    $('#cmbTipoCuenta3').attr('class', 'css_select_inactivo');
    $('#cmbTipoCuenta3').attr('disabled', 'disabled');
    $('#cmbMoneda3').attr('class', 'css_select_inactivo');
    $('#cmbMoneda3').attr('disabled', 'disabled');
    
    //Fin IBK 
    trace("fn_inicializaCampos-end");
}

function fn_cargaUbigeoInicialBien(){
	
	var strUbigeoUbicacion = $("#hddUbigeoUbicacion").val();
	if (fn_util_trim(strUbigeoUbicacion)!="") {
		
		var strDepartamento = strUbigeoUbicacion.substring(0, 2);
		var strProvincia = strUbigeoUbicacion.substring(2, 4);
		var strDistrito = strUbigeoUbicacion.substring(4, 6);
	    
		if (DestinoCredito_Inmueble.indexOf($("#hddTipoRubroFinanciamiento").val()) != -1) {
			$("#cmbDepartamentoInmueble").val(strDepartamento);
			fn_cargaComboProvincia("#cmbProvinciaInmueble","#cmbDistritoInmueble",strDepartamento);
			$("#cmbProvinciaInmueble").val(strProvincia);
			fn_cargaComboDistrito("#cmbDistritoInmueble",strDepartamento, strProvincia);
			$("#cmbDistritoInmueble").val(strDistrito);
		} else if (DestinoCredito_Maquinaria.indexOf($("#hddTipoRubroFinanciamiento").val()) != -1) {
			$("#cmbDepartamentoMaquinaria").val(strDepartamento);
			fn_cargaComboProvincia("#cmbProvinciaMaquinaria","#cmbDistritoMaquinaria",strDepartamento);
			$("#cmbProvinciaMaquinaria").val(strProvincia);
			fn_cargaComboDistrito("#cmbDistritoMaquinaria",strDepartamento, strProvincia);
			$("#cmbDistritoMaquinaria").val(strDistrito);
			
		}else {
			$("#cmbDepartamentoDatosOtros").val(strDepartamento);
			fn_cargaComboProvincia("#cmbProvinciaDatosOtros","#cmbDistritoDatosOtros",strDepartamento);
			$("#cmbProvinciaDatosOtros").val(strProvincia);
			fn_cargaComboDistrito("#cmbDistritoDatosOtros",strDepartamento, strProvincia);
			$("#cmbDistritoDatosOtros").val(strDistrito);
		}
		
	}
		
		
}

//****************************************************************
// Función		:: 	fn_seteaCamposObligatorios
// Descripción	::	Inicializa campos
// Log			:: 	JRC - 10/02/2012
//****************************************************************
//function fn_seteaCamposObligatorios() {
//    // Obligatorio si es persona natural
//    if ($("#hddTipoDocumento").val()) {
//        fn_util_SeteaObligatorio($("#cmbEstadoCivil"), "select");
//    }
// 
//    // Obligatorio si es persona natural y casado
//    fn_util_SeteaObligatorio($("#txtNombreConyuge"), "input");
//    fn_util_SeteaObligatorio($("#cmbTipoDocumentoConyuge"), "select");
//    fn_util_SeteaObligatorio($("#txtnumerodocumento"), "input");

//    switch(destinoCredito) {    
//        // Controles Bien Inmueble
//        case DestinoCredito.Inmueble:
//            fn_util_SeteaObligatorio($("#txtUsoInmueble"), "input");
//            fn_util_SeteaObligatorio($("#txtUbicacionInmueble"), "input");
//            fn_util_SeteaObligatorio($("#txtDescripcionInmueble"), "input");
//            fn_util_SeteaObligatorio($("#cmbEstadoBienInmueble"), "select");
//            fn_util_SeteaObligatorio($("#txtCantidadInmueble"), "input");
//            fn_util_SeteaObligatorio($("#cmbDepartamentoInmueble"), "select");
//            fn_util_SeteaObligatorio($("#cmbProvinciaInmueble"), "select");
//            break;
//            // Controles obligatorios maquinaria
//        case DestinoCredito.Maquinaria:
//            fn_util_SeteaObligatorio($("#txtMarcaMaquina"), "input");
//            fn_util_SeteaObligatorio($("#txtDescripcionAutoMaquina"), "input");
//            fn_util_SeteaObligatorio($("#cmbEstadobienMaquina"), "select");
//            fn_util_SeteaObligatorio($("#txtUsoBienMaquina"), "input");
//            fn_util_SeteaObligatorio($("#txtUbicacionBienMaquina"), "input");
//            fn_util_SeteaObligatorio($("#txtCantidadMaquina"), "input");
//            fn_util_SeteaObligatorio($("#cmbDepartamentoMaquinaria"), "select");
//            fn_util_SeteaObligatorio($("#cmbProvinciaMaquinaria"), "select");
//            break;
//            // Datos Otros
//        default:
//            fn_util_SeteaObligatorio($("#txtUsoDatosOtros"), "input");
//            fn_util_SeteaObligatorio($("#txtUbicacionDatosOtros"), "input");
//            fn_util_SeteaObligatorio($("#txtDescripcionDatosOtros"), "input");
//            fn_util_SeteaObligatorio($("#txtMarcaDatosOtros"), "input");
//            fn_util_SeteaObligatorio($("#txtCantidadDatosOtros"), "input");
//            fn_util_SeteaObligatorio($("#cmbEstadoDatosOtros"), "select");
//            fn_util_SeteaObligatorio($("#cmbDepartamentoDatosOtros"), "select");
//            fn_util_SeteaObligatorio($("#cmbProvinciaDatosOtros"), "select");
//    }

//    // Controles datos notariales
//    if (!$("#imgArchivoAdenda").hasClass("css_input_obligatorio")) {
//        $("#imgArchivoAdenda").addClass("css_input_obligatorio");
//    }
//    fn_util_SeteaObligatorio($("#cmbDepartamento"), "select");
//    fn_util_SeteaObligatorio($("#cmbNotariaDatoNotarial"), "select");
//    fn_util_SeteaObligatorio($("#cmbTipoContrato"), "select");
//    fn_util_SeteaObligatorio($("#txtFechaContratoNotarial"), "input");
//    fn_util_SeteaCalendario($("#txtFechaContratoNotarial"));

//    if ($("#hddCodigoEstadoContrato").val() == CodigoEstadoContrato.Formalizado) {
//         // Controles Adenda
//         fn_util_SeteaObligatorio($("#txtFechaAdenda"), "input");
//         fn_util_SeteaObligatorio($("#txtFechaEscrituraPub"), "input");
//         fn_util_SeteaObligatorio($("#cmbDepartamentoAdenda"), "select");
//         fn_util_SeteaObligatorio($("#cmbNotariaAdenda"), "select");
//         fn_util_SeteaObligatorio($("#txtKardexAdenda"), "input");
//         fn_util_SeteaObligatorio($("#cmbporCuentade"), "select");
//    } else {
//         // Obligatorio para "Guardar y Enviar"
//         fn_util_SeteaObligatorio($("#cmbClasificacionContrato"), "select");
//         fn_util_SeteaObligatorio($("#txtImporteAtrasoPorc"), "input");
//         fn_util_SeteaObligatorio($("#txtdiasVencimiento"), "input");
//         fn_util_SeteaObligatorio($("#txtPorcentajeCuota"), "input");
//    }
//}

//************************************************************
// Función		:: 	fn_grabar
// Descripción 	:: 	Método que graba los datos del contrato: Datos del Cónyuge, datos del contrato,
//                  otros conceptos.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_grabar() {
    parent.fn_blockUI();
    
    if (fn_Valida_Grabar()) { 
        parent.fn_mdl_confirma("¿Desea guardar las actualizaciones efectuadas al contrato?"
		                        , fn_Contrato_GuardarWM
		                        , "util/images/question.gif"
		                        , function() { }
		                        , "ACTUALIZACIÓN DE CONTRATO"
	                            );
    }
    
     parent.fn_unBlockUI();
}

//************************************************************
// Función		:: 	fn_Valida_Grabar
// Descripción 	:: 	Método que graba los datos del contrato: Datos del Cónyuge, datos del contrato,
//                  otros conceptos.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_Valida_Grabar() {
    var strCodigoEstadoContrato = $("#hddCodigoEstadoContrato").val();
    var strError = new StringBuilderEx();

    if ($("#hddCodigoEstadoContrato").val() == CodigoEstadoContrato.EnElaboracion ||
        $("#hddCodigoEstadoContrato").val() == CodigoEstadoContrato.PendienteDeCarta ||
        $("#hddCodigoEstadoContrato").val() == CodigoEstadoContrato.EnviadoAlCliente ||
        $("#hddCodigoEstadoContrato").val() == CodigoEstadoContrato.PendienteDeFirma) {
        var txtImporteAtrasoPorc = $('input[id=txtImporteAtrasoPorc]:text');
        var txtPorcentajeCuota = $('input[id=txtPorcentajeCuota]:text');
        
        strError.append(fn_util_ValidateControl(txtImporteAtrasoPorc[0], 'el porcentaje del importe pendiente de pago, por día de atraso.', CrLf, ''));
        strError.append(fn_util_Validar_Rango(txtImporteAtrasoPorc[0], 0, 100, 'un valor entre 0 y 100 para el porcentaje del importe pendiente de pago.', CrLf, ''));
        strError.append(fn_util_ValidateControl(txtPorcentajeCuota[0], 'el porcentaje de la cuota.', CrLf, ''));
        strError.append(fn_util_Validar_Rango(txtPorcentajeCuota[0], 0, 100, 'un valor entre 0 y 100 para el porcentaje de la cuota.', CrLf, ''));
    }
    
    // Valida si hay cambios en el contrato, exije generar anexos
    if ($("#hddFlagModificado").val() == "1") {

        if (strCodigoEstadoContrato == CodigoEstadoContrato.PendienteDeCarta) {
            if ($("#hddGeneraContrato_Adjunto").val() == "0") {
                strError.append("Por favor vuelva a generar o adjuntar el contrato antes de guardar.");
            }
        }
        if (strCodigoEstadoContrato == CodigoEstadoContrato.EnviadoAlCliente) {
            if( $("#hddGeneraContrato_Adjunto").val()=="0") {
                strError.append("Por favor vuelva a generar o adjuntar el contrato antes de guardar.");
            }
        }

        if (strCodigoEstadoContrato == CodigoEstadoContrato.PendienteDeFirma) {
            if ($("#hddGeneraContrato_Adjunto").val() == "0") {
                strError.append("Por favor vuelva a generar o adjuntar el contrato antes de guardar.");
            }
        }
        if (strCodigoEstadoContrato == CodigoEstadoContrato.PendienteDeEnvio) {
            if ($("#hddGeneraContrato_Adjunto").val() == "0") {
                strError.append("Por favor vuelva a generar o adjuntar el contrato antes de guardar.");
            }
        }
    }
    
    if (strError.toString() != '') {
        //fn_seteaCamposObligatorios();
        parent.fn_mdl_mensajeIco(strError.toString(), "Util/images/warning.gif", "GUARDAR CONTRATO");
        return false;
    } else {
        return true;
    }
}

//************************************************************
// Función		:: 	fn_util_Validar_Rango
// Descripción 	:: 	Evalua si el dato contenido en un control se encuentra contenido en el rango indicado.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_util_Validar_Rango(pControl, minimumValue, maximumValue, pMensaje, pTipoSalto, pTextoAdicional) {
    if (pControl.value == "") {
        return "";
    }
    
    var strMensaje = '';
    var strSaltoLinea;
    var strEspacio = '';
    if (pTipoSalto == 1) {
        strSaltoLinea = '<br />';
        strEspacio = '&nbsp;&nbsp;';
    } else {
        strSaltoLinea = '\n';
    }

    if (!(pControl.value >= minimumValue && pControl.value <= maximumValue)) {
        strMensaje = strMensaje + '- Ingrese ';
        pControl.className = 'css_input_error';
    } else {
        pControl.className = '';
    }
    
    if (strMensaje != '') {
         strMensaje = strEspacio + pTextoAdicional + strMensaje + pMensaje + strSaltoLinea;
    }
    
    return strMensaje;
}

//************************************************************
// Función		:: 	fn_Contrato_GuardarWM
// Descripción 	:: 	Graba los datos del contrato mediante webmethod: Datos del Cónyuge, datos del contrato,
//                  otros conceptos.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_Contrato_GuardarWM() {
    //Verifica si hay cambios para retroceder un estado

    var strCodigoEstadoContrato = $("#hddCodigoEstadoContrato").val();

    if (strCodigoEstadoContrato != "07" && strCodigoEstadoContrato != "03") {  // Si hay Modificaciones en estado de Formalizado y en Elaboracion no Regresa de Estado Estado
        if ($("#hddFlagModificado").val() == "1") {
            strCodigoEstadoContrato = CodigoEstadoContrato.PendienteDeCarta;   //04  Pendiente de carta//
        }    
    }
    var paramArray = [
                      // Datos del contrato
                      "pCodigoContrato",        $("#txtNroContrato").val(),
                      "pClasificacionContrato", $("#cmbClasificacionContrato").val(),
                      "pCodigoEstadoContrato",  strCodigoEstadoContrato,
                      "pFechaRegistroPublico",  Fn_util_DateToString($("#txtFechaRegistroPublico").val()),
                      "pFechaFirmaNotaria",     Fn_util_DateToString($("#txtFechaFirmaNotaria").val()),
                      // Dato del cliente
                      "pCodigoEstadoCivil",     $("#cmbEstadoCivil").val(),
                      // Datos del Cónyuge
                      "pNombreConyuge",         $("#txtNombreConyuge").val(),
                      "pTipoDocumentoConyuge",  $("#cmbTipoDocumentoConyuge").val(),
                      "pnumerodocumento",       $("#txtnumerodocumento").val(),
                      // Penalidades
                      "pImporteAtrasoPorc",     $("#txtImporteAtrasoPorc").val(),
                      "pOtrasPenalidades",      $("#txtaOtrasPenalidades").val(), 
                      "pdiasVencimiento",       $("#txtdiasVencimiento").val(), 
                      "pPorcentajeCuota",       $("#txtPorcentajeCuota").val(),
    	              "pClienteRazonSocial",    $("#txtRazonSocial").val(), 
                      "pClienteDomicilioLegal", $("#txtaDomicilioCliente").val(),
                      "pModificado",            fn_huboCambios()
                      ];
       
       fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/GuardarContratoActualizar",
                   paramArray,
                   function(result) {
                       if (result == "0") {
                           $("#hddFlagModificado").val("0");
                           $("#hddCambiosSinGuardar").val("0");
                           $("#hddGeneraContrato_Adjunto").val("0");
                           parent.fn_mdl_mensajeIco("Se actualizó con éxito el contrato.", "util/images/ok.gif", "ACTUALIZACIÓN DE CONTRATO");
                         
                           fn_util_redirect('frmContratoListado.aspx');
                       } else {
                           parent.fn_mdl_mensajeIco("No se pudo actualizar el contrato, por favor inténtelo nuevamente.", "util/images/warning.gif", "ACTUALIZACIÓN DE CONTRATO");
                       }
                   },
                   function(result) {
                       parent.fn_mdl_mensajeIco(result.responseText, "util/images/warning.gif", "ACTUALIZACIÓN DE CONTRATO");
                   });
 
    // Regresa al estado pendiente de carta por modificaciones
   parent.fn_unBlockUI();
} 

//************************************************************
// Función		:: 	fn_grabaryEnviar
// Descripción 	::  Guarda los datos ingresados por el usuario y actualiza el estado del contrato al siguiente estado correspondiente.
//                  Verifica si los datos de los campos obligatorios son validos, si los camspos requeridos para el estado actual
//                  estan ingresados antes de realizar la acción.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_grabaryEnviar() {
    parent.fn_blockUI();
    
    if (EsValidoGuardarYEnviar() && ValidarEstadoActual() && !fn_EsRequeridoGeneraContrato()) {
        parent.fn_mdl_confirma("¿Desea guardar las actualizaciones efectuadas al contrato?",
                               fn_contrato_GuardarYEnviarWM,
                               "util/images/warning.gif",
                               function() { }, 
                               "GUARDAR Y ENVIAR CONTRATO"
                               );
    }
    
    parent.fn_unBlockUI();
}

//************************************************************
// Función		:: 	fn_EsRequeridoGeneraContrato
// Descripción 	::  Verifica si se requiere volver a generar el documento de contrato antes de guardr y enviar.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_EsRequeridoGeneraContrato() {
      if ($("#hddGeneraContrato_Adjunto").val() == "0") {
            parent.fn_mdl_mensajeIco("Por favor vuelva a generar el contrato antes de Guardar y enviar.", "util/images/warning.gif", "GUARDAR Y ENVIAR CONTRATO");
            return true;
      } else {
        return false;
      }
}
  
//************************************************************
// Función		:: 	ValidarEstadoActual
// Descripción 	::  Verifica si se cumple con los requisitos para enviar el contrato, evaluando los requisitos del estado actual.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function ValidarEstadoActual() {
    
    var codigoEstadoContrato = $("#hddCodigoEstadoContrato").val();
    var bEsValido = false;

    switch (codigoEstadoContrato) {
        case CodigoEstadoContrato.EnElaboracion:
            if ($("#hddAdjuntarArchivo").val() != "") {
                bEsValido = true;
            } else {
            parent.fn_mdl_mensajeIco("Genere o adjunte un contrato.", "util/images/warning.gif", "ACTUALIZACIÓN DE CONTRATO");
            }
            break;
        case CodigoEstadoContrato.PendienteDeCarta:
            parent.fn_mdl_mensajeIco("Primero debe generar la carta desde el listado de contratos.", "util/images/warning.gif", "ACTUALIZACIÓN DE CONTRATO");
            break;
        case CodigoEstadoContrato.PendienteDeFirma:
            if ($("#txtFechaFirmaNotaria").val() != "") {
                bEsValido = true;

                if (!fn_util_ComparaFecha($("#txtFechaFirmaNotaria").val(),$("#hddFechaActual").val())) {
                    parent.fn_mdl_mensajeIco("La fecha de firma no puede ser mayor a la fecha actual.", "util/images/warning.gif", "FIRMAR CONTRATO");
                    bEsValido = false;
                }
                
            } else {
                parent.fn_mdl_mensajeIco("Ingrese la fecha firma en notaría para enviar el contrato.", "util/images/warning.gif", "ACTUALIZACIÓN DE CONTRATO");
            }
            break;
         case CodigoEstadoContrato.Formalizado:
             if ($("#txtFechaRegistroPublico").val() != "") {
                 bEsValido = true;
             } else {
                 parent.fn_mdl_mensajeIco("Ingrese la fecha de registros públicos para enviar el contrato.", "util/images/warning.gif", "ACTUALIZACIÓN DE CONTRATO");
             }
             break; 
    }

    return bEsValido;
}

//************************************************************
// Función		:: 	EsValidoGuardarYEnviar
// Descripción 	::  Verifica si el contrato cuenta con al menos un representante, un bien y las penalidades.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function EsValidoGuardarYEnviar() {

    var strError = new StringBuilderEx();

    // Leer los controles a ser evaluados.
    var cmbEstadoCivil =        $('select[id=cmbEstadoCivil]');
    var txtImporteAtrasoPorc =  $('input[id=txtImporteAtrasoPorc]:text');
    var txtdiasVencimiento =    $('input[id=txtdiasVencimiento]:text');
    var txtPorcentajeCuota =    $('input[id=txtPorcentajeCuota]:text');
    var txtRazonSocial=         $('input[id=txtRazonSocial]:text');
	
	if($("#txtRazonSocial").val()=="") {
	        strError.append(fn_util_ValidateControl(txtRazonSocial[0], 'la razón social', CrLf, ''));
	}

	// Clasificación del contrato
    if ($("#hddCodigoEstadoContrato").val() == CodigoEstadoContrato.EnElaboracion) {
        var cmbClasificacionContrato = $('select[id=cmbClasificacionContrato]');
        strError.append(fn_util_ValidateControl(cmbClasificacionContrato[0], 'la clasificación del contrato.', CrLf, ''));
    }

    // Estado civil
    if ($("#hddCodigoTipoPersona").val() == CodigoTipoPersona.Natural
        && $("#cmbEstadoCivil").val() == "0") {
        strError.append(fn_util_ValidateControl(cmbEstadoCivil[0], 'el estado civil.', CrLf, ''));
    }

    // Validar cuando es persona natural y casado
    if ($("#hddTipoDocumento").val() == CodigoTipoPersona.Natural
        && $("#hddCodigoEstadoCivil").val() == CodigoEstadoCivil.Casado) {
        var txtNombreConyuge = $('input[id=txtNombreConyuge]:text');
        var cmbTipoDocumentoConyuge = $('select[id=cmbTipoDocumentoConyuge]');

        strError.append(fn_util_ValidateControl(txtNombreConyuge[0], 'el nombre del conyugue.', CrLf, ''));
        strError.append(fn_util_ValidateControl(cmbTipoDocumentoConyuge[0], 'el tipo de documento del conyugue.', CrLf, ''));
        strError.append(fn_util_ValidarNroDocumento());
    }

    // Grilla representantes
    if ($("#jqGrid_lista_H").getGridParam("reccount") == 0) {
        strError.append("- Ingrese al menos un representante.<br />");
    }
    // Bienes
    switch(destinoCredito) {    
        // Controles Bien Inmueble
        case DestinoCredito.Inmueble:
            if ($("#jqGrid_lista_A").getGridParam("reccount") == 0) {
                strError.append("- Ingrese al menos un bien.<br />");
            }
            break;
        case DestinoCredito.Maquinaria:
        // Grilla Maquinaria
            if ($("#jqGrid_lista_B").getGridParam("reccount") == 0) {
                strError.append("- Ingrese al menos un bien.<br />");
            }
            break;
        // Datos Otros
        default:
			if ($("#jqGrid_lista_J").getGridParam("reccount") == 0) {
				strError.append("- Ingrese al menos un bien.<br />");
			}
            
    }
    
    // % Del importe pendiente de pago, por día de atraso
    if ($("#txtImporteAtrasoPorc").val() == "") {
        strError.append(fn_util_ValidateControl(txtImporteAtrasoPorc[0], 'el porcentaje del importe pendiente de pago, por día de atraso.', CrLf, ''));
    }
    // Días desde el vencimiento a la fecha de pago
    if ($("#txtdiasVencimiento").val() == "") {
        strError.append(fn_util_ValidateControl(txtdiasVencimiento[0], 'los días desde la fecha de vencimiento.', CrLf, ''));
    }
    // % De la cuota
    if ($("#txtPorcentajeCuota").val() == "") {
        strError.append(fn_util_ValidateControl(txtPorcentajeCuota[0], 'el porcentaje de la cuota.', CrLf, ''));
    }
    
  
    if (strError.toString() != '') {
        //fn_seteaCamposObligatorios();
        parent.fn_mdl_mensajeIco(strError.toString(), "Util/images/warning.gif", "GUARDAR Y ENVIAR CONTRATO");

        return false;
    } else {
        return true;
    }
}

//****************************************************************
// Función		:: 	fn_util_ValidarNroDocumento
// Descripción	::	Verifica si el número de documento es válido, dependiendo del tipo de documento seleccionado.
//                  
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function fn_util_ValidarNroDocumento() {
    var sError;
    var txtNroDocumento = $('input[id=txtnumerodocumento]:text');

    sError = fn_util_ValidateControl(txtNroDocumento[0], 'el número de documento.', CrLf, '');

    if (sError.length === 0) {
        var strTipoDocumento = $("#cmbTipoDocumentoConyuge").val();
        var strNroDocumento = $("#txtnumerodocumento").val();
        var intNroDocumento = strNroDocumento.length;

        if (fn_util_trim(strTipoDocumento) == strTipoDocumentoDNI) {
            if (intNroDocumento < 8) {
                sError = '&nbsp;&nbsp;- Número de documento inválido <br />';
            }
        } else if (fn_util_trim(strTipoDocumento) == strTipoDocumentoRUC) {
            if (intNroDocumento < 11) {
                sError = '&nbsp;&nbsp;- Número de documento inválido <br />';
            }
        } else if (fn_util_trim(strTipoDocumento) == strTipoDocumentoCarnetEx) {
            if (intNroDocumento < 4) {
                sError = '&nbsp;&nbsp;- Número de documento inválido <br />';
            }
        } else if (fn_util_trim(strTipoDocumento) == strTipoDocumentoPasaporte) {
            if (intNroDocumento < 7) {
                sError = '&nbsp;&nbsp;- Número de documento inválido <br />';
            }
        } else {
            if (intNroDocumento < 4) {
                sError = '&nbsp;&nbsp;- Número de documento inválido <br />';
            }
        }
    }

    return sError;
}


//****************************************************************
// Función		:: 	fn_contrato_GeneraCartaWM
// Descripción	::	Envía la carta al cliente.
//                  Si ocurre una excepción, muestra el mensaje describiendo la excepción.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function fn_contrato_GuardarYEnviarWM() {
    var strCodigoEstadoContrato = $("#hddCodigoEstadoContrato").val();
    
    var paramArray = [
                      // Datos del contrato
                      "pCodigoContrato",        $("#txtNroContrato").val(),
                      "pClasificacionContrato", $("#cmbClasificacionContrato").val(),
                      "pFechaRegistroPublico",  Fn_util_DateToString($("#txtFechaRegistroPublico").val()),
                      "pFechaFirmaNotaria",     Fn_util_DateToString($("#txtFechaFirmaNotaria").val()),
                       // Dato del cliente
                      "pCodigoEstadoCivil",     $("#cmbEstadoCivil").val(),
                       // Datos del Cónyuge
                      "pNombreConyuge",         $("#txtNombreConyuge").val(),
                      "pTipoDocumentoConyuge",  $("#cmbTipoDocumentoConyuge").val(),
                      "pnumerodocumento",       $("#txtnumerodocumento").val(),
                       // Penalidades
                      "pImporteAtrasoPorc",     $("#txtImporteAtrasoPorc").val(),
                      "pOtrasPenalidades",      $("#txtaOtrasPenalidades").val(),
                      "pdiasVencimiento",       $("#txtdiasVencimiento").val(),
                      "pPorcentajeCuota",       $("#txtPorcentajeCuota").val(),
                       // Si hay cambio de estado
                      "pCodigoEstadoContrato",  strCodigoEstadoContrato,
    	              "pClienteRazonSocial",    $("#txtRazonSocial").val(), 
                      "pClienteDomicilioLegal", $("#txtaDomicilioCliente").val()
                     ];

   fn_util_AjaxSyncWM("frmConsultaSituacionCreditoRegistro.aspx/GuardarYEnviar",
                       paramArray,
                       ResultadoGuardarYEnviar,
                       function(result) {
                            var err = eval("(" + result.responseText + ")");
                            parent.fn_mdl_mensajeIco(err.Message, "util/images/warning.gif", "GUARDAR Y ENVIAR CONTRATO");
                       });
}

//****************************************************************
// Función		:: 	ResultadoGuardarYEnviar
// Descripción	::	Le muestra un mensaje de éxito en el envío del contrato, caso contrario le describe el error.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function ResultadoGuardarYEnviar(result) {
    if (result == "0") {
        parent.fn_mdl_mensajeIco("Contrato se registrado correctamente", "util/images/ok.gif", "GUARDAR Y ENVIAR CONTRATO");
        $("#hddValidaModificacion").val("0");
        fn_util_redirect('frmContratoListado.aspx');
   } else {
        parent.fn_mdl_mensajeIco("Ocurrió un error al enviar el contrato.", "util/images/warning.gif", "GUARDAR Y ENVIAR CONTRATO");
   }
}

//************************************************************
// Función		:: 	fn_Cancelar
// Descripción 	::  Retorna a la ventana de búsquedas de contratos.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_Cancelar() {
    var sMensaje;
    if ($("#hddCambiosSinGuardar").val() == "0") {
        sMensaje = "¿Está seguro de volver?";
    } else {
        sMensaje = "¿Está seguro de volver?<br />No se guardará la información ingresada.";
    }

    parent.fn_mdl_confirma(sMensaje
		                    , fn_Volver_A_Listado
		                    , "util/images/question.gif"
		                    , function() { }
		                    , "VOLVER A LISTADO DE CONTRATO"
	                       );
}

//****************************************************************
// Función		:: 	fn_Volver_A_Listado
// Descripción	::	Carga Grilla de las adendas
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_Volver_A_Listado() {
    fn_util_redirect('frmConsultaSituacionCreditoListado.aspx');
}

//****************************************************************
// Función		:: 	fn_ConfigurarGrillaAdenda
// Descripción	::	Carga Grilla de las adendas
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ConfigurarGrillaAdenda() {
    trace("fn_ConfigurarGrillaAdenda-start");
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
        colNames: ['CodigoNotarial', 'RutaArchivo', 'Archivo', 'Fecha Envío', 'Motivo', 'CodigoPorCuenta', 'CodUbigeo', 'Por cuenta de', 'Kardex', 'CodigoNotaria', 'FechaEscrituraPublica','','','',''],
        colModel: [
            { name: 'CodigoNotarial', index: 'CodigoNotarial', hidden: true },
		    { name: 'NombreArchivo', index: 'NombreArchivo', hidden: true, editable: true },
		    { name: 'Adjunto', index: 'Adjunto', width: 20, sorttype: "string", align: "center", formatter: fnVerAdjuntoAdenda },
		    { name: 'Fecha', index: 'Fecha', width: 22, sorttype: "string", align: "center", formatter: fn_util_ValidaStringFecha },
		    { name: 'Motivo', index: 'Motivo', width: 60, sorttype: "string", align: "left" },
		    { name: 'CodigoPorCuenta', index: 'porcuentade', hidden: true },
            { name: 'CodUbigeo', index: 'CodUbigeo', hidden: true },
		    { name: 'PorCuentaDe', index: 'PorCuentaDe', width: 45, align: "left", sorttype: "string" },
		    { name: 'Kardex', index: 'Kardex', width: 35, sorttype: "string", align: "left" },
		    { name: 'CodigoNotaria', index: 'CodigoNotaria', hidden: true },
		    { name: 'FechaEscrituraPublica', index: 'FechaEscrituraPublica', hidden: true, formatter: fn_util_ValidaStringFecha },
        	 { name: 'Notaria', index: 'Notaria', hidden: true },
        	 { name: 'Departamento', index: 'Departamento', hidden: true },
        	 { name: 'Provincia', index: 'Provincia', hidden: true },
        	 { name: 'Distrito', index: 'Distrito', hidden: true }
        	 
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
        multiselect: true, 
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_I").jqGrid('getRowData', id);
            $("#hdnCodigoAdenda").val(rowData.CodigoNotarial);
        }
    });
    //****************************************************************
    // Función		:: 	fnVerAdjuntoAdenda
    // Descripción	::	Agrega botón para descargar el documento adjunto que contienen la adenda.
    // Log			:: 	EBL - 25/02/2012
    //****************************************************************
    function fnVerAdjuntoAdenda(cellvalue, options, rowObject) {
        if (rowObject.NombreArchivo != "") {
            var strNombreArchivo = rowObject.NombreArchivo.split('\\').pop();
            strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
            return "<img src='../../Util/images/ico_download.gif' alt='Descargar/Mostrar Archivo' title='" + strNombreArchivo + "' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowObject.NombreArchivo) + "');\" style='cursor:pointer;'/>";
        } else {
            return "";
        }
    };
    $("#jqGrid_lista_I").jqGrid('navGrid', '#jqGrid_pager_I', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_I").hide();
    $("#jqGrid_lista_I").setGridWidth($(window).width() - 250);
    trace("fn_ConfigurarGrillaAdenda-end");
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
// Función		:: 	fn_ConfigurarGrillaDatosNotariales
// Descripción	::	Carga Grilla datos notariales
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ConfigurarGrillaGastos() {
    
    $("#jqGrid_lista_G").jqGrid({
        datatype: function() {
            fn_ListarGastos();
        },
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "codoperacionactiva" // Índice de la columna con la clave primaria.
        },
        colNames: ['', '', 'Concepto','Fecha Vencimiento', 'Comisión', 'IGV', 'Total', 'Estado', 'Fecha Cancelación'],
        colModel: [
            { name: 'codoperacionactiva', index: 'codoperacionactiva', hidden: true },
            { name: 'CodigoTipoMinuta', index: 'CodigoTipoMinuta', hidden: true},
            { name: 'TipoComision', index: 'TipoComision', width: 50, sorttype: "string", align: "left" },
           // { name: 'fechapago', index: 'fechapago', width: 50, sorttype: "string", align: "left" },
            { name: 'fechavencimiento', index: 'fechavencimiento', width: 30 },
            //{ name: 'Importe', index: 'Importe', hidden: true},
            { name: 'MontoComision', index: 'MontoComision', width: 50, sorttype: "string", align: "left" },
            { name: 'MontoIGVComision', index: 'MontoIGVComision', width: 50, sorttype: "string", align: "left"},
            { name: 'MontoRecuperacion', index: 'MontoRecuperacion', width: 50, sorttype: "string", align: "left" },
            { name: 'estadocobro', index: 'estadocobro', width: 50, sorttype: "string", align: "left" },
            { name: 'fechacancelacion', index: 'fechacancelacion', width: 50, sorttype: "string", align: "left" }
        ],
        height: '100%',
        pager: '#jqGrid_pager_G',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 10, // Tamaño de la página
        rowList: [10, 20, 30],
        sortname: 'codoperacionactiva',
        sortorder: 'asc',
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: false,
        altclass: 'gridAltClass',
        multiselect: true,
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_G").jqGrid('getRowData', id);
         
       }
    });
    jQuery("#jqGrid_lista_G").jqGrid('navGrid', '#jqGrid_pager_G', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_G").hide();
    $("#jqGrid_lista_G").setGridWidth($(window).width() - 250);
   
}


function fn_ListarGastos() {
    
    var arrParametros = [
                         "pPageSize",    fn_util_getJQGridParam("jqGrid_lista_G", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_G", "page"), // Página actual
                         "pSortColumn",  fn_util_getJQGridParam("jqGrid_lista_G", "sortname"), // Columna a ordenar
                         "pSortOrder",   fn_util_getJQGridParam("jqGrid_lista_G", "sortorder"), // Criterio de ordenación
                         "pCodContrato", $('#hddCodigoContrato').val()
                        ];
    
    fn_util_AjaxSyncWM("frmConsultaSituacionCreditoRegistro.aspx/ListarGastos",
                       arrParametros,
                       function(jsondata) {
                           jqGrid_lista_G.addJSONData(jsondata);
                       },
                       function(request) {
                           parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR AL LISTAR CONDICIONES ADICIONALES");
                       }
                       );
}

//****************************************************************
// Función		:: 	fn_ListaDatosNotariales
// Descripción	::	Devuelve los datos notariales, para el tipo de origen.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ListaDatosNotariales(tipoDatoNotarial) {

    trace("fn_ListaDatosNotariales-" + tipoDatoNotarial + "-start");
    try {
        parent.fn_blockUI();
        var arrParametros;

   
            arrParametros = [
                                "pPageSize", fn_util_getJQGridParam("jqGrid_lista_I", "rowNum"), // Cantidad de elementos de la página
                                "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_I", "page"), // Página actual
                                "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_I", "sortname"), // Columna a ordenar
                                "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_I", "sortorder"), // Criterio de ordenación
                                "pContrato", $('#txtNroContrato').html(),
                                "pTipoDatoNotarial", tipoDatoNotarial,
                                "pFields", "CodigoNotarial|NombreArchivo|Fecha|Motivo|CodigoPorCuenta|CodUbigeo|PorCuentaDe|Kardex|CodigoNotaria|FechaEscrituraPublica|NombreContacto|CorreoContacto|Notaria|Departamento|Provincia|Distrito"
                            ];

        fn_util_AjaxSyncWM("frmConsultaSituacionCreditoRegistro.aspx/ListadoContratoNotarialPaginado",
                            arrParametros,
                            function(jsondata) {
                                
                                    jqGrid_lista_I.addJSONData(jsondata);
                       
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
    trace("fn_ListaDatosNotariales-" + tipoDatoNotarial + "-end");
}

//****************************************************************
    // Función		:: 	fn_ListaCondicionesAdicionales
// Descripción	::	Devuelve una lista de objetos GCC_ContratoDocumento para la grilla jqGrid_lista_E.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ListaCondicionesAdicionales() {
    trace("fn_ListaCondicionesAdicionales-start");
    var arrParametros = [
                         "pPageSize",    fn_util_getJQGridParam("jqGrid_lista_E", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_E", "page"), // Página actual
                         "pSortColumn",  fn_util_getJQGridParam("jqGrid_lista_E", "sortname"), // Columna a ordenar
                         "pSortOrder",   fn_util_getJQGridParam("jqGrid_lista_E", "sortorder"), // Criterio de ordenación
                         "pCodigo", $('#hddCodigoContrato').val(),
                         "pFields", "CodigoTipoCondicion|CodigoDocumento|CodigoContratoDocumento|Descripcion|adjunto|FlagObservacionLegal|Flagobservacion"
                        ];
    
    fn_util_AjaxSyncWM("frmConsultaSituacionCreditoRegistro.aspx/ListaCondicionesAdicionales",
                       arrParametros,
                       function(jsondata) {
                           jqGrid_lista_E.addJSONData(jsondata);
                           if ($("#jqGrid_lista_E").getGridParam("reccount") > 0) {
                               $("#dv_info_Req_condiciones_ad").show();
                           } else {
                               $("#dv_info_Req_condiciones_ad").hide();
                           }
                       },
                       function(request) {
                           parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR AL LISTAR CONDICIONES ADICIONALES");
                       }
                       );
   trace("fn_ListaCondicionesAdicionales-end");
}

//****************************************************************
// Función		:: 	fn_ListaCondicionesAdicionales
// Descripción	::	Devuelve una lista de representantes del cliente para la grilla jqGrid_lista_H.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ListaRepresentantesCliente() {
    trace("fn_ListaRepresentantesCliente-start");
    var arrParametros = [
                         "pPageSize",       fn_util_getJQGridParam("jqGrid_lista_H", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage",    fn_util_getJQGridParam("jqGrid_lista_H", "page"), // Página actual
                         "pSortColumn",     fn_util_getJQGridParam("jqGrid_lista_H", "sortname"), // Columna a ordenar
                         "pSortOrder",      fn_util_getJQGridParam("jqGrid_lista_H", "sortorder"), // Criterio de ordenación
                         "pNumeroContrato", $('#hddCodigoContrato').val(),
                         "pCodigoTipoRepresentante", CodigoTipoRepresentante.Cliente,
                         "pFields", "CodigoRepresentante|TipoDocumento|NroDocumento|NombreRepresentante|PartidaRegistral|OficinaRegistral|Departamento|Provincia|Distrito"
                         ];

    fn_util_AjaxSyncWM("frmConsultaSituacionCreditoRegistro.aspx/ListaRepresentantes",
                       arrParametros,
                       function(jsondata) {
                           jqGrid_lista_H.addJSONData(jsondata);
                           fn_doResize();
                       },
                       function(request) {
                           parent.fn_unBlockUI();
                           parent.fn_mdl_mensajeIco(jQuery.parseJSON(request.responseText).Message, "util/images/warning.gif", "ERROR EN LA BÚSQUEDA");
                       }
                       );
   trace("fn_ListaRepresentantesCliente-end");
}

//****************************************************************
// Función		:: 	fn_ConfigurarGrillaRepresentantesCliente
// Descripción	::	Configura la estructura de la grilla jqGrid_lista_H, para los representantes del cliente.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ConfigurarGrillaRepresentantesCliente() {
    trace("fn_ConfigurarGrillaRepresentantesCliente-start");
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
        colNames: ['', 'Tipo de Documento', 'Nro. Documento', 'Representantes', 'Partida Registral', 'Oficina Registral', 'Departamento / Provincia / Distrito', '', '', ''],
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
        altclass: 'gridAltClass'
        
    });
    $("#jqGrid_lista_H").jqGrid('navGrid', '#jqGrid_pager_H', { edit: false, add: false, del: false });
    $("#search_jqGrid_lista_H").hide();
    $("#jqGrid_lista_H").setGridWidth($(window).width() - 150);
    trace("fn_ConfigurarGrillaRepresentantesCliente-end");
}

//****************************************************************
// Función		:: 	fn_ConfigurarGrillaRepresentantesInterbank
// Descripción	::	Configura la estructura de la grilla jqGrid_lista_C, para los representantes del banco.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ConfigurarGrillaRepresentantesInterbank() {
    trace("fn_ConfigurarGrillaRepresentantesInterbank-start");
    $("#jqGrid_lista_C").jqGrid({
        datatype: function() {
            fn_ListaRepresentantesBanco();
        },
        jsonReader: { //Set the jsonReader to the JQGridJSonResponse squema to bind the data.
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount", // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodigoRepresentante" // Índice de la columna con la clave primaria.
        },
        colNames: ['CodigoRepresentante', 'DNI', 'Representante', 'Departamento / Provincia / Distrito', 'Departamento', 'Provincia', 'Distrito'],
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
    trace("fn_ConfigurarGrillaRepresentantesInterbank-end");
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
// Descripción	::	Devuelve una lista de representantes del banco para la grilla jqGrid_lista_C.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_ListaRepresentantesBanco() {
    trace("fn_ListaRepresentantesBanco-start");
    var arrParametros = [
                         "pPageSize",       fn_util_getJQGridParam("jqGrid_lista_C", "rowNum"), // Cantidad de elementos de la página
                         "pCurrentPage",    fn_util_getJQGridParam("jqGrid_lista_C", "page"), // Página actual
                         "pSortColumn",     fn_util_getJQGridParam("jqGrid_lista_C", "sortname"), // Columna a ordenar
                         "pSortOrder",      fn_util_getJQGridParam("jqGrid_lista_C", "sortorder"), // Criterio de ordenación
                         "pNumeroContrato", $('#hddCodigoContrato').val(),
                         "pCodigoTipoRepresentante", CodigoTipoRepresentante.Banco,
                         "pFields", "CodigoRepresentante|NroDocumento|NombreRepresentante|Departamento|Provincia|Distrito"
                         ];

    fn_util_AjaxSyncWM("frmConsultaSituacionCreditoRegistro.aspx/ListaRepresentantes",
                       arrParametros,
                       function(jsondata) {
                           jqGrid_lista_C.addJSONData(jsondata);
                       },
                       function(request) {
                           fn_util_alert(jQuery.parseJSON(request.responseText).Message);
                       }
                       );
   trace("fn_ListaRepresentantesBanco-end");
}

//****************************************************************
// Funcion		:: 	fn_cargaGrillaDocumentos
// Descripción	::	Carga Grilla Documentos
// Log			:: 	AEP - 12022013
//****************************************************************
function fn_cargaGrillaDocumentos() {
       $("#jqGrid_lista_E").jqGrid({
        datatype: function() {
            fn_ListarContratoEstructuraDoc();
        },
        jsonReader: {
            root: "Items",
            page: "CurrentPage", // Número de página actual.
            total: "PageCount",     // Número total de páginas.
            records: "RecordCount", // Total de registros a mostrar.
            repeatitems: false,
            id: "CodProveedor,TipoDocumento,NroDocumento,FechaEmision" // Índice de la columna con la clave primaria.
        },
        colNames: ['', '', '', '', 'Tipo Comprobante','Nº Serie','Nº Comprobante','Fecha Emision','Fecha Registro','Tipo Doc.', 'Nº Documento', 'Proveedor', 'Moneda','Total', 'Total Convertido','Fecha Desembolso','', 'CodMoneda', 'CodEstadoInstruccion'],
        colModel: [
                { name: 'CodProveedor', index: 'CodProveedor', width: 0, hidden: true, sorttype: "string" },
                { name: 'TipoDocumento', index: 'TipoDocumento', width: 0, hidden: true, sorttype: "string" },
                { name: 'NroDocumento', index: 'NroDocumento', width: 0, hidden: true, sorttype: "string" },
                { name: 'FechaEmision', index: 'FechaEmision', width: 0, hidden: true },
        	    { name: 'NombreTipoDocumento', index: 'NombreTipoDocumento', width: 130, sorttype: "string" },
        	    { name: 'NumeroSerieDocumento', index: 'NumeroSerieDocumento', width: 80, sorttype: "string" },
        	    { name: 'NroDocumentoC', index: 'NroDocumentoC', width: 120, sorttype: "string" },
                { name: 'StringFechaEmision', index: 'StringFechaEmision', width: 80, sorttype: "string" },
        	    { name: 'FechaRegistro', index: 'FechaRegistro', width: 120, sorttype: "string" },
                { name: 'TipoDocumentoProveedor', index: 'TipoDocumentoProveedor', width: 80 },
                { name: 'NumeroDocumentoProveedor', index: 'NumeroDocumentoProveedor', width: 120, align: "left" },
                { name: 'NombreProveedor', index: 'NombreProveedor', width: 250, align: "left" },
                { name: 'NombreMoneda', index: 'NombreMoneda', width: 100, align: "left" },
                //{ name: 'MontoGravado', index: 'MontoGravado', width: 150, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                //{ name: 'MontoIGV', index: 'MontoIGV', width: 150, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                //{ name: 'MontoNoGravado', index: 'MontoNoGravado', width: 150, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'Total', index: 'Total', width: 100, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
                { name: 'TotalConvertido', index: 'TotalConvertido', width: 100, align: "right", formatter: Fn_util_ReturnValidDecimal2 },
        	    { name: 'FechaDesembolso', index: 'FechaDesembolso', width: 100, align: "right" },
        	    { name: 'aaaa', index: 'MonedaaaaOriginal', width: 7 },
                //{ name: 'NombreEstadoDocumento', index: 'NombreEstadoDocumento', width: 150, align: "center" },
                //{ name: 'EstadoDocumento', index: 'EstadoDocumento', hidden: true },
                { name: 'MonedaOriginal', index: 'MonedaOriginal', hidden: true, sorttype: "string" },
                { name: 'CodEstadoInstruccion', index: 'CodEstadoInstruccion', hidden: true, sorttype: "string" }
              ],
        height: '60%',
        pager: '#jqGrid_pager_E',
        loadtext: 'Cargando datos...',
        emptyrecords: 'No hay resultados',
        rowNum: 5000,
        rowList: [10, 20, 30],
        sortname: 'Fecharegistro', // Columna a ordenar por defecto.
        sortorder: 'asc', // Criterio de ordenación por defecto.
        viewrecords: true, // Muestra la cantidad de registros.
        gridview: true,
        autowidth: true,
        altRows: true,
        loadonce: true,
        altclass: 'gridAltClass',
        onSelectRow: function(id) {
            var rowData = $("#jqGrid_lista_E").jqGrid('getRowData', id);

            $("#hidCodigoEstadoDoc").val(rowData.EstadoDocumento);
            $("#hidCodProveedor").val(rowData.CodProveedor);
            $("#hidTipoDocumento").val(rowData.TipoDocumento);
            $("#hidNumeroDocumento").val(rowData.NroDocumento);
            $("#hidFecEmision").val(rowData.FechaEmision);
            $("#hidEstadoID").val(rowData.CodEstadoInstruccion);

        }
    });
    jQuery("#jqGrid_lista_E").jqGrid('navGrid', '#jqGrid_pager_E', { edit: false, add: false, del: false });

    $("#jqGrid_lista_E").setGridWidth($(window).width() - 110);
    $("#search_jqGrid_lista_E").hide();

}



//****************************************************************
// Funcion		:: 	fn_ListarContratoEstructuraDoc
// Descripción	::	ListaContratoEstructDocumentos
// Log			:: 	JRC - 25/02/2012
//****************************************************************
function fn_ListarContratoEstructuraDoc() {
    var arrParametros = ["pPageSize", fn_util_getJQGridParam("jqGrid_lista_E", "rowNum"),
                         "pCurrentPage", fn_util_getJQGridParam("jqGrid_lista_E", "page"),
                         "pSortColumn", fn_util_getJQGridParam("jqGrid_lista_E", "sortname"),
                         "pSortOrder", fn_util_getJQGridParam("jqGrid_lista_E", "sortorder"),
                         "pCodContrato", $("#txtNroContrato").html()
                         ];
    fn_util_AjaxSyncWM("frmConsultaSituacionCreditoRegistro.aspx/ListaContratoEstructDocumentos",
                   arrParametros,
                   function(jsondata) {
                       jqGrid_lista_E.addJSONData(jsondata);
                       fnVisualizarTablaTotal();
                       fn_doResize();
                   },
                   function(resultado) {
                       var error = eval("(" + resultado.responseText + ")");
                       parent.fn_mdl_mensajeIco(error.Message, "util/images/error.gif", "ERROR AL LISTAR");
                   }
    );
}

//****************************************************************
// Función		:: 	fn_abreArchivo 
// Descripción	::	Abre Archivo
// Log			:: 	JRC - 22/05/2012
//****************************************************************
function fn_abreArchivo(pstrRuta) {
    window.open("../../frmDownload.aspx?nombreArchivo=" + pstrRuta);
    
    return false;
}

//****************************************************************
// Función		:: 	AdjuntarArchivoDocumento
// Descripción	::	Abrea una ventana modal para que el usuario puada adjuntar el archivo de contrato editado.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function AdjuntarArchivoDocumento(codigoContratoDocumento) {
    var strNumeroContrato = $("#hddCodigoContrato").val();
    var sTitulo = "Formalización";
    var sSubTitulo = "Contrato :: Editar";

    parent.fn_util_AbreModal(sSubTitulo, "Formalizacion/frmSubirArchivo.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodConDoc=" + codigoContratoDocumento + "&hddCodContrato=" + strNumeroContrato + "&Add=False", 550, 150, function() { });
}

//****************************************************************
// Función		:: 	VerObservaciones
// Descripción	::	Registrar Observaciones
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function VerObservaciones(strCodDocContrato, el) {
    var sTitulo = "Formalización";
    var sSubTitulo = "Formalización :: Contrato";

    parent.fn_util_AbreModal(sSubTitulo, "Formalizacion/frmObservacion.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&sflagtipoobs=" + el + "&Add=False" + "&hddCodContrato=" + $("#txtNroContrato").val() + "&hddCodConDoc=" + strCodDocContrato, 550, 200, function() { });
}

//****************************************************************
// Función		:: 	fn_AgregarRepresentantes
// Descripción	::	Muestra la ventana modal de representantes del cliente disponibles, permitiendo seleccionar al usuario cuales agregar al contrato.
//                  La ventana permite realizar el mantenimiento de los representantes.
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_AgregarRepresentantes() {
    var sFlag = "0";
    var sTitulo = "Contrato";
    var sSubTitulo = "Registro de Representantes";
    var sCodUnico = fn_util_trim($("#txtCodUnico").val());
    var sContrato = fn_util_trim($("#txtNroContrato").val());

    parent.fn_util_AbreModal(sSubTitulo, "Formalizacion/frmRepresentanteRegistro.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&sflagtipoobs=" + sFlag + "&Add=False&CodigoTipoRepresentante=002&CodigoContrato=" + sContrato + "&CodUnico=" + sCodUnico, 900, 550, function() { });
}

//************************************************************
// Función		:: 	fn_eliminarDetDatosBien
// Descripción 	:: 	Elimina el bien inmueble o la lista de bienes seleccionada por el usuario
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_eliminarDetDatosBien() {
    var vElementosAEliminar;

    vElementosAEliminar = $("#jqGrid_lista_A").getGridParam('selarrrow');
    if (vElementosAEliminar.length == 0) {
        parent.fn_mdl_mensajeIco("Seleccione el/los bienes para eliminarlos.", "util/images/warning.gif", "ELIMINACIÓN DE BIENES");
    } else {
        parent.fn_mdl_confirma(
		    "¿Está seguro de eliminar el/los bienes seleccionado(s)?"
		    , fn_BienInmueble_eliminarWM
		    , "util/images/warning.gif"
		    , function() { }
		    , "ELIMINACIÓN DE REGISTRO"
	    );
    }
}

//****************************************************************
// Función		:: 	fn_DatosNotariales_eliminarWM
// Descripción	::	Elimina un registro por web method, si obtiene respuesta llama al método ResultadoEliminar.
//                  Si ocurre una excepción, muestra el mensaje describiendo la excepción.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function fn_BienInmueble_eliminarWM() {
    var vElementosAEliminar;
    var sResult = "";

    vElementosAEliminar = $("#jqGrid_lista_A").getGridParam('selarrrow');

    for (var i = 0; i < vElementosAEliminar.length; i++) {
        sResult = sResult + vElementosAEliminar[i] + "|";
    }
    // Eliminar el último palito
    sResult = sResult.slice(0, -1);

    var paramArray = [
                      "pCodigoContrato", $("#txtNroContrato").val(),
                      "pBienesEliminar", sResult
                      ];
    fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/BienesEliminar",
                   paramArray,
                   ResultadoEliminarBien,
                   function(result) {
                       parent.fn_mdl_mensajeIco(result.responseText, "util/images/warning.gif", "ELIMINACIÓN DE REGISTRO");
                   });
}

//****************************************************************
// Función		:: 	ResultadoEliminarBien
// Descripción	::	Le muestra un mensaje de éxito en la eliminación, caso contrario le describe el error.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function ResultadoEliminarBien(result) {
    if (result == "0") {
        parent.fn_mdl_mensajeIco("Se eliminó con éxito el/los bien(es).", "util/images/ok.gif", "ELIMINACIÓN DE BIEN");

        $("#hddFlagModificado").val("1");
        $("#hddGeneraContrato_Adjunto").val("0");
        fn_RefreshDatosBienes();
    } else {
        var vResult = result.split("|");
        if (vResult[0] == "1") {
            parent.fn_mdl_mensajeIco(vResult[1], "util/images/warning.gif", "ELIMINACIÓN DE BIEN");
        } else {
            parent.fn_mdl_mensajeIco("Ocurrió un error al eliminar el/los bien(es).", "util/images/warning.gif", "ELIMINACIÓN DE BIEN");
        }
    }
}

//************************************************************
// Función		:: 	fn_EditarDetDatosBien
// Descripción 	:: 	Habilita los controles de los bienes inmuebles, y lee los datos del bien inmueble, para que el usuario pueda editarlos.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_EditarDetDatosBien() {
   //debugger;
    var id = $("#hddRowId").val();
    
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un bien para editarlo.", "util/images/warning.gif", "EDITAR BIEN");
    } else {
        var vElementosAEditar;
        
        vElementosAEditar = $("#jqGrid_lista_A").getGridParam('selarrrow');
        if (vElementosAEditar.length > 1) {
            parent.fn_mdl_mensajeIco("Seleccione un sólo bien para editarlo.", "util/images/warning.gif", "EDITAR BIEN");
        } else {
            fn_HabilitarControlesBienInmueble(true);

            fn_LeerBienInmueble(id);

             $('#btnCancelarDatosBien').css('display','block');
              $('#btnEditarDatosBien').css('display','none');
        }
    }
}


//************************************************************
// Función		:: 	fn_LeerBienInmueble
// Descripción 	:: 	Lee los datos del bien inmueble
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_LeerBienInmueble(id) {
    var rowData = $("#jqGrid_lista_A").jqGrid('getRowData', id);
  
    $("#txtUsoInmueble").html(rowData.Uso);
    $("#txtUbicacionInmueble").html(rowData.Ubicacion);
    $("#txtDescripcionInmueble").html(rowData.Comentario);

    //$("#hddCodigoEstadoBien").html(rowData.CodigoEstadoBien);

    $("#cmbEstadoBienInmueble").html(fn_util_trim(rowData.EstadoBien));
    
    $("#txtCantidadInmueble").html(rowData.CantidadProducto);

    $("#cmbDepartamentoInmueble").html(rowData.DepartamentoNombre);
    
     $("#cmbProvinciaInmueble").html(rowData.ProvinciaNombre);
        
    $("#cmbDistritoInmueble").html(rowData.DistritoNombre);
    
    $("#txtPartidaRegistralInmueble").html(rowData.PartidaRegistral);
    $("#txtOficinaRegistralInmueble").html(rowData.OficinaRegistral);
}

//************************************************************
// Función		:: 	fn_CancelarBienInmueble
// Descripción 	:: 	Cancela la operación de agregar o editar y configura
//                  los controles a su estado original
// Log			:: 	EBL - 09/05/2012
//************************************************************
function fn_CancelarBienInmueble() {
    fn_HabilitarControlesBienInmueble();

    $('#btnCancelarDatosBien').css('display','none');
    $('#btnEditarDatosBien').css('display','block');
    $("#hddRowId").val("");
    $("#jqGrid_lista_A").jqGrid('resetSelection');
    
    //fn_seteaCamposObligatorios();
}

//************************************************************
// Función		:: 	fn_CancelarMaquina
// Descripción 	:: 	Cancela la operación de agregar o editar y configura
//                  los controles a su estado original
// Log			:: 	EBL - 09/05/2012
//************************************************************
function fn_CancelarMaquina() {
    fn_HabilitarControlesMaquina();

   $('#btnCancelarMaquina').css('display','none');
    $('#btnEditarMaquina').css('display','block');
    
    $("#hddRowId").val("");
    $("#jqGrid_lista_B").jqGrid('resetSelection');
        
   // fn_seteaCamposObligatorios();
}

//************************************************************
// Función		:: 	fn_GuardarBienInmueble
// Descripción 	:: 	Guarda los datos del bien inmueble, previa validación de los datos
// Log			:: 	EBL - 09/05/2012
//************************************************************
function fn_GuardarBienInmueble() {
    parent.fn_blockUI();
   
    if (EsValidoBienInmueble()) {
        // Si es una operación de agregar (no tiene número secuencial)
        if ($("#hddSecFinanciamiento").val() == "") {
            fn_GuardarBienInmuebleNuevo();
        } else {
            // Si es una operación de edición
            fn_GuardarBienInmuebleEditar();
        }
        
        $("#dv_AccionesBien").show();
        $("#dv_ProcesoBien").hide();

        fn_inicializaCampos();
        fn_RefreshDatosBienes();
    }

    parent.fn_unBlockUI();
}

//****************************************************************
// Función		:: 	fn_GuardarBienInmuebleNuevo
// Descripción	::	Guarda los datos ingresados de un nuevo bien (operación nuevo).
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function fn_GuardarBienInmuebleNuevo() {
    parent.fn_blockUI();
    
    if (EsValidoBienInmueble()) {
        var arrParametros = [
                             "strNroContrato",              $("#txtNroContrato").val(),
                             "strTipoRubroFinanciamiento",  $("#hddTipoRubroFinanciamiento").val(),
                             "strUsoInmueble",              $("#txtUsoInmueble").val(),
                             "strUbicacionInmueble",        $('#txtUbicacionInmueble').val(),
                             "strDescripcionInmueble",      $("#txtDescripcionInmueble").val(),
                             "strEstadoBienInmueble",       $("#cmbEstadoBienInmueble").val(),
                             "intCantidadInmueble",         $("#txtCantidadInmueble").val(),
                             "strDepartamentoInmueble",     $("#cmbDepartamentoInmueble").val(),
                             "strProvinciaInmueble",        $("#cmbProvinciaInmueble").val(),
                             "strDistritoInmueble",         $("#cmbDistritoInmueble").val(),
                             "strPartidaRegistralInmueble", $("#txtPartidaRegistralInmueble").val(),
                             "strOficinaRegistralInmueble", $("#txtOficinaRegistralInmueble").val(),
        	                 "strCodigoPredioInmueble",     "",
        	                 "intFlagOrigen", "1",
        	                 "strCodTipoBien", $("#hddCodigoTipoBien").val() // IBK - RPH
                             ];

        fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/GuardarBienInmuebleNuevo",
                       arrParametros,
                       function() {
                           $("#hddFlagModificado").val("1");
                           $("#hddGeneraContrato_Adjunto").val("0");
                           $("#jqGrid_lista_A").trigger("reloadGrid");
                           fn_CancelarBienInmueble();
                       },
                       function(result) {
                           parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                       });

       $("#dv_AccionesBien").show();
       $("#dv_ProcesoBien").hide();

       fn_inicializaCampos();
       fn_RefreshDatosBienes();
    }

    parent.fn_unBlockUI();
}

//****************************************************************
// Función		:: 	fn_GuardarBienInmuebleEditar
// Descripción	::	Guarda los datos ingresados de un bien inmueble existente (operación editar).
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function  fn_GuardarBienInmuebleEditar(){

    var arrParametros = [
                         "strNroContrato",              $("#txtNroContrato").val(),
                         "intSecFinanciamiento",        $("#hddSecFinanciamiento").val(),
                         "strTipoRubroFinanciamiento",  $("#hddTipoRubroFinanciamiento").val(),
                         "strUsoInmueble",              $("#txtUsoInmueble").val(),
                         "strUbicacionInmueble",        $('#txtUbicacionInmueble').val(),
                         "strDescripcionInmueble",      $("#txtDescripcionInmueble").val(),
                         "strEstadoBienInmueble",       $("#cmbEstadoBienInmueble").val(),
                         "intCantidadInmueble",         $("#txtCantidadInmueble").val(),
                         "strDepartamentoInmueble",     $("#cmbDepartamentoInmueble").val(),
                         "strProvinciaInmueble",        $("#cmbProvinciaInmueble").val(),
                         "strDistritoInmueble",         $("#cmbDistritoInmueble").val(),
                         "strPartidaRegistralInmueble", $("#txtPartidaRegistralInmueble").val(),
                         "strOficinaRegistralInmueble", $("#txtOficinaRegistralInmueble").val()
                         ];

    fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/GuardarBienInmuebleEditar",
                   arrParametros,
                   function() {
                       $("#hddFlagModificado").val("1");
                       $("#hddGeneraContrato_Adjunto").val("0");
                       $("#jqGrid_lista_A").trigger("reloadGrid");
                       fn_CancelarBienInmueble();
                   },
                   function(result) {
                       parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                   });
}

//****************************************************************
// Función		:: 	fn_GuardarMaquinaEditar
// Descripción	::	Guarda los datos ingresados de un bien máquina existente (operación editar).
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function fn_GuardarMaquinaEditar() {
    var arrParametros = [
                         "strNroContrato",              $("#txtNroContrato").val(),
                         "intSecFinanciamiento",        $("#hddSecFinanciamiento").val(),
                         "strTipoRubroFinanciamiento",  $("#hddTipoRubroFinanciamiento").val(),
                         "strSerieMotorMaquina",        $("#txtSerieMotorMaquina").val(),
                         "strNumeroMotorMaquina",       $("#txtNumeroMotorMaquina").val(),
                         "strFabricacionMaquina",       $('#txtFabricacionMaquina').val(),
                         "strMarcaMaquina",             $("#txtMarcaMaquina").val(),
                         "strModeloMaquina",            $("#txtModeloMaquina").val(),
                         "strTipoCarroceriaMaquina",    $("#txtTipoCarroceriaMaquina").val(),
                         "strDescripcionAutoMaquina",   $("#txtDescripcionAutoMaquina").val(),
                         "strEstadobienMaquina",        $("#cmbEstadobienMaquina").val(),
                         "strPlacaMaquina",             $("#txtPlacaMaquina").val(),
                         "strMedidasMaquina",           $("#txtMedidasMaquina").val(),
                         "intCantidadMaquina",          $("#txtCantidadMaquina").val(),
                         "strUsoBienMaquina",           $("#txtUsoBienMaquina").val(),
                         "strUbicacionBienMaquina",     $("#txtUbicacionBienMaquina").val(),
                         "strDepartamentoMaquinaria",   $("#cmbDepartamentoMaquinaria").val(),
                         "strProvinciaMaquinaria",      $("#cmbProvinciaMaquinaria").val(),
                         "strDistritoMaquinaria",       $("#cmbDistritoMaquinaria").val()
                         ];

    fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/GuardarMaquinaEditar",
                   arrParametros,
                   function() {
                       //valida Modificaciones para generar anexos
                       $("#hddFlagModificado").val("1");

                       //Valida si hay algun cambio en contrato
                       $("#hddGeneraContrato_Adjunto").val("0");
                       
                       $("#jqGrid_lista_B").trigger("reloadGrid");
                       fn_CancelarMaquina();
                      
                   },
                   function(result) {
                       parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                   });
}

//************************************************************
// Función		:: 	EsValidoBienInmueble
// Descripción 	:: 	Verifica si los datos ingresados del bien inmueble son válidos.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function EsValidoBienInmueble() {
    // Cadena para el mensaje de error, en caso hubiese alguno.
    var strError = new StringBuilderEx();

    /****** VALIDACIONES DE TEXTO */
    var txtUsoInmueble =            $('input[id=txtUsoInmueble]:text');
    var txtUbicacionInmueble =      $('input[id=txtUbicacionInmueble]:text');
    var txtDescripcionInmueble =    $('textarea[id=txtDescripcionInmueble]');
    var cmbEstadoBienInmueble =     $('select[id=cmbEstadoBienInmueble]');
    var txtCantidadInmueble =       $('input[id=txtCantidadInmueble]:text');
    var cmbDepartamentoInmueble =   $('select[id=cmbDepartamentoInmueble]');
    var cmbProvinciaInmueble =      $('select[id=cmbProvinciaInmueble]'); 


    strError.append(fn_util_ValidateControl(txtUsoInmueble[0], 'el uso.', CrLf, ''));
    strError.append(fn_util_ValidateControl(txtUbicacionInmueble[0], 'la ubicación.', CrLf, ''));
    strError.append(fn_util_ValidateControl(txtDescripcionInmueble[0], 'la descripción.', CrLf, ''));
    strError.append(fn_util_ValidateControl(cmbEstadoBienInmueble[0], 'el estado.', CrLf, ''));
    strError.append(fn_util_ValidateControl(txtCantidadInmueble[0], 'la cantidad.', CrLf, ''));
    strError.append(fn_util_ValidateControl(cmbDepartamentoInmueble[0], 'el departamento.', CrLf, ''));
    strError.append(fn_util_ValidateControl(cmbProvinciaInmueble[0], 'la provincia.', CrLf, ''));

    if (strError.toString() != '') {
        //fn_seteaCamposObligatorios();
        parent.fn_mdl_alert(strError.toString(), function() { });

        return false;
    } else {
        return true;
    }
}

//************************************************************
// Función		:: 	EsValidoMaquina
// Descripción 	:: 	Verifica si los datos ingresados del bien maquina son válidos.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function EsValidoMaquina() {
    // Cadena para el mensaje de error, en caso hubiese alguno.
    var strError = new StringBuilderEx();

    /****** VALIDACIONES DE TEXTO */
   
    var txtMarcaMaquina =           $('input[id=txtMarcaMaquina]:text');
    var txtDescripcionAutoMaquina = $('textarea[id=txtDescripcionAutoMaquina]');
    var cmbEstadobienMaquina =      $('select[id=cmbEstadobienMaquina]');
    var txtUsoBienMaquina =         $('input[id=txtUsoBienMaquina]:text');
    var txtUbicacionBienMaquina =   $('input[id=txtUbicacionBienMaquina]:text');
    var txtCantidadMaquina =        $('input[id=txtCantidadMaquina]:text');
    var cmbDepartamentoMaquinaria = $('select[id=cmbDepartamentoMaquinaria]');
    var cmbProvinciaMaquinaria =    $('select[id=cmbProvinciaMaquinaria]');

    strError.append(fn_util_ValidateControl(txtMarcaMaquina[0], 'la marca.', CrLf, ''));
    strError.append(fn_util_ValidateControl(txtDescripcionAutoMaquina[0], 'la descripción.', CrLf, ''));
    strError.append(fn_util_ValidateControl(cmbEstadobienMaquina[0], 'el estado.', CrLf, ''));
    strError.append(fn_util_ValidateControl(txtUsoBienMaquina[0], 'el uso.', CrLf, ''));
    strError.append(fn_util_ValidateControl(txtUbicacionBienMaquina[0], 'la ubicación.', CrLf, ''));
    strError.append(fn_util_ValidateControl(txtCantidadMaquina[0], 'la cantidad.', CrLf, ''));
    strError.append(fn_util_ValidateControl(cmbDepartamentoMaquinaria[0], 'el departamento.', CrLf, ''));
    strError.append(fn_util_ValidateControl(cmbProvinciaMaquinaria[0], 'la provincia.', CrLf, ''));

    if (strError.toString() != '') {
        //fn_seteaCamposObligatorios();
        parent.fn_mdl_mensajeIco(strError.toString(), "util/images/warning.gif", "DATOS DEL BIEN");

        return false;
    } else {
        return true;
    }
}

//************************************************************
// Función		:: 	fn_agregarDetDatosBien
// Descripción 	::  Habilita los controles para que el usuario pueda agregar un nuevo bien inmueble, asociado al proveedor seleccionado
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_agregarDetDatosBien() {
    if ($("#hddCodProveedor").val() == "") {
        parent.fn_mdl_mensajeIco("Seleccione un proveedor antes de agregar un bien.", "util/images/warning.gif", "AGREGAR BIEN");
    } else {
        fn_HabilitarControlesBienInmueble(true);

        $("#dv_AccionesBien").hide();
        $("#dv_ProcesoBien").show();
    }
}

//************************************************************
// Función		:: 	fn_agregarDatosOtros
// Descripción 	::  Habilita los controles para que el usuario pueda agregar un nuevo bien, asociado al proveedor seleccionado.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_HabilitarControlesDatosOtros() {
    $("#txtDescripcionDatosOtros").html("");
    $("#txtMarcaDatosOtros").html("");
    $("#txtModeloDatosOtros").html("");
    $("#txtCantidadDatosOtros").html("");
    $("#txtPartidaRegistralDatosOtros").html("");
    $("#txtOficinaRegistralDatosOtros").html("");

$("#cmbEstadoDatosOtros").html("");
$("#cmbDepartamentoDatosOtros").html("");
$("#cmbProvinciaDatosOtros").html("");
$("#cmbDistritoDatosOtros").html("");
   	
    $("#txtUsoDatosOtros").val($("#hddUso").val());
    $("#txtUbicacionDatosOtros").val($("#hddUbicacion").val());
    
    //fn_seteaCamposObligatorios();
}

//************************************************************
// Función		:: 	fn_eliminarDatosOtros
// Descripción 	::  Permite eliminar el bien/bienes seleccionados.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_eliminarDatosOtros() {
    var vElementosAEliminar;

    vElementosAEliminar = $("#jqGrid_lista_J").getGridParam('selarrrow');
    if (vElementosAEliminar.length == 0) {
        parent.fn_mdl_mensajeIco("Seleccione el/los bienes para eliminarlos.", "util/images/warning.gif", "ELIMINAR BIEN");
    } else {
        parent.fn_mdl_confirma("¿Está seguro de eliminar el/los bienes seleccionado(s)?"
		                       , fn_DatosOtros_eliminarWM
		                       , "util/images/warning.gif"
		                       , function() { }
		                       , "ELIMINACIÓN DE REGISTRO"
	                          );
    }
}

//****************************************************************
// Función		:: 	fn_DatosOtros_eliminarWM
// Descripción	::	Elimina un registro por web method, si obtiene respuesta llama al método ResultadoEliminar.
//                  Si ocurre una excepción, muestra el mensaje describiendo la excepción.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function fn_DatosOtros_eliminarWM() {
    var vElementosAEliminar;
    var sResult = "";

    vElementosAEliminar = $("#jqGrid_lista_J").getGridParam('selarrrow');

    for (var i = 0; i < vElementosAEliminar.length; i++) {
        sResult = sResult + vElementosAEliminar[i] + "|";
    }
    // Eliminar el último palito
    sResult = sResult.slice(0, -1);

    var paramArray = [
                      "pCodigoContrato", $("#txtNroContrato").val(),
                      "pBienesEliminar", sResult
                      ];
    fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/BienesEliminar",
                   paramArray,
                   ResultadoEliminarBien,
                   function(result) {
                       parent.fn_mdl_mensajeIco(result.responseText, "util/images/warning.gif", "ELIMINAR BIEN");
                   });
}

//************************************************************
// Función		:: 	fn_EditarDatosOtros
// Descripción 	:: 	Habilita los controles de los bienes otro, y lee los datos del bien otro, para que el usuario pueda editarlos.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_EditarDatosOtros() {
    var vElementosAEditar;

   var id = $("#hddRowId").val();
    
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un bien para editarlo.", "util/images/warning.gif", "EDITAR BIEN");
    } else {
        var vElementosAEditar;
        
        vElementosAEditar = $("#jqGrid_lista_J").getGridParam('selarrrow');
        if (vElementosAEditar.length > 1) {
            parent.fn_mdl_mensajeIco("Seleccione un sólo bien para editarlo.", "util/images/warning.gif", "EDITAR BIEN");
        } else {
            fn_HabilitarControlesDatosOtros(true);

            fn_LeerDatosOtros(id);

             $('#btnCancelarDatosOtros').css('display','block');
              $('#btnEditarDatosOtros').css('display','none');
        }
    }

}



//************************************************************
// Función		:: 	fn_LeerBienInmueble
// Descripción 	:: 	Lee los datos del bien otro tipo
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_LeerDatosOtros(id) {
    var rowData = $("#jqGrid_lista_J").jqGrid('getRowData', id);
    $("#txtUsoDatosOtros").html(rowData.Uso);
    $("#txtUbicacionDatosOtros").html(rowData.Ubicacion);
    $("#txtDescripcionDatosOtros").html(rowData.Comentario);
    $("#txtMarcaDatosOtros").html(rowData.Marca);
    $("#txtModeloDatosOtros").html(rowData.Modelo);
    $("#txtCantidadDatosOtros").html(rowData.CantidadProducto);
    $("#txtPartidaRegistralDatosOtros").html(rowData.PartidaRegistral);
    $("#txtOficinaRegistralDatosOtros").html(rowData.OficinaRegistral);
    $("#cmbEstadoDatosOtros").html(fn_util_trim(rowData.EstadoBien));
    $("#cmbDepartamentoDatosOtros").html(rowData.DepartamentoNombre);
    $("#cmbProvinciaDatosOtros").html(rowData.ProvinciaNombre);    
    $("#cmbDistritoDatosOtros").html(rowData.DistritoNombre);
}

//************************************************************
// Función		:: 	fn_GuardarDatosOtros
// Descripción 	:: 	Guarda los datos del bien otros, para editar o nuevo.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_GuardarDatosOtros() {
    parent.fn_blockUI();

    if (EsValidoDatosOtros()) {
        if ($("#hddSecFinanciamiento").val() != "") {
            // Si es una operación de edición
           parent.fn_mdl_confirma(
		    "¿Está seguro de modificar los datos del bien ?"
		    , function () {
                    fn_GuardarDatosOtrosEditar();
                    $("#dv_AccionesDatosOtros").show();
                    $("#dv_ProcesoDatosOtros").hide();
                    fn_inicializaCampos();
                    fn_RefreshDatosBienes();
             }
		    , "util/images/question.gif"
		    , function() { }
		    , "EDITAR DE REGISTRO"
	        );
        }
    }

    parent.fn_unBlockUI();
}

//************************************************************
// Función		:: 	EsValidoDatosOtros
// Descripción 	:: 	Verifica si los datos ingresados del bien otros son válidos.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function EsValidoDatosOtros() {
    // Cadena para el mensaje de error, en caso hubiese alguno.
    var strError = new StringBuilderEx();

    /****** VALIDACIONES DE TEXTO */   
    var txtUsoDatosOtros =          $('input[id=txtUsoDatosOtros]:text');
    var txtUbicacionDatosOtros =    $('input[id=txtUbicacionDatosOtros]:text');
    var txtDescripcionDatosOtros =  $('textarea[id=txtDescripcionDatosOtros]');
    var txtMarcaDatosOtros =        $('input[id=txtMarcaDatosOtros]:text');
  
    var txtCantidadDatosOtros =     $('input[id=txtCantidadDatosOtros]:text');
    var cmbEstadoDatosOtros =       $('select[id=cmbEstadoDatosOtros]');
    var cmbDepartamentoDatosOtros = $('select[id=cmbDepartamentoDatosOtros]');
    var cmbProvinciaDatosOtros =    $('select[id=cmbProvinciaDatosOtros]');
    
    
    strError.append(fn_util_ValidateControl(txtUsoDatosOtros[0], 'el uso.', CrLf, ''));
    strError.append(fn_util_ValidateControl(txtUbicacionDatosOtros[0], 'la ubicación.', CrLf, ''));
    strError.append(fn_util_ValidateControl(txtDescripcionDatosOtros[0], 'la descripción.', CrLf, ''));
    strError.append(fn_util_ValidateControl(txtMarcaDatosOtros[0], 'la marca.', CrLf, ''));
    strError.append(fn_util_ValidateControl(txtCantidadDatosOtros[0], 'la cantidad.', CrLf, ''));
    strError.append(fn_util_ValidateControl(cmbEstadoDatosOtros[0], 'el estado.', CrLf, ''));
    strError.append(fn_util_ValidateControl(cmbDepartamentoDatosOtros[0], 'el departamento.', CrLf, ''));
    strError.append(fn_util_ValidateControl(cmbProvinciaDatosOtros[0], 'la provincia.', CrLf, ''));

    if (strError.toString() != '') {
        //fn_seteaCamposObligatorios();
        parent.fn_mdl_mensajeIco(strError.toString(), "util/images/warning.gif", "VALIDAR BIENES");

        return false;
    } else {
        return true;
    }
}

//****************************************************************
// Función		:: 	fn_GuardarDatosOtrosNuevo
// Descripción	::	Guarda los datos ingresados de un nuevo bien (operación nuevo).
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function fn_GuardarDatosOtrosNuevo() {
    parent.fn_blockUI();

    if (EsValidoDatosOtros()) {
        var arrParametros = ["strNroContrato",               $("#txtNroContrato").val(),
                            "strTipoRubroFinanciamiento",    $("#hddTipoRubroFinanciamiento").val(),
                            "strUsoDatosOtros",              $("#txtUsoDatosOtros").val(),
                            "strUbicacionDatosOtros",        $("#txtUbicacionDatosOtros").val(),
                            "strDescripcionDatosOtros",      $("#txtDescripcionDatosOtros").val(),
                            "strMarcaDatosOtros",            $("#txtMarcaDatosOtros").val(),
                            "strModeloDatosOtros",           $("#txtModeloDatosOtros").val(),
                            "intCantidadDatosOtros",         $("#txtCantidadDatosOtros").val(),
                            "strPartidaRegistralDatosOtros", $("#txtPartidaRegistralDatosOtros").val(),
                            "strOficinaRegistralDatosOtros", $("#txtOficinaRegistralDatosOtros").val(),
                            "strDepartamentoDatosOtros",     $("#cmbDepartamentoDatosOtros").val(),
                            "strProvinciaDatosOtros",        $("#cmbProvinciaDatosOtros").val(),
                            "strDistritoDatosOtros",         $("#cmbDistritoDatosOtros").val(),
                            "strEstadoBienInmueble",         $("#cmbEstadoDatosOtros").val(),
        	                "intFlagOrigen", "1",
        	                "strCodTipoBien", $("#hddCodigoTipoBien").val() // IBK - RPH
                            ];

        fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/GuardarDatosOtrosNuevo",
            arrParametros,
            function() {
                $("#hddFlagModificado").val("1");
                $("#hddGeneraContrato_Adjunto").val("0");
                $("#jqGrid_lista_J").trigger("reloadGrid");
                fn_CancelarDatosOtros();
            },
            function(result) {
                parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
            });

        $("#dv_AccionesDatosOtros").show();
        $("#dv_ProcesoDatosOtros").hide();

        fn_inicializaCampos();
        fn_RefreshDatosBienes();
    }

    parent.fn_unBlockUI();
}

//****************************************************************
// Función		:: 	fn_GuardarDatosOtrosEditar
// Descripción	::	Guarda los datos ingresados de un bien existente (operación editar).
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function fn_GuardarDatosOtrosEditar() {
    parent.fn_blockUI();
    var arrParametros = [
                         "strNroContrato",                 $("#txtNroContrato").val(),
                         "intSecFinanciamiento",           $("#hddSecFinanciamiento").val(),
                         "strTipoRubroFinanciamiento",     $("#hddTipoRubroFinanciamiento").val(),
                         "strUsoDatosOtros",               $("#txtUsoDatosOtros").val(),
                         "strUbicacionDatosOtros",         $("#txtUbicacionDatosOtros").val(),
                         "strDescripcionDatosOtros",       $("#txtDescripcionDatosOtros").val(),
                         "strMarcaDatosOtros",             $("#txtMarcaDatosOtros").val(),
                         "strModeloDatosOtros",            $("#txtModeloDatosOtros").val(),
                         "intCantidadDatosOtros",          $("#txtCantidadDatosOtros").val(),
                         "strPartidaRegistralDatosOtros",  $("#txtPartidaRegistralDatosOtros").val(),
                         "strOficinaRegistralDatosOtros",  $("#txtOficinaRegistralDatosOtros").val(),
                         "strDepartamentoDatosOtros",      $("#cmbDepartamentoDatosOtros").val(),
                         "strProvinciaDatosOtros",         $("#cmbProvinciaDatosOtros").val(),
                         "strDistritoDatosOtros",          $("#cmbDistritoDatosOtros").val(),
                         "strEstadoBienInmueble",          $("#cmbEstadoDatosOtros").val()
                        ];
   fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/GuardarDatosOtrosEditar",
                   arrParametros,
                   function() {
                       parent.fn_mdl_mensajeIco("Se grabó con éxito datos del bien.", "util/images/ok.gif", "EDITAR BIEN");
                            $("#hddFlagModificado").val("1");
                            $("#hddGeneraContrato_Adjunto").val("0");
                            fn_CancelarDatosOtros();
                            $("#jqGrid_lista_J").trigger("reloadGrid");
                   },
                   function(result) {
                       parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                   });
    
    parent.fn_unBlockUI();
}

//****************************************************************
// Función		:: 	fn_CancelarDatosOtros
// Descripción	::	Cancela la operación de edición o actualización, restaurando los controles y los botones.
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function fn_CancelarDatosOtros() {
    fn_HabilitarControlesDatosOtros(false);

        $('#btnCancelarDatosOtros').css('display','none');
        $('#btnEditarDatosOtros').css('display','block');
        
    $("#hddRowId").val("");
    $("#jqGrid_lista_J").jqGrid('resetSelection');
    
   
   // fn_seteaCamposObligatorios();
}


//************************************************************
// Función		:: 	fn_HabilitarControlesMaquina
// Descripción 	:: 	Habilita los controles para que el usuario pueda agregar un nuevo bien maquinaria.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_HabilitarControlesMaquina() {
    $("#txtSerieMotorMaquina").html("");
    $("#txtNumeroMotorMaquina").html("");
    $("#txtFabricacionMaquina").html("");
    $("#txtMarcaMaquina").html("");
    $("#txtModeloMaquina").html("");
    $("#txtTipoCarroceriaMaquina").html("");
    $("#txtDescripcionAutoMaquina").html("");
    $("#txtValorMaquina").html("");
    $("#txtPlacaMaquina").html("");
    $("#txtMedidasMaquina").html("");
    $("#txtCantidadMaquina").html("");   
    $("#cmbEstadobienMaquina").html(""); 
    $("#cmbDepartamentoMaquinaria").html(""); 
    $("#cmbProvinciaMaquinaria").html(""); 
    $("#cmbDistritoMaquinaria").html(""); 	
    $("#txtUsoBienMaquina").val($("#hddUso").val());
    $("#txtUbicacionBienMaquina").val($("#hddUbicacion").val());
    
   // fn_seteaCamposObligatorios();
}


//************************************************************
// Función		:: 	fn_EditarMaquina
// Descripción 	:: 	Lee los datos de la maquina seleccionada por el usuario para que pueda actualizar los datos de los mismos.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_EditarMaquina() {


var id = $("#hddRowId").val();
    
    if (IsNullOrEmpty(id)) {
        parent.fn_mdl_mensajeIco("Seleccione un bien para editarlo.", "util/images/warning.gif", "EDITAR BIEN");
    } else {
        var vElementosAEditar;
        
        vElementosAEditar = $("#jqGrid_lista_B").getGridParam('selarrrow');
        if (vElementosAEditar.length > 1) {
            parent.fn_mdl_mensajeIco("Seleccione un sólo bien para editarlo.", "util/images/warning.gif", "EDITAR BIEN");
        } else {
            fn_HabilitarControlesMaquina(true);

            fn_LeerMaquina(id);

             $('#btnCancelarMaquina').css('display','block');
              $('#btnEditarMaquina').css('display','none');
        }
    }
   
   
}


//************************************************************
// Función		:: 	fn_LeerMaquina
// Descripción 	::  Lee los datos de la máquina, contenidos en la grilla y los asigna a los controles.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_LeerMaquina(id) {
    var rowData = $("#jqGrid_lista_B").jqGrid('getRowData', id);

    $("#txtSerieMotorMaquina").html(rowData.NroSerie);
    $("#txtNumeroMotorMaquina").html(rowData.NroMotor);
    $("#txtFabricacionMaquina").html(rowData.Anio);
    $("#txtMarcaMaquina").html(rowData.Marca);
    $("#txtModeloMaquina").html(rowData.Modelo);
    $("#txtTipoCarroceriaMaquina").html(rowData.Carroceria);
    $("#txtDescripcionAutoMaquina").html(rowData.Comentario);
    $("#cmbEstadobienMaquina").html(fn_util_trim(rowData.EstadoBien));
    $("#txtValorMaquina").html(rowData.ValorBien);
    $("#txtPlacaMaquina").html(rowData.Placa);
    $("#txtMedidasMaquina").html(rowData.Medidas);
    $("#txtCantidadMaquina").html(rowData.CantidadProducto);
    $("#txtUsoBienMaquina").html(rowData.Uso);
    $("#txtUbicacionBienMaquina").html(rowData.Ubicacion);
    $("#cmbDepartamentoMaquinaria").html(rowData.DepartamentoNombre);    
    $("#cmbProvinciaMaquinaria").html(rowData.ProvinciaNombre);   
    $("#cmbDistritoMaquinaria").html(rowData.DistritoNombre);
}



//************************************************************
// Función		:: 	fn_HabilitarControlesDatosNotariales
// Descripción 	:: 	Limpia los datos contenidos en los controles de datos notariales y los 
//                  reconfigura como obligatorios los correspondientes.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_HabilitarControlesDatosNotariales() {
    fn_LimpiaComboDistritOProvincia("#cmbProvincia2");
    fn_LimpiaComboDistritOProvincia("#cmbDistrito2");

    $("#txtKardex").val("");
    $("#txtFechaContratoNotarial").val($("#hddFechaActual").val());
    $("#txtObservacionesNotariales").html("");
	$("#cmbDepartamento").html("");
	$("#cmbProvincia2").html("");
	$("#cmbNotariaDatoNotarial").html("");
	$("#cmbTipoContrato").html("");
	$("#txtKardex").html("");
	$("#txtFechaContratoNotarial").html("");

    //fn_seteaCamposObligatorios();
}


//****************************************************************
// Función		:: 	fn_DatosNotariales_eliminarWM
// Descripción	::	Elimina un registro por web method, si obtiene respuesta llama al método ResultadoEliminar.
//                  Si ocurre una excepción, muestra el mensaje describiendo la excepción.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function fn_DatosNotariales_eliminarWM() {
    var vElementosAEliminar;
    var sResult = "";

    vElementosAEliminar = $("#jqGrid_lista_G").getGridParam('selarrrow');
    for (var i = 0; i < vElementosAEliminar.length; i++) {
        sResult = sResult + vElementosAEliminar[i] + "|";
    }
    // Eliminar el último palito
    sResult = sResult.slice(0, -1);

    var paramArray = [
                      "pCodigoContrato", $("#txtNroContrato").val(),
                      "pCodigosEliminar", sResult
                     ];
    fn_util_AjaxSyncWM("frmConsultaSituacionCreditoRegistro.aspx/DatosNotarialesEliminar",
                       paramArray,
                       ResultadoEliminarDatoNotarial,
                       function(result) {
                            parent.fn_mdl_mensajeIco(result.responseText, "util/images/warning.gif", "ELIMINACIÓN DE DATOS NOTARIALES");
                       });
}

//****************************************************************
// Función		:: 	ResultadoEliminar
// Descripción	::	Le muestra un mensaje de éxito en la eliminación, caso contrario le describe el error.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function ResultadoEliminarDatoNotarial(result) {
    if (result == "0") {
        $("#jqGrid_lista_G").trigger("reloadGrid");
    } else {
        parent.fn_mdl_mensajeIco("Ocurrió un error al eliminar el/los datos notariales.", "util/images/warning.gif", "ELIMINACIÓN DE DATOS NOTARIALES");
    }
}

//************************************************************
// Función		:: 	fn_EditarDatosNotariales
// Descripción 	:: 	Lee los datos del documento notarial y habilita los controles
//                  para su edición.
// Log			:: 	EBL - 09/05/2012
//************************************************************
function fn_EditarDatosNotariales() {
    var vElementosAEditar;

    vElementosAEditar = $("#jqGrid_lista_G").getGridParam('selarrrow');

    if (vElementosAEditar.length == 0) {
        parent.fn_mdl_mensajeIco("Seleccione un dato notarial para editarlo.", "util/images/warning.gif", "EDITAR DATO NOTARIAL");
    } else if (vElementosAEditar.length > 1) {
        parent.fn_mdl_mensajeIco("Seleccione un solo dato notarial para editarlo.", "util/images/warning.gif", "EDITAR DATO NOTARIAL");
    } else {
        fn_HabilitarControlesDatosNotariales(true);
        fn_LeerDatoNotarial(vElementosAEditar[0]);

        $('#btnCancelarDatosNotariales').css('display','block');
        $('#btnEditarDatosNotariales').css('display','none');
    }
}

//************************************************************
// Función		:: 	fn_LeerDatoNotarial
// Descripción 	:: 	Lee los datos de la grilla y los carga a los controles.
// Log			:: 	EBL - 09/05/2012
//************************************************************
function fn_LeerDatoNotarial(id) {
    var rowData = $("#jqGrid_lista_G").jqGrid('getRowData', id);
    
 
    $("#cmbNotariaDatoNotarial").html(rowData.Notaria);
    
    $("#cmbDepartamento").html(rowData.Departamento);      
   
    $("#cmbProvincia2").html(rowData.Provincia);
   
    $("#cmbTipoContrato").html(fn_util_trim(rowData.Minuta));

    $("#txtKardex").html(rowData.Kardex);
    $("#txtFechaContratoNotarial").html(Fn_util_ReturnValidDate(rowData.Fecha));
    $("#txtObservacionesNotariales").html(rowData.Observacion);
    
    //IBK - RPH
    $("#txtContactoNotario").html(rowData.NombreContacto);
    $("#txtCorreoNotaria").html(rowData.CorreoContacto);

}







 
//************************************************************
// Función		:: 	fn_CancelarDatosNotariales
// Descripción 	:: 	Cancela la operación de agregar o editar y configura
//                  los controles a su estado original
// Log			:: 	EBL - 09/05/2012
//************************************************************
function fn_CancelarDatosNotariales() {
    fn_HabilitarControlesDatosNotariales(false);
    
    $('#btnCancelarDatosNotariales').css('display','none');
    $('#btnEditarDatosNotariales').css('display','block');
    $("#hdnCodigoNotarial").val("");
	
    $("#jqGrid_lista_G").jqGrid('resetSelection');
    
     //IBK - RPH
    $("#txtContactoNotario").val('');
    $("#txtCorreoNotaria").val('');
    
   // fn_seteaCamposObligatorios();
}

//************************************************************
// Función		:: 	EsValidoDatosNotariales
// Descripción 	:: 	Verifica si la información de los datos notariales ingresados son validos, previo a una operación 
//                  de agregar un nuevo dato o edición.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function EsValidoDatosNotariales() {

    // Cadena para el mensaje de error, en caso hubiese alguno.
    var strError = new StringBuilderEx();
    /****** VALIDACIONES DE COMBOS */
    var cmbDepartamento = $('select[id=cmbDepartamento]');
    var cmbNotaria2 =     $('select[id=cmbNotariaDatoNotarial]');
    var cmbTipoContrato = $('select[id=cmbTipoContrato]'); 
    /****** VALIDACIONES DE TEXTO */
    var txtFechaContratoNotarial = $('input[id=txtFechaContratoNotarial]:text');

    strError.append(fn_util_ValidateControl(cmbDepartamento[0], 'el departamento.', CrLf, ''));
    strError.append(fn_util_ValidateControl(cmbNotaria2[0], 'la notaria.', CrLf, ''));
    strError.append(fn_util_ValidateControl(cmbTipoContrato[0], 'el tipo de contrato.', CrLf, ''));
    strError.append(fn_util_ValidateControl(txtFechaContratoNotarial[0], 'fecha de envío.', CrLf, ''));

    if ($("#txtFechaContratoNotarial").val() != "") {
		//if (fn_util_ComparaFecha($("#txtFechaContratoNotarial").val(), $("#hddFechaActual").val()) && ($("#txtFechaContratoNotarial").val() != $("#hddFechaActual").val())) {
        if (!fn_util_ComparaFecha($("#txtFechaContratoNotarial").val(),$("#hddFechaActual").val()) ) {
            strError.append("&nbsp;&nbsp;- La fecha de envío no puede ser mayor a la fecha actual.<br />");
            $("#txtFechaContratoNotarial").removeClass();
            $("#txtFechaContratoNotarial").addClass('css_input_error hasDatepicker css_calendario_error');
        } else {
            $("#txtFechaContratoNotarial").removeClass("css_input_error");
            $("#txtFechaContratoNotarial").addClass("css_input hasDatepicker css_calendario css_input_obligatorio");
        }
    }

    if (strError.toString() != '') {
       // fn_seteaCamposObligatorios();
        parent.fn_mdl_mensajeIco(strError.toString(), "util/images/warning.gif", "DATO NOTARIAL");
        return false;
    } else {
        return true;
    }
}

//************************************************************
// Función		:: 	fn_AgregarAdenda
// Descripción 	::  Habilita los controles para permitir agregar una nueva adenda y muestra la barra de herramientas con
//                  los botones guardar y cancelar .
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_AgregarAdenda() {
    fn_HabilitarControlesAdenda(true);

    $("#dv_AccionesAdenda").hide();
    $("#dv_ProcesoAdenda").show();
}

//************************************************************
// Función		:: 	fn_HabilitarControlesAdenda
// Descripción 	::  Habilita los controles para permitir agregar una nueva adenda.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_HabilitarControlesAdenda() {
   

    $("#cmbporCuentade option:first").attr('selected', 'selected');
    //$("#cmbDepartamentoAdenda").val(Departamento.Lima);
   
 
    $("#cmbDistritoAdenda option:first").attr('selected', 'selected');

    $("#txtFechaAdenda").html("");
    $("#txtFechaEscrituraPub").html("");
	$("#cmbporCuentade").html("");
	$("#cmbDepartamentoAdenda").html("");
	$("#cmbProvienciaAdenda").html("");
	$("#cmbDistritoAdenda").html("");
	$("#cmbNotariaAdenda").html("");
	$("#txtaMotivo").html("");
    $("#txtKardexAdenda").html("");
    $("#hddAdjuntarArchivoNotarialNuevo").val("");
    //$("#dv_DescargarArchivoAdenda").html("");
    
    //fn_seteaCamposObligatorios();
}

//************************************************************
// Función		:: 	fn_EliminarAdenda
// Descripción 	:: 	Elimina el o las adendas seleccionados por el usuario, solicitandoles previamente al
//                  usuario la confirmación para la operación.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function fn_EliminarAdenda() {
    var vElementosAEliminar;
    vElementosAEliminar = $("#jqGrid_lista_I").getGridParam('selarrrow');
    
    if (vElementosAEliminar.length == 0) {
        parent.fn_mdl_alert("Seleccione al menos una adenda para eliminarla.", function() { });
    } else {
        var sMsg;
        if (vElementosAEliminar.length == 1) {
            sMsg = "¿Está seguro de eliminar la adenda seleccionada?";
        } else {
            sMsg = "¿Está seguro de eliminar las adendas seleccionadas?";
        }
        parent.fn_mdl_confirma(sMsg
		                       , fn_Adenda_eliminarWM
		                       , "util/images/warning.gif"
		                       , function() { }
		                       , "ELIMINACIÓN DE ADENDA"
	                           );
    }
}

//****************************************************************
// Función		:: 	fn_Adenda_eliminarWM
// Descripción	::	Elimina un registro por web method, si obtiene respuesta llama al método ResultadoEliminar.
//                  Si ocurre una excepción, muestra el mensaje describiendo la excepción.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function fn_Adenda_eliminarWM() {
    var sResult = "";
    var vElementosAEliminar;
    
    vElementosAEliminar = $("#jqGrid_lista_I").getGridParam('selarrrow');

    for (var i = 0; i < vElementosAEliminar.length; i++) {
        sResult = sResult + vElementosAEliminar[i] + "|";
    }
    // Eliminar el último palito
    sResult = sResult.slice(0, -1);

    var paramArray = [
                        "pCodigoContrato",  $("#txtNroContrato").val(),
                        "pCodigosEliminar", sResult
                     ];
    fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/DatosNotarialesEliminar",
                   paramArray,
                   ResultadoAdendaEliminar,
                   function(result) {
                       parent.fn_mdl_mensajeIco(result.responseText, "util/images/warning.gif", "ELIMINACIÓN DE ADENDA");
                   });
}

//****************************************************************
// Función		:: 	ResultadoAdendaEliminar
// Descripción	::	Le muestra un mensaje de éxito en la eliminación, caso contrario le describe el error.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function ResultadoAdendaEliminar(result) {
    if (result == "0") {
        $("#jqGrid_lista_I").trigger("reloadGrid");
    } else {
        parent.fn_mdl_mensajeIco("Ocurrió un error al eliminar el/las adenda(s).", "util/images/warning.gif", "ELIMINACIÓN DE ADENDA");
    }
}

//************************************************************
// Función		:: 	fn_EditarAdenda
// Descripción 	:: 	Lee el índice de la adenda seleccionada y asigna los datos de la misma a los controles para su edición.
// Log			:: 	EBl - 10/02/2012
//************************************************************
function fn_EditarAdenda() {
    var vElementosAEditar;

    vElementosAEditar = $("#jqGrid_lista_I").getGridParam('selarrrow');

    if (vElementosAEditar.length == 0) {
        parent.fn_mdl_mensajeIco("Seleccione una adenda para editarla.", "util/images/warning.gif", "EDITAR ADENDA");
    } else if (vElementosAEditar.length > 1) {
        parent.fn_mdl_mensajeIco("Seleccione una sola adenda para editarla.", "util/images/warning.gif", "EDITAR ADENDA");
    } else {
        if (!IsNullOrEmpty(vElementosAEditar[0])) {
            fn_HabilitarControlesAdenda(true);

            fn_LeerAdenda(vElementosAEditar[0]);

           $('#btn_EditarAdenda').css('display','none');
        	$('#btn_CancelarAdenda').css('display','block');
        }
    }
}

//************************************************************
// Función		:: 	fn_LeerAdenda
// Descripción 	:: 	Lee los datos de la adenda.
// Log			:: 	EBL - 09/05/2012
//************************************************************
function fn_LeerAdenda(id) {
    var rowData = $("#jqGrid_lista_I").jqGrid('getRowData', id);

    $("#txtFechaAdenda").html(Fn_util_ReturnValidDate(rowData.Fecha));
    $("#txtFechaEscrituraPub").html(Fn_util_ReturnValidDate(rowData.FechaEscrituraPublica));
    $("#dv_AdjuntarAdenda").html("<a href='#'>" + rowData.NombreArchivo + "</a>");
    $("#txtaMotivo").html(rowData.Motivo);
    $("#cmbporCuentade").html(rowData.PorCuentaDe);    
    $("#txtKardexAdenda").html(rowData.Kardex);
    $("#cmbNotariaAdenda").html(rowData.Notaria);
	
	$("#cmbDepartamentoAdenda").html(rowData.Departamento);
	$("#cmbProvienciaAdenda").html(rowData.Provincia);
	$("#cmbDistritoAdenda").html(rowData.Distrito);

    if (rowData.NombreArchivo != "") {
        var strNombreArchivo = rowData.NombreArchivo.split('\\').pop();
        strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
        var img = "<img src='../../Util/images/ico_download.gif' alt='Descargar archivo de adenda' title='Descargar archivo de adenda' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowData.NombreArchivo) + "');\" style='cursor:pointer;cursor: hand;' />";
        var lnk = "<a href='#' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(rowData.NombreArchivo) + "');\">" + strNombreArchivo + "</a>";
      
        $("#dv_DescargarArchivoAdenda").html(img + "&nbsp;&nbsp;" + lnk);
        $("#hddAdjuntarArchivoNotarialEditar").val(rowData.NombreArchivo);
    }

//    $("#cmbDepartamentoAdenda").html(rowData.CodUbigeo.substring(0, 2));
//    $("#cmbProvienciaAdenda").html(rowData.CodUbigeo.substring(2, 4));
//    $("#cmbDistritoAdenda").html(rowData.CodUbigeo.substring(4, 6));
}

//************************************************************
// Función		:: 	fn_CancelarAdenda
// Descripción 	:: 	Cancela la operación de agregar o editar y configura
//                  los controles a su estado original
// Log			:: 	EBL - 09/05/2012
//************************************************************
function fn_CancelarAdenda() {
    fn_HabilitarControlesAdenda(false);

    
    $("#hdnCodigoAdenda").val("");
    $("#jqGrid_lista_I").jqGrid('resetSelection');

    fn_inicializaCampos();
	$('#btn_EditarAdenda').css('display','block');
    //fn_seteaCamposObligatorios();
}

//************************************************************
// Función		:: 	fn_GuardarAdenda
// Descripción 	:: 	Guarda las adendas, previa validación de los datos
// Log			:: 	EBL - 09/05/2012
//************************************************************
function fn_GuardarAdenda() {
    parent.fn_blockUI();

    if (EsValidoAdenda()) {
        parent.fn_mdl_confirma ("¿Está seguro de modificar los datos de la adenda?"
	                            , function() {
	                                // Si es una operación de edición
	                                fn_GuardarAdendaEditar();
	                                $("#dv_AccionesAdenda").show();
	                                $("#dv_ProcesoAdenda").hide();
	                            }
	                            , "util/images/question.gif"
	                            , function() {
	                                fn_CancelarAdenda();
	                            }
	                            , "EDITAR ADENDA"
                                );
        
        fn_inicializaCampos();
        fn_ConfigurarGrillaAdenda();
        fn_ListaDatosNotariales(TipoDatoNotarial.Adenda);
    }

    parent.fn_unBlockUI();
}

//************************************************************
// Función		:: 	EsValidoAdenda
// Descripción 	:: 	Verifica la validez de los datos ingresados por el usuario.
// Log			:: 	EBL - 10/02/2012
//************************************************************
function EsValidoAdenda() {
    // Cadena para el mensaje de error, en caso hubiese alguno.
    var strError = new StringBuilderEx();

    /****** VALIDACIONES DE COMBOS */
    var cmbDepartamentoAdenda = $('select[id=cmbDepartamentoAdenda]');
    var cmbNotaria2 = $('select[id=cmbNotariaAdenda]');

    /****** VALIDACIONES DE TEXTO */
    var txtFechaAdenda = $('input[id=txtFechaAdenda]:text');
    var txtFechaEscrituraPub = $('input[id=txtFechaEscrituraPub]:text');
    var txtKardex = $('input[id=txtKardexAdenda]:text');
    var cmbporCuentade = $('select[id=cmbporCuentade]');

    // Validar la fecha de envío.
    strError.append(fn_util_ValidateControl(txtFechaAdenda[0], 'la fecha de envío.', CrLf, ''));
    if ($("#txtFechaAdenda").val() == "") {
        $("#txtFechaAdenda").addClass('css_input_error hasDatepicker css_calendario_error');
    } else {
    if (!fn_util_ComparaFecha($("#txtFechaAdenda").val(), $("#hddFechaActual").val()) && ($("#txtFechaAdenda").val() != $("#hddFechaActual").val())) {
            $("#txtFechaAdenda").removeClass();
            strError.append("&nbsp;&nbsp;- La fecha de envío no puede ser mayor a la fecha actual.<br />");
            $("#txtFechaAdenda").addClass('css_input_error hasDatepicker css_calendario_error');
        } else {
            $("#txtFechaAdenda").removeClass("css_input_error");
            $("#txtFechaAdenda").addClass("css_input hasDatepicker css_calendario css_input_obligatorio");
        }
    }
    
    // Validar la fecha de escritura pública.
    strError.append(fn_util_ValidateControl(txtFechaEscrituraPub[0], 'la fecha de escritura pública.', CrLf, ''));
    $("#txtFechaEscrituraPub").removeClass();
    if ($("#txtFechaEscrituraPub").val() == "") {
        $("#txtFechaEscrituraPub").addClass('css_input_error hasDatepicker css_calendario_error');
    } else {
        $("#txtFechaEscrituraPub").removeClass("css_input_error");
        $("#txtFechaEscrituraPub").addClass("css_input hasDatepicker css_calendario css_input_obligatorio");
    }

    // Validar el archivo de adenda.
    if ($("#dv_DescargarArchivoAdenda").html() == "") {
        strError.append("&nbsp;&nbsp;- Falta adjuntar la adenda.<br />");
        $("#imgArchivoAdenda").removeClass("css_input_obligatorio");
        $("#imgArchivoAdenda").addClass("css_input_error");
    } else {
        $("#imgArchivoAdenda").removeClass("css_input_error");
        $("#imgArchivoAdenda").addClass("css_input_obligatorio");
    }
   
    strError.append(fn_util_ValidateControl(cmbDepartamentoAdenda[0], 'el departamento.', CrLf, ''));
    strError.append(fn_util_ValidateControl(cmbNotaria2[0], 'la notaría.', CrLf, ''));
    strError.append(fn_util_ValidateControl(txtKardex[0], 'un número de kardex.', CrLf, ''));
    strError.append(fn_util_ValidateControl(cmbporCuentade[0], 'por cuenta de.', CrLf, ''));

    if (strError.toString() != '') {
        //fn_seteaCamposObligatorios();
        parent.fn_mdl_mensajeIco(strError.toString(), "util/images/warning.gif", "VALIDAR ADENDA");

        return false;
    } else {
        return true;
    }
}

//****************************************************************
// Función		:: 	fn_GuardarAdendaEditar
// Descripción	::	Guarda los datos ingresados para una adenda existente (operación editar).
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function fn_GuardarAdendaEditar() {
    var arrParametros = [
                         "intCodigoNotarial",    $("#hdnCodigoAdenda").val(),
                         "strNroContrato",       $("#txtNroContrato").val(),
                         "strFechaAdenda",       Fn_util_DateToString($("#txtFechaAdenda").val()),
                         "strFechaEscrituraPub", Fn_util_DateToString($("#txtFechaEscrituraPub").val()),
                         "strAdjuntarAdenda",    encodeURIComponent($('#hddAdjuntarArchivoNotarialEditar').val()),
                         "strMotivo",            $("#txtaMotivo").val(),
                         "strPorCuentaDe",       $("#cmbporCuentade").val(),
                         "strNotariaAdenda",     $("#cmbNotariaAdenda").val(),
                         "strKardexAdenda",      $("#txtKardexAdenda").val(),
                         "strDepartamentoAdenda",$("#cmbDepartamentoAdenda").val(),
                         "strProvinciaAdenda",   $("#cmbProvienciaAdenda").val(),
                         "strDistritoAdenda",    $("#cmbDistritoAdenda").val()
                        ];
    
    fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/GuardarAdendaEditar",
                   arrParametros,
                   function() {
                       $("#jqGrid_lista_I").trigger("reloadGrid");
                       fn_CancelarAdenda();
                   },
                   function(result) {
                       parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR");
                       fn_CancelarAdenda();
                   });
}

//****************************************************************
// Función		:: 	fn_GuardarAdendaNuevo
// Descripción	::	Guarda los datos ingresados de una nueva adenda (operación nuevo).
// Log			:: 	EBL - 07/05/2012
//****************************************************************
function fn_GuardarAdendaNuevo() {
    parent.fn_blockUI();

    if (EsValidoAdenda()) {
        var arrParametros = [
                             "strNroContrato",        $("#txtNroContrato").val(),
                             "strFechaAdenda",        Fn_util_DateToString($("#txtFechaAdenda").val()),
                             "strFechaEscrituraPub",  Fn_util_DateToString($("#txtFechaEscrituraPub").val()),
                             "strAdjuntarAdenda",     encodeURIComponent($('#hddAdjuntarArchivoNotarialNuevo').val()),
                             "strMotivo",             $("#txtaMotivo").val(),
                             "strPorCuentaDe",        $("#cmbporCuentade").val(),
                             "strNotariaAdenda",      $("#cmbNotariaAdenda").val(),
                             "strKardexAdenda",       $("#txtKardexAdenda").val(),
                             "strDepartamentoAdenda", $("#cmbDepartamentoAdenda").val(),
                             "strProvinciaAdenda",    $("#cmbProvienciaAdenda").val(),
                             "strDistritoAdenda",     $("#cmbDistritoAdenda").val()
                            ];

        fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/GuardarAdendaNuevo",
                   arrParametros,
                   function() {
                       $("#jqGrid_lista_I").trigger("reloadGrid");
                       fn_CancelarAdenda();
                   },
                   function(result) {
                       parent.fn_mdl_mensajeIco("ERROR " + result.status + ' ' + result.statusText, "util/images/warning.gif", "ERROR AL GUARDAR ADENDA");
                       fn_CancelarAdenda();
                   });

       $("#dv_AccionesAdenda").show();
       $("#dv_ProcesoAdenda").hide();

       fn_inicializaCampos();
       fn_ConfigurarGrillaAdenda();
       fn_ListaDatosNotariales(TipoDatoNotarial.Adenda);
   }

   parent.fn_unBlockUI();
}

//************************************************************
// Función		:: 	fn_editarTextoPredefinido
// Descripción 	:: 	
// Log			:: 	JRC - 10/02/2012
//************************************************************
 function fn_editarTextoPredefinido(codigoContratoDocumento) {
     var codigoContrato = $("#hddCodigoContrato").val();
     var strEditable;
     
     if ($("#hddCodigoEstadoContrato").val() == "07") {
        strEditable = "no";
     } else {
        strEditable = "si";
     }

     parent.fn_util_AbreModal("Texto Predefinido :: Editar", "Consultas/SituacionCredito/frmSituacionCreditoTextoPredefinido.aspx?CodigoContrato=" + codigoContrato + "&CodigoContratoDocumento=" + codigoContratoDocumento + "&edita= "+ strEditable , 675, 415, function() { });
 }

 function fn_ListaRepresentantesClienteFromModal() {
     parent.fn_blockUI();
     $("#hddFlagModificado").val("1");
     $("#hddGeneraContrato_Adjunto").val("0");
     $("#jqGrid_lista_H").trigger("reloadGrid");
     fn_ConfigurarGrillaRepresentantesCliente();
     fn_ListaRepresentantesCliente();
     parent.fn_unBlockUI();
 }

 //************************************************************
 // Función		:: 	fn_EliminarRepresentantes
 // Descripción :: 	Elimina el o los representantes seleccionados por el usuario, solicitandoles previamente al
 //                 usuario la confirmación para la operación.
 // Log			:: 	EBL - 10/02/2012
 //************************************************************
 function fn_EliminarRepresentantes() {
     var vElementosAEliminar;

     vElementosAEliminar = $("#jqGrid_lista_H").getGridParam('selarrrow');
     if (vElementosAEliminar.length == 0) {
         parent.fn_mdl_mensajeIco("Seleccione el/los representantes para eliminarlos.", "util/images/warning.gif", "ELIMINAR REPRESENTANTE");
     } else {
         parent.fn_mdl_confirma("¿Está seguro de eliminar el/los representantes seleccionado(s)?"
		                        , fn_Representantes_eliminarWM
		                        , "util/images/warning.gif"
		                        , function() { }
		                        , "ELIMINACIÓN DE REGISTRO"
	                            );
     }
 }

 //****************************************************************
 // Función		:: 	fn_DatosNotariales_eliminarWM
 // Descripción	::	Elimina un registro por web method, si obtiene respuesta llama al método ResultadoEliminar.
 //                 Si ocurre una excepción, muestra el mensaje describiendo la excepción.
 // Log			:: 	EBL - 06/03/2012
 //****************************************************************
 function fn_Representantes_eliminarWM() {
     parent.fn_blockUI();
     
     var vElementosAEliminar;
     var sResult = "";

     vElementosAEliminar = $("#jqGrid_lista_H").getGridParam('selarrrow');

     for (var i = 0; i < vElementosAEliminar.length; i++) {
         sResult = sResult + vElementosAEliminar[i] + "|";
     }
     // Eliminar el último palito
     sResult = sResult.slice(0, -1);

     var paramArray = [
                       "pCodigoContrato",           $("#txtNroContrato").val(),
                       "pCodigoTipoRepresentante",  "002", //REPRESENTANTE CLIENTE
                       "pRepresentantesEliminar",   sResult
                      ];
     fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/RepresentanteEliminar",
                   paramArray,
                   ResultadoRepresentanteEliminar,
                   function(result) {
                       parent.fn_mdl_mensajeIco(result.responseText, "util/images/warning.gif", "ELIMINACIÓN DE REGISTRO");
                   });

   parent.fn_unBlockUI();
}

//****************************************************************
// Función		:: 	ResultadoRepresentanteEliminar
// Descripción	::	Le muestra un mensaje de éxito en la eliminación, caso contrario le describe el error.
// Log			:: 	EBL - 06/03/2012
//****************************************************************
function ResultadoRepresentanteEliminar(result) {
   if (result == "0") {
       parent.fn_mdl_mensajeIco("Se eliminó con éxito el/los representante(s).", "util/images/ok.gif", "ELIMINACIÓN DE REPRESENTANTES");
       $("#hddFlagModificado").val("1");
       $("#hddGeneraContrato_Adjunto").val("0");
       fn_ListaRepresentantesCliente();
   } else {
       parent.fn_mdl_mensajeIco("Ocurrió un error al eliminar el/los representante(s).", "util/images/warning.gif", "ELIMINACIÓN DE REPRESENTANTES");
   }
}

//****************************************************************
// Función		:: 	fn_GenerarContrato
// Descripción	::	Abre anexos
// Log			:: 	EBL - 25/05/2012
//****************************************************************
function fn_GenerarContrato() {
    parent.fn_blockUI();

    var nroContrato = $("#txtNroContrato").val();
    
    if (EsValidoGuardarYEnviar()) {
           if (EsValidaGeneracionAnexos()) {
                parent.fn_mdl_confirma("¿Desea generar el contrato " + nroContrato + "?"
		                                , fn_generarAnexosWM
		                                , "util/images/warning.gif"
		                                , function() { }
		                                , "GENERAR CONTRATO"
	                                  );
           }
    }
    
    parent.fn_unBlockUI();
}

//****************************************************************
// Función		:: 	EsValidaGeneracionAnexos
// Descripción	::	Valida si los anexos pueden generarse en el estado actual del contrato, y si el tipo de persona
//                  es jurídica.
// Log			:: 	EBL - 25/05/2012
//****************************************************************
function EsValidaGeneracionAnexos() {
    var bEsValido = true;
    var codigoTipoPersona = $("#hddCodigoTipoPersona").val();
    
    if (codigoTipoPersona != CodigoTipoPersona.Juridica) {
        parent.fn_mdl_mensajeIco("Sólo existen modelos de contrato para persona jurídicas.", "util/images/warning.gif", "GENERAR CONTRATO");
        bEsValido = false;
    }

    return bEsValido;
}

//****************************************************************
// Función		:: 	fn_generarAnexosWM
// Descripción	::	Genera los anexos a traves del llamado a web method.
// Log			:: 	EBL - 25/05/2012
//****************************************************************
function fn_generarAnexosWM() {
    var paramArray = [
                      // Datos del contrato
                      "pCodigoContrato",        $("#txtNroContrato").val(),
                      "pCodigoCotizacion",      $("#hddCodigoCotizacion").val(),
                      "pClasificacionContrato", $("#cmbClasificacionContrato").val(),
                      "pCodigoEstadoContrato",  $("#hddCodigoEstadoContrato").val(),
                      "pFechaRegistroPublico",  Fn_util_DateToString($("#txtFechaRegistroPublico").val()),
                      "pFechaFirmaNotaria",     Fn_util_DateToString($("#txtFechaFirmaNotaria").val()),
                      // Dato del cliente
                      "pCodigoEstadoCivil",     $("#cmbEstadoCivil").val(),
                      // Datos del Cónyuge
                      "pNombreConyuge",         $("#txtNombreConyuge").val(),
                      "pTipoDocumentoConyuge",  $("#cmbTipoDocumentoConyuge").val(),
                      "pnumerodocumento",       $("#txtnumerodocumento").val(),
                      // Penalidades
                      "pImporteAtrasoPorc",     $("#txtImporteAtrasoPorc").val(),
                      "pOtrasPenalidades",      $("#txtaOtrasPenalidades").val(),
                      "pdiasVencimiento",       $("#txtdiasVencimiento").val(),
                      "pPorcentajeCuota",       $("#txtPorcentajeCuota").val(),
    	              "pClienteRazonSocial",       $("#txtRazonSocial").val(),
    	              "pClienteDomicilioLegal",       $("#txtaDomicilioCliente").val()
                     ];

    fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/GuardarContratoYGenerarAnexos",
                   paramArray,
                   fn_ResultadoGenerarAnexos,
                   function(result) {
                       parent.fn_mdl_mensajeIco(result.responseText, "util/images/warning.gif", "GENERAR CONTRATO");
                   });
}

//****************************************************************
// Función		:: 	fn_ResultadoGenerarAnexos
// Descripción	::	Abre anexos
// Log			:: 	EBL - 25/05/2012
//****************************************************************
function fn_ResultadoGenerarAnexos(result) {
    var vResult = result.split('|');
    
    if (vResult[0] == "0") {
        var strRutaArchivo = vResult[1];
        var strNombreArchivo = vResult[1].split('\\').pop();
        strNombreArchivo = strNombreArchivo.substr(28, strNombreArchivo.length);
        $("#hddAdjuntarArchivo").val(strRutaArchivo);
        var img = "<img src='../../Util/images/ico_download.gif' alt='Descargar archivo de anexo' title='Descargar archivo de anexos' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(strRutaArchivo) + "');\" style='cursor:pointer;cursor: hand;' />";
        var lnk = "<a href='#' onclick=\"javascript:fn_abreArchivo('" + encodeURIComponent(strRutaArchivo) + "');\">" + strNombreArchivo + "</a>";

        $("#dv_DescargarArchivoContrato").show();
        $("#dv_DescargarArchivoContrato").html(img + "&nbsp;&nbsp;" + lnk);

        var adjuntarArchivoContrato;
        adjuntarArchivoContrato = "<a href=\"javascript:fn_AdjuntarArchivoDocumento('ArchivoContratoAdjunto');\">";
        adjuntarArchivoContrato = adjuntarArchivoContrato + "\n<img style=\"cursor: pointer;cursor: hand;width: 35px;height: 35px;border: 0;\" id=\"imgArchivoContratoAdjunto\" title=\"Adjuntar correo\" alt=\"\" src=\"../../Util/images/ico_acc_adjuntarMini.gif\" />";
        adjuntarArchivoContrato = adjuntarArchivoContrato + "\n<br />Adjuntar Contrato";
        adjuntarArchivoContrato = adjuntarArchivoContrato + "\n</a>";
        $("#dv_AdjuntarArchivoContrato").html(adjuntarArchivoContrato);
        $("#hddGeneraContrato_Adjunto").val("1");

        parent.fn_mdl_mensajeIco("Se actualizó con éxito el contrato y se generó con éxito el contrato.", "util/images/ok.gif", "CONTRATO");
    } else {
        parent.fn_mdl_mensajeIco("Ocurrió un error al actualizar el contrato y generar los anexos.", "util/images/warning.gif", "ACTUALIZACIÓN DE CONTRATO");
    }
}

//****************************************************************
// Función		:: 	fn_aprobar
// Descripción	::	Le pide confirmación para aprobar y pasar el contrato a estado pendiente de firma.
// Log			:: 	EBL - 25/05/2012
//****************************************************************
function fn_aprobar() {

   var codigoEstadoContrato = $("#hddCodigoEstadoContrato").val();
    
   if ($("#hddFlagModificado").val() == "0") {
        if (codigoEstadoContrato == CodigoEstadoContrato.EnviadoAlCliente) {
            parent.fn_mdl_confirma("¿Está seguro de realizar la aprobación del cliente?",
                                   fn_aprobarContratoWM,
                                   "util/images/warning.gif",
                                   function() { }, 
                                   "APROBAR CONTRATO"
                                   );
        }
   } else {
        parent.fn_mdl_mensajeIco("No se puede aprobar por que existen cambios en el contrato", "util/images/warning.gif", "ACTUALIZACIÓN DE CONTRATO");
   }   
}

//****************************************************************
// Función		:: 	fn_aprobarContratoWM
// Descripción	::	Aprueba el contrato a través del llamado a web method.
// Log			:: 	EBL - 25/05/2012
//****************************************************************
function fn_aprobarContratoWM() {
    var paramArray = [
                      "pCodigoContrato", $("#txtNroContrato").val(),
    	              "pClienteRazonSocial", $("#txtRazonSocial").val(),
    	              "pClienteDomicilioLegal", $("#txtaDomicilioCliente").val()
                     ];

	fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/Aprobar",
		           paramArray,
		           ResultadoAprobar,
                   function(result) {
                       var err = eval("(" + result.responseText + ')');
                       parent.fn_mdl_mensajeIco(err.Message, "util/images/warning.gif", "APROBAR CONTRATO");
                   });
}

//****************************************************************
// Función		:: 	ResultadoAprobar
// Descripción	::	Si la operación del ejecución del webmethod de aprobación del contrato fue exitosa, muestra un mensaje al usuario y lo
//                  redirecciona al listado del contrato.
//                  Caso contrario le muestra un mensaje de error al usuario.
// Log			:: 	EBL - 25/05/2012
//****************************************************************
function ResultadoAprobar(result) {
    if (result == "0") {
        parent.fn_mdl_mensajeIco("Se aprobó con éxito el contrato.", "util/images/ok.gif", "APROBAR CONTRATO");

        fn_util_redirect('frmContratoListado.aspx');
    } else {
        parent.fn_mdl_mensajeIco("Ocurrió un error al aprobar el contrato.", "util/images/warning.gif", "APROBAR CONTRATO");
    }
}

//****************************************************************
// Función		:: 	        
// Descripción	::	Le pide confirmación para enviar el contrato a notaria.
// Log			:: 	EBL - 25/05/2012
//****************************************************************
function fn_enviar_notaria() {
var strError = new StringBuilderEx();
        //INICIO IBK - RPH
        if ($("#jqGrid_lista_G").getGridParam("reccount") == 0) {
            strError.append("- Ingrese al menos una notaria.");
            parent.fn_mdl_mensajeIco(strError.toString(), "Util/images/warning.gif", "ENVIAR NOTARIA");
        }
        //FIN
    else {
    if ($(  "#hddFlagModificado").val() == "0") {
        parent.fn_mdl_confirma("¿Está seguro de efectuar el envío a notaría?"
		                        , function () {
	                                var paramArray = [
                                                      "pCodigoContrato", $("#txtNroContrato").val(),
	                                	              "pClienteRazonSocial", $("#txtRazonSocial").val(),
    	                                              "pClienteDomicilioLegal", $("#txtaDomicilioCliente").val()
                                                     ];
                                    fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/EnviaNotaria",
                                                    paramArray,
                                                    ResultadoAprobarEnvioNotaria,
                                                    function(result) {
                                                        var err = eval("(" + result.responseText + ')');
                                                        parent.fn_mdl_mensajeIco(err.Message, "util/images/warning.gif", "ENVÍO A NOTARÍA");
                                                    });}
		                        , "util/images/question.gif"
		                        , function() { }
		                        , "ENVÍO A NOTARIA"
	                            );
    } else {
        parent.fn_mdl_mensajeIco("No se envió a notaria. Existen modificaciones en el contrato.", "util/images/warning.gif", "ENVÍO A NOTARÍA");
    }
  }
}

//****************************************************************
// Función		:: 	ResultadoAprobarEnvioNotaria
// Descripción	::	Si la operación del ejecución del webmethod de envío a notaria fue exitosa, muestra un mensaje al usuario y lo
//                  redirecciona al listado del contrato.
//                  Caso contrario le muestra un mensaje de error al usuario.
// Log			:: 	EBL - 25/05/2012
//****************************************************************
function ResultadoAprobarEnvioNotaria(result) {
   if (result == "0") {
       parent.fn_mdl_mensajeIco("Se envió a notaría con éxito el contrato.", "util/images/ok.gif", "ENVÍO A NOTARÍA");

       fn_util_redirect('frmContratoListado.aspx');
   } else {
       parent.fn_mdl_mensajeIco("Ocurrió un error al aprobar el contrato.", "util/images/warning.gif", "ENVÍO A NOTARÍA");
   }
}

function fn_Anular() {
    var nroContrato = $("#txtNroContrato").val();
    var codigoEstadoContrato = $("#hddCodigoEstadoContrato").val();

    if (codigoEstadoContrato == CodigoEstadoContrato.EnElaboracion  || 
        codigoEstadoContrato == CodigoEstadoContrato.PendienteDeCarta ||
        codigoEstadoContrato == CodigoEstadoContrato.EnviadoAlCliente ||
        codigoEstadoContrato == CodigoEstadoContrato.PendienteDeFirma) {
        parent.fn_mdl_confirma("¿Desea anular el contrato " + nroContrato + "?"
		                       , fn_AnularContratoWM
		                       , "util/images/warning.gif"
		                       , function() { }
		                       , "ANULAR CONTRATO");
    }
}

function fn_AnularContratoWM() {
    var paramArray = [
                      "pCodigoContrato", $("#txtNroContrato").val()
                     ];
    fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/Anular",
                   paramArray,
                   ResultadoAnular,
                   function(result) {
                       var err = eval("(" + result.responseText + ')');
                       parent.fn_mdl_mensajeIco(err.Message, "util/images/warning.gif", "ANULAR CONTRATO");
                   });
}

function ResultadoAnular(result) {
    if (result == "0") {
        parent.fn_mdl_mensajeIco("Se anuló con éxito el contrato.", "util/images/ok.gif", "ANULAR CONTRATO");

        fn_util_redirect('frmContratoListado.aspx');
    } else {
        parent.fn_mdl_mensajeIco("Ocurrió un error al anular el contrato.", "util/images/warning.gif", "ANULAR CONTRATO");
    }
}

/*function fn_cargaComboNotaria(departamento, notaria) {
    var arrParametros = [
                            "pstrDepartamento", departamento, 
                            "pstrOp", "8"
                        ];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $(notaria).html(arrResultado[1]);
        }
    }
}
*/
//Inicio IBK - RPH
function fn_cargaComboNotaria(departamento, provincia, notaria) {
    var arrParametros = [
                            "pstrDepartamento", departamento,
                            "pstrProvincia", provincia,
                            "pstrOp", "8"
                        ];

    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    //IBK - RPH limpio los contactos de Notaria
    $("#txtContactoNotario").val('');
    $("#txtCorreoNotaria").val('');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $(notaria).html(arrResultado[1]);
        }
    }

}

//Inicio IBK - RPH
function fn_ObtenerContactoNotarias(codnotaria) {
    var arrParametros = [
                            "pstrCodNotaria", codnotaria,
                            "pstrOp", "11"
                        ];

    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        var contactos = arrResultado[0].split("*");
        //if (arrResultado[0] == "0") 
        //{
        for (i = 0; i < contactos.length; i++) {

            if (i == 0) {
                $("#txtContactoNotario").val(contactos[i]);
            }
            else {
                $("#txtCorreoNotaria").val(contactos[i]);
            }
        }
        //}
    }
}
//FIN
function fn_ExisteArrendamientoFinanciero() {
    var bResult;
    var paramArray = [
                         "pCodigoContrato", $("#txtNroContrato").val()
                     ];
    fn_util_AjaxSyncWM("frmConsultaSituacionCreditoRegistro.aspx/ExisteArrendamientoFinanciero",
                       paramArray,
                       function(result) {
                           if (result == "0") {
                               bResult = true;
                           } else {
                               bResult = false;
                           }
                       },
                       function() {
                           bResult = false;
                       });

    return bResult;
}

//****************************************************************
// Función		:: 	fn_abreSeguimiento
// Descripción	::	Muestra la ventana modal de representantes del cliente disponibles, permitiendo seleccionar al usuario 
//                  cuales agregar al contrato.
// Log			:: 	ijm - 25/02/2012
//****************************************************************
function fn_abreSeguimiento() {
    var sTitulo = "Contrato";
    var sSubTitulo = "Contrato :: Editar";
    var strCodDocContrato = $("#txtNroContrato").html();
 
    parent.fn_util_AbreModal(sSubTitulo, "Comun/frmContratoSeguimientoListado.aspx?Titulo=" + sTitulo + "&sSubTitulo=" + sSubTitulo + "&hddCodigoContrato=" + strCodDocContrato + "&Add=False", 750, 500, function() { });
}

//****************************************************************
// Función		:: 	fn_FirmarContrato
// Descripción	::	
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_FirmarContrato() {
    if (ValidarEstadoActual()) {
        if ($("#hddFlagModificado").val() == "0") {

            parent.fn_mdl_confirma("¿Está seguro de cambiar el estado ha formalizado?"
                                    , function() {
                                                    var paramArray = [
                                                                       "pCodigoContrato", $("#txtNroContrato").val(),
                                                                       "pFechaFirmaNotaria", $("#txtFechaFirmaNotaria").val(),
                                                    	               "pClienteRazonSocial", $("#txtRazonSocial").val(),
    	                                                               "pClienteDomicilioLegal", $("#txtaDomicilioCliente").val()
                                                                     ];
                                        
                                                    fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/Formalizado",
                                                                       paramArray,
                                                                       ResultadoAprobarFormalizado,
                                                                       function(result) {
                                                                           var err = eval("(" + result.responseText + ')');
                                                                           parent.fn_mdl_mensajeIco(err.Message, "util/images/warning.gif", "FORMALIZADO");
                                                                       });
                                    }
                                    , "util/images/question.gif"
                                    , function() { }
                                    , "FIRMA CONTRATO"
                                    );
              } else {
                parent.fn_mdl_mensajeIco("No se firmó el contrato. Hay modificaciones en el contrato", "util/images/warning.gif", "ENVÍO A NOTARÍA");
            }
    }
}

function ResultadoAprobarFormalizado(result) {
    if (result == "0") {
        parent.fn_mdl_mensajeIco("Se Formalizó con éxito el contrato.", "util/images/ok.gif", "FORMALIZADO");
        fn_util_redirect('frmContratoListado.aspx');
    } else {
        parent.fn_mdl_mensajeIco("Ocurrió un error al aprobar el contrato.", "util/images/warning.gif", "FORMALIZADO");
    }
}

//****************************************************************
// Función		:: 	fn_ValidaBloqueo() 
// Descripción	::	Lista Ejecutivos
// Log			:: 	JRC - 18/07/2012
//****************************************************************
function fn_ValidaBloqueo() {
    trace("fn_ValidaBloqueo-start");

    var strBloqueoExistente = $("#hddBloqueoExistente").val();
    var strBloqueoCodigo = $("#hddBloqueoCodigo").val();
    var strBloqueoCodUsuario = $("#hddBloqueoCodUsuario").val();
    var strBloqueoNomUsuario = $("#hddBloqueoNomUsuario").val();
    var strBloqueoFecha = $("#hddBloqueoFecha").val();

    if (fn_util_trim(strBloqueoExistente) == "1") {

        parent.fn_mdl_confirmaBloqueo(
                        "El contrato está siendo modificado por el usuario (" + strBloqueoCodUsuario + ") " + strBloqueoNomUsuario + " desde la fecha " + strBloqueoFecha + " ¿Desea continuar con la modificación?"
                        , function() {
                             fn_ActualizaBloqueo(strBloqueoCodigo);
                        }
                        , "Util/images/img_bloqueo.gif"
                        , function() {
                             fn_util_redirect('frmContratoListado.aspx');
                        }
                        , null
                        );

                    }
    trace("fn_ValidaBloqueo-end");
}

//****************************************************************
// Función		:: 	fn_ActualizaBloqueo
// Descripción	::	Actualiza Bloqueo
// Log			:: 	JRC - 18/07/2012
//****************************************************************
function fn_ActualizaBloqueo(pstrBloqueoCodigo) {

    //Consulta Ultimus
    var arrParametros = ["pstrCodBloqueo", pstrBloqueoCodigo];
    fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/ActualizaBloqueo",
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

//****************************************************************
// Función		:: 	Valida % Del Importe Pendiente de Pago, por Día de Atraso 
// Descripción	::	
// Log			:: 	ijm- 30/07/2012
//****************************************************************
 $("#txtImporteAtrasoPorc").focusout(function() {
     var decTea = fn_util_ValidaDecimal($('#txtImporteAtrasoPorc').val());

     if (decTea >= 100) {
         $('#txtImporteAtrasoPorc').val(fn_util_ValidaMonto("0", 3));
         parent.fn_util_MuestraLogPage("El Porcentaje del Importe Pendiente de Pago no es correcto", "E");
     } else {
        $('#txtImporteAtrasoPorc').val(fn_util_ValidaMonto("0", 3));
     }
 });

//****************************************************************
// Función		:: 	fn_cargaCombo
// Descripción	::	
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_cargaCombo(cmb, pstrTablaGenerica) {

    var arrParametros = [
                            "pstrTablaGenerica", pstrTablaGenerica,
                            "pstrOp", "1"
                        ];
   var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

   if (arrResultado.length > 0) {
       if (arrResultado[0] == "0") {
           $('#' + cmb).html(arrResultado[1]);
       } else {
           var strError = arrResultado[1];
           parent.fn_mdl_mensajeIco(strError.toString(), "util/images/error.gif", "ERROR AL LISTAR");
       }
   }
}

//****************************************************************
// Función		:: 	fn_cargaDepartamento
// Descripción	::	
// Log			:: 	EBL - 25/02/2012
//****************************************************************
function fn_cargaDepartamento(cmb) {
    trace("fn_cargaDepartamento-" + cmb + "-start");
    var arrParametros = ["pstrOp", "0"];
    var arrResultado = fn_util_ejecutarASHX(arrParametros, 'whUtil', '../../');

    if (arrResultado.length > 0) {
        if (arrResultado[0] == "0") {
            $('#' + cmb).html(arrResultado[1]);
        } else {
            var strError = arrResultado[1];
            parent.fn_mdl_mensajeIco(strError.toString(), "util/images/error.gif", "ERROR AL LISTAR");
        }
    }
    trace("fn_cargaDepartamento-" + cmb + "-end");
}

function trace(funcion) {
    var d = new Date();
    sTrace.append($.format.date(d.toString(), "hh:mm:ss") + ":" + d.getMilliseconds() + " " +funcion + "\n");
}

function go() {
    var lapso = fin - inicio;

    sTrace.append(segundos(lapso));
    $('#TextArea1').val(sTrace.toString());
}

function segundos(t) {
    if (t<1000) {
        return t;
    } else {
        return Math.round(t / 1000).toString() + ":" + (t % 1000).toString();
    }
}


//****************************************************************
// Función		:: 	fn_retornarFlujo
// Descripción	::	Retornar el FLujo General
// Log			:: 	JRC - 05/09/2012
//****************************************************************
function fn_retornarFlujo(){

	parent.fn_mdl_confirma(
                    "¿Está seguro que desea retornar a Comercial?",
                    function() {
                        
                        parent.fn_blockUI();          
                        var paramArray = ["pstrCodigoContrato", $("#txtNroContrato").val()];
						fn_util_AjaxWM("frmConsultaSituacionCreditoRegistro.aspx/GestionFlujo",
							   paramArray,
							   function(resultado) {
									parent.fn_unBlockUI();
									parent.fn_mdl_mensajeOk("Se retornó correctamente el Contrato", function() { fn_util_redirect('frmContratoListado.aspx') }, "RETORNO CORRECTO");
							   },
							   function(resultado) {
								   parent.fn_unBlockUI();
								   parent.fn_mdl_mensajeIco("Se produjo un error al retornar a Comercial", "util/images/error.gif", "ERROR EN RETORNAR");
							   }
						);
                        
                    },
                    "Util/images/question.gif",
                    function() { },
                    'CONFIRMACION'
	);

}

