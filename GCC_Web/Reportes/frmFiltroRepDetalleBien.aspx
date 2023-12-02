<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmFiltroRepDetalleBien.aspx.vb"
    Inherits="Reportes_frmFiltroRepDetalleBien" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Reporte Detalle Bien</title>
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

    <script type='text/javascript' src="frmFiltroRepDetalleBien.aspx.js"> </script>

</head>
<body>
    <form id="frmReporte" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="icono">
                    <asp:Button ID="btnGenerar" runat="server" Style="display: none" />
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Util/images/ico_mdl_cotizacion.gif" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Reportes</div>
                    <div class="css_lbl_titulo">
                        Detalle del Bien</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_Limpiar();">
                            <img src="../Util/images/ico_acc_limpiar.gif" border="0" /><br />
                            Limpiar </a>
                    </div>
                    <div id="dv_separador" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton" style="width: 90px">
                        <a href="javascript:fn_Reporte();">
                            <img alt="" src="../Util/images/ico_acc_ver.gif" border="0" /><br />
                            Generar Reporte </a>
                    </div>
                </td>
            </tr>
        </table>
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="lineas">
                        <input type="hidden" name="hddFecInicio" id="hddFecInicio" value="" runat="server" />
                        <input type="hidden" name="hddFecFin" id="hddFecFin" value="" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido" style="width: 419px">
                                    Datos de Búsqueda
                                </td>
                                <td class="botones">
                                    <img alt="" src="../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <div id="dv_datos" class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3" style="width: 520px;">
                                <tr>
                                    <td class="label" style="width: 230px">
                                        Fecha de Activación Contrato Desde
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaActIni" name="txtFechaActIni" type="text" class="css_input" size="12"
                                            runat="server" title="Fecha de Activación Desde" />
                                    </td>
                                    <td class="label" style="width: 70px">
                                        Hasta
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaActFin" name="txtFechaActFin" type="text" class="css_input" size="12"
                                            runat="server" title="Fecha de Activación Hasta" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
