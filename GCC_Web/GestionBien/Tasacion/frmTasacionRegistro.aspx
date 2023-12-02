<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTasacionRegistro.aspx.vb"
    Inherits="GestionBien_Tasacion_frmTasacionRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SGL :: Sistema de Gestión de Leasing</title>
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

    <script type="text/javascript" src="../../Util/js/jquery/jquery.dateFormat-1.0.js"></script>

    <script type="text/javascript" src="../../Util/js/jquery/jshashtable.js"></script>

    <script type="text/javascript" src="../../Util/js/js_util.Grilla.js"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmTasacionRegistro.aspx.js"> </script>

</head>
<body>
    <form id="frmTasacionRegistro" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_tasacion.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Gestión del Bien</div>
                    <div class="css_lbl_titulo">
                        Tasación :: Registro</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_regresar();">
                            <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Volver </a>
                    </div>
                    <div id="Div1" class="dv_img_boton">
                        <a href="javascript:fn_enviarcorreo();">
                            <img alt="" src="../../Util/images/ico_acc_msgEnviar.gif" border="0" /><br />
                            Enviar </a>
                    </div>
                    <div id="Div2" class="dv_img_boton">
                        <img alt="" src="../../Util/images/ico_acc_msgEnviar_des.gif" border="0" /><br />
                        Enviar
                    </div>
                </td>
            </tr>
        </table>
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="lineas">
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Contrato
                                </td>
                                <td class="botones">
                                    <img src="../../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <div class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        Nº Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtcontrato" name="txtcontrato" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        CU Cliente
                                    </td>
                                    <td class="input">
                                        <input id="txtcucliente" name="txtcucliente" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtrazonsocial" name="txtrazonsocial" type="text" class="css_input" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Estado del Contrato
                                    </td>
                                    <td class="input">
                                        <select id="cbmEstadoContrato" name="cbmEstadoContrato" runat="server">
                                            <option value="0">[-Seleccionar-]</option>
                                        </select>
                                    </td>
                                    <td class="label">
                                        Clasificaciòn del Bien
                                    </td>
                                    <td class="input">
                                        <select id="cmbClasificacionBien" name="cmbClasificacionBien" runat="server">
                                            <option value="0">[-Seleccionar-]</option>
                                        </select>
                                    </td>
                                    <td class="label">
                                        Fecha Activación
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaActivacion" name="txtFechaActivacion" type="text" class="css_input"
                                            runat="server" />
                                    </td>
                                    <td class="label">
                                        Banca
                                    </td>
                                    <td class="input">
                                        <select id="cmbbanca" name="cmbbanca" runat="server">
                                            <option value="0">[-Seleccionar-]</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Ejecutivo de Banca
                                    </td>
                                    <td class="input">
                                        <input id="txtdesejecutivobanca" name="txtdesejecutivobanca" type="text" class="css_input"
                                            runat="server" />
                                    </td>
                                    <td class="label">
                                        Ejecutivo de Leasing
                                    </td>
                                    <td class="input">
                                        <select id="cmbEjecutivoLeasing" name="cmbEjecutivoLeasing" runat="server">
                                            <option value="0">[-Seleccionar-]</option>
                                        </select>
                                    </td>
                                    <td class="label">
                                        Moneda
                                    </td>
                                    <td class="input">
                                        <select id="cmbMoneda" name="cmbMoneda" runat="server">
                                            <option value="0">[-Seleccionar-]</option>
                                        </select>
                                    </td>
                                    <td class="label">
                                        T.C. del Dia
                                    </td>
                                    <td class="input">
                                        <input id="txttipocambio" name="txttipocambio" type="text" class="css_input" style="text-align: right"
                                            runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Capital Financiado S/.
                                    </td>
                                    <td class="input">
                                        <input id="txtcapitalfinanciadosoles" name="txtcapitalfinanciadosoles" type="text"
                                            style="text-align: right" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Capital Financiado $
                                    </td>
                                    <td class="input">
                                        <input id="txtcapitalfinanciadodolar" name="txtcapitalfinanciadodolar" type="text"
                                            style="text-align: right" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Saldo Capital S/.
                                    </td>
                                    <td class="input">
                                        <input id="txtsaldocapitalsoles" name="txtsaldocapitalsoles" type="text" style="text-align: right"
                                            class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Saldo Capital $.
                                    </td>
                                    <td class="input">
                                        <input id="txtsaldocapitaldolares" name="txtsaldocapitaldolares" style="text-align: right"
                                            type="text" class="css_input" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <!-- Inicio Grilla Listado -->
                        <div class="dv_tabla_contenedoraSola">
                            <table id="jqGrid_lista_A">
                                <tr>
                                    <td />
                                </tr>
                            </table>
                            <div id="jqGrid_pager_A">
                            </div>
                        </div>
                        <!-- Fin Grilla -->
                        <table id="tb_formulario" border="0" style="width: 60%">
                            <colgroup>
                                <col style="width: 30%" />
                                <col style="width: 20%" />
                                <col style="width: 30%" />
                                <col style="width: 20%" />
                                <col style="width: 30%" />
                                <col style="width: 20%" />
                            </colgroup>
                            <tr>
                                <td class="label">
                                    TOTAL VALOR COMERCIAL
                                </td>
                                <td class="input" style="text-align: right">
                                    <div id="divTotalComercial">
                                    </div>
                                </td>
                                <td class="label">
                                    TOTAL VALOR REALIZACION
                                </td>
                                <td class="input" style="text-align: right">
                                    <div id="divTotalRealizacion">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    DIFERENCIA COMERCIAL
                                </td>
                                <td class="input" style="text-align: right">
                                    <div id="divDiferenciaComercial">
                                    </div>
                                </td>
                                <td class="label">
                                    DIFERENCIA REALIZACION
                                </td>
                                <td class="input" style="text-align: right">
                                    <div id="divDiferenciaRealizacion">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <!-- **************************************************************************************** -->
        <!-- DIV LOG -->
        <!-- **************************************************************************************** -->
        <div id="dv_LogAviso" class="cssLogPage_Aviso" style="display: none;">
            <strong>Info &nbsp;:</strong><br />
            <span id="dv_LogAviso_Msg"></span>
        </div>
        <div id="dv_LogError" class="cssLogPage_Error" style="display: none;">
            <strong>Advertencia &nbsp;:</strong><br />
            <span id="dv_LogError_Msg"></span>
        </div>
    </div>
    <!-- Fin Cuerpo -->
    <input type="button" name="cmdListarDocumentos" id="cmdListarDocumentos" onclick="javascript:fn_ListadoContratoBienTasador();"
        style="display: none;" />
    <asp:HiddenField ID="hddCodigoContratos" runat="server" />
    <asp:HiddenField ID="hddFechaActual" runat="server" />
    <asp:HiddenField ID="hddtiopcambiosunat" runat="server" />
    <asp:HiddenField ID="hddtotalRealizacion" runat="server" />
    <asp:HiddenField ID="hddtotalcomercial" runat="server" />
    <asp:HiddenField ID="hddDiferenciaRealizacion" runat="server" />
    <asp:HiddenField ID="hddDiferenciaComercial" runat="server" />
    <asp:HiddenField ID="hddComboTasador" runat="server" Value="" />
    </form>
</body>
</html>
