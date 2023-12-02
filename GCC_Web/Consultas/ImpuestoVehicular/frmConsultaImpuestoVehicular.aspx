<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultaImpuestoVehicular.aspx.vb"
    Inherits="Consultas_ImpuestoVehicular_frmConsultaImpuestoVehicular" %>

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

    <script type='text/javascript' src="frmConsultaImpuestoVehicular.aspx.js"> </script>

</head>
<body>
    <form id="frmConsultaImpuestoVehicular" runat="server">
    <!-- **************************************************************************************** -->
    <!-- CUERPO -->
    <!-- **************************************************************************************** -->
    <input type="button" name="btnResumenCarga" id="btnResumenCarga" onclick="javascript:fn_AbrirResumenCarga();"
        style="display: none;" />
    <div id="dv_cuerpo">
        <!-- Botones de Cabezera -->
        <table id="tb_cuerpoCabecera" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="icono">
                    <img alt="" src="../../Util/images/ico_mdl_impuesto.gif" class="jd_menu_icono" />
                </td>
                <td class="titulos">
                    <div class="css_lbl_subTitulo">
                        Consulta</div>
                    <div class="css_lbl_titulo">
                        Impuesto Vehicular :: Listado</div>
                </td>
                <td class="espacio">
                    &nbsp;
                </td>
                <td class="botones" style="height: 60px; vertical-align: top;">
                    <div id="dv_img_boton" class="dv_img_boton">
                        <a href="javascript:fn_buscar(true);">
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
                            <img src="../../Util/images/ico_acc_importar.gif" border="0" /><br />
                            Exportar </a>
                    </div>
                </td>
            </tr>
        </table>
        <div id="dv_contenedor" class="css_scrollPane">
            <table id="tb_tabla_comun" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="lineas">
                         <asp:button id="btnGenerar" runat="server" style="display: none" />
                    </td>
                </tr>
                <tr>
                    <td class="cuerpo">
                        <input id="hddRowId" type="hidden" />
                        <input id="btnCargarImpuesto" type="button" value="button" runat="server" style="display: none"
                            onclick="javascript:fn_ListarImpuesto();" />
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
                                        Nº Contrato
                                    </td>
                                    <td class="input">
                                        <input id="txtNroContrato" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        CU Cliente
                                    </td>
                                    <td class="input">
                                        <input id="txtcuCliente" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Razón Social o Nombre
                                    </td>
                                    <td class="input">
                                        <input id="txtRazonSocialNombre" type="text" class="css_input" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Tipo de documento
                                    </td>
                                    <td class="input">
                                        <select id="ddlTipoDocumento" runat="server" name="ddlTipoDocumento" runat="server">
                                            <option value='0'>[-Seleccione-]</option>
                                        </select>
                                    </td>
                                    <td class="label">
                                        Nº Documento
                                    </td>
                                    <td class="input">
                                        <input id="txtNroDocumento" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Placa Actual
                                    </td>
                                    <td class="input">
                                        <input id="txtPlaca" type="text" class="css_input"  runat="server"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Nº Lote
                                    </td>
                                    <td class="input">
                                        <input id="txtNroLote" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Año Fabricación
                                    </td>
                                    <td class="input">
                                        <input id="txtAnioFabricacion" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Periodo
                                    </td>
                                    <td class="input">
                                        <input id="txtPeriodo" type="text" class="css_input" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        F. Inscripción Registral Desde
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaInscripcionDesde" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        F. Inscripción Registral Hasta
                                    </td>
                                    <td class="input">
                                        <input id="txtFechaInscripcionHasta" type="text" class="css_input" runat="server" />
                                    </td>
                                    <td class="label">
                                        Estado Pago
                                    </td>
                                    <td class="input">
                                        <select id="ddlEstadoPago" name="ddlEstadoPago" runat="server">
                                            <option value='0'>[-Seleccione-]</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Estado Cobro
                                    </td>
                                    <td class="input">
                                        <select id="ddlEstadoCobro" runat="server" name="ddlEstadoCobro">
                                            <option value='0'>[-Seleccione-]</option>
                                        </select>
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
