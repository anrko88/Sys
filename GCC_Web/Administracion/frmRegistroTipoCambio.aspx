<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistroTipoCambio.aspx.vb"
    Inherits="Administracion_frmRegistroTipoCambio" %>

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

    <script type='text/javascript' src="../Util/js/jquery/jquery-1.6.2.min.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.jscrollpane.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.jscrollpane.min.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.mousewheel.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.ui.global.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.validText.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.validNumber.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.maxlength.js"> </script>

    <script type="text/javascript" src="../Util/js/js_global.js"></script>

    <script type='text/javascript' src="../Util/js/js_util.modal.js"> </script>

    <script type='text/javascript' src="../Util/js/js_util.funcion.js"> </script>

    <script type='text/javascript' src="../Util/js/js_util.date.js"> </script>

    <script type='text/javascript' src="../Util/js/js_util.ajax.js"> </script>

    <script type="text/javascript" src="../Util/js/jquery/jquery.dateFormat-1.0.js"></script>

    <script type="text/javascript" src="../Util/js/jquery/jshashtable.js"></script>

    <script type="text/javascript" src="../Util/js/js_util.Grilla.js"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../Util/css/css_grilla.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmRegistroTipoCambio.aspx.js"> </script>

</head>
<body>
    <form id="frmRegistroTipoCambio" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Mantenimiento</div>
                    <div class="css_lbl_titulo">
                        Tipo de Cambio : Nuevo</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton1" class="dv_img_boton">
                        <a href="javascript:fn_cancelar();">
                            <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0" title="Cancelar" /><br />
                            Volver </a>
                    </div>
                    <div id="dv_img_boton4" class="dv_img_boton">
                        <a href="javascript:fn_eliminar();">
                            <img alt="" src="../Util/images/ico_acc_eliminar.gif" border="0" title="Guardar" /><br />
                            Eliminar </a>
                    </div>
                    <div id="dv_img_boton2" class="dv_img_boton">
                        <a href="javascript:fn_grabar();">
                            <img alt="" src="../Util/images/ico_acc_grabar.gif" border="0" title="Guardar" /><br />
                            Guardar </a>
                    </div>
                    <div id="dv_img_boton3" class="dv_img_boton">
                        <a href="javascript:fn_actualizar();">
                            <img alt="" src="../Util/images/ico_acc_editar.gif" border="0" title="Guardar" /><br />
                            Editar </a>
                    </div>
                    <input type="hidden" name="hddPerfil" id="hddPerfil" value="" runat="server" />
                </td>
            </tr>
        </table>
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="lineas">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Tipo de Cambio
                                </td>
                                <td class="botones">
                                    <img alt="" src="../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <div id="dv_datos" class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        Modalidad
                                    </td>
                                    <td class="input">
                                        <select id="cmbTipoModalidad" name="cmbTipoModalidad" runat="server">
                                            <option value="">- Seleccionar -</option>
                                        </select>
                                        <input type="hidden" id="hidCodTipoCambio" name="hidCodTipoCambio" value="" runat="server" />
                                        <input type="hidden" id="hddValida" name="hddValida" runat="server" />
                                        <input type="hidden" id="hidOpcion" name="hidOpcion" value="" runat="server" />
                                    </td>
                                    <td class="label">
                                        Fecha Inicio Vigencia
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaIni" name="txtFechaIni" runat="server" />
                                    </td>
                                    <td class="label">
                                        Fecha Final Vigencia
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaFin" name="txtFechaFin" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Moneda
                                    </td>
                                    <td class="input">
                                        <input id="txtNombreMoneda" name="txtNombreMoneda" runat="server" />
                                    </td>
                                    <td class="label">
                                        Tipo de Cambio de Compra
                                    </td>
                                    <td class="input">
                                        <input id="txtValorCompra" name="txtValorCompra" runat="server" />
                                    </td>
                                    <td class="label">
                                        Tipo de Cambio de Venta
                                    </td>
                                    <td class="input">
                                        <input id="txtValorVenta" name="txtValorVenta" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <!-- Inicio Grilla Listado de Concepto -->
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
                                          
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
