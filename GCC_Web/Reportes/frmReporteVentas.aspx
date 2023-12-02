<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmReporteVentas.aspx.vb"
    Inherits="Reportes_frmReporteVentas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Consulta Reporte</title>
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

    <script type="text/javascript" src="../Util/js/jquery/jquery.dateFormat-1.0.js"></script>

    <script type="text/javascript" src="../Util/js/jquery/json2.js"></script>

    <script type="text/javascript" src="../Util/js/js_global.js"></script>

    <script type='text/javascript' src="../Util/js/js_util.modal.js"> </script>

    <script type='text/javascript' src="../Util/js/js_util.funcion.js"> </script>

    <script type='text/javascript' src="../Util/js/js_util.date.js"> </script>

    <script type='text/javascript' src="../Util/js/js_util.ajax.js"> </script>

    <script type='text/javascript' src="../Util/js/js_util.grilla.js"> </script>

    <script type='text/javascript' src="../Reportes/frmReporteVentas.aspx.js"> </script>

</head>
<body>
    <form id="frmReporte" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpoModal">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="icono">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Util/images/ico_mdl_cotizacion.gif" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Reportes</div>
                    <div class="css_lbl_titulo">
                        Reporte de Ventas</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:parent.fn_util_CierraModal();">
                            <img alt="" src="../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Cerrar </a>
                    </div>
                    <div id="Div1" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton" style="width: 60px;">
                        <a href="javascript:parent.fn_util_CierraModal();">
                            <img alt="" src="../Util/images/ico_acc_ver.gif" border="0" /><br />
                            Ver Reporte </a>
                    </div>
                    <div id="Div2" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton" style="width: 60px;">
                        <a href="javascript:fn_venta();">
                            <img alt="" src="../Util/images/PDT.jpeg" border="0" style="height: 35px; width: 35px" /><br />
                            Ver PDT </a>
                    </div>
                </td>
            </tr>
        </table>
        <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            padding-right: 0px;">
            <tr>
                <td class="lineas">
                </td>
            </tr>
        </table>
        <input type="hidden" name="hddCodigoCotizacion" id="hddCodigoCotizacion" value=""
            runat="server" />
        <asp:Button ID="btnGrabar" runat="server" Style="display: none;" Text="Graba" />
        <table id="tb_formulario" border="0" cellpadding="0">           
            <tr>
                <td class="label">
                    Fecha Inicial
                </td>
                <td class="input">
                    <input id="txtFechaInicial" name="txtFechaInicial" type="text" class="css_input"
                        size="12" runat="server" title="Fecha Inicial " />
                </td>
                <td class="label">
                    Fecha Final
                </td>
                <td class="input">
                    <input id="txtFechaFinal" name="txtFechaFinal" type="text" class="css_input" size="12"
                        runat="server" title="Fecha Final" />
                </td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
            </tr>
        </table>
    </div>
    <div style="display: none">
        <asp:Button ID="btnGenerar" runat="server" />
    </div>
    </form>
</body>
</html>
