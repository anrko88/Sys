<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSegurosDetalle.aspx.vb" Inherits="GestionBien_frmSegurosDetalle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Detalle de Seguros</title>
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

    <script type='text/javascript' src="../Util/js/jquery/jquery.jscrollpane.min.js"> </script>

    <script type='text/javascript' src="../Util/js/jquery/jquery.mousewheel.js"> </script>

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

    <script type='text/javascript' src="frmSegurosDetalle.aspx.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dv_cuerpo">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../Util/images/ico_mdl_contrato.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Reporte de Seguros</div>
                    <div class="css_lbl_titulo">
                        Reporte :: Detalle Seguros</div>
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
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos del Seguro
                                </td>
                                <td class="botones">
                                    <img alt="" src="../Util/images/ico_resize.jpg" style="cursor: pointer" onclick="javascript:fn_doResize();" />
                                </td>
                            </tr>
                        </table>
                        <div class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label" style="width: 90px">
                                        Nro. Contrato
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNroContrato" runat="server"></asp:Label>
                                    </td>
                                    <td class="label" style="width: 90px">
                                        Cliente
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCliente" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label" style="width: 90px">
                                        Nro. Prenda
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNroPrenda" runat="server"></asp:Label>
                                    </td>
                                    <td class="label" style="width: 90px">
                                        Nro. Poliza
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNroPoliza" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label" style="width: 90px">
                                        Tipo Seguro
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTipoSeguro" runat="server"></asp:Label>
                                    </td>
                                    <td class="label" style="width: 90px">
                                        Cia. Seguro
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCiaSeguro" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label" style="width: 90px">
                                        F. Inicio Poliza
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFini" runat="server"></asp:Label>
                                    </td>
                                    <td class="label" style="width: 90px">
                                        F. Fin Poliza
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFfin" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div class="dv_tabla_contenedora">
                            <table id="jqGrid_lista_A">
                                <tr>
                                    <td />
                                </tr>
                            </table>
                            <div id="jqGrid_pager_A">
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
            <input id="hdnCodigoContrato" name="hdnCodigoContrato" type="hidden" runat="server" />
        <!-- Fin Cuerpo -->
    </div>
    </form>
</body>
</html>
