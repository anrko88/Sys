<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmContratoRegistro.aspx.vb"
    Inherits="Formalizacion_frmContratoRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GCC :: Sistema de Gestión de Leasing</title>
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../Util/images/PV16x16.ico" />
    <!-- Estilos -->
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/jquery/jquery.jscrollpane.css"
        media="all" />
    <link type="text/css" rel="stylesheet" href="../Util/css/css_global.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/css_formulario.css" />
    <link type="text/css" rel="stylesheet" href="../Util/css/css_fuente.css" />
    <!-- JavaScript -->

    <script type='text/javascript' src="../Util/js/jquery/jquery-1.6.2.min.js"></script>

    <script type='text/javascript' src="../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"></script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.jscrollpane.min.js"></script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.mousewheel.js"></script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.ui.global.js"></script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.validText.js"></script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.validNumber.js"></script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.maxlength.js"></script>

    <script type="text/javascript" src="../Util/js/js_global.js"></script>

    <script type='text/javascript' src="../Util/js/js_util.modal.js"></script>

    <script type='text/javascript' src="../Util/js/js_util.funcion.js"></script>

    <script type='text/javascript' src="../Util/js/js_util.date.js"></script>

    <script type='text/javascript' src="../Util/js/js_util.ajax.js"></script>

    <script src="../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>

    <script src="../Util/js/jquery/jshashtable.js" type="text/javascript"></script>

    <script src="../Util/js/js_util.Grilla.js" type="text/javascript"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->


    <script src="frmContratoRegistro.aspx.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmContratoRegistro" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!--
        BOTONES
        -->
        <table id="tb_cuerpoCabecera" style="border: 0;" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Formalización</div>
                    <div class="css_lbl_titulo">
                        Contrato :: Editar</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_Volver" class="dv_img_boton">
                        <a href="javascript:fn_Cancelar();">
                            <img alt="Volver" src="../Util/images/ico_acc_cancelar.gif" style="border: 0" title="Volver" /><br />
                            Volver </a>
                    </div>
                    <div id="dv_img_Guardar" class="dv_img_boton">
                        <a href="javascript:fn_grabar();">
                            <img alt="Guardar" src="../Util/images/ico_acc_grabar.gif" style="border: 0" title="Guardar" /><br />
                            Guardar </a>
                    </div>
                    <div id="dv_img_EnviarNotaria" class="dv_img_boton" style="width: 82px;">
                        <a href="javascript:fn_enviar_notaria();">
                            <img alt="Guardar" src="../Util/images/ico_acc_grabar.gif" style="border: 0" title="Guardar" /><br />
                            Enviar Notaria </a>
                    </div>
                    <div id="dv_img_GuardarEnviar" class="dv_img_boton" style="width: 82px;">
                        <a href="javascript:fn_grabaryEnviar();">
                            <img alt="Guardar y enviar el contrato" src="../Util/images/ico_acc_grabarEnviar.gif"
                                style="border: 0" title="Guardar y enviar el contrato" /><br />
                            Guardar y Enviar </a>
                    </div>
                    <div id="dv_img_Anular" class="dv_img_boton" style="width: 82px; visibility: hidden;
                        display: none;">
                        <a href="javascript:fn_Anular();">
                            <img alt="Anular el contrato" src="../Util/images/ico_acc_grabarEnviar.gif" style="border: 0"
                                title="Anular el contrato" /><br />
                            Anular </a>
                    </div>
                    <div id="dv_aprobarContrato">
                        <div id="dv_img_boton" class="dv_img_boton" style="width: 82px;">
                            <a href="javascript:fn_aprobar();">
                                <img alt="Aprobar el contrato" src="../Util/images/ico_aprobar_act.gif" style="border: 0;
                                    width: 32px; height: 32px;" title="Aprobar el contrato" /><br />
                                Aprobar </a>
                        </div>
                    </div>
                    <div id="dv_FirmarContrato" style="display: none">
                        <div id="dv_img_boton" class="dv_img_boton" style="width: 82px;">
                            <a href="javascript:fn_FirmarContrato();">
                                <img alt="Firmar el contrato" src="../Util/images/ico_acc_firmar.png" style="border: 0;
                                    width: 32px; height: 32px;" title="Firmar el contrato" /><br />
                                Firmar Contrato </a>
                        </div>
                    </div>
                    <div id="dv_AdjuntarArchivoContrato" class="dv_img_boton" style="width: 92px;">
                    </div>
                    <div id="dv_img_GenerarContrato" class="dv_img_boton" style="width: 92px;">
                        <a href="javascript:fn_GenerarContrato();">
                            <img alt="Generar el contrato" src="../Util/images/ico_acc_editar.gif" title="Generar el contrato"
                                style="border: 0" /><br />
                            Generar Contrato </a>
                    </div>
                    <div id="dv_btnSeguimiento" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_abreSeguimiento();">
                            <img alt="" src="../Util/images/ico_seguimiento.gif" style="border: 0" /><br />
                            Seguimiento </a>
                    </div>
                    <div id="dv_btnRetornaFlujo" class="dv_img_boton" style="width: 70px;">
                        <a href="javascript:fn_retornarFlujo();">
                            <img alt="" src="../Util/images/ico_acc_retornar.gif" style="border: 0" /><br />
                            Retornar </a>
                    </div>
                </td>
            </tr>
        </table>
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" cellpadding="0" cellspacing="0" style="width: 100%; border: 0;">
                <tr>
                    <td class="lineas">
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <!--
        				CABECERA DEL CONTRATO
        				-->
                        <table id="tb_tabla_comunCabecera" cellpadding="0" cellspacing="0" style="border: 0;">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Contrato
                                </td>
                                <td class="botones">
                                    <img alt="" src="../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <div style="display: none">
                            <textarea id="TextArea1" cols="200" rows="15" style="width: 600px;"></textarea>
                            <input id="Button1" type="button" value="button" onclick="go();" /></div>
                        <div id="dv_datos" class="dv_tabla_contenedora">
                            <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                <colgroup>
                                    <col style="width: 16.66%;" />
                                    <col style="width: 16.66%;" />
                                    <col style="width: 16.66%;" />
                                    <col style="width: 16.66%;" />
                                    <col style="width: 16.66%;" />
                                    <col style="width: 16.66%;" />
                                </colgroup>
                                <tr>
                                    <td class="label">
                                        Nº Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtNroContrato" type="text" class="css_input_inactivo" readonly="readonly"
                                            style="width: 100px;" runat="server" />
                                    </td>
                                    <td class="label">
                                        Estado del Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtEstadoDelContrato" name="txtEstadoDelContrato" type="text" class="css_input_inactivo"
                                            readonly="readonly" style="width: 160px;" runat="server" />
                                    </td>
                                    <td class="label">
                                        F. Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaContrato" name="txtFechaContrato" type="text" class="css_input_inactivo"
                                            readonly="readonly" style="width: 100px;" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Fecha Máx. de Disponibilidad
                                    </td>
                                    <td class="input">
                                        <input id="txtFechamaxdisp" name="txtFechamaxdisp" type="text" class="css_input_inactivo"
                                            style="width: 100px;" runat="server" />
                                    </td>
                                    <td class="label">
                                        Fecha Máx. de Activación
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaActivacion" name="txtFechaActivacion" type="text" class="css_input_inactivo"
                                            runat="server" style="width: 100px;" />
                                    </td>
                                    <td class="label">
                                        Periodo de Disponibilidad
                                    </td>
                                    <td class="input">
                                        <input id="txtPeriodoDisponible" name="txtPeriodoDisponible" type="text" class="css_input_inactivo"
                                            readonly="readonly" style="width: 100px; text-align: right;" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Clasificación del Bien
                                    </td>
                                    <td class="input">
                                        <input id="txtClasificacionDelBien" name="txtClasificacionDelBien" type="text" class="css_input_inactivo"
                                            readonly="readonly" style="width: 160px;" runat="server" />
                                    </td>
                                    <td class="label">
                                        Tipo de Bien
                                    </td>
                                    <td class="input">
                                        <input id="txtTipoDeBien" name="txtTipoDeBien" type="text" class="css_input_inactivo"
                                            readonly="readonly" style="width: 160px;" runat="server" />
                                    </td>
                                    <td class="label">
                                        Procedencia
                                    </td>
                                    <td class="input">
                                        <input id="txtProcedencia" name="txtProcedencia" type="text" class="css_input_inactivo"
                                            readonly="readonly" style="width: 160px;" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Moneda
                                    </td>
                                    <td class="input">
                                        <input id="txtMoneda" name="txtMoneda" type="text" class="css_input_inactivo" readonly="readonly"
                                            style="width: 160px;" runat="server" />
                                    </td>
                                    <td class="label">
                                        Precio Venta
                                    </td>
                                    <td class="input">
                                        <input id="txtMontoFinanciado" name="txtMontoFinanciado" type="text" class="css_input_inactivo"
                                            readonly="readonly" style="width: 100px; text-align: right;" runat="server" />
                                    </td>
                                    <td class="label">
                                        Ejecutivo Leasing
                                    </td>
                                    <td class="input">
                                        <input id="txtEjecutivoLeasing" name="txtEjecutivoLeasing" type="text" class="css_input_inactivo"
                                            readonly="readonly" style="width: 160px;" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Clasificación de Contrato
                                    </td>
                                    <td class="input">
                                        <select id="cmbClasificacionContrato" name="cmbClasificacionContrato">
                                        </select>
                                    </td>
                                    <td class="label" id="dv_lblRegistrosPublicos">
                                        <div id="dv_RegistrosPublicos">
                                            Registros Públicos
                                        </div>
                                    </td>
                                    <td class="input">
                                        <div id="dv_FechaRegistroPublico">
                                            <input id="chkRegistroPublico" name="chkRegistroPublico" type="checkbox" runat="server" />
                                            <input id="txtFechaRegistroPublico" name="txtFechaRegistroPublico" type="text" class="css_input_inactivo"
                                                size="11" readonly="readonly" runat="server" />
                                        </div>
                                    </td>
                                    <td class="label">
                                        <!-- Inicio IBK - AAE - Cambio Etiqueta 
                                        Firmado en Notaría -->
                                        Fecha Escritura Pública
                                    </td>
                                    <td class="input">
                                        <input id="chkFirmaNotaria" name="chkFirmaNotaria" type="checkbox" runat="server" />
                                        <input id="txtFechaFirmaNotaria" name="txtFechaFirmaNotaria" type="text" class="css_input_inactivo"
                                            size="11" readonly="readonly" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Contrato Adjunto
                                    </td>
                                    <td class="input" colspan="5">
                                        <table style="border: 0;">
                                            <tr>
                                                <td>
                                                    <div id="dv_DescargarArchivoContrato">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <!--
        				INICIA TABS
        				-->
                        <div id="divTabs" style="border: 0; background: none;" class="dv_tabla_contenedora">
                            <ul>
                                <li><a href="#tab-6">DATOS DEL CLIENTE</a></li>
                                <li><a href="#tab-0">DATOS DEL BIEN</a></li>
                                <li><a href="#tab-1">INFORMACIÓN REQUERIDA</a></li>
                                <li><a href="#tab-2">REPRESENTANTES A FIRMAR</a></li>
                                <li><a href="#tab-3">OTROS CONCEPTOS</a></li>
                                <li><a href="#tab-4">DATOS NOTARIALES</a></li>
                                <li id="li_adenda"><a href="#tab-5">ADENDAS</a></li>
                            </ul>
                            <!--
        			         TAB :: DATOS DEL CLIENTE
        			         !-->
                            <div id="tab-6">
                                <div id="dv_datosCliente" style="border: 0px;">
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Datos del Cliente</legend>
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                            <colgroup>
                                                <col style="width: 25%;" />
                                                <col style="width: 25%;" />
                                                <col style="width: 25%;" />
                                                <col style="width: 25%;" />
                                            </colgroup>
                                            <tr>
                                                <td class="label">
                                                    CU Cliente
                                                </td>
                                                <td class="input">
                                                    <input id="txtCodUnico" name="txtCodUnico" type="text" class="css_input_inactivo"
                                                        size="14" readonly="readonly" runat="server" />
                                                </td>
                                                <td class="label">
                                                    Razón social o Nombre
                                                </td>
                                                <td class="input">
                                                    <input id="txtRazonSocial" name="txtRazonSocial" type="text" class="css_input" runat="server"
                                                        style="width: 250px;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Tipo Persona
                                                </td>
                                                <td class="input">
                                                    <input id="txtTipoPersona" name="txtTipoPersona" type="text" class="css_input_inactivo"
                                                        readonly="readonly" runat="server" style="width: 160px;" />
                                                </td>
                                                <td class="label" id="td_EstadoCivil">
                                                    Estado Civil
                                                </td>
                                                <td class="input">
                                                    <select id="cmbEstadoCivil" name="cmbEstadoCivil" class="css_input">
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Tipo de Documento
                                                </td>
                                                <td class="input">
                                                    <input name="txtTipoDocumento" id="txtTipoDocumento" runat="server" type="text" class="css_input_inactivo"
                                                        size="14" readonly="readonly" />
                                                </td>
                                                <td class="label">
                                                    Nº de Documento
                                                </td>
                                                <td class="input">
                                                    <input id="txtNroDeDocumento" name="txtNroDeDocumento" type="text" class="css_input_inactivo"
                                                        size="14" readonly="readonly" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Domicilio
                                                </td>
                                                <td class="input" colspan="2">
                                                    <textarea id="txtaDomicilioCliente" name="txtaDomicilioCliente" class="css_input"
                                                        rows="3" cols="90" runat="server"></textarea>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset id="fs_DatosConyugue">
                                        <legend class="css_lbl_subTitulo">Datos del Cónyuge</legend>
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                            <colgroup>
                                                <col style="width: 25%;" />
                                                <col style="width: 25%;" />
                                                <col style="width: 25%;" />
                                                <col style="width: 25%;" />
                                            </colgroup>
                                            <tr>
                                                <td class="label">
                                                    Nombre del Cónyuge
                                                </td>
                                                <td class="input">
                                                    <input id="txtNombreConyuge" name="txtNombreConyuge" type="text" class="css_input"
                                                        runat="server" style="width: 350px;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Tipo de Documento
                                                </td>
                                                <td class="input">
                                                    <select name="cmbTipoDocumentoConyuge" id="cmbTipoDocumentoConyuge">
                                                    </select>
                                                </td>
                                                <td class="label">
                                                    Nro. de Documento
                                                </td>
                                                <td class="input">
                                                    <input id="txtnumerodocumento" name="txtnumerodocumento" type="text" class="css_input"
                                                        size="14" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Documento de Separación
                                                </td>
                                                <td colspan="3" class="input">
                                                    <table style="border: 0;">
                                                        <tr>
                                                            <td>
                                                                <img title="Adjuntar correo" id="imgAdjuntarArchivoDocumentoSeparacion" style="cursor: pointer;
                                                                    cursor: hand;" onclick="javascript:fn_AdjuntarArchivoDocumento('ArchivoDocumentoSeparacion');"
                                                                    alt="" src="../Util/images/ico_acc_adjuntarMini.gif" />
                                                            </td>
                                                            <td>
                                                                <div id="dv_AdjuntarArchivoDocumentoSeparacion" style="border: 0px;">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Datos de Representantes a Firmar</legend>
                                        <table id="tb_formulario" cellpadding="0" cellspacing="0" style="border: 0;">
                                            <tr>
                                                <td>
                                                    <div id="dv_AccionesRepresentantes" style="border: 0;">
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_AgregarRepresentantes();" style="display: inline;">
                                                                <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;
                                                                    display: inline; border: 0px;" />Agregar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_EliminarRepresentantes();">
                                                                <img alt="" src="../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;
                                                                    border: 0px;" />Eliminar </a>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="jqGrid_lista_H">
                                                        <tr>
                                                            <td />
                                                        </tr>
                                                    </table>
                                                    <div id="jqGrid_pager_H">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>
                            <!--
        			         TAB :: DATOS DEL BIEN
        			         !-->
                            <div id="tab-0">
                                <!-- Datos Inmueble -->
                                <div id="dvDatosBien" style="border: 0;">
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Datos del Bien</legend>
                                        <table id="tb_DatosInmueble" border="0">
                                            <tr>
                                                <td>
                                                    <table id="tb_formulario" cellpadding="0" cellspacing="3" style="border: 0;">
                                                        <colgroup>
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="label">
                                                                Uso
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtUsoInmueble" name="txtUsoInmueble" type="text" class="css_input" size="50"
                                                                    runat="server" />
                                                            </td>
                                                            <td class="label">
                                                                Ubicación
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtUbicacionInmueble" name="txtUbicacionInmueble" type="text" class="css_input"
                                                                    size="50" runat="server" />
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Descripción
                                                            </td>
                                                            <td class="input">
                                                                <textarea id="txtDescripcionInmueble" name="txtDescripcionInmueble" rows="2" cols="51"
                                                                    runat="server"></textarea>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Cantidad
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtCantidadInmueble" name="txtCantidadInmueble" type="text" class="css_input"
                                                                    size="12" style="text-align: right;" />
                                                            </td>
                                                            <td class="label">
                                                                Estado del Bien
                                                            </td>
                                                            <td class="input">
                                                                <select id="cmbEstadoBienInmueble" name="cmbEstadoBienInmueble" runat="server">
                                                                </select>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Departamento
                                                            </td>
                                                            <td class="input">
                                                                <select id="cmbDepartamentoInmueble" runat="server" name="cmbDepartamentoInmueble"
                                                                    onchange="javascript:fn_cargaComboProvincia('#cmbProvinciaInmueble','#cmbDistritoInmueble',this.value);">
                                                                </select>
                                                            </td>
                                                            <td class="label">
                                                                Provincia
                                                            </td>
                                                            <td class="input">
                                                                <select id="cmbProvinciaInmueble" name="cmbProvinciaInmueble" onchange="javascript:fn_cargaComboDistrito('#cmbDistritoInmueble',cmbDepartamentoInmueble.value, this.value);">
                                                                    <option value="0">[- Seleccionar -]</option>
                                                                </select>
                                                            </td>
                                                            <td class="label">
                                                                Distrito
                                                            </td>
                                                            <td class="input">
                                                                <select id="cmbDistritoInmueble" name="cmbDistritoInmueble">
                                                                    <option value="0">[- Seleccionar -]</option>
                                                                </select>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Partida Registral
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtPartidaRegistralInmueble" name="txtPartidaRegistralInmueble" type="text"
                                                                    class="css_input" size="50" />
                                                            </td>
                                                            <td class="label">
                                                                Oficina Registral
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtOficinaRegistralInmueble" name="txtOficinaRegistralInmueble" type="text"
                                                                    class="css_input" size="50" />
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <div id="dv_AccionesBien" style="border: 0;">
                                                        <div class="dv_img_boton_mini" style="border: 0; height: 22px; float: right;">
                                                            <a href="javascript:fn_GuardarBienInmuebleNuevo();">
                                                                <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />&nbsp;Agregar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="border: 0; height: 22px; float: right;">
                                                            <a href="javascript:fn_eliminarDetDatosBien();">
                                                                <img alt="" src="../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />Eliminar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="border: 0; height: 22px; float: right;">
                                                            <a href="javascript:fn_EditarDetDatosBien();">
                                                                <img alt="" src="../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />&nbsp;&nbsp;&nbsp;Editar </a>
                                                        </div>
                                                        <%--
                                                                    <div class="dv_img_boton_mini" style="border:0;height:22px;float:right;">
                                                                <a href="javascript:fn_ReplicarDetDatosBien();">
                                                                    <img alt="" src="../Util/images/ico_acc_replicar.gif" style="width:16px; height:16px;border:0;" />&nbsp;Replicar
                                                                </a>
                                                             </div>
                                                        <div class="dv_img_boton_mini" style="height:22px;float:right;border:0;">
                                                                <a href="javascript:fn_CancelarBienInmueble();">
                                                                    <img alt="" src="../Util/images/ico_acc_limpiar.gif" style="width:16px; height:16px;border:0;" />&nbsp;&nbsp;&nbsp;Limpiar
                                                                </a>
                                                            </div>--%>
                                                    </div>
                                                    <div id="dv_ProcesoBien" style="border: 0;">
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_GuardarBienInmueble();">
                                                                <img alt="" src="../Util/images/ico_acc_grabar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />Guardar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_CancelarBienInmueble();">
                                                                <img alt="" src="../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />&nbsp;&nbsp;&nbsp;Cancelar </a>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="jqGrid_lista_A">
                                                        <tr>
                                                            <td />
                                                        </tr>
                                                    </table>
                                                    <div id="jqGrid_pager_A">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                                <!-- Datos Maquinaria -->
                                <div id="dvDatosMaquinaria" style="border: 0;">
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Datos del Bien</legend>
                                        <table id="tb_DatosMaquinaria">
                                            <tr>
                                                <td>
                                                    <table id="tb_formulario" cellpadding="0" cellspacing="3" style="border: 0;">
                                                        <colgroup>
                                                            <col style="width: 10.00%;" />
                                                            <col style="width: 23.32%;" />
                                                            <col style="width: 10.00%;" />
                                                            <col style="width: 23.32%;" />
                                                            <col style="width: 10.00%;" />
                                                            <col style="width: 23.32%;" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="label">
                                                                Nº Serie
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtSerieMotorMaquina" name="txtSerieMotorMaquina" type="text" class="css_input"
                                                                    size="25" />
                                                            </td>
                                                            <td class="label" id="lblNumeroMotorMaquina">
                                                                Nº Motor
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtNumeroMotorMaquina" name="txtNumeroMotorMaquina" type="text" class="css_input"
                                                                    size="25" />
                                                            </td>
                                                            <td class="label">
                                                                Año
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtFabricacionMaquina" name="txtFabricacionMaquina" type="text" class="css_input"
                                                                    size="12" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Marca
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtMarcaMaquina" name="txtMarcaMaquina" type="text" class="css_input"
                                                                    size="25" />
                                                            </td>
                                                            <td class="label">
                                                                Modelo
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtModeloMaquina" name="txtModeloMaquina" type="text" class="css_input"
                                                                    size="25" />
                                                            </td>
                                                            <td class="label">
                                                                Tipo Carrocería
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtTipoCarroceriaMaquina" name="txtTipoCarroceriaMaquina" type="text"
                                                                    class="css_input" size="25" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Descripción
                                                            </td>
                                                            <td class="input">
                                                                <textarea id="txtDescripcionAutoMaquina" runat="server" name="txtDescripcionAutoMaquina"
                                                                    class="css_input" rows="2" cols="51"></textarea>
                                                            </td>
                                                            <td class="label">
                                                                Estado del Bien
                                                            </td>
                                                            <td class="input">
                                                                <select id="cmbEstadobienMaquina" name="cmbEstadobienMaquina" runat="server">
                                                                </select>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Cantidad
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtCantidadMaquina" name="txtCantidadMaquina" type="text" class="css_input"
                                                                    size="12" style="text-align: right;" />
                                                            </td>
                                                            <td class="label">
                                                                Medidas
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtMedidasMaquina" name="txtMedidasMaquina" type="text" class="css_input"
                                                                    size="50" />
                                                            </td>
                                                            <td class="label">
                                                                Placa
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtPlacaMaquina" name="txtPlacaMaquina" type="text" class="css_input"
                                                                    size="12" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Uso
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtUsoBienMaquina" name="txtUsoBienMaquina" type="text" class="css_input"
                                                                    size="50" runat="server" />
                                                            </td>
                                                            <td class="label">
                                                                Ubicación
                                                            </td>
                                                            <td class="input" colspan="3">
                                                                <input id="txtUbicacionBienMaquina" name="txtUbicacionBienMaquina" type="text" class="css_input"
                                                                    size="50" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Departamento
                                                            </td>
                                                            <td class="input">
                                                                <select id="cmbDepartamentoMaquinaria" name="cmbDepartamentoMaquinaria" runat="server"
                                                                    onchange="javascript:fn_cargaComboProvincia('#cmbProvinciaMaquinaria','#cmbDistritoMaquinaria',this.value);">
                                                                </select>
                                                            </td>
                                                            <td class="label">
                                                                Provincia
                                                            </td>
                                                            <td class="input">
                                                                <select id="cmbProvinciaMaquinaria" name="cmbProvinciaMaquinaria" onchange="javascript:fn_cargaComboDistrito('#cmbDistritoMaquinaria',cmbDepartamentoMaquinaria.value, this.value);">
                                                                    <option value="0">[- Seleccionar -]</option>
                                                                </select>
                                                            </td>
                                                            <td class="label">
                                                                Distrito
                                                            </td>
                                                            <td class="input">
                                                                <select id="cmbDistritoMaquinaria" name="cmbDistritoMaquinaria">
                                                                    <option value="0">[- Seleccionar -]</option>
                                                                </select>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                    <div id="dv_AccionesMaquina" style="border: 0;">
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_GuardarMaquinaNuevo();">
                                                                <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />Agregar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_eliminarMaquina();">
                                                                <img alt="" src="../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />Eliminar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_EditarMaquina();">
                                                                <img alt="" src="../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />&nbsp;&nbsp;&nbsp;Editar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="border: 0; height: 22px; float: right;">
                                                            <a href="javascript:fn_ReplicarMaquina();">
                                                                <img alt="" src="../Util/images/ico_acc_replicar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />&nbsp;Replicar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_CancelarMaquina();">
                                                                <img alt="" src="../Util/images/ico_acc_limpiar.gif" style="width: 16px; height: 16px;
                                                                    border: 0px;" />&nbsp;&nbsp;&nbsp;Limpiar </a>
                                                        </div>
                                                    </div>
                                                    <div id="dv_ProcesoMaquina" style="border: 0;">
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_GuardarMaquina();">
                                                                <img alt="" src="../Util/images/ico_acc_grabar.gif" style="width: 16px; height: 16px;
                                                                    border: 0px;" />Guardar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_CancelarMaquina();">
                                                                <img alt="" src="../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;
                                                                    border: 0px;" />&nbsp;&nbsp;&nbsp;Cancelar </a>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="jqGrid_lista_B">
                                                        <tr>
                                                            <td />
                                                        </tr>
                                                    </table>
                                                    <div id="jqGrid_pager_B">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                                <!-- Datos Otros -->
                                <div id="dvDatosOtros" style="border: 0;">
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Datos del Bien</legend>
                                        <table id="tb_DatosOtros">
                                            <tr>
                                                <td>
                                                    <table id="tb_formulario" cellpadding="0" cellspacing="3" style="border: 0;">
                                                        <colgroup>
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                            <col style="width: 16.66%;" />
                                                        </colgroup>
                                                        <tr>
                                                            <td class="label">
                                                                Uso
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtUsoDatosOtros" name="txtUsoDatosOtros" type="text" class="css_input"
                                                                    size="50" runat="server" />
                                                            </td>
                                                            <td class="label">
                                                                Ubicación
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtUbicacionDatosOtros" name="txtUbicacionDatosOtros" type="text" class="css_input"
                                                                    size="50" runat="server" />
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Descripción
                                                            </td>
                                                            <td class="input">
                                                                <textarea id="txtDescripcionDatosOtros" name="txtDescripcionDatosOtros" rows="2"
                                                                    class="css_input" cols="51"></textarea>
                                                            </td>
                                                            <td class="label">
                                                                Marca
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtMarcaDatosOtros" name="txtMarcaDatosOtros" type="text" class="css_input"
                                                                    size="50" />
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Cantidad
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtCantidadDatosOtros" name="txtCantidadDatosOtros" type="text" class="css_input"
                                                                    size="12" style="text-align: right;" />
                                                            </td>
                                                            <td class="label">
                                                                Modelo
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtModeloDatosOtros" name="txtModeloDatosOtros" type="text" class="css_input"
                                                                    size="50" />
                                                            </td>
                                                            <td class="label">
                                                                Estado del Bien
                                                            </td>
                                                            <td class="input">
                                                                <select id="cmbEstadoDatosOtros" name="cmbEstadoDatosOtros" runat="server">
                                                                </select>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Departamento
                                                            </td>
                                                            <td class="input">
                                                                <select id="cmbDepartamentoDatosOtros" name="cmbDepartamentoDatosOtros" runat="server"
                                                                    onchange="javascript:fn_cargaComboProvincia('#cmbProvinciaDatosOtros','#cmbDistritoDatosOtros',this.value);">
                                                                </select>
                                                            </td>
                                                            <td class="label">
                                                                Provincia
                                                            </td>
                                                            <td class="input">
                                                                <select id="cmbProvinciaDatosOtros" name="cmbProvinciaDatosOtros" onchange="javascript:fn_cargaComboDistrito('#cmbDistritoDatosOtros',cmbDepartamentoDatosOtros.value, this.value);">
                                                                    <option value="0">[- Seleccionar -]</option>
                                                                </select>
                                                            </td>
                                                            <td class="label">
                                                                Distrito
                                                            </td>
                                                            <td class="input">
                                                                <select id="cmbDistritoDatosOtros" name="cmbDistritoDatosOtros">
                                                                    <option value="0">[- Seleccionar -]</option>
                                                                </select>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label">
                                                                Partida Registral
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtPartidaRegistralDatosOtros" name="txtPartidaRegistralDatosOtros" type="text"
                                                                    class="css_input" size="50" />
                                                            </td>
                                                            <td class="label">
                                                                Oficina Registral
                                                            </td>
                                                            <td class="input">
                                                                <input id="txtOficinaRegistralDatosOtros" name="txtOficinaRegistralDatosOtros" type="text"
                                                                    class="css_input" size="50" />
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td style="text-align: left;">
                                                    <div id="dv_AccionesDatosOtros" style="border: 0;">
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_GuardarDatosOtrosNuevo();">
                                                                <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />Agregar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_eliminarDatosOtros();">
                                                                <img alt="" src="../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />Eliminar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_EditarDatosOtros();">
                                                                <img alt="" src="../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />&nbsp;&nbsp;Editar&nbsp; </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="border: 0; height: 22px; float: right;">
                                                            <a href="javascript:fn_ReplicarDatosOtros();">
                                                                <img alt="" src="../Util/images/ico_acc_replicar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />&nbsp;Replicar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_CancelarDatosOtros();">
                                                                <img alt="" src="../Util/images/ico_acc_limpiar.gif" style="width: 16px; height: 16px;
                                                                    border: 0px;" />&nbsp;&nbsp;&nbsp;Limpiar </a>
                                                        </div>
                                                    </div>
                                                    <div id="dv_ProcesoDatosOtros" style="border: 0;">
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_GuardarDatosOtros();">
                                                                <img alt="" src="../Util/images/ico_acc_grabar.gif" style="width: 16px; height: 16px;
                                                                    border: 0px;" />Guardar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_CancelarDatosOtros();">
                                                                <img alt="" src="../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;
                                                                    border: 0px;" />&nbsp;&nbsp;&nbsp;Cancelar </a>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="jqGrid_lista_J">
                                                        <tr>
                                                            <td />
                                                        </tr>
                                                    </table>
                                                    <div id="jqGrid_pager_J">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                                <!-- Proveedores -->
                                <fieldset>
                                    <legend class="css_lbl_subTitulo">Proveedores</legend>
                                    <table id="jqGrid_lista_F">
                                        <tr>
                                            <td />
                                        </tr>
                                    </table>
                                    <div id="jqGrid_pager_F">
                                    </div>
                                </fieldset>
                            </div>
                            <!--
        			         TAB :: CONDICIONES ADICIONALES
        			         !-->
                            <div id="tab-1">
                                <div class="dv_tabla_contenedora" id="dv_info_Req_condiciones_ad">
                                    <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                        <tr>
                                            <td>
                                                <strong>CONDICIONES ADICIONALES</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="border: 0;">
                                                    <tr>
                                                        <td>
                                                            <table id="jqGrid_lista_E">
                                                                <tr>
                                                                    <td />
                                                                </tr>
                                                            </table>
                                                            <div id="jqGrid_pager_E">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="dv_tabla_contenedora">
                                    <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                        <tr>
                                            <td>
                                                <strong>DOCUMENTOS</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="border: 0;">
                                                    <tr>
                                                        <td>
                                                            <table id="jqGrid_lista_K">
                                                                <tr>
                                                                    <td />
                                                                </tr>
                                                            </table>
                                                            <div id="jqGrid_pager_K">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <!--
        			         TAB :: REPRESENTANTES A FIRMAR
        			         !-->
                            <div id="tab-2">
                                <div class="dv_tabla_contenedora">
                                    <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                        <tr>
                                            <td>
                                                <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                                    <tr>
                                                        <td class="titulo css_lbl_tituloContenido">
                                                            Representantes Interbank
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="border: 0;">
                                                    <tr>
                                                        <td>
                                                            <table id="jqGrid_lista_C">
                                                                <tr>
                                                                    <td />
                                                                </tr>
                                                            </table>
                                                            <div id="jqGrid_pager_C">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <!--
        			         TAB :: OTROS CONCEPTOS
        			         !-->
                            <div id="tab-3">
                                <div class="dv_tabla_contenedora">
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Tasas y Comisiones</legend>
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                            <colgroup>
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                            </colgroup>
                                            <tr>
                                                <td class="label">
                                                    T.E.A. %
                                                </td>
                                                <td class="input">
                                                    <input id="txtTea" name="txtTea" type="text" class="css_input_inactivo" size="10"
                                                        runat="server" style="text-align: right;" readonly="readonly" />
                                                </td>
                                                <td class="label">
                                                    Precuota %
                                                </td>
                                                <td class="input">
                                                    <input id="txtprecuota" name="txtprecuota" type="text" class="css_input_inactivo"
                                                        size="10" runat="server" style="text-align: right;" readonly="readonly" />
                                                </td>
                                                <td class="label">
                                                    Opción de Compra
                                                </td>
                                                <td class="input">
                                                <%--
                                                    <input id="txtOpcionCompra_old" name="txtOpcionCompra_old" type="text" class="css_input_inactivo"
                                                        style="width: 160px; text-align: right;" runat="server" readonly="readonly" />
                                                --%>
                                                    <input id="txtOpcionCompra" name="txtOpcionCompra" type="text" class="css_input"
                                                        style="width: 160px; text-align: right;" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Comisión de Activación
                                                </td>
                                                <td class="input">
                                                <%--
                                                    <input id="txtComisionActivacion_old" name="txtComisionActivacion_old" type="text" class="css_input_inactivo"
                                                        size="10" runat="server" style="text-align: right;" readonly="readonly" />
                                                --%>
                                                    <input id="txtComisionActivacion" name="txtComisionActivacion" type="text" class="css_input"
                                                        size="10" runat="server" style="text-align: right;" />
                                                </td>
                                                <td class="label">
                                                    Comisión de Estructuración
                                                </td>
                                                <td class="input">
                                                    <%--
                                                    <input id="txtComisionEstructuracion_old" name="txtComisionEstructuracion_old" type="text"
                                                        class="css_input_inactivo" size="10" runat="server" style="text-align: right;"
                                                        readonly="readonly" />
                                                    --%>
                                                    <input id="txtComisionEstructuracion" name="txtComisionEstructuracion" type="text"
                                                        class="css_input" size="10" runat="server" style="text-align: right;" />
                                                </td>
                                                <td class="label">
                                                    Otras Comisiones
                                                </td>
                                                <td class="input">
                                                    <textarea id="txtaOtrasComisiones" name="txtaOtrasComisiones" cols="20" rows="2"
                                                        style="width: 350px;" runat="server" class="css_input_inactivo" readonly="readonly">
                                                    
                                                        </textarea>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset>
                                        <legend class="css_lbl_subTitulo">Penalidades</legend>
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                            <colgroup>
                                                <col style="width: 25%;" />
                                                <col style="width: 25%;" />
                                                <col style="width: 25%;" />
                                                <col style="width: 25%;" />
                                            </colgroup>
                                            <tr>
                                                <td class="label">
                                                    % Del Importe Pendiente de Pago, por Día de Atraso
                                                </td>
                                                <td class="input">
                                                    <input id="txtImporteAtrasoPorc" name="txtImporteAtrasoPorc" type="text" class="css_input"
                                                        size="10" runat="server" style="text-align: right;" />
                                                </td>
                                                <td class="label">
                                                    Otros
                                                </td>
                                                <td class="input">
                                                    <textarea id="txtaOtrasPenalidades" name="txtaOtrasPenalidades" cols="20" rows="2"
                                                        style="width: 350px;" runat="server"></textarea>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Días desde el Vencimiento a la Fecha de Pago
                                                </td>
                                                <td class="input">
                                                    <input id="txtdiasVencimiento" name="txtdiasVencimiento" type="text" class="css_input"
                                                        size="10" runat="server" style="text-align: right;" />&nbsp;&nbsp;días
                                                </td>
                                                <td class="label">
                                                    Adjuntar Correo
                                                </td>
                                                <td class="input">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <img src="../Util/images/ico_acc_adjuntarMini.gif" id="img_ArchivoOtroConcepto" alt=""
                                                                    title="Adjuntar correo" onclick="javascript:fn_AdjuntarArchivoDocumento('CorreoAdjunto');"
                                                                    style="cursor: pointer; cursor: hand;" />
                                                            </td>
                                                            <td>
                                                                <div id="dv_ArchivoOtroConcepto" style="border: 0;">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    % De la cuota
                                                </td>
                                                <td class="input" colspan="3">
                                                    <input id="txtPorcentajeCuota" name="txtPorcentajeCuota" runat="server" type="text"
                                                        class="css_input" size="10" style="text-align: right;" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>
                            <!--
        			         TAB :: DATOS NOTARIALES
        			         !-->
                            <div id="tab-4">
                                <div id="dv_datosNotariales" style="border: 0px;">
                                    <fieldset>
                                        <input id="hdnCodigoNotarial" type="hidden" />
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                            <colgroup>
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                                <col style="width: 16.66%;" />
                                            </colgroup>
                                            <tr>
                                                <td class="label">
                                                    Departamento
                                                </td>
                                                <td class="input">
                                                    <select id="cmbDepartamento" name="cmbDepartamento" onchange="javascript:fn_cargaComboProvincia('#cmbProvincia2','#cmbDistrito2',this.value);fn_cargaComboNotaria(this.value, '0','#cmbNotariaDatoNotarial');">
                                                    </select>
                                                </td>
                                                <td class="label">
                                                    Provincia
                                                </td>
                                                <td class="input">
                                                    <select id="cmbProvincia2" name="cmbProvincia2" onchange="javascript:fn_cargaComboDistrito('#cmbDistrito2',cmbDepartamento.value,this.value);fn_cargaComboNotaria(cmbDepartamento.value,this.value, '#cmbNotariaDatoNotarial');">
                                                        <option value="0">[- Seleccionar -]</option>
                                                    </select>
                                                </td>
                                                <td class="label" id="lblcmbdistrito">
                                                    Distrito
                                                </td>
                                                <td class="input">
                                                    <select id="cmbDistrito2" name="cmbDistrito2">
                                                        <option value="0">[- Seleccionar -]</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Notaria
                                                </td>
                                                <td class="input">
                                                    <select id="cmbNotariaDatoNotarial" name="cmbNotariaDatoNotarial" onchange="javascript:fn_ObtenerContactoNotarias(this.value);">
                                                    </select>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <!--INICIO IBK - RPH -->
							                <tr>
							                   <td class="label">Contacto</td>
						                       <td class="input">
					                            <input id="txtContactoNotario" name="txtContactoNotario" type="text" class="css_input_inactivo" readonly="readonly" style="width:150px;" runat="server" />
						                       </td>
						                       <td class="label">Correo Contacto</td>
						                       <td class="input" colspan="3"><input id="txtCorreoNotaria" name="txtCorreoNotaria" type="text" class="css_input_inactivo" readonly="readonly" style="width:160px;" runat="server" /></td>  
							                </tr>
							                <!--FIN-->
                                            <tr>
                                                <td class="label">
                                                    Contrato de
                                                </td>
                                                <td class="input">
                                                    <select id="cmbTipoContrato" name="cmbTipoContrato">
                                                    </select>
                                                </td>
                                                <td class="label">
                                                    Nº Kardex
                                                </td>
                                                <td class="input">
                                                    <input id="txtKardex" name="txtKardex" type="text" class="css_input" size="12" />
                                                </td>
                                                <td class="label">
                                                    Fecha de Envío
                                                </td>
                                                <td class="input">
                                                    <input id="txtFechaContratoNotarial" name="txtFechaContratoNotarial" type="text"
                                                        class="css_input" size="12" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Observaciones
                                                </td>
                                                <td class="input" colspan="5">
                                                    <textarea id="txtObservacionesNotariales" name="txtObservacionesNotariales" class="css_input"
                                                        rows="3" cols="70" style="width: 400px;"></textarea>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="border: 0;">
                                            <tr>
                                                <td>
                                                    <div id="dv_AccionesDatosNotariales" style="border: 0;">
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_AgregarDatosNotarialesNuevo();">
                                                                <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;
                                                                    display: inline; border: 0;" />Agregar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_EliminarDatosNotariales();">
                                                                <img alt="" src="../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />Eliminar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_EditarDatosNotariales();">
                                                                <img alt="" src="../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />&nbsp;&nbsp;&nbsp;Editar </a>
                                                        </div>
                                                    </div>
                                                    <div id="dv_ProcesoDatosNotariales" style="border: 0;">
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_GuardarDatosNotariales();">
                                                                <img alt="" src="../Util/images/ico_acc_grabar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />Guardar </a>
                                                        </div>
                                                        <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                            <a href="javascript:fn_CancelarDatosNotariales();">
                                                                <img alt="" src="../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;
                                                                    border: 0;" />&nbsp;&nbsp;&nbsp;Cancelar </a>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="jqGrid_lista_G">
                                                        <tr>
                                                            <td />
                                                        </tr>
                                                    </table>
                                                    <div id="jqGrid_pager_G">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>
                            <!--
        			         TAB :: ADENDAS
        			         !-->
                            <div id="tab-5">
                                <div id="dv_adendas" style="border: 0;">
                                    <fieldset>
                                        <table id="tb_formulario" style="border: 0;" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td class="label">
                                                    Fecha de Envio
                                                </td>
                                                <td class="input">
                                                    <input id="txtFechaAdenda" name="txtFechaAdenda" type="text" class="css_input" size="11" />
                                                </td>
                                                <td class="label">
                                                    Fecha de Escritura Pública
                                                </td>
                                                <td class="input">
                                                    <input id="txtFechaEscrituraPub" name="txtFechaEscrituraPub" type="text" class="css_input"
                                                        size="11" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Adjuntar Adenda
                                                </td>
                                                <td class="input" colspan="5">
                                                    <table style="border: 0;">
                                                        <tr>
                                                            <td>
                                                                <img src="../Util/images/ico_acc_adjuntarMini.gif" id="imgArchivoAdenda" alt="" title="Adjuntar documento de adenda"
                                                                    onclick="javascript:fn_AdjuntarAdenda();" style="cursor: pointer; cursor: hand;" />
                                                            </td>
                                                            <td>
                                                                <div id="dv_DescargarArchivoAdenda" style="border: 0;">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Motivo
                                                </td>
                                                <td class="input" colspan="5">
                                                    <textarea id="txtaMotivo" name="txtaMotivo" cols="90" class="css_input" rows="4"></textarea>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Por Cuenta de
                                                </td>
                                                <td class="input" colspan="5">
                                                    <select id="cmbporCuentade" name="cmbporCuentade">
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Departamento
                                                </td>
                                                <td class="input">
                                                    <select id="cmbDepartamentoAdenda" name="cmbDepartamentoAdenda" onchange="javascript:fn_cargaComboProvincia('#cmbProvienciaAdenda','#cmbDistritoAdenda',this.value);fn_cargaComboNotaria(this.value, '#cmbNotariaAdenda');"
                                                        runat="server">
                                                    </select>
                                                </td>
                                                <td class="label">
                                                    Provincia
                                                </td>
                                                <td class="input">
                                                    <select id="cmbProvienciaAdenda" name="cmbProvienciaAdenda" onchange="javascript:fn_cargaComboDistrito('#cmbDistritoAdenda',cmbDepartamentoAdenda.value, this.value);">
                                                        <option value="0">[- Seleccionar -]</option>
                                                    </select>
                                                </td>
                                                <td class="label">
                                                    Distrito
                                                </td>
                                                <td class="input">
                                                    <select id="cmbDistritoAdenda" name="cmbDistritoAdenda">
                                                        <option value="0">[- Seleccionar -]</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Notaria
                                                </td>
                                                <td class="input">
                                                    <select id="cmbNotariaAdenda" name="cmbNotariaAdenda">
                                                    </select>
                                                </td>
                                                <td class="label">
                                                    Nº Kardex
                                                </td>
                                                <td class="input" colspan="3">
                                                    <input id="txtKardexAdenda" class="css_input_inactivo" name="txtKardexAdenda" type="text"
                                                        size="11" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <table style="border: 0;">
                                                        <tr>
                                                            <td>
                                                                <div id="dv_AccionesAdenda" style="border: 0;">
                                                                    <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                                        <a href="javascript:fn_GuardarAdendaNuevo();" style="display: inline;">
                                                                            <img alt="" src="../Util/images/ico_acc_agregar.gif" style="width: 16px; height: 16px;
                                                                                display: inline; border: 0;" />Agregar </a>
                                                                    </div>
                                                                    <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                                        <a href="javascript:fn_EliminarAdenda();">
                                                                            <img alt="" src="../Util/images/ico_acc_eliminar.gif" style="width: 16px; height: 16px;
                                                                                border: 0;" />Eliminar </a>
                                                                    </div>
                                                                    <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                                        <a href="javascript:fn_EditarAdenda();">
                                                                            <img alt="" src="../Util/images/ico_acc_editar.gif" style="width: 16px; height: 16px;
                                                                                border: 0;" />&nbsp;&nbsp;&nbsp;Editar </a>
                                                                    </div>
                                                                </div>
                                                                <div id="dv_ProcesoAdenda" style="border: 0;">
                                                                    <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                                        <a href="javascript:fn_GuardarAdenda();">
                                                                            <img alt="" src="../Util/images/ico_acc_grabar.gif" style="width: 16px; height: 16px;
                                                                                border: 0;" />Guardar </a>
                                                                    </div>
                                                                    <div class="dv_img_boton_mini" style="height: 22px; float: right; border: 0;">
                                                                        <a href="javascript:fn_CancelarAdenda();">
                                                                            <img alt="" src="../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;
                                                                                border: 0;" />&nbsp;&nbsp;&nbsp;Cancelar </a>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table id="jqGrid_lista_I">
                                                                    <tr>
                                                                        <td />
                                                                    </tr>
                                                                </table>
                                                                <div id="jqGrid_pager_I">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <input type="hidden" id="hddBloqueoExistente" name="hddBloqueoExistente" />
        <input type="hidden" id="hddBloqueoCodigo" name="hddBloqueoCodigo" />
        <input type="hidden" id="hddBloqueoCodUsuario" name="hddBloqueoCodUsuario" />
        <input type="hidden" id="hddBloqueoNomUsuario" name="hddBloqueoNomUsuario" />
        <input type="hidden" id="hddBloqueoFecha" name="hddBloqueoFecha" />
        <input type="hidden" id="hddUbigeoUbicacion" name="hddUbigeoUbicacion" runat="server" />
        <input type="button" name="cmdGuardarRepresentante" id="cmdGuardarRepresentante"
            onclick="javascript:fn_ListaRepresentantesClienteFromModal();" style="display: none;" />
        <input id="hddCodigoCotizacion" type="hidden" runat="server" />
        <input id="hddCorreocontacto" type="hidden" runat="server" />
        <input id="hdnCodigoAdenda" type="hidden" />
        <input id="hddCodProductoFinancieroActivo" type="hidden" runat="server" />
        <input id="hddCodMoneda" type="hidden" runat="server" />
        <input id="hddTipoDocumentoConyuge" type="hidden" runat="server" />
        <input id="hddCodigoTipoPersona" type="hidden" runat="server" />
        <input id="hddTipoDocumento" type="hidden" runat="server" />
        <input id="hddCodigoEstadoCivil" type="hidden" runat="server" />
        <input id="hddEstadoCivil" type="hidden" runat="server" />
        <input id="hddCodigoContrato" type="hidden" runat="server" />
        <input id="hddTipoRubroFinanciamiento" type="hidden" runat="server" />
        <input id="hddCodigoClasificacionContrato" type="hidden" runat="server" />
        <input id="hddCodigoTipoBien" type="hidden" runat="server" />
        <input id="hddSecFinanciamiento" type="hidden" />
        <input id="hddRowId" type="hidden" />
        <input id="hddCodProveedor" type="hidden" />
        <input id="hddCodigoEstadoBien" type="hidden" runat="server" />
        <input id="hddCodigoEstadoContrato" type="hidden" runat="server" />
        <input id="hddFechaFirmaNotaria" type="hidden" runat="server" />
        <input id="hddClasifContratoSeleccion" type="hidden" runat="server" />
        <!-- Datos del bien -->
        <!-- Detecta cambios en el contenido de los controles, antes de salir de la actual ventana. -->
        <input id="hddCambiosSinGuardar" type="hidden" />
        <!-- Indica si alguno de los datos del contrato o de otros documentos han cambiado y requieren volver a generar el contrato.
             El cambio se registra a través de la base de datos -->
        <input id="hddFlagModificado" type="hidden" runat="server" />
        <input id="hddMensajeCorreo" type="hidden" runat="server" />
        <input id="hddFechaActual" type="hidden" runat="server" />
        <!-- Datos del bien -->
        <input id="hddUso" type="hidden" runat="server" />
        <input id="hddUbicacion" type="hidden" runat="server" />
        <!-- Contrato (Anexos) -->
        <input id="btnAdjuntarArchivo" type="button" runat="server" onclick="javascript:fn_ActualizarAnexo();"
            style="visibility: hidden; display: none;" />
        <!-- Contrato Valida Modificaciones -->
        <input id="hddAdjuntarArchivo" type="hidden" runat="server" />
        <input id="hddValidaModificacion" type="hidden" />
        <input id="hddGeneraContrato_Adjunto" type="hidden" />
        <!-- Datos del conyugue -->
        <input id="btnAdjuntarArchivoDocumentoSeparacion" type="button" runat="server" onclick="javascript:fn_ActualizarDocumentoSeparacion();"
            style="visibility: hidden; display: none;" />
        <input id="hddAdjuntarArchivoDocumentoSeparacion" type="hidden" runat="server" />
        <!-- Otros conceptos -->
        <input id="btnAdjuntarArchivoOtroConcepto" type="button" runat="server" onclick="javascript:fn_ActualizarArchivoAdjunto();"
            style="visibility: hidden; display: none;" />
        <input id="hddAdjuntarArchivoOtroConcepto" type="hidden" runat="server" />
        <!-- Adenda - nuevo -->
        <input id="btnAdjuntarArchivoNotarialNuevo" type="button" runat="server" onclick="javascript:fn_ActualizarArchivoNotarialNuevo();"
            style="visibility: hidden; display: none;" />
        <input id="hddAdjuntarArchivoNotarialNuevo" type="hidden" runat="server" />
        <!-- Adenda - editar -->
        <input id="btnAdjuntarArchivoNotarialEditar" type="button" runat="server" onclick="javascript:fn_ActualizarArchivoNotarialEditar();"
            style="visibility: hidden; display: none;" />
        <input id="hddAdjuntarArchivoNotarialEditar" type="hidden" runat="server" />
        <!-- Texto predefinido -->
        <input id="btnTextoPredefinido" type="button" runat="server" onclick="javascript:fn_ActualizarTextoPredefinido();"
            style="visibility: hidden; display: none;" />
        <input id="hddTextoPredefinido" type="hidden" runat="server" />
        <!-- Retorno -->
        <input type="hidden" id="hddFlagRetorno" name="hddFlagRetorno" value="" runat="server" />
    </div>
    </form>
</body>
</html>
