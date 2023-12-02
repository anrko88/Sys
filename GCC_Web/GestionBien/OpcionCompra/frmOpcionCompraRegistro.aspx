<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmOpcionCompraRegistro.aspx.vb"
    Inherits="GestionBien_OpcionCompra_frmOpcionCompraRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cobros Registro </title>
    <!-- Icono URL -->
    <link rel="SHORTCUT ICON" href="../../Util/images/PV16x16.ico" />
    <!-- Estilos -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery-ui-1.8.15.custom.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jquery/jquery.jscrollpane.css"
        media="all" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_global.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_formulario.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_fuente.css" />
    <!-- JavaScript -->

    <script type='text/javascript' src="../../Util/js/jquery/jquery-1.6.2.min.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.min.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.mousewheel.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.ui.global.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.validText.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.validNumber.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.maxlength.js"> </script>

    <script type="text/javascript" src="../../Util/js/js_global.js"></script>

    <script type='text/javascript' src="../../Util/js/js_util.modal.js"> </script>

    <script type='text/javascript' src="../../Util/js/js_util.funcion.js"> </script>

    <script type='text/javascript' src="../../Util/js/js_util.date.js"> </script>

    <script type='text/javascript' src="../../Util/js/js_util.ajax.js"> </script>

    <script src="../../Util/js/jquery/jquery.dateFormat-1.0.js" type="text/javascript"></script>

    <script src="../../Util/js/jquery/jshashtable.js" type="text/javascript"></script>

    <script src="../../Util/js/js_util.Grilla.js" type="text/javascript"></script>

    <!-- JQGrid -->

    <script src="../../Util/js/js_util.Grilla.js" type="text/javascript"></script>

    <link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmOpcionCompraRegistro.aspx.js"> </script>

</head>
<body>
    <form id="frmObservacion" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="icono">
                    <img src="../../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo" id="divTitulo" runat="server">
                        Gestión del Bien
                    </div>
                    <div class="css_lbl_titulo" id="divSubTitulo" runat="server">
                        Opción de Compra:: Registro</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_Cancelar();">
                            <img src="../../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Volver </a>
                    </div>
                    <div id="Div3" class="dv_img_boton_separador">
                        :
                    </div>
