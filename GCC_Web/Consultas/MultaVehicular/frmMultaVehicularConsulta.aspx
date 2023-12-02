<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMultaVehicularConsulta.aspx.vb"
    Inherits="Consultas_MultaVehicular_frmMultaVehicularConsulta" %>

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

    <script type='text/javascript' src="frmMultaVehicularConsulta.aspx.js"> </script>

</head>
<body>
    <form id="frmImpuestoVehicularRegistro" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_multa.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Consultas</div>
                    <div class="css_lbl_titulo">
                        Multa Vehicular :: Consulta</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones" style="height: 60px; vertical-align: top;">
                    <div class="dv_img_boton" id="dv_img_boton" style="border: 0">
                        <a href="javascript:fn_Volver();">
                            <img alt="" src="../../Util/images/ico_acc_cancelar.gif" border="0" /><br />
                            Volver </a>
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
                        <input id="hddRowId" type="hidden" runat="server" />
                        <input id="hddTipo" type="hidden" runat="server" />
                        <input id="hddRowIdMulta" type="hidden" runat="server" />
                        <input id="hddPlaca" type="hidden" runat="server" />
                        <input id="hddCodMulta" type="hidden" runat="server" />
                        <input id="hddNroCuotas" type="hidden" runat="server" />
                        <input id="hddSecFinanciamiento" type="hidden" runat="server" />
                        <input id="hddCodMunicipalidad" type="hidden" runat="server" />
                        <input id="hddEstadoPago" type="hidden" runat="server" />
                        <input id="hddPeriodos" type="hidden" runat="server" />
                        <input id="hddFecTransferencia" type="hidden" runat="server" />
                        <input id="btnCargarMulta" type="button" value="button" runat="server" style="display: none"
                            onclick="javascript:fn_ListarMultasLote();" />
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Datos de Búsqueda
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
                                        Municipalidad
                                    </td>
                                    <td class="input" id="tdMunicipalidad1" runat="server">
                                        <select id="ddlMunicipalidad" runat="server" name="ddlMunicipalidad">
                                            <option value='0'>[-Seleccione-]</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Placa Actual
                                    </td>
                                    <td class="input" id="tdPlaca" runat="server">
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Lista de Bienes
                                </td>
                            </tr>
                        </table>
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
                        <br />
                        <table id="tb_tabla_comunCabecera" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="titulo css_lbl_tituloContenido">
                                    Lista de Multas
                                </td>
                            </tr>
                        </table>
                        <!-- Inicio Grilla Listado -->
                        <div class="dv_tabla_contenedoraSola">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td align="left">
                                        <div id="dv_ver" class="dv_img_boton_mini" style="width: 100px; border: 0px solid #ffffff;"
                                            runat="server">
                                            <a href="javascript:fn_verImpuesto();">
                                                <img alt="" src="../../Util/images/ico_mdl_cotizacion.gif" style="width: 16px; height: 16px;"
                                                    border="0" />
                                                Ver Multa</a>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table id="jqGrid_lista_B">
                                <tr>
                                    <td />
                                </tr>
                            </table>
                            <div id="jqGrid_pager_B">
                            </div>
                        </div>
                        <!-- Fin Grilla -->
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!-- Fin Cuerpo -->
    </form>
</body>
</html>
