<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiniestroListadoConsulta.aspx.vb"
    Inherits="Consultas_Siniestro_frmSiniestroListadoConsulta" %>

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

    <script type='text/javascript' src="frmSiniestroListadoConsulta.aspx.js"> </script>

</head>
<body>
    <form id="frmSiniestroListadoConsulta" runat="server">
    <input type="hidden" name="hddCodContrato" id="hddCodContrato" value="" runat="server" />
    <input type="hidden" name="hddCodBien" id="hddCodBien" value="" runat="server" />
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_siniestro.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Consultas</div>
                    <div class="css_lbl_titulo">
                        Siniestro :: Listado</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_buscarSiniestro(true);">
                            <img alt="" src="../../Util/images/ico_acc_buscar.gif" border="0" /><br />
                            Buscar </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_limpiar();">
                            <img src="../../Util/images/ico_acc_limpiar.gif" border="0" /><br />
                            Limpiar </a>
                    </div>
                    <div id="dv_separador" class="dv_img_boton_separador">
                        :
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_abreConsulta()">
                            <img alt="" src="../../Util/images/ico_acc_ver.gif" border="0" /><br />
                            Ver </a>
                    </div>
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_Reporte();">
                            <img alt="" src="../../Util/images/ico_acc_importar.gif" border="0" /><br />
                            Exportar </a>
                    </div>
                </td>
            </tr>
        </table>
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="lineas">
                        <asp:Button ID="btnGenerar" runat="server" Style="display: none" />
                        <asp:HiddenField ID="hidTipoBien" runat="server" Value="0" />
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <div class="dv_tabla_contenedora">
                            <table id="tb_formulario" border="0" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="label">
                                        Nro. Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtNroContrato" type="text" class="css_input" size="10" runat="server" />
                                    </td>
                                    <td class="label">
                                        Estado Contrato
                                    </td>
                                    <td class="input">
                                        <select id="cmdEstadoContrato" name="cmdEstadoContrato" runat="server" runat="server">
                                        </select>
                                    </td>
                                    <td class="label" style="width: 130px">
                                        CU Cliente
                                    </td>
                                    <td class="input">
                                        <input id="txtCUCliente" type="text" class="css_input" size="10" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Tipo Documento
                                    </td>
                                    <td class="input">
                                        <select id="cmdTipoDoc" name="cmdTipoDoc" runat="server" runat="server">
                                        </select>
                                    </td>
                                    <td class="label">
                                        Nº Documento
                                    </td>
                                    <td class="input">
                                        <input id="txtNroDocumento" type="text" class="css_input" size="13" runat="server" />
                                    </td>
                                    <td class="label" style="width: 130px">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtRazonSocial" type="text" class="css_input" size="30" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Clasificación del Bien
                                    </td>
                                    <td class="input">
                                        <select id="cmdClasificacion" name="cmdClasificacion" runat="server" runat="server">
                                        </select>
                                    </td>
                                    <td class="label">
                                        Placa Actual
                                    </td>
                                    <td class="input">
                                        <input id="txtPlaca" type="text" class="css_input" size="15" runat="server" />
                                    </td>
                                    <td class="label" style="width: 130px">
                                        Nº Motor
                                    </td>
                                    <td class="input">
                                        <input id="txtMotor" type="text" class="css_input" size="25" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Tipo Bien
                                    </td>
                                    <td class="input">
                                        <select id="cmdTipoBien" name="cmdTipoBien" runat="server" runat="server" onchange="fn_SetearTipo(this.value);">
                                            <option value="0">[-Seleccione-]</option>
                                        </select>
                                    </td>
                                    <td class="label" style="width: 150px">
                                        Ubicación
                                    </td>
                                    <td colspan="3" class="input">
                                        <input id="txtUbicacion" type="text" class="css_input" size="60" runat="server" />
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
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!-- Fin Cuerpo -->
    </form>
</body>
</html>