<%--                    <div id="dv_guardar" class="dv_img_boton">
                        <div id="dv_img_boton" class="dv_img_boton">
                            <a href="javascript:fn_guardar();">
                                <img src="../../Util/images/ico_acc_grabar.gif" border="0" /><br />
                                Guardar </a>
                        </div>
                    </div>--%>
                    <div id="dv_Documentos" class="dv_img_boton" style="width: 80px;">
                        <a href="javascript:fn_GBAbreDocumentos();">
                            <img alt="" src="../../Util/images/ico_version.gif" border="0" /><br />
                            Documentos </a>
                    </div>
                </td>
            </tr>
        </table>
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
                padding-right: 0px;">
                <tr>
                    <td class="lineas">
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <table id="tb_Contrato" border="0" cellpadding="3" cellspacing="0" width="100%">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido" style="height: 25px" valign="bottom">
                                    Datos del Contrato
                                </td>
                            </tr>
                        </table>
                        <div class="dv_tabla_contenedora" style="padding-top: 0px;">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" width="100%">
                                <tr>
                                    <td class="label">
                                        N° Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtNroContrato" name="txtNroContrato" type="text" class="css_input_inactivo"
                                            size="12" runat="server" readonly="true" />
                                    </td>
                                    <td class="label">
                                        CU Cliente
                                    </td>
                                    <td class="input">
                                        <input id="txtCUCliente" name="txtCUCliente" runat="server" type="text" class="css_input_inactivo"
                                            size="12" readonly="true" />
                                    </td>
                                    <td class="label" style="width: 140px">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtRazonSocialProveedor" runat="server" name="txtRazonSocialProveedor"
                                            type="text" class="css_input_inactivo" style="width: 300px" readonly="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Clasificación del Bien
                                    </td>
                                    <td class="input" colspan="1">
                                        <input id="txtClasificacionBien" name="txtClasificacionBien" runat="server" type="text"
                                            class="css_input_inactivo" size="40" readonly="true" />
                                    </td>
                                    <td class="label">
                                        Estado
                                    </td>
                                    <td class="input">
                                        <input id="txtEstadoContrato" name="txtEstadoContrato" runat="server" type="text"
                                            class="css_input_inactivo" size="25" readonly="true" />
                                    </td>
                                    <td class="label">
                                        Fecha de Activación
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaActivacion" name="txtFechaActivacion" runat="server" type="text"
                                            class="css_input_inactivo" size="12" readonly="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Ejecutivo de Banca
                                    </td>
                                    <td class="input" colspan="1">
                                        <input id="txtEjecutivoBanca" name="txtEjecutivoBanca" runat="server" type="text"
                                            class="css_input_inactivo" size="40" readonly="true" />
                                    </td>
                                    <td class="label">
                                        Banca
                                    </td>
                                    <td class="input">
                                        <input id="txtBanca" name="txtBanca" runat="server" type="text" class="css_input_inactivo"
                                            size="25" readonly="true" />
                                    </td>
                                    <td class="label">
                                        Moneda
                                    </td>
                                    <td class="input">
                                        <input id="txtMoneda" name="txtEstadoContrato0" runat="server" type="text" class="css_input_inactivo"
                                            size="18" readonly="true" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table id="tb_Cobro" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido" style="height: 25px" valign="bottom">
                                    Opción de Compra
                                </td>
                            </tr>
                        </table>
                        <div class="dv_tabla_contenedora" style="padding-top: 0px;">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" width="100%">
                                <tr>
                                    <td class="label">
                                        Opción de Compra %
                                    </td>
                                    <td class="input">
                                        <input id="txtPorcentajeOC" name="txtPorcentajeOC" type="text" class="css_input_inactivo"
                                            size="15" runat="server" readonly="true" style="text-align: right" />
                                    </td>
                                    <td class="label">
                                        Importe OC
                                    </td>
                                    <td class="input">
                                        <input id="txtImporteOC" name="txtImporteOC" type="text" class="css_input_inactivo"
                                            size="15" runat="server" readonly="true" style="text-align: right" />
                                    </td>
                                    <td class="label">
                                        Comisión OC %
                                    </td>
                                    <td class="input">
                                        <input id="txtComisionOC" name="txtComisionOC" type="text" class="css_input_inactivo"
                                            size="15" runat="server" readonly="true" style="text-align: right" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label" style="width: 250px">
                                        Comisión de Gastos de Transferencia %
                                    </td>
                                    <td class="input" colspan="1">
                                        <input id="txtPorcentajeCGT" name="txtPorcentajeCGT" type="text" class="css_input_inactivo"
                                            size="15" runat="server" readonly="true" style="text-align: right" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <!-- ******************************************************************************************** -->
                        <!-- Inicia Tabs -->
                        <!-- ******************************************************************************************** -->
                        <div id="divTabs" style="border: 0px; background: none;" class="dv_tabla_contenedora">
                            <ul>
                                <li><a href="#tab-0">ENVIO DE CARTAS</a></li>
                                <li><a href="#tab-1">LISTA DE BIENES</a></li>
                                <li><a href="#tab-2">CHECKLIST DOCUMENTOS</a></li>
                            </ul>
                            <!-- **************** -->
                            <!-- TAB :: LISTA DE BIENES   -->
                            <!-- **************** -->
                            <div id="tab-0">
                                <table id="tb_Carta" border="0" cellpadding="3" cellspacing="0">
                                    <tr>
                                        <td class="titulo css_lbl_tituloContenido" style="height: 25px" valign="bottom">
                                            Envio de Carta
                                        </td>
                                    </tr>
                                </table>
                                <%--<div class="dv_tabla_contenedora" style="padding-top: 0px;">--%>
                                <table border="0" cellpadding="0" cellspacing="3" width="100%">
                                    <tr>
                                        <td colspan="6">
                                            <div class="dv_tabla_contenedora" style="width: 300px">
                                                <table id="jqGrid_lista_A">
                                                    <tr>
                                                        <td />
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <%--</div>--%>
                            </div>
                            <!-- **************** -->
                            <!-- TAB :: LISTA DE BIENES   -->
                            <!-- **************** -->
                            <div id="tab-1">
                                <table id="tb_Bienes" border="0" cellpadding="3" cellspacing="0">
                                    <tr>
                                        <td class="titulo css_lbl_tituloContenido" style="height: 25px" valign="bottom">
                                            Listado de Bienes
                                        </td>
                                    </tr>
                                </table>
                                <div class="dv_tabla_contenedora">
                                    <table id="jqGrid_lista_B">
                                        <tr>
                                            <td />
                                        </tr>
                                    </table>
                                    <div id="jqGrid_pager_B">
                                    </div>
                                </div>
                                <table id="tbDatos" border="0" cellpadding="3" cellspacing="0">
                                    <tr>
                                        <td class="titulo css_lbl_tituloContenido" style="height: 25px" valign="bottom">
                                            Datos de Bien
                                        </td>
                                    </tr>
                                </table>
                                <div class="dv_tabla_contenedora" id="divDatos">
                                    <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" width="100%">
                                        <tr>
                                            <td class="label" style="width: 140px">
                                                Tipo del Bien
                                            </td>
                                            <td class="input">
                                                <input id="txtTipoBien" name="txtTipoBien" type="text" class="css_input_inactivo"
                                                    size="20" runat="server" readonly="true" />
                                            </td>
                                            <td class="label">
                                                Descripción del Bien
                                            </td>
                                            <td class="input" colspan="3">
                                                <input id="txtDescripcion" name="txtDescripcion" runat="server" type="text" class="css_input_inactivo"
                                                    size="50" readonly="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Ubicación
                                            </td>
                                            <td class="input" colspan="5">
                                                <input id="txtUbicacion" name="txtUbicacion" runat="server" type="text" class="css_input_inactivo"
                                                    size="80" readonly="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 140px">
                                                Departamento
                                            </td>
                                            <td class="input">
                                                <input id="txtDepartamento" name="txtDepartamento" runat="server" type="text" class="css_input_inactivo"
                                                    size="20" readonly="true" />
                                            </td>
                                            <td class="label">
                                                Provincia
                                            </td>
                                            <td class="input">
                                                <input id="txtProvincia" name="txtProvincia" runat="server" type="text" class="css_input_inactivo"
                                                    size="20" readonly="true" />
                                            </td>
                                            <td class="label">
                                                Distrito
                                            </td>
                                            <td class="input">
                                                <input id="txtDistrito" name="txtDistrito" runat="server" type="text" class="css_input_inactivo"
                                                    size="20" readonly="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 140px">
                                                Placa Actual
                                            </td>
                                            <td class="input" colspan="1">
                                                <input id="txtPlacaActual" name="txtDeparatamento" runat="server" type="text" class="css_input_inactivo"
                                                    size="20" readonly="true" />
                                            </td>
                                            <td class="label">
                                                N° Motor
                                            </td>
                                            <td class="input">
                                                <input id="txtNroMotor" name="txtNroMotor" runat="server" type="text" class="css_input_inactivo"
                                                    size="20" readonly="true" />
                                            </td>
                                            <td class="label">
                                                Marca
                                            </td>
                                            <td class="input">
                                                <input id="txtMarca" name="txtMarca" runat="server" type="text" class="css_input_inactivo"
                                                    size="20" readonly="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 140px">
                                                Carta opción de compra
                                            </td>
                                            <td class="input">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <input type="checkbox" id="chkAceptacion" name="chkAceptacion" />
                                                        </td>
                                                        <td>
                                                            <input id="txtFechaAceptacion" name="txtFechaAceptacion" runat="server" type="text"
                                                                class="css_input_inactivo" size="10" readonly="true" maxlength="10" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="label">
                                                Fecha Transferencia
                                            </td>
                                            <td class="input">
                                                <input id="txtFechaTransferencia" name="txtFechaTransferencia" runat="server" type="text"
                                                    class="css_input" size="10" maxlength="10" />
                                            </td>
                                            <td class="label" style="width: 180px">
                                                Fecha Transferencia en RRPP
                                            </td>
                                            <td class="input">
                                                <input id="txtFechaTransferenciaRRPP" name="txtFechaTransferenciaRRPP" runat="server"
                                                    type="text" class="css_input" size="10" maxlength="10" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 140px">
                                                Pago
                                            </td>
                                            <td class="input">
                                                <select id="ddlPago" runat="server" name="ddlPago">
                                                    <%--                                                    <option value='0'>[-Seleccione-]</option>
                                                    <option value='1'>LEASING OPERATIVO</option>
                                                    <option value='2'>PENDIENTE CANCELAR CONCEPTOS</option>
                                                    <option value='3'>TODO CANCELADO</option>
                                                    <option value='4'>TODO PAGADO</option>
                                                    <option value='5'>VERIFICAR</option>--%>
                                                </select>
                                            </td>
                                            <td class="label">
                                                Fecha de Pago OC
                                            </td>
                                            <td class="input">
                                                <input id="txtFechaPagoOC" name="txtFechaPagoOC" maxlength="10" runat="server" type="text"
                                                    class="css_input" size="10" />
                                            </td>
                                            <td class="input">
                                            </td>
                                            <td class="input">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="text-align: right; padding-right: 10px">
                                                <div id="dv_cancelar" class="dv_img_boton_mini" style="width: 80px; border: 0px solid #ffffff;"
                                                    runat="server">
                                                    <a href="javascript:fn_LimpiarBien();">
                                                        <img alt="" src="../../Util/images/ico_acc_cancelar.gif" style="width: 16px; height: 16px;"
                                                            border="0" />
                                                        Cancelar </a>
                                                </div>
                                                <div id="dv_Modificar" colspan="5" class="dv_img_boton_mini" style="width: 80px;
                                                    border: 0px solid #ffffff;" runat="server">
                                                    <a href="javascript:fn_ModificarBien();">
                                                        <img alt="" src="../../Util/images/ico_acc_grabar.gif" style="width: 16px; height: 16px;"
                                                            border="0" />
                                                        Grabar </a>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <!-- **************** -->
                            <!-- TAB :: CHECKLIST DOCUMENTOS   -->
                            <!-- **************** -->
                            <div id="tab-2">
                                <table id="tbDocumento" border="0" cellpadding="3" cellspacing="0">
                                    <tr>
                                        <td class="titulo css_lbl_tituloContenido" style="height: 25px" valign="bottom">
                                            CheckList Documentos
                                        </td>
                                    </tr>
                                </table>
                                <div class="dv_tabla_contenedora">
                                    <table id="jqGrid_lista_C">
                                        <tr>
                                            <td />
                                        </tr>
                                    </table>
                                    <div id="jqGrid_pager_C">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="hddFechaActual" runat="server" Value="" />
    <input type="hidden" name="hidCodOpcionCompra" id="hidCodOpcionCompra" value="0"
        runat="server" />
    <input type="hidden" name="hidNumeroContrato" id="hidNumeroContrato" value="0" runat="server" />
    <input type="hidden" name="hidCodEstadoContrato" id="hidCodEstadoContrato" value="0" runat="server" />
    <input type="hidden" name="hidSecFinanciamiento" id="hidSecFinanciamiento" value="0"
        runat="server" />
    <input type="hidden" name="hidTotalBien" id="hidTotalBien" value="0" runat="server" />
    <input type="hidden" name="hidCantidadDemanda" id="hidCantidadDemanda" value="0" runat="server" />
    <input type="hidden" name="hidFlagAprobacion" id="hidFlagAprobacion" value="0" runat="server" />
    <!-- **************************************************************************************** -->
    <!-- DIV LOG -->
    <!-- **************************************************************************************** -->
    <div id="dv_LogAviso" class="cssLogPage_Aviso" style="display: none;">
        <strong>Info&nbsp;:</strong><br />
        <span id="dv_LogAviso_Msg"></span>
    </div>
    <div id="dv_LogError" class="cssLogPage_Error" style="display: none;">
        <strong>Advertencia &nbsp;:</strong><br />
        <span id="dv_LogError_Msg"></span>
    </div>
    </form>
</body>
</html>
