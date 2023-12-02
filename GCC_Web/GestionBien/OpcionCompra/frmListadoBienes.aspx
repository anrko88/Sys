<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListadoBienes.aspx.vb"
    Inherits="GestionBien_OpcionCompra_frmListadoBienes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GCC :: Sistema de Gestión de Leasing</title>
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

    <script src="../../Util/js/jquery/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery-ui-1.8.13.custom.min.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.jscrollpane.min.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.mousewheel.js"></script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.ui.global.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.validText.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.validNumber.js"> </script>

    <script type='text/javascript' src="../../Util/js/jquery/jquery.maxlength.js"> </script>

    <script type="text/javascript" src="../../Util/js/jquery/json2.js"></script>

    <script type="text/javascript" src="../../Util/js/js_global.js"></script>

    <script type='text/javascript' src="../../Util/js/js_util.modal.js"></script>

    <script type='text/javascript' src="../../Util/js/js_util.funcion.js"></script>

    <script type='text/javascript' src="../../Util/js/js_util.date.js"></script>

    <script type='text/javascript' src="../../Util/js/js_util.ajax.js"></script>

    <!-- JQGrid -->
    <link type="text/css" rel="stylesheet" href="../../Util/css/css_grilla.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jqgrid/css_grilla_A.css" />
    <link type="text/css" rel="stylesheet" href="../../Util/css/jqgrid/css_grilla_B.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="../../Util/componentes/jqgrid/themes/ui.jqgrid.css" />

    <script src="../../Util/componentes/jqgrid/js/i18n/grid.locale-sp.js" type="text/javascript"></script>

    <script src="../../Util/componentes/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>

    <!-- Local -->

    <script type='text/javascript' src="frmListadoBienes.aspx.js"> </script>

</head>
<body>
    <form id="frmListadoOpcionCompra" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_version.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo" id="divTitulo" runat="server">
                        Gestión del Bien</div>
                    <div class="css_lbl_titulo" style="width: 400px" id="divSubTitulo" runat="server">
                        Opción de Compra :: Demanda</div>
                </td>
                <td class="espacio">
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:parent.fn_util_CierraModal();">
                            <img src="../../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Cerrar </a>
                    </div>
                    <div id="Div3" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_guardar" class="dv_img_boton">
                        <div id="dv_img_boton" class="dv_img_boton">
                            <a href="javascript:fn_guardar();">
                                <img src="../../Util/images/ico_acc_grabar.gif" border="0" /><br />
                                Guardar </a>
                        </div>
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
                        <table id="tb_Bienes" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido" style="height: 25px" valign="bottom">
                                    Listado de Bienes
                                </td>
                            </tr>
                        </table>                    
                        <div class="dv_tabla_contenedora">
                            <table id="jqGrid_lista_A">
                                <tr>
                                    <td />
                                </tr>
                            </table>
                            <div id="jqGrid_pager_A">
                            </div>
                        </div>
                        <table id="tb_Demanda" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido" style="height: 25px" valign="bottom">
                                    Demanda
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
                    </td>
                </tr>
            </table>
        </div>
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
    <input type="hidden" name="hddCodContrato" id="hddCodContrato" value="" runat="server" />
    <input type="hidden" name="hddCodBien" id="hddCodBien" value="" runat="server" />
    </form>
</body>
</html>
